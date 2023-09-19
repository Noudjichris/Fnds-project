using System;

namespace SGSP.AppCode
{
 public    class Projet
 {
     public int NumeroProjet { get; set; }
     public int IDRegion { get; set; }
     public int NumeroPartenaire { get; set; }
     public string Partenaire { get; set; }
     public string TypePartenaire { get; set; }
     public string Localisation { get; set; }
     public string NomProjet { get; set; }
     public string Description { get; set; }
     public string Region { get; set; }
     public DateTime DateDebut { get; set; }
     public DateTime DateFin { get; set; }
     public bool Etat { get; set; }
     public string Status { get; set; }
    }
}
