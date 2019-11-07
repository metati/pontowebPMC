using System;
using System.Data;
using System.Text;

namespace MetodosPontoFrequencia.Justificativa
{
    public class Justificativa
    {
        public Justificativa()
        {
        }


        public string ChangeStatusPedidoJustificativa(int StatusPedido, DateTime? DTExclusao, DateTime? DTAprovacao,
            int? IDVinculoUsuarioAprovacao, int IDVinculoUsuario, int IDFrequencia, DateTime DTFrequencia, int IDUsuario, string OBS)
        {
            DataSetPontoFrequenciaTableAdapters.TBFrequenciaJustificativa_PedidoTableAdapter adpFreqPedido =
                new DataSetPontoFrequenciaTableAdapters.TBFrequenciaJustificativa_PedidoTableAdapter();
            DataSetPontoFrequencia.TBFrequenciaJustificativa_PedidoDataTable TBFrequenciaJustPedido = new DataSetPontoFrequencia.TBFrequenciaJustificativa_PedidoDataTable();

            if (adpFreqPedido.UpdateStatusPedido(StatusPedido, DTExclusao, DTAprovacao, IDVinculoUsuarioAprovacao, OBS, IDFrequencia, DTFrequencia, IDUsuario) > 0)
            {
                return "Sucesso";
            }
            else
                return "falha";
        }


        public string SalvarPedidoJustificativa(int IDFrequencia, int IDMotivoFalta, string OBS, string DTJust, int? TotaDia, int TPJust, int IDEmpresa, string Index, int IDUsuario, int IDVinculoUsuario, int IDUsuarioOperador)
        {
            //-Acrescentado Dayan 06/01/2019 - Quando ainda não há justificativa (IDFrequencia). Salvar no banco o pedido
            //Caso o ID da frequencia seja zerado, salvar em TBFrequencia primeiro, após dar sequência

            //22/01/2019 - Colocar o total do dia quando este for null. Para Meio período e justificativa 
            //de registro único
            string retorno = "";
            Frequencia freq = new Frequencia();
            DataSetPontoFrequenciaTableAdapters.TBFrequenciaJustificativa_Pedido1TableAdapter adpPedido =
                new DataSetPontoFrequenciaTableAdapters.TBFrequenciaJustificativa_Pedido1TableAdapter();

            if (TotaDia == null)
            {
                TotaDia = 0;
            }

            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("DECLARE @IDFrequencia bigint");
                if (IDFrequencia != 0)
                {
                    sb.AppendLine("SET @IDFrequencia = " + IDFrequencia);
                    sb.AppendLine("UPDATE dbo.TBFrequencia SET SituacaoJustificativa = 1 where IDFrequencia = " + IDFrequencia);

                }
                else
                {
                    sb.AppendLine("INSERT INTO dbo.TBFrequencia");
                    sb.AppendLine("(IDUsuario, DTFrequencia, MesReferencia, AnoReferencia, IDEmpresa, IDVinculoUsuario, SituacaoJustificativa)");
                    sb.AppendLine("VALUES");
                    sb.AppendLine("(" + IDUsuario + ", CONVERT(DATETIME,'" + DTJust + "',103), DATEPART(MM,CONVERT(DATETIME,'" + DTJust + "',103)), DATEPART(YYYY,CONVERT(DATETIME,'" + DTJust + "',103)), " + IDEmpresa + "," + IDVinculoUsuario + ", 1)");
                    sb.AppendLine("SET @IDFrequencia = (SELECT SCOPE_IDENTITY())");
                }

                sb.AppendLine("INSERT INTO dbo.TBFrequenciaJustificativa_Pedido");
                sb.AppendLine("(IDFrequencia, IDMotivoFalta, DTJust, IDTPJustificativa,TotaDia, IndexU, IDUsuario, IDVinculoUsuario, IDEmpresa, OBS, StatusPedido)");
                sb.AppendLine("VALUES");
                sb.AppendLine("(@IDFrequencia, " + IDMotivoFalta + ", CONVERT(DATETIME,'" + DTJust + "',103), " + TPJust + "," + TotaDia + ", " + Index + "," + IDUsuario + ", " + IDVinculoUsuario + "," + IDEmpresa + ",'" + OBS + "', 2)");

                Util.ExecuteNonQuery(Util.GetSqlBeginTry(sb.ToString()));
                retorno = "sucesso";
            }
            catch
            {
                retorno = "false";
            }
            return retorno;
        }

