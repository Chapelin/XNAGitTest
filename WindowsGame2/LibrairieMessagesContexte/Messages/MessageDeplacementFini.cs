using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibrairieMessagesContexte.Messages;

namespace LibrairieMessagesContexte.Messages
{
    //renvoi le nom de la carte et la position de depart
    public class MessageDeplacementFini : Message, IMessage
    {
         /// <summary>
        /// Construit un MessageDemandeDeplacement
        /// </summary>
        public MessageDeplacementFini()
            : base(TypeMessage.DeplacementFini, 0)
        {
            Console.WriteLine("MessageDeplacementFini : instance créée");
        }
    }
}
