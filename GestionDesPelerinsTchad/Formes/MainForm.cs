using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using GestionAcademique;
using SGSP.AppCode;
using SGSP.Formes;
using GestionPharmacetique;

namespace SGSP
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            var mGraphics = e.Graphics;
            var pen1 = new Pen(Color.Silver, 0);
            var area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            var linearGradientBrush = new LinearGradientBrush(area1,
                Color.Transparent, Color.Transparent, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            var mGraphics = e.Graphics;
            var pen1 = new Pen(Color.FromArgb(0, 0, 64), 0);
            var area1 = new Rectangle(0, 0, this.groupBox2.Width - 1, this.groupBox2.Height - 1);
            var linearGradientBrush =
               new LinearGradientBrush(area1, Color.FromArgb(0, 0, 64),
               Color.Black, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        public static int width, height, height1;
        //LoginForm loginForm = new LoginForm();
             private void MainForm_Load(object sender, EventArgs e)
        {
            label2.Location = new Point(Width - label2.Width - 20, 30);
            label1.Location = new Point((Width - label1.Width) / 2, 15);
            pictureBox2.Location = new Point((Width - pictureBox2.Width) / 2, pictureBox2.Location.Y);
            pictureBox5.Location = new Point(Width - pictureBox5.Width - 20, pictureBox5.Location.Y);
            button2.Location = new Point(2, Height - 120);
            timer1.Start(); groupBox2.Visible = false;

            //SeDeconnecter();
            ConnectionClass.MettreFinAuContrat();
            ConnectionClass.MettreAjourAvancement();


            //width = tabControl1.Width + groupBox12.Width;
            height = groupBox3.Height;
            height1 = Height;
            width = Width;
        }

        //se deconnecter
        void SeDeconnecter()
        {
            toolStripMenuItem2.Visible = false;
            gestionDesDocumentsToolStripMenuItem.Visible = false;
            seConnecterToolStripMenuItem.Enabled = true;
            seDeconnecterToolStripMenuItem.Enabled = false;
            //tacheToolStripMenuItem.Visible = false;
            notificationToolStripMenuItem.Visible = false;
            utilisateurToolStripMenuItem.Visible = false;
            personnelToolStripMenuItem.Visible = false;
            gestionSalarialToolStripMenuItem.Visible = false;
            gallerieToolStripMenuItem.Visible = false;
            toolStripMenuItem1.Visible = false;
        }

        void SeConnecter()
        {
            if (LoginFrm.typeUtilisateur == "admin")
            {
                departPersonnelToolStripMenuItem.Visible = true;
                toolStripMenuItem2.Visible = true;
                toolStripMenuItem1.Visible = true;
                gestionDesDocumentsToolStripMenuItem.Visible = true;
                notificationToolStripMenuItem.Visible = true;
                //maintenanceToolStripMenuItem.Visible = true;
                utilisateurToolStripMenuItem.Visible = true;
                personnelToolStripMenuItem.Visible = true;
                gestionSalarialToolStripMenuItem.Visible = true;
                gallerieToolStripMenuItem.Visible = true;
                enregistrementPersonnelToolStripMenuItem.Visible = Enabled;
                divisionToolStripMenuItem.Enabled = true;
                directionToolStripMenuItem.Enabled = true;
            }
            else
            {
                if (LoginFrm.typeUtilisateur == "compta")
                {
                    toolStripMenuItem1.Visible = true;
                    gestionDesDocumentsToolStripMenuItem.Visible = true;
                    //tacheToolStripMenuItem.Visible = true;
                    //maintenanceToolStripMenuItem.Visible = true;
                    utilisateurToolStripMenuItem.Visible = true;
                    personnelToolStripMenuItem.Visible = true;
                    gestionSalarialToolStripMenuItem.Visible = true;
                    gallerieToolStripMenuItem.Visible = true;
                    enregistrementPersonnelToolStripMenuItem.Visible = false;
                    divisionToolStripMenuItem.Visible = false;
                    directionToolStripMenuItem.Visible = false;
                }
                if (LoginFrm.typeUtilisateur == "utilisateur")
                {
                    //gestionDesDocumentsToolStripMenuItem.Visible = true;
                    personnelToolStripMenuItem.Visible = true;
                    departPersonnelToolStripMenuItem.Visible = false;
                    enregistrementPersonnelToolStripMenuItem.Enabled = false;
                    divisionToolStripMenuItem.Enabled = false;
                    directionToolStripMenuItem.Enabled = false;
                }
            }
        }

        private void groupBox3_Paint(object sender, PaintEventArgs e)
        {
            var mGraphics = e.Graphics;
            var pen1 = new Pen(Color.SteelBlue, 2);
            var area1 = new Rectangle(0, 0, this.groupBox3.Width - 1, this.groupBox3.Height - 1);
            var linearGradientBrush = new LinearGradientBrush(area1,
                SystemColors.Control, SystemColors.Control, LinearGradientMode.Horizontal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);

        }

        private void groupBox10_Paint(object sender, PaintEventArgs e)
        {
            var mGraphics = e.Graphics;
            var pen1 = new Pen(Color.White, 0);
            var area1 = new Rectangle(0, 0, this.groupBox10.Width - 1, this.groupBox10.Height - 1);
            var linearGradientBrush = new LinearGradientBrush(area1,
               Color.SteelBlue, Color.DodgerBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }


        private void btnDirection_Click(object sender, EventArgs e)
        {
            var frm = new ServiceFrm();
            frm.ShowDialog();
        }

        void ActiverLesControles()
        {
            if (LoginFrm.typeUtilisateur == "admin")
            {
                //btnDirmation.Enabled = true;
                //btnIImprimante.Enabled = true;
                //btnJournal.Enabled = true;
                //btnListePersonnel.Enabled = true;
                //btnModifierPerection.Enabled = true
                //btnForsonnel.Enabled = true;
                //btnPaiement.Enabled = true;
                //linkLabel1.Visible = true;
                //btnQuittance.Enabled = true;
                //btnAdministrer.Enabled = true;
                //btnBon.Enabled = true;btnFactures.Enabled=true;
                //btnEngagement.Enabled = true;
            }
            else if (LoginFrm.typeUtilisateur.ToLower() == "drh")
            {

            }
            else if (LoginFrm.typeUtilisateur == "compta")
            {

            }
            else if (LoginFrm.typeUtilisateur == "caisse")
            {

            }
        }

        void DesactiverLesControles()
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var frm = new ModifierMotPasse();

            frm.ShowDialog();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            var path = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10) + "\\log\\log_erreur.txt";
            if (System.IO.File.Exists(path))
            {
                System.Diagnostics.Process.Start(path);
            }
        }

        private void button2_Click_2(object sender, EventArgs e)
        {

            groupBox2.Size = new System.Drawing.Size(700, 500);
            groupBox2.Location = new Point(2, Height - 510);
            this.groupBox2.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox2_Paint);
            button2.BackColor = Color.FromArgb(0, 0, 64);
            lblUtilisateur.Location = new Point(500, 255);
            groupBox2.Visible = true;
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            button2.Text = "Acceuil";
            button2.BackColor = Color.Blue;
        }

        private void button2_MouseLeave(object sender, EventArgs e)
        {
            button2.Text = "";
            button2.BackColor = Color.Transparent;
        }

        private void button2_MouseMove(object sender, MouseEventArgs e)
        {
            button2.Text = "Acceuil";
            button2.BackColor = Color.Blue;
        }

        private void MainForm_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
        }

        //se connecter
        private void seConnecterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                groupBox2.Visible = false;
                if (LoginFrm.ShowBox() == "1")
                {
                    SeConnecter();
                    seDeconnecterToolStripMenuItem.Enabled = true;
                    seConnecterToolStripMenuItem.Enabled = false;
                    lblUtilisateur.Text = LoginFrm.nomUtilisateur;
                    if (LoginFrm.typeUtilisateur != "utilisateur")
                    {
                        var listeFinContrat = ConnectionClass.ListeAlerteFinContrat();
                        if (listeFinContrat.Count > 0)
                        {
                            if (GestionPharmacetique.Forme.AlerteFinContraCtrl.ShowBox(this) == "1")
                            {

                            }
                        }

                        var listeAvancement = ConnectionClass.AlerteAvancement();
                        if (listeAvancement.Count > 0)
                        {
                            if (AlerteEchelon.ShowBox(this) == "1")
                            {

                            }
                        }
                        ConnectionClass.MettreAJourAnciennete();
                    }
                    var photo = LoginFrm.photo;
                    if (System.IO.File.Exists(photo))
                    {
                        pictureBox4.Image = Image.FromFile(photo);
                    }
                    else
                    {
                        pictureBox4.Image = null;
                    }
                    ActiverLesControles();
                    groupBox2.Visible = false;
                }
            }
            catch (Exception)
            {
                
            }
        }

        private void seDeconnecterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MonMessageBox.ShowBox("Voulez vous vous deconnecter ?", "Confirmation") == "1")
            {
                SeDeconnecter();
                groupBox2.Visible = false;
            }
        }

        private void enregistrementPersonnelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new ServiceFrm();
            groupBox2.Visible = false;
            frm.Size = new Size(Width - 15, Height - 145);
            frm.Location = new Point(5, 145);
            frm.ShowDialog();
        }
        private void quittanceToolStripMenuItem9_Click(object sender, EventArgs e)
        {
            var frm = new GestionPharmacetique.Forme.DepenseFrm();
            groupBox2.Visible = false;
            frm.Size = new Size(Width - 15, Height - 145);
            frm.Location = new Point(5, 145);
            frm.ShowDialog();
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            var frm = new DetailDocumentFrm();
            frm.stateReglement = 1;
            frm.Size = new Size(Width - 15, Height - 145); 
            frm.Location = new Point(5, 145);
            frm.ShowDialog();
        }
        
        private void bulletinDePaieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new ListeAvanceSurSalaireFrm();
            frm.Location = new Point(5, 145);
            frm.Size = new Size(Width - 15, Height - 150);
            frm.ShowDialog();
        }

        private void etatDeSalaireToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new ListePaiemenFrm();
            groupBox2.Visible = false;
            frm.Location = new Point(5, 120);
            frm.ShowDialog();
        }

        private void procederAuPayeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new SGSP.Formes.PayeFrm();
            groupBox2.Visible = false;
            frm.Size = new Size(Width - 15, Height - 150);
            frm.Location = new Point(5, 145);
            frm.ShowDialog();
        }

        private void avanceSurSalaireToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new ListeAccompteFrm();
            groupBox2.Visible = false;
            frm.Location = new Point(5, 145);
            frm.Size = new Size(Width - 15, Height - 150);
            frm.ShowDialog();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            Dispose();
        }


        private void enregistrementUtilisateurToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new UtilisateurFrm();
            groupBox2.Visible = false;
            frm.Location = new Point(5, 140);
            frm.ShowDialog();
        }
  private void insererImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Controls.Remove(groupBox3);
            var ctrl = new SGDP.Formes.FournisseurCtrl();
            ctrl.Location = new Point(5, 120);
            ctrl.Size = new Size(Width - 15, Height - 140);
            Controls.Add(ctrl);
            //button2.Visible = false;
            groupBox2.Visible = false;
        }

        private void documentsEntrantsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var frm = new ListeDocumentFrm();
            frm.categorieDoc = "Documents entrants";
            frm.Size = new Size(Width - 15, Height - 145); groupBox2.Visible = false;
            frm.Location = new Point(5, 145);
            frm.ShowDialog();
        }

        private void documentsEntrantsToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            var frm = new ListeDocumentFrm();
            frm.categorieDoc = "Documents sortants"; groupBox2.Visible = false;
            frm.Size = new Size(Width - 15, Height - 145);
            frm.Location = new Point(5, 145);
            frm.ShowDialog();
        }

        private void autresDocumentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new ListeDocumentFrm();
            frm.categorieDoc = "Autres documents";
            frm.Size = new Size(Width - 15, Height - 145); 
            groupBox2.Visible = false;
            frm.Location = new Point(5, 145);
            frm.ShowDialog();
        }

        private void autorisationDabsenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new AbsenceFrm();
            frm.Size = new Size(Width - 15, Height - 145);
            groupBox2.Visible = false;
            frm.Location = new Point(5, 145);
            frm.ShowDialog();
        }

        private void demandeDeCongéToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new CongeFrm();
            frm.Size = new Size(Width - 15, Height - 145);
            groupBox2.Visible = false;
            frm.Location = new Point(5, 145);
            frm.ShowDialog();
        }

        private void administrerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new SGSP.Formes.ModifierMotPasse();
            frm.textBox1.Text = GestionAcademique.LoginFrm.nomUtilisateur; groupBox2.Visible = false;
            frm.ShowDialog();
        }

        private void etatDeDepensesrecettesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new EtatDepenseRecetteFrm();
            groupBox2.Visible = false;
            frm.Location = new Point(5, 145);
            frm.Size = new Size(Width - 15, Height - 150);
            frm.ShowDialog();
        }

        private void journalCaisseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new JournalDeCaisseFrm();
            groupBox2.Visible = false;
            frm.Location = new Point(5, 145);
            frm.Size = new Size(Width - 15, Height - 150);
            frm.ShowDialog();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
        }

        private void gallerieToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void bilanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
            var frm = new BilanFrm();
            frm.Location = new Point(5, 140);
            frm.Height = Height - 150;
            frm.ShowDialog();
        }

        private void formationToolStripMenuItem_Click(object sender, EventArgs e)
        {

            groupBox2.Visible = false;
            var frm = new FormationFrm();
            frm.Location = new Point(5, 140);
            frm.Size = new Size(Width - 15, Height - 150);
            frm.ShowDialog();
        }

        private void fraisCongéToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new SGDP.Formes.BanqueFrm();
            groupBox2.Visible = false;
            frm.ShowDialog();
        }

        private void directionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new DirectionFrm();
            groupBox2.Visible = false;
            frm .ShowDialog();
        }

        private void divisionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new DivisionFrm();
            //groupBox2.Visible = false;
            //frm.Location = new Point(5, 145);
            //frm.Size = new Size(Width - 15, Height - 150);
            frm.ShowDialog();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void gestionDesÉtudiantsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
            var frm = new  EtudiantListeFrm();
            frm.Location = new Point(5, 140);
            frm.Size = new Size(Width - 15, Height - 150);
            frm.ShowDialog();
        }

        private void gestionDesStaigiairesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
            var frm = new StagiaireListeFrm();
            frm.Location = new Point(5, 140);
            frm.Size = new Size(Width - 15, Height - 150);
            frm.ShowDialog();
        }

        private void listeProjetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
            var frm = new PartenaireFrm();
            frm.Location = new Point(5, 140);
            frm.Size = new Size(Width - 15, Height - 150);
            frm.ShowDialog();
        }

        private void personnelProjetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new ListePersonnelProjetFrm();
            groupBox2.Visible = false;
            frm.Location = new Point(5, 140);
            frm.Size = new Size(Width - 15, Height - 140);
            frm.ShowDialog();
        }

        private void gesionDesProjetsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new ProjetFrm();
            groupBox2.Visible = false;
            frm.ShowDialog();
        }

        private void missionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new MissionFrm();
            groupBox2.Visible = false;
            frm.Location = new Point(3, 140);
            frm.Size = new Size(Width - 15, Height - 140);
            frm.ShowDialog();
        }

        private void demandeDemploiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new DemandeEmploiFrm();
            groupBox2.Visible = false;
            frm.Location = new Point(3, 140);
            frm.Size = new Size(Width - 15, Height - 140);
            frm.ShowDialog();
        }

        private void alerteDepartPersonnelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
            var listeFinContrat = ConnectionClass.ListeAlerteFinContrat();
            if (listeFinContrat.Count > 0)
            {
                if (GestionPharmacetique.Forme.AlerteFinContraCtrl.ShowBox(this) == "1")
                {

                }
            }
                    }

        private void alerteAvancementPersonnelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
            var listeAvancement = ConnectionClass.AlerteAvancement();
            if (listeAvancement.Count > 0)
            {
                if (AlerteEchelon.ShowBox(this) == "1")
                {

                }
            }
        }

        private void alerteFinDeStageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                groupBox2.Visible = false;
                var AlerteStages = ConnectionClass.AlerteStages();
                if (AlerteStages.Count > 0)
                {
                    if (AlerteFinStageCtrl.ShowBox(this) == "1")
                    {

                    }
                }
            }
            catch { }
        }

        private void alerteFinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                groupBox2.Visible = false;
                var AlerteFinEtude= ConnectionClass.ListeInformationEtudiant();
                if (AlerteFinEtude.Count > 0)
                {
                    if (AlerteFinEtudeFrm.ShowBox(this) == "1")
                    {

                    }
                }
            }
            catch { }
        }

        private void personnelActiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new ListeDesPersonelsFrm();
            groupBox2.Visible = false;
            frm.Location = new Point(5, 140);
            frm.state = "0";
            frm.Size = new Size(Width - 15, Height - 140);
            frm.ShowDialog();
        }

        private void departPersonnelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new ListeDesPersonelsFrm();
            groupBox2.Visible = false;
            frm.Location = new Point(5, 140);
            frm.state = "1";
            frm.Size = new Size(Width - 15, Height - 140);
            frm.ShowDialog();
        }

        private void alerteRepriseAprèsCongéToolStripMenuItem_Click(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
            var liste = from l in ConnectionClass.ListeConge()
                        where l.DateRetour <= DateTime.Now.AddDays(7)
                        orderby l.IDConge descending
                        select l;
            if (liste.Count() > 0)
            {
                if (SGSP.Formes.AlerteCongeCtrl.ShowBox(this) == "1")
                {

                }
            }
            
        }

        private void tableauDeBordRHToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new TBRHFrm();
            frm.Location = new Point(5, 145);
            frm.Size = new Size(Width - 15, Height - 150);
            frm.ShowDialog();
        }

        private void tableauDeBordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frm = new TBActivitesFrm();
            frm.Location = new Point(5, 145);
            frm.Size = new Size(Width - 15, Height - 150);
            frm.ShowDialog();
        }

    }
}
