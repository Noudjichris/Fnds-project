using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SGSP.Formes
{
    public partial class ListePaiemenFrm : Form
    {
        public ListePaiemenFrm()
        {
            InitializeComponent();
        }

        private void ListePaiemenFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue, 5);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                SystemColors.Control, SystemColors.Control, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.Silver, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.SteelBlue, Color.DarkSlateBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        static ListePaiemenFrm listePaiFrm;
        static public string btnClick, mois;
        static public int numPaie, exercice;
        static public DateTime datePaie;
        static public decimal montant;

        private void ListePaiemenFrm_Load(object sender, EventArgs e)
        {
            dataGridView1.RowTemplate.Height = 25;
            Location = new Point((MainForm.width - Width) / 2, 143);
            var dt = AppCode.ConnectionClass.ListeoOrdrePaiement();
            ListePaiement();
            for (var i = 2017; i < DateTime.Now.Year +10; i++)
            {
                cmbExercice.Items.Add(i);
            }
            cmbExercice.Text = DateTime.Now.Date.Year.ToString();
        }

        void ListePaiement()
        {
            try
            {
                var elGR = System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR");
                dataGridView1.Rows.Clear();
                var listePaie = from p in AppCode.ConnectionClass.ListeoOrdrePaiement()
                                where p.Exercice == Convert.ToInt32(cmbExercice.Text)
                                select p;
                foreach (var p  in listePaie)
                {
                    dataGridView1.Rows.Add
                        (p.IDPaie,p.Exercice,p.MoisPaiement,p.DatePaiement.ToShortDateString(),p.MontantTotal,p.Directeur,p.Liquidateur,p.Controleur);
                }
            }
            catch { }
        }

        public static string ShowBox()
        {
            try
            {
                listePaiFrm = new  ListePaiemenFrm();
                listePaiFrm.ShowDialog();
            }
            catch (Exception ) {  }
            return btnClick;
        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            btnClick = "2";
            Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(dataGridView1.SelectedRows.Count>0)
            if (GestionPharmacetique.MonMessageBox.ShowBox("Voulez vous supprimer ces données?", "Confirmation") == "1")
            {
                numPaie = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());

                AppCode.ConnectionClass.SupprimerOrdreDePaiement(numPaie );
                var dt = AppCode.ConnectionClass.ListeoOrdrePaiement();
                ListePaiement( );
            }
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex ==8)
            {
                button1_Click(null, null);
            }
        }
       

        private void button2_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (dataGridView1.SelectedRows.Count > 0)
            //    {

            //        var numPaiement = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            //        var exercice = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[3].Value.ToString());
            //        var date = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
            //        var motantotal = Convert.ToDouble(dataGridView1.SelectedRows[0].Cells[2].Value.ToString());
            //        var mois = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            
            //        var p = AppCode.ConnectionClass.ListeDetailsPaiement(numPaiement);
            //        var count = p.Count;
                    
            //            SaveFileDialog sfd = new SaveFileDialog();
            //            sfd.Filter = "PDF Documents (*.pdf)|*.pdf";

            //            sharpPDF.pdfDocument document = new sharpPDF.pdfDocument("christian", "cdali");
            //            var jour = DateTime.Now.Day;
            //            var moiSs = DateTime.Now.Month;
            //            var year = DateTime.Now.Year;
            //            var hour = DateTime.Now.Hour;
            //            var min = DateTime.Now.Minute;
            //            var sec = DateTime.Now.Second;
            //            var datTe = jour.ToString() + "_" + moiSs.ToString() + "_" + year.ToString() + "_" + hour + "_" + min + "_" + sec;
            //            sfd.FileName = label1.Text.Replace("/", "_") + "_imprimé_le_" + datTe + ".pdf";
            //            //string pathFile = "";
            //            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            //            {

            //                var bitmap = AppCode.Impression.ImprimerOrdreDePaiement(numPaiement, date, exercice, mois);
            //                string inputImage1 = @"cdali" ;
            //                // Create an empty page
            //                sharpPDF.pdfPage page = document.addPage(500,700);

            //                document.addImageReference(bitmap, inputImage1);
            //                sharpPDF.Elements.pdfImageReference img = document.getImageReference(inputImage1);
            //                page.addImage(img, -10, 0, page.height, page.width);

                            
            //                if (count > 17)
            //                {
            //                    var div = (count - 17) / 26;
            //                    for (var i = 0; i <= div; i++)
            //                    {
            //                        if (i * 26 < count)
            //                        {
            //                            bitmap = AppCode.Impression.ImprimerOrdreDePaiement(numPaiement, date, exercice, mois, i);
            //                            var inputImage = @"cdali" + i;
            //                            // Create an empty page
            //                            sharpPDF.pdfPage pageIndex = document.addPage();
            //                            document.addImageReference(bitmap, inputImage);
            //                            sharpPDF.Elements.pdfImageReference img1 = document.getImageReference(inputImage);
            //                            pageIndex.addImage(img1, 10, 0, pageIndex.height, pageIndex.width);
            //                        }
            //                    }
            //                }
            //                document.createPDF(sfd.FileName);
            //                System.Diagnostics.Process.Start(sfd.FileName);
            //            }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    GestionPharmacetique.MonMessageBox.ShowBox("Imprimer paiement", ex);
            //}
        }

        private void cmbMois_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListePaiement();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    numPaie = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                    datePaie = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells[1].Value.ToString());
                    montant = Convert.ToDecimal(dataGridView1.SelectedRows[0].Cells[2].Value.ToString());
                    exercice = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[3].Value.ToString());
                    mois = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                    btnClick = "1";
                    Dispose();
                }
            }
            catch (Exception)
            {

            }
        }

        private void btnFermer_Click_1(object sender, EventArgs e)
        {
            Dispose();
        }
    }
}
