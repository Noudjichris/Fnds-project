using System;
using System.Security;
using System.Drawing.Drawing2D;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GestionPharmacetique;
using SGSP.AppCode;

namespace SGSP.Formes
{
    public partial class ListeDesPersonelsFrm : Form
    {
        public ListeDesPersonelsFrm()
        {
            InitializeComponent();
        }

        private void groupBox3_Paint(object sender, PaintEventArgs e)
        {
            var mGraphics = e.Graphics;
            var pen1 = new Pen(Color.White, 1);
            var area1 = new Rectangle(0, 0, this.groupBox3.Width - 1, this.groupBox3.Height - 1);
            var linearGradientBrush = new LinearGradientBrush(area1,
                SystemColors.Control, SystemColors.Control, LinearGradientMode.Horizontal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox5_Paint(object sender, PaintEventArgs e)
        {
            var mGraphics = e.Graphics;
            var pen1 = new Pen(Color.FromArgb(255, 255, 128), 2);
            var area1 = new Rectangle(0, 0, this.groupBox5.Width - 1, this.groupBox5.Height - 1);
            var linearGradientBrush = new LinearGradientBrush(area1,
                Color.FromArgb(255, 255, 128), Color.FromArgb(255, 255, 128), LinearGradientMode.Horizontal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            var mGraphics = e.Graphics;
            var pen1 = new Pen(Color.White, 2);
            var area1 = new Rectangle(0, 0, this.groupBox2.Width - 1, this.groupBox2.Height - 1);
            var linearGradientBrush = new LinearGradientBrush(area1,
                SystemColors.Control, SystemColors.Control, LinearGradientMode.Horizontal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void button1_Click(object sender, EventArgs e)
        {

            var frm = new PersonnelFrm();

            frm.btnAjouter.Enabled = true;
            frm.ShowDialog();
            ListeDesPersonnels();
        }
        public int indexEtat;
        public string state;
        private void btnModifierPersonnel_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    if (string.IsNullOrWhiteSpace(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()))
                    {
                        return;
                    }
                    PersonnelFrm.numeroMatricule = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    PersonnelFrm.etat = "2";
                    if (PersonnelFrm.ShowBox())
                    {
                        textBox1.Text = PersonnelFrm.nom;
                        ListeDesPersonnels();
                    }
                }
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("Liste personnel", ex);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    if (string.IsNullOrWhiteSpace(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()))
                    {
                        return;
                    }
                    if (MonMessageBox.ShowBox("Voulez vous supprimer les données de  " + dataGridView1.SelectedRows[0].Cells[1].Value.ToString() + "?", "Demande confirmation") == "1")
                    {
                        ConnectionClass.SupprimerUnPersonnel(dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
                        dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);
                        //ListeDesPersonnels();
                    }
                }
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("Liste personnel", ex);
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    if (string.IsNullOrWhiteSpace(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()))
                    {
                        return;
                    }
                    var frmPersonnel = new InformationPersonnelFrm();
                    var numeroMatricule = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    var personnels = ConnectionClass.ListePersonnelParMatricule(numeroMatricule);
                    var services = ConnectionClass.ListeServicePersonnel(numeroMatricule);
                    var salaire = ConnectionClass.ListeSalaire(numeroMatricule);
                    frmPersonnel.lblMatricule.Text = numeroMatricule;
                    frmPersonnel.lblNom.Text = personnels[0].Nom;
                    frmPersonnel.lblPrenom.Text = personnels[0].Prenom;
                    var datenaissance = personnels[0].DateNaissance;
                    frmPersonnel.lblNaissance.Text = datenaissance.ToShortDateString();
                    frmPersonnel.lblLieuNaissance.Text = personnels[0].LieuNaissance;
                    frmPersonnel.lblAdresse.Text = personnels[0].Adresse;
                    frmPersonnel.lblTelephone.Text = personnels[0].Telephone1 + "/" + personnels[0].Telephone2;
                    frmPersonnel.lblEmail.Text = personnels[0].Email;
                    frmPersonnel.lblEchelon.Text = services[0].Echelon;
                    frmPersonnel.lblDateService.Text = services[0].DateService.ToShortDateString();
                    frmPersonnel.lblPoste.Text = services[0].Poste;
                    var salaireBase = salaire[0].SalaireBase;
                    

                    frmPersonnel.lblSalaire.Text = salaire[0].SalaireBase.ToString();
                    frmPersonnel.lblCategorie.Text = services[0].Categorie;
                    frmPersonnel.lblTypeContrat.Text = services[0].TypeContrat;
                    //frmPersonnel.lblService.Text = grille.ToString();
                    frmPersonnel.lblAnciennete.Text = services[0].Anciennete;
                    frmPersonnel.lblNoCNPS.Text = services[0].NoCNPS;
                    frmPersonnel.lblDiplome.Text = services[0].Diplome;

                    var anneeActuel = DateTime.Now.Year;
                    var moisActuel = DateTime.Now.Month;
                    var mois = datenaissance.Month;
                    var age = new int();
                    if (moisActuel >= mois)
                    {
                        age = anneeActuel - datenaissance.Year;
                    }
                    else
                    {
                        age = anneeActuel - datenaissance.Year - 1;
                    }
                   frmPersonnel.lblEtat.Text = services[0].Status;
                    if (AppCode.ConnectionClass.StatusContrat(services[0].Status))
                    {
                     
                        frmPersonnel.lblEtat.ForeColor = Color.Red;
                    }
                    else
                    {
                        frmPersonnel.lblEtat.ForeColor = Color.GreenYellow;
                    }
                
                    frmPersonnel.lblAge.Text = age.ToString();
                    
                    var image = personnels[0].Photo;

                    if (System.IO.File.Exists(image))
                    {
                        frmPersonnel.pictureBox2.Image = Image.FromFile(image);
                    }
                    else
                    {
                        frmPersonnel.pictureBox2.Image = null;
                    }
                    frmPersonnel.lblSexe.Text = personnels[0].Sexe;

                    var direction = from dir in ConnectionClass.ListeDirection()
                                    join div in ConnectionClass.ListeDivision()
                                    on dir.IDDirection equals div.IDDirection
                                    where div.IDDivision == services[0].IDDivision
                                    select dir;
                    foreach (var dir in direction)
                    {
                        frmPersonnel.lblDirection.Text = dir.Direction;
                    }
                    var division = from div in ConnectionClass.ListeDivision()
                                   where div.IDDivision == services[0].IDDivision
                                   select div;
                    foreach (var div in division)
                    {
                        frmPersonnel.lblDivision.Text = div.Division;
                    }
                    var departement = ConnectionClass.ListeDepartement(services[0].IDDepartement);
                    if (departement.Count > 0)
                    {
                        frmPersonnel.lblService.Text = departement[0].Departement;
                    }
                    frmPersonnel.lblGrade.Text = services[0].Grade;
                    frmPersonnel.lblNoPiece.Text = personnels[0].NumeroPiece;
                    frmPersonnel.lblTypePiece.Text = personnels[0].TypePiece;
                    frmPersonnel.lblLocalisation.Text = services[0].Localite;
                    frmPersonnel.lblFraisComm.Text = salaire[0].FraisCommunication.ToString();
                    frmPersonnel.lblAutresPrimes.Text = salaire[0].AutresPrimes.ToString();
                    frmPersonnel.lblIndemnite.Text= salaire[0].Indemnites.ToString();
                    frmPersonnel.lblPrimeTransport .Text= salaire[0].PrimeTransport.ToString();
                    frmPersonnel.lblPrimeMotivation.Text = salaire[0].PrimeMotivation.ToString();
                    frmPersonnel.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Information formation", ex);
            }
        }

        private Bitmap document;
        string titreImpression;
        //liste des personnels
        private void ListeDesPersonnels()
        {
            try
            {
                dataGridView1.Rows.Clear();
                var direction = ConnectionClass.ListeDirection();
                var j = 0;
                var rang = 1;
                var countFeminin = 0;
                var countMasculin = 0;
                foreach (var di in direction)
                {var count=0;
                    if(state=="0")
                        count = AppCode.ConnectionClass.ListePersonnel(textBox1.Text, di.Direction).Where(s => s.SituationMatrimoniale.ToUpper() == "EN COURS").Count();
                    else
                        count = AppCode.ConnectionClass.ListePersonnel(textBox1.Text, di.Direction).Where(s => s.SituationMatrimoniale.ToUpper() != "EN COURS").Count();
                    if (count > 0)
                    {
                        dataGridView1.Rows.Add(
                                   "", di.Direction.ToUpper(), "", "", "", "", "", "", "", "", "",""
                                   );
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightGray;
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                        var dd = from p in ConnectionClass.ListeDivision()
                                 where p.IDDirection == di.IDDirection
                                 select p;
                        foreach (var d in dd)
                        {
                            var listePersonnel = from p in ConnectionClass.ListePersonnel(textBox1.Text)
                                                 join s in ConnectionClass.ListeServicePersonnel()
                                                 on p.NumeroMatricule equals s.NumeroMatricule
                                                 join dep in ConnectionClass.ListeDivision()
                                                 on s.IDDivision equals dep.IDDivision
                                                 where dep.Division == d.Division
                                                 orderby p.NumeroMatricule
                                                 select new
                                                 {
                                                     p.NumeroMatricule,
                                                     p.Nom,
                                                     p.Prenom,
                                                     s.Poste,
                                                     s.Categorie,
                                                     s.Echelon,
                                                     s.Anciennete,
                                                     s.TypeContrat,
                                                     p.Sexe,
                                                     EtatRetraite = s.Status,
                                                     DateFinContrat = s.DateDepart,
                                                     s.DateService,
                                                     dep,s.Grade
                                                 };
                            if (listePersonnel.Count() > 0)
                            {
                                dataGridView1.Rows.Add(
                                     "", d.Division, "", "", "", "", "", "", "", "", "",""
                                     );
                                dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = SystemColors.Control;
                                dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                                if (state == "0")
                                {
                                    listePersonnel = listePersonnel.Where(p => p.EtatRetraite == "En cours");
                                }
                                else
                                {
                                    listePersonnel = listePersonnel.Where(p => p.EtatRetraite != "En cours");
                                }
                                foreach (var p in listePersonnel)
                                {
                                    var SalairePersonnel = ConnectionClass.ListeSalairePersonnel(p.NumeroMatricule);

                                    dataGridView1.Rows.Add(
                                        p.NumeroMatricule, p.Nom + " " + p.Prenom,p.Sexe, p.Poste,p.Grade, p.TypeContrat, p.Categorie, p.Echelon, p.Anciennete,
                                      p.DateService.ToShortDateString(),
                                       p.DateFinContrat.ToShortDateString(), p.EtatRetraite, p.EtatRetraite,rang
                                        );
                                    if (p.Poste.ToUpper().Contains("CHEF") || p.Poste.ToUpper().Contains("DIRECT"))
                                    {
                                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                                    }
                                    if (ConnectionClass.StatusContrat(p.EtatRetraite) || p.DateFinContrat <= DateTime.Now.Date)
                                    {
                                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.Red;
                                    }
                                    j++;
                                    rang++;
                                    if (p.Sexe == "F")
                                    {
                                        countFeminin += 1;
                                    }
                                    else if (p.Sexe == "M")
                                    {
                                        countMasculin += 1;
                                    }
                                }
                            }
                        }
                    }
                }
                lblNombre.Text = j.ToString();
                lblEleveFemele.Text = countFeminin.ToString();
                lblEleveMale.Text = countMasculin.ToString();
                lblPourcentFeminin.Text = Math.Round((double)countFeminin * 100 / j) + "%";
                lblMalePourcent.Text = Math.Round((double)countMasculin * 100 / j) + "%";
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("t", ex);
            }
        }
        private void ListeDesPersonnelsParMatricule()
        {
            try
            {
                dataGridView1.Rows.Clear();
                var direction = ConnectionClass.ListeDirection();
                var j = 0;
                var rang = 1;
                var countFeminin = 0;
                var countMasculin = 0;
                foreach (var di in direction)
                {
                    var count = AppCode.ConnectionClass.ListePersonnelParMat(GestionAcademique.LoginFrm.matricule, di.Direction).Count();
                    if (count > 0)
                    {
                        dataGridView1.Rows.Add(
                                   "", di.Direction.ToUpper(), "", "", "", "", "", "", "", "", "", ""
                                   );
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightGray;
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                        var dd = from p in ConnectionClass.ListeDivision()
                                 where p.IDDirection == di.IDDirection
                                 select p;
                        foreach (var d in dd)
                        {
                            var listePersonnel = from p in ConnectionClass.ListePersonnel(textBox1.Text)
                                                 join s in ConnectionClass.ListeServicePersonnel()
                                                 on p.NumeroMatricule equals s.NumeroMatricule
                                                 join dep in ConnectionClass.ListeDivision()
                                                 on s.IDDivision equals dep.IDDivision
                                                 where dep.Division == d.Division
                                                 orderby p.NumeroMatricule
                                                 select new
                                                 {
                                                     p.NumeroMatricule,
                                                     p.Nom,
                                                     p.Prenom,
                                                     s.Poste,
                                                     s.Categorie,
                                                     s.Echelon,
                                                     s.Anciennete,
                                                     s.TypeContrat,
                                                     p.Sexe,
                                                     EtatRetraite = s.Status,
                                                     DateFinContrat = s.DateDepart,
                                                     s.DateService,
                                                     dep
                                                 };
                            if (listePersonnel.Count() > 0)
                            {
                                //dataGridView1.Rows.Add(
                                //     "", d.Division, "", "", "", "", "", "", "", "", "", ""
                                //     );
                                //////dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = SystemColors.Control;
                                //////dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                                foreach (var p in listePersonnel)
                                {
                                    if (p.NumeroMatricule == GestionAcademique.LoginFrm.matricule)
                                    {
                                        var SalairePersonnel = ConnectionClass.ListeSalairePersonnel(p.NumeroMatricule);

                                        dataGridView1.Rows.Add(
                                            p.NumeroMatricule, p.Nom + " " + p.Prenom, p.Sexe, p.Poste, p.TypeContrat, p.Categorie, p.Echelon, p.Anciennete,
                                          p.DateService.ToShortDateString(),
                                           p.DateFinContrat.ToShortDateString(), SalairePersonnel[0].SalaireBase, rang
                                            );
                                        if (p.Poste.ToUpper().Contains("CHEF") || p.Poste.ToUpper().Contains("DIRECT"))
                                        {
                                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                                        }
                                        if (ConnectionClass.StatusContrat(p.EtatRetraite) || p.DateFinContrat <= DateTime.Now.Date)
                                        {
                                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.Red;
                                        }
                                        j++;
                                        rang++;
                                        if (p.Sexe == "F")
                                        {
                                            countFeminin += 1;
                                        }
                                        else if (p.Sexe == "M")
                                        {
                                            countMasculin += 1;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                lblNombre.Text = j.ToString();
                lblEleveFemele.Text = countFeminin.ToString();
                lblEleveMale.Text = countMasculin.ToString();
                lblPourcentFeminin.Text = Math.Round((double)countFeminin * 100 / j) + "%";
                lblMalePourcent.Text = Math.Round((double)countMasculin * 100 / j) + "%";
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("t", ex);
            }
        }


        private void btnIImprimante_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count > 0)
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

                            var pathFolder = "C:\\Dossier Personnel\\Liste Personnel";
                            if (!System.IO.Directory.Exists(pathFolder))
                            {
                                System.IO.Directory.CreateDirectory(pathFolder);
                            }
                            sfd.InitialDirectory = pathFolder;
                            sfd.FileName = titreImpression.Replace("/", "_") + "_" + date;
                            sfd.FileName = sfd.FileName.Replace(">>", "_") + "_" + date;
                            sfd.FileName = sfd.FileName.Replace("<<", "_") + "_" + date + ".pdf";

                            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            {
                                if (checkBox1.Checked)
                                {

                                    var div = dataGridView1.Rows.Count / 28;
                                    for (var i = 0; i <= div; i++)
                                    {
                                        document = Impression.ImprimerLalisteDesPersonnelsAvecSalaire(dataGridView1, titreImpression, i, lblNombre.Text);
                                        sharpPDF.pdfPage pageIndex = pdfDocument.addPage(500, 700);
                                        var inputImage = @"cdali" + i;
                                        pdfDocument.addImageReference(document, inputImage);
                                        sharpPDF.Elements.pdfImageReference img1 = pdfDocument.getImageReference(inputImage);
                                        pageIndex.addImage(img1, 0, 0, pageIndex.height, pageIndex.width);
                                    }
                                }
                                else if (checkBox2.Checked)
                                {
                                    var div = dataGridView1.Rows.Count / 44;
                                    for (var i = 0; i <= div; i++)
                                    {
                                        document = Impression.ImprimerLalisteDesPersonnelsInformationBanciaire(dataGridView1, i);
                                        sharpPDF.pdfPage pageIndex = pdfDocument.addPage();
                                        var inputImage = @"cdali" + i;
                                        pdfDocument.addImageReference(document, inputImage);
                                        sharpPDF.Elements.pdfImageReference img1 = pdfDocument.getImageReference(inputImage);
                                        pageIndex.addImage(img1, 0, 0, pageIndex.height, pageIndex.width);
                                    }
                                }
                                else
                                {
                                    var div = dataGridView1.Rows.Count / 44;
                                    for (var i = 0; i <= div; i++)
                                    {
                                        document = Impression.ImprimerLalisteDesPersonnels(dataGridView1, titreImpression, i, lblNombre.Text);
                                        sharpPDF.pdfPage pageIndex = pdfDocument.addPage(500, 700);
                                        var inputImage = @"cdali" + i;
                                        pdfDocument.addImageReference(document, inputImage);
                                        sharpPDF.Elements.pdfImageReference img1 = pdfDocument.getImageReference(inputImage);
                                        pageIndex.addImage(img1, 0, 0, pageIndex.height, pageIndex.width);
                                    }
                                }
                                pdfDocument.createPDF(sfd.FileName);
                                System.Diagnostics.Process.Start(sfd.FileName);
                            }
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            var width = printDocument1.PrinterSettings.DefaultPageSettings.PaperSize.Width - 5;
            var height = printDocument1.PrinterSettings.DefaultPageSettings.PaperSize.Height;
            e.Graphics.DrawImage(document, 0, 10, width, height);
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

            try
            {
                titreImpression = "Liste general du personnel";
                ListeDesPersonnels();
            }
            catch { }
        }

        private void ListeDesPersonels_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.RowTemplate.Height = 23;
                panel1.Location = new Point(dataGridView1.Width + 15, panel1.Location.Y);
                btnFermer.Location = new Point(Width - 45, btnFermer.Location.Y);
                if (GestionAcademique.LoginFrm.typeUtilisateur == "utilisateur")
                {
                    ListeDesPersonnelsParMatricule();
                    button1.Enabled = false;
                    button4.Enabled = false;
                    button10.Enabled = false;
                    button11.Enabled = false;
                    //button6.Enabled = false;
                    button3.Enabled = false;
                    groupBox2.Enabled = false;
                    btnIImprimante.Enabled = false;
                }
                else
                {
                    cmbTypeContrat.Items.Add("<<Recherche par status contrat>>");
                    foreach (var c in AppCode.ConnectionClass.ListeContrat().Where( cn => !cn.TypeContrat.Contains("PDST") && !cn.TypeContrat.ToUpper().Contains("PROJET")))
                        cmbTypeContrat.Items.Add(c.TypeContrat);
                    cmbTypeContrat.Text = "<<Recherche par status contrat>>";

                    cmbDivision.Items.Add("<<Recherche par direction>>");
                    foreach (var c in AppCode.ConnectionClass.ListeDirection())
                        cmbDivision.Items.Add(c.Direction);
                    cmbDivision.Text = "<<Recherche par direction>>";

                    titreImpression = "Liste general du personnel";
            
                    cmbEtatContrat.Items.Add("<<Par status>>");
                    foreach (var status in ConnectionClass.StatusDesPersonnels())
                    {
                        cmbEtatContrat.Items.Add(status);
                    }
                    cmbEtatContrat.Text = "<<Par status>>";
                    ConnectionClass.MettreAJourAge();
                    ListeDesPersonnels();
                }
            }
            catch (Exception)
            {
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var frm = new  TBRHFrm();
            frm.Size = new Size(Width, Height);
            frm.Location = new Point(Location.X, Location.Y);
            frm.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    if (string.IsNullOrWhiteSpace(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()))
                    {
                        return;
                    }
                    var nomPersonnel = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                   var  rootPath = "";
                    if (GestionAcademique.LoginFrm.typeUtilisateur == "utilisateur")
                    {
                         rootPath = GlobalVariable.rootPathPersonnelTempo + nomPersonnel;
                    }else{
                         rootPath = GlobalVariable.rootPathPersonnel + nomPersonnel;
                    }
                    //if (System.IO.Directory.Exists(rootPath))
                    var open = new OpenFileDialog();
                    //open.InitialDirectory = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10);
                    open.Filter = "Tous les fichiers (all files(*.*)|*.*";
                    open.FilterIndex = 1;
                    if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        if (open.CheckFileExists)
                        {
                            var fileName = open.FileName;

                            if (!System.IO.Directory.Exists(rootPath))
                            {
                                System.IO.Directory.CreateDirectory(rootPath);
                            }
                            rootPath = rootPath + @"//" + System.IO.Path.GetFileName(fileName);
                            if (!System.IO.File.Exists(rootPath))
                            {
                                System.IO.File.Copy(fileName, rootPath);
                                MonMessageBox.ShowBox("fichier transferé avec succés ", "Information fichier");
                            }
                            else
                            {
                                if (MonMessageBox.ShowBox("Ce fichier existe deja Voulez vous faire une copie?", "confirmation") == "1")
                                {
                                    var rootPath1 = rootPath.Substring(0, rootPath.LastIndexOf(".")) + "_copie1" + rootPath.Substring(rootPath.LastIndexOf("."));
                                    if (!System.IO.File.Exists(rootPath1))
                                    {
                                        System.IO.File.Copy(fileName, rootPath1);
                                        MonMessageBox.ShowBox("fichier transferé avec succés ", "Information fichier");
                                    }
                                    else
                                    {

                                        var rootPath2 = rootPath.Substring(0, rootPath.LastIndexOf(".")) + "_copie2" + rootPath.Substring(rootPath.LastIndexOf("."));
                                        if (!System.IO.File.Exists(rootPath2))
                                        {
                                            System.IO.File.Copy(fileName, rootPath2);
                                            MonMessageBox.ShowBox("fichier transferé avec succés ", "Information fichier");
                                        }
                                        else
                                        {
                                            var rootPath3 = rootPath.Substring(0, rootPath.LastIndexOf(".")) + "_copie3" + rootPath.Substring(rootPath.LastIndexOf("."));
                                            if (!System.IO.File.Exists(rootPath3))
                                            {
                                                System.IO.File.Copy(fileName, rootPath3);
                                                MonMessageBox.ShowBox("fichier transferé avec succés ", "Information fichier");
                                            }
                                            else
                                            {
                                                var rootPath4 = rootPath.Substring(0, rootPath.LastIndexOf(".")) + "_copie4" + rootPath.Substring(rootPath.LastIndexOf("."));
                                                if (!System.IO.File.Exists(rootPath4))
                                                {
                                                    System.IO.File.Copy(fileName, rootPath4);
                                                    MonMessageBox.ShowBox("fichier transferé avec succés ", "Information fichier");
                                                }
                                            }
                                        }
                                    }

                                }
                            }


                        }

                    }
                }
                else
                {
                    MonMessageBox.ShowBox("Veuillez selectionner les données du personnel. Puis réessayez ", "Information Photo");
                }
            }

            catch (Exception ex) { MonMessageBox.ShowBox("Transfert de l'image", ex); }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    if (string.IsNullOrWhiteSpace(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()))
                    {
                        return;
                    }
                    var open = new OpenFileDialog();
                    //open.InitialDirectory = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10);
                    open.Filter = "Image Files (*.jpg)|*.jpg|all files(*.*)|*.*";
                    open.FilterIndex = 1;
                    if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        if (open.CheckFileExists)
                        {
                            var _photo = System.IO.Path.GetFileName(open.FileName);
                            var imagePath = GlobalVariable.rootPathPersonnel +
                                 dataGridView1.SelectedRows[0].Cells[1].Value.ToString() + "//";
                            if (!System.IO.Directory.Exists(imagePath))
                            {
                                System.IO.Directory.CreateDirectory(imagePath);
                            }
                            imagePath = imagePath + _photo;
                            if (!System.IO.File.Exists(imagePath))
                            {
                                System.IO.File.Copy(open.FileName, imagePath);
                                //pictureBox1.Image = Image.FromFile(open.FileName);
                                ConnectionClass.InsereImage(dataGridView1.SelectedRows[0].Cells[0].Value.ToString(), imagePath);
                                MonMessageBox.ShowBox("Photo transferée avec succés ", "Information Photo");
                            }
                            else
                            {
                                if (MonMessageBox.ShowBox("Ce fichier existe deja Voulez vous faire une copie?", "confirmation") == "1")
                                {
                                    var rootPath1 = imagePath.Substring(0, imagePath.LastIndexOf(".")) + "_copie1" + imagePath.Substring(imagePath.LastIndexOf("."));
                                    if (!System.IO.File.Exists(rootPath1))
                                    {
                                        System.IO.File.Copy(open.FileName, rootPath1);
                                        MonMessageBox.ShowBox("fichier transferé avec succés ", "Information Photo");
                                    }
                                    else
                                    {

                                        var rootPath2 = imagePath.Substring(0, imagePath.LastIndexOf(".")) + "_copie2" + imagePath.Substring(imagePath.LastIndexOf("."));
                                        if (!System.IO.File.Exists(rootPath2))
                                        {
                                            System.IO.File.Copy(open.FileName, rootPath2);
                                            MonMessageBox.ShowBox("fichier transferé avec succés ", "Information Photo");
                                        }
                                        else
                                        {
                                            var rootPath3 = imagePath.Substring(0, imagePath.LastIndexOf(".")) + "_copie3" + imagePath.Substring(imagePath.LastIndexOf("."));
                                            if (!System.IO.File.Exists(rootPath3))
                                            {
                                                System.IO.File.Copy(open.FileName, rootPath3);
                                                MonMessageBox.ShowBox("fichier transferé avec succés ", "Information Photo");
                                            }
                                            else
                                            {
                                                var rootPath4 = imagePath.Substring(0, imagePath.LastIndexOf(".")) + "_copie4" + imagePath.Substring(imagePath.LastIndexOf("."));
                                                if (!System.IO.File.Exists(rootPath4))
                                                {
                                                    System.IO.File.Copy(open.FileName, rootPath4);
                                                    MonMessageBox.ShowBox("fichier transferé avec succés ", "Information Photo");
                                                }
                                            }
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
                else
                {
                    MonMessageBox.ShowBox("Veuillez selectionner les données du personnel. Puis réessayez ", "Information Photo");
                }
            }

            catch (Exception ex) { MonMessageBox.ShowBox("Transfert de l'image", ex); }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ListeDesPersonnels();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (GestionAcademique.LoginFrm.typeUtilisateur != "util")
             
                    btnModifierPersonnel_Click(null, null);
                
        }
        private void cmbTypeContrat_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                var typeContrat = cmbTypeContrat.Text;
                if (cmbTypeContrat.Text == "<<Recherche par status contrat>>")
                    typeContrat = "";
                var direction = ConnectionClass.ListeDirection();
                var j = 0;
                var rang = 1;
                var countFeminin = 0;
                var countMasculin = 0;
                foreach (var di in direction)
                {
                    var count = 0;//  AppCode.ConnectionClass.ListePersonnel(textBox1.Text, di.Direction, cmbTypeContrat.Text).Count(); var count = 0;
                    if (state == "0")
                        count = AppCode.ConnectionClass.ListePersonnel(textBox1.Text, di.Direction, cmbTypeContrat.Text).Where(s => s.SituationMatrimoniale.ToUpper() == "EN COURS").Count();
                    else
                        count = AppCode.ConnectionClass.ListePersonnel(textBox1.Text, di.Direction, cmbTypeContrat.Text).Where(s => s.SituationMatrimoniale.ToUpper() != "EN COURS").Count();
                
                    if (count > 0)
                    {
                        dataGridView1.Rows.Add(
                                   "", di.Direction.ToUpper(), "", "", "", "", "", "", "", "", "",""
                                   );
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightGray;
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                        var dd = from p in ConnectionClass.ListeDivision()
                                 where p.IDDirection == di.IDDirection
                                 select p;
                        foreach (var d in dd)
                        {
                            var listePersonnel = from p in ConnectionClass.ListePersonnel(textBox1.Text)
                                                 join s in ConnectionClass.ListeServicePersonnel()
                                                 on p.NumeroMatricule equals s.NumeroMatricule
                                                 join dep in ConnectionClass.ListeDivision()
                                                 on s.IDDivision equals dep.IDDivision
                                                 where dep.Division == d.Division
                                                 where s.TypeContrat.StartsWith(typeContrat, StringComparison.CurrentCultureIgnoreCase)
                                                 orderby p.NumeroMatricule
                                                 select new
                                                 {
                                                     p.NumeroMatricule,
                                                     p.Nom,
                                                     p.Prenom,
                                                     s.Poste,
                                                     s.Categorie,
                                                     s.Echelon,
                                                     s.Anciennete,
                                                     s.TypeContrat,
                                                     p.Sexe,
                                                     EtatRetraite = s.Status,
                                                     DateFinContrat = s.DateDepart,
                                                     s.DateService,
                                                     dep,s.Grade
                                                 };
                            if (listePersonnel.Count() > 0)
                            {
                                dataGridView1.Rows.Add(
                                     "", d.Division, "", "", "", "", "", "", "", "", "",""
                                     );
                                dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = SystemColors.Control;
                                dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                                if (state == "0")
                                {
                                    listePersonnel = listePersonnel.Where(p => p.EtatRetraite == "En cours");
                                }
                                else
                                {
                                    listePersonnel = listePersonnel.Where(p => p.EtatRetraite != "En cours");
                                } 
                                foreach (var p in listePersonnel)
                                {
                                    var SalairePersonnel = ConnectionClass.ListeSalairePersonnel(p.NumeroMatricule);

                                    dataGridView1.Rows.Add(
                                    p.NumeroMatricule, p.Nom + " " + p.Prenom, p.Sexe, p.Poste, p.Grade, p.TypeContrat, p.Categorie, p.Echelon, p.Anciennete,
                                  p.DateService.ToShortDateString(),
                                   p.DateFinContrat.ToShortDateString(), p.EtatRetraite, p.EtatRetraite, rang
                                    );
                                    if (p.Poste.ToUpper().Contains("CHEF") || p.Poste.ToUpper().Contains("DIRECT"))
                                    {
                                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);

                                    }

                                    if (ConnectionClass.StatusContrat(p.EtatRetraite) || p.DateFinContrat <= DateTime.Now.Date)
                                    {
                                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.Red;
                                    }
                                    j++;
                                    rang++;
                                    if (p.Sexe == "F")
                                    {
                                        countFeminin += 1;
                                    }
                                    else if (p.Sexe == "M")
                                    {
                                        countMasculin += 1;
                                    }

                                }
                            }
                        }
                    }
                }
                titreImpression = "Liste personnel " + cmbTypeContrat.Text.ToUpper();
                if (cmbTypeContrat.Text == "<<Recherche par status contrat>>")
                    titreImpression = "Liste personnel";
                lblNombre.Text = j.ToString();
                lblEleveFemele.Text = countFeminin.ToString();
                lblEleveMale.Text = countMasculin.ToString();
                lblPourcentFeminin.Text = Math.Round((double)countFeminin * 100 / j) + "%";
                lblMalePourcent.Text = Math.Round((double)countMasculin * 100 / j) + "%";
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("t", ex);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (MonMessageBox.ShowBox("Ce dossier pourrait comporter des fichiers. Voulez vous vraiment supprimer ?", "Confirmation") == "1")
                {
                    var nomPersonnel = dataGridView1.SelectedRows[0].Cells[1].Value.ToString() + " "
                                       + dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
                    var rootPath = GlobalVariable.rootPathPersonnel;
                    rootPath = rootPath + nomPersonnel;
                    if (System.IO.Directory.Exists(rootPath))
                    {
                        System.IO.Directory.Delete(rootPath);
                        MonMessageBox.ShowBox("Dossier du personnel " + nomPersonnel + " supprimée avec succés", "Inforation");
                    }
                }
            }
            catch (Exception ex)
            { }
        }

        private void ToCsV(DataGridView dGV, string filename)
        {
            try
            {
                string stOutput = "";
                // Export titles:
                string sHeaders = "";

                for (int j = 1; j < dGV.Columns.Count - 3; j++)
                    sHeaders = sHeaders.ToString() + Convert.ToString(dGV.Columns[j].HeaderText) + "\t";
                stOutput += sHeaders + "\r\n";
                // Export data.
                for (int i = 0; i < dGV.RowCount; i++)
                {
                    string stLine = "";
                    for (int j = 1; j < dGV.Rows[i].Cells.Count - 3; j++)
                        stLine = stLine.ToString() + Convert.ToString(dGV.Rows[i].Cells[j].Value) + "\t";
                    stOutput += stLine + "\r\n";
                }
                Encoding utf16 = Encoding.GetEncoding(1254);
                byte[] output = utf16.GetBytes(stOutput);
                System.IO.FileStream fs = new System.IO.FileStream(filename, System.IO.FileMode.Create);
                System.IO.BinaryWriter bw = new System.IO.BinaryWriter(fs);
                bw.Write(output, 0, output.Length); //write the encoded file
                bw.Flush();
                bw.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("Erreur exportation", ex);
            }
        }

        private void button10_Click(object sender, EventArgs e)
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

                sfd.FileName = "Liste personnel " + date + ".xls";
                if (sfd.ShowDialog() == DialogResult.OK)
                {

                    ToCsV(dataGridView1, sfd.FileName); // Here dataGridview1 is your grid view name
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("", ex);
            }
        }


        private void cmbEtatContrat_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                var direction = ConnectionClass.ListeDirection();
                var j = 0;
                var rang = 1;
                var countFeminin = 0;
                var countMasculin = 0;
                foreach (var di in direction)
                {
                    var count = AppCode.ConnectionClass.ListePersonnelParStatus(textBox1.Text, cmbEtatContrat.Text,di.Direction).Count();
                    if (count > 0)
                    {
                        dataGridView1.Rows.Add(
                                   "", di.Direction.ToUpper(), "", "", "", "", "", "", "", "", "",""
                                   );
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightGray;
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                        var dd = from p in ConnectionClass.ListeDivision()
                                 where p.IDDirection == di.IDDirection
                                 select p;
                        foreach (var d in dd)
                        {
                            var listePersonnel = from p in ConnectionClass.ListePersonnel(textBox1.Text)
                                                 join s in ConnectionClass.ListeServicePersonnel()
                                                 on p.NumeroMatricule equals s.NumeroMatricule
                                                 join dep in ConnectionClass.ListeDivision()
                                                 on s.IDDivision equals dep.IDDivision
                                                 where dep.Division == d.Division
                                                 where s.Status.StartsWith(cmbEtatContrat.Text, StringComparison.CurrentCultureIgnoreCase)
                                                 orderby p.NumeroMatricule
                                                 select new
                                                 {
                                                     p.NumeroMatricule,
                                                     p.Nom,
                                                     p.Prenom,
                                                     s.Poste,
                                                     s.Categorie,
                                                     s.Echelon,
                                                     s.Anciennete,
                                                     s.TypeContrat,
                                                     p.Sexe,
                                                     EtatRetraite = s.Status,
                                                     DateFinContrat = s.DateDepart,
                                                     s.DateService,
                                                     dep
                                                 };
                            if (listePersonnel.Count() > 0)
                            {
                                dataGridView1.Rows.Add(
                                     "", d.Division, "", "", "", "", "", "", "", "", "",""
                                     );
                                dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = SystemColors.Control;
                                dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                                foreach (var p in listePersonnel)
                                {
                                    var SalairePersonnel = ConnectionClass.ListeSalairePersonnel(p.NumeroMatricule);

                                    dataGridView1.Rows.Add(
                                        p.NumeroMatricule, p.Nom + " " + p.Prenom,p.Sexe, p.Poste, p.TypeContrat, p.Categorie, p.Echelon, p.Anciennete,
                                      p.DateService.ToShortDateString(),
                                       p.DateFinContrat.ToShortDateString(), SalairePersonnel[0].SalaireBase, rang
                                        );
                                    if (p.Poste.ToUpper().Contains("CHEF") || p.Poste.ToUpper().Contains("DIRECT"))
                                    {
                                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                                    }
                                    if (ConnectionClass.StatusContrat(p.EtatRetraite) || p.DateFinContrat <=DateTime.Now.Date)
                                    {
                                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.Red;
                                    }
                                    j++; rang++;
                                    if (p.Sexe == "F")
                                    {
                                        countFeminin += 1;
                                    }
                                    else if (p.Sexe == "M")
                                    {
                                        countMasculin += 1;
                                    }
                                }
                            }

                        }
                    }
                }
                lblNombre.Text = j.ToString();
                lblEleveFemele.Text = countFeminin.ToString();
                lblEleveMale.Text = countMasculin.ToString();
                lblPourcentFeminin.Text = Math.Round((double)countFeminin * 100 / j) + "%";
                lblMalePourcent.Text = Math.Round((double)countMasculin * 100 / j) + "%";
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("t", ex);
            }
        }
        private void cmbDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Rows.Clear();
                var direction = ConnectionClass.ListeDirection();
                var j = 0;
                var rang = 1;
                var countFeminin = 0;
                var countMasculin = 0;

