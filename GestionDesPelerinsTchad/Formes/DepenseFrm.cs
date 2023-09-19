using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using SGSP.AppCode;
using System.IO;
using System.Text;

namespace GestionPharmacetique.Forme
{
    public partial class DepenseFrm : Form
    {
        public DepenseFrm()
        {
            InitializeComponent();
        }

        private void DepenseFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.BlueViolet, 0);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush =
                new LinearGradientBrush(area1, SystemColors.Control, Color.White, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.CadetBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.DodgerBlue, Color.SteelBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        
        private void i_Click(object sender, EventArgs e)
        {
            try
            {
                double montant;
                if (double.TryParse(txtPrix.Text, out montant))
                {

                }
                else
                {
                    MonMessageBox.ShowBox("Veuillez entrer un chiffre valide pour le montant", "Erreur");
                    return;
                }
                if (!string.IsNullOrEmpty(lblLibelle.Text))
                {
                    var depense = new Depenses();
       
                    depense.Date = dtpDebut.Value;
                    depense.IDDepense = idDepense;
                    depense.Beneficiaire = txtBeneficiaire.Text;
                    depense.Montant = montant;
                    depense.NumeroFacture = txtFacture.Text;
                    depense.Annee = Convert.ToInt32(lblExercice.Text);
                    depense.Mois = cmbMois.Text;
                    var liste = from l in ConnectionClass.ListeLibelle(2)
                                join lc in ConnectionClass.ListeCategorie(2)
                                on l.IDCategorie equals lc.IDCategorie
                                where l.Libelle == lblLibelle.Text
                                select l.IDLibelle;
                    foreach(var l in liste)
                         depense.IDLibelle = l;

                    if (ConnectionClass.EnregistrerUneDepense(depense))
                    {
                        idDepense = 0;
                        txtBeneficiaire.Text = "";
                        txtFacture.Text = "";
                        txtPrix.Text = "";
                        lblLibelle.Text = "";
                        dtpDebut.Value = DateTime.Now;
                        var mois = DateTime.Now.ToLongDateString();
                        mois = mois.Remove(mois.LastIndexOf(" "), 5);
                        mois = mois.Substring(mois.LastIndexOf(" ") + 1);
                        cmbMois.Text = mois;
                        lblExercice.Text = DateTime.Now.Year.ToString();
                        ListeDesDepensesEffectues();
                    }                    
                }
                else
                {
                    MonMessageBox.ShowBox("Veuillez selectionner le libellé sur la liste ci haut ", "Erreur");
                }
                
            }
            catch (Exception ex) { MonMessageBox.ShowBox("Enregistrer depense", ex); }
        }

        private void DepenseFrm_Load(object sender, EventArgs e)
        {
            try
            {
                ListeDesDepensesEffectues();
                btnFermer.Location = new Point(Width - 45, btnFermer.Location.Y);
 
                clCode.Width = 100;
                clAnne.Width = 100;
                clDate.Width = 100;
                clMois.Width = 100;
                clFacture.Width = 120;
                Column2.Width = 220; Column3.Width = 220;

                clMontant.Width = 160;
                Column9.Width = 190;
                Column5.Width = 40;
                Column4.Width = 40;
                cmbCategorie.Items.Clear();
                var liste = ConnectionClass.ListeCategorie(2);
                foreach (var l in liste)
                {
                    cmbCategorie.Items.Add(l.Categorie);
                }
                
                for (var i = 2017; i < DateTime.Now.Year + 10; i++)
                {
                    lblExercice.Items.Add(i.ToString());
                }
                var mois = DateTime.Now.ToLongDateString();
                mois = mois.Remove(mois.LastIndexOf(" "), 5);
                mois = mois.Substring(mois.LastIndexOf(" ") + 1);
                cmbMois.Text = mois;
                lblExercice.Text = DateTime.Now.Year.ToString();
                dataGridView1.RowTemplate.Height = 30;
            }
            catch { }
        }
        
        System.Globalization.CultureInfo elGR = System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR");
        private void button2_Click(object sender, EventArgs e)
        {

            //try
            //{
                //var elGR = System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR");
                //listView1.Items.Clear();
                //listView2.Items.Clear();
                //listView2.Visible = false;
                //listView1.Visible = true;

                //var libelle = ConnectionClass.ListeCategorie(2);
                //var q = from l in libelle
                //        orderby l.Categorie
                //        group l by l.Categorie;

                //var montantTotal = 0.0;
                //foreach (var l in q)
                //{
                //    var count = 0;

                //    var p = from c in libelle
                //            where c.Categorie == l.Key
                //            orderby c.Libelle
                //            select c;
                //    var montant = 0.0;
                //    var items11 = new string[]
                //            {
                //                "","",l.Key,"", "","","",""
                //            };
                //    var listItems11 = new ListViewItem(items11);
                //    listView1.Items.Add(listItems11);
                    //foreach (var lib in p)
                    //{
                    //    var listeDepense = ConnectionClass.ListeDesDepenses();
                    //    var liste = from ld in listeDepense
                    //                join bl in libelle on
                    //                ld.ID equals bl.ID
                    //                orderby bl.Categorie, bl.Libelle, ld.Date
                    //                where bl.ID == lib.ID
                    //                where ld.Date >= dateTimePicker1.Value.Date
                    //                where ld.Date <dateTimePicker2.Value.Date.AddHours(24)
                    //                select new
                    //                {
                    //                    bl.Categorie,
                    //                    ld.IDDepense,
                    //                    bl.ID,
                    //                    ld.Date,
                    //                    ld.Beneficiaire,
                    //                    ld.Montant,
                    //                    ld.NumeroFacture,
                    //                    bl.Libelle
                    //                };
                    //    foreach (var d in liste)
                    //    {
                    //        var items = new string[]
                    //        {
                    //            d.IDDepense.ToString(),d.ID.ToString(),"",d.Date.ToShortDateString(), d.NumeroFacture,d.Libelle ,String.Format(elGR, "{0:0,0}",d.Montant),d.Beneficiaire
                    //        };
                    //        var listItems = new ListViewItem(items);
                    //        listView1.Items.Add(listItems);
                    //        montant += d.Montant;
                    //        montantTotal += d.Montant;
                    //    }
                    //    count = liste.Count();
                    //}
                    //if (count > 0)
                    //{
            //        var items1 = new string[]
            //                {
            //                    "","","Total "+cate,"", "","",String.Format(elGR, "{0:0,0}",String.Format(elGR, "{0:0,0}",montant)),""
            //                };
            //        var listItems1 = new ListViewItem(items1);
            //        listView1.Items.Add(listItems1);
            //        var items2 = new string[]
            //                {
            //                    "","","","", "","","",""
            //                };
            //        var listItems2 = new ListViewItem(items2);
            //        listView1.Items.Add(listItems2);
            //        ////}
            //    }

            //    var items3 = new string[]
            //                {
            //                    "","","TOTAL","", "","",String.Format(elGR, "{0:0,0}",montantTotal ),""
            //                };
            //    var listItems3 = new ListViewItem(items3);
            //    listView1.Items.Add(listItems3);

            //    foreach (ListViewItem lstItems in listView1.Items)
            //    {
            //        if (string.IsNullOrEmpty(lstItems.SubItems[0].Text) && lstItems.SubItems[2].Text == "Total "+cate)
            //        {
            //            //lstItems.BackColor = Color.LightSkyBlue; 
            //            lstItems.Font = new System.Drawing.Font("Arial unicode", 12F, System.Drawing.FontStyle.Regular |
            //               FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));

            //        }
            //        else if (string.IsNullOrEmpty(lstItems.SubItems[0].Text) &&
            //            lstItems.SubItems[2].Text != "Total "+cate && lstItems.SubItems[2].Text != "")
            //        {
            //            lstItems.ForeColor = Color.White;
            //            lstItems.BackColor = Color.MidnightBlue;
            //            lstItems.Font = new System.Drawing.Font("Arial unicode", 12F, System.Drawing.FontStyle.Regular |
            //                FontStyle.Bold | FontStyle.Underline | FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MonMessageBox.ShowBox("Afficher raaport", ex);
            //}
        }

        Bitmap rapportDepense;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(rapportDepense, 0, 10, rapportDepense.Width, rapportDepense.Height);
            e.HasMorePages = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbLibelle.Items.Clear();
                var liste = from le in ConnectionClass.ListeLibelle(2)
                            join li in ConnectionClass.ListeCategorie(2)
                            on le.IDCategorie equals li.IDCategorie
                            where li.Categorie.StartsWith(cmbCategorie.Text, StringComparison.CurrentCultureIgnoreCase)
                            where le.Libelle.StartsWith(txtRechercherlibelle.Text, StringComparison.CurrentCultureIgnoreCase)
                            select le.Libelle;
                foreach (var le in liste)
                {
                    cmbLibelle.Items.Add(le);
                }
            }
            catch { }
        }

        int idDepense;     
        private void ListeDesDepensesEffectues()
        {
            try
            {
                dataGridView1.Rows.Clear();
                var totalDepenses = 0.0;
                var count = 0;
                var categorie = ConnectionClass.ListeCategorie(2);
                foreach (var c in categorie)
                {
                    var cate = c.Categorie;
                    if (!string.IsNullOrWhiteSpace(c.Code))
                        cate = c.Code + " - " + c.Categorie;
                    var p = from d in ConnectionClass.ListeDesDepenses()
                            join li in ConnectionClass.ListeLibelle(2)
                            on d.IDLibelle equals li.IDLibelle
                            where li.IDCategorie == c.IDCategorie
                            where d.Annee==Convert.ToInt32(lblExercice.Text)
                            select new
                            {
                                li.Code,
                                li.Libelle,
                                d.IDDepense,
                                d.Montant,
                                d.Mois,
                                d.Annee,
                                d.Beneficiaire,
                                d.NumeroFacture,
                                d.Date
                            };
                    var montant = 0.0;

                    if (p.Count() > 0)
                    {
                        dataGridView1.Rows.Add("", cate, "", "", "", "", "", "", "", "");
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Lavender;
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 11F, System.Drawing.FontStyle.Regular | FontStyle.Bold);
                        foreach (var q in p)
                        {
                            totalDepenses += q.Montant;
                            montant += q.Montant;
                            dataGridView1.Rows.Add(q.IDDepense, "", q.Annee, q.Mois, q.Code, q.Libelle, q.Date.ToShortDateString(), q.NumeroFacture, q.Beneficiaire, String.Format(elGR, "{0:0,0}", q.Montant));
                            count++;
                        }
                        dataGridView1.Rows.Add("", "Total "+cate, "", "", "", "", "", "", "", String.Format(elGR, "{0:0,0}", montant));
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Lavender;
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 11F, System.Drawing.FontStyle.Regular | FontStyle.Bold);

                    }
                }
                if (count > 0)
                {
                    dataGridView1.Rows.Add("", "Total", "", "", "", "", "", "", "", String.Format(elGR, "{0:0,0}", totalDepenses));
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.GreenYellow;
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 11F, System.Drawing.FontStyle.Regular | FontStyle.Bold);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste Depenses", ex);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    var titre = "Journal de depenses du mois de " + comboBox2.Text;
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "PDF Documents (*.pdf)|*.pdf";

                    sharpPDF.pdfDocument pdfDocument = new sharpPDF.pdfDocument("christian", "cdali");
                    var jour = DateTime.Now.Day;
                    var mois = DateTime.Now.Month;
                    var year = DateTime.Now.Year;
                    var hour = DateTime.Now.Hour;
                    var min = DateTime.Now.Minute;
                    var sec = DateTime.Now.Second;
                    var date = jour.ToString() + "_" + mois.ToString() + "_" + year.ToString() + "_" + hour + "_" + min + "_" + sec;

                    var pathFolder = "C:\\Dossier Personnel\\Depenses";
                    if (!System.IO.Directory.Exists(pathFolder))
                    {
                        System.IO.Directory.CreateDirectory(pathFolder);
                    }
                    sfd.InitialDirectory = pathFolder;
                    sfd.FileName = label13.Text + "_" + date + ".pdf";

                    var div = dataGridView1.Rows.Count / 45;
                    for (var i = 0; i <= div; i++)
                    {
                        var  document = Impression.ImprimerRapportDepenses(dataGridView1, titre, i);

                        sharpPDF.pdfPage pageIndex = pdfDocument.addPage();

                        var inputImage = @"cdali" + i;
                        pdfDocument.addImageReference(document, inputImage);
                        sharpPDF.Elements.pdfImageReference img1 = pdfDocument.getImageReference(inputImage);
                        pageIndex.addImage(img1, 0, 0, pageIndex.height, pageIndex.width);
                    }
                    pdfDocument.createPDF(sfd.FileName);
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
            }
            catch (Exception ex) { MonMessageBox.ShowBox("", ex); }
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cmbLibelle.Items.Clear();
                var liste = from le in ConnectionClass.ListeLibelle(2)
                            join li in ConnectionClass.ListeCategorie(2)
                            on le.IDCategorie equals li.IDCategorie
                            where li.Categorie.StartsWith(cmbCategorie.Text, StringComparison.CurrentCultureIgnoreCase)
                            where le.Libelle.StartsWith(txtRechercherlibelle.Text, StringComparison.CurrentCultureIgnoreCase)
                            select le.Libelle;
                foreach (var le in liste)
                {
                    cmbLibelle.Items.Add(le);
                }
            }
            catch { }
        }
        
        private void button5_Click(object sender, EventArgs e)
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
                foreach(DataGridViewRow r in dataGridView1.Rows)
                {
                    double montant;
                    if(double.TryParse(r.Cells[9].Value.ToString(), out montant))
                    r.Cells[9].Value = montant ;
                }
                    sfd.FileName = "Journal_Des_Depenses__Impriméé_le_" + date + ".xls";
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {

                        ToCsV(dataGridView1 , sfd.FileName); // Here dataGridview1 is your grid view name
                        System.Diagnostics.Process.Start(sfd.FileName);
                    }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("", ex);
            }
        }

        private void ToCsV(DataGridView dGV, string filename)
        {
            try
            {
                string stOutput = "";
                // Export titles:
                string sHeaders = "";

                for (int j = 1; j < dGV.Columns.Count-2; j++)
                    sHeaders = sHeaders.ToString() + Convert.ToString(dGV.Columns[j].HeaderText) + "\t";
                stOutput += sHeaders + "\r\n";
                // Export data.
                for (int i = 0; i < dGV.RowCount; i++)
                {
                    string stLine = "";
                    for (int j = 1; j < dGV.Rows[i].Cells.Count-2; j++)
                        stLine = stLine.ToString() + Convert.ToString(dGV.Rows[i].Cells[j].Value) + "\t";
                    stOutput += stLine + "\r\n";
                }
                Encoding utf16 = Encoding.GetEncoding(1254);
                byte[] output = utf16.GetBytes(stOutput);
                FileStream fs = new FileStream(filename, FileMode.Create);
                BinaryWriter bw = new BinaryWriter(fs);
                bw.Write(output, 0, output.Length); //write the encoded file
                bw.Flush();
                bw.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Erreur exportation", ex);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SGSP.Formes.CategorieFrm.etat = 2;
            if (SGSP.Formes.CategorieFrm.ShowBox())
            {
                cmbCategorie.Items.Clear();
                var liste = ConnectionClass.ListeCategorie(2);
                foreach (var l in liste)
                {
                    cmbCategorie.Items.Add( l.Categorie);
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var frm = new SGSP.Formes.LibelleDepenseFrm();
            frm.etat = 2;
            frm.ShowDialog();
        }

        private void cmbLibelle_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var liste = from le in ConnectionClass.ListeLibelle(2)
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

 
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (GestionAcademique.LoginFrm.typeUtilisateur == "compta")
                    return;
                if (e.ColumnIndex == 10)
                {
                    if (!string.IsNullOrEmpty(dataGridView1.CurrentRow.Cells[0].Value.ToString()))
                    {

                        idDepense = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                        cmbMois.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                        lblExercice.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                        lblCode.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                        lblLibelle.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                        dtpDebut.Value = DateTime.Parse(dataGridView1.CurrentRow.Cells[6].Value.ToString());
                        txtFacture.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                        txtBeneficiaire.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                        txtPrix.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
                    }
                }
                else if (e.ColumnIndex == 11)
                {
                    if (!string.IsNullOrEmpty(dataGridView1.CurrentRow.Cells[0].Value.ToString()))
                    {
                        if (ConnectionClass.SupprimerUneDepense(Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString())))
                            dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                    }
                }
            }
            catch { }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (SGSP.Formes.TiersFrm.Showbox())
            {
                txtBeneficiaire.Text = SGSP.Formes.TiersFrm.nomTiers;
            }
        }

        private void ToCsV1(DataGridView dGV, string filename)
        {
            string stOutput = "";
            // Export titles:
            string sHeaders = "";

            for (int j = 0; j < dGV.Columns.Count; j++)
                sHeaders = sHeaders.ToString() + Convert.ToString(dGV.Columns[j].HeaderText) + "\t";
            stOutput += sHeaders + "\r\n";
            // Export data.
            for (int i = 0; i < dGV.RowCount; i++)
            {
                string stLine = "";
                for (int j = 0; j < dGV.Rows[i].Cells.Count; j++)
                    stLine = stLine.ToString() + Convert.ToString(dGV.Rows[i].Cells[j].Value) + "\t";
                stOutput += stLine + "\r\n";
            }
            Encoding utf16 = Encoding.GetEncoding(1254);
            byte[] output = utf16.GetBytes(stOutput);
            FileStream fs = new FileStream(filename, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(output, 0, output.Length); //write the encoded file
            bw.Flush();
            bw.Close();
            fs.Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                var totalDepenses = 0.0;
                var categorie = ConnectionClass.ListeCategorie(2);
            
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste Depenses", ex);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                var totalDepenses = 0.0;
                var categorie = ConnectionClass.ListeCategorie(2);
                var count = 0;
                foreach (var c in categorie)
                {
                    var cate = c.Categorie;
                    if (!string.IsNullOrWhiteSpace(c.Code))
                        cate = c.Code + " - " + c.Categorie;
                    var p = from d in ConnectionClass.ListeDesDepenses()
                            join li in ConnectionClass.ListeLibelle(2)
                            on d.IDLibelle equals li.IDLibelle
                            where li.IDCategorie == c.IDCategorie
                            where d.Mois == comboBox2.Text 
                            where d.Annee==Convert.ToInt32(lblExercice.Text)
                            select new
                            {
                                li.Code,
                                li.Libelle,
                                d.IDDepense,
                                d.Montant,
                                d.Mois,
                                d.Annee,
                                d.Beneficiaire,
                                d.NumeroFacture,
                                d.Date
                            };
                    var montant = 0.0;

                    if (p.Count() > 0)
                    {
                        dataGridView1.Rows.Add("", cate, "", "", "", "", "", "", "", "");
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Lavender;
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 11F, System.Drawing.FontStyle.Regular | FontStyle.Bold);
                        foreach (var q in p)
                        {
                            totalDepenses += q.Montant;
                            montant += q.Montant;
                            dataGridView1.Rows.Add(q.IDDepense, "", q.Annee, q.Mois, q.Code, q.Libelle, q.Date.ToShortDateString(), q.NumeroFacture, q.Beneficiaire, String.Format(elGR, "{0:0,0}", q.Montant));
                            count++;
                        }
                        dataGridView1.Rows.Add("", "Total "+cate, "", "", "", "", "", "", "", String.Format(elGR, "{0:0,0}", montant));
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Lavender;
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 11F, System.Drawing.FontStyle.Regular | FontStyle.Bold);

                    }
                }
                if (count > 0)
                {
                    dataGridView1.Rows.Add("", "Total", "", "", "", "", "", "", "", String.Format(elGR, "{0:0,0}", totalDepenses));
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.GreenYellow;
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 11F, System.Drawing.FontStyle.Regular | FontStyle.Bold);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste Depenses", ex);
            }
        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            button4_Click(null, null);
        }

        private void lblExercice_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListeDesDepensesEffectues();
        }

        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(SystemColors.ControlLightLight, 2);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
               Color.SteelBlue, Color.DodgerBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void dtpDebut_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                var totalDepenses = 0.0;
                var categorie = ConnectionClass.ListeCategorie(2);
                var count = 0;
                foreach (var c in categorie)
                {
                    var cate = c.Categorie;
                    if (!string.IsNullOrWhiteSpace(c.Code))
                        cate = c.Code + " - " + c.Categorie;
                    var p = from d in ConnectionClass.ListeDesDepenses()
                            join li in ConnectionClass.ListeLibelle(2)
                            on d.IDLibelle equals li.IDLibelle
                            where li.IDCategorie == c.IDCategorie
                            where d.Date >= dtpDebut.Value.Date
                            where d.Date <dtpDebut.Value.Date.AddHours(24)
                            select new
                            {
                                li.Code,
                                li.Libelle,
                                d.IDDepense,
                                d.Montant,
                                d.Mois,
                                d.Annee,
                                d.Beneficiaire,
                                d.NumeroFacture,
                                d.Date
                            };
                    var montant = 0.0;

                    if (p.Count() > 0)
                    {
                        dataGridView1.Rows.Add("", cate, "", "", "", "", "", "", "", "");
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Lavender;
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 11F, System.Drawing.FontStyle.Regular | FontStyle.Bold);
                        foreach (var q in p)
                        {
                            totalDepenses += q.Montant;
                            montant += q.Montant;
                            dataGridView1.Rows.Add(q.IDDepense, "", q.Annee, q.Mois, q.Code, q.Libelle, q.Date.ToShortDateString(), q.NumeroFacture, q.Beneficiaire, String.Format(elGR, "{0:0,0}", q.Montant));
                            count++;
                        }
                        dataGridView1.Rows.Add("", "Total " + cate, "", "", "", "", "", "", "", String.Format(elGR, "{0:0,0}", montant));
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Lavender;
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 11F, System.Drawing.FontStyle.Regular | FontStyle.Bold);

                    }
                }
                if (count > 0)
                {
                    dataGridView1.Rows.Add("", "Total", "", "", "", "", "", "", "", String.Format(elGR, "{0:0,0}", totalDepenses));
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.GreenYellow;
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 11F, System.Drawing.FontStyle.Regular | FontStyle.Bold);
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste Depenses", ex);
            }
        }
    }
}