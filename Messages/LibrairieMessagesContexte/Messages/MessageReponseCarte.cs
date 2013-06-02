using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibrairieMessagesContexte.Messages;

namespace LibrairieMessagesContexte.Messages
{
    //renvoi le nom de la carte et la position de depart
    public class MessageReponseCarte : Message, IMessage
    {
         /// <summary>
        /// Construit un MessageDemandeDeplacement
        /// </summary>
        public MessageReponseCarte()
            : base(TypeMessage.ReponseCarte, 3)
        {
            Console.WriteLine("MessageReponseCarte : instance créée");
        }
    }
}
