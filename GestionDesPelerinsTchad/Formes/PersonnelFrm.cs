using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using GestionPharmacetique;
using SGSP.AppCode;
using BarcodeLib.Barcode;

namespace SGSP
{
    public partial class PersonnelFrm : Form
    {
        public PersonnelFrm()
        {
            InitializeComponent();
        }

        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.AliceBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox2.Width - 1, this.groupBox2.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.AliceBlue, Color.AliceBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox3_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.AliceBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox3.Width - 1, this.groupBox3.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.AliceBlue, Color.AliceBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        public static PersonnelFrm frm=new PersonnelFrm();
        static bool fl;
        public static bool ShowBox()
        {
            frm = new PersonnelFrm();
            frm.ShowDialog();
            return fl;
        }

        // les proprietes du pelerin
        #region proprietesDuPersonnel
        public string _ancienMatricule;
        DateTime _dateNaissance;
        string _prenom, _nom;
        string _lieuNaissance, _sexe, anciennete, diplome, typeContrat;
        string _telephone1, _telephone2, _photo, _numeroMatricule;
        int _numeroDepartement,age;
        string  _adresse, _email, _numeroCompte, categorie, noCNPS;
        string _echelon, _poste;
        DateTime _dateService;
        private double salaireBrut, grilleSalariale, indice;
        double primeMotivation, autresPrimes, primeTransport, fraisComm, indeminite;
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

                if (string.IsNullOrEmpty(txtNummatricule.Text))
                {
                    MonMessageBox.ShowBox("Le matricule ne doit être vide", "Erreur");
                    return;
                }
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
                //if (string.IsNullOrEmpty(txtTelephone1.Text))
                //{
                //    MonMessageBox.ShowBox("Veuillez saisir le numero de telephone", "Erreur");
                //    return;
                //}

                //valider le champ pour l adresse
             
                //valider le champ pour le poste
                if (string.IsNullOrEmpty(txtPoste.Text))
                {
                    MonMessageBox.ShowBox("Veuillez saisir le poste du personnel", "Erreur");
                    return;
                }
                //valider le champ pour le grade
                if (string.IsNullOrEmpty(cmbGrade.Text))
                {
                    MonMessageBox.ShowBox("Veuillez selectionner la grade du personnel sur la liste deroulante", "Erreur");
                    return;
                }
                //valider le champ pour le departement
                if (string.IsNullOrEmpty(cmbDivision.Text))
                {
                    MonMessageBox.ShowBox("Veuillez selectionner la division du personnel", "Erreur");
                    return;
                }
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
 grilleSalariale = 0;
 indice = 0;

 


                var numeroSalaire = 0;
                ObtenirlesDonneesPersonnel();
                var personnel = new Personnel();
                personnel.NumeroMatricule = _numeroMatricule;
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
                personnel.NumeroPiece = txtNoPiece.Text;
                personnel.TypePiece = cmbTypePiece.Text;
                personnel.SituationMatrimoniale = cmbSitutationMatri.Text;
            
