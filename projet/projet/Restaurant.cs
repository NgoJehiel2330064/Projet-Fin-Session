using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projet
{
    public enum Statut
    {
        Ouvert,
        Ferme
    }
    public class Restaurant
    {
        public string Nom { get; set; }
        public Statut Statut { get; set; }
        public List<Plat> MenuClient { get; set; }
        public List<Plat> PlatsAchetes { get; set; }
        public List<Plat> PlatsPouvantEtreAchetes { get; set; }
        public double Budget { get; set; }



        public Restaurant(string nom, double budget)
        {
            Nom = nom;

            //par défaut le restaurant est ouvert à sa création
            Statut = Statut.Ouvert;
            PlatsPouvantEtreAchetes = new List<Plat>();
            MenuClient = new List<Plat>();
            PlatsAchetes = new List<Plat>();
            Budget = budget;
        }






        /// <summary>
        /// Cette méthode permet au gestionnaire de choisir directement les actions à mener au sein du restaurant
        /// </summary>
        public void AfficherMenuGestion()
        {
            Console.Clear();
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
                        SOccuperDesClients(MenuClient);
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




        public void SOccuperDesClients(List<Plat> menuClient)
        {
            double montantTotalDebourseParLesClients = 0;
            List<Plat> platsCommandes = new List<Plat>();
            //afficher le nombre de clients présents
            Console.WriteLine($"Le restaurant {Nom} compte actuellement {GestionnaireClients.Clients.Count} client(s)");

            //Servir tous le monde automatiquement
            Console.WriteLine("Le service automatique des clients est lancé");
            for (int i = 0; i < GestionnaireClients.Clients.Count; i++)
            {
                montantTotalDebourseParLesClients += ServirClient(GestionnaireClients.Clients[i]);
                foreach (Plat plat in GestionnaireClients.Clients[i].Choix)
                {
                    platsCommandes.Add(plat);
                }
            }
            //supprimer les doublons dans la liste de plats commandés 
            //afficher les plats commandés par les clients et montant total déboursé par les clients
            Console.WriteLine($"Liste des plats commandés par les clients:\n");
            foreach(Plat plat in platsCommandes)
                Console.WriteLine($" ~" + plat + "\n");

            Console.WriteLine($"Au total, les clients ont déboursé {montantTotalDebourseParLesClients}$");

        }


        public double ServirClient(Client client)
        {
            bool tousLesPlatsDuClientServis = true;
            double sommeDeboursee = 0;
            //on parcours la liste des plats commandés par le client
            for (int i = 0; i < client.Choix.Count; i++)
            {
                double montantVerse = ServirUnPlat(client.Choix[i]);
                if (montantVerse == 0)
                    tousLesPlatsDuClientServis = false;
                else
                    sommeDeboursee += montantVerse;
            }
            //on verifie si tous les plats du client ont ete servis
            if (tousLesPlatsDuClientServis)
            {
                //on retire le client servi de la liste des clients
                Console.WriteLine($"Le client {client.NomComplet} a recu tous ses plats");
                GestionnaireClients.Clients.Remove(client);
            }
            else
                Console.WriteLine($"Le client {client.NomComplet} n'a pas recu tous ses plats");
            return sommeDeboursee;
        }

        public double ServirUnPlat(Plat plat)
        {
            bool disposeAssezIngredients = true;
            string listeIngredientsFinis = "";

            foreach (Ingredient ingredient in plat.Ingredients)
            {
                //on verifie pour chaque ingredient si le gestionnaire d'ingredients dispose d'assez d'ingredients en réserve
                if (GestionnaireIngredients.QuantiteIngredients[GestionnaireIngredients.Ingredients.IndexOf(ingredient)] < 1)
                {
                    disposeAssezIngredients = false;
                    listeIngredientsFinis = listeIngredientsFinis + ingredient.Nom + " ";
                }
            }

            if (disposeAssezIngredients)
            {
                for (int j = 0; j < plat.Ingredients.Count; j++)
                {
                    //on identifie l'indice de cet ingredient dans la liste d'ingredient du gestionnaire d'ingredients
                    int indiceIngredientDansGestionnaire = GestionnaireIngredients.Ingredients.IndexOf(plat.Ingredients[j]);

                    //on retire 1 occurence des ingrédients utilisés
                    GestionnaireIngredients.QuantiteIngredients[indiceIngredientDansGestionnaire] -= 1;
                }
                //preparation du plat

                //on ajoute la somme versée au budget du restaurant
                Budget += plat.PrixVente;
                return plat.PrixVente;
            }
            else
            {
                Console.WriteLine($"Le restaurant ne dispose pas d'assez d'ingrédients({listeIngredientsFinis}) pour préparer le plat {plat.Nom}");
                return 0;
            }
        }

        /// <summary>
        /// cette méthode permettra d'ajouter ou de retirer un plat du menu présenté aux clients
        /// </summary>
        public void AjusterMenu()
        {
            //on s'assure que l'utilisateur entre un chiffre entre 1 et 2
            bool nombreValide = true;
            int nombreEntre = 0;

            do
            {
                Console.WriteLine("Entrer le nombre correspondant:\n\n (1) Retirer un plat du menu des clients \n (2) Ajouter un plat au menu des clients");
                nombreValide = int.TryParse(Console.ReadLine(), out nombreEntre);
                if (nombreEntre != 1 && nombreEntre != 2)
                {
                    Console.WriteLine("Veuillez entrer un nombre valide");
                    nombreValide = false;
                }
            } while (!nombreValide);

            //on retire un plat du menu client
            if (nombreEntre == 1)
            {
                //on affiche la liste des plats contenus actuellement dans le menu client
                Console.WriteLine("Entrer le numero du plat que vous voulez retirer du menu client\n");
                for (int i = 0; i < MenuClient.Count; i++)
                {
                    //on adapte le numéro des plat pour qu'il soit supérieur à 1(esthétique)
                    Console.WriteLine($"({i + 1}) " + MenuClient[i].Nom + "\n");
                }

                if (nombreEntre >= 1 && nombreEntre <= MenuClient.Count)
                {
                    MenuClient.Remove(MenuClient[nombreEntre - 1]);
                    Console.WriteLine("Le plat a été retiré du menu client");
                }
            }
            else
            {
                Console.WriteLine("Entrer le numero du plat que vous voulez Ajouter au menu client\n");

                //on crée une nouvelle liste contenant uniquement les plats qu'on peut ajouter au menu client
                List<Plat> PlatsAjoutablesAuMenu = new List<Plat>();

                for (int i = 0; i < PlatsAchetes.Count; i++)
                {
                    //la liste de plats ajoutable au menu contient les plats dont la recette est achetée

                    PlatsAjoutablesAuMenu.Add(PlatsAchetes[i]);
                }

                for (int i = 0; i < MenuClient.Count; i++)
                {
                    //la liste de plats ajoutable au menu ne contient pas les plats qui sont déjà dans le menu client

                    PlatsAjoutablesAuMenu.Remove(MenuClient[i]);
                }

                //on affiche donc les plats que l'on peut ajouter au menu client
                for (int i = 0; i < PlatsAjoutablesAuMenu.Count; i++)
                {
                    //on adapte le numéro des plat pour qu'il soit supérieur à 1(esthétique)
                    Console.WriteLine($"({i + 1}) " + PlatsAjoutablesAuMenu[i].Nom + "\n");
                }

                //on fait choisir un plat à l'utilisateur
                int.TryParse(Console.ReadLine(), out nombreEntre);

                //on l'ajoute au menu client(sans frais)
                if (nombreEntre >= 1 && nombreEntre <= PlatsAjoutablesAuMenu.Count)
                {
                    MenuClient.Add(PlatsAjoutablesAuMenu[nombreEntre - 1]);
                    Console.WriteLine("Le plat a été ajouté du menu client");
                }
                else
                {
                    Console.WriteLine("Vous avez entré un nombre invalide");
                }
            }
        }



        public void AcheterNouveauPlat()
        {
            //afficher la liste des plats pouvant être achetés
            Console.WriteLine("Entrer le numero du plat dont vous voulez acheter la recette\n");
            for (int i = 0; i < PlatsPouvantEtreAchetes.Count; i++)
            {
                //on adapte le numéro des plat pour qu'il soit supérieur à 1(esthétique)
                Console.WriteLine($"({i + 1}) " + PlatsPouvantEtreAchetes[i] + "\n");
            }

            //proposer à l'utilisateur de faire un choix
            int.TryParse(Console.ReadLine(), out int nombreEntre);
            if (nombreEntre >= 1 && nombreEntre <= PlatsPouvantEtreAchetes.Count)
            {
                //acheter s'il le budget du restaurant le permet
                if (Budget >= PlatsPouvantEtreAchetes[nombreEntre - 1].PrixRecette)
                {
                    //on achete la recette avec le budget du restaurant
                    Budget -= PlatsPouvantEtreAchetes[nombreEntre - 1].PrixRecette;
                    PlatsAchetes.Add(PlatsPouvantEtreAchetes[nombreEntre - 1]);
                    Console.WriteLine("Le plat a été ajouté à la liste de plats achetés");
                    //on retire le plat à la liste des plats pouvant être achetés
                    PlatsPouvantEtreAchetes.Remove(PlatsPouvantEtreAchetes[nombreEntre - 1]);

                }
                else
                {
                    Console.WriteLine($"Le restaurant {Nom} ne dispose pas d'assez de fonds pour acheter la recette du plat {PlatsPouvantEtreAchetes[nombreEntre - 1].Nom}");
                }
            }
            else
            {
                Console.WriteLine("Vous avez entré un nombre invalide");
            }

        }




        /// <summary>
        /// permet d'acheter des  ingrédients avec le budget du restaurant
        /// </summary>
        public void CommanderIngredient()
        {
            //afficher la liste des ingrédients
            Console.WriteLine("Voici la liste des ingrédients, entrer le numero correspondant à celui que vous voulez acheter");
            for (int i = 0; i < GestionnaireIngredients.Ingredients.Count; i++)
            {
                Console.WriteLine($"({i + 1}) {GestionnaireIngredients.Ingredients[i].Nom} quantité actuelle: {GestionnaireIngredients.QuantiteIngredients[i]}");

            }
            //s'assurer que l'utilisateur entre un numéro correspondant à un ingredient
            int numeroIngredient = 0;
            do
            {
                int.TryParse(Console.ReadLine(), out numeroIngredient);
            } while (numeroIngredient < 1 || numeroIngredient > GestionnaireIngredients.Ingredients.Count);

            Console.WriteLine("Entrer la quantité à acheter");
            int.TryParse(Console.ReadLine(), out int quantiteIngredientAAcheter);

            //ON VÉRIFIE SI LE RESTAURANT POSSÈDE LES FONDS NÉCESSAIRES POUR ACHETER CET INGRÉDIENT
            if (Budget >= quantiteIngredientAAcheter * GestionnaireIngredients.Ingredients[numeroIngredient - 1].Prix)
            {
                Budget = Budget - quantiteIngredientAAcheter * GestionnaireIngredients.Ingredients[numeroIngredient - 1].Prix;
                GestionnaireIngredients.QuantiteIngredients[numeroIngredient - 1] += quantiteIngredientAAcheter;
                Console.WriteLine($"Vous venez d'acheter {quantiteIngredientAAcheter} {GestionnaireIngredients.Ingredients[numeroIngredient - 1].Prix}");
            }
            else
            {
                Console.WriteLine($" Le restaurant {Nom} ne possède pas les fonds nécessaires pour effectuer cet achat");
            }
        }



        public override string ToString()
        {
            return $"Nom : {Nom} | Statut : {Statut} |  Budget : {Budget}";
        }
    }
}
