using System;
using System.Net;
using ClientServeur;
using Commun.Map;

namespace TTRPG_Serveur
{
    public class Joueur : Object
    {
        public readonly Guid UiUnique;
        String _name;
        private Coordonnees position;

        public Joueur(string name, IPAddress ip, Emetteur em)
        {
            this._name = name;
            UiUnique = new Guid(DateTime.Now.Millisecond,0,1,new byte[]{4,2,5,4,4,4,4,4});
            this.Adresse = ip;
            this.EmetteurJoueur = em;
        }

        public Joueur(string name, IPAddress ip)
        {
            this._name = name;
            UiUnique = new Guid(DateTime.Now.Millisecond, 0, 1, new byte[] { 4, 2, 5,1,1,1,1,1 });
            this.Adresse = ip;
        }


        public Emetteur EmetteurJoueur { get; set; }

        public IPAddress Adresse { get; private set; }

        public Coordonnees Position
        {
            get { return this.position; }
            set { this.position = value; }
        }

        public override int GetHashCode()
        {
            return this.UiUnique.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return this.UiUnique == ((Joueur)obj).UiUnique;
        }

        public static bool operator ==(Joueur j1, Joueur j2)
        {
            return (object)j1 != null && (object)j2 !=null && j1.Equals(j2);
        }

        public static bool operator !=(Joueur j1, Joueur j2)
        {
            return !(j1 == j2);
        }
    }
}
