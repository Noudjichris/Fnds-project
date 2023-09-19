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
    public partial class ListeFinEtudesFrm : Form
    {
        public ListeFinEtudesFrm()
        {
            InitializeComponent();
        }

        private void ListeFinEtudesFrm_Load(object sender, EventArgs e)
        {
            foreach (var et in AppCode.ConnectionClass.ListeInformationEtudiant())
            {
                var etudiant=AppCode.ConnectionClass.ListeDesEtudiants(et.NumeroEtudiant);
                dgvAcompte.Rows.Add(etudiant[0].Nom + " " + etudiant[0].Prenom, AppCode.ConnectionClass.ListeDesEtablissements(et.NumeroInstitution)[0].NomEtablissment,
                et.Niveau, et.Discipline, et.AnneeEntree, et.AnneeSortie, et.AnneeScolaire);
            }
        }
    }
}
