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
    public partial class ListeAvancementFrm : Form
    {
        public ListeAvancementFrm()
        {
            InitializeComponent();
        }

        private void ListeAvancementFrm_Load(object sender, EventArgs e)
        {
            foreach (var l in AppCode.ConnectionClass.AlerteAvancement())
            {
                var p  =AppCode.ConnectionClass.ListePersonnelParMatricule(l.NumeroMatricule);
                dataGridView1.Rows.Add(l.NumeroMatricule , p[0].Nom + " " + p[0].Prenom , p[0].Sexe,
                    l.DateService.ToShortDateString(), l.DateService.AddYears(l.IDDirection).ToShortDateString(), l.Poste, l.Echelon,"EMAIL",p[0].Email);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 7)
                {
                    var echelon = Int32.Parse(dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString()) + 1;
                    var message = "Bonjour  " + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + ",\nNous vous rappelons que votre Votre prochain avancement est au :  "
                      +   DateTime.Parse(dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString()).ToShortDateString() + " avec un echelon de : " + echelon+
                      ".\nNous vous prions de vous rapprocher aux services des ressources humaines pour d'autres informations";
                    var mailReceveur = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                    MailMessage email = new MailMessage(AppCode.GlobalVariable.mailFrom, mailReceveur, "Rappel avancement", message);
                    SmtpClient client = new SmtpClient(AppCode.GlobalVariable.smtpClient);
                    client.Port = AppCode.GlobalVariable.emaiPort;
                    client.Credentials = new System.Net.NetworkCredential(AppCode.GlobalVariable.mailFrom, AppCode.GlobalVariable.credentialPaasword);
                    client.EnableSsl = true;
                    client.Send(email);
                    GestionPharmacetique.MonMessageBox.ShowBox("Email envoyé avec succés", "Affirmation");
                }
            }
            catch(Exception ex)
            { }
        }
    }
}
