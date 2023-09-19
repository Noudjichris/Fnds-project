using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SGSP.AppCode;

namespace SGSP.Formes
{
    public partial class InformationPersonnelFrm : Form
    {
        public InformationPersonnelFrm()
        {
            InitializeComponent();
        }

        private void InformationPersonnelFrm_Paint(object sender, PaintEventArgs e)
        {
            //var mGraphics = e.Graphics;
            //var pen1 = new Pen(Color.SteelBlue, 0);
            //var area1 = new Rectangle(0, 0, Width - 1, Height - 1);
            //var linearGradientBrush = new LinearGradientBrush(area1,
            //    Color.Black, Color.SteelBlue, LinearGradientMode.ForwardDiagonal);
            //mGraphics.FillRectangle(linearGradientBrush, area1);
            //mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            var mGraphics = e.Graphics;
            var pen1 = new Pen(Color.DarkBlue, 0);
            var area1 = new Rectangle(0, 0, groupBox1.Width - 1, groupBox1.Height - 1);
            var linearGradientBrush = new LinearGradientBrush(area1,
                Color.DarkBlue, Color.Black, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            var mGraphics = e.Graphics;
            var pen1 = new Pen(Color.FromArgb(128, 255, 128), 5);
            var area1 = new Rectangle(0, 0, groupBox2.Width - 1, groupBox2.Height - 1);
            var linearGradientBrush = new LinearGradientBrush(area1,
                Color.Black, Color.SteelBlue, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }


        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                var numeroMatricule = lblMatricule.Text;
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "PDF Documents (*.pdf)|*.pdf";

                sharpPDF.pdfDocument pdfDocument = new sharpPDF.pdfDocument("christian", "cdali");
                var jour = DateTime.Now.Day;
                var mois = DateTime.Now.Month;
                var year = DateTime.Now.Year;
                var hour = DateTime.Now.Hour;
                var min = DateTime.Now.Minute;
                var sec = DateTime.Now.Second;
                var date = jour.ToString() + "_" + mois.ToString() + "_" + year.ToString() + "_" + hour + "_" + min + "_" + sec;

                var pathFolder = "C:\\Dossier Personnel\\Liste Personnel";
            
                if (!System.IO.Directory.Exists(pathFolder))
                {
                    System.IO.Directory.CreateDirectory(pathFolder);
                }
                sfd.InitialDirectory = pathFolder;
                sfd.FileName = "Details de _" +lblNom.Text + " " + lblPrenom.Text +" "+ date + ".pdf";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    document = Impression.ImprimerInformatonDunPersonnel(numeroMatricule);

                    sharpPDF.pdfPage pageIndex = pdfDocument.addPage();

                    var inputImage = @"cdali";
                    pdfDocument.addImageReference(document, inputImage);
                    sharpPDF.Elements.pdfImageReference img1 = pdfDocument.getImageReference(inputImage);
                    pageIndex.addImage(img1, -10, 0, pageIndex.height, pageIndex.width);
                    //}
                    pdfDocument.createPDF(sfd.FileName);
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
            }
            catch (Exception)
            {
            }
        }

        private Bitmap document;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(document, 5, 20, document.Width, document.Height);
            e.HasMorePages = false;
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
        }

            private void printPreviewDialog2_Load(object sender, EventArgs e)
        {
            printPreviewDialog2.Document = printDocument1;
        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void InformationPersonnelFrm_Load(object sender, EventArgs e)
        {

        }

        //private void printDocument2_PrintPage(object sender, PrintPageEventArgs e)
        //{
        //    e.Graphics.DrawImage(document1, 5, 20, document1.Width, document1.Height);
        //    e.HasMorePages = false;
        //}

    }
}
