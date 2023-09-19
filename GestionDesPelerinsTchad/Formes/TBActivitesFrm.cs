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
    public partial class TBActivitesFrm : Form
    {
        public TBActivitesFrm()
        {
            InitializeComponent();
        }
        private void groupBox5_Paint(object sender, PaintEventArgs e)
        {
            var mGraphics = e.Graphics;
            var pen1 = new Pen(Color.FromArgb(255, 128, 0), 1);
            var area1 = new Rectangle(0, 0, this.groupBox5.Width - 1, this.groupBox5.Height - 1);
            var linearGradientBrush = new LinearGradientBrush(area1,
                Color.FromArgb(255, 128, 0), Color.FromArgb(255, 128, 0), LinearGradientMode.Horizontal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void TBPersonnelFrm_Paint(object sender, PaintEventArgs e)
        {
            var mGraphics = e.Graphics;
            var pen1 = new Pen(Color.White, 1);
            var area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            var linearGradientBrush = new LinearGradientBrush(area1,
                Color.White, Color.White, LinearGradientMode.Horizontal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            var mGraphics = e.Graphics;
            var pen1 = new Pen(Color.White, 1);
            var area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            var linearGradientBrush = new LinearGradientBrush(area1,
                Color.DodgerBlue, Color.DodgerBlue, LinearGradientMode.Horizontal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            var mGraphics = e.Graphics;
            var pen1 = new Pen(Color.FromArgb(255, 128, 0), 1);
            var area1 = new Rectangle(0, 0, this.grpSexe.Width - 1, this.grpSexe.Height - 1);
            var linearGradientBrush = new LinearGradientBrush(area1,
                Color.White, Color.White, LinearGradientMode.Horizontal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }

        private void TBActivitesFrm_Load(object sender, EventArgs e)
        {
            try
            {
                for (var i = 2017; i <= DateTime.Now.AddYears(5).Year; i++)
                {
                    cmbExercice.Items.Add(i.ToString());
                }
                cmbExercice.Text = DateTime.Now.Year.ToString();
                button6.Location = new Point(Width - button6.Width - 10, button6.Location.Y);
                dataGridView1.RowTemplate.Height =dataGridView1.Height/ 13;
                var listeMois = new string[] 
                    { 
                       "Tous les mois", "Janvier ", "Fevrier", "Mars", "Avril", "Mai", "Juin", "Juillet", "Aout", "Septembre", "Octobre", "Novembre", "Decembre",     
                    };
                foreach (var s in listeMois)
                {
                    dataGridView1.Rows.Add(s); 
                //if(DateTime.Now.Month==AppCode. Impression.ObtenirMois(s))
                //    dataGridView1.Rows[dataGridView1.Rows.Count-1].Selected = true;
                }

                cmbDivision.Items.Add("");
                foreach (var c in AppCode.ConnectionClass.ListeDirection())
                {
                    cmbDivision.Items.Add(c.Direction);
                }

                TB();
            }
            catch { }
        }

        void TB()
        {
            try
            {
                
                if (dataGridView1.SelectedRows.Count > 0)
                mois = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                if (mois == "Tous les mois")
                    mois = "";
                var debut = DateTime.Parse("01/01/" + DateTime.Now.Year);
                var fin =DateTime.Parse("31/12/"+DateTime.Now.Year);
                if (!string.IsNullOrEmpty(mois))
                {
                     debut = AppCode.Impression.ObtenirDebutJour(mois, Convert.ToInt32(cmbExercice.Text));
                     fin = AppCode.Impression.ObtenirFinJour(mois, Convert.ToInt32(cmbExercice.Text));
                }
              
                #region CONGE
                var listeConge = from c in AppCode.ConnectionClass.ListeConge()
                                 where c.Mois.StartsWith(mois, StringComparison.CurrentCultureIgnoreCase)
                                 where c.Exercice == Convert.ToInt32(cmbExercice.Text)
                                 select c;
                var countMasculin = 0; var countFeminin = 0; var count = 0;
                foreach (var c in listeConge)
                {
                    if (c.SiProjet)
                    {
                        var liste = from p in AppCode.ConnectionClass.ListePersonnelProjet(Int32.Parse(c.NumeroMatricule))
                                    join s in AppCode.ConnectionClass.ListeServicePersonnelProjet(Int32.Parse(c.NumeroMatricule))
                                    on p.IDPersonelProjet equals s.IDPersonelProjet
                                    join div in AppCode.ConnectionClass.ListeDivision()
                                    on s.IDDivision equals div.IDDivision
                                    join dir in AppCode.ConnectionClass.ListeDirection()
                                    on div.IDDirection equals dir.IDDirection
                                    where dir.Direction.StartsWith(cmbDivision.Text, StringComparison.CurrentCultureIgnoreCase)
                                    select new { p.Sexe };
                        countFeminin += liste.Where(p => p.Sexe == "F").Count();
                        countMasculin += liste.Where(p => p.Sexe == "M").Count();
                        count += liste.Count();
                    }
                    else
                    {

                        var liste = from p in AppCode.ConnectionClass.ListePersonnelParMatricule(c.NumeroMatricule)
                                    join s in AppCode.ConnectionClass.ListeServicePersonnel(c.NumeroMatricule)
                                    on p.NumeroMatricule equals s.NumeroMatricule
                                    join div in AppCode.ConnectionClass.ListeDivision()
                                    on s.IDDivision equals div.IDDivision
                                    join dir in AppCode.ConnectionClass.ListeDirection()
                                    on div.IDDirection equals dir.IDDirection
                                    where dir.Direction.StartsWith(cmbDivision.Text, StringComparison.CurrentCultureIgnoreCase)
                                    select new { p.Sexe };
                        countFeminin += liste.Where(p => p.Sexe == "F").Count();
                        countMasculin += liste.Where(p => p.Sexe == "M").Count();
                        count += liste.Count();
                    }
                }
                lblCountTotalSexe.Text = count.ToString();
                lblHomme.Text = countMasculin.ToString();
                lblFemme.Text = countFeminin.ToString();
                if (count > 0)
                {
                    lblPourFemelle.Text = Math.Round((double)(countFeminin * 100 / count), 2).ToString() + "%";
                    lblPourSexeMale.Text = Math.Round((double)(countMasculin * 100 / count), 2).ToString() + "%";
                }
                else
                {
                    lblPourFemelle.Text = "0%";
                    lblPourSexeMale.Text = "0%";
                } 
                #endregion

                #region ABSENCE
                if (!string.IsNullOrEmpty(mois))
                {
                    var listeAbsence = from c in AppCode.ConnectionClass.ListeUneAbsence()
                                       where c.DateDebutAbscense >=debut
                                       where c.DateDebutAbscense <= fin 
                                       where c.Exercice == Convert.ToInt32(cmbExercice.Text)
                                       select c;
                    countMasculin = 0; countFeminin = 0; count = 0;
                    foreach (var c in listeAbsence)
                    {
                        if (c.SiPersonnelProjet)
                        {
                            var liste = from p in AppCode.ConnectionClass.ListePersonnelProjet(Int32.Parse(c.NumeroEmploye))
                                        join s in AppCode.ConnectionClass.ListeServicePersonnelProjet(Int32.Parse(c.NumeroEmploye))
                                        on p.IDPersonelProjet equals s.IDPersonelProjet
                                        join div in AppCode.ConnectionClass.ListeDivision()
                                        on s.IDDivision equals div.IDDivision
                                        join dir in AppCode.ConnectionClass.ListeDirection()
                                        on div.IDDirection equals dir.IDDirection
                                        where dir.Direction.StartsWith(cmbDivision.Text, StringComparison.CurrentCultureIgnoreCase)
                                        select new { p.Sexe };
                            countFeminin += liste.Where(p => p.Sexe == "F").Count();
                            countMasculin += liste.Where(p => p.Sexe == "M").Count();
                            count += liste.Count();
                        }
                        else
                        {

                            var liste = from p in AppCode.ConnectionClass.ListePersonnelParMatricule(c.NumeroEmploye)
                                        join s in AppCode.ConnectionClass.ListeServicePersonnel(c.NumeroEmploye)
                                        on p.NumeroMatricule equals s.NumeroMatricule
                                        join div in AppCode.ConnectionClass.ListeDivision()
                                        on s.IDDivision equals div.IDDivision
                                        join dir in AppCode.ConnectionClass.ListeDirection()
                                        on div.IDDirection equals dir.IDDirection
                                        where dir.Direction.StartsWith(cmbDivision.Text, StringComparison.CurrentCultureIgnoreCase)
                                        select new { p.Sexe };
                            countFeminin += liste.Where(p => p.Sexe == "F").Count();
                            countMasculin += liste.Where(p => p.Sexe == "M").Count();
                            count += liste.Count();
                        }
                    }
                    lblTotalAbcense.Text = count.ToString();
                    lblAbsenceMasculin.Text = countMasculin.ToString();
                    lblAbsenceFeminin.Text = countFeminin.ToString();
                    if (count > 0)
                    {
                        lblAbsencePourFeminin.Text = Math.Round((double)(countFeminin * 100 / count), 2).ToString() + "%";
                        lblAbsencePourMascu.Text = Math.Round((double)(countMasculin * 100 / count), 2).ToString() + "%";
                    }
                    else
                    {
                        lblAbsencePourFeminin.Text = "0%";
                        lblAbsencePourMascu.Text = "0%";
                    }
                }
                else
                {
                    var listeAbsence = from c in AppCode.ConnectionClass.ListeUneAbsence()
                                       where c.Exercice == Convert.ToInt32(cmbExercice.Text)
                                       select c;
                    countMasculin = 0; countFeminin = 0; count = 0;
                    foreach (var c in listeAbsence)
                    {
                        if (c.SiPersonnelProjet)
                        {
                            var liste = from p in AppCode.ConnectionClass.ListePersonnelProjet(Int32.Parse(c.NumeroEmploye))
                                        join s in AppCode.ConnectionClass.ListeServicePersonnelProjet(Int32.Parse(c.NumeroEmploye))
                                        on p.IDPersonelProjet equals s.IDPersonelProjet
                                        join div in AppCode.ConnectionClass.ListeDivision()
                                        on s.IDDivision equals div.IDDivision
                                        join dir in AppCode.ConnectionClass.ListeDirection()
                                        on div.IDDirection equals dir.IDDirection
                                        where dir.Direction.StartsWith(cmbDivision.Text, StringComparison.CurrentCultureIgnoreCase)
                                        select new { p.Sexe };
                            countFeminin += liste.Where(p => p.Sexe == "F").Count();
                            countMasculin += liste.Where(p => p.Sexe == "M").Count();
                            count += liste.Count();
                        }
                        else
                        {

                            var liste = from p in AppCode.ConnectionClass.ListePersonnelParMatricule(c.NumeroEmploye)
                                        join s in AppCode.ConnectionClass.ListeServicePersonnel(c.NumeroEmploye)
                                        on p.NumeroMatricule equals s.NumeroMatricule
                                        join div in AppCode.ConnectionClass.ListeDivision()
                                        on s.IDDivision equals div.IDDivision
                                        join dir in AppCode.ConnectionClass.ListeDirection()
                                        on div.IDDirection equals dir.IDDirection
                                        where dir.Direction.StartsWith(cmbDivision.Text, StringComparison.CurrentCultureIgnoreCase)
                                        select new { p.Sexe };
                            countFeminin += liste.Where(p => p.Sexe == "F").Count();
                            countMasculin += liste.Where(p => p.Sexe == "M").Count();
                            count += liste.Count();
                        }
                    }
                    lblTotalAbcense.Text = count.ToString();
                    lblAbsenceMasculin.Text = countMasculin.ToString();
                    lblAbsenceFeminin.Text = countFeminin.ToString();
                    if (count > 0)
                    {
                        lblAbsencePourFeminin.Text = Math.Round((double)(countFeminin * 100 / count), 2).ToString() + "%";
                        lblAbsencePourMascu.Text = Math.Round((double)(countMasculin * 100 / count), 2).ToString() + "%";
                    }
                    else
                    {
                        lblAbsencePourFeminin.Text = "0%";
                        lblAbsencePourMascu.Text = "0%";
                    }
                }
                #endregion

                #region MISSION
                if (string.IsNullOrEmpty(mois))
                {
                    var listeMission = from c in AppCode.ConnectionClass.ListeMisson()
                                       where c.Exercice == Convert.ToInt32(cmbExercice.Text)
                                       select c;
                    countMasculin = 0; countFeminin = 0; count = 0;
                    foreach (var c in listeMission)
                    {
                         foreach (var m in AppCode.ConnectionClass.ListeMisson(c.IDMission))
                        {
                            if (m.SiPersonnelProjet)
                            {
                                var liste = from p in AppCode.ConnectionClass.ListePersonnelProjet(Int32.Parse(m.Matricule))
                                            join s in AppCode.ConnectionClass.ListeServicePersonnelProjet(Int32.Parse(m.Matricule))
                                            on p.IDPersonelProjet equals s.IDPersonelProjet
                                            join div in AppCode.ConnectionClass.ListeDivision()
                                            on s.IDDivision equals div.IDDivision
                                            join dir in AppCode.ConnectionClass.ListeDirection()
                                            on div.IDDirection equals dir.IDDirection
                                            where dir.Direction.StartsWith(cmbDivision.Text, StringComparison.CurrentCultureIgnoreCase)
                                            select new { p.Sexe };
                                foreach (var p in liste)
                                {
                                    if (p.Sexe == "F")
                                    {
                                        countFeminin += 1;
                                    }
                                    else if (p.Sexe == "M")
                                    {
                                        countMasculin += 1;
                                    }
                                    count += liste.Count();
                                }

                            }
                            else
                            {

                                var liste = from p in AppCode.ConnectionClass.ListePersonnelParMatricule(m.Matricule)
                                            join s in AppCode.ConnectionClass.ListeServicePersonnel(m.Matricule)
                                            on p.NumeroMatricule equals s.NumeroMatricule
                                            join div in AppCode.ConnectionClass.ListeDivision()
                                            on s.IDDivision equals div.IDDivision
                                            join dir in AppCode.ConnectionClass.ListeDirection()
                                            on div.IDDirection equals dir.IDDirection
                                            where dir.Direction.StartsWith(cmbDivision.Text, StringComparison.CurrentCultureIgnoreCase)
                                            select new { p.Sexe };
                                foreach (var p in liste)
                                {
                                    if (p.Sexe == "F")
                                    {
                                        countFeminin += 1;
                                    }
                                    else if (p.Sexe == "M")
                                    {
                                        countMasculin += 1;
                                    }
                                    count += liste.Count();
                                }
                            }
                        }
                    }
                    lblNbrePersonnelmission.Text = count.ToString();
                    lblMissionMale.Text = countMasculin.ToString();
                    lblMissionFeminin.Text = countFeminin.ToString();
                    lblMission.Text = listeMission.Count().ToString();
                    if (count > 0)
                    {
                        lblMissionPourFeminin.Text = Math.Round((double)(countFeminin * 100 / count), 2).ToString() + "%";
                        lblMissionPourcentMale.Text = Math.Round((double)(countMasculin * 100 / count), 2).ToString() + "%";
                    }
                    else
                    {
                        lblMissionPourFeminin.Text = "0%";
                        lblMissionPourcentMale.Text = "0%";
                    }
                }
                else
                {
                    var listeMission = from c in AppCode.ConnectionClass.ListeMisson()
                                       where c.DateDepart >=debut 
                                       where c.DateDepart <= fin 
                                       where c.Exercice == Convert.ToInt32(cmbExercice.Text)
                                       select c;
                    countMasculin = 0; countFeminin = 0; count = 0;
                      foreach (var c in listeMission)
                    {
                      foreach (var m in AppCode.ConnectionClass.ListeMisson(c.IDMission))
                        {
                            if (m.SiPersonnelProjet)
                            {
                                var liste = from p in AppCode.ConnectionClass.ListePersonnelProjet(Int32.Parse(m.Matricule))
                                            join s in AppCode.ConnectionClass.ListeServicePersonnelProjet(Int32.Parse(m.Matricule))
                                            on p.IDPersonelProjet equals s.IDPersonelProjet
                                            join div in AppCode.ConnectionClass.ListeDivision()
                                            on s.IDDivision equals div.IDDivision
                                            join dir in AppCode.ConnectionClass.ListeDirection()
                                            on div.IDDirection equals dir.IDDirection
                                            where dir.Direction.StartsWith(cmbDivision.Text, StringComparison.CurrentCultureIgnoreCase)
                                            select new { p.Sexe };
                                foreach (var p in liste)
                                {
                                    if (p.Sexe == "F")
                                    {
                                        countFeminin += 1;
                                    }
                                    else if (p.Sexe == "M")
                                    {
                                        countMasculin += 1;
                                    }
                                    count += liste.Count();
                                }

                            }
                            else
                            {

                                var liste = from p in AppCode.ConnectionClass.ListePersonnelParMatricule(m.Matricule)
                                            join s in AppCode.ConnectionClass.ListeServicePersonnel(m.Matricule)
                                            on p.NumeroMatricule equals s.NumeroMatricule
                                            join div in AppCode.ConnectionClass.ListeDivision()
                                            on s.IDDivision equals div.IDDivision
                                            join dir in AppCode.ConnectionClass.ListeDirection()
                                            on div.IDDirection equals dir.IDDirection
                                            where dir.Direction.StartsWith(cmbDivision.Text, StringComparison.CurrentCultureIgnoreCase)
                                            select new { p.Sexe };
                                foreach (var p in liste)
                                {
                                    if (p.Sexe == "F")
                                    {
                                        countFeminin += 1;
                                    }
                                    else if (p.Sexe == "M")
                                    {
                                        countMasculin += 1;
                                    }
                                    count += liste.Count();
                                }
                            }
                        }
                    }
                    lblNbrePersonnelmission.Text = count.ToString();
                    lblMissionMale.Text = countMasculin.ToString();
                    lblMissionFeminin.Text = countFeminin.ToString();
                    lblMission.Text = listeMission.Count().ToString();
                    if (count > 0)
                    {
                        lblMissionPourFeminin.Text = Math.Round((double)(countFeminin * 100 / count), 2).ToString() + "%";
                        lblMissionPourcentMale.Text = Math.Round((double)(countMasculin * 100 / count), 2).ToString() + "%";
                    }
                    else
                    {
                        lblMissionPourFeminin.Text = "0%";
                        lblMissionPourcentMale.Text = "0%";
                    }
                }
        
                #endregion

                #region FORMATION
                if (string.IsNullOrEmpty(mois))
                {
                    var ListeFormation = from c in AppCode.ConnectionClass.ListeFormation()
                                       where c.Exercice == Convert.ToInt32(cmbExercice.Text)
                                       select c;
                    countMasculin = 0; countFeminin = 0; count = 0;
                    foreach (var c in ListeFormation)
                    {
                        foreach (var f in AppCode.ConnectionClass.ListeFormation(c.NumeroFormation))
                        {
                            if (f.SiProjet)
                            {
                                var liste = from p in AppCode.ConnectionClass.ListePersonnelProjet(Int32.Parse(f.NumeroMatricule))
                                            join s in AppCode.ConnectionClass.ListeServicePersonnelProjet(Int32.Parse(f.NumeroMatricule))
                                            on p.IDPersonelProjet equals s.IDPersonelProjet
                                            join div in AppCode.ConnectionClass.ListeDivision()
                                            on s.IDDivision equals div.IDDivision
                                            join dir in AppCode.ConnectionClass.ListeDirection()
                                            on div.IDDirection equals dir.IDDirection
                                            where dir.Direction.StartsWith(cmbDivision.Text, StringComparison.CurrentCultureIgnoreCase)
                                            select new { p.Sexe };
                                foreach (var p in liste)
                                {
                                    if (p.Sexe == "F")
                                    {
                                        countFeminin += 1;
                                    }
                                    else if (p.Sexe == "M")
                                    {
                                        countMasculin += 1;
                                    }
                                    count += liste.Count();
                                }

                            }
                            else
                            {

                                var liste = from p in AppCode.ConnectionClass.ListePersonnelParMatricule(f.NumeroMatricule)
                                            join s in AppCode.ConnectionClass.ListeServicePersonnel(f.NumeroMatricule)
                                            on p.NumeroMatricule equals s.NumeroMatricule
                                            join div in AppCode.ConnectionClass.ListeDivision()
                                            on s.IDDivision equals div.IDDivision
                                            join dir in AppCode.ConnectionClass.ListeDirection()
                                            on div.IDDirection equals dir.IDDirection
                                            where dir.Direction.StartsWith(cmbDivision.Text, StringComparison.CurrentCultureIgnoreCase)
                                            select new { p.Sexe };
                                foreach (var p in liste)
                                {
                                    if (p.Sexe == "F")
                                    {
                                        countFeminin += 1;
                                    }
                                    else if (p.Sexe == "M")
                                    {
                                        countMasculin += 1;
                                    }
                                    count += liste.Count();
                                }
                            }
                        }
                    }
                    lblNbrePersonnelmission.Text = count.ToString();
                    lblMissionMale.Text = countMasculin.ToString();
                    lblMissionFeminin.Text = countFeminin.ToString();
                    lblMission.Text = ListeFormation.Count().ToString();
                    if (count > 0)
                    {
                        lblMissionPourFeminin.Text = Math.Round((double)(countFeminin * 100 / count), 2).ToString() + "%";
                        lblMissionPourcentMale.Text = Math.Round((double)(countMasculin * 100 / count), 2).ToString() + "%";
                    }
                    else
                    {
                        lblMissionPourFeminin.Text = "0%";
                        lblMissionPourcentMale.Text = "0%";
                    }
                }
                else
                {
                    var listeMission = from c in AppCode.ConnectionClass.ListeMisson()
                                       where c.DateDepart >= debut
                                       where c.DateDepart <= fin
                                       where c.Exercice == Convert.ToInt32(cmbExercice.Text)
                                       select c;
                    countMasculin = 0; countFeminin = 0; count = 0;
                    foreach (var c in listeMission)
                    {
                        foreach (var m in AppCode.ConnectionClass.ListeMisson(c.IDMission))
                        {
                            if (m.SiPersonnelProjet)
                            {
                                var liste = from p in AppCode.ConnectionClass.ListePersonnelProjet(Int32.Parse(m.Matricule))
                                            join s in AppCode.ConnectionClass.ListeServicePersonnelProjet(Int32.Parse(m.Matricule))
                                            on p.IDPersonelProjet equals s.IDPersonelProjet
                                            join div in AppCode.ConnectionClass.ListeDivision()
                                            on s.IDDivision equals div.IDDivision
                                            join dir in AppCode.ConnectionClass.ListeDirection()
                                            on div.IDDirection equals dir.IDDirection
                                            where dir.Direction.StartsWith(cmbDivision.Text, StringComparison.CurrentCultureIgnoreCase)
                                            select new { p.Sexe };
                                foreach (var p in liste)
                                {
                                    if (p.Sexe == "F")
                                    {
                                        countFeminin += 1;
                                    }
                                    else if (p.Sexe == "M")
                                    {
                                        countMasculin += 1;
                                    }
                                    count += liste.Count();
                                }

                            }
                            else
                            {

                                var liste = from p in AppCode.ConnectionClass.ListePersonnelParMatricule(m.Matricule)
                                            join s in AppCode.ConnectionClass.ListeServicePersonnel(m.Matricule)
                                            on p.NumeroMatricule equals s.NumeroMatricule
                                            join div in AppCode.ConnectionClass.ListeDivision()
                                            on s.IDDivision equals div.IDDivision
                                            join dir in AppCode.ConnectionClass.ListeDirection()
                                            on div.IDDirection equals dir.IDDirection
                                            where dir.Direction.StartsWith(cmbDivision.Text, StringComparison.CurrentCultureIgnoreCase)
                                            select new { p.Sexe };
                                foreach (var p in liste)
                                {
                                    if (p.Sexe == "F")
                                    {
                                        countFeminin += 1;
                                    }
                                    else if (p.Sexe == "M")
                                    {
                                        countMasculin += 1;
                                    }
                                    count += liste.Count();
                                }
                            }
                        }
                    }
                    lblNbrePersonnelmission.Text = count.ToString();
                    lblMissionMale.Text = countMasculin.ToString();
                    lblMissionFeminin.Text = countFeminin.ToString();
                    lblMission.Text = listeMission.Count().ToString();
                    if (count > 0)
                    {
                        lblMissionPourFeminin.Text = Math.Round((double)(countFeminin * 100 / count), 2).ToString() + "%";
                        lblMissionPourcentMale.Text = Math.Round((double)(countMasculin * 100 / count), 2).ToString() + "%";
                    }
                    else
                    {
                        lblMissionPourFeminin.Text = "0%";
                        lblMissionPourcentMale.Text = "0%";
                    }
                }

                #endregion
            }
            catch { }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            TB();
        }
        string mois;
        private void cmbExercice_SelectedIndexChanged(object sender, EventArgs e)
        {
            mois="";
            TB();
        }

        private void cmbDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            TB();
        }
    }
}
