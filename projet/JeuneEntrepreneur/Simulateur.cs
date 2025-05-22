using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JeuneEntrepreneur.Actifs;
using JeuneEntrepreneur.Banque;

namespace JeuneEntrepreneur
{
    public class Simulateur
    {
        public Joueur Joueur {  get; set; }
        public BanqueCentrale Banque { get; set; }
        public Actif Actif { get; set; }
        public void Demarrer()
        {
            Console.WriteLine(" Bienvenue dans le simulateur de jeune entrepreneur !");
            Console.Write("Entrez votre nom : ");
            string nom = Console.ReadLine();
            Console.Write("Entrez votre capital : ");
            int.TryParse(Console.ReadLine(), out int capital);



            Joueur = new Joueur(nom, capital); // capital initial
            Banque = new BanqueCentrale();
            bool enCours = true;

            while (enCours)
            {
                Console.Clear();
                Console.WriteLine($"=== Menu principal de {Joueur.Nom} ===");
                Console.WriteLine("1. Voir le statut");
                Console.WriteLine("2. Créer une entreprise");
                Console.WriteLine("3. Acheter un terrain");
                Console.WriteLine("4. Construire une maison");
                Console.WriteLine("5. Acheter une voiture");
                Console.WriteLine("6. Acheter un immeuble locatif");
                Console.WriteLine("7. Emprunter à la banque");
                Console.WriteLine("8. Rembourser le prêt");
                Console.WriteLine("9. Passer au mois suivant (générer les revenus)");
                Console.WriteLine("10. Vendre un actif");
                Console.WriteLine("0. Quitter");
                Console.Write("\nVotre choix : ");
                int.TryParse(Console.ReadLine(),out int choix);

                switch (choix)
                {
                    case 1:
                        Console.WriteLine( Joueur.ToString());
                        break;
                    case 2:
                        AcheterEntreprise();
                        break;
                    case 3:
                        AcheterTerrain();
                        break;
                    case 4:
                        ConstruireMaison();
                        break;
                    case 5:
                        AcheterVoiture();
                        break;
                    case 6:
                        AcheterImmeuble();
                        break;
                    case 7:
                        EmprunterBanque();
                        break;
                    case 8:
                        RembourserPret();
                        break;
                    case 9:
                        PasserMois();
                        break;
                    case 10:
                        VendreActif();
                        break;
                    case 0:
                        enCours = false;
                        Console.WriteLine(" Merci d’avoir joué !");
                        break;
                    default:
                        Console.WriteLine(" Choix invalide.");
                        break;
                }

                Console.WriteLine("\nAppuie sur une touche pour continuer...");
                Console.ReadKey();
            }
            
        }

       
        private void AcheterEntreprise()
        {
            Console.Write("\nNom de l'entreprise : ");
            string nom = Console.ReadLine();

            Console.Write("Secteur d'activité : ");
            string secteur = Console.ReadLine();

            Console.Write("Valeur de l'entreprise : ");
            if (!int.TryParse(Console.ReadLine(), out int prix))
            {
                Console.WriteLine(" Montant invalide.");
                return;
            }

            if (Joueur.Depenser(prix))
            {
                Entreprise e = new Entreprise(nom, prix, secteur);
                Joueur.AjouterActif(e);
                Console.WriteLine(" Entreprise achetée avec succès !");
            }
        }

        private void AcheterTerrain()
        {
            Console.Write("\nNom du terrain : ");
            string nom = Console.ReadLine();

            Console.Write("Prix du terrain : ");
            if (!int.TryParse(Console.ReadLine(), out int prix))
            {
                Console.WriteLine(" Montant invalide.");
                return;
            }

            if (Joueur.Depenser(prix))
            {
                Terrain terrain = new Terrain(nom, prix);
                Joueur.AjouterActif(terrain);
                Console.WriteLine($" Terrain acheté avec succès ! Sa superficie est de {terrain.Superficie} m²");
            }
        }

        private void ConstruireMaison()
        {
            Console.Write("\nNom de la maison : ");
            string nom = Console.ReadLine();

            Console.Write("Prix de la maison : ");
            if (!int.TryParse(Console.ReadLine(), out int prix))
            {
                Console.WriteLine(" Montant invalide.");
                return;
            }

            if (Joueur.Depenser(prix))
            {
                Maison maison = new Maison(nom, prix);
                Joueur.AjouterActif(maison);
                Console.WriteLine($" Maison construite avec succès !\n Information de votre nouvelle maison : \n {maison.ToString()}");
            }
        }


        private void AcheterVoiture()
        {
            

            Console.Write("Marque : ");
            string marque = Console.ReadLine();
            string nom = marque;

            Console.Write("Année : ");
            int modele = Convert.ToInt32(Console.ReadLine());

            Console.Write("Prix : ");
            if (!int.TryParse(Console.ReadLine(), out int prix))
            {
                Console.WriteLine(" Prix invalide.");
                return;
            }

            if (Joueur.Depenser(prix))
            {
                Voiture voiture = new Voiture(nom, prix, marque, modele);
                Joueur.AjouterActif(voiture);
                Console.WriteLine($" Voiture achetée avec succès ! \n Information de votre nouvelle voiture : \n {voiture.ToString()}");
            }
        }



