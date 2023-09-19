using System;

namespace SGSP.AppCode
{
    public class Absence
    {
        public string NumeroEmploye { get; set; }
        public DateTime DateDebutAbscense { get; set; }
        public DateTime DateRetour { get; set; }
        public DateTime DateDemande { get; set; }
        public string Motif { get; set; }
        public int IDAbsence { get; set; }
        public string NomPersonnel { get; set; }
        public string Destination { get; set; }      
        public bool  SiPersonnelProjet { get; set; }
        public int Duree { get; set; }
        public int Exercice { get; set; }
        public string Fonction { get; set; }      
    }

}
