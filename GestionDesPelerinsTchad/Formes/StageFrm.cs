using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SGSP.Formes
{
    public partial class StageFrm : Form
    {
        public StageFrm()
        {
            InitializeComponent();
        }
        public int numeroStagiaire;
        public string nomStagiaire;
        private void StageFrm_Load(object sender, EventArgs e)
        {
            try
            {
                ListeStage();
                cmbUniversite.Items.Add("");
                foreach (var i in AppCode.ConnectionClass.ListeDesEtablissements())
                    cmbUniversite.Items.Add(i.NomEtablissment);
                Location = new Point((MainForm.width - Width) / 2, 143);
                lblNomStagiaie.Text = nomStagiaire;
                cmbDirection.Items.Add("");
                foreach (var d in AppCode.ConnectionClass.ListeDirection())
                {
                    cmbDirection.Items.Add(d.Direction);
                }
            }catch{}
        }

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(cmbNatureStage.Text))
                {
                    if (!string.IsNullOrWhiteSpace(cmbStatus.Text))
                    {
                        int duree;
                        if (int.TryParse(txtDuree.Text, out duree))
                        {
                            double montant;
                            var stage = new AppCode.Stagiaire();
                            stage.IDStage = idStage;
                            stage.DateFin = dtpFinContrat.Value;
                            stage.DateDebut = dtpDateservice.Value;
                            stage.Faculte = txtFaculte.Text;
                            stage.Diplome = txtDiplome.Text;
                            stage.Universite = cmbUniversite.Text;
                            stage.Duree = duree;
                            stage.Direction = cmbDirection.Text;
                            stage.Status = cmbStatus.Text;
                            stage.NatureStage = cmbNatureStage.Text;
                            stage.SiRenumere = checkBox1.Checked;
                            stage.IDStagiaire = numeroStagiaire;
                            stage.SiDiplome = checkBox2.Checked;
                            if (checkBox1.Checked)
                            {
                                txtMontant.Visible = true;
                                if (double.TryParse(txtMontant.Text, out montant))
                                {

                                }
                                else
                                {
                                    txtMontant.Focus();
                                    txtMontant.BackColor = Color.Red;
                                    GestionPharmacetique.MonMessageBox.ShowBox("Veuillez entrer un chiffre valide pour le montant de la rénumération du stagiaire.", "Erreur");
                                }
                            }
                            else
                            {
                                txtMontant.Text = "";
                                txtMontant.Visible = false;
                                montant = 0;
                            }
                            if (AppCode.ConnectionClass.EnregistrerUnStage(stage))
                            {
                                idStage = 0;
                                txtFaculte.Text = "";
                                cmbStatus.Text = "";
                                txtDuree.Text = "";
                                txtDiplome.Text = "";
                                cmbUniversite.Text = "";
                                checkBox2.Checked = false;
                                checkBox1.Checked = false;
                                ListeStage();
                            }
                        }
                        else
                        {
                            GestionPharmacetique.MonMessageBox.ShowBox("Veuillez entrer la durée du stage", "Erreur");
                        }
                    }
                    else
                    {
                        GestionPharmacetique.MonMessageBox.ShowBox("Veuillez selectionner le statut de ce stage", "Erreur");
                    }
                }
                else
                {
                    GestionPharmacetique.MonMessageBox.ShowBox("Veuillez selectionner la nature du stage", "Erreur");
                }
            }
            catch (Exception Exception)
            { }
        }

                  void ListeStage()
        {
            try
            {
                dataGridView1.Rows.Clear();
                foreach (var st in AppCode.ConnectionClass.ListeDesStages(numeroStagiaire))
                {
                    dataGridView1.Rows.Add(
                        st.IDStage,
                        st.NatureStage,
                        st.DateDebut.ToShortDateString(),
                        st.DateFin.ToShortDateString(),st.Status
                        );
                }
            }
            catch { }
        }
        int idStage;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                idStage = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                var liste = from st in AppCode.ConnectionClass.ListeDesStages(numeroStagiaire)
                            where st.IDStage == idStage
                            select st;
                foreach (var st in liste)
                {                    
                    txtMontant.Text = st.Montant.ToString();
                    txtFaculte.Text = st.Faculte;
                    checkBox1.Checked = st.SiRenumere;
                    cmbNatureStage.Text = st.NatureStage;
                    cmbStatus.Text = st.Status;
                    dtpDateservice.Value = st.DateDebut;
                    dtpFinContrat.Value = st.DateFin;
                    cmbUniversite.Text = st.Universite;
                    cmbDirection.Text = st.Direction;
                    txtDiplome.Text = st.Diplome;
                    txtDuree.Text = st.Duree.ToString();
                    checkBox2.Checked = st.SiDiplome;
                }
            }
            else if (e.ColumnIndex == 6)
            {
                if (AppCode.ConnectionClass.SupprimerUnStage(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString())))
                    dataGridView1.Rows.Remove(dataGridView1.Rows[e.RowIndex]);
            }
            else if (e.ColumnIndex == 7)
            {
                idStage = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                var liste = from st in AppCode.ConnectionClass.ListeDesStages(numeroStagiaire)
                            where st.IDStage == idStage
                            select st;
                foreach (var stage in liste)
                {
                    stage.Nom = lblNomStagiaie.Text;
                    document = AppCode.Impression.AutorisationDeStage(stage);
                    if (printDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                        printPreviewDialog1.ShowDialog();
                    }
                    idStage = 0;
                }
            }
            else if (e.ColumnIndex == 8)
            {
                idStage = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                var liste = from st in AppCode.ConnectionClass.ListeDesStages(numeroStagiaire)
                            where st.IDStage == idStage
                            select st;
                foreach (var stage in liste)
                {
                    stage.Nom = lblNomStagiaie.Text;
                    document = AppCode.Impression.AttestationDeStage(stage);
                    if (printDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                        printPreviewDialog1.ShowDialog();
                    }
                }
            }
        }

        Bitmap document;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(document, 15, 15, document.Width, document.Height);
            e.HasMorePages = false;
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
        }
        private void txtSuperviseur_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ListePerso.indexRecherche = txtFaculte.Text;
                if (ListePerso.ShowBox() == "1")
                {
                    txtFaculte.Text=ListePerso.nomPersonnel;
                }
            }
        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                txtMontant.Visible = true;
            else
                txtMontant.Visible = false;
        }

        private void dtpFinContrat_ValueChanged(object sender, EventArgs e)
        {
            var duree = (dtpFinContrat.Value.Date.Subtract(dtpDateservice.Value.Date).Days + 1).ToString();
            txtDuree.Text = duree.ToString();
        }


    }
}
