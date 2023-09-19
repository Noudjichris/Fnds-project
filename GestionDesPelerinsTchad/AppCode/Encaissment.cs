using System;

namespace SGSP.AppCode
{
    public class Encaissement
    {
        public int ID { get; set;}
        public DateTime Date { get; set; }
        public DateTime DateEncaissment { get; set; }
        public double  Avoir { get; set; }
        public double  Montant { get; set; }
        public string   Tiers { get; set; }
        public int Exercice { get; set; }
        public string  Mois { get; set; }
        public string  Code { get; set; }
        public string  Objet { get; set; }
        public string Caissier { get; set; }

    }
}
