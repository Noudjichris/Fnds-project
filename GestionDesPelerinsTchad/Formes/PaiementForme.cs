using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SGSP.Formes
{
    public partial class PaiementForme : Form
    {
        public PaiementForme()
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
        private void groupBox6_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.WhiteSmoke, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox6.Width - 1, this.groupBox6.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.AliceBlue, Color.AliceBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        System.Globalization.CultureInfo elGR = System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR");
        private void RemplissageDesChamps()
        {
            try
            {
                var paiement = AppCode.ConnectionClass.PaiementParMatricule(numeroPaiement, numMatricule);

                if (paiement != null)
                {
                    var personnel = AppCode.ConnectionClass.ListePersonnelParMatricule(paiement.NumeroMatricule);
                    var nomEmploye = personnel[0].Nom + " " + personnel[0].Prenom;
                    var service = AppCode.ConnectionClass.ListeServicePersonnel(paiement.NumeroMatricule);
                    var indiceAnciennete = service[0].Anciennete;

                    txtSalaireBase.Text = String.Format(elGR, "{0:0,0}", paiement.SalaireBase);
                    lblSalaireBrut.Text = String.Format(elGR, "{0:0,0}", paiement.SalaireBrut);
                    txtNomPersonnel.Text = nomEmploye;

                    var anciennete = 1;

                    var salaireGain = paiement.SalaireBase + paiement.GainAnciennete;
                    tauxConge = salaireGain / 24;
                    tauxAbscense = salaireGain / 24;
                    lblTauxAbscense.Text = Math.Round(tauxAbscense).ToString();
                    lblTauxConges.Text = Math.Round(tauxConge).ToString();
                    if (paiement.CongeAnnuel > 0)
                        txtNbreJourConge.Text = "24";
                    txtCategorie.Text = anciennete.ToString();
                    txtGainAnciennete.Text = String.Format(elGR, "{0:0,0}", paiement.GainAnciennete);
                    lblMontantConge.Text = String.Format(elGR, "{0:0,0}", paiement.CongeAnnuel);
                    lblMontantAbscenses.Text = String.Format(elGR, "{0:0,0}", paiement.CoutAbsence);
                    lblTotalCnps.Text = String.Format(elGR, "{0:0,0}", paiement.CNPS);
                    lblMontantCNRTEmploye.Text = String.Format(elGR, "{0:0,0}", paiement.CNRT);
                    lblMontantCNRTEmployeur.Text = String.Format(elGR, "{0:0,0}", paiement.CNRTEmploye);
                    txtTauxCNRTEmploye.Text = "5";
                    txtTauxCNRTEmployeur.Text = "12";
                    txtBaseCRNT.Text = Math.Round(paiement.CNRT * 5 / 100).ToString();
                    var cnps = .0;
                    var cnpsCsdn = .0;
                    if (paiement.CNPS > 0)
                    {
                        lblBaseCNPS.Text = String.Format(elGR, "{0:0,0}", paiement.SalaireBrut);
                        label51.Text = String.Format(elGR, "{0:0,0}", paiement.SalaireBrut);
                        cnps = 3.5;
                        cnpsCsdn = 16.5;
                    }
                    txtCNPSCSND.Text = cnpsCsdn.ToString();
                    txttauxCNPS.Text = cnps.ToString();
                    lblTotalIrpp.Text = String.Format(elGR, "{0:0,0}", paiement.IRPP);
                        lblBaseIRPP.Text = String.Format(elGR, "{0:0,0}", paiement.SalaireBrut - paiement.CNPS-paiement.CNRT);
                    
                    txttauxIRPP.Text = "NB";
                    txtOnasa.Text = String.Format(elGR, "{0:0,0}", paiement.ONASA);
                    lblTotalIrpp.Text = String.Format(elGR, "{0:0,0}", paiement.IRPP);
                    lblChargePat.Text = String.Format(elGR, "{0:0,0}", paiement.ChargePatronale);
                    var totalCharge =paiement.CNRT+ paiement.ONASA + paiement.IRPP + paiement.CNPS;
                    lblTotalCharges.Text = String.Format(elGR, "{0:0,0}", totalCharge);
                    txtAvanceSurSalaaire.Text = String.Format(elGR, "{0:0,0}", paiement.AvanceSurSalaire);
                    txtSoinFamille.Text = String.Format(elGR, "{0:0,0}", paiement.ChargeSoinFamille);
                    txtAcomptePayer.Text = String.Format(elGR, "{0:0,0}", paiement.AcomptePaye);
                    cmbTypeContrat.Text = paiement.Service;
                    var listeAcompte = AppCode.ConnectionClass.ListeDesAccompte(paiement.IDAcompte, numMatricule, paiement.AcomptePaye);
                    if (listeAcompte.Count() > 0)
                    {
                        txtAccounptPaye.Text = String.Format(elGR, "{0:0,0}", listeAcompte[0].Rembourser);
                        txtAcompte.Text = String.Format(elGR, "{0:0,0}", listeAcompte[0].MontantAcompte);

                    }
                    else
                    {
                        txtAccounptPaye.Text = "0";
                        txtAcompte.Text = "0";
                    }
                    //String.Format(elGR, "{0:0,0}", paiement.ChargeSoinFamille);
                    txtIndemnites.Text = String.Format(elGR, "{0:0,0}", paiement.Indemnites);
                    txtAutresPrimes.Text = String.Format(elGR, "{0:0,0}", paiement.AutresPrimes);
                    txtPrimeMotivation.Text = String.Format(elGR, "{0:0,0}", paiement.PrimeMotivation);
                    txtFraisComm.Text = String.Format(elGR, "{0:0,0}", paiement.FraisCommunication);
                    txtTransport.Text = String.Format(elGR, "{0:0,0}", paiement.Transport);
                    lblTotalPrimes.Text = String.Format(elGR, "{0:0,0}", paiement.AutresPrimes + paiement.Indemnites + paiement.PrimeMotivation + paiement.Transport + paiement.FraisCommunication);
                    txtNetPayer.Text = String.Format(elGR, "{0:0,0}", paiement.SalaireNet);
                    lblCoutSalarial.Text = String.Format(elGR, "{0:0,0}", paiement.CoutDuSalarie);
                    if (paiement.ModePaiement == "Paiement en espèces")
                        rdbEspeces.Checked = true;
                    else if (paiement.ModePaiement == "Paiement par chèques")
                        rdbCheques.Checked = true;
                    else if (paiement.ModePaiement == "Virement bancaire")
                        rdbBancaire.Checked = true;
                    ancienCoutSalarial = paiement.CoutDuSalarie;
                    ancienAcomptePaye = paiement.AcomptePaye;
                    idAccompe = paiement.IDAcompte;
                    cmbBanque.Text = paiement.Banque;
                    txtCompte.Text = paiement.Compte;
                }
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("Remplissage des donnees", ex);
            }
        }

        private void PaiementForme_Load(object sender, EventArgs e)
        {
            try
            {
                Location = new Point(0, 0);

                foreach (var contrat in AppCode.ConnectionClass.ListeContrat())
                {
                    cmbTypeContrat.Items.Add(contrat.TypeContrat);
                }
                cmbBanque.Items.Add("");
                foreach (var bank in AppCode.ConnectionClass.ListeDonneesBancaires(numMatricule))
                {
                    cmbBanque.Items.Add(bank.NomBanque);
                    if (bank.EtatParDefaut)
                    {
                        txtCompte.Text = bank.Compte;
                        cmbBanque.Text = bank.NomBanque;
                    }
                }
                cmbTypeContrat.Text = typeContrat;
                if (etatModifier == "1")
                {
                    RemplissageDesChamps();
                    changerAuto = false;
                }
                else
                {
                    changerAuto = true;
                    var cnps = .0;
                    var cnpsCsdn = .0;
                    var onasa = .0;
                    var irpp = "NB";
                    var cnrtEmploye = .0;
                    var cnrtEmployeur = .0;
                    var moisDeService = 0;
                    if (typeContrat.ToUpper().Contains("CONTRACTUEL".ToUpper()))
                    {
                        cnps = 3.5;
                        cnpsCsdn = 16.5;

                        txtBaseCRNT.Text = "0";
                        moisDeService = (DateTime.Now.Date.Subtract(datePriseService).Days / 30);
                        textBox1.Text = moisDeService.ToString();
                    }
                    else if (siCNRT)
                    {
                        cnps = 0;
                        cnpsCsdn = 11.5;
                        cnrtEmploye = 5.0;
                        cnrtEmployeur = 12.0;
                        if (fonction == "Directeur Général")
                        {
                            txtBaseCRNT.Text = "224250";
                        }
                        else if (fonction == "Directrice Générale Adjointe")
                        {
                            txtBaseCRNT.Text = "138000";
                        }
                        else
                        {
                            txtBaseCRNT.Text = "0";
                        }
                    }
                    else
                    {
                        cnps = 0;
                        cnpsCsdn = 0;
                        txtBaseCRNT.Text = "0";
                    }

                    lblBaseIRPP.Text = "0";
                    txtTauxCNRTEmploye.Text = cnrtEmploye.ToString();
                    txtTauxCNRTEmployeur.Text = cnrtEmployeur.ToString();
                    txttauxCNPS.Text = cnps.ToString(); ;
                    txtCNPSCSND.Text = cnpsCsdn.ToString();
                    ViderLesControles();
                    txtNomPersonnel.Text = nomEmploye;
                    txtMatricule.Text = numMatricule;
                    txtFonction.Text = fonction;
                    //Height = MainForm.height1;
                    //Location = new Point((MainForm.width - Width) / 2, 5);
                    if (!string.IsNullOrWhiteSpace(numeroCompte))
                    {
                        if (GestionPharmacetique.MonMessageBox.ShowBox("Ce salarié possède un compte bancaire, voulez vous proceder par un mode de paiement par virement bancaire?", "Confirmation") == "1")
                        {
                            rdbBancaire.Checked = true;
                        }
                        else
                        {
                            rdbEspeces.Checked = true;
                        }
                    }
                    else
                    {
                        rdbEspeces.Checked = true;
                    }

                    cmbBanque.Text = banque;
                    var anciennete = .0;
                    if (typeContrat.ToUpper().Contains("CONTRACTUEL"))
                    {
                        anciennete = 1;
                    }
                    txtCategorie.Text = anciennete.ToString();
                    var salaire = AppCode.ConnectionClass.ListeSalaire(numMatricule);
                    var dtAccompte = AppCode.ConnectionClass.ListeAccompte(numMatricule);

                    var avanceSurSalaire = 0.0;

                    var liste = AppCode.ConnectionClass.ListeAvanceSurSalaire(exercice, mois, numMatricule);
                    if (liste != null)
                    {
                        var montantTotal = 0.0;
                        foreach (var p in liste)
                        {
                            montantTotal += p.AvanceSurSalaire;
                        }
                        avanceSurSalaire = montantTotal;
                        txtAvanceSurSalaaire.Text = montantTotal.ToString();

                    }
                    else
                    {
                        avanceSurSalaire = 0.0;
                        txtAvanceSurSalaaire.Text = "";
                    }

                    txtSalaireBase.Text = salaire[0].SalaireBase.ToString();
                    txtAutresPrimes.Text = salaire[0].AutresPrimes.ToString();
                    txtIndemnites.Text = salaire[0].Indemnites.ToString();
                    txtPrimeMotivation.Text = salaire[0].PrimeMotivation.ToString();
                    txtTransport.Text = salaire[0].PrimeTransport.ToString();
                    txtFraisComm.Text = salaire[0].FraisCommunication.ToString();

                    Double remboursement, accompte, aPayer;
                    if (dtAccompte.Rows.Count > 0)
                    {
                        if (Double.TryParse(dtAccompte.Rows[0].ItemArray[2].ToString(), out accompte) &&
                            Double.TryParse(dtAccompte.Rows[0].ItemArray[3].ToString(), out remboursement) &&
                            Double.TryParse(dtAccompte.Rows[0].ItemArray[4].ToString(), out aPayer))
                        {
                            idAccompe = Convert.ToInt32(dtAccompte.Rows[0].ItemArray[0].ToString());
                            if (remboursement >= accompte)
                            {
                                txtAcomptePayer.Text = "0";
                                txtAcompte.Text = "0";
                                txtAcomptePayer.Text = "0";
                                aPayer = 0.0;
                            }
                            else
                            {
                                txtAcompte.Text = accompte.ToString();
                                txtAccounptPaye.Text = remboursement.ToString();
                                txtAcomptePayer.Text = aPayer.ToString();
                                var reste = accompte - remboursement;
                            }
                        }
                        else
                        {
                            idAccompe = 0;
                            accompte = remboursement = aPayer = 0.0;
                            txtAcomptePayer.Text = "0";
                            txtAcompte.Text = "0";
                        }

                    }
                    else
                    {

                        aPayer = 0.0;
                        txtAcomptePayer.Text = "0";
                        txtAcompte.Text = "0";
                    }

                    //var salaireGain = System.Math.Round(salaireDeBase * anciennete);
                    salaireDeBase = salaire[0].SalaireBase;
                    tauxConge = salaireDeBase / 24;
                    tauxAbscense = salaireDeBase / 26;
                    lblTauxAbscense.Text = Math.Round(tauxAbscense).ToString();
                    lblTauxConges.Text = Math.Round(tauxConge).ToString();

                    if (double.TryParse(txtCNPSCSND.Text, out cnpsCsdn))
                    {
                    }
                    else if (txtCNPSCSND.Text.Contains(","))
                    {
                        txtCNPSCSND.Text.Replace(",", ".");
                        {
                            if (double.TryParse(txtCNPSCSND.Text, out cnpsCsdn))
                            {
                            }
                            else
                            {
                                cnpsCsdn = 0;
                                txtCNPSCSND.BackColor = Color.Red;
                                return;
                            }
                        }
                    }
                    else
                    {
                        cnpsCsdn = 0;
                        txtCNPSCSND.BackColor = Color.Red; return;
                    }

                    if (double.TryParse(txttauxCNPS.Text, out cnps))
                    {
                    }
                    else if (txttauxCNPS.Text.Contains(","))
                    {
                        txttauxCNPS.Text.Replace(",", ".");
                        {
                            if (double.TryParse(txttauxCNPS.Text, out cnps))
                            {
                            }
                            else
                            {
                                txttauxCNPS.BackColor = Color.Red;
                                cnps = 0; return;
                            }
                        }
                    }
                    else
                    {
                        txttauxCNPS.BackColor = Color.Red;
                        cnps = 0; return;
                    }

                    if (double.TryParse(txtOnasa.Text, out onasa))
                    {
                    }
                    else
                    {
                        onasa = 0;
                        txtOnasa.BackColor = Color.Red; return;
                    }


                    totalCnps = System.Math.Round(salaireDeBase * cnps / 100);
                    double baseCNRT = .0;
                    if (double.TryParse(txtBaseCRNT.Text, out baseCNRT))
                    {
                    }

                    lblMontantCNRTEmploye.Text = (baseCNRT * cnrtEmploye / 100).ToString();
                    lblMontantCNRTEmployeur.Text = (baseCNRT * cnrtEmployeur / 100).ToString();
                    totalCnpsCndn = System.Math.Round(salaireDeBase * cnpsCsdn / 100);
                    if (salaireDeBase > 500000)
                    {
                        totalCnps = System.Math.Round(500000 * cnps / 100);
                        totalCnpsCndn = System.Math.Round(500000 * cnpsCsdn / 100);
                    }

                    var tauxIrrp = .0;

                    var salaireImposable = (salaireDeBase - totalCnps) * 12;

                    if (typeContrat.ToUpper().Contains("CONTRACTUEL"))
                    {
                        totalIRPP = AppCode.GlobalVariable.IRPP(salaireImposable, salaireBrut, totalCnps, 0);
                        onasa = AppCode.GlobalVariable.ONASA(salaireImposable, 0);
                    }
                    else if (siCNRT)
                    {
                        totalIRPP = AppCode.GlobalVariable.IRPP(salaireImposable, salaireBrut, (baseCNRT * cnrtEmploye / 100), 0);
                        onasa = AppCode.GlobalVariable.ONASA(salaireImposable, 0);
                    }
                    else
                    {
                        totalIRPP = 0; onasa = 0;
                    }

                    txtOnasa.Text = onasa.ToString();
                    txttauxIRPP.Text = tauxIrrp.ToString();
                    lblChargePat.Text = totalCnpsCndn.ToString();
                    lblTotalCnps.Text = totalCnps.ToString();
                    lblTotalIrpp.Text = totalIRPP.ToString();
                    var totalCharge = totalCnps + totalIRPP + onasa;
                    lblTotalCharges.Text = totalCharge.ToString();
                    salaireBrut = salaireDeBase;
                    lblSalaireBrut.Text = salaireBrut.ToString();
                    netAPayer = salaireBrut - aPayer - avanceSurSalaire;
                    //lblBaseCNPS.Text = "";
                    txtGainAnciennete.Text = Math.Round(salaireDeBase * anciennete / 100).ToString();
                    txtCategorie.Text = anciennete.ToString();
                    txtNetPayer.Text = netAPayer.ToString();
                    txtAvanceSurSalaaire.Text = avanceSurSalaire.ToString();

                    NetAPayer();
                }
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("form load", ex);
            }
        }

        #region
        public static string mois, ancienneteDuPersonnel, nomEmploye, banque, numMatricule, numeroCompte, fonction, typeContrat, etatModifier = "0";
        double netAPayer, totalCnps, totalCnpsCndn, ancienCoutSalarial, ancienAcomptePaye,
            totalIRPP, salaireDeBase, salaireBrut, transport, congeAnnuel, tauxConge, tauxAbscense;
        public static bool siCNRT;
        private void cmbBanque_SelectedIndexChanged(object sender, EventArgs e)
        {
            var comptes = from bank in AppCode.ConnectionClass.ListeDonneesBancaires(numMatricule)
                          where bank.NomBanque == cmbBanque.Text
                          select bank.Compte;
            foreach (var compte in comptes)
            {
                txtCompte.Text = compte;
            }
        }

        private void PaiementForme_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue, 1); Rectangle area1 = new Rectangle(0, 0, Width - 1, Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.AliceBlue, Color.AliceBlue, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void txtNbreJourConge_MouseEnter(object sender, EventArgs e)
        {
            changerAuto = true;
        }

        public static int idAccompe, numeroPaiement, exercice;
        public static DateTime datePriseService;
        public static AppCode.Paiement paiement;
        public static PaiementForme frm;
        static bool flag, changerAuto;

        #endregion

        public static bool ShowBox()
        {
            try
            {
                frm = new PaiementForme();
                frm.ShowDialog();
                return flag;
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("", ex);
                return false;
            }
        }


        private void NetAPayer()
        {
            try
            {
                if (changerAuto)
                {
                    
                    double priseEnChargeFamille, totalAbsence, totalConges, primeTransport, primeRende, primeGarde, primePerfect, heureSupp, acompte, avanceSurSalaire;


                    if (!string.IsNullOrEmpty(txtSalaireBase.Text))
                    {
                        salaireDeBase = Convert.ToDouble(txtSalaireBase.Text);
                    }
                    else
                    {
                        txtSalaireBase.BackColor = Color.Red;
                        txtGainAnciennete.BackColor = Color.Red;
                        lblSalaireBrut.Text = "";
                        txtGainAnciennete.Text = "";
                        txtNetPayer.Text = "";
                        return;
                    }

                    double cat = .0;
                        txtGainAnciennete.Text = "0";

                    #region CongesAbscence
                    if (!string.IsNullOrEmpty(txtNbreJourConge.Text))
                    {
                        if (Double.TryParse(txtNbreJourConge.Text, out totalConges))
                        {
                            totalConges = totalConges * Math.Round(tauxConge);
                            lblMontantConge.Text = totalConges.ToString();
                        }
                        else
                        {
                            txtNbreJourConge.BackColor = Color.Red;
                            txtNbreJourConge.Focus();
                            return;
                        }
                    }
                    else
                    {
                        totalConges = 0;
                        lblMontantConge.Text = "";
                    }

                    if (!string.IsNullOrEmpty(txtNbreJourAbscente.Text))
                    {
                        if (Double.TryParse(txtNbreJourAbscente.Text, out totalAbsence))
                        {
                            totalAbsence = totalAbsence * Math.Round(tauxAbscense);
                            lblMontantAbscenses.Text = totalAbsence.ToString();
                        }
                        else
                        {
                            txtNbreJourAbscente.BackColor = Color.Red;
                            txtNbreJourAbscente.Focus();
                            return;
                        }
                    }
                    else
                    {
                        lblMontantAbscenses.Text = "";
                        totalAbsence = 0;
                    }

                    #endregion

                    #region ChargeDuPersonnel
                    double cnpsCsdn, cnps, onasa, cnrtEmploye, cnrtEmployeur;
                    string irpp = "NB";
                    if (double.TryParse(txtCNPSCSND.Text, out cnpsCsdn))
                    {
                    }
                    else if (txtCNPSCSND.Text.Contains(","))
                    {
                        txtCNPSCSND.Text.Replace(",", ".");
                        {
                            if (double.TryParse(txtCNPSCSND.Text, out cnpsCsdn))
                            {
                            }
                            else
                            {
                                txtCNPSCSND.BackColor = Color.Red;
                                cnpsCsdn = 0;
                            }
                        }
                    }
                    else
                    {
                        txtCNPSCSND.BackColor = Color.Red;
                        cnpsCsdn = 0;
                    }

                    if (double.TryParse(txttauxCNPS.Text, out cnps))
                    {
                    }
                    else if (txttauxCNPS.Text.Contains(","))
                    {
                        txttauxCNPS.Text.Replace(",", ".");
                        {
                            if (double.TryParse(txttauxCNPS.Text, out cnps))
                            {
                            }
                            else
                            {
                                txttauxCNPS.BackColor = Color.Red;
                                cnps = 0;
                            }
                        }
                    }
                    else
                    {
                        txttauxCNPS.BackColor = Color.Red;
                        cnps = 0;
                    }



                    if (double.TryParse(txtTauxCNRTEmploye.Text, out cnrtEmploye))
                    {
                    }
                    else if (txtTauxCNRTEmploye.Text.Contains(","))
                    {
                        txtTauxCNRTEmploye.Text.Replace(",", ".");
                        {
                            if (double.TryParse(txtTauxCNRTEmploye.Text, out cnrtEmploye))
                            {
                            }
                            else
                            {
                                txtTauxCNRTEmploye.BackColor = Color.Red;
                                cnrtEmploye = 0;
                            }
                        }
                    }
                    else
                    {
                        txtTauxCNRTEmploye.BackColor = Color.Red;
                        cnrtEmploye = 0;
                    }

                    if (double.TryParse(txtOnasa.Text, out onasa))
                    {
                    }
                    else
                    {
                        onasa = 0;
                        txtOnasa.BackColor = Color.Red;
                    }

                    if (double.TryParse(txtTauxCNRTEmployeur.Text, out cnrtEmployeur))
                    {
                    }
                    else if (txtTauxCNRTEmployeur.Text.Contains(","))
                    {
                        txtTauxCNRTEmployeur.Text.Replace(",", ".");
                        {
                            if (double.TryParse(txtTauxCNRTEmployeur.Text, out cnrtEmployeur))
                            {
                            }
                            else
                            {
                                txtTauxCNRTEmployeur.BackColor = Color.Red;
                                cnrtEmployeur = 0;
                            }
                        }
                    }
                    else
                    {
                        txtTauxCNRTEmployeur.BackColor = Color.Red;
                        cnrtEmployeur = 0;
                    }
                    double baseCNRT;
                    if (double.TryParse(txtBaseCRNT.Text, out baseCNRT))
                    {
                    }
                    else if (txtBaseCRNT.Text.Contains(","))
                    {
                        txtBaseCRNT.Text.Replace(",", ".");
                        {
                            if (double.TryParse(txtBaseCRNT.Text, out baseCNRT))
                            {
                            }
                            else
                            {
                                txtBaseCRNT.BackColor = Color.Red;
                                baseCNRT = 0;
                            }
                        }
                    }
                    else
                    {
                        txtBaseCRNT.BackColor = Color.Red;
                        baseCNRT = 0;
                    }

                    if (double.TryParse(txtOnasa.Text, out onasa))
                    {
                    }
                    else
                    {
                        onasa = 0;
                        txtOnasa.BackColor = Color.Red;
                    }
                    salaireBrut = salaireDeBase + double.Parse(txtGainAnciennete.Text) + totalConges - totalAbsence;
                    lblSalaireBrut.Text = salaireBrut.ToString();



                    totalCnps = System.Math.Round(salaireBrut * cnps / 100);
                    totalCnpsCndn = System.Math.Round(salaireBrut * cnpsCsdn / 100);
                    if (salaireBrut > 500000)
                    {
                        totalCnps = System.Math.Round(500000 * cnps / 100);
                        totalCnpsCndn = System.Math.Round(500000 * cnpsCsdn / 100);
                    }
                    var totalCNRTEmploye = System.Math.Round(baseCNRT * cnrtEmploye / 100);
                    var totalCNRTEmployeur = System.Math.Round(baseCNRT * cnrtEmployeur / 100);

                    var salaireImposable = (double.Parse(txtSalaireBase.Text) + double.Parse(txtGainAnciennete.Text) - totalCnps) * 12;
                    if (typeContrat.ToUpper().Contains("CONTRACTUEL"))
                    {
                        totalIRPP = AppCode.GlobalVariable.IRPP(salaireImposable, salaireBrut, totalCnps, totalConges);
                        onasa = AppCode.GlobalVariable.ONASA(salaireImposable, totalConges);
                    }
                    else if (siCNRT)
                    {
                        totalIRPP = AppCode.GlobalVariable.IRPP(salaireImposable, salaireBrut, totalCNRTEmploye, totalConges);
                        onasa = AppCode.GlobalVariable.ONASA(salaireImposable, totalConges);
                    }
                    else
                    {
                        totalIRPP = 0; onasa = 0;
                    }

                    lblBaseCNPS.Text = (salaireBrut + congeAnnuel - totalAbsence).ToString();
                    label51.Text = (salaireBrut + congeAnnuel + totalAbsence - totalCnps).ToString();
                    if (salaireBrut > 500000)
                    {
                        if (congeAnnuel > 500000)
                            congeAnnuel = 500000.0;
                        lblBaseCNPS.Text = (500000 + congeAnnuel  - totalAbsence).ToString();
                        label51.Text = (500000 + congeAnnuel  - totalAbsence).ToString();
                    }
                    lblBaseIRPP.Text = (salaireBrut + congeAnnuel + totalAbsence - totalCnps-totalCNRTEmploye).ToString();
                    txttauxIRPP.Text = irpp.ToString();
                    txtOnasa.Text = onasa.ToString();
                    lblChargePat.Text = totalCnpsCndn.ToString();
                    lblTotalCnps.Text = totalCnps.ToString();
                    lblTotalIrpp.Text = totalIRPP.ToString();
                    lblMontantCNRTEmploye.Text = totalCNRTEmploye.ToString();
                    lblMontantCNRTEmployeur.Text = totalCNRTEmployeur.ToString();
                    var totalCharge = totalCnps + totalIRPP + onasa + totalCNRTEmploye;
                    lblTotalCharges.Text = totalCharge.ToString();
                    salaireBrut = salaireBrut - totalCharge;
                    #endregion

                    #region PrimesIndemnites

                    if (!string.IsNullOrEmpty(txtIndemnites.Text))
                    {
                        if (Double.TryParse(txtIndemnites.Text, out primeRende))
                        {

                        }
                        else
                        {
                            txtIndemnites.BackColor = Color.Red;
                            txtNetPayer.Text = "";
                            return;
                        }
                    }
                    else
                    {
                        primeRende = 0.0;
                    }

                    if (!string.IsNullOrEmpty(txtAutresPrimes.Text))
                    {
                        if (Double.TryParse(txtAutresPrimes.Text, out primeGarde))
                        {
                        }
                        else
                        {
                            txtAutresPrimes.BackColor = Color.Red;
                            txtNetPayer.Text = "";
                            return;
                        }
                    }
                    else
                    {
                        primeGarde = 0.0;
                    }

                    if (!string.IsNullOrEmpty(txtPrimeMotivation.Text))
                    {
                        if (Double.TryParse(txtPrimeMotivation.Text, out primePerfect))
                        {
                        }
                        else
                        {
                            txtPrimeMotivation.BackColor = Color.Red;
                            txtNetPayer.Text = "";
                            return;
                        }
                    }
                    else
                    {
                        primePerfect = 0.0;
                    }
                    //double jourPrestation, fraisPresatation;
                    if (!string.IsNullOrEmpty(txtFraisComm.Text))
                    {
                        if (Double.TryParse(txtFraisComm.Text, out heureSupp))
                        {
                        }
                        else
                        {
                            txtFraisComm.BackColor = Color.Red;
                            txtNetPayer.Text = "";
                            return;
                        }
                    }
                    else
                    {
                        heureSupp = .0;
                    }
                    //       if (Double.TryParse(txtTauxPrestation.Text, out fraisPresatation))
                    //    {
                    //    }
                    //    else
                    //    {
                    //        txtTauxPrestation.BackColor = Color.Red;
                    //        txtNetPayer.Text = "";
                    //        return;
                    //    }
                    //    heureSupp = fraisPresatation * jourPrestation;
                    //    txtHeurSupp.Text = heureSupp.ToString();
                    //}
                    //else
                    //{
                    //    heureSupp = 0.0;
                    //}

                    if (!string.IsNullOrEmpty(txtTransport.Text))
                    {
                        if (Double.TryParse(txtTransport.Text, out primeTransport))
                        {
                        }
                        else
                        {
                            txtTransport.BackColor = Color.Red;
                            txtNetPayer.Text = "";
                            return;
                        }
                    }
                    else
                    {
                        primeTransport = 0.0;
                    }

                    #endregion

                    #region Deductions

                    if (!string.IsNullOrEmpty(txtAcomptePayer.Text))
                    {
                        if (Double.TryParse(txtAcomptePayer.Text, out acompte))
                        {
                        }
                        else
                        {
                            txtAcomptePayer.BackColor = Color.Red;
                            txtNetPayer.Text = "";
                            return;
                        }
                    }
                    else
                    {
                        acompte = 0.0;
                    }
                    if (!string.IsNullOrEmpty(txtAvanceSurSalaaire.Text))
                    {
                        if (double.TryParse(txtAvanceSurSalaaire.Text, out avanceSurSalaire))
                        { }
                        else
                        {
                            txtAvanceSurSalaaire.BackColor = Color.Red;
                            txtAvanceSurSalaaire.Focus();
                            return;
                        }
                    }
                    else
                    {
                        return;
                    }

                    if (!string.IsNullOrEmpty(txtSoinFamille.Text))
                    {
                        if (Double.TryParse(txtSoinFamille.Text, out priseEnChargeFamille))
                        {
                        }
                        else
                        {
                            txtSoinFamille.BackColor = Color.Red;
                            txtNetPayer.Text = "";
                            return;
                        }
                    }
                    else
                    {
                        priseEnChargeFamille = 0.0;
                    }

                    #endregion

                    var totalPrime = primeGarde + primePerfect + primeRende + heureSupp + primeTransport;
                    //var totalDeduction = totalCnps + totalIRPP + onasa;
                    var totalDette = avanceSurSalaire + acompte + priseEnChargeFamille;
                    netAPayer = salaireBrut + totalPrime - totalDette;
                    lblTotalPrimes.Text = totalPrime.ToString();
                    lblTotalDuctuin.Text = totalDette.ToString();
                    var totalDeductif = totalDette + totalCharge;
                    lblTotalDeductif.Text = totalDeductif.ToString();
                    txtNetPayer.Text = netAPayer.ToString();
                    var coutSalarie = double.Parse(lblSalaireBrut.Text) + totalCnpsCndn + totalPrime + totalCNRTEmployeur;
                    lblCoutSalarial.Text = coutSalarie.ToString();
                }
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("Calcul net a payer", ex);
            }

        }

        private void txtGain_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            flag = false;
            Dispose();
        }

        private void CalulDuNet_TextChanged(object sender, EventArgs e)
        {

            NetAPayer();
            txtFraisComm.BackColor = txtAutresPrimes.BackColor = txtIndemnites.BackColor =
                txtSalaireBase.BackColor = txttauxCNPS.BackColor = txtNbreJourAbscente.BackColor = txtNbreJourConge.BackColor =
                txtPrimeMotivation.BackColor = txtTransport.BackColor = lblMontantConge.BackColor =
             lblCoutSalarial.BackColor = txtOnasa.BackColor = txtCNPSCSND.BackColor = txttauxIRPP.BackColor = txttauxCNPS.BackColor = txtGainAnciennete.BackColor = Color.AliceBlue;
            lblCoutSalarial.BackColor = lblSalaireBrut.BackColor = Color.SteelBlue;
            txtBaseCRNT.BackColor = txtTauxCNRTEmploye.BackColor = txtTauxCNRTEmployeur.BackColor = Color.AliceBlue;
        }

        private void CalulDuNetConge(object sender, EventArgs e)
        {

        }

        void ViderLesControles()
        {
            txtGainAnciennete.Text = "";
            txtCategorie.Text = "";
            txtAcomptePayer.Text = "";
            lblTotalIrpp.Text = "";
            lblMontantConge.Text = "";
            txtAcompte.Text = "";
            txtAcomptePayer.Text = "";
            txtAvanceSurSalaaire.Text = "";
            //cmbMois.Text = "";
            txtFraisComm.Text = "";
            txtNetPayer.Text = "";
            txtAutresPrimes.Text = "";
            txtPrimeMotivation.Text = "";
            txtIndemnites.Text = "";
            txtTransport.Text = "";
            lblTotalCnps.Text = "";
            lblChargePat.Text = "";
            txtSalaireBase.Text = "";
            lblSalaireBrut.Text = "";
        }

        AppCode.Paiement CreerLesDetailsDePaie()
        {
            try
            {
                double primeLogement, primeMotivation, provision, fraisComm, acompte, irpp, onasa, cnps, salaireBrut, soinFamille, avanceSurSalaire, coutAbscence;
                var p = new AppCode.Paiement();
                p.IDAcompte = idAccompe;

                //if (!string.IsNullOrEmpty(txtJourPrestation.Text))
                //{
                //    double jour;
                //    if (Double.TryParse(txtJourPrestation.Text, out jour))
                //    { p.JourPrestations = jour; }
                //    else
                //    {
                //        p.JourPrestations = 0;
                //    }

                //}
                //else
                //{
                //    p.JourPrestations = 0;
                //}
                if (!string.IsNullOrEmpty(txtGainAnciennete.Text))
                {
                    p.GainAnciennete = Double.Parse(txtGainAnciennete.Text);
                }
                else
                {
                    txtSalaireBase.BackColor = Color.Red;
                    return null;
                }

                if (!string.IsNullOrEmpty(txtSalaireBase.Text))
                {
                    p.SalaireBase = Double.Parse(txtSalaireBase.Text);
                }
                else
                {
                    txtSalaireBase.BackColor = Color.Red;
                    return null;
                }

                if (!string.IsNullOrEmpty(lblSalaireBrut.Text))
                {
                    p.SalaireBrut = Double.Parse(lblSalaireBrut.Text);
                }
                else
                {
                    lblSalaireBrut.BackColor = Color.Red;
                    return null;
                }

                if (!string.IsNullOrEmpty(numMatricule))
                {
                    p.NumeroMatricule = numMatricule;
                }
                else
                {
                    GestionPharmacetique.MonMessageBox.ShowBox("Veuillez selectionner le nom de l'employé sur la liste avant de continuer", "Erreur");
                    return null;
                }
                if (!string.IsNullOrEmpty(txtIndemnites.Text))
                {
                    if (Double.TryParse(txtIndemnites.Text, out primeLogement))
                    {
                        p.Indemnites = primeLogement;
                    }
                    else
                    {
                        txtIndemnites.BackColor = Color.Red;
                        txtIndemnites.Focus();
                        return null;
                    }
                }
                else
                {
                    {
                        p.Indemnites = 0.0;
                    }
                }

                if (!string.IsNullOrEmpty(txtAutresPrimes.Text))
                {
                    if (Double.TryParse(txtAutresPrimes.Text, out provision))
                    {
                        p.AutresPrimes = provision;
                    }
                    else
                    {
                        txtAutresPrimes.BackColor = Color.Red;
                        txtAutresPrimes.Focus();
                        return null;
                    }
                }
                else
                {
                    {
                        p.AutresPrimes = 0.0;
                    }
                }

                if (Double.TryParse(lblTotalCnps.Text, out cnps))
                {
                    p.CNPS = cnps;
                }
                else
                {
                    lblTotalCnps.BackColor = Color.Red;
                    return null;
                }

                if (Double.TryParse(lblChargePat.Text, out totalCnpsCndn))
                {
                    p.ChargePatronale = totalCnpsCndn;
                }
                else
                {
                    lblChargePat.BackColor = Color.Red;
                    return null;
                }

                if (Double.TryParse(txtOnasa.Text, out onasa))
                {
                    p.ONASA = onasa;
                }
                else
                {
                    txtOnasa.BackColor = Color.Red;
                    return null;
                }
                if (Double.TryParse(lblTotalIrpp.Text, out irpp))
                {
                    p.IRPP = irpp;
                }
                else
                {
                    lblTotalIrpp.BackColor = Color.Red;
                    return null;
                }
                if (!string.IsNullOrEmpty(txtPrimeMotivation.Text))
                {
                    if (Double.TryParse(txtPrimeMotivation.Text, out primeMotivation))
                    {
                        p.PrimeMotivation = primeMotivation;
                    }
                    else
                    {
                        txtPrimeMotivation.BackColor = Color.Red;
                        txtPrimeMotivation.Focus();
                        return null;
                    }
                }
                else
                {
                    {
                        p.PrimeMotivation = 0.0;
                    }
                }


                if (!string.IsNullOrEmpty(txtFraisComm.Text))
                {
                    if (Double.TryParse(txtFraisComm.Text, out fraisComm))
                    {
                        p.FraisCommunication = fraisComm;
                    }
                    else
                    {
                        txtFraisComm.BackColor = Color.Red;
                        txtFraisComm.Focus();
                        return null;
                    }
                }
                else
                {
                    {
                        p.FraisCommunication = 0.0;
                    }
                }

                if (!string.IsNullOrEmpty(txtAcomptePayer.Text))
                {
                    if (Double.TryParse(txtAcomptePayer.Text, out acompte))
                    {
                        p.AcomptePaye = acompte;
                    }
                    else
                    {
                        txtAcomptePayer.BackColor = Color.Red;
                        txtAcomptePayer.Focus();
                        return null;
                    }
                }
                else
                {
                    {
                        p.AcomptePaye = 0.0;
                    }
                }

                if (!string.IsNullOrEmpty(txtSoinFamille.Text))
                {
                    if (Double.TryParse(txtSoinFamille.Text, out soinFamille))
                    {
                        p.ChargeSoinFamille = soinFamille;
                    }
                    else
                    {
                        txtSoinFamille.BackColor = Color.Red;
                        txtSoinFamille.Focus();
                        return null;
                    }
                }
                else
                {
                    {
                        p.ChargeSoinFamille = 0.0;
                    }
                }

                if (!string.IsNullOrEmpty(lblMontantConge.Text))
                {
                    if (Double.TryParse(lblMontantConge.Text, out congeAnnuel))
                    {
                        p.CongeAnnuel = congeAnnuel;
                    }
                    else
                    {
                        lblMontantConge.BackColor = Color.Red;
                        lblMontantConge.Focus();
                        return null;
                    }
                }
                else
                {
                    {
                        p.CongeAnnuel = 0.0;
                    }
                }

                if (!string.IsNullOrEmpty(lblMontantAbscenses.Text))
                {
                    if (Double.TryParse(lblMontantAbscenses.Text, out coutAbscence))
                    {
                        p.CoutAbsence = coutAbscence;
                    }
                    else
                    {
                        lblMontantAbscenses.BackColor = Color.Red;
                        lblMontantAbscenses.Focus();
                        return null;
                    }
                }
                else
                {
                    {
                        p.CoutAbsence = 0.0;
                    }
                }

                if (!string.IsNullOrEmpty(txtAvanceSurSalaaire.Text))
                {
                    if (Double.TryParse(txtAvanceSurSalaaire.Text, out avanceSurSalaire))
                    {
                        p.AvanceSurSalaire = avanceSurSalaire;
                    }
                    else
                    {
                        txtAvanceSurSalaaire.BackColor = Color.Red;
                        txtAvanceSurSalaaire.Focus();
                        return null;
                    }
                }
                else
                {
                    {
                        p.AvanceSurSalaire = 0.0;
                    }
                }

                if (!string.IsNullOrEmpty(txtTransport.Text))
                {
                    if (Double.TryParse(txtTransport.Text, out transport))
                    {
                        p.Transport = transport;
                    }
                    else
                    {
                        txtTransport.BackColor = Color.Red;
                        txtTransport.Focus();
                        return null;
                    }
                }
                else
                {
                    {
                        p.Transport = 0.0;
                    }
                }


                if (Double.TryParse(txtNetPayer.Text, out netAPayer))
                {
                    p.SalaireNet = netAPayer;
                }
                else
                {
                    txtNetPayer.BackColor = Color.Red;
                    txtNetPayer.Focus();
                    return null;
                }

                if (Double.TryParse(lblSalaireBrut.Text, out salaireBrut))
                {
                    p.SalaireBrut = salaireBrut;
                }
                else
                {
                    lblSalaireBrut.BackColor = Color.Red;
                    lblSalaireBrut.Focus();
                    return null;
                }

                double coutSalarie;
                if (Double.TryParse(lblCoutSalarial.Text, out coutSalarie))
                {
                    p.CoutDuSalarie = coutSalarie;
                }
                else
                {
                    lblCoutSalarial.BackColor = Color.Red;
                    lblCoutSalarial.Focus();
                    return null;
                }

                double cnrt, cnrtEmployeur;
                if (Double.TryParse(lblMontantCNRTEmploye.Text, out cnrt))
                {
                    p.CNRT = cnrt;
                }
                else
                {
                    p.CNRT = 0;
                }

                if (Double.TryParse(lblMontantCNRTEmployeur.Text, out cnrtEmployeur))
                {
                    p.CNRTEmploye = cnrtEmployeur;
                }
                else
                {
                    p.CNRTEmploye = 0;
                }

                if (rdbEspeces.Checked)
                {
                    p.ModePaiement = "Paiement en espèces";
                }
                else if (rdbCheques.Checked)
                {
                    p.ModePaiement = "Paiement par chèques";
                }
                else if (rdbBancaire.Checked)
                {
                    p.ModePaiement = "Virement bancaire";
                }
                else
                {
                    p.ModePaiement = "";
                }
                p.DatePaiement = DateTime.Now;
                p.Service = cmbTypeContrat.Text;
                p.Banque = cmbBanque.Text;
                return p;
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("Creer detail de paie", ex);
                return null;
            }
        }

        private void btnModifier_Click(object sender, EventArgs e)
        {

            try
            {
                paiement = CreerLesDetailsDePaie();
                if (paiement != null)
                {
                    paiement.MontantTotal = double.Parse(txtNetPayer.Text); ;
                    paiement.IDPaie = numeroPaiement;
                    paiement.IDAcompte = idAccompe;
                    if (etatModifier == "1")
                    {
                        if (AppCode.ConnectionClass.ModifierOrdreDePaiement(paiement, ancienCoutSalarial, ancienAcomptePaye))
                        {
                            flag = true;
                            Dispose();
                        }
                    }
                    else
                    {
                        if (AppCode.ConnectionClass.InsererModifierOrdreDePaiement(paiement, idAccompe))
                        {
                            flag = true;
                            Dispose();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("ajouter les details paiement", ex);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double primeTransport = 0, indemnites = 0, autresPrimes = 0, primeMotivations = 0, fraisCom = 0, baseCNRT =  0;
            
            if (button1.Text == "AJOUTER CONGE")
            {
                #region PrimesIndemnites
                 if (!string.IsNullOrEmpty(txtIndemnites.Text))
                {
                    if (Double.TryParse(txtIndemnites.Text, out indemnites))
                    {
                        indemnites = indemnites * 2;
                    }
                }

                if (!string.IsNullOrEmpty(txtAutresPrimes.Text))
                {
                    if (Double.TryParse(txtAutresPrimes.Text, out autresPrimes))
                    {
                        autresPrimes = autresPrimes * 2;
                    }
                }

                if (!string.IsNullOrEmpty(txtPrimeMotivation.Text))
                {
                    if (Double.TryParse(txtPrimeMotivation.Text, out primeMotivations))
                    {
                        primeMotivations = primeMotivations * 2;
                    }
                }

                if (!string.IsNullOrEmpty(txtFraisComm.Text))
                {
                    if (Double.TryParse(txtFraisComm.Text, out fraisCom))
                    {
                        fraisCom = fraisCom * 2;
                    }
                }
                if (!string.IsNullOrEmpty(txtTransport.Text))
                {
                    if (Double.TryParse(txtTransport.Text, out primeTransport))
                    {
                        primeTransport = primeTransport * 2;
                    }
                }

                  if (!string.IsNullOrEmpty(txtBaseCRNT.Text))
                  {
                      if (Double.TryParse(txtBaseCRNT.Text, out baseCNRT))
                      {
                          baseCNRT = baseCNRT * 2;
                      }
                  }
                lblMontantConge.Text = txtSalaireBase.Text;
                txtNbreJourConge.Text = "24";
                txtTransport.Text = primeTransport.ToString();
                txtIndemnites.Text = indemnites.ToString();
                txtFraisComm.Text = fraisCom.ToString();
                txtPrimeMotivation.Text = primeMotivations.ToString();
                txtAutresPrimes.Text = autresPrimes.ToString();
                txtBaseCRNT.Text = baseCNRT.ToString();
                button1.Text = "RETIRER CONGE";
                #endregion

            }
            else if (button1.Text == "RETIRER CONGE")
            {
                if (!string.IsNullOrEmpty(txtIndemnites.Text))
                {
                    if (Double.TryParse(txtIndemnites.Text, out indemnites))
                    {
                        indemnites = indemnites / 2;
                    }
                }

                if (!string.IsNullOrEmpty(txtAutresPrimes.Text))
                {
                    if (Double.TryParse(txtAutresPrimes.Text, out autresPrimes))
                    {
                        autresPrimes = autresPrimes / 2;
                    }
                }

                if (!string.IsNullOrEmpty(txtPrimeMotivation.Text))
                {
                    if (Double.TryParse(txtPrimeMotivation.Text, out primeMotivations))
                    {
                        primeMotivations = primeMotivations / 2;
                    }
                }

                if (!string.IsNullOrEmpty(txtFraisComm.Text))
                {
                    if (Double.TryParse(txtFraisComm.Text, out fraisCom))
                    {
                        fraisCom = fraisCom / 2;
                    }
                }
                if (!string.IsNullOrEmpty(txtTransport.Text))
                {
                    if (Double.TryParse(txtTransport.Text, out primeTransport))
                    {
                        primeTransport = primeTransport / 2;
                    }
                }
                
                if (!string.IsNullOrEmpty(txtBaseCRNT.Text))
                {
                    if (Double.TryParse(txtBaseCRNT.Text, out baseCNRT))
                    {
                        baseCNRT = baseCNRT /2;
                    }
                }
                lblMontantConge.Text = "0";
                txtNbreJourConge.Text = "0";
                txtTransport.Text = primeTransport.ToString();
                txtIndemnites.Text = indemnites.ToString();
                txtFraisComm.Text = fraisCom.ToString();
                txtPrimeMotivation.Text = primeMotivations.ToString();
                txtAutresPrimes.Text = autresPrimes.ToString();
                txtBaseCRNT.Text = baseCNRT.ToString();
                button1.Text = "AJOUTER CONGE";
            }
            //NetAPayer();
        }
        
    }

}
