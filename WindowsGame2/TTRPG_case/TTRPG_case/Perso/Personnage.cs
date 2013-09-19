using System;
using System.Text;
using CommunXnaFree.Deplacement;
using CommunXnaFree.Spacialisation;
using LibrairieUtil.AnimatedSprite;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Commun;
using Commun.Map;

namespace TTRPG_case.Perso
{
    public class Personnage : DrawableGameComponent
    {
        public Texture2D Sprite
        {
            get
            {
                return (this._cheminPrevu!=null && this._cheminPrevu.TailleParcours > 0) ? this._spritesAnimees[this.GetNextDirection()].ActualSprite : this._spritesAnimees[this.Direction].ActualSprite;
            }
        }


        public Coordonnees NextCase
        {
            get { return this._nextCase; }
        }

        /// <summary>
        /// Handler de l'evenement de fin de chemin, pour regler l'animation
        /// </summary>
        readonly Chemin.CheminFiniHandler _cfh;

        Coordonnees _position;

        /// <summary>
        /// Offset entre le 0,0 du perso et le 0,0 de la case pour centrer le sprite
        /// </summary>
        public Coordonnees OffsetCaseSprite;

        /// <summary>
        /// Offset de deplacement
        /// </summary>
        public Vecteur OffsetCaseDepl;


        Chemin _cheminPrevu;
        /// <summary>
        /// Compteur de ticks et facilite la gestion du deplacement
        /// Deplacement par frame : taille_case/nombre_tick_total
        /// </summary>
        public int Compteur;

        //position de la case du perso
        //int posX;
        //int posY;
        /// <summary>
        /// 0 : haut, 1 : droit, 2 : bas, 3 : gauche
        /// </summary>
        public int Direction;

        AnimatedSprite[] _spritesAnimees;
        public bool Flagdepl;
        private Coordonnees _nextCase;


        /// <summary>
        /// Constructeur d'un personnage
        /// </summary>
        /// <param name="spriteDroit"></param>
        /// <param name="posX">position X</param>
        /// <param name="posY">positionY</param>
        /// <param name="spriteHaut"></param>
        /// <param name="spriteBas"></param>
        /// <param name="spriteGauche"></param>
        public Personnage(AnimatedSprite spriteHaut, AnimatedSprite spriteBas, AnimatedSprite spriteGauche, AnimatedSprite spriteDroit, int posX, int posY, Game g)
            : this(posX, posY, g)
        {

            this.SetSprites(spriteHaut, spriteBas, spriteGauche, spriteDroit);

        }



        /// <summary>
        /// Constructeur d'un personnage
        /// </summary>
        /// <param name="posX"></param>
        /// <param name="posY"></param>
        public Personnage(int posX, int posY, Game g):this(g)
        {
            this._position = new Coordonnees { X = posX, Y = posY };

            this.OffsetCaseDepl = Vecteur.Zero;
            this.Direction = 2;
            this.Compteur = -1;
            // this.compteur = 0;
            this._cheminPrevu = new Chemin();
            this._cfh = this.FinCheminPrevu;
            this.Flagdepl = false;

        }

        public Personnage (Game g) : base(g)
        {}


        /// <summary>
        /// Setter des sprites pour le perso
        /// </summary>
        /// <param name="spriteHaut"></param>
        /// <param name="spriteBas"></param>
        /// <param name="spriteGauche"></param>
        /// <param name="spriteDroit"></param>
        public void SetSprites(AnimatedSprite spriteHaut, AnimatedSprite spriteBas, AnimatedSprite spriteGauche, AnimatedSprite spriteDroit)
        {
            this._spritesAnimees = new[] { spriteHaut, spriteDroit, spriteBas, spriteGauche };
            for (var i = 0; i < 4; i++)
                this._spritesAnimees[i].InitialiserAnimation();
            this.OffsetCaseSprite = new Coordonnees(((Game1.TailleCaseX - this.GetSprite.Width) / 2), ((Game1.TailleCaseY - this.GetSprite.Height) / 2));
        }


        public Chemin CheminPerso
        {
            set
            {
                //desabonnement au cas ou
                if (null != this._cheminPrevu)
                    this._cheminPrevu.CheminFini -= _cfh;
                this._cheminPrevu = value;
                //abonnement au nouveau chemin
                if (null != this._cheminPrevu)
                    this._cheminPrevu.CheminFini += _cfh;

            }
        }


