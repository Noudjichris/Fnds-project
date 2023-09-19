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
    public partial class FraisConFrm : Form
    {
        public FraisConFrm()
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
            Pen pen1 = new Pen(Color.White, 1);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox6.Width - 1, this.groupBox6.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                SystemColors.Control, SystemColors.Control, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox4_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.White, 1);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox4.Width - 1, this.groupBox4.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                SystemColors.Control, SystemColors.Control, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void AvanceFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.Silver, 0);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                SystemColors.Control, SystemColors.Control, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void AvanceFrm_Load(object sender, EventArgs e)
        {
            try
            {
                Location = new Point((MainForm.width - Width) / 2, 5);
                txtExercice.Text = DateTime.Now.Year.ToString();
                if (etat == "2")
                {
                    var liste = from p in AppCode.ConnectionClass.ListeFraisConge()
                                where p.NumeroMatricule.StartsWith(numeroEmploye, StringComparison.CurrentCultureIgnoreCase)
                                select p;
                    foreach (var p in liste)
                    {
                        txtExercice.Text = p.Exercice.ToString();
                        txtTotal.Text = p.MontantConge.ToString();
                        cmbMois.Text = p.Mois;

                        dataGridView2.Rows.Clear();
                        var listePersonnel =AppCode. ConnectionClass.ListePersonnelParMatricule(p.NumeroMatricule);
                        foreach (var l in listePersonnel)
                        {
                            dataGridView2.Rows.Add(
                                l.NumeroMatricule,
                                l.Nom  + " " +l.Prenom);
                        }
                    }
                }
            }
            catch (Exception)
            {
            }

        }
        public static string etat, numeroEmploye;

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {

                if (dataGridView2.Rows.Count > 0)
                    if (!string.IsNullOrEmpty(cmbMois.Text))
                    {
                        int exercice;
                        if (Int32.TryParse(txtExercice.Text, out exercice))
                        {
                            double total;
                            if (Double.TryParse(txtTotal.Text, out total))
                            {

                            }
                            else
                            {
                                txtTotal.BackColor = Color.Red;
                                return;
                            }
                            var conge = new AppCode.Conge();
                            conge.MontantConge = total;
                            conge.Mois = cmbMois.Text;
                            conge.Exercice = exercice;
                            conge.IDConge = idConge;
                            if (dataGridView2.SelectedRows.Count > 0)
                            {
                                conge.NumeroMatricule = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
                            }
                            else if (numeroEmploye == "" || dataGridView2.SelectedRows.Count <= 0)
                            {
                                GestionPharmacetique.MonMessageBox.ShowBox("Veuillez selectionner le nom du personnel sur la liste", "Erreur");
                                return;
                            }
                            else if (!string.IsNullOrEmpty(numeroEmploye))
                            {
                                conge.NumeroMatricule = numeroEmploye;
                            }
                            if (etat == "1")
                            {
                                if (AppCode.ConnectionClass.EnregistrerFraisConge(conge))
                                {
                                    Dispose();
                                    btnClick = "1";
                                }
                            }
                            else if (etat == "2")

                                if (GestionPharmacetique.MonMessageBox.ShowBox("Voulez vous modifiez ces données?", "Confirmation") == "1")
                                    if (AppCode.ConnectionClass.ModifierFraisConge(conge))
                                    {
                                        Dispose();
                                        btnClick = "1";
                                    }

                        }
                        else
                        {
                            GestionPharmacetique.MonMessageBox.ShowBox("Veuillez entrer un chiffre valide pour l'annee de paiement", "Erreur");
                        }
                    }
                    else
                    {
                        GestionPharmacetique.MonMessageBox.ShowBox("Veuillez selectionner le mois de paiement sur la liste puis continuez.", "Erreur");
                    }

            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("Enregistrer avance sur salaire", ex);
            }
        }

        private void txtTotal_TextChanged(object sender, EventArgs e)
        {
            txtTotal.BackColor = Color.White;
        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            Close();
        }

        public static FraisConFrm frm;
        public static string btnClick;
        public static int idConge;
        public static string ShowBox()
        {
            frm = new FraisConFrm();
            frm.ShowDialog();
            return btnClick;
        }

        private void txtRechercherEmploye_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView2.Rows.Clear();
                foreach (var p in AppCode.ConnectionClass.ListePersonnel(txtRechercherEmploye.Text))
                {
                    dataGridView2.Rows.Add(
                     p.NumeroMatricule,
                     p.Nom + " " + p.Prenom);
                }
            }
            catch { }
        }

        private void btnFermer_Click_1(object sender, EventArgs e)
        {
            btnClick = "2";
            Dispose();
        }

    }
}
