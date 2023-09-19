namespace SGSP.Formes
{
    partial class ListeFraisCongeFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListeFraisCongeFrm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblMontant = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbAnnee = new System.Windows.Forms.ComboBox();
            this.cmbMois = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.btnApercu = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.dgvConge = new System.Windows.Forms.DataGridView();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clDesidgnation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clPEmploye = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clRemise = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clQte = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnFermer = new System.Windows.Forms.Button();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConge)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.SteelBlue;
            this.groupBox2.Controls.Add(this.lblMontant);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox2.Location = new System.Drawing.Point(0, 320);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(890, 40);
            this.groupBox2.TabIndex = 134;
            this.groupBox2.TabStop = false;
            this.groupBox2.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox2_Paint);
            // 
            // lblMontant
            // 
            this.lblMontant.AutoSize = true;
            this.lblMontant.BackColor = System.Drawing.Color.Transparent;
            this.lblMontant.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMontant.ForeColor = System.Drawing.Color.White;
            this.lblMontant.Location = new System.Drawing.Point(162, 8);
            this.lblMontant.Name = "lblMontant";
            this.lblMontant.Size = new System.Drawing.Size(0, 24);
            this.lblMontant.TabIndex = 127;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(12, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 20);
            this.label4.TabIndex = 126;
            this.label4.Text = "MONTANT TOTAL :";
            // 
            // cmbAnnee
            // 
            this.cmbAnnee.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbAnnee.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbAnnee.FormattingEnabled = true;
            this.cmbAnnee.Location = new System.Drawing.Point(732, 63);
            this.cmbAnnee.Name = "cmbAnnee";
            this.cmbAnnee.Size = new System.Drawing.Size(92, 28);
            this.cmbAnnee.TabIndex = 133;
            this.cmbAnnee.SelectedIndexChanged += new System.EventHandler(this.cmbAnnee_SelectedIndexChanged);
            this.cmbAnnee.Click += new System.EventHandler(this.cmbAnnee_SelectedIndexChanged);
            // 
            // cmbMois
            // 
            this.cmbMois.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMois.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbMois.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMois.FormattingEnabled = true;
            this.cmbMois.Items.AddRange(new object[] {
            "Janvier",
            "Fevrier",
            "Mars",
            "Avril",
            "Mai",
            "Juin",
            "Juillet",
            "Aout",
            "Septembre",
            "Octobre",
            "Novembre",
            "Decembre"});
            this.cmbMois.Location = new System.Drawing.Point(555, 63);
            this.cmbMois.Name = "cmbMois";
            this.cmbMois.Size = new System.Drawing.Size(111, 28);
            this.cmbMois.TabIndex = 132;
            this.cmbMois.SelectedIndexChanged += new System.EventHandler(this.cmbMois_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(374, 71);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 20);
            this.label1.TabIndex = 131;
            this.label1.Text = "RECHERCHER PAR MOIS :";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.SteelBlue;
            this.button2.FlatAppearance.BorderColor = System.Drawing.Color.Navy;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(6, 50);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(119, 41);
            this.button2.TabIndex = 130;
            this.button2.Text = "NOUVEAU ";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btnApercu
            // 
            this.btnApercu.BackColor = System.Drawing.Color.SteelBlue;
            this.btnApercu.FlatAppearance.BorderColor = System.Drawing.Color.Navy;
            this.btnApercu.FlatAppearance.BorderSize = 0;
            this.btnApercu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnApercu.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApercu.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnApercu.Image = ((System.Drawing.Image)(resources.GetObject("btnApercu.Image")));
            this.btnApercu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnApercu.Location = new System.Drawing.Point(255, 50);
            this.btnApercu.Name = "btnApercu";
            this.btnApercu.Size = new System.Drawing.Size(113, 41);
            this.btnApercu.TabIndex = 129;
            this.btnApercu.Text = "APERÇU";
            this.btnApercu.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnApercu.UseVisualStyleBackColor = false;
            this.btnApercu.Click += new System.EventHandler(this.btnAjouter_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.SteelBlue;
            this.button1.FlatAppearance.BorderColor = System.Drawing.Color.Navy;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(126, 50);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(127, 41);
            this.button1.TabIndex = 128;
            this.button1.Text = "SUPPRIMER";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dgvConge
            // 
            this.dgvConge.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.DeepSkyBlue;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvConge.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvConge.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvConge.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvConge.BackgroundColor = System.Drawing.Color.White;
            this.dgvConge.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvConge.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.RaisedVertical;
            this.dgvConge.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(10, 10, 0, 5);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvConge.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvConge.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvConge.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column7,
            this.clDesidgnation,
            this.clPEmploye,
            this.clRemise,
            this.Column1,
            this.clQte});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.PaleTurquoise;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvConge.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvConge.EnableHeadersVisualStyles = false;
            this.dgvConge.GridColor = System.Drawing.Color.White;
            this.dgvConge.Location = new System.Drawing.Point(6, 97);
            this.dgvConge.Name = "dgvConge";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.AliceBlue;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvConge.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvConge.RowHeadersVisible = false;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.InactiveBorder;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.DeepSkyBlue;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvConge.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvConge.RowTemplate.DefaultCellStyle.NullValue = null;
            this.dgvConge.RowTemplate.Height = 25;
            this.dgvConge.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvConge.Size = new System.Drawing.Size(878, 219);
            this.dgvConge.TabIndex = 127;
            this.dgvConge.DoubleClick += new System.EventHandler(this.dgvConge_DoubleClick);
            // 
            // Column7
            // 
            this.Column7.HeaderText = "NUMERO";
            this.Column7.Name = "Column7";
            // 
            // clDesidgnation
            // 
            this.clDesidgnation.HeaderText = "ID";
            this.clDesidgnation.Name = "clDesidgnation";
            this.clDesidgnation.Visible = false;
            // 
            // clPEmploye
            // 
            this.clPEmploye.HeaderText = "EMPLOYE";
            this.clPEmploye.Name = "clPEmploye";
            this.clPEmploye.ReadOnly = true;
            // 
            // clRemise
            // 
            this.clRemise.HeaderText = "MONTANT";
            this.clRemise.Name = "clRemise";
            // 
            // Column1
            // 
            this.Column1.HeaderText = "ANNEE";
            this.Column1.Name = "Column1";
            // 
            // clQte
            // 
            this.clQte.HeaderText = "MOIS";
            this.clQte.Name = "clQte";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.SteelBlue;
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnFermer);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(890, 40);
            this.groupBox1.TabIndex = 126;
            this.groupBox1.TabStop = false;
            this.groupBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox1_Paint);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Ubuntu", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(12, -1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(365, 34);
            this.label3.TabIndex = 0;
            this.label3.Text = "Gestion des frais de congés";
            // 
            // btnFermer
            // 
            this.btnFermer.BackColor = System.Drawing.Color.Transparent;
            this.btnFermer.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnFermer.BackgroundImage")));
            this.btnFermer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnFermer.FlatAppearance.BorderColor = System.Drawing.Color.Navy;
            this.btnFermer.FlatAppearance.BorderSize = 0;
            this.btnFermer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFermer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFermer.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnFermer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFermer.Location = new System.Drawing.Point(838, 2);
            this.btnFermer.Name = "btnFermer";
            this.btnFermer.Size = new System.Drawing.Size(46, 37);
            this.btnFermer.TabIndex = 106;
            this.btnFermer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFermer.UseVisualStyleBackColor = false;
            this.btnFermer.Click += new System.EventHandler(this.btnFermer_Click);
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            this.printPreviewDialog1.Load += new System.EventHandler(this.printPreviewDialog1_Load);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(672, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 20);
            this.label2.TabIndex = 135;
            this.label2.Text = "ANNEE";
            // 
            // ListeFraisCongeFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(890, 360);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.cmbAnnee);
            this.Controls.Add(this.cmbMois);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnApercu);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dgvConge);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "ListeFraisCongeFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ListeFraisConge";
            this.Load += new System.EventHandler(this.ListeFraisCongeFrm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ListeFraisCongeFrm_Paint);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConge)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblMontant;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.ComboBox cmbAnnee;
        public System.Windows.Forms.ComboBox cmbMois;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btnApercu;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dgvConge;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnFermer;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn clDesidgnation;
        private System.Windows.Forms.DataGridViewTextBoxColumn clPEmploye;
        private System.Windows.Forms.DataGridViewTextBoxColumn clRemise;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn clQte;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.Label label2;
    }
}