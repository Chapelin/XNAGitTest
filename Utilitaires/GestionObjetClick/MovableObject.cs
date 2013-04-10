using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GestionObjetClick;
using Microsoft.Xna.Framework;

namespace Utilitaires.GestionObjetClick
{
    public class MovableObject : Objet
    {
        bool isDragged = false;
        public bool isMovable = true;

        public void Click()
        {

            this.isDragged = true;
        }

        public void Relache()
        {
            this.isDragged = false;
        }

        public void moveTo(Vector2 v)
        {
            this.Position = v;
        }
        

    }
}
