using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SecuGen.FDxSDKPro.Windows;

namespace pnrClient
{
    public partial class frmTestaBiometria : Form
    {
        private UtilConexao utilConexao;
        public frmTestaBiometria()
        {
            InitializeComponent();
            utilConexao = new UtilConexao();
            utilConexao.AtualizaStatus(false);
        }

        #region Botões

        private void btComparar_Click(object sender, EventArgs e)
        {
            ComparaBiometria();
        }
        private void btOk_Click(object sender, EventArgs e)
        {
            ComparaBiometria();
            this.Close();
        }

        private void btLimparLog_Click(object sender, EventArgs e)
        {
            lbLogComparativo.Items.Clear();
        }
        #endregion

        #region Variáveis
        string msg;
        crudPtServer crud;
        DataSetpnrClient ds;
        DataSetpnrClient dsUsuarioSelecionado;
        Int32 error = (Int32)SGFPMError.ERROR_NONE;
        Int32 img_qlty = 0;
        private SGFingerPrintManager m_FPM;
        private Byte[] m_VrfMin;
        EscreverLog LG;
        string similares;
        #endregion

        #region Usuarios
        private void GetUsuarios()
        {
            crud = new crudPtServer(utilConexao.tcm.UrlWS);
            ds = new DataSetpnrClient();
            crud.GetUsuarioTemplate(ds);

            cbNomes.DisplayMember = "DSUsuario";
            cbNomes.ValueMember = "IDVinculoUsuario";
            cbNomes.DataSource = ds.TBUsuarioLocal;
            cbNomes.Update();
        }
        #endregion

        #region Comparação
        protected void ComparaBiometria()
        {
            bool matched = false;
            LG = new EscreverLog();
            LG.Esrevelog("Processando comparações...", lbLogComparativo);
            similares = "Biometria compatível com: ";
            lbnomes.Text = string.Empty;
            for (int i = 0; i <= ds.TBUsuarioLocal.Rows.Count - 1; i++)
            {
                lbnomes.Text = string.Format("Comparando com: {0}", ds.TBUsuarioLocal[i].DSUsuario).Trim();
                SGFPMISOTemplateInfo sample_info = new SGFPMISOTemplateInfo();
                error = m_FPM.GetIsoTemplateInfo(ds.TBUsuarioLocal[i].Template1, sample_info);

                for (int z = 0; z <= sample_info.TotalSamples; z++)
                {
                    error = m_FPM.MatchIsoTemplate(ds.TBUsuarioLocal[i].Template1, z, m_VrfMin, 0, SecuGen.FDxSDKPro.Windows.SGFPMSecurityLevel.NORMAL, ref matched);

                    if (matched)
                    {
                        //Se deu match. Listar o nome na listbox.
                        LG.Esrevelog(string.Format("Biometria compativa com: {0}", ds.TBUsuarioLocal[i].DSUsuario), lbLogComparativo);
                        similares += ds.TBUsuarioLocal[i].DSUsuario;

                        pontonarede.ServiceSoapClient WebS = new pontonarede.ServiceSoapClient("ServiceSoap", utilConexao.tcm.UrlWS);
                        WebS.Endpoint.Binding.OpenTimeout = TimeSpan.FromSeconds(10); //OpenTimeout p/ 3 e SendTimeout p 15
                        WebS.Endpoint.Binding.SendTimeout = TimeSpan.FromSeconds(15);
                        WebS.Endpoint.Binding.ReceiveTimeout = TimeSpan.FromSeconds(7);

                        //msg = WebS.BaterPonto(45, 410, Convert.ToInt32(ds.TBUsuarioLocal[i].IDUsuario), DateTime.Now, "TentoWebServiceNovamente7x24dm12");
                        WebS.Close();
                    }
                }
            }
            if (matched)
            {
                MessageBox.Show("Biometria compatível com os nomes listados na caixa de texto.");
                lbnomes.Text = string.Format("Total de usuários comparados: ", ds.TBUsuarioLocal.Rows.Count);
            }
            else
            {
                MessageBox.Show("Não foram encontradas compatibilidades com o usuário selecionado.");
                lbnomes.Text = string.Format("Total de usuários comparados: {0}", ds.TBUsuarioLocal.Rows.Count);
                lbLogComparativo.Items.Clear();
            }
        }
        #endregion


        private void frmTestaBiometria_Load(object sender, EventArgs e)
        {
            GetUsuarios();
            m_FPM = new SGFingerPrintManager();
        }

        private void cbNomes_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_VrfMin = null;
            m_VrfMin = crud.templateComparativo((Convert.ToInt32(cbNomes.SelectedValue)));
        }

    }
}