                int nbreEnfant = 0;
                if (Int32.TryParse(txtNbreEnfant.Text , out nbreEnfant))
                    personnel.NombreEnfant = nbreEnfant;
                var service = new Service();
                service.Anciennete = anciennete;
                service.Categorie = categorie;
                service.TypeContrat = typeContrat;
                service.DateService = _dateService;
                service.Diplome = diplome;
                service.Echelon = _echelon;
                service.NoCNPS = noCNPS;
                service.NumeroMatricule = _numeroMatricule;
                service.Poste=_poste;
                service.DateDepart = dtpFinContrat.Value;
                service.IDDepartement = _numeroDepartement;
                service.Grade = cmbGrade.Text;
                service.SiCNRT = radioButton2.Checked;
                service.Localite = cmbLocalisation.Text;
                var listeDiv = from div in ConnectionClass.ListeDivision()
                               where div.Division==cmbDivision.Text
                               select div.IDDivision;
                foreach (var div in listeDiv)
                    service.IDDivision = div;
                service.Status = cmbStatus.Text;
                if (double.TryParse(txtAutresPrimes.Text, out autresPrimes)) { }
                if (double.TryParse(txtTransport.Text, out primeTransport)) { }
                if (double.TryParse(txtPrimeMotivation.Text, out primeMotivation)) { }
                if (double.TryParse(txtIndemnite.Text, out indeminite)) { }
                if (double.TryParse(txtFraisComm.Text, out fraisComm)) { }
                var salaire = new Salaire();
                salaire.GrilleSalarialle = grilleSalariale;
                salaire.Indice = indice;
                salaire.SalaireBase = salaireBrut;
                salaire.IDSalaire = numeroSalaire;
                salaire.Indemnites = indeminite;
                salaire.PrimeMotivation = primeMotivation;
                salaire.PrimeTransport = primeTransport;
                salaire.FraisCommunication = fraisComm;
                salaire.AutresPrimes = autresPrimes;

