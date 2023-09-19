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
    public partial class ListeFraisCongeFrm : Form
    {
        public ListeFraisCongeFrm()
        {
            InitializeComponent();
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.SteelBlue, Color.DodgerBlue, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }


        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox2.Width - 1, this.groupBox2.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.SteelBlue, Color.DodgerBlue, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void ListeFraisCongeFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.Silver, 0);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                SystemColors.Control, SystemColors.Control, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void ListeFraisCongeFrm_Load(object sender, EventArgs e)
        {
            btnFermer.Location = new Point(Width - 50, 2);
            clPEmploye.Width = dgvConge.Width / 3;
            //= 100;
            for (var i = 2017; i <( DateTime.Now.Year + 10); i++)
            {
                cmbAnnee.Items.Add(i);
            }
            cmbAnnee.Text = DateTime.Now.Year.ToString();
            ListeDesFraisCOnge("");
        }
        System.Globalization.CultureInfo elGR = System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR");

        void ListeDesFraisCOnge(string numMatricule)
        {
            try
            {
                var liste = from p in AppCode.ConnectionClass.ListeFraisConge()
                            where p.NumeroMatricule.StartsWith(numMatricule, StringComparison.CurrentCultureIgnoreCase)
                            select p;


                dgvConge.Rows.Clear();
                var somme = 0.0;
                foreach (var p in liste)
                {
                    var num_mat = p.NumeroMatricule;
                    var dtPers = AppCode.ConnectionClass.ListePersonnelParMatricule(num_mat);
                    string nom = dtPers[0].Nom+ " " + dtPers[0].Prenom;
                    dgvConge.Rows.Add(
                        p.IDConge,
                         num_mat, nom,
                       string.Format(elGR, "{0:0,0}", p.MontantConge),
                        p.Exercice,
                        p.Mois
                        );
                    somme += p.MontantConge;
                }
                lblMontant.Text = string.Format(elGR, "{0:0,0}", somme);
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("Liste accompte", ex);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dgvConge.SelectedRows.Count > 0)
            {
                var id = Convert.ToInt32(dgvConge.SelectedRows[0].Cells[0].Value.ToString());
                if (GestionPharmacetique.MonMessageBox.ShowBox("Voulez vous supprimer ces données?", "Confirmation") == "1")
                {
                    AppCode.ConnectionClass.SupprimerFraisConge(id);
                    dgvConge.Rows.Remove(dgvConge.SelectedRows[0]);
                }
            }
        }
        Bitmap document;
        private void btnAjouter_Click(object sender, EventArgs e)
        {
            try
            {
                document = AppCode.Impression.ImprimerListeDesFraisConges(dgvConge, cmbMois.Text, DateTime.Now.Year);
                if (printDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                    printPreviewDialog1.ShowDialog();
                }
                   }
            catch (Exception Exception)
            { GestionPharmacetique.MonMessageBox.ShowBox("", Exception); }
        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void cmbMois_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var liste = from p in AppCode.ConnectionClass.ListeFraisConge()
                            where p.Mois.StartsWith(cmbMois.Text, StringComparison.CurrentCultureIgnoreCase)
                            where p.Exercice == Convert.ToInt32(cmbAnnee.Text)
                            select p;

                dgvConge.Rows.Clear();
                foreach (var p in liste)
                {
                    var num_mat = p.NumeroMatricule;
                    var personnel = AppCode.ConnectionClass.ListePersonnelParMatricule(num_mat);
                    string nom = personnel[0].Nom + " " + personnel[0].Prenom;
                    dgvConge.Rows.Add(
                        p.IDConge,
                         num_mat, nom,
                        string.Format(elGR, "{0:0,0}", p.MontantConge),
                        p.Exercice,
                        p.Mois
                        );
                }
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("Liste accompte", ex);
            }
        }

        private void cmbAnnee_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var liste = from p in AppCode.ConnectionClass.ListeAvanceSurSalaire()
                            where p.MoisPaiement == cmbMois.Text
                            where p.Exercice == Convert.ToInt32(cmbAnnee.Text)
                            select p;

                dgvConge.Rows.Clear();
                foreach (var p in liste)
                {
                    var num_mat = p.NumeroMatricule;
                    var dtPers = AppCode.ConnectionClass.ListePersonnelParMatricule(num_mat);
                    string nom = dtPers[0].Nom+ " "+ dtPers[0].Prenom;
                    dgvConge.Rows.Add(
                        p.ID,
                         num_mat, nom,
                        p.DatePaiement.ToShortDateString(),
                        string.Format(elGR, "{0:0,0}", p.MontantTotal),
                        p.Exercice,
                        p.MoisPaiement
                        );
                }
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("Liste accompte", ex);
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(document, 2, 20, document.Width, document.Height);
            e.HasMorePages = false;
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FraisConFrm.etat = "1";
            if (FraisConFrm.ShowBox() == "1")
            {
                cmbMois_SelectedIndexChanged(null, null);
            }
        }

        private void dgvConge_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                FraisConFrm.etat = "2";
                FraisConFrm.numeroEmploye = dgvConge.SelectedRows[0].Cells[1].Value.ToString();
                FraisConFrm.idConge = Int32.Parse(dgvConge.SelectedRows[0].Cells[0].Value.ToString());
                if (FraisConFrm.ShowBox() == "1")
                {
                    cmbMois_SelectedIndexChanged(null, null);
                }
            }
            catch { }
        }

        private void printPreviewDialog1_Load_1(object sender, EventArgs e)
        {

        }

    }
}
