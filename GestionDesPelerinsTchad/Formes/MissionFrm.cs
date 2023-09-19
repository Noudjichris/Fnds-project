using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GestionPharmacetique;
using SGSP.AppCode;

namespace SGSP.Formes
{
    public partial class MissionFrm : Form
    {
        public MissionFrm()
        {
            InitializeComponent();
        }

        private void groupBox5_Paint(object sender, PaintEventArgs e)
        {
            var mGraphics = e.Graphics;
            var pen1 = new Pen(Color.DodgerBlue, 2);
            var area1 = new Rectangle(0, 0, this.groupBox5.Width - 1, this.groupBox5.Height - 1);
            var linearGradientBrush = new LinearGradientBrush(area1,
                Color.DodgerBlue, Color.SteelBlue, LinearGradientMode.Horizontal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            var mGraphics = e.Graphics;
            var pen1 = new Pen(SystemColors.ControlLight, 2);
            var area1 = new Rectangle(0, 0, this.groupBox2.Width - 1, this.groupBox2.Height - 1);
            var linearGradientBrush = new LinearGradientBrush(area1,
                SystemColors.ControlLight, SystemColors.ControlLight, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            var mGraphics = e.Graphics;
            var pen1 = new Pen(SystemColors.ControlLight, 2);
            var area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            var linearGradientBrush = new LinearGradientBrush(area1,
                SystemColors.ControlLight, SystemColors.ControlLight, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void MissionFrm_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.RowTemplate.Height = 30;
                clObjet.Width = dataGridView1.Width / 4-40;
                clDestination.Width = dataGridView1.Width / 4-40;
                for (var i = 2017; i < DateTime.Now.Year + 10; i++)
                {
                    cmbExercice.Items.Add(i.ToString());
                }
                cmbExercice.Text = DateTime.Now.Year.ToString();
                btnFermer.Location = new Point(Width - 40, 4);
                ListeDesMissions();
            }
            catch { }
        }
        int idMission;
        void ListeDesMissions()
        {
            dataGridView1.Rows.Clear();
            foreach (var m in ConnectionClass.ListeMisson())
            {
                dataGridView1.Rows.Add(m.IDMission, m.Objet, m.Destination, m.DateDepart.ToShortDateString(),
                    m.DateRetour.ToShortDateString(), m.Transport, m.Imputation, m.Exercice, m.SiPayant, m.Etat,"Personnels");
            }
        }
        private void btnAjouter_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtObjet.Text))
                {
                    if (!string.IsNullOrEmpty(cmbTransport.Text))
                    {
                        if (!string.IsNullOrEmpty(txtDestination.Text))
                        {
                            if (!string.IsNullOrEmpty(cmbEtat.Text))
                            {
                                int exercice;
                                if (Int32.TryParse(cmbExercice.Text, out exercice))
                                {
                                    var mission = new Mission();
                                    mission.IDMission = idMission;
                                    mission.Etat = cmbEtat.Text;
                                    mission.DateDepart = dtpDepart.Value.Date;
                                    mission.DateRetour = dtpRetour.Value.Date;
                                    mission.Destination = txtDestination.Text;
                                    mission.Exercice = exercice;
                                    mission.Imputation = txtImputation.Text;
                                    mission.SiPayant = checkBox1.Checked;
                                    mission.Transport = cmbTransport.Text;
                                    mission.Objet = txtObjet.Text;
                                    if (ConnectionClass.EnregistrerUneMission(mission))
                                    {
                                        txtDestination.Text = "";
                                        txtImputation.Text = "";
                                        txtObjet.Text = "";
                                        dtpDepart.Value = DateTime.Now;
                                        dtpRetour.Value = DateTime.Now;
                                        cmbEtat.Text = "";
                                        checkBox1.Checked = true;
                                        cmbTransport.Text = "";
                                        idMission = 0;
                                        cmbExercice.Text = DateTime.Now.Year.ToString();
                                        ListeDesMissions();
                                    }
                                }
                                else
                                {
                                    MonMessageBox.ShowBox("Veuillez selectionner l'année sur la liste deroulante", "Erreur");
                                }
                            }
                            else
                            {
                                MonMessageBox.ShowBox("Veuillez selectionner l'etat de la mission sur la liste deroulante", "Erreur");
                            }
                        }
                        else
                        {
                            MonMessageBox.ShowBox("Veuillez entrer la destination", "Erreur");
                        }
                    }
                    else
                    {
                        MonMessageBox.ShowBox("Veuillez selectionner le moyen de transport sur la liste deroulante", "Erreur");
                    }
                }
                else
                {
                    MonMessageBox.ShowBox("Veuillez entrer l'objet de la mission", "Erreur");
                }
            }
            catch { }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 10)
            {
                var frm = new MissionPersonnelFrm();
                frm.idMission = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                frm.objetMission = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                if (Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString()) ==
                    Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString()))
                {
                    frm.duree = 1;
                }
                else
                {
                    frm.duree = Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString()).Subtract(
                    Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString())).Days;
                }
                frm.ShowDialog();
            }
        }

        private void dtpDepart_ValueChanged(object sender, EventArgs e)
        {

            if (dtpDepart.Value.Date == dtpRetour.Value.Date)
            {
                txtDuree.Text = "1";
            }
            else
            {
                txtDuree.Text = (1+dtpRetour.Value.Date.Subtract(dtpDepart.Value.Date).Days).ToString();
            }
        }
        Bitmap document;
        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {

                var mission = new AppCode.Mission();
                mission.IDMission = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                mission.Objet = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                mission.Destination = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                mission.DateDepart = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells[3].Value.ToString());
                mission.DateRetour = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells[4].Value.ToString());
                mission.Transport = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
                mission.Imputation = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
                mission.Exercice = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[7].Value.ToString());

                var liste = AppCode.ConnectionClass.ListeMisson(mission.IDMission);

                if (checkBox2.Checked)
                {
                    document = AppCode.Impression.ListeEmargementDeLaMission(mission, liste);
                }
                else
                {
                    document = AppCode.Impression.ImprimerOrdreMission(mission, liste);
                }
                if (printDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                    printPreviewDialog1.ShowDialog();
                }
            }
        }


        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(document, 0, 0, document.Width, document.Height);
            e.HasMorePages = false;
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtObjet.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            txtDestination.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            dtpDepart.Value = DateTime.Parse(dataGridView1.SelectedRows[0].Cells[3].Value.ToString());
            dtpRetour.Value = DateTime.Parse(dataGridView1.SelectedRows[0].Cells[4].Value.ToString());
            cmbTransport.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            txtImputation.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
            cmbExercice.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
            checkBox1.Checked = bool.Parse(dataGridView1.SelectedRows[0].Cells[8].Value.ToString());
            cmbEtat.Text = dataGridView1.SelectedRows[0].Cells[9].Value.ToString();
            idMission = Int32.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
        }


    }
}
