namespace GestionPharmacetique.Forme
{
    partial class DepenseFrm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DepenseFrm));
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clAnne = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clMois = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clFacture = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clMontant = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewImageColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewImageColumn();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.lblExercice = new System.Windows.Forms.ComboBox();
            this.cmbMois = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPrix = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txtFacture = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.dtpDebut = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.txtBeneficiaire = new System.Windows.Forms.TextBox();
            this.lblLibelle = new System.Windows.Forms.Label();
            this.lblCode = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.button7 = new System.Windows.Forms.Button();
            this.cmbLibelle = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtRechercherlibelle = new System.Windows.Forms.TextBox();
            this.button6 = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbCategorie = new System.Windows.Forms.ComboBox();
            this.i = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnFermer = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button4 = new System.Windows.Forms.Button();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.BackColor = System.Drawing.Color.Transparent;
            this.groupBox4.Controls.Add(this.dataGridView1);
            this.groupBox4.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(11, 238);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(1080, 331);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.clAnne,
            this.clMois,
            this.clCode,
            this.Column3,
            this.clDate,
            this.clFacture,
            this.Column9,
            this.clMontant,
            this.Column4,
            this.Column5});
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.GridColor = System.Drawing.Color.White;
            this.dataGridView1.Location = new System.Drawing.Point(2, 0);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1078, 326);
            this.dataGridView1.TabIndex = 41;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "ID";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Visible = false;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Catégorie";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // clAnne
            // 
            this.clAnne.HeaderText = "Année";
            this.clAnne.Name = "clAnne";
            this.clAnne.ReadOnly = true;
            // 
            // clMois
            // 
            this.clMois.HeaderText = "Mois";
            this.clMois.Name = "clMois";
            this.clMois.ReadOnly = true;
            // 
            // clCode
            // 
            this.clCode.HeaderText = "Code";
            this.clCode.Name = "clCode";
            this.clCode.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Libellé";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // clDate
            // 
            this.clDate.HeaderText = "Date";
            this.clDate.Name = "clDate";
            this.clDate.ReadOnly = true;
            // 
            // clFacture
            // 
            this.clFacture.HeaderText = "N° facture";
            this.clFacture.Name = "clFacture";
            this.clFacture.ReadOnly = true;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "Béneficiare";
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            // 
            // clMontant
            // 
            this.clMontant.HeaderText = "Montant";
            this.clMontant.Name = "clMontant";
            this.clMontant.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "";
            this.Column4.Image = global::SGSP.Properties.Resources.edit;
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Column5
            // 
            this.Column5.HeaderText = "";
            this.Column5.Image = global::SGSP.Properties.Resources.DeleteRed1;
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // groupBox6
            // 
            this.groupBox6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox6.Controls.Add(this.lblExercice);
            this.groupBox6.Controls.Add(this.cmbMois);
            this.groupBox6.Controls.Add(this.button1);
            this.groupBox6.Controls.Add(this.label8);
            this.groupBox6.Controls.Add(this.txtPrix);
            this.groupBox6.Controls.Add(this.label14);
            this.groupBox6.Controls.Add(this.txtFacture);
            this.groupBox6.Controls.Add(this.label2);
            this.groupBox6.Controls.Add(this.label12);
            this.groupBox6.Controls.Add(this.label9);
            this.groupBox6.Controls.Add(this.dtpDebut);
            this.groupBox6.Controls.Add(this.label7);
            this.groupBox6.Controls.Add(this.txtBeneficiaire);
            this.groupBox6.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox6.Location = new System.Drawing.Point(675, 50);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(413, 140);
            this.groupBox6.TabIndex = 44;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Détails des dépenses";
            // 
            // lblExercice
            // 
            this.lblExercice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.lblExercice.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.lblExercice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExercice.FormattingEnabled = true;
            this.lblExercice.Location = new System.Drawing.Point(96, 31);
            this.lblExercice.Name = "lblExercice";
            this.lblExercice.Size = new System.Drawing.Size(130, 28);
            this.lblExercice.TabIndex = 185;
            this.lblExercice.SelectedIndexChanged += new System.EventHandler(this.lblExercice_SelectedIndexChanged);
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
            this.cmbMois.Location = new System.Drawing.Point(96, 65);
            this.cmbMois.Name = "cmbMois";
            this.cmbMois.Size = new System.Drawing.Size(130, 28);
            this.cmbMois.TabIndex = 184;
            // 
            // button1
            // 
            this.button1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button1.BackgroundImage")));
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(294, 95);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(33, 30);
            this.button1.TabIndex = 125;
            this.button1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(6, 69);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 23);
            this.label8.TabIndex = 183;
            this.label8.Text = "Mois : ";
            // 
            // txtPrix
            // 
            this.txtPrix.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPrix.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrix.Location = new System.Drawing.Point(335, 60);
            this.txtPrix.Name = "txtPrix";
            this.txtPrix.Size = new System.Drawing.Size(72, 29);
            this.txtPrix.TabIndex = 2;
            this.txtPrix.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(6, 31);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(84, 23);
            this.label14.TabIndex = 182;
            this.label14.Text = "Exercice :";
            // 
            // txtFacture
            // 
            this.txtFacture.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFacture.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFacture.Location = new System.Drawing.Point(335, 28);
            this.txtFacture.Name = "txtFacture";
            this.txtFacture.Size = new System.Drawing.Size(72, 29);
            this.txtFacture.TabIndex = 1;
            this.txtFacture.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(232, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 23);
            this.label2.TabIndex = 31;
            this.label2.Text = "Montant :";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(6, 103);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(53, 23);
            this.label12.TabIndex = 34;
            this.label12.Text = "Date :";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(232, 34);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(95, 23);
            this.label9.TabIndex = 40;
            this.label9.Text = "N° facture :";
            // 
            // dtpDebut
            // 
            this.dtpDebut.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDebut.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpDebut.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDebut.Location = new System.Drawing.Point(96, 97);
            this.dtpDebut.Name = "dtpDebut";
            this.dtpDebut.Size = new System.Drawing.Size(130, 29);
            this.dtpDebut.TabIndex = 5;
            this.dtpDebut.ValueChanged += new System.EventHandler(this.dtpDebut_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(228, 102);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 23);
            this.label7.TabIndex = 37;
            this.label7.Text = "Benéf :";
            // 
            // txtBeneficiaire
            // 
            this.txtBeneficiaire.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBeneficiaire.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBeneficiaire.Location = new System.Drawing.Point(335, 93);
            this.txtBeneficiaire.Name = "txtBeneficiaire";
            this.txtBeneficiaire.Size = new System.Drawing.Size(72, 29);
            this.txtBeneficiaire.TabIndex = 3;
            this.txtBeneficiaire.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblLibelle
            // 
            this.lblLibelle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblLibelle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLibelle.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLibelle.Location = new System.Drawing.Point(100, 93);
            this.lblLibelle.Name = "lblLibelle";
            this.lblLibelle.Size = new System.Drawing.Size(382, 28);
            this.lblLibelle.TabIndex = 42;
            this.lblLibelle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblCode
            // 
            this.lblCode.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCode.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCode.Location = new System.Drawing.Point(510, 94);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(83, 28);
            this.lblCode.TabIndex = 41;
            this.lblCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.button7);
            this.groupBox5.Controls.Add(this.cmbLibelle);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.lblCode);
            this.groupBox5.Controls.Add(this.txtRechercherlibelle);
            this.groupBox5.Controls.Add(this.lblLibelle);
            this.groupBox5.Controls.Add(this.button6);
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Controls.Add(this.cmbCategorie);
            this.groupBox5.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(11, 50);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(658, 140);
            this.groupBox5.TabIndex = 42;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Catégories et Libellés des dépenses";
            // 
            // button7
            // 
            this.button7.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button7.BackgroundImage")));
            this.button7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button7.FlatAppearance.BorderSize = 0;
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button7.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button7.Location = new System.Drawing.Point(599, 91);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(49, 31);
            this.button7.TabIndex = 125;
            this.button7.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // cmbLibelle
            // 
            this.cmbLibelle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLibelle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbLibelle.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbLibelle.FormattingEnabled = true;
            this.cmbLibelle.Items.AddRange(new object[] {
            ""});
            this.cmbLibelle.Location = new System.Drawing.Point(100, 60);
            this.cmbLibelle.Name = "cmbLibelle";
            this.cmbLibelle.Size = new System.Drawing.Size(410, 28);
            this.cmbLibelle.TabIndex = 46;
            this.cmbLibelle.SelectedIndexChanged += new System.EventHandler(this.cmbLibelle_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(4, 99);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 23);
            this.label6.TabIndex = 44;
            this.label6.Text = "Libellé :";
            // 
            // txtRechercherlibelle
            // 
            this.txtRechercherlibelle.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRechercherlibelle.Location = new System.Drawing.Point(510, 62);
            this.txtRechercherlibelle.Name = "txtRechercherlibelle";
            this.txtRechercherlibelle.Size = new System.Drawing.Size(83, 29);
            this.txtRechercherlibelle.TabIndex = 45;
            this.txtRechercherlibelle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtRechercherlibelle.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // button6
            // 
            this.button6.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button6.BackgroundImage")));
            this.button6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button6.FlatAppearance.BorderSize = 0;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button6.Location = new System.Drawing.Point(599, 57);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(49, 31);
            this.button6.TabIndex = 124;
            this.button6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(4, 34);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(96, 23);
            this.label11.TabIndex = 45;
            this.label11.Text = "Catégorie : ";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(4, 63);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(69, 23);
            this.label10.TabIndex = 44;
            this.label10.Text = "Libellé :";
            // 
            // cmbCategorie
            // 
            this.cmbCategorie.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCategorie.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbCategorie.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCategorie.FormattingEnabled = true;
            this.cmbCategorie.Items.AddRange(new object[] {
            ""});
            this.cmbCategorie.Location = new System.Drawing.Point(100, 28);
            this.cmbCategorie.Name = "cmbCategorie";
            this.cmbCategorie.Size = new System.Drawing.Size(410, 28);
            this.cmbCategorie.TabIndex = 41;
            this.cmbCategorie.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // i
            // 
            this.i.BackColor = System.Drawing.Color.SteelBlue;
            this.i.FlatAppearance.BorderSize = 0;
            this.i.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.i.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.i.ForeColor = System.Drawing.Color.White;
            this.i.Image = ((System.Drawing.Image)(resources.GetObject("i.Image")));
            this.i.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.i.Location = new System.Drawing.Point(6, 4);
            this.i.Name = "i";
            this.i.Size = new System.Drawing.Size(132, 32);
            this.i.TabIndex = 4;
            this.i.Text = "ENREGISTRER";
            this.i.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.i.UseVisualStyleBackColor = false;
            this.i.Click += new System.EventHandler(this.i_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnFermer);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1100, 43);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            this.groupBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox1_Paint);
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
            this.btnFermer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFermer.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnFermer.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnFermer.Location = new System.Drawing.Point(987, 2);
            this.btnFermer.Name = "btnFermer";
            this.btnFermer.Size = new System.Drawing.Size(40, 41);
            this.btnFermer.TabIndex = 29;
            this.btnFermer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnFermer.UseVisualStyleBackColor = false;
            this.btnFermer.Click += new System.EventHandler(this.btnFermer_Click);
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(7, 5);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(332, 24);
            this.label13.TabIndex = 28;
            this.label13.Text = "JOURNAL DES DEPENSES";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.SteelBlue;
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 572);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1100, 29);
            this.label1.TabIndex = 28;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.comboBox2);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.button5);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.i);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 196);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1076, 38);
            this.groupBox2.TabIndex = 29;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "RAPPORT DU JOURNAL DE LA CAISSE";
            this.groupBox2.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox2_Paint);
            // 
            // button4
            // 
            this.button4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button4.BackgroundImage")));
            this.button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.Image = ((System.Drawing.Image)(resources.GetObject("button4.Image")));
            this.button4.Location = new System.Drawing.Point(795, 6);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(38, 30);
            this.button4.TabIndex = 189;
            this.button4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
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
            this.comboBox2.Location = new System.Drawing.Point(629, 7);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(160, 28);
            this.comboBox2.TabIndex = 192;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(437, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(186, 23);
            this.label4.TabIndex = 191;
            this.label4.Text = "Dépenses mensuèlles :";
            // 
            // button5
            // 
            this.button5.BackColor = System.Drawing.Color.SteelBlue;
            this.button5.FlatAppearance.BorderSize = 0;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button5.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.ForeColor = System.Drawing.Color.White;
            this.button5.Image = ((System.Drawing.Image)(resources.GetObject("button5.Image")));
            this.button5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button5.Location = new System.Drawing.Point(278, 4);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(133, 32);
            this.button5.TabIndex = 42;
            this.button5.Text = "EXPORTER";
            this.button5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.SteelBlue;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.Location = new System.Drawing.Point(140, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(135, 32);
            this.button3.TabIndex = 36;
            this.button3.Text = "IMPRIMER";
            this.button3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
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
            // DepenseFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 601);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox4);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "DepenseFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "DepenseFrm";
            this.Load += new System.EventHandler(this.DepenseFrm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.DepenseFrm_Paint);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DateTimePicker dtpDebut;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Button i;
        private System.Windows.Forms.TextBox txtFacture;
        private System.Windows.Forms.TextBox txtPrix;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.Button button3;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.TextBox txtBeneficiaire;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cmbCategorie;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.TextBox txtRechercherlibelle;
        public System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.ComboBox cmbLibelle;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label lblLibelle;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.ComboBox lblExercice;
        public System.Windows.Forms.ComboBox cmbMois;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnFermer;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn clAnne;
        private System.Windows.Forms.DataGridViewTextBoxColumn clMois;
        private System.Windows.Forms.DataGridViewTextBoxColumn clCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn clDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn clFacture;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn clMontant;
        private System.Windows.Forms.DataGridViewImageColumn Column4;
        private System.Windows.Forms.DataGridViewImageColumn Column5;
        private System.Windows.Forms.Button button4;
        public System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label4;
    }
}