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
    public partial class StagiaireFrm : Form
    {
        public StagiaireFrm()
        {
            InitializeComponent();
        }
             private void StagiaireFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(SystemColors.Control, 2);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.WhiteSmoke, Color.WhiteSmoke, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void groupBox1_Paint_1(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(SystemColors.Control, 2);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.FromArgb(185,210,199), Color.FromArgb(185,210,199), LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        public static StagiaireFrm frm;
        public static SGSP.AppCode.Stagiaire stagiaire;
       public  static int numeroStagiaire, width;
        static bool flag;
        private void btnExit_Click(object sender, EventArgs e)
        {
            stagiaire = null;
            flag = false;
            Dispose();
        }

        public static  bool ShowBox()
        {
            frm = new StagiaireFrm();
            frm.ShowDialog();
            return flag;
        }

       SGSP.AppCode.Stagiaire CreerUnStagiaire()
        {
            try
            {
                stagiaire = new SGSP.AppCode.Stagiaire();
                if (!string.IsNullOrEmpty(txtNom.Text) && !string.IsNullOrEmpty(txtPrenom.Text))
                {
                        if (!string.IsNullOrEmpty(txtLieuNaissance.Text))
                        {
                            DateTime dateNaissance;
                            var date = cmbJour.Text + "/" + cmbMois.Text + "/" + nUDAnnee.Value;
                            if (DateTime.TryParse(date, out dateNaissance))
                            {
                                if (rdbF.Checked)
                                {
                                    stagiaire.Sexe = "F";
                                }
                                else if (rdbM.Checked)
                                {
                                    stagiaire.Sexe = "M";
                                }
                                else
                                {
                                    GestionPharmacetique.MonMessageBox.ShowBox("Veuillez cocher pour le sexe du stagiaire", "Erreur");
                                    stagiaire = null;
                                }
                                stagiaire.Nom = txtNom.Text;
                                stagiaire.Prenom = txtPrenom.Text;
                                stagiaire.LieuNaissance = txtLieuNaissance.Text;
                                stagiaire.DateNaissance = dateNaissance;
                                stagiaire.Adresse = txtAdresse.Text;
                                stagiaire.Email = txtEmail.Text;
                                stagiaire.Telephone1 = txtPhone1.Text;
                                stagiaire.Telephone2 = txtPhone2.Text;
                                stagiaire.Matricule = txtMatricule.Text;
                                stagiaire.IDStagiaire = numeroStagiaire;
                            }
                            else
                            {
                                GestionPharmacetique.MonMessageBox.ShowBox("Veuillez selectionner la date de naissance", "Erreur");
                                stagiaire = null;
                            }                          
                        }
                        else
                        {
                            GestionPharmacetique.MonMessageBox.ShowBox("Veuillez saisir le lieu de naissance du stagiaire", "Erreur");
                            stagiaire = null;
                        }
                }
                else
                {
                    GestionPharmacetique.MonMessageBox.ShowBox("Veuillez saisir le nom et prenom du stagiaire", "Erreur");
                    stagiaire = null;
                }
                return stagiaire;
            }
            catch { return null; }
        }
        private void btnNouveauEtudiant_Click(object sender, EventArgs e)
        {
            stagiaire = CreerUnStagiaire();
            if (stagiaire != null)
            {
                if (AppCode.ConnectionClass.EnregistreUnStagiaire(stagiaire))
                {
                    flag = true;
                    frm.Dispose();
                }
            }
        }

        private void StagiareFrm_Load(object sender, EventArgs e)
        {
            try
            {
                if (numeroStagiaire > 0)
                {
                    var liste = from st in AppCode.ConnectionClass.ListeDesStagiaires()
                                where st.IDStagiaire == numeroStagiaire
                                select st;
                    foreach (var st in liste)
                    {
                        txtNom.Text = st.Nom;
                        txtPrenom.Text = st.Prenom;
                        txtLieuNaissance.Text = st.LieuNaissance;
                        txtNumeroEtudiant.Text = numeroStagiaire.ToString();
                        txtMatricule.Text = st.Matricule;
                        txtAdresse.Text = st.Adresse;
                        txtEmail.Text = st.Email;
                        txtPhone1.Text = st.Telephone1;
                        txtPhone2.Text = st.Telephone2;
                        if (st.Sexe == "M")
                        {
                            rdbM.Checked = true;
                        }
                        else
                        {
                            rdbF.Checked = true;
                        }
                        var jour= st.DateNaissance.Day.ToString();
                        if( st.DateNaissance.Day<10)
                            jour ="0"+ st.DateNaissance.Day;
                         var mois= st.DateNaissance.Month.ToString();
                        if( st.DateNaissance.Month<10)
                            jour ="0"+ st.DateNaissance.Month;
                        cmbJour.Text = jour;
                        cmbMois.Text =mois;
                        nUDAnnee.Value = st.DateNaissance.Year;
                    }
                }
                else
                {
                    var numero = SGSP.AppCode.ConnectionClass.ObtenirDernierNumeroStagiaire() + 1;
                    txtNumeroEtudiant.Text = numero.ToString();
                    txtMatricule.Text = numero.ToString() + "/" + DateTime.Now.Year;
                }
                //Location = new Point((width - Width) / 2, 100);
            }
            catch { }
        }

    }
    
}
