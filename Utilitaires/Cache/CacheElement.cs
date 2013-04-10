using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilitaires.Cache
{
    class CacheElement
    {
        Type nomtype;
        Object obj;

        public CacheElement(Object objet)
        {
            nomtype = objet.GetType();
            obj = objet;
        }

        public Object getElement()
        {
            return this.obj;
        }

        public Type getType()
        {
            return nomtype;
        }
 
    }
}
