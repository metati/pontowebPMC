using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace MetodosPontoFrequencia
{
    public class PreencheTabela
    {
        //mas uno teste
        public void Testanu()
        {
        }
        //solamiente un teste storedProcedure
        //public void ToTestando()
        //{
        //    try
        //    {
        //        DataSetREPTableAdapters.QueriesTableAdapter Query = new DataSetREPTableAdapters.QueriesTableAdapter();
        //        Query.spFechamentoFolhaUsuario(12247);
        //    }
        //    catch(Exception ex)
        //    {
        //        ex.ToString();
        //    }
        //}
        int cargo, setor;
        #region Auditoria
        public void GetAuditoria(DataSetPontoFrequencia ds, DateTime DTinicio, DateTime DTFim, string Nome, int IDEmpresa)
        {
            DataSetPontoFrequenciaTableAdapters.vwFrequenciaJustificativa_PedidoTableAdapter adpFrequencia =
    new DataSetPontoFrequenciaTableAdapters.vwFrequenciaJustificativa_PedidoTableAdapter();
            if (Nome != string.Empty)
            {

                adpFrequencia.FillIDEmpresaPeriodoNomeUsuario(ds.vwFrequenciaJustificativa_Pedido, IDEmpresa, DTinicio, DTFim, Nome);
            }
            else
            {
                adpFrequencia.FillIDEmpresaPeriodo(ds.vwFrequenciaJustificativa_Pedido, IDEmpresa, DTinicio, DTFim);
            }
        }


        public void GetInfoMaquinas(DataSetPontoFrequencia ds)
        {
            DataSetPontoFrequenciaTableAdapters.vwInfoClint_TableAdapter adpFrequencia =
    new DataSetPontoFrequenciaTableAdapters.vwInfoClint_TableAdapter();
            adpFrequencia.Fill(ds.vwInfoClint);
        }


        public void GetLogsMaquinas(DataSetPontoFrequencia ds)
        {
            DataSetPontoFrequenciaTableAdapters.vwLogConexoesClintTableAdapter adpFrequencia =
    new DataSetPontoFrequenciaTableAdapters.vwLogConexoesClintTableAdapter();
            adpFrequencia.Fill(ds.vwLogConexoesClint);
        }
        #endregion
        //ATENDE A INTEGRAÇÃO COM O E-TURMALINA
        #region Vínculo Usuario
        public void GetVinculoUsuario(DataSetPontoFrequencia ds, string Matricula)
        {
            DataSetPontoFrequenciaTableAdapters.TBVinculoUsuarioTableAdapter adpVinculo = new DataSetPontoFrequenciaTableAdapters.TBVinculoUsuarioTableAdapter();
            adpVinculo.FillMatricula(ds.TBVinculoUsuario, Matricula);
        }
        public void GetVinculoUsuario(DataSetPontoFrequencia ds, string Matricula, int IDUsuario)
        {
            DataSetPontoFrequenciaTableAdapters.TBVinculoUsuarioTableAdapter adpVinculo =
                new DataSetPontoFrequenciaTableAdapters.TBVinculoUsuarioTableAdapter();
            adpVinculo.FillMatricula_IDUsuario(ds.TBVinculoUsuario, Matricula, IDUsuario);
        }
        public int GetIDCargoVinculo(long IDVinculousuario)
        {
            DataSetPontoFrequenciaTableAdapters.TBVinculoUsuarioTableAdapter adpVinculo =
                new DataSetPontoFrequenciaTableAdapters.TBVinculoUsuarioTableAdapter();
            DataSetPontoFrequencia.TBVinculoUsuarioDataTable TBVinculo =
                new DataSetPontoFrequencia.TBVinculoUsuarioDataTable();
            try
            {
                adpVinculo.FillIDVinculoUsuario(TBVinculo, IDVinculousuario);
                return TBVinculo[0].IDCargo;
            }
            catch
            {
                return 0;
            }
        }
        #endregion

        #region Relatórios Descontos/Ausências

        public void PreenchevwFaltaInjustificada(DataSetPontoFrequencia ds, int IDEmpresa, DateTime Inicio, DateTime Fim)
        {
            DataSetPontoFrequenciaTableAdapters.vwFaltaInjustificadaTableAdapter adpFalta = new DataSetPontoFrequenciaTableAdapters.vwFaltaInjustificadaTableAdapter();
            adpFalta.Connection.Open();
            adpFalta.FillIDEmpresaData(ds.vwFaltaInjustificada, IDEmpresa, Inicio, Fim);
            adpFalta.Connection.Close();
        }
        public void PreenchevwFaltaInjustificada(DataSetPontoFrequencia ds, int IDEmpresa, int IDSetor, DateTime Inicio, DateTime Fim)
        {
            DataSetPontoFrequenciaTableAdapters.vwFaltaInjustificadaTableAdapter adpFalta = new DataSetPontoFrequenciaTableAdapters.vwFaltaInjustificadaTableAdapter();
            adpFalta.Connection.Open();
            adpFalta.FillIDEmpresaSetorData(ds.vwFaltaInjustificada, IDEmpresa, IDSetor, Inicio, Fim);
            adpFalta.Connection.Close();
        }
        public void PreenchevwFaltaInjustificada(DataSetPontoFrequencia ds, int IDEmpresa, int IDSetor, int IDVinculoUsuario, DateTime Inicio, DateTime Fim)
        {
            DataSetPontoFrequenciaTableAdapters.vwFaltaInjustificadaTableAdapter adpFalta = new DataSetPontoFrequenciaTableAdapters.vwFaltaInjustificadaTableAdapter();
            adpFalta.Connection.Open();
            adpFalta.FillIDEmpresaSetorUsuarioData(ds.vwFaltaInjustificada, IDEmpresa, IDSetor, IDVinculoUsuario, Inicio, Fim);
            adpFalta.Connection.Close();
        }
        public void PreenchevwRegistroAusente(DataSetPontoFrequencia ds, int IDEmpresa, DateTime Inicio, DateTime Fim)
        {
            DataSetPontoFrequenciaTableAdapters.vwRegistroAusenteTableAdapter adpRegAusente = new DataSetPontoFrequenciaTableAdapters.vwRegistroAusenteTableAdapter();
            adpRegAusente.Connection.Open();
            adpRegAusente.FillIDEmpresaData(ds.vwRegistroAusente, IDEmpresa, Inicio, Fim);
            adpRegAusente.Connection.Close();
        }
        public void PreenchevwRegistroAusente(DataSetPontoFrequencia ds, int IDEmpresa, int IDsetor, DateTime Inicio, DateTime Fim)
        {
            DataSetPontoFrequenciaTableAdapters.vwRegistroAusenteTableAdapter adpRegAusente = new DataSetPontoFrequenciaTableAdapters.vwRegistroAusenteTableAdapter();
            adpRegAusente.Connection.Open();
            adpRegAusente.FillIDEmpresaSetorData(ds.vwRegistroAusente, IDEmpresa, IDsetor, Inicio, Fim);
            adpRegAusente.Connection.Close();
        }
        public void PreenchevwRegistroAusente(DataSetPontoFrequencia ds, int IDEmpresa, int IDsetor, int IDVinculoUsuario, DateTime Inicio, DateTime Fim)
        {
            DataSetPontoFrequenciaTableAdapters.vwRegistroAusenteTableAdapter adpRegAusente = new DataSetPontoFrequenciaTableAdapters.vwRegistroAusenteTableAdapter();
            adpRegAusente.Connection.Open();
            adpRegAusente.FillIDEmpresaSetorUsuarioData(ds.vwRegistroAusente, IDEmpresa, IDsetor, IDVinculoUsuario, Inicio, Fim);
            adpRegAusente.Connection.Close();
        }
        public void PreenchevwDesconto(DataSetPontoFrequencia ds, int IDEmrpesa, DateTime Inicio, DateTime Fim)
        {
            DataSetPontoFrequenciaTableAdapters.vwDescontoTableAdapter adpDesconto = new DataSetPontoFrequenciaTableAdapters.vwDescontoTableAdapter();
            adpDesconto.Connection.Open();
            adpDesconto.FillIDEmpresaData(ds.vwDesconto, IDEmrpesa, Inicio, Fim);
            adpDesconto.Connection.Close();
        }
        public void PreenchevwDesconto(DataSetPontoFrequencia ds, int IDEmrpesa, int IDSetor, DateTime Inicio, DateTime Fim)
        {
            DataSetPontoFrequenciaTableAdapters.vwDescontoTableAdapter adpDesconto = new DataSetPontoFrequenciaTableAdapters.vwDescontoTableAdapter();
            adpDesconto.Connection.Open();
            adpDesconto.FillIDEmpresaSetorData(ds.vwDesconto, IDEmrpesa, IDSetor, Inicio, Fim);
            adpDesconto.Connection.Close();
        }
        public void PreenchevwDesconto(DataSetPontoFrequencia ds, int IDEmrpesa, int IDSetor, int IDVinculoUsuario, DateTime Inicio, DateTime Fim)
        {
            DataSetPontoFrequenciaTableAdapters.vwDescontoTableAdapter adpDesconto = new DataSetPontoFrequenciaTableAdapters.vwDescontoTableAdapter();
            adpDesconto.Connection.Open();
            adpDesconto.FillIDEmpresaSetorUsuarioData(ds.vwDesconto, IDEmrpesa, IDSetor, IDVinculoUsuario, Inicio, Fim);
            adpDesconto.Connection.Close();
        }
        public void PreenchevwDescontoFiltro(DataSetPontoFrequencia ds, int IDEmrpesa, DateTime Inicio, DateTime Fim, string Filtro)
        {
            DataSetPontoFrequenciaTableAdapters.vwDescontoTableAdapter adpDesconto = new DataSetPontoFrequenciaTableAdapters.vwDescontoTableAdapter();
            adpDesconto.Connection.Open();
            adpDesconto.FillIDEmpresaDataDesconto(ds.vwDesconto, IDEmrpesa, Inicio, Fim, Filtro);
            adpDesconto.Connection.Close();
        }
        public void PreenchevwDescontoFiltro(DataSetPontoFrequencia ds, int IDEmrpesa, int IDSetor, DateTime Inicio, DateTime Fim, string Filtro)
        {
            DataSetPontoFrequenciaTableAdapters.vwDescontoTableAdapter adpDesconto = new DataSetPontoFrequenciaTableAdapters.vwDescontoTableAdapter();
            adpDesconto.Connection.Open();
            adpDesconto.FillIDEmpresaSetorDataDesconto(ds.vwDesconto, IDEmrpesa, IDSetor, Inicio, Fim, Filtro);
            adpDesconto.Connection.Close();
        }
        public void PreenchevwDescontoFiltro(DataSetPontoFrequencia ds, int IDEmrpesa, int IDSetor, int IDVinculoUsuario, DateTime Inicio, DateTime Fim, string Filtro)
        {
            DataSetPontoFrequenciaTableAdapters.vwDescontoTableAdapter adpDesconto = new DataSetPontoFrequenciaTableAdapters.vwDescontoTableAdapter();
            adpDesconto.Connection.Open();
            adpDesconto.FillIDEmpresaSetorUsuarioDataDesconto(ds.vwDesconto, IDEmrpesa, IDSetor, IDVinculoUsuario, Filtro, Inicio, Fim);
            adpDesconto.Connection.Close();
        }
        #endregion

        #region Gráfico para Relatórios

        public string PreencheGraficoRelatorio(DataSetPontoFrequencia ds, string visao, string periodo, string IDTPUsuario, string TPGrafico)
        {
            string texto = string.Empty;
            string empresa = string.Empty;
            string diaMesAno = string.Empty;
            string idempresa = string.Empty;
            string setor = string.Empty;

            return "";
        }

        #endregion

        #region Empresa

        #region integração e-turmalina
        public void GetEmpresa(DataSetPontoFrequencia ds, int IDEmpresa_eTurmalina)
        {
            DataSetPontoFrequenciaTableAdapters.TBEmpresaTableAdapter adpEmpresa =
            new DataSetPontoFrequenciaTableAdapters.TBEmpresaTableAdapter();
            adpEmpresa.FillIDEturmalina(ds.TBEmpresa, IDEmpresa_eTurmalina);
        }
        #endregion

        private string NOMEEMPRESA;

        public string NomeEmpresa
        {
            get
            {
                return NOMEEMPRESA;
            }
        }

        public void PreencheTBEmpresa(DataSetPontoFrequencia ds)
        {
            DataSetPontoFrequenciaTableAdapters.TBEmpresaTableAdapter adpEmpresa = new DataSetPontoFrequenciaTableAdapters.TBEmpresaTableAdapter();
            adpEmpresa.Connection.Open();
            adpEmpresa.Fill(ds.TBEmpresa);
            adpEmpresa.Connection.Close();
        }
        public void PreencheTBEmpresaID(DataSetPontoFrequencia ds, int IDEmpresa)
        {
            DataSetPontoFrequenciaTableAdapters.TBEmpresaTableAdapter adpEmpresa = new DataSetPontoFrequenciaTableAdapters.TBEmpresaTableAdapter();
            adpEmpresa.Connection.Open();
            adpEmpresa.FillByIDEmpresa(ds.TBEmpresa, IDEmpresa);
            adpEmpresa.Connection.Close();

            if (!ds.TBEmpresa[0].IsDSEmpresaNull())
            {
                NOMEEMPRESA = ds.TBEmpresa[0].DSEmpresa.ToString();
            }

        }
        public void PreencheTBEmpresaAdmin(DataSetPontoFrequencia ds)
        {
            DataSetPontoFrequenciaTableAdapters.TBEmpresaTableAdapter adpEmpresa = new DataSetPontoFrequenciaTableAdapters.TBEmpresaTableAdapter();
            adpEmpresa.Connection.Open();
            adpEmpresa.FillByComboEmpresaDefalt(ds.TBEmpresa);
            adpEmpresa.Connection.Close();
        }
        #endregion

        #region Setor

        public void PreencheTBSetorGestor(DataSetPontoFrequencia ds, int IDUsuario, int IDEmpresa)
        {
            DataSetPontoFrequenciaTableAdapters.TBSetorTableAdapter adpSetor = new DataSetPontoFrequenciaTableAdapters.TBSetorTableAdapter();
            adpSetor.Connection.Open();
            adpSetor.FillGestorSetor(ds.TBSetor, IDEmpresa, IDUsuario);
            adpSetor.Connection.Close();
        }

        public void PreencheVWGestorSetor(DataSetPontoFrequencia ds, int IDEmpresa, int IDUsuario)
        {
            DataSetPontoFrequenciaTableAdapters.vwGestorSetorTableAdapter adpGestorSetor = new DataSetPontoFrequenciaTableAdapters.vwGestorSetorTableAdapter();
            adpGestorSetor.Connection.Open();
            adpGestorSetor.FillIDUsuario(ds.vwGestorSetor, IDEmpresa, IDUsuario);
            adpGestorSetor.Connection.Close();
        }

        public void PreencheTBSetor(DataSetPontoFrequencia ds)
        {
            DataSetPontoFrequenciaTableAdapters.TBSetorTableAdapter adpSetor = new DataSetPontoFrequenciaTableAdapters.TBSetorTableAdapter();
            adpSetor.Connection.Open();
            adpSetor.Fill(ds.TBSetor);
            adpSetor.Connection.Close();
        }
        public void PreencheTBSetorIDEmpresa(DataSetPontoFrequencia ds, int IDEmpresa)
        {
            DataSetPontoFrequenciaTableAdapters.TBSetorTableAdapter adpSetor = new DataSetPontoFrequenciaTableAdapters.TBSetorTableAdapter();
            adpSetor.Connection.Open();
            adpSetor.FillByIDEmpresa(ds.TBSetor, IDEmpresa);
            adpSetor.Connection.Close();
        }
        public void PreencheTBSetorIDEmpresaGeral(DataSetPontoFrequencia ds, int IDEmpresa)
        {
            DataSetPontoFrequenciaTableAdapters.TBSetorTableAdapter adpSetor = new DataSetPontoFrequenciaTableAdapters.TBSetorTableAdapter();
            adpSetor.Connection.Open();
            adpSetor.FillIDEmpresaGeral(ds.TBSetor, IDEmpresa);
            adpSetor.Connection.Close();
        }
        public void PreencheTBSetorIDSetor(DataSetPontoFrequencia ds, int IDSetor, int IDEmpresa)
        {
            DataSetPontoFrequenciaTableAdapters.TBSetorTableAdapter adpSetor = new DataSetPontoFrequenciaTableAdapters.TBSetorTableAdapter();
            adpSetor.Connection.Open();
            adpSetor.FillByIDSetor(ds.TBSetor, IDSetor, IDEmpresa);
            adpSetor.Connection.Close();
        }
        #endregion

        #region Mes
        public void PreencheTBMes(DataSetPontoFrequencia ds)
        {
            DataSetPontoFrequenciaTableAdapters.TBMesTableAdapter adpMes = new DataSetPontoFrequenciaTableAdapters.TBMesTableAdapter();
            adpMes.Connection.Open();
            adpMes.Fill(ds.TBMes);
            adpMes.Connection.Close();
        }
        #endregion

        #region Cargo
        public void PreencheTBCargo(DataSetPontoFrequencia ds)
        {
            DataSetPontoFrequenciaTableAdapters.TBCargoTableAdapter adpCargo = new DataSetPontoFrequenciaTableAdapters.TBCargoTableAdapter();
            adpCargo.Connection.Open();
            adpCargo.FillCargo(ds.TBCargo);
            adpCargo.Connection.Close();
        }
        #endregion

        #region Status
        public void PreencheTBStatus(DataSetPontoFrequencia ds)
        {
            DataSetPontoFrequenciaTableAdapters.TBStatusTableAdapter adpStatus = new DataSetPontoFrequenciaTableAdapters.TBStatusTableAdapter();
            adpStatus.Connection.Open();
            adpStatus.Fill(ds.TBStatus);
            adpStatus.Connection.Close();
        }
        #endregion

        #region Entidade
        public void PreencheTBEntidade(DataSetPontoFrequencia ds)
        {
            DataSetPontoFrequenciaTableAdapters.TBEntidadeTableAdapter adpEntidade = new DataSetPontoFrequenciaTableAdapters.TBEntidadeTableAdapter();
            adpEntidade.Connection.Open();
            adpEntidade.FillEntidade(ds.TBEntidade);
            adpEntidade.Connection.Close();
        }
        #endregion

        #region DiasAno

        public DataSetPontoFrequencia.TBDiasAnoDataTable FeriadosSAD()
        {
            DataSetPontoFrequencia.TBDiasAnoDataTable TBDiasAno = new DataSetPontoFrequencia.TBDiasAnoDataTable();
            DataSetPontoFrequenciaTableAdapters.TBDiasAnoTableAdapter adpDiaAno = new DataSetPontoFrequenciaTableAdapters.TBDiasAnoTableAdapter();
            adpDiaAno.Connection.Open();
            adpDiaAno.FillIDEmpresa(TBDiasAno, 45);
            adpDiaAno.Connection.Close();

            return TBDiasAno;
        }
        public void GetDiasMesCorrente(DataSetPontoFrequencia dsP, int IDEmpresa, int Mes, int Ano, int IDVinculoUsuario)
        {
            DataSetPontoFrequencia.TBDiasAnoDataTable TBDiasAno = new DataSetPontoFrequencia.TBDiasAnoDataTable();
            DataSetPontoFrequenciaTableAdapters.TBDiasAnoTableAdapter adpDiaAno = new DataSetPontoFrequenciaTableAdapters.TBDiasAnoTableAdapter();
            adpDiaAno.Connection.Open();
            adpDiaAno.FillMesCorrenteLimiteData(dsP.TBDiasAno, IDEmpresa, Mes, Ano, IDVinculoUsuario);
            adpDiaAno.Connection.Close();
        }

        public void GetDiasMesCorrente(DataSetPontoFrequencia dsP, int IDEmpresa, int Mes, int Ano)
        {
            DataSetPontoFrequencia.TBDiasAnoDataTable TBDiasAno = new DataSetPontoFrequencia.TBDiasAnoDataTable();
            DataSetPontoFrequenciaTableAdapters.TBDiasAnoTableAdapter adpDiaAno = new DataSetPontoFrequenciaTableAdapters.TBDiasAnoTableAdapter();
            adpDiaAno.Connection.Open();
            adpDiaAno.FillMesCorrenteEmpresa(dsP.TBDiasAno, IDEmpresa, Mes, Ano);
            adpDiaAno.Connection.Close();
        }


        public bool Permitealteracao(DataSetPontoFrequencia ds, string data, int IDEmpresa)
        {
            DataSetPontoFrequenciaTableAdapters.TBDiasAnoTableAdapter adpDias = new DataSetPontoFrequenciaTableAdapters.TBDiasAnoTableAdapter();

            adpDias.Connection.Open();
            adpDias.FillData(ds.TBDiasAno, data, IDEmpresa);
            adpDias.Connection.Close();

            if (ds.TBDiasAno.Rows.Count > 0)
                return true;
            else
                return false;
        }
        #endregion

        #region DadosFTP

        public void dd(System.Data.IDataAdapter da)
        {
        }

        public void PreencheUltimaVersaoClientBiometria(DataSetUsuario dsU, int IDEmpresa)
        {
            DataSetUsuarioTableAdapters.TBConfiguracaoFTPTableAdapter adpConfFTP = new DataSetUsuarioTableAdapters.TBConfiguracaoFTPTableAdapter();

            try
            {
                adpConfFTP.Connection.Open();
                adpConfFTP.FillIDEmpresa(dsU.TBConfiguracaoFTP, IDEmpresa);
                adpConfFTP.Connection.Close();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        #endregion

        public void PreencheTipoUsuarioFunc(DataSetPontoFrequencia ds)
        {
            DataSetPontoFrequenciaTableAdapters.TBTipoUsuarioTableAdapter adpTpUSer = new DataSetPontoFrequenciaTableAdapters.TBTipoUsuarioTableAdapter();
            adpTpUSer.Connection.Open();
            adpTpUSer.FillFuncionario(ds.TBTipoUsuario);
            adpTpUSer.Connection.Close();
        }
        public void PreencheTipoUsuarioAdminOrgao(DataSetPontoFrequencia ds)
        {
            DataSetPontoFrequenciaTableAdapters.TBTipoUsuarioTableAdapter adpTpUSer = new DataSetPontoFrequenciaTableAdapters.TBTipoUsuarioTableAdapter();
            adpTpUSer.Connection.Open();
            adpTpUSer.FillAdminOrgao(ds.TBTipoUsuario);
            adpTpUSer.Connection.Close();
        }


        //Integração com o SIG
        #region Integração SIG

        public void PreenchevwHorasDias(DataSetPontoFrequencia ds, int IDCliente, string cpfLogin, string DTFrequencia)
        {
            DataSetPontoFrequenciaTableAdapters.vWHorasDiaTableAdapter adpVwHoras = new DataSetPontoFrequenciaTableAdapters.vWHorasDiaTableAdapter();
            try
            {
                adpVwHoras.FillClienteUsuarioDia(ds.vWHorasDia, IDCliente, cpfLogin, DTFrequencia);
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                ex.ToString();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        public void PreenchevwHorasMesAno(DataSetPontoFrequencia ds, int IDCliente, int MesRef, int AnoRef)
        {
            DataSetPontoFrequenciaTableAdapters.vWHorasDiaTableAdapter adpVwHoras = new DataSetPontoFrequenciaTableAdapters.vWHorasDiaTableAdapter();
            try
            {
                adpVwHoras.FillClienteUsuarioMesAno(ds.vWHorasDia, IDCliente, MesRef, AnoRef);
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                ex.ToString();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        //public void PreenchevwHorasDias(DataSetPontoFrequencia ds, int IDCliente, string DTFrequencia)
        //{
        //    DataSetPontoFrequenciaTableAdapters.vWHorasDiaTableAdapter adpVwHoras = new DataSetPontoFrequenciaTableAdapters.vWHorasDiaTableAdapter();
        //    try
        //    {
        //        adpVwHoras.FillFreqDiaSIG(ds.vWHorasDia, IDCliente, DTFrequencia);
        //    }
        //    catch (System.Data.OleDb.OleDbException ex)
        //    {
        //        ex.ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ToString();
        //    }
        //}

        public void PreenchevwHorasDiasJust(DataSetPontoFrequencia ds, int IDCliente, string DTFrequencia)
        {
            DataSetPontoFrequenciaTableAdapters.vWHorasDiaTableAdapter adpVwHoras = new DataSetPontoFrequenciaTableAdapters.vWHorasDiaTableAdapter();
            try
            {
                //adpVwHoras.FillFreqDiaSIGJust(ds.vWHorasDia, IDCliente, DTFrequencia);
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                ex.ToString();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        //public void PreenchevwHorasDiasFalta(DataSetPontoFrequencia ds, int IDCliente, string DTFrequencia)
        //{
        //    DataSetPontoFrequenciaTableAdapters.vWHorasDiaTableAdapter adpVwHoras = new DataSetPontoFrequenciaTableAdapters.vWHorasDiaTableAdapter();
        //    try
        //    {
        //        adpVwHoras.FillFreqDiaSIGFalta(ds.vWHorasDia, IDCliente, DTFrequencia);
        //    }
        //    catch (System.Data.OleDb.OleDbException ex)
        //    {
        //        ex.ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.ToString();
        //    }
        //}


        #endregion
        //------------------

        #region Regime de Horas

        public void PreencheRegiomeHora(DataSetPontoFrequencia ds)
        {
            DataSetPontoFrequenciaTableAdapters.TBRegimeHoraTableAdapter adpRegimeHora = new DataSetPontoFrequenciaTableAdapters.TBRegimeHoraTableAdapter();
            adpRegimeHora.Connection.Open();
            adpRegimeHora.Fill(ds.TBRegimeHora);
            adpRegimeHora.Connection.Close();
        }

        #endregion


        #region Vinculo Usuário

        public int GetIDEmpresaVinculo(long IDVinculo)
        {
            DataSetPontoFrequencia.TBVinculoUsuarioDataTable TBVinculousuario = new DataSetPontoFrequencia.TBVinculoUsuarioDataTable();
            DataSetPontoFrequenciaTableAdapters.TBVinculoUsuarioTableAdapter adpTBVinculoUsuario = new DataSetPontoFrequenciaTableAdapters.TBVinculoUsuarioTableAdapter();

            try
            {
                adpTBVinculoUsuario.Connection.Open();
                adpTBVinculoUsuario.FillIDVinculoUsuario(TBVinculousuario, IDVinculo);
                adpTBVinculoUsuario.Connection.Close();

                if (TBVinculousuario.Rows.Count > 0)
                {
                    return TBVinculousuario[0].IDEmpresa;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }

        }


        public int GetIDEmpresaVinculoScalar(long IDVinculo)
        {

            int intEmpresa = 0;
            try
            {
                int.TryParse(Util.getScalar("SELECT IDEmpresa FROM TBVinculoUsuario WITH(NOLOCK) WHERE IDVinculoUsuario = " + IDVinculo.ToString()), out intEmpresa);
                return intEmpresa;
            }
            catch (Exception ex)
            {
                UtilLog.EscreveLog(DateTime.Now.ToLongDateString() + " ERRO GetIDEmpresaVinculoScalar " + ex.Message);
                return 0;
            }

        }

        public bool GetVerificaSeEstaDeFerias(int IdUsuario, long IDVinculo, DateTime dataBatida)
        {
            bool IsFerias = false;
            try
            {
                int intCount = 0;
                int.TryParse(Util.getScalar("SELECT COUNT(*) FROM TBFerias WHERE (IDVinculoUsuario = " + IDVinculo + ") AND (IDUsuario = " + IdUsuario + ") AND (CONVERT(DATETIME,'" + dataBatida.ToString("dd/MM/yyyy") + "',103) BETWEEN DTInicioFerias AND DTFimFerias)"), out intCount);
                IsFerias = (intCount > 0);
            }
            catch (Exception ex)
            {
                UtilLog.EscreveLog(DateTime.Now.ToLongDateString() + " ERRO GetVerificaSeEstaDeFerias " + ex.Message);
            }
            return IsFerias;
        }

        public long RetornaIDVinculoUsuario(int idempresa, int idsetor, int idusuario)
        {
            DataSetPontoFrequencia.TBVinculoUsuarioDataTable TBVinculousuario = new DataSetPontoFrequencia.TBVinculoUsuarioDataTable();
            DataSetPontoFrequenciaTableAdapters.TBVinculoUsuarioTableAdapter adpTBVinculoUsuario = new DataSetPontoFrequenciaTableAdapters.TBVinculoUsuarioTableAdapter();

            adpTBVinculoUsuario.Connection.Open();
            adpTBVinculoUsuario.FillIDEmpresaSetorUsuario(TBVinculousuario, idempresa, idsetor, idusuario);
            adpTBVinculoUsuario.Connection.Close();

            if (TBVinculousuario.Rows.Count > 0)
                return TBVinculousuario[0].IDVinculoUsuario;
            else
                return 0;
        }

        public long RetornaIDVinculoUsuarioAtivo(int idempresa, int idsetor, int idusuario)
        {
            DataSetPontoFrequencia.TBVinculoUsuarioDataTable TBVinculousuario = new DataSetPontoFrequencia.TBVinculoUsuarioDataTable();
            DataSetPontoFrequenciaTableAdapters.TBVinculoUsuarioTableAdapter adpTBVinculoUsuario = new DataSetPontoFrequenciaTableAdapters.TBVinculoUsuarioTableAdapter();

            adpTBVinculoUsuario.Connection.Open();
            adpTBVinculoUsuario.FillIDEmpresaSetorUsuarioAtivo(TBVinculousuario, idempresa, idsetor, idusuario);
            adpTBVinculoUsuario.Connection.Close();

            if (TBVinculousuario.Rows.Count > 0)
                return TBVinculousuario[0].IDVinculoUsuario;
            else
                return 0;
        }

        public void PreenchevwVinculoUsuarioEmpresa(DataSetPontoFrequencia ds, int IDUsuario, int IDEmpresa)
        {
            ds.EnforceConstraints = false;
            DataSetPontoFrequenciaTableAdapters.vwVinculoUsuarioTableAdapter adpVinculoUsuario = new DataSetPontoFrequenciaTableAdapters.vwVinculoUsuarioTableAdapter();
            adpVinculoUsuario.Connection.Open();
            adpVinculoUsuario.FillUsuarioEmpresa(ds.vwVinculoUsuario, IDUsuario, IDEmpresa);
            adpVinculoUsuario.Connection.Close();
        }
        public void PreenchevwVinculoUsuarioEmpresa(DataSetPontoFrequencia ds, int IDVinculoUsuario)
        {
            ds.EnforceConstraints = false;
            DataSetPontoFrequenciaTableAdapters.vwVinculoUsuarioTableAdapter adpVinculoUsuario = new DataSetPontoFrequenciaTableAdapters.vwVinculoUsuarioTableAdapter();
            adpVinculoUsuario.Connection.Open();
            adpVinculoUsuario.FillIDVinculo(ds.vwVinculoUsuario, IDVinculoUsuario);
            adpVinculoUsuario.Connection.Close();
        }

        public void PreenchevwVinculoUsuarioEmpresa(DataSetPontoFrequencia ds, int IDUsuario, int IDEmpresa, int IDSetor)
        {
            ds.EnforceConstraints = false;
            DataSetPontoFrequenciaTableAdapters.vwVinculoUsuarioTableAdapter adpVinculoUsuario = new DataSetPontoFrequenciaTableAdapters.vwVinculoUsuarioTableAdapter();
            adpVinculoUsuario.Connection.Open();
            adpVinculoUsuario.FillIDUSuarioVinculoEmpresa(ds.vwVinculoUsuario, IDUsuario, IDSetor, IDEmpresa);
            adpVinculoUsuario.Connection.Close();
        }

        public bool PermitePreenchevwVinculoUsuarioEmpresa(int IDUsuario, int IDEmpresa, int IDSetor)
        {
            DataSetPontoFrequencia dsR = new DataSetPontoFrequencia();
            dsR.EnforceConstraints = false;
            DataSetPontoFrequenciaTableAdapters.vwVinculoUsuarioTableAdapter adpVinculoUsuario = new DataSetPontoFrequenciaTableAdapters.vwVinculoUsuarioTableAdapter();
            adpVinculoUsuario.Connection.Open();
            adpVinculoUsuario.FillIDUSuarioVinculoEmpresa(dsR.vwVinculoUsuario, IDUsuario, IDSetor, IDEmpresa);
            adpVinculoUsuario.Connection.Close();

            if (dsR.vwVinculoUsuario.Rows.Count > 0)
                return false;
            else
                return true;
        }

        public void PreenchevwVinculoUsuario(DataSetPontoFrequencia ds, int IDusuario)
        {
            ds.EnforceConstraints = false;
            DataSetPontoFrequenciaTableAdapters.vwVinculoUsuarioTableAdapter adpvwVinculo = new DataSetPontoFrequenciaTableAdapters.vwVinculoUsuarioTableAdapter();
            adpvwVinculo.Connection.Open();
            adpvwVinculo.FillIDUsuario(ds.vwVinculoUsuario, IDusuario);
            adpvwVinculo.Connection.Close();
        }

        public int PreenchevwVinculoUsuarioInt(int IDusuario)
        {
            DataSetPontoFrequenciaTableAdapters.vwVinculoUsuarioTableAdapter adpvwVinculo = new DataSetPontoFrequenciaTableAdapters.vwVinculoUsuarioTableAdapter();
            DataSetPontoFrequencia.vwVinculoUsuarioDataTable vwVinculo = new DataSetPontoFrequencia.vwVinculoUsuarioDataTable();

            adpvwVinculo.Connection.Open();
            adpvwVinculo.FillIDUsuario(vwVinculo, IDusuario);
            adpvwVinculo.Connection.Close();

            if (vwVinculo.Rows.Count > 0)
                return vwVinculo[0].IDEmpresa;
            else
                return 0;
        }

        public void PreenchevwVinculoUsuarioIDVinculoUsuario(DataSetPontoFrequencia ds, int IDVInculoUsuario)
        {
            DataSetPontoFrequenciaTableAdapters.vwVinculoUsuarioTableAdapter adpvwVinculo = new DataSetPontoFrequenciaTableAdapters.vwVinculoUsuarioTableAdapter();
            adpvwVinculo.Connection.Open();
            adpvwVinculo.FillIDVinculo(ds.vwVinculoUsuario, IDVInculoUsuario);
            adpvwVinculo.Connection.Close();
        }

        public void PreenchevwVinculoUsuarioIDEmpresa(DataSetPontoFrequencia ds, int IDEmpresa)
        {
            DataSetPontoFrequenciaTableAdapters.vwVinculoUsuarioTableAdapter adpvwVinculo = new DataSetPontoFrequenciaTableAdapters.vwVinculoUsuarioTableAdapter();
            adpvwVinculo.Connection.Open();
            adpvwVinculo.FillIDEmpresa(ds.vwVinculoUsuario, IDEmpresa);
            adpvwVinculo.Connection.Close();
        }

        #endregion

        #region Usuario

        //03/07/2018 - Pega os usuários para o gráfico inicial.
        public void GetvwUsuarioGridEmpresaSetorAtivos(DataSetPontoFrequencia dsP, int IDEmpresa, int IDSetor)
        {
            DataSetPontoFrequenciaTableAdapters.vwUsuariogridTableAdapter adpGrid =
                new DataSetPontoFrequenciaTableAdapters.vwUsuariogridTableAdapter();
            adpGrid.Connection.Open();
            adpGrid.FillIDEmpresaSetor(dsP.vwUsuariogrid, IDEmpresa, IDSetor);
            adpGrid.Connection.Close();
        }

        public void PreenchevwUsuarioGestao(DataSetPontoFrequencia ds, int IDEmpresa)
        {
            DataSetPontoFrequenciaTableAdapters.vwUsuarioGestaoTableAdapter adpUserGestao = new DataSetPontoFrequenciaTableAdapters.vwUsuarioGestaoTableAdapter();
            adpUserGestao.Connection.Open();
            adpUserGestao.FillIDEmpresa(ds.vwUsuarioGestao, IDEmpresa);
            adpUserGestao.Connection.Close();
        }

        public void PreenchevwUsuarioGestao(DataSetPontoFrequencia ds, int IDEmpresa, int IDSetor)
        {
            DataSetPontoFrequenciaTableAdapters.vwUsuarioGestaoTableAdapter adpUserGestao = new DataSetPontoFrequenciaTableAdapters.vwUsuarioGestaoTableAdapter();
            adpUserGestao.Connection.Open();
            adpUserGestao.FillIDEmpresaSetor(ds.vwUsuarioGestao, IDEmpresa, IDSetor);
            adpUserGestao.Connection.Close();
        }

        public void PreenchevwUsuarioGestao(DataSetPontoFrequencia ds, int IDEmpresa, int IDSetor, long IDVinculoUsuario)
        {
            DataSetPontoFrequenciaTableAdapters.vwUsuarioGestaoTableAdapter adpUserGestao = new DataSetPontoFrequenciaTableAdapters.vwUsuarioGestaoTableAdapter();
            adpUserGestao.Connection.Open();
            adpUserGestao.FillIDEmpresaSetorUsuario(ds.vwUsuarioGestao, IDEmpresa, IDSetor, IDVinculoUsuario);
            adpUserGestao.Connection.Close();
        }

        public void PreenchevwUsuarioGridAdminSetor(DataSetPontoFrequencia ds, int IDempresa, int IDSetor)
        {
            DataSetPontoFrequenciaTableAdapters.vwUsuarioGridAdminTableAdapter adpUsuarioGridAdmin = new DataSetPontoFrequenciaTableAdapters.vwUsuarioGridAdminTableAdapter();
            ds.EnforceConstraints = false;
            adpUsuarioGridAdmin.Connection.Open();
            adpUsuarioGridAdmin.FillIDEmpresaSetorAdmin(ds.vwUsuarioGridAdmin, IDempresa, IDSetor);
            adpUsuarioGridAdmin.Connection.Close();
        }

        public void PreenchevwUsuarioGridAdminOrgaoSetor(DataSetPontoFrequencia ds, int IDempresa, int IDSetor)
        {
            DataSetPontoFrequenciaTableAdapters.vwUsuarioGridAdminTableAdapter adpUsuarioGridAdmin = new DataSetPontoFrequenciaTableAdapters.vwUsuarioGridAdminTableAdapter();
            ds.EnforceConstraints = false;
            adpUsuarioGridAdmin.Connection.Open();
            adpUsuarioGridAdmin.FillIDEmpresaSetorAdminOrgao(ds.vwUsuarioGridAdmin, IDempresa, IDSetor);
            adpUsuarioGridAdmin.Connection.Close();
        }

        public void PreenchevwUsuarioGridAdminGestor(DataSetPontoFrequencia ds, int IDempresa, int IDSetor)
        {
            DataSetPontoFrequenciaTableAdapters.vwUsuarioGridAdminTableAdapter adpUsuarioGridAdmin = new DataSetPontoFrequenciaTableAdapters.vwUsuarioGridAdminTableAdapter();
            ds.EnforceConstraints = false;
            adpUsuarioGridAdmin.Connection.Open();
            adpUsuarioGridAdmin.FillIDEmpresaSetor(ds.vwUsuarioGridAdmin, IDempresa, IDSetor);
            adpUsuarioGridAdmin.Connection.Close();
        }

        public void PreenchevwUsuarioGridAdmin(DataSetPontoFrequencia ds, int IDempresa)
        {
            DataSetPontoFrequenciaTableAdapters.vwUsuarioGridAdminTableAdapter adpUsuarioGridAdmin = new DataSetPontoFrequenciaTableAdapters.vwUsuarioGridAdminTableAdapter();
            ds.EnforceConstraints = false;
            adpUsuarioGridAdmin.Connection.Open();
            adpUsuarioGridAdmin.FillIDEmpresa(ds.vwUsuarioGridAdmin, IDempresa);
            adpUsuarioGridAdmin.Connection.Close();
        }

        public void PreenchevwUsuarioGridAdminOrgao(DataSetPontoFrequencia ds, int IDempresa)
        {
            DataSetPontoFrequenciaTableAdapters.vwUsuarioGridAdminTableAdapter adpUsuarioGridAdmin = new DataSetPontoFrequenciaTableAdapters.vwUsuarioGridAdminTableAdapter();
            ds.EnforceConstraints = false;
            adpUsuarioGridAdmin.Connection.Open();
            adpUsuarioGridAdmin.FillIDEmpresaAdminOrgao(ds.vwUsuarioGridAdmin, IDempresa);
            //adpUsuarioGridAdmin.FillAdminOrgao(ds.vwUsuarioGridAdmin, IDempresa);
            adpUsuarioGridAdmin.Connection.Close();
        }


        public void PreenchevwUsuarioGrid_Gestor(DataSetPontoFrequencia ds, int IDUsuario, int IDSetor, int IDEmpresa)
        {
            DataSetPontoFrequenciaTableAdapters.vwUsuariogridTableAdapter adpUserGrid = new DataSetPontoFrequenciaTableAdapters.vwUsuariogridTableAdapter();
            ds.EnforceConstraints = false;
            adpUserGrid.Connection.Open();
            adpUserGrid.FillIDEmpresaSetorUsuario(ds.vwUsuariogrid, IDEmpresa, IDSetor, IDUsuario);
            adpUserGrid.Connection.Close();
        }

        public void PreenchevwUsuarioGridNomeGeral(DataSetPontoFrequencia ds, string Nome)
        {
            DataSetPontoFrequenciaTableAdapters.vwUsuariogridTableAdapter adpUserGrid = new DataSetPontoFrequenciaTableAdapters.vwUsuariogridTableAdapter();
            try
            {
                ds.EnforceConstraints = false;
                adpUserGrid.Connection.Open();
                adpUserGrid.FillNomeUsuarioGeral(ds.vwUsuariogrid, Nome);
                adpUserGrid.Connection.Close();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        public void PreenchevwUsuarioGridCPFGeral(DataSetPontoFrequencia ds, string CPF)
        {
            DataSetPontoFrequenciaTableAdapters.vwUsuariogridTableAdapter adpUserGrid = new DataSetPontoFrequenciaTableAdapters.vwUsuariogridTableAdapter();
            try
            {
                ds.EnforceConstraints = false;
                adpUserGrid.Connection.Open();
                adpUserGrid.FillLoginGeral(ds.vwUsuariogrid, CPF);
                adpUserGrid.Connection.Close();

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        //web service

        public void PreenchevwUsuarioWebServiceIDUsuario(DataSetUsuario dsU, int IDempresa, int IDUsuario)
        {
            dsU.EnforceConstraints = false;
            DataSetUsuarioTableAdapters.vwUsuarioWebServiceFotoTableAdapter adpWV = new DataSetUsuarioTableAdapters.vwUsuarioWebServiceFotoTableAdapter();
            adpWV.Connection.Open();
            adpWV.FillIDUsuario(dsU.vwUsuarioWebServiceFoto, IDUsuario, IDempresa);
            adpWV.Connection.Close();
        }

        public void PreenchevwUsuarioWebService(DataSetUsuario dsU, int IDempresa, string PIS)
        {
            dsU.EnforceConstraints = false;
            DataSetUsuarioTableAdapters.vwUsuarioWebServiceTableAdapter adpWV = new DataSetUsuarioTableAdapters.vwUsuarioWebServiceTableAdapter();
            adpWV.Connection.Open();
            adpWV.FillPIS(dsU.vwUsuarioWebService, PIS, IDempresa);
            adpWV.Connection.Close();
        }

        public void PreenchevwUsuarioWebService(DataSetUsuario dsU, int IDempresa)
        {
            dsU.EnforceConstraints = false;
            int gruporegistro;
            DataSetUsuarioTableAdapters.vwUsuarioWebServiceTableAdapter adpWV = new DataSetUsuarioTableAdapters.vwUsuarioWebServiceTableAdapter();

            //Verificar se empresa faz parte de um grupo
            gruporegistro = GrupoRegistro(IDempresa);

            adpWV.Connection.Open();

            if (gruporegistro == 0)
            {

                adpWV.FillIDEmpresa(dsU.vwUsuarioWebService, IDempresa);
            }
            else
                adpWV.FillIDGrupoRegistro(dsU.vwUsuarioWebService, gruporegistro);

            adpWV.Connection.Close();

        }

        public void PreenchevwUsuarioWebServiceHash2(DataSetUsuario dsU, int IDempresa, string HashMaquina)
        {
            dsU.EnforceConstraints = false;
            int gruporegistro;
            DataSetUsuarioTableAdapters.vwUsuarioWebServiceTableAdapter adpWV = new DataSetUsuarioTableAdapters.vwUsuarioWebServiceTableAdapter();

            //Verificar se empresa faz parte de um grupo
            gruporegistro = GrupoRegistro(IDempresa);

            adpWV.Connection.Open();

            adpWV.FillIDEmpresaByHash(dsU.vwUsuarioWebService, IDempresa, HashMaquina);

            adpWV.Connection.Close();

        }

        public void PreenchevwUsuarioWebServiceHash3(DataSetUsuario dsU, int IDempresa, string HashMaquina)
        {
            dsU.EnforceConstraints = false;
            int gruporegistro;
            DataSetUsuarioTableAdapters.vwUsuarioWebServiceTableAdapter adpWV = new DataSetUsuarioTableAdapters.vwUsuarioWebServiceTableAdapter();

            //Verificar se empresa faz parte de um grupo
            gruporegistro = GrupoRegistro(IDempresa);

            adpWV.Connection.Open();
            try
            {

                //StringBuilder sb = new StringBuilder();
                //sb.AppendLine("EXEC dbo.spGetUsuariosPontoHash " + IDempresa + ", '" + HashMaquina + "';");
                //var select = adpWV.Connection.CreateCommand();
                //select.CommandText = sb.ToString();
                //adpWV.Adapter.SelectCommand = select;
                //adpWV.Adapter.SelectCommand.ExecuteReader();
                //if (gruporegistro == 0)
                //{

                //    adpWV.FillIDEmpresa(dsU.vwUsuarioWebService, IDempresa);
                //}
                //else
                //    adpWV.FillIDGrupoRegistro(dsU.vwUsuarioWebService, gruporegistro);

                //dsU.vwUsuarioWebService.Clear();
                adpWV.FillIDEmpresaByHash(dsU.vwUsuarioWebService, IDempresa, HashMaquina);

                //adpWV.Fill(dsU.vwUsuarioWebService);

                //adpWV.Adapter.Update(dsU.vwUsuarioWebService);
                //SqlDataReader sqlData = adpWV.Adapter.SelectCommand.ExecuteReader();
                //dsU.vwUsuarioWebService.Load(sqlData);
                adpWV.Connection.Close();
            }
            catch
            {
                adpWV.Connection.Close();
            }


        }

        public void PreenchevwUsuarioWebServiceHash(DataSetUsuario dsU, int IDempresa, string HashMaquina)
        {
            dsU.EnforceConstraints = false;
            int gruporegistro;
            DataSetUsuarioTableAdapters.vwUsuarioWebServiceTableAdapter adpWV = new DataSetUsuarioTableAdapters.vwUsuarioWebServiceTableAdapter();

            //Verificar se empresa faz parte de um grupo
            gruporegistro = GrupoRegistro(IDempresa);

            adpWV.Connection.Open();
            try
            {

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("EXEC dbo.spGetUsuariosPontoHash " + IDempresa + ", '" + HashMaquina + "';");
                var select = adpWV.Connection.CreateCommand();
                select.CommandText = sb.ToString();
                adpWV.Adapter.SelectCommand = select;
                //adpWV.Adapter.SelectCommand.ExecuteReader();
                //if (gruporegistro == 0)
                //{

                //    adpWV.FillIDEmpresa(dsU.vwUsuarioWebService, IDempresa);
                //}
                //else
                //    adpWV.FillIDGrupoRegistro(dsU.vwUsuarioWebService, gruporegistro);

                dsU.vwUsuarioWebService.Clear();

                //adpWV.Fill(dsU.vwUsuarioWebService);

                //adpWV.Adapter.Update(dsU.vwUsuarioWebService);
                SqlDataReader sqlData = adpWV.Adapter.SelectCommand.ExecuteReader();
                dsU.vwUsuarioWebService.Load(sqlData);
                adpWV.Connection.Close();
            }
            catch
            {
                adpWV.Connection.Close();
            }
            

        }


        public void PreenchevwUsuarioWebServiceNome(DataSetUsuario dsU, int IDempresa, string Usuario)
        {
            int gruporegistro;
            DataSetUsuarioTableAdapters.vwUsuarioWebServiceTableAdapter adpWV = new DataSetUsuarioTableAdapters.vwUsuarioWebServiceTableAdapter();

            //Verificar se empresa faz parte de um grupo
            gruporegistro = GrupoRegistro(IDempresa);

            dsU.EnforceConstraints = false;

            adpWV.Connection.Open();

            if (gruporegistro == 0)
            {
                adpWV.FillDSUsuario(dsU.vwUsuarioWebService, IDempresa, Usuario);
            }
            else
                adpWV.FillDSUsuarioGrupoRegistro(dsU.vwUsuarioWebService, Usuario, gruporegistro);

            adpWV.Connection.Close();
        }

        public void PreenchevwUsuarioWebServiceLogin(DataSetUsuario dsU, int IDempresa, string Login)
        {
            int gruporegistro;
            DataSetUsuarioTableAdapters.vwUsuarioWebServiceTableAdapter adpWV = new DataSetUsuarioTableAdapters.vwUsuarioWebServiceTableAdapter();
            //Verificar se empresa faz parte de um grupo
            gruporegistro = GrupoRegistro(IDempresa);

            dsU.EnforceConstraints = false;

            adpWV.Connection.Open();

            if (gruporegistro == 0)
            {
                adpWV.FillLogin(dsU.vwUsuarioWebService, Login, IDempresa);
            }
            else
                adpWV.FilllLoginGrupoRegistro(dsU.vwUsuarioWebService, Login, gruporegistro);

            adpWV.Connection.Close();

        }

        public void PreenchevwUsuarioWebServiceLogar2(DataSetUsuario dsU, int IDempresa, string Login, string Senha)
        {
            DataSetUsuarioTableAdapters.vwUsuarioWebServiceTableAdapter adpWV = new DataSetUsuarioTableAdapters.vwUsuarioWebServiceTableAdapter();

            dsU.EnforceConstraints = false;
            Cript cript = new Cript();


            try
            {
                adpWV.Connection.Open();
                adpWV.FillLogar2(dsU.vwUsuarioWebService, Login, cript.ActionEncrypt(Senha));

                if (dsU.vwUsuarioWebService.Rows.Count == 0)
                    adpWV.FillLogarGrupoRegistro2(dsU.vwUsuarioWebService, Login, cript.ActionEncrypt(Senha), IDempresa);

                adpWV.Connection.Close();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        public void PreenchevwUsuarioWebServiceLogar(DataSetUsuario dsU, int IDempresa, string Login, string Senha)
        {
            DataSetUsuarioTableAdapters.vwUsuarioWebServiceTableAdapter adpWV = new DataSetUsuarioTableAdapters.vwUsuarioWebServiceTableAdapter();

            DataSetPontoFrequenciaTableAdapters.TBUsuarioTableAdapter adpUsario = new DataSetPontoFrequenciaTableAdapters.TBUsuarioTableAdapter();
            DataSetPontoFrequencia.TBUsuarioDataTable TBuser = new DataSetPontoFrequencia.TBUsuarioDataTable();
            adpUsario.FillLoginSenhaAdmin(TBuser, Login, Senha);

            if (TBuser.Rows.Count > 0)
            {
                dsU.EnforceConstraints = false;
                adpWV.Filldologin(dsU.vwUsuarioWebService, TBuser[0].IDUsuario, IDempresa);

                if (dsU.vwUsuarioWebService.Rows.Count == 0)
                {
                    adpWV.FilldoLoginAdmin(dsU.vwUsuarioWebService, TBuser[0].IDUsuario);
                }
            }
            //try
            //{
            //    adpWV.Connection.Open();
            //    adpWV.FillLogar3(dsU.vwUsuarioWebService, Login, Senha);

            //    if(dsU.vwUsuarioWebService.Rows.Count == 0)
            //        adpWV.FillLogarGrupoRegistro(dsU.vwUsuarioWebService, Login, Senha, IDempresa);

            //    adpWV.Connection.Close();
            //}
            //catch (Exception ex)
            //{
            //    ex.ToString();
            //}
        }

        public void PreencheTBUsuarioLogin(DataSetPontoFrequencia dsU, string Login)
        {
            DataSetPontoFrequenciaTableAdapters.TBUsuarioTableAdapter adpUser = new DataSetPontoFrequenciaTableAdapters.TBUsuarioTableAdapter();
            adpUser.Connection.Open();
            adpUser.FillLogin(dsU.TBUsuario, Login);
            adpUser.Connection.Close();
        }

        //Maior Ref REP
        public string UltimaRefREPCadastrada(DataSetUsuario dsU, int IDempresa)
        {
            string MAnior = "";
            DataSetUsuarioTableAdapters.vwMaiorRefREPTableAdapter adpVWMaior = new DataSetUsuarioTableAdapters.vwMaiorRefREPTableAdapter();
            adpVWMaior.Connection.Open();
            adpVWMaior.FillIDEmpresa(dsU.vwMaiorRefREP, IDempresa);

            adpVWMaior.Connection.Close();

            if (dsU.vwMaiorRefREP.Rows.Count > 0)
            {
                MAnior = Convert.ToString(dsU.vwMaiorRefREP[0].MaiorRefREP);
            }
            else
            {
                MAnior = "0";
            }

            return MAnior;
        }


        //--------

        /// <summary>
        /// Que não está cadastrdo no REP por Empresa
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="IDEmpresa"></param>
        /// 

        public void PreencheVWUsuarioWebServiceREP(DataSetUsuario ds, int IDEmpresa)
        {
            DataSetUsuarioTableAdapters.vwUsuarioWebServiceTableAdapter adpUser = new DataSetUsuarioTableAdapters.vwUsuarioWebServiceTableAdapter();
            adpUser.Connection.Open();
            adpUser.FillRefREP(ds.vwUsuarioWebService, IDEmpresa);
            adpUser.Connection.Close();
        }

        public void PreencheVWUsuarioWebServiceREP(DataSetUsuario ds, int IDEmpresa, int IDREP)
        {
            DataSetUsuarioTableAdapters.vwUsuarioWebServiceTableAdapter adpUser = new DataSetUsuarioTableAdapters.vwUsuarioWebServiceTableAdapter();
            adpUser.Connection.Open();
            adpUser.FillREPEmpresa(ds.vwUsuarioWebService, IDREP, IDEmpresa);
            adpUser.Connection.Close();
        }

        public void PreencheVWUsuarioWebServiceREPGeral(DataSetUsuario ds, int IDEmpresa, int IDREP)
        {
            DataSetUsuarioTableAdapters.vwUsuarioWebServiceTableAdapter adpUser = new DataSetUsuarioTableAdapters.vwUsuarioWebServiceTableAdapter();
            adpUser.Connection.Open();
            adpUser.FillREPEmpresaGeral(ds.vwUsuarioWebService, IDEmpresa, IDREP);
            adpUser.Connection.Close();
        }

        public void PreenchevwUsuario(DataSetPontoFrequencia ds, int IDEmpresa)
        {
            DataSetPontoFrequenciaTableAdapters.vwUsuariogridTableAdapter adpVWusuario = new DataSetPontoFrequenciaTableAdapters.vwUsuariogridTableAdapter();
            ds.EnforceConstraints = false;
            adpVWusuario.Connection.Open();
            adpVWusuario.Fill(ds.vwUsuariogrid, IDEmpresa);
            adpVWusuario.Connection.Close();
        }
        public void PreenchevwUsuarioAdminOrgao(DataSetPontoFrequencia ds, int IDEmpresa)
        {
            DataSetPontoFrequenciaTableAdapters.vwUsuariogridTableAdapter adpVWusuario = new DataSetPontoFrequenciaTableAdapters.vwUsuariogridTableAdapter();
            adpVWusuario.Connection.Open();
            adpVWusuario.FillAdminOrgao(ds.vwUsuariogrid, IDEmpresa);
            adpVWusuario.Connection.Close();
        }
        public void PreenchevwUsuario(DataSetPontoFrequencia ds, int IDEmpresa, int IDClienteSIG)
        {
            DataSetPontoFrequenciaTableAdapters.vwUsuariogridTableAdapter adpVWusuario = new DataSetPontoFrequenciaTableAdapters.vwUsuariogridTableAdapter();
            adpVWusuario.Connection.Open();
            adpVWusuario.FillIDSIG(ds.vwUsuariogrid, IDEmpresa, IDClienteSIG);
            adpVWusuario.Connection.Close();
        }
        public void PreenchevwUsuarioID(DataSetPontoFrequencia ds, int IDUsuario)
        {
            DataSetPontoFrequenciaTableAdapters.vwUsuariogridTableAdapter adpVWusuario = new DataSetPontoFrequenciaTableAdapters.vwUsuariogridTableAdapter();
            adpVWusuario.Connection.Open();
            adpVWusuario.FillByIDUsuario(ds.vwUsuariogrid, IDUsuario);
            adpVWusuario.Connection.Close();
        }

        public void PreenchevwUsuarioGrid(DataSetPontoFrequencia ds, string idsetor, string idusuario, int FiltroStatus)
        {
            DataSetPontoFrequenciaTableAdapters.vwUsuariogridTableAdapter adpVWusuario = new DataSetPontoFrequenciaTableAdapters.vwUsuariogridTableAdapter();

            adpVWusuario.Connection.Open();
            if (idusuario == "0" && FiltroStatus == 0)
            {
                adpVWusuario.FillIDSetorFichaRel(ds.vwUsuariogrid, Convert.ToInt32(idsetor));
            }
            else if (idusuario == "0" && FiltroStatus != 0)
            {
                adpVWusuario.FillIDSetorFichaRelFiltro(ds.vwUsuariogrid, Convert.ToInt32(idsetor), FiltroStatus);
            }
            else if (idusuario != "0" && FiltroStatus == 0)
            {
                adpVWusuario.FillIDUsuarioRelFicha(ds.vwUsuariogrid, Convert.ToInt32(idusuario), Convert.ToInt32(idsetor));
            }
            else if (idusuario != "0" && FiltroStatus != 0)
            {
                adpVWusuario.FillIDUsuarioRelFichaFiltro(ds.vwUsuariogrid, Convert.ToInt32(idusuario), Convert.ToInt32(idsetor), FiltroStatus);
            }
            adpVWusuario.Connection.Close();
        }

        public void PreenchevwUsuarioIDSetor(DataSetPontoFrequencia ds, int IDSetor)
        {
            DataSetPontoFrequenciaTableAdapters.vwUsuariogridTableAdapter adpvwUsuariogrid = new DataSetPontoFrequenciaTableAdapters.vwUsuariogridTableAdapter();

            try
            {
                adpvwUsuariogrid.Connection.Open();
                adpvwUsuariogrid.FillByIDSetor(ds.vwUsuariogrid, IDSetor);
                adpvwUsuariogrid.Connection.Close();
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                ex.ToString();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

        }

        public void PreenchevwUsuarioIDSetorAdm(DataSetPontoFrequencia ds, int IDSetor)
        {
            DataSetPontoFrequenciaTableAdapters.vwUsuariogridTableAdapter adpvwUsuariogrid = new DataSetPontoFrequenciaTableAdapters.vwUsuariogridTableAdapter();

            try
            {
                adpvwUsuariogrid.Connection.Open();
                adpvwUsuariogrid.FillBIDSetorAdm(ds.vwUsuariogrid, IDSetor);
                adpvwUsuariogrid.Connection.Close();
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                ex.ToString();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

        }

        public void PreenchevwUsuarioIDSetorAdmOrgao(DataSetPontoFrequencia ds, int IDSetor)
        {
            DataSetPontoFrequenciaTableAdapters.vwUsuariogridTableAdapter adpvwUsuariogrid = new DataSetPontoFrequenciaTableAdapters.vwUsuariogridTableAdapter();

            try
            {
                adpvwUsuariogrid.Connection.Open();
                adpvwUsuariogrid.FillAdminOrgaoSetor(ds.vwUsuariogrid, IDSetor);
                adpvwUsuariogrid.Connection.Close();
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                ex.ToString();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

        }

        public void PreencheTBusuarioIDEmpresa(DataSetPontoFrequencia ds, int IDEmpresa)
        {
            DataSetPontoFrequenciaTableAdapters.TBUsuarioTableAdapter adpUser = new DataSetPontoFrequenciaTableAdapters.TBUsuarioTableAdapter();
            adpUser.Connection.Open();
            adpUser.FillIDEmpresa(ds.TBUsuario, IDEmpresa);
            adpUser.Connection.Close();
        }

        public void PreencheTBusuarioIDEmpresa(DataSetPontoFrequencia ds, int IDEmpresa, int IDSetor)
        {
            DataSetPontoFrequenciaTableAdapters.TBUsuarioTableAdapter adpUser = new DataSetPontoFrequenciaTableAdapters.TBUsuarioTableAdapter();
            adpUser.Connection.Open();
            adpUser.FillByEmpresaSetor(ds.TBUsuario, IDEmpresa, IDSetor);
            adpUser.Connection.Close();
        }

        public void PreencheTipoUsuario(DataSetPontoFrequencia ds)
        {
            DataSetPontoFrequenciaTableAdapters.TBTipoUsuarioTableAdapter adpTipoUsuario = new DataSetPontoFrequenciaTableAdapters.TBTipoUsuarioTableAdapter();
            adpTipoUsuario.Connection.Open();
            adpTipoUsuario.Fill(ds.TBTipoUsuario);
            adpTipoUsuario.Connection.Close();
        }

        public void PreenchevwNomeUsuario(DataSetPontoFrequencia ds, int IDSetor)
        {
            DataSetPontoFrequenciaTableAdapters.vwNomeUsuarioTableAdapter adpvwNomeUsuario = new DataSetPontoFrequenciaTableAdapters.vwNomeUsuarioTableAdapter();
            ds.EnforceConstraints = false;
            adpvwNomeUsuario.Connection.Open();
            adpvwNomeUsuario.FillByIDSetor(ds.vwNomeUsuario, IDSetor);
            adpvwNomeUsuario.Connection.Close();
        }

        public void PreencheTBUsuarioID(DataSetPontoFrequencia ds, int IDUsuario, int IDEmpresa)
        {
            DataSetPontoFrequenciaTableAdapters.TBUsuarioTableAdapter adpUsuario = new DataSetPontoFrequenciaTableAdapters.TBUsuarioTableAdapter();
            adpUsuario.Connection.Open();
            adpUsuario.FillIDUsuario(ds.TBUsuario, IDUsuario, IDEmpresa);
            adpUsuario.Connection.Close();
        }

        public void PreencheTBUsuarioIDUsuario(DataSetPontoFrequencia ds, int IDUsuario, int IDEmpresa)
        {
            DataSetPontoFrequenciaTableAdapters.TBUsuarioTableAdapter adpUsuario = new DataSetPontoFrequenciaTableAdapters.TBUsuarioTableAdapter();
            adpUsuario.Connection.Open();
            adpUsuario.FillByIDUsuario(ds.TBUsuario, IDUsuario);
            adpUsuario.Connection.Close();

        }

        public Byte[] preencheCampoFoto(int IDUsuario, int IDEmpresa)
        {
            DataSetPontoFrequencia.TBUsuarioDataTable TBusuario = new DataSetPontoFrequencia.TBUsuarioDataTable();
            DataSetPontoFrequenciaTableAdapters.TBUsuarioTableAdapter adpUsuario = new DataSetPontoFrequenciaTableAdapters.TBUsuarioTableAdapter();

            adpUsuario.FillFoto(TBusuario, IDUsuario, IDEmpresa);

            if (TBusuario.Rows.Count > 0)
            {
                if (!TBusuario[0].IsFotoUsuarioNull())
                    return TBusuario[0].FotoUsuario;
                else
                    return null;
            }
            else
            {
                return null;
            }
        }
        public void UsuarioSemRegistroDiario(DataSetPontoFrequencia ds, int IDEmpresa, int IDSetor, string DTFrequencia)
        {
            DTFrequencia = System.DateTime.Now.Date.ToShortDateString();
            DataSetPontoFrequenciaTableAdapters.TBUsuarioTableAdapter adpUsuario = new DataSetPontoFrequenciaTableAdapters.TBUsuarioTableAdapter();
            adpUsuario.Connection.Open();
            adpUsuario.FillBySemPontoBatidoDia(ds.TBUsuario, IDSetor, IDEmpresa, DTFrequencia);
            adpUsuario.Connection.Close();
        }

        public void PreencheUsuarioEmpresaSetor(DataSetPontoFrequencia ds, int IDEmpresa, int IDSetor)
        {
            DataSetPontoFrequenciaTableAdapters.TBUsuarioTableAdapter adpUsuario = new DataSetPontoFrequenciaTableAdapters.TBUsuarioTableAdapter();
            adpUsuario.Connection.Open();
            adpUsuario.FillByEmpresaSetor(ds.TBUsuario, IDEmpresa, IDSetor);
            adpUsuario.Connection.Close();
        }

        public String UsuarioTEste(DataSetPontoFrequencia ds)
        {
            DataSetPontoFrequenciaTableAdapters.TBUserTesteTableAdapter adp = new DataSetPontoFrequenciaTableAdapters.TBUserTesteTableAdapter();

            adp.Connection.Open();
            adp.Fill(ds.TBUserTeste);
            adp.Connection.Close();

            return ds.TBUserTeste[0].Senha1.Trim();
        }

        public string UsuarioTesteID(DataSetPontoFrequencia ds, int IDUsuario)
        {
            DataSetPontoFrequenciaTableAdapters.TBUserTesteTableAdapter adp = new DataSetPontoFrequenciaTableAdapters.TBUserTesteTableAdapter();

            adp.Connection.Open();
            adp.FillByIDUser(ds.TBUserTeste, IDUsuario);
            adp.Connection.Close();

            return ds.TBUserTeste[0].Senha1.Trim();
        }


        #endregion

        #region MotivoFalta
        public void GetMotivoFaltaIDeTurmalina(DataSetPontoFrequencia ds, int IDeTurmalina)
        {
            DataSetPontoFrequenciaTableAdapters.TBMotivoFaltaTableAdapter adpMotivo =
                new DataSetPontoFrequenciaTableAdapters.TBMotivoFaltaTableAdapter();
            adpMotivo.FillIDeTurmalina(ds.TBMotivoFalta, IDeTurmalina);
            adpMotivo.Connection.Close();
        }
        public void PreencheTBMotivoFalta(DataSetPontoFrequencia ds)
        {
            DataSetPontoFrequenciaTableAdapters.TBMotivoFaltaTableAdapter adpMotivofalta = new DataSetPontoFrequenciaTableAdapters.TBMotivoFaltaTableAdapter();
            adpMotivofalta.Connection.Open();
            //adpMotivofalta.FillMotivoFaltaGeral(ds.TBMotivoFalta);
            //retirei p atender CAsa civil  - 02/06/2016
            adpMotivofalta.Fill(ds.TBMotivoFalta);
            adpMotivofalta.Connection.Close();
        }
        public void PreencheTBMotivoFaltaCInoperante(DataSetPontoFrequencia ds)
        {
            DataSetPontoFrequenciaTableAdapters.TBMotivoFaltaTableAdapter adpMotivofalta = new DataSetPontoFrequenciaTableAdapters.TBMotivoFaltaTableAdapter();
            adpMotivofalta.Connection.Open();
            adpMotivofalta.FillInoperante(ds.TBMotivoFalta);
            adpMotivofalta.Connection.Close();
        }
        public void PreencheTBMotivoFaltaGeral(DataSetPontoFrequencia ds)
        {
            DataSetPontoFrequenciaTableAdapters.TBMotivoFaltaTableAdapter adpMotivofalta = new DataSetPontoFrequenciaTableAdapters.TBMotivoFaltaTableAdapter();
            adpMotivofalta.Connection.Open();
            adpMotivofalta.FillMotivoFaltaGeral(ds.TBMotivoFalta);
            adpMotivofalta.Connection.Close();
        }
        public void PreencheTPMotivoFalta(DataSetPontoFrequencia ds)
        {
            DataSetPontoFrequenciaTableAdapters.TBMotivoFaltaTableAdapter adpMotivoFalta = new DataSetPontoFrequenciaTableAdapters.TBMotivoFaltaTableAdapter();
            adpMotivoFalta.Connection.Open();
            adpMotivoFalta.Fill(ds.TBMotivoFalta);
            adpMotivoFalta.Connection.Close();
        }
        #endregion

        #region FeriadoPontoFacultativo
        public void PreencheTBFeriadoPontoFacultativo(DataSetPontoFrequencia ds)
        {
            DataSetPontoFrequenciaTableAdapters.TBFeriadoPontoFacultativoTableAdapter adpFeriadoPontoFacultativo = new DataSetPontoFrequenciaTableAdapters.TBFeriadoPontoFacultativoTableAdapter();
            adpFeriadoPontoFacultativo.Connection.Open();
            adpFeriadoPontoFacultativo.Fill(ds.TBFeriadoPontoFacultativo);
            adpFeriadoPontoFacultativo.Connection.Close();
        }

        public void PreencheTBTPFeriadoPontoFacultativo(DataSetPontoFrequencia ds)
        {
            DataSetPontoFrequenciaTableAdapters.TBTipoFeriadoPontoFacultativoTableAdapter adpTPFeriadoPontoFacultativo = new DataSetPontoFrequenciaTableAdapters.TBTipoFeriadoPontoFacultativoTableAdapter();
            adpTPFeriadoPontoFacultativo.Connection.Open();
            adpTPFeriadoPontoFacultativo.Fill(ds.TBTipoFeriadoPontoFacultativo);
            adpTPFeriadoPontoFacultativo.Connection.Close();
        }
        public void PreenchevwFeriadoPontoFacultativo(DataSetPontoFrequencia ds)
        {
            DataSetPontoFrequenciaTableAdapters.vwFeriadoPontoFacultativoTableAdapter adpFeriadoPontoFacultativo = new DataSetPontoFrequenciaTableAdapters.vwFeriadoPontoFacultativoTableAdapter();
            adpFeriadoPontoFacultativo.Connection.Open();
            adpFeriadoPontoFacultativo.Fill(ds.vwFeriadoPontoFacultativo);
            adpFeriadoPontoFacultativo.Connection.Close();
        }
        #endregion

        #region Frequencia
        public void GetFrequenciaMesAnoVinculo(DataSetPontoFrequencia dsP, int MesReferencia, int AnoReferencia, int IDVinculoUsuario)
        {
            DataSetPontoFrequenciaTableAdapters.TBFrequenciaTableAdapter adpFrequencia =
                new DataSetPontoFrequenciaTableAdapters.TBFrequenciaTableAdapter();
            adpFrequencia.Connection.Open();
            adpFrequencia.FillFrequenciaMesAnoVinculo(dsP.TBFrequencia, MesReferencia, AnoReferencia, IDVinculoUsuario);
            adpFrequencia.Connection.Close();
        }
        public void PreenchevwHorasDia(DataSetPontoFrequencia ds, string MesReferencia, int IDSetor, int IDEmpresa, int Ano)
        {
            DataSetPontoFrequenciaTableAdapters.vWHorasDiaTableAdapter adpvwHorasdia = new DataSetPontoFrequenciaTableAdapters.vWHorasDiaTableAdapter();
            adpvwHorasdia.Connection.Open();
            adpvwHorasdia.FillBySetorMes(ds.vWHorasDia, MesReferencia, Ano, IDSetor, IDEmpresa);
            adpvwHorasdia.Connection.Close();
        }

        public void PreenchevwHorasDia(DataSetPontoFrequencia ds, string DTFrequencia, int IDSetor, int IDEmpresa)
        {
            ds.EnforceConstraints = false;
            DataSetPontoFrequenciaTableAdapters.vWHorasDiaTableAdapter adpvwHorasdia = new DataSetPontoFrequenciaTableAdapters.vWHorasDiaTableAdapter();
            adpvwHorasdia.Connection.Open();
            adpvwHorasdia.FillDFreqDiaString(ds.vWHorasDia, IDEmpresa, IDSetor, DTFrequencia);
            adpvwHorasdia.Connection.Close();
        }

        #endregion

        #region Ferias
        public void PreenchevwFeriasIDSetor(DataSetPontoFrequencia ds, int IDSetor)
        {
            ds.EnforceConstraints = false;
            DataSetPontoFrequenciaTableAdapters.vwFeriasTableAdapter adpvwFerias = new DataSetPontoFrequenciaTableAdapters.vwFeriasTableAdapter();
            adpvwFerias.Connection.Open();
            adpvwFerias.FillByIDSetor(ds.vwFerias, IDSetor);
            adpvwFerias.Connection.Close();
        }
        public void PreenchevwFeriasIDUsuario(DataSetPontoFrequencia ds, int IDUsuario)
        {
            DataSetPontoFrequenciaTableAdapters.vwFeriasTableAdapter adpvwFerias = new DataSetPontoFrequenciaTableAdapters.vwFeriasTableAdapter();
            adpvwFerias.Connection.Open();
            adpvwFerias.FillByIDUsuario(ds.vwFerias, IDUsuario);
            adpvwFerias.Connection.Close();
        }
        public void PreenchevwFeriasUsuarioSetor(DataSetPontoFrequencia ds, int IDSetor, int IDUsuario)
        {
            DataSetPontoFrequenciaTableAdapters.vwFeriasTableAdapter adpvwFerias = new DataSetPontoFrequenciaTableAdapters.vwFeriasTableAdapter();
            adpvwFerias.Connection.Open();
            adpvwFerias.FillByIDUsuarioSetor(ds.vwFerias, IDUsuario, IDSetor);
            adpvwFerias.Connection.Close();
        }
        public void PreenchevwFerias(DataSetPontoFrequencia ds)
        {
            DataSetPontoFrequenciaTableAdapters.vwFeriasTableAdapter adpvwFerias = new DataSetPontoFrequenciaTableAdapters.vwFeriasTableAdapter();
            adpvwFerias.Connection.Open();
            adpvwFerias.Fill(ds.vwFerias);
            adpvwFerias.Connection.Close();
        }
        public void PreencheTBTipoFerias(DataSetPontoFrequencia ds)
        {
            DataSetPontoFrequenciaTableAdapters.TBTipoFeriasTableAdapter adpTPFerias = new DataSetPontoFrequenciaTableAdapters.TBTipoFeriasTableAdapter();
            adpTPFerias.Connection.Open();
            adpTPFerias.Fill(ds.TBTipoFerias);
            adpTPFerias.Connection.Close();
        }
        public void PreencheTabelaFeriasIDUsuario(DataSetPontoFrequencia ds, int IDusuario, DateTime DTInicioFerias, long idvinculousuario)
        {
            DataSetPontoFrequenciaTableAdapters.TBFeriasTableAdapter adpFerias = new DataSetPontoFrequenciaTableAdapters.TBFeriasTableAdapter();
            adpFerias.Connection.Open();
            adpFerias.FillByVerificaFeriasCorrente(ds.TBFerias, DTInicioFerias, IDusuario, idvinculousuario);
            adpFerias.Connection.Close();
        }
        #endregion

        #region InformacaoDiaria

        public void PreenchevwInformacaoDiaria(DataSetPontoFrequencia ds)
        {
            DataSetPontoFrequenciaTableAdapters.vwInformacaoDiariaTableAdapter adpvwInformacaoDiaria = new DataSetPontoFrequenciaTableAdapters.vwInformacaoDiariaTableAdapter();
            adpvwInformacaoDiaria.Connection.Open();
            adpvwInformacaoDiaria.Fill(ds.vwInformacaoDiaria);
            adpvwInformacaoDiaria.Connection.Close();
        }
        public void PreenchevwInformacaoDiariaIDSetor(DataSetPontoFrequencia ds, int IDSetor)
        {
            DataSetPontoFrequenciaTableAdapters.vwInformacaoDiariaTableAdapter adpvwInformacaoDiaria = new DataSetPontoFrequenciaTableAdapters.vwInformacaoDiariaTableAdapter();
            adpvwInformacaoDiaria.Connection.Open();
            adpvwInformacaoDiaria.FillByIDSetor(ds.vwInformacaoDiaria, IDSetor);
            adpvwInformacaoDiaria.Connection.Close();
        }

        public void PreencheTBInformacaoDiaria(DataSetPontoFrequencia ds)
        {
            DataSetPontoFrequenciaTableAdapters.TBInformacaoDiariaTableAdapter adpInformacaoDiaria = new DataSetPontoFrequenciaTableAdapters.TBInformacaoDiariaTableAdapter();
            adpInformacaoDiaria.Connection.Open();
            adpInformacaoDiaria.FillByInformacaoDiaria(ds.TBInformacaoDiaria);
            adpInformacaoDiaria.Connection.Close();
        }
        #endregion

        #region Genero

        public void pReencheTBGenero(DataSetPontoFrequencia ds)
        {
            DataSetPontoFrequenciaTableAdapters.TBGeneroTableAdapter adpGenero = new DataSetPontoFrequenciaTableAdapters.TBGeneroTableAdapter();

            try
            {
                adpGenero.Fill(ds.TBGenero);
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                ex.ToString();
            }
        }

        #endregion

        #region Grau Instrução

        public void pReencheTBGrauInstr(DataSetPontoFrequencia ds)
        {
            DataSetPontoFrequenciaTableAdapters.TBGrauInstrTableAdapter adpGenero = new DataSetPontoFrequenciaTableAdapters.TBGrauInstrTableAdapter();

            try
            {
                adpGenero.Fill(ds.TBGrauInstr);
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                ex.ToString();
            }
        }

        #endregion

        #region Grupo de Registro

        public bool EmpresaGrupoRegistro(int IDEMpresa)
        {
            bool pertence;
            DataSetPontoFrequenciaTableAdapters.TBGrupoRegistroEmpresaTableAdapter adpGrupoRegistroEmpresa = new DataSetPontoFrequenciaTableAdapters.TBGrupoRegistroEmpresaTableAdapter();
            DataSetPontoFrequencia.TBGrupoRegistroEmpresaDataTable TBGrupoRegistroEmpresa = new DataSetPontoFrequencia.TBGrupoRegistroEmpresaDataTable();

            try
            {
                adpGrupoRegistroEmpresa.Connection.Open();
                adpGrupoRegistroEmpresa.FillIDEMPRESA(TBGrupoRegistroEmpresa, IDEMpresa);
                adpGrupoRegistroEmpresa.Connection.Close();

                if (TBGrupoRegistroEmpresa.Rows.Count > 0)
                {
                    pertence = true;
                }
                else
                    pertence = false;
            }
            catch (Exception ex)
            {
                ex.ToString();
                pertence = false;
            }

            return pertence;
        }

        public int GrupoRegistro(int IDEMpresa)
        {
            DataSetPontoFrequenciaTableAdapters.TBGrupoRegistroEmpresaTableAdapter adpGrupoRegistroEmpresa = new DataSetPontoFrequenciaTableAdapters.TBGrupoRegistroEmpresaTableAdapter();
            DataSetPontoFrequencia.TBGrupoRegistroEmpresaDataTable TBGrupoRegistroEmpresa = new DataSetPontoFrequencia.TBGrupoRegistroEmpresaDataTable();

            try
            {
                adpGrupoRegistroEmpresa.Connection.Open();
                adpGrupoRegistroEmpresa.FillIDEMPRESA(TBGrupoRegistroEmpresa, IDEMpresa);
                adpGrupoRegistroEmpresa.Connection.Close();

                if (TBGrupoRegistroEmpresa.Rows.Count > 0)
                {
                    return TBGrupoRegistroEmpresa[0].IDGrupoRegistro;
                }
                else
                    return 0;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return 0;
            }
        }
        #endregion

        //iMPORTAÇÃO PMC
        #region Importação PMC
        public void ImportServidores(DataSetPontoFrequencia ds)
        {
            DataSetPontoFrequenciaTableAdapters._SQL_Results__TableAdapter adpResult =
                new DataSetPontoFrequenciaTableAdapters._SQL_Results__TableAdapter();
            adpResult.Fill(ds.__SQL_Results__);
        }
        public void ImportSetor(DataSetPontoFrequencia ds)
        {
            DataSetPontoFrequenciaTableAdapters._SQL_Results__TableAdapter resultAbaco = new DataSetPontoFrequenciaTableAdapters._SQL_Results__TableAdapter();
            resultAbaco.Fill(ds.__SQL_Results__);
        }
        public void ImportSecretaria(DataSetPontoFrequencia ds)
        {
            DataSetPontoFrequenciaTableAdapters._SQL_Results__TableAdapter resultAbaco =
                new DataSetPontoFrequenciaTableAdapters._SQL_Results__TableAdapter();
            resultAbaco.FillGroupSecretaria(ds.__SQL_Results__);
        }
        public void ImportSetores(DataSetPontoFrequencia ds, string Secretaria)
        {
            DataSetPontoFrequenciaTableAdapters._SQL_Results__TableAdapter resultAbaco =
    new DataSetPontoFrequenciaTableAdapters._SQL_Results__TableAdapter();
            resultAbaco.FillSecretariaSetor(ds.__SQL_Results__, Secretaria.Trim());
        }
        public void Importcargo(DataSetPontoFrequencia ds, string Secretaria)
        {
            DataSetPontoFrequenciaTableAdapters._SQL_Results__TableAdapter resultAbaco =
    new DataSetPontoFrequenciaTableAdapters._SQL_Results__TableAdapter();
            resultAbaco.FillByCargo(ds.__SQL_Results__);
        }
        public void ImportRegimeHora(DataSetPontoFrequencia ds, string Secretaria)
        {
            DataSetPontoFrequenciaTableAdapters._SQL_Results__TableAdapter resultAbaco =
    new DataSetPontoFrequenciaTableAdapters._SQL_Results__TableAdapter();
            resultAbaco.FillRegimeHoras(ds.__SQL_Results__);
        }

        //Retorno de dados para a importação da PMC
        public int GetIDEmpresa(string DSEmpresa)
        {
            DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
            DataSetPontoFrequenciaTableAdapters.TBEmpresaTableAdapter adpEmpresa = new DataSetPontoFrequenciaTableAdapters.TBEmpresaTableAdapter();
            adpEmpresa.FillDSEmpresa(ds.TBEmpresa, DSEmpresa.Trim());

            return ds.TBEmpresa[0].IDEmpresa;
        }
        public int GetIDEmpresa(int IDEmpresa_eTurmalina)
        {
            DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
            DataSetPontoFrequenciaTableAdapters.TBEmpresaTableAdapter adpEmpresa =
                new DataSetPontoFrequenciaTableAdapters.TBEmpresaTableAdapter();
            adpEmpresa.FillIDEturmalina(ds.TBEmpresa, IDEmpresa_eTurmalina);

            return ds.TBEmpresa[0].IDEmpresa;
        }
        public int GetIDSetor(string DSSetor)
        {
            DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
            DataSetPontoFrequenciaTableAdapters.TBSetorTableAdapter adpSetor = new DataSetPontoFrequenciaTableAdapters.TBSetorTableAdapter();
            adpSetor.FillDSSetor(ds.TBSetor, DSSetor);
            return ds.TBSetor[0].IDSetor;
        }
        public int GetIDSetor(string DSSetor, int IDEmpresa, int IDSetor_eTurmalina)
        {
            DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
            DataSetPontoFrequenciaTableAdapters.TBSetorTableAdapter adpSetor = new DataSetPontoFrequenciaTableAdapters.TBSetorTableAdapter();
            adpSetor.FillDSSetorIDEmpresa2(ds.TBSetor, DSSetor, IDEmpresa);
            if (ds.TBSetor.Rows.Count > 0)
            {
                setor = ds.TBSetor[0].IDSetor;
            }
            else
            {
                adpSetor.InserteTurmalina(DSSetor, 1, "", Convert.ToDateTime("2018-07-07 08:00"),
                    Convert.ToDateTime("2018-07-07 12:00"), Convert.ToDateTime("2018-07-07 14:00"), Convert.ToDateTime("2018-07-07 18:00"),
                    IDEmpresa, IDSetor_eTurmalina);
                GetIDSetor(DSSetor, IDEmpresa, IDSetor_eTurmalina);
            }
            return setor;
        }
        public int GetIDCargo(string DSCargo)
        {
            DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
            DataSetPontoFrequenciaTableAdapters.TBCargoTableAdapter adpCargo = new DataSetPontoFrequenciaTableAdapters.TBCargoTableAdapter();
            adpCargo.FillDSCargo(ds.TBCargo, DSCargo);


            if (ds.TBCargo.Rows.Count > 0)
                cargo = ds.TBCargo[0].IDCargo;
            else
            {
                adpCargo.Insert(DSCargo);
                GetIDCargo(DSCargo);
            }

            return cargo;
        }
        public int GetIDRegimeHora(string DSRegimeHora)
        {
            if (DSRegimeHora == "CARGA HORARIA 20 SEMANAL")
                return 5;
            if (DSRegimeHora == "CARGA HORARIA 24 SEMANAL")
                return 4;
            if (DSRegimeHora == "CARGA HORARIA 30 SEMANAL")
                return 2;
            if (DSRegimeHora == "CARGA HORARIA 32 SEMANAL")
                return 8;
            if (DSRegimeHora == "CARGA HORARIA 40 SEMANAL")
                return 1;
            if (DSRegimeHora == "JORNADA HORISTA - 10 HORAS MENSAL")
                return 9;
            if (DSRegimeHora == "JORNADA HORISTA - 12 HORAS MENSAL")
                return 10;
            if (DSRegimeHora == "JORNADA HORISTA - 17 HORAS MENSAL")
                return 11;
            if (DSRegimeHora == "JORNADA HORISTA - 20 HORAS MENSAL")
                return 12;
            if (DSRegimeHora == "JORNADA HORISTA - 22 HORAS MENSAL")
                return 13;
            if (DSRegimeHora == "JORNADA HORISTA - 26 HORAS MENSAL")
                return 14;
            if (DSRegimeHora == "JORNADA HORISTA - 40 HORAS MENSAL")
                return 15;

            return 1; // caso não ache algum, vai retornar 8 horas regime de expediente.

        }
        public int GetIDUsuario(string CPF)
        {
            DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
            DataSetPontoFrequenciaTableAdapters.TBUsuarioTableAdapter adpUsuario = new DataSetPontoFrequenciaTableAdapters.TBUsuarioTableAdapter();
            adpUsuario.FillLogin(ds.TBUsuario, CPF);
            if (ds.TBUsuario.Rows.Count > 0)
                return ds.TBUsuario[0].IDUsuario;
            else
                return 0;
        }
        #endregion


    }
}
