namespace pnrClient
{
    partial class FrmListaLogsConexao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmListaLogsConexao));
            this.gridListaLog = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridListaLog)).BeginInit();
            this.SuspendLayout();
            // 
            // gridListaLog
            // 
            this.gridListaLog.AllowUserToAddRows = false;
            this.gridListaLog.AllowUserToDeleteRows = false;
            this.gridListaLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridListaLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridListaLog.Location = new System.Drawing.Point(0, 0);
            this.gridListaLog.Name = "gridListaLog";
            this.gridListaLog.ReadOnly = true;
            this.gridListaLog.Size = new System.Drawing.Size(800, 450);
            this.gridListaLog.TabIndex = 0;
            // 
            // FrmListaLogsConexao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.gridListaLog);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmListaLogsConexao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lista de Logs de Conexao";
            ((System.ComponentModel.ISupportInitialize)(this.gridListaLog)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView gridListaLog;
    }
}