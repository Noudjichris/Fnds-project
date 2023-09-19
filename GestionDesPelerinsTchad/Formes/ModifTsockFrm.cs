using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace SGDP.Formes
{
    public partial class ModifTsockFrm : Form
    {
        public ModifTsockFrm()
        {
            InitializeComponent();
        }


        private void groupBox3_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(SystemColors.ControlLightLight, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox3.Width - 1, this.groupBox3.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.Gainsboro
                , SystemColors.ControlLight, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox4_Paint(object sender, PaintEventArgs e)
        {
Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(SystemColors.ControlLightLight, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox4.Width - 1, this.groupBox4.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.Gainsboro
                , SystemColors.ControlLight, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        System.Globalization.CultureInfo elGR = System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR");
        //txtPrixTotal.Text = String.Format(elGR, "{0:0,0}", montantTh);
        private void ModifTsockFrm_Load(object sender, EventArgs e)
        {
            try
            {
                dgvProduit.Rows.Clear();
                lblNumeroPiece.Text = numeroPiece.ToString();
                lblNomTiers.Text = nomTiers;
                lblEntete.Text = "DETAILS " + titre;
                lblReference.Text = reference;
                comboBox2.Text = date;
                lblTiers.Text = tier;
                Location = new Point(xLoc, yLoc);
                Size = new System.Drawing.Size(xSize, ySize);
                btnExit.Location = new Point(Width - 50, btnExit.Location.Y);

                clDesidgnation.Width = dgvProduit.Width / 2;
                clPrixAchat.Width = dgvProduit.Width / 6;
                clStock.Width = dgvProduit.Width / 6;
                Column3.Width = dgvProduit.Width / 6;

                txtDesignation.Width = dgvProduit.Width / 2 - 1;
                txtPrixAchat.Width = dgvProduit.Width / 6 - 2;
                txtStock.Width = dgvProduit.Width / 6 - 2;
                txtPrixTotal.Width = dgvProduit.Width / 6 - 2;

                txtPrixAchat.Location = new Point(txtDesignation.Width + txtPrixAchat.Width + 12, txtDesignation.Location.Y);
                txtStock.Location = new Point(txtDesignation.Width + 10, txtDesignation.Location.Y);
                txtPrixTotal.Location = new Point(txtDesignation.Width + 2 * txtPrixAchat.Width + 15, txtDesignation.Location.Y);

                ListeDetailFacture();

                lblMontantTTC.Text = String.Format(elGR, "{0:0,0}", totalTTC);
                lblTotalHT.Text = String.Format(elGR, "{0:0,0}", totalHT);
                txtTVA.Text = String.Format(elGR, "{0:0,0}", totalTVA);
                txtDesignation.Focus();
            }
            catch { }
        }

        void ListeDetailFacture()
        {
            try
            {
                dgvProduit.Rows.Clear();
                var listeDetail = SGSP.AppCode.ConnectionClass.ListeDetailsFacures(numeroPiece,titre);
                var j = 1;
                foreach (var fac in listeDetail)
                {
                    dgvProduit.Rows.Add(
                        fac.NumeroFacture,
                        fac.Designation,
                        String.Format(elGR, "{0:0,0}", fac.Quantite),
                        String.Format(elGR, "{0:0,0}", fac.PrixUnitaire),
                        String.Format(elGR, "{0:0,0}", fac.PrixTotal),
                           String.Format(elGR, "{0:0,0}", j)
                        );
                    j++;
                    var liste = from r in SGSP.AppCode.ConnectionClass.ListeDetailsRubriquesFacures()
                                join f in SGSP.AppCode.ConnectionClass.ListeDetailsFacures(numeroPiece, titre)
                                on r.NumeroFacture equals f.NumeroFacture
                                where r.NumeroFacture == fac.NumeroFacture
                                select new
                                {
                                    r.NumeroFacture,
                                    r.NumeroRubrique,
                                    r.Designation
                                };
                    //var g= liste.Count;
                    if (liste.Count() > 0)
                    {
                        foreach (var r in liste)
                        {
                            dgvProduit.Rows.Add(
                              "0",
                              "              " + r.Designation,
                              "",
                              "",
                              "",
                              ""
                              );
                        }
                    }
                }
            }
            catch (Exception ex) { GestionPharmacetique.MonMessageBox.ShowBox("", ex); }
        }
        public static string btnClick, nomTiers, reference, titre, date, tier, dateLivraion , modalitePaiement;
        public static int numeroDocument, grandStock, stock, prix, etat, numeroPiece, xLoc, yLoc, xSize,ySize;
        public static double totalHT, totalTVA, totalTTC;
       public  static ModifTsockFrm frm;
               public static string ShowBox()
        {
            frm = new ModifTsockFrm();
            //frm.Size = new System.Drawing.Size(MainForme.XSize, MainForme.YSize);
            //frm.Location = new Point(MainForme.XLocation, MainForme.YLocation);
            frm.ShowDialog();
            return btnClick;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            btnClick = "1";
            totalHT = double.Parse(lblTotalHT.Text);
            totalTVA = double.Parse(txtTVA.Text);
            totalTTC = double.Parse(lblMontantTTC.Text);
            Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dgvProduit.SelectedRows.Count > 0)
            {
               
            }
        }

        int numero = 0;
        private void dgvProduit_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dgvProduit.SelectedRows.Count > 0)
                {
                    int idd;
                    if (Int32.TryParse(dgvProduit.SelectedRows[0].Cells[0].Value.ToString(), out idd))
                    {
                        if (idd > 0)
                        {
                            rowIndex = dgvProduit.SelectedRows[0].Index;
                            txtDesignation.Text = dgvProduit.SelectedRows[0].Cells[1].Value.ToString();
                            txtPrixAchat.Text = dgvProduit.SelectedRows[0].Cells[3].Value.ToString();
                            txtStock.Text = dgvProduit.SelectedRows[0].Cells[2].Value.ToString();
                            txtPrixTotal.Text = dgvProduit.SelectedRows[0].Cells[4].Value.ToString();
                            etat = 2;
                            txtDesignation.Focus();
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvProduit.Rows.Count > 0)
                {
                    if (GestionPharmacetique.MonMessageBox.ShowBox("Voulez vous retirer cet element de la liste?", "Confirmation") == "1")
                    {
                        var facture = new SGSP.AppCode.Document();

                        facture.MontantHT = double.Parse(lblTotalHT.Text) - double.Parse(dgvProduit.SelectedRows[0].Cells[4].Value.ToString());
                        facture.NumeroDocument = numeroDocument;
                        facture.MontantTTC = facture.MontantHT - double.Parse(txtTVA.Text);
                        facture.NumeroFacture = Int32.Parse(dgvProduit.SelectedRows[0].Cells[0].Value.ToString());
                        if (SGSP.AppCode.ConnectionClass.RetirerUnElementDansUneFacture(facture))
                        {
                            ListeDetailFacture();
                            CalculerPrixTotal();
                        }
                    }
                }
            }
            catch { }
        }

        void  CalculerPrixTotal()
        {
            try
            {
                double  totalHT = 0.0, totalTVA =0.0, qte, prix;
                for (var i = 0; i < dgvProduit.Rows.Count; i++)
                {
                    double t;
                    if (double.TryParse(dgvProduit.Rows[i].Cells[4].Value.ToString(), out t))
                    {
                        totalHT += t;
                    }
                }

                if (double.TryParse(txtStock.Text, out qte) && double.TryParse(txtPrixAchat.Text,out  prix))
                {
                    totalHT += qte * prix;
                }
                if (double.TryParse(txtTVA.Text, out totalTVA))
                {
                }

                double reduction = 0.0;
                if (etat == 1)
                {
                    reduction = 0.0;
                }
                else if (etat == 2)
                {
                    reduction = Convert.ToDouble(dgvProduit.Rows[rowIndex].Cells[2].Value.ToString()) *
                        Convert.ToDouble(dgvProduit.Rows[rowIndex].Cells[3].Value.ToString());
                }
                totalHT -= reduction;

                var totalTTC = totalHT + totalTVA;
                lblTotalHT.Text =   String.Format(elGR, "{0:0,0}",totalHT);
                txtTVA.Text   = String.Format(elGR, "{0:0,0}",totalTVA);
                lblMontantTTC.Text = String.Format(elGR, "{0:0,0}", totalTTC);
            }
            catch
            {
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                var facture = new SGSP.AppCode.Document();
                facture.IDTypeDocument = Convert.ToInt32(lblNumeroPiece.Text);
                facture.RootPathDocument = lblNomTiers.Text;
                facture.DateEnregistrement = Convert.ToDateTime(comboBox2.Text);
                facture.ReferenceDocument = lblReference.Text;
                facture.MontantHT = double.Parse(lblTotalHT.Text);
                facture.MontantTTC = double.Parse(lblMontantTTC.Text);
                facture.TVA = double.Parse(txtTVA.Text);
                facture.ModalitePaiement = modalitePaiement;
                facture.EcheanceLivraison =DateTime.Parse( dateLivraion);
                facture.TypeDocument = lblEntete.Text.Substring(lblEntete.Text.IndexOf(" "));
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PDF Documents (*.pdf)|*.pdf";
                var dateInv = DateTime.Now;               

                sharpPDF.pdfDocument document = new sharpPDF.pdfDocument("christian", "cdali");
                var jour = DateTime.Now.Day;
                var mois = DateTime.Now.Month;
                var year = DateTime.Now.Year;
                var hour = DateTime.Now.Hour;
                var min = DateTime.Now.Minute;
                var sec = DateTime.Now.Second;
                var date = jour.ToString() + "_" + mois.ToString() + "_" + year.ToString() + "_" + hour + "_" + min + "_" + sec;
                sfd.FileName = lblEntete.Text +"_imprimé_le_" + date + ".pdf";

                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (facture.TypeDocument.ToUpper().Contains("LIVRAISON") || checkBox1.Checked)
                    {
                        var _inventaireStock = SGSP.AppCode.Impression.ImprimerUnBonLivraison(facture, dgvProduit);

                        var inputImage = @"cdali";
                        // Create an empty page
                        sharpPDF.pdfPage pageIndex = document.addPage(842, 595);
                        pageIndex.drawRectangle(0, 0, 595, 842, sharpPDF.pdfColor.White, sharpPDF.pdfColor.White);
                                  
                        document.addImageReference(_inventaireStock, inputImage);
                        sharpPDF.Elements.pdfImageReference img = document.getImageReference(inputImage);
                        pageIndex.addImage(img, 0, 0, pageIndex.height, pageIndex.width);  // pageIndex.addImage(img, 28, 0, 842, 595);
                        var index = (dgvProduit.Rows.Count - 25) / 40;
                        if (dgvProduit.Rows.Count > 25)
                        {
                            for (var i = 0; i <= index; i++)
                            {
                                if (i * 40 < dgvProduit.Rows.Count)
                                {
                                    _inventaireStock = SGSP.AppCode.Impression.ImprimerUnBonLivraison(facture, dgvProduit, i);

                                    inputImage = @"cdali" + i;
                                    // Create an empty page
                                    pageIndex = document.addPage(842, 595);
                                    pageIndex.drawRectangle(0, 0, 595, 842, sharpPDF.pdfColor.White,sharpPDF.pdfColor.White);
                                    document.addImageReference(_inventaireStock, inputImage);
                                    sharpPDF.Elements.pdfImageReference img1 = document.getImageReference(inputImage);
                                    pageIndex.addImage(img1, 0, 0, pageIndex.height, pageIndex.width);
                                }
                            }
                        }
                        checkBox1.Checked = false;
                    }
                    else
                    {
                        var _inventaireStock = SGSP.AppCode.Impression.ImprimerUneFature(facture, dgvProduit);

                        var inputImage = @"cdali";
                        // Create an empty page
                        sharpPDF.pdfPage pageIndex = document.addPage(842, 595);

                        document.addImageReference(_inventaireStock, inputImage);
                        sharpPDF.Elements.pdfImageReference img = document.getImageReference(inputImage);
                        pageIndex.addImage(img, 0, 0, pageIndex.height, pageIndex.width);  // pageIndex.addImage(img, 28, 0, 842, 595);
                        var index = (dgvProduit.Rows.Count - 18) / 37;
                        if (dgvProduit.Rows.Count > 18)
                        {
                            for (var i = 0; i <= index; i++)
                            {
                                if (i * 37 < dgvProduit.Rows.Count-18)
                                {
                                    _inventaireStock = SGSP.AppCode.Impression.ImprimerUneFaturePage(facture, dgvProduit, i);

                                    inputImage = @"cdali" + i;
                                    // Create an empty page
                                    pageIndex = document.addPage(842, 595);

                                    document.addImageReference(_inventaireStock, inputImage);
                                    sharpPDF.Elements.pdfImageReference img1 = document.getImageReference(inputImage);
                                    pageIndex.addImage(img1, 0, 0, pageIndex.height, pageIndex.width);
                                }
                            }
                        }
                    }
                }
                document.createPDF(sfd.FileName);
                System.Diagnostics.Process.Start(sfd.FileName);
            }

            catch (Exception ex) { GestionPharmacetique.MonMessageBox.ShowBox("", ex); }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (!string.IsNullOrEmpty(txtDesignation.Text))
                    {
                        txtStock.Focus();
                    }
                }
                catch { }
            }
        }

        private void txtStock_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    double quantite;
                    if (double.TryParse(txtStock.Text, out quantite))
                    {
                        CalculerPrixTotal();
                        txtPrixAchat.Focus();
                    }
                }
                catch { }
            }
            else if (e.KeyCode == Keys.Left)
            {
                txtDesignation.Focus();
            }
        }

        private void txtPrixAchat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    double quantite;
                    if (double.TryParse(txtStock.Text, out quantite))
                    {
                        InsererDetailFacture();
                    }
                }
                catch { }
            }
            else if (e.KeyCode == Keys.Left)
            {
                txtStock.Focus();
            }
        }

        private void txtPrixAchat_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double prixAchat, quantite;
                if (double.TryParse(txtPrixAchat.Text, out prixAchat) && double.TryParse(txtStock.Text, out quantite))
                {
                    var prixTotal = prixAchat * quantite;
                    txtPrixTotal.Text = String.Format(elGR, "{0:0,0}", prixTotal);
                    //CalculerPrixTotal();
                }
            }
            catch { }
        }
        int rowIndex;
        void InsererDetailFacture()
        {
            try
            {
                double prixUnitaire, quantite, prixTotal;
                if (double.TryParse(txtPrixAchat.Text, out prixUnitaire) &&
                    double.TryParse(txtStock.Text, out quantite) &&
                    double.TryParse(txtPrixTotal.Text, out prixTotal))
                {
                    if (!string.IsNullOrEmpty(txtDesignation.Text))
                    {
                        CalculerPrixTotal();
                        var facture = new SGSP.AppCode.Document();
                        facture.IDTypeDocument = Int32.Parse(lblNumeroPiece.Text);
                        facture.PrixTotal = prixTotal;
                        facture.PrixUnitaire = prixUnitaire;
                        facture.Quantite = quantite;
                        facture.Designation = txtDesignation.Text;
                        facture.NumeroDocument = numeroDocument;
                        facture.MontantHT = double.Parse(lblTotalHT.Text);
                        facture.MontantTTC = double.Parse(lblMontantTTC.Text);
                        facture.TVA = double.Parse(txtTVA.Text);
                        facture.TypeDocument = titre;
                        if(SGSP.AppCode.ConnectionClass.InsererDetailsDesDocuments(facture,dgvProduit,etat,rowIndex))
                        {
                            etat = 1;
                            txtDesignation.Text = "";
                            txtPrixAchat.Text = "";
                            txtPrixTotal.Text = "";
                            txtStock.Text = "";
                            txtDesignation.Focus();
                            //CalculerPrixTotal();
                        }
                    }
                }
            }
            catch (Exception ex) { GestionPharmacetique.MonMessageBox.ShowBox("", ex); }
        }

        private void txtTVA_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                double tva, totalHT;
                if (double.TryParse(txtTVA.Text, out tva) &&
                    double.TryParse(lblTotalHT.Text , out totalHT))
                {
                    if (GestionPharmacetique.MonMessageBox.ShowBox("Voulez vous mettre à jour la TVA?", "confirmation") == "1")
                    {
                        double totaltva;
                        if (tva < 50)
                        {
                            totaltva=totalHT * tva / 100;
                        }
                        else
                        {
                            totaltva = tva;
                        }
                        var totalttc = totalHT + totaltva;
                        lblMontantTTC.Text = String.Format(elGR, "{0:0,0}", totalttc);
                        txtTVA.Text = String.Format(elGR, "{0:0,0}", totaltva);
                        var facture = new SGSP.AppCode.Document();
                        facture.TVA = totaltva;
                        facture.MontantTTC = totalttc;
                        facture.NumeroDocument = numeroDocument;
                        if (SGSP.AppCode.ConnectionClass.MettreAjourLaTVA(facture))
                        {
                            GestionPharmacetique.MonMessageBox.ShowBox("Données mis à jour avec succés", "affirmation");
                            dgvProduit.Focus();
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvProduit.SelectedRows.Count > 0)
                {
                    int id;
                    if (Int32.TryParse(dgvProduit.SelectedRows[0].Cells[0].Value.ToString(), out id))
                    {
                        SGSP.Formes.RubriqueFrm.id = id;
                        SGSP.Formes.RubriqueFrm.numeroFacture = numeroPiece;
                        SGSP.Formes.RubriqueFrm.type = titre;
                        SGSP.Formes.RubriqueFrm.rubrique = dgvProduit.SelectedRows[0].Cells[1].Value.ToString();
                        if (SGSP.Formes.RubriqueFrm.ShowBox())
                        {
                            ListeDetailFacture();
                            //var indexInsertion = dgvProduit.SelectedRows[0].Index;
                            //foreach (DataGridViewRow row in GestionDesPelerinsTchad.Formes.RubriqueFrm.dgvRow.Rows)
                            //{
                            //    indexInsertion++;
                            //    DataGridViewRow newRow = new DataGridViewRow();
                            //    newRow.Cells[0].Value = "0";
                            //    newRow.Cells[1].Value = row.Cells[2].Value.ToString();
                            //    newRow.Cells[2].Value = "";
                            //    newRow.Cells[3].Value = "";
                            //    newRow.Cells[4].Value = "";
                            //    dgvProduit.Rows.Insert(indexInsertion,newRow);
                            //}
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("", ex);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (printDialog1.ShowDialog() == DialogResult.OK)
                {
                    var facture = new SGSP.AppCode.Document();
                    facture.IDTypeDocument = Convert.ToInt32(lblNumeroPiece.Text);
                    facture.RootPathDocument = lblNomTiers.Text;
                    facture.DateEnregistrement = Convert.ToDateTime(comboBox2.Text);
                    facture.ReferenceDocument = lblReference.Text;
                    facture.MontantHT = double.Parse(lblTotalHT.Text);
                    facture.MontantTTC = double.Parse(lblMontantTTC.Text);
                    facture.TVA = double.Parse(txtTVA.Text);
                    facture.ModalitePaiement = modalitePaiement;
                    facture.EcheanceLivraison = DateTime.Parse(dateLivraion);
                    facture.TypeDocument = lblEntete.Text.Substring(lblEntete.Text.IndexOf(" "));
                    var index = (dgvProduit.Rows.Count -18 ) / 33;

                    printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                    //if(facture.TypeDocument
                    if (facture.TypeDocument.ToUpper().Contains("LIVRAISON") || checkBox1.Checked)
                    {
                        document = SGSP.AppCode.Impression.ImprimerUnBonLivraison(facture, dgvProduit);
               
                    }
                    else
                    {
                        document = SGSP.AppCode.Impression.ImprimerUneFature(facture, dgvProduit);
                    }
                    printDocument1.Print();
                    //printPreviewDialog1.ShowDialog();
                    if(dgvProduit.Rows.Count>18)
                    {
                        for (var i = 0; i <= index; i++)
                        {
                            if (i * 33 < dgvProduit.Rows.Count-18)
                            {
                                if (facture.TypeDocument.ToUpper().Contains("LIVRAISON") || checkBox1.Checked)
                                { document = SGSP.AppCode.Impression.ImprimerUnBonLivraison(facture, dgvProduit, i); }
                                else
                                {
                                    document = SGSP.AppCode.Impression.ImprimerUneFaturePage(facture, dgvProduit, i);
                                }
                                printDocument1.Print();
                            }
                        }
                    }
                 
                    //printDocument1.Print();
                }
            }

            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("", ex);
            }
        }
        Bitmap document;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(document, 7, 0, document.Width+45, document.Height-75);
            e.HasMorePages = false;
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
        }
        
    }
}
