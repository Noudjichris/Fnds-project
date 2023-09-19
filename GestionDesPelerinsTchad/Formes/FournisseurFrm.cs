using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SGSP.AppCode;
using GestionPharmacetique;
namespace SGDP.Formes
{
    public partial class FournisseurFrm : Form
    {
        public FournisseurFrm()
        {
            InitializeComponent();
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(SystemColors.ControlDarkDark, 3);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, SystemColors.Control
                , SystemColors.Control, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
       
        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(SystemColors.ControlDarkDark, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox2.Width - 1, this.groupBox2.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, SystemColors.ControlDarkDark
                , SystemColors.ControlDark, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void FournisseurFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(SystemColors.ControlDarkDark, 3);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, SystemColors.Control
                , SystemColors.Control, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void btnValider_Click(object sender, EventArgs e)
        {
            try
            {

                var fournisseur = CreerUnFournisseur();
                if (fournisseur != null)
                {
                    etat = 1;
                    if (etat == 1)
                    {
                        if (ConnectionClass.CreerUnNouveauFournisseur(fournisseur))
                        {
                            flag = true;
                            Dispose();
                        }
                    }
                    else if (etat == 2)
                    {
                        if (MonMessageBox.ShowBox("Voulez vous modifier les données de ce fournisseur?","Confirmation") == "1")
                        {
                            if (ConnectionClass.ModifierUnFournisseur(fournisseur))
                            {
                                flag = true;
                                Dispose();
                            }
                        }
                    }
                }
            }
            catch { }
        }

        Fournisseur CreerUnFournisseur()
        {
            try
            {
                var fournisseur = new Fournisseur();
                if (string.IsNullOrEmpty(txtLabo.Text) || string.IsNullOrEmpty(txtAdresse.Text) || 
                    string.IsNullOrEmpty(txtTele1.Text) || string.IsNullOrEmpty(txtVille.Text ) || string.IsNullOrEmpty(txtPays.Text))
                {
                    MonMessageBox.ShowBox("Veuillez remplir les champs nom, adresse, téléphone 1, ville. et pays avant de continuer","Erreur");
                    return null;
                }
                if(!string.IsNullOrEmpty(txtEmail.Text) && !txtEmail.Text.Contains("@") && !txtEmail.Text.Contains("."))
                {
                   MonMessageBox.ShowBox("Veuillez entrer une bonne adresse pour le champ email s'il existe","Erreur");
                    return null;
                }

                fournisseur.Commentaire = txtCommentaire.Text;
                fournisseur.Adresse = txtAdresse.Text;
                fournisseur.NomFournisseur = txtLabo.Text;
                fournisseur.FAX = txtFax.Text;
                fournisseur.Email = txtEmail.Text;
                fournisseur.Pays = txtPays.Text;
                fournisseur.Telephone1 = txtTele1.Text;
                fournisseur.Telephone2 = txtTele2.Text;
                fournisseur.Ville = txtVille.Text;
                fournisseur.NumeroPostal = txtNoPostal.Text;
                fournisseur.ID = Convert.ToInt32(txtID.Text) ;
                fournisseur.Reference = txtRefLabo.Text;
                fournisseur.Telecopie = txtTelecopie.Text;
                fournisseur.NIF = txtNIF.Text;
                fournisseur.NoCompte = txtNoCompte.Text;
                fournisseur.Type = cmbDivision.Text;
                return fournisseur;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Creer un labo", ex);
                return null;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var ID = ConnectionClass.ObtenirLeDernierNumeroFournisseur() + 1;
                txtID.Text = ID.ToString();
                txtVille.Text = "";
                txtAdresse.Text = "";
                txtCommentaire.Text = "";
                txtEmail.Text = "";
                txtFax.Text = "";
                txtLabo.Text = "";
                txtNoPostal.Text = "";
                txtPays.Text = "";
                txtRefLabo.Text = "";
                txtTele1.Text = "";
                txtTele2.Text = "";
                txtVille.Text = "";

            }
            catch
            { }
        }

        static FournisseurFrm produitFrm;
        public static int ID, etat;
        public static bool flag;
        //public int 
        public static bool ShowBox()
        {
            produitFrm = new FournisseurFrm();
            produitFrm.ShowDialog();
            return flag;
        }
        
        private void txtLabo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtLabo.Text))
                {
                    txtLabo.Focus();
                }
                else
                {
                    txtRefLabo.Focus();
                }
            }
        }

        private void txtAdresse_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtAdresse.Text))
                {
                    txtAdresse.Focus();
                }
                else
                {
                    txtNoPostal.Focus();
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                txtLabo.Focus();
            }
        }

        private void txtNoPostal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtNoPostal.Text))
                {
                    txtNoPostal.Focus();
                }
                else
                {
                    txtFax.Focus();
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                txtAdresse.Focus();
            }
        }

        private void txtFax_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtFax.Text))
                {
                    txtFax.Focus();
                }
                else
                {
                    txtVille.Focus();
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                txtNoPostal.Focus();
            }
        }

        private void txtVille_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtVille.Text))
                {
                    txtVille.Focus();
                }
                else
                {
                    txtPays.Focus();
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                txtFax.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            flag = false;
            Dispose();
        }

        private void txtPays_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtPays.Text))
                {
                    txtTele1.Focus();
                }
                else
                {
                    txtPays.Focus();
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                txtVille.Focus();
            }
        }

        private void FournisseurFrm_Load(object sender, EventArgs e)
        {
            try
            {
                txtLabo.Focus();
                var liste = from f in ConnectionClass.ListeFournisseur()
                            where f.ID == ID
                            select f;
                    foreach(var f in liste)
                    {
                        txtAdresse.Text = f.Adresse;
                        txtCommentaire.Text = f.Commentaire;
                        txtEmail.Text = f.Email;
                        txtFax.Text = f.FAX;
                        txtID.Text = f.ID.ToString();
                        txtLabo.Text = f.NomFournisseur;
                        txtNoPostal.Text = f.NumeroPostal;
                        txtPays.Text = f.Pays;
                        txtRefLabo.Text = f.Reference;
                        txtTele1.Text = f.Telephone1;
                        txtTele2.Text = f.Telephone2;
                        txtVille.Text = f.Ville;
                        txtTelecopie.Text = f.Telecopie;
                        txtNoCompte.Text = f.NoCompte;
                        txtNIF.Text = f.NIF;
                    }
                    if (string.IsNullOrEmpty(txtID.Text))
                    {
                        txtID.Text = ID.ToString(); ;
                    }
            }
            catch { }
        }

        private void txtTele1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtTele1.Text))
                {
                    txtTele1.Focus();
                }
                else
                {
                    txtTele2.Focus();
                }
            }
            else if (e.KeyCode == Keys.Up)
            {
                txtPays.Focus();
            }
        }

        private void txtTele2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                    txtEmail.Focus();                
            }
            else if (e.KeyCode == Keys.Up)
            {
                txtTele1.Focus();
            }
        }

        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtEmail.Text) && txtEmail.Text.Contains("@") && txtEmail.Text.Contains("."))
                {
                    txtEmail.Focus();
                }
                else
                {
                    txtTelecopie.Focus();
                }
            }
            else if (e.KeyCode == Keys.Up)
            {txtCommentaire.Focus();
            }
        }

        private void txtCommentaire_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnValider.Focus();
            }
        }

        private void txtRefLabo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(txtRefLabo.Text))
                {
                    txtRefLabo.Focus();
                }
                else
                {
                    txtAdresse.Focus();
                }
            }
        }


        private void txtTelecopie_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCommentaire.Focus();
            }
            else if (e.KeyCode == Keys.Up)
            {
                txtEmail.Focus();
            }
        }
       
    }
}
