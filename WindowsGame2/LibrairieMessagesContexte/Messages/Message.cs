using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibrairieMessagesContexte.Messages;

namespace LibrairieMessagesContexte.Messages
{

   
    /// <summary>
    /// Classe mère des messages
    /// </summary>
    public class Message : IMessage
    {
        #region Constantes

        public const char SEPARATEUR_PARAM = '°';
        /// <summary>
        /// taille max des data
        /// </summary>
        public static int TAILLE_MAX = 2036;

        /// <summary>
        /// Taille du champ indiquant la taille des data
        /// </summary>
        public static int TAILLE_COMPTE = 8;

        /// <summary>
        /// Taille du champ indiquant le type du message
        /// </summary>
        public static int TAILLE_TYPE = 4;

        #endregion

        #region variables
        /// <summary>
        /// MEssage sous forme de byte[]
        /// </summary>
        protected byte[] _message;

        /// <summary>
        /// Type du message
        /// </summary>
        protected TypeMessage _typeDuMessage;

        /// <summary>
        /// Nombre de parametresprix en comptes
        /// </summary>
        protected int _numParam;

        /// <summary>
        /// Valeur en string des data
        /// </summary>
        protected string _valeur;


        #endregion

        #region getters/setter
        public string MessageString
        {
            get { return this._valeur; }
            set { this._valeur = value; }
        }

        public TypeMessage getType()
        {
            return this._typeDuMessage;
        }

       

        public byte[] getMessageBytes()
        {
            return this._message;
        }


      

        public void setMessageString(string t)
        {
            this._valeur = t;
        }
        #endregion

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="type">Type du message</param>
        /// <param name="nombre">Nombre de parametres pà prendre en compte</param>
        public Message(TypeMessage type, int nombre)
        {
            this._typeDuMessage = type;
            this._numParam = nombre;
           
        }

        public String[] elements
        {
            get { return this._valeur.Split(SEPARATEUR_PARAM); }
        }

        /// <summary>
        /// Ovverride du toString
        /// </summary>
        /// <returns>Affiche une string indiquant certaines valeurs du message</returns>
        public override string ToString()
        {
            var retour = new StringBuilder();
            retour.Append("Type de message : " + this._typeDuMessage).Append(" | valeur string : " + this._valeur);
            return retour.ToString();
            
        }





        #region implementation interface
        /// <summary>
        /// Prepare le MessageDemandeCarte avec les parametres indiquée
        /// </summary>
        /// <param name="parametres">valeurs à mettre dans le emssage</param>
        public void PreparerMessage(object[] parametres)
        {

            this._message = new byte[Message.TAILLE_COMPTE + Message.TAILLE_MAX + Message.TAILLE_TYPE];
            if (parametres.Length != this._numParam)
            {
                throw new Exception("Non concordance des parametres (" + parametres.Length + ") avec le nombre prévu (" + this._numParam + ")");
            }
            string stringtemp = String.Empty;
            for (int i = 0; i < this._numParam; i++)
            {
                stringtemp += parametres[i].ToString();
                if (i != this._numParam-1)
                    stringtemp += "°";
            }
            byte[] temp = MessageFactory.MettreEnForme(stringtemp, Message.TAILLE_MAX);
            byte[] receptacle_type = MessageFactory.MettreEnForme(((int)this._typeDuMessage).ToString(), Message.TAILLE_TYPE);
            byte[] receptacle_taille = MessageFactory.MettreEnForme(UnicodeEncoding.Unicode.GetByteCount(stringtemp).ToString(), Message.TAILLE_COMPTE);
            receptacle_type.CopyTo(this._message, 0);
            receptacle_taille.CopyTo(this._message, Message.TAILLE_TYPE);
            temp.CopyTo(this._message, Message.TAILLE_TYPE + Message.TAILLE_COMPTE);
            //Console.WriteLine("Message preparé : " + this._message.Tostring() + "  de type " + this._typeDuMessage);
        }



        #endregion
    }
}
