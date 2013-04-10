using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilitaires.Timers
{
    public class Bench
    {
        #region variables de classe
        private Dictionary<String, TickSaved> gestionnaireTick;
        #endregion


        #region Constructeur
        public Bench()
        {
            this.gestionnaireTick = new Dictionary<string, TickSaved>();
        }
        #endregion

        #region Methodes d'utilisation
        public void DemarrerTimer(String id)
        {
            if(!gestionnaireTick.ContainsKey(id))
                gestionnaireTick.Add(id, new TickSaved(id));
        }

        public int ArreterTimer(String id)
        {
            int retour = -1;
            if (gestionnaireTick.ContainsKey(id))
            {
                retour = Environment.TickCount- gestionnaireTick[id].Savedtick;
                gestionnaireTick.Remove(id);
            }
            return retour;
        }

        #endregion



    }
}
