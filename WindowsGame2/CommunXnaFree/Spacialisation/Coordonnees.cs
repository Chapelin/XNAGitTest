

using System;

namespace CommunXnaFree.Spacialisation
{
    public class Coordonnees
    {
        public int X;
        public int Y;

        public Coordonnees(int x, int y):this()
        {
            this.X = x;
            this.Y = y;
        }

        public Coordonnees()
        {
        }

        public Coordonnees(Vecteur vecteur):this(vecteur.vx,vecteur.vy)
        {
            
        }

        public override string ToString()
        {
            return X + "," + Y;
        }


        public Coordonnees Abs()
        {
            return new Coordonnees(Math.Abs(X),Math.Abs(Y));
        }

        #region operateurs

        public static Coordonnees operator +(Coordonnees a, Vecteur b)
        {
            return new Coordonnees(a.X + b.vx, a.Y + b.vy);
        }

        public static Coordonnees operator -(Coordonnees a, Vecteur b)
        {
            return new Coordonnees(a.X-b.vx, a.Y-b.vy);
        }


        public static Coordonnees operator +(Coordonnees a, Coordonnees b)
        {
            return new Coordonnees(a.X + b.X, a.Y + b.Y);
        }

        public static Coordonnees operator -(Coordonnees a, Coordonnees b)
        {
            return new Coordonnees(a.X - b.X, a.Y - b.Y);
        } 
        public bool DifferenceSurDeuxAxes(Coordonnees c)
        {
            var tx = this.X - c.X;
            var ty = this.Y - c.Y;
            return Math.Abs(tx + ty) > 1;
        }
        #endregion

        
    }
}
