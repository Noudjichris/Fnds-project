using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SGSP.Formes
{
    public partial class ListePerso : Form
    {
        public ListePerso()
        {
            InitializeComponent();
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.Silver, 0);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                SystemColors.Control, SystemColors.Control, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        public static bool siPersonnelProjet;
        private void ListePerso_Load(object sender, EventArgs e)
        {
            dataGridView2.Focus();
            ListeEmploye();
        }
        public static string btnClick, mois,state, indiceAnciennete, fonction, indexRecherche, compteBancaire,banque, numerMatricule, nomPersonnel, etatContrat, etatRetraite, typeContrat;
        public static ListePerso frm;
        public static DateTime datePriseService;
        public static int exercice, numeroPaiement;
        public static AppCode.Paiement paiement;
        public static string ShowBox()
        {
            frm = new ListePerso();
            frm.ShowDialog();
            return btnClick;
        }
        private void ListeEmploye()
        {
            try
            {

                dataGridView2.Rows.Clear();
                if (siPersonnelProjet)
                {
                    var personnels = AppCode.ConnectionClass.ListePersonnelProjet(indexRecherche);
                    foreach (var p in personnels)
                    {
                        string bank = "", compte = "";
                        var services = from s in AppCode.ConnectionClass.ListeServicePersonnelProjet(p.IDPersonelProjet)
                                       join pr in AppCode.ConnectionClass.ListeDesProjets()
                                           on s.IDProjet equals pr.NumeroProjet
                                       where pr.DateFin>=DateTime.Now
                                       select new { s.Anciennete, s.Poste, s.DateDepart, s.DateService, s.TypeContrat, s.Status, s.SiCNRT };
                        foreach (var service in services)
                        {
                            var banques = from b in AppCode.ConnectionClass.ListeDonneesBancaires(p.NumeroMatricule)
                                          where b.EtatParDefaut == true
                                          select b;
                            foreach (var b in banques)
                            {
                                bank = b.NomBanque;
                                compte = b.Compte;
                            }
                            var IndiceAnciennete = service.Anciennete;
                            if (IndiceAnciennete.Contains("%"))
                            {
                                IndiceAnciennete = IndiceAnciennete.Remove(IndiceAnciennete.IndexOf("%"));
                            }
                            dataGridView2.Rows.Add(
                           p.IDPersonelProjet,
                            p.Nom + " " + p.Prenom,
                             service.Poste,
                             service.TypeContrat,
                             IndiceAnciennete,
                             service.DateService.ToShortDateString(),
                             service.DateDepart.ToShortDateString(),
                             service.Status,
                             bank, compte, service.SiCNRT
                             );
                            if (AppCode.ConnectionClass.StatusContrat(service.Status))
                            {
                                dataGridView2.Rows[dataGridView2.Rows.Count].DefaultCellStyle.BackColor = Color.Red;  //
                                dataGridView2.Rows[dataGridView2.Rows.Count].DefaultCellStyle.ForeColor = Color.White;
                            }
                        }
                    }
                }
                else
                {
                    var personnels = AppCode.ConnectionClass.ListePersonnel(indexRecherche);
                    foreach (var p in personnels)
                    {
                        string bank = "", compte = "";
                        var service = AppCode.ConnectionClass.ListeServicePersonnel(p.NumeroMatricule);
                        var banques = from b in AppCode.ConnectionClass.ListeDonneesBancaires(p.NumeroMatricule)
                                      where b.EtatParDefaut == true
                                      select b;
                        foreach (var b in banques)
                        {
                            bank = b.NomBanque;
                            compte = b.Compte;
                        }
                        var IndiceAnciennete = service[0].Anciennete;
                        if (IndiceAnciennete.Contains("%"))
                        {
                            IndiceAnciennete = IndiceAnciennete.Remove(IndiceAnciennete.IndexOf("%"));
                        }
                        dataGridView2.Rows.Add(
                       p.NumeroMatricule,
                        p.Nom + " " + p.Prenom,
                         service[0].Poste,
                         service[0].TypeContrat,
                         IndiceAnciennete,
                         service[0].DateService.ToShortDateString(),
                         service[0].DateDepart.ToShortDateString(),
                         service[0].Status,
                         bank, compte, service[0].SiCNRT
                         );
                        if (AppCode.ConnectionClass.StatusContrat(service[0].Status))
                        {
                            dataGridView2.Rows[dataGridView2.Rows.Count].DefaultCellStyle.BackColor = Color.Red;  //
                            dataGridView2.Rows[dataGridView2.Rows.Count].DefaultCellStyle.ForeColor = Color.White;
                        }
                    }
                }

            }
            catch { }
        }

        private void dataGridView2_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView2.SelectedRows.Count > 0)
                {
                    numerMatricule = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
                    nomPersonnel = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
                    fonction = dataGridView2.SelectedRows[0].Cells[2].Value.ToString();
                    typeContrat = dataGridView2.SelectedRows[0].Cells[3].Value.ToString();
                    indiceAnciennete = dataGridView2.SelectedRows[0].Cells[4].Value.ToString();
                    datePriseService =DateTime.Parse( dataGridView2.SelectedRows[0].Cells[5].Value.ToString());
                        //state = dataGridView2.SelectedRows[0].Cells[7].Value.ToString();
                    banque = dataGridView2.SelectedRows[0].Cells[8].Value.ToString();
                    compteBancaire = dataGridView2.SelectedRows[0].Cells[9].Value.ToString();
                    if (AppCode.ConnectionClass.StatusContrat(dataGridView2.SelectedRows[0].Cells[7].Value.ToString()))
                    {
                        GestionPharmacetique.MonMessageBox.ShowBox("Le contrat de cet employé est deja à terme", "Erreur");
                            btnClick = "2"; Dispose();
                    }
                        else
                        {
                            if (state == "1")
                            {
                                PaiementForme.ancienneteDuPersonnel = indiceAnciennete;
                                PaiementForme.nomEmploye = nomPersonnel;
                                PaiementForme.numMatricule = numerMatricule;
                                PaiementForme.numeroCompte = compteBancaire;
                                PaiementForme.fonction = fonction;
                                PaiementForme.mois = mois;
                                PaiementForme.exercice = exercice;
                                PaiementForme.numeroPaiement = numeroPaiement;
                                PaiementForme.datePriseService = datePriseService;
                                PaiementForme.typeContrat = typeContrat;
                                PaiementForme.etatModifier = "0";
                                PaiementForme.banque = banque;
                                PaiementForme.siCNRT = Boolean.Parse(dataGridView2.SelectedRows[0].Cells[10].Value.ToString());
                                if (PaiementForme.ShowBox())
                                {
                                    paiement = PaiementForme.paiement;
                                    btnClick = "1";
                                }
                                else
                                {
                                    btnClick = "0";
                                }
                            }
                            else
                            {
                                //nomPersonnel = 
                                btnClick = "1";
                            }
                        }
                    }
                    else
                    {
                        btnClick = "1";
                    }
                    Dispose();
                   
                
            }
            catch { }
        }

        private void dataGridView2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dataGridView2_DoubleClick(null, null);
            }
        }

        private void btnFermer_Click(object sender, EventArgs e)

        {
            btnClick = "2";
            Dispose();
        }

        private void txtRef_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                indexRecherche = txtRef.Text;
                ListeEmploye();
                dataGridView2.Focus();
            }
        }

        private void txtRef_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
