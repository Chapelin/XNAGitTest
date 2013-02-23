using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using LibrairieMessagesContexte;
using LibrairieMessagesContexte.Messages;
namespace ClientServeur
{
    /// <summary>
    /// Classe representant un emetteur de données
    /// 
    /// </summary>
    public class Emetteur : IEmetteur
    {

        #region variables

        /// <summary>
        /// Ip du serveur ciblé
        /// </summary>
        IPAddress ipServ;

        /// <summary>
        /// Port sur le serveur
        /// </summary>
        int Portsert;

        /// <summary>
        /// Socket d'envoi de donnée
        /// </summary>
        Socket socket_envoi;

        #endregion

        #region accesseurs

        public IPAddress adresse()
        {
            return this.ipServ; 
        }

        public int Port()
        {
            return this.Portsert;
        }

        #endregion
        #region constructeur

        /// <summary>
        /// Creation d'un emetteur
        /// </summary>
        /// <param name="IpServeur">Ip de la cible des messages</param>
        /// <param name="portserveur">Port ouvert chez la cible</param>
        public Emetteur(string IpServeur, int portserveur)
        {
            this.ipServ = IPAddress.Parse(IpServeur);
            this.Portsert = portserveur;
            socket_envoi = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
        #endregion

        #region connexion.deconnection
        /// <summary>
        /// Methode permettant d'etablir une connexion avec la cible
        /// </summary>
        /// <returns>True si la connexion est effectuée</returns>
        public bool Connecter()
        {
            bool retour = false;
            try
            {
                this.socket_envoi.Connect(new IPEndPoint(this.ipServ, this.Portsert));
                retour = true;
                Console.WriteLine("Connexion Ok vers " + socket_envoi.RemoteEndPoint);
            }
            catch { }

            return retour;
        }


        /// <summary>
        /// Methode de deconnection du serveur, proprement
        /// </summary>
        public void Deconnecter()
        {
            Console.WriteLine("Deconnection de " + this.socket_envoi);
            this.socket_envoi.Disconnect(false);
            Console.WriteLine("Deconnecté.");
        }

        #endregion


        /// <summary>
        /// Methode d'envoie d'un message
        /// </summary>
        /// <param name="message">Message preparé à envoyer</param>
        public void envoyer(IMessage message)
        {
            byte[] donnes = message.getMessageBytes();
            try
            {
                this.socket_envoi.Send(donnes);
                //Console.WriteLine("Données envoyées : " + message.ToString());
                Console.WriteLine("Donnees envoyées de type " + message.getType());
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception durant l'envoie de données : " + message.ToString() + "\n " + e.ToString());
            }
        }


    }
}
