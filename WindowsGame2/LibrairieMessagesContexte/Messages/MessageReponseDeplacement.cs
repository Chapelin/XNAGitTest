using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibrairieMessagesContexte;
using LibrairieUtil;

namespace LibrairieMessagesContexte.Messages
{
    public class MessageReponseDeplacement : Message, IMessage
    {


      

        /// <summary>
        /// Construit un MessageDemandeCarte
        /// </summary>
        public MessageReponseDeplacement()
            : base(TypeMessage.ReponseDeplacement, 1)
        {
            Console.WriteLine("MessageReponseDeplacement : instance créée");
        }




      

       

    }
}
