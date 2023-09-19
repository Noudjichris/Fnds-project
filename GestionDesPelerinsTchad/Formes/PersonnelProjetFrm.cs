using System;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using GestionPharmacetique;
using SGSP.AppCode;

namespace GestionDesPersonnelsTchad
{
    public partial class PersonnelProjetFrm : Form
    {
        public PersonnelProjetFrm()
        {
            InitializeComponent();
        }

        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.Silver, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox2.Width - 1, this.groupBox2.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.White, Color.White, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox3_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.Silver, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox3.Width - 1, this.groupBox3.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.White, Color.White, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        // les proprietes du pelerin
        #region proprietesDuPersonnel
        DateTime _dateNaissance;
        string _prenom, _nom;
        string _barcode;
        string _lieuNaissance, _sexe, anciennete, diplome, typeContrat;
        string _telephone1, _telephone2, _photo;
        int _numeroDepartement,age;
        string _division, _adresse, _email, _numeroCompte, categorie, noCNPS;
        string _echelon, _poste;
        DateTime _dateService;
        private double  salaireBrut, prime;
        public int idProjet, idPersonnel;
        #endregion

       //ajouter un personnel
        private void btnAjouter_Click(object sender, EventArgs e)
        {
            try
            {
                // mettre la validation pour la date de naissance
                if (!string.IsNullOrEmpty(cmbJour.Text) && !string.IsNullOrEmpty(cmbMois.Text) && !string.IsNullOrEmpty(txtDateNaissance.Text))
                {
                    _dateNaissance = DateTime.Parse(cmbJour.Text + "/" + cmbMois.Text + "/" + txtDateNaissance.Text);

                }
                else if (string.IsNullOrEmpty(cmbJour.Text) && string.IsNullOrEmpty(cmbMois.Text) && !string.IsNullOrEmpty(txtDateNaissance.Text))
                {
                    _dateNaissance = DateTime.Parse("1/1/" + txtDateNaissance.Text);
                }
                else
                {
                    _dateNaissance = DateTime.Now;
                    age = 0;
                    txtAge.Text = age.ToString();
                }

                //if (string.IsNullOrEmpty(txtNummatricule.Text))
                //{
                //    MonMessageBox.ShowBox("Le matricule ne doit être vide", "Erreur");
                //    return;
                //}
                //valider le champs pour le nom 
                if (string.IsNullOrEmpty(txtNom.Text))
                {
                    MonMessageBox.ShowBox("Veuillez saisir le nom ", "Erreur");
                    return;
                }

                //valider le champs pour le nom du personnel
                if (string.IsNullOrEmpty(txtPrenom.Text))
                {
                    MonMessageBox.ShowBox("Veuillez saisir le prenom ", "Erreur");
                    return;
                }

                //valider le champ pour le numero telephone
                if (string.IsNullOrEmpty(txtTelephone1.Text))
                {
                    MonMessageBox.ShowBox("Veuillez saisir le numero de telephone", "Erreur");
                    return;
                }

                //valider le champ pour l adresse

                //valider le champ pour le poste
                if (string.IsNullOrEmpty(txtPoste.Text))
                {
                    MonMessageBox.ShowBox("Veuillez saisir le poste du personnel", "Erreur");
                    return;
                }

                //valider le champ pour le departement
                //if (string.IsNullOrEmpty(cmbService.Text))
                //{
                //    MonMessageBox.ShowBox("Veuillez selectionner le service du personnel", "Erreur");
                //    return;
                //}
                //valider le case du sexe du pelerin
                if (!chkFemelle.Checked && !chkMale.Checked)
                {
                    MonMessageBox.ShowBox("Veuillez le cocher pour le sexe", "Erreur");
                    return;
                }
                else if (chkMale.Checked)
                {
                    _sexe = "M";
                }
                else if (chkFemelle.Checked)
                {
                    _sexe = "F";
                }
                
                if (double.TryParse(txtSalaire.Text, out salaireBrut) || string.IsNullOrEmpty(txtSalaire.Text))
                {

                }
                else
                {
                    MonMessageBox.ShowBox("Veuillez entrer un chiffre valide pour le salaire de l'employé", "Erreur");
                    return;
                }
                if (string.IsNullOrEmpty(txtSalaire.Text))
                {
                    salaireBrut = 0;
                }
           
                       if (double.TryParse(txtGrille.Text, out prime) || string.IsNullOrEmpty(txtGrille.Text))
                {

                }
                else
                {
                    MonMessageBox.ShowBox("Veuillez entrer un chiffre valide pour le salaire de l'employé", "Erreur");
                    return;
                }
                if (string.IsNullOrEmpty(txtGrille.Text))
                {
                    prime = 0;
                }
           
                ObtenirlesDonneesPersonnel();
                var personnel = new Personnel();
                personnel.NumeroMatricule = txtNummatricule.Text;
                personnel.Nom = _nom;
                personnel.Prenom = _prenom;
                personnel.Sexe = _sexe;
                personnel.Telephone1 = _telephone1;
                personnel.Telephone2 = _telephone2;
                personnel.LieuNaissance = _lieuNaissance;
                personnel.DateNaissance = _dateNaissance;
                personnel.Adresse = _adresse;
                personnel.Age = age;
                personnel.Email = _email;
                
                personnel.SituationMatrimoniale = "";
                personnel.IDPersonelProjet = idPersonnel;

                int nbreEnfant = 0;
                    personnel.NombreEnfant = nbreEnfant;
                var service = new Service();
                service.Anciennete = anciennete;
                service.Categorie = categorie;
                service.TypeContrat = typeContrat;
                service.DateService = _dateService;
                service.Diplome = diplome;
                service.Echelon = _echelon;
                service.Grade = cmbGrade.Text;
                service.NoCNPS = noCNPS;
                service.Poste = _poste;
                service.DateDepart = dtpFinContrat.Value;
                service.IDProjet = idProjet;
                service.SalaireBrut = salaireBrut;
                service.Primes = prime;
                service.IDDepartement = _numeroDepartement;
                service.IDPersonelProjet = idPersonnel;
                var listeDiv = from div in ConnectionClass.ListeDivision()
                               where div.Division == cmbDivision.Text
                               select div.IDDivision;
                foreach (var div in listeDiv)
                    service.IDDivision = div;
                service.Status = cmbStatus.Text;
                var bank = new Banque();
                bank.Compte = _numeroCompte;
                bank.CodeBanque = txtCodeBank.Text;
                bank.CodeGuichet = txtCodeGuichet.Text;
                bank.Cle = txtClef.Text;
                bank.NomBanque = cmbBank.Text;
             
         if (etat == "2")
                {
                    if (MonMessageBox.ShowBox("Voulez vous modifier les données de ce personnel?", "Confirmation") == "1")
                    {
                        if (ConnectionClass.ModifierUnPersonnelProjet(personnel, service,bank))
                        {
                            ViderLesChamps();
                            this.Close();
                        }
                    }
                }
               else  
                {
                    
                    if (ConnectionClass.AjouterUnPersonnelProjet(personnel, service,bank))
                    {
                        ViderLesChamps();
                        //fl = true;
                    }
                }
                
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Enregistrement personnel", ex);
            }
        }

