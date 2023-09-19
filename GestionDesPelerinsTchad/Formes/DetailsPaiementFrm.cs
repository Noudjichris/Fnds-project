using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SGSP.AppCode;

namespace SGSP.Formes
{
    public partial class DetailsPaiementFrm : Form
    {
        public DetailsPaiementFrm()
        {
            InitializeComponent();
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.Silver, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.SteelBlue, Color.DodgerBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox3_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.DodgerBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.DodgerBlue, Color.DodgerBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void DetailsPaiementFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SlateBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.WhiteSmoke, Color.WhiteSmoke, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        static System.Globalization.CultureInfo elGR = System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR");
        public int numeroPaiement, exercice; public string moisEtat;
        private void DetailsPaiementFrm_Load(object sender, EventArgs e)
        {
            var paiement = ConnectionClass.ListeoOrdrePaiement(numeroPaiement);
            txtCntroleur.Text = paiement.Controleur;
            txtLiquidateur.Text = paiement.Liquidateur;
            label1.Text = "ETAT DE SALAIRE DU MOIS DE " + moisEtat.ToUpper();
            Column2.Width = dataGridView1.Width / 4-80;
            Column3.Width = dataGridView1.Width / 5-30;
            button5.Location = new Point(Width - 45, button5.Location.Y);
            button10.Location = new Point(Width - 90, button5.Location.Y);
            button2.Location = new Point(Width - 155, button5.Location.Y);
         
               MontantTotal();
               cmbTypeContrat.Items.Add("<<Trier par type contrat >>");
            foreach (var s in ConnectionClass.ListeContrat())
                cmbTypeContrat.Items.Add(s.TypeContrat);
            cmbTypeContrat.Items.Add("<<Tous les salariés>>");
            cmbTypeContrat.Text = "<<Trier par type contrat >>";
            cmbBank.Items.Add( "Toutes les banques");
            foreach (var  b in ConnectionClass.ListeBanques())
            {
                cmbBank.Items.Add(b.NomBanque);
            }
            cmbBank.Items.Add("Paiement en espèces");
            ListeDesEtatsPaiements();
        }

        void ListeDesEtatsPaiements()
        {
            try
            {
                var typeContrat = cmbTypeContrat.Text;
                if (typeContrat == "<<Tous les salariés>>" || typeContrat== "<<Trier par type contrat >>")
                {typeContrat ="";}
                dataGridView1.Rows.Clear();
                  var listeDep = ConnectionClass.ListeDirection();
                  double totalGain = 0.0 , totalSalaireBase = 0.0, totalGainSalarial = 0.0, totalPrimes = 0.0, totalIRPP = 0.0, totalONASA = 0.0, totalConge = 0.0,
                  totalChargePatronal = 0.0, totalDeduction = 0.0, totalDeductif = 0.0, totalCoutSalarial = 0.0,
                totalChargeMedic=0.0,  totalSalaireBrut = 0.0, totalAcompte = 0.0, totalCNPS = 0.0, totalSalaireNet = 0.0, totalAvanceSurSalaire = 0.0, totalCoutAbscence = 0.0;
                  var i = 1;
                  for (var k = 0; k < listeDep.Count; k++)
                  {
                      double sousTotalSalaireBase = 0.0, sousTotalGainSalarial = 0.0, sousTotalPrimes = 0.0, sousTotalIRPP = 0.0, sousTotalONASA = 0.0, sousTotalConge = 0.0,
                          sousTotalChargePatronal = 0.0, sousTotalDeduction = 0.0, sousTotalDeductif = 0.0, sousTotalCoutSalarial = 0.0,
                       sousTotalChargeMedic=0.0,  sousTotalSalaireBrut = 0.0,sousTotalGain=0.0, sousTotalAcompte = 0.0, sousTotalCNPS = 0.0, sousTotalSalaireNet = 0.0, sousTotalAvanceSurSalaire = 0.0, sousTotalCoutAbscence = 0.0;

                      var listeMatricule = ConnectionClass.ListeMatriculePaye(numeroPaiement, listeDep[k].Direction, typeContrat);
                      if (listeMatricule.Count() > 0)
                      {
                          dataGridView1.Rows.Add("", listeDep[k].Direction, "","",
                             "", "", "", "", "", "", "", "", "", "", "", "", "", "", "","","100","","");
                          dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightSteelBlue;
                          dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial Narrow", 12.25F, System.Drawing.FontStyle.Bold);
                  
                          foreach (var personnel in listeMatricule)
                          {
                              var p = ConnectionClass.PaiementParMatricule(numeroPaiement, personnel.NumeroMatricule);
                              var personnels = ConnectionClass.ListePersonnelParMatricule(personnel.NumeroMatricule);
                            var service = ConnectionClass.ListeServicePersonnel(personnel.NumeroMatricule);
                            var salaire = ConnectionClass.ListeSalaire(p.NumeroMatricule);
                           
                              p.SalaireBase = salaire[0].SalaireBase;
                              var gainSalariale =  p.SalaireBase + p.GainAnciennete;
                              var employe = personnels[0].Nom + " " + personnels[0].Prenom;
                              var qualification = service[0].Poste;
                              var chargeTotal = p.CNPS + p.IRPP + p.ONASA;
                               var deduction = p.AvanceSurSalaire + p.AcomptePaye;
                               var primes = p.Transport + p.FraisCommunication + p.AutresPrimes + p.Indemnites + p.PrimeMotivation;
                               var deductif = p.ChargeSoinFamille + p.AcomptePaye + p.AvanceSurSalaire + p.CNPS + p.ONASA + p.IRPP;

                            dataGridView1.Rows.Add((object)personnel.NumeroMatricule, employe, qualification,
                                  service[0].Categorie + "-" + service[0].Echelon ,
                                    string.Format(elGR, "{0:0,0}", p.SalaireBase),
                                      string.Format(elGR, "{0:0,0}", p.GainAnciennete),
                                    string.Format(elGR, "{0:0,0}", gainSalariale),
                                    string.Format(elGR, "{0:0,0}", p.CongeAnnuel),
                                    string.Format(elGR, "{0:0,0}", p.CoutAbsence),
                                    string.Format(elGR, "{0:0,0}", p.SalaireBrut),
                                    string.Format(elGR, "{0:0,0}", p.CNPS),
                                    string.Format(elGR, "{0:0,0}", p.IRPP),
                                    string.Format(elGR, "{0:0,0}", p.ONASA),
                                           string.Format(elGR, "{0:0,0}", p.ChargeSoinFamille),
                                    string.Format(elGR, "{0:0,0}", deduction),
                                      string.Format(elGR, "{0:0,0}", deductif),
                                    string.Format(elGR, "{0:0,0}", primes),
                                    string.Format(elGR, "{0:0,0}", p.SalaireNet),
                                    string.Format(elGR, "{0:0,0}", p.ChargePatronale),
                                    string.Format(elGR, "{0:0,0}", p.CoutDuSalarie),
                                    i, p.Banque,p.Compte
                                  );
                              i++;
                              totalChargePatronal += p.ChargePatronale;
                              totalSalaireBase += p.SalaireBase;
                              totalGainSalarial += gainSalariale;
                              totalPrimes += primes;
                              totalSalaireBrut += p.SalaireBrut;
                              totalCNPS += p.CNPS;
                              totalSalaireNet += p.SalaireNet;
                              totalIRPP += p.IRPP;
                              totalONASA += p.ONASA;
                              totalDeduction += deduction;
                              totalDeductif += deductif;
                              totalConge += p.CongeAnnuel;
                              totalCoutAbscence += p.CoutAbsence;
                              totalCoutSalarial += p.CoutDuSalarie;
                              totalGain += p.GainAnciennete;
                              totalChargeMedic += p.ChargeSoinFamille;

                              sousTotalChargeMedic += p.ChargeSoinFamille;
                              sousTotalGain += p.GainAnciennete;
                              sousTotalChargePatronal += p.ChargePatronale;
                              sousTotalSalaireBase += p.SalaireBase;
                              sousTotalGainSalarial += gainSalariale;
                              sousTotalPrimes += primes;
                              sousTotalSalaireBrut += p.SalaireBrut;
                              sousTotalCNPS += p.CNPS;
                              sousTotalSalaireNet += p.SalaireNet;
                              sousTotalIRPP += p.IRPP;
                              sousTotalONASA += p.ONASA;
                              sousTotalDeduction += deduction;
                              sousTotalDeductif += deductif;
                              sousTotalConge += p.CongeAnnuel;
                              sousTotalCoutAbscence += p.CoutAbsence;
                              sousTotalCoutSalarial += p.CoutDuSalarie;

                          }
                          dataGridView1.Rows.Add("", "TOTAL", "", "",
                                        String.Format(elGR, "{0:0,0}", sousTotalSalaireBase),
                                        String.Format(elGR, "{0:0,0}", sousTotalGain),
                                          String.Format(elGR, "{0:0,0}", sousTotalGainSalarial),
                                        String.Format(elGR, "{0:0,0}", sousTotalConge),
                                        String.Format(elGR, "{0:0,0}", sousTotalCoutAbscence),
                                        String.Format(elGR, "{0:0,0}", sousTotalSalaireBrut),
                                        String.Format(elGR, "{0:0,0}", sousTotalCNPS),
                                        String.Format(elGR, "{0:0,0}", sousTotalIRPP),
                                        String.Format(elGR, "{0:0,0}", sousTotalONASA),
                                        String.Format(elGR, "{0:0,0}", sousTotalChargeMedic),
                                        String.Format(elGR, "{0:0,0}", sousTotalDeduction),
                                          String.Format(elGR, "{0:0,0}", sousTotalDeductif),
                                        String.Format(elGR, "{0:0,0}", sousTotalPrimes),
                                        String.Format(elGR, "{0:0,0}", sousTotalSalaireNet),
                                        String.Format(elGR, "{0:0,0}", sousTotalChargePatronal),
                                        String.Format(elGR, "{0:0,0}", sousTotalCoutSalarial),"","",""
                                      );
                          dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightSkyBlue;
                          dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial Narrow", 12.2F, System.Drawing.FontStyle.Bold);

                      }

                  }

                  dataGridView1.Rows.Add("", "TOTAL GENERAL", "", "",
                                String.Format(elGR, "{0:0,0}", totalSalaireBase),
                                        String.Format(elGR, "{0:0,0}", totalGain),
                                String.Format(elGR, "{0:0,0}", totalGainSalarial),
                                String.Format(elGR, "{0:0,0}", totalConge),
                                String.Format(elGR, "{0:0,0}", totalCoutAbscence),
                                String.Format(elGR, "{0:0,0}", totalSalaireBrut),
                                String.Format(elGR, "{0:0,0}", totalCNPS),
                                String.Format(elGR, "{0:0,0}", totalIRPP),
                                String.Format(elGR, "{0:0,0}", totalONASA),
                                String.Format(elGR, "{0:0,0}", totalChargeMedic),
                                String.Format(elGR, "{0:0,0}", totalDeduction),
                                 String.Format(elGR, "{0:0,0}", totalDeductif),
                                String.Format(elGR, "{0:0,0}",totalPrimes),
                                String.Format(elGR, "{0:0,0}", totalSalaireNet),
                                String.Format(elGR, "{0:0,0}",totalChargePatronal),
                                String.Format(elGR, "{0:0,0}", totalCoutSalarial),"","",""
                              );
                  dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.SteelBlue;
                  dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.White;
                  dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial Narrow", 12.2F, System.Drawing.FontStyle.Bold);
                  MontantTotal();
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("", ex);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void cmbTypeContrat_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (comboBox1.Text == "Autres")
            {
                ListeDesEtatsPaiements();
                //ListeDesEtatsPaiementsParContrat();
            }
        }

        void MontantTotal()
        {
            try
            {
                var total = 0.0;
                var count = 0;
                for (var i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (!dataGridView1.Rows[i].Cells[1].Value.ToString().Contains("TOTAL") && ! string.IsNullOrWhiteSpace(dataGridView1.Rows[i].Cells[2].Value.ToString()))
                    {
                        total += double.Parse(dataGridView1.Rows[i].Cells[17].Value.ToString());
                        count++;
                    }
                }

                txtTotal.Text = "Nombre salarié traité  : " + count + "        Cout total : " + String.Format(elGR, "{0:0,0}", total) + "   ";
            }
            catch { }
        }
        void ListeDesEtatsPaiementsParContrat()
        {
            try
            {
                dataGridView1.Rows.Clear();
                var listeDep = ConnectionClass.ListeDepartement();
                double totalSalaireBase = 0.0, totalGainSalarial = 0.0, totalPrimes = 0.0, totalIRPP = 0.0, totalONASA = 0.0, totalConge = 0.0,
                totalChargePatronal = 0.0, totalDeduction = 0.0, totalDeductif = 0.0, totalCoutSalarial = 0.0,
            totalChargeMedical=0.0,   totalSalaireBrut = 0.0, totalAcompte = 0.0, totalCNPS = 0.0, totalSalaireNet = 0.0, totalAvanceSurSalaire = 0.0, totalCoutAbscence = 0.0;

                for (var k = 0; k < listeDep.Count; k++)
                {
                    var listeMatricule = ConnectionClass.ListeMatriculePaye(numeroPaiement, listeDep[k].Departement.ToString(), cmbTypeContrat.Text );
                    if (listeMatricule.Count() > 0)
                    {
                       
                        foreach (var personnel in listeMatricule)
                        {
                            var p = ConnectionClass.PaiementParMatricule(numeroPaiement, personnel.NumeroMatricule);
                            var personnels = ConnectionClass.ListePersonnelParMatricule(personnel.NumeroMatricule);
                            var service = ConnectionClass.ListeServicePersonnel(personnel.NumeroMatricule);
                            
                            
                            #region MyRegion
                            {
                            
                                //p.SalaireBase = Convert.ToDouble(dtSal.Rows[0].ItemArray[0].ToString());
                                var gainSalariale =p.SalaireBase;
                                var employe = personnels[0].Nom + " " + personnels[0].Prenom;
                                var qualification = service[0].Poste;
                                var chargeTotal = p.CNPS + p.IRPP + p.ONASA;
                                var deduction = p.AvanceSurSalaire + p.AcomptePaye ;
                                var primes = p.Transport + p.FraisCommunication + p.AutresPrimes + p.Indemnites + p.PrimeMotivation;
                                var deductif = p.AcomptePaye + p.ChargeSoinFamille + p.AvanceSurSalaire + p.CNPS + p.ONASA + p.IRPP;

                                dataGridView1.Rows.Add(personnel.NumeroMatricule, employe, qualification,
                                      service[0].Categorie + "-" + service[0].Echelon ,
                                      String.Format(elGR, "{0:0,0}", p.SalaireBase),
                                      String.Format(elGR, "{0:0,0}", 0),
                                      String.Format(elGR, "{0:0,0}", gainSalariale),
                                      String.Format(elGR, "{0:0,0}", p.CongeAnnuel),
                                      String.Format(elGR, "{0:0,0}", p.CoutAbsence),
                                      String.Format(elGR, "{0:0,0}", p.SalaireBrut),
                                      String.Format(elGR, "{0:0,0}", p.CNPS),
                                      String.Format(elGR, "{0:0,0}", p.IRPP),
                                      String.Format(elGR, "{0:0,0}", p.ONASA),
                                                 String.Format(elGR, "{0:0,0}", p.ChargeSoinFamille),
                                      String.Format(elGR, "{0:0,0}", deduction),
                                        String.Format(elGR, "{0:0,0}", deductif),
                                      String.Format(elGR, "{0:0,0}", primes),
                                      String.Format(elGR, "{0:0,0}", p.SalaireNet),
                                      String.Format(elGR, "{0:0,0}", p.ChargePatronale),
                                      String.Format(elGR, "{0:0,0}", p.CoutDuSalarie),"",p.Banque
                                    );

                                totalChargePatronal += p.ChargePatronale;
                                totalSalaireBase += p.SalaireBase;
                                totalGainSalarial += gainSalariale;
                                totalPrimes += primes;
                                totalSalaireBrut += p.SalaireBrut;
                                totalCNPS += p.CNPS;
                                totalSalaireNet += p.SalaireNet;
                                totalIRPP += p.IRPP;
                                totalONASA += p.ONASA;
                                totalDeduction += deduction;
                                totalDeductif += deductif;
                                totalConge += p.CongeAnnuel;
                                totalCoutAbscence += p.CoutAbsence;
                                totalCoutSalarial += p.CoutDuSalarie;
                                totalChargeMedical = +p.ChargeSoinFamille;
                            } 
                            #endregion
                        }
                    }

                }

                dataGridView1.Rows.Add("", "TOTAL", "", "",
                              String.Format(elGR, "{0:0,0}", totalSalaireBase),
                                     String.Format(elGR, "{0:0,0}", 0),
                              String.Format(elGR, "{0:0,0}", totalGainSalarial),
                              String.Format(elGR, "{0:0,0}", totalConge),
                              String.Format(elGR, "{0:0,0}", totalCoutAbscence),
                              String.Format(elGR, "{0:0,0}", totalSalaireBrut),
                              String.Format(elGR, "{0:0,0}", totalCNPS),
                              String.Format(elGR, "{0:0,0}", totalIRPP),
                              String.Format(elGR, "{0:0,0}", totalONASA),
            String.Format(elGR, "{0:0,0}", totalChargeMedical),
                              String.Format(elGR, "{0:0,0}", totalDeduction),
                                String.Format(elGR, "{0:0,0}", totalDeductif),
                              String.Format(elGR, "{0:0,0}", totalPrimes),
                              String.Format(elGR, "{0:0,0}", totalSalaireNet),
                              String.Format(elGR, "{0:0,0}", totalChargePatronal),
                              String.Format(elGR, "{0:0,0}", totalCoutSalarial),"",""
                            );
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.SteelBlue;
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.White;
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial Narrow", 12.2F, System.Drawing.FontStyle.Bold);
                MontantTotal();
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("", ex);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {

                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "PDF Documents (*.pdf)|*.pdf";

                    sharpPDF.pdfDocument document = new sharpPDF.pdfDocument("christian", "cdali");
                    var jour = DateTime.Now.Day;
                    var moiSs = DateTime.Now.Month;
                    var year = DateTime.Now.Year;
                    var hour = DateTime.Now.Hour;
                    var min = DateTime.Now.Minute;
                    var sec = DateTime.Now.Second;
                    var datTe = jour.ToString() + "_" + moiSs.ToString() + "_" + year.ToString() + "_" + hour + "_" + min + "_" + sec;
                    //sfd.FileName = label1.Text.Replace("/", "_") + "_imprimé_le_" + datTe + ".pdf";

                    string pathFile = @"C:\Dossier Personnel\Ordre Virement\";
                    if (!System.IO.Directory.Exists(pathFile))
                    {
                        System.IO.Directory.CreateDirectory(pathFile);
                    }
                    sfd.FileName = @"C:\Dossier Personnel\Ordre Virement\" + label1.Text.Replace("/", "_") + "_imprimé_le_" + datTe + ".pdf"; ;
                    if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        //if (comboBox1.Text == "Autres")
                        //{
                        //    var count = dataGridView1.Rows.Count;
                        //    var div = (count) / 14;
                        //    for (var i = 0; i <= div; i++)
                        //    {
                        //        if (i * 14 < count)
                        //        {
                        //            var bitmap = AppCode.Impression.ImprimerOrdreDePaiementPourJournalierEtStagiaire(numeroPaiement, dataGridView1, cmbTypeContrat.Text, exercice, moisEtat, i);
                        //            string inputImage1 = @"cdali" + i;
                        //            // Create an empty page
                        //            sharpPDF.pdfPage page = document.addPage(500, 700);
                                    
                        //            document.addImageReference(bitmap, inputImage1);
                        //            sharpPDF.Elements.pdfImageReference img = document.getImageReference(inputImage1);
                        //            page.addImage(img, 10, 0, page.height, page.width);
                        //        }

                        //    }
                        //}
                        //else if (comboBox1.Text == "Contractuel" || comboBox1.Text=="<Trier par>")
                        {  
                            var bitmap = AppCode.Impression.ImprimerOrdreDePaiement(numeroPaiement, dataGridView1, exercice, moisEtat);
                                       
                            var inputImage = @"cdali" ;
                            // Create an empty page
                            sharpPDF.pdfPage pageIndex = document.addPage(500,700);
                            document.addImageReference(bitmap, inputImage);
                            sharpPDF.Elements.pdfImageReference img1 = document.getImageReference(inputImage);
                            pageIndex.addImage(img1, 0, 0, pageIndex.height, pageIndex.width);
                            if (dataGridView1.Rows.Count > 15)
                            {
                                var div = (dataGridView1.Rows.Count - 15) / 27;

                                for (var i = 0; i <= div; i++)
                                {
                                    if (i * 27 < dataGridView1.Rows.Count)
                                    {
                                        bitmap = AppCode.Impression.ImprimerOrdreDePaiement(numeroPaiement, dataGridView1, exercice, moisEtat, i);
                                        inputImage = @"cdali" + i;
                                        // Create an empty page
                                        pageIndex = document.addPage(500, 700);

                                        document.addImageReference(bitmap, inputImage);
                                        img1 = document.getImageReference(inputImage);
                                        pageIndex.addImage(img1, 0, 0, pageIndex.height, pageIndex.width);
                                       
                                    }
                                }
                                var diff = (div + 1) * 27 + 15;
                                if (diff - dataGridView1.Rows.Count < 8)
                                {
                                    var paiement = ConnectionClass.ListeoOrdrePaiement(numeroPaiement);
                                    var montantTotal = .0;
                                    foreach (var paie in ConnectionClass.ListeDetailsPaiement(numeroPaiement))
                                        montantTotal += paie.SalaireNet + paie.IRPP + paie.ONASA;
                                    var text = "Arrêté le présent état de paiement à la somme de : " + Impression.Converti((int)montantTotal) + "(NEP+IRPP+FIR =  " + String.Format(elGR, "{0:0,0}", montantTotal) + ") Franc CFA";
          
                                    bitmap = AppCode.Impression.ImprimerOrdreDePaiement(numeroPaiement,text);
                                    inputImage = @"cdali0012";
                                    // Create an empty page
                                    pageIndex = document.addPage(500, 700);

                                    document.addImageReference(bitmap, inputImage);
                                    img1 = document.getImageReference(inputImage);
                                    pageIndex.addImage(img1, 0, 0, pageIndex.height, pageIndex.width);
                                }
                            }
                        }
                        document.createPDF(sfd.FileName);
                        System.Diagnostics.Process.Start(sfd.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("Imprimer paiement", ex);
            }
        }
        private void ToCsV(DataGridView dGV, string filename)
        {
            try
            {
                string stOutput = "";
                // Export titles:
                string sHeaders = "";

                for (int j = 0; j < dGV.Columns.Count ; j++)
                    sHeaders = sHeaders.ToString() + Convert.ToString(dGV.Columns[j].HeaderText) + "\t";
                stOutput += sHeaders + "\r\n";
                // Export data.
                for (int i = 0; i < dGV.RowCount; i++)
                {
                    string stLine = "";
                    for (int j = 0; j < dGV.Rows[i].Cells.Count ; j++)
                        stLine = stLine.ToString() + Convert.ToString(dGV.Rows[i].Cells[j].Value) + "\t";
                    stOutput += stLine + "\r\n";
                }
                Encoding utf16 = Encoding.GetEncoding(1254);
                byte[] output = utf16.GetBytes(stOutput);
                System.IO.FileStream fs = new System.IO.FileStream(filename, System.IO.FileMode.Create);
                System.IO.BinaryWriter bw = new System.IO.BinaryWriter(fs);
                bw.Write(output, 0, output.Length); //write the encoded file
                bw.Flush();
                bw.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("Erreur exportation", ex);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    var lstPaiement = new List<Paiement>();
                    var montant = 0.0; var montantCNPS = 0.0;
                    for (var p = 0; p < dataGridView1.Rows.Count; p++)
                    {
                        if (!string.IsNullOrWhiteSpace(dataGridView1.Rows[p].Cells[0].Value.ToString()))
                        {
                            if (double.Parse(dataGridView1.Rows[p].Cells[10].Value.ToString()) +
                                double.Parse(dataGridView1.Rows[p].Cells[18].Value.ToString()) > 0)
                            {
                                var paiement = new Paiement();
                                paiement.SalaireBrut = double.Parse(dataGridView1.Rows[p].Cells[9].Value.ToString());
                                if (paiement.SalaireBrut > 500000)
                                    paiement.SalaireBrut = 500000.0;
                                paiement.CNPS = double.Parse(dataGridView1.Rows[p].Cells[10].Value.ToString()) +
                                    double.Parse(dataGridView1.Rows[p].Cells[18].Value.ToString());
                                paiement.Employe = dataGridView1.Rows[p].Cells[1].Value.ToString();
                                paiement.NumeroMatricule = dataGridView1.Rows[p].Cells[0].Value.ToString();
                                lstPaiement.Add(paiement);
                                montantCNPS += paiement.CNPS;
                            }
                        }
                    }
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "PDF Documents (*.pdf)|*.pdf";

                    sharpPDF.pdfDocument document = new sharpPDF.pdfDocument("christian", "cdali");
                    var jour = DateTime.Now.Day;
                    var moiSs = DateTime.Now.Month;
                    var year = DateTime.Now.Year;
                    var hour = DateTime.Now.Hour;
                    var min = DateTime.Now.Minute;
                    var sec = DateTime.Now.Second;
                    var datTe = jour.ToString() + "_" + moiSs.ToString() + "_" + year.ToString() + "_" + hour + "_" + min + "_" + sec;
                    sfd.FileName = "CNPS du mois " +moisEtat +"_imprimé_le_" + datTe + ".pdf";
                    var l = from li in lstPaiement
                            orderby li.Employe
                            select li;
                    var listePaiement = new List<Paiement>();
                    
                    foreach (var li in lstPaiement)
                    {
                        var p = new Paiement();
                        p.CNPS = li.CNPS;
                        p.SalaireBrut = li.SalaireBrut;
                        p.Employe = li.Employe;
                        p.NumeroMatricule = li.NumeroMatricule;
                        listePaiement.Add(p);
                    }
                    var pp = new Paiement();
                    pp.Employe = "TOTAL";
                    pp.CNPS = montantCNPS;
                    listePaiement.Add(pp);
                    //string pathFile = "";
                    if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        //if (comboBox1.Text != "Autres")
                        {
                            var count = listePaiement.Count;
                            var div = (count) / 40;
                            for (var i = 0; i <= div; i++)
                            {
                                if (i * 40 < count)
                                {
                                    var bitmap = AppCode.Impression.ImprimerListeDesCNPS(listePaiement,montant,montantCNPS, moisEtat, exercice, i);
                                    string inputImage1 = @"cdali" + i;
                                    // Create an empty page
                                    sharpPDF.pdfPage page = document.addPage();

                                    document.addImageReference(bitmap, inputImage1);
                                    sharpPDF.Elements.pdfImageReference img = document.getImageReference(inputImage1);
                                    page.addImage(img, 10, 0, page.height, page.width);
                                }
                            }
                        }
                        document.createPDF(sfd.FileName);
                        System.Diagnostics.Process.Start(sfd.FileName);
                    }
                }
            }
            catch { }
        }

        private void toolStripMenuItem13_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel Documents (*.xls)|*.xls";
                var jour = DateTime.Now.Day;
                var mois = DateTime.Now.Month;
                var year = DateTime.Now.Year;
                var hour = DateTime.Now.Hour;
                var min = DateTime.Now.Minute;
                var sec = DateTime.Now.Second;
                var date = jour.ToString() + "_" + mois.ToString() + "_" + year.ToString() + "_" + hour + "_" + min + "_" + sec;
                sfd.FileName = "Paiement_Impriméé_le_" + date + ".xls";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    ToCsV(dataGridView1, sfd.FileName);
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("", ex);
            }
        }

        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    var lstPaiement = new List<Paiement>();
                    var montant = 0.0; var montantIRPP = 0.0;
                    var totalONASA = 0.0;
                    for (var p = 0; p < dataGridView1.Rows.Count; p++)
                    {
                        if (!string.IsNullOrWhiteSpace(dataGridView1.Rows[p].Cells[0].Value.ToString()))
                        {
                            if (double.Parse(dataGridView1.Rows[p].Cells[11].Value.ToString()) > 0)
                            {
                                var paiement = new Paiement();
                                paiement.SalaireBrut = double.Parse(dataGridView1.Rows[p].Cells[9].Value.ToString());
                                paiement.ONASA = double.Parse(dataGridView1.Rows[p].Cells[12].Value.ToString());
                                paiement.IRPP = double.Parse(dataGridView1.Rows[p].Cells[11].Value.ToString());
                                paiement.Employe = dataGridView1.Rows[p].Cells[1].Value.ToString();
                                paiement.NumeroMatricule = dataGridView1.Rows[p].Cells[0].Value.ToString();
                                lstPaiement.Add(paiement);
                                montant += paiement.IRPP;
                                totalONASA += paiement.ONASA;
                            }
                        }
                    }
                        var l = from li in lstPaiement
                                orderby li.Employe
                                select li;
                        var listePaiement = new List<Paiement>();

