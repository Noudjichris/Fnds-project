using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SGSP.Formes
{
    public partial class AlerteEchelon : UserControl
    {
        public AlerteEchelon()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Dispose();
        }
        public static string ShowBox( Form frm)
        {
            ctrl = new AlerteEchelon();
            ctrl.Location = new Point(450, 270);
            frm.Controls.Add(ctrl);
            return state;
        }

        private void AlerteEchelon_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            BackColor = Color.DeepSkyBlue;
            label1.ForeColor = Color.White;
            label2.ForeColor = Color.White;
            timer2.Start();
            timer1.Stop();
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            BackColor = SystemColors.Control; ;
            label1.ForeColor = SystemColors.Control;
            button1.ForeColor = SystemColors.Control;
            label2.ForeColor = SystemColors.Control;
            timer1.Start();
            timer2.Stop();

        }

        static string state;
        public static AlerteEchelon ctrl;
      
        private void label1_Click(object sender, EventArgs e)
        {
            try
            {
                state = "1";
                Dispose();
                var frm = new  ListeAvancementFrm();
                //frm.indexState = 2;
                frm.ShowDialog();
            }
            catch (Exception)
            {
            }
        }

    }
}
