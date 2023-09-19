using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using SGSP.AppCode;
using GestionPharmacetique;

namespace SGSP.Formes
{
    public partial class AbsenceFrm : Form
    {
        public AbsenceFrm()
        {
            InitializeComponent();
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.DodgerBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.SteelBlue, Color.DodgerBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox4_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.AliceBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox4.Width - 1, this.groupBox4.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.AliceBlue, Color.AliceBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void groupBox6_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.AliceBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox6.Width - 1, this.groupBox6.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.AliceBlue, Color.AliceBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox5_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.AliceBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox5.Width - 1, this.groupBox5.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.AliceBlue, Color.AliceBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.AliceBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox2.Width - 1, this.groupBox2.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.SteelBlue, Color.DodgerBlue, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void groupBox3_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.AliceBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox3.Width - 1, this.groupBox3.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.SteelBlue, Color.DodgerBlue, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox7_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.AliceBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox7.Width - 1, this.groupBox7.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.SteelBlue, Color.DodgerBlue, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void AbsenceFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.White, 2);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                SystemColors.Control, SystemColors.Control, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void AbsenceFrm_Load(object sender, EventArgs e)
        {
            try
            {
                Column4.Width = 70;
                Column5.Width = 70;
                Column2.Width = 60;
                Column6.Width = 150;
                Column8.Width = 80;
                etat = "1";
                panel1.Location = new Point(dataGridView1.Width + 15, panel1.Location.Y);
                btnFermer.Location = new Point(Width - 40, 5);
         
                for (var i = 2017; i < DateTime.Now.Year + 10; i++)
                {
                    cmbExercice.Items.Add(i.ToString());
                }
                cmbExercice.Text =DateTime.Now.Year.ToString();
                ListeAbsence("");
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste  dep", ex);
            }

        }

        void ListeAbsence(string nom)
        {
            try 
            {
                dataGridView1.Rows.Clear();
                var liste = from a in ConnectionClass.ListeUneAbsence()
                            where a.Exercice == Convert.ToInt32(cmbExercice.Text)
                            select a;
                foreach (var a in liste)
                {
                    dataGridView1.Rows.Add
                        (
                        a.IDAbsence,   a.NumeroEmploye,a.NomPersonnel,
                        a.DateDebutAbscense.ToShortDateString(),  a.DateRetour.ToShortDateString(),
                       a.Duree, a.Motif,a.Destination,a.Exercice,a.SiPersonnelProjet,a.Fonction
                        );
                }
             }
            catch (Exception ex) { MonMessageBox.ShowBox("", ex); }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Dispose();
        }
        string idEMploye;
        private void txtEmploye_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    ListePerso.indexRecherche = txtEmploye.Text;
                    if(radioButton1.Checked)
                    ListePerso.siPersonnelProjet = false;
                    else if(radioButton2.Checked)
                        ListePerso.siPersonnelProjet = true;
                    if (ListePerso.ShowBox() == "1")
                    {
                        txtEmploye.Text = ListePerso.nomPersonnel;
                        txtFonction.Text = ListePerso.fonction;
                        idEMploye = ListePerso.numerMatricule;
                    }
                }
            }
            catch { }
        }
        string etat; int id;
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int duree;
                if (!string.IsNullOrEmpty(txtEmploye.Text) && !string.IsNullOrEmpty(txtMotif.Text))
                {
                    if (Int32.TryParse(txtDuree.Text , out duree))
                    {
                        var absence = new Absence();
                        absence.Motif = txtMotif.Text;
                        absence.IDAbsence = id;
                        absence.NomPersonnel = txtEmploye.Text;
                        absence.DateDemande = DateTime.Now;
                        absence.DateDebutAbscense = dtpDateservice.Value.Date;
                        absence.DateRetour = dtpFinContrat.Value.Date;
                        absence.Duree = duree;
                        absence.Destination = txtDestination.Text;
                        absence.Exercice =Int32.Parse(cmbExercice.Text);
                        absence.NumeroEmploye = idEMploye;
                        absence.Fonction = txtFonction.Text ;
                        absence.SiPersonnelProjet = radioButton2.Checked;
                        if (ConnectionClass.EnregistrerUneAbsence(absence))
                        {
                            etat = "1";
                            txtEmploye.Text = "";
                            txtMotif.Text = "";
                            txtEmploye.Text = "";
                            txtFonction.Text = "";
                            txtDestination.Text = "";
                            txtDuree.Text = "";
                            ListeAbsence("");
                            id = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("", ex);
            }
        }

        private void btnModifierPersonnel_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    idEMploye = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                    txtEmploye.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                    dtpDateservice.Value = DateTime.Parse(dataGridView1.SelectedRows[0].Cells[3].Value.ToString());
                    dtpFinContrat.Value = DateTime.Parse(dataGridView1.SelectedRows[0].Cells[4].Value.ToString());
                    txtDuree.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
                    txtDestination.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
                    txtMotif.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
                    cmbExercice.Text = dataGridView1.SelectedRows[0].Cells[8].Value.ToString();
                    if (Boolean.Parse(dataGridView1.SelectedRows[0].Cells[9].Value.ToString()))
                        radioButton2.Checked = true;
                    else
                        radioButton1.Checked = true;
                    txtFonction.Text=dataGridView1.SelectedRows[0].Cells[10].Value.ToString();
                }
            }
            catch { }
        }
        
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    if (MonMessageBox.ShowBox("Voulez vous supprimer ces données?", "Confirmation") == "1")
                    {
                        AppCode.ConnectionClass.SupprimerUneAbsence(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()));
                        dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);
                    }
                }
            }
            catch { }
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

        private void cmbExercice_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListeAbsence("");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    var liste = from absc in AppCode.ConnectionClass.ListeUneAbsence( )
                                where absc.IDAbsence == id 
                                select absc;
                    foreach (var stage in liste)
                    {
                        //stage.Nom = lblNomStagiaie.Text;
                        document = AppCode.Impression.AutorisationDAbsence(stage);
                        if (printDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                            printPreviewDialog1.ShowDialog();
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void dtpFinContrat_ValueChanged(object sender, EventArgs e)
        {
            if (dtpDateservice.Value.Date == dtpFinContrat.Value.Date)
            {
                txtDuree.Text = "1";
            }
            else
            {
                txtDuree.Text = (1 + dtpFinContrat.Value.Date.Subtract(dtpDateservice.Value.Date).Days).ToString();
            }
        }
   
    }
}
