using System;

namespace SGSP.AppCode
{
    public class Etudiant
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public int NumeroEtudiant { get; set; }
        public string Matricule { get; set; }
        public DateTime DateNaissance { get; set; }
        public string LieuDeNaissance { get; set; }
        public string Telephone1 { get; set; }
        public string Telephone2 { get; set; }
        public string Adresse { get; set; }
        public string Email { get; set; }
        public string Nationalite { get; set; }
        public string NomUtilsateur { get; set; }
        public string MotDePasse { get; set; }
        public string Photo { get; set; }
        public string TypeUtilsateur { get; set; }
        public string Sexe { get; set; }
    }
}
