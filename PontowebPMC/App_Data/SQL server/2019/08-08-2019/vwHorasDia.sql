ALTER VIEW [dbo].[vwHorasDia]
AS
SELECT        vwHorasDiaD.SituacaoN, vwHorasDiaD.Situacao, vwHorasDiaD.DataFrequencia, CONVERT(datetime, DataFrequencia, 103) AS DTFrequencia, vwHorasDiaD.DSUsuario, vwHorasDiaD.EntradaManha, 
                         vwHorasDiaD.SaidaManha, vwHorasDiaD.EntradaTarde, vwHorasDiaD.SaidaTarde, vwHorasDiaD.HorasDia, vwHorasDiaD.IDEmpresa, vwHorasDiaD.DSEmpresa, vwHorasDiaD.IDClienteSIG, vwHorasDiaD.login, 
                         vwHorasDiaD.PrimeiroNome, vwHorasDiaD.IDusuario, vwHorasDiaD.FotoUsuario, vwHorasDiaD.DSSetor, vwHorasDiaD.OBS, vwHorasDiaD.IDSetor, vwHorasDiaD.TotHorasDiarias, vwHorasDiaD.Justificado, 
                         vwHorasDiaD.MF, vwHorasDiaD.MesReferencia, vwHorasDiaD.DSMes, vwHorasDiaD.IMF, vwHorasDiaD.AnoReferencia, vwHorasDiaD.IDFrequencia, vwHorasDiaD.IMGEmpresa, 
                         vwHorasDiaD.DSEntidade, vWHorasDiaD.IDVinculoUsuario,vWHorasDiaD.IDRegimeHora,
						 vWHorasDiaD.matricula, SituacaoJustificativa,
						 vwhorasdiad.HoraEntradaManha,
						 vWHorasDiaD.HoraSaidaManha,
						 vWHorasDiaD.HoraEntradaTarde,
						 vWHorasDiaD.HoraSaidaTarde,
						 vwHorasDiaD.IDTPJustificativa,
						 vwHorasDiaD.DescontoTotalJornada,
						 vwHorasDiaD.IsencaoPonto
