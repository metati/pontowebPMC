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
    public partial class frmLoginAdministrador : Form
    {
        private string _URL;
        #region Variaveis
        public int IDS;
        public bool acesso;
        public int IDEmpresa;
        public string CPF;

        //Form
        frmPrincipal frmPrinc = new frmPrincipal();

        public int IDTPUsuario;
        #endregion

        #region login offLine
        private bool AcessoOff(string usuario, string Senha)
        {
            if (usuario == "Admin" && Senha == "pontoMalaquias")
            {
                return true;
            }
            else
                return false;
        }
        #endregion
        private void btOk_Click(object sender, EventArgs e)
        {

            pontonarede.ServiceSoapClient WebS = new pontonarede.ServiceSoapClient("ServiceSoap", _URL);
            pontonarede.DataSetUsuario dsU = new pontonarede.DataSetUsuario();
            WebS.Open();
            try
            {
                if (WebS.LogarAdmin(IDEmpresa, tbUsuario.Text.Trim(), tbSenha.Text.Trim(), "TentoWebServiceNovamente7x24dm12"))
                {
                    //Se logado - Pegar o tipo de usuário
                    dsU = WebS.UsuariosPontoLogin(IDEmpresa, tbUsuario.Text.Trim(), "TentoWebServiceNovamente7x24dm12");

                    //frmPrinc.btManutencao.Enabled = true;
                    //frmPrinc.btLogar.Text = "Logout";
                    acesso = true;

                    //if (dsU.vwUsuarioWebService.Rows.Count > 0) // Verificar aqui. O tipo de usuário 1 deverá ter acesso a todas as rotinas do aplicativo.
                    //{
                        //IDTPUsuario = dsU.vwUsuarioWebService[0].IDTPUsuario;
                        CPF = tbUsuario.Text.Trim();
                    //}

                    this.Hide();
                    // frmPrinc.idsetor = Convert.ToInt32(Ac.IDSETOR)
                }
                else
                {
                    MessageBox.Show("Usuário desconhecido ou sem permissão de Administrador.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);

                    tbSenha.Text = "";
                    tbSenha.Update();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                MessageBox.Show("Houve falha na tentativo de login. Falha de comunicação com a rede.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                acesso = AcessoOff(tbUsuario.Text.Trim(), tbSenha.Text.Trim());
                this.Hide();
            }


            //string msg = Ac.VerificaAcesso(tbUsuario.Text.Trim(), tbSenha.Text.Trim(), ds);
            WebS.Close();
        }
        private void btCancelar_Click(object sender, EventArgs e)
        {

        }
        #region Botões
        
        #endregion
        public frmLoginAdministrador(string URL)
        {
            InitializeComponent();
            _URL = URL;
        }

        private void tbUsuario_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                tbSenha.Focus();
        }

        private void tbSenha_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btOk.Focus();
        }
    }
}
