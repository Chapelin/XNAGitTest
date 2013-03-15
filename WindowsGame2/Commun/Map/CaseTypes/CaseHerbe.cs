using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Commun.Map.CaseTypes
{
    public class CaseHerbe : CaseBase
    {
        public CaseHerbe(Texture2D im) : base(im)
        {
            this.franchissable = true;
        }
    }
}