        public string LogExcluirJustificativa(int IDFrequencia, int IdUsuario)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("DECLARE @IdJustificativaPedido INT");
            sb.AppendLine("SET @IdJustificativaPedido = (SELECT TOP 1 MAX(IDJustificativaPedido) FROM dbo.TBFrequenciaJustificativa_Pedido ");
            sb.AppendLine("WHERE IDFrequencia = " + IDFrequencia + ")");

            sb.AppendLine("IF((SELECT COUNT(*) FROM dbo.TBFrequenciaJustificativa_Pedido WHERE IDJustificativaPedido = @IdJustificativaPedido AND TipoJustificativa = 1 AND StatusPedido = 1) > 0)");
            sb.AppendLine("BEGIN");
            sb.AppendLine("UPDATE dbo.TBFrequenciaJustificativa_Pedido ");
            sb.AppendLine("SET StatusPedido = 0, DataExclusao = GETDATE(), IdUsuarioExclusao = " + IdUsuario);
            sb.AppendLine("WHERE IDFrequencia = " + IDFrequencia + " AND TipoJustificativa = 1 AND StatusPedido = 1");
            sb.AppendLine("UPDATE dbo.TBFrequencia SET SituacaoJustificativa = 0 where IDFrequencia = " + IDFrequencia);
            sb.AppendLine("END");

            sb.AppendLine("ELSE IF(@IdJustificativaPedido IS NOT NULL)");
            sb.AppendLine("BEGIN");
            sb.AppendLine("UPDATE dbo.TBFrequenciaJustificativa_Pedido ");
            sb.AppendLine("SET StatusPedido = 0, DataExclusao = GETDATE(), IdUsuarioExclusao = " + IdUsuario);
            sb.AppendLine("WHERE IDJustificativaPedido = @IdJustificativaPedido");
            //INSERE NOVAMENTE PEDIDO
            sb.AppendLine("INSERT INTO dbo.TBFrequenciaJustificativa_Pedido");
            sb.AppendLine("(IDFrequencia, IDMotivoFalta, DTJust, TotaDia, IndexU, IDTPJustificativa, IDUsuario, IDVinculoUsuario, IDEmpresa, OBS, StatusPedido, DataInclusao)");
            sb.AppendLine("SELECT IDFrequencia, IDMotivoFalta, DTJust, TotaDia, IndexU, IDTPJustificativa, IDUsuario, IDVinculoUsuario, IDEmpresa, OBS, 2, GETDATE()");
            sb.AppendLine("FROM dbo.TBFrequenciaJustificativa_Pedido");
            sb.AppendLine("WHERE IDJustificativaPedido = @IdJustificativaPedido");
            sb.AppendLine("UPDATE dbo.TBFrequencia SET SituacaoJustificativa = 1 where IDFrequencia = " + IDFrequencia);
            sb.AppendLine("END");

            Util.ExecuteNonQuery(Util.GetSqlBeginTry(sb.ToString()));

