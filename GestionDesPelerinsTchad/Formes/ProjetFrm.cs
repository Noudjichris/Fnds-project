using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SGSP.AppCode;
namespace SGSP.Formes
{
    public partial class ProjetFrm : Form
    {
        public ProjetFrm()
        {
            InitializeComponent();
        }

        private void ProjetFrm_Load(object sender, EventArgs e)
        {
            dataGridView1.RowTemplate.Height = 28;
            btnFermer.Location = new Point(Width - 40, 4);
            Location = new Point((MainForm.width - Width) / 2, 140);
            cmbPartenaire.Items.Add("");

            foreach (var projet in AppCode.ConnectionClass.ListeDesPartenaires())
            {
                cmbPartenaire.Items.Add(projet.Partenaire);
            }
            ListeDesProjets();
            cmbPartenaire.Text = partenaire;
            var region = new string[]
          {
              "Batha", "Chari-Baguirmi","Hadjer-Lamis","Wadi Fira","Bahr elGazel","Borkou","Ennedi-Ouest","Ennedi-Est","Guéra","Kanem",
              "Lac","Logone Occidental","Logone Oriental","Mandoul","Mayo-Kebbi Est","Mayo-Kebbi Ouest","Moyen Chari",
              "Ouaddaï","Salamat","Sila","Tandjilé","Tibesti","N'Djamena"
          };
            var regions = from s in region
                          orderby s
                          select s;
            foreach (var s in regions)
                cmbRegion.Items.Add(s);
        }
        public string partenaire;
        public int numeroPartenaire;
        private void btnFermer_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.Silver, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                SystemColors.ButtonFace, SystemColors.ButtonFace, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox5_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.Silver, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox5.Width - 1, this.groupBox5.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.DodgerBlue, Color.SteelBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void ProjetFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.Silver, 0);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                SystemColors.ControlLightLight, SystemColors.Control, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.Silver, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox2.Width - 1, this.groupBox2.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                SystemColors.ButtonFace, SystemColors.ButtonFace, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox3_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.Silver, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox3.Width - 1, this.groupBox3.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                SystemColors.ButtonFace, SystemColors.ButtonFace, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(cmbPartenaire.Text))
                {
                    GestionPharmacetique.MonMessageBox.ShowBox("Veuillez selectionner le nom partenaire sur la liste deroulante", "Erreur");
                    return;
                }
                if(string.IsNullOrEmpty(txtNomProjet.Text))
                {
                    GestionPharmacetique.MonMessageBox.ShowBox("Veuillez entrer le nom du projet avant de continuer", "Erreur");
                    return;
                }
                if (dtp1.Value.Date <= dtp2.Value.Date)
                {
                    //GestionPharmacetique.MonMessageBox.ShowBox("La date de fin doit être differente et supérieur à la date de debut du projet", "Erreur");
                    //return;
                }
                if (string.IsNullOrEmpty(comboBox1.Text))
                {
                    GestionPharmacetique.MonMessageBox.ShowBox("Veuillez selectionner le status d projet sur la liste deroulante", "Erreur");
                    return;
                }
                var liste = from p in ConnectionClass.ListeDesPartenaires()
                            where p.Partenaire == cmbPartenaire.Text
                            select p.NumeroPartenaire;

                var projet = new Projet();
                projet.NumeroProjet = idProjet;
                projet.Status = comboBox1.Text;
                projet.Description = txtDescription.Text;
                projet.NomProjet = txtNomProjet.Text;
                projet.Etat = checkBox1.Checked;
                projet.DateDebut = dtp1.Value.Date;
                projet.DateFin = dtp2.Value.Date;
                foreach (var p in liste)
                {
                    var count = liste.Count();
                    projet.NumeroPartenaire = p;
                }
                if (ConnectionClass.EnregistrerUnProjet(projet))
                {
                    ListeDesProjets();
                    idProjet = 0;
                    txtNomProjet.Text = "";
                    txtDescription.Text = "";
                    comboBox1.Text = "";
                    dtp2.Value = DateTime.Now;
                    dtp1.Value = DateTime.Now;
                    checkBox1.Checked = false;
                }
                           
            }
            catch { }
        }

        void ListeDesProjets()
        
        {
            try
            {
                dataGridView1.Rows.Clear();
                if (numeroPartenaire == 0)
                {
                    foreach (var p in ConnectionClass.ListeDesProjets())
                    {
                        var date = p.DateFin.ToShortDateString();
                        if (p.Etat)
                            date = "Inconnue";
                        dataGridView1.Rows.Add(p.NumeroProjet, p.NomProjet, p.Description, p.Etat, p.DateDebut.ToShortDateString(), date, p.Status);
                    }
                }
                else
                {
                    foreach (var p in ConnectionClass.ListeDesProjets(numeroPartenaire))
                    {
                        var date = p.DateFin.ToShortDateString();
                        if (p.Etat)
                            date = "Inconnue";
                        dataGridView1.Rows.Add(p.NumeroProjet, p.NomProjet, p.Description, p.Etat, p.DateDebut.ToShortDateString(), date, p.Status);
                    }
                }
            }
            catch { }
        }
        int idProjet;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 7)
                {
                    idProjet = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                    txtDescription.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    txtNomProjet.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    dtp1.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());

                    comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                  
                    checkBox1.Checked = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());
                    if (checkBox1.Checked)
                        dtp2.Value = dtp1.Value.Date.AddYears(100);
                    else
                        dtp2.Value = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString());
                }
                else if (e.ColumnIndex == 8)
                {
                    if (GestionPharmacetique.MonMessageBox.ShowBox("Voulez vous supprimer ces données?", "Confirmation") == "1")
                    {
                        if (ConnectionClass.SupprimerUnProjet(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString())))
                            dataGridView1.Rows.Remove(dataGridView1.Rows[e.RowIndex]);
                    }
                }
            }
            catch { }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                label8.Text = "Illimité";
                dtp2.Visible = false;
                dtp2.Value = dtp1.Value.Date.AddYears(100);
            }
            else
            {
                label8.Text = "Au";
                dtp2.Visible = true;
                                dtp2.Value = dtp1.Value.Date;
            }
        }

        void ListeRegion()
        {
            try
            {
                dataGridView2.Rows.Clear();
                foreach (var reg in ConnectionClass.ListeDesRegions(idProjetRegion))
                {
                    dataGridView2.Rows.Add(reg.IDRegion, reg.NumeroProjet, reg.Region, reg.Localisation);
                }
            }

            catch { }
        }
        int idProjetRegion, idRegion;
        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                txtLocalisation.Enabled = true;
                cmbRegion.Enabled = true;
                idProjetRegion = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                ListeRegion();
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
       if (e.ColumnIndex == 4)
            {
                idRegion = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString());
                idProjetRegion = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString());
                cmbRegion.Text = dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtLocalisation.Text = dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtLocalisation.Enabled = true;
                cmbRegion.Enabled = true;
            }
            else if (e.ColumnIndex == 5)
            {
                if (GestionPharmacetique.MonMessageBox.ShowBox("Voulez vous supprimer ces données?", "Confirmation") == "1")
                {
                    if (ConnectionClass.SupprimerUneRegion(Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString())))
                    {
                        dataGridView2.Rows.Remove(dataGridView2.Rows[e.RowIndex]);
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(cmbRegion.Text))
                {
                    var region = new Projet();
                    region.IDRegion = idRegion;
                    region.Region = cmbRegion.Text;
                    region.Localisation = txtLocalisation.Text;
                    region.NumeroProjet = idProjetRegion;
                    if (ConnectionClass.EnregistreUneRegion(region))
                    {
                        idRegion = 0;
                        txtLocalisation.Text = "";
                        cmbRegion.Text = "";
                        cmbRegion.Enabled = false;
                        txtLocalisation.Enabled = false;
                        ListeRegion();
                        idProjetRegion = 0;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var frm = new GestionDesPersonnelsTchad.PersonnelProjetFrm();
                frm.idProjet = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                frm.ShowDialog();
            }
        }

        private void cmbPartenaire_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var p in ConnectionClass.ListeDesPartenaires())
            {
                if (p.Partenaire == cmbPartenaire.Text)
                {
                    numeroPartenaire = p.NumeroPartenaire;
                    ListeDesProjets();
                }
            }
        }

        
    }
}
