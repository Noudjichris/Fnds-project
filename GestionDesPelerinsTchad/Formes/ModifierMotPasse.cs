using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GestionAcademique;
using SGSP.AppCode;
using GestionPharmacetique;

namespace SGSP.Formes
{
    public partial class ModifierMotPasse : Form
    {
        public ModifierMotPasse()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            var mGraphics = e.Graphics;
            var pen1 = new Pen(Color.White, 0);
            var area1 = new Rectangle(0, 0, Width - 1, Height - 1);
            var linearGradientBrush = new LinearGradientBrush(area1,
                Color.SteelBlue, Color.SteelBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            var mGraphics = e.Graphics;
            var pen1 = new Pen(Color.White, 1);
            var area1 = new Rectangle(0, 0, groupBox1.Width - 1, groupBox1.Height - 1);
            var linearGradientBrush = new LinearGradientBrush(area1,
                Color.SteelBlue, Color.SteelBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void btnAnnuler_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public int numeroUtilisateur;
        string numMatricule;
        private void ModifierMotPasse_Load(object sender, EventArgs e)
        {
            numMatricule = LoginFrm.matricule;
            numeroUtilisateur = LoginFrm.numUtilisateur;
            textBox1.Text = LoginFrm.nomUtilisateur;
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            var utilisateur = ConnectionClass.ListesDesUtilisateurs(numeroUtilisateur,textBox4.Text.GetHashCode().ToString());
            if (utilisateur.Count >0)
            {
                btnLogin.Enabled = true;
            }
            else
            {
                btnLogin.Enabled = false;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(textBox1.Text))
                {
                    if (textBox3.Text.Length >= 6)
                    {
                        if (textBox3.Text.Equals(textBox2.Text))
                        {
                            var utilisateur = new Utilisateur(numeroUtilisateur, textBox1.Text,
                                textBox3.Text.GetHashCode().ToString(), LoginFrm.typeUtilisateur);
                            if (ConnectionClass.ModifierUnUtilisateur(utilisateur ))
                            {
                                Close();
                            }
                        } else
                        {
                            MonMessageBox.ShowBox("Les mots de passe ne sont pas conformes", "Erreur");
                        }
                    } else
                    {
                        MonMessageBox.ShowBox("Le mot de passe doit comporter au moins six(6) caracteres", "Erreur");
                    }
                }
                else
                {
                    MonMessageBox.ShowBox("Veuillez saisir le nom d'utilisateur", "Erreur");
                }
            }
            catch (Exception ex )
            {
                MonMessageBox.ShowBox("Modifier utilisateur", ex);
            }
        }

  
    }
}
