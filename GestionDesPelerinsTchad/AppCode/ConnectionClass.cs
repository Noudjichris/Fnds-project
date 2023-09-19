using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;

using System.Windows.Forms;
using GestionPharmacetique;

namespace SGSP.AppCode
{
    public class ConnectionClass
    {
        private static MySqlConnection _connection;
        private static MySqlCommand _command;
        private static MySqlTransaction _transaction = null;

        static ConnectionClass()
        {
            var connectionString =
               //@"server=192.168.11.80;port=3306;user id=inseed;password=inseed2021;database=personnel_db";
            //@"server=192.168.11.220;port=3306;user id=chris;password=chris@2020;database=personnel_db";
                @"server=localhost;port=3306;user id=root;password=chris@2022;database=personnel_db";
            _connection = new MySqlConnection(connectionString);
            _command = new MySqlCommand("", _connection);
        }

        public static void Backup(string sFilePath)
        {
            try
            {
                using (MySqlBackup mb = new MySqlBackup(_command))
                {
                    _connection.Open();
                    mb.ExportToFile(sFilePath);
                    _connection.Close();
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("back up files", ex);
            }
        }
       
        #region UTILISATEUR
        // ecrire la methode qui permet d' authentifier l'employee quand il veux se connecter
        public static Utilisateur SeConnecter(string nomUtil, string motPasse)
        {
            Utilisateur utilisateur = null;

            try
            {
                if (!string.IsNullOrEmpty(nomUtil))
                {
                    _connection.Open();
                    var query = string.Format(
                        "SELECT mot_passe FROM util_tbl WHERE nom_util = '{0}' ", nomUtil);
                    _command.CommandText = query;
                    var dbMotPassword = _command.ExecuteScalar().ToString().Trim();

                    if (dbMotPassword == motPasse)
                    {
                        query =
                            string.Format("SELECT *  from util_tbl" +
                            " WHERE  (nom_util ='{0}')", nomUtil);
                        _command.CommandText = query;
                        var reader = _command.ExecuteReader();
                        while (reader.Read())
                        {
                            var numMat = !reader.IsDBNull(0) ? reader.GetString(0) : "";
                            var typeUtil = reader.GetString(3).Trim();
                            var nom = "";
                            var prenom = "";
                            var numUtil = reader.GetInt32(0);
                            var photo = "";
                            utilisateur = new Utilisateur(numUtil, nomUtil, motPasse, typeUtil, numMat, nom, prenom, photo);
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                //MonMessageBox.ShowBox("l'authentification a échouée. Veuiller réessayer.", "Erreur d'authentification",ex);
            }
            finally
            {
                _connection.Close();
            }
            return utilisateur;
        }

        //obtenir la liste des utilisateurs
        public static List<Utilisateur> ListesDesUtilisateurs(int numUtilisateur, string motPasses)
        {
            var listeUtilisateur = new List<Utilisateur>();
            try
            {
                _connection.Open();
                var query = "SELECT * FROM util_tbl WHERE num_util = " + numUtilisateur + " AND mot_passe ='" + motPasses + "'";
                _command.CommandText = query;
                var reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var nomUtil = reader.GetString(1);
                    var motPasse = reader.GetString(2);
                    var type = reader.GetString(3);

                    var utilisateur = new Utilisateur(numUtilisateur, nomUtil, motPasse, type);
                    listeUtilisateur.Add(utilisateur);

                }
            }
            catch (Exception) { }
            finally
            {
                _connection.Close();
            }
            return listeUtilisateur;
        }
        //obtenir la liste des utilisateurs
        public static List<Utilisateur> ListesDesUtilisateurs()
        {
            var listeUtilisateur = new List<Utilisateur>();
            try
            {
                _connection.Open();
                var query = "SELECT * FROM util_tbl ";
                _command.CommandText = query;
                var reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var numeroUtilsateur = reader.GetInt32(0);
                    var nomUtil = reader.GetString(1);
                    var motPasse = reader.GetString(2);
                    var type = reader.GetString(3);

                    var utilisateur = new Utilisateur(numeroUtilsateur, nomUtil, motPasse, type);
                    listeUtilisateur.Add(utilisateur);

                }
            }
            catch (Exception) { }
            finally
            {
                _connection.Close();
            }
            return listeUtilisateur;
        }

        //liste des utilisateurs
        public static DataTable ListeUtilisteur()
        {
            var dt = new DataTable();
            try
            {
                _connection.Open();
                var requete =
                    "SELECT  util_tbl.num_util, pers_tbl.nom, pers_tbl.prenom, util_tbl.nom_util, util_tbl.mot_passe," +
                    " util_tbl.type_util, util_tbl.num_mat FROM pers_tbl INNER JOIN util_tbl ON pers_tbl.num_mat = util_tbl.num_mat";
                _command.CommandText = requete;
                var reader = _command.ExecuteReader();
                dt.Load(reader);
            }
            finally
            {
                _connection.Close();

            }
            return dt;
        }
        public static bool ListeUtilisateur(string nomUtilisateur, string motPasse)
        {
            var flag = false;
            try
            {
                _connection.Open();
                var query = string.Format(
                    "SELECT mot_pass FROM util_tbl WHERE nom_util = '{0}' ", nomUtilisateur);
                _command.CommandText = query;
                var dbMotPassword = _command.ExecuteScalar().ToString().Trim();
                if (motPasse.ToLower() == dbMotPassword.ToLower())
                {
                    flag = true;
                }
                else
                {
                    GestionPharmacetique.MonMessageBox.ShowBox("Le mot de passe n'est pas correct. Veuillez reessayer.", "Erreur");
                    flag = false;
                }
            }
            finally
            {
                _connection.Close();
            }
            return flag;
        }
        //ajouter un utilisateur
        public static bool AjouterUnUtilisateur(Utilisateur utilisateur)
        {
            var flag = false;
            try
            {
                _connection.Open();
                var requete = "SELECT * FROM `util_tbl` WHERE `num_mat` = @num_mat";
                _command.CommandText = requete;
                _command.Parameters.Add(new MySqlParameter("num_mat", utilisateur.Matricule));
                var reader = _command.ExecuteReader();
                if (!reader.HasRows)
                {
                    reader.Close();
                    requete = string.Format("INSERT INTO `util_tbl` (`nom_util`, `mot_passe`, `type_util`,`num_mat`)" +
                                            " VALUES ('{0}','{1}','{2}',@num_mat1)",
                        utilisateur.NomUtilsateur, utilisateur.MotdePasse, utilisateur.TypeUtilisateur,
                        utilisateur.Matricule);
                    _command.CommandText = requete;
                    _command.Parameters.Add(new MySqlParameter("num_mat1", utilisateur.Matricule));
                    _command.ExecuteNonQuery();
                    MonMessageBox.ShowBox("Nouveau utilisateur ajouté avec succés", "Enregistrement utilisateur");
                    flag = true;
                }
                else
                {
                    MonMessageBox.ShowBox("Ce personnel est deja enregistré", "Erreur");
                }
            }
            catch (Exception exception)
            {
                flag = false;
                MonMessageBox.ShowBox("L'enregistrement de l'utilisateur a échoué", "Ajouter utilisateur", exception);
            } finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
            return flag;
        }

        //modifier un utilisateur
        public static bool ModifierUnUtilisateur(Utilisateur utilisateur)
        {
            bool flag = false;
            try
            {
                string requete = string.Format("UPDATE util_tbl SET nom_util = '{0}', mot_passe = '{1}', type_util = '{2}' WHERE (num_util = {3})",
                    utilisateur.NomUtilsateur, utilisateur.MotdePasse, utilisateur.TypeUtilisateur, utilisateur.NumUtilisateur);
                _command.CommandText = requete;
                _connection.Open();
                _command.ExecuteNonQuery();
                MonMessageBox.ShowBox("Compte d'utilisateur modifié avec succés", "Modification utilisateur");
                flag = true;
            }
            catch (Exception exception)
            {
                flag = false;
                MonMessageBox.ShowBox("L'enregistrement de l'utilisateur a échoué", "Ajouter utilisateur", exception);
            }
            finally
            {
                _connection.Close();
            }
            return flag;
        }

        //modifier un utilisateur
        public static bool ModifierUnUtilisateur(string nomUtilisateur, string motPasse)
        {
            var flag = false;
            try
            {
                var requete = string.Format("UPDATE util_tbl SET mot_pass = '{0}' WHERE (nom_util = '{1}')",
                   motPasse, nomUtilisateur);
                _command.CommandText = requete;
                _connection.Open();
                _command.ExecuteNonQuery();
                MonMessageBox.ShowBox("le mot de passe a été modifié avec succés", "Modification utilisateur");
                flag = true;
            }
            catch (Exception exception)
            {
                flag = false;
                MonMessageBox.ShowBox("L'enregistrement de l'utilisateur a échoué", "Ajouter utilisateur", exception);
            }
            finally
            {
                _connection.Close();
            }
            return flag;
        }

        //modifier un utilisateur
        public static bool AccorderPrivileges(int id, string type)
        {
            var flag = false;
            try
            {
                var requete = string.Format("UPDATE util_tbl SET type_util = '{0}' WHERE (num_util = {1})",
                   type, id);
                _command.CommandText = requete;
                _connection.Open();
                _command.ExecuteNonQuery();
                flag = true;
            }
            catch (Exception exception)
            {
                flag = false;
                MonMessageBox.ShowBox("L'enregistrement de l'utilisateur a échoué", "Ajouter utilisateur", exception);
            }
            finally
            {
                _connection.Close();
            }
            return flag;
        }

        //ajouter un utilisateur
        public static void SupprimerUnUtilisateur(int numeroUtil)
        {
            try
            {
                string requete = string.Format("DELETE FROM util_tbl  WHERE num_util = {0}", numeroUtil);
                _command.CommandText = requete;
                _connection.Open();
                _command.ExecuteNonQuery();
                MonMessageBox.ShowBox("Données d'utilisateur supprimées avec succés", "Suppression utilisateur");
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("La Suppression de l'utilisateur a échoué", "Suppression utilisateur", exception);
            }
            finally
            {
                _connection.Close();
            }
        }
        #endregion
 //Modifier LES PERSONNELS
        #region PERSONNEL_ET_AUTRES
        //AJOUTER LES PERSONNELS
        public static bool AjouterUnPersonnel(Personnel personnel, Service service, Salaire salaire, Banque bank)
        {
            var flag = false;
            try
            {
                _connection.Open();
                _transaction = _connection.BeginTransaction();
                var requete = "INSERT INTO `pers_tbl` (`num_mat`, `nom`, `prenom`, `date_nai`, `lieu_nai`, `addres`, `tele1`, `tele2`, `email`, `sexe`, " +
                    "  `photo`,`age`,situation_mat,nbrEnfant,no_piece,type_piece) VALUES (@num_mat01, @nom, @prenom, @date_nai,@lieu_nai," +
                "@addres, @tele1, @tele2, @email, @sexe, @photo,@age,@situation_mat,@nbrEnfant,@no_piece,@type_piece)";
                _command.Parameters.Add(new MySqlParameter("num_mat01", personnel.NumeroMatricule));
                _command.Parameters.Add(new MySqlParameter("nom", personnel.Nom));
                _command.Parameters.Add(new MySqlParameter("prenom", personnel.Prenom));
                _command.Parameters.Add(new MySqlParameter("date_nai", personnel.DateNaissance));
                _command.Parameters.Add(new MySqlParameter("addres", personnel.Adresse));
                _command.Parameters.Add(new MySqlParameter("tele1", personnel.Telephone1));
                _command.Parameters.Add(new MySqlParameter("tele2", personnel.Telephone2));
                _command.Parameters.Add(new MySqlParameter("sexe", personnel.Sexe));
                _command.Parameters.Add(new MySqlParameter("email", personnel.Email));
                _command.Parameters.Add(new MySqlParameter("lieu_nai", personnel.LieuNaissance));
                _command.Parameters.Add(new MySqlParameter("photo", personnel.Photo));
                _command.Parameters.Add(new MySqlParameter("age", personnel.Age));
                _command.Parameters.Add(new MySqlParameter("situation_mat", personnel.SituationMatrimoniale));
                _command.Parameters.Add(new MySqlParameter("nbrEnfant", personnel.NombreEnfant));
                _command.Parameters.Add(new MySqlParameter("no_piece", personnel.NumeroPiece));
                _command.Parameters.Add(new MySqlParameter("type_piece", personnel.TypePiece));
                _command.CommandText = requete;
                _command.Transaction = _transaction;
                _command.Connection = _connection;
                _command.ExecuteNonQuery();

                requete = "INSERT INTO `service_tbl` (`date_pris`, `poste`, `num_mat`, `echelon`,`categorie`,`anciennete`,`no_cnps`,`diplome`,`contrat`,`status`,date_depart,grade,id_dep,id_div,cnrt,localisation)" +
                    " VALUES (@date_pris, @poste, @num_mat1, @echelon,@categorie,@anciennete,@no_cnps,@diplome,@contrat,@status,@date_depart,@grade,@id_dep,@id_div,@cnrt,@localisation)";
                _command.Parameters.Add(new MySqlParameter("date_pris", service.DateService));
                _command.Parameters.Add(new MySqlParameter("poste", service.Poste));
                _command.Parameters.Add(new MySqlParameter("num_mat1", service.NumeroMatricule));
                _command.Parameters.Add(new MySqlParameter("echelon", service.Echelon));
                _command.Parameters.Add(new MySqlParameter("categorie", service.Categorie));
                _command.Parameters.Add(new MySqlParameter("anciennete", service.Anciennete));
                _command.Parameters.Add(new MySqlParameter("contrat", service.TypeContrat));
                _command.Parameters.Add(new MySqlParameter("status", service.Status));
                _command.Parameters.Add(new MySqlParameter("no_cnps", service.NoCNPS));
                _command.Parameters.Add(new MySqlParameter("diplome", service.Diplome));
                _command.Parameters.Add(new MySqlParameter("date_depart", service.DateDepart));
                _command.Parameters.Add(new MySqlParameter("id_dep", service.IDDepartement));
                _command.Parameters.Add(new MySqlParameter("id_div", service.IDDivision));
                _command.Parameters.Add(new MySqlParameter("grade", service.Grade));
                _command.Parameters.Add(new MySqlParameter("cnrt", service.SiCNRT));
                _command.Parameters.Add(new MySqlParameter("localisation", service.Localite));
                _command.Connection = _connection;
                _command.Transaction = _transaction;
                _command.CommandText = requete;
                _command.ExecuteNonQuery();

                requete = "INSERT INTO `salaire_tbl` (`salaire`, `grille`, `num_mat`, `indice`,prime_loge,prime_grd,prime_respo,heure_supp,ind_transp)" +
                   " VALUES (@salaire, @grille, @num_mat12, @indice,@prime_loge,@prime_grd,@prime_respo,@heure_supp,@ind_transp)";
                _command.Parameters.Add(new MySqlParameter("salaire", salaire.SalaireBase));
                _command.Parameters.Add(new MySqlParameter("grille", salaire.GrilleSalarialle));
                _command.Parameters.Add(new MySqlParameter("num_mat12", personnel.NumeroMatricule));
                _command.Parameters.Add(new MySqlParameter("indice", salaire.Indice));
                _command.Parameters.Add(new MySqlParameter("prime_loge", salaire.Indemnites));
                _command.Parameters.Add(new MySqlParameter("prime_grd", salaire.AutresPrimes));
                _command.Parameters.Add(new MySqlParameter("prime_respo", salaire.PrimeMotivation));
                _command.Parameters.Add(new MySqlParameter("heure_supp", salaire.FraisCommunication));
                _command.Parameters.Add(new MySqlParameter("ind_transp", salaire.PrimeTransport));

                _command.Connection = _connection;
                _command.Transaction = _transaction;
                _command.CommandText = requete;
                _command.ExecuteNonQuery();

                requete = "INSERT INTO `bank_data_tbl` (`num_mat`, `no_compte`,code_banc,code_gui,cle,bank,etat) VALUES (@num_mat2,@no_compte,@code_banc,@code_gui,@cle,@bank,@etat)";
                _command.Parameters.Add(new MySqlParameter("no_compte", bank.Compte));
                _command.Parameters.Add(new MySqlParameter("code_banc", bank.CodeBanque));
                _command.Parameters.Add(new MySqlParameter("code_gui", bank.CodeGuichet));
                _command.Parameters.Add(new MySqlParameter("cle", bank.Cle));
                _command.Parameters.Add(new MySqlParameter("bank", bank.NomBanque));
                _command.Parameters.Add(new MySqlParameter("etat", true));
                _command.Parameters.Add(new MySqlParameter("num_mat2", personnel.NumeroMatricule));
                _command.Connection = _connection;
                _command.Transaction = _transaction;
                _command.CommandText = requete;
                _command.ExecuteNonQuery();

                _transaction.Commit();
                MonMessageBox.ShowBox("Nouveau personnel est inseré avec succés dans la base de données", "Enregistrement personnel");
                flag = true;
            }
            catch (Exception exception)
            {
                if (_transaction != null)
                    _transaction.Rollback();
                MonMessageBox.ShowBox("L'enregistrement du personnel a échoué", "Enregistrement personnel", exception);
                flag = false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
            return flag;
        }

        //AJOUTER LES PERSONNELS
        public static bool InsereImage(string numeroMatricule, string image)
        {
            var flag = false;
            try
            {
                _connection.Open();
                var requete = "UPDATE `pers_tbl` SET  `photo` = @photo WHERE `num_mat` = @numMat";
                _command.Parameters.Add(new MySqlParameter("photo", image));
                _command.Parameters.Add(new MySqlParameter("numMat", numeroMatricule));
                _command.CommandText = requete;
                _command.ExecuteNonQuery();
                //MonMessageBox.ShowBox("Image du personnel est inserée avec succés dans la base de données", "Enregistrement personnel");
                flag = true;
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("L'insertion de l'image du personnel a échoué", "Enregistrement personnel", exception);
                flag = false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
            return flag;
        }

        //verifier le statut du contrat de l'employe
        public static bool StatusContrat(string contrat)
        {
            if (contrat == "En cours")
                return false;
            else
                return true;
        }
        public static bool ModifierUnPersonnel(Personnel personnel, Service service, Salaire salaire, string ancienMatricule)
        {
            var flag = false;
            try
            {
                _connection.Open();

                _transaction = _connection.BeginTransaction();
                var requete = "UPDATE `pers_tbl` SET  `nom` = @nom, `prenom` = @prenom, `date_nai` = @date_nai, `lieu_nai` = @lieu_nai," +
                    " `addres` =@addres, `tele1` = @tele1, `tele2` = @tele2, `email` =@email, `sexe`=@sexe,no_piece=@no_piece,type_piece=@type_piece, " +
                    "`age` = @age, `num_mat`  = @num_mat01,situation_mat=@situation_mat,nbrEnfant=@nbrEnfant  WHERE `num_mat`  = @num_mat";
                _command.Parameters.Add(new MySqlParameter("num_mat01", personnel.NumeroMatricule));
                _command.Parameters.Add(new MySqlParameter("num_mat", ancienMatricule));
                _command.Parameters.Add(new MySqlParameter("nom", personnel.Nom));
                _command.Parameters.Add(new MySqlParameter("prenom", personnel.Prenom));
                _command.Parameters.Add(new MySqlParameter("date_nai", personnel.DateNaissance));
                _command.Parameters.Add(new MySqlParameter("addres", personnel.Adresse));
                _command.Parameters.Add(new MySqlParameter("tele1", personnel.Telephone1));
                _command.Parameters.Add(new MySqlParameter("tele2", personnel.Telephone2));
                _command.Parameters.Add(new MySqlParameter("sexe", personnel.Sexe));
                _command.Parameters.Add(new MySqlParameter("email", personnel.Email));
                _command.Parameters.Add(new MySqlParameter("lieu_nai", personnel.LieuNaissance));
                _command.Parameters.Add(new MySqlParameter("age", personnel.Age));
                _command.Parameters.Add(new MySqlParameter("situation_mat", personnel.SituationMatrimoniale));
                _command.Parameters.Add(new MySqlParameter("nbrEnfant", personnel.NombreEnfant));
                _command.Parameters.Add(new MySqlParameter("no_piece", personnel.NumeroPiece));
                _command.Parameters.Add(new MySqlParameter("type_piece", personnel.TypePiece));
                _command.CommandText = requete;
                _command.Transaction = _transaction;
                _command.Connection = _connection;
                _command.ExecuteNonQuery();

                requete =
                    "UPDATE `service_tbl` SET `date_pris` =@date_pris, `poste`=@poste,  `echelon` = @echelon,`categorie`=@categorie,`anciennete`=@anciennete, id_dep=@id_dep,localisation=@localisation" +
                    ",`no_cnps` = @no_cnps,`diplome` = @diplome,`contrat` = @contrat,`status` = @status,  date_depart=@date_depart, grade=@grade,id_div=@id_div,cnrt=@cnrt WHERE num_mat = @num_mat1";
                _command.Parameters.Add(new MySqlParameter("date_pris", service.DateService));
                _command.Parameters.Add(new MySqlParameter("poste", service.Poste));
                _command.Parameters.Add(new MySqlParameter("num_mat1", service.NumeroMatricule));
                _command.Parameters.Add(new MySqlParameter("echelon", service.Echelon));
                _command.Parameters.Add(new MySqlParameter("categorie", service.Categorie));
                _command.Parameters.Add(new MySqlParameter("anciennete", service.Anciennete));
                _command.Parameters.Add(new MySqlParameter("contrat", service.TypeContrat));
                _command.Parameters.Add(new MySqlParameter("status", service.Status));
                _command.Parameters.Add(new MySqlParameter("no_cnps", service.NoCNPS));
                _command.Parameters.Add(new MySqlParameter("diplome", service.Diplome));
                _command.Parameters.Add(new MySqlParameter("date_depart", service.DateDepart));
                _command.Parameters.Add(new MySqlParameter("id_dep", service.IDDepartement));
                _command.Parameters.Add(new MySqlParameter("id_div", service.IDDivision));
                _command.Parameters.Add(new MySqlParameter("grade", service.Grade));
                _command.Parameters.Add(new MySqlParameter("cnrt", service.SiCNRT));
                _command.Parameters.Add(new MySqlParameter("localisation", service.Localite));
                _command.Connection = _connection;
                _command.Transaction = _transaction;
                _command.CommandText = requete;
                _command.ExecuteNonQuery();

                requete =
                    "UPDATE  `salaire_tbl` SET `salaire` =@salaire, `grille` = @grille, `indice` = @indice,prime_loge=@prime_loge,prime_grd=@prime_grd," +
                    " prime_respo=@prime_respo ,heure_supp=@heure_supp, ind_transp=@ind_transp WHERE num_mat = @num_mat2";
                _command.Parameters.Add(new MySqlParameter("salaire", salaire.SalaireBase));
                _command.Parameters.Add(new MySqlParameter("grille", salaire.GrilleSalarialle));
                _command.Parameters.Add(new MySqlParameter("num_mat2", personnel.NumeroMatricule));
                _command.Parameters.Add(new MySqlParameter("indice", salaire.Indice));
                _command.Parameters.Add(new MySqlParameter("prime_loge", salaire.Indemnites));
                _command.Parameters.Add(new MySqlParameter("prime_grd", salaire.AutresPrimes));
                _command.Parameters.Add(new MySqlParameter("prime_respo", salaire.PrimeMotivation));
                _command.Parameters.Add(new MySqlParameter("heure_supp", salaire.FraisCommunication));
                _command.Parameters.Add(new MySqlParameter("ind_transp", salaire.PrimeTransport));
                _command.Connection = _connection;
                _command.Transaction = _transaction;
                _command.CommandText = requete;
                _command.ExecuteNonQuery();


                _transaction.Commit();
                MonMessageBox.ShowBox("Les donnees du personnel ont été modifiées avec succés", "Modification personnel");
                flag = true;
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("La modification des données du personnel a échoué", "Modification personnel", exception);
                flag = false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
            return flag;
        }
        public static bool EnregistrerLesDonneesBancaires(Banque bank)
        {
            try
            {
                _connection.Open();
                var requete = "";
                if (bank.ID > 0)
                {
                    requete = "UPDATE `bank_data_tbl` SET `no_compte`=@no_compte,code_banc=@code_banc,num_mat=@num_mat," +
                       "code_gui=@code_gui,cle=@cle,bank=@bank WHERE id =" + bank.ID;
                }
                else
                {
                    requete = "INSERT INTO `bank_data_tbl` (`num_mat`, `no_compte`,code_banc,code_gui,cle,bank,etat) VALUES (@num_mat,@no_compte,@code_banc,@code_gui,@cle,@bank,@etat)";

                }
                _command.Parameters.Add(new MySqlParameter("no_compte", bank.Compte));
                _command.Parameters.Add(new MySqlParameter("code_banc", bank.CodeBanque));
                _command.Parameters.Add(new MySqlParameter("code_gui", bank.CodeGuichet));
                _command.Parameters.Add(new MySqlParameter("cle", bank.Cle));
                _command.Parameters.Add(new MySqlParameter("bank", bank.NomBanque));
                _command.Parameters.Add(new MySqlParameter("num_mat", bank.NumeroMatricule));
                _command.Parameters.Add(new MySqlParameter("id", bank.ID));
                _command.Parameters.Add(new MySqlParameter("num_mat", bank.NumeroMatricule));
                _command.Parameters.Add(new MySqlParameter("etat", true));
                _command.Connection = _connection;
                _command.CommandText = requete;
                _command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            { return false; }
            finally { _connection.Close(); _command.Parameters.Clear(); }
        }
        public static bool SupprimerLesDonneesBancaires(int id)
        {
            try
            {
                _connection.Open();
                var requete =
                     "DELETE FROM `bank_data_tbl` WHERE id = " + id;
                _command.Connection = _connection;
                _command.CommandText = requete;
                _command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            { return false; }
            finally { _connection.Close(); _command.Parameters.Clear(); }
        }
        public static bool ChoisirLeCompteParDefaut(int id,string matricule)
        {
            try
            {
                _connection.Open();
                var requete = "UPDATE `bank_data_tbl` SET `etat`=@etatt  AND num_mat=@num_mat";
                _command.Parameters.Add(new MySqlParameter("etatt", false));
                _command.Parameters.Add(new MySqlParameter("num_mat", matricule));
                _command.Connection = _connection;
                _command.CommandText = requete;
                _command.ExecuteNonQuery();

                requete = "UPDATE `bank_data_tbl` SET `etat`=@etat WHERE id =" + id;
                _command.Parameters.Add(new MySqlParameter("etat", true));
                _command.Connection = _connection;
                _command.CommandText = requete;
                _command.ExecuteNonQuery();
                return true;
            }
            catch (Exception e)
            { return false; }
            finally
            {
                _command.Parameters.Clear();
                _connection.Close();
            }
        }

        //Mettre a jour l'anciennete
        public static bool MettreFinAuContrat()
        {
            try
            {
                _connection.Open();
                var requete =
                     "UPDATE `service_tbl` SET  status=@etat_cntr WHERE date_depart<=@date_fnctr";
                _command.Parameters.Add(new MySqlParameter("etat_cntr", "Fin contrat"));
                _command.Parameters.Add(new MySqlParameter("date_fnctr", DateTime.Now.Date));
                _command.CommandText = requete;
                _command.ExecuteNonQuery();

          requete=      "UPDATE `service_tbl` SET  status=@etat_cntr WHERE date_depart>@date_fnctr";
                _command.Parameters.Add(new MySqlParameter("etat_cntr", "En cours"));
                _command.Parameters.Add(new MySqlParameter("date_fnctr", DateTime.Now.Date));
                _command.CommandText = requete;
                _command.ExecuteNonQuery();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }

        public static List<Service> ListeAlerteFinContrat()
        {
            try
            {
                _connection.Open();
                var requete = "SELECT * FROM `service_tbl` WHERE  date_depart<=@date  AND status='EN COURS'";
                _command.Parameters.Add(new MySqlParameter("date", DateTime.Now.Date.AddYears(1)));
                _command.CommandText = requete;
                var reader = _command.ExecuteReader();
                var liste = new List<Service>();
                while (reader.Read())
                {
                    var s = new Service();
                    s.NumeroMatricule = reader.GetString(3);
          
                    liste.Add(s);
                }
                return liste;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }
      
        public static void MettreAjourAvancement()
        {
            try
            {
                _connection.Open();
                var requete = "SELECT * FROM `service_tbl` WHERE date_depart >@date AND status = 'EN COURS'";
                _command.Parameters.Add(new MySqlParameter("date", DateTime.Now.Date));
                _command.CommandText = requete;
                var reader = _command.ExecuteReader();
                var dt = new DataTable();
                dt.Load(reader);
                reader.Close();
                foreach (DataRow dtRow in dt.Rows)
                {
                    requete = "UPDATE `service_tbl` SET echelon =@echelon WHERE num_mat=@num_mat";
                    int intervalle = DateTime.Now.Subtract(DateTime.Parse(dtRow.ItemArray[1].ToString())).Days / 365;
                    var echelon = "";
                    if (intervalle % 2 == 0)
                    {
                        echelon = (intervalle / 2).ToString();
                    }
                    else
                    {
                        echelon = ((intervalle - 1) / 2).ToString();
                    }
                    _command.Parameters.Add(new MySqlParameter("num_mat", dtRow.ItemArray[3].ToString()));
                    _command.Parameters.Add(new MySqlParameter("echelon", echelon));
                    _command.CommandText = requete;
                    _command.ExecuteNonQuery();
                    _command.Parameters.Clear();
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }

        public static List<Service> AlerteAvancement()
        {
            try
            {
                var liste = new List<Service>();
                _connection.Open();
                var requete = "SELECT * FROM `service_tbl` WHERE  status = 'EN COURS'";
                //_command.Parameters.Add(new MySqlParameter("date", DateTime.Now.Date.AddDays(60)));
                _command.CommandText = requete;
                var reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var service = new Service();
                    service.DateService = reader.GetDateTime(1);
                    service.NumeroMatricule = reader.GetString(3);
                    service.Poste = reader.GetString(2);
                    service.Echelon = reader.GetString(4);
                    int intervalle = DateTime.Now.Date.AddDays(30).Subtract(service.DateService).Days / 365;
                    service.IDDirection = intervalle;
                    if (intervalle % 2 == 0)
                    {
                        if(intervalle>0)
                        {
                            var date = service.DateService.AddYears(intervalle);
                            if (date>= DateTime.Now.Date)
                        liste.Add(service);
                        }
                    }
                }
                return liste;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }

        public static List<Service> ListeServicePersonnel()
        {
            try
            {
                _connection.Open();
                var requete = "SELECT * FROM `service_tbl`";
                _command.CommandText = requete;
                var reader = _command.ExecuteReader();
                var liste = new List<Service>();
                while (reader.Read())
                {
                    var s = new Service();
                    s.NumeroService = reader.GetInt32(0);
                    s.DateService = reader.GetDateTime(1);
                    s.Poste = reader.GetString(2);
                    s.NumeroMatricule = reader.GetString(3);
                    s.Echelon = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    s.Categorie = !reader.IsDBNull(5) ? reader.GetString(5) : "";
                    s.Anciennete = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    s.NoCNPS = !reader.IsDBNull(7) ? reader.GetString(7) : "";
                    s.Diplome = !reader.IsDBNull(8) ? reader.GetString(8) : "";
                    s.TypeContrat = !reader.IsDBNull(9) ? reader.GetString(9) : "";
                    s.Status = !reader.IsDBNull(10) ? reader.GetString(10) : "";
                    s.DateDepart = reader.GetDateTime(11);
                    s.Grade = !reader.IsDBNull(12) ? reader.GetString(12) : "";
                    s.IDDepartement = !reader.IsDBNull(13) ? reader.GetInt32(13) : 0;
                    s.IDDivision = !reader.IsDBNull(14) ? reader.GetInt32(14) : 0;
                    s.SiCNRT = !reader.IsDBNull(15) ? reader.GetBoolean(15) : false;
                    s.Localite = !reader.IsDBNull(16) ? reader.GetString(16) : "";
                    liste.Add(s);
                }
                return liste;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }
     
        public static List<Service> ListeServicePersonnel(string numMatricule)
        {
            try
            {
                _connection.Open();
                var requete = "SELECT * FROM `service_tbl` WHERE num_mat=@numMat";
                _command.CommandText = requete;
                _command.Parameters.Add(new MySqlParameter("numMat", numMatricule));
                var reader = _command.ExecuteReader();
                var liste = new List<Service>();
                while (reader.Read())
                {
                    var s = new Service();
                    s.NumeroService = reader.GetInt32(0);
                    s.DateService = reader.GetDateTime(1);
                    s.Poste = reader.GetString(2);
                    s.NumeroMatricule = reader.GetString(3);
                    s.Echelon = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    s.Categorie = !reader.IsDBNull(5) ? reader.GetString(5) : "";
                    s.Anciennete = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    s.NoCNPS = !reader.IsDBNull(7) ? reader.GetString(7) : "";
                    s.Diplome = !reader.IsDBNull(8) ? reader.GetString(8) : "";
                    s.TypeContrat = !reader.IsDBNull(9) ? reader.GetString(9) : "";
                    s.Status = !reader.IsDBNull(10) ? reader.GetString(10) : "";
                    s.DateDepart = reader.GetDateTime(11);
                    s.Grade = !reader.IsDBNull(12) ? reader.GetString(12) : "";
                    s.IDDepartement = !reader.IsDBNull(13) ? reader.GetInt32(13) : 0;
                    s.IDDivision = !reader.IsDBNull(14) ? reader.GetInt32(14) : 0;
                    s.SiCNRT = !reader.IsDBNull(15) ? reader.GetBoolean(15) : false;
                    s.Localite = !reader.IsDBNull(16) ? reader.GetString(16) : "";
                    liste.Add(s);
                }
                return liste;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }

        public static void MettreAJourAnciennete()
        {
            try
            {
                var requete = "SELECT num_mat ,anciennete,date_pris ,contrat FROM service_tbl" +
                    " WHERE (contrat LIKE '%CONTRACTUEL%' OR contrat LIKE '%FONCTIONNAIRE%' ) AND status='EN COURS' AND date_depart >= @date";
                _command.CommandText = requete;
                _command.Parameters.Add(new MySqlParameter("date", DateTime.Now.Date));

                _connection.Open();
                var reader = _command.ExecuteReader();
                List<Service> liste = new List<Service>();
                while (reader.Read())
                {
                    var service = new Service();
                    service.NumeroMatricule = reader.GetString(0);
                    service.DateService = reader.GetDateTime(2);
                    //service.Status = reader.GetBoolean(3);
                    service.Anciennete = !reader.IsDBNull(1) ? reader.GetString(1) : "";
                    service.TypeContrat = !reader.IsDBNull(3) ? reader.GetString(3) : "";
                    liste.Add(service);
                }
                reader.Close();
                foreach (var service in liste)
                {
                    int anc;
                    if (DateTime.Now.DayOfYear >= service.DateService.DayOfYear)
                        anc = DateTime.Now.Date.Year - service.DateService.Year;
                    else
                        anc = DateTime.Now.Date.Year - service.DateService.Year - 1;

                    service.Anciennete = anc.ToString();
                    requete = "UPDATE `service_tbl` SET anciennete=@anciennete WHERE num_mat = @num_mat1";
                    _command.Parameters.Add(new MySqlParameter("num_mat1", service.NumeroMatricule));
                    _command.Parameters.Add(new MySqlParameter("anciennete", service.Anciennete));
                    _command.CommandText = requete;
                    _command.ExecuteNonQuery();
                    _command.Parameters.Clear();
                    //MonMessageBox.ShowBox("Données mises à jour avec succés", "Affirmation");
                }
            }
            catch (Exception ex)
            { MonMessageBox.ShowBox("", ex); }
            finally { _connection.Close(); }
        }
        public static void MettreAJourAge()
        {
            try
            {
                _connection.Open();
                _transaction = _connection.BeginTransaction();
                var requete = "SELECT * FROM pers_tbl";
                _command.CommandText = requete;
                _command.Transaction = _transaction;
                var reader = _command.ExecuteReader();
                var dt = new DataTable();
                dt.Load(reader);
                reader.Close();

                foreach (DataRow dtRow in dt.Rows)
                {
                    var age = 0;//DateTime.Now.Date.Subtract(DateTime.Parse(dtRow.ItemArray[3].ToString()));
                    var annee = DateTime.Parse(dtRow.ItemArray[3].ToString()).Year;
                    var anneeActuel = DateTime.Now.Year;
                    var moisActuel = DateTime.Now.Month;
                    var mois = DateTime.Parse(dtRow.ItemArray[3].ToString()).Month;
                    var sexe = dtRow.ItemArray[9].ToString();
                    if (moisActuel >= mois)
                    {
                        age = anneeActuel - annee;
                    }
                    else
                    {
                        age = anneeActuel - annee - 1;
                    }
                    requete = "UPDATE `pers_tbl` SET age=@age WHERE num_mat=@num_mat";
                    _command.Parameters.Add(new MySqlParameter("age", age));
                    _command.Parameters.Add(new MySqlParameter("num_mat", dtRow.ItemArray[0].ToString()));
                    _command.CommandText = requete;
                    _command.Transaction = _transaction;
                    _command.ExecuteNonQuery();
                    var flag = false;

                    //if(age>=60 && age <65 )
                    //{
                    //    requete =
                    //"UPDATE `service_tbl` SET  status=@status WHERE num_mat=@num_mat0 AND (categorie ='A' ) ";
                    //}else if( age>=65) 
                    //{
                    //    requete =
                    //"UPDATE `service_tbl` SET  status=@status WHERE  num_mat=@num_mat0 AND (categorie ='C' OR categorie='B')";
                    //}
                    //var status= "Retraité";
                    //if(sexe=="F")
                    //    status ="Retraitée";
                    //_command.Parameters.Add(new MySqlParameter("status", status));
                    ////_command.Parameters.Add(new MySqlParameter("dateret", DateTime.Parse(dtRow.ItemArray[3].ToString()).AddYears(60)));
                    //_command.Parameters.Add(new MySqlParameter("num_mat0", dtRow.ItemArray[0].ToString()));
                    //_command.CommandText = requete;
                    //_command.Transaction = _transaction;
                    //_command.ExecuteNonQuery();

                    //else
                    //{
                    //    requete =
                    //    "UPDATE `service_tbl` SET  etat=@etat, date_retr=@date WHERE num_mat=@num_mat1";
                    //    _command.Parameters.Add(new MySqlParameter("etat", false));
                    //    _command.Parameters.Add(new MySqlParameter("date", DateTime.Parse(dtRow.ItemArray[3].ToString()).AddYears(60)));
                    //    _command.Parameters.Add(new MySqlParameter("num_mat1", dtRow.ItemArray[0].ToString()));
                    //    _command.CommandText = requete;
                    //    _command.Transaction = _transaction;
                    //    _command.ExecuteNonQuery();
                    //}
                    _command.Parameters.Clear();
                }
                _transaction.Commit();
            }
            catch (Exception ex)
            {
                if (_transaction != null)
                    _transaction.Rollback();
                MonMessageBox.ShowBox("", ex);
            }
            finally { _connection.Close(); }
        }

        //supprimer LES PERSONNELS       
        public static void SupprimerUnPersonnel(string numeroPersonnel)
        {
            try
            {
                string requete = "DELETE FROM pers_tbl  WHERE (num_mat =@num_mat)";
                _command.CommandText = requete;
                _command.Parameters.Add(new MySqlParameter("num_mat", numeroPersonnel));
                _connection.Open();
                _command.ExecuteNonQuery();
                MonMessageBox.ShowBox("La suppression des données du personnel est faite avec succés", "Suppression personnel");

            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("La suppression des données du personnel a échoué", "Suppression personnel", exception);
            }
            finally
            {
                _command.Parameters.Clear();
                _connection.Close();
            }
        }

        //listes des personnels trie par departement
        public static List<Personnel> ListePersonnel(string nom)
        {
            var liste = new List<Personnel>();
            try
            {
                var requete = "SELECT * FROM pers_tbl WHERE nom LIKE '%" + nom + "%'";
                _command.CommandText = requete;
                _connection.Open();
                MySqlDataReader reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var p = new Personnel();
                    p.NumeroMatricule = reader.GetString(0);
                    p.Nom = reader.GetString(1);
                    p.Prenom = reader.GetString(2);
                    p.DateNaissance = !reader.IsDBNull(3) ? reader.GetDateTime(3) : DateTime.Now;
                    p.LieuNaissance = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    p.Adresse = !reader.IsDBNull(5) ? reader.GetString(5) : "";
                    p.Telephone1 = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    p.Telephone2 = !reader.IsDBNull(7) ? reader.GetString(7) : "";
                    p.Email = !reader.IsDBNull(8) ? reader.GetString(8) : "";
                    p.Sexe = !reader.IsDBNull(9) ? reader.GetString(9) : "";
                    p.Photo = !reader.IsDBNull(10) ? reader.GetString(10) : "";
                    p.Age = !reader.IsDBNull(11) ? reader.GetInt32(11) : 0;
                    p.SituationMatrimoniale = !reader.IsDBNull(12) ? reader.GetString(12) : "";
                    p.NombreEnfant = !reader.IsDBNull(13) ? reader.GetInt32(13) : 0;
                    p.NumeroPiece = !reader.IsDBNull(14) ? reader.GetString(14) : "";
                    p.TypePiece = !reader.IsDBNull(15) ? reader.GetString(15) : "";
                    liste.Add(p);
                }
                return liste;
            }
            catch { return null; }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }
        public static List<Personnel> ListePersonnel(string nom, string direction, string contrat)
        {
            var liste = new List<Personnel>();
            try
            {
                var requete = "SELECT pers_tbl.*,service_tbl.status FROM pers_tbl INNER JOIN service_tbl ON pers_tbl.num_mat =service_tbl.num_mat" +
                    " INNER JOIN division_tbl  ON service_tbl.id_div=division_tbl.id_div INNER JOIN direction_tbl ON division_tbl.id_direction=" +
                    "direction_tbl.id_direction  WHERE pers_tbl.nom LIKE '%" + nom + "%' AND direction_tbl.direction=@direction AND service_tbl.contrat LIKE @contrat";
                _command.CommandText = requete;
                _command.Parameters.Add(new MySqlParameter("direction", direction));
                _command.Parameters.Add(new MySqlParameter("contrat", "%" + contrat + "%"));
                _connection.Open();
                MySqlDataReader reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var p = new Personnel();
                    p.NumeroMatricule = reader.GetString(0);
                    p.SituationMatrimoniale = reader.GetString(16);
        
                    liste.Add(p);
                }
                return liste;
            }
            catch
            { return null; }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }
        public static List<Personnel> ListePersonnelParStatus(string nom, string status,string direction)
        {
            var liste = new List<Personnel>();
            try
            {
                var requete = "SELECT pers_tbl.* FROM pers_tbl INNER JOIN service_tbl ON pers_tbl.num_mat =service_tbl.num_mat" +
                    " INNER JOIN division_tbl  ON service_tbl.id_div=division_tbl.id_div INNER JOIN direction_tbl ON division_tbl.id_direction=" +
                    "direction_tbl.id_direction  WHERE pers_tbl.nom LIKE '%" + nom + "%' AND  service_tbl.status = @status AND direction_tbl.direction=@direction";
                _command.CommandText = requete;
                _command.Parameters.Add(new MySqlParameter("direction", direction));
                _command.Parameters.Add(new MySqlParameter("status", status));
                _connection.Open();
                MySqlDataReader reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var p = new Personnel();
                    liste.Add(p);
                }
                return liste;
            }
            catch
            { return null; }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }
        public static List<Personnel> ListePersonnelParGrade(string nom, int  idDirection, string grade)
        {
            var liste = new List<Personnel>();
            try
            {
                var requete = "SELECT pers_tbl.*,service_tbl.status FROM pers_tbl INNER JOIN service_tbl ON pers_tbl.num_mat =service_tbl.num_mat" +
                    " INNER JOIN division_tbl  ON service_tbl.id_div=division_tbl.id_div INNER JOIN direction_tbl ON division_tbl.id_direction=" +
                    "direction_tbl.id_direction  WHERE pers_tbl.nom LIKE '%" + nom + "%'  AND service_tbl.grade=@grade AND direction_tbl.id_direction= "+idDirection;
                _command.CommandText = requete;
                _command.Parameters.Add(new MySqlParameter("grade", grade));
                _connection.Open();
                MySqlDataReader reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var p = new Personnel();
                    p.SituationMatrimoniale = reader.GetString(16);
                    liste.Add(p);
                }
                return liste;
            }
            catch
            { return null; }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }
        public static List<Personnel> ListePersonnelParCategorie(string nom, int idDirection, string categorie)
        {
            var liste = new List<Personnel>();
            try
            {
                var requete = "SELECT pers_tbl.*,service_tbl.status FROM pers_tbl INNER JOIN service_tbl ON pers_tbl.num_mat =service_tbl.num_mat" +
                    " INNER JOIN division_tbl  ON service_tbl.id_div=division_tbl.id_div INNER JOIN direction_tbl ON division_tbl.id_direction=" +
                    "direction_tbl.id_direction  WHERE pers_tbl.nom LIKE '%" + nom + "%' AND service_tbl.categorie=@cat AND direction_tbl.id_direction =" + idDirection; ;
                _command.CommandText = requete;
                _command.Parameters.Add(new MySqlParameter("cat", categorie));
                _connection.Open();
                MySqlDataReader reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var p = new Personnel();
                    p.SituationMatrimoniale = reader.GetString(16);
                    liste.Add(p);
                }
                return liste;
            }
            catch
            { return null; }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }
        public static List<Personnel> ListePersonnel(string nom, string direction)
        {
            var liste = new List<Personnel>();
            try
            {
                var requete = "SELECT pers_tbl.*,service_tbl.status FROM pers_tbl INNER JOIN service_tbl ON pers_tbl.num_mat =service_tbl.num_mat" +
                    " INNER JOIN division_tbl  ON service_tbl.id_div=division_tbl.id_div INNER JOIN direction_tbl ON division_tbl.id_direction=" +
                    "direction_tbl.id_direction  WHERE pers_tbl.nom LIKE '%" + nom + "%' AND direction_tbl.direction=@direction ";
                _command.CommandText = requete;
                _command.Parameters.Add(new MySqlParameter("direction", direction));
                _connection.Open();
                MySqlDataReader reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    
                    var p = new Personnel();
                    p.SituationMatrimoniale = reader.GetString(16);
                    liste.Add(p);
                }
                return liste;
            }
            catch(Exception ex)
            { MonMessageBox.ShowBox("", ex);
                return null; }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }
        public static List<Personnel> ListePersonnelParMat(string mat, string direction)
        {
            var liste = new List<Personnel>();
            try
            {
                var requete = "SELECT pers_tbl.* ,service_tbl.status FROM pers_tbl INNER JOIN service_tbl ON pers_tbl.num_mat =service_tbl.num_mat" +
                    " INNER JOIN division_tbl  ON service_tbl.id_div=division_tbl.id_div INNER JOIN direction_tbl ON division_tbl.id_direction=" +
                    "direction_tbl.id_direction  WHERE pers_tbl.num_mat LIKE '%" + mat + "%' AND direction_tbl.direction=@direction";
                _command.CommandText = requete;
                _command.Parameters.Add(new MySqlParameter("direction", direction));
                _connection.Open();
                MySqlDataReader reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var p = new Personnel();
                    p.SituationMatrimoniale = reader.GetString(16);
                    liste.Add(p);
                }
                return liste;
            }
            catch
            { return null; }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }   
        public static List<Personnel> ListePersonnelParMatricule(string matricule)
        {
            var liste = new List<Personnel>();
            try
            {
                var requete = "SELECT * FROM pers_tbl WHERE num_mat =@num_mat";
                _command.CommandText = requete;
                _command.Parameters.Add(new MySqlParameter("num_mat", matricule));
                _connection.Open();
                MySqlDataReader reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var p = new Personnel();
                    p.NumeroMatricule = reader.GetString(0);
                    p.Nom = reader.GetString(1);
                    p.Prenom = reader.GetString(2);
                    p.DateNaissance = !reader.IsDBNull(3) ? reader.GetDateTime(3) : DateTime.Now;
                    p.LieuNaissance = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    p.Adresse = !reader.IsDBNull(5) ? reader.GetString(5) : "";
                    p.Telephone1 = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    p.Telephone2 = !reader.IsDBNull(7) ? reader.GetString(7) : "";
                    p.Email = !reader.IsDBNull(8) ? reader.GetString(8) : "";
                    p.Sexe = !reader.IsDBNull(9) ? reader.GetString(9) : "";
                    p.Photo = !reader.IsDBNull(10) ? reader.GetString(10) : "";
                    p.Age = !reader.IsDBNull(11) ? reader.GetInt32(11) : 0;
                    p.SituationMatrimoniale = !reader.IsDBNull(12) ? reader.GetString(12) : "";
                    p.NombreEnfant = !reader.IsDBNull(13) ? reader.GetInt32(13) : 0;
                    p.NumeroPiece = !reader.IsDBNull(14) ? reader.GetString(14) : "";
                    p.TypePiece = !reader.IsDBNull(15) ? reader.GetString(15) : "";
                    liste.Add(p);
                }
                return liste;
            }
            catch { return null; }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }
        public static List<Salaire> ListeSalairePersonnel(string numeroMatricule)
        {
            var liste = new List<Salaire>();
            try
            {
                var requete = "SELECT * FROM salaire_tbl WHERE num_mat =@numeroMatricule";
                _command.CommandText = requete;
                _command.Parameters.Add(new MySqlParameter("numeroMatricule", numeroMatricule));
                _connection.Open();
                MySqlDataReader reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var s = new Salaire();
                    s.IDSalaire = reader.GetInt32(0);
                    s.SalaireBase = reader.GetDouble(1);
                    s.GrilleSalarialle = reader.GetDouble(2);
                    s.NumeroMatricule = !reader.IsDBNull(3) ? reader.GetString(3) : "";
                    s.Indemnites = reader.GetDouble(4);
                    s.AutresPrimes = reader.GetDouble(5);
                    s.PrimeMotivation = reader.GetDouble(6);
                    s.FraisCommunication = reader.GetDouble(7);
                    s.PrimeTransport = reader.GetDouble(8);
                    liste.Add(s);
                }
                return liste;
            }
            catch (Exception ex)
            { return null; }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }

        public static DataTable ListeDesPersonnelsParDepartement(string departement)
        {
            var dataTable = new DataTable();
            try
            {
                var requete = "SELECT pers_tbl.*, service_tbl.*,dep_tbl.* FROM pers_tbl INNER JOIN dep_tbl ON pers_tbl.id_dep = dep_tbl.id_dep INNER JOIN service_tbl " +
                    "ON pers_tbl.num_mat = service_tbl.num_mat WHERE dep_tbl.dep = @dep";
                _command.CommandText = requete;
                _command.Parameters.Add(new MySqlParameter("dep", departement));
                _connection.Open();
                MySqlDataReader reader = _command.ExecuteReader();
                dataTable.Load(reader);
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
            return dataTable;
        }

        //listes des personnels trie par departement
        public static DataTable ListeDesPersonnelsParDirection(string direction)
        {
            var dataTable = new DataTable();
            try
            {
                var requete = "SELECT pers_tbl.num_mat, pers_tbl.nom, pers_tbl.prenom, pers_tbl.date_nai, pers_tbl.lieu_nai, " +
                    "pers_tbl.addres, pers_tbl.tele1, pers_tbl.tele2, pers_tbl.email, pers_tbl.sexe, pers_tbl.photo, dep_tbl.dep," +
                    "service_tbl.poste ,service_tbl.echelon,service_tbl.categorie,pers_tbl.no_compte FROM pers_tbl INNER JOIN dep_tbl ON pers_tbl.id_dep = dep_tbl.id_dep INNER JOIN service_tbl " +
                    "ON pers_tbl.num_mat = service_tbl.num_mat INNER JOIN direction_tbl ON dep_tbl.id_direction = " +
                    "direction_tbl.id_direction  WHERE direction_tbl.direction = @direction";
                _command.CommandText = requete;
                _command.Parameters.Add(new MySqlParameter("direction", direction));
                _connection.Open();
                var reader = _command.ExecuteReader();
                dataTable.Load(reader);
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
            return dataTable;
        }

        //liste des personnels par nom fonction
        public static DataTable ListeDesPersonnelParFonction(string nomFonction)
        {
            DataTable dataTable = new DataTable();
            try
            {
                string requete = "SELECT pers_tbl.num_mat, pers_tbl.nom, pers_tbl.prenom, pers_tbl.date_nai, pers_tbl.lieu_nai, " +
                    "pers_tbl.addres, pers_tbl.tele1, pers_tbl.tele2, pers_tbl.email, pers_tbl.sexe, pers_tbl.photo, " +
                    "service_tbl.poste,service_tbl.echelon,service_tbl.categorie,service_tbl.no_cnps,service_tbl.contrat," +
                " service_tbl.date_pris ,service_tbl.anciennete  FROM pers_tbl INNER JOIN" +
                "  service_tbl ON pers_tbl.num_mat = service_tbl.num_mat WHERE service_tbl.poste LIKE '%" + nomFonction + "%'";
                _command.CommandText = requete;
                //_command.Parameters.AddWithValue("fonction", nomFonction);
                _connection.Open();
                MySqlDataReader reader = _command.ExecuteReader();
                dataTable.Load(reader);
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }

            return dataTable;
        }

        //liste des departements
        public static List<Service> ListeDepartement()
        {
            var liste = new List<Service>();
            try
            {
                string requete = "SELECT * FROM dep_tbl  ORDER BY ordre";
                _command.CommandText = requete;
                _connection.Open();
                MySqlDataReader reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var d = new Service();
                    d.IDDepartement = reader.GetInt32(0);
                    d.Departement = reader.GetString(1);
                    d.Abreviation = !reader.IsDBNull(2) ? reader.GetString(2) : "";
                    d.IDDivision = !reader.IsDBNull(3) ? reader.GetInt32(3) : 0;
                    d.Ordre = !reader.IsDBNull(4) ? reader.GetInt32(4) : 0;
                    liste.Add(d);
                }
                return liste;
            }
            catch { return null; }
            finally
            {
                _connection.Close();
            }

        }
        public static List<Service> ListeDepartement(int iddep)
        {
            var liste = new List<Service>();
            try
            {
                string requete = "SELECT * FROM dep_tbl WHERE id_dep =" + iddep;
                _command.CommandText = requete;
                _connection.Open();
                MySqlDataReader reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var d = new Service();
                    d.IDDepartement = reader.GetInt32(0);
                    d.Departement = reader.GetString(1);
                    d.Abreviation = !reader.IsDBNull(2) ? reader.GetString(2) : "";
                    d.IDDivision = !reader.IsDBNull(3) ? reader.GetInt32(3) : 0;
                    liste.Add(d);
                }
                return liste;
            }
            catch { return null; }
            finally
            {
                _connection.Close();
            }

        }

        public static void SupprimerUnDepartement(int id)
        {
            try
            {
                string requete = "DELETE FROM dep_tbl  WHERE id_dep = " + id;
                _command.CommandText = requete;
                _connection.Open();
                _command.ExecuteNonQuery();
                MonMessageBox.ShowBox("La suppression des données de la division est faite avec succés", "Suppression personnel");
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("La suppression des données a échoué", "Suppression personnel", exception);
            }
            finally
            {
                _connection.Close();
            }
        }

        //ajouter liste de departement
        public static bool EnregistrerDepartement(Service service)
        {
            var flag = false;
            try
            {
                _connection.Open();
                var requete = "UPDATE  dep_tbl SET  dep = @dep, abv=@abv,id_div=@id_div WHERE id_dep = " + service.IDDepartement;
                if (service.IDDepartement == 0)
                {
                    requete = "INSERT INTO dep_tbl ( dep,abv,id_div) VALUES (@dep,@abv,@id_div)";
                }
                _command.Parameters.Add(new MySqlParameter("dep", service.Departement));
                _command.Parameters.Add(new MySqlParameter("abv", service.Abreviation));
                _command.Parameters.Add(new MySqlParameter("id_div", service.IDDivision));
                _command.CommandText = requete;
                _command.ExecuteNonQuery();

                flag = true;
                MonMessageBox.ShowBox("Données modifieés avec succés", "Information");
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("La modification des données a echoue", "Erreur", ex);
                flag = false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
            return flag;
        }

        public static List<Service> ListeDirection()
        {
            var liste = new List<Service>();
            try
            {
                string requete = "SELECT * FROM direction_tbl ORDER BY ordre";
                _command.CommandText = requete;
                _connection.Open();
                MySqlDataReader reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var d = new Service();
                    d.IDDirection = reader.GetInt32(0);
                    d.Direction = reader.GetString(1);
                    d.Abreviation = !reader.IsDBNull(2) ? reader.GetString(2) : "";
                    d.Ordre = !reader.IsDBNull(3) ? reader.GetInt32(3) : 0;
                    liste.Add(d);
                }
                return liste;
            }
            catch { return null; }
            finally
            {
                _connection.Close();
            }

        }


        public static List<Service> ListeDirection(string direction)
        {
            var liste = new List<Service>();
            try
            {
                string requete = "SELECT * FROM direction_tbl WHERE direction=@direction ORDER BY ordre";
                _command.CommandText = requete;
                _command.Parameters.Add(new MySqlParameter("direction", direction));
                _connection.Open();
                MySqlDataReader reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var d = new Service();
                    d.IDDirection = reader.GetInt32(0);
                    d.Direction = reader.GetString(1);
                    d.Abreviation = !reader.IsDBNull(2) ? reader.GetString(2) : "";
                    d.Ordre = !reader.IsDBNull(3) ? reader.GetInt32(3) : 0;
                    liste.Add(d);
                }
                return liste;
            }
            catch { return null; }
            finally
            {
                _command.Parameters.Clear();
                _connection.Close();
            }

        }

        public static List<string> StatusDesPersonnels()
        {
            var liste = new List<string>();
            try
            {
                string requete = "SELECT * FROM etat_tbl order by etat";
                _command.CommandText = requete;
                _connection.Open();
                MySqlDataReader reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var s = reader.GetString(1);
                    liste.Add(s);
                }
                return liste;
            }
            catch { return null; }
            finally
            {
                _connection.Close();
            }

        }

        //ajouter diretion
        public static bool EnregistrerDirection(Service service)
        {
            var flag = false;
            try
            {
                _connection.Open();
                var requete = "UPDATE  direction_tbl SET  direction = @direction, abv=@abv WHERE id_direction = " + service.IDDirection;
                if (service.IDDirection == 0)
                {
                    requete = "INSERT INTO direction_tbl ( direction,abv) VALUES (@direction,@abv)";
                }
                _command.Parameters.Add(new MySqlParameter("direction", service.Direction));
                _command.Parameters.Add(new MySqlParameter("abv", service.Abreviation));
                _command.CommandText = requete;
                _command.ExecuteNonQuery();

                flag = true;
                MonMessageBox.ShowBox("Données modifieés avec succés", "Information");
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("La modification des données a echoue", "Erreur", ex);
                flag = false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
            return flag;
        }
        public static bool OrdonnerDirection(Service service)
        {
            var flag = false;
            try
            {
                _connection.Open();
                var requete = "UPDATE  direction_tbl SET  ordre =" + service.Ordre + "  WHERE id_direction = " + service.IDDirection;
                _command.CommandText = requete;
                _command.ExecuteNonQuery();
                flag = true;
            }
            catch (Exception ex)
            {
                //MonMessageBox.ShowBox("La modification des données a echoue", "Erreur", ex);
                flag = false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
            return flag;
        }
        public static bool OrdonnerService(Service service)
        {
            var flag = false;
            try
            {
                _connection.Open();
                var requete = "UPDATE  dep_tbl  SET  ordre =" + service.Ordre + "  WHERE id_dep = " + service.IDDepartement;
                _command.CommandText = requete;
                _command.ExecuteNonQuery();
                flag = true;
            }
            catch (Exception ex)
            {
                //MonMessageBox.ShowBox("La modification des données a echoue", "Erreur", ex);
                flag = false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
            return flag;
        }//supprimer LES departement       
        public static void SupprimerUneDirection(int id)
        {
            try
            {
                string requete = "DELETE FROM direction_tbl  WHERE id_direction = " + id;
                _command.CommandText = requete;
                _connection.Open();
                _command.ExecuteNonQuery();
                MonMessageBox.ShowBox("La suppression des données de la division est faite avec succés", "Suppression personnel");
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("La suppression des données a échoué", "Suppression personnel", exception);
            }
            finally
            {
                _connection.Close();
            }
        }

        public static List<Service> ListeDivision()
        {
            var liste = new List<Service>();
            try
            {
                string requete = "SELECT * FROM division_tbl ORDER BY ordre";
                _command.CommandText = requete;
                _connection.Open();
                MySqlDataReader reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var d = new Service();
                    d.IDDivision = reader.GetInt32(0);
                    d.Division = reader.GetString(1);
                    d.Abreviation = !reader.IsDBNull(2) ? reader.GetString(2) : "";
                    d.IDDirection = reader.GetInt32(3);
                    d.Ordre = !reader.IsDBNull(4) ? reader.GetInt32(4) : 0;
                    liste.Add(d);
                }
                return liste;
            }
            catch { return null; }
            finally
            {
                _connection.Close();
            }

        }

        //ajouter diretion
        public static bool EnregistrerDivision(Service service)
        {
            var flag = false;
            try
            {
                _connection.Open();
                var requete = "UPDATE  division_tbl SET  division = @division,id_direction=@id_direction, abv=@abv WHERE id_div = " + service.IDDivision;
                if (service.IDDivision == 0)
                {
                    requete = "INSERT INTO division_tbl ( division,abv, id_direction) VALUES (@division,@abv,@id_direction)";
                }
                _command.Parameters.Add(new MySqlParameter("division", service.Division));
                _command.Parameters.Add(new MySqlParameter("abv", service.Abreviation));
                _command.Parameters.Add(new MySqlParameter("id_direction", service.IDDirection));
                _command.CommandText = requete;
                _command.ExecuteNonQuery();

                flag = true;
                MonMessageBox.ShowBox("Données enregistreés avec succés", "Information");
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("La modification des données a echoue", "Erreur", ex);
                flag = false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
            return flag;
        }
        public static bool OrdonnerDivision(Service service)
        {
            var flag = false;
            try
            {
                _connection.Open();
                var requete = "UPDATE  division_tbl SET  ordre =" + service.Ordre + "  WHERE id_div = " + service.IDDivision;
                _command.CommandText = requete;
                _command.ExecuteNonQuery();
                flag = true;
            }
            catch (Exception ex)
            {
                //MonMessageBox.ShowBox("La modification des données a echoue", "Erreur", ex);
                flag = false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
            return flag;
        }

        //supprimer LES divisions       
        public static void SupprimerUneDivision(int id)
        {
            try
            {
                string requete = "DELETE FROM division_tbl  WHERE id_div = " + id;
                _command.CommandText = requete;
                _connection.Open();
                _command.ExecuteNonQuery();
                MonMessageBox.ShowBox("La suppression des données de la division est faite avec succés", "Suppression personnel");
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("La suppression des données a échoué", "Suppression personnel", exception);
            }
            finally
            {
                _connection.Close();
            }
        }

        //liste des departements
        public static DataTable ListeDepartement(string division)
        {
            var dataTable = new DataTable();
            try
            {
                var requete = "SELECT  dep_tbl.id_dep, dep_tbl.dep FROM dep_tbl WHERE dep_tbl.dep =@division ";
                _command.CommandText = requete;
                _command.Parameters.Add(new MySqlParameter("division", division));
                _connection.Open();
                var reader = _command.ExecuteReader();
                dataTable.Load(reader);
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
            return dataTable;
        }

        //liste de salaire
        public static List<Salaire> ListeSalaire(string numeroMatricule)
        {
            var liste = new List<Salaire>();
            try
            {
                string requete = "SELECT * FROM salaire_tbl WHERE num_mat=@Param";
                _command.CommandText = requete;
                _connection.Open();
                _command.Parameters.Add(new MySqlParameter("Param", numeroMatricule));
                MySqlDataReader reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var s = new Salaire();
                    s.IDSalaire = reader.GetInt32(0);
                    s.SalaireBase = reader.GetDouble(1);
                    s.NumeroMatricule = reader.GetString(3);
                    s.Indemnites = reader.GetDouble(5);
                    s.AutresPrimes = reader.GetDouble(6);
                    s.PrimeMotivation = reader.GetDouble(7);
                    s.FraisCommunication = reader.GetDouble(8);
                    s.PrimeTransport = reader.GetDouble(9);
                    liste.Add(s);
                }
                return liste;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liset Salaire", ex); return null;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }
        
        #endregion
        //liste de salaire
        public static DataTable ListeAccompte(string  num_mat)
        {
            DataTable dataTable = new DataTable();
            try
            {
                string requete = "SELECT * FROM accompte_tbl  WHERE num_mat =@num_mat ORDER BY id DESC, date_acp DESC";
                _command.CommandText = requete;
                _connection.Open();
                _command.Parameters.Add(new MySqlParameter("num_mat", num_mat));
                MySqlDataReader reader = _command.ExecuteReader();
                dataTable.Load(reader);
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liset Salaire", ex);
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
            return dataTable;
        }

        public static bool EnregistrerUneLocalisation(Service service)
        {
            var flag = false;
            try
            {
                _connection.Open();
                var requete = "UPDATE  localisation_tbl SET  localisation = @localisation WHERE id = " + service.IDDivision;
                if (service.IDDivision == 0)
                {
                    requete = "INSERT INTO localisation_tbl ( localisation) VALUES (@localisation)";
                }
                _command.Parameters.Add(new MySqlParameter("localisation", service.Localite));
                _command.CommandText = requete;
                _command.ExecuteNonQuery();
                flag = true;
                MonMessageBox.ShowBox("Données enregistreés avec succés", "Information");
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("La modification des données a echoue", "Erreur", ex);
                flag = false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
            return flag;
        }

        //supprimer LES localisations       
        public static void SupprimerUneLocalisation(int id)
        {
            try
            {
                string requete = "DELETE FROM localisation_tbl  WHERE id_div = " + id;
                _command.CommandText = requete;
                _connection.Open();
                _command.ExecuteNonQuery();
                MonMessageBox.ShowBox("La suppression des données est faite avec succés", "Suppression");
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("La suppression des données a échoué", "Suppression", exception);
            }
            finally
            {
                _connection.Close();
            }
        }

        //liste des localisations
        public static List<Service> ListeLocalisation()
        {
            var liste = new List<Service>();
            try
            {
                string requete = "SELECT * FROM localisation_tbl ";
                _command.CommandText = requete;
                _connection.Open();
                MySqlDataReader reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var s = new Service();
                    s.IDDivision = reader.GetInt32(0);
                    s.Localite = reader.GetString(1);                 
                    liste.Add(s);
                }
                return liste;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste localisation", ex); 
                return null;
            }
            finally
            {
                _connection.Close();
            }
        }
        
        #region ABSENCE_CONGE
        //ajouter une demande de conge        
        public static bool EnregistrerUnConge(Conge conge, string etat)
        {
            bool flag = false;
            try
            {
                _connection.Open();
                string requete;
                requete = "SELECT  * FROM conge_tbl WHERE num_mat =@num_mat01 AND annee=@annee01 AND nom=@nom01 AND nature_con=@nature_con01";
                _command.Parameters.Add(new MySqlParameter("num_mat01", conge.NumeroMatricule));
                _command.Parameters.Add(new MySqlParameter("nature_con01", conge.NatureConge));
                _command.Parameters.Add(new MySqlParameter("annee01", conge.Exercice));
                _command.Parameters.Add(new MySqlParameter("nom01", conge.NomPersonnel));
                _command.CommandText = requete;
                var reader = _command.ExecuteReader();
                var dt = new DataTable();
                dt.Load(reader);
                reader.Close();
                if (dt.Rows.Count>0 && etat =="1")
                {
                    MonMessageBox.ShowBox(conge.NomPersonnel + " est deja  planifié(e) pour le mois de " + dt.Rows[0].ItemArray[5].ToString()+" cette année", "Erreur");
                    flag= false;
                }
                else
                {
                    if (etat == "1")
                    {
                        requete = "INSERT INTO conge_tbl(`num_mat` ,`date_dep` ,`date_rep` , `nature_con`,`mois`,`annee`,nom,siProjet,duree,fonction,date_dem) " +
                        "VALUES(@num_mat ,@date_dep ,@date_rep ,@nature_con, @mois,@annee,@nom,@siProjet,@duree,@fonction,@date_dem)";
                    }
                    else
                    {
                        requete = "UPDATE conge_tbl SET  `date_dep` = @date_dep ,duree=@duree, fonction=@fonction, date_dem=@date_dem,`date_rep` = " +
                            "@date_rep ,`nature_con` = @nature_con,mois=@mois, annee=@annee ,nom=@nom,siProjet=@siProjet WHERE `id_conge` = " + conge.IDConge;
                    }
                    _command.Parameters.Add(new MySqlParameter("num_mat", conge.NumeroMatricule));
                    _command.Parameters.Add(new MySqlParameter("date_dep", conge.DateDebutConge));
                    _command.Parameters.Add(new MySqlParameter("date_rep", conge.DateRetour));
                    _command.Parameters.Add(new MySqlParameter("nature_con", conge.NatureConge));
                    _command.Parameters.Add(new MySqlParameter("mois", conge.Mois));
                    _command.Parameters.Add(new MySqlParameter("annee", conge.Exercice));
                    _command.Parameters.Add(new MySqlParameter("nom", conge.NomPersonnel));
                    _command.Parameters.Add(new MySqlParameter("siProjet", conge.SiProjet));
                    _command.Parameters.Add(new MySqlParameter("duree", conge.Duree));
                    _command.Parameters.Add(new MySqlParameter("fonction", conge.Fonction));
                    _command.Parameters.Add(new MySqlParameter("date_dem", conge.DateDemande));
                    _command.CommandText = requete;
                    _command.ExecuteNonQuery();
                    MonMessageBox.ShowBox("L'enregistrement est fait avec succés", "Information");
                    flag = true;
                }
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("L' enregistrement des données du congé a échoué", "Erreur", exception);
                flag = false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
            return flag;
        }

        public static void SupprimerUnConge(int idConge)
        {
            try
            {
                _connection.Open();
                string requete = "DELETE FROM conge_tbl  WHERE `id_conge` = " + idConge;
                _command.CommandText = requete;
                _command.ExecuteNonQuery();

                MonMessageBox.ShowBox("La suppression est faite avec succés", "Information");
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("La suppression des données du congé a échoué", "Erreur", exception);
            }
            finally
            {
                _connection.Close();
            }
        }

        //liste des conges
        public static List<Conge> ListeConge()
        {
            var liste = new List<Conge>();
            try
            {
                string requete = "SELECT * FROM conge_tbl  ";
                _command.CommandText = requete;
                _connection.Open();
                MySqlDataReader reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var conge = new Conge();
                    conge.IDConge = reader.GetInt32(0);
                    conge.NumeroMatricule = reader.GetString(1);
                    conge.DateDebutConge = reader.GetDateTime(2);
                    conge.DateRetour = reader.GetDateTime(3);
                    conge.Mois = reader.GetString(5);
                    conge.Exercice = reader.GetInt32(6);
                    conge.NatureConge = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    conge.NomPersonnel = !reader.IsDBNull(7) ? reader.GetString(7):"";
                    conge.SiProjet = !reader.IsDBNull(8) ? reader.GetBoolean(8) : false;
                    conge.Duree = !reader.IsDBNull(9) ? reader.GetInt32(9) : 0;
                    conge.Fonction = !reader.IsDBNull(10) ? reader.GetString(10) : "";
                    conge.DateDemande = !reader.IsDBNull(11) ? reader.GetDateTime(11) : DateTime.Now;
                    liste.Add(conge);
                }
            }
            catch { }
            finally
            {
                _connection.Close();
            }
            return liste;
        }

        //ajouter une absence
        public static bool EnregistrerUneAbsence(Absence abscence)
        {
            var flag = false;
            try
            {
                string requete;
                if (abscence.IDAbsence<=0)
                {
                    requete = "INSERT INTO `personnel_db`.`absence_tbl`(`num_emplo`,`nomEmploye`,`date_abs`,`date_retour`,`nbr_jr`,`motif`,`siProjet`,`destination`,`exercice`,`fonction`)" +
                                " VALUES (@num_emplo,@nomEmploye, @date_abs, @date_retour, @nbr_jr, @motif, @siProjet, @destination, @exercice,@fonction)";
                }
                else
                {
                    requete = "UPDATE absence_tbl SET  num_emplo=@num_emplo,nomEmploye=@nomEmploye,date_abs=@date_abs,date_retour=@date_retour,nbr_jr=@nbr_jr,motif=@motif,siProjet=@siProjet," +
                        "destination=@destination,exercice=@exercice,fonction=@fonction WHERE `id_absc` = " + abscence.IDAbsence;
                }
                _command.Parameters.Add(new MySqlParameter("num_emplo", abscence.NumeroEmploye));
                _command.Parameters.Add(new MySqlParameter("date_abs", abscence.DateDebutAbscense));
                _command.Parameters.Add(new MySqlParameter("date_retour", abscence.DateRetour));
                _command.Parameters.Add(new MySqlParameter("motif", abscence.Motif));
                _command.Parameters.Add(new MySqlParameter("siProjet", abscence.SiPersonnelProjet));
                _command.Parameters.Add(new MySqlParameter("destination", abscence.Destination));
                _command.Parameters.Add(new MySqlParameter("exercice", abscence.Exercice));
                _command.Parameters.Add(new MySqlParameter("nbr_jr", abscence.Duree));
                _command.Parameters.Add(new MySqlParameter("nomEmploye", abscence.NomPersonnel));
                _command.Parameters.Add(new MySqlParameter("fonction", abscence.Fonction));
                _command.CommandText = requete;
                _connection.Open();
                _command.ExecuteNonQuery();
                MonMessageBox.ShowBox("L'enregistrement est fait avec succés", "Information");
                flag = true;
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("L' enregistrement des données  a échoué", "Erreur", exception);
                flag = false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
            return flag;
        }

        //supprimer une demande de absences
        public static void SupprimerUneAbsence(int id)
        {
            try
            {
                _connection.Open();
                string requete = "DELETE FROM absence_tbl  WHERE `id_absc` = " + id;
                _command.CommandText = requete;
                _command.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("La suppression des données a échoué", "Erreur", exception);
            }
            finally
            {
                _connection.Close();
            }
        }

        //liste des absences
        public static List<Absence> ListeUneAbsence()
        {
            var dataTable = new List<Absence>();
            try
            {
                string requete = "SELECT * FROM absence_tbl ORDER BY id_absc ";
                _command.CommandText = requete;
                _connection.Open();
                MySqlDataReader reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var absence = new Absence();
                    absence.IDAbsence = reader.GetInt32(0);
                    absence.NumeroEmploye = !reader.IsDBNull(1) ? reader.GetString(1) : "";
                    absence.NomPersonnel = !reader.IsDBNull(2) ? reader.GetString(2) : "";
                    absence.DateDebutAbscense = reader.GetDateTime(3);
                    absence.DateRetour = reader.GetDateTime(4);
                    absence.Duree = reader.GetInt32(5);
                    absence.Motif = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    absence.Destination = reader.GetString(7) ;
                    absence.Exercice = reader.GetInt32(8);
                    absence.SiPersonnelProjet = !reader.IsDBNull(9) ? reader.GetBoolean(9) : false;
                    absence.Fonction = !reader.IsDBNull(10) ? reader.GetString(10) : "";
                    dataTable.Add(absence);
                }
            }
            catch { }
            finally
            {
                _connection.Close();
            }
            return dataTable;
        }

        #endregion
        public static string Converti(long chiffre)
        {
            long centaine, dizaine, unite, reste, y;
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

        //enregistrer une ordre de paiement
        #region PAIEMENT

        public static int CreerUnOrdreDePaiement(Paiement paiement, List<Paiement> liste, bool flag)
        {
            try
            {
                _connection.Open();
                _transaction = _connection.BeginTransaction();
                var requete = "select * FROM ordre_pai WHERE mois = '" + paiement.MoisPaiement + "' AND exercice = " + paiement.Exercice;
                _command.CommandText = requete;
                _command.Transaction = _transaction;
                var reader = _command.ExecuteReader();
                if (!reader.HasRows)
                {
                    reader.Close();
                    requete = "INSERT INTO ordre_pai (`montant_total`,`date_ord`,`exercice`,`mois`,controleur,liquidateur,directeur) " +
                       " VALUES (@montant_total,@date_ord, " + paiement.Exercice + ", '" + paiement.MoisPaiement + "',@controleur,@liquidateur,@directeur)";
                    _command.Parameters.Add(new MySqlParameter("montant_total", paiement.MontantTotal));
                    _command.Parameters.Add(new MySqlParameter("date_ord", DateTime.Now));
                    _command.Parameters.Add(new MySqlParameter("controleur", paiement.Controleur));
                    _command.Parameters.Add(new MySqlParameter("liquidateur",paiement.Liquidateur));
                    _command.Parameters.Add(new MySqlParameter("directeur", paiement.Directeur));
                    _command.CommandText = requete;
                    _command.Transaction = _transaction;
                    _command.ExecuteNonQuery();

                    //selectionner le dernier numero de paiement
                    requete = "select MAX(id_paie)  FROM ordre_pai";
                    _command.CommandText = requete;
                    _command.Connection = _connection;
                    _command.Transaction = _transaction;
                    _command.ExecuteNonQuery();
                    paiement.IDPaie = (int)_command.ExecuteScalar();
                    if (flag)
                    {
                        foreach (var p in liste)
                        {
                            requete = "UPDATE ordre_pai SET `montant_total` = `montant_total`+" + p.SalaireNet + " WHERE `id_paie` = " + paiement.IDPaie;
                            _command.CommandText = requete;
                            _command.Transaction = _transaction;
                            _command.ExecuteNonQuery();

                            requete = "INSERT INTO paimn_tbl( id_pai, num_mat,conge, cout_absc, salaire_brut,taux_cnps,irpp,onasa,charge_pat,prime_loge" +
                                                ",prime_grd,prime_respo,heure_supp,ind_transp,acompte_paye,avanceSa,soinFami,salaire_net,modePaiement," +
                                                "idAcpt,cout_salariale,salaireBase,jour_pres,service,bank,compte,anciennete,cnrt,cnrtEmployeur) VALUES(  @id_pai, @num_mat,@conge," +
                                                "@cout_absc, @salaire_brut,@taux_cnps,@irpp,@onasa,@charge_pat,@prime_loge,@prime_grd,@prime_respo," +
                                                "@heure_supp,@ind_transp,@acompte_paye,@avanceSa,@soinFami,@salaire_net,@mode_paiement,@idAcpt," +
                                                "@cout_salariale,@salaireBase,@jour_pres,@service,@bank,@compte,@anciennete,@cnrt,@cnrtEmployeur)";
                            _command.Parameters.Add(new MySqlParameter("id_pai", paiement.IDPaie));
                            _command.Parameters.Add(new MySqlParameter("num_mat", p.NumeroMatricule));
                            _command.Parameters.Add(new MySqlParameter("conge", p.CongeAnnuel));
                            _command.Parameters.Add(new MySqlParameter("cout_absc", p.CoutAbsence));
                            _command.Parameters.Add(new MySqlParameter("salaire_brut", p.SalaireBrut));
                            _command.Parameters.Add(new MySqlParameter("taux_cnps", p.CNPS));
                            _command.Parameters.Add(new MySqlParameter("irpp", p.IRPP));
                            _command.Parameters.Add(new MySqlParameter("onasa", p.ONASA));
                            _command.Parameters.Add(new MySqlParameter("charge_pat", p.ChargePatronale));
                            _command.Parameters.Add(new MySqlParameter("prime_loge", p.Indemnites));
                            _command.Parameters.Add(new MySqlParameter("prime_grd", p.AutresPrimes));
                            _command.Parameters.Add(new MySqlParameter("prime_respo", p.PrimeMotivation));
                            _command.Parameters.Add(new MySqlParameter("heure_supp", p.FraisCommunication));
                            _command.Parameters.Add(new MySqlParameter("ind_transp", p.Transport));
                            _command.Parameters.Add(new MySqlParameter("acompte_paye", p.AcomptePaye));
                            _command.Parameters.Add(new MySqlParameter("avanceSa", p.AvanceSurSalaire));
                            _command.Parameters.Add(new MySqlParameter("soinFami", p.ChargeSoinFamille));
                            _command.Parameters.Add(new MySqlParameter("salaire_net", p.SalaireNet));
                            _command.Parameters.Add(new MySqlParameter("mode_paiement", p.ModePaiement));
                            _command.Parameters.Add(new MySqlParameter("idAcpt", p.IDAcompte));
                            _command.Parameters.Add(new MySqlParameter("cout_salariale", p.CoutDuSalarie));
                            _command.Parameters.Add(new MySqlParameter("salaireBase", p.SalaireBase));
                            _command.Parameters.Add(new MySqlParameter("jour_pres", p.JourPrestations));
                            _command.Parameters.Add(new MySqlParameter("service", p.Service));
                            _command.Parameters.Add(new MySqlParameter("bank", p.Banque));
                            _command.Parameters.Add(new MySqlParameter("compte", p.Compte));
                            _command.Parameters.Add(new MySqlParameter("anciennete", p.GainAnciennete));
                            _command.Parameters.Add(new MySqlParameter("cnrt", p.CNRT));
                            _command.Parameters.Add(new MySqlParameter("cnrtEmployeur", p.CNRTEmploye));
                            _command.Transaction = _transaction;
                            _command.CommandText = requete;
                            _command.ExecuteNonQuery();

                            requete = "UPDATE accompte_tbl SET rembourse = rembourse + " + p.AcomptePaye + " WHERE id = " +
                                p.IDAcompte + " AND num_mat = @num_mat11";
                            _command.CommandText = requete;
                            _command.Parameters.Add(new MySqlParameter("num_mat11", p.NumeroMatricule));
                            _command.Transaction = _transaction;
                            _command.ExecuteNonQuery();

                            _command.Parameters.Clear();
                        }
                    }
                    _transaction.Commit();
                    MonMessageBox.ShowBox("Nouveau etat de paie crée avec succés", "Enregistrement ordre de paiement");
                    return paiement.IDPaie;
                }
                else
                {
                    MonMessageBox.ShowBox("L' etat de paie du mois de " + paiement.MoisPaiement + " " + paiement.Exercice +
                        " est deja enregistrer dans la base de données. Veuillez seulement le modifier", "Erreur");
                    return 0;
                }
            }
            catch (Exception exception)
            {
                if (_transaction != null)
                {
                    _transaction.Rollback();
                }
                MonMessageBox.ShowBox("L'enregistrement a échoué", "Enregistre paiement", exception);
                return 0;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }

        //Modifier les ordres de paiement
        public static bool ModifierOrdreDePaiement(Paiement paiement, double ancienCoutSalarial, double ancienAcomptePaye)
        {
            bool flag = false;
            try
            {
                _connection.Open();
                _transaction = _connection.BeginTransaction();
                string requete = "UPDATE ordre_pai SET `montant_total` = `montant_total`-@ancienCout + @nouveauCout WHERE `id_paie` = " + paiement.IDPaie;
                _command.Parameters.Add(new MySqlParameter("ancienCout", ancienCoutSalarial));
                _command.Parameters.Add(new MySqlParameter("nouveauCout", paiement.CoutDuSalarie));
                _command.CommandText = requete;
                _command.Transaction = _transaction;
                _command.ExecuteNonQuery();

                requete = "UPDATE paimn_tbl SET conge=@conge, cout_absc=@cout_absc, salaire_brut=@salaire_brut,taux_cnps=@taux_cnps,irpp=@irpp" +
                    ",onasa=@onasa,charge_pat=@charge_pat,prime_loge=@prime_loge,prime_grd=@prime_grd,prime_respo=@prime_respo,heure_supp=@heure_supp" +
                    ",ind_transp=@ind_transp,acompte_paye=@acompte_paye,avanceSa=@avanceSa,soinFami=@soinFami,salaire_net=@salaire_net" +
                    ",modePaiement=@modePaiement,cout_salariale=@cout_salariale,salaireBase=@salaireBase,jour_pres=@jour_pres,service=@service " +
                    ",bank=@bank, compte=@compte,anciennete=@anciennete , cnrt=@cnrt WHERE num_mat=@num_mat AND id_pai =" + paiement.IDPaie;

                _command.Parameters.Add(new MySqlParameter("num_mat", paiement.NumeroMatricule));
                _command.Parameters.Add(new MySqlParameter("conge", paiement.CongeAnnuel));
                _command.Parameters.Add(new MySqlParameter("cout_absc", paiement.CoutAbsence));
                _command.Parameters.Add(new MySqlParameter("salaire_brut", paiement.SalaireBrut));
                _command.Parameters.Add(new MySqlParameter("taux_cnps", paiement.CNPS));
                _command.Parameters.Add(new MySqlParameter("irpp", paiement.IRPP));
                _command.Parameters.Add(new MySqlParameter("onasa", paiement.ONASA));
                _command.Parameters.Add(new MySqlParameter("charge_pat", paiement.ChargePatronale));
                _command.Parameters.Add(new MySqlParameter("prime_loge", paiement.Indemnites));
                _command.Parameters.Add(new MySqlParameter("prime_grd", paiement.AutresPrimes));
                _command.Parameters.Add(new MySqlParameter("prime_respo", paiement.PrimeMotivation));
                _command.Parameters.Add(new MySqlParameter("heure_supp", paiement.FraisCommunication));
                _command.Parameters.Add(new MySqlParameter("ind_transp", paiement.Transport));
                _command.Parameters.Add(new MySqlParameter("acompte_paye", paiement.AcomptePaye));
                _command.Parameters.Add(new MySqlParameter("avanceSa", paiement.AvanceSurSalaire));
                _command.Parameters.Add(new MySqlParameter("soinFami", paiement.ChargeSoinFamille));
                _command.Parameters.Add(new MySqlParameter("salaire_net", paiement.SalaireNet));
                _command.Parameters.Add(new MySqlParameter("modePaiement", paiement.ModePaiement));
                _command.Parameters.Add(new MySqlParameter("idAcpt", paiement.IDAcompte));
                _command.Parameters.Add(new MySqlParameter("cout_salariale", paiement.CoutDuSalarie));
                _command.Parameters.Add(new MySqlParameter("salaireBase", paiement.SalaireBase));
                _command.Parameters.Add(new MySqlParameter("jour_pres", paiement.JourPrestations));
                _command.Parameters.Add(new MySqlParameter("service", paiement.Service));
                _command.Parameters.Add(new MySqlParameter("bank", paiement.Banque));
                _command.Parameters.Add(new MySqlParameter("compte", paiement.Compte));
                _command.Parameters.Add(new MySqlParameter("anciennete", paiement.GainAnciennete));
                _command.Parameters.Add(new MySqlParameter("cnrt", paiement.CNRT));

                _command.Transaction = _transaction;
                _command.CommandText = requete;
                _command.ExecuteNonQuery();

                requete = "UPDATE accompte_tbl SET rembourse = rembourse - " + ancienAcomptePaye + " + " + paiement.AcomptePaye + " WHERE id = " +
                    paiement.IDAcompte + " AND num_mat = @num_mat11";
                _command.CommandText = requete;
                _command.Parameters.Add(new MySqlParameter("num_mat11", paiement.NumeroMatricule));
                _command.Transaction = _transaction;
                _command.ExecuteNonQuery();

                _transaction.Commit();
                MonMessageBox.ShowBox("l'ordre de paiement a été enregistrée avec succés", "Enregistrement ordre de paiement");
                flag = true;
            }
            catch (Exception exception)
            {
                if (_transaction != null)
                {
                    _transaction.Rollback();
                }
                MonMessageBox.ShowBox("L'enregistrement a échoué", "Modification paiement", exception);
                flag = false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
            return flag;
        }
        public static bool SignateursLiquidateurDePaiement(Paiement paiement)
        {
            try
            {
                _connection.Open();
                string requete = "UPDATE ordre_pai SET `liquidateur` = @liquidateur WHERE  `id_paie` = " + paiement.IDPaie;
                _command.Parameters.Add(new MySqlParameter("liquidateur",paiement.Liquidateur));
                _command.CommandText = requete;
                _command.ExecuteNonQuery();
                return true;
            }
            catch { return false; }
            finally            
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }
        public static bool SignateursControleurDePaiement(Paiement paiement)
        {
            try
            {
                _connection.Open();
                string requete = "UPDATE ordre_pai SET  controleur=@controleur WHERE `id_paie` = " + paiement.IDPaie;
                _command.Parameters.Add(new MySqlParameter("controleur", paiement.Controleur));
                _command.CommandText = requete;
                _command.ExecuteNonQuery();
                return true;
            }
            catch { return false; }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }
        //supprimer un ordre de paiement       
        public static void SupprimerOrdreDePaiement(DataGridView dgvPaiement, int numPaiement)
        {
            try
            {
                _connection.Open();
                _transaction = _connection.BeginTransaction();
                foreach (DataGridViewRow dtgRow in dgvPaiement.SelectedRows)
                {
                    var numeroMatricule = dtgRow.Cells[0].Value.ToString();
                    int numeroAcompte = 0;
                    if (Int32.TryParse(dtgRow.Cells[24].Value.ToString(), out numeroAcompte))
                    { }
                    var salaireNet = Double.Parse(dtgRow.Cells[18].Value.ToString());
                    var montantAvance = Double.Parse(dtgRow.Cells[14].Value.ToString());
                    var requete = "DELETE FROM paimn_tbl  WHERE (id_pai = " + numPaiement + " AND num_mat =@num_mat)";
                    _command.Parameters.Add(new MySqlParameter("num_mat", numeroMatricule));
                    _command.CommandText = requete;
                    _command.Transaction = _transaction;
                    _command.ExecuteNonQuery();

                    if (numeroAcompte > 0)
                    {
                        requete = "UPDATE accompte_tbl SET `rembourse` = `rembourse` - @rembourser WHERE (  num_mat =@num_matri AND id =@idAcmpt)";
                        _command.Parameters.Add(new MySqlParameter("rembourser", montantAvance));
                        _command.Parameters.Add(new MySqlParameter("num_matri", numeroMatricule));
                        _command.Parameters.Add(new MySqlParameter("idAcmpt", numeroAcompte));
                        _command.CommandText = requete;
                        _command.Transaction = _transaction;
                        _command.ExecuteNonQuery();
                    }

                    _transaction.Commit();
                }
                MonMessageBox.ShowBox("La suppression des données est faite avec succés", "Suppression paiement");
            }
            catch (Exception exception)
            {
                if (_transaction != null)
                {
                    _transaction.Rollback();
                }
                MonMessageBox.ShowBox("La suppression des données du personnel a échoué", "Suppression paiement", exception);
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }
        //supprimer un ordre de paiement       
        public static void SupprimerOrdreDePaiement(int numPaiement)
        {
            try
            {
                _connection.Open();
                _transaction = _connection.BeginTransaction();
                var requete = "SELECT * FROM paimn_tbl WHERE acompte_paye >0 AND (id_pai=" + numPaiement + ")";
                _command.CommandText = requete;
                _command.Transaction = _transaction;
                var reader = _command.ExecuteReader();
                var dt = new DataTable();
                dt.Load(reader);
                reader.Close();

                var requete1 = "DELETE FROM ordre_pai  WHERE (id_paie = " + numPaiement + ")";
                _command.CommandText = requete1;
                _command.Transaction = _transaction;
                _command.ExecuteNonQuery();

                foreach (DataRow dtR in dt.Rows)
                {
                    var acompte = Convert.ToDouble(dtR.ItemArray[15].ToString());
                    var numeroMatricule = dtR.ItemArray[2].ToString();
                    var numeroAcompte = Convert.ToInt32(dtR.ItemArray[20].ToString());
                    requete = "UPDATE accompte_tbl SET `rembourse` = `rembourse` - @rembourser WHERE (  num_mat =@num_matri AND id =@idAcmpt)";
                    _command.Parameters.Add(new MySqlParameter("rembourser", acompte));
                    _command.Parameters.Add(new MySqlParameter("num_matri", numeroMatricule));
                    _command.Parameters.Add(new MySqlParameter("idAcmpt", numeroAcompte));
                    _command.CommandText = requete;
                    _command.Transaction = _transaction;
                    _command.ExecuteNonQuery();
                    _command.Parameters.Clear();
                }
                _transaction.Commit();
                MonMessageBox.ShowBox("La suppression des données est faite avec succés", "Suppression paiement");
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("La suppression des données du personnel a échoué", "Suppression paiement", exception);
            }
            finally
            {
                _connection.Close();
            }
        }
        public static double CumulDesOperations(string field, int exercice, string numeroMatricule)
        {
            try
            {

                var p = new Paiement();
                _connection.Open();
                string requete = "SELECT SUM(paimn_tbl." + field + ") FROM paimn_tbl INNER JOIN ordre_pai ON paimn_tbl.id_pai" +
                "= ordre_pai.id_paie WHERE paimn_tbl.num_mat=@num_mat AND  ordre_pai.exercice=" + exercice;
                _command.CommandText = requete;
                _command.Parameters.Add(new MySqlParameter("num_mat", numeroMatricule));
                return (double)_command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("", ex);
                return 0;
            }
            finally
            {
                _command.Parameters.Clear();
                _connection.Close();
            }
        }
        public static bool InsererModifierOrdreDePaiement
            (Paiement paiement, int idAcompte)
        {
            try
            {
                _connection.Open();
                _transaction = _connection.BeginTransaction();
                string requete = "SELECT * FROM paimn_tbl WHERE id_pai = " + paiement.IDPaie + " AND num_mat =@num ";
                _command.CommandText = requete;
                _command.Parameters.Add(new MySqlParameter("num", paiement.NumeroMatricule));
                _command.Transaction = _transaction;
                var reader = _command.ExecuteReader();
                if (!reader.HasRows)
                {
                    reader.Close();
                    requete = "UPDATE ordre_pai SET `montant_total` = `montant_total`+" + paiement.SalaireNet + " WHERE `id_paie` = " + paiement.IDPaie;
                    _command.CommandText = requete;
                    _command.Transaction = _transaction;
                    _command.ExecuteNonQuery();

                    requete = "INSERT INTO paimn_tbl( id_pai, num_mat,conge, cout_absc, salaire_brut,taux_cnps,irpp,onasa,charge_pat,prime_loge" +
                                        ",prime_grd,prime_respo,heure_supp,ind_transp,acompte_paye,avanceSa,soinFami,salaire_net,modePaiement,idAcpt,cout_salariale," +
                                        "salaireBase,jour_pres,service,bank,compte,anciennete,cnrt) VALUES(  @id_pai, @num_mat,@conge, @cout_absc, @salaire_brut,@taux_cnps,@irpp," +
                                        "@onasa,@charge_pat,@prime_loge,@prime_grd,@prime_respo,@heure_supp,@ind_transp,@acompte_paye,@avanceSa," +
                                        "@soinFami,@salaire_net,@mode_paiement,@idAcpt,@cout_salariale,@salaireBase,@jour_pres,@service,@bank,@compte,@anciennete,@cnrt)";
                    _command.Parameters.Add(new MySqlParameter("id_pai", paiement.IDPaie));
                    _command.Parameters.Add(new MySqlParameter("num_mat", paiement.NumeroMatricule));
                    _command.Parameters.Add(new MySqlParameter("conge", paiement.CongeAnnuel));
                    _command.Parameters.Add(new MySqlParameter("cout_absc", paiement.CoutAbsence));
                    _command.Parameters.Add(new MySqlParameter("salaire_brut", paiement.SalaireBrut));
                    _command.Parameters.Add(new MySqlParameter("taux_cnps", paiement.CNPS));
                    _command.Parameters.Add(new MySqlParameter("irpp", paiement.IRPP));
                    _command.Parameters.Add(new MySqlParameter("onasa", paiement.ONASA));
                    _command.Parameters.Add(new MySqlParameter("charge_pat", paiement.ChargePatronale));
                    _command.Parameters.Add(new MySqlParameter("prime_loge", paiement.Indemnites));
                    _command.Parameters.Add(new MySqlParameter("prime_grd", paiement.AutresPrimes));
                    _command.Parameters.Add(new MySqlParameter("prime_respo", paiement.PrimeMotivation));
                    _command.Parameters.Add(new MySqlParameter("heure_supp", paiement.FraisCommunication));
                    _command.Parameters.Add(new MySqlParameter("ind_transp", paiement.Transport));
                    _command.Parameters.Add(new MySqlParameter("acompte_paye", paiement.AcomptePaye));
                    _command.Parameters.Add(new MySqlParameter("avanceSa", paiement.AvanceSurSalaire));
                    _command.Parameters.Add(new MySqlParameter("soinFami", paiement.ChargeSoinFamille));
                    _command.Parameters.Add(new MySqlParameter("salaire_net", paiement.SalaireNet));
                    _command.Parameters.Add(new MySqlParameter("mode_paiement", paiement.ModePaiement));
                    _command.Parameters.Add(new MySqlParameter("idAcpt", paiement.IDAcompte));
                    _command.Parameters.Add(new MySqlParameter("cout_salariale", paiement.CoutDuSalarie));
                    _command.Parameters.Add(new MySqlParameter("salaireBase", paiement.SalaireBase));
                    _command.Parameters.Add(new MySqlParameter("jour_pres", paiement.JourPrestations));
                    _command.Parameters.Add(new MySqlParameter("service", paiement.Service));
                    _command.Parameters.Add(new MySqlParameter("bank", paiement.Banque));
                    _command.Parameters.Add(new MySqlParameter("compte", paiement.Compte));
                    _command.Parameters.Add(new MySqlParameter("anciennete", paiement.GainAnciennete));
                    _command.Parameters.Add(new MySqlParameter("cnrt", paiement.GainAnciennete));
                    _command.Transaction = _transaction;
                    _command.CommandText = requete;
                    _command.ExecuteNonQuery();

                    requete = "UPDATE accompte_tbl SET rembourse = rembourse + " + paiement.AcomptePaye + " WHERE id = " +
                        idAcompte + " AND num_mat = @num_mat11";
                    _command.CommandText = requete;
                    _command.Parameters.Add(new MySqlParameter("num_mat11", paiement.NumeroMatricule));
                    _command.Transaction = _transaction;
                    _command.ExecuteNonQuery();

                    _transaction.Commit();
                    MonMessageBox.ShowBox("Nouvelles données de paiement est inserées avec succés dans la base de données", "Insertion paiement");
                    return true;
                }
                else
                {
                    MonMessageBox.ShowBox("Les données de paiement de cet employé sont inserées deja dans ce bulletin de paye", "Erreur paiement");
                    return false;
                }

            }
            catch (Exception exception)
            {
                if (_transaction != null)
                {
                    _transaction.Rollback();
                }
                MonMessageBox.ShowBox("L'insertion  des données de paiement a échoué", "Suppression paiement", exception);
                return false;
            }
            finally
            {
                _command.Parameters.Clear();
                _connection.Close();
            }
        }

        // listes des paiement
        public static List<Paiement > ListeoOrdrePaiement()
        {
            var liste = new List<Paiement>();
            try
            {
                _connection.Open();
                string requete = "SELECT * FROM ordre_pai ORDER BY date_ord DESC";
                _command.CommandText = requete;
                MySqlDataReader reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var paie = new Paiement();
                    paie.IDPaie = reader.GetInt32(0);
                    paie.MontantTotal = reader.GetDouble(3);
                    paie.DatePaiement = reader.GetDateTime(2);
                    paie.Exercice = reader.GetInt32(3);
                    paie.MoisPaiement = reader.GetString(4);
                    paie.Controleur =!reader.IsDBNull(5) ? reader.GetString(5) : "";
                    paie.Liquidateur = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    paie.Directeur = !reader.IsDBNull(7) ? reader.GetString(7) : "";
                    liste.Add(paie);
                }
            }
            catch { }
            finally
            {
                _connection.Close();
            }
            return liste;
        }
        public static Paiement ListeoOrdrePaiement(int idPaie)
        {
         
            try
            {
                _connection.Open();
                string requete = "SELECT * FROM ordre_pai WHERE id_Paie = " +idPaie +" ORDER BY date_ord DESC";
                _command.CommandText = requete;
                MySqlDataReader reader = _command.ExecuteReader();

                var paie = new Paiement(); 
                while (reader.Read())
                {
                    paie.IDPaie = reader.GetInt32(0);
                    paie.MontantTotal = reader.GetDouble(3);
                    paie.DatePaiement = reader.GetDateTime(2);
                    paie.Exercice = reader.GetInt32(3);
                    paie.MoisPaiement = reader.GetString(4);
                    paie.Controleur = !reader.IsDBNull(5) ? reader.GetString(5) : "";
                    paie.Liquidateur = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    paie.Directeur = !reader.IsDBNull(7) ? reader.GetString(7) : "";
                }
                return paie;
            }
            catch { return null; }
            finally
            {
                _connection.Close();
            }
        }

        // listes des paiement
        public static DataTable ListeoOrdrePaiementParAnnee(int exercice)
        {
            DataTable dt = new DataTable();
            try
            {
                _connection.Open();
                string requete = "SELECT * FROM ordre_pai WHERE exercice = " + exercice + " ORDER BY date_ord";
                _command.CommandText = requete;
                MySqlDataReader reader = _command.ExecuteReader();
                dt.Load(reader);
            }
            finally
            {
                _connection.Close();
            }
            return dt;
        }

        // listes des paiement
        public static List<Paiement> ListeDetailsPaiement(int numeroPaiement)
        {

            try
            {
                var liste = new List<Paiement>();
                _connection.Open();
                string requete = "SELECT paimn_tbl.*, pers_tbl.* FROM paimn_tbl INNER JOIN ordre_pai ON paimn_tbl.id_pai" +
                "= ordre_pai.id_paie INNER JOIN pers_tbl ON paimn_tbl.num_mat = pers_tbl.num_mat  " +
                " WHERE paimn_tbl.id_pai = " + numeroPaiement + " ORDER BY pers_tbl.nom,pers_tbl.prenom ";
                _command.CommandText = requete;
                MySqlDataReader reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var p = new Paiement();
                    p.ID = reader.GetInt32(0);
                    p.IDPaie = reader.GetInt32(1);
                    p.NumeroMatricule = reader.GetString(2);
                    p.CongeAnnuel = reader.GetDouble(3);
                    p.CoutAbsence = reader.GetDouble(4);
                    p.SalaireBrut = reader.GetDouble(5);
                    p.CNPS = reader.GetDouble(6);
                    p.IRPP = reader.GetDouble(7);
                    p.ONASA = reader.GetDouble(8);
                    p.ChargePatronale = reader.GetDouble(9);
                    p.Indemnites = reader.GetDouble(10);
                    p.AutresPrimes = reader.GetDouble(11);
                    p.PrimeMotivation = reader.GetDouble(12);
                    p.FraisCommunication = reader.GetDouble(13);
                    p.Transport = !reader.IsDBNull(14) ? reader.GetDouble(14) : 0;
                    p.AcomptePaye = reader.GetDouble(15);
                    p.AvanceSurSalaire = reader.GetDouble(16);
                    p.ChargeSoinFamille = reader.GetDouble(17);
                    p.SalaireNet = reader.GetDouble(18);
                    p.ModePaiement = !reader.IsDBNull(19) ? reader.GetString(19) : "";
                    p.IDAcompte = reader.GetInt32(20);
                    p.CoutDuSalarie = reader.GetDouble(21);
                    p.SalaireBase = !reader.IsDBNull(22) ? reader.GetDouble(22) : .0;
                    p.JourPrestations = !reader.IsDBNull(23) ? reader.GetDouble(23) : .0;
                    p.Service = !reader.IsDBNull(24) ? reader.GetString(24) : "";
                    p.Banque = !reader.IsDBNull(25) ? reader.GetString(25) : "";
                    p.Compte = !reader.IsDBNull(26) ? reader.GetString(26) : "";
                    p.GainAnciennete = !reader.IsDBNull(27) ? reader.GetDouble(27) : 0;
                    p.CNRT = !reader.IsDBNull(28) ? reader.GetDouble(28) : 0;
                    p.CNRTEmploye = !reader.IsDBNull(29) ? reader.GetDouble(29) : 0;

                    p.Employe = reader.GetString(31) + " " + reader.GetString(32);
                    liste.Add(p);
                }
                return liste;
            }

            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste detail paiement", ex);
                return null;
            }
            finally
            {
                _connection.Close();
            }
        }
        public static Paiement PaiementParMatricule(int numeroPaiement, string numeMatricule)
        {
            try
            {
                var p = new Paiement();
                _connection.Open();
                string requete = "SELECT paimn_tbl.*, pers_tbl.* FROM paimn_tbl INNER JOIN ordre_pai ON paimn_tbl.id_pai" +
                "= ordre_pai.id_paie INNER JOIN pers_tbl ON paimn_tbl.num_mat = pers_tbl.num_mat  " +
                " WHERE paimn_tbl.id_pai = " + numeroPaiement + " AND pers_tbl.num_mat  = @numMat  ORDER BY pers_tbl.nom, pers_tbl.prenom";
                _command.CommandText = requete;
                _command.Parameters.Add(new MySqlParameter("numMat", numeMatricule));
                MySqlDataReader reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    p.ID = reader.GetInt32(0);
                    p.IDPaie = reader.GetInt32(1);
                    p.NumeroMatricule = reader.GetString(2);
                    p.CongeAnnuel = reader.GetDouble(3);
                    p.CoutAbsence = reader.GetDouble(4);
                    p.SalaireBrut = reader.GetDouble(5);
                    p.CNPS = reader.GetDouble(6);
                    p.IRPP = reader.GetDouble(7);
                    p.ONASA = reader.GetDouble(8);
                    p.ChargePatronale = reader.GetDouble(9);
                    p.Indemnites = reader.GetDouble(10);
                    p.AutresPrimes = reader.GetDouble(11);
                    p.PrimeMotivation = reader.GetDouble(12);
                    p.FraisCommunication = reader.GetDouble(13);
                    p.Transport = !reader.IsDBNull(14) ? reader.GetDouble(14) : 0;
                    p.AcomptePaye = reader.GetDouble(15);
                    p.AvanceSurSalaire = reader.GetDouble(16);
                    p.ChargeSoinFamille = reader.GetDouble(17);
                    p.SalaireNet = reader.GetDouble(18);
                    p.ModePaiement = !reader.IsDBNull(19) ? reader.GetString(19) : "";
                    p.IDAcompte = reader.GetInt32(20);
                    p.CoutDuSalarie = reader.GetDouble(21);
                    p.SalaireBase = !reader.IsDBNull(22) ? reader.GetDouble(22) : .0;
                    p.JourPrestations = !reader.IsDBNull(23) ? reader.GetDouble(23) : .0;
                    p.Service = !reader.IsDBNull(24) ? reader.GetString(24) : "";
                    p.Banque = !reader.IsDBNull(25) ? reader.GetString(25) : "";
                    p.Compte = !reader.IsDBNull(26) ? reader.GetString(26) : "";
                    p.GainAnciennete = !reader.IsDBNull(27) ? reader.GetDouble(27) : 0;
                    p.CNRT = !reader.IsDBNull(28) ? reader.GetDouble(28) : 0;
                    p.CNRTEmploye = !reader.IsDBNull(29) ? reader.GetDouble(29) : 0;
                    p.Employe = reader.GetString(31) + " " + reader.GetString(32);
                }
                return p;
            }

            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste detail paiement", ex);
                return null;
            }
            finally
            {
                _command.Parameters.Clear();
                _connection.Close();
            }
        }

        public static List<Personnel> ListeMatriculePaye(int numeroPaiement, string departement, string typeContrat1)
        {
            var liste = new List<Personnel>();
            try
            {
                _connection.Open();
                string requete = "	SELECT  paimn_tbl.num_mat, direction_tbl.direction , paimn_tbl.id_pai FROM  pers_tbl INNER JOIN service_tbl ON pers_tbl.num_mat" +
                "= service_tbl.num_mat INNER JOIN  paimn_tbl ON pers_tbl.num_mat = paimn_tbl.num_mat INNER JOIN " +
                "division_tbl ON  division_tbl.id_div =service_tbl.id_div  INNER JOIN direction_tbl ON direction_tbl.id_direction = division_tbl.id_direction WHERE (paimn_tbl.id_pai = " + numeroPaiement +
                ") AND (direction_tbl.direction = @dep)  AND (service_tbl.contrat LIKE @type1 OR service_tbl.contrat LIKE @type1) ORDER BY pers_tbl.nom, pers_tbl.prenom";
                _command.CommandText = requete;
                _command.Parameters.Add(new MySqlParameter("dep", departement));
                _command.Parameters.AddWithValue("type1", "%" + typeContrat1 + "%");
                //_command.Parameters.AddWithValue("type2", "%" + typeContrat2 + "%");
                MySqlDataReader reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var personnel = new Personnel();
                    personnel.NumeroMatricule = reader.GetString(0);
                    liste.Add(personnel);
                }
                return liste;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("", ex);
                return null;
            }
            finally
            {
                _command.Parameters.Clear();
                _connection.Close();
            }
        }

        public static List<Paiement> ListeDetailsPaiementParNumeroAcompte(int numero)
        {

            try
            {
                var liste = new List<Paiement>();
                _connection.Open();
                string requete = "SELECT paimn_tbl.*, pers_tbl.* FROM paimn_tbl INNER JOIN ordre_pai ON paimn_tbl.id_pai" +
                "= ordre_pai.id_paie INNER JOIN pers_tbl ON paimn_tbl.num_mat = pers_tbl.num_mat  " +
                " WHERE paimn_tbl.idAcpt = " + numero + " ORDER BY pers_tbl.nom,pers_tbl.prenom ";
                _command.CommandText = requete;
                MySqlDataReader reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var p = new Paiement();
                    p.ID = reader.GetInt32(0);
                    p.IDPaie = reader.GetInt32(1);
                    p.NumeroMatricule = reader.GetString(2);
                    p.CongeAnnuel = reader.GetDouble(3);
                    p.CoutAbsence = reader.GetDouble(4);
                    p.SalaireBrut = reader.GetDouble(5);
                    p.CNPS = reader.GetDouble(6);
                    p.IRPP = reader.GetDouble(7);
                    p.ONASA = reader.GetDouble(8);
                    p.ChargePatronale = reader.GetDouble(9);
                    p.Indemnites = reader.GetDouble(10);
                    p.AutresPrimes = reader.GetDouble(11);
                    p.PrimeMotivation = reader.GetDouble(12);
                    p.FraisCommunication = reader.GetDouble(13);
                    p.Transport = !reader.IsDBNull(14) ? reader.GetDouble(14) : 0;
                    p.AcomptePaye = reader.GetDouble(15);
                    p.AvanceSurSalaire = reader.GetDouble(16);
                    p.ChargeSoinFamille = reader.GetDouble(17);
                    p.SalaireNet = reader.GetDouble(18);
                    p.ModePaiement = !reader.IsDBNull(19) ? reader.GetString(19) : "";
                    p.IDAcompte = reader.GetInt32(20);
                    p.CoutDuSalarie = reader.GetDouble(21);
                    p.SalaireBase = !reader.IsDBNull(22) ? reader.GetDouble(22) : .0;
                    p.JourPrestations = !reader.IsDBNull(23) ? reader.GetDouble(23) : .0;
                    p.Service = !reader.IsDBNull(24) ? reader.GetString(24) : "";
                    p.Banque = !reader.IsDBNull(25) ? reader.GetString(25) : "";
                    p.Compte = !reader.IsDBNull(26) ? reader.GetString(26) : "";
                    p.GainAnciennete = !reader.IsDBNull(27) ? reader.GetDouble(27) : 0;
                    p.CNRT = !reader.IsDBNull(28) ? reader.GetDouble(28) : 0;
                    liste.Add(p);
                }
                return liste;
            }

            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste detail paiement", ex);
                return null;
            }
            finally
            {
                _connection.Close();
            }
        }

        // listes des paiement
        public static List<Paiement> ListeDetailsPaiement(int exercice, string mois)
        {

            try
            {
                var liste = new List<Paiement>();
                _connection.Open();
                string requete = "SELECT paimn_tbl.*, pers_tbl.* FROM paimn_tbl INNER JOIN ordre_pai ON paimn_tbl.id_pai" +
                "= ordre_pai.id_paie INNER JOIN pers_tbl ON paimn_tbl.num_mat = pers_tbl.num_mat  " +
                " WHERE ordre_pai.exercice = " + exercice + " AND ordre_pai.mois ='" + mois + "'  ORDER BY pers_tbl.nom,pers_tbl.prenom ";
                _command.CommandText = requete;
                MySqlDataReader reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var p = new Paiement();
                    p.ID = reader.GetInt32(0);
                    p.IDPaie = reader.GetInt32(1);
                    p.NumeroMatricule = reader.GetString(2);
                    p.CongeAnnuel = reader.GetDouble(3);
                    p.CoutAbsence = reader.GetDouble(4);
                    p.SalaireBrut = reader.GetDouble(5);
                    p.CNPS = reader.GetDouble(6);
                    p.IRPP = reader.GetDouble(7);
                    p.ONASA = reader.GetDouble(8);
                    p.ChargePatronale = reader.GetDouble(9);
                    p.Indemnites = reader.GetDouble(10);
                    p.AutresPrimes = reader.GetDouble(11);
                    p.PrimeMotivation = reader.GetDouble(12);
                    p.FraisCommunication = reader.GetDouble(13);
                    p.Transport = !reader.IsDBNull(14) ? reader.GetDouble(14) : 0;
                    p.AcomptePaye = reader.GetDouble(15);
                    p.AvanceSurSalaire = reader.GetDouble(16);
                    p.ChargeSoinFamille = reader.GetDouble(17);
                    p.SalaireNet = reader.GetDouble(18);
                    p.ModePaiement = !reader.IsDBNull(19) ? reader.GetString(19) : "";
                    p.IDAcompte = reader.GetInt32(20);
                    p.CoutDuSalarie = reader.GetDouble(21);
                    p.SalaireBase = !reader.IsDBNull(22) ? reader.GetDouble(22) : 0.0;
                    p.JourPrestations = !reader.IsDBNull(23) ? reader.GetDouble(23) : .0;
                    p.Service = !reader.IsDBNull(24) ? reader.GetString(24) : "";
                    p.Banque = !reader.IsDBNull(25) ? reader.GetString(25) : "";
                    p.Banque = !reader.IsDBNull(26) ? reader.GetString(26) : "";
                    p.GainAnciennete = !reader.IsDBNull(27) ? reader.GetDouble(27) : 0;
                    p.CNRT = !reader.IsDBNull(28) ? reader.GetDouble(28) : 0;
                    liste.Add(p);
                }
                return liste;
            }

            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste detail paiement", ex);
                return null;
            }
            finally
            {
                _connection.Close();
            }
        }
        public static List<Paiement> ListeRecapPaiement(int idPaie)
        {

            try
            {
                var liste = new List<Paiement>();
                _connection.Open();
                string requete = "SELECT service, SUM(salaire_net),id_pai FROM paimn_tbl  WHERE id_pai = " + idPaie + " GROUP BY service ";
                _command.CommandText = requete;
                MySqlDataReader reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var p = new Paiement();
                    p.MontantTotal = reader.GetDouble(1);
                    p.LibelleRecap = !reader.IsDBNull(0) ? reader.GetString(0) : "";
                    p.IDPaie = reader.GetInt32(2);
                    liste.Add(p);
                }
                return liste;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste detail paiement", ex);
                return null;
            }
            finally
            {
                _connection.Close();
            }
        }

        public static int ObtenirDernierNumeroPaiement()
        {
            int numero = new int();
            try
            {
                _connection.Open();
                //selectionner le dernier numero d'ordre de paiement
                string requete = "select MAX(id_paie)  FROM ordre_pai";
                _command.CommandText = requete;
                _command.ExecuteNonQuery();
                numero = (int)_command.ExecuteScalar();
            }
            catch
            {
            }
            finally
            {
                _connection.Close();
            }
            return numero;
        }

        //enregistrer uneavance sur salaire
        public static bool EnregistrerUneAvanceSurSalaire(Paiement paiement)
        {
            bool flag = false;
            try
            {
                _connection.Open();

                var requete = "INSERT INTO avc_tbl (`montant`,`num_mat`,`date`,`exercice`,`mois`) " +
                       " VALUES (@montant_total, @num_mat ,@date_ord, " + paiement.Exercice + ", '" + paiement.MoisPaiement + "')";
                _command.Parameters.Add(new MySqlParameter("montant_total", paiement.MontantTotal));
                _command.Parameters.Add(new MySqlParameter("date_ord", paiement.DatePaiement));
                _command.Parameters.Add(new MySqlParameter("num_mat", paiement.NumeroMatricule));
                _command.CommandText = requete;
                _command.ExecuteNonQuery();
                MonMessageBox.ShowBox("Données de l'avance sur salaire a été enregistrée avec succés", "Enregistrement ordre de paiement");
                flag = true;
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("L'enregistrement a échoué", "Enregistre paiement", exception);
                flag = false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
            return flag;
        }

        //enregistrer uneavance sur salaire
        public static bool ModifierUneAvanceSurSalaire(Paiement paiement)
        {
            bool flag = false;
            try
            {
                _connection.Open();

                var requete = "UPDATE avc_tbl SET `montant` =@montant_total,`num_mat` = @num_mat" +
                    ",`date`=@date_ord,`exercice`=" + paiement.Exercice + ",`mois` = '" +
                    paiement.MoisPaiement + "' WHERE id = " + paiement.ID;
                _command.Parameters.Add(new MySqlParameter("montant_total", paiement.MontantTotal));
                _command.Parameters.Add(new MySqlParameter("date_ord", paiement.DatePaiement));
                _command.Parameters.Add(new MySqlParameter("num_mat", paiement.NumeroMatricule));
                _command.CommandText = requete;
                _command.ExecuteNonQuery();
                MonMessageBox.ShowBox("Données de l'avance sur salaire a été modifiée avec succés", "Enregistrement ordre de paiement");
                flag = true;
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("L'enregistrement a échoué", "Enregistre paiement", exception);
                flag = false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
            return flag;
        }

        //enregistrer uneavance sur salaire
        public static bool SupprimerUneAvanceSurSalaire(Paiement paiement)
        {
            bool flag = false;
            try
            {
                _connection.Open();
                var requete = "DELETE FROM avc_tbl WHERE id = " + paiement.ID;
                _command.CommandText = requete;
                _command.ExecuteNonQuery();
                MonMessageBox.ShowBox("Données de l'avance sur salaire a été supprimées avec succés", "Enregistrement ordre de paiement");
                flag = true;
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("L'enregistrement a échoué", "Enregistre paiement", exception);
                flag = false;
            }
            finally
            {
                _connection.Close();
            }
            return flag;
        }

        public static List<Paiement> ListeAvanceSurSalaire()
        {

            try
            {
                var liste = new List<Paiement>();
                _connection.Open();
                string requete = "SELECT * FROM avc_tbl  ORDER BY date DESC";
                _command.CommandText = requete;
                MySqlDataReader reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var p = new Paiement();
                    p.ID = reader.GetInt32(0);
                    p.MontantTotal = reader.GetDouble(1);
                    p.NumeroMatricule = reader.GetString(2);
                    p.DatePaiement = reader.GetDateTime(3);
                    p.MoisPaiement = reader.GetString(4);
                    p.Exercice = reader.GetInt32(5);
                    liste.Add(p);
                }
                return liste;
            }

            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste avance sur salaire", ex);
                return null;
            }
            finally
            {
                _connection.Close();
            }
        }

        public static List<Paiement> ListeAvanceSurSalaire(int exercice, string mois)
        {

            try
            {
                var liste = new List<Paiement>();
                _connection.Open();
                string requete = "SELECT * FROM avc_tbl  WHERE exercice = " + exercice + " AND  mois ='" + mois + "' ORDER BY date DESC";
                _command.CommandText = requete;
                MySqlDataReader reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var p = new Paiement();
                    p.ID = reader.GetInt32(0);
                    p.MontantTotal = reader.GetDouble(1);
                    p.NumeroMatricule = reader.GetString(2);
                    p.DatePaiement = reader.GetDateTime(3);
                    p.MoisPaiement = reader.GetString(4);
                    p.Exercice = reader.GetInt32(5);
                    liste.Add(p);
                }
                return liste;
            }

            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste avance sur salaire", ex);
                return null;
            }
            finally
            {
                _connection.Close();
            }
        }

