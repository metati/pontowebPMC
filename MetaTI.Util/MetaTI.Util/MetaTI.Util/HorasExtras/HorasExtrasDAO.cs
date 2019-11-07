using MetaTI.Util.Model;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace MetaTI.Util.HorasExtras
{
    public class HorasExtrasDAO
    {
        public void Incluir(string IDVinculoUsuario, string CodVerba, string data, string QtdeHoras, string idUsuarioInclusao, string idEmpresa)
        {
            IDVinculoUsuario = Util.TratarString(IDVinculoUsuario);
            CodVerba = Util.TratarString(CodVerba);
            QtdeHoras = Util.TratarString(QtdeHoras);
            string dataJust = Util.TratarString(data);
            data = Util.TratarDataINSERT(data);
            idEmpresa = Util.TratarString(idEmpresa);
            idUsuarioInclusao = Util.TratarString(idUsuarioInclusao);
            #region VERIFICA
            StringBuilder sbScalar = new StringBuilder();
            sbScalar.AppendLine("DECLARE @HorasMes decimal(18,2), @HorasDia decimal(18,2), @DataOcorrencia datetime, @SegundosDia bigint, @SegundosMes bigint, @IdHorasExtrasPermissao int, @HorasForm time(4), @SegundosForm int ");
            sbScalar.AppendLine("SET @DataOcorrencia = " + data);
            sbScalar.AppendLine("SET @SegundosMes = (SELECT sum( DATEPART(SECOND, TotalHoras) + 60 * DATEPART(MINUTE, TotalHoras) + 3600 * DATEPART(HOUR, TotalHoras )) FROM dbo.TBHorasExtrasLancamentos ");
            sbScalar.AppendLine("			   WHERE DATEPART(YYYY,DataOcorrencia) = DATEPART(YYYY,@DataOcorrencia) AND DATEPART(MM,DataOcorrencia) = DATEPART(MM,@DataOcorrencia) AND CodigoVerba = " + CodVerba + " AND IDVinculoUsuario = " + IDVinculoUsuario + ")");
            sbScalar.AppendLine("SET @SegundosDia = (SELECT sum( DATEPART(SECOND, TotalHoras) + 60 * DATEPART(MINUTE, TotalHoras) + 3600 * DATEPART(HOUR, TotalHoras )) FROM dbo.TBHorasExtrasLancamentos ");
            sbScalar.AppendLine("			   WHERE DataOcorrencia = @DataOcorrencia AND CodigoVerba = " + CodVerba + " AND IDVinculoUsuario = " + IDVinculoUsuario + ")");
            sbScalar.AppendLine("SET @IdHorasExtrasPermissao = (SELECT MAX(IdHorasExtrasPermissao) FROM PontoFrequenciaPMC.dbo.TBHorasExtrasPermissoes WHERE @DataOcorrencia BETWEEN DataInicio AND (isNull(DataFim, @DataOcorrencia+360)) AND IDVinculoUsuario = " + IDVinculoUsuario + ")");
            sbScalar.AppendLine("SET @HorasForm = CONVERT(TIME, '" + QtdeHoras + "')");
            sbScalar.AppendLine("SET @SegundosForm = sum(DATEPART(SECOND, @HorasForm) + 60 * DATEPART(MINUTE, @HorasForm) + 3600 * DATEPART(HOUR, @HorasForm))");
            sbScalar.AppendLine("SET @HorasMes = ((@SegundosMes + @SegundosForm) / 3600)");
            sbScalar.AppendLine("SET @HorasDia = ((@SegundosDia + @SegundosForm) / 3600)");
            sbScalar.AppendLine("IF(@SegundosForm <= 0)");
            sbScalar.AppendLine("BEGIN");
            sbScalar.AppendLine("	RAISERROR ('ESTE VALOR NÃO É PERMITIDO, FAVOR VERIFICAR !', 16, 1 );");
            sbScalar.AppendLine("END");
            sbScalar.AppendLine("IF(@IdHorasExtrasPermissao IS NULL)");
            sbScalar.AppendLine("BEGIN");
            sbScalar.AppendLine("	RAISERROR ('ESTA DATA NÃO É PERMITIDA, FAVOR VERIFICAR!', 16, 1 );");
            sbScalar.AppendLine("END");
            sbScalar.AppendLine("IF((@HorasMes >= 40.1 AND " + CodVerba + " = 1235) OR (@HorasMes > 129 AND " + CodVerba + " = 1244))");
            sbScalar.AppendLine("BEGIN");
            sbScalar.AppendLine("	RAISERROR ('O LIMITE MÁXIMO DE HORAS DO MÊS JÁ FOI PREENCHIDO', 16, 1 );");
            sbScalar.AppendLine("END");
            sbScalar.AppendLine("IF(@HorasDia >= 2.1 AND " + CodVerba + " = 1235)");
            sbScalar.AppendLine("BEGIN");
            sbScalar.AppendLine("	RAISERROR ('O LIMITE MÁXIMO DE HORAS DO DIA JÁ FOI PREENCHIDO', 16, 1 );");
            sbScalar.AppendLine("END ");
            sbScalar.AppendLine("IF(@HorasDia >= 7.1 AND " + CodVerba + " = 1244)");
            sbScalar.AppendLine("BEGIN");
            sbScalar.AppendLine("	RAISERROR ('O LIMITE MÁXIMO DE HORAS DO DIA JÁ FOI PREENCHIDO', 16, 1 );");
            sbScalar.AppendLine("END ");
            Util.getScalar(sbScalar.ToString());
            #endregion VERIFICA
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("DECLARE @IdFrequencia int,@IdUsuario bigint, @Horas time, @Segundo bigint, @TipoVerba varchar(20), @IdHorasExtrasPermissao INT");
            sb.AppendLine("SET @Horas = CONVERT(TIME(4),'" + QtdeHoras + "')");
            sb.AppendLine("SET @Segundo = (SELECT sum( DATEPART(SECOND, @Horas) + 60 * DATEPART(MINUTE, @Horas) + 3600 * DATEPART(HOUR, @Horas )))");
            sb.AppendLine("SET @IdUsuario = (SELECT IDUsuario FROM PontoFrequenciaPMC.dbo.TBVinculoUsuario WHERE IDVinculoUsuario = " + IDVinculoUsuario + ")");
            sb.AppendLine("SET @IdFrequencia = (SELECT MAX(IdFrequencia) FROM TBFrequencia WHERE DTFrequencia =  " + data + " AND IDVinculoUsuario = " + IDVinculoUsuario + ")");
            //VERIFICA SE TEM REGISTRO DE PONTO, SE NÃO, INSERE UMA JUSTIFICATIVA
            //sb.AppendLine("IF(@IdFrequencia IS NULL)");
            //sb.AppendLine("BEGIN");
            //sb.AppendLine("SET @TipoVerba = (CASE WHEN " + CodVerba + " = 1235 THEN 'HORAS EXTRAS' ELSE 'ADICIONAL NOTURNO' END)");
            //sb.AppendLine("INSERT INTO PontoFrequenciaPMC.dbo.TBFrequencia");
            //sb.AppendLine("(IdUsuario,IDVinculoUsuario, IDEmpresa, DTFrequencia, OBS, MesReferencia, AnoReferencia)");
            //sb.AppendLine("VALUES");
            //sb.AppendLine("(@IdUsuario," + IDVinculoUsuario + ", " + idEmpresa + ", " + data + ", 'LANÇAMENTO DE MANUAL DE ' + @TipoVerba + ' EM " + dataJust + ", PORÉM NÃO HOUVE REGISTRO DE PONTO', DATEPART(MM, " + data + "), DATEPART(YYYY, " + data + "))");
            //sb.AppendLine("SET @IdFrequencia = (SELECT SCOPE_IDENTITY())");
            //sb.AppendLine("END");
            //PEGA O ID DA PERMISSÃO
            sb.AppendLine("SET @IdHorasExtrasPermissao = (SELECT MAX(IdHorasExtrasPermissao) FROM PontoFrequenciaPMC.dbo.TBHorasExtrasPermissoes WHERE " + data + " BETWEEN DataInicio AND (isNull(DataFim, (" + data + ")+360)) AND IDVinculoUsuario = " + IDVinculoUsuario + ")");

            sb.AppendLine("INSERT INTO dbo.TBHorasExtrasLancamentos");
            sb.AppendLine("(IDVinculoUsuario, IdFrequencia, IdHorasExtrasPermissao,CodigoVerba, DataOcorrencia, TotalHoras, TotalSegundos,IdUsuarioRegistro)");
            sb.AppendLine("VALUES");
            sb.AppendLine("(" + IDVinculoUsuario + ", @IdFrequencia, @IdHorasExtrasPermissao," + CodVerba + ", " + data + ", @Horas, @Segundo," + idUsuarioInclusao + ")");
            Util.ExecuteNonQuery(Util.GetSqlBeginTry(sb.ToString()));
        }

        public DataTable GetLista(string idUsuario)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT Sequencial Codigo,CodigoVerba, ");
            sb.AppendLine("CASE ");
            sb.AppendLine("	WHEN CodigoVerba = 1244 THEN 'Adicional Noturno' ");
            sb.AppendLine("	WHEN CodigoVerba = 1235 THEN 'Hora Extra' ");
            sb.AppendLine("	WHEN CodigoVerba = 4544 THEN 'Atraso No Ponto' ");
            sb.AppendLine("	WHEN CodigoVerba = 4504 THEN 'FALTA INJUSTIFICADA' ");
            sb.AppendLine("	WHEN CodigoVerba = 20142 THEN 'FALTA INJUSTIFICADA - LIMPURB' ");
            sb.AppendLine("END Verba, ");
            sb.AppendLine("DataOcorrencia, TotalHoras");
            sb.AppendLine("FROM TBHorasExtrasLancamentos");
            sb.AppendLine("WHERE IDVinculoUsuario = " + idUsuario);
            sb.AppendLine("AND CodigoVerba IN (1244, 1235)");
            return Util.getDatset(sb.ToString());
        }

        public void DeletarPermissoes(List<object> list)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in list)
            {
                //sb.AppendLine("DECLARE @IDFrequencia int SET @IDFrequencia = (SELECT IDFrequencia FROM TBHorasExtrasLancamentos WHERE Sequencial = " + item.ToString() + "); DELETE FROM TBHorasExtrasLancamentos WHERE Sequencial = " + item.ToString() + "; DELETE FROM TBFrequencia WHERE IDFrequencia = @IDFrequencia ");
                sb.AppendLine("DELETE FROM TBHorasExtrasLancamentos WHERE Sequencial = " + item.ToString());
            }
            Util.ExecuteNonQuery(sb.ToString());
        }

        public DataTable GetUsuarios(string IdEmpresa, string IdSetor, string Nome, string Matricula)
        {
            StringBuilder sb = new StringBuilder();
            string where = "";
            if (!string.IsNullOrEmpty(Nome))
            {
                where += " AND A.DSUsuario like '%" + Util.TratarString(Nome) + "%'";
            }
            if (!string.IsNullOrEmpty(Matricula))
            {
                where += " AND B.Matricula like '" + Util.TratarString(Matricula) + "'";
            }
            if (!string.IsNullOrEmpty(IdSetor))
            {
                where += " AND B.IDSetor = " + Util.TratarString(IdSetor);
            }
            sb.AppendLine("SELECT DISTINCT B.IDVinculoUsuario Codigo, A.DSUsuario, isNull(B.Matricula, '') Matricula");
            sb.AppendLine("FROM TBUsuario A");
            sb.AppendLine("JOIN TBVinculoUsuario B ON (A.IDUsuario = B.IDUsuario)");
            sb.AppendLine("JOIN TBHorasExtrasPermissoes C ON (C.IDVinculoUsuario = B.IDVinculoUsuario)");

            sb.AppendLine("WHERE B.IDEmpresa = " + IdEmpresa);
            sb.AppendLine(where);
            return Util.getDatset(sb.ToString());
        }

        public List<HorasDiaModel> GetDadosWS(string IDEmpresaOrgao_eTurmalina, string MesRef, string AnoRef)
        {
            StringBuilder sb = new StringBuilder();
            string sqlWhere = "";
            if (!string.IsNullOrEmpty(IDEmpresaOrgao_eTurmalina) && IDEmpresaOrgao_eTurmalina != "0")
            {
                sqlWhere += " AND E.IDEmpresa_eTurmalina = " + IDEmpresaOrgao_eTurmalina;
            }
            if (!string.IsNullOrEmpty(MesRef))
            {
                sqlWhere += " AND DATEPART(MM,HL.DataOcorrencia) = " + MesRef;
            }
            if (!string.IsNullOrEmpty(AnoRef))
            {
                sqlWhere += " AND DATEPART(YYYY,HL.DataOcorrencia) = " + AnoRef;
            }
            sb.AppendLine("SELECT DISTINCT VU.Matricula,  U.Login CPF, CONVERT(VARCHAR(5),isNull(HL.TotalHoras,'00:00'), 108) Hora, HL.CodigoVerba,  CONVERT(VARCHAR(12),HL.DataOcorrencia,103) DataOcorrencia, '' Situacao");
            sb.AppendLine("FROM dbo.TBHorasExtrasLancamentos HL WITH(NOLOCK)");
            sb.AppendLine("JOIN dbo.TBVinculoUsuario VU WITH(NOLOCK) ON (VU.IdVinculoUsuario = HL.IdVinculoUsuario)");
            sb.AppendLine("JOIN dbo.TBUsuario U WITH(NOLOCK) ON (U.IdUsuario = VU.IdUsuario)");
            sb.AppendLine("JOIN dbo.TBEmpresa E WITH(NOLOCK) ON (VU.IdEmpresa = E.IDEmpresa)");
            sb.AppendLine("WHERE 1=1");
            sb.AppendLine(sqlWhere);
            SqlDataReader dr = Util.getDataReader(sb.ToString());
            List<HorasDiaModel> list = new List<HorasDiaModel>();
            try
            {
                while (dr.Read())
                {
                    list.Add(new HorasDiaModel
                    {
                        Matricula = dr["Matricula"].ToString(),
                        CPF = dr["CPF"].ToString(),
                        Hora = dr["Hora"].ToString(),
                        CodVerba = dr["CodigoVerba"].ToString(),
                        Data = dr["DataOcorrencia"].ToString(),
                        Situacao = dr["Situacao"].ToString(),
                    });
                }
            }
            catch { }
            finally { dr.Close(); }
            return list;
        }
    }
}