                var bank = new Banque();
                bank.Compte = _numeroCompte;
                bank.CodeBanque = txtCodeBank.Text;
                bank.CodeGuichet = txtCodeGuichet.Text;
                bank.Cle = txtClef.Text;
                bank.NomBanque = cmbBank.Text;
                if (etat == "2")
                {
                    if (MonMessageBox.ShowBox("Voulez vous modifier les données de \nce personnel?", "Confirmation") == "1")
                    {
                        if (ConnectionClass.ModifierUnPersonnel(personnel, service,salaire, _ancienMatricule))
                        {
                            nom = _nom;
                            ViderLesChamps();
                            fl = true;
                            this.Close();
                        }
                    }
                }
               else  
                {
                    var dtPers = ConnectionClass.ListePersonnelParMatricule(txtNummatricule.Text);
                    if (dtPers.Count > 0)
                    {
                        MonMessageBox.ShowBox("Le matricule que vous avez saisi existe deja dans la base de données", "Erreur");
                        return;
                    }
                    if (ConnectionClass.AjouterUnPersonnel(personnel, service,salaire,bank))
                    {
                        ViderLesChamps();
                        fl = true;
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
            var  dtPersonnel = ConnectionClass.ListeDepartement(cmbService.Text);
            if (dtPersonnel.Rows.Count > 0)
                _numeroDepartement = Int32.Parse(dtPersonnel.Rows[0].ItemArray[0].ToString());
            else
                _numeroDepartement = 0;
            _poste = txtPoste.Text;
            _numeroMatricule = txtNummatricule.Text;
            anciennete = txtAnciennete.Text;
            noCNPS = txtCNPS.Text;
            diplome = cmbDiplome.Text;
            typeContrat = cmbTypeContrat.Text;
            _echelon = txtGrade.Text;
            _dateService = dtpDateservice.Value.Date;
        }
        //vider les champs
        private void ViderLesChamps()
        {
            txtCodeBank.Text = "";
            txtCodeGuichet.Text = "";
            txtClef.Text = "";
            txtNoCompte.Text = "";
            txtSalaire.Text = "";
            txtTransport.Text = "";
            txtAutresPrimes.Text = "";
            txtFraisComm.Text = "";
            txtPrimeMotivation.Text="";
            txtIndemnite.Text = "";
            //txtIndice.Text = "";
            txtEmail.Text = "";
            //txtNummatricule.Text = "";
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
            txtGrade.Text = "";
        }

        private void dtpDateservice_ValueChanged(object sender, EventArgs e)
        {
            var annee = DateTime.Now.Date.Subtract(dtpDateservice.Value).Days / 365;
            txtAnciennete.Text = annee.ToString();
            txtGrade.Text =( (DateTime.Now.Date.Subtract(dtpDateservice.Value).Days / 365)/2).ToString();
            cmbTypeContrat_SelectedIndexChanged(null, null);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            cmbTypeContrat.Items.Clear();
            var frm = new Formes.TypeContratFrm();
            frm.ShowDialog();
            foreach (var s in ConnectionClass.ListeContrat())
                cmbTypeContrat.Items.Add(s.TypeContrat);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            cmbService.Items.Clear();
            var frm = new Formes.ServiceFrm();
            frm.ShowDialog();
            foreach (var d in ConnectionClass.ListeDepartement())
            {
                cmbService.Items.Add(d.Abreviation);
            }
        }

        int numeroBancaire;
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex==6)
            {
                txtCodeGuichet.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtCodeBank.Text= dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                cmbBank.Text= dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtClef.Text= dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                txtNoCompte.Text= dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                numeroBancaire = Int32.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            }else if(e.ColumnIndex==7)
            {
                if (MonMessageBox.ShowBox("Voulez vous supprimer ces données ?", "Confirmation") == "1")
                {
                    if(AppCode.ConnectionClass.SupprimerLesDonneesBancaires(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString())));
                    dataGridView1.Rows.Remove(dataGridView1.Rows[e.RowIndex]);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var bank = new Banque();
            bank.NomBanque = cmbBank.Text;
            bank.CodeBanque = txtCodeBank.Text;
            bank.CodeGuichet = txtCodeGuichet.Text;
            bank.Compte = txtNoCompte.Text;
            bank.Cle = txtClef.Text;
            bank.NumeroMatricule = txtNummatricule.Text;
            bank.ID = numeroBancaire;
            if(ConnectionClass.EnregistrerLesDonneesBancaires(bank))
            {
                dataGridView1.Rows.Clear();
                numeroBancaire = 0;
                foreach (var b in ConnectionClass.ListeDonneesBancaires(numeroMatricule))
                {
                    dataGridView1.Rows.Add(b.ID, b.NomBanque, b.Compte, b.CodeBanque, b.CodeGuichet, b.Cle);
                    if (b.EtatParDefaut)
                    {
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.Blue;
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.SelectionBackColor = Color.White;
                        dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.SelectionForeColor = Color.Blue;
                    }
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (ConnectionClass.ChoisirLeCompteParDefaut(numeroBancaire, numeroMatricule))
                {
                    
                        dataGridView1.Rows.Clear();
                        numeroBancaire = 0;
                        foreach (var b in ConnectionClass.ListeDonneesBancaires(numeroMatricule))
                        {
                            dataGridView1.Rows.Add(b.ID, b.NomBanque, b.Compte, b.CodeBanque, b.CodeGuichet, b.Cle);
                            if (b.EtatParDefaut)
                            {
                                dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.Blue;
                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.SelectionBackColor = Color.White;
                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.SelectionForeColor = Color.Blue;
                        }
                        }
                    
                }
            }
            catch { }
        }
        //nouveau personnel
        private void button1_Click(object sender, EventArgs e)
        {
          ViderLesChamps();
        }

        //quitter la forme
        private void btnFermer_Click(object sender, EventArgs e)
        {
            fl = false;
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
                             var imagePath = AppCode.GlobalVariable.rootPathPersonnel + txtNom.Text + " " + txtPrenom.Text + "//";
                             if (!System.IO.Directory.Exists(imagePath))
                             {
                                 System.IO.Directory.CreateDirectory(imagePath);
                             }
                             imagePath = imagePath + _photo;
                             if (!System.IO.File.Exists(imagePath))
                               {
                                   System.IO.File.Copy(open.FileName, imagePath);
                                   pictureBox1.Image = Image.FromFile(open.FileName);
                                   ConnectionClass.InsereImage(txtNummatricule.Text, imagePath);
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
                Color.SteelBlue, Color.SteelBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox4_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.AliceBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox4.Width - 1, this.groupBox4.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.AliceBlue, Color.AliceBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void PersonnelFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue, 2);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                SystemColors.Control, Color.WhiteSmoke, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
     
        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.AliceBlue, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.AliceBlue, Color.AliceBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        public static string etat, numeroMatricule,nom;
        private void PersonnelFrm_Load(object sender, EventArgs e)
        {
            try
            {
                cmbGrade.Items.Add("");
                var grades = new string[] {
                          "Ingénieur Statisticien Economiste",	
                             "Ingénieurs Statisticien Démographes",
                            "Ingénieurs d'Etat en Statistique et Planification",		
                        "Ingénieurs des Travaux Statistiques",		
                        "Ingénieurs des Travaux de Planification",		
                        "Informaticiens",		
                        "Economistes",		
                        "Administrateur/Manager",		
                        "Comptable",		
                        "Adjoints Techniques de la Statistique",
                        "Agents Techniques de la Statistique",		
                        "Autre cadres",		
                        "Personnel d'Appui"};
                cmbGrade.Items.AddRange(grades);
                foreach (var s in ConnectionClass.ListeContrat())
                    if (!s.TypeContrat.Contains("PDST"))
                    cmbTypeContrat.Items.Add(s.TypeContrat);
                foreach (var   b in ConnectionClass.ListeBanques())
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
               "Master",
                "Doctorat",
                "Autres"
            };
                //cmbLocalisation,
                foreach (var s in ConnectionClass.ListeLocalisation())
                    cmbLocalisation.Items.Add(s.Localite);
                foreach (string diplome in listeDiplome)
                {
                    cmbDiplome.Items.Add(diplome);
                }
                if (etat == "2")
                {
                    var listePersonnel = ConnectionClass.ListePersonnelParMatricule(numeroMatricule);
                    var listeService =  ConnectionClass.ListeServicePersonnel(numeroMatricule);
                    var salaire = ConnectionClass.ListeSalaire(numeroMatricule);
                    var frmPersonnel = new PersonnelFrm();
                    _ancienMatricule = numeroMatricule;
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
                    cmbSitutationMatri.Text = listePersonnel[0].SituationMatrimoniale;
                    txtNbreEnfant.Text = listePersonnel[0].NombreEnfant.ToString();
                    txtNoPiece.Text = listePersonnel[0].NumeroPiece;
                    cmbTypePiece.Text = listePersonnel[0].TypePiece;
                    foreach (var service in listeService)
                    {
                        if (service.SiCNRT)
                            radioButton2.Checked = true;
                        else
                            radioButton1.Checked = true;
                        cmbStatus.Text = service.Status;
                        txtGrade.Text = service.Echelon;
                        dtpDateservice.Value = service.DateService;
                        txtPoste.Text = service.Poste;
                        txtAnciennete.Text = service.Anciennete;
                        cmbCategorie.Text = service.Categorie;
                        txtCNPS.Text = service.NoCNPS;
                        cmbDiplome.Text = service.Diplome;
                        cmbTypeContrat.Text = service.TypeContrat;
                        dtpFinContrat.Value = service.DateDepart;
                        cmbLocalisation.Text = service.Localite;
                        
                        cmbGrade.Text = service.Grade;
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
                    txtSalaire.Text = salaire[0].SalaireBase.ToString();
                    txtAutresPrimes.Text = salaire[0].AutresPrimes.ToString();
                    txtFraisComm.Text = salaire[0].FraisCommunication.ToString();
                    txtPrimeMotivation.Text = salaire[0].PrimeMotivation.ToString();
                    txtIndemnite.Text = salaire[0].Indemnites.ToString();
                    txtTransport.Text = salaire[0].PrimeTransport.ToString();
                    foreach(var b in ConnectionClass.ListeDonneesBancaires(numeroMatricule))
                    {
                        dataGridView1.Rows.Add(b.ID, b.NomBanque, b.Compte, b.CodeBanque, b.CodeGuichet, b.Cle) ;
                        if(b.EtatParDefaut)
                        {
                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.ForeColor = Color.Blue;
                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.SelectionBackColor = Color.White;
                            dataGridView1.Rows[dataGridView1.Rows.Count - 1].DefaultCellStyle.SelectionForeColor = Color.Blue;
                            txtCodeBank.Text = b.CodeBanque;
                           txtCodeGuichet.Text = b.CodeGuichet;
                            txtClef.Text = b.Cle;
                            txtNoCompte.Text = b.Compte;
                            cmbBank.Text = b.NomBanque;
                        }
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
                if (txtDateNaissance.Text.Length > 4)
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
                cmbCategorie_SelectedIndexChanged(null, null);
            }
            catch
            {
            }
        }

        private void cmbDepartement_Click(object sender, EventArgs e)
        {
            //liste des departements
            //cmbDivision.Items.Clear();
            //foreach (var d in ConnectionClass.ListeDepartement())
            //{
            //    cmbDivision.Items.Add(d.Departement);
            //}
        }

        private void cmbTypeContrat_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbTypeContrat.Text.ToUpper() .Contains("CONTRACTUEL".ToUpper()) ||
                    cmbTypeContrat.Text.ToUpper().Contains("CONTRACTUEL DE L'ETAT".ToUpper()))
                {
                    if (cmbTypeContrat.Text.ToUpper().Contains("ETAT".ToUpper()))
                        radioButton2.Checked = true;
                    else
                        radioButton1.Checked = true;
                    cmbCategorie.Items.Clear();
                    var categories = new String[] { "I","II","III","IV","V","VI","VII" };
                    cmbCategorie.Items.AddRange(categories);
                    if (DateTime.TryParse(cmbJour.Text + "/" + cmbMois.Text + "/" + txtDateNaissance.Text, out _dateNaissance))
                    {
                        dtpFinContrat.Value = _dateNaissance.AddYears(60);
                    }
                }
                else if (
                   cmbTypeContrat.Text.ToUpper().Contains("FONCTIONNAIRE".ToUpper()))
                {
                    radioButton2.Checked = true;
                    cmbCategorie.Items.Clear();
                    var categories = new String[] { "A", "B", "C" };
                    cmbCategorie.Items.AddRange(categories);
                 
                    if (DateTime.TryParse(cmbJour.Text + "/" + cmbMois.Text + "/" + txtDateNaissance.Text, out _dateNaissance))
                    {
                        if (cmbCategorie.Text == "A")
                        {
                            dtpFinContrat.Value = _dateNaissance.AddYears(65);
                        }
                        else
                        {
                            dtpFinContrat.Value = _dateNaissance.AddYears(60);
                        }
                    }
                }
                else
                {
                    cmbCategorie.Items.Clear();
                    var categories = new String[] { "A", "B", "C" ,"I", "II", "III", "IV", "V", "VI", "VII" };
                    cmbCategorie.Items.AddRange(categories);
                    if (DateTime.TryParse(cmbJour.Text + "/" + cmbMois.Text + "/" + txtDateNaissance.Text, out _dateNaissance))
                    {
                        dtpFinContrat.Value = _dateNaissance.AddYears(60);
                    }
                }
                //txtCNPS.Focus();

            }
            catch { }
        }

        
        private void PersonnelFrm_FormClosed(object sender, FormClosedEventArgs e)
        {
            etat = "1";
        }

        private void txtNummatricule_TextChanged(object sender, EventArgs e)
        {
            //ViderLesChamps();
            //try
            //{

            //    var dtpersonnel = ConnectionClass.ListeDesPersonnelParNumeroMatricule(txtNummatricule.Text);
            //    var dtService = ConnectionClass.ListeDesServices(txtNummatricule.Text);
            //    var dtsalaire = ConnectionClass.ListeSalaire(txtNummatricule.Text);
            //    var frmPersonnel = new PersonnelFrm();
            //    _ancienMatricule = numeroMatricule;
            //    txtNummatricule.Text = dtpersonnel.Rows[0].ItemArray[0].ToString();
            //    txtNom.Text = dtpersonnel.Rows[0].ItemArray[1].ToString();
            //    txtPrenom.Text = dtpersonnel.Rows[0].ItemArray[2].ToString();
            //    var datenaissance = DateTime.Parse(dtpersonnel.Rows[0].ItemArray[3].ToString());
            //    cmbJour.Text = datenaissance.Day.ToString();
            //    txtNoCompte.Text =
            //    cmbMois.Text = datenaissance.Month.ToString();
            //    txtDateNaissance.Text = datenaissance.Year.ToString();
            //    txtLieuNaissance.Text = dtpersonnel.Rows[0].ItemArray[4].ToString();
            //    txtAdresse.Text = dtpersonnel.Rows[0].ItemArray[5].ToString();
            //    txtTelephone1.Text = dtpersonnel.Rows[0].ItemArray[6].ToString();
            //    txtTelephone2.Text = dtpersonnel.Rows[0].ItemArray[7].ToString();
            //    txtEmail.Text = dtpersonnel.Rows[0].ItemArray[8].ToString();
            //    cmbDivision.Text = dtpersonnel.Rows[0].ItemArray[11].ToString();
            //    txtGrade.Text = dtService.Rows[0].ItemArray[4].ToString();
            //    dtpDateservice.Value = DateTime.Parse(dtService.Rows[0].ItemArray[1].ToString());
            //    txtPoste.Text = dtService.Rows[0].ItemArray[2].ToString();
            //    txtAnciennete.Text = dtService.Rows[0].ItemArray[6].ToString();
            //    txtCategorie.Text = dtService.Rows[0].ItemArray[5].ToString();
            //    txtCNPS.Text = dtService.Rows[0].ItemArray[7].ToString();
            //    cmbDiplome.DropDownStyle = ComboBoxStyle.DropDown;
            //    cmbTypeContrat.DropDownStyle = ComboBoxStyle.DropDown;
            //    cmbDiplome.Text = dtService.Rows[0].ItemArray[8].ToString();
            //    cmbTypeContrat.Text = dtService.Rows[0].ItemArray[9].ToString();
            //    txtCodeBank.Text = dtpersonnel.Rows[0].ItemArray[23].ToString();
            //    txtClef.Text = dtpersonnel.Rows[0].ItemArray[25].ToString();
            //    txtCodeGuichet.Text = dtpersonnel.Rows[0].ItemArray[24].ToString();
            //    txtPrimeDeGarde.Text = dtsalaire.Rows[0].ItemArray[6].ToString();
            //    txtPrimeExcep.Text = dtsalaire.Rows[0].ItemArray[8].ToString();
            //    txtPrimeResponsabilite.Text = dtsalaire.Rows[0].ItemArray[7].ToString();
            //    txtPrmeDeTransport.Text = dtsalaire.Rows[0].ItemArray[9].ToString();
            //    txtLogement.Text = dtsalaire.Rows[0].ItemArray[5].ToString();
            //    cmbBank.Text = dtsalaire.Rows[0].ItemArray[10].ToString();
            //    DateTime dateRetraite, dateFinContrat;
            //    if (DateTime.TryParse(dtService.Rows[0].ItemArray[12].ToString(), out dateRetraite))
            //    {
            //    }
            //    else
            //    {
            //        dateRetraite = DateTime.Now;
            //    }
            //    if (DateTime.TryParse(dtService.Rows[0].ItemArray[13].ToString(), out dateFinContrat))
            //    {
            //    }
            //    else
            //    {
            //        dateFinContrat = DateTime.Now;
            //    }

            //    if (dtService.Rows[0].ItemArray[10].ToString() == "1")
            //    {
            //        chkRetraite.Checked = true;
            //    }
            //    else
            //    {
            //        chkNonRetraite.Checked = true;
            //    }

            //    if (dtService.Rows[0].ItemArray[11].ToString() == "1")
            //    {
            //        chkFinContrat.Checked = true;
            //    }
            //    else
            //    {
            //        chkEnCours.Checked = true;
            //    }

            //    dtpDateRetraite.Value = dateRetraite;
            //    dtpFinContrat.Value = dateFinContrat;

            //    if (dtsalaire.Rows.Count > 0)
            //    {
            //        txtSalaire.Text = dtsalaire.Rows[0].ItemArray[0].ToString();
            //        //txtIndice.Text = dtsalaire.Rows[0].ItemArray[2].ToString();
            //    }
            //    txtNoCompte.Text = dtpersonnel.Rows[0].ItemArray[18].ToString();
            //    var image = dtpersonnel.Rows[0].ItemArray[10].ToString();
            //    if (System.IO.File.Exists(image))
            //    {
            //        pictureBox1.Image = Image.FromFile(image);
            //    }
            //    else
            //    {
            //        pictureBox1.Image = null;
            //    }
            //    if (dtpersonnel.Rows[0].ItemArray[9].ToString().ToUpper() == "F")
            //    {
            //        chkFemelle.Checked = true;
            //    }
            //    else if (dtpersonnel.Rows[0].ItemArray[9].ToString().ToUpper() == "M")
            //    {
            //        chkMale.Checked = true;
            //    }
            //}
            //catch { }
        }

        private void cmbDivision_SelectedIndexChanged(object sender, EventArgs e)
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

        private void txtNummatricule_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if(!string.IsNullOrEmpty(txtNummatricule.Text))
                {
                    txtNom.Focus();
                }
            }
        }

