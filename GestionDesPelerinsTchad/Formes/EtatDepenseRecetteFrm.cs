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
    public partial class EtatDepenseRecetteFrm : Form
    {
        public EtatDepenseRecetteFrm()
        {
            InitializeComponent();
        }

        private void EtatDepenseRecetteFrm_Load(object sender, EventArgs e)
        {
            btnFermer.Location = new Point(Width - 45, btnFermer.Location.Y);
            var items = new string[] { "<<Selectionner type rapport>>", "Premier trimestre", "Deuxième trimestre", "Troisième trimestre","Quatrième trimestre", "Premier semestre", "Deuxième semestre", "Mensuel", "Annuel" };

            cmbRapport.Items.AddRange(items);
            for (var i = 2017; i < DateTime.Now.Year + 10; i++)
            {
                cmbAnne.Items.Add(i.ToString());
            }
            cmbRapport.Text = "<<Selectionner type rapport>>";
            cmbAnne.Text = DateTime.Now.Year.ToString();
            var mois = DateTime.Now.ToLongDateString();
            mois = mois.Remove(mois.LastIndexOf(" "), 5);
            mois = mois.Substring(mois.LastIndexOf(" ") + 1);
            cmbMois.Text = mois;
        }
        DateTime dateDebut, dateFin;
        string ObtenirMois(int mois)
        {
            switch (mois)
            {
                case 1:
                    return "Janvier";
                case 2:
                    return "Février";
                case 3:
                    return "Mars";
                case 4:
                    return "Avril";
                case 5:
                    return "Mai";
                case 6:
                    return "Juin";
                case 7:
                    return "Juillet";
                case 8:
                    return "Août";
                case 9:
                    return "Septembre";
                case 10:
                    return "Octobre";
                case 11:
                    return "Novembre";
                case 12:
                    return "Decembre";
                default:
                    return "";
            };
        }

        DateTime ObtenirDebutJour(int mois)
        {
            return DateTime.Parse("01/" + mois + "/" + dateDebut.Year);
        }

        DateTime ObtenirFinJour(int mois)
        {
            if (mois == 1)
            {
                return DateTime.Parse("31/" + mois + "/" + dateDebut.Year);
            }
            else if (mois == 2)
            {
                if (DateTime.IsLeapYear(dateDebut.Year))
                    return DateTime.Parse("29/" + mois + "/" + dateDebut.Year);
                else
                    return DateTime.Parse("28/" + mois + "/" + dateDebut.Year);
            }
            else if (mois == 3)
            {
                return DateTime.Parse("31/" + mois + "/" + dateDebut.Year);
            }
            else if (mois == 4)
            {
                return DateTime.Parse("30/" + mois + "/" + dateDebut.Year);
            }
            else if (mois == 5)
            {
                return DateTime.Parse("31/" + mois + "/" + dateDebut.Year);
            }
            else if (mois == 6)
            {
                return DateTime.Parse("30/" + mois + "/" + dateDebut.Year);
            }
            else if (mois == 7)
            {
                return DateTime.Parse("31/" + mois + "/" + dateDebut.Year);
            }
            else if (mois == 8)
            {
                return DateTime.Parse("31/" + mois + "/" + dateDebut.Year);
            }
            else if (mois == 9)
            {
                return DateTime.Parse("30/" + mois + "/" + dateDebut.Year);
            }
            else if (mois == 10)
            {
                return DateTime.Parse("31/" + mois + "/" + dateDebut.Year);
            }
            else if (mois == 11)
            {
                return DateTime.Parse("30/" + mois + "/" + dateDebut.Year);
            }
            else if (mois == 12)
            {
                return DateTime.Parse("31/" + mois + "/" + dateDebut.Year);
            }
            else
            {
                return dateDebut;
            }
        }

        static System.Globalization.CultureInfo elGR = System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR");

        void AjouterLibelleDepense()
        {
            try
            {

                var categorie = AppCode.ConnectionClass.ListeCategorieOrdonneeParCode(2);
                if (cmbRapport.Text == "Mensuel")
                {
                    dataGridView2.Rows.Add("", "Dépenses", "", "", "", "", "", "", "1");
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.BackColor = Color.GreenYellow;
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 10.5F, System.Drawing.FontStyle.Regular | FontStyle.Bold);

                    foreach (var c in categorie)
                    {
                        var cate = c.Categorie;
                        if (!string.IsNullOrWhiteSpace(c.Code))
                            cate = c.Code + " - " + c.Categorie;
                        var p = from li in AppCode.ConnectionClass.ListeLibelleOrdonneParCode(2)
                                where li.IDCategorie == c.IDCategorie
                                select new
                                {
                                    li.Code,
                                    li.Libelle,
                                    li.IDLibelle
                                };
                        if (p.Count() > 0)
                        {
                            dataGridView2.Rows.Add("", cate, "", "", "", "",  "1");
                            dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Lavender;
                            dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 10.5F, System.Drawing.FontStyle.Regular | FontStyle.Bold);

                            foreach (var q in p)
                            {
                                var libelle = q.Libelle;
                                if (!string.IsNullOrWhiteSpace(q.Code))
                                    libelle = q.Code + " - " + q.Libelle;
                                dataGridView2.Rows.Add(q.IDLibelle, libelle, "", "", "",  "", "1");
                            }
                            dataGridView2.Rows.Add("", "Total - " + c.Categorie, "", "", "",  "", "1");
                            dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 10.5F, System.Drawing.FontStyle.Regular | FontStyle.Bold);

                        }
                    }
                    dataGridView2.Rows.Add("", "Total  dépenses ", "", "", "", "", "1");
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 10.5F, System.Drawing.FontStyle.Regular | FontStyle.Bold);
                }
                else if (cmbRapport.Text.Contains("trimestre"))
                {
                    dataGridView2.Rows.Add("", "Dépenses", "", "", "", "", "", "", "1");
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.BackColor = Color.GreenYellow;
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 10.5F, System.Drawing.FontStyle.Regular | FontStyle.Bold);

                    foreach (var c in categorie)
                    {
                        var cate = c.Categorie;
                        if (!string.IsNullOrWhiteSpace(c.Code))
                            cate = c.Code + " - " + c.Categorie;
                        var p = from li in AppCode.ConnectionClass.ListeLibelleOrdonneParCode(2)
                                where li.IDCategorie == c.IDCategorie
                                select new
                                {
                                    li.Code,
                                    li.Libelle,
                                    li.IDLibelle
                                };
                        if (p.Count() > 0)
                        {
                            dataGridView2.Rows.Add("", cate, "", "", "", "", "", "", "1");
                            dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Lavender;
                            dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 10.5F, System.Drawing.FontStyle.Regular | FontStyle.Bold);

                            foreach (var q in p)
                            {
                                var libelle = q.Libelle;
                                if (!string.IsNullOrWhiteSpace(q.Code))
                                    libelle = q.Code + " - " + q.Libelle;
                                dataGridView2.Rows.Add(q.IDLibelle, libelle, "", "", "", "", "", "", "1");
                            }
                            dataGridView2.Rows.Add("", "Total - " + c.Categorie, "", "", "", "", "", "", "1");
                            dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 10.5F, System.Drawing.FontStyle.Regular | FontStyle.Bold);

                        }
                    }
                    dataGridView2.Rows.Add("", "Total  dépenses ", "", "", "", "", "", "", "1");
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 10.5F, System.Drawing.FontStyle.Regular | FontStyle.Bold);
                }
                else if (cmbRapport.Text.Contains("semestre"))
                {
                    dataGridView2.Rows.Add("", "Dépenses", "", "", "", "", "", "", "", "", "", "1");
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.BackColor = Color.GreenYellow;
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 10.5F, System.Drawing.FontStyle.Regular | FontStyle.Bold);

                    foreach (var cc in categorie)
                    {
                        var cate = cc.Categorie;
                        if (!string.IsNullOrWhiteSpace(cc.Code))
                            cate = cc.Code + " - " + cc.Categorie;
                        var p = from li in AppCode.ConnectionClass.ListeLibelleOrdonneParCode(2)
                                where li.IDCategorie == cc.IDCategorie
                                select new
                                {
                                    li.Code,
                                    li.Libelle,
                                    li.IDLibelle
                                };
                        if (p.Count() > 0)
                        {
                            dataGridView2.Rows.Add("", cate, "", "", "", "", "", "", "", "", "", "1");
                            dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Lavender;
                            dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 10.5F, System.Drawing.FontStyle.Regular | FontStyle.Bold);

                            foreach (var q in p)
                            {
                                var libelle = q.Libelle;
                                if (!string.IsNullOrWhiteSpace(q.Code))
                                    libelle = q.Code + " - " + q.Libelle;
                                dataGridView2.Rows.Add(q.IDLibelle, libelle, "", "", "", "", "", "", "", "", "", "1");
                            }
                            dataGridView2.Rows.Add("", "Total - " + cc.Categorie, "", "", "", "", "", "", "", "", "", "1");
                            dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 10.5F, System.Drawing.FontStyle.Regular | FontStyle.Bold);

                        }
                    }
                    dataGridView2.Rows.Add("", "Total  dépenses ", "", "", "", "", "", "", "", "", "", "1");
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 10.5F, System.Drawing.FontStyle.Regular | FontStyle.Bold);
                }
                else if (cmbRapport.Text.Contains("Annuel"))
                {
                    dataGridView2.Rows.Add("", "Dépenses", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "1");
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.BackColor = Color.GreenYellow;
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 10.5F, System.Drawing.FontStyle.Regular | FontStyle.Bold);

                    foreach (var ccc in categorie)
                    {
                        var cate = ccc.Categorie;
                        if (!string.IsNullOrWhiteSpace(ccc.Code))
                            cate = ccc.Code + " - " + ccc.Categorie;
                        var p = from li in AppCode.ConnectionClass.ListeLibelleOrdonneParCode(2)
                                where li.IDCategorie == ccc.IDCategorie
                                select new
                                {
                                    li.Code,
                                    li.Libelle,
                                    li.IDLibelle
                                };
                        if (p.Count() > 0)
                        {
                            dataGridView2.Rows.Add("", cate, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "1");
                            dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Lavender;
                            dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 10.5F, System.Drawing.FontStyle.Regular | FontStyle.Bold);

                            foreach (var q in p)
                            {
                                var libelle = q.Libelle;
                                if (!string.IsNullOrWhiteSpace(q.Code))
                                    libelle = q.Code + " - " + q.Libelle;
                                dataGridView2.Rows.Add(q.IDLibelle, libelle, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "1");
                            }
                            dataGridView2.Rows.Add("", "Total - " + ccc.Categorie, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "1");
                            dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 10.5F, System.Drawing.FontStyle.Regular | FontStyle.Bold);

                        }
                    }
                    dataGridView2.Rows.Add("", "Total  dépenses ", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "1");
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 10.5F, System.Drawing.FontStyle.Regular | FontStyle.Bold);
                }
            }
            catch
            { }
        }


        void AjouterLibelleRecette()
        {
            try
            {


                if (cmbRapport.Text == "Mensuel")
                {
                    var categorie = from ct in AppCode.ConnectionClass.ListeCategorieOrdonneeParCode(1)
                                    where !ct.Categorie.ToUpper().Contains("SUBVENTION")
                                    select ct;

                    dataGridView2.Rows.Add("", "", "", "", "", "", "2");
                    dataGridView2.Rows.Add("", "Recettes", "", "", "", "", "2");
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.BackColor = Color.GreenYellow;
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 10.5F, System.Drawing.FontStyle.Regular | FontStyle.Bold);

                    foreach (var c in categorie)
                    {
                        var cate = c.Categorie;
                        if (!string.IsNullOrWhiteSpace(c.Code))
                            cate = c.Code + " - " + c.Categorie;
                        var p = from li in AppCode.ConnectionClass.ListeLibelleOrdonneParCode(1)
                                where li.IDCategorie == c.IDCategorie
                                select new
                                {
                                    li.Code,
                                    li.Libelle,
                                    li.IDLibelle
                                };
                        if (p.Count() > 0)
                        {
                            dataGridView2.Rows.Add("", cate, "", "", "", "", "2");
                            dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Lavender;
                            dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 10.5F, System.Drawing.FontStyle.Regular | FontStyle.Bold);

                            foreach (var q in p)
                            {
                                var libelle = q.Libelle;
                                if (!string.IsNullOrWhiteSpace(q.Code))
                                    libelle = q.Code + " - " + q.Libelle;
                                dataGridView2.Rows.Add(q.IDLibelle, libelle, "", "", "", "", "2");
                            }
                            dataGridView2.Rows.Add("", "Total - " + c.Categorie, "", "", "", "", "2");
                            dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 10.5F, System.Drawing.FontStyle.Regular | FontStyle.Bold);

                        }
                    }
                    dataGridView2.Rows.Add("", "Totales recettes Hôpital", "", "", "", "", "2");
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 10.5F, System.Drawing.FontStyle.Regular | FontStyle.Bold);

                    categorie = from ct in AppCode.ConnectionClass.ListeCategorieOrdonneeParCode(1)
                                where ct.Categorie.ToUpper().Contains("SUBVENTION")
                                select ct;
                    dataGridView2.Rows.Add("", "", "", "", "", "", "2");
                 
                    foreach (var c in categorie)
                    {
                        var cate = c.Categorie;
                        if (!string.IsNullOrWhiteSpace(c.Code))
                            cate = c.Code + " - " + c.Categorie;
                        var p = from li in AppCode.ConnectionClass.ListeLibelleOrdonneParCode(1)
                                where li.IDCategorie == c.IDCategorie
                                select new
                                {
                                    li.Code,
                                    li.Libelle,
                                    li.IDLibelle
                                };
                        if (p.Count() > 0)
                        {
                            dataGridView2.Rows.Add("", cate, "", "", "", "", "2");
                            dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Lavender;
                            dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 10.5F, System.Drawing.FontStyle.Regular | FontStyle.Bold);

                            foreach (var q in p)
                            {
                                var libelle = q.Libelle;
                                if (!string.IsNullOrWhiteSpace(q.Code))
                                    libelle = q.Code + " - " + q.Libelle;
                                dataGridView2.Rows.Add(q.IDLibelle, libelle, "", "", "", "", "2");
                            }
                            dataGridView2.Rows.Add("", "Total - " + c.Categorie, "", "", "", "", "2");
                            dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 10.5F, System.Drawing.FontStyle.Regular | FontStyle.Bold);

                        }
                    }
                    dataGridView2.Rows.Add("", " ", "", "", "", "", "2");
                    dataGridView2.Rows.Add("", "Totales recettes", "", "", "", "", "2");
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 10.5F, System.Drawing.FontStyle.Regular | FontStyle.Bold);

                    dataGridView2.Rows.Add("", " ", "", "", "", "", "2");
                    dataGridView2.Rows.Add("", "Résultat", "", "", "", "", "2");
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 10.5F, System.Drawing.FontStyle.Regular | FontStyle.Bold);
                }
                else if (cmbRapport.Text.Contains("trimestre"))
                {
                    var categorie = from ct in AppCode.ConnectionClass.ListeCategorieOrdonneeParCode(1)
                                    where !ct.Categorie.ToUpper().Contains("SUBVENTION")
                                    select ct;
                    dataGridView2.Rows.Add("", "", "", "", "", "", "", "", "2");
                    dataGridView2.Rows.Add("", "Recettes", "", "", "", "", "", "", "2");
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.BackColor = Color.GreenYellow;
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 10.5F, System.Drawing.FontStyle.Regular | FontStyle.Bold);

                    foreach (var c in categorie)
                    {
                        var cate = c.Categorie;
                        if (!string.IsNullOrWhiteSpace(c.Code))
                            cate = c.Code + " - " + c.Categorie;
                        var p = from li in AppCode.ConnectionClass.ListeLibelleOrdonneParCode(1)
                                where li.IDCategorie == c.IDCategorie
                                select new
                                {
                                    li.Code,
                                    li.Libelle,
                                    li.IDLibelle
                                };
                        if (p.Count() > 0)
                        {
                            dataGridView2.Rows.Add("", cate, "", "", "", "", "", "", "2");
                            dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Lavender;
                            dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 10.5F, System.Drawing.FontStyle.Regular | FontStyle.Bold);

                            foreach (var q in p)
                            {
                                var libelle = q.Libelle;
                                if (!string.IsNullOrWhiteSpace(q.Code))
                                    libelle = q.Code + " - " + q.Libelle;
                                dataGridView2.Rows.Add(q.IDLibelle, libelle, "", "", "", "", "", "", "2");
                            }
                            dataGridView2.Rows.Add("", "Total - " + c.Categorie, "", "", "", "", "", "", "2");
                            dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 10.5F, System.Drawing.FontStyle.Regular | FontStyle.Bold);

                        }
                    }
                    dataGridView2.Rows.Add("", "Totales recettes Hôpital ", "", "", "", "", "", "", "2");
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 10.5F, System.Drawing.FontStyle.Regular | FontStyle.Bold);

                    categorie = from ct in AppCode.ConnectionClass.ListeCategorieOrdonneeParCode(1)
                                    where ct.Categorie.ToUpper().Contains("SUBVENTION")
                                    select ct;
                    dataGridView2.Rows.Add("", "", "", "", "", "", "", "", "2");
                
                    foreach (var c in categorie)
                    {
                        var cate = c.Categorie;
                        if (!string.IsNullOrWhiteSpace(c.Code))
                            cate = c.Code + " - " + c.Categorie;
                        var p = from li in AppCode.ConnectionClass.ListeLibelleOrdonneParCode(1)
                                where li.IDCategorie == c.IDCategorie
                                select new
                                {
                                    li.Code,
                                    li.Libelle,
                                    li.IDLibelle
                                };
                        if (p.Count() > 0)
                        {
                            dataGridView2.Rows.Add("", cate, "", "", "", "", "", "", "2");
                            dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Lavender;
                            dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 10.5F, System.Drawing.FontStyle.Regular | FontStyle.Bold);

                            foreach (var q in p)
                            {
                                var libelle = q.Libelle;
                                if (!string.IsNullOrWhiteSpace(q.Code))
                                    libelle = q.Code + " - " + q.Libelle;
                                dataGridView2.Rows.Add(q.IDLibelle, libelle, "", "", "", "", "", "", "2");
                            }
                            dataGridView2.Rows.Add("", "Total - " + c.Categorie, "", "", "", "", "", "", "2");
                            dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 10.5F, System.Drawing.FontStyle.Regular | FontStyle.Bold);

                        }
                    }
                    dataGridView2.Rows.Add("", "", "", "", "", "", "", "", "2");
                    dataGridView2.Rows.Add("", "Totales recettes", "", "", "", "", "", "", "2");
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 10.5F, System.Drawing.FontStyle.Regular | FontStyle.Bold);

                    dataGridView2.Rows.Add("", "", "", "", "", "", "", "", "2");
                    dataGridView2.Rows.Add("", "Résultat", "", "", "", "", "", "", "2");
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 10.5F, System.Drawing.FontStyle.Regular | FontStyle.Bold);

                }
                else if (cmbRapport.Text.Contains("semestre"))
                {
                    var categorie = from ct in AppCode.ConnectionClass.ListeCategorieOrdonneeParCode(1)
                                    where !ct.Categorie.ToUpper().Contains("SUBVENTION")
                                    select ct;
                    dataGridView2.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "2");
                    dataGridView2.Rows.Add("", "Recettes", "", "", "", "", "", "", "", "", "", "2");
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.BackColor = Color.GreenYellow;
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 10.5F, System.Drawing.FontStyle.Regular | FontStyle.Bold);

                    foreach (var cc in categorie)
                    {
                        var cate = cc.Categorie;
                        if (!string.IsNullOrWhiteSpace(cc.Code))
                            cate = cc.Code + " - " + cc.Categorie;
                        var p = from li in AppCode.ConnectionClass.ListeLibelleOrdonneParCode(1)
                                where li.IDCategorie == cc.IDCategorie
                                select new
                                {
                                    li.Code,
                                    li.Libelle,
                                    li.IDLibelle
                                };
                        if (p.Count() > 0)
                        {
                            dataGridView2.Rows.Add("", cate, "", "", "", "", "", "", "", "", "", "2");
                            dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Lavender;
                            dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 10.5F, System.Drawing.FontStyle.Regular | FontStyle.Bold);

                            foreach (var q in p)
                            {
                                var libelle = q.Libelle;
                                if (!string.IsNullOrWhiteSpace(q.Code))
                                    libelle = q.Code + " - " + q.Libelle;
                                dataGridView2.Rows.Add(q.IDLibelle, libelle, "", "", "", "", "", "", "", "", "", "2");
                            }
                            dataGridView2.Rows.Add("", "Total - " + cc.Categorie, "", "", "", "", "", "", "", "", "", "2");
                            dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 10.5F, System.Drawing.FontStyle.Regular | FontStyle.Bold);

                        }
                    }
                    dataGridView2.Rows.Add("", "Totales recettes Hôpital", "", "", "", "", "", "", "", "", "", "2");
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 10.5F, System.Drawing.FontStyle.Regular | FontStyle.Bold);

                    categorie = from ct in AppCode.ConnectionClass.ListeCategorieOrdonneeParCode(1)
                                    where ct.Categorie.ToUpper().Contains("SUBVENTION")
                                    select ct;
                    dataGridView2.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "2");
                   
                    foreach (var cc in categorie)
                    {
                        var cate = cc.Categorie;
                        if (!string.IsNullOrWhiteSpace(cc.Code))
                            cate = cc.Code + " - " + cc.Categorie;
                        var p = from li in AppCode.ConnectionClass.ListeLibelleOrdonneParCode(1)
                                where li.IDCategorie == cc.IDCategorie
                                select new
                                {
                                    li.Code,
                                    li.Libelle,
                                    li.IDLibelle
                                };
                        if (p.Count() > 0)
                        {
                            dataGridView2.Rows.Add("", cate, "", "", "", "", "", "", "", "", "", "2");
                            dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Lavender;
                            dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 10.5F, System.Drawing.FontStyle.Regular | FontStyle.Bold);

                            foreach (var q in p)
                            {
                                var libelle = q.Libelle;
                                if (!string.IsNullOrWhiteSpace(q.Code))
                                    libelle = q.Code + " - " + q.Libelle;
                                dataGridView2.Rows.Add(q.IDLibelle, libelle, "", "", "", "", "", "", "", "", "", "2");
                            }
                            dataGridView2.Rows.Add("", "Total - " + cc.Categorie, "", "", "", "", "", "", "", "", "", "2");
                            dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 10.5F, System.Drawing.FontStyle.Regular | FontStyle.Bold);

                        }
                    }
                    dataGridView2.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "2");
                    dataGridView2.Rows.Add("", "Totales recettes", "", "", "", "", "", "", "", "", "", "2");
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 10.5F, System.Drawing.FontStyle.Regular | FontStyle.Bold);

                    dataGridView2.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "2");
                    dataGridView2.Rows.Add("", "Résultat", "", "", "", "", "", "", "", "", "", "2");
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 10.5F, System.Drawing.FontStyle.Regular | FontStyle.Bold);
                }
                else if (cmbRapport.Text.Contains("Annuel"))
                {
                    var categorie = from ct in AppCode.ConnectionClass.ListeCategorieOrdonneeParCode(1)
                                    where !ct.Categorie.ToUpper().Contains("SUBVENTION")
                                    select ct;
                    dataGridView2.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "2");
                    dataGridView2.Rows.Add("", "Recettes", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "2");
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.BackColor = Color.GreenYellow;
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 10.5F, System.Drawing.FontStyle.Regular | FontStyle.Bold);

                    foreach (var ccc in categorie)
                    {
                        var cate = ccc.Categorie;
                        if (!string.IsNullOrWhiteSpace(ccc.Code))
                            cate = ccc.Code + " - " + ccc.Categorie;
                        var p = from li in AppCode.ConnectionClass.ListeLibelleOrdonneParCode(1)
                                where li.IDCategorie == ccc.IDCategorie
                                select new
                                {
                                    li.Code,
                                    li.Libelle,
                                    li.IDLibelle
                                };
                        if (p.Count() > 0)
                        {
                            dataGridView2.Rows.Add("", cate, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "2");
                            dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Lavender;
                            dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 10.5F, System.Drawing.FontStyle.Regular | FontStyle.Bold);

                            foreach (var q in p)
                            {
                                var libelle = q.Libelle;
                                if (!string.IsNullOrWhiteSpace(q.Code))
                                    libelle = q.Code + " - " + q.Libelle;
                                dataGridView2.Rows.Add(q.IDLibelle, libelle, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "2");
                            }
                            dataGridView2.Rows.Add("", "Total - " + ccc.Categorie, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "2");
                            dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 10.5F, System.Drawing.FontStyle.Regular | FontStyle.Bold);

                        }
                    }
                    dataGridView2.Rows.Add("", "Totales recettes Hôpital", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "2");
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 10.5F, System.Drawing.FontStyle.Regular | FontStyle.Bold);

                    categorie = from ct in AppCode.ConnectionClass.ListeCategorieOrdonneeParCode(1)
                                    where ct.Categorie.ToUpper().Contains("SUBVENTION")
                                    select ct;
                    dataGridView2.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "2");
                   
                    foreach (var ccc in categorie)
                    {
                        var cate = ccc.Categorie;
                        if (!string.IsNullOrWhiteSpace(ccc.Code))
                            cate = ccc.Code + " - " + ccc.Categorie;
                        var p = from li in AppCode.ConnectionClass.ListeLibelleOrdonneParCode(1)
                                where li.IDCategorie == ccc.IDCategorie
                                select new
                                {
                                    li.Code,
                                    li.Libelle,
                                    li.IDLibelle
                                };
                        if (p.Count() > 0)
                        {
                            dataGridView2.Rows.Add("", cate, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "2");
                            dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.BackColor = Color.Lavender;
                            dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 10.5F, System.Drawing.FontStyle.Regular | FontStyle.Bold);

                            foreach (var q in p)
                            {
                                var libelle = q.Libelle;
                                if (!string.IsNullOrWhiteSpace(q.Code))
                                    libelle = q.Code + " - " + q.Libelle;
                                dataGridView2.Rows.Add(q.IDLibelle, libelle, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "2");
                            }
                            dataGridView2.Rows.Add("", "Total - " + ccc.Categorie, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "2");
                            dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 10.5F, System.Drawing.FontStyle.Regular | FontStyle.Bold);

                        }
                    }
                    dataGridView2.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "2");
                    dataGridView2.Rows.Add("", "Totales recettes", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "2");
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 10.5F, System.Drawing.FontStyle.Regular | FontStyle.Bold);

                    dataGridView2.Rows.Add("", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "2");
                    dataGridView2.Rows.Add("", "Résultat", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "2");
                    dataGridView2.Rows[dataGridView2.Rows.Count - 1].DefaultCellStyle.Font = new System.Drawing.Font("Arial unicode", 10.5F, System.Drawing.FontStyle.Regular | FontStyle.Bold);

                }

            }
            catch
            { }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView2.Columns.Clear();
                dataGridView2.Rows.Clear();


                if (cmbRapport.Text == "Mensuel")
                {
                    dataGridView2.Columns.Add("l0", "N°");
                    dataGridView2.Columns.Add("l1b", "Libellés");
                    dataGridView2.Columns.Add("l1", cmbMois.Text);
                    dataGridView2.Columns.Add("l8", "Totaux");
                    dataGridView2.Columns.Add("l9", "% à la  Rubrique");
                    dataGridView2.Columns.Add("l10", "% Dép. Totale");
                    dataGridView2.Columns.Add("Etat", "Etat");
                    dataGridView2.Columns[1].Width = dataGridView2.Width / 4;
                    dataGridView2.Columns[0].Visible = false;
                    dataGridView2.Columns[dataGridView2.ColumnCount - 1].Visible = false;
                    AjouterLibelleDepense();
                    var startIndex=DateTime.Parse("25/"+cmbMois.Text+"/"+DateTime.Now.Year).Month;
                    var endIndex = startIndex+1;
                    DepenseMensuelle(startIndex , endIndex );
                    CalculerPourcentageRubrique(startIndex , endIndex );

                    AjouterLibelleRecette();
                    RecetteMensuelle(startIndex , endIndex );
                    CalculerPourcentageRecetteParRubrique(startIndex, endIndex);
                }
                else if (cmbRapport.Text == "Premier semestre")
                {
                    dataGridView2.Columns.Add("l0", "N°");
                    dataGridView2.Columns.Add("l1", "Libellés");
                    dataGridView2.Columns.Add("l2", "Janvier");
                    dataGridView2.Columns.Add("l3", "Fevrier");
                    dataGridView2.Columns.Add("l4", "Mars");
                    dataGridView2.Columns.Add("l5", "Avril");
                    dataGridView2.Columns.Add("l6", "Mai");
                    dataGridView2.Columns.Add("l7", "Juin");
                    dataGridView2.Columns.Add("l8", "Totaux");
                    dataGridView2.Columns.Add("l9", "% à la  Rubrique");
                    dataGridView2.Columns.Add("l10", "% Dép. Totale");
                    dataGridView2.Columns.Add("Etat", "Etat");
                    dataGridView2.Columns[1].Width = dataGridView2.Width / 4;
                    dataGridView2.Columns[0].Visible = false;
                    dataGridView2.Columns[dataGridView2.ColumnCount - 1].Visible = false;
                    AjouterLibelleDepense();
                    DepenseMensuelle(1, 7);
                    CalculerPourcentageRubrique(1, 7);

                    AjouterLibelleRecette();
                    RecetteMensuelle(1, 7);
                    CalculerPourcentageRecetteParRubrique(1, 7);
                }
                else if (cmbRapport.Text == "Deuxième semestre")
                {
                    dataGridView2.Columns.Add("l0", "N°");
                    dataGridView2.Columns.Add("l1", "Libellés");
                    dataGridView2.Columns.Add("l2", "Juillet");
                    dataGridView2.Columns.Add("l3", "Août");
                    dataGridView2.Columns.Add("l4", "Septembre");
                    dataGridView2.Columns.Add("l5", "Octobre");
                    dataGridView2.Columns.Add("l6", "Novembre");
                    dataGridView2.Columns.Add("l7", "Decembre");
                    dataGridView2.Columns.Add("l8", "Totaux");
                    dataGridView2.Columns.Add("l9", "% à la  Rubrique");
                    dataGridView2.Columns.Add("l10", "% Dép. Totale");
                    dataGridView2.Columns.Add("Etat", "Etat");
                    dataGridView2.Columns[1].Width = dataGridView2.Width / 4;
                    dataGridView2.Columns[0].Visible = false;
                    dataGridView2.Columns[dataGridView2.ColumnCount - 1].Visible = false;
                    AjouterLibelleDepense();
                    DepenseMensuelle(7, 13);
                    CalculerPourcentageRubrique(7, 13);

                    AjouterLibelleRecette();
                    RecetteMensuelle(7, 13);
                    CalculerPourcentageRecetteParRubrique(7, 13);
                }
                else if (cmbRapport.Text == "Premier trimestre")
                {
                    dataGridView2.Columns.Add("l0", "N°");
                    dataGridView2.Columns.Add("l1", "Libellés");
                    dataGridView2.Columns.Add("l2", "Janvier");
                    dataGridView2.Columns.Add("l3", "Fevrier");
                    dataGridView2.Columns.Add("l4", "Mars");
                    dataGridView2.Columns.Add("l5", "Totaux");
                    dataGridView2.Columns.Add("l6", "% à la  Rubrique");
                    dataGridView2.Columns.Add("l7", "% Dép. Totale");
                    dataGridView2.Columns.Add("Etat", "Etat");
                    //dataGridView2.Columns[0].Visible = false;
                    dataGridView2.Columns[1].Width = dataGridView2.Width / 3;
                    dataGridView2.Columns[0].Visible = false;
                    dataGridView2.Columns[dataGridView2.ColumnCount - 1].Visible = false;
                    AjouterLibelleDepense();
                    DepenseMensuelle(1, 4);
                    CalculerPourcentageRubrique(1, 4);

                    AjouterLibelleRecette();
                    RecetteMensuelle(1, 4);
                    CalculerPourcentageRecetteParRubrique(1, 4);
                }
                else if (cmbRapport.Text == "Deuxième trimestre")
                {
                    dataGridView2.Columns.Add("l0", "N°");
                    dataGridView2.Columns.Add("l1", "Libellés");
                    dataGridView2.Columns.Add("l2", "Avril");
                    dataGridView2.Columns.Add("l3", "Mai");
                    dataGridView2.Columns.Add("l4", "Juin");
                    dataGridView2.Columns.Add("l5", "Totaux");
                    dataGridView2.Columns.Add("l6", "% à la  Rubrique");
                    dataGridView2.Columns.Add("l7", "% Dép. Totale");
                    dataGridView2.Columns.Add("Etat", "Etat");
                    dataGridView2.Columns[1].Width = dataGridView2.Width /3;
                    dataGridView2.Columns[0].Visible = false;
                    dataGridView2.Columns[dataGridView2.ColumnCount - 1].Visible = false;
                    AjouterLibelleDepense();
                    DepenseMensuelle(4, 7);
                    CalculerPourcentageRubrique(4, 7);

                    AjouterLibelleRecette();
                    RecetteMensuelle(4, 7);
                    CalculerPourcentageRecetteParRubrique(4, 7);
                }
                else if (cmbRapport.Text == "Troisième trimestre")
                {
                    dataGridView2.Columns.Add("l0", "N°");
                    dataGridView2.Columns.Add("l1", "Libellés");
                    dataGridView2.Columns.Add("l2", "Juillet");
                    dataGridView2.Columns.Add("l3", "Août");
                    dataGridView2.Columns.Add("l4", "Septembre");
                    dataGridView2.Columns.Add("l5", "Totaux");
                    dataGridView2.Columns.Add("l6", "% à la  Rubrique");
                    dataGridView2.Columns.Add("l7", "% Dép. Totale");
                    dataGridView2.Columns.Add("Etat", "Etat");
                    dataGridView2.Columns[1].Width = dataGridView2.Width / 3;
                    dataGridView2.Columns[0].Visible = false;
                    dataGridView2.Columns[dataGridView2.ColumnCount - 1].Visible = false;
                    AjouterLibelleDepense();
                    DepenseMensuelle(7, 10);
                    CalculerPourcentageRubrique(7, 10);

                    AjouterLibelleRecette();
                    RecetteMensuelle(7, 10);
                    CalculerPourcentageRecetteParRubrique(7, 10);
                }
                else if (cmbRapport.Text == "Quatrième trimestre")
                {
                    dataGridView2.Columns.Add("l0", "N°");
                    dataGridView2.Columns.Add("l1", "Libellés");
                    dataGridView2.Columns.Add("l2", "Octobre");
                    dataGridView2.Columns.Add("l3", "Novembre");
                    dataGridView2.Columns.Add("l4", "Decembre");
                    dataGridView2.Columns.Add("l5", "Totaux");
                    dataGridView2.Columns.Add("l6", "% à la  Rubrique");
                    dataGridView2.Columns.Add("l7", "% Dép. Totale");
                    dataGridView2.Columns.Add("Etat", "Etat");
                    dataGridView2.Columns[1].Width = dataGridView2.Width / 3;
                    dataGridView2.Columns[0].Visible = false;
                    dataGridView2.Columns[dataGridView2.ColumnCount - 1].Visible = false;
                    AjouterLibelleDepense();
                    DepenseMensuelle(10, 13);
                    CalculerPourcentageRubrique(10, 13);


                    AjouterLibelleRecette();
                    RecetteMensuelle(10, 13);
                    CalculerPourcentageRecetteParRubrique(10, 13);
                }
                else if (cmbRapport.Text == "Annuel")
                {
                    dataGridView2.Columns.Add("l0", "N°");
                    dataGridView2.Columns.Add("l1", "Libellés");
                    dataGridView2.Columns.Add("l2", "Janvier");
                    dataGridView2.Columns.Add("l3", "Fevrier");
                    dataGridView2.Columns.Add("l4", "Mars");
                    dataGridView2.Columns.Add("l5", "Avril");
                    dataGridView2.Columns.Add("l6", "Mai");
                    dataGridView2.Columns.Add("l7", "Juin");
                    dataGridView2.Columns.Add("l8", "Juillet");
                    dataGridView2.Columns.Add("l9", "Août");
                    dataGridView2.Columns.Add("l10", "Septembre");
                    dataGridView2.Columns.Add("l11", "Octobre");
                    dataGridView2.Columns.Add("l11", "Novembre");
                    dataGridView2.Columns.Add("l12", "Decembre");
                    dataGridView2.Columns.Add("l13", "Totaux");
                    dataGridView2.Columns.Add("l14", "% à la  Rubrique");
                    dataGridView2.Columns.Add("l15", "% Dép. Totale");
                    dataGridView2.Columns.Add("Etat", "Etat");
                    dataGridView2.Columns[1].Width = dataGridView2.Width / 4;
                    dataGridView2.Columns[0].Visible = false;
                    dataGridView2.Columns[dataGridView2.ColumnCount - 1].Visible = false;
                    AjouterLibelleDepense();
                    DepenseMensuelle(1, 13);
                    CalculerPourcentageRubrique(1, 13);
                    
                    AjouterLibelleRecette();
                    RecetteMensuelle(1, 13);
                    CalculerPourcentageRecetteParRubrique(1, 13);
                }
                
            }
            catch (Exception ex)
            { }
        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        void DepenseMensuelle( int indexColumn, int nombreMois)
        {
            try
            {
                int annee;
                if(Int32.TryParse(cmbAnne.Text, out annee))
                { }else { return; }

                foreach(DataGridViewRow  dtRow in dataGridView2.Rows )
                {
                    var nbreMois = new List<int>();
                    for (var i = indexColumn; i < nombreMois; i++)
                    {
                        nbreMois.Add(i);
                    }

                    int idLibelle;
                    if (!string.IsNullOrWhiteSpace(dtRow.Cells[0].Value.ToString()))
                    {
                        if (Int32.TryParse(dtRow.Cells[0].Value.ToString(), out idLibelle))
                        {
                            
                            var totalParLibelle = .0;
                            var index = 0;
                            foreach (var m in nbreMois)
                            {
                                var mois = ObtenirMois(m);
                                var montant = .0;
                                var listeDepenses = AppCode.ConnectionClass.EtatDepense(annee, mois, idLibelle);
                                foreach (var p in listeDepenses)
                                {
                                    montant += p.Montant;
                                    totalParLibelle += montant;
                                }
                                index = 2 + m - indexColumn;
                                dtRow.Cells[index].Value =  montant;
                            }

                            dtRow.Cells[index + 1].Value = String.Format(elGR, "{0:0,0}", totalParLibelle);
                        }
                    }
                    else if(dtRow.Cells[1].Value.ToString().Contains("Total - "))
                    {
                        var categorie = dtRow.Cells[1].Value.ToString().Substring(dtRow.Cells[1].Value.ToString().IndexOf("- ")+2);
                        var totalParLibelle = .0;
                        var index = 0;
                        foreach (var m in nbreMois)
                        {
                            var mois = ObtenirMois(m);
                            var montant = .0;
                            var listeDepenses = AppCode.ConnectionClass.EtatDepenseParCategorie(annee, mois, categorie );
                            foreach (var p in listeDepenses)
                            {
                                montant += p.Montant;
                                totalParLibelle += montant;
                            }
                            index = 2 + m - indexColumn;
                            dtRow.Cells[index].Value = String.Format(elGR, "{0:0,0}", montant);
                        }
                        dtRow.Cells[index + 1].Value = String.Format(elGR, "{0:0,0}", totalParLibelle);
                    }
                    else if (dtRow.Cells[1].Value.ToString().Contains("Total  dépenses "))
                    {
                        var categorie = dtRow.Cells[1].Value.ToString().Substring(dtRow.Cells[1].Value.ToString().IndexOf("- ") + 2);
                        var totalParLibelle = .0;
                        var index = 0;
                        foreach (var m in nbreMois)
                        {
                            var mois = ObtenirMois(m);
                            var montant = .0;
                            var listeDepenses = AppCode.ConnectionClass.EtatDepenseParMois(annee, mois);
                            foreach (var p in listeDepenses)
                            {
                                montant += p.Montant;
                                totalParLibelle += montant;
                            }
                            index = 2 + m - indexColumn;
                            dtRow.Cells[index].Value = String.Format(elGR, "{0:0,0}", montant);
                        }
                        dtRow.Cells[index + 1].Value = String.Format(elGR, "{0:0,0}", totalParLibelle);
                    }
                }
            }
            catch(Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("", ex);
            }
        }

        void CalculerPourcentageRubrique(int indexColumn, int nombreMois)
        {
            try
            {
                int annee;
                if (Int32.TryParse(cmbAnne.Text, out annee))
                { }
                else { return; }
                foreach (DataGridViewRow dtRow in dataGridView2.Rows)
                {
                    var nbreMois = new List<int>();
                    for (var i = indexColumn; i < nombreMois; i++)
                    {
                        nbreMois.Add(i);
                    }

                    double montantParLibelle; int idLibelle;
                    //if(dtRow.Cells[])
                    if (dtRow.Cells[dataGridView2.ColumnCount - 1].Value.ToString() == "1")
                        {
                        if (Double.TryParse(dtRow.Cells[dataGridView2.ColumnCount - 4].Value.ToString(), out montantParLibelle))
                        {
                            var categorie = "";
                            if (Int32.TryParse(dtRow.Cells[0].Value.ToString(), out idLibelle))
                            {
                                var l = from q in AppCode.ConnectionClass.ListeLibelleOrdonneParCode(2)
                                        join p in AppCode.ConnectionClass.ListeCategorieOrdonneeParCode(2)
                                        on q.IDCategorie equals p.IDCategorie
                                        where q.IDLibelle == idLibelle
                                        select p.Categorie;

                                foreach (var c in l)
                                    categorie = c;
                            }
                            else if (dtRow.Cells[1].Value.ToString().Contains("Total  dépenses "))
                            {
                                dtRow.Cells[dataGridView2.ColumnCount -3].Value = "100%";
                            }
                            else
                            {
                                categorie = dtRow.Cells[1].Value.ToString().Substring(dtRow.Cells[1].Value.ToString().IndexOf("- ") + 2);
                            }
                            var totalParLibelle = .0;
                            var totauxParLibelle = .0;
                            foreach (var m in nbreMois)
                            {
                                var mois = ObtenirMois(m);
                                var listeDepenses = AppCode.ConnectionClass.EtatDepenseParCategorie(annee, mois, categorie);
                                foreach (var p in listeDepenses)
                                {
                                    totalParLibelle += p.Montant;
                                }
                            }
                            double pourcentage = 0;
                            if (totalParLibelle > 0)
                            {
                                pourcentage = montantParLibelle / totalParLibelle;
                            }

                            dtRow.Cells[dataGridView2.ColumnCount - 3].Value = Math.Round(pourcentage * 100, 3) + "%";

                            foreach (var m in nbreMois)
                            {
                                var mois = ObtenirMois(m);
                                var listeDepenses = AppCode.ConnectionClass.EtatDepenseParMois(annee, mois);
                                foreach (var p in listeDepenses)
                                {
                                    totauxParLibelle += p.Montant;
                                }
                            }
                            pourcentage = 0;
                            if (totauxParLibelle > 0)
                            {
                                pourcentage = montantParLibelle / totauxParLibelle;
                            }
                            dtRow.Cells[dataGridView2.ColumnCount - 2].Value = Math.Round(pourcentage * 100, 3) + "%";
                        }
                    }
                }         
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("", ex);
            }
        }


        void RecetteMensuelle(int indexColumn, int nombreMois)
        {
            try
            {
                int annee;
                if (Int32.TryParse(cmbAnne.Text, out annee))
                { }
                else { return; }
               
                foreach (DataGridViewRow dtRow in dataGridView2.Rows)
                {
                    var nbreMois = new List<int>();
                    for (var i = indexColumn; i < nombreMois; i++)
                    {
                        nbreMois.Add(i);
                    }

                    int idLibelle;
                    if (dtRow.Cells[dataGridView2.ColumnCount - 1].Value.ToString() == "2")
                    {
                        if (!string.IsNullOrWhiteSpace(dtRow.Cells[0].Value.ToString()))
                        {
                            if (Int32.TryParse(dtRow.Cells[0].Value.ToString(), out idLibelle))
                            {

                                var totalParLibelle = .0;
                                var index = 0;
                                foreach (var m in nbreMois)
                                {
                                    var mois = ObtenirMois(m);
                                    var montant = .0;
                                    var listeRecette = AppCode.ConnectionClass.EtatRecette(annee, mois, idLibelle);
                                    foreach (var p in listeRecette)
                                    {
                                        montant += p.MontantPaiement;
                                        totalParLibelle += montant;
                                    }
                                    index = 2 + m - indexColumn;
                                    dtRow.Cells[index].Value = montant;
                                }

                                dtRow.Cells[index + 1].Value = String.Format(elGR, "{0:0,0}", totalParLibelle);
                            }
                        }
                        else if (dtRow.Cells[1].Value.ToString().Contains("Total - "))
                        {
                            var categorie = dtRow.Cells[1].Value.ToString().Substring(dtRow.Cells[1].Value.ToString().IndexOf("- ") + 2);
                            var totalParLibelle = .0;
                            var index = 0;
                            foreach (var m in nbreMois)
                            {
                                var mois = ObtenirMois(m);
                                var montant = .0;
                                var listeRecette = AppCode.ConnectionClass.EtatRecetteParCategorie(annee, mois, categorie);
                                foreach (var p in listeRecette)
                                {
                                    montant += p.MontantPaiement;
                                    totalParLibelle += montant;
                                }
                                index = 2 + m - indexColumn;
                                dtRow.Cells[index].Value = String.Format(elGR, "{0:0,0}", montant);
                            }
                            dtRow.Cells[index + 1].Value = String.Format(elGR, "{0:0,0}", totalParLibelle);
                        }
                        else if (dtRow.Cells[1].Value.ToString().Equals("Totales recettes"))
                        {
                            var categorie = dtRow.Cells[1].Value.ToString().Substring(dtRow.Cells[1].Value.ToString().IndexOf("- ") + 2);
                            var totalParLibelle = .0;
                            var index = 0;
                            foreach (var m in nbreMois)
                            {
                                var mois = ObtenirMois(m);
                                var montant = .0;
                                var listeRecette = AppCode.ConnectionClass.EtatRecetteParMois(annee, mois);
                                foreach (var p in listeRecette)
                                {
                                    montant += p.MontantPaiement;
                                    totalParLibelle += p.MontantPaiement; ;
                                }
                                index = 2 + m - indexColumn;
                                dtRow.Cells[index].Value = String.Format(elGR, "{0:0,0}", montant);
                            }
                            dtRow.Cells[index + 1].Value = String.Format(elGR, "{0:0,0}", totalParLibelle);
                        }
                        else if (dtRow.Cells[1].Value.ToString().Contains("Totales recettes Hôpital"))
                        {
                            var categorie = dtRow.Cells[1].Value.ToString().Substring(dtRow.Cells[1].Value.ToString().IndexOf("- ") + 2);
                            if (!categorie.Contains("SUBVENTION)"))
                            {
                                var totalParLibelle = .0;
                                var index = 0;
                                foreach (var m in nbreMois)
                                {
                                    var montant = .0;
                                    var mois = ObtenirMois(m);
                                    var ListeCategorieOrdonneeParCode = from ct in AppCode.ConnectionClass.ListeCategorieOrdonneeParCode(1)
                                                                        where !ct.Categorie.ToUpper().Contains("SUBVENTION")
                                                                        select ct;
                                    foreach (var cct in ListeCategorieOrdonneeParCode)
                                    {
                                        var listeRecette = AppCode.ConnectionClass.EtatRecetteParCategorie(annee, mois, cct.Categorie);
                                        foreach (var p in listeRecette)
                                        {
                                            
                                            montant += p.MontantPaiement;
                                            totalParLibelle += p.MontantPaiement;
                                        }
                                    }
                                    index = 2 + m - indexColumn;
                                    dtRow.Cells[index ].Value = String.Format(elGR, "{0:0,0}", montant );
                                }
                                dtRow.Cells[index + 1].Value = String.Format(elGR, "{0:0,0}", totalParLibelle);
                            }
                        }
                        else if (dtRow.Cells[1].Value.ToString().Contains("Résultat"))
                        {
                            var totalRecetteParLibelle = .0;
                            var totalDepenseParLibelle = .0;
                            var index = 0;
                            foreach (var m in nbreMois)
                            {
                                var mois = ObtenirMois(m);
                                var montantRecette = .0;
                                var montantDepense = .0;
                                var listeRecette = AppCode.ConnectionClass.EtatRecetteParMois(annee, mois);
                                var listeDepenses = AppCode.ConnectionClass.EtatDepenseParMois(annee, mois);

                                foreach (var p in listeRecette)
                                {
                                    montantRecette += p.MontantPaiement;
                                    totalRecetteParLibelle += p.MontantPaiement; ;
                                }

                                foreach (var p in listeDepenses)
                                {
                                    montantDepense += p.Montant;
                                    totalDepenseParLibelle += montantDepense;
                                }
                            
                            index = 2 + m - indexColumn;
                            dtRow.Cells[index].Value = String.Format(elGR, "{0:0,0}", montantRecette-montantDepense);
                        
                        }
                            dtRow.Cells[index + 1].Value = String.Format(elGR, "{0:0,0}", totalRecetteParLibelle - totalDepenseParLibelle);
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("", ex);
            }
        }

        private void button2_Click(object sender, EventArgs e)
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
                
                    sfd.FileName = "Etat_financier_du_ "+cmbRapport.Text +cmbAnne.Text+"_Impriméé_le_" + date + ".xls";
                if (sfd.ShowDialog() == DialogResult.OK)
                {

                    ToCsV(dataGridView2, sfd.FileName); // Here dataGridview1 is your grid view name
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
            }
            catch (Exception ex)
            {
              GestionPharmacetique.  MonMessageBox.ShowBox("", ex);
            }
        }

        private void ToCsV(DataGridView dGV, string filename)
        {
            try
            {
                string stOutput = "";
                // Export titles:
                string sHeaders = "";

                for (int j = 1; j < dGV.Columns.Count-1; j++)
                    sHeaders = sHeaders.ToString() + Convert.ToString(dGV.Columns[j].HeaderText) + "\t";
                stOutput += sHeaders + "\r\n";
                // Export data.
                for (int i = 0; i < dGV.RowCount; i++)
                {
                    string stLine = "";
                    for (int j = 1; j < dGV.Rows[i].Cells.Count-1; j++)
                        stLine = stLine.ToString() + Convert.ToString(dGV.Rows[i].Cells[j].Value) + "\t";
                    stOutput += stLine + "\r\n";
                }
                Encoding utf16 = Encoding.GetEncoding(1254);
                byte[] output = utf16.GetBytes(stOutput);
               System.IO. FileStream fs = new System.IO.FileStream(filename, System.IO.FileMode.Create);
                System.IO.BinaryWriter bw = new System.IO.BinaryWriter(fs);
                bw.Write(output, 0, output.Length); //write the encoded file
                bw.Flush();
                bw.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
               GestionPharmacetique. MonMessageBox.ShowBox("Erreur exportation", ex);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView2.Rows.Count > 0)
                {

                    var titre = "";

                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "PDF Documents (*.pdf)|*.pdf";

                    sharpPDF.pdfDocument document = new sharpPDF.pdfDocument("christian", "cdali");
                    var jour = DateTime.Now.Day;
                    var moiSs = DateTime.Now.Month;
                    var year = DateTime.Now.Year;
                    var hour = DateTime.Now.Hour;
                    var min = DateTime.Now.Minute;
                    var sec = DateTime.Now.Second;
                    var datTe = jour.ToString() + "_" + moiSs.ToString() + "_" + year.ToString() + "_" + hour + "_" + min + "_" + sec;
                    sfd.FileName = label1.Text.Replace("/", "_") + "_imprimé_le_" + datTe + ".pdf";
                    //string pathFile = "";
                    if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        if (cmbRapport.Text == "Mensuel")
                        {
                            titre = "Etat financier  " + cmbRapport.Text + " " + cmbAnne.Text;
                            var div = (dataGridView2.Rows.Count) / 50;

                            for (var i = 0; i <= div; i++)
                            {
                                if (i * 55 < dataGridView2.Rows.Count)
                                {

                                    var bitmap = AppCode.Impression.ImprimerEtatFinanceMensuel(dataGridView2, titre, i);
                                    var inputImage = @"cdali" + i;
                                    // Create an empty page
                                    var pageIndex = document.addPage();

                                    document.addImageReference(bitmap, inputImage);
                                    var img1 = document.getImageReference(inputImage);
                                    pageIndex.addImage(img1, 0, 0, pageIndex.height, pageIndex.width);
                                    //pageIndex.addImage(img1, 0, 0, pageIndex.height, pageIndex.width);
                                }
                            }
                            document.createPDF(sfd.FileName);
                        }
                        else if (cmbRapport.Text.Contains("semestre"))
                        {
                            titre = "Etat financier  " + cmbRapport.Text + " " + cmbAnne.Text;
                            var div = (dataGridView2.Rows.Count) / 35;

                            for (var i = 0; i <= div; i++)
                            {
                                if (i * 35 < dataGridView2.Rows.Count)
                                {

                                    var bitmap = AppCode.Impression.ImprimerEtatFinanceSemestriel(dataGridView2, titre, i);
                                    var inputImage = @"cdali" + i;
                                    // Create an empty page
                                    var pageIndex = document.addPage(500, 700);

                                    document.addImageReference(bitmap, inputImage);
                                    var img1 = document.getImageReference(inputImage);
                                    pageIndex.addImage(img1, 0, 0, pageIndex.height, pageIndex.width);
                                }
                                document.createPDF(sfd.FileName);
                            }
                        }
                        else if (cmbRapport.Text.Contains("trimestre"))
                        {
                            titre = "Etat financier  " + cmbRapport.Text + " " + cmbAnne.Text;
                            double totaux;
                            //var dgvView = new DataGridView();
                            //var count = 0;// dgvView.Rows.Count ;
                            //dataGridView1.Columns.Clear();
                            //dataGridView1.Rows.Clear();
                            //for (var j = 0; j < dataGridView2.ColumnCount; j++)
                            //{
                            //    dataGridView1.Columns.Add("c" + j, dataGridView2.Columns[j].HeaderText);
                            //}
                            
                            //for (var i = 0; i < dataGridView2.Rows.Count; i++)
                            //{
                            //    if (double.TryParse(dataGridView2.Rows[i].Cells[5].Value.ToString(), out totaux))
                            //    {
                                    
                            //            if (totaux < 0 || totaux > 0)
                            //            {
                            //            dataGridView1.Rows.Add(
                            //                dataGridView2.Rows[i].Cells[0].Value.ToString(),
                            //                dataGridView2.Rows[i].Cells[1].Value.ToString(),
                            //                dataGridView2.Rows[i].Cells[2].Value.ToString(),
                            //                dataGridView2.Rows[i].Cells[3].Value.ToString(),
                            //                dataGridView2.Rows[i].Cells[4].Value.ToString(),
                            //                dataGridView2.Rows[i].Cells[5].Value.ToString(),
                            //                dataGridView2.Rows[i].Cells[6].Value.ToString(),
                            //                dataGridView2.Rows[i].Cells[7].Value.ToString(),
                            //                dataGridView2.Rows[i].Cells[8].Value.ToString()
                            //                );
                            //        }
                                    
                            //    }
                            //    else
                            //    {
                            //        dataGridView1.Rows.Add(
                            //           dataGridView2.Rows[i].Cells[0].Value.ToString(),
                            //           dataGridView2.Rows[i].Cells[1].Value.ToString(),
                            //           dataGridView2.Rows[i].Cells[2].Value.ToString(),
                            //           dataGridView2.Rows[i].Cells[3].Value.ToString(),
                            //           dataGridView2.Rows[i].Cells[4].Value.ToString(),
                            //           dataGridView2.Rows[i].Cells[5].Value.ToString(),
                            //           dataGridView2.Rows[i].Cells[6].Value.ToString(),
                            //           dataGridView2.Rows[i].Cells[7].Value.ToString(),
                            //           dataGridView2.Rows[i].Cells[8].Value.ToString()
                            //           );

                            //    }
                            //}

                            var count = dataGridView2.Rows.Count ;
                            var div = count / 35;

                            for (var i = 0; i <= div; i++)
                            {
                                if (i * 35 < count)
                                {

                                    var bitmap = AppCode.Impression.ImprimerEtatFinanceTrimestre(dataGridView2, titre, i);
                                    var inputImage = @"cdali" + i;
                                    // Create an empty page
                                    var pageIndex = document.addPage(500, 700);

                                    document.addImageReference(bitmap, inputImage);
                                    var img1 = document.getImageReference(inputImage);
                                    pageIndex.addImage(img1, 0, 0, pageIndex.height, pageIndex.width);
                                }
                            }
                            document.createPDF(sfd.FileName);
                        }
                        else if (cmbRapport.Text.Contains("Annuel"))
                        {
                            titre = "Etat financier  " + cmbRapport.Text + " " + cmbAnne.Text;
                            var div = (dataGridView2.Rows.Count) / 35;

                            for (var i = 0; i <= div; i++)
                            {
                                if (i * 35 < dataGridView2.Rows.Count)
                                {

                                    var bitmap = AppCode.Impression.ImprimerEtatFinanceAnnuel(dataGridView2, titre, i);
                                    var inputImage = @"cdali" + i;
                                    // Create an empty page
                                    var pageIndex = document.addPage(500, 700);

                                    document.addImageReference(bitmap, inputImage);
                                    var img1 = document.getImageReference(inputImage);
                                    pageIndex.addImage(img1, 0, 0, pageIndex.height, pageIndex.width);
                                }
                            }
                            document.createPDF(sfd.FileName);
                        }

                        System.Diagnostics.Process.Start(sfd.FileName);

                    }
                }
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("Imprimer paiement", ex);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var frm = new BilanFrm();
            if (cmbRapport.Text == "Mensuel")
            {
                frm.titre = "Bilan du " + cmbRapport.Text + " du mois de  "+cmbMois.Text + " " + cmbAnne.Text;

            }
            else
            {
                frm.titre = "Bilan du " + cmbRapport.Text + "  " + cmbAnne.Text;
            }

            frm.ShowDialog();
        }

        void CalculerPourcentageRecetteParRubrique(int indexColumn, int nombreMois)
        {
            try
            {
                int annee;
                if (Int32.TryParse(cmbAnne.Text, out annee))
                { }
                else { return; }
                foreach (DataGridViewRow dtRow in dataGridView2.Rows)
                {
                    var nbreMois = new List<int>();
                    for (var i = indexColumn; i < nombreMois; i++)
                    {
                        nbreMois.Add(i);
                    }

                    double montantParLibelle; int idLibelle;
                    if (dtRow.Cells[dataGridView2.ColumnCount - 1].Value.ToString() == "2")
                    {
                        if (Double.TryParse(dtRow.Cells[dataGridView2.ColumnCount - 4].Value.ToString(), out montantParLibelle))
                        {
                            var categorie = "";
                            if (Int32.TryParse(dtRow.Cells[0].Value.ToString(), out idLibelle))
                            {
                                var l = from q in AppCode.ConnectionClass.ListeLibelleOrdonneParCode(1)
                                        join p in AppCode.ConnectionClass.ListeCategorieOrdonneeParCode(1)
                                        on q.IDCategorie equals p.IDCategorie
                                        where q.IDLibelle == idLibelle
                                        select p.Categorie;

                                foreach (var c in l)
                                    categorie = c;
                            }
                            else if (dtRow.Cells[1].Value.ToString().Contains("Totales recettes"))
                            {
                                dtRow.Cells[dataGridView2.ColumnCount - 2].Value = "100%";
                                dtRow.Cells[dataGridView2.ColumnCount - 3].Value = "";
                            }
                            else if (dtRow.Cells[1].Value.ToString().Contains("Totales recettes Hôpital"))
                            {
                                dtRow.Cells[dataGridView2.ColumnCount - 2].Value = "";
                            }
                            else if (dtRow.Cells[1].Value.ToString().ToLower().Contains("Total - subvention".ToLower()))
                            {
                                dtRow.Cells[dataGridView2.ColumnCount - 2].Value = "";
                            }

                            else
                            {
                                categorie = dtRow.Cells[1].Value.ToString().Substring(dtRow.Cells[1].Value.ToString().IndexOf("- ") + 2);
                            }
                                var totalParLibelle = .0;
                                var totauxParLibelle = .0;
                                foreach (var m in nbreMois)
                                {
                                    var mois = ObtenirMois(m);
                                    var listeRecettes = AppCode.ConnectionClass.EtatRecetteParCategorie(annee, mois, categorie);
                                    foreach (var p in listeRecettes)
                                    {
                                        totalParLibelle += p.MontantPaiement;
                                    }
                                }
                                double pourcentage = 0;
                                if (totalParLibelle > 0)
                                {
                                    pourcentage = montantParLibelle / totalParLibelle;
                                }

                                dtRow.Cells[dataGridView2.ColumnCount - 3].Value = Math.Round(pourcentage * 100, 3) + "%";

                                foreach (var m in nbreMois)
                                {
                                    var mois = ObtenirMois(m);
                                    var listeRecettes = AppCode.ConnectionClass.EtatRecetteParMois(annee, mois);
                                    foreach (var p in listeRecettes)
                                    {
                                        totauxParLibelle += p.MontantPaiement;
                                    }
                                }
                                pourcentage = 0;
                                if (totauxParLibelle > 0)
                                {
                                    pourcentage = montantParLibelle / totauxParLibelle;
                                }
                                dtRow.Cells[dataGridView2.ColumnCount - 2].Value = Math.Round(pourcentage * 100, 3) + "%";
                            
                        }
                    }
                }
                
                dataGridView2.Rows[dataGridView2.Rows.Count-1].Cells[dataGridView2.ColumnCount - 2].Value = "";
                dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[dataGridView2.ColumnCount - 3].Value = "";
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("", ex);
            }
        }

        void BilanDesEtats(DataGridView dataGridView, int indexColumn, int nombreMois)
        {
            try
            {
                int annee;
                if (Int32.TryParse(cmbAnne.Text, out annee))
                { }
                else { return; }

                //var stockTotal = AppCode.ConnectionClassPharmacie.StockGeneral();
                //dataGridView.Rows.Add("'Stock médicaments ", stockTotal);
                foreach (DataGridViewRow dtRow in dataGridView.Rows)
                {
                    var nbreMois = new List<int>();
                    for (var i = indexColumn; i < nombreMois; i++)
                    {
                        nbreMois.Add(i);
                    }
                    //foreach (var m in nbreMois)
                    //{
                    //    var mois = ObtenirMois(m);
                    //    var montant = .0;
                    //    var listeRecette = AppCode.ConnectionClass.EtatRecette(annee, mois, idLibelle);
                    //    foreach (var p in listeRecette)
                    //    {
                    //        montant += p.MontantPaiement;
                    //        totalParLibelle += montant;
                    //    }
                        
                    //}
                }
            }
            catch (Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("", ex);
            }
        }

    }
}
