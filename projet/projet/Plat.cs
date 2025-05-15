using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projet
{
    public class Plat
    {
        public string Nom {  get; set; }
        public List<Ingredient> Ingredients { get; set; } 
        public double PrixRecette { get; set; }
        public int Rarete { get; set; }
        public double PrixVente { get; set; }

        public Plat(string nom, double prix, List<Ingredient> ingredients) 
        {
            Nom = nom;
            PrixRecette = prix;
            PrixVente = 0;
            Ingredients = new List<Ingredient>();
            foreach(Ingredient ingredient in ingredients)
            {
                PrixVente += ingredient.Prix;
                Ingredients.Add(ingredient);
            }
            //on obtient le prix de vente en augmentant 20% au prix total d'achat des ingrdients et de la recette
            PrixVente += prix;
            PrixVente = Math.Round(PrixVente * 1.2, 2);

            //on détermine la rareté en fonction du prix de vente
            if (PrixVente < 5)
                Rarete = 1;
            else if (PrixVente < 10)
                Rarete = 2;
            else if (PrixVente < 15)
                Rarete = 3;
            else if (PrixVente < 20)
                Rarete = 4;
            else
                Rarete = 5;

        }

        public override string ToString()
        {
            string infoIngredients = "Liste des ingrédients:\n";
            foreach(Ingredient ingredient in Ingredients)
                infoIngredients = infoIngredients + " ~" + ingredient +"\n";
            return $"Nom : {Nom} | Rareté : {Rarete}/5 | Prix d'achat de la recette : {PrixRecette} | Prix de vente : {PrixVente} | " + infoIngredients;
        }
    }
}
