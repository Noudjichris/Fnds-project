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
    public partial class AvanceSurSalaireFrm : Form
    {
        public AvanceSurSalaireFrm()
        {
            InitializeComponent();
        }

        private void AccompteFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.Silver, 0);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                SystemColors.Control, SystemColors.Control, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        string nomEmploye, numeroMatricule; int id ;
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                double  accompte, rembourser, deduction;
                string  numMat;
                if (double.TryParse(txtAccompte.Text, out accompte) &&
                        double.TryParse(txtDeduction.Text, out deduction))
                    {
                        if (double.TryParse(textBox1.Text, out rembourser))
                        {
                        }
                        else
                        {
                            rembourser = 0;
                        }
                        if (dataGridView2.SelectedRows.Count > 0)
                        {
                            numMat = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
                            nomEmploye = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
                        }
                        else  //(numeroMatricule == null && dataGridView2.SelectedRows.Count <= 0)
                        {
                            GestionPharmacetique.MonMessageBox.ShowBox("Veuillez selectionner le nom du personnel sur la liste", "Erreur");
                            return;
                        }

                       

                        acompte.MontantAcompte = accompte;
                        acompte.Deduction = deduction;
                        acompte.Rembourser = rembourser;
                        acompte.NumeroMatricule = numMat;
                        acompte.DateAcompte = dateTimePicker1.Value;
                        acompte.NomEmploye = nomEmploye ;
                        acompte.NumeroAcompte = id;
                        if (rdbEspeces.Checked)
                        {
                            acompte.ModePaiement = "Paiement en espèces";
                        }
                        else if (rdbCheques.Checked)
                        {
                            acompte.ModePaiement = "Paiement par chèques";
                        }
                        else if (rdbBancaire.Checked)
                        {
                            acompte.ModePaiement = "Virement bancaire";
                        }
                        else
                        {
                            acompte.ModePaiement = "";
                        }
                        if (ConnectionClass.EnregistrerUnAccompte(acompte,etat))
                        {
                            btnClick = "1";
                            Dispose();
                        }
                    }
                    else
                    {
                        GestionPharmacetique.MonMessageBox.ShowBox("Veuillez entrer de chiffres valides pour les champs accompte et deduction","erreur");
                    }

            }
            catch(Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("Ajout accompte",ex);
            }
        }

       private void AccompteFrm_Load(object sender, EventArgs e)
        {
            try
            {
                rdbEspeces.Checked = true;
                Location = new Point((MainForm.width - Width) / 2, 5);
                btnClick = "2";
                if (!etat)
                {

                    if (acompte.ModePaiement == "Paiement en espèces")
                    {
                        rdbEspeces.Checked  = true;
                    }
                    else if (acompte.ModePaiement == "Paiement par chèques")
                    {
                        rdbCheques.Checked=true ;
                    }
                    else if (acompte.ModePaiement == "Virement bancaire")
                    {
                        rdbBancaire.Checked = true;
                    }
                    txtAccompte.Text = acompte.MontantAcompte.ToString();
                    txtDeduction.Text = acompte.Deduction.ToString();
                    textBox1.Text = acompte.Rembourser.ToString();
                    dateTimePicker1.Value = acompte.DateAcompte;
                    numeroMatricule = acompte.NumeroMatricule;
                    id = acompte.NumeroAcompte;
                    dataGridView2.Rows.Clear();
                    var dtPersonnel = ConnectionClass.ListePersonnelParMatricule(acompte.NumeroMatricule);
                    foreach (var dtRow in dtPersonnel)
                    {
                        dataGridView2.Rows.Add(
                            dtRow.NumeroMatricule,
                            dtRow.Nom+ " " +dtRow.Prenom);
                    }
                }
            }
            catch { }
       }
       private void btnFermer_Click(object sender, EventArgs e)
       {
           btnClick = "2";
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

       private void txtRechercherEmploye_TextChanged(object sender, EventArgs e)
       {
           ListeEmploye(txtRechercherEmploye.Text);
       }
       void ListeEmploye(string nom)
       {
           try
           {

               dataGridView2.Rows.Clear();
            
               foreach (var  p in ConnectionClass.ListePersonnel(txtRechercherEmploye.Text))
               {
                    dataGridView2.Rows.Add(
                     p.NumeroMatricule,
                     p.Nom + " " + p.Prenom);
                }

           }
           catch { }
       }
       public static bool etat;
       public static AvanceSurSalaireFrm frm;
       public static string btnClick;
       public static Acompte acompte = new Acompte();
       public static string ShowBox()
       {
           frm = new AvanceSurSalaireFrm();
           frm.ShowDialog();
           return btnClick;
       }

       private void button1_Click(object sender, EventArgs e)
       {
           txtRechercherEmploye_TextChanged(null, null);
       }

       private void groupBox6_Paint(object sender, PaintEventArgs e)
       {
           Graphics mGraphics = e.Graphics;
           Pen pen1 = new Pen(Color.White, 0);
           Rectangle area1 = new Rectangle(0, 0, this.groupBox6.Width - 1, this.groupBox6.Height - 1);
           LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
               SystemColors.Control, SystemColors.Control, LinearGradientMode.Vertical);
           mGraphics.FillRectangle(linearGradientBrush, area1);
           mGraphics.DrawRectangle(pen1, area1);
       }
    }
}
