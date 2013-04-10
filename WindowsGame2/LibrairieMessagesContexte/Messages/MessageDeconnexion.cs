using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibrairieMessagesContexte;
using LibrairieUtil;

namespace LibrairieMessagesContexte.Messages
{
    /// <summary>
    /// 1 : id
    /// 2 : chemin
    /// </summary>
    public class MessageDeconnection : Message, IMessage
    {


      

        /// <summary>
        /// Construit un MessageDemandeCarte
        /// </summary>
        public MessageDeconnection()
            : base(TypeMessage.DeconnexionJoueur,1)
        {
            
        }


       

       

    }
}
