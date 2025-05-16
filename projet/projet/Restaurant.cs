using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Xml.Linq;

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
        public List<Employe> Employes { get; set; }
        public int NombreMaxClient { get; set; }
        public double Budget { get; set; }

        static Random rand = new Random();



        public Restaurant(string nom, double budget)
        {
            Nom = nom;
            //par défaut le restaurant est ouvert à sa création
            Statut = Statut.Ouvert;

            PlatsPouvantEtreAchetes = new List<Plat>();
            MenuClient = new List<Plat>();
            PlatsAchetes = new List<Plat>();
            Employes = new List<Employe>();

            Budget = budget;
            NombreMaxClient = 20;

            //Disons qu'au début, le restaurant compte 5 clients
            GestionnaireClients.GenererClients(5);
            for (int i = 0; i < GestionnaireClients.Clients.Count; i++)
                GestionnaireClients.Clients[i].Commander(MenuClient);
        }


        public override string ToString()
        {
            return $"Restaurant : {Nom} | Statut : {Statut} |  Budget : {Budget}$";
        }
    }
}
