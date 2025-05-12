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

        public Client(string nomComplet,string temperament)
        {
            NomComplet = nomComplet;
            Temperament = temperament;
        }

        public override string ToString()
        {
            return $"Nom : {NomComplet} | Tempérament : {Temperament}";
        }
    }
}
