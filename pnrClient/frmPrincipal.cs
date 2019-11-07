using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Net;
using System.Configuration;

using System.Timers;

using SecuGen.FDxSDKPro.Windows;
using static pnrClient.pontonarede.DataSetUsuario;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.ServiceModel;
using System.Threading;
using System.Web.UI;

namespace pnrClient
{
    public partial class frmPrincipal : Form
    {

        public frmPrincipal()
        {
            InitializeComponent();

            IniciaRelagio();

            InitializeBackgroundWorker();

            //INICIA HARDWARE LEITORA
            m_LedOn = false;
            m_FPM = new SGFingerPrintManager();

            
            utilConexao = new UtilConexao();
            _ComThread = false;
            _EnvioImediato = false;
            VerificaConexoes();
            _hashMaquina = Util.GetHash(utilConexao.tcm.UrlWS);

            if (string.IsNullOrEmpty(_hashMaquina))
            {
                string hash = "";
                pontonarede.ServiceSoapClient WebS = new pontonarede.ServiceSoapClient("ServiceSoap", utilConexao.tcm.UrlWS);
                WebS.Endpoint.Binding.OpenTimeout = TimeSpan.FromSeconds(10); //OpenTimeout p/ 3 e SendTimeout p 15
                WebS.Endpoint.Binding.SendTimeout = TimeSpan.FromSeconds(15);
                WebS.Endpoint.Binding.ReceiveTimeout = TimeSpan.FromSeconds(7);
                try
                {
                    ChaveHardware ch = new ChaveHardware(pnrClient.Properties.Settings.Default.ptServer35ConnectionString);
                    ch.SetaInfoHardware();
                    WebS.Open();
                    hash = WebS.GetHashMaquina(ch.SerialPROCESSADOR, ch.SERIALHD, ch.MACREDE);
                    WebS.Close();
                }
                catch
                {
                }
                Util.CriarHash(hash);
                _hashMaquina = Util.GetHash(utilConexao.tcm.UrlWS);

            }



        }
        #region Controles da leitora
        private SGFPMDeviceList[] m_DevList; // Used for EnumerateDevice

        private void IniciaSecugen2()
        {
            //Inicia
            Int32 error;
            SGFPMDeviceName device_name = SGFPMDeviceName.DEV_UNKNOWN;
            Int32 device_id = (Int32)SGFPMPortAddr.USB_AUTO_DETECT;

            openedSecugen = false;

            // Get device name
            device_name = SGFPMDeviceName.DEV_AUTO;

            if (device_name != SGFPMDeviceName.DEV_UNKNOWN)
            {
                error = m_FPM.Init(device_name);

                if (error == (Int32)SGFPMError.ERROR_NONE)
                {
                    m_FPM.CloseDevice();
                    error = m_FPM.OpenDevice(device_id);
                }

                if (error == (Int32)SGFPMError.ERROR_NONE)
                {
                    SGFPMDeviceInfoParam pInfo = new SGFPMDeviceInfoParam();
                    m_FPM.GetDeviceInfo(pInfo);
                    Device_ID = pInfo.FWVersion;
                    m_ImageWidth = pInfo.ImageWidth;
                    m_ImageHeight = pInfo.ImageHeight;
                }
            }
            else
                error = m_FPM.InitEx(m_ImageWidth, m_ImageHeight, m_Dpi);

            if (error == (Int32)SGFPMError.ERROR_NONE)
            {
                //EnableButtons(true);
                if (enableRegistro)
                {
                    //StatusBar.Text = "Dispositivo iniciado com sucesso.";
                    ColorStatusBar(true);
                }

            }
            else
            {
                if (enableRegistro)
                {
                    //EnableButtons(false);
                    // StatusBar.Text = "Falha ao iniciar dispositivo biométrico: " + error;
                    ColorStatusBar(false);
                    return;
                }
            }


            error = m_FPM.SetTemplateFormat(SGFPMTemplateFormat.ISO19794);

            // Get Max template size
            Int32 max_template_size = 0;
            error = m_FPM.GetMaxTemplateSize(ref max_template_size);

            m_RegMin1 = new Byte[max_template_size];
            m_RegMin2 = new Byte[max_template_size];
            m_VrfMin = new Byte[max_template_size];

            // OpenDevice if device is selected
            if (device_name != SGFPMDeviceName.DEV_UNKNOWN)
            {
                error = m_FPM.OpenDevice(device_id);
                if (error == (Int32)SGFPMError.ERROR_NONE)
                {
                    openedSecugen = true;
                }
                else
                {
                    //LG.Esrevelog("Erro ao iniciar o dispositivo", lbLog);
                    //EnableButtons(false);
                }
            }

            m_FPM.EnableAutoOnEvent(true, (int)this.Handle);
        }
        private void IniciaSecugen()
        {
            //Inicia
            Int32 error;
            SGFPMDeviceName device_name = SGFPMDeviceName.DEV_UNKNOWN;
            Int32 device_id = (Int32)SGFPMPortAddr.USB_AUTO_DETECT;

            openedSecugen = false;

            // Get device name
            device_name = SGFPMDeviceName.DEV_AUTO;

            if (device_name != SGFPMDeviceName.DEV_UNKNOWN)
            {
                error = m_FPM.Init(device_name);

                if (error == (Int32)SGFPMError.ERROR_NONE)
                {
                    m_FPM.CloseDevice();
                    error = m_FPM.OpenDevice(device_id);
                }

                if (error == (Int32)SGFPMError.ERROR_NONE)
                {
                    SGFPMDeviceInfoParam pInfo = new SGFPMDeviceInfoParam();
                    m_FPM.GetDeviceInfo(pInfo);
                    Device_ID = pInfo.FWVersion;
                    m_ImageWidth = pInfo.ImageWidth;
                    m_ImageHeight = pInfo.ImageHeight;
                }
            }
            else
                error = m_FPM.InitEx(m_ImageWidth, m_ImageHeight, m_Dpi);

            if (error == (Int32)SGFPMError.ERROR_NONE)
            {
                //EnableButtons(true);
                if (enableRegistro)
                {
                    StatusBar.Text = "Dispositivo iniciado com sucesso.";
                    ColorStatusBar(true);
                }

            }
            else
            {
                if (enableRegistro)
                {
                    //EnableButtons(false);
                    StatusBar.Text = "Falha ao iniciar dispositivo biométrico: " + error;
                    ColorStatusBar(false);
                    return;
                }
            }


            error = m_FPM.SetTemplateFormat(SGFPMTemplateFormat.ISO19794);

            // Get Max template size
            Int32 max_template_size = 0;
            error = m_FPM.GetMaxTemplateSize(ref max_template_size);

            m_RegMin1 = new Byte[max_template_size];
            m_RegMin2 = new Byte[max_template_size];
            m_VrfMin = new Byte[max_template_size];

            // OpenDevice if device is selected
            if (device_name != SGFPMDeviceName.DEV_UNKNOWN)
            {
                error = m_FPM.OpenDevice(device_id);
                if (error == (Int32)SGFPMError.ERROR_NONE)
                {
                    openedSecugen = true;
                }
                else
                {
                    LG.Esrevelog("Erro ao iniciar o dispositivo", lbLog);
                    //EnableButtons(false);
                }
            }

            m_FPM.EnableAutoOnEvent(true, (int)this.Handle);
        }
        private UtilConexao utilConexao;


