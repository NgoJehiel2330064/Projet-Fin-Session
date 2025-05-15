using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projet
{
    
    public static class FabriqueNom
    {
        static List<string> Noms = new List<string>();
        static List<string> Prenoms = new List<string>();
        static Random rand = new Random();

        public static void ChargerFichiers(string nom, string prenom)
        {
            if(File.Exists(nom))
            Noms.AddRange(File.ReadAllLines(nom));
            else
                Console.WriteLine($"Fichier des noms non trouvé !");

            if (File.Exists(prenom))
                Prenoms.AddRange(File.ReadAllLines(prenom));
            else
                Console.WriteLine($"Fichier des prenoms non trouvé !");

        }

        public static string GeNomComplet()
        {
            if (Noms.Count == 0 || Prenoms.Count == 0)
                return "Pas de Nom";

            string prenom = Prenoms[rand.Next(Prenoms.Count)];
            string nom = Noms[rand.Next(Noms.Count)];
            return $"{prenom} {nom}";   
        }

        public static TypeTemperemment GetTemperament()
        {
            TypeTemperemment[] temperaments = { TypeTemperemment.Calme, TypeTemperemment.Presse, TypeTemperemment.Impulsif, TypeTemperemment.Patient, TypeTemperemment.Indécis };
            return temperaments[rand.Next(temperaments.Length)];
        }

    }
}
