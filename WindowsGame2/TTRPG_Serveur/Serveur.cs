using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using LibrairieMessagesContexte;
using LibrairieUtil;
using ClientServeur;
using Commun.Deplacement;
using Commun.Map;
using System.Collections;
using LibrairieMessagesContexte.Messages;

namespace TTRPG_Serveur
{
    public class Serveur
    {
    

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
                catch (SocketException e)
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
                    InterpreterMessage(temp,j);

                    
                }
                catch (SocketException)
                {
                    j.EmetteurJoueur.Deconnecter();
                    this.Annuaire.Remove(cp);
                    break;
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }

            }
        }

        private void InterpreterMessage(object message, Joueur j)
        {
            var mess = (IMessage)message;
            switch (mess.getType())
            {
                case TypeMessage.DemandeCarte:
                    this.DemandeCarte(mess.elements[0],j);
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
                default:
                    //erreur
                    break;
            }
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
            t.PreparerMessage(new object[] { "carte2",5,5 });
            j.EmetteurJoueur.envoyer(t);
            //TODO : temporaire
            if (Contenu.Keys.Count == 0)
                Contenu.Add(new CarteEcran(), new ListeJoueur());
            Contenu[Contenu.Keys.First()].AjouteJoueur(j);
            this.NotifierConnexion(j);
            //endtodo
            
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
            Console.WriteLine(c);
            temp.PreparerMessage(new object[] { "true"});
            //notifier autres joueurs
            var t = new CoupleIpPort(emm.adresse(), emm.Port());
            //trouver le joueur
            var j = (Joueur)this.Annuaire[t];
            //puis recuperer la listejoueur de ceux de son écran
            //à optimiser
            //TODO:DEBUG ICI
            //var lj = Contenu.Values.Where(l => l.Contains(j)).FirstOrDefault();
            ListeJoueur lj = CalculerListJouersAPrevenir(j);

            //ici lj contient l'ensemble des joueurs de la carte
            emm.envoyer(temp);
            if (lj != null)
                NotifierDeplacement(c, j, lj);
            else
                Console.WriteLine("pas de jouers à notif pour le deplacement de " + j.UiUnique);
            //this.NotifierDeplacement(c,j,lj) - chemin, joueurs qui bouge, liste des joueurs à notifier
        }

        private static void NotifierDeplacement(Chemin c, Joueur j, ListeJoueur lj)
        {
            //envoie message noptif
            var message = MessageFactory.GetInstanceOf(TypeMessage.Notif);
                    message.PreparerMessage((object)j.UiUnique.ToString(),(object)c.ToString());
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
                message.PreparerMessage(j.UiUnique, string.Empty);
                foreach (var joueur in lj.ToList().Where(joueur => joueur != j))
                {
                    joueur.EmetteurJoueur.envoyer(message);
                }
            }
        }


        private static ListeJoueur CalculerListJouersAPrevenir(Joueur j)
        {
            ListeJoueur lj = null;
            foreach (var cont in Contenu.Values)
            {
                if (cont.Contains(j))
                {
                    lj = cont;
                    break;
                }

            }
            return lj;
        }
    }
}
