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
    public partial class PartenaireFrm : Form
    {
        public PartenaireFrm()
        {
            InitializeComponent();
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
            Rectangle area1 = new Rectangle(0, 0, this.groupBox5.Width - 1, this.groupBox5.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                SystemColors.Control, SystemColors.Control, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void ProjetFrm_Load(object sender, EventArgs e)
        {
            btnFermer.Location = new Point(Width - 40, 4);

            var width = (dataGridView1.Width - 240) / 3;
            Column2.Width = width;
            Column3.Width = width;
            Column4.Width = width;
            Column5.Width = 90;
            Column6.Width = 80;
            Column7.Width = 80;
            txtNomProjet.Width=txtLocalisation.Width =cmbStatus.Width = width;
            cmbStatus.Location = new Point(width + 13, 55);
            txtLocalisation.Location = new Point(width * 2 + 14, 55);
            button2.Location = new Point(width * 3 + 20, 50);
            ListeDesProjets();
        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            Dispose();
        }
        int id;
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.RowTemplate.Height = 25;
                if (!string.IsNullOrEmpty(txtNomProjet.Text))
                {
                    var projet = new AppCode.Projet();
                    projet.NumeroPartenaire = id;
                    projet.Partenaire = txtNomProjet.Text; 
                    projet.TypePartenaire = cmbStatus.Text ;
                    projet.Localisation =txtLocalisation.Text;
                   if(AppCode.ConnectionClass.EnregistreUnPartenaire(projet))
                    {
                        ListeDesProjets();
                        txtLocalisation.Text="";
                        txtNomProjet.Text="";
                        cmbStatus.Text="";
                        id = 0;
                    }
                }
            }
            catch { }
        }

        void ListeDesProjets()
        {
            try
            {
                dataGridView1.Rows.Clear();
                foreach (var projet in AppCode.ConnectionClass.ListeDesPartenaires())
                {
                    dataGridView1.Rows.Add(projet.NumeroPartenaire, projet.Partenaire, projet.TypePartenaire, projet.Localisation, "Projets", "Modifier", "Supprimer");
                    Column5.DefaultCellStyle.BackColor = Color.Green;
                }
            }
            catch { }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                var frm = new ProjetFrm();
                frm.numeroPartenaire = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                frm.partenaire = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                frm.ShowDialog();
            }
            else if (e.ColumnIndex == 5)
            {
                id = Convert.ToInt32( dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                txtLocalisation.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtNomProjet.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                cmbStatus.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
            else if (e.ColumnIndex == 6)
            {
                if(GestionPharmacetique.MonMessageBox.ShowBox("Voulez vous supprimer ces données?","Confirmation")=="1")
                if(AppCode.ConnectionClass.SupprimerUnPartenaire(Convert.ToInt32( dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString())))
                {
                    dataGridView1.Rows.Remove(dataGridView1.Rows[e.RowIndex]);
                }
            }
        }

    }
}
