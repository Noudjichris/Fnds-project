using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SGSP.Formes
{
    public partial class UniveristeFrm : Form
    {
        public UniveristeFrm()
        {
            InitializeComponent();
        }

       
        private void button2_Click(object sender, EventArgs e)
        {
            var etablissement = new AppCode.Etablissement();
            etablissement.NumerEtablissment = numero;
            etablissement.NomEtablissment = txtEtablissement.Text;
            etablissement.Email = txtEmail.Text;
            etablissement.Pays = txtPays.Text;
            etablissement.Telephone1 = txtTele1.Text;
            etablissement.Telephone2 = txtTele2.Text;
            etablissement.Ville = txtVille.Text;
            etablissement.SiteWeb = txtSiteWeb.Text;
            if (AppCode.ConnectionClass.EnregistrerEtablissement(etablissement))
            {
                numero = 0;
                txtEmail.Text = "";
                txtEmail.Text = "";
                txtPays.Text = "";
                txtSiteWeb.Text = "";
                txtTele1.Text = "";
                txtTele2.Text = "";
                txtVille.Text = "";
                ListeEtablissement();
            }
        }

        void ListeEtablissement()
        {
            dataGridView1.Rows.Clear();
            foreach (var e in AppCode.ConnectionClass.ListeDesEtablissements())
            {
                dataGridView1.Rows.Add(e.NumerEtablissment,
                    e.NomEtablissment,
                    e.Ville,e.Pays,e.Telephone2,e.Telephone2,e.Email,e.SiteWeb);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Dispose();
        }
        int numero;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 8)
            {
                numero = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                txtEtablissement.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtVille.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtPays.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                txtTele2.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                txtTele1.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtEmail.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                txtSiteWeb.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            }
            else if (e.ColumnIndex == 9)
            {
                if(GestionPharmacetique.MonMessageBox.ShowBox("Voulez vous supprimer ces données?","Confirmation")=="1")
                {
                AppCode.ConnectionClass.SupprimerUnEtablissement(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()));
                dataGridView1.Rows.Remove(dataGridView1.Rows[e.RowIndex]);
                }
            }
        }

        private void UniveristeFrm_Load(object sender, EventArgs e)
        {
            ListeEtablissement();
        }
    }
}
