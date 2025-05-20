using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projet
{
    public static class GestionnaireClients
    {
        public static List<Client> Clients { get; set; } = new List<Client>();

        public static void GenererClients(int nombre)
        {
            //vide la liste actuelle de clients
            Clients.Clear();

            //fabrique des nouveaux clients et les ajoute à la liste de clients
            for (int i = 0; i < nombre; i++)
            {
                string nom = FabriquerClient.GeNomComplet();
                TypeTemperemment temperament = FabriquerClient.GetTemperament();
                Clients.Add(new Client(nom, temperament));

            }
        }

    }
}
