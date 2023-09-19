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
    public partial class RecapSalarialFrm : Form
    {
        public RecapSalarialFrm()
        {
            InitializeComponent();
        }

        public int numeroPaiement, idRecap, exercice;
        public string moinPaiement;
        private void RecapSalarialFrm_Load(object sender, EventArgs e)
        {
            dataGridView1.RowTemplate.Height = 25;
            var l =AppCode.ConnectionClass. ListeRecapitulatifs(numeroPaiement);
            if(l.Count()<=0)
            {
                var liste = new List<AppCode.Paiement>();
                foreach (var s in AppCode.ConnectionClass.ListeRecapPaiement(numeroPaiement))
                {
                    var p = new AppCode.Paiement();
                   if( s.LibelleRecap == "CDI" || s.LibelleRecap == "CDD")
                    {
                        p.LibelleRecap = "Contractuels";
                    }
                    else
                    {
                        p.LibelleRecap = s.LibelleRecap;
                    }
                    p.MontantTotal = s.MontantTotal;
                    liste.Add(p);
                }
                var dd = from f in liste
                        group f by f.LibelleRecap into ListRecap
                         select new { 
                                        LibelleRecap = ListRecap.Key,
                                        Montant=ListRecap.Sum( x =>x.MontantTotal)
                                    };
                foreach (var p in dd)
                {
                    var pp = new AppCode.Paiement();
                    pp.IDPaie = numeroPaiement;
                    pp.LibelleRecap = p.LibelleRecap;
                    pp.MontantTotal = p.Montant;
                    AppCode.ConnectionClass.EnregistrerRecapitulatif(pp, "1");
                }
            }
            ListeRecapitulatif();
        }

        void ListeRecapitulatif()
        {
            try
            {
                state=
                    "1";
                dataGridView1.Rows.Clear();
                var liste = AppCode.ConnectionClass.ListeRecapitulatifs(numeroPaiement);
                var montant = 0.0;
                foreach (var p in liste)
                {
                    dataGridView1.Rows.Add(p.IDRecap, p.IDPaie, p.LibelleRecap,  String.Format(elGR, "{0:0,0}", p.MontantTotal));
                    montant +=   p.MontantTotal;
                }
                label4.Text = String.Format(elGR, "{0:0,0}", montant);
            }
            catch { }
        }
        static System.Globalization.CultureInfo elGR = System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR");
        private void btnAjouter_Click(object sender, EventArgs e)
        {
            double montant;
            if (numeroPaiement > 0)
            {
                if (double.TryParse(txtMontant.Text, out montant))
                {
                    if (!string.IsNullOrWhiteSpace(txtLibelle.Text))
                    {
                        var p = new AppCode.Paiement();
                        p.MontantTotal = montant;
                        p.LibelleRecap = txtLibelle.Text;
                        p.IDPaie = numeroPaiement;
                        p.IDRecap = idRecap;
                        if (AppCode.ConnectionClass.EnregistrerRecapitulatif(p, state))
                        {
                            txtLibelle.Text = "";
                            txtMontant.Text = "";
                            ListeRecapitulatif(); state = "1";
                        }
                    }
                }
            }
        }
        string state;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                txtLibelle.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                txtMontant.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                idRecap = Int32.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                state = "2";
            }
            else if (e.ColumnIndex == 5)
            {
                var p =new AppCode.Paiement();
                p.IDRecap= Int32.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                if(AppCode.ConnectionClass.EnregistrerRecapitulatif(p, "3"))
                    dataGridView1.Rows.Remove(dataGridView1.CurrentRow);
            }
        }
        Bitmap document;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(document, 20, 20, document.Width, document.Height);
            e.HasMorePages = false;
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
        }

        private void btnApercu_Click(object sender, EventArgs e)
        {
            try
            {
                document = AppCode.Impression.ImprimerListeRecapitulatifs( dataGridView1,moinPaiement, exercice);
                if (printDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    printDocument1.PrinterSettings = printDialog1.PrinterSettings;
                    printPreviewDialog1.ShowDialog();
                }
            }
            catch (Exception Exception)
            { GestionPharmacetique.MonMessageBox.ShowBox("", Exception); }
        }
    }
}
