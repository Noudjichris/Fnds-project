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
    public partial class DocumentFrm : Form
    {
        public DocumentFrm()
        {
            InitializeComponent();
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.Silver, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.SteelBlue, Color.DarkSlateBlue, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void groupBox3_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(SystemColors.ControlLightLight, 2);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox3.Width - 1, this.groupBox3.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                SystemColors.Control, SystemColors.Control, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void FacturesFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.Silver, 0);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                SystemColors.Control, SystemColors.Control, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            Close();
        }

        public static string etat, typeDocument, categorieDoc;
        public static DocumentFrm frm;
        public static string btnClick;
        public static int numero, idDocument;
        public static string ShowBox()
        {
            try
            {
                frm = new DocumentFrm();
                frm.ShowDialog();
            }
            catch { }
            return btnClick ;
        }
        private void btnValider_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtNo.Text))
                { return; }
                
                if (string.IsNullOrEmpty(txtRef.Text))
                {
                    GestionPharmacetique.MonMessageBox.ShowBox("Veuillez entrer le numero de reference du document", "Erreur");
                    return;
                }
                double exercice, th=0,ttc=0, tva=0;
                if(double.TryParse(cmbAnnne.Text, out exercice))
                {
                }
                else
                {
                    GestionPharmacetique.MonMessageBox.ShowBox("Veuillez entrer un chiffre valide pour l'année d'exercice", "Erreur");
                    return;
                }
              

                var document = new AppCode.Document();
                document.EcheanceLivraison = DateTime.Now.Date;
                document.Exercice = Convert.ToInt32(cmbAnnne.Text);
                document.MontantHT = th;
                document.MotCle = txtMotCle.Text;
                document.MontantTTC = ttc;
                document.TVA = tva;
                document.DateEnregistrement = dateTimePicker1.Value;
                document.ReferenceDocument = txtRef.Text;
                document.NumeroDocument = numero;
                document.RootPathDocument = txtDesignation.Text;
                document.CategorieDocument = categorieDoc;
                document.EcheancePaiement = DateTime.Now.Date;
                document.IDTypeDocument = Convert.ToInt32(txtNo.Text);
              
                    document.Payable = false;
              
                    document.ModalitePaiement = "";
                
                var listeFourn = from f in AppCode.ConnectionClass.ListeFournisseur()
                                 where f.NomFournisseur == cmbFournisseur.Text
                                 select f.ID;
                foreach (var f in listeFourn)
                    document.NumeroTiers = f;

                var listeType = from f in AppCode.ConnectionClass.ListeDesTypesDocuments()
                                 where f.TypeDocument == cmbTypeDoc.Text
                                 select f.NumeroType;
                foreach (var f in listeType)
                    document.NumeroType = f;
                if(AppCode.ConnectionClass.EnregistrerUnDocument(document,etat))
                {
                    btnClick ="1";
                    typeDocument = cmbTypeDoc.Text;
                    if (!string.IsNullOrEmpty(txtDesignation.Text))
                    {
                        var rootPath = AppCode.GlobalVariable.rootPathDocuments +  cmbTypeDoc.Text;
                        if (!System.IO.Directory.Exists(rootPath))
                        {
                            System.IO.Directory.CreateDirectory(rootPath);
                        }
                        rootPath = rootPath + "\\" + System.IO.Path.GetFileName(txtDesignation.Text);
                        if (!System.IO.File.Exists(rootPath))
                        {
                            System.IO.File.Copy(txtDesignation.Text, rootPath);
                        }
                       
                    }
                    Dispose();
                }
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("Enregistrer document",ex);
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Dispose();
        }
      
        private void button3_Click(object sender, EventArgs e)
        {
            var frm=new  SGDP.Formes.FournisseurFrm();
            frm.ShowDialog();
        }

        private void FacturesFrm_Load(object sender, EventArgs e)
        {
            try
            {
                cmbAnnne.Items.Clear();
                for (var i = 2017; i < DateTime.Now.Year + 10; i++)
                {
                    cmbAnnne.Items.Add(i.ToString());
                }
                cmbAnnne.Text = DateTime.Now.Year.ToString();
                foreach (var t in ConnectionClass.ListeDesTypesDocuments())
                {
                    cmbTypeDoc.Items.Add(t.TypeDocument);
                }
                if (etat == "1")
                {
                   var id  = AppCode.ConnectionClass.DernierDuDocument(idDocument, categorieDoc) + 1;
                   txtNo.Text = id.ToString();
                   if (id < 10)
                   {
                       txtRef.Text = "0000"+id + "/" + DateTime.Now.Year;
                   }
                   else if (id>=10 && id < 100)
                   {
                       txtRef.Text = "000" + id + "/" + DateTime.Now.Year;
                   }
                   else if (id >= 100 && id < 1000)
                   {
                       txtRef.Text = "00" + id + "/" + DateTime.Now.Year;
                   }
                   else if (id >= 1000 && id < 10000)
                   {
                       txtRef.Text = "0" + id + "/" + DateTime.Now.Year;
                   }
                   else
                   {
                       txtRef.Text =  id + "/" + DateTime.Now.Year;
                   }
                    cmbAnnne.Text = DateTime.Now.Year.ToString();
                   cmbTypeDoc.Text = typeDocument;
                }
                else if (etat == "2")
                {
                    var liste =from d in AppCode.ConnectionClass.ListeDesDocuments(categorieDoc)
                               join t in AppCode.ConnectionClass.ListeDesTypesDocuments()
                               on d.NumeroType equals t.NumeroType
                               join f in AppCode.ConnectionClass.ListeFournisseur()
                               on d.NumeroTiers equals f.ID
                               where d.NumeroDocument== numero
                               select new
                               {
                                   Date = d.EcheanceLivraison,
                                   d.DateEnregistrement,
                                   d.Description,
                                   d.Exercice,
                                   d.MontantHT,
                                   d.MontantTTC,
                                   d.NumeroDocument,
                                   d.NumeroTiers,
                                   d.NumeroType,
                                   d.ReferenceDocument,
                                   d.RootPathDocument,
                                   d.TVA,
                                   f.ID,
                                   f.NomFournisseur,
                                   t.TypeDocument,
                                   d.Payable,
                                   d.MotCle
                                   
                               };
                    foreach (var d in liste)
                    {
                      
                        txtDesignation.Text = d.RootPathDocument;
                        cmbAnnne.Text = d.Exercice.ToString();
                     
                        txtNo.Text = numero.ToString();
                        txtRef.Text = d.ReferenceDocument;
                        cmbFournisseur.Text = d.NomFournisseur;
                        cmbTypeDoc.Text = d.TypeDocument;
                        txtMotCle.Text = d.MotCle;
                    }

                }

                ListeForunisseur();
            }
            catch { }
        }

        void ListeForunisseur()
        {
            try
            {
               

               
                cmbFournisseur.Items.Clear();
                var liste = from l in SGSP.AppCode.ConnectionClass.ListeFournisseur()
                            where l.ID > 0
                            where l.NomFournisseur.StartsWith(cmbFournisseur.Text, StringComparison.CurrentCultureIgnoreCase)
                            orderby l.NomFournisseur
                            select l;
                foreach (var l in liste)
                {
                    cmbFournisseur.Items.Add(l.NomFournisseur);
                }
            }
            catch { }
        }
        private void cmbFournisseur_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ListeForunisseur();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(cmbTypeDoc.Text))
                {
                    var open = new OpenFileDialog();
                    //open.InitialDirectory = Application.StartupPath.Substring(0, Application.StartupPath.Length - 10);
                    open.Filter = "Tous les fichiers (all files(*.*)|*.*";
                    open.FilterIndex = 1;
                    if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                       
                        var rootPath = AppCode.GlobalVariable.rootPathDocuments + "//" + cmbTypeDoc.Text;
                       
                        rootPath = rootPath + "//" + System.IO.Path.GetFileName(txtDesignation.Text);
                        if (System.IO.File.Exists(rootPath) && etat=="1")
                        {
                            GestionPharmacetique.MonMessageBox.ShowBox("Ce document existe deja, Veuillez le renommer avant de proceder au transfert.", "Information");
                            return;
                        }
                        txtDesignation.Text = open.FileName;
                        //fileName=
                    }
                }else
                {GestionPharmacetique.
                    MonMessageBox.ShowBox("Veuillez selectionner le type de document, puis réessayez ", "Information Photo");
                }
            }

            catch (Exception ex) { GestionPharmacetique.MonMessageBox.ShowBox("Transfert de l'image", ex); }
        }
        

    }
}
