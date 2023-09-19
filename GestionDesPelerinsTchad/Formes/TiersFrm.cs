using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SGSP.Formes
{
    public partial class TiersFrm : Form
    {
        public TiersFrm()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            flag = false;
            Dispose();
        }

        private void TiersFrm_Load(object sender, EventArgs e)
        {
            
        }
        public static TiersFrm frm;
       static  bool flag;
       public static string nomTiers; public static int numeroTiers;
            public static bool Showbox()
            {
                frm = new TiersFrm();
                frm.ShowDialog();
                return flag;
            }
        void ListeFournisseur()
        {
            try
            {
                dgvFournisseur.Rows.Clear();
                var liste = from l in SGSP.AppCode.ConnectionClass.ListeFournisseur()
                            where l.ID > 0
                            where l.NomFournisseur.ToUpper().Contains(txtRef.Text.ToUpper())
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

        private void txtRef_TextChanged(object sender, EventArgs e)
        {
            ListeFournisseur();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
               SGDP.Formes. FournisseurFrm.ID =AppCode. ConnectionClass.ObtenirLeDernierNumeroFournisseur() + 1;
               SGDP.Formes.FournisseurFrm.etat = 1;
               if (SGDP.Formes.FournisseurFrm.ShowBox())
                {
                    ListeFournisseur();
                }
            }
            catch (Exception ex) { GestionPharmacetique.MonMessageBox.ShowBox("Enregistrer fournisseur", ex); }
        }

        private void dgvFournisseur_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                nomTiers = dgvFournisseur.SelectedRows[0].Cells[3].Value.ToString();
                numeroTiers =Convert.ToInt32( dgvFournisseur.SelectedRows[0].Cells[0].Value.ToString());
                flag= true;
                Dispose();
            }
            catch { }
        }

       
    }
}
