using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SGSP.AppCode;

namespace SGSP.Formes
{
    public partial class TBRHFrm : Form
    {
        public TBRHFrm()
        {
            InitializeComponent();
        }
        private void groupBox5_Paint(object sender, PaintEventArgs e)
        {
            var mGraphics = e.Graphics;
            var pen1 = new Pen(Color.FromArgb(255, 128, 0), 1);
            var area1 = new Rectangle(0, 0, this.groupBox5.Width - 1, this.groupBox5.Height - 1);
            var linearGradientBrush = new LinearGradientBrush(area1,
                Color.FromArgb(255, 128, 0), Color.FromArgb(255, 128, 0), LinearGradientMode.Horizontal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void TBPersonnelFrm_Paint(object sender, PaintEventArgs e)
        {
            var mGraphics = e.Graphics;
            var pen1 = new Pen(Color.White, 1);
            var area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            var linearGradientBrush = new LinearGradientBrush(area1,
                Color.White, Color.White, LinearGradientMode.Horizontal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            var mGraphics = e.Graphics;
            var pen1 = new Pen(Color.White, 1);
            var area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            var linearGradientBrush = new LinearGradientBrush(area1,
                Color.DodgerBlue, Color.DodgerBlue, LinearGradientMode.Horizontal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            var mGraphics = e.Graphics;
            var pen1 = new Pen(Color.FromArgb(255, 128, 0), 1);
            var area1 = new Rectangle(0, 0, this.grpSexe.Width - 1, this.grpSexe.Height - 1);
            var linearGradientBrush = new LinearGradientBrush(area1,
                Color.White, Color.White, LinearGradientMode.Horizontal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void TBRHFrm_Load(object sender, EventArgs e)
        {
            try
            {
                button6.Location = new Point(Width - button6.Width - 10, button6.Location.Y);
                //dataGridView1.RowTemplate.Height = dataGridView1.Height / 12;
                var height = (dataGridView2.Height - 35) / AppCode.ConnectionClass.ListeDirection().Count();
                dataGridView2.RowTemplate.Height = height;

                cmbTypeContrat.Items.Add("");
                dataGridView5.Columns.Add("cl1", "STATUS /DIRECTION");
                foreach (var c in AppCode.ConnectionClass.ListeContrat().Where(cn => !cn.TypeContrat.Contains("PDST") && !cn.TypeContrat.ToUpper().Contains("PROJET")))
                {
                    cmbTypeContrat.Items.Add(c.TypeContrat);
                    dataGridView4.Rows.Add(c.TypeContrat, "", "", "", "");
                    dataGridView5.Rows.Add(c.TypeContrat);
                }
                cmbDivision.Items.Add("");

                dataGridView5.Columns[dataGridView5.Columns.Count - 1].Width = dataGridView5.Width / 4;
                dataGridView6.Columns.Add("cl11", "GRADE /DIRECTION");
                dataGridView6.Columns[dataGridView5.Columns.Count - 1].Width = dataGridView6.Width / 4;
                foreach (var c in AppCode.ConnectionClass.ListeDirection())
                {

                    cmbDivision.Items.Add(c.Direction);
                    dataGridView2.Rows.Add(c.Abreviation, "", "", "", "");
                    dataGridView5.Columns.Add("cl01", c.Abreviation);
                    dataGridView6.Columns.Add("cl11", c.Abreviation);
                }



                var grades = new string[] {
                          "Ingénieur Statisticien Economiste",	
                             "Ingénieurs Statisticien Démographes",
                            "Ingénieurs d'Etat en Statistique et Planification",		
                        "Ingénieurs des Travaux Statistiques",		
                        "Ingénieurs des Travaux de Planification",		
                        "Informaticiens",		
                        "Economistes",		
                        "Administrateur/Manager",		
                        "Comptable",		
                        "Adjoints Techniques de la Statistique",
                        "Agents Techniques de la Statistique",		
                        "Autre cadres",		
                        "Personnel d'Appui"};
                dataGridView3.RowTemplate.Height = (dataGridView3.Height - 25) / grades.Count();
                foreach (var g in grades)
                {
                    dataGridView3.Rows.Add(g, "", "", "", "");
                    dataGridView6.Rows.Add(g);
                }
                ListeTB();
            }
            catch (Exception)
            {
            }          
        }

        void ListeTB()
        {
            try
            {
                var listePersonnel = from p in ConnectionClass.ListePersonnel("")
                                     join s in ConnectionClass.ListeServicePersonnel()
                                     on p.NumeroMatricule equals s.NumeroMatricule
                                     join dep in ConnectionClass.ListeDivision()
                                                    on s.IDDivision equals dep.IDDivision
                                                    join dir in ConnectionClass.ListeDirection()
                                                    on dep.IDDirection equals dir.IDDirection
                                     where dir.Direction.StartsWith(cmbDivision.Text, StringComparison.CurrentCultureIgnoreCase)
                                     where s.TypeContrat.StartsWith(cmbTypeContrat.Text, StringComparison.CurrentCultureIgnoreCase)
                                     select new { p.NumeroMatricule, p.Sexe ,dir.Abreviation,s.Grade,s.TypeContrat};
                var countFeminin = 0;
                var countMasculin = 0;
                foreach (var p in listePersonnel)
                {
                    if (p.Sexe == "F")
                    {
                        countFeminin += 1;
                    }
                    else if (p.Sexe == "M")
                    {
                        countMasculin += 1;
                    }
                }
                #region ParSexe
                var count = countMasculin + countFeminin;
                lblFemme.Text = countFeminin.ToString() ;
                lblHomme.Text = countMasculin.ToString()   ;
                lblPourFemelle.Text = Math.Round((double)countFeminin * 100 / count) + "%";
                lblPourSexeMale.Text= Math.Round((double)countMasculin * 100 / count )+ "%";
                lblCountTotalSexe.Text = count.ToString();
                #endregion
         
            #region ParDirection 
                foreach (DataGridViewRow dgv in dataGridView2.Rows)
                {
                    countFeminin = 0;
                    countMasculin = 0;
                    foreach (var p in listePersonnel.Where(c => c.Abreviation == dgv.Cells[0].Value.ToString()))
                    {
                        if (p.Sexe == "F")
                        {
                            countFeminin += 1;
                        }
                        else if (p.Sexe == "M")
                        {
                            countMasculin += 1;
                        }
                    }
                    count = countFeminin + countMasculin;
                    dgv.Cells[1].Value = countFeminin;
                    dgv.Cells[2].Value = countMasculin;
                    dgv.Cells[3].Value = count;
                    dgv.Cells[4].Value = Math.Round((double)count/listePersonnel.Count()*100,2);
                }
            #endregion

                #region ParGraddes
                foreach (DataGridViewRow dgv in dataGridView3.Rows)
                {
                    countFeminin = 0;
                    countMasculin = 0;
                    foreach (var p in listePersonnel.Where(c => c.Grade == dgv.Cells[0].Value.ToString()))
                    {
                        if (p.Sexe == "F")
                        {
                            countFeminin += 1;
                        }
                        else if (p.Sexe == "M")
                        {
                            countMasculin += 1;
                        }
                    }
                    count = countFeminin + countMasculin;
                    dgv.Cells[1].Value = countFeminin;
                    dgv.Cells[2].Value = countMasculin;
                    dgv.Cells[3].Value = count;
                    dgv.Cells[4].Value = Math.Round((double)count / listePersonnel.Count() * 100, 2);
                }
                #endregion

                #region ParStatusContrat
                foreach (DataGridViewRow dgv in dataGridView4.Rows)
                {
                    countFeminin = 0;
                    countMasculin = 0;
                    foreach (var p in listePersonnel.Where(c => c.TypeContrat == dgv.Cells[0].Value.ToString()))
                    {
                        if (p.Sexe == "F")
                        {
                            countFeminin += 1;
                        }
                        else if (p.Sexe == "M")
                        {
                            countMasculin += 1;
                        }
                    }
                    count = countFeminin + countMasculin;
                    dgv.Cells[1].Value = countFeminin;
                    dgv.Cells[2].Value = countMasculin;
                    dgv.Cells[3].Value = count;
                    dgv.Cells[4].Value = Math.Round((double)count / listePersonnel.Count() * 100, 2);
                }
                #endregion
      
                #region ParStatusDirection
                for(var  i = 0  ; i< dataGridView5.Rows.Count ; i++)
                {
                    for (var j= 1; j < dataGridView5.Columns.Count;j++)
                    {
                        countFeminin = 0;
                        countMasculin = 0;
                        var   liste =listePersonnel.Where(c => c.TypeContrat == dataGridView5.Rows[i].Cells[0].Value.ToString() && c.Abreviation == dataGridView5.Columns[j].HeaderText.ToString());
                      //var count 
                        dataGridView5.Rows[i].Cells[j].Value = liste.Count();
                        //dgv.Cells[4].Value = Math.Round((double)count / listePersonnel.Count() * 100, 2);
                    }
                }
                #endregion

                #region ParGradeDirection
                for (var i = 0; i < dataGridView6.Rows.Count; i++)
                {
                    for (var j = 1; j < dataGridView6.Columns.Count; j++)
                    {
                        countFeminin = 0;
                        countMasculin = 0;
                        var liste = listePersonnel.Where(c => c.Grade == dataGridView6.Rows[i].Cells[0].Value.ToString() && c.Abreviation == dataGridView6.Columns[j].HeaderText.ToString());
                        //var count 
                        dataGridView6.Rows[i].Cells[j].Value = liste.Count();
                        //dgv.Cells[4].Value = Math.Round((double)count / listePersonnel.Count() * 100, 2);
                    }
                }
                #endregion
            }
            catch (Exception)
            {
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {
            var countFeminin = 70;
            var countMasculin = 30;
            var count = countMasculin + countFeminin;
            lblFemme.Text = countFeminin.ToString() + "\n"+(int)countFeminin * 100/count + "%";
            lblHomme.Text = countMasculin.ToString() + "\n" + (int)countMasculin * 100 / count + "%"; ;
         
            var normalHeight = (grpSexe.Height - 25);
            lblFemme.Height = normalHeight * countFeminin / count;
            lblHomme.Height = normalHeight * countMasculin / count;
            lblHomme.Location = new Point(lblHomme.Location.X,  normalHeight - lblHomme.Height);
            lblFemme.Location = new Point(lblFemme.Location.X,  normalHeight-lblFemme.Height); 
        }

        private void cmbTypeContrat_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListeTB();
        }

        private void cmbDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListeTB();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Dispose();
        }


    }
}
