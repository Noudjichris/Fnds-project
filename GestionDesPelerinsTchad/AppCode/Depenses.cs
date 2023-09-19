using System;

namespace SGSP.AppCode
{
   public  class Depenses
    {
       public int IDLibelle { get; set; }
        public int IDCategorie { get; set; }
        public int Etat { get; set; }
        public string Categorie { get; set; }
       public string Libelle { get; set; }
       public int IDDepense { get; set; }
        public int ORDRE { get; set; }
        public DateTime Date { get; set; }
       public double Montant { get; set; }
       public string Beneficiaire { get; set; }
       public string NumeroFacture { get; set; }
        public string Code { get; set; }
        public string Mois { get; set; }
        public int Annee { get; set; }
    }
}
