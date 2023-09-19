using System;

namespace SGSP.AppCode
{
   public  class Stagiaire
    {
       public int IDStage { get; set; }
       public string NatureStage { get; set; }
       public DateTime DateDebut{get;set;}
       public DateTime DateFin { get; set; }
       public bool SiRenumere { get; set; }
       public bool SiDiplome { get; set; }
       public double Montant { get; set; }
       public String Status { get; set; }
       public string Faculte{get;set;}
       public string Direction{get;set;}
       public string Service{get;set;}
       public int IDStagiaire { get; set; }
       public string  Nom { get; set; }
       public string Prenom { get; set; }
       public string Adresse { get; set; }
       public string Telephone1 { get; set; }
       public string Telephone2 { get; set; }
       public string Email { get; set; }
       public DateTime DateNaissance { get; set; }
       public string LieuNaissance { get; set; }
       public string Matricule { get; set; }
       public string Sexe { get; set; }
       public int  Duree { get; set; }
       public string Diplome { get; set; }
       public string Universite { get; set; }
    }
}
