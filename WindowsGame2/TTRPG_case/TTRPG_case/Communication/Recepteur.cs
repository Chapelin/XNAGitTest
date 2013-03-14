using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using LibrairieUtil;
using LibrairieMessagesContexte.Messages;

namespace TTRPG_case.Communication
{
    /// <summary>
    /// Classe de reception coté client
    /// </summary>
    public class Recepteur
    {
        #region variables
        /// <summary>
        /// Port d'écoute
        /// </summary>
        readonly int _portReception;
        
        //Client
        readonly Game1 _client;

        /// <summary>
        /// Socket de recption
        /// </summary>
        readonly Socket _reception;

        /// <summary>
        /// IP perso
        /// </summary>
        readonly IPAddress _ip;

        /// <summary>
        /// Thread d'ecoute (pas de bloquage appli)
        /// </summary>
        Thread _recepteur;

        #endregion

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="port">Port d'ecoute</param>
        /// <param name="cl"></param>
        public Recepteur(int port, Game1 cl)
        {
            this._client = cl;
            this._portReception = port;
            this._ip = GestionReseau.GetMyLocalIp();
            _reception = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        /// <summary>
        /// Initialisation de l'ecouteur : parametrage
        /// </summary>
        public void Initialiser()
        {
            var tempo = new IPEndPoint(_ip, _portReception);
            _reception.Bind(tempo);
            _reception.Listen(1);
        }

        /// <summary>
        /// Lancement de l'ecoute threadée
        /// </summary>
        public void LancerEcoute()
        {
            this._recepteur = new Thread(LancerEcouteThread);
            this._recepteur.Start();
            
        }

        /// <summary>
        /// Boucle d'écoute
        /// </summary>
        private void LancerEcouteThread()
        {
            while (true)
                try
                {
                    var connexionServer = _reception.Accept();
                    this.TraiterConnexion(connexionServer);
                }
                catch (Exception e)
                {
                    Console.WriteLine("REcepteur Ko sur l'ecoute : " + e);
                }
        }

        /// <summary>
        /// Traitement de la connexion au serveur (boucle de rececption)
        /// </summary>
        /// <param name="connexionServer">Socket connecté</param>
        private void TraiterConnexion(Socket connexionServer)
        {
            var buf = new byte[2048];
            while (true)
            {
                try
                {
                    connexionServer.Receive(buf);
                    var temp = MessageFactory.DecoderMessage(buf);
                   
                    //thread : interpreterMessage()
                    var tr = new Thread(this.InterpreterMessage);
                    tr.Start(temp);
                 
                    
                }
                catch (SocketException e)
                {

                }
            }
        }

        private void InterpreterMessage(object message)
        {
            var mess = (IMessage)message;
            switch (mess.getType())
            {

                case TypeMessage.ReponseCarte :
                    this._client.ReponseCarte(mess.elements[0], Int32.Parse(mess.elements[1]), Int32.Parse(mess.elements[2]));
                    break;

                case TypeMessage.IndicationClick : 
                    //indicationclick
                    break;
                case TypeMessage.ReponseDeplacement : 
                    //reponsedplacement
                    this._client.ReponseDeplacement(mess.elements[0]);
                    break;
                case TypeMessage.Texte : 
                    //texte
                    break;
                case TypeMessage.Notif :
                    this._client.NotifDepl(mess.elements[0], mess.elements[1]);
                    break;
                case TypeMessage.ConnexionJoueur :
                    this._client.ConnexionNvxJoueur(mess.elements[0], mess.elements[1], mess.elements[2],mess.elements[3]);
                    break;
            default : 
                    //erreur
                    break;
            }
            
            
        }


        
    }
}
