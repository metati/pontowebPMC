LTER PROCEDURE [dbo].[spFechamentoFolhaUsuario_Plantonistas]
(
	@IDVinculoUsuario int, @Mes int, @Ano int
)
as
BEGIN TRANSACTION Tran1 BEGIN TRY
	DELETE FROM TBHorasExtrasLancamentos WHERE IdVinculoUsuario = @IDVinculoUsuario AND IdUsuarioRegistro = 1 AND DATEPART(MM,DataOcorrencia) = @Mes AND DATEPART(YYYY,DataOcorrencia) = @Ano
	DECLARE @IdEmpresa int
	SET @IdEmpresa = (SELECT IDEmpresa FROM TBVinculoUsuario WHERE IDVinculoUsuario = @IDVinculoUsuario)


	
	/***********************INICIO TEMP TBFREQUENCIA E TEMP PERMISSÃO DE HORAS EXTRAS******************************/
	SELECT *
	INTO #TEMP_FREQUENCIA1
	FROM PontoFrequenciaPMC.dbo.TBFrequencia 
	WHERE IDVinculoUsuario = @IDVinculoUsuario
	AND MesReferencia = @Mes
	AND AnoReferencia = @Ano



	SELECT DISTINCT  MAX(IdHorasExtrasPermissao) IdHorasExtrasPermissao, MAX(DataFim) DataFim, MIN(DataInicio) DataInicio, A.IDVinculoUsuario
	INTO #TEMP_PermisaoHorasExtras
	FROM PontoFrequenciaPMC.dbo.TBHorasExtrasPermissoes F WITH(NOLOCK)     
	JOIN #TEMP_FREQUENCIA1 A ON (A.DTFrequencia BETWEEN F.DataInicio AND (isNull(F.DataFim, (A.DTFrequencia)+360)) AND F.IDVinculoUsuario = A.IDVinculoUsuario)
	GROUP BY A.IDVinculoUsuario
	/***********************FIM TEMP TBFREQUENCIA E TEMP PERMISSÃO DE HORAS EXTRAS******************************/


	SELECT DISTINCT
		  A.IDVinculoUsuario
		  ,DTFrequencia
		  ,CONVERT(VARCHAR(5),CONVERT(TIME(4),A.HoraEntraManha)) HoraEntra1
		  ,CONVERT(VARCHAR(5),CONVERT(TIME(4),A.HoraSaidaManha)) HoraSaida1
		  ,CONVERT(VARCHAR(5),CONVERT(TIME(4),A.HoraEntradaTarde)) HoraEntrada2
		  ,CONVERT(VARCHAR(5),CONVERT(TIME(4),A.HoraSaidaTarde)) HoraSaida2
		  ,CASE WHEN ES.DataHoraEntrada IS NOT NULL THEN DATEDIFF(HOUR, ES.DataHoraEntrada,ES.DataHorasSaida) ELSE PHU.TotHorasDiarias END TotalHoraDia
		  ,dbo.ConvertDecimalToTime(CONVERT(DECIMAL(12,4),TotalHorasDiaSegundos)/3600) Jornada
		  ,dbo.ConvertHorasToMinutos(dbo.ConvertDecimalToTime(CONVERT(DECIMAL(12,4),TotalHorasDiaSegundos)/3600)) TotalJornadaMinutos
		  ,CASE WHEN ((CONVERT(DECIMAL(12,4),TotalHorasDiaSegundos)/3600-PHU.TotHorasDiarias) < 0) THEN dbo.ConvertDecimalToTime(ABS((CONVERT(DECIMAL(12,4),TotalHorasDiaSegundos)-(PHU.TotHorasDiarias*3600))/3600)) END HorasFalta
		  ,CASE 
				WHEN ((IdHorasExtrasPermissao IS NOT NULL) AND (CONVERT(DECIMAL(12,4),TotalHorasDiaSegundos)/3600-PHU.TotHorasDiarias) > 0) 
				THEN dbo.ConvertDecimalToTime((CONVERT(DECIMAL(12,4),TotalHorasDiaSegundos)/3600-PHU.TotHorasDiarias)) END HorasExtras
		  , dbo.CalculaAdNoturno(A.HoraEntraManha, A.HoraSaidaManha, A.HoraEntradaTarde, A.HoraSaidaTarde) AdiNoturno
		,OBS Observacao
		,IdHorasExtrasPermissao
		,E.RegimePlantonista
		,TotalHorasDiaSegundos
		,IDMotivoFalta
		,B.IDCargo
		,B.IDEmpresa
		--INICIO CalculaAtrazoPonto
		,dbo.CalculaAtrazoPontoPlantonista(A.HoraEntraManha, A.HoraSaidaManha, A.HoraEntradaTarde, A.HoraSaidaTarde, 
										   isNull(ES.DataHoraEntrada, PHU.HoraEntradaManha), isNull(ES.DataHorasSaida, PHU.HoraSaidaManha),
										   isNull(ES.DataHoraEntrada, PHU.HoraEntradaTarde), isNull(ES.DataHorasSaida, PHU.HoraSaidaTarde), 
										   A.DTFrequencia)  MinutosFaltantes
		  --FIM MinutosFaltantes- CalculaAtrazoPonto()
		 , CONVERT(TIME,'00:00') HorasFaltantes, DTInicioVinculo
		 ,B.DescontoTotalJornada
		 ,ES.DataEscala
		 ,isNull(CONVERT(VARCHAR(5),CONVERT(TIME(4),ES.DataHoraEntrada)), PHU.HoraEntradaManha) HoraEntradaManha, isNull(CONVERT(VARCHAR(5),CONVERT(TIME(4),ES.DataHorasSaida)), PHU.HoraSaidaManha) HoraSaidaManha,
		 isNull(CONVERT(VARCHAR(5),CONVERT(TIME(4),ES.DataHoraEntrada)), PHU.HoraEntradaTarde) HoraEntradaTarde, isNull(CONVERT(VARCHAR(5),CONVERT(TIME(4),ES.DataHorasSaida)), PHU.HoraSaidaTarde) HoraSaidaTarde
	  INTO #TEMP
	  FROM #TEMP_FREQUENCIA1 A WITH(NOLOCK)
	  LEFT JOIN dbo.TbRegimeHorario_Escala ES WITH(NOLOCK) ON (ES.IdVinculoUsuario = A.IdVInculoUsuario AND ES.DataEscala = A.DTFrequencia)
	  JOIN dbo.TBVinculoUsuario B WITH(NOLOCK) ON (A.IDVinculoUsuario = B.IDVinculoUsuario)
	  JOIN dbo.TBUsuario C WITH(NOLOCK) ON (C.IdUsuario = B.IdUsuario)
	  JOIN dbo.TBEmpresa D WITH(NOLOCK) ON (D.IdEmpresa = B.IdEmpresa)
	  JOIN dbo.TBRegimeHora E WITH(NOLOCK) ON (E.IDRegimeHora = B.IDRegimeHora)
      JOIN PontoFrequenciaPMC.dbo.TBPadraoHoraUsuario PHU WITH(NOLOCK) ON (PHU.IDVinculoUsuario = A .IDVinculoUsuario 
                        AND A.DTFrequencia BETWEEN PHU.DTInicioPadrao AND (CASE WHEN PHU.DTFimPadrao = CONVERT(DATETIME, '1900-01-01') 
                                                                              THEN GETDATE()+360 ELSE PHU.DTFimPadrao END)
                        )
	  LEFT JOIN 
	  (
		 SELECT IdHorasExtrasPermissao, DataFim, DataInicio, IDVinculoUsuario
		 FROM #TEMP_PermisaoHorasExtras WITH(NOLOCK)    
	  ) 
	  F ON  (A.DTFrequencia BETWEEN F.DataInicio AND (isNull(F.DataFim, (A.DTFrequencia)+360)) AND F.IDVinculoUsuario = A.IDVinculoUsuario)
	  WHERE 
	  A.IDVinculoUsuario = @IDVinculoUsuario
	  AND MesReferencia = @Mes
	  AND AnoReferencia = @Ano
	  AND E.RegimePlantonista = 1
	  ORDER BY A.IDVinculoUsuario,DTFrequencia



	----CARGOS MEDICOS E ODONTOLOGOS
      SELECT IDCargo
	  INTO #TEMPCargos
	  FROM [PontoFrequenciaPMC].[dbo].[TBCargo]
	  WHERE ((DSCargo LIKE '%CIRURGIÃO%'
	  OR DSCargo LIKE '%DENTISTA%'
	  OR DSCargo LIKE '%MEDICO%'
	  OR DSCargo LIKE '%ODONTOLOGO%')
	  AND DSCargo NOT LIKE '%BIOMEDICO%'
	  AND IDCargo NOT IN (529))
	  OR (IDCargo in (397,398,410,586,709,711,710,722,723))



	----UPDATE HORAS FALTA
	UPDATE #TEMP
	SET HorasFalta = CASE WHEN ((CONVERT(DECIMAL(12,4),TotalHorasDiaSegundos)/3600-TotalHoraDia) < 0) THEN dbo.ConvertDecimalToTime(ABS((CONVERT(DECIMAL(12,4),TotalHorasDiaSegundos)-(TotalHoraDia*3600))/3600)) ELSE NULL END


	--HORAS EXTRAS - VERIFICA PARA MÉDICOS E DENTISTAS
	UPDATE #TEMP
	SET HorasExtras = CASE WHEN (TotalJornadaMinutos >= 1440)  THEN '04:00' 
						   WHEN (TotalJornadaMinutos BETWEEN 720 AND 1200)  THEN '02:00' 
						   WHEN (TotalJornadaMinutos BETWEEN 1200 AND 1439) THEN dbo.ConvertDecimalToTime(CONVERT(DECIMAL(18,4),(TotalJornadaMinutos - 1200))/60) 
						   WHEN (TotalJornadaMinutos BETWEEN 720 AND 1200)  THEN '02:00' 
						   WHEN (CONVERT(DECIMAL(18,4), (TotalJornadaMinutos - 600))/60) > 0 THEN dbo.ConvertDecimalToTime(CONVERT(DECIMAL(18,4), (TotalJornadaMinutos - 600))/60)
						   END
	WHERE IDCargo in (SELECT IDCargo FROM #TEMPCargos)
	AND IDEmpresa = 54

	UPDATE #TEMP
	SET HorasExtras = CASE WHEN (HorasExtras >= '00:00' AND HorasExtras NOT LIKE '%-%') THEN HorasExtras ELSE NULL END

	--VERIFICA HORAS EXCEDENTES
	UPDATE #TEMP
	SET HorasExtras = '02:00'
	WHERE(CONVERT(DECIMAL(18, 4), (DATEPART(SECOND, HorasExtras) + 60 * DATEPART(MINUTE, HorasExtras) + 3600 * DATEPART(HOUR, HorasExtras))) / 3600) > 2
	AND IDCargo NOT in (SELECT IDCargo FROM #TEMPCargos)
	AND IDEmpresa <> 54

	--SETA EM FORMATO HORAS OS MINUTOS EM ATRASO
	UPDATE #TEMP
	SET HorasFaltantes = CONVERT(TIME,convert(char(8), dateadd(MINUTE, MinutosFaltantes, ''), 114))
	WHERE MinutosFaltantes > 0



	--SETA EM MinutosFaltantes o campo HorasFaltantes convertido em minutos, caso o DescontoTotalJornada for igual a 1
	IF((SELECT TOP 1 DescontoTotalJornada FROM #TEMP) = 1)
	BEGIN
		UPDATE #TEMP
		SET MinutosFaltantes = CASE WHEN (DATEDIFF(MINUTE,0, HorasFalta)) > 15 THEN (DATEDIFF(MINUTE,0, HorasFalta)) ELSE NULL END, HorasFaltantes = HorasFalta
	END


	UPDATE A
	SET A.Observacao = 'ATRASO NO PONTO'
	FROM #TEMP A
	WHERE (MinutosFaltantes*60) BETWEEN 1 AND 900
	AND IDMotivoFalta IS NULL

--FALTA INJUSTIFICADA
	UPDATE #TEMP
	SET Observacao = 'FALTA INJUSTIFICADA'
	WHERE (
			(MinutosFaltantes*60) > 900 OR Jornada IS NULL OR 
			--REGIME DE 8 HORAS
			(
				(TotalHoraDia = 8 AND DescontoTotalJornada = 0 AND (HoraEntra1 IS NULL OR HoraSaida1 IS NULL OR HoraEntrada2 IS NULL OR HoraSaida2 IS NULL)
			)
		 )
	)
			 --(DATEPART(SECOND, HorasFalta) + 60 * DATEPART(MINUTE, HorasFalta) + 3600 * DATEPART(HOUR, HorasFalta)) > 1800
	AND IDMotivoFalta IS NULL


	--**********************INICIO DOS INSERT DAS OCORRENCIAS************************************************************

	--INSERT HORAS EXTRAS
	INSERT INTO TBHorasExtrasLancamentos(IdHorasExtrasPermissao,IDVinculoUsuario,CodigoVerba, DataOcorrencia, TotalHoras, TotalSegundos, IdUsuarioRegistro)
	SELECT DISTINCT IdHorasExtrasPermissao,IDVinculoUsuario,1235 CodigoVerba, DTFrequencia DataOcorrencia, HorasExtras TotalHoras, (DATEPART(SECOND, HorasExtras) + 60 * DATEPART(MINUTE, HorasExtras) + 3600 * DATEPART(HOUR, HorasExtras)) TotalSegundos, 1 IdUsuarioRegistro
	FROM #TEMP A
	WHERE HorasExtras IS NOT NULL
	AND CONVERT(VARCHAR(12),A.DTFrequencia,103) NOT IN (select CONVERT(VARCHAR(12),DataOcorrencia,103) from [dbo].[TBHorasExtrasLancamentos] WHERE IDVinculoUsuario = @IdVinculoUsuario AND CodigoVerba = 1235)
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
	AND RegimePlantonista = 1
	AND IDMotivoFalta IS NULL
	AND DescontoTotalJornada = 0
	AND CONVERT(VARCHAR(12),A.DTFrequencia,103) NOT IN (select CONVERT(VARCHAR(12),DataOcorrencia,103) from [dbo].[TBHorasExtrasLancamentos] WHERE IDVinculoUsuario = @IdVinculoUsuario AND CodigoVerba = 4544)
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
	AND RegimePlantonista = 1
	AND IDMotivoFalta IS NULL
	ORDER BY DataOcorrencia



	--FALTAS NÃO REGISTRADAS
	SELECT DATEPART(week,A.DataEscala) NSemana,A.DataEscala DTDiasAno, B.*
	INTO #TEMP2
	FROM dbo.TbRegimeHorario_Escala A  WITH(NOLOCK)
	LEFT JOIN #TEMP B ON (A.DataEscala = CONVERT(DATETIME,B.DTFrequencia,103))
	WHERE DATEPART(YYYY,A.DataEscala) IN (@Ano)
	AND DATEPART(MM,A.DataEscala) IN (@Mes)
	AND A.DataEscala < GETDATE()
	AND A.IDVinculoUsuario IN (@IdVinculoUsuario)
	ORDER BY A.DataEscala

	
	INSERT INTO TBHorasExtrasLancamentos(IdHorasExtrasPermissao,IDVinculoUsuario,CodigoVerba, DataOcorrencia, TotalHoras, TotalSegundos, IdUsuarioRegistro)
	SELECT DISTINCT IdHorasExtrasPermissao,
	(SELECT TOP 1 IDVinculoUsuario FROM #TEMP) IDVinculoUsuario,
	CASE WHEN IDEmpresa = 59 THEN 20142 ELSE 4504 END CodigoVerba, 
	DTDiasAno DataOcorrencia, 
	HorasFalta TotalHoras, 
	isNull((DATEPART(SECOND, HorasFalta) + 60 * DATEPART(MINUTE, HorasFalta) + 3600 * DATEPART(HOUR, HorasFalta)),0) TotalSegundos,
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
