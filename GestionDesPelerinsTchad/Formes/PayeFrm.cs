using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SGSP.AppCode;
using Excel = Microsoft.Office.Interop.Excel; 

namespace SGSP.Formes
{
    public partial class PayeFrm : Form
    {
        public PayeFrm()
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

        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.Silver, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox2.Width - 1, this.groupBox2.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.SteelBlue, Color.DarkSlateBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void PayeFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SlateBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.WhiteSmoke, Color.WhiteSmoke, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox3_Paint(object sender, PaintEventArgs e)
        {

            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.Silver, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox3.Width - 1, this.groupBox3.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.SteelBlue, Color.DodgerBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void PayeFrm_Load(object sender, EventArgs e)
        {
            txtExercice.Text = DateTime.Now.Year.ToString();
            txtExercice.ReadOnly = false;
            Column2.Width = dataGridView1.Width / 2 - 340;
            button5.Location = new Point(Width - 45, button5.Location.Y);
            panel1.Location = new Point(dataGridView1.Width + 8, panel1.Location.Y);
        }

        void MontantTotal()
        {
            try
            {
                var total = 0.0;
                for (var i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    total += double.Parse(dataGridView1.Rows[i].Cells[22].Value.ToString());
                }

                txtTotal.Text = "Nombre salarié traité  : " + dataGridView1.Rows.Count + "        Cout total : " + String.Format(elGR, "{0:0,0}", total) + "   ";
            }
            catch { }
        }
        int exercice, numeroPaiement;
        string mois;
        DateTime date;
        System.Globalization.CultureInfo elGR = System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR"); 
        private void RemplissageDesDonnees()
        {
            try
            {
                dataGridView1.Rows.Clear();
                numeroPaiement = 0;
                if (Int32.TryParse(txtExercice.Text,out  exercice) && ! string.IsNullOrEmpty(cmbMois.Text))
                {
                    cmbMois.BackColor = Color.White;
                    var liste = ConnectionClass.ListeDetailsPaiement(exercice ,cmbMois.Text);
                    var listePaie = from p in  ConnectionClass.ListeoOrdrePaiement()
                                        where p.Exercice == exercice
                                        where p.MoisPaiement ==cmbMois.Text
                                        select p;
                      foreach(var p in listePaie)
                      {
                    numeroPaiement = p.IDPaie;
                    mois = p.MoisPaiement;
                    date = p.DatePaiement;
                      }
                    foreach (var paiement in liste)
                    {
                        var personnel = ConnectionClass.ListePersonnelParMatricule(paiement.NumeroMatricule);
                        var nomEmploye = personnel[0].Nom + " " + personnel[0].Prenom;
                        //var dtSalaire = ConnectionClass.ListeSalaire(paiement.NumeroMatricule);
                        //paiement.SalaireBase = Double.Parse(dtSalaire.Rows[0].ItemArray[0].ToString());
                        var indiceAnciennete = ConnectionClass.ListeServicePersonnel(paiement.NumeroMatricule)[0].Anciennete;
                    if (indiceAnciennete.Contains("%"))
                    {
                        indiceAnciennete.Remove(indiceAnciennete.IndexOf("%"));
                    }
                    double anciennete;
                    if (double.TryParse(indiceAnciennete, out  anciennete))
                    {
                    }
                    else
                    {
                        anciennete = 0;
                    }
                    var salaireGain = System.Math.Round(paiement.SalaireBase * (1 + anciennete / 100));
                        
                            dataGridView1.Rows.Add(
                              paiement.NumeroMatricule, nomEmploye, 
                              String.Format(elGR, "{0:0,0}", paiement.SalaireBase),
                                    String.Format(elGR, "{0:0,0}", salaireGain),
                                    String.Format(elGR, "{0:0,0}", paiement.CongeAnnuel),
                                     String.Format(elGR, "{0:0,0}", paiement.CoutAbsence),
                                     String.Format(elGR, "{0:0,0}", paiement.SalaireBrut),
                                     String.Format(elGR, "{0:0,0}", paiement.CNPS),
                                    String.Format(elGR, "{0:0,0}", paiement.IRPP),
                                    paiement.ONASA,
                                    String.Format(elGR, "{0:0,0}", paiement.ChargePatronale),
                                    String.Format(elGR, "{0:0,0}", paiement.AvanceSurSalaire),
                                      String.Format(elGR, "{0:0,0}", paiement.AcomptePaye),
                                      String.Format(elGR, "{0:0,0}", paiement.ChargeSoinFamille),
                                           String.Format(elGR, "{0:0,0}", paiement.CNRT),
                                      String.Format(elGR, "{0:0,0}", paiement.CNRTEmploye),
                                  String.Format(elGR, "{0:0,0}", paiement.Indemnites),
                                  String.Format(elGR, "{0:0,0}", paiement.AutresPrimes),
                                  String.Format(elGR, "{0:0,0}", paiement.PrimeMotivation),
                                  String.Format(elGR, "{0:0,0}", paiement.FraisCommunication),
                                  String.Format(elGR, "{0:0,0}", paiement.Transport),
                                  String.Format(elGR, "{0:0,0}", paiement.SalaireNet),
                                   String.Format(elGR, "{0:0,0}", paiement.CoutDuSalarie),
                                  paiement.ModePaiement,
                                   paiement.IDAcompte,paiement.Banque);
                                                    
                    }
                    var salaireNetTotal = 0.0;
                    for (var j = 0; j < dataGridView1.Rows.Count; j++)
                    {
                        salaireNetTotal += double.Parse(dataGridView1.Rows[j].Cells[16].Value.ToString());
                    }
                    label4.Text = "Total : " +   String.Format(elGR, "{0:0,0}", salaireNetTotal) + " F. CFA";
                }
                else
                {
                    cmbMois.BackColor = Color.Red;
                    //GestionPharmacetique.MonMessageBox.ShowBox("L'exercice ou le mois ne sont pa valides", "Erreur");
                }
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("Remplissage des donnees", ex);
            }
        }

        private void cmbMois_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                mois = cmbMois.Text;
                RemplissageDesDonnees();
                MontantTotal();
            }
            catch (Exception ex) { GestionPharmacetique.MonMessageBox.ShowBox("remplissage des donnees", ex); }
        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        Bitmap document;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(document, 10, 10, document.Width, document.Height);
            e.HasMorePages = false;
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
        }


        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void cmbMois_Click(object sender, EventArgs e)
        {
            cmbMois.BackColor = Color.White;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkBox1.Checked)
                {
                    #region MyRegion
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
                    sfd.FileName = label1.Text.Replace("/", "_") + "_imprimé_le_" + datTe + ".pdf";

                    if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                               
                    for (var i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                            var paiement = ConnectionClass.PaiementParMatricule(numeroPaiement, dataGridView1.Rows[i].Cells[0].Value.ToString());
                            paiement.Exercice = exercice;
                            paiement.MoisPaiement = mois;
                            paiement.DatePaiement = date;
                            var personnel = ConnectionClass.ListePersonnelParMatricule(paiement.NumeroMatricule)[0];
                            var service = ConnectionClass.ListeServicePersonnel(paiement.NumeroMatricule)[0];
                        if (service.TypeContrat .ToUpper().Contains("CONTRACTUEL") || service.SiCNRT)
                        {
                            var bitmap = Impression.ImprimerUnBulletinDePaie(paiement, personnel, service);
                            string inputImage1 = @"cdali" + i;
                            // Create an empty page
                            sharpPDF.pdfPage page = document.addPage();

                            document.addImageReference(bitmap, inputImage1);
                            sharpPDF.Elements.pdfImageReference img = document.getImageReference(inputImage1);
                            page.addImage(img, -5, 0, page.height, page.width);
                        }
                    }
                    document.createPDF(sfd.FileName);
                    System.Diagnostics.Process.Start(sfd.FileName);
                    }
	#endregion
                        
                }
                else
                {
                    #region MyRegion
                    if (dataGridView1.SelectedRows.Count > 0)
                    {
                        var paiement = ConnectionClass.PaiementParMatricule(numeroPaiement, dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                        paiement.Exercice = exercice;
                        paiement.MoisPaiement = mois;
                        paiement.DatePaiement = date;
                        var personnel = ConnectionClass.ListePersonnelParMatricule(paiement.NumeroMatricule)[0];
                        var service = ConnectionClass.ListeServicePersonnel(paiement.NumeroMatricule)[0];

                        if (service.TypeContrat.ToUpper().Contains("CONTRACTUEL") || service.SiCNRT)
                        {
                            if (printDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            {
                                document = Impression.ImprimerUnBulletinDePaie(paiement, personnel, service);
                                printPreviewDialog1.ShowDialog();
                            }
                        }
                        else
                        {
                            GestionPharmacetique.MonMessageBox.ShowBox("Ce personnel n'est pas un contractuel ", "Erreur");
                        }

                    #endregion
                    }
                }
                    
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("Impression du bulletin", ex);
            }
        }

        private void button10_Click(object sender, EventArgs e)
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

                    ToCsV(dataGridView1, sfd.FileName); // Here dataGridview1 is your grid view name
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("", ex);
            }
        }

        private void ToCsV(DataGridView dGV, string filename)
        {
            try
            {
                string stOutput = "";
                // Export titles:
                string sHeaders = "";

                for (int j = 1; j < dGV.Columns.Count-1; j++)
                    sHeaders = sHeaders.ToString() + Convert.ToString(dGV.Columns[j].HeaderText) + "\t";
                stOutput += sHeaders + "\r\n";
                // Export data.
                for (int i = 0; i < dGV.RowCount; i++)
                {
                    string stLine = "";
                    for (int j = 1; j < dGV.Rows[i].Cells.Count-1; j++)
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
      
        List<Paiement> ListeDesPaiements()
        {
            try
            {    
                var listePaie = new List<Paiement>();
                foreach (var  personnel in ConnectionClass.ListePersonnel(""))
                {
                    var numMatricule = personnel.NumeroMatricule;
                    var service = ConnectionClass.ListeServicePersonnel(personnel.NumeroMatricule);
                    //var banque = dtRow.ItemArray[26].ToString();
                    var listeConge = ConnectionClass.ListeFraisConge(Convert.ToInt32(txtExercice.Text), mois, numMatricule);
                    if (listeConge.Count <= 0)
                        if (!ConnectionClass.StatusContrat( service[0].Status)  )
                        {
                            var salaire = AppCode.ConnectionClass.ListeSalaire(numMatricule);

                            if (salaire[0].SalaireBase + salaire[0].FraisCommunication + salaire[0].AutresPrimes +
                                salaire[0].Indemnites + salaire[0].PrimeMotivation + salaire[0].PrimeTransport > 0)
                            {
                                double primeLogement, primeResponsabilite, primeGarde, primeTransport, primeExceptionnelle, salaireDeBase, avanceSurSalaire, coutAbscence;
                                double netAPayer, onasa, totalCnps, chargePatronale,  totalCNRTEmploye=.0, totalCNRTEmployeur=.0 ,totalIRPP = 0;
                                int numeroAcompte;
                                double indice = 1 ;
                     
                                var dtAccompte = AppCode.ConnectionClass.ListeAccompte(numMatricule);

                                var liste = AppCode.ConnectionClass.ListeAvanceSurSalaire(exercice, mois, numMatricule);
                                if (liste != null)
                                {
                                    var montantTotal = 0.0;
                                    foreach (var av in liste)
                                    {
                                        montantTotal += av.AvanceSurSalaire;
                                    }
                                    avanceSurSalaire = montantTotal;
                                }
                                else
                                {
                                    avanceSurSalaire = 0.0;
                                }
                                salaireDeBase =salaire[0].SalaireBase;
                                primeGarde = salaire[0].AutresPrimes;
                                primeLogement = salaire[0].Indemnites;
                                primeResponsabilite = salaire[0].PrimeMotivation;
                                primeTransport =salaire[0].PrimeTransport;
                                primeExceptionnelle = salaire[0].FraisCommunication;

                                Double remboursement, accompte, aPayer;
                                if (dtAccompte.Rows.Count > 0)
                                {
                                    if (Double.TryParse(dtAccompte.Rows[0].ItemArray[2].ToString(), out accompte) &&
                                        Double.TryParse(dtAccompte.Rows[0].ItemArray[3].ToString(), out remboursement) &&
                                        Double.TryParse(dtAccompte.Rows[0].ItemArray[4].ToString(), out aPayer))
                                    {
                                        numeroAcompte = Convert.ToInt32(dtAccompte.Rows[0].ItemArray[0].ToString());
                                        if (remboursement >= accompte)
                                        {
                                            aPayer = 0.0;
                                            numeroAcompte = 0;
                                        }
                                        else
                                        {
                                            var reste = accompte - remboursement;
                                        }
                                    }
                                    else
                                    {
                                        numeroAcompte = 0;
                                        accompte = remboursement = aPayer = 0.0;
                                    }
                                }
                                else
                                {
                                    numeroAcompte = 0;
                                    aPayer = 0.0;
                                }

                                
                                var typeContrat = service[0].TypeContrat;
                                var salaireBrut = .0;
                                var gainSalarial = .0;
                                if (typeContrat.ToUpper().Contains("CONTRACTUEL"))
                                {
                                    salaireBrut = System.Math.Round(salaireDeBase * indice);
                                    gainSalarial = System.Math.Round(salaireDeBase *( indice-1));
                                }
                                else
                                {
                                    gainSalarial =0;
                                    salaireBrut = salaireDeBase;
                                }
                                var tauxConge = System.Math.Round(salaireBrut / 24);
                                var tauxAbscense = System.Math.Round(salaireBrut / 26);

                                if (typeContrat.ToUpper().Contains("CONTRACTUEL".ToUpper()))
                                {
                                    var cnps = 3.5;
                                    var cnpsCsdn = 16.5;
                                    onasa = 40;
                                    totalCnps = System.Math.Round(salaireBrut * cnps / 100);
                                    chargePatronale = System.Math.Round(salaireBrut * cnpsCsdn / 100);
                                    if (salaireBrut > 500000)
                                    {
                                        totalCnps = 500000 * cnps / 100;
                                        chargePatronale = System.Math.Round(500000 * cnpsCsdn / 100);
                                    }
                               
                                    var salaireImposable = (System.Math.Round(salaireDeBase *  indice ) - totalCnps) * 12;
                                   totalIRPP=GlobalVariable.IRPP(salaireImposable,salaireBrut,totalCnps,.0);
                                   onasa = GlobalVariable.ONASA(salaireImposable, .0);
                                }
                                else if (service[0].SiCNRT)
                                {
                                    var cnps = .0;
                                    var cnpsCsdn = 11.5;
                                    onasa = 40;
                                    totalCnps = System.Math.Round(salaireBrut * cnps / 100);
                                    chargePatronale = System.Math.Round(salaireBrut * cnpsCsdn / 100);
                                    if (salaireBrut > 500000)
                                    {
                                        totalCnps = 500000 * cnps / 100;
                                        chargePatronale = System.Math.Round(500000 * cnpsCsdn / 100);
                                    }
                                    var baseCRNT=.0;
                                    var cnrtEmploye = 5.0;
                                    var cnrtEmployeur = 12.0;
                         
                                    if (service[0].Poste == "Directeur Général")
                                    {
                                        baseCRNT = 224250.0;
                                    }
                                    else if (service[0].Poste == "Directrice Générale Adjointe")
                                    {
                                        baseCRNT = 138000.0;
                                    }
                                    totalCNRTEmploye = System.Math.Round(baseCRNT * cnrtEmploye / 100);
                                    totalCNRTEmployeur = System.Math.Round(baseCRNT * cnrtEmployeur / 100);
                                    var salaireImposable = (System.Math.Round(salaireDeBase * indice) - totalCnps) * 12;
                                    totalIRPP = GlobalVariable.IRPP(salaireImposable, salaireBrut, totalCNRTEmploye, .0);
                                    onasa = GlobalVariable.ONASA(salaireImposable, .0);
                                }
                                else
                                {
                                    chargePatronale = 0;
                                    totalCnps = 0;
                                    totalIRPP = 0;
                                    onasa = 0;
                                }
                                    var soinFamille = .0;
                                    var totalPrime = primeGarde + primeExceptionnelle + primeLogement + primeTransport + primeResponsabilite;
                                    var totalCharge = totalCnps + totalIRPP + onasa+totalCNRTEmploye;

                                    var totalDette = avanceSurSalaire + aPayer + soinFamille;
                                    netAPayer = salaireBrut + totalPrime - totalDette - totalCharge;
                                    var coutSalarial = salaireBrut + chargePatronale + totalPrime+totalCNRTEmployeur;

                                    var p = new AppCode.Paiement();

                                    p.ChargeSoinFamille = soinFamille;
                                    p.DatePaiement = DateTime.Now;
                                    p.AcomptePaye = aPayer;
                                    p.AvanceSurSalaire = avanceSurSalaire;
                                    p.ChargePatronale = chargePatronale;
                                    p.CNPS = totalCnps;
                                    p.CongeAnnuel = .0;
                                    p.CoutDuSalarie = coutSalarial;
                                    p.SalaireBrut = salaireBrut;
                                    p.FraisCommunication = primeExceptionnelle;
                                    p.IDAcompte = numeroAcompte;
                                    p.IRPP = totalIRPP;
                                    p.NumeroMatricule = numMatricule;
                                    p.ONASA = onasa;
                                    p.AutresPrimes = primeGarde;
                                    p.Indemnites = primeLogement;
                                    p.PrimeMotivation = primeResponsabilite;
                                    p.SalaireBase = salaireDeBase;
                                    p.SalaireNet = netAPayer;
                                    p.Transport = primeTransport;
                                    p.IDAcompte = numeroAcompte;
                                    p.Service = typeContrat;
                                    p.CNRT = totalCNRTEmploye;
                                    p.CNRTEmploye = totalCNRTEmployeur;
                                    p.GainAnciennete = Math.Round(gainSalarial);
                                    var banques = ConnectionClass.ListeDonneesBancaires(numMatricule);
                                    foreach(var bank in banques)
                                    {
                                       p.Banque= bank.NomBanque;
                                      p.Compte=  bank.Compte;
                                    }
                                    if (!string.IsNullOrWhiteSpace(p.Compte))
                                    {
                                        p.ModePaiement = "Virement bancaire";
                                    }
                                    else
                                    {
                                        p.ModePaiement = "Paiement en espèce";
                                    }
                                    listePaie.Add(p);
                                
                            }

                        }
                }
                    return listePaie;
                
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("Creer detail de paie", ex);
                return null;
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(cmbMois.Text) && cmbMois.Text != "<Choisir mois>")
                {
                    int exercice;
                    if (Int32.TryParse(txtExercice.Text, out exercice))
                    {
                        var listePaiement = ListeDesPaiements();
                          var dtP = ConnectionClass.ListeDesPersonnelParFonction("DIRECTEUR");
                        
                        var flag = checkBox2.Checked;
                        var paiement = new Paiement();
                        paiement.MontantTotal = 0.0;
                        paiement.MoisPaiement = cmbMois.Text;
                        paiement.Exercice = exercice;
                        paiement.Liquidateur = "";
                        paiement.Controleur = "";
                        if (dtP.Rows.Count > 0)
                        {
                            paiement.Directeur = dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString();
                        }
                        else
                        {
                            paiement.Directeur = "";
                        }
                        numeroPaiement = ConnectionClass.CreerUnOrdreDePaiement(paiement, listePaiement,flag) ;
                        if (numeroPaiement > 0)
                        {
                            cmbMois.Text = paiement.MoisPaiement;
                            dataGridView1.Rows.Clear();
                            txtTotal.Text = paiement.MontantTotal.ToString();
                            RemplissageDesDonnees(numeroPaiement);
                        }
                    }
                    else
                    {
                        GestionPharmacetique.MonMessageBox.ShowBox("Veuillez entrer un chiffre valide pour l'annee de paiement", "Erreur");
                    }
                }
                else
                {
                    GestionPharmacetique.MonMessageBox.ShowBox("Veuillez selectionner le mois de paiement sur la liste puis continuez.", "Erreur");
                }

            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("", ex);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RemplissageDesDonnees();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void RemplissageDesDonnees(int numeroPaiement)
        {
            try
            {
                dataGridView1.Rows.Clear();
                var liste = ConnectionClass.ListeDetailsPaiement(numeroPaiement);

                dataGridView1.Rows.Clear();

                foreach (var paiement in liste)
                {
                    var personnel = ConnectionClass.ListePersonnelParMatricule(paiement.NumeroMatricule);
                    var nomEmploye = personnel[0].Nom + " " + personnel[0].Prenom;
                    var service = ConnectionClass.ListeServicePersonnel(paiement.NumeroMatricule);
                    var indiceAnciennete = service[0].Anciennete;
                    if (indiceAnciennete.Contains("%"))
                    {
                        indiceAnciennete.Remove(indiceAnciennete.IndexOf("%"));
                    }
                    double anciennete;
                    if (double.TryParse(indiceAnciennete, out  anciennete))
                    {
                    }
                    else
                    {
                        anciennete = 0;
                    }
                    var salaireGain = System.Math.Round(paiement.SalaireBase * (1 + anciennete / 100));
                    dataGridView1.Rows.Add(
                              paiement.NumeroMatricule, nomEmploye, 
                              String.Format(elGR, "{0:0,0}", paiement.SalaireBase),
                                    String.Format(elGR, "{0:0,0}", salaireGain),
                                    String.Format(elGR, "{0:0,0}", paiement.CongeAnnuel),
                                     String.Format(elGR, "{0:0,0}", paiement.CoutAbsence),
                                     String.Format(elGR, "{0:0,0}", paiement.SalaireBrut),
                                     String.Format(elGR, "{0:0,0}", paiement.CNPS),
                                    String.Format(elGR, "{0:0,0}", paiement.IRPP),
                                    paiement.ONASA,
                                    String.Format(elGR, "{0:0,0}", paiement.ChargePatronale),
                                      String.Format(elGR, "{0:0,0}", paiement.CNRT),
                                    String.Format(elGR, "{0:0,0}", paiement.CNRTEmploye),
                                    String.Format(elGR, "{0:0,0}", paiement.AvanceSurSalaire),
                                      String.Format(elGR, "{0:0,0}", paiement.AcomptePaye),
                                      String.Format(elGR, "{0:0,0}", paiement.ChargeSoinFamille),
                                  String.Format(elGR, "{0:0,0}", paiement.Indemnites),
                                  String.Format(elGR, "{0:0,0}", paiement.AutresPrimes),
                                  String.Format(elGR, "{0:0,0}", paiement.PrimeMotivation),
                                  String.Format(elGR, "{0:0,0}", paiement.FraisCommunication),
                                  String.Format(elGR, "{0:0,0}", paiement.Transport),
                                  String.Format(elGR, "{0:0,0}", paiement.SalaireNet),
                                String.Format(elGR, "{0:0,0}",paiement.CoutDuSalarie), paiement.ModePaiement,
                                   paiement.IDAcompte, paiement.Banque);


                }
                MontantTotal();
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("Remplissage des donnees", ex);
            }
        }
      
        private void button14_Click(object sender, EventArgs e)
        {
            if (ListePaiemenFrm.ShowBox() == "1")
            {
                txtExercice.Text = ListePaiemenFrm.exercice.ToString();
                cmbMois.Text = ListePaiemenFrm.mois;
                numeroPaiement = ListePaiemenFrm.numPaie;
                txtTotal.Text = ListePaiemenFrm.montant.ToString();
                RemplissageDesDonnees(numeroPaiement);
                MontantTotal();               
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    ////var numeroMatricule = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    if (GestionPharmacetique.MonMessageBox.ShowBox("Voulez vous retirer les données de paiement de " +
                        dataGridView1.SelectedRows[0].Cells[1].Value.ToString() + " de la liste?", "Confirmation") == "1")
                    {
                        ConnectionClass.SupprimerOrdreDePaiement(dataGridView1, numeroPaiement);
                        RemplissageDesDonnees(numeroPaiement);
                        MontantTotal();
                    }
                }
            }
            catch { }
        }

        private void txtNomSalarie_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.KeyCode == Keys.Enter)
                {
                    if (string.IsNullOrEmpty(cmbMois.Text))
                    {
                        GestionPharmacetique.MonMessageBox.ShowBox("Veuillez selectionner le mois sur la liste ci-dessous", "Erreur");
                        return;
                    }
                    if (numeroPaiement <= 0)
                    {
                        return;
                    }
                    if (Int32.TryParse(txtExercice.Text, out exercice))
                    {
                        ListePerso.siPersonnelProjet = false;
                        ListePerso.indexRecherche = txtNomSalarie.Text;
                        ListePerso.exercice = exercice;
                        ListePerso.mois = cmbMois.Text;
                        ListePerso.numeroPaiement = numeroPaiement;
                        ListePerso.state = "1";
                        if (ListePerso.ShowBox() == "1")
                        {
                            var paiement = ListePerso.paiement;
                            var indiceAnciennete = 1;
                            var salaireGain = System.Math.Round(paiement.SalaireBase * indiceAnciennete);
                            dataGridView1.Rows.Add(
                                      paiement.NumeroMatricule, ListePerso.nomPersonnel,
                                      String.Format(elGR, "{0:0,0}", paiement.SalaireBase),
                                            String.Format(elGR, "{0:0,0}", salaireGain),
                                            String.Format(elGR, "{0:0,0}", paiement.CongeAnnuel),
                                             String.Format(elGR, "{0:0,0}", paiement.CoutAbsence),
                                             String.Format(elGR, "{0:0,0}", paiement.SalaireBrut),
                                             String.Format(elGR, "{0:0,0}", paiement.CNPS),
                                            String.Format(elGR, "{0:0,0}", paiement.IRPP),
                                            paiement.ONASA,
                                            String.Format(elGR, "{0:0,0}", paiement.ChargePatronale),
                                          String.Format(elGR, "{0:0,0}", paiement.AvanceSurSalaire),
                                          String.Format(elGR, "{0:0,0}", paiement.AcomptePaye),
                                          String.Format(elGR, "{0:0,0}", paiement.ChargeSoinFamille),
                                          String.Format(elGR, "{0:0,0}", paiement.Indemnites),
                                          String.Format(elGR, "{0:0,0}", paiement.AutresPrimes),
                                          String.Format(elGR, "{0:0,0}", paiement.PrimeMotivation),
                                          String.Format(elGR, "{0:0,0}", paiement.FraisCommunication),
                                          String.Format(elGR, "{0:0,0}", paiement.Transport),
                                          String.Format(elGR, "{0:0,0}", paiement.SalaireNet),
                                          String.Format(elGR, "{0:0,0}", paiement.CoutDuSalarie),
                                          paiement.ModePaiement,
                                           paiement.IDAcompte,paiement.Banque);
                            txtNomSalarie.Text = "";
                            MontantTotal();
                        }
                    }
                    else
                    {
                        GestionPharmacetique.MonMessageBox.ShowBox("Veuillez entrer un chiffre valide pour l'année d'exercice", "Erreur");
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            var frm = new ListeAccompteFrm();
            groupBox2.Visible = false;
            frm.Location = new Point(Location.X, Location.Y);
            frm.Size = new Size(Width , Height );
            frm.ShowDialog();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var frm = new ListeAvanceSurSalaireFrm();
            groupBox2.Visible = false;
            frm.Location = new Point(Location.X, Location.Y);
            frm.Size = new Size(Width, Height);
            frm.ShowDialog();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            Dispose();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var frm = new DetailsPaiementFrm();
            frm.numeroPaiement = numeroPaiement;
            frm.moisEtat = cmbMois.Text;
            frm.exercice = exercice;
            if (numeroPaiement > 0)
            {
                frm.Location = new Point(Location.X, Location.Y);
                frm.Size = new Size(Width, Height);
                frm.ShowDialog();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                    var  listePaiement = new List<Paiement>();
                for(var i = 0; i< dataGridView1.Rows.Count;i++)
                    {
                        var paiement = new Paiement();
                        paiement.NumeroMatricule = dataGridView1.Rows[i].Cells[0].Value.ToString();
                        paiement.SalaireBase = Double.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString());
                     paiement.Employe = dataGridView1.Rows[i].Cells[1].Value.ToString();
                        paiement.GainSalarial = Double.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
                        paiement.CongeAnnuel = Double.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString());
                        paiement.CoutAbsence = Double.Parse(dataGridView1.Rows[i].Cells[5].Value.ToString());
                        paiement.SalaireBrut = Double.Parse(dataGridView1.Rows[i].Cells[6].Value.ToString());
                        paiement.CNPS = Double.Parse(dataGridView1.Rows[i].Cells[7].Value.ToString());
                        paiement.IRPP = Double.Parse(dataGridView1.Rows[i].Cells[8].Value.ToString());
                        paiement.ONASA = Double.Parse(dataGridView1.Rows[i].Cells[9].Value.ToString());
                        paiement.ChargePatronale = Double.Parse(dataGridView1.Rows[i].Cells[10].Value.ToString());
                        paiement.AvanceSurSalaire = Double.Parse(dataGridView1.Rows[i].Cells[12].Value.ToString());
                        paiement.AcomptePaye = Double.Parse(dataGridView1.Rows[i].Cells[11].Value.ToString());
                        paiement.ChargeSoinFamille = Double.Parse(dataGridView1.Rows[i].Cells[13].Value.ToString());
                        paiement.Indemnites = Double.Parse(dataGridView1.Rows[i].Cells[14].Value.ToString());
                        paiement.AutresPrimes = Double.Parse(dataGridView1.Rows[i].Cells[15].Value.ToString());
                        paiement.PrimeMotivation = Double.Parse(dataGridView1.Rows[i].Cells[16].Value.ToString());
                        paiement.FraisCommunication = Double.Parse(dataGridView1.Rows[i].Cells[17].Value.ToString());
                        paiement.Transport = Double.Parse(dataGridView1.Rows[i].Cells[18].Value.ToString());
                        paiement.SalaireNet = Double.Parse(dataGridView1.Rows[i].Cells[19].Value.ToString());
                        paiement.ModePaiement = dataGridView1.Rows[i].Cells[21].Value.ToString();
                        paiement.IDAcompte = Int32.Parse(dataGridView1.Rows[i].Cells[22].Value.ToString());
                            paiement.CoutDuSalarie = Double.Parse(dataGridView1.Rows[i].Cells[20].Value.ToString());
                        listePaiement.Add(paiement);
                    }
                  
                dataGridView1.Rows.Clear();
                var p = from l in listePaiement
                        where l.Employe.StartsWith(txtNomSalarie.Text, StringComparison.CurrentCultureIgnoreCase)
                        orderby l.Employe
                        select l;
                    foreach(var paiement in p)
                    dataGridView1.Rows.Add(
                              paiement.NumeroMatricule, paiement.Employe,
                              String.Format(elGR, "{0:0,0}", paiement.SalaireBase),
                                    String.Format(elGR, "{0:0,0}", paiement.GainSalarial),
                                    String.Format(elGR, "{0:0,0}", paiement.CongeAnnuel),
                                     String.Format(elGR, "{0:0,0}", paiement.CoutAbsence),
                                     String.Format(elGR, "{0:0,0}", paiement.SalaireBrut),
                                     String.Format(elGR, "{0:0,0}", paiement.CNPS),
                                    String.Format(elGR, "{0:0,0}", paiement.IRPP),
                                    paiement.ONASA,
                                    String.Format(elGR, "{0:0,0}", paiement.ChargePatronale),
                                  String.Format(elGR, "{0:0,0}", paiement.AvanceSurSalaire),
                                  String.Format(elGR, "{0:0,0}", paiement.AcomptePaye),
                                  String.Format(elGR, "{0:0,0}", paiement.ChargeSoinFamille),
                                  String.Format(elGR, "{0:0,0}", paiement.Indemnites),
                                  String.Format(elGR, "{0:0,0}", paiement.AutresPrimes),
                                  String.Format(elGR, "{0:0,0}", paiement.PrimeMotivation),
                                  String.Format(elGR, "{0:0,0}", paiement.FraisCommunication),
                                  String.Format(elGR, "{0:0,0}", paiement.Transport),
                                  String.Format(elGR, "{0:0,0}", paiement.SalaireNet),
                                  String.Format(elGR, "{0:0,0}", paiement.CoutDuSalarie),
                                  paiement.ModePaiement,
                                   paiement.IDAcompte);
                    txtNomSalarie.Text = "";
                    MontantTotal();
                
            }
            catch (Exception)
            {
            }
        }

        private void btnModifierPersonnel_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                PaiementForme.etatModifier = "1";
                PaiementForme.numeroPaiement = numeroPaiement;
                PaiementForme.numMatricule = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                if(PaiementForme.ShowBox())
                {
                   var  paiement = PaiementForme.paiement;
                    dataGridView1.SelectedRows[0].Cells[4].Value = String.Format(elGR, "{0:0,0}", paiement.CongeAnnuel);
                    dataGridView1.SelectedRows[0].Cells[5].Value = String.Format(elGR, "{0:0,0}", paiement.CoutAbsence);
                    dataGridView1.SelectedRows[0].Cells[6].Value = String.Format(elGR, "{0:0,0}", paiement.SalaireBrut);
                    dataGridView1.SelectedRows[0].Cells[7].Value = String.Format(elGR, "{0:0,0}", paiement.CNPS);
                    dataGridView1.SelectedRows[0].Cells[8].Value = String.Format(elGR, "{0:0,0}", paiement.IRPP);
                    dataGridView1.SelectedRows[0].Cells[9].Value = String.Format(elGR, "{0:0,0}", paiement.ONASA);
                    dataGridView1.SelectedRows[0].Cells[10].Value = String.Format(elGR, "{0:0,0}", paiement.ChargePatronale);
                    dataGridView1.SelectedRows[0].Cells[11].Value = String.Format(elGR, "{0:0,0}", paiement.CNRT);
                    dataGridView1.SelectedRows[0].Cells[12].Value = String.Format(elGR, "{0:0,0}", paiement.CNRTEmploye);
                    dataGridView1.SelectedRows[0].Cells[13].Value = String.Format(elGR, "{0:0,0}", paiement.AcomptePaye);
                    dataGridView1.SelectedRows[0].Cells[14].Value = String.Format(elGR, "{0:0,0}", paiement.AvanceSurSalaire);
                    dataGridView1.SelectedRows[0].Cells[15].Value = String.Format(elGR, "{0:0,0}", paiement.ChargeSoinFamille);
                    dataGridView1.SelectedRows[0].Cells[16].Value = String.Format(elGR, "{0:0,0}", paiement.Indemnites);
                    dataGridView1.SelectedRows[0].Cells[17].Value = String.Format(elGR, "{0:0,0}", paiement.AutresPrimes);
                    dataGridView1.SelectedRows[0].Cells[18].Value = String.Format(elGR, "{0:0,0}", paiement.PrimeMotivation);
                    dataGridView1.SelectedRows[0].Cells[19].Value = String.Format(elGR, "{0:0,0}", paiement.FraisCommunication);
                    dataGridView1.SelectedRows[0].Cells[20].Value = String.Format(elGR, "{0:0,0}", paiement.Transport);
                    dataGridView1.SelectedRows[0].Cells[21].Value = String.Format(elGR, "{0:0,0}", paiement.SalaireNet);
                    dataGridView1.SelectedRows[0].Cells[22].Value = String.Format(elGR, "{0:0,0}", paiement.CoutDuSalarie);
                    dataGridView1.SelectedRows[0].Cells[23].Value = paiement.ModePaiement;
                    dataGridView1.SelectedRows[0].Cells[25].Value = paiement.Banque;

                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var frm = new RecapSalarialFrm();
            frm.numeroPaiement = numeroPaiement;
            frm.moinPaiement = mois;
            frm.exercice = exercice;
            if (numeroPaiement > 0)
                frm.ShowDialog();
        }

    }
}
