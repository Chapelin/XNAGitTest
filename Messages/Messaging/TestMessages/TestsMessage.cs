using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using LibrairieMessagesContexte;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestMessages
{
    [TestClass]
    public class TestsMessage
    {
        public string CheminXML = @"C:\Users\Antoine\Desktop\sortie.xml";
        [TestMethod]
        public void EcritureLecture()
        {

            List<ObjectMessage> listeMessages = new List<ObjectMessage>();

            var temp = new ObjectMessage() { Nom = "MessageConnexion", NombreArg = 4 };
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

            LecteurXML.EcrireXml(CheminXML, listeMessages);


            var lect = LecteurXML.LireXml(CheminXML);

            Assert.AreEqual(lect.Count,listeMessages.Count);
            for (int i = 0; i < lect.Count; i++)
            {
                Assert.AreEqual(lect[i],listeMessages[i]);
                
            }

        }


        [TestMethod]
        public void TestOverrideEqual()
        {
            var t1 = new ObjectMessage() {Nom="Nom1", NombreArg = 1};
            var t2 = new ObjectMessage() { Nom = "Nom2", NombreArg = 2 };
            var t3 = new ObjectMessage() { Nom = "Nom1", NombreArg = 1 };
            var t4 = new ObjectMessage() { Nom = "Nom1", NombreArg = 2 };

            Assert.AreEqual(t1,t3);
            Assert.AreNotEqual(t1,t2);
            Assert.AreNotEqual(t2,t4);
            Assert.AreNotEqual(t1,t4);
        }

    }
}
