using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Commun.Deplacement;
using Commun;

namespace TTRPG_case.Map
{
    public class CarteEcran
    {
        Case[,] cases_contenues;
        string nom_carte;
        //Nombre de cases X
        public int iNombreCasesX;
        public int iNombreCasesY;
        public static int TailleDefautX;
        public static int TailleDefautY;


        public CarteEcran()
        {
            new CarteEcran(CarteEcran.TailleDefautX, CarteEcran.TailleDefautY);
        }


        public CarteEcran(int taille)
        {
            new CarteEcran(taille, taille);
        }

        public CarteEcran(int tailleX, int tailleY)
        {
            this.iNombreCasesX = tailleX;
            this.iNombreCasesY = tailleY;
            this.cases_contenues = new Case[this.iNombreCasesX,this.iNombreCasesY];
        }

        public Case getCase(int posX, int posY)
        {
            return this.cases_contenues[posX,posY];
        }

        /// <summary>
        /// Genere une carte avec une seule texture de sol
        /// </summary>
        /// <param name="imageDefaut">Texture par défaut</param>
        public void GenererCases(Texture2D imageDefaut)
        {

            for(int i=0;i<this.iNombreCasesX;i++)
            {
                for(int j = 0;j<this.iNombreCasesY;j++)
                    this.cases_contenues[i,j] = new Case(imageDefaut);
            }
        }

        /// <summary>
        /// Met en place une texture sur une case
        /// </summary>
        /// <param name="x">Coord X de la case sur la map</param>
        /// <param name="y">Coord Y de la case sur la map</param>
        /// <param name="image">Texture du sol à mettre</param>
        public void InitialiserCase(int x, int y,Texture2D image)
        {
            this.cases_contenues[x, y] = new Case(image);
        }

        /// <summary>
        /// Calcule le chemin (prenant en compte les obstacles) pour aller d'un point à un autre
        /// </summary>
        /// <param name="oDepart">Coordonnee de depart</param>
        /// <param name="oArrivee">coordonnee d'arrivee</param>
        /// <returns>Chemin à emprunter pour y arriver</returns>
        public Chemin CalculerChemin(Coordonnees oDepart, Coordonnees oArrivee)
        {
            Chemin c = new Chemin();

            int a = oDepart.x;
            int b = oArrivee.x;
            int cpt = oDepart.x;
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


            a = oDepart.y;
            b = oArrivee.y;

            if (a != b)
            {
                diff = (b - a) / Math.Abs(b - a);
                cpt = oDepart.y;
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
