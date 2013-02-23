using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LibrairieUtil.AnimatedSprite
{
    public class AnimatedSprite
    {
        /// <summary>
        /// Ensemble des frames de l'animation
        /// </summary>
        Frame[] _animations;

        /// <summary>
        /// Taille en frames de l'animation
        /// </summary>
        int _taille;

        /// <summary>
        /// Curseur sur le tableau des frames indiquant celle en cours
        /// </summary>
        int _actuelle;

        /// <summary>
        /// Accesseurs de la texture actuelle de l'animation
        /// </summary>
        public Texture2D ActualSprite { get; private set; }


        public AnimatedSprite()
        {
            this._animations = null;
            this._taille = 0;
            _actuelle = 0;           
        }

        /// <summary>
        /// Ajouter une frame à l'animation
        /// </summary>
        /// <param name="sprite">Texture à ajouter</param>
        /// <param name="nombre">Temps en frames de la frame</param>
        public void AjoutAnimationFrame(Texture2D sprite, int nombre)
        {
            var temp = new Frame[this._taille + 1];
            var temporaire = new Frame(sprite, nombre);
            for(var i=0;i<this._taille;i++)
                temp[i] = _animations[i];
            temp[_taille] = temporaire;
            this._animations = temp;
            this._taille++;
        }

        /// <summary>
        /// Initialiser l'animation, remet l'actuelleframe au depart
        /// </summary>
        public void InitialiserAnimation()
        {
            this._actuelle = 0;
            for (var i = 0; i < this._taille; i++)
            {
                this._animations[i].Reset();
                
            }
            this.ActualSprite = this._animations[this._actuelle].Sprite;
        }

        /// <summary>
        /// Ticker l'animation
        /// </summary>
        public void Tick()
        {
            
            this.ActualSprite = this._animations[this._actuelle].Sprite;
            if (this._animations[this._actuelle].Tick())
            {
                this._actuelle++;
                if (this._actuelle >= this._taille)
                    this._actuelle = 0;
            }
            
        }

        /// <summary>
        /// Ticker d'un certain nombre de fois
        /// </summary>
        /// <param name="nombre"></param>
        public void Tick(float nombre)
        {

            this.ActualSprite = this._animations[this._actuelle].Sprite;
            if (!this._animations[this._actuelle].Tick(nombre)) return;
            this._actuelle++;
            if (this._actuelle >= this._taille)
                this._actuelle = 0;
        }
    }
}
