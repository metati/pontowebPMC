using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace pnrClient
{
    public partial class frmConfirmaCadastro : Form
    {

        #region Variaveis
        public string pergunta, nome;
        public bool permite = false;
        #endregion
        private void frmConfirmaCadastro_Load(object sender, EventArgs e)
        {
            lbTexto.Text = pergunta;
            lbNome.Text = nome;
        }

        private void frmConfirmaCadastro_FormClosed(object sender, FormClosedEventArgs e)
        {
            permite = false;
        }



        public frmConfirmaCadastro()
        {
            InitializeComponent();
        }

        #region botões
        private void btOk_Click(object sender, EventArgs e)
        {
            permite = true;
            this.Close();
        }

        private void frmConfirmaCadastro_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(!permite)
            {
                MessageBox.Show("Operação cancelada pelo usuário. Selecione novamente para cadastrar a biometria.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btCancelar_Click(object sender, EventArgs e)
        {
            permite = false;
            this.Close();
        }
        #endregion
    }
}
