namespace projet
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GestionnaireIngredients.ChargerFichier("json_ingredient.json");

            FabriqueNom.ChargerFichiers("nom_famille.txt", "prenom.txt");

            Console.WriteLine("Liste des ingrédients :\n");

            for (int i = 0; i < GestionnaireIngredients.Ingredients.Count; i++)
            {
                Ingredient ingr = GestionnaireIngredients.Ingredients[i];
                Console.WriteLine($"Ingrédient #{i + 1} : {ingr.Nom} | {ingr.Prix}$ | Calories : {ingr.Calories}  | Qualite : {ingr.Qualite}");
            }

            GestionnaireClients.GenererClients(5);

            for (int i = 0; i < GestionnaireClients.Clients.Count; i++)
            {
                Client client = GestionnaireClients.Clients[i];
                Console.WriteLine($"Client {i + 1} : Nom {client.NomComplet} | Temperament : {client.Temperament}");
            }
        }
    }
}
