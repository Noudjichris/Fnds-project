namespace SGSP.Formes
{
    partial class ScanFrm
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
            this.cmbCategorie = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cmbCategorie
            // 
            this.cmbCategorie.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cmbCategorie.Font = new System.Drawing.Font("Arial Unicode MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbCategorie.FormattingEnabled = true;
            this.cmbCategorie.Location = new System.Drawing.Point(104, 69);
            this.cmbCategorie.Name = "cmbCategorie";
            this.cmbCategorie.Size = new System.Drawing.Size(370, 34);
            this.cmbCategorie.TabIndex = 125;
            // 
            // ScanFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(566, 377);
            this.Controls.Add(this.cmbCategorie);
            this.Name = "ScanFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "ScanFrm";
            this.Load += new System.EventHandler(this.ScanFrm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ComboBox cmbCategorie;
    }
}