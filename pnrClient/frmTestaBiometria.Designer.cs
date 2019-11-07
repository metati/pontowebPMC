namespace pnrClient
{
    partial class frmTestaBiometria
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTestaBiometria));
            this.btComparar = new System.Windows.Forms.Button();
            this.lbLogComparativo = new System.Windows.Forms.ListBox();
            this.btOk = new System.Windows.Forms.Button();
            this.btLimparLog = new System.Windows.Forms.Button();
            this.cbNomes = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbnomes = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btComparar
            // 
            this.btComparar.Location = new System.Drawing.Point(508, 25);
            this.btComparar.Name = "btComparar";
            this.btComparar.Size = new System.Drawing.Size(109, 23);
            this.btComparar.TabIndex = 1;
            this.btComparar.Text = "Comparar";
            this.btComparar.UseVisualStyleBackColor = true;
            this.btComparar.Click += new System.EventHandler(this.btComparar_Click);
            // 
            // lbLogComparativo
            // 
            this.lbLogComparativo.FormattingEnabled = true;
            this.lbLogComparativo.Location = new System.Drawing.Point(13, 73);
            this.lbLogComparativo.Name = "lbLogComparativo";
            this.lbLogComparativo.Size = new System.Drawing.Size(604, 147);
            this.lbLogComparativo.TabIndex = 3;
            // 
            // btOk
            // 
            this.btOk.Location = new System.Drawing.Point(12, 226);
            this.btOk.Name = "btOk";
            this.btOk.Size = new System.Drawing.Size(109, 23);
            this.btOk.TabIndex = 4;
            this.btOk.Text = "Ok";
            this.btOk.UseVisualStyleBackColor = true;
            this.btOk.Click += new System.EventHandler(this.btOk_Click);
            // 
            // btLimparLog
            // 
            this.btLimparLog.Location = new System.Drawing.Point(127, 226);
            this.btLimparLog.Name = "btLimparLog";
            this.btLimparLog.Size = new System.Drawing.Size(109, 23);
            this.btLimparLog.TabIndex = 5;
            this.btLimparLog.Text = "Limpar";
            this.btLimparLog.UseVisualStyleBackColor = true;
            this.btLimparLog.Click += new System.EventHandler(this.btLimparLog_Click);
            // 
            // cbNomes
            // 
            this.cbNomes.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbNomes.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbNomes.FormattingEnabled = true;
            this.cbNomes.Location = new System.Drawing.Point(13, 25);
            this.cbNomes.Name = "cbNomes";
            this.cbNomes.Size = new System.Drawing.Size(489, 21);
            this.cbNomes.TabIndex = 0;
            this.cbNomes.SelectedIndexChanged += new System.EventHandler(this.cbNomes_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Selecione quem deseja comparar";
            // 
            // lbnomes
            // 
            this.lbnomes.AutoSize = true;
            this.lbnomes.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbnomes.Location = new System.Drawing.Point(10, 53);
            this.lbnomes.Name = "lbnomes";
            this.lbnomes.Size = new System.Drawing.Size(219, 17);
            this.lbnomes.TabIndex = 7;
            this.lbnomes.Text = "Selecione quem deseja comparar";
            // 
            // frmTestaBiometria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 260);
            this.Controls.Add(this.lbnomes);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btLimparLog);
            this.Controls.Add(this.btOk);
            this.Controls.Add(this.lbLogComparativo);
            this.Controls.Add(this.btComparar);
            this.Controls.Add(this.cbNomes);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "frmTestaBiometria";
            this.Text = "Comparar Biometrias";
            this.Load += new System.EventHandler(this.frmTestaBiometria_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btComparar;
        private System.Windows.Forms.ListBox lbLogComparativo;
        private System.Windows.Forms.Button btOk;
        private System.Windows.Forms.Button btLimparLog;
        private System.Windows.Forms.ComboBox cbNomes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbnomes;
    }
}