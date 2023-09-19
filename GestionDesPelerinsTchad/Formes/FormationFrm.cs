using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using SGSP.AppCode;
using GestionPharmacetique;

namespace SGSP.Formes
{
    public partial class FormationFrm : Form
    {
        public FormationFrm()
        {
            InitializeComponent();
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.Silver, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.SteelBlue, Color.DodgerBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox4_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.AliceBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox4.Width - 1, this.groupBox4.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.AliceBlue, Color.AliceBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue, 1);
            Rectangle area1 = new Rectangle(0, 0, this.panel2.Width - 1, this.panel2.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.AliceBlue, Color.AliceBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void groupBox6_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.AliceBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox6.Width - 1, this.groupBox6.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.AliceBlue, Color.AliceBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox5_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.AliceBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox5.Width - 1, this.groupBox5.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.AliceBlue, Color.AliceBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.AliceBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox2.Width - 1, this.groupBox2.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.SteelBlue, Color.DodgerBlue, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void groupBox3_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.AliceBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox3.Width - 1, this.groupBox3.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.SteelBlue, Color.DodgerBlue, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox7_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.AliceBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox7.Width - 1, this.groupBox7.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.SteelBlue, Color.DodgerBlue, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
       
       private void DemandeFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.White, 0);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
              SystemColors.Control, Color.AliceBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
       string fonction;
        private void button21_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DemandeFrm_Load(object sender, EventArgs e)
        {
            try
            {
                etat = "1";
                panel2.Visible = false;
                txtDureeFormation.Enabled = true;
                panel1.Location = new Point(dataGridView1.Width + 15, panel1.Location.Y);
                for (var i = 2017; i < DateTime.Now.Year + 10; i++)
                {
                    cmbExercice.Items.Add(i.ToString());
                }
                cmbExercice.Text = DateTime.Now.Year.ToString();
            
                button5.Location = new Point(Width - button5.Width - 10, button5.Location.Y);
              
                etat = "1";
                //DureeDeFormation();
                _typeFormation = "";
                ListeDesFormations();
                Column5.Width = 100;
                Column4.Width = 100;
                Column6.Width = 100;
                Column9.Width = 40;
                Column11.Width = 40;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste  dep", ex);
            }

        }

        //liste des personnels
        private void ListeDesFormations()
        {
            try
            {
                dataGridView1.Rows.Clear();
                var liste = from f in ConnectionClass.ListeFormation()
                            where f.Exercice == Convert.ToInt32(cmbExercice.Text)
                            select f;
                foreach (var  f in liste)
                {
                    dataGridView1.Rows.Add(
                        f.NumeroFormation,
                        f.TypeFormation,f.Exercice,
                        f.DateDebutFormation.ToShortDateString(),
                        f.DateFinFormation.ToShortDateString(),
                        f.DureeFormation,f.Formateur,f.LieuFormation,
                        f.Imputation,
                        f.Description
                    );

                }
            }
            catch (Exception ex) { GestionPharmacetique.MonMessageBox.ShowBox("t", ex); }
        }

        
        #region

        private int _duree;
        private DateTime _dateFomation;
        private string _typeFormation, _nom, _prenom;
        #endregion

        private int numeroFormation; string etat;

        private void button1_Click(object sender, EventArgs e)
        {
            ListeDesFormations();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        void  DureeDeFormation()
        {
            try
            {
                var dateDebut =dtp1.Value.Date;
                var dateFin = dtp2.Value.Date;
                         
                var day = dateFin.DayOfYear - dateDebut.DayOfYear;
                var i = dateFin.Year - dateDebut.Year;
                 day = day + 365*i;
                
                 if (day >= 0)
                 {
                         day++;
                     txtDureeFormation.Text = day.ToString();
                 }
                 else
                 {
                     txtDureeFormation.Text = "0";
                 }
            }
            catch { }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtTypeFormation.Text))
                {

                    var formation = new Formation();

                    formation.TypeFormation = txtTypeFormation.Text;
                    formation.Description = txtDescription.Text;
                    formation.DureeFormation = Convert.ToInt32(txtDureeFormation.Text);
                    formation.NumeroFormation = numeroFormation;
                    formation.Formateur = txtFormateur.Text;
                    formation.DateDebutFormation = dtp1.Value.Date;
                    formation.DateFinFormation = dtp2.Value.Date;
                    formation.LieuFormation = txtieuFormation.Text;
                    formation.Imputation = txtImputation.Text;
                    
                    formation.Exercice = Convert.ToInt32(cmbExercice.Text);
                    if (ConnectionClass.EnregistrerUneFormation(formation))
                    {
                        numeroFormation = 0;
                        _typeFormation = "";
                        txtDescription.Text = "";
                        txtDureeFormation.Text = "";
                        txtTypeFormation.Text = "";
                        txtFormateur.Text = "";
                        txtDureeFormation.Text = "";
                        txtImputation.Text = "";
                        txtieuFormation.Text = "";
                        fonction = "";
                        cmbExercice.Text = DateTime.Now.Year.ToString();
                        ListeDesFormations();
                        //DureeDeFormation();
                    }
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("", ex);
            }
        }
        string  numeroMatricule;
        private void txtPersonnel_KeyDown(object sender, KeyEventArgs e)
        {
            string matricule = "";
            if (e.KeyCode == Keys.Enter)
            {

                if (string.IsNullOrEmpty(matricule))
                {
                    button2.Focus();
                    ListePerso.indexRecherche = txtPersonnel.Text;
                    if (radioButton1.Checked)
                        ListePerso.siPersonnelProjet = false;
                    else if (radioButton2.Checked)
                        ListePerso.siPersonnelProjet = true;
                    if (ListePerso.ShowBox() == "1")
                    {
                        txtPersonnel.Text = ListePerso.nomPersonnel;
                        matricule = ListePerso.numerMatricule;
                        numeroMatricule = ListePerso.numerMatricule;
                        fonction = ListePerso.fonction;
                        //txtDuree.Focus();
                        button2.Focus();
                    }
                }
                else
                {
                    matricule = "";
                    txtDuree.Focus();
                }
            }
            //try
            //{
            //    if (e.KeyCode == Keys.Enter)
            //    {
            //        ListePerso.indexRecherche = txtPersonnel.Text;
            //        if (ListePerso.ShowBox() == "1")
            //        {
            //            var f = new AppCode.Formation();
            //            f.NumeroMatricule = ListePerso.numerMatricule;
            //            f.NumeroFormation = numeroFormation;
            //            var found = false;
            //            if (dataGridView2.Rows.Count > 0)
            //            {
            //                foreach (DataGridViewRow dg in dataGridView2.Rows)
            //                {
            //                    if (dg.Cells[1].Value.ToString().Equals(f.NumeroMatricule))
            //                    {
            //                        found = true;
            //                    }
            //                }
            //                if (!found)
            //                {
            //                    if (AppCode.ConnectionClass.InsererUneFormation(f))
            //                    {
            //                        txtPersonnel.Text = "";
            //                        ListeParticipants();
            //                    }
            //                }

            //            }
            //            else
            //            {   
            //                if (AppCode.ConnectionClass.InsererUneFormation(f))
            //                {
            //                    txtPersonnel.Text = "";
            //                    ListeParticipants();
            //                }
            //            }
            //        }
            //    }
            //}
            //catch { }
        }