        ///////////////////////
        /// EnumerateDevice(), GetEnumDeviceInfo()
        /// EnumerateDevice() can be called before Initializing SGFingerPrintManager
        public bool GetDeviceConnection()
        {
            Int32 iError;
            string enum_device;

            // Enumerate Device
            iError = m_FPM.EnumerateDevice();

            // Get enumeration info into SGFPMDeviceList
            if (m_FPM.NumberOfDevice > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        #endregion

        #region Variáveis
        //hash maquina
        private string _hashMaquina = "";
        private bool _ComThread = true;
        private bool _EnvioImediato = true;
        private Stopwatch _spBackGround = new Stopwatch();
        private pontonarede.ConfigHorasSincModel configSinc = null;
        private bool AtualizandoBaseLocal = false;
        //do WSGeral
        private string _UrlWSAnterior = "";
        pontonarede.ServiceSoapClient webS;// = new pontonarede.ServiceSoapClient();
        public pontonarede.DataSetUsuario dsU;// = new pontonarede.DataSetUsuario();
        private vwUsuarioWebServiceDataTable vwUsuarioFiltrados = new vwUsuarioWebServiceDataTable();
        public bool restartApplication;
        public DateTime DTRelogioData;
        pontonarede.ServiceSoapClient WebSRelogio;
        private bool loginEfetuado;
        public EscreverLog LG = new EscreverLog();
        private bool openedSecugen;
        private Int32 m_Dpi;
        private int Device_ID;

        //Controle de permissão de registro.
        private bool enableRegistro;

        //Variável que controla a verificação do envio de registros offline
        int zeraAvisos = 0; //de 3 em 3 minutos 180 ciclos, envia os registros feitos em off.
        bool msgRede;
        string msgrede;

        //Variáveis Secugen
        private SGFingerPrintManager m_FPM;

        private bool m_LedOn = false;
        private Int32 m_ImageWidth;
        private Int32 m_ImageHeight;
        private Byte[] m_RegMin1;
        private Byte[] m_RegMin2;
        private Byte[] m_VrfMin;
        private Byte[] templateCompare;
        //private SGFPMDeviceList[] m_DevList; // Used for EnumerateDevice
        //criando templates
        private Byte[] m_StoredTemplate;
        //private Int32 max_template_size = 0;


        //Int32 iError;

        //Variaveis do form e globais
        public bool AlteracaoBiometrica; //Se for verdadeira, a gente manda fazer update. senão....
        public int IDEmpresa, IDSetor, IdUsuarioRegistro;
        int IDTPUsuario, idusuario;
        crudPtServer crud;
        public string Nome, PrimeiroNome, Matricula, CPF, SenhaAdmin, ColaboradoresInseridos, ColaboradoresNaoInseridos, nomeusuario, matricula, text;
        byte[] Template1, Template2;
        private string msg, primeiroNome, CPFLogin, Nomegrid;
        public string dsSetor;
        //dataset dos usuários
        DataSetpnrClient dsLp = new DataSetpnrClient();

        //para evitar abrir duas instâncias
        private static int WM_QUERYENDSESSION = 0x11;

        //variável setor Trocado: Para controlar se houve a troca do setor e permitir que o label receba o novo setor
        // assim como a variácel IDSetor.
        public bool ChangeSetor = false;

        //20/07/2018 - Controle de importação.
        string lbControleimportacao, Inseridos, NomeInserido, nomeAtual;
        int TotalDaImporacao;
        bool FinishImportation;

        //02/08/2018 - Evitando falso Positivo
        int matchingScore, IDUsuarioLocalizado;
        int cont = 0;
        int scoreRegistro;

        //05/08/2018
        int ContadorRelogio;
        pontonarede.ServiceSoapClient WebSRelogio2;

        //21/08/2018
        string msg2; //registro em backgroundWorker
        Bitmap imgBio;

        //23/08/2018
        byte[] Template1Cad = null; byte[] Template2Cad = null;

        //29/08/2018 Banco local apenas com os usuários do setor.
        DataTable TBUsuarioLocalSetor;


        #endregion

        #region Grid Registros locais

        private void GetGridRegistroLocal()
        {
            DataSetpnrClient ds = new DataSetpnrClient();
            DataSetpnrClientTableAdapters.TBFrequenciaLocalTableAdapter adpFreq = new DataSetpnrClientTableAdapters.TBFrequenciaLocalTableAdapter();
            adpFreq.Connection.ConnectionString = pnrClient.Properties.Settings.Default.ptServer35ConnectionString;
            try
            {
                //gridFrequencia.AutoGenerateColumns = false;
                adpFreq.Fill(ds.TBFrequenciaLocal);
                gridFrequencia.DataSource = ds.TBFrequenciaLocal;
                gridFrequencia.Update();
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

        }

        #endregion

        #region Controle de horas

        private DateTime HoraServidor2()
        {
            WebSRelogio2 = new pontonarede.ServiceSoapClient("ServiceSoap", utilConexao.tcm.UrlWS);
            WebSRelogio2.Endpoint.Binding.OpenTimeout = TimeSpan.FromSeconds(2);
            WebSRelogio2.Endpoint.Binding.SendTimeout = TimeSpan.FromSeconds(3);
            try
            {
                WebSRelogio2.Open();
                DTRelogioData = WebSRelogio2.HoraServidor();
                WebSRelogio2.Close();
                return DTRelogioData.AddSeconds(25);
            }
            catch (Exception ex)
            {
                WebSRelogio2.Close();
                return DateTime.Now;
            }

        }
        private void HoraServidor()
        {
            WebSRelogio.Endpoint.Binding.OpenTimeout = TimeSpan.FromSeconds(2);
            WebSRelogio.Endpoint.Binding.SendTimeout = TimeSpan.FromSeconds(3);
            try
            {
                WebSRelogio.Open();
                DTRelogioData = WebSRelogio.HoraServidor();
                WebSRelogio.Close();
                DTRelogioData = DTRelogioData.AddSeconds(25);
                lbRelogio.Text = DTRelogioData.ToLongTimeString();
            }
            catch (Exception ex)
            {
                WebSRelogio.Close();
                ex.ToString();
                DTRelogioData = DateTime.Now;
            }

        }
        #endregion

        #region Timers
        private void Relogio_Tick(object sender, EventArgs e)
        {

            if (DTRelogioData.Hour == 15 && DTRelogioData.Minute == 5)
            {
                //RegistroLocais RL = new RegistroLocais();
                try
                {
                    //RL.RegistroComFalha(IDEmpresa, idsetor);
                }
                catch
                {
                }
            }
            lbRelogio.Update();

            if (!bwRegistro.IsBusy)
            {
                bwRegistro.RunWorkerAsync();
                //29/05/2018 - Mudei a busca pela hora para backuground.
                lbRelogio.Text = DTRelogioData.ToLongTimeString();
                lbData.Text = DTRelogioData.ToShortDateString();
                // -------------------------------------------
                if (msgRede)
                {
                    lbrede.Text = msgrede;
                    lbrede.Visible = true;
                    StatusBar.Text = msgrede;
                    ColorStatusBar(false);
                }
                else
                {
                    lbrede.Visible = false;
                    //Caso o dsU estiver vazio, faz o Fill /05/08/2018
                    if (dsU != null)
                    {
                        if (dsU.vwUsuarioWebService.Rows.Count == 0)
                        {
                            GetUsuariosWS();
                        }
                    }
                    if (enableRegistro)
                    {
                        StatusBar.Text = string.Empty;
                        ColorStatusBar(true);
                    }
                }


                lbrede.Update();

                //Atualização de Digitais no banco local.
                if (configSinc != null && !AtualizandoBaseLocal)
                {
                    if (System.DateTime.Now >= Convert.ToDateTime(string.Format("{0} " + configSinc.SegundaSincHoraInicio, System.DateTime.Now.Date.ToShortDateString()))
                        && System.DateTime.Now <= Convert.ToDateTime(string.Format("{0} " + configSinc.SegundaSincHoraFim, System.DateTime.Now.Date.ToShortDateString())))
                    {
                        LG.Esrevelog("Atualizando banco biométrico, aguarde por favor!", lbLog);
                        AtualizandoBaseLocal = true;
                        if (IDEmpresa == 0)
                        {
                            crud.GetIDEmpresa();
                            crud.GetIDSetor();
                        }
                        try
                        {
                            var thread = new Thread(() =>
                            {
                                crud.ImportUsuarios(IDEmpresa, IDSetor);
                                AtualizandoBaseLocal = false;
                            });
                            thread.Start();

                        }
                        catch
                        {
                            LG.Esrevelog("", lbLog);
                        }

                        LG.Esrevelog("", lbLog);
                    }

                    if (System.DateTime.Now >=
                            Convert.ToDateTime(string.Format("{0} " + configSinc.PrimeiraSincHoraInicio, System.DateTime.Now.Date.ToShortDateString())) && System.DateTime.Now
                            <= Convert.ToDateTime(string.Format("{0} " + configSinc.PrimeiraSincHoraFim, System.DateTime.Now.Date.ToShortDateString())))
                    {
                        AtualizandoBaseLocal = true;
                        LG.Esrevelog("Atualizando banco biométrico, aguarde por favor!", lbLog);
                        try
                        {
                            var thread = new Thread(() =>
                            {
                                crud.ImportUsuarios(IDEmpresa, IDSetor);
                                AtualizandoBaseLocal = false;
                            });
                            thread.Start();
                        }
                        catch
                        {
                            LG.Esrevelog("", lbLog);
                        }
                        if (System.DateTime.Now >=
                                Convert.ToDateTime(string.Format("{0} 10:32:00.000", System.DateTime.Now.Date.ToShortDateString())) && System.DateTime.Now
                                <= Convert.ToDateTime(string.Format("{0} 10:32:02.000", System.DateTime.Now.Date.ToShortDateString())))
                        {
                            LG.Esrevelog("Atulizando banco biométrico, aguarde por favor!", lbLog);
                            try
                            {
                                crud.ImportUsuarios(IDEmpresa, IDSetor);
                            }
                            catch
                            {
                                if (System.DateTime.Now >=
                                        Convert.ToDateTime(string.Format("{0} 10:05:00.000", System.DateTime.Now.Date.ToShortDateString())) && System.DateTime.Now
                                        <= Convert.ToDateTime(string.Format("{0} 10:05:02.000", System.DateTime.Now.Date.ToShortDateString())))
                                {
                                    LG.Esrevelog("Atulizando banco biométrico, aguarde por favor!", lbLog);
                                    try
                                    {
                                        crud.ImportUsuarios(IDEmpresa, IDSetor);
                                    }
                                    catch
                                    {
                                        LG.Esrevelog("", lbLog);
                                    }
                                }
                                else if (configSinc != null && AtualizandoBaseLocal)
                                {
                                    StatusBar.Text = "Atualizando banco biométrico...";
                                }


                                if (System.DateTime.Now >=
                        Convert.ToDateTime(string.Format("{0} 16:52:00.000", System.DateTime.Now.Date.ToShortDateString())) && System.DateTime.Now
                        <= Convert.ToDateTime(string.Format("{0} 16:52:05.000", System.DateTime.Now.Date.ToShortDateString())))
                                {
                                    try
                                    {
                                        //Envia Ocorrencias.
                                        crud.EnviaOcorrencia();
                                    }
                                    catch (Exception ex)
                                    {
                                        ex.ToString();
                                    }
                                }
                            }

                            if (zeraAvisos == 5500 && enableRegistro)
                            {
                                LG.Esrevelog("FAÇA A SUA MARCAÇÃO...", lbLog);
                                lbLog.ForeColor = System.Drawing.Color.ForestGreen;
                                StatusBar.Text = string.Empty;
                                zeraAvisos = 0;
                            }

                            zeraAvisos++;

                        }
                    }
                }
            }
        }
                    
                 
        #endregion

        #region TabControl
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Caso 0 apaga o log já feito.
            //Cas0 1 Carrega a Grid.
            if (loginEfetuado)
            {
                switch (tabControl1.SelectedIndex)
                {
                    case 0:
                        lbLogUsuario.Items.Clear();
                        StatusBar.Text = "Atualizando a base de usuários...";
                        enableRegistro = true;
                        ColorStatusBar(true);
                        if (AlteracaoBiometrica)// Se houve atualização biométrica, busca no ws.
                            GetUsuariosWS(); //- Atualiza na importação e no abrir e fechar o programa.
                                             //Zera a template e o IDusuario para privinir de salvar um em outro usuário.
                        m_StoredTemplate = null;
                        idusuario = 0;
                        pictureBoxR1.Image = null;
                        pictureBoxR2.Image = null;
                        pictureBoxR1.Update();
                        pictureBoxR2.Update();
                        tbNome.Text = string.Empty;
                        break;
                    case 1:
                        //Caso a aba seja a de cadastro. não permite registrar frequencia
                        enableRegistro = false;
                        PreencheGrid(IDEmpresa);
                        break;
                    case 2:
                        enableRegistro = false;
                        m_StoredTemplate = null;
                        idusuario = 0;
                        tbNome.Text = string.Empty;
                        break;
                    case 3:
                        enableRegistro = false;
                        m_StoredTemplate = null;
                        idusuario = 0;
                        //GetGridRegistroLocal();
                        GetGridRegistroLocal();
                        tbNome.Text = string.Empty;
                        break;
                }
            }


        }
        #endregion

        #region Grid de Usuários. Carregando com o banco local.
        //Controle dos checkBox.
        private void cbLogin_CheckedChanged(object sender, EventArgs e)
        {
            if (cbLogin.Checked)
            {
                checkBox1.Checked = false;
                cbCPF.Checked = false;
            }
        }
        //
        private void gridUsuario_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != (-1)) // Se não for o cabeçalho faz o procedimento abaixo. Caso contrário, apenas ordenar.
            {
                frmConfirmaCadastro frmconfirma = new frmConfirmaCadastro();
                frmconfirma.pergunta = "Cadastrar/Alterar biometria para: ";
                frmconfirma.nome = gridUsuario.SelectedRows[0].Cells[0].Value.ToString();
                frmconfirma.ShowDialog();

                if (frmconfirma.permite)
                {
                    idusuario = int.Parse("0");
                    idusuario = Convert.ToInt32(gridUsuario.SelectedRows[0].Cells[2].Value);
                    Nomegrid = gridUsuario.SelectedRows[0].Cells[0].Value.ToString();
                    StatusBar.Text = string.Format("Mantenção biométrica para {0}", gridUsuario.SelectedRows[0].Cells[0].Value.ToString());
                }
                else
                    idusuario = int.Parse("0");
            }
        }
        private void PreencheGrid(int IDEmpresa)
        {
            if (dsU.vwUsuarioWebService.Rows.Count == 0)
            {
                DataSetpnrClient dsL = new DataSetpnrClient();
                gridUsuario.AutoGenerateColumns = false;
                crud.GetTBUsuario(dsL);
                gridUsuario.DataSource = dsL.TBUsuarioLocal;
                //gridUsuario.DataMember = dsL.TBUsuarioLocal.ToString();
            }
            else if (dsU.vwUsuarioWebService.Rows.Count <= 2)
            {
                GetUsuariosWS();
                gridUsuario.AutoGenerateColumns = false;
                gridUsuario.DataSource = dsU.vwUsuarioWebService;
            }
            else
            {
                gridUsuario.AutoGenerateColumns = false;
                gridUsuario.DataSource = dsU.vwUsuarioWebService;
            }
            gridUsuario.Update();
        }
        private void PreencheGridNome(int IDEmpresa, string Nome)
        {
            //busca por nome como era antes.
            pontonarede.DataSetUsuario dsU;
            try
            {
                webS = new pontonarede.ServiceSoapClient("ServiceSoap", utilConexao.tcm.UrlWS);
                dsU = new pontonarede.DataSetUsuario();
                webS.Endpoint.Binding.OpenTimeout = TimeSpan.FromSeconds(3); //OpenTimeout p/ 3 e SendTimeout p 15
                webS.Endpoint.Binding.SendTimeout = TimeSpan.FromSeconds(10);
                webS.Endpoint.Binding.ReceiveTimeout = TimeSpan.FromSeconds(15);
                webS.Open();
                dsU = webS.SearchNomeCPFLogin(tbNome.Text.Trim(), 1, IDEmpresa, "TentoWebServiceNovamente7x24dm12");
                webS.Close();
                gridUsuario.DataSource = dsU.vwUsuarioWebService;

            }
            catch
            {
                DataSetpnrClient dsL = new DataSetpnrClient();
                gridUsuario.AutoGenerateColumns = false;
                crud.GetTBUsuarioNome(dsL, IDEmpresa, Nome);
                gridUsuario.DataSource = dsL.TBUsuarioLocal;
                //gridUsuario.DataMember = dsL.TBUsuarioLocal.ToString();
            }
            gridUsuario.Update();
        }
        private void PreencheGridMatricula(int IDEmpresa, string Matricula)
        {
            pontonarede.DataSetUsuario dsU;
            try
            {
                webS = new pontonarede.ServiceSoapClient("ServiceSoap", utilConexao.tcm.UrlWS);
                dsU = new pontonarede.DataSetUsuario();
                webS.Endpoint.Binding.OpenTimeout = TimeSpan.FromSeconds(3); //OpenTimeout p/ 3 e SendTimeout p 15
                webS.Endpoint.Binding.SendTimeout = TimeSpan.FromSeconds(10);
                webS.Endpoint.Binding.ReceiveTimeout = TimeSpan.FromSeconds(15);
                webS.Open();
                dsU = webS.SearchNomeCPFLogin(tbNome.Text.Trim(), 2, IDEmpresa, "TentoWebServiceNovamente7x24dm12");
                gridUsuario.DataSource = dsU.vwUsuarioWebService;
                webS.Close();
            }
            catch
            {
                DataSetpnrClient dsL = new DataSetpnrClient();
                gridUsuario.AutoGenerateColumns = false;
                crud.GetTBUsuarioMatricula(dsL, IDEmpresa, Matricula);
                gridUsuario.DataSource = dsL.TBUsuarioLocal;
                //gridUsuario.DataMember = dsL.TBUsuarioLocal.ToString();
            }

            gridUsuario.Update();
        }
        private void PreencheGridCPF(int IDEmpresa, string CPF)
        {
            pontonarede.DataSetUsuario dsU;
            try
            {
                webS = new pontonarede.ServiceSoapClient("ServiceSoap", utilConexao.tcm.UrlWS);
                dsU = new pontonarede.DataSetUsuario();
                webS.Endpoint.Binding.OpenTimeout = TimeSpan.FromSeconds(3); //OpenTimeout p/ 3 e SendTimeout p 15
                webS.Endpoint.Binding.SendTimeout = TimeSpan.FromSeconds(10);
                webS.Endpoint.Binding.ReceiveTimeout = TimeSpan.FromSeconds(15);
                webS.Open();
                dsU = webS.SearchNomeCPFLogin(tbNome.Text.Trim(), 3, IDEmpresa, "TentoWebServiceNovamente7x24dm12");
                gridUsuario.DataSource = dsU.vwUsuarioWebService;
                webS.Close();
            }
            catch
            {
                DataSetpnrClient dsL = new DataSetpnrClient();
                gridUsuario.AutoGenerateColumns = false;
                crud.GetTBUsuarioCPF(dsL, IDEmpresa, CPF);
                gridUsuario.DataSource = dsL.TBUsuarioLocal;
                //gridUsuario.DataMember = dsL.TBUsuarioLocal.ToString();
            }

            gridUsuario.Update();
        }
        #endregion

        #region Controles do form Principal

