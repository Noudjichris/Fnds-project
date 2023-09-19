namespace SGDP.Formes
{
    partial class ModifTsockFrm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ModifTsockFrm));
            this.lblEntete = new System.Windows.Forms.Label();
            this.lineShape1 = new Microsoft.VisualBasic.PowerPacks.LineShape();
            this.shapeContainer1 = new Microsoft.VisualBasic.PowerPacks.ShapeContainer();
            this.dgvProduit = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clDesidgnation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clStock = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clPrixAchat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtStock = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblNumeroPiece = new System.Windows.Forms.Label();
            this.lblReference = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblTiers = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblNomTiers = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtTVA = new System.Windows.Forms.TextBox();
            this.lblMontantTTC = new System.Windows.Forms.Label();
            this.lblTotalHT = new System.Windows.Forms.Label();
            this.txtPrixTotal = new System.Windows.Forms.TextBox();
            this.txtDesignation = new System.Windows.Forms.TextBox();
            this.txtPrixAchat = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProduit)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblEntete
            // 
            this.lblEntete.BackColor = System.Drawing.Color.Transparent;
            this.lblEntete.Font = new System.Drawing.Font("Arial Unicode MS", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEntete.ForeColor = System.Drawing.Color.Black;
            this.lblEntete.Location = new System.Drawing.Point(22, 9);
            this.lblEntete.Name = "lblEntete";
            this.lblEntete.Size = new System.Drawing.Size(673, 37);
            this.lblEntete.TabIndex = 7;
            this.lblEntete.Text = "Mouvement d\'entrée et sortie des articles";
            // 
            // lineShape1
            // 
            this.lineShape1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lineShape1.BorderColor = System.Drawing.SystemColors.ButtonShadow;
            this.lineShape1.BorderWidth = 2;
            this.lineShape1.Name = "lineShape1";
            this.lineShape1.X1 = 1;
            this.lineShape1.X2 = 1026;
            this.lineShape1.Y1 = 54;
            this.lineShape1.Y2 = 55;
            // 
            // shapeContainer1
            // 
            this.shapeContainer1.Location = new System.Drawing.Point(0, 0);
            this.shapeContainer1.Margin = new System.Windows.Forms.Padding(0);
            this.shapeContainer1.Name = "shapeContainer1";
            this.shapeContainer1.Shapes.AddRange(new Microsoft.VisualBasic.PowerPacks.Shape[] {
            this.lineShape1});
            this.shapeContainer1.Size = new System.Drawing.Size(1028, 386);
            this.shapeContainer1.TabIndex = 9;
            this.shapeContainer1.TabStop = false;
            // 
            // dgvProduit
            // 
            this.dgvProduit.AllowUserToAddRows = false;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.DeepSkyBlue;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvProduit.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle11;
            this.dgvProduit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvProduit.BackgroundColor = System.Drawing.Color.White;
            this.dgvProduit.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvProduit.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SunkenVertical;
            this.dgvProduit.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Arial Unicode MS", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle12.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProduit.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.dgvProduit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProduit.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.clDesidgnation,
            this.clStock,
            this.clPrixAchat,
            this.Column3,
            this.Column2});
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvProduit.DefaultCellStyle = dataGridViewCellStyle13;
            this.dgvProduit.EnableHeadersVisualStyles = false;
            this.dgvProduit.GridColor = System.Drawing.Color.White;
            this.dgvProduit.Location = new System.Drawing.Point(7, 244);
            this.dgvProduit.Margin = new System.Windows.Forms.Padding(4);
            this.dgvProduit.Name = "dgvProduit";
            this.dgvProduit.ReadOnly = true;
            this.dgvProduit.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvProduit.RowHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this.dgvProduit.RowHeadersVisible = false;
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.Honeydew;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.Color.DeepSkyBlue;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvProduit.RowsDefaultCellStyle = dataGridViewCellStyle15;
            this.dgvProduit.RowTemplate.DefaultCellStyle.NullValue = null;
            this.dgvProduit.RowTemplate.Height = 25;
            this.dgvProduit.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProduit.Size = new System.Drawing.Size(1014, 131);
            this.dgvProduit.TabIndex = 139;
            this.dgvProduit.DoubleClick += new System.EventHandler(this.dgvProduit_DoubleClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "ID";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Visible = false;
            // 
            // clDesidgnation
            // 
            this.clDesidgnation.HeaderText = "DESIGNATION";
            this.clDesidgnation.Name = "clDesidgnation";
            this.clDesidgnation.ReadOnly = true;
            this.clDesidgnation.Width = 450;
            // 
            // clStock
            // 
            this.clStock.HeaderText = "QUANTITE";
            this.clStock.Name = "clStock";
            this.clStock.ReadOnly = true;
            this.clStock.Width = 200;
            // 
            // clPrixAchat
            // 
            this.clPrixAchat.HeaderText = "PRIX UNITAIRE";
            this.clPrixAchat.Name = "clPrixAchat";
            this.clPrixAchat.ReadOnly = true;
            this.clPrixAchat.Width = 200;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "PRIX TOTAL";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 200;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "NO";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Visible = false;
            // 
            // txtStock
            // 
            this.txtStock.BackColor = System.Drawing.SystemColors.Window;
            this.txtStock.Font = new System.Drawing.Font("Arial Unicode MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStock.ForeColor = System.Drawing.Color.Black;
            this.txtStock.Location = new System.Drawing.Point(632, 209);
            this.txtStock.Margin = new System.Windows.Forms.Padding(4);
            this.txtStock.Name = "txtStock";
            this.txtStock.Size = new System.Drawing.Size(147, 36);
            this.txtStock.TabIndex = 141;
            this.txtStock.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtStock.TextChanged += new System.EventHandler(this.txtPrixAchat_TextChanged);
            this.txtStock.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtStock_KeyDown);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblNumeroPiece);
            this.groupBox2.Controls.Add(this.lblReference);
            this.groupBox2.Controls.Add(this.comboBox2);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.lblTiers);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.lblNomTiers);
            this.groupBox2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(7, 67);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(635, 128);
            this.groupBox2.TabIndex = 146;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Référence";
            // 
            // lblNumeroPiece
            // 
            this.lblNumeroPiece.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNumeroPiece.BackColor = System.Drawing.Color.Gainsboro;
            this.lblNumeroPiece.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblNumeroPiece.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumeroPiece.ForeColor = System.Drawing.Color.Black;
            this.lblNumeroPiece.Location = new System.Drawing.Point(125, 30);
            this.lblNumeroPiece.Name = "lblNumeroPiece";
            this.lblNumeroPiece.Size = new System.Drawing.Size(242, 29);
            this.lblNumeroPiece.TabIndex = 159;
            this.lblNumeroPiece.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblReference
            // 
            this.lblReference.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblReference.BackColor = System.Drawing.Color.Gainsboro;
            this.lblReference.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblReference.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReference.ForeColor = System.Drawing.Color.Black;
            this.lblReference.Location = new System.Drawing.Point(125, 62);
            this.lblReference.Name = "lblReference";
            this.lblReference.Size = new System.Drawing.Size(504, 29);
            this.lblReference.TabIndex = 158;
            this.lblReference.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBox2
            // 
            this.comboBox2.BackColor = System.Drawing.Color.Gainsboro;
            this.comboBox2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.comboBox2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox2.ForeColor = System.Drawing.Color.Black;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "Ajustement stock",
            "Produits avariés",
            "Produits expirés"});
            this.comboBox2.Location = new System.Drawing.Point(447, 26);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(182, 29);
            this.comboBox2.TabIndex = 157;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(374, 32);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 21);
            this.label5.TabIndex = 154;
            this.label5.Text = "Date :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(8, 68);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 21);
            this.label4.TabIndex = 153;
            this.label4.Text = "Référence :";
            // 
            // lblTiers
            // 
            this.lblTiers.AutoSize = true;
            this.lblTiers.BackColor = System.Drawing.Color.Transparent;
            this.lblTiers.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTiers.Location = new System.Drawing.Point(8, 101);
            this.lblTiers.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTiers.Name = "lblTiers";
            this.lblTiers.Size = new System.Drawing.Size(50, 21);
            this.lblTiers.TabIndex = 152;
            this.lblTiers.Text = "Client";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(8, 39);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(78, 21);
            this.label10.TabIndex = 151;
            this.label10.Text = "N° pièce :";
            // 
            // lblNomTiers
            // 
            this.lblNomTiers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNomTiers.BackColor = System.Drawing.Color.Gainsboro;
            this.lblNomTiers.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblNomTiers.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNomTiers.ForeColor = System.Drawing.Color.Black;
            this.lblNomTiers.Location = new System.Drawing.Point(125, 93);
            this.lblNomTiers.Name = "lblNomTiers";
            this.lblNomTiers.Size = new System.Drawing.Size(504, 29);
            this.lblNomTiers.TabIndex = 150;
            this.lblNomTiers.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(783, 63);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(117, 135);
            this.groupBox3.TabIndex = 150;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Référence";
            this.groupBox3.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox3_Paint);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 36);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 25);
            this.label2.TabIndex = 155;
            this.label2.Text = "Total HT:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 64);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 25);
            this.label1.TabIndex = 154;
            this.label1.Text = "TVA (%):";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Font = new System.Drawing.Font("Arial Unicode MS", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(7, 97);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(113, 25);
            this.label8.TabIndex = 153;
            this.label8.Text = "Total TTC:";
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.txtTVA);
            this.groupBox4.Controls.Add(this.lblMontantTTC);
            this.groupBox4.Controls.Add(this.lblTotalHT);
            this.groupBox4.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(901, 63);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(122, 135);
            this.groupBox4.TabIndex = 154;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Référence";
            this.groupBox4.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox4_Paint);
            // 
            // txtTVA
            // 
            this.txtTVA.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTVA.BackColor = System.Drawing.SystemColors.ControlLight;
            this.txtTVA.Font = new System.Drawing.Font("Arial Unicode MS", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTVA.ForeColor = System.Drawing.Color.Black;
            this.txtTVA.Location = new System.Drawing.Point(2, 60);
            this.txtTVA.Margin = new System.Windows.Forms.Padding(4);
            this.txtTVA.Name = "txtTVA";
            this.txtTVA.Size = new System.Drawing.Size(118, 36);
            this.txtTVA.TabIndex = 159;
            this.txtTVA.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTVA_KeyDown);
            // 
            // lblMontantTTC
            // 
            this.lblMontantTTC.BackColor = System.Drawing.Color.Transparent;
            this.lblMontantTTC.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMontantTTC.Font = new System.Drawing.Font("Arial Unicode MS", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMontantTTC.Location = new System.Drawing.Point(3, 100);
            this.lblMontantTTC.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMontantTTC.Name = "lblMontantTTC";
            this.lblMontantTTC.Size = new System.Drawing.Size(116, 32);
            this.lblMontantTTC.TabIndex = 154;
            this.lblMontantTTC.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotalHT
            // 
            this.lblTotalHT.BackColor = System.Drawing.Color.Transparent;
            this.lblTotalHT.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTotalHT.Font = new System.Drawing.Font("Arial Unicode MS", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalHT.Location = new System.Drawing.Point(3, 25);
            this.lblTotalHT.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalHT.Name = "lblTotalHT";
            this.lblTotalHT.Size = new System.Drawing.Size(116, 32);
            this.lblTotalHT.TabIndex = 153;
            this.lblTotalHT.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPrixTotal
            // 
            this.txtPrixTotal.BackColor = System.Drawing.Color.White;
            this.txtPrixTotal.Enabled = false;
            this.txtPrixTotal.Font = new System.Drawing.Font("Arial Unicode MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrixTotal.ForeColor = System.Drawing.Color.Black;
            this.txtPrixTotal.Location = new System.Drawing.Point(794, 206);
            this.txtPrixTotal.Margin = new System.Windows.Forms.Padding(4);
            this.txtPrixTotal.Name = "txtPrixTotal";
            this.txtPrixTotal.Size = new System.Drawing.Size(147, 36);
            this.txtPrixTotal.TabIndex = 156;
            this.txtPrixTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtDesignation
            // 
            this.txtDesignation.BackColor = System.Drawing.SystemColors.Window;
            this.txtDesignation.Font = new System.Drawing.Font("Arial Unicode MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDesignation.ForeColor = System.Drawing.Color.Black;
            this.txtDesignation.Location = new System.Drawing.Point(7, 206);
            this.txtDesignation.Margin = new System.Windows.Forms.Padding(4);
            this.txtDesignation.Name = "txtDesignation";
            this.txtDesignation.Size = new System.Drawing.Size(151, 36);
            this.txtDesignation.TabIndex = 157;
            this.txtDesignation.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDesignation.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            // 
            // txtPrixAchat
            // 
            this.txtPrixAchat.BackColor = System.Drawing.SystemColors.Window;
            this.txtPrixAchat.Font = new System.Drawing.Font("Arial Unicode MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPrixAchat.ForeColor = System.Drawing.Color.Black;
            this.txtPrixAchat.Location = new System.Drawing.Point(345, 210);
            this.txtPrixAchat.Margin = new System.Windows.Forms.Padding(4);
            this.txtPrixAchat.Name = "txtPrixAchat";
            this.txtPrixAchat.Size = new System.Drawing.Size(147, 36);
            this.txtPrixAchat.TabIndex = 158;
            this.txtPrixAchat.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPrixAchat.TextChanged += new System.EventHandler(this.txtPrixAchat_TextChanged);
            this.txtPrixAchat.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPrixAchat_KeyDown);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.SteelBlue;
            this.button4.FlatAppearance.BorderSize = 2;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button4.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.Color.White;
            this.button4.Image = ((System.Drawing.Image)(resources.GetObject("button4.Image")));
            this.button4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button4.Location = new System.Drawing.Point(648, 128);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(130, 35);
            this.button4.TabIndex = 155;
            this.button4.Text = "Aperçu";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.SteelBlue;
            this.button3.FlatAppearance.BorderSize = 2;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.ForeColor = System.Drawing.Color.White;
            this.button3.Image = ((System.Drawing.Image)(resources.GetObject("button3.Image")));
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.Location = new System.Drawing.Point(648, 93);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(130, 34);
            this.button3.TabIndex = 149;
            this.button3.Text = "Retirer";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Arial Unicode MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(860, 5);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(40, 41);
            this.btnExit.TabIndex = 144;
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.SteelBlue;
            this.button1.FlatAppearance.BorderSize = 2;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(648, 58);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 34);
            this.button1.TabIndex = 159;
            this.button1.Text = "Sous details";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.SteelBlue;
            this.button2.FlatAppearance.BorderSize = 2;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.White;
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(648, 164);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(130, 35);
            this.button2.TabIndex = 160;
            this.button2.Text = "Imprimer";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
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
            this.checkBox1.Font = new System.Drawing.Font("Arial Unicode MS", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.Location = new System.Drawing.Point(648, 24);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(136, 22);
            this.checkBox1.TabIndex = 161;
            this.checkBox1.Text = "Bon de livraison";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // ModifTsockFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1028, 386);
            this.ControlBox = false;
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtPrixAchat);
            this.Controls.Add(this.txtDesignation);
            this.Controls.Add(this.txtPrixTotal);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.txtStock);
            this.Controls.Add(this.dgvProduit);
            this.Controls.Add(this.lblEntete);
            this.Controls.Add(this.shapeContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ModifTsockFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = " ";
            this.Load += new System.EventHandler(this.ModifTsockFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProduit)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.VisualBasic.PowerPacks.LineShape lineShape1;
        private Microsoft.VisualBasic.PowerPacks.ShapeContainer shapeContainer1;
        private System.Windows.Forms.DataGridView dgvProduit;
        public System.Windows.Forms.TextBox txtStock;
        public System.Windows.Forms.Button btnExit;
        public System.Windows.Forms.Label lblEntete;
        private System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.Button button3;
        public System.Windows.Forms.Label lblNomTiers;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.Label lblNumeroPiece;
        public System.Windows.Forms.Label lblReference;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lblTotalHT;
        public System.Windows.Forms.Button button4;
        public System.Windows.Forms.TextBox txtPrixTotal;
        public System.Windows.Forms.TextBox txtDesignation;
        public System.Windows.Forms.TextBox txtPrixAchat;
        private System.Windows.Forms.Label lblMontantTTC;
        public System.Windows.Forms.TextBox txtTVA;
        public System.Windows.Forms.ComboBox comboBox2;
        public System.Windows.Forms.Label lblTiers;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button button1;
        public System.Windows.Forms.Button button2;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn clDesidgnation;
        private System.Windows.Forms.DataGridViewTextBoxColumn clStock;
        private System.Windows.Forms.DataGridViewTextBoxColumn clPrixAchat;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}