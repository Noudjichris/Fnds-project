using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SGSP.AppCode
{
    public class Salaire
    {
        public double SalaireBase { get; set; }
        public double GrilleSalarialle { get; set; }
        public double Indice { get; set; }
        public string NumeroMatricule { get; set; }
        public int  IDSalaire { get; set; }
        public double Indemnites { get; set; }
        public double AutresPrimes { get; set; }
        public double PrimeMotivation { get; set; }
        public double PrimeTransport { get; set; }
        public double FraisCommunication { get; set; }
    }
}
