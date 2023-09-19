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
    public partial class AlerteFinEtudeFrm : UserControl
    {
        public AlerteFinEtudeFrm()
        {
            InitializeComponent();
        }

        private void AlerteFinEtudeFrm_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
        
        public static string ShowBox(Form frm)
        {
            ctrl = new AlerteFinEtudeFrm();
            ctrl.Location = new Point(80, 380);
            frm.Controls.Add(ctrl);
            return state;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            BackColor = Color.GreenYellow;
            label1.ForeColor = Color.Black;
            label2.ForeColor = Color.Black;
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
        public static AlerteFinEtudeFrm ctrl;

        private void label1_Click(object sender, EventArgs e)
        {
            try
            {
                state = "1";
                Dispose();
                var frm = new ListeFinEtudesFrm();
                //frm.indexState = 2;
                frm.ShowDialog();
            }
            catch (Exception)
            {
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dispose();
        }

    }
}
