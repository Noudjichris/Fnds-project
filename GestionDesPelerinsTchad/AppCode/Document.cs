using System;

namespace SGSP.AppCode
{
    public class Document
    {
        public int NumeroDocument { get; set; }
        public string ReferenceDocument { get; set; }
        public DateTime EcheanceLivraison { get; set; }
        public DateTime DateEnregistrement { get; set; }
        public DateTime EcheancePaiement { get; set; }
        public double MontantHT { get; set; }
        public double TVA { get; set; }
        public double MontantTTC {get;set;}
        public int NumeroTiers {get;set;}
        public string TypeDocument {get;set;}
        public string Description { get; set; }
        public string MotCle { get; set; }
        public string RootPathDocument { get; set; }
        public string CategorieDocument { get; set; }
        public int NumeroType { get; set; }
        public int Exercice { get; set; }
        public string Matricule { get; set; }
        public int Approbation { get; set; }
        public bool  Payable { get; set; }
        public int IDTypeDocument { get; set; }
        public string ModalitePaiement { get; set; }

        public int NumeroFacture { get; set; }
        public string Designation { get; set; }
        public double Quantite { get; set; }
        public double PrixUnitaire { get; set; }
        public double PrixTotal { get; set; }
        public int NumeroRubrique { get; set; }
        public string SousRubrique { get; set; }

        public System.Drawing.Image ApprobationDeDocument(int app)
        {
            if (app == 1)
            {
               return  global::SGSP.Properties.Resources.ok;
            }
            else if (app == 2)
            {
                return global::SGSP.Properties.Resources.deny;
            }
            else if (app == 0)
            {
                return global::SGSP.Properties.Resources.pending;
            }
            else 
            {
                return null;
            }
        }

        public string ApprobationDeDocumentText(int app)
        {
            if (app == 1)
            {
                return "Approuvé";
            }
            else if (app == 2)
            {
                return "Rejeté";
            }
            else
            {
                return "En instance";
            }
        }
    }
}
