using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GestionPharmacetique.Forme
{
    public partial class AlerteFinContraCtrl : UserControl
    {
        public AlerteFinContraCtrl()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            BackColor = Color.OrangeRed;
            label1.ForeColor = SystemColors.Control;
            label2.ForeColor = SystemColors.Control;
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

        private void AlerteExpirationFrm_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
        static string state;
        public static AlerteFinContraCtrl ctrl;
        public static string ShowBox(Form box)
        {
            ctrl = new AlerteFinContraCtrl();
            ctrl.Location = new Point(20, 150);
            box.Controls.Add(ctrl);
            return state;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            state = "2";
            Dispose();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            state = "1";
            var frm = new SGSP.Formes.ListeRetraiteFinContratFrm();
            Dispose();
            frm.indexState = 1;
            frm.ShowDialog();
        }

    }
}
