using System;

namespace SGSP.AppCode
{
   public  class Banque
   {
       public string NomEmploye { get; set; }
        public string NomBanque { get; set; }
        public string CodeBanque { get; set; }
        public string CodeGuichet { get; set; }
        public string IBAN { get; set; }
        public string Cle { get; set; }
        public int ID { get; set; }
        public string NumeroMatricule { get; set; }
        public string Compte { get; set; }
        public bool  EtatParDefaut { get; set; }
        public double NetAPayer { get; set; }
        public double SalaireNet { get; set; }
        public double FraisCommunication { get; set; }
        public double PrimeMotivation { get; set; }
        public double AutresPrimes { get; set; }
    }
}
