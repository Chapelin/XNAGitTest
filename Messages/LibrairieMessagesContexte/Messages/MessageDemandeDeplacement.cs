using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibrairieMessagesContexte;
using LibrairieUtil;

namespace LibrairieMessagesContexte.Messages
{
    public class MessageDemandeDeplacement : Message, IMessage
    {


      

        /// <summary>
        /// Construit un MessageDemandeDeplacement
        /// </summary>
        public MessageDemandeDeplacement()
            : base(TypeMessage.DemandeDeplacement, 1)
        {
            Console.WriteLine("MessageDemandeDeplacement : instance créée");
        }


        

       

    }
}
