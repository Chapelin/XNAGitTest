using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibrairieMessagesContexte;

namespace LibrairieMessagesContexte.Messages
{
    public class MessageIndiquerPort : Message, IMessage
    {


      

        /// <summary>
        /// Construit un MessageDemandeCarte
        /// </summary>
        public MessageIndiquerPort()
            : base(TypeMessage.IndiquerPort, 1)
        {
            Console.WriteLine("MessageIndiquerPort : instance créée");
        }


       

       

    }
}
