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
    public partial class frmRegistrarFrequenciaManual : Form
    {
        public frmRegistrarFrequenciaManual()
        {
            InitializeComponent();
        }
        #region Variáveis
        public int IDEmpresa;
        public DateTime DTfrequencia;
        pontonarede.ServiceSoapClient Webs = new pontonarede.ServiceSoapClient();
        Cript cr = new Cript();
        string msg;
        #endregion

        #region keydown
        private void tbUsuario_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                tbSenha.Focus();
        }

        private void tbSenha_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                tbMatricula.Focus();
        }
        #endregion

        #region Botões
        private void btCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btOk_Click(object sender, EventArgs e)
        {
            Webs.Endpoint.Binding.OpenTimeout = TimeSpan.FromSeconds(2);
            Webs.Endpoint.Binding.SendTimeout = TimeSpan.FromSeconds(7);

            try
            {
                Webs.Open();
                msg = Webs.PontoEspecial2(tbUsuario.Text.Trim(), tbSenha.Text.Trim(), IDEmpresa, DTfrequencia,tbMatricula.Text.Trim(), "TentoWebServiceNovamente7x24dm12");
            }
            catch (Exception ex)
            {
                RegistroLocal RL = new RegistroLocal();
                msg = RL.PontoManual(tbUsuario.Text.Trim(), cr.ActionEncrypt(tbSenha.Text.Trim()), System.DateTime.Now) + " :"+ ex.Message.Trim();
            }

            Webs.Close();
            this.Hide();
            MessageBox.Show(msg, "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1); 
            tbSenha.Text = "";
            tbSenha.Update();
            System.GC.Collect();
        }
        #endregion

        private void tbMatricula_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void tbMatricula_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btOk.Focus();
        }

        private void tbMatricula_PreviewKeyDown_1(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btOk.Focus();
        }
    }
}
