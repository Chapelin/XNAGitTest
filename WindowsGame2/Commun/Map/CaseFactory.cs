﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                    break;
                case "1":
                    return new CaseHerbe(tex);
                    break;
                default :
                    throw  new ArgumentException("Case inconnue : "+id);
                    break;
            }
        }
    }
}