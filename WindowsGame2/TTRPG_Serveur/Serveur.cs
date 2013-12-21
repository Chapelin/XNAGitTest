using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using CommunXnaFree.Deplacement;
using CommunXnaFree.Spacialisation;
using LibrairieMessagesContexte;
using LibrairieUtil;
using ClientServeur;
using Commun.Map;
using System.Collections;
using LibrairieMessagesContexte.Messages;

namespace TTRPG_Serveur
{
    public class Serveur
    {


        //TODO:Temporaire
        public Dictionary<string,Coordonnees> coordonnesArriveesCartes = new Dictionary<string, Coordonnees>(){
        {"0",new Coordonnees(10,13)},
        {"1",new Coordonnees(18,8)},
        {"2", new Coordonnees(1,7)},
        {"3", new Coordonnees(10,1)},
        {"4",new Coordonnees(9,13)},
        {"5",new Coordonnees(1,6)},
        {"6", new Coordonnees(18,6)},
        {"7", new Coordonnees(9,1)}
        }; 

        //Dico des clients
        /// <summary>
        /// Table (coupleIpPourt,joueur)
        /// </summary>
        public Hashtable Annuaire;
        /// <summary>
        /// Dicote (carte, ListeJoueurs)
        /// </summary>
        public static Dictionary<CarteEcran, ListeJoueur> Contenu;

