namespace projet
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GestionnaireIngredients.ChargerFichier("json_ingredient.json");
            var Ingredients = GestionnaireIngredients.Ingredients;

            FabriqueNom.ChargerFichiers("nom_famille.txt", "prenom.txt");


        }
    }
}
