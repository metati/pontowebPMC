using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace MetodosPontoFrequencia
{
    
    public class Cadastro
    {
        protected int idsetor = 0;
        protected int idempresa = 0;
        protected int IDTipoFerias;
        protected string msg = "";
        protected bool achou;
        protected string[] NovosSetores;
        protected string[] SetoresRemovidos;
        protected LogOperacao log = new LogOperacao();
        protected DataSetPontoFrequenciaTableAdapters.TBSetorTableAdapter adpSetor = new DataSetPontoFrequenciaTableAdapters.TBSetorTableAdapter();
        protected DataSetPontoFrequenciaTableAdapters.TBUsuarioTableAdapter adpUsuario = new DataSetPontoFrequenciaTableAdapters.TBUsuarioTableAdapter();
        protected DataSetPontoFrequenciaTableAdapters.TBMotivoFaltaTableAdapter adpMotivoFalta = new DataSetPontoFrequenciaTableAdapters.TBMotivoFaltaTableAdapter();
        protected DataSetPontoFrequenciaTableAdapters.TBFeriadoPontoFacultativoTableAdapter adpFeriadoPontoFacultativo = new DataSetPontoFrequenciaTableAdapters.TBFeriadoPontoFacultativoTableAdapter();
        protected DataSetPontoFrequenciaTableAdapters.TBFeriasTableAdapter adpFerias = new DataSetPontoFrequenciaTableAdapters.TBFeriasTableAdapter();
        protected DataSetPontoFrequenciaTableAdapters.TBCargoTableAdapter adpCargo = new DataSetPontoFrequenciaTableAdapters.TBCargoTableAdapter();
        protected DataSetPontoFrequenciaTableAdapters.TBInformacaoDiariaTableAdapter adpInformacaoDiaria = new DataSetPontoFrequenciaTableAdapters.TBInformacaoDiariaTableAdapter();
        protected DataSetPontoFrequenciaTableAdapters.TBEntidadeTableAdapter adpEntidade = new DataSetPontoFrequenciaTableAdapters.TBEntidadeTableAdapter();

        //Variaveis para a integração com o e-turmalina
        private string HoraEntradaManha, HoraSaidaManha, HoraEntradaTarde, HoraSaidaTarde, Nome, matricula, situacao;
        private int TotalHorasDia, TotalHoraSemana, cont, IDCargo;


        #region INTEGRAÇÃO e-turmalina HORAS/MINUTOS
        public DataSet HorasMesAno(string Usuario, string Senha, int IDEmpresaOrgao_eTurmalina, int Mes, int Ano)
        {
            //NÃO ESQUECER DE COLOCAR UMA CONSTRAIN NO CAMPO ETURMALINA NA TABELA DE EMPRESA PARA 
            //SER ÚNICO
            DataSet dsXML = new DataSet();
            DataSetPontoFrequencia ds;
            if (!PermiteAcesso(Usuario, Senha))
                return dsXML;

            dsXML.Tables.Add("Horas");
            dsXML.Tables["Horas"].Columns.Add("Matricula");
            dsXML.Tables["Horas"].Columns.Add("CodVerba");
            dsXML.Tables["Horas"].Columns.Add("Data");
            dsXML.Tables["Horas"].Columns.Add("Situacao");

            DataSetPontoFrequenciaTableAdapters.vWHorasDiaTableAdapter adpHorasDia =
                new DataSetPontoFrequenciaTableAdapters.vWHorasDiaTableAdapter();

            DataSetPontoFrequenciaTableAdapters.TBEmpresaTableAdapter adpEmpresa = new DataSetPontoFrequenciaTableAdapters.TBEmpresaTableAdapter();
            ds = new DataSetPontoFrequencia();
            ds.EnforceConstraints = false;
            if (IDEmpresaOrgao_eTurmalina != 0)
            {
                adpEmpresa.FillIDEturmalina(ds.TBEmpresa, IDEmpresaOrgao_eTurmalina);
                adpHorasDia.FillEmpresaMesAno(ds.vWHorasDia, ds.TBEmpresa[0].IDEmpresa, Mes, Ano);
            }
            else
                adpHorasDia.FillMesAno(ds.vWHorasDia, Mes, Ano);

            for (int i = 0; i <= ds.vWHorasDia.Rows.Count - 1; i++)
            {
                //novo registro
                DataRow newRow = dsXML.Tables["Horas"].NewRow();
                if (!ds.vWHorasDia[i].IsmatriculaNull())
                    matricula = ds.vWHorasDia[i].matricula.ToString();
                else
                    matricula = string.Empty;

                newRow["Matricula"] = matricula;
                newRow["CodVerba"] = "";
                newRow["Data"] = ds.vWHorasDia[i].DataFrequencia.ToString();

                if (ds.vWHorasDia[i].HorasDia == "00:00:00")
                    situacao = "FALTA";
                else
                    situacao = ds.vWHorasDia[i].HorasDia.ToString();
                newRow["Situacao"] = situacao;
                dsXML.Tables["Horas"].Rows.Add(newRow);
            }

            return dsXML;
        }

        public DataSetPontoFrequencia HorasMesAno1(string Usuario, string Senha, int IDEmpresaOrgao_eTurmalina, int Mes, int Ano, string FiltroSituacao)
        {
            //NÃO ESQUECER DE COLOCAR UMA CONSTRAIN NO CAMPO ETURMALINA NA TABELA DE EMPRESA PARA 
            //SER ÚNICO

            //15/06/2018 - Log de operações para testes do eTurmalina
            //eTurmalinaIntegracao

            try
            {
                LogOperacao log = new LogOperacao();
                log.RegistraLog(010101, DateTime.Now, "ImportHoras", "Import Horas", string.Format("Período: {0}/{1} Situacao: {2}", Mes, Ano, situacao), "", 36);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            DataSetPontoFrequencia ds;
            if (!PermiteAcesso(Usuario, Senha))
                return null;

            DataSetPontoFrequenciaTableAdapters.vWHorasDiaTableAdapter adpHorasDia =
                new DataSetPontoFrequenciaTableAdapters.vWHorasDiaTableAdapter();

            DataSetPontoFrequenciaTableAdapters.TBEmpresaTableAdapter adpEmpresa = new DataSetPontoFrequenciaTableAdapters.TBEmpresaTableAdapter();
            ds = new DataSetPontoFrequencia();
            ds.EnforceConstraints = false;
            if (IDEmpresaOrgao_eTurmalina != 0)
            {
                adpEmpresa.FillIDEturmalina(ds.TBEmpresa, IDEmpresaOrgao_eTurmalina);
                switch (FiltroSituacao)
                {
                    case "T":
                        adpHorasDia.FillEmpresaMesAno(ds.vWHorasDia, ds.TBEmpresa[0].IDEmpresa, Mes, Ano);
                        break;
                    case "H":
                        adpHorasDia.FillHorasIDEmpresa(ds.vWHorasDia, Mes, Ano, 2, ds.TBEmpresa[0].IDEmpresa);
                        break;
                    case "F":
                        adpHorasDia.FillFaltaIDEmpresa(ds.vWHorasDia, Mes, Ano, 3, ds.TBEmpresa[0].IDEmpresa);
                        break;
                }
            }
            else
            {
                switch (FiltroSituacao)
                {
                    case "T":
                        adpHorasDia.FillMesAno(ds.vWHorasDia, Mes, Ano);
                        break;
                    case "H":
                        adpHorasDia.FillHorasTodos(ds.vWHorasDia, Mes, Ano, 2);
                        break;
                    case "F":
                        //adpHorasDia.FillFaltaTodos(ds.vWHorasDia, Mes, Ano, 3);
                        break;
                }

            }

            return ds;
        }
        #endregion

        #region Verifica Usuario e Senha - INTEGRAÇÃO e-Turmalina
        public bool PermiteAcesso(string Usuario, string Senha)
        {
            if (Usuario == "eTurmalinaIntegracao" && Senha == "e@Turmalina2pontonarede")
                return true;
            else
                return false;
        }
        #endregion

        #region ponto na rede Client

        public void RegistroLocalFalha(int IDFrequenciaLocal, DateTime DTFrequenciaLocal, int IDusuarioLocal, string RetornoMSG, int IDEmpresa, int IDSetor)
        {
            DataSetPontoFrequenciaTableAdapters.TBFrequenciaLocalTableAdapter adpFreqLocal = new DataSetPontoFrequenciaTableAdapters.TBFrequenciaLocalTableAdapter();

            try
            {
                adpFreqLocal.Insert(IDFrequenciaLocal, DTFrequenciaLocal, IDusuarioLocal, RetornoMSG, IDEmpresa, IDSetor, IDFrequenciaLocal.ToString() + IDEmpresa.ToString() + IDSetor.ToString());
            }
            catch
            {

            }
        }


        public void InsereNovoClient(string SerialHD, string SerialProcessador, string MACRede, string IPLocal, string NomeComputador,
        string SistemaOperacional, DateTime DTultimaConexao, int IDEmpresa, int IDSetor, string VersaoClient, string MemoriaTotal,
        string EspacoLivreHD, string CapacidadeHD, string ArquiteturaMaquina, string Processador, string Chave)
        {
            DataSetPontoFrequenciaTableAdapters.TBClientTableAdapter adpClient = new DataSetPontoFrequenciaTableAdapters.TBClientTableAdapter();
            try
            {
                adpClient.Insert(Chave, SerialHD, SerialProcessador, MACRede, MemoriaTotal, CapacidadeHD, EspacoLivreHD, Processador, ArquiteturaMaquina, IPLocal,
                    NomeComputador, SistemaOperacional, DTultimaConexao, IDEmpresa, IDSetor, VersaoClient);
            }
            catch (Exception ex)
            {
                ex.ToString();
                AtualizaUltimaExecucao(DTultimaConexao, IPLocal, NomeComputador, Chave, VersaoClient, IDEmpresa, IDSetor, EspacoLivreHD);
            }
        }


        public void InsereNovoClientHash(string SerialHD, string SerialProcessador, string MACRede, string IPLocal, string NomeComputador,
string SistemaOperacional, DateTime DTultimaConexao, int IDEmpresa, int IDSetor, string VersaoClient, string MemoriaTotal,
string EspacoLivreHD, string CapacidadeHD, string ArquiteturaMaquina, string Processador, string Chave, string HashMaquina)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("IF((SELECT COUNT(*) FROM TBClient WHERE Chave = '" + Chave + "') = 0)");
            sb.AppendLine("BEGIN");
            sb.AppendLine("INSERT INTO dbo.TBClient");
            sb.AppendLine("(Chave, SerialHD, SerialProcessador, MACRede, TotalMemoria, CapacidadeHD, EspacoLivreHD, NomeProcessador, Arquitetura, IPLocal, NomeComputador, SistemaOperacional, DTUltimaExecucao, IDEmpresa, IDSetor, VersaoClient, HashMaquina)");
            sb.AppendLine("VALUES");
            sb.AppendLine("('" + Chave + "', '" + SerialHD + "', '" + SerialProcessador + "', '" + MACRede + "', '" + MemoriaTotal + "', '" + CapacidadeHD + "', '" + EspacoLivreHD + "','" + Processador + "' ,'" + ArquiteturaMaquina + "' , " +
                "'" + IPLocal + "' , '" + NomeComputador + "' , '" + SistemaOperacional + "', '" + DTultimaConexao.ToString("yyyy-MM-dd HH:mm:ss.fff") + "', " + IDEmpresa + ", " + IDSetor + ", '" + VersaoClient + "', '" + Util.TratarString(HashMaquina) + "')");
            sb.AppendLine("END");
            sb.AppendLine("ELSE");
            sb.AppendLine("BEGIN");
            sb.AppendLine("UPDATE dbo.TBClient");
            sb.AppendLine("SET DTUltimaExecucao = '" + DTultimaConexao.ToString("yyyy-MM-dd HH:mm:ss.fff") + "',");
            sb.AppendLine("SerialHD = '" + SerialHD + "',");
            sb.AppendLine("SerialProcessador = '" + SerialProcessador + "',");
            sb.AppendLine("MACRede = '" + MACRede + "',");
            sb.AppendLine("TotalMemoria = '" + MemoriaTotal + "',");
            sb.AppendLine("CapacidadeHD = '" + CapacidadeHD + "',");
            sb.AppendLine("EspacoLivreHD = '" + EspacoLivreHD + "',");
            sb.AppendLine("NomeProcessador = '" + Processador + "',");
            sb.AppendLine("Arquitetura = '" + ArquiteturaMaquina + "',");
            sb.AppendLine("IPLocal = '" + IPLocal + "',");
            sb.AppendLine("NomeComputador = '" + NomeComputador + "',");
            sb.AppendLine("SistemaOperacional = '" + SistemaOperacional + "',");
            sb.AppendLine("IDEmpresa = " + IDEmpresa + ",");
            sb.AppendLine("IDSetor = " + IDSetor + ",");
            sb.AppendLine("VersaoClient = '" + VersaoClient + "',");
            sb.AppendLine("HashMaquina = '" + Util.TratarString(HashMaquina) + "'");
            sb.AppendLine("WHERE Chave = '" + Chave + "'");
            sb.AppendLine("END");
            try
            {
                Util.ExecuteNonQuery(sb.ToString());
            }
            catch { }
        }


        public string GetHashMaquina(string Chave)
        {
            string Hash = Util.getScalar("SELECT HashMaquina FROM TBClient WHERE Chave = '" + Chave + "'");
            return Hash;
        }

        public void AtualizaUltimaExecucao(DateTime DTUltimaExecucao, string IPLocal, string NomeMaquina, string Chave, string versao, int IDEmpresa, int IDSetor, string EspacoLivreHD)
        {
            DataSetPontoFrequenciaTableAdapters.TBClientTableAdapter adpClient = new DataSetPontoFrequenciaTableAdapters.TBClientTableAdapter();
            try
            {
                adpClient.UpdateUltimaExecucao(DTUltimaExecucao, IPLocal, NomeMaquina, versao, IDEmpresa, IDSetor, EspacoLivreHD, Chave);
            }
            catch
            {
            }
        }

        #endregion

        #region Dias Ano
        public string DiasAnoS(DataSetPontoFrequencia.TBDiasAnoDataTable TBDiasAno)
        {
            DataSetPontoFrequenciaTableAdapters.TBDiasAnoTableAdapter adpDiasAno = new DataSetPontoFrequenciaTableAdapters.TBDiasAnoTableAdapter();
            DataSetPontoFrequenciaTableAdapters.TBEmpresaTableAdapter adpEmpresa = new DataSetPontoFrequenciaTableAdapters.TBEmpresaTableAdapter();
            DataSetPontoFrequencia.TBEmpresaDataTable TBEmpresa = new DataSetPontoFrequencia.TBEmpresaDataTable();

            adpEmpresa.FillNaoSAD(TBEmpresa);

            if (TBEmpresa.Rows.Count > 0)
            {
                try
                {
                    for (int i = 0; (i <= TBEmpresa.Rows.Count - 1); i++)
                    {
                        for (int y = 0; (y <= TBDiasAno.Rows.Count - 1); y++)
                        {
                            adpDiasAno.UpdateDiasAno(TBDiasAno[y].DTDiasAno, TBDiasAno[y].FeriadoPontoFacultativo, TBDiasAno[y].OBS,
                                TBEmpresa[i].IDEmpresa, TBDiasAno[y].DTDiasAno);
                        }
                    }

                    msg = "Rotina executada com sucesso";
                }
                catch (Exception ex)
                {
                    msg = "Houve falha na rotina. Tente novamente.";
                }
            }

            return msg;

        }

        #endregion

        #region Gestor Setor

        protected void ManutencaoGestorSetor(int IDUsuario, int IDTPUsuario, int IDEmpresa, string[] Setores, string Operacao)
        {
            //Prevendo se o usuário Foi removido da gestão
            DataSetPontoFrequenciaTableAdapters.TBGestorSetorTableAdapter adpGestorSetor = new DataSetPontoFrequenciaTableAdapters.TBGestorSetorTableAdapter();
            DataSetPontoFrequencia.TBGestorSetorDataTable TBGestorSetor = new DataSetPontoFrequencia.TBGestorSetorDataTable();
            adpGestorSetor.FillIDUsuario(TBGestorSetor, IDUsuario);


            //Removido da gestão
            if (TBGestorSetor.Rows.Count > 0 && (IDTPUsuario != 3 && IDTPUsuario != 9))
            {
                adpGestorSetor.DeleteIDUsuario(IDUsuario);
            }

            //Incluído na gestão
            if (TBGestorSetor.Rows.Count == 0 && (IDTPUsuario == 3 || IDTPUsuario == 9) && Operacao == "Inclusao")
            {
                for (int i = 0; i <= (Setores.Length - 1); i++)
                {
                    adpGestorSetor.Insert(IDEmpresa, Convert.ToInt32(Setores[i]), IDUsuario);
                }
            }
            //Altaração de gestão - Incluindo novos setores para gestão
            if ((IDTPUsuario == 3 || IDTPUsuario == 9) && Operacao == "Alteracao")
            {
                NovosSetores = new string[Setores.Length];
                for (int Y = 0; Y <= (Setores.Length - 1); Y++)
                {
                    if (TBGestorSetor.Rows.Count > 0)
                    {
                        achou = false;
                        for (int i = 0; i <= (TBGestorSetor.Rows.Count - 1); i++)
                        {
                            if (TBGestorSetor[i].IDSetor.ToString() == Setores[Y])
                            {
                                achou = true;
                                NovosSetores[Y] = "0"; //Se Zero, não acrescentar no insert.
                                break;
                            }

                            if (!achou)
                                NovosSetores[Y] = Setores[Y];
                        }
                    }
                    else
                    {
                        for (int i = 0; i <= (Setores.Length - 1); i++)
                        {
                            NovosSetores[Y] = Setores[Y];
                        }
                    }
                }

                //Altaração de gestão - Removendo setores que não estejam na lista.
                SetoresRemovidos = new string[TBGestorSetor.Rows.Count];
                for (int Y = 0; Y <= (TBGestorSetor.Rows.Count - 1); Y++)
                {
                    achou = false;
                    for (int i = 0; i <= (Setores.Length - 1); i++)
                    {
                        if (TBGestorSetor[Y].IDSetor.ToString() == Setores[i])
                        {
                            achou = true;
                            SetoresRemovidos[Y] = "0"; //Se Zero, não acrescentar no insert.
                            break;
                        }

                        if (!achou)
                            SetoresRemovidos[Y] = TBGestorSetor[Y].IDGestorSetor.ToString();
                    }
                }
            }
            if (Operacao == "Alteracao" && (IDTPUsuario == 3 || IDTPUsuario == 9))
            {
                //Inserindo novos Setores ---
                int TotSetor = 0;
                while (TotSetor <= (NovosSetores.Length - 1))
                {
                    if (NovosSetores[TotSetor] != "0")
                        adpGestorSetor.Insert(IDEmpresa, Convert.ToInt32(NovosSetores[TotSetor]), IDUsuario);
                    TotSetor++;
                }
                //Retirando setores removidos
                int TotSetorRemovido = 0;

                while (TotSetorRemovido <= (SetoresRemovidos.Length - 1))
                {
                    if (SetoresRemovidos[TotSetorRemovido] != "0")
                        adpGestorSetor.DeleteIDGEstorSetor(Convert.ToInt32(SetoresRemovidos[TotSetorRemovido]));
                    TotSetorRemovido++;
                }
            }

        }

        #endregion

        #region Vínculo Usuario

        public string CadastraVinculoUsuario2(int IDUsuario, int IDTPUsuario, int IDEmpresa, int IDRegimeHora, int idsetor,
    DateTime dtinicio, int idcargo, int identidade, bool cadastraDigital, 
    bool PainelDashboards, string HoraEntradaManha, string HoraSaidaManha, 
    string HoraEntradaTarde, string HoraSaidaTarde, int IDUsuarioOperador, int IDstatus, 
    string Matricula, bool DescontoJornada, bool Isencao)
        {
            DataSetPontoFrequenciaTableAdapters.TBVinculoUsuarioTableAdapter adpVinculo = new DataSetPontoFrequenciaTableAdapters.TBVinculoUsuarioTableAdapter();
            try
            {
                DataSetPontoFrequencia.TBVinculoUsuarioDataTable TBVinculoUsuario =
                    new DataSetPontoFrequencia.TBVinculoUsuarioDataTable();

                adpVinculo.FillMatriculaEmpresa(TBVinculoUsuario, Matricula, IDEmpresa, IDUsuario);

                if (TBVinculoUsuario.Rows.Count > 0)
                    return "Vínculo Já existente para a Matricula e a Lotação informada.";
                adpVinculo.InsertNew(IDEmpresa, IDUsuario, IDRegimeHora, IDTPUsuario, IDstatus, 
                    idsetor, dtinicio, idcargo, identidade, cadastraDigital, PainelDashboards, 
                    HoraSaidaTarde, HoraSaidaManha, HoraEntradaTarde, HoraEntradaManha, Matricula,DescontoJornada,Isencao);
                msg = "Vínculo incluído com sucesso.";
            }
            catch (Exception ex)
            {
                msg = "Houve falha na tentativa de cadastro. Tente Novamente!";
                ex.ToString();
            }

            //FAZER APÓS O LOG DA OPERAÇÃO
            log.RegistraLog(IDUsuarioOperador, System.DateTime.Now, adpVinculo.Adapter.InsertCommand.ToString(), "Inclusão de Vínculo de Usuário", string.Format("Nome: {0} Tipo: {1} Setor: {2}, Empresa: {3}", IDUsuario, IDTPUsuario, idsetor, IDEmpresa), "", IDEmpresa);

            return msg;

        }

        public string CadastraVinculoUsuario(int IDUsuario, int IDTPUsuario, int IDEmpresa, int IDRegimeHora, int idsetor,
            DateTime dtinicio, int idcargo, int identidade, 
            bool cadastraDigital, bool PainelDashboards, string HoraEntradaManha, 
            string HoraSaidaManha, string HoraEntradaTarde, string HoraSaidaTarde, 
            int IDUsuarioOperador, string Matricula, bool DescontoTotalJornada,bool Isento)
        {
            DataSetPontoFrequenciaTableAdapters.TBVinculoUsuarioTableAdapter adpVinculo = new DataSetPontoFrequenciaTableAdapters.TBVinculoUsuarioTableAdapter();
            try
            {
                DataSetPontoFrequencia.TBVinculoUsuarioDataTable TBVinculoUsuario =
                    new DataSetPontoFrequencia.TBVinculoUsuarioDataTable();
                if (Matricula != string.Empty)
                {
                    adpVinculo.FillMatriculaEmpresa(TBVinculoUsuario, Matricula, IDEmpresa, IDUsuario);

                    if (TBVinculoUsuario.Rows.Count > 0)
                        return "Vínculo Já existente para a Matricula e a Lotação informada.";

                }
                else
                {

                }
                PreencheTabela PT = new PreencheTabela();

                //16/02/2018 INCLUSÃO RETIRADA PARA ATENDER A PREFEITURA PMC - DUPLO VÍNCULO COM O MESMO LUGAR

                //Se retornar um valor que não seja 0. O usuário já possui um vínculo naquele determinado órgão/Setor.
                //if (PT.RetornaIDVinculoUsuario(IDEmpresa, idsetor, IDUsuario) == 0)
                //{

                //06/08/2018
                //Pesquisa se já tem alguma biomtria para o vínculo em questão, se houver copia e cadatra a biometria
                //no novo vínculo.

                //09/08/2018 - NOVA MUDANÇA, SE JÁ EXISTE O VÍNCULO, FAZ A TROCA DE SECRETARIA/SETOR.

                adpVinculo.FillMatricula_IDUsuario(TBVinculoUsuario, Matricula, IDUsuario); //IDUsuario

                if (TBVinculoUsuario.Rows.Count > 0)
                {
                    if (adpVinculo.UpdateNew(IDEmpresa, IDUsuario, IDRegimeHora, IDTPUsuario, 1, idsetor,
                        idcargo, identidade, cadastraDigital, PainelDashboards, HoraSaidaTarde, HoraSaidaManha,
                        HoraEntradaTarde, HoraEntradaManha, Matricula,DescontoTotalJornada,Isento,
                        TBVinculoUsuario[0].IDVinculoUsuario) != 0)
                    {
                        DataSetPontoFrequenciaTableAdapters.TBMovVinculoUsuarioTableAdapter adpMov =
                            new DataSetPontoFrequenciaTableAdapters.TBMovVinculoUsuarioTableAdapter();
                        adpMov.Insert(TBVinculoUsuario[0].IDVinculoUsuario, TBVinculoUsuario[0].IDSetor, idsetor,
                            TBVinculoUsuario[0].IDEmpresa, IDEmpresa);
                    }
                }
                else
                {
                    adpVinculo.InsertNew(IDEmpresa, IDUsuario, IDRegimeHora, IDTPUsuario, 1, idsetor, dtinicio, 
                        idcargo, identidade, cadastraDigital, PainelDashboards, 
                        HoraSaidaTarde, HoraSaidaManha, HoraEntradaTarde, HoraEntradaManha, Matricula,DescontoTotalJornada,Isento);
                    msg = "Vínculo incluído com sucesso.";
                }

                //adpVinculo.FillTemplateIDUsuario(TBVinculoUsuario, IDUsuario);

                //if (TBVinculoUsuario.Rows.Count == 0)
                //{

                //}
                //else
                //{
                //    adpVinculo.InsertVinculoTemplate(IDEmpresa, IDUsuario, IDRegimeHora, IDTPUsuario, 1, idsetor, dtinicio, idcargo, identidade, cadastraDigital, PainelDashboards, HoraSaidaTarde, HoraSaidaManha, HoraEntradaTarde, HoraEntradaManha, Matricula,TBVinculoUsuario[0].Template1);
                //}
                //}
                //else
                //    msg = "Vínculo já existente para este Órgão/Setor.";
            }
            catch (Exception ex)
            {
                msg = "Houve falha na tentativa de cadastro. Tente Novamente!";
                ex.ToString();
            }

            //FAZER APÓS O LOG DA OPERAÇÃO
            log.RegistraLog(IDUsuarioOperador, System.DateTime.Now, adpVinculo.Adapter.InsertCommand.ToString(), "Inclusão de Vínculo de Usuário", string.Format("Nome: {0} Tipo: {1} Setor: {2}, Empresa: {3}", IDUsuario, IDTPUsuario, idsetor, IDEmpresa), "", IDEmpresa);

            return msg;

        }

        public string AlteraVinculoUsuario(int IDUsuario, int IDTPUsuario, int IDEmpresa, int IDRegimeHora, int idsetor,
            DateTime dtinicio, int idcargo, int identidade, bool cadastraDigital, bool PainelDashboards, string HoraEntradaManha, string HoraSaidaManha, string HoraEntradaTarde, string HoraSaidaTarde, 
            int IDVinculoUsaurio, int IDStatus, string Matricula, bool DescontoTotalJornada, bool Isento)
        {
            DataSetPontoFrequenciaTableAdapters.TBVinculoUsuarioTableAdapter adpVinculo = new DataSetPontoFrequenciaTableAdapters.TBVinculoUsuarioTableAdapter();
            try
            {
                //mudei para o updatenew por causa do pau que deu no updateNormal 23/-8/2018
                adpVinculo.UpdateNew(IDEmpresa, IDUsuario, IDRegimeHora, IDTPUsuario, IDStatus, idsetor, idcargo, identidade, cadastraDigital, PainelDashboards
                    , HoraSaidaTarde, HoraSaidaManha,
                HoraEntradaTarde, HoraEntradaManha, Matricula,DescontoTotalJornada,Isento, IDVinculoUsaurio);

                msg = "Alteração efetuada com sucesso!";
            }
            catch (Exception ex)
            {
                ex.ToString();
                msg = "Houve falha na alteração do vínculo. Tente novamente!";
            }

            return msg;
        }

        public string RemoveVinculoUsuario(int IDUsuario, int IDVinculoUsuario)
        {
            DataSetPontoFrequenciaTableAdapters.TBVinculoUsuarioTableAdapter adpVinculo = new DataSetPontoFrequenciaTableAdapters.TBVinculoUsuarioTableAdapter();
            try
            {
                adpVinculo.DeleteNew(IDVinculoUsuario);
                msg = "Registro excluído com sucesso.";
            }
            catch (Exception x)
            {
                msg = "Houve falha na tentativa de exclusão. Tente novamente!";
                x.ToString();
            }
            return msg;
        }

        public string DesativaVinculo(int IDUsuario, int IDVinculo)
        {
            DataSetPontoFrequenciaTableAdapters.TBVinculoUsuarioTableAdapter adpVinculo = new DataSetPontoFrequenciaTableAdapters.TBVinculoUsuarioTableAdapter();
            try
            {
                adpVinculo.UpdateStatusVinculo(IDVinculo, IDUsuario);
                msg = "Vínculo desativado com sucesso.";
            }
            catch (Exception ex)
            {
                msg = "Houve falha ao desativar o vínculo. Tente novamente.";
                ex.ToString();
            }

            return msg;
        }
        #endregion

        #region Usuario

        #region Usuário Integração com o e-turmalina - PMC
        protected string PrimeiroNome(string NomeCompleto)
        {
            if (NomeCompleto.IndexOf(' ') > 0)
            {
                while (NomeCompleto[cont] != ' ')
                {
                    Nome = Nome + NomeCompleto[cont];
                    cont++;
                }
            }
            else
            {
                Nome = NomeCompleto;
            }

            return Nome;
        }
        public bool InsertModifyUsuarioVinculo(string Usuario, string Senha, string TipoProcedimento, string CPF
            , string Matricula, string Nome, int IDCargo_eTurmalina, int IDEmpresaOrgao_eTurmalina, int IDRegimeHora, int IDSetor_eTurmalina,
            string TelefoneSMS)
        {
            //ajusta usuário para o eTurmalina
            if (Usuario == "ETURMALINAINTEGRACAO") { Usuario = "eTurmalinaIntegracao"; }
            if (Senha == "E@TURMALINA2PONTONAREDE") { Senha = "e@Turmalina2pontonarede"; }
            //try
            //{
            //    LogOperacao log = new LogOperacao();
            //    log.RegistraLog(010101, DateTime.Now, "ImportUsuario", "Import Usuario", string.Format("Usuario: {0} CPF: {1} Mat: {2}", Nome, CPF, Matricula), "", 36);
            //}
            //catch (Exception ex)
            //{
            //    ex.ToString();
            //}

            if (!PermiteAcesso(Usuario, Senha))
            {
                LogOperacao log = new LogOperacao();
                log.RegistraLog(010101, DateTime.Now, "ImportUsuario", "Import Usuario", string.Format("Usuario: {0} CPF: {1} Mat: {2}", Nome, CPF, Matricula), "Result: false. Usuário não autorizado." + Usuario + " Senha: " + Senha, 36);

                return false;
            }
            DataSetPontoFrequenciaTableAdapters.TBVinculoUsuarioTableAdapter adpVinculo = new 
                DataSetPontoFrequenciaTableAdapters.TBVinculoUsuarioTableAdapter();
            DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
            DataSetPontoFrequenciaTableAdapters.TBEmpresaTableAdapter adpEmpresa = new DataSetPontoFrequenciaTableAdapters.TBEmpresaTableAdapter();

            adpEmpresa.FillIDEturmalina(ds.TBEmpresa, IDEmpresaOrgao_eTurmalina);

            if (ds.TBEmpresa.Rows.Count == 0)
            {
                LogOperacao log = new LogOperacao();
                log.RegistraLog(010101, DateTime.Now, "ImportUsuario", "Import Usuario", string.Format("Usuario: {0} CPF: {1} Mat: {2}", Nome, CPF, Matricula), "Result: false. Empresa not found.", 36);
                return false;
            }
            else
                idempresa = ds.TBEmpresa[0].IDEmpresa;

            adpSetor.FilleTurmalina(ds.TBSetor, IDSetor_eTurmalina);

            if (ds.TBSetor.Rows.Count == 0)
            {
                adpSetor.FillByIDEmpresa(ds.TBSetor, ds.TBEmpresa[0].IDEmpresa);

                if (ds.TBSetor.Rows.Count == 0)
                {
                    LogOperacao log = new LogOperacao();
                    log.RegistraLog(010101, DateTime.Now, "ImportUsuario", "Import Usuario", string.Format("Usuario: {0} CPF: {1} Mat: {2}", Nome, CPF, Matricula), "Result: false. Setor não identificado.", 36);

                    //15/04/2019 - caso não ache o setor, add como padrão setor DAF -
                    adpSetor.FillDSSetorIDempresaImport(ds.TBSetor, "ADMINISTRATIVA E FINANCEIRA", idempresa);
                    if (ds.TBSetor.Rows.Count > 0)
                        idsetor = ds.TBSetor[0].IDSetor;

                    //return false;
                }
                else
                {
                    idsetor = ds.TBSetor[0].IDSetor;
                }
            }


            //Tratando o cargo
            DataSetPontoFrequenciaTableAdapters.TBCargoTableAdapter adpCargo =
                new DataSetPontoFrequenciaTableAdapters.TBCargoTableAdapter();
            try
            {
                adpCargo.FillIDCargo_eTurmalina(ds.TBCargo, IDCargo_eTurmalina);
                //Quando for 0 cadastrar o cargo. Informar isso a ábaco.
                if (ds.TBCargo.Rows.Count == 0)
                {
                    LogOperacao log = new LogOperacao();
                    log.RegistraLog(010101, DateTime.Now, "ImportUsuario", "Import Usuario", string.Format("Usuario: {0} CPF: {1} Mat: {2}", Nome, CPF, Matricula), "Result: false. Cargo Inexistente.", 36);
                    //15/04/2019 - caso não encontre o cargo. add o usuário com um cargo padrão.
                    IDCargo = 1;
                    //return false;
                }

                IDCargo = ds.TBCargo[0].IDCargo;
            }
            catch
            {
            }
            LogOperacao logGeral = new LogOperacao();
            switch (IDRegimeHora)
            {
                case 1:
                    HoraEntradaManha = "08:00";
                    HoraSaidaManha = "12:00";
                    HoraEntradaTarde = "14:00";
                    HoraSaidaTarde = "18:00";
                    TotalHorasDia = 8;
                    TotalHoraSemana = 40;
                    break;
                case 2:
                    HoraEntradaManha = "07:00";
                    HoraSaidaManha = "14:00";
                    HoraEntradaTarde = "13:00";
                    HoraSaidaTarde = "19:00";
                    TotalHorasDia = 6;
                    TotalHoraSemana = 30;
                    break;
                case 3:
                    HoraEntradaManha = "07:00";
                    HoraSaidaManha = "19:00";
                    HoraEntradaTarde = "19:00";
                    HoraSaidaTarde = "07:00";
                    TotalHorasDia = 12;
                    TotalHoraSemana = 40;
                    break;
                case 4:
                    HoraEntradaManha = "07:00";
                    HoraSaidaManha = "17:00";
                    HoraEntradaTarde = "19:00";
                    HoraSaidaTarde = "19:00";
                    TotalHorasDia = 24;
                    TotalHoraSemana = 40;
                    break;
                case 5:
                    HoraEntradaManha = "08:00";
                    HoraSaidaManha = "12:00";
                    HoraEntradaTarde = "14:00";
                    HoraSaidaTarde = "18:00";
                    TotalHorasDia = 4;
                    TotalHoraSemana = 20;
                    break;
            }
            try
            {
                switch (TipoProcedimento)
                {
                    case "I":

                        adpUsuario.FillLogin(ds.TBUsuario, CPF.Trim());
                        if (ds.TBUsuario.Rows.Count > 0)
                        {
                            adpUsuario.UpdateChangeStatus(1, ds.TBUsuario[0].IDUsuario);
                            adpVinculo.UpdateIDUsuarioMatricula(1, ds.TBUsuario[0].IDUsuario, Matricula.Trim());
                            
                            logGeral.RegistraLog(010101, DateTime.Now, "ImportUsuario", "Import Usuario",
                            string.Format("Usuario: {0} CPF: {1} Mat: {2}", Nome, CPF, Matricula),
                            "Result: True. Usuário reativado.", 36);

                            return true;
                        }

                            Cript cript = new Cript();
                        adpUsuario.Insert(ds.TBSetor[0].IDSetor, 1, 1, Nome, CPF, DateTime.Now, 2, "pontonarede", IDCargo,
                            TotalHorasDia, HoraEntradaManha, HoraEntradaTarde, HoraSaidaManha, HoraSaidaTarde, PrimeiroNome(Nome),
                            1, "0", true, ds.TBEmpresa[0].IDEmpresa, string.Empty, false, false, true, IDRegimeHora, TelefoneSMS, Matricula, cript.ActionEncrypt("pontonarede"));
                        break;
                    case "M":
                        adpUsuario.FillLogin(ds.TBUsuario, CPF.Trim());
                        if (ds.TBUsuario.Rows.Count > 0)
                        {
                            if(idsetor != 0 && idempresa != 0)
                            {
                                adpVinculo.UpdateIntegracaoMudaLotacao(1, idsetor, idempresa, ds.TBUsuario[0].IDUsuario, Matricula.Trim());
                                logGeral.RegistraLog(010101, DateTime.Now, "ImportUsuario", "Import Usuario",
    string.Format("Usuario: {0} CPF: {1} Mat: {2}", Nome, CPF, Matricula),
    "Result: True. Alterado.", 36);
                            }

                            else
                            {
                                logGeral.RegistraLog(010101, DateTime.Now, "ImportUsuario", "Alterar Usuario",
    string.Format("Usuario: {0} CPF: {1} Mat: {2}", Nome, CPF, Matricula),
    "Result: False. Setor ou Empresa not found.", 36);
                                return false;
                            }

                        }
                        else
                        {
                            logGeral.RegistraLog(010101, DateTime.Now, "ImportUsuario", "Alterar Usuario",
                                string.Format("Usuario: {0} CPF: {1} Mat: {2}", Nome, CPF, Matricula),
                                "Result: False. CPF não encontrado.", 36);
                            return false;
                        }
                        break;
                    case "E":
                        adpUsuario.FillLogin(ds.TBUsuario, CPF.Trim());
                        if (ds.TBUsuario.Rows.Count > 0)
                        {
                            if (adpUsuario.UpdateChangeStatus(2, ds.TBUsuario[0].IDUsuario) > 0)
                            {
                                adpVinculo.UpdateIDUsuarioMatricula(2, ds.TBUsuario[0].IDUsuario, Matricula.Trim());
                            }
                            else
                                return false;


                            logGeral.RegistraLog(010101, DateTime.Now, "ImportUsuario", "Import Usuario",
string.Format("Usuario: {0} CPF: {1} Mat: {2}", Nome, CPF, Matricula),
"Result: True. Usuário desativado.", 36);

                        }

                        else
                        {

                            logGeral.RegistraLog(010101, DateTime.Now, "ImportUsuario", "Import Usuario", 
                                string.Format("Usuario: {0} CPF: {1} Mat: {2}", Nome, CPF, Matricula), 
                                "Result: False. CPF não encontrado.", 36);
                            return false;
                        }
                        break;
                }
                LogOperacao log = new LogOperacao();
                log.RegistraLog(010101, DateTime.Now, "ImportUsuario", "Import Usuario", string.Format("Usuario: {0} CPF: {1} Mat: {2}", Nome, CPF, Matricula), "Result: True. Usuário Importado.", 36);

                return true;
            }
            catch (Exception ex)
            {
                //Não deu certo, cadastro o vínculo caso ele seja inexistente.
                adpUsuario.FillLogin(ds.TBUsuario, CPF.Trim());

                if (ds.TBUsuario.Rows.Count > 0)
                {
                    if (CadastraVinculoUsuario2(ds.TBUsuario[0].IDUsuario, 2, ds.TBEmpresa[0].IDEmpresa, 1, ds.TBSetor[0].IDSetor,
                        DateTime.Now, IDCargo, 1, false, false, HoraEntradaManha, HoraSaidaManha, HoraEntradaTarde, HoraSaidaTarde,
                        010101,1, Matricula,false,false).IndexOf("Já existente") > (-1))
                    {
                        LogOperacao logg = new LogOperacao();
                        logg.RegistraLog(010101, DateTime.Now, "ImportUsuario", "Import Usuario", string.Format("Usuario: {0} CPF: {1} Mat: {2}", Nome, CPF, Matricula), "Result: false Vinculo já existente.", 36);
                        return false;
                    }
                    else
                    {
                        LogOperacao logg = new LogOperacao();
                        logg.RegistraLog(010101, DateTime.Now, "ImportUsuario", "Import Usuario", string.Format("Usuario: {0} CPF: {1} Mat: {2}", Nome, CPF, Matricula), "Result: true Vinculo importado.", 36);
                        return true;
                    }


                    return true;
                }

                LogOperacao log = new LogOperacao();
                log.RegistraLog(010101, DateTime.Now, "ImportUsuario", "Import Usuario", string.Format("Usuario: {0} CPF: {1} Mat: {2}", Nome, CPF, Matricula), "Result: " + ex.Message.Trim(), 36);
                return false;
            }

        }
        #endregion

        //Referência REP
        public bool AlteraReferenciaREP(int IDEmpresa, int IDUsuario, string RefREP)
        {
            bool Result = false;
            DataSetPontoFrequenciaTableAdapters.TBUsuarioTableAdapter adpUsuario = new DataSetPontoFrequenciaTableAdapters.TBUsuarioTableAdapter();
            try
            {
                adpUsuario.UpdateReferenciaREP(RefREP, IDEmpresa, IDUsuario);
                Result = true;
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                Result = false;
            }
            catch (Exception ex)
            {
                Result = false;
            }

            return Result;
        }
        //-------------

        //Altera Vinculo com a inclusão de Template
        public bool InsertTemplate1_2(int IDVinculoUsuario, int IDEmpresa, byte[] Template1, byte[] Template2, string LoginOperador)
        {
            DataSetPontoFrequenciaTableAdapters.TBVinculoUsuarioTableAdapter adpVinculo = new DataSetPontoFrequenciaTableAdapters.TBVinculoUsuarioTableAdapter();

            //salva os dois templates
            if (Template1 != null && Template2 != null)
            {
                //salva os dois templates
                adpVinculo.UpdateTemplate1_2(Template1, Template2, LoginOperador, IDVinculoUsuario);
            }
            //salva template1
            if (Template1 != null && Template2 == null)
            {
                //em 20/06/2018 - Retirei o parâmetro empresa por causa do grupo de registro.
                //23/08/2018 -- para a nova verão.
                adpVinculo.UpdateTemplate1(Template1, LoginOperador, IDVinculoUsuario);
                return true;
            }

            DataSetPontoFrequencia.TBVinculoUsuarioDataTable TBVinculousuario = new DataSetPontoFrequencia.TBVinculoUsuarioDataTable();

            adpVinculo.FillIDVinculoUsuario(TBVinculousuario, IDVinculoUsuario);

            return false;
        }

        public bool InsertTemplate1(int IDVinculoUsuario, int IDEmpresa, byte[] Template1, string LoginOperador)
        {
            DataSetPontoFrequenciaTableAdapters.TBVinculoUsuarioTableAdapter adpVinculo = new DataSetPontoFrequenciaTableAdapters.TBVinculoUsuarioTableAdapter();
            try
            {
                //em 20/06/2018 - Retirei o parâmetro empresa por causa do grupo de registro.
                adpVinculo.UpdateTemplate1(Template1, LoginOperador, IDVinculoUsuario);
                return true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }
        }
        public bool InsertTemplate2(int IDVinculoUsuario, int IDEmpresa, byte[] Template2)
        {
            DataSetPontoFrequenciaTableAdapters.TBVinculoUsuarioTableAdapter adpVinculo = new DataSetPontoFrequenciaTableAdapters.TBVinculoUsuarioTableAdapter();
            try
            {
                adpVinculo.UpdateTemplate2(Template2, IDVinculoUsuario);
                return true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }
        }

        //SenhaTextHashCode

        public string ManutencaoSenhaTextHashCode(int IDUsuario, string SenhaHashCode, int IDEmpresa)
        {

            //pesquisar antes se faz parte de algum grupo de registro.
            DataSetPontoFrequencia dsT = new DataSetPontoFrequencia();
            dsT.EnforceConstraints = false;
            adpUsuario.FillIDusuarioGrupoEmpresa(dsT.TBUsuario, IDUsuario);

            if (dsT.TBUsuario.Rows.Count > 0)
            {
                PreencheTabela PT = new PreencheTabela();
                if (PT.EmpresaGrupoRegistro(dsT.TBUsuario[0].IDEmpresa))//Para continuar verifica-se se pertence a algum grupo de registro
                    IDEmpresa = dsT.TBUsuario[0].IDEmpresa;
            }
            //-------------------------------------------------------

            try
            {
                if (adpUsuario.UpdateSenhaTextHashCode(SenhaHashCode, IDUsuario) > 0)
                    msg = string.Format("Manutenção de digital concluída com sucesso.");
                else
                    msg = "Digital não atualizada. Favor repetir o processo.";
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                ErroOperacao EO = new ErroOperacao();
                msg = EO.RetornaErroOperacao(ex);
            }
            return msg;
        }

        //Apenas teste
        public string CadastraUsuarioTeste(string Nome, string Senha1, string Senha2)
        {
            DataSetPontoFrequenciaTableAdapters.TBUserTesteTableAdapter adpUser = new DataSetPontoFrequenciaTableAdapters.TBUserTesteTableAdapter();
            try
            {
                adpUser.Insert(Nome, Senha1, Senha2, null, null, null, null);
                msg = "Registro incluído com sucesso.";
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                msg = ex.ToString();
            }


            return msg;
        }

        //

        public string CadastraUsuario(string login, System.Byte[] Senha, string NomeUsuario, int Entidade, int Status, int Setor, int TPUsuario, int IDCArgo, int TotHorasDiarias, string EntradaManha, string SaindaManha, string EntradaTarde, string SaidaTarde, int PrimeiroAcesso, string PrimeiroNome, int UsuarioOperador, string SenhaDigital, Boolean AcessoEspecial, int IDEmpresa, DateTime DTNascimento, DateTime DTAdmissao, string PIS, int IDGenero, DateTime DTDemissao, string Pai, string Mae, string CTPS, DateTime DTCTPS, string CartReserv, string TituloEleitor, string RG, DateTime DTRG, string OrgaoEmissor, string Logradouro, string Numero, string CEP, string Cidade, string Fone, string Celular, int GrauInstr, string OBS, string[] Setores, bool Dashboard, bool CadastraDigital, bool finalizaDiaSeguinte, int IDRegimeHora, string Telefone, string Matricula, bool Isencao)
        {
            try
            {
                Cript cript = new Cript();

                adpUsuario.Insert(Setor, Status, Entidade, NomeUsuario, login, System.DateTime.Now, TPUsuario, "pontonarede", IDCArgo, TotHorasDiarias,
                    EntradaManha, EntradaTarde, SaindaManha, SaidaTarde, PrimeiroNome, PrimeiroAcesso, SenhaDigital, AcessoEspecial, IDEmpresa, PIS, Dashboard, CadastraDigital, finalizaDiaSeguinte, IDRegimeHora, Telefone, Matricula, cript.ActionEncrypt("pontonarede"));

                //Incluindo a gestão Caso seja usuário Tipo 3
                if (TPUsuario == 3 || TPUsuario == 9)
                {
                    DataSetPontoFrequencia.TBUsuarioDataTable TBUserLogin = new DataSetPontoFrequencia.TBUsuarioDataTable();
                    DataSetPontoFrequenciaTableAdapters.TBUsuarioTableAdapter adpUserP = new DataSetPontoFrequenciaTableAdapters.TBUsuarioTableAdapter();

                    adpUserP.FillLogin(TBUserLogin, login);

                    if (TBUserLogin.Rows.Count > 0)
                    {
                        ManutencaoGestorSetor(TBUserLogin[0].IDUsuario, TPUsuario, IDEmpresa, Setores, "Inclusao");
                    }
                }

                log.RegistraLog(UsuarioOperador, System.DateTime.Now, adpUsuario.Adapter.InsertCommand.ToString(), "Inclusão de usuário", string.Format("Nome: {0} Tipo: {1} Setor: {2}", NomeUsuario, TPUsuario, Setor), "", IDEmpresa);
                msg = "Registro Incluso com Sucesso.";
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                //if(ex.GetType == System.Data.)
                if (ex.ErrorCode == -2147217873)
                    msg = String.Format("Já existe um usuário cadastrado com essa matricula/CPF");
                else
                    msg = "Falha ao incluir o registro. Tente Novamente.";
            }

            return msg;
        }

        public string AlteraUsuario(string login, System.Byte[] Senha, string NomeUsuario, int Entidade, int Status, int Setor, int TPUsuario, int IDCArgo, int TotHorasDiarias, string EntradaManha, string SaindaManha, string EntradaTarde, string SaidaTarde, int PrimeiroAcesso, string PrimeiroNome, int IDUsuario, int UsuarioOperador, int IDEmpresa, string CaminhoFoto, string[] Setores)
        {
            try
            {
                //adpUsuario.Update(Setor, Status, Entidade, NomeUsuario, login, System.DateTime.Now, Senha, null, TPUsuario, "pontonarede", IDCArgo, TotHorasDiarias, EntradaManha, EntradaTarde, SaindaManha, SaidaTarde, PrimeiroNome, PrimeiroAcesso, false, CaminhoFoto, 1, IDEmpresa);
                log.RegistraLog(UsuarioOperador, System.DateTime.Now, "Alteracao de Usuário " + "" + PrimeiroNome + "" + NomeUsuario + "" + Setor, "AlteracaoUsuario de usuario", PrimeiroNome + "" + NomeUsuario, "", IDEmpresa);

                ManutencaoGestorSetor(IDUsuario, TPUsuario, IDEmpresa, Setores, "Alteracao");

                msg = "Registro Alterado com Sucesso.";
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                //if(ex.GetType == System.Data.)
                if (ex.ErrorCode == -2147217873)
                    msg = String.Format("Já existe um usuário cadastrado com essa matricula/CPF");
                else
                    msg = "Falha ao incluir o registro. Tente Novamente.";
            }
            return msg;
        }

        public string AlteraUsuarioAdminSenha(string login, string NomeUsuario, int Entidade, 
            int Status, int Setor, int TPUsuario, int IDCArgo, int TotHorasDiarias, 
            string EntradaManha, string SaindaManha, string EntradaTarde, string SaidaTarde, 
            int PrimeiroAcesso, string PrimeiroNome, int IDUsuario, string senha, 
            int TipoUsuario, int cargo, int UsuarioOperador, string SenhaDigital,
            Boolean AcessoEspecial, int IDEmpresa, DateTime DTNascimento, 
            DateTime DTAdmissao, string PIS, int IDGenero, DateTime DTDemissao,
            string Pai, string Mae, string CTPS, DateTime DTCTPS, string CartReserv, 
            string TituloEleitor, string RG, DateTime DTRG, string OrgaoEmissor, 
            string Logradouro, string Numero, string CEP, string Cidade, string Fone, 
            string Celular, int GrauInstr, string OBS, string[] Setores, bool dashboard, 
            bool CadastraDigital, bool finalizadiaSeguinte, string Telefone)
        {
            Cript critp = new Cript();
            try
            {
                adpUsuario.UpdateAdminSenhaUser(Setor, Status, Entidade, NomeUsuario, login, TPUsuario, critp.ActionEncrypt(senha), IDCArgo, TotHorasDiarias, EntradaManha, EntradaTarde, SaindaManha, SaidaTarde, PrimeiroNome, PrimeiroAcesso, AcessoEspecial, IDEmpresa, dashboard, CadastraDigital, finalizadiaSeguinte, Telefone, PIS, senha, IDUsuario);

                ManutencaoGestorSetor(IDUsuario, TPUsuario, IDEmpresa, Setores, "Alteracao");

                //try
                //{
                //    Util.ExecuteNonQuery("UPDATE TBVinculoUsuario SET IsencaoPonto = " + ((Isencao) ? 1 : 0) + " WHERE IDEmpresa = " + IDEmpresa + " AND IDUsuario = " + IDUsuario + " AND IDSetor = " + Setor);
                //}
                //catch { }

                //log.RegistraLog(UsuarioOperador, System.DateTime.Now, "Alteração de Usuário " + PrimeiroNome + "" + NomeUsuario + "" + Setor, "Alteração de Usuário", PrimeiroNome + "" + NomeUsuario, "");

                msg = "Registro alterado com sucesso";
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                //if(ex.GetType == System.Data.)
                if (ex.ErrorCode == -2147217873)
                    msg = String.Format("Já existe um usuário cadastrado com essa matricula/CPF");
                else
                    msg = "Falha ao incluir o registro. Tente Novamente.";
            }

            log.RegistraLog(UsuarioOperador, System.DateTime.Now, adpUsuario.Adapter.UpdateCommand.ToString(), "Alteração de usuário", string.Format("Nome: {0} Tipo: {1} Setor: {2}", NomeUsuario, TPUsuario, Setor), "", IDEmpresa);

            return msg;
        }

        public string AlteraUsuarioAdmin(string login, string NomeUsuario, int Entidade, 
            int Status, int Setor, int TPUsuario, int IDCArgo, int TotHorasDiarias, string EntradaManha, 
            string SaindaManha, string EntradaTarde, string SaidaTarde, int PrimeiroAcesso, 
            string PrimeiroNome, int IDUsuario, int TipoUsuario, int cargo, 
            int UsuarioOperador, string SenhaDigital, Boolean AcessoEspecial, int IDEmpresa, 
            DateTime DTNascimento, DateTime DTAdmissao, string PIS, int IDGenero, DateTime DTDemissao, 
            string Pai, string Mae, string CTPS, DateTime DTCTPS, string CartReserv, string TituloEleitor, 
            string RG, DateTime DTRG, string OrgaoEmissor, string Logradouro, string Numero, string CEP, 
            string Cidade, string Fone, string Celular, int GrauInstr, string OBS, string[] Setores, 
            bool dashboard, bool cadastraDigital, bool finalizadiaseguinte, 
            string Telefone, bool Isencao)
        {
            int LinhaAlterada;
            try
            {
                LinhaAlterada = adpUsuario.UpdateAdmin(Setor, Status, Entidade, NomeUsuario, login, null, TipoUsuario, IDCArgo,
                    TotHorasDiarias, EntradaManha, EntradaTarde, SaindaManha, SaidaTarde, PrimeiroNome, PrimeiroAcesso,
                    SenhaDigital, AcessoEspecial, dashboard, cadastraDigital, finalizadiaseguinte, Telefone, PIS, IDUsuario);

                ManutencaoGestorSetor(IDUsuario, TPUsuario, IDEmpresa, Setores, "Alteracao");

                //try
                //{
                //    Util.ExecuteNonQuery("UPDATE TBVinculoUsuario SET IsencaoPonto = " + ((Isencao) ? 1 : 0) + " WHERE IDEmpresa = " + IDEmpresa + " AND IDUsuario = " + IDUsuario + " AND IDSetor = " + Setor);
                //}
                //catch { }

                //log.RegistraLog(UsuarioOperador, System.DateTime.Now, "Alteração de Usuário " +PrimeiroNome + "" + NomeUsuario + "" + Setor, "Alteração de Usuário", PrimeiroNome+""+NomeUsuario, "");

                msg = "Registro alterado com sucesso";
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                //if(ex.GetType == System.Data.)
                if (ex.ErrorCode == -2147217873)
                    msg = String.Format("Já existe um usuário cadastrado com essa matricula/CPF");
                else
                    msg = "Falha ao incluir o registro. Tente Novamente.";
            }

            log.RegistraLog(UsuarioOperador, System.DateTime.Now, adpUsuario.Adapter.UpdateCommand.ToString(), "Alteração de usuário", string.Format("Nome: {0} Tipo: {1} Setor: {2}", NomeUsuario, TPUsuario, Setor), "", IDEmpresa);

            return msg;
        }
        public string AlteraHorasUsuario(int IDUsuario, String Setor, string EntradaManha, string EntradaTarde, string SaidaManha,
            string SaidaTarde, int UsuarioOperador, int IDEmpresa)
        {
            try
            {
                adpUsuario.UpdateHorasDiarias(EntradaManha, EntradaTarde, SaidaManha, SaidaTarde, IDUsuario);

                //log.RegistraLog(UsuarioOperador, System.DateTime.Now, "adpUsuario.UpdateHorasDiarias(EntradaManha, EntradaTarde, SaidaManha, SaidaTarde, IDUsuario)", "Alterar Horário Usuario", Convert.ToString(IDUsuario),"");

                msg = "Registro Incluso com sucesso.";
            }
            catch (Exception ex)
            {
                msg = "Falha ao incluir o registro. Tente Novamente.";
                ex.ToString();
            }

            //log.RegistraLog(UsuarioOperador, System.DateTime.Now, adpUsuario.Adapter.UpdateCommand.ToString(), "Alteração de usuário", string.Format("Nome: {0} Tipo: {1} Setor: {2}", NomeUsuario, TPUsuario, Setor), "");

            return msg;
        }

        #endregion //Logs  //

        #region Setor

        #region Setor Intregração e-turmalina
        public bool InsertSetor(string Usuario, string Senha, string TipoProcedimento, string DSSetor, string Sigla, int IDSetor_eTurmalina, int IDEmpresaOrgao_eTurmalina)
        {
            try
            {
                LogOperacao log = new LogOperacao();
                log.RegistraLog(010101, DateTime.Now, "InsertSetor", "Import Setor", DSSetor, "", 36);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            if (!PermiteAcesso(Usuario, Senha))
                return false;

            DataSetPontoFrequenciaTableAdapters.TBSetorTableAdapter adpSetor = new
             DataSetPontoFrequenciaTableAdapters.TBSetorTableAdapter();
            try
            {
                switch (TipoProcedimento)
                {
                    case "I":
                        //Procura o ID da Empresa pelo IDeTurmalina
                        PreencheTabela PT = new PreencheTabela();
                        DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
                        PT.GetEmpresa(ds, IDEmpresaOrgao_eTurmalina);
                        if (ds.TBEmpresa.Rows.Count == 0)
                            return false;
                        adpSetor.InserteTurmalina(DSSetor, 1, Sigla, DateTime.Now, DateTime.Now, DateTime.Now, DateTime.Now, ds.TBEmpresa[0].IDEmpresa, IDSetor_eTurmalina);
                        break;
                    case "M":
                        adpSetor.UpdateeTurmalina(DSSetor, null, 1, Sigla, IDSetor_eTurmalina);
                        break;
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        #endregion

        public void trocaUsuario(int IDEmpresa, int IDSetor, int IDUsuario)
        {
            DataSetPontoFrequenciaTableAdapters.TBUsuarioTableAdapter adpUser = new DataSetPontoFrequenciaTableAdapters.TBUsuarioTableAdapter();
            try
            {
                adpUser.UpdateUsuarioTroca(IDEmpresa, IDSetor, IDUsuario);
            }
            catch (
                Exception ex)
            {

            }
        }

        public string CadastraSetor(string Setor, int Status, string SIGLA, int IDEmpresa)
        {

            try
            {
                adpSetor.Insert(Setor, 1, Status, SIGLA, Convert.ToDateTime("08:00:00"), Convert.ToDateTime("12:00:00"), Convert.ToDateTime("14:00:00"), Convert.ToDateTime("18:00:00"), IDEmpresa);
                msg = "Registro Incluso com Sucesso.";
            }
            catch (Exception ex)
            {
                msg = "Falha ao Incluir o Registro. Tente Novamente.";
                ex.ToString();
            }

            return msg;
        }

        public string AlterarSetor(string Setor, string SIGLA, int IDSetor, int IDEmpresa, int IDSTATUS)
        {
            try
            {
                adpSetor.Update(Setor, 1, IDSTATUS, SIGLA, IDSetor, IDEmpresa);
                msg = "Registro Alterado com Sucesso.";
            }
            catch (Exception ex)
            {
                msg = "Falha ao alterar o registro. Tente novamente.";
                ex.ToString();
            }
            return msg;
        }

        public string AlteraSetorHoras(int IDSetor, String Setor, DateTime EntradaManha, DateTime EntradaTarde, DateTime SaidaManha, DateTime SaidaTarde, int IDEmpresa)
        {
            try
            {
                adpSetor.UpdateHorasDiarias(EntradaManha, EntradaTarde, SaidaManha, SaidaTarde, IDSetor, IDEmpresa);
                msg = "Registro Incluso com Sucesso";
            }
            catch (Exception ex)
            {
                ex.ToString();
                msg = "Falha ao incluir o registro. Tente Novamente.";
            }

            return msg;
        }

        public void SubirFoto(int IDUsuario, int IDempresa, Byte[] FOto)
        {
            try
            {
                //DataSetPontoFrequenciaTableAdapters.TBEmpresaTableAdapter adpEmpresa = new DataSetPontoFrequenciaTableAdapters.TBEmpresaTableAdapter();
                //adpEmpresa.UpdateImgEmpresa(FOto, IDempresa);
                adpUsuario.UpdateSubirFoto(FOto, IDUsuario, IDempresa);
                msg = "Foto Incluída com sucesso.";
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                ErroOperacao EO = new ErroOperacao();
                msg = EO.RetornaErroOperacao(ex);
            }
            catch (Exception ex)
            {
                msg = "Falha ao tentar subir arquivo. Tente Novamente.";
                ex.ToString();
            }
        }

        #endregion

        #region MotivoFalta

        public string CadastraMotivoFalta_eTurmalina(string MotivoFalta, bool AbonoFalta, int IDMotivoFalta, int IDMotivoFalta_eTurmalina)
        {
            try
            {
                adpMotivoFalta.InsertNeweTurmalina(MotivoFalta, AbonoFalta, 2, IDMotivoFalta_eTurmalina);
                msg = "Registro Incluso com Sucesso.";
            }
            catch (Exception ex)
            {
                msg = "Falha ao Incluir o Registro. Tente Novamente.";
                ex.ToString();
            }
            return msg;
        }

        public string CadastraMotivoFalta(string MotivoFalta, bool AbonoFalta, int IDMotivoFalta, int IDMotivoFalta_eTurmalina)
        {
            try
            {
                adpMotivoFalta.Insert(MotivoFalta, AbonoFalta, IDMotivoFalta, IDMotivoFalta_eTurmalina);
                msg = "Registro Incluso com Sucesso.";
            }
            catch (Exception ex)
            {
                msg = "Falha ao Incluir o Registro. Tente Novamente.";
                ex.ToString();
            }
            return msg;
        }

        public string AlteraMotivoFalta(string MotivoFalta, int IDMotivoFalta, bool AbonoFalta, int IDStatus)
        {
            try
            {
                adpMotivoFalta.Update(MotivoFalta, AbonoFalta, IDStatus, IDMotivoFalta);
                msg = "Registro Alterado com Sucesso.";
            }
            catch (Exception ex)
            {
                msg = "Falha ao Alterar o Registro. Tente novamente.";
                ex.ToString();
            }
            return msg;
        }
        #endregion

        #region FeriadoPontoFacultativo
        public string CadastraFeriadoPontoFacultativo(string FeriadoPontoFacultativo, DateTime DTFeriadoPontoFacultativo, int IDTPFeriadoPontoFacultativo, int IDEmpresa)
        {

            try
            {
                adpFeriadoPontoFacultativo.Insert(FeriadoPontoFacultativo, null, IDTPFeriadoPontoFacultativo, IDEmpresa, DTFeriadoPontoFacultativo);
                msg = "Registro Incluso com Sucesso.";
            }
            catch (Exception ex)
            {
                msg = "Falha ao incluir o Registro. Tente Novamente.";
                ex.ToString();
            }
            return msg;
        }

        public string AlteraFeriadoPontoFacultativo(string FeriadoPontoFacultativo, DateTime DTFeriadoPontoFacultativo, int IDFeriadoPontoFacultativo, int IDTPFeriadoPontoFacultativo, int IDEMpresa)
        {
            try
            {
                adpFeriadoPontoFacultativo.Update(FeriadoPontoFacultativo, null, IDTPFeriadoPontoFacultativo, DTFeriadoPontoFacultativo, IDFeriadoPontoFacultativo, IDEMpresa);
                msg = "Registro Alterado com Sucesso.";
            }
            catch (Exception ex)
            {
                msg = "Falha ao tentar alterar o Registro. Tente novamente.";
                ex.ToString();
            }
            return msg;
        }
        #endregion

        #region Cargo

        #region Cargo integração e-turmalina
        public bool InsertCargo(string Usuario, string Senha, string TipoProcedimento, string DSCargo, int IDCargo_eTurmalina, int IDEmpresa_eTurmalina, int IDSetor_eTurmalina)
        {
            if (!PermiteAcesso(Usuario, Senha))
                return false;

            DataSetPontoFrequenciaTableAdapters.TBCargoTableAdapter adpCargo =
                new DataSetPontoFrequenciaTableAdapters.TBCargoTableAdapter();

            try
            {
                switch (TipoProcedimento)
                {
                    case "I":
                        adpCargo.Insert_eTurmalina(DSCargo, IDCargo_eTurmalina);
                        break;
                    case "M":
                        adpCargo.UpdateIDCargoeTurmalina(DSCargo, IDCargo_eTurmalina);
                        break;
                }

                return true;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }
        }
        #endregion

        public void ModificaOBSCargo(int IDCargo, string OBSCargo)
        {
            adpCargo.UpdateCBOCargo(OBSCargo, IDCargo);
        }

        public string CadastraCargo(string Cargo)
        {
            try
            {
                adpCargo.Insert(Cargo);
                msg = "Registro incluído com Sucesso.";
            }
            catch (Exception ex)
            {
                msg = "Falha ao incluir o Registro.";
                ex.ToString();
            }
            return msg;
        }

        public string AlteraCargo(string Cargo, int IDCargo)
        {
            try
            {
                adpCargo.Update(Cargo, IDCargo);
                msg = "Registro Alterado com Sucesso.";
            }
            catch (Exception ex)
            {
                msg = "Falha ao Alterar o Registro. Tente novamente.";
                ex.ToString();
            }

            return msg;
        }
        #endregion

        #region InformacaoDiaria
        public string CadastraInformacaoDiaria(string InformacaoDiaria, DateTime DTInformacaoDiaria, int IDEmpresa)
        {
            try
            {
                adpInformacaoDiaria.Insert(InformacaoDiaria, IDEmpresa, DTInformacaoDiaria);
                msg = "Registro Incluso com Sucesso.";
            }
            catch (Exception ex)
            {
                msg = "Falha ao Incluir o Registro. Tente Novamente.";
                ex.ToString();
            }
            return msg;
        }
        public string AlteraInformacaoDiaria(string InformacaoDiaria, int IDInformacaoDiaria, int IDEmpresa)
        {
            try
            {
                adpInformacaoDiaria.Update(InformacaoDiaria, IDInformacaoDiaria, IDEmpresa);
                msg = "Registro Alterado com sucesso.";
            }
            catch (Exception ex)
            {
                msg = "Falha na tentativa de alterar o registro. Tente novamente.";
                ex.ToString();
            }

            return msg;
        }
        #endregion

        #region Férias

        #region Integração e-turmalina
        //public bool ModifyAfastamento(string Usuario, string Senha, string Matricula, int IDSetor, int IDEmpresa, int TipoAfastamento, int IDjustificativa, DateTime DTInicial, DateTime DTFinal)
        //{
        //    PreencheTabela PT = new PreencheTabela();
        //    DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
        //    PT.GetVinculoUsuario(ds, Matricula);

        //    //OBS: ver se é possível ter mais de um afastamento no mesmo período. 14/06/2018

        //    if (ds.TBVinculoUsuario.Rows.Count == 0)
        //        return false;


        //    //Objeto para inserir as férias
        //    DataSetPontoFrequenciaTableAdapters.TBFeriasTableAdapter adpFerias =
        //        new DataSetPontoFrequenciaTableAdapters.TBFeriasTableAdapter();
        //    Frequencia freq = new Frequencia();

        //    DataSetPontoFrequenciaTableAdapters.TBTipoFeriasTableAdapter adpTPFerias =
        //        new DataSetPontoFrequenciaTableAdapters.TBTipoFeriasTableAdapter();
        //    adpTPFerias.FilleTurmalina(ds.TBTipoFerias, TipoAfastamento);

        //    if (ds.TBTipoFerias.Rows.Count == 0)
        //        return false; //Retorna falso pq não o motivo cadastrado no pontoweb.
        //    else
        //        IDTipoFerias = ds.TBTipoFerias[0].IDTPFerias;

        //    //Achar a férias que será alterada.

        //    try
        //    {
        //        adpFerias.Insert(ds.TBVinculoUsuario[0].IDUsuario, 1, DTInicial, DTFinal, IDTipoFerias, ds.TBVinculoUsuario[0].IDVinculoUsuario);
        //        freq.ManutencaoFrequenciaFeriasLicenca(ds.TBVinculoUsuario[0].IDUsuario, IDTipoFerias, DTInicial, DTFinal, "Inclusao", DTInicial, DTFinal, ds.TBVinculoUsuario[0].IDEmpresa, ds.TBVinculoUsuario[0].IDVinculoUsuario);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        ///Construir log de excessões para essa integração
        //        return false;
        //    }
        //}
        public bool InsertAfastamento(string Usuario, string Senha, string Matricula, int IDSetor, int IDEmpresa, int TipoAfastamento, int IDjustificativa, DateTime DTInicial, DateTime DTFinal, string TipoProcedimento)
        {
            PreencheTabela PT = new PreencheTabela();
            DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
            PT.GetVinculoUsuario(ds, Matricula);

            try
            {
                LogOperacao log = new LogOperacao();
                log.RegistraLog(010101, DateTime.Now, "InsertAfastamento", "Import Afastamento", string.Format("Usuario: {3} Per: {0} a {1} Situacao: {2} Op: {4}", DTInicial.ToShortDateString(), DTFinal.ToShortDateString(), IDjustificativa, Matricula, TipoProcedimento), "", 36);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            if (ds.TBVinculoUsuario.Rows.Count == 0)
                return false;


            //Objeto para inserir as férias
            DataSetPontoFrequenciaTableAdapters.TBFeriasTableAdapter adpFerias =
                new DataSetPontoFrequenciaTableAdapters.TBFeriasTableAdapter();
            Frequencia freq = new Frequencia();
            //Retornou maior que zero, segue o plano
            //Case 2 = férias
            //Case 3 = Afastamento
            //NÃO USAR - LEIA NO CASE 4//Case 4 = Desligado - Achar um meio de desabilitar o vínculo do fdp

            //25/05/2018 - Mudança no cenário - Busca do TIpoAfastamento/TBMotivoFalta pelo IDeTurmalina
            DataSetPontoFrequenciaTableAdapters.TBTipoFeriasTableAdapter adpTPFerias =
                new DataSetPontoFrequenciaTableAdapters.TBTipoFeriasTableAdapter();
            adpTPFerias.FilleTurmalina(ds.TBTipoFerias, TipoAfastamento);

            if (ds.TBTipoFerias.Rows.Count == 0)
                return false; //Retorna falso pq não o motivo cadastrado no pontoweb.
            else
                IDTipoFerias = ds.TBTipoFerias[0].IDTPFerias;

            //Se for Inclusão segue adiante. Afastamento, segue abaixo.
            switch (TipoProcedimento)
            {
                case "I":
                    try
                    {
                        adpFerias.Insert(ds.TBVinculoUsuario[0].IDUsuario, 1, DTInicial, DTFinal, IDTipoFerias, ds.TBVinculoUsuario[0].IDVinculoUsuario);
                        freq.ManutencaoFrequenciaFeriasLicenca(ds.TBVinculoUsuario[0].IDUsuario, IDTipoFerias, DTInicial, DTFinal, "Inclusao", DTInicial, DTFinal, ds.TBVinculoUsuario[0].IDEmpresa, ds.TBVinculoUsuario[0].IDVinculoUsuario);
                        return true;
                    }
                    catch (Exception ex)
                    {
                        ///Construir log de excessões para essa integração
                        return false;
                    }
                    break;
                case "M":
                    try
                    {
                        //Primeiro, localiza se tem um afastamento.
                        adpFerias.FillByVerificaFeriasCorrente(ds.TBFerias, DTInicial, ds.TBVinculoUsuario[0].IDUsuario,
                            ds.TBVinculoUsuario[0].IDVinculoUsuario);
                        //se não achar, return false
                        if (ds.TBFerias.Rows.Count == 0)
                        {
                            return false;
                        }
                        else
                        {
                            adpFerias.Update(ds.TBVinculoUsuario[0].IDUsuario, 1, DTInicial, DTFinal,
                                IDTipoFerias, ds.TBVinculoUsuario[0].IDVinculoUsuario, ds.TBFerias[0].IDFerias);
                            //Abaixo duas datas de inicio e fim. A primeira com as que deve ficar, a segunda com as anteriores.
                            freq.ManutencaoFrequenciaFeriasLicenca(ds.TBFerias[0].IDUsuario, IDTipoFerias,
                                DTInicial, DTFinal, "Alteracao", ds.TBFerias[0].DTInicioFerias, ds.TBFerias[0].DTFimFerias, ds.TBVinculoUsuario[0].IDEmpresa, ds.TBVinculoUsuario[0].IDVinculoUsuario);
                            return true;
                        }
                    }
                    catch
                    {
                        return false;
                    }
                case "E":
                    try
                    {
                        Cadastro cad = new Cadastro();
                        //Primeiro, localiza se tem um afastamento.
                        adpFerias.FillByVerificaFeriasCorrente(ds.TBFerias, DTInicial, ds.TBVinculoUsuario[0].IDUsuario,
                            ds.TBVinculoUsuario[0].IDVinculoUsuario);

                        //se não achar, return false
                        if (ds.TBFerias.Rows.Count == 0)
                        {
                            try
                            {
                                LogOperacao log = new LogOperacao();
                                log.RegistraLog(010101, DateTime.Now, "InsertAfastamento", "Import Afastamento", string.Format("Usuario: {3} Per: {0} a {1} Result: {2} Op: {4}", DTInicial.ToShortDateString(), DTFinal.ToShortDateString(), "false", Matricula, TipoProcedimento), "", 36);
                            }
                            catch (Exception ex)
                            {
                                LogOperacao log = new LogOperacao();
                                log.RegistraLog(010101, DateTime.Now, "InsertAfastamento", "Import Afastamento", string.Format("Usuario: {3} Per: {0} a {1} Falha Exclu: {2} Op: {4}", DTInicial.ToShortDateString(), DTFinal.ToShortDateString(), ex.InnerException.ToString().Trim(), Matricula, TipoProcedimento), "", 36);
                                ex.ToString();
                            }
                            return false;
                        }
                        else
                        {
                            adpFerias.Delete(ds.TBFerias[0].IDFerias);
                            //Abaixo duas datas de inicio e fim. A primeira com as que deve ficar, a segunda com as anteriores.
                            freq.ManutencaoFrequenciaFeriasLicenca(ds.TBFerias[0].IDUsuario, IDTipoFerias,
                                DTInicial, DTFinal, "Exclusao", ds.TBFerias[0].DTInicioFerias, ds.TBFerias[0].DTFimFerias, ds.TBVinculoUsuario[0].IDEmpresa, ds.TBVinculoUsuario[0].IDVinculoUsuario);

                            return true;
                        }
                    }
                    catch (Exception ex)
                    {
                        return false;
                    }
                    break;

            }

            return false;
        }
        #endregion

        public string CadastraFerias(int IDUsuario, int Status, DateTime DTInicial, DateTime DTFinal, int IDTPFerias, int IDUsuarioOperador, int IDEmpresa, long IDVinculoUsuario)
        {
            PreencheTabela PT = new PreencheTabela();
            DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
            PT.PreencheTabelaFeriasIDUsuario(ds, IDUsuario, DTInicial, IDVinculoUsuario);

            if ((DTFinal < DTInicial) || (DTInicial == DTFinal))
            {
                msg = string.Format("A data final não poderá ser menor ou igual a data inicial. Repita o processo.");
            }
            else
            {
                if (ds.TBFerias.Rows.Count > 0)
                {
                    msg = string.Format("Há um evento cadastrado no período desejado. Selecione outro período.");
                }
                else
                {
                    try
                    {
                        adpFerias.Insert(IDUsuario, 1, DTInicial, DTFinal, IDTPFerias, IDVinculoUsuario);

                        log.RegistraLog(IDUsuarioOperador, System.DateTime.Now, adpFerias.Adapter.InsertCommand.ToString(), "Cadastro de férias", string.Format("Servidor: {0} Tipo: {1} Período: {2} - {3}", IDUsuario.ToString(), IDTPFerias.ToString(), DTInicial.ToShortDateString(), DTFinal.ToShortDateString()), "", IDEmpresa);

                        msg = "Registro Incluso com Sucesso.";
                    }
                    catch (Exception ex)
                    {
                        msg = "Falha ao incluir o registro. Tente Novamente.";
                        ex.ToString();
                    }
                }
            }

            return msg;
        }
        public string AlteraFerias(int IDUsuario, int Status, DateTime DTInicial, DateTime DTFinal, int IDFerias, int IDTPFerias, int IDUsuarioOperador, int IDEmpresa, long IDVinculoUsuario)
        {
            try
            {
                adpFerias.Update(IDUsuario, Status, DTInicial, DTFinal, IDTPFerias, IDVinculoUsuario, IDFerias);

                log.RegistraLog(IDUsuarioOperador, System.DateTime.Now, adpFerias.Adapter.UpdateCommand.ToString(), "Alteração de férias", string.Format("Servidor: {0} Tipo: {1} Período: {2} - {3}", IDUsuario.ToString(), IDTPFerias.ToString(), DTInicial.ToShortDateString(), DTFinal.ToShortDateString()), "", IDEmpresa);

                msg = "Registro Alterado com sucesso.";
            }
            catch (Exception ex)
            {
                msg = "Falha ao Alterar o registro.";
                ex.ToString();
            }
            return msg;
        }
        public string ExcluiFerias(int IDFerias, int IDUsuarioOperador, int IDEmpresa, DateTime DTInicio, DateTime DTFim, int IDUsuario, int IDTPFerias)
        {
            try
            {
                adpFerias.Delete(IDFerias);

                log.RegistraLog(IDUsuarioOperador, System.DateTime.Now, adpFerias.Adapter.DeleteCommand.ToString(), "Exclusão de férias", string.Format("Servidor: {0} Tipo: {1} Período: {2} - {3}", IDUsuario.ToString(), IDTPFerias.ToString(), DTInicio.ToShortDateString(), DTFim.ToShortDateString()), "", IDEmpresa);

                msg = string.Format("Registro excluído com sucesso.");
            }
            catch (Exception ex)
            {
                msg = string.Format("Houve Falha ao tentar excluir o registro. Tente novamente.");
                ex.ToString();
            }

            return msg;
        }
        #endregion

        #region Entidade
        public string CadastraEntidade(string Entidade, string vinculo)
        {
            try
            {
                adpEntidade.Insert(Entidade, 1, vinculo);
                msg = "Registro incluso com sucesso.";
            }
            catch (Exception ex)
            {
                ex.ToString();
                msg = "Falha ao incluir o registro. Tente Novamente.";
            }
            return msg;
        }
        public string AlteraEntidade(string Entidade, int IDEntidade, string vinculo)
        {
            try
            {
                adpEntidade.Update(Entidade, 1, vinculo, IDEntidade);
                msg = "Registro alterado com sucesso.";
            }
            catch (Exception ex)
            {
                ex.ToString();
                msg = "Falha ao alterar o registro. Tente novamente.";
            }
            return msg;
        }
        #endregion

        #region Empresa LOGO da Empresa
        public void ImgEmpresa(int IDempresa, Byte[] FOto)
        {
            DataSetPontoFrequenciaTableAdapters.TBEmpresaTableAdapter adpEmpresa =
                new DataSetPontoFrequenciaTableAdapters.TBEmpresaTableAdapter();
            adpEmpresa.UpdateImgEmpresa(FOto, IDempresa);
        }
        #region Empresa - Integração e-turmalina
        public bool InsertEmpresaOrgao(string Usuario, string Senha, string TipoProcedimento, string NomeEmpresaOrgao, int IDEmpresaOrgao_eTurmalina, string Sigla, int StatusOrgao)
        {
            try
            {
                LogOperacao log = new LogOperacao();
                log.RegistraLog(010101, DateTime.Now, "InsertEmpresaOrgao", "Import Orgao", NomeEmpresaOrgao, "", 36);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            if (!PermiteAcesso(Usuario, Senha))
                return false;

            DataSetPontoFrequenciaTableAdapters.TBEmpresaTableAdapter adpEmpresa =
                new DataSetPontoFrequenciaTableAdapters.TBEmpresaTableAdapter();

            try
            {
                switch (TipoProcedimento)
                {
                    case "I":
                        adpEmpresa.Insert(string.Empty, 1, "08:00", "14:00", "12:00", "18:00", "", "00:15", true, null, "01:00", false, string.Empty, Sigla, IDEmpresaOrgao_eTurmalina, NomeEmpresaOrgao);
                        break;
                    case "M":
                        adpEmpresa.UpdateeTurmalina("", StatusOrgao, "08:00", "14:00", "12:00", "18:00", "", "00:15", true, null, "01:00", false,
                            string.Empty, Sigla, IDEmpresaOrgao_eTurmalina);
                        break;
                }

                return true;
            }
            catch (Exception ex)
            {
                //Fazer um log de operações dessa integração.
                return false;
            }
        }
        #endregion
        #endregion

        #region Ocorrências
        //28/06/2018 -- INDISPONIBILIDADE E ERROS NOS CLIENTS
        public void InsertOcorrencia(DateTime dtOcorrencia, string DSOcorrencia, int IDEmpresa, int IDSetor,
            string IPLocal, string NomeMaquina, string VersaoClient)
        {
            DataSetPontoFrequenciaTableAdapters.TBOcorrenciaTableAdapter adpOcorrencia =
                new DataSetPontoFrequenciaTableAdapters.TBOcorrenciaTableAdapter();
            try
            {
                adpOcorrencia.Insert(dtOcorrencia, DSOcorrencia, IDEmpresa, IDSetor, IPLocal, VersaoClient, NomeMaquina);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        //------------
        #endregion

        //Import PMC
        public void ImportErroPMC(string CPF, string Erro)
        {
            DataSetPontoFrequenciaTableAdapters.TBImportErradoTableAdapter apdImportError =
                new DataSetPontoFrequenciaTableAdapters.TBImportErradoTableAdapter();
            apdImportError.Insert(CPF, Erro);
        }
    }
}
