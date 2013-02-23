using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibrairieMessagesContexte.Messages
{
    public class MessageConnexion : Message
    {
        // 1 : ouid joueur
        // 2 nom skin ?
        public MessageConnexion()
            : base(TypeMessage.ConnexionJoueur,2)
        {
            
        }
    }
}
