using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SGSP.AppCode;

namespace SGSP.Formes
{
    public partial class DirectionFrm : Form
    {
        public DirectionFrm()
        {
            InitializeComponent();
        }

        void ListeDirection()
        {
            try
            {
                dataGridView2.Rows.Clear();
                foreach (var d in ConnectionClass.ListeDirection())
                {
                    dataGridView2.Rows.Add(d.IDDirection,d.Ordre, d.Direction, d.Abreviation);
                }
            }
            catch (Exception ex)
            {
              
            }
        }
        int id;
        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtDivision.Text))
                {
                    var service = new Service();
                    service.IDDirection = id;
                    service.Direction = txtDivision.Text;
                    service.Abreviation = txtAbreviation.Text;
                    if (GestionPharmacetique. MonMessageBox.ShowBox("Voulez vous enregistrer ces données", "Confirmation") == "1")
                    {
                        if (ConnectionClass.EnregistrerDirection(service))
                        {
                            ListeDirection();
                            txtDivision.Text = "";
                            txtAbreviation.Text = "";
                            id = 0;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                //MonMessageBox.ShowBox("modification division", ex);
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 5)
                {
                    if (GestionPharmacetique.MonMessageBox.ShowBox("Voulez vous supprimer ces dounnées?", "Confirmation") == "1")
                    {
                        id = Int32.Parse(dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString());
                        ConnectionClass.SupprimerUneDirection(id);
                        id = 0;
                        ListeDirection();
                    }
                }
                else if (e.ColumnIndex == 4)
                {
                    txtDivision.Text = dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();
                    txtAbreviation.Text = dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString();
                    id = Int32.Parse(dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString());
                }
            }
            catch (Exception ex)
            { }
        }

        private void DirectionFrm_Load(object sender, EventArgs e)
        {
            ListeDirection();
        }

        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var d = new AppCode.Service();
                d.Ordre = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString());
                d.IDDirection = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString());
                AppCode.ConnectionClass.OrdonnerDirection(d);
                ListeDirection();
            }
            catch { }
        }
    }
}
