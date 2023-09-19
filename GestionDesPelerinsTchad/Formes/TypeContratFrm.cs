using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SGSP.Formes
{
    public partial class TypeContratFrm : Form
    {
        public TypeContratFrm()
        {
            InitializeComponent();
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.DodgerBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.DodgerBlue, Color.SteelBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex==3)
            {
                idContrat = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                checkBox1.Checked = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString());
            }else if(e.ColumnIndex==4)
            {
                if (AppCode.ConnectionClass.SupprimerUnTypeContrat(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString())))
                {
                    Liste();
                }
            }
        }

        void Liste()
        {
            try
            {
                dataGridView1.Rows.Clear();
                foreach(var s in AppCode.ConnectionClass.ListeContrat())
                {
                    dataGridView1.Rows.Add(s.IDTypeContrat, s.TypeContrat, s.EtatTaxe);
                }
            }
            catch { }
        }
        int idContrat;
        private void TypeContratFrm_Load(object sender, EventArgs e)
        {
            Liste();
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            try
            {
                
                    var s = new AppCode.Service();
                    s.TypeContrat = textBox1.Text;
                    s.IDTypeContrat = idContrat;
                s.EtatTaxe = checkBox1.Checked;
                    if (AppCode.ConnectionClass.EnregistrerUnTypeContrat(s))
                    {
                        Liste();
                    }
                
            }
            catch { }
        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
