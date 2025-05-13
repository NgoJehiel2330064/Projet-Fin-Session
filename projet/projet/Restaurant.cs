using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projet
{
    public enum Statut
    {
        Ouvert,
        Fermé
    }
    public class Restaurant
    {
        public string Nom { get; set; }
        public Statut Statut { get; set; }
        public List<Client> Clients { get; set; }
        public List<Plat> MenuClient { get; set; }
        public List<Plat> PlatsAchetes { get; set; }


        /// <summary>
        /// Cette méthode permet au gestionnaire de choisir directement les actions à mener au sein du restaurant
        /// </summary>
        public void AfficherMenuGestion()
        {
            bool veutQuitterMenu = false;
            do
            {
                Console.WriteLine(" ********** Menu De Gestion **********\n\n");
                Console.WriteLine($"Entrer le nombre correspondant à votre choix:\n\n" +
                    $"(1) Regarder le statut du restaurant\n" +
                    $"(2) S'occuper des différents clients\n" +
                    $"(3) Ajuster le menu\n" +
                    $"(4) Acheter de nouveaux plats\n" +
                    $"(5) Commander des ingrédients\n" +
                    $"(6) Quiter le menu\n");

                int.TryParse(Console.ReadLine(), out int nombreEntre);

                switch (nombreEntre)
                {
                    case 1:
                        RegarderStatutRestaurant();
                        break;
                    case 2:
                        SOccuperDesClients();
                        break;
                    case 3:
                        AjusterMenu();
                        break;
                    case 4:
                        AcheterNouveauPlat();
                        break;
                    case 5:
                        CommanderIngredient();
                        break;
                    case 6:
                        veutQuitterMenu = true;
                        break;
                    default:
                        Console.WriteLine("Veuillez entrer un nombre valide");
                        break;
                }

            } while (!veutQuitterMenu);
        }

        /// <summary>
        /// affiche le statut du restaurant
        /// </summary>
        public void RegarderStatutRestaurant()
        {
            Console.WriteLine($"Le restaurant {Nom} est actuellement {Statut}");
        }


        public void SOccuperDesClients()
        {

        }

        /// <summary>
        /// cette méthode permettra d'ajouter ou de retirer un plat du menu présenté aux clients
        /// </summary>
        public void AjusterMenu()
        {
            //on s'assure que l'utilisateur entre un chiffre entre1 et 2
            bool nombreValide = true;
            int nombreEntre = 0;

            do {
                Console.WriteLine("Entrer le nombre correspondant:\n\n (1) Retirer un plat du menu des clients \n (2) Ajouter un plat au menu des clients");
                nombreValide = int.TryParse(Console.ReadLine(), out nombreEntre);
                if (nombreEntre < 1 || nombreEntre > 2)
                    Console.WriteLine("Veuillez entrer un nombre valide");
            } while (!nombreValide);
             
            //on retire un plat du menu client
            if (nombreEntre == 1)
            {
                for(int i = 0; i < MenuClient.Count; i++)
                {
                    Console.WriteLine($"({i + 1})"
                }
            }
        }
        public void AcheterNouveauPlat()
        {

        }

        public void CommanderIngredient()
        {

        }
    }
}
