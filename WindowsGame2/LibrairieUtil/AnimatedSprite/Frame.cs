using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace LibrairieUtil.AnimatedSprite
{
    public class Frame
    {
        /// <summary>
        /// Texture de la frame
        /// </summary>
        readonly Texture2D sprite;

        /// <summary>
        /// Accesseur de la texture de la frame
        /// </summary>
        public Texture2D Sprite
        {
            get { return sprite; }
        }

        /// <summary>
        /// Nombre de ticks avant la fin de cette frame
        /// </summary>
        readonly int _nombreTicks;

        /// <summary>
        /// Compteur de ticks
        /// </summary>
        float _compteur;

        public Frame(Texture2D sprite, int attente)
        {
            this.sprite = sprite;
            this._nombreTicks = attente;
            _compteur = 0;
        }


        /// <summary>
        /// Permet de "ticker" la sprite
        /// </summary>
        /// <returns>True si la frame est finie</returns>
        public bool Tick()
        {
            _compteur++;
            var retour = _compteur >= _nombreTicks;
            if (retour)
                _compteur = 0;
            return retour;
        }

        /// <summary>
        /// Ticke la frame un certain nombre de fois
        /// </summary>
        /// <param name="nombre">Nombre de ticks à effectuer</param>
        /// <returns>True si la frame est finie</returns>
        public bool Tick(float nombre)
        {
            var retour = false;
            this._compteur+=nombre;
            retour = this._compteur >= this._nombreTicks;
            if (retour)
                this.Reset();
            return retour;
        }

        /// <summary>
        /// Remet à zéro le compteur de ticks
        /// </summary>
        public void Reset()
        {
            _compteur = 0;
        }

        
       
    }
}
