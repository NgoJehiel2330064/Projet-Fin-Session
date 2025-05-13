namespace projet
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GestionnaireIngredients.ChargerFichier("json_ingredient.json");
            var Ingredients = GestionnaireIngredients.Ingredients;


           for(int i = 0; i < 10; i++)
            FabriqueNom.ChargerFichiers("nom_famille.txt", "prenom.txt");

        }
    }
}
