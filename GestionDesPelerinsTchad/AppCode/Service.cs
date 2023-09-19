using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGSP.AppCode
{
   public  class Service
    {
        public string Anciennete { get; set; }
        public string Categorie { get; set; }
        public int  IDTypeContrat { get; set; }
        public bool EtatTaxe { get; set; }
        public bool SiCNRT { get; set; }
        public string TypeContrat { get; set; }
        public DateTime DateService { get; set; }
        public string Diplome { get; set; }
        public string Echelon { get; set; }
        public string  Status { get; set; }
        public string NoCNPS { get; set; }
        public string Grade { get; set; }
        public int IDDepartement { get; set; }
        public int IDDirection { get; set; }
        public int Ordre { get; set; }
        public string  Direction { get; set; }
        public string  NumeroMatricule { get; set; }
        public int NumeroService { get; set; }
        public string  Poste { get; set; }
        public string Departement { get; set; }
        public string Abreviation { get; set; }
        public DateTime DateDepart { get; set; }
        public string Division { get; set; }
        public int IDDivision { get; set; }
        public double SalaireBrut { get; set; }
        public double  Primes { get; set; }
        public int  IDProjet { get; set; }
        public int IDPersonelProjet { get; set; }
        public string Region { get; set; }
        public string Localite { get; set; }
    }
}
