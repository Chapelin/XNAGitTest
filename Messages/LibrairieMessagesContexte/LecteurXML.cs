using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace LibrairieMessagesContexte
{
    public static class LecteurXML
    {
        public static List<ObjectMessage> LireXml(string chemin)
        {
           XmlSerializer ser = new XmlSerializer(typeof(List<ObjectMessage>));

            var res = (List<ObjectMessage>)ser.Deserialize(new StreamReader(chemin));

            return res;
        }

        public static void EcrireXml(string chemin, List<ObjectMessage> liste )
        {
            XmlSerializer ser = new XmlSerializer(typeof(List<ObjectMessage>));
            using (var ecr = new StreamWriter(chemin))
            {
                ser.Serialize(ecr, liste);
            }
        }
    }
}
