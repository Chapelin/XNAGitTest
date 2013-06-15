using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LibrairieMessagesContexte
{
    public class ObjectMessage
    {
        public string Nom;

        public int NombreArg;


        public override bool Equals(object obj)
        {

            var conv = (obj as ObjectMessage);
            if (obj == null ||conv ==null)
                return false;


            return conv.Nom == this.Nom && conv.NombreArg == this.NombreArg;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(this.Nom).Append(" : ").Append(this.NombreArg);
            return sb.ToString();
        }
    }
}
