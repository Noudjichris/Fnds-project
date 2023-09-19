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
    public partial class RubriqueFrm : Form
    {
        public RubriqueFrm()
        {
            InitializeComponent();
        }

        private void RubriqueFrm_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.RowTemplate.Height = 30;
                ListeRubriques();
            }
            catch (Exception ex) { GestionPharmacetique.MonMessageBox.ShowBox("", ex); }
        }
        void ListeRubriques()
        {
            try
            {
                dataGridView1.Rows.Clear();
                lblReference.Text = "Détails de " + rubrique;
                var liste = from r in AppCode.ConnectionClass.ListeDetailsRubriquesFacures()
                            join f in AppCode.ConnectionClass.ListeDetailsFacures(numeroFacture, type)
                            on r.NumeroFacture equals f.NumeroFacture
                            where r.NumeroFacture == id
                            select new
                            {
                                r.NumeroFacture,
                                r.NumeroRubrique,
                                r.Designation
                            };
                //var g= liste.Count;
                foreach (var r in liste)
                {
                    dataGridView1.Rows.Add(r.NumeroRubrique, r.NumeroFacture, r.Designation);
                    Column6.Image = global::SGSP.Properties.Resources.DeleteRed1;
                }
            }
            catch { }
        }
        public static string type, rubrique;
        public static int numeroFacture, id;
        public static RubriqueFrm frm;
        public static DataGridView dgvRow;
        static bool state;

        public static bool ShowBox()
        {
            frm = new RubriqueFrm();
            frm.ShowDialog();
            return state;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            state = true;
            Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var facture = new AppCode.Document();
                facture.NumeroFacture = id;
                facture.Designation = "";
                if (AppCode.ConnectionClass.InsererDetailsSousRubriques(facture, false))
                {
                    dataGridView1.Rows.Add(AppCode.ConnectionClass.NumeroRubrique(), id, facture.Designation);
                    Column6.Image = global::SGSP.Properties.Resources.DeleteRed1;
                }
            }
            catch (Exception)
            {
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var facture = new AppCode.Document();
                var index = dataGridView1.CurrentRow.Index;
                facture.NumeroRubrique = Convert.ToInt32( dataGridView1.Rows[index].Cells[0].Value.ToString());
                facture.Designation = dataGridView1.Rows[index].Cells[2].Value.ToString();

                if (AppCode.ConnectionClass.InsererDetailsSousRubriques(facture, true ))
                {
                    ListeRubriques();
                }
            }
            catch { }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                var id = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString());
                if (AppCode.ConnectionClass.SupprimerDetailsSousRubriques(id))
                {
                    ListeRubriques();
                }
            }
        }

        private void RubriqueFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            dgvRow = dataGridView1;
            state = true;
        }

    }
}
