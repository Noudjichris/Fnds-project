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
    public partial class LibelleDepenseFrm : Form
    {
        public LibelleDepenseFrm()
        {
            InitializeComponent();
        }
        public int etat, idLibelle;
        private void button1_Click(object sender, EventArgs e)
        {
            if(idCategorie==0)
            if(!string.IsNullOrEmpty(cmbCategorie.Text))
            {
            }
            else
            {
                GestionPharmacetique.MonMessageBox.ShowBox("Veuillez selectionner la catégorie de dépense sur la liste", "Erreur");
            }
            //if (!string.IsNullOrEmpty(textBox1.Text))
                //{
                    if (!string.IsNullOrEmpty(textBox2.Text))
                    {
                        var depense = new AppCode.Depenses();
                        depense.IDLibelle = idLibelle;
                        depense.Libelle = textBox2.Text;
                        depense.Etat = etat;
                        depense.Code = textBox1.Text;
                        if (!string.IsNullOrEmpty(cmbCategorie.Text))
                        {
                            var liste = from l in AppCode.ConnectionClass.ListeCategorie(etat)
                                        where l.Categorie.StartsWith(cmbCategorie.Text, StringComparison.CurrentCultureIgnoreCase)
                                        select l.IDCategorie;
                            foreach (var l in liste)
                                depense.IDCategorie = l;
                        }
                        else
                        {
                            depense.IDCategorie = idCategorie;
                        }
                        if(AppCode.ConnectionClass.EnregistrerUnLibelle(depense))
                        {
                            textBox1.Text = "";textBox2.Text = "";textBox1.Focus();idLibelle = 0;idCategorie = 0;
                            ListeLibelle();
                        }
                    }
                    else
                    {
                        GestionPharmacetique.MonMessageBox.ShowBox("Veuillez saisir le libellé de  dépense ", "Erreur");
                    }
                //}
                //else
                //{
                //    GestionPharmacetique.MonMessageBox.ShowBox("Veuillez selectionner le code du libellé de depense", "Erreur");
                //}
            
        }

        void ListeLibelle()
        {
            try
            {
                dataGridView1.Rows.Clear();
                foreach (var l in AppCode.ConnectionClass.ListeCategorie(etat))
                {

                    var liste = from le in AppCode.ConnectionClass.ListeLibelle(etat)
                                join li in AppCode.ConnectionClass.ListeCategorie(etat)
                                on le.IDCategorie equals li.IDCategorie
                                where le.IDCategorie ==l.IDCategorie
                                where le.Libelle.StartsWith(textBox2.Text, StringComparison.CurrentCultureIgnoreCase)
                                select le;
                    if (liste.Count() > 0)
                    {
                        dataGridView1.Rows.Add("","", l.Categorie, "", "");
                        foreach (var le in liste)
                        {
                            dataGridView1.Rows.Add(le.IDLibelle, l.IDCategorie,"", le.Code, le.Libelle);
                        }
                    }
                }
            }
            catch
            { }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            ListeLibelle();
        }

        private void LibelleDepenseFrm_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.RowTemplate.Height = 25;
                var liste = AppCode.ConnectionClass.ListeCategorie(etat);
                foreach (var l in liste)
                {
                    cmbCategorie.Items.Add(l.Categorie);
                }
                ListeLibelle();
            }
            catch { }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode== Keys.Enter)
            {
                textBox2.Focus();
            }
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.Focus();
            }
        }
        int idCategorie;

        private void cmbCategorie_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 5)
                {
                    if (!string.IsNullOrEmpty(dataGridView1.CurrentRow.Cells[0].Value.ToString()))
                        {
                        textBox1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                    
                        idCategorie = Convert.ToInt32(dataGridView1.CurrentRow.Cells[1].Value.ToString());
                        idLibelle = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                        var liste = from l in AppCode.ConnectionClass.ListeCategorie(etat)
                                    where l.IDCategorie ==idCategorie
                                    select l.Categorie;
                        foreach (var l in liste)
                          cmbCategorie.Text = l;
                        textBox2.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                    }
                }
                else if (e.ColumnIndex == 6)
                {
                    if (!string.IsNullOrEmpty(dataGridView1.CurrentRow.Cells[0].Value.ToString()))
                        {
                        idLibelle = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                        if (AppCode.ConnectionClass.SuprimerUnLibelle(idLibelle))
                        {
                            dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
                        }
                    }
                }
            }
            catch
            { }
        }
    }
}
