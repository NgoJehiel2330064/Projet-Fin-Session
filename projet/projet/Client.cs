using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projet
{
    public class Client
    {
        public string NomComplet { get; set; }    
        public string Temperament { get; set; }
        public List<Plat> Choix { get; set; }
        static Random rand = new Random();

        public Client(string nomComplet,string temperament)
        {
            NomComplet = nomComplet;
            Temperament = temperament;
            Choix = new List<Plat>();
        }


        public void Commander(List<Plat> menuClient)
        {
            //disons qu'un client commande à lui seul entre 1 et 3 plats
            int nombrePlatCommande = rand.Next(1, 4);
            for (int i = 0; i < nombrePlatCommande; i++)
            { 
                //les plats commandés sont choisis au hasard(je ne comprend pas bien le principe de rareté)
                int numeroPlat = rand.Next(1, menuClient.Count);
                Choix.Add(menuClient[numeroPlat]);
            }

        }

        public override string ToString()
        {
            string message = $"Nom : {NomComplet} | Tempérament : {Temperament}";
            if(Choix.Count != 0)
            {
                message += " | Commande : (";
                foreach (Plat plat in Choix)
                    message = message + " "+ plat.Nom;
                message += " )";
            }
            return message;
        }
    }
}