        private void ObtenirlesDonneesPersonnel()
        {
            _numeroCompte = txtNoCompte.Text;
            _nom = txtNom.Text;
            _prenom = txtPrenom.Text;
            _telephone1 = txtTelephone1.Text;
            _telephone2  = txtTelephone2.Text;
            _lieuNaissance = txtLieuNaissance.Text;
            _adresse = txtAdresse.Text;
            _email = txtEmail.Text;
            categorie = cmbCategorie.Text;
            age = Int32.Parse(txtAge.Text);
            foreach (var dp in ConnectionClass.ListeDepartement())
            {
                if (dp.Departement == cmbService.Text)
                    _numeroDepartement = dp.IDDepartement;
                else
                    _numeroDepartement = 0;
            }
            _poste = txtPoste.Text;
            //_numeroMatricule = txtNummatricule.Text;
            anciennete = txtAnciennete.Text;
            noCNPS = txtCNPS.Text;
            diplome = cmbDiplome.Text;
            typeContrat = cmbTypeContrat.Text;
            _echelon = txtEchelon.Text;
            _dateService = dtpDateservice.Value.Date;
        }

        //vider les champs
        private void ViderLesChamps()
        {
            cmbGrade.Text = "";
            txtCodeBank.Text = "";
            txtCodeGuichet.Text = "";
            txtClef.Text = "";
            txtNoCompte.Text = "";
            txtSalaire.Text = "";
            txtGrille.Text = "";
            txtEmail.Text = "";
            txtNummatricule.Text = "";
            txtPrenom.Text = "";
            txtTelephone1.Text = "";
            txtNom.Text = "";
            txtLieuNaissance.Text = "";
            txtDateNaissance.Text = "";
            cmbCategorie.Text = "";
            txtAnciennete.Text = "";
            chkFemelle.Checked = false;
            chkMale.Checked = false;
            btnAjouter.Enabled = true;
            txtNummatricule.Focus();
            txtAge.Text = "";
            txtCNPS.Text = "";
            txtAnciennete.Text = "";
            cmbDiplome.Text = "";
            txtAdresse.Text = "";
            txtTelephone2.Text = "";
            txtTelephone1.Text = "";
            txtPoste.Text = "";
            txtEchelon.Text = "";
        }
        //nouveau personnel
        private void button1_Click(object sender, EventArgs e)
        {
          ViderLesChamps();
        }

