using System;

namespace SGSP.AppCode
{
    public class Mission
    {
        public int IDMission { get; set; }
        public string Objet { get; set; }
        public string Destination { get; set; }
        public DateTime DateDepart { get; set; }
        public DateTime DateRetour { get; set; }
        public string Transport { get; set; }
        public string Imputation { get; set; }
        public string  Matricule { get; set; }
        public int Exercice { get; set; }
        public string Etat { get; set; }
        public bool SiPayant { get; set; }
        public bool SiPersonnelProjet { get; set; }
        public double Frais { get; set; }
        public int  Durée { get; set; }
        public int Ordre { get; set; }
        public double FraisTotal { get; set; }
        public string Role { get; set; }
        public string NomEmploye { get; set; }
    }
}
