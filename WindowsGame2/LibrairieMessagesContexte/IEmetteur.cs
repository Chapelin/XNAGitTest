using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibrairieMessagesContexte.Messages;
using System.Net;

namespace LibrairieMessagesContexte
{
    public interface IEmetteur
    {
        void envoyer(IMessage message);
        int Port();
        IPAddress adresse();

    }
}
