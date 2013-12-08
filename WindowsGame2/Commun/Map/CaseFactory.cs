using System;
using Commun.Map.CaseTypes;
using Microsoft.Xna.Framework.Graphics;

namespace Commun.Map
{
    public static class CaseFactory
    {
        public static CaseBase GetCase(string id, Texture2D tex)
        {
            switch (id)
            {
                case "0":
                    return new CaseVide(tex);
                case "1":
                    return new CaseHerbe(tex);
                case "2":
                    return new CaseTelep(tex);
                default :
                    throw  new ArgumentException("Case inconnue : "+id);
            }
        }

        public static Type GetCaseType(CaseBase ca)
        {
            return ca != null ? (ca.GetType()) : null;
        }
    }
}
