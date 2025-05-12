using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projet
{
    public class Plat
    {
        public Ingredient Ingredient { get; set; } 
        public int Prix { get; set; }
        public int Rarete { get; set; }
        public double PrixVente { get; set; }
    }
}
