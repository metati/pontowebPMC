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
    public partial class frmConfiguraEmpresaSetor : Form
    {
        private UtilConexao utilConexao;
        public frmConfiguraEmpresaSetor()
        {
            InitializeComponent();
            utilConexao = new UtilConexao();
            utilConexao.AtualizaStatus(true);
        }

        #region variáveis
        frmPrincipal frmprincipal;
        #endregion

        #region PreencheCombos
        private void PreenchecbEmpresa()
        {
            pontonarede.ServiceSoapClient webS = new pontonarede.ServiceSoapClient("ServiceSoap", utilConexao.tcm.UrlWS);

            pontonarede.DataSetPontoFrequencia ds = new pontonarede.DataSetPontoFrequencia();

            ds = webS.RetornaEmpresas();

            cbEmpresa.ValueMember = "IDEmpresa";
            cbEmpresa.DisplayMember = "DSEmpresa";

            cbEmpresa.DataSource = ds.TBEmpresa;
            cbEmpresa.Update();

        }
        private void PreenchecbSetor(int IDEmpresa)
        {
            cbSetor.DataSource = null;
            cbSetor.Items.Clear();

            pontonarede.ServiceSoapClient webS = new pontonarede.ServiceSoapClient("ServiceSoap", utilConexao.tcm.UrlWS);

            pontonarede.DataSetPontoFrequencia ds = new pontonarede.DataSetPontoFrequencia();

            cbSetor.ValueMember = "IDSetor";
            cbSetor.DisplayMember = "DSSetor";

            ds = webS.SetorEmpresa(IDEmpresa);

            cbSetor.DataSource = ds.TBSetor;
            cbSetor.Update();

            cbSetor.SelectedIndex = -1;
        }
        private void cbEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                PreenchecbSetor(Convert.ToInt32(cbEmpresa.SelectedValue.ToString()));
            }
            catch (Exception ex)
            {
                ex.ToString();
                MessageBox.Show("Houve falha ao listar os setores referente ao órgão selecionado. Tente novamente ou contate o administrador.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Grid
        private void PreencheGrid()
        {
            DataSetpnrClientTableAdapters.TBSetorTableAdapter adpsetor = new DataSetpnrClientTableAdapters.TBSetorTableAdapter();
            DataSetpnrClient dsL = new DataSetpnrClient();
            adpsetor.Connection.ConnectionString = pnrClient.Properties.Settings.Default.ptServer35ConnectionString;
            adpsetor.Fill(dsL.TBSetor);

            if (dsL.TBSetor.Rows.Count > 0)
            {
                gridSetor.DataSource = dsL;
                gridSetor.DataMember = "TBSetor";
                gridSetor.Update();
            }
        }
        #endregion

        #region Botões

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            crudPtServer crud = new crudPtServer(utilConexao.tcm.UrlWS);
            //Selecionar as combos
            //if (cbEmpresa.SelectedIndex < 0)
            //{
            //    MessageBox.Show("Favor selecionar uma empresa para continuar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}
            //if (cbSetor.SelectedIndex < 0)
            //{
            //    MessageBox.Show("Favor selecionar um setor para continuar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            try
            {
                crud.InsertEmpresaSetor(cbEmpresa.SelectedValue.ToString(), cbEmpresa.Text, cbSetor.SelectedValue.ToString(), cbSetor.Text);
                //MessageBox.Show("A aplicação será reiniciada!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Apagar o banco local antes de reinicar.
                crud.DeleteUsuario();
                this.Close();
            }
            catch (Exception ex)
            {
                ex.ToString();
                MessageBox.Show("Houve falha ao tentar salvar arquivo de configuração. Verifique os filtros adicionados.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private void frmConfiguraEmpresaSetor_Load(object sender, EventArgs e)
        {
            try
            {
                PreenchecbEmpresa();
                PreencheGrid();
            }
            catch (Exception Ex)
            {
                Ex.ToString();
                MessageBox.Show("Houve falha ao tentar acessar o servidor de dados. Tente novamente ou contate o administrador.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
