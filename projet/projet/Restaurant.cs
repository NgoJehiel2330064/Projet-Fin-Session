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
        public Menu Menu { get; set; }

    }
}
