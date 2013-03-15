using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Text;
using System.Runtime.Serialization.Formatters;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Commun.Map;

namespace Commun.Deplacement
{
    public class Chemin
    {

        /// <summary>
        /// Liste des vecteurs de deplacement à parcourir
        /// </summary>
        ArrayList parcours;


        /// <summary>
        /// Evenement déclenché lors de la fin d'un chemin
        /// </summary>
        public event CheminFiniHandler CheminFini;


        public delegate void CheminFiniHandler(object sender, EventArgs e);


        /// <summary>
        /// Constructeur de base
        /// </summary>
        public Chemin()
        {
            this.parcours = new ArrayList();
        }


        /// <summary>
        /// Ajouter un deplacement à la fin du parcours
        /// </summary>
        /// <param name="v">Vecteur à ajouter</param>
        public void AddMouvementFin(Vecteur v)
        {
            this.parcours.Add(v);
        }

        /// <summary>
        /// Ajouter un deplacement au tout debut du parcours
        /// </summary>
        /// <param name="v">Vecteur à ajouter</param>
        public void AddMouvementDebut(Vecteur v)
        {
            this.parcours.Insert(0, v);
        }


        /// <summary>
        /// Getter de la taille du parcours
        /// </summary>
        public int TailleParcours
        {
            get { return this.parcours.Count; }
        }

        /// <summary>
        /// Retourne le premier vecteur de deplacement de la liste, et le supprime
        /// Declenche l'evenement de chemin fini si c'est bien le cas
        /// TOFO : peut etre trop tot
        /// </summary>
        /// <returns>Le vecteur de deplacement, vecteur Zero si la liste est vide</returns>
        public Vecteur Next()
        {
            Vecteur v = Vecteur.Zero;
            if (this.TailleParcours > 0)
            {
                v = (Vecteur)this.parcours[0];
                this.parcours.RemoveAt(0);
                if (this.TailleParcours == 0 && CheminFini!=null)
                    CheminFini(this, null);
            }
            return v;
        }


        public int getNextDirection()
        {
            Vecteur v;
            int dir = 1;
            if (this.TailleParcours > 0)
            {
                v = (Vecteur)this.parcours[0];
                switch (v.vx * 10 + v.vy)
                {
                    //vx = 1
                    case 10:
                        dir = 1;
                        break;
                    case -10:
                        dir = 3;
                        break;
                    case 1:
                        dir = 2;
                        break;
                    case -1:
                        dir = 0;
                        break;
                }
            }
            return dir;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            if (this.TailleParcours == 0)
                sb.Append("Chemin vide");
            else
            {
                for (int i = 0; i < this.TailleParcours; i++)
                {
                    sb.Append(this.parcours[i].ToString());
                    if (i != this.TailleParcours - 1)
                        sb.Append("|");
                }
            }
            
            return sb.ToString();
        }


        #region gestion serialization
        //Deserialization constructor.
        public Chemin(SerializationInfo info, StreamingContext ctxt)
        {
            //Get the values from info and assign them to the appropriate properties
            this.parcours = (ArrayList)info.GetValue("parcours", typeof(ArrayList));


        }

        //Serialization function.
        public void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            //You can use any custom name for your name-value pair. But make sure you
            // read the values with the same name. For ex:- If you write EmpId as "EmployeeId"
            // then you should read the same with "EmployeeId"
            info.AddValue("parcours", this.parcours);
        }
        #endregion


        public static Chemin GetFromString(string chemin)
        {
            Chemin c = new Chemin();
            string[] temp = chemin.Split('|');
            if (temp.Length == 1 && temp[0] == "Chemin vide")
                return c;
            
            //ici one st sur que chemin non vide
            foreach (string t in temp)
            {
                string[] tempo = t.Split(',');
                c.AddMouvementFin(new Vecteur(Int32.Parse(tempo[0]), Int32.Parse(tempo[1])));

            }


            return c;
        }


        /// <summary>
        /// REtourne les coordonnées d'arrivée pour le chemin depuis des coordonnées de depart
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public Coordonnees AppliquerAPosition(Coordonnees c)
        {
            Coordonnees res = new Coordonnees(c.X,c.Y);
            foreach (Vecteur parcour in parcours)
            {
                res = res + parcour;
            }

            return res;
        }


        public Chemin Clone()
        {
            Chemin c = new Chemin();
            foreach (Vecteur parcour in parcours)
            {
                c.AddMouvementFin(parcour);
            }
            return c;
        }

       
    }
}
