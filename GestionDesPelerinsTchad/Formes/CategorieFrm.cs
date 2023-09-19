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
    public partial class CategorieFrm : Form
    {
        public CategorieFrm()
        {
            InitializeComponent();
        }
        
        public static CategorieFrm frm;
        static bool state;

        public static bool ShowBox()
        {
            frm = new CategorieFrm();
            frm.ShowDialog();
            return state;
        }

        private void TypeDocumentFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            state = true;
        }
        private void CategorieFrm_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.RowTemplate.Height = 30;
                var liste = AppCode.ConnectionClass.ListeCategorie(etat);
                foreach (var l in liste)
                {
                    dataGridView1.Rows.Add(l.IDCategorie,l.Code, l.Categorie);
                }
            }
            catch { }
        }
        public static int etat;

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 4)
                {

                    if (GestionPharmacetique.MonMessageBox.ShowBox("Voulez vous vous supprimer cette ligne?", "Confirmation") == "1")
                    {
                        idCategorie = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                        if (AppCode.ConnectionClass.SuprimerUneCategorie(idCategorie))
                        {
                            dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);
                        }
                    }
                }
                else if(e.ColumnIndex==3)
                {
                    textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                    textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                    idCategorie = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                    textBox1.Focus();
                }
            }
            catch { }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
            var liste = from le in AppCode.ConnectionClass.ListeCategorie(etat)
                        where le.Categorie.StartsWith(textBox2.Text, StringComparison.CurrentCultureIgnoreCase)
                        select le;
            foreach (var l in liste)
            {
                dataGridView1.Rows.Add(l.IDCategorie,l.Code, l.Categorie);
            }
            }
            catch
            { }
        }
        int idCategorie;
        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode== Keys.Enter)
            { 
            var depense = new AppCode.Depenses();
            depense.IDCategorie = idCategorie;
            depense.Categorie = textBox2.Text;
                depense.Code = textBox1.Text;
            depense.Etat = etat;
                if ( !string.IsNullOrEmpty(textBox2.Text))
                {
                    if (AppCode.ConnectionClass.EnregistrerUnUneCategorie(depense))
                    {
                        textBox1.Focus();
                        textBox1.Text = "";
                        textBox1.Text = "";
                        textBox2.Text = ""; idCategorie = 0;
                        dataGridView1.Rows.Clear();
                        var liste = AppCode.ConnectionClass.ListeCategorie(etat);
                        foreach (var l in liste)
                        {
                            dataGridView1.Rows.Add(l.IDCategorie, l.Code, l.Categorie);
                        }
                    }
                }
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode== Keys.Enter)
            {
                textBox2.Focus();
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                AppCode.ConnectionClass.OrdonneeCategorie(dataGridView1);
            }
            catch { }
        }
    }
}
