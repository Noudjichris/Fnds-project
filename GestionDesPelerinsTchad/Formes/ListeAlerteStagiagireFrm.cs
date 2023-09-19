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
    public partial class ListeAlerteStagiagireFrm : Form
    {
        public ListeAlerteStagiagireFrm()
        {
            InitializeComponent();
        }

        private void ListeAlerteStagiagireFrm_Load(object sender, EventArgs e)
        {
            foreach (var s in AppCode.ConnectionClass.AlerteStages())
            {var liste=AppCode.ConnectionClass.ListeDesStagiaires(s.IDStagiaire);
                dataGridView1.Rows.Add(liste[0].Nom+" "+liste[0].Prenom,s.NatureStage,s.DateDebut.ToShortDateString(),
                   s.DateFin.ToShortDateString(), liste[0].Email, s.Status, s.DateFin.Subtract(DateTime.Now.Date).Days, "EMAIL");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 7)
                {
                    var message = "Bonjour  " + dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString() + ",\nNous vous rappelons que votre Votre stage prendra fin le :  "
                      + DateTime.Parse(dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString()).ToShortDateString()  +" dans "+
                      dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString() +"jour(s)"+
                      ".\nNous vous prions de vous rapprocher aux services des ressources humaines pour d'autres informations";
                    var mailReceveur = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                    MailMessage email = new MailMessage(AppCode.GlobalVariable.mailFrom, mailReceveur, "Rappel fin de stage", message);
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