        public bool ACheminPrevu()
        {
            var prevu = false;
            if (this._cheminPrevu != null)
                prevu = this._cheminPrevu.TailleParcours > 0;

            return prevu;
        }



        /// <summary>
        /// REtourne le premier vecteur de la liste, et le supprime
        /// </summary>
        /// <returns></returns>
        public Vecteur GetNextMouvement()
        {
            var v = Vecteur.Zero;
            if (this.ACheminPrevu())
                v = this._cheminPrevu.Next();
            return v;
        }


        public int GetNextDirection()
        {
            var retour = this._cheminPrevu.getNextDirection();

            return retour;
        }


        public void FinCheminPrevu(object sender, EventArgs e)
        {
            Console.WriteLine("Fin chemin prevu pour perso");
            this.Flagdepl = false;
            this.ResetSpriteDirection();
        }

        /// <summary>
        /// Deplace le perso du vecteur indiqué
        /// </summary>
        public void Move(Vecteur v)
        {
            Console.WriteLine("Perso deplacé de : " + v.vx + " , " + v.vy);
            this._position.X += v.vx;
            this._position.Y += v.vy;

        }

        /// <summary>
        /// Deplace le perso à l'endroit indiqué
        /// </summary>
        /// <param name="coord"></param>
        public void MoveTo(Coordonnees coord)
        {
            this._position = coord;
            Console.WriteLine("Perso deplacé en :" + this._position);
        }


        /// <summary>
        /// Calcul le vecteur de deplacement pour aller à la coordonnée indiquée
        /// </summary>
        /// <param name="coord">Coordonnée cible</param>
        /// <returns>ecteur de deplacement necessaire</returns>
        public Vecteur CalculerVecteurDeplacement(Coordonnees coord)
        {
            return new Vecteur(coord.X - this._position.X, coord.Y - this._position.Y);
        }


        #region gestion animation

        public void Tick()
        {


            int dir = this.GetNextDirection();
            this._spritesAnimees[dir].Tick();
            //gerer offset
            //l'erreur est forcement ici
            var t = (float)(Game1.NombreTickDeplacement - this.Compteur) / Game1.NombreTickDeplacement;
            var d = t * Game1.TailleCaseX;

            switch (dir)
            {
                //cas stop
                case -1:
                    this.OffsetCaseDepl = Vecteur.Zero;
                    break;
                case 0:
                    this.OffsetCaseDepl = new Vecteur(0, -(int)d);
                    break;
                case 1:
                    this.OffsetCaseDepl = new Vecteur((int)d, 0);
                    break;
                case 2:
                    this.OffsetCaseDepl = new Vecteur(0, (int)d);
                    break;
                case 3:
                    this.OffsetCaseDepl = new Vecteur(-(int)d, 0);
                    break;
            }

            //Console.WriteLine("après tick");
            //Console.Write(this);




        }

        public void ResetSpriteDirection()
        {
            Console.WriteLine("Resetspritedirection");
            foreach (AnimatedSprite sp in this._spritesAnimees)
            {
                sp.InitialiserAnimation();
            }
            this.OffsetCaseDepl = Vecteur.Zero;
        }
        #endregion

        #region Getters
        /// <summary>
        /// Represente les coordonnées de la case du personnage, attention, en cas de mouvement, 
        /// tant que le mouvement entre deux cases adjacente n'st pas fini, 
        /// il s'agit de la precedente case (celle de depart du mouvement intercase)
        /// </summary>
        public Coordonnees Coordonnees
        {
            get { return this._position; }
            set { this._position = value; }
        }

        public Texture2D GetSprite
        {
            get { return this.Sprite; }
        }
        #endregion

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine("*************************************PERSO*****************");
            sb.Append("Offset case dep").AppendLine(this.OffsetCaseDepl.ToString());
            sb.Append("Case actuelle ").AppendLine(this._position.ToString());
            sb.Append("Direction ").AppendLine(this.Direction.ToString());
            sb.AppendLine("******************************************************");
            return sb.ToString();
        }


        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            var sp = new SpriteBatch(this.Game.GraphicsDevice);
            sp.Begin();
            sp.Draw(this.GetSprite, new Rectangle(this.Coordonnees.X * Game1.TailleCaseX + this.OffsetCaseSprite.X + this.OffsetCaseDepl.vx, this.Coordonnees.Y * Game1.TailleCaseY + this.OffsetCaseSprite.Y + this.OffsetCaseDepl.vy, this.GetSprite.Width, this.GetSprite.Height), Color.White);
            sp.End();
        }


    }
}
