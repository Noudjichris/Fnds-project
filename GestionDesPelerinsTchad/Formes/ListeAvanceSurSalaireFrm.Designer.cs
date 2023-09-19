namespace SGSP.Formes
{
    partial class ListeAvanceSurSalaireFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ListeAvanceSurSalaireFrm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnFermer = new System.Windows.Forms.Button();
            this.dgvAcompte = new System.Windows.Forms.DataGridView();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clDesidgnation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clPEmploye = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cldATE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clRemise = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clQte = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clPrixTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnAjouterUneAgence = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.txtDeduction = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbAnnnee = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbMois = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAcompte)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.SteelBlue;
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnFermer);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1113, 46);
            this.groupBox1.TabIndex = 60;
            this.groupBox1.TabStop = false;
            this.groupBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox1_Paint);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(42, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(431, 31);
            this.label3.TabIndex = 0;
            this.label3.Text = "Gestion des avances sur salaire";
            // 
            // btnFermer
            // 
            this.btnFermer.BackColor = System.Drawing.Color.Transparent;
            this.btnFermer.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnFermer.BackgroundImage")));
            this.btnFermer.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnFermer.FlatAppearance.BorderColor = System.Drawing.Color.Navy;
            this.btnFermer.FlatAppearance.BorderSize = 0;
            this.btnFermer.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnFermer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnFermer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFermer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFermer.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnFermer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFermer.Location = new System.Drawing.Point(942, 3);
            this.btnFermer.Name = "btnFermer";
            this.btnFermer.Size = new System.Drawing.Size(46, 37);
            this.btnFermer.TabIndex = 106;
            this.btnFermer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFermer.UseVisualStyleBackColor = false;
            this.btnFermer.Click += new System.EventHandler(this.btnFermer_Click);
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
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(10, 10, 0, 5);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAcompte.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvAcompte.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAcompte.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column7,
            this.clDesidgnation,
            this.clPEmploye,
            this.cldATE,
            this.clRemise,
            this.Column1,
            this.clQte,
            this.clPrixTotal,
            this.Column2});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.PaleTurquoise;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAcompte.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvAcompte.EnableHeadersVisualStyles = false;
            this.dgvAcompte.GridColor = System.Drawing.Color.White;
            this.dgvAcompte.Location = new System.Drawing.Point(3, 99);
            this.dgvAcompte.Name = "dgvAcompte";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.AliceBlue;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAcompte.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dgvAcompte.RowHeadersVisible = false;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.InactiveBorder;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.DeepSkyBlue;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvAcompte.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvAcompte.RowTemplate.DefaultCellStyle.NullValue = null;
            this.dgvAcompte.RowTemplate.Height = 25;
            this.dgvAcompte.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAcompte.Size = new System.Drawing.Size(1107, 331);
            this.dgvAcompte.TabIndex = 61;
            this.dgvAcompte.DoubleClick += new System.EventHandler(this.dgvAcompte_DoubleClick);
            // 
            // Column7
            // 
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Column7.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column7.HeaderText = "NUMERO";
            this.Column7.Name = "Column7";
            this.Column7.Width = 161;
            // 
            // clDesidgnation
            // 
            this.clDesidgnation.HeaderText = "ID";
            this.clDesidgnation.Name = "clDesidgnation";
            this.clDesidgnation.Visible = false;
            this.clDesidgnation.Width = 215;
            // 
            // clPEmploye
            // 
            this.clPEmploye.HeaderText = "EMPLOYE";
            this.clPEmploye.Name = "clPEmploye";
            this.clPEmploye.ReadOnly = true;
            this.clPEmploye.Width = 214;
            // 
            // cldATE
            // 
            this.cldATE.HeaderText = "DATE";
            this.cldATE.Name = "cldATE";
            this.cldATE.Width = 215;
            // 
            // clRemise
            // 
            this.clRemise.HeaderText = "MONTANT";
            this.clRemise.Name = "clRemise";
            this.clRemise.Width = 215;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "DEDUCTION";
            this.Column1.Name = "Column1";
            // 
            // clQte
            // 
            this.clQte.HeaderText = "PAYE";
            this.clQte.Name = "clQte";
            this.clQte.Width = 214;
            // 
            // clPrixTotal
            // 
            this.clPrixTotal.HeaderText = "SOLDE";
            this.clPrixTotal.Name = "clPrixTotal";
            this.clPrixTotal.ReadOnly = true;
            this.clPrixTotal.Width = 215;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "MODE PAIEMENT";
            this.Column2.Name = "Column2";
            // 
            // btnAjouterUneAgence
            // 
            this.btnAjouterUneAgence.BackColor = System.Drawing.Color.SteelBlue;
            this.btnAjouterUneAgence.FlatAppearance.BorderColor = System.Drawing.Color.Navy;
            this.btnAjouterUneAgence.FlatAppearance.BorderSize = 0;
            this.btnAjouterUneAgence.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAjouterUneAgence.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAjouterUneAgence.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnAjouterUneAgence.Image = ((System.Drawing.Image)(resources.GetObject("btnAjouterUneAgence.Image")));
            this.btnAjouterUneAgence.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAjouterUneAgence.Location = new System.Drawing.Point(249, 57);
            this.btnAjouterUneAgence.Name = "btnAjouterUneAgence";
            this.btnAjouterUneAgence.Size = new System.Drawing.Size(118, 41);
            this.btnAjouterUneAgence.TabIndex = 108;
            this.btnAjouterUneAgence.Text = "IMPRIMER";
            this.btnAjouterUneAgence.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAjouterUneAgence.UseVisualStyleBackColor = false;
            this.btnAjouterUneAgence.Click += new System.EventHandler(this.btnAjouterUneAgence_Click);
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
            this.button1.Location = new System.Drawing.Point(116, 57);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(132, 41);
            this.button1.TabIndex = 107;
            this.button1.Text = "SUPPRIMER";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            this.button2.Location = new System.Drawing.Point(0, 57);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(114, 41);
            this.button2.TabIndex = 110;
            this.button2.Text = "NOUVEAU";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtDeduction
            // 
            this.txtDeduction.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDeduction.Location = new System.Drawing.Point(942, 60);
            this.txtDeduction.Name = "txtDeduction";
            this.txtDeduction.Size = new System.Drawing.Size(125, 29);
            this.txtDeduction.TabIndex = 96;
            this.txtDeduction.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDeduction.TextChanged += new System.EventHandler(this.txtDeduction_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(815, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 16);
            this.label1.TabIndex = 111;
            this.label1.Text = "RECHERCHER :";
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.White;
            this.button3.FlatAppearance.BorderColor = System.Drawing.SystemColors.ButtonFace;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.Location = new System.Drawing.Point(1066, 60);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(39, 29);
            this.button3.TabIndex = 112;
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
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
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.checkBox1.Location = new System.Drawing.Point(373, 67);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(58, 22);
            this.checkBox1.TabIndex = 125;
            this.checkBox1.Text = "Liste";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(433, 67);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 20);
            this.label4.TabIndex = 153;
            this.label4.Text = "Année :";
            // 
            // cmbAnnnee
            // 
            this.cmbAnnnee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAnnnee.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbAnnnee.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbAnnnee.FormattingEnabled = true;
            this.cmbAnnnee.Location = new System.Drawing.Point(495, 62);
            this.cmbAnnnee.Name = "cmbAnnnee";
            this.cmbAnnnee.Size = new System.Drawing.Size(99, 26);
            this.cmbAnnnee.TabIndex = 152;
            this.cmbAnnnee.SelectedIndexChanged += new System.EventHandler(this.cmbAnnnee_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(600, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 20);
            this.label2.TabIndex = 155;
            this.label2.Text = "Mois :";
            // 
            // cmbMois
            // 
            this.cmbMois.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMois.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbMois.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbMois.FormattingEnabled = true;
            this.cmbMois.Items.AddRange(new object[] {
            "Janvier",
            "Février",
            "Mars",
            "Avril",
            "Mai",
            "Juin",
            "Juillet",
            "Août",
            "Septembre",
            "Octobre",
            "Novembre",
            "Decembre"});
            this.cmbMois.Location = new System.Drawing.Point(653, 61);
            this.cmbMois.Name = "cmbMois";
            this.cmbMois.Size = new System.Drawing.Size(156, 28);
            this.cmbMois.TabIndex = 181;
            this.cmbMois.SelectedIndexChanged += new System.EventHandler(this.cmbMois_SelectedIndexChanged);
            // 
            // ListeAvanceSurSalaireFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1113, 442);
            this.Controls.Add(this.cmbMois);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cmbAnnnee);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDeduction);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnAjouterUneAgence);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dgvAcompte);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ListeAvanceSurSalaireFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ListeAcompteFrm";
            this.Load += new System.EventHandler(this.ListeAcompteFrm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAcompte)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dgvAcompte;
        private System.Windows.Forms.Button btnFermer;
        private System.Windows.Forms.Button btnAjouterUneAgence;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox txtDeduction;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button3;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn clDesidgnation;
        private System.Windows.Forms.DataGridViewTextBoxColumn clPEmploye;
        private System.Windows.Forms.DataGridViewTextBoxColumn cldATE;
        private System.Windows.Forms.DataGridViewTextBoxColumn clRemise;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn clQte;
        private System.Windows.Forms.DataGridViewTextBoxColumn clPrixTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbAnnnee;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.ComboBox cmbMois;
    }
}