        public static List<Paiement> ListeAvanceSurSalaire(int exercice, string mois, string numEmpl)
        {

            try
            {
                var liste = new List<Paiement>();
                _connection.Open();
                string requete = "SELECT * FROM avc_tbl  WHERE exercice = " + exercice + " AND  mois ='" + mois +
                    "' AND num_mat = @numMat ORDER BY date DESC";
                _command.CommandText = requete;
                _command.Parameters.Add(new MySqlParameter("numMat", numEmpl));
                MySqlDataReader reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var p = new Paiement();
                    p.ID = reader.GetInt32(0);
                    p.AvanceSurSalaire = reader.GetDouble(1);
                    p.NumeroMatricule = reader.GetString(2);
                    p.DatePaiement = reader.GetDateTime(3);
                    p.MoisPaiement = reader.GetString(4);
                    p.Exercice = reader.GetInt32(5);
                    p.Controleur = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    p.Liquidateur = !reader.IsDBNull(7) ? reader.GetString(7) : "";
                    p.Directeur = !reader.IsDBNull(8) ? reader.GetString(8) : "";
                    liste.Add(p);
                }
                return liste;
            }

            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste avance sur salaire", ex);
                return null;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }
        
        #endregion
    
        #region FORMATIONS
        //enregistrer les donnees de la formation
        public static bool EnregistrerUneFormation(Formation formation)
        {
            var flag = false;
            try
            {
                _connection.Open();
                var requete = "";
                if (formation.NumeroFormation==0)
                {
                    requete = "INSERT INTO `formation_tbl` (`type_form`, `date_debut`,`date_fin`, `duree_form`,`description`,formateur,lieu,imputation,exercice) " +
                     "VALUES (@type_form, @date_debut,@date_fin, @duree_form,@description,@formateur,@lieu,@imputation,@exercice)";
                }
                else
                {
                    requete = "UPDATE `formation_tbl` SET `type_form` =@type_form, `date_debut` = @date_debut,`date_fin`=@date_fin,lieu=@lieu,imputation=@imputation" +
                         ",`duree_form` =@duree_form , description =@description,formateur=@formateur,exercice=@exercice WHERE `id_form` =" + formation.NumeroFormation;
                }
                _command.Parameters.Add(new MySqlParameter("type_form", formation.TypeFormation));
                _command.Parameters.Add(new MySqlParameter("date_debut", formation.DateDebutFormation));
                _command.Parameters.Add(new MySqlParameter("date_fin", formation.DateFinFormation));
                _command.Parameters.Add(new MySqlParameter("duree_form", formation.DureeFormation));
                _command.Parameters.Add(new MySqlParameter("description", formation.Description));
                _command.Parameters.Add(new MySqlParameter("formateur", formation.Formateur));
                _command.Parameters.Add(new MySqlParameter("lieu", formation.LieuFormation));
                _command.Parameters.Add(new MySqlParameter("imputation", formation.Imputation));
                _command.Parameters.Add(new MySqlParameter("exercice", formation.Exercice));
                _command.CommandText = requete;
                _command.ExecuteNonQuery();
                flag = true;
            }
            catch (Exception exception)
            {
                if (_transaction != null)
                {
                    _transaction.Rollback();
                }
                MonMessageBox.ShowBox("L'enregistrement a échoué veuillez verifier les parametres et réessayer",
                    "Erreur enregistrement", exception);
                flag = false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
            return flag;
        }

        public static bool InsererUneFormation(Formation formation)
        {
            var flag = false;
            try
            {
                _connection.Open();
                var requete =
                   "INSERT INTO `form_detail` (`id_form`, `num_mat`,duree,frais,total,si_paye,nom,siProjet,fonction) VALUES (@id_form, @num_mat,@duree,@frais,@total,@si_paye,@nom,@siProjet,@fonction)";
                _command.Parameters.Add(new MySqlParameter("id_form", formation.NumeroFormation));
                _command.Parameters.Add(new MySqlParameter("num_mat", formation.NumeroMatricule));
                _command.Parameters.Add(new MySqlParameter("duree", formation.DureeFormation));
                _command.Parameters.Add(new MySqlParameter("frais", formation.Frais));
                _command.Parameters.Add(new MySqlParameter("total", formation.FraisTotal));
                _command.Parameters.Add(new MySqlParameter("si_paye", false));
                _command.Parameters.Add(new MySqlParameter("nom", formation.NomPersonnel));
                _command.Parameters.Add(new MySqlParameter("siProjet", formation.SiProjet));
                _command.Parameters.Add(new MySqlParameter("fonction", formation.Fonction));
                _command.CommandText = requete;
                _command.ExecuteNonQuery();
                _command.Parameters.Clear();
                flag = true;
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("L'enregistrement a échoué veuillez verifier les parametres et réessayer",
                    "Erreur enregistrement", exception);
                flag = false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
            return flag;
        }
        //supprimer les donnees de la formation
        public static void SuppressionDeLaFormation(Formation f)
        {
            try
            {
                var requete = "DELETE FROM form_detail WHERE id_form = " + f.NumeroFormation + " AND num_mat = @mat";
                _command.Parameters.Add(new MySqlParameter("mat", f.NumeroMatricule));
                _command.CommandText = requete;
                _connection.Open();
                _command.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("La suppression a échoué veuillez verifier les parametres et réessayer",
                    "Erreur modification", exception);
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }
        //supprimer les donnees de la formation
        public static void SuppressionDeLaFormation(int idFormation)
        {
            try
            {
                var requete = "DELETE FROM formation_tbl WHERE id_form = " + idFormation;
                _command.CommandText = requete;
                _connection.Open();
                _command.ExecuteNonQuery();
                MonMessageBox.ShowBox("Les données supprimées avec succés",
                    "Information modification");
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("La suppression a échoué veuillez verifier les parametres et réessayer",
                    "Erreur modification", exception);
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }

        //liste de la formation 
        public static List<Formation> ListeFormation()
        {
            var liste = new List<Formation>();
            try
            {
                var requete = "SELECT *  FROM  formation_tbl";
                _command.CommandText = requete;
                _connection.Open();
                var reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var formation = new Formation();
                    formation.NumeroFormation = reader.GetInt32(0);
                    formation.TypeFormation = reader.GetString(1);
                    formation.DateDebutFormation = reader.GetDateTime(2);
                    formation.DateFinFormation = reader.GetDateTime(3);
                    formation.DureeFormation = reader.GetInt32(4);
                    formation.Description = !reader.IsDBNull(5) ? reader.GetString(5) : "";
                    formation.Formateur = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    formation.LieuFormation = !reader.IsDBNull(7) ? reader.GetString(7) : "";
                    formation.Imputation = !reader.IsDBNull(8) ? reader.GetString(8) : "";
                    formation.Exercice = !reader.IsDBNull(9) ? reader.GetInt32(9) : 0;
                    liste.Add(formation);
                }

            }
            finally
            {
                _connection.Close();
            }
            return liste;
        }
        //liste de la formation 
        public static List<Formation> ListeFormation(int id)
        {
            var liste = new List<Formation>();
            try
            {
                var requete = "SELECT * FROM form_detail  WHERE id_form=" + id;
                _connection.Open();
                _command.CommandText = requete;
                var reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var f = new Formation();
                    f.NumeroFormation = reader.GetInt32(0);
                    f.NumeroMatricule = reader.GetString(1);
                    f.NomPersonnel = reader.GetString(6);
                    f.DureeFormation = !reader.IsDBNull(2) ? reader.GetInt32(2) : 0; 
                    f.Frais = !reader.IsDBNull(3) ? reader.GetDouble(3) : 0;
                    f.FraisTotal = !reader.IsDBNull(4) ? reader.GetDouble(4) : 0;
                    f.SiPayant = !reader.IsDBNull(5) ? reader.GetBoolean(5) : false;
                    f.SiProjet = !reader.IsDBNull(7) ? reader.GetBoolean(7) : false;
                    f.Fonction = !reader.IsDBNull(8) ? reader.GetString(8) : "";
                    liste.Add(f);
                }
                return liste;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste", ex);
                return null;
            }
            finally
            {
                _connection.Close();
            }
        }
        
        #endregion
        //liste de  log 
        public static DataTable Log()
        {
            DataTable dt = new DataTable();
            try
            {
                var requete = "SELECT * FROM track_tbl";
                _command.CommandText = requete;
                _connection.Open();
                var reader = _command.ExecuteReader();
                dt.Load(reader);

            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste track", ex);
            }
            finally
            {
                _connection.Close();
            }
            return dt;
        }

        //liste de  journal 
        public static DataTable Log(DateTime date)
        {
            var dt = new DataTable();
            try
            {
                var requete = "SELECT * FROM track_tbl WHERE time >=@time1 AND time <=@time2";
                _command.CommandText = requete;
                _command.Parameters.Add(new MySqlParameter("time1", date));
                _command.Parameters.Add(new MySqlParameter("time2", date.AddHours(24)));
                _connection.Open();
                var reader = _command.ExecuteReader();
                dt.Load(reader);

            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste track", ex);
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
            return dt;
        }

        //liste de  log 
        public static DataTable Log(string nomPers)
        {
            var dt = new DataTable();
            try
            {
                var requete = "SELECT * FROM track_tbl WHERE nom LIKE '%"+nomPers +"%'";
                _command.CommandText = requete;
                _connection.Open();
                var reader = _command.ExecuteReader();dt.Load(reader);

            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste track", ex);
            }
            finally
            {
                _connection.Close();
            }
            return dt;
        }
        //enregistrer les donnees de la log
        public static bool Tracker(string nomUtil, string nom,bool etat)
        {
            var flag = false;
            try
            {
                _connection.Open();
                var requete =
                    "INSERT INTO `track_tbl` (nom_util, nom,time,etat) VALUES(@nom_util,@nom,@time,@etat)";
                _command.Parameters.Add(new MySqlParameter("nom_util", nomUtil));
                _command.Parameters.Add(new MySqlParameter("nom", nom ));
                _command.Parameters.Add(new MySqlParameter("time", DateTime.Now));
                _command.Parameters.Add(new MySqlParameter("etat", etat));
                _command.CommandText = requete;
                _command.ExecuteNonQuery();
                flag = true;
            }
            catch (Exception )
            {
                flag = false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
            return flag;
        }

        //liste de  journal 
        public static DataTable AccorderPrivilege()
        {
            var dt = new DataTable();
            try
            {
                var requete = "SELECT * FROM privi_tbl";
                _command.CommandText = requete;
                _connection.Open();
                var reader = _command.ExecuteReader(); dt.Load(reader);

            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste track", ex);
            }
            finally
            {
                _connection.Close();
            }
            return dt;
        }

        //supprimer les donnees de la 
        public static void ViderLog()
        {
            try
            {
                var requete = "DELETE FROM track_tbl ";
                _command.CommandText = requete;
                _connection.Open();
                _command.ExecuteNonQuery();
                MonMessageBox.ShowBox("Données supprimées avec succés",
                    "Information");
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("La suppression a échoué veuillez verifier les parametres et réessayer",
                    "Erreur suppresiion", exception);
            }
            finally
            {
                _connection.Close();
            }
        }

        #region DOCUMENTS_APPROBATION
        //enregistrer les donnees de les bons 
        public static bool SupprimerUnDocument(int no, int idType)
        {
            var flag = false;
            try
            {
                if (MonMessageBox.ShowBox("Voulez vous supprimer les données de le document numéro " + no + "?", "Confirmation") == "1")
                {
                    _connection.Open();
                    _transaction = _connection.BeginTransaction();
                    var requete = "DELETE FROM `doc_tbl`  WHERE no = " + no ;
                    _command.CommandText = requete;
                    _command.Transaction = _transaction;
                    _command.ExecuteNonQuery();

                     requete = "DELETE FROM `det_facture_tbl`  WHERE id_doc = " + idType;
                    _command.CommandText = requete;
                    _command.Transaction = _transaction;
                    _command.ExecuteNonQuery();
                    MonMessageBox.ShowBox("Les données supprimées avec succés",
                        "Information enregistrement");
                    _transaction.Commit();
                    flag = true;
                }
            }
            catch (Exception exception)
            {
                if (_transaction != null)
                    _transaction.Rollback();
                MonMessageBox.ShowBox("La modification a échoué veuillez verifier les parametres et réessayer",
                    "Erreur enregistrement", exception);
                flag = false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
            return flag;
        }
        //liste de  bons 
  
        public static int DernierDuDocument(int id_doc, string categorie)
        {
            try
            {
                var requete = "SELECT MAX(id_doc) FROM doc_tbl WHERE id_type ="+id_doc +" AND cat_doc ='"+ categorie+"'" ;
                _command.CommandText = requete;
                _connection.Open();
                return (int) _command.ExecuteScalar();               
            }
            catch (Exception )
            {
                return 0 ;
            }
            finally
            {
                _connection.Close();
            }
        }

        public static List<Document> ListeDesDocuments(string categorie)
        {
            var liste = new List<Document>();
            try
            {
                var requete = "SELECT * FROM doc_tbl WHERE cat_doc like '%"+categorie +"'";
                _command.CommandText = requete;
                _connection.Open();
                var reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var doc = new Document();
                    doc.NumeroDocument  = reader.GetInt32(0);
                    doc.ReferenceDocument = reader.GetString(1);
                    doc.RootPathDocument = !reader.IsDBNull(2) ? reader.GetString(2) : "";
                    doc.MontantHT = !reader.IsDBNull(3) ? reader.GetDouble(3) : 0.0;
                    doc.TVA = !reader.IsDBNull(4) ? reader.GetDouble(4) : 0.0;
                    doc.MontantTTC = !reader.IsDBNull(5) ? reader.GetDouble(5) : 0.0;
                    doc.EcheancePaiement = !reader.IsDBNull(6) ? reader.GetDateTime(6) : DateTime.Now;
                    doc.DateEnregistrement = !reader.IsDBNull(7) ? reader.GetDateTime(7) : DateTime.Now;
                    doc.NumeroTiers = !reader.IsDBNull(8) ? reader.GetInt32(8) : 0;
                    doc.NumeroType = !reader.IsDBNull(9) ? reader.GetInt32(9) : 0;
                    doc.Exercice = !reader.IsDBNull(10) ? reader.GetInt32(10) : 0;
                    doc.MotCle = !reader.IsDBNull(11) ? reader.GetString(11) : "";
                    doc.EcheanceLivraison = !reader.IsDBNull(12) ? reader.GetDateTime(12) : DateTime.Now;
                    doc.Payable = !reader.IsDBNull(13) ? reader.GetBoolean(13) : true;
                    doc.IDTypeDocument = !reader.IsDBNull(14) ? reader.GetInt32(14) : 0;
                    doc.CategorieDocument = !reader.IsDBNull(15) ? reader.GetString(15) : "";
                    doc.ModalitePaiement = !reader.IsDBNull(16) ? reader.GetString(16) : "Espèce";
                    liste.Add(doc);
                }
                return liste;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste", ex);
                return null;
            }
            finally
            {
                _connection.Close();
            }
        }

        //enregistrer les bons
        public static bool EnregistrerUnDocument(Document document, string etat)
        {
            var flag = false;
            try
            {
                _connection.Open();
                var requete = "SELECT * FROM doc_tbl WHERE ref = @reff";
                _command.CommandText = requete;
                _command.Parameters.Add(new MySqlParameter("reff", document.TypeDocument));
                var reader = _command.ExecuteReader();
                if (etat == "1")
                {
                    if (!reader.HasRows)
                    {
                        reader.Close();
                        requete =
                           "INSERT INTO `doc_tbl` ( `ref`,`rootPath`, `ht`, `tva`,`ttc`, `echeance_pai`,`id_fourn`, `id_type`, `date_enre`,exercice,mot_cle,echeance_liv,payable, id_doc,cat_doc,mode)" +
                           "VALUES ( @ref,@rootPath, @ht, @tva, @ttc, @echeance_pai,@id_fourn,@id_type,@date_enre,@exercice,@mot_cle,@echeance_liv,@payable,@id_doc, @cat_doc,@mode)";

                    }
                    else
                    {
                        MonMessageBox.ShowBox("La référence de ce document " + document.ReferenceDocument + " existe déja dans la base de données", "Erreur enregistrement");
                    }
                }
                else if (etat == "2")
                {
                    reader.Close();
                    requete =
                       "UPDATE `doc_tbl` SET `ref`=@ref,`rootPath`=@rootPath, `ht`=@ht, `tva`=@tva,`ttc`=@ttc,echeance_liv=@echeance_liv,payable=@payable" +
                       ",`echeance_pai`=@echeance_pai,`id_fourn`=@id_fourn,mode=@mode, `id_type`=@id_type,exercice=@exercice,mot_cle=@mot_cle WHERE no =" + document.NumeroDocument;
                      
                }
                _command.Parameters.Add(new MySqlParameter("echeance_pai", document.EcheancePaiement));
                _command.Parameters.Add(new MySqlParameter("ref", document.ReferenceDocument));
                _command.Parameters.Add(new MySqlParameter("rootPath", document.RootPathDocument));
                _command.Parameters.Add(new MySqlParameter("ht", document.MontantHT));
                _command.Parameters.Add(new MySqlParameter("tva", document.TVA));
                _command.Parameters.Add(new MySqlParameter("ttc", document.MontantTTC));
                _command.Parameters.Add(new MySqlParameter("echeance_liv", document.EcheanceLivraison));
                _command.Parameters.Add(new MySqlParameter("id_fourn", document.NumeroTiers));
                _command.Parameters.Add(new MySqlParameter("mode", document.ModalitePaiement));
                _command.Parameters.Add(new MySqlParameter("id_type", document.NumeroType));
                _command.Parameters.Add(new MySqlParameter("exercice", document.Exercice));
                _command.Parameters.Add(new MySqlParameter("mot_cle", document.MotCle));
                _command.Parameters.Add(new MySqlParameter("payable", document.Payable));
                _command.Parameters.Add(new MySqlParameter("id_doc", document.IDTypeDocument));
                _command.Parameters.Add(new MySqlParameter("date_enre", document.DateEnregistrement));
                _command.Parameters.Add(new MySqlParameter("cat_doc", document.CategorieDocument));
                _command.CommandText = requete;
                _command.ExecuteNonQuery();
                flag = true;
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("L'enregistrement a échoué veuillez verifier les parametres et réessayer",
                    "Erreur enregistrement", exception);
                flag = false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
            return flag;
        }

        public static bool TransformerUnDocument(Document document, int idType, string typeDocument,int id)
        {
            var flag = false;
            try
            {
                _connection.Open();
                _transaction = _connection.BeginTransaction();
                var requete = "SELECT * FROM doc_tbl WHERE ref = @reff";
                _command.CommandText = requete;
                _command.Transaction = _transaction;
                _command.Parameters.Add(new MySqlParameter("reff", typeDocument));
                var reader = _command.ExecuteReader();
                
                    if (!reader.HasRows)
                    {
                        reader.Close();
                        requete =
                           "INSERT INTO `doc_tbl` ( `ref`,`rootPath`, `ht`, `tva`,`ttc`, `echeance_pai`,`id_fourn`, `id_type`, `date_enre`,exercice,mot_cle,echeance_liv,payable, id_doc,cat_doc,mode)" +
                           "VALUES ( @ref,@rootPath, @ht, @tva, @ttc, @echeance_pai,@id_fourn,@id_type,@date_enre,@exercice,@mot_cle,@echeance_liv,@payable,@id_doc, @cat_doc,@mode)";

                    }
                    else
                    {
                        MonMessageBox.ShowBox("La référence de ce document " + document.ReferenceDocument + " existe déja dans la base de données", "Erreur enregistrement");
                    }
                _command.Parameters.Add(new MySqlParameter("echeance_pai", document.EcheancePaiement));
                _command.Parameters.Add(new MySqlParameter("ref", document.ReferenceDocument));
                _command.Parameters.Add(new MySqlParameter("rootPath", document.RootPathDocument));
                _command.Parameters.Add(new MySqlParameter("ht", document.MontantHT));
                _command.Parameters.Add(new MySqlParameter("tva", document.TVA));
                _command.Parameters.Add(new MySqlParameter("ttc", document.MontantTTC));
                _command.Parameters.Add(new MySqlParameter("echeance_liv", document.EcheanceLivraison));
                _command.Parameters.Add(new MySqlParameter("id_fourn", document.NumeroTiers));
                _command.Parameters.Add(new MySqlParameter("mode", document.ModalitePaiement));
                _command.Parameters.Add(new MySqlParameter("id_type", idType));
                _command.Parameters.Add(new MySqlParameter("exercice", document.Exercice));
                _command.Parameters.Add(new MySqlParameter("mot_cle", document.MotCle));
                _command.Parameters.Add(new MySqlParameter("payable", document.Payable));
                _command.Parameters.Add(new MySqlParameter("id_doc", id ));
                _command.Parameters.Add(new MySqlParameter("date_enre", document.DateEnregistrement));
                _command.Parameters.Add(new MySqlParameter("cat_doc", document.CategorieDocument));
                _command.CommandText = requete;
                _command.Transaction = _transaction;
                _command.ExecuteNonQuery();

                //requete = "SELECT MAX(id_doc) FROM doc_tbl WHERE WHERE type=@type";
                //_command.Parameters.Add(new MySqlParameter("type", typeDocument));
                //_command.Transaction = _transaction;
                //_command.CommandText = requete;
                //var idFacture = (int)_command.ExecuteScalar();

                 requete = "SELECT * FROM det_facture_tbl WHERE id_doc = " + document.IDTypeDocument +" AND type=@type";

                _command.CommandText = requete;
                _command.Parameters.Add(new MySqlParameter("type", document.TypeDocument));
                reader = _command.ExecuteReader();
                var dt = new DataTable(); dt.Load(reader); reader.Close();
               for(var i=0; i< dt.Rows.Count;i++)
                {
                   
                    var facture = new Document();
                    facture.NumeroFacture =Convert.ToInt32( dt.Rows[i].ItemArray[0].ToString());;
                    facture.IDTypeDocument = Convert.ToInt32(dt.Rows[i].ItemArray[1].ToString());
                    facture.Designation = dt.Rows[i].ItemArray[2].ToString();
                    facture.Quantite =Convert.ToInt32( dt.Rows[i].ItemArray[3].ToString());
                    facture.PrixUnitaire = Convert.ToDouble(dt.Rows[i].ItemArray[4].ToString());
                    facture.PrixTotal = Convert.ToDouble(dt.Rows[i].ItemArray[5].ToString());

                    requete = "INSERT INTO det_facture_tbl (id_doc, designation,qte,prix,prix_total,type) VALUES(@id_doc2, @designation2,@qte2,@prix2,@prix_total2,@type2)";
                    _command.Parameters.Add(new MySqlParameter("id_doc2", id));
                    _command.Parameters.Add(new MySqlParameter("designation2", facture.Designation));
                    _command.Parameters.Add(new MySqlParameter("prix2", facture.PrixUnitaire));
                    _command.Parameters.Add(new MySqlParameter("prix_total2", facture.PrixTotal));
                    _command.Parameters.Add(new MySqlParameter("qte2", facture.Quantite));
                    _command.Parameters.Add(new MySqlParameter("type2", typeDocument));
                    _command.CommandText = requete;
                    _command.Transaction = _transaction;
                    _command.ExecuteNonQuery();
                    _command.Parameters.Clear();

                    requete = "SELECT * FROM sous_rb_tbl WHERE id_det="+facture.NumeroFacture;
                    _command.CommandText = requete;
                    _command.Transaction = _transaction;
                    reader = _command.ExecuteReader();
                    var dt2 = new DataTable();
                    dt2.Load(reader);
                    reader.Close();

                    requete = "SELECT MAX(id) FROM det_facture_tbl ";
                    _command.Parameters.Add(new MySqlParameter("type", typeDocument));
                    _command.Transaction = _transaction;
                    _command.CommandText = requete;
                    var idFacture = (int)_command.ExecuteScalar();
                    for (var j = 0; j < dt2.Rows.Count; j++)
                    {
                        var rubriques = dt2.Rows[j].ItemArray[2].ToString();
                        requete = "INSERT INTO sous_rb_tbl (id_det, rubrique) VALUES(" + idFacture + ", @designation)";
                        _command.Parameters.Add(new MySqlParameter("designation", rubriques));
                        _command.Transaction = _transaction;
                        _command.CommandText = requete;
                        _command.ExecuteNonQuery();
                        _command.Parameters.Clear();
                    }
                }
                _transaction.Commit();
                flag = true;
            }
            catch (Exception exception)
            {
                if (_transaction != null)
                    _transaction.Rollback();
                MonMessageBox.ShowBox("L'enregistrement a échoué veuillez verifier les parametres et réessayer",
                    "Erreur enregistrement", exception);
                flag = false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
            return flag;
        }
        //inserer details de factures
        public static bool InsererDetailsDesDocuments(Document doc, DataGridView dgvDetails, int etat, int rowIndex)
        {
            try
            { 
                var elGR = System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR");
                _connection.Open();
                _transaction = _connection.BeginTransaction();
                var requete = "";
                if (etat == 1)
                {

                    requete = "INSERT INTO det_facture_tbl (id_doc, designation,qte,prix,prix_total,type) VALUES(@id_doc, @designation,@qte,@prix,@prix_total,@type)";
                    _command.Parameters.Add(new MySqlParameter("id_doc", doc.IDTypeDocument));
                    _command.Parameters.Add(new MySqlParameter("designation", doc.Designation));
                    _command.Parameters.Add(new MySqlParameter("prix", doc.PrixUnitaire));
                    _command.Parameters.Add(new MySqlParameter("prix_total", doc.PrixTotal));
                    _command.Parameters.Add(new MySqlParameter("qte", doc.Quantite));
                    _command.Parameters.Add(new MySqlParameter("type", doc.TypeDocument));
                    _command.CommandText = requete;
                    _command.Transaction = _transaction;
                    _command.ExecuteNonQuery();

                    requete = "SELECT MAX(id) FROM det_facture_tbl  ";
                    _command.CommandText = requete;
                    doc.NumeroFacture = (int)_command.ExecuteScalar();
                    dgvDetails.Rows.Add(
                           doc.NumeroFacture,
                           doc.Designation,
                           String.Format(elGR, "{0:0,0}", doc.Quantite),
                           String.Format(elGR, "{0:0,0}", doc.PrixUnitaire),
                           String.Format(elGR, "{0:0,0}", doc.PrixTotal)
                           );
                }
                else if (etat == 2)
                {

                    doc.NumeroFacture = Int32.Parse(dgvDetails.Rows[rowIndex].Cells[0].Value.ToString());

                    requete = "UPDATE det_facture_tbl SET designation=@designation,qte=@qte,prix=@prix,prix_total=@prix_total,type=@type WHERE id=" + doc.NumeroFacture;
                    _command.Parameters.Add(new MySqlParameter("id_doc", doc.IDTypeDocument));
                    _command.Parameters.Add(new MySqlParameter("designation", doc.Designation));
                    _command.Parameters.Add(new MySqlParameter("prix", doc.PrixUnitaire));
                    _command.Parameters.Add(new MySqlParameter("prix_total", doc.PrixTotal));
                    _command.Parameters.Add(new MySqlParameter("qte", doc.Quantite));
                    _command.Parameters.Add(new MySqlParameter("type", doc.TypeDocument));

                    _command.CommandText = requete;
                    _command.Transaction = _transaction;
                    _command.ExecuteNonQuery();

                    dgvDetails.Rows[rowIndex].Cells[1].Value = doc.Designation;
                    dgvDetails.Rows[rowIndex].Cells[2].Value = String.Format(elGR, "{0:0,0}", doc.Quantite);
                    dgvDetails.Rows[rowIndex].Cells[3].Value = String.Format(elGR, "{0:0,0}", doc.PrixUnitaire);
                    dgvDetails.Rows[rowIndex].Cells[4].Value = String.Format(elGR, "{0:0,0}", doc.PrixTotal);
                }
                 requete = "UPDATE doc_tbl SET ht=@ht,tva=@tva,ttc=@ttc WHERE no=" + doc.NumeroDocument;
               _command.Parameters.Add(new MySqlParameter("ht", doc.MontantHT));
               _command.Parameters.Add(new MySqlParameter("tva", doc.TVA));
               _command.Parameters.Add(new MySqlParameter("ttc", doc.MontantTTC));
               _command.CommandText = requete;
               _command.Transaction = _transaction;
               _command.ExecuteNonQuery();
               _transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                if (_transaction != null)
                    _transaction.Rollback();
                MonMessageBox.ShowBox("", ex);
                return false;
            }
            finally
            {
                _command.Parameters.Clear();
                _connection.Close();
            }
        }

        public static bool InsererDetailsSousRubriques(Document doc, bool etat)
        {
            try
            {
                _connection.Open();
                string requete;
                if (etat)
                {
                    requete = "UPDATE sous_rb_tbl SET rubrique=@designation WHERE id=" + doc.NumeroRubrique;
                }
                else
                {
                    requete = "INSERT INTO sous_rb_tbl (id_det, rubrique) VALUES(" + doc.NumeroFacture + ", @designation)";
                }
                    _command.Parameters.Add(new MySqlParameter("designation", doc.Designation));
                    _command.CommandText = requete;
                    _command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("", ex);
                return false;
            }
            finally
            {
                _command.Parameters.Clear();
                _connection.Close();
            }
        }

        public static bool SupprimerDetailsSousRubriques(int id)
        {
            try
            {
                _connection.Open();
               
                   var  requete = "DELETE FROM sous_rb_tbl WHERE id="+id;
               
                _command.CommandText = requete;
                _command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("", ex);
                return false;
            }
            finally
            {
                _command.Parameters.Clear();
                _connection.Close();
            }
        }
     
        public static int NumeroRubrique()
        {
            try
            {
                var elGR = System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR");
                _connection.Open();
                var requete = "SELECT MAX(id) from sous_rb_tbl ";
                _command.CommandText = requete;       
                return (int)_command.ExecuteScalar();  
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("", ex);
                return 0;
            }
            finally
            {
                _connection.Close();
            }
        }

        //liste des detauls des factures
        public static List<Document> ListeDetailsFacures(int idFacture, string type)
        {
            try
            {
                var liste = new List<Document>();
                var requete = "SELECT * FROM det_facture_tbl WHERE id_doc = " + idFacture +" AND type=@type";
                _connection.Open();
                _command.CommandText = requete;
                _command.Parameters.Add(new MySqlParameter("type", type));
                var reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var facture = new Document();
                    facture.NumeroFacture = reader.GetInt32(0);
                    facture.IDTypeDocument = reader.GetInt32(1);
                    facture.Designation = reader.GetString(2);
                    facture.Quantite = reader.GetDouble(3);
                    facture.PrixUnitaire = reader.GetDouble(4);
                    facture.PrixTotal = reader.GetDouble(5);
                    facture.TypeDocument = reader.GetString(6);
                    liste.Add(facture);
                }
                return liste;

            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("", ex);
                return null;
            }
            finally
            {
                _command.Parameters.Clear();
                _connection.Close();
            }
        }

        public static List<Document> ListeDetailsRubriquesFacures()
        {
            try
            {
                var liste = new List<Document>();
                var requete = "SELECT * FROM sous_rb_tbl";
                _connection.Open();
                _command.CommandText = requete;
                var reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var facture = new Document();
                    facture.NumeroRubrique = reader.GetInt32(0);
                    facture.NumeroFacture = reader.GetInt32(1);
                    facture.Designation = reader.GetString(2);
                    liste.Add(facture);
                }
                return liste;

            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("", ex);
                return null;
            }
            finally
            {
                _connection.Close();
            }
        }
        //retirer un element dans une facture
        public static bool RetirerUnElementDansUneFacture(Document facture)
        {
            try
            {
                _connection.Open();
                _transaction = _connection.BeginTransaction();
                var requete = "DELETE FROM det_facture_tbl WHERE id = " + facture.NumeroFacture;
                _command.Transaction = _transaction;
                _command.CommandText = requete;
                _command.ExecuteNonQuery();

                requete = "UPDATE doc_tbl SET ht= ht - @ht ,ttc=ttc - @ttc WHERE no=" + facture.NumeroDocument;
                _command.Parameters.Add(new MySqlParameter("ht", facture.MontantHT));
                _command.Parameters.Add(new MySqlParameter("ttc", facture.MontantTTC));
                _command.CommandText = requete;
                _command.Transaction = _transaction;
                _command.ExecuteNonQuery();
                _transaction.Commit();
                return true;
            }
            catch(Exception ex)
            {
                if (_transaction != null)
                    _transaction.Rollback();
                MonMessageBox.ShowBox("", ex);
                return false;
            }
            finally
            {
                _command.Parameters.Clear();
                _connection.Close();
            }
        }
        //mettre  a jour la tva
        public static bool MettreAjourLaTVA(Document facture)
        {
            try
            {
                _connection.Open();
                var requete = "UPDATE doc_tbl SET tva= @tva  ,ttc= @ttc WHERE no=" + facture.NumeroDocument;
                _command.Parameters.Add(new MySqlParameter("tva", facture.TVA));
                _command.Parameters.Add(new MySqlParameter("ttc", facture.MontantTTC));
                _command.CommandText = requete;
                _command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("", ex);
                return false;
            }
            finally
            {
                _command.Parameters.Clear();
                _connection.Close();
            }
        }
       
        //enregistrer les donnees de les bons 
        public static bool SupprimerUnTypeDocument(int no)
        {
            var flag = false;
            try
            {
                if (MonMessageBox.ShowBox("Voulez vous supprimer les données de le document numéro " + no + "?", "Confirmation") == "1")
                {
                    _connection.Open();
                    var requete = "DELETE FROM `type_doc_tbl`  WHERE id = " + no;
                    _command.CommandText = requete;
                    _command.ExecuteNonQuery();
                    MonMessageBox.ShowBox("Les données supprimées avec succés",
                        "Information enregistrement");
                    flag = true;
                }
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("La modification a échoué veuillez verifier les parametres et réessayer",
                    "Erreur enregistrement", exception);
                flag = false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
            return flag;
        }

        public static List<Document> ListeDesTypesDocuments()
        {
            var liste = new List<Document>();
            try
            {
                var requete = "SELECT * FROM type_doc_tbl ORDER BY type_doc";
                _command.CommandText = requete;
                _connection.Open();
                var reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var doc = new Document();
                    doc.NumeroType = reader.GetInt32(0);
                    doc.TypeDocument = reader.GetString(1);
                    liste.Add(doc);
                }
                return liste;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste", ex);
                return null;
            }
            finally
            {
                _connection.Close();
            }
        }

        //enregistrer les bons
        public static bool EnregistrerUnTypeDocument(Document document, string etat)
        {
            var flag = false;
            try
            {
                _connection.Open();
                var requete = "SELECT * FROM type_doc_tbl WHERE type_doc = @type" ;
                _command.CommandText = requete;
                _command.Parameters.Add(new MySqlParameter("type", document.TypeDocument));
                var reader = _command.ExecuteReader();
                if (etat == "1")
                {
                    if (!reader.HasRows)
                    {
                        reader.Close();
                        requete =
                           "INSERT INTO `type_doc_tbl` ( `type_doc`) VALUES ( @type_doc)";
                    }
                    else
                    {
                        MonMessageBox.ShowBox("Le type de ce document " + document.TypeDocument + " existe déja dans la base de données", "Erreur enregistrement");
                    }
                }
                else if (etat == "2")
                {
                    reader.Close();
                    requete =
                       "UPDATE `type_doc_tbl` SET type_doc=@type_doc WHERE id="+document.NumeroType;

                }
                _command.Parameters.Add(new MySqlParameter("type_doc", document.TypeDocument));
                _command.CommandText = requete;
                _command.ExecuteNonQuery();
                flag = true;
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("L'enregistrement a échoué veuillez verifier les parametres et réessayer",
                    "Erreur enregistrement", exception);
                flag = false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
            return flag;
        }

        public static bool AjouterUnApprobateur(Document document)
        {
            var flag = false;
            try
            {
                _connection.Open();
                       var  requete =
                           "INSERT INTO `approv_tbl` ( `idDoc`,`num_mat`, `etat`)" +
                           "VALUES ( @idDoc,@num_mat, @etat)";
                       _command.Parameters.Add(new MySqlParameter("idDoc", document.NumeroDocument));
                       _command.Parameters.Add(new MySqlParameter("num_mat", document.Matricule));
                       _command.Parameters.Add(new MySqlParameter("etat", document.Approbation));
                _command.CommandText = requete;
                _command.ExecuteNonQuery();
                flag = true;
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("L'enregistrement a échoué veuillez verifier les parametres et réessayer",
                    "Erreur enregistrement", exception);
                flag = false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
            return flag;
        }

        public static bool DonnerUneApprobation(Document document)
        {
            var flag = false;
            try
            {
                if(MonMessageBox.ShowBox("Voulez vous mettre à jour la validation pour ce document?", "Confirmation")=="1")
                {
                _connection.Open();
                var requete =
                    "UPDATE `approv_tbl` SET `etat`="+document.Approbation +" WHERE id = "+ document.NumeroType;
                _command.CommandText = requete;
                _command.ExecuteNonQuery();
                flag = true;
                    }
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("L'enregistrement a échoué veuillez verifier les parametres et réessayer",
                    "Erreur enregistrement", exception);
                flag = false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
            return flag;
        }
       
        public static bool SupprimerUneApprobation(int no)
        {
            var flag = false;
            try
            {
                if (MonMessageBox.ShowBox("Voulez vous supprimer ces données  " + no + "?", "Confirmation") == "1")
                {
                    _connection.Open();
                    var requete = "DELETE FROM `approv_tbl`  WHERE id = " + no;
                    _command.CommandText = requete;
                    _command.ExecuteNonQuery();
                    MonMessageBox.ShowBox("Les données supprimées avec succés",
                        "Information enregistrement");
                    flag = true;
                }
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("La modification a échoué veuillez verifier les parametres et réessayer",
                    "Erreur enregistrement", exception);
                flag = false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
            return flag;
        }

        public static List<Document> ListeDesApprobations(int numeroDocument)
        {
            var liste = new List<Document>();
            try
            {
                var requete = "SELECT * FROM approv_tbl WHERE idDoc = "+ numeroDocument;
                _command.CommandText = requete;
                _connection.Open();
                var reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var doc = new Document();
                    doc.NumeroType = reader.GetInt32(0);
                    doc.NumeroDocument = reader.GetInt32(1);
                    doc.Matricule = reader.GetString(2);
                    doc.Approbation = reader.GetInt32(3);
                    liste.Add(doc);
                }
                return liste;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste", ex);
                return null;
            }
            finally
            {
                _connection.Close();
            }
        }
    
        public static int DernierApprobation()
        {
            try
            {
                var requete = "SELECT MAX(id) FROM approv_tbl ";
                _command.CommandText = requete;
                _connection.Open();
                return (int) _command.ExecuteScalar();
               
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste", ex);
                return 0;
            }
            finally
            {
                _connection.Close();
            }
        }

        #endregion
        //enregistrer les donnees de les bons 
     
        #region CODE_ACOMPTE

        //anregistrer un accompte
        public static bool EnregistrerUnAccompte(Acompte acompte, bool etat)
        {
            var flag = false;
            try
            {
                _connection.Open();
                if (etat)
                {
                    var requete = "INSERT INTO accompte_tbl (num_mat, accompte, rembourse,a_payer,date_acp, mode) " +
                        " VALUES(@num_mat," + acompte.MontantAcompte + "," + acompte.Rembourser + "," + acompte.Deduction + ",@date,@mode)";
                    _command.CommandText = requete;
                    _command.Parameters.Add(new MySqlParameter("date", acompte.DateAcompte));
                    _command.Parameters.Add(new MySqlParameter("num_mat", acompte.NumeroMatricule));
                    _command.Parameters.Add(new MySqlParameter("mode", acompte.ModePaiement));
                    _command.ExecuteNonQuery();
                    MonMessageBox.ShowBox("Les données  enregistrées avec succés",
                           "Information enregistrement");
                    flag = true;
                }
                else
                {
                    var requete = "UPDATE accompte_tbl SET  accompte = " + acompte.MontantAcompte + ", rembourse = " + acompte.Rembourser +
              ",a_payer = " + acompte.Deduction + ", date_acp = @date, num_mat =@num_mat, mode=@mode WHERE id = " + acompte.NumeroAcompte;
                    _command.CommandText = requete;

                    _command.Parameters.Add(new MySqlParameter("date", acompte.DateAcompte));
                    _command.Parameters.Add(new MySqlParameter("num_mat", acompte.NumeroMatricule));
                    _command.Parameters.Add(new MySqlParameter("mode", acompte.ModePaiement));
                    _command.ExecuteNonQuery();
                    MonMessageBox.ShowBox("Les données  modifiées avec succés",
                           "Information enregistrement");
                    flag = true;
                }
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("L'enregistrement a échoué veuillez verifier les parametres et réessayer",
                    "Erreur enregistrement", exception);
                flag = false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
            return flag;
        }

        public static List<Acompte> ListeDesAccompte(string nomEmploye)
        {
            var liste = new List<Acompte>();
            try
            {
                var requete = "SELECT accompte_tbl.* , pers_tbl.nom, pers_tbl.prenom FROM accompte_tbl INNER JOIN pers_tbl" +
                " ON accompte_tbl.num_mat = pers_tbl.num_mat  WHERE pers_tbl.nom LIKE '%" + nomEmploye + "%'";
                _command.CommandText = requete;
                _connection.Open();
                var reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var acompte = new Acompte();
                    acompte.NumeroAcompte = reader.GetInt32(0);
                    acompte.NumeroMatricule = reader.GetString(1);
                    acompte.MontantAcompte = reader.GetDouble(2);
                    acompte.Rembourser = reader.GetDouble(3);
                    acompte.Deduction = reader.GetDouble(4);
                    acompte.DateAcompte = reader.GetDateTime(5);
                    acompte.ModePaiement = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    acompte.NomEmploye = reader.GetString(7) + " " + reader.GetString(8);
                    liste.Add(acompte);
                }
                return liste;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste", ex);
                return null;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }
        public static List<Acompte> ListeDesAccompte(int  id, string numMatricule,double montant)
        {
            var liste = new List<Acompte>();
            try
            {
                var requete = "SELECT accompte_tbl.* , pers_tbl.nom, pers_tbl.prenom FROM accompte_tbl INNER JOIN pers_tbl" +
                " ON accompte_tbl.num_mat = pers_tbl.num_mat  WHERE pers_tbl.num_mat=@num AND accompte_tbl.id =" + id;
                _command.CommandText = requete;
                _command.Parameters.Add(new MySqlParameter("num", numMatricule));
                _connection.Open();
                var reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var acompte = new Acompte();
                    acompte.NumeroAcompte = reader.GetInt32(0);
                    acompte.NumeroMatricule = reader.GetString(1);
                    acompte.MontantAcompte = reader.GetDouble(2);
                    acompte.Rembourser = reader.GetDouble(3);
                    acompte.Deduction = reader.GetDouble(4);
                    acompte.DateAcompte = reader.GetDateTime(5);
                    acompte.ModePaiement = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    acompte.NomEmploye = reader.GetString(7) + " " + reader.GetString(8);
                    liste.Add(acompte);
                }
                return liste;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste", ex);
                return null;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }

        public static bool SupprimerUnAccompte(int id)
        {
            var flag = false;
            try
            {
                _connection.Open();
                var requete = "DELETE FROM accompte_tbl  WHERE id = " + id;
                _command.CommandText = requete;
                _command.ExecuteNonQuery();
                MonMessageBox.ShowBox("Les données  supprimées avec succés",
                       "Information enregistrement");
                flag = true;

            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("L'enregistrement a échoué veuillez verifier les parametres et réessayer",
                    "Erreur enregistrement", exception);
                flag = false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
            return flag;
        }
        
        #endregion

        #region Depenses
        //enregistrer une depenses
        public static bool EnregistrerUnUneCategorie(Depenses depense )
        {
            try
            {
                _connection.Open();
                    if (depense.IDCategorie > 0)
                    {
                        var requete = "UPDATE catedeprec_tbl  SET cat =@cat,code=@code  WHERE id = " + depense.IDCategorie;
                        _command.Parameters.Add(new MySqlParameter("cat", depense.Categorie));
                    _command.Parameters.Add(new MySqlParameter("code", depense.Code));
                    _command.CommandText = requete;
                        _command.ExecuteNonQuery();
                        _command.Parameters.Clear();
                    }
                    else
                    {
                        string requete = "INSERT INTO catedeprec_tbl (cat , etat,code) VALUES(@cat, @etat,@code)";
                        _command.CommandText = requete;
                        _command.Parameters.Add(new MySqlParameter("cat", depense.Categorie));
                        _command.Parameters.Add(new MySqlParameter("etat", depense.Etat));
                        _command.Parameters.Add(new MySqlParameter("code", depense.Code));
                        _command.ExecuteNonQuery();
                        _command.Parameters.Clear();
                    }
                
                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L'enregistrement des données a échoué", "Erreur", ex);
                return false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }

        public static bool OrdonneeCategorie(DataGridView dataGridView)
        {
            try
            {
                _connection.Open();
                Depenses depense = new Depenses ();
                depense.ORDRE = Convert.ToInt32(dataGridView.CurrentRow.Cells[3].Value.ToString());
                depense.IDCategorie = Convert.ToInt32(dataGridView.CurrentRow.Cells[0].Value.ToString());
                var requete = "UPDATE ordre=@ordre  WHERE id = " + depense.IDCategorie;
                    _command.Parameters.Add(new MySqlParameter("ordre", depense.ORDRE));
                    _command.CommandText = requete;
                    _command.ExecuteNonQuery();
                    _command.Parameters.Clear();

                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L'enregistrement des données a échoué", ex);
                return false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }

        public static bool EnregistrerUnLibelle(Depenses depense)
        {
            try
            {
                _connection.Open();
               

                    if (depense.IDLibelle > 0)
                    {
                        var requete = "UPDATE libelle_tbl  SET id_cat =@id_cat ,code=@code, libelle=@libelle WHERE id = " + depense.IDLibelle;
                        _command.Parameters.Add(new MySqlParameter("libelle", depense.Libelle));
                    _command.Parameters.Add(new MySqlParameter("id_cat", depense.IDCategorie));
                    _command.Parameters.Add(new MySqlParameter("code", depense.Code));
                    _command.CommandText = requete;
                        _command.ExecuteNonQuery();
                        _command.Parameters.Clear();
                    }
                    else
                    {
                        string requete = "INSERT INTO libelle_tbl (id_cat , libelle,code,etat) VALUES(@id_cat, @libelle,@code,@etat)";
                        _command.CommandText = requete;
                        _command.Parameters.Add(new MySqlParameter("id_cat", depense.IDCategorie));
                        _command.Parameters.Add(new MySqlParameter("etat", depense.Etat));
                    _command.Parameters.Add(new MySqlParameter("libelle", depense.Libelle));
                    _command.Parameters.Add(new MySqlParameter("code", depense.Code));
                    _command.ExecuteNonQuery();
                        _command.Parameters.Clear();
                    }
                
                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L'enregistrement des données a échoué", "Erreur", ex);
                return false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }

        //enregistrer une depenses
        public static bool SuprimerUneCategorie(int id)
        {
            try
            {
                _connection.Open();

                if (MonMessageBox.ShowBox(" Voulez vous supprimer ces données", "Confirmation") == "1")
                {
                    var requete = "DELETE FROM catedeprec_tbl  WHERE id = " + id;
                    _command.CommandText = requete;
                    _command.ExecuteNonQuery();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L'enregistrement des données a échoué", "Erreur", ex);
                return false;
            }
            finally
            {
                _connection.Close();
            }
        }

        public static bool SuprimerUnLibelle(int id)
        {
            try
            {
                _connection.Open();

                if (MonMessageBox.ShowBox(" Voulez vous supprimer ces données", "Confirmation") == "1")
                {
                    var requete = "DELETE FROM libelle_tbl  WHERE id = " + id;
                    _command.CommandText = requete;
                    _command.ExecuteNonQuery();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L'enregistrement des données a échoué", "Erreur", ex);
                return false;
            }
            finally
            {
                _connection.Close();
            }
        }

        public static List<Depenses> ListeCategorie(int etat)
        {
            try
            {
                var liste = new List<Depenses>();
                _connection.Open();
                var requete = "SELECT * FROM catedeprec_tbl WHERE etat= "+ etat+" ORDER BY cat";
                _command.CommandText = requete;
               var reader= _command.ExecuteReader();
               while (reader.Read())
               {
                   var depense = new Depenses();
                   depense.IDCategorie = reader.GetInt32(0);
                   depense.Categorie = reader.GetString(1);
                    depense.Etat = reader.GetInt32(2);
                    depense.Code = reader.GetString(3);
                    //depense.ORDRE = !reader.IsDBNull(4) ? reader.GetInt32(4) : 100;
                    liste.Add(depense);
               }
               return liste;
            }
            catch (Exception exception){ MonMessageBox.ShowBox("", exception); return null; }
            finally { _connection.Close(); }
        }


        public static List<Depenses> ListeCategorieOrdonneeParCode(int etat)
        {
            try
            {
                var liste = new List<Depenses>();
                _connection.Open();
                var requete = "SELECT * FROM catedeprec_tbl WHERE etat= " + etat + " ORDER BY cast(code AS unsigned)";
                _command.CommandText = requete;
                var reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var depense = new Depenses();
                    depense.IDCategorie = reader.GetInt32(0);
                    depense.Categorie = reader.GetString(1);
                    depense.Etat = reader.GetInt32(2);
                    depense.Code = reader.GetString(3);
                    liste.Add(depense);
                }
                return liste;
            }
            catch (Exception exception) { MonMessageBox.ShowBox("", exception); return null; }
            finally { _connection.Close(); }
        }

        public static List<Depenses> ListeLibelle(int etat)
        {
            try
            {
                var liste = new List<Depenses>();
                _connection.Open();
                var requete = "SELECT * FROM libelle_tbl WHERE etat= " + etat + " ORDER BY libelle";
                _command.CommandText = requete;
                var reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var depense = new Depenses();
                    depense.IDLibelle = reader.GetInt32(0);
                    depense.IDCategorie = reader.GetInt32(1);
                    depense.Code = reader.GetString(2);
                    depense.Libelle = reader.GetString(3);
                    depense.Etat = reader.GetInt32(4);
                    liste.Add(depense);
                }
                return liste;
            }
            catch (Exception exception) { MonMessageBox.ShowBox("", exception); return null; }
            finally { _connection.Close(); }
        }

        public static List<Depenses> ListeLibelleOrdonneParCode(int etat)
        {
            try
            {
                var liste = new List<Depenses>();
                _connection.Open();
                var requete = "SELECT * FROM libelle_tbl WHERE etat= " + etat + " ORDER BY cast(code AS unsigned)";
                _command.CommandText = requete;
                var reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var depense = new Depenses();
                    depense.IDLibelle = reader.GetInt32(0);
                    depense.IDCategorie = reader.GetInt32(1);
                    depense.Code = reader.GetString(2);
                    depense.Libelle = reader.GetString(3);
                    depense.Etat = reader.GetInt32(4);
                    liste.Add(depense);
                }
                return liste;
            }
            catch (Exception exception) { MonMessageBox.ShowBox("", exception); return null; }
            finally { _connection.Close(); }
        }

        public static List<Depenses> EtatDepense(int annee, string mois,int  idLib)
        {
            try
            {
                var liste = new List<Depenses>();
                _connection.Open();
                var requete = "SELECT SUM(montant) FROM det_dep GROUP BY idLib, mois, annee HAVING(idLib = @idLib) AND(mois = @mois) AND(annee = @annee)";
                _command.CommandText = requete;
                _command.Parameters.Add(new MySqlParameter("idLib", idLib));
                _command.Parameters.Add(new MySqlParameter("mois", mois));
                _command.Parameters.Add(new MySqlParameter("annee", annee ));
                var reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var depense = new Depenses();
                    depense.Montant = reader.GetDouble(0);
                    liste.Add(depense);
                }
                return liste;
            }
            catch (Exception exception) {
                MonMessageBox.ShowBox("", exception); return null; }
            finally {
                _command.Parameters.Clear(); _connection.Close(); }
        }
        public static List<Depenses> EtatDepenseParCategorie(int annee, string mois,string categorie)
        {
            try
            {
                var liste = new List<Depenses>();
                _connection.Open();
                var requete = "SELECT SUM(det_dep.montant) FROM det_dep INNER JOIN  libelle_tbl ON det_dep.idLib = libelle_tbl.id INNER JOIN"+
                                "   catedeprec_tbl ON libelle_tbl.id_cat = catedeprec_tbl.id GROUP BY det_dep.mois, det_dep.annee, catedeprec_tbl.cat "+
                                " HAVING (det_dep.mois = @mois) AND (det_dep.annee = @annee) AND (catedeprec_tbl.cat = @cat)";
                _command.CommandText = requete;
                _command.Parameters.Add(new MySqlParameter("mois", mois));
                _command.Parameters.Add(new MySqlParameter("annee", annee));
                _command.Parameters.Add( new MySqlParameter("cat", categorie ));
                var reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var depense = new Depenses();
                    depense.Montant = reader.GetDouble(0);
                    liste.Add(depense);
                }
                return liste;
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("", exception); return null;
            }
            finally
            {
                _command.Parameters.Clear(); _connection.Close();
            }
        }
        public static List<Depenses> EtatDepenseParMois(int annee, string mois)
        {
            try
            {
                var liste = new List<Depenses>();
                _connection.Open();
                var requete = "SELECT SUM(det_dep.montant) FROM det_dep INNER JOIN  libelle_tbl ON det_dep.idLib = libelle_tbl.id " +
                                "   GROUP BY det_dep.mois, det_dep.annee  HAVING (det_dep.mois = @mois) AND (det_dep.annee = @annee) ";
                _command.CommandText = requete;
                _command.Parameters.Add(new MySqlParameter("mois", mois));
                _command.Parameters.Add(new MySqlParameter("annee", annee));
                var reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var depense = new Depenses();
                    depense.Montant = reader.GetDouble(0);
                    liste.Add(depense);
                }
                return liste;
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("", exception); return null;
            }
            finally
            {
                _command.Parameters.Clear(); _connection.Close();
            }
        }


        public static List<Reglement> EtatRecette(int annee, string mois, int idLib)
        {
            try
            {
                var liste = new List<Reglement>();
                _connection.Open();
                var requete = "SELECT SUM(montant) FROM recette_tbl GROUP BY id_libelle, mois, annee HAVING(id_libelle = @id_libelle) AND(mois = @mois) AND(annee = @annee)";
                _command.CommandText = requete;
                _command.Parameters.Add(new MySqlParameter("id_libelle", idLib));
                _command.Parameters.Add(new MySqlParameter("mois", mois));
                _command.Parameters.Add(new MySqlParameter("annee", annee));
                var reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var depense = new Reglement();
                    depense.MontantPaiement = reader.GetDouble(0);
                    liste.Add(depense);
                }
                return liste;
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("", exception); return null;
            }
            finally
            {
                _command.Parameters.Clear(); _connection.Close();
            }
        }
        public static List<Reglement> EtatRecetteParCategorie(int annee, string mois, string categorie)
        {
            try
            {
                var liste = new List<Reglement>();
                _connection.Open();
                var requete = "SELECT SUM(recette_tbl.montant) FROM recette_tbl INNER JOIN  libelle_tbl ON recette_tbl.id_libelle = libelle_tbl.id INNER JOIN" +
                                "   catedeprec_tbl ON libelle_tbl.id_cat = catedeprec_tbl.id GROUP BY recette_tbl.mois, recette_tbl.annee, catedeprec_tbl.cat " +
                                " HAVING (recette_tbl.mois = @mois) AND (recette_tbl.annee = @annee) AND (catedeprec_tbl.cat = @cat)";
                _command.CommandText = requete;
                _command.Parameters.Add(new MySqlParameter("mois", mois));
                _command.Parameters.Add(new MySqlParameter("annee", annee));
                _command.Parameters.Add(new MySqlParameter("cat", categorie ));
                var reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var depense = new Reglement();
                    depense.MontantPaiement = reader.GetDouble(0);
                    liste.Add(depense);
                }
                return liste;
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("", exception); return null;
            }
            finally
            {
                _command.Parameters.Clear(); _connection.Close();
            }
        }

        public static List<Reglement> EtatRecetteParMois(int annee, string mois)
        {
            try
            {
                var liste = new List<Reglement>();
                _connection.Open();
                var requete = "SELECT SUM(recette_tbl.montant) FROM recette_tbl INNER JOIN  libelle_tbl ON recette_tbl.id_libelle = libelle_tbl.id " +
                                "   GROUP BY recette_tbl.mois, recette_tbl.annee  HAVING (recette_tbl.mois = @mois) AND (recette_tbl.annee = @annee) ";
                _command.CommandText = requete;
                _command.Parameters.Add(new MySqlParameter("mois", mois));
                _command.Parameters.Add(new MySqlParameter("annee", annee));
                var reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var depense = new Reglement();
                    depense.MontantPaiement = reader.GetDouble(0);
                    liste.Add(depense);
                }
                return liste;
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("", exception); return null;
            }
            finally
            {
                _command.Parameters.Clear(); _connection.Close();
            }
        }

        //enregistrer une depenses
        public static bool EnregistrerUneDepense(Depenses depense)
        {
            try
            {
                _connection.Open();
                var requete = "";
                if (depense.IDDepense == 0)
                {
                    requete = "INSERT INTO `det_dep`(`idLib`,mois,annee, `montant`, `benef`,`date`, no_fact) VALUES ("
                         + "@idLib,@mois,@annee, @montant,  @benef,  @date,@no_fact)";
                }
                else
                {
                    if (MonMessageBox.ShowBox("Voulez vous modifier ces données ?", "Confirmation") == "1")
                    {
                        requete = "UPDATE `det_dep` SET `idLib`=@idLib,mois=@mois,annee=@annee, " +
                            "`montant`=@montant, `benef`=@benef,`date`=  @date, no_fact=@no_fact WHERE id =" + depense.IDDepense;
                    }
                }
                _command.Parameters.Add(new MySqlParameter("date", depense.Date));
                _command.Parameters.Add(new MySqlParameter("idLib", depense.IDLibelle));
                _command.Parameters.Add(new MySqlParameter("montant", depense.Montant));
                _command.Parameters.Add(new MySqlParameter("benef", depense.Beneficiaire));
                _command.Parameters.Add(new MySqlParameter("no_fact", depense.NumeroFacture));
                _command.Parameters.Add(new MySqlParameter("mois", depense.Mois));
                _command.Parameters.Add(new MySqlParameter("annee", depense.Annee));
                _command.CommandText = requete;
                _command.ExecuteNonQuery();

                MonMessageBox.ShowBox("L'enregistrement a été éffectuée avec succés", "ajouter depenses");
                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L'enregistrement a echoue", "Ajouter depenses", ex);
                return false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }
        
        public static DataTable ListeDesDepensesGrouperParLibelle(DateTime date, DateTime date1)
        {
            DataTable dt = new DataTable();
            try
            {
                _connection.Open();
                string query = "SELECT catedep.libelle, SUM(det_dep.montant) FROM det_dep INNER JOIN personnel_db.catedep" +
                    " ON personnel_db.det_dep.id_lib=personnel_db.catedep.id WHERE " +
                    " det_dep.date >=@date AND det_dep.date <@date1 GROUP BY personnel_db.catedep.libelle ";
                _command.CommandText = query;
                _command.Parameters.Add(new MySqlParameter("date", date));
                _command.Parameters.Add(new MySqlParameter("date1", date1.AddHours(24)));
                MySqlDataReader reader = _command.ExecuteReader();
                dt.Load(reader);
            }
            catch (Exception exception) { MonMessageBox.ShowBox("Liste depenses", exception); }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
            return dt;
        }
        //enregistrer une hospitalisation
        public static bool  SupprimerUneDepense(int id)
        {
            try
            {
                _connection.Open();
                if (MonMessageBox.ShowBox("Voulez vous supprimer ces données ?", "Supprimer depense") == "1")
                {
                    string query = "DELETE FROM det_dep WHERE id =" + id;
                    _command.CommandText = query;
                    _command.ExecuteNonQuery();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("La suppression des données a echoue", "Supprimer depense", ex);
                return false;
            }
            finally
            {
                _connection.Close();
            }
        }

        //liste des depenses
        public static List<Depenses> ListeDesDepenses()
        {
            var liste = new List<Depenses>();
            try
            {
                _connection.Open();
                string query = "SELECT * FROM det_dep";
                _command.CommandText = query;
                var  reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var depense = new Depenses();
                    depense.IDDepense = reader.GetInt32(0);
                    depense.IDLibelle = reader.GetInt32(1);
                    depense.Date = reader.GetDateTime(2);
                    depense.Montant = reader.GetDouble(3);
                    depense.Beneficiaire =!reader.IsDBNull(4) ?  reader.GetString(4): "";
                    depense.NumeroFacture = !reader.IsDBNull(5) ? reader.GetString(5) : "";
                    depense.Annee  = reader.GetInt32(7);
                    depense.Mois = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    liste.Add(depense);
                }
                return liste;
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("Liste depense", exception); 
                return null ; 
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }

        public static List<Depenses> ListeDesDepensesEntreDeuxDates(DateTime date, DateTime date1)
        {
            var liste = new List<Depenses>();
            try
            {
                _connection.Open();
                string query = "SELECT * FROM depen_tbl WHERE date >= @date AND date<@date1";
                _command.CommandText = query;
                _command.Parameters.Add(new MySqlParameter("date", date));
                _command.Parameters.Add(new MySqlParameter("date1", date1.AddHours(24)));
                var reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var depense = new Depenses();
                    depense.IDDepense = reader.GetInt32(0);
                    depense.IDLibelle = reader.GetInt32(1);
                    depense.Date = reader.GetDateTime(2);
                    depense.Montant = reader.GetDouble(3);
                    depense.Beneficiaire = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    depense.NumeroFacture = !reader.IsDBNull(5) ? reader.GetString(5) : "";
                    depense.Annee = reader.GetInt32(6);
                    depense.Mois = !reader.IsDBNull(7) ? reader.GetString(7) : "";
                    liste.Add(depense);
                }
                return liste;
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("Liste depense", exception);
                return null;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }

        //liste des depense par date
        public static DataTable ListeDesDepenses(DateTime date, DateTime date1)
        {
            DataTable dt = new DataTable();
            try
            {
                _connection.Open();
                string query = "SELECT * FROM depen_tbl WHERE date >= @date AND date < @date1 ORDER BY id DESC";
                _command.CommandText = query;
                _command.Parameters.Add(new MySqlParameter("date", date));
                _command.Parameters.Add(new MySqlParameter("date1", date1));
                MySqlDataReader reader = _command.ExecuteReader();
                dt.Load(reader);
            }
            catch (Exception exception) { MonMessageBox.ShowBox("Liste depenses", exception); }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
            return dt;
        }

        public static DataTable ListeDesDepenses(int id)
        {
            var dt = new DataTable();
            try
            {
                _connection.Open();
                string query = "SELECT * FROM det_dep WHERE id =" + id;
                _command.CommandText = query;
                var reader = _command.ExecuteReader();
                dt.Load(reader);
                return dt;
            }
            catch (Exception exception) { MonMessageBox.ShowBox("Liste depense", exception); return null; }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }

        public static bool EnregistrerUnEncaissement(Encaissement encaissement )
        {
            try
            {
                _connection.Open();
                var requete = "";
                if (encaissement.ID== 0)
                {
                    requete = "INSERT INTO `caisse_tbl`(`date`,mois,annee, `montant`, `tiers`,`avoir`, libelle,code,datePaiement) VALUES ("
                         + "@date,@mois,@annee, @montant,  @tiers,  @avoir,@libelle,@code,@datePaiement)";
                }
                else
                {
                    if (MonMessageBox.ShowBox("Voulez vous modifier ces données ?", "Confirmation") == "1")
                    {
                        requete = "UPDATE `caisse_tbl` SET `date`=@date,mois=@mois,annee=@annee, " +
                            "`montant`=@montant, `tiers`=@tiers,`avoir`=  @avoir, code=@code, libelle=@libelle WHERE id =" + encaissement.ID;
                    }
                }
                _command.Parameters.Add(new MySqlParameter("date", encaissement.Date));
                _command.Parameters.Add(new MySqlParameter("code", encaissement.Code));
                _command.Parameters.Add(new MySqlParameter("montant", encaissement.Montant));
                _command.Parameters.Add(new MySqlParameter("tiers", encaissement.Tiers));
                _command.Parameters.Add(new MySqlParameter("avoir", encaissement.Avoir));
                _command.Parameters.Add(new MySqlParameter("mois", encaissement.Mois));
                _command.Parameters.Add(new MySqlParameter("annee", encaissement.Exercice));
                _command.Parameters.Add(new MySqlParameter("libelle", encaissement.Objet));
                _command.Parameters.Add(new MySqlParameter("datePaiement", DateTime.Now));
                _command.CommandText = requete;
                _command.ExecuteNonQuery();

                MonMessageBox.ShowBox("L'enregistrement a été éffectuée avec succés", "ajouter depenses");
                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L'enregistrement a echoue", "Ajouter depenses", ex);
                return false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }
        public static bool SupprimerUnEncaissement(int id)
        {
            try
            {
                _connection.Open();
                if (MonMessageBox.ShowBox("Voulez vous supprimer ces données ?", "Supprimer depense") == "1")
                {
                    string query = "DELETE FROM caisse_tbl WHERE id =" + id;
                    _command.CommandText = query;
                    _command.ExecuteNonQuery();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("La suppression des données a echoue", "Supprimer depense", ex);
                return false;
            }
            finally
            {
                _connection.Close();
            }
        }

        //liste des depenses
        public static List<Encaissement > ListeEncaissement(int annee)
        {
            var liste = new List<Encaissement >();
            try
            {
                _connection.Open();
                string query = "SELECT * FROM  caisse_tbl WHERE annee=" + annee;
                _command.CommandText = query;
                var reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var depense = new Encaissement();
                    depense.ID = reader.GetInt32(0);
                    depense.Objet = reader.GetString(2);
                    depense.Date = reader.GetDateTime(1);
                    depense.Montant = reader.GetDouble(3);
                    depense.Mois = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    depense.Exercice = !reader.IsDBNull(5) ? reader.GetInt32(5) :  0;
                    depense.Avoir = !reader.IsDBNull(6) ? reader.GetDouble(6) : .0;
                    depense.Tiers = !reader.IsDBNull(7) ? reader.GetString(7) : "";
                    depense.Code = !reader.IsDBNull(8) ? reader.GetString(8) : "";
                    depense.DateEncaissment =!reader.IsDBNull(9) ? reader.GetDateTime(9) :DateTime.Now;
                    liste.Add(depense);
                }
                return liste;
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("Liste depense", exception);
                return null;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }

        public static List<Encaissement> ListeEncaissementGroupeParJour(DateTime dateTime)
        {
            var liste = new List<Encaissement>();
            try
            {
                _connection.Open();
                string query = "SELECT * FROM  caisse_tbl WHERE datePaiement>=@date1 AND datePaiement<@date2";
                _command.CommandText = query;
                _command.Parameters.Add(new MySqlParameter("date1", dateTime.Date));
                _command.Parameters.Add(new MySqlParameter("date2", dateTime.Date.AddHours(24)));
                var reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var depense = new Encaissement();
                    depense.ID = reader.GetInt32(0);
                    depense.Objet = reader.GetString(2);
                    depense.Date = reader.GetDateTime(1);
                    depense.Montant = reader.GetDouble(3);
                    depense.Mois = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    depense.Exercice = !reader.IsDBNull(5) ? reader.GetInt32(5) : 0;
                    depense.Avoir = !reader.IsDBNull(6) ? reader.GetDouble(6) : .0;
                    depense.Tiers = !reader.IsDBNull(7) ? reader.GetString(7) : "";
                    depense.Code = !reader.IsDBNull(8) ? reader.GetString(8) : "";
                    depense.DateEncaissment = !reader.IsDBNull(9) ? reader.GetDateTime(9) : DateTime.Now;
                    liste.Add(depense);
                }
                return liste;
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("Liste depense", exception);
                return null;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }

        #endregion
        /****************************REGION FOURNISSEUR****************************/
        #region FOURNISSUERS

        public static bool CreerUnNouveauFournisseur(Fournisseur fournisseur)
        {
            try
            {
                _connection.Open();
                var _requete = "INSERT INTO fourn_tbl ( nom,adresse,tel1,tel2," +
            "fax,email, ville, pays, commentaire, postal,reference,telecopie,no_compte,nif,type) VALUES  (@nom,@adresse,@tel1,@tel2," +
            "@fax,@email, @ville, @pays, @commentaire, @postal,@reference,@telecopie,@no_compte,@nif,@type)";
                _command.Parameters.Add(new MySqlParameter("id", fournisseur.ID));
                _command.Parameters.Add(new MySqlParameter("nom", fournisseur.NomFournisseur));
                _command.Parameters.Add(new MySqlParameter("adresse", fournisseur.Adresse));
                _command.Parameters.Add(new MySqlParameter("tel1", fournisseur.Telephone1));
                _command.Parameters.Add(new MySqlParameter("tel2", fournisseur.Telephone2));
                _command.Parameters.Add(new MySqlParameter("fax", fournisseur.FAX));
                _command.Parameters.Add(new MySqlParameter("email", fournisseur.Email));
                _command.Parameters.Add(new MySqlParameter("ville", fournisseur.Ville));
                _command.Parameters.Add(new MySqlParameter("pays", fournisseur.Pays));
                _command.Parameters.Add(new MySqlParameter("commentaire", fournisseur.Commentaire));
                _command.Parameters.Add(new MySqlParameter("postal", fournisseur.NumeroPostal));
                _command.Parameters.Add(new MySqlParameter("reference", fournisseur.Reference));
                _command.Parameters.Add(new MySqlParameter("telecopie", fournisseur.Telecopie));
                _command.Parameters.Add(new MySqlParameter("no_compte", fournisseur.NoCompte));
                _command.Parameters.Add(new MySqlParameter("nif", fournisseur.NIF));
                _command.Parameters.Add(new MySqlParameter("type", fournisseur.Type ));
                _command.CommandText = _requete;
                _command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L'enregistrement du nouveau a échoué", "Erreur", ex);
                return false;
            }
            finally
            {
                _command.Parameters.Clear();
                _connection.Close();
            }
        }

        public static bool ModifierUnFournisseur(Fournisseur fournisseur)
        {
            try
            {
                _connection.Open();
                var _requete = string.Format("UPDATE fourn_tbl SET nom= @nom, adresse=@adresse,tel1=@tel1,tel2= @tel2," +
            "fax=@fax,email=@email, ville=@ville,pays= @pays,commentaire= @commentaire, type=@type," +
            " postal=@postal,reference=@reference,telecopie=@telecopie, no_compte=@no_compte, nif=@nif WHERE id ={0}", fournisseur.ID);

                _command.Parameters.Add(new MySqlParameter("nom", fournisseur.NomFournisseur));
                _command.Parameters.Add(new MySqlParameter("adresse", fournisseur.Adresse));
                _command.Parameters.Add(new MySqlParameter("tel1", fournisseur.Telephone1));
                _command.Parameters.Add(new MySqlParameter("tel2", fournisseur.Telephone2));
                _command.Parameters.Add(new MySqlParameter("fax", fournisseur.FAX));
                _command.Parameters.Add(new MySqlParameter("email", fournisseur.Email));
                _command.Parameters.Add(new MySqlParameter("ville", fournisseur.Ville));
                _command.Parameters.Add(new MySqlParameter("pays", fournisseur.Pays));
                _command.Parameters.Add(new MySqlParameter("commentaire", fournisseur.Commentaire));
                _command.Parameters.Add(new MySqlParameter("postal", fournisseur.NumeroPostal));
                _command.Parameters.Add(new MySqlParameter("reference", fournisseur.Reference));
                _command.Parameters.Add(new MySqlParameter("telecopie", fournisseur.Telecopie));
                _command.Parameters.Add(new MySqlParameter("no_compte", fournisseur.NoCompte));
                _command.Parameters.Add(new MySqlParameter("nif", fournisseur.NIF));
                _command.Parameters.Add(new MySqlParameter("type", fournisseur.Type));

                _command.CommandText = _requete;
                _command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L'enregistrement du nouveau a échoué", "Erreur", ex);
                return false;
            }
            finally
            {
                _command.Parameters.Clear();
                _connection.Close();
            }
        }

        public static bool SupprimerFournisseur(Fournisseur fournisseur)
        {
            try
            {
                _connection.Open();
               var  _requete = string.Format("DELETE FROM fourn_tbl WHERE id={0} ", fournisseur.ID);
                _command.CommandText = _requete;
                _command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L'enregistrement du nouveau a échoué", "Erreur", ex);
                return false;
            }
            finally
            {
                _connection.Close();
            }
        }

        public static List<Fournisseur> ListeFournisseur()
        {
            try
            {
                var listeLabo = new List<Fournisseur>();
                var _requete = "SELECT * FROM fourn_tbl ORDER BY nom";
                _command.CommandText = _requete;
                _connection.Open();
                var reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var fournisseur = new Fournisseur();
                    fournisseur.ID = reader.GetInt32(0);
                    fournisseur.NomFournisseur = reader.GetString(1);
                    fournisseur.Adresse = !reader.IsDBNull(2) ? reader.GetString(2) : "";
                    fournisseur.Telephone1 = !reader.IsDBNull(3) ? reader.GetString(3) : "";
                    fournisseur.Telephone2 = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    fournisseur.FAX = !reader.IsDBNull(5) ? reader.GetString(5) : "";
                    fournisseur.Email = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    fournisseur.Ville = !reader.IsDBNull(7) ? reader.GetString(7) : "";
                    fournisseur.Pays = !reader.IsDBNull(8) ? reader.GetString(8) : "";
                    fournisseur.Commentaire = !reader.IsDBNull(9) ? reader.GetString(9) : "";
                    fournisseur.NumeroPostal = !reader.IsDBNull(10) ? reader.GetString(10) : "";
                    fournisseur.Reference = !reader.IsDBNull(11) ? reader.GetString(11) : "";
                    fournisseur.Telecopie = !reader.IsDBNull(12) ? reader.GetString(12) : "";
                    fournisseur.NoCompte = !reader.IsDBNull(13) ? reader.GetString(13) : "";
                    fournisseur.NIF = !reader.IsDBNull(14) ? reader.GetString(14) : "";
                    fournisseur.Type = !reader.IsDBNull(15) ? reader.GetString(15) : "";
                    listeLabo.Add(fournisseur);
                }
                return listeLabo;
            }
            catch
            {
                return null;
            }
            finally
            {
                _connection.Close();
            }
        }

        public static List<Fournisseur> ListeFournisseur(int id)
        {
            try
            {
                var listeLabo = new List<Fournisseur>();
                var _requete = "SELECT * FROM fourn_tbl where id =" + id;
                _command.CommandText = _requete;
                _connection.Open();
                var reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var fournisseur = new Fournisseur();
                    fournisseur.ID = reader.GetInt32(0);
                    fournisseur.NomFournisseur = !reader.IsDBNull(1) ? reader.GetString(1) : "";
                    fournisseur.Adresse = !reader.IsDBNull(2) ? reader.GetString(2) : "";
                    fournisseur.Telephone1 = !reader.IsDBNull(3) ? reader.GetString(3) : "";
                    fournisseur.Telephone2 = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    fournisseur.FAX = !reader.IsDBNull(5) ? reader.GetString(5) : "";
                    fournisseur.Email = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    fournisseur.Ville = !reader.IsDBNull(7) ? reader.GetString(7) : "";
                    fournisseur.Pays = !reader.IsDBNull(8) ? reader.GetString(8) : "";
                    fournisseur.Commentaire = !reader.IsDBNull(9) ? reader.GetString(9) : "";
                    fournisseur.NumeroPostal = !reader.IsDBNull(10) ? reader.GetString(10) : "";
                    fournisseur.Reference = !reader.IsDBNull(11) ? reader.GetString(11) : "";
                    fournisseur.Telecopie = !reader.IsDBNull(12) ? reader.GetString(12) : "";
                    fournisseur.NoCompte = !reader.IsDBNull(13) ? reader.GetString(13) : "";
                    fournisseur.NIF = !reader.IsDBNull(14) ? reader.GetString(14) : "";
                    fournisseur.Type = !reader.IsDBNull(15) ? reader.GetString(15) : "";
                    listeLabo.Add(fournisseur);
                }
                return listeLabo;
            }
            catch
            {
                return null;
            }
            finally
            {
                _connection.Close();
            }
        }

        public static int ObtenirLeDernierNumeroFournisseur()
        {
            try
            {
                var _requete = "SELECT MAX(id) FROM fourn_tbl";
                _command.CommandText = _requete;
                _connection.Open();
                return (int)_command.ExecuteScalar();
            }
            catch { return 0; }
            finally
            {
                _connection.Close();
            }
        }

        #endregion
        /****************************REGION DEPOT****************************/
        #region REGLEMENT_DES_FACTURES

        public static List<Reglement> ListeDesReglements(int annee)
        {
            try
            {
                var listeReglement = new List<Reglement>();
               var _requete = "SELECT * FROM recette_tbl WHERE  annee= " + annee;
                _command.CommandText = _requete;
                _connection.Open();
                var reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var reglement = new Reglement();
                    reglement.NumeroReglement = reader.GetInt32(0);
                    reglement.DatePaiement = reader.GetDateTime(1);
                    reglement.MontantPaiement = reader.GetDouble(2);
                    reglement.Mois = !reader.IsDBNull(3) ? reader.GetString(3) : "";
                    reglement.idLibelle = !reader.IsDBNull(5) ? reader.GetInt32(5) : 0;
                    reglement.Exercice = !reader.IsDBNull(4) ? reader.GetInt32(4) : 0;
                    listeReglement.Add(reglement);
                }
                return listeReglement;
            }
            catch
            {
                return null;
            }
            finally
            {
                _connection.Close();
            }
        }

        public static List<Reglement> ListeDesReglements()
        {
            try
            {
                var listeReglement = new List<Reglement>();
                var _requete = "SELECT * FROM reglement_tbl " ;
                _command.CommandText = _requete;
                _connection.Open();
                var reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var reglement = new Reglement();
                    reglement.NumeroReglement = reader.GetInt32(0);
                    reglement.DatePaiement = reader.GetDateTime(1);
                    reglement.MontantPaiement = reader.GetDouble(2);
                    reglement.Mois = !reader.IsDBNull(3) ? reader.GetString(3) : "";
                    reglement.idLibelle = !reader.IsDBNull(5) ? reader.GetInt32(5) : 0;
                    reglement.Exercice = !reader.IsDBNull(4) ? reader.GetInt32(4) : 0;
                    listeReglement.Add(reglement);
                }
                return listeReglement;
            }
            catch
            {
                return null;
            }
            finally
            {
                _connection.Close();
            }
        }

        public static List<Reglement> ListeDesReglements(int id_fact, int stateReglement)
        {
            try
            {
                var listeReglement = new List<Reglement>();
                var _requete = "SELECT * FROM reglement_tbl WHERE id_fact= " + id_fact + " AND state= " + stateReglement;
                _command.CommandText = _requete;
                _connection.Open();
                var reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var reglement = new Reglement();
                    reglement.NumeroReglement = reader.GetInt32(0);
                    reglement.DatePaiement = reader.GetDateTime(1);
                    reglement.MontantPaiement = reader.GetDouble(2);
                    reglement.Mois = !reader.IsDBNull(3) ? reader.GetString(3) : "";
                    reglement.idLibelle = !reader.IsDBNull(5) ? reader.GetInt32(5) : 0;
                    reglement.Exercice = !reader.IsDBNull(4) ? reader.GetInt32(4) : 0;
                    listeReglement.Add(reglement);
                }
                return listeReglement;
            }
            catch
            {
                return null;
            }
            finally
            {
                _connection.Close();
            }
        }

        public static bool SupprimerUnReglementDuClient(int numeroReglement)
        {
            try
            {
                _connection.Open();
               var _requete = "DELETE FROM reglement_tbl WHERE id = " + numeroReglement;
                _command.CommandText = _requete;
                _command.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L'enregistrement a échoué", "Erreur", ex);
                return false;
            }
            finally
            {
                _connection.Close();
            }
        }
        public static bool EnregistrerUneRecette(Reglement reglement )
        {
            try
            {
                _connection.Open();
                var _requete = "";

                if (reglement.NumeroReglement>0)
                {
                    _requete = "UPDATE recette_tbl SET date_paiement=@date_paiement,mois=@mois, annee=@annee" +
                                ",montant=@montant, id_libelle =@id_libelle WHERE id = " + reglement.NumeroReglement;
                }
                else
                {
                    _requete = "INSERT INTO recette_tbl(date_paiement,montant,mois,annee,id_libelle)" +
                                   " VALUES( @date_paiement,@montant, @mois,@annee,@id_libelle)";          
                }
                
                _command.CommandText = _requete;
                _command.Parameters.Add(new MySqlParameter("date_paiement", reglement.DatePaiement));
                _command.Parameters.Add(new MySqlParameter("montant", reglement.MontantPaiement));
               _command.Parameters.Add(new MySqlParameter("mois", reglement.Mois));
                _command.Parameters.Add(new MySqlParameter("annee", reglement.Exercice));
                _command.Parameters.Add(new MySqlParameter("id_libelle", reglement.idLibelle));
                _command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L'enregistrement a échoué", "Erreur", ex);
                return false;
            }
            finally
            {
                _command.Parameters.Clear();
                _connection.Close();
            }
        }
        public static bool SupprimerUneRecette(int numeroReglement)
        {
            try
            {
                _connection.Open();
                if (MonMessageBox.ShowBox("Voulez vous supprimer ces données ? ", "Confirmation") == "1")
                {
                    var _requete = "DELETE FROM recette_tbl  WHERE id = " + numeroReglement;
                    _command.CommandText = _requete;
                    _command.ExecuteNonQuery();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L'enregistrement a échoué", "Erreur", ex);
                return false;
            }
            finally
            {
                _connection.Close();
            }
        }

        public static List<Reglement> ListeDesRecettes(int annee)
        {
            try
            {
                var listeReglement = new List<Reglement>();
                var _requete = "SELECT * FROM recette_tbl WHERE annee="+annee;
                _command.CommandText = _requete;
                _connection.Open();
                var reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var reglement = new Reglement();
                    reglement.NumeroReglement = reader.GetInt32(0);
                    reglement.DatePaiement = reader.GetDateTime(1);
                    reglement.MontantPaiement = !reader.IsDBNull(2) ? reader.GetDouble(2) : .0;
                    reglement.Mois = !reader.IsDBNull(3) ? reader.GetString(3) : "";
                    reglement.Exercice = !reader.IsDBNull(4) ? reader.GetInt32(4) : 0;
                    reglement.idLibelle = !reader.IsDBNull(5) ? reader.GetInt32(5) : 0;
                    listeReglement.Add(reglement);
                }
                return listeReglement;
            }
            catch
            {
                return null;
            }
            finally
            {
                _connection.Close();
            }
        }

        public static List<Reglement> ListeDesRecettes()
        {
            try
            {
                var listeReglement = new List<Reglement>();
                var _requete = "SELECT * FROM recette_tbl";
                _command.CommandText = _requete;
                _connection.Open();
                var reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var reglement = new Reglement();
                    reglement.NumeroReglement = reader.GetInt32(0);
                    reglement.DatePaiement = reader.GetDateTime(1);
                    reglement.MontantPaiement = !reader.IsDBNull(2) ? reader.GetDouble(2) : .0;
                    reglement.Mois = !reader.IsDBNull(3) ? reader.GetString(3) : "";
                    reglement.Exercice = !reader.IsDBNull(4) ? reader.GetInt32(4) : 0;
                    reglement.idLibelle = !reader.IsDBNull(5) ? reader.GetInt32(5) : 0;
                    listeReglement.Add(reglement);
                }
                return listeReglement;
            }
            catch
            {
                return null;
            }
            finally
            {
                _connection.Close();
            }
        }

        #endregion

        #region BANKS

        public static bool EnregistrerUneBank(Banque banque)
        {
            try
            {
                var _requete = "";
                _connection.Open();
                if (banque.ID==0)
                {
                    _requete = "INSERT INTO bank_tbl (bank,code_guichet,code_bank,iban) VALUES(@bank,@code_guichet,@code_bank,@iban)";
                }
                else
                {
                    if (MonMessageBox.ShowBox("Voulez vous modifier ces données?", "Confirmation") == "1")
                    {
                        _requete = "UPDATE bank_tbl SET bank = @bank ,code_guichet=@code_guichet,code_bank=@code_bank,iban=@iban WHERE id = " + banque.ID;
                     
                    }
                }
                  _command.Parameters.Add(new MySqlParameter("bank", banque.NomBanque));
                    _command.Parameters.Add(new MySqlParameter("code_guichet", banque.CodeGuichet));
                    _command.Parameters.Add(new MySqlParameter("code_bank", banque.CodeBanque));
                   _command.Parameters.Add(new MySqlParameter("iban", banque.IBAN));
                    _command.CommandText = _requete;
                    _command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L'enregistrement du nouveau a échoué", "Erreur", ex);
                return false;
            }
            finally
            {
                _command.Parameters.Clear();
                _connection.Close();
            }
        }

        public static  bool SupprimerUneBanque(int id)
        {
            try
            {
                if (MonMessageBox.ShowBox("Voulez vous supprimer ces données?", "Confirmation") == "1")
                {
                    _connection.Open();
                    var _requete = "DELETE FROM bank_tbl  WHERE id = " + id;
                    _command.CommandText = _requete;
                    _command.ExecuteNonQuery();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch { return false; }
            finally { _connection.Close(); }
        }

        public static List<Banque> ListeBanques()
        {
            try
            {
                var liste = new List<Banque>();
                var _requete = "SELECT * FROM bank_tbl ORDER BY bank ";
                _command.CommandText = _requete;
                _connection.Open();
                var reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var banque = new Banque();
                    banque.ID = reader.GetInt32(0);
                    banque.NomBanque = reader.GetString(1);
                    banque.CodeBanque = !reader.IsDBNull(2) ? reader.GetString(2) : "";
                    banque.CodeGuichet= !reader.IsDBNull(3) ? reader.GetString(3) : "";
                    banque.IBAN=!reader.IsDBNull(4) ? reader.GetString(4) : "";
                    banque.EtatParDefaut = !reader.IsDBNull(5) ? reader.GetBoolean(5) : false;
                    liste.Add(banque);
                }
                return liste;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("liste des bank", ex);
                return null;
            }
            finally
            {
                _connection.Close();
            }
        }

        public static List<Banque> ListeBanques(string bank)
        {
            try
            {
                var liste = new List<Banque>();
                var _requete = "SELECT * FROM bank_tbl WHERE bank = @bank ORDER BY bank ";
                _command.CommandText = _requete;
                _command.Parameters.Add(new MySqlParameter("bank", bank));
                _connection.Open();
                var reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var banque = new Banque();
                    banque.ID = reader.GetInt32(0);
                    banque.NomBanque = reader.GetString(1);
                    banque.CodeBanque = !reader.IsDBNull(2) ? reader.GetString(2) : "";
                    banque.CodeGuichet = !reader.IsDBNull(3) ? reader.GetString(3) : "";
                    banque.IBAN = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    banque.EtatParDefaut = !reader.IsDBNull(5) ? reader.GetBoolean(5) : false;
                    liste.Add(banque);
                }
                return liste;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("liste des bank", ex);
                return null;
            }
            finally
            {
                _command.Parameters.Clear();
                _connection.Close();
            }
        }
     
        public static List<Banque> ListeDonneesBancaires(string numeroMatricule)
        {
            try
            {
                var liste = new List<Banque>();
                var _requete = "SELECT * FROM bank_data_tbl WHERE num_mat=@num_mat ORDER BY etat DESC";
                _command.CommandText = _requete;
                _command.Parameters.Add(new MySqlParameter("num_mat", numeroMatricule));
                _connection.Open();
                var reader = _command.ExecuteReader();
                while(reader.Read())
                {
                    var b = new Banque();
                    b.ID = reader.GetInt32(0);
                    b.NumeroMatricule = reader.GetString(1);
                    b.Compte = reader.GetString(2);
                    b.CodeBanque = reader.GetString(3);
                    b.CodeGuichet = reader.GetString(4);
                    b.Cle = reader.GetString(5);
                    b.NomBanque = reader.GetString(6);
                    b.EtatParDefaut = !reader.IsDBNull(7) ? reader.GetBoolean(7) : false;
                    liste.Add(b);
                }
                return liste;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("liste des bank", ex);
                return null;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }
        public static List<Banque> ListeDonneesBancaires(int  id)
        {
            try
            {
                var liste = new List<Banque>();
                var _requete = "SELECT * FROM bank_data_tbl WHERE id_perso_proj=@id_perso_proj ORDER BY etat DESC";
                _command.CommandText = _requete;
                _command.Parameters.Add(new MySqlParameter("id_perso_proj", id));
                _connection.Open();
                var reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var b = new Banque();
                    b.ID = reader.GetInt32(0);
                    b.Compte = reader.GetString(2);
                    b.CodeBanque = reader.GetString(3);
                    b.CodeGuichet = reader.GetString(4);
                    b.Cle = reader.GetString(5);
                    b.NomBanque = reader.GetString(6);
                    b.EtatParDefaut = !reader.IsDBNull(7) ? reader.GetBoolean(7) : false;
                    liste.Add(b);
                }
                return liste;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("liste des bank", ex);
                return null;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }

       
       

        #endregion

        #region FraisConge

        //enregistrer uneavance sur salaire
        public static bool EnregistrerFraisConge(Conge  conge)
        {
            bool flag = false;
            try
            {
                _connection.Open();

                var requete = "INSERT INTO congec_tbl (`montant`,`num_mat`,`exercice`,`mois`) " +
                       " VALUES (@montant_total, @num_mat , " + conge.Exercice + ", '" + conge.Mois + "')";
                _command.Parameters.Add(new MySqlParameter("montant_total", conge.MontantConge));
                _command.Parameters.Add(new MySqlParameter("num_mat", conge.NumeroMatricule));
                _command.CommandText = requete;
                _command.ExecuteNonQuery();
                MonMessageBox.ShowBox("Données  enregistrées avec succés", "Enregistrement ");
                flag = true;
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("L'enregistrement a échoué", "Enregistre paiement", exception);
                flag = false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
            return flag;
        }

        //enregistrer uneavance sur salaire
        public static bool ModifierFraisConge(Conge conge)
        {
            bool flag = false;
            try
            {
                _connection.Open();

                var requete = "UPDATE congec_tbl SET `montant` =@montant_total,`num_mat` = @num_mat" +
                    ",`exercice`=" + conge.Exercice + ",`mois` = '" +
                    conge.Mois + "' WHERE id = " + conge.IDConge;
                _command.Parameters.Add(new MySqlParameter("montant_total", conge.MontantConge));
                _command.Parameters.Add(new MySqlParameter("num_mat", conge.NumeroMatricule));
                _command.CommandText = requete;
                _command.ExecuteNonQuery();
                MonMessageBox.ShowBox("Données modifiées avec succés", "Modification");
                flag = true;
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("L'enregistrement a échoué", "Enregistre paiement", exception);
                flag = false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
            return flag;
        }

        //enregistrer uneavance sur salaire
        public static bool SupprimerFraisConge(int  id)
        {
            bool flag = false;
            try
            {
                _connection.Open();
                var requete = "DELETE FROM congec_tbl WHERE id = " + id;
                _command.CommandText = requete;
                _command.ExecuteNonQuery();
                MonMessageBox.ShowBox("Données  supprimées avec succés", "Suppression ");
                flag = true;
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("L'enregistrement a échoué", "Enregistre paiement", exception);
                flag = false;
            }
            finally
            {
                _connection.Close();
            }
            return flag;
        }

        public static List<Conge> ListeFraisConge()
        {

            try
            {
                var liste = new List<Conge>();
                _connection.Open();
                string requete = "SELECT * FROM congec_tbl  ORDER BY id DESC";
                _command.CommandText = requete;
                MySqlDataReader reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var p = new Conge();
                    p.IDConge = reader.GetInt32(0);
                    p.MontantConge = reader.GetDouble(1);
                    p.NumeroMatricule = reader.GetString(2);
                    p.Mois = reader.GetString(3);
                    p.Exercice = reader.GetInt32(4);
                    liste.Add(p);
                }
                return liste;
            }

            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste avance sur salaire", ex);
                return null;
            }
            finally
            {
                _connection.Close();
            }
        }

        public static List<Conge> ListeFraisConge(int exercice, string mois)
        {

            try
            {
                var liste = new List<Conge>();
                _connection.Open();
                string requete = "SELECT * FROM congec_tbl  WHERE exercice = " + exercice + " AND  mois ='" + mois + "' ";
                _command.CommandText = requete;
                MySqlDataReader reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var p = new Conge();
                    p.IDConge = reader.GetInt32(0);
                    p.MontantConge = reader.GetDouble(1);
                    p.NumeroMatricule = reader.GetString(2);
                    p.Mois = reader.GetString(3);
                    p.Exercice = reader.GetInt32(4);
                    liste.Add(p);
                }
                return liste;
            }

            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste avance sur salaire", ex);
                return null;
            }
            finally
            {
                _connection.Close();
            }
        }

        public static List<Conge> ListeFraisConge(int exercice, string mois, string numEmpl)
        {

            try
            {
                var liste = new List<Conge>();
                _connection.Open();
                string requete = "SELECT * FROM congec_tbl  WHERE exercice = " + exercice + " AND  mois ='" + mois +
                    "' AND num_mat = @numMat";
                _command.CommandText = requete;
                _command.Parameters.Add(new MySqlParameter("numMat", numEmpl));
                MySqlDataReader reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var p = new Conge();
                    p.IDConge = reader.GetInt32(0);
                    p.MontantConge = reader.GetDouble(1);
                    p.NumeroMatricule = reader.GetString(2);
                    p.Mois = reader.GetString(3);
                    p.Exercice = reader.GetInt32(4);
                    liste.Add(p);
                }
                return liste;
            }

            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste avance sur salaire", ex);
                return null;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }

        #endregion


        #region RECAP_PAYE

        public static bool EnregistrerRecapitulatif(Paiement p, string state)
        {
            try
            {
                var _requete = "";
                _connection.Open();
                if (state == "1")
                {
                    _requete = "INSERT INTO recap_tbl (id_paie, libelle, montant) VALUES(@id_paie, @libelle, @montant)";
                    _command.Parameters.Add(new MySqlParameter("id_paie", p.IDPaie));
                    _command.Parameters.Add(new MySqlParameter("libelle",p.LibelleRecap));
                    _command.Parameters.Add(new MySqlParameter("montant", p.MontantTotal));
                    _command.CommandText = _requete;
                    _command.ExecuteNonQuery();
                }
                else if (state == "2")
                {
                    if (MonMessageBox.ShowBox("Voulez vous modifier ces données?", "Confirmation") == "1")
                    {
                        _requete = "UPDATE recap_tbl  SET  libelle= @libelle, montant=@montant WHERE id = " + p.IDRecap;

                        _command.Parameters.Add(new MySqlParameter("libelle", p.LibelleRecap));
                        _command.Parameters.Add(new MySqlParameter("montant", p.MontantTotal));
                        _command.CommandText = _requete;
                        _command.ExecuteNonQuery();
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (state == "3")
                {
                    if (MonMessageBox.ShowBox("Voulez vous supprimer ces données?", "Confirmation") == "1")
                    {
                        _requete = "DELETE FROM recap_tbl  WHERE id = " + p.IDRecap;
                        _command.CommandText = _requete;
                        _command.ExecuteNonQuery();
                    }
                    else
                    {
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L'enregistrement du nouveau a échoué", "Erreur", ex);
                return false;
            }
            finally
            {
                _command.Parameters.Clear();
                _connection.Close();
            }
        }


        public static List<Paiement> ListeRecapitulatifs(int numeroPaiement)
        {
            try
            {
                var liste = new List<Paiement>();
                var _requete = "SELECT * FROM recap_tbl WHERE id_paie = "+numeroPaiement +" ORDER BY libelle ";
                _command.CommandText = _requete;
                _connection.Open();
                var reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var p = new Paiement();
                    p.IDRecap = reader.GetInt32(0);
                    p.LibelleRecap = reader.GetString(2);
                    p.IDPaie = reader.GetInt32(1);
                    p.MontantTotal = reader.GetDouble(3);
                    liste.Add(p);
                }
                return liste;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("liste des recap", ex);
                return null;
            }
            finally
            {
                _connection.Close();
            }
        }


        #endregion

        #region Bilan
        public static bool EnregistrerBilan(Bilan bilan, string state)
        {
            try
            {
                _connection.Open();
                var _requete = "SELECT * FROM bilan_tbl WHERE type_bilan = @type_bilan1 AND annee="+bilan.Annee;
                _command.Parameters.Add(new MySqlParameter("type_bilan1", bilan.TypeBilan));
                _command.CommandText = _requete;
                var reader = _command.ExecuteReader();
                var dt = new DataTable();
                dt.Load(reader);
                if (dt.Rows.Count == 0)
                {

                    if (state == "1")
                    {
                        _requete = "INSERT INTO bilan_tbl (type_bilan,annee ) VALUES(@type_bilan," + bilan.Annee + ")";
                        _command.Parameters.Add(new MySqlParameter("type_bilan", bilan.TypeBilan));
                        _command.CommandText = _requete;
                        _command.ExecuteNonQuery();
                    }
                    if (state == "2")
                    {
                        if (MonMessageBox.ShowBox("Voulez vous modifier ces données?", "Confirmation") == "1")
                        {
                            _requete = "UPDATE bilan_tbl  SET type_bilan =@type_bilan , annee = " + bilan.Annee + " WHERE id =" + bilan.IDBilan;
                            _command.Parameters.Add(new MySqlParameter("type_bilan", bilan.TypeBilan));
                            _command.CommandText = _requete;
                            _command.ExecuteNonQuery();
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    MonMessageBox.ShowBox("Le type de bilan " + bilan.TypeBilan + " a deja été crée pour l'année " + bilan.Annee, "Erreur");
                    return false ;
                }
                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L'enregistrement du nouveau a échoué", "Erreur", ex);
                return false;
            }
            finally
            {
                _command.Parameters.Clear();
                _connection.Close();
            }
        }

        public static bool SupprimerUnBilan(int id)
        {
            try
            {
                _connection.Open();
                if (MonMessageBox.ShowBox("Voulez vous supprimer ces données?", "Confirmation") == "1")
                {
                    var _requete = "DELETE FROM bilan_tbl  WHERE id = " + id;
                    _command.CommandText = _requete;
                    _command.ExecuteNonQuery();
                    MonMessageBox.ShowBox("Données supprimées avec succés", "Affirmation");
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                _connection.Close();
            }
        }

        public static bool InsererUnBilan(Bilan bilan)
        {
            try
            {
                var _requete = "";
                _connection.Open();
                if (bilan.IDDetailBilan==0)
                {
                    _requete = "INSERT INTO detail_bilan (id_bil,designation, montant,etat ,type_detail) VALUES(@id_bil,@designation, @montant,@etat,@type_detail)";
                    _command.Parameters.Add(new MySqlParameter("id_bil", bilan.IDBilan));
                    _command.Parameters.Add(new MySqlParameter("designation", bilan.Designation));
                    _command.Parameters.Add(new MySqlParameter("montant", bilan.Montant));
                    _command.Parameters.Add(new MySqlParameter("etat", bilan.Etat));
                    _command.Parameters.Add(new MySqlParameter("type_detail", bilan.TypeDetail));
                    _command.CommandText = _requete;
                    _command.ExecuteNonQuery();
                }
                else if (bilan.IDDetailBilan>0)
                {
                    if (MonMessageBox.ShowBox("Voulez vous modifier ces données?", "Confirmation") == "1")
                    {
                        _requete = "UPDATE detail_bilan  SET designation=@designation, montant =@montant,etat=@etat,type_detail=@type_detail WHERE id = " + bilan.IDDetailBilan;
                        _command.Parameters.Add(new MySqlParameter("etat", bilan.Etat));
                        _command.Parameters.Add(new MySqlParameter("designation", bilan.Designation));
                        _command.Parameters.Add(new MySqlParameter("montant", bilan.Montant));
                        _command.Parameters.Add(new MySqlParameter("type_detail", bilan.TypeDetail));
                        _command.CommandText = _requete;
                        _command.ExecuteNonQuery();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("L'enregistrement du nouveau a échoué", "Erreur", ex);
                return false;
            }
            finally
            {
                _command.Parameters.Clear();
                _connection.Close();
            }
        }

        public static int DernierNumeroBilan()
        {
            try
            {
                var requete = "SELECT MAX(id) FROM bilan_tbl";
                _connection.Open();
                _command.CommandText = requete;
               return (int)_command.ExecuteScalar();
              

            }
            catch
            {
                return 0;
            }
            finally
            {
                _connection.Close();
            }
        }

        public static List<Bilan> ListeBilans(int annee)
        {
            try
            {
                var requete = "SELECT * FROM bilan_tbl WHERE annee=" + annee;
                _connection.Open();
                _command.CommandText = requete;
                var reader = _command.ExecuteReader();
                var liste = new List<Bilan>();
                while (reader.Read())
                {
                    var b = new Bilan();
                    b.IDBilan = reader.GetInt32(0);
                    b.TypeBilan = reader.GetString(1);
                    b.Annee = reader.GetInt32(2);
                    liste.Add(b);
                }
                return liste;
            }
            catch
            {
                return null;
            }
            finally
            {
                _connection.Close();
            }
        }

        public static List<Bilan> ListeDetailsBilans(int idBilan)
        {
            try
            {
                var requete = "SELECT * FROM detail_bilan WHERE id_bil=" + idBilan;
                _connection.Open();
                _command.CommandText = requete;
                var reader = _command.ExecuteReader();
                var liste = new List<Bilan>();
                while (reader.Read())
                {
                    var b = new Bilan();
                    b.IDDetailBilan = reader.GetInt32(0);
                    b.IDBilan = reader.GetInt32(1);
                    b.Designation = reader.GetString(2);
                    b.Montant = reader.GetDouble(3);
                    b.Etat = reader.GetString(4);
                    b.TypeDetail  =!reader.IsDBNull(5) ? reader.GetString(5) :"";
                    liste.Add(b);
                }
                return liste;
            }
            catch
            {
                return null;
            }
            finally
            {
                _connection.Close();
            }
        }

        public static List<Bilan> ListeDistinctDesDetailsBilans(int idBilan)
        {
            try
            {
                var requete = "SELECT DISTINCT etat FROM detail_bilan WHERE id_bil=" + idBilan;
                _connection.Open();
                _command.CommandText = requete;
                var reader = _command.ExecuteReader();
                var liste = new List<Bilan>();
                while (reader.Read())
                {
                    var b = new Bilan();
                    b.Etat = reader.GetString(0);
                    liste.Add(b);
                }
                return liste;
            }
            catch
            {
                return null;
            }
            finally
            {
                _connection.Close();
            }
        }

        public static List<Bilan> ListeDistinctTypeDesDetailsBilans(int idBilan)
        {
            try
            {
                var requete = "SELECT DISTINCT type_detail, etat FROM detail_bilan WHERE id_bil=" + idBilan;
                _connection.Open();
                _command.CommandText = requete;
                var reader = _command.ExecuteReader();
                var liste = new List<Bilan>();
                while (reader.Read())
                {
                    var b = new Bilan();
                    b.TypeDetail = reader.GetString(0);
                    b.Etat = reader.GetString(1);
                    //b.Code = !reader.IsDBNull(2) ? reader.GetString(2) : "";
                    liste.Add(b);
                }
                return liste;
            }
            catch(Exception ex)
            {
                MonMessageBox.ShowBox("", ex);
                return null;
            }
            finally
            {
                _connection.Close();
            }
        }

        public static bool  SupprimerUnBilanDetaille(int id)
        {
            try
            {
                if (MonMessageBox.ShowBox("Voulez vous supprimer ces données?", "Confirmation") == "1")
                {
                    _connection.Open();
                    var _requete = "DELETE FROM detail_bilan  WHERE id= " + id;
                    _command.CommandText = _requete;
                    _command.ExecuteNonQuery();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                _connection.Close();
            }
        }

        public static bool EnregistrerUnTypeBilan(Bilan bilan, string etat)
        {
            var flag = false;
            try
            {
                _connection.Open();
                var requete = "SELECT * FROM detail_type_tbl  WHERE type = @type1";
                _command.CommandText = requete;
                _command.Parameters.Add(new MySqlParameter("type1", bilan.TypeDetail));
                var reader = _command.ExecuteReader();
                if (etat == "1")
                {
                    if (!reader.HasRows)
                    {
                        reader.Close();
                        requete =
                           "INSERT INTO `detail_type_tbl` ( `type`,code) VALUES ( @type,@code)";
                    }
                    else
                    {
                        MonMessageBox.ShowBox("Ce type  " +bilan.TypeDetail + " existe déja dans la base de données", "Erreur enregistrement");
                    }
                }
                else if (etat == "2")
                {
                    reader.Close();
                    requete =
                       "UPDATE `detail_type_tbl` SET type=@type,code=@code WHERE id=" +bilan.IDDetailBilan;
                }
                _command.Parameters.Add(new MySqlParameter("type", bilan.TypeDetail));
                _command.Parameters.Add(new MySqlParameter("code", bilan.Code));
                _command.CommandText = requete;
                _command.ExecuteNonQuery();
                flag = true;
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("L'enregistrement a échoué veuillez verifier les parametres et réessayer",
                    "Erreur enregistrement", exception);
                flag = false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
            return flag;
        }

        public static bool SupprimerUnBilanDetailleType(int id)
        {
            try
            {
                if (MonMessageBox.ShowBox("Voulez vous supprimer ces données?", "Confirmation") == "1")
                {
                    _connection.Open();
                    var _requete = "DELETE FROM detail_type_tbl  WHERE id= " + id;
                    _command.CommandText = _requete;
                    _command.ExecuteNonQuery();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                _connection.Close();
            }
        }

        public static List<Bilan> ListeDistinctDesDetailsBilansType()
        {
            try
            {
                var requete = "SELECT  * FROM detail_type_tbl" ;
                _connection.Open();
                _command.CommandText = requete;
                var reader = _command.ExecuteReader();
                var liste = new List<Bilan>();
                while (reader.Read())
                {
                    var b = new Bilan();
                    b.IDDetailBilan = reader.GetInt32(0);
                    b.TypeDetail = !reader.IsDBNull(1) ? reader.GetString(1) : "";
                    b.Code = !reader.IsDBNull(2) ? reader.GetString(2) : "";
                    liste.Add(b);
                }
                return liste;
            }
            catch
            {
                return null;
            }
            finally
            {
                _connection.Close();
            }
        }

        public static string  ListeDistinctDesDetailsBilansType(string designation)
        {
            try
            {
                var requete = "SELECT  code FROM detail_type_tbl WHERE type =@type";
                _connection.Open();
                _command.CommandText = requete;
                _command.Parameters.Add(new MySqlParameter("type", designation));
                var reader = _command.ExecuteReader();
                string code = "";
                while (reader.Read())
                {
                    code = !reader.IsDBNull(0) ? reader.GetString(0) : "";
                }
                return code ;
            }
            catch
            {
                return "";
            }
            finally
            {
                _command.Parameters.Clear();
                _connection.Close();
            }
        }

        #endregion

        #region TYPECONTRAT
        public static bool EnregistrerUnTypeContrat(Service service)
        {
            try
            {
                _connection.Open();
                var requete = "";
                if(service.IDTypeContrat>0)
                {
                    requete = "UPDATE contrat_tbl SET type_contrat = @type_contrat, `etat`=@etat WHERE id = "+service.IDTypeContrat;
                    _command.Parameters.Add(new MySqlParameter("type_contrat", service.TypeContrat));
                    _command.Parameters.Add(new MySqlParameter("etat", service.EtatTaxe));
                    _command.CommandText = requete;
                    _command.ExecuteNonQuery();
                    return true;
                }
                else
                {
                    requete = "INSERT INTO contrat_tbl (type_contrat,`etat`) VALUES(@type_contrat,@etat)";
                    _command.Parameters.Add(new MySqlParameter("type_contrat", service.TypeContrat));
                    _command.Parameters.Add(new MySqlParameter("etat", service.EtatTaxe));
                    _command.CommandText = requete;
                    _command.ExecuteNonQuery();
                    return true;
                }

            }catch(Exception ex)
            {
                MonMessageBox.ShowBox("type contrat", ex);
                return false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }
        public static bool SupprimerUnTypeContrat(int iD)
        {
            try
            {
                _connection.Open();
                var requete = "DELETE contrat_tbl WHERE id = " + iD;
                _command.CommandText = requete;
                _command.ExecuteNonQuery();
                return true;

            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("type contrat", ex);
                return false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }
     
        public static  List<Service> ListeContrat()
        {
            try
            {
                var liste = new List<Service>();
                _connection.Open();
                var requete = "SELECT * FROM contrat_tbl ORDER BY type_contrat";
                _command.CommandText = requete;
                var reader= _command.ExecuteReader();
                while(reader.Read())
                {
                    var ser = new Service();
                    ser.IDTypeContrat = reader.GetInt32(0);
                    ser.TypeContrat = reader.GetString(1);
                    ser.EtatTaxe =!reader.IsDBNull(2) ? reader.GetBoolean(2) :false;
                    liste.Add(ser);
                }
                return liste;
            }
            catch { return null; }
            finally { _connection.Close(); }
        }
        #endregion
 
        #region ETUDIANT

        public static bool EnregistrerEtudiant(Etudiant etudiant)
        {
            try
            {
                _connection.Open();
                var requete = "";
                if (etudiant.NumeroEtudiant > 0)
                {
                    requete = "UPDATE etudiant_tbl SET nom = @nom, prenom = @prenom, date_naissance = @dob, " +
                        "lieu_naissance = @lieu_nai, sexe = @sexe, nationalite = @nation, telephone1 = @telephone1," +
                        "telephone2 = @telephone2, mail = @mail, adresse = @adresse, matricule=@matricule  WHERE(id =" + etudiant.NumeroEtudiant + ")";
                    _command.Parameters.Add(new MySqlParameter("nom", etudiant.Nom));
                    _command.Parameters.Add(new MySqlParameter("prenom", etudiant.Prenom));
                    _command.Parameters.Add(new MySqlParameter("dob", etudiant.DateNaissance));
                    _command.Parameters.Add(new MySqlParameter("lieu_nai", etudiant.LieuDeNaissance));
                    _command.Parameters.Add(new MySqlParameter("telephone1", etudiant.Telephone1));
                    _command.Parameters.Add(new MySqlParameter("telephone2", etudiant.Telephone2));
                    _command.Parameters.Add(new MySqlParameter("mail", etudiant.Email));
                    _command.Parameters.Add(new MySqlParameter("nation", etudiant.Nationalite));
                    _command.Parameters.Add(new MySqlParameter("sexe", etudiant.Sexe));
                    _command.Parameters.Add(new MySqlParameter("adresse", etudiant.Adresse));
                    _command.Parameters.Add(new MySqlParameter("matricule", etudiant.Matricule));
                    _command.CommandText = requete;
                    _command.ExecuteNonQuery();

                }
                else
                {
                    requete = "INSERT INTO `etudiant_tbl` ( `nom`, `prenom`, `date_naissance`, `lieu_naissance`, " +
                        "`telephone1`, `telephone2`, `mail`, `nationalite`,`adresse` ,`sexe`,`matricule`) " +
                        " VALUES ( @nom, @prenom,@dob, @lieu_nai, @telephone1, @telephone2, @email,@nation,@adresse,@sexe,@mat)";
                    _command.Parameters.Add(new MySqlParameter("nom", etudiant.Nom));
                    _command.Parameters.Add(new MySqlParameter("prenom", etudiant.Prenom));
                    _command.Parameters.Add(new MySqlParameter("dob", etudiant.DateNaissance));
                    _command.Parameters.Add(new MySqlParameter("lieu_nai", etudiant.LieuDeNaissance));
                    _command.Parameters.Add(new MySqlParameter("telephone1", etudiant.Telephone1));
                    _command.Parameters.Add(new MySqlParameter("telephone2", etudiant.Telephone2));
                    _command.Parameters.Add(new MySqlParameter("email", etudiant.Email));
                    _command.Parameters.Add(new MySqlParameter("nation", etudiant.Nationalite));
                    _command.Parameters.Add(new MySqlParameter("sexe", etudiant.Sexe));
                    _command.Parameters.Add(new MySqlParameter("adresse", etudiant.Adresse));
                    _command.Parameters.Add(new MySqlParameter("mat", etudiant.Matricule));
                    _command.CommandText = requete;
                    _command.ExecuteNonQuery();
                }
                return true;

            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("enregister etudiant", ex);
                return false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }
        //obtenir le dernier nuemro inscrit de l'etudiant
        public static int ObtenirDernierNumeroEtudiant()
        {
            try
            {
                //obtenir le dernier numero d'enregistrement de l'etudiant
                var requete = "SELECT MAX(id) FROM etudiant_tbl";
                _connection.Open();
                _command.CommandText = requete;
                return (int)_command.ExecuteScalar();
            }
            catch
            {
                return 0;
            }
            finally
            {
                _connection.Close();
            }
        }
        public static List<Etudiant> ListeDesEtudiants()
        {
            try
            {
                var liste = new List<Etudiant>();
                var requete = "SELECT * FROM etudiant_tbl";
                _connection.Open();
                _command.CommandText = requete;
                var reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var etudiant = new Etudiant();
                    etudiant.NumeroEtudiant = reader.GetInt32(0);
                    etudiant.Nom = reader.GetString(1);
                    etudiant.Prenom = reader.GetString(2);
                    etudiant.DateNaissance = !reader.IsDBNull(3) ? reader.GetDateTime(3) : DateTime.Now;
                    etudiant.LieuDeNaissance = reader.GetString(4);
                    etudiant.Telephone1 = !reader.IsDBNull(7) ? reader.GetString(7) : "";
                    etudiant.Telephone2 = !reader.IsDBNull(8) ? reader.GetString(8) : "";
                    etudiant.Adresse = !reader.IsDBNull(10) ? reader.GetString(10) : "";
                    etudiant.Email = !reader.IsDBNull(9) ? reader.GetString(9) : "";
                    etudiant.Nationalite = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    etudiant.Photo = !reader.IsDBNull(11) ? reader.GetString(11) : "";
                    etudiant.Sexe = !reader.IsDBNull(5) ? reader.GetString(5) : "";
                    etudiant.Matricule = !reader.IsDBNull(12) ? reader.GetString(12) : "";
                    liste.Add(etudiant);
                }
                return liste;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste des etudiants", ex);
                return null;
            }
            finally
            {
                _connection.Close();
            }
        }
        public static List<Etudiant> ListeDesEtudiants(int idEtudiant)
        {
            try
            {
                var liste = new List<Etudiant>();
                var requete = "SELECT * FROM etudiant_tbl WHERE id="+idEtudiant;
                _connection.Open();
                _command.CommandText = requete;
                var reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var etudiant = new Etudiant();
                    etudiant.NumeroEtudiant = reader.GetInt32(0);
                    etudiant.Nom = reader.GetString(1);
                    etudiant.Prenom = reader.GetString(2);
                    etudiant.DateNaissance = !reader.IsDBNull(3) ? reader.GetDateTime(3) : DateTime.Now;
                    etudiant.LieuDeNaissance = reader.GetString(4);
                    etudiant.Telephone1 = !reader.IsDBNull(7) ? reader.GetString(7) : "";
                    etudiant.Telephone2 = !reader.IsDBNull(8) ? reader.GetString(8) : "";
                    etudiant.Adresse = !reader.IsDBNull(10) ? reader.GetString(10) : "";
                    etudiant.Email = !reader.IsDBNull(9) ? reader.GetString(9) : "";
                    etudiant.Nationalite = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    etudiant.Photo = !reader.IsDBNull(11) ? reader.GetString(11) : "";
                    etudiant.Sexe = !reader.IsDBNull(5) ? reader.GetString(5) : "";
                    etudiant.Matricule = !reader.IsDBNull(12) ? reader.GetString(12) : "";
                    liste.Add(etudiant);
                }
                return liste;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste des etudiants", ex);
                return null;
            }
            finally
            {
                _connection.Close();
            }
        }
        public static bool EnregistrerEtablissement(Etablissement etablissement)
        {
            try
            {
                _connection.Open();
                var requete = "";
                if (etablissement.NumerEtablissment > 0)
                {
                    requete = "UPDATE etablissement_tbl SET  etablissement = @etablissement, ville = @ville, pays = @pays, " +
                        "telephone1 = @telephone1,telephone2 = @telephone2, email = @email, web = @web  WHERE(id =" + etablissement.NumerEtablissment + ")";
                }
                else
                {
                    requete = "INSERT INTO `etablissement_tbl` ( `etablissement`, `ville`, `pays`, `telephone1`, `telephone2`, `email`, `web`)" +
                        " VALUES ( @etablissement, @ville,@pays, @telephone1, @telephone2, @email,@web)";
                }
                _command.Parameters.Add(new MySqlParameter("etablissement", etablissement.NomEtablissment));
                _command.Parameters.Add(new MySqlParameter("ville", etablissement.Ville));
                _command.Parameters.Add(new MySqlParameter("pays", etablissement.Pays));
                _command.Parameters.Add(new MySqlParameter("telephone1", etablissement.Telephone1));
                _command.Parameters.Add(new MySqlParameter("telephone2", etablissement.Telephone2));
                _command.Parameters.Add(new MySqlParameter("email", etablissement.Email));
                _command.Parameters.Add(new MySqlParameter("web", etablissement.SiteWeb));
                _command.CommandText = requete;
                _command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("enregister etudiant", ex);
                return false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }
        public static List<Etablissement> ListeDesEtablissements()
        {
            try
            {
                var liste = new List<Etablissement>();
                var requete = "SELECT * FROM etablissement_tbl";
                _connection.Open();
                _command.CommandText = requete;
                var reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var etablissement = new Etablissement();
                    etablissement.NumerEtablissment = reader.GetInt32(0);
                    etablissement.NomEtablissment = reader.GetString(1);
                    etablissement.Ville = reader.GetString(2);
                    etablissement.Pays = reader.GetString(3);
                    etablissement.Telephone1 = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    etablissement.Telephone2 = !reader.IsDBNull(5) ? reader.GetString(5) : "";
                    etablissement.SiteWeb = !reader.IsDBNull(7) ? reader.GetString(7) : "";
                    etablissement.Email = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    liste.Add(etablissement);
                }
                return liste;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste des etudiants", ex);
                return null;
            }
            finally
            {
                _connection.Close();
            }
        }
        public static List<Etablissement> ListeDesEtablissements(string universite)
        {
            try
            {
                var liste = new List<Etablissement>();
                var requete = "SELECT * FROM etablissement_tbl WHERE etablissement = @etabliss";
                _connection.Open();
                _command.CommandText = requete;
                _command.Parameters.Add(new MySqlParameter("etabliss", universite));
                var reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var etablissement = new Etablissement();
                    etablissement.NumerEtablissment = reader.GetInt32(0);
                    etablissement.NomEtablissment = reader.GetString(1);
                    etablissement.Ville = reader.GetString(2);
                    etablissement.Pays = reader.GetString(3);
                    etablissement.Telephone1 = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    etablissement.Telephone2 = !reader.IsDBNull(5) ? reader.GetString(5) : "";
                    etablissement.SiteWeb = !reader.IsDBNull(7) ? reader.GetString(7) : "";
                    etablissement.Email = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    liste.Add(etablissement);
                }
                return liste;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste des etudiants", ex);
                return null;
            }
            finally
            {
                _command.Parameters.Clear();
                _connection.Close();
            }
        }
        public static List<Etablissement> ListeDesEtablissements(int id)
        {
            try
            {
                var liste = new List<Etablissement>();
                var requete = "SELECT * FROM etablissement_tbl WHERE id = @etabliss";
                _connection.Open();
                _command.CommandText = requete;
                _command.Parameters.Add(new MySqlParameter("etabliss", id));
                var reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var etablissement = new Etablissement();
                    etablissement.NumerEtablissment = reader.GetInt32(0);
                    etablissement.NomEtablissment = reader.GetString(1);
                    etablissement.Ville = reader.GetString(2);
                    etablissement.Pays = reader.GetString(3);
                    etablissement.Telephone1 = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    etablissement.Telephone2 = !reader.IsDBNull(5) ? reader.GetString(5) : "";
                    etablissement.SiteWeb = !reader.IsDBNull(7) ? reader.GetString(7) : "";
                    etablissement.Email = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    liste.Add(etablissement);
                }
                return liste;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste des etudiants", ex);
                return null;
            }
            finally
            {
                _command.Parameters.Clear();
                _connection.Close();
            }
        }

        public static bool SupprimerUnEtudiant(int iD)
        {
            try
            {
                _connection.Open();
                var requete = "DELETE FROM etudiant_tbl WHERE id = " + iD;
                _command.CommandText = requete;
                _command.ExecuteNonQuery();
                return true;

            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("type contrat", ex);
                return false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }

        public static bool SupprimerUnEtablissement(int iD)
        {
            try
            {
                _connection.Open();
                var requete = "DELETE FROM etablissement_tbl WHERE  id = " + iD;
                _command.CommandText = requete;
                _command.ExecuteNonQuery();
                return true;

            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("type contrat", ex);
                return false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }
        public static bool EnregistrerInformationEtudiant(InformationEtudiant inf)
        {
            try
            {
                _connection.Open();
                var requete = "";
                if (inf.NumerInformation > 0)
                {
                    requete = "UPDATE info_tbl SET  annee_scolaire = @annee_scolaire, niveau = @niveau, discipline = @discipline, debut=@debut," +
                        "fin = @fin,id_etudiant = @id_etudiant, id_institution = @id_institution, status_etu = @status_etu,status_pro=@status_pro  WHERE (id =" + inf.NumerInformation + ")";
                }
                else
                {
                    requete = "INSERT INTO `info_tbl` ( `annee_scolaire`, `niveau`, `discipline`, `debut`, `fin`, `id_etudiant`, `id_institution`,status_etu,status_pro)" +
                        " VALUES ( @annee_scolaire, @niveau,@discipline, @debut, @fin, @id_etudiant,@id_institution,@status_etu,@status_pro)";
                }
                _command.Parameters.Add(new MySqlParameter("annee_scolaire", inf.AnneeScolaire));
                _command.Parameters.Add(new MySqlParameter("niveau", inf.Niveau));
                _command.Parameters.Add(new MySqlParameter("discipline", inf.Discipline));
                _command.Parameters.Add(new MySqlParameter("debut", inf.AnneeEntree));
                _command.Parameters.Add(new MySqlParameter("fin", inf.AnneeSortie));
                _command.Parameters.Add(new MySqlParameter("id_etudiant", inf.NumeroEtudiant));
                _command.Parameters.Add(new MySqlParameter("id_institution", inf.NumeroInstitution));
                _command.Parameters.Add(new MySqlParameter("status_etu", inf.StatusEtudiant));
                _command.Parameters.Add(new MySqlParameter("status_pro", inf.StatusProgression));
                _command.CommandText = requete;
                _command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("enregister infos etudiant", ex);
                return false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }
        public static List<InformationEtudiant> ListeInformationEtudiant(int numeroEtudiant)
        {
            try
            {
                var liste = new List<InformationEtudiant>();
                var requete = "SELECT * FROM info_tbl WHERE id_etudiant=" + numeroEtudiant + " ORDER BY annee_scolaire DESC";
                _connection.Open();
                _command.CommandText = requete;
                var reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var inf = new InformationEtudiant();
                    inf.NumerInformation = reader.GetInt32(0);
                    inf.AnneeScolaire = reader.GetString(1);
                    inf.Niveau = reader.GetString(2);
                    inf.Discipline = reader.GetString(3);
                    inf.AnneeEntree = !reader.IsDBNull(4) ? reader.GetInt32(4) : 0;
                    inf.AnneeSortie = !reader.IsDBNull(5) ? reader.GetInt32(5) : 0;
                    inf.NumeroEtudiant = !reader.IsDBNull(6) ? reader.GetInt32(6) : 0;
                    inf.NumeroInstitution = !reader.IsDBNull(7) ? reader.GetInt32(7) : 0;
                    inf.StatusEtudiant = reader.GetString(8);
                    inf.StatusProgression = reader.GetString(9);
                    liste.Add(inf);
                }
                return liste;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste des etudiants", ex);
                return null;
            }
            finally
            {
                _connection.Close();
            }
        }
        public static List<InformationEtudiant> ListeInformationEtudiant()
        {
            try
            {
                var liste = new List<InformationEtudiant>();
                var requete = "SELECT * FROM info_tbl WHERE fin='"+DateTime.Now.Year+"' ORDER BY annee_scolaire DESC";
                _connection.Open();
                _command.CommandText = requete;
                var reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var inf = new InformationEtudiant();
                    inf.NumerInformation = reader.GetInt32(0);
                    inf.AnneeScolaire = reader.GetString(1);
                    inf.Niveau = reader.GetString(2);
                    inf.Discipline = reader.GetString(3);
                    inf.AnneeEntree = !reader.IsDBNull(4) ? reader.GetInt32(4) : 0;
                    inf.AnneeSortie = !reader.IsDBNull(5) ? reader.GetInt32(5) : 0;
                    inf.NumeroEtudiant = !reader.IsDBNull(6) ? reader.GetInt32(6) : 0;
                    inf.NumeroInstitution = !reader.IsDBNull(7) ? reader.GetInt32(7) : 0;
                    inf.StatusEtudiant = reader.GetString(8);
                    inf.StatusProgression = reader.GetString(9);
                    liste.Add(inf);
                }
                return liste;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste des etudiants", ex);
                return null;
            }
            finally
            {
                _connection.Close();
            }
        }

        public static bool SupprimerUneInformation(int iD)
        {
            try
            {
                _connection.Open();
                var requete = "DELETE FROM info_tbl WHERE id = " + iD;
                _command.CommandText = requete;
                _command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("supp info", ex);
                return false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }
        
        #endregion
   
        #region STAGIAIRE
        public static bool EnregistreUnStagiaire(Stagiaire stagiaire)
        {
            try
            {
                _connection.Open();
                var requete = "";
                if (stagiaire.IDStagiaire > 0)
                {
                    requete = "UPDATE stagiaire_tbl SET  nom = @nom, prenom = @prenom, adresse = @adresse, tele1=@tele1," +
                        "tele2 = @tele2,email = @email, date_nai = @date_nai, lieu_nai = @lieu_nai ,matricule=@matricule,sexe=@sexe  WHERE (id_stagiaire=" + stagiaire.IDStagiaire + ")";
                }
                else
                {
                    requete = "INSERT INTO `stagiaire_tbl` ( `nom`, `prenom`, `adresse`, `tele1`, `tele2`, `email`, `date_nai`,lieu_nai,matricule,sexe)" +
                        " VALUES ( @nom, @prenom,@adresse, @tele1, @tele2, @email,@date_nai,@lieu_nai,@matricule,@sexe)";
                }
                _command.Parameters.Add(new MySqlParameter("nom", stagiaire.Nom));
                _command.Parameters.Add(new MySqlParameter("prenom", stagiaire.Prenom));
                _command.Parameters.Add(new MySqlParameter("adresse", stagiaire.Adresse));
                _command.Parameters.Add(new MySqlParameter("tele1", stagiaire.Telephone1));
                _command.Parameters.Add(new MySqlParameter("tele2", stagiaire.Telephone2));
                _command.Parameters.Add(new MySqlParameter("email", stagiaire.Email));
                _command.Parameters.Add(new MySqlParameter("date_nai", stagiaire.DateNaissance));
                _command.Parameters.Add(new MySqlParameter("lieu_nai", stagiaire.LieuNaissance));
                _command.Parameters.Add(new MySqlParameter("matricule", stagiaire.Matricule));
                _command.Parameters.Add(new MySqlParameter("sexe", stagiaire.Sexe));
                _command.CommandText = requete;
                _command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("enregister infos etudiant", ex);
                return false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }
        public static int ObtenirDernierNumeroStagiaire()
        {
            try
            {
                //obtenir le dernier numero d'enregistrement du stagiaire_tbl
                var requete = "SELECT MAX(id) FROM stagiaire_tbl";
                _connection.Open();
                _command.CommandText = requete;
                return (int)_command.ExecuteScalar();
            }
            catch
            {
                return 0;
            }
            finally
            {
                _connection.Close();
            }
        }
   
        public static List<Stagiaire> ListeDesStagiaires()
        {
            try
            {
                var liste = new List<Stagiaire>();
                var requete = "SELECT * FROM stagiaire_tbl ORDER BY nom,prenom";
                _connection.Open();
                _command.CommandText = requete;
                var reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var stag = new Stagiaire();
                    stag.IDStagiaire = reader.GetInt32(0);
                    stag.Nom= reader.GetString(1);
                    stag.Prenom = reader.GetString(2);
                    stag.Adresse = reader.GetString(3);
                    stag.Telephone1 = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    stag.Telephone2 = !reader.IsDBNull(5) ? reader.GetString(5) : "";
                    stag.Email = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    stag.DateNaissance = !reader.IsDBNull(7) ? reader.GetDateTime(7) : DateTime.Now;
                    stag.LieuNaissance = reader.GetString(8);
                    stag.Matricule = reader.GetString(9);
                    stag.Sexe = reader.GetString(10);
                    liste.Add(stag);
                }
                return liste;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste des etudiants", ex);
                return null;
            }
            finally
            {
                _connection.Close();
            }
        }

        public static List<Stagiaire> ListeDesStagiaires(int id)
        {
            try
            {
                var liste = new List<Stagiaire>();
                var requete = "SELECT * FROM stagiaire_tbl  WHERE id_stagiaire = "+id +" ORDER BY nom,prenom";
                _connection.Open();
                _command.CommandText = requete;
                var reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var stag = new Stagiaire();
                    stag.IDStagiaire = reader.GetInt32(0);
                    stag.Nom = reader.GetString(1);
                    stag.Prenom = reader.GetString(2);
                    stag.Adresse = reader.GetString(3);
                    stag.Telephone1 = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    stag.Telephone2 = !reader.IsDBNull(5) ? reader.GetString(5) : "";
                    stag.Email = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    stag.DateNaissance = !reader.IsDBNull(7) ? reader.GetDateTime(7) : DateTime.Now;
                    stag.LieuNaissance = reader.GetString(8);
                    stag.Matricule = reader.GetString(9);
                    stag.Sexe = reader.GetString(10);
                    liste.Add(stag);
                }
                return liste;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste des etudiants", ex);
                return null;
            }
            finally
            {
                _connection.Close();
            }
        }

        public static bool SupprimerUnStagiaire(int iD)
        {
            try
            {
                _connection.Open();
                var requete = "DELETE FROM stagiaire_tbl WHERE id_stagiaire = " + iD;
                _command.CommandText = requete;
                _command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("supp info", ex);
                return false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }

        public static bool EnregistrerUnStage(Stagiaire stagiaire)
        {
            try
            {
                _connection.Open();
                var requete = "";
                if (stagiaire.IDStage > 0)
                {
                    requete = "UPDATE stage_tbl SET  type_stage = @type_stage, date_debut = @date_debut, date_fin = @date_fin, renum=@renum,duree=@duree,universite=@universite" +
                        ",montant = @montant,status = @status, superviseur = @superviseur, division = @division,service=@service,diplome=@diplome ,siDiplome=@siDiplome  WHERE (id=" + stagiaire.IDStage + ")";
                }
                else
                {
                    requete = "INSERT INTO `stage_tbl` ( `type_stage`, `date_debut`, `date_fin`, `renum`, `montant`, `status`, `superviseur`,division,service,id_stagiaire,duree,universite,diplome,siDiplome)" +
                        " VALUES ( @type_stage, @date_debut,@date_fin, @renum, @montant, @status,@superviseur,@division,@service,@id_stagiaire,@duree,@universite,@diplome,@siDiplome)";
                }
                _command.Parameters.Add(new MySqlParameter("type_stage", stagiaire.NatureStage));
                _command.Parameters.Add(new MySqlParameter("date_debut", stagiaire.DateDebut));
                _command.Parameters.Add(new MySqlParameter("date_fin", stagiaire.DateFin));
                _command.Parameters.Add(new MySqlParameter("renum", stagiaire.SiRenumere));
                _command.Parameters.Add(new MySqlParameter("montant", stagiaire.Montant));
                _command.Parameters.Add(new MySqlParameter("status", stagiaire.Status));
                _command.Parameters.Add(new MySqlParameter("superviseur", stagiaire.Faculte));
                _command.Parameters.Add(new MySqlParameter("division", stagiaire.Direction));
                _command.Parameters.Add(new MySqlParameter("service", stagiaire.Service));
                _command.Parameters.Add(new MySqlParameter("id_stagiaire", stagiaire.IDStagiaire));
                _command.Parameters.Add(new MySqlParameter("duree", stagiaire.Duree));
                _command.Parameters.Add(new MySqlParameter("universite", stagiaire.Universite));
                _command.Parameters.Add(new MySqlParameter("diplome", stagiaire.Diplome));
                _command.Parameters.Add(new MySqlParameter("siDiplome", stagiaire.SiDiplome));
                _command.CommandText = requete;
                _command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("enregister infos etudiant", ex);
                return false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }
        public static List<Stagiaire> ListeDesStages(int id)
        {
            try
            {
                var liste = new List<Stagiaire>();
                var requete = "SELECT * FROM stage_tbl WHERE id_stagiaire = "+id +" ORDER BY date_fin DESC";
                _connection.Open();
                _command.CommandText = requete;
                var reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var stag = new Stagiaire();
                    stag.IDStage = reader.GetInt32(0);
                    stag.NatureStage = reader.GetString(1);
                    stag.DateDebut = reader.GetDateTime(2);
                    stag.DateFin = reader.GetDateTime(3);
                    stag.SiRenumere = !reader.IsDBNull(4) ? reader.GetBoolean(4) : false;
                    stag.Montant = !reader.IsDBNull(5) ? reader.GetDouble(5) : 0;
                    stag.Status = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    stag.Faculte = !reader.IsDBNull(7) ? reader.GetString(7) : "";
                    stag.Direction = reader.GetString(8);
                    stag.Service = !reader.IsDBNull(9) ? reader.GetString(9) : "";
                    stag.IDStagiaire = reader.GetInt32(10);
                    stag.Duree = !reader.IsDBNull(11) ? reader.GetInt32(11) : 0;
                    stag.Diplome = !reader.IsDBNull(12) ? reader.GetString(12) : "";
                    stag.Universite = !reader.IsDBNull(13) ? reader.GetString(13) : "";
                    stag.SiDiplome = !reader.IsDBNull(14) ? reader.GetBoolean(14) : false ;
                    liste.Add(stag);
                }
                return liste;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste des etudiants", ex);
                return null;
            }
            finally
            {
                _connection.Close();
            }
        }
        public static List<Stagiaire> ListeDesStages()
        {
            try
            {
                var liste = new List<Stagiaire>();
                var requete = "SELECT * FROM stage_tbl  ORDER BY date_fin DESC";
                _connection.Open();
                _command.CommandText = requete;
                var reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var stag = new Stagiaire();
                    stag.IDStage = reader.GetInt32(0);
                    stag.NatureStage = reader.GetString(1);
                    stag.DateDebut = reader.GetDateTime(2);
                    stag.DateFin = reader.GetDateTime(3);
                    stag.SiRenumere = !reader.IsDBNull(4) ? reader.GetBoolean(4) : false;
                    stag.Montant = !reader.IsDBNull(5) ? reader.GetDouble(5) : 0;
                    stag.Status = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    stag.Faculte = !reader.IsDBNull(7) ? reader.GetString(7) : "";
                    stag.Direction = reader.GetString(8);
                    stag.Service = !reader.IsDBNull(9) ? reader.GetString(9) : "";
                    stag.IDStagiaire = reader.GetInt32(10);
                    stag.Duree = !reader.IsDBNull(11) ? reader.GetInt32(11) : 0;
                    stag.Diplome = !reader.IsDBNull(12) ? reader.GetString(12) : "";
                    stag.Universite = !reader.IsDBNull(13) ? reader.GetString(13) : "";
                    stag.SiDiplome = !reader.IsDBNull(14) ? reader.GetBoolean(14) : false;
                    liste.Add(stag);
                }
                return liste;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste des etudiants", ex);
                return null;
            }
            finally
            {
                _connection.Close();
            }
        }
        public static List<Stagiaire> AlerteStages()
        {
            try
            {
                var liste = new List<Stagiaire>();
                var requete = "SELECT * FROM stage_tbl WHERE date_fin <=@dateFin AND date_fin>@date ORDER BY date_fin DESC";
                _connection.Open();
                _command.CommandText = requete;
                _command.Parameters.Add(new MySqlParameter("date", DateTime.Now.Date));
                _command.Parameters.Add(new MySqlParameter("dateFin",DateTime.Now.Date.AddDays(7)));
                var reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var stag = new Stagiaire();
                    stag.IDStage = reader.GetInt32(0);
                    stag.NatureStage = reader.GetString(1);
                    stag.DateDebut = reader.GetDateTime(2);
                    stag.DateFin = reader.GetDateTime(3);
                    stag.SiRenumere = !reader.IsDBNull(4) ? reader.GetBoolean(4) : false;
                    stag.Montant = !reader.IsDBNull(5) ? reader.GetDouble(5) : 0;
                    stag.Status = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    stag.Faculte = !reader.IsDBNull(7) ? reader.GetString(7) : "";
                    stag.Direction = reader.GetString(8);
                    stag.Service = !reader.IsDBNull(9) ? reader.GetString(9) : "";
                    stag.IDStagiaire = reader.GetInt32(10);
                    stag.Duree = !reader.IsDBNull(11) ? reader.GetInt32(11) : 0;
                    stag.Diplome = !reader.IsDBNull(12) ? reader.GetString(12) : "";
                    stag.Universite = !reader.IsDBNull(13) ? reader.GetString(13) : "";
                    stag.SiDiplome = !reader.IsDBNull(14) ? reader.GetBoolean(14) : false;
                    liste.Add(stag);
                }
                return liste;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste des etudiants", ex);
                return null;
            }
            finally
            {
                _command.Parameters.Clear();
                _connection.Close();
            }
        }

        public static bool SupprimerUnStage(int iD)
        {
            try
            {
                _connection.Open();
                var requete = "DELETE FROM stage_tbl WHERE id = " + iD;
                _command.CommandText = requete;
                _command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("supp info", ex);
                return false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }

        #endregion

        #region PROJET
        public static bool EnregistreUnPartenaire(Projet projet)
        {
            try
            {
                _connection.Open();
                var requete = "";
                if (projet.NumeroPartenaire > 0)
                {
                    requete = "UPDATE partenaire_tbl SET  partenaire = partenaire, type = @type, localisation = @localisation WHERE (id=" + projet.NumeroPartenaire + ")";
                }
                else
                {
                    requete = "INSERT INTO `partenaire_tbl` ( `partenaire`, `type`, `localisation`) VALUES(@partenaire, @type, @localisation)";
                }
                _command.Parameters.Add(new MySqlParameter("partenaire", projet.Partenaire));
                _command.Parameters.Add(new MySqlParameter("type", projet.TypePartenaire));
                _command.Parameters.Add(new MySqlParameter("localisation", projet.Localisation));
                _command.CommandText = requete;
                _command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("enregister infos etudiant", ex);
                return false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }
     
        public static List<Projet> ListeDesPartenaires()
        {
            try
            {
                var liste = new List<Projet>();
                var requete = "SELECT * FROM partenaire_tbl ORDER BY partenaire";
                _connection.Open();
                _command.CommandText = requete;
                var reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var pro = new Projet();
                    pro.NumeroPartenaire = reader.GetInt32(0);
                    pro.Partenaire = reader.GetString(1);
                    pro.TypePartenaire = reader.GetString(2);
                    pro.Localisation = reader.GetString(3);
                    liste.Add(pro);
                }
                return liste;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste des etudiants", ex);
                return null;
            }
            finally
            {
                _connection.Close();
            }
        }

        public static bool SupprimerUnPartenaire(int iD)
        {
            try
            {
                _connection.Open();
                var requete = "DELETE FROM partenaire_tbl WHERE id = " + iD;
                _command.CommandText = requete;
                _command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("supp info", ex);
                return false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }

        public static bool EnregistrerUnProjet(Projet projet)
        {
            try
            {
                _connection.Open();
                var requete = "";
                if (projet.NumeroProjet > 0)
                {
                    requete = "UPDATE projet_tbl SET  id_part = @id_part, activites = @activities, description = @description"+
                        " , debut =@debut, fin =@fin , etat=@etat, status=@status WHERE (id=" + projet.NumeroProjet + ")";
                }
                else
                {
                    requete = "INSERT INTO projet_tbl ( id_part, activites, description,debut,fin,etat,status)"+
                        "VALUES(@id_part, @activities,@description,@debut,@fin,@etat,@status)";
                }
                _command.Parameters.Add(new MySqlParameter("id_part", projet.NumeroPartenaire));
                _command.Parameters.Add(new MySqlParameter("activities", projet.NomProjet));
                _command.Parameters.Add(new MySqlParameter("description", projet.Description));
                _command.Parameters.Add(new MySqlParameter("debut", projet.DateDebut));
                _command.Parameters.Add(new MySqlParameter("fin", projet.DateFin));
                _command.Parameters.Add(new MySqlParameter("etat", projet.Etat));
                _command.Parameters.Add(new MySqlParameter("status", projet.Status));
                _command.CommandText = requete;
                _command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("enregister infos etudiant", ex);
                return false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }

        public static List<Projet> ListeDesProjets()
        {
            try
            {
                var liste = new List<Projet>();
                var requete = "SELECT * FROM projet_tbl ORDER BY fin DESC";
                _connection.Open();
                _command.CommandText = requete;
                var reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var pro = new Projet();
                    pro.NumeroProjet = reader.GetInt32(0);
                    pro.NumeroPartenaire = reader.GetInt32(1);
                    pro.NomProjet = reader.GetString(2);
                    pro.Description = reader.GetString(3);
                    pro.DateDebut = reader.GetDateTime(4);
                    pro.DateFin = reader.GetDateTime(5);
                    pro.Etat = reader.GetBoolean(6);
                    pro.Status = reader.GetString(7);
                    liste.Add(pro);
                }
                return liste;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste des etudiants", ex);
                return null;
            }
            finally
            {
                _connection.Close();
            }
        }

        public static List<Projet> ListeDesProjets(int idPartenaire)
        {
            try
            {
                var liste = new List<Projet>();
                var requete = "SELECT * FROM projet_tbl WHERE id_part ="+idPartenaire+" ORDER BY fin DESC";
                _connection.Open();
                _command.CommandText = requete;
                var reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var pro = new Projet();
                    pro.NumeroProjet = reader.GetInt32(0);
                    pro.NumeroPartenaire = reader.GetInt32(1);
                    pro.NomProjet = reader.GetString(2);
                    pro.Description = reader.GetString(3);
                    pro.DateDebut = reader.GetDateTime(4);
                    pro.DateFin = reader.GetDateTime(5);
                    pro.Etat = reader.GetBoolean(6);
                    pro.Status = reader.GetString(7);
                    liste.Add(pro);
                }
                return liste;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste des etudiants", ex);
                return null;
            }
            finally
            {
                _connection.Close();
            }
        }
        public static List<Projet> ListeDesProjetParIDProjet(int idProjet)
        {
            try
            {
                var liste = new List<Projet>();
                var requete = "SELECT * FROM projet_tbl WHERE id =" + idProjet + " ORDER BY fin DESC";
                _connection.Open();
                _command.CommandText = requete;
                var reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var pro = new Projet();
                    pro.NumeroProjet = reader.GetInt32(0);
                    pro.NumeroPartenaire = reader.GetInt32(1);
                    pro.NomProjet = reader.GetString(2);
                    pro.Description = reader.GetString(3);
                    pro.DateDebut = reader.GetDateTime(4);
                    pro.DateFin = reader.GetDateTime(5);
                    pro.Etat = reader.GetBoolean(6);
                    pro.Status = reader.GetString(7);
                    liste.Add(pro);
                }
                return liste;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste des etudiants", ex);
                return null;
            }
            finally
            {
                _connection.Close();
            }
        }

        public static bool SupprimerUnProjet(int iD)
        {
            try
            {
                _connection.Open();
                var requete = "DELETE FROM projet_tbl WHERE id = " + iD;
                _command.CommandText = requete;
                _command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("supp info", ex);
                return false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }
        public static bool EnregistreUneRegion(Projet projet)
        {
            try
            {
                _connection.Open();
                var requete = "";
                if (projet.IDRegion > 0)
                {
                    requete = "UPDATE region_tbl SET  region = @region, id_projet = @id_projet, localisation = @localisation WHERE (id=" + projet.IDRegion + ")";
                }
                else
                {
                    requete = "INSERT INTO `region_tbl` ( `region`, `id_projet`, `localisation`) VALUES(@region, @id_projet, @localisation)";
                }
                _command.Parameters.Add(new MySqlParameter("region", projet.Region));
                _command.Parameters.Add(new MySqlParameter("id_projet", projet.NumeroProjet));
                _command.Parameters.Add(new MySqlParameter("localisation", projet.Localisation));
                _command.CommandText = requete;
                _command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("enregister infos etudiant", ex);
                return false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }

        public static List<Projet> ListeDesRegions(int idProjet)
        {
            try
            {
                var liste = new List<Projet>();
                var requete = "SELECT * FROM region_tbl WHERE id_projet = "+idProjet+" ORDER BY region";
                _connection.Open();
                _command.CommandText = requete;
                var reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var pro = new Projet();
                    pro.IDRegion = reader.GetInt32(0);
                    pro.Region = reader.GetString(1);
                    pro.Localisation = reader.GetString(2);
                    pro.NumeroProjet = reader.GetInt32(3);
                    liste.Add(pro);
                }
                return liste;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("Liste des etudiants", ex);
                return null;
            }
            finally
            {
                _connection.Close();
            }
        }

        public static bool SupprimerUneRegion(int iD)
        {
            try
            {
                _connection.Open();
                var requete = "DELETE FROM region_tbl WHERE id = " + iD;
                _command.CommandText = requete;
                _command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("supp info", ex);
                return false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }

        #endregion

        #region PERSONNEL_PROJET
        public static bool AjouterUnPersonnelProjet(Personnel personnel, Service service, Banque bank)
        {
            var flag = false;
            try
            {
                _connection.Open();
                _transaction = _connection.BeginTransaction();
                var requete = "INSERT INTO `pers_projet_tbl` ( `nom`, `prenom`, `date_nai`, `lieu_nai`, `addres`, `tele1`, `tele2`, `email`, `sexe`, " +
                    "  `photo`,`age`,situation_mat,nbrEnfant) VALUES ( @nom, @prenom, @date_nai,@lieu_nai," +
                "@addres, @tele1, @tele2, @email, @sexe, @photo,@age,@situation_mat,@nbrEnfant)";
                //_command.Parameters.Add(new MySqlParameter("id", personnel.IDPersonelProjet));
                _command.Parameters.Add(new MySqlParameter("nom", personnel.Nom));
                _command.Parameters.Add(new MySqlParameter("prenom", personnel.Prenom));
                _command.Parameters.Add(new MySqlParameter("date_nai", personnel.DateNaissance));
                _command.Parameters.Add(new MySqlParameter("addres", personnel.Adresse));
                _command.Parameters.Add(new MySqlParameter("tele1", personnel.Telephone1));
                _command.Parameters.Add(new MySqlParameter("tele2", personnel.Telephone2));
                _command.Parameters.Add(new MySqlParameter("sexe", personnel.Sexe));
                _command.Parameters.Add(new MySqlParameter("email", personnel.Email));
                _command.Parameters.Add(new MySqlParameter("lieu_nai", personnel.LieuNaissance));
                _command.Parameters.Add(new MySqlParameter("photo", personnel.Photo));
                _command.Parameters.Add(new MySqlParameter("age", personnel.Age));
                _command.Parameters.Add(new MySqlParameter("situation_mat", personnel.SituationMatrimoniale));
                _command.Parameters.Add(new MySqlParameter("nbrEnfant", personnel.NombreEnfant));
                _command.CommandText = requete;
                _command.Transaction = _transaction;
                _command.Connection = _connection;
                _command.ExecuteNonQuery();

                requete = "SELECT MAX(id) FROM pers_projet_tbl";
                _command.CommandText = requete;
                _command.Transaction = _transaction;
                _command.Connection = _connection;
                personnel.IDPersonelProjet = (int)_command.ExecuteScalar();

                requete = "INSERT INTO `service_projet_tbl` (`date_pris`, `poste`, `id_perso`, `echelon`,`categorie`,`anciennete`,`no_cnps`,`diplome`,`contrat`,`status`,date_depart,id_dep,id_div,salaire_brut,autre_prime,id_projet,region,localite,grade)" +
                    " VALUES (@date_pris, @poste, @id_perso, @echelon,@categorie,@anciennete,@no_cnps,@diplome,@contrat,@status,@date_depart,@id_dep,@id_div,@salaire_brut,@autre_prime,@id_projet,@region,@localite,@grade)";
                _command.Parameters.Add(new MySqlParameter("date_pris", service.DateService));
                _command.Parameters.Add(new MySqlParameter("poste", service.Poste));
                _command.Parameters.Add(new MySqlParameter("id_perso", personnel.IDPersonelProjet));
                _command.Parameters.Add(new MySqlParameter("echelon", service.Echelon));
                _command.Parameters.Add(new MySqlParameter("categorie", service.Categorie));
                _command.Parameters.Add(new MySqlParameter("anciennete", service.Anciennete));
                _command.Parameters.Add(new MySqlParameter("contrat", service.TypeContrat));
                _command.Parameters.Add(new MySqlParameter("status", service.Status));
                _command.Parameters.Add(new MySqlParameter("no_cnps", service.NoCNPS));
                _command.Parameters.Add(new MySqlParameter("diplome", service.Diplome));
                _command.Parameters.Add(new MySqlParameter("date_depart", service.DateDepart));
                _command.Parameters.Add(new MySqlParameter("id_dep", service.IDDepartement));
                _command.Parameters.Add(new MySqlParameter("id_div", service.IDDivision));
                _command.Parameters.Add(new MySqlParameter("salaire_brut", service.SalaireBrut));
                _command.Parameters.Add(new MySqlParameter("autre_prime", service.Primes));
                _command.Parameters.Add(new MySqlParameter("id_projet", service.IDProjet));
                _command.Parameters.Add(new MySqlParameter("region", service.Region));
                _command.Parameters.Add(new MySqlParameter("localite", service.Localite));
                _command.Parameters.Add(new MySqlParameter("grade", service.Grade));


                _command.Connection = _connection;
                _command.Transaction = _transaction;
                _command.CommandText = requete;
                _command.ExecuteNonQuery();

                requete = "INSERT INTO `bank_data_tbl` (`id_perso_proj`, `no_compte`,code_banc,code_gui,cle,bank) VALUES (@id_perso_proj,@no_compte,@code_banc,@code_gui,@cle,@bank)";
                _command.Parameters.Add(new MySqlParameter("no_compte", bank.Compte));
                _command.Parameters.Add(new MySqlParameter("code_banc", bank.CodeBanque));
                _command.Parameters.Add(new MySqlParameter("code_gui", bank.CodeGuichet));
                _command.Parameters.Add(new MySqlParameter("cle", bank.Cle));
                _command.Parameters.Add(new MySqlParameter("bank", bank.NomBanque));
                _command.Parameters.Add(new MySqlParameter("id_perso_proj", personnel.IDPersonelProjet));
                _command.Connection = _connection;
                _command.Transaction = _transaction;
                _command.CommandText = requete;
                _command.ExecuteNonQuery();

                _transaction.Commit();
                MonMessageBox.ShowBox("Nouveau personnel est inseré avec succés dans la base de données", "Enregistrement personnel");
                flag = true;
            }
            catch (Exception exception)
            {
                if (_transaction != null)
                    _transaction.Rollback();
                MonMessageBox.ShowBox("L'enregistrement du personnel a échoué", "Enregistrement personnel", exception);
                flag = false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
            return flag;
        }
        public static bool ModifierUnPersonnelProjet(Personnel personnel, Service service, Banque bank)
        {
            var flag = false;
            try
            {
                _connection.Open();

                _transaction = _connection.BeginTransaction();
                var requete = "UPDATE `pers_projet_tbl` SET  `nom` = @nom, `prenom` = @prenom, `date_nai` = @date_nai, `lieu_nai` = @lieu_nai," +
                    " `addres` =@addres, `tele1` = @tele1, `tele2` = @tele2, `email` =@email, `sexe`=@sexe," +
                    "`age` = @age,situation_mat=@situation_mat,nbrEnfant=@nbrEnfant  WHERE `id`  ="+personnel.IDPersonelProjet;
                //_command.Parameters.Add(new MySqlParameter("num_mat01", personnel.NumeroMatricule));
                _command.Parameters.Add(new MySqlParameter("nom", personnel.Nom));
                _command.Parameters.Add(new MySqlParameter("prenom", personnel.Prenom));
                _command.Parameters.Add(new MySqlParameter("date_nai", personnel.DateNaissance));
                _command.Parameters.Add(new MySqlParameter("addres", personnel.Adresse));
                _command.Parameters.Add(new MySqlParameter("tele1", personnel.Telephone1));
                _command.Parameters.Add(new MySqlParameter("tele2", personnel.Telephone2));
                _command.Parameters.Add(new MySqlParameter("sexe", personnel.Sexe));
                _command.Parameters.Add(new MySqlParameter("email", personnel.Email));
                _command.Parameters.Add(new MySqlParameter("lieu_nai", personnel.LieuNaissance));
                _command.Parameters.Add(new MySqlParameter("age", personnel.Age));
                _command.Parameters.Add(new MySqlParameter("situation_mat", personnel.SituationMatrimoniale));
                _command.Parameters.Add(new MySqlParameter("nbrEnfant", personnel.NombreEnfant));
                _command.CommandText = requete;
                _command.Transaction = _transaction;
                _command.Connection = _connection;
                _command.ExecuteNonQuery();

                requete =
                    "UPDATE `service_projet_tbl` SET `date_pris` =@date_pris, `poste`=@poste,  `echelon` = @echelon,`categorie`=@categorie,`anciennete`=@anciennete, id_dep=@id_dep, region=@region,localite=@localite," +
                    "`no_cnps` = @no_cnps,`diplome` = @diplome,`contrat` = @contrat,grade=@grade,`status` = @status,  date_depart=@date_depart, salaire_brut=@salaire_brut,autre_prime=@autre_prime,id_div=@id_div WHERE id_perso=" + personnel.IDPersonelProjet;
                _command.Parameters.Add(new MySqlParameter("date_pris", service.DateService));
                _command.Parameters.Add(new MySqlParameter("poste", service.Poste));
                _command.Parameters.Add(new MySqlParameter("num_mat1", service.NumeroMatricule));
                _command.Parameters.Add(new MySqlParameter("echelon", service.Echelon));
                _command.Parameters.Add(new MySqlParameter("categorie", service.Categorie));
                _command.Parameters.Add(new MySqlParameter("anciennete", service.Anciennete));
                _command.Parameters.Add(new MySqlParameter("contrat", service.TypeContrat));
                _command.Parameters.Add(new MySqlParameter("status", service.Status));
                _command.Parameters.Add(new MySqlParameter("no_cnps", service.NoCNPS));
                _command.Parameters.Add(new MySqlParameter("diplome", service.Diplome));
                _command.Parameters.Add(new MySqlParameter("date_depart", service.DateDepart));
                _command.Parameters.Add(new MySqlParameter("id_dep", service.IDDepartement));
                _command.Parameters.Add(new MySqlParameter("id_div", service.IDDivision));
                _command.Parameters.Add(new MySqlParameter("salaire_brut", service.SalaireBrut));
                _command.Parameters.Add(new MySqlParameter("autre_prime", service.Primes));
                _command.Parameters.Add(new MySqlParameter("region", service.Region));
                _command.Parameters.Add(new MySqlParameter("localite", service.Localite));
                _command.Parameters.Add(new MySqlParameter("grade", service.Grade));
                _command.Connection = _connection;
                _command.Transaction = _transaction;
                _command.CommandText = requete;
                _command.ExecuteNonQuery();

              
                requete = "UPDATE `bank_data_tbl` SET `no_compte`=@no_compte,code_banc=@code_banc," +
             "code_gui=@code_gui,cle=@cle,bank=@bank WHERE id_perso_proj =" + personnel.IDPersonelProjet;
                _command.Parameters.Add(new MySqlParameter("no_compte", bank.Compte));
                _command.Parameters.Add(new MySqlParameter("code_banc", bank.CodeBanque));
                _command.Parameters.Add(new MySqlParameter("code_gui", bank.CodeGuichet));
                _command.Parameters.Add(new MySqlParameter("cle", bank.Cle));
                _command.Parameters.Add(new MySqlParameter("bank", bank.NomBanque));
                _command.Connection = _connection;
                _command.Transaction = _transaction;
                _command.CommandText = requete;
                _command.ExecuteNonQuery();
                _transaction.Commit();
                MonMessageBox.ShowBox("Les donnees du personnel ont été modifiées avec succés", "Modification personnel");
                flag = true;
            }
            catch (Exception exception)
            {
                MonMessageBox.ShowBox("La modification des données du personnel a échoué", "Modification personnel", exception);
                flag = false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
            return flag;
        }
        public static List<Personnel> ListePersonnelProjet(string nom)
        {
            var liste = new List<Personnel>();
            try
            {
                var requete = "SELECT * FROM pers_projet_tbl WHERE nom LIKE '%" + nom + "%'";
                _command.CommandText = requete;
                _connection.Open();
                MySqlDataReader reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var p = new Personnel();
                    p.IDPersonelProjet = reader.GetInt32(0);
                    p.Nom = reader.GetString(1);
                    p.Prenom = reader.GetString(2);
                    p.DateNaissance = !reader.IsDBNull(3) ? reader.GetDateTime(3) : DateTime.Now;
                    p.LieuNaissance = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    p.Adresse = !reader.IsDBNull(5) ? reader.GetString(5) : "";
                    p.Telephone1 = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    p.Telephone2 = !reader.IsDBNull(7) ? reader.GetString(7) : "";
                    p.Email = !reader.IsDBNull(8) ? reader.GetString(8) : "";
                    p.Sexe = !reader.IsDBNull(9) ? reader.GetString(9) : "";
                    p.Photo = !reader.IsDBNull(10) ? reader.GetString(10) : "";
                    p.Age = !reader.IsDBNull(11) ? reader.GetInt32(11) : 0;
                    p.SituationMatrimoniale = !reader.IsDBNull(12) ? reader.GetString(12) : "";
                    p.NombreEnfant = !reader.IsDBNull(13) ? reader.GetInt32(13) : 0;
                    liste.Add(p);
                }
                return liste;
            }
            catch { return null; }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }
        public static List<Service> ListeServicePersonnelProjet()
        {
            try
            {
                _connection.Open();
                var requete = "SELECT * FROM `service_projet_tbl`";
                _command.CommandText = requete;
                var reader = _command.ExecuteReader();
                var liste = new List<Service>();
                while (reader.Read())
                {
                    var s = new Service();
                    s.NumeroService = reader.GetInt32(0);
                    s.DateService = reader.GetDateTime(1);
                    s.Poste = reader.GetString(2);
                    s.IDPersonelProjet = reader.GetInt32(3);
                    s.Echelon = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    s.Categorie = !reader.IsDBNull(5) ? reader.GetString(5) : "";
                    s.Anciennete = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    s.NoCNPS = !reader.IsDBNull(7) ? reader.GetString(7) : "";
                    s.Diplome = !reader.IsDBNull(8) ? reader.GetString(8) : "";
                    s.TypeContrat = !reader.IsDBNull(9) ? reader.GetString(9) : "";
                    s.Status = !reader.IsDBNull(10) ? reader.GetString(10) : "";
                    s.DateDepart = reader.GetDateTime(11);
                    s.IDDepartement = !reader.IsDBNull(12) ? reader.GetInt32(12) : 0;
                    s.IDDivision = !reader.IsDBNull(13) ? reader.GetInt32(13) : 0;
                    s.SalaireBrut = !reader.IsDBNull(14) ? reader.GetDouble(14) : 0;
                    s.Primes = !reader.IsDBNull(15) ? reader.GetDouble(15) : 0;
                    s.IDProjet = !reader.IsDBNull(16) ? reader.GetInt32(16) : 0;
                    s.Region = !reader.IsDBNull(17) ? reader.GetString(17) : "";
                    s.Localite = !reader.IsDBNull(18) ? reader.GetString(18) : "";
                    s.Grade = !reader.IsDBNull(19) ? reader.GetString(19) : "";
                    liste.Add(s);
                }
                return liste;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }
        public static List<Personnel> ListePersonnelProjet(int  id)
        {
            var liste = new List<Personnel>();
            try
            {
                var requete = "SELECT * FROM pers_projet_tbl WHERE id =" + id ;
                _command.CommandText = requete;
                _connection.Open();
                MySqlDataReader reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var p = new Personnel();
                    p.IDPersonelProjet = reader.GetInt32(0);
                    p.Nom = reader.GetString(1);
                    p.Prenom = reader.GetString(2);
                    p.DateNaissance = !reader.IsDBNull(3) ? reader.GetDateTime(3) : DateTime.Now;
                    p.LieuNaissance = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    p.Adresse = !reader.IsDBNull(5) ? reader.GetString(5) : "";
                    p.Telephone1 = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    p.Telephone2 = !reader.IsDBNull(7) ? reader.GetString(7) : "";
                    p.Email = !reader.IsDBNull(8) ? reader.GetString(8) : "";
                    p.Sexe = !reader.IsDBNull(9) ? reader.GetString(9) : "";
                    p.Photo = !reader.IsDBNull(10) ? reader.GetString(10) : "";
                    p.Age = !reader.IsDBNull(11) ? reader.GetInt32(11) : 0;
                    p.SituationMatrimoniale = !reader.IsDBNull(12) ? reader.GetString(12) : "";
                    p.NombreEnfant = !reader.IsDBNull(13) ? reader.GetInt32(13) : 0;
                    liste.Add(p);
                }
                return liste;
            }
            catch { return null; }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }
        public static List<Service> ListeServicePersonnelProjet(int idPer)
        {
            try
            {
                _connection.Open();
                var requete = "SELECT * FROM `service_projet_tbl` WHERE id_perso ="+idPer;
                _command.CommandText = requete;
                var reader = _command.ExecuteReader();
                var liste = new List<Service>();
                while (reader.Read())
                {
                    var s = new Service();
                    s.NumeroService = reader.GetInt32(0);
                    s.DateService = reader.GetDateTime(1);
                    s.Poste = reader.GetString(2);
                    s.IDPersonelProjet = reader.GetInt32(3);
                    s.Echelon = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    s.Categorie = !reader.IsDBNull(5) ? reader.GetString(5) : "";
                    s.Anciennete = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    s.NoCNPS = !reader.IsDBNull(7) ? reader.GetString(7) : "";
                    s.Diplome = !reader.IsDBNull(8) ? reader.GetString(8) : "";
                    s.TypeContrat = !reader.IsDBNull(9) ? reader.GetString(9) : "";
                    s.Status = !reader.IsDBNull(10) ? reader.GetString(10) : "";
                    s.DateDepart = reader.GetDateTime(11);
                    s.IDDepartement = !reader.IsDBNull(12) ? reader.GetInt32(12) : 0;
                    s.IDDivision = !reader.IsDBNull(13) ? reader.GetInt32(13) : 0;
                    s.SalaireBrut = !reader.IsDBNull(14) ? reader.GetDouble(14) : 0;
                    s.Primes = !reader.IsDBNull(15) ? reader.GetDouble(15) : 0;
                    s.IDProjet = !reader.IsDBNull(16) ? reader.GetInt32(16) : 0;
                    s.Region = !reader.IsDBNull(17) ? reader.GetString(17) : "";
                    s.Localite = !reader.IsDBNull(18) ? reader.GetString(18) : "";
                    s.Grade = !reader.IsDBNull(19) ? reader.GetString(19) : "";
                    liste.Add(s);
                }
                return liste;
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }
        public static bool SupprimerUnPersonnelProjet(int iD)
        {
            try
            {
                _connection.Open();
                var requete = "DELETE FROM pers_projet_tbl WHERE id = " + iD;
                _command.CommandText = requete;
                _command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("supp info", ex);
                return false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }
        #endregion

        #region MISSION
        public static bool EnregistrerUneMission(Mission mission)
        {
            try
            {
                _connection.Open();
                var requete = "";
                if (mission.IDMission  > 0)
                {
                    requete = "UPDATE mission_tbl SET objet = @objet,  destination=@destination,date_depart=@date_depart,date_retour=@date_retour, " +
                        "transport=@transport,imputation=@imputation ,exercice=@exercice, si_payant=@si_payant, etat=@etat WHERE id = " + mission.IDMission;

                }
                else
                {
                    requete = "INSERT INTO mission_tbl (objet,destination,date_depart,date_retour,transport,imputation,exercice,si_payant,etat)" +
                        " VALUES(@objet,@destination,@date_depart,@date_retour,@transport,@imputation,@exercice,@si_payant,@etat)";                  
                }

                _command.Parameters.Add(new MySqlParameter("objet", mission.Objet));
                _command.Parameters.Add(new MySqlParameter("destination", mission.Destination));
                _command.Parameters.Add(new MySqlParameter("date_depart", mission.DateDepart));
                _command.Parameters.Add(new MySqlParameter("date_retour", mission.DateRetour));
                _command.Parameters.Add(new MySqlParameter("transport", mission.Transport));
                _command.Parameters.Add(new MySqlParameter("imputation", mission.Imputation));
                _command.Parameters.Add(new MySqlParameter("exercice", mission.Exercice));
                _command.Parameters.Add(new MySqlParameter("si_payant", mission.SiPayant));
                _command.Parameters.Add(new MySqlParameter("etat", mission.Etat));
                _command.CommandText = requete;
                _command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("type contrat", ex);
                return false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }
        public static bool SupprimerUneMission(int iDTypeContrat)
        {
            try
            {
                _connection.Open();
                var requete = "DELETE mission_tbl WHERE id = " + iDTypeContrat;
                _command.CommandText = requete;
                _command.ExecuteNonQuery();
                return true;

            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("type contrat", ex);
                return false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }
        public static bool InsererPersonnelDansUneMission(Mission mission)
        {
            try
            {
                _connection.Open();
                _transaction = _connection.BeginTransaction();
                var requete = "SELECT * FROM mission_perso_tbl WHERE id_pers = @idPerss  AND id_mission = "+ mission.IDMission;
                _command.Parameters.Add(new MySqlParameter("idPerss", mission.Matricule));
                _command.CommandText = requete;
                _command.Transaction = _transaction;
                var reader = _command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Close();
                    requete = "UPDATE mission_perso_tbl SET role=@role,frais=@frais,duree=@duree,total=@total,nom=@nom,siProjet=@siProjet " +
                        "WHERE id_mission=@id_mission AND id_pers=@id_pers";
                    _command.Parameters.Add(new MySqlParameter("id_mission", mission.IDMission));
                    _command.Parameters.Add(new MySqlParameter("id_pers", mission.Matricule));
                    _command.Parameters.Add(new MySqlParameter("role", mission.Role));
                    _command.Parameters.Add(new MySqlParameter("frais", mission.Frais));
                    _command.Parameters.Add(new MySqlParameter("duree", mission.Durée));
                    _command.Parameters.Add(new MySqlParameter("total", mission.FraisTotal));
                    _command.Parameters.Add(new MySqlParameter("siProjet", mission.SiPersonnelProjet));
                    _command.Parameters.Add(new MySqlParameter("nom", mission.NomEmploye));
                    _command.CommandText = requete;
                    _command.Transaction = _transaction;
                    _command.ExecuteNonQuery();
                    _transaction.Commit();
                    return true;
                }
                else
                {
                    reader.Close();
                    requete = "INSERT INTO mission_perso_tbl (id_mission,id_pers,role,frais,duree,total,nom,siProjet,no) "+
                        "VALUES(@id_mission,@id_pers,@role,@frais,@duree,@total,@nom,@siProjet,@ordre)";
                    _command.Parameters.Add(new MySqlParameter("id_mission", mission.IDMission));
                    _command.Parameters.Add(new MySqlParameter("id_pers", mission.Matricule));
                    _command.Parameters.Add(new MySqlParameter("role", mission.Role));
                    _command.Parameters.Add(new MySqlParameter("frais", mission.Frais));
                    _command.Parameters.Add(new MySqlParameter("duree", mission.Durée));
                    _command.Parameters.Add(new MySqlParameter("total", mission.FraisTotal));
                    _command.Parameters.Add(new MySqlParameter("siProjet", mission.SiPersonnelProjet));
                    _command.Parameters.Add(new MySqlParameter("nom", mission.NomEmploye));
                    _command.Parameters.Add(new MySqlParameter("ordre", mission.Ordre));
                    _command.CommandText = requete;
                    _command.Transaction = _transaction;
                    _command.ExecuteNonQuery();
                    _transaction.Commit();
                    return true;
                }
            }
            catch (Exception ex)
            {
                if(_transaction !=null)
                _transaction.Rollback();
                MonMessageBox.ShowBox("type contrat", ex);
                return false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }
        public static bool RetirerPersonnelDansUneMission(Mission mission)
        {
            try
            {
                _connection.Open();
                var
                    requete = "DELETE FROM  mission_perso_tbl WHERE id_mission=@id_mission AND nom=@nom";
                _command.Parameters.Add(new MySqlParameter("id_mission", mission.IDMission));
                _command.Parameters.Add(new MySqlParameter("nom", mission.NomEmploye));
                _command.CommandText = requete;
                _command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("type contrat", ex);
                return false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }
        public static bool EtatPayementPersonnelDansUneMission(Mission mission)
        {
            try
            {
                _connection.Open();
                var
                    requete = "UPDATE mission_perso_tbl SET sidejaPaye=@sidejaPaye WHERE id_mission=@id_mission AND id_pers=@id_pers";
                _command.Parameters.Add(new MySqlParameter("id_mission", mission.IDMission));
                _command.Parameters.Add(new MySqlParameter("id_pers", mission.Matricule));
                _command.Parameters.Add(new MySqlParameter("sidejaPaye", mission.SiPayant));
                _command.CommandText = requete;
                _command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("type contrat", ex);
                return false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }
        public static bool OrdonnerLesPersonnelsDeLaMission(Mission mission)
        {
            try
            {
                _connection.Open();
                var
                    requete = "UPDATE mission_perso_tbl SET no="+mission.Ordre+"  WHERE id_mission=@id_mission AND id_pers=@id_pers";
                _command.Parameters.Add(new MySqlParameter("id_mission", mission.IDMission));
                _command.Parameters.Add(new MySqlParameter("id_pers", mission.Matricule));
                _command.CommandText = requete;
                _command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("type contrat", ex);
                return false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }
        public static bool MettreFinLaMission(Mission mission)
        {
            try
            {
                _connection.Open();
                var
                    requete = "UPDATE mission_perso_tbl SET sidejaPaye=@sidejaPaye WHERE id_mission=@id_mission AND id_pers=@id_pers";
                _command.Parameters.Add(new MySqlParameter("id_mission", mission.IDMission));
                _command.Parameters.Add(new MySqlParameter("id_pers", mission.Matricule));
                _command.Parameters.Add(new MySqlParameter("sidejaPaye", mission.SiPayant));
                _command.CommandText = requete;
                _command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("type contrat", ex);
                return false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }
     
        public static List<Mission> ListeMisson()
        {
            try
            {
                var liste = new List<Mission>();
                _connection.Open();
                var requete = "SELECT * FROM mission_tbl ORDER BY date_depart DESC";
                _command.CommandText = requete;
                var reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var m = new Mission();
                    m.IDMission = reader.GetInt32(0);
                    m.Objet = reader.GetString(1);
                    m.Destination = reader.GetString(2);
                    m.DateDepart = reader.GetDateTime(3);
                    m.DateRetour = reader.GetDateTime(4);
                    m.Transport = reader.GetString(5);
                    m.Imputation = reader.GetString(6);
                    m.Exercice = reader.GetInt32(7);
                    m.SiPayant = reader.GetBoolean(8);
                    m.Etat = reader.GetString(9);
                    liste.Add(m);
                }
                return liste;
            }
            catch { return null; }
            finally { _connection.Close(); }
        }
       
        public static List<Mission> ListeMisson(int id)
        {
            try
            {
                var liste = new List<Mission>();
                _connection.Open();
                var requete = "SELECT * FROM mission_perso_tbl WHERE id_mission="+id + " ORDER BY no";
                _command.CommandText = requete;
                var reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var m = new Mission();
                    m.IDMission = reader.GetInt32(0);
                    m.Matricule =!reader.IsDBNull(1) ? reader.GetString(1) : "";
                    m.Role = reader.GetString(2);
                    m.Frais = reader.GetDouble(3);
                    m.Durée = reader.GetInt32(4);
                    m.FraisTotal = reader.GetInt32(5);
                    m.SiPayant = !reader.IsDBNull(6) ? reader.GetBoolean(6) : false;
                    m.NomEmploye = !reader.IsDBNull(7) ? reader.GetString(7) : "";
                    m.SiPersonnelProjet = !reader.IsDBNull(8) ? reader.GetBoolean(8) : false;
                    m.Ordre = !reader.IsDBNull(9) ? reader.GetInt32(9) : 0;
                    liste.Add(m);
                }
                return liste;
            }
            catch { return null; }
            finally { _connection.Close(); }
        }

       
        #endregion


        #region DEMANDE
        public static bool EnregistrerUneDemande(Demande demande)
        {
            try
            {
                _connection.Open();
                var requete = "";
                if (demande.NumeroDemande > 0)
                {
                    requete = "UPDATE demande_tbl SET `date_dem`=@date_dem,`nat_dem`=@nat_dem,`diplome`=@diplome,`poste`=@poste,`nom`=@nom,email=@email,email=@email,experience=@experience" +
                    ",`disponibilite`=@disponibilite,`exercice`=@exercice,`qualification`=@qualification,`domaine`=@domaine, telephone=@telephone WHERE id_dem = " + demande.NumeroDemande;
                   
                }
                else
                {
                    requete = "INSERT INTO `demande_tbl`(`date_dem`,`nat_dem`,`diplome`,`poste`,`nom`,`disponibilite`,`exercice`,`qualification`,`domaine`,`email`,`sexe`,`telephone`,experience)" +
                    " VALUES(@date_dem,@nat_dem,@diplome,@poste,@nom,@disponibilite,@exercice,@qualification,@domaine,@email,@sexe,@telephone,@experience)";
                  
                }
                _command.Parameters.Add(new MySqlParameter("date_dem", demande.DateDemande));
                _command.Parameters.Add(new MySqlParameter("nat_dem", demande.NatureStage));
                _command.Parameters.Add(new MySqlParameter("diplome", demande.Diplome));
                _command.Parameters.Add(new MySqlParameter("poste", demande.Poste));
                _command.Parameters.Add(new MySqlParameter("nom", demande.Nom));
                _command.Parameters.Add(new MySqlParameter("duree", demande.Duree));
                _command.Parameters.Add(new MySqlParameter("disponibilite", demande.Disponibilite));
                _command.Parameters.Add(new MySqlParameter("exercice", demande.Exercice));
                _command.Parameters.Add(new MySqlParameter("qualification", demande.Qualification));
                _command.Parameters.Add(new MySqlParameter("domaine", demande.DomaineEtude));
                _command.Parameters.Add(new MySqlParameter("sexe", demande.Sexe));
                _command.Parameters.Add(new MySqlParameter("email", demande.Email));
                _command.Parameters.Add(new MySqlParameter("telephone", demande.Telephone));
                _command.Parameters.Add(new MySqlParameter("experience", demande.Experience));
                _command.CommandText = requete;
                _command.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("type contrat", ex);
                return false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }
        public static bool SupprimerUneDemande(int iD)
        {
            try
            {
                _connection.Open();
                var requete = "DELETE FROM demande_tbl WHERE id_dem = " + iD;
                _command.CommandText = requete;
                _command.ExecuteNonQuery();
                return true;

            }
            catch (Exception ex)
            {
                MonMessageBox.ShowBox("type contrat", ex);
                return false;
            }
            finally
            {
                _connection.Close();
                _command.Parameters.Clear();
            }
        }

        public static List<Demande > ListeDemande()
        {
            try
            {
                var liste = new List<Demande >();
                _connection.Open();
                var requete = "SELECT * FROM demande_tbl ORDER BY  date_dem";
                _command.CommandText = requete;
                var reader = _command.ExecuteReader();
                while (reader.Read())
                {
                    var dem = new Demande();
                    dem.NumeroDemande = reader.GetInt32(0);
                    dem.DateDemande = reader.GetDateTime(1);
                    dem.NatureStage = !reader.IsDBNull(2) ? reader.GetString(2) : "";
                    dem.Diplome = !reader.IsDBNull(3) ? reader.GetString(3) : "";
                    dem.Poste = !reader.IsDBNull(4) ? reader.GetString(4) : "";
                    dem.Nom = !reader.IsDBNull(5) ? reader.GetString(5) : "";
                    dem.Disponibilite = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                    dem.Exercice = !reader.IsDBNull(7) ? reader.GetInt32(7) : 0;
                    dem.Qualification = !reader.IsDBNull(8) ? reader.GetString(8) : "";
                    dem.DomaineEtude = !reader.IsDBNull(9) ? reader.GetString(9) : "";
                    dem.Sexe = !reader.IsDBNull(10) ? reader.GetString(10) : "";
                    dem.Telephone = !reader.IsDBNull(11) ? reader.GetString(11) : ""; 
                    dem.Email = !reader.IsDBNull(12) ? reader.GetString(12) : "";
                    dem.Experience = reader.GetInt32(13);
                    liste.Add(dem);
                }
                return liste;
            }
            catch { return null; }
            finally { _connection.Close(); }
        }
        #endregion
 
    }
}
