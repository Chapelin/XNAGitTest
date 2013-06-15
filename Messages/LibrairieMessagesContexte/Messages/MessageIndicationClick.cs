using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibrairieMessagesContexte;

namespace LibrairieMessagesContexte.Messages
{
    public class MessageIndicationClick : Message, IMessage
    {


      

        #region getters et setters
  

        #endregion

        /// <summary>
        /// Construit un MessageDemandeCarte
        /// </summary>
        public MessageIndicationClick():base(TypeMessage.IndicationClick,2)
        {
            Console.WriteLine("MessageIndicationClick : instance créée");
        }


      

       

    }
}