                        foreach (var li in l)
                        {
                            var p = new Paiement();
                            p.IRPP = li.IRPP;
                            p.GainSalarial = li.GainSalarial;
                            p.ONASA = li.ONASA;
                            p.SalaireBrut = li.SalaireBrut;
                            p.Employe = li.Employe;
                            p.NumeroMatricule = li.NumeroMatricule;
                            listePaiement.Add(p);
                        }

                           SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "Excel Documents (*.xls)|*.xls";

                        var jour = DateTime.Now.Day;
                        var moiSs = DateTime.Now.Month;
                        var year = DateTime.Now.Year;
                        var hour = DateTime.Now.Hour;
                        var min = DateTime.Now.Minute;
                        var sec = DateTime.Now.Second;
                        var datTe = jour.ToString() + "_" + moiSs.ToString() + "_" + year.ToString() + "_" + hour + "_" + min + "_" + sec;
                        sfd.FileName = "IRPP du mois " + moisEtat + "_imprimé_le_" + datTe + ".xls";
                       
                        var dgvView = new DataGridView();
                        dgvView.Columns.Add("no", "N°");
                        dgvView.Columns.Add("nom", "NOM & PRENOM");
                        dgvView.Columns.Add("nomt", "MONTANT");
                        dgvView.Columns.Add("nocnp", "IRPP");
                        dgvView.Columns.Add("noas", "FIR");
                        dgvView.Columns.Add("noast", "TOTAL");