FROM            vwHorasDiaD
UNION
SELECT        3 AS SituacaoN, 
CASE WHEN datepart(dw, DTDiasAno) = 1 THEN 'Domingo' 
WHEN datepart(dw, DTDiasAno) = 7 
THEN 'Sábado' ELSE 'Não Bateu' END AS Situacao, 
CONVERT(nvarchar, vwh.DTDiasAno, 103) AS DTsFreq, vwh.DTDiasAno AS DTFrequencia, TBUsuario.DSUsuario AS DSUsuario, '00:00:00' AS EntradaManha, 
'00:00:00' AS SaidaManha, '00:00:00' AS EntradaTarde, '00:00:00' AS SaidaTarde, 
                         '00:00:00' AS HorasDiaD, TBVinculoUsuario.IDEmpresa, TBEmpresa.DSEmpresa, TBSetor.IDClienteSIG, TBUsuario.login, TBUsuario.PrimeiroNome,
						  TBVinculoUsuario.IDusuario, TBUsuario.FotoUsuario, 
                         TBSetor.DSSetor, 
						 CASE WHEN
						 ((SELECT dbo.DATA_ISENTA (vwh.DTDiasAno, TBUsuario.IDUsuario,TBVinculoUsuario.IDVinculoUsuario)) = 'S' 
						   AND (datepart(dw, DTDiasAno) <> 1 and datepart(dw, DTDiasAno) <> 7  )) THEN 'ISENTO DO PONTO'
						 WHEN (DTDiasAno <= GETDATE() AND datepart(dw, DTDiasAno) <> 7 
						 AND datepart(dw, DTDiasAno) <> 1) AND TBRegimeHora.RegimePlantonista = 0 
						 THEN 'FALTA INJUSTIFICADA.' 
						 WHEN datepart(dw, DTDiasAno) = 1 THEN 'Domingo' 
						 WHEN datepart(dw, DTDiasAno) = 7 THEN 'Sábado' 
						 ELSE '' END AS OBS, 
						 TBVinculoUsuario.IDSetor, CASE WHEN vwh.DTDiasAno <
                             (SELECT        TBTrocaPadraoHoraUsuario.DTTrocaPadraoHora
                               FROM            TBTrocaPadraoHoraUsuario
                               WHERE        IDVinculoUsuario = TBVinculoUsuario.IDVinculoUsuario) THEN
                             (SELECT        TOP 1 TBPadraoHoraUsuario.TotHorasDiarias
                               FROM            TBPadraoHoraUsuario
                               WHERE        DTFimPadrao != '1900-01-01' 
							   AND IDVinculoUsuario = TBVinculoUsuario.IDVinculoUsuario
							   and  vwh.DTDiasAno between DTInicioPadrao and DTFimPadrao) ELSE
                             (SELECT        TOP 1 TBPadraoHoraUsuario.TotHorasDiarias
                               FROM            TBPadraoHoraUsuario
                               WHERE        PadraoAtual = 1 
							   AND IDVinculoUsuario = TBVinculoUsuario.IDVinculoUsuario) END AS TotHorasDiarias, '' AS Justificado, 
							   CASE WHEN datepart(dw, DTDiasAno) = 1 THEN 'DOMINGO' WHEN datepart(dw,DTDiasAno) = 7 THEN 'SÁBADO' 
							   WHEN (CONVERT(datetime, vwh.DTDiasAno, 103) < CONVERT(datetime, GetDate(), 103)) 
							   							   AND TBRegimeHora.RegimePlantonista = 0
														   AND  ((SELECT dbo.DATA_ISENTA (vwh.DTDiasAno, TBUsuario.IDUsuario,TBVinculoUsuario.IDVinculoUsuario)) = 'S') THEN 'ISENTO DO PONTO'
                               WHEN (CONVERT(datetime, vwh.DTDiasAno, 103) < CONVERT(datetime, GetDate(), 103)) 
							   							   AND TBRegimeHora.RegimePlantonista = 0
														   AND  ((SELECT dbo.DATA_ISENTA (vwh.DTDiasAno, TBUsuario.IDUsuario,TBVinculoUsuario.IDVinculoUsuario)) = 'N')
							                               THEN 'FALTA INJUSTIFICADA.' ELSE '' END AS MF, 
                         datepart(month, vwH.DTDiasAno) AS MesReferencia, TBMes.DSMes, '0' AS IMF, datepart(year, vwH.DTDiasAno) AS AnoReferencia, 0 AS IDFrequencia, 
						 TBEmpresa.IMGEmpresa, TBEntidade.DSEntidade, 
                         TBVinculoUsuario.IDVinculoUsuario,TBVinculoUsuario.IDRegimeHora,
						 TBVinculoUsuario.Matricula, null SituacaoJustificativa,
						 						 tbvinculoUsuario.HoraEntradaManha,
						 tbvinculoUsuario.HoraSaidaManha,
						 tbvinculoUsuario.HoraEntradaTarde,
						 tbvinculoUsuario.HoraSaidaTarde,
						 Null as IDTPJustificativa,
						 TBVinculoUsuario.DescontoTotalJornada,
						 TBVinculoUsuario.IsencaoPonto
FROM            TBVinculoUsuario INNER JOIN
                         TBSetor ON TBVinculoUsuario.IDSetor = TBSetor.IDSetor 
						 INNER JOIN TBEmpresa ON TBVinculoUsuario.IDEmpresa = TBEmpresa.IDEmpresa 
						 AND TBEmpresa.IDEmpresa = TBSetor.IDEmpresa 
						 INNER JOIN TBUsuario ON TBVinculoUsuario.IDUsuario = TBUsuario.IDUsuario 
						 INNER JOIN TBRegimeHora ON TBVinculoUsuario.IDRegimeHora = TBRegimeHora.IDRegimeHora
						 , TBEntidade, TBDiasAno AS vwh, TBMes
WHERE        TBVinculoUsuario.IDUsuario NOT IN
                             (SELECT        IDUsuario
                               FROM            TBFrequencia
                               WHERE        CONVERT(date, DTFrequencia) = CONVERT(date, vwh.DTDiasAno) 
							   
							   AND IDVinculoUsuario = TBVinculoUsuario.IDVinculoUsuario) 
							   AND datepart(month, vwh.DTDiasAno) = TBMEs.IDmes 
							   AND TBVinculoUsuario.IDStatus = 1 
							   AND CONVERT(datetime, vwh.DTDiasAno, 103) >= CONVERT(datetime, TBVinculoUsuario.DTInicioVinculo, 103) 
							   AND (vwh.FeriadoPontoFacultativo = 0 
							   or (tbvinculousuario.idregimehora in (select idregimehora from tbregimehora where regimeplantonista = 1)))
							   AND vwh.IDEmpresa = TBEmpresa.IDEmpresa  
							   AND TBEntidade.IDEntidade = TBVinculoUsuario.identidade
