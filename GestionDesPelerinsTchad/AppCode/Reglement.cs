using System;

namespace SGSP.AppCode
{

    public class Reglement
    {
        public int NumeroReglement { get; set; }
        public DateTime DatePaiement { get; set; }
        public double MontantPaiement { get; set; }
        public int NumeroFacture { get; set; }
        public string ModeReglement { get; set; }
        public string NumeroBorderaux { get; set; }
        public string NomBanque { get; set; }
        public string Libelle { get; set; }
        public string Mois { get; set; }
        public int  Exercice { get; set; }
        public int idLibelle { get; set; }
        public string Code { get; set; }
        public int NumeroFournisseur { get; set; }
        public int State { get; set; }
    }
}
