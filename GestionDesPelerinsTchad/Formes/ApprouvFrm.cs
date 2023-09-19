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
    public partial class ApprouvFrm : Form
    {
        public ApprouvFrm()
        {
            InitializeComponent();
        }
        private void ApprouvFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue, 2);
            Rectangle area1 = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                SystemColors.Control, Color.White, LinearGradientMode.Vertical);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics mGraphics = e.Graphics;
            Pen pen1 = new Pen(Color.SteelBlue, 2);
            Rectangle area1 = new Rectangle(0, 0, this.groupBox1.Width - 1, this.groupBox1.Height - 1);
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(area1,
                Color.SteelBlue, Color.SteelBlue, LinearGradientMode.ForwardDiagonal);
            mGraphics.FillRectangle(linearGradientBrush, area1);
            mGraphics.DrawRectangle(pen1, area1);
        }
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                
                   var numDoc = Convert.ToInt32(lblNoDoc.Text);
                    if (ListePerso.ShowBox() == "1")
                    {
                        if (dataGridView2.Rows.Count > 0)
                        {
                            for (var i = 0; i < dataGridView2.Rows.Count; i++)
                            {
                                if (dataGridView2.Rows[i].Cells[1].Value.ToString() == ListePerso.numerMatricule)
                                {
                                    return;
                                }
                            }
                        }
                        if (GestionPharmacetique.MonMessageBox.ShowBox("Voulez vous ajouter " + ListePerso.nomPersonnel +
                            " comme approbateur de ce document à référence " + lblRef.Text + "?", "Confirmation") == "1")
                        {
                            var document = new AppCode.Document();
                            document.NumeroDocument = Convert.ToInt32(lblNoDoc.Text);
                            document.Matricule = ListePerso.numerMatricule;
                            document.Approbation = 0;
                            if (AppCode.ConnectionClass.AjouterUnApprobateur(document))
                            {
                                dataGridView2.Rows.Add(
                                  AppCode.ConnectionClass. DernierApprobation(),
                                    ListePerso.numerMatricule,
                                    ListePerso.nomPersonnel,
                                    ListePerso.fonction
                                  );
                                clStatus.Image = document.ApprobationDeDocument(document.Approbation);
                                clStatus.HeaderText = document.ApprobationDeDocumentText(document.Approbation);
                                 clOk.Image = global::SGSP.Properties.Resources.ok;
                                 clDeny.Image = global::SGSP.Properties.Resources.deny;
                            }
                        }
                    }
                
            }
            catch (Exception)
            {
            }
        }

        private void btnFermer_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void ApprouvFrm_Load(object sender, EventArgs e)
        {
            try
            {
                dataGridView2.RowTemplate.Height = 30;
                ListeDesApprobations();
            }
            catch (Exception)
            {
            }
                      
        }

        void ListeDesApprobations()
        {
            try
            {
                dataGridView2.Rows.Clear();
                var liste = AppCode.ConnectionClass.ListeDesApprobations(Int32.Parse(lblNoDoc.Text));
                foreach (var a in liste)
                {
                    var personnel = AppCode.ConnectionClass.ListePersonnelParMatricule(a.Matricule);
                    var service = AppCode.ConnectionClass.ListeServicePersonnel(a.Matricule);
                    dataGridView2.Rows.Add(a.NumeroType,
                        a.Matricule,
                        personnel[0].Nom ,personnel[1].Prenom,
                        service[0].Poste,
                          clStatus.Image=a.ApprobationDeDocument(a.Approbation)
                     );
                    //if (a.Approbation == 1)
                    //{
                    //    clStatus.Image = global::GestionDesPelerinsTchad.Properties.Resources.ok;
                    //}
                    //else if (a.Approbation == 2)
                    //{
                    //    clStatus.Image = global::GestionDesPelerinsTchad.Properties.Resources.deny;
                    //}
                    //else if (a.Approbation == 0 )
                    //{
                    //    clStatus.Image = global::GestionDesPelerinsTchad.Properties.Resources.pending;
                    //}

                    //clStatus.HeaderText = a.ApprobationDeDocumentText(a.Approbation);
                    clOk.Image = global::SGSP.Properties.Resources.ok;
                    clDeny.Image = global::SGSP.Properties.Resources.deny;

                }
               
            }
            catch (Exception)
            {
            }
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            clOk.Visible = true;
            clDeny.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count > 0)
            {
                var num= Convert.ToInt32(dataGridView2.SelectedRows[0].Cells[0].Value.ToString());
                if(AppCode.ConnectionClass.SupprimerUneApprobation(num))
                {
                    dataGridView2.Rows.Remove(dataGridView2.SelectedRows[0]);
                }
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var doc = new AppCode.Document();
            var index = dataGridView2.CurrentRow.Index;
            doc.NumeroType = Convert.ToInt32(dataGridView2.Rows[index].Cells[0].Value.ToString());
            if (e.ColumnIndex == 4)
            {
                clOk.Visible = true;
                clDeny.Visible = true;
            }
            else if (e.ColumnIndex == 5)
            {
                doc.Approbation = 1;
                if (AppCode.ConnectionClass.DonnerUneApprobation(doc))
                {
                    ListeDesApprobations();
                    clOk.Visible = false;
                    clDeny.Visible = false;
                }
            }
            else if (e.ColumnIndex == 6)
            {
                doc.Approbation = 2;
                if (AppCode.ConnectionClass.DonnerUneApprobation(doc))
                {
                 ListeDesApprobations();
                    clOk.Visible = false;
                    clDeny.Visible = false;
                }
            }
        }
    }
}