        public Serveur()
        {
            Contenu = new Dictionary<CarteEcran, ListeJoueur>();
            var ip = GestionReseau.GetMyLocalIp();
            Console.WriteLine("Mon ip : " + ip);
            var recepteur = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            recepteur.Bind(new IPEndPoint(ip, Constantes.PORT_RECEPTION_SERVEUR));
            recepteur.Listen(10);
            this.Annuaire = new Hashtable();
            while (true)
            {

                try
                {
                    var reponse = recepteur.Accept();
                    Console.WriteLine("Connexion acceptée de " + reponse.RemoteEndPoint);
                    var t = new Thread(TraiterClient);
                    t.Start(reponse);
                }
                catch (SocketException)
                {

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        /// <summary>
        /// Méthode threadée. Permet de suivre un client
        /// </summary>
        /// <param name="socket">Socket conecté au client</param>
        public void TraiterClient(Object socket)
        {
            var client = (Socket)socket;
            var buffer = new byte[2048];
            //recevoir hors boucle la premiere communication du client : message comportant son port d'ecoute
            client.Receive(buffer);
            var ipep = (IPEndPoint)client.RemoteEndPoint;
            var j = new Joueur("Test", ipep.Address);
            var tempor = MessageFactory.DecoderMessage(buffer);//, this);
            InterpreterMessage(tempor, j);
            var cp = new CoupleIpPort(ipep.Address, j.EmetteurJoueur.Port());
            Console.WriteLine("Nouveau client : " + ipep.Address + ":" + j.EmetteurJoueur.Port());
            this.Annuaire.Add(cp, j);
            j.EmetteurJoueur.Connecter();

            while (true)
            {
                try
                {
                    client.Receive(buffer);
                    //on recupere le emssage
                    var temp = MessageFactory.DecoderMessage(buffer);//, this);
                    InterpreterMessage(temp, j);


                }
                catch (SocketException)
                {

                    this.TraiterDeconnection(j,cp);
                   
                    break;

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }

            }
        }

        private void TraiterDeconnection(Joueur j, CoupleIpPort cp)
        {
            j.EmetteurJoueur.Deconnecter();
            var liste = CalculerListJouersAPrevenir(j);
            var mess = MessageFactory.GetInstanceOf(TypeMessage.DeconnexionJoueur);
            mess.PreparerMessage(j.UiUnique);
            this.InformerListeJoueur(liste, j, mess);
            this.Annuaire.Remove(cp);
            Contenu.Values.FirstOrDefault(x => x.Contains(j)).RetirerJoueur(j);
        }

        private void InterpreterMessage(object message, Joueur j)
        {
            var mess = (IMessage)message;
            switch (mess.getType())
            {
                case TypeMessage.DemandeCarte:
                    this.DemandeCarte(mess.elements[0], j);
                    break;
                case TypeMessage.DemandeDeplacement:
                    this.DemandeDeplacement(mess.elements[0], j.EmetteurJoueur);
                    break;
                case TypeMessage.Deplacement:
                    //deplacement

                    break;
                case TypeMessage.IndicationClick:
                    //indicationclick
                    break;
                case TypeMessage.ReponseDeplacement:
                    //reponsedplacement
                    break;
                case TypeMessage.Texte:
                    //texte
                    break;
                case TypeMessage.IndiquerPort:
                    IndiquerPortJoueur(mess.elements[0], j);
                    break;
                case TypeMessage.DemandeTelep:
                    TraiterTeleportation(mess.elements[0], j);
                    break;
                case TypeMessage.Stop:
                    TraiterStop(mess.elements[0], j);
                    break;
                default:
                    //erreur
                    break;
            }
        }

        private void TraiterStop(string s, Joueur joueur)
        {
            var coord = Coordonnees.FromString(s);
            //TODO verifier si coordonnées possible;
            joueur.Position = coord;


        }

        private void TraiterTeleportation(string idTeleporteur, Joueur joueur)
        {
            var carte = "carte2";
            if (Convert.ToInt32(idTeleporteur) < 4)
                carte = "carte3";
            //TODO : verifier si l'id de la telep est probable et recuperer l'id de carte adéquat
            if(!this.coordonnesArriveesCartes.ContainsKey(idTeleporteur))
                throw new ArgumentException("Erreur, l'id de teleport n'est pas bon");
            else
            {
                Console.WriteLine("Id teleport : "+idTeleporteur);
            }
            joueur.Position = this.coordonnesArriveesCartes[idTeleporteur];
            // puis positionner j.position.x et Y comme il faut
            DemandeCarte(carte,joueur);
        }

        private void IndiquerPortJoueur(string p, Joueur j)
        {
            int port = Int32.Parse(p);
            var tempo = new Emetteur(j.Adresse.ToString(), port);
            j.EmetteurJoueur = tempo;
        }

        public void DemandeCarte(string valeur, Joueur j)
        {
            Console.WriteLine("Carte demandée : " + valeur + " depuis " + j.EmetteurJoueur);
            var t = MessageFactory.GetInstanceOf(TypeMessage.ReponseCarte);
            if(j.Position == null)
                j.Position = new Coordonnees(5, 5);
            var nomCarte =valeur;
            t.PreparerMessage(new object[] { nomCarte, j.Position.X, j.Position.Y });
            j.EmetteurJoueur.envoyer(t);
            

            if (!Contenu.Keys.Any(carte => carte.NomCarte == nomCarte))
            {
                var sr = new StreamReader(@"Cartes\"+nomCarte+".txt");
                CarteEcran ce = CarteReader.InterpreterCarte(sr);
                ce.NomCarte = nomCarte;
                Contenu.Add(ce,new ListeJoueur());
            }
            //un joueur ne peux etre contenu dans deux cartes à la fois
            var temp = Contenu.Values.FirstOrDefault(x => x.Contains(j));
            if(temp!=null)
                temp.RetirerJoueur(j);
            Contenu[Contenu.Keys.First(carte => carte.NomCarte == nomCarte)].AjouteJoueur(j);

            this.NotifierConnexion(j);
        }




        public void IndiqueClick(string p)
        {
            Console.WriteLine("Click reçu : " + p);
        }



        public void DemandeDeplacement(string p, IEmetteur emm)
        {
            Console.WriteLine("Chemin demandé : " + p);
            IMessage temp = MessageFactory.GetInstanceOf(TypeMessage.ReponseDeplacement);
            Chemin c = Chemin.GetFromString(p);
            //notifier autres joueurs
            var t = new CoupleIpPort(emm.adresse(), emm.Port());
            //trouver le joueur
            var j = (Joueur)this.Annuaire[t];
            Console.WriteLine(c);
            var result = this.VerifierChemin(c, j);
            temp.PreparerMessage(new object[] { result.ToString() });
            emm.envoyer(temp);
            if (result)
            {

                //puis recuperer la listejoueur de ceux de son écran
                //à optimiser
                //TODO:DEBUG ICI
                //var lj = Contenu.Values.Where(l => l.Contains(j)).FirstOrDefault();
                ListeJoueur lj = CalculerListJouersAPrevenir(j);

                j.Position = c.AppliquerAPosition(j.Position);

                //ici lj contient l'ensemble des joueurs de la carte
                
                if (lj != null)
                    NotifierDeplacement(c, j, lj);
                else
                    Console.WriteLine("pas de jouers à notif pour le deplacement de " + j.UiUnique);
                //this.NotifierDeplacement(c,j,lj) - chemin, joueurs qui bouge, liste des joueurs à notifier
            }
            else
            {
                Console.WriteLine("Deplacement refusé");
            }
        }

        /// <summary>
        /// Verifie que le chemin est bien franchissable
        /// </summary>
        /// <param name="chemin">Chemin à suivre</param>
        /// <param name="joueur">Joueur qà tester</param>
        /// <returns>True si le chemin est valide</returns>
        private bool VerifierChemin(Chemin chemin, Joueur joueur)
        {
            Chemin temp = chemin.Clone();
            var res = true;
            var carte = Contenu.Keys.FirstOrDefault(cont => Contenu[cont].Contains(joueur));
            Coordonnees c = joueur.Position;
            while(temp.TailleParcours>0)
            {
                c = c + temp.Avance();
                res &= carte.GetCase(c.X, c.Y).Franchissable;
                if (!res)
                    break;
            }
            return res;

        }

        private static void NotifierDeplacement(Chemin c, Joueur j, ListeJoueur lj)
        {
            //envoie message noptif
            var message = MessageFactory.GetInstanceOf(TypeMessage.Notif);
            message.PreparerMessage((object)j.UiUnique.ToString(), (object)c.ToString());
            foreach (var joueur in lj.ToList().Where(joueur => joueur != j))
            {
                joueur.EmetteurJoueur.envoyer(message);
            }
        }

        private void NotifierConnexion(Joueur j)
        {
            var lj = CalculerListJouersAPrevenir(j);
            if (lj != null)
            {
                var message = MessageFactory.GetInstanceOf(TypeMessage.ConnexionJoueur);
                message.PreparerMessage(j.UiUnique, string.Empty, j.Position.X, j.Position.Y);
                foreach (var joueur in lj.ToList().Where(joueur => joueur != j))
                {
                    joueur.EmetteurJoueur.envoyer(message);
                }
            }

            this.InformerDesJoueursConnectes(j);
        }

        /// <summary>
        /// Permet d'informer le jour J des joueurs presents sur la carte
        /// </summary>
        /// <param name="j"></param>
        private void InformerDesJoueursConnectes(Joueur j)
        {
            var lj = CalculerListJouersAPrevenir(j);
            if (lj != null)
            {
                foreach (var joueurCo in lj.ToList().Where(joueur => joueur != j))
                {
                    var message = MessageFactory.GetInstanceOf(TypeMessage.ConnexionJoueur);
                    message.PreparerMessage(joueurCo.UiUnique, string.Empty, joueurCo.Position.X, joueurCo.Position.Y);
                    j.EmetteurJoueur.envoyer(message);
                }
            }
        }


        private void InformerListeJoueur(ListeJoueur lj, Joueur j, IMessage mes, bool sauflui =true)
        {
            if (lj != null)
            {
                foreach (var joueurCo in lj.ToList().Where(joueur => joueur != j || !sauflui))
                {
                   joueurCo.EmetteurJoueur.envoyer(mes);
                }
            }
        }

        private static ListeJoueur CalculerListJouersAPrevenir(Joueur j)
        {
            return Contenu.Values.FirstOrDefault(cont => cont.Contains(j));
        }
    }
}
