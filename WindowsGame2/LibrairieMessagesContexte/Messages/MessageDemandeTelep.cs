using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibrairieMessagesContexte;
using LibrairieUtil;

namespace LibrairieMessagesContexte.Messages
{
    public class MessageDemandeTelep : Message, IMessage
    {


      

        /// <summary>
        /// Construit un MessageDemandeCarte
        /// </summary>
        public MessageDemandeTelep()
            : base(TypeMessage.DemandeTelep, 1)
        {
            Console.WriteLine("MessageDemandeTelep : instance créée");
        }




      

       

    }
}
