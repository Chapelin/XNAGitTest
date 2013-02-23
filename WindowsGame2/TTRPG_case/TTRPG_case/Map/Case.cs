using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace TTRPG_case.Map
{
    public class Case
    {
        Guid id;
        Texture2D image;


        public Case(Texture2D im)
        {
            this.image = im;
            this.id = new Guid();
        }


        public Texture2D getImage()
        {
            return this.image;
        }

    }
}
