using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commun
{
    public class Vecteur
    {
        public int vx;
        public int vy;

        public Vecteur(int x, int y)
        {
            this.vx = x;
            this.vy = y;
        }

        public static Vecteur Unitaire
        {
            get { return new Vecteur(1, 1); }
        }

        public static Vecteur Zero
        {
            get { return new Vecteur(0, 0); }
        }

        public double Taille
        {
            get { return Math.Sqrt(this.vx * this.vx + this.vy * this.vy); }

        }

        public override string ToString()
        {
            return this.vx + "," + this.vy;
        }

        public static Vecteur Inverse(Vecteur v)
        {
            return new Vecteur(-v.vx, -v.vy);
        }

        #region operateurs
        public static Vecteur operator +(Vecteur a, Vecteur b)
        {
            return new Vecteur(a.vx + b.vx, a.vy + b.vy);
        }

        public static Vecteur operator -(Vecteur a, Vecteur b)
        {
            return new Vecteur(a.vx - b.vx, a.vy - b.vy);
        }

        public static bool operator ==(Vecteur a, Vecteur b)
        {
            return a.vx==b.vx && a.vy == b.vy;
        }

        public static bool operator !=(Vecteur a, Vecteur b)
        {
            return !(a == b);
        }

        public static Vecteur operator *(Vecteur a, int num)
        {
            return new Vecteur(a.vx * num, a.vy * num);
        }

        public static Vecteur operator *(Vecteur a, float b)
        {
            return new Vecteur((int)(a.vx * b), (int)(a.vy * b));
        }

        #endregion



    }
}