        private void txtNom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtNom.Text))
                {
                    txtPrenom.Focus();
                }
            }
        }

        private void txtLieuNaissance_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtPrenom.Text))
                {
                    cmbJour.Focus();
                    cmbJour.DroppedDown = true;
                }
            }
        }

        private void cmbDivision_SelectedIndexChanged_1(object sender, EventArgs e)
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

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbStatus.Text == "En cours")
                cmbStatus.ForeColor = Color.Green;
            else
                cmbStatus.ForeColor = Color.Red;
        }

        private void cmbCategorie_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCategorie.Text == "A")
            {
                dtpFinContrat.Value = _dateNaissance.AddYears(65);
            }
            else
            {
                dtpFinContrat.Value = _dateNaissance.AddYears(60);
            }
        }

        private void dtpFinContrat_ValueChanged(object sender, EventArgs e)
        {
            if (dtpFinContrat.Value.Date > DateTime.Now.Date)
                cmbStatus.Text = "En cours";
            else
                cmbStatus.Text = "Fin contrat";
        }

        private void cmbBank_SelectedIndexChanged(object sender, EventArgs e)
        {
            var liste = from b in AppCode.ConnectionClass.ListeBanques()
                        where b.NomBanque == cmbBank.Text
                        select b;
            foreach (var b in liste)
            {
                txtCodeBank.Text = b.CodeBanque;
                txtCodeGuichet.Text = b.CodeGuichet;
                txtNoCompte.Focus();
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            cmbLocalisation.Items.Clear();
            cmbLocalisation.Items.Add("");
            var frm = new Formes.LocalisationFrm();
            frm.ShowDialog();
            foreach (var s in ConnectionClass.ListeLocalisation())
                cmbLocalisation.Items.Add(s.Localite);
        }

        private void chkFemelle_CheckedChanged(object sender, EventArgs e)
        {
            if(chkFemelle.Checked)
            chkMale.Checked = false;
        }

        private void chkMale_CheckedChanged(object sender, EventArgs e)
        {
            if (chkMale.Checked)
                chkFemelle.Checked = false;
        }

        private void cmbDiplome_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDiplome.Text == "Master")
            {
                cmbDiplome.DropDownStyle = ComboBoxStyle.DropDown;
                cmbDiplome.Focus();
            }
        }

    }
}
