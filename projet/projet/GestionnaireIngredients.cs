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

        public static void ChargerFichier(string chemin)
        {
            try
            {
                string json = File.ReadAllText(chemin);
                Ingredients = JsonConvert.DeserializeObject<List<Ingredient>>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur de chargement du fichier JSON : {ex.Message}");
            }
        }
    }
}
