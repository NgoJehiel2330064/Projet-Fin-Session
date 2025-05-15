using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeuneEntrepreneur.Actifs
{
    public class Maison : Actif
    {
        public int SurfaceHabitable {  get; set; }

        public Maison(string nom,int valeur) : base(nom,valeur,TypeActif.Terrain) 
        {
            SurfaceHabitable = Program.rand.Next(100, 2000);
        }

        public override string ToString()
        {
            return $"{base.ToString()} | Surface Habitable : {SurfaceHabitable} m²";
        }
    }
}
