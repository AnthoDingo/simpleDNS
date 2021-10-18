
namespace simpleDNS
{
    partial class frmAssistant
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
            this.tbxResult = new System.Windows.Forms.TextBox();
            this.lblDomain = new System.Windows.Forms.Label();
            this.tbxDomain = new System.Windows.Forms.TextBox();
            this.lblTTL = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.lblSubDomain = new System.Windows.Forms.Label();
            this.tbxSubDomain = new System.Windows.Forms.TextBox();
            this.lblType = new System.Windows.Forms.Label();
            this.cbxType = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.lblValue = new System.Windows.Forms.Label();
            this.btnCopy = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // tbxResult
            // 
            this.tbxResult.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxResult.Location = new System.Drawing.Point(12, 137);
            this.tbxResult.Multiline = true;
            this.tbxResult.Name = "tbxResult";
            this.tbxResult.ReadOnly = true;
            this.tbxResult.Size = new System.Drawing.Size(481, 278);
            this.tbxResult.TabIndex = 7;
            // 
            // lblDomain
            // 
            this.lblDomain.AutoSize = true;
            this.lblDomain.Location = new System.Drawing.Point(12, 15);
            this.lblDomain.Name = "lblDomain";
            this.lblDomain.Size = new System.Drawing.Size(49, 15);
            this.lblDomain.TabIndex = 1;
            this.lblDomain.Text = "Domain";
            // 
            // tbxDomain
            // 
            this.tbxDomain.Location = new System.Drawing.Point(12, 33);
            this.tbxDomain.Name = "tbxDomain";
            this.tbxDomain.Size = new System.Drawing.Size(126, 23);
            this.tbxDomain.TabIndex = 1;
            // 
            // lblTTL
            // 
            this.lblTTL.AutoSize = true;
            this.lblTTL.Location = new System.Drawing.Point(183, 15);
            this.lblTTL.Name = "lblTTL";
            this.lblTTL.Size = new System.Drawing.Size(98, 15);
            this.lblTTL.TabIndex = 3;
            this.lblTTL.Text = "TTL (Défaut 3660)";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(183, 34);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(110, 23);
            this.numericUpDown1.TabIndex = 2;
            // 
            // lblSubDomain
            // 
            this.lblSubDomain.AutoSize = true;
            this.lblSubDomain.Location = new System.Drawing.Point(12, 78);
            this.lblSubDomain.Name = "lblSubDomain";
            this.lblSubDomain.Size = new System.Drawing.Size(85, 15);
            this.lblSubDomain.TabIndex = 5;
            this.lblSubDomain.Text = "Sous-Domaine";
            // 
            // tbxSubDomain
            // 
            this.tbxSubDomain.Location = new System.Drawing.Point(12, 96);
            this.tbxSubDomain.Name = "tbxSubDomain";
            this.tbxSubDomain.Size = new System.Drawing.Size(142, 23);
            this.tbxSubDomain.TabIndex = 3;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(183, 78);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(31, 15);
            this.lblType.TabIndex = 7;
            this.lblType.Text = "Type";
            // 
            // cbxType
            // 
            this.cbxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxType.FormattingEnabled = true;
            this.cbxType.Items.AddRange(new object[] {
            "A",
            "CNAME"});
            this.cbxType.Location = new System.Drawing.Point(183, 96);
            this.cbxType.Name = "cbxType";
            this.cbxType.Size = new System.Drawing.Size(134, 23);
            this.cbxType.TabIndex = 4;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(348, 96);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(145, 23);
            this.textBox1.TabIndex = 6;
            // 
            // lblValue
            // 
            this.lblValue.AutoSize = true;
            this.lblValue.Location = new System.Drawing.Point(348, 78);
            this.lblValue.Name = "lblValue";
            this.lblValue.Size = new System.Drawing.Size(39, 15);
            this.lblValue.TabIndex = 9;
            this.lblValue.Text = "Valeur";
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(218, 421);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(75, 23);
            this.btnCopy.TabIndex = 8;
            this.btnCopy.Text = "Copier";
            this.btnCopy.UseVisualStyleBackColor = true;
            // 
            // frmAssistant
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 450);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.lblValue);
            this.Controls.Add(this.cbxType);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.tbxSubDomain);
            this.Controls.Add(this.lblSubDomain);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.lblTTL);
            this.Controls.Add(this.tbxDomain);
            this.Controls.Add(this.lblDomain);
            this.Controls.Add(this.tbxResult);
            this.Name = "frmAssistant";
            this.Text = "Assistant";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbxResult;
        private System.Windows.Forms.Label lblDomain;
        private System.Windows.Forms.TextBox tbxDomain;
        private System.Windows.Forms.Label lblTTL;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label lblSubDomain;
        private System.Windows.Forms.TextBox tbxSubDomain;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ComboBox cbxType;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.Button btnCopy;
    }
}