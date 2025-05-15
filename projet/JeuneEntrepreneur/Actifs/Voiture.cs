using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeuneEntrepreneur.Actifs
{
    public class Voiture : Actif
    {
        public string Marque {  get; set; }
        public string Modele { get; set; }
        public int Annee { get; set; }

        public Voiture (string nom,int valeur,string modele,int annee): base(nom,valeur,TypeActif.Voiture)
        {
            Marque = nom;
            Modele = modele;
            Annee = annee;
        }

        public override string ToString()
        {
            return $"{base.ToString()} | Marque : {Marque} | Modèle : {Modele} | Année : {Annee}";
        }
    }
}