            return "";
        }

        public void InsertaLogJustificativaDireta(int IDFrequencia, int IDUsuario, int IDMotivoFalta, string OBS, DateTime DTFrequencia, int? TotalHorasDiaP, int IDTPJustificativa, int IDEmpresa, string OBSJustificativa, int IDUsuarioOperador, int IDVinculoUsuario, int SitJustificativa)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("DECLARE @IDFrequencia BIGINT");
            if (IDFrequencia == 0)
            {
                sb.AppendLine("IF(0 = 0)");
                sb.AppendLine("BEGIN");
                sb.AppendLine("SET @IDFrequencia = (SELECT IdFrequencia FROM TBFrequencia WHERE DTFrequencia = CONVERT(DATETIME,'" + DTFrequencia + "',103) AND IdUsuario = " + IDUsuario + " AND IdEmpresa = " + IDEmpresa + ")");
            }
            else
            {
                sb.AppendLine("IF((select COUNT(*) FROM TBFrequenciaJustificativa_Pedido WHERE StatusPedido = 1 AND TipoJustificativa = 0 AND IDFrequencia =" + IDFrequencia + ") = 0)");
                sb.AppendLine("BEGIN");
                sb.AppendLine("SET @IDFrequencia = " + IDFrequencia);
            }
            sb.AppendLine("INSERT INTO dbo.TBFrequenciaJustificativa_Pedido");
            sb.AppendLine("(IDFrequencia, IDMotivoFalta, DTJust, TotaDia, IndexU, IDTPJustificativa, IDUsuario, IDVinculoUsuario, IDEmpresa, OBS, StatusPedido, DataInclusao, IDVinculoUsuario_Aprovacao, DataAprovacao, TipoJustificativa)");
            sb.AppendLine("VALUES");
            sb.AppendLine("(@IDFrequencia, " + IDMotivoFalta + ", CONVERT(DATETIME,'" + DTFrequencia + "',103), NULL, " + IDTPJustificativa + 1 + ", " + IDTPJustificativa + 1 + ", " + IDUsuario + "," + IDVinculoUsuario + "," + IDEmpresa + ",'" + OBS + "', 1, GETDATE(), " + IDUsuarioOperador + ", GETDATE(), 1)");
            sb.AppendLine("END");
            try
            {
                Util.ExecuteNonQuery(sb.ToString());
            }
            catch { }
        }

        public int SalvarPedidoLocal(string Usuario, string Matricula, string Senha, string Observacao, int IDEmpresa, int IDSetor)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("DECLARE @Retorno int, @Obs varchar(500)");
            sb.AppendLine("SET @Obs =  '" + Observacao + "' + ' (' + (SELECT SiglaEmpresa FROM TBEmpresa WHERE IdEmpresa = " + IDEmpresa + ") + ' - ' + (SELECT DSSetor FROM TBSetor WHERE IDSetor = " + IDSetor + ") + ')'");
            sb.AppendLine("IF((SELECT COUNT(*) FROM TBUsuario WHERE Login = '" + Usuario + "') = 0)");
            sb.AppendLine("BEGIN");
            sb.AppendLine("	SET @Retorno = -1");
            sb.AppendLine("END");
            sb.AppendLine("ELSE IF((SELECT COUNT(*) FROM TBUsuario WHERE Login = '" + Usuario + "' AND SenhaAdmin = '" + Senha + "') = 0)");
            sb.AppendLine("BEGIN");
            sb.AppendLine("	SET @Retorno = -2");
            sb.AppendLine("END");
            sb.AppendLine("ELSE IF((SELECT COUNT(*) FROM TBUsuario A JOIN TBVinculoUsuario B ON (A.IdUsuario = B.IdUsuario) WHERE A.Login = '" + Usuario + "' AND A.SenhaAdmin = '" + Senha + "' AND B.Matricula = '" + Matricula + "') = 0)");
            sb.AppendLine("BEGIN");
            sb.AppendLine("	SET @Retorno = -3");
            sb.AppendLine("END");
            sb.AppendLine("ELSE");
            sb.AppendLine("BEGIN");

            //INSERT NO BANCO
            sb.AppendLine("DECLARE @IDFrequencia bigint, @IdVinculoUsuario bigint, @IDUsuario BIGINT");

            sb.AppendLine("SELECT @IdVinculoUsuario = B.IdVinculoUsuario, @IDUsuario = A.IDUsuario");
            sb.AppendLine("FROM TBUsuario A ");
            sb.AppendLine("JOIN TBVinculoUsuario B ON (A.IdUsuario = B.IdUsuario) ");
            sb.AppendLine("WHERE A.Login = '" + Usuario + "' AND A.SenhaAdmin = '" + Senha + "' AND B.Matricula = '" + Matricula + "'");

            sb.AppendLine("SET @IDFrequencia = (SELECT IDFrequencia FROM dbo.TBFrequencia WHERE IdVinculoUsuario = @IdVinculoUsuario AND DTFrequencia = CONVERT(DATE,GETDATE()))");
            //VERIFICA A FREQUENCIA
            sb.AppendLine("IF(@IDFrequencia IS NULL)");
            sb.AppendLine("BEGIN");
            sb.AppendLine("	INSERT INTO dbo.TBFrequencia");
            sb.AppendLine("    (IDUsuario, DTFrequencia, MesReferencia, AnoReferencia, IDEmpresa, IDVinculoUsuario, SituacaoJustificativa)");
            sb.AppendLine("    VALUES");
            sb.AppendLine("    (@IDUsuario, CONVERT(DATE,GETDATE()), DATEPART(MM,GETDATE()), DATEPART(YYYY,GETDATE()), " + IDEmpresa + ",@IdVinculoUsuario, 1)");
            sb.AppendLine("    SET @IDFrequencia = (SELECT SCOPE_IDENTITY())");
            sb.AppendLine("END");

            sb.AppendLine("UPDATE TBFrequenciaJustificativa_Pedido SET StatusPedido = 0 WHERE IDFrequencia = @IDFrequencia AND StatusPedido = 2");
            sb.AppendLine("INSERT INTO TBFrequenciaJustificativa_Pedido");
            sb.AppendLine("(IDFrequencia,IDMotivoFalta,DTJust,IndexU,IDTPJustificativa,IDUsuario,IDVinculoUsuario,IDEmpresa,OBS,StatusPedido,DataInclusao)");
            sb.AppendLine("VALUES");
            sb.AppendLine("(@IDFrequencia,241,CONVERT(DATE,GETDATE()),2,3,@IDUsuario,@IDVinculoUsuario," + IDEmpresa + ",@Obs,2,GETDATE())");


            sb.AppendLine("	SET @Retorno = 1");
            sb.AppendLine("END");
            sb.AppendLine("SELECT @Retorno");
            int retorno = int.Parse(Util.getScalar(Util.GetSqlBeginTry(sb.ToString())));

            return retorno;
        }
        public void AtualizaSituacaoJustificativa(int IDFrequencia, int IdUsuario, int situacao)
        {
            try
            {
                StringBuilder query = new StringBuilder();
                query.AppendFormat("  UPDATE TBFrequencia");
                query.AppendFormat(" Set SituacaoJustificativa = {0}", situacao);
                query.AppendFormat(" Where IDFrequencia = {0}", IDFrequencia);
                query.AppendFormat(" AND IDUsuario = {0}", IdUsuario);
                Util.ExecuteNonQuery(Util.GetSqlBeginTry(query.ToString()));
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public bool ValidaMotivoExiste(string motivo)
        {
            try
            {
                StringBuilder query = new StringBuilder();
                query.AppendFormat(" select IDMotivoFalta  from TBMotivoFalta where IDMotivoFalta = {0} and IDstatus = 1", motivo);
                IDataReader reader = Util.getDataReader(query.ToString());
                using (reader)
                {
                    if (reader.Read())
                    {
                        return true;
                    }
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }


        }

        public string GetIdUserByVinculo(string vinculoId)
        {
            string IdUsuario = string.Empty;
            string query = string.Format("select IDUsuario from TBVinculoUsuario where IDVinculoUsuario = {0}", vinculoId);
            IDataReader reader = Util.getDataReader(query);
            using (reader)
            {
                if (reader.Read())
                {
                    IdUsuario = reader["IDUsuario"].ToString();
                }
            }

            return IdUsuario;
        }

        public bool DataIsenta(DateTime dataFrenquencia, int usuario, int vinculoID)
        {
            try
            {
                string query = string.Format(" SELECT dbo.DATA_ISENTA ( CONVERT(DATETIME,'{0}',103),{1},{2})", dataFrenquencia, usuario, vinculoID);
                var result = Util.getScalar(query);
                if (result == "S")
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw;
            }


        }

    }
}
