using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TTRPG_case;

namespace LibrairieMessagesContexte.Messages
{
    public enum TypeMessage
    {
        Deplacement = 1,
        Texte = 2,
        DemandeCarte = 3,
        IndicationClick = 4,
        DemandeDeplacement = 5,
        ReponseDeplacement = 6,
        IndiquerPort = 7,
        ReponseCarte = 8,
        Notif = 9,
        ConnexionJoueur = 10,
        DeconnexionJoueur =11,
        DemandeTelep = 12,
        DeplacementFini = 13,
        Stop = 14
    }

    public static class MessageFactory
    {

        /// <summary>
        /// Retourne une instance de message de type indiqué
        /// </summary>
        /// <param name="type">Type du message voulu</param>
        /// <returns></returns>
        public static IMessage GetInstanceOf(TypeMessage type)
        {
            IMessage retour;

            switch (type)
            {
                case TypeMessage.DemandeCarte:
                    retour = new MessageDemandeCarte();
                    break;
                case TypeMessage.IndicationClick:
                    retour = new MessageIndicationClick();
                    break;
                case TypeMessage.DemandeDeplacement:
                    retour = new MessageDemandeDeplacement();
                    break;
                case TypeMessage.ReponseDeplacement:
                    retour = new MessageReponseDeplacement();
                    break;
                case TypeMessage.IndiquerPort:
                    retour = new MessageIndiquerPort();
                    break;
                case TypeMessage.ReponseCarte:
                    retour = new MessageReponseCarte();
                    break;
                case TypeMessage.Notif:
                    retour = new MessageNotif();
                    break;
                case TypeMessage.ConnexionJoueur:
                    retour = new MessageConnexion();
                    break;
                case TypeMessage.DeconnexionJoueur:
                    retour = new MessageDeconnection();
                    break;
                case TypeMessage.DemandeTelep:
                    retour = new MessageDemandeTelep();
                    break;
                    case TypeMessage.Stop:
                    retour = new MessageStop();
                    break;
                default:
                    retour = null;
                    break;
            }


            return retour;

        }

        /// <summary>
        /// Permet de deserialiser une donnée reçue
        /// </summary>
        /// <param name="data">Donnée reçue</param>
        /// <param name="cont">Contexte</param>
        /// <returns>IMessage, du type du message envoyé, comportant les infos </returns>
        public static IMessage DecoderMessage(byte[] data)
        {
            IMessage t = null;
            byte[] conteneur_type = data.Slice<byte>(0, Message.TAILLE_TYPE);
            byte[] conteneur_taille = data.Slice<byte>(Message.TAILLE_TYPE, Message.TAILLE_TYPE+Message.TAILLE_COMPTE);
            int taille_data = int.Parse(MessageFactory.DecoderString(conteneur_taille, -1));
            byte[] conteneur_data = data.Slice<byte>(Message.TAILLE_COMPTE + Message.TAILLE_TYPE, Message.TAILLE_TYPE+Message.TAILLE_COMPTE + taille_data);
            TypeMessage reçu = (TypeMessage)Enum.Parse(typeof(TypeMessage), MessageFactory.DecoderString(conteneur_type,-1));
            t = MessageFactory.GetInstanceOf(reçu);
            t.setMessageString(DecoderString(conteneur_data, -1));
            //t.setContexte(cont);
            
            return t;
        }

        /// <summary>
        /// Met en forme un string en bytes
        /// </summary>
        /// <param name="donnees">données à transformer</param>
        /// <param name="tmax">taille du tableau</param>
        /// <returns></returns>
        public static byte[] MettreEnForme(string donnees, int tmax)
        {
            byte[] temp = new byte[tmax];
            byte[] data = UnicodeEncoding.Unicode.GetBytes(donnees);
            if(temp.Length<data.Length)
                throw new Exception("Erreur, la taille des données est sur "+data.Length + " alors qu'on est à "+tmax+ "maximum");
            data.CopyTo(temp, 0);
            for (int i = data.Length; i < tmax; i++)
                temp[i] = 0;

            return temp;
        }

        /// <summary>
        /// Decode une partie d'un tableau de byte en string
        /// </summary>
        /// <param name="data">tableau de byte</param>
        /// <param name="nombre">nombre de byte à prendre</param>
        /// <returns></returns>
        public static string DecoderString(byte[] data, int nombre)
        {
            if (nombre > 0 && nombre < data.Length)
                data = data.Slice<byte>(0, nombre);
            return UnicodeEncoding.Unicode.GetString(data);
        }

       

    }
}
