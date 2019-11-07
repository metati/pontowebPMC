SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[spFechamentoFolhaUsuario]
(
	@IDVinculoUsuario int, @Mes int, @Ano int
)
as
BEGIN TRANSACTION Tran1 BEGIN TRY
	DELETE FROM TBHorasExtrasLancamentos WHERE IdVinculoUsuario = @IDVinculoUsuario AND IdUsuarioRegistro = 1 AND DATEPART(MM,DataOcorrencia) = @Mes AND DATEPART(YYYY,DataOcorrencia) = @Ano
	DECLARE @IdEmpresa int
	SET @IdEmpresa = (SELECT IDEmpresa FROM TBVinculoUsuario WITH(NOLOCK) WHERE IDVinculoUsuario = @IDVinculoUsuario)


	/***********************INICIO TEMP TBFREQUENCIA E TEMP PERMISSÃO DE HORAS EXTRAS******************************/
	SELECT *
	INTO #TEMP_FREQUENCIA
	FROM PontoFrequenciaPMC.dbo.TBFrequencia 
	WHERE IDVinculoUsuario = @IDVinculoUsuario
	AND MesReferencia = @Mes
	AND AnoReferencia = @Ano



	SELECT DISTINCT  MAX(IdHorasExtrasPermissao) IdHorasExtrasPermissao, MAX(DataFim) DataFim, MIN(DataInicio) DataInicio, A.IDVinculoUsuario
	INTO #TEMP_PermisaoHorasExtras
	FROM PontoFrequenciaPMC.dbo.TBHorasExtrasPermissoes F WITH(NOLOCK)     
	JOIN #TEMP_FREQUENCIA A ON (A.DTFrequencia BETWEEN F.DataInicio AND (isNull(F.DataFim, (A.DTFrequencia)+360)) AND F.IDVinculoUsuario = A.IDVinculoUsuario)
	GROUP BY A.IDVinculoUsuario
	/***********************FIM TEMP TBFREQUENCIA E TEMP PERMISSÃO DE HORAS EXTRAS******************************/
 
	SELECT DISTINCT 
		  A.IDVinculoUsuario
		  ,DTFrequencia
		  ,CONVERT(VARCHAR(5),CONVERT(TIME(4),A.HoraEntraManha)) HoraEntra1
		  ,CONVERT(VARCHAR(5),CONVERT(TIME(4),A.HoraSaidaManha)) HoraSaida1
		  ,CONVERT(VARCHAR(5),CONVERT(TIME(4),A.HoraEntradaTarde)) HoraEntrada2
		  ,CONVERT(VARCHAR(5),CONVERT(TIME(4),A.HoraSaidaTarde)) HoraSaida2
		  
		  --,CASE WHEN DTFrequencia < (SELECT        TBTrocaPadraoHoraUsuario.DTTrocaPadraoHora
    --                           FROM            TBTrocaPadraoHoraUsuario
    --                           WHERE        IDVinculoUsuario = B.IDVinculoUsuario) THEN ISNULL
    --                         ((SELECT        TOP 1 TBPadraoHoraUsuario.TotHorasDiarias
    --                             FROM            TBPadraoHoraUsuario
    --                             WHERE        DTFimPadrao != '1900-01-01' AND IDVinculoUsuario = B.IDVinculoUsuario
				--				 and  DTFrequencia between DTInicioPadrao and DTFimPadrao), E.TotalHoraDia) ELSE
    --                         (SELECT        TOP 1 TBPadraoHoraUsuario.TotHorasDiarias
    --                           FROM            TBPadraoHoraUsuario
    --                           WHERE        PadraoAtual = 1 AND IDVinculoUsuario = B.IDVinculoUsuario) END AS TotalHoraDia
		  
		  --07/07/2019
		  ,PHU.TotHorasDiarias TotalHoraDia
		  ,PontoFrequenciaPMC.dbo.ConvertDecimalToTime(CONVERT(DECIMAL(12,4),TotalHorasDiaSegundos)/3600) Jornada
		  ,CASE WHEN ((CONVERT(DECIMAL(12,4),TotalHorasDiaSegundos)/3600-PHU.TotHorasDiarias) < 0) THEN PontoFrequenciaPMC.dbo.ConvertDecimalToTime(ABS((CONVERT(DECIMAL(12,4),TotalHorasDiaSegundos)-(PHU.TotHorasDiarias*3600))/3600)) END HorasFalta
		  ,CASE WHEN ((IdHorasExtrasPermissao IS NOT NULL) AND (CONVERT(DECIMAL(12,4),TotalHorasDiaSegundos)/3600-PHU.TotHorasDiarias) > 0) THEN PontoFrequenciaPMC.dbo.ConvertDecimalToTime((CONVERT(DECIMAL(12,4),TotalHorasDiaSegundos)/3600-PHU.TotHorasDiarias)) END HorasExtras
		  --,CASE WHEN ((IdHorasExtrasPermissao IS NOT NULL OR E.RegimePlantonista = 1)) THEN PontoFrequenciaPMC.dbo.CalculaAdNoturno(A.HoraEntraManha, A.HoraSaidaManha, A.HoraEntradaTarde, A.HoraSaidaTarde) END AdiNoturno
		  ,PontoFrequenciaPMC.dbo.CalculaAdNoturno(A.HoraEntraManha, A.HoraSaidaManha, A.HoraEntradaTarde, A.HoraSaidaTarde) AdiNoturno
		,OBS Observacao
		,IdHorasExtrasPermissao
		,E.RegimePlantonista
		,TotalHorasDiaSegundos
		,IDMotivoFalta
		,B.IDEmpresa
		,dbo.CalculaAtrazoPonto(CONVERT(TIME,A.HoraEntraManha), CONVERT(TIME,A.HoraSaidaManha), CONVERT(TIME,A.HoraEntradaTarde), CONVERT(TIME,A.HoraSaidaTarde), 
		 PHU.HoraEntradaManha, PHU.HoraSaidaManha, PHU.HoraEntradaTarde, PHU.HoraSaidaTarde) MinutosFaltantes
		 , CONVERT(TIME,'00:00') HorasFaltantes, DTInicioVinculo
		 ,B.DescontoTotalJornada
	  INTO #TEMP  
	  FROM #TEMP_FREQUENCIA A WITH(NOLOCK)
	  JOIN PontoFrequenciaPMC.dbo.TBVinculoUsuario B WITH(NOLOCK) ON (A.IDVinculoUsuario = B.IDVinculoUsuario)
	  JOIN PontoFrequenciaPMC.dbo.TBUsuario C WITH(NOLOCK) ON (C.IdUsuario = B.IdUsuario)
	  JOIN PontoFrequenciaPMC.dbo.TBEmpresa D WITH(NOLOCK) ON (D.IdEmpresa = B.IdEmpresa)
	  JOIN PontoFrequenciaPMC.dbo.TBRegimeHora E WITH(NOLOCK) ON (E.IDRegimeHora = B.IDRegimeHora)
    JOIN PontoFrequenciaPMC.dbo.TBPadraoHoraUsuario PHU WITH(NOLOCK) ON (PHU.IDVinculoUsuario = A .IDVinculoUsuario 
                        AND A.DTFrequencia BETWEEN PHU.DTInicioPadrao 
						AND (CASE WHEN PHU.DTFimPadrao = CONVERT(DATETIME, '1900-01-01') 
                                                                              THEN GETDATE()+360 
																			  ELSE PHU.DTFimPadrao END)
                        )
	  LEFT JOIN 
	  (
		 SELECT IdHorasExtrasPermissao, DataFim, DataInicio, IDVinculoUsuario
		 FROM #TEMP_PermisaoHorasExtras WITH(NOLOCK)     
	  ) F ON  (A.DTFrequencia BETWEEN F.DataInicio AND (isNull(F.DataFim, (A.DTFrequencia)+360)) AND F.IDVinculoUsuario = A.IDVinculoUsuario)
	  WHERE 
	  A.IDVinculoUsuario = @IDVinculoUsuario
	  --A.IDVinculoUsuario = 7191
	  --AND AnoReferencia = DATEPART(YYYY,GETDATE())
	  AND MesReferencia = @Mes
	  AND AnoReferencia = @Ano
	  ORDER BY A.IDVinculoUsuario,DTFrequencia
	  
	  
	  
	--VERIFICA HORAS EXCEDENTES
	UPDATE #TEMP
	SET HorasExtras = '02:00'
	WHERE(CONVERT(DECIMAL(18, 4), (DATEPART(SECOND, HorasExtras) + 60 * DATEPART(MINUTE, HorasExtras) + 3600 * DATEPART(HOUR, HorasExtras))) / 3600) > 2


	--SETA EM FORMATO HORAS OS MINUTOS EM ATRASO
	UPDATE #TEMP
	SET HorasFaltantes =  CONVERT(TIME,convert(char(8), dateadd(MINUTE, MinutosFaltantes, ''), 114))


	--SETA EM MinutosFaltantes o campo HorasFaltantes convertido em minutos, caso o DescontoTotalJornada for igual a 1
	IF((SELECT TOP 1 DescontoTotalJornada FROM #TEMP) = 1)
	BEGIN
		UPDATE #TEMP
		SET MinutosFaltantes = CASE WHEN (DATEDIFF(MINUTE,0, HorasFalta)) > 15 THEN (DATEDIFF(MINUTE,0, HorasFalta)) ELSE NULL END, HorasFaltantes = HorasFalta
	END

	--INSERT HORAS EXTRAS
	INSERT INTO TBHorasExtrasLancamentos(IdHorasExtrasPermissao,IDVinculoUsuario,CodigoVerba, DataOcorrencia, TotalHoras, TotalSegundos, IdUsuarioRegistro)
	SELECT DISTINCT IdHorasExtrasPermissao,IDVinculoUsuario,1235 CodigoVerba, DTFrequencia DataOcorrencia, HorasExtras TotalHoras, (DATEPART(SECOND, HorasExtras) + 60 * DATEPART(MINUTE, HorasExtras) + 3600 * DATEPART(HOUR, HorasExtras)) TotalSegundos, 1 IdUsuarioRegistro
	FROM #TEMP A
	WHERE HorasExtras IS NOT NULL
	AND CONVERT(VARCHAR(12),A.DTFrequencia,103) NOT IN (select CONVERT(VARCHAR(12),DataOcorrencia,103) from [dbo].[TBHorasExtrasLancamentos] WITH(NOLOCK) WHERE IDVinculoUsuario = @IdVinculoUsuario AND CodigoVerba = 1235)
	AND HorasExtras <> '00:00'
	ORDER BY DataOcorrencia



	--ADICIONAL NOTURNO
	INSERT INTO TBHorasExtrasLancamentos(IdHorasExtrasPermissao,IDVinculoUsuario,CodigoVerba, DataOcorrencia, TotalHoras, TotalSegundos, IdUsuarioRegistro)
	SELECT DISTINCT IdHorasExtrasPermissao,IDVinculoUsuario,1244 CodigoVerba, DTFrequencia DataOcorrencia, AdiNoturno TotalHoras, (DATEPART(SECOND, AdiNoturno) + 60 * DATEPART(MINUTE, AdiNoturno) + 3600 * DATEPART(HOUR, AdiNoturno)) TotalSegundos, 1 IdUsuarioRegistro
	FROM #TEMP A
	WHERE AdiNoturno IS NOT NULL
	AND AdiNoturno <> '00:00'
	AND CONVERT(VARCHAR(12),A.DTFrequencia,103) NOT IN (select CONVERT(VARCHAR(12),DataOcorrencia,103) from [dbo].[TBHorasExtrasLancamentos] WITH(NOLOCK) WHERE IDVinculoUsuario = @IdVinculoUsuario AND CodigoVerba = 1244)
	ORDER BY DataOcorrencia





	--ATRASO NO PONTO
	INSERT INTO TBHorasExtrasLancamentos(IdHorasExtrasPermissao,IDVinculoUsuario,CodigoVerba, DataOcorrencia, TotalHoras, TotalSegundos, IdUsuarioRegistro)
	SELECT DISTINCT IdHorasExtrasPermissao,IDVinculoUsuario,4544 CodigoVerba, DTFrequencia DataOcorrencia, HorasFaltantes TotalHoras, (DATEPART(SECOND, HorasFalta) + 60 * DATEPART(MINUTE, HorasFalta) + 3600 * DATEPART(HOUR, HorasFalta)) TotalSegundos, 1 IdUsuarioRegistro
	FROM #TEMP A
	WHERE (MinutosFaltantes*60) BETWEEN 1 AND 900
	AND RegimePlantonista = 0
	AND IDMotivoFalta IS NULL
	AND CONVERT(VARCHAR(12),A.DTFrequencia,103) NOT IN (select CONVERT(VARCHAR(12),DataOcorrencia,103) from [dbo].[TBHorasExtrasLancamentos] WITH(NOLOCK) WHERE IDVinculoUsuario = @IdVinculoUsuario AND CodigoVerba = 4544)
	ORDER BY DataOcorrencia




	--FALTA INJUSTIFICADA
	INSERT INTO TBHorasExtrasLancamentos(IdHorasExtrasPermissao,IDVinculoUsuario,CodigoVerba, DataOcorrencia, TotalHoras, TotalSegundos, IdUsuarioRegistro)
	SELECT DISTINCT IdHorasExtrasPermissao,IDVinculoUsuario,CASE WHEN IDEmpresa = 59 THEN 20142 ELSE 4504 END CodigoVerba, DTFrequencia DataOcorrencia, NULL TotalHoras, NULL TotalSegundos, 1 IdUsuarioRegistro
	FROM #TEMP
	WHERE (
			((MinutosFaltantes*60) > 900) OR
			(Jornada IS NULL) OR 
			--REGIME DE 8 HORAS
			(
				(TotalHoraDia = 8 AND DescontoTotalJornada = 0 AND (HoraEntra1 IS NULL OR HoraSaida1 IS NULL OR HoraEntrada2 IS NULL OR HoraSaida2 IS NULL))
		    )
	      )
			 --(DATEPART(SECOND, HorasFalta) + 60 * DATEPART(MINUTE, HorasFalta) + 3600 * DATEPART(HOUR, HorasFalta)) > 1800
	AND RegimePlantonista = 0
	AND IDMotivoFalta IS NULL
	ORDER BY DataOcorrencia




	--FALTAS NÃO REGISTRADAS
	
	SELECT DATEPART(week,A.DTDiasAno) NSemana,A.DTDiasAno,
	NULL IdHorasExtrasPermissao,@IDVinculoUsuario IDVinculoUsuario,CASE WHEN @IDEmpresa = 59 THEN 20142 ELSE 4504 END CodigoVerba, A.DTDiasAno DataOcorrencia, NULL TotalHoras, NULL TotalSegundos, 1 IdUsuarioRegistro,
	B.DTFrequencia
	INTO #TEMP2
	FROM dbo.TBDiasAno A  WITH(NOLOCK)
	LEFT JOIN #TEMP B ON (A.DTDiasAno = CONVERT(DATETIME,B.DTFrequencia,103) AND (B.TotalHoraDia < 12 OR B.TotalHoraDia IS NULL))
	WHERE A.IdEmpresa = @IdEmpresa
	AND DATEPART(YYYY,A.DTDiasAno) IN (@Ano)
	AND DATEPART(MM,A.DTDiasAno) IN (@Mes)
	AND A.FeriadoPontoFacultativo = 0
	AND datepart(dw, DTDiasAno) <> 1 
	AND datepart(dw, DTDiasAno) <> 7 
	AND DTDiasAno < GETDATE()
	AND 0 = (SELECT D.RegimePlantonista FROM TBVinculoUsuario C WITH(NOLOCK) JOIN TBRegimeHora D WITH(NOLOCK) ON (C.IdRegimeHora = D.IdRegimeHora) WHERE D.TotalHoraDia < 12 AND C.IdVinculoUsuario = @IdVinculoUsuario)
	ORDER BY A.DTDiasAno


	INSERT INTO TBHorasExtrasLancamentos(IdHorasExtrasPermissao,IDVinculoUsuario,CodigoVerba, DataOcorrencia, TotalHoras, TotalSegundos, IdUsuarioRegistro)
	SELECT DISTINCT IdHorasExtrasPermissao,
	IDVinculoUsuario,
	CodigoVerba, 
	DTDiasAno DataOcorrencia, 
	NULL TotalHoras, 
	NULL TotalSegundos,
	1 IdUsuarioRegistro
	FROM #TEMP2
	WHERE DTFrequencia IS NULL
	ORDER BY DataOcorrencia
	





COMMIT TRANSACTION Tran1
END TRY
BEGIN CATCH
ROLLBACK TRANSACTION Tran1
DECLARE @Menssagem varchar(1000)
SET @Menssagem = (SELECT ERROR_MESSAGE())
RAISERROR (@Menssagem, 16, 1 );
END CATCH  



GO