        void ListeParticipants()
        {
            try
            {
                dataGridView2.Rows.Clear();
                var liste = from l in AppCode.ConnectionClass.ListeFormation(numeroFormation)
                            orderby l.NomPersonnel
                            select l;
                if (liste.Count() > 0)
                {
                    foreach (var l in liste)
                    {
                          dataGridView2.Rows.Add(l.NumeroFormation, l.NumeroMatricule,l.NomPersonnel, l.DureeFormation,l.Frais,l.FraisTotal,l.SiPayant);
                    }
                }
            }
            catch { }
        }

        private void btnParticipants_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                numeroFormation = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                txtDuree.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
                ListeParticipants();
                panel2.Visible = true;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                try
                {
                        var f = new Formation();
                        f.NumeroFormation = numeroFormation;
                        f.NumeroMatricule = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
                        if (MonMessageBox.ShowBox("Voulez vous retirer cette personne de la liste ?", "Confirmation") == "1")
                        {
                            ConnectionClass.SuppressionDeLaFormation(f);
                            ListeParticipants();
                        }
                    }
                
                catch { }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 11)
                {
                    if (MonMessageBox.ShowBox("Voulez vous supprimer ces données?", "Confirmation") == "1")
                    {
                        AppCode.ConnectionClass.SuppressionDeLaFormation(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()));
                        dataGridView1.Rows.Remove(dataGridView1.Rows[e.RowIndex]);
                    }
                }
                else if (e.ColumnIndex == 10)
                {
                    numeroFormation = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                    txtTypeFormation.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    cmbExercice.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    dtp1.Value=Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString());
                    dtp2.Value=Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString());
                    txtDureeFormation.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                    txtDescription.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                    txtFormateur.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                    txtieuFormation.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                    txtImputation.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                }
            }
            catch { }
        }

        private void dtpDateDemande_ValueChanged(object sender, EventArgs e)
        {
            DureeDeFormation();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPersonnel.Text))
            {
                int durre;
                if (Int32.TryParse(txtDuree.Text, out durre))
                {
                    double frais, total;
                    if (double.TryParse(txtFrais.Text, out frais))
                    {
                    }
                    else
                    {
                        frais = 0;
                    }
                    if (double.TryParse(txtTotal.Text, out total))
                    {

                    }
                    else
                    {
                        total = 0;
                    }
                    var formation = new Formation();
                    formation.NumeroFormation = numeroFormation;
                    formation.NumeroMatricule = numeroMatricule;
                    formation.DureeFormation = durre;
                    formation.FraisTotal = total;
                    formation.Fonction = fonction;
                    formation.NomPersonnel = txtPersonnel.Text;
                    if (radioButton1.Checked)
                        formation.SiProjet = false;
                    else if (radioButton2.Checked)
                        formation.SiProjet = true;
                    formation.Frais = frais;
                    if (AppCode.ConnectionClass.InsererUneFormation(formation))
                    {
                        //txtTotal.Text = "";
                        txtPersonnel.Text = "";
                        //txtDuree.Text = "";
                        //txtFrais.Text = "";
                        //txtTotal.Text = "";
                        txtPersonnel.Focus();
                        ListeParticipants();
                    }
                }
            }
        }

        private void txtDuree_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int durre;
                if (Int32.TryParse(txtDuree.Text, out durre))
                {
                    txtFrais.Focus();
                }
            }
        }

        private void txtFrais_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                double frais, durre;
                if (double.TryParse(txtDuree.Text, out durre) && double.TryParse(txtFrais.Text, out frais))
                {
                    button2.Focus();
                    var fraisTotal = frais * durre;
                    txtTotal.Text = fraisTotal.ToString();
                }
            }
        }

    }
}
