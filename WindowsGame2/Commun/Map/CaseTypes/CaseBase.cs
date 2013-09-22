using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Commun.Map.CaseTypes
{
    public class CaseBase
    {
        Guid id;
        Texture2D image;
        protected bool franchissable;
        public bool ForceStop = false;


        public CaseBase(Texture2D im)
        {
            this.image = im;
            this.id = new Guid();
        }


        public Texture2D getImage()
        {
            return this.image;
        }

        public bool Franchissable
        {
            get { return this.franchissable; }
        }

        public virtual string OnOver()
        {
            return "Erreur on est sur la classe base";
        }

        public virtual void SetParameters(object[] parametres)
        {
            throw new NotImplementedException();
        }
    }

}
