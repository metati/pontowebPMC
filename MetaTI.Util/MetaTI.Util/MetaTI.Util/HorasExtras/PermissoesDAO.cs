using MetodosPontoFrequencia;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MetaTI.Util.HorasExtras
{
    public class PermissoesDAO
    {
        public string DataDesconsidera
        {
            get
            {
                return Acesso.DataDesconsidera;
            }
        }

        public string ObrigaQuatroBatidas
        {
            get
            {
                return Acesso.ObrigaQuatroBatidas;
            }
        }
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
            sb.AppendLine("FROM dbo.TBHorasExtrasPermissoes A");
            sb.AppendLine("JOIN dbo.TBVinculoUsuario C ON(A.IDVinculoUsuario = C.IDVinculoUsuario)");
            sb.AppendLine("JOIN dbo.TBUsuario B ON(C.IDUsuario = B.IDUsuario)");
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

        public DataTable GetLista(string idUsuario, string Mes, string Ano)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("");
            sb.AppendLine("DECLARE @IDVinculoUsuario INT");
            sb.AppendLine("SET @IDVinculoUsuario = " + idUsuario);

            string RegimePlantonista = Util.getScalar("SELECT R.RegimePlantonista FROM TBVinculoUsuario V JOIN TBRegimeHora R ON(R.IDRegimeHora = V.IDRegimeHora) WHERE V.IDVinculoUsuario = " + idUsuario);
            if (RegimePlantonista == "True")
            {
                sb.AppendLine(GetSelectFolhaPlantonista(idUsuario, Mes, Ano));
            }
            else
            {
                sb.AppendLine(GetSelectFolhaNaoPlantonista(idUsuario, Mes, Ano));
            }

            sb.AppendLine("SELECT CONVERT(VARCHAR(12), A.DTFrequencia,103) + ' - ' + CONVERT(VARCHAR(3), dbo.DiaSemana(DATEPART(DW, A.DTFrequencia))) DATA,");
            sb.AppendLine("HoraEntra1 'Hora Entrada 1', HoraSaida1 'Hora Saida 1', HoraEntrada2 'Hora Entrada 2', HoraSaida2 'Hora Saida 2', Jornada, CASE WHEN Observacao = 'ATRASO NO PONTO' THEN CONVERT(VARCHAR(5),HorasFaltantes) ELSE ''END TotalAtraso,");
            sb.AppendLine("HorasExtras, AdiNoturno, Observacao");
            sb.AppendLine("FROM #TEMP A");
            sb.AppendLine("ORDER BY A.DTFrequencia");

            return Util.getDatset(sb.ToString());
        }

        #region SQL CONFERENCIA FOLHA
        private string GetSelectFolhaNaoPlantonista(string idUsuario, string Mes, string Ano)
        {

            StringBuilder sb = new StringBuilder();
            if (ObrigaQuatroBatidas.Equals("S") && DataDesconsidera != string.Empty)
            {
                sb.AppendLine("DECLARE  @DataDesconsidera date");
                sb.AppendLine("SET @DataDesconsidera = convert(date,'" + DataDesconsidera + "',103)");
            }
            sb.AppendLine("DECLARE @IdEmpresa int");
            sb.AppendLine("SET @IdEmpresa = (SELECT IDEmpresa FROM TBVinculoUsuario WHERE IDVinculoUsuario = @IDVinculoUsuario)");
            sb.AppendLine("SELECT");
            sb.AppendLine("	  A.IDVinculoUsuario");
            sb.AppendLine("	  ,DTFrequencia");
            sb.AppendLine("	  ,CONVERT(VARCHAR(5),CONVERT(TIME(4),A.HoraEntraManha)) HoraEntra1");
            sb.AppendLine("	  ,CONVERT(VARCHAR(5),CONVERT(TIME(4),A.HoraSaidaManha)) HoraSaida1");
            sb.AppendLine("	  ,CONVERT(VARCHAR(5),CONVERT(TIME(4),A.HoraEntradaTarde)) HoraEntrada2");
            sb.AppendLine("	  ,CONVERT(VARCHAR(5),CONVERT(TIME(4),A.HoraSaidaTarde)) HoraSaida2");
            sb.AppendLine("	  ,E.TotalHoraDia");
            sb.AppendLine("	  ,dbo.ConvertDecimalToTime(CONVERT(DECIMAL(12,4),TotalHorasDiaSegundos)/3600) Jornada");
            sb.AppendLine("	  ,CASE WHEN ((CONVERT(DECIMAL(12,4),TotalHorasDiaSegundos)/3600-E.TotalHoraDia) < 0) THEN dbo.ConvertDecimalToTime(ABS((CONVERT(DECIMAL(12,4),TotalHorasDiaSegundos)-(E.TotalHoraDia*3600))/3600)) END HorasFalta");
            sb.AppendLine("	  ,CASE WHEN ((IdHorasExtrasPermissao IS NOT NULL OR E.RegimePlantonista = 1) AND (CONVERT(DECIMAL(12,4),TotalHorasDiaSegundos)/3600-E.TotalHoraDia) > 0) THEN dbo.ConvertDecimalToTime((CONVERT(DECIMAL(12,4),TotalHorasDiaSegundos)/3600-E.TotalHoraDia)) END HorasExtras");
            sb.AppendLine("	  ,dbo.CalculaAdNoturno(A.HoraEntraManha, A.HoraSaidaManha, A.HoraEntradaTarde, A.HoraSaidaTarde) AdiNoturno");
            sb.AppendLine("    ,OBS Observacao");
            sb.AppendLine("	,IdHorasExtrasPermissao");
            sb.AppendLine("	,E.RegimePlantonista");
            sb.AppendLine("	,TotalHorasDiaSegundos");
            sb.AppendLine("	,IDMotivoFalta");
            sb.AppendLine("	,B.IDEmpresa");
            sb.AppendLine("	,dbo.CalculaAtrazoPonto(CONVERT(TIME,A.HoraEntraManha), CONVERT(TIME,A.HoraSaidaManha), CONVERT(TIME,A.HoraEntradaTarde), CONVERT(TIME,A.HoraSaidaTarde), ");
            sb.AppendLine("	 B.HoraEntradaManha, B.HoraSaidaManha, B.HoraEntradaTarde, B.HoraSaidaTarde) MinutosFaltantes");
            sb.AppendLine("	 , CONVERT(TIME,'00:00') HorasFaltantes, DTInicioVinculo");
            sb.AppendLine("  ,B.DescontoTotalJornada");
            sb.AppendLine("  INTO #TEMP  ");
            sb.AppendLine("  FROM dbo.TBFrequencia A WITH(NOLOCK)");
            sb.AppendLine("  JOIN dbo.TBVinculoUsuario B WITH(NOLOCK) ON (A.IDVinculoUsuario = B.IDVinculoUsuario)");
            sb.AppendLine("  JOIN dbo.TBUsuario C WITH(NOLOCK) ON (C.IdUsuario = B.IdUsuario)");
            sb.AppendLine("  JOIN dbo.TBEmpresa D WITH(NOLOCK) ON (D.IdEmpresa = B.IdEmpresa)");
            sb.AppendLine("  JOIN dbo.TBRegimeHora E WITH(NOLOCK) ON (E.IDRegimeHora = B.IDRegimeHora)");
            sb.AppendLine("  LEFT JOIN ");
            sb.AppendLine("  (");
            sb.AppendLine("	 SELECT MAX(IdHorasExtrasPermissao) IdHorasExtrasPermissao, MAX(DataFim) DataFim, MIN(DataInicio) DataInicio, IDVinculoUsuario");
            sb.AppendLine("	 FROM dbo.TBHorasExtrasPermissoes WITH(NOLOCK)     ");
            sb.AppendLine("	 GROUP BY IDVinculoUsuario");
            sb.AppendLine("  ) F ON  (A.DTFrequencia BETWEEN F.DataInicio AND (isNull(F.DataFim, (A.DTFrequencia)+360)) AND F.IDVinculoUsuario = A.IDVinculoUsuario)");
            sb.AppendLine("  WHERE ");
            sb.AppendLine("  A.IDVinculoUsuario = @IDVinculoUsuario");
            sb.AppendLine("  AND AnoReferencia = " + Ano);
            sb.AppendLine("  AND (IsencaoPonto IS NULL OR IsencaoPonto = 0) ");
            sb.AppendLine("  AND MesReferencia = " + Mes);
            sb.AppendLine("  ORDER BY A.IDVinculoUsuario,DTFrequencia");

            sb.AppendLine("  --VERIFICA HORAS EXCEDENTES");
            sb.AppendLine("  UPDATE #TEMP");
            sb.AppendLine("  SET HorasExtras = '02:00'");
            sb.AppendLine("  WHERE(CONVERT(DECIMAL(18, 4), (DATEPART(SECOND, HorasExtras) + 60 * DATEPART(MINUTE, HorasExtras) + 3600 * DATEPART(HOUR, HorasExtras))) / 3600) > 2");

            sb.AppendLine(" --SETA EM FORMATO HORAS OS MINUTOS EM ATRASO");
            sb.AppendLine(" UPDATE #TEMP");
            sb.AppendLine(" SET HorasFaltantes =  CONVERT(TIME,convert(char(8), dateadd(MINUTE, MinutosFaltantes, ''), 114))");


            sb.AppendLine("  --SETA EM MinutosFaltantes o campo HorasFaltantes convertido em minutos, caso o DescontoTotalJornada for igual a 1");
            sb.AppendLine("  IF((SELECT TOP 1 DescontoTotalJornada FROM #TEMP) = 1)");
            sb.AppendLine("  BEGIN");
            sb.AppendLine("  UPDATE #TEMP");
            sb.AppendLine("  SET MinutosFaltantes = CASE WHEN(DATEDIFF(MINUTE, 0, HorasFalta)) > 15 THEN(DATEDIFF(MINUTE, 0, HorasFalta)) ELSE NULL END, HorasFaltantes = HorasFalta");
            sb.AppendLine("  END");


            sb.AppendLine("UPDATE A");
            sb.AppendLine("SET A.Observacao = 'ATRASO NO PONTO'");
            sb.AppendLine("FROM #TEMP A");
            sb.AppendLine("WHERE (MinutosFaltantes*60) BETWEEN 1 AND 900");
            sb.AppendLine("AND RegimePlantonista = 0");
            sb.AppendLine("AND IDMotivoFalta IS NULL");

            sb.AppendLine("--FALTA INJUSTIFICADA");
            if (ObrigaQuatroBatidas.Equals("S") && DataDesconsidera != string.Empty)
            {
                sb.AppendLine("UPDATE #TEMP");
                sb.AppendLine(" SET Observacao = 'FALTA INJUSTIFICADA'  ");
                sb.AppendLine("WHERE (");
                sb.AppendLine("		(MinutosFaltantes*60) > 900 OR Jornada IS NULL OR ");
                sb.AppendLine("		--REGIME DE 8 HORAS");
                sb.AppendLine("		(");
                sb.AppendLine("			(TotalHoraDia = 8 AND DescontoTotalJornada = 1 AND DTFrequencia > @DataDesconsidera  AND (HoraEntra1 IS NULL OR HoraSaida1 IS NULL OR HoraEntrada2 IS NULL OR HoraSaida2 IS NULL)");
                sb.AppendLine("		)");
                sb.AppendLine("	 )");
                sb.AppendLine(")");
                sb.AppendLine("AND RegimePlantonista = 0");
                sb.AppendLine("AND IDMotivoFalta IS NULL");
            }

            sb.AppendLine("UPDATE #TEMP");
            sb.AppendLine(" SET Observacao = 'FALTA INJUSTIFICADA'  ");
            sb.AppendLine("WHERE (");
            sb.AppendLine("		(MinutosFaltantes*60) > 900 OR Jornada IS NULL OR ");
            sb.AppendLine("		--REGIME DE 8 HORAS");
            sb.AppendLine("		(");
            sb.AppendLine("			(TotalHoraDia = 8 AND DescontoTotalJornada = 0 AND (HoraEntra1 IS NULL OR HoraSaida1 IS NULL OR HoraEntrada2 IS NULL OR HoraSaida2 IS NULL)");
            sb.AppendLine("		)");
            sb.AppendLine("	 )");
            sb.AppendLine(")");

            sb.AppendLine("		 --(DATEPART(SECOND, HorasFalta) + 60 * DATEPART(MINUTE, HorasFalta) + 3600 * DATEPART(HOUR, HorasFalta)) > 1800");
            sb.AppendLine("AND RegimePlantonista = 0");
            sb.AppendLine("AND IDMotivoFalta IS NULL");


            sb.AppendLine("--FALTAS NÃO REGISTRADAS");
            sb.AppendLine("IF(((SELECT TOP 1 TotalHoraDia FROM #TEMP) < 12) AND (SELECT TOP 1 RegimePlantonista FROM #TEMP) = 0)");
            sb.AppendLine("BEGIN");
            sb.AppendLine("	SELECT DATEPART(week,A.DTDiasAno) NSemana,A.DTDiasAno, B.*");
            sb.AppendLine("	INTO #TEMP2");
            sb.AppendLine("	FROM dbo.TBDiasAno A  WITH(NOLOCK)");
            sb.AppendLine("	LEFT JOIN #TEMP B ON (A.DTDiasAno = CONVERT(DATETIME,B.DTFrequencia,103))");
            sb.AppendLine("	WHERE A.IdEmpresa = @IdEmpresa");
            sb.AppendLine("	AND DATEPART(YYYY,A.DTDiasAno) IN (SELECT DATEPART(YYYY,DTFrequencia) FROM #TEMP)");
            sb.AppendLine("	AND DATEPART(MM,A.DTDiasAno) IN (SELECT DATEPART(MM,DTFrequencia) FROM #TEMP)");
            sb.AppendLine("	AND A.FeriadoPontoFacultativo = 0");
            sb.AppendLine("	AND (B.TotalHoraDia < 12 OR B.TotalHoraDia IS NULL)");
            sb.AppendLine("	AND (B.RegimePlantonista = 0 OR B.RegimePlantonista IS NULL)");
            sb.AppendLine("	AND datepart(dw, DTDiasAno) <> 1 ");
            sb.AppendLine("	AND datepart(dw, DTDiasAno) <> 7 ");
            sb.AppendLine("	AND DTDiasAno < GETDATE()");
            sb.AppendLine("	AND DTDiasAno > CONVERT(DATETIME,(SELECT TOP 1 DTInicioVinculo FROM  #TEMP),103)");
            sb.AppendLine("	ORDER BY A.DTDiasAno");

            sb.AppendLine("	INSERT INTO #TEMP");
            sb.AppendLine("	(IdVinculoUsuario, DTFrequencia, Observacao)");

            sb.AppendLine("	SELECT ");
            sb.AppendLine("	(SELECT TOP 1 IDVinculoUsuario FROM #TEMP) IDVinculoUsuario,");
            sb.AppendLine("	DTDiasAno DataOcorrencia,");
            sb.AppendLine("	'FALTA INJUSTIFICADA'");
            sb.AppendLine("	FROM #TEMP2");
            sb.AppendLine("	WHERE DTFrequencia IS NULL");
            sb.AppendLine("	ORDER BY DataOcorrencia");
            sb.AppendLine("END");
            return sb.ToString();
        }
        private string GetSelectFolhaPlantonista(string idUsuario, string Mes, string Ano)
        {
            StringBuilder sb = new StringBuilder();
            if (ObrigaQuatroBatidas.Equals("S") && DataDesconsidera != string.Empty)
            {
                sb.AppendLine("DECLARE  @DataDesconsidera date");
                sb.AppendLine("SET @DataDesconsidera = convert(date,'" + DataDesconsidera + "',103)");
            }
            sb.AppendLine("DECLARE @Mes int, @Ano int");
            sb.AppendLine("	SET @IDVinculoUsuario = " + idUsuario);
            sb.AppendLine("	SET @Mes = " + Mes);
            sb.AppendLine("	SET @Ano = " + Ano);
            sb.AppendLine("	--DELETE FROM TBHorasExtrasLancamentos WHERE IdVinculoUsuario = @IDVinculoUsuario AND IdUsuarioRegistro = 1 AND DATEPART(MM,DataOcorrencia) = @Mes AND DATEPART(YYYY,DataOcorrencia) = @Ano");
            sb.AppendLine("	DECLARE @IdEmpresa int");
            sb.AppendLine("	SET @IdEmpresa = (SELECT IDEmpresa FROM TBVinculoUsuario WHERE IDVinculoUsuario = @IDVinculoUsuario)");
            sb.AppendLine("	SELECT");
            sb.AppendLine("		  A.IDVinculoUsuario");
            sb.AppendLine("		  ,DTFrequencia");
            sb.AppendLine("		  ,CONVERT(VARCHAR(5),CONVERT(TIME(4),A.HoraEntraManha)) HoraEntra1");
            sb.AppendLine("		  ,CONVERT(VARCHAR(5),CONVERT(TIME(4),A.HoraSaidaManha)) HoraSaida1");
            sb.AppendLine("		  ,CONVERT(VARCHAR(5),CONVERT(TIME(4),A.HoraEntradaTarde)) HoraEntrada2");
            sb.AppendLine("		  ,CONVERT(VARCHAR(5),CONVERT(TIME(4),A.HoraSaidaTarde)) HoraSaida2");
            sb.AppendLine("		  ,CASE WHEN ES.DataHoraEntrada IS NOT NULL THEN DATEDIFF(HOUR, ES.DataHoraEntrada,ES.DataHorasSaida) ELSE E.TotalHoraDia END TotalHoraDia");
            sb.AppendLine("		  ,dbo.ConvertDecimalToTime(CONVERT(DECIMAL(12,4),TotalHorasDiaSegundos)/3600) Jornada");
            sb.AppendLine("		  ,CASE WHEN ((CONVERT(DECIMAL(12,4),TotalHorasDiaSegundos)/3600-E.TotalHoraDia) < 0) THEN dbo.ConvertDecimalToTime(ABS((CONVERT(DECIMAL(12,4),TotalHorasDiaSegundos)-(E.TotalHoraDia*3600))/3600)) END HorasFalta");
            sb.AppendLine("		  ,CASE WHEN ((IdHorasExtrasPermissao IS NOT NULL) AND (CONVERT(DECIMAL(12,4),TotalHorasDiaSegundos)/3600-E.TotalHoraDia) > 0) THEN dbo.ConvertDecimalToTime((CONVERT(DECIMAL(12,4),TotalHorasDiaSegundos)/3600-E.TotalHoraDia)) END HorasExtras");
            sb.AppendLine("		  --,CASE WHEN ((IdHorasExtrasPermissao IS NOT NULL OR E.RegimePlantonista = 1)) THEN dbo.CalculaAdNoturno(A.HoraEntraManha, A.HoraSaidaManha, A.HoraEntradaTarde, A.HoraSaidaTarde) END AdiNoturno");
            sb.AppendLine("		  , dbo.CalculaAdNoturno(A.HoraEntraManha, A.HoraSaidaManha, A.HoraEntradaTarde, A.HoraSaidaTarde) AdiNoturno");
            sb.AppendLine("		,OBS Observacao");
            sb.AppendLine("		,IdHorasExtrasPermissao");
            sb.AppendLine("		,E.RegimePlantonista");
            sb.AppendLine("		,TotalHorasDiaSegundos");
            sb.AppendLine("		,IDMotivoFalta");
            sb.AppendLine("		,B.IDEmpresa");
            sb.AppendLine("		--INICIO CalculaAtrazoPonto");
            sb.AppendLine("		,dbo.CalculaAtrazoPonto(CONVERT(TIME,A.HoraEntraManha), CONVERT(TIME,A.HoraSaidaManha), CONVERT(TIME,A.HoraEntradaTarde), CONVERT(TIME,A.HoraSaidaTarde), ");
            sb.AppendLine("		 isNull(CONVERT(VARCHAR(5),CONVERT(TIME(4),ES.DataHoraEntrada)), B.HoraEntradaManha), isNull(CONVERT(VARCHAR(5),CONVERT(TIME(4),ES.DataHorasSaida)), B.HoraSaidaManha),");
            sb.AppendLine("		 isNull(CONVERT(VARCHAR(5),CONVERT(TIME(4),ES.DataHoraEntrada)), B.HoraEntradaTarde), isNull(CONVERT(VARCHAR(5),CONVERT(TIME(4),ES.DataHorasSaida)), B.HoraSaidaTarde)) MinutosFaltantes");
            sb.AppendLine("		  --FIM MinutosFaltantes- CalculaAtrazoPonto()");
            sb.AppendLine("		 , CONVERT(TIME,'00:00') HorasFaltantes, DTInicioVinculo");
            sb.AppendLine("		 ,B.DescontoTotalJornada");
            sb.AppendLine("		 ,ES.DataEscala");
            sb.AppendLine("		 ,isNull(CONVERT(VARCHAR(5),CONVERT(TIME(4),ES.DataHoraEntrada)), B.HoraEntradaManha) HoraEntradaManha, isNull(CONVERT(VARCHAR(5),CONVERT(TIME(4),ES.DataHorasSaida)), B.HoraSaidaManha) HoraSaidaManha,");
            sb.AppendLine("		 isNull(CONVERT(VARCHAR(5),CONVERT(TIME(4),ES.DataHoraEntrada)), B.HoraEntradaTarde) HoraEntradaTarde, isNull(CONVERT(VARCHAR(5),CONVERT(TIME(4),ES.DataHorasSaida)), B.HoraSaidaTarde) HoraSaidaTarde");
            sb.AppendLine("	  INTO #TEMP");
            sb.AppendLine("	  FROM dbo.TBFrequencia A WITH(NOLOCK)");
            sb.AppendLine("	  LEFT JOIN dbo.TbRegimeHorario_Escala ES WITH(NOLOCK) ON (ES.IdVinculoUsuario = A.IdVInculoUsuario AND ES.DataEscala = A.DTFrequencia)");
            sb.AppendLine("	  JOIN dbo.TBVinculoUsuario B WITH(NOLOCK) ON (A.IDVinculoUsuario = B.IDVinculoUsuario)");
            sb.AppendLine("	  JOIN dbo.TBUsuario C WITH(NOLOCK) ON (C.IdUsuario = B.IdUsuario)");
            sb.AppendLine("	  JOIN dbo.TBEmpresa D WITH(NOLOCK) ON (D.IdEmpresa = B.IdEmpresa)");
            sb.AppendLine("	  JOIN dbo.TBRegimeHora E WITH(NOLOCK) ON (E.IDRegimeHora = B.IDRegimeHora)");
            sb.AppendLine("	  LEFT JOIN ");
            sb.AppendLine("	  (");
            sb.AppendLine("		 SELECT MAX(IdHorasExtrasPermissao) IdHorasExtrasPermissao, MAX(DataFim) DataFim, MIN(DataInicio) DataInicio, IDVinculoUsuario");
            sb.AppendLine("		 FROM dbo.TBHorasExtrasPermissoes WITH(NOLOCK)     ");
            sb.AppendLine("		 GROUP BY IDVinculoUsuario");
            sb.AppendLine("	  ) F ON  (A.DTFrequencia BETWEEN F.DataInicio AND (isNull(F.DataFim, (A.DTFrequencia)+360)) AND F.IDVinculoUsuario = A.IDVinculoUsuario)");
            sb.AppendLine("	  WHERE ");
            sb.AppendLine("	  A.IDVinculoUsuario = @IDVinculoUsuario");
            sb.AppendLine("	  --A.IDVinculoUsuario = 7191");
            sb.AppendLine("	  --AND AnoReferencia = DATEPART(YYYY,GETDATE())");
            sb.AppendLine("	  AND MesReferencia = @Mes");
            sb.AppendLine("	  AND AnoReferencia = @Ano");
            sb.AppendLine("  AND (IsencaoPonto IS NULL OR IsencaoPonto = 0) ");
            sb.AppendLine("	  ORDER BY A.IDVinculoUsuario,DTFrequencia");


            sb.AppendLine("	----UPDATE HORAS FALTA");
            sb.AppendLine("	UPDATE #TEMP");
            sb.AppendLine("	SET HorasFalta = CASE WHEN ((CONVERT(DECIMAL(12,4),TotalHorasDiaSegundos)/3600-TotalHoraDia) < 0) THEN dbo.ConvertDecimalToTime(ABS((CONVERT(DECIMAL(12,4),TotalHorasDiaSegundos)-(TotalHoraDia*3600))/3600)) ELSE NULL END");



            sb.AppendLine("	--HORAS EXTRAS - VERIFICA SE TEM DOIS TURNOS E HORAS EXCEDENTES ");
            sb.AppendLine("	UPDATE #TEMP");
            sb.AppendLine("	SET HorasExtras = CASE ");
            sb.AppendLine("							WHEN (TotalHoraDia <= 12 AND Jornada < '20:00') THEN '02:00' ");
            sb.AppendLine("							ELSE ");
            sb.AppendLine("								CASE ");
            sb.AppendLine("									WHEN  (dbo.ConvertHorasToMinutos(Jornada) - 1200) > 240 THEN '04:00'");
            sb.AppendLine("									ELSE dbo.ConvertDecimalToTime(CONVERT(DECIMAL(18,4),(dbo.ConvertHorasToMinutos(Jornada) - 1200))/60) ");
            sb.AppendLine("								 END");
            sb.AppendLine("							END");
            sb.AppendLine("	WHERE(CONVERT(DECIMAL(18,4),dbo.ConvertHorasToMinutos(HorasExtras))/60 > 2)");



            sb.AppendLine("	--SETA EM FORMATO HORAS OS MINUTOS EM ATRASO");
            sb.AppendLine("	UPDATE #TEMP");
            sb.AppendLine("	SET HorasFaltantes =  CONVERT(TIME,convert(char(8), dateadd(MINUTE, MinutosFaltantes, ''), 114))");



            sb.AppendLine("	--SETA EM MinutosFaltantes o campo HorasFaltantes convertido em minutos, caso o DescontoTotalJornada for igual a 1");
            sb.AppendLine("	IF((SELECT TOP 1 DescontoTotalJornada FROM #TEMP) = 1)");
            sb.AppendLine("	BEGIN");
            sb.AppendLine("		UPDATE #TEMP");
            sb.AppendLine("		SET MinutosFaltantes = CASE WHEN (DATEDIFF(MINUTE,0, HorasFalta)) > 15 THEN (DATEDIFF(MINUTE,0, HorasFalta)) ELSE NULL END, HorasFaltantes = HorasFalta");
            sb.AppendLine("	END");


            sb.AppendLine("	UPDATE A");
            sb.AppendLine("	SET A.Observacao = 'ATRASO NO PONTO'");
            sb.AppendLine("	FROM #TEMP A");
            sb.AppendLine("	WHERE (MinutosFaltantes*60) BETWEEN 1 AND 900");
            sb.AppendLine("	AND IDMotivoFalta IS NULL");

            sb.AppendLine("--FALTA INJUSTIFICADA");
            if (ObrigaQuatroBatidas.Equals("S") && DataDesconsidera != string.Empty)
            {
                sb.AppendLine("UPDATE #TEMP");
                sb.AppendLine(" SET Observacao = 'FALTA INJUSTIFICADA'  ");
                sb.AppendLine("WHERE (");
                sb.AppendLine("		(MinutosFaltantes*60) > 900 OR Jornada IS NULL OR ");
                sb.AppendLine("		--REGIME DE 8 HORAS");
                sb.AppendLine("		(");
                sb.AppendLine("			(TotalHoraDia = 8 AND DescontoTotalJornada = 1 AND DTFrequencia > @DataDesconsidera  AND (HoraEntra1 IS NULL OR HoraSaida1 IS NULL OR HoraEntrada2 IS NULL OR HoraSaida2 IS NULL)");
                sb.AppendLine("		)");
                sb.AppendLine("	 )");
                sb.AppendLine(")");
                sb.AppendLine("AND IDMotivoFalta IS NULL");
            }
            sb.AppendLine("	UPDATE #TEMP");
            sb.AppendLine("	SET Observacao = 'FALTA INJUSTIFICADA'");
            sb.AppendLine("	WHERE (");
            sb.AppendLine("			(MinutosFaltantes*60) > 900 OR Jornada IS NULL OR ");
            sb.AppendLine("			--REGIME DE 8 HORAS");
            sb.AppendLine("			(");
            sb.AppendLine("			(TotalHoraDia = 8 AND DescontoTotalJornada = 0 AND (HoraEntra1 IS NULL OR HoraSaida1 IS NULL OR HoraEntrada2 IS NULL OR HoraSaida2 IS NULL)");
            sb.AppendLine("			)");
            sb.AppendLine("		 )");
            sb.AppendLine("	)");
            sb.AppendLine("			 --(DATEPART(SECOND, HorasFalta) + 60 * DATEPART(MINUTE, HorasFalta) + 3600 * DATEPART(HOUR, HorasFalta)) > 1800");
            sb.AppendLine("	AND IDMotivoFalta IS NULL");

            sb.AppendLine("	--FALTAS NÃO REGISTRADAS");
            sb.AppendLine("	SELECT DATEPART(week,A.DataEscala) NSemana,A.DataEscala DTDiasAno, B.*");
            sb.AppendLine("	INTO #TEMP2");
            sb.AppendLine("	FROM dbo.TbRegimeHorario_Escala A  WITH(NOLOCK)");
            sb.AppendLine("	LEFT JOIN #TEMP B ON (A.DataEscala = CONVERT(DATETIME,B.DTFrequencia,103))");
            sb.AppendLine("	WHERE DATEPART(YYYY,A.DataEscala) IN (SELECT DATEPART(YYYY,DTFrequencia) FROM #TEMP)");
            sb.AppendLine("	AND DATEPART(MM,A.DataEscala) IN (SELECT DATEPART(MM,DTFrequencia) FROM #TEMP)");
            sb.AppendLine("	AND A.DataEscala < GETDATE()");
            sb.AppendLine("	AND A.IDVinculoUsuario IN (SELECT TOP 1 IDVinculoUsuario FROM #TEMP)");
            sb.AppendLine("	ORDER BY A.DataEscala");

            sb.AppendLine("	INSERT INTO #TEMP");
            sb.AppendLine("	(IdVinculoUsuario, DTFrequencia, Observacao)");
            sb.AppendLine("	SELECT ");
            sb.AppendLine("	(SELECT TOP 1 IDVinculoUsuario FROM #TEMP) IDVinculoUsuario,");
            sb.AppendLine("	DTDiasAno DataOcorrencia,");
            sb.AppendLine("	'FALTA INJUSTIFICADA'");
            sb.AppendLine("	FROM #TEMP2");
            sb.AppendLine("	WHERE DTFrequencia IS NULL");
            sb.AppendLine("	ORDER BY DataOcorrencia");
            return sb.ToString();
        }

        #endregion SQL CONFERENCIA FOLHA


        public string GetDescRegime(string IDVinculoUsuario)
        {
            return Util.getScalar("SELECT DSRegimeHora FROM TBVinculoUsuario A JOIN dbo.TBRegimeHora B ON (A.IDRegimeHora = B.IDRegimeHora) WHERE IDVinculoUsuario = " + IDVinculoUsuario);
        }

    }
}