        //quitter la forme
        private void btnFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }
     
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtNummatricule.Text))
                {
                    var  open = new OpenFileDialog();
                    //open.InitialDirectory = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10);
                    open.Filter = "Image Files (*.jpg)|*.jpg|all files(*.*)|*.*";
                    open.FilterIndex = 1;
                    if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        if (open.CheckFileExists)
                        {
                             _photo = System.IO.Path.GetFileName(open.FileName);
                            var imagePath = "C:\\ImagesPersonnel\\";

                           System.IO.Directory.CreateDirectory(imagePath);
                           if (System.IO.Directory.Exists(imagePath))
                           {
                               var image = imagePath + _photo;
                               if (!System.IO.File.Exists(image))
                               {
                                   System.IO.File.Copy(open.FileName, image);
                                   pictureBox1.Image = Image.FromFile(open.FileName);
                                   ConnectionClass.InsereImage(txtNummatricule.Text, image);
                                   MonMessageBox.ShowBox("Photo transferée avec succés ", "Information Photo");
                               }
                               else
                               {
                                   MonMessageBox.ShowBox(
                                       "Une image existe deja sous ce nom. Rénommer le fichier et réessayez.",
                                       "Erreur insertion");
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

        private void cmbMois_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDateNaissance.Focus();
        }
 
        private void groupBox7_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.Silver, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox7.Width - 1, this.groupBox7.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.SteelBlue, Color.DarkSlateBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        
        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
        }

        Bitmap badge;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            e.Graphics.DrawImage(badge, -5, 20, badge.Width, badge.Height);
        
            e.HasMorePages = false;
        }


        public string etat;
        private void PersonnelFrm_Load(object sender, EventArgs e)
        {
            try
            {
                foreach (var re in ConnectionClass.ListeDesRegions(idProjet))
                {
                    cmbRegion.Items.Add(re.Region);
                    cmbLocalite.Items.Add(re.Localisation);
                }

                foreach (var s in ConnectionClass.ListeContrat())
                    cmbTypeContrat.Items.Add(s.TypeContrat);
                foreach (var  b in ConnectionClass.ListeBanques())
                {
                    cmbBank.Items.Add(b.NomBanque);
                }
                foreach (var s in ConnectionClass.StatusDesPersonnels())
                {
                    cmbStatus.Items.Add(s);
                }
                cmbStatus.Text = "En cours";
                if (cmbStatus.Text == "En cours")
                    cmbStatus.ForeColor = Color.Green;
                else
                    cmbStatus.ForeColor = Color.Red;
                cmbDirection.Items.Add("");
                foreach (var d in ConnectionClass.ListeDirection())
                {
                    cmbDirection.Items.Add(d.Direction);
                }
                cmbDivision_SelectedIndexChanged(null, null);
                var listeDiplome = new string[]
            {
                "CEP/T",
                "BEPC/T",
                "Baccalaureat",
                "Diplôme BTS",
                "Licence",
                "Maitrise",
                "DEA",
                "Doctorat",
                "Autres"
            };

                foreach (string diplome in listeDiplome)
                {
                    cmbDiplome.Items.Add(diplome);
                }
                if (etat == "2")
                {
                    var listePersonnel = ConnectionClass.ListePersonnelProjet(idPersonnel);
                    var listeService = ConnectionClass.ListeServicePersonnelProjet(idPersonnel);
                  
                    txtNummatricule.Text = listePersonnel[0].NumeroMatricule;
                    txtNom.Text = listePersonnel[0].Nom;
                    txtPrenom.Text = listePersonnel[0].Prenom;
                    cmbJour.Text = listePersonnel[0].DateNaissance.Day < 10 ? "0" + listePersonnel[0].DateNaissance.Day : listePersonnel[0].DateNaissance.Day.ToString();
                    cmbMois.Text = listePersonnel[0].DateNaissance.Month < 10 ? "0" + listePersonnel[0].DateNaissance.Month : listePersonnel[0].DateNaissance.Month.ToString();
                    txtDateNaissance.Text = listePersonnel[0].DateNaissance.Year.ToString();
                    txtLieuNaissance.Text = listePersonnel[0].LieuNaissance;
                    txtAdresse.Text = listePersonnel[0].Adresse;
                    txtTelephone1.Text = listePersonnel[0].Telephone1;
                    txtTelephone2.Text = listePersonnel[0].Telephone2;
                    txtEmail.Text = listePersonnel[0].Email;

                    foreach (var service in listeService)
                    {
                        cmbGrade.Text = service.Grade;
                        cmbStatus.Text = service.Status;
                        txtEchelon.Text = service.Echelon;
                        dtpDateservice.Value = service.DateService;
                        txtPoste.Text = service.Poste;
                        txtAnciennete.Text = service.Anciennete;
                        cmbCategorie.Text = service.Categorie;
                        txtCNPS.Text = service.NoCNPS;
                        cmbDiplome.Text = service.Diplome;
                        cmbTypeContrat.Text = service.TypeContrat;
                        cmbLocalite.Text = service.Localite;
                        cmbRegion.Text = service.Region;
                        dtpFinContrat.Value = service.DateDepart;
                        var direction = from dir in ConnectionClass.ListeDirection()
                                        join div in ConnectionClass.ListeDivision()
                                        on dir.IDDirection equals div.IDDirection
                                        where div.IDDivision == service.IDDivision
                                        select dir.Direction;
                        foreach (var dir in direction)
                        {
                            cmbDirection.Text = dir;
                        }
                        var division = from div in ConnectionClass.ListeDivision()
                                       where div.IDDivision == service.IDDivision
                                       select div.Division;
                        foreach (var div in division)
                        {
                            cmbDivision.Text = div;
                        }
                        var departement = ConnectionClass.ListeDepartement(service.IDDepartement);
                        if (departement.Count > 0)
                        {
                            cmbService.Text = departement[0].Departement;
                        }
                    }
                    txtSalaire.Text = listeService[0].SalaireBrut.ToString();
                    txtGrille.Text = listeService[0].Primes.ToString();
                    foreach (var b in ConnectionClass.ListeDonneesBancaires(idPersonnel))
                    {
                          txtCodeBank.Text = b.CodeBanque;
                            txtCodeGuichet.Text = b.CodeGuichet;
                            txtClef.Text = b.Cle;
                            txtNoCompte.Text = b.Compte;
                            cmbBank.Text = b.NomBanque;
                    }
                    var image = listePersonnel[0].Photo;
                    if (System.IO.File.Exists(image))
                    {
                        pictureBox1.Image = Image.FromFile(image);
                    }
                    else
                    {
                        pictureBox1.Image = null;
                    }
                    if (listePersonnel[0].Sexe.ToString().ToUpper() == "F")
                    {
                        chkFemelle.Checked = true;
                    }
                    else if (listePersonnel[0].Sexe.ToString().ToUpper() == "M")
                    {
                        chkMale.Checked = true;
                    }

                    btnAjouter.Enabled = true;

                    btnFermer.Location = new Point(Width - 45, btnFermer.Location.Y);

                }
            }
            catch { }
        }

        private void cmbDepartement_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtAnciennete.Focus();
        }

        private void txtDateNaissance_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtDateNaissance.Text.Length >=4)
                {
                    txtDateNaissance.Text = txtDateNaissance.Text.Substring(0, 4);
                }
                var annee = Int32.Parse(txtDateNaissance.Text);
                var anneeActuel = DateTime.Now.Year;
                var moisActuel = DateTime.Now.Month;
                var mois = Int32.Parse(cmbMois.Text );
                if (moisActuel >= mois)
                {
                    age = anneeActuel - annee;
                    txtAge.Text = age.ToString();
                }
                else
                {
                    age = anneeActuel - annee - 1;
                    txtAge.Text = age.ToString();
                }
            }
            catch
            {
            }
        }

         private void cmbTypeContrat_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCNPS.Focus();
        }

        private void btnFermer_Click_1(object sender, EventArgs e)
        {
            Dispose();
        }

        private void cmbDirection_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbDivision.Items.Clear();
                var liste = from de in ConnectionClass.ListeDivision()
                            join di in ConnectionClass.ListeDirection()
                    on de.IDDirection equals di.IDDirection
                            where di.Direction.StartsWith(cmbDirection.Text, StringComparison.CurrentCultureIgnoreCase)
                            select new { de.Division };
                foreach (var d in liste)
                {
                    cmbDivision.Items.Add(d.Division);
                }
            }
            catch (Exception)
            {
            }
        }

        private void cmbDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbService.Items.Clear();
                var liste = from di in ConnectionClass.ListeDivision()
                            join de in ConnectionClass.ListeDepartement()
                           on di.IDDivision equals de.IDDivision
                            where di.Division.StartsWith(cmbDivision.Text, StringComparison.CurrentCultureIgnoreCase)
                            select new { de.Departement };
                foreach (var d in liste)
                {
                    cmbService.Items.Add(d.Departement);
                }
            }
            catch (Exception)
            {
            }
        }

        private void cmbRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbLocalite.Items.Clear();
            var liste = from l in ConnectionClass.ListeDesRegions(idProjet)
                        where l.Region == cmbRegion.Text
                        select l;
            foreach (var re in liste)
            {
                cmbLocalite.Items.Add(re.Localisation);
            }
        }

        private void PersonnelProjetFrm_Paint(object sender, PaintEventArgs e)
        {

        }

        
    }
}
