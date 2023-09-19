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
    public partial class DetailDocumentFrm : Form
    {
        public DetailDocumentFrm()
        {
            InitializeComponent();
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(SystemColors.ControlLightLight, 2);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
               Color.SteelBlue, Color.DodgerBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        public int id,idTiers, stateReglement; public string categorieDoc;
        public double totalCredit;
        private void DetailDocumentFrm_Load(object sender, EventArgs e)
        {
            try
            {
                if (GestionAcademique.LoginFrm.typeUtilisateur == "compta")
                    button3.Enabled = false;
                var liste =AppCode. ConnectionClass.ListeCategorie(1);
                foreach (var l in liste)
                {
                    cmbCategorie.Items.Add(l.Categorie);
                }
                dataGridView2.RowTemplate.Height = 25;
                for (var i = 2017; i < DateTime.Now.Year + 10; i++)
                {
                    lblExercice.Items.Add(i.ToString());
                }
                button8_Click(null,null);
                var mois = DateTime.Now.ToLongDateString();
                mois = mois.Remove(mois.LastIndexOf(" "),5);
                mois = mois.Substring(mois.LastIndexOf(" ")+1);
                cmbMois.Text = mois;
               lblExercice.Text= DateTime.Now.Year.ToString();
                ListeDesReglements();
                btnExit.Location = new Point(Width - 53, 3);
                titre = "Recèttes de l' année " + lblExercice.Text;
                state = "1";
                cliCategorie.Width = clLibelle.Width = dataGridView2.Width / 4;
                Column12.Width = 40;
                Column9.Width = 40;
            }
            catch (Exception)
            {
            }

        }

        void ListeDesReglements()
        {
            try
            {
                dataGridView2.Rows.Clear();
                var totalRecettes = 0.0;
                var count = 0;
                var categorie =AppCode. ConnectionClass.ListeCategorie(1);
                foreach (var c in categorie)
                {
                    var cate = c.Categorie;
                    if(!string.IsNullOrWhiteSpace(c.Code ))
                        cate = c.Code + " - " + c.Categorie;
                    var p = from d in AppCode.ConnectionClass.ListeDesReglements(Convert.ToInt32(lblExercice.Text))
                            join li in AppCode.ConnectionClass.ListeLibelle(1)
                            on d.idLibelle equals li.IDLibelle
                            where li.IDCategorie == c.IDCategorie
                            select new
                            {
                                li.Code,
                                li.Libelle,
                                d.NumeroReglement,
                                d.MontantPaiement,
                                d.Mois,
                                d.Exercice,
                                d.DatePaiement
                            };
                    var montant = 0.0;

                    if (p.Count() > 0)
                    {
                        
                        dataGridView2.Rows.Add("",cate,"", "", "", "", "",  "");
                        dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Lavender;
                        dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 11F, System.Drawing.FontStyle.Regular | FontStyle.Bold);
                        foreach (var q in p)
                        {
                            totalRecettes += q.MontantPaiement;
                            montant += q.MontantPaiement;
                            dataGridView2.Rows.Add(q.NumeroReglement, "",q.Code, q.Libelle, q.Exercice, q.Mois,  q.DatePaiement.ToShortDateString(),  String.Format(elGR, "{0:0,0}", q.MontantPaiement));
                            count++;
                        }
                        dataGridView2.Rows.Add("", "Sous total", "", "", "", "", "",  String.Format(elGR, "{0:0,0}", montant));
                        dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Lavender;
                        dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 11F, System.Drawing.FontStyle.Regular | FontStyle.Bold);

                    }
                }
                if (count > 0)
                {
                    dataGridView2.Rows.Add("", "Total", "", "", "", "", "", String.Format(elGR, "{0:0,0}", totalRecettes));
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.BackColor = Color.GreenYellow;
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 11F, System.Drawing.FontStyle.Regular | FontStyle.Bold);
                }
            }
            catch (Exception ex)
            {
              GestionPharmacetique.  MonMessageBox.ShowBox("Liste Depenses", ex);
            }
        }
        

        private void btnExit_Click(object sender, EventArgs e)
        {
            Dispose();
        }
        int numeroReglement; string state;
        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
               double montantPaye;
              
               if (double.TryParse(lblMontant.Text, out montantPaye))
               {
                   var reglement = new AppCode.Reglement();
               
                   reglement.MontantPaiement = montantPaye;
                   reglement.DatePaiement = dateTimePicker2.Value;
                   reglement.NumeroReglement = numeroReglement;
                   reglement.Mois = cmbMois.Text;
                    reglement.Exercice = Convert.ToInt32(lblExercice.Text);

                    var liste = from l in AppCode. ConnectionClass.ListeLibelle(1)
                                join lc in AppCode.ConnectionClass.ListeCategorie(1)
                                on l.IDCategorie equals lc.IDCategorie
                                where l.Libelle == lblLibelle.Text
                                select l.IDLibelle;
                    foreach (var lc in liste)
                        reglement.idLibelle = lc;
                    if (AppCode.ConnectionClass.EnregistrerUneRecette(reglement))
                    {
                        numeroReglement = 0;
                        button8_Click(null,null);
                        lblMontant.Text = "";
                        lblLibelle.Text = "";
                        lblCode.Text = "";
                        //dateTimePicker2.Value = DateTime.Now;
                   }
               }
            }
            catch (Exception Exception) { GestionPharmacetique.MonMessageBox.ShowBox("Paiement", Exception); }
        }
        
        

        private void dataGridView2_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                state = "2";
            }
            catch { }
        }

        private void dataGridView2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView2.SelectedRows.Count > 0)
                {
                    numeroReglement = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells[0].Value.ToString());
                    lblMontant.Text = dataGridView2.SelectedRows[0].Cells[5].Value.ToString();
                    dateTimePicker2.Value = DateTime.Parse(dataGridView2.SelectedRows[0].Cells[4].Value.ToString());
                    state = "2";
                  
                }
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView2.SelectedRows.Count > 0)
                {
                    if (GestionPharmacetique.MonMessageBox.ShowBox("Voulez vous supprimer ces données?", "Confirmation") == "1")
                    {
                        AppCode.ConnectionClass.SupprimerUnReglementDuClient(Convert.ToInt32(dataGridView2.SelectedRows[0].Cells[0].Value.ToString()));
                        dataGridView2.Rows.Remove(dataGridView2.SelectedRows[0]);
                        id = Convert.ToInt32(dataGridView2.Rows[0].Cells[1].Value.ToString());
                    }
                }
            }
            catch (Exception)
            {
            }
        }
    string titre;
        private void button2_Click(object sender, EventArgs e)
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
                sfd.FileName = "Details des paiements" + date + ".xls";
                if (sfd.ShowDialog() == DialogResult.OK)
                {

                    foreach (DataGridViewRow r in dataGridView2.Rows)
                    {
                        double montant;
                        if (double.TryParse(r.Cells[7].Value.ToString(), out montant))
                            r.Cells[7].Value = montant;
                    }
                    string stOutput = "";
                    // Export titles:
                    string sHeaders = "";

                    for (int j = 1; j < dataGridView2.Columns.Count-2; j++)
                        sHeaders = sHeaders.ToString() + Convert.ToString(dataGridView2.Columns[j].HeaderText) + "\t";
                    stOutput += sHeaders + "\r\n";
                    // Export data.
                    for (int i = 0; i < dataGridView2.RowCount; i++)
                    {
                        string stLine = "";
                        for (int j = 1; j < dataGridView2.Rows[i].Cells.Count-2; j++)
                            stLine = stLine.ToString() + Convert.ToString(dataGridView2.Rows[i].Cells[j].Value) + "\t";
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
                GestionPharmacetique.MonMessageBox.ShowBox("Erreur exportation", ex);
            }
        }
        
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView2.SelectedRows.Count > 0)
                {
                    if (dataGridView2.Rows.Count > 0)
                    {
                        //titre = "Recette du mois " + cmbMois.Text + " " + lblExercice.Text;
                           var count = dataGridView2.Rows.Count;

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
                        sfd.FileName = "_imprimé_le_" + datTe + ".pdf";
                        //string pathFile = "";
                        if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            var Count = dataGridView2.Rows.Count / 47;
                            for (var i = 0; i <= Count; i++)
                            {
                                _listeImpression = AppCode.Impression.ImprimerRapporRecettes(dataGridView2, titre, i);
                                var inputImage = @"cdali" + i;
                                // Create an empty page
                                sharpPDF.pdfPage page = document.addPage();

                                document.addImageReference(_listeImpression, inputImage);
                                sharpPDF.Elements.pdfImageReference img = document.getImageReference(inputImage);
                                page.addImage(img, -10, 0, page.height, page.width);
                            }
                            document.createPDF(sfd.FileName);
                            System.Diagnostics.Process.Start(sfd.FileName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("Imprimer paiement", ex);
            }
        }

        static System.Globalization.CultureInfo elGR = System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR");
        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
        }

        Bitmap _listeImpression;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                e.Graphics.DrawImage(_listeImpression, 15, 20, _listeImpression.Width, _listeImpression.Height);
                e.HasMorePages = false;
            }
            catch { }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbLibelle.Items.Clear();
                var liste = from le in AppCode. ConnectionClass.ListeLibelle(1)
                            join li in AppCode.ConnectionClass.ListeCategorie(1)
                            on le.IDCategorie equals li.IDCategorie
                            where li.Categorie.StartsWith(cmbCategorie.Text, StringComparison.CurrentCultureIgnoreCase)
                            select le.Libelle;
                foreach (var le in liste)
                {
                    cmbLibelle.Items.Add(le);
                }
            }
            catch { }
        }
       
        private void lblMontant_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double montant;

                if (double.TryParse(lblMontant.Text, out montant))
                    lblTTC.Text = String.Format(elGR, "{0:0,0}", montant);
                else
                    lblTTC.Text = "";
            }
            catch { }
        }
        
        private void button1_Click_1(object sender, EventArgs e)
        {
            ListeDesReglements();
            titre = "Recèttes de l' année " + lblExercice.Text;
        }

        private void button8_Click(object sender, EventArgs e)
        {
                try
                {
                dataGridView2.Rows.Clear();
                    var totalRecettes = 0.0;
                    var count = 0;
                    var categorie = AppCode.ConnectionClass.ListeCategorie(1);
                    foreach (var c in categorie)
                {
                    var cate = c.Categorie;
                    if (!string.IsNullOrWhiteSpace(c.Code))
                        cate = c.Code + " - " + c.Categorie;
                    var p = from d in AppCode.ConnectionClass.ListeDesReglements(Convert.ToInt32(lblExercice.Text))
                                join li in AppCode.ConnectionClass.ListeLibelle(1)
                                on d.idLibelle equals li.IDLibelle
                                where li.IDCategorie == c.IDCategorie
                                where d.Mois.ToUpper()==cmbMois.Text.ToUpper()
                            select new
                                {
                                    li.Code,
                                    li.Libelle,
                                    d.NumeroReglement,
                                    d.MontantPaiement,
                                    d.Mois,
                                    d.Exercice,
                                    d.DatePaiement
                                };
                        var montant = 0.0;

                        if (p.Count() > 0)
                        {
                        dataGridView2.Rows.Add("",cate , "", "", "", "", "", "");
                        dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Lavender;
                        dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 11F, System.Drawing.FontStyle.Regular | FontStyle.Bold);
                            foreach (var q in p)
                            {
                                totalRecettes += q.MontantPaiement;
                                montant += q.MontantPaiement;
                            dataGridView2.Rows.Add(q.NumeroReglement, "", q.Code, q.Libelle, q.Exercice, q.Mois, q.DatePaiement.ToShortDateString(), String.Format(elGR, "{0:0,0}", q.MontantPaiement));
                                count++;
                            }
                        dataGridView2.Rows.Add("", "Sous total", "", "", "", "", "", String.Format(elGR, "{0:0,0}", montant));
                        dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Lavender;
                        dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 11F, System.Drawing.FontStyle.Regular | FontStyle.Bold);

                        }
                    }
                    if (count > 0)
                    {
                    dataGridView2.Rows.Add("", "Total", "", "", "", "", "", String.Format(elGR, "{0:0,0}", totalRecettes));
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.BackColor = Color.GreenYellow;
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 11F, System.Drawing.FontStyle.Regular | FontStyle.Bold);
                    }
                }
                catch (Exception ex)
                {
                    GestionPharmacetique.MonMessageBox.ShowBox("Liste Depenses", ex);
                }
            }
        
        private void cmbLibelle_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var liste = from le in AppCode. ConnectionClass.ListeLibelle(1)
                            where le.Libelle.ToUpper().Equals(cmbLibelle.Text.ToUpper())
                            select le;
                foreach (var le in liste)
                {
                    lblCode.Text = le.Code;
                    lblLibelle.Text = le.Libelle;
                }
            }
            catch { }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            SGSP.Formes.CategorieFrm.etat = 1;
            if (SGSP.Formes.CategorieFrm.ShowBox())
            {
                cmbCategorie.Items.Clear();
                var liste = AppCode.ConnectionClass.ListeCategorie(1);
                foreach (var l in liste)
                {
                    cmbCategorie.Items.Add(l.Categorie);
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
                {
                dataGridView2.Rows.Clear();
                    var totalRecettes = 0.0;
                    var count = 0;
                    var categorie = AppCode.ConnectionClass.ListeCategorie(1);
                    foreach (var c in categorie)
                {
                    var cate = c.Categorie;
                    if (!string.IsNullOrWhiteSpace(c.Code))
                        cate = c.Code + " - " + c.Categorie;
                    var p = from d in AppCode.ConnectionClass.ListeDesReglements(Convert.ToInt32(lblExercice.Text))
                                join li in AppCode.ConnectionClass.ListeLibelle(1)
                                on d.idLibelle equals li.IDLibelle
                                where li.IDCategorie == c.IDCategorie
                                where d.DatePaiement>=dateTimePicker2.Value.Date
                                where d.DatePaiement <dateTimePicker2.Value.Date.AddHours(24)

                                select new
                                {
                                    li.Code,
                                    li.Libelle,
                                    d.NumeroReglement,
                                    d.MontantPaiement,
                                    d.Mois,
                                    d.Exercice,
                                    d.DatePaiement
                                };
                        var montant = 0.0;

                        if (p.Count() > 0)
                        {
                        dataGridView2.Rows.Add("", cate ,"", "", "", "", "", "");
                        dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Lavender;
                        dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 11F, System.Drawing.FontStyle.Regular | FontStyle.Bold);
                            foreach (var q in p)
                            {
                                totalRecettes += q.MontantPaiement;
                                montant += q.MontantPaiement;
                            dataGridView2.Rows.Add(q.NumeroReglement, "", q.Code, q.Libelle, q.Exercice, q.Mois, q.DatePaiement.ToShortDateString(), String.Format(elGR, "{0:0,0}", q.MontantPaiement));
                                count++;
                            }
                        dataGridView2.Rows.Add("", "Sous total", "", "", "", "", "", String.Format(elGR, "{0:0,0}", montant));
                        dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Lavender;
                        dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 11F, System.Drawing.FontStyle.Regular | FontStyle.Bold);

                        }
                    }
                    if (count > 0)
                    {
                    dataGridView2.Rows.Add("", "Total", "", "", "", "", "", String.Format(elGR, "{0:0,0}", totalRecettes));
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.BackColor = Color.GreenYellow;
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 11F, System.Drawing.FontStyle.Regular | FontStyle.Bold);
                    }
                }
                catch (Exception ex)
                {
                    GestionPharmacetique.MonMessageBox.ShowBox("Liste Depenses", ex);
                }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var frm = new SGSP.Formes.LibelleDepenseFrm();
            frm.etat =1;
            frm.ShowDialog();
        }
        
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
               
                if (e.ColumnIndex == 8)
                {
                    lblCode.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
                    cmbMois.Text = dataGridView2.CurrentRow.Cells[5].Value.ToString();
                    lblExercice.Text = dataGridView2.CurrentRow.Cells[4].Value.ToString();
                    dateTimePicker2.Value = DateTime.Parse(dataGridView2.CurrentRow.Cells[6].Value.ToString());
                    lblLibelle.Text = dataGridView2.CurrentRow.Cells[3].Value.ToString();
                    lblMontant.Text = dataGridView2.CurrentRow.Cells[7].Value.ToString();
                    lblTTC.Text = dataGridView2.CurrentRow.Cells[7].Value.ToString();
                    numeroReglement = Convert.ToInt32( dataGridView2.CurrentRow.Cells[0].Value.ToString());
                }
                else if (e.ColumnIndex == 9)
                {
                    if (GestionAcademique.LoginFrm.typeUtilisateur == "compta")
                        return;
                    numeroReglement = Convert.ToInt32(dataGridView2.CurrentRow.Cells[0].Value.ToString());
                    if (AppCode.ConnectionClass.SupprimerUneRecette(numeroReglement))
                    {
                        numeroReglement = 0;
                        ListeDesReglements();
                    }
                }
            }
            catch { }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView2.Rows.Clear();
                var totalRecettes = 0.0;
                var count = 0;
                var categorie = AppCode.ConnectionClass.ListeCategorie(1);
                foreach (var c in categorie)
                {
                    var cate = c.Categorie;
                    if (!string.IsNullOrWhiteSpace(c.Code))
                        cate = c.Code + " - " + c.Categorie;
                    var p = from d in AppCode.ConnectionClass.ListeDesReglements(Convert.ToInt32(lblExercice.Text))
                            join li in AppCode.ConnectionClass.ListeLibelle(1)
                            on d.idLibelle equals li.IDLibelle
                            where li.IDCategorie == c.IDCategorie
                            where d.DatePaiement >= dtp1.Value.Date
                            where d.DatePaiement < dtp2.Value.Date.AddHours(24)

                            select new
                            {
                                li.Code,
                                li.Libelle,
                                d.NumeroReglement,
                                d.MontantPaiement,
                                d.Mois,
                                d.Exercice,
                                d.DatePaiement
                            };
                    var montant = 0.0;

                    if (p.Count() > 0)
                    {
                        dataGridView2.Rows.Add("", cate, "", "", "", "", "", "");
                        dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Lavender;
                        dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 11F, System.Drawing.FontStyle.Regular | FontStyle.Bold);
                        foreach (var q in p)
                        {
                            totalRecettes += q.MontantPaiement;
                            montant += q.MontantPaiement;
                            dataGridView2.Rows.Add(q.NumeroReglement, "", q.Code, q.Libelle, q.Exercice, q.Mois, q.DatePaiement.ToShortDateString(), String.Format(elGR, "{0:0,0}", q.MontantPaiement));
                            count++;
                        }
                        dataGridView2.Rows.Add("", "Sous total", "", "", "", "", "", String.Format(elGR, "{0:0,0}", montant));
                        dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Lavender;
                        dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 11F, System.Drawing.FontStyle.Regular | FontStyle.Bold);

                    }
                }
                if (count > 0)
                {
                    dataGridView2.Rows.Add("", "Total", "", "", "", "", "", String.Format(elGR, "{0:0,0}", totalRecettes));
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.BackColor = Color.GreenYellow;
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 11F, System.Drawing.FontStyle.Regular | FontStyle.Bold);
                }
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("Liste Depenses", ex);
            }
        }


    }
}