UNION
SELECT        4 AS SituacaoN, 'Feriado/Ponto Facultativo' AS Situacao, CONVERT(nvarchar, TBDiasAno.DTDiasAno, 103) AS DTsFreq, TBDiasAno.DTDiasAno AS DTFrequencia, TBUsuario.DSUsuario AS DSUsuario, 
                         '00:00:00' AS EntradaManha, '00:00:00' AS SaidaManha, '00:00:00' AS EntradaTarde, '00:00:00' AS SaidaTarde, '00:00:00' AS HorasDiaD, TBVinculoUsuario.IDEmpresa, TBEmpresa.DSEmpresa, 
                         TBSetor.IDClienteSIG, TBUsuario.login, TBUsuario.PrimeiroNome, TBVinculoUsuario.IDUsuario, TBUsuario.FotoUsuario, TBSetor.DSSetor, TBDiasAno.OBS AS OBS, TBVinculoUsuario.IDSetor, 
                         CASE WHEN TBDiasAno.DTDiasAno <
                             (SELECT        TBTrocaPadraoHoraUsuario.DTTrocaPadraoHora
                               FROM            TBTrocaPadraoHoraUsuario
                               WHERE        IDVinculoUsuario = TBVinculoUsuario.IDVinculoUsuario) THEN
                             (SELECT        TOP 1 TBPadraoHoraUsuario.TotHorasDiarias
                               FROM            TBPadraoHoraUsuario
                               WHERE        DTFimPadrao != '1900-01-01' AND IDVinculoUsuario = TBVinculoUsuario.IDVinculoUsuario
							   and  TBDiasAno.DTDiasAno between DTInicioPadrao and DTFimPadrao) ELSE
                             (SELECT        TOP 1 TBPadraoHoraUsuario.TotHorasDiarias
                               FROM            TBPadraoHoraUsuario
                               WHERE        PadraoAtual = 1 AND 
							   IDVinculoUsuario = TBVinculoUsuario.IDVinculoUsuario) END AS TotHorasDiarias, '1' AS Justificado, 'Feriado/Ponto Facultativo' AS MF, datepart(month, TBDiasAno.DTDiasAno) 
                         AS MesReferencia, TBMes.DSMes, '9' AS IMF, datepart(year, TBDiasAno.DTDiasAno) AS AnoReferencia, 0 AS IDFrequencia, TBEmpresa.IMGEmpresa, TBEntidade.DSEntidade, 
                         TBVinculoUsuario.IDVinculoUsuario,TBVinculoUsuario.IDRegimeHora,
						 TBVinculoUsuario.IDRegimeHora, null SituacaoJustificativa,
						 						 tbvinculoUsuario.HoraEntradaManha,
						 tbvinculoUsuario.HoraSaidaManha,
						 tbvinculoUsuario.HoraEntradaTarde,
						 tbvinculoUsuario.HoraSaidaTarde,
						 null as IDTPJustificativa,
						 TBVinculoUsuario.DescontoTotalJornada,
						 TBVinculoUsuario.IsencaoPonto
FROM            TBDiasAno, TBUsuario, TBSetor, TBEmpresa, TBMes, TBEntidade, TBVinculoUsuario
WHERE        TBVinculoUsuario.IDUsuario = TBUsuario.IDUsuario 
AND TBVinculoUsuario.IDSetor = TBSetor.IDSetor 
AND TBVinculoUsuario.IDEmpresa = TBEmpresa.IDEmpresa 
AND TBEmpresa.IDEmpresa = TBSetor.IDEmpresa 
AND TBVinculoUsuario.IDEntidade = TBEntidade.IDEntidade 
AND datepart(month, TBDiasAno.DTDiasAno) = TBMEs.IDmes 
AND TBDiasAno.IDEmpresa = TBEmpresa.IDEmpresa 
AND TBVinculoUsuario.IDStatus = 1 
AND CONVERT(datetime, TBDiasAno.DTDiasAno, 103) >= CONVERT(datetime, TBVinculoUsuario.DTInicioVinculo, 103) 
AND TBDiasAno.FeriadoPontoFacultativo = 1
AND TBVINCULOusuario.idregimehora in (select idregimehora from tbregimehora where regimeplantonista = 0)
AND TBVinculoUsuario.IDVinculoUsuario not in 
(Select IDVinculoUsuario from TBFrequencia where DTFrequencia = TBDiasAno.DTDiasAno)



GO


