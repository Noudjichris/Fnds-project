using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SGSP.AppCode;
using GestionPharmacetique;

namespace SGSP.Formes
{
    public partial class ServiceFrm : Form
    {
        public ServiceFrm()
        {
            InitializeComponent();
        }

        private void ServiceFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.Silver, 0);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                SystemColors.Control, Color.WhiteSmoke, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.DodgerBlue, 0);
            var area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            var  linearGradientBrush = new LinearGradientBrush(area1,
                Color.DodgerBlue, Color.DodgerBlue
                , LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void groupBox5_Paint(object sender, PaintEventArgs e)
        {
            var mGraphics = e.Graphics;
            var pen1 = new Pen(Color.White, 1);
            var area1 = new Rectangle(0, 0, groupBox5.Width - 1, groupBox5.Height - 1);
            var linearGradientBrush = new LinearGradientBrush(area1,
                  SystemColors.Control, SystemColors.Control, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        void ListeService()
        {
            try
            {
                dataGridView2.Rows.Clear();
                foreach (var dir in ConnectionClass.ListeDirection())
                {
                    dataGridView2.Rows.Add("", "", dir.Direction , "", "");
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightGray;
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial ", 11.75F, System.Drawing.FontStyle.Bold);

                    var listeDivision = from d in ConnectionClass.ListeDivision()
                                       where d.IDDirection == dir.IDDirection
                                       select d;
                    foreach (var div in listeDivision)
                    {
                        dataGridView2.Rows.Add("", "", div.Division , "", "");
                        dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.BackColor = Color.WhiteSmoke;
                        dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial ", 11.75F, System.Drawing.FontStyle.Bold);

                        var listeService = from d in ConnectionClass.ListeDepartement()
                                           where d.IDDivision == div.IDDivision
                                           select d;
                        if (listeService.Count() > 0)
                        {
                            foreach (var d in listeService)
                            {
                                dataGridView2.Rows.Add(d.IDDepartement, d.Ordre, d.Departement, d.Abreviation, div.Division);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("", ex);
            }
        }

        private void ServiceFrm_Load(object sender, EventArgs e)
        {
            try
            {
                foreach (var d in ConnectionClass.ListeDivision())
                {
                    cmbDivision.Items.Add(d.Division.ToUpper());
                }
                dataGridView2.RowTemplate.Height = 30;
                if (GestionAcademique.LoginFrm.typeUtilisateur == "")
                {
                    txtDivision.Enabled = false;
                    txtAbreviation.Enabled = false;
                }
                button5.Location = new Point(Width - 40, button5.Location.Y);
                txtAbreviation.Location = new Point(groupBox5.Width-txtAbreviation.Width - 10, txtAbreviation.Location.Y);
                button13.Location = new Point(groupBox5.Width-button13.Width - 10, button13.Location.Y);
                dataGridViewTextBoxColumn1.Width = dataGridView2.Width * 3 / 4;
                ListeService();
            }
            catch (Exception)
            {
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 6)
                {

                    if (MonMessageBox.ShowBox("Voulez vous supprimer ces données", "Confirmation") == "1")
                    {
                        id = Int32.Parse(dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString());
                        ConnectionClass.SupprimerUnDepartement(id);
                        id = 0;
                        ListeService();
                    }
                }
                else if (e.ColumnIndex == 5)
                {
                    id = Int32.Parse(dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString());
                    txtDivision.Text = dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();
                    txtAbreviation.Text = dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString();
                    cmbDivision.Text= dataGridView2.Rows[e.RowIndex].Cells[4].Value.ToString();
                }
            }
            catch (Exception ex)
            { }
        }
        int id;
        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtDivision.Text) && !string.IsNullOrEmpty(cmbDivision.Text))
                {
                    var service = new Service();
                    service.IDDepartement = id;
                    service.Departement = txtDivision.Text;
                    service.Abreviation = txtAbreviation.Text;
                    var liste = from d in ConnectionClass.ListeDivision()
                                where d.Division.StartsWith(cmbDivision.Text, StringComparison.CurrentCultureIgnoreCase)
                                select d;
                    foreach (var d in liste)
                        service.IDDivision = d.IDDivision;
                    if (MonMessageBox.ShowBox("Voulez vous enregistrer ces données", "Confirmation") == "1")
                    {
                        if (ConnectionClass.EnregistrerDepartement(service))
                        {
                            ListeService();
                            txtDivision.Text = "";
                            txtAbreviation.Text = "";
                            id = 0;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("modification division", ex);
            }
        }

        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var d = new AppCode.Service();
                d.Ordre = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString());
                d.IDDepartement= Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[0].Value.ToString());
                AppCode.ConnectionClass.OrdonnerService(d);
               ListeService();
            }
            catch { }
        }

    }
}
