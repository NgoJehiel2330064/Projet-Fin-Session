namespace projet
{
    internal class Program
    {
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
            Restaurant monRestaurant = new Restaurant("Nordic", 100000);

            //Generation des clients ainsi que de leurs commandes
            GestionnaireClients.GenererClients(5);
            for (int i = 0; i < GestionnaireClients.Clients.Count; i++)
                GestionnaireClients.Clients[i].Commander(monRestaurant.MenuClient);
            Console.WriteLine();
            Console.WriteLine();

        }
    }
}
