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
    public partial class EtudiantListeFrm : Form
    {
        public EtudiantListeFrm()
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

        private void EtudiantListeFrm_Load(object sender, EventArgs e)
        {
            button5.Location = new Point(Width - 43, 5);
            Column3.Width = dataGridView1.Width / 4;
            Column12.Width = 35; ;
            ListeEtudiant();
        }

        void ListeEtudiant()
        {
            try
            {
                dataGridView1.Rows.Clear();
                var liste = from et in AppCode.ConnectionClass.ListeDesEtudiants()
                            orderby et.Nom
                            select et;
                foreach (var et in liste)
                {
                    dataGridView1.Rows.Add(et.NumeroEtudiant, et.Matricule, et.Nom + " " + et.Prenom, et.DateNaissance.ToShortDateString(),
                        et.LieuDeNaissance,et.Sexe,et.Nationalite, et.Telephone1, et.Telephone2, et.Email,"Informations");
                }
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (EcoSoft.Formes.EtudiantFrm.ShowBox())
            {
                ListeEtudiant();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var frm = new UniveristeFrm();
            frm.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 11)
            {
                if (GestionPharmacetique.MonMessageBox.ShowBox("Voulez vous supprimer ces données? ", "Confirmation") == "1")
                {
                    if(AppCode.ConnectionClass.SupprimerUnEtudiant(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString())))
                    {
                        dataGridView1.Rows.Remove(dataGridView1.Rows[e.RowIndex]);
                    }
                }
            }
            else if (e.ColumnIndex == 10)
            {
                var frm = new InformationEtudiantFrm();
                frm.numeroEtudiant = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                frm.nomEtudiant = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                frm.Size = this.Size;
                frm.Location = this.Location;
                frm.ShowDialog();
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            EcoSoft.Formes.EtudiantFrm.numeroEtudiant = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
        if   (EcoSoft.Formes.EtudiantFrm.ShowBox())
            {
                ListeEtudiant();
            }
        }

    }
}
