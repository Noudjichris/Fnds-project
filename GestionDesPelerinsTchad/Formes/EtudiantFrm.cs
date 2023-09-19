using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EcoSoft.Formes
{
    public partial class EtudiantFrm : Form
    {
        public EtudiantFrm()
        {
            InitializeComponent();
        }

        private void EtudiantFrm_Paint(object sender, PaintEventArgs e)
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
        public static EtudiantFrm frm;
        public static SGSP.AppCode.Etudiant etudiant;
       public  static int numeroEtudiant, width;
        static bool flag;
        private void btnExit_Click(object sender, EventArgs e)
        {
            etudiant = null;
            flag = false;
            Dispose();
        }

        public static  bool ShowBox()
        {
            frm = new EtudiantFrm();
            frm.ShowDialog();
            return flag;
        }

       SGSP.AppCode. Etudiant CreerUnEtudiant()
        {
            try
            {
                etudiant = new SGSP.AppCode.Etudiant();
                if (!string.IsNullOrEmpty(txtNom.Text) && !string.IsNullOrEmpty(txtPrenom.Text))
                {
                    if (!string.IsNullOrEmpty(txtNationalite.Text))
                    {
                        if (!string.IsNullOrEmpty(txtLieuNaissance.Text))
                        {
                            DateTime dateNaissance;
                            var date = nUDJour.Value + "/" + nUDMois.Value + "/" + nUDAnnee.Value;
                            if (DateTime.TryParse(date, out dateNaissance))
                            {
                                if (rdbF.Checked)
                                {
                                    etudiant.Sexe = "F";
                                }
                                else if (rdbM.Checked)
                                {
                                    etudiant.Sexe = "M";
                                }
                                else
                                {
                                    GestionPharmacetique.MonMessageBox.ShowBox("Veuillez cocher pour le sexe de l'élève", "Erreur");
                                    etudiant = null;
                                }
                                etudiant.Nom = txtNom.Text;
                                etudiant.Prenom = txtPrenom.Text;
                                etudiant.Nationalite = txtNationalite.Text;
                                etudiant.LieuDeNaissance = txtLieuNaissance.Text;
                                etudiant.DateNaissance = dateNaissance;
                                etudiant.Adresse = txtAdresse.Text;
                                etudiant.Email = txtEmail.Text;
                                etudiant.Telephone1 = txtPhone1.Text;
                                etudiant.Telephone2 = txtPhone2.Text;
                                etudiant.Matricule = txtMatricule.Text;
                                etudiant.NumeroEtudiant = numeroEtudiant;
                            }
                            else
                            {
                                GestionPharmacetique.MonMessageBox.ShowBox("Veuillez selectionner la date de naissance", "Erreur");
                                etudiant = null;
                            }                          
                        }
                        else
                        {
                            GestionPharmacetique.MonMessageBox.ShowBox("Veuillez saisir le lieu de naissance de l'élève", "Erreur");
                            etudiant = null;
                        }
                    }
                    else
                    {
                        GestionPharmacetique.MonMessageBox.ShowBox("Veuillez saisir la nationalité de l'élève", "Erreur");
                        etudiant = null;
                    }
                }
                else
                {
                  GestionPharmacetique.  MonMessageBox.ShowBox("Veuillez saisir le nom et prenom de l'élève", "Erreur");
                    etudiant = null;
                }
                return etudiant;
            }
            catch { return null; }
        }
        private void btnNouveauEtudiant_Click(object sender, EventArgs e)
        {
            etudiant = CreerUnEtudiant();
            if (etudiant != null)
            {
                if(SGSP.AppCode.ConnectionClass.EnregistrerEtudiant(etudiant ))
                {
                    flag = true;
                    frm.Dispose();
                }
            }
        }

        private void EtudiantFrm_Load(object sender, EventArgs e)
        {
            try
            {
                if (numeroEtudiant > 0)
                {
                    var liste = from et in SGSP.AppCode.ConnectionClass.ListeDesEtudiants()
                                where et.NumeroEtudiant == numeroEtudiant
                                select et;
                    foreach (var etudiant in liste)
                    {
                        txtNom.Text = etudiant.Nom;
                        txtPrenom.Text = etudiant.Prenom;
                        txtNationalite.Text = etudiant.Nationalite;
                        txtLieuNaissance.Text = etudiant.LieuDeNaissance;
                        txtNumeroEtudiant.Text = numeroEtudiant.ToString();
                        txtMatricule.Text = etudiant.Matricule;
                        txtAdresse.Text = etudiant.Adresse;
                        txtEmail.Text = etudiant.Email;
                        txtPhone1.Text = etudiant.Telephone1;
                        txtPhone2.Text = etudiant.Telephone2;
                        if (etudiant.Sexe == "M")
                        {
                            rdbM.Checked = true;
                        }
                        else
                        {
                            rdbF.Checked = true;
                        }
                        nUDJour.Value = etudiant.DateNaissance.Day;
                        nUDMois.Value = etudiant.DateNaissance.Month;
                        nUDAnnee.Value = etudiant.DateNaissance.Year;
                    }
                }
                else
                {
                    var numero = SGSP.AppCode.ConnectionClass.ObtenirDernierNumeroEtudiant()+1;
                    txtNumeroEtudiant.Text = numero.ToString();
                    txtMatricule.Text = numero.ToString() + "/" + DateTime.Now.Year;
                }
                //Location = new Point((width - Width) / 2, 100);
            }
            catch { }
        }

    }
}
