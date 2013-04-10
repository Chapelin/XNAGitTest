using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilitaires.Cache
{
    public class Cache
    {
        Dictionary<String, CacheElement> dico;
        String last = "";
        Object lastelm;
        Type lastype;


        public Cache()
        {
            dico = new Dictionary<string, CacheElement>();

        }

        public void Add(String clef, Object el)
        {
            if (!dico.ContainsKey(clef))
                dico.Add(clef, null);
            CacheElement t = new CacheElement(el);
            dico[clef]=t;
            if (last == clef)
                last = "";

        }
        public bool Contient(String clef)
        {
            return dico.ContainsKey(clef);
        }

        public Object RecupererObj(String cle)
        {
            if (last != cle)
            {
                if (!dico.ContainsKey(cle))
                    throw new Exception("Clef non présente : " + cle);
                else
                {
                    last = cle;
                    lastelm = dico[cle].getElement();
                    lastype = dico[cle].getType();
                }
            }
            return lastelm;

        }

        public Type RecupererType(String cle)
        {
            if (last != cle)
            {
                if (!dico.ContainsKey(cle))
                    throw new Exception("Clef non présente : " + cle);
                else
                {
                    last = cle;
                    lastelm = dico[cle];
                    lastype = dico[cle].getType();
                }
            }
            return lastype;

        }




    }
}
