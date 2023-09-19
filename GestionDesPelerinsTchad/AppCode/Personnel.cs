using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGSP.AppCode
{
  public   class Personnel
    {
        public string Adresse { get; set; }
        public int Age { get; set; }
        public int IDPersonelProjet { get; set; }
        public DateTime DateNaissance { get; set; }
        public string Email { get; set; }
        public string LieuNaissance { get; set; }
        public string Nom { get; set; }
        public string  NumeroMatricule { get; set; }
        public string Photo { get; set; }
        public string Prenom { get; set; }
        public string Sexe { get; set; }
        public string Telephone1 { get; set; }
        public string Telephone2 { get; set; }
        public string SituationMatrimoniale { get; set; }
        public int NombreEnfant { get; set; }
        public string NumeroPiece { get; set; }
        public string TypePiece { get; set; }
    }
}