        private void frmPrincipal_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_FPM.CloseDevice();
        }
        private void SystemEvents_SessionSwitch(object source, Microsoft.Win32.SessionSwitchEventArgs e)
        {
            if (e.Reason == Microsoft.Win32.SessionSwitchReason.ConsoleConnect || e.Reason == Microsoft.Win32.SessionSwitchReason.SessionLock)
            {
                Application.Exit();
            }
        }
        private void ColorStatusBar(bool situacao)
        {
            switch (situacao)
            {
                case true:
                    StatusBar.ForeColor = System.Drawing.Color.Green;
                    break;
                case false:
                    StatusBar.ForeColor = System.Drawing.Color.Red;
                    break;
            }
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            WebSRelogio = new pontonarede.ServiceSoapClient("ServiceSoap", utilConexao.tcm.UrlWS);

            //Arquivo de configuração Empresa Setor - se Não houver um idlocal pra ambos, chama a tela de configuração
            crud = new crudPtServer(utilConexao.tcm.UrlWS);
            crud.VerificaTBLogConexao();
            lbrede.Visible = false;
            IDEmpresa = crud.GetIDEmpresa();
            //permite registrar
            enableRegistro = true;
            //Pega a hora do servidor.
            HoraServidor();

            if (IDEmpresa == 0)
            {
                //Tela de configuração Empresa e setor.
                MessageBox.Show("Para iniciar, será necessário configurar empresa e setor.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                frmConfiguraEmpresaSetor frmConfig = new frmConfiguraEmpresaSetor();
                frmConfig.ShowDialog();
            }
            else
            {
                IDSetor = crud.GetIDSetor();
                lbSetor.Text = crud.DSSETOR;
            }
            //Refaz a busca.
            IDEmpresa = crud.GetIDEmpresa();
            IDSetor = crud.GetIDSetor();
            lbSetor.Text = crud.DSSETOR;
            labelEmpresa.Text = crud.DSEMPRESA;


            //Preenche Tabela para busca de usuários via biometria.
            //GetUsuariosWS();
            GetUsuariosWS();
            //new Action(GetUsuariosWS).BeginInvoke(new AsyncCallback(OnKillProcessComplete), null);

            //Iniciando a leitora

            IniciaSecugen();

            //Inicia sempre com a tabpage 0
            tabControl1.SelectedTab = tabControl1.TabPages[0];
            //AjustaConexaoLocal();

            //20/04/2018 - Só p testar SQLite - descontinuado sqlite não suporta bytes[]
            //crudPtServer crud = new crudPtServer();
            //DataTable TB = new DataTable();
            //crud.GetUsuario(TB, 4);

            //Chave de hardware. Para identificar o sistema em uso e seus dados.
            try
            {
                ChaveHardware ch = new ChaveHardware(pnrClient.Properties.Settings.Default.ptServer35ConnectionString);
                //ch.VerificaRegistro();

                //se não existir , cria a tabela
                if (!crud.TabelaExistTBUL())
                {
                    crud.CreateTBUsuarioLocalSetor(IDSetor);
                }
                ch.EnviaInformacaohardware(IDEmpresa, IDSetor, string.Format(lblVersaoSistema.Text, Device_ID.ToString()), IPLocal(), GetComputadorUserOS(1), _hashMaquina, utilConexao.tcm.UrlWS);
            }
            catch (Exception ex)
            {
                StatusBar.Text = string.Format("Impossível obter informações de hardware: {0}", ex.Message.Trim());
            }
            utilConexao.FinalizaCronometro();
        }

        private void frmPrincipal_Shown(object sender, EventArgs e)
        {
            _ComThread = true;
            _EnvioImediato = true;
            VerificaConexoes();
            // Do blocking stuff here
        }

        protected override void WndProc(ref Message m)
        {
            //if (m.Msg == WM_QUERYENDSESSION)
            //{
            //    MessageBox.Show("Impossível abrir duas instâncias do aplicativo. O mesmo será encerrado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    Application.Exit();
            //}
            //else
            //{
            //    Microsoft.Win32.SystemEvents.SessionSwitch += new Microsoft.Win32.SessionSwitchEventHandler(SystemEvents_SessionSwitch);
            //}
            //Registro.
            if (m.Msg == (int)SGFPMMessages.DEV_AUTOONEVENT)
            {
                if (m.WParam.ToInt32() == (Int32)SGFPMAutoOnEvent.FINGER_ON)
                {
                    if (enableRegistro)
                    {
                        lbLog.ForeColor = Color.Green;
                        LG.Esrevelog("IDENTIFICANDO...", lbLog);

                        if (!bwRegistroPonto.IsBusy)
                            bwRegistroPonto.RunWorkerAsync();
                        //RegistrarFrequencia();
                    }
                }
            }
            if (!loginEfetuado)
            {
                tabControl1.SelectedTab = tabControl1.TabPages[0];
            }

            base.WndProc(ref m);
        }
        #endregion
        #region Ajustando a conexão com o banco local.
        public void AjustaConexaoLocal()
        {
            var config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var connStringSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
            connStringSection.ConnectionStrings["ptServer35ConnectionString"].ConnectionString = "Data Source=|DataDirectory|\\ptServer35.sdf;Password=dbpontoSADCTI";
            ConfigurationManager.RefreshSection("connStringSection");
        }
        #endregion
        #region Controle das Imagens
        private void DrawImage(Byte[] imgData, PictureBox picBox)
        {
            int colorval;
            Bitmap bmp = new Bitmap(m_ImageWidth, m_ImageHeight);
            picBox.Image = (Image)bmp;

            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    colorval = (int)imgData[(j * m_ImageWidth) + i];
                    bmp.SetPixel(i, j, Color.FromArgb(colorval, colorval, colorval));
                }
            }
            picBox.Refresh();
        }
        private Bitmap DrawImage2(Byte[] imgData)
        {
            int colorval;
            Bitmap bmp = new Bitmap(m_ImageWidth, m_ImageHeight);

            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    colorval = (int)imgData[(j * m_ImageWidth) + i];
                    bmp.SetPixel(i, j, Color.FromArgb(colorval, colorval, colorval));
                }
            }
            return bmp;
        }
        private void GetImage()
        {
            Int32 iError;
            Int32 elap_time;
            Byte[] fp_image;

            elap_time = Environment.TickCount;
            fp_image = new Byte[m_ImageWidth * m_ImageHeight];

            iError = m_FPM.GetImage(fp_image);

            if (iError == (Int32)SGFPMError.ERROR_NONE)
            {
                elap_time = Environment.TickCount - elap_time;
                DrawImage(fp_image, staticPB);
                //StatusBar.Text = "Capturado em: " + elap_time + " ms";
            }
            //else
            //DisplayError("GetImage()", iError);
        }
        private void Imagens()
        {
            SGFPMDeviceInfoParam pInfo = new SGFPMDeviceInfoParam();
            Int32 iError = m_FPM.GetDeviceInfo(pInfo);

            if (iError == (Int32)SGFPMError.ERROR_NONE)
            {
                m_ImageWidth = pInfo.ImageWidth;
                m_ImageHeight = pInfo.ImageHeight;

                //textDeviceID.Text = Convert.ToString(pInfo.DeviceID);
                //textImageDPI.Text	   = Convert.ToString(pInfo.ImageDPI);
                //textFWVersion.Text	   = Convert.ToString(pInfo.FWVersion, 16);

                //ASCIIEncoding encoding = new ASCIIEncoding();
                //textSerialNum.Text = encoding.GetString(pInfo.DeviceSN);

                //textImageHeight.Text = Convert.ToString(pInfo.ImageHeight);
                //textImageWidth.Text  = Convert.ToString(pInfo.ImageWidth);
                //textBrightness.Text   = Convert.ToString(pInfo.Brightness);
                //textContrast.Text	   = Convert.ToString(pInfo.Contrast);
                //textGain.Text         = Convert.ToString(pInfo.Gain);

                BrightnessUpDown.Value = pInfo.Brightness;
            }
        }
        #endregion

        #region Botões
        private void btApagar_Click(object sender, EventArgs e)
        {
            try
            {
                DataSetpnrClientTableAdapters.TBFrequenciaLocalTableAdapter adpFrequencia = new DataSetpnrClientTableAdapters.TBFrequenciaLocalTableAdapter();
                adpFrequencia.Connection.ConnectionString = pnrClient.Properties.Settings.Default.ptServer35ConnectionString;
                adpFrequencia.DeleteGeral();
                //GetGridRegistroLocal();
                GetGridRegistroLocal();
                MessageBox.Show("Registros excluídos com sucesso.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Houve a seguinte falha ao excluir os registros: " + ex.Message.ToString(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btEnviar_Click(object sender, EventArgs e)
        {
            StatusBar.ForeColor = System.Drawing.Color.ForestGreen;
            StatusBar.Text = "Enviando registros. Aguarde.";
            //System.Threading.Thread.Sleep(3000);
            StatusBar.Text = "Enviando registros. Aguarde..";
            //System.Threading.Thread.Sleep(3000);
            StatusBar.Text = "Enviando registros. Aguarde...";
            RegistroLocal RL = new RegistroLocal();
            RL.EnviarFrequencia(System.DateTime.Now, IDEmpresa, IDSetor);
            StatusBar.Text = "Registros enviados com sucesso!";
            MessageBox.Show("Registros enviados com sucesso.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            StatusBar.Visible = false;
        }
        private void btRegistroManual_Click(object sender, EventArgs e)
        {
            frmRegistrarFrequenciaManual frmRegistro = new frmRegistrarFrequenciaManual();
            frmRegistro.IDEmpresa = IDEmpresa;
            frmRegistro.DTfrequencia = DTRelogioData;
            frmRegistro.Show();
        }
        private void btConfigLocal_Click(object sender, EventArgs e)
        {
            frmConfiguraEmpresaSetor frmConfigEmpresaSetor = new frmConfiguraEmpresaSetor();
            frmConfigEmpresaSetor.Show();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (idusuario == 0)
            {
                MessageBox.Show("Selecione um usuário para cadastrar a biometria.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (m_StoredTemplate == null)
            {
                MessageBox.Show("Biometria não formada. Impossível salvar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                StatusBar.Text = crud.InsertModifyTemplate2(IDEmpresa, idusuario, Template1Cad, Template2Cad, CPFLogin);

                if (StatusBar.Text.IndexOf("sucesso") > 0)
                {
                    MessageBox.Show(StatusBar.Text + " para: " + Nomegrid, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ColorStatusBar(true);
                }
                else
                {
                    MessageBox.Show(StatusBar.Text, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (!crud.InsertTemplate2(idusuario, Template1Cad, Template2Cad))
                {
                    StatusBar.Text += string.Format("{0}. Obs: Registro não incluído localmente.");
                    ColorStatusBar(false);
                }

                pictureBoxR1.Image = null;
                pictureBoxR2.Image = null;
                pictureBoxR1.Update();
                pictureBoxR2.Update();
                AlteracaoBiometrica = true;
                Nomegrid = string.Empty;
            }
            catch (Exception ex)
            {
                StatusBar.Text = "Falha ao incluir a biometria: " + ex.Message.Trim();
                ColorStatusBar(false);
            }
            //zera as variáveis para não ocorrer de cadastrar a biometria de um em outro usuário.
            idusuario = int.Parse("0");
            m_StoredTemplate = null;
            Template1Cad = null;
            Template2Cad = null;
        }
        private void btListar_Click(object sender, EventArgs e)
        {
            tbNome.Text = string.Empty;
            PreencheGrid(IDEmpresa);
        }
        private void btBuscar_Click(object sender, EventArgs e)
        {
            if (tbNome.Text.Length <= 3)
            {
                MessageBox.Show("Para buscar, informe nome, mátricula ou CPF com mais de 3 caracteres", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (tbNome.Text != "")
            {
                if (checkBox1.Checked)
                {
                    // TODO: This line of code loads data into the 'pontoFrequenciaDataSet.vwUsuariogrid' table. You can move, or remove it, as needed.
                    //this.vwUsuariogridTableAdapter.FillNome(this.pontoFrequenciaDataSet.vwUsuariogrid, tbNome.Text.Trim());
                    PreencheGridNome(IDEmpresa, tbNome.Text.Trim());
                }
                else if (cbLogin.Checked)
                {
                    //bem aqui outro fill 
                    //this.vwUsuariogridTableAdapter.FillLogin(this.pontoFrequenciaDataSet.vwUsuariogrid, tbNome.Text.Trim());
                    PreencheGridMatricula(IDEmpresa, tbNome.Text.Trim());
                }
                else if (cbCPF.Checked)
                {
                    PreencheGridCPF(IDEmpresa, tbNome.Text.Trim());
                }


            }
            else
            {
                MessageBox.Show("Para buscar, informe o nome ou a matrícula do usuário", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btcarregar_Click(object sender, EventArgs e)
        {
            //Rodar em backGround
            //lbgeral.Text = "Total de importados: 0";
            //Desativa o botão para não clicarem novamente.
            btcarregar.Enabled = false;
            btCancelar.Enabled = true;
            lbLogUsuario.Items.Clear();
            GetUsuariosWS2();
            if (dsU.vwUsuarioWebService.Rows.Count < 100)
                ControleProgressbar(100);
            else
                ControleProgressbar(dsU.vwUsuarioWebService.Rows.Count);

            //Sem parâmetro por enquanto.
            bwImportacao.RunWorkerAsync();
        }
        private void btfechar_Click(object sender, EventArgs e)
        {
            lbLogUsuario.Items.Clear();
        }
        private void btLogar_Click(object sender, EventArgs e)
        {
            //Tela de login
            frmLoginAdministrador frmLogin = new frmLoginAdministrador(utilConexao.tcm.UrlWS);

            if (btLogar.Text == "Fazer login")
            {

                // cbUsuario.Text = "";
                //cbUsuario.Enabled = false;
                // cbUsuario.Update();
                frmLogin.IDEmpresa = IDEmpresa;

                frmLogin.ShowDialog();
                //btRegistrarFrequencia.Enabled = false;
                //cbUsuario.Enabled = false;


                loginEfetuado = frmLogin.acesso;
                IDTPUsuario = frmLogin.IDTPUsuario;

                if (loginEfetuado)
                {
                    btLogar.Text = "Logout";
                    CPFLogin = frmLogin.CPF;
                }
            }
            else
            {
                frmLogin.Close();
                btLogar.Text = "Fazer login";
                //btRegistrarFrequencia.Enabled = true;

                loginEfetuado = false;
                CPFLogin = string.Empty;
            }
        }

        //Capturas
        private void BtnCaptura1_Click(object sender, EventArgs e)
        {
            //Supondo que mesmo assim ele tentou cadastrar outra
            if (Template1Cad != null && Template2Cad != null)
            {
                if (MessageBox.Show("DESEJA REPETIR O REIGISTROS DA(S) BIOMETRIA(S). O PROCESSO JÁ REGISTRADO SERÁ PERDIDO.", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Template1Cad = null;
                    Template2Cad = null;
                }
                else
                {
                    return;
                }
            }

            if (idusuario == 0)
            {
                MessageBox.Show("Selecione um usuário para cadastrar a biometria.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            zeraAvisos = 0;
            Byte[] fp_image = new Byte[m_ImageWidth * m_ImageHeight];
            Int32 error = (Int32)SGFPMError.ERROR_NONE;
            Int32 img_qlty = 0;

            if (openedSecugen)
                error = m_FPM.GetImage(fp_image);
            else
            {
                StatusBar.Text = "Leitora biométrica não iniciada corretamente.";
                ColorStatusBar(false);
                MessageBox.Show("Leitora biométrica não iniciada corretamente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (error == (Int32)SGFPMError.ERROR_NONE)
            {
                m_FPM.GetImageQuality(m_ImageWidth, m_ImageHeight, fp_image, ref img_qlty);
                progressBar_R1.Value = img_qlty;

                DrawImage(fp_image, pictureBoxR1);

                SGFPMFingerInfo finger_info = new SGFPMFingerInfo();
                finger_info.FingerNumber = SGFPMFingerPosition.FINGPOS_UK;
                finger_info.ImageQuality = (Int16)img_qlty;
                finger_info.ImpressionType = (Int16)SGFPMImpressionType.IMPTYPE_LP;
                finger_info.ViewNumber = 1;

                // CreateTemplate
                error = m_FPM.CreateTemplate(finger_info, fp_image, m_RegMin1);

                if (error == (Int32)SGFPMError.ERROR_NONE)
                {
                    StatusBar.Text = "Imagem 1 capturada com sucesso. Capture a segunda imagem!";
                    //MessageBox.Show("Imagem 1 capturada com sucesso. Capture a segunda imagem!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ColorStatusBar(true);
                }

                else
                    DisplayError("GetImage()", error);
            }
            else
                DisplayError("GetImage()", error);
        }

        private void BtnCaptura2_Click(object sender, EventArgs e)
        {
            //Supondo que mesmo assim ele tentou cadastrar outra
            if (Template1Cad != null && Template2Cad != null)
            {
                if (MessageBox.Show("DESEJA REPETIR O(S) REGISTRO(S) DA(S) BIOMETRIA(S)? O PROCESSO JÁ REGISTRADO SERÁ PERDIDO.", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Template1Cad = null;
                    Template2Cad = null;
                }
                else
                {
                    return;
                }
            }
            if (idusuario == 0)
            {
                MessageBox.Show("Selecione um usuário para cadastrar a biometria.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            zeraAvisos = 0;
            Byte[] fp_image = new Byte[m_ImageWidth * m_ImageHeight];
            Int32 error = (Int32)SGFPMError.ERROR_NONE;
            Int32 img_qlty = 0;

            if (openedSecugen)
                error = m_FPM.GetImage(fp_image);
            else
            {
                StatusBar.Text = "Leitora biométrica não iniciada corretamente.";
                MessageBox.Show("Leitora biométrica não iniciada corretamente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ColorStatusBar(false);
                return;
            }

            m_FPM.GetImageQuality(m_ImageWidth, m_ImageHeight, fp_image, ref img_qlty);
            progressBar_R2.Value = img_qlty;

            if (error == (Int32)SGFPMError.ERROR_NONE)
            {
                DrawImage(fp_image, pictureBoxR2);

                SGFPMFingerInfo finger_info = new SGFPMFingerInfo();
                finger_info.FingerNumber = (SGFPMFingerPosition)0;
                finger_info.ImageQuality = (Int16)img_qlty;
                finger_info.ImpressionType = (Int16)SGFPMImpressionType.IMPTYPE_LP;
                finger_info.ViewNumber = 1;

                error = m_FPM.CreateTemplate(finger_info, fp_image, m_RegMin2);

                if (error == (Int32)SGFPMError.ERROR_NONE)
                {
                    StatusBar.Text = "Imagem 2 capturada com sucesso. Clique em Registrar.";
                    //MessageBox.Show("Imagem 2 capturada com sucesso. Clique em Registrar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ColorStatusBar(true);
                }
                else
                {
                    DisplayError("GetImage()", error);
                }
            }
            else
                DisplayError("GetImage()", error);
        }

        private void BtRegistrar_Click(object sender, EventArgs e)
        {
            if (idusuario == 0)
            {
                MessageBox.Show("Selecione um usuário para cadastrar a biometria.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            zeraAvisos = 0;
            //Int32 iError;
            //bool matched = false;
            //Int32 match_score = 0;
            //SGFPMSecurityLevel secu_level; //

            //secu_level = SecuGen.FDxSDKPro.Windows.SGFPMSecurityLevel.HIGH;

            //iError = m_FPM.MatchTemplate(m_RegMin1, m_RegMin2, secu_level, ref matched);
            //iError = m_FPM.GetMatchingScore(m_RegMin1, m_RegMin2, ref match_score);

            //if (iError == (Int32)SGFPMError.ERROR_NONE)
            //{
            //    if (matched)
            //    {
            //        //Aqui, acrescentei a parte do merge para um dedo
            //        Byte[] merged_template;
            //        Int32 buf_size = 0;
            //        m_FPM.GetIsoTemplateSizeAfterMerge(m_RegMin1, m_RegMin2, ref buf_size);
            //        merged_template = new Byte[buf_size];
            //        m_FPM.MergeIsoTemplate(m_RegMin1, m_RegMin2, merged_template);

            //        StatusBar.Text = "Registro efetivado, Qualidade do template final: " + match_score;
            //    }

            //    else
            //        StatusBar.Text = "Registro não efetuado.";
            //}
            //else
            //    DisplayError("GetMatchingScore()", iError);

            if (!openedSecugen)
            {
                StatusBar.Text = "Leitora biométrica não iniciada corretamente.";
                ColorStatusBar(false);
                MessageBox.Show("Leitora biométrica não iniciada corretamente.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool matched = false;
            Int32 err = 0;
            try
            {
                err = m_FPM.MatchTemplate(m_RegMin1, m_RegMin2, SGFPMSecurityLevel.HIGHEST, ref matched);
                //SÓ PARA TESTAR.
                m_FPM.GetMatchingScore(m_RegMin1, m_RegMin2, ref scoreRegistro);

                if (scoreRegistro < 140)// se o Score menor que 140 não deixa prosseguir com o cadastro.
                {
                    StatusBar.Text = "BIOMETRIA INSATISFATÓRIA. FAVOR REPETIR A OPERAÇÃO.";
                    ColorStatusBar(false);
                    return;
                }
            }
            catch
            {
                DisplayError("", err);
                return;
            }

            if ((err == (Int32)SGFPMError.ERROR_NONE))
            {
                if (matched)
                {
                    // Save template after merging two template - m_FetBuf1, m_FetBuf2
                    Byte[] merged_template;
                    Int32 buf_size = 0;

                    //if (m_useAnsiTemplate)
                    //{
                    //    m_FPM.GetTemplateSizeAfterMerge(m_RegMin1, m_RegMin2, ref buf_size);
                    //    merged_template = new Byte[buf_size];
                    //    m_FPM.MergeAnsiTemplate(m_RegMin1, m_RegMin2, merged_template);
                    //}
                    //else
                    //{
                    m_FPM.GetIsoTemplateSizeAfterMerge(m_RegMin1, m_RegMin2, ref buf_size);
                    merged_template = new Byte[buf_size];
                    m_FPM.MergeIsoTemplate(m_RegMin1, m_RegMin2, merged_template);
                    //}

                    if (m_StoredTemplate == null)
                    {
                        m_StoredTemplate = new Byte[buf_size];
                        merged_template.CopyTo(m_StoredTemplate, 0);
                    }
                    else
                    {
                        Int32 new_size = 0;

                        //if (m_useAnsiTemplate)
                        //{
                        //    err = m_FPM.GetTemplateSizeAfterMerge(m_StoredTemplate, merged_template, ref new_size);
                        //}
                        //else
                        //{
                        err = m_FPM.GetIsoTemplateSizeAfterMerge(m_StoredTemplate, merged_template, ref new_size);
                        //}

                        Byte[] new_enroll_template = new Byte[new_size];

                        //if (m_useAnsiTemplate)
                        //{
                        //    err = m_FPM.MergeAnsiTemplate(merged_template, m_StoredTemplate, new_enroll_template);
                        //}
                        //else
                        //{
                        err = m_FPM.MergeIsoTemplate(merged_template, m_StoredTemplate, new_enroll_template);
                        //}

                        m_StoredTemplate = new Byte[new_size];

                        new_enroll_template.CopyTo(m_StoredTemplate, 0);
                    }


                    //if (m_useAnsiTemplate)
                    //{
                    //    SGFPMANSITemplateInfo sample_info = new SGFPMANSITemplateInfo();
                    //    err = m_FPM.GetAnsiTemplateInfo(m_StoredTemplate, sample_info);
                    //    for (int i = 0; i < sample_info.TotalSamples; i++)
                    //        m_RadioButton[(Int32)sample_info.SampleInfo[i].FingerNumber].Checked = true;
                    //}
                    //else
                    //{
                    SGFPMISOTemplateInfo sample_info = new SGFPMISOTemplateInfo();
                    err = m_FPM.GetIsoTemplateInfo(m_StoredTemplate, sample_info);

                    //23/08/2018. Se ambos estiverem nulo, pega o primeiro template
                    if (Template1Cad == null && Template2Cad == null)
                    {
                        Template1Cad = m_StoredTemplate;
                        StatusBar.Text = "BIOMETRIA FORMADA COM SUCESSO. REPITA O PROCESSO PARA GERAR UMA NOVA OU CLIQUE EM SALVAR.";
                        MessageBox.Show("BIOMETRIA FORMADA COM SUCESSO. REPITA O PROCESSO PARA GERAR UMA NOVA OU CLIQUE EM SALVAR.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ColorStatusBar(true);
                        pictureBoxR1.Image = null;
                        pictureBoxR2.Image = null;
                        pictureBoxR1.Update();
                        pictureBoxR2.Update();
                        return;
                    }

                    if (Template1Cad != null && Template2Cad == null)
                    {
                        Template2Cad = m_StoredTemplate;
                        StatusBar.Text = "BIOMETRIA FORMADA COM SUCESSO. CLIQUE EM SALVAR.";
                        MessageBox.Show("BIOMETRIA FORMADA COM SUCESSO. CLIQUE EM SALVAR.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ColorStatusBar(true);
                        pictureBoxR1.Image = null;
                        pictureBoxR2.Image = null;
                        pictureBoxR1.Update();
                        pictureBoxR2.Update();
                        return;
                    }

                }
                else
                {
                    StatusBar.Text = string.Empty;
                    DisplayError("", err);
                    ColorStatusBar(false);
                }
            }
            else
            {
                StatusBar.Text = "Biometria não construída. Repita o processo: " + err;
                MessageBox.Show("Biometria não construída. Repita o processo: " + err);
                ColorStatusBar(false);
            }

        }

        private void frmPrincipal_Activated(object sender, EventArgs e)
        {
            IniciaSecugen();
        }

        private void btTestaBiometria_Click(object sender, EventArgs e)
        {
            frmTestaBiometria frmteste = new frmTestaBiometria();
            frmteste.ShowDialog();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                cbLogin.Checked = false;
                cbCPF.Checked = false;
            }
        }

        private void cbCPF_CheckedChanged(object sender, EventArgs e)
        {
            if (cbCPF.Checked)
            {
                checkBox1.Checked = false;
                cbLogin.Checked = false;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                crud.EnviaOcorrencia();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            RegistroLocal rl = new RegistroLocal();
            rl.EnviarFrequencia(DateTime.Now, IDEmpresa, IDSetor);
        }

        private void tbNome_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbLogin.Checked || cbCPF.Checked)
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != 08)
                {
                    e.Handled = true;
                }
                else
                {
                }
            }
        }

        #endregion

        #region tratamento de erro das leitoras secugen
        private void DisplayError(string funcName, int iError)
        {

            switch (iError)
            {
                case 0:                             //SGFDX_ERROR_NONE				= 0,
                    text = "Houve um erro desconhecido.";
                    break;

                case 1:                             //SGFDX_ERROR_CREATION_FAILED	= 1,
                    text = "Impossível criar objeto de captura.";
                    break;

                case 2:                             //   SGFDX_ERROR_FUNCTION_FAILED	= 2,
                    text = "Função falhou.";
                    break;

                case 3:                             //   SGFDX_ERROR_INVALID_PARAM	= 3,
                    text = "Parâmetros inválidos.";
                    break;

                case 4:                          //   SGFDX_ERROR_NOT_USED			= 4,
                    text = "Not used function.";
                    break;

                case 5:                                //SGFDX_ERROR_DLLLOAD_FAILED	= 5,
                    text = "Não foi possível criar o objeto.";
                    break;

                case 6:                                //SGFDX_ERROR_DLLLOAD_FAILED_DRV	= 6,
                    text = "Não foi possível abrir o driver do dispositivo.";
                    break;
                case 7:                                //SGFDX_ERROR_DLLLOAD_FAILED_ALGO = 7,
                    text = "Não foi possível abrir o arquivo: sgfpamx.dll";
                    break;

                case 51:                //SGFDX_ERROR_SYSLOAD_FAILED	   = 51,	// system file load fail
                    text = "Não foi possível abrir o driver kernel file";
                    break;

                case 52:                //SGFDX_ERROR_INITIALIZE_FAILED  = 52,   // chip initialize fail
                    text = "Falha ao iniciar o dispositivo biométrico.";
                    break;

                case 53:                //SGFDX_ERROR_LINE_DROPPED		   = 53,   // image data drop
                    text = "Transmissão de dados não é boa o suficiente.";
                    break;

                case 54:                //SGFDX_ERROR_TIME_OUT			   = 54,   // getliveimage timeout error
                    text = "Tempo esgotado";
                    break;

                case 55:                //SGFDX_ERROR_DEVICE_NOT_FOUND	= 55,   // device not found
                    text = "Dispositivo biométrico não encontrado.";
                    break;

                case 56:                //SGFDX_ERROR_DRVLOAD_FAILED	   = 56,   // dll file load fail
                    text = "Não foi possível abrir o arquivo de drivers.";
                    break;

                case 57:                //SGFDX_ERROR_WRONG_IMAGE		   = 57,   // wrong image
                    text = "Biometria falha ou não localizada.";
                    break;

                case 58:                //SGFDX_ERROR_LACK_OF_BANDWIDTH  = 58,   // USB Bandwith Lack Error
                    text = "Falta de largura da banda USB.";
                    break;

                case 59:                //SGFDX_ERROR_DEV_ALREADY_OPEN	= 59,   // Device Exclusive access Error
                    text = "Dispositivo biométrico já está acionado.";
                    break;

                case 60:                //SGFDX_ERROR_GETSN_FAILED		   = 60,   // Fail to get Device Serial Number
                    text = "Número serial do dispositivo com erros.";
                    break;

                case 61:                //SGFDX_ERROR_UNSUPPORTED_DEV		   = 61,   // Unsupported device
                    text = "Dispositivo biométrico não suportado por este equipamento.";
                    break;

                case 62:                //SGFDX_ERROR_UNSUPPORTED_DEV		   = 61,   // Unsupported device
                    text = "LEITURA INVÁLIDE. AGUARDE 3 SEG. E TENTE NOVAMENTE.";
                    break;

                // Extract & Verification error
                case 101:                //SGFDX_ERROR_FEAT_NUMBER		= 101, // utoo small number of minutiae
                    text = "O número de minúcioas é muito pequeno.";
                    break;

                case 102:                //SGFDX_ERROR_INVALID_TEMPLATE_TYPE		= 102, // wrong template type
                    text = "Template inválido.";
                    break;

                case 103:                //SGFDX_ERROR_INVALID_TEMPLATE1		= 103, // wrong template type
                    text = "Imagem 1 inválida.";
                    break;

                case 104:                //SGFDX_ERROR_INVALID_TEMPLATE2		= 104, // vwrong template type
                    text = "Imagem 2 inválida.";
                    break;

                case 105:                //SGFDX_ERROR_EXTRACT_FAIL		= 105, // extraction fail
                    text = "extração de minúcias inválida.";
                    break;

                case 106:                //SGFDX_ERROR_MATCH_FAIL		= 106, // matching  fail
                    text = "Combinação de imagens falhou. Repita o processo.";
                    break;

            }

            //text = funcName + " Erro: # " + iError + " :" + text;
            text = " Erro: # " + iError + " :" + text;
            StatusBar.Text = text;
            ColorStatusBar(false);
        }
        private string DisplayError2(string funcName, int iError)
        {

            switch (iError)
            {
                case 0:                             //SGFDX_ERROR_NONE				= 0,
                    text = "Houve um erro desconhecido.";
                    break;

                case 1:                             //SGFDX_ERROR_CREATION_FAILED	= 1,
                    text = "Impossível criar objeto de captura.";
                    break;

                case 2:                             //   SGFDX_ERROR_FUNCTION_FAILED	= 2,
                    text = "Função falhou.";
                    break;

                case 3:                             //   SGFDX_ERROR_INVALID_PARAM	= 3,
                    text = "Parâmetros inválidos.";
                    break;

                case 4:                          //   SGFDX_ERROR_NOT_USED			= 4,
                    text = "Not used function.";
                    break;

                case 5:                                //SGFDX_ERROR_DLLLOAD_FAILED	= 5,
                    text = "Não foi possível criar o objeto.";
                    break;

                case 6:                                //SGFDX_ERROR_DLLLOAD_FAILED_DRV	= 6,
                    text = "Não foi possível abrir o driver do dispositivo.";
                    break;
                case 7:                                //SGFDX_ERROR_DLLLOAD_FAILED_ALGO = 7,
                    text = "Não foi possível abrir o arquivo: sgfpamx.dll";
                    break;

                case 51:                //SGFDX_ERROR_SYSLOAD_FAILED	   = 51,	// system file load fail
                    text = "Não foi possível abrir o driver kernel file";
                    break;

                case 52:                //SGFDX_ERROR_INITIALIZE_FAILED  = 52,   // chip initialize fail
                    text = "Falha ao iniciar o dispositivo biométrico.";
                    break;

                case 53:                //SGFDX_ERROR_LINE_DROPPED		   = 53,   // image data drop
                    text = "Transmissão de dados não é boa o suficiente.";
                    break;

                case 54:                //SGFDX_ERROR_TIME_OUT			   = 54,   // getliveimage timeout error
                    text = "Tempo esgotado";
                    break;

                case 55:                //SGFDX_ERROR_DEVICE_NOT_FOUND	= 55,   // device not found
                    text = "Dispositivo biométrico não encontrado.";
                    break;

                case 56:                //SGFDX_ERROR_DRVLOAD_FAILED	   = 56,   // dll file load fail
                    text = "Não foi possível abrir o arquivo de drivers.";
                    break;

                case 57:                //SGFDX_ERROR_WRONG_IMAGE		   = 57,   // wrong image
                    text = "POSICIONE O DEDO AGUARDE UMA RESPOSTA.";
                    break;

                case 58:                //SGFDX_ERROR_LACK_OF_BANDWIDTH  = 58,   // USB Bandwith Lack Error
                    text = "Falta de largura da banda USB.";
                    break;

                case 59:                //SGFDX_ERROR_DEV_ALREADY_OPEN	= 59,   // Device Exclusive access Error
                    text = "Dispositivo biométrico já está acionado.";
                    break;

                case 60:                //SGFDX_ERROR_GETSN_FAILED		   = 60,   // Fail to get Device Serial Number
                    text = "Número serial do dispositivo com erros.";
                    break;

                case 61:                //SGFDX_ERROR_UNSUPPORTED_DEV		   = 61,   // Unsupported device
                    text = "Dispositivo biométrico não suportado por este equipamento.";
                    break;

                case 62:                //SGFDX_ERROR_UNSUPPORTED_DEV		   = 61,   // Unsupported device
                    text = "LEITURA INVÁLIDA. APÓS 3 SEG. TENTE NOVAMENTE.";
                    break;

                // Extract & Verification error
                case 101:                //SGFDX_ERROR_FEAT_NUMBER		= 101, // utoo small number of minutiae
                    text = "O número de minúcioas é muito pequeno.";
                    break;

                case 102:                //SGFDX_ERROR_INVALID_TEMPLATE_TYPE		= 102, // wrong template type
                    text = "Template inválido.";
                    break;

                case 103:                //SGFDX_ERROR_INVALID_TEMPLATE1		= 103, // wrong template type
                    text = "Imagem 1 inválida.";
                    break;

                case 104:                //SGFDX_ERROR_INVALID_TEMPLATE2		= 104, // vwrong template type
                    text = "Imagem 2 inválida.";
                    break;

                case 105:                //SGFDX_ERROR_EXTRACT_FAIL		= 105, // extraction fail
                    text = "extração de minúcias inválida.";
                    break;

                case 106:                //SGFDX_ERROR_MATCH_FAIL		= 106, // matching  fail
                    text = "Combinação de imagens falhou. Repita o processo.";
                    break;

            }

            //text = funcName + " Erro: # " + iError + " :" + text;
            text = " Erro: # " + iError + " :" + text;
            ColorStatusBar(false);
            return text;
        }
        #endregion

        #region Importar Colaboradores

        //20/07/2018 -- Mais uma tentativa
        //background.
        private void InitializeBackgroundWorker()
        {
            bwImportacao.DoWork +=
                new DoWorkEventHandler(bwImportacao_DoWork);
            bwImportacao.RunWorkerCompleted +=
                new RunWorkerCompletedEventHandler(
            bwImportacao_RunWorkerCompleted);
            bwImportacao.ProgressChanged +=
                new ProgressChangedEventHandler(
            bwImportacao_ProgressChanged);
        }

        private void btCancelar_Click(object sender, EventArgs e)
        {
            this.bwImportacao.CancelAsync();
            btCancelar.Enabled = false;
        }

        private void bwImportacao_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.pbServidores.Value = e.ProgressPercentage;
            pbServidores.CreateGraphics().DrawString(e.ProgressPercentage + "%", new Font("Arial", (float)8.25, FontStyle.Regular), Brushes.Black, new PointF(pbServidores.Width / 2 - 10, pbServidores.Height / 2 - 7));
            pbServidores.PerformStep();
            pbServidores.Update();
            lbgeral.Text = lbControleimportacao;
            if (nomeAtual != NomeInserido)
                LG.EscreverLogUser(string.Format("{0} importando...", NomeInserido.Trim()), lbLogUsuario);
            nomeAtual = NomeInserido;

        }

        string ImportColaboradores(int IDEmpresa, BackgroundWorker worker, DoWorkEventArgs e)
        {
            System.GC.Collect();
            int percent;
            try
            {
                //GetUsuariosWS2();
                //lbConnect.Text = "Conectado, gerando importação...";
                //LG.Esrevelog("Conectado, buscando usuários...", lbLogUsuario);
                ColaboradoresNaoInseridos = string.Empty;

                if (dsU.vwUsuarioWebService.Rows.Count > 0)
                {
                    TotalDaImporacao = dsU.vwUsuarioWebService.Rows.Count;
                    crud.DeleteUsuario();
                    //ControleProgressbar(dsU.vwUsuarioWebService.Rows.Count); //Movimenta a barra de atividades
                    //LG.Esrevelog(string.Format("{0} Colaboradores localizados...", dsU.vwUsuarioWebService.Rows.Count.ToString()), lbLogUsuario);
                    for (int i = 0; i <= dsU.vwUsuarioWebService.Rows.Count - 1; i++)
                    {

                        if (dsU.vwUsuarioWebService[i].IDUsuario == 10386)
                        {
                            primeiroNome = "";
                        }

                        if (!dsU.vwUsuarioWebService[i].IsPrimeiroNomeNull())
                        {
                            PrimeiroNome = dsU.vwUsuarioWebService[i].PrimeiroNome;
                        }
                        else
                            PrimeiroNome = string.Empty;

                        if (!dsU.vwUsuarioWebService[i].IsMatriculaNull())
                        {
                            Matricula = dsU.vwUsuarioWebService[i].Matricula.Trim();
                        }
                        else
                            Matricula = string.Empty;

                        if (!dsU.vwUsuarioWebService[i].IsTemplate1Null())
                        {
                            Template1 = dsU.vwUsuarioWebService[i].Template1;
                        }
                        else
                            Template1 = null;

                        if (!dsU.vwUsuarioWebService[i].IsTemplate2Null())
                        {
                            Template2 = dsU.vwUsuarioWebService[i].Template2;
                        }
                        else
                            Template2 = null;

                        if (!dsU.vwUsuarioWebService[i].IsSenhaAdminNull())
                        {
                            SenhaAdmin = dsU.vwUsuarioWebService[i].SenhaAdmin;
                        }
                        else
                            SenhaAdmin = string.Empty;

                        //AtualizaLabel(dsU.vwUsuarioWebService.Rows.Count.ToString(), (i + 1).ToString());

                        //pbServidores.CreateGraphics().DrawString(percent.ToString() + "%", new Font("Arial", (float)8.25, FontStyle.Regular), Brushes.Black, new PointF(pbServidores.Width / 2 - 10, pbServidores.Height / 2 - 7));
                        //pbServidores.PerformStep();
                        //pbServidores.Update();


                        if (!crud.InsertModifyUsuario(dsU.vwUsuarioWebService[i].Login.Trim(), Matricula,
                            PrimeiroNome, dsU.vwUsuarioWebService[i].DSUsuario, dsU.vwUsuarioWebService[i].CadastraDigital,
                            dsU.vwUsuarioWebService[i].IDTPUsuario, SenhaAdmin, Template1, Template2,
                            dsU.vwUsuarioWebService[i].IDUsuario, Convert.ToInt32(dsU.vwUsuarioWebService[i].IDVinculoUsuario), true,
                            dsU.vwUsuarioWebService[i].HoraEntradaManha, dsU.vwUsuarioWebService[i].HoraSaidaManha, dsU.vwUsuarioWebService[i].HoraEntradaTarde,
                            dsU.vwUsuarioWebService[i].HoraSaidaTarde, dsU.vwUsuarioWebService[i].TotalHoraDia, dsU.vwUsuarioWebService[i].RegimePlantonista, ""))
                        {
                            ColaboradoresNaoInseridos += string.Format("{0} ", dsU.vwUsuarioWebService[i].DSUsuario);
                        }
                        else
                        {
                            NomeInserido = string.Empty;
                            NomeInserido = dsU.vwUsuarioWebService[i].DSUsuario;
                        }

                        //LG.EscreverLogUser(string.Format("{0} importando...", dsU.vwUsuarioWebService[i].DSUsuario), lbLogUsuario);

                        if (worker.CancellationPending)
                        {
                            e.Cancel = true;
                        }

                        percent = (int)(((double)(i + 1) / dsU.vwUsuarioWebService.Rows.Count) * 100);

                        worker.ReportProgress(percent);

                        lbControleimportacao = string.Format("Importando {0} de {1}", i + 1, dsU.vwUsuarioWebService.Rows.Count);

                    }

                    //AtualizaLabel(dsU.vwUsuarioWebService.Rows.Count.ToString(),dsU.vwUsuarioWebService.Rows.Count.ToString());
                    PrimeiroNome = string.Empty;
                    Matricula = string.Empty;
                    Template1 = null;
                    Template2 = null;
                    SenhaAdmin = string.Empty;


                    if (ColaboradoresNaoInseridos == string.Empty)
                    {
                        //MessageBox.Show("Colaboradores importados com sucesso. ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return string.Format("Colaboradores importados com sucesso.");
                    }
                    //MessageBox.Show("Colaboradores importados com sucesso. ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                    {
                        //MessageBox.Show("Colaboradores importados com sucesso. Com a excessão dos colaboradores: " + ColaboradoresNaoInseridos, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return string.Format("Colaboradores importados com sucesso. Com a excessão dos colaboradores: {0}", ColaboradoresNaoInseridos);
                    }
                }
                else
                {
                    //MessageBox.Show("Não foi possível obter uma comunicação com a central de dados. Comunique o administrador!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return "Não foi possível obter uma comunicação com a central de dados. Comunique o administrador!";
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Falha ao importar colaboradores. Tente novamente: " + ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return string.Format("Falha ao importar colaboradores. Tente novamente: {0}", ex.Message.Trim());
            }
        }

        private int TimeConsumingOperation(BackgroundWorker bw, int sleepPeriod)
        {
            int result = 0;

            Random rand = new Random();

            while (!bw.CancellationPending)
            {
                bool exit = false;

                switch (rand.Next(3))
                {
                    // Raise an exception.
                    case 0:
                        {
                            throw new Exception("An error condition occurred.");
                            break;
                        }

                    // Sleep for the number of milliseconds
                    // specified by the sleepPeriod parameter.
                    case 1:
                        {
                            System.Threading.Thread.Sleep(sleepPeriod);
                            break;
                        }

                    // Exit and return normally.
                    case 2:
                        {
                            result = 23;
                            exit = true;
                            break;
                        }

                    default:
                        {
                            break;
                        }
                }

                if (exit)
                {
                    break;
                }
            }

            return result;
        }

        private void bwRegistroPonto_DoWork(object sender, DoWorkEventArgs e)
        {

            RegistrarFrequencia2();

        }

        private void bwRegistroPonto_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                staticPB.Image = imgBio;

                if (msg2.IndexOf("efetivada") > 0 || msg2.IndexOf("finalizada") > 0)
                {
                    lbLog.ForeColor = System.Drawing.Color.ForestGreen;
                    Console.Beep(750, 400);
                }
                else
                {
                    lbLog.ForeColor = System.Drawing.Color.Red;
                    Console.Beep(600, 350);
                    Console.Beep(600, 350);
                }

                LG.Esrevelog(msg2, lbLog);

                if (this.WindowState == FormWindowState.Minimized)
                {
                    niSegundoPlano.BalloonTipText = msg2;
                    niSegundoPlano.ShowBalloonTip(2);
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            msg2 = string.Empty;
        }

        private void bwImportacao_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            if (e.Result == null)
            {
                e.Result = ImportColaboradores(IDEmpresa, worker, e);
                FinishImportation = true;
            }
        }

        private void bwImportacao_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Só exibe a mensagem apenas uma vez.
            if (FinishImportation)
            {
                if (e.Cancelled)
                {
                    //O Usuário cancelou a operação.
                    MessageBox.Show("Importação cancelada pelo usuário. ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (e.Error != null)
                {
                    //Houve um erro durante a operação.
                    string msg = string.Format("Ocorreu um erro durante a importação: {0}", e.Error.Message.Trim());
                    MessageBox.Show(msg, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    //Ocorreu td normalmente.
                    string msg = string.Format(e.Result.ToString().Trim());
                    MessageBox.Show(msg, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                btcarregar.Enabled = true;
                btCancelar.Enabled = false;
                FinishImportation = false;
            }
        }

        private void btnInformarPresenca_Click(object sender, EventArgs e)
        {
            if (utilConexao.ConexaoOK)
            {
                frmJustificarPresenca frmJust = new frmJustificarPresenca(utilConexao.tcm.UrlWS, IDEmpresa, IDSetor);
                frmJust.ShowDialog();
            }
            else
            {
                MessageBox.Show("Sistema operando off-line. Favor aguardar o restabelecimento da conexão!");
            }

        }

        private void ControleProgressbar(int MaxProgress)
        {

            pbServidores.Visible = true;
            pbServidores.Step = 1;
            pbServidores.Maximum = MaxProgress;
            pbServidores.Minimum = 0;

            pbServidores.Update();
        }

        private void AtualizaLabel(string Total, string Atual)
        {
            lbgeral.Text = string.Format("Importando {0} de {1}...", Atual, Total);
            lbgeral.Update();
        }

        private void ImportarColaboradores(int IDEmpresa)
        {
            //lbConnect.Text = "Conectando a central de dados. Aguarde!";
            //LG.Esrevelog("Conectando a central de dados...", lbLogUsuario);
            System.GC.Collect();
            int percent;
            try
            {
                GetUsuariosWS();
                //lbConnect.Text = "Conectado, gerando importação...";
                //LG.Esrevelog("Conectado, buscando usuários...", lbLogUsuario);
                ColaboradoresNaoInseridos = string.Empty;

                if (dsU.vwUsuarioWebService.Rows.Count > 0)
                {
                    crud.DeleteUsuario();
                    ControleProgressbar(dsU.vwUsuarioWebService.Rows.Count); //Movimenta a barra de atividades
                    //LG.Esrevelog(string.Format("{0} Colaboradores localizados...", dsU.vwUsuarioWebService.Rows.Count.ToString()), lbLogUsuario);
                    for (int i = 0; i <= dsU.vwUsuarioWebService.Rows.Count - 1; i++)
                    {
                        if (!dsU.vwUsuarioWebService[i].IsPrimeiroNomeNull())
                        {
                            PrimeiroNome = dsU.vwUsuarioWebService[i].PrimeiroNome;
                        }
                        else
                            PrimeiroNome = string.Empty;

                        if (!dsU.vwUsuarioWebService[i].IsMatriculaNull())
                        {
                            Matricula = dsU.vwUsuarioWebService[i].Matricula.Trim();
                        }
                        else
                            Matricula = string.Empty;

                        if (!dsU.vwUsuarioWebService[i].IsTemplate1Null())
                        {
                            Template1 = dsU.vwUsuarioWebService[i].Template1;
                        }
                        else
                            Template1 = null;

                        if (!dsU.vwUsuarioWebService[i].IsTemplate2Null())
                        {
                            Template1 = dsU.vwUsuarioWebService[i].Template2;
                        }
                        else
                            Template2 = null;

                        if (!dsU.vwUsuarioWebService[i].IsSenhaAdminNull())
                        {
                            SenhaAdmin = dsU.vwUsuarioWebService[i].SenhaAdmin;
                        }
                        else
                            SenhaAdmin = string.Empty;

                        //AtualizaLabel(dsU.vwUsuarioWebService.Rows.Count.ToString(), (i + 1).ToString());

                        //percent = (int)(((double)pbServidores.Value / (double)pbServidores.Maximum) * 100);
                        //pbServidores.CreateGraphics().DrawString(percent.ToString() + "%", new Font("Arial", (float)8.25, FontStyle.Regular), Brushes.Black, new PointF(pbServidores.Width / 2 - 10, pbServidores.Height / 2 - 7));
                        //pbServidores.PerformStep();
                        //pbServidores.Update();

                        if (!crud.InsertModifyUsuario(dsU.vwUsuarioWebService[i].Login.Trim(), Matricula,
                            PrimeiroNome, dsU.vwUsuarioWebService[i].DSUsuario, dsU.vwUsuarioWebService[i].CadastraDigital,
                            dsU.vwUsuarioWebService[i].IDTPUsuario, SenhaAdmin, Template1, Template2,
                            dsU.vwUsuarioWebService[i].IDUsuario, Convert.ToInt32(dsU.vwUsuarioWebService[i].IDVinculoUsuario), true,
                            dsU.vwUsuarioWebService[i].HoraEntradaManha, dsU.vwUsuarioWebService[i].HoraSaidaManha, dsU.vwUsuarioWebService[i].HoraEntradaTarde,
                            dsU.vwUsuarioWebService[i].HoraSaidaTarde, dsU.vwUsuarioWebService[i].TotalHoraDia, dsU.vwUsuarioWebService[i].RegimePlantonista, ""))
                        {
                            ColaboradoresNaoInseridos += string.Format("{0} ", dsU.vwUsuarioWebService[i].DSUsuario);
                        }

                        //LG.EscreverLogUser(string.Format("{0} importando...", dsU.vwUsuarioWebService[i].DSUsuario), lbLogUsuario);

                    }

                    //AtualizaLabel(dsU.vwUsuarioWebService.Rows.Count.ToString(),dsU.vwUsuarioWebService.Rows.Count.ToString());
                    PrimeiroNome = string.Empty;
                    Matricula = string.Empty;
                    Template1 = null;
                    Template2 = null;
                    SenhaAdmin = string.Empty;


                    if (ColaboradoresNaoInseridos == string.Empty)
                        MessageBox.Show("Colaboradores importados com sucesso. ", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    else
                        MessageBox.Show("Colaboradores importados com sucesso. Com a excessão dos colaboradores: " + ColaboradoresNaoInseridos, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show("Não foi possível obter uma comunicação com a central de dados. Comunique o administrador!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Falha ao importar colaboradores. Tente novamente: " + ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            System.GC.Collect();
        }
        #endregion

        #region Identificação biométrica

        public void GetUsuariosWS2()
        {
            try
            {
                webS = new pontonarede.ServiceSoapClient("ServiceSoap", utilConexao.tcm.UrlWS);
                dsU = new pontonarede.DataSetUsuario();
                webS.Endpoint.Binding.OpenTimeout = TimeSpan.FromSeconds(10); //OpenTimeout p/ 3 e SendTimeout p 15
                webS.Endpoint.Binding.SendTimeout = TimeSpan.FromSeconds(15);
                webS.Endpoint.Binding.ReceiveTimeout = TimeSpan.FromSeconds(7);
                webS.Open();
                dsU = webS.UsuariosPontoHash(IDEmpresa, "TentoWebServiceNovamente7x24dm12", _hashMaquina);
                //StatusBar.Text = "Biometrias carregadas! WS.";
                webS.Close();
            }
            catch (Exception ex)
            {
                ex.ToString();
                //GetUsuarios();
            }

            AlteracaoBiometrica = false;
        }
        public void GetUsuariosWS()
        {
            try
            {
                webS = new pontonarede.ServiceSoapClient("ServiceSoap", utilConexao.tcm.UrlWS);
                dsU = new pontonarede.DataSetUsuario();
                webS.Endpoint.Binding.OpenTimeout = TimeSpan.FromSeconds(10); //OpenTimeout p/ 3 e SendTimeout p 15
                webS.Endpoint.Binding.SendTimeout = TimeSpan.FromSeconds(15);
                webS.Endpoint.Binding.ReceiveTimeout = TimeSpan.FromSeconds(7);
                webS.Open();
                
                dsU = webS.UsuariosPontoHash(IDEmpresa, "TentoWebServiceNovamente7x24dm12", _hashMaquina);
                StatusBar.Text = "Biometrias carregadas! WS.";
                webS.Close();
                foreach (var item in dsU.vwUsuarioWebService)
                {
                    var teste = item;
                }

                //GetUsuarios();
            }
            catch (Exception ex)
            {
                ex.ToString();
                GetUsuarios();
            }
            //Populando a tabela recem criada. Se der zica, segue com o parangolÊ.
            try
            {
                TBUsuarioLocalSetor = crud.GetTBUsuarioSetorLocalHash();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }


            AlteracaoBiometrica = false;
        }
        private void GetUsuarios()
        {
            StatusBar.Text = crud.GetUsuarioTemplate(dsLp, idusuario);
            LG.Esrevelog("Operando com a base local", lbLog);
        }

        private int CompareAchado(pontonarede.DataSetUsuario dsUparam, DataSetpnrClient dsLpParam, byte[] templateComparativo)
        {
            //zerar o Localizado.
            IDUsuarioLocalizado = 0;
            //Color padrão verde. green.
            lbLog.ForeColor = Color.Green;
            LG.Esrevelog("CONFIRMANDO...", lbLog);

            //Da um tempo de 2 segundos para o próximo registro.
            //em 01/06/2018, aumentei a threading p 2 segundos.
            // troquei o sleep de lugar também. Do fim da rotina para o começo.
            //em 12/06/2018, dia dos namorados, retirei o sleep pois a leitora só relê quando o dedo é
            //retirado o colocado novamente.
            //System.Threading.Thread.Sleep(1000);

            StatusBar.Text = string.Empty;
            Int32 error = (Int32)SGFPMError.ERROR_NONE;


            bool matched = false;

            //11/06/2018 - Se houver dados no dsU (do WS), segue. Senão, fazer tudo com a base local.
            if (dsUparam.vwUsuarioWebService.Rows.Count > 0)
            {
                for (int i = 0; i <= dsUparam.vwUsuarioWebService.Rows.Count - 1; i++)
                {
                    //se não for nulo, segue com a busca. Senão, pula.
                    if (!dsUparam.vwUsuarioWebService[i].IsTemplate1Null())
                    {
                        SGFPMISOTemplateInfo sample_info = new SGFPMISOTemplateInfo();

                        error = m_FPM.GetIsoTemplateInfo(dsUparam.vwUsuarioWebService[i].Template1, sample_info);

                        for (int z = 0; z <= sample_info.TotalSamples; z++)
                        {
                            //Deixar o nível biométrico para HIGHER - 03/08/2018 - No normal, conforme layout padrão da fabricante
                            //mto possível acontecer o falso positivo
                            error = m_FPM.MatchIsoTemplate(dsUparam.vwUsuarioWebService[i].Template1, z, templateComparativo, 0, SecuGen.FDxSDKPro.Windows.SGFPMSecurityLevel.HIGH, ref matched);
                            if (matched)
                            {
                                return dsUparam.vwUsuarioWebService[i].IDUsuario;
                            }
                        }
                    }
                }
                return 0;
            }//Senão, faz td com a base local.
            else
            {
                if (dsLpParam.TBUsuarioLocal.Rows.Count > 0)
                {
                    for (int i = 0; i <= dsLpParam.TBUsuarioLocal.Rows.Count - 1; i++)
                    {
                        //se não for nulo, segue com a busca. Senão, pula.
                        if (!dsLpParam.TBUsuarioLocal[i].IsTemplate1Null())
                        {
                            SGFPMISOTemplateInfo sample_info = new SGFPMISOTemplateInfo();

                            error = m_FPM.GetIsoTemplateInfo(dsLpParam.TBUsuarioLocal[i].Template1, sample_info);

                            for (int z = 0; z <= sample_info.TotalSamples; z++)
                            {
                                //Deixar o nível biométrico para HIGHER - 03/08/2018 - No normal, conforme layout padrão da fabricante
                                //mto possível acontecer o falso positivo
                                error = m_FPM.MatchIsoTemplate(dsLpParam.TBUsuarioLocal[i].Template1, z, templateComparativo, 0, SecuGen.FDxSDKPro.Windows.SGFPMSecurityLevel.HIGH, ref matched);

                                if (matched)
                                {
                                    return Convert.ToInt32(dsLpParam.TBUsuarioLocal[i].IDUsuario);
                                }
                            }
                        }
                    }
                    return 0;
                }
                else
                    return 0;//Senão, faz td com a base local.
            }

            matched = false;
            System.GC.Collect();
            return 0;
        }

        public void RegistrarFrequencia2()
        {
            if (!enableRegistro) //Se estiver como falso, não prossegue pq estará em outra aba que não seja a 1.
                return;
            zeraAvisos = 0;
            Byte[] fp_image = new Byte[m_ImageWidth * m_ImageHeight];
            Int32 error = (Int32)SGFPMError.ERROR_NONE;
            Int32 img_qlty = 0;
            if (openedSecugen)
            {
                error = m_FPM.GetImage(fp_image);
                m_FPM.GetImageQuality(m_ImageWidth, m_ImageHeight, fp_image, ref img_qlty);
            }
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            if (error == (Int32)SGFPMError.ERROR_NONE)
            {
                imgBio = DrawImage2(fp_image);

                SGFPMFingerInfo finger_info = new SGFPMFingerInfo();
                finger_info.FingerNumber = (SGFPMFingerPosition)0;
                finger_info.ImageQuality = (Int16)img_qlty;
                finger_info.ImpressionType = (Int16)SGFPMImpressionType.IMPTYPE_LP;
                finger_info.ViewNumber = 1;

                // Create template
                error = m_FPM.CreateTemplate(finger_info, fp_image, m_VrfMin);

                if (error == (Int32)SGFPMError.ERROR_NONE)
                {
                    bool matched = false;

                    #region
                    //bool template2 = false;

                    ////procura pelo local
                    //if (TBUsuarioLocalSetor.Rows.Count > 0)
                    //{
                    //    for (int i = 0; i <= TBUsuarioLocalSetor.Rows.Count - 1; i++)
                    //    {
                    //        //Amarrar no cadastro. O primeiro campo cadastrado será sempre o 1.
                    //        if (TBUsuarioLocalSetor.Rows[i]["Template1"] != null)
                    //        {
                    //            error = m_FPM.MatchTemplate((byte[])TBUsuarioLocalSetor.Rows[i]["Template1"], m_VrfMin, SecuGen.FDxSDKPro.Windows.SGFPMSecurityLevel.HIGHEST, ref matched);

                    //            if (matched)
                    //            {
                    //                error = m_FPM.MatchTemplate((byte[])TBUsuarioLocalSetor.Rows[i]["Template1"], m_VrfMin, SecuGen.FDxSDKPro.Windows.SGFPMSecurityLevel.HIGHEST, ref matched);

                    //                if (!matched)
                    //                {
                    //                    msg2 = "SERVIDOR NÃO IDENTIFICADO.";
                    //                    //return;
                    //                }
                    //            }
                    //            //Se não deu matched, faz com a outra biometria. em outro campo.
                    //            if (!matched && TBUsuarioLocalSetor.Rows[i]["Template2"].ToString() != string.Empty)
                    //            {
                    //                error = m_FPM.MatchTemplate((byte[])TBUsuarioLocalSetor.Rows[i]["Template2"], m_VrfMin, SecuGen.FDxSDKPro.Windows.SGFPMSecurityLevel.HIGHEST, ref matched);

                    //                if (matched)
                    //                {
                    //                    error = m_FPM.MatchTemplate((byte[])TBUsuarioLocalSetor.Rows[i]["Template2"], m_VrfMin, SecuGen.FDxSDKPro.Windows.SGFPMSecurityLevel.HIGHEST, ref matched);

                    //                    if (!matched)
                    //                    {
                    //                        msg2 = "SERVIDOR NÃO IDENTIFICADO.";
                    //                        //return;
                    //                    }
                    //                }
                    //            }

                    //            if (error == (Int32)SGFPMError.ERROR_NONE)
                    //            {
                    //                if (matched)
                    //                {
                    //                    pontonarede.ServiceSoapClient WebS = new pontonarede.ServiceSoapClient();
                    //                    WebS.Endpoint.Binding.OpenTimeout = TimeSpan.FromSeconds(10); //OpenTimeout p/ 3 e SendTimeout p 15
                    //                    WebS.Endpoint.Binding.SendTimeout = TimeSpan.FromSeconds(15);
                    //                    WebS.Endpoint.Binding.ReceiveTimeout = TimeSpan.FromSeconds(7);
                    //                    try
                    //                    {
                    //                        WebS.Open();
                    //                        msg2 = WebS.BaterPonto(IDEmpresa, IDSetor, Convert.ToInt32(TBUsuarioLocalSetor.Rows[i]["IDUsuario"]),
                    //                        DTRelogioData, "TentoWebServiceNovamente7x24dm12", TBUsuarioLocalSetor.Rows[i]["HoraEntradaManha"].ToString(),
                    //                        TBUsuarioLocalSetor.Rows[i]["HoraSaidaManha"].ToString(), TBUsuarioLocalSetor.Rows[i]["HoraEntradaTarde"].ToString(),
                    //                        TBUsuarioLocalSetor.Rows[i]["HoraSaidaTarde"].ToString(), (int)TBUsuarioLocalSetor.Rows[i]["TotalHoraDia"],
                    //                        TBUsuarioLocalSetor.Rows[i]["DSUsuario"].ToString(), TBUsuarioLocalSetor.Rows[i]["PrimeiroNome"].ToString(),
                    //                        (bool)TBUsuarioLocalSetor.Rows[i]["RegimePlantonista"],
                    //                        (int)TBUsuarioLocalSetor.Rows[i]["IDVinculoUsuario"]);
                    //                        WebS.Close();
                    //                        return;
                    //                        //Se algumas dessas foram encontrada, pinta de verde ou vermelho
                    //                    }
                    //                    catch (Exception ex) //caso haja alguma falha. faz com a base local.
                    //                    {
                    //                        crud = new crudPtServer();

                    //                        if (IDEmpresa == 0)
                    //                        {
                    //                            IDEmpresa = crud.GetIDEmpresa();
                    //                            IDSetor = crud.GetIDSetor();
                    //                        }
                    //                        crud.InsertOcorrencia(DTRelogioData, ex.Message.Trim(), IDEmpresa, IDSetor);
                    //                        //aqui - Se não deu certo enviar o ponto. faz o registro local.
                    //                        RegistroLocal RL = new RegistroLocal();
                    //                        msg2 = RL.InsertRegistroLocal(Convert.ToInt32(dsU.vwUsuarioWebService[i].IDUsuario), DTRelogioData, Convert.ToInt32(dsU.vwUsuarioWebService[i].IDVinculoUsuario));
                    //                        return;
                    //                    }
                    //                }
                    //            }
                    //            else
                    //            {
                    //                msg2 = DisplayError2("", error);
                    //                //return;
                    //            }
                    //        }
                    //    }

                    //    msg2 = "SERVIDOR NÃO IDENTIFICADO.";
                    //    //return;
                    //}
                    //else
                    //{
                    //    msg2 = "SERVIDOR NÃO IDENTIFICADO.";
                    //    //return;
                    //}
                    #endregion

                    if (dsU.vwUsuarioWebService.Rows.Count > 0)
                    {
                        vwUsuarioFiltrados = dsU.vwUsuarioWebService;
                        for (int i = 0; i <= dsU.vwUsuarioWebService.Rows.Count - 1; i++)
                        {
                            //Amarrar no cadastro. O primeiro campo cadastrado será sempre o 1.
                            var teste = dsU.vwUsuarioWebService[i];
                            if (!dsU.vwUsuarioWebService[i].IsTemplate1Null())
                            {
                                error = m_FPM.MatchTemplate(dsU.vwUsuarioWebService[i].Template1, m_VrfMin, SecuGen.FDxSDKPro.Windows.SGFPMSecurityLevel.HIGHER, ref matched);

                                if (matched)
                                {
                                    error = m_FPM.MatchTemplate(dsU.vwUsuarioWebService[i].Template1, m_VrfMin, SecuGen.FDxSDKPro.Windows.SGFPMSecurityLevel.HIGHER, ref matched);

                                    if (!matched)
                                    {

                                        msg2 = "SERVIDOR NÃO IDENTIFICADO.";
                                        return;
                                    }

                                }
                                //Se não deu matched, faz com a outra biometria. em outro campo.
                                if (!matched && !dsU.vwUsuarioWebService[i].IsTemplate2Null())
                                {
                                    error = m_FPM.MatchTemplate(dsU.vwUsuarioWebService[i].Template2, m_VrfMin, SecuGen.FDxSDKPro.Windows.SGFPMSecurityLevel.HIGHER, ref matched);

                                    if (matched)
                                    {
                                        error = m_FPM.MatchTemplate(dsU.vwUsuarioWebService[i].Template2, m_VrfMin, SecuGen.FDxSDKPro.Windows.SGFPMSecurityLevel.HIGHER, ref matched);

                                        if (!matched)
                                        {

                                            msg2 = "SERVIDOR NÃO IDENTIFICADO.";
                                            return;
                                        }
                                    }
                                }

                                if (error == (Int32)SGFPMError.ERROR_NONE)
                                {
                                    if (matched)
                                    {
                                        pontonarede.ServiceSoapClient WebS = new pontonarede.ServiceSoapClient("ServiceSoap", utilConexao.tcm.UrlWS);
                                        WebS.Endpoint.Binding.OpenTimeout = TimeSpan.FromSeconds(10); //OpenTimeout p/ 3 e SendTimeout p 15
                                        WebS.Endpoint.Binding.SendTimeout = TimeSpan.FromSeconds(15);
                                        WebS.Endpoint.Binding.ReceiveTimeout = TimeSpan.FromSeconds(7);
                                        try
                                        {
                                            var teste1 = dsU.vwUsuarioWebService[i].Template1;
                                            IdUsuarioRegistro = Convert.ToInt32(dsU.vwUsuarioWebService[i].IDUsuario);
                                            WebS.Open();
                                            msg2 = WebS.BaterPontoHash(IDEmpresa, IDSetor, Convert.ToInt32(dsU.vwUsuarioWebService[i].IDUsuario),
                                            DTRelogioData, "TentoWebServiceNovamente7x24dm12", dsU.vwUsuarioWebService[i].HoraEntradaManha,
                                            dsU.vwUsuarioWebService[i].HoraSaidaManha, dsU.vwUsuarioWebService[i].HoraEntradaTarde,
                                            dsU.vwUsuarioWebService[i].HoraSaidaTarde, dsU.vwUsuarioWebService[i].TotalHoraDia,
                                            dsU.vwUsuarioWebService[i].DSUsuario, dsU.vwUsuarioWebService[i].PrimeiroNome,
                                            dsU.vwUsuarioWebService[i].RegimePlantonista, dsU.vwUsuarioWebService[i].IDVinculoUsuario, _hashMaquina, stopwatch.Elapsed.ToString());
                                            WebS.Close();
                                            //COLOCA O REGISTRO NO TOPO DA LISTA
                                            try
                                            {
                                                DataRow item = dsU.vwUsuarioWebService.NewRow();
                                                item.ItemArray = dsU.vwUsuarioWebService[i].ItemArray.Clone() as object[];
                                                dsU.vwUsuarioWebService.Rows.RemoveAt(i);
                                                dsU.vwUsuarioWebService.Rows.InsertAt(item, 0);
                                                crudPtServer cps = new crudPtServer(utilConexao.tcm.UrlWS);
                                                cps.InsertLogRegistrosClint(IdUsuarioRegistro, IDEmpresa, IDSetor);
                                                var teste2 = dsU.vwUsuarioWebService[i].Template1;

                                                if (teste1.Equals(teste2))
                                                {
                                                    string a = "a";
                                                }
                                            }
                                            catch (Exception e) { }

                                            return;

                                            //Se algumas dessas foram encontrada, pinta de verde ou vermelho
                                        }
                                        catch (Exception ex) //caso haja alguma falha. faz com a base local.
                                        {
                                            crud = new crudPtServer(utilConexao.tcm.UrlWS);

                                            if (IDEmpresa == 0)
                                            {
                                                IDEmpresa = crud.GetIDEmpresa();
                                                IDSetor = crud.GetIDSetor();
                                            }
                                            if (IDEmpresa == 0)
                                            {

                                            }
                                            crud.InsertOcorrencia(DTRelogioData, ex.Message.Trim(), IDEmpresa, IDSetor);
                                            //aqui - Se não deu certo enviar o ponto. faz o registro local.
                                            RegistroLocal RL = new RegistroLocal();
                                            msg2 = RL.InsertRegistroLocal(Convert.ToInt32(dsU.vwUsuarioWebService[i].IDUsuario), DTRelogioData, Convert.ToInt32(dsU.vwUsuarioWebService[i].IDVinculoUsuario));
                                            try
                                            {

                                                DataRow item = dsU.vwUsuarioWebService.NewRow();
                                                item.ItemArray = dsU.vwUsuarioWebService[i].ItemArray.Clone() as object[];
                                                dsU.vwUsuarioWebService.Rows.RemoveAt(i);
                                                dsU.vwUsuarioWebService.Rows.InsertAt(item, 0);
                                                crudPtServer cps = new crudPtServer(utilConexao.tcm.UrlWS);
                                                cps.InsertLogRegistrosClint(IdUsuarioRegistro, IDEmpresa, IDSetor);

                                            }
                                            catch (Exception e) { }

                                            return;
                                        }
                                    }
                                }
                                else
                                {
                                    msg2 = DisplayError2("", error);
                                    return;
                                }
                            }
                        }

                        msg2 = "SERVIDOR NÃO IDENTIFICADO.";

                        return;
                    }
                    else //Senão faz td com a base local
                    {
                        if (dsLp.TBUsuarioLocal.Rows.Count > 0)
                        {
                            for (int i = 0; i <= dsLp.TBUsuarioLocal.Rows.Count - 1; i++)
                            {
                                if (!dsLp.TBUsuarioLocal[i].IsTemplate1Null())
                                {
                                    error = m_FPM.MatchTemplate(dsLp.TBUsuarioLocal[i].Template1, m_VrfMin, SecuGen.FDxSDKPro.Windows.SGFPMSecurityLevel.HIGHER, ref matched);
                                    //Não deu erro segue em frente

                                    if (matched)
                                    {
                                        error = m_FPM.MatchTemplate(dsLp.TBUsuarioLocal[i].Template1, m_VrfMin, SecuGen.FDxSDKPro.Windows.SGFPMSecurityLevel.HIGHER, ref matched);

                                        if (!matched)
                                        {
                                            msg2 = "SERVIDOR NÃO IDENTIFICADO.";
                                            return;
                                        }
                                    }
                                    //Se não deu matched, faz com a outra biometria. em outro campo.
                                    if (!matched && !dsLp.TBUsuarioLocal[i].IsTemplate2Null())
                                    {
                                        error = m_FPM.MatchTemplate(dsLp.TBUsuarioLocal[i].Template2, m_VrfMin, SecuGen.FDxSDKPro.Windows.SGFPMSecurityLevel.HIGHER, ref matched);

                                        if (matched)
                                        {
                                            error = m_FPM.MatchTemplate(dsLp.TBUsuarioLocal[i].Template2, m_VrfMin, SecuGen.FDxSDKPro.Windows.SGFPMSecurityLevel.HIGHER, ref matched);

                                            if (!matched)
                                            {
                                                msg2 = "SERVIDOR NÃO IDENTIFICADO.";
                                                return;
                                            }
                                        }
                                    }

                                    if (error == (Int32)SGFPMError.ERROR_NONE)
                                    {
                                        if (matched)
                                        {
                                            pontonarede.ServiceSoapClient WebS = new pontonarede.ServiceSoapClient("ServiceSoap", utilConexao.tcm.UrlWS);
                                            WebS.Endpoint.Binding.OpenTimeout = TimeSpan.FromSeconds(10); //OpenTimeout p/ 3 e SendTimeout p 15
                                            WebS.Endpoint.Binding.SendTimeout = TimeSpan.FromSeconds(15);
                                            WebS.Endpoint.Binding.ReceiveTimeout = TimeSpan.FromSeconds(7);
                                            try
                                            {
                                                IdUsuarioRegistro = Convert.ToInt32(dsLp.TBUsuarioLocal[i].IDUsuario);
                                                WebS.Open();
                                                msg2 = WebS.BaterPontoHash(IDEmpresa, IDSetor, Convert.ToInt32(dsLp.TBUsuarioLocal[i].IDUsuario),
                                                DTRelogioData, "TentoWebServiceNovamente7x24dm12", dsLp.TBUsuarioLocal[i].HoraEntradaManha,
                                                dsLp.TBUsuarioLocal[i].HoraSaidaManha, dsLp.TBUsuarioLocal[i].HoraEntradaTarde,
                                                dsLp.TBUsuarioLocal[i].HoraSaidaTarde, dsLp.TBUsuarioLocal[i].TotalHoraDia,
                                                dsLp.TBUsuarioLocal[i].DSUsuario, "", dsLp.TBUsuarioLocal[i].RegimePlantonista, dsLp.TBUsuarioLocal[i].IDVinculoUsuario, _hashMaquina, stopwatch.Elapsed.ToString());
                                                WebS.Close();

                                                //COLOCA O REGISTRO NO TOPO DA LISTA
                                                try
                                                {

                                                    DataRow item = dsLp.TBUsuarioLocal.NewRow();
                                                    item.ItemArray = dsLp.TBUsuarioLocal[i].ItemArray.Clone() as object[];
                                                    dsLp.TBUsuarioLocal.Rows.RemoveAt(i);
                                                    dsLp.TBUsuarioLocal.Rows.InsertAt(item, 0);
                                                    crudPtServer cps = new crudPtServer(utilConexao.tcm.UrlWS);
                                                    cps.InsertLogRegistrosClint(IdUsuarioRegistro, IDEmpresa, IDSetor);

                                                }
                                                catch (Exception e) { }
                                                return;
                                            }
                                            catch (Exception ex)
                                            {
                                                crud = new crudPtServer(utilConexao.tcm.UrlWS);
                                                if (IDEmpresa == 0)
                                                {
                                                    IDEmpresa = crud.GetIDEmpresa();
                                                    IDSetor = crud.GetIDSetor();
                                                }
                                                crud.InsertOcorrencia(DTRelogioData, ex.Message.Trim(), IDEmpresa, IDSetor);
                                                //aqui - Se não deu certo enviar o ponto. faz o registro local.
                                                RegistroLocal RL = new RegistroLocal();
                                                msg2 = RL.InsertRegistroLocal(Convert.ToInt32(dsLp.TBUsuarioLocal[i].IDUsuario), DTRelogioData, Convert.ToInt32(dsLp.TBUsuarioLocal[i].IDVinculoUsuario));
                                                //COLOCA O REGISTRO NO TOPO DA LISTA
                                                try
                                                {

                                                    DataRow item = dsLp.TBUsuarioLocal.NewRow();
                                                    item.ItemArray = dsLp.TBUsuarioLocal[i].ItemArray.Clone() as object[];
                                                    dsLp.TBUsuarioLocal.Rows.RemoveAt(i);
                                                    dsLp.TBUsuarioLocal.Rows.InsertAt(item, 0);
                                                    crudPtServer cps = new crudPtServer(utilConexao.tcm.UrlWS);
                                                    cps.InsertLogRegistrosClint(IdUsuarioRegistro, IDEmpresa, IDSetor);

                                                }
                                                catch (Exception e) { }
                                                return;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        msg2 = DisplayError2("", error);
                                        return;
                                    }
                                }
                            }

                            msg2 = "SERVIDOR NÃO IDENTIFICADO.";
                            return;
                        }
                        else
                        {
                            msg2 = "SEM USUÁRIOS PARA IDENTIFICAR. FAVOR REABRIAR O PROGRAMA!";
                        }
                    }

                }
                else
                {
                    msg2 = DisplayError2("", error);
                }

            }
            else
            {
                msg2 = DisplayError2("", error);
            }
        }

        private void RegistrarFrequencia()
        {
            if (!enableRegistro) //Se estiver como falso, não prossegue pq estará em outra aba que não seja a 1.
                return;
            zeraAvisos = 0;
            if (cont == 0)
                IDUsuarioLocalizado = 0;
            //Color padrão verde. green.
            lbLog.ForeColor = Color.Green;
            LG.Esrevelog("IDENTIFICANDO...", lbLog);


            StatusBar.Text = string.Empty;
            Byte[] fp_image = new Byte[m_ImageWidth * m_ImageHeight];
            Int32 error = (Int32)SGFPMError.ERROR_NONE;
            Int32 img_qlty = 0;

            if (openedSecugen)
                error = m_FPM.GetImage(fp_image);
            else
                LG.Esrevelog("Dispositivo biométrico não localizado. Reinicie o sistema", lbLog);

            m_FPM.GetImageQuality(m_ImageWidth, m_ImageHeight, fp_image, ref img_qlty);

            if (error == (Int32)SGFPMError.ERROR_NONE)
            {
                DrawImage(fp_image, staticPB);

                SGFPMFingerInfo finger_info = new SGFPMFingerInfo();
                finger_info.FingerNumber = (SGFPMFingerPosition)0;
                finger_info.ImageQuality = (Int16)img_qlty;
                finger_info.ImpressionType = (Int16)SGFPMImpressionType.IMPTYPE_LP;
                finger_info.ViewNumber = 1;

                // Create template
                error = m_FPM.CreateTemplate(finger_info, fp_image, m_VrfMin);

                //crud.GetUsuarioTemplateTeste(dsLp, IDEmpresa, m_VrfMin, "");

                //Se não houver erros, prossegue com a busca
                if (error == (Int32)SGFPMError.ERROR_NONE)
                {
                    //if (dsU.vwUsuarioWebService.Rows.Count == 0)
                    //{
                    //    if(dsLp.TBUsuarioLocal.Rows.Count == 0)
                    //        GetUsuarios();
                    //}
                    //Segue com a localização.
                    bool matched = false;

                    //11/06/2018 - Se houver dados no dsU (do WS), segue. Senão, fazer tudo com a base local.
                    if (dsU.vwUsuarioWebService.Rows.Count > 0)
                    {
                        for (int i = 0; i <= dsU.vwUsuarioWebService.Rows.Count - 1; i++)
                        {
                            //se não for nulo, segue com a busca. Senão, pula.
                            if (!dsU.vwUsuarioWebService[i].IsTemplate1Null())
                            {
                                error = m_FPM.MatchTemplate(dsU.vwUsuarioWebService[i].Template1, m_VrfMin, SecuGen.FDxSDKPro.Windows.SGFPMSecurityLevel.HIGHEST, ref matched);

                                SGFPMISOTemplateInfo sample_info = new SGFPMISOTemplateInfo();
                                error = m_FPM.GetIsoTemplateInfo(dsU.vwUsuarioWebService[i].Template1, sample_info);
                                //for (int z = 0; z <= sample_info.TotalSamples; z++)
                                //{
                                //Deixar o nível biométrico para HIGHER - 03/08/2018 - No normal, conforme layout padrão da fabricante
                                //error = m_FPM.MatchIsoTemplate(dsU.vwUsuarioWebService[i].Template1, z, m_VrfMin, 0, SecuGen.FDxSDKPro.Windows.SGFPMSecurityLevel.HIGHEST, ref matched);
                                if (matched)
                                {
                                    m_FPM.GetMatchingScore(dsU.vwUsuarioWebService[i].Template1, m_VrfMin, ref matchingScore);

                                    //Se menor que 120 score, fazer um comparativo para confirmar o achado evitando falso-positivo
                                    if ((matchingScore <= 120))
                                    {
                                        if (error == (Int32)SGFPMError.ERROR_NONE)
                                        {
                                            //error = m_FPM.MatchIsoTemplate(dsU.vwUsuarioWebService[i].Template1, z, m_VrfMin, 0, SecuGen.FDxSDKPro.Windows.SGFPMSecurityLevel.HIGH, ref matched);

                                            if (!matched)
                                            {
                                                LG.Esrevelog("(comp.) SERVIDOR NÃO IDENTIFICADO...", lbLog);
                                                lbLog.ForeColor = Color.Red;
                                                return;
                                            }

                                        }
                                        else
                                        {
                                            LG.Esrevelog("FALHA NO PROCESSO DE IDENTIFICAÇÃO...", lbLog);
                                            lbLog.ForeColor = Color.Red;
                                            return;
                                        }
                                    }

                                    pontonarede.ServiceSoapClient WebS = new pontonarede.ServiceSoapClient("ServiceSoap", utilConexao.tcm.UrlWS);
                                    WebS.Endpoint.Binding.OpenTimeout = TimeSpan.FromSeconds(10); //OpenTimeout p/ 3 e SendTimeout p 15
                                    WebS.Endpoint.Binding.SendTimeout = TimeSpan.FromSeconds(15);
                                    WebS.Endpoint.Binding.ReceiveTimeout = TimeSpan.FromSeconds(7);
                                    try
                                    {
                                        IdUsuarioRegistro = Convert.ToInt32(dsU.vwUsuarioWebService[i].IDUsuario);
                                        msg = WebS.BaterPontoHash(IDEmpresa, IDSetor, Convert.ToInt32(dsU.vwUsuarioWebService[i].IDUsuario),
                                           DTRelogioData, "TentoWebServiceNovamente7x24dm12", dsU.vwUsuarioWebService[i].HoraEntradaManha,
                                            dsU.vwUsuarioWebService[i].HoraSaidaManha, dsU.vwUsuarioWebService[i].HoraEntradaTarde,
                                            dsU.vwUsuarioWebService[i].HoraSaidaTarde, dsU.vwUsuarioWebService[i].TotalHoraDia,
                                            dsU.vwUsuarioWebService[i].DSUsuario, dsU.vwUsuarioWebService[i].PrimeiroNome,
                                            dsU.vwUsuarioWebService[i].RegimePlantonista, dsU.vwUsuarioWebService[i].IDVinculoUsuario, _hashMaquina, "00:00");
                                        WebS.Close();

                                        //COLOCA O REGISTRO NO TOPO DA LISTA
                                        try
                                        {
                                            DataRow item = dsU.vwUsuarioWebService.NewRow();
                                            item.ItemArray = dsU.vwUsuarioWebService[i].ItemArray.Clone() as object[];
                                            dsU.vwUsuarioWebService.Rows.RemoveAt(i);
                                            dsU.vwUsuarioWebService.Rows.InsertAt(item, 0);
                                            crudPtServer cps = new crudPtServer(utilConexao.tcm.UrlWS);
                                            cps.InsertLogRegistrosClint(IdUsuarioRegistro, IDEmpresa, IDSetor);

                                        }
                                        catch (Exception e) { }

                                        //Se algumas dessas foram encontrada, pinta de verde ou vermelho
                                        if (msg.IndexOf("efetivada") > 0 || msg.IndexOf("finalizada") > 0)
                                        {
                                            lbLog.ForeColor = System.Drawing.Color.ForestGreen;
                                        }
                                        else
                                        {
                                            lbLog.ForeColor = System.Drawing.Color.Red;
                                        }
                                        Console.Beep(750, 400);
                                        LG.Esrevelog(msg, lbLog);
                                    }
                                    catch (Exception ex)
                                    {
                                        crud = new crudPtServer(utilConexao.tcm.UrlWS);
                                        if (IDEmpresa == 0)
                                        {
                                            IDEmpresa = crud.GetIDEmpresa();
                                            IDSetor = crud.GetIDSetor();
                                        }
                                        crud.InsertOcorrencia(DTRelogioData, ex.Message.Trim(), IDEmpresa, IDSetor);
                                        //aqui - Se não deu certo enviar o ponto. faz o registro local.
                                        RegistroLocal RL = new RegistroLocal();
                                        msg = RL.InsertRegistroLocal(Convert.ToInt32(dsU.vwUsuarioWebService[i].IDUsuario), DTRelogioData, Convert.ToInt32(dsU.vwUsuarioWebService[i].IDVinculoUsuario));
                                        lbLog.ForeColor = System.Drawing.Color.Red;
                                        Console.Beep(600, 350);
                                        Console.Beep(600, 350);
                                        LG.Esrevelog(msg, lbLog);
                                        GC.Collect();

                                        try
                                        {
                                            DataRow item = dsU.vwUsuarioWebService.NewRow();
                                            item.ItemArray = dsU.vwUsuarioWebService[i].ItemArray.Clone() as object[];
                                            dsU.vwUsuarioWebService.Rows.RemoveAt(i);
                                            dsU.vwUsuarioWebService.Rows.InsertAt(item, 0);
                                            crudPtServer cps = new crudPtServer(utilConexao.tcm.UrlWS);
                                            cps.InsertLogRegistrosClint(IdUsuarioRegistro, IDEmpresa, IDSetor);

                                        }
                                        catch (Exception e) { }
                                    }
                                    //Se achar o usuário, sai dos laços aplicando o return 
                                    if (this.WindowState == FormWindowState.Minimized)
                                    {
                                        niSegundoPlano.BalloonTipText = msg;
                                        niSegundoPlano.ShowBalloonTip(2);
                                    }
                                    msg = null;
                                    IDUsuarioLocalizado = 0;
                                    cont = 0;
                                    return;
                                }
                                //}
                            }
                        }
                    }//Senão, faz td com a base local.
                    else
                    {
                        for (int i = 0; i <= dsLp.TBUsuarioLocal.Rows.Count - 1; i++)
                        {
                            //MessageBox.Show("Base Local");    
                            //se não for nulo, segue com a busca. Senão, pula.
                            if (!dsLp.TBUsuarioLocal[i].IsTemplate1Null())
                            {
                                SGFPMISOTemplateInfo sample_info = new SGFPMISOTemplateInfo();
                                error = m_FPM.GetIsoTemplateInfo(dsLp.TBUsuarioLocal[i].Template1, sample_info);

                                for (int z = 0; z <= sample_info.TotalSamples; z++)
                                {
                                    error = m_FPM.MatchIsoTemplate(dsLp.TBUsuarioLocal[i].Template1, z, m_VrfMin, 0, SecuGen.FDxSDKPro.Windows.SGFPMSecurityLevel.NORMAL, ref matched);

                                    if (matched)
                                    {
                                        if ((matchingScore <= 120))
                                        {
                                            if (error == (Int32)SGFPMError.ERROR_NONE)
                                            {
                                                error = m_FPM.MatchIsoTemplate(dsLp.TBUsuarioLocal[i].Template1, z, m_VrfMin, 0, SecuGen.FDxSDKPro.Windows.SGFPMSecurityLevel.HIGH, ref matched);

                                                if (!matched)
                                                {
                                                    LG.Esrevelog("(comp.) SERVIDOR NÃO IDENTIFICADO...", lbLog);
                                                    lbLog.ForeColor = Color.Red;
                                                    return;
                                                }

                                            }
                                            else
                                            {
                                                LG.Esrevelog("FALHA NO PROCESSO DE IDENTIFICAÇÃO...", lbLog);
                                                lbLog.ForeColor = Color.Red;
                                                return;
                                            }
                                        }

                                        pontonarede.ServiceSoapClient WebS = new pontonarede.ServiceSoapClient("ServiceSoap", utilConexao.tcm.UrlWS);
                                        WebS.Endpoint.Binding.OpenTimeout = TimeSpan.FromSeconds(10); //OpenTimeout p/ 3 e SendTimeout p 15
                                        WebS.Endpoint.Binding.SendTimeout = TimeSpan.FromSeconds(15);
                                        WebS.Endpoint.Binding.ReceiveTimeout = TimeSpan.FromSeconds(7);
                                        try
                                        {
                                            IdUsuarioRegistro = Convert.ToInt32(dsU.vwUsuarioWebService[i].IDUsuario);
                                            msg = WebS.BaterPontoHash(IDEmpresa, IDSetor, Convert.ToInt32(dsLp.TBUsuarioLocal[i].IDUsuario),
                                                DTRelogioData, "TentoWebServiceNovamente7x24dm12", dsLp.TBUsuarioLocal[i].HoraEntradaManha,
                                                dsLp.TBUsuarioLocal[i].HoraSaidaManha, dsLp.TBUsuarioLocal[i].HoraEntradaTarde,
                                                dsLp.TBUsuarioLocal[i].HoraSaidaTarde, dsLp.TBUsuarioLocal[i].TotalHoraDia,
                                                dsLp.TBUsuarioLocal[i].DSUsuario, "", dsLp.TBUsuarioLocal[i].RegimePlantonista, dsLp.TBUsuarioLocal[i].IDVinculoUsuario, _hashMaquina, "00:00");
                                            WebS.Close();

                                            //COLOCA O REGISTRO NO TOPO DA LISTA
                                            try
                                            {
                                                DataRow item = dsLp.TBUsuarioLocal.NewRow();
                                                item.ItemArray = dsLp.TBUsuarioLocal[i].ItemArray.Clone() as object[];
                                                dsLp.TBUsuarioLocal.Rows.RemoveAt(i);
                                                dsLp.TBUsuarioLocal.Rows.InsertAt(item, 0);
                                                crudPtServer cps = new crudPtServer(utilConexao.tcm.UrlWS);
                                                cps.InsertLogRegistrosClint(IdUsuarioRegistro, IDEmpresa, IDSetor);

                                            }
                                            catch (Exception e) { }
                                            //Se algumas dessas foram encontrada, pinta de verde ou vermelho
                                            if (msg.IndexOf("efetivada") > 0 || msg.IndexOf("finalizada") > 0)
                                            {
                                                lbLog.ForeColor = System.Drawing.Color.ForestGreen;
                                            }
                                            else
                                            {
                                                lbLog.ForeColor = System.Drawing.Color.Red;
                                            }
                                            Console.Beep(750, 400);
                                            LG.Esrevelog(msg, lbLog);
                                        }
                                        catch (Exception ex)
                                        {
                                            IdUsuarioRegistro = Convert.ToInt32(dsLp.TBUsuarioLocal[i].IDUsuario);
                                            crud = new crudPtServer(utilConexao.tcm.UrlWS);
                                            if (IDEmpresa == 0)
                                            {
                                                IDEmpresa = crud.GetIDEmpresa();
                                                IDSetor = crud.GetIDSetor();
                                            }
                                            crud.InsertOcorrencia(DTRelogioData, ex.Message.Trim(), IDEmpresa, IDSetor);
                                            //aqui - Se não deu certo enviar o ponto. faz o registro local.
                                            RegistroLocal RL = new RegistroLocal();
                                            msg = RL.InsertRegistroLocal(Convert.ToInt32(dsLp.TBUsuarioLocal[i].IDUsuario), DTRelogioData, Convert.ToInt32(dsLp.TBUsuarioLocal[i].IDVinculoUsuario));
                                            lbLog.ForeColor = System.Drawing.Color.Red;
                                            Console.Beep(600, 350);
                                            Console.Beep(600, 350);
                                            LG.Esrevelog(msg, lbLog);
                                            //COLOCA O REGISTRO NO TOPO DA LISTA
                                            try
                                            {
                                                DataRow item = dsLp.TBUsuarioLocal.NewRow();
                                                item.ItemArray = dsLp.TBUsuarioLocal[i].ItemArray.Clone() as object[];
                                                dsLp.TBUsuarioLocal.Rows.RemoveAt(i);
                                                dsLp.TBUsuarioLocal.Rows.InsertAt(item, 0);
                                                crudPtServer cps = new crudPtServer(utilConexao.tcm.UrlWS);
                                                cps.InsertLogRegistrosClint(IdUsuarioRegistro, IDEmpresa, IDSetor);

                                            }
                                            catch (Exception e) { }
                                        }
                                        //Se achar o usuário, sai dos laços aplicando o return 
                                        if (this.WindowState == FormWindowState.Minimized)
                                        {
                                            niSegundoPlano.BalloonTipText = msg;
                                            niSegundoPlano.ShowBalloonTip(2);
                                        }
                                        msg = null;
                                        return;
                                    }
                                }
                            }
                        }
                    }

                    if (!matched)
                    {
                        lbLog.ForeColor = System.Drawing.Color.Red;
                        LG.Esrevelog("SERVIDOR NÃO IDENTIFICADO.", lbLog);
                        Console.Beep(600, 350);
                        Console.Beep(600, 350);


                        if (this.WindowState == FormWindowState.Minimized)
                        {
                            niSegundoPlano.BalloonTipText = "SERVIDOR NÃO IDENTIFICADO.";
                            niSegundoPlano.ShowBalloonTip(2);
                        }
                    }


                    //matched = false;
                    fp_image = null;
                    //System.GC.Collect();

                }
                else
                {
                    LG.Esrevelog(string.Format("Biometria não extraída: Cod: {0}", error), lbLog);
                    StatusBar.Text = "Erro ao extrair a biometria. Erro: " + error;
                    ColorStatusBar(false);
                }
            }
            else
            {
                DisplayError("", error);
                lbLog.ForeColor = Color.Red;
                LG.Esrevelog(string.Format("SERVIDOR NÃO IDENTIFICADO: {0}", text), lbLog);

                if (this.WindowState == FormWindowState.Minimized)
                {
                    niSegundoPlano.BalloonTipText = string.Format("SERVIDOR NÃO IDENTIFICADO: {0}", text);
                    niSegundoPlano.ShowBalloonTip(2);
                }
                text = string.Empty;
            }

        }
        #endregion

        #region Verifica rede
        private string GetComputadorUserOS(int Informacao)
        {
            if (Informacao == 1)
            {
                try
                {
                    return Environment.MachineName.ToString();
                }
                catch (Exception ex)
                {
                    return string.Format("Erro Nome do Computador: {0}", ex.Message.Trim());
                }
            }
            if (Informacao == 2)
            {
                try
                {
                    var teste = Environment.CurrentDirectory;
                    var version = Environment.Version;
                    var version2 = Environment.UserDomainName;
                    return Environment.UserName.ToString();
                }
                catch (Exception ex)
                {
                    return string.Format("Erro Usuário local: {0}", ex.Message.Trim());
                }
            }
            if (Informacao == 3)
            {
                try
                {

                    return Environment.OSVersion.ToString();
                }
                catch (Exception ex)
                {
                    return string.Format("Erro Usuário local: {0}", ex.Message.Trim());
                }
            }

            return "";
        }
        private string IPLocal()
        {

            try
            {
                IPAddress[] ip = Dns.GetHostAddresses(Dns.GetHostName());
                return ip[1].ToString();
            }
            catch (Exception ex)
            {
                return "Não obtido: " + ex.ToString();
            }
        }
        [DllImport("wininet.dll")] //dll do windows ... Verifica estado da conexão.

        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);
        public Boolean IsConnected()
        {
            int Desc;

            return InternetGetConnectedState(out Desc, 0);
        }
        private bool DispServidor()
        {
            try
            {
                pontonarede.ServiceSoapClient WebS = new pontonarede.ServiceSoapClient("ServiceSoap", utilConexao.tcm.UrlWS);
                WebS.Endpoint.Binding.OpenTimeout = TimeSpan.FromSeconds(2);
                WebS.Endpoint.Binding.SendTimeout = TimeSpan.FromSeconds(2);
                WebS.Open();
                WebS.DisponibilidadeServidor(IDEmpresa, IDSetor);
                WebS.Close();
                return true;

            }
            catch (Exception ex)
            {
                ex.ToString();
                webS.Close();
                return false;
            }
        }
        #endregion

        #region atividades em background
        private void bwRegistro_DoWork(object sender, DoWorkEventArgs e)
        {
            //Trabalhando em segundo plano.
            if (_spBackGround.Elapsed.Seconds > 6)
            {
                _ComThread = true;
                _EnvioImediato = false;
                VerificaConexoes();
                _spBackGround.Reset();
                _spBackGround.Start();
            }
        }

        void Registro_DoWork()
        {

            if (IsConnected() && DispServidor())
            {
                msgRede = false;

                try
                {
                    //Envia registros locais
                    RegistroLocal rl = new RegistroLocal();
                    IDEmpresa = crud.GetIDEmpresa();
                    IDSetor = crud.GetIDSetor();
                    rl.EnviarFrequencia(DTRelogioData, IDEmpresa, IDSetor);
                }
                catch (Exception ex)
                {
                    msgrede = string.Format("Falha ao enviar marcações locais: {0}", ex.Message.Trim());
                }
            }
            else if (!IsConnected())
            {
                msgRede = true;
                msgrede = "Cabo de rede desconectado ou wifi indisponível. Sitema operando em offline. Contate o administrador.";
                //lbrede.Visible = true;
            }
            else if (!DispServidor())
            {
                msgRede = true;
                msgrede = "WS de comunicação indisponível no momento. Sistema operando em offline. Contate o administrador.";
                //lbrede.Visible = true;
                //lbrede.Update();
            }
            else
                msgRede = false;
        }

        private void OnKillProcessComplete(IAsyncResult result)
        {
            //Console.WriteLine("Process killed ...");
            //Console.WriteLine(utilConexao.GetSqlInsert());

            Thread.Sleep(5000);
        }

        public void VerificaConexoes()
        {
            if (_ComThread)
            {
                utilConexao.AtualizaStatusThread(GetDeviceConnection());
                if (configSinc != null)
                {
                    utilConexao.QtdeMinutos = configSinc.MinutosLogConexao;
                }
                else
                {
                    utilConexao.QtdeMinutos = 10;
                }
                
            }
            else
            {
                utilConexao.QtdeMinutos = 0;
                utilConexao.AtualizaStatus(GetDeviceConnection());
                _spBackGround.Reset();
                _spBackGround.Start();
            }

            if (utilConexao.tcm.ConexaoRedeLocal == 1 && (utilConexao.tcm.ConexaoWSInterno == 1 || utilConexao.tcm.ConexaoWSExterno == 1))
            {
                utilConexao.ConexaoOK = true;
                msgRede = false;
                try
                {
                    //Envia registros locais
                    IDEmpresa = crud.GetIDEmpresa();
                    IDSetor = crud.GetIDSetor();
                    var threadEnviarFrequencia = new Thread(() =>
                    {
                        RegistroLocal rl = new RegistroLocal();
                        rl.EnviarFrequencia(DTRelogioData, IDEmpresa, IDSetor);
                    });
                    threadEnviarFrequencia.Start();
                }
                catch (Exception ex)
                {
                    msgrede = string.Format("Falha ao enviar marcações locais: {0}", ex.Message.Trim());
                }
            }
            else if (utilConexao.tcm.ConexaoRedeLocal == 0)
            {
                utilConexao.ConexaoOK = false;
                msgRede = true;
                msgrede = utilConexao.tcm.ConexaoRedeLocalMensagem;
                //lbrede.Visible = true;
            }
            else if (utilConexao.tcm.ConexaoWSInterno == 0 && utilConexao.tcm.ConexaoWSExterno == 0)
            {
                utilConexao.ConexaoOK = false;
                msgRede = true;
                if (utilConexao.tcm.ConexaoRedeLocal == 0)
                {
                    msgrede = "Maquina local sem conexao com a rede";
                }
                else
                {
                    msgrede = "WS - Web Service";//"Maquina local sem conexao com a rede (" + utilConexao.tcm.ConexaoWSInternoMensagem + ")";
                }
                    
                //"WS de comunicação indisponível no momento. Sistema operando em offline. Contate o administrador.";
                //lbrede.Visible = true;
                //lbrede.Update();
            }
            else
            {
                utilConexao.ConexaoOK = true;
                msgRede = false;
                msgrede = "";
            }

            //VERIFICA A CONEXÃO COM O LEITOR LOCAL
            if (utilConexao.tcm.ConexaoLeitor == 0 && !msgRede)
            {
                msgRede = true;
                msgrede = utilConexao.tcm.ConexaoLeitorMensagem;
            }
            //VERIFICA CONEXAO COM O BANCO LOCAL
            else if (utilConexao.tcm.ConexaoBancoLocal == 0 && !msgRede)
            {
                msgRede = true;
                msgrede = utilConexao.tcm.ConexaoBancoLocalMensagem;
            }
            

            if (utilConexao.swServidor.Elapsed.Minutes > utilConexao.QtdeMinutos || _EnvioImediato)
            {
                Console.WriteLine("DEZ MINUTOS");
                Console.WriteLine(utilConexao.swServidor.Elapsed.ToString());
                crudPtServer crud = new crudPtServer(utilConexao.tcm.UrlWS);
                pontonarede.TesteConexaoModel[] list = crud.InsertLogConexoes(utilConexao.tcm).ToArray();
                //ENVIA OS REGISTROS DE LOGS DE CONEXÃO AO SERVIDOR
                var threadEnviarLog = new Thread(() =>
                {
                    pontonarede.ServiceSoapClient WebS = new pontonarede.ServiceSoapClient("ServiceSoap", utilConexao.tcm.UrlWS);
                    WebS.Endpoint.Binding.OpenTimeout = TimeSpan.FromSeconds(10); //OpenTimeout p/ 3 e SendTimeout p 15
                    WebS.Endpoint.Binding.SendTimeout = TimeSpan.FromSeconds(15);
                    WebS.Endpoint.Binding.ReceiveTimeout = TimeSpan.FromSeconds(7);
                    try
                    {
                        WebS.Open();
                        var retorno = WebS.SalvarLogConexoes(list, IDEmpresa, IDSetor, _hashMaquina, "TentoWebServiceNovamente7x24dm12");
                        WebS.Close();

                        if (retorno)
                        {
                            crud.AtualizaLogsConexoes();
                        }
                    }
                    catch
                    {
                    }
                });
                threadEnviarLog.Start();

                utilConexao.swServidor.Reset();
                utilConexao.swServidor.Start();

                //PEGA AS CONFIGURAÇÕES DE SINCRONIZACAO
                var threadConfig = new Thread(() =>
                {
                    pontonarede.ServiceSoapClient WebS = new pontonarede.ServiceSoapClient("ServiceSoap", utilConexao.tcm.UrlWS);
                    WebS.Endpoint.Binding.OpenTimeout = TimeSpan.FromSeconds(10); //OpenTimeout p/ 3 e SendTimeout p 15
                    WebS.Endpoint.Binding.SendTimeout = TimeSpan.FromSeconds(15);
                    WebS.Endpoint.Binding.ReceiveTimeout = TimeSpan.FromSeconds(7);
                    try
                    {
                        WebS.Open();
                        var retorno = WebS.GetConfigHorasSincronizacao(IDEmpresa, IDSetor, _hashMaquina, "TentoWebServiceNovamente7x24dm12");
                        WebS.Close();

                        if (retorno != null)
                        {
                            configSinc = retorno;
                        }
                    }
                    catch
                    {
                    }
                });
                threadConfig.Start();


            }

            if (utilConexao.tcm.UrlWS != _UrlWSAnterior)
            {
                EndpointIdentity spn = EndpointIdentity.CreateSpnIdentity("host/mikev-ws");
                Uri uri = new Uri(utilConexao.tcm.UrlWS);
                var address = new EndpointAddress(uri, spn);
                var client = new pontonarede.ServiceSoapClient("ServiceSoap", address);
                _UrlWSAnterior = utilConexao.tcm.UrlWS;
            }

        }
        #endregion

        #region Relógio
        private void ExecutaRelogio(object source, ElapsedEventArgs e)
        {

            ContadorRelogio++;
            //Passar 4 minutos, busca novamente a hora no servidor;
            if (ContadorRelogio == 240)
            {
                DTRelogioData = HoraServidor2();
                ContadorRelogio = 0;
            }
            else
            {
                DTRelogioData = DTRelogioData.AddSeconds(1);
            }
        }

        private void IniciaRelagio()
        {
            System.Timers.Timer relogio = new System.Timers.Timer();
            relogio.Elapsed += new ElapsedEventHandler(ExecutaRelogio);
            relogio.Interval = 1000;
            relogio.Enabled = true;
        }
        #endregion

        #region Registro de Ponto em background

        #endregion


        void CheckPressKey(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\u0014')
            {
                var thread = new Thread(() =>
                {
                    FrmListaLogsConexao frm = new FrmListaLogsConexao();
                    frm.ShowDialog();
                });
                thread.Start();
                // Enter key pressed
            }
        }
    }
}
