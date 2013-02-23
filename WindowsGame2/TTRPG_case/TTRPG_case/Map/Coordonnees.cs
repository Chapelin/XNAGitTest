using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Commun;

namespace TTRPG_case.Map
{
    public class Coordonnees
    {
        public int x;
        public int y;

        public Coordonnees(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Coordonnees()
        {
            // TODO: Complete member initialization
        }

        public override string ToString()
        {
            return x + "," + y;
        }


        #region operateurs

        public static Coordonnees operator +(Coordonnees a, Vecteur b)
        {
            return new Coordonnees(a.x + b.vx, a.y + b.vy);
        }

        #endregion
    }
}
