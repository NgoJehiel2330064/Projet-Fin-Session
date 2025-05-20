using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projet
{
    public class Employe
    {
        public string Nom { get; set; }
        public int Rarete { get; set; }
        public string Description { get; set; }
        public bool BonusActive { get; private set; }

        static Random rand = new Random();

        public Employe(string nom) 
        {
            Nom = nom;
            BonusActive = false;
            int indiceRarete = rand.Next(1,101);

            if (indiceRarete > 61)
            {
                Rarete = 1;
                Description = "Il permettra au restaurant d'acheter ses ingredients pour 1$ de moins.";
            }
            else if (indiceRarete > 31)
            {
                Rarete = 2;
                Description = "Sa courtoisie et son sourire rendront les clients du restaurant plus patients.";
            }
            else if (indiceRarete > 11)
            {
                Rarete = 3;
                Description = "Il connait la recette d'un plat très rare qui augmentera les profits du restaurant.";
            }
            else
            {
                Rarete = 4;
                Description = "Son sens de l'organisation permettra au restaurant d'accueillir 10 clients de plus.";
            }
        }

        public void ActiverBonus(Restaurant restaurant)
        {
            if (!BonusActive)
            {
                switch (Rarete)
                {
                    case 1:
                        LancerBonus1();
                        break;
                    case 2:
                        LancerBonus2();
                        break;
                    case 3:
                        LancerBonus3(restaurant);
                        break;
                    default:
                        LancerBonus4(restaurant);
                        break;
                }
                BonusActive = true;
            }
            else
            {
                Console.WriteLine($"Le bonus de l'employé {Nom} est déjà actif");
            }
        }

        public void LancerBonus1()
        {
            //Les ingredients coûtent 1$ de moins

            for(int i = 0; i < GestionnaireIngredients.Ingredients.Count; i++)
            {
                GestionnaireIngredients.Ingredients[i].Prix -= 1;
            }
            Console.WriteLine($"Grace à l'employé {Nom}, tous les ingrédients coûtent désormais 1$ de moins");
        }

        public void LancerBonus2()
        {
            //Les clients sont plus patients
            //on rend patients tous les clients pressés ou impulsif du restaurant
            for(int i = 0; i < GestionnaireClients.Clients.Count; i++)
            {
                if (GestionnaireClients.Clients[i].Temperament == TypeTemperemment.Presse || GestionnaireClients.Clients[i].Temperament == TypeTemperemment.Impulsif)
                    GestionnaireClients.Clients[i].Temperament = TypeTemperemment.Patient;
            }
            //on fait en sorte que tous les futurs clients soient soient soit calme, soit indécis soit patient
            FabriquerClient.CliensPlusPatients = true;

            Console.WriteLine($"Grace à l'employé {Nom}, les clients sont désormais plus patients");

        }

        public void LancerBonus3(Restaurant restaurant)
        {
            //Une recette très rare est ajoutée dans la liste des plats achetés

                    //la recette contient les meilleurs ingrédients du gestionnaire
            List<Ingredient> ingredients = new List<Ingredient>();
            ingredients.Add(GestionnaireIngredients.Ingredients[1]);
            ingredients.Add(GestionnaireIngredients.Ingredients[4]);
            ingredients.Add(GestionnaireIngredients.Ingredients[5]);
            ingredients.Add(GestionnaireIngredients.Ingredients[12]);
            ingredients.Add(GestionnaireIngredients.Ingredients[13]);
            ingredients.Add(GestionnaireIngredients.Ingredients[14]);
            ingredients.Add(GestionnaireIngredients.Ingredients[17]);
            ingredients.Add(GestionnaireIngredients.Ingredients[18]);
            ingredients.Add(GestionnaireIngredients.Ingredients[19]);
            restaurant.PlatsAchetes.Add(new Plat("Plat spécial bonus " + Nom,0, ingredients));
            Console.WriteLine($"Grace à l'employé {Nom}, une recette très rare figure désormais dans la liste des plats achetés par le restaurant");


        }

        public void LancerBonus4(Restaurant restaurant)
        {
            //Augmenter de 10 le nombre de places du restaurant
            restaurant.NombreMaxClient += 10;

            Console.WriteLine($"Grace à l'employé {Nom}, le restaurant {restaurant.Nom} pourra désormais accueillir 10 clients de plus");

        }

        public override string ToString()
        {
            string message = $"L'employé {Nom} a une rareté de {Rarete}/4\n" + Description ;
            return message;
        }
    }
}
