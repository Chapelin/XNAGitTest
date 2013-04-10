using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilitaires.Timers
{
    class TickSaved
    {
        #region variables de classe
        private int savedtick;
        private String id;
        #endregion

        #region Accesseurs
        internal int Savedtick
        {
            get
            {
                return savedtick;
            }
            set
            {
                savedtick = value;
            }
        }

        internal String Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        #endregion

        #region constructeur

        internal TickSaved(string id)
        {
            this.Id = id;
            this.Savedtick = Environment.TickCount;
        }
        #endregion

        #region Methodes

        #endregion
    }
}
