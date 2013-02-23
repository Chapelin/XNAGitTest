using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace TTRPG_Serveur
{
    public class ListeJoueur
    {
        readonly List<Joueur> _listeJoueur;


        public ListeJoueur()
        {
            _listeJoueur = new List<Joueur>();
        }


        public void AjouteJoueur(Joueur j)
        {
            this._listeJoueur.Add(j);
        }


        public bool RetirerJoueur(Joueur j)
        {
            return this._listeJoueur.Remove(j);
            
        }

        /// <summary>
        /// Indique si un joueur est présent dans la liste, sur la base de son ui unique
        /// </summary>
        /// <param name="j">Joueur</param>
        /// <returns>True si le joueur est présent dans la liste</returns>
        public bool Contains(Joueur j)
        {
            var retour = false;
            var i=0;
            while (retour == false && i < this._listeJoueur.Count)
            {
                retour = this._listeJoueur[i] == j;
                i++;
            }

            return retour;

        }


        public List<Joueur> ToList()
        {
            return this._listeJoueur.ToList<Joueur>();
        }


    }
}
