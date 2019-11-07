using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlServerCe;

namespace pnrClient
{
    public class crudPtServer
    {
        private string _UrlWS;
        public crudPtServer(string UrlWS)
        {
            _UrlWS = UrlWS;
        }
        #region Variáveis
        private string localApp = string.Format("{0}", System.Environment.CurrentDirectory.ToString() + "\\ptServer.db");
        private string DSEmpresa;
        private string DSSetor;
        SqlCeConnection SQLConn;
        SqlCeDataAdapter DA;
        byte[] template;
        string cod;

        #endregion
        //ANTENÇÃO - DESCONTINUADO POR NÃO TRABALHAR COM CAMPOS BYTE[]. Futuramente, tentar usar o tipo blob para esta situa
        //ção no SQLite.
        //public DataTable GetUsuario(DataTable TB, int IDEmpresa)
        //{
        //    string stringConn = @"Data Source="+localApp;
        //    DataTable localTBusuario = new DataTable();
        //    string instSQL = "Select * from TBUsuario";
        //    SQLiteConnection conn = new SQLiteConnection(stringConn);
        //    SQLiteDataAdapter da = new SQLiteDataAdapter(instSQL, conn);
        //    da.Fill(localTBusuario);
        //    return localTBusuario;
        //}

        #region IUD

        //public int VerificaRegistro()
        //{
        //    //Variáveis.
        //    DataTable TBCH;
        //    DA = null;
        //    DA = new SqlCeDataAdapter("Select * from TBUsuarioLocal where Template1 = @Template1", SQLConn); //Instância DataAdapter com o cod a executar e a conexão
        //    TBCH = null;
        //    TBCH = new DataTable(); //Instância DataTable.
        //    DA.Fill(TBCH); //preenche tabela
        //    string Processador, HD, MAC;

        //    if (TBCH.Rows.Count == 0) //Se igual a zero, insere na tabela.
        //    {
        //        HD = TBCH.Rows[0]["SERIALHD"].ToString();
        //        //SQLConn.Close();
        //        //InsereDados();
        //        //retorno = "C";
        //    }
        //}

