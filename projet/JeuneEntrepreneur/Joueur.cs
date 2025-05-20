using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JeuneEntrepreneur.Actifs;
using JeuneEntrepreneur.Banque;

namespace JeuneEntrepreneur
{
    public class Joueur
    {
        public string Nom {  get; set; }
        public int Capital { get; set; }
        public List<Actif> Actifs { get; set; }
        public Pret? PretEnCours { get; set; }

        public Joueur(string nom,int capital) 
        {
            Nom = nom;
            Capital = capital;
            Actifs = new List<Actif>();
            PretEnCours = null;
        }

        public Joueur() 
        {
            Nom = "Jehiel";
            Capital = 100000;
            Actifs = new List<Actif>();
            PretEnCours = null;
        }

        public string AfficherActifs()
        {
            Console.WriteLine();
            string info = string.Empty;
            if (Actifs.Count == 0)
            {
                info += $"Vous n'avez aucun actif pour le moment";
            }
            else
            {
                foreach (var actif in Actifs)
                    info +=  $"- {actif}\n";
            }
            return info;
        }

        public void AjouterActif(Actif actif)
        {
            Actifs.Add(actif);
            Console.WriteLine($"\n {actif.Nom} ajouté à vos actifs !");
        }

        public bool Depenser(int montant)
        {
            if (Capital > montant)
            { Capital -= montant;
                return true; }
            else 
            {
                Console.WriteLine($"Fonds Insuffisants.");
                return false;
            }
        } 

        public void AjouterArgent(int montant)
        {
            Capital += montant;
        }



        public void RembourserPret(int montant)
        {
            if (PretEnCours == null)
            {
                Console.WriteLine("📭 Vous n'avez aucun prêt à rembourser.");
                return;
            }

            if (montant > Capital)
            {
                Console.WriteLine("❌ Fonds insuffisants pour ce remboursement.");
                return ;
            }

            Capital -= montant;
            PretEnCours.Rembourse += montant;

            Console.WriteLine($"✅ Vous avez remboursé {montant} $.");

            if (PretEnCours.EstRembourser())
            {
                Console.WriteLine("🎉 Vous avez remboursé l'intégralité de votre prêt !");
                PretEnCours = null;
            }
        }


        public override string ToString()
        {
            return $"Nom : {Nom} | Capitale disponible : {Capital} \n Voici la listes de vos actifs : \n{AfficherActifs()} ";
        }



    }
}
