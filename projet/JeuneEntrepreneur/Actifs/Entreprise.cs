using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeuneEntrepreneur.Actifs
{
    public class Entreprise : Actif
    {
        public string Secteur { get; set; }
        public int RevenuMensuel { get; set; }

        public Entreprise(string nom,int valeur,string secteur)  : base(nom,valeur,TypeActif.Entreprise) 
        { 
            Secteur = secteur;
            RevenuMensuel = Program.rand.Next(10000);
        }

        public override string ToString()
        {
            return $"{ base.ToString()} \n Secteur : {Secteur} | Revenu Mensuel : {RevenuMensuel} ";
        }

        public void GenererRevenu(Joueur joueur)
        {
            Console.WriteLine($"L'entreprise {Nom} génère {RevenuMensuel} $");
            joueur.AjouterArgent(RevenuMensuel);
        }
    }
}
