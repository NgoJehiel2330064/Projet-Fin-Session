using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JeuneEntrepreneur.Actifs;
using JeuneEntrepreneur;

namespace JeuneEntrepreneur.Banque
{
    public class Pret
    {
        public int Montant {get; set;}
        public int Rembourse {get; set;}
        public int TauxInteret { get; set;}
        public Actif Garantie {get; set;}

        public Pret(int montant,Actif garantie)
        {
            Montant = montant;
            TauxInteret = Program.rand.Next(2,10);
            Garantie = garantie;
            Rembourse = 0;
        }

        public int MontantTotalARembourser()
        {
            return Montant *  TauxInteret;
        }

        public int MontantRestant()
        {
            return MontantTotalARembourser() - Rembourse;
        }

        public bool EstRembourser()
        {
            return Rembourse >= MontantTotalARembourser();
        }

    }
}
