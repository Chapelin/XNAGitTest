using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilitaires.Random
{
    public class RandomManager
    {

        private int[] seed;
        private int[] resu;
        private int cpt;
        private Rand random;

        public RandomManager()
        {
            this.seed = new int[256];
            this.seed[0] = int.Parse(DateTime.Now.Ticks.ToString().Substring(4,8));
            Recharge();
            Recharge();
        }

        public int GetInt(int max)
        {
            if (this.cpt >= 255)
            {
                Recharge();
            }
            int tempo = Math.Abs(this.resu[this.cpt]);
            tempo = (int)(max * ((tempo) / (Math.Pow(2,31))));


            this.cpt++;

            return tempo;



        }

        private void Recharge()
        {
            this.cpt = 0;
            this.random = new Rand(seed);
            this.random.Isaac();
            this.resu = this.random.rsl;
            this.seed = this.random.rsl;

        }


    }
}
