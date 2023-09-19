using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SGSP.Formes
{
    public partial class BilanFrm : Form
    {
        public BilanFrm()
        {
            InitializeComponent();
        }

        public string titre; int idBilan, idDetailBilan;
        private void BilanFrm_Load(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            var liste = AppCode.ConnectionClass.ListeDistinctDesDetailsBilansType();
            foreach (var l in liste)
            {
                comboBox2.Items.Add(l.TypeDetail);
            }
            var items = new string[] { "<<Selectionner type bilan>>", "Premier trimestre", "Deuxième trimestre", "Troisième trimestre", "Quatrième trimestre", "Premier semestre", "Deuxième semestre", "Mensuel", "Annuel" };
            comboBox1.Text = "A-ACTIF";
            cmbRapport.Items.AddRange(items);
            cmbAnne.Items.Clear();
            for (var i = 2017; i < DateTime.Now.Year + 10; i++)
            {
                cmbAnne.Items.Add(i.ToString());
            }
            cmbRapport.Text = "<<Selectionner type bilan>>";
            cmbAnne.Text = DateTime.Now.Year.ToString();

            var mois = DateTime.Now.ToLongDateString();
            mois = mois.Remove(mois.LastIndexOf(" "), 5);
            mois = mois.Substring(mois.LastIndexOf(" ") + 1);
            cmbMois.Text = mois;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbRapport.Text == "<<Selectionner type bilan>>")
                {
                    GestionPharmacetique.MonMessageBox.ShowBox("Veuillez selection le type de bilan sur la liste", "Erreur");
                    return;
                }
                var bilan = new AppCode.Bilan();
                bilan.Annee = Convert.ToInt32(cmbAnne.Text);
                bilan.TypeBilan = cmbRapport.Text;
                if(cmbRapport.Text=="Mensuel")
                {
                    bilan.TypeBilan = cmbRapport.Text +" mois de " + cmbMois.Text;
                }

                if (AppCode.ConnectionClass.EnregistrerBilan(bilan, "1"))
                {
                    idBilan = AppCode.ConnectionClass.DernierNumeroBilan();
                    GestionPharmacetique.MonMessageBox.ShowBox("Nouveau bilan crée avec succés", "Affirmation");
                }
            }
            catch { }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtRechercherlibelle_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtRechercherlibelle.Text))
                {
                    txtMontant.Focus();
                }
            }
        }

        private void txtMontant_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode== Keys.Enter)
            { double montant;
                if (double.TryParse(txtMontant.Text, out montant))
                {
                    button11.Focus();
                }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                double montant;
                if (double.TryParse(txtMontant.Text, out montant))
                {
                    
                        var bilan = new AppCode.Bilan();
                        bilan.IDDetailBilan = idDetailBilan;
                        bilan.IDBilan = idBilan;
                        bilan.TypeDetail = comboBox2.Text;
                        bilan.Montant = montant;
                    if (!string.IsNullOrWhiteSpace(txtRechercherlibelle.Text))
                    {
                        bilan.Designation = txtRechercherlibelle.Text;
                    }
                    else
                    {
                        bilan.Designation = comboBox2.Text;
                    }
                        bilan.Etat = comboBox1.Text;
                    if (AppCode.ConnectionClass.InsererUnBilan(bilan))
                    {
                        idDetailBilan = 0;
                        txtMontant.Text = "";
                        txtRechercherlibelle.Text = "";
                        txtRechercherlibelle.Focus();
                        ListeBilan();
                    }
                    else
                    {
                        idDetailBilan = 0;
                    }
                    //}
                    //else
                    //{
                    //    GestionPharmacetique.MonMessageBox.ShowBox("Veuillez entrer la désignation du bilan", "Erreur");
                    //}
                }
                else
                {
                    GestionPharmacetique.MonMessageBox.ShowBox("Veuillez entrer un chiffre valide pour le montant", "Erreur");
                }
            }
            catch { }
        }

        private void cmbAnne_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    dataGridView2.Rows.Clear();
            //    var listeBilan = from li in AppCode.ConnectionClass.ListeBilans(Convert.ToInt32(cmbAnne.Text))
            //                     where li.TypeBilan.StartsWith(cmbRapport.Text,  StringComparison.CurrentCultureIgnoreCase)
            //                     select li;
            //    foreach (var i in listeBilan)
            //    {
            //        idBilan = i.IDBilan;
            //        cmbMois.Text = i.TypeBilan.Substring(i.TypeBilan.LastIndexOf(" ")+1);
            //        foreach (var etc in AppCode.ConnectionClass.ListeDistinctDesDetailsBilans(idBilan))
            //        {
            //            var total = .0;
            //            dataGridView2.Rows.Add("","", etc.Etat, "");
            //            dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.BackColor = Color.GreenYellow;
            //            dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 11F, System.Drawing.FontStyle.Regular | FontStyle.Bold);

            //            var liste = from li in AppCode.ConnectionClass.ListeDetailsBilans(idBilan)
            //                        where li.Etat == etc.Etat
            //                        select li;
            //            foreach (var b in liste)
            //            {
            //                total += b.Montant;
            //                dataGridView2.Rows.Add(b.IDDetailBilan,etc.Etat, b.Designation, String.Format(elGR, "{0:0,0}", b.Montant));
            //                Column3.Image= global::SGSP.Properties.Resources.DeleteRed1;
            //                Column4.Image = global::SGSP.Properties.Resources.edit;
            //            }
            //            dataGridView2.Rows.Add("","", "Total", String.Format(elGR, "{0:0,0}", total));
            //            dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.BackColor = Color.GreenYellow;
            //            dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 11F, System.Drawing.FontStyle.Regular | FontStyle.Bold);

            //            dataGridView2.Rows.Add("","", "",  "");
            //        }
            //    }
            //}
            //catch
            //{ }
            try
            {
                idBilan = 0;
                   var listeBilan = from li in AppCode.ConnectionClass.ListeBilans(Convert.ToInt32(cmbAnne.Text))
                                 where li.TypeBilan.StartsWith(cmbRapport.Text, StringComparison.CurrentCultureIgnoreCase)
                                 select li;
                foreach (var i in listeBilan)
                {
                    idBilan = i.IDBilan;
                }
                    dataGridView2.Rows.Clear();
                foreach (var etc in AppCode.ConnectionClass.ListeDistinctDesDetailsBilans(idBilan))
                {
                    var total = .0;
                    dataGridView2.Rows.Add("","", "", etc.Etat, "");
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.BackColor = Color.GreenYellow;
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 11F, System.Drawing.FontStyle.Regular | FontStyle.Bold);

                    var listeType = from lt in AppCode.ConnectionClass.ListeDistinctTypeDesDetailsBilans(idBilan)      
                                    where lt.Etat==etc.Etat
                                    select lt;
                    foreach (var ty in listeType)
                    {
                        var sousTotal = .0;
                        var liste = from li in AppCode.ConnectionClass.ListeDetailsBilans(idBilan)
                                    where li.Etat == etc.Etat
                                    where li.TypeDetail == ty.TypeDetail
                                    select li;

                        //var listeChiffre = from li in AppCode.ConnectionClass.ListeDetailsBilans(idBilan)
                        //                   where li.Etat == etc.Etat
                        //                   where li.TypeDetail == ty.TypeDetail
                        //                   select li;
                        //foreach (var tp in listeChiffre)
                        //{
                        //    if (!string.IsNullOrEmpty(tp.Designation))
                        //        dataGridView2.Rows.Add("", "" ,"", ty.TypeDetail, tp.Montant);
                        //}
                        var code = AppCode.ConnectionClass.ListeDistinctDesDetailsBilansType(ty.TypeDetail);
                        var detail = "";
                        if (code != null)
                        {
                            detail = code + "-" + ty.TypeDetail;
                        }
                        dataGridView2.Rows.Add("", "","",detail, "");
                        dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 11F, System.Drawing.FontStyle.Regular | FontStyle.Bold);

                        if (liste.Count() == 1)
                        {
                            foreach (var b in liste)
                            {
                            
                                total += b.Montant;
                                dataGridView2.Rows.Add(b.IDDetailBilan, etc.Etat, ty.TypeDetail, b.Designation, String.Format(elGR, "{0:0,0}", b.Montant));
                                Column3.Image = global::SGSP.Properties.Resources.DeleteRed1;
                                Column4.Image = global::SGSP.Properties.Resources.edit;
                            }
                        }
                        else if (liste.Count() >1)
                        {
                            foreach (var b in liste)
                            {
                                sousTotal += b.Montant;
                                total += b.Montant;
                                dataGridView2.Rows.Add(b.IDDetailBilan, etc.Etat, ty.TypeDetail, b.Designation, String.Format(elGR, "{0:0,0}", b.Montant));
                                Column3.Image = global::SGSP.Properties.Resources.DeleteRed1;
                                Column4.Image = global::SGSP.Properties.Resources.edit;

                            }
                            dataGridView2.Rows.Add("", "","","Total " + ty.TypeDetail, String.Format(elGR, "{0:0,0}", sousTotal));
                            dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 11F, System.Drawing.FontStyle.Regular | FontStyle.Bold);
                        }
                    }
                    dataGridView2.Rows.Add("", "","", "Total", String.Format(elGR, "{0:0,0}", total));
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.BackColor = Color.GreenYellow;
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 11F, System.Drawing.FontStyle.Regular | FontStyle.Bold);
                    dataGridView2.Rows.Add("", "","","", "");
                }
            }
            catch
            { }
        }


        static System.Globalization.CultureInfo elGR = System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR");

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if(e.ColumnIndex==5)
                {
                    txtMontant.Text = "";
                    txtRechercherlibelle.Text = "";
                    if (Int32.TryParse(dataGridView2.CurrentRow.Cells[0].Value.ToString(), out idDetailBilan))
                    {

                        txtMontant.Text = dataGridView2.CurrentRow.Cells[4].Value.ToString();
                        txtRechercherlibelle.Text = dataGridView2.CurrentRow.Cells[3].Value.ToString();
                        comboBox2.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
                        comboBox1.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
                    }
                }else if(e.ColumnIndex==6)
                {
                    if(AppCode.ConnectionClass.SupprimerUnBilanDetaille(Convert.ToInt32(dataGridView2.CurrentRow.Cells[0].Value.ToString())))
                    {
                        dataGridView2.Rows.Remove(dataGridView2.CurrentRow);
                    }
                }
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
               
                var listeBilan = from li in AppCode.ConnectionClass.ListeBilans(Convert.ToInt32(cmbAnne.Text))
                                 where li.TypeBilan == cmbRapport.Text
                                 select li.IDBilan;
                foreach (var i in listeBilan)
                {
                    idBilan = i;
                  if(  AppCode.ConnectionClass.SupprimerUnBilan(i))
                    {
                        dataGridView2.Rows.Clear();
                        txtMontant.Text = "";
                        txtRechercherlibelle.Text = "";
                    }
                }
            }
            catch
            { }
        }

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
                sfd.FileName = "Bilan financier " +cmbRapport.Text +" "+ date + ".xls";
                if (sfd.ShowDialog() == DialogResult.OK)
                {

                    foreach (DataGridViewRow r in dataGridView2.Rows)
                    {
                        double montant;
                        if (double.TryParse(r.Cells[4].Value.ToString(), out montant))
                            r.Cells[4].Value = montant;
                    }
                    string stOutput = "";
                    // Export titles:
                    string sHeaders = "";

                    for (int j = 3; j < dataGridView2.Columns.Count - 2; j++)
                        sHeaders = sHeaders.ToString() + Convert.ToString(dataGridView2.Columns[j].HeaderText) + "\t";
                    stOutput += sHeaders + "\r\n";
                    // Export data.
                    for (int i = 0; i < dataGridView2.RowCount; i++)
                    {
                        string stLine = "";
                        for (int j = 3; j < dataGridView2.Rows[i].Cells.Count - 2; j++)
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
                        if (cmbRapport.Text == "Mensuel")
                        {
                            titre = "Bilan financier   :  mois de :  " + cmbAnne.Text;
                        }
                        else
                        {
                            titre = "Bilan financier   :   " +cmbRapport.Text +" "+ cmbAnne.Text;
                        }
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
                            var Count = dataGridView2.Rows.Count / 45;
                            for (var i = 0; i <= Count; i++)
                            {
                             var    _listeImpression = AppCode.Impression.ImprimerBilanFinancier(dataGridView2, titre, i);
                                var inputImage = @"cdali" + i;
                                // Create an empty page
                                sharpPDF.pdfPage page = document.addPage();

                                document.addImageReference(_listeImpression, inputImage);
                                sharpPDF.Elements.pdfImageReference img = document.getImageReference(inputImage);
                                page.addImage(img, -0, 0, page.height, page.width);
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

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode== Keys.Enter)
            {
                txtRechercherlibelle.Focus();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new TypeBilanFrm();
                frm.ShowDialog();
                comboBox2.Items.Clear();
                var liste = AppCode.ConnectionClass.ListeDistinctDesDetailsBilansType();
                foreach (var l in liste)
                {
                    comboBox2.Items.Add(l.TypeDetail);
                }
            }
            catch { }
        }
        void ListeBilan()
        {
            try
            {
                dataGridView2.Rows.Clear();
                foreach (var etc in AppCode.ConnectionClass.ListeDistinctDesDetailsBilans(idBilan))
                {
                    var total = .0;
                        dataGridView2.Rows.Add("", "","", etc.TypeDetail, "");
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.BackColor = Color.GreenYellow;
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 11F, System.Drawing.FontStyle.Regular | FontStyle.Bold);

                    var listeType = from lt in AppCode.ConnectionClass.ListeDistinctTypeDesDetailsBilans(idBilan)
                                    where lt.Etat == etc.Etat
                                    select lt;
                    foreach (var ty in listeType)
                    {
                        var sousTotal = .0;
                        var liste = from li in AppCode.ConnectionClass.ListeDetailsBilans(idBilan)
                                    where li.Etat == etc.Etat
                                    where li.TypeDetail == ty.TypeDetail
                                    select li;
                        var code = AppCode.ConnectionClass.ListeDistinctDesDetailsBilansType(ty.TypeDetail);
                        var detail = "";
                        if (code != null)
                        {
                            detail = code + "-" + ty.TypeDetail;
                        }
                        dataGridView2.Rows.Add("", "", "", detail  , "");
                        dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 11F, System.Drawing.FontStyle.Regular | FontStyle.Bold);

                        if (liste.Count() == 1)
                        {
                            foreach (var b in liste)
                            {
                                total += b.Montant;
                                dataGridView2.Rows.Add(b.IDDetailBilan, etc.Etat ,ty.TypeDetail, b.Designation, String.Format(elGR, "{0:0,0}", b.Montant));
                                Column3.Image = global::SGSP.Properties.Resources.DeleteRed1;
                                Column4.Image = global::SGSP.Properties.Resources.edit;
                            }
                        }
                        else if (liste.Count() > 1)
                        {
                            foreach (var b in liste)
                            {
                                sousTotal += b.Montant;
                                total += b.Montant;
                                dataGridView2.Rows.Add(b.IDDetailBilan, etc.Etat , ty.TypeDetail, b.Designation, String.Format(elGR, "{0:0,0}", b.Montant));
                                Column3.Image = global::SGSP.Properties.Resources.DeleteRed1;
                                Column4.Image = global::SGSP.Properties.Resources.edit;

                            }
                            dataGridView2.Rows.Add("", "", "", "Total " + ty.TypeDetail, String.Format(elGR, "{0:0,0}", sousTotal));
                            dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 11F, System.Drawing.FontStyle.Regular | FontStyle.Bold);
                        }
                    }
                    dataGridView2.Rows.Add("", "", "", "Total", String.Format(elGR, "{0:0,0}", total));
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.BackColor = Color.GreenYellow;
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 11F, System.Drawing.FontStyle.Regular | FontStyle.Bold);
                    dataGridView2.Rows.Add("", "", "", "", "");
                }
            }
            catch
            { }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
