using SGSP.AppCode;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Mail;

namespace SGSP.Formes
{
    public partial class ListeRetraiteFinContratFrm : Form
    {
        public ListeRetraiteFinContratFrm()
        {
            InitializeComponent();
        }
       public  int indexState;
        private void ListeRetraiteFinContratFrm_Load(object sender, EventArgs e)
        {
            try
            {
                var List = new List<Service>();
                if(indexState==1)
                {
                    List = ConnectionClass.ListeAlerteFinContrat();
                    foreach (var s in List)
                    {
                        var listePersonnel = ConnectionClass.ListePersonnelParMatricule(s.NumeroMatricule);
                           var service = ConnectionClass.ListeServicePersonnel(s.NumeroMatricule);
                        dataGridView1.Rows.Add(
                               listePersonnel[0].NumeroMatricule,
                               listePersonnel[0].Nom +" "+listePersonnel[0].Prenom,
                               service[0].Poste,
                               service[0].DateService.ToShortDateString(),
                               service[0].DateDepart.ToShortDateString(),
                               service[0].Echelon,
                               service[0].Anciennete,
                               service[0].Categorie,
                               listePersonnel[0].Email,
                               "EMAIL"
                               );
                    }
                }
                else if(indexState==2)
                {
                    //List = ConnectionClass.ListeAlerteRetraite();
                    //foreach (var s in List)
                    //{
                    //    var dtPersonnel = ConnectionClass.ListeDesPersonnelParNumeroMatricule(s.NumeroMatricule);
                    //     dataGridView1.Rows.Add(
                    //            dtPersonnel.Rows[0].ItemArray[0].ToString(),
                    //            dtPersonnel.Rows[0].ItemArray[1].ToString(),
                    //            dtPersonnel.Rows[0].ItemArray[2].ToString(),
                    //            dtPersonnel.Rows[0].ItemArray[12].ToString(),
                    //            DateTime.Parse(dtPersonnel.Rows[0].ItemArray[17].ToString()).ToShortDateString(),
                    //           DateTime.Parse(dtPersonnel.Rows[0].ItemArray[21].ToString()).ToShortDateString(),
                    //            dtPersonnel.Rows[0].ItemArray[14].ToString(),
                    //            dtPersonnel.Rows[0].ItemArray[17].ToString(),
                    //           dtPersonnel.Rows[0].ItemArray[25].ToString(),
                    //            dtPersonnel.Rows[0].ItemArray[18].ToString(),
                    //            dtPersonnel.Rows[0].ItemArray[19].ToString()
                    //            );
                    //}
                }
               
            }
            catch { }
        }

        private void btnAjouterUneAgence_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
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

                    var pathFolder = "C:\\Dossier Personnel";
                    if (!System.IO.Directory.Exists(pathFolder))
                    {
                        System.IO.Directory.CreateDirectory(pathFolder);
                    }
                    pathFolder = pathFolder + "\\Liste Personnel";
                    if (!System.IO.Directory.Exists(pathFolder))
                    {
                        System.IO.Directory.CreateDirectory(pathFolder);
                    }
                    sfd.InitialDirectory = pathFolder;
                    string titreImpression="";
                    if (indexState == 1)
                        titreImpression = "Liste de personnel approchant vers la fin du contrat";
                    else if (indexState == 2)
                        titreImpression = "Liste de personnel approchant vers la retraite";
                    sfd.FileName = titreImpression + "_" + date + ".pdf";
                    Bitmap document;

                    var div = dataGridView1.Rows.Count / 44;
                    for (var i = 0; i <= div; i++)
                    {
                       
                            document = Impression.ImprimerLalisteDesPersonnelsContratFinOuRetraite(dataGridView1, titreImpression, i, dataGridView1.Rows.Count.ToString());
                        
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
            catch (Exception)
            {
            }
        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 9)
                {
                   var mois =  (DateTime.Parse(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString()).Subtract(DateTime.Now.Date)).Days / 30;
                   var jour = (DateTime.Parse(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString()).Subtract(DateTime.Now.Date)).Days %30;
                    var message = "Bonjour  " + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + ",\nNous vous rappelons que votre Votre date de depart pour la retraite  est dans    "+mois +"mois et " + jour +"jours. ("
                      + DateTime.Parse(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString()).ToShortDateString() + ") "+
                      ".\nNous vous prions de vous rapprocher aux services des ressources humaines pour d'autres informations";
                    var mailReceveur = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                    MailMessage email = new MailMessage(AppCode.GlobalVariable.mailFrom, mailReceveur, "Rappel depart retraite", message);
                    SmtpClient client = new SmtpClient(AppCode.GlobalVariable.smtpClient);
                    client.Port = AppCode.GlobalVariable.emaiPort;
                    client.Credentials = new System.Net.NetworkCredential(AppCode.GlobalVariable.mailFrom, AppCode.GlobalVariable.credentialPaasword);
                    client.EnableSsl = true;
                    client.Send(email);
                    GestionPharmacetique.MonMessageBox.ShowBox("Email envoyé avec succés", "Affirmation");
                }
            }
            catch (Exception ex)
            { }
        }
    }
}
