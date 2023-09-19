namespace SGSP.Formes
{
    partial class ListeFinEtudesFrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListeFinEtudesFrm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvAcompte = new System.Windows.Forms.DataGridView();
            this.btnAjouterUneAgence = new System.Windows.Forms.Button();
            this.clPEmploye = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cldATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clRemise = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clQte = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clPrixTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAcompte)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvAcompte
            // 
            this.dgvAcompte.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.DeepSkyBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvAcompte.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAcompte.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvAcompte.BackgroundColor = System.Drawing.Color.White;
            this.dgvAcompte.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvAcompte.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SunkenVertical;
            this.dgvAcompte.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial Narrow", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(10, 10, 0, 5);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAcompte.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvAcompte.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAcompte.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clPEmploye,
            this.cldATE,
            this.Column1,
            this.clRemise,
            this.clQte,
            this.clPrixTotal,
            this.Column2});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.PaleTurquoise;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Arial Narrow", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAcompte.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvAcompte.EnableHeadersVisualStyles = false;
            this.dgvAcompte.GridColor = System.Drawing.Color.White;
            this.dgvAcompte.Location = new System.Drawing.Point(3, 49);
            this.dgvAcompte.Name = "dgvAcompte";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.AliceBlue;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Arial Narrow", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAcompte.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvAcompte.RowHeadersVisible = false;
            dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.InactiveBorder;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.DeepSkyBlue;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvAcompte.RowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvAcompte.RowTemplate.DefaultCellStyle.NullValue = null;
            this.dgvAcompte.RowTemplate.Height = 25;
            this.dgvAcompte.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAcompte.Size = new System.Drawing.Size(922, 354);
            this.dgvAcompte.TabIndex = 62;
            // 
            // btnAjouterUneAgence
            // 
            this.btnAjouterUneAgence.BackColor = System.Drawing.Color.Transparent;
            this.btnAjouterUneAgence.FlatAppearance.BorderColor = System.Drawing.Color.Navy;
            this.btnAjouterUneAgence.FlatAppearance.BorderSize = 0;
            this.btnAjouterUneAgence.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAjouterUneAgence.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAjouterUneAgence.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnAjouterUneAgence.Image = ((System.Drawing.Image)(resources.GetObject("btnAjouterUneAgence.Image")));
            this.btnAjouterUneAgence.Location = new System.Drawing.Point(856, 6);
            this.btnAjouterUneAgence.Name = "btnAjouterUneAgence";
            this.btnAjouterUneAgence.Size = new System.Drawing.Size(69, 37);
            this.btnAjouterUneAgence.TabIndex = 110;
            this.btnAjouterUneAgence.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnAjouterUneAgence.UseVisualStyleBackColor = false;
            // 
            // clPEmploye
            // 
            this.clPEmploye.HeaderText = "Etudiant";
            this.clPEmploye.Name = "clPEmploye";
            this.clPEmploye.ReadOnly = true;
            this.clPEmploye.Width = 250;
            // 
            // cldATE
            // 
            this.cldATE.HeaderText = "Université";
            this.cldATE.Name = "cldATE";
            this.cldATE.Width = 150;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Niveau";
            this.Column1.Name = "Column1";
            // 
            // clRemise
            // 
            this.clRemise.HeaderText = "Filière";
            this.clRemise.Name = "clRemise";
            this.clRemise.Width = 150;
            // 
            // clQte
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.clQte.DefaultCellStyle = dataGridViewCellStyle3;
            this.clQte.HeaderText = "Debut";
            this.clQte.Name = "clQte";
            this.clQte.Width = 75;
            // 
            // clPrixTotal
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            this.clPrixTotal.DefaultCellStyle = dataGridViewCellStyle4;
            this.clPrixTotal.HeaderText = "Fin";
            this.clPrixTotal.Name = "clPrixTotal";
            this.clPrixTotal.ReadOnly = true;
            this.clPrixTotal.Width = 75;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Année scol";
            this.Column2.Name = "Column2";
            this.Column2.Width = 120;
            // 
            // ListeFinEtudesFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(928, 415);
            this.Controls.Add(this.btnAjouterUneAgence);
            this.Controls.Add(this.dgvAcompte);
            this.Font = new System.Drawing.Font("Arial Narrow", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.Name = "ListeFinEtudesFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Liste des etudiants en fin d\' études";
            this.Load += new System.EventHandler(this.ListeFinEtudesFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAcompte)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvAcompte;
        private System.Windows.Forms.Button btnAjouterUneAgence;
        private System.Windows.Forms.DataGridViewTextBoxColumn clPEmploye;
        private System.Windows.Forms.DataGridViewTextBoxColumn cldATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn clRemise;
        private System.Windows.Forms.DataGridViewTextBoxColumn clQte;
        private System.Windows.Forms.DataGridViewTextBoxColumn clPrixTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    }
}