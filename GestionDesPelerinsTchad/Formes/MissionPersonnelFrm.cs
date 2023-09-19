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
    public partial class MissionPersonnelFrm : Form
    {
        public MissionPersonnelFrm()
        {
            InitializeComponent();
        }
        public int idMission,duree;
        public string numeroMatricule,objetMission;
        private void txtNom_KeyDown(object sender, KeyEventArgs e)
        {
            string matricule = "";
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(matricule))
                {
                    ListePerso.indexRecherche = txtNom.Text;
                    if (radioButton1.Checked)
                        ListePerso.siPersonnelProjet = false;
                    else if (radioButton2.Checked)
                        ListePerso.siPersonnelProjet = true;
                    if (ListePerso.ShowBox() == "1")
                    {
                        txtNom.Text = ListePerso.nomPersonnel;
                        matricule = ListePerso.numerMatricule;
                        numeroMatricule = ListePerso.numerMatricule;
                        txtRole.Focus();
                    }
                }
                else
                {
                    matricule = "";
                    txtRole.Focus();
                }
            }
        }

        private void txtRole_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtRole.Text))
                    txtFrais.Focus();

            }
        }

        private void txtFrais_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                double jour, frais;
                if (double.TryParse(txtFrais.Text, out frais) && double.TryParse(txtDuree.Text, out jour))
                {
                    txtTotal.Text=(jour*frais).ToString();
                    btnAjouter.Focus();
                }
            }
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtNom.Text) && !string.IsNullOrEmpty(txtRole.Text))
                {
                    double frais = .0, fraisTotal = .0; var  jour = 0;
                    if (double.TryParse(txtFrais.Text, out frais) &&
                        int.TryParse(txtDuree.Text, out jour)
                        && double.TryParse(txtTotal.Text, out fraisTotal))
                    {
                    }
                    var m = new AppCode.Mission();
                    if (string.IsNullOrEmpty(numeroMatricule))
                        numeroMatricule = "0";
                    m.Matricule = numeroMatricule;
                    m.IDMission = idMission;
                    m.Frais = frais;
                    m.FraisTotal = fraisTotal;
                    m.Durée = jour;
                    m.Role = txtRole.Text;
                    m.NomEmploye = txtNom.Text;
                    m.Ordre = dataGridView1.Rows.Count + 1;
                    if (radioButton1.Checked)
                        m.SiPersonnelProjet = false;
                    else if (radioButton2.Checked)
                        m.SiPersonnelProjet = true;
                    if (AppCode.ConnectionClass.InsererPersonnelDansUneMission(m))
                    {
                        txtRole.Text = "";
                        txtNom.Text = "";
                        txtFrais.Text = "";
                        txtTotal.Text = "";
                        txtNom.Focus(); ListePersonnelMission();
                    }
                }
            }
            catch
            { }
        }

        private void MissionPersonnelFrm_Load(object sender, EventArgs e)
        {
            //ListePersonnelMission();
            txtDuree.Text = duree.ToString();
            label2.Text = objetMission;
            ListePersonnelMission();
        }

        void ListePersonnelMission()
        {
            try
            {
                dataGridView1.Rows.Clear();
                var liste = AppCode.ConnectionClass.ListeMisson(idMission);
                foreach (var m in liste)
                {
                    dataGridView1.Rows.Add(m.IDMission, m.Matricule, m.Ordre, m.NomEmploye, m.Role, m.Durée, m.Frais, m.FraisTotal, m.SiPayant, m.SiPersonnelProjet);
                }
            }
            catch { }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex==10)
            {
               if( GestionPharmacetique.MonMessageBox.ShowBox("Voulez vous supprimer ces données?","Confirmation")=="1")
                {
                    var mission =new AppCode.Mission();
                    mission.IDMission = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                       mission.NomEmploye =  dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    if(AppCode.ConnectionClass.RetirerPersonnelDansUneMission(mission))
                        dataGridView1.Rows.Remove(dataGridView1.Rows[e.RowIndex]);

                }
            }
            
               //ListePersonnelMission();
            
        }

        private void txtDuree_TextChanged(object sender, EventArgs e)
        {
            double jour, frais;
            if (double.TryParse(txtFrais.Text, out frais) && double.TryParse(txtDuree.Text, out jour))
            {
                txtTotal.Text = (jour * frais).ToString();
                btnAjouter.Focus();
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 8)
            {
                var mission = new AppCode.Mission();
                mission.IDMission = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                mission.SiPayant = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString());
                mission.Matricule = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                if (AppCode.ConnectionClass.EtatPayementPersonnelDansUneMission(mission))
                { }
            }
            else if (e.ColumnIndex == 2)
            {
                var mission = new AppCode.Mission();
                mission.IDMission = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                //mission.SiPayant = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString());
                mission.Matricule = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                int ordre = 0;
                if (int.TryParse(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString(), out ordre))
                    mission.Ordre = ordre;

                AppCode.ConnectionClass.OrdonnerLesPersonnelsDeLaMission(mission);
                ListePersonnelMission();
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            txtNom.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            txtRole.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            txtDuree.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            txtFrais.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
            txtTotal.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
            numeroMatricule = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            if (Convert.ToBoolean(dataGridView1.SelectedRows[0].Cells[8].Value.ToString()))
                radioButton2.Checked = true;
            else 
                radioButton1.Checked = true;
            
        }

  
    }
}
