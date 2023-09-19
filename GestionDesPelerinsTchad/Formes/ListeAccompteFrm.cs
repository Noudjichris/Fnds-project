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
    public partial class ListeAccompteFrm : Form
    {
        public ListeAccompteFrm()
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
        private void ListeAvanceFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.Silver, 0);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                SystemColors.Control, SystemColors.Control, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void ListeAvanceFrm_Load(object sender, EventArgs e)
        {
            btnFermer.Location = new Point(Width - 50, 2);
            clPEmploye.Width = dgvAcompte.Width / 3;
            //= 100;
            clRemise.Width = Column1.Width = clQte.Width = cldATE.Width = Column7.Width=(dgvAcompte.Width - dgvAcompte.Width / 3) / 5;
            for (var i = 2017; i < 2030; i++)
            {
                cmbAnnee.Items.Add(i);
            }
            cmbAnnee.Text = DateTime.Now.Year.ToString();
            ListeDesAvanceSurSalaire("");
        }
        System.Globalization.CultureInfo elGR = System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR");

        void ListeDesAvanceSurSalaire(string numMatricule)
        {
            try
            {
                var liste = from p in AppCode.ConnectionClass.ListeAvanceSurSalaire()
                                where p.NumeroMatricule.StartsWith(numMatricule, StringComparison.CurrentCultureIgnoreCase)
                                select p;


                dgvAcompte.Rows.Clear();
                var somme = 0.0;
                foreach (var p in liste)
                {
                    var num_mat = p.NumeroMatricule;
                    var listeP = AppCode.ConnectionClass.ListePersonnelParMatricule(num_mat);
                    string nom = listeP[0].Nom + " " + listeP[0].Prenom; ;
                    dgvAcompte.Rows.Add(
                        p.ID,
                         num_mat, nom,
                        p.DatePaiement.ToShortDateString(),
                       string.Format(elGR, "{0:0,0}", p.MontantTotal),
                        p.Exercice,
                        p.MoisPaiement
                        );
                    somme += p.MontantTotal;
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
            if (dgvAcompte.SelectedRows.Count > 0)
            {
                var p = new AppCode.Paiement();
                p.ID = Convert.ToInt32(dgvAcompte.SelectedRows[0].Cells[0].Value.ToString());
                if (GestionPharmacetique.MonMessageBox.ShowBox("Voulez vous supprimer ces données?", "Confirmation") == "1")
                {
                    AppCode.ConnectionClass.SupprimerUneAvanceSurSalaire(p);
                    dgvAcompte.Rows.Remove(dgvAcompte.SelectedRows[0]);
                }
            }
        }
        Bitmap document;
        private void btnAjouterUneAgence_Click(object sender, EventArgs e)
        {
            try
            {

                if (checkBox1.Checked)
                {
                    document = AppCode.Impression.ImprimerListeDesAcomptes(dgvAcompte, cmbMois.Text, DateTime.Now.Year);
                }
                else
                {
                    document = AppCode.Impression.ImprimerRecuDePaiement(dgvAcompte);

                }
                if (printDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                    printPreviewDialog1.ShowDialog();
                }
            }
            catch { }
        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void cmbMois_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var liste =from p in AppCode.ConnectionClass.ListeAvanceSurSalaire()
                               where p.MoisPaiement.StartsWith(cmbMois.Text, StringComparison.CurrentCultureIgnoreCase)
                               where p.Exercice==Convert.ToInt32(cmbAnnee.Text)
                               select p;

                dgvAcompte.Rows.Clear();
                foreach (var p in liste)
                {
                    var num_mat = p.NumeroMatricule;
                    var listeP = AppCode.ConnectionClass.ListePersonnelParMatricule(num_mat);
                    string nom = listeP[0].Nom + " " + listeP[0].Prenom; ;
                    dgvAcompte.Rows.Add(
                        p.ID,
                         num_mat, nom,
                        p.DatePaiement.ToShortDateString(),
                        string.Format(elGR, "{0:0,0}",p.MontantTotal),
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

        private void cmbAnnee_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var liste = from p in AppCode.ConnectionClass.ListeAvanceSurSalaire()
                            where p.MoisPaiement == cmbMois.Text
                            where p.Exercice == Convert.ToInt32(cmbAnnee.Text)
                            select p;

                dgvAcompte.Rows.Clear();
                foreach (var p in liste)
                {
                    var num_mat = p.NumeroMatricule;
                    var listeP = AppCode.ConnectionClass.ListePersonnelParMatricule(num_mat);
                    string nom = listeP[0].Nom + " " + listeP[0].Prenom; ;
                    dgvAcompte.Rows.Add(
                        p.ID,
                         num_mat, nom,
                        p.DatePaiement.ToShortDateString(),
                        string.Format(elGR, "{0:0,0}",p.MontantTotal),
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
            e.Graphics.DrawImage(document, 15, 20, document.Width, document.Height);
            e.HasMorePages = false;
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AcompteFrm.etat = "1";
            if (AcompteFrm.ShowBox()=="1")
            {
                cmbMois_SelectedIndexChanged(null, null);
            }
        }

        private void dgvAcompte_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                AcompteFrm.etat = "2";
                AcompteFrm.numeroEmploye = dgvAcompte.SelectedRows[0].Cells[1].Value.ToString();
                AcompteFrm.idAvance = Int32.Parse(dgvAcompte.SelectedRows[0].Cells[0].Value.ToString());
                if (AcompteFrm.ShowBox() == "1")
                {
                    cmbMois_SelectedIndexChanged(null, null);
                }
            }
            catch { }
        }

    }
}
