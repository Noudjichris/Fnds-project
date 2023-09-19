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
    public partial class ListeFacturesFrm : Form
    {
        public ListeFacturesFrm()
        {
            InitializeComponent();
        }

        static ListeFacturesFrm listeFacturesFrm;
       static  string btnClick;
       public static int numero;
        private void ListeFacturesFrm_Load(object sender, EventArgs e)
       {
           var dt = SGSP.AppCode.ConnectionClass.ListeDesDocuments("");
            //foreach(DataRow dtRow in dt.Rows)
            //{
            //    dataGridView1.Rows.Add(dtRow.ItemArray[0].ToString(),
            //        DateTime.Parse(dtRow.ItemArray[9].ToString()).ToShortDateString(),
            //        dtRow.ItemArray[1].ToString(), dtRow.ItemArray[10].ToString());
            //}
       }

        public static string ShowBox()
        {
            try
            {
                listeFacturesFrm = new ListeFacturesFrm();

                listeFacturesFrm.ShowDialog();
            }
            catch
            { }
            return btnClick;
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                btnClick = "1";
                numero = Int32.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                listeFacturesFrm.Dispose();
            }
        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
