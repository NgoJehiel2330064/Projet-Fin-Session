using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projet
{
    public static class GestionnaireIngredients
    {
        public static List<Ingredient> Ingredients { get; set; }
        public static int[] QuantiteIngredients { get; set; }

        public static void ChargerFichier(string chemin)
        {
            Ingredients = new List<Ingredient>();
            try
            {
                string json = File.ReadAllText(chemin);
                Ingredients = JsonConvert.DeserializeObject<List<Ingredient>>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur de chargement du fichier JSON : {ex.Message}");
            }

            //on crée une liste contenant la quantité actuelle de chaque ingrédient dans le restaurant
            QuantiteIngredients = new int[Ingredients.Count];

            //au départ, le restaurant posséede 20 exemplaires de tous les ingrédients
            for(int i = 0; i <  QuantiteIngredients.Length; i++)
            {
                QuantiteIngredients[i] = 20;
            }
        }
    }
}
