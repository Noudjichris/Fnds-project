using System;

namespace SGSP.AppCode
{
    public class Formation
    {
        public int NumeroFormation { get; set; }
        public string TypeFormation { get; set; }
        public DateTime DateDebutFormation { get; set; }
        public DateTime DateFinFormation { get; set; }
        public string Description { get; set; }
        public int DureeFormation { get; set; }
        public string   NumeroMatricule { get; set; }
        public string Formateur { get; set; }
        public string NomPersonnel { get; set; }
        public string Imputation { get; set; }
        public string LieuFormation { get; set; }
        public bool SiProjet { get; set; }
        public int  Exercice { get; set; }
        public bool SiPayant { get; set; }
        public double Frais { get; set; }
        public string Fonction        { get; set; }
        public double  FraisTotal { get; set; }
    }
}
