using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projet
{
    public class Menu
    {


        public void AfficherMenu(Restaurant restaurant)
        {
            Console.WriteLine(" ********** Menu De Gestion **********\n\n");
            Console.WriteLine($"Entrer le nombre correspondant à votre choix:\n\n" +
                $"(1) Regarder le statut du restaurant\n" +
                $"(2) S'occuper des différents clients\n" +
                $"(3) Ajuster le menu\n" +
                $"(4) Acheter de nouveaux plats\n" +
                $"(5) Commander des ingrédients\n");

            bool nombreValide = int.TryParse(Console.ReadLine(), out int nombreEntre);

        }

        public void RegarderStatutRestaurant(Restaurant restaurant)
        {
                Console.WriteLine($"Le restaurant {restaurant.Nom} est actuellement {restaurant.Statut}");
        }

        public void SOccuperDesClients()
        {

        }

        public void AjusterMenu()
        {

        }

        public void AcheterNouveauPlat()
        {

        }
        
        public void CommanderIngredient()
        {

        }
    }
}
