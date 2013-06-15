using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibrairieMessagesContexte;

namespace LibrairieMessagesContexte.Messages
{
    public class MessageDemandeCarte : Message
    {


      

        /// <summary>
        /// Construit un MessageDemandeCarte
        /// </summary>
        public MessageDemandeCarte()
            : base(TypeMessage.DemandeCarte, 1)
        {
            Console.WriteLine("MessageDemandeCarte : instance créée");
        }


       

       

    }
}
