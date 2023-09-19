using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace GestionPharmacetique
{
    public partial class MonMessageBox : Form
    {
        public MonMessageBox()
        {
            InitializeComponent();
        }

        static string boutonID;
        static MonMessageBox newMsgBox;
        
        
        private void MonMessageBox_Load(object sender, EventArgs e)
        {
        }


        public static string ShowBox(string txtMessage, string title)
        {

            try
            {
                newMsgBox = new MonMessageBox();
                newMsgBox.lblMessage.Text = txtMessage;
                newMsgBox.label2.Text = title;
                Image imagePath = null;
                if (title.ToLower().Contains("erreur"))
                {
                    imagePath = global::SGSP.Properties.Resources.erreur;
                    newMsgBox.button2.Visible = false;

                }
                else if (title.ToLower().Contains("affirma") || title.ToLower().Contains("info"))
                {
                    imagePath = global::SGSP.Properties.Resources.affirmation;
                    newMsgBox.button2.Visible = false;
                }
                else if (title.ToLower().Contains("confirma"))
                {
                    imagePath = global::SGSP.Properties.Resources.confirmation;
                }

                newMsgBox.pictureBox1.Image = imagePath;
                newMsgBox.ShowDialog();
            }
            catch (Exception)
            {
            }
            return boutonID;
        }

        public static string ShowBox(string txtMessage, string title, Exception exception)
        {
            try
            {
                newMsgBox = new MonMessageBox();
                newMsgBox.lblMessage.Text = txtMessage;
                newMsgBox.label2.Text = title;
                Image imagePath = null;
                if (title.ToLower().Contains("erreur"))
                {
                    imagePath = global::SGSP.Properties.Resources.erreur;
                    newMsgBox.button2.Visible = false;

                }
                else if (title.ToLower().Contains("affirma"))
                {
                    imagePath = global::SGSP.Properties.Resources.affirmation;
                    newMsgBox.button2.Visible = false;
                }
                else if (title.ToLower().Contains("confirma"))
                {
                    imagePath = global::SGSP.Properties.Resources.confirmation;
                }

                newMsgBox.pictureBox1.Image = imagePath;
                newMsgBox.ShowDialog();

                string filePath = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10) + "\\log\\log_erreur.txt";
                    //global::GestionPharmacetique.Properties.Resources.log_erreur;
                if (File.Exists(filePath))
                {
                    StreamWriter sw = new StreamWriter(filePath, true);
                    sw.WriteLine("*****************Erreur apparue le " + DateTime.Now.ToString() + "**********************");
                    sw.WriteLine(title);
                    sw.WriteLine(exception.GetType().Name);
                    sw.WriteLine(exception.Message);
                    sw.WriteLine(exception.ToString());
                    sw.WriteLine("\n");
                    sw.Close();

                }
              
                newMsgBox.ShowDialog();
            }
            catch (Exception)
            {
            }
            return boutonID;

        }

        public static string ShowBox(string txtTitle, Exception exception)
        {
            newMsgBox = new MonMessageBox();

            // inserer les erreurs dans un fichier text

            string filePath = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10) + "\\log\\log_erreur.txt";
            if (File.Exists(filePath))
            {
                StreamWriter sw = new StreamWriter(filePath, true);
                sw.WriteLine("*****************Erreur apparue le " + DateTime.Now.ToString() + "**********************");
                sw.WriteLine(txtTitle);
                sw.WriteLine(exception.GetType().Name);
                sw.WriteLine(exception.Message);
                sw.WriteLine(exception.ToString());
                sw.WriteLine();
                sw.Close();

            }
      
            return boutonID;
        }


        private void button1_Click_1(object sender, EventArgs e)
        {
            boutonID = "1";
            newMsgBox.Dispose();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            boutonID = "2";
            newMsgBox.Dispose();
        }


        private void MonMessageBox_Paint(object sender, PaintEventArgs e)
        {
            var mGraphics = e.Graphics;
            var pen1 = new Pen(Color.SteelBlue, 2);
            var area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            var linearGradientBrush = new LinearGradientBrush(area1,
                SystemColors.Control, SystemColors.Control, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

    }
}