                foreach (var di in direction)
                {
                    if (di.Direction.ToUpper() == cmbDivision.Text.ToUpper())
                    {
                        //var count = AppCode.ConnectionClass.ListePersonnel(textBox1.Text, di.Direction).Count();
                        //if(count>0)
                        {
                            dataGridView1.Rows.Add(
                                "", di.Direction.ToUpper(), "", "", "", "", "", "", "", "", "",""
                                       );
                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = Color.LightGray;
                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                            var dd = from p in ConnectionClass.ListeDivision()
                                     where p.IDDirection == di.IDDirection

                                     select p;
                            foreach (var d in dd)
                            {
                                var listePersonnel = from p in ConnectionClass.ListePersonnel(textBox1.Text)
                                                     join s in ConnectionClass.ListeServicePersonnel()
                                                     on p.NumeroMatricule equals s.NumeroMatricule
                                                     join dep in ConnectionClass.ListeDivision()
                                                     on s.IDDivision equals dep.IDDivision
                                                     where dep.Division == d.Division
                                                     orderby p.NumeroMatricule
                                                     select new
                                                     {
                                                         p.NumeroMatricule,
                                                         p.Nom,
                                                         p.Prenom,
                                                         s.Poste,
                                                         s.Categorie,
                                                         s.Echelon,
                                                         s.Anciennete,
                                                         s.TypeContrat,
                                                         p.Sexe,
                                                         Status = s.Status,
                                                         DateFinContrat = s.DateDepart,
                                                         s.DateService,
                                                         dep,
                                                         s.Grade
                                                     };
                                if (listePersonnel.Count() > 0)
                                {
                                    dataGridView1.Rows.Add(
                                         "", d.Division, "", "", "", "", "", "", "", "", "",""
                                         );
                                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.BackColor = SystemColors.Control;
                                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                                    if (state == "0")
                                    {
                                        listePersonnel = listePersonnel.Where(p => p.Status == "En cours");
                                    }
                                    else
                                    {
                                        listePersonnel = listePersonnel.Where(p => p.Status != "En cours");
                                    }
                                    foreach (var p in listePersonnel)
                                    {
                                        var SalairePersonnel = ConnectionClass.ListeSalairePersonnel(p.NumeroMatricule);

                                        dataGridView1.Rows.Add(
                                        p.NumeroMatricule, p.Nom + " " + p.Prenom, p.Sexe, p.Poste,p.Grade, p.TypeContrat, p.Categorie, p.Echelon, p.Anciennete,
                                      p.DateService.ToShortDateString(),
                                       p.DateFinContrat.ToShortDateString(), p.Status, p.Status, rang
                                        );
                                        if (p.Poste.ToUpper().Contains("CHEF") || p.Poste.ToUpper().Contains("DIRECT"))
                                        {
                                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                                        }
                                        if (ConnectionClass.StatusContrat(p.Status) || p.DateFinContrat <= DateTime.Now.Date)
                                        {
                                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold);
                                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.Red;
                                        }
                                        j++;
                                        rang++;
                                        if (p.Sexe == "F")
                                        {
                                            countFeminin += 1;
                                        }
                                        else if (p.Sexe == "M")
                                        {
                                            countMasculin += 1;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                titreImpression = "Liste personnel " +cmbDivision.Text;
                if (cmbDivision.Text == "<<Recherche par direction>>")
                    titreImpression = "Liste personnel ";
                lblNombre.Text = j.ToString();
                lblEleveFemele.Text = countFeminin.ToString();
                lblEleveMale.Text = countMasculin.ToString();
                lblPourcentFeminin.Text = Math.Round((double)countFeminin * 100 / j) + "%";
                lblMalePourcent.Text = Math.Round((double)countMasculin * 100 / j) + "%";
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("t", ex);
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    if (string.IsNullOrWhiteSpace(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()))
                    {
                        return;
                    }
                    var frm = new DossierForm();
                    frm.Size = new Size(Width, Height);
                    frm.Location = new Point(Location.X, Location.Y);
                    frm.nomPersonnel = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                    frm.state = "1";
                    frm.stateTemp = "1";
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("creation du dossier", ex);
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    if (string.IsNullOrWhiteSpace(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()))
                    {
                        return;
                    }
                    var frm = new DossierForm();
                    frm.Size = new Size(Width, Height);
                    frm.Location = new Point(Location.X, Location.Y);
                    frm.nomPersonnel = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                    frm.state = "1";
                    frm.stateTemp = "2";
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("creation du dossier", ex);
            }
        }

        private void button9_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                button10.ContextMenuStrip = contextMenuStrip1;
                button10.ContextMenuStrip.Show(button10, e.Location);
            }
        }

    }
}
