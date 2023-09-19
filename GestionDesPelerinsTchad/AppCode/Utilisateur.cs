using System;

namespace SGSP.AppCode
{
    public class Utilisateur
    {

        public  int NumUtilisateur{get;set;}
        public string  NomUtilsateur{get;set;}
        public string MotdePasse{get;set;}
        public string TypeUtilisateur{get;set;}
        public string Matricule { get; set; }
        public string Nom { get; set; }
       public string Prenom { get; set; }
        public string Photo { get; set; }

        public Utilisateur(string nomUtilsateur, string motdePasse, string typeUtilisateur, string matricule)
        {
            NomUtilsateur = nomUtilsateur;
            MotdePasse = motdePasse;
            TypeUtilisateur = typeUtilisateur;
            Matricule = matricule;
        }

        public Utilisateur(int numUtil, string nomUtilsteur, string motPasse, string typeUtilisateur)
        {
            NumUtilisateur = numUtil;
            NomUtilsateur = nomUtilsteur;
            MotdePasse = motPasse;
            TypeUtilisateur = typeUtilisateur;

        }

        public Utilisateur(int numUtil, string nomUtilsteur, string motPasse, string typeUtilisateur, string mat, string nom, string prenom, string photo)
        {
            NomUtilsateur = nomUtilsteur;
            MotdePasse = motPasse;
            TypeUtilisateur = typeUtilisateur;
            Prenom = prenom;
            Nom = nom;
            Photo = photo;
            Matricule = mat;
            NumUtilisateur = numUtil;
        }
            
    }
}
