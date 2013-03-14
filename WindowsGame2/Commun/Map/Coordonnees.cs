namespace Commun.Map
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

        public override string ToString()
        {
            return X + "," + Y;
        }


        #region operateurs

        public static Coordonnees operator +(Coordonnees a, Vecteur b)
        {
            return new Coordonnees(a.X + b.vx, a.Y + b.vy);
        }

        #endregion

        
    }
}
