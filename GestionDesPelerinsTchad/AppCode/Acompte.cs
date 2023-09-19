using System;
namespace SGSP.AppCode
{
   public  class Acompte
    {
       public int NumeroAcompte { get; set; }
       public double MontantAcompte {get;set;}
       public double Deduction { get; set; }
       public double Rembourser{ get; set; }
       public DateTime DateAcompte { get; set; }
       public string  NumeroMatricule { get; set; }
       public string NomEmploye { get; set; }
       public string ModePaiement { get; set; }
    }
}
