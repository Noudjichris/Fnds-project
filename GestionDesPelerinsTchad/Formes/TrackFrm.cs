using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SGSP.AppCode;
using GestionPharmacetique;

namespace SGSP.Formes
{
    public partial class TrackFrm : Form
    {
        public TrackFrm()
        {
            InitializeComponent();
        }

        private void TrackFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.WhiteSmoke, 5);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
               Color.SteelBlue, Color.SteelBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.White, 0);
            Rectangle area1 = new Rectangle(0, 0, groupBox1.Width - 1, groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
               Color.SteelBlue, Color.SteelBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void TrackFrm_Load(object sender, EventArgs e)
        {
            var dt = ConnectionClass.Log();
Track(dt);
        }

        void Track(DataTable dt)
        {
            try
            {
                dataGridView3.Rows.Clear();
                foreach (DataRow dtRow in dt.Rows)
                {
                    var etat = "";
                    if (dtRow.ItemArray[4].ToString()=="1")
                    {
                        etat = "Succés";
                    }
                    else
                    {
                        etat = "Echoué";
                    }

                    dataGridView3.Rows.Add(
                        dtRow.ItemArray[0].ToString(),
                        dtRow.ItemArray[1].ToString(),
                        dtRow.ItemArray[2].ToString(),
                        dtRow.ItemArray[3].ToString(),
                        etat
                        );
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("track", ex);
            }
        }

        private void eventLog1_EntryWritten(object sender, System.Diagnostics.EntryWrittenEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();

        }

        private void groupBox3_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.White, 0);
            Rectangle area1 = new Rectangle(0, 0, groupBox2.Width - 1, groupBox2.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
               Color.SteelBlue, Color.SteelBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            var dt = ConnectionClass.Log(dateTimePicker1.Value.Date);
            Track(dt);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var dt = ConnectionClass.Log(textBox1.Text);
            Track(dt);
        }

        private void button1_Click(object sender, EventArgs e)
        {
           if(
                MonMessageBox.ShowBox("Voulez vous vider les donnees","Confirmation")=="1")
            {  ConnectionClass.ViderLog();
            var dt = ConnectionClass.Log();
            Track(dt);
            }
        }
    }
}
