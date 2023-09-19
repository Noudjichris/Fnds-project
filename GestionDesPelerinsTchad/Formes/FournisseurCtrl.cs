using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SGSP.AppCode;

namespace SGDP.Formes
{
    public partial class FournisseurCtrl : UserControl
    {
        public FournisseurCtrl()
        {
            InitializeComponent();
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(SystemColors.ControlLight, 3);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, Color.SteelBlue
                , SystemColors.ControlLight, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void LaboFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(SystemColors.ControlLight, 3);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1, SystemColors.ControlLight
                , SystemColors.ControlLight, LinearGradientMode.BackwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void LaboFrm_Load(object sender, EventArgs e)
        {
            btnExit.Location = new Point(Width - 55, btnExit.Location.Y);
            label3.Location = new Point((Width - label3.Width) / 2, label3.Location.Y);
            ListeFournisseur();
        }

        void ListeFournisseur()
        {
            try
            {
                dgvFournisseur.Rows.Clear();
                var liste = from l in SGSP.AppCode.ConnectionClass.ListeFournisseur()
                            where l.ID>0
                            where l.NomFournisseur.StartsWith(txtRechercher.Text, StringComparison.CurrentCultureIgnoreCase)
                            orderby l.NomFournisseur
                            select l;
                foreach (var l in liste)
                {
                    dgvFournisseur.Rows.Add
                        (l.ID,
                            l.Reference,
                            l.Type,
                            l.NomFournisseur,
                            l.Adresse,
                            l.Telephone1,
                            l.Telephone2,
                            l.Telecopie,
                            l.Email,
                            l.FAX,
                            l.NumeroPostal,
                            l.Ville,
                            l.Pays,
                            l.NoCompte,
                            l.NIF,
                            l.Commentaire
                    );
                }
            }
            catch (Exception Exception) {GestionPharmacetique. MonMessageBox.ShowBox("Liste ", Exception); }
        }
       
       

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                FournisseurFrm. ID = ConnectionClass.ObtenirLeDernierNumeroFournisseur() + 1;
                FournisseurFrm.etat = 1;
                if(FournisseurFrm.ShowBox())
                {
                    ListeFournisseur();
                }
            }
            catch (Exception ex) {GestionPharmacetique. MonMessageBox.ShowBox("Enregistrer fournisseur", ex); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvFournisseur.SelectedRows.Count > 0)
                {
                    FournisseurFrm.ID = Convert.ToInt32(dgvFournisseur.SelectedRows[0].Cells[0].Value.ToString());
                    FournisseurFrm.etat = 2;
                    if (FournisseurFrm.ShowBox())
                    {
                        ListeFournisseur();
                    }
                }
            }
            catch { }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            try
            {
                if (dgvFournisseur.SelectedRows.Count > 0)
                {
                    if (GestionPharmacetique. MonMessageBox.ShowBox("Voulez vous supprimer les données de ce fournisseur?", "Confirmation") == "1")
                    {
                        var fournisseur = new Fournisseur();
                        fournisseur.ID = Convert.ToInt32(dgvFournisseur.SelectedRows[0].Cells[0].Value.ToString());
                        if (ConnectionClass.SupprimerFournisseur(fournisseur))
                        {
                            ListeFournisseur();
                        }
                    }
                }
            }
            catch { }
        }

        private void txtRechercher_TextChanged(object sender, EventArgs e)
        {
            ListeFournisseur();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Visible = false;
        }

        private void cmbDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dgvFournisseur.Rows.Clear();
                var liste = from l in SGSP.AppCode.ConnectionClass.ListeFournisseur()
                            where l.ID > 0
                            where l.Type.StartsWith(cmbDivision.Text, StringComparison.CurrentCultureIgnoreCase)
                            orderby l.NomFournisseur
                            select l;
                foreach (var l in liste)
                {
                    dgvFournisseur.Rows.Add
                        (l.ID,
                            l.Reference,
                            l.Type,
                            l.NomFournisseur,
                            l.Adresse,
                            l.Telephone1,
                            l.Telephone2,
                            l.Telecopie,
                            l.Email,
                            l.FAX,
                            l.NumeroPostal,
                            l.Ville,
                            l.Pays,
                            l.NoCompte,
                            l.NIF,
                            l.Commentaire
                    );
                }
            }
            catch (Exception Exception) { GestionPharmacetique.MonMessageBox.ShowBox("Liste ", Exception); }
        }

       
    }
}
