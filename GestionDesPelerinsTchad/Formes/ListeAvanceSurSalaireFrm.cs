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
    public partial class ListeAvanceSurSalaireFrm : Form
    {
        public ListeAvanceSurSalaireFrm()
        {
            InitializeComponent();
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.Silver, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.SteelBlue, Color.DodgerBlue, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void ListeAcompteFrm_Load(object sender, EventArgs e)
        {
            btnFermer.Location = new Point(Width - 50, 2);
            clPEmploye.Width = dgvAcompte.Width / 3;
            for (var i = 2017; i < DateTime.Now.Year + 10; i++)
            {
                cmbAnnnee.Items.Add(i.ToString());
            }

            var mois = DateTime.Now.ToLongDateString();
            mois = mois.Remove(mois.LastIndexOf(" "), 5);
            mois = mois.Substring(mois.LastIndexOf(" ") + 1);
            cmbMois.Text = mois;

            cmbAnnnee.Text = DateTime.Now.Year.ToString();
            cldATE.Width = Column7.Width =clRemise.Width = Column1.Width = clPrixTotal.Width =
                clQte.Width = (dgvAcompte.Width -dgvAcompte.Width/3) / 7-10;
            Column2.Width = 180;
            var liste =AppCode. ConnectionClass.ListeDesAccompte("");
            ListeAccompte( liste);
        }
        System.Globalization.CultureInfo elGR = System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR");
        void ListeAccompte(List<AppCode.Acompte> liste)
        {
            try
            {
                var acp = from a in liste
                          orderby a.NumeroAcompte descending
                          where a.DateAcompte>=DateTime.Parse( "01/01/"+cmbAnnnee.Text)
                          where a.DateAcompte< DateTime.Parse("31/12/" + cmbAnnnee.Text).AddHours(24)
                          select a;
                dgvAcompte.Rows.Clear();
                var total = .0; var totalPaye = 0.0;
                foreach (var p in acp)
                {
                    total += p.MontantAcompte;
                    totalPaye += p.Rembourser;
                    dgvAcompte.Rows.Add
                    (
                        p.NumeroAcompte,
                        p.NumeroMatricule,
                        p.NomEmploye,
                        p.DateAcompte.ToShortDateString(),
                        String.Format(elGR, "{0:0,0}", p.MontantAcompte),
                        String.Format(elGR, "{0:0,0}", p.Deduction),
                        String.Format(elGR, "{0:0,0}", p.Rembourser),
                        String.Format(elGR, "{0:0,0}", p.MontantAcompte - p.Rembourser),
                        p.ModePaiement
                    );
                }

                dgvAcompte.Rows.Add
               (
                   "",
                   "",
                   "Total",
                   "",
                     String.Format(elGR, "{0:0,0}", total),
                   "",
                   String.Format(elGR, "{0:0,0}", totalPaye),
                   String.Format(elGR, "{0:0,0}", total - totalPaye),
                 ""
               );
                dgvAcompte.Rows[dgvAcompte.Rows.Count - 1].DefaultCellStyle.BackColor = Color.GreenYellow;
                dgvAcompte.Rows[dgvAcompte.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 10.5F, System.Drawing.FontStyle.Regular | FontStyle.Bold);
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("Liste accompte", ex);
            }
        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void txtDeduction_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var liste =AppCode. ConnectionClass.ListeDesAccompte(txtDeduction.Text);
            ListeAccompte(liste);
        }

        private void btnAjouterUneAgence_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkBox1.Checked)
                {
                    if(dgvAcompte.Rows.Count>0)
                    {
                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "PDF Documents (*.pdf)|*.pdf";

                        sharpPDF.pdfDocument pdfDocument = new sharpPDF.pdfDocument("christian", "cdali");
                        var jour = DateTime.Now.Day;
                        var moiSs = DateTime.Now.Month;
                        var year = DateTime.Now.Year;
                        var hour = DateTime.Now.Hour;
                        var min = DateTime.Now.Minute;
                        var sec = DateTime.Now.Second;
                        var datTe = jour.ToString() + "_" + moiSs.ToString() + "_" + year.ToString() + "_" + hour + "_" + min + "_" + sec;
                        sfd.FileName =  "Liste_avance_imprimé_le_" + datTe + ".pdf";
                        //string pathFile = "";
                        if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            var div = dgvAcompte.Rows.Count / 48;
                            for (var i = 0; i <= div; i++)
                            {
                                var bitmap = AppCode.Impression.ImprimerListeDeAvancesSurSalaire(dgvAcompte, Int32.Parse(cmbAnnnee.Text), i);

                                 sharpPDF.pdfPage pageIndex = pdfDocument.addPage();

                                var inputImage = @"cdali" + i;
                                pdfDocument.addImageReference(bitmap, inputImage);
                                sharpPDF.Elements.pdfImageReference img1 = pdfDocument.getImageReference(inputImage);
                                pageIndex.addImage(img1, 0, 0, pageIndex.height, pageIndex.width);
                            }
                            pdfDocument.createPDF(sfd.FileName);
                            System.Diagnostics.Process.Start(sfd.FileName);
                        }
                }
            }
                else
                {
                    document = AppCode.Impression.ImprimerRecuDeAvancesSureSalaire(dgvAcompte);
                    if (printDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                        printPreviewDialog1.ShowDialog();
                    }
                }
            }
            catch(Exception ex)
            { }

        }
        Bitmap document;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(document, 15, 20, document.Width, document.Height);
            e.HasMorePages = false;
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        { printPreviewDialog1.Document = printDocument1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dgvAcompte.SelectedRows.Count > 0)
            {
                if (GestionPharmacetique.MonMessageBox.ShowBox("Voulez vous supprimer ces données?", "Confirmation") == "1")
                {
                    var id = Convert.ToInt32(dgvAcompte.SelectedRows[0].Cells[0].Value.ToString());
                    AppCode.ConnectionClass.SupprimerUnAccompte(id);
                    dgvAcompte.Rows.Remove(dgvAcompte.SelectedRows[0]);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                SGSP.Formes.AvanceSurSalaireFrm.etat = true;
                if (SGSP.Formes.AvanceSurSalaireFrm.ShowBox() == "1")
                {
                    var ac = SGSP.Formes.AvanceSurSalaireFrm.acompte;
                    var nomEmploye = ac.NomEmploye.Substring(0, 4);
                    var liste = AppCode.ConnectionClass.ListeDesAccompte(nomEmploye );
                    ListeAccompte(liste);
                }
            }
            catch { }
        }

        private void dgvAcompte_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dgvAcompte.SelectedRows.Count > 0)
                {
                    AppCode.Acompte acp = new AppCode.Acompte();
                    acp.NumeroAcompte = Int32.Parse(dgvAcompte.SelectedRows[0].Cells[0].Value.ToString());
                    acp.NumeroMatricule = dgvAcompte.SelectedRows[0].Cells[1].Value.ToString();
                    acp.NomEmploye = dgvAcompte.SelectedRows[0].Cells[2].Value.ToString();
                    acp.DateAcompte = DateTime.Parse(dgvAcompte.SelectedRows[0].Cells[3].Value.ToString());
                    acp.MontantAcompte = double.Parse(dgvAcompte.SelectedRows[0].Cells[4].Value.ToString());
                    acp.Deduction = double.Parse(dgvAcompte.SelectedRows[0].Cells[5].Value.ToString());
                    acp.Rembourser = double.Parse(dgvAcompte.SelectedRows[0].Cells[6].Value.ToString());
                    acp.ModePaiement = dgvAcompte.SelectedRows[0].Cells[8].Value.ToString();
                    SGSP.Formes.AvanceSurSalaireFrm.acompte = acp;
                    SGSP.Formes.AvanceSurSalaireFrm.etat = false;
                    if (SGSP.Formes.AvanceSurSalaireFrm.ShowBox() == "1")
                    {
                      acp = SGSP.Formes.AvanceSurSalaireFrm.acompte;
                        var nomEmploye = acp.NomEmploye.Substring(0, 4);
                        var liste = AppCode.ConnectionClass.ListeDesAccompte(nomEmploye);
                        ListeAccompte(liste);
                    }
                }
            }
            catch { }
        }

        private void cmbAnnnee_SelectedIndexChanged(object sender, EventArgs e)
        {
            var liste = AppCode.ConnectionClass.ListeDesAccompte("");
            ListeAccompte(liste);
        }

        private void cmbMois_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var acp = from a in AppCode.ConnectionClass.ListeDesAccompte("")
                          orderby a.NumeroAcompte descending
                          where a.DateAcompte >= ObtenirDebutJour(ObtenirMois(cmbMois.Text))
                          where a.DateAcompte < ObtenirFinJour(ObtenirMois(cmbMois.Text))
                          select a;
                dgvAcompte.Rows.Clear();
                var total = .0;var totalPaye = 0.0;
                foreach (var p in acp)
                {
                    total += p.MontantAcompte;
                    totalPaye += p.Rembourser;
                    dgvAcompte.Rows.Add
                    (
                        p.NumeroAcompte,
                        p.NumeroMatricule,
                        p.NomEmploye,
                        p.DateAcompte.ToShortDateString(),
                        String.Format(elGR, "{0:0,0}", p.MontantAcompte),
                        String.Format(elGR, "{0:0,0}", p.Deduction),
                        String.Format(elGR, "{0:0,0}", p.Rembourser),
                        String.Format(elGR, "{0:0,0}", p.MontantAcompte - p.Rembourser),
                        p.ModePaiement
                    );
                }

                dgvAcompte.Rows.Add
               (
                   "",
                   "",
                   "Total",
                   "",
                     String.Format(elGR, "{0:0,0}", total),
                   "",
                   String.Format(elGR, "{0:0,0}", totalPaye),
                   String.Format(elGR, "{0:0,0}", total - totalPaye),
                 ""
               );
                dgvAcompte.Rows[dgvAcompte.Rows.Count - 1].DefaultCellStyle.BackColor = Color.GreenYellow;
                dgvAcompte.Rows[dgvAcompte.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 10.5F, System.Drawing.FontStyle.Regular | FontStyle.Bold);

            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("Liste accompte", ex);
            }
        }
        
        int  ObtenirMois(string  mois)
        {
            switch (mois)
            {
                case "Janvier":
                    return 1;
                case "Fevrier":
                    return 2;
                case "Mars":
                    return 3;
                case "Avril":
                    return 4;
                case "Mai":
                    return 5;
                case "Juin":
                    return 6;
                case "Juillet":
                    return 7;
                case "Août":
                    return 8;
                case "Septembre":
                    return 9;
                case "Octobre":
                    return 10;
                case "Novembre":
                    return 11;
                case "Decembre":
                    return 12;
                default:
                    return 0;
            };
        }

        DateTime ObtenirDebutJour(int mois)
        {
            return DateTime.Parse("01/" + mois + "/" + cmbAnnnee.Text);
        }
       
        DateTime ObtenirFinJour(int mois)
        {
            if (mois == 1)
            {
                return DateTime.Parse("31/" + mois + "/" + cmbAnnnee.Text);
            }
            else if (mois == 2)
            {
                if (DateTime.IsLeapYear(Convert.ToInt32(cmbAnnnee.Text)))
                    return DateTime.Parse("29/" + mois + "/" + cmbAnnnee.Text);
                else
                    return DateTime.Parse("28/" + mois + "/" + cmbAnnnee.Text);
            }
            else if (mois == 3)
            {
                return DateTime.Parse("31/" + mois + "/" + cmbAnnnee.Text);
            }
            else if (mois == 4)
            {
                return DateTime.Parse("30/" + mois + "/" + cmbAnnnee.Text);
            }
            else if (mois == 5)
            {
                return DateTime.Parse("31/" + mois + "/" + cmbAnnnee.Text);
            }
            else if (mois == 6)
            {
                return DateTime.Parse("30/" + mois + "/" + cmbAnnnee.Text);
            }
            else if (mois == 7)
            {
                return DateTime.Parse("31/" + mois + "/" + cmbAnnnee.Text);
            }
            else if (mois == 8)
            {
                return DateTime.Parse("31/" + mois + "/" + cmbAnnnee.Text);
            }
            else if (mois == 9)
            {
                return DateTime.Parse("30/" + mois + "/" + cmbAnnnee.Text);
            }
            else if (mois == 10)
            {
                return DateTime.Parse("31/" + mois + "/" + cmbAnnnee.Text);
            }
            else if (mois == 11)
            {
                return DateTime.Parse("30/" + mois + "/" + cmbAnnnee.Text);
            }
            else if (mois == 12)
            {
                return DateTime.Parse("31/" + mois + "/" + cmbAnnnee.Text);
            }
            else
            {
                return ObtenirDebutJour(mois);
            }
        }

    }
}
