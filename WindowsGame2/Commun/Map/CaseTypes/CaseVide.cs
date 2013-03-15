using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Commun.Map.CaseTypes
{
    public class CaseVide : CaseBase
    {
        public CaseVide(Texture2D im) : base(im)
        {
            this.franchissable = false;
        }
    }
}
