using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JeuneEntrepreneur.Actifs;
using JeuneEntrepreneur;


namespace JeuneEntrepreneur.Banque
{
    public class BanqueCentrale
    {
        public int TauxInteretParDefaut { get; private set; }

        public BanqueCentrale() 
        {
            // Génère un taux d’intérêt unique entre 3% et 9%
            TauxInteretParDefaut = Program.rand.Next(3, 10);
        }

        // Donne un prêt si possible
        public void AccorderPret(Joueur joueur, int montant, Actif garantie)
        {
            if (joueur.PretEnCours != null && !joueur.PretEnCours.EstRembourser())
            {
                Console.WriteLine(" Vous avez déjà un prêt actif non remboursé.");
                return;
            }

            if (!joueur.Actifs.Contains(garantie))
            {
                Console.WriteLine(" Cette garantie ne vous appartient pas.");
                return;
            }

            if (garantie.Valeur < montant)
            {
                Console.WriteLine(" L’actif garanti est moins cher que le montant emprunté.");
                return;
            }

            // Création et attribution du prêt
            Pret pret = new Pret(montant, garantie,joueur);
            joueur.PretEnCours = pret;
            joueur.AjouterArgent(montant);

            Console.WriteLine($"Prêt de {montant}$ accordé avec {garantie.Nom} en garantie à {pret.TauxInteret}% d’intérêt.");
        }

        // Vérifie si le prêt n'est pas remboursé, et saisit la garantie
        public void VerifierEtSaisirGarantie(Joueur joueur)
        {
            if (joueur.PretEnCours != null && !joueur.PretEnCours.EstRembourser())
            {
                Console.WriteLine("Le prêt n’a pas été remboursé. La banque saisit votre actif en garantie !");
                joueur.Actifs.Remove(joueur.PretEnCours.Garantie);
                joueur.PretEnCours = null;
            }
        }
    }
}
