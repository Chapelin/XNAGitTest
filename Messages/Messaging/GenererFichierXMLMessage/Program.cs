using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LibrairieMessagesContexte;

namespace GenererFichierXMLMessage
{
    class Program
    {

        

        public static void Main(string[] args)
        {
            List<ObjectMessage> listeMessages = new List<ObjectMessage>();

            var temp = new ObjectMessage() {Nom="MessageConnexion", NombreArg = 4};
            listeMessages.Add(temp);
            temp = new ObjectMessage() { Nom = "MessageReponseCarte", NombreArg = 3 };
            listeMessages.Add(temp);
            temp = new ObjectMessage() { Nom = "MessageReponseDeplacement", NombreArg = 1 };
            listeMessages.Add(temp);
            temp = new ObjectMessage() { Nom = "MessageDeconnexion", NombreArg = 1 };
            listeMessages.Add(temp);
            temp = new ObjectMessage() { Nom = "MessageDemandeCarte", NombreArg = 1 };
            listeMessages.Add(temp);
            temp = new ObjectMessage() { Nom = "MessageDemandeDeplacement", NombreArg = 1 };
            listeMessages.Add(temp);
            temp = new ObjectMessage() { Nom = "MessageIndicationClick", NombreArg = 2 };
            listeMessages.Add(temp);
            temp = new ObjectMessage() { Nom = "MessageIndiquerPort", NombreArg = 1 };
            listeMessages.Add(temp);
            temp = new ObjectMessage() { Nom = "MessageNotif", NombreArg = 2 };
            listeMessages.Add(temp);

            LecteurXML.EcrireXml(@"C:\Messages.xml",listeMessages);









        }
    }
}
