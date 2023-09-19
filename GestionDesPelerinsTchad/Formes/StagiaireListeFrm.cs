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
    public partial class StagiaireListeFrm : Form
    {
        public StagiaireListeFrm()
        {
            InitializeComponent();
        }

        private void StagiaireListeFrm_Load(object sender, EventArgs e)
        {
            button5.Location = new Point(Width - 43, 5);
            Column3.Width = dataGridView1.Width / 4;
            Column6.Width = 50;
            Column12.Width = 35; 
            ListeStagiaires();
        }

        void ListeStagiaires()
        {
            try
            {
                dataGridView1.Rows.Clear();
                var liste = from et in AppCode.ConnectionClass.ListeDesStagiaires()
                            orderby et.Nom, et.Prenom
                            select et;
                foreach (var et in liste)
                {
                    dataGridView1.Rows.Add(et.IDStagiaire, et.Matricule, et.Nom + " " + et.Prenom, et.DateNaissance.ToShortDateString(),
                        et.LieuNaissance, et.Sexe,et.Adresse, et.Telephone1, et.Telephone2, et.Email, "Stage");
                }
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (StagiaireFrm.ShowBox())
            {
                ListeStagiaires();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Dispose();
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

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            StagiaireFrm.numeroStagiaire = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            if (StagiaireFrm.ShowBox())
            {
                ListeStagiaires();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                try
                {
                    dataGridView1.Rows.Clear();
                    var liste = from et in AppCode.ConnectionClass.ListeDesStagiaires()
                                join ets in AppCode.ConnectionClass.ListeDesStages()
                                on et.IDStagiaire equals ets.IDStagiaire
                                where ets.DateFin >= DateTime.Now.Date
                                orderby et.Nom, et.Prenom
                                select new { et .IDStagiaire , et.Nom , et.Prenom ,et.DateNaissance, et.Matricule,et.LieuNaissance, et.Sexe,et.Adresse,et.Telephone1,et.Telephone2,et.Email};
                    foreach (var et in liste)
                    {
                        dataGridView1.Rows.Add(et.IDStagiaire, et.Matricule, et.Nom + " " + et.Prenom, et.DateNaissance.ToShortDateString(),
                            et.LieuNaissance, et.Sexe, et.Adresse, et.Telephone1, et.Telephone2, et.Email, "Stage");
                    }
                }
                catch { }
            }
            else
            {
                ListeStagiaires();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 10)
            {
                var frm = new StageFrm();
                frm.numeroStagiaire = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                frm.nomStagiaire = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                frm.ShowDialog();
            }
            else if (e.ColumnIndex == 11)
            {
                if (GestionPharmacetique.MonMessageBox.ShowBox("Voulez vous supprimer ces données? ", "Confirmation") == "1")
                {
                    if (AppCode.ConnectionClass.SupprimerUnStagiaire(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString())))
                        dataGridView1.Rows.Remove(dataGridView1.Rows[e.RowIndex]);
                }
            }
            else if (e.ColumnIndex == 12)
            {
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "PDF Documents (*.pdf)|*.pdf";

                    sharpPDF.pdfDocument pdfDocument = new sharpPDF.pdfDocument("christian", "cdali");
                    var date = DateTime.Now.ToString().Replace(":", "_");
                    date = date.Replace("/", "_");
                    var titreImpression = "Liste des stagiaires";
                        if(checkBox1.Checked)
                        {
                            titreImpression = "Liste des stagiaires actifs";
                        }
                    var pathFolder = "C:\\Dossier Personnel";
                    if (!System.IO.Directory.Exists(pathFolder))
                    {
                        System.IO.Directory.CreateDirectory(pathFolder);
                    }
                    pathFolder = pathFolder + "\\Liste stagiaire";
                    if (!System.IO.Directory.Exists(pathFolder))
                    {
                        System.IO.Directory.CreateDirectory(pathFolder);
                    }
                    sfd.InitialDirectory = pathFolder;
                    sfd.FileName = titreImpression + "_" + date + ".pdf";

                    var div = dataGridView1.Rows.Count / 44;
                    for (var i = 0; i <= div; i++)
                    {                        
                           var  document =AppCode. Impression.ImprimerLalisteDesStagiaires(dataGridView1, titreImpression, i);                        
                        sharpPDF.pdfPage pageIndex = pdfDocument.addPage();
                        var inputImage = @"cdali" + i;
                        pdfDocument.addImageReference(document, inputImage);
                        sharpPDF.Elements.pdfImageReference img1 = pdfDocument.getImageReference(inputImage);
                        pageIndex.addImage(img1, 0, 0, pageIndex.height, pageIndex.width);
                    }
                    pdfDocument.createPDF(sfd.FileName);
                    System.Diagnostics.Process.Start(sfd.FileName);
                }

            }
            catch (Exception)
            {
            }
        }

    }
}
