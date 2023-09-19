using System;

namespace SGSP.AppCode
{
   public  class GlobalVariable
   {
       public static System.Globalization.CultureInfo elGR = System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR");
       //public static string rootPathPersonnel = @"\\192.168.11.80\Users\HP\Pictures\Sharded Folder\Dossier Personnel\";
       //public static string rootPathPersonnelTempo = @"\\192.168.11.80\Users\HP\Pictures\Sharded Folder\Dossier PersonnelTempo\";
       //public static string rootPathDocuments = @"\\192.168.11.80\Users\HP\Pictures\Sharded Folder\Dossiers des documents\";
       //public static string rootPathDemande = @"\\192.168.11.80\Users\HP\Pictures\Sharded Folder\Dossier Personnel\";

       public static string rootPathPersonnel = @"C:\Dossier Personnel\";
       public static string rootPathPersonnelTempo = @"C:\Dossier PersonnelTempo\";
       public static string rootPathDocuments = @"C:\Dossiers des documents\";
       public static string rootPathDemande = @"C:\Dossier Personnel\";

       public static string mailFrom ="noudjichris8721@outlook.fr";
       public static int emaiPort = 25;
       public static string smtpClient = "smtp.live.com";
       public static string credentialPaasword = "Asael@20211006";

       public static double IRPP(double salaireImposable,double salaireBrut,double totalCnps ,double totalConges)
       {
           try
           {
               double totalIRPP;
               if (totalConges > 0)
               {
                   salaireBrut = salaireBrut - totalConges+totalCnps/2;
               }
                  if (salaireImposable <= 800000)
                    {
                        totalIRPP = .0;
                    }
                    else if (salaireImposable > 800000 && salaireImposable <= 2500000)
                    {
                        totalIRPP = System.Math.Round((((salaireBrut - totalCnps) * 12 - 800000) * 10 / 100) / 12);
                        if (totalConges > 0)
                        {
                            totalIRPP = totalIRPP * 2;
                        }
                    }
                    else if (salaireImposable > 2500000 && salaireImposable < 7500000)
                    {
                        totalIRPP = System.Math.Round((((salaireBrut - totalCnps) * 12 -( 800000 + 1700000)) * 20 / 100));
                        totalIRPP += System.Math.Round(1700000.0 * 10 / 100);
                        totalIRPP = Math.Round(totalIRPP / 12);
                        if (totalConges > 0)
                        {
                            totalIRPP = totalIRPP * 2;
                        }
                    }
                    else 
                    {
                        totalIRPP = System.Math.Round((((salaireBrut - totalCnps) * 12 - (800000+1700000+4700000)) * 30 / 100));
                        totalIRPP += System.Math.Round(4700000.0* 20 / 100);
                        totalIRPP += System.Math.Round(1700000.0 * 10 / 100);
                        totalIRPP =Math.Round( totalIRPP / 12);
                        if (totalConges > 0)
                        {
                            totalIRPP = totalIRPP * 2;

                        }
                    }              

               return System.Math.Round(totalIRPP);
           }
           catch { return .0; }
       }

       public static double ONASA(double salaireImposable,  double totalConges)
       {
           if (salaireImposable > 800000)
           {
               if (totalConges > 0)
                   return 80.0;
               else return 
                   40.0;
           }
           else
               return .0;
       }

    }
}
