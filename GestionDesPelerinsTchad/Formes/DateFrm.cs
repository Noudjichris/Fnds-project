using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SGDP.Formes
{
    public partial class DateFrm : Form
    {
        public DateFrm()
        {
            InitializeComponent();
        }

        private void DateFrm_Load(object sender, EventArgs e)
        {
            if (state)
            {
                checkBox1.Visible = true ;
            }
        }

        private void DateFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(SystemColors.ControlLight, 3);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.White
                , SystemColors.Control, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(SystemColors.ControlLight, 3);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.SlateGray
                , Color.SteelBlue, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        public static bool flag, state;
        public static DateTime dateDebut, dateFin;
        static DateFrm frm;

        public static bool ShowBox()
        {
            try
            {
                frm = new DateFrm();
                frm.ShowDialog();
                return flag;
            }
            catch 
            { return false; }
        }

        public static bool checkVenteTotal;

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (checkBox1.Checked)
                {
                    checkVenteTotal = true;
                }
                else
                {
                    checkVenteTotal = false;
                }
                dateDebut = dateTimePicker1.Value.Date;
                dateFin = dateTimePicker2.Value.Date

                    ;
                flag = true;
                Dispose();
            }
            catch { }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            flag = false;
            Dispose();
        }

        private void dateTimePicker2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button6_Click(null, null);
            }
        }
    }
}
