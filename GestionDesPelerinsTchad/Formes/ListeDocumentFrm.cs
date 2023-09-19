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
    public partial class ListeDocumentFrm : Form
    {
        public ListeDocumentFrm()
        {
            InitializeComponent();
        }
        public string categorieDoc;
        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue, 2);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.SteelBlue, Color.SteelBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void ListeDocuments_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue, 2);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                SystemColors.Control, Color.White, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void ListeDocuments_Load(object sender, EventArgs e)
        {
            try
            {
                comboBox2.Items.Clear();
                //comboBox2.Items.Add("");
                var listeFourn = from f in AppCode.ConnectionClass.ListeFournisseur()
                                 where f.NomFournisseur.StartsWith(comboBox2.Text, StringComparison.CurrentCultureIgnoreCase)
                                 select f.NomFournisseur;
                foreach (var f in listeFourn)
                    comboBox2.Items.Add(f);

                comboBox1.Text = DateTime.Now.Year.ToString();
                for (var i = 2017; i < 2035; i++)
                {
                    comboBox1.Items.Add(i.ToString());
                }
                var liste = AppCode.ConnectionClass.ListeDesTypesDocuments();
                foreach (var l in liste)
                {
                    cmbTypeDoc.Items.Add(l.TypeDocument);
                }

                titre = "Liste des documents de l'année " + comboBox1.Text;
                btnExit.Location = new Point(Width - 52, 3);
                ListeDocument("");
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(cmbTypeDoc.Text))
                {
                    var idDoc = from d in AppCode.ConnectionClass.ListeDesTypesDocuments()
                                where d.TypeDocument.ToUpper() == cmbTypeDoc.Text.ToUpper()
                                select d;
                    foreach (var d in idDoc)
                        DocumentFrm.idDocument = d.NumeroType;
                    DocumentFrm.typeDocument = cmbTypeDoc.Text;
                    DocumentFrm.etat = "1";
                    DocumentFrm.categorieDoc = categorieDoc;
                    if (DocumentFrm.ShowBox() == "1")
                    {
                        ListeDocument(DocumentFrm.typeDocument);
                    }
                }
            }
            catch (Exception ex) { GestionPharmacetique.MonMessageBox.ShowBox("", ex); }
        }

        void ListeDocument(string typeDocument)
        {
            try
            {
                dataGridView1.Rows.Clear();
                var liste = from d in AppCode.ConnectionClass.ListeDesDocuments(categorieDoc)
                            join t in AppCode.ConnectionClass.ListeDesTypesDocuments()
                            on d.NumeroType equals t.NumeroType
                            join f in AppCode.ConnectionClass.ListeFournisseur()
                            on d.NumeroTiers equals f.ID
                            where d.Exercice == Convert.ToInt32(comboBox1.Text)
                            where t.TypeDocument.StartsWith(typeDocument, StringComparison.CurrentCultureIgnoreCase)
                            orderby d.NumeroDocument descending
                            select new
                            {
                                d.EcheancePaiement,
                                d.EcheanceLivraison,
                                d.DateEnregistrement,
                                d.Description,
                                d.Exercice,
                                d.MontantHT,
                                d.MontantTTC,
                                d.NumeroDocument,
                                d.NumeroTiers,
                                d.NumeroType,
                                d.ReferenceDocument,
                                d.RootPathDocument,
                                d.TVA,
                                f.ID,
                                f.NomFournisseur,
                                d.Payable,
                                t.TypeDocument,
                                d.CategorieDocument,
                                d.IDTypeDocument,
                                d.ModalitePaiement
                            };
                foreach (var d in liste)
                {

                    dataGridView1.Rows.Add(
                        d.NumeroDocument,
                        d.IDTypeDocument,
                        d.CategorieDocument,
                        d.Exercice,
                        d.ReferenceDocument,
                        d.TypeDocument,
                        d.DateEnregistrement.ToShortDateString(),
                        d.EcheancePaiement.ToShortDateString(),
                        d.EcheanceLivraison.ToShortDateString(),
                        String.Format(AppCode.GlobalVariable.elGR, "{0:0,0}", d.MontantHT),
                        String.Format(AppCode.GlobalVariable.elGR, "{0:0,0}", d.TVA),
                        String.Format(AppCode.GlobalVariable.elGR, "{0:0,0}", d.MontantTTC),
                        d.RootPathDocument.Substring(d.RootPathDocument.LastIndexOf(@"\") + 1),
                        d.Payable,
                        d.ID, d.NomFournisseur,
                        d.Description,
                       ""
                        );
                }
            }
            catch (Exception ex) { GestionPharmacetique.MonMessageBox.ShowBox("", ex); }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DocumentFrm.etat = "2";
                DocumentFrm.categorieDoc = categorieDoc;
                DocumentFrm.numero = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                if (DocumentFrm.ShowBox() == "1")
                {
                    ListeDocument(DocumentFrm.typeDocument);
                }
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 12)
                {
                    var rootPath = AppCode.GlobalVariable.rootPathDocuments +
                        dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[5].Value.ToString() +
                        "\\" + dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[12].Value.ToString();
                    System.Diagnostics.Process.Start(rootPath);
                }
            }
            catch { }
        }

        private void btnModifierPersonnel_Click(object sender, EventArgs e)
        {
            dataGridView1_DoubleClick(null, null);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var numero = Int32.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                var idTYpe = Int32.Parse(dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
                if (GestionPharmacetique.MonMessageBox.ShowBox("Voulez vous supprimer ces données?", "Confirmation") == "1")
                {
                    AppCode.ConnectionClass.SupprimerUnDocument(numero, idTYpe);
                    dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            titre = "Liste des " + cmbTypeDoc.Text + "s de l'année " + comboBox1.Text;
            ListeDocument(cmbTypeDoc.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                var liste = from d in AppCode.ConnectionClass.ListeDesDocuments(categorieDoc)
                            join t in AppCode.ConnectionClass.ListeDesTypesDocuments()
                            on d.NumeroType equals t.NumeroType
                            join f in AppCode.ConnectionClass.ListeFournisseur()
                            on d.NumeroTiers equals f.ID
                            where d.Exercice == Convert.ToInt32(comboBox1.Text)
                            where t.ReferenceDocument.StartsWith(txtRef.Text, StringComparison.CurrentCultureIgnoreCase)
                            orderby d.NumeroDocument descending
                            select new
                            {
                                d.EcheancePaiement,
                                d.EcheanceLivraison,
                                d.DateEnregistrement,
                                d.Description,
                                d.Exercice,
                                d.MontantHT,
                                d.MontantTTC,
                                d.NumeroDocument,
                                d.NumeroTiers,
                                d.NumeroType,
                                d.ReferenceDocument,
                                d.RootPathDocument,
                                d.TVA,
                                f.ID,
                                f.NomFournisseur,
                                d.Payable,
                                t.TypeDocument,
                                d.CategorieDocument,
                                d.IDTypeDocument,
                                d.ModalitePaiement
                            };
                foreach (var d in liste)
                {

                    dataGridView1.Rows.Add(
                        d.NumeroDocument,
                        d.IDTypeDocument,
                        d.CategorieDocument,
                        d.Exercice,
                        d.ReferenceDocument,
                        d.TypeDocument,
                        d.DateEnregistrement.ToShortDateString(),
                        d.EcheancePaiement.ToShortDateString(),
                        d.EcheanceLivraison.ToShortDateString(),
                        String.Format(AppCode.GlobalVariable.elGR, "{0:0,0}", d.MontantHT),
                        String.Format(AppCode.GlobalVariable.elGR, "{0:0,0}", d.TVA),
                        String.Format(AppCode.GlobalVariable.elGR, "{0:0,0}", d.MontantTTC),
                        d.RootPathDocument.Substring(d.RootPathDocument.LastIndexOf(@"\") + 1),
                        d.Payable,
                        d.ID, d.NomFournisseur,
                        d.Description,
                                d.ModalitePaiement
                        );
                }
            }
            catch (Exception ex) { GestionPharmacetique.MonMessageBox.ShowBox("", ex); }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                var liste = from d in AppCode.ConnectionClass.ListeDesDocuments(categorieDoc)
                            join t in AppCode.ConnectionClass.ListeDesTypesDocuments()
                            on d.NumeroType equals t.NumeroType
                            join f in AppCode.ConnectionClass.ListeFournisseur()
                            on d.NumeroTiers equals f.ID
                            where d.Exercice == Convert.ToInt32(comboBox1.Text)
                            orderby d.NumeroDocument descending
                            select new
                            {
                                d.EcheancePaiement,
                                d.EcheanceLivraison,
                                d.DateEnregistrement,
                                d.Description,
                                d.Exercice,
                                d.MontantHT,
                                d.MontantTTC,
                                d.NumeroDocument,
                                d.NumeroTiers,
                                d.NumeroType,
                                d.ReferenceDocument,
                                d.RootPathDocument,
                                d.TVA,
                                f.ID,
                                f.NomFournisseur,
                                d.Payable,
                                t.TypeDocument,
                                d.CategorieDocument,
                                d.IDTypeDocument,
                                d.ModalitePaiement
                            };
                foreach (var d in liste)
                {

                    dataGridView1.Rows.Add(
                        d.NumeroDocument,
                        d.IDTypeDocument,
                        d.CategorieDocument,
                        d.Exercice,
                        d.ReferenceDocument,
                        d.TypeDocument,
                        d.DateEnregistrement.ToShortDateString(),
                        d.EcheancePaiement.ToShortDateString(),
                        d.EcheanceLivraison.ToShortDateString(),
                        String.Format(AppCode.GlobalVariable.elGR, "{0:0,0}", d.MontantHT),
                        String.Format(AppCode.GlobalVariable.elGR, "{0:0,0}", d.TVA),
                        String.Format(AppCode.GlobalVariable.elGR, "{0:0,0}", d.MontantTTC),
                        d.RootPathDocument.Substring(d.RootPathDocument.LastIndexOf(@"\") + 1),
                        d.Payable,
                        d.ID, d.NomFournisseur,
                        d.Description,
                       ""
                        );
                }
            }
            catch (Exception ex) { GestionPharmacetique.MonMessageBox.ShowBox("", ex); }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                titre = "Liste des documents du " + dateTimePicker1.Value.Date.ToShortDateString() +
                    " au " + dateTimePicker2.Value.Date.ToShortDateString();
                dataGridView1.Rows.Clear();
                var liste = from d in AppCode.ConnectionClass.ListeDesDocuments(categorieDoc)
                            join t in AppCode.ConnectionClass.ListeDesTypesDocuments()
                            on d.NumeroType equals t.NumeroType
                            join f in AppCode.ConnectionClass.ListeFournisseur()
                            on d.NumeroTiers equals f.ID
                            where d.EcheanceLivraison >= dateTimePicker1.Value.Date
                            where d.EcheanceLivraison < dateTimePicker2.Value.Date.AddHours(24)
                            orderby d.NumeroDocument descending
                            select new
                            {
                                d.EcheancePaiement,
                                d.EcheanceLivraison,
                                d.DateEnregistrement,
                                d.Description,
                                d.Exercice,
                                d.MontantHT,
                                d.MontantTTC,
                                d.NumeroDocument,
                                d.NumeroTiers,
                                d.NumeroType,
                                d.ReferenceDocument,
                                d.RootPathDocument,
                                d.TVA,
                                f.ID,
                                f.NomFournisseur,
                                d.Payable,
                                t.TypeDocument,
                                d.CategorieDocument,
                                d.IDTypeDocument,
                                d.ModalitePaiement
                            };
                foreach (var d in liste)
                {

                    dataGridView1.Rows.Add(
                        d.NumeroDocument,
                        d.IDTypeDocument,
                        d.CategorieDocument,
                        d.Exercice,
                        d.ReferenceDocument,
                        d.TypeDocument,
                        d.DateEnregistrement.ToShortDateString(),
                        d.EcheancePaiement.ToShortDateString(),
                        d.EcheanceLivraison.ToShortDateString(),
                        String.Format(AppCode.GlobalVariable.elGR, "{0:0,0}", d.MontantHT),
                        String.Format(AppCode.GlobalVariable.elGR, "{0:0,0}", d.TVA),
                        String.Format(AppCode.GlobalVariable.elGR, "{0:0,0}", d.MontantTTC),
                        d.RootPathDocument.Substring(d.RootPathDocument.LastIndexOf(@"\") + 1),
                        d.Payable,
                        d.ID, d.NomFournisseur,
                        d.Description,
                                d.ModalitePaiement
                        );
                }
            }
            catch (Exception ex) { GestionPharmacetique.MonMessageBox.ShowBox("", ex); }
        }


        private void button9_Click(object sender, EventArgs e)
        {
            try
            {
                titre = "Liste des documents de l'année " + comboBox1.Text + " de " + comboBox2.Text;
                dataGridView1.Rows.Clear();
                var liste = from d in AppCode.ConnectionClass.ListeDesDocuments(categorieDoc)
                            join t in AppCode.ConnectionClass.ListeDesTypesDocuments()
                            on d.NumeroType equals t.NumeroType
                            join f in AppCode.ConnectionClass.ListeFournisseur()
                            on d.NumeroTiers equals f.ID
                            where d.Exercice == Convert.ToInt32(comboBox1.Text)
                            where f.NomFournisseur.StartsWith(comboBox2.Text, StringComparison.CurrentCultureIgnoreCase)
                            orderby d.NumeroDocument descending
                            select new
                            {
                                d.EcheancePaiement,
                                d.EcheanceLivraison,
                                d.DateEnregistrement,
                                d.Description,
                                d.Exercice,
                                d.MontantHT,
                                d.MontantTTC,
                                d.NumeroDocument,
                                d.NumeroTiers,
                                d.NumeroType,
                                d.ReferenceDocument,
                                d.RootPathDocument,
                                d.TVA,
                                f.ID,
                                f.NomFournisseur,
                                d.Payable,
                                t.TypeDocument,
                                d.CategorieDocument,
                                d.IDTypeDocument,
                                d.ModalitePaiement
                            };
                foreach (var d in liste)
                {

                    dataGridView1.Rows.Add(
                        d.NumeroDocument,
                        d.IDTypeDocument,
                        d.CategorieDocument,
                        d.Exercice,
                        d.ReferenceDocument,
                        d.TypeDocument,
                        d.DateEnregistrement.ToShortDateString(),
                        d.EcheancePaiement.ToShortDateString(),
                        d.EcheanceLivraison.ToShortDateString(),
                        String.Format(AppCode.GlobalVariable.elGR, "{0:0,0}", d.MontantHT),
                        String.Format(AppCode.GlobalVariable.elGR, "{0:0,0}", d.TVA),
                        String.Format(AppCode.GlobalVariable.elGR, "{0:0,0}", d.MontantTTC),
                        d.RootPathDocument.Substring(d.RootPathDocument.LastIndexOf(@"\") + 1),
                        d.Payable,
                        d.ID, d.NomFournisseur,
                        d.Description,
                                d.ModalitePaiement
                        );
                }
            }
            catch (Exception ex) { GestionPharmacetique.MonMessageBox.ShowBox("", ex); }
        }

        private void comboBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    comboBox2.Items.Clear();
                    var liste = from f in AppCode.ConnectionClass.ListeFournisseur()
                                where f.NomFournisseur.StartsWith(comboBox2.Text, StringComparison.CurrentCultureIgnoreCase)
                                select f.NomFournisseur;
                    foreach (var f in liste)
                        comboBox2.Items.Add(f);
                }
                catch { }
            }
        }

        string titre;

        Bitmap bitmap;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                e.Graphics.DrawImage(bitmap, 15, 20, bitmap.Width, bitmap.Height);
                e.HasMorePages = false;
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("", ex);
            }
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    var frm = new ApprouvFrm();
                    frm.lblNoDoc.Text = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    frm.lblMontant.Text = dataGridView1.SelectedRows[0].Cells[8].Value.ToString();
                    frm.lblDate.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
                    frm.lblRef.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                    frm.label4.Text = dataGridView1.SelectedRows[0].Cells[11].Value.ToString();
                    frm.ShowDialog();
                }
            }
            catch (Exception)
            {
            }
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            var total = 0.0;
            for (var i = 0; i < dataGridView1.Rows.Count; i++)
            {
                total += double.Parse(dataGridView1.Rows[i].Cells[8].Value.ToString());
            }
            var frm = new CoutFrm();
            //frm.label1.Text = "Coût global des " + dataGridView1.Rows[0].Cells[5].Value.ToString();
            frm.label2.Text = string.Format(AppCode.GlobalVariable.elGR, "{0:0,0}", total);
            frm.ShowDialog();
        }

        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {

            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    var count = dataGridView1.Rows.Count;

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

                        bitmap = AppCode.Impression.ImprimerListedocument(dataGridView1, titre);
                        string inputImage1 = @"cdali";
                        // Create an empty page
                        sharpPDF.pdfPage page = document.addPage(500, 700);

                        document.addImageReference(bitmap, inputImage1);
                        sharpPDF.Elements.pdfImageReference img = document.getImageReference(inputImage1);
                        page.addImage(img, -10, 0, page.height, page.width);


                        if (count > 18)
                        {
                            var div = (count - 18) / 26;
                            for (var i = 0; i <= div; i++)
                            {
                                if (i * 26 < count)
                                {
                                    //bitmap = AppCode.Impression.ImprimerOrdreDePaiement(numPaiement, date, exercice, mois, i);
                                    //var inputImage = @"cdali" + i;
                                    //// Create an empty page
                                    //sharpPDF.pdfPage pageIndex = document.addPage();
                                    //document.addImageReference(bitmap, inputImage);
                                    //sharpPDF.Elements.pdfImageReference img1 = document.getImageReference(inputImage);
                                    //pageIndex.addImage(img1, -10, 0, pageIndex.height, pageIndex.width);
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

        private void toolStripMenuItem13_Click(object sender, EventArgs e)
        {

            if (printDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                    bitmap = AppCode.Impression.ImprimerListedocument(dataGridView1, titre);
                    printDocument1.Print(); }
                catch (Exception ex) { GestionPharmacetique.MonMessageBox.ShowBox("", ex); }
            }
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                dataGridView1.ContextMenuStrip = contextMenuStripParametre;
                dataGridView1.ContextMenuStrip.Show(dataGridView1, e.Location);
            }
        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(cmbTypeDoc.Text))
                {
                    var frm = new DossierForm();
                    frm.state = "2";
                    frm.typeDocument = cmbTypeDoc.Text;
                    frm.ShowDialog();
                }

            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("creation du dossier", ex);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    SGDP.Formes.ModifTsockFrm.reference = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                    if (!string.IsNullOrEmpty(dataGridView1.SelectedRows[0].Cells[15].Value.ToString()))
                    {
                        SGDP.Formes.ModifTsockFrm.nomTiers = dataGridView1.SelectedRows[0].Cells[15].Value.ToString();
                    }
                    SGDP.Formes.ModifTsockFrm.numeroPiece = Int32.Parse(dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
                    SGDP.Formes.ModifTsockFrm.numeroDocument = Int32.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    SGDP.Formes.ModifTsockFrm.titre = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
                    SGDP.Formes.ModifTsockFrm.date = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
                    SGDP.Formes.ModifTsockFrm.dateLivraion = dataGridView1.SelectedRows[0].Cells[8].Value.ToString();
                    SGDP.Formes.ModifTsockFrm.totalHT = double.Parse(dataGridView1.SelectedRows[0].Cells[9].Value.ToString());
                    SGDP.Formes.ModifTsockFrm.totalTVA = double.Parse(dataGridView1.SelectedRows[0].Cells[10].Value.ToString());
                    SGDP.Formes.ModifTsockFrm.totalTTC = double.Parse(dataGridView1.SelectedRows[0].Cells[11].Value.ToString());
                    var liste = from l in AppCode.ConnectionClass.ListeDesDocuments("")
                                where l.ReferenceDocument == dataGridView1.SelectedRows[0].Cells[4].Value.ToString()
                                where l.IDTypeDocument == Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[1].Value.ToString())
                                //where l.NumeroFacture == Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[14].Value.ToString())
                                select l.ModalitePaiement;
                    foreach (var f in liste)
                        SGDP.Formes.ModifTsockFrm.modalitePaiement = f;
                    //SGDP.Formes.ModifTsockFrm.modalitePaiement = 
                    SGDP.Formes.ModifTsockFrm.etat = 1;

                    if (dataGridView1.SelectedRows[0].Cells[2].Value.ToString() == "Documents sortants")
                    {
                        SGDP.Formes.ModifTsockFrm.tier = "Client : ";
                    }
                    else
                    {
                        SGDP.Formes.ModifTsockFrm.tier = "Fournisseur : ";
                    }
                    SGDP.Formes.ModifTsockFrm.xSize = Width;
                    SGDP.Formes.ModifTsockFrm.ySize = Height;
                    SGDP.Formes.ModifTsockFrm.xLoc = Location.X;
                    SGDP.Formes.ModifTsockFrm.yLoc = Location.Y;

                    if (SGDP.Formes.ModifTsockFrm.ShowBox() == "1")
                    {
                        dataGridView1.SelectedRows[0].Cells[9].Value = SGDP.Formes.ModifTsockFrm.totalHT;
                        dataGridView1.SelectedRows[0].Cells[10].Value = SGDP.Formes.ModifTsockFrm.totalTVA;
                        dataGridView1.SelectedRows[0].Cells[11].Value = SGDP.Formes.ModifTsockFrm.totalTTC;
                    }
                }
            }
            catch (Exception ex)
            { GestionPharmacetique.MonMessageBox.ShowBox("", ex); }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            TypeDocumentFrm.index = 0;
            if (TypeDocumentFrm.ShowBox())
            {
                cmbTypeDoc.Items.Clear();
                var liste = AppCode.ConnectionClass.ListeDesTypesDocuments();
                foreach (var l in liste)
                {
                    cmbTypeDoc.Items.Add(l.TypeDocument);
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    TypeDocumentFrm.index = 1;
                    if (TypeDocumentFrm.ShowBox1())
                    {
                        if (GestionPharmacetique.MonMessageBox.ShowBox("Voulez vous transformez cette " + dataGridView1.SelectedRows[0].Cells[5].Value.ToString() +
                            " en " + TypeDocumentFrm.typeDocument, "Confirmation") == "1")
                        {

                            var liste = from d in AppCode.ConnectionClass.ListeDesDocuments(categorieDoc)
                                        join t in AppCode.ConnectionClass.ListeDesTypesDocuments()
                                        on d.NumeroType equals t.NumeroType
                                        join f in AppCode.ConnectionClass.ListeFournisseur()
                                        on d.NumeroTiers equals f.ID
                                        where d.Exercice == Convert.ToInt32(comboBox1.Text)
                                        where d.NumeroDocument == Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString())
                                        orderby d.NumeroDocument descending
                                        select new
                                        {
                                            d.NumeroFacture,
                                            d.EcheancePaiement,
                                            d.EcheanceLivraison,
                                            d.DateEnregistrement,
                                            d.Description,
                                            d.Exercice,
                                            d.MontantHT,
                                            d.MontantTTC,
                                            d.NumeroDocument,
                                            d.NumeroTiers,
                                            d.NumeroType,
                                            d.ReferenceDocument,
                                            d.RootPathDocument,
                                            d.TVA,
                                            f.ID,
                                            f.NomFournisseur,
                                            d.Payable,
                                            t.TypeDocument,
                                            d.CategorieDocument,
                                            d.IDTypeDocument,
                                            d.ModalitePaiement,
                                            d.MotCle
                                        };

                            var document = new AppCode.Document();
                            foreach (var d in liste)
                            {
                                document.EcheanceLivraison = d.EcheanceLivraison;
                                document.Exercice = d.Exercice;
                                document.MontantHT = d.MontantHT;
                                document.MotCle = d.MotCle;
                                document.MontantTTC = d.MontantTTC;
                                document.TVA = d.TVA;
                                document.DateEnregistrement = dateTimePicker1.Value;
                                document.ReferenceDocument = txtRef.Text;
                                document.NumeroType = d.NumeroType;
                                document.TypeDocument = d.TypeDocument;
                                document.RootPathDocument = d.RootPathDocument;
                                document.CategorieDocument = categorieDoc;
                                document.EcheancePaiement = dateTimePicker2.Value;
                                document.IDTypeDocument = d.IDTypeDocument; //AppCode.ConnectionClass.DernierDuDocument(TypeDocumentFrm.numeroDocument, categorieDoc) + 1;// Convert.ToInt32(txtNo.Text);
                                document.Payable = true;
                                document.ModalitePaiement = d.ModalitePaiement;
                                document.NumeroTiers = d.NumeroTiers;
                                document.NumeroFacture = d.NumeroDocument;
                            }

                            var id = AppCode.ConnectionClass.DernierDuDocument(TypeDocumentFrm.numeroDocument, categorieDoc) + 1;

                            if (id < 10)
                            {
                                document.ReferenceDocument = "0000" + id + "/" + DateTime.Now.Year;
                            }
                            else if (id >= 10 && id < 100)
                            {
                                document.ReferenceDocument = "000" + id + "/" + DateTime.Now.Year;
                            }
                            else if (id >= 100 && id < 1000)
                            {
                                document.ReferenceDocument = "00" + id + "/" + DateTime.Now.Year;
                            }
                            else if (id >= 1000 && id < 10000)
                            {
                                document.ReferenceDocument = "0" + id + "/" + DateTime.Now.Year;
                            }
                            else
                            {
                                document.ReferenceDocument = id + "/" + DateTime.Now.Year;
                            }

                            if (AppCode.ConnectionClass.TransformerUnDocument(document, TypeDocumentFrm.numeroDocument, TypeDocumentFrm.typeDocument, id))
                            {
                                ListeDocument("");
                            }

                        }
                    }
                }
            }
            catch (Exception ex) { GestionPharmacetique.MonMessageBox.ShowBox("", ex); }
        }

        private void txtRef_TextChanged(object sender, EventArgs e)
        {
            try
            { 
            titre = "Liste des documents du " + dateTimePicker1.Value.Date.ToShortDateString() +
                 " au " + dateTimePicker2.Value.Date.ToShortDateString();
            dataGridView1.Rows.Clear();
            var liste = from d in AppCode.ConnectionClass.ListeDesDocuments(categorieDoc)
                        join t in AppCode.ConnectionClass.ListeDesTypesDocuments()
                        on d.NumeroType equals t.NumeroType
                        join f in AppCode.ConnectionClass.ListeFournisseur()
                        on d.NumeroTiers equals f.ID
                        where d.MotCle.ToUpper().Contains(txtRef.Text.ToUpper())
                        select new
                        {
                            d.EcheancePaiement,
                            d.EcheanceLivraison,
                            d.DateEnregistrement,
                            d.Description,
                            d.Exercice,
                            d.MontantHT,
                            d.MontantTTC,
                            d.NumeroDocument,
                            d.NumeroTiers,
                            d.NumeroType,
                            d.ReferenceDocument,
                            d.RootPathDocument,
                            d.TVA,
                            f.ID,
                            f.NomFournisseur,
                            d.Payable,
                            t.TypeDocument,
                            d.CategorieDocument,
                            d.IDTypeDocument,
                            d.ModalitePaiement
                        };
            foreach (var d in liste)
            {

                dataGridView1.Rows.Add(
                    d.NumeroDocument,
                    d.IDTypeDocument,
                    d.CategorieDocument,
                    d.Exercice,
                    d.ReferenceDocument,
                    d.TypeDocument,
                    d.DateEnregistrement.ToShortDateString(),
                    d.EcheancePaiement.ToShortDateString(),
                    d.EcheanceLivraison.ToShortDateString(),
                    String.Format(AppCode.GlobalVariable.elGR, "{0:0,0}", d.MontantHT),
                    String.Format(AppCode.GlobalVariable.elGR, "{0:0,0}", d.TVA),
                    String.Format(AppCode.GlobalVariable.elGR, "{0:0,0}", d.MontantTTC),
                    d.RootPathDocument.Substring(d.RootPathDocument.LastIndexOf(@"\") + 1),
                    d.Payable,
                    d.ID, d.NomFournisseur,
                    d.Description,
                            d.ModalitePaiement
                    );
            }

        }
            catch (Exception ex) { GestionPharmacetique.MonMessageBox.ShowBox("", ex); }
}
}
    
}
