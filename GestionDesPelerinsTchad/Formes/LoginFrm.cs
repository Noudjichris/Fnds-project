using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SGSP.AppCode;
using GestionPharmacetique;

namespace GestionAcademique
{
    public partial class LoginFrm : Form
    {
        public LoginFrm()
        {
            InitializeComponent();
        }

        private void LoginFrm_Paint(object sender, PaintEventArgs e)
        {
            var  mGraphics = e.Graphics;
            var  pen1 = new Pen(Color.Silver, 0);
            var  area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            var  linearGradientBrush = new LinearGradientBrush(area1,
                Color.SteelBlue, Color.SteelBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        static LoginFrm loginFrm;
        static string btnClick;

        public static string ShowBox()
        {

            try
            {
                
                loginFrm = new LoginFrm();
                loginFrm.ShowDialog();
            }
            catch (Exception)
            {
            }
            return btnClick;
        }

        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            btnClick = "0";
            loginFrm.Dispose();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            loginFrm.progressBar1.Visible = true;
            loginFrm.lblEtat.Text = "Connection au serveur de la base de données...  ";
            loginFrm.lblEtat.Visible = true;
            loginFrm.timerProgresBar1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label3.Visible = true;
            label3.ForeColor = Color.Red;
            timer2.Start();
            timer1.Stop();
          
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            label3.Visible = true;
            label3.ForeColor = Color.FromArgb(64, 64, 64);
            timer1.Start();
            timer2.Stop();
        }

        public static string nomUtilisateur, typeUtilisateur, nom, photo, matricule;
        public static int  numUtilisateur;
        private void timerProgressBar2_Tick(object sender, EventArgs e)
        {
            try
            {
                if (loginFrm.progressBar2.Value < 100)
                {
                    loginFrm.progressBar2.Value++;
                    if (loginFrm.progressBar2.Value == 100)
                    {
                        loginFrm.progressBar1.Visible = false;
                    }
                }
                else
                {
                    loginFrm.timerProgressBar2.Dispose();
                    loginFrm.progressBar2.Visible = false;
                    loginFrm.progressBar2.Value = 0;
                    loginFrm.lblEtat.Visible = false;

                    var nomUtil = loginFrm.textBox1.Text.Trim();
                    var motPasse = loginFrm.txtMotPasse.Text.Trim().GetHashCode().ToString();
                    var utilisateur = ConnectionClass.SeConnecter(nomUtil, motPasse);
                    if (utilisateur != null)
                    {
                        typeUtilisateur = utilisateur.TypeUtilisateur;
                        nom = utilisateur.Nom + " " + utilisateur.Prenom;
                        matricule = utilisateur.Matricule;
                        nomUtilisateur = nomUtil;
                        numUtilisateur = utilisateur.NumUtilisateur;
                        photo = utilisateur.Photo;
                        ConnectionClass.Tracker(nomUtil, nom, true);
                        btnClick = "1";
                        Close();
                    }
                    else
                    {
                        var flag =false;
                        var liste = ConnectionClass.ListePersonnel(nomUtil);
                        if (liste.Count > 0)
                        {
                            foreach (var l in liste)
                            {
                                if (l.NumeroMatricule == txtMotPasse.Text)
                                {
                                    flag = true;
                                    typeUtilisateur = "utilisateur";
                                    nom = l.Nom + " " + l.Prenom;
                                    matricule = l.NumeroMatricule;
                                    nomUtilisateur = textBox1.Text;
                                    photo = l.Photo;
                                    ConnectionClass.Tracker(nomUtil, nom, true);
                                }
                            }
                        }
                        if (flag)
                        {                           
                            btnClick = "1";
                            Close();
                        }
                        else
                        {
                            ConnectionClass.Tracker(nomUtil, nom, false);
                            btnClick = "2";
                            timer1.Start();
                            timer2.Stop();
                            label3.ForeColor = Color.Red;
                            timer2.Start();
                            timer1.Stop();
                            btnClick = "0";
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void timerProgresBar1_Tick(object sender, EventArgs e)
        {
            if (loginFrm.progressBar1.Value < 100)
            {
                loginFrm.progressBar1.Value++;
                if (loginFrm.progressBar1.Value == 100)
                {
                    loginFrm.progressBar1.Visible = false;
                }
            }
            else
            {
                loginFrm.lblEtat.Text = "Connection établie  ";
                loginFrm.timerProgresBar1.Enabled = false;
                loginFrm.progressBar1.Value = 0;
                loginFrm.progressBar2.Visible = true;
                lblEtat.Text = "Vérification du mot de passe...";
                loginFrm.timerProgressBar2.Start();
            }
        }


        private void LoginFrm_Load(object sender, EventArgs e)
        {
            var listeUtilisateur = ConnectionClass.ListesDesUtilisateurs();
            if (listeUtilisateur.Count > 0)
            {
                textBox1.Enabled = true;
                textBox1.Focus();
            }
            txtMotPasse.Enabled = false;
        }

        private void txtMotPasse_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtMotPasse.Enabled == true)
            {
                if (e.KeyCode == Keys.Enter)
                {
btnLogin_Click(null,null);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            txtMotPasse.Enabled = true;
            btnLogin.Enabled = true;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(null, null);
            }
        }

    }
}
