using MetodosPontoFrequencia.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MetodosPontoFrequencia.FechamentoFolha
{
    public class FechamentoFolhaSrv
    {

        #region Buscas
        public DataTable GetEmpresa(string mes)
        {
            DataTable dt = new DataTable();
            StringBuilder query = new StringBuilder();

            query.Append(" select IDEmpresa, DSEmpresa from TBEmpresa");
            query.Append(" where 1 = 1");

            using (dt = Util.getDatset(query.ToString()))
                return dt;

        }

        public DataTable GetSetor(string mes, string empresaId = null)
        {
            DataTable dt = new DataTable();
            StringBuilder query = new StringBuilder();
            query.Append(" select s.IDSetor IDSetor, cast(s.IDSetor as varchar) + ' - '+  s.DSSetor DSSetor from tbsetor s");
            query.Append(" inner join TBVinculoUsuario v on v.IDSetor = s.IDSetor and v.IDEmpresa = s.IDEmpresa");
            query.Append(" where 1 = 1");
            query.Append(" and s.IDSetor not in (select se.IDSetor from TBFechamentoFolha f");
            query.Append(" inner join TBFechamentoFolha_Setor se on se.IDFechamento = f.IDFechamento");
            query.AppendFormat(" where f.Ano = {0} and f.mes = {1} and f.IDEmpresa={2})", DateTime.Now.Year, mes, empresaId);

            if (!string.IsNullOrEmpty(empresaId))
                query.AppendFormat(" And s.idempresa = {0}", empresaId);

            query.Append(" group by s.IDSetor, s.DSSetor");
            query.Append(" order by s.DSSetor");

            using (dt = Util.getDatset(query.ToString()))
                return dt;
        }

        public DataTable GetCargo(string mes, string empresaId)
        {
            DataTable dt = new DataTable();
            StringBuilder query = new StringBuilder();
            query.Append(" select c.IDCargo IDCargo, cast(c.IDCargo as varchar) + ' - '+ c.DSCargo DSCargo from TBCargo c");
            query.Append(" inner join TBVinculoUsuario v on v.IDCargo = c.IDCargo");
            query.AppendFormat(" Where v.IDEmpresa = {0}", empresaId);
            query.Append(" and c.IDCargo not in(select se.IDCargo from TBFechamentoFolha f");
            query.Append(" inner join TBFechamentoFolha_Cargo se on se.IDFechamento = f.IDFechamento");
            query.AppendFormat("  where f.Ano = {0} and f.mes = {1} and f.IDEmpresa = {2})", DateTime.Now.Year, mes, empresaId);
            query.Append(" group by c.IDCargo, c.DSCargo");
            query.Append(" order by c.DSCargo");
            using (dt = Util.getDatset(query.ToString()))
                return dt;
        }

        public List<int> GetCargoList(string mes, string empresaId)
        {
            DataTable dt = new DataTable();
            List<int> lista = new List<int>();
            StringBuilder query = new StringBuilder();
            query.Append(" select c.IDCargo from TBCargo c");
            query.Append(" inner join TBVinculoUsuario v on v.IDCargo = c.IDCargo");
            query.AppendFormat(" Where v.IDEmpresa = {0}", empresaId);
            query.Append(" and c.IDCargo not in(select se.IDCargo from TBFechamentoFolha f");
            query.Append(" inner join TBFechamentoFolha_Cargo se on se.IDFechamento = f.IDFechamento");
            query.AppendFormat("  where f.Ano = {0} and f.mes = {1} and f.IDEmpresa = {2})", DateTime.Now.Year, mes, empresaId);
            query.Append(" group by c.IDCargo");
            IDataReader reader = Util.getDataReader(query.ToString());
            using (reader)
            {
                while (reader.Read())
                {
                    int id = int.Parse(reader["IDCargo"].ToString());
                    lista.Add(id);
                }
            }

            return lista;
        }

        public List<int> GetSetorList(string mes, string empresaId = null)
        {
            DataTable dt = new DataTable();
            List<int> lista = new List<int>();
            StringBuilder query = new StringBuilder();
            query.Append(" select s.IDSetor from tbsetor s");
            query.Append(" inner join TBVinculoUsuario v on v.IDSetor = s.IDSetor and v.IDEmpresa = s.IDEmpresa");
            query.Append(" where 1 = 1");
            query.Append(" and s.IDSetor not in (select se.IDSetor from TBFechamentoFolha f");
            query.Append(" inner join TBFechamentoFolha_Setor se on se.IDFechamento = f.IDFechamento");
            query.AppendFormat(" where f.Ano = {0} and f.mes = {1} and f.IDEmpresa={2})", DateTime.Now.Year, mes, empresaId);

            if (!string.IsNullOrEmpty(empresaId))
                query.AppendFormat(" And s.idempresa = {0}", empresaId);

            query.Append(" group by s.IDSetor");
            IDataReader reader = Util.getDataReader(query.ToString());
            using (reader)
            {
                while (reader.Read())
                {
                    int id = int.Parse(reader["IDSetor"].ToString());
                    lista.Add(id);
                }
            }

            return lista;
        }

        public DataTable GetMes()
        {
            DataTable dt = new DataTable();
            var query = "select IDMEs, DSMes from TBMes";
            using (dt = Util.getDatset(query.ToString()))
                return dt;
        }

        public DataTable GetFechamento(string mes)
        {
            StringBuilder query = new StringBuilder();
            DataTable dt = new DataTable();
            query.Append("  select f.Ano, CONVERT(VARCHAR(10), f.DataProcessamento, 103) as DataProcessamento, f.IDEmpresa, f.IDFechamento, e.DSEmpresa, 'Processado' as Situacao,");
            query.Append(" case f.mes when 1 then 'Janeiro'");
            query.Append(" when 2 then 'Fevereiro'");
            query.Append(" when 3 then 'Março'");
            query.Append(" when 4 then 'Abril'");
            query.Append(" when 5 then 'Maio'");
            query.Append(" when 6 then 'Junho'");
            query.Append(" when 7 then 'Julho'");
            query.Append(" when 8 then 'Agosto'");
            query.Append(" when 9 then 'Setembro'");
            query.Append(" when 10 then 'Outubro'");
            query.Append(" when 11 then 'Novembro'");
            query.Append(" when 12 then 'Dezembro' end Mes");
            query.Append(" from TBFechamentoFolha f");
            query.Append(" left join TBEmpresa e on e.IDEmpresa = f.IDEmpresa");
            query.Append(" where 1=1");
            query.AppendFormat(" and f.Mes = {0}", mes);
            query.AppendFormat(" and f.Ano = {0}", DateTime.Now.Year);

            using (dt = Util.getDatset(query.ToString()))
                return dt;
        }

        public TBFechamentoFolha GetEmpresaProcessada(int idFechamento)
        {
            string query = string.Format("select IDFechamento, Mes, Ano, DataProcessamento, IDEmpresa from TBFechamentoFolha where IDFechamento = {0} ", idFechamento);
            return ReturnEmpresaProcessada(query);
        }

        public TBFechamentoFolha GetEmpresaProcessada(string mes, string empresa)
        {
            TBFechamentoFolha dado = new TBFechamentoFolha();
            string query = string.Format("select IDFechamento, Mes, Ano, DataProcessamento, IDEmpresa from TBFechamentoFolha where IDEmpresa = {0} and Mes = {1} and Ano = {2}", empresa, mes, DateTime.Now.Year);
            return ReturnEmpresaProcessada(query);
        }

        public DataTable GetDadosImprimir(string mes)
        {
            DataTable dt = new DataTable();

            StringBuilder query = new StringBuilder();
            query.Append(" select v.IDEmpresa, e.DSEmpresa, v.IDSetor, s.DSSetor,");
            query.Append(" u.Login, l.IDVinculoUsuario, v.IDUsuario, u.DSUsuario, u.Matricula,");
            query.Append(" v.IDCargo, c.DSCargo, l.DataOcorrencia, l.CodigoVerba as CodVerba, l.TotalHoras as TotalHora, v.IDRegimeHora, r.DSRegimeHora as RegimeHora");
            query.Append(" from TBHorasExtrasLancamentos l");
            query.Append(" left join TBVinculoUsuario v on v.IDVinculoUsuario = l.IDVinculoUsuario");
            query.Append(" left join TBUsuario u on u.IDUsuario = v.IDUsuario");
            query.Append(" left join TBEmpresa e on e.IDEmpresa = u.IDEmpresa");
            query.Append(" left join TBSetor s on s.IDSetor = u.IDSetor");
            query.Append(" left join TBCargo c on c.IDCargo = v.IDCargo");
            query.Append(" left join TBRegimeHora r on r.IDRegimeHora = v.IDRegimeHora");
            query.AppendFormat(" where DATEPART(month, l.DataOcorrencia) = {0}", mes);
            query.AppendFormat(" and DATEPART(year, l.DataOcorrencia) = {0}", DateTime.Now.Year);
            query.Append(" order by v.IDEmpresa, v.IDSetor, u.DSUsuario");

            using (dt = Util.getDatset(query.ToString()))
                return dt;
        }

        public TBFechamentoFolha ReturnEmpresaProcessada(string query)
        {
            TBFechamentoFolha dado = new TBFechamentoFolha();
            IDataReader reader = Util.getDataReader(query);
            using (reader)
            {
                while (reader.Read())
                {
                    dado.IDFechamento = int.Parse(reader["IDFechamento"].ToString());
                    dado.IDEmpresa = int.Parse(reader["IDEmpresa"].ToString());
                    dado.Ano = int.Parse(reader["Ano"].ToString());
                    dado.Mes = int.Parse(reader["Mes"].ToString());
                    dado.DataProcessamento = DateTime.Parse(reader["DataProcessamento"].ToString());
                }
            }
            return dado;
        }

        public List<int> GetCargosBYEmpresaProcessados(int idFechamento)
        {
            List<int> lista = new List<int>();
            string query = string.Format("select IDCargo from TBFechamentoFolha_Cargo where IDFechamento = {0} ", idFechamento);
            IDataReader reader = Util.getDataReader(query);
            using (reader)
            {
                while (reader.Read())
                {
                    int id = int.Parse(reader["IDCargo"].ToString());
                    lista.Add(id);
                }
            }
            return lista;
        }

        public List<int> GetSetoresBYEmpresaProcessados(int idFechamento)
        {
            List<int> lista = new List<int>();
            string query = string.Format("select IDSetor from TBFechamentoFolha_Setor where IDFechamento = {0} ", idFechamento);
            IDataReader reader = Util.getDataReader(query);
            using (reader)
            {
                while (reader.Read())
                {
                    int id = int.Parse(reader["IDSetor"].ToString());
                    lista.Add(id);
                }
            }
            return lista;
        }

        #endregion

        #region IAE

        public string Processar(string mes, string secretaria, string setores, string cargos, int idFechamento = 0)
        {
            try
            {
                StringBuilder query = new StringBuilder();
                List<string> lista = new List<string>();
                string retorno = string.Empty;
                int ano = DateTime.Now.Year;
                query.Append("  SELECT idvinculousuario from TBVinculoUsuario");
                query.Append("  where TBVinculoUsuario.IDStatus = 1");
                if (!string.IsNullOrEmpty(cargos))
                    query.AppendFormat("  and TBVinculoUsuario.IDCargo in ({0})", cargos);

                query.Append(" AND TBVINCULOUSUARIO.IDREGIMEHORA IN(SELECT IDREGIMEHORA FROM TBREGIMEHORA WHERE REGIMEPLANTONISTA = 1)");

                if (!string.IsNullOrEmpty(setores))
                    query.AppendFormat(" AND TBVINCULOUSUARIO.IDSETOR IN ({0})", setores);
                query.Append(" and Matricula<> '000000'");
                query.Append(" and matricula<> '0000000'");
                query.Append(" and (TBVinculoUsuario.IsencaoPonto is null or TBVinculoUsuario.IsencaoPonto = 0 or TBVinculoUsuario.IsencaoPonto = '')");

                if (secretaria != "54")
                {
                    query.Append(" and tbvinculousuario.idcargo <> 4");
                    query.Append(" and tbvinculousuario.idcargo <> 690");
                    query.Append(" and tbvinculousuario.idcargo <> 691");
                }
                query.AppendFormat(" and TBVinculoUsuario.IDEmpresa = {0}", secretaria);

                query.Append(" Union");

                query.Append(" SELECT idvinculousuario from TBVinculoUsuario");
                query.Append(" where TBVinculoUsuario.IDStatus = 1");
                query.Append(" AND TBVINCULOUSUARIO.IDREGIMEHORA IN(SELECT IDREGIMEHORA FROM TBREGIMEHORA WHERE REGIMEPLANTONISTA = 0)");
                if (!string.IsNullOrEmpty(setores))
                    query.AppendFormat(" AND TBVINCULOUSUARIO.IDSETOR IN ({0})", setores);
                query.Append(" AND Matricula<> '000000'");
                query.Append(" and matricula<> '0000000'");
                query.Append(" and (TBVinculoUsuario.IsencaoPonto is null or TBVinculoUsuario.IsencaoPonto = 0 or TBVinculoUsuario.IsencaoPonto = '')");

                if (secretaria != "54")
                {
                    query.Append(" and tbvinculousuario.idcargo <> 4");
                    query.Append(" and tbvinculousuario.idcargo <> 690");
                    query.Append(" and tbvinculousuario.idcargo <> 691");
                }
                query.AppendFormat(" and TBVinculoUsuario.IDEmpresa = {0}", secretaria);

                IDataReader reader = Util.getDataReader(query.ToString());
                using (reader)
                {
                    while (reader.Read())
                    {
                        var vinculoId = reader["idvinculousuario"].ToString();
                        string item = string.Format("exec spFechamentoFolhaUsuario {0},{1},{2};", vinculoId, mes, ano);
                        lista.Add(item);
                    }
                }

                if (lista.Count > 0)
                {
                    Util.ExecuteTransaction(lista);
                    DeletaTBHorasExtrasLancamentos(mes);
                    retorno = PopularFechamento(mes, secretaria, setores, cargos, idFechamento);
                }
                else
                    retorno = "Não foi encontrado nenhum servidor para processamento com a secretaria, setores e cargos informados.";

                return retorno;

            }
            catch (Exception ex)
            {
                return "Erro ao processar!";
            }
        }

        private string PopularFechamento(string mes, string secretaria, string setores, string cargos, int idFechamento)
        {
            string retorno = string.Empty;
            try
            {
                List<string> queryList = new List<string>();
                StringBuilder query = new StringBuilder();
                bool reprocessar = false;

                if (idFechamento > 0)
                    reprocessar = true;

                var dadoFechamento = GetEmpresaProcessada(mes, secretaria);
                idFechamento = dadoFechamento.IDFechamento > 0 ? dadoFechamento.IDFechamento : idFechamento;
                if (idFechamento == 0)
                {
                    query.Append(" Insert TBFechamentoFolha (IDEmpresa, DataProcessamento, Mes, Ano)");
                    query.AppendFormat(" Values({0},", secretaria);
                    query.Append(" CONVERT(DATETIME,getdate(),120),");
                    query.AppendFormat(" {0},", mes);
                    query.AppendFormat(" {0})", DateTime.Now.Year);

                    Util.ExecuteNonQuery(query.ToString());
                    var select = string.Format(" Select IDFechamento from TBFechamentoFolha where Mes = {0} and Ano = {1} and IDEmpresa = {2} ", mes, DateTime.Now.Year, secretaria);
                    IDataReader reader = Util.getDataReader(select);
                    using (reader)
                        if (reader.Read())
                            idFechamento = int.Parse(reader["IDFechamento"].ToString());
                }
                else
                    UpdateReprocessado(idFechamento);

                if (idFechamento > 0 && !reprocessar)
                {
                    var setorList = !string.IsNullOrEmpty(setores) ? setores.Split(',').Select(item => int.Parse(item)).ToList() : null;
                    var cargoList = !string.IsNullOrEmpty(cargos) ? cargos.Split(',').Select(item => int.Parse(item)).ToList() : null;

                    if (setorList == null)
                        setorList = GetSetorList(mes, secretaria);

                    foreach (var item in setorList)
                    {
                        string stringQuery = InsereSetorToString(idFechamento, item);
                        queryList.Add(stringQuery);
                    }


                    if (cargoList == null)
                        cargoList = GetCargoList(mes, secretaria);
                    foreach (var item in cargoList)
                    {
                        string stringQuery = InsereCargoToString(idFechamento, item);
                        queryList.Add(stringQuery);
                    }


                    if (queryList.Count > 0)
                        Util.ExecuteTransaction(queryList);
                }
                return "";

            }
            catch (Exception ex)
            {
                return "Processamento Executado. Erro ao Gravar Historico Folha!";
            }
        }

        private string InsereSetorToString(int FechamentoID, int setor)
        {
            StringBuilder query = new StringBuilder();
            query.Append(" Insert TBFechamentoFolha_Setor (IDFechamento, IDSetor, DataProcessamento)");
            query.AppendFormat(" Values ({0},{1},CONVERT(DATETIME,getDate(),120))", FechamentoID, setor);

            return query.ToString();
        }

        private string InsereCargoToString(int FechamentoID, int cargo)
        {
            StringBuilder query = new StringBuilder();
            query.Append(" Insert TBFechamentoFolha_Cargo (IDFechamento, IDCargo, DataProcessamento)");
            query.AppendFormat(" Values ({0},{1},CONVERT(DATETIME,getDate(),120))", FechamentoID, cargo);

            return query.ToString();
        }

        private void UpdateReprocessado(int id)
        {
            string query = string.Format(" Update TBFechamentoFolha set DataProcessamento = CONVERT(DATETIME,getDate(),120) where IDFechamento = {0}", id);
            Util.ExecuteNonQuery(query);
        }

        private void DeletaTBHorasExtrasLancamentos(string mes)
        {
            StringBuilder query = new StringBuilder();
            List<string> lista = new List<string>();
            //falta antes do inicio do vinculo
            query.Append(" DELETE FROM TBHORASEXTRASLANCAMENTOS WHERE");
            query.Append(" IDVINCULOUSUARIO IN(SELECT IDVINCULOUSUARIO");
            query.Append(" FROM TBVINCULOUSUARIO WHERE DTINICIOVINCULO > DATAOCORRENCIA)");
            query.AppendFormat(" AND DATEPART(MONTH, DATAOCORRENCIA) = {0}", mes);
            query.AppendFormat(" AND DATEPART(YEAR, DATAOCORRENCIA) = {0}", DateTime.Now.Year);
            query.Append(" AND CODIGOVERBA = '4504';");

            lista.Add(query.ToString());
            query = new StringBuilder();
            //falta regime de expediente no final de semana
            query.Append(" DELETE FROM TBHORASEXTRASLANCAMENTOS");
            query.AppendFormat(" WHERE DATEPART(MONTH, DATAOCORRENCIA) = {0}", mes);
            query.AppendFormat(" AND DATEPART(YEAR, DATAOCORRENCIA) = {0}", DateTime.Now.Year);
            query.AppendFormat(" AND(DATEPART(dw, DATAOCORRENCIA) = 1");
            query.AppendFormat(" OR DATEPART(dw, DATAOCORRENCIA) = {0})", mes);
            query.Append(" AND CODIGOVERBA = '4504'");
            query.Append(" AND IDVINCULOUSUARIO IN ");
            query.Append(" (SELECT IDVINCULOUSUARIO FROM TBVINCULOUSUARIO");
            query.Append(" WHERE IDREGIMEHORA IN(1,2,5,6,7,9,11,12,13,14,15,23));");
            lista.Add(query.ToString());
            if (lista.Count > 0)
                Util.ExecuteTransaction(lista);
        }

        #endregion

    }
}
