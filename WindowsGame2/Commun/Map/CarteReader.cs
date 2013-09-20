using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace Commun.Map
{
    public static class CarteReader
    {
        public static CarteEcran InterpreterCarte(StreamReader sr, Dictionary<string,Texture2D>_listeTextureCarte = null)
        {
            CarteEcran _carteEcran;
            var temp = sr.ReadLine();
            var li = temp.Split(';');
            var tx = Int32.Parse(li[0]);
            var ty = Int32.Parse(li[1]);
            _carteEcran = new CarteEcran(tx, ty);
            for (var y = 0; y < ty; y++)
            {
                temp = sr.ReadLine();
                li = temp.Split(';');
                for (var x = 0; x < tx; x++)
                {

                    if (!li[x].Contains(":"))
                        _carteEcran.InitialiserCase(x, y, _listeTextureCarte == null ? null : _listeTextureCarte[li[x]], li[x]);
                    else
                    {
                        var valeurs = li[x].Split(':');
                        var id = valeurs[0];
                        var res = valeurs.ToList();
                        res.RemoveAt(0);

                        _carteEcran.InitialiserCase(x, y, _listeTextureCarte == null ? null : _listeTextureCarte[id], id, res.ToArray());
                    }
                }

            }
            return _carteEcran;
        }
    }
}
