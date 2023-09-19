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
    public partial class DivisionFrm : Form
    {
        public DivisionFrm()
        {
            InitializeComponent();
        }

        private void DivisionFrm_Load(object sender, EventArgs e)
        {
            try
            {
                foreach (var dir in AppCode.ConnectionClass.ListeDirection())
                    cmbDirection.Items.Add(dir.Direction);
                ListeDivision();
            }
            catch { }
        }

        void ListeDivision()
        {
            dataGridView2.Rows.Clear();
            foreach (var dir in AppCode.ConnectionClass.ListeDirection())
            {
                var divisions = from d in AppCode.ConnectionClass.ListeDivision()
                                where d.IDDirection == dir.IDDirection
                                select d;
                if (divisions.Count() > 0)
                {
                    dataGridView2.Rows.Add("","", dir.Direction.ToUpper(), "","");
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial ", 11.75F, System.Drawing.FontStyle.Bold);

                    foreach (var d in divisions)
                    {
                        dataGridView2.Rows.Add(d.IDDivision,d.Ordre, d.Division, d.Abreviation,dir.Direction);
                    }
                }
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                //ListeDivision();
                if (!string.IsNullOrEmpty(cmbDirection.Text) && !string.IsNullOrEmpty(txtAbreviation.Text) && !string.IsNullOrEmpty(txtDivision.Text))
                {
                    var division = new AppCode.Service();
                    division.IDDivision = idDivision;
                    division.Division = txtDivision.Text;
                    division.Abreviation = txtAbreviation.Text;
                    var liste = from d in AppCode.ConnectionClass.ListeDirection()
                                where d.Direction == cmbDirection.Text
                                select d;
                    foreach (var d in liste)
                        division.IDDirection = d.IDDirection;
                    if (AppCode.ConnectionClass.EnregistrerDivision(division))
                    {
                        idDivision = 0;
                        txtAbreviation.Text = "";
                        txtDivision.Text = "";
                        ListeDivision();
                    }
                }
            }
            catch { }
        }
        int idDivision;
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                idDivision = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString());
                txtDivision.Text = dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtAbreviation.Text = dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString();
                cmbDirection.Text = dataGridView2.Rows[e.RowIndex].Cells[4].Value.ToString();
            }
            else if (e.ColumnIndex == 6)
            {
                if(GestionPharmacetique.MonMessageBox.ShowBox("Voulez vos supprimer ces données?","Confirmation")=="1")
                {
                    AppCode.ConnectionClass.SupprimerUneDivision(Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString()));
                    ListeDivision();
                }
            }
        }

        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var d = new AppCode.Service();
                d.Ordre = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString());
                d.IDDivision = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString());
                AppCode.ConnectionClass.OrdonnerDivision(d);
                ListeDivision();
            }
            catch { }
        }

        private void txtDivision_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
