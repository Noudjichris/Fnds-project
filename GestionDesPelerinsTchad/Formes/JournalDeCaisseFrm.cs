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
    public partial class JournalDeCaisseFrm : Form
    {
        public JournalDeCaisseFrm()
        {
            InitializeComponent();
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

        private void JournalDeCaisseFrm_Load(object sender, EventArgs e)
        {
            checkBox2.Checked = true;
            Column4.Width = 55;
            Column5.Width = 55;
            Column9.Width = dataGridView1.Width / 4 - 50;
            Column3.Width = dataGridView1.Width / 4 + 50;
            clDate.Width = 100;
            clCode.Width = 100;
            cmbAnnne.Items.Clear();
            for (var i = 2017; i < DateTime.Now.Year + 10; i++)
            {
                cmbAnnne.Items.Add(i.ToString());
            }
            cmbAnnne.Text = DateTime.Now.Year.ToString();
            var mois = DateTime.Now.ToLongDateString();
            mois = mois.Remove(mois.LastIndexOf(" "), 5);
            mois = mois.Substring(mois.LastIndexOf(" ") + 1);
            cmbMois.Text = mois;
            cmbMois.Text = DateTime.Now.Year.ToString();
            btnFermer.Location = new Point(Width - 45, btnFermer.Location.Y);
            ListeEncaissement();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (TiersFrm.Showbox())
            {
                txtTiers.Text = TiersFrm.nomTiers;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                txtMontantAvoir.Enabled = true;
            }
            else
            {
                txtMontantAvoir.Enabled = false;
            }
        }
        void ListeEncaissement()
        {
            try
            {
                dataGridView1.Rows.Clear();
                var liste = from l in AppCode.ConnectionClass.ListeEncaissement(Convert.ToInt32(cmbAnnne.Text))
                            orderby l.ID descending
                            select l;
                var total = 0.0;
                foreach (var l in liste)
                {
                    dataGridView1.Rows.Add(l.ID, l.Exercice, l.Mois, l.DateEncaissment.ToShortDateString(), l.Date.Date.ToShortDateString(), l.Tiers, l.Code, l.Objet, l.Montant, l.Avoir);
                    total += l.Avoir + l.Montant;
                }
                dataGridView1.Rows.Add("", "", "", "Total + avoir", "", "", "", "", total, "");
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Lavender;
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 11F, System.Drawing.FontStyle.Bold);
            }
            catch { }
        }

        private void cmbLibelle_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtObjet.Text = cmbLibelle.Text;
        }
        int idEncaissement;
        private void i_Click(object sender, EventArgs e)
        {
            try
            {
                if (GestionAcademique.LoginFrm.typeUtilisateur == "compta")
                    return;
                double montant, avoir;
                if (!string.IsNullOrWhiteSpace(txtObjet.Text) && !string.IsNullOrWhiteSpace(txtCode.Text))
                {
                    if (double.TryParse(txtMontant.Text, out montant))
                    {
                        if (!string.IsNullOrEmpty(txtTiers.Text))
                        {
                            if (!string.IsNullOrWhiteSpace(txtMontantAvoir.Text))
                            {
                                if (double.TryParse(txtMontantAvoir.Text, out avoir))
                                {

                                }
                                else
                                {
                                    avoir = 0;
                                    GestionPharmacetique.MonMessageBox.ShowBox("Si avoir existe, veuillez entrer un chiffre valide", "Erreur");
                                    return;
                                }
                            }
                            else
                            {
                                avoir = 0;
                            }
                            var encaissement = new AppCode.Encaissement();

                            encaissement.Code = txtCode.Text;
                            encaissement.Date = dtpEncaissement.Value.Date;
                            encaissement.Mois = cmbMois.Text;
                            encaissement.Montant = montant;
                            encaissement.Avoir = avoir;
                            encaissement.Objet = txtObjet.Text;
                            encaissement.Tiers = txtTiers.Text;
                            encaissement.Exercice = Convert.ToInt32(cmbAnnne.Text);
                            encaissement.ID = idEncaissement;
                            if (AppCode.ConnectionClass.EnregistrerUnEncaissement(encaissement))
                            {
                                checkBox1.Checked = false;
                                idEncaissement = 0;
                                txtCode.Text = "";
                                txtMontant.Text = "";
                                txtMontantAvoir.Text = "";
                                txtObjet.Text = "";
                                txtTiers.Text = "";
                                ListeEncaissement();
                                var Encaissement = new AppCode.Encaissement();
                                Encaissement.Exercice = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
                                Encaissement.Code = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
                                Encaissement.Objet = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
                                Encaissement.Montant = Convert.ToDouble(dataGridView1.SelectedRows[0].Cells[9].Value.ToString());
                                Encaissement.Tiers = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
                                Encaissement.Date = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells[4].Value.ToString());
                                Encaissement.DateEncaissment = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells[3].Value.ToString());

                                bitmap = AppCode.Impression.RecuDePaiement(Encaissement);

                                if (printDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                                {
                                    printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                                    printPreviewDialog1.ShowDialog();
                                }
                            }

                        }
                        else
                        {
                            GestionPharmacetique.MonMessageBox.ShowBox("Veuillez entrer le nom du tiers", "Erreur");
                        }
                    }
                    else
                    {
                        GestionPharmacetique.MonMessageBox.ShowBox("Veuillez entrer un chiffre valide pour le montant", "Erreur");
                    }
                }

                else
                {
                    GestionPharmacetique.MonMessageBox.ShowBox("Veuillez entrer l'objet et le code avant de continuer", "Erreur");
                }
            }
            catch { }
        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (GestionAcademique.LoginFrm.typeUtilisateur == "compta")
                    return;
                if (e.ColumnIndex == 10)
                {
                    checkBox1.Enabled = true;
                    idEncaissement = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                    cmbAnnne.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    cmbMois.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    dtpEncaissement.Value = DateTime.Parse(dataGridView1.CurrentRow.Cells[4].Value.ToString());
                    txtTiers.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                    txtCode.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                    txtObjet.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                    txtMontant.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                    if (double.Parse(dataGridView1.CurrentRow.Cells[9].Value.ToString()) > 0)
                    { checkBox1.Checked = true; }
                    else
                    { checkBox1.Checked = false; }
                    txtMontantAvoir.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();
                }
                else if (e.ColumnIndex == 11)
                {
                    if (AppCode.ConnectionClass.SupprimerUnEncaissement(Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString())))
                    {
                        dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                    }
                }
            }
            catch { }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                if (checkBox2.Checked)
                {
                    var Encaissement = new AppCode.Encaissement();
                    Encaissement.Exercice = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
                    Encaissement.Code = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
                    Encaissement.Objet = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
                    Encaissement.Montant = Convert.ToDouble(dataGridView1.SelectedRows[0].Cells[9].Value.ToString());
                    Encaissement.Tiers = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
                    Encaissement.Date = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells[4].Value.ToString());
                    Encaissement.DateEncaissment = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells[3].Value.ToString());

                    bitmap = AppCode.Impression.RecuDePaiement(Encaissement);

                    if (printDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                        printPreviewDialog1.ShowDialog();
                    }
                }
                else
                {
                    if (dataGridView1.Rows.Count > 0)
                    {
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

                        var div = dataGridView1.Rows.Count / 48;
                        for (var i = 0; i <= div; i++)
                        {
                            var document = AppCode.Impression.ImprimerRapportJournalCaisse(dataGridView1, titre, i);

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
            }
        }
        Bitmap bitmap;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bitmap, 15, 20, bitmap.Width, bitmap.Height);
            e.HasMorePages = false;
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            titre = "Journal de la caisse de l'année " + cmbMois.Text;
            ListeEncaissement();
        }
        string titre;
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                titre = "Journal de la caisse du mois de  " + cmbMois.Text;
                dataGridView1.Rows.Clear();
                var liste = from l in AppCode.ConnectionClass.ListeEncaissement(Convert.ToInt32(cmbAnnne.Text))
                            where l.Mois == cmbMois.Text
                            orderby l.ID ascending
                            select l;
                var total = .0;
                foreach (var l in liste)
                {
                    dataGridView1.Rows.Add(l.ID, l.Exercice, l.Mois, l.DateEncaissment.ToShortDateString(), l.Date.Date.ToShortDateString(), l.Tiers, l.Code, l.Objet, l.Montant, l.Avoir);
                    total += l.Avoir + l.Montant;
                }
                dataGridView1.Rows.Add("", "", "", "Total + avoir", "", "", "", "", total, "");
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Lavender;
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 11F, System.Drawing.FontStyle.Bold);
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                titre = "Journal de la caisse de " + txtTiers.Text;
                dataGridView1.Rows.Clear();
                var liste = from l in AppCode.ConnectionClass.ListeEncaissement(Convert.ToInt32(cmbAnnne.Text))
                            where l.Tiers.StartsWith(txtTiers.Text, StringComparison.CurrentCultureIgnoreCase)
                            orderby l.Mois ascending
                            select l;
                var total = .0;
                foreach (var l in liste)
                {
                    dataGridView1.Rows.Add(l.ID, l.Exercice, l.Mois, l.DateEncaissment.ToShortDateString(), l.Date.Date.ToShortDateString(), l.Tiers, l.Code, l.Objet, l.Montant, l.Avoir);
                    total += l.Avoir + l.Montant;
                }
                dataGridView1.Rows.Add("", "", "", "Total + avoir", "", "", "", "", total, "");
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Lavender;
                dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 11F, System.Drawing.FontStyle.Bold);
            }
            catch { }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var frm = new SoldeJournaFrm();
            frm.Location = new Point(Location.X, Location.Y);
            frm.Height = Height;
            frm.ShowDialog();
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
                foreach (DataGridViewRow r in dataGridView1.Rows)
                {
                    double montant;
                    if (double.TryParse(r.Cells[9].Value.ToString(), out montant))
                        r.Cells[9].Value = montant;
                }
                sfd.FileName = "Journal_caisses__Impriméé_le_" + date + ".xls";
                if (sfd.ShowDialog() == DialogResult.OK)
                {

                    ToCsV1(dataGridView1, sfd.FileName); // Here dataGridview1 is your grid view name
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
            }
            catch (Exception ex)
            {
               GestionPharmacetique. MonMessageBox.ShowBox("", ex);
            }
        }

        private void ToCsV1(DataGridView dGV, string filename)
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
            System.IO. FileStream fs = new System.IO.FileStream(filename, System.IO.FileMode.Create);
            System.IO.BinaryWriter bw = new System.IO.BinaryWriter(fs);
            bw.Write(output, 0, output.Length); //write the encoded file
            bw.Flush();
            bw.Close();
            fs.Close();
        }

    }
}
