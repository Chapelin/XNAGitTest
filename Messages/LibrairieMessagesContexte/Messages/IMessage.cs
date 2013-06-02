using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibrairieMessagesContexte.Messages;
using LibrairieMessagesContexte;

namespace LibrairieMessagesContexte.Messages
{
    public interface IMessage
    {
        void PreparerMessage(params object[] parametres);
        TypeMessage getType();
        //int getNombreParam;
        byte[] getMessageBytes();
        void setMessageString(string message);
        String[] elements { get; }
    }
}
