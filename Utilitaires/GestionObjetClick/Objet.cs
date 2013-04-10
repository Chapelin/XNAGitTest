using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GestionObjetClick
{
    /// <summary>
    /// Classe d'objet survolable a la souris.
    /// </summary>
    public class Objet
    {


        #region variable const static
        /// <summary>
        /// Valeur d'alpha minimum pour la reconnaissance des pixels
        /// </summary>
        public const int ALPHAMIN = 100; 

        #endregion

        #region Variables de classe
        /// <summary>
        /// Sprite affichée a l'ecran
        /// </summary>
        private Texture2D sprite;

        /// <summary>
        /// Sprite colorée cachées, calculée a partir de celle affichée
        /// </summary>
        private Texture2D sprite_coloree;

        /// <summary>
        /// Position Z. Plus z est petot, plus le sprite sera devant pour la reconnaissance.
        /// Compris entre 0 et 1
        /// </summary>
        private float z;

        /// <summary>
        /// Position du sprite
        /// </summary>
        private Vector2 position;

        /// <summary>
        /// Couleur de sprite_coloree
        /// </summary>
        private Color couleur;

        /// <summary>
        /// Graphique device, sert a colorer.
        /// </summary>
        private GraphicsDevice gd;


        /// <summary>
        /// Proto
        /// </summary>
       public bool isMovable = false;
        #endregion

        #region accesseurs
        public Texture2D Sprite
        {
            get { return sprite; }
            set { sprite = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }
       
        public Color Couleur
        {
            get { return couleur; }
            set { 
                this.sprite_coloree = Colorer(this.sprite,value,ALPHAMIN,this.gd);
                couleur= value; }
        }

        public float Z
        {
            get { return z; }
            set
            {
                if (value > 1)
                    z = 1;
                else
                    if (value < 0)
                        z = 0;
                    else
                        z = value;
            }
        }


        public Texture2D Sprite_coloree
        {
            get { return sprite_coloree; }
            set { sprite_coloree = value; }
        }
        #endregion

        #region constructeur
        /// <summary>
        /// Constructeur d'objet
        /// </summary>
        /// <param name="text">Nom de l'objet</param>
        /// <param name="pos">Position de son sprite</param>
        /// <param name="z">Position Z, plus Z est faible, plus il sera devant</param>
        /// <param name="c">Colorkey</param>
        /// <param name="gd">GRaphiqueDevice</param>
        public Objet(Texture2D text, Vector2 pos, float z, Color c, GraphicsDevice gd)
        {
            this.gd = gd;
            Sprite = text;
            Position = pos;
            Z = z;
            Couleur = c;
            
        }

        protected Objet()
        {
            
        }

        #endregion

        #region private methodes
        /// <summary>
        /// Retourne une texture colorée pixel par pixel
        /// </summary>
        /// <param name="image">texture a colorer</param>
        /// <param name="couleur">Couleur a appliquer</param>
        /// <param name="alphamin">Alpha a partir duquel les pixels sont colorés</param>
        /// <param name="gd">Graphiqudevice</param>
        /// <returns>Texture colorée</returns>
        private Texture2D Colorer(Texture2D image, Color couleur, int alphamin, GraphicsDevice gd)
        {
            Color[] retrievedColor = new Color[image.Height * image.Width];//creation ud tableau de couleurs
            image.GetData<Color>(retrievedColor); //recuperation des couleurs dans ce tableau
            Texture2D temp = new Texture2D(gd, image.Width, image.Height); //creation de la nouvelle texture, indispensable

            for (int i = 0; i < retrievedColor.Length; i++) //pour chaque px
            {
                if (retrievedColor[i].A > alphamin) //si alpha > limite
                {
                    retrievedColor[i] = couleur; //colorisation
                }
            }
            temp.SetData<Color>(retrievedColor);//enregistrement de la version colorée dans la texture tempo
            return temp;

        }
        #endregion
    }
}
