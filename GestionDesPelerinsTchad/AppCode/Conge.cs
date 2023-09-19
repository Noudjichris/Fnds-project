using System;

namespace SGSP.AppCode
{
   public  class Conge
   {
       public string NumeroMatricule { get; set; }
       public DateTime DateDebutConge { get; set; }
       public DateTime DateRetour { get; set; }
       public DateTime DateDemande { get; set; }
       public string NatureConge { get; set; }
       public int IDConge { get; set; }
       public string NomPersonnel { get; set; }
       public double MontantConge { get; set; }
       public string  Mois { get; set; }
       public string Fonction { get; set; }
       public int Exercice { get; set; }
       public int Duree { get; set; }
       public bool SiProjet { get; set; }
    }
}
