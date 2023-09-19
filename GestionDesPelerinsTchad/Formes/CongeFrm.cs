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
    public partial class CongeFrm : Form
    {
        public CongeFrm()
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
       public int indexState;
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

        private void CongeFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.White, 2);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                SystemColors.Control, SystemColors.Control, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void CongeFrm_Load(object sender, EventArgs e)
        {
            try
            {
                var natureConge = new string[]
                {
                   "Télétravail","Congé administratif régulier ", "Congé de récupérations",  "Congé de maladie", "Arrêt de travail simplifié",
                    "Congé pour cas sociaux","Congé epargne temps","Congé de maternité","Congé de paternité","Formation","Déplacement","Congé sans solde"
                };
                dudNatureConge.Items.AddRange(natureConge);

                cmbAnne.Items.Clear();
                for (var i = 2017; i < DateTime.Now.Year + 10; i++)
                {
                    cmbAnne.Items.Add(i.ToString());
                }
                cmbAnne.Text = DateTime.Now.Year.ToString();
                etat = "1";
                Column3.Width = dataGridView1.Width / 3;
          
            ListeConge("");
            Column2.Width = dataGridView1.Width / 4;
            Column4.Width = dataGridView1.Width / 4;
            Column9.Width = 40;
            Column10.Width = 40;
            Column11.Width = 50;
            button1.Location = new Point(Width - button1.Width - 5, button1.Location.Y);
            button5.Location = new Point(Width - button5.Width - 5, button5.Location.Y);
            dataGridView1.RowTemplate.Height = 25;
            }
            catch (Exception ex)
            {
               GestionPharmacetique. MonMessageBox.ShowBox("Liste  dep", ex);
            }

        }
        string etat; int  idConge;
        void ListeConge(string nom)
        {
            try 
            {
                if (indexState == 2)
                {
                    dataGridView1.Rows.Clear();
                    var liste = from l in AppCode.ConnectionClass.ListeConge()
                                where l.NomPersonnel.StartsWith(nom, StringComparison.CurrentCultureIgnoreCase)
                                where l.DateRetour <= DateTime.Now.AddDays(7)
                                where l.Exercice == Convert.ToInt32(cmbAnne.Text)
                                orderby l.IDConge descending
                                select l;

                    foreach (var a in liste)
                    {

                        dataGridView1.Rows.Add
                            (
                            a.IDConge,
                            a.NumeroMatricule,
                            a.NomPersonnel,
                            a.NatureConge,
                            a.DateDemande.ToShortDateString(),
                            a.DateDebutConge.ToShortDateString(),
                            a.DateRetour.ToShortDateString(),
                            a.Duree,
                            a.Mois,
                            a.Exercice,
                            a.SiProjet,
                            a.Fonction
                            );
                    }
                }else{
                dataGridView1.Rows.Clear();
                var liste = from l in AppCode. ConnectionClass.ListeConge()
                            where l.NomPersonnel.StartsWith(nom, StringComparison.CurrentCultureIgnoreCase)
                            where l.Exercice==Convert.ToInt32(cmbAnne.Text)
                            orderby l.IDConge descending
                            select l;

                foreach (var a in liste)
                {

                    dataGridView1.Rows.Add
                        (
                        a.IDConge,
                        a.NumeroMatricule,
                        a.NomPersonnel,
                        a.NatureConge,
                        a.DateDemande.ToShortDateString(),
                        a.DateDebutConge.ToShortDateString(),
                        a.DateRetour.ToShortDateString(),
                        a.Duree,
                        a.Mois,
                        a.Exercice,
                        a.SiProjet,
                        a.Fonction
                        );
                }
                }
             }
            catch (Exception ex) {GestionPharmacetique. MonMessageBox.ShowBox("", ex); }
        }

        private void txtEmploye_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {

                    ListePerso.indexRecherche = txtEmploye.Text;
                    if (radioButton1.Checked)
                        ListePerso.siPersonnelProjet = false;
                    else if (radioButton2.Checked)
                        ListePerso.siPersonnelProjet = true;
                    ListePerso.indexRecherche = txtEmploye.Text;
                    if (ListePerso.ShowBox() == "1")
                    {
                        txtEmploye.Text = ListePerso.nomPersonnel;
                        txtFonction.Text = ListePerso.fonction;
                        txtMatricule.Text = ListePerso.numerMatricule;
                    }
                }
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtMatricule.Text))
                {
                    if (!string.IsNullOrEmpty(cmbMois.Text) && !string.IsNullOrEmpty(cmbAnne.Text))
                    {
                        int duree;
                        if (int.TryParse(txtDuree.Text, out duree))
                        {
                        }
                        else { duree = 0; }

                        var conge = new AppCode.Conge();
                        conge.NatureConge = dudNatureConge.Text;
                        conge.IDConge = idConge;
                        conge.NumeroMatricule = txtMatricule.Text;
                        conge.Mois = cmbMois.Text;
                        conge.SiProjet = radioButton2.Checked;
                        conge.NomPersonnel = txtEmploye.Text;
                        conge.Exercice = Convert.ToInt32(cmbAnne.Text);
                        conge.DateDebutConge = dtp1.Value.Date;
                        conge.DateRetour = dtp2.Value.Date;
                        conge.DateDemande = dtpDemande.Value.Date;
                        conge.Duree = duree;
                        conge.Fonction = txtFonction.Text;
                        if (AppCode.Impression.ObtenirMois(cmbMois.Text) == conge.DateDebutConge.Month)
                        {
                            if (AppCode.ConnectionClass.EnregistrerUnConge(conge, etat))
                            {
                                etat = "1";
                                txtMatricule.Text = "";
                                dudNatureConge.Text = "";
                                txtEmploye.Text = "";
                                txtFonction.Text = "";
                                dtp1.Value = DateTime.Now;
                                dtp2.Value = DateTime.Now;
                                dtpDemande.Value = DateTime.Now;
                                txtDuree.Text = "";
                                ListeConge("");

                            }
                        }
                        else
                        {
                            GestionPharmacetique.MonMessageBox.ShowBox("La date et le mois de congé ne sont pas compatible", "Erreur");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
              GestionPharmacetique.  MonMessageBox.ShowBox("", ex);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void cmbAnne_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListeConge("");
        }

        private void cmbMois_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                var liste = from l in AppCode.ConnectionClass.ListeConge()
                            where l.Mois.StartsWith(cmbMois.Text, StringComparison.CurrentCultureIgnoreCase)
                            where l.Exercice == Convert.ToInt32(cmbAnne.Text)
                            orderby l.NomPersonnel
                            select l;

                foreach (var a in liste)
                {

                    dataGridView1.Rows.Add
                        (
                               a.IDConge,
                        a.NumeroMatricule,
                        a.NomPersonnel,
                        a.NatureConge,
                        a.DateDemande.ToShortDateString(),
                        a.DateDebutConge.ToShortDateString(),
                        a.DateRetour.ToShortDateString(),
                        a.Duree,
                        a.Mois,
                        a.Exercice,
                        a.SiProjet,
                        a.Fonction
                        );
                }
            }
            catch (Exception ex) { GestionPharmacetique.MonMessageBox.ShowBox("", ex); }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 12)
                {
                    idConge = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                    txtEmploye.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    txtMatricule.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    dtpDemande.Value = DateTime.Parse(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
                    dtp1.Value=DateTime.Parse(dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString());
                    dtp2.Value= DateTime.Parse(dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString());
                    dudNatureConge.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                    cmbMois.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                    cmbAnne.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                    txtDuree.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                    txtFonction.Text = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
                    if (bool.Parse(dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString()))
                        radioButton2.Checked = true;
                    else
                        radioButton1.Checked = true;
                    etat = "2";
                }
                else if (e.ColumnIndex ==13)
                {
                    if (GestionPharmacetique.MonMessageBox.ShowBox("Voulez vous supprimer ces données?", "Confirmation") == "1")
                    {
                        AppCode.ConnectionClass.SupprimerUnConge(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()));
                        dataGridView1.Rows.Remove(dataGridView1.Rows[e.RowIndex]);
                    }
                }
                else if (e.ColumnIndex == 14)
                {
                        if (dataGridView1.SelectedRows.Count > 0)
                        {
                            var id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                            var liste = from conge in AppCode.ConnectionClass.ListeConge()
                                        where conge.IDConge == id
                                        select conge;
                            foreach (var conge in liste)
                            {
                                //stage.Nom = lblNomStagiaie.Text;
                                document = AppCode.Impression.LettreConge(conge);
                                if (printDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                                {
                                    printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                                    printPreviewDialog1.ShowDialog();
                                }
                            }
                        }
                }
            }
            catch { }
        }

        private void dtp1_ValueChanged(object sender, EventArgs e)
        {
            if (dtp1.Value.Date == dtp2.Value.Date)
            {
                txtDuree.Text = "1";
            }
            else
            {
                txtDuree.Text = (1+dtp2.Value.Date.Subtract(dtp1.Value.Date).Days).ToString();
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


    }
}
