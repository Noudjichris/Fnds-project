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
    public partial class InformationEtudiantFrm : Form
    {
        public InformationEtudiantFrm()
        {
            InitializeComponent();
        }
        public string nomEtudiant;
        public int numeroEtudiant;
        int idInfo;
        private void InformationEtudiantFrm_Load(object sender, EventArgs e)
        {
            button5.Location = new Point(Width - 43, 5);
            //Location=new Point((MainForm.width-Width)/2,10);
            clDisipline.Width = dataGridView1.Width / 5;
            clInstitution.Width = dataGridView1.Width /5;
            Column12.Width = 40;
            lbEtudiant.Text = "Informations " + nomEtudiant;
            ListeInformation();
            foreach (var i in AppCode.ConnectionClass.ListeDesEtablissements())
                cmbInstitution.Items.Add(i.NomEtablissment);
            for (var i = 2017; i < DateTime.Now.Year + 5; i++)
            {
                cmbAnneEntre.Items.Add(i.ToString());
                cmbAnneSortie.Items.Add(i.ToString());
              var debut=i;
                var fin =i+1;
                cmbAnneScolaire.Items.Add(debut + "-" + fin);
            }
        }

        void ListeInformation()
        {
            dataGridView1.Rows.Clear();
            foreach (AppCode.InformationEtudiant inf in AppCode.ConnectionClass.ListeInformationEtudiant(numeroEtudiant))
            {
                var liste = from ins in AppCode.ConnectionClass.ListeDesEtablissements()
                            where ins.NumerEtablissment ==inf.NumeroInstitution
                            select ins;
                string pays = "", ville = "";
                foreach (var et in liste)
                {
                    pays = et.Pays;
                    ville = et.Ville  ;
                    inf.Institution = et.NomEtablissment;
                }
                dataGridView1.Rows.Add
                    (
                    inf.NumerInformation,
                    inf.AnneeScolaire,
                    inf.Discipline,
                    inf.Institution,
                    pays,
                    ville,
                    inf.AnneeEntree,
                    inf.AnneeSortie,
                    inf.StatusEtudiant,
                    inf.StatusProgression,
                    inf.Niveau
                    );
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int debut, fin;
            if(Int32.TryParse(cmbAnneEntre.Text, out debut) && Int32.TryParse(cmbAnneSortie.Text, out fin))
            {
                if (!string.IsNullOrWhiteSpace(cmbInstitution.Text) && !string.IsNullOrWhiteSpace(txtDiscipline.Text))
                {
                    if (!string.IsNullOrWhiteSpace(cmbAnneScolaire.Text) && !string.IsNullOrEmpty(txtNiveau.Text))
                    {
                        var inf = new AppCode.InformationEtudiant();
                        inf.AnneeEntree = debut;
                        inf.AnneeScolaire = cmbAnneScolaire.Text;
                        inf.AnneeSortie = fin;
                        inf.Discipline = txtDiscipline.Text;
                        if (radioButton1.Checked)
                        {
                            inf.StatusProgression = "Boursier(e)";
                        }
                        else if (radioButton2.Checked)
                        {
                            inf.StatusProgression = "Non boursier(e)";
                        }
                        inf.Niveau = txtNiveau.Text;
                        inf.StatusEtudiant = cmbStatusInscrip.Text;
                         var liste = from ins in AppCode.ConnectionClass.ListeDesEtablissements()
                                    where ins.NomEtablissment == cmbInstitution.Text
                                    select ins.NumerEtablissment;
                        foreach (var ins in liste)
                            inf.NumeroInstitution=ins;
                        inf.NumeroEtudiant = numeroEtudiant;
                        inf.NumerInformation = idInfo;
                        if (AppCode.ConnectionClass.EnregistrerInformationEtudiant(inf))
                        {
                            txtDiscipline.Text = "";
                            txtNiveau.Text = "";
                            txtPays.Text = "";
                            txtVille.Text = "";
                            idInfo = 0;
                            ListeInformation();
                        }
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void InformationEtudiantFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.DodgerBlue, 2);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.AliceBlue, Color.AliceBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            idInfo = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            txtDiscipline.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            cmbAnneScolaire.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            cmbInstitution.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            cmbAnneEntre.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
            cmbAnneSortie.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
            cmbStatusInscrip.Text = dataGridView1.SelectedRows[0].Cells[8].Value.ToString();
            if (dataGridView1.SelectedRows[0].Cells[9].Value.ToString() == "Boursier(e)")
            {
                radioButton1.Checked = true;
            }
            else
            {
                radioButton2.Checked = false;
            }
            txtNiveau.Text = dataGridView1.SelectedRows[0].Cells[10].Value.ToString();
        }

        private void cmbInstitution_SelectedIndexChanged(object sender, EventArgs e)
        {
            var liste = from ins in AppCode.ConnectionClass.ListeDesEtablissements()
                        where ins.NomEtablissment == cmbInstitution.Text
                        select ins;
            foreach (var ins in liste)
            {
                txtPays.Text = ins.Pays;
                txtVille.Text = ins.Ville;
            }

        }
    }
}
