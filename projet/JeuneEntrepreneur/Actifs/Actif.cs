using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JeuneEntrepreneur.Banque;
using JeuneEntrepreneur;

namespace JeuneEntrepreneur.Actifs
{
    public enum TypeActif
    {
        Entreprise,
        Terrain,
        Maison,
        Voiture,
        Immeuble
    };
    public abstract class Actif
    {
        public string Nom {  get; set; }
        public int Valeur { get; set; }

        public TypeActif TypeActif { get; set; }

        public Actif(string nom,int valeur,TypeActif typeActif) 
        {
            Nom = nom;
            Valeur = valeur;
            TypeActif = typeActif;
        }

        public override string ToString()
        {
            return $"Nom : {Nom} | Valeur : {Valeur} | Type d'Actif : {TypeActif}";
        }

    }
}
