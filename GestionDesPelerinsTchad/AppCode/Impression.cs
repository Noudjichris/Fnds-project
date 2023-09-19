using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using System.Drawing;
using System.Data;
using System.Linq;

namespace SGSP.AppCode
{
    public class Impression
    {
        //imprimer dossier d'autorisation
        public static Bitmap AutorisationDeStage(Stagiaire stage)
        {
   
            #region
            int unite_hauteur = 27;
            int unite_largeur = 32;
            int largeur_facture = 25 * unite_largeur;
            int hauteur_facture = 57 * unite_hauteur;

            var drawFormatCenter = new StringFormat();
            drawFormatCenter.Alignment = StringAlignment.Center;
            var drawFormatLeft = new StringFormat();
            drawFormatLeft.Alignment = StringAlignment.Near;
            var drawFormatRight = new StringFormat();
            drawFormatRight.Alignment = StringAlignment.Far ;
            //var drawFormatJustify=new str
            //creer un bit mapdra
            Bitmap bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);



            try
            {
                Image logo = global::SGSP.Properties.Resources.stagePNG;
                graphic.DrawImage(logo, 0, 10, 24 * unite_largeur, 14 * unite_hauteur);
            }
            catch { } //definir les polices 
            Font fnt1 = new Font("Arial", 12, FontStyle.Bold);
            Font fnt11 = new Font("Arial", 15, FontStyle.Bold | FontStyle.Underline);
            Font fnt3 = new Font("Arial", 25, FontStyle.Bold);
            Font fnt33 = new Font("Arial", 14, FontStyle.Regular);
              Font fnt2 = new Font("Calibri", 9, FontStyle.Bold);

            graphic.FillRectangle(Brushes.White, 10 * unite_largeur+12, 14 * unite_hauteur-7, 2 * unite_hauteur, unite_hauteur);
            graphic.DrawString("/ " + DateTime.Now.Year, fnt2, Brushes.Black, 10 * unite_largeur + 11, 14 * unite_hauteur - 7);

            var universite = ConnectionClass.ListeDesEtablissements(stage.Universite);
            var pays ="";
            if(universite.Count >0)
                pays = " (" + universite[0].Pays +")";
            var abbreviation = "";
            var direction = ConnectionClass.ListeDirection(stage.Direction);
            if(direction.Count>0)
            abbreviation = " (" + direction[0].Abreviation + ")";

            var debut =  "";
            if(stage.DateFin.Year==stage.DateDebut.Year)
                debut = ObtenirJour(stage.DateDebut) +" " + ObtenirMoisComplet(stage.DateDebut.Month)+" au " +
                    ObtenirJour(stage.DateFin) +" " + ObtenirMoisComplet(stage.DateFin.Month) + " " + stage.DateFin.Year;
            else
                debut = ObtenirJour(stage.DateDebut) + " " + ObtenirMoisComplet(stage.DateDebut.Month) +" " + stage.DateDebut.Year+ " au " +
                  ObtenirJour(stage.DateFin) + " " + ObtenirMoisComplet(stage.DateFin.Month) + " " + stage.DateFin.Year;


            graphic.DrawString(" AUTORISATION DE STAGE", fnt11, Brushes.Black, 12*unite_largeur,16*unite_hauteur, drawFormatCenter);
            var diplome = ", titulaire d’un " + stage.Diplome +
                " délivré par la " + stage.Faculte + " de l' " + stage.Universite + pays;
            if (!stage.SiDiplome)
            {
                diplome = ", étudiant en " + stage.Diplome + " à la " + stage.Faculte + " de l' " + stage.Universite + pays;
            }
            var text = "Il est autorisé à Monsieur " + stage.Nom + "  " + diplome+
                ", d’effectuer un  " + stage.NatureStage + " de " + Converti(stage.Duree) + " (" + stage.Duree + ") jours allant du " + debut +
                " à la " + stage.Direction + abbreviation + " de l’INSEED";

            RectangleF rect = new RectangleF(16, 18 *unite_hauteur-10, 23 * unite_largeur+16, 7* unite_hauteur);
            var justification = JustifyText.TextJustification.Full;
            JustifyText.DrawParagraph(graphic, rect, fnt33, Brushes.Black, text, justification, 1.2f, 1.2f, 1.2f);

            graphic.DrawString(" Le Directeur Général de l’INSEED ", fnt33, Brushes.Black, 12 * unite_largeur, 25 * unite_hauteur, drawFormatCenter);
            var listePersonnel = from p in ConnectionClass.ListePersonnel("")
                                 join s in ConnectionClass.ListeServicePersonnel()
                                 on p.NumeroMatricule equals s.NumeroMatricule
                                 where s.Poste == "Directeur Général"
                                 select new { p.Nom, p.Prenom };
            foreach (var p in listePersonnel)
            {
                graphic.DrawString(p.Nom.ToUpper() + " " + p.Prenom.ToUpper(), fnt11, Brushes.Black, 12 * unite_largeur, 31 * unite_hauteur, drawFormatCenter);
            }
            #endregion


            return bitmap;
        }

        public static Bitmap AutorisationDAbsence(Absence absence)
        {
            #region
            int unite_hauteur = 27;
            int unite_largeur = 32;
            int largeur_facture = 25 * unite_largeur;
            int hauteur_facture = 57 * unite_hauteur;

           var drawFormatCenter = new StringFormat();
            drawFormatCenter.Alignment = StringAlignment.Center;
            var drawFormatLeft = new StringFormat();
            drawFormatLeft.Alignment = StringAlignment.Near;
            var drawFormatRight = new StringFormat();
            drawFormatRight.Alignment = StringAlignment.Far;
            //var drawFormatJustify=new str
            //creer un bit mapdra
            Bitmap bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);



            try
            {
                Image logo = global::SGSP.Properties.Resources.stagePNG;
                graphic.DrawImage(logo, 0, 10, 24 * unite_largeur, 14 * unite_hauteur);
            }
            catch { } //definir les polices 
            Font fnt1 = new Font("Arial ", 15, FontStyle.Bold);
            Font fnt11 = new Font("Arial ", 17, FontStyle.Bold | FontStyle.Underline);
            Font fnt3 = new Font("Arial", 25, FontStyle.Bold);
            Font fnt33 = new Font("Aria", 15, FontStyle.Regular);
            Font fnt2 = new Font("Calibri", 9, FontStyle.Bold);

            graphic.FillRectangle(Brushes.White, 10 * unite_largeur+12, 14 * unite_hauteur-7, 2 * unite_hauteur, unite_hauteur);
            graphic.DrawString("/ "+absence.Exercice,fnt2, Brushes.Black, 10 * unite_largeur + 13, 14 * unite_hauteur - 7);
            var debut = "";
            if (absence.DateDebutAbscense.Year == absence.DateRetour.Year)
            {
                debut = "allant du " +ObtenirJour(absence.DateDebutAbscense) + " " + ObtenirMoisComplet(absence.DateDebutAbscense.Month) + " au " +
                    ObtenirJour(absence.DateRetour) + " " + ObtenirMoisComplet(absence.DateRetour.Month) + " " + absence.DateRetour.Year;
                if (absence.DateDebutAbscense.Month == absence.DateRetour.Month)
                {
                    debut = "allant du "+ObtenirJour(absence.DateDebutAbscense) + " au " +
                  ObtenirJour(absence.DateRetour) + " " + ObtenirMoisComplet(absence.DateRetour.Month) + " " + absence.DateRetour.Year;
                }
                if (absence.DateDebutAbscense == absence.DateRetour)
                {
                    debut = "le "+ObtenirJour(absence.DateRetour) + " " + ObtenirMoisComplet(absence.DateRetour.Month) + " " + absence.DateRetour.Year;
                 }
            }
            else
                debut = "allant du "+ ObtenirJour(absence.DateDebutAbscense) + " " + ObtenirMoisComplet(absence.DateDebutAbscense.Month) + " " + absence.DateDebutAbscense.Year + " au " +
                  ObtenirJour(absence.DateRetour) + " " + ObtenirMoisComplet(absence.DateRetour.Month) + " " + absence.DateRetour.Year;


            graphic.DrawString("AUTORISATION D’ABSENCE", fnt11, Brushes.Black, 12*unite_largeur, 15 * unite_hauteur+10, drawFormatCenter);

            var fonction = absence.Fonction + " à l’Institut National de la Statistique, des Etudes Economiques et Démographiques (INSEED) ";
            if (absence.SiPersonnelProjet)
            {
                var listePersProjet = ConnectionClass.ListeServicePersonnelProjet(int.Parse(absence.NumeroEmploye));
                var projet = ConnectionClass.ListeDesProjetParIDProjet(listePersProjet[0].IDProjet)[0].NomProjet;
                fonction = absence.Fonction + " au " + projet +" de l'INSEED";
            }
            var dureeEnLettre="de "+Converti(absence.Duree) ;
            if (absence.Duree == 1 || absence.Duree == 1)
            {
                dureeEnLettre = "d' " + Converti(absence.Duree);
            }
            var text = "Il est accordé une autorisation d’absence "+dureeEnLettre+" ("+absence.Duree +") jours, "+debut+" à "+absence.NomPersonnel.ToUpper()+
                ", "+fonction+", pour se rendre "+absence.Destination+".";

            RectangleF rect = new RectangleF(16, 17 * unite_hauteur - 0, 23 * unite_largeur + 16, 5* unite_hauteur+10);
            
            //DrawText(graphic, text, rectangle, StringAlignment.Far, fnt33);
            var justification = JustifyText.TextJustification.Full;
            JustifyText.DrawParagraph(graphic, rect, fnt33, Brushes.Black, text, justification, 1.2f, 1.2f, 1.2f);

            graphic.DrawString("Motif : ", fnt1, Brushes.Black, 16, 23* unite_hauteur+10);
            graphic.DrawString(absence.Motif, fnt33, Brushes.Black, 3 * unite_largeur, 23 * unite_hauteur+10);

            var rect2 = new RectangleF(16, 24 * unite_hauteur + 20, 23 * unite_largeur+16,2 * unite_largeur);

           JustifyText.DrawParagraph(graphic, rect2, fnt33, Brushes.Black, "En foi de quoi, la présente autorisation d’absence est établie pour servir et valoir ce que de droit. ", justification, 1.2f, 1f, 1f);
           
            graphic.DrawString(" Le Directeur Général de l’INSEED ", fnt33, Brushes.Black, 12 * unite_largeur, 28* unite_hauteur+10, drawFormatCenter);
            var listePersonnel = from p in ConnectionClass.ListePersonnel("")
                                 join s in ConnectionClass.ListeServicePersonnel()
                                 on p.NumeroMatricule equals s.NumeroMatricule
                                 where s.Poste == "Directeur Général"
                                 select new { p.Nom, p.Prenom };
            foreach (var p in listePersonnel)
            {
                graphic.DrawString(p.Nom.ToUpper() + " " + p.Prenom.ToUpper(), fnt11, Brushes.Black, 12 * unite_largeur, 34 * unite_hauteur+10, drawFormatCenter);
            }
            #endregion


            return bitmap;
        }
        //imprimer dossier d'attestation
        public static Bitmap AttestationDeStage(Stagiaire stage)
        {

            #region
            int unite_hauteur = 25;
            int unite_largeur = 32;
            int largeur_facture = 25 * unite_largeur;
            int hauteur_facture = 46* unite_hauteur+5;

            var drawFormatCenter = new StringFormat();
            drawFormatCenter.Alignment = StringAlignment.Center;
            var drawFormatLeft = new StringFormat();
            drawFormatLeft.Alignment = StringAlignment.Near;
            var drawFormatRight = new StringFormat();
            drawFormatRight.Alignment = StringAlignment.Far;
            //var drawFormatJustify=new str
            //creer un bit mapdra
            Bitmap bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            var universite = ConnectionClass.ListeDesEtablissements(stage.Universite);
            var pays = "";
            if (universite.Count > 0)
                pays = " (" + universite[0].Pays + ")";
            var abbreviation = "";
            var direction = ConnectionClass.ListeDirection(stage.Direction);
            if (direction.Count > 0)
                abbreviation = " (" + direction[0].Abreviation + ")";

            var debut = "";
            if (stage.DateFin.Year == stage.DateDebut.Year)
                debut = ObtenirJour(stage.DateDebut) + " " + ObtenirMoisComplet(stage.DateDebut.Month) + " au " +
                    ObtenirJour(stage.DateFin) + " " + ObtenirMoisComplet(stage.DateFin.Month) + " " + stage.DateFin.Year;
            else
                debut = ObtenirJour(stage.DateDebut) + " " + ObtenirMoisComplet(stage.DateDebut.Month) + " " + stage.DateDebut.Year + " au " +
                  ObtenirJour(stage.DateFin) + " " + ObtenirMoisComplet(stage.DateFin.Month) + " " + stage.DateFin.Year;


            try
            {
                Image logo = global::SGSP.Properties.Resources.stage_1;
                graphic.DrawImage(logo, 0, 0, largeur_facture, hauteur_facture);
            }
            catch { } //definir les polices 
            Font fnt1 = new Font("Arial ", 12, FontStyle.Bold);
            Font fnt11 = new Font("Arial ", 15, FontStyle.Bold | FontStyle.Underline);
            Font fnt3 = new Font("Arial ", 22, FontStyle.Bold | FontStyle.Underline);
            Font fnt33 = new Font("Arial ", 14, FontStyle.Regular);

            graphic.DrawString(" ATTESTATION DE STAGE", fnt3, Brushes.Black, 12 * unite_largeur, 19 * unite_hauteur+10, drawFormatCenter);
            var text = "Je soussigné, Directeur Général de l’Institut National de la Statistique, des Etudes Economiques et Démographiques (INSEED) "+
                "atteste que Monsieur "+stage.Nom+", étudiant en "+stage.Diplome+" à la Faculté des Sciences Humaines à l’Université de Ngaoundéré "+
                "(Cameroun), a suivi avec satisfaction un stage académique de "+Converti(stage.Duree) + "("+stage.Duree +")jours allant du "+debut +" à "+stage.Direction+" de l’INSEED. ";
      
            RectangleF rect = new RectangleF(unite_largeur+10, 21* unite_hauteur + 10, 23 * unite_largeur - 15, 7 * unite_hauteur);
            var justification = JustifyText.TextJustification.Full;
            JustifyText.DrawParagraph(graphic, rect, fnt33, Brushes.Black, text, justification, 1.1f, 1.1f, 1.1f);

            var text1 = "Pendant cette période, l’intéressé a fait preuve d’assiduité et de régularité.";
            RectangleF rect1 = new RectangleF(unite_largeur+10, 29 * unite_hauteur + 10, 23* unite_largeur - 15, 1* unite_hauteur);
    
            JustifyText.DrawParagraph(graphic, rect1, fnt33, Brushes.Black, text1, justification, 1.2f, 1.2f, 1.2f);

            rect1 = new RectangleF(unite_largeur + 10, 31 * unite_hauteur + 0, 23 * unite_largeur - 15, 4 * unite_hauteur);
           text1 =  "En foi de quoi, la présente attestation de stage lui est délivrée, pour servir et valoir ce que de droit. ";
            //var text2 = "En foi de quoi, la présente attestation de stage lui est délivrée, pour servir et valoir ce que de droit. ";
            //RectangleF rect2 = new RectangleF(unite_largeur, 33 * unite_hauteur + 10, 23 * unite_largeur - 5, 2 * unite_hauteur);
            graphic.DrawString(text1, fnt33, Brushes.Black, rect1, drawFormatLeft);
            graphic.DrawString(" Le Directeur Général de l’INSEED ", fnt33, Brushes.Black, 12 * unite_largeur, 34 * unite_hauteur, drawFormatCenter);
            var listePersonnel = from p in ConnectionClass.ListePersonnel("")
                                 join s in ConnectionClass.ListeServicePersonnel()
                                 on p.NumeroMatricule equals s.NumeroMatricule
                                 where s.Poste == "Directeur Général"
                                 select new { p.Nom, p.Prenom };
            foreach (var p in listePersonnel)
            {
                graphic.DrawString(p.Nom.ToUpper() + " " + p.Prenom.ToUpper(), fnt11, Brushes.Black, 12 * unite_largeur, 40 * unite_hauteur, drawFormatCenter);
            }
            #endregion


            return bitmap;
        }
     
        public static Bitmap LettreConge(Conge  conge)
        {
            try
            {
                #region
                int unite_hauteur = 20;
                int unite_largeur = 32;
                int largeur_facture = 25 * unite_largeur;
                int hauteur_facture = 57 * unite_hauteur;

                var drawFormatCenter = new StringFormat();
                drawFormatCenter.Alignment = StringAlignment.Center;
                var drawFormatLeft = new StringFormat();
                drawFormatLeft.Alignment = StringAlignment.Near;
                var drawFormatRight = new StringFormat();
                drawFormatRight.Alignment = StringAlignment.Far;
                //var drawFormatJustify=new str
                //creer un bit mapdra
                Bitmap bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                // creer un objet graphic
                Graphics graphic = Graphics.FromImage(bitmap);

                //la couleur de l'image
                graphic.Clear(Color.White);



                try
                {
                    Image logo = global::SGSP.Properties.Resources.stagePNG;
                    graphic.DrawImage(logo, 0, 10, 24 * unite_largeur, 14 * unite_hauteur);
                }
                catch { } //definir les polices 
                Font fnt1 = new Font("Arial Narrow ", 11.5f, FontStyle.Bold);
                Font fnt11 = new Font("Arial ", 17, FontStyle.Bold | FontStyle.Underline);
                Font fnt3 = new Font("Arial", 25, FontStyle.Bold);
                Font fnt33 = new Font("Arial Narrow", 12, FontStyle.Regular);
                Font fnt2 = new Font("Calibri", 9, FontStyle.Bold);

                graphic.FillRectangle(Brushes.White, 10 * unite_largeur + 12, 14 * unite_hauteur - 7, 2 * unite_hauteur, unite_hauteur);
                graphic.DrawString("/ " + conge.Exercice, fnt2, Brushes.Black, 10 * unite_largeur + 13, 14 * unite_hauteur - 7);
                var debut = "";
                if (conge.DateDebutConge.Year == conge.DateRetour.Year)
                {
                    debut = ObtenirJour(conge.DateDebutConge) + " " + ObtenirMoisComplet(conge.DateDebutConge.Month) + " au " +
                        ObtenirJour(conge.DateRetour) + " " + ObtenirMoisComplet(conge.DateRetour.Month) + " " + conge.DateRetour.Year;
                    if (conge.DateDebutConge.Month == conge.DateRetour.Month)
                    {
                        debut = ObtenirJour(conge.DateDebutConge) +" au " +
                      ObtenirJour(conge.DateRetour) + " " + ObtenirMoisComplet(conge.DateRetour.Month) + " " + conge.DateRetour.Year;
                    }
                    if (conge.DateDebutConge == conge.DateRetour)
                    {
                        debut = ObtenirJour(conge.DateRetour) + " " + ObtenirMoisComplet(conge.DateRetour.Month) + " " + conge.DateRetour.Year;
                    }
                }
                else
                    debut = ObtenirJour(conge.DateDebutConge) + " " + ObtenirMoisComplet(conge.DateDebutConge.Month) + " " + conge.DateDebutConge.Year + " au " +
                      ObtenirJour(conge.DateRetour) + " " + ObtenirMoisComplet(conge.DateRetour.Month) + " " + conge.DateRetour.Year;

                var fonction = conge.Fonction + " à l' INSEED";
                var prefixe = "Monsieur";

                if (!conge.SiProjet)
                {
                    if (ConnectionClass.ListePersonnelParMatricule(conge.NumeroMatricule)[0].Sexe == "F")
                    {
                        if (ConnectionClass.ListePersonnelParMatricule(conge.NumeroMatricule)[0].SituationMatrimoniale.Contains("Mari") ||
                            string.IsNullOrEmpty(ConnectionClass.ListePersonnelParMatricule(conge.NumeroMatricule)[0].SituationMatrimoniale))
                        {
                            prefixe = "Madame";
                        }
                        else
                        {
                            prefixe = "Mademoiselle";
                        }
                    }
                }
               else 
                {
                    var listePersProjet = ConnectionClass.ListeServicePersonnelProjet(int.Parse(conge.NumeroMatricule));
                    var projet = ConnectionClass.ListeDesProjetParIDProjet(listePersProjet[0].IDProjet)[0].NomProjet;
                    fonction = conge.Fonction + " au " + projet + "/INSEED";
                    if (ConnectionClass.ListePersonnelProjet(Convert.ToInt32(conge.NumeroMatricule))[0].Sexe == "F")
                    {
                        prefixe = "Madame";
                    }
                }
                var duree = (int)conge.Duree / 30;
                var mois = Converti(duree) + "(" + duree + ") mois à ";
               
                if (conge.Duree < 28)
                {
                    mois = Converti(duree) + "(" + duree + ") jour(s) à ";
                    if (duree == 0)
                    {
                        mois = Converti(conge.Duree) + "(" + conge.Duree + ") jours à ";
                    }
                }
                else if (conge.Duree > 30 && conge.Duree % 30 >= 2)
                {
                    var jour = conge.Duree % 30;
                    mois = Converti(duree) + " ("+duree +") mois et " + Converti(jour) + "("+jour+")jour(s) à ";
                }
                if (mois.StartsWith("u"))
                {
                    mois = "d' u" + mois.Substring(1);
                }
                else
                {
                    mois = "de " + mois;
                }
                graphic.DrawString("DECISION N°__________/INSEED/DAFRH/DARH/SGAP/" + conge.Exercice, fnt1, Brushes.Black, 12 * unite_largeur, 16 * unite_hauteur + 10, drawFormatCenter);
                graphic.DrawString("Portant congé administratif régulier  " + mois + prefixe, fnt33, Brushes.Black, 12 * unite_largeur, 17 * unite_hauteur + 10, drawFormatCenter);
                graphic.DrawString(conge.NomPersonnel.ToUpper(), fnt1, Brushes.Black, 12 * unite_largeur, 18 * unite_hauteur + 10, drawFormatCenter);
                graphic.DrawString(fonction, fnt33, Brushes.Black, 12 * unite_largeur, 19 * unite_hauteur + 10, drawFormatCenter);

                graphic.DrawString("LE DIRECTEUR GENERAL DE L’INSTITUT NATIONAL DE LA\n" +
    "STATISTIQUE, DES ETUDES ECONOMIQUES ET DEMOGRAPHIQUES", fnt1, Brushes.Black, 12 * unite_largeur, 21 * unite_hauteur + 5, drawFormatCenter);

                graphic.DrawString("Vu ", fnt1, Brushes.Black, 2 * unite_largeur, 23 * unite_hauteur + 10, drawFormatLeft);
                graphic.DrawString("la charte de la transition ; ", fnt33, Brushes.Black, 2 * unite_largeur + 25, 23 * unite_hauteur + 10, drawFormatLeft);
                graphic.DrawString("Vu ", fnt1, Brushes.Black, 2 * unite_largeur, 24 * unite_hauteur + 10, drawFormatLeft);
                graphic.DrawString("la Loi N° 38/PR/96 du 11 décembre 1996, portant Code du Travail ; ", fnt33, Brushes.Black, 2 * unite_largeur + 25, 24 * unite_hauteur + 10, drawFormatLeft);
                graphic.DrawString("Vu ", fnt1, Brushes.Black, 2 * unite_largeur, 25 * unite_hauteur + 10, drawFormatLeft);
                graphic.DrawString(" le Décret N° 567/PR/PM/MFPT07, du 31 juillet 2007, fixant le régime des congés et des autorisations \n" +
                "d’absence exceptionnelles des agents fonctionnaires et contractuels ;", fnt33, Brushes.Black, 2 * unite_largeur + 25, 25 * unite_hauteur + 10, drawFormatLeft);
                graphic.DrawString("Vu ", fnt1, Brushes.Black, 2 * unite_largeur, 27 * unite_hauteur + 5, drawFormatLeft);
                graphic.DrawString("la convention collective du 15 décembre 2012, applicables aux agents contractuels des services publics \nde la République du Tchad ;  ", fnt33, Brushes.Black, 2 * unite_largeur + 25, 27 * unite_hauteur + 5, drawFormatLeft);
                graphic.DrawString("Vu ", fnt1, Brushes.Black, 2 * unite_largeur, 29 * unite_hauteur + 0, drawFormatLeft);
                graphic.DrawString("la demande de congé formulée par l’intéressé en date du " + conge.DateDemande.ToShortDateString() + ".", fnt33, Brushes.Black, 2 * unite_largeur + 25, 29 * unite_hauteur + 0, drawFormatLeft);

                graphic.DrawString("DECIDE", fnt1, Brushes.Black, 12 * unite_largeur, 30 * unite_hauteur + 5, drawFormatCenter);

                fonction = conge.Fonction;
                if (conge.SiProjet)
                    fonction = conge.Fonction + " au ";
                var text1 = "Article 1 : Il est accordé un " + conge.NatureConge + " " + mois.Remove(mois.Length - 2) + ", allant du " + debut + "  inclus à "+prefixe+" " + conge.NomPersonnel.ToUpper() + "," +
                    fonction + " à l’Institut National de la Statistique, des Etudes Economiques et Démographiques (INSEED). ";

                RectangleF rect = new RectangleF(2 * unite_largeur, 31 * unite_hauteur + 15, 21 * unite_largeur + 0, 3 * unite_hauteur + 10);

                //DrawText(graphic, text, rectangle, StringAlignment.Far, fnt33);
                var justification = JustifyText.TextJustification.Full;
                JustifyText.DrawParagraph(graphic, rect, fnt33, Brushes.Black, text1, justification, 1.2f, 1.2f, 1.2f);

                rect = new RectangleF(2 * unite_largeur, 35 * unite_hauteur - 0, 21 * unite_largeur + 0, 3 * unite_hauteur + 10);
                text1 = "Article 2 : la présente décision qui prend effet pour compter de la date ci-dessus indiquée, sera enregistrée et communiquée partout où besoin sera.";
                JustifyText.DrawParagraph(graphic, rect, fnt33, Brushes.Black, text1, justification, 1.2f, 1.2f, 1.2f);

                graphic.DrawString("Fait à N'Djaména le" + DateTime.Now.Date.ToShortDateString(), fnt33, Brushes.Black, 12 * unite_largeur, 38 * unite_hauteur + 5, drawFormatCenter);

                graphic.DrawString(" Le Directeur Général de l’INSEED ", fnt1, Brushes.Black, 12 * unite_largeur, 40 * unite_hauteur, drawFormatCenter);
                var listePersonnel = from p in ConnectionClass.ListePersonnel("")
                                     join s in ConnectionClass.ListeServicePersonnel()
                                     on p.NumeroMatricule equals s.NumeroMatricule
                                     where s.Poste == "Directeur Général"
                                     select new { p.Nom, p.Prenom };
                foreach (var p in listePersonnel)
                {
                    graphic.DrawString(p.Nom.ToUpper() + " " + p.Prenom.ToUpper(), fnt11, Brushes.Black, 12 * unite_largeur, 46 * unite_hauteur, drawFormatCenter);
                }
                #endregion
                return bitmap;
            }
            catch (Exception)
            {
                return null;
            }
        }
     
        //imprimer dossier d'un personnel
        public static Bitmap ImprimerInformatonDunPersonnel(string numeroMatricule)
        {
            var personnels = ConnectionClass.ListePersonnelParMatricule(numeroMatricule);
            var services = ConnectionClass.ListeServicePersonnel(numeroMatricule);
            var salaires = ConnectionClass.ListeSalaire(numeroMatricule);

            #region
            int unite_hauteur = 25;
            int unite_largeur = 32;
            int largeur_facture = 24 * unite_largeur ;
            int hauteur_facture = 57 * unite_hauteur ;

            //creer un bit map
            Bitmap bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);

            try
            {
                var image = personnels[0].Photo;
                graphic.DrawImage(Image.FromFile(image), 19* unite_largeur+15,5* unite_hauteur+5, 5* unite_largeur, 10 * unite_hauteur-10);
            }
            catch { } //definir les polices 
            Font fnt1 = new Font("Arial Unicode MS", 12, FontStyle.Bold);
            Font fnt11 = new Font("Arial Unicode MS", 13, FontStyle.Bold);
            Font fnt3 = new Font("Arial Unicode MS", 25, FontStyle.Bold);
            Font fnt33 = new Font("Arial Unicode MS", 12, FontStyle.Regular);

            #endregion

            graphic.FillRectangle(Brushes.SaddleBrown,2* unite_largeur, unite_hauteur, unite_largeur * 22, unite_hauteur+15);
            graphic.DrawString("Details personnel ", fnt3, Brushes.White,9* unite_largeur,  unite_hauteur-5);

                    #region
            graphic.DrawString("Etat civil ", fnt11, Brushes.Black, 2 * unite_largeur, 4 * unite_hauteur - 5);
            graphic.FillRectangle(Brushes.SaddleBrown, 2*unite_largeur, 5* unite_hauteur-5, unite_largeur * 22, 3);

            graphic.DrawString("Matricule :", fnt1, Brushes.Black, 2 * unite_largeur, 6 * unite_hauteur-10);
            graphic.DrawString("Nom :", fnt1, Brushes.Black, 2 * unite_largeur, 7 * unite_hauteur - 10);
            graphic.DrawString("Prenom  :", fnt1, Brushes.Black, 2 * unite_largeur, 8 * unite_hauteur - 10);
            graphic.DrawString("Né(e) le  :", fnt1, Brushes.Black, 2 * unite_largeur, 9 * unite_hauteur - 10);
            graphic.DrawString("à :", fnt1, Brushes.Black, 2 * unite_largeur, 10 * unite_hauteur - 10);
            graphic.DrawString("Age :", fnt1, Brushes.Black, 2 * unite_largeur, 11 * unite_hauteur - 10);
            graphic.DrawString("Sexe :", fnt1, Brushes.Black, 2 * unite_largeur, 12 * unite_hauteur - 10);
            graphic.DrawString("Diplome :", fnt1, Brushes.Black, 2 * unite_largeur, 13 * unite_hauteur-10);      

            graphic.DrawString("Adresses et contacts ", fnt11, Brushes.Black, 2 * unite_largeur, 14 * unite_hauteur +5);
            graphic.FillRectangle(Brushes.SaddleBrown, 2 * unite_largeur, 15 * unite_hauteur +5, unite_largeur * 22, 3);
            graphic.DrawString("Adresse :", fnt1, Brushes.Black, 2 * unite_largeur, 16 * unite_hauteur);
            graphic.DrawString("Télephone :", fnt1, Brushes.Black, 2 * unite_largeur, 17 * unite_hauteur);
            graphic.DrawString("Email :", fnt1, Brushes.Black, 2 * unite_largeur, 18 * unite_hauteur);

            graphic.DrawString("Services ", fnt11, Brushes.Black, 2 * unite_largeur, 19 * unite_hauteur + 6);
            graphic.FillRectangle(Brushes.SaddleBrown, 2 * unite_largeur, 20 * unite_hauteur + 5, unite_largeur * 22, 3);
            graphic.DrawString("Récruté(e) le :", fnt1, Brushes.Black, 2 * unite_largeur, 21 * unite_hauteur);
            graphic.DrawString("Au poste de :", fnt1, Brushes.Black, 2 * unite_largeur, 22 * unite_hauteur);
            graphic.DrawString("Grade:", fnt1, Brushes.Black, 2 * unite_largeur, 23 * unite_hauteur);
            graphic.DrawString("Type de contrat ", fnt1, Brushes.Black, 2 * unite_largeur, 24 * unite_hauteur);
            graphic.DrawString("Catégorie :", fnt1, Brushes.Black, 2 * unite_largeur, 25 * unite_hauteur);
            graphic.DrawString("Echelon :", fnt1, Brushes.Black, 2 * unite_largeur, 26 * unite_hauteur);
            graphic.DrawString("Ancienneté :", fnt1, Brushes.Black, 2 * unite_largeur, 27 * unite_hauteur);
            graphic.DrawString("Status :", fnt1, Brushes.Black, 2 * unite_largeur, 28 * unite_hauteur);
            graphic.DrawString("Date depart :", fnt1, Brushes.Black, 2 * unite_largeur, 29 * unite_hauteur);
            graphic.DrawString("Direction :", fnt1, Brushes.Black, 2 * unite_largeur, 30 * unite_hauteur);
            graphic.DrawString("Division :", fnt1, Brushes.Black, 2 * unite_largeur, 31 * unite_hauteur);
            graphic.DrawString("Service :", fnt1, Brushes.Black, 2 * unite_largeur, 32 * unite_hauteur);
            graphic.DrawString("Localisation :", fnt1, Brushes.Black, 2 * unite_largeur, 33* unite_hauteur);

            graphic.DrawString("Services sociaux", fnt11, Brushes.Black, 2 * unite_largeur, 34* unite_hauteur + 6);
            graphic.FillRectangle(Brushes.SaddleBrown, 2 * unite_largeur, 35 * unite_hauteur + 5, unite_largeur * 22, 3);
            graphic.DrawString("No CNPS :", fnt1, Brushes.Black, 2 * unite_largeur, 36 * unite_hauteur-6);
            //graphic.DrawString("Numéro social : ", fnt1, Brushes.Black, 2 * unite_largeur, 35 * unite_hauteur);

            graphic.DrawString("Renumérations", fnt11, Brushes.Black, 2 * unite_largeur, 37* unite_hauteur + 0);
            graphic.FillRectangle(Brushes.SaddleBrown, 2 * unite_largeur, 38 * unite_hauteur + 0, unite_largeur * 22, 3);
             graphic.DrawString("Salaire :", fnt1, Brushes.Black, 2 * unite_largeur, 39 * unite_hauteur-6);
             graphic.DrawString("Prime de motivation  :", fnt1, Brushes.Black, 2 * unite_largeur, 40 * unite_hauteur - 6);
             graphic.DrawString("Frais de communication  :", fnt1, Brushes.Black, 2 * unite_largeur, 41 * unite_hauteur - 6);
             graphic.DrawString("Indemnités :", fnt1, Brushes.Black, 13 * unite_largeur, 39 * unite_hauteur - 6);
             graphic.DrawString("Prime de transport  :", fnt1, Brushes.Black, 13 * unite_largeur, 40 * unite_hauteur - 6);
             graphic.DrawString("Autres primes  :", fnt1, Brushes.Black, 13 * unite_largeur, 41 * unite_hauteur - 6);

             graphic.DrawString("Service bancaire", fnt11, Brushes.Black, 2 * unite_largeur, 42 * unite_hauteur + 0);
             graphic.FillRectangle(Brushes.SaddleBrown, 2 * unite_largeur, 43 * unite_hauteur + 5, unite_largeur * 22, 3);
             graphic.DrawString("Nom banque :", fnt1, Brushes.Black, 2 * unite_largeur, 44 * unite_hauteur);
             graphic.DrawString("Compte bancaire :", fnt1, Brushes.Black, 2 * unite_largeur, 45* unite_hauteur);
             graphic.DrawString("Code guichet :", fnt1, Brushes.Black, 2 * unite_largeur, 46 * unite_hauteur);
             graphic.DrawString("Code banque :", fnt1, Brushes.Black, 2 * unite_largeur, 47* unite_hauteur);
             graphic.DrawString("Clé :", fnt1, Brushes.Black, 2 * unite_largeur, 48 * unite_hauteur);
            graphic.FillRectangle(Brushes.SaddleBrown, 2 * unite_largeur, 49 * unite_hauteur + 5, unite_largeur * 22, 3);

            #endregion
            graphic.DrawString(personnels[0].NumeroMatricule, fnt33, Brushes.Black, 8 * unite_largeur, 6 * unite_hauteur-10);
            graphic.DrawString(personnels[0].Nom, fnt33, Brushes.Black, 8 * unite_largeur, 7 * unite_hauteur-10);
            graphic.DrawString(personnels[0].Prenom, fnt33, Brushes.Black, 8 * unite_largeur, 8 * unite_hauteur-10);
            graphic.DrawString(personnels[0].DateNaissance.ToShortDateString(), fnt33, Brushes.Black, 8 * unite_largeur, 9 * unite_hauteur-10);
            graphic.DrawString(personnels[0].LieuNaissance, fnt33, Brushes.Black, 8 * unite_largeur, 10 * unite_hauteur-10);
            graphic.DrawString(personnels[0].Age.ToString(), fnt33, Brushes.Black, 8 * unite_largeur, 11 * unite_hauteur-10);
            graphic.DrawString(personnels[0].Sexe, fnt33, Brushes.Black, 8 * unite_largeur, 12 * unite_hauteur-10);
            graphic.DrawString(services[0].Diplome, fnt33, Brushes.Black, 8 * unite_largeur, 13 * unite_hauteur - 10);
            graphic.DrawString(personnels[0].Adresse, fnt33, Brushes.Black, 8 * unite_largeur, 16 * unite_hauteur);
            graphic.DrawString(personnels[0].Telephone1 + " / " + personnels[0].Telephone2        , fnt33, Brushes.Black, 8 * unite_largeur, 17 * unite_hauteur);
            graphic.DrawString(personnels[0].Email, fnt33, Brushes.Black, 8 * unite_largeur, 18 * unite_hauteur);
            graphic.DrawString(services[0].DateService.ToShortDateString(), fnt33, Brushes.Black, 8 * unite_largeur, 21 * unite_hauteur);
            graphic.DrawString(services[0].Poste, fnt33, Brushes.Black, 8 * unite_largeur, 22 * unite_hauteur);
            graphic.DrawString(services[0].Grade, fnt33, Brushes.Black, 8 * unite_largeur, 23 * unite_hauteur);
            graphic.DrawString(services[0].TypeContrat, fnt33, Brushes.Black, 8 * unite_largeur, 24 * unite_hauteur);
            graphic.DrawString(services[0].Categorie, fnt33, Brushes.Black, 8 * unite_largeur, 25 * unite_hauteur);
            graphic.DrawString(services[0].Echelon, fnt33, Brushes.Black, 8 * unite_largeur, 26 * unite_hauteur);

            //graphic.DrawString(dtService.Rows[0].ItemArray[5].ToString(), fnt33, Brushes.Black, 8 * unite_largeur, 25 * unite_hauteur);
            graphic.DrawString(services[0].Anciennete, fnt33, Brushes.Black, 8 * unite_largeur, 27 * unite_hauteur);
            if(ConnectionClass.StatusContrat(services[0].Status))
            {
                graphic.DrawString(services[0].Status, fnt33, Brushes.Red, 8 * unite_largeur, 28 * unite_hauteur);
            }
            else
            {
                graphic.DrawString(services[0].Status, fnt33, Brushes.Green, 8 * unite_largeur, 28 * unite_hauteur);
            }
            graphic.DrawString(services[0].DateDepart.ToShortDateString(), fnt33, Brushes.Black, 8 * unite_largeur, 29 * unite_hauteur);

                        var direction = from dir in ConnectionClass.ListeDirection()
                            join div in ConnectionClass.ListeDivision()
                            on dir.IDDirection equals div.IDDirection
                            where div.IDDivision == services[0].IDDivision
                            select dir.Direction;
            foreach (var dir in direction)
            {
                graphic.DrawString(dir, fnt33, Brushes.Black, 8 * unite_largeur, 30 * unite_hauteur);
            }
            var division = from div in ConnectionClass.ListeDivision()
                           where div.IDDivision == services[0].IDDivision
                           select div.Division;
            foreach (var div in division)
            {
                graphic.DrawString(div, fnt33, Brushes.Black, 8 * unite_largeur, 31 * unite_hauteur);
            }
            var departement = ConnectionClass.ListeDepartement(services[0].IDDepartement);
            if (departement.Count > 0)
            {
                graphic.DrawString(departement[0].Departement, fnt33, Brushes.Black, 8 * unite_largeur, 32 * unite_hauteur);
            }
            graphic.DrawString(services[0].Localite, fnt33, Brushes.Black, 8 * unite_largeur, 33 * unite_hauteur);

            graphic.DrawString(services[0].NoCNPS, fnt33, Brushes.Black, 8 * unite_largeur, 36 * unite_hauteur-6);
            graphic.DrawString("", fnt33, Brushes.Black, 8 * unite_largeur, 36 * unite_hauteur);

            graphic.DrawString(string.Format(elGR, "{0:0,0}", salaires[0].SalaireBase), fnt33, Brushes.Black, 8 * unite_largeur+12, 39 * unite_hauteur-6);
            graphic.DrawString(string.Format(elGR, "{0:0,0}", salaires[0].PrimeMotivation), fnt33, Brushes.Black, 8 * unite_largeur+12, 40 * unite_hauteur-6);
            graphic.DrawString(string.Format(elGR, "{0:0,0}", salaires[0].FraisCommunication), fnt33, Brushes.Black, 8 * unite_largeur+12, 41 * unite_hauteur - 6);
            graphic.DrawString(string.Format(elGR, "{0:0,0}", salaires[0].Indemnites), fnt33, Brushes.Black, 19 * unite_largeur, 39 * unite_hauteur - 6);
            graphic.DrawString(string.Format(elGR, "{0:0,0}", salaires[0].PrimeTransport), fnt33, Brushes.Black, 19 * unite_largeur, 40 * unite_hauteur - 6);
            graphic.DrawString(string.Format(elGR, "{0:0,0}", salaires[0].AutresPrimes), fnt33, Brushes.Black, 19 * unite_largeur, 41 * unite_hauteur - 6);

            var banks = ConnectionClass.ListeDonneesBancaires(numeroMatricule);
            graphic.DrawString(banks[0].NomBanque, fnt33, Brushes.Black, 8 * unite_largeur, 44 * unite_hauteur);
            graphic.DrawString(banks[0].Compte, fnt33, Brushes.Black, 8 * unite_largeur, 45 * unite_hauteur);
            graphic.DrawString(banks[0].CodeGuichet, fnt33, Brushes.Black, 8 * unite_largeur, 46 * unite_hauteur);
            graphic.DrawString(banks[0].CodeBanque, fnt33, Brushes.Black, 8 * unite_largeur, 47 * unite_hauteur);
            graphic.DrawString(banks[0].Cle, fnt33, Brushes.Black, 8 * unite_largeur, 48 * unite_hauteur);

            graphic.DrawString("Informations Supplémentaires", fnt11, Brushes.Black, 2 * unite_largeur, 50 * unite_hauteur + 6);
            graphic.FillRectangle(Brushes.SaddleBrown, 2 * unite_largeur, 51 * unite_hauteur + 5, unite_largeur * 22, 3);
            graphic.DrawString("Situation matrimoniale :", fnt1, Brushes.Black, 2 * unite_largeur, 52 * unite_hauteur);
            graphic.DrawString("Nombre d'enfants : ", fnt1, Brushes.Black, 2 * unite_largeur, 53 * unite_hauteur);
            graphic.DrawString(personnels[0].SituationMatrimoniale, fnt33, Brushes.Black, 8 * unite_largeur, 52 * unite_hauteur);
            graphic.DrawString(personnels[0].NombreEnfant.ToString(), fnt33, Brushes.Black, 8 * unite_largeur, 53* unite_hauteur);
            graphic.DrawString("Piece :", fnt1, Brushes.Black, 2 * unite_largeur - 0, 54 * unite_hauteur - 0);
            graphic.DrawString("N° piece :", fnt1, Brushes.Black, 2 * unite_largeur - 0, 55 * unite_hauteur - 0);
            graphic.DrawString(personnels[0].TypePiece, fnt33, Brushes.Black, 8 * unite_largeur + 0, 54 * unite_hauteur - 0);
            graphic.DrawString(personnels[0].NumeroPiece, fnt33, Brushes.Black, 8 * unite_largeur + 0, 55 * unite_hauteur - 0);
            return bitmap;
        }

        //imprimer dossier d'un personnel
        public static Bitmap ImprimerFormation(string numeroMatricule)
        {
            #region
            int unite_hauteur = 21;
            int unite_largeur = 32;
            int largeur_facture = 25 * unite_largeur + 10;
            int hauteur_facture = 54 * unite_hauteur ;

            //creer un bit map
            Bitmap bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);

            try
            {
                Image logo = global::SGSP.Properties.Resources.Logo;
                graphic.DrawImage(logo, 11 * unite_largeur, unite_hauteur, 4 * unite_largeur, 3 * unite_hauteur);
            }
            catch { } //definir les polices 
            Font fnt1 = new Font("Arial Unicode MS", 9, FontStyle.Italic);
            Font fnt0 = new Font("Arial Unicode MS", 18, FontStyle.Bold);
            Font fnt11 = new Font("Arial Unicode MS", 12, FontStyle.Regular);
            Font fnt3 = new Font("Arial Unicode MS", 30, FontStyle.Bold);
            Font fnt33 = new Font("Arial Unicode MS", 14, FontStyle.Regular);

            #endregion


            graphic.DrawString("Formation éffectuées ", fnt3, Brushes.SlateGray, unite_largeur, 7 * unite_hauteur);



            //var dtFormation = ConnectionClass.ListeFormation(numeroMatricule);
            //for (var i = 0; i < dtFormation.Rows.Count; i++)
            //{
            //    var Yloc = 4 * unite_hauteur * i + 10 * unite_hauteur;

            //    graphic.FillRectangle(Brushes.Snow, unite_largeur, Yloc, unite_largeur * 24, 3 * unite_hauteur);
            //    graphic.DrawString("Type de formation", fnt11, Brushes.SlateGray, unite_largeur, Yloc);
            //    graphic.DrawString("Date ", fnt11, Brushes.SlateGray, unite_largeur, Yloc + unite_hauteur);
            //    graphic.DrawString("Durée", fnt11, Brushes.SlateGray, unite_largeur, Yloc + 2 * unite_hauteur);
            //    graphic.DrawString(dtFormation.Rows[i].ItemArray[0].ToString(), fnt11, Brushes.SlateGray, 8 * unite_largeur, Yloc);
            //    graphic.DrawString(DateTime.Parse(dtFormation.Rows[i].ItemArray[1].ToString()).ToShortDateString(),
            //        fnt11, Brushes.SlateGray, 8 * unite_largeur, Yloc + unite_hauteur);
            //    graphic.DrawString(dtFormation.Rows[i].ItemArray[2].ToString(), fnt11, Brushes.SlateGray, 8 * unite_largeur, Yloc + 2 * unite_hauteur);
            //    //graphic.DrawString("", fnt11, Brushes.SlateGray, unite_largeur, Yloc + 6 * unite_hauteur);
            //}

            return bitmap;
        }

        public static Bitmap ImprimerLalisteDesPersonnels(DataGridView dgvPersonnel, string titreImpression, int start, string  nombre)
        {
            try
            {
                #region
                int unite_hauteur = 20;
                int unite_largeur = 32;
                int largeur_facture = 33 * unite_largeur + 10;
                int hauteur_facture = 35 * unite_hauteur;

                //creer un bit map
                Bitmap bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                // creer un objet graphic
                Graphics graphic = Graphics.FromImage(bitmap);

                //la couleur de l'image
                graphic.Clear(Color.White);
                #endregion


                //definir les polices 
                Font fnt1 = new Font("Arial Narrow", 10, FontStyle.Regular);
                Font fnt0 = new Font("Arial Narrow", 9, FontStyle.Italic);
                Font fnt11 = new Font("Arial Narrow", 11, FontStyle.Bold);
                Font fnt3 = new Font("Arial Narrow", 18, FontStyle.Bold);
                Font fnt33 = new Font("Arial Narrow", 9, FontStyle.Regular);

                if (start + 1 == 1)
                {
                    Image logo = global::SGSP.Properties.Resources.Logo;
                    graphic.DrawImage(logo, 10, 18, 20 * unite_largeur + 0, 3 * unite_hauteur);
                    graphic.DrawString("Nombre total : " + nombre, fnt33, Brushes.Black, 29 * unite_largeur, 4 * unite_hauteur - 12);
                }
                graphic.DrawRectangle(Pens.Black, 14, 4 * unite_hauteur + 12, unite_largeur * 32 + 19, 31 * (unite_hauteur)- 25);

                graphic.DrawString(titreImpression.ToUpper(), fnt11, Brushes.Black, 15, 4 * unite_hauteur - 12);
                graphic.DrawRectangle(Pens.Black, 15, 4 * unite_hauteur + 12, unite_largeur + 10, unite_hauteur + 3);
                graphic.DrawRectangle(Pens.Black, 25 + unite_largeur, 4 * unite_hauteur + 12, unite_largeur * 2 + 7, unite_hauteur + 3);
                graphic.DrawRectangle(Pens.Black, 4 * unite_largeur, 4 * unite_hauteur + 12, unite_largeur * 6, unite_hauteur + 3);
                graphic.DrawRectangle(Pens.Black, 10 * unite_largeur, 4 * unite_hauteur + 12, unite_largeur * 1, unite_hauteur + 3);
                graphic.DrawRectangle(Pens.Black, 11 * unite_largeur, 4 * unite_hauteur + 12, unite_largeur * 5, unite_hauteur + 3);
                graphic.DrawRectangle(Pens.Black, 16 * unite_largeur + 0, 4 * unite_hauteur + 12, 4 * unite_largeur + 0, unite_hauteur + 3);
                graphic.DrawRectangle(Pens.Black, 20 * unite_largeur + 0, 4 * unite_hauteur + 12, unite_largeur - 5, unite_hauteur + 3);
                graphic.DrawRectangle(Pens.Black, 21 * unite_largeur - 5, 4 * unite_hauteur + 12, unite_largeur - 5, unite_hauteur + 3);
                graphic.DrawRectangle(Pens.Black, 22 * unite_largeur - 10, 4 * unite_hauteur + 12, unite_largeur - 5, unite_hauteur + 3);
                graphic.DrawRectangle(Pens.Black, 23 * unite_largeur - 15, 4 * unite_hauteur + 12, 3 * unite_largeur + 15, unite_hauteur + 3);
                graphic.DrawRectangle(Pens.Black, 26 * unite_largeur + 0, 4 * unite_hauteur + 12, unite_largeur * 2 + 16, unite_hauteur + 3);
                graphic.DrawRectangle(Pens.Black, 28 * unite_largeur + 16, 4 * unite_hauteur + 12, unite_largeur * 2 + 16, unite_hauteur + 3);
                graphic.DrawRectangle(Pens.Black, 31 * unite_largeur + 0, 4 * unite_hauteur + 12, unite_largeur * 2, unite_hauteur + 3);
                //graphic.DrawRectangle(Pens.Black, 26 * unite_largeur + 0, 6 * unite_hauteur + 12, unite_largeur * 3 + 10, unite_hauteur + 3);
                graphic.DrawString("N°", fnt11, Brushes.Black, unite_largeur * 1, 4 * unite_hauteur + 13);
                graphic.DrawString("Matricule", fnt11, Brushes.Black, unite_largeur * 2 - 7, 4 * unite_hauteur + 13);
                graphic.DrawString("Nom & Prenom", fnt11, Brushes.Black, 4 * unite_largeur + 10, 4 * unite_hauteur + 13);
                graphic.DrawString("Sexe", fnt11, Brushes.Black, 10 * unite_largeur, 4 * unite_hauteur + 13);
                graphic.DrawString("Qualification", fnt11, Brushes.Black, 11 * unite_largeur + 10, 4 * unite_hauteur + 13);
                graphic.DrawString("Grade", fnt11, Brushes.Black, 16 * unite_largeur + 10, 4 * unite_hauteur + 13);
                graphic.DrawString("Cat", fnt11, Brushes.Black, 20 * unite_largeur + 0, 4 * unite_hauteur + 13);
                graphic.DrawString("Ech", fnt11, Brushes.Black, 21 * unite_largeur - 3, 4 * unite_hauteur + 13);
                graphic.DrawString("Anc", fnt11, Brushes.Black, 22 * unite_largeur - 8, 4 * unite_hauteur + 13);
                graphic.DrawString("Type contrat", fnt11, Brushes.Black, 23 * unite_largeur + 3, 4 * unite_hauteur + 13);
                graphic.DrawString("Date contrat", fnt11, Brushes.Black, 26 * unite_largeur + 3, 4 * unite_hauteur + 13);
                graphic.DrawString("Date depart", fnt11, Brushes.Black, 28 * unite_largeur + 17, 4 * unite_hauteur + 13);
                graphic.DrawString("Etat", fnt11, Brushes.Black, 31 * unite_largeur + 3, 4 * unite_hauteur + 13);
                var j = 0;

                var numero = 1 + start * 25;
                for (var i = start * 25; i < dgvPersonnel.Rows.Count; i++)
                {
                    int Yloc =( unite_hauteur+3) * j + 5 * unite_hauteur + 15;
                    if (dgvPersonnel.Rows[i].DefaultCellStyle.BackColor == SystemColors.Control)
                    {
                        graphic.FillRectangle(Brushes.LightGray, 15, Yloc, unite_largeur * 32 + 18, unite_hauteur);
                        graphic.DrawString(dgvPersonnel.Rows[i].Cells[1].Value.ToString(), fnt11, Brushes.Black, unite_largeur * 2, Yloc);
                        Yloc += unite_hauteur;
                    }
                    else if (dgvPersonnel.Rows[i].DefaultCellStyle.BackColor == Color.LightGray)
                    {
                        graphic.FillRectangle(Brushes.SaddleBrown, 15, Yloc, unite_largeur * 32 + 18, unite_hauteur);
                        graphic.DrawString(dgvPersonnel.Rows[i].Cells[1].Value.ToString().ToUpper(), fnt11, Brushes.White, unite_largeur * 2, Yloc);
                        Yloc += unite_hauteur;
                    }
                    else //if(dgvPersonnel.Rows[i].DefaultCellStyle.BackColor == Color.White)
                    {
                        graphic.DrawRectangle(Pens.Black, 15, Yloc, unite_largeur + 10, unite_hauteur + 3);
                        graphic.DrawRectangle(Pens.Black, 25 + unite_largeur, Yloc, unite_largeur * 2 + 7, unite_hauteur + 3);
                        graphic.DrawRectangle(Pens.Black, 4 * unite_largeur, Yloc, unite_largeur * 6, unite_hauteur + 3);
                        graphic.DrawRectangle(Pens.Black, 10 * unite_largeur, Yloc, unite_largeur * 1, unite_hauteur + 3);
                        graphic.DrawRectangle(Pens.Black, 11 * unite_largeur, Yloc, unite_largeur * 5, unite_hauteur + 3);
                        graphic.DrawRectangle(Pens.Black, 16 * unite_largeur + 0, Yloc, 4 * unite_largeur + 0, unite_hauteur + 3);
                        graphic.DrawRectangle(Pens.Black, 20 * unite_largeur + 0, Yloc, unite_largeur + -5, unite_hauteur + 3);
                        graphic.DrawRectangle(Pens.Black, 21 * unite_largeur - 5, Yloc, unite_largeur - 5, unite_hauteur + 3);
                        graphic.DrawRectangle(Pens.Black, 22 * unite_largeur - 10, Yloc, unite_largeur - 5, unite_hauteur + 3);
                        graphic.DrawRectangle(Pens.Black, 23 * unite_largeur - 15, Yloc, 3 * unite_largeur + 15, unite_hauteur + 3);
                        graphic.DrawRectangle(Pens.Black, 26 * unite_largeur + 0, Yloc, unite_largeur * 2 + 16, unite_hauteur + 3);
                        graphic.DrawRectangle(Pens.Black, 28 * unite_largeur + 16, Yloc, unite_largeur * 2 + 16, unite_hauteur + 3);
                        graphic.DrawRectangle(Pens.Black, 31 * unite_largeur + 0, Yloc, unite_largeur * 2, unite_hauteur + 3);

                        graphic.DrawString(dgvPersonnel.Rows[i].Cells[13].Value.ToString(), fnt1, Brushes.Black, 25, Yloc);
                        graphic.DrawString(dgvPersonnel.Rows[i].Cells[0].Value.ToString(), fnt1, Brushes.Black, unite_largeur * 2, Yloc);
                        var employe = dgvPersonnel.Rows[i].Cells[1].Value.ToString();
                        if (employe.Length > 35)
                        {
                            employe = employe.Substring(0, 35) + ".";

                        }
                        graphic.DrawString(employe, fnt1, Brushes.Black, 4 * unite_largeur + 10, Yloc + 1);
                        graphic.DrawString(dgvPersonnel.Rows[i].Cells[2].Value.ToString(), fnt33, Brushes.Black, 10 * unite_largeur + 10, Yloc + 1);
                        if (dgvPersonnel.Rows[i].Cells[3].Value.ToString().Length > 30)
                        {
                            graphic.DrawString(dgvPersonnel.Rows[i].Cells[3].Value.ToString().Substring(0, 30) + "...", fnt33, Brushes.Black, 11 * unite_largeur + 4, Yloc + 1);
                        }
                        else
                        {
                            graphic.DrawString(dgvPersonnel.Rows[i].Cells[3].Value.ToString(), fnt33, Brushes.Black, 11 * unite_largeur + 6, Yloc + 1);
                        }
                        var grade = dgvPersonnel.Rows[i].Cells[4].Value.ToString();
                        if (grade.Length > 22)
                        {
                            grade = grade.Substring(0, 22);
                        }
                        graphic.DrawString(grade, fnt1, Brushes.Black, 16 * unite_largeur + 6, Yloc + 1);
                        graphic.DrawString(dgvPersonnel.Rows[i].Cells[6].Value.ToString(), fnt1, Brushes.Black, 20 * unite_largeur + 6, Yloc + 1);
                        graphic.DrawString(dgvPersonnel.Rows[i].Cells[7].Value.ToString(), fnt1, Brushes.Black, 21 * unite_largeur + 6, Yloc + 1);
                        graphic.DrawString(dgvPersonnel.Rows[i].Cells[8].Value.ToString(), fnt1, Brushes.Black, 22 * unite_largeur + 0, Yloc + 1);
                        graphic.DrawString(dgvPersonnel.Rows[i].Cells[5].Value.ToString(), fnt1, Brushes.Black, 23 * unite_largeur - 13, Yloc + 1);
                        graphic.DrawString(dgvPersonnel.Rows[i].Cells[9].Value.ToString(), fnt1, Brushes.Black, 26 * unite_largeur + 5, Yloc + 1);
                        graphic.DrawString(dgvPersonnel.Rows[i].Cells[10].Value.ToString(), fnt1, Brushes.Black, 28 * unite_largeur + 20, Yloc + 1);
                        graphic.DrawString(dgvPersonnel.Rows[i].Cells[11].Value.ToString(), fnt1, Brushes.Black, 31 * unite_largeur + 5, Yloc + 1);

                    }
                 

                    j++;
                } 
                graphic.FillRectangle(Brushes.White, 14, 34 * unite_hauteur +8, unite_largeur * 34 - 0, 8 * unite_hauteur);
                graphic.DrawString("Page " + (start + 1), fnt1, Brushes.Black, 31 * unite_largeur + 20, 0);
                return bitmap;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
     
        public static Bitmap ImprimerLalisteDesPersonnelsContratFinOuRetraite(DataGridView dgvPersonnel, string titreImpression, int start, string nombre)
        {
            #region
            int unite_hauteur = 22;
            int unite_largeur = 32;
            int largeur_facture = 24 * unite_largeur + 10;
            int hauteur_facture = 56 * unite_hauteur;

            //creer un bit map
            Bitmap bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            #endregion
            try
            {
                Image logo = global::SGSP.Properties.Resources.Logo;
                graphic.DrawImage(logo, 15, 5, 23 * unite_largeur + 10, 3 * unite_hauteur+16);
            }
            catch { } //definir les polices 
            Font fnt1 = new Font("Arial Narrow", 10, FontStyle.Regular);
            Font fnt0 = new Font("Arial Narrow", 9, FontStyle.Italic);
            Font fnt11 = new Font("Arial Narrow", 11, FontStyle.Bold);
            Font fnt3 = new Font("Arial Narrow", 18, FontStyle.Bold);
            Font fnt33 = new Font("Arial Narrow", 9, FontStyle.Regular);

            graphic.DrawRectangle(Pens.Black, 14, 6 * unite_hauteur + 12, unite_largeur * 23 - 13, 45 * (unite_hauteur) + unite_hauteur + 3);

            graphic.DrawString(titreImpression.ToUpper(), fnt11, Brushes.Black, 15, 5 * unite_hauteur + 3);
            graphic.DrawString("Nombre total : " + nombre, fnt33, Brushes.Black, 20 * unite_largeur, 5 * unite_hauteur + 3);
            graphic.DrawRectangle(Pens.Black, 15, 6 * unite_hauteur + 12, unite_largeur + 10, unite_hauteur + 3);
            graphic.DrawRectangle(Pens.Black, 25 + unite_largeur, 6 * unite_hauteur + 12, unite_largeur * 2 + 7, unite_hauteur + 3);
            graphic.DrawRectangle(Pens.Black, 4 * unite_largeur, 6 * unite_hauteur + 12, unite_largeur * 8, unite_hauteur + 3);
            graphic.DrawRectangle(Pens.Black, 12 * unite_largeur, 6 * unite_hauteur + 12, unite_largeur * 5, unite_hauteur + 3);
            graphic.DrawRectangle(Pens.Black, 17 * unite_largeur + 0, 6 * unite_hauteur + 12, unite_largeur *3, unite_hauteur + 3);
            graphic.DrawRectangle(Pens.Black, 20 * unite_largeur + 0, 6 * unite_hauteur + 12, unite_largeur * 3, unite_hauteur + 0);

            graphic.DrawString("Matricule", fnt11, Brushes.Black, unite_largeur * 2 - 7, 6 * unite_hauteur + 13);
            graphic.DrawString("Nom & Prenom", fnt11, Brushes.Black, 4 * unite_largeur + 10, 6 * unite_hauteur + 13);
            graphic.DrawString("Qualification", fnt11, Brushes.Black, 12 * unite_largeur + 10, 6 * unite_hauteur + 13);
            graphic.DrawString("Date service", fnt11, Brushes.Black, 17 * unite_largeur + 3, 6 * unite_hauteur + 13);
            graphic.DrawString("Date", fnt11, Brushes.Black, 21 * unite_largeur + 3, 6 * unite_hauteur + 13);
            var j = 0;

            //var numero = 0;
            //for (var p = 1 + st; p = nombre; p++)
            //{
            //    numero = p;
            //}
            for (var i = start * 45; i < dgvPersonnel.Rows.Count; i++)
            {
                int Yloc = unite_hauteur * j + 7 * unite_hauteur + 15;
                graphic.DrawRectangle(Pens.Black, 15, Yloc, unite_largeur + 10, unite_hauteur + 0);
                //graphic.DrawRectangle(Pens.Black, 1 * unite_largeur + 25, Yloc, unite_largeur * 8, unite_hauteur + 0);
                if (dgvPersonnel.Rows[i].DefaultCellStyle.BackColor == SystemColors.Control)
                {
                    graphic.FillRectangle(Brushes.LightGray, 15, Yloc, unite_largeur * 23 - 14, unite_hauteur);
                    graphic.DrawString(dgvPersonnel.Rows[i].Cells[1].Value.ToString(), fnt11, Brushes.Black, unite_largeur * 2, Yloc);
                    Yloc += unite_hauteur;
                }
                else if (dgvPersonnel.Rows[i].DefaultCellStyle.BackColor == Color.LightGray)
                {
                    graphic.FillRectangle(Brushes.SaddleBrown, 15, Yloc, unite_largeur * 23 - 14, unite_hauteur);
                    graphic.DrawString(dgvPersonnel.Rows[i].Cells[1].Value.ToString().ToUpper(), fnt11, Brushes.White, unite_largeur * 5, Yloc);
                    Yloc += unite_hauteur;
                }
                else
                {
                    graphic.DrawRectangle(Pens.Black, 15, Yloc, unite_largeur + 10, unite_hauteur);
                    graphic.DrawRectangle(Pens.Black, 25 + unite_largeur, Yloc, unite_largeur * 2 + 7, unite_hauteur);
                    graphic.DrawRectangle(Pens.Black, 4 * unite_largeur, Yloc, unite_largeur * 8, unite_hauteur);
                    graphic.DrawRectangle(Pens.Black, 12 * unite_largeur, Yloc, unite_largeur * 5, unite_hauteur);
                    graphic.DrawRectangle(Pens.Black, 17 * unite_largeur , Yloc, unite_largeur *3, unite_hauteur);
                     graphic.DrawRectangle(Pens.Black, 20 * unite_largeur + 0, Yloc, unite_largeur * 3, unite_hauteur);

                    //graphic.DrawString(numero.ToString(), fnt1, Brushes.Black, unite_largeur , Yloc);
                    graphic.DrawString(dgvPersonnel.Rows[i].Cells[0].Value.ToString(), fnt1, Brushes.Black, unite_largeur * 2, Yloc);
                    var employe = dgvPersonnel.Rows[i].Cells[1].Value.ToString() + " " + dgvPersonnel.Rows[i].Cells[2].Value.ToString();
                    if (employe.Length > 28)
                    {
                        employe = employe.Substring(0, 28) + ".";

                    }
                    graphic.DrawString(employe, fnt1, Brushes.Black, 4 * unite_largeur + 10, Yloc + 1);
                    if (dgvPersonnel.Rows[i].Cells[3].Value.ToString().Length > 20)
                    {
                        graphic.DrawString(dgvPersonnel.Rows[i].Cells[3].Value.ToString().Substring(0, 20) + "...", fnt33, Brushes.Black, 12 * unite_largeur + 4, Yloc + 1);
                    }
                    else
                    {
                        graphic.DrawString(dgvPersonnel.Rows[i].Cells[3].Value.ToString(), fnt33, Brushes.Black, 12 * unite_largeur + 6, Yloc + 1);
                    }
                    graphic.DrawString(dgvPersonnel.Rows[i].Cells[4].Value.ToString(), fnt1, Brushes.Black, 17 * unite_largeur + 6, Yloc + 1);
                    graphic.DrawString(dgvPersonnel.Rows[i].Cells[5].Value.ToString(), fnt1, Brushes.Black, 20 * unite_largeur + 10, Yloc + 1);

                }
                graphic.FillRectangle(Brushes.White, 15, 52 * unite_hauteur + 17, unite_largeur * 24 - 14, 8 * unite_hauteur);
                graphic.DrawString("Page " + (start + 1), fnt1, Brushes.Black, 21 * unite_largeur + 20, 0);

                j++;
            }
            return bitmap;
        }

        public static Bitmap ImprimerLalisteDesPersonnelsAvecSalaire(DataGridView dgvPersonnel, string titreImpression, int start, string nombre)
        {   try
            {
                #region
                int unite_hauteur = 20;
                int unite_largeur = 32;
                int largeur_facture = 37* unite_largeur + 16;
                int hauteur_facture = 35 * unite_hauteur;

                //creer un bit map
                Bitmap bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                // creer un objet graphic
                Graphics graphic = Graphics.FromImage(bitmap);

                //la couleur de l'image
                graphic.Clear(Color.White);
                #endregion

                var drawFormatCenter = new StringFormat();
                drawFormatCenter.Alignment = StringAlignment.Center;
                var drawFormatLeft = new StringFormat();
                drawFormatLeft.Alignment = StringAlignment.Near;
                var drawFormatRight = new StringFormat();
                drawFormatRight.Alignment = StringAlignment.Far;

                //definir les polices 
                Font fnt1 = new Font("Arial Narrow", 10, FontStyle.Regular);
                Font fnt0 = new Font("Arial Narrow", 10, FontStyle.Italic);
                Font fnt11 = new Font("Arial Narrow", 10, FontStyle.Bold);
                Font fnt3 = new Font("Arial Narrow", 18, FontStyle.Bold);
                Font fnt33 = new Font("Arial Narrow", 11, FontStyle.Regular);

                if (start + 1 == 1)
                {
                    Image logo = global::SGSP.Properties.Resources.Logo;
                    graphic.DrawImage(logo, 10, 18, 20 * unite_largeur + 0, 3 * unite_hauteur);
                    graphic.DrawString("Nombre total : " + nombre, fnt33, Brushes.Black, 33 * unite_largeur, 4 * unite_hauteur - 12);
                }
                //graphic.DrawRectangle(Pens.Black, 14, 4 * unite_hauteur + 12, unite_largeur * 32 + 19, 24 * (unite_largeur)- 50);

                graphic.DrawString(titreImpression.ToUpper(), fnt11, Brushes.Black, 15, 4 * unite_hauteur - 12);
                graphic.DrawRectangle(Pens.Black, 15, 4 * unite_hauteur + 12, unite_largeur + 10, unite_hauteur * 2);
                graphic.DrawRectangle(Pens.Black, 25 + unite_largeur, 4 * unite_hauteur + 12, unite_largeur * 2 + 7, unite_hauteur *2);
                graphic.DrawRectangle(Pens.Black, 4 * unite_largeur, 4 * unite_hauteur + 12, unite_largeur * 9-10, unite_hauteur * 2);
                graphic.DrawRectangle(Pens.Black, 13 * unite_largeur-10 , 4 * unite_hauteur + 12, 2 * unite_largeur + 10, unite_hauteur * 2);
                graphic.DrawRectangle(Pens.Black, 15 * unite_largeur , 4 * unite_hauteur + 12, unite_largeur * 8 + 0, unite_hauteur * 1);
                graphic.DrawRectangle(Pens.Black, 15 * unite_largeur , 5 * unite_hauteur + 12, unite_largeur * 2+5, unite_hauteur * 1);
                graphic.DrawRectangle(Pens.Black, 17 * unite_largeur+5, 5 * unite_hauteur + 12, unite_largeur * 2+7, unite_hauteur *1);
                graphic.DrawRectangle(Pens.Black, 19 * unite_largeur+12 , 5 * unite_hauteur + 12, 2* unite_largeur + 0, unite_hauteur *1);
                graphic.DrawRectangle(Pens.Black, 21 * unite_largeur +12, 5 * unite_hauteur + 12, 2 * unite_largeur -12, unite_hauteur *1);
                graphic.DrawRectangle(Pens.Black, 23 * unite_largeur , 4 * unite_hauteur + 12, 2 * unite_largeur + 0, unite_hauteur *2);
                graphic.DrawRectangle(Pens.Black, 25 * unite_largeur + 0, 4 * unite_hauteur + 12, 2 * unite_largeur + 0, unite_hauteur *2);
                graphic.DrawRectangle(Pens.Black, 27 * unite_largeur + 0, 4 * unite_hauteur + 12, 2 * unite_largeur + 0, unite_hauteur *2);
                graphic.DrawRectangle(Pens.Black, 27 * unite_largeur + 0, 4 * unite_hauteur + 12, 2 * unite_largeur + 0, unite_hauteur * 2);
                graphic.DrawRectangle(Pens.Black, 29 * unite_largeur + 0, 4 * unite_hauteur + 12, 2 * unite_largeur + 0, unite_hauteur * 2);
                graphic.DrawRectangle(Pens.Black, 31 * unite_largeur + 0, 4 * unite_hauteur + 12, 2 * unite_largeur + 0, unite_hauteur * 2);
                graphic.DrawRectangle(Pens.Black, 33 * unite_largeur + 0, 4 * unite_hauteur + 12, 2 * unite_largeur + 0, unite_hauteur * 2);
                graphic.DrawRectangle(Pens.Black, 35 * unite_largeur + 0, 4 * unite_hauteur + 12, 2 * unite_largeur + 0, unite_hauteur * 2);
                //graphic.DrawRectangle(Pens.Black, 35 * unite_largeur + 0, 4 * unite_hauteur + 12, 2 * unite_largeur + 0, unite_hauteur * 2);

                graphic.DrawString("N°", fnt11, Brushes.Black, unite_largeur * 1, 4 * unite_hauteur + 23);
                graphic.DrawString("Matricule", fnt11, Brushes.Black, unite_largeur * 2 - 7, 4 * unite_hauteur + 23);
                graphic.DrawString("Nom & Prenom", fnt11, Brushes.Black, 4 * unite_largeur + 10, 4 * unite_hauteur + 23);
                graphic.DrawString("Salaire brut  ", fnt11, Brushes.Black, 13 * unite_largeur -3, 4 * unite_hauteur + 23);
                graphic.DrawString("Retenus  ", fnt11, Brushes.Black, 18 * unite_largeur + 0, 4 * unite_hauteur + 12);
                graphic.DrawString("CNPS(3,5%)  ", fnt11, Brushes.Black, 15 * unite_largeur+2, 5* unite_hauteur + 13);
                graphic.DrawString("CNPS(16,5%)  ", fnt11, Brushes.Black, 17 * unite_largeur + 5, 5 * unite_hauteur + 13);
                graphic.DrawString("IRPP  ", fnt11, Brushes.Black, 19 * unite_largeur + 29, 5 * unite_hauteur + 13);
                graphic.DrawString("ONASA  ", fnt11, Brushes.Black, 21 * unite_largeur + 17, 5 * unite_hauteur + 13);
                graphic.DrawString("Total\nRetenue ", fnt11, Brushes.Black, 23 * unite_largeur + 10, 4 * unite_hauteur + 13);
                graphic.DrawString("Prime\nMotivation", fnt11, Brushes.Black, 25 * unite_largeur + 3, 4 * unite_hauteur + 13);
                graphic.DrawString("Frais \nComm@", fnt11, Brushes.Black, 27 * unite_largeur + 6, 4 * unite_hauteur + 13);
                graphic.DrawString("Indemnités ", fnt11, Brushes.Black, 29 * unite_largeur + 1, 4 * unite_hauteur + 23);
                graphic.DrawString("Primes\nTransport", fnt11, Brushes.Black, 31 * unite_largeur + 5, 4 * unite_hauteur + 13);
                graphic.DrawString("Autres\nPrimes", fnt11, Brushes.Black, 33 * unite_largeur + 8, 4 * unite_hauteur + 13);
                graphic.DrawString("Net à\n payer", fnt11, Brushes.Black, 35 * unite_largeur + 5, 4 * unite_hauteur + 13);
                //graphic.DrawString("Indemnité ", fnt11, Brushes.Black, 21 * unite_largeur + 10, 4 * unite_hauteur + 13);
                var j = 0;
                var coutSalarialMensuels = .0;
                for (var i = start * 28; i < dgvPersonnel.Rows.Count; i++)
                {
                    int Yloc =unite_hauteur * j + 6 * unite_hauteur + 12;
                    //graphic.DrawRectangle(Pens.Black, 15, Yloc, unite_largeur + 10, unite_hauteur + 0);
                    //graphic.DrawRectangle(Pens.Black, 1 * unite_largeur + 25, Yloc, unite_largeur * 8, unite_hauteur + 0);
                    if (dgvPersonnel.Rows[i].DefaultCellStyle.BackColor == SystemColors.Control)
                    {
                        graphic.FillRectangle(Brushes.LightGray, 15, Yloc+1, unite_largeur * 37- 16, unite_hauteur);
                        graphic.DrawRectangle(Pens.Black, 15, Yloc+0, unite_largeur * 37 - 15, unite_hauteur+0);
                        graphic.DrawString(dgvPersonnel.Rows[i].Cells[1].Value.ToString().ToUpper(), fnt11, Brushes.Black, unite_largeur * 2, Yloc);
                        Yloc += unite_hauteur;
                    }
                    else if (dgvPersonnel.Rows[i].DefaultCellStyle.BackColor == Color.LightGray)
                    {
                        graphic.FillRectangle(Brushes.SaddleBrown, 15, Yloc+1, unite_largeur * 37 - 15, unite_hauteur+1); 
                        graphic.DrawRectangle(Pens.Black, 15, Yloc+0, unite_largeur * 37 - 15, unite_hauteur+0);
                        graphic.DrawString(dgvPersonnel.Rows[i].Cells[1].Value.ToString().ToUpper(), fnt11, Brushes.White, unite_largeur * 2, Yloc);
                        Yloc += unite_hauteur;
                    }
                    else //if(dgvPersonnel.Rows[i].DefaultCellStyle.BackColor == Color.White)
                    {

                        graphic.DrawRectangle(Pens.Black, 15, Yloc, unite_largeur + 10, unite_hauteur * 1);
                        graphic.DrawRectangle(Pens.Black, 25 + unite_largeur, Yloc, unite_largeur * 2 + 7, unite_hauteur * 1);
                        graphic.DrawRectangle(Pens.Black, 4 * unite_largeur, Yloc, unite_largeur * 9 - 10, unite_hauteur * 1);
                        graphic.FillRectangle(Brushes.WhiteSmoke, 13 * unite_largeur - 10, Yloc, 2 * unite_largeur + 10, unite_hauteur * 1);
                        graphic.DrawRectangle(Pens.Black, 13 * unite_largeur - 10, Yloc, 2 * unite_largeur + 10, unite_hauteur * 1);
                        graphic.DrawRectangle(Pens.Black, 15 * unite_largeur, Yloc, unite_largeur * 8 + 0, unite_hauteur * 1);
                        graphic.DrawRectangle(Pens.Black, 15 * unite_largeur, Yloc, unite_largeur * 2 + 5, unite_hauteur * 1);
                        graphic.DrawRectangle(Pens.Black, 17 * unite_largeur + 5, Yloc, unite_largeur * 2 + 7, unite_hauteur * 1);
                        graphic.DrawRectangle(Pens.Black, 19 * unite_largeur + 12, Yloc, 2 * unite_largeur + 0, unite_hauteur * 1);
                        graphic.FillRectangle(Brushes.WhiteSmoke, 23 * unite_largeur + 0, Yloc, 2 * unite_largeur - 0, unite_hauteur * 1);
                        graphic.DrawRectangle(Pens.Black, 21 * unite_largeur + 12, Yloc, 2 * unite_largeur - 12, unite_hauteur * 1);
                        graphic.DrawRectangle(Pens.Black, 23 * unite_largeur, Yloc, 2 * unite_largeur + 0, unite_hauteur );
                        graphic.DrawRectangle(Pens.Black, 25 * unite_largeur + 0, Yloc, 2 * unite_largeur + 0, unite_hauteur );
                        graphic.DrawRectangle(Pens.Black, 27 * unite_largeur + 0, Yloc, 2 * unite_largeur + 0, unite_hauteur );
                        graphic.DrawRectangle(Pens.Black, 27 * unite_largeur + 0, Yloc, 2 * unite_largeur + 0, unite_hauteur );
                        graphic.DrawRectangle(Pens.Black, 29 * unite_largeur + 0, Yloc, 2 * unite_largeur + 0, unite_hauteur );
                        graphic.DrawRectangle(Pens.Black, 31 * unite_largeur + 0, Yloc, 2 * unite_largeur + 0, unite_hauteur );
                        graphic.DrawRectangle(Pens.Black, 33 * unite_largeur + 0, Yloc, 2 * unite_largeur + 0, unite_hauteur);
                        graphic.FillRectangle(Brushes.WhiteSmoke, 35 * unite_largeur - 0, Yloc, 2 * unite_largeur + 0, unite_hauteur * 1);
                        graphic.DrawRectangle(Pens.Black, 35 * unite_largeur + 0, Yloc, 2 * unite_largeur + 0, unite_hauteur );

                        graphic.DrawString(dgvPersonnel.Rows[i].Cells[13].Value.ToString(), fnt1, Brushes.Black, 25, Yloc);
                        graphic.DrawString(dgvPersonnel.Rows[i].Cells[0].Value.ToString(), fnt1, Brushes.Black, unite_largeur * 2, Yloc);
                        var employe = dgvPersonnel.Rows[i].Cells[1].Value.ToString();
                        if (employe.Length > 40)
                        {
                            employe = employe.Substring(0, 40) + ".";

                        }
                        graphic.DrawString(employe.ToUpper(), fnt1, Brushes.Black, 4 * unite_largeur + 10, Yloc + 1);
              
                        var salaire = ConnectionClass.ListeSalaire(dgvPersonnel.Rows[i].Cells[0].Value.ToString());
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", salaire[0].SalaireBase), fnt1, Brushes.Black, 15 * unite_largeur - 5, Yloc + 1, drawFormatRight);

                        graphic.DrawString(String.Format(elGR, "{0:0,0}", salaire[0].PrimeMotivation), fnt1, Brushes.Black, 27 * unite_largeur - 5, Yloc + 1, drawFormatRight);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", salaire[0].FraisCommunication), fnt1, Brushes.Black, 29 * unite_largeur - 5, Yloc + 1, drawFormatRight);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", salaire[0].Indemnites), fnt1, Brushes.Black, 31 * unite_largeur - 5, Yloc + 1, drawFormatRight);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", salaire[0].PrimeTransport), fnt1, Brushes.Black, 33 * unite_largeur - 5, Yloc + 1, drawFormatRight);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", salaire[0].AutresPrimes), fnt1, Brushes.Black, 35 * unite_largeur -5, Yloc + 1,drawFormatRight);
                        double onasa, totalCnps, chargePatronale,  totalIRPP = 0;

                        var service = ConnectionClass.ListeServicePersonnel(dgvPersonnel.Rows[i].Cells[0].Value.ToString());
                        if (dgvPersonnel.Rows[i].Cells[5].Value.ToString().ToUpper().Contains("CONTRACTUEL".ToUpper()))
                        {

                            var cnps = 3.5;
                            var cnpsCsdn = 16.5;
                            onasa = 40;
                            totalCnps = System.Math.Round(salaire[0].SalaireBase * cnps / 100);
                            chargePatronale = System.Math.Round(salaire[0].SalaireBase * cnpsCsdn / 100);
                            if (salaire[0].SalaireBase > 500000)
                            {
                                totalCnps = 500000 * cnps / 100;
                                chargePatronale = System.Math.Round(500000 * cnpsCsdn / 100);
                            }

                            var salaireImposable =(salaire[0].SalaireBase  - totalCnps) * 12;
                            totalIRPP = GlobalVariable.IRPP(salaireImposable, salaire[0].SalaireBase, totalCnps, .0);
                            onasa = GlobalVariable.ONASA(salaireImposable, .0);
                            graphic.DrawString(String.Format(elGR, "{0:0,0}", totalCnps), fnt1, Brushes.Black, 17 * unite_largeur -5, Yloc + 1, drawFormatRight);
                            graphic.DrawString(String.Format(elGR, "{0:0,0}", chargePatronale), fnt1, Brushes.Black, 19 * unite_largeur +5, Yloc + 1, drawFormatRight);
                   
                        }

                        else if (service[0].SiCNRT)
                        {                          
                            var baseCRNT = .0;
                            var cnrtEmploye = 5.0;
                            var cnrtEmployeur = 12.0;

                            if (service[0].Poste == "Directeur Général")
                            {
                                baseCRNT = 224250.0;
                            }
                            else if (service[0].Poste == "Directrice Générale Adjointe")
                            {
                                baseCRNT = 138000.0;
                            }
                            totalCnps = System.Math.Round(baseCRNT * cnrtEmploye / 100);
                            chargePatronale = System.Math.Round(baseCRNT * cnrtEmployeur / 100);
                            var salaireImposable = (salaire[0].SalaireBase - totalCnps) * 12;
                            totalIRPP = GlobalVariable.IRPP(salaireImposable, salaire[0].SalaireBase, totalCnps, .0);
                            onasa = GlobalVariable.ONASA(salaireImposable, .0);
                            graphic.DrawString(String.Format(elGR, "{0:0,0}", totalCnps) + "*", fnt1, Brushes.Black, 17 * unite_largeur -5, Yloc + 1, drawFormatRight);
                            graphic.DrawString(String.Format(elGR, "{0:0,0}", chargePatronale+"*"), fnt1, Brushes.Black, 19 * unite_largeur +5, Yloc + 1, drawFormatRight);
                        }
                        else
                        {
                            chargePatronale = 0;
                            totalCnps = 0;
                            totalIRPP = 0;
                            onasa = 0;
                            graphic.DrawString(String.Format(elGR, "{0:0,0}", totalCnps), fnt1, Brushes.Black, 17 * unite_largeur -5, Yloc + 1, drawFormatRight);
                            graphic.DrawString(String.Format(elGR, "{0:0,0}", chargePatronale), fnt1, Brushes.Black, 19 * unite_largeur +5, Yloc + 1, drawFormatRight);
                   
                        }
                        var totalCharge = totalCnps + totalIRPP + onasa ;
                        var netPayer =salaire[0].SalaireBase -totalCharge+  salaire[0].PrimeTransport + salaire[0].PrimeMotivation + salaire[0].FraisCommunication + salaire[0].AutresPrimes + salaire[0].Indemnites;
                        coutSalarialMensuels += netPayer + chargePatronale;
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", totalIRPP), fnt1, Brushes.Black, 21 * unite_largeur +10, Yloc + 1, drawFormatRight);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", onasa), fnt1, Brushes.Black, 23 * unite_largeur - 5, Yloc + 1, drawFormatRight);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", totalCharge), fnt1, Brushes.Black, 25 * unite_largeur - 5, Yloc + 1, drawFormatRight);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", netPayer), fnt1, Brushes.Black, 37 * unite_largeur - 5, Yloc + 1, drawFormatRight);
                        //graphic.DrawString(dgvPersonnel.Rows[i].Cells[5].Value.ToString(), fnt1, Brushes.Black, 23 * unite_largeur - 13, Yloc + 1);

                    }
                 

                    j++;
                }
                if (start + 1 == 1)
                {
                    graphic.DrawString("Masse salariale mensuelle : " + String.Format(elGR, "{0:0,0}", coutSalarialMensuels) + " F. CFA  -  " +
                     " Masse salariale mensuelle : " + String.Format(elGR, "{0:0,0}", coutSalarialMensuels * 12) + " F. CFA"
                     , fnt11, Brushes.Black, 15 * unite_largeur, 4 * unite_hauteur - 8);
                }
                graphic.FillRectangle(Brushes.White, 14, 34 * unite_hauteur +13, unite_largeur * 37 - 0, 8 * unite_hauteur);
                graphic.DrawString("Page " + (start + 1), fnt1, Brushes.Black, 23 * unite_largeur + 20, 0);
                return bitmap;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static Bitmap ImprimerLalisteDesPersonnelsInformationBanciaire(DataGridView dgvPersonnel, int start)
        {
            try
            {
                #region
                int unite_hauteur = 20;
                int unite_largeur = 32;
                int largeur_facture = 26 * unite_largeur + 5;
                int hauteur_facture = 56 * unite_hauteur;

                //creer un bit map
                Bitmap bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                // creer un objet graphic
                Graphics graphic = Graphics.FromImage(bitmap);

                //la couleur de l'image
                graphic.Clear(Color.White);
                #endregion


                //definir les polices 
                Font fnt1 = new Font("Century Gothic", 11, FontStyle.Regular);
                Font fnt0 = new Font("Century Gothic", 9, FontStyle.Italic);
                Font fnt11 = new Font("Century Gothic", 10, FontStyle.Bold);
                Font fnt3 = new Font("Century Gothic", 18, FontStyle.Bold);
                Font fnt33 = new Font("Century Gothic", 9, FontStyle.Regular);

                if (start + 1 == 1)
                {
                    Image logo = global::SGSP.Properties.Resources.Logo;
                    graphic.DrawImage(logo, 10, 18, 20 * unite_largeur + 0, 3 * unite_hauteur);
                }
                //graphic.DrawRectangle(Pens.Black, 14, 4 * unite_hauteur + 12, unite_largeur * 32 + 19, 24 * (unite_largeur)- 50);

                graphic.DrawString("Informations bancaires des salariés" , fnt11, Brushes.Black, 15, 4 * unite_hauteur - 12);
                graphic.DrawRectangle(Pens.Black, 15, 4 * unite_hauteur + 12, unite_largeur + 7, unite_hauteur + 3);
                graphic.DrawRectangle(Pens.Black, 2* unite_largeur-10, 4 * unite_hauteur + 12, unite_largeur * 2+20 , unite_hauteur + 3);
                graphic.DrawRectangle(Pens.Black, 4 * unite_largeur+10, 4 * unite_hauteur + 12, unite_largeur * 9-10, unite_hauteur + 3);
                graphic.DrawRectangle(Pens.Black, 13 * unite_largeur, 4 * unite_hauteur + 12, unite_largeur * 4, unite_hauteur + 3);
                graphic.DrawRectangle(Pens.Black, 17 * unite_largeur, 4 * unite_hauteur + 12, unite_largeur *6, unite_hauteur + 3);
                graphic.DrawRectangle(Pens.Black, 23 * unite_largeur, 4 * unite_hauteur + 12, unite_largeur * 2, unite_hauteur + 3);

                graphic.DrawString("N°", fnt11, Brushes.Black, unite_largeur * 1, 4 * unite_hauteur + 13);
                graphic.DrawString("MATRICULE", fnt11, Brushes.Black, unite_largeur *2-5, 4 * unite_hauteur + 13);
                graphic.DrawString("NOM & PRENOM", fnt11, Brushes.Black, 4 * unite_largeur+15 , 4 * unite_hauteur + 13);
                graphic.DrawString("BANQUES", fnt11, Brushes.Black, 13 * unite_largeur+15, 4 * unite_hauteur + 13);
                graphic.DrawString("COMPTE ", fnt11, Brushes.Black, 17 * unite_largeur + 15, 4 * unite_hauteur + 13);
                graphic.DrawString("CLE ", fnt11, Brushes.Black, 23 * unite_largeur + 15, 4 * unite_hauteur + 13);
                var j = 0;

                for (var i = start * 43; i < dgvPersonnel.Rows.Count; i++)
                {
                    int Yloc = (unite_hauteur + 3) * j + 5 * unite_hauteur + 15;
                    //graphic.DrawRectangle(Pens.Black, 15, Yloc, unite_largeur + 10, unite_hauteur + 0);
                    //graphic.DrawRectangle(Pens.Black, 1 * unite_largeur + 25, Yloc, unite_largeur * 8, unite_hauteur + 0);
                    if (dgvPersonnel.Rows[i].DefaultCellStyle.BackColor == SystemColors.Control)
                    {
                        graphic.FillRectangle(Brushes.LightGray, 15, Yloc + 1, unite_largeur * 25 - 15, unite_hauteur);
                        graphic.DrawRectangle(Pens.Black, 15, Yloc + 1, unite_largeur * 25 - 15, unite_hauteur + 1);
                        graphic.DrawString(dgvPersonnel.Rows[i].Cells[1].Value.ToString().ToUpper(), fnt11, Brushes.Black, unite_largeur * 2, Yloc+3);
                        Yloc += unite_hauteur;
                    }
                    else if (dgvPersonnel.Rows[i].DefaultCellStyle.BackColor == Color.LightGray)
                    {
                        graphic.FillRectangle(Brushes.SaddleBrown, 15, Yloc + 1, unite_largeur * 25 - 15, unite_hauteur + 1);
                        graphic.DrawRectangle(Pens.Black, 15, Yloc + 1, unite_largeur * 25 - 15, unite_hauteur + 1);
                        graphic.DrawString(dgvPersonnel.Rows[i].Cells[1].Value.ToString().ToUpper(), fnt11, Brushes.White, unite_largeur * 2, Yloc+3);
                        Yloc += unite_hauteur;
                    }
                    else //if(dgvPersonnel.Rows[i].DefaultCellStyle.BackColor == Color.White)
                    {
                        graphic.DrawRectangle(Pens.Black, 15,Yloc, unite_largeur + 7, unite_hauteur + 3);
                        graphic.DrawRectangle(Pens.Black, 2 * unite_largeur-10, Yloc, unite_largeur * 2+20, unite_hauteur + 3);
                        graphic.DrawRectangle(Pens.Black, 4 * unite_largeur+10, Yloc, unite_largeur * 9-10, unite_hauteur + 3);
                        graphic.DrawRectangle(Pens.Black, 13 * unite_largeur, Yloc, unite_largeur * 4, unite_hauteur + 3);
                        graphic.DrawRectangle(Pens.Black, 17 * unite_largeur, Yloc, unite_largeur * 6, unite_hauteur + 3);
                        graphic.DrawRectangle(Pens.Black, 23 * unite_largeur, Yloc, unite_largeur * 2, unite_hauteur + 3);

                        graphic.DrawString(dgvPersonnel.Rows[i].Cells[13].Value.ToString(), fnt1, Brushes.Black, 25, Yloc + 3);
                        graphic.DrawString(dgvPersonnel.Rows[i].Cells[0].Value.ToString(), fnt1, Brushes.Black, unite_largeur * 2-5, Yloc+3);
                        var employe = dgvPersonnel.Rows[i].Cells[1].Value.ToString();
                        var banque = ConnectionClass.ListeDonneesBancaires(dgvPersonnel.Rows[i].Cells[0].Value.ToString());
                        graphic.DrawString(employe, fnt1, Brushes.Black, 4 * unite_largeur + 15, Yloc+3 );

                        var codeBanque = ConnectionClass.ListeBanques(banque[0].NomBanque).Count >0 ? ConnectionClass.ListeBanques(banque[0].NomBanque)[0].CodeBanque : "";
                        var codeGuichet =ConnectionClass.ListeBanques(banque[0].NomBanque).Count >0 ?   ConnectionClass.ListeBanques(banque[0].NomBanque)[0].CodeGuichet  : "" ;
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", banque[0].NomBanque), fnt1, Brushes.Black, 13 * unite_largeur + 6, Yloc + 3);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}",codeBanque + "-" + codeGuichet + "-" + banque[0].Compte), fnt1, Brushes.Black, 17 * unite_largeur + 6, Yloc + 3);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", banque[0].Cle), fnt1, Brushes.Black, 23 * unite_largeur + 20, Yloc + 3);
                        //graphic.DrawString(dgvPersonnel.Rows[i].Cells[5].Value.ToString(), fnt1, Brushes.Black, 23 * unite_largeur - 13, Yloc + 1);

                    }
                    j++;
                }
                graphic.FillRectangle(Brushes.White, 14, 55 * unite_hauteur + 5, unite_largeur * 34 - 0, 8 * unite_hauteur);
                graphic.DrawString("Page " + (start + 1), fnt33, Brushes.Black, 23 * unite_largeur + 20, 0);
                return bitmap;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //imprimer le badge
        public static Bitmap ImprimerBadgePersonnel(string numero, string nom, string prenom,
             string fonction, Image photo)
        {
            int unite_hauteur = 25;
            int unite = 20;
            int unite_largeur = 25;
            int largeur_batch = 9 * unite_largeur;
            int hauteur_batch = 14 * unite_hauteur;

            //creer un bit map
            Bitmap bitmap = new Bitmap(largeur_batch + 2, hauteur_batch + 7, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);
            Font fnt1 = new Font("Arial", 7, FontStyle.Regular);
            Font fnt2 = new Font("Arial", 8, FontStyle.Bold);
            Font fnt3 = new Font("Arial", 11, FontStyle.Bold);
            //la couleur de l'image
            graphic.Clear(Color.White);
            Image image1 = global::SGSP.Properties.Resources.Logo;
            //Image capture = global::GestionDesPelerinsTchad.Properties.Resources.Capture2;
            //graphic.DrawImage(capture, 0, 0, bitmap.Width, bitmap.Height);
            graphic.DrawImage(image1, 7 * unite_largeur + 7, 5, 2 * unite, unite_hauteur);

            graphic.DrawString("MON ENTREPRISE ", fnt3, Brushes.White, 10, 5);
            graphic.DrawString("CARTE PROFESSIONNELLE ", fnt3, Brushes.White, 10, 50);
            graphic.FillRectangle(Brushes.SlateGray, 0, 70, bitmap.Width, 5);

            graphic.DrawString(numero, fnt2, Brushes.White, 2 * unite_largeur, 3 * unite_hauteur + 19);
            graphic.DrawString(nom, fnt2, Brushes.White, 2 * unite_largeur, 3 * unite_hauteur + 39);
            graphic.DrawString(prenom, fnt2, Brushes.White, 2 * unite_largeur, 3 * unite_hauteur + 59);
            graphic.DrawString(fonction, fnt2, Brushes.White, 2 * unite_largeur, 3 * unite_hauteur + 79);
            graphic.DrawString("expiration : 31/12/" + DateTime.Now.Year, fnt2, Brushes.Red, 2 * unite_largeur, 13 * unite_hauteur + 15);
            graphic.DrawString("ANNEE " + DateTime.Now.Year, fnt3, Brushes.Yellow, 2 * unite_largeur, 3 * unite_hauteur + 100);

            graphic.DrawImage(photo, 2 * unite_largeur + 10, 10 * unite + 5, 4 * unite_largeur + 10, 6 * unite_hauteur - 15);
            //graphic.DrawImage(barcode, 15, 12 * unite_hauteur, 3 * unite_largeur, 2 * unite);

            return bitmap;
        }
        //imprimer le badge
        public static Bitmap ImprimerBadgePersonnel(Image barcode)
        {
            int unite_hauteur = 25;
            int unite = 20;
            int unite_largeur = 25;
            int largeur_batch = 9 * unite_largeur;
            int hauteur_batch = 14 * unite_hauteur;

            //creer un bit map
            Bitmap bitmap = new Bitmap(largeur_batch + 2, hauteur_batch + 7, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);
            Font fnt1 = new Font("Arial", 7, FontStyle.Regular);
            Font fnt2 = new Font("Arial", 8, FontStyle.Bold);
            Font fnt3 = new Font("Arial", 11, FontStyle.Bold);
            //la couleur de l'image
            graphic.Clear(Color.White);
            Image image1 = global::SGSP.Properties.Resources.Logo;
            //Image capture = global::GestionDesPelerinsTchad.Properties.Resources.Capture2;
            //graphic.DrawImage(capture, 0, 0, bitmap.Width, bitmap.Height);
            graphic.DrawImage(image1, 3 * unite_largeur + 7, unite_hauteur + 10, 3 * unite, unite_hauteur + 10);

            graphic.DrawString("REPUBLIQUE DU TCHAD ", fnt3, Brushes.White, 15, 10);

            graphic.FillRectangle(Brushes.SlateGray, 0, 87, bitmap.Width, 5);

            graphic.DrawImage(barcode, 2 * unite_largeur + 10, 8 * unite_hauteur, 4 * unite_largeur, 2 * unite_hauteur);
            graphic.DrawString("Soft development company", fnt1, Brushes.White, 10, 11 * unite_hauteur);
            graphic.DrawString("Devevloppement des applicaton informatique", fnt1, Brushes.White, 10, 11 * unite_hauteur + 15);
            graphic.DrawString("Adresse : Motorway Hill Fox 40", fnt1, Brushes.White, 10, 11 * unite_hauteur + 30);
            graphic.DrawString("Tel : (+235) 66304238 / 66661286", fnt1, Brushes.White, 10, 11 * unite_hauteur + 45);
            graphic.DrawString("Email : noudjichrist87@yahoo.com", fnt1, Brushes.White, 10, 11 * unite_hauteur + 60);
            return bitmap;
        }
        
        #region ORDRE_VIREMENT

        //imprimer l'ordre de paiement
        public static Bitmap ImprimerOrdreDePaiement(int numeroPaiement
            , DataGridView dgvPaiement, int exercice, string mois)
        {
            #region facture1
            //les dimension de la facture
            int unite_hauteur = 23;
            int unite_largeur = 32;
            int largeur_facture = 35 * unite_largeur + 10;
            int hauteur_facture = 35 * unite_hauteur + 20;//+ 15 + dtGrid.Rows.Count * unite_hauteur;


            //creer un bit map
            var bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            try
            {
                Image logo = global::SGSP.Properties.Resources.etatPaiement_1;
                graphic.DrawImage(logo, 0, 0, largeur_facture, hauteur_facture);
            }
            catch { }
            //definir les polices 
            Font fnt1 = new Font("Century Gothic", 10.8f, FontStyle.Regular);
            //Font fnt33 = new Font("Century Gothic", 10, FontStyle.Bold);
            Font fnt0 = new Font("Century Gothic", 10.5F, FontStyle.Bold);
            Font fnt00 = new Font("Century Gothic", 12, FontStyle.Regular);
            Font fnt11 = new Font("Century Gothic", 12, FontStyle.Bold);
            Font fnt3 = new Font("Arial Narrow", 15, FontStyle.Bold | FontStyle.Underline);

            #endregion

            graphic.DrawString("Page " + 1, fnt1, Brushes.Black, 33 * unite_largeur, unite_hauteur);
                graphic.DrawString(DateTime.Now.ToShortDateString(), fnt11, Brushes.Black, unite_largeur * 23-2, 11 * unite_hauteur - 8);
                graphic.DrawString("Etat pour servir de paiement des salaires du personnel contractuel de l'INSEED pour la période du : " + mois.ToUpper() + " " +
               exercice, fnt3, Brushes.Black, unite_largeur * 4, 16 * unite_hauteur - 5);
            var drawFormatCenter = new StringFormat();
            drawFormatCenter.Alignment = StringAlignment.Center;
            var drawFormatLeft = new StringFormat();
            drawFormatLeft.Alignment = StringAlignment.Near;
            var drawFormatRight = new StringFormat();
            drawFormatRight.Alignment = StringAlignment.Far;

            graphic.DrawRectangle(Pens.Black, 12, 17 * unite_hauteur + 8, unite_largeur + 4, 2 * unite_hauteur - 1);
            graphic.DrawRectangle(Pens.Black, 1 * unite_largeur + 16, 17 * unite_hauteur + 8, 10 * unite_largeur - 16, 2 * unite_hauteur - 1);
            graphic.DrawRectangle(Pens.Black, 11 * unite_largeur + 0, 17 * unite_hauteur + 8, 3 * unite_largeur + 0, 2 * unite_hauteur - 1);
            graphic.DrawRectangle(Pens.Black, 14 * unite_largeur + 0, 17 * unite_hauteur + 8, 2 * unite_largeur + 16, 2 * unite_hauteur - 1);
            graphic.DrawRectangle(Pens.Black, 16 * unite_largeur + 16, 17 * unite_hauteur + 8, 3 * unite_largeur + 0, 2 * unite_hauteur - 1);
            graphic.DrawRectangle(Pens.Black, 19 * unite_largeur + 16, 17 * unite_hauteur + 8, 2 * unite_largeur + 16, 2 * unite_hauteur - 1);
            graphic.DrawRectangle(Pens.Black, 22 * unite_largeur + 0, 17 * unite_hauteur + 8, 3 * unite_largeur + 0, 2 * unite_hauteur - 1);
            graphic.DrawRectangle(Pens.Black, 25 * unite_largeur + 0, 17 * unite_hauteur + 8, 2 * unite_largeur + 16, 2 * unite_hauteur - 1);
            graphic.DrawRectangle(Pens.Black, 27 * unite_largeur + 16, 17 * unite_hauteur + 8, 1 * unite_largeur + 16, 2 * unite_hauteur - 1);
            graphic.DrawRectangle(Pens.Black, 29 * unite_largeur + 0, 17 * unite_hauteur + 8, 3 * unite_largeur + 0, 2 * unite_hauteur - 1);
            graphic.DrawRectangle(Pens.Black, 32 * unite_largeur, 17 * unite_hauteur + 8, 3 * unite_largeur + 0, 2 * unite_hauteur - 1);

            graphic.DrawString("N°", fnt0, Brushes.Black, 15, 17 * unite_hauteur + 9);
            graphic.DrawString("Nom & prenom".ToUpper(), fnt0, Brushes.Black, 4 * unite_largeur + 18, 17 * unite_hauteur + 9);
            graphic.DrawString("Salaire \nde base".ToUpper(), fnt0, Brushes.Black, 11 * unite_largeur + 26, 17 * unite_hauteur + 9);
            graphic.DrawString("Congé".ToUpper(), fnt0, Brushes.Black, 13 * unite_largeur + 46, 17 * unite_hauteur + 9);
            graphic.DrawString("Salaire brut".ToUpper(), fnt0, Brushes.Black, 16 * unite_largeur + 20, 17 * unite_hauteur + 9);
            graphic.DrawString("CNPS", fnt0, Brushes.Black, 20 * unite_largeur + 6, 17 * unite_hauteur + 9);
            graphic.DrawString("Charge\npatronnale".ToUpper(), fnt0, Brushes.Black, 21 * unite_largeur + 36, 17 * unite_hauteur + 9);
            graphic.DrawString("IRPP".ToUpper(), fnt0, Brushes.Black, 25 * unite_largeur + 24, 17 * unite_hauteur + 9);
            graphic.DrawString("FIR".ToUpper(), fnt0, Brushes.Black, 27 * unite_largeur + 29, 17 * unite_hauteur + 9);
           graphic.DrawString("COTISATIONS\nSOCIALES".ToUpper(), fnt0, Brushes.Black, 29 * unite_largeur + 2, 17 * unite_hauteur + 9);
            graphic.DrawString("Salaire Net".ToUpper(), fnt0, Brushes.Black, 32 * unite_largeur + 5, 17 * unite_hauteur + 9);
            var j = 0;
            var count = 1;
            for (var i = 0; i < dgvPaiement.Rows.Count; i++)
            {
                #region MyRegion

                var YLOC = unite_hauteur * 19 + 9 + unite_hauteur * j;

                var employe = dgvPaiement.Rows[i].Cells[1].Value.ToString();

                if (dgvPaiement.Rows[i].Cells[1].Value.ToString().Contains("TOTAL"))
                {
                    if (dgvPaiement.Rows[i].Cells[1].Value.ToString().Contains("TOTAL GENERAL"))
                    {
                        graphic.FillRectangle(Brushes.WhiteSmoke, 13, YLOC - 3, unite_largeur * 35 - 13, unite_hauteur - 2);
                    }

                    graphic.DrawRectangle(Pens.Black, 12, YLOC, 11 * unite_largeur - 12, unite_hauteur - 2);
                    //graphic.DrawRectangle(Pens.Black, 1 * unite_largeur + 16, YLOC, 10 * unite_largeur -16, unite_hauteur - 2);
                    graphic.DrawRectangle(Pens.Black, 11 * unite_largeur + 0, YLOC, 3 * unite_largeur + 0, unite_hauteur - 2);
                    graphic.DrawRectangle(Pens.Black, 14 * unite_largeur + 0, YLOC, 2 * unite_largeur + 16, unite_hauteur - 2);
                    graphic.DrawRectangle(Pens.Black, 16 * unite_largeur + 16, YLOC, 3 * unite_largeur + 0, unite_hauteur - 2);
                    graphic.DrawRectangle(Pens.Black, 19 * unite_largeur + 16, YLOC, 2 * unite_largeur + 16, unite_hauteur - 2);
                    graphic.DrawRectangle(Pens.Black, 22 * unite_largeur + 0, YLOC, 3 * unite_largeur + 0, unite_hauteur - 2);
                    graphic.DrawRectangle(Pens.Black, 25 * unite_largeur + 0, YLOC, 2 * unite_largeur + 16, unite_hauteur - 2);
                    graphic.DrawRectangle(Pens.Black, 27 * unite_largeur + 16, YLOC, 1 * unite_largeur + 16, unite_hauteur - 2);
                    graphic.DrawRectangle(Pens.Black, 29 * unite_largeur + 0, YLOC, 3 * unite_largeur + 0, unite_hauteur - 2);
                    graphic.DrawRectangle(Pens.Black, 32 * unite_largeur, YLOC, 3 * unite_largeur + 0, unite_hauteur - 2);

                    graphic.DrawString(employe.ToUpper(), fnt0, Brushes.Black, 15, YLOC + 3);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[4].Value.ToString())), fnt0, Brushes.Black, 14 * unite_largeur - 10, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[7].Value.ToString())), fnt0, Brushes.Black, 16 * unite_largeur + 6, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[9].Value.ToString())), fnt0, Brushes.Black, 19 * unite_largeur + 6, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[10].Value.ToString())), fnt0, Brushes.Black, 22 * unite_largeur - 10, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[18].Value.ToString())), fnt0, Brushes.Black, 25 * unite_largeur - 10, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[11].Value.ToString())), fnt0, Brushes.Black, 27 * unite_largeur + 6, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[12].Value.ToString())), fnt0, Brushes.Black, 29 * unite_largeur - 5, YLOC + 3, drawFormatRight);
                    //graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[16].Value.ToString())), fnt0, Brushes.Black, 32 * unite_largeur - 10, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[18].Value.ToString()) + Double.Parse(dgvPaiement.Rows[i].Cells[10].Value.ToString())), fnt0, Brushes.Black, 32 * unite_largeur - 10, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[17].Value.ToString())), fnt0, Brushes.Black, 35 * unite_largeur - 10, YLOC + 3, drawFormatRight);

                }
                else
                {
                    if (string.IsNullOrWhiteSpace(dgvPaiement.Rows[i].Cells[4].Value.ToString()) && string.IsNullOrWhiteSpace(dgvPaiement.Rows[i].Cells[5].Value.ToString()))
                    {
                        graphic.DrawRectangle(Pens.Black, 12, YLOC - 2, unite_largeur * 35 - 12, unite_hauteur + 0);
                        graphic.FillRectangle(Brushes.WhiteSmoke, 13, YLOC + 1, unite_largeur * 35 - 14, unite_hauteur - 1);
                        graphic.DrawString(employe.ToUpper(), fnt0, Brushes.Black, 20, YLOC + 3);
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(dgvPaiement.Rows[i].Cells[20].Value.ToString()))
                        {
                            var numero = dgvPaiement.Rows[i].Cells[20].Value.ToString();
                            if (numero.Length == 1)
                                numero = "0" + numero;
                            graphic.DrawString(numero, fnt1, Brushes.Black, unite_largeur + 12, YLOC + 3, drawFormatRight);
                        }
                        graphic.DrawRectangle(Pens.Black, 12, YLOC, unite_largeur + 4, unite_hauteur - 0);
                        graphic.DrawRectangle(Pens.Black, 1 * unite_largeur + 16, YLOC, 10 * unite_largeur - 16, unite_hauteur - 0);
                        graphic.DrawRectangle(Pens.Black, 11 * unite_largeur + 0, YLOC, 3 * unite_largeur + 0, unite_hauteur - 0);
                        graphic.DrawRectangle(Pens.Black, 14 * unite_largeur + 0, YLOC, 2 * unite_largeur + 16, unite_hauteur - 0);
                        graphic.DrawRectangle(Pens.Black, 16 * unite_largeur + 16, YLOC, 3 * unite_largeur + 0, unite_hauteur - 0);
                        graphic.DrawRectangle(Pens.Black, 19 * unite_largeur + 16, YLOC, 2 * unite_largeur + 16, unite_hauteur - 0);
                        graphic.DrawRectangle(Pens.Black, 22 * unite_largeur + 0, YLOC, 3 * unite_largeur + 0, unite_hauteur - 0);
                        graphic.DrawRectangle(Pens.Black, 25 * unite_largeur + 0, YLOC, 2 * unite_largeur + 16, unite_hauteur - 0);
                        graphic.DrawRectangle(Pens.Black, 27 * unite_largeur + 16, YLOC, 1 * unite_largeur + 16, unite_hauteur - 0);
                        graphic.DrawRectangle(Pens.Black, 29 * unite_largeur + 0, YLOC, 3 * unite_largeur + 0, unite_hauteur - 0);
                        graphic.DrawRectangle(Pens.Black, 32 * unite_largeur, YLOC, 3 * unite_largeur + 0, unite_hauteur - 0);
                        graphic.DrawString(employe.ToUpper(), fnt1, Brushes.Black, unite_largeur + 20, YLOC + 3);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[4].Value.ToString())), fnt1, Brushes.Black, 14 * unite_largeur - 10, YLOC + 3, drawFormatRight);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[7].Value.ToString())), fnt1, Brushes.Black, 16 * unite_largeur + 6, YLOC + 3, drawFormatRight);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[9].Value.ToString())), fnt1, Brushes.Black, 19 * unite_largeur + 6, YLOC + 3, drawFormatRight);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[10].Value.ToString())), fnt1, Brushes.Black, 22 * unite_largeur - 10, YLOC + 3, drawFormatRight);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[18].Value.ToString())), fnt1, Brushes.Black, 25 * unite_largeur - 10, YLOC + 3, drawFormatRight);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[11].Value.ToString())), fnt1, Brushes.Black, 27 * unite_largeur + 6, YLOC + 3, drawFormatRight);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[12].Value.ToString())), fnt1, Brushes.Black, 29 * unite_largeur - 5, YLOC + 3, drawFormatRight);
                        //graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[16].Value.ToString())), fnt1, Brushes.Black, 32 * unite_largeur - 10, YLOC + 3, drawFormatRight);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[18].Value.ToString()) + Double.Parse(dgvPaiement.Rows[i].Cells[10].Value.ToString())), fnt1, Brushes.Black, 32 * unite_largeur - 10, YLOC + 3, drawFormatRight);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[17].Value.ToString())), fnt1, Brushes.Black, 35 * unite_largeur - 10, YLOC + 3, drawFormatRight);

                        count++;
                    }
                }
                j++;
                #endregion
            }

            graphic.FillRectangle(Brushes.White, 0, 34 * unite_hauteur + 10, unite_largeur * 36, unite_hauteur * 6);
            if (dgvPaiement.Rows.Count <= 6)
            {
                var index = unite_hauteur * 20 + j * unite_hauteur + 10;
                var paiement = ConnectionClass.ListeoOrdrePaiement(numeroPaiement);
                var montantTotal = .0;
                foreach (var paie in ConnectionClass.ListeDetailsPaiement(numeroPaiement))
                    montantTotal += paie.SalaireNet + paie.IRPP + paie.ONASA;
                var justification = JustifyText.TextJustification.Full;
                var rect = new Rectangle(1 * 15, index - 15, 34 * unite_largeur, 2 * unite_hauteur);
                var text = "Arrêté le présent état de paiement à la somme de : " + Converti((int)montantTotal) + "(NEP+IRPP+FIR =  " + String.Format(elGR, "{0:0,0}", montantTotal) + ") Franc CFA";
                JustifyText.DrawParagraph(graphic, rect, fnt00, Brushes.Black, text, justification, 1.2f, 1.2f, 1.2f);

                var ecart = hauteur_facture - (index + 2 * unite_hauteur + 10);
                if (ecart > 8 * unite_hauteur)
                {
                    graphic.DrawString("L'Ordonnateur des Dépenses\nLe Directeur Général de l'INSEED ", fnt11, Brushes.Black, 1 * unite_largeur + 10, index + 2 * unite_hauteur + 5);
                    graphic.DrawString("Le Contrôleur Financier Adjoint ", fnt11, Brushes.Black, 17 * unite_largeur + 10, index + 9 * unite_hauteur - 5, drawFormatCenter);
                     graphic.DrawString("Le liquidateur\nL' Agent Comptable de l'INSEED", fnt11, Brushes.Black, 34 * unite_largeur + 20, index + 2 * unite_hauteur + 5, drawFormatRight);

                    graphic.DrawString(paiement.Directeur, fnt3, Brushes.Black, 1 * unite_largeur + 10, index + 8 * unite_hauteur - 5);
                    graphic.DrawString(paiement.Liquidateur, fnt3, Brushes.Black, 34 * unite_largeur + 20, index + 8 * unite_hauteur - 5, drawFormatRight);
                }
                else
                {
                }

            }
            return bitmap;
        }

        public static Bitmap ImprimerOrdreDePaiement(int numeroPaiement
       , DataGridView dgvPaiement, int exercice, string mois, int start)
        {
            #region facture1
            //les dimension de la facture
            int unite_hauteur = 23;
            int unite_largeur = 32;
            int largeur_facture = 35 * unite_largeur + 10;
            int hauteur_facture = 32 * unite_hauteur + 16;//+ 15 + dtGrid.Rows.Count * unite_hauteur;
            if (dgvPaiement.Rows.Count <= (1 + start) * 27 + 15)
            { hauteur_facture = 33 * unite_hauteur + 16; }

            //creer un bit map
            var bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);

            Font fnt1 = new Font("Century Gothic", 10.8f, FontStyle.Regular);
            //Font fnt33 = new Font("Century Gothic", 10, FontStyle.Bold);
            Font fnt0 = new Font("Century Gothic", 10.5F, FontStyle.Bold);
            Font fnt00 = new Font("Century Gothic", 12, FontStyle.Regular);
            Font fnt11 = new Font("Century Gothic", 12, FontStyle.Bold);
            Font fnt3 = new Font("Arial Narrow", 15, FontStyle.Bold | FontStyle.Underline);

            #endregion


            var drawFormatCenter = new StringFormat();
            drawFormatCenter.Alignment = StringAlignment.Center;
            var drawFormatLeft = new StringFormat();
            drawFormatLeft.Alignment = StringAlignment.Near;
            var drawFormatRight = new StringFormat();
            drawFormatRight.Alignment = StringAlignment.Far;

            graphic.DrawRectangle(Pens.Black, 12, 1 * unite_hauteur + 8, unite_largeur + 4, 2 * unite_hauteur - 1);
            graphic.DrawRectangle(Pens.Black, 1 * unite_largeur + 16, 1 * unite_hauteur + 8, 10 * unite_largeur - 16, 2 * unite_hauteur - 1);
            graphic.DrawRectangle(Pens.Black, 11 * unite_largeur + 0, 1 * unite_hauteur + 8, 3 * unite_largeur + 0, 2 * unite_hauteur - 1);
            graphic.DrawRectangle(Pens.Black, 14 * unite_largeur + 0, 1 * unite_hauteur + 8, 2 * unite_largeur + 16, 2 * unite_hauteur - 1);
            graphic.DrawRectangle(Pens.Black, 16 * unite_largeur + 16, 1 * unite_hauteur + 8, 3 * unite_largeur + 0, 2 * unite_hauteur - 1);
            graphic.DrawRectangle(Pens.Black, 19 * unite_largeur + 16, 1 * unite_hauteur + 8, 2 * unite_largeur + 16, 2 * unite_hauteur - 1);
            graphic.DrawRectangle(Pens.Black, 22 * unite_largeur + 0, 1 * unite_hauteur + 8, 3 * unite_largeur + 0, 2 * unite_hauteur - 1);
            graphic.DrawRectangle(Pens.Black, 25 * unite_largeur + 0, 1 * unite_hauteur + 8, 2 * unite_largeur + 16, 2 * unite_hauteur - 1);
            graphic.DrawRectangle(Pens.Black, 27 * unite_largeur + 16, 1 * unite_hauteur + 8, 1 * unite_largeur + 16, 2 * unite_hauteur - 1);
            graphic.DrawRectangle(Pens.Black, 29 * unite_largeur + 0, 1 * unite_hauteur + 8, 3 * unite_largeur + 0, 2 * unite_hauteur - 1);
            graphic.DrawRectangle(Pens.Black, 32 * unite_largeur, 1 * unite_hauteur + 8, 3 * unite_largeur + 0, 2 * unite_hauteur - 1);

            graphic.DrawString("N°", fnt0, Brushes.Black, 15, 1 * unite_hauteur + 9);
            graphic.DrawString("Nom & prenom".ToUpper(), fnt0, Brushes.Black, 4 * unite_largeur + 18, 1 * unite_hauteur + 9);
            graphic.DrawString("Salaire \nde base".ToUpper(), fnt0, Brushes.Black, 11 * unite_largeur + 26, 1 * unite_hauteur + 9);
            graphic.DrawString("Congé".ToUpper(), fnt0, Brushes.Black, 13 * unite_largeur + 46, 1 * unite_hauteur + 9);
            graphic.DrawString("Salaire brut".ToUpper(), fnt0, Brushes.Black, 16 * unite_largeur + 20, 1 * unite_hauteur + 9);
            graphic.DrawString("CNPS", fnt0, Brushes.Black, 20 * unite_largeur + 6, 1 * unite_hauteur + 9);
            graphic.DrawString("Charge\npatronnale".ToUpper(), fnt0, Brushes.Black, 21 * unite_largeur + 36, 1 * unite_hauteur + 9);
            graphic.DrawString("IRPP".ToUpper(), fnt0, Brushes.Black, 25 * unite_largeur + 24, 1 * unite_hauteur + 9);
            graphic.DrawString("FIR".ToUpper(), fnt0, Brushes.Black, 27 * unite_largeur + 29, 1 * unite_hauteur + 9);
           graphic.DrawString("COTISATIONS\nSOCIALES".ToUpper(), fnt0, Brushes.Black, 29 * unite_largeur + 2, 1 * unite_hauteur + 9);
            graphic.DrawString("Salaire Net".ToUpper(), fnt0, Brushes.Black, 32 * unite_largeur + 5, 1 * unite_hauteur + 9);
            var j = 0;
            var count = 1;
            for (var i = 27 * start + 15; i < dgvPaiement.Rows.Count; i++)
            {
                #region MyRegion

                var YLOC = unite_hauteur * 3 + 7 + unite_hauteur * j;

                var employe = dgvPaiement.Rows[i].Cells[1].Value.ToString();

                if (dgvPaiement.Rows[i].Cells[1].Value.ToString().Contains("TOTAL"))
                {
                    if (dgvPaiement.Rows[i].Cells[1].Value.ToString().Contains("TOTAL GENERAL"))
                    {
                        graphic.FillRectangle(Brushes.WhiteSmoke, 13, YLOC - 3, unite_largeur * 35 - 13, unite_hauteur - 2);
                    }

                    graphic.DrawRectangle(Pens.Black, 12, YLOC, 11 * unite_largeur - 12, unite_hauteur - 2);
                    //graphic.DrawRectangle(Pens.Black, 1 * unite_largeur + 16, YLOC, 10 * unite_largeur -16, unite_hauteur - 2);
                    graphic.DrawRectangle(Pens.Black, 11 * unite_largeur + 0, YLOC, 3 * unite_largeur + 0, unite_hauteur - 2);
                    graphic.DrawRectangle(Pens.Black, 14 * unite_largeur + 0, YLOC, 2 * unite_largeur + 16, unite_hauteur - 2);
                    graphic.DrawRectangle(Pens.Black, 16 * unite_largeur + 16, YLOC, 3 * unite_largeur + 0, unite_hauteur - 2);
                    graphic.DrawRectangle(Pens.Black, 19 * unite_largeur + 16, YLOC, 2 * unite_largeur + 16, unite_hauteur - 2);
                    graphic.DrawRectangle(Pens.Black, 22 * unite_largeur + 0, YLOC, 3 * unite_largeur + 0, unite_hauteur - 2);
                    graphic.DrawRectangle(Pens.Black, 25 * unite_largeur + 0, YLOC, 2 * unite_largeur + 16, unite_hauteur - 2);
                    graphic.DrawRectangle(Pens.Black, 27 * unite_largeur + 16, YLOC, 1 * unite_largeur + 16, unite_hauteur - 2);
                    graphic.DrawRectangle(Pens.Black, 29 * unite_largeur + 0, YLOC, 3 * unite_largeur + 0, unite_hauteur - 2);
                    graphic.DrawRectangle(Pens.Black, 32 * unite_largeur, YLOC, 3 * unite_largeur + 0, unite_hauteur - 2);

                    graphic.DrawString(employe.ToUpper(), fnt0, Brushes.Black, 15, YLOC + 3);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[4].Value.ToString())), fnt0, Brushes.Black, 14 * unite_largeur - 10, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[7].Value.ToString())), fnt0, Brushes.Black, 16 * unite_largeur + 6, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[9].Value.ToString())), fnt0, Brushes.Black, 19 * unite_largeur + 6, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[10].Value.ToString())), fnt0, Brushes.Black, 22 * unite_largeur - 10, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[18].Value.ToString())), fnt0, Brushes.Black, 25 * unite_largeur - 10, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[11].Value.ToString())), fnt0, Brushes.Black, 27 * unite_largeur + 6, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[12].Value.ToString())), fnt0, Brushes.Black, 29 * unite_largeur - 5, YLOC + 3, drawFormatRight);
                    //graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[16].Value.ToString())), fnt0, Brushes.Black, 32 * unite_largeur - 10, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[18].Value.ToString()) + Double.Parse(dgvPaiement.Rows[i].Cells[10].Value.ToString())), fnt0, Brushes.Black, 32 * unite_largeur - 10, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[17].Value.ToString())), fnt0, Brushes.Black, 35 * unite_largeur - 10, YLOC + 3, drawFormatRight);

                }
                else
                {
                    if (string.IsNullOrWhiteSpace(dgvPaiement.Rows[i].Cells[4].Value.ToString()) && string.IsNullOrWhiteSpace(dgvPaiement.Rows[i].Cells[5].Value.ToString()))
                    {
                        graphic.DrawRectangle(Pens.Black, 12, YLOC - 2, unite_largeur * 35 - 12, unite_hauteur + 0);
                        graphic.FillRectangle(Brushes.WhiteSmoke, 13, YLOC + 1, unite_largeur * 35 - 14, unite_hauteur - 1);
                        graphic.DrawString(employe.ToUpper(), fnt0, Brushes.Black, 20, YLOC + 3);
                    }
                    else
                    {
                        if (!string.IsNullOrWhiteSpace(dgvPaiement.Rows[i].Cells[20].Value.ToString()))
                        {
                            var numero = dgvPaiement.Rows[i].Cells[20].Value.ToString();
                            if (numero.Length == 1)
                                numero = "0" + numero;
                            graphic.DrawString(numero, fnt1, Brushes.Black, unite_largeur + 12, YLOC + 3, drawFormatRight);
                        }
                        graphic.DrawRectangle(Pens.Black, 12, YLOC, unite_largeur + 4, unite_hauteur - 0);
                        graphic.DrawRectangle(Pens.Black, 1 * unite_largeur + 16, YLOC, 10 * unite_largeur - 16, unite_hauteur - 0);
                        graphic.DrawRectangle(Pens.Black, 11 * unite_largeur + 0, YLOC, 3 * unite_largeur + 0, unite_hauteur - 0);
                        graphic.DrawRectangle(Pens.Black, 14 * unite_largeur + 0, YLOC, 2 * unite_largeur + 16, unite_hauteur - 0);
                        graphic.DrawRectangle(Pens.Black, 16 * unite_largeur + 16, YLOC, 3 * unite_largeur + 0, unite_hauteur - 0);
                        graphic.DrawRectangle(Pens.Black, 19 * unite_largeur + 16, YLOC, 2 * unite_largeur + 16, unite_hauteur - 0);
                        graphic.DrawRectangle(Pens.Black, 22 * unite_largeur + 0, YLOC, 3 * unite_largeur + 0, unite_hauteur - 0);
                        graphic.DrawRectangle(Pens.Black, 25 * unite_largeur + 0, YLOC, 2 * unite_largeur + 16, unite_hauteur - 0);
                        graphic.DrawRectangle(Pens.Black, 27 * unite_largeur + 16, YLOC, 1 * unite_largeur + 16, unite_hauteur - 0);
                        graphic.DrawRectangle(Pens.Black, 29 * unite_largeur + 0, YLOC, 3 * unite_largeur + 0, unite_hauteur - 0);
                        graphic.DrawRectangle(Pens.Black, 32 * unite_largeur, YLOC, 3 * unite_largeur + 0, unite_hauteur - 0);
                        graphic.DrawString(employe.ToUpper(), fnt1, Brushes.Black, unite_largeur + 20, YLOC + 3);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[4].Value.ToString())), fnt1, Brushes.Black, 14 * unite_largeur - 10, YLOC + 3, drawFormatRight);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[7].Value.ToString())), fnt1, Brushes.Black, 16 * unite_largeur + 6, YLOC + 3, drawFormatRight);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[9].Value.ToString())), fnt1, Brushes.Black, 19 * unite_largeur + 6, YLOC + 3, drawFormatRight);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[10].Value.ToString())), fnt1, Brushes.Black, 22 * unite_largeur - 10, YLOC + 3, drawFormatRight);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[18].Value.ToString())), fnt1, Brushes.Black, 25 * unite_largeur - 10, YLOC + 3, drawFormatRight);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[11].Value.ToString())), fnt1, Brushes.Black, 27 * unite_largeur + 6, YLOC + 3, drawFormatRight);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[12].Value.ToString())), fnt1, Brushes.Black, 29 * unite_largeur - 5, YLOC + 3, drawFormatRight);
                        //graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[16].Value.ToString())), fnt1, Brushes.Black, 32 * unite_largeur - 10, YLOC + 3, drawFormatRight);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[18].Value.ToString()) + Double.Parse(dgvPaiement.Rows[i].Cells[10].Value.ToString())), fnt1, Brushes.Black, 32 * unite_largeur - 10, YLOC + 3, drawFormatRight);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[17].Value.ToString())), fnt1, Brushes.Black, 35 * unite_largeur - 10, YLOC + 3, drawFormatRight);

                        count++;
                    }
                }
                j++;
                #endregion
            }
            graphic.FillRectangle(Brushes.White, 0, 30 * unite_hauteur + 8, unite_largeur * 36, unite_hauteur * 6);
            if (dgvPaiement.Rows.Count <= (1 + start) * 27 + 15)
            {
                var index = unite_hauteur * 4 + j * unite_hauteur + 10;
                var paiement = ConnectionClass.ListeoOrdrePaiement(numeroPaiement);
                var montantTotal = .0;
                foreach (var paie in ConnectionClass.ListeDetailsPaiement(numeroPaiement))
                    montantTotal += paie.SalaireNet + paie.IRPP + paie.ONASA;
                var justification = JustifyText.TextJustification.Full;
                var rect = new Rectangle(1 * 15, index - 15, 34 * unite_largeur, 2 * unite_hauteur);
                var text = "Arrêté le présent état de paiement à la somme de : " + Converti((int)montantTotal) + "(NEP+IRPP+FIR =  " + String.Format(elGR, "{0:0,0}", montantTotal) + ") Franc CFA";
                JustifyText.DrawParagraph(graphic, rect, fnt00, Brushes.Black, text, justification, 1.2f, 1.2f, 1.2f);

          var ecart = hauteur_facture - (index + 2 * unite_hauteur + 10);
                if (ecart >8 * unite_hauteur)
                {
                    graphic.DrawString("L'Ordonnateur des Dépenses\nLe Directeur Général de l'INSEED ", fnt11, Brushes.Black, 1 * unite_largeur + 10, index + 2 * unite_hauteur + 5);
                    graphic.DrawString("Le Contrôleur Financier Adjoint ", fnt11, Brushes.Black, 17 * unite_largeur + 10, index + 9 * unite_hauteur - 5, drawFormatCenter);
                    graphic.DrawString("Le liquidateur\nL' Agent Comptable de l'INSEED", fnt11, Brushes.Black, 34 * unite_largeur + 20, index + 2 * unite_hauteur + 5, drawFormatRight);

                    graphic.DrawString(paiement.Directeur, fnt3, Brushes.Black, 1 * unite_largeur + 10, index + 9 * unite_hauteur - 5);
                        graphic.DrawString(paiement.Liquidateur, fnt3, Brushes.Black, 34 * unite_largeur + 20, index + 9 * unite_hauteur - 5, drawFormatRight);
                }
                else
                {
                    graphic.FillRectangle(Brushes.White, 0, index - 16, unite_largeur * 36, unite_hauteur * 9);
                }
            }
            return bitmap;
        }

        public static Bitmap ImprimerOrdreDePaiement(int numeroPaiement
     , List<Paiement> listePaiement, int exercice, string mois)
        {
            #region facture1
            //les dimension de la facture
            int unite_hauteur = 23;
            int unite_largeur = 32;
            int largeur_facture = 35 * unite_largeur + 10;
            int hauteur_facture = 35 * unite_hauteur + 20;//+ 15 + dtGrid.Rows.Count * unite_hauteur;


            //creer un bit map
            var bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            try
            {
                Image logo = global::SGSP.Properties.Resources.etatPaiement_1;
                graphic.DrawImage(logo, 0, 0, largeur_facture, hauteur_facture);
            }
            catch { }
            //definir les polices 
            Font fnt1 = new Font("Century Gothic", 10.8f, FontStyle.Regular);
            //Font fnt33 = new Font("Century Gothic", 10, FontStyle.Bold);
            Font fnt0 = new Font("Century Gothic", 10.5F, FontStyle.Bold);
            Font fnt00 = new Font("Century Gothic", 12, FontStyle.Regular);
            Font fnt11 = new Font("Century Gothic", 12, FontStyle.Bold);
            Font fnt3 = new Font("Arial Narrow", 15, FontStyle.Bold | FontStyle.Underline);

            #endregion

            graphic.DrawString("Page " + 1, fnt1, Brushes.Black, 33 * unite_largeur, unite_hauteur);
            graphic.DrawString(DateTime.Now.ToShortDateString(), fnt11, Brushes.Black, unite_largeur * 23 - 2, 11 * unite_hauteur - 8);
            graphic.DrawString("Etat pour servir de paiement des salaires du personnel contractuel de l'INSEED pour la période du : " + mois.ToUpper() + " " +
                exercice, fnt3, Brushes.Black, unite_largeur * 4, 16 * unite_hauteur - 5);
           
            var drawFormatCenter = new StringFormat();
            drawFormatCenter.Alignment = StringAlignment.Center;
            var drawFormatLeft = new StringFormat();
            drawFormatLeft.Alignment = StringAlignment.Near;
            var drawFormatRight = new StringFormat();
            drawFormatRight.Alignment = StringAlignment.Far;

            graphic.DrawRectangle(Pens.Black, 12, 17 * unite_hauteur + 8, unite_largeur + 4, 2 * unite_hauteur - 1);
            graphic.DrawRectangle(Pens.Black, 1 * unite_largeur + 16, 17 * unite_hauteur + 8, 10 * unite_largeur - 16, 2 * unite_hauteur - 1);
            graphic.DrawRectangle(Pens.Black, 11 * unite_largeur + 0, 17 * unite_hauteur + 8, 3 * unite_largeur + 0, 2 * unite_hauteur - 1);
            graphic.DrawRectangle(Pens.Black, 14 * unite_largeur + 0, 17 * unite_hauteur + 8, 2 * unite_largeur + 16, 2 * unite_hauteur - 1);
            graphic.DrawRectangle(Pens.Black, 16 * unite_largeur + 16, 17 * unite_hauteur + 8, 3 * unite_largeur + 0, 2 * unite_hauteur - 1);
            graphic.DrawRectangle(Pens.Black, 19 * unite_largeur + 16, 17 * unite_hauteur + 8, 2 * unite_largeur + 16, 2 * unite_hauteur - 1);
            graphic.DrawRectangle(Pens.Black, 22 * unite_largeur + 0, 17 * unite_hauteur + 8, 3 * unite_largeur + 0, 2 * unite_hauteur - 1);
            graphic.DrawRectangle(Pens.Black, 25 * unite_largeur + 0, 17 * unite_hauteur + 8, 2 * unite_largeur + 16, 2 * unite_hauteur - 1);
            graphic.DrawRectangle(Pens.Black, 27 * unite_largeur + 16, 17 * unite_hauteur + 8, 1 * unite_largeur + 16, 2 * unite_hauteur - 1);
            graphic.DrawRectangle(Pens.Black, 29 * unite_largeur + 0, 17 * unite_hauteur + 8, 3 * unite_largeur + 0, 2 * unite_hauteur - 1);
            graphic.DrawRectangle(Pens.Black, 32 * unite_largeur, 17 * unite_hauteur + 8, 3 * unite_largeur + 0, 2 * unite_hauteur - 1);

            graphic.DrawString("N°", fnt0, Brushes.Black, 15, 17 * unite_hauteur + 9);
            graphic.DrawString("Nom & prenom".ToUpper(), fnt0, Brushes.Black, 4 * unite_largeur + 18, 17 * unite_hauteur + 9);
            graphic.DrawString("Salaire".ToUpper(), fnt0, Brushes.Black, 11 * unite_largeur + 26, 17 * unite_hauteur + 9);
            graphic.DrawString("Congé".ToUpper(), fnt0, Brushes.Black, 13 * unite_largeur + 46, 17 * unite_hauteur + 9);
            graphic.DrawString("Salaire brut".ToUpper(), fnt0, Brushes.Black, 16 * unite_largeur + 20, 17 * unite_hauteur + 9);
            graphic.DrawString("CNPS", fnt0, Brushes.Black, 20 * unite_largeur + 6, 17 * unite_hauteur + 9);
            graphic.DrawString("Charge\npatronnale".ToUpper(), fnt0, Brushes.Black, 21 * unite_largeur + 36, 17 * unite_hauteur + 9);
            graphic.DrawString("IRPP".ToUpper(), fnt0, Brushes.Black, 25 * unite_largeur + 24, 17 * unite_hauteur + 9);
            graphic.DrawString("FIR".ToUpper(), fnt0, Brushes.Black, 27 * unite_largeur + 29, 17 * unite_hauteur + 9);
            graphic.DrawString("COTISATIONS\nSOCIALES".ToUpper(), fnt0, Brushes.Black, 29 * unite_largeur + 2, 17 * unite_hauteur + 9);
            graphic.DrawString("Salaire Net".ToUpper(), fnt0, Brushes.Black, 32 * unite_largeur + 5, 17 * unite_hauteur + 9);
            var j = 0;
            var count = 1;
            for (var i = 0; i < listePaiement.Count; i++)
            {
                #region MyRegion

                var YLOC = unite_hauteur * 19 + 7 + unite_hauteur * j;

                graphic.DrawRectangle(Pens.Black, 11 * unite_largeur + 0, YLOC, 3 * unite_largeur + 0, unite_hauteur - 0);
                graphic.DrawRectangle(Pens.Black, 14 * unite_largeur + 0, YLOC, 2 * unite_largeur + 16, unite_hauteur - 0);
                graphic.DrawRectangle(Pens.Black, 16 * unite_largeur + 16, YLOC, 3 * unite_largeur + 0, unite_hauteur - 0);
                graphic.DrawRectangle(Pens.Black, 19 * unite_largeur + 16, YLOC, 2 * unite_largeur + 16, unite_hauteur - 0);
                graphic.DrawRectangle(Pens.Black, 22 * unite_largeur + 0, YLOC, 3 * unite_largeur + 0, unite_hauteur - 0);
                graphic.DrawRectangle(Pens.Black, 25 * unite_largeur + 0, YLOC, 2 * unite_largeur + 16, unite_hauteur - 0);
                graphic.DrawRectangle(Pens.Black, 27 * unite_largeur + 16, YLOC, 1 * unite_largeur + 16, unite_hauteur - 0);
                graphic.DrawRectangle(Pens.Black, 29 * unite_largeur + 0, YLOC, 3 * unite_largeur + 0, unite_hauteur - 0);
                graphic.DrawRectangle(Pens.Black, 32 * unite_largeur, YLOC, 3 * unite_largeur + 0, unite_hauteur - 0);
                if (listePaiement[i].Employe == "TOTAL")
                {
                    graphic.DrawRectangle(Pens.Black, 12, YLOC, 11*unite_largeur -12, unite_hauteur - 0);
                //graphic.DrawRectangle(Pens.Black, 1 * unite_largeur + 16, YLOC, 10 * unite_largeur - 16, unite_hauteur - 0);
                    graphic.DrawString(listePaiement[i].Employe, fnt0, Brushes.Black, unite_largeur + 12, YLOC + 3);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].SalaireBase), fnt0, Brushes.Black, 14 * unite_largeur - 10, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].CongeAnnuel), fnt0, Brushes.Black, 16 * unite_largeur + 6, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].SalaireBrut), fnt0, Brushes.Black, 19 * unite_largeur + 6, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].CNPS), fnt0, Brushes.Black, 22 * unite_largeur - 10, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].ChargePatronale), fnt0, Brushes.Black, 25 * unite_largeur - 10, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].IRPP), fnt0, Brushes.Black, 27 * unite_largeur + 6, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].ONASA), fnt0, Brushes.Black, 29 * unite_largeur - 5, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].CNPS + listePaiement[0].ChargePatronale), fnt0, Brushes.Black, 32 * unite_largeur - 10, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].SalaireNet), fnt0, Brushes.Black, 35 * unite_largeur - 10, YLOC + 3, drawFormatRight);
                }
                else
                {
                    graphic.DrawRectangle(Pens.Black, 12, YLOC, unite_largeur + 4, unite_hauteur - 0);
                    graphic.DrawRectangle(Pens.Black, 1 * unite_largeur + 16, YLOC, 10 * unite_largeur - 16, unite_hauteur - 0); 
                    graphic.DrawString(count.ToString(), fnt1, Brushes.Black, unite_largeur + 10, YLOC + 3, drawFormatRight);
                    graphic.DrawString(listePaiement[i].Employe, fnt1, Brushes.Black, unite_largeur + 20, YLOC + 3);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].SalaireBase), fnt1, Brushes.Black, 14 * unite_largeur - 10, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].CongeAnnuel), fnt1, Brushes.Black, 16 * unite_largeur + 6, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].SalaireBrut), fnt1, Brushes.Black, 19 * unite_largeur + 6, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].CNPS), fnt1, Brushes.Black, 22 * unite_largeur - 10, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].ChargePatronale), fnt1, Brushes.Black, 25 * unite_largeur - 10, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].IRPP), fnt1, Brushes.Black, 27 * unite_largeur + 6, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].ONASA), fnt1, Brushes.Black, 29 * unite_largeur - 5, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].CNPS + listePaiement[0].ChargePatronale), fnt1, Brushes.Black, 32 * unite_largeur - 10, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].SalaireNet), fnt1, Brushes.Black, 35 * unite_largeur - 10, YLOC + 3, drawFormatRight);

                    count++;
                }
                j++;
                #endregion
            }

            graphic.FillRectangle(Brushes.White, 0, 34 * unite_hauteur + 9, unite_largeur * 36, unite_hauteur * 6);
            if (listePaiement.Count <= 6)
            {
                var index = unite_hauteur * 20 + j * unite_hauteur + 10;
                var paiement = ConnectionClass.ListeoOrdrePaiement(numeroPaiement);
                var montantTotal = .0;
                foreach (var paie in ConnectionClass.ListeDetailsPaiement(numeroPaiement))
                    montantTotal += paie.SalaireNet + paie.IRPP + paie.ONASA;
                var justification = JustifyText.TextJustification.Center;
                var rect = new Rectangle(1 * 12, index - 15, 35* unite_largeur, 2 * unite_hauteur);
                var text = "Arrêté le présent état de paiement à la somme de : " + Converti((int)montantTotal) + "(NEP+IRPP+FIR =  " + String.Format(elGR, "{0:0,0}", montantTotal) + ") Franc CFA";
                JustifyText.DrawParagraph(graphic, rect, fnt00, Brushes.Black, text, justification, 1.2f, 1.2f, 1.2f);
                var ecart = hauteur_facture - (index + 2 * unite_hauteur + 10);
                if (ecart > 8 * unite_hauteur)
                {
                    graphic.DrawString("L'Ordonnateur des Dépenses\nLe Directeur Général de l'INSEED ", fnt11, Brushes.Black, 1 * unite_largeur + 10, index + 2 * unite_hauteur + 5);
                    graphic.DrawString("Le Contrôleur Financier Adjoint ", fnt11, Brushes.Black, 17 * unite_largeur + 10, index + 9 * unite_hauteur - 5, drawFormatCenter);
                    graphic.DrawString("Le liquidateur\nL' Agent Comptable de l'INSEED", fnt11, Brushes.Black, 34 * unite_largeur + 20, index + 2 * unite_hauteur + 5, drawFormatRight);

                    graphic.DrawString(paiement.Directeur, fnt3, Brushes.Black, 1 * unite_largeur + 10, index + 8 * unite_hauteur - 5);
                    graphic.DrawString(paiement.Liquidateur, fnt3, Brushes.Black, 34 * unite_largeur + 20, index + 8 * unite_hauteur - 5, drawFormatRight);
                }
                else
                {
                }
            }
            return bitmap;
        }

        public static Bitmap ImprimerOrdreDePaiement(int numeroPaiement
       , List<Paiement> listePaiement, int exercice, string mois, int start)
        {
            #region facture1
            //les dimension de la facture
            int unite_hauteur = 23;
            int unite_largeur = 32;
            int largeur_facture = 35 * unite_largeur + 10;
            int hauteur_facture = 32 * unite_hauteur + 16;//+ 15 + dtGrid.Rows.Count * unite_hauteur;
            if (listePaiement.Count <= (1 + start) * 27 + 15)
            { hauteur_facture = 33 * unite_hauteur + 16; }

            //creer un bit map
            var bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);

            Font fnt1 = new Font("Century Gothic", 10.8f, FontStyle.Regular);
            //Font fnt33 = new Font("Century Gothic", 10, FontStyle.Bold);
            Font fnt0 = new Font("Century Gothic", 10.5F, FontStyle.Bold);
            Font fnt00 = new Font("Century Gothic", 12, FontStyle.Regular);
            Font fnt11 = new Font("Century Gothic", 12, FontStyle.Bold);
            Font fnt3 = new Font("Arial Narrow", 15, FontStyle.Bold | FontStyle.Underline);

            #endregion


            var drawFormatCenter = new StringFormat();
            drawFormatCenter.Alignment = StringAlignment.Center;
            var drawFormatLeft = new StringFormat();
            drawFormatLeft.Alignment = StringAlignment.Near;
            var drawFormatRight = new StringFormat();
            drawFormatRight.Alignment = StringAlignment.Far;

            graphic.DrawRectangle(Pens.Black, 12, 1 * unite_hauteur + 8, unite_largeur + 4, 2 * unite_hauteur - 1);
            graphic.DrawRectangle(Pens.Black, 1 * unite_largeur + 16, 1 * unite_hauteur + 8, 10 * unite_largeur - 16, 2 * unite_hauteur - 1);
            graphic.DrawRectangle(Pens.Black, 11 * unite_largeur + 0, 1 * unite_hauteur + 8, 3 * unite_largeur + 0, 2 * unite_hauteur - 1);
            graphic.DrawRectangle(Pens.Black, 14 * unite_largeur + 0, 1 * unite_hauteur + 8, 2 * unite_largeur + 16, 2 * unite_hauteur - 1);
            graphic.DrawRectangle(Pens.Black, 16 * unite_largeur + 16, 1 * unite_hauteur + 8, 3 * unite_largeur + 0, 2 * unite_hauteur - 1);
            graphic.DrawRectangle(Pens.Black, 19 * unite_largeur + 16, 1 * unite_hauteur + 8, 2 * unite_largeur + 16, 2 * unite_hauteur - 1);
            graphic.DrawRectangle(Pens.Black, 22 * unite_largeur + 0, 1 * unite_hauteur + 8, 3 * unite_largeur + 0, 2 * unite_hauteur - 1);
            graphic.DrawRectangle(Pens.Black, 25 * unite_largeur + 0, 1 * unite_hauteur + 8, 2 * unite_largeur + 16, 2 * unite_hauteur - 1);
            graphic.DrawRectangle(Pens.Black, 27 * unite_largeur + 16, 1 * unite_hauteur + 8, 1 * unite_largeur + 16, 2 * unite_hauteur - 1);
             graphic.DrawRectangle(Pens.Black, 29 * unite_largeur + 0, 1 * unite_hauteur + 8, 3 * unite_largeur + 0, 2 * unite_hauteur - 1);
            graphic.DrawRectangle(Pens.Black, 32 * unite_largeur, 1 * unite_hauteur + 8, 3 * unite_largeur + 0, 2 * unite_hauteur - 1);

            graphic.DrawString("N°", fnt0, Brushes.Black, 15, 1 * unite_hauteur + 9);
            graphic.DrawString("Nom & prenom".ToUpper(), fnt0, Brushes.Black, 4 * unite_largeur + 18, 1 * unite_hauteur + 9);
            graphic.DrawString("Salaire \nde base".ToUpper(), fnt0, Brushes.Black, 11 * unite_largeur + 26, 1 * unite_hauteur + 9);
            graphic.DrawString("Congé".ToUpper(), fnt0, Brushes.Black, 13 * unite_largeur + 46, 1 * unite_hauteur + 9);
            graphic.DrawString("Salaire brut".ToUpper(), fnt0, Brushes.Black, 16 * unite_largeur + 20, 1 * unite_hauteur + 9);
            graphic.DrawString("CNPS", fnt0, Brushes.Black, 20 * unite_largeur + 6, 1 * unite_hauteur + 9);
            graphic.DrawString("Charge\npatronnale".ToUpper(), fnt0, Brushes.Black, 21 * unite_largeur + 36, 1 * unite_hauteur + 9);
            graphic.DrawString("IRPP".ToUpper(), fnt0, Brushes.Black, 25 * unite_largeur + 24, 1 * unite_hauteur + 9);
            graphic.DrawString("FIR".ToUpper(), fnt0, Brushes.Black, 27 * unite_largeur + 29, 1 * unite_hauteur + 9);
           graphic.DrawString("COTISATIONS\nSOCIALES".ToUpper(), fnt0, Brushes.Black, 29 * unite_largeur + 2, 1 * unite_hauteur + 9);
            graphic.DrawString("Salaire Net".ToUpper(), fnt0, Brushes.Black, 32 * unite_largeur + 5, 1 * unite_hauteur + 9);
            var j = 0;
            var count = 27 * start+16;
            var montantTotal = .0;
            for (var i = 27 * start + 15; i < listePaiement.Count; i++)
            {
                #region MyRegion

                var YLOC = unite_hauteur * 3 + 7 + unite_hauteur * j;

                graphic.DrawRectangle(Pens.Black, 11 * unite_largeur + 0, YLOC, 3 * unite_largeur + 0, unite_hauteur - 0);
                graphic.DrawRectangle(Pens.Black, 14 * unite_largeur + 0, YLOC, 2 * unite_largeur + 16, unite_hauteur - 0);
                graphic.DrawRectangle(Pens.Black, 16 * unite_largeur + 16, YLOC, 3 * unite_largeur + 0, unite_hauteur - 0);
                graphic.DrawRectangle(Pens.Black, 19 * unite_largeur + 16, YLOC, 2 * unite_largeur + 16, unite_hauteur - 0);
                graphic.DrawRectangle(Pens.Black, 22 * unite_largeur + 0, YLOC, 3 * unite_largeur + 0, unite_hauteur - 0);
                graphic.DrawRectangle(Pens.Black, 25 * unite_largeur + 0, YLOC, 2 * unite_largeur + 16, unite_hauteur - 0);
                graphic.DrawRectangle(Pens.Black, 27 * unite_largeur + 16, YLOC, 1 * unite_largeur + 16, unite_hauteur - 0);
                graphic.DrawRectangle(Pens.Black, 29 * unite_largeur + 0, YLOC, 3 * unite_largeur + 0, unite_hauteur - 0);
                graphic.DrawRectangle(Pens.Black, 32 * unite_largeur, YLOC, 3 * unite_largeur + 0, unite_hauteur - 0);
                if (listePaiement[i].Employe == "TOTAL")
                {
                montantTotal=   listePaiement[i].ONASA + listePaiement[i].SalaireNet+listePaiement[i].IRPP ;
                        
                    graphic.DrawRectangle(Pens.Black, 12, YLOC, 11 * unite_largeur - 12, unite_hauteur - 0);
                    //graphic.DrawRectangle(Pens.Black, 1 * unite_largeur + 16, YLOC, 10 * unite_largeur - 16, unite_hauteur - 0);
                    graphic.DrawString(listePaiement[i].Employe, fnt0, Brushes.Black, unite_largeur + 12, YLOC + 3);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].SalaireBase), fnt0, Brushes.Black, 14 * unite_largeur - 10, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].CongeAnnuel), fnt0, Brushes.Black, 16 * unite_largeur + 6, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].SalaireBrut), fnt0, Brushes.Black, 19 * unite_largeur + 6, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].CNPS), fnt0, Brushes.Black, 22 * unite_largeur - 10, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].ChargePatronale), fnt0, Brushes.Black, 25 * unite_largeur - 10, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].IRPP), fnt0, Brushes.Black, 27 * unite_largeur + 6, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].ONASA), fnt0, Brushes.Black, 29 * unite_largeur - 5, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].CNPS + listePaiement[i].ChargePatronale), fnt0, Brushes.Black, 32 * unite_largeur - 10, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].SalaireNet), fnt0, Brushes.Black, 35 * unite_largeur - 10, YLOC + 3, drawFormatRight);
                }
                else
                {
                    graphic.DrawRectangle(Pens.Black, 12, YLOC, unite_largeur + 4, unite_hauteur - 0);
                    graphic.DrawRectangle(Pens.Black, 1 * unite_largeur + 16, YLOC, 10 * unite_largeur - 16, unite_hauteur - 0);

                    graphic.DrawString(count.ToString(), fnt1, Brushes.Black,  unite_largeur +10, YLOC + 3,drawFormatRight);
                    graphic.DrawString(listePaiement[i].Employe, fnt1, Brushes.Black, unite_largeur + 20, YLOC + 3);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].SalaireBase), fnt1, Brushes.Black, 14 * unite_largeur - 10, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].CongeAnnuel), fnt1, Brushes.Black, 16 * unite_largeur + 6, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].SalaireBrut), fnt1, Brushes.Black, 19 * unite_largeur + 6, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].CNPS), fnt1, Brushes.Black, 22 * unite_largeur - 10, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].ChargePatronale), fnt1, Brushes.Black, 25 * unite_largeur - 10, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].IRPP), fnt1, Brushes.Black, 27 * unite_largeur + 6, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].ONASA), fnt1, Brushes.Black, 29 * unite_largeur - 5, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].CNPS + listePaiement[0].ChargePatronale), fnt1, Brushes.Black, 32 * unite_largeur - 10, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].SalaireNet), fnt1, Brushes.Black, 35 * unite_largeur - 10, YLOC + 3, drawFormatRight);

                    count++;
                }
                j++;
                #endregion
            }
            graphic.FillRectangle(Brushes.White, 0, 30 * unite_hauteur + 8, unite_largeur * 36, unite_hauteur * 6);
            if (listePaiement.Count <= (1 + start) * 27 + 15)
            {
                var index = unite_hauteur * 4 + j * unite_hauteur + 10;
                var paiement = ConnectionClass.ListeoOrdrePaiement(numeroPaiement);
           
                var justification = JustifyText.TextJustification.Full;
                var rect = new Rectangle(1 * 15, index - 15, 34 * unite_largeur, 2 * unite_hauteur);
                var text = "Arrêté le présent état de paiement à la somme de : " + Converti((int)montantTotal) + "(NEP+IRPP+FIR =  " + String.Format(elGR, "{0:0,0}", montantTotal) + ") Franc CFA";
                JustifyText.DrawParagraph(graphic, rect, fnt00, Brushes.Black, text, justification, 1.2f, 1.2f, 1.2f);

                var ecart = hauteur_facture - (index + 2 * unite_hauteur + 10);
                if (ecart >8 * unite_hauteur)
                {
                   graphic.DrawString("L'Ordonnateur des Dépenses\nLe Directeur Général de l'INSEED ", fnt11, Brushes.Black, 1 * unite_largeur + 10, index + 2 * unite_hauteur + 5);
                    graphic.DrawString("Le Contrôleur Financier Adjoint ", fnt11, Brushes.Black, 17 * unite_largeur + 10, index + 8 * unite_hauteur - 5, drawFormatCenter);
                  graphic.DrawString("Le liquidateur\nL' Agent Comptable de l'INSEED", fnt11, Brushes.Black, 34 * unite_largeur + 20, index + 2 * unite_hauteur + 5, drawFormatRight);

                    graphic.DrawString(paiement.Directeur, fnt3, Brushes.Black, 1 * unite_largeur + 10, index + 8 * unite_hauteur - 5);
                   graphic.DrawString(paiement.Liquidateur, fnt3, Brushes.Black, 34 * unite_largeur + 20, index + 8 * unite_hauteur - 5, drawFormatRight);
                }
                else
                {
                    graphic.FillRectangle(Brushes.White, 0, index - 16, unite_largeur * 36, unite_hauteur * 9);
                }
            }
            return bitmap;
        }

        public static Bitmap ImprimerOrdreDePaiementPrimes(int numeroPaiement
     , List<Paiement> listePaiement, int exercice, string mois)
        {
            #region facture1
            //les dimension de la facture
            int unite_hauteur = 23;
            int unite_largeur = 32;
            int largeur_facture = 35 * unite_largeur + 10;
            int hauteur_facture = 35 * unite_hauteur + 20;//+ 15 + dtGrid.Rows.Count * unite_hauteur;


            //creer un bit map
            var bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            try
            {
                Image logo = global::SGSP.Properties.Resources.etatPaiement_1;
                graphic.DrawImage(logo, -15, 0, largeur_facture+15, hauteur_facture);
            }
            catch { }
            //definir les polices 
            Font fnt1 = new Font("Century Gothic", 10.8f, FontStyle.Regular);
            //Font fnt33 = new Font("Century Gothic", 10, FontStyle.Bold);
            Font fnt0 = new Font("Century Gothic", 10.8F, FontStyle.Bold);
            Font fnt00 = new Font("Century Gothic", 12, FontStyle.Regular);
            Font fnt11 = new Font("Century Gothic", 12, FontStyle.Bold);
            Font fnt3 = new Font("Arial Narrow", 15, FontStyle.Bold | FontStyle.Underline);

            #endregion

            var drawFormatCenter = new StringFormat();
            drawFormatCenter.Alignment = StringAlignment.Center;
            var drawFormatLeft = new StringFormat();
            drawFormatLeft.Alignment = StringAlignment.Near;
            var drawFormatRight = new StringFormat();
            drawFormatRight.Alignment = StringAlignment.Far;

            graphic.DrawString("Page " + 1, fnt1, Brushes.Black, 33 * unite_largeur, unite_hauteur);
            graphic.DrawString(DateTime.Now.ToShortDateString(), fnt11, Brushes.Black, unite_largeur * 23 - 2, 11 * unite_hauteur - 8);
            graphic.DrawString("Etat pour servir de paiement des primes du personnel de l'INSEED pour la période du : " + mois.ToUpper() + " " +
                exercice, fnt3, Brushes.Black, unite_largeur * 17, 16 * unite_hauteur - 5, drawFormatCenter);

            //var rect1 = new Rectangle(1 * unite_largeur + 0, 17 * unite_hauteur + 8, 2 * unite_largeur + 0, 2 * unite_hauteur - 1);
            //var rect2=new Rectangle(3 * unite_largeur + 0, 17 * unite_hauteur + 8, 11 * unite_largeur + 0, 2 * unite_hauteur - 1);
            //var rect3 = new Rectangle(14 * unite_largeur + 0, 17 * unite_hauteur + 8, 5 * unite_largeur + 0, 2 * unite_hauteur - 1);
            //var rect4 = new Rectangle(19 * unite_largeur + 0, 17 * unite_hauteur + 8, 5 * unite_largeur + 0, 2 * unite_hauteur - 1);
            //var rect5 = new Rectangle(24 * unite_largeur + 0, 17 * unite_hauteur + 8, 5 * unite_largeur + 0, 2 * unite_hauteur - 1);
            //var rect6=new Rectangle( 29 * unite_largeur, 17 * unite_hauteur + 8, 5 * unite_largeur + 0, 2 * unite_hauteur - 1);
           

            var rect1 = new Rectangle(0 * unite_largeur + 0, 17 * unite_hauteur + 8, 1 * unite_largeur + 10, 2 * unite_hauteur - 1);
            var rect2 = new Rectangle(1 * unite_largeur + 10, 17 * unite_hauteur + 8, 10 * unite_largeur - 10, 2 * unite_hauteur - 1);
            var rect3 = new Rectangle(11 * unite_largeur + 0, 17 * unite_hauteur + 8, 4 * unite_largeur + 0, 2 * unite_hauteur - 1);
            var rect4 = new Rectangle(15 * unite_largeur + 0, 17 * unite_hauteur + 8, 4 * unite_largeur + 10, 2 * unite_hauteur - 1);
            var rect5 = new Rectangle(19 * unite_largeur + 10, 17 * unite_hauteur + 8, 4 * unite_largeur - 10, 2 * unite_hauteur - 1);
            var rect6 = new Rectangle(23 * unite_largeur, 17 * unite_hauteur + 8, 4 * unite_largeur + 0, 2 * unite_hauteur - 1);
            var rect7 = new Rectangle(27 * unite_largeur + 0, 17 * unite_hauteur + 8, 4 * unite_largeur + 0, 2 * unite_hauteur - 1);
            var rect8 = new Rectangle(31 * unite_largeur, 17 * unite_hauteur + 8, 4 * unite_largeur + 0, 2 * unite_hauteur - 1);
            graphic.DrawRectangle(Pens.Black, rect1);
            graphic.DrawRectangle(Pens.Black, rect2);
            graphic.DrawRectangle(Pens.Black, rect3);
            graphic.DrawRectangle(Pens.Black, rect4);
            graphic.DrawRectangle(Pens.Black, rect5);
            graphic.DrawRectangle(Pens.Black, rect6);
            graphic.DrawRectangle(Pens.Black, rect7);
            graphic.DrawRectangle(Pens.Black, rect8);

            graphic.DrawString("N°", fnt0, Brushes.Black, rect1, drawFormatCenter);
            graphic.DrawString("Nom & prenom".ToUpper(), fnt0, Brushes.Black, rect2, drawFormatCenter);
            graphic.DrawString("PRIMES DE MOTIVATION".ToUpper(), fnt0, Brushes.Black, rect3, drawFormatCenter);
            graphic.DrawString("FRAIS DE COMMUNICATION ".ToUpper(), fnt0, Brushes.Black, rect4, drawFormatCenter);
            graphic.DrawString("INDEMNITES".ToUpper(), fnt0, Brushes.Black, rect5, drawFormatCenter);
            graphic.DrawString("PRIMES DE TRANSPORT ".ToUpper(), fnt0, Brushes.Black, rect6, drawFormatCenter);
            graphic.DrawString("AUTRES PRIMES".ToUpper(), fnt0, Brushes.Black, rect7, drawFormatCenter);
            graphic.DrawString("TOTAL PRIMES", fnt0, Brushes.Black, rect8, drawFormatCenter);

            var j = 0;
            var count = 1;
            for (var i = 0; i < listePaiement.Count; i++)
            {
                #region MyRegion

                var YLOC = unite_hauteur * 19 + 7 + unite_hauteur * j;
                rect1 = new Rectangle(0 * unite_largeur + 0, YLOC, 1 * unite_largeur + 10, unite_hauteur);
                rect2 = new Rectangle(1 * unite_largeur + 10, YLOC, 10 * unite_largeur - 10, unite_hauteur);
                rect3 = new Rectangle(11 * unite_largeur + 0, YLOC, 4 * unite_largeur + 0, unite_hauteur);
                rect4 = new Rectangle(15 * unite_largeur + 0, YLOC, 4 * unite_largeur + 10, unite_hauteur);
                rect5 = new Rectangle(19 * unite_largeur + 10, YLOC, 4 * unite_largeur - 10, unite_hauteur);
                rect6 = new Rectangle(23 * unite_largeur, YLOC, 4 * unite_largeur + 0, unite_hauteur);
                rect7 = new Rectangle(27 * unite_largeur + 0, YLOC, 4 * unite_largeur + 0, unite_hauteur);
                rect8 = new Rectangle(31 * unite_largeur, YLOC, 4 * unite_largeur + 0, unite_hauteur);
                graphic.DrawRectangle(Pens.Black, rect1);
                graphic.DrawRectangle(Pens.Black, rect2);
                graphic.DrawRectangle(Pens.Black, rect3);
                graphic.DrawRectangle(Pens.Black, rect4);
                graphic.DrawRectangle(Pens.Black, rect5);
                graphic.DrawRectangle(Pens.Black, rect6);
                graphic.DrawRectangle(Pens.Black, rect7);
                graphic.DrawRectangle(Pens.Black, rect8);
                if (listePaiement[i].Employe == "TOTAL")
                {
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].Employe), fnt0, Brushes.Black, 10, YLOC);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].PrimeMotivation), fnt0, Brushes.Black, rect3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].FraisCommunication), fnt0, Brushes.Black, rect4, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].Indemnites), fnt0, Brushes.Black, rect5, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].Transport), fnt0, Brushes.Black, rect6, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].AutresPrimes), fnt0, Brushes.Black, rect7, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].SalaireNet), fnt0, Brushes.Black, rect8, drawFormatRight);
                }
                else
                {
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", "0" + count.ToString()), fnt1, Brushes.Black, rect1, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].Employe), fnt1, Brushes.Black, 1 * unite_largeur + 20, YLOC);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].PrimeMotivation), fnt1, Brushes.Black, rect3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].FraisCommunication), fnt1, Brushes.Black, rect4, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].Indemnites), fnt1, Brushes.Black, rect5, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].Transport), fnt1, Brushes.Black, rect6, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].AutresPrimes), fnt1, Brushes.Black, rect7, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].SalaireNet), fnt1, Brushes.Black, rect8, drawFormatRight);
                    count++;
                }
                j++;
                #endregion
            }

            graphic.FillRectangle(Brushes.White, 0, 34 * unite_hauteur + 9, unite_largeur * 36, unite_hauteur * 6);
            if (listePaiement.Count <= 6)
            {
                var index = unite_hauteur * 20 + j * unite_hauteur + 10;
                var paiement = ConnectionClass.ListeoOrdrePaiement(numeroPaiement);
                var montantTotal = .0;
                foreach (var paie in ConnectionClass.ListeDetailsPaiement(numeroPaiement))
                    montantTotal += paie.SalaireNet + paie.IRPP + paie.ONASA;
                var justification = JustifyText.TextJustification.Center;
                var rect = new Rectangle(1 * 12, index - 15, 35 * unite_largeur, 2 * unite_hauteur);
                var text = "Arrêté le présent état de paiement à la somme de : " + Converti((int)montantTotal) + "(NEP+IRPP+FIR =  " + String.Format(elGR, "{0:0,0}", montantTotal) + ") Franc CFA";
                JustifyText.DrawParagraph(graphic, rect, fnt00, Brushes.Black, text, justification, 1.2f, 1.2f, 1.2f);
                var ecart = hauteur_facture - (index + 2 * unite_hauteur + 10);
                if (ecart > 8 * unite_hauteur)
                {
                    graphic.DrawString("L'Ordonnateur des Dépenses\nLe Directeur Général de l'INSEED ", fnt11, Brushes.Black, 1 * unite_largeur + 10, index + 2 * unite_hauteur + 5);
                    graphic.DrawString("Le Contrôleur Financier Adjoint ", fnt11, Brushes.Black, 17 * unite_largeur + 10, index + 9 * unite_hauteur - 5, drawFormatCenter);
                    graphic.DrawString("Le liquidateur\nL' Agent Comptable de l'INSEED", fnt11, Brushes.Black, 34 * unite_largeur + 20, index + 2 * unite_hauteur + 5, drawFormatRight);

                    graphic.DrawString(paiement.Directeur, fnt3, Brushes.Black, 1 * unite_largeur + 10, index + 8 * unite_hauteur - 5);
                    graphic.DrawString(paiement.Liquidateur, fnt3, Brushes.Black, 34 * unite_largeur + 20, index + 8 * unite_hauteur - 5, drawFormatRight);
                }
                else
                {
                }
            }
            return bitmap;
        }

        public static Bitmap ImprimerOrdreDePaiementPrimes(int numeroPaiement
       , List<Paiement> listePaiement, int exercice, string mois, int start)
        {
            #region facture1
            //les dimension de la facture
            int unite_hauteur = 23;
            int unite_largeur = 32;
            int largeur_facture = 35 * unite_largeur + 10;
            int hauteur_facture = 32 * unite_hauteur + 16;//+ 15 + dtGrid.Rows.Count * unite_hauteur;
            if (listePaiement.Count <= (1 + start) * 27 + 15)
            { hauteur_facture = 33 * unite_hauteur + 16; }

            //creer un bit map
            var bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);

            Font fnt1 = new Font("Century Gothic", 10.8f, FontStyle.Regular);
            Font fnt0 = new Font("Century Gothic", 10.8F, FontStyle.Bold);
            Font fnt00 = new Font("Century Gothic", 12, FontStyle.Regular);
            Font fnt11 = new Font("Century Gothic", 12, FontStyle.Bold);
            Font fnt3 = new Font("Arial Narrow", 15, FontStyle.Bold | FontStyle.Underline);

            #endregion


            var drawFormatCenter = new StringFormat();
            drawFormatCenter.Alignment = StringAlignment.Center;
            var drawFormatLeft = new StringFormat();
            drawFormatLeft.Alignment = StringAlignment.Near;
            var drawFormatRight = new StringFormat();
            drawFormatRight.Alignment = StringAlignment.Far;

            var rect1 = new Rectangle(0 * unite_largeur + 0, 1 * unite_hauteur + 8, 1 * unite_largeur + 10, 2 * unite_hauteur - 1);
            var rect2 = new Rectangle(1 * unite_largeur + 10, 1 * unite_hauteur + 8, 10 * unite_largeur -10, 2 * unite_hauteur - 1);
            var rect3 = new Rectangle(11 * unite_largeur + 0, 1 * unite_hauteur + 8, 4 * unite_largeur + 0, 2 * unite_hauteur - 1);
            var rect4 = new Rectangle(15 * unite_largeur + 0, 1 * unite_hauteur + 8, 4 * unite_largeur + 10, 2 * unite_hauteur - 1);
            var rect5 = new Rectangle(19 * unite_largeur + 10, 1 * unite_hauteur + 8, 4 * unite_largeur - 10, 2 * unite_hauteur - 1);
            var rect6 = new Rectangle(23 * unite_largeur, 1 * unite_hauteur + 8, 4 * unite_largeur + 0, 2 * unite_hauteur - 1);
            var rect7 = new Rectangle(27 * unite_largeur + 0, 1 * unite_hauteur + 8, 4 * unite_largeur + 0, 2 * unite_hauteur - 1);
            var rect8 = new Rectangle(31 * unite_largeur, 1 * unite_hauteur + 8, 4 * unite_largeur + 0, 2 * unite_hauteur - 1);
            graphic.DrawRectangle(Pens.Black, rect1);
            graphic.DrawRectangle(Pens.Black, rect2);
            graphic.DrawRectangle(Pens.Black, rect3);
            graphic.DrawRectangle(Pens.Black, rect4);
            graphic.DrawRectangle(Pens.Black, rect5);
            graphic.DrawRectangle(Pens.Black, rect6);
            graphic.DrawRectangle(Pens.Black, rect7);
            graphic.DrawRectangle(Pens.Black, rect8);

            graphic.DrawString("N°", fnt0, Brushes.Black, rect1, drawFormatCenter);
            graphic.DrawString("Nom & prenom".ToUpper(), fnt0, Brushes.Black, rect2, drawFormatCenter);
            graphic.DrawString("PRIMES DE MOTIVATION".ToUpper(), fnt0, Brushes.Black, rect3, drawFormatCenter);
            graphic.DrawString("FRAIS DE COMMUNICATION ".ToUpper(), fnt0, Brushes.Black, rect4, drawFormatCenter);
            graphic.DrawString("INDEMNITES".ToUpper(), fnt0, Brushes.Black, rect5, drawFormatCenter);
            graphic.DrawString("PRIMES DE TRANSPORT ".ToUpper(), fnt0, Brushes.Black, rect6, drawFormatCenter);
            graphic.DrawString("AUTRES PRIMES".ToUpper(), fnt0, Brushes.Black, rect7, drawFormatCenter);
            graphic.DrawString("TOTAL PRIMES", fnt0, Brushes.Black, rect8, drawFormatCenter);
            var j = 0;
            var count = 27 * start + 16;
            for (var i = 27 * start + 15; i < listePaiement.Count; i++)
            {
                #region MyRegion

                var YLOC = unite_hauteur * 3 + 7 + unite_hauteur * j;

                rect1 = new Rectangle(0 * unite_largeur + 0, YLOC, 1 * unite_largeur + 10, unite_hauteur);
                rect2 = new Rectangle(1 * unite_largeur + 10, YLOC, 10 * unite_largeur - 10, unite_hauteur);
                rect3 = new Rectangle(11 * unite_largeur + 0, YLOC, 4 * unite_largeur + 0, unite_hauteur);
                rect4 = new Rectangle(15 * unite_largeur + 0, YLOC, 4 * unite_largeur + 10, unite_hauteur);
                rect5 = new Rectangle(19 * unite_largeur + 10, YLOC, 4 * unite_largeur -10, unite_hauteur);
                rect6 = new Rectangle(23 * unite_largeur, YLOC, 4 * unite_largeur + 0, unite_hauteur);
                rect7 = new Rectangle(27 * unite_largeur + 0, YLOC, 4 * unite_largeur + 0, unite_hauteur);
                rect8 = new Rectangle(31 * unite_largeur, YLOC, 4 * unite_largeur + 0, unite_hauteur);

                graphic.DrawRectangle(Pens.Black, rect1);
                graphic.DrawRectangle(Pens.Black, rect2);
                graphic.DrawRectangle(Pens.Black, rect3);
                graphic.DrawRectangle(Pens.Black, rect4);
                graphic.DrawRectangle(Pens.Black, rect5);
                graphic.DrawRectangle(Pens.Black, rect6);
                graphic.DrawRectangle(Pens.Black, rect7);
                graphic.DrawRectangle(Pens.Black, rect8);
                if (listePaiement[i].Employe == "TOTAL")
                {
                    graphic.FillRectangle(Brushes.White, 0, YLOC, 11 * unite_largeur, unite_hauteur);
                    graphic.DrawRectangle(Pens.Black,0,YLOC,11*unite_largeur,unite_hauteur);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].Employe), fnt0, Brushes.Black, 10, YLOC);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].PrimeMotivation), fnt0, Brushes.Black, rect3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].FraisCommunication), fnt0, Brushes.Black, rect4, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].Indemnites), fnt0, Brushes.Black, rect5, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].Transport), fnt0, Brushes.Black, rect6, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].AutresPrimes), fnt0, Brushes.Black, rect7, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].SalaireNet), fnt0, Brushes.Black, rect8, drawFormatRight);
                }
                else
                {
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", "0"+count.ToString()), fnt1, Brushes.Black, rect1, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].Employe), fnt1, Brushes.Black, 1 * unite_largeur + 20, YLOC);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].PrimeMotivation), fnt1, Brushes.Black, rect3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].FraisCommunication), fnt1, Brushes.Black, rect4, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].Indemnites), fnt1, Brushes.Black, rect5, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].Transport), fnt1, Brushes.Black, rect6, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].AutresPrimes), fnt1, Brushes.Black, rect7, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].SalaireNet), fnt1, Brushes.Black, rect8, drawFormatRight);
                    count++;
                }
                j++;
                #endregion
            }
            graphic.FillRectangle(Brushes.White, 0, 30 * unite_hauteur + 8, unite_largeur * 36, unite_hauteur * 6);
            if (listePaiement.Count <= (1 + start) * 27 + 15)
            {
                var index = unite_hauteur * 4 + j * unite_hauteur + 10;
                var paiement = ConnectionClass.ListeoOrdrePaiement(numeroPaiement);
                var montantTotal = .0;
                foreach (var paie in ConnectionClass.ListeDetailsPaiement(numeroPaiement))
                    { montantTotal += paie.PrimeMotivation + paie.FraisCommunication + paie.AutresPrimes+paie.Indemnites+paie.Transport; }
                var justification = JustifyText.TextJustification.Full;
                var rect = new Rectangle(10, index - 15, 34 * unite_largeur+10, 2 * unite_hauteur);
                var text = "Arrêté le présent état de paiement à la somme de : " + Converti((int)montantTotal) + "(  " + String.Format(elGR, "{0:0,0}", montantTotal) + ") Franc CFA";
                JustifyText.DrawParagraph(graphic, rect, fnt00, Brushes.Black, text, justification, 1.2f, 1.2f, 1.2f);

                var ecart = hauteur_facture - (index + 2 * unite_hauteur + 10);
                if (ecart > 8 * unite_hauteur)
                {
                    graphic.DrawString("L'Ordonnateur des Dépenses\nLe Directeur Général de l'INSEED ", fnt11, Brushes.Black, 1 * unite_largeur + 0, index + 2 * unite_hauteur + 5);
                    graphic.DrawString("Le Contrôleur Financier Adjoint ", fnt11, Brushes.Black, 17 * unite_largeur + 10, index + 8 * unite_hauteur - 5, drawFormatCenter);
                    graphic.DrawString("Le liquidateur\nL' Agent Comptable de l'INSEED", fnt11, Brushes.Black, 34 * unite_largeur + 0, index + 2 * unite_hauteur + 5, drawFormatRight);

                    graphic.DrawString(paiement.Directeur, fnt3, Brushes.Black, 1 * unite_largeur + 10, index + 8 * unite_hauteur - 5);
                    graphic.DrawString(paiement.Liquidateur, fnt3, Brushes.Black, 34 * unite_largeur + 20, index + 8 * unite_hauteur - 5, drawFormatRight);
                }
                else
                {
                    graphic.FillRectangle(Brushes.White, 0, index - 16, unite_largeur * 36, unite_hauteur * 9);
                }
            }
            return bitmap;
        }

        public static Bitmap ImprimerOrdreDePaiementDirectionGenerale(int numeroPaiement
     , List<Paiement> listePaiement, int exercice, string mois)
        {
            #region facture1
            //les dimension de la facture
            int unite_hauteur = 23;
            int unite_largeur = 32;
            int largeur_facture = 35 * unite_largeur + 10;
            int hauteur_facture = 35 * unite_hauteur + 20;//+ 15 + dtGrid.Rows.Count * unite_hauteur;


            //creer un bit map
            var bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            try
            {
                Image logo = global::SGSP.Properties.Resources.etatPaiement_1;
                graphic.DrawImage(logo, -10, 0, 25+largeur_facture, hauteur_facture);
            }
            catch { }
            //definir les polices 
            Font fnt1 = new Font("Century Gothic", 10.8f, FontStyle.Regular);
            Font fnt33 = new Font("Century Gothic", 7.5f, FontStyle.Bold);
            Font fnt0 = new Font("Century Gothic", 10.5F, FontStyle.Bold);
            Font fnt00 = new Font("Century Gothic", 12, FontStyle.Regular);
            Font fnt11 = new Font("Century Gothic", 12, FontStyle.Bold);
            Font fnt3 = new Font("Arial Narrow", 15, FontStyle.Bold | FontStyle.Underline);
            Font fnt111 = new Font("Century Gothic", 9.5F, FontStyle.Bold);
            #endregion

            var drawFormatCenter = new StringFormat();
            drawFormatCenter.Alignment = StringAlignment.Center;
            var drawFormatLeft = new StringFormat();
            drawFormatLeft.Alignment = StringAlignment.Near;
            var drawFormatRight = new StringFormat();
            drawFormatRight.Alignment = StringAlignment.Far;

            graphic.DrawString("Page " + 1, fnt1, Brushes.Black, 33 * unite_largeur, unite_hauteur);
            graphic.DrawString(DateTime.Now.ToShortDateString(), fnt11, Brushes.Black, unite_largeur * 23 - 2, 11 * unite_hauteur - 8);
            graphic.DrawString("Etat pour servir de paiement des salaires du personnel contractuel de l'INSEED pour la période du : " + mois.ToUpper() + " " +
                exercice, fnt3, Brushes.Black, unite_largeur * 17+10, 16 * unite_hauteur - 5,drawFormatCenter);


            graphic.DrawRectangle(Pens.Black, 16, 17 * unite_hauteur + 8, unite_largeur *2, 2*unite_hauteur+10);
            graphic.DrawRectangle(Pens.Black, 2 * unite_largeur + 16, 17 * unite_hauteur + 8, 8 * unite_largeur - 0, 2 * unite_hauteur +10);
            graphic.DrawRectangle(Pens.Black, 10 * unite_largeur + 16, 17 * unite_hauteur + 8, 3 * unite_largeur + 5, 2 * unite_hauteur +10);
            graphic.DrawRectangle(Pens.Black, 13* unite_largeur + 21, 17 * unite_hauteur + 8, 4 * unite_largeur + 32, 1 * unite_hauteur + 10);
            graphic.DrawRectangle(Pens.Black, 13 * unite_largeur + 21, 18 * unite_hauteur + 18, 2 * unite_largeur + 10, 1 * unite_hauteur-0);
            graphic.DrawRectangle(Pens.Black, 16 * unite_largeur-1, 18 * unite_hauteur + 18, 2 * unite_largeur + 22, 1 * unite_hauteur - 0);
            graphic.DrawRectangle(Pens.Black, 18 * unite_largeur + 21, 17 * unite_hauteur + 8, 3 * unite_largeur -5, 2 * unite_hauteur +10);
            graphic.DrawRectangle(Pens.Black, 21 * unite_largeur + 16, 17 * unite_hauteur + 8, 1 * unite_largeur + 16, 2 * unite_hauteur +10);
            graphic.DrawRectangle(Pens.Black, 23 * unite_largeur + 0, 17 * unite_hauteur + 8, 3 * unite_largeur + 0, 2 * unite_hauteur +10);
            graphic.DrawRectangle(Pens.Black, 26 * unite_largeur + 0, 17 * unite_hauteur + 8, 3 * unite_largeur + 0, 2 * unite_hauteur +10);
            graphic.DrawRectangle(Pens.Black, 29 * unite_largeur + 0, 17 * unite_hauteur + 8, 3 * unite_largeur + 0, 2 * unite_hauteur +10);
            graphic.DrawRectangle(Pens.Black, 32 * unite_largeur, 17 * unite_hauteur + 8, 3 * unite_largeur + 0, 2 * unite_hauteur +10);

            var rect2 = new Rectangle(13 * unite_largeur + 21, 17 * unite_hauteur + 8,4 * unite_largeur +29, 1 * unite_hauteur );
            var rect3 = new Rectangle(10 * unite_largeur + 0, 17 * unite_hauteur + 8, 4 * unite_largeur + 5, 1 * unite_hauteur +20);
            //graphic.DrawLine(Pens.Black, 10*unite_largeur ,17 * unite_hauteur + 8,15*unite_largeur ,17 * unite_hauteur + 8);
            graphic.DrawString("MAT.", fnt0, Brushes.Black, 25, 17 * unite_hauteur + 9);
            graphic.DrawString("Nom & prenom".ToUpper(), fnt0, Brushes.Black, 4 * unite_largeur + 18, 17 * unite_hauteur + 9);
            graphic.DrawString("SALAIRE MENS. FORFAIT.".ToUpper(), fnt0, Brushes.Black, rect3,drawFormatCenter);
            graphic.DrawString("PENSION CNRT".ToUpper(), fnt0, Brushes.Black, 14*unite_largeur+23, 17*unite_hauteur+10);
            graphic.DrawString("EMPLOYE(5%)", fnt33, Brushes.Black, 13 * unite_largeur + 23, 18 * unite_hauteur + 19);
            graphic.DrawString("EMPLOYEUR(12%)".ToUpper(), fnt33, Brushes.Black, 16 * unite_largeur -1, 18 * unite_hauteur + 19);
            graphic.DrawString("IRPP".ToUpper(), fnt0, Brushes.Black, 19 * unite_largeur + 20, 17 * unite_hauteur + 9);
            graphic.DrawString("FIR".ToUpper(), fnt0, Brushes.Black, 22 * unite_largeur + 0, 17 * unite_hauteur + 9);
            graphic.DrawString("NET A PAYER".ToUpper(), fnt0, Brushes.Black, 23 * unite_largeur + 3, 17 * unite_hauteur + 9);
            graphic.DrawString("INDEMNITES".ToUpper(), fnt0, Brushes.Black, 26 * unite_largeur + 0, 17 * unite_hauteur + 9);
            graphic.DrawString("COTISATIONS\nCNRT(17%)".ToUpper(), fnt0, Brushes.Black, 29 * unite_largeur + 2, 17 * unite_hauteur + 9);
            graphic.DrawString("Salaire Net".ToUpper(), fnt0, Brushes.Black, 32 * unite_largeur + 5, 17 * unite_hauteur + 9);
            var j = 0;
            var count = 1;
            for (var i = 0; i < listePaiement.Count; i++)
            {
                #region MyRegion

                var YLOC = unite_hauteur * 19 + 18 + unite_hauteur * j;

            
                graphic.DrawRectangle(Pens.Black, 10 * unite_largeur + 16, YLOC, 3 * unite_largeur + 5, unite_hauteur);
                graphic.DrawRectangle(Pens.Black, 13 * unite_largeur + 21, YLOC, 2 * unite_largeur + 10, unite_hauteur);
                graphic.DrawRectangle(Pens.Black, 16 * unite_largeur -1, YLOC, 2 * unite_largeur + 22, unite_hauteur);
                graphic.DrawRectangle(Pens.Black, 18 * unite_largeur + 21, YLOC, 3 * unite_largeur -5, unite_hauteur);
                graphic.DrawRectangle(Pens.Black, 21 * unite_largeur + 16, YLOC, 1 * unite_largeur + 16, unite_hauteur);
                graphic.DrawRectangle(Pens.Black, 23 * unite_largeur + 0, YLOC, 3 * unite_largeur + 0, unite_hauteur);
                graphic.DrawRectangle(Pens.Black, 26 * unite_largeur + 0, YLOC, 3 * unite_largeur + 0, unite_hauteur);
                graphic.DrawRectangle(Pens.Black, 29 * unite_largeur + 0, YLOC, 3 * unite_largeur + 0, unite_hauteur);
                graphic.DrawRectangle(Pens.Black, 32 * unite_largeur, YLOC, 3 * unite_largeur + 0, unite_hauteur);

                if (listePaiement[i].Employe == "TOTAL")
                {
                    graphic.DrawRectangle(Pens.Black, 16, YLOC, unite_largeur * 10, unite_hauteur);
                    graphic.DrawString(listePaiement[i].Employe, fnt0, Brushes.Black, unite_largeur + 0, YLOC + 3);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].SalaireBrut), fnt0, Brushes.Black, 13 * unite_largeur + 11, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].CNRT), fnt0, Brushes.Black, 16 * unite_largeur - 10, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].CNRTEmploye), fnt0, Brushes.Black, 18 * unite_largeur + 6, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].IRPP), fnt0, Brushes.Black, 21 * unite_largeur + 6, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].ONASA), fnt0, Brushes.Black, 23 * unite_largeur - 10, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].SalaireBrut - listePaiement[i].CNRT - listePaiement[i].IRPP - listePaiement[i].ONASA), fnt0, Brushes.Black, 26 * unite_largeur - 10, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].Indemnites), fnt0, Brushes.Black, 29 * unite_largeur - 10, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].CNRTEmploye + listePaiement[0].CNRT), fnt0, Brushes.Black, 32 * unite_largeur - 10, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].SalaireNet), fnt0, Brushes.Black, 35 * unite_largeur - 10, YLOC + 3, drawFormatRight);
                }
                else
                {
                    graphic.DrawRectangle(Pens.Black, 16, YLOC, unite_largeur * 2, unite_hauteur);
                    graphic.DrawRectangle(Pens.Black, 2 * unite_largeur + 16, YLOC, 8 * unite_largeur - 0, unite_hauteur);
                    graphic.DrawString(listePaiement[i].NumeroMatricule.ToUpper(), fnt1, Brushes.Black, 2*unite_largeur + 16, YLOC + 3, drawFormatRight);
                    graphic.DrawString(listePaiement[i].Employe.ToUpper(), fnt1, Brushes.Black,2* unite_largeur + 20, YLOC + 3);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].SalaireBrut), fnt1, Brushes.Black, 13 * unite_largeur + 11, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].CNRT), fnt1, Brushes.Black, 16 * unite_largeur - 10, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].CNRTEmploye), fnt1, Brushes.Black, 18 * unite_largeur +6 , YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].IRPP), fnt1, Brushes.Black, 21 * unite_largeur + 6, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].ONASA), fnt1, Brushes.Black, 23 * unite_largeur -10, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].SalaireBrut - listePaiement[i].CNRT - listePaiement[i].IRPP - listePaiement[i].ONASA), fnt1, Brushes.Black, 26 * unite_largeur - 10, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].Indemnites), fnt1, Brushes.Black, 29 * unite_largeur - 10, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].CNRTEmploye + listePaiement[0].CNRT), fnt1, Brushes.Black, 32 * unite_largeur - 10, YLOC + 3, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", listePaiement[i].SalaireNet), fnt1, Brushes.Black, 35 * unite_largeur - 10, YLOC + 3, drawFormatRight);

                    count++;
                }
                j++;
                #endregion
            }

            graphic.FillRectangle(Brushes.White, 0, 34 * unite_hauteur + 9, unite_largeur * 36, unite_hauteur * 6);
            if (listePaiement.Count >0)
            {
                var index = unite_hauteur * 21 + j * unite_hauteur + 10;

                var montantTotal = listePaiement[0].SalaireNet + listePaiement[0].IRPP + listePaiement[0].ONASA;
                var justification = JustifyText.TextJustification.Center;
                var rect = new Rectangle(1 * 12, index - 15, 35 * unite_largeur, 2 * unite_hauteur);
                var text = "Arrêté le présent état de paiement à la somme de : " + Converti((int)montantTotal) + "(NEP+IRPP+FIR =  " + String.Format(elGR, "{0:0,0}", montantTotal) + ") Franc CFA";
                JustifyText.DrawParagraph(graphic, rect, fnt00, Brushes.Black, text, justification, 1.2f, 1.2f, 1.2f);
                var ecart = hauteur_facture - (index + 2 * unite_hauteur + 10);
                if (ecart > 8 * unite_hauteur)
                {
                    graphic.DrawString("L'Ordonnateur des Dépenses\nLe Directeur Général de l'INSEED ", fnt11, Brushes.Black, 1 * unite_largeur + 10, index + 2 * unite_hauteur + 5);
                    graphic.DrawString("Le Contrôleur Financier Adjoint ", fnt11, Brushes.Black, 17 * unite_largeur + 10, index + 10 * unite_hauteur - 5, drawFormatCenter);
                    graphic.DrawString("Le liquidateur\nL' Agent Comptable de l'INSEED", fnt11, Brushes.Black, 34 * unite_largeur + 20, index + 2 * unite_hauteur + 5, drawFormatRight);
                    
                    graphic.DrawString(ConnectionClass.ListeoOrdrePaiement(numeroPaiement).Directeur, fnt3, Brushes.Black, 1 * unite_largeur + 10, index + 10 * unite_hauteur - 5);
                    graphic.DrawString(ConnectionClass.ListeoOrdrePaiement(numeroPaiement).Liquidateur, fnt3, Brushes.Black, 34 * unite_largeur + 20, index + 10 * unite_hauteur - 5, drawFormatRight);
                }
                else
                {
                }
            }
            return bitmap;
        }

        public static Bitmap ImprimerOrdreDePaiement(int numeroPaiement, string text)
        {
            #region facture1
            //les dimension de la facture
            int unite_hauteur = 23;
            int unite_largeur = 32;
            int largeur_facture = 35 * unite_largeur + 10;
            int hauteur_facture = 32 * unite_hauteur + 16;

            //creer un bit map
            var bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);

            Font fnt1 = new Font("Century Gothic", 10, FontStyle.Regular);
            //Font fnt33 = new Font("Century Gothic", 10, FontStyle.Bold);
            Font fnt0 = new Font("Century Gothic", 10.5F, FontStyle.Bold);
            Font fnt00 = new Font("Century Gothic", 12, FontStyle.Regular);
            Font fnt11 = new Font("Century Gothic", 12, FontStyle.Bold);
            Font fnt3 = new Font("Arial Narrow", 15, FontStyle.Bold | FontStyle.Underline);

            #endregion


            var drawFormatCenter = new StringFormat();
            drawFormatCenter.Alignment = StringAlignment.Center;
            var drawFormatLeft = new StringFormat();
            drawFormatLeft.Alignment = StringAlignment.Near;
            var drawFormatRight = new StringFormat();
            drawFormatRight.Alignment = StringAlignment.Far;

            var index = unite_hauteur;
            var paiement = ConnectionClass.ListeoOrdrePaiement(numeroPaiement);        
            var justification = JustifyText.TextJustification.Center;
            var rect = new Rectangle(1 * 17, index - 15, 34 * unite_largeur, 2 * unite_hauteur);
           JustifyText.DrawParagraph(graphic, rect, fnt00, Brushes.Black, text, justification, 1.2f, 1.2f, 1.2f);


            //graphic.DrawString("Fait à N'Djaména le  " + DateTime.Now.ToShortDateString(), fnt0, Brushes.Black, 17 * unite_largeur + 10, index + unite_hauteur, drawFormatCenter);
            graphic.DrawString("L'Ordonnateur des Dépenses\nLe Directeur Général de l'INSEED ", fnt00, Brushes.Black, 1 * unite_largeur + 10, index + 2 * unite_hauteur + 5);
            graphic.DrawString("Le Contrôleur Financier Adjoint ", fnt00, Brushes.Black, 17 * unite_largeur + 10, index + 9 * unite_hauteur - 5, drawFormatCenter);
            graphic.DrawString("Le liquidateur\nL' Agent Comptable de l'INSEED", fnt00, Brushes.Black, 34 * unite_largeur + 20, index + 2 * unite_hauteur + 5, drawFormatRight);

            graphic.DrawString(paiement.Directeur, fnt3, Brushes.Black, 1 * unite_largeur + 10, index + 9 * unite_hauteur - 5);
            graphic.DrawString(paiement.Liquidateur, fnt3, Brushes.Black, 34 * unite_largeur + 20, index + 9 * unite_hauteur - 5, drawFormatRight);

         
            return bitmap;
        }
        
        #endregion
        static bool flagAjoutPage;
        //imprimer l'ordre de paiement
        public static Bitmap ImprimerOrdreDePaiementPourJournalierEtStagiaire(int numeroPaiement ,DataGridView dgvPaiement, string typeContrat, int exercice, string mois, int start)
        {
            #region facture1
            //les dimension de la facture
            int unite_hauteur = 22;
            int unite_largeur = 32;
            int largeur_facture = 35 * unite_largeur;
            int hauteur_facture = 35 * unite_hauteur + 16;//+ 15 + dtGrid.Rows.Count * unite_hauteur;


            //creer un bit map
            var bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            try
            {
                Image logo = global::SGSP.Properties.Resources.Logo;
                graphic.DrawImage(logo, unite_largeur, 5, 12 * unite_largeur, 4 * unite_hauteur - 5);
            }
            catch { }
            //definir les polices 
            Font fnt1 = new Font("Calibri", 10, FontStyle.Regular);
            Font fnt33 = new Font("Calibri", 10, FontStyle.Regular);
            Font fnt0 = new Font("Calibri", 9, FontStyle.Bold);
            Font fnt00 = new Font("Calibri", 11, FontStyle.Bold);
            Font fnt11 = new Font("Calibri", 10, FontStyle.Bold);
            Font fnt3 = new Font("Arial Unicode MS", 18, FontStyle.Bold | FontStyle.Underline);
            Font fnt22 = new Font("Calibri", 12, FontStyle.Regular);

            #endregion

            graphic.DrawString("Page " + (start + 1).ToString(), fnt1, Brushes.Black, 33 * unite_largeur, unite_hauteur);
            //graphic.FillRectangle(Brushes.SlateGray,10* unite_largeur, 9 * unite_hauteur + 15, unite_largeur * 15, unite_hauteur + 8);
            if (typeContrat == "Stage")
            {
                graphic.DrawString( "  FORFAITS TRANSPORT STAGIAIRE DU MOIS DE " + mois.ToUpper() + " " + exercice, fnt3, Brushes.Black, unite_largeur * 6, 4 * unite_hauteur - 10);
            }
            else
            {
                graphic.DrawString(typeContrat.ToUpper() + "  ET FORFAITS DU MOIS DE " + mois.ToUpper() + " " + exercice, fnt3, Brushes.Black, unite_largeur * 8, 4 * unite_hauteur - 10);
            }
            //graphic.FillRectangle(Brushes.SaddleBrown, 15, 6 * unite_hauteur + 4, unite_largeur * 34 - 18, 2 * unite_hauteur - 4);

         
            var height = (13 + unite_hauteur);
            graphic.FillRectangle(Brushes.Lavender, 17, 6 * unite_hauteur + 0, unite_largeur * 1 - 2, 2 * unite_hauteur);
            graphic.FillRectangle(Brushes.Lavender, unite_largeur + 17, 6 * unite_hauteur + 0, unite_largeur * 8 - 20, 2 * unite_hauteur);
            graphic.FillRectangle(Brushes.Lavender, 9 * unite_largeur + 0, 6 * unite_hauteur + 0, unite_largeur * 3 + 21, 2 * unite_hauteur);
            graphic.FillRectangle(Brushes.Lavender, 12 * unite_largeur + 24, 6 * unite_hauteur + 0, unite_largeur * 4 + 16, 2 * unite_hauteur);
            graphic.FillRectangle(Brushes.Lavender, 16 * unite_largeur + 11, 6 * unite_hauteur + 0, unite_largeur * 2 - 6, 2 * unite_hauteur);
            graphic.FillRectangle(Brushes.Lavender, 18 * unite_largeur + 8, 6 * unite_hauteur + 0, unite_largeur * 1 - 4, 2 * unite_hauteur);
            graphic.FillRectangle(Brushes.Lavender, 19 * unite_largeur + 6, 6 * unite_hauteur + 0, unite_largeur * 2 + 16, 2 * unite_hauteur);
            //graphic.FillRectangle(Brushes.Lavender, 18 * unite_largeur + 6, 6 * unite_hauteur + 0 + 1, unite_largeur * 2 + 16, 2 * unite_hauteur);
            graphic.FillRectangle(Brushes.Lavender, 21 * unite_largeur + 25, 6 * unite_hauteur + 0, unite_largeur * 2 - 2, 2 * unite_hauteur);
            graphic.FillRectangle(Brushes.Lavender, 23 * unite_largeur + 25, 6 * unite_hauteur + 0, unite_largeur * 2 - 2, 2 * unite_hauteur);
            graphic.FillRectangle(Brushes.Lavender, 25 * unite_largeur + 25, 6 * unite_hauteur + 0, unite_largeur * 2 - 2, 2 * unite_hauteur);
            //graphic.FillRectangle(Brushes.Lavender, 26 * unite_largeur + 26, 6 * unite_hauteur + 0 + 1, unite_largeur * 2 + 14, 2 * unite_hauteur);
            graphic.FillRectangle(Brushes.Lavender, 27 * unite_largeur + 26, 6 * unite_hauteur + 0, unite_largeur * 2 + 14, 2 * unite_hauteur);
            graphic.FillRectangle(Brushes.Lavender, 30 * unite_largeur + 11, 6 * unite_hauteur + 0, unite_largeur * 4 - 15, 2 * unite_hauteur);
            graphic.DrawString("N°", fnt00, Brushes.Black, unite_largeur - 10, 6 * unite_hauteur + 2);
            graphic.DrawString("Nom & prenom", fnt00, Brushes.Black, unite_largeur + 20, 6 * unite_hauteur + 2);
            graphic.DrawString("Qualification", fnt00, Brushes.Black, 9* unite_largeur + 10, 6 * unite_hauteur + 2);
            graphic.DrawString("Service", fnt00, Brushes.Black, 12 * unite_largeur + 30, 6 * unite_hauteur + 2);
            graphic.DrawString("Prime\n/Jour", fnt00, Brushes.Black,16* unite_largeur +20, 6 * unite_hauteur + 2);
            graphic.DrawString("Nbr\nJr", fnt00, Brushes.Black, 18*unite_largeur + 11, 6 * unite_hauteur + 2);
            graphic.DrawString("Montant", fnt00, Brushes.Black, 19 * unite_largeur + 15, 6 * unite_hauteur + 2);
            graphic.DrawString("Acompte", fnt00, Brushes.Black, 21 * unite_largeur + 30, 6 * unite_hauteur + 2);
            graphic.DrawString("Frais\nMedic.", fnt00, Brushes.Black, 23 * unite_largeur + 32, 6 * unite_hauteur + 2);
            graphic.DrawString("Total\nDeduct.", fnt00, Brushes.Black, 25 * unite_largeur +30, 6 * unite_hauteur + 2);
            graphic.DrawString("Solde à \npayer", fnt00, Brushes.Black, 28 * unite_largeur + 5, 6 * unite_hauteur + 2);
            graphic.DrawString("Signature", fnt00, Brushes.Black, 30 * unite_largeur + 16, 6 * unite_hauteur + 2);
            //graphic.DrawString("Nbr\nJr", fnt00, Brushes.Black, 16 * unite_largeur + 3, 6 * unite_hauteur + 2);
            var j = 0;
            var totalAcompte = 0.0;
            var totalChargeSoin = .0;
            for (var i = start * 14; i < dgvPaiement.Rows.Count; i++)
            {
                #region MyRegion
              
                var YLOC = unite_hauteur * 8 + height* j;
                                
              
                var employe = dgvPaiement.Rows[i].Cells[1].Value.ToString();
                //if (employe.Length > 21)
                //{
                //    employe = employe.Substring(0, 21);
                //}
                var qualification = dgvPaiement.Rows[i].Cells[2].Value.ToString();
                if (qualification.Length > 13)
                {
                    qualification = qualification.Substring(0, 13) + "\n" + qualification.Substring(13);
                }
                if (!string.IsNullOrWhiteSpace(qualification))
                {
                    qualification = qualification.Substring(0, 1).ToUpper() + qualification.Substring(1).ToLower();
                }
                if (qualification.Length <= 5)
                {
                    qualification = qualification.ToUpper();
                }
                var dtService = ConnectionClass.ListeServicePersonnel(dgvPaiement.Rows[i].Cells[0].Value.ToString());
                var p = ConnectionClass.PaiementParMatricule(numeroPaiement,dgvPaiement.Rows[i].Cells[0].Value.ToString());
                var service = "";
                //if (dtService.Count > 0)
                //{
                //    service = dtService[0].TypeContrat;
                //}
                //if (service.Length > 17)
                //{
                //    service = service.Substring(0, 17)+"...";
                //}
                if (dgvPaiement.Rows[i].Cells[1].Value.ToString().ToUpper().Contains("TOTAL"))
                {
                    //graphic.DrawRectangle(Pens.Black, 17, YLOC, unite_largeur * 1 - 2, height - 3);
                    graphic.DrawRectangle(Pens.Black,  17, YLOC, unite_largeur * 34 - 20, height - 3);
                    var montant = Double.Parse(dgvPaiement.Rows[i].Cells[4].Value.ToString());
                    graphic.DrawString(employe, fnt00, Brushes.Black, unite_largeur + 20, YLOC + 3);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", montant), fnt00, Brushes.Black, 19 * unite_largeur + 10, YLOC + 3);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[14].Value.ToString())), fnt00, Brushes.Black, 21 * unite_largeur + 30, YLOC + 3);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[15].Value.ToString())), fnt00, Brushes.Black, 23 * unite_largeur + 30, YLOC + 3);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[16].Value.ToString())), fnt00, Brushes.Black, 25 * unite_largeur + 30, YLOC + 3);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[17].Value.ToString())), fnt00, Brushes.Black, 28 * unite_largeur + 10, YLOC + 3);
                 
                }
                else
                {
                    graphic.DrawRectangle(Pens.Black, 17, YLOC, unite_largeur * 1 - 2, height - 3);
                    graphic.DrawRectangle(Pens.Black, unite_largeur + 17, YLOC, unite_largeur * 8 - 20, height - 3);
                    graphic.DrawRectangle(Pens.Black, 9 * unite_largeur + 0, YLOC, unite_largeur * 3 + 21, height - 3);
                    graphic.DrawRectangle(Pens.Black, 12 * unite_largeur + 24, YLOC, unite_largeur * 3 + 16, height - 3);
                    graphic.DrawRectangle(Pens.Black, 16 * unite_largeur +11, YLOC, unite_largeur * 2 - 6, height - 3);
                    graphic.DrawRectangle(Pens.Black, 18 * unite_largeur + 8, YLOC, unite_largeur * 1 - 4, height - 3);
                    graphic.FillRectangle(Brushes.Lavender, 19 * unite_largeur + 6, YLOC + 1, unite_largeur * 2 + 16, height - 3);
                    graphic.DrawRectangle(Pens.Black, 19 * unite_largeur +6, YLOC, unite_largeur *2 +16, height - 3);                  
                    graphic.DrawRectangle(Pens.Black, 21 * unite_largeur + 25, YLOC, unite_largeur * 2 - 2, height - 3);
                    graphic.DrawRectangle(Pens.Black, 23 * unite_largeur + 25, YLOC, unite_largeur * 2 - 2, height - 3);
                    graphic.DrawRectangle(Pens.Black, 25 * unite_largeur + 25, YLOC, unite_largeur * 2 - 2, height - 3);
                    graphic.FillRectangle(Brushes.Lavender, 27 * unite_largeur + 26, YLOC + 1, unite_largeur * 2 +14, height - 3);
                    graphic.DrawRectangle(Pens.Black, 27 * unite_largeur + 26, YLOC, unite_largeur * 2+14, height - 3);
                    graphic.DrawRectangle(Pens.Black, 30 * unite_largeur + 11, YLOC, unite_largeur *4 - 15, height - 3);

                    graphic.DrawString((i + 1).ToString(), fnt1, Brushes.Black, 20, YLOC + 3);
                    graphic.DrawString(employe, fnt1, Brushes.Black, unite_largeur + 22, YLOC + 3);
                    graphic.DrawString(service.ToLower(), fnt1, Brushes.Black, 12 * unite_largeur + 30, YLOC + 3);
                    if (string.IsNullOrWhiteSpace(dgvPaiement.Rows[i].Cells[4].Value.ToString()) && string.IsNullOrWhiteSpace(dgvPaiement.Rows[i].Cells[5].Value.ToString()))
                    {

                    }
                    else
                    {
                        var montant = Double.Parse(dgvPaiement.Rows[i].Cells[4].Value.ToString());

                        graphic.DrawString(qualification, fnt1, Brushes.Black, 9*unite_largeur + 5, YLOC + 1);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", montant / 30), fnt1, Brushes.Black, 16 * unite_largeur + 17, YLOC + 3);
                        graphic.DrawString("30", fnt1, Brushes.Black, 18 * unite_largeur + 15, YLOC + 3);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", montant), fnt00, Brushes.Black, 19 * unite_largeur + 15, YLOC + 3);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", p.AvanceSurSalaire + p.AcomptePaye), fnt1, Brushes.Black, 21 * unite_largeur + 30, YLOC + 3);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", p.ChargeSoinFamille), fnt1, Brushes.Black, 23 * unite_largeur + 30, YLOC + 3);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", p.ChargeSoinFamille + p.AvanceSurSalaire), fnt1, Brushes.Black, 25 * unite_largeur + 30, YLOC + 3);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", Double.Parse(dgvPaiement.Rows[i].Cells[17].Value.ToString())), fnt00, Brushes.Black, 28 * unite_largeur + 10, YLOC + 3);
                        totalAcompte += p.AvanceSurSalaire+ p.AcomptePaye;
                        totalChargeSoin += p.ChargeSoinFamille;
                    }
                }
                j++;
            }
                #endregion
            graphic.FillRectangle(Brushes.White,  2,30 * unite_hauteur + 4, unite_largeur * 38 - 5, 8 * unite_hauteur);
            if ( dgvPaiement.Rows.Count<=(1+start) * 14 )
            {
                //height = (13 + unite_hauteur);
                //var YLOC = unite_hauteur * 8 + height * j;
             
                var index = unite_hauteur * 8 + j * 35+10;
                graphic.DrawString("Fait à N'Djaménale  "+DateTime.Now.ToShortDateString(), fnt00, Brushes.Black, 15 * unite_largeur + 10, index +0);
                graphic.DrawString("Le directeur ", fnt00, Brushes.Black, 15 * unite_largeur + 10, index + unite_hauteur+5);
                var dtP = ConnectionClass.ListeDesPersonnelParFonction("DIRECTEUR");
                if (dtP.Rows.Count > 0)
                {
                    var ecart = hauteur_facture -( index + 2 * unite_hauteur + 10);
                    if (ecart<=2*unite_hauteur)
                    {
                        graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt00, Brushes.Black, 15 * unite_largeur + 10, index +3 * unite_hauteur + 10);
                    }
                    else if (ecart >2*unite_hauteur && ecart <=3*unite_hauteur )
                    {
                        graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt00, Brushes.Black, 15 * unite_largeur + 10, index + 4 * unite_hauteur -5);
                    }
                    else if (ecart > 3 * unite_hauteur && ecart <= 4* unite_hauteur)
                    {
                        graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt00, Brushes.Black, 15 * unite_largeur + 10, index + 5 * unite_hauteur - 5);
                    }
                    else if (ecart >4 * unite_hauteur && ecart <= 5 * unite_hauteur)
                    {
                        graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt00, Brushes.Black, 15 * unite_largeur + 10, index + 6 * unite_hauteur - 5);
                    }
                    else if (ecart > 5 * unite_hauteur && ecart <= 6 * unite_hauteur)
                    {
                        graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt00, Brushes.Black, 15 * unite_largeur + 10, index +7 * unite_hauteur - 5);
                    }
                    else if (ecart > 6 * unite_hauteur )
                    {
                        graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt00, Brushes.Black, 15 * unite_largeur + 10, index + 8 * unite_hauteur - 5);
                    } 
                }
            }

            return bitmap;
        }
    public   static  int  ObtenirMois(string  mois)
        {
            switch (mois)
            {
                case "Janvier":
                    return 1;
                case "Fevrier":
                    return 2;
                case "Mars":
                    return 3;
                case "Avril":
                    return 4;
                case "Mai":
                    return 5;
                case "Juin":
                    return 6;
                case "Juillet":
                    return 7;
                case "Aout":
                    return 8;
                case "Septembre":
                    return 9;
                case "Octobre":
                    return 10;
                case "Novembre":
                    return 11;
                case "Decembre":
                    return 12 ;
                default:
                    return 0;
            };
        }
    static   string ObtenirMoisComplet(int mois)
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
    static string ObtenirJour(DateTime date)
    {
        if (date.Day < 10)
            return "0" + date.Day;
        else
            return date.Day.ToString();
    }

     public    static DateTime ObtenirDebutJour(string mois,int exercice)
        {
            return DateTime.Parse("01/" + ObtenirMois(mois) + "/" + exercice);
        }

    public    static DateTime ObtenirFinJour(string mois,int exercice)
        {
            if (mois == "Janvier")
            {
                return DateTime.Parse("31/" +ObtenirMois( mois )+ "/" + exercice);
            }
            else if (mois == "Fevrier")
            {
                if (DateTime.IsLeapYear(exercice))
                    return DateTime.Parse("29/" + ObtenirMois(mois) + "/" + exercice);
                else
                    return DateTime.Parse("28/" + ObtenirMois(mois) + "/" + exercice);
            }
            else if (mois == "Mars")
            {
                return DateTime.Parse("31/" + ObtenirMois(mois) + "/" + exercice);
            }
            else if (mois == "Avril")
            {
                return DateTime.Parse("30/" + ObtenirMois(mois) + "/" + exercice);
            }
            else if (mois == "Mai")
            {
                return DateTime.Parse("31/" + ObtenirMois(mois) + "/" + exercice);
            }
            else if (mois == "Juin")
            {
                return DateTime.Parse("30/" + ObtenirMois(mois) + "/" + exercice);
            }
            else if (mois == "Juillet")
            {
                return DateTime.Parse("31/" + ObtenirMois(mois) + "/" + exercice);
            }
            else if (mois == "Aout")
            {
                return DateTime.Parse("31/" + ObtenirMois(mois) + "/" + exercice);
            }
            else if (mois == "Septembre")
            {
                return DateTime.Parse("30/" + ObtenirMois(mois) + "/" + exercice);
            }
            else if (mois == "Octobre")
            {
                return DateTime.Parse("31/" + ObtenirMois(mois) + "/" + exercice);
            }
            else if (mois == "Novembre")
            {
                return DateTime.Parse("30/" + ObtenirMois(mois) + "/" + exercice);
            }
            else if (mois == "Decembre")
            {
                return DateTime.Parse("31/" + ObtenirMois(mois) + "/" + exercice);
            }
            else
            {
                return DateTime.Now.Date; ;
            }
        }

     static  string ObtenirMoisEnString(int mois)
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

        //imprimer fiche d'emargement
        public static Bitmap ImprimerUnBulletinDePaie(Paiement paiement, Personnel personnel, Service service)
        {
            #region
            int unite_hauteur = 18;
            int unite_largeur = 32;
            int largeur_facture = 24 * unite_largeur + 10;
            int detail_hauteur_facture = 10 * unite_hauteur;
            int hauteur_facture = 62* unite_hauteur+10;

            //creer un bit map
            Bitmap bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            #endregion
          Image logo = global::SGSP.Properties.Resources.Logo;
                graphic.DrawImage(logo, 15, 10, 13 * unite_largeur, 5* unite_hauteur);
            
            Font fnt1 = new Font("Arial Narrow", 9.5f, FontStyle.Regular);
            Font fnt0 = new Font("Arial Unicode MS",11, FontStyle.Regular);
            Font fnt11 = new Font("Arial Narrow", 9F, FontStyle.Bold);
            Font fnt3 = new Font("Arial Narrow", 12, FontStyle.Bold);
            Font fnt2 = new Font("Arial Unicode MS", 12, FontStyle.Underline);
            Font fnt01 = new Font("Arial Narrow", 10, FontStyle.Regular);
            Font fnt110 = new Font("Arial Unicode MS", 12, FontStyle.Bold | FontStyle.Italic);
             //logo = global::SGSP.Properties.Resources.rectangleEntete;

             graphic.DrawString("N° Employeur CNPS : 2005 09 04 0434".ToUpper(), fnt01, Brushes.Black,5 * unite_largeur+10,3 * unite_hauteur+10);
             graphic.DrawString("BP: 453 N'Djaména-Tchad", fnt01, Brushes.Black, 17 * unite_largeur + 10,1 * unite_hauteur);
             graphic.DrawString("Tél : 22 52 31 64        Fax : 22 52 66 13 ", fnt01, Brushes.Black, 17 * unite_largeur + 10, 2* unite_hauteur);
             graphic.DrawString("Site web : www.inseed-td.net", fnt01, Brushes.Black, 17 * unite_largeur + 10, 3 * unite_hauteur+10);
             //var banques = from b in ConnectionClass.ListeDonneesBancaires(personnel.NumeroMatricule)
             //              where b.Compte == paiement.Compte
             //              select b;
            var cle = "";
            var guichet = "";
            var codeBank = "";
            //foreach (var b in banques)
            //{
            //    cle = b.Cle;
            //    guichet = b.CodeGuichet;
            //    codeBank = b.CodeBanque;
            //}

            var drawFormatCenter = new StringFormat();
            drawFormatCenter.Alignment = StringAlignment.Center;
            var drawFormatLeft = new StringFormat();
            drawFormatLeft.Alignment = StringAlignment.Near;
            var drawFormatRight = new StringFormat();
            drawFormatRight.Alignment = StringAlignment.Far;
            #region DONNEES_PRSONNEL
            graphic.FillRectangle(Brushes.WhiteSmoke, 15, 6 * unite_hauteur + 0, 24 * unite_largeur, 1 * unite_hauteur + 0);
            graphic.FillRectangle(Brushes.WhiteSmoke, 15, 12 * unite_hauteur + 0, 24 * unite_largeur, 1 * unite_hauteur + 0);
            graphic.FillRectangle(Brushes.WhiteSmoke, 15, 14 * unite_hauteur + 0, 24 * unite_largeur, 1 * unite_hauteur + 0);
            graphic.FillRectangle(Brushes.WhiteSmoke, 15, 16 * unite_hauteur + 0, 24 * unite_largeur, 1 * unite_hauteur + 0);
            graphic.FillRectangle(Brushes.WhiteSmoke, 15, 19 * unite_hauteur + 0, 24 * unite_largeur, 1 * unite_hauteur + 0);
            graphic.DrawString("Nom : ", fnt11, Brushes.Black, 15, 6 * unite_hauteur);
            graphic.DrawString("Prénom : ", fnt11, Brushes.Black, 12 * unite_largeur, 6 * unite_hauteur);
            graphic.DrawString(personnel.Nom, fnt1, Brushes.Black, 4 * unite_largeur - 15, 6 * unite_hauteur);
            graphic.DrawString(personnel.Prenom, fnt1, Brushes.Black, 16 * unite_largeur - 5, 6 * unite_hauteur);
            graphic.DrawString("Numéro matricule : ", fnt11, Brushes.Black, 15, 7 * unite_hauteur);
            graphic.DrawString("N° Sécurité sociale : ", fnt11, Brushes.Black, 12 * unite_largeur, 7 * unite_hauteur);
            graphic.DrawString(personnel.NumeroMatricule, fnt1, Brushes.Black, 4 * unite_largeur - 15, 7 * unite_hauteur);
            graphic.DrawString(service.NoCNPS, fnt1, Brushes.Black, 16 * unite_largeur - 5, 7 * unite_hauteur);

            graphic.FillRectangle(Brushes.Gainsboro, 15, 10 * unite_hauteur - 5, 24 * unite_largeur, 1 * unite_hauteur + 3);
            graphic.DrawString("BULLETIN DE PAIE  " + paiement.MoisPaiement.ToUpper() + " " + paiement.Exercice, fnt3, Brushes.Black, 9 * unite_largeur - 5, 10 * unite_hauteur - 4);

            graphic.DrawString("Nom : ", fnt11, Brushes.Black, 15, 11 * unite_hauteur);
            graphic.DrawString("Type contrat : ", fnt11, Brushes.Black, 15, 12 * unite_hauteur);
            graphic.DrawString("Catégorie : ", fnt11, Brushes.Black, 15, 13 * unite_hauteur);
            graphic.DrawString("Echelon : ", fnt11, Brushes.Black, 15, 14 * unite_hauteur);
            graphic.DrawString("Date de naissance : ", fnt11, Brushes.Black, 15, 15 * unite_hauteur);
            graphic.DrawString("Période du : ", fnt11, Brushes.Black, 15, 16 * unite_hauteur);
            graphic.DrawString("Situation matrimoniale ", fnt11, Brushes.Black, 15, 17 * unite_hauteur);
            //graphic.DrawString("Echelon : ", fnt11, Brushes.Black, 15, 18 * unite_hauteur);
            graphic.DrawString("Adresse de l'employé : ", fnt11, Brushes.Black, 15, 19 * unite_hauteur);

            graphic.DrawString(personnel.Nom, fnt1, Brushes.Black, 4 * unite_largeur + 15, 11 * unite_hauteur);
            graphic.DrawString(service.TypeContrat, fnt1, Brushes.Black, 4 * unite_largeur + 15, 12 * unite_hauteur);
            graphic.DrawString(service.Categorie, fnt1, Brushes.Black, 4 * unite_largeur + 15, 13 * unite_hauteur);
            graphic.DrawString(service.Echelon, fnt1, Brushes.Black, 4 * unite_largeur + 15, 14 * unite_hauteur);
            //graphic.DrawString(ObtenirDebutJour(paiement.MoisPaiement, paiement.Exercice).ToShortDateString(), fnt1, Brushes.Black, 4 * unite_largeur + 15, 14 * unite_hauteur);
            graphic.DrawString(personnel.DateNaissance.ToShortDateString(), fnt1, Brushes.Black, 4 * unite_largeur + 15, 15 * unite_hauteur);
            graphic.DrawString(ObtenirDebutJour(paiement.MoisPaiement, paiement.Exercice).ToShortDateString(), fnt1, Brushes.Black, 4 * unite_largeur + 15, 16 * unite_hauteur);
            //graphic.DrawString(service.Contrat, fnt1, Brushes.Black, 4 * unite_largeur + 15, 17 * unite_hauteur);
            //graphic.DrawString(service.Echelon, fnt1, Brushes.Black, 4 * unite_largeur + 15, 18 * unite_hauteur);
            graphic.DrawString(personnel.Adresse, fnt1, Brushes.Black, 4 * unite_largeur + 15, 19 * unite_hauteur);


            graphic.DrawString("Prénom : ", fnt11, Brushes.Black, 9 * unite_largeur, 11 * unite_hauteur);
            graphic.DrawString("Emploi : ", fnt11, Brushes.Black, 9 * unite_largeur, 12 * unite_hauteur);
            graphic.DrawString("Banque : ", fnt11, Brushes.Black, 9 * unite_largeur, 13 * unite_hauteur);
            graphic.DrawString("Guichet : ", fnt11, Brushes.Black, 9 * unite_largeur, 14 * unite_hauteur);
            graphic.DrawString("Date d'embauche : ", fnt11, Brushes.Black, 9 * unite_largeur, 15 * unite_hauteur);
            graphic.DrawString("Au : ", fnt11, Brushes.Black, 9 * unite_largeur, 16 * unite_hauteur);
            graphic.DrawString("Nombre d'enfant\nen charge : ", fnt11, Brushes.Black, 9 * unite_largeur, 17 * unite_hauteur);
            if (service.Poste.Length > 20)
                service.Poste = service.Poste.Substring(0, 20);
            graphic.DrawString(personnel.Prenom, fnt1, Brushes.Black, 13 * unite_largeur, 11 * unite_hauteur);
            graphic.DrawString(service.Poste, fnt1, Brushes.Black, 13 * unite_largeur, 12 * unite_hauteur);
            graphic.DrawString(paiement.Banque, fnt1, Brushes.Black, 13 * unite_largeur, 13 * unite_hauteur);
            graphic.DrawString(guichet, fnt1, Brushes.Black, 13 * unite_largeur, 14 * unite_hauteur);
            graphic.DrawString(service.DateService.ToShortDateString(), fnt1, Brushes.Black, 13 * unite_largeur, 15 * unite_hauteur);
            graphic.DrawString(ObtenirFinJour(paiement.MoisPaiement, paiement.Exercice).ToShortDateString(), fnt1, Brushes.Black, 13 * unite_largeur, 16 * unite_hauteur);
            graphic.DrawString("".ToString(), fnt1, Brushes.Black, 13 * unite_largeur, 17 * unite_hauteur);

            graphic.DrawString("Grade : ", fnt11, Brushes.Black, 17 * unite_largeur, 11 * unite_hauteur);
            graphic.DrawString("Numéro matricule : ", fnt11, Brushes.Black, 17 * unite_largeur, 12 * unite_hauteur);
            graphic.DrawString("N° compte bancaire : ", fnt11, Brushes.Black, 17 * unite_largeur, 13 * unite_hauteur);
            graphic.DrawString("Code banque : ", fnt11, Brushes.Black, 17 * unite_largeur, 14 * unite_hauteur);
            graphic.DrawString("Date depart : ", fnt11, Brushes.Black, 17 * unite_largeur, 15 * unite_hauteur);
            graphic.DrawString("Date paiement : ", fnt11, Brushes.Black, 17 * unite_largeur, 16 * unite_hauteur);
            graphic.DrawString("Clé", fnt11, Brushes.Black, 17 * unite_largeur, 17 * unite_hauteur);

            graphic.DrawString(service.Grade, fnt1, Brushes.Black, 21 * unite_largeur-15, 11 * unite_hauteur);
            graphic.DrawString(personnel.NumeroMatricule, fnt1, Brushes.Black, 21 * unite_largeur - 15, 12 * unite_hauteur);
            graphic.DrawString(paiement.Compte, fnt1, Brushes.Black, 21 * unite_largeur - 15, 13 * unite_hauteur);
            graphic.DrawString(codeBank, fnt1, Brushes.Black, 21 * unite_largeur - 15, 14 * unite_hauteur);
            graphic.DrawString(service.DateDepart.ToShortDateString(), fnt1, Brushes.Black, 21 * unite_largeur - 15, 15 * unite_hauteur);
            graphic.DrawString(paiement.DatePaiement.ToShortDateString(), fnt1, Brushes.Black, 21 * unite_largeur - 15, 16 * unite_hauteur);
            graphic.DrawString(cle, fnt1, Brushes.Black, 21 * unite_largeur - 15, 17 * unite_hauteur);
            #endregion

            #region GAINS
            graphic.FillRectangle(Brushes.Gainsboro, 16, 20 * unite_hauteur +10, 24 * unite_largeur-2, 1 * unite_hauteur +1);
            graphic.FillRectangle(Brushes.Gainsboro, 12*unite_largeur+15, 21 * unite_hauteur + 10, 12 * unite_largeur - 6, 7 * unite_hauteur + 1);
            graphic.FillRectangle(Brushes.WhiteSmoke,  15, 27 * unite_hauteur + 10, 12 * unite_largeur - 1, 1 * unite_hauteur + 1);


            graphic.DrawRectangle(Pens.Black, 15, 20 * unite_hauteur + 10, 6 * unite_largeur, unite_hauteur + 0);
            graphic.DrawRectangle(Pens.Black, 6 * unite_largeur + 15, 20 * unite_hauteur + 10, 3 * unite_largeur, unite_hauteur + 0);
            graphic.DrawRectangle(Pens.Black, 9 * unite_largeur+15 , 20 * unite_hauteur + 10, 3 * unite_largeur , unite_hauteur + 0);
            graphic.DrawRectangle(Pens.Black, 12 * unite_largeur +15, 20 * unite_hauteur + 10, 3 * unite_largeur, unite_hauteur + 0);
            graphic.DrawRectangle(Pens.Black, 15 * unite_largeur + 15, 20 * unite_hauteur + 10, 4 * unite_largeur+8, unite_hauteur + 0);
            graphic.DrawRectangle(Pens.Black, 19 * unite_largeur + 23, 20 * unite_hauteur + 10, 4 * unite_largeur + 18, unite_hauteur + 0);
            
            graphic.DrawString("GAINS", fnt11, Brushes.Black, 17, 20 * unite_hauteur+10);
            graphic.DrawString("Base ", fnt11, Brushes.Black, 9 * unite_largeur + 10, 20 * unite_hauteur + 10, drawFormatRight);
            graphic.DrawString("Taux", fnt11, Brushes.Black, 12 * unite_largeur + 10, 20 * unite_hauteur + 10, drawFormatRight);
            graphic.DrawString("Montant", fnt11, Brushes.Black, 15 * unite_largeur + 10, 20 * unite_hauteur + 10, drawFormatRight);
            graphic.DrawString(" ", fnt11, Brushes.Black, 17 * unite_largeur, 20 * unite_hauteur+10);

            graphic.DrawRectangle(Pens.Black, 15, 21 * unite_hauteur + 10, 6 * unite_largeur, 7 * unite_hauteur + 0);
            graphic.DrawRectangle(Pens.Black, 6 * unite_largeur + 15, 21 * unite_hauteur + 10, 3 * unite_largeur, 7 * unite_hauteur + 0);
            graphic.DrawRectangle(Pens.Black, 9 * unite_largeur + 15, 21 * unite_hauteur + 10, 3 * unite_largeur, 7 * unite_hauteur + 0);
            graphic.DrawRectangle(Pens.Black, 12 * unite_largeur + 15, 21 * unite_hauteur + 10, 3 * unite_largeur, 7 * unite_hauteur + 0);
            graphic.DrawRectangle(Pens.Black, 15 * unite_largeur + 15, 21 * unite_hauteur + 10, 4 * unite_largeur + 8, 7 * unite_hauteur + 0);
            graphic.DrawRectangle(Pens.Black, 19 * unite_largeur + 23, 21 * unite_hauteur + 10, 4 * unite_largeur + 18, 7 * unite_hauteur + 0);
            graphic.DrawRectangle(Pens.Black, 15, 21 * unite_hauteur + 10, 24 * unite_largeur - 2, 7 * unite_hauteur + 0);

            graphic.DrawString("Salaire De Base ", fnt1, Brushes.Black, 17, 21 * unite_hauteur+10);
            graphic.DrawString("Heures Supplementaire ", fnt1, Brushes.Black, 17, 22 * unite_hauteur+10);
            graphic.DrawString("Indemnités de transport", fnt1, Brushes.Black, 17, 23 * unite_hauteur+10);
            graphic.DrawString("Congés payé", fnt1, Brushes.Black, 17, 24 * unite_hauteur + 10);
            graphic.DrawString("Coûts d'absence", fnt1, Brushes.Black, 17, 25 * unite_hauteur + 10);
            graphic.DrawString("Salaire Brut ".ToUpper(), fnt11, Brushes.Black, 17, 27 * unite_hauteur+10);

            graphic.DrawString(String.Format(elGR, "{0:0,0}", paiement.SalaireBase), fnt1, Brushes.Black, 15 * unite_largeur + 10, 21 * unite_hauteur + 13, drawFormatRight);
          graphic.DrawString(String.Format(elGR, "{0:0,0}","00"), fnt1, Brushes.Black, 15 * unite_largeur + 10, 22 * unite_hauteur + 13, drawFormatRight);
            graphic.DrawString(String.Format(elGR, "{0:0,0}", paiement.Transport), fnt1, Brushes.Black, 15 * unite_largeur + 10, 23 * unite_hauteur + 13, drawFormatRight);
            graphic.DrawString(String.Format(elGR, "{0:0,0}", paiement.CongeAnnuel), fnt1, Brushes.Black, 15 * unite_largeur + 10, 24 * unite_hauteur + 13, drawFormatRight);
            graphic.DrawString(String.Format(elGR, "{0:0,0}", paiement.CoutAbsence), fnt1, Brushes.Black, 15 * unite_largeur + 10, 25 * unite_hauteur + 13, drawFormatRight);
            graphic.DrawString(String.Format(elGR, "{0:0,0}", paiement.SalaireBrut), fnt11, Brushes.Black, 15 * unite_largeur + 10, 27 * unite_hauteur + 11,drawFormatRight);

            #endregion


            #region RETENUES
            graphic.FillRectangle(Brushes.Gainsboro, 15, 29 * unite_hauteur + 13, 24 * unite_largeur - 7, 1 * unite_hauteur + 1);
            graphic.FillRectangle(Brushes.Gainsboro, 9 * unite_largeur + 15, 28 * unite_hauteur + 13, 14 * unite_largeur +234, 1 * unite_hauteur + 1);
            graphic.FillRectangle(Brushes.Gainsboro, 12 * unite_largeur + 15, 29 * unite_hauteur + 13, 12 * unite_largeur - 6, 10 * unite_hauteur + 1);
            graphic.FillRectangle(Brushes.WhiteSmoke, 15, 38 * unite_hauteur + 13, 12 * unite_largeur - 1, 1 * unite_hauteur + 1);

            graphic.DrawRectangle(Pens.Black, 15, 29 * unite_hauteur + 13, 24 * unite_largeur - 5, 1 * unite_hauteur + 0);
            graphic.DrawRectangle(Pens.Black, 15, 28 * unite_hauteur + 13, 24 * unite_largeur -6, 1 * unite_hauteur + 0);
            graphic.DrawRectangle(Pens.Black, 15, 29 * unite_hauteur + 13, 6 * unite_largeur, 10 * unite_hauteur + 0);
            graphic.DrawRectangle(Pens.Black, 6 * unite_largeur + 15, 29 * unite_hauteur + 13, 3 * unite_largeur, 10 * unite_hauteur + 0);
            graphic.DrawRectangle(Pens.Black, 9 * unite_largeur + 15, 28 * unite_hauteur + 13, 6 * unite_largeur, 11 * unite_hauteur + 0);
            graphic.DrawRectangle(Pens.Black, 12 * unite_largeur + 15, 29 * unite_hauteur + 13, 3 * unite_largeur, 10 * unite_hauteur + 0);
            graphic.DrawRectangle(Pens.Black, 15 * unite_largeur + 15, 29 * unite_hauteur + 13, 4 * unite_largeur + 8, 10 * unite_hauteur + 0);
            graphic.DrawRectangle(Pens.Black, 19 * unite_largeur + 23, 29 * unite_hauteur + 13, 4 * unite_largeur + 18, 10 * unite_hauteur + 0);
            graphic.DrawRectangle(Pens.Black, 15, 38 * unite_hauteur + 13, 24 * unite_largeur - 5, 1 * unite_hauteur + 0);
            graphic.DrawRectangle(Pens.Black, 15, 39 * unite_hauteur + 13, 24 * unite_largeur - 6, 1 * unite_hauteur + 3);

            graphic.DrawString("Part de l' employé", fnt11, Brushes.Black, 11*unite_largeur, 28 * unite_hauteur + 13);
            graphic.DrawString("Part de l' employeur ", fnt11, Brushes.Black, 16 * unite_largeur + 20, 28 * unite_hauteur + 13);
            graphic.DrawString("RETENUES", fnt11, Brushes.Black, 17, 29 * unite_hauteur + 13);
            graphic.DrawString("Base ", fnt11, Brushes.Black, 9 * unite_largeur + 10, 29 * unite_hauteur + 13,drawFormatRight);
            graphic.DrawString("Taux", fnt11, Brushes.Black, 12 * unite_largeur + 10, 29 * unite_hauteur + 13, drawFormatRight);
            graphic.DrawString("Montant", fnt11, Brushes.Black, 15 * unite_largeur + 10, 29 * unite_hauteur + 13, drawFormatRight);
            graphic.DrawString("Taux ", fnt11, Brushes.Black, 19 * unite_largeur + 15, 29 * unite_hauteur + 13, drawFormatRight);
            graphic.DrawString("Montant ", fnt11, Brushes.Black, 24 * unite_largeur -0, 29 * unite_hauteur + 13, drawFormatRight);

            graphic.DrawString("Avance salaire", fnt1, Brushes.Black, 17, 30 * unite_hauteur + 13);
            graphic.DrawString("Acompte ", fnt1, Brushes.Black, 17, 31 * unite_hauteur + 13);
            graphic.DrawString("Charge santé ", fnt1, Brushes.Black, 17, 32* unite_hauteur + 13);
            graphic.DrawString("CNPS", fnt1, Brushes.Black, 17, 33 * unite_hauteur + 13);
            graphic.DrawString("IRPP".ToUpper(), fnt1, Brushes.Black, 17, 34* unite_hauteur + 13);
            graphic.DrawString("FIR", fnt1, Brushes.Black, 17, 35* unite_hauteur + 13);
            graphic.DrawString("Coûts d'absence", fnt1, Brushes.Black, 17, 37 * unite_hauteur + 13);
            graphic.DrawString("Total", fnt11, Brushes.Black, 17, 38 * unite_hauteur + 13);
            graphic.DrawString("TOTAL RETENUES", fnt11, Brushes.Black, 17, 39 * unite_hauteur + 16);

                  graphic.DrawString(String.Format(elGR, "{0:0,0}", paiement.AvanceSurSalaire), fnt1, Brushes.Black, 15 * unite_largeur + 10, 30 * unite_hauteur + 13, drawFormatRight);
                graphic.DrawString(String.Format(elGR, "{0:0,0}", paiement.AcomptePaye), fnt1, Brushes.Black, 15 * unite_largeur + 10, 31 * unite_hauteur + 13, drawFormatRight);
                graphic.DrawString(String.Format(elGR, "{0:0,0}", paiement.ChargeSoinFamille), fnt1, Brushes.Black, 15 * unite_largeur + 10, 32 * unite_hauteur + 13, drawFormatRight);
            graphic.DrawString(String.Format(elGR, "{0:0,0}", paiement.CNPS), fnt1, Brushes.Black, 15 * unite_largeur + 10, 33 * unite_hauteur + 13, drawFormatRight);
            graphic.DrawString(String.Format(elGR, "{0:0,0}", paiement.IRPP), fnt1, Brushes.Black, 15 * unite_largeur + 10, 34 * unite_hauteur + 13, drawFormatRight);
            graphic.DrawString(String.Format(elGR, "{0:0,0}", paiement.ONASA), fnt1, Brushes.Black, 15 * unite_largeur + 10, 35 * unite_hauteur + 13, drawFormatRight);
           graphic.DrawString(String.Format(elGR, "{0:0,0}", paiement.CoutAbsence), fnt1, Brushes.Black, 15 * unite_largeur + 10, 37 * unite_hauteur + 13, drawFormatRight);
            graphic.DrawString(String.Format(elGR, "{0:0,0}", paiement.CoutAbsence + paiement.AcomptePaye+paiement.AvanceSurSalaire+
                paiement.ChargeSoinFamille + paiement.IRPP + paiement.ONASA + paiement.CNPS), fnt11, Brushes.Black, 15 * unite_largeur + 10, 38 * unite_hauteur + 13, drawFormatRight);
            graphic.DrawString(String.Format(elGR, "{0:0,0}", paiement.CoutAbsence + paiement.AcomptePaye + paiement.AvanceSurSalaire +
    paiement.ChargeSoinFamille + paiement.IRPP + paiement.ONASA + paiement.CNPS+paiement.CNRT), fnt11, Brushes.Black, 15 * unite_largeur + 10, 39 * unite_hauteur + 13,drawFormatRight);

             graphic.DrawString(String.Format(elGR, "{0:0,0}", "NB"), fnt1, Brushes.Black, 12 * unite_largeur + 10, 34 * unite_hauteur + 13, drawFormatRight);
             graphic.DrawString(String.Format(elGR, "{0:0,0}", paiement.ChargePatronale), fnt1, Brushes.Black, 24 * unite_largeur - 0, 33 * unite_hauteur + 13, drawFormatRight);

            if (service.SiCNRT)
            {
                var baseCNRT = Math.Round(paiement.CNRT * 100 / 5);
       
                graphic.DrawString("CNRT", fnt1, Brushes.Black, 17, 36 * unite_hauteur + 13); 
                graphic.DrawString(String.Format(elGR, "{0:0,0}", "0%"), fnt1, Brushes.Black, 12 * unite_largeur + 10, 33 * unite_hauteur + 13, drawFormatRight);
                graphic.DrawString(String.Format(elGR, "{0:0,0}", paiement.CNRT), fnt1, Brushes.Black, 15 * unite_largeur + 10, 36 * unite_hauteur + 13, drawFormatRight);
                graphic.DrawString(String.Format(elGR, "{0:0,0}", "5%"), fnt1, Brushes.Black, 12 * unite_largeur + 10, 36 * unite_hauteur + 13, drawFormatRight);
                graphic.DrawString(String.Format(elGR, "{0:0,0}", "11,5%"), fnt1, Brushes.Black, 19 * unite_largeur+15, 33 * unite_hauteur + 13, drawFormatRight);
                graphic.DrawString(String.Format(elGR, "{0:0,0}", "12%"), fnt1, Brushes.Black, 19 * unite_largeur+15, 36 * unite_hauteur + 13, drawFormatRight);
                graphic.DrawString(String.Format(elGR, "{0:0,0}", paiement.ChargePatronale), fnt1, Brushes.Black, 24 * unite_largeur - 0, 33 * unite_hauteur + 13, drawFormatRight);
                graphic.DrawString(String.Format(elGR, "{0:0,0}", paiement.CNRTEmploye), fnt1, Brushes.Black, 24 * unite_largeur - 0, 36 * unite_hauteur + 13, drawFormatRight);
                //graphic.DrawString(String.Format(elGR, "{0:0,0}", cnrt) + " - " + String.Format(elGR, "{0:0,0}", cnrtEmp), fnt1, Brushes.Black, 24 * unite_largeur + 5, 36 * unite_hauteur + 13, drawFormatRight);
                graphic.DrawString(String.Format(elGR, "{0:0,0}", baseCNRT), fnt1, Brushes.Black, 9 * unite_largeur + 13, 36 * unite_hauteur + 13, drawFormatRight);
        
            }else
            {
                graphic.DrawString(String.Format(elGR, "{0:0,0}", "3,5%"), fnt1, Brushes.Black, 12 * unite_largeur + 10, 33 * unite_hauteur + 13, drawFormatRight);
                graphic.DrawString(String.Format(elGR, "{0:0,0}", "16,5%"), fnt1, Brushes.Black, 19 * unite_largeur+15, 33 * unite_hauteur + 13, drawFormatRight);
            }
            var baseCNPS =  paiement.SalaireBrut;
            if(baseCNPS >500000)
                baseCNPS = 500000.0;
            graphic.DrawString(String.Format(elGR, "{0:0,0}",baseCNPS), fnt1, Brushes.Black, 9 * unite_largeur + 13, 33 * unite_hauteur + 13, drawFormatRight);
            graphic.DrawString(String.Format(elGR, "{0:0,0}", paiement.SalaireBrut - paiement.CNPS), fnt1, Brushes.Black, 9 * unite_largeur + 13, 34 * unite_hauteur + 13, drawFormatRight);

           #endregion

            #region PRIMES
            graphic.FillRectangle(Brushes.Gainsboro, 16, 40 * unite_hauteur + 19, 24 * unite_largeur - 2, 1 * unite_hauteur + 1);
            //graphic.FillRectangle(Brushes.Gainsboro, 9 * unite_largeur + 15, 41 * unite_hauteur + 19, 12 * unite_largeur - 6, 1 * unite_hauteur + 1);
            graphic.FillRectangle(Brushes.Gainsboro, 12 * unite_largeur + 15, 41 * unite_hauteur + 19, 12 * unite_largeur - 6, 7 * unite_hauteur + 1);
            graphic.FillRectangle(Brushes.WhiteSmoke, 15, 47 * unite_hauteur + 19, 12 * unite_largeur - 1, 1 * unite_hauteur + 1);

            graphic.DrawRectangle(Pens.Black, 15, 40 * unite_hauteur + 19, 24 * unite_largeur - 6, 1 * unite_hauteur + 0);
            graphic.DrawRectangle(Pens.Black, 15, 40 * unite_hauteur + 19, 6 * unite_largeur, 8 * unite_hauteur + 0);
            graphic.DrawRectangle(Pens.Black, 6 * unite_largeur + 15, 40 * unite_hauteur + 19, 3 * unite_largeur, 8 * unite_hauteur + 0);
            graphic.DrawRectangle(Pens.Black, 9 * unite_largeur + 15, 40 * unite_hauteur + 19, 3 * unite_largeur, 8 * unite_hauteur + 0);
            graphic.DrawRectangle(Pens.Black, 12 * unite_largeur + 15, 40 * unite_hauteur + 19, 3 * unite_largeur, 8 * unite_hauteur + 0);
            graphic.DrawRectangle(Pens.Black, 15 * unite_largeur + 15, 40 * unite_hauteur + 19, 4 * unite_largeur +8, 8 * unite_hauteur + 0);
            graphic.DrawRectangle(Pens.Black, 19 * unite_largeur +23, 40 * unite_hauteur + 19, 4 * unite_largeur +18, 8 * unite_hauteur + 0);
            graphic.DrawRectangle(Pens.Black, 15, 47* unite_hauteur + 19, 24 * unite_largeur - 5, 1 * unite_hauteur + 0);
            //graphic.DrawRectangle(Pens.Black, 15, 3 * unite_hauteur + 16, 24 * unite_largeur - 6, 1 * unite_hauteur + 0);

            graphic.DrawString("PRIMES ET INDEMNITES", fnt11, Brushes.Black, 17, 40 * unite_hauteur + 19);
            graphic.DrawString("Base ", fnt11, Brushes.Black, 9 * unite_largeur + 10, 40 * unite_hauteur + 19,drawFormatRight);
            graphic.DrawString("Taux", fnt11, Brushes.Black, 12 * unite_largeur + 10, 40 * unite_hauteur + 19, drawFormatRight);
            graphic.DrawString("Montant", fnt11, Brushes.Black, 15 * unite_largeur + 10, 40 * unite_hauteur + 19, drawFormatRight);
            graphic.DrawString(" ", fnt11, Brushes.Black, 17 * unite_largeur, 40 * unite_hauteur + 19);
            //graphic.DrawString("Cumul de l'année", fnt11, Brushes.Black, 21 * unite_largeur + 12, 40 * unite_hauteur + 19);

            graphic.DrawString("Autres primes", fnt1, Brushes.Black, 17, 41 * unite_hauteur + 20);
            graphic.DrawString("Indemnité de Transport ", fnt1, Brushes.Black, 17, 42 * unite_hauteur + 20);
            graphic.DrawString("Indemnités", fnt1, Brushes.Black, 17, 43 * unite_hauteur + 20);
            graphic.DrawString("Prime de Motivation", fnt1, Brushes.Black, 17, 44 * unite_hauteur + 20);
            graphic.DrawString("Frais de communication", fnt1, Brushes.Black, 17, 45 * unite_hauteur + 20);
            //graphic.DrawString("Autres Primes", fnt1, Brushes.Black, 17, 46 * unite_hauteur + 20);
            graphic.DrawString("TOTAL PRIMES", fnt11, Brushes.Black, 17, 47 * unite_hauteur + 20);
            graphic.DrawString(String.Format(elGR, "{0:0,0}", paiement.AutresPrimes), fnt1, Brushes.Black, 15 * unite_largeur + 10, 41 * unite_hauteur + 20, drawFormatRight);
            graphic.DrawString(String.Format(elGR, "{0:0,0}", paiement.Transport), fnt1, Brushes.Black, 15 * unite_largeur + 10, 42 * unite_hauteur + 20, drawFormatRight);
            graphic.DrawString(String.Format(elGR, "{0:0,0}", paiement.Indemnites), fnt1, Brushes.Black, 15 * unite_largeur + 10, 43 * unite_hauteur + 20, drawFormatRight);
            graphic.DrawString(String.Format(elGR, "{0:0,0}", paiement.PrimeMotivation), fnt1, Brushes.Black, 15 * unite_largeur + 10, 44 * unite_hauteur + 20, drawFormatRight);
            graphic.DrawString(String.Format(elGR, "{0:0,0}", paiement.FraisCommunication), fnt1, Brushes.Black, 15 * unite_largeur + 10, 45 * unite_hauteur + 20, drawFormatRight);
            graphic.DrawString("", fnt1, Brushes.Black, 12 * unite_largeur + 20, 46 * unite_hauteur + 20);
            graphic.DrawString(String.Format(elGR, "{0:0,0}", paiement.FraisCommunication+paiement.Indemnites+paiement.Transport+
                paiement.PrimeMotivation + paiement.AutresPrimes), fnt11, Brushes.Black, 15 * unite_largeur + 10, 47 * unite_hauteur + 20, drawFormatRight);

               #endregion
            graphic.FillRectangle(Brushes.Gainsboro, 15, 48 * unite_hauteur + 23, 24 * unite_largeur - 6, 1 * unite_hauteur + 1);
            //graphic.FillRectangle(Brushes.Gainsboro, 15, 49 * unite_hauteur + 28, 24 * unite_largeur - 6, 1 * unite_hauteur + 1);

            graphic.DrawRectangle(Pens.Black, 15, 48 * unite_hauteur + 22, 24 * unite_largeur - 6, 1 * unite_hauteur + 2);
            graphic.DrawRectangle(Pens.Black, 15, 48 * unite_hauteur + 22, 6 * unite_largeur,  unite_hauteur + 2);
            graphic.DrawRectangle(Pens.Black, 6 * unite_largeur + 15, 48 * unite_hauteur + 22, 3 * unite_largeur,  unite_hauteur + 2);
            graphic.DrawRectangle(Pens.Black, 9 * unite_largeur + 15, 48 * unite_hauteur + 22, 3 * unite_largeur,  unite_hauteur + 2);
            graphic.DrawRectangle(Pens.Black, 12 * unite_largeur + 15, 48 * unite_hauteur + 22, 3 * unite_largeur,  unite_hauteur + 2);
            graphic.DrawRectangle(Pens.Black, 15 * unite_largeur + 15, 48 * unite_hauteur + 22, 4 * unite_largeur +8,unite_hauteur + 2);
            graphic.DrawRectangle(Pens.Black, 19 * unite_largeur + 23, 48 * unite_hauteur + 22, 4 * unite_largeur + 18, unite_hauteur + 2);
          
            graphic.DrawString("NET A PAYER ", fnt11, Brushes.Black, 17, 48 * unite_hauteur + 24);
            graphic.DrawString(String.Format(elGR, "{0:0,0}", paiement.SalaireNet), fnt11, Brushes.Black, 15 * unite_largeur +10, 48 * unite_hauteur + 24, drawFormatRight);
            graphic.DrawRectangle(Pens.Black, 15, 49 * unite_hauteur + 28, 24 * unite_largeur - 6, 1 * unite_hauteur + 10);
            //graphic.DrawRectangle(Pens.Black, 15, 49 * unite_hauteur + 28, 6 * unite_largeur, unite_hauteur + 10);
            //graphic.DrawRectangle(Pens.Black, 6 * unite_largeur + 15, 49 * unite_hauteur + 28, 3 * unite_largeur, unite_hauteur + 10);
            //graphic.DrawRectangle(Pens.Black, 9 * unite_largeur + 15, 49* unite_hauteur + 28, 3 * unite_largeur, unite_hauteur + 10);
            //graphic.DrawRectangle(Pens.Black, 12 * unite_largeur + 15, 49 * unite_hauteur + 28, 3 * unite_largeur, unite_hauteur + 10);
            //graphic.DrawRectangle(Pens.Black, 15 * unite_largeur + 15, 49 * unite_hauteur + 28, 6 * unite_largeur - 10, unite_hauteur + 10);
            //graphic.DrawRectangle(Pens.Black, 21 * unite_largeur + 5, 49 * unite_hauteur + 28, 3 * unite_largeur + 4, unite_hauteur +10);
            graphic.DrawRectangle(Pens.Black, 15, 51 * unite_hauteur + 20, 12 * unite_largeur, unite_hauteur * 10);
            graphic.DrawRectangle(Pens.Black, 15, 51 * unite_hauteur + 20, 24 * unite_largeur -6, unite_hauteur * 10);
            graphic.DrawString("L'EMPLOYE", fnt11, Brushes.Black, 17, 51 * unite_hauteur + 22);
            graphic.DrawString("L'EMPLOYEUR", fnt11, Brushes.Black, 12*unite_largeur +18, 51 * unite_hauteur + 22);

    
            return bitmap;
        }

        //imprimer acompte
        public static Bitmap ImprimerListeDesAcomptes(DataGridView dgvAvance, string mois, int exercice)
        {
            #region
            int unite_hauteur = 18;
            int unite_largeur = 32;
            int largeur_facture = 25 * unite_largeur + 10;
            int detail_hauteur_facture = 10 * unite_hauteur;
            int hauteur_facture = 64 * unite_hauteur;

            //creer un bit map
            Bitmap bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            #endregion
            try
            {
                Image logo = global::SGSP.Properties.Resources.Logo;
                graphic.DrawImage(logo, unite_largeur, 10, 12 * unite_largeur, 3* unite_hauteur);
            }
            catch { } //definir les polices 
            Font fnt1 = new Font("Arial Narrow", 11, FontStyle.Regular);
            Font fnt0 = new Font("Arial Narrow", 9, FontStyle.Regular);
            Font fnt11 = new Font("Arial Narrow", 11, FontStyle.Bold);
            Font fnt3 = new Font("Arial Narrow", 12, FontStyle.Bold);
            Font fnt33 = new Font("Arial Narrow", 11, FontStyle.Regular);
            Font fnt2 = new Font("Arial Unicode MS", 12, FontStyle.Underline);
            // dessiner les ecritures 
            if(!string.IsNullOrEmpty(mois))
            {
            graphic.DrawString("Liste des acomptes du mois "+ mois + " " + exercice, fnt3, Brushes.Black, unite_largeur, 3 * unite_hauteur+5);
            }else
            {
                graphic.DrawString("Liste des acomptes", fnt3, Brushes.Black, unite_largeur, 3 * unite_hauteur+5);
            }
            graphic.DrawString("Emis le " + DateTime.Now, fnt0, Brushes.Black,18* unite_largeur, 3* unite_hauteur + 5);

            graphic.FillRectangle(Brushes.SaddleBrown, unite_largeur, 5 * unite_hauteur-2, 23 * unite_largeur + 2, unite_hauteur+5);
            //graphic.DrawRectangle(Pens.White, unite_largeur - 1, 15 * unite_hauteur - 1, 9 * unite_largeur, unite_hauteur + 2);
            graphic.DrawString("N° ", fnt11, Brushes.White, unite_largeur , 5 * unite_hauteur);
            graphic.DrawString("Date ", fnt11, Brushes.White, 2*unite_largeur + 10, 5 * unite_hauteur); 
            graphic.DrawString("Nom du salarié ", fnt11, Brushes.White, unite_largeur * 6, 5 * unite_hauteur);
            graphic.DrawString("Montant ", fnt11, Brushes.White, unite_largeur * 17, 5 * unite_hauteur);
            graphic.DrawString("Signature ", fnt11, Brushes.White, unite_largeur * 20, 5* unite_hauteur);
            var total = 0.0;
            for (int i = 0; i < dgvAvance.Rows.Count; i++)
            {
                var YLOC = 6 * unite_hauteur + 6 + i * 20;
                
                graphic.DrawLine(Pens.Salmon, unite_largeur, YLOC, unite_largeur * 24,  YLOC);
               
                if(i%2==1)
                graphic.FillRectangle(Brushes.PeachPuff, unite_largeur, YLOC, unite_largeur * 23, 20 );
                
                graphic.DrawRectangle(Pens.SaddleBrown, unite_largeur, YLOC, 23 * unite_largeur + 2, 20);
                graphic.DrawString((i+1).ToString(), fnt1, Brushes.Black, unite_largeur + 10, YLOC+3);
                graphic.DrawString(dgvAvance.Rows[i].Cells[3].Value.ToString(), fnt1, Brushes.Black, 2*unite_largeur + 10, YLOC+3);
                graphic.DrawString(dgvAvance.Rows[i].Cells[2].Value.ToString(), fnt1, Brushes.Black, unite_largeur * 6, YLOC+3);
                graphic.DrawString(dgvAvance.Rows[i].Cells[4].Value.ToString() , fnt1, Brushes.Black, unite_largeur * 17, YLOC+3);
                total += Double.Parse(dgvAvance.Rows[i].Cells[4].Value.ToString());
            }
            var LOC = 6 * unite_hauteur + 8 + dgvAvance.Rows.Count* 20;
            graphic.FillRectangle(Brushes.SaddleBrown, unite_largeur, LOC, 23 * unite_largeur + 2, unite_hauteur+5);
            graphic.DrawString("Total", fnt11, Brushes.White, unite_largeur + 10, LOC+2);
            graphic.DrawString(string.Format(elGR, "{0:0,0}", total) + " F.CFA", fnt11, Brushes.White, 17 * unite_largeur-0, LOC+2);
            //graphic.DrawString("Fait à N’Djamena, le  " + DateTime.Now.ToShortDateString(), fnt33, Brushes.Black, 2 * unite_largeur, 51 * unite_hauteur);
            //graphic.DrawString("Le Responsable", fnt2, Brushes.Black, 16 * unite_largeur, 51 * unite_hauteur);
            //graphic.DrawString("Signature du l'employeur ", fnt2, Brushes.SlateGray, 16 * unite_largeur, 38 * unite_hauteur);
            return bitmap;
        }
    
       public static Bitmap ImprimerListeDesFraisConges(DataGridView dgvConge, string mois, int exercice)
       {
           #region
           int unite_hauteur = 20;
           int unite_largeur = 32;
           int largeur_facture = 24 * unite_largeur + 10;
           int detail_hauteur_facture = 10 * unite_hauteur;
           int hauteur_facture = 56 * unite_hauteur;

           //creer un bit map
           Bitmap bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

           // creer un objet graphic
           Graphics graphic = Graphics.FromImage(bitmap);

           //la couleur de l'image
           graphic.Clear(Color.White);
           #endregion
           try
           {
               Image logo = global::SGSP.Properties.Resources.Logo;
               graphic.DrawImage(logo, unite_largeur, 10, 12 * unite_largeur, 4 * unite_hauteur);
           }
           catch { } //definir les polices 
           Font fnt1 = new Font("Arial Unicode MS", 11, FontStyle.Regular);
           Font fnt0 = new Font("Arial Unicode MS", 9, FontStyle.Regular);
           Font fnt11 = new Font("Arial Unicode MS", 11, FontStyle.Bold);
           Font fnt3 = new Font("Arial Unicode MS", 15, FontStyle.Bold);
           Font fnt33 = new Font("Arial Unicode MS", 11, FontStyle.Regular);
           Font fnt2 = new Font("Arial Unicode MS", 11, FontStyle.Underline);
           // dessiner les ecritures 
           if (!string.IsNullOrEmpty(mois))
           {
               graphic.DrawString("Frais de congé du mois " + mois + " " + exercice, fnt3, Brushes.Black, unite_largeur, 5 * unite_hauteur + 3);
           }
           else
           {
               graphic.DrawString("Frais de congé ", fnt3, Brushes.Black, unite_largeur, 5 * unite_hauteur + 3);
           }
           graphic.DrawString("Emis le " + DateTime.Now, fnt0, Brushes.Black, 18 * unite_largeur, 5 * unite_hauteur + 5);

           graphic.FillRectangle(Brushes.Lavender, 17, 7 * unite_hauteur - 2,1  * unite_largeur , unite_hauteur *2);
           graphic.FillRectangle(Brushes.Lavender, 19 + unite_largeur, 7 * unite_hauteur - 2, 8 * unite_largeur+11, unite_hauteur * 2);
           graphic.FillRectangle(Brushes.Lavender, unite_largeur*10, 7 * unite_hauteur - 2, 4 * unite_largeur-2, unite_hauteur * 2);
           graphic.FillRectangle(Brushes.Lavender, unite_largeur * 14, 7 * unite_hauteur - 2, 4 * unite_largeur-4, unite_hauteur * 2);
           graphic.FillRectangle(Brushes.Lavender, unite_largeur * 18, 7 * unite_hauteur - 2, 3 * unite_largeur - 18, unite_hauteur * 2);
           graphic.FillRectangle(Brushes.Lavender, unite_largeur * 21 - 16, 7 * unite_hauteur - 2, 3 * unite_largeur+16, unite_hauteur * 2);


           graphic.DrawString("N° ", fnt11, Brushes.Black, 20, 7 * unite_hauteur + 10);
           graphic.DrawString("Nom du salarié ", fnt11, Brushes.Black, unite_largeur + 25, 7 * unite_hauteur+10);
           graphic.DrawString("Service ", fnt11, Brushes.Black, unite_largeur * 10 + 15, 7 * unite_hauteur + 10);
           graphic.DrawString("Fonction ", fnt11, Brushes.Black, unite_largeur * 14 + 15, 7 * unite_hauteur + 10);
           graphic.DrawString("Montant ", fnt11, Brushes.Black, unite_largeur * 18 + 5, 7 * unite_hauteur + 10);
           graphic.DrawString("Observation ", fnt11, Brushes.Black, 21 * unite_largeur -5, 7 * unite_hauteur + 10);
           var total = 0.0;
           for (int i = 0; i < dgvConge.Rows.Count; i++)
           {
               var YLOC = 9 * unite_hauteur + i * 35;

               graphic.DrawRectangle(Pens.Black, 17, YLOC, 1 * unite_largeur, 33);
               graphic.DrawRectangle(Pens.Black, 20 + unite_largeur,  YLOC, 8 * unite_largeur + 10, 33);
               graphic.DrawRectangle(Pens.Black, unite_largeur * 10, YLOC, 4 * unite_largeur - 3, 33);
               graphic.DrawRectangle(Pens.Black, unite_largeur * 14,  YLOC, 4 * unite_largeur - 3, 33);
               graphic.DrawRectangle(Pens.Black, unite_largeur * 18,  YLOC, 3 * unite_largeur - 19, 33);
               graphic.DrawRectangle(Pens.Black, unite_largeur * 21 - 16, YLOC, 3 * unite_largeur + 14, 33);

               var dt = ConnectionClass.ListeServicePersonnel(dgvConge.Rows[i].Cells[1].Value.ToString());
               var service = "";
               var qualification = "";
               //if (dt.Rows.Count > 0)
               //{
               //    service=dt.Rows[0].ItemArray[11].ToString();
               //    qualification = dt.Rows[0].ItemArray[12].ToString();
               //}
               //if (qualification.Length > 17)
               //{
               //    qualification = qualification.Substring(0, 17) + "\n" + qualification.Substring(17);
               //}
               if (!string.IsNullOrWhiteSpace(qualification))
               {
                   qualification = qualification.Substring(0, 1).ToUpper() + qualification.Substring(1).ToLower();
               }
               if (qualification.Length <= 5)
               {
                   qualification = qualification.ToUpper();
               }
               if (!string.IsNullOrWhiteSpace(service ))
               {
                   service = service.Substring(0, 1).ToUpper() + service.Substring(1).ToLower();
               }
               graphic.DrawString((i + 1).ToString(), fnt1, Brushes.Black, 20, YLOC + 3);
               graphic.DrawString(dgvConge.Rows[i].Cells[2].Value.ToString(), fnt1, Brushes.Black,  unite_largeur + 22, YLOC + 3);
               graphic.DrawString(service, fnt1, Brushes.Black, unite_largeur * 10+5, YLOC + 3);
               graphic.DrawString(qualification, fnt1, Brushes.Black, unite_largeur * 14 + 10, YLOC + 3);
               graphic.DrawString(dgvConge.Rows[i].Cells[3].Value.ToString(), fnt1, Brushes.Black, unite_largeur*18+15, YLOC + 3);
               total += Double.Parse(dgvConge.Rows[i].Cells[3].Value.ToString());
           }
           var LOC = 9 * unite_hauteur + 3 + dgvConge.Rows.Count * 35;
           graphic.DrawRectangle(Pens.Black, 17, LOC, 23 * unite_largeur + 15, 2*unite_hauteur );
           graphic.DrawString("Total", fnt11, Brushes.Black, unite_largeur + 10, LOC + 12);
           graphic.DrawString(string.Format(elGR, "{0:0,0}", total) , fnt11, Brushes.Black, 18 * unite_largeur +12, LOC +12);

           graphic.DrawString("Fait à N'Djaménale  " + DateTime.Now.ToShortDateString(), fnt11, Brushes.Black, 8 * unite_largeur + 10, LOC + unite_hauteur * 6 - 10);
           graphic.DrawString("Le directeur ", fnt11, Brushes.Black, 10 * unite_largeur + 10, LOC + unite_hauteur * 7);
           var dtP = ConnectionClass.ListeDesPersonnelParFonction("DIRECTEUR");
           if (dtP.Rows.Count > 0)
           {
               graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt11, Brushes.Black, 8 * unite_largeur + 10, LOC + 13 * unite_hauteur + 10);
           }
           return bitmap;
       }

       public static Bitmap ImprimerListeDesCNPS(List<Paiement> liste, double montant, double montantCNPS, string mois, int exercice, int start)
       {
           #region
           int unite_hauteur = 20;
           int unite_largeur = 32;
           int largeur_facture = 24 * unite_largeur + 10;
           int detail_hauteur_facture = 10 * unite_hauteur;
           int hauteur_facture = 56 * unite_hauteur;

           var drawFormatCenter = new StringFormat();
           drawFormatCenter.Alignment = StringAlignment.Center;
           var drawFormatLeft = new StringFormat();
           drawFormatLeft.Alignment = StringAlignment.Near;
           var drawFormatRight = new StringFormat();
           drawFormatRight.Alignment = StringAlignment.Far;

           //creer un bit map
           Bitmap bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

           // creer un objet graphic
           Graphics graphic = Graphics.FromImage(bitmap);

           //la couleur de l'image
           graphic.Clear(Color.White);
           #endregion
           //try
           //{
           //    Image logo = global::SGSP.Properties.Resources.Logo;
           //    graphic.DrawImage(logo, unite_largeur, 10, 12 * unite_largeur, 3* unite_hauteur);
           //}
           //catch { } //definir les polices 
           Font fnt1 = new Font("Arial", 11, FontStyle.Regular);
           Font fnt0 = new Font("Arial", 9, FontStyle.Regular);
           Font fnt11 = new Font("Arial ", 10.0f, FontStyle.Bold);
           Font fnt3 = new Font("Arial", 15, FontStyle.Bold);
           Font fnt33 = new Font("Arial", 11, FontStyle.Regular);
           Font fnt2 = new Font("Arial", 11, FontStyle.Bold );
           // dessiner les ecritures 
           graphic.DrawString("BORDEREAU DE LA CNPS DU MOIS DE " + mois.ToUpper() + " " + exercice, fnt3, Brushes.Black, 17,  20);
          
           graphic.DrawString("Page" + (start+1).ToString(),fnt0, Brushes.Black, 22 * unite_largeur-10,  unite_hauteur );

           graphic.FillRectangle(Brushes.Lavender, 17, 3 * unite_hauteur - 2, 1 * unite_largeur, unite_hauteur * 2);
           graphic.FillRectangle(Brushes.Lavender, 19 + unite_largeur, 3 * unite_hauteur - 2, 10 * unite_largeur + 11, unite_hauteur * 2);
           graphic.FillRectangle(Brushes.Lavender, unite_largeur * 12, 3 * unite_hauteur - 2, 3 * unite_largeur - 2, unite_hauteur * 2);
           graphic.FillRectangle(Brushes.Lavender, unite_largeur * 15, 3 * unite_hauteur - 2, 2* unite_largeur - 4, unite_hauteur * 2);
           graphic.FillRectangle(Brushes.Lavender, unite_largeur * 17, 3* unite_hauteur - 2, 3 * unite_largeur - 18, unite_hauteur * 2);
           graphic.FillRectangle(Brushes.Lavender, unite_largeur * 20- 16,3 * unite_hauteur - 2, 3 * unite_largeur + 19, unite_hauteur * 2);
           
           graphic.DrawString("N° ", fnt11, Brushes.Black, 20, 3 * unite_hauteur + 10);
           graphic.DrawString("NOMS & PRENOMS ", fnt11, Brushes.Black, unite_largeur * 2,3 * unite_hauteur + 10);
           graphic.DrawString("MONTANT ", fnt11, Brushes.Black, unite_largeur * 12 + 10, 3 * unite_hauteur + 10);
           graphic.DrawString("TAUX ", fnt11, Brushes.Black, unite_largeur * 15 + 10, 3 * unite_hauteur + 10);
           graphic.DrawString("CNPS A\n PAYER", fnt11, Brushes.Black, unite_largeur * 17 + 5, 3 * unite_hauteur + 2);
           graphic.DrawString("ASSURANCE ", fnt11, Brushes.Black, 20 * unite_largeur - 12, 3 * unite_hauteur + 10);
          
           var j = 0;
           var numero = 1 + start * 48;
           var total=.0;
           for (int i = start * 48; i < liste.Count; i++)
           {
               var YLOC = 5 * unite_hauteur + j * unite_hauteur ;
               if (liste[i].Employe == "TOTAL")
               {
                   total = liste[i].CNPS;
                   graphic.DrawRectangle(Pens.Black, 17, YLOC, 23 * unite_largeur - 15, unite_hauteur*2);
                
                   graphic.DrawString(liste[i].Employe.ToUpper(), fnt2, Brushes.Black, unite_largeur -10, YLOC + 10);
                   graphic.DrawString(string.Format(elGR, "{0:0,0}", liste[i].CNPS), fnt2, Brushes.Black, unite_largeur * 19 + 5, YLOC + 10, drawFormatRight);
               }
               else
               {
                   graphic.DrawRectangle(Pens.Black, 17, YLOC, 1 * unite_largeur + 2, unite_hauteur);
                   graphic.DrawRectangle(Pens.Black, 19 + unite_largeur, YLOC, 10 * unite_largeur + 13, unite_hauteur);
                   graphic.DrawRectangle(Pens.Black, unite_largeur * 12, YLOC, 3 * unite_largeur - 0, unite_hauteur);
                   graphic.DrawRectangle(Pens.Black, unite_largeur * 15, YLOC, 2 * unite_largeur - 0, unite_hauteur);
                   graphic.DrawRectangle(Pens.Black, unite_largeur * 17, YLOC, 3 * unite_largeur - 16, unite_hauteur);
                   graphic.DrawRectangle(Pens.Black, unite_largeur * 20 - 16, YLOC, 3 * unite_largeur + 18, unite_hauteur);

                   var dt = ConnectionClass.ListeServicePersonnel(liste[i].NumeroMatricule);
                   var cnps = "";
                   if (dt.Count > 0)
                   {
                       cnps = dt[0].NoCNPS;
                   }
                   double taux = Math.Round(liste[i].CNPS * 100 / liste[i].SalaireBrut, 1);
                   graphic.DrawString(numero.ToString(), fnt1, Brushes.Black, unite_largeur +15, YLOC + 2, drawFormatRight);
                   graphic.DrawString(liste[i].Employe.ToUpper(), fnt1, Brushes.Black, unite_largeur * 2, YLOC + 2);
                   graphic.DrawString(string.Format(elGR, "{0:0,0}", liste[i].SalaireBrut), fnt1, Brushes.Black, unite_largeur * 15 - 10, YLOC + 2, drawFormatRight);
                   graphic.DrawString(taux + "%", fnt1, Brushes.Black, unite_largeur * 17 - 10, YLOC + 2, drawFormatRight);
                   graphic.DrawString(string.Format(elGR, "{0:0,0}", liste[i].CNPS), fnt1, Brushes.Black, unite_largeur * 19 + 5, YLOC + 2, drawFormatRight);
                   graphic.DrawString(cnps.ToLower(), fnt1, Brushes.Black, unite_largeur * 23 - 5, YLOC + 2, drawFormatRight);
                   numero++;
               }
                   j++;
               
           }
           graphic.FillRectangle(Brushes.White, 16, 53*unite_hauteur+1, 24 * unite_largeur + 16, unite_hauteur * 8);

           var LOC = 6 * unite_hauteur  + j * 20;
           var text = "Arrêté le présent paiement de la CNPS à la somme de : " + Converti((int)total) + "(" + string.Format(elGR, "{0:0,0}", total) + ") Francs CFA";//, fnt1, Brushes.Black,  12*unite_largeur, LOC + unite_hauteur * 4 - 15,drawFormatCenter);
           RectangleF rect = new RectangleF(16, LOC +1 * unite_hauteur, 23 * unite_largeur - 10, 2 * unite_hauteur);
           var justification = JustifyText.TextJustification.Full;
           JustifyText.DrawParagraph(graphic, rect, fnt33, Brushes.Black, text, justification, 1.2f, 1.2f, 1.2f);

           graphic.DrawString("Le directeur Général de l'INSEED", fnt2, Brushes.Black, 12 * unite_largeur + 2, LOC + unite_hauteur * 5, drawFormatCenter);
                var dtP = ConnectionClass.ListeDesPersonnelParFonction("DIRECTEUR");
                if (dtP.Rows.Count > 0)
                {
                    var index = unite_hauteur * 5 + j * 20 + 10;
                    //graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt11, Brushes.Black, 8 * unite_largeur + 10, LOC + 13 * unite_hauteur + 10);
                    var ecart = hauteur_facture - (LOC + 2 * unite_hauteur + 10);
                    if (ecart <= 2 * unite_hauteur)
                    {
                        graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString().ToUpper() + " " + dtP.Rows[0].ItemArray[2].ToString().ToUpper(), fnt2, Brushes.Black, 8 * unite_largeur + 10, index + 8 * unite_hauteur - 5);
                    }
                    else if (ecart > 2 * unite_hauteur && ecart <= 3 * unite_hauteur)
                    {
                        graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString().ToUpper() + " " + dtP.Rows[0].ItemArray[2].ToString().ToUpper(), fnt2, Brushes.Black, 8 * unite_largeur + 10, index +8 * unite_hauteur - 5);
                    }
                    else if (ecart > 3 * unite_hauteur && ecart <= 4 * unite_hauteur)
                    {
                        graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt2, Brushes.Black, 8 * unite_largeur + 10, index + 4 * unite_hauteur - 5);
                    }
                    else if (ecart > 4 * unite_hauteur && ecart <= 5 * unite_hauteur)
                    {
                        graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt2, Brushes.Black, 8 * unite_largeur + 10, index + 5* unite_hauteur - 5);
                    }
                    else if (ecart > 5 * unite_hauteur && ecart <= 6 * unite_hauteur)
                    {
                        graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt2, Brushes.Black, 8 * unite_largeur + 10, index + 6 * unite_hauteur - 5);
                    }
                    else if (ecart > 6 * unite_hauteur)
                    {
                        graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString().ToUpper() + " " + dtP.Rows[0].ItemArray[2].ToString().ToUpper(), fnt2, Brushes.Black, 8 * unite_largeur + 10, index + 8 * unite_hauteur - 5);
                    } 
                }
           return bitmap;
       }

       public static Bitmap ImprimerListeDesIRPPFIR(List<Paiement> liste, double montant,double totalFIR, string mois, int exercice, int start)
       {
           #region
           int unite_hauteur = 20;
           int unite_largeur = 32;
           int largeur_facture = 24 * unite_largeur + 10;
           int detail_hauteur_facture = 10 * unite_hauteur;
           int hauteur_facture = 56 * unite_hauteur;
            if(liste.Count()>40)
            {
                hauteur_facture = 54 * unite_hauteur + 1;
            }
           //creer un bit map
           Bitmap bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

           // creer un objet graphic
           Graphics graphic = Graphics.FromImage(bitmap);

           //la couleur de l'image
           graphic.Clear(Color.White);
           #endregion
           var drawFormatCenter = new StringFormat();
           drawFormatCenter.Alignment = StringAlignment.Center;
           var drawFormatLeft = new StringFormat();
           drawFormatLeft.Alignment = StringAlignment.Near;
           var drawFormatRight = new StringFormat();
           drawFormatRight.Alignment = StringAlignment.Far;
           //try
           //{
           //    Image logo = global::SGSP.Properties.Resources.Logo;
           //    graphic.DrawImage(logo, unite_largeur, 10, 12 * unite_largeur, 3 * unite_hauteur);
           //}
           //catch { } //definir les polices 
           Font fnt1 = new Font("Arial ", 11, FontStyle.Regular);
           Font fnt0 = new Font("Arial ", 9, FontStyle.Regular);
           Font fnt11 = new Font("Arial ", 11.0f, FontStyle.Bold);
           Font fnt3 = new Font("Arial", 15, FontStyle.Bold);
           Font fnt33 = new Font("Arial ", 11, FontStyle.Regular);
           Font fnt2 = new Font("Arial ", 10, FontStyle.Bold | FontStyle.Italic);
           // dessiner les ecritures 
           graphic.DrawString("BORDEREAU IRPP / FIR DU MOIS " + mois.ToUpper() + " " + exercice, fnt3, Brushes.Black, unite_largeur, 25);

           graphic.DrawString("Page" + (start + 1).ToString(), fnt0, Brushes.Black, 22 * unite_largeur - 10, unite_hauteur);

           graphic.FillRectangle(Brushes.Lavender, 17, 3 * unite_hauteur - 2, 1 * unite_largeur, unite_hauteur * 2);
           graphic.FillRectangle(Brushes.Lavender, 19 + unite_largeur, 3 * unite_hauteur - 2,11 * unite_largeur -6, unite_hauteur * 2);
           graphic.FillRectangle(Brushes.Lavender, unite_largeur * 12+14, 3 * unite_hauteur - 2,3 * unite_largeur - 16, unite_hauteur * 2);
           graphic.FillRectangle(Brushes.Lavender, unite_largeur * 15, 3 * unite_hauteur - 2, 3 * unite_largeur - 2, unite_hauteur * 2);
           graphic.FillRectangle(Brushes.Lavender, unite_largeur * 18 - 0, 3 * unite_hauteur - 2, 2* unite_largeur + 10, unite_hauteur * 2);
           graphic.FillRectangle(Brushes.Lavender, unite_largeur * 20+12, 3 * unite_hauteur - 2, 3 * unite_largeur-0 , unite_hauteur * 2);

           graphic.DrawString("N° ", fnt11, Brushes.Black, 20, 3 * unite_hauteur + 10);
           graphic.DrawString("NOMS & PRENOMS ", fnt11, Brushes.Black, unite_largeur + 25, 3 * unite_hauteur + 10);
           graphic.DrawString("MONTANT", fnt11, Brushes.Black, unite_largeur *13 - 19,3 * unite_hauteur + 10);
           graphic.DrawString("IRPP", fnt11, Brushes.Black, unite_largeur * 16-2, 3 * unite_hauteur + 10);
           graphic.DrawString("FIR ", fnt11, Brushes.Black, 19 * unite_largeur - 12,3 * unite_hauteur + 10);
           graphic.DrawString("TOTAL ", fnt11, Brushes.Black, 21 * unite_largeur+5,3 * unite_hauteur + 10);

           var j = 0; var numero = 1 + start * 48;
           var total = .0;
           for (int i = start * 48; i < liste.Count; i++)
           {
               var YLOC = 5 * unite_hauteur + j * 20;
            

               graphic.DrawRectangle(Pens.Black, 17,  YLOC , 1 * unite_largeur+2, unite_hauteur);
               graphic.DrawRectangle(Pens.Black, 19 + unite_largeur, YLOC, 11 * unite_largeur - 5, unite_hauteur );
               graphic.DrawRectangle(Pens.Black, unite_largeur * 12 + 14, YLOC, 3 * unite_largeur - 14, unite_hauteur);
               graphic.DrawRectangle(Pens.Black, unite_largeur * 15, YLOC , 3 * unite_largeur - 0, unite_hauteur );
               graphic.DrawRectangle(Pens.Black, unite_largeur * 18 - 0,  YLOC , 2 * unite_largeur + 12, unite_hauteur );
               graphic.DrawRectangle(Pens.Black, unite_largeur * 20 + 12,  YLOC, 3 * unite_largeur - 0, unite_hauteur);

               graphic.DrawString(numero.ToString(), fnt1, Brushes.Black, 20, YLOC + 2);
               graphic.DrawString(liste[i].Employe.ToUpper(), fnt1, Brushes.Black, unite_largeur *2, YLOC + 1);
               graphic.DrawString(string.Format(elGR, "{0:0,0}", liste[i].SalaireBrut), fnt1, Brushes.Black, unite_largeur * 15 -10, YLOC + 1, drawFormatRight);
               graphic.DrawString(string.Format(elGR, "{0:0,0}", liste[i].IRPP), fnt1, Brushes.Black, unite_largeur * 18  -10, YLOC + 1, drawFormatRight);
               graphic.DrawString(string.Format(elGR, "{0:0,0}", liste[i].ONASA), fnt1, Brushes.Black, unite_largeur * 20 +2, YLOC + 1, drawFormatRight);
               graphic.DrawString(string.Format(elGR, "{0:0,0}", liste[i].IRPP + liste[i].ONASA), fnt1, Brushes.Black, unite_largeur * 23 +2, YLOC + 1,drawFormatRight);
               numero++;
               j++;
               
           }
            graphic.FillRectangle(Brushes.White, 16, 53 * unite_hauteur+1, 24 * unite_largeur + 16, unite_hauteur * 10);
           foreach(var l in liste)
               total += l.IRPP + l.ONASA;
           var LOC = 5 * unite_hauteur + j * unite_hauteur;
            graphic.DrawRectangle(Pens.Black, 17, LOC, 23 * unite_largeur -5, unite_hauteur*2);
            graphic.DrawString("Total", fnt11, Brushes.Black, unite_largeur + 10, LOC + 8);
            graphic.DrawString(string.Format(elGR, "{0:0,0}", total), fnt11, Brushes.Black, 23 * unite_largeur+2, LOC + 8,drawFormatRight);

            //graphic.DrawString("Le directeur ", fnt11, Brushes.Black, 10 * unite_largeur + 10, LOC + unite_hauteur * 7);

          var  text = "Arrêté le présent bordereau à la somme de : "+Converti((int)total) + "("+string.Format(elGR, "{0:0,0}", total) +") Francs CFA";//, fnt1, Brushes.Black,  12*unite_largeur, LOC + unite_hauteur * 4 - 15,drawFormatCenter);
           RectangleF rect = new RectangleF(16, LOC+3*unite_hauteur, 23 * unite_largeur + 0, 2 * unite_hauteur);
           var justification = JustifyText.TextJustification.Full;
           JustifyText.DrawParagraph(graphic, rect, fnt33, Brushes.Black, text, justification, 1.2f, 1.2f, 1.2f);

            graphic.DrawString("Le directeur Général de l'INSEED ", fnt11, Brushes.Black, 12 * unite_largeur + 10, LOC + unite_hauteur * 7, drawFormatCenter);
           var dtP = ConnectionClass.ListeDesPersonnelParFonction("DIRECTEUR");
           if (dtP.Rows.Count > 0)
           {
                //graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt11, Brushes.Black, 8 * unite_largeur + 10, LOC + 13 * unite_hauteur + 10);
                //var index = unite_hauteur * 9 + j * 22 + 10;
               //graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt11, Brushes.Black, 8 * unite_largeur + 10, LOC + 13 * unite_hauteur + 10);
               var ecart = hauteur_facture - (LOC + 2 * unite_hauteur + 10);
               if (ecart <= 2 * unite_hauteur)
               {
                   graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt11, Brushes.Black, 8 * unite_largeur + 10, LOC + 7 * 22 + 10);
               }
               else if (ecart > 2 * unite_hauteur && ecart <= 3 * unite_hauteur)
               {
                   graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt11, Brushes.Black, 8 * unite_largeur + 10, LOC + 9 * 22 - 5);
               }
               else if (ecart > 3 * unite_hauteur && ecart <= 4 * unite_hauteur)
               {
                   graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString().ToUpper() + " " + dtP.Rows[0].ItemArray[2].ToString().ToUpper(), fnt11, Brushes.Black, 8 * unite_largeur + 10, LOC + 11 * 22 - 5);
               }
               else if (ecart > 4 * unite_hauteur && ecart <= 5 * unite_hauteur)
               {
                   graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString().ToUpper() + " " + dtP.Rows[0].ItemArray[2].ToString().ToUpper(), fnt11, Brushes.Black, 8 * unite_largeur + 10, LOC + 10 * 22 - 5);
               }
               else if (ecart > 5 * unite_hauteur && ecart <= 6 * unite_hauteur)
               {
                   graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString().ToUpper() + " " + dtP.Rows[0].ItemArray[2].ToString().ToUpper(), fnt11, Brushes.Black, 8 * unite_largeur + 10, LOC + 11 * 22 - 5);
               }
               else if (ecart > 6 * unite_hauteur)
               {
                   graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString().ToUpper() + " " + dtP.Rows[0].ItemArray[2].ToString().ToUpper(), fnt11, Brushes.Black, 12 * unite_largeur + 5, LOC + 12 * 22 - 5, drawFormatCenter);
               } 
           }
           return bitmap;
       }

        static System.Globalization.CultureInfo elGR = System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR");

       public static Bitmap ImprimerListeRecapitulatifs(DataGridView dgvRecap, string mois, int exercice)
       {
           #region
           int unite_hauteur = 20;
           int unite_largeur = 32;
           int largeur_facture = 24 * unite_largeur + 10;
           int detail_hauteur_facture = 10 * unite_hauteur;
           int hauteur_facture = 56 * unite_hauteur;

           //creer un bit map
           Bitmap bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

           // creer un objet graphic
           Graphics graphic = Graphics.FromImage(bitmap);

           //la couleur de l'image
           graphic.Clear(Color.White);
           #endregion
           try
           {
               Image logo = global::SGSP.Properties.Resources.Logo;
               graphic.DrawImage(logo, unite_largeur, 10, 12 * unite_largeur, 4 * unite_hauteur);
           }
           catch { } //definir les polices 
           Font fnt1 = new Font("Arial Unicode MS", 12, FontStyle.Regular);
           Font fnt0 = new Font("Arial Unicode MS", 10, FontStyle.Regular);
           Font fnt11 = new Font("Arial Unicode MS", 12, FontStyle.Bold);
           Font fnt3 = new Font("Arial Unicode MS", 15, FontStyle.Bold | FontStyle.Underline);
           Font fnt33 = new Font("Arial Unicode MS", 11, FontStyle.Regular);
           Font fnt2 = new Font("Arial Unicode MS", 11, FontStyle.Underline);
           // dessiner les ecritures 
           graphic.DrawString("Recapitulatif de l' etat de paiement du   " + mois + " " + exercice, fnt3, Brushes.Black, unite_largeur, 7 * unite_hauteur + 3);
         
           graphic.DrawString("Emis le " + DateTime.Now, fnt0, Brushes.Black, 15 * unite_largeur, 5 * unite_hauteur + 5);

           graphic.FillRectangle(Brushes.Lavender, 17, 9 * unite_hauteur - 2, 1 * unite_largeur, unite_hauteur * 2);
           graphic.FillRectangle(Brushes.Lavender, 19 + unite_largeur, 9 * unite_hauteur - 2, 14 * unite_largeur + 11, unite_hauteur * 2);
           graphic.FillRectangle(Brushes.Lavender, unite_largeur * 16, 9 * unite_hauteur - 2, 4 * unite_largeur - 2, unite_hauteur * 2);


           graphic.DrawString("N° ", fnt11, Brushes.Black, 20, 9 * unite_hauteur + 10);
           graphic.DrawString("Libellé ", fnt11, Brushes.Black, unite_largeur *2, 9 * unite_hauteur + 10);
           graphic.DrawString("Montant ", fnt11, Brushes.Black, unite_largeur * 17 - 10, 9 * unite_hauteur + 10);
           var total = 0.0;
           for (int i = 0; i < dgvRecap.Rows.Count; i++)
           {
               var YLOC = 11 * unite_hauteur + i * 35;

               graphic.DrawRectangle(Pens.Black, 17, YLOC, 1 * unite_largeur, 33);
               graphic.DrawRectangle(Pens.Black, 20 + unite_largeur, YLOC, 14 * unite_largeur + 10, 33);
               graphic.DrawRectangle(Pens.Black, unite_largeur * 16, YLOC, 4 * unite_largeur - 3, 33);

               graphic.DrawString((i + 1).ToString(), fnt1, Brushes.Black, 20, YLOC + 3);
               graphic.DrawString(dgvRecap.Rows[i].Cells[2].Value.ToString(), fnt1, Brushes.Black, unite_largeur + 22, YLOC + 3);
               graphic.DrawString(dgvRecap.Rows[i].Cells[3].Value.ToString(), fnt1, Brushes.Black, unite_largeur * 17 -10, YLOC + 3);
               total += Double.Parse(dgvRecap.Rows[i].Cells[3].Value.ToString());
           }
           var LOC = 11 * unite_hauteur +2 + dgvRecap.Rows.Count * 35;
           graphic.DrawRectangle(Pens.Black, 16, LOC, 16 * unite_largeur - 18, 2 * unite_hauteur);
                     graphic.DrawRectangle(Pens.Black, 16*unite_largeur, LOC, 4 * unite_largeur -3, 2 * unite_hauteur);
           graphic.DrawString("Total", fnt11, Brushes.Black, unite_largeur + 10, LOC + 12);
           graphic.DrawString(string.Format(elGR, "{0:0,0}", total), fnt11, Brushes.Black, 17 * unite_largeur - 15, LOC + 12);

           graphic.DrawString("Fait à N'Djaménale  " + DateTime.Now.ToShortDateString(), fnt33, Brushes.Black, 7 * unite_largeur + 0,25*unite_hauteur + 0);
           graphic.DrawString("Le directeur ", fnt33, Brushes.Black, 8 * unite_largeur + 20, 26* unite_hauteur+10);
           var dtP = ConnectionClass.ListeDesPersonnelParFonction("DIRECTEUR");
           if (dtP.Rows.Count > 0)
           {
               graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt33, Brushes.Black, 6 * unite_largeur + 10, 34* unite_hauteur + 15);
           }
            return bitmap;
       }
     
        public static Bitmap ImprimerRecuDePaiement
            (System.Windows.Forms.DataGridView dgvVente)
        {
            //les dimension de la facture
            #region
            int unite_hauteur = 25;
            int unite_largeur = 32;
            int largeur_facture = 25 * unite_largeur + 10;
            int detail_hauteur_facture = 10 * unite_hauteur;
            int hauteur_facture = 42 * unite_hauteur;

            //creer un bit map
            Bitmap bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);

            try
            {
                Image logo = global::SGSP.Properties.Resources.Logo;
                graphic.DrawImage(logo, unite_largeur, unite_hauteur + 5, 23 * unite_largeur, 6 * unite_hauteur);
            }
            catch (Exception)
            { }
            //definir les polices 
            Font fnt1 = new Font("Ubuntu", 12, FontStyle.Regular);
            Font fnt11 = new Font("Ubuntu", 16, FontStyle.Bold);
            Font fnt12 = new Font("Ubuntu", 11, FontStyle.Italic);
            Font fnt3 = new Font("Ubuntu", 11, FontStyle.Bold);
            Font fnt2 = new Font("Ubuntu", 10, FontStyle.Regular);
            Font fnt22 = new Font("Ubuntu", 9, FontStyle.Regular);
            #endregion

            #region Section1
            graphic.FillRectangle(Brushes.Black, 15 * unite_largeur, 8 * unite_hauteur, 8 * unite_largeur, unite_hauteur);
            graphic.DrawRectangle(Pens.Black, 15 * unite_largeur, 8 * unite_hauteur, 8 * unite_largeur, unite_hauteur);
            graphic.DrawRectangle(Pens.Black, 15 * unite_largeur, 9 * unite_hauteur, 8 * unite_largeur, 2 * unite_hauteur);
            graphic.DrawString("Acompte  ", fnt11, Brushes.White, 17 * unite_largeur, 8 * unite_hauteur - 1);
            graphic.DrawString("Reçu No : " + dgvVente.SelectedRows[0].Cells[0].Value.ToString(), fnt2, Brushes.Black, 16 * unite_largeur, 9 * unite_hauteur + 4);
            graphic.DrawString("Date :        " + dgvVente.SelectedRows[0].Cells[3].Value.ToString(), fnt2, Brushes.Black, 16 * unite_largeur, 10 * unite_hauteur);

            graphic.DrawString("REÇU DE PAIEMENT DE L' ACOMPTE", fnt11, Brushes.Black, unite_largeur, 16 * unite_hauteur - 4);
            graphic.DrawString("Emis le " + DateTime.Now.ToString(), fnt2, Brushes.Black, unite_largeur, 17 * unite_hauteur);


            graphic.DrawRectangle(Pens.Black, unite_largeur, 19 * unite_hauteur, 23 * unite_largeur, 14 * unite_hauteur);

            graphic.DrawString("N° MATRICULE : " + dgvVente.SelectedRows[0].Cells[1].Value.ToString(), fnt1, Brushes.Black, 14 * unite_largeur, 13 * unite_hauteur - 10);
            graphic.DrawString("EMPLOYE : " + dgvVente.SelectedRows[0].Cells[2].Value.ToString(), fnt1, Brushes.Black, 14 * unite_largeur, 14 * unite_hauteur - 10);

            graphic.DrawString("Montant perçu ".ToUpper(), fnt1, Brushes.Black, unite_largeur * 4, 25 * unite_hauteur);
            graphic.DrawString(dgvVente.SelectedRows[0].Cells[4].Value.ToString() + " Franc CFA", fnt1, Brushes.Black, 15 * unite_largeur, 25 * unite_hauteur);

            var total = Double.Parse(dgvVente.SelectedRows[0].Cells[4].Value.ToString());
            graphic.DrawString("Arreté la présente somme de : " + Converti((int)total) + " Franc CFA", fnt1, Brushes.Black, 4 * unite_largeur, 27 * unite_hauteur);

            graphic.DrawString("Montant perçu ".ToUpper(), fnt1, Brushes.Black, unite_largeur * 4, 25 * unite_hauteur);
            graphic.DrawString(dgvVente.SelectedRows[0].Cells[4].Value.ToString() + " Franc CFA", fnt1, Brushes.Black, 15 * unite_largeur, 25 * unite_hauteur);


            graphic.DrawString("Fait à N’Djamena, le  " + DateTime.Now.ToShortDateString(), fnt1, Brushes.Black, 16 * unite_largeur, 37 * unite_hauteur - 10);
            graphic.DrawString("Signature du l'employé ", fnt1, Brushes.Black, unite_largeur, 38 * unite_hauteur);
            graphic.DrawString("Signature du l'employeur ", fnt1, Brushes.Black, 16 * unite_largeur, 38 * unite_hauteur);

            #endregion


            return bitmap;
        }

        public static Bitmap ImprimerRecuDeAvancesSureSalaire
              (System.Windows.Forms.DataGridView dgvVente)
        {
            //les dimension de la facture
            #region
            int unite_hauteur = 25;
            int unite_largeur = 32;
            int largeur_facture = 25 * unite_largeur + 10;
            int detail_hauteur_facture = 10 * unite_hauteur;
            int hauteur_facture = 42 * unite_hauteur;

            //creer un bit map
            Bitmap bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);

            try
            {
                Image logo = global::SGSP.Properties.Resources.Logo;
                graphic.DrawImage(logo, unite_largeur, unite_hauteur + 5, 23 * unite_largeur, 6 * unite_hauteur);
            }
            catch (Exception)
            { }
            //definir les polices 
            Font fnt1 = new Font("Arial Unicode MS", 13, FontStyle.Regular);
            Font fnt11 = new Font("Arial Unicode MS", 16, FontStyle.Bold);
            Font fnt12 = new Font("Arial Unicode MS", 11, FontStyle.Italic);
            Font fnt3 = new Font("Arial Unicode MS", 11, FontStyle.Bold);
            Font fnt2 = new Font("Arial Unicode MS", 10, FontStyle.Regular);
            Font fnt22 = new Font("Arial Unicode MS", 9, FontStyle.Regular);
            #endregion

            #region Section1
            graphic.FillRectangle(Brushes.Black, 15 * unite_largeur, 8 * unite_hauteur, 8 * unite_largeur, unite_hauteur);
            graphic.DrawRectangle(Pens.Black, 15 * unite_largeur, 8 * unite_hauteur, 8 * unite_largeur, unite_hauteur);
            graphic.DrawRectangle(Pens.Black, 15 * unite_largeur, 9 * unite_hauteur, 8 * unite_largeur, 2 * unite_hauteur);
            graphic.DrawString("Avance sur salaire  ", fnt11, Brushes.White, 17 * unite_largeur - 22, 8 * unite_hauteur - 1);
            graphic.DrawString("N° : " + dgvVente.SelectedRows[0].Cells[0].Value.ToString(), fnt2, Brushes.Black, 16 * unite_largeur, 9 * unite_hauteur + 4);
            graphic.DrawString("Date    :          " + dgvVente.SelectedRows[0].Cells[3].Value.ToString(), fnt2, Brushes.Black, 16 * unite_largeur, 10 * unite_hauteur);

            graphic.DrawString("REÇU DE PAIEMENT DE L'AVANCE SUR SALAIRE", fnt11, Brushes.Black, unite_largeur, 17 * unite_hauteur - 4);
            graphic.DrawString("Emis le " + DateTime.Now.ToString(), fnt2, Brushes.Black, unite_largeur, 18 * unite_hauteur);


            graphic.FillRectangle(Brushes.Black, unite_largeur - 1, 20 * unite_hauteur, 23 * unite_largeur + 2, unite_hauteur);

            graphic.DrawRectangle(Pens.Black, unite_largeur, 21 * unite_hauteur, 6 * unite_largeur, 5 * unite_hauteur);
            graphic.DrawRectangle(Pens.Black, 7 * unite_largeur, 21 * unite_hauteur, 6 * unite_largeur, 5 * unite_hauteur);
            graphic.DrawRectangle(Pens.Black, 13 * unite_largeur, 21 * unite_hauteur, 6 * unite_largeur, 5 * unite_hauteur);
            graphic.DrawRectangle(Pens.Black, 19 * unite_largeur, 21 * unite_hauteur, 5 * unite_largeur, 5 * unite_hauteur);
            graphic.DrawRectangle(Pens.Black, unite_largeur, 26 * unite_hauteur, 23 * unite_largeur, unite_hauteur);

            graphic.DrawString(dgvVente.SelectedRows[0].Cells[4].Value.ToString() + " F. CFA", fnt1, Brushes.Black, 3 * unite_largeur - 15, 23 * unite_hauteur);
            graphic.DrawString(dgvVente.SelectedRows[0].Cells[5].Value.ToString() + " F. CFA", fnt1, Brushes.Black, 8 * unite_largeur + 15, 23 * unite_hauteur);
            graphic.DrawString(dgvVente.SelectedRows[0].Cells[6].Value.ToString() + " F. CFA", fnt1, Brushes.Black, 14 * unite_largeur + 15, 23 * unite_hauteur);
            graphic.DrawString(dgvVente.SelectedRows[0].Cells[7].Value.ToString() + " F. CFA", fnt1, Brushes.Black, 20 * unite_largeur, 23 * unite_hauteur);

            graphic.DrawString("Montant  ", fnt1, Brushes.White, 3 * unite_largeur, 20 * unite_hauteur);
            graphic.DrawString("Payé  ", fnt1, Brushes.White, 8 * unite_largeur, 20 * unite_hauteur);
            graphic.DrawString("Restant ", fnt1, Brushes.White, 14 * unite_largeur, 20 * unite_hauteur);
            graphic.DrawString("Déduction ", fnt1, Brushes.White, 21 * unite_largeur, 20 * unite_hauteur);

            graphic.DrawString("N° MATRICULE : " + dgvVente.SelectedRows[0].Cells[1].Value.ToString(), fnt1, Brushes.Black, 12 * unite_largeur, 13 * unite_hauteur - 10);
            graphic.DrawString("EMPLOYE : " + dgvVente.SelectedRows[0].Cells[2].Value.ToString(), fnt1, Brushes.Black, 12 * unite_largeur, 14 * unite_hauteur - 10);
            var total = Double.Parse(dgvVente.SelectedRows[0].Cells[4].Value.ToString());
            graphic.DrawString("Somme de : " + Converti((int)total) + " Franc CFA", fnt1, Brushes.Black, unite_largeur + 10, 26 * unite_hauteur);

            graphic.FillRectangle(Brushes.Black, unite_largeur - 1, 28 * unite_hauteur, 23 * unite_largeur + 2, unite_hauteur);
            graphic.DrawString("Détails de remboursement des avances sur salaire ", fnt1, Brushes.White, 8 * unite_largeur, 28 * unite_hauteur);
            graphic.DrawRectangle(Pens.Black, unite_largeur, 28 * unite_hauteur, 23 * unite_largeur, 9 * unite_hauteur);

            var IdAcompte = Convert.ToInt32(dgvVente.SelectedRows[0].Cells[0].Value.ToString());
            var liste = ConnectionClass.ListeDetailsPaiementParNumeroAcompte(IdAcompte);
            if (liste.Count > 0)
            {
                var j = 0;
                var q = from p in liste
                        orderby p.DatePaiement
                        select p;
                foreach (var p in q)
                {
                    var YLOC = 29 * unite_hauteur + 15 + j * unite_hauteur;
                    graphic.DrawString(p.DatePaiement.ToShortDateString(), fnt1, Brushes.Black, 9 * unite_largeur, YLOC);
                    graphic.DrawString(p.AcomptePaye.ToString() + " F. CFA", fnt1, Brushes.Black, 14 * unite_largeur, YLOC);
                    j++;
                }
            }
            graphic.DrawString("Fait à N’Djamena, le  " + DateTime.Now.ToShortDateString(), fnt1, Brushes.Black, 16 * unite_largeur, 38 * unite_hauteur - 10);
            graphic.DrawString("Signature du l'employé ", fnt1, Brushes.Black, unite_largeur, 39 * unite_hauteur);
            graphic.DrawString("Signature du l'employeur ", fnt1, Brushes.Black, 16 * unite_largeur, 39 * unite_hauteur);

            #endregion


            return bitmap;
        }

        public static Bitmap ImprimerListeDeAvancesSurSalaire(DataGridView dgv,  int exercice, int start)
        {
            #region
            int unite_hauteur = 20;
            int unite_largeur = 32;
            int largeur_facture = 24 * unite_largeur + 10;
            int detail_hauteur_facture = 10 * unite_hauteur;
            int hauteur_facture = 56 * unite_hauteur;

            //creer un bit map
            Bitmap bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            #endregion
            try
            {
                Image logo = global::SGSP.Properties.Resources.Logo;
                graphic.DrawImage(logo, unite_largeur, 10, 12 * unite_largeur, 4 * unite_hauteur);
            }
            catch { } //definir les polices 
            Font fnt1 = new Font("Arial Unicode MS", 10, FontStyle.Regular);
            Font fnt0 = new Font("Arial Unicode MS", 9, FontStyle.Regular);
            Font fnt11 = new Font("Arial Unicode MS", 10, FontStyle.Bold);
            Font fnt3 = new Font("Arial Unicode MS", 15, FontStyle.Bold);
            Font fnt33 = new Font("Arial Unicode MS", 11, FontStyle.Regular);
            Font fnt2 = new Font("Arial Unicode MS", 10, FontStyle.Bold | FontStyle.Italic);
            // dessiner les ecritures 
            graphic.DrawString("Liste des avances sur salaire " , fnt3, Brushes.Black, unite_largeur, 4 * unite_hauteur + 15);

            graphic.DrawString("Page" + (start + 1).ToString(), fnt0, Brushes.Black, 22 * unite_largeur - 10, unite_hauteur);
            graphic.DrawString("Emis le " + DateTime.Now, fnt0, Brushes.Black, 18 * unite_largeur, 5 * unite_hauteur + 7);

            graphic.FillRectangle(Brushes.Lavender, 17, 7 * unite_hauteur - 2, 1 * unite_largeur, unite_hauteur * 2);
            graphic.FillRectangle(Brushes.Lavender, 19 + unite_largeur, 7 * unite_hauteur - 2, 9 * unite_largeur + 19, unite_hauteur * 2);
            //graphic.FillRectangle(Brushes.Lavender, unite_largeur * 9 + 21, 7 * unite_hauteur - 2, 3 * unite_largeur - 9, unite_hauteur * 2);
            graphic.FillRectangle(Brushes.Lavender, unite_largeur * 11 + 9, 7 * unite_hauteur - 2, 2 * unite_largeur+13, unite_hauteur * 2);
            graphic.FillRectangle(Brushes.Lavender, unite_largeur * 13+25, 7 * unite_hauteur - 2, 2 * unite_largeur +13, unite_hauteur * 2);
            graphic.FillRectangle(Brushes.Lavender, unite_largeur * 16+9, 7 * unite_hauteur - 2, 2 * unite_largeur + 13, unite_hauteur * 2);
            graphic.FillRectangle(Brushes.Lavender, unite_largeur * 18+25, 7 * unite_hauteur - 2, 2 * unite_largeur + 13, unite_hauteur * 2);
            graphic.FillRectangle(Brushes.Lavender, unite_largeur * 21 +9, 7 * unite_hauteur - 2, 2 * unite_largeur +16, unite_hauteur * 2);

            graphic.DrawString("N° ", fnt11, Brushes.Black, 20, 7 * unite_hauteur + 10);
            graphic.DrawString("NOMS & PRENOMS ", fnt11, Brushes.Black, unite_largeur + 25, 7 * unite_hauteur + 10);
            graphic.DrawString("DATE\n", fnt11, Brushes.Black, unite_largeur * 11 + 23, 7 * unite_hauteur + 10);
            graphic.DrawString("MONTANT  ", fnt11, Brushes.Black, unite_largeur * 14-6, 7 * unite_hauteur + 10);
            graphic.DrawString("DEDUCT ", fnt11, Brushes.Black, unite_largeur * 16 + 15, 7 * unite_hauteur + 10);
            graphic.DrawString("PAYE", fnt11, Brushes.Black, unite_largeur * 19 + 5, 7 * unite_hauteur + 10);
            graphic.DrawString("SOLDE ", fnt11, Brushes.Black, 21 * unite_largeur +20, 7 * unite_hauteur + 10);

            var j = 0; var numero = 1 + start * 40;
            for (int i = start * 40; i < dgv.Rows.Count; i++)
            {
                var YLOC = 9 * unite_hauteur + j * 22;
                if (dgv.Rows[i].Cells[2].Value.ToString() == "Total")
                {
                    graphic.FillRectangle(Brushes.Lavender, 16 , YLOC-3, 23 * unite_largeur +12, unite_hauteur +3);
              
                    graphic.DrawString(dgv.Rows[i].Cells[2].Value.ToString(), fnt11, Brushes.Black, unite_largeur + 25, YLOC + 3);
                    graphic.DrawString(dgv.Rows[i].Cells[3].Value.ToString(), fnt11, Brushes.Black, unite_largeur * 11 + 15, YLOC + 3);
                    graphic.DrawString(dgv.Rows[i].Cells[4].Value.ToString(), fnt11, Brushes.Black, unite_largeur * 14 - 6, YLOC + 3);
                    graphic.DrawString(dgv.Rows[i].Cells[5].Value.ToString(), fnt11, Brushes.Black, unite_largeur * 16 + 15, YLOC + 3);
                    graphic.DrawString(dgv.Rows[i].Cells[6].Value.ToString(), fnt11, Brushes.Black, unite_largeur * 19 + 5, YLOC + 3);
                    graphic.DrawString(dgv.Rows[i].Cells[7].Value.ToString(), fnt11, Brushes.Black, unite_largeur * 21 + 20, YLOC + 3);
                }
                else
                {
                    graphic.DrawRectangle(Pens.Black, 17, YLOC, 1 * unite_largeur, unite_hauteur * 2);
                    graphic.DrawRectangle(Pens.Black, 19 + unite_largeur, YLOC, 9 * unite_largeur + 19, unite_hauteur * 2);
                    //graphic.FillRectangle(Brushes.Lavender, unite_largeur * 9 + 21, 7 * unite_hauteur - 2, 3 * unite_largeur - 9, unite_hauteur * 2);
                    graphic.DrawRectangle(Pens.Black, unite_largeur * 11 + 9, YLOC, 2 * unite_largeur + 13, unite_hauteur * 2);
                    graphic.DrawRectangle(Pens.Black, unite_largeur * 13 + 25, YLOC, 2 * unite_largeur + 13, unite_hauteur * 2);
                    graphic.DrawRectangle(Pens.Black, unite_largeur * 16 + 9, YLOC, 2 * unite_largeur + 13, unite_hauteur * 2);
                    graphic.DrawRectangle(Pens.Black, unite_largeur * 18 + 25, YLOC, 2 * unite_largeur + 13, unite_hauteur * 2);
                    graphic.DrawRectangle(Pens.Black, unite_largeur * 21 + 9, YLOC, 2 * unite_largeur + 16, unite_hauteur * 2);

                    graphic.DrawString(numero.ToString(), fnt1, Brushes.Black, 20, YLOC + 3);
                    graphic.DrawString(dgv.Rows[i].Cells[2].Value.ToString(), fnt1, Brushes.Black, unite_largeur + 25, YLOC + 3);
                    graphic.DrawString(dgv.Rows[i].Cells[3].Value.ToString(), fnt1, Brushes.Black, unite_largeur * 11 + 15, YLOC + 3);
                    graphic.DrawString(dgv.Rows[i].Cells[4].Value.ToString(), fnt1, Brushes.Black, unite_largeur * 14 - 6, YLOC + 3);
                    graphic.DrawString(dgv.Rows[i].Cells[5].Value.ToString(), fnt1, Brushes.Black, unite_largeur * 16 + 15, YLOC + 3);
                    graphic.DrawString(dgv.Rows[i].Cells[6].Value.ToString(), fnt1, Brushes.Black, unite_largeur * 19 + 5, YLOC + 3);
                    graphic.DrawString(dgv.Rows[i].Cells[7].Value.ToString(), fnt1, Brushes.Black, unite_largeur * 21 + 20, YLOC + 3);
                }
                numero++;
                j++;

            }
            //graphic.FillRectangle(Brushes.White, 16, 9 * unite_hauteur + 40 * 22, 24 * unite_largeur + 16, unite_hauteur * 8);

            var LOC = 9 * unite_hauteur + j * 22;
            //graphic.DrawRectangle(Pens.Black, 17, LOC, 9 * unite_largeur + 2, unite_hauteur * 2);
            //graphic.DrawRectangle(Pens.Black, unite_largeur * 9 + 21, LOC, 6 * unite_largeur - 23, unite_hauteur * 2);
            //graphic.DrawRectangle(Pens.Black, unite_largeur * 15, LOC, 5 * unite_largeur - 18, unite_hauteur * 2);
            //graphic.DrawRectangle(Pens.Black, unite_largeur * 20 - 16, LOC, 1 * unite_largeur + 10, unite_hauteur * 2);
            //graphic.DrawRectangle(Pens.Black, unite_largeur * 21 - 4, LOC, 3 * unite_largeur - 12, unite_hauteur * 2);

            //graphic.DrawString("Total", fnt11, Brushes.Black, unite_largeur + 10, LOC + 12);
            //graphic.DrawString(string.Format(elGR, "{0:0,0}", Math.Round(montant - montant * 3.5 / 100)), fnt11, Brushes.Black, 12 * unite_largeur + 15, LOC + 12);
            //graphic.DrawString(string.Format(elGR, "{0:0,0}", Math.Round((montant - montant * 3.5 / 100) * 10.5 / 100)), fnt11, Brushes.Black, 17 * unite_largeur + 15, LOC + 12);
            //graphic.DrawString(string.Format(elGR, "{0:0,0}", totalFIR), fnt11, Brushes.Black, unite_largeur * 20 - 15, LOC + 12);
            //graphic.DrawString(string.Format(elGR, "{0:0,0}", totalFIR + Math.Round((montant - montant * 3.5 / 100) * 10.5 / 100)), fnt11, Brushes.Black, unite_largeur * 21 + 8, LOC + 12);
            //graphic.DrawString("Arrêté le présent bordereau à la somme de : ", fnt11, Brushes.Black, 20, LOC + unite_hauteur * 4 - 15);
            //graphic.DrawString(Converti((int)(totalFIR + Math.Round(montant * 10.5 / 100))) + "Frs CFA", fnt11, Brushes.Black, 9 * unite_largeur + 20, LOC + unite_hauteur * 4 - 15);

            graphic.DrawString("Fait à N'Djaménale  " + DateTime.Now.ToShortDateString(), fnt11, Brushes.Black, 8 * unite_largeur + 10, LOC + unite_hauteur * 6 - 10);
            graphic.DrawString("Le directeur ", fnt11, Brushes.Black, 10 * unite_largeur + 10, LOC + unite_hauteur * 7);
            var dtP = ConnectionClass.ListeDesPersonnelParFonction("DIRECTEUR");
            if (dtP.Rows.Count > 0)
            {
                graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt11, Brushes.Black, 8 * unite_largeur + 10, LOC + 13 * unite_hauteur + 10);
            }
            return bitmap;
        }

        public static string Converti(int chiffre)
        {
            int centaine, dizaine, unite, reste, y;
            bool dix = false;
            string lettre = "";
            //strcpy(lettre, "");

            reste = chiffre / 1;

            for (int i = 1000000000; i >= 1; i /= 1000)
            {
                y = reste / i;
                if (y != 0)
                {
                    centaine = y / 100;
                    dizaine = (y - centaine * 100) / 10;
                    unite = y - (centaine * 100) - (dizaine * 10);
                    switch (centaine)
                    {
                        case 0:
                            break;
                        case 1:
                            lettre += "cent ";
                            break;
                        case 2:
                            if ((dizaine == 0) && (unite == 0))
                            {
                                lettre += "deux cents ";
                            }
                            else
                            {
                                lettre += "deux cent ";
                            }
                            break;
                        case 3:
                            if ((dizaine == 0) && (unite == 0))
                            {
                                lettre += "trois cents ";
                            }
                            else
                            {
                                lettre += "trois cent ";
                            }
                            break;
                        case 4:
                            if ((dizaine == 0) && (unite == 0))
                            {
                                lettre += "quatre cents ";
                            }
                            else { lettre += "quatre cent "; }
                            break;
                        case 5:
                            if ((dizaine == 0) && (unite == 0)) { lettre += "cinq cents "; }
                            else { lettre += "cinq cent "; }
                            break;
                        case 6:
                            if ((dizaine == 0) && (unite == 0)) { lettre += "six cents "; }
                            else { lettre += "six cent "; }
                            break;
                        case 7:
                            if ((dizaine == 0) && (unite == 0)) { lettre += "sept cents "; }
                            else { lettre += "sept cent "; }
                            break;
                        case 8:
                            if ((dizaine == 0) && (unite == 0)) { lettre += "huit cents "; }
                            else { lettre += "huit cent "; }
                            break;
                        case 9:
                            if ((dizaine == 0) && (unite == 0)) lettre += "neuf cents ";
                            else lettre += "neuf cent ";
                            break;
                    }// endSwitch(centaine)

                    switch (dizaine)
                    {
                        case 0:
                            break;
                        case 1:
                            dix = true;
                            break;
                        case 2:
                            lettre += "vingt ";
                            break;
                        case 3:
                            lettre += "trente ";
                            break;
                        case 4:
                            lettre += "quarante ";
                            break;
                        case 5:
                            lettre += "cinquante ";
                            break;
                        case 6:
                            lettre += "soixante ";
                            break;
                        case 7:
                            dix = true;
                            lettre += "soixante ";
                            break;
                        case 8:
                            lettre += "quatre-vingt ";
                            break;
                        case 9:
                            dix = true;
                            lettre += "quatre-vingt ";
                            break;
                    } // endSwitch(dizaine)

                    switch (unite)
                    {
                        case 0:
                            if (dix) lettre += "dix ";
                            break;
                        case 1:
                            if (dix) lettre += "onze ";
                            else lettre += "un ";
                            break;
                        case 2:
                            if (dix) lettre += "douze ";
                            else lettre += "deux ";
                            break;
                        case 3:
                            if (dix) lettre += "treize ";
                            else lettre += "trois ";
                            break;
                        case 4:
                            if (dix) lettre += "quatorze ";
                            else lettre += "quatre ";
                            break;
                        case 5:
                            if (dix) lettre += "quinze ";
                            else lettre += "cinq ";
                            break;
                        case 6:
                            if (dix) lettre += "seize ";
                            else lettre += "six ";
                            break;
                        case 7:
                            if (dix) lettre += "dix-sept ";
                            else lettre += "sept ";
                            break;
                        case 8:
                            if (dix) lettre += "dix-huit ";
                            else lettre += "huit ";
                            break;
                        case 9:
                            if (dix) lettre += "dix-neuf ";
                            else lettre += "neuf ";
                            break;
                    } // endSwitch(unite)

                    switch (i)
                    {
                        case 1000000000:
                            if (y > 1) lettre += "milliards ";
                            else lettre += "milliard ";
                            break;
                        case 1000000:
                            if (y > 1) lettre += "millions ";
                            else lettre += "million ";
                            break;
                        case 1000:
                            lettre += "mille ";
                            break;
                    }
                } // end if(y!=0)
                reste -= y * i;
                dix = false;
            } // end for
            if (lettre.Length == 0) lettre += "zero";

            return lettre;
        }

#region MyRegion
	
        public static Bitmap ImprimerListeDesBancaries(List<Banque> liste, int start, int numeroPaiement)
        {
            #region
            int unite_hauteur = 22;
            int unite_largeur = 32;
            int largeur_facture = 35 * unite_largeur ;
            int hauteur_facture = 36 * unite_hauteur;
            //creer un bit map
            Bitmap bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            #endregion
            Font fnt1 = new Font("Century Gothic", 10.5f, FontStyle.Regular);
            Font fnt0 = new Font("Century Gothic", 12.5f, FontStyle.Underline);
            Font fnt11 = new Font("Century Gothic", 10.45f, FontStyle.Bold);
            Font fnt4 = new Font("Century Gothic", 14, FontStyle.Bold | FontStyle.Underline);
            Font fnt00 = new Font("Century Gothic", 11.00f, FontStyle.Bold | FontStyle.Underline);
            Font fnt111 = new Font("Century Gothic", 11.00f, FontStyle.Bold);
            Font fnt3 = new Font("Century Gothic", 11.50f, FontStyle.Regular);
            var page = start + 1;
            var drawFormatCenter = new StringFormat();
            drawFormatCenter.Alignment = StringAlignment.Center;
            var drawFormatLeft = new StringFormat();
            drawFormatLeft.Alignment = StringAlignment.Near;
            var drawFormatRight = new StringFormat();
            drawFormatRight.Alignment = StringAlignment.Far;

            graphic.DrawRectangle(Pens.Black, 22, 1 * unite_hauteur, unite_largeur + 10, 2 * +unite_hauteur + 0);

            graphic.DrawRectangle(Pens.Black, unite_largeur * 2, 1 * unite_hauteur, unite_largeur * 3, 2 * +unite_hauteur + 0);
            graphic.DrawRectangle(Pens.Black, 5 * unite_largeur + 0, 1 * unite_hauteur, unite_largeur * 6 + 16, 2 * unite_hauteur);
            graphic.DrawRectangle(Pens.Black, 11 * unite_largeur + 16, 1 * unite_hauteur, unite_largeur * 1 + 0, 2 * unite_hauteur);
            graphic.DrawRectangle(Pens.Black, 12 * unite_largeur + 16, 1 * unite_hauteur, unite_largeur * 9, 2 * unite_hauteur);
            graphic.DrawRectangle(Pens.Black, 21 * unite_largeur + 16, 1 * unite_hauteur, unite_largeur * 3, 2 * unite_hauteur);
            graphic.DrawRectangle(Pens.Black, 24 * unite_largeur + 16, 1 * unite_hauteur, unite_largeur * 3, unite_hauteur * 2);
            graphic.DrawRectangle(Pens.Black, 27 * unite_largeur + 16, 1 * unite_hauteur, unite_largeur * 3, unite_hauteur * 2);
            graphic.DrawRectangle(Pens.Black, 30 * unite_largeur + 16, 1 * unite_hauteur, unite_largeur * 3 + 16, unite_hauteur * 2);

            var rect1 = new RectangleF(22, 1 * unite_hauteur, unite_largeur + 10, 2 * +unite_hauteur + 5);
            var rect2 = new RectangleF(unite_largeur * 2, 1 * unite_hauteur, unite_largeur * 3, 2 * +unite_hauteur + 5);
            var rect3 = new RectangleF(5 * unite_largeur + 0, 1 * unite_hauteur, unite_largeur * 6 + 16, 2 * unite_hauteur + 5);
            var rect4 = new RectangleF(11 * unite_largeur + 16, 1 * unite_hauteur, unite_largeur * 1 + 0, 2 * unite_hauteur + 5);
            var rect5 = new RectangleF(12 * unite_largeur + 16, 1 * unite_hauteur, unite_largeur * 9, 2 * unite_hauteur + 5);
            var rect6 = new RectangleF(21 * unite_largeur + 16, 1 * unite_hauteur, unite_largeur * 3, 2 * unite_hauteur + 5);
            var rect7 = new RectangleF(24 * unite_largeur + 16, 1 * unite_hauteur, unite_largeur * 3, 2 * unite_hauteur + 5);
            var rect8 = new RectangleF(27 * unite_largeur + 16, 1 * unite_hauteur, unite_largeur * 3, 2 * unite_hauteur + 5);
            var rect9 = new RectangleF(30 * unite_largeur + 16, 1 * unite_hauteur, unite_largeur * 3 + 16, 2 * unite_hauteur + 5);

            graphic.DrawString("N°", fnt11, Brushes.Black, rect1, drawFormatCenter);
            graphic.DrawString("Banque".ToUpper(), fnt11, Brushes.Black, rect2, drawFormatCenter);
            graphic.DrawString("N° compte".ToUpper(), fnt11, Brushes.Black, rect3, drawFormatCenter);
            graphic.DrawString("Clé".ToUpper(), fnt11, Brushes.Black, rect4, drawFormatCenter);
            graphic.DrawString("Nom & Penom du salarié".ToUpper(), fnt11, Brushes.Black, rect5, drawFormatCenter);
            graphic.DrawString("SALAIRES".ToUpper(), fnt11, Brushes.Black, rect6, drawFormatCenter);
            graphic.DrawString("PRIMES DE MOTIVATION".ToUpper(), fnt11, Brushes.Black, rect7, drawFormatCenter);
            graphic.DrawString("FRAIS DE COM".ToUpper(), fnt11, Brushes.Black, rect8, drawFormatCenter);
            graphic.DrawString("MONTANT  TOTAL".ToUpper(), fnt11, Brushes.Black, rect9, drawFormatCenter);
            var j = 0;
            for (int i = start*32+9; i <= liste.Count() - 1; i++)
            {
                int Yloc = unite_hauteur *j + 3 * unite_hauteur + 0;
                if (i < liste.Count - 1)
                {
                    var rect11 = new Rectangle(22, Yloc, unite_largeur + 10, unite_hauteur);
                    var rect22 = new Rectangle(unite_largeur * 2, Yloc, unite_largeur * 3, unite_hauteur);
                    var rect33 = new Rectangle(5 * unite_largeur + 0, Yloc, unite_largeur * 6 + 16, unite_hauteur);
                    var rect44 = new Rectangle(11 * unite_largeur + 16, Yloc, unite_largeur * 1 + 0, unite_hauteur);
                    var rect55 = new Rectangle(12 * unite_largeur + 16, Yloc, unite_largeur * 9, unite_hauteur);
                    var rect66 = new Rectangle(21 * unite_largeur + 16, Yloc, unite_largeur * 3, unite_hauteur);
                    var rect77 = new Rectangle(24 * unite_largeur + 16, Yloc, unite_largeur * 3, unite_hauteur);
                    var rect88 = new Rectangle(27 * unite_largeur + 16, Yloc, unite_largeur * 3, unite_hauteur);
                    var rect99 = new Rectangle(30 * unite_largeur + 16, Yloc, unite_largeur * 3 + 16, unite_hauteur);

                    graphic.DrawRectangle(Pens.Black, rect11);
                    graphic.DrawRectangle(Pens.Black, rect22);
                    graphic.DrawRectangle(Pens.Black, rect33);
                    graphic.DrawRectangle(Pens.Black, rect44);
                    graphic.DrawRectangle(Pens.Black, rect55);
                    graphic.DrawRectangle(Pens.Black, rect66);
                    graphic.DrawRectangle(Pens.Black, rect77);
                    graphic.DrawRectangle(Pens.Black, rect88);
                    graphic.DrawRectangle(Pens.Black, rect99);
                    var numero = (i + 1);
                    var num = "";
                    if (numero < 10)
                    {
                        num = "00" + numero;
                    }
                    else
                    {
                        num = "0" + numero;
                    }
                    var banque = ConnectionClass.ListeBanques(liste[i].NomBanque);
                    if (banque[0].EtatParDefaut)
                    {
                        graphic.DrawString(liste[i].Compte, fnt1, Brushes.Black, rect33, drawFormatCenter);
                    }
                    else
                    {
                        var numeroCompte = banque[0].CodeBanque + "-" + banque[0].CodeGuichet + "-" + liste[i].Compte;
                        graphic.DrawString(numeroCompte, fnt1, Brushes.Black, rect33, drawFormatCenter);
                    }
                    graphic.DrawString(num.ToString(), fnt1, Brushes.Black, rect11, drawFormatRight);
                    graphic.DrawString(liste[i].NomEmploye.ToUpper(), fnt1, Brushes.Black, 13 * unite_largeur - 10, Yloc, drawFormatLeft);
                    graphic.DrawString(liste[i].NomBanque, fnt1, Brushes.Black, rect22, drawFormatCenter);
                    graphic.DrawString(liste[i].Cle, fnt1, Brushes.Black, rect44, drawFormatCenter);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", liste[i].NetAPayer), fnt1, Brushes.Black, rect99, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", liste[i].PrimeMotivation), fnt1, Brushes.Black, unite_largeur * 27 + 10, Yloc, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", liste[i].NetAPayer - liste[i].FraisCommunication - liste[i].PrimeMotivation), fnt1, Brushes.Black, unite_largeur * 24 + 10, Yloc, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", liste[i].FraisCommunication), fnt1, Brushes.Black, unite_largeur * 30 + 10, Yloc, drawFormatRight);
                }
                else if (i == liste.Count - 1)
                {
                    var rect66 = new Rectangle(21 * unite_largeur + 16, Yloc, unite_largeur * 3, 2*unite_hauteur-9);
                    var rect77 = new Rectangle(24 * unite_largeur + 16, Yloc, unite_largeur * 3, 2 * unite_hauteur - 9);
                    var rect88 = new Rectangle(27 * unite_largeur + 16, Yloc, unite_largeur * 3, 2 * unite_hauteur - 9);
                    var rect99 = new Rectangle(30 * unite_largeur + 16, Yloc, unite_largeur * 3 + 16, 2 * unite_hauteur - 9);

                    graphic.FillRectangle(Brushes.White, -2, Yloc + 0, unite_largeur * 25 + 2, unite_hauteur * 2 - 9);
                    graphic.DrawRectangle(Pens.Black, 22, Yloc + 0, unite_largeur * 21-6, unite_hauteur * 2 - 9);

                    graphic.DrawRectangle(Pens.Black, rect66);
                    graphic.DrawRectangle(Pens.Black, rect77);
                    graphic.DrawRectangle(Pens.Black, rect88);
                    graphic.DrawRectangle(Pens.Black, rect99);
                    graphic.DrawString(liste[i].NomEmploye, fnt11, Brushes.Black, unite_largeur, Yloc + 9);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", liste[i].NetAPayer), fnt11, Brushes.Black, 33 * unite_largeur + 22, Yloc + 9, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", liste[i].PrimeMotivation), fnt11, Brushes.Black, unite_largeur * 27 + 10, Yloc+9, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", liste[i].NetAPayer - liste[i].FraisCommunication - liste[i].PrimeMotivation), fnt11, Brushes.Black, unite_largeur * 24 + 10, Yloc+9, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", liste[i].FraisCommunication), fnt11, Brushes.Black, unite_largeur * 30 + 10, Yloc+9, drawFormatRight);
             
                }
                j++;
            }
            if (liste.Count <= (1 + start) * 32 + 9)
            {
                var index = unite_hauteur * 4 + j * unite_hauteur + 10;
                var paiement = ConnectionClass.ListeoOrdrePaiement(numeroPaiement);

                var ecart = hauteur_facture - (index + 2 * unite_hauteur + 10);
                if (ecart > 8 * unite_hauteur)
                {  
                    var montantTotal = .0;
                            foreach (var paie in ConnectionClass.ListeDetailsPaiement(numeroPaiement))
                                montantTotal += paie.SalaireNet ;
                            var text = "Arrêté le présent état de virement à la somme de :  " + Impression.Converti((int)montantTotal) + "(  " + String.Format(elGR, "{0:0,0}", montantTotal) + ") Franc CFA";

                    var justification = JustifyText.TextJustification.Center;
                    var rect = new Rectangle(1 * 22, index - 15, 33 * unite_largeur+20, 2 * unite_hauteur);
                    JustifyText.DrawParagraph(graphic, rect, fnt3, Brushes.Black, text, justification, 1.2f, 1.2f, 1.2f);
                   
                    graphic.DrawString("L'Ordonnateur des Dépenses\nLe Directeur Général de l'INSEED ", fnt3, Brushes.Black, 22, index + 1 * unite_hauteur + 5);
                    graphic.DrawString("Le Contrôleur Financier Adjoint ", fnt3, Brushes.Black, 17 * unite_largeur + 10, index +8 * unite_hauteur - 5, drawFormatCenter);
                    graphic.DrawString("Le liquidateur\nL' Agent Comptable de l'INSEED", fnt3, Brushes.Black, 33 * unite_largeur + 20, index + 1 * unite_hauteur + 5, drawFormatRight);

                    graphic.DrawString(paiement.Directeur, fnt00, Brushes.Black, 22, index + 8 * unite_hauteur - 5);
                    graphic.DrawString(paiement.Liquidateur, fnt00, Brushes.Black, 33 * unite_largeur + 20, index + 8 * unite_hauteur - 5, drawFormatRight);
                }
                else
                {
                }
            }
            graphic.FillRectangle(Brushes.White, 0, 35 * unite_hauteur +2, unite_largeur * 35, unite_hauteur * 15);

            return bitmap;
        }

        public static Bitmap ImprimerListeDesBancaries(int numeroPaiement, List<Banque> liste, string mois, int exercice)
        {
            #region
            int unite_hauteur = 22;
            int unite_largeur = 32;
            int largeur_facture = 35 * unite_largeur;
            int hauteur_facture = 36 * unite_hauteur + 0;//+ 15 + dtGrid.Rows.Count * unite_hauteur;
            if (liste.Count <= 2)
            {
                hauteur_facture = 39 * unite_hauteur + 16;
            }
            //creer un bit map
            Bitmap bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            #endregion
            try
            {
                Image logo = global::SGSP.Properties.Resources.ordreVirement;
                graphic.DrawImage(logo, unite_largeur - 18, 10, largeur_facture - 2 * unite_largeur + 15, 19 * unite_hauteur);
            }
            catch { } //definir les polices 
            Font fnt1 = new Font("Century Gothic", 10.5f, FontStyle.Regular);
            Font fnt0 = new Font("Century Gothic", 14.25f, FontStyle.Bold | FontStyle.Underline);
            Font fnt11 = new Font("Century Gothic", 10.40f, FontStyle.Bold);
            Font fnt4 = new Font("Century Gothic", 13.0f, FontStyle.Bold);
            Font fnt01 = new Font("Arial", 9.3f, FontStyle.Regular);
            Font fnt2 = new Font("Century Gothic", 12.5f, FontStyle.Regular);
            Font fnt22 = new Font("Century Gothic", 12.5f, FontStyle.Bold);

            var drawFormatCenter = new StringFormat();
            drawFormatCenter.Alignment = StringAlignment.Center;
            var drawFormatLeft = new StringFormat();
            drawFormatLeft.Alignment = StringAlignment.Near;
            var drawFormatRight = new StringFormat();
            drawFormatRight.Alignment = StringAlignment.Far;
            var moisChiffre = ObtenirMois(mois).ToString();
            if (ObtenirMois(mois) < 10)
                moisChiffre = "0" + ObtenirMois(mois);

            graphic.FillRectangle(Brushes.White, unite_largeur, 18 * unite_hauteur - 15, unite_largeur * 35, 3 * +unite_hauteur + 9);
            var text = "ORDRE DE VIREMENT : N° " + moisChiffre + "/CMT/PMT/MEPDCI/SE/DGM/INSEED/DAFRH/SC/" + exercice;
            graphic.DrawString(text, fnt0, Brushes.Black, 17 * unite_largeur, 18 * unite_hauteur - 6, drawFormatCenter);
            var somme = Converti((int)liste[liste.Count - 1].NetAPayer) + "Franc FCFA (" + String.Format(elGR, "{0:0,0}", liste[liste.Count - 1].NetAPayer) + " FCFA)";

            RectangleF rect = new RectangleF(17, 21 * unite_hauteur - 0, 33 * unite_largeur + 0, 3 * unite_hauteur);
            var justification = JustifyText.TextJustification.Full;
            var moisVoyelle = mois;
            if (mois.ToUpper().StartsWith("A") || mois.ToUpper().StartsWith("O"))
                moisVoyelle = "d'" + mois;
            somme = " la somme de : " + somme + " à repartir aux bénéficiaires conformément au tableau ci-après : (Mois  " + moisVoyelle + " " + exercice + ")";
            graphic.DrawString("Par le débit de notre compte N°/", fnt2, Brushes.Black, 17, 20 * unite_hauteur - 5);
            graphic.DrawString("60002 / 00001 /03010892302 / 96 ", fnt22, Brushes.Black, 10 * unite_largeur - 25, 20 * unite_hauteur - 5);
            graphic.DrawString("intitulé « ", fnt2, Brushes.Black, 18 * unite_largeur - 5, 20 * unite_hauteur - 5);
            graphic.DrawString("INSEED ", fnt22, Brushes.Black, 20 * unite_largeur - 2, 20 * unite_hauteur - 5);
            graphic.DrawString(" » ouvert dans votre banque SGT, veuillez virer", fnt2, Brushes.Black, 33 * unite_largeur + 21, 20 * unite_hauteur - 5, drawFormatRight);
            JustifyText.DrawParagraph(graphic, rect, fnt2, Brushes.Black, somme, justification, 1.2f, 1.2f, 1.2f);

            graphic.DrawRectangle(Pens.Black, 22, 24 * unite_hauteur, unite_largeur + 10, 2 * +unite_hauteur + 0);

            graphic.DrawRectangle(Pens.Black, unite_largeur * 2, 24 * unite_hauteur, unite_largeur * 3, 2 * +unite_hauteur + 0);
            graphic.DrawRectangle(Pens.Black, 5 * unite_largeur + 0, 24 * unite_hauteur, unite_largeur * 6 + 16, 2 * unite_hauteur);
            graphic.DrawRectangle(Pens.Black, 11 * unite_largeur + 16, 24 * unite_hauteur, unite_largeur * 1 + 0, 2 * unite_hauteur);
            graphic.DrawRectangle(Pens.Black, 12 * unite_largeur + 16, 24 * unite_hauteur, unite_largeur * 9, 2 * unite_hauteur);
            graphic.DrawRectangle(Pens.Black, 21 * unite_largeur + 16, 24 * unite_hauteur, unite_largeur * 3, 2 * unite_hauteur);
            graphic.DrawRectangle(Pens.Black, 24 * unite_largeur + 16, 24 * unite_hauteur, unite_largeur * 3, unite_hauteur * 2);
            graphic.DrawRectangle(Pens.Black, 27 * unite_largeur + 16, 24 * unite_hauteur, unite_largeur * 3, unite_hauteur * 2);
            graphic.DrawRectangle(Pens.Black, 30 * unite_largeur + 16, 24 * unite_hauteur, unite_largeur * 3 + 16, unite_hauteur * 2);

            var rect1 = new RectangleF(22, 24 * unite_hauteur, unite_largeur + 10, 2 * +unite_hauteur + 5);
            var rect2 = new RectangleF(unite_largeur * 2, 24 * unite_hauteur, unite_largeur * 3, 2 * +unite_hauteur + 5);
            var rect3 = new RectangleF(5 * unite_largeur + 0, 24 * unite_hauteur, unite_largeur * 6 + 16, 2 * unite_hauteur + 5);
            var rect4 = new RectangleF(11 * unite_largeur + 16, 24 * unite_hauteur, unite_largeur * 1 + 0, 2 * unite_hauteur + 5);
            var rect5 = new RectangleF(12 * unite_largeur + 16, 24 * unite_hauteur, unite_largeur * 9, 2 * unite_hauteur + 5);
            var rect6 = new RectangleF(21 * unite_largeur + 16, 24 * unite_hauteur, unite_largeur * 3, 2 * unite_hauteur + 5);
            var rect7 = new RectangleF(24 * unite_largeur + 16, 24 * unite_hauteur, unite_largeur * 3, 2 * unite_hauteur + 5);
            var rect8 = new RectangleF(27 * unite_largeur + 16, 24 * unite_hauteur, unite_largeur * 3, 2 * unite_hauteur + 5);
            var rect9 = new RectangleF(30 * unite_largeur + 16, 24 * unite_hauteur, unite_largeur * 3 + 16, 2 * unite_hauteur + 5);

            graphic.DrawString("N°", fnt11, Brushes.Black, rect1, drawFormatCenter);
            graphic.DrawString("Banque".ToUpper(), fnt11, Brushes.Black, rect2, drawFormatCenter);
            graphic.DrawString("N° compte".ToUpper(), fnt11, Brushes.Black, rect3, drawFormatCenter);
            graphic.DrawString("Clé".ToUpper(), fnt11, Brushes.Black, rect4, drawFormatCenter);
            graphic.DrawString("Nom & Penom du salarié".ToUpper(), fnt11, Brushes.Black, rect5, drawFormatCenter);
            graphic.DrawString("SALAIRES".ToUpper(), fnt11, Brushes.Black, rect6, drawFormatCenter);
            graphic.DrawString("PRIMES DE MOTIVATION".ToUpper(), fnt11, Brushes.Black, rect7, drawFormatCenter);
            graphic.DrawString("FRAIS DE COM".ToUpper(), fnt11, Brushes.Black, rect8, drawFormatCenter);
            graphic.DrawString("MONTANT  TOTAL".ToUpper(), fnt11, Brushes.Black, rect9, drawFormatCenter);
            for (int i = 0; i <= liste.Count() - 1; i++)
            {
                int Yloc = unite_hauteur * i + 26 * unite_hauteur + 0;
                if (i < liste.Count - 1)
                {
                    var rect11 = new Rectangle(22, Yloc, unite_largeur + 10, unite_hauteur);
                    var rect22 = new Rectangle(unite_largeur * 2, Yloc, unite_largeur * 3, unite_hauteur);
                    var rect33 = new Rectangle(5 * unite_largeur + 0, Yloc, unite_largeur * 6 + 16, unite_hauteur);
                    var rect44 = new Rectangle(11 * unite_largeur + 16, Yloc, unite_largeur * 1 + 0, unite_hauteur);
                    var rect55 = new Rectangle(12 * unite_largeur + 16, Yloc, unite_largeur * 9, unite_hauteur);
                    var rect66 = new Rectangle(21 * unite_largeur + 16, Yloc, unite_largeur * 3, unite_hauteur);
                    var rect77 = new Rectangle(24 * unite_largeur + 16, Yloc, unite_largeur * 3, unite_hauteur);
                    var rect88 = new Rectangle(27 * unite_largeur + 16, Yloc, unite_largeur * 3, unite_hauteur);
                    var rect99 = new Rectangle(30 * unite_largeur + 16, Yloc, unite_largeur * 3 + 16, unite_hauteur);

                    graphic.DrawRectangle(Pens.Black, rect11);
                    graphic.DrawRectangle(Pens.Black, rect22);
                    graphic.DrawRectangle(Pens.Black, rect33);
                    graphic.DrawRectangle(Pens.Black, rect44);
                    graphic.DrawRectangle(Pens.Black, rect55);
                    graphic.DrawRectangle(Pens.Black, rect66);
                    graphic.DrawRectangle(Pens.Black, rect77);
                    graphic.DrawRectangle(Pens.Black, rect88);
                    graphic.DrawRectangle(Pens.Black, rect99);
                    var numero = (i + 1);
                    var num = "";
                    if (numero < 10)
                    {
                        num = "00" + numero;
                    }
                    else
                    {
                        num = "0" + numero;
                    }
                    var banque = ConnectionClass.ListeBanques(liste[i].NomBanque);
                    if (banque[0].EtatParDefaut)
                    {
                        graphic.DrawString(liste[i].Compte, fnt1, Brushes.Black, rect33, drawFormatCenter);
                    }
                    else
                    {
                        var numeroCompte = banque[0].CodeBanque + "-" + banque[0].CodeGuichet + "-" + liste[i].Compte;
                        graphic.DrawString(numeroCompte, fnt1, Brushes.Black, rect33, drawFormatCenter);
                    }
                    graphic.DrawString(num.ToString(), fnt1, Brushes.Black, rect11, drawFormatRight);
                    graphic.DrawString(liste[i].NomEmploye.ToUpper(), fnt1, Brushes.Black, 13 * unite_largeur - 10, Yloc, drawFormatLeft);
                    graphic.DrawString(liste[i].NomBanque, fnt01, Brushes.Black, rect22, drawFormatCenter);
                    graphic.DrawString(liste[i].Cle, fnt1, Brushes.Black, rect44, drawFormatCenter);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", liste[i].NetAPayer), fnt1, Brushes.Black, rect99, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", liste[i].PrimeMotivation), fnt1, Brushes.Black, unite_largeur * 27 + 10, Yloc, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", liste[i].NetAPayer - liste[i].FraisCommunication - liste[i].PrimeMotivation), fnt1, Brushes.Black, unite_largeur * 24 + 10, Yloc, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", liste[i].FraisCommunication), fnt1, Brushes.Black, unite_largeur * 30 + 10, Yloc, drawFormatRight);
                }
                else if (i == liste.Count - 1)
                {
                    var rect66 = new Rectangle(21 * unite_largeur + 16, Yloc, unite_largeur * 3, 2 * unite_hauteur - 9);
                    var rect77 = new Rectangle(24 * unite_largeur + 16, Yloc, unite_largeur * 3, 2 * unite_hauteur - 9);
                    var rect88 = new Rectangle(27 * unite_largeur + 16, Yloc, unite_largeur * 3, 2 * unite_hauteur - 9);
                    var rect99 = new Rectangle(30 * unite_largeur + 16, Yloc, unite_largeur * 3 + 16, 2 * unite_hauteur - 9);

                    graphic.FillRectangle(Brushes.White, -2, Yloc + 0, unite_largeur * 25 + 2, unite_hauteur * 2 - 9);
                    graphic.DrawRectangle(Pens.Black, 22, Yloc + 0, unite_largeur * 21 - 6, unite_hauteur * 2 - 9);

                    graphic.DrawRectangle(Pens.Black, rect66);
                    graphic.DrawRectangle(Pens.Black, rect77);
                    graphic.DrawRectangle(Pens.Black, rect88);
                    graphic.DrawRectangle(Pens.Black, rect99);
                    graphic.DrawString(liste[i].NomEmploye, fnt11, Brushes.Black, unite_largeur, Yloc + 9);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", liste[i].NetAPayer), fnt11, Brushes.Black, 33 * unite_largeur + 22, Yloc + 9, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", liste[i].PrimeMotivation), fnt11, Brushes.Black, unite_largeur * 27 + 10, Yloc + 9, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", liste[i].NetAPayer - liste[i].FraisCommunication - liste[i].PrimeMotivation), fnt11, Brushes.Black, unite_largeur * 24 + 10, Yloc + 9, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", liste[i].FraisCommunication), fnt11, Brushes.Black, unite_largeur * 30 + 10, Yloc + 9, drawFormatRight);

                }
            }

            graphic.FillRectangle(Brushes.White, 0, 35 * unite_hauteur + 1, unite_largeur * 35, unite_hauteur * 15);
            if (liste.Count <= 2)
            {
                var index = unite_hauteur * 26 + liste.Count * unite_hauteur + 0;
                var paiement = ConnectionClass.ListeoOrdrePaiement(numeroPaiement);

                var ecart = hauteur_facture - (index + 2 * unite_hauteur + 10);
                if (ecart > 8 * unite_hauteur)
                {
                    var montantTotal = .0;
                    foreach (var paie in ConnectionClass.ListeDetailsPaiement(numeroPaiement))
                        montantTotal += paie.SalaireNet;
                    text = "Arrêté le présent état de virement à la somme de :  " + Impression.Converti((int)montantTotal) + "(  " + String.Format(elGR, "{0:0,0}", montantTotal) + ") Franc CFA";

                    justification = JustifyText.TextJustification.Center;
                    rect = new Rectangle(1 * 22, index + 15, 33 * unite_largeur + 20, 2 * unite_hauteur);
                    JustifyText.DrawParagraph(graphic, rect, fnt2, Brushes.Black, text, justification, 1.2f, 1.2f, 1.2f);

                    graphic.DrawString("L'Ordonnateur des Dépenses\nLe Directeur Général de l'INSEED ", fnt2, Brushes.Black, 22, index + 2 * unite_hauteur + 20);
                    graphic.DrawString("Le Contrôleur Financier Adjoint ", fnt2, Brushes.Black, 17 * unite_largeur + 10, index + 9 * unite_hauteur + 15, drawFormatCenter);
                    graphic.DrawString("Le liquidateur\nL' Agent Comptable de l'INSEED", fnt2, Brushes.Black, 33 * unite_largeur + 20, index + 2 * unite_hauteur + 20, drawFormatRight);

                    graphic.DrawString(paiement.Directeur, fnt22, Brushes.Black, 22, index + 9 * unite_hauteur + 15);
                    graphic.DrawString(paiement.Liquidateur, fnt22, Brushes.Black, 33 * unite_largeur + 20, index + 9 * unite_hauteur + 15, drawFormatRight);
                }
                else
                {
                }
            }

            return bitmap;
        }

        public static Bitmap OrdreDeVirementDesPrimes(int numeroPaiement, List<Banque> liste, string mois, int exercice)
        {
            #region
            int unite_hauteur = 22;
            int unite_largeur = 32;
            int largeur_facture = 35 * unite_largeur;
            int hauteur_facture = 36 * unite_hauteur + 0;//+ 15 + dtGrid.Rows.Count * unite_hauteur;
            if (liste.Count <= 2)
            {
                hauteur_facture = 39 * unite_hauteur + 16;
            }
            //creer un bit map
            Bitmap bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            #endregion
            try
            {
                Image logo = global::SGSP.Properties.Resources.ordreVirement;
                graphic.DrawImage(logo, unite_largeur - 18, 10, largeur_facture - 2 * unite_largeur + 15, 19 * unite_hauteur);
            }
            catch { } //definir les polices 
            Font fnt1 = new Font("Century Gothic", 10.5f, FontStyle.Regular);
            Font fnt0 = new Font("Century Gothic", 14.25f, FontStyle.Bold | FontStyle.Underline);
            Font fnt11 = new Font("Century Gothic", 10.40f, FontStyle.Bold);
            Font fnt4 = new Font("Century Gothic", 13.0f, FontStyle.Bold);
            Font fnt01 = new Font("Arial", 9.3f, FontStyle.Regular);
            Font fnt2 = new Font("Century Gothic", 12.5f, FontStyle.Regular);
            Font fnt22 = new Font("Century Gothic", 12.5f, FontStyle.Bold);

            var drawFormatCenter = new StringFormat();
            drawFormatCenter.Alignment = StringAlignment.Center;
            var drawFormatLeft = new StringFormat();
            drawFormatLeft.Alignment = StringAlignment.Near;
            var drawFormatRight = new StringFormat();
            drawFormatRight.Alignment = StringAlignment.Far;
            var moisChiffre = ObtenirMois(mois).ToString();
            if (ObtenirMois(mois) < 10)
                moisChiffre = "0" + ObtenirMois(mois);

            graphic.FillRectangle(Brushes.White, unite_largeur, 18 * unite_hauteur - 15, unite_largeur * 35, 3 * +unite_hauteur + 9);
            var text = "ORDRE DE VIREMENT : N° " + moisChiffre + "/CMT/PMT/MEPDCI/SE/DGM/INSEED/DAFRH/SC/" + exercice;
            graphic.DrawString(text, fnt0, Brushes.Black, 17 * unite_largeur, 18 * unite_hauteur - 6, drawFormatCenter);
            var somme = Converti((int)liste[liste.Count - 1].NetAPayer) + "Franc FCFA (" + String.Format(elGR, "{0:0,0}", liste[liste.Count - 1].NetAPayer) + " FCFA)";

            RectangleF rect = new RectangleF(17, 21 * unite_hauteur - 0, 33 * unite_largeur + 0, 3 * unite_hauteur);
            var justification = JustifyText.TextJustification.Full;
            var moisVoyelle = mois;
            if (mois.ToUpper().StartsWith("A") || mois.ToUpper().StartsWith("O"))
                moisVoyelle = "d'" + mois;
            somme = " la somme de : " + somme + " à repartir aux bénéficiaires conformément au tableau ci-après : (Mois  " + moisVoyelle + " " + exercice + ")";
            graphic.DrawString("Par le débit de notre compte N°/", fnt2, Brushes.Black, 17, 20 * unite_hauteur - 5);
            graphic.DrawString("60002 / 00001 /03010892302 / 96 ", fnt22, Brushes.Black, 10 * unite_largeur - 25, 20 * unite_hauteur - 5);
            graphic.DrawString("intitulé « ", fnt2, Brushes.Black, 18 * unite_largeur - 5, 20 * unite_hauteur - 5);
            graphic.DrawString("INSEED ", fnt22, Brushes.Black, 20 * unite_largeur - 2, 20 * unite_hauteur - 5);
            graphic.DrawString(" » ouvert dans votre banque SGT, veuillez virer", fnt2, Brushes.Black, 33 * unite_largeur + 21, 20 * unite_hauteur - 5, drawFormatRight);
            JustifyText.DrawParagraph(graphic, rect, fnt2, Brushes.Black, somme, justification, 1.2f, 1.2f, 1.2f);

            graphic.DrawRectangle(Pens.Black, 22, 24 * unite_hauteur - 5, unite_largeur + 10, 2 * +unite_hauteur + 0 + 5);

            graphic.DrawRectangle(Pens.Black, unite_largeur * 2, 24 * unite_hauteur - 5, unite_largeur * 3, 2 * +unite_hauteur + 5);
            graphic.DrawRectangle(Pens.Black, 5 * unite_largeur + 0, 24 * unite_hauteur - 5, unite_largeur * 6 + 16, 2 * unite_hauteur + 5);
            graphic.DrawRectangle(Pens.Black, 11 * unite_largeur + 16, 24 * unite_hauteur - 5, unite_largeur * 1 + 0, 2 * unite_hauteur + 5);
            graphic.DrawRectangle(Pens.Black, 12 * unite_largeur + 16, 24 * unite_hauteur - 5, unite_largeur * 9, 2 * unite_hauteur + 5);
            graphic.DrawRectangle(Pens.Black, 21 * unite_largeur + 16, 24 * unite_hauteur - 5, unite_largeur * 3, 2 * unite_hauteur + 5);
            graphic.DrawRectangle(Pens.Black, 24 * unite_largeur + 16, 24 * unite_hauteur - 5, unite_largeur * 3, unite_hauteur * 2 + 5);
            graphic.DrawRectangle(Pens.Black, 27 * unite_largeur + 16, 24 * unite_hauteur - 5, unite_largeur * 3, unite_hauteur * 2 + 5);
            graphic.DrawRectangle(Pens.Black, 30 * unite_largeur + 16, 24 * unite_hauteur - 5, unite_largeur * 3 + 16, unite_hauteur * 2 + 5);

            var rect1 = new RectangleF(22, 24 * unite_hauteur - 5, unite_largeur + 10, 2 * +unite_hauteur + 5);
            var rect2 = new RectangleF(unite_largeur * 2, 24 * unite_hauteur - 5, unite_largeur * 3, 2 * +unite_hauteur + 5);
            var rect3 = new RectangleF(5 * unite_largeur + 0, 24 * unite_hauteur - 5, unite_largeur * 6 + 16, 2 * unite_hauteur + 5);
            var rect4 = new RectangleF(11 * unite_largeur + 16, 24 * unite_hauteur - 5, unite_largeur * 1 + 0, 2 * unite_hauteur + 5);
            var rect5 = new RectangleF(12 * unite_largeur + 16, 24 * unite_hauteur - 5, unite_largeur * 9, 2 * unite_hauteur + 5);
            var rect6 = new RectangleF(21 * unite_largeur + 16, 24 * unite_hauteur - 5, unite_largeur * 3, 2 * unite_hauteur + 5);
            var rect7 = new RectangleF(24 * unite_largeur + 16, 24 * unite_hauteur - 5, unite_largeur * 3, 2 * unite_hauteur + 5);
            var rect8 = new RectangleF(27 * unite_largeur + 16, 24 * unite_hauteur - 5, unite_largeur * 3, 2 * unite_hauteur + 5);
            var rect9 = new RectangleF(30 * unite_largeur + 16, 24 * unite_hauteur - 5, unite_largeur * 3 + 16, 2 * unite_hauteur + 5);

            graphic.DrawString("N°", fnt11, Brushes.Black, rect1, drawFormatCenter);
            graphic.DrawString("Banque".ToUpper(), fnt11, Brushes.Black, rect2, drawFormatCenter);
            graphic.DrawString("N° compte".ToUpper(), fnt11, Brushes.Black, rect3, drawFormatCenter);
            graphic.DrawString("Clé".ToUpper(), fnt11, Brushes.Black, rect4, drawFormatCenter);
            graphic.DrawString("Nom & Penom".ToUpper(), fnt11, Brushes.Black, rect5, drawFormatCenter);
            graphic.DrawString("PRIMES DE MOTIVATION".ToUpper(), fnt11, Brushes.Black, rect6, drawFormatCenter);
            graphic.DrawString("INDEMNITES AGENT COMPTABLE".ToUpper(), fnt11, Brushes.Black, rect7, drawFormatCenter);
            graphic.DrawString("FRAIS DE COM".ToUpper(), fnt11, Brushes.Black, rect8, drawFormatCenter);
            graphic.DrawString("MONTANT  TOTAL".ToUpper(), fnt11, Brushes.Black, rect9, drawFormatCenter);
            for (int i = 0; i <= liste.Count() - 1; i++)
            {
                int Yloc = unite_hauteur * i + 26 * unite_hauteur + 0;
                if (i < liste.Count - 1)
                {
                    var rect11 = new Rectangle(22, Yloc, unite_largeur + 10, unite_hauteur);
                    var rect22 = new Rectangle(unite_largeur * 2, Yloc, unite_largeur * 3, unite_hauteur);
                    var rect33 = new Rectangle(5 * unite_largeur + 0, Yloc, unite_largeur * 6 + 16, unite_hauteur);
                    var rect44 = new Rectangle(11 * unite_largeur + 16, Yloc, unite_largeur * 1 + 0, unite_hauteur);
                    var rect55 = new Rectangle(12 * unite_largeur + 16, Yloc, unite_largeur * 9, unite_hauteur);
                    var rect66 = new Rectangle(21 * unite_largeur + 16, Yloc, unite_largeur * 3, unite_hauteur);
                    var rect77 = new Rectangle(24 * unite_largeur + 16, Yloc, unite_largeur * 3, unite_hauteur);
                    var rect88 = new Rectangle(27 * unite_largeur + 16, Yloc, unite_largeur * 3, unite_hauteur);
                    var rect99 = new Rectangle(30 * unite_largeur + 16, Yloc, unite_largeur * 3 + 16, unite_hauteur);

                    graphic.DrawRectangle(Pens.Black, rect11);
                    graphic.DrawRectangle(Pens.Black, rect22);
                    graphic.DrawRectangle(Pens.Black, rect33);
                    graphic.DrawRectangle(Pens.Black, rect44);
                    graphic.DrawRectangle(Pens.Black, rect55);
                    graphic.DrawRectangle(Pens.Black, rect66);
                    graphic.DrawRectangle(Pens.Black, rect77);
                    graphic.DrawRectangle(Pens.Black, rect88);
                    graphic.DrawRectangle(Pens.Black, rect99);
                    var numero = (i + 1);
                    var num = "";
                    if (numero < 10)
                    {
                        num = "00" + numero;
                    }
                    else
                    {
                        num = "0" + numero;
                    }
                    var banque = ConnectionClass.ListeBanques(liste[i].NomBanque);
                    if (banque[0].EtatParDefaut)
                    {
                        graphic.DrawString(liste[i].Compte, fnt1, Brushes.Black, rect33, drawFormatCenter);
                    }
                    else
                    {
                        var numeroCompte = banque[0].CodeBanque + "-" + banque[0].CodeGuichet + "-" + liste[i].Compte;
                        graphic.DrawString(numeroCompte, fnt1, Brushes.Black, rect33, drawFormatCenter);
                    }
                    graphic.DrawString(num.ToString(), fnt1, Brushes.Black, rect11, drawFormatRight);
                    graphic.DrawString(liste[i].NomEmploye.ToUpper(), fnt1, Brushes.Black, 13 * unite_largeur - 10, Yloc, drawFormatLeft);
                    graphic.DrawString(liste[i].NomBanque, fnt01, Brushes.Black, rect22, drawFormatCenter);
                    graphic.DrawString(liste[i].Cle, fnt1, Brushes.Black, rect44, drawFormatCenter);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", liste[i].NetAPayer), fnt1, Brushes.Black, rect99, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", liste[i].AutresPrimes), fnt1, Brushes.Black, unite_largeur * 27 + 10, Yloc, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", liste[i].PrimeMotivation), fnt1, Brushes.Black, unite_largeur * 24 + 10, Yloc, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", liste[i].FraisCommunication), fnt1, Brushes.Black, unite_largeur * 30 + 10, Yloc, drawFormatRight);
                }
                else if (i == liste.Count - 1)
                {
                    var rect66 = new Rectangle(21 * unite_largeur + 16, Yloc, unite_largeur * 3, 2 * unite_hauteur - 9);
                    var rect77 = new Rectangle(24 * unite_largeur + 16, Yloc, unite_largeur * 3, 2 * unite_hauteur - 9);
                    var rect88 = new Rectangle(27 * unite_largeur + 16, Yloc, unite_largeur * 3, 2 * unite_hauteur - 9);
                    var rect99 = new Rectangle(30 * unite_largeur + 16, Yloc, unite_largeur * 3 + 16, 2 * unite_hauteur - 9);

                    graphic.FillRectangle(Brushes.White, -2, Yloc + 0, unite_largeur * 25 + 2, unite_hauteur * 2 - 9);
                    graphic.DrawRectangle(Pens.Black, 22, Yloc + 0, unite_largeur * 21 - 6, unite_hauteur * 2 - 9);

                    graphic.DrawRectangle(Pens.Black, rect66);
                    graphic.DrawRectangle(Pens.Black, rect77);
                    graphic.DrawRectangle(Pens.Black, rect88);
                    graphic.DrawRectangle(Pens.Black, rect99);
                    graphic.DrawString(liste[i].NomEmploye, fnt11, Brushes.Black, unite_largeur, Yloc + 9);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", liste[i].NetAPayer), fnt11, Brushes.Black, 33 * unite_largeur + 22, Yloc + 9, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", liste[i].AutresPrimes), fnt11, Brushes.Black, unite_largeur * 27 + 10, Yloc + 9, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", liste[i].PrimeMotivation), fnt11, Brushes.Black, unite_largeur * 24 + 10, Yloc + 9, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", liste[i].FraisCommunication), fnt11, Brushes.Black, unite_largeur * 30 + 10, Yloc + 9, drawFormatRight);

                }
            }

            graphic.FillRectangle(Brushes.White, 0, 35 * unite_hauteur + 1, unite_largeur * 35, unite_hauteur * 15);
            if (liste.Count <= 2)
            {
                var index = unite_hauteur * 26 + liste.Count * unite_hauteur + 0;
                var paiement = ConnectionClass.ListeoOrdrePaiement(numeroPaiement);

                var ecart = hauteur_facture - (index + 2 * unite_hauteur + 10);
                if (ecart > 8 * unite_hauteur)
                {
                    var montantTotal = .0;
                    foreach (var paie in ConnectionClass.ListeDetailsPaiement(numeroPaiement))
                        montantTotal += paie.SalaireNet;
                    text = "Arrêté le présent état de virement à la somme de :  " + Impression.Converti((int)montantTotal) + "(  " + String.Format(elGR, "{0:0,0}", montantTotal) + ") Franc CFA";

                    justification = JustifyText.TextJustification.Center;
                    rect = new Rectangle(1 * 22, index + 15, 33 * unite_largeur + 20, 2 * unite_hauteur);
                    JustifyText.DrawParagraph(graphic, rect, fnt2, Brushes.Black, text, justification, 1.2f, 1.2f, 1.2f);

                    graphic.DrawString("L'Ordonnateur des Dépenses\nLe Directeur Général de l'INSEED ", fnt2, Brushes.Black, 22, index + 2 * unite_hauteur + 20);
                    graphic.DrawString("Le Contrôleur Financier Adjoint ", fnt2, Brushes.Black, 17 * unite_largeur + 10, index + 9 * unite_hauteur + 15, drawFormatCenter);
                    graphic.DrawString("Le liquidateur\nL' Agent Comptable de l'INSEED", fnt2, Brushes.Black, 33 * unite_largeur + 20, index + 2 * unite_hauteur + 20, drawFormatRight);

                    graphic.DrawString(paiement.Directeur, fnt22, Brushes.Black, 22, index + 9 * unite_hauteur + 15);
                    graphic.DrawString(paiement.Liquidateur, fnt22, Brushes.Black, 33 * unite_largeur + 20, index + 9 * unite_hauteur + 15, drawFormatRight);
                }
                else
                {
                }
            }

            return bitmap;
        }

        public static Bitmap OrdreDeVirementDesPrimes(List<Banque> liste, int start, int numeroPaiement)
        {
            #region
            int unite_hauteur = 22;
            int unite_largeur = 32;
            int largeur_facture = 35 * unite_largeur;
            int hauteur_facture = 36 * unite_hauteur;
            //creer un bit map
            Bitmap bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            #endregion
            Font fnt1 = new Font("Century Gothic", 10.5f, FontStyle.Regular);
            Font fnt0 = new Font("Century Gothic", 12.5f, FontStyle.Underline);
            Font fnt11 = new Font("Century Gothic", 10.45f, FontStyle.Bold);
            Font fnt4 = new Font("Century Gothic", 14, FontStyle.Bold | FontStyle.Underline);
            Font fnt00 = new Font("Century Gothic", 11.00f, FontStyle.Bold | FontStyle.Underline);
            Font fnt111 = new Font("Century Gothic", 11.00f, FontStyle.Bold);
            Font fnt3 = new Font("Century Gothic", 11.50f, FontStyle.Regular);
            var page = start + 1;
            var drawFormatCenter = new StringFormat();
            drawFormatCenter.Alignment = StringAlignment.Center;
            var drawFormatLeft = new StringFormat();
            drawFormatLeft.Alignment = StringAlignment.Near;
            var drawFormatRight = new StringFormat();
            drawFormatRight.Alignment = StringAlignment.Far;

            graphic.DrawRectangle(Pens.Black, 22, 1 * unite_hauteur, unite_largeur + 10, 2 * +unite_hauteur + 0);

            graphic.DrawRectangle(Pens.Black, unite_largeur * 2, 1 * unite_hauteur-5, unite_largeur * 3, 2 * +unite_hauteur + 5);
            graphic.DrawRectangle(Pens.Black, 5 * unite_largeur + 0, 1 * unite_hauteur - 5, unite_largeur * 6 + 16, 2 * unite_hauteur + 5);
            graphic.DrawRectangle(Pens.Black, 11 * unite_largeur + 16, 1 * unite_hauteur - 5, unite_largeur * 1 + 0, 2 * unite_hauteur + 5);
            graphic.DrawRectangle(Pens.Black, 12 * unite_largeur + 16, 1 * unite_hauteur - 5, unite_largeur * 9, 2 * unite_hauteur + 5);
            graphic.DrawRectangle(Pens.Black, 21 * unite_largeur + 16, 1 * unite_hauteur - 5, unite_largeur * 3, 2 * unite_hauteur + 5);
            graphic.DrawRectangle(Pens.Black, 24 * unite_largeur + 16, 1 * unite_hauteur - 5, unite_largeur * 3, unite_hauteur * 2 + 5);
            graphic.DrawRectangle(Pens.Black, 27 * unite_largeur + 16, 1 * unite_hauteur - 5, unite_largeur * 3, unite_hauteur * 2 + 5);
            graphic.DrawRectangle(Pens.Black, 30 * unite_largeur + 16, 1 * unite_hauteur - 5, unite_largeur * 3 + 16, unite_hauteur * 2+5);

            var rect1 = new RectangleF(22, 1 * unite_hauteur-5, unite_largeur + 10, 2 * +unite_hauteur + 5);
            var rect2 = new RectangleF(unite_largeur * 2, 1 * unite_hauteur - 5, unite_largeur * 3, 2 * +unite_hauteur + 5);
            var rect3 = new RectangleF(5 * unite_largeur + 0, 1 * unite_hauteur - 5, unite_largeur * 6 + 16, 2 * unite_hauteur + 5);
            var rect4 = new RectangleF(11 * unite_largeur + 16, 1 * unite_hauteur - 5, unite_largeur * 1 + 0, 2 * unite_hauteur + 5);
            var rect5 = new RectangleF(12 * unite_largeur + 16, 1 * unite_hauteur - 5, unite_largeur * 9, 2 * unite_hauteur + 5);
            var rect6 = new RectangleF(21 * unite_largeur + 16, 1 * unite_hauteur - 5, unite_largeur * 3, 2 * unite_hauteur + 5);
            var rect7 = new RectangleF(24 * unite_largeur + 16, 1 * unite_hauteur - 5, unite_largeur * 3, 2 * unite_hauteur + 5);
            var rect8 = new RectangleF(27 * unite_largeur + 16, 1 * unite_hauteur - 5, unite_largeur * 3, 2 * unite_hauteur + 5);
            var rect9 = new RectangleF(30 * unite_largeur + 16, 1 * unite_hauteur - 5, unite_largeur * 3 + 16, 2 * unite_hauteur + 5);

            graphic.DrawString("N°", fnt11, Brushes.Black, rect1, drawFormatCenter);
            graphic.DrawString("Banque".ToUpper(), fnt11, Brushes.Black, rect2, drawFormatCenter);
            graphic.DrawString("N° compte".ToUpper(), fnt11, Brushes.Black, rect3, drawFormatCenter);
            graphic.DrawString("Clé".ToUpper(), fnt11, Brushes.Black, rect4, drawFormatCenter);
            graphic.DrawString("Nom & Penom".ToUpper(), fnt11, Brushes.Black, rect5, drawFormatCenter);
            graphic.DrawString("PRIMES DE MOTIVATION".ToUpper(), fnt11, Brushes.Black, rect6, drawFormatCenter);
            graphic.DrawString("INDEMNITES AGENT COMPTABLE".ToUpper(), fnt11, Brushes.Black, rect7, drawFormatCenter);
            graphic.DrawString("FRAIS DE COM".ToUpper(), fnt11, Brushes.Black, rect8, drawFormatCenter);
            graphic.DrawString("MONTANT  TOTAL".ToUpper(), fnt11, Brushes.Black, rect9, drawFormatCenter);
            var j = 0;
            for (int i = start * 32 + 9; i <= liste.Count() - 1; i++)
            {
                int Yloc = unite_hauteur * j + 3 * unite_hauteur + 0;
                if (i < liste.Count - 1)
                {
                    var rect11 = new Rectangle(22, Yloc, unite_largeur + 10, unite_hauteur);
                    var rect22 = new Rectangle(unite_largeur * 2, Yloc, unite_largeur * 3, unite_hauteur);
                    var rect33 = new Rectangle(5 * unite_largeur + 0, Yloc, unite_largeur * 6 + 16, unite_hauteur);
                    var rect44 = new Rectangle(11 * unite_largeur + 16, Yloc, unite_largeur * 1 + 0, unite_hauteur);
                    var rect55 = new Rectangle(12 * unite_largeur + 16, Yloc, unite_largeur * 9, unite_hauteur);
                    var rect66 = new Rectangle(21 * unite_largeur + 16, Yloc, unite_largeur * 3, unite_hauteur);
                    var rect77 = new Rectangle(24 * unite_largeur + 16, Yloc, unite_largeur * 3, unite_hauteur);
                    var rect88 = new Rectangle(27 * unite_largeur + 16, Yloc, unite_largeur * 3, unite_hauteur);
                    var rect99 = new Rectangle(30 * unite_largeur + 16, Yloc, unite_largeur * 3 + 16, unite_hauteur);

                    graphic.DrawRectangle(Pens.Black, rect11);
                    graphic.DrawRectangle(Pens.Black, rect22);
                    graphic.DrawRectangle(Pens.Black, rect33);
                    graphic.DrawRectangle(Pens.Black, rect44);
                    graphic.DrawRectangle(Pens.Black, rect55);
                    graphic.DrawRectangle(Pens.Black, rect66);
                    graphic.DrawRectangle(Pens.Black, rect77);
                    graphic.DrawRectangle(Pens.Black, rect88);
                    graphic.DrawRectangle(Pens.Black, rect99);
                    var numero = (i + 1);
                    var num = "";
                    if (numero < 10)
                    {
                        num = "00" + numero;
                    }
                    else
                    {
                        num = "0" + numero;
                    }
                    var banque = ConnectionClass.ListeBanques(liste[i].NomBanque);
                    if (banque[0].EtatParDefaut)
                    {
                        graphic.DrawString(liste[i].Compte, fnt1, Brushes.Black, rect33, drawFormatCenter);
                    }
                    else
                    {
                        var numeroCompte = banque[0].CodeBanque + "-" + banque[0].CodeGuichet + "-" + liste[i].Compte;
                        graphic.DrawString(numeroCompte, fnt1, Brushes.Black, rect33, drawFormatCenter);
                    }
                    graphic.DrawString(num.ToString(), fnt1, Brushes.Black, rect11, drawFormatRight);
                    graphic.DrawString(liste[i].NomEmploye.ToUpper(), fnt1, Brushes.Black, 13 * unite_largeur - 10, Yloc, drawFormatLeft);
                    graphic.DrawString(liste[i].NomBanque, fnt1, Brushes.Black, rect22, drawFormatCenter);
                    graphic.DrawString(liste[i].Cle, fnt1, Brushes.Black, rect44, drawFormatCenter);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", liste[i].NetAPayer), fnt1, Brushes.Black, rect99, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", liste[i].AutresPrimes), fnt1, Brushes.Black, unite_largeur * 27 + 10, Yloc, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", liste[i].PrimeMotivation), fnt1, Brushes.Black, unite_largeur * 24 + 10, Yloc, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", liste[i].FraisCommunication), fnt1, Brushes.Black, unite_largeur * 30 + 10, Yloc, drawFormatRight);
                }
                else if (i == liste.Count - 1)
                {
                    var rect66 = new Rectangle(21 * unite_largeur + 16, Yloc, unite_largeur * 3, 2 * unite_hauteur - 9);
                    var rect77 = new Rectangle(24 * unite_largeur + 16, Yloc, unite_largeur * 3, 2 * unite_hauteur - 9);
                    var rect88 = new Rectangle(27 * unite_largeur + 16, Yloc, unite_largeur * 3, 2 * unite_hauteur - 9);
                    var rect99 = new Rectangle(30 * unite_largeur + 16, Yloc, unite_largeur * 3 + 16, 2 * unite_hauteur - 9);

                    graphic.FillRectangle(Brushes.White, -2, Yloc + 0, unite_largeur * 25 + 2, unite_hauteur * 2 - 9);
                    graphic.DrawRectangle(Pens.Black, 22, Yloc + 0, unite_largeur * 21 - 6, unite_hauteur * 2 - 9);

                    graphic.DrawRectangle(Pens.Black, rect66);
                    graphic.DrawRectangle(Pens.Black, rect77);
                    graphic.DrawRectangle(Pens.Black, rect88);
                    graphic.DrawRectangle(Pens.Black, rect99);
                    graphic.DrawString(liste[i].NomEmploye, fnt11, Brushes.Black, unite_largeur, Yloc + 9);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", liste[i].NetAPayer), fnt11, Brushes.Black, 33 * unite_largeur + 22, Yloc + 9, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", liste[i].AutresPrimes), fnt11, Brushes.Black, unite_largeur * 27 + 10, Yloc + 9, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}",  liste[i].PrimeMotivation), fnt11, Brushes.Black, unite_largeur * 24 + 10, Yloc + 9, drawFormatRight);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", liste[i].FraisCommunication), fnt11, Brushes.Black, unite_largeur * 30 + 10, Yloc + 9, drawFormatRight);

                }
                j++;
            }
            if (liste.Count <= (1 + start) * 32 + 9)
            {
                var index = unite_hauteur * 4 + j * unite_hauteur + 10;
                var paiement = ConnectionClass.ListeoOrdrePaiement(numeroPaiement);

                var ecart = hauteur_facture - (index + 2 * unite_hauteur + 10);
                if (ecart > 8 * unite_hauteur)
                {
                    var montantTotal = .0;
                    foreach (var paie in ConnectionClass.ListeDetailsPaiement(numeroPaiement))
                        montantTotal += paie.SalaireNet;
                    var text = "Arrêté le présent état de virement à la somme de :  " + Impression.Converti((int)montantTotal) + "(  " + String.Format(elGR, "{0:0,0}", montantTotal) + ") Franc CFA";

                    var justification = JustifyText.TextJustification.Center;
                    var rect = new Rectangle(1 * 22, index - 15, 33 * unite_largeur + 20, 2 * unite_hauteur);
                    JustifyText.DrawParagraph(graphic, rect, fnt3, Brushes.Black, text, justification, 1.2f, 1.2f, 1.2f);

                    graphic.DrawString("L'Ordonnateur des Dépenses\nLe Directeur Général de l'INSEED ", fnt3, Brushes.Black, 22, index + 1 * unite_hauteur + 5);
                    graphic.DrawString("Le Contrôleur Financier Adjoint ", fnt3, Brushes.Black, 17 * unite_largeur + 10, index + 8 * unite_hauteur - 5, drawFormatCenter);
                    graphic.DrawString("Le liquidateur\nL' Agent Comptable de l'INSEED", fnt3, Brushes.Black, 33 * unite_largeur + 20, index + 1 * unite_hauteur + 5, drawFormatRight);

                    graphic.DrawString(paiement.Directeur, fnt00, Brushes.Black, 22, index + 8 * unite_hauteur - 5);
                    graphic.DrawString(paiement.Liquidateur, fnt00, Brushes.Black, 33 * unite_largeur + 20, index + 8 * unite_hauteur - 5, drawFormatRight);
                }
                else
                {
                }
            }
            graphic.FillRectangle(Brushes.White, 0, 35 * unite_hauteur + 2, unite_largeur * 35, unite_hauteur * 15);

            return bitmap;
        }
    
	#endregion   
        public static Bitmap ExtraPageA4(int numeroPaiement)
        {
            #region
            int unite_hauteur = 22;
            int unite_largeur = 32;
            int largeur_facture = 26 * unite_largeur - 5;
            int hauteur_facture = 52 * unite_hauteur;
            //creer un bit map
            Bitmap bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            #endregion
         

            //la couleur de l'image
            graphic.Clear(Color.White);

            Font fnt00 = new Font("Century Gothic", 12, FontStyle.Regular);
            Font fnt11 = new Font("Century Gothic", 12, FontStyle.Bold | FontStyle.Underline);

            var drawFormatCenter = new StringFormat();
            drawFormatCenter.Alignment = StringAlignment.Center;
            var drawFormatLeft = new StringFormat();
            drawFormatLeft.Alignment = StringAlignment.Near;
            var drawFormatRight = new StringFormat();
            drawFormatRight.Alignment = StringAlignment.Far;
            var index = 0;

            var paiement = ConnectionClass.ListeoOrdrePaiement(numeroPaiement);
            graphic.DrawString("Le Directeur Général de l'INSEED ", fnt00, Brushes.Black, 15, index + 2 * unite_hauteur + 5);
            graphic.DrawString("Le Contrôleur Financier Adjoint ", fnt00, Brushes.Black, 13 * unite_largeur + 0, index + 2 * unite_hauteur + 5, drawFormatCenter);
            graphic.DrawString("Le liquidateur,\n L'Agent Comptable de l'INSEED", fnt00, Brushes.Black, 21 * unite_largeur + 0, index + 2 * unite_hauteur + 5, drawFormatCenter);

            graphic.DrawString(paiement.Directeur, fnt11, Brushes.Black, 15, index + 10 * unite_hauteur - 5);
            graphic.DrawString(paiement.Controleur, fnt11, Brushes.Black, 13 * unite_largeur + 0, index + 10 * unite_hauteur - 5, drawFormatCenter);
            graphic.DrawString(paiement.Liquidateur, fnt11, Brushes.Black, 21 * unite_largeur -0, index + 10 * unite_hauteur - 5, drawFormatCenter);
            return bitmap;
        }

        public static Bitmap ImprimerLalisteDesRetraitesPersonnels(DataGridView dtListePersonnel, string titreImpression, int start)
        {
            #region
            int unite_hauteur = 23;
            int unite_largeur = 32;
            int largeur_facture = 25 * unite_largeur + 10;
            int detail_hauteur_facture = 10 * unite_hauteur;
            int hauteur_facture = 38 * unite_hauteur + detail_hauteur_facture;

            //creer un bit map
            Bitmap bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            #endregion
            try
            {
                Image logo = global::SGSP.Properties.Resources.Logo;
                graphic.DrawImage(logo, unite_largeur, unite_hauteur + 5, 24 * unite_largeur, 6 * unite_hauteur);
            }
            catch { } //definir les polices 
            Font fnt1 = new Font("Arial Unicode MS", 9, FontStyle.Italic);
            Font fnt0 = new Font("Arial Unicode MS", 9, FontStyle.Regular);
            Font fnt11 = new Font("Arial Unicode MS", 12, FontStyle.Bold);
            Font fnt3 = new Font("Arial Unicode MS", 18, FontStyle.Bold);
            Font fnt33 = new Font("Arial Unicode MS", 12, FontStyle.Regular);

            graphic.DrawString("Page " + (start + 1), fnt33, Brushes.Black, 16, 10);
            graphic.DrawString(titreImpression, fnt11, Brushes.Black, 10, 10 * unite_hauteur);
            graphic.DrawString("Matricule", fnt33, Brushes.Black, 16, 12 * unite_hauteur);
            graphic.DrawString("Nom", fnt33, Brushes.Black, 6 * unite_largeur - 15, 12 * unite_hauteur + 3);
            graphic.DrawString("Prenom", fnt33, Brushes.Black, 11 * unite_largeur, 12 * unite_hauteur + 3);
            graphic.DrawString("Fonction", fnt33, Brushes.Black, 16 * unite_largeur + 15, 12 * unite_hauteur + 3);
            graphic.DrawString("Date", fnt33, Brushes.Black, 22 * unite_largeur, 12 * unite_hauteur + 3);

            graphic.DrawRectangle(Pens.Black, 15, 12 * unite_hauteur, unite_largeur * 2 + 14, unite_hauteur);
            graphic.DrawRectangle(Pens.Black, unite_largeur * 3, 12 * unite_hauteur, unite_largeur * 6 - 3, unite_hauteur);
            graphic.DrawRectangle(Pens.Black, unite_largeur * 9, 12 * unite_hauteur, unite_largeur * 6 - 3, unite_hauteur);
            graphic.DrawRectangle(Pens.Black, unite_largeur * 15, 12 * unite_hauteur, unite_largeur * 6 - 3, unite_hauteur);
            graphic.DrawRectangle(Pens.Black, unite_largeur * 21, 12 * unite_hauteur, unite_largeur * 4 - 3, unite_hauteur);


            for (int i = start * 30; i <= dtListePersonnel.Rows.Count - 1; i++)
            {
                int Yloc = unite_hauteur * i + 13 * unite_hauteur;

                graphic.DrawRectangle(Pens.Black, 15, Yloc, unite_largeur * 2 + 14, unite_hauteur);
                graphic.DrawRectangle(Pens.Black, unite_largeur * 3, Yloc, unite_largeur * 6 - 3, unite_hauteur);
                graphic.DrawRectangle(Pens.Black, unite_largeur * 9, Yloc, unite_largeur * 6 - 3, unite_hauteur);
                graphic.DrawRectangle(Pens.Black, unite_largeur * 15, Yloc, unite_largeur * 6 - 3, unite_hauteur);
                graphic.DrawRectangle(Pens.Black, unite_largeur * 21, Yloc, unite_largeur * 4 - 3, unite_hauteur);


                graphic.DrawString(dtListePersonnel.Rows[i].Cells[0].Value.ToString(), fnt33, Brushes.Black, 16, Yloc);
                graphic.DrawString(dtListePersonnel.Rows[i].Cells[1].Value.ToString(), fnt33, Brushes.Black, 3 * unite_largeur + 3, Yloc);
                graphic.DrawString(dtListePersonnel.Rows[i].Cells[2].Value.ToString(), fnt33, Brushes.Black, 9 * unite_largeur + 3, Yloc + 3);
                graphic.DrawString(dtListePersonnel.Rows[i].Cells[3].Value.ToString(), fnt33, Brushes.Black, 15 * unite_largeur + 3, Yloc + 3);
                graphic.DrawString(dtListePersonnel.Rows[i].Cells[7].Value.ToString(), fnt33, Brushes.Black, 21 * unite_largeur + 3, Yloc + 3);

            }
            graphic.FillRectangle(Brushes.White, unite_largeur * 22, 44 * unite_hauteur, unite_largeur * 3 - 3, unite_hauteur);

            return bitmap;
        }


        public static Bitmap ImprimerLalisteDesStagiaires(DataGridView dtListePersonnel, string titreImpression, int start)
        {
            #region
            int unite_hauteur = 20;
            int unite_largeur = 32;
            int largeur_facture = 24 * unite_largeur + 10;
            int hauteur_facture = 52 * unite_hauteur;

            //creer un bit map
            Bitmap bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            #endregion
            try
            {
                Image logo = global::SGSP.Properties.Resources.Logo;
                graphic.DrawImage(logo, unite_largeur, unite_hauteur + 5, 24 * unite_largeur, 6 * unite_hauteur);
            }
            catch { } //definir les polices 
            Font fnt1 = new Font("Arial Narrow", 10, FontStyle.Italic);
            Font fnt0 = new Font("Arial Narrow", 11, FontStyle.Regular);
            Font fnt11 = new Font("Arial Narrow", 11, FontStyle.Bold);
            Font fnt3 = new Font("Arial Unicode MS", 18, FontStyle.Bold);
            Font fnt33 = new Font("Arial Unicode MS", 12, FontStyle.Regular);

            graphic.DrawString("Page " + (start + 1), fnt33, Brushes.Black, 16, 10);
            graphic.DrawString(titreImpression, fnt11, Brushes.Black, 10, 10 * unite_hauteur);
            graphic.DrawString("Matricule", fnt11, Brushes.Black, 16, 12 * unite_hauteur+1);
            graphic.DrawString("Nom & prenom", fnt11, Brushes.Black, 3 * unite_largeur + 5, 12 * unite_hauteur + 1);
            graphic.DrawString("Nature de stage", fnt11, Brushes.Black, 12 * unite_largeur + 5, 12 * unite_hauteur + 1);
            graphic.DrawString("Debut", fnt11, Brushes.Black, 18 * unite_largeur + 5, 12 * unite_hauteur + 1);
            graphic.DrawString("Fin", fnt11, Brushes.Black, 21 * unite_largeur + 5, 12 * unite_hauteur + 1);

            graphic.DrawRectangle(Pens.Black, 15, 12 * unite_hauteur, unite_largeur * 2 + 14, unite_hauteur);
            graphic.DrawRectangle(Pens.Black, unite_largeur * 3, 12 * unite_hauteur, unite_largeur * 9 - 3, unite_hauteur);
            graphic.DrawRectangle(Pens.Black, unite_largeur * 12, 12 * unite_hauteur, unite_largeur * 6 - 3, unite_hauteur);
            graphic.DrawRectangle(Pens.Black, unite_largeur * 18, 12 * unite_hauteur, unite_largeur * 3 - 3, unite_hauteur);
            graphic.DrawRectangle(Pens.Black, unite_largeur * 21, 12 * unite_hauteur, unite_largeur * 3 - 3, unite_hauteur);


            for (int i = start * 30; i <= dtListePersonnel.Rows.Count - 1; i++)
            {
                int Yloc = unite_hauteur * i + 13 * unite_hauteur;

                graphic.DrawRectangle(Pens.Black, 15, Yloc, unite_largeur * 2 + 14, unite_hauteur);
                graphic.DrawRectangle(Pens.Black, unite_largeur * 3, Yloc, unite_largeur * 9 - 3, unite_hauteur);
                graphic.DrawRectangle(Pens.Black, unite_largeur * 12, Yloc, unite_largeur * 6 - 3, unite_hauteur);
                graphic.DrawRectangle(Pens.Black, unite_largeur * 18, Yloc, unite_largeur * 3 - 3, unite_hauteur);
                graphic.DrawRectangle(Pens.Black, unite_largeur * 21, Yloc, unite_largeur * 3 - 3, unite_hauteur);
                
                var liste = ConnectionClass.ListeDesStages(Convert.ToInt32(dtListePersonnel.Rows[i].Cells[0].Value.ToString()));
                var natureStage = "";
                var dateDebut = "";
                var dateFin = "";
                if (liste.Count > 0)
                {
                    natureStage = liste[0].NatureStage;
                    dateDebut = liste[0].DateDebut.ToShortDateString();
                    dateFin = liste[0].DateFin.ToShortDateString();
                }
                graphic.DrawString(dtListePersonnel.Rows[i].Cells[1].Value.ToString(), fnt0, Brushes.Black, 16, Yloc+1);
                graphic.DrawString(dtListePersonnel.Rows[i].Cells[2].Value.ToString(), fnt0, Brushes.Black, 3 * unite_largeur + 5, Yloc+1);
                graphic.DrawString(natureStage, fnt0, Brushes.Black, 12 * unite_largeur + 5, Yloc + 1);
                graphic.DrawString(dateDebut, fnt0, Brushes.Black, 18 * unite_largeur + 5, Yloc + 1);
                graphic.DrawString(dateFin, fnt0, Brushes.Black, 21 * unite_largeur + 5, Yloc + 1);

            }
            graphic.FillRectangle(Brushes.White, unite_largeur * 22, 44 * unite_hauteur, unite_largeur * 3 - 3, unite_hauteur);

            return bitmap;
        }

        #region RapportDepenses
        //fonction pour dessiner rapport depenses
        public static Bitmap ImprimerRapportDepenses(DataGridView dataGridView,string titre , int start)
        {
            //les dimension de la facture
            #region
            int unite_hauteur = 18;
            int unite_largeur = 32;
            int largeur_facture = 26 * unite_largeur;
            int hauteur_facture = 56 * unite_hauteur ;
            
            Bitmap bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            #endregion
            try
            {
                Image logo = global::SGSP.Properties.Resources.Logo;
                graphic.DrawImage(logo, unite_largeur, 10, 12 * unite_largeur,4 * unite_hauteur);
            }
            catch { } //definir les polices 
            Font fnt1 = new Font("Arial Unicode MS", 10, FontStyle.Regular);
            Font fnt3 = new Font("Arial Unicode MS", 13, FontStyle.Bold);
            Font fnt33 = new Font("Arial Unicode MS", 9, FontStyle.Bold);
            graphic.DrawString("Page "+(1+start ).ToString(), fnt1, Brushes.Black, 22*unite_largeur,5);

            graphic.DrawString(titre , fnt3, Brushes.Black, unite_largeur, 4 * unite_hauteur + 10);

            graphic.FillRectangle(Brushes.Lavender, 15, 6 * unite_hauteur - 4, 23 * unite_largeur, unite_hauteur);
            graphic.DrawString("Date", fnt33, Brushes.Black, unite_largeur, 6 * unite_hauteur - 4);
            graphic.DrawString("Code", fnt33, Brushes.Black, 3*unite_largeur, 6 * unite_hauteur - 4);
            graphic.DrawString("Libellé", fnt33, Brushes.Black, 4 * unite_largeur +10, 6 * unite_hauteur - 4);
            graphic.DrawString("Bénéficiaire", fnt33, Brushes.Black, 16 * unite_largeur, 6 * unite_hauteur - 4);
            graphic.DrawString("Montant", fnt33, Brushes.Black, 21 * unite_largeur, 6 * unite_hauteur-4);
            var j = 0;
            for (int i = 45* start; i <= dataGridView.Rows.Count - 1; i++)
            {
                int Yloc = unite_hauteur * j + 7 * unite_hauteur;

                
                 if (!string.IsNullOrEmpty(dataGridView.Rows[i].Cells[1].Value.ToString()))
                {
                    graphic.FillRectangle(Brushes.Lavender, 15, Yloc, 23 * unite_largeur, unite_hauteur-2);
                    graphic.DrawString(dataGridView.Rows[i].Cells[1].Value.ToString(), fnt33, Brushes.Black, 3*unite_largeur+5, Yloc);
                     graphic.DrawString(dataGridView.Rows[i].Cells[9].Value.ToString(), fnt33, Brushes.Black, 21 * unite_largeur, Yloc);

                }
                else
                {
                    graphic.DrawString(dataGridView.Rows[i].Cells[6].Value.ToString() , fnt1, Brushes.Black, 15, Yloc);
                    graphic.DrawString(dataGridView.Rows[i].Cells[4].Value.ToString(), fnt1, Brushes.Black, 3*unite_largeur+5, Yloc);
                    graphic.DrawString(dataGridView.Rows[i].Cells[5].Value.ToString(), fnt1, Brushes.Black, 4 * unite_largeur +20, Yloc);
                    graphic.DrawString(dataGridView.Rows[i].Cells[8].Value.ToString(), fnt1, Brushes.Black, 16 * unite_largeur, Yloc);
                    graphic.DrawString(dataGridView.Rows[i].Cells[9].Value.ToString(), fnt1, Brushes.Black, 21 * unite_largeur, Yloc);
                }
                j++;
            }
            //if (dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[2].Value.ToString() == "Total")
            //{
            //    int Yloc = unite_hauteur * dataGridView.Rows.Count + 10 * unite_hauteur;
            //    graphic.FillRectangle(Brushes.White, unite_largeur, Yloc, 24 * unite_largeur, 3 * unite_hauteur);
            //    graphic.FillRectangle(Brushes.Black, unite_largeur, Yloc, 23 * unite_largeur, unite_hauteur);
            //    graphic.DrawString(dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[2].Value.ToString(), fnt33, Brushes.White, unite_largeur, Yloc);
            //    graphic.DrawString(dataGridView.Rows[dataGridView.Rows.Count - 1].Cells[6].Value.ToString(), fnt33, Brushes.White, 21 * unite_largeur + 15, Yloc);
                
            //}
           
                graphic.FillRectangle(Brushes.White, 12, 52 * unite_hauteur, 24 * unite_largeur, 10 * unite_hauteur);
            
            return bitmap;
        }

        public static Bitmap ImprimerRapporRecettes(DataGridView dataGridView, string titre, int start)
        {
            //les dimension de la facture
            #region
            int unite_hauteur = 18;
            int unite_largeur = 32;
            int largeur_facture = 26 * unite_largeur;
            int hauteur_facture = 56 * unite_hauteur;

            Bitmap bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            #endregion
            try
            {
                Image logo = global::SGSP.Properties.Resources.Logo;
                graphic.DrawImage(logo, unite_largeur, 10, 12 * unite_largeur, 4 * unite_hauteur);
            }
            catch { } //definir les polices 
            Font fnt1 = new Font("Arial Unicode MS", 10, FontStyle.Regular);
            Font fnt3 = new Font("Arial Unicode MS", 13, FontStyle.Bold);
            Font fnt33 = new Font("Arial Unicode MS", 9, FontStyle.Bold);
            graphic.DrawString("Page " + (1 + start).ToString(), fnt1, Brushes.Black, 22 * unite_largeur, 5);

            graphic.DrawString(titre, fnt3, Brushes.Black, unite_largeur, 4 * unite_hauteur + 10);

            graphic.FillRectangle(Brushes.Lavender, 15, 6 * unite_hauteur - 4, 23 * unite_largeur, unite_hauteur);
            graphic.DrawString("Date", fnt33, Brushes.Black, unite_largeur, 6 * unite_hauteur - 4);
            graphic.DrawString("Code", fnt33, Brushes.Black, 3 * unite_largeur, 6 * unite_hauteur - 4);
            graphic.DrawString("Libellé", fnt33, Brushes.Black, 4 * unite_largeur + 10, 6 * unite_hauteur - 4);
            //graphic.DrawString("Bénéficiaire", fnt33, Brushes.Black, 16 * unite_largeur, 6 * unite_hauteur - 4);
            graphic.DrawString("Montant", fnt33, Brushes.Black, 20 * unite_largeur, 6 * unite_hauteur - 4);
            var j = 0;
            for (int i = 45 * start; i <= dataGridView.Rows.Count - 1; i++)
            {
                int Yloc = unite_hauteur * j + 7 * unite_hauteur;


                if (!string.IsNullOrEmpty(dataGridView.Rows[i].Cells[1].Value.ToString()))
                {
                    graphic.FillRectangle(Brushes.Lavender, 15, Yloc, 23 * unite_largeur, unite_hauteur - 2);
                    graphic.DrawString(dataGridView.Rows[i].Cells[1].Value.ToString(), fnt33, Brushes.Black, 3 * unite_largeur + 5, Yloc);
                    graphic.DrawString(dataGridView.Rows[i].Cells[7].Value.ToString(), fnt33, Brushes.Black, 20 * unite_largeur, Yloc);

                }
                else
                {
                    graphic.DrawString(dataGridView.Rows[i].Cells[6].Value.ToString(), fnt1, Brushes.Black, 15, Yloc);
                    graphic.DrawString(dataGridView.Rows[i].Cells[2].Value.ToString(), fnt1, Brushes.Black, 3 * unite_largeur + 5, Yloc);
                    graphic.DrawString(dataGridView.Rows[i].Cells[3].Value.ToString(), fnt1, Brushes.Black, 4 * unite_largeur + 20, Yloc);
                    //graphic.DrawString(dataGridView.Rows[i].Cells[8].Value.ToString(), fnt1, Brushes.Black, 16 * unite_largeur, Yloc);
                    graphic.DrawString(dataGridView.Rows[i].Cells[7].Value.ToString(), fnt1, Brushes.Black, 20 * unite_largeur, Yloc);
                }
                j++;
            }
            graphic.FillRectangle(Brushes.White, 12, 52 * unite_hauteur, 24 * unite_largeur, 10 * unite_hauteur);

            return bitmap;
        }

        public static Bitmap ImprimerRapportJournalCaisse(DataGridView dataGridView, string titre, int start)
        {
            //les dimension de la facture
            #region
            int unite_hauteur = 18;
            int unite_largeur = 32;
            int largeur_facture = 26 * unite_largeur;
            int hauteur_facture = 56 * unite_hauteur;

            Bitmap bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            #endregion
            try
            {
                Image logo = global::SGSP.Properties.Resources.Logo;
                graphic.DrawImage(logo, unite_largeur, 10, 12 * unite_largeur, 3 * unite_hauteur);
            }
            catch { } //definir les polices 
            Font fnt1 = new Font("Arial Unicode MS", 10, FontStyle.Regular);
            Font fnt3 = new Font("Arial Unicode MS", 13, FontStyle.Bold);
            Font fnt33 = new Font("Arial Unicode MS", 10, FontStyle.Bold);
            graphic.DrawString("Page " + (1 + start).ToString(), fnt1, Brushes.Black, 22 * unite_largeur, 5);

            graphic.DrawString(titre, fnt3, Brushes.Black, unite_largeur, 3* unite_hauteur + 5);

            graphic.FillRectangle(Brushes.Lavender, 5, 5* unite_hauteur - 4, 23 * unite_largeur+17, unite_hauteur);
            graphic.DrawString("Date", fnt33, Brushes.Black, 10, 5 * unite_hauteur - 4);
            graphic.DrawString("Code", fnt33, Brushes.Black, 3 * unite_largeur-10, 5 * unite_hauteur - 4);
            graphic.DrawString("Libellé", fnt33, Brushes.Black, 4 * unite_largeur + 0, 5 * unite_hauteur - 4);
            graphic.DrawString("Caissier", fnt33, Brushes.Black, 15 * unite_largeur, 5 * unite_hauteur - 4);
            graphic.DrawString("Montant", fnt33, Brushes.Black, 21 * unite_largeur+10, 5* unite_hauteur - 4);
            var j = 0;
            for (int i = 48 * start; i <= dataGridView.Rows.Count - 1; i++)
            {
                int Yloc = unite_hauteur * j + 6 * unite_hauteur;

                var libelle = dataGridView.Rows[i].Cells[7].Value.ToString();
                if(libelle.Length>53)
                {
                    libelle = libelle.Substring(0, 53)+"...";
                }
                if (dataGridView.Rows[i].Cells[3].Value.ToString().Contains("Total + avoir"))
                {
                    graphic.DrawString(dataGridView.Rows[i].Cells[3].Value.ToString(), fnt33, Brushes.Black, 15 , Yloc);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", double.Parse(dataGridView.Rows[i].Cells[8].Value.ToString())), fnt33, Brushes.Black, 21 * unite_largeur + 15, Yloc);

                }
                else
                {
                    var total = double.Parse(dataGridView.Rows[i].Cells[8].Value.ToString()) + double.Parse(dataGridView.Rows[i].Cells[9].Value.ToString());
                    graphic.DrawString(dataGridView.Rows[i].Cells[3].Value.ToString(), fnt1, Brushes.Black, 15, Yloc);
                    graphic.DrawString(dataGridView.Rows[i].Cells[6].Value.ToString(), fnt1, Brushes.Black, 3 * unite_largeur - 10, Yloc);
                    graphic.DrawString(libelle, fnt1, Brushes.Black, 4 * unite_largeur + 8, Yloc);
                    graphic.DrawString(dataGridView.Rows[i].Cells[5].Value.ToString(), fnt1, Brushes.Black, 15 * unite_largeur, Yloc);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}",total), fnt1, Brushes.Black, 21 * unite_largeur + 15, Yloc);
                }
                j++;
            }
            graphic.FillRectangle(Brushes.White, 12, 54 * unite_hauteur, 24 * unite_largeur, 10 * unite_hauteur);

            return bitmap;
        }


        public static Bitmap ImprimerJournalCaisse(DataGridView dataGridView, string titre, int start)
        {
            //les dimension de la facture
            #region
            int unite_hauteur = 18;
            int unite_largeur = 32;
            int largeur_facture = 26 * unite_largeur;
            int hauteur_facture = 56 * unite_hauteur;

            Bitmap bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            #endregion
            try
            {
                Image logo = global::SGSP.Properties.Resources.Logo;
                graphic.DrawImage(logo, unite_largeur, 10, 12 * unite_largeur, 3 * unite_hauteur);
            }
            catch { } //definir les polices 
            Font fnt1 = new Font("Arial Unicode MS", 10, FontStyle.Regular);
            Font fnt3 = new Font("Arial Unicode MS", 13, FontStyle.Bold);
            Font fnt33 = new Font("Arial Unicode MS", 10, FontStyle.Bold);
            graphic.DrawString("Page " + (1 + start).ToString(), fnt1, Brushes.Black, 22 * unite_largeur, 5);

            graphic.DrawString(titre, fnt3, Brushes.Black, 5*unite_largeur, 3 * unite_hauteur + 5);

            graphic.FillRectangle(Brushes.Lavender, 2*unite_largeur, 5 * unite_hauteur - 4, 20 * unite_largeur , unite_hauteur);
            graphic.DrawString("Date", fnt33, Brushes.Black, 3*unite_largeur, 5 * unite_hauteur - 4);
            graphic.DrawString("Montant ", fnt33, Brushes.Black, 8 * unite_largeur, 5 * unite_hauteur - 4);
            graphic.DrawString("Depense ", fnt33, Brushes.Black, 13 * unite_largeur + 10, 5 * unite_hauteur - 4);
            graphic.DrawString("Solde ", fnt33, Brushes.Black, 18 * unite_largeur + 10, 5 * unite_hauteur - 4);
            var j = 0;
            for (int i = 48 * start; i <= dataGridView.Rows.Count - 1; i++)
            {
                int Yloc = unite_hauteur * j + 6 * unite_hauteur;
                graphic.DrawRectangle(Pens.Black, 2 * unite_largeur, Yloc , 5 * unite_largeur -3, unite_hauteur);
                graphic.DrawRectangle(Pens.Black, 7 * unite_largeur, Yloc , 5 * unite_largeur-3 , unite_hauteur);
                graphic.DrawRectangle(Pens.Black, 12* unite_largeur, Yloc , 5 * unite_largeur-3, unite_hauteur);
                graphic.DrawRectangle(Pens.Black, 17 * unite_largeur, Yloc, 5 * unite_largeur, unite_hauteur);
                if (i== dataGridView.Rows.Count-1)
                {
                    graphic.DrawString(dataGridView.Rows[i].Cells[0].Value.ToString(), fnt33, Brushes.Black, 3 * unite_largeur, Yloc);
                    graphic.DrawString(dataGridView.Rows[i].Cells[1].Value.ToString(), fnt33, Brushes.Black, 8 * unite_largeur, Yloc);
                    graphic.DrawString(dataGridView.Rows[i].Cells[2].Value.ToString(), fnt33, Brushes.Black, 13 * unite_largeur, Yloc);
                    graphic.DrawString(dataGridView.Rows[i].Cells[3].Value.ToString(), fnt33, Brushes.Black, 18 * unite_largeur, Yloc);
                }
                else
                {
                    graphic.DrawString(dataGridView.Rows[i].Cells[0].Value.ToString(), fnt1, Brushes.Black, 3*unite_largeur, Yloc);
                    graphic.DrawString(dataGridView.Rows[i].Cells[1].Value.ToString(), fnt1, Brushes.Black, 8 * unite_largeur , Yloc);
                    graphic.DrawString(dataGridView.Rows[i].Cells[2].Value.ToString(), fnt1, Brushes.Black, 13 * unite_largeur, Yloc);
                    graphic.DrawString(dataGridView.Rows[i].Cells[3].Value.ToString(), fnt1, Brushes.Black, 18 * unite_largeur, Yloc);
                }
                j++;
            }
            graphic.FillRectangle(Brushes.White, 5, 54 * unite_hauteur+1, 24 * unite_largeur, 10 * unite_hauteur);

            return bitmap;
        }

        #endregion

        //imprimer l'ordre de paiement
        public static Bitmap ImprimerListedocument(DataGridView dgvView, string titre)
        {
            #region facture1
            //les dimension de la facture
            int unite_hauteur = 23;
            int unite_largeur = 32;
            int largeur_facture = 35 * unite_largeur;
            int hauteur_facture = 32 * unite_hauteur + 7;//+ 15 + dtGrid.Rows.Count * unite_hauteur;


            //creer un bit map
            var bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            try
            {
                Image logo = global::SGSP.Properties.Resources.Logo;
                graphic.DrawImage(logo, unite_largeur, unite_hauteur + 5, 34 * unite_largeur, 7 * unite_hauteur);
            }
            catch { }
            //definir les polices 
            Font fnt1 = new Font("Century Gothic", 11, FontStyle.Regular);
            Font fnt33 = new Font("Century Gothic", 12, FontStyle.Regular);
            Font fnt0 = new Font("Century Gothic", 11, FontStyle.Bold);
            Font fnt11 = new Font("Century Gothic", 13, FontStyle.Bold);
            Font fnt3 = new Font("Century Gothic", 18, FontStyle.Underline);
            Font fnt22 = new Font("Century Gothic", 12, FontStyle.Regular);

            #endregion


            //graphic.FillRectangle(Brushes.SlateGray,10* unite_largeur, 9 * unite_hauteur + 15, unite_largeur * 15, unite_hauteur + 8);
            graphic.DrawString(titre, fnt11, Brushes.Black, unite_largeur * 8, 9 * unite_hauteur + 18);

            graphic.FillRectangle(Brushes.SteelBlue, unite_largeur, 12 * unite_hauteur-3, unite_largeur * 35,  unite_hauteur+3);

            graphic.DrawString("N°", fnt0, Brushes.White, unite_largeur + 2, 12 * unite_hauteur + 1);
            graphic.DrawString("EXERCICE", fnt0, Brushes.White, 3 * unite_largeur, 12 * unite_hauteur + 1);
            graphic.DrawString("REFERENCE", fnt0, Brushes.White, 6 * unite_largeur , 12 * unite_hauteur + 1);
            graphic.DrawString("EMIS LE", fnt0, Brushes.White, 11 * unite_largeur -5, 12 * unite_hauteur + 1);
            graphic.DrawString("ENREGISTRER LE", fnt0, Brushes.White, 14 * unite_largeur -13, 12 * unite_hauteur + 1);
            graphic.DrawString("TYPE DOCUMENT", fnt0, Brushes.White, 17 * unite_largeur +10, 12 * unite_hauteur + 1);
            graphic.DrawString("TOTAL HT", fnt0, Brushes.White, 22 * unite_largeur , 12 * unite_hauteur + 1);
            graphic.DrawString("TVA", fnt0, Brushes.White, 25 * unite_largeur , 12 * unite_hauteur + 1);
            graphic.DrawString("TOTAL TTC", fnt0, Brushes.White, 27 * unite_largeur , 12 * unite_hauteur + 1);
            graphic.DrawString("DOCUMENT DE", fnt0, Brushes.White, 30 * unite_largeur , 12 * unite_hauteur + 1);
            //graphic.DrawString("DESTINE A", fnt0, Brushes.White, 24 * unite_largeur - 40, 12 * unite_hauteur + 1);
          
            
            bool flag;
            if (dgvView.Rows.Count <= 18)
            {
                flag = true;
            }
            else
            {
                flag = false;
            }

            var j = 0;
            graphic.FillRectangle(Brushes.LightYellow, unite_largeur, 13 * unite_hauteur +1, unite_largeur * 2, 18 * unite_hauteur);
            graphic.FillRectangle(Brushes.SteelBlue, unite_largeur*6, 13 * unite_hauteur + 1, unite_largeur * 5-5, 18 * unite_hauteur);
            graphic.FillRectangle(Brushes.Green, unite_largeur * 17-3, 13 * unite_hauteur + 1, unite_largeur * 5 +3, 18 * unite_hauteur);
            graphic.FillRectangle(Brushes.Yellow, unite_largeur * 27 - 3, 13 * unite_hauteur + 1, unite_largeur * 3+3, 18 * unite_hauteur);
            graphic.FillRectangle(Brushes.OrangeRed, unite_largeur * 30, 13 * unite_hauteur + 1, unite_largeur * 5, 18 * unite_hauteur);
          
            for (var i = 0; i < dgvView.Rows.Count; i++)
            {
                var YLOC = unite_hauteur * 13 + 3 + unite_hauteur * j;
                graphic.DrawString(dgvView.Rows[i].Cells[0].Value.ToString(), fnt1, Brushes.Black, unite_largeur + 5, YLOC);
                graphic.DrawString(dgvView.Rows[i].Cells[1].Value.ToString(), fnt1, Brushes.Black, unite_largeur * 3, YLOC);
                graphic.DrawString(dgvView.Rows[i].Cells[2].Value.ToString(), fnt1, Brushes.White, unite_largeur * 6, YLOC);
                graphic.DrawString(dgvView.Rows[i].Cells[3].Value.ToString(), fnt1, Brushes.Black, unite_largeur * 11-5, YLOC);
                graphic.DrawString(DateTime.Parse(dgvView.Rows[i].Cells[4].Value.ToString()).ToShortDateString(), fnt1, Brushes.Black, unite_largeur * 14, YLOC);
                graphic.DrawString(dgvView.Rows[i].Cells[5].Value.ToString(), fnt1, Brushes.White, unite_largeur * 17, YLOC);
                graphic.DrawString(dgvView.Rows[i].Cells[6].Value.ToString(), fnt1, Brushes.Black, unite_largeur * 22, YLOC);
                graphic.DrawString(dgvView.Rows[i].Cells[7].Value.ToString(), fnt1, Brushes.Black, unite_largeur * 25, YLOC);
                graphic.DrawString(dgvView.Rows[i].Cells[8].Value.ToString(), fnt1, Brushes.Black, unite_largeur *27, YLOC);
                graphic.DrawString(dgvView.Rows[i].Cells[11].Value.ToString(), fnt1, Brushes.White, unite_largeur * 30, YLOC);
                j++;
            }

            if (flag)
            {
                //var LOC = unite_hauteur * 14 + +unite_hauteur * p.Count;
                //graphic.FillRectangle(Brushes.White, unite_largeur, 31 * unite_hauteur - 3, unite_largeur * 35, 3 * unite_hauteur);
                //graphic.FillRectangle(Brushes.SlateGray, unite_largeur, LOC, unite_largeur * 35, unite_hauteur);
                //graphic.DrawString("TOTAL", fnt11, Brushes.White, unite_largeur + 2, LOC);
           }
            return bitmap;
        }
    
        public static Bitmap RecuDePaiement( Encaissement  reglement)
        {
            try
            {
                
                //les dimension de la facture
                int unite_hauteur = 20;
                int unite_largeur = 32;
                int largeur_facture = 24 * unite_largeur + 10;
                int detail_hauteur_facture = 14 * unite_hauteur;
                int hauteur_facture = 43 * unite_hauteur + detail_hauteur_facture;
                var hauteurFacture2 = 29 * unite_hauteur;
                //creer un bit map
                Bitmap bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                // creer un objet graphic
                Graphics graphic = Graphics.FromImage(bitmap);

                //la couleur de l'image
                graphic.Clear(Color.White);
                #region facture1

              
                //definir les polices 
                Font fnt1 = new Font("Arial Narrow", 12F, FontStyle.Regular);
                Font fnt11 = new Font("Arial Narrow", 12, FontStyle.Bold);
                Font fnt0 = new Font("Arial Narrow", 13, FontStyle.Bold);
                Font fnt2 = new Font("Bodoni MT", 22, FontStyle.Bold);
                Font fnt3 = new Font("Californian FB", 16, FontStyle.Bold);
                Font fnt4 = new Font("Bradley Hand ITC", 17, FontStyle.Bold);
                              
                //graphic.DrawString("MED PHARMA", fnt2, Brushes.White, 15 * unite_largeur + 10, 2 * unite_hauteur);
                #region FACTURE1
                graphic.DrawRectangle(Pens.Black, 10, 0, 24 * unite_largeur, 27 * unite_hauteur - 10);
                graphic.DrawString("REÇU OFFICIEL", fnt3, Brushes.Black, 10, unite_hauteur);
                graphic.DrawLine(Pens.Black, 10, 2 * unite_hauteur + 10, 16 * unite_largeur - 20, 2 * unite_hauteur + 10);
                graphic.DrawRectangle(Pens.Black, 16 * unite_largeur-15, 10, 8 * unite_largeur+15, 8* unite_hauteur -10);

                graphic.DrawString("DIOCESE DE DOBA", fnt1, Brushes.Black, 16 * unite_largeur , 1 * unite_hauteur + 10);
                graphic.DrawString("BELACD CARITAS", fnt1, Brushes.Black, 16 * unite_largeur, 2 * unite_hauteur+10);
                graphic.DrawString("COORDINATION DE SANTE", fnt1, Brushes.Black, 16 * unite_largeur, 3 * unite_hauteur +10);
                graphic.DrawString("HOPITAL ST JOSEPH DE BEBEDJIA", fnt1, Brushes.Black, 16 * unite_largeur,4 * unite_hauteur+10);
                graphic.DrawString("B.P : 22 DOBA TCHAD", fnt1, Brushes.Black, 16 * unite_largeur, 5 * unite_hauteur +10);

                var elGR = System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR");
                
                    graphic.DrawString(reglement.Tiers.ToUpper(), fnt0, Brushes.Black, 2 * unite_largeur, 5 * unite_hauteur -4);

                graphic.DrawString("Objet: " + reglement.Objet, fnt11, Brushes.Black, unite_largeur, 9 * unite_hauteur - 5);

                graphic.DrawLine(Pens.Black, 10, 7 * unite_hauteur - 3, 16 * unite_largeur - 20, 7 * unite_hauteur - 3);
                graphic.DrawLine(Pens.Black, 10, 8 * unite_hauteur, 16 * unite_largeur - 20, 8 * unite_hauteur);

                graphic.DrawString(reglement.Code, fnt0, Brushes.Black, 7*unite_largeur, 7 * unite_hauteur -2);
                graphic.DrawRectangle(Pens.Black, 7 * unite_largeur, 11 * unite_hauteur+5, 5 * unite_largeur, unite_hauteur);
                graphic.DrawRectangle(Pens.Black, 12 * unite_largeur, 11 * unite_hauteur+5, 5 * unite_largeur, unite_hauteur);

                graphic.DrawString("Désignation", fnt11, Brushes.Black, 8* unite_largeur, 11 * unite_hauteur +6);
                graphic.DrawString("Frais perçu ", fnt11, Brushes.Black, 13* unite_largeur+10, 11 * unite_hauteur+6);

                var liste = from lc in ConnectionClass.ListeEncaissement(reglement.Exercice)
                            where lc.DateEncaissment == reglement.DateEncaissment
                            where lc.Tiers == reglement.Tiers
                            select lc;
                var j = 0;
                var total = .0;
                foreach(var l in liste)
                {
                    var YLOC = 12 * unite_hauteur+5 + j * unite_hauteur;

                    graphic.DrawRectangle(Pens.Black, 7*unite_largeur, YLOC, 5 * unite_largeur,  unite_hauteur );
                    graphic.DrawRectangle(Pens.Black, 12 * unite_largeur, YLOC, 5 * unite_largeur,  unite_hauteur );
                    total += l.Montant;
                    graphic.DrawString(l.Date.ToShortDateString(), fnt1, Brushes.Black, 8 * unite_largeur, YLOC);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", l.Montant), fnt1, Brushes.Black, 13 * unite_largeur + 10, YLOC);
                    if (l.Avoir > 0)
                    {
                        total += l.Avoir;
                        if (liste.Count() == 1)
                        {
                            graphic.DrawRectangle(Pens.Black,7 * unite_largeur, YLOC + unite_hauteur, 5 * unite_largeur, unite_hauteur);
                            graphic.DrawRectangle(Pens.Black, 12 * unite_largeur, YLOC + unite_hauteur, 5 * unite_largeur, unite_hauteur);
                            graphic.DrawString("Avoir", fnt1, Brushes.Black, 8 * unite_largeur, YLOC + unite_hauteur);
                            graphic.DrawString(String.Format(elGR, "{0:0,0}", l.Avoir), fnt1, Brushes.Black, 13 * unite_largeur + 10, YLOC + unite_hauteur);
                        }
                        else if (liste.Count() > 1)
                        {
                            graphic.DrawRectangle(Pens.Black, 7 * unite_largeur, YLOC + 2*unite_hauteur, 5 * unite_largeur, unite_hauteur);
                            graphic.DrawRectangle(Pens.Black, 12 * unite_largeur, YLOC + 2*unite_hauteur, 5 * unite_largeur, unite_hauteur);
                            graphic.DrawString("Avoir", fnt1, Brushes.Black, 8 * unite_largeur, YLOC + 2 * unite_hauteur);
                            graphic.DrawString(String.Format(elGR, "{0:0,0}", l.Avoir), fnt1, Brushes.Black, 13 * unite_largeur + 10, YLOC + 2 * unite_hauteur);
                        }
                    }
                    j++;
                }

                graphic.DrawString("La somme de : " + Converti((int)total ), fnt1, Brushes.Black, 2 * unite_largeur, 4 * unite_hauteur - 5);

                var LOC = 14 * unite_hauteur + 5 + j * unite_hauteur;
                graphic.DrawRectangle(Pens.Black, 7 * unite_largeur, LOC - 1* unite_hauteur, 10 * unite_largeur,2* unite_hauteur);
                graphic.DrawString("Total", fnt11, Brushes.Black, 8 * unite_largeur, LOC);
                graphic.DrawString(String.Format(elGR, "{0:0,0}", total), fnt11, Brushes.Black, 13 * unite_largeur + 10, LOC);

                //graphic.DrawLine(Pens.Black, unite_largeur, 25 * unite_hauteur + 20, 23 * unite_largeur + 10, 25 * unite_hauteur + 20);
                //graphic.DrawString(paiement + " à la somme de " + Converti((int)reglement.MontantPaiement), fnt1, Brushes.Black, unite_largeur, 21 * unite_hauteur-5);
                graphic.DrawString("N'Djaménale : " + reglement.DateEncaissment.ToShortDateString(), fnt1, Brushes.Black, 11 * unite_largeur - 10, 19 * unite_hauteur);
                graphic.DrawString("Reçu pour paiement conforme", fnt1, Brushes.Black, 10* unite_largeur, 25 * unite_hauteur - 5);
                graphic.DrawString("Signature ", fnt1, Brushes.Black, 2 * unite_largeur, 21 * unite_hauteur -5);
                graphic.DrawString("Caissier  " +reglement.Caissier, fnt1, Brushes.Black, 19 * unite_largeur, 21* unite_hauteur -5);
                
                #endregion
                graphic.DrawString("- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - "
                    ,fnt1,Brushes.Black, 10, 28 * unite_hauteur-15);
                #region FACTURE1
                graphic.DrawRectangle(Pens.Black, 10, hauteurFacture2, 24 * unite_largeur, 27 * unite_hauteur -10);
                graphic.DrawString("REÇU OFFICIEL", fnt3, Brushes.Black, 10, unite_hauteur + hauteurFacture2);
                graphic.DrawLine(Pens.Black, 10, 2 * unite_hauteur + 10 + hauteurFacture2, 16 * unite_largeur - 20, 2 * unite_hauteur + 10 + hauteurFacture2);
                graphic.DrawRectangle(Pens.Black, 16 * unite_largeur - 15, 10 + hauteurFacture2, 8 * unite_largeur + 15 , 8 * unite_hauteur - 10);

                graphic.DrawString("DIOCESE DE DOBA", fnt1, Brushes.Black, 16 * unite_largeur, 1 * unite_hauteur + 10 + hauteurFacture2);
                graphic.DrawString("BELACD CARITAS", fnt1, Brushes.Black, 16 * unite_largeur, 2 * unite_hauteur + 10 + hauteurFacture2);
                graphic.DrawString("COORDINATION DE SANTE", fnt1, Brushes.Black, 16 * unite_largeur, 3 * unite_hauteur + 10 + hauteurFacture2);
                graphic.DrawString("HOPITAL ST JOSEPH DE BEBEDJIA", fnt1, Brushes.Black, 16 * unite_largeur, 4 * unite_hauteur + 10 + hauteurFacture2);
                graphic.DrawString("B.P : 22 DOBA TCHAD", fnt1, Brushes.Black, 16 * unite_largeur, 5 * unite_hauteur + 10 + hauteurFacture2);
                
                graphic.DrawString("La somme de : " + Converti((int)total), fnt1, Brushes.Black, 2 * unite_largeur, 4 * unite_hauteur - 5 + hauteurFacture2);
                graphic.DrawString(reglement.Tiers.ToUpper(), fnt0, Brushes.Black, 2 * unite_largeur, 5 * unite_hauteur - 4 + hauteurFacture2);

                graphic.DrawString("Objet: " + reglement.Objet, fnt11, Brushes.Black, unite_largeur, 9 * unite_hauteur - 5 + hauteurFacture2);

                graphic.DrawLine(Pens.Black, 10, 7 * unite_hauteur - 3 + hauteurFacture2, 16 * unite_largeur - 20, 7 * unite_hauteur - 3 + hauteurFacture2);
                graphic.DrawLine(Pens.Black, 10, 8 * unite_hauteur + hauteurFacture2, 16 * unite_largeur - 20, 8 * unite_hauteur + hauteurFacture2);

                graphic.DrawString(reglement.Code, fnt0, Brushes.Black, 7 * unite_largeur, 7 * unite_hauteur - 2 + hauteurFacture2);
                graphic.DrawRectangle(Pens.Black, 7 * unite_largeur, 11 * unite_hauteur + 5 + hauteurFacture2, 5 * unite_largeur, unite_hauteur );
                graphic.DrawRectangle(Pens.Black, 12 * unite_largeur, 11 * unite_hauteur + 5 + hauteurFacture2, 5 * unite_largeur, unite_hauteur );

                graphic.DrawString("Désignation", fnt11, Brushes.Black, 8 * unite_largeur, 11 * unite_hauteur + 6 + hauteurFacture2);
                graphic.DrawString("Frais perçu ", fnt11, Brushes.Black, 13 * unite_largeur + 10, 11 * unite_hauteur + 6 + hauteurFacture2);
                j = 0;
                foreach (var l in liste)
                {
                    var YLOC = 12 * unite_hauteur + 5 + j * unite_hauteur + hauteurFacture2;

                    graphic.DrawRectangle(Pens.Black, 7 * unite_largeur, YLOC, 5 * unite_largeur, unite_hauteur);
                    graphic.DrawRectangle(Pens.Black, 12 * unite_largeur, YLOC, 5 * unite_largeur, unite_hauteur);

                    graphic.DrawString(l.Date.ToShortDateString(), fnt1, Brushes.Black, 8 * unite_largeur, YLOC);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", l.Montant), fnt1, Brushes.Black, 13 * unite_largeur + 10, YLOC);
                    if (l.Avoir > 0)
                    {
                        if (liste.Count() == 1)
                        {
                            graphic.DrawRectangle(Pens.Black, 7 * unite_largeur, YLOC + unite_hauteur, 5 * unite_largeur, unite_hauteur);
                            graphic.DrawRectangle(Pens.Black, 12 * unite_largeur, YLOC + unite_hauteur, 5 * unite_largeur, unite_hauteur);
                            graphic.DrawString("Avoir", fnt1, Brushes.Black, 8 * unite_largeur, YLOC + unite_hauteur);
                            graphic.DrawString(String.Format(elGR, "{0:0,0}", l.Avoir), fnt1, Brushes.Black, 13 * unite_largeur + 10, YLOC + unite_hauteur);
                        }
                        else if (liste.Count() > 1)
                        {
                            graphic.DrawRectangle(Pens.Black, 7 * unite_largeur, YLOC + 2 * unite_hauteur, 5 * unite_largeur, unite_hauteur);
                            graphic.DrawRectangle(Pens.Black, 12 * unite_largeur, YLOC + 2 * unite_hauteur, 5 * unite_largeur, unite_hauteur);
                            graphic.DrawString("Avoir", fnt1, Brushes.Black, 8 * unite_largeur, YLOC + 2 * unite_hauteur);
                            graphic.DrawString(String.Format(elGR, "{0:0,0}", l.Avoir), fnt1, Brushes.Black, 13 * unite_largeur + 10, YLOC + 2 * unite_hauteur);
                        }
                    }
                    j++;
                }

                 LOC = 14 * unite_hauteur + 5 + j * unite_hauteur + hauteurFacture2;
                graphic.DrawRectangle(Pens.Black, 7 * unite_largeur, LOC - 1 * unite_hauteur, 10 * unite_largeur, 2 * unite_hauteur);
                graphic.DrawString("Total", fnt11, Brushes.Black, 8 * unite_largeur, LOC);
                graphic.DrawString(String.Format(elGR, "{0:0,0}", total), fnt11, Brushes.Black, 13 * unite_largeur + 10, LOC);

                //graphic.DrawLine(Pens.Black, unite_largeur, 25 * unite_hauteur + 20, 23 * unite_largeur + 10, 25 * unite_hauteur + 20);
                //graphic.DrawString(paiement + " à la somme de " + Converti((int)reglement.MontantPaiement), fnt1, Brushes.Black, unite_largeur, 21 * unite_hauteur-5);
                graphic.DrawString("N'Djaménale : " + reglement.DateEncaissment.ToShortDateString(), fnt1, Brushes.Black, 11 * unite_largeur - 10, 19 * unite_hauteur + hauteurFacture2);
                graphic.DrawString("Reçu pour paiement conforme", fnt1, Brushes.Black, 10 * unite_largeur, 25 * unite_hauteur - 5 + hauteurFacture2);
                graphic.DrawString("Signature ", fnt1, Brushes.Black, 2 * unite_largeur, 21 * unite_hauteur - 5 + hauteurFacture2);
                graphic.DrawString("Caissier  " + reglement.Caissier, fnt1, Brushes.Black, 19 * unite_largeur, 21 * unite_hauteur - 5 + hauteurFacture2);

                #endregion

                return bitmap;
                #endregion

            }
            catch(Exception ex)
            {
                GestionPharmacetique.MonMessageBox.ShowBox("", ex);
                return null;
            }
        }

        //imprimer une facture
        public static Bitmap ImprimerUneFature(Document facture, DataGridView dgv )
        {
            try
            {

                //les dimension de la facture
                int unite_hauteur = 23;
                int unite_largeur = 32;
                int largeur_facture = 24 * unite_largeur + 5;
                int detail_hauteur_facture = 20 * unite_hauteur;
                int hauteur_facture = 54 * unite_hauteur+10;
                //creer un bit map
                Bitmap bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                // creer un objet graphic
                Graphics graphic = Graphics.FromImage(bitmap);

                //la couleur de l'image
                graphic.Clear(Color.White);
                #region facture1

                Image imageLogo = global::SGSP.Properties.Resources.Logo;
                graphic.DrawImage(imageLogo,  1*unite_largeur, 32, 7 * unite_largeur, 4 * unite_hauteur -17);

                //definir les polices 
                Font fnt12 = new Font("Arial Narrow", 12.5f, FontStyle.Bold);
                Font fnt1 = new Font("Arial Narrow", 12.5f, FontStyle.Regular);
                Font fnt11 = new Font("Arial Narrow", 10.5f, FontStyle.Regular);
                Font fnt0 = new Font("Arial Narrow", 12, FontStyle.Bold);
                Font fnt2 = new Font("Arial Narrow", 16, FontStyle.Bold);
                Font fnt3 = new Font("Arial Narrow", 14, FontStyle.Bold);
                Font fnt4 = new Font("Bradley Hand ITC", 17, FontStyle.Bold);

               graphic.DrawString("Esprit metier", fnt4, Brushes.Black, unite_largeur * 18, unite_hauteur+10);
                graphic.DrawString("N'Djaména-TCHAD", fnt11, Brushes.Black,  1*unite_largeur , 6 * unite_hauteur);
                graphic.DrawString("Tél. (+235) 66 36 17 04 / 99 82 77 55 ", fnt11, Brushes.Black,  1*unite_largeur , 7 * unite_hauteur-2);
                graphic.DrawString("Email : honographic@gmail.com ", fnt11, Brushes.Black,  1*unite_largeur , 8 * unite_hauteur-5);
                graphic.DrawString("Avenue Mobutu/Radio Arc-en ciel : ", fnt11, Brushes.Black,  1*unite_largeur , 9 * unite_hauteur-8);

                if (facture.TypeDocument.ToUpper().Contains("PROFORMA"))
                {
                    graphic.DrawString("Date de facture proforma : " + facture.DateEnregistrement.ToShortDateString(), fnt1, Brushes.Black, 16 * unite_largeur, 9 * unite_hauteur);
                }
                else if (facture.TypeDocument.ToUpper().Contains("TRAVAIL"))
                {
                    graphic.DrawString("Date de bon de travail : " + facture.DateEnregistrement.ToShortDateString(), fnt1, Brushes.Black, 15 * unite_largeur, 9 * unite_hauteur);
                }
                else if (facture.TypeDocument.ToUpper().Contains("COMMANDE"))
                {
                    graphic.DrawString("Date de commande : " + facture.DateEnregistrement.ToShortDateString(), fnt1, Brushes.Black, 15 * unite_largeur, 9 * unite_hauteur);
                }
                else if (facture.TypeDocument.ToUpper().Contains("ACHAT"))
                {
                    graphic.DrawString("Date de bon d'achat : " + facture.DateEnregistrement.ToShortDateString(), fnt1, Brushes.Black, 15 * unite_largeur, 9 * unite_hauteur);
                }
                else 
                {
                    graphic.DrawString("Date de facture : " + facture.DateEnregistrement.ToShortDateString(), fnt1, Brushes.Black, 16 * unite_largeur, 9 * unite_hauteur);
                } 
                imageLogo = global::SGSP.Properties.Resources.roundedRectangle;
                graphic.DrawImage(imageLogo,  1*unite_largeur ,11*unite_hauteur, 8 * unite_largeur,  3 * unite_hauteur+15);
                graphic.DrawImage(imageLogo,10* unite_largeur, 11 * unite_hauteur, 13 * unite_largeur, 3 * unite_hauteur+15);
                var idF="";
                if (facture.IDTypeDocument < 10)
                {
                    idF = "0000" + facture.IDTypeDocument;
                }
                else if (facture.IDTypeDocument >= 10 && facture.IDTypeDocument < 100)
                {
                    idF = "000" + facture.IDTypeDocument;
                }
                else if (facture.IDTypeDocument >= 100 && facture.IDTypeDocument < 1000)
                {
                    idF = "00" + facture.IDTypeDocument;
                }
                else if (facture.IDTypeDocument >= 1000 && facture.IDTypeDocument < 10000)
                {
                    idF = "0" + facture.IDTypeDocument;
                }

                graphic.DrawString(facture.TypeDocument, fnt1, Brushes.Black, 2 * unite_largeur, 11 * unite_hauteur + 10);
                graphic.DrawString("N° : " + idF, fnt1, Brushes.Black, 2*unite_largeur, 12 * unite_hauteur+10);
                graphic.DrawString("Référence : " + facture.ReferenceDocument, fnt1, Brushes.Black, 2*unite_largeur, 13 * unite_hauteur+10);

                graphic.DrawString("Destinataire ", fnt1, Brushes.Black, 15* unite_largeur, 11 * unite_hauteur + 10);
                graphic.DrawString(facture.RootPathDocument.ToUpper(), fnt2, Brushes.Black, 12 * unite_largeur-15, 13 * unite_hauteur );
                graphic.FillRectangle(Brushes.DarkGray, 1* unite_largeur , 15 * unite_hauteur + 5, 22 * unite_largeur ,  unite_hauteur + 5);
                graphic.DrawString(facture.TypeDocument, fnt3, Brushes.Black, 10 * unite_largeur, 15 * unite_hauteur + 7);

                imageLogo = global::SGSP.Properties.Resources.detailFactures;
                graphic.DrawRectangle(Pens.Black, 1 * unite_largeur, 17 * unite_hauteur, 22 * unite_largeur, unite_hauteur+10);
                //graphic.DrawRectangle(Pens.Black, 1 * unite_largeur, 16 * unite_hauteur, 22 * unite_largeur+20, unite_hauteur + 10);
                graphic.DrawRectangle(Pens.Black, 1 * unite_largeur, 17 * unite_hauteur, 22 * unite_largeur, detail_hauteur_facture);
                graphic.DrawRectangle(Pens.Black, 1 * unite_largeur, 17 * unite_hauteur, 1 * unite_largeur, detail_hauteur_facture);
                graphic.DrawRectangle(Pens.Black, 14 * unite_largeur-4, 17 * unite_hauteur, 2 * unite_largeur+16, detail_hauteur_facture);
                graphic.DrawRectangle(Pens.Black, 16 * unite_largeur+12, 17 * unite_hauteur, 2 * unite_largeur + 16, detail_hauteur_facture);
                //graphic.DrawRectangle(Pens.Black, 18 * unite_largeur + 4, 17 * unite_hauteur, 3 * unite_largeur + 16, detail_hauteur_facture);
               
                graphic.DrawString("N° ", fnt0, Brushes.Black, 1 * unite_largeur+8, 17 * unite_hauteur + 5);
                graphic.DrawString("Désignation ", fnt0, Brushes.Black, 6 * unite_largeur + 8, 17 * unite_hauteur + 5);
                graphic.DrawString("Quantité", fnt0, Brushes.Black, 14 * unite_largeur+10, 17 * unite_hauteur + 5);
                graphic.DrawString("P.U ", fnt0, Brushes.Black, 17 * unite_largeur , 17 * unite_hauteur + 5);
                graphic.DrawString("P.T ", fnt0, Brushes.Black, 20 * unite_largeur , 17 * unite_hauteur + 5);
                
                int j = 0;
                for (var i = 0; i < dgv.Rows.Count; i++)
                {
                    var YLOC = 19 * unite_hauteur + unite_hauteur * j;

                    double prixAchat, qte, prixTotal;
                    if (double.TryParse(dgv.Rows[i].Cells[2].Value.ToString(), out qte)
                        && double.TryParse(dgv.Rows[i].Cells[3].Value.ToString(), out prixAchat)
                            &&
                            double.TryParse(dgv.Rows[i].Cells[4].Value.ToString(), out prixTotal))
                    {
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", qte), fnt12, Brushes.Black, 14 * unite_largeur+10, YLOC);
                        graphic.DrawString(dgv.Rows[i].Cells[1].Value.ToString(), fnt12, Brushes.Black, 3 * unite_largeur - 8, YLOC);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", prixAchat), fnt12, Brushes.Black, 17 * unite_largeur - 8, YLOC);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", prixTotal), fnt12, Brushes.Black, 19 * unite_largeur + 10, YLOC); graphic.DrawString(dgv.Rows[i].Cells[5].Value.ToString(), fnt1, Brushes.Black, 1 * unite_largeur + 8, YLOC);
                        graphic.DrawString(dgv.Rows[i].Cells[5].Value.ToString(), fnt12, Brushes.Black, 1 * unite_largeur + 8, YLOC);
    
                    }
                   else
                    {   
                        graphic.DrawString(dgv.Rows[i].Cells[1].Value.ToString() , fnt1, Brushes.Black, 3 * unite_largeur - 8, YLOC);
                    }
                    j++;
                }

                graphic.FillRectangle(Brushes.White, 1* unite_largeur, 37 * unite_hauteur+5, 24 * unite_largeur, 18 * unite_hauteur + 5);
                if (dgv.Rows.Count <= 18 )
                {
                   
                    graphic.DrawRectangle(Pens.Black, 1 * unite_largeur, detail_hauteur_facture  +17* unite_hauteur, 22 * unite_largeur, 3 * unite_hauteur + 5);

                   graphic.DrawString("Modalité paiement : "+facture.ModalitePaiement, fnt1, Brushes.Black, 2 * unite_largeur, 37 * unite_hauteur + 15);
                    graphic.DrawString("Délai de livraison : "+facture.EcheanceLivraison.ToShortDateString(), fnt1, Brushes.Black,2 * unite_largeur, 38 * unite_hauteur + 15);
                    var tva = .0;
                    if (facture.MontantTTC != facture.MontantHT)
                    {
                         tva = 100 * (facture.MontantTTC - facture.MontantHT) / facture.MontantHT;
                    }
                    graphic.DrawString("Total HT", fnt0, Brushes.Black, 16 * unite_largeur + 10, 37 * unite_hauteur + 5);
                    graphic.DrawString("TVA (" + (int)tva + "%)", fnt0, Brushes.Black, 16 * unite_largeur + 10, 38 * unite_hauteur + 5);
                    graphic.DrawString("Total TTC", fnt0, Brushes.Black, 16 * unite_largeur + 10, 39 * unite_hauteur + 5);

                    graphic.DrawString(String.Format(elGR, "{0:0,0}", facture.MontantHT), fnt0, Brushes.Black, 19 * unite_largeur + 10, 37 * unite_hauteur + 5);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", facture.TVA), fnt0, Brushes.Black, 19 * unite_largeur + 10, 38 * unite_hauteur + 5);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", facture.MontantTTC), fnt0, Brushes.Black, 19* unite_largeur + 10, 39 * unite_hauteur + 5);
                    string arrete = "";
                    if (facture.TypeDocument.ToUpper().Contains("FACTURE"))
                    {
                        arrete = "Arrêtée la présente " + facture.TypeDocument.ToLower() + " à la somme";
                    }
                    else
                    {

                    }
                    graphic.DrawString(arrete + " : " + Converti((int)facture.MontantTTC).ToLower() + " Franc CFA", fnt1, Brushes.Black, unite_largeur, 41 * unite_hauteur + 10);
                    graphic.DrawString("La Direction", fnt1, Brushes.Black, 18 * unite_largeur, 43 * unite_hauteur );
                }

                if (dgv.Rows.Count > 18)
                {
                    var page = 1;
                    graphic.DrawString("Page " + page, fnt11, Brushes.Black, 22 * unite_largeur-5, 53 * unite_hauteur-5);
                }
                imageLogo = global::SGSP.Properties.Resources.bas_2;
                graphic.DrawImage(imageLogo, 1 * unite_largeur, 51*unite_hauteur+5, 22 * unite_largeur,  unite_hauteur + 5);

                return bitmap;
               
                #endregion

            }
            catch (Exception ex) { GestionPharmacetique.MonMessageBox.ShowBox("", ex); return null; }
        }

        public static Bitmap ImprimerUneFaturePage(Document facture, DataGridView dgv, int index)
        {
            try
            {

                //les dimension de la facture
                int unite_hauteur = 23;
                int unite_largeur = 32;
                int largeur_facture = 24 * unite_largeur + 5;
                int detail_hauteur_facture = 39 * unite_hauteur;
                int hauteur_facture = 54 * unite_hauteur+10;
                //creer un bit map
                Bitmap bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                // creer un objet graphic
                Graphics graphic = Graphics.FromImage(bitmap);

                //la couleur de l'image
                graphic.Clear(Color.White);
                #region facture1
                
                //definir les polices 
                Font fnt12 = new Font("Arial Narrow", 12.5f, FontStyle.Bold);
                Font fnt1 = new Font("Arial Narrow", 12.5f, FontStyle.Regular);
                Font fnt11 = new Font("Arial Narrow", 12, FontStyle.Regular);
                Font fnt0 = new Font("Arial Narrow", 12, FontStyle.Bold);
                Font fnt2 = new Font("Arial Narrow", 16, FontStyle.Bold);
                Font fnt3 = new Font("Arial Narrow", 14, FontStyle.Bold);
                Font fnt110 = new Font("Arial Narrow", 10.5f, FontStyle.Regular);
                var page = 2 + index;

                graphic.DrawRectangle(Pens.Black, 1 * unite_largeur, 2 * unite_hauteur, 22 * unite_largeur, unite_hauteur + 10);
                
                graphic.DrawRectangle(Pens.Black, 1 * unite_largeur, 2 * unite_hauteur, 22 * unite_largeur, detail_hauteur_facture);
                graphic.DrawRectangle(Pens.Black, 1 * unite_largeur, 2 * unite_hauteur, 1 * unite_largeur, detail_hauteur_facture);
                graphic.DrawRectangle(Pens.Black, 14 * unite_largeur - 4, 2 * unite_hauteur, 2 * unite_largeur + 16, detail_hauteur_facture);
                graphic.DrawRectangle(Pens.Black, 16 * unite_largeur + 12, 2 * unite_hauteur, 2 * unite_largeur + 16, detail_hauteur_facture);

                graphic.DrawString("N° ", fnt0, Brushes.Black, 1 * unite_largeur + 8, 2 * unite_hauteur + 5);
                graphic.DrawString("Désignation ", fnt0, Brushes.Black, 6 * unite_largeur + 8, 2 * unite_hauteur + 5);
                graphic.DrawString("Quantité", fnt0, Brushes.Black, 14 * unite_largeur + 10, 2 * unite_hauteur + 5);
                graphic.DrawString("P.U ", fnt0, Brushes.Black, 17 * unite_largeur, 2 * unite_hauteur + 5);
                graphic.DrawString("P.T ", fnt0, Brushes.Black, 20 * unite_largeur, 2 * unite_hauteur + 5);

                int j = 0;
                for (var i = 18 + index * 37; i < dgv.Rows.Count; i++)
                {
                    var YLOC = 4 * unite_hauteur + unite_hauteur * j;

                    double prixAchat, qte, prixTotal;
                    if (double.TryParse(dgv.Rows[i].Cells[2].Value.ToString(), out qte)
                        && double.TryParse(dgv.Rows[i].Cells[3].Value.ToString(), out prixAchat)
                            &&
                            double.TryParse(dgv.Rows[i].Cells[4].Value.ToString(), out prixTotal))
                    {
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", qte), fnt12, Brushes.Black, 14 * unite_largeur + 10, YLOC);
                        graphic.DrawString(dgv.Rows[i].Cells[1].Value.ToString(), fnt12, Brushes.Black, 3 * unite_largeur - 8, YLOC);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", prixAchat), fnt12, Brushes.Black, 17 * unite_largeur - 8, YLOC);
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", prixTotal), fnt12, Brushes.Black, 19 * unite_largeur + 10, YLOC); graphic.DrawString(dgv.Rows[i].Cells[5].Value.ToString(), fnt1, Brushes.Black, 1 * unite_largeur + 8, YLOC);
                        graphic.DrawString(dgv.Rows[i].Cells[5].Value.ToString(), fnt12, Brushes.Black, 1 * unite_largeur + 8, YLOC);

                    }
                    else
                    {
                        graphic.DrawString(dgv.Rows[i].Cells[1].Value.ToString() , fnt1, Brushes.Black, 3 * unite_largeur - 8, YLOC);
                    }
                    graphic.DrawString("Page " + page, fnt110, Brushes.Black, 22 * unite_largeur, 53 * unite_hauteur);
                    j++;
                }
                graphic.FillRectangle(Brushes.White, 1 * unite_largeur, 41 * unite_hauteur + 5, 24 * unite_largeur, 20 * unite_hauteur + 22);
                if (dgv.Rows.Count <= 18 + 37 * (1 + index))
                {

                    graphic.DrawRectangle(Pens.Black, 1 * unite_largeur, 37 * unite_hauteur + 5, 22 * unite_largeur, 4 * unite_hauteur-5 );

                    graphic.DrawString("Modalité paiement : " + facture.ModalitePaiement, fnt1, Brushes.Black, 2 * unite_largeur, 37 * unite_hauteur + 15);
                    graphic.DrawString("Délai de livraison : " + facture.EcheanceLivraison.ToShortDateString(), fnt1, Brushes.Black, 2 * unite_largeur, 38 * unite_hauteur + 15);

                    var tva = 100 * (facture.MontantTTC - facture.MontantHT) / facture.MontantHT;
                    graphic.DrawString("Total HT", fnt0, Brushes.Black, 16 * unite_largeur + 10, 37 * unite_hauteur + 5);
                    graphic.DrawString("TVA (" + (int)tva + "%)", fnt0, Brushes.Black, 16 * unite_largeur + 10, 38 * unite_hauteur + 5);
                    graphic.DrawString("Total TTC", fnt0, Brushes.Black, 16 * unite_largeur + 10, 39 * unite_hauteur + 5);

                    graphic.DrawString(String.Format(elGR, "{0:0,0}", facture.MontantHT), fnt0, Brushes.Black, 19 * unite_largeur + 10, 37 * unite_hauteur + 5);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", facture.TVA), fnt0, Brushes.Black, 19 * unite_largeur + 10, 38 * unite_hauteur + 5);
                    graphic.DrawString(String.Format(elGR, "{0:0,0}", facture.MontantTTC), fnt0, Brushes.Black, 19 * unite_largeur + 10, 39 * unite_hauteur + 5);
                    string arrete = "";
                    if (facture.TypeDocument.ToUpper().Contains("FACTURE"))
                    {
                        arrete = "Arrêtée la présente " + facture.TypeDocument.ToLower() + " à la somme";
                    }
                    else
                    {

                    }
                    graphic.DrawString(arrete + " : " + Converti((int)facture.MontantTTC).ToLower() + " Franc CFA", fnt1, Brushes.Black, unite_largeur, 41 * unite_hauteur + 10);
                    graphic.DrawString("La Direction", fnt1, Brushes.Black, 18 * unite_largeur, 43 * unite_hauteur);
                }

                graphic.DrawString("Page " + page, fnt110, Brushes.Black, 22 * unite_largeur-5, 53 * unite_hauteur-5);
                var imageLogo = global::SGSP.Properties.Resources.bas_2;
                graphic.DrawImage(imageLogo, 1 * unite_largeur, 51 * unite_hauteur + 5, 22 * unite_largeur, unite_hauteur + 5);

                return bitmap;

                #endregion

            }
            catch (Exception ex) { GestionPharmacetique.MonMessageBox.ShowBox("", ex); return null; }
        }

        public static Bitmap ImprimerUnBonLivraison(Document facture, DataGridView dgv)
        {
            try
            {

                //les dimension de la facture
                int unite_hauteur = 23;
                int unite_largeur = 32;
                int largeur_facture = 24 * unite_largeur + 5;
                int detail_hauteur_facture = 27 * unite_hauteur;
                int hauteur_facture = 54 * unite_hauteur + 10;
                //creer un bit map
                Bitmap bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                // creer un objet graphic
                Graphics graphic = Graphics.FromImage(bitmap);

                //la couleur de l'image
                graphic.Clear(Color.White);
                #region facture1

                Image imageLogo = global::SGSP.Properties.Resources.Logo;
                graphic.DrawImage(imageLogo, 1 * unite_largeur, 32, 7 * unite_largeur, 4 * unite_hauteur - 17);

                //definir les polices 
                Font fnt12 = new Font("Arial Narrow", 12.5f, FontStyle.Bold);
                Font fnt1 = new Font("Arial Narrow", 12.5f, FontStyle.Regular);
                Font fnt11 = new Font("Arial Narrow", 10.5f, FontStyle.Regular);
                Font fnt0 = new Font("Arial Narrow", 12, FontStyle.Bold);
                Font fnt2 = new Font("Arial Narrow", 16, FontStyle.Bold);
                Font fnt3 = new Font("Arial Narrow", 14, FontStyle.Bold);
                Font fnt4 = new Font("Bradley Hand ITC", 17, FontStyle.Bold);

                graphic.DrawString("Esprit metier", fnt4, Brushes.Black, unite_largeur * 18, unite_hauteur + 10);
                graphic.DrawString("N'Djaména-TCHAD", fnt11, Brushes.Black, 1 * unite_largeur, 6 * unite_hauteur);
                graphic.DrawString("Tél. (+235) 66 36 17 04 / 99 82 77 55 ", fnt11, Brushes.Black, 1 * unite_largeur, 7 * unite_hauteur - 2);
                graphic.DrawString("Email : honographic@gmail.com ", fnt11, Brushes.Black, 1 * unite_largeur, 8 * unite_hauteur - 5);
                graphic.DrawString("Avenue Mobutu/Radio Arc-en ciel : ", fnt11, Brushes.Black, 1 * unite_largeur, 9 * unite_hauteur - 8);

                graphic.DrawString("Date de livraison : " + DateTime.Now.ToShortDateString(), fnt1, Brushes.Black, 15 * unite_largeur, 9 * unite_hauteur);

                imageLogo = global::SGSP.Properties.Resources.roundedRectangle;
                graphic.DrawImage(imageLogo, 1 * unite_largeur, 11 * unite_hauteur, 8 * unite_largeur, 3 * unite_hauteur + 15);
                graphic.DrawImage(imageLogo, 10 * unite_largeur, 11 * unite_hauteur, 13 * unite_largeur, 3 * unite_hauteur + 15);
                var idF = "";
                if (facture.IDTypeDocument < 10)
                {
                    idF = "0000" + facture.IDTypeDocument;
                }
                else if (facture.IDTypeDocument >= 10 && facture.IDTypeDocument < 100)
                {
                    idF = "000" + facture.IDTypeDocument;
                }
                else if (facture.IDTypeDocument >= 100 && facture.IDTypeDocument < 1000)
                {
                    idF = "00" + facture.IDTypeDocument;
                }
                else if (facture.IDTypeDocument >= 1000 && facture.IDTypeDocument < 10000)
                {
                    idF = "0" + facture.IDTypeDocument;
                }

                graphic.DrawString("BON DE LIVRAISON", fnt1, Brushes.Black, 2 * unite_largeur, 11 * unite_hauteur + 10);
                graphic.DrawString("N° : " + idF, fnt1, Brushes.Black, 2 * unite_largeur, 12 * unite_hauteur + 10);
                graphic.DrawString("Référence : " + facture.ReferenceDocument, fnt1, Brushes.Black, 2 * unite_largeur, 13 * unite_hauteur + 10);

                graphic.DrawString("Destinataire ", fnt1, Brushes.Black, 15 * unite_largeur, 11 * unite_hauteur + 10);
                graphic.DrawString(facture.RootPathDocument.ToUpper(), fnt2, Brushes.Black, 12 * unite_largeur - 15, 13 * unite_hauteur);
                graphic.FillRectangle(Brushes.DarkGray, 1 * unite_largeur, 15 * unite_hauteur + 5, 22 * unite_largeur, unite_hauteur + 5);
                graphic.DrawString("BON DE LIVRAISON", fnt3, Brushes.Black, 10 * unite_largeur, 15 * unite_hauteur + 7);

                imageLogo = global::SGSP.Properties.Resources.detailFactures;
                graphic.DrawRectangle(Pens.Black, 1 * unite_largeur, 17 * unite_hauteur, 22 * unite_largeur, unite_hauteur + 10);
                graphic.DrawRectangle(Pens.Black, 1 * unite_largeur, 17 * unite_hauteur, 22 * unite_largeur, detail_hauteur_facture);
                graphic.DrawRectangle(Pens.Black, 1 * unite_largeur, 17 * unite_hauteur, 18 * unite_largeur, detail_hauteur_facture);
                graphic.DrawRectangle(Pens.Black, 1 * unite_largeur, 17 * unite_hauteur, 1 * unite_largeur, detail_hauteur_facture);
   
                graphic.DrawString("N° ", fnt0, Brushes.Black, 1 * unite_largeur + 8, 17 * unite_hauteur + 5);
                graphic.DrawString("Désignation ", fnt0, Brushes.Black, 9 * unite_largeur + 8, 17 * unite_hauteur + 5);
                graphic.DrawString("Quantité", fnt0, Brushes.Black, 20 * unite_largeur, 17 * unite_hauteur + 5);

                int j = 0;
                for (var i = 0; i < dgv.Rows.Count; i++)
                {
                    var YLOC = 19 * unite_hauteur + unite_hauteur * j;
                    //graphic.DrawString(String.Format(elGR, "{0:0,0}", i), fnt12, Brushes.Black, 19 * unite_largeur + 10, YLOC);
                       
                    double prixAchat, qte, prixTotal;
                    if (double.TryParse(dgv.Rows[i].Cells[2].Value.ToString(), out qte)
                        && double.TryParse(dgv.Rows[i].Cells[3].Value.ToString(), out prixAchat)
                            &&
                            double.TryParse(dgv.Rows[i].Cells[4].Value.ToString(), out prixTotal))
                    {
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", qte), fnt12, Brushes.Black, 20 * unite_largeur + 10, YLOC);
                        graphic.DrawString(dgv.Rows[i].Cells[1].Value.ToString() + " " , fnt12, Brushes.Black, 3 * unite_largeur - 8, YLOC);
                       graphic.DrawString(dgv.Rows[i].Cells[5].Value.ToString(), fnt12, Brushes.Black, 1 * unite_largeur + 8, YLOC);

                    }
                    else
                    {
                        graphic.DrawString(dgv.Rows[i].Cells[1].Value.ToString(), fnt1, Brushes.Black, 3 * unite_largeur - 8, YLOC);
                    }
                    j++;
                }

                graphic.FillRectangle(Brushes.White, 1 * unite_largeur, 44 * unite_hauteur + 5, 24 * unite_largeur, 18 * unite_hauteur + 5);
                if (dgv.Rows.Count <= 25)
                {
                    graphic.DrawString("Le Fournisseur", fnt1, Brushes.Black, 18 * unite_largeur, 46 * unite_hauteur);
                   graphic.DrawString("Le Receptionniste", fnt1, Brushes.Black, 2 * unite_largeur, 46 * unite_hauteur);
                        var page = 1;
                    graphic.DrawString("Page " + page, fnt11, Brushes.Black, 22 * unite_largeur - 5, 53 * unite_hauteur - 5);
                }
                imageLogo = global::SGSP.Properties.Resources.bas_2;
                graphic.DrawImage(imageLogo, 1 * unite_largeur, 51 * unite_hauteur + 5, 22 * unite_largeur, unite_hauteur + 5);

                return bitmap;

                #endregion

            }
            catch (Exception ex) { GestionPharmacetique.MonMessageBox.ShowBox("", ex); return null; }
        }

        public static Bitmap ImprimerUnBonLivraison(Document facture, DataGridView dgv, int index)
        {
            try
            {

                //les dimension de la facture
                int unite_hauteur = 23;
                int unite_largeur = 32;
                int largeur_facture = 24 * unite_largeur + 5;
                int detail_hauteur_facture = 42 * unite_hauteur;
                int hauteur_facture = 54 * unite_hauteur + 10;
                //creer un bit map
                Bitmap bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                // creer un objet graphic
                Graphics graphic = Graphics.FromImage(bitmap);

                //la couleur de l'image
                graphic.Clear(Color.White);
                #region facture1

                //definir les polices 
                Font fnt12 = new Font("Arial Narrow", 12.5f, FontStyle.Bold);
                Font fnt1 = new Font("Arial Narrow", 12.5f, FontStyle.Regular);
                Font fnt11 = new Font("Arial Narrow", 12, FontStyle.Regular);
                Font fnt0 = new Font("Arial Narrow", 12, FontStyle.Bold);
                Font fnt2 = new Font("Arial Narrow", 16, FontStyle.Bold);
                Font fnt3 = new Font("Arial Narrow", 14, FontStyle.Bold);
                Font fnt110 = new Font("Arial Narrow", 10.5f, FontStyle.Regular);
                var page = 2 + index;

                graphic.DrawRectangle(Pens.Black, 1 * unite_largeur, 2 * unite_hauteur, 22 * unite_largeur, unite_hauteur + 10);

                graphic.DrawRectangle(Pens.Black, 1 * unite_largeur, 2 * unite_hauteur, 22 * unite_largeur, detail_hauteur_facture);

                graphic.DrawRectangle(Pens.Black, 1 * unite_largeur, 2 * unite_hauteur, 22 * unite_largeur, detail_hauteur_facture);
                graphic.DrawRectangle(Pens.Black, 1 * unite_largeur, 2 * unite_hauteur, 18 * unite_largeur, detail_hauteur_facture);
                graphic.DrawRectangle(Pens.Black, 1 * unite_largeur, 2 * unite_hauteur, 1 * unite_largeur, detail_hauteur_facture);
   
                graphic.DrawString("N° ", fnt0, Brushes.Black, 1 * unite_largeur + 8, 2 * unite_hauteur + 5);
                graphic.DrawString("Désignation ", fnt0, Brushes.Black, 9 * unite_largeur + 8, 2 * unite_hauteur + 5);
                graphic.DrawString("Quantité", fnt0, Brushes.Black, 20 * unite_largeur + 10, 2 * unite_hauteur + 5);

                int j = 0;
                for (var i = 25 + index * 40; i < dgv.Rows.Count; i++)
                {
                    var YLOC = 4 * unite_hauteur + unite_hauteur * j;

                    double prixAchat, qte, prixTotal;
                    if (double.TryParse(dgv.Rows[i].Cells[2].Value.ToString(), out qte)
                        && double.TryParse(dgv.Rows[i].Cells[3].Value.ToString(), out prixAchat)
                            &&
                            double.TryParse(dgv.Rows[i].Cells[4].Value.ToString(), out prixTotal))
                    {
                        graphic.DrawString(String.Format(elGR, "{0:0,0}", qte), fnt12, Brushes.Black, 20 * unite_largeur + 10, YLOC);
                        graphic.DrawString(dgv.Rows[i].Cells[1].Value.ToString(), fnt12, Brushes.Black, 3 * unite_largeur - 8, YLOC);
                        graphic.DrawString(dgv.Rows[i].Cells[5].Value.ToString(), fnt12, Brushes.Black, 1 * unite_largeur + 8, YLOC);

                    }
                    else
                    {
                        graphic.DrawString(dgv.Rows[i].Cells[1].Value.ToString() , fnt1, Brushes.Black, 3 * unite_largeur - 8, YLOC);
                    }
                    graphic.DrawString("Page " + page, fnt110, Brushes.Black, 22 * unite_largeur, 53 * unite_hauteur);
                    j++;
                }
                graphic.FillRectangle(Brushes.White, 1 * unite_largeur, 44 * unite_hauteur+5 , 24 * unite_largeur, 20 * unite_hauteur + 5);
                if (dgv.Rows.Count <= 25 + 40 * (1 + index))
                {
                    graphic.DrawString("Le Fournisseur", fnt1, Brushes.Black, 18 * unite_largeur, 45 * unite_hauteur);
                    graphic.DrawString("Le Receptionniste", fnt1, Brushes.Black, 2 * unite_largeur, 45 * unite_hauteur);
                }

                graphic.DrawString("Page " + page, fnt110, Brushes.Black, 22 * unite_largeur - 5, 53 * unite_hauteur - 5);
                var imageLogo = global::SGSP.Properties.Resources.bas_2;
                graphic.DrawImage(imageLogo, 1 * unite_largeur, 51 * unite_hauteur + 5, 22 * unite_largeur, unite_hauteur + 5);

                return bitmap;

                #endregion

            }
            catch (Exception ex) { GestionPharmacetique.MonMessageBox.ShowBox("", ex); return null; }
        }

        public static Bitmap ImprimerEtatFinanceSemestriel(DataGridView dgvPaiement, string titre, int index)
        {
            #region facture1
            //les dimension de la facture
            int unite_hauteur = 16;
            int unite_largeur = 32;
            int largeur_facture = 35 * unite_largeur;
            int hauteur_facture = 45 * unite_hauteur + 16;//+ 15 + dtGrid.Rows.Count * unite_hauteur;


            //creer un bit map
            var bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            try
            {
                Image logo = global::SGSP.Properties.Resources.Logo;
                graphic.DrawImage(logo, unite_largeur, 20, 12 * unite_largeur, 4 * unite_hauteur - 15);
            }
            catch { }
            //definir les polices 
            Font fnt1 = new Font("Calibri",9, FontStyle.Regular);
            Font fnt33 = new Font("Calibri", 11, FontStyle.Bold);
            Font fnt0 = new Font("Calibri", 9, FontStyle.Bold);
            Font fnt00 = new Font("Calibri", 9, FontStyle.Bold);
            Font fnt11 = new Font("Calibri", 9, FontStyle.Bold);
            Font fnt3 = new Font("Calibri", 15, FontStyle.Bold | FontStyle.Underline);

            #endregion

            graphic.DrawString("Page " + 1, fnt1, Brushes.Black, 33 * unite_largeur, unite_hauteur);
            //graphic.FillRectangle(Brushes.SlateGray,10* unite_largeur, 9 * unite_hauteur + 15, unite_largeur * 15, unite_hauteur + 8);
            graphic.DrawString(titre, fnt3, Brushes.Black, unite_largeur * 2, 5 * unite_hauteur - 10);

            graphic.FillRectangle(Brushes.Lavender, 15, 6 * unite_hauteur + 4, unite_largeur * 35 - 18, 2 * unite_hauteur - 4);

            graphic.DrawString(dgvPaiement.Columns[1].HeaderText, fnt0, Brushes.Black, 20, 6 * unite_hauteur + 6);
            for (var i = 2; i < dgvPaiement.Columns.Count - 1; i++)
            {
                var XLOC = 12 * unite_largeur + i * (2 * unite_largeur);
                var header = dgvPaiement.Columns[i].HeaderText;
                if (header.Length > 10)
                {
                    header = header.Substring(0, 10) + "\n" + header.Substring(10);
                }
                graphic.DrawString(header, fnt0, Brushes.Black, XLOC, 6 * unite_hauteur + 6);
            }


            var j = 0;

            for (var i = index *35; i < dgvPaiement.Rows.Count; i++)
            {
                #region MyRegion

                var YLOC = unite_hauteur * 8 + 3 + unite_hauteur * j;
                graphic.DrawRectangle(Pens.SlateGray, 17, YLOC, unite_largeur * 35 - 22, unite_hauteur);
                if (string.IsNullOrEmpty(dgvPaiement.Rows[i].Cells[0].Value.ToString()) &&
                    !string.IsNullOrEmpty(dgvPaiement.Rows[i].Cells[1].Value.ToString()))
                {
                    graphic.FillRectangle(Brushes.AliceBlue, 18, YLOC + 1, unite_largeur * 35 - 24, unite_hauteur - 1);
                    graphic.DrawString(dgvPaiement.Rows[i].Cells[1].Value.ToString(), fnt0, Brushes.Black, 20, YLOC + 3);

                    for (var y = 2; y < dgvPaiement.Columns.Count - 1; y++)
                    {
                        var XLOC = 12 * unite_largeur + y * (2 * unite_largeur);
                        double montant;
                        if (double.TryParse(dgvPaiement.Rows[i].Cells[y].Value.ToString(), out montant))
                        {
                            graphic.DrawString(String.Format(elGR, "{0:0,0}", montant), fnt0, Brushes.Black, XLOC, YLOC + 3);
                        }
                        else
                        {
                            graphic.DrawString(dgvPaiement.Rows[i].Cells[y].Value.ToString(), fnt0, Brushes.Black, XLOC, YLOC + 3);
                        }
                    }
                }
                else
                {

                    graphic.DrawString(dgvPaiement.Rows[i].Cells[1].Value.ToString(), fnt1, Brushes.Black, 20, YLOC + 3);

                    for (var y = 2; y < dgvPaiement.Columns.Count - 1; y++)
                    {
                        var XLOC = 12 * unite_largeur + y * (2 * unite_largeur);
                        double montant;
                        if (double.TryParse(dgvPaiement.Rows[i].Cells[y].Value.ToString(), out montant))
                        {
                            graphic.DrawString(String.Format(elGR, "{0:0,0}", montant), fnt1, Brushes.Black, XLOC, YLOC + 3);
                        }
                        else
                        {
                            graphic.DrawString(dgvPaiement.Rows[i].Cells[y].Value.ToString(), fnt1, Brushes.Black, XLOC, YLOC + 3);
                        }
                    }
                    #endregion
                }
                j++;
            }
                graphic.FillRectangle(Brushes.White, 0, 43 * unite_hauteur + 4, unite_largeur * 35, unite_hauteur * 6);
                if (dgvPaiement.Rows.Count <= 35*(1+index))
                {
                 var height = (13 + unite_hauteur);
                var YLOC = unite_hauteur * 8 + height * j;

                var index2 = unite_hauteur * 9 + j * unite_hauteur + 15;
                graphic.DrawString("Fait à N'Djaménale  " + DateTime.Now.ToShortDateString(), fnt33, Brushes.Black, 15 * unite_largeur + 10, index2 + 0);
                graphic.DrawString("Le directeur ", fnt33, Brushes.Black, 15 * unite_largeur + 10, index2 + unite_hauteur + 5);
                var dtP = ConnectionClass.ListeDesPersonnelParFonction("DIRECTEUR");
                if (dtP.Rows.Count > 0)
                {
                    graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt33, Brushes.Black, 15 * unite_largeur + 10, index2 + 2 * unite_hauteur + 10);
                }
            }
                return bitmap;
            }

        public static Bitmap ImprimerEtatFinanceTrimestre(DataGridView dgvPaiement, string titre, int index)
        {
            #region facture1
            //les dimension de la facture
            int unite_hauteur = 16;
            int unite_largeur = 32;
            int largeur_facture = 35 * unite_largeur;
            int hauteur_facture = 45 * unite_hauteur + 16;//+ 15 + dtGrid.Rows.Count * unite_hauteur;


            //creer un bit map
            var bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            try
            {
                Image logo = global::SGSP.Properties.Resources.Logo;
                graphic.DrawImage(logo, unite_largeur, 20, 12 * unite_largeur, 4 * unite_hauteur - 15);
            }
            catch { }
            //definir les polices 
            Font fnt1 = new Font("Calibri", 10, FontStyle.Regular);
            Font fnt33 = new Font("Calibri", 11, FontStyle.Bold);
            Font fnt0 = new Font("Calibri", 9, FontStyle.Bold);
            Font fnt00 = new Font("Calibri", 9, FontStyle.Bold);
            Font fnt11 = new Font("Calibri", 9, FontStyle.Bold);
            Font fnt3 = new Font("Calibri", 15, FontStyle.Bold | FontStyle.Underline);

            #endregion

            graphic.DrawString("Page " + 1, fnt1, Brushes.Black, 33 * unite_largeur, unite_hauteur);
            //graphic.FillRectangle(Brushes.SlateGray,10* unite_largeur, 9 * unite_hauteur + 15, unite_largeur * 15, unite_hauteur + 8);
            graphic.DrawString(titre, fnt3, Brushes.Black, unite_largeur * 2, 5 * unite_hauteur - 10);

            graphic.FillRectangle(Brushes.Lavender, 15, 6 * unite_hauteur + 4, unite_largeur * 35 - 18, 2 * unite_hauteur - 4);

            graphic.DrawString(dgvPaiement.Columns[1].HeaderText, fnt0, Brushes.Black, 20, 6 * unite_hauteur + 6);
            for (var i = 2; i < dgvPaiement.Columns.Count - 1; i++)
            {
                var XLOC = 12 * unite_largeur + i * (2 * unite_largeur+25);
                var header = dgvPaiement.Columns[i].HeaderText;
                if (header.Length > 10)
                {
                    ////header = header.Substring(0, 10) + "\n" + header.Substring(10);
                }
                graphic.DrawString(header, fnt0, Brushes.Black, XLOC, 6 * unite_hauteur + 6);
            }


            var j = 0;

            for (var i = index * 35; i < dgvPaiement.Rows.Count; i++)
            {
                #region MyRegion

                var YLOC = unite_hauteur * 8 + 3 + unite_hauteur * j;
                //double totaux;
                //if (double.TryParse(dgvPaiement.Rows[i].Cells[5].Value.ToString(), out totaux))
                //{
                //    if (totaux < 0 || totaux > 0)
                //    {
                //if (string.IsNullOrEmpty(dgvPaiement.Rows[i].Cells[2].Value.ToString()) &&
                //   string.IsNullOrEmpty(dgvPaiement.Rows[i].Cells[3].Value.ToString()) &&
                //!string.IsNullOrEmpty(dgvPaiement.Rows[i].Cells[1].Value.ToString()))
                //{

                //}
                //else
                //{
                    graphic.DrawRectangle(Pens.SlateGray, 17, YLOC, unite_largeur * 35 - 22, unite_hauteur);
                    if (string.IsNullOrEmpty(dgvPaiement.Rows[i].Cells[0].Value.ToString()) &&
                        !string.IsNullOrEmpty(dgvPaiement.Rows[i].Cells[1].Value.ToString()))
                    {
                        graphic.FillRectangle(Brushes.AliceBlue, 18, YLOC + 1, unite_largeur * 35 - 24, unite_hauteur - 1);
                        graphic.DrawString(dgvPaiement.Rows[i].Cells[1].Value.ToString(), fnt0, Brushes.Black, 20, YLOC + 3);

                        for (var y = 2; y < dgvPaiement.Columns.Count - 1; y++)
                        {
                            var XLOC = 12 * unite_largeur + y * (2 * unite_largeur + 25);
                            double montant;
                            if (double.TryParse(dgvPaiement.Rows[i].Cells[y].Value.ToString(), out montant))
                            {
                                graphic.DrawString(String.Format(elGR, "{0:0,0}", montant), fnt0, Brushes.Black, XLOC, YLOC + 3);
                            }
                            else
                            {
                                graphic.DrawString(dgvPaiement.Rows[i].Cells[y].Value.ToString(), fnt0, Brushes.Black, XLOC, YLOC + 3);
                            }
                        }
                    }
                    else
                    {

                        graphic.DrawString(dgvPaiement.Rows[i].Cells[1].Value.ToString(), fnt1, Brushes.Black, 20, YLOC - 0);

                        for (var y = 2; y < dgvPaiement.Columns.Count - 1; y++)
                        {
                            var XLOC = 12 * unite_largeur + y * (2 * unite_largeur + 25);
                            double montant;
                            if (double.TryParse(dgvPaiement.Rows[i].Cells[y].Value.ToString(), out montant))
                            {
                                graphic.DrawString(String.Format(elGR, "{0:0,0}", montant), fnt1, Brushes.Black, XLOC, YLOC + 0);
                            }
                            else
                            {
                                graphic.DrawString(dgvPaiement.Rows[i].Cells[y].Value.ToString(), fnt1, Brushes.Black, XLOC, YLOC + 0);
                            }
                        }
                        #endregion
                    }
                    j++;
                //}
                //}
                //                            else
                //                            {
                //                                graphic.DrawRectangle(Pens.SlateGray, 17, YLOC, unite_largeur* 35 - 22, unite_hauteur);
                //                                if (string.IsNullOrEmpty(dgvPaiement.Rows[i].Cells[0].Value.ToString()) &&
                //                                    !string.IsNullOrEmpty(dgvPaiement.Rows[i].Cells[1].Value.ToString()))
                //                                {
                //                                    graphic.FillRectangle(Brushes.AliceBlue, 18, YLOC + 1, unite_largeur* 35 - 24, unite_hauteur - 1);
                //                                    graphic.DrawString(dgvPaiement.Rows[i].Cells[1].Value.ToString(), fnt0, Brushes.Black, 20, YLOC + 3);

                //                                    for (var y = 2; y<dgvPaiement.Columns.Count - 1; y++)
                //                                    {
                //                                        var XLOC = 12 * unite_largeur + y * (2 * unite_largeur + 25);
                //        double montant;
                //                                        if (double.TryParse(dgvPaiement.Rows[i].Cells[y].Value.ToString(), out montant))
                //                                        {
                //                                            graphic.DrawString(String.Format(elGR, "{0:0,0}", montant), fnt0, Brushes.Black, XLOC, YLOC + 3);
                //                                        }
                //                                        else
                //                                        {
                //                                            graphic.DrawString(dgvPaiement.Rows[i].Cells[y].Value.ToString(), fnt0, Brushes.Black, XLOC, YLOC + 3);
                //                                        }
                //                                    }
                //                                }
                //                                else
                //                                {

                //                                    graphic.DrawString(dgvPaiement.Rows[i].Cells[1].Value.ToString(), fnt1, Brushes.Black, 20, YLOC + 0);

                //                                    for (var y = 2; y<dgvPaiement.Columns.Count - 1; y++)
                //                                    {
                //                                        var XLOC = 12 * unite_largeur + y * (2 * unite_largeur + 25);
                //double montant;
                //                                        if (double.TryParse(dgvPaiement.Rows[i].Cells[y].Value.ToString(), out montant))
                //                                        {
                //                                            graphic.DrawString(String.Format(elGR, "{0:0,0}", montant), fnt1, Brushes.Black, XLOC, YLOC + 0);
                //                                        }
                //                                        else
                //                                        {
                //                                            graphic.DrawString(dgvPaiement.Rows[i].Cells[y].Value.ToString(), fnt1, Brushes.Black, XLOC, YLOC + 0);
                //                                        }
                //                                    }
                //            //#endregion
                //                                }
                //                                j++;

                //}
            }
            graphic.FillRectangle(Brushes.White, 0, 43 * unite_hauteur + 4, unite_largeur * 35, unite_hauteur * 6);
            if (dgvPaiement.Rows.Count <= 35 * (1 + index))
            {
                var height = (13 + unite_hauteur);
                var YLOC = unite_hauteur * 8 + height * j;

                var index2 = unite_hauteur * 9 + j * unite_hauteur + 15;
                graphic.DrawString("Fait à N'Djaménale  " + DateTime.Now.ToShortDateString(), fnt33, Brushes.Black, 15 * unite_largeur + 10, index2 + 0);
                graphic.DrawString("Le directeur ", fnt33, Brushes.Black, 15 * unite_largeur + 10, index2 + unite_hauteur + 5);
                var dtP = ConnectionClass.ListeDesPersonnelParFonction("DIRECTEUR");
                if (dtP.Rows.Count > 0)
                {
                    graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt33, Brushes.Black, 15 * unite_largeur + 10, index2 + 2 * unite_hauteur + 10);
                }
            }
            return bitmap;
        }

        public static Bitmap ImprimerEtatFinanceAnnuel(DataGridView dgvPaiement, string titre, int index)
        {
            #region facture1
            //les dimension de la facture
            int unite_hauteur = 16;
            int unite_largeur = 32;
            int largeur_facture = 35 * unite_largeur;
            int hauteur_facture = 45 * unite_hauteur + 16;//+ 15 + dtGrid.Rows.Count * unite_hauteur;


            //creer un bit map
            var bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            try
            {
                Image logo = global::SGSP.Properties.Resources.Logo;
                graphic.DrawImage(logo, unite_largeur, 20, 12 * unite_largeur, 4 * unite_hauteur - 15);
            }
            catch { }
            //definir les polices 
            Font fnt1 = new Font("Calibri", 7.8f, FontStyle.Regular);
            Font fnt33 = new Font("Calibri", 11, FontStyle.Bold);
            Font fnt0 = new Font("Calibri", 7.8f, FontStyle.Bold);
            Font fnt11 = new Font("Calibri", 9, FontStyle.Bold);
            Font fnt3 = new Font("Calibri", 15, FontStyle.Bold | FontStyle.Underline);

            #endregion

            graphic.DrawString("Page " + 1, fnt1, Brushes.Black, 33 * unite_largeur, unite_hauteur);
            //graphic.FillRectangle(Brushes.SlateGray,10* unite_largeur, 9 * unite_hauteur + 15, unite_largeur * 15, unite_hauteur + 8);
            graphic.DrawString(titre, fnt3, Brushes.Black, unite_largeur * 2, 5 * unite_hauteur - 10);

            graphic.FillRectangle(Brushes.Lavender, 15, 6 * unite_hauteur + 4, unite_largeur * 35 - 18, 2 * unite_hauteur - 4);

            graphic.DrawString(dgvPaiement.Columns[1].HeaderText, fnt0, Brushes.Black, 20, 6 * unite_hauteur + 6);
            for (var i = 2; i < dgvPaiement.Columns.Count - 1; i++)
            {
                var XLOC = 6* unite_largeur + i * (1 * unite_largeur + 23);
                var header = dgvPaiement.Columns[i].HeaderText;
                if (header.Length > 8)
                {
                    header = header.Substring(0, 8) + "\n" + header.Substring(8);
                }
                graphic.DrawString(header, fnt0, Brushes.Black, XLOC, 6 * unite_hauteur + 6);
            }


            var j = 0;

            for (var i = index * 35; i < dgvPaiement.Rows.Count; i++)
            {
                #region MyRegion

                var YLOC = unite_hauteur * 8 + 3 + unite_hauteur * j;
                graphic.DrawRectangle(Pens.SlateGray, 17, YLOC, unite_largeur * 35 - 22, unite_hauteur);
                if (string.IsNullOrEmpty(dgvPaiement.Rows[i].Cells[0].Value.ToString()) &&
                    !string.IsNullOrEmpty(dgvPaiement.Rows[i].Cells[1].Value.ToString()))
                {
                    graphic.FillRectangle(Brushes.AliceBlue, 18, YLOC + 1, unite_largeur * 35 - 24, unite_hauteur - 1);
                    graphic.DrawString(dgvPaiement.Rows[i].Cells[1].Value.ToString(), fnt0, Brushes.Black, 20, YLOC + 3);

                    for (var y = 2; y < dgvPaiement.Columns.Count - 1; y++)
                    {
                        var XLOC = 6 * unite_largeur + y * (1 *unite_largeur + 23);
                        double montant;
                        if (double.TryParse(dgvPaiement.Rows[i].Cells[y].Value.ToString(), out montant))
                        {
                            graphic.DrawString(String.Format(elGR, "{0:0,0}", montant), fnt0, Brushes.Black, XLOC, YLOC + 3);
                        }
                        else
                        {
                            graphic.DrawString(dgvPaiement.Rows[i].Cells[y].Value.ToString(), fnt0, Brushes.Black, XLOC, YLOC + 3);
                        }
                    }
                }
                else
                {

                    graphic.DrawString(dgvPaiement.Rows[i].Cells[1].Value.ToString(), fnt1, Brushes.Black, 20, YLOC + 3);

                    for (var y = 2; y < dgvPaiement.Columns.Count - 1; y++)
                    {
                        var XLOC = 6 * unite_largeur + y * (1 * unite_largeur + 23);
                        double montant;
                        if (double.TryParse(dgvPaiement.Rows[i].Cells[y].Value.ToString(), out montant))
                        {
                            graphic.DrawString(String.Format(elGR, "{0:0,0}", montant), fnt1, Brushes.Black, XLOC, YLOC + 3);
                        }
                        else
                        {
                            graphic.DrawString(dgvPaiement.Rows[i].Cells[y].Value.ToString(), fnt1, Brushes.Black, XLOC, YLOC + 3);
                        }
                    }
                    #endregion
                }
                j++;
            }
            graphic.FillRectangle(Brushes.White, 0, 43 * unite_hauteur + 4, unite_largeur * 35, unite_hauteur * 6);
            if (dgvPaiement.Rows.Count <= 35 * (1 + index))
            {
                var height = (13 + unite_hauteur);
                var YLOC = unite_hauteur * 8 + height * j;

                var index2 = unite_hauteur * 9 + j * unite_hauteur + 15;
                graphic.DrawString("Fait à N'Djaménale  " + DateTime.Now.ToShortDateString(), fnt33, Brushes.Black, 15 * unite_largeur + 10, index2 + 0);
                graphic.DrawString("Le directeur ", fnt33, Brushes.Black, 15 * unite_largeur + 10, index2 + unite_hauteur + 5);
                var dtP = ConnectionClass.ListeDesPersonnelParFonction("DIRECTEUR");
                if (dtP.Rows.Count > 0)
                {
                    graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt33, Brushes.Black, 15 * unite_largeur + 10, index2 + 2 * unite_hauteur + 10);
                }
            }
            return bitmap;
        }

        public static Bitmap ImprimerEtatFinanceMensuel(DataGridView dgvPaiement, string titre, int index)
        {
            #region facture1
            //les dimension de la facture
            int unite_hauteur = 16;
            int unite_largeur = 32;
            int largeur_facture = 24 * unite_largeur;
            int hauteur_facture = 62 * unite_hauteur + 16;//+ 15 + dtGrid.Rows.Count * unite_hauteur;


            //creer un bit map
            var bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            try
            {
                Image logo = global::SGSP.Properties.Resources.Logo;
                graphic.DrawImage(logo, unite_largeur, 20, 12 * unite_largeur, 3* unite_hauteur - 15);
            }
            catch { }
            //definir les polices 
            Font fnt1 = new Font("Calibri", 10f, FontStyle.Regular);
            Font fnt33 = new Font("Calibri", 11, FontStyle.Bold);
            Font fnt0 = new Font("Calibri", 10f, FontStyle.Bold);
            Font fnt11 = new Font("Calibri", 9, FontStyle.Bold);
            Font fnt3 = new Font("Calibri", 15, FontStyle.Bold | FontStyle.Underline);

            #endregion

            graphic.DrawString("Page " + 1, fnt1, Brushes.Black, 22 * unite_largeur, unite_hauteur);
            //graphic.FillRectangle(Brushes.SlateGray,10* unite_largeur, 9 * unite_hauteur + 15, unite_largeur * 15, unite_hauteur + 8);
            graphic.DrawString(titre, fnt3, Brushes.Black, unite_largeur * 2, 4 * unite_hauteur - 10);

            graphic.FillRectangle(Brushes.Lavender, 15, 5 * unite_hauteur + 4, unite_largeur * 24 - 18, 2 * unite_hauteur - 1);

            graphic.DrawString(dgvPaiement.Columns[1].HeaderText, fnt0, Brushes.Black, 20, 5 * unite_hauteur + 6);
            for (var i = 2; i < dgvPaiement.Columns.Count - 1; i++)
            {
                var XLOC = 11 * unite_largeur -15+ i * (2 * unite_largeur + 10);
                var header = dgvPaiement.Columns[i].HeaderText;
                if (header.Length > 9)
                {
                    header = header.Substring(0, 9) + "\n" + header.Substring(8);
                }
                graphic.DrawString(header, fnt0, Brushes.Black, XLOC, 5 * unite_hauteur + 3);
            }


            var j = 0;

            for (var i = index * 55; i < dgvPaiement.Rows.Count; i++)
            {
                #region MyRegion

                var YLOC = unite_hauteur * 7 + 3 + unite_hauteur * j;
                graphic.DrawRectangle(Pens.SlateGray, 17, YLOC, unite_largeur * 24 - 22, unite_hauteur);
                if (string.IsNullOrEmpty(dgvPaiement.Rows[i].Cells[0].Value.ToString()) &&
                    !string.IsNullOrEmpty(dgvPaiement.Rows[i].Cells[1].Value.ToString()))
                {
                    graphic.FillRectangle(Brushes.AliceBlue, 18, YLOC + 1, unite_largeur * 23+7 , unite_hauteur - 1);
                    graphic.DrawString(dgvPaiement.Rows[i].Cells[1].Value.ToString(), fnt0, Brushes.Black, 20, YLOC + 1);

                    for (var y = 2; y < dgvPaiement.Columns.Count - 1; y++)
                    {
                        var XLOC = 11 * unite_largeur -15+ y * (2 * unite_largeur + 10);
                        double montant;
                        if (double.TryParse(dgvPaiement.Rows[i].Cells[y].Value.ToString(), out montant))
                        {
                            graphic.DrawString(String.Format(elGR, "{0:0,0}", montant), fnt0, Brushes.Black, XLOC, YLOC + 1);
                        }
                        else
                        {
                            graphic.DrawString(dgvPaiement.Rows[i].Cells[y].Value.ToString(), fnt0, Brushes.Black, XLOC, YLOC + 1);
                        }
                    }
                }
                else
                {

                    graphic.DrawString(dgvPaiement.Rows[i].Cells[1].Value.ToString(), fnt1, Brushes.Black, 20, YLOC + 1);

                    for (var y = 2; y < dgvPaiement.Columns.Count - 1; y++)
                    {
                        var XLOC = 11 * unite_largeur -15+ y * (2 * unite_largeur + 10);
                        double montant;
                        if (double.TryParse(dgvPaiement.Rows[i].Cells[y].Value.ToString(), out montant))
                        {
                            graphic.DrawString(String.Format(elGR, "{0:0,0}", montant), fnt1, Brushes.Black, XLOC, YLOC + 1);
                        }
                        else
                        {
                            graphic.DrawString(dgvPaiement.Rows[i].Cells[y].Value.ToString(), fnt1, Brushes.Black, XLOC, YLOC +1);
                        }
                    }
                    #endregion
                }
                j++;
            }
            graphic.FillRectangle(Brushes.White, 0, 62 * unite_hauteur + 4, unite_largeur * 25, unite_hauteur * 6);
            if (dgvPaiement.Rows.Count <= 55 * (1 + index))
            {
                var height = (13 + unite_hauteur);
                var YLOC = unite_hauteur * 8 + height * j;

                var index2 = unite_hauteur * 9 + j * unite_hauteur + 15;
                graphic.DrawString("Fait à N'Djaménale  " + DateTime.Now.ToShortDateString(), fnt33, Brushes.Black, 10 * unite_largeur + 10, index2 + 0);
                graphic.DrawString("Le directeur ", fnt33, Brushes.Black, 10 * unite_largeur + 10, index2 + unite_hauteur + 5);
                var dtP = ConnectionClass.ListeDesPersonnelParFonction("DIRECTEUR");
                if (dtP.Rows.Count > 0)
                {
                    graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt33, Brushes.Black, 10 * unite_largeur + 10, index2 + 2 * unite_hauteur + 10);
                }
            }
            return bitmap;
        }

        public static Bitmap ImprimerBilanFinancier(DataGridView dataGridView, string titre, int start)
        {
            //les dimension de la facture
            #region
            int unite_hauteur = 16;
            int unite_largeur = 32;
            int largeur_facture = 26 * unite_largeur;
            int hauteur_facture = 60 * unite_hauteur;

            Bitmap bitmap = new Bitmap(largeur_facture + 1, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            #endregion
            try
            {
                Image logo = global::SGSP.Properties.Resources.Logo;
                graphic.DrawImage(logo, 4*unite_largeur, 10, 12 * unite_largeur, 3 * unite_hauteur);
            }
            catch { } //definir les polices 
            Font fnt1 = new Font("Arial Unicode MS", 10, FontStyle.Regular);
            Font fnt3 = new Font("Arial Unicode MS", 12, FontStyle.Bold);
            Font fnt33 = new Font("Arial Unicode MS", 10, FontStyle.Bold);
            graphic.DrawString("Page " + (1 + start).ToString(), fnt1, Brushes.Black, 20 * unite_largeur, 5);

            graphic.DrawString(titre, fnt3, Brushes.Black, 5 * unite_largeur, 4 * unite_hauteur + 5);

            graphic.FillRectangle(Brushes.Lavender, 5 * unite_largeur, 6 * unite_hauteur - 4, 10 * unite_largeur-3, unite_hauteur);
            graphic.FillRectangle(Brushes.Lavender, 15 * unite_largeur, 6 * unite_hauteur - 4, 5 * unite_largeur, unite_hauteur);
            graphic.DrawString("Désignation ", fnt33, Brushes.Black, 6 * unite_largeur, 6 * unite_hauteur - 4);
            graphic.DrawString("Montant ", fnt33, Brushes.Black, 16 * unite_largeur + 10, 6 * unite_hauteur - 4);
            var j = 0;
            for (int i = 50 * start; i <= dataGridView.Rows.Count - 2; i++)
            {
                int Yloc = unite_hauteur * j + 7 * unite_hauteur;
           
                if (string.IsNullOrWhiteSpace(dataGridView.Rows[i].Cells[0].Value.ToString()))
                {
                    if (dataGridView.Rows[i].Cells[3].Value.ToString() == "B-PASSIF" || dataGridView.Rows[i].Cells[3].Value.ToString() == "A-ACTIF")
                    {
                        graphic.FillRectangle(Brushes.Lavender, 5 * unite_largeur, Yloc, 15 * unite_largeur - 3, unite_hauteur);
                        graphic.DrawString(dataGridView.Rows[i].Cells[3].Value.ToString(), fnt33, Brushes.Black, 6 * unite_largeur, Yloc);
                        graphic.DrawString(dataGridView.Rows[i].Cells[4].Value.ToString(), fnt33, Brushes.Black, 16 * unite_largeur, Yloc);
                    }else if (dataGridView.Rows[i].Cells[3].Value.ToString() == "" &&  dataGridView.Rows[i].Cells[4].Value.ToString() == "")
                    {
                    }
                    else
                    {
                        graphic.DrawRectangle(Pens.Black, 5 * unite_largeur, Yloc, 10 * unite_largeur - 3, unite_hauteur);
                        graphic.DrawRectangle(Pens.Black, 15 * unite_largeur, Yloc, 5 * unite_largeur - 3, unite_hauteur);
                        graphic.DrawString(dataGridView.Rows[i].Cells[3].Value.ToString(), fnt33, Brushes.Black, 6 * unite_largeur, Yloc);
                        graphic.DrawString(dataGridView.Rows[i].Cells[4].Value.ToString(), fnt33, Brushes.Black, 16 * unite_largeur, Yloc);
                    }
                }
                else
                {
                    graphic.DrawRectangle(Pens.Black, 5 * unite_largeur, Yloc, 10 * unite_largeur - 3, unite_hauteur);
                    graphic.DrawRectangle(Pens.Black, 15 * unite_largeur, Yloc, 5 * unite_largeur - 3, unite_hauteur);
                    graphic.DrawString(dataGridView.Rows[i].Cells[3].Value.ToString(), fnt1, Brushes.Black, 6 * unite_largeur, Yloc);
                    graphic.DrawString(dataGridView.Rows[i].Cells[4].Value.ToString(), fnt1, Brushes.Black, 16 * unite_largeur, Yloc);
                }
                j++;
            }
            graphic.FillRectangle(Brushes.White, 5, 57 * unite_hauteur + 1, 24 * unite_largeur, 10 * unite_hauteur);
            if (dataGridView.Rows.Count <= 45 * (1 + start))
            {
                var height = (10 + unite_hauteur);
                var YLOC = unite_hauteur * 8 + height * j;

                var index2 = unite_hauteur * 9 + j * unite_hauteur + 15;
                graphic.DrawString("Fait à N'Djaménale  " + DateTime.Now.ToShortDateString(), fnt33, Brushes.Black, 9 * unite_largeur + 10, index2 + 0);
                graphic.DrawString("Le directeur ", fnt33, Brushes.Black, 9 * unite_largeur + 10, index2 + unite_hauteur * 4);
                var dtP = ConnectionClass.ListeDesPersonnelParFonction("DIRECTEUR");
                if (dtP.Rows.Count > 0)
                {
                    graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt33, Brushes.Black, 9 * unite_largeur + 10, index2 + 5 * unite_hauteur + 10);
                }
            }
            return bitmap;
        }

        public static Bitmap ImprimerOrdreMission(Mission mission, List<Mission> listeMissionnaire)
        {
            #region
            int unite_hauteur = 20;
            int unite_largeur = 32;
            int largeur_facture = 26* unite_largeur + 0;
            int detail_hauteur_facture = 10 * unite_hauteur;
            int hauteur_facture = 62* unite_hauteur;

            //creer un bit map
            Bitmap bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            #endregion
            try
            {
                Image logo = global::SGSP.Properties.Resources.ordreMission;
                graphic.DrawImage(logo, 0, 0,largeur_facture, hauteur_facture);
            }

            catch { } //definir les polices 
            var drawFormatCenter = new StringFormat();
            drawFormatCenter.Alignment = StringAlignment.Center;
            var drawFormatLeft = new StringFormat();
            drawFormatLeft.Alignment = StringAlignment.Near;
            var drawFormatRight = new StringFormat();
            drawFormatRight.Alignment = StringAlignment.Far;
            Font fnt1 = new Font("Century Gothic", 12, FontStyle.Bold);
            Font fnt0 = new Font("Century Gothic", 12, FontStyle.Bold | FontStyle.Underline);
            Font fnt11 = new Font("Century Gothic", 15, FontStyle.Bold);
            Font fnt3 = new Font("Century Gothic", 19, FontStyle.Bold);
            Font fnt33 = new Font("Century Gothic", 12, FontStyle.Regular);
            Font fnt2 = new Font("Century Gothic", 11, FontStyle.Underline);
            // dessiner les ecritures 
            graphic.DrawString("ORDRE DE MISSION " , fnt3, Brushes.Black, 12 * unite_largeur +16,  0+unite_hauteur * 18,drawFormatCenter);
           graphic.DrawString("IL EST ORDONNE A : ", fnt1, Brushes.Black, 2 * unite_largeur +15, 10+unite_hauteur * 19);

            var j = 0;
            foreach (var m in listeMissionnaire)
            {
                var YLOC = 21* unite_hauteur+j*unite_hauteur;
                graphic.DrawString(" -    " + m.NomEmploye.ToUpper() + ", " + m.Role, fnt1, Brushes.Black, 4 * unite_largeur + 0, YLOC);
                j++;
            }
            var unit = unite_hauteur +5;
            var ZLOC = 22* unite_hauteur+0 + listeMissionnaire.Count * unite_hauteur;
            graphic.DrawString("DE SE RENDRE A : ", fnt1, Brushes.Black, 2 * unite_largeur + 15, ZLOC);
            graphic.DrawString("OBJET DE LA MISSION :  ", fnt1, Brushes.Black, 2 * unite_largeur + 15, ZLOC+unit);
            graphic.DrawString("DATE DE DEPART : ", fnt1, Brushes.Black, 2 * unite_largeur + 15, ZLOC+2*unit);
            graphic.DrawString("DATE DE RETOUR  : ", fnt1, Brushes.Black, 2 * unite_largeur + 15, ZLOC+3*unit);
            graphic.DrawString("MOYEN DE TRANSPORT : ", fnt1, Brushes.Black, 2 * unite_largeur + 15, ZLOC+4*unit);
            graphic.DrawString("IMPUTATION BUDGETAIRE  : ", fnt1, Brushes.Black, 2 * unite_largeur + 15, ZLOC + 5 * unit);
            graphic.DrawString(mission.Destination, fnt33, Brushes.Black, 7* unite_largeur -12, ZLOC);
            graphic.DrawString(mission.Objet, fnt33, Brushes.Black, 8 * unite_largeur + 0, ZLOC + unit);
            graphic.DrawString(mission.DateDepart.ToShortDateString(), fnt33, Brushes.Black, 6 * unite_largeur + 23, ZLOC + 2 * unit);
            graphic.DrawString(mission.DateRetour.ToShortDateString(), fnt33, Brushes.Black, 6 * unite_largeur + 28, ZLOC + 3 * unit);
            graphic.DrawString(mission.Transport, fnt33, Brushes.Black, 8* unite_largeur + 10, ZLOC + 4 * unit);
            graphic.DrawString(mission.Imputation, fnt33, Brushes.Black, 9 * unite_largeur + 5, ZLOC + 5 * unit);

            graphic.DrawString("Fait à N'Djaména le  " + DateTime.Now.ToShortDateString(), fnt33, Brushes.Black, 15* unite_largeur + 10, ZLOC + unit *7);
            graphic.DrawString("Le directeur  Général de l'INSEED", fnt33, Brushes.Black, 8* unite_largeur + 18, ZLOC + unit  *8+15);
            var listePersonnel = from p in ConnectionClass.ListePersonnel("")
                                 join s in ConnectionClass.ListeServicePersonnel()
                                 on p.NumeroMatricule equals s.NumeroMatricule
                                 where s.Poste == "Directeur Général"
                                 select new { p.Nom, p.Prenom };
            foreach (var p in listePersonnel)
            {
                graphic.DrawString(p.Nom + " " + p.Prenom, fnt0, Brushes.Black, 8 * unite_largeur + 10, ZLOC + 13 * unit + 0);
            }
            
            return bitmap;
        }


        public static Bitmap ListeEmargementDeLaMission(Mission mission, List<Mission> listeMissionnaire)
        {
            #region
            int unite_hauteur = 20;
            int unite_largeur = 32;
            int largeur_facture = 25 * unite_largeur + 0;
            int detail_hauteur_facture = 10 * unite_hauteur;
            int hauteur_facture = 56 * unite_hauteur;

            //creer un bit map
            Bitmap bitmap = new Bitmap(largeur_facture, hauteur_facture, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            // creer un objet graphic
            Graphics graphic = Graphics.FromImage(bitmap);

            //la couleur de l'image
            graphic.Clear(Color.White);
            #endregion
            try
            {
                Image logo = global::SGSP.Properties.Resources.Logo;
                graphic.DrawImage(logo, unite_largeur, 10, 13 * unite_largeur, 5 * unite_hauteur);
            }
            catch { } //definir les polices 
            var drawFormatCenter = new StringFormat();
            drawFormatCenter.Alignment = StringAlignment.Center;
            var drawFormatLeft = new StringFormat();
            drawFormatLeft.Alignment = StringAlignment.Near;
            var drawFormatRight = new StringFormat();
            drawFormatRight.Alignment = StringAlignment.Far;
            Font fnt1 = new Font("Century Gothic", 12, FontStyle.Bold);
            Font fnt0 = new Font("Century Gothic", 12, FontStyle.Bold | FontStyle.Underline);
            Font fnt11 = new Font("Century Gothic", 15, FontStyle.Bold);
            Font fnt3 = new Font("Century Gothic", 19, FontStyle.Bold);
            Font fnt33 = new Font("Century Gothic", 11.5F, FontStyle.Regular);
            // dessiner les ecritures 
            graphic.DrawString("Liste des membres de la mission "+mission.Objet, fnt0, Brushes.Black, unite_largeur, 5 * unite_hauteur + 3);

            graphic.FillRectangle(Brushes.Lavender, 17, 7 * unite_hauteur - 2, 1 * unite_largeur, unite_hauteur * 2);
            graphic.FillRectangle(Brushes.Lavender, 19 + unite_largeur, 7 * unite_hauteur - 2, 10 * unite_largeur + 11, unite_hauteur * 2);
            graphic.FillRectangle(Brushes.Lavender, unite_largeur * 12, 7 * unite_hauteur - 2, 4 * unite_largeur + 15, unite_hauteur * 2);
            graphic.FillRectangle(Brushes.Lavender, unite_largeur * 16+17, 7 * unite_hauteur - 2, 4 * unite_largeur - 21, unite_hauteur * 2);
            graphic.FillRectangle(Brushes.Lavender, unite_largeur * 20, 7 * unite_hauteur - 2, 5* unite_largeur - 2, unite_hauteur * 2);


            graphic.DrawString("N° ", fnt1, Brushes.Black, 20, 7 * unite_hauteur + 10);
            graphic.DrawString("Nom & prenom", fnt1, Brushes.Black, unite_largeur + 25, 7 * unite_hauteur + 10);
            graphic.DrawString("Role ", fnt1, Brushes.Black, unite_largeur * 13 + 15, 7 * unite_hauteur + 10);
            graphic.DrawString("Montant ", fnt1, Brushes.Black, unite_largeur * 20 -12, 7 * unite_hauteur + 10,drawFormatRight);
            graphic.DrawString("Signature ", fnt1, Brushes.Black, 21 * unite_largeur - 5, 7 * unite_hauteur + 10);

            for (int i = 0; i <listeMissionnaire.Count; i++)
            {
                var YLOC = 9 * unite_hauteur + i * 35;
                var rect = new RectangleF(12 * unite_largeur, YLOC+3, 4 * unite_largeur + 15, 33);
                graphic.DrawRectangle(Pens.Black, 17, YLOC, 1 * unite_largeur, 33);
                graphic.DrawRectangle(Pens.Black, 20 + unite_largeur, YLOC, 10 * unite_largeur + 10, 33);
                graphic.DrawRectangle(Pens.Black, unite_largeur * 12, YLOC, 4 * unite_largeur +14, 33);
                graphic.DrawRectangle(Pens.Black, unite_largeur * 16+17, YLOC, 4 * unite_largeur - 20, 33);
                graphic.DrawRectangle(Pens.Black, unite_largeur * 20, YLOC, 5 * unite_largeur - 2, 33);

                graphic.DrawString((i + 1).ToString(), fnt33, Brushes.Black, 20, YLOC + 3);
                graphic.DrawString(listeMissionnaire[i].NomEmploye.ToUpper(), fnt33, Brushes.Black, unite_largeur + 25, YLOC + 3);
                graphic.DrawString(listeMissionnaire[i].Role.ToUpper(), fnt33,Brushes.Black, rect , drawFormatCenter );
                graphic.DrawString(string.Format(elGR, "{0:0,0}", listeMissionnaire[i].FraisTotal), fnt33, Brushes.Black, unite_largeur * 20 -12, YLOC + 3,drawFormatRight);

            }
            var LOC = 9 * unite_hauteur + 3 + listeMissionnaire.Count * 35;
            //graphic.DrawRectangle(Pens.Black, 17, LOC, 23 * unite_largeur + 15, 2 * unite_hauteur);
            //graphic.DrawString("Total", fnt11, Brushes.Black, unite_largeur + 10, LOC + 12);
            //graphic.DrawString(string.Format(elGR, "{0:0,0}", total), fnt11, Brushes.Black, 18 * unite_largeur + 12, LOC + 12);

            graphic.DrawString("Fait à N'Djaménale  " + DateTime.Now.ToShortDateString(), fnt1, Brushes.Black, 8 * unite_largeur + 10, LOC + unite_hauteur * 6 - 10);
            //graphic.DrawString("Le directeur ", fnt11, Brushes.Black, 10 * unite_largeur + 10, LOC + unite_hauteur * 7);
            //var dtP = ConnectionClass.ListeDesPersonnelParFonction("DIRECTEUR");
            //if (dtP.Rows.Count > 0)
            //{
            //    graphic.DrawString(dtP.Rows[0].ItemArray[1].ToString() + " " + dtP.Rows[0].ItemArray[2].ToString(), fnt11, Brushes.Black, 8 * unite_largeur + 10, LOC + 13 * unite_hauteur + 10);
            //}
            return bitmap;
        }

    }
}
