using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projet
{
    public class Ingredient
    {
        public string Nom { get; set; }
        public double Prix { get; set; }
        public string Qualite { get; set; }
        public int Calories {  get; set; }

        public Ingredient(string nom,  double prix, string qualite, int calories)
        {
            Nom = nom;
            Prix = prix;
            Qualite = qualite;
            Calories = calories;
        }

        public override string ToString()
        {
            return $"Nom : {Nom} | Prix : {Prix} | Qualité : {Qualite} | Calories : {Calories}";
        }
    }

}
