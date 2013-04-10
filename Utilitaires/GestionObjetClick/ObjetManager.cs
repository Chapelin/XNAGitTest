using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace GestionObjetClick
{
    /// <summary>
    /// Classe de gestion des objets
    /// </summary>
    public class ObjetManager
    {

        #region Variables de classe
        /// <summary>
        /// Dictionnaire des objets sous la forme nom,objet
        /// </summary>
        Dictionary<String,Objet> dic;

        /// <summary>
        /// Dictionnaire par couleurs des noms d'objet, sous la forme Couleur.ToString(), nom de l'objet
        /// </summary>
        Dictionary<String,String> dicolor;

        /// <summary>
        /// Game en cours
        /// </summary>
        Game game;

        /// <summary>
        /// Texture cachee contenant les sprites colorées
        /// </summary>
        Texture2D cachee;

        /// <summary>
        /// RenderTarget servant a cachee
        /// </summary>
        RenderTarget2D renderTarget;

        /// <summary>
        /// Couleur actuelle
        /// </summary>
        uint actualcolor;
        #endregion


        #region Constructeurs
        /// <summary>
        /// Constructeur d'OBjetManager
        /// </summary>
        /// <param name="g">Game</param>
        /// <param name="cach">TExture 2D de la taille de l'écran</param>
        public ObjetManager(Game g, Texture2D cach)
        {
            dic = new Dictionary<string,Objet>();
            dicolor = new Dictionary<String,String>();
            this.game = g;
            renderTarget = new RenderTarget2D(
                game.GraphicsDevice,
                game.GraphicsDevice.PresentationParameters.BackBufferWidth,
                game.GraphicsDevice.PresentationParameters.BackBufferHeight
                );
            cachee = cach;

            actualcolor = 0x000001;

        }

        #endregion


        #region Gestion des listes d'objets


        /// <summary>
        /// Ajout d'un objet au manager
        /// </summary>
        /// <param name="nom">Nom de l'objet</param>
        /// <param name="ob">Objet a ajouter</param>
        /// <param name="n">Si true : couleur aléatoire</param>
        public void Add(String nom, Objet ob)
        {

            Color t = new Color((byte)(actualcolor >> 16), (byte)(actualcolor >> 8), (byte)(actualcolor));
            ob.Couleur = t;
            dic.Add(nom, ob);
            dicolor.Add(ob.Couleur.ToString(), nom);
            actualcolor= actualcolor+1;
        }

        /// <summary>
        /// Recuperation d'un objet en fonction de son nom
        /// </summary>
        /// <param name="s">Nom de l'objet</param>
        /// <returns>L'objet identifié, ou null s'il n'existe pas</returns>
        public Objet Get(String s)
        {
            Objet temp = null;
            try
            {
                temp = dic[s];
            }
            catch { }
            
            return temp;
        }

        /// <summary>
        /// Recuperation du nom de l'objet par sa couleur
        /// </summary>
        /// <param name="c">couleur de l'objet</param>
        /// <returns>le nom de l'objet, ou null</returns>
        public String Get(Color c)
        {
            String temp = null;
            try
            {
                temp = dicolor[c.ToString()];
            }
            catch { };

            return temp;
        }

        /// <summary>
        /// Supprime un objet du manager
        /// </summary>
        /// <param name="s"></param>
        public void Remove(String s)
        {
            try
            {
                String c = dic[s].Couleur.ToString();
                dic.Remove(s);
                dicolor.Remove(c);
            }
            catch { }
            
        }

        /// <summary>
        /// Mise a Zero du manager
        /// </summary>
        public void Clear()
        {
            dic = new Dictionary<string, Objet>();
            dicolor = new Dictionary<string, string>();
        }


        /// <summary>
        /// Retourne la liste des objets
        /// </summary>
        /// <returns>Liste des objets triés par leur Z dans l'ordre croissant</returns>
        public List<Objet> ToList()
        {
            List<Objet> ltemp = new List<Objet>();
            try
            {
                dic.OrderBy(temp => temp.Value.Z);
                ltemp = dic.Select(temp => temp.Value).ToList();
            }
            catch { }
            return ltemp;
        }

        #endregion


        #region Méthodes de dessin, caché ou non

        /// <summary>
        /// Dessine dans cachee les sprties colorées
        /// </summary>
        public void DrawHiddenObjets()
        {
            // Set the render target
            List<Objet> l = this.ToList();
            game.GraphicsDevice.SetRenderTarget(renderTarget);
            SpriteBatch te = new SpriteBatch(game.GraphicsDevice);
            te.Begin();
            for (int i = 0; i < l.Count; i++)
                te.Draw(l[i].Sprite_coloree, l[i].Position, Color.White);
            te.End();
            // Drop the render target
            game.GraphicsDevice.SetRenderTarget(null);
            cachee = renderTarget;
            //cachee = renderTarget.GetTexture<Texture2D>(cachee);
        }

        /// <summary>
        /// Dessine via le spritebatch fourni les sprites classique des objets
        /// </summary>
        /// <param name="sp">Spritebatch initialisé</param>
        public void DrawObjets(SpriteBatch sp)
        {
            
            List<Objet> l = this.ToList();
            for (int i = 0; i < l.Count; i++)
            {
                //sp.Draw(l[i].Sprite, l[i].Position, Color.White);
                sp.Draw(l[i].Sprite, new Rectangle((int)l[i].Position.X, (int)l[i].Position.Y, l[i].Sprite.Width, l[i].Sprite.Height), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, l[i].Z);
            }
        }

        #endregion


        #region Utilitaires

        /// <summary>
        /// Retourne le nom de l'objet sous la souris
        /// </summary>
        /// <param name="state">MouseState</param>
        /// <returns>Nom de l'objet, ou Empty</returns>
        public String DevinerObjet(MouseState state)
        {
            Color[] dessous = new Color[1];
            string retour = String.Empty;
            if (state.X > 0 && state.Y > 0 && state.X < game.GraphicsDevice.PresentationParameters.BackBufferWidth && state.Y < game.GraphicsDevice.PresentationParameters.BackBufferHeight)
            {
                cachee.GetData<Color>(0, new Rectangle(state.X, state.Y, 1, 1), dessous, 0, 1);
                if (Get(dessous[0]) != null)
                {
                    retour = this.Get(dessous[0]);
                }
                else
                {
                    retour = String.Empty;
                }
            }
            else
                retour = String.Empty;
            
            return retour;
        }

        /// <summary>
        /// Prend un screen en png
        /// </summary>
        /// <param name="path">Chemine et non du ficher</param>
        public void TakeScreenOfHidden(String path)
        {
            Stream temp = File.Open(path,FileMode.Create);
            cachee.SaveAsPng(temp,cachee.Height,cachee.Width);
        }

        #endregion
    }
}
