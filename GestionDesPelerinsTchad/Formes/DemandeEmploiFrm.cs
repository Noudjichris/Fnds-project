using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SGSP.AppCode;

namespace SGSP.Formes
{
    public partial class DemandeEmploiFrm : Form
    {
        public DemandeEmploiFrm()
        {
            InitializeComponent();
        }

        private void groupBox5_Paint(object sender, PaintEventArgs e)
        {
            var mGraphics = e.Graphics;
            var pen1 = new Pen(SystemColors.ControlLight, 2);
            var area1 = new Rectangle(0, 0, this.groupBox5.Width - 1, this.groupBox5.Height - 1);
            var linearGradientBrush = new LinearGradientBrush(area1,Color.DodgerBlue,
                Color.SteelBlue, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            var mGraphics = e.Graphics;
            var pen1 = new Pen(SystemColors.ControlLight, 2);
            var area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            var linearGradientBrush = new LinearGradientBrush(area1,
                SystemColors.ControlLight, SystemColors.ControlLight, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void DemandeEmploiFrm_Load(object sender, EventArgs e)
        {
            btnFermer.Location = new Point(Width - 40, 3);
            for (var i = 2017; i < DateTime.Now.Year + 10; i++)
            {
                cmbExercice.Items.Add(i.ToString());
            }
            cmbExercice.Text = DateTime.Now.Year.ToString();
            clObjet.Width = dataGridView1.Width / 5;
            Column2.Width = 50;
            Column5.Width = 35;
            Column3.Width = 35;
            Column13.Width =80;
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
            cmbDiplome.Items.Add("");
            ListeDemande();
        }
        int idDemande;

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDemandeur.Text))
            {
                GestionPharmacetique.MonMessageBox.ShowBox("Veuillez entrer le nom du demandeur", "Erreur");
                return;
            }
            if (string.IsNullOrWhiteSpace(cmbNatureDemande.Text))
            {
                GestionPharmacetique.MonMessageBox.ShowBox("Veuillez selectionner la nature de la demande sur la liste deroulante", "Erreur");
                return;
            }
            if (string.IsNullOrWhiteSpace(cmbDiplome.Text))
            {
                GestionPharmacetique.MonMessageBox.ShowBox("Veuillez selectionner le type de dilpôme obetnu", "Erreur");
                return;
            }
            if (string.IsNullOrWhiteSpace(cmbDomaine.Text))
            {
                GestionPharmacetique.MonMessageBox.ShowBox("Veuillez selectionner le domaine d'étude du demandeur sur la liste deroulante", "Erreur");
                return;
            }
            if (string.IsNullOrEmpty(txtTelephone.Text))
            {
                GestionPharmacetique.MonMessageBox.ShowBox("Veuillez entrer le numéro  du demandeur", "Erreur");
                return;
            }
            if (string.IsNullOrEmpty(txtQualification.Text))
            {
                GestionPharmacetique.MonMessageBox.ShowBox("Veuillez entrer la qualification du demandeur", "Erreur");
                return;
            }
            if (string.IsNullOrEmpty(txtPosteDemande.Text))
            {
                GestionPharmacetique.MonMessageBox.ShowBox("Veuillez entrer la poste du demandeur", "Erreur");
                return;
            }
            if (!radioButton1.Checked && !radioButton2.Checked)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("Veuillez cocher le sexe du demandeur", "Erreur");
                return;
            }
                        int anneeDexperience;
                        if (Int32.TryParse(txtExperience.Text, out anneeDexperience))
                        {
                            
                        }
                        else
                        {
                            GestionPharmacetique.MonMessageBox.ShowBox("Veuillez entrer un chiffre valide pour l'année d'experience du demandeur", "Erreur");
                            return;
                        }
            var demande = new Demande();
            demande.NumeroDemande = idDemande;
            demande.Qualification = txtQualification.Text;
            demande.Poste = txtPosteDemande.Text;
            demande.Nom = txtDemandeur.Text;
            demande.Exercice = Convert.ToInt32(cmbExercice.Text);
            demande.DomaineEtude = cmbDomaine.Text;
            demande.Disponibilite = cmbDisponibilite.Text;
            demande.Diplome = cmbDiplome.Text;
            demande.DateDemande = dtpDateDemande.Value;
            demande.NatureStage = cmbNatureDemande.Text;
            demande.Sexe = radioButton1.Checked ? "M" : "F";
            demande.Telephone = txtTelephone.Text;
            demande.Email = txtEmail.Text;
            demande.Experience = anneeDexperience;
            if (ConnectionClass.EnregistrerUneDemande(demande))
            {
                idDemande = 0;
                txtEmail.Text = "";
                txtDemandeur.Text = "";
                txtPosteDemande.Text = "";
                txtQualification.Text = "";
                txtTelephone.Text = "";
                cmbNatureDemande.Text = " ";
                cmbDomaine.Text = " ";
                    cmbDisponibilite.Text=" ";
                cmbDiplome.Text=" ";
                txtExperience.Text = "";
                cmbExercice.Text = DateTime.Now.Year.ToString();
                dtpDateDemande.Value = DateTime.Now;
                ListeDemande();
            }
        }
   void  ListeDemande()
        {
            try
            {
                dataGridView1.Rows.Clear();
                foreach (var d in AppCode.ConnectionClass.ListeDemande())
                {
                    dataGridView1.Rows.Add(d.NumeroDemande, d.Nom, d.Sexe, d.Telephone, d.Email, d.Qualification, d.DomaineEtude, d.Diplome, d.DateDemande.ToShortDateString(), d.NatureStage,d.Experience,d.Exercice,d.Poste,d.Disponibilite);
                }
            }
            catch { }
        }

