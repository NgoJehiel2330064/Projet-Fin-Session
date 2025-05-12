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
            Clients.Clear();

            for (int i = 0; i < nombre; i++)
            {
                string nom = FabriqueNom.GeNomComplet();
                string temperament = FabriqueNom.GetTemperament();
                Clients.Add(new Client(nom, temperament));
            }
        }
    }
}