        public bool ImportUsuarios(int IDEmpresa, int IDSetor)
        {
            byte[] template2;
            pontonarede.ServiceSoapClient webS = new pontonarede.ServiceSoapClient("ServiceSoap", _UrlWS);
            pontonarede.DataSetUsuario dsU = new pontonarede.DataSetUsuario();
            string matricula;

            webS.Open();

            try
            {
                dsU = webS.UsuariosPonto(IDEmpresa, "TentoWebServiceNovamente7x24dm12");
                DataSetpnrClientTableAdapters.TBUsuarioLocalTableAdapter adpUser =
                    new DataSetpnrClientTableAdapters.TBUsuarioLocalTableAdapter();
                adpUser.Connection.ConnectionString = pnrClient.Properties.Settings.Default.ptServer35ConnectionString;

                if (dsU.vwUsuarioWebService.Rows.Count > 0)
                    DeleteUsuario();

                DeleteTBUsuarioLocalsetor();

                for (int i = 0; i <= dsU.vwUsuarioWebService.Rows.Count - 1; i++)
                {
                    if (!dsU.vwUsuarioWebService[i].IsMatriculaNull())
                        matricula = dsU.vwUsuarioWebService[i].Matricula.Trim();
                    else
                        matricula = string.Empty;

                    if (!dsU.vwUsuarioWebService[i].IsTemplate2Null())
                        template2 = dsU.vwUsuarioWebService[i].Template2;
                    else
                        template2 = null;

                    if (!dsU.vwUsuarioWebService[i].IsTemplate1Null())
                    {
                        InsertModifyUsuario(string.Empty, matricula, dsU.vwUsuarioWebService[i].PrimeiroNome,
                        dsU.vwUsuarioWebService[i].DSUsuario, dsU.vwUsuarioWebService[i].CadastraDigital,
                        dsU.vwUsuarioWebService[i].IDTPUsuario, string.Empty, dsU.vwUsuarioWebService[i].Template1,
                        null, dsU.vwUsuarioWebService[i].IDUsuario, Convert.ToInt32(dsU.vwUsuarioWebService[i].IDVinculoUsuario), true,
                            dsU.vwUsuarioWebService[i].HoraEntradaManha, dsU.vwUsuarioWebService[i].HoraSaidaManha, dsU.vwUsuarioWebService[i].HoraEntradaTarde,
                            dsU.vwUsuarioWebService[i].HoraEntradaTarde, dsU.vwUsuarioWebService[i].TotalHoraDia, dsU.vwUsuarioWebService[i].RegimePlantonista, "");

                        if (dsU.vwUsuarioWebService[i].IDSetor == IDSetor)
                        {
                            InsertTBUsuarioLocalSetor(dsU.vwUsuarioWebService[i].IDUsuario, dsU.vwUsuarioWebService[i].DSUsuario, dsU.vwUsuarioWebService[i].CadastraDigital,
                                dsU.vwUsuarioWebService[i].IDTPUsuario, dsU.vwUsuarioWebService[i].PrimeiroNome,
                                null, dsU.vwUsuarioWebService[i].Template1, matricula, dsU.vwUsuarioWebService[i].Login,
                                template2, Convert.ToInt32(dsU.vwUsuarioWebService[i].IDVinculoUsuario), dsU.vwUsuarioWebService[i].HoraEntradaManha,
                                dsU.vwUsuarioWebService[i].HoraSaidaManha, dsU.vwUsuarioWebService[i].HoraEntradaTarde,
                                dsU.vwUsuarioWebService[i].HoraSaidaTarde, dsU.vwUsuarioWebService[i].TotalHoraDia, dsU.vwUsuarioWebService[i].RegimePlantonista);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                webS.Close();
                return false;
            }
            webS.Close();
            return true;
        }
        public bool InsertUsuarioLocal(int IDUsuario, string DSUsuario, string TextHashCode, byte[] Template1, byte[] Template2, string Matricula, string CPF, string SenhaDigital, bool CadastraDigital, int IDTPUsuario,
            string PrimeiroNome, int IDVinculoUsuario, string TextHasCode, string HoraEntradaManha, string HoraSaidaManha,
            string HoraEntradaTarde, string HoraSaidaTArde, int TotalHoraDia, bool RegimePlantonista)
        {
            DataSetpnrClientTableAdapters.TBUsuarioLocalTableAdapter adpUsuario = new DataSetpnrClientTableAdapters.TBUsuarioLocalTableAdapter();

            //acerta a conexão de acordo com a pasta padrão da instalação.
            adpUsuario.Connection.ConnectionString = pnrClient.Properties.Settings.Default.ptServer35ConnectionString;
            try
            {
                //Tentando fazer em lote.
                // adpUsuario.Adapter.InsertCommand = "";

                adpUsuario.Insert(IDUsuario, DSUsuario, TextHasCode, CadastraDigital, IDTPUsuario, DateTime.Now, PrimeiroNome,
                    Template1, Matricula, CPF, IDVinculoUsuario, RegimePlantonista, TotalHoraDia, HoraSaidaTArde, HoraEntradaTarde,
                    HoraSaidaManha, HoraEntradaManha, Template2);
                return true;
            }
            catch (DataException ex)
            {
                ex.ToString();
                return false;
            }
        }
        public bool InsertEmpresa(int IDEmpresa, string DSEmpresa, string Sigla)
        {
            DataSetpnrClientTableAdapters.TBEmpresaTableAdapter adpEmpresa = new DataSetpnrClientTableAdapters.TBEmpresaTableAdapter();
            adpEmpresa.Connection.ConnectionString = pnrClient.Properties.Settings.Default.ptServer35ConnectionString;
            try
            {
                adpEmpresa.Insert(IDEmpresa, DSEmpresa, Sigla);
                return true;
            }
            catch (DataException ex)
            {
                ex.ToString();
                return false;
            }
        }
        public bool InsertSetor(int IDSetor, string DSEmpesa, string Sigla)
        {
            DataSetpnrClientTableAdapters.TBSetorTableAdapter adpSetor = new DataSetpnrClientTableAdapters.TBSetorTableAdapter();
            adpSetor.Connection.ConnectionString = pnrClient.Properties.Settings.Default.ptServer35ConnectionString;
            try
            {
                adpSetor.Insert(IDSetor, DSEmpesa, Sigla);
                return true;
            }
            catch (DataException ex)
            {
                ex.ToString();
                return false;
            }
        }
        public bool InsertCH_Hardware(string SerialProcessador, string SerialHD, string EnderecoMAC, string ModeloProcessador, string ArquiteturaMaquina,
            string TotalMemoria, string CapacidadeHD, string EspacoLivreHD, string DispositivoBiometrico, string versaoClient)
        {
            DataSetpnrClientTableAdapters.CH_HARDWARETableAdapter adpHard = new DataSetpnrClientTableAdapters.CH_HARDWARETableAdapter();
            adpHard.Connection.ConnectionString = pnrClient.Properties.Settings.Default.ptServer35ConnectionString;

            try
            {
                adpHard.Insert(SerialProcessador, SerialHD, EnderecoMAC, ModeloProcessador, ArquiteturaMaquina, TotalMemoria, CapacidadeHD, EspacoLivreHD, DispositivoBiometrico, versaoClient);
                return true;
            }
            catch (DataException ex)
            {
                ex.ToString();
                return false;
            }
        }

        public void DeleteUsuario()
        {
            DataSetpnrClientTableAdapters.TBUsuarioLocalTableAdapter adpUsuario = new DataSetpnrClientTableAdapters.TBUsuarioLocalTableAdapter();
            adpUsuario.Connection.ConnectionString = pnrClient.Properties.Settings.Default.ptServer35ConnectionString;
            adpUsuario.Delete();
        }

        public bool InsertModifyUsuario(string CPF, string Matricula, string PrimeiroNome, string Nome, bool CadastraDigital,
            int IDTPUsuario, string SenhaDigital, byte[] Template1, byte[] Template2, int IDUsuario, int IDVinculo, bool ApagarAnteriores,
            string HoraEntradaManha, string HoraSaidaManha, string HoraEntradaTarde, string HoraSaidaTarde,
            int TotalHoraDia, bool RegimePlantonista, string Hash)
        {
            DataSetpnrClientTableAdapters.TBUsuarioLocalTableAdapter adpUsuario = new DataSetpnrClientTableAdapters.TBUsuarioLocalTableAdapter();
            //DataSetpnrClient.TBUsuarioLocalDataTable TBUsuario = new DataSetpnrClient.TBUsuarioLocalDataTable();

            adpUsuario.Connection.ConnectionString = pnrClient.Properties.Settings.Default.ptServer35ConnectionString;
            try
            {
                //SqlCeDataAdapter adapter = new SqlCeDataAdapter();
                //adapter.InsertCommand = new SqlCeCommand("Insert into TBUsuarioLocal(IDUsuarioLocal) Values(1)",new SqlCeConnection(pnrClient.Properties.Settings.Default.ptServer35ConnectionString));
                //adapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                //adapter.UpdateBatchSize = 10;
                //adpUsuario.Adapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                //adpUsuario.Adapter.UpdateBatchSize = 10;

                adpUsuario.Insert(IDUsuario, Nome, Hash, CadastraDigital, IDTPUsuario, DateTime.Now, PrimeiroNome, Template1,
                    Matricula, CPF, IDVinculo, RegimePlantonista, TotalHoraDia, HoraSaidaTarde, HoraEntradaTarde, HoraSaidaManha, HoraEntradaManha, Template2);
                //adpUsuario.Update(TBUsuario);
                return true;
            }
            catch (DataException ex)
            {
                ex.ToString();
                return false;
            }
        }
        public bool InsertTemplate(int IDVinculoUsuario, byte[] Template)
        {
            DataSetpnrClientTableAdapters.TBUsuarioLocalTableAdapter adpUsuario =
                new DataSetpnrClientTableAdapters.TBUsuarioLocalTableAdapter();
            adpUsuario.Connection.ConnectionString = pnrClient.Properties.Settings.Default.ptServer35ConnectionString;
            try
            {
                adpUsuario.UpdateTemplate(Template, IDVinculoUsuario);
                return true;
            }
            catch (Exception Ex)
            {
                Ex.ToString();
                return false;
            }
        }
        public bool InsertTemplate2(int IDVinculoUsuario, byte[] Template, byte[] Template2)
        {
            DataSetpnrClientTableAdapters.TBUsuarioLocalTableAdapter adpUsuario =
                new DataSetpnrClientTableAdapters.TBUsuarioLocalTableAdapter();
            adpUsuario.Connection.ConnectionString = pnrClient.Properties.Settings.Default.ptServer35ConnectionString;

            if (Template != null && Template2 != null)
            {
                try
                {
                    adpUsuario.UpdateTemplate1_2(Template, Template2, IDVinculoUsuario);
                    return true;
                }
                catch (Exception Ex)
                {
                    Ex.ToString();
                    return false;
                }
            }

            if (Template != null && Template2 == null)
            {
                try
                {
                    adpUsuario.UpdateTemplate(Template, IDVinculoUsuario);
                    return true;
                }
                catch (Exception Ex)
                {
                    Ex.ToString();
                    return false;
                }
            }

            return false;
        }
        //23/08/2018 -- DEIXAR DO JEITO QUE ESTÁ. IMPORTAR NA ROTINA DE IMPORTACAO O TEMPLATE 2.
        public bool InsertTemplate2(int IDVinculoUsuario, byte[] Template)
        {
            DataSetpnrClientTableAdapters.TBUsuarioLocalTableAdapter adpUsuario =
                new DataSetpnrClientTableAdapters.TBUsuarioLocalTableAdapter();

            DataSetpnrClient.TBUsuarioLocalDataTable TBUserLocal = new DataSetpnrClient.TBUsuarioLocalDataTable();
            adpUsuario.Connection.ConnectionString = pnrClient.Properties.Settings.Default.ptServer35ConnectionString;

            adpUsuario.FillIDVinculoUsuario(TBUserLocal, IDVinculoUsuario);

            if (TBUserLocal.Rows.Count > 0)
            {
                if (TBUserLocal[0].IsTemplate1Null() && TBUserLocal[0].IsTemplate2Null())
                {
                    try
                    {
                        adpUsuario.UpdateTemplate(Template, IDVinculoUsuario);
                        return true;
                    }
                    catch (Exception Ex)
                    {
                        Ex.ToString();
                        return false;
                    }
                }

                if (!TBUserLocal[0].IsTemplate1Null() && TBUserLocal[0].IsTemplate2Null())
                {
                    //template2
                }


                if (!TBUserLocal[0].IsTemplate1Null() && !TBUserLocal[0].IsTemplate2Null())
                {
                    try
                    {
                        adpUsuario.UpdateTemplate(Template, IDVinculoUsuario);
                        return true;
                    }
                    catch (Exception Ex)
                    {
                        Ex.ToString();
                        return false;
                    }
                }
            }

            return false;

        }

        public bool InsertEmpresaSetor(string IDEmpresa, string DSEmpresa, string IDSetor, string DSSetor)
        {
            //Cript cript = new Cript();   
            //IDEmpresa = cript.ActionEncrypt(IDEmpresa);
            //IDSetor = cript.ActionEncrypt(IDSetor);
            DataSetpnrClient dsL = new DataSetpnrClient();
            DataSetpnrClientTableAdapters.TBEmpresaTableAdapter adpEmpresa = new DataSetpnrClientTableAdapters.TBEmpresaTableAdapter();
            DataSetpnrClientTableAdapters.TBSetorTableAdapter adpsetor = new DataSetpnrClientTableAdapters.TBSetorTableAdapter();
            try
            {
                adpEmpresa.Connection.ConnectionString = pnrClient.Properties.Settings.Default.ptServer35ConnectionString;
                adpsetor.Connection.ConnectionString = pnrClient.Properties.Settings.Default.ptServer35ConnectionString;
                adpEmpresa.Fill(dsL.TBEmpresa);
                adpsetor.Fill(dsL.TBSetor);

                if (dsL.TBEmpresa.Rows.Count > 0)
                    adpEmpresa.Delete();

                if (dsL.TBSetor.Rows.Count > 0)
                    adpsetor.Delete();

                adpEmpresa.Insert(Convert.ToInt32(IDEmpresa), DSEmpresa, "");
                adpsetor.Insert(Convert.ToInt32(IDSetor), DSSetor, "");

                return true;

                //if (!System.IO.File.Exists(string.Format("{0}\\config.xml", caminho)))
                //{

                //XmlTextWriter escritor = new XmlTextWriter(string.Format("{0}\\config.xml", caminho), null);

                //Inicia o documento xml
                //escritor.WriteStartDocument();

                //Escreve elemento raiz
                //escritor.WriteStartElement("config");
                //escritor.WriteElementString("IDEmpresa", IDEmpresa.ToString());
                //escritor.WriteElementString("IDSetor", IDSetor.ToString());
                //escritor.WriteElementString("DSEmpresa", cbEmpresa.Text.Trim());
                //escritor.WriteElementString("DSSetor", cbSetor.Text.Trim());
                //escritor.WriteEndElement();

                //escritor.Close();

                //Mensagem de sucesso aqui.
                //}
            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }

        }

        //Manutenção biométrica da Secugen. Salva os templates na tabela de vínculos
        public string InsertModifyTemplate(int IDEmpresa, int IDVinculoUsuario, byte[] Template, string CPF)
        {
            pontonarede.ServiceSoapClient webS = new pontonarede.ServiceSoapClient("ServiceSoap", _UrlWS);
            pontonarede.DataSetUsuario dsU = new pontonarede.DataSetUsuario();
            webS.Endpoint.Binding.OpenTimeout = TimeSpan.FromSeconds(10); //OpenTimeout p/ 3 e SendTimeout p 15
            webS.Endpoint.Binding.SendTimeout = TimeSpan.FromSeconds(10);
            webS.Endpoint.Binding.ReceiveTimeout = TimeSpan.FromSeconds(7);
            try
            {
                webS.ManutencaoTemplate(IDVinculoUsuario, IDEmpresa, Template, CPF);
                return "Biometria incluída com sucesso";
            }
            catch (Exception ex)
            {
                ex.ToString();
                return "Erro: " + ex.Message.Trim() + " Biometria salva localmente.";
            }

        }
        public string InsertModifyTemplate2(int IDEmpresa, int IDVinculoUsuario, byte[] Template1, byte[] Template2, string CPF)
        {
            pontonarede.ServiceSoapClient webS = new pontonarede.ServiceSoapClient("ServiceSoap", _UrlWS);
            pontonarede.DataSetUsuario dsU = new pontonarede.DataSetUsuario();
            webS.Endpoint.Binding.OpenTimeout = TimeSpan.FromSeconds(10); //OpenTimeout p/ 3 e SendTimeout p 15
            webS.Endpoint.Binding.SendTimeout = TimeSpan.FromSeconds(10);
            webS.Endpoint.Binding.ReceiveTimeout = TimeSpan.FromSeconds(7);
            try
            {
                webS.ManutencaoTemplate2(IDVinculoUsuario, IDEmpresa, Template1, Template2, CPF);
                return "Biometria incluída com sucesso";
            }
            catch (Exception ex)
            {
                ex.ToString();
                return "Erro: " + ex.Message.Trim() + " Biometria salva localmente.";
            }

        }
        //28/06/2018 -- Insert de Ocorrencias
        public void InsertOcorrencia(DateTime dtOcorrencia, string DSOcorrencia, int IDEmpresa, int IDSetor)
        {
            //Primeiramente, pegar as informações de Hardware
            //deixar sem IPLocal e Nome da máquina por enquanto. Pegar Direto na TBClient do sistema pq lá tem essas informações.

            try
            {
                DataSetpnrClientTableAdapters.TBOcorrenciaTableAdapter adpOcorrencia =
                    new DataSetpnrClientTableAdapters.TBOcorrenciaTableAdapter();
                adpOcorrencia.Connection.ConnectionString = pnrClient.Properties.Settings.Default.ptServer35ConnectionString;
                adpOcorrencia.Insert(dtOcorrencia, DSOcorrencia, IDEmpresa, IDSetor, string.Empty,
                    string.Empty, string.Empty);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        public void DeleteOcorrencia()
        {
            DataSetpnrClientTableAdapters.TBOcorrenciaTableAdapter adpOcorrencia =
    new DataSetpnrClientTableAdapters.TBOcorrenciaTableAdapter();
            adpOcorrencia.Connection.ConnectionString = pnrClient.Properties.Settings.Default.ptServer35ConnectionString;

            try
            {
                adpOcorrencia.DeleteOcorrencia();
            }
            catch
            {

            }
        }
        public void EnviaOcorrencia()
        {
            pontonarede.ServiceSoapClient webS = new pontonarede.ServiceSoapClient("ServiceSoap", _UrlWS);
            webS.Endpoint.Binding.OpenTimeout = TimeSpan.FromSeconds(3); //OpenTimeout p/ 3 e SendTimeout p 15
            webS.Endpoint.Binding.SendTimeout = TimeSpan.FromSeconds(5);
            webS.Endpoint.Binding.ReceiveTimeout = TimeSpan.FromSeconds(5);
            DataSetpnrClient.TBOcorrenciaDataTable TBOcorrencia = new DataSetpnrClient.TBOcorrenciaDataTable();
            DataSetpnrClientTableAdapters.TBOcorrenciaTableAdapter adpOcorrencia =
                new DataSetpnrClientTableAdapters.TBOcorrenciaTableAdapter();
            adpOcorrencia.Connection.ConnectionString = pnrClient.Properties.Settings.Default.ptServer35ConnectionString;

            try
            {
                adpOcorrencia.Fill(TBOcorrencia);
                webS.Open();
                for (int i = 0; i <= TBOcorrencia.Rows.Count - 1; i++)
                {
                    webS.InformarOcorrencia(TBOcorrencia[i].DTOcorrencia, TBOcorrencia[i].DSOcorrencia,
                        TBOcorrencia[i].IDEmpresa, TBOcorrencia[i].IDSetor, "", "", "");
                }
                DeleteOcorrencia();
                webS.Close();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        //-------------------------------
        #endregion

        #region GetTabelas

        public string DSEMPRESA
        {
            get
            {
                return DSEmpresa;
            }
        }
        public string DSSETOR
        {
            get
            {
                return DSSetor;
            }
        }
        public void GetTBUsuario(DataSetpnrClient dsl)
        {
            DataSetpnrClientTableAdapters.TBUsuarioLocalTableAdapter adpUsuario =
                new DataSetpnrClientTableAdapters.TBUsuarioLocalTableAdapter();
            adpUsuario.Connection.ConnectionString = pnrClient.Properties.Settings.Default.ptServer35ConnectionString;
            adpUsuario.Fill(dsl.TBUsuarioLocal);
        }
        public void GetTBUsuarioNome(DataSetpnrClient dsl, int IDEmpresa, string Nome)
        {
            DataSetpnrClientTableAdapters.TBUsuarioLocalTableAdapter adpUsuario =
                new DataSetpnrClientTableAdapters.TBUsuarioLocalTableAdapter();
            adpUsuario.Connection.ConnectionString = pnrClient.Properties.Settings.Default.ptServer35ConnectionString;
            adpUsuario.FillNomeUsuario(dsl.TBUsuarioLocal, Nome);
        }
        public void GetTBUsuarioMatricula(DataSetpnrClient dsl, int IDEmpresa, string Matricula)
        {
            DataSetpnrClientTableAdapters.TBUsuarioLocalTableAdapter adpUsuario =
                new DataSetpnrClientTableAdapters.TBUsuarioLocalTableAdapter();
            adpUsuario.Connection.ConnectionString = pnrClient.Properties.Settings.Default.ptServer35ConnectionString;
            adpUsuario.FillMatricula(dsl.TBUsuarioLocal, Matricula);
        }
        public void GetTBUsuarioCPF(DataSetpnrClient dsl, int IDEmpresa, string CPF)
        {
            DataSetpnrClientTableAdapters.TBUsuarioLocalTableAdapter adpUsuario =
                new DataSetpnrClientTableAdapters.TBUsuarioLocalTableAdapter();
            adpUsuario.Connection.ConnectionString = pnrClient.Properties.Settings.Default.ptServer35ConnectionString;
            adpUsuario.FillCPF(dsl.TBUsuarioLocal, CPF.Trim());
        }
        public int GetIDEmpresa()
        {
            DataSetpnrClient dsL = new DataSetpnrClient();
            DataSetpnrClientTableAdapters.TBEmpresaTableAdapter adpEmpresa = new DataSetpnrClientTableAdapters.TBEmpresaTableAdapter();
            adpEmpresa.Connection.ConnectionString = pnrClient.Properties.Settings.Default.ptServer35ConnectionString;
            adpEmpresa.Fill(dsL.TBEmpresa);
            if (dsL.TBEmpresa.Rows.Count > 0)
            {
                DSEmpresa = dsL.TBEmpresa[0].DSEmpresa;
                return dsL.TBEmpresa[0].IDEmpresa;
            }
            else
                return 0;
        }
        public int GetIDSetor()
        {
            DataSetpnrClient dsL = new DataSetpnrClient();
            DataSetpnrClientTableAdapters.TBSetorTableAdapter adpSetor = new DataSetpnrClientTableAdapters.TBSetorTableAdapter();
            adpSetor.Connection.ConnectionString = pnrClient.Properties.Settings.Default.ptServer35ConnectionString;
            adpSetor.Fill(dsL.TBSetor);
            if (dsL.TBSetor.Rows.Count > 0)
            {
                DSSetor = dsL.TBSetor[0].DSSetor;
                return dsL.TBSetor[0].IDSetor;
            }
            else
                return 0;
        }
        //Buscar usuários com digitais cadastradas.
        public string GetUsuarioTemplate(DataSetpnrClient dsL, int IDEmpresa)
        {
            DataSetpnrClientTableAdapters.TBUsuarioLocalTableAdapter adpUsuarioLocal =
                new DataSetpnrClientTableAdapters.TBUsuarioLocalTableAdapter();
            adpUsuarioLocal.Connection.ConnectionString = pnrClient.Properties.Settings.Default.ptServer35ConnectionString;
            try
            {
                adpUsuarioLocal.FillTemplate(dsL.TBUsuarioLocal);
                if (dsL.TBUsuarioLocal.Rows.Count > 0)
                    return "Biometrias carregadas! Local";
                else
                    return "Ausência de banco biométrico local. Favor importar!";
            }
            catch (Exception ex)
            {
                return string.Format("Erro local: {0}", ex.Message.Trim());
            }


            //if(dsL.TBUsuarioLocal.Rows.Count == 0)
            //{
            //    try
            //    {
            //        ImportUsuarios(IDEmpresa);
            //    }
            //    catch
            //    {
            //        Exceptio
            //    }
            //}
        }

        public string GetUsuarioTemplateTeste(DataSetpnrClient dsL, int IDEmpresa, byte[] array, string conn)
        {
            SqlCeDataAdapter DA;
            DataTable TBCH = new DataTable();
            string teste = Convert.ToBase64String(array);
            conn = pnrClient.Properties.Settings.Default.ptServer35ConnectionString;
            DA = null;
            DA = new SqlCeDataAdapter("Select * from TBUsuarioLocal where Template1 = (@Template1)", conn); //Instância DataAdapter com o cod a executar e a conexão
            DA.SelectCommand.Parameters.Add("@Template1", array);
            DA.Fill(TBCH);

            if (TBCH.Rows.Count > 0)
            {
                return "achou";
            }
            else
                return "Não";
        }

        public void GetUsuarioTemplate(DataSetpnrClient dsL)
        {
            DataSetpnrClientTableAdapters.TBUsuarioLocalTableAdapter adpUsuarioLocal =
                new DataSetpnrClientTableAdapters.TBUsuarioLocalTableAdapter();
            adpUsuarioLocal.Connection.ConnectionString = pnrClient.Properties.Settings.Default.ptServer35ConnectionString;
            adpUsuarioLocal.FillTemplate(dsL.TBUsuarioLocal);
        }

        public byte[] templateComparativo(int IDVinculoUsuario)
        {
            DataSetpnrClientTableAdapters.TBUsuarioLocalTableAdapter adpUsuario =
                new DataSetpnrClientTableAdapters.TBUsuarioLocalTableAdapter();
            DataSetpnrClient ds = new DataSetpnrClient();
            adpUsuario.FillIDVinculoUsuario(ds.TBUsuarioLocal, IDVinculoUsuario);

            if (ds.TBUsuarioLocal.Rows.Count > 0)
                return ds.TBUsuarioLocal[0].Template1;
            else
                return null;
        }

        //AQUI, FAZ UM ULTIMO COMPARATIVO ANTES DE DEIXAR PROSSEGUIR COM O CÓDIGO DE FREQUÊNCIA.
        public string ConfirmaCPFFrequencia(int IDUsuario)
        {
            //para o dataset Tipado.
            DataSetpnrClientTableAdapters.TBUsuarioLocalTableAdapter adpLocal =
                new DataSetpnrClientTableAdapters.TBUsuarioLocalTableAdapter();
            adpLocal.Connection.ConnectionString = pnrClient.Properties.Settings.Default.ptServer35ConnectionString;
            //DataSetpnrClient.TBUsuarioLocalDataTable TBUsuarioLocal;


            SQLConn = new SqlCeConnection(pnrClient.Properties.Settings.Default.ptServer35ConnectionString);

            DA = null;
            DA = new SqlCeDataAdapter("SELECT * FROM TBUsuarioLocal WHERE IDUsuario = @IDUsuario", SQLConn);

            DA.SelectCommand.Parameters.Add("IDUsuario", IDUsuario);

            DataTable TBFL = null;
            TBFL = new DataTable();
            DA.Fill(TBFL);

            if (TBFL.Rows.Count > 0)
            {
                return TBFL.Rows[0]["CPF"].ToString();
            }
            else
                return "";
        }

        //29/08/2018

        public DataTable GetTBUsuarioSetorLocal()
        {
            //Objetos para Vefificação de registros
            SqlCeDataAdapter DA = new SqlCeDataAdapter();
            DataTable TBUsuarioLocalSetor;
            TBUsuarioLocalSetor = new DataTable();

            DA = new SqlCeDataAdapter("SELECT * FROM TBUSUARIOLOCALSETOR", pnrClient.Properties.Settings.Default.ptServer35ConnectionString);
            DA.Fill(TBUsuarioLocalSetor);

            if (TBUsuarioLocalSetor.Rows.Count > 0)
            {
                return TBUsuarioLocalSetor;
            }
            else
                return null;

        }

        public DataTable GetTBUsuarioSetorLocalHash()
        {
            //Objetos para Vefificação de registros
            SqlCeDataAdapter DA = new SqlCeDataAdapter();
            DataTable TBUsuarioLocalSetor;
            TBUsuarioLocalSetor = new DataTable();

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT DISTINCT A.IDUsuario,A.DSUsuario,A.TextHashCode,A.TextHashCode2,A.CadastraDigital,A.IDTPUsuario,A.DTCriacao,A.PrimeiroNome,A.SenhaDigital,A.Template1,A.Matricula,A.CPF,A.Template2,A.IDVinculoUsuario,A.HoraEntradaManha,A.HoraSaidaManha,A.HoraEntradaTarde,A.HoraSaidaTarde,A.TotalHoraDia,A.RegimePlantonista");
            sb.AppendLine("FROM TBUsuarioLocal A");
            sb.AppendLine("LEFT JOIN Log_RegistrosClint B ON(A.IDUsuario = B.IDUsuario)");
            sb.AppendLine("ORDER BY B.DTLocalRegistro DESC");
            DA = new SqlCeDataAdapter(sb.ToString(), pnrClient.Properties.Settings.Default.ptServer35ConnectionString);
            DA.Fill(TBUsuarioLocalSetor);

            if (TBUsuarioLocalSetor.Rows.Count > 0)
            {
                var teste = TBUsuarioLocalSetor.Rows[0];
                return TBUsuarioLocalSetor;
            }
            else
                return null;
        }

        public bool TabelaExistTBUL()
        {
            //Objetos para Vefificação de registros
            SqlCeDataAdapter DA = new SqlCeDataAdapter();
            DataTable TBUsuarioLocalSetor;
            TBUsuarioLocalSetor = new DataTable();

            DA = new SqlCeDataAdapter("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME LIKE '%TBUsuarioLocalSetor%'", pnrClient.Properties.Settings.Default.ptServer35ConnectionString);
            DA.Fill(TBUsuarioLocalSetor);

            if (TBUsuarioLocalSetor.Rows.Count > 0)
            {
                return true;
            }
            else
                return false;

        }

        public void CreateTBUsuarioLocalSetor(int IDsetor)
        {
            string cod;
            cod = "Create table TBUsuarioLocalSetor" +
                "(IDUsuario bigint," +
                "DSUsuario nvarchar(300)," +
                "CadastraDigital bit," +
                "IDTPUsuario int," +
                "DTCriacao datetime," +
                "PrimeiroNome nvarchar(100)," +
                "SenhaDigital nvarchar(1)," +
                "Template1 varbinary(8000)," +
                "Matricula nvarchar(7)," +
                "CPF nvarchar(11)," +
                "Template2 varbinary(8000)," +
                "IDVinculoUsuario bigint," +
                "HoraEntradaManha nvarchar(5)," +
                "HoraSaidaManha nvarchar(5)," +
                "HoraEntradaTarde nvarchar(5)," +
                "HoraSaidaTarde nvarchar(5)," +
                "TotalHoraDia int," +
                "RegimePlantonista bit)";
            SqlCeConnection SQLConn = new SqlCeConnection(pnrClient.Properties.Settings.Default.ptServer35ConnectionString);
            SqlCeCommand SQLComand = new SqlCeCommand(cod, SQLConn);

            try
            {
                SQLConn.Open();
                SQLComand.ExecuteNonQuery();
                SQLConn.Close();
                //Cria e depois delete e insere
                DeleteTBUsuarioLocalsetor();
                //InsertTBUsuarioLocalsetor(IDsetor);
                //Já lança os registros na tabela.
                //InsereDados();
            }
            catch (Exception ex)
            {
                ex.ToString();
                SQLConn.Close();
            }
        }

        //Deletando.
        protected bool DeleteTBUsuarioLocalsetor()
        {
            string cod;
            cod = "Delete from tbUsuarioLocalSetor";

            SqlCeConnection SQLConn = new SqlCeConnection(pnrClient.Properties.Settings.Default.ptServer35ConnectionString);
            SqlCeCommand SQLComand = new SqlCeCommand(cod, SQLConn);

            try
            {
                SQLConn.Open();
                SQLComand.ExecuteNonQuery();
                SQLConn.Close();
                return true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                SQLConn.Close();
                return false;
            }
        }

        //Inserindo dados na tabela
        protected void InsertTBUsuarioLocalsetor(int IDSetor)
        {
            string cod;
            cod = "Insert into TBUsuarioLocalSetor" +
                "(IDUsuario," +
                "DSUsuario," +
                "CadastraDigital," +
                "IDTPUsuario," +
                "DTCriacao," +
                "PrimeiroNome," +
                "SenhaDigital," +
                "Template1," +
                "Matricula," +
                "CPF," +
                "Template2," +
                "IDVinculoUsuario," +
                "HoraEntradaManha," +
                "HoraSaidaManha," +
                "HoraEntradaTarde," +
                "HoraSaidaTarde," +
                "TotalHoraDia," +
                "RegimePlantonista)" +
                "SELECT IDUsuario," +
                "DSUsuario," +
                "CadastraDigital," +
                "IDTPUsuario," +
                "DTCriacao," +
                "PrimeiroNome," +
                "SenhaDigital," +
                "Template1," +
                "Matricula," +
                "CPF," +
                "Template2," +
                "IDVinculoUsuario," +
                "HoraEntradaManha," +
                "HoraSaidaManha," +
                "HoraEntradaTarde," +
                "HoraSaidaTarde," +
                "TotalHoraDia," +
                "RegimePlantonista from TBusuarioLocal where IDSetor = @IDSetor";

            SqlCeConnection SQLConn = new SqlCeConnection(pnrClient.Properties.Settings.Default.ptServer35ConnectionString);
            SqlCeCommand SQLComand = new SqlCeCommand(cod, SQLConn);

            try
            {
                SQLConn.Open();
                SQLComand.Parameters.Add("IDSetor", IDSetor);
                SQLComand.ExecuteNonQuery();
                SQLConn.Close();
            }
            catch (Exception ex)
            {
                ex.ToString();
                SQLConn.Close();
            }
        }
        protected void InsertTBUsuarioLocalSetor(int IDUsuario, string DSUsaurio, bool CadastraDigital,
           int IDTPUsuario, string PrimeiroNome, string SenhaDigital, byte[] Template1, string Matricula, string CPF,
           byte[] Template2, int IDVinculoUsaurio, string HoraEntradaManha, string HoraSaidaManha, string HoraEntradaTarde, string HoraSaidaTarde,
           int TotalHoraDia, bool RegimePlantonista)
        {

            if (Template2 != null)
            {
                cod = "Insert into TBUsuarioLocalSetor" +
    "(IDUsuario," +
    "DSUsuario," +
    "CadastraDigital," +
    "IDTPUsuario," +
    "DTCriacao," +
    "PrimeiroNome," +
    "Template1," +
    "Matricula," +
    "CPF," +
    "Template2," +
    "IDVinculoUsuario," +
    "HoraEntradaManha," +
    "HoraSaidaManha," +
    "HoraEntradaTarde," +
    "HoraSaidaTarde," +
    "TotalHoraDia," +
    "RegimePlantonista)" +
    "Values(@IDUsuario," +
    "@DSUsuario," +
    "@CadastraDigital," +
    "@IDTPUsuario," +
    "@DTCriacao," +
    "@PrimeiroNome," +
    "@Template1," +
    "@Matricula," +
    "@CPF," +
    "@Template2," +
    "@IDVinculoUsuario," +
    "@HoraEntradaManha," +
    "@HoraSaidaManha," +
    "@HoraEntradaTarde," +
    "@HoraSaidaTarde," +
    "@TotalHoradia," +
    "@RegimePlantonista)";
            }
            else
            {
                cod = "Insert into TBUsuarioLocalSetor" +
"(IDUsuario," +
"DSUsuario," +
"CadastraDigital," +
"IDTPUsuario," +
"DTCriacao," +
"PrimeiroNome," +
"Template1," +
"Matricula," +
"CPF," +
"IDVinculoUsuario," +
"HoraEntradaManha," +
"HoraSaidaManha," +
"HoraEntradaTarde," +
"HoraSaidaTarde," +
"TotalHoraDia," +
"RegimePlantonista)" +
"Values(@IDUsuario," +
"@DSUsuario," +
"@CadastraDigital," +
"@IDTPUsuario," +
"@DTCriacao," +
"@PrimeiroNome," +
"@Template1," +
"@Matricula," +
"@CPF," +
"@IDVinculoUsuario," +
"@HoraEntradaManha," +
"@HoraSaidaManha," +
"@HoraEntradaTarde," +
"@HoraSaidaTarde," +
"@TotalHoradia," +
"@RegimePlantonista)";
            }


            SqlCeConnection SQLConn = new SqlCeConnection(pnrClient.Properties.Settings.Default.ptServer35ConnectionString);
            SqlCeCommand SQLComand = new SqlCeCommand(cod, SQLConn);

            try
            {
                SQLConn.Open();
                SQLComand.Parameters.Add("@IDUsuario", IDUsuario);
                SQLComand.Parameters.Add("@DSUsuario", DSUsaurio);
                SQLComand.Parameters.Add("@CadastraDigital", CadastraDigital);
                SQLComand.Parameters.Add("@IDTPUsuario", IDTPUsuario);
                SQLComand.Parameters.Add("@PrimeiroNome", PrimeiroNome);
                SQLComand.Parameters.Add("@SenhaDigital", SenhaDigital);
                SQLComand.Parameters.Add("@Template1", Template1);
                SQLComand.Parameters.Add("@Matricula", Matricula);
                SQLComand.Parameters.Add("@CPF", CPF);
                SQLComand.Parameters.Add("@Template2", Template2);
                SQLComand.Parameters.Add("@IDVinculoUsuario", IDVinculoUsaurio);
                SQLComand.Parameters.Add("@HoraEntradaManha", HoraEntradaManha);
                SQLComand.Parameters.Add("@HoraSaidaManha", HoraSaidaManha);
                SQLComand.Parameters.Add("@HoraEntradaTarde", HoraEntradaTarde);
                SQLComand.Parameters.Add("@HoraSaidaTarde", HoraSaidaTarde);
                SQLComand.Parameters.Add("@TotalHoraDia", TotalHoraDia);
                SQLComand.Parameters.Add("@RegimePlantonista", RegimePlantonista);
                SQLComand.Parameters.Add("@DTCriacao", DateTime.Now);


                SQLComand.ExecuteNonQuery();
                SQLConn.Close();
            }
            catch (Exception ex)
            {
                ex.ToString();
                SQLConn.Close();
            }


        }

        //Tabela de usuário local apenas com os dados dos setores
        #endregion


        public void InsertLogRegistrosClint(int IDUsuario, int IDEmpresa, int IDSetorBatida)
        {
            UtilLocalDB localDB = new UtilLocalDB();
            localDB.Insert("INSERT INTO Log_RegistrosClint (IDUsuario,IDEmpresa,IDSetorBatida,DTLocalRegistro) VALUES (" + IDUsuario + "," + IDEmpresa + "," + IDSetorBatida + ",GETDATE())");
        }


        public string GetTBBancoLocalHash()
        {
            SqlCeDataAdapter DA = new SqlCeDataAdapter();
            StringBuilder sb = new StringBuilder();
            DataTable TBUsuarioLocalSetor;
            TBUsuarioLocalSetor = new DataTable();
            if (!TabelaExistHash())
            {
                sb.AppendLine("SELECT HashLocal FROM TBHashLocal");
            }
            DA = new SqlCeDataAdapter(sb.ToString(), pnrClient.Properties.Settings.Default.ptServer35ConnectionString);
            DA.Fill(TBUsuarioLocalSetor);
            DA.Dispose();
            if (TBUsuarioLocalSetor.Rows.Count > 0)
            {
                var item = TBUsuarioLocalSetor.Rows[0];
                return item["HashLocal"].ToString(); ;
            }
            else
                return null;
        }


        public string GetTBLocalHash(string Hash)
        {
            //Objetos para Vefificação de registros
            SqlCeDataAdapter DA = new SqlCeDataAdapter();
            DataTable TBUsuarioLocalSetor;
            TBUsuarioLocalSetor = new DataTable();
            StringBuilder sb = new StringBuilder();
            if (!TabelaExistHash())
            {
                UtilLocalDB localDB = new UtilLocalDB();
                localDB.Insert("	CREATE TABLE TBHashLocal(HashLocal nchar(300))");
                UtilLocalDB localDB2 = new UtilLocalDB();
                localDB2.Insert("	INSERT INTO TBHashLocal(HashLocal) VALUES	('" + Hash + "')");
            }
            sb.AppendLine("SELECT HashLocal FROM TBHashLocal");

            DA = new SqlCeDataAdapter(sb.ToString(), pnrClient.Properties.Settings.Default.ptServer35ConnectionString);
            DA.Fill(TBUsuarioLocalSetor);
            DA.Dispose();
            if (TBUsuarioLocalSetor.Rows.Count > 0)
            {
                var item = TBUsuarioLocalSetor.Rows[0];
                return item["HashLocal"].ToString(); ;
            }
            else
                return null;
        }


        public bool TabelaExistHash()
        {
            //Objetos para Vefificação de registros
            SqlCeDataAdapter DA = new SqlCeDataAdapter();
            DataTable TBUsuarioLocalSetor;
            TBUsuarioLocalSetor = new DataTable();

            DA = new SqlCeDataAdapter("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME LIKE '%TBHashLocal%'", pnrClient.Properties.Settings.Default.ptServer35ConnectionString);
            DA.Fill(TBUsuarioLocalSetor);

            if (TBUsuarioLocalSetor.Rows.Count > 0)
            {
                return true;
            }
            else
                return false;

        }

        public void LimpaTabelaHash()
        {
            //Objetos para Vefificação de registros
            SqlCeDataAdapter DA = new SqlCeDataAdapter();
            DataTable TBUsuarioLocalSetor;
            TBUsuarioLocalSetor = new DataTable();

            DA = new SqlCeDataAdapter("SELECT COUNT(*) TotalRows, MAX(Sequencial) Sequencial FROM Log_RegistrosClint", pnrClient.Properties.Settings.Default.ptServer35ConnectionString);
            DA.Fill(TBUsuarioLocalSetor);


            if (TBUsuarioLocalSetor.Rows.Count > 0)
            {
                var item = TBUsuarioLocalSetor.Rows[0];
                if (int.Parse(item["TotalRows"].ToString()) > 10000)
                {
                    UtilLocalDB localDB = new UtilLocalDB();
                    localDB.Insert("DELETE FROM Log_RegistrosClint WHERE Sequencial < (" + item["Sequencial"].ToString() + " - 10000)");
                }
            }

        }

        public void AtualizaScriptBanco()
        {
            UtilLocalDB localDB = new UtilLocalDB();
            DataTable table = localDB.Load("SELECT * FROM INFORMATION_SCHEMA.COLUMNS A WHERE TABLE_NAME LIKE '%Log_RegistrosClint%' AND COLUMN_NAME LIKE '%Sequencial%'");
            if (table.Rows.Count == 0)
            {
                UtilLocalDB localDB2 = new UtilLocalDB();
                localDB2.Insert("ALTER TABLE Log_RegistrosClint ADD Sequencial bigint IDENTITY (1,1)  PRIMARY KEY NOT NULL");
            }
        }


        //LOG DE CONEXÕES
        public List<pontonarede.TesteConexaoModel> InsertLogConexoes(pontonarede.TesteConexaoModel conexao)
        {
            List<pontonarede.TesteConexaoModel> list;
            try
            {
                UtilLocalDB localDB = new UtilLocalDB();
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("INSERT INTO TBLogConexao (RedeLocal,RedeLocalMensagem,Internet, InternetMensagem, WSInterno, WSInternoMensagem, WSInternoTempo, WSExterno, WSExternoMensagem, WSExternoTempo, Leitor, LeitorMensagem, BancoLocal, BancoLocalMensagem, DataLog,EnviadoWS) VALUES ");
                sb.AppendLine("(" + conexao.ConexaoRedeLocal + ",'" + Util.TratarString(conexao.ConexaoRedeLocalMensagem) + "'," +
                    conexao.ConexaoInternet + ", '" + Util.TratarString(conexao.ConexaoInternetMensagem) + "', " +
                    conexao.ConexaoWSInterno + ", '" + Util.TratarString(conexao.ConexaoWSInternoMensagem) + "', '" + Util.TratarString(conexao.ConexaoWSInternoTempo) + "', " +
                    conexao.ConexaoWSExterno + ", '" + Util.TratarString(conexao.ConexaoWSExternoMensagem) + "', '" + Util.TratarString(conexao.ConexaoWSExternoTempo) + "', " +
                    conexao.ConexaoLeitor + ",'" + conexao.ConexaoLeitorMensagem + "', " + conexao.ConexaoBancoLocal + " ,'" + Util.TratarString(conexao.ConexaoBancoLocalMensagem) + "', GETDATE(),0 )");
                localDB.Insert(sb.ToString());
            }
            catch { }
            


            list = GetListLogConexoes();


            return list;
        }

        private List<pontonarede.TesteConexaoModel> GetListLogConexoes()
        {
            
            List<pontonarede.TesteConexaoModel> list = new List<pontonarede.TesteConexaoModel>();
            //GET LOGS TBLogConexao
            SqlCeDataAdapter DA = new SqlCeDataAdapter();
            DataTable TBUsuarioLocalSetor;
            TBUsuarioLocalSetor = new DataTable();
            try
            {
                DA = new SqlCeDataAdapter("SELECT DISTINCT RedeLocal,RedeLocalMensagem,Internet, InternetMensagem, WSInterno, WSInternoMensagem, WSInternoTempo, WSExterno, WSExternoMensagem, WSExternoTempo, Leitor, LeitorMensagem, BancoLocal, BancoLocalMensagem, DataLog FROM TBLogConexao WHERE EnviadoWS = 0", pnrClient.Properties.Settings.Default.ptServer35ConnectionString);
                DA.Fill(TBUsuarioLocalSetor);
            }
            catch { }
            

            int i = 0;

            while (TBUsuarioLocalSetor.Rows.Count > i)
            {
                var item = TBUsuarioLocalSetor.Rows[i];
                var teste = item["RedeLocal"].ToString();
                list.Add(new pontonarede.TesteConexaoModel()
                {
                    ConexaoRedeLocal = int.Parse(item["RedeLocal"].ToString()),
                    ConexaoRedeLocalMensagem = Util.TratarString(item["RedeLocalMensagem"].ToString()),
                    ConexaoInternet = int.Parse(item["Internet"].ToString()),
                    ConexaoInternetMensagem = Util.TratarString(item["InternetMensagem"].ToString()),
                    ConexaoWSInterno = int.Parse(item["WSInterno"].ToString()),
                    ConexaoWSInternoMensagem = Util.TratarString(item["WSInternoMensagem"].ToString()),
                    ConexaoWSInternoTempo = Util.TratarString(item["WSInternoTempo"].ToString()),
                    ConexaoWSExterno = int.Parse(item["WSExterno"].ToString()),
                    ConexaoWSExternoMensagem = Util.TratarString(item["WSExternoMensagem"].ToString()),
                    ConexaoWSExternoTempo = Util.TratarString(item["WSExternoTempo"].ToString()),
                    ConexaoLeitor = int.Parse(item["Leitor"].ToString()),
                    ConexaoLeitorMensagem = Util.TratarString(item["LeitorMensagem"].ToString()),
                    ConexaoBancoLocal = int.Parse(item["BancoLocal"].ToString()),
                    ConexaoBancoLocalMensagem = Util.TratarString(item["BancoLocalMensagem"].ToString()),
                    DataLog = Util.TratarString(DateTime.Parse(item["DataLog"].ToString()).ToString("yyyy-MM-dd HH:mm:ss.fff")),
                });
                i++;
            }
            return list;
        }


        public void AtualizaLogsConexoes()
        {
            UtilLocalDB localDB = new UtilLocalDB();
            StringBuilder sb = new StringBuilder();
            localDB.Insert("UPDATE TBLogConexao SET EnviadoWS = 1 WHERE  EnviadoWS = 0");
        }

        public void VerificaTBLogConexao()
        {
            //Objetos para Vefificação de registros
            SqlCeDataAdapter DA = new SqlCeDataAdapter();
            DataTable TBUsuarioLocalSetor;
            TBUsuarioLocalSetor = new DataTable();

            DA = new SqlCeDataAdapter("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME LIKE '%TBLogConexao%'", pnrClient.Properties.Settings.Default.ptServer35ConnectionString);
            DA.Fill(TBUsuarioLocalSetor);

            if (TBUsuarioLocalSetor.Rows.Count == 0)
            {
                //Objetos para Vefificação de registros
                //SqlCeDataAdapter DA2 = new SqlCeDataAdapter();
                //DataTable TBUsuarioLocalSetor;
                //TBUsuarioLocalSetor = new DataTable();
                UtilLocalDB localDB = new UtilLocalDB();
                StringBuilder sb = new StringBuilder();

                sb.AppendLine("CREATE TABLE TBLogConexao");
                sb.AppendLine("(");
                sb.AppendLine("    RedeLocal int,");
                sb.AppendLine("    RedeLocalMensagem nchar(500),");
                sb.AppendLine("    Internet int,");
                sb.AppendLine("    InternetMensagem nchar(500),");
                sb.AppendLine("    WSInterno int,");
                sb.AppendLine("    WSInternoMensagem nchar(500),");
                sb.AppendLine("    WSInternoTempo nchar(20),");
                sb.AppendLine("    WSExterno int,");
                sb.AppendLine("    WSExternoMensagem nchar(500),");
                sb.AppendLine("    WSExternoTempo nchar(20),");
                sb.AppendLine("    Leitor int,");
                sb.AppendLine("    LeitorMensagem nchar(500),");
                sb.AppendLine("    BancoLocal int,");
                sb.AppendLine("    BancoLocalMensagem nchar(500),");
                sb.AppendLine("    DataLog Datetime,");
                sb.AppendLine("    EnviadoWS int");
                sb.AppendLine(")");
                localDB.Insert(sb.ToString());
            }
        }


        public DataTable GetListaLogs()
        {
            //Objetos para Vefificação de registros
            SqlCeDataAdapter DA = new SqlCeDataAdapter();
            DataTable TBUsuarioLocalSetor;
            TBUsuarioLocalSetor = new DataTable();

            DA = new SqlCeDataAdapter("SELECT RedeLocalMensagem RedeLocal,InternetMensagem Internet,WSInternoMensagem WSInfoVia,WSExternoMensagem WSExterno,LeitorMensagem Leitor,BancoLocalMensagem BancoLocal,DataLog FROM TBLogConexao ORDER BY DataLog DESC; ", pnrClient.Properties.Settings.Default.ptServer35ConnectionString);
            DA.Fill(TBUsuarioLocalSetor);


            int i = 0;

            while (TBUsuarioLocalSetor.Rows.Count > i)
            {
                var item = TBUsuarioLocalSetor.Rows[i];
                item["RedeLocal"] = Util.TratarString(item["RedeLocal"].ToString());
                item["Internet"] = Util.TratarString(item["Internet"].ToString());
                item["WSInfoVia"] = Util.TratarString(item["WSInfoVia"].ToString());
                item["WSExterno"] = Util.TratarString(item["WSExterno"].ToString());
                item["Leitor"] = Util.TratarString(item["Leitor"].ToString());
                item["BancoLocal"] = Util.TratarString(item["BancoLocal"].ToString());
                item["DataLog"] = Util.TratarString(item["DataLog"].ToString());
                i++;
            }
            return TBUsuarioLocalSetor;
        }
    }




}
