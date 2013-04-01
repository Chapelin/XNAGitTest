using System;
using Commun.Map.CaseTypes;
using CommunXnaFree.Deplacement;
using CommunXnaFree.Spacialisation;
using Microsoft.Xna.Framework.Graphics;

namespace Commun.Map
{
    public class CarteEcran
    {
        readonly CaseBase[,] _casesContenues;
        private string _nomCarte;
        //Nombre de cases X
        public int NombreCasesX;
        public int NombreCasesY;
        public static int TailleDefautX;
        public static int TailleDefautY;


        public CarteEcran()
        {
            new CarteEcran(TailleDefautX, TailleDefautY);
        }


        public CarteEcran(int taille)
        {
            new CarteEcran(taille, taille);
        }

        public CarteEcran(int tailleX, int tailleY)
        {
            this.NombreCasesX = tailleX;
            this.NombreCasesY = tailleY;
            this._casesContenues = new CaseBase[this.NombreCasesX,this.NombreCasesY];
        }

        public string NomCarte
        {
            get { return this._nomCarte; }
            set { this._nomCarte = value; }
        }

        public CaseBase GetCase(int posX, int posY)
        {
            return this._casesContenues[posX,posY];
        }

        /// <summary>
        /// Genere une carte avec une seule texture de sol
        /// </summary>
        /// <param name="imageDefaut">Texture par défaut</param>
        public void GenererCases(Texture2D imageDefaut)
        {

            for(var i=0;i<this.NombreCasesX;i++)
            {
                for(var j = 0;j<this.NombreCasesY;j++)
                    this._casesContenues[i,j] = new CaseBase(imageDefaut);
            }
        }

        /// <summary>
        /// Met en place une texture sur une case
        /// </summary>
        /// <param name="x">Coord X de la case sur la map</param>
        /// <param name="y">Coord Y de la case sur la map</param>
        /// <param name="image">Texture du sol à mettre</param>
        public void InitialiserCase(int x, int y,Texture2D image, string id)
        {
            this._casesContenues[x, y] = CaseFactory.GetCase(id, image);
        }

        /// <summary>
        /// Calcule le chemin (prenant en compte les obstacles) pour aller d'un point à un autre
        /// </summary>
        /// <param name="oDepart">Coordonnee de depart</param>
        /// <param name="oArrivee">coordonnee d'arrivee</param>
        /// <returns>Chemin à emprunter pour y arriver</returns>
        public Chemin CalculerChemin(Coordonnees oDepart, Coordonnees oArrivee)
        {
            var c = new Chemin();

            var a = oDepart.X;
            var b = oArrivee.X;
            var cpt = oDepart.X;
            //todo : prendre en compte les obstacles
            int diff;

            if (a != b)
            {
                diff = (b - a) / Math.Abs(b - a);
                //diff : operateur de difference, si xDep<Xarr, diff = 1, et vice versa, idem pour y
                //tant que le compteur sur x n'est pas egale à la valeur x d'arrivée
                while (cpt != b)
                {
                    cpt += diff;
                    c.AddMouvementFin(new Vecteur(diff, 0));
                }
                //ici, on a comblé la diff sur X
            }


            a = oDepart.Y;
            b = oArrivee.Y;

            if (a != b)
            {
                diff = (b - a) / Math.Abs(b - a);
                cpt = oDepart.Y;
                while (cpt != b)
                {
                    cpt += diff;
                    c.AddMouvementFin(new Vecteur(0, diff));
                }

            }

            //ajout du premier vecteur, nul pour initialiser le deplacement du sprite dans al bonne direction
            if (c.TailleParcours > 0)
            {
                c.AddMouvementDebut(Vecteur.Zero);
            }
            Console.WriteLine("Chemin calculé pour aller de " + oDepart + " à " + oArrivee + " : ");
            Console.WriteLine(c.ToString());

            return c;
        }
    }

}
