using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Commun.Map.CaseTypes
{
    public class CaseTelep : CaseBase
    {
        
        public CaseTelep(Texture2D im) : base(im)
        {
            this.franchissable = true;
        }

        public override string OnOver()
        {
            return "CaseTelep";
        }

        public int idTelep =0;
    }
}
