using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projet
{
    public class Simulateur
    {
        public Restaurant LeRestaurant { get; set; }
        static Random rand = new Random();

        public Simulateur(Restaurant restaurant)
        {
            LeRestaurant = restaurant;
        }



        /// <summary>
        /// Cette méthode permet au gestionnaire de choisir directement les actions à mener au sein du restaurant
        /// </summary>
        public void AfficherMenuGestion()
        {
            bool veutQuitterMenu = false;
            do
            {
                Console.Clear();

                Console.WriteLine(" ********** Menu De Gestion **********\n\n");
                Console.WriteLine($"Entrer le nombre correspondant à votre choix:\n\n" +
                    $"(1) Regarder le statut du restaurant\n" +
                    $"(2) S'occuper des différents clients");
                if (LeRestaurant.MenuClient.Count == 0)
                    Console.WriteLine("PS: Actuellement, les clients ne peuvent commander car le Menu est vide");
                Console.WriteLine($"(3) Ajuster le menu\n" +
                    $"(4) Acheter de nouveaux plats\n" +
                    $"(5) Commander des ingrédients\n" +
                    $"(6) Engager un nouvel employé\n" +
                    $"(7) Quiter le menu\n");

                int.TryParse(Console.ReadLine(), out int nombreEntre);
                Console.WriteLine("\n");
                switch (nombreEntre)
                {
                    case 1:
                        RegarderStatutRestaurant();
                        break;
                    case 2:
                        LancerServiceAutomatique(LeRestaurant.MenuClient);
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
                        EngagerEmploye();
                        break;
                    case 7:
                        veutQuitterMenu = true;
                        break;
                    default:
                        Console.WriteLine("Veuillez entrer un nombre valide");
                        break;
                }
                Console.WriteLine($"\n\nPeser sur une touche pour continuer");
                Console.ReadKey();
            } while (!veutQuitterMenu);
            Console.WriteLine("******************** La  Simulation est terminée, Merci d'avoir participé ********************");
        }





        /// <summary>
        /// affiche le statut du restaurant
        /// </summary>
        public void RegarderStatutRestaurant()
        {
            Console.WriteLine(LeRestaurant);
        }





        public void LancerServiceAutomatique(List<Plat> menuClient)
        {
            if (GestionnaireClients.Clients.Count > 0)
            {
                SOccuperDesClients(menuClient);
            }
            else
                Console.WriteLine($"Le restaurant {LeRestaurant.Nom} est actuellement vide");

            //on génère de nouveaux clients ainsi que leurs commandes en fonction du nombre de places libres dans le restaurant
            int nombrePlacesLibres = LeRestaurant.NombreMaxClient - GestionnaireClients.Clients.Count;
            if (nombrePlacesLibres > 0)
            {
                int nombreNouveauxClients = rand.Next(1, nombrePlacesLibres + 1);
                GestionnaireClients.GenererClients(nombreNouveauxClients);
                Console.WriteLine($"\n\nAttention, {nombreNouveauxClients} nouveau(x) client(s) vient(viennent) d'entrer\n");

                for (int i = 0; i < GestionnaireClients.Clients.Count; i++)
                    GestionnaireClients.Clients[i].Commander(LeRestaurant.MenuClient);
            }

        }




        public void SOccuperDesClients(List<Plat> menuClient)
        {

            double montantTotalDebourseParLesClients = 0;
            List<Plat> platsCommandes = new List<Plat>();
            //afficher le nombre de clients présents
            Console.WriteLine($"Le restaurant {LeRestaurant.Nom} compte actuellement {GestionnaireClients.Clients.Count} client(s)");

            //Servir tous le monde automatiquement
            Console.WriteLine("Le service automatique des clients est lancé\n\n");
            if (GestionnaireClients.Clients.Count > 0)
            {
                for (int i = 0; i < GestionnaireClients.Clients.Count; i++)
                {
                    if (GestionnaireClients.Clients[i].Choix.Count > 0)
                    {
                        montantTotalDebourseParLesClients += ServirClient(GestionnaireClients.Clients[i], ref platsCommandes, out int j);
                        i = i - j;
                    }
                }
                montantTotalDebourseParLesClients = Math.Round(montantTotalDebourseParLesClients, 2);

                //afficher les plats commandés par les clients et montant total déboursé par les clients
                Console.WriteLine($"Liste des plats commandés par les clients:\n");
                foreach (Plat plat in platsCommandes)
                    Console.WriteLine($" ~" + plat.Nom + "\n");

                Console.WriteLine($"Au total, les clients ont déboursé {montantTotalDebourseParLesClients}$");
            }
            else
                Console.WriteLine($"Le restaurant ne compte actuellement aucun client");
        }





        public double ServirClient(Client client, ref List<Plat> platsCommandes, out int j)
        {
            j = 0;
            //on enregistre toutes les commandes du client
            foreach (Plat plat in client.Choix)
                if (!platsCommandes.Contains(plat))
                    platsCommandes.Add(plat);

            bool tousLesPlatsDuClientServis = true;
            double sommeDeboursee = 0;
            bool[] platsServis = new bool[client.Choix.Count];
            //on parcours la liste des plats commandés par le client
            for (int i = 0; i < client.Choix.Count; i++)
            {
                //on part du principe que chaque plat du client doit lui etre servi
                platsServis[i] = true;

                double montantVerse = ServirUnPlat(client.Choix[i]);
                if (montantVerse == 0)
                {
                    tousLesPlatsDuClientServis = false;
                    //on marque false dans la case correspondante de platsServis
                    platsServis[i] = false;

                }
                else
                    sommeDeboursee += montantVerse;
            }
            //on retire de la liste des commandes du client les plats qui lui ont déjà été servis
            for (int i = platsServis.Length - 1; i >= 0; i--)
                if (platsServis[i] == true)
                    client.Choix.RemoveAt(i);
            //on verifie si tous les plats du client ont ete servis
            if (tousLesPlatsDuClientServis || client.Temperament == TypeTemperemment.Presse || client.Temperament == TypeTemperemment.Impulsif)
            {
                //on retire le client de la liste des clients
                if (tousLesPlatsDuClientServis)
                    Console.WriteLine($"Le client {client.NomComplet} a recu tous ses plats");
                else
                    Console.WriteLine($"Le client {client.NomComplet} était trop {client.Temperament} pour attendre la fin du service. Il a quitté le restaurant {LeRestaurant.Nom}");
                GestionnaireClients.Clients.Remove(client);
                j = 1;
            }
            else
            {
                Console.WriteLine($"Le client {client.NomComplet} n'a pas recu tous ses plats");
            }
            return sommeDeboursee;
        }






        public double ServirUnPlat(Plat plat)
        {
            bool disposeAssezIngredients = true;
            string listeIngredientsFinis = "";

            VerifierSiDisposeAssezIngredients(plat, ref listeIngredientsFinis, ref disposeAssezIngredients);

            if (disposeAssezIngredients)
            {
                return PreparerPlat(ref plat);
            }
            else
            {
                Console.WriteLine($"Le restaurant ne dispose pas d'assez d'ingrédients({listeIngredientsFinis}) pour préparer le plat {plat.Nom}");
                return 0;
            }
        }






        public void VerifierSiDisposeAssezIngredients(Plat plat, ref string listeIngredientsFinis, ref bool disposeAssezIngredients)
        {
            foreach (Ingredient ingredient in plat.Ingredients)
            {
                //on verifie pour chaque ingredient si le gestionnaire d'ingredients dispose d'assez d'ingredients en réserve
                if (GestionnaireIngredients.QuantiteIngredients[GestionnaireIngredients.Ingredients.IndexOf(ingredient)] < 1)
                {
                    disposeAssezIngredients = false;
                    listeIngredientsFinis = listeIngredientsFinis + ingredient.Nom + " ";
                }
            }

        }





        public double PreparerPlat(ref Plat plat)
        {

            for (int j = 0; j < plat.Ingredients.Count; j++)
            {
                //on identifie l'indice de cet ingredient dans la liste d'ingredient du gestionnaire d'ingredients
                int indiceIngredientDansGestionnaire = GestionnaireIngredients.Ingredients.IndexOf(plat.Ingredients[j]);

                //on retire 1 occurence des ingrédients utilisés
                GestionnaireIngredients.QuantiteIngredients[indiceIngredientDansGestionnaire] -= 1;
            }

            //on ajoute la somme versée au budget du restaurant
            LeRestaurant.Budget += plat.PrixVente;
            return plat.PrixVente;
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
                do
                {
                    Console.WriteLine("Entrer le nombre correspondant:\n\n (1) Retirer un plat du menu des clients \n (2) Ajouter un plat au menu des clients \n (3)Si vous avez terminé d'ajuster le menu");
                    nombreValide = int.TryParse(Console.ReadLine(), out nombreEntre);
                    if (nombreEntre != 1 && nombreEntre != 2 && nombreEntre != 3)
                    {
                        Console.WriteLine("Veuillez entrer un nombre valide");
                        nombreValide = false;
                    }
                } while (!nombreValide);

                //on retire un plat du menu client
                if (nombreEntre == 1)
                {
                    RetirerPlatDuMenuClient();
                }
                else if (nombreEntre == 2)
                {
                    //on crée une nouvelle liste contenant uniquement les plats qu'on peut ajouter au menu client
                    List<Plat> PlatsAjoutablesAuMenu = new List<Plat>();
                    ListerPlatsAjoutablesAuMenu(ref PlatsAjoutablesAuMenu);
                    if (PlatsAjoutablesAuMenu.Count > 0)
                        AjouterPlatAuMenu(PlatsAjoutablesAuMenu);
                    else
                        Console.WriteLine($"Tous les plats dont la recette est achetée font actuellement partie du menu. Pour en ajouter, veuillez acheter de nouvelles recettes de plat");
                }
            } while (nombreEntre != 3);

            //on fait commander les clients qui n'ont pas pu car le menu était vide
            FaireCommanderClients();
        }

        /// <summary>
        /// Permet de faire commander les clients qui n'ont pas pu car le menu était vide
        /// </summary>
        public void FaireCommanderClients()
        {
            for (int i = 0; i < GestionnaireClients.Clients.Count; i++)
            {
                if (GestionnaireClients.Clients[i].Choix.Count == 0)
                    GestionnaireClients.Clients[i].Commander(LeRestaurant.MenuClient);
            }
        }


        public void ListerPlatsAjoutablesAuMenu(ref List<Plat> PlatsAjoutablesAuMenu)
        {
            if (LeRestaurant.PlatsAchetes.Count > 0)
            {
                for (int i = 0; i < LeRestaurant.PlatsAchetes.Count; i++)
                {
                    //la liste de plats ajoutable au menu contient les plats dont la recette est achetée

                    PlatsAjoutablesAuMenu.Add(LeRestaurant.PlatsAchetes[i]);
                }

                if (LeRestaurant.MenuClient.Count > 0)
                    for (int i = 0; i < LeRestaurant.MenuClient.Count; i++)
                    {
                        //la liste de plats ajoutable au menu ne contient pas les plats qui sont déjà dans le menu client

                        PlatsAjoutablesAuMenu.Remove(LeRestaurant.MenuClient[i]);
                    }
            }
        }





        public void AjouterPlatAuMenu(List<Plat> PlatsAjoutablesAuMenu)
        {
            Console.WriteLine("Entrer le numero du plat que vous voulez Ajouter au menu client\n");

            //on affiche donc les plats que l'on peut ajouter au menu client
            for (int i = 0; i < PlatsAjoutablesAuMenu.Count; i++)
            {
                //on adapte le numéro des plat pour qu'il soit supérieur à 1(esthétique)
                Console.WriteLine($"({i + 1}) " + PlatsAjoutablesAuMenu[i].Nom + "\n");
            }

            //on fait choisir un plat à l'utilisateur
            int.TryParse(Console.ReadLine(), out int nombreEntre);

            //on l'ajoute au menu client(sans frais)
            if (nombreEntre >= 1 && nombreEntre <= PlatsAjoutablesAuMenu.Count)
            {
                LeRestaurant.MenuClient.Add(PlatsAjoutablesAuMenu[nombreEntre - 1]);
                Console.WriteLine("Le plat a été ajouté du menu client");
            }
            else
            {
                Console.WriteLine("Vous avez entré un nombre invalide");
            }
        }





        public void RetirerPlatDuMenuClient()
        {
            if (LeRestaurant.MenuClient.Count > 0)
            {
                int nombreEntre = 0;
                //on affiche la liste des plats contenus actuellement dans le menu client
                Console.WriteLine("Entrer le numero du plat que vous voulez retirer du menu client\n");
                for (int i = 0; i < LeRestaurant.MenuClient.Count; i++)
                {
                    //on adapte le numéro des plat pour qu'il soit supérieur à 1(esthétique)
                    Console.WriteLine($"({i + 1}) " + LeRestaurant.MenuClient[i].Nom + "\n");
                }

                if (nombreEntre >= 1 && nombreEntre <= LeRestaurant.MenuClient.Count)
                {
                    LeRestaurant.MenuClient.Remove(LeRestaurant.MenuClient[nombreEntre - 1]);
                    Console.WriteLine("Le plat a été retiré du menu client");
                }
            }
            else
                Console.WriteLine($"Le menu client est actuellement vide, vous ne pouvez pas en retirer un plat");
        }





        public void AcheterNouveauPlat()
        {
            if (LeRestaurant.PlatsPouvantEtreAchetes.Count > 0)
            {//afficher la liste des plats pouvant être achetés
                Console.WriteLine("Entrer le numero du plat dont vous voulez acheter la recette\n");
                for (int i = 0; i < LeRestaurant.PlatsPouvantEtreAchetes.Count; i++)
                {
                    //on adapte le numéro des plat pour qu'il soit supérieur à 1(esthétique)
                    Console.WriteLine($"({i + 1}) " + LeRestaurant.PlatsPouvantEtreAchetes[i] + "\n");
                }

                //proposer à l'utilisateur de faire un choix
                int.TryParse(Console.ReadLine(), out int nombreEntre);
                if (nombreEntre >= 1 && nombreEntre <= LeRestaurant.PlatsPouvantEtreAchetes.Count)
                {
                    //acheter s'il le budget du restaurant le permet
                    if (LeRestaurant.Budget >= LeRestaurant.PlatsPouvantEtreAchetes[nombreEntre - 1].PrixRecette)
                    {
                        //on achete la recette avec le budget du restaurant
                        LeRestaurant.Budget -= LeRestaurant.PlatsPouvantEtreAchetes[nombreEntre - 1].PrixRecette;
                        LeRestaurant.PlatsAchetes.Add(LeRestaurant.PlatsPouvantEtreAchetes[nombreEntre - 1]);
                        Console.WriteLine("Le plat a été ajouté à la liste de plats achetés");
                        //on retire le plat à la liste des plats pouvant être achetés
                        LeRestaurant.PlatsPouvantEtreAchetes.Remove(LeRestaurant.PlatsPouvantEtreAchetes[nombreEntre - 1]);
                    }
                    else
                        Console.WriteLine($"Le restaurant {LeRestaurant.Nom} ne dispose pas d'assez de fonds pour acheter la recette du plat {LeRestaurant.PlatsPouvantEtreAchetes[nombreEntre - 1].Nom}");
                }
                else
                    Console.WriteLine("Vous avez entré un nombre invalide");
            }
            else
                Console.WriteLine("Vous avez déjà acheté toutes les recettes de plat en réserve");
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

            int numeroIngredient = 0;
            int quantiteIngredientAAcheter = 0;
            VerifierSiIngredientEtQuantiteValides(ref numeroIngredient, ref quantiteIngredientAAcheter);
            //ON VÉRIFIE SI LE RESTAURANT POSSÈDE LES FONDS NÉCESSAIRES POUR ACHETER CET INGRÉDIENT
            if (LeRestaurant.Budget >= quantiteIngredientAAcheter * GestionnaireIngredients.Ingredients[numeroIngredient - 1].Prix)
            {
                LeRestaurant.Budget = LeRestaurant.Budget - quantiteIngredientAAcheter * GestionnaireIngredients.Ingredients[numeroIngredient - 1].Prix;
                GestionnaireIngredients.QuantiteIngredients[numeroIngredient - 1] += quantiteIngredientAAcheter;
                Console.WriteLine($"Vous venez d'acheter {quantiteIngredientAAcheter} {GestionnaireIngredients.Ingredients[numeroIngredient - 1].Nom}");
            }
            else
            {
                Console.WriteLine($" Le restaurant {LeRestaurant.Nom} ne possède pas les fonds nécessaires pour effectuer cet achat");
            }
        }




        public void VerifierSiIngredientEtQuantiteValides(ref int numeroIngredient, ref int quantiteIngredientAAcheter)
        {
            //s'assurer que l'utilisateur entre un numéro correspondant à un ingredient
            numeroIngredient = 0;
            do
            {
                int.TryParse(Console.ReadLine(), out numeroIngredient);
            } while (numeroIngredient < 1 || numeroIngredient > GestionnaireIngredients.Ingredients.Count);

            Console.WriteLine("Entrer la quantité à acheter");
            int.TryParse(Console.ReadLine(), out quantiteIngredientAAcheter);

            //s'assurer que l'utilisateur rentre une quantité valide
            while (quantiteIngredientAAcheter <= 0)
            {
                Console.WriteLine("Veuillez entrer une quantité valide");
                int.TryParse(Console.ReadLine(), out quantiteIngredientAAcheter);
            }
        }






        public void EngagerEmploye()
        {
            string[] noms = { "Bouchard", "Brassard", "Perron", "Dupont", "Cote", "Morin", "Lapointe", "Larouche", "Gaudreault", "Pineault", "Boivin", "Potvin", "Lavoie", "Simard", "Dallaire" };
            string[] prenoms = { "Réal", "Annie-Claude", "Rebecca", "Nicolas", "Laura", "Michel", "Kim", "Cindy", "Jeffrey", "Hendrick", "Laurent", "Kevin", "Félix", "Nathan", "Sarah" };

            //on génère une liste de postulants

            Employe[] postulants = new Employe[5];
            for (int i = 0; i < postulants.Length; i++)
            {
                postulants[i] = new Employe(prenoms[rand.Next(0, prenoms.Length)] + " " + noms[rand.Next(0, noms.Length)]);
            }

            Console.WriteLine($"Voici la liste des employés qui ont actuellement postulé au restaurant {LeRestaurant.Nom}:\n");
            for (int i = 0; i < postulants.Length; i++)
            {
                Console.WriteLine($"({i + 1}) " + postulants[i]);
            }
            Console.WriteLine("Entrer le numero de celui que vous voulez engager");

            int.TryParse(Console.ReadLine(), out int numeroEmployeEngage);

            if (numeroEmployeEngage > 0 && numeroEmployeEngage <= postulants.Length)
            {
                postulants[numeroEmployeEngage - 1].ActiverBonus(LeRestaurant);
                //to do : construire un simulateur afin que le activerBonus de l'employé puisse prendre en paramètre le restaurant 
                LeRestaurant.Employes.Add(postulants[numeroEmployeEngage - 1]);

            }
            else
                Console.WriteLine("Vous avez entré un numero invalide");

        }

    }
}
