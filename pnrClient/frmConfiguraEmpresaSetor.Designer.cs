namespace pnrClient
{
    partial class frmConfiguraEmpresaSetor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConfiguraEmpresaSetor));
            this.Setor = new System.Windows.Forms.Label();
            this.cbSetor = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbEmpresa = new System.Windows.Forms.ComboBox();
            this.gridSetor = new System.Windows.Forms.DataGridView();
            this.NomeComp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridSetor)).BeginInit();
            this.SuspendLayout();
            // 
            // Setor
            // 
            this.Setor.AutoSize = true;
            this.Setor.Location = new System.Drawing.Point(8, 42);
            this.Setor.Name = "Setor";
            this.Setor.Size = new System.Drawing.Size(35, 13);
            this.Setor.TabIndex = 7;
            this.Setor.Text = "Setor:";
            // 
            // cbSetor
            // 
            this.cbSetor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbSetor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbSetor.Location = new System.Drawing.Point(84, 39);
            this.cbSetor.Name = "cbSetor";
            this.cbSetor.Size = new System.Drawing.Size(711, 21);
            this.cbSetor.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Organização:";
            // 
            // cbEmpresa
            // 
            this.cbEmpresa.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cbEmpresa.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbEmpresa.Location = new System.Drawing.Point(84, 12);
            this.cbEmpresa.Name = "cbEmpresa";
            this.cbEmpresa.Size = new System.Drawing.Size(711, 21);
            this.cbEmpresa.TabIndex = 4;
            this.cbEmpresa.SelectedIndexChanged += new System.EventHandler(this.cbEmpresa_SelectedIndexChanged);
            // 
            // gridSetor
            // 
            this.gridSetor.AllowUserToAddRows = false;
            this.gridSetor.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gridSetor.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.gridSetor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridSetor.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NomeComp});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gridSetor.DefaultCellStyle = dataGridViewCellStyle2;
            this.gridSetor.Location = new System.Drawing.Point(12, 66);
            this.gridSetor.Name = "gridSetor";
            this.gridSetor.ReadOnly = true;
            this.gridSetor.Size = new System.Drawing.Size(783, 148);
            this.gridSetor.TabIndex = 9;
            // 
            // NomeComp
            // 
            this.NomeComp.DataPropertyName = "DSSetor";
            this.NomeComp.HeaderText = "Setor Selecioado";
            this.NomeComp.Name = "NomeComp";
            this.NomeComp.ReadOnly = true;
            this.NomeComp.Width = 500;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(93, 226);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 11;
            this.button2.Text = "Cancelar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 226);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // frmConfiguraEmpresaSetor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 261);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.gridSetor);
            this.Controls.Add(this.Setor);
            this.Controls.Add(this.cbSetor);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbEmpresa);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmConfiguraEmpresaSetor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Definir Locallização";
            this.Load += new System.EventHandler(this.frmConfiguraEmpresaSetor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridSetor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Setor;
        private System.Windows.Forms.ComboBox cbSetor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbEmpresa;
        private System.Windows.Forms.DataGridView gridSetor;
        private System.Windows.Forms.DataGridViewTextBoxColumn NomeComp;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
    }
}