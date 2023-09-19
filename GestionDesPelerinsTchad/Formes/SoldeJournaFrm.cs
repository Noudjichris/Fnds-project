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
    public partial class SoldeJournaFrm : Form
    {
        public SoldeJournaFrm()
        {
            InitializeComponent();
        }

        DateTime dateDebut, dateFin; public string mois; public int annee;
        string ObtenirMois(int mois)
        {
            switch (mois)
            {
                case 1:
                    return "Janvier";
                case 2:
                    return "Fevrier";
                case 3:
                    return "Mars";
                case 4:
                    return "Avril";
                case 5:
                    return "Mai";
                case 6:
                    return "Juin";
                case 7:
                    return "Juillet";
                case 8:
                    return "Août";
                case 9:
                    return "Septembre";
                case 10:
                    return "Octobre";
                case 11:
                    return "Novembre";
                case 12:
                    return "Decembre";
                default:
                    return "";
            };
        }

        DateTime ObtenirDebutJour(int mois)
        {
            return DateTime.Parse("01/" + mois + "/" + dateDebut.Year);
        }

        DateTime ObtenirFinJour(int mois)
        {
            if (mois == 1)
            {
                return DateTime.Parse("31/" + mois + "/" + dateDebut.Year);
            }
            else if (mois == 2)
            {
                if (DateTime.IsLeapYear(dateDebut.Year))
                    return DateTime.Parse("29/" + mois + "/" + dateDebut.Year);
                else
                    return DateTime.Parse("28/" + mois + "/" + dateDebut.Year);
            }
            else if (mois == 3)
            {
                return DateTime.Parse("31/" + mois + "/" + dateDebut.Year);
            }
            else if (mois == 4)
            {
                return DateTime.Parse("30/" + mois + "/" + dateDebut.Year);
            }
            else if (mois == 5)
            {
                return DateTime.Parse("31/" + mois + "/" + dateDebut.Year);
            }
            else if (mois == 6)
            {
                return DateTime.Parse("30/" + mois + "/" + dateDebut.Year);
            }
            else if (mois == 7)
            {
                return DateTime.Parse("31/" + mois + "/" + dateDebut.Year);
            }
            else if (mois == 8)
            {
                return DateTime.Parse("31/" + mois + "/" + dateDebut.Year);
            }
            else if (mois == 9)
            {
                return DateTime.Parse("30/" + mois + "/" + dateDebut.Year);
            }
            else if (mois == 10)
            {
                return DateTime.Parse("31/" + mois + "/" + dateDebut.Year);
            }
            else if (mois == 11)
            {
                return DateTime.Parse("30/" + mois + "/" + dateDebut.Year);
            }
            else if (mois == 12)
            {
                return DateTime.Parse("31/" + mois + "/" + dateDebut.Year);
            }
            else
            {
                return dateDebut;
            }
        }
        static System.Globalization.CultureInfo elGR = System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR");

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                var listeDesJour = new List<DateTime>();
                var days =dateTimePicker1.Value.Date.Subtract(dtpEncaissement.Value.Date).Days;

                for (var i = 0; i <= days; i++)
                {
                    listeDesJour.Add(dtpEncaissement.Value.Date.AddDays(i));
                }
                label13.Text = "JOURNAL DE LA CAISSE DU " +
                    dtpEncaissement.Value.ToShortDateString()+ " AU " + dateTimePicker1.Value.ToShortDateString();
                var totalSolde = .0; 
                foreach (var d in listeDesJour)
                {
                    var totalDepense = .0;
                    var liste = AppCode.ConnectionClass.ListeEncaissementGroupeParJour(d);
                    var listeDep = from dep in AppCode.ConnectionClass.ListeDesDepenses()
                                   where dep.Date >= d
                                   where dep.Date < d.AddHours(24)
                                   select dep.Montant;
                    foreach(var m in listeDep)
                    {
                        totalDepense +=m;
                    }
                    var totalJour = .0;
                    foreach(var c in liste)
                    {
                        totalSolde += c.Montant;
                        totalSolde += c.Avoir;
                        totalJour += c.Montant;
                        totalJour += c.Avoir;
                    }
                    totalSolde = totalSolde - totalDepense;
                    dataGridView1.Rows.Add(d.ToShortDateString(), String.Format(elGR, "{0:0,0}", totalJour), String.Format(elGR, "{0:0,0}", totalDepense), String.Format(elGR, "{0:0,0}", totalSolde));
                }
            }
            catch { }
        }

        private void ToCsV1(DataGridView dGV, string filename)
        {
            string stOutput = "";
            // Export titles:
            string sHeaders = "";

            for (int j = 0; j < dGV.Columns.Count; j++)
                sHeaders = sHeaders.ToString() + Convert.ToString(dGV.Columns[j].HeaderText) + "\t";
            stOutput += sHeaders + "\r\n";
            // Export data.
            for (int i = 0; i < dGV.RowCount; i++)
            {
                string stLine = "";
                for (int j = 0; j < dGV.Rows[i].Cells.Count; j++)
                    stLine = stLine.ToString() + Convert.ToString(dGV.Rows[i].Cells[j].Value) + "\t";
                stOutput += stLine + "\r\n";
            }
            Encoding utf16 = Encoding.GetEncoding(1254);
            byte[] output = utf16.GetBytes(stOutput);
           System.IO. FileStream fs = new System.IO.FileStream(filename, System.IO.FileMode.Create);
            System.IO.BinaryWriter bw = new System.IO.BinaryWriter(fs);
            bw.Write(output, 0, output.Length); //write the encoded file
            bw.Flush();
            bw.Close();
            fs.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    var titre = label13.Text;
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

                    var pathFolder = "C:\\Dossier Personnel\\Caisse";
                    if (!System.IO.Directory.Exists(pathFolder))
                    {
                        System.IO.Directory.CreateDirectory(pathFolder);
                    }
                    sfd.InitialDirectory = pathFolder;
                    sfd.FileName ="Journal de la caisse_" + date + ".pdf";

                    var div = dataGridView1.Rows.Count / 48;
                    for (var i = 0; i <= div; i++)
                    {
                        var document =AppCode. Impression.ImprimerJournalCaisse(dataGridView1, titre, i);

                        sharpPDF.pdfPage pageIndex = pdfDocument.addPage();

                        var inputImage = @"cdali" + i;
                        pdfDocument.addImageReference(document, inputImage);
                        sharpPDF.Elements.pdfImageReference img1 = pdfDocument.getImageReference(inputImage);
                        pageIndex.addImage(img1, 0, 0, pageIndex.height, pageIndex.width);
                    }
                    pdfDocument.createPDF(sfd.FileName);
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
            }
            catch (Exception ex) {GestionPharmacetique. MonMessageBox.ShowBox("", ex); }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Excel Documents (*.xls)|*.xls";
                var jour = DateTime.Now.Day;
                var mois = DateTime.Now.Month;
                var year = DateTime.Now.Year;
                var hour = DateTime.Now.Hour;
                var min = DateTime.Now.Minute;
                var sec = DateTime.Now.Second;
                var date = jour.ToString() + "_" + mois.ToString() + "_" + year.ToString() + "_" + hour + "_" + min + "_" + sec;
                foreach (DataGridViewRow r in dataGridView1.Rows)
                {
                    double montant;
                    if (double.TryParse(r.Cells[2].Value.ToString(), out montant))
                        r.Cells[1].Value = montant;
                    if (double.TryParse(r.Cells[2].Value.ToString(), out montant))
                        r.Cells[2].Value = montant;
                }
                sfd.FileName = "Journal_caisses__Impriméé_le_" + date + ".xls";
                if (sfd.ShowDialog() == DialogResult.OK)
                {

                    ToCsV1(dataGridView1, sfd.FileName); // Here dataGridview1 is your grid view name
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("", ex);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1_Click(null, null);
        }

        private void SoldeJournaFrm_Load(object sender, EventArgs e)
        {
            try
            {
                label13.Text = "JOURNAL DE LA CAISSE GROUPE PAR JOUR";
               
            }
            catch { }
        }
    }
}
