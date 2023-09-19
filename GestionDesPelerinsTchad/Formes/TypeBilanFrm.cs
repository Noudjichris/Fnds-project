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
    public partial class TypeBilanFrm : Form
    {
        public TypeBilanFrm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add( "");
            Column6.Image = global::SGSP.Properties.Resources.DeleteRed1;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 3)
                {
                    int numero;
                    var rowIndex = dataGridView1.CurrentRow.Index;

                    if (Int32.TryParse(dataGridView1.Rows[rowIndex].Cells[0].Value.ToString(), out numero))
                    {
                    }
                    if (numero > 0)
                    {
                        if (GestionPharmacetique.MonMessageBox.ShowBox("Voulez vous vous supprimer ce type de document?", "Confirmation") == "1")
                        {
                            AppCode.ConnectionClass.SupprimerUnBilanDetailleType(numero);
                            dataGridView1.Rows.Remove(dataGridView1.Rows[rowIndex]);
                        }
                    }
                }
            }
            catch { }
        }
        string typeBilan;
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int numero; string etat;
                var rowIndex = dataGridView1.CurrentRow.Index;
                var bilan = new AppCode.Bilan();
                bilan.TypeDetail = dataGridView1.Rows[rowIndex].Cells[2].Value.ToString();
                bilan.Code = dataGridView1.Rows[rowIndex].Cells[1].Value.ToString();
                if (Int32.TryParse(dataGridView1.Rows[rowIndex].Cells[0].Value.ToString(), out numero))
                {
                    etat = "2";
                    bilan.IDDetailBilan = numero;
                }
                else
                {
                    etat = "1";
                    bilan.IDDetailBilan = 0;
                }
                if (AppCode.ConnectionClass.EnregistrerUnTypeBilan(bilan, etat))
                {
                    dataGridView1.Rows.Clear();
                    var liste = AppCode.ConnectionClass.ListeDistinctDesDetailsBilansType();
                    foreach (var l in liste)
                    {
                        dataGridView1.Rows.Add(l.IDDetailBilan,l.Code, l.TypeDetail);
                        Column6.Image = global::SGSP.Properties.Resources.DeleteRed1;
                    }

                }
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("Edit type ", ex);
            }
        }

        private void TypeBilanFrm_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                var liste = AppCode.ConnectionClass.ListeDistinctDesDetailsBilansType();
                foreach (var l in liste)
                {
                    dataGridView1.Rows.Add(l.IDDetailBilan,l.Code, l.TypeDetail);
                    Column6.Image = global::SGSP.Properties.Resources.DeleteRed1;
                }
            }
            catch { }
        }
    }
}
