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
    public partial class DossierForm : Form
    {
        public DossierForm()
        {
            InitializeComponent();
        }

        public string nomPersonnel,     stateTemp , state, typeDocument, rootPath;

        private void DossierForm_Load(object sender, EventArgs e)
        {
            try
            {
                if (GestionAcademique.LoginFrm.typeUtilisateur == "utilisateur")
                {
                    Column1.Visible = false;
                    Column2.Visible = false;
                    Column6.Width = 80;
                }
                else
                {
                    Column6.Width = 60;
                    Column1.Width = 60;
                    Column2.Width = 60;
                }
                dataGridView1.RowTemplate.Height = 40;
            
                if (state == "1")
                {
                    label1.Text = "Dossiers du personnel " + nomPersonnel;
                    if(stateTemp =="1")
                    rootPath = AppCode.GlobalVariable.rootPathPersonnel + nomPersonnel;
                    else if (stateTemp == "2")
                        rootPath = AppCode.GlobalVariable.rootPathPersonnelTempo + nomPersonnel;
                }
                else if (state == "2")
                {
                    label1.Text = "Document de  " + typeDocument;
                    rootPath = AppCode.GlobalVariable.rootPathDocuments + typeDocument;
                }
                // Put all file names in root directory into array.
                string[] array1 = System.IO.Directory.GetFiles(rootPath);

                //Console.WriteLine("--- Files: ---");
                foreach (string name in array1)
                {
                    var name1 = name.Substring(name.LastIndexOf(@"\") + 1);
                    dataGridView1.Rows.Add(name1);
                    Column6.Image = global::SGSP.Properties.Resources.index15;
                    Column1.Image = global::SGSP.Properties.Resources.edit;
                    Column2.Image = global::SGSP.Properties.Resources.DeleteRed1;
                    if (stateTemp == "2")
                    {
                        Column1.Image = global::SGSP.Properties.Resources.Stock_Index_Up0;
                    }
                }
              
            }
            catch (Exception)
            {
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1)
                {
                    if (state == "1")
                    {
                        rootPath = AppCode.GlobalVariable.rootPathPersonnel + nomPersonnel + "\\" + dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(); ;
                    }
                    else if (state == "2")
                    {
                        rootPath = AppCode.GlobalVariable.rootPathDocuments + typeDocument + "\\" + dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(); ;
                    }
                    //var rootPath = AppCode.GlobalVariable.rootPathPersonnel + nomPersonnel + "//" + dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    if (GestionAcademique.LoginFrm.typeUtilisateur == "utilisateur")
                    {
                        System.IO.File.SetAttributes(rootPath, System.IO.FileAttributes.ReadOnly);
                        System.Diagnostics.Process.Start(rootPath);
                    }
                    else
                    {
                        System.Diagnostics.Process.Start(rootPath);
                    }
                }
                else if (e.ColumnIndex == 2)
                {
                    if (stateTemp == "2")
                    {
                        TransfertFichiers();
                    }
                    else
                    {
                        textBox1.Visible = true;
                        label2.Visible = true;
                        dataGridView1.Height = 375;
                        rowIndex = dataGridView1.CurrentRow.Index;
                        //textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                        textBox1.Focus();
                    }
                }
                else if (e.ColumnIndex == 3)
                {
                    if (GestionPharmacetique.MonMessageBox.ShowBox("Voulez vous supprimer ce fichier?", "Confirmation") == "1")
                    {
                        if (state == "1")
                        {
                            rootPath = AppCode.GlobalVariable.rootPathPersonnel + nomPersonnel + "\\" + dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(); ;
                        }
                        else if (state == "2")
                        {
                            rootPath = AppCode.GlobalVariable.rootPathDocuments + typeDocument + "\\" + dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(); ;
                        }
                        System.IO.File.Delete(rootPath);
                        GestionPharmacetique.MonMessageBox.ShowBox("Fichier supprimer avec succés", "Affirmation");
                        dataGridView1.Rows.Remove(dataGridView1.Rows[e.RowIndex]);
                    }
                }
            }
            catch(Exception ex )
            {
                GestionPharmacetique.MonMessageBox.ShowBox("", ex);
            }
        }
        int rowIndex;
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    if (state == "1")
                    {
                        rootPath = AppCode.GlobalVariable.rootPathPersonnel + nomPersonnel + "//" + dataGridView1.Rows[rowIndex].Cells[0].Value.ToString(); ;
                    }
                    else if (state == "2")
                    {
                        rootPath = AppCode.GlobalVariable.rootPathDocuments + typeDocument + "//" + dataGridView1.Rows[rowIndex].Cells[0].Value.ToString(); ;
                    } 
                    var nomFichier = textBox1.Text + rootPath.Substring(rootPath.LastIndexOf("."));
                        var rootPath1="";

                        if (state == "1")
                        {
                           rootPath1= AppCode.GlobalVariable.rootPathPersonnel + nomPersonnel + "//" + nomFichier;
                        }
                        else if (state == "2")
                        {
                            rootPath1 = AppCode.GlobalVariable.rootPathDocuments + typeDocument + "//" + nomFichier;
                        } 
                        System.IO.File.Move(rootPath, rootPath1);
                        dataGridView1.CurrentRow.Cells[0].Value = nomFichier;
                        textBox1.Visible = false;
                        label2.Visible = false;
                        textBox1.Text = "";
                        dataGridView1.Height = 438;
                        textBox1.Focus(); 
                }
                catch (Exception ) { }
            }
        }

        void TransfertFichiers()
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    if (string.IsNullOrWhiteSpace(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()))
                    {
                        return;
                    }
                    var nomFichier = dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
                    var   initialRootPath =AppCode. GlobalVariable.rootPathPersonnelTempo + nomPersonnel+"//"+nomFichier;
                    var finalRootPath = AppCode.GlobalVariable.rootPathPersonnel + nomPersonnel;

                    if (!System.IO.Directory.Exists(finalRootPath))
                    {
                        System.IO.Directory.CreateDirectory(rootPath);
                    }
                    finalRootPath = finalRootPath + "//" +nomFichier;
                    if (!System.IO.File.Exists(finalRootPath))
                    {
                        System.IO.File.Move(initialRootPath, finalRootPath);
                        GestionPharmacetique.MonMessageBox.ShowBox("fichier transferé avec succés ", "Information fichier");
                        dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);
                    }
                    else
                    {
                        if (GestionPharmacetique.MonMessageBox.ShowBox("Ce fichier existe deja voulez vous continuer à le transferer ?", "Confirmation") == "1")
                        {
                            finalRootPath = finalRootPath.Substring(0, finalRootPath.LastIndexOf(".")) + "_copie_" + finalRootPath.Substring(finalRootPath.LastIndexOf("."));
                            GestionPharmacetique.MonMessageBox.ShowBox("fichier transferé avec succés ", "Information fichier");
                            dataGridView1.Rows.Remove(dataGridView1.SelectedRows[0]);
                        }
                    }
                }
                else
                {
                 GestionPharmacetique.   MonMessageBox.ShowBox("Veuillez selectionner les données du personnel. Puis réessayez ", "Information Photo");
                }
            }

            catch (Exception ex) { GestionPharmacetique.MonMessageBox.ShowBox("Transfert de l'image", ex); }
        }
        
    }
}
