using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeuneEntrepreneur.Actifs
{
    public class Terrain : Actif
    {
        public int Superficie {  get; set; }

        public Terrain (string nom,int valeur) : base (nom,valeur,TypeActif.Terrain)
        {
            Superficie = Program.rand.Next(100, 5000);
        }

        public override string ToString()
        {
            return $"{base.ToString()} | Superficie : {Superficie} m²";
        }
    }
}
