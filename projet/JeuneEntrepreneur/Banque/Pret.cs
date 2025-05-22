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
        public Joueur Joueur {get; set;}

        public Pret(int montant,Actif garantie,Joueur joueur)
        {
            Montant = montant;
            TauxInteret = Program.rand.Next(2,10);
            Garantie = garantie;
            Rembourse = 0;
            Joueur = joueur;
        }

        public int MontantTotalARembourser()
        {
            return Montant + (Montant*TauxInteret/100);
        }

        public int MontantRestant()
        {
            return MontantTotalARembourser() - Rembourse;
        }

        public bool EstRembourser()
        {
            return Rembourse >= MontantTotalARembourser();
        }


        public string AfficherInfoPret()
        {

            string info = $"\n📄 Détails du prêt :";
            info += $"Montant emprunté : {Joueur?.PretEnCours?.Montant} $ | Taux d’intérêt : {Joueur?.PretEnCours?.TauxInteret} % \n";
            info += $"Montant total à rembourser : {Joueur?.PretEnCours?.MontantTotalARembourser()} $ | Déjà remboursé : {Joueur?.PretEnCours?.Rembourse} $\n";
            info += $"Montant restant : {Joueur?.PretEnCours?.MontantRestant()} $";
            return info ;
        }

        public override string ToString()
        {
            return $"{AfficherInfoPret()}";
        }
    }
}