   private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
   {
       if (e.ColumnIndex == 14)
       {
           idDemande = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
           txtDemandeur.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
           txtTelephone.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
           txtEmail.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
           txtQualification.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
           cmbDomaine.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
           dtpDateDemande.Value = DateTime.Parse(dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString());
           cmbNatureDemande.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
           cmbExercice.Text = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
           txtPosteDemande.Text = dataGridView1.Rows[e.RowIndex].Cells[12].Value.ToString();
           cmbDiplome.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
           txtExperience.Text = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
           cmbDisponibilite.Text = dataGridView1.Rows[e.RowIndex].Cells[13].Value.ToString();
           if (dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString() == "M")
           {
               radioButton1.Checked = true;
           }
           else if (dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString() == "F")
           {
               radioButton2.Checked = true;
           }
       }
       else if (e.ColumnIndex == 15)
       {
           if (GestionPharmacetique.MonMessageBox.ShowBox("voulez vous supprimer ces données?", "Confirmation") == "1")
           {
               if (ConnectionClass.SupprimerUneDemande(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString())))
               {
                   dataGridView1.Rows.Remove(dataGridView1.Rows[e.RowIndex]);
               }
           }
       }

   }

   private void btnFermer_Click(object sender, EventArgs e)
   {
       Dispose();
   }

   private void button1_Click(object sender, EventArgs e)
   {
       try
       {
           if (dataGridView1.SelectedRows.Count > 0)
           {
               if (string.IsNullOrWhiteSpace(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()))
               {
                   return;
               }
               var rootPath = GlobalVariable.rootPathDemande;
               {

                   var nomPersonnel = "Demande_" + dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                   rootPath = rootPath + nomPersonnel;
                   //if (GestionPharmacetique.MonMessageBox.ShowBox("Voulez vous creer le dossier de " + nomPersonnel + "?", "Confirmation") == "1")
                   //{
                       if (!System.IO.Directory.Exists(rootPath))
                       {
                           System.IO.Directory.CreateDirectory(rootPath);
                           //GestionPharmacetique.MonMessageBox.ShowBox("Dossier du personnel " + nomPersonnel + " crée avec succés", "Inforation");
                       }
                       InsererDossier();
               }
           }
       }
       catch (Exception ex)
       {
           GestionPharmacetique.MonMessageBox.ShowBox("creation du dossier", ex);
       }
   }
            private void InsererDossier()
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    if (string.IsNullOrWhiteSpace(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()))
                    {
                        return;
                    }
                    var nomPersonnel ="Demande_"+ dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                    var rootPath = GlobalVariable.rootPathDemande + nomPersonnel;
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
                             GestionPharmacetique.   MonMessageBox.ShowBox("fichier transferé avec succés ", "Information fichier");
                            }
                            else
                            {
                                if (GestionPharmacetique.MonMessageBox.ShowBox("Ce fichier existe deja Voulez vous faire une copie?", "confirmation") == "1")
                                {
                                    var rootPath1 = rootPath.Substring(0, rootPath.LastIndexOf(".")) + "_copie1" + rootPath.Substring(rootPath.LastIndexOf("."));
                                    if (!System.IO.File.Exists(rootPath1))
                                    {
                                        System.IO.File.Copy(fileName, rootPath1);
                                        GestionPharmacetique.MonMessageBox.ShowBox("fichier transferé avec succés ", "Information fichier");
                                    }
                                    else
                                    {

                                        var rootPath2 = rootPath.Substring(0, rootPath.LastIndexOf(".")) + "_copie2" + rootPath.Substring(rootPath.LastIndexOf("."));
                                        if (!System.IO.File.Exists(rootPath2))
                                        {
                                            System.IO.File.Copy(fileName, rootPath2);
                                            GestionPharmacetique.MonMessageBox.ShowBox("fichier transferé avec succés ", "Information fichier");
                                        }
                                        else
                                        {
                                            var rootPath3 = rootPath.Substring(0, rootPath.LastIndexOf(".")) + "_copie3" + rootPath.Substring(rootPath.LastIndexOf("."));
                                            if (!System.IO.File.Exists(rootPath3))
                                            {
                                                System.IO.File.Copy(fileName, rootPath3);
                                                GestionPharmacetique.MonMessageBox.ShowBox("fichier transferé avec succés ", "Information fichier");
                                            }
                                            else
                                            {
                                                var rootPath4 = rootPath.Substring(0, rootPath.LastIndexOf(".")) + "_copie4" + rootPath.Substring(rootPath.LastIndexOf("."));
                                                if (!System.IO.File.Exists(rootPath4))
                                                {
                                                    System.IO.File.Copy(fileName, rootPath4);
                                                    GestionPharmacetique.MonMessageBox.ShowBox("fichier transferé avec succés ", "Information fichier");
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
                    GestionPharmacetique.MonMessageBox.ShowBox("Veuillez selectionner les données du personnel. Puis réessayez ", "Information Photo");
                }
            }

            catch (Exception ex) { GestionPharmacetique.MonMessageBox.ShowBox("Transfert de l'image", ex); }
        }

            private void button9_Click(object sender, EventArgs e)
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
                        frm.nomPersonnel ="Demande_"+ dataGridView1.SelectedRows[0].Cells[1].Value.ToString() ;
                        frm.state = "1";
                        frm.ShowDialog();

                    }

                }
                catch (Exception ex)
                {
                   GestionPharmacetique. MonMessageBox.ShowBox("creation du dossier", ex);
                }
            }
    

    }
}
