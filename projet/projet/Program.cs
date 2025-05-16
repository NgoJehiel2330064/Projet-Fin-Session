namespace projet
{
    internal class Program
    {
        static Random rand = new Random();

        static void Main(string[] args)
        {
            GestionnaireIngredients.ChargerFichier("json_ingredient.json");

            FabriqueNom.ChargerFichiers("nom_famille.txt", "prenom.txt");

            //on vérifie que les noms des clients et la liste d'ingredients sont bien chargés 
            /*Console.WriteLine("Liste des ingrédients :\n");

            for (int i = 0; i < GestionnaireIngredients.Ingredients.Count; i++)
            {
                Ingredient ingr = GestionnaireIngredients.Ingredients[i];
                Console.WriteLine($"Ingrédient #{i + 1} : {ingr.Nom} | {ingr.PrixRecette}$ | Calories : {ingr.Calories}  | Qualite : {ingr.Qualite}");
            }*/

            /*
            for (int i = 0; i < GestionnaireClients.Clients.Count; i++)
            {
                Client client = GestionnaireClients.Clients[i];
                Console.WriteLine($"Client {i + 1} : Nom {client.NomComplet} | Temperament : {client.Temperament}");
            }*/


            //Création du restaurant 
            Restaurant monRestaurant = new Restaurant("Nordic", 10000);

            //Creation du simulateur
            Simulateur simulateur = new Simulateur(monRestaurant);

            //générer une liste de plats achetables
            List<Ingredient> ingredients = new List<Ingredient>();
            for (int i = 1; i <= 5; i++)
            {
                ingredients.Clear();
                int nombreIngredientsNecessaire = rand.Next(1, 11);

                for(int j = 0; j < nombreIngredientsNecessaire; j++)
                { int numeroIngredient = rand.Next(0, GestionnaireIngredients.Ingredients.Count);
                  if(!ingredients.Contains(GestionnaireIngredients.Ingredients[numeroIngredient]))
                    ingredients.Add(GestionnaireIngredients.Ingredients[numeroIngredient]); 
                }
                monRestaurant.PlatsPouvantEtreAchetes.Add(new Plat("Plat" + i, 25, ingredients));

            }

            
            Console.WriteLine();
            Console.WriteLine();

            simulateur.AfficherMenuGestion();

        }
    }
}