                        var k = 1;
                        foreach (var pai in lstPaiement)
                        {
                            dgvView.Rows.Add(k, pai.Employe.ToUpper(), pai.SalaireBrut, pai.IRPP, pai.ONASA, pai.ONASA + pai.IRPP);
                            k++;
                        }
                        dgvView.Rows.Add("", "Total", "", montant, totalONASA, montant + totalONASA);
                        if (sfd.ShowDialog() == DialogResult.OK)
                        {

                            string stOutput = "";
                            // Export titles:
                            string sHeaders = "";

                            for (int j = 0; j < dgvView.Columns.Count; j++)
                                sHeaders = sHeaders.ToString() + Convert.ToString(dgvView.Columns[j].HeaderText) + "\t";
                            stOutput += sHeaders + "\r\n";
                            // Export data.
                            for (int i = 0; i < dgvView.RowCount; i++)
                            {
                                string stLine = "";
                                for (int j = 0; j < dgvView.Rows[i].Cells.Count; j++)
                                    stLine = stLine.ToString() + Convert.ToString(dgvView.Rows[i].Cells[j].Value) + "\t";
                                stOutput += stLine + "\r\n";
                            }
                            Encoding utf16 = Encoding.GetEncoding(1254);
                            byte[] output = utf16.GetBytes(stOutput);
                            System.IO.FileStream fs = new System.IO.FileStream(sfd.FileName, System.IO.FileMode.Create);
                            System.IO.BinaryWriter bw = new System.IO.BinaryWriter(fs);
                            bw.Write(output, 0, output.Length); //write the encoded file
                            bw.Flush();
                            bw.Close();
                            fs.Close();
                            System.Diagnostics.Process.Start(sfd.FileName);
                        }
                    }
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("", ex);
            }
        }

        private void entréeStcokToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var lstPaiement = new List<Paiement>();
                var montant = 0.0; var montantCNPS = 0.0;
                for (var p = 0; p < dataGridView1.Rows.Count; p++)
                {
                    if (!string.IsNullOrWhiteSpace(dataGridView1.Rows[p].Cells[0].Value.ToString()))
                    {
                        var paiement = new Paiement();
                        paiement.SalaireBrut = double.Parse(dataGridView1.Rows[p].Cells[9].Value.ToString());
                        if (paiement.SalaireBrut > 500000)
                            paiement.SalaireBrut = 500000.0;
                        paiement.CNPS = double.Parse(dataGridView1.Rows[p].Cells[10].Value.ToString()) +
                            double.Parse(dataGridView1.Rows[p].Cells[18].Value.ToString());
                        paiement.ONASA = Math.Round(paiement.CNPS * 100 / paiement.SalaireBrut, 1);
                        paiement.Employe = dataGridView1.Rows[p].Cells[1].Value.ToString().ToUpper();
                        paiement.NumeroMatricule = dataGridView1.Rows[p].Cells[0].Value.ToString();
                        lstPaiement.Add(paiement);

                        montantCNPS += paiement.CNPS;
                        montant += paiement.SalaireBrut;
                    }
                }
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel Documents (*.xls)|*.xls";
                var jour = DateTime.Now.Day;
                var moiSs = DateTime.Now.Month;
                var year = DateTime.Now.Year;
                var hour = DateTime.Now.Hour;
                var min = DateTime.Now.Minute;
                var sec = DateTime.Now.Second;
                var datTe = jour.ToString() + "_" + moiSs.ToString() + "_" + year.ToString() + "_" + hour + "_" + min + "_" + sec;
                sfd.FileName = "CNPS du mois " + moisEtat + "_imprimé_le_" + datTe + ".xls";
                var l = from li in lstPaiement
                        orderby li.Employe
                        select li;
                var listePaiement = new List<Paiement>();

                foreach (var li in l)
                {
                    var p = new Paiement();
                    p.CNPS = li.CNPS;
                    p.SalaireBrut = li.SalaireBrut;
                    p.ONASA = li.ONASA;
                    p.Employe = li.Employe;
                    p.NumeroMatricule = li.NumeroMatricule;
                    listePaiement.Add(p);
                }
                var dgvView = new DataGridView();
                dgvView.Columns.Add("no","N°");
                dgvView.Columns.Add("nom", "Nom & Prenom".ToUpper());
                dgvView.Columns.Add("nomt", "Montant".ToUpper());
                dgvView.Columns.Add("nonta", "Taux".ToUpper());
                dgvView.Columns.Add("nocnp", "CNPS à payer".ToUpper());
                dgvView.Columns.Add("noas", "Assurance".ToUpper());
                var k= 1;
                foreach (var pai in listePaiement)
                {
                    var service = ConnectionClass.ListeServicePersonnel(pai.NumeroMatricule);
                    var cnps = "";
                    if (service.Count > 0)
                    {
                        cnps = service[0].NoCNPS;
                    }
                    dgvView.Rows.Add(k,pai.Employe,pai.SalaireBrut,pai.ONASA+"%", pai.CNPS,cnps );
                    k++;
                }
                dgvView.Rows.Add("","Total", montant, "",montantCNPS, "");
                if (sfd.ShowDialog() == DialogResult.OK)
                {

                    string stOutput = "";
                    // Export titles:
                    string sHeaders = "";

                    for (int j = 0; j < dgvView.Columns.Count; j++)
                        sHeaders = sHeaders.ToString() + Convert.ToString(dgvView.Columns[j].HeaderText) + "\t";
                    stOutput += sHeaders + "\r\n";
                    // Export data.
                    for (int i = 0; i < dgvView.RowCount; i++)
                    {
                        string stLine = "";
                        for (int j = 0; j < dgvView.Rows[i].Cells.Count; j++)
                            stLine = stLine.ToString() + Convert.ToString(dgvView.Rows[i].Cells[j].Value) + "\t";
                        stOutput += stLine + "\r\n";
                    }
                    Encoding utf16 = Encoding.GetEncoding(1254);
                    byte[] output = utf16.GetBytes(stOutput);
                    System.IO.FileStream fs = new System.IO.FileStream(sfd.FileName, System.IO.FileMode.Create);
                    System.IO.BinaryWriter bw = new System.IO.BinaryWriter(fs);
                    bw.Write(output, 0, output.Length); //write the encoded file
                    bw.Flush();
                    bw.Close();
                    fs.Close();
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("", ex);
            }
        }

        private void button10_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                button10.ContextMenuStrip = ctxInventaire ;
                button10.ContextMenuStrip.Show(button10, e.Location);
            }
        }

        private void btnImprimer_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                button2.ContextMenuStrip = contextMenuStrip1;
                button2.ContextMenuStrip.Show(button2, e.Location);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    var lstPaiement = new List<Paiement>();
                    var montant = 0.0; var totalFIR=0.0;
                    for (var p = 0; p < dataGridView1.Rows.Count; p++)
                    {
                        if (!string.IsNullOrWhiteSpace(dataGridView1.Rows[p].Cells[0].Value.ToString()))
                        {
                            if (double.Parse(dataGridView1.Rows[p].Cells[11].Value.ToString()) > 0)
                            {
                                var paiement = new Paiement();
                                paiement.SalaireBrut = double.Parse(dataGridView1.Rows[p].Cells[9].Value.ToString());
                                paiement.ONASA = double.Parse(dataGridView1.Rows[p].Cells[12].Value.ToString());
                                paiement.IRPP = double.Parse(dataGridView1.Rows[p].Cells[11].Value.ToString());
                                paiement.Employe = dataGridView1.Rows[p].Cells[1].Value.ToString();
                                paiement.NumeroMatricule = dataGridView1.Rows[p].Cells[0].Value.ToString();
                                lstPaiement.Add(paiement);
                                montant += paiement.GainSalarial;
                                totalFIR += paiement.ONASA;
                            }
                        }
                    }
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "PDF Documents (*.pdf)|*.pdf";

                    sharpPDF.pdfDocument document = new sharpPDF.pdfDocument("christian", "cdali");
                    var jour = DateTime.Now.Day;
                    var moiSs = DateTime.Now.Month;
                    var year = DateTime.Now.Year;
                    var hour = DateTime.Now.Hour;
                    var min = DateTime.Now.Minute;
                    var sec = DateTime.Now.Second;
                    var datTe = jour.ToString() + "_" + moiSs.ToString() + "_" + year.ToString() + "_" + hour + "_" + min + "_" + sec;
                    sfd.FileName = "IRPP du mois " + moisEtat + "_imprimé_le_" + datTe + ".pdf";
                    var l = from li in lstPaiement
                            orderby li.Employe
                            select li;
                    var listePaiement = new List<Paiement>();

                    foreach (var li in l)
                    {
                        var p = new Paiement();
                        p.IRPP = li.IRPP;
                        p.GainSalarial = li.GainSalarial;
                        p.ONASA = li.ONASA;
                        p.SalaireBrut = li.SalaireBrut;
                        p.Employe = li.Employe;
                        p.NumeroMatricule = li.NumeroMatricule;
                        listePaiement.Add(p);
                    }

                    if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                      
                            var count = listePaiement.Count;
                            var div = (count) / 48;
                            for (var i = 0; i <= div; i++)
                            {
                                if (i * 48 < count)
                                {
                                    var bitmap = AppCode.Impression.ImprimerListeDesIRPPFIR(listePaiement, montant, totalFIR, moisEtat, exercice, i);
                                    string inputImage1 = @"cdali" + i;
                                    // Create an empty page
                                    sharpPDF.pdfPage page = document.addPage();

                                    document.addImageReference(bitmap, inputImage1);
                                    sharpPDF.Elements.pdfImageReference img = document.getImageReference(inputImage1);
                                    page.addImage(img, 10, 0, page.height, page.width);
                                }
                            }
                        }
                        document.createPDF(sfd.FileName);
                        System.Diagnostics.Process.Start(sfd.FileName);
                    }
                
            }
            catch { }
        }

        private void txtLiquidateur_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtLiquidateur.Text))
                {
                    var p = new Paiement();
                    p.Liquidateur = txtLiquidateur.Text;
                    p.IDPaie = numeroPaiement;
                    ConnectionClass.SignateursLiquidateurDePaiement(p);
                }
            }
            catch { }
        }

        private void txtCntroleur_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCntroleur.Text))
                {
                    var p = new Paiement();
                    p.Controleur = txtCntroleur.Text;
                    p.IDPaie = numeroPaiement;
                    ConnectionClass.SignateursControleurDePaiement(p);
                }
            }
            catch { }
        }

        private void txtLiquidateur_TextChanged(object sender, EventArgs e)
        {
            txtCntroleur_MouseLeave(null, null);
        }

        private void txtCntroleur_TextChanged(object sender, EventArgs e)
        {
            txtCntroleur_MouseLeave(null, null);
        }


        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel Documents (*.xls)|*.xls";

                var jour = DateTime.Now.Day;
                var moiSs = DateTime.Now.Month;
                var year = DateTime.Now.Year;
                var hour = DateTime.Now.Hour;
                var min = DateTime.Now.Minute;
                var sec = DateTime.Now.Second;
                var datTe = jour.ToString() + "_" + moiSs.ToString() + "_" + year.ToString() + "_" + hour + "_" + min + "_" + sec;
                sfd.FileName = "Virement bancaire  du mois " + moisEtat + "_imprimé_le_" + datTe + ".xls";
                var liste = new List<Banque>();
                var total = .0;
             
                
                    foreach (DataGridViewRow dgRows in dataGridView1.Rows)
                    {
                        if (!string.IsNullOrEmpty( dgRows.Cells[22].Value.ToString()))
                        {
                            var banks = from b in ConnectionClass.ListeDonneesBancaires(dgRows.Cells[0].Value.ToString())
                                        where b.Compte == dgRows.Cells[22].Value.ToString()
                                        select b;
                            var bank = new Banque();
                            bank.NomEmploye = dgRows.Cells[1].Value.ToString();
                            bank.NumeroMatricule = dgRows.Cells[0].Value.ToString();
                            bank.Compte = dgRows.Cells[22].Value.ToString();
                            bank.NomBanque = dgRows.Cells[21].Value.ToString();
                        
                                          var date = Impression.ObtenirMois(moisEtat).ToString() + exercice.ToString().Substring(2);
                        if(Impression.ObtenirMois(moisEtat)<10)
                            date = "0" + Impression.ObtenirMois(moisEtat).ToString() + exercice.ToString().Substring(2);
                        bank.IBAN = date;
                            foreach (var b in banks)
                            {
                                bank.CodeBanque = b.CodeBanque;
                                bank.CodeGuichet = b.CodeGuichet;
                                bank.Cle = b.Cle;
                                bank.EtatParDefaut = ConnectionClass.ListeBanques(bank.NomBanque)[0].EtatParDefaut;
                            }
                            bank.NetAPayer = double.Parse(dgRows.Cells[17].Value.ToString());
                            liste.Add(bank);
                        }
                    }
                
                var l = from lp in liste
                        orderby lp.NomBanque
                        //orderby lp.NomEmploye
                        select lp;
                var listeB1 = new List<Banque>();
                foreach (var p in l)
                {
                    listeB1.Add(p);
                }

                var dgvView1 = new DataGridView();
                dgvView1.Columns.Add("no", "Banque");
                dgvView1.Columns.Add("nom", "Compte");
                dgvView1.Columns.Add("noas", "Clé");
                dgvView1.Columns.Add("nomt", "Matricule");    
                dgvView1.Columns.Add("nomt", "Nom & Prenom");               
                dgvView1.Columns.Add("noas", "Total");
                dgvView1.Columns.Add("noas", "Date");

                var dgvView2 = new DataGridView();
                dgvView2.Columns.Add("no", "Banque");
                dgvView2.Columns.Add("nom", "Compte");
                dgvView2.Columns.Add("noas", "Clé");
                dgvView2.Columns.Add("nomt", "Matricule");
                dgvView2.Columns.Add("nomt", "Nom & Prenom");
                dgvView2.Columns.Add("noas", "Total");
                dgvView2.Columns.Add("noas", "Date");
                var f = 1;
                foreach (var lp in listeB1)
                {
                    if (lp.EtatParDefaut)
                    {
                        var numeroCompte = lp.Compte;
                        dgvView1.Rows.Add(lp.NomBanque, numeroCompte, lp.Cle, lp.NumeroMatricule, lp.NomEmploye.ToUpper(), lp.NetAPayer, lp.IBAN);
                    }
                    else
                    {
                        var numeroCompte = lp.CodeBanque + "-" + lp.CodeGuichet + "-" + lp.Compte;
                        dgvView2.Rows.Add(lp.NomBanque,numeroCompte,lp.Cle,lp.NumeroMatricule, lp.NomEmploye.ToUpper(),  lp.NetAPayer, lp.IBAN);
                        f++;
                    }
                  
                }
                if (sfd.ShowDialog() == DialogResult.OK)
                {

                    string stOutput = "";
                    // Export titles:
                    string sHeaders = "";

                    for (int j = 0; j < dgvView1.Columns.Count; j++)
                        sHeaders = sHeaders.ToString() + Convert.ToString(dgvView1.Columns[j].HeaderText) + "\t";
                    stOutput += sHeaders + "\r\n";
                    // Export data.
                    for (int i = 0; i < dgvView1.RowCount; i++)
                    {
                        string stLine1 = "";
                        for (int j = 0; j < dgvView1.Rows[i].Cells.Count; j++)
                            stLine1 = stLine1.ToString() + Convert.ToString(dgvView1.Rows[i].Cells[j].Value) + "\t";
                        stOutput += stLine1 + "\r\n";
                    }


                    stOutput += "\r\n";
                    //for (int j = 0; j < dgvView2.Columns.Count; j++)
                        //sHeaders = sHeaders.ToString() + Convert.ToString(dgvView2.Columns[j].HeaderText) + "\t";
                    stOutput += sHeaders + "\r\n";
                    // Export data.
                    for (int i = 0; i < dgvView2.RowCount; i++)
                    {
                        string stLine = "";
                        for (int j = 0; j < dgvView2.Rows[i].Cells.Count; j++)
                            stLine = stLine.ToString() + Convert.ToString(dgvView2.Rows[i].Cells[j].Value) + "\t";
                        stOutput += stLine + "\r\n";
                    }

                    Encoding utf16 = Encoding.GetEncoding(1254);
                    byte[] output = utf16.GetBytes(stOutput);
                    System.IO.FileStream fs = new System.IO.FileStream(sfd.FileName, System.IO.FileMode.Create);
                    System.IO.BinaryWriter bw = new System.IO.BinaryWriter(fs);
                    bw.Write(output, 0, output.Length); //write the encoded file
                    bw.Flush();
                    bw.Close();
                    fs.Close();
                     System.Diagnostics.Process.Start(sfd.FileName);
                }
            }
            catch (Exception ex)
            { }
        }

        private void contractuelINSEEDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var liste = new List<Banque>();
                var total = .0;
                var totalFraisCom = .0;
                var totalPrimeMotivation = .0;
                if (cmbBank.Text == "Toutes les banques")
                {
                    foreach (DataGridViewRow dgRows in dataGridView1.Rows)
                    {
                        if (!string.IsNullOrWhiteSpace(dgRows.Cells[21].Value.ToString().ToUpper()))
                        {
                            if (double.Parse(dgRows.Cells[9].Value.ToString()) > 0)
                            {
                                var banks = from b in ConnectionClass.ListeDonneesBancaires(dgRows.Cells[0].Value.ToString())
                                            where b.Compte == dgRows.Cells[22].Value.ToString()
                                            select b;
                                var bank = new Banque();
                                bank.NomEmploye = dgRows.Cells[1].Value.ToString();
                                bank.NumeroMatricule = dgRows.Cells[0].Value.ToString();
                                bank.Compte = dgRows.Cells[22].Value.ToString();
                                bank.NomBanque = dgRows.Cells[21].Value.ToString();
                                foreach (var b in banks)
                                {
                                    bank.CodeBanque = b.CodeBanque;
                                    bank.CodeGuichet = b.CodeGuichet;
                                    bank.Cle = b.Cle;
                                }
                                bank.NetAPayer = double.Parse(dgRows.Cells[17].Value.ToString());
                                bank.FraisCommunication = ConnectionClass.PaiementParMatricule(numeroPaiement, dgRows.Cells[0].Value.ToString()).FraisCommunication;
                                bank.PrimeMotivation = ConnectionClass.PaiementParMatricule(numeroPaiement, dgRows.Cells[0].Value.ToString()).PrimeMotivation;
                                liste.Add(bank);
                                total += double.Parse(dgRows.Cells[17].Value.ToString());
                                totalFraisCom += ConnectionClass.PaiementParMatricule(numeroPaiement, dgRows.Cells[0].Value.ToString()).FraisCommunication;
                                totalPrimeMotivation += ConnectionClass.PaiementParMatricule(numeroPaiement, dgRows.Cells[0].Value.ToString()).PrimeMotivation;
                            }
                        }
                    }
                }
                else if (cmbBank.Text == "Paiement en espèces")
                {
                }
                else
                {
                    foreach (DataGridViewRow dgRows in dataGridView1.Rows)
                    {
                        if (dgRows.Cells[21].Value.ToString().ToUpper() == cmbBank.Text.ToUpper())
                        {
                        if (double.Parse(dgRows.Cells[9].Value.ToString()) >0)
                        {
                            var banks = from b in ConnectionClass.ListeDonneesBancaires(dgRows.Cells[0].Value.ToString())
                                        where b.Compte == dgRows.Cells[22].Value.ToString()
                                        select b;
                            var bank = new Banque();
                            bank.NomEmploye = dgRows.Cells[1].Value.ToString();
                            bank.NumeroMatricule = dgRows.Cells[0].Value.ToString();
                            bank.Compte = dgRows.Cells[22].Value.ToString();
                            bank.NomBanque = dgRows.Cells[21].Value.ToString();
                            foreach (var b in banks)
                            {
                                bank.CodeBanque = b.CodeBanque;
                                bank.CodeGuichet = b.CodeGuichet;
                                bank.Cle = b.Cle;
                            }
                            bank.NetAPayer = double.Parse(dgRows.Cells[17].Value.ToString());
                            bank.FraisCommunication = ConnectionClass.PaiementParMatricule(numeroPaiement, dgRows.Cells[0].Value.ToString()).FraisCommunication;
                            bank.PrimeMotivation = ConnectionClass.PaiementParMatricule(numeroPaiement, dgRows.Cells[0].Value.ToString()).PrimeMotivation;
                            liste.Add(bank);
                            total += double.Parse(dgRows.Cells[17].Value.ToString());
                            totalFraisCom += ConnectionClass.PaiementParMatricule(numeroPaiement, dgRows.Cells[0].Value.ToString()).FraisCommunication;
                            totalPrimeMotivation += ConnectionClass.PaiementParMatricule(numeroPaiement, dgRows.Cells[0].Value.ToString()).PrimeMotivation;
                        }
                    }
                    }
                }
                var l = from lp in liste
                        orderby lp.NomBanque
                        //orderby lp.NomEmploye
                        select lp;
                var listeB1 = new List<Banque>();
                foreach (var p in l)
                {
                    listeB1.Add(p);
                }
                var listeB2 = new Banque();
                listeB2.NomEmploye = "TOTAL   SALAIRES, PRIMES ET FRAIS DE COMMUNICATION POUR  TOUTES LES  BANQUES";
                listeB2.NetAPayer = total;
                listeB2.FraisCommunication = totalFraisCom;
                listeB2.PrimeMotivation = totalPrimeMotivation;
                listeB1.Add(listeB2);
                if (listeB1.Count > 0)
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "PDF Documents (*.pdf)|*.pdf";

                    sharpPDF.pdfDocument document = new sharpPDF.pdfDocument("christian", "cdali");
                    var date = DateTime.Now.ToString().Replace("/", "_");
                    date = date.Replace(":", "_");

                    string pathFile = @"C:\Dossier Personnel\Ordre Virement\";
                    if (!System.IO.Directory.Exists(pathFile))
                    {
                        System.IO.Directory.CreateDirectory(pathFile);
                    }
                    sfd.FileName = @"C:\Dossier Personnel\Ordre Virement\ ordre virement mois  " + moisEtat + "_imprimé_le_" + date + ".pdf";
                    if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        var count = listeB1.Count;
                        var bitmap = Impression.ImprimerListeDesBancaries(numeroPaiement, listeB1, moisEtat, exercice);
                        string inputImage1 = @"cdali";
                        // Create an empty page
                        sharpPDF.pdfPage page = document.addPage(500, 700);
                        document.addImageReference(bitmap, inputImage1);
                        sharpPDF.Elements.pdfImageReference img = document.getImageReference(inputImage1);
                        page.addImage(img, 10, 0, page.height, page.width);
                        var div = (count - 9) / 32;
                        for (var i = 0; i <= div; i++)
                        {
                            if (i * 32 < count - 9)
                            {
                                bitmap = Impression.ImprimerListeDesBancaries(listeB1, i, numeroPaiement);
                                inputImage1 = @"cdali" + i;
                                // Create an empty page
                                page = document.addPage(500, 700);

                                document.addImageReference(bitmap, inputImage1);
                                img = document.getImageReference(inputImage1);
                                page.addImage(img, 10, 0, page.height, page.width);
                            }
                        }
                        var diff = (div + 1) * 32 + 9;
                        if (diff - count <= 9)
                        {
                            var paiement = ConnectionClass.ListeoOrdrePaiement(numeroPaiement);
                            var montantTotal = .0;
                            foreach (var paie in ConnectionClass.ListeDetailsPaiement(numeroPaiement))
                                if (paie.SalaireBrut <= 0) 
                                    montantTotal += paie.SalaireNet;
                            var text = "Arrêté le présent état de virement à la somme de :  " + Impression.Converti((int)montantTotal) + "(  " + String.Format(elGR, "{0:0,0}", montantTotal) + ") Franc CFA";

                            bitmap = AppCode.Impression.ImprimerOrdreDePaiement(numeroPaiement, text);
                            var inputImage = @"cdali0012";
                            // Create an empty page
                            var pageIndex = document.addPage(500, 700);
                            document.addImageReference(bitmap, inputImage);
                            var img1 = document.getImageReference(inputImage);
                            pageIndex.addImage(img1, 0, 0, pageIndex.height, pageIndex.width);
                        }
                        document.createPDF(sfd.FileName);
                        System.Diagnostics.Process.Start(sfd.FileName);
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        private void fonctionnaireToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var liste = new List<Banque>();
                var total = .0;
                var totalFraisCom = .0;
                var totalPrimeMotivation = .0;
                var totalAutresPrimes=.0;
                if (cmbBank.Text == "Toutes les banques")
                {
                    foreach (DataGridViewRow dgRows in dataGridView1.Rows)
                    {
                        if (!string.IsNullOrWhiteSpace(dgRows.Cells[21].Value.ToString().ToUpper()))
                        {
                            if (double.Parse(dgRows.Cells[9].Value.ToString()) <=0)
                            {
                                var banks = from b in ConnectionClass.ListeDonneesBancaires(dgRows.Cells[0].Value.ToString())
                                            where b.Compte == dgRows.Cells[22].Value.ToString()
                                            select b;
                                var bank = new Banque();
                                bank.NomEmploye = dgRows.Cells[1].Value.ToString();
                                bank.NumeroMatricule = dgRows.Cells[0].Value.ToString();
                                bank.Compte = dgRows.Cells[22].Value.ToString();
                                bank.NomBanque = dgRows.Cells[21].Value.ToString();
                                foreach (var b in banks)
                                {
                                    bank.CodeBanque = b.CodeBanque;
                                    bank.CodeGuichet = b.CodeGuichet;
                                    bank.Cle = b.Cle;
                                }
                                bank.NetAPayer = double.Parse(dgRows.Cells[17].Value.ToString());
                                bank.AutresPrimes = ConnectionClass.PaiementParMatricule(numeroPaiement, dgRows.Cells[0].Value.ToString()).AutresPrimes;
                                bank.FraisCommunication = ConnectionClass.PaiementParMatricule(numeroPaiement, dgRows.Cells[0].Value.ToString()).FraisCommunication;
                                bank.PrimeMotivation = ConnectionClass.PaiementParMatricule(numeroPaiement, dgRows.Cells[0].Value.ToString()).PrimeMotivation;
                                liste.Add(bank);
                                total += double.Parse(dgRows.Cells[17].Value.ToString());
                                totalFraisCom += ConnectionClass.PaiementParMatricule(numeroPaiement, dgRows.Cells[0].Value.ToString()).FraisCommunication;
                                totalPrimeMotivation += ConnectionClass.PaiementParMatricule(numeroPaiement, dgRows.Cells[0].Value.ToString()).PrimeMotivation;
                                totalAutresPrimes += ConnectionClass.PaiementParMatricule(numeroPaiement, dgRows.Cells[0].Value.ToString()).AutresPrimes;
                            }
                        }
                    }
                }
                else if (cmbBank.Text == "Paiement en espèces")
                {
                }
                else
                {
                    foreach (DataGridViewRow dgRows in dataGridView1.Rows)
                    {
                        if (double.Parse(dgRows.Cells[9].Value.ToString()) <= 0)
                        {
                            if (dgRows.Cells[21].Value.ToString().ToUpper() == cmbBank.Text.ToUpper())
                            {
                                var banks = from b in ConnectionClass.ListeDonneesBancaires(dgRows.Cells[0].Value.ToString())
                                            where b.Compte == dgRows.Cells[22].Value.ToString()
                                            select b;
                                var bank = new Banque();
                                bank.NomEmploye = dgRows.Cells[1].Value.ToString();
                                bank.NumeroMatricule = dgRows.Cells[0].Value.ToString();
                                bank.Compte = dgRows.Cells[22].Value.ToString();
                                bank.NomBanque = dgRows.Cells[21].Value.ToString();
                                foreach (var b in banks)
                                {
                                    bank.CodeBanque = b.CodeBanque;
                                    bank.CodeGuichet = b.CodeGuichet;
                                    bank.Cle = b.Cle;
                                }
                                bank.NetAPayer = double.Parse(dgRows.Cells[17].Value.ToString());
                                bank.AutresPrimes = ConnectionClass.PaiementParMatricule(numeroPaiement, dgRows.Cells[0].Value.ToString()).AutresPrimes;
                                bank.FraisCommunication = ConnectionClass.PaiementParMatricule(numeroPaiement, dgRows.Cells[0].Value.ToString()).FraisCommunication;
                                bank.PrimeMotivation = ConnectionClass.PaiementParMatricule(numeroPaiement, dgRows.Cells[0].Value.ToString()).PrimeMotivation;
                                liste.Add(bank);
                                total += ConnectionClass.PaiementParMatricule(numeroPaiement, dgRows.Cells[0].Value.ToString()).AutresPrimes;
                                totalFraisCom += ConnectionClass.PaiementParMatricule(numeroPaiement, dgRows.Cells[0].Value.ToString()).FraisCommunication;
                                totalPrimeMotivation += ConnectionClass.PaiementParMatricule(numeroPaiement, dgRows.Cells[0].Value.ToString()).PrimeMotivation;
                                totalAutresPrimes += ConnectionClass.PaiementParMatricule(numeroPaiement, dgRows.Cells[0].Value.ToString()).AutresPrimes;
                            }
                        }
                    }
                }
                var l = from lp in liste
                        orderby lp.NomBanque
                        //orderby lp.NomEmploye
                        select lp;
                var listeB1 = new List<Banque>();
                foreach (var p in l)
                {
                    listeB1.Add(p);
                }
                var listeB2 = new Banque();
                listeB2.NomEmploye = "TOTAL PRIMES ET FRAIS DE COMMUNICATION POUR  TOUTES LES  BANQUES";
                listeB2.NetAPayer = total;
                listeB2.FraisCommunication = totalFraisCom;
                listeB2.PrimeMotivation = totalPrimeMotivation;
                listeB1.Add(listeB2);
                if (listeB1.Count > 0)
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "PDF Documents (*.pdf)|*.pdf";

                    sharpPDF.pdfDocument document = new sharpPDF.pdfDocument("christian", "cdali");
                    var date = DateTime.Now.ToString().Replace("/", "_");
                    date = date.Replace(":", "_");

                    string pathFile = @"C:\Dossier Personnel\Ordre Virement\";
                    if (!System.IO.Directory.Exists(pathFile))
                    {
                        System.IO.Directory.CreateDirectory(pathFile);
                    }
                    sfd.FileName = @"C:\Dossier Personnel\Ordre Virement\ ordre virement mois  " + moisEtat + "_imprimé_le_" + date + ".pdf";
                    if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        var count = listeB1.Count;
                        var bitmap = Impression.OrdreDeVirementDesPrimes(numeroPaiement, listeB1, moisEtat, exercice);
                        string inputImage1 = @"cdali";
                        // Create an empty page
                        sharpPDF.pdfPage page = document.addPage(500, 700);
                        document.addImageReference(bitmap, inputImage1);
                        sharpPDF.Elements.pdfImageReference img = document.getImageReference(inputImage1);
                        page.addImage(img, 10, 0, page.height, page.width);
                        var div = (count - 9) / 32;
                        for (var i = 0; i <= div; i++)
                        {
                            if (i * 32 < count - 9)
                            {
                                bitmap = Impression.OrdreDeVirementDesPrimes(listeB1, i, numeroPaiement);
                                inputImage1 = @"cdali" + i;
                                // Create an empty page
                                page = document.addPage(500, 700);

                                document.addImageReference(bitmap, inputImage1);
                                img = document.getImageReference(inputImage1);
                                page.addImage(img, 10, 0, page.height, page.width);
                            }
                        }
                        var diff = (div + 1) * 32 + 9;
                        if (diff - count <= 9)
                        {
                            var paiement = ConnectionClass.ListeoOrdrePaiement(numeroPaiement);
                            var montantTotal = .0;
                            foreach (var paie in ConnectionClass.ListeDetailsPaiement(numeroPaiement))
                                if(paie.SalaireBrut<=0)
                                montantTotal += paie.SalaireNet;
                            var text = "Arrêté le présent état de virement à la somme de :  " + Impression.Converti((int)montantTotal) + "(  " + String.Format(elGR, "{0:0,0}", montantTotal) + ") Franc CFA";

                            bitmap = AppCode.Impression.ImprimerOrdreDePaiement(numeroPaiement, text);
                            var inputImage = @"cdali0012";
                            // Create an empty page
                            var pageIndex = document.addPage(500, 700);
                            document.addImageReference(bitmap, inputImage);
                            var img1 = document.getImageReference(inputImage);
                            pageIndex.addImage(img1, 0, 0, pageIndex.height, pageIndex.width);
                        }
                        document.createPDF(sfd.FileName);
                        System.Diagnostics.Process.Start(sfd.FileName);
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    var lstPaiement = new List<Paiement>();
                    var montantTotal=.0;
                    var irpp = .0; ; var salaireBase = .0; var congeAnnuel = .0; var salaireBrut = .0; var cnps = .0; var onasa = .0; var salaireNet = .0; var chargePatronale = .0;
                    for (var p = 0; p < dataGridView1.Rows.Count; p++)
                    {
                        if (!string.IsNullOrWhiteSpace(dataGridView1.Rows[p].Cells[0].Value.ToString()))
                        {
                            if (!string.IsNullOrWhiteSpace(dataGridView1.Rows[p].Cells[2].Value.ToString()))
                            {
                                if (double.Parse(dataGridView1.Rows[p].Cells[9].Value.ToString()) > 0 && double.Parse(dataGridView1.Rows[p].Cells[10].Value.ToString())>0)
                                {
                                    var paiement = new Paiement();
                                    paiement.SalaireBase = double.Parse(dataGridView1.Rows[p].Cells[4].Value.ToString());
                                    paiement.CongeAnnuel = double.Parse(dataGridView1.Rows[p].Cells[7].Value.ToString());
                                    paiement.SalaireBrut = double.Parse(dataGridView1.Rows[p].Cells[9].Value.ToString());
                                    paiement.Employe = dataGridView1.Rows[p].Cells[1].Value.ToString().ToUpper();
                                    paiement.CNPS = double.Parse(dataGridView1.Rows[p].Cells[10].Value.ToString());
                                    paiement.ChargePatronale = double.Parse(dataGridView1.Rows[p].Cells[18].Value.ToString());
                                    paiement.IRPP = double.Parse(dataGridView1.Rows[p].Cells[11].Value.ToString());
                                    paiement.ONASA = double.Parse(dataGridView1.Rows[p].Cells[12].Value.ToString());
                                    paiement.SalaireNet = paiement.SalaireBrut - paiement.CNPS - paiement.IRPP - paiement.ONASA;
                                    lstPaiement.Add(paiement);

                                    salaireBase += paiement.SalaireBase;
                                    congeAnnuel += paiement.CongeAnnuel;
                                    salaireBrut += paiement.SalaireBrut;
                                    cnps += paiement.CNPS;
                                    irpp += paiement.IRPP;
                                    onasa += paiement.ONASA;
                                    salaireNet += paiement.SalaireNet;
                                    chargePatronale += paiement.ChargePatronale;
                                    montantTotal += paiement.ONASA + paiement.SalaireNet + paiement.IRPP;
                                }
                            }
                        }
                    }
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "PDF Documents (*.pdf)|*.pdf";

                    sharpPDF.pdfDocument document = new sharpPDF.pdfDocument("christian", "cdali");
                    var jour = DateTime.Now.Day;
                    var moiSs = DateTime.Now.Month;
                    var year = DateTime.Now.Year;
                    var hour = DateTime.Now.Hour;
                    var min = DateTime.Now.Minute;
                    var sec = DateTime.Now.Second;
                    var datTe = jour.ToString() + "_" + moiSs.ToString() + "_" + year.ToString() + "_" + hour + "_" + min + "_" + sec;
                    sfd.FileName = "IRPP du mois " + moisEtat + "_imprimé_le_" + datTe + ".pdf";
                    var l = from li in lstPaiement
                            orderby li.Employe
                            select li;
                    var listePaiement = new List<Paiement>();

                    foreach (var paiement in l)
                    {
                        var p = new Paiement();
                        p.SalaireBase = paiement.SalaireBase;
                        p.CongeAnnuel = paiement.CongeAnnuel;
                        p.SalaireBrut = paiement.SalaireBrut;
                        p.Employe = paiement.Employe;
                        p.CNPS = paiement.CNPS;
                        p.ChargePatronale = paiement.ChargePatronale;
                        p.IRPP = paiement.IRPP;
                        p.ONASA = paiement.ONASA;
                        p.SalaireNet = paiement.SalaireNet;
                        listePaiement.Add(p);
                    }

                    var pp = new Paiement();
                    pp.SalaireBase = salaireNet;
                    pp.CongeAnnuel = congeAnnuel;
                    pp.SalaireBrut = salaireBrut;
                    pp.Employe = "TOTAL";
                    pp.CNPS = cnps;
                    pp.ChargePatronale = chargePatronale;
                    pp.IRPP = irpp;
                    pp.ONASA = onasa;
                    pp.SalaireNet = salaireNet;
                    listePaiement.Add(pp);
                    string pathFile = @"C:\Dossier Personnel\Ordre Virement\";
                    if (!System.IO.Directory.Exists(pathFile))
                    {
                        System.IO.Directory.CreateDirectory(pathFile);
                    }
                    sfd.FileName = @"C:\Dossier Personnel\Ordre Virement\" + label1.Text.Replace("/", "_") + "_imprimé_le_" + datTe + ".pdf"; ;
                    if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        {
                            var bitmap = AppCode.Impression.ImprimerOrdreDePaiement(numeroPaiement, listePaiement, exercice, moisEtat);

                            var inputImage = @"cdali";
                            // Create an empty page
                            sharpPDF.pdfPage pageIndex = document.addPage(500, 700);
                            document.addImageReference(bitmap, inputImage);
                            sharpPDF.Elements.pdfImageReference img1 = document.getImageReference(inputImage);
                            pageIndex.addImage(img1, 0, 0, pageIndex.height, pageIndex.width);
                            if (listePaiement.Count > 15)
                            {
                                var div = (listePaiement.Count - 15) / 27;

                                for (var i = 0; i <= div; i++)
                                {
                                    if (i * 27 < listePaiement.Count - 15)
                                    {
                                        bitmap = AppCode.Impression.ImprimerOrdreDePaiement(numeroPaiement, listePaiement, exercice, moisEtat, i);
                                        inputImage = @"cdali" + i;
                                        // Create an empty page
                                        pageIndex = document.addPage(500, 700);
                                        document.addImageReference(bitmap, inputImage);
                                        img1 = document.getImageReference(inputImage);
                                        pageIndex.addImage(img1, 0, 0, pageIndex.height, pageIndex.width);
                                    }
                                }
                                var diff = (div + 1) * 27 + 15;
                                if (diff - listePaiement.Count < 8)
                                {
                                    var paiement = ConnectionClass.ListeoOrdrePaiement(numeroPaiement);
                                    //var montantTotal = .0;
                                    //foreach (var paie in ConnectionClass.ListeDetailsPaiement(numeroPaiement))
                                    //    if(paie.SalaireBrut >0)
                                    //    montantTotal += paie.SalaireNet + paie.IRPP + paie.ONASA;
                                    var text = "Arrêté le présent état de paiement à la somme de : " + Impression.Converti((int)montantTotal) + "(NEP+IRPP+FIR =  " + String.Format(elGR, "{0:0,0}", montantTotal) + ") Franc CFA";

                                    bitmap = AppCode.Impression.ImprimerOrdreDePaiement(numeroPaiement, text);
                                    inputImage = @"cdali0012";
                                    // Create an empty page
                                    pageIndex = document.addPage(500, 700);
                                    document.addImageReference(bitmap, inputImage);
                                    img1 = document.getImageReference(inputImage);
                                    pageIndex.addImage(img1, 0, 0, pageIndex.height, pageIndex.width);
                                }
                            }
                        }
                        document.createPDF(sfd.FileName);
                        System.Diagnostics.Process.Start(sfd.FileName);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    var lstPaiement = new List<Paiement>();
                    var transport = .0;
                    var indemnites = .0;
                     var fraisCom = .0;
                    var primeMotiv = .0; 
                    var netPayer = .0;
                   var autresPrimes = .0;
                    for (var p = 0; p < dataGridView1.Rows.Count; p++)
                    {
                        if (!string.IsNullOrWhiteSpace(dataGridView1.Rows[p].Cells[0].Value.ToString()))
                        {
                            if (!string.IsNullOrWhiteSpace(dataGridView1.Rows[p].Cells[2].Value.ToString()))
                            {
                                if (!dataGridView1.Rows[p].Cells[2].Value.ToString().ToUpper().Contains("Directeur Général".ToUpper()) &&
                                    !dataGridView1.Rows[p].Cells[2].Value.ToString().ToUpper().Contains("Directrice Générale".ToUpper()) &&
                                    !dataGridView1.Rows[p].Cells[2].Value.ToString().ToUpper().Contains("Directeur General".ToUpper()) &&
                                    !dataGridView1.Rows[p].Cells[2].Value.ToString().ToUpper().Contains("Directrice Generale".ToUpper()))
                                {
                                    var numeroMatricule = dataGridView1.Rows[p].Cells[0].Value.ToString();
                                    var paiement = new Paiement();
                                    paiement.Employe = dataGridView1.Rows[p].Cells[1].Value.ToString().ToUpper();
                                    paiement.AutresPrimes = ConnectionClass.PaiementParMatricule(numeroPaiement, numeroMatricule).AutresPrimes;
                                    paiement.FraisCommunication = ConnectionClass.PaiementParMatricule(numeroPaiement, numeroMatricule).FraisCommunication;
                                    paiement.PrimeMotivation = ConnectionClass.PaiementParMatricule(numeroPaiement, numeroMatricule).PrimeMotivation;
                                    paiement.Indemnites = ConnectionClass.PaiementParMatricule(numeroPaiement, numeroMatricule).Indemnites;
                                    paiement.Transport = ConnectionClass.PaiementParMatricule(numeroPaiement, numeroMatricule).Transport;
                                    paiement.SalaireNet = paiement.AutresPrimes + paiement.FraisCommunication + paiement.PrimeMotivation + paiement.Transport + paiement.Indemnites;
                                    autresPrimes += paiement.AutresPrimes;
                                    netPayer += paiement.SalaireNet;
                                    primeMotiv += paiement.PrimeMotivation;
                                    fraisCom += paiement.FraisCommunication;
                                    indemnites += paiement.Indemnites;
                                    transport += paiement.Transport;

                                    if (paiement.SalaireNet > 0)
                                        lstPaiement.Add(paiement);
                                }
                            }
                        }
                    }
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "PDF Documents (*.pdf)|*.pdf";

                    sharpPDF.pdfDocument document = new sharpPDF.pdfDocument("christian", "cdali");
                    var jour = DateTime.Now.Day;
                    var moiSs = DateTime.Now.Month;
                    var year = DateTime.Now.Year;
                    var hour = DateTime.Now.Hour;
                    var min = DateTime.Now.Minute;
                    var sec = DateTime.Now.Second;
                    var datTe = jour.ToString() + "_" + moiSs.ToString() + "_" + year.ToString() + "_" + hour + "_" + min + "_" + sec;
                    sfd.FileName = "IRPP du mois " + moisEtat + "_imprimé_le_" + datTe + ".pdf";
                    var l = from li in lstPaiement
                            orderby li.Employe
                            select li;
                    var listePaiement = new List<Paiement>();

                    foreach (var paiement in l)
                    {
                        var p = new Paiement();
                        p.FraisCommunication = paiement.FraisCommunication;
                        p.PrimeMotivation = paiement.PrimeMotivation;
                        p.AutresPrimes = paiement.AutresPrimes;
                        p.Employe = paiement.Employe;                    
                        p.Indemnites = paiement.Indemnites;
                        p.Transport = paiement.Transport;
                        p.SalaireNet = paiement.SalaireNet;
                        listePaiement.Add(p);
                    }

                    var pp = new Paiement();
                    pp.Employe = "TOTAL";
                    pp.FraisCommunication = fraisCom;
                    pp.PrimeMotivation = primeMotiv;
                    pp.SalaireNet = netPayer;
                    pp.AutresPrimes = autresPrimes;
                    pp.Indemnites = indemnites;
                    pp.Transport = transport;
                    listePaiement.Add(pp);
                    string pathFile = @"C:\Dossier Personnel\Ordre Virement\";
                    if (!System.IO.Directory.Exists(pathFile))
                    {
                        System.IO.Directory.CreateDirectory(pathFile);
                    }
                    sfd.FileName = @"C:\Dossier Personnel\Ordre Virement\" + label1.Text.Replace("/", "_") + "_imprimé_le_" + datTe + ".pdf"; ;
                    if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        {
                            var bitmap = AppCode.Impression.ImprimerOrdreDePaiementPrimes(numeroPaiement, listePaiement, exercice, moisEtat);

                            var inputImage = @"cdali";
                            // Create an empty page
                            sharpPDF.pdfPage pageIndex = document.addPage(500, 700);
                            document.addImageReference(bitmap, inputImage);
                            sharpPDF.Elements.pdfImageReference img1 = document.getImageReference(inputImage);
                            pageIndex.addImage(img1, 0, 0, pageIndex.height, pageIndex.width);
                            if (listePaiement.Count > 15)
                            {
                                var div = (listePaiement.Count - 15) / 27;

                                for (var i = 0; i <= div; i++)
                                {
                                    if (i * 27 < listePaiement.Count - 15)
                                    {
                                        bitmap = AppCode.Impression.ImprimerOrdreDePaiementPrimes(numeroPaiement, listePaiement, exercice, moisEtat, i);
                                        inputImage = @"cdali" + i;
                                        // Create an empty page
                                        pageIndex = document.addPage(500, 700);
                                        document.addImageReference(bitmap, inputImage);
                                        img1 = document.getImageReference(inputImage);
                                        pageIndex.addImage(img1, 0, 0, pageIndex.height, pageIndex.width);
                                    }
                                }
                                var diff = (div + 1) * 27 + 15;
                                if (diff - listePaiement.Count < 8)
                                {
                                    var paiement = ConnectionClass.ListeoOrdrePaiement(numeroPaiement);
                                    var montantTotal = autresPrimes+fraisCom+primeMotiv;
                                    //foreach (var paie in ConnectionClass.ListeDetailsPaiement(numeroPaiement))
                                      
                                    var text = "Arrêté le présent état de paiement à la somme de : " + Impression.Converti((int)montantTotal) + "( " + String.Format(elGR, "{0:0,0}", montantTotal) + ") Franc CFA";

                                    bitmap = AppCode.Impression.ImprimerOrdreDePaiement(numeroPaiement, text);
                                    inputImage = @"cdali0012";
                                    // Create an empty page
                                    pageIndex = document.addPage(500, 700);
                                    document.addImageReference(bitmap, inputImage);
                                    img1 = document.getImageReference(inputImage);
                                    pageIndex.addImage(img1, 0, 0, pageIndex.height, pageIndex.width);
                                }
                            }
                        }
                        document.createPDF(sfd.FileName);
                        System.Diagnostics.Process.Start(sfd.FileName);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    var lstPaiement = new List<Paiement>();
                    
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "PDF Documents (*.pdf)|*.pdf";

                    sharpPDF.pdfDocument document = new sharpPDF.pdfDocument("christian", "cdali");
                    var jour = DateTime.Now.Day;
                    var moiSs = DateTime.Now.Month;
                    var year = DateTime.Now.Year;
                    var hour = DateTime.Now.Hour;
                    var min = DateTime.Now.Minute;
                    var sec = DateTime.Now.Second;
                    var datTe = jour.ToString() + "_" + moiSs.ToString() + "_" + year.ToString() + "_" + hour + "_" + min + "_" + sec;
                    sfd.FileName = "IRPP du mois " + moisEtat + "_imprimé_le_" + datTe + ".pdf";
               
                    var listePaiement = new List<Paiement>();
                    var liste = from p in ConnectionClass.ListeDetailsPaiement(numeroPaiement)
                                join s in ConnectionClass.ListeServicePersonnel()
                                on p.NumeroMatricule equals s.NumeroMatricule
                                //where (s.Poste == "Directeur Général" || where s.Poste==""
                                select new
                                {
                               s.Poste,   p.CongeAnnuel,  p.Employe, p.SalaireBase,p.SalaireBrut,p.CNRT,p.CNRTEmploye,p.IRPP ,p.ChargePatronale,p.Indemnites ,p.SalaireNet,p.ONASA,p.NumeroMatricule
                                };
                    liste = liste.Where(p => p.Poste.ToLower() == "Directeur Général".ToLower() || p.Poste.ToLower() == "Directrice Générale".ToLower());
                    foreach (var paiement in liste)
                    {
                        var p = new Paiement();
                        p.SalaireBase = paiement.SalaireBase;
                        p.CongeAnnuel = paiement.CongeAnnuel;
                        p.SalaireBrut = paiement.SalaireBrut;
                        p.Employe = paiement.Employe;
                        p.CNRT = paiement.CNRT;
                        p.CNRTEmploye = paiement.CNRTEmploye;
                        p.Indemnites = paiement.Indemnites;
                        p.NumeroMatricule = paiement.NumeroMatricule;
                        p.ChargePatronale = paiement.ChargePatronale;
                        p.IRPP = paiement.IRPP;
                        p.ONASA = paiement.ONASA;
                        p.SalaireNet = paiement.SalaireNet;
                        listePaiement.Add(p);
                    }
                    foreach (var paiement in liste)
                    {
                        var p = new Paiement();
                        p.SalaireBase = paiement.SalaireBase;
                        p.CongeAnnuel = paiement.CongeAnnuel;
                        p.SalaireBrut = paiement.SalaireBrut;
                        p.Employe ="TOTAL";
                        p.CNRT = paiement.CNRT;
                        p.CNRTEmploye = paiement.CNRTEmploye;
                        p.Indemnites = paiement.Indemnites;
                        p.ChargePatronale = paiement.ChargePatronale;
                        p.IRPP = paiement.IRPP;
                        p.ONASA = paiement.ONASA;
                        p.SalaireNet = paiement.SalaireNet;
                        listePaiement.Add(p);
                    }
                    string pathFile = @"C:\Dossier Personnel\Ordre Virement\";
                    if (!System.IO.Directory.Exists(pathFile))
                    {
                        System.IO.Directory.CreateDirectory(pathFile);
                    }
                    sfd.FileName = @"C:\Dossier Personnel\Ordre Virement\" + label1.Text.Replace("/", "_") + "_imprimé_le_" + datTe + ".pdf"; ;
                    if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        var bitmap = AppCode.Impression.ImprimerOrdreDePaiementDirectionGenerale(numeroPaiement, listePaiement, exercice, moisEtat);
                        var inputImage = @"cdali";
                        // Create an empty page
                        sharpPDF.pdfPage pageIndex = document.addPage(500, 700);
                        document.addImageReference(bitmap, inputImage);
                        sharpPDF.Elements.pdfImageReference img1 = document.getImageReference(inputImage);
                        pageIndex.addImage(img1, 0, 0, pageIndex.height, pageIndex.width);
                        document.createPDF(sfd.FileName);
                        System.Diagnostics.Process.Start(sfd.FileName);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    var lstPaiement = new List<Paiement>();

                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "PDF Documents (*.pdf)|*.pdf";

                    sharpPDF.pdfDocument document = new sharpPDF.pdfDocument("christian", "cdali");
                    var jour = DateTime.Now.Day;
                    var moiSs = DateTime.Now.Month;
                    var year = DateTime.Now.Year;
                    var hour = DateTime.Now.Hour;
                    var min = DateTime.Now.Minute;
                    var sec = DateTime.Now.Second;
                    var datTe = jour.ToString() + "_" + moiSs.ToString() + "_" + year.ToString() + "_" + hour + "_" + min + "_" + sec;
                    sfd.FileName = "IRPP du mois " + moisEtat + "_imprimé_le_" + datTe + ".pdf";

                    var listePaiement = new List<Paiement>();
                    var liste = from p in ConnectionClass.ListeDetailsPaiement(numeroPaiement)
                                join s in ConnectionClass.ListeServicePersonnel()
                                on p.NumeroMatricule equals s.NumeroMatricule
                                select new
                                {
                                    p.CongeAnnuel,
                                    p.Employe,
                                    p.SalaireBase,
                                    p.SalaireBrut,
                                    p.CNRT,
                                    p.CNRTEmploye,
                                    p.IRPP,
                                    p.ChargePatronale,
                                    p.Indemnites,
                                    p.SalaireNet,
                                    p.ONASA,
                                    p.NumeroMatricule,s.Poste
                                };
                    liste = liste.Where(p => p.Poste.ToLower() == "Directeur Général Adjoint".ToLower() || p.Poste.ToLower() == "Directrice Générale Adjointe".ToLower());
                    foreach (var paiement in liste)
                    {
                        var p = new Paiement();
                        p.SalaireBase = paiement.SalaireBase;
                        p.CongeAnnuel = paiement.CongeAnnuel;
                        p.SalaireBrut = paiement.SalaireBrut;
                        p.Employe = paiement.Employe;
                        p.CNRT = paiement.CNRT;
                        p.CNRTEmploye = paiement.CNRTEmploye;
                        p.Indemnites = paiement.Indemnites;
                        p.NumeroMatricule = paiement.NumeroMatricule;
                        p.ChargePatronale = paiement.ChargePatronale;
                        p.IRPP = paiement.IRPP;
                        p.ONASA = paiement.ONASA;
                        p.SalaireNet = paiement.SalaireNet;
                        listePaiement.Add(p);
                    }
                    foreach (var paiement in liste)
                    {
                        var p = new Paiement();
                        p.SalaireBase = paiement.SalaireBase;
                        p.CongeAnnuel = paiement.CongeAnnuel;
                        p.SalaireBrut = paiement.SalaireBrut;
                        p.Employe = "TOTAL";
                        p.CNRT = paiement.CNRT;
                        p.CNRTEmploye = paiement.CNRTEmploye;
                        p.Indemnites = paiement.Indemnites;
                        p.ChargePatronale = paiement.ChargePatronale;
                        p.IRPP = paiement.IRPP;
                        p.ONASA = paiement.ONASA;
                        p.SalaireNet = paiement.SalaireNet;
                        listePaiement.Add(p);
                    }
                    string pathFile = @"C:\Dossier Personnel\Ordre Virement\";
                    if (!System.IO.Directory.Exists(pathFile))
                    {
                        System.IO.Directory.CreateDirectory(pathFile);
                    }
                    sfd.FileName = @"C:\Dossier Personnel\Ordre Virement\" + label1.Text.Replace("/", "_") + "_imprimé_le_" + datTe + ".pdf"; ;
                    if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        var bitmap = AppCode.Impression.ImprimerOrdreDePaiementDirectionGenerale(numeroPaiement, listePaiement, exercice, moisEtat);
                        var inputImage = @"cdali";
                        // Create an empty page
                        sharpPDF.pdfPage pageIndex = document.addPage(500, 700);
                        document.addImageReference(bitmap, inputImage);
                        sharpPDF.Elements.pdfImageReference img1 = document.getImageReference(inputImage);
                        pageIndex.addImage(img1, 0, 0, pageIndex.height, pageIndex.width);
                        document.createPDF(sfd.FileName);
                        System.Diagnostics.Process.Start(sfd.FileName);
                    }
                }
            }
            catch (Exception)
            {
            }
        }

    }
}
