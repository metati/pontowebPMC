using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace pnrClient
{
    public partial class frmJustificarPresenca : Form
    {
        private string _UrlWS { get; set; }
        private int _IDEmpresa;
        private int _IDSetor;
        public frmJustificarPresenca(string UrlWS, int IDEmpresa, int IDSetor)
        {
            _UrlWS = UrlWS;
            _IDEmpresa = IDEmpresa;
            _IDSetor = IDSetor;
            InitializeComponent();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            int retorno = 0;
            try
            {
                var webS = new pontonarede.ServiceSoapClient("ServiceSoap", _UrlWS);
                webS.Endpoint.Binding.OpenTimeout = TimeSpan.FromSeconds(10); //OpenTimeout p/ 3 e SendTimeout p 15
                webS.Endpoint.Binding.SendTimeout = TimeSpan.FromSeconds(15);
                webS.Endpoint.Binding.ReceiveTimeout = TimeSpan.FromSeconds(7);
                webS.Open();
                retorno = webS.PedidoJustificativaLocal(txtCPF.Text, txtMatricula.Text, txtSenha.Text, txtObs.Text, _IDEmpresa, _IDSetor, "TentoWebServiceNovamente7x24dm12");
                //StatusBar.Text = "Biometrias carregadas! WS.";
                webS.Close();

                if (retorno == 1)
                {
                    MessageBox.Show("Salvo com sucesso!");
                    this.Close();
                    this.Dispose();
                }
                else if (retorno == -1)
                {
                    MessageBox.Show("Login incorreto!");
                }
                else if (retorno == -2)
                {
                    MessageBox.Show("Senha incorreta!");
                }
                else if (retorno == -3)
                {
                    MessageBox.Show("Matrícula Incorreta!");
                }
            }
            catch { }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}
