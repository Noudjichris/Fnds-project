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
    public partial class TypeDocumentFrm : Form
    {
        public TypeDocumentFrm()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int numero;string etat;
                var rowIndex = dataGridView1.CurrentRow.Index;
                var typeDocument = new AppCode.Document();
                typeDocument.TypeDocument = dataGridView1.Rows[rowIndex].Cells[1].Value.ToString();
                if (Int32.TryParse(dataGridView1.Rows[rowIndex].Cells[0].Value.ToString(), out numero))
                {
                    etat= "2";
                    typeDocument.NumeroType = numero;
                }
                else
                {
                    etat="1";
                    typeDocument.NumeroType = 0;
                }
                if (AppCode.ConnectionClass.EnregistrerUnTypeDocument(typeDocument, etat))
                {
                    dataGridView1.Rows.Clear();
                    var liste = AppCode.ConnectionClass.ListeDesTypesDocuments();
                    foreach (var l in liste)
                    {
                        dataGridView1.Rows.Add(l.NumeroType, l.TypeDocument);
                        Column6.Image = global::SGSP.Properties.Resources.DeleteRed1;
                    }

                }
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("Edit type de document", ex);
            }
        }

        private void TypeDocumentFrm_Load(object sender, EventArgs e)
        {
            try
            {
                btnExit.Location = new Point(Width - 52, 3);
                dataGridView1.RowTemplate.Height = 30;
                var liste = AppCode.ConnectionClass.ListeDesTypesDocuments();
                foreach (var l in liste)
                {
                    dataGridView1.Rows.Add(l.NumeroType, l.TypeDocument);
                    Column6.Image = global::SGSP.Properties.Resources.DeleteRed1;
                }
            }
            catch { }
        }

        public static TypeDocumentFrm frm;
        public static string typeDocument;
        static  bool state=false;
        public static int numeroDocument, index;
        public static bool ShowBox()
        {
            frm = new TypeDocumentFrm();
            frm.ShowDialog();
            return state;
        }

        public static bool ShowBox1()
        {
            frm = new TypeDocumentFrm();
            frm.button1.Visible = false;
            frm.Column6.Visible = false;
            frm.Width = frm.Width - 45;
            frm.ShowDialog();
            return state;
        }

        private void TypeDocumentFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (index != 1)
            {
                state = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Add("");
            Column6.Image = global::SGSP.Properties.Resources.DeleteRed1;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 2)
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
                            AppCode.ConnectionClass.SupprimerUnTypeDocument(numero);
                            dataGridView1.Rows.Remove(dataGridView1.Rows[rowIndex]);
                        }
                    }
                }
            }
            catch { }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (index == 1)
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    numeroDocument = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    typeDocument = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                    state = true;
                    frm.Close();
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            state = false;
            frm.Dispose();
        }

    }
}
