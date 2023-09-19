namespace SGSP.Formes
{
    partial class DocumentFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DocumentFrm));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtMotCle = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnValider = new System.Windows.Forms.Button();
            this.cmbTypeDoc = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.button3 = new System.Windows.Forms.Button();
            this.cmbFournisseur = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.txtRef = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtDesignation = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtNo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cmbAnnne = new System.Windows.Forms.ComboBox();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.cmbAnnne);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.txtMotCle);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.btnValider);
            this.groupBox3.Controls.Add(this.cmbTypeDoc);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.dateTimePicker1);
            this.groupBox3.Controls.Add(this.button3);
            this.groupBox3.Controls.Add(this.cmbFournisseur);
            this.groupBox3.Controls.Add(this.button2);
            this.groupBox3.Controls.Add(this.txtRef);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.txtDesignation);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.txtNo);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Location = new System.Drawing.Point(13, 53);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(706, 305);
            this.groupBox3.TabIndex = 49;
            this.groupBox3.TabStop = false;
            this.groupBox3.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox3_Paint);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(1, 241);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(54, 20);
            this.label14.TabIndex = 89;
            this.label14.Text = "Libellé";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(364, 195);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(52, 20);
            this.label11.TabIndex = 130;
            this.label11.Text = "Date :";
            // 
            // txtMotCle
            // 
            this.txtMotCle.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMotCle.Location = new System.Drawing.Point(168, 225);
            this.txtMotCle.Multiline = true;
            this.txtMotCle.Name = "txtMotCle";
            this.txtMotCle.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtMotCle.Size = new System.Drawing.Size(411, 67);
            this.txtMotCle.TabIndex = 88;
            this.txtMotCle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(7, 127);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(51, 20);
            this.label9.TabIndex = 129;
            this.label9.Text = "Tiers :";
            // 
            // btnValider
            // 
            this.btnValider.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btnValider.FlatAppearance.BorderSize = 0;
            this.btnValider.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnValider.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnValider.ForeColor = System.Drawing.Color.Black;
            this.btnValider.Image = ((System.Drawing.Image)(resources.GetObject("btnValider.Image")));
            this.btnValider.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnValider.Location = new System.Drawing.Point(584, 251);
            this.btnValider.Name = "btnValider";
            this.btnValider.Size = new System.Drawing.Size(111, 41);
            this.btnValider.TabIndex = 50;
            this.btnValider.Text = "Valider";
            this.btnValider.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnValider.UseVisualStyleBackColor = false;
            this.btnValider.Click += new System.EventHandler(this.btnValider_Click);
            // 
            // cmbTypeDoc
            // 
            this.cmbTypeDoc.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbTypeDoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbTypeDoc.FormattingEnabled = true;
            this.cmbTypeDoc.Location = new System.Drawing.Point(171, 85);
            this.cmbTypeDoc.Name = "cmbTypeDoc";
            this.cmbTypeDoc.Size = new System.Drawing.Size(408, 28);
            this.cmbTypeDoc.TabIndex = 127;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(7, 199);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(69, 20);
            this.label13.TabIndex = 87;
            this.label13.Text = "Exercice";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(433, 189);
            this.dateTimePicker1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(147, 26);
            this.dateTimePicker1.TabIndex = 126;
            // 
            // button3
            // 
            this.button3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("button3.BackgroundImage")));
            this.button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button3.FlatAppearance.BorderSize = 0;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button3.Location = new System.Drawing.Point(584, 115);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(49, 35);
            this.button3.TabIndex = 123;
            this.button3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // cmbFournisseur
            // 
            this.cmbFournisseur.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbFournisseur.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbFournisseur.FormattingEnabled = true;
            this.cmbFournisseur.Location = new System.Drawing.Point(170, 119);
            this.cmbFournisseur.Name = "cmbFournisseur";
            this.cmbFournisseur.Size = new System.Drawing.Size(408, 28);
            this.cmbFournisseur.TabIndex = 122;
            this.cmbFournisseur.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbFournisseur_KeyDown);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Location = new System.Drawing.Point(589, 156);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(49, 32);
            this.button2.TabIndex = 85;
            this.button2.Text = "...";
            this.button2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtRef
            // 
            this.txtRef.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRef.Location = new System.Drawing.Point(171, 50);
            this.txtRef.Name = "txtRef";
            this.txtRef.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtRef.Size = new System.Drawing.Size(408, 29);
            this.txtRef.TabIndex = 2;
            this.txtRef.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(7, 59);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(122, 20);
            this.label12.TabIndex = 82;
            this.label12.Text = "Référence doc :";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(7, 93);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(81, 20);
            this.label10.TabIndex = 77;
            this.label10.Text = "Type doc :";
            // 
            // txtDesignation
            // 
            this.txtDesignation.Location = new System.Drawing.Point(173, 153);
            this.txtDesignation.Name = "txtDesignation";
            this.txtDesignation.ReadOnly = true;
            this.txtDesignation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDesignation.Size = new System.Drawing.Size(407, 26);
            this.txtDesignation.TabIndex = 3;
            this.txtDesignation.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(7, 24);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 20);
            this.label8.TabIndex = 71;
            this.label8.Text = "No :";
            // 
            // txtNo
            // 
            this.txtNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNo.Location = new System.Drawing.Point(171, 15);
            this.txtNo.Name = "txtNo";
            this.txtNo.ReadOnly = true;
            this.txtNo.Size = new System.Drawing.Size(159, 29);
            this.txtNo.TabIndex = 1;
            this.txtNo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(7, 159);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 20);
            this.label4.TabIndex = 2;
            this.label4.Text = "Documents :";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.SteelBlue;
            this.groupBox1.Controls.Add(this.btnExit);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(731, 47);
            this.groupBox1.TabIndex = 47;
            this.groupBox1.TabStop = false;
            this.groupBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBox1_Paint);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnExit.BackgroundImage")));
            this.btnExit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(678, 3);
            this.btnExit.Margin = new System.Windows.Forms.Padding(4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(40, 44);
            this.btnExit.TabIndex = 88;
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(417, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enregistrement des documents";
            // 
            // cmbAnnne
            // 
            this.cmbAnnne.BackColor = System.Drawing.Color.Azure;
            this.cmbAnnne.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbAnnne.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbAnnne.FormattingEnabled = true;
            this.cmbAnnne.Location = new System.Drawing.Point(166, 185);
            this.cmbAnnne.Name = "cmbAnnne";
            this.cmbAnnne.Size = new System.Drawing.Size(181, 34);
            this.cmbAnnne.TabIndex = 139;
            // 
            // DocumentFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 372);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "DocumentFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FacturesFrm";
            this.Load += new System.EventHandler(this.FacturesFrm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FacturesFrm_Paint);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtRef;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtDesignation;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtNo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Button btnValider;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        public System.Windows.Forms.ComboBox cmbFournisseur;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.ComboBox cmbTypeDoc;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        public System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtMotCle;
        private System.Windows.Forms.Label label14;
        public System.Windows.Forms.ComboBox cmbAnnne;
    }
}