        private void AcheterImmeuble()
        {
            Console.Write("\nNom de l’immeuble : ");
            string nom = Console.ReadLine();

            Console.Write("Prix : ");
            if (!int.TryParse(Console.ReadLine(), out int prix))
            {
                Console.WriteLine(" Prix invalide.");
                return;
            }

            Console.Write("Nombre d'appartements : ");
            if (!int.TryParse(Console.ReadLine(), out int nbAppart))
            {
                Console.WriteLine(" Nombre invalide.");
                return;
            }

            if (Joueur.Depenser(prix))
            {
                Immeuble immeuble = new Immeuble(nom, prix, nbAppart);
                Joueur.AjouterActif(immeuble);
                Console.WriteLine(" Immeuble acheté avec succès !");
            }
        }

        private void EmprunterBanque()
        {
            if (Joueur.PretEnCours != null && !Joueur.PretEnCours.EstRembourser())
            {
                Console.WriteLine(" Vous avez déjà un prêt actif non remboursé.");
                return;
            }

            if (Joueur.Actifs.Count == 0)
            {
                Console.WriteLine(" Vous devez posséder au moins un actif pour garantir votre prêt.");
                return;
            }

            Console.WriteLine("\n Vos actifs disponibles pour garantie :");
            for (int i = 0; i < Joueur.Actifs.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Joueur.Actifs[i]}");
            }

            Console.Write("\nChoisissez l’actif à mettre en garantie (par numéro) : ");
            if (!int.TryParse(Console.ReadLine(), out int choixGarantie) || choixGarantie < 1 || choixGarantie > Joueur.Actifs.Count)
            {
                Console.WriteLine(" Choix invalide.");
                return;
            }

            Actif actifChoisi = Joueur.Actifs[choixGarantie - 1];

            Console.Write("Montant à emprunter : ");
            if (!int.TryParse(Console.ReadLine(), out int montant) || montant <= 0)
            {
                Console.WriteLine(" Montant invalide.");
                return;
            }

            Banque.AccorderPret(Joueur, montant, actifChoisi);
        }




        private void RembourserPret()
        {
            if (Joueur.PretEnCours == null)
            {
                Console.WriteLine("📭 Vous n'avez aucun prêt actif.");
                return;
            }

            Pret pret = Joueur.PretEnCours;

            Console.WriteLine($"\n📄 Détails du prêt :");
            Console.WriteLine($"Montant emprunté : {pret.Montant} $");
            Console.WriteLine($"Taux d’intérêt : {pret.TauxInteret} %");
            Console.WriteLine($"Montant total à rembourser : {pret.MontantTotalARembourser()} $");
            Console.WriteLine($"Déjà remboursé : {pret.Rembourse} $");
            Console.WriteLine($"Montant restant : {pret.MontantRestant()} $");

            Console.Write("Montant que vous voulez rembourser maintenant : ");
            if (!int.TryParse(Console.ReadLine(), out int montant) || montant <= 0)
            {
                Console.WriteLine(" Montant invalide.");
                return;
            }

            if (montant > Joueur.Capital)
            {
                Console.WriteLine(" Vous n’avez pas assez d’argent.");
                return;
            }

            Joueur.Capital -= montant;
            pret.Rembourse += montant;

            Console.WriteLine($" Vous avez remboursé {montant} $.");

            if (pret.EstRembourser())
            {
                Console.WriteLine(" Vous avez remboursé la totalité du prêt !");
                Joueur.PretEnCours = null;
            }
        }




        private void PasserMois()
        {
            Console.WriteLine("\n📆 Un mois s'est écoulé...");
            int totalRevenus = 0;

            foreach (var actif in Joueur.Actifs)
            {
                if (actif is Entreprise entreprise)
                {
                    entreprise.GenererRevenu(Joueur);
                    totalRevenus += entreprise.RevenuMensuel;
                }
                else if (actif is Immeuble immeuble)
                {
                    immeuble.GenererLoyer(Joueur);
                    totalRevenus += immeuble.LoyerMensuelTotal;
                }
            }

            Console.WriteLine($"\n Total des revenus générés ce mois-ci : {totalRevenus} $");

            // Vérifie si le prêt est toujours actif et saisit la garantie si besoin
            Banque.VerifierEtSaisirGarantie(Joueur);
        }


        private void VendreActif()
        {
            if (Joueur.Actifs.Count == 0)
            {
                Console.WriteLine(" Vous ne possédez aucun actif à vendre.");
                return;
            }

            Console.WriteLine("\n Vos actifs :");
            for (int i = 0; i < Joueur.Actifs.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Joueur.Actifs[i]}");
            }

            Console.Write("Choisissez l’actif à vendre (par numéro) : ");
            if (!int.TryParse(Console.ReadLine(), out int choix) || choix < 1 || choix > Joueur.Actifs.Count)
            {
                Console.WriteLine(" Choix invalide.");
                return;
            }

            Actif actifAVendre = Joueur.Actifs[choix - 1];

            // Empêche de vendre un actif mis en garantie
            if (Joueur.PretEnCours != null && Joueur.PretEnCours.Garantie == actifAVendre)
            {
                Console.WriteLine(" Vous ne pouvez pas vendre un actif utilisé comme garantie de prêt.");
                return;
            }

            // Par défaut : récupération de 90% de la valeur
            int montantRecu = (int)(actifAVendre.Valeur * 0.9);

            Joueur.AjouterArgent(montantRecu);
            Joueur.Actifs.Remove(actifAVendre);

            Console.WriteLine($" Vous avez vendu {actifAVendre.Nom} pour {montantRecu} $");
        }
    }
}
