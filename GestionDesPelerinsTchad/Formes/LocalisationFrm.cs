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
    public partial class LocalisationFrm : Form
    {
        public LocalisationFrm()
        {
            InitializeComponent();
        }
        int ID;
        private void txtDivision_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (!string.IsNullOrEmpty(txtLocalisation.Text))
                    {
                        var service = new AppCode.Service();
                        service.Localite = txtLocalisation.Text;
                        service.IDDivision = ID;
                        if (AppCode.ConnectionClass.EnregistrerUneLocalisation(service))
                        {
                            ID = 0;
                            txtLocalisation.Text = "";
                            ListeLocalisation();
                        }
                    }
                }
            }
            catch { }
        }

        void ListeLocalisation()
        {
            dataGridView2.Rows.Clear();
            foreach (var s in AppCode.ConnectionClass.ListeLocalisation())
                dataGridView2.Rows.Add(s.IDDivision, s.Localite);
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {ID =Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString());
                txtLocalisation.Text =dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
            }
            else if (e.ColumnIndex == 3)
            {
                AppCode.ConnectionClass.SupprimerUneLocalisation(Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString()));
               dataGridView2.Rows.Remove(dataGridView2.Rows[e.RowIndex]);
            }
        }

        private void LocalisationFrm_Load(object sender, EventArgs e)
        {
            ListeLocalisation();
        }
    }
}
