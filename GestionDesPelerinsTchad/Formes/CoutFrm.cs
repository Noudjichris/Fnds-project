using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace SGSP.Formes
{
    public partial class CoutFrm : Form
    {
        public CoutFrm()
        {
            InitializeComponent();
        }


        private void CoutFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue, 2);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                SystemColors.Control, Color.White, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }


        private void label2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CoutFrm_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
