using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MetodosPontoFrequencia.HorasExtras
{
    public class PermissoesDAO
    {
        public void IncluirPermissoes(List<object> listaItens, string dataInicio, string dataFim, string idUsuario)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in listaItens)
            {
                sb.AppendLine("INSERT INTO TBHorasExtrasPermissoes (IDVinculoUsuario, DataInicio, DataFim, IdUsuarioInclusao) VALUES (" + Util.TratarString(item.ToString()) + "," + Util.TratarDataINSERT(dataInicio) + "," + Util.TratarDataINSERT(dataFim) + ", " + idUsuario + ")");
            }
            sb.ToString();
            Util.ExecuteNonQuery(sb.ToString());
        }

        public DataTable GetPermissoes(string IdEmpresa, string IdSetor, string Nome, string Matricula)
        {
            StringBuilder sb = new StringBuilder();
            string where = "";
            if (!string.IsNullOrEmpty(Nome))
            {
                where += " AND B.DSUsuario like '%" + Util.TratarString(Nome) + "%'";
            }
            if (!string.IsNullOrEmpty(Matricula))
            {
                where += " AND C.Matricula like '" + Util.TratarString(Matricula) + "'";
            }
            if (!string.IsNullOrEmpty(IdSetor))
            {
                where += " AND C.IDSetor = " + Util.TratarString(IdSetor);
            }
            sb.AppendLine("SELECT DISTINCT A.IdHorasExtrasPermissao Codigo, A.IDVinculoUsuario CodigoUsuario,B.DSUsuario Nome,B.Matricula,A.DataInicio,A.DataFim");
            sb.AppendLine("FROM PontoFrequenciaPMC.dbo.TBHorasExtrasPermissoes A");
            sb.AppendLine("JOIN PontoFrequenciaPMC.dbo.TBVinculoUsuario C ON(A.IDVinculoUsuario = C.IDVinculoUsuario)");
            sb.AppendLine("JOIN PontoFrequenciaPMC.dbo.TBUsuario B ON(C.IDUsuario = B.IDUsuario)");
            sb.AppendLine("WHERE C.IDEmpresa = " + IdEmpresa);
            sb.AppendLine(where);
            return Util.getDatset(sb.ToString());
        }

        public void DeletarPermissoes(List<object> listaItens)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in listaItens)
            {
                sb.AppendLine("DELETE FROM TBHorasExtrasPermissoes WHERE IdHorasExtrasPermissao = " + Util.TratarString(item.ToString()) + ";");
            }
            sb.ToString();
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
            sb.AppendLine("WHERE B.IDEmpresa = " + IdEmpresa);
            sb.AppendLine(where);
            return Util.getDatset(sb.ToString());
        }

        public DataTable GetLista(string idUsuario, string Mes)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT");
            sb.AppendLine("	   CONVERT(VARCHAR(12),DTFrequencia,103) Data");
            sb.AppendLine("	  ,CONVERT(VARCHAR(5),CONVERT(TIME(4),A.HoraEntraManha)) HoraEntra1");
            sb.AppendLine("	  ,CONVERT(VARCHAR(5),CONVERT(TIME(4),A.HoraSaidaManha)) HoraSaida1");
            sb.AppendLine("	  ,CONVERT(VARCHAR(5),CONVERT(TIME(4),A.HoraEntradaTarde)) HoraEntrada2");
            sb.AppendLine("	  ,CONVERT(VARCHAR(5),CONVERT(TIME(4),A.HoraSaidaTarde)) HoraSaida2");
            sb.AppendLine("	  ,PontoFrequenciaPMC.dbo.ConvertDecimalToTime(CONVERT(DECIMAL(12,4),TotalHorasDiaSegundos)/3600) Jornada");
            sb.AppendLine("	  ,CASE WHEN ((IdHorasExtrasPermissao IS NOT NULL OR E.RegimePlantonista = 1) AND (CONVERT(DECIMAL(12,4),TotalHorasDiaSegundos)/3600-E.TotalHoraDia) < 0) THEN PontoFrequenciaPMC.dbo.ConvertDecimalToTime((CONVERT(DECIMAL(12,4),TotalHorasDiaSegundos)/3600-E.TotalHoraDia)) END HorasFalta");
            sb.AppendLine("	  ,CASE WHEN ((IdHorasExtrasPermissao IS NOT NULL OR E.RegimePlantonista = 1) AND (CONVERT(DECIMAL(12,4),TotalHorasDiaSegundos)/3600-E.TotalHoraDia) > 0) THEN PontoFrequenciaPMC.dbo.ConvertDecimalToTime((CONVERT(DECIMAL(12,4),TotalHorasDiaSegundos)/3600-E.TotalHoraDia)) END HorasExtras");
            sb.AppendLine("	  ,CASE WHEN ((IdHorasExtrasPermissao IS NOT NULL OR E.RegimePlantonista = 1)) THEN PontoFrequenciaPMC.dbo.CalculaAdNoturno(A.HoraEntraManha, A.HoraSaidaManha, A.HoraEntradaTarde, A.HoraSaidaTarde) END AdiNoturno");
            sb.AppendLine("    ,OBS Observacao");
            sb.AppendLine("  INTO #TEMP  ");
            sb.AppendLine("  FROM PontoFrequenciaPMC_BKP.dbo.TBFrequencia A");
            sb.AppendLine("  JOIN PontoFrequenciaPMC_BKP.dbo.TBVinculoUsuario B ON (A.IDVinculoUsuario = B.IDVinculoUsuario)");
            sb.AppendLine("  JOIN PontoFrequenciaPMC_BKP.dbo.TBUsuario C ON (C.IdUsuario = B.IdUsuario)");
            sb.AppendLine("  JOIN PontoFrequenciaPMC_BKP.dbo.TBEmpresa D ON (D.IdEmpresa = B.IdEmpresa)");
            sb.AppendLine("  JOIN PontoFrequenciaPMC_BKP.dbo.TBRegimeHora E ON (E.IDRegimeHora = B.IDRegimeHora)");
            sb.AppendLine("  LEFT JOIN ");
            sb.AppendLine("  (");
            sb.AppendLine("	 SELECT MAX(IdHorasExtrasPermissao) IdHorasExtrasPermissao, DataFim, DataInicio, IDVinculoUsuario");
            sb.AppendLine("	 FROM PontoFrequenciaPMC.dbo.TBHorasExtrasPermissoes     ");
            sb.AppendLine("	 GROUP BY DataFim, DataInicio, IDVinculoUsuario");
            sb.AppendLine("  ) F ON  (A.DTFrequencia BETWEEN F.DataInicio AND (isNull(F.DataFim, (A.DTFrequencia)+360)) AND F.IDVinculoUsuario = A.IDVinculoUsuario)");
            sb.AppendLine("  WHERE ");
            sb.AppendLine("  A.IDVinculoUsuario = " + idUsuario);
            sb.AppendLine("  AND AnoReferencia = DATEPART(YYYY,GETDATE())");
            sb.AppendLine("  AND MesReferencia = " + Mes);
            sb.AppendLine("  ORDER BY A.IDVinculoUsuario,DTFrequencia");
            sb.AppendLine("  UPDATE #TEMP");
            sb.AppendLine("  SET HorasExtras = '02:00'");
            sb.AppendLine("  WHERE(CONVERT(DECIMAL(18, 4), (DATEPART(SECOND, HorasExtras) + 60 * DATEPART(MINUTE, HorasExtras) + 3600 * DATEPART(HOUR, HorasExtras))) / 3600) > 2");
            
            sb.AppendLine("SELECT CONVERT(VARCHAR(12),A.DTDiasAno,103) + ' - ' + CONVERT(VARCHAR(3),PontoFrequenciaPMC.dbo.DiaSemana(DATEPART(DW, A.DTDiasAno))) DATA, B.HoraEntra1 'Hora Entrada 1', B.HoraSaida1 'Hora Saida 1', B.HoraEntrada2 'Hora Entrada 2', B.HoraSaida2 'Hora Saida 2', B.Jornada, B.HorasFalta, B.HorasExtras, B.AdiNoturno, B.Observacao");
            sb.AppendLine("INTO #TEMP2");
            sb.AppendLine("FROM PontoFrequenciaPMC.dbo.TBDiasAno A");
            sb.AppendLine("LEFT JOIN #TEMP B ON (A.DTDiasAno = CONVERT(DATETIME,B.Data,103))");
            sb.AppendLine("WHERE IdEmpresa = 45");
            sb.AppendLine("AND DATEPART(YYYY,A.DTDiasAno) = DATEPART(YYYY,CONVERT(DATETIME,GETDATE(),103))");
            sb.AppendLine("AND DATEPART(MM,A.DTDiasAno) = " + Mes);
            sb.AppendLine("ORDER BY A.DTDiasAno");


            sb.AppendLine("INSERT INTO #TEMP2(Jornada, HorasExtras, AdiNoturno)");
            sb.AppendLine("SELECT PontoFrequenciaPMC.dbo.ConvertDecimalToTime((CONVERT(DECIMAL(18,4),sum( DATEPART(SECOND, Jornada) + 60 * DATEPART(MINUTE, Jornada) + 3600 * DATEPART(HOUR, Jornada)))/3600)) Jornada, ");
            sb.AppendLine("PontoFrequenciaPMC.dbo.ConvertDecimalToTime((CONVERT(DECIMAL(18,4),sum( DATEPART(SECOND, HorasExtras) + 60 * DATEPART(MINUTE, HorasExtras) + 3600 * DATEPART(HOUR, HorasExtras)))/3600)) HorasExtras,");
            sb.AppendLine("PontoFrequenciaPMC.dbo.ConvertDecimalToTime((CONVERT(DECIMAL(18,4),sum( DATEPART(SECOND, AdiNoturno) + 60 * DATEPART(MINUTE, AdiNoturno) + 3600 * DATEPART(HOUR, AdiNoturno)))/3600)) AdiNoturno");
            sb.AppendLine("FROM #TEMP2");

            sb.AppendLine("SELECT * FROM #TEMP2");
            return Util.getDatset(sb.ToString());
        }

    }
}
