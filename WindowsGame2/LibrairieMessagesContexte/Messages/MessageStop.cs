using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibrairieMessagesContexte;
using LibrairieUtil;

namespace LibrairieMessagesContexte.Messages
{
    public class MessageStop : Message
    {


      

        /// <summary>
        /// Construit un MessageDemandeCarte
        /// </summary>
        public MessageStop()
            : base(TypeMessage.Stop, 1)
        {
            Console.WriteLine("MessageStop : instance créée");
        }


       

       

    }
}
