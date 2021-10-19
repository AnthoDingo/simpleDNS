
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
            this.nudTTL = new System.Windows.Forms.NumericUpDown();
            this.lblSubDomain = new System.Windows.Forms.Label();
            this.tbxSubDomain = new System.Windows.Forms.TextBox();
            this.lblType = new System.Windows.Forms.Label();
            this.cbxType = new System.Windows.Forms.ComboBox();
            this.tbxValue = new System.Windows.Forms.TextBox();
            this.lblValue = new System.Windows.Forms.Label();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudTTL)).BeginInit();
            this.SuspendLayout();
            // 
            // tbxResult
            // 
            this.tbxResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxResult.Location = new System.Drawing.Point(12, 137);
            this.tbxResult.Multiline = true;
            this.tbxResult.Name = "tbxResult";
            this.tbxResult.ReadOnly = true;
            this.tbxResult.Size = new System.Drawing.Size(575, 278);
            this.tbxResult.TabIndex = 98;
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
            this.tbxDomain.TextChanged += new System.EventHandler(this.tbxDomain_TextChanged);
            // 
            // lblTTL
            // 
            this.lblTTL.AutoSize = true;
            this.lblTTL.Location = new System.Drawing.Point(177, 15);
            this.lblTTL.Name = "lblTTL";
            this.lblTTL.Size = new System.Drawing.Size(98, 15);
            this.lblTTL.TabIndex = 3;
            this.lblTTL.Text = "TTL (Défaut 3660)";
            // 
            // nudTTL
            // 
            this.nudTTL.Location = new System.Drawing.Point(177, 34);
            this.nudTTL.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.nudTTL.Name = "nudTTL";
            this.nudTTL.Size = new System.Drawing.Size(110, 23);
            this.nudTTL.TabIndex = 2;
            this.nudTTL.ValueChanged += new System.EventHandler(this.nudTTL_ValueChanged);
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
            this.tbxSubDomain.Size = new System.Drawing.Size(126, 23);
            this.tbxSubDomain.TabIndex = 3;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(177, 78);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(31, 15);
            this.lblType.TabIndex = 7;
            this.lblType.Text = "Type";
            // 
            // cbxType
            // 
            this.cbxType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxType.FormattingEnabled = true;
            this.cbxType.Location = new System.Drawing.Point(177, 96);
            this.cbxType.Name = "cbxType";
            this.cbxType.Size = new System.Drawing.Size(134, 23);
            this.cbxType.TabIndex = 4;
            // 
            // tbxValue
            // 
            this.tbxValue.Location = new System.Drawing.Point(348, 96);
            this.tbxValue.Name = "tbxValue";
            this.tbxValue.Size = new System.Drawing.Size(145, 23);
            this.tbxValue.TabIndex = 5;
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
            this.btnCopy.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCopy.Location = new System.Drawing.Point(265, 421);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(75, 23);
            this.btnCopy.TabIndex = 99;
            this.btnCopy.Text = "Copier";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(512, 95);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(75, 23);
            this.btnAdd.TabIndex = 6;
            this.btnAdd.Text = "Ajouter";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // frmAssistant
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(601, 450);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.tbxValue);
            this.Controls.Add(this.lblValue);
            this.Controls.Add(this.cbxType);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.tbxSubDomain);
            this.Controls.Add(this.lblSubDomain);
            this.Controls.Add(this.nudTTL);
            this.Controls.Add(this.lblTTL);
            this.Controls.Add(this.tbxDomain);
            this.Controls.Add(this.lblDomain);
            this.Controls.Add(this.tbxResult);
            this.MinimumSize = new System.Drawing.Size(617, 489);
            this.Name = "frmAssistant";
            this.Text = "Assistant";
            ((System.ComponentModel.ISupportInitialize)(this.nudTTL)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbxResult;
        private System.Windows.Forms.Label lblDomain;
        private System.Windows.Forms.TextBox tbxDomain;
        private System.Windows.Forms.Label lblTTL;
        private System.Windows.Forms.NumericUpDown nudTTL;
        private System.Windows.Forms.Label lblSubDomain;
        private System.Windows.Forms.TextBox tbxSubDomain;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.ComboBox cbxType;
        private System.Windows.Forms.TextBox tbxValue;
        private System.Windows.Forms.Label lblValue;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Button btnAdd;
    }
}