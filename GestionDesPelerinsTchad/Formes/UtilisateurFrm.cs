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
    public partial class UtilisateurFrm : Form
    {
        public UtilisateurFrm()
        {
            InitializeComponent();
        }
        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            var mGraphics = e.Graphics;
            var pen1 = new Pen(Color.White, 2);
            var area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            var linearGradientBrush = new LinearGradientBrush(area1,
                Color.SteelBlue,Color.DodgerBlue, LinearGradientMode.Horizontal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void UtilisateurFrm_Paint(object sender, PaintEventArgs e)
        {
            var mGraphics = e.Graphics;
            var pen1 = new Pen(Color.White, 2);
            var area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            var linearGradientBrush = new LinearGradientBrush(area1,
                SystemColors.Control, SystemColors.Control, LinearGradientMode.Horizontal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void UtilisateurFrm_Load(object sender, EventArgs e)
        {
            try
            {
                var liste = AppCode.ConnectionClass.ListeUtilisteur();
                for (var i = 0; i < liste.Rows.Count; i++)
                {
                    dataGridView1.Rows.Add(
                        liste.Rows[i].ItemArray[0].ToString(),
                        liste.Rows[i].ItemArray[6].ToString(),
                        liste.Rows[i].ItemArray[1].ToString() + " " + liste.Rows[i].ItemArray[2].ToString(),
                        liste.Rows[i].ItemArray[3].ToString(),
                        liste.Rows[i].ItemArray[4].ToString(),
                        liste.Rows[i].ItemArray[5].ToString());
                }

                rdbUtilisateur.Checked = true;
            }
            catch { }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
             try
            {
                if (ListePerso.ShowBox() == "1")
                {
                    var numMatricule = ListePerso.numerMatricule;
                    var nomUtilisateur = ListePerso.nomPersonnel.Substring(0,ListePerso.nomPersonnel.LastIndexOf(" "));
                    var utilisateur = new AppCode.Utilisateur(nomUtilisateur, numMatricule.GetHashCode().ToString(), "", numMatricule);
                    if (AppCode.ConnectionClass.AjouterUnUtilisateur(utilisateur))
                    {
                        dataGridView1.Rows.Clear();
                        var liste = AppCode.ConnectionClass.ListeUtilisteur();
                        for (var i = 0; i < liste.Rows.Count; i++)
                        {
                            dataGridView1.Rows.Add(
                                liste.Rows[i].ItemArray[0].ToString(),
                                liste.Rows[i].ItemArray[6].ToString(),
                                liste.Rows[i].ItemArray[1].ToString() + " " + liste.Rows[i].ItemArray[2].ToString(),
                                liste.Rows[i].ItemArray[3].ToString(),
                                liste.Rows[i].ItemArray[4].ToString(),
                                liste.Rows[i].ItemArray[5].ToString());
                        }
                    }

                }
            }
            catch
            { }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count>0)
                {
                    var id= Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    if (GestionPharmacetique.MonMessageBox.ShowBox("Voulez vous supprimer ces données?", "confirmation") == "1")
                    {
                        AppCode.ConnectionClass.SupprimerUnUtilisateur(id);
                        {
                            dataGridView1.Rows.Clear();
                            var liste = AppCode.ConnectionClass.ListeUtilisteur();
                            for (var i = 0; i < liste.Rows.Count; i++)
                            {
                                dataGridView1.Rows.Add(
                                    liste.Rows[i].ItemArray[0].ToString(),
                                    liste.Rows[i].ItemArray[6].ToString(),
                                    liste.Rows[i].ItemArray[1].ToString() + " " + liste.Rows[i].ItemArray[2].ToString(),
                                    liste.Rows[i].ItemArray[3].ToString(),
                                    liste.Rows[i].ItemArray[4].ToString(),
                                    liste.Rows[i].ItemArray[5].ToString()
                                    );
                            }
                        }
                    }
                }
            }
            catch
            { }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                var typeUtilisateur="";
                if (rdbUtilisateur.Checked)
                {
                    typeUtilisateur = "";
                }else if(rdbDRH.Checked)
                {
                    typeUtilisateur = "drh";
                }
                else if(rdbComp.Checked)
                {
                    typeUtilisateur = "compta";
                }else if(rdbAdminstateur.Checked)
                {
                    typeUtilisateur = "admin";
                }
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    var id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    if (AppCode.ConnectionClass.AccorderPrivileges(id, typeUtilisateur))
                    {
                        rdbUtilisateur.Checked = true;
                        dataGridView1.Rows.Clear();
                        var liste = AppCode.ConnectionClass.ListeUtilisteur();
                        for (var i = 0; i < liste.Rows.Count; i++)
                        {
                            dataGridView1.Rows.Add(
                                liste.Rows[i].ItemArray[0].ToString(),
                                liste.Rows[i].ItemArray[6].ToString(),
                                liste.Rows[i].ItemArray[1].ToString() + " " + liste.Rows[i].ItemArray[2].ToString(),
                                liste.Rows[i].ItemArray[3].ToString(),
                                liste.Rows[i].ItemArray[4].ToString(),
                                liste.Rows[i].ItemArray[5].ToString()
                                );
                        }
                    }
                }
            }
            catch { }
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Dispose();
        }


     
    }
}
