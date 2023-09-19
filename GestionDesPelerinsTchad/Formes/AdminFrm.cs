using System;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using GestionAcademique;
using SGSP.AppCode;
using GestionPharmacetique;

namespace SGSP.Formes
{
    public partial class AdminFrm : Form
    {
        public AdminFrm()
        {
            InitializeComponent();
        }

        private void AdminFrm_Paint(object sender, PaintEventArgs e)
        {
            var mGraphics = e.Graphics;
            var pen1 = new Pen(Color.Silver, 0);
            var area1 = new Rectangle(0, 0, Width - 1, Height - 1);
            var linearGradientBrush = new LinearGradientBrush(area1,
                Color.WhiteSmoke, Color.WhiteSmoke, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            var mGraphics = e.Graphics;
            var pen1 = new Pen(Color.SteelBlue, 0);
            var area1 = new Rectangle(0, 0, groupBox1.Width - 1, groupBox1.Height - 1);
            var linearGradientBrush = new LinearGradientBrush(area1,
                Color.SteelBlue, Color.SteelBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void AdminFrm_Load(object sender, EventArgs e)
        {
            Size = new Size(1044, 430);
            Controls.Remove(groupBox2);
            ListeDesUtilisateurs();

            foreach (var d in ConnectionClass.ListeDepartement())
            {
                comboBox1.Items.Add(d.Departement);
            }
        }

        void ListeDesUtilisateurs()
        {
            try
            {
                dataGridView1.Rows.Clear();
                var dt = ConnectionClass.ListeUtilisteur();
                foreach (DataRow dtRow in dt.Rows)
                {
                    dataGridView1.Rows.Add(
                        dtRow.ItemArray[0].ToString(),
                        dtRow.ItemArray[1].ToString(),
                          dtRow.ItemArray[2].ToString(),
                        dtRow.ItemArray[3].ToString(),
                          dtRow.ItemArray[4].ToString(),
                        dtRow.ItemArray[5].ToString()
                        );
                }
            }
            catch (Exception)
            {

            }
        }

        //supprimer un utilisateur
        private void button1_Click(object sender, EventArgs e)
        {
            if (LoginFrm.ShowBox() == "1")
            {if (LoginFrm.typeUtilisateur == "admin")
                {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    if (
                        MonMessageBox.ShowBox(
                            "Voulez vous vous supprimer les données de l'utilisateur " +
                            dataGridView1.SelectedRows[0].Cells[3].Value.ToString() + "?", "Confirmation") == "1")
                    {
                        ConnectionClass.SupprimerUnUtilisateur(
                            Int32.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()));
                        ListeDesUtilisateurs();

                    }
                }
                 }
                else
                {
                    MonMessageBox.ShowBox("Vous etes pas autorisés à faire cette opération", "Avertissement");
                }
            }
        }

        //rechercher personnel par la division
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                comboBox2.Items.Clear();
                var dtPersonnel = ConnectionClass.ListeDesPersonnelsParDepartement(comboBox1.Text);
                foreach (DataRow dtRow in dtPersonnel.Rows)
                {
                    comboBox2.Items.Add(
                         dtRow.ItemArray[1].ToString() + " " +
                         dtRow.ItemArray[2].ToString());

                }
            }
            catch { }
        }

        private void btnAjouterUneAgence_Click(object sender, EventArgs e)
        {
           
                if (btnAjouterUneAgence.Text == "AJOUTER UTILISATEUR")
                {
                    label2.Visible = true;
                    label3.Visible = true;
                    comboBox1.Visible = true;
                    comboBox2.Visible = true;
                    btnAjouterUneAgence.Text = "ENREGISTRER";
                }
                else if (btnAjouterUneAgence.Text == "ENREGISTRER")
                {
                    if (LoginFrm.ShowBox() == "1")
                    {
                        if (LoginFrm.typeUtilisateur == "admin")
                        {
                            var nom = comboBox2.Text.Substring(0, comboBox2.Text.IndexOf(" "));
                            var prenom = comboBox2.Text.Substring(comboBox2.Text.IndexOf(" ") + 1);
                            var dt = ConnectionClass.ListePersonnel(nom);
                            var numMatricule = dt[0].NumeroMatricule;
                            var utilisateur = new Utilisateur(nom, numMatricule.ToString().GetHashCode().ToString(),
                                "utilisateur",
                                numMatricule);
                            if (ConnectionClass.AjouterUnUtilisateur(utilisateur))
                            {
                                ListeDesUtilisateurs();
                                label2.Visible = false;
                                label3.Visible = false;
                                comboBox1.Visible = false;
                                comboBox2.Visible = false;
                                btnAjouterUneAgence.Text = "AJOUTER UTILISATEUR";
                            }
                        }
                    }
                    else
                    {
                        MonMessageBox.ShowBox("Vous etes pas autorisés à faire cette opération", "Avertissement");
                    }
                }
                    
                  
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close()
            ;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (LoginFrm.ShowBox() == "1")
            {
                if (LoginFrm.typeUtilisateur == "admin")
                {
                var frm = new TrackFrm();
                frm.ShowDialog();
                 }
                else
                {
                    MonMessageBox.ShowBox("Vous etes pas autorisés à faire cette opération", "Avertissement");
                }
            }
        }

        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            var mGraphics = e.Graphics;
            var pen1 = new Pen(Color.SteelBlue, 0);
            var area1 = new Rectangle(0, 0, groupBox1.Width - 1, groupBox1.Height - 1);
            var linearGradientBrush = new LinearGradientBrush(area1,
                Color.SteelBlue, Color.SteelBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void btnModifier_Click(object sender, EventArgs e)
        {
            if (LoginFrm.ShowBox() == "1")
            {
                if (LoginFrm.typeUtilisateur == "admin")
                {
                    Size = new Size(1044, 485);
                    Controls.Add( groupBox2);
                }
            }
        }


        private void button4_Click(object sender, EventArgs e)
        {var id=
            Int32.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            var nomUtil = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            var motPasse = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            var type = comboBox3.Text;
            var utilisateur = new Utilisateur(id,nomUtil,motPasse,type);
            if (ConnectionClass.ModifierUnUtilisateur(utilisateur))
            {
                Size = new Size(1044, 430);
                Controls.Remove(groupBox2);
                ListeDesUtilisateurs();
            }
        }
    }
}
