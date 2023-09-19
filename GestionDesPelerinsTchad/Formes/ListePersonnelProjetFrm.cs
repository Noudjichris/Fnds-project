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
    public partial class ListePersonnelProjetFrm : Form
    {
        public ListePersonnelProjetFrm()
        {
            InitializeComponent();
        }
        private void groupBox5_Paint(object sender, PaintEventArgs e)
        {
            var mGraphics = e.Graphics;
            var pen1 = new Pen(Color.DodgerBlue, 2);
            var area1 = new Rectangle(0, 0, this.groupBox5.Width - 1, this.groupBox5.Height - 1);
            var linearGradientBrush = new LinearGradientBrush(area1,
                Color.DodgerBlue, Color.SteelBlue, LinearGradientMode.Horizontal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            var mGraphics = e.Graphics;
            var pen1 = new Pen(SystemColors.ControlLight, 1);
            var area1 = new Rectangle(0, 0, this.groupBox2.Width - 1, this.groupBox2.Height - 1);
            var linearGradientBrush = new LinearGradientBrush(area1,
                SystemColors.ControlLight, SystemColors.Control, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            var mGraphics = e.Graphics;
            var pen1 = new Pen(SystemColors.ControlLight, 2);
            var area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            var linearGradientBrush = new LinearGradientBrush(area1,
                SystemColors.ControlLight, SystemColors.Control, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox3_Paint(object sender, PaintEventArgs e)
        {
            var mGraphics = e.Graphics;
            var pen1 = new Pen(SystemColors.ControlLight, 2);
            var area1 = new Rectangle(0, 0, this.groupBox3.Width - 1, this.groupBox3.Height - 1);
            var linearGradientBrush = new LinearGradientBrush(area1,
                SystemColors.ControlLight, SystemColors.ControlLight, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        public string partenaire, nomProjet;
        private void ListePersonnelProjetFrm_Load(object sender, EventArgs e)
        {
            try
            {
                btnFermer.Location = new Point(Width - 45, btnFermer.Location.Y);
                cmbPartenaire.Items.Add("");
                cmbProjet.Items.Add("");
                foreach (var projet in AppCode.ConnectionClass.ListeDesPartenaires())
                {
                    cmbPartenaire.Items.Add(projet.Partenaire);
                }
                cmbPartenaire.Text = partenaire;
                foreach (var projet in AppCode.ConnectionClass.ListeDesProjets())
                {
                    cmbProjet.Items.Add(projet.NomProjet);
                }
                cmbProjet.Text = nomProjet;
                ListeDesPersonnels();
                cmbRegion.Items.Add("");
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
            catch { }
        }

        private void ListeDesPersonnels()
        {
           try
            {
                dataGridView1.Rows.Clear();
                var j = 0;
                var rang = 1;
                var countFeminin = 0;
                var countMasculin = 0;
                var listePersonnel = from p in ConnectionClass.ListePersonnelProjet(textBox1.Text)
                                     join s in ConnectionClass.ListeServicePersonnelProjet()
                                     on p.IDPersonelProjet equals s.IDPersonelProjet
                                     join pr in ConnectionClass.ListeDesProjets()
                                     on s.IDProjet equals pr.NumeroProjet
                                     join pat in ConnectionClass.ListeDesPartenaires()
                                     on pr.NumeroPartenaire equals pat.NumeroPartenaire
                                     where p.Nom.StartsWith(textBox1.Text, StringComparison.CurrentCultureIgnoreCase)
                                     where pr.NomProjet.StartsWith(cmbProjet.Text, StringComparison.CurrentCultureIgnoreCase)
                                     where pat.Partenaire.StartsWith(cmbPartenaire.Text , StringComparison.CurrentCultureIgnoreCase)
                                     where s.Region.StartsWith(cmbRegion.Text, StringComparison.CurrentCultureIgnoreCase)
                                     where s.Localite.StartsWith(txtLocalite.Text , StringComparison.CurrentCultureIgnoreCase)
                                     orderby p.Nom
                                     select new
                                     {p.IDPersonelProjet,
                                         p.NumeroMatricule,
                                         p.Nom,
                                         p.Prenom,
                                         s.Poste,
                                         s.Categorie,
                                         s.Echelon,
                                         s.Anciennete,
                                         s.TypeContrat,
                                         p.Sexe,
                                         EtatRetraite = s.Status,
                                         DateFinContrat = s.DateDepart,
                                         s.DateService,
                                         s.SalaireBrut,s.IDProjet
                                     };

                foreach (var p in listePersonnel)
                {


                    dataGridView1.Rows.Add(
                        p.IDPersonelProjet, p.Nom + " " + p.Prenom, p.Sexe, p.Poste, p.TypeContrat, p.Categorie, p.Echelon, p.Anciennete,
                      p.DateService.ToShortDateString(),
                       p.DateFinContrat.ToShortDateString(), p.SalaireBrut, p.IDProjet
                        );
                    if (p.Poste.ToUpper().Contains("CHEF") || p.Poste.ToUpper().Contains("DIRECT"))
                    {
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                    }
                    if (ConnectionClass.StatusContrat(p.EtatRetraite) || p.DateFinContrat <= DateTime.Now.Date)
                    {
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.Red;
                    }
                    j++;
                    rang++;
                    if (p.Sexe == "F")
                    {
                        countFeminin += 1;
                    }
                    else if (p.Sexe == "M")
                    {
                        countMasculin += 1;
                    }
                }
                lblNombre.Text = j.ToString();
                lblEleveFemele.Text = countFeminin.ToString();
                lblEleveMale.Text = countMasculin.ToString();
                lblPourcentFeminin.Text = Math.Round((double)countFeminin * 100 / j) + "%";
                lblMalePourcent.Text = Math.Round((double)countMasculin * 100 / j) + "%";
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("t", ex);
            }
        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            ListeDesPersonnels();
        }

        private void cmbPartenaire_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListeDesPersonnels();
        }

        private void cmbProjet_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListeDesPersonnels();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                var frm = new GestionDesPersonnelsTchad.PersonnelProjetFrm();
                frm.etat = "2";
                frm.idPersonnel=Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                frm.idProjet = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[11].Value.ToString());
                frm.ShowDialog();
                ListeDesPersonnels();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count>0)
            if (GestionPharmacetique.MonMessageBox.ShowBox("Voulez vous supprimer ces données?", "Confirmation") == "1")
            {
                if (ConnectionClass.SupprimerUnPersonnelProjet(Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString())))
                {
                    dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cmbProjet.Text))
            {
                var frm = new GestionDesPersonnelsTchad.PersonnelProjetFrm();
                foreach (var p in ConnectionClass.ListeDesProjets())
                {
                    if (p.NomProjet == cmbProjet.Text)
                    {
                        frm.idProjet = p.NumeroProjet;
                        frm.ShowDialog();
                    }
                }
              
            }
        }

        private void cmbRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListeDesPersonnels();
        }

        private void txtLocalite_TextChanged(object sender, EventArgs e)
        {
            ListeDesPersonnels();
        }

    }
}
