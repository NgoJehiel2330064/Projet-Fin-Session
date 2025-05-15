using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeuneEntrepreneur.Actifs
{
    public class Immeuble : Actif
    {
        public int NbAppartements { get; set; }
        public int LoyerMensuelTotal { get; set; }

        public Immeuble(string nom, int valeur, int nbAppartements ): base(nom,valeur,TypeActif.Immeuble)
        {
            NbAppartements = nbAppartements;
            
            LoyerMensuelTotal = nbAppartements * Program.rand.Next(500, 1500);
        }

        public override string ToString()
        {
            return $"{base.ToString()} | Appartements : {NbAppartements} | Revenus : {LoyerMensuelTotal} $/mois";
        }

        public void GenererLoyer(Joueur joueur)
        {
            Console.WriteLine($"🏢 L’immeuble {Nom} génère {LoyerMensuelTotal} $ de loyer.");
            joueur.AjouterArgent(LoyerMensuelTotal);
        }


    }
}
