USE [PontoFrequenciaPMC]
GO

/****** Object:  UserDefinedFunction [dbo].[CalculaAtrazoPontoPlantonista]    Script Date: 30/07/2019 14:23:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER FUNCTION [dbo].[CalculaAtrazoPontoPlantonista](@EntradaManha DATETIME, @SaidaManha DATETIME, @EntradaTarde DATETIME, @SaidaTarde DATETIME, 
										   @EntradaManhaVinculo DATETIME, @SaidaManhaVinculo DATETIME, @EntradaTardeVinculo DATETIME, @SaidaTardeVinculo DATETIME, 
										   @DataFrequencia DATETIME)
RETURNS int
AS
BEGIN
  DECLARE @MinutoEntrada1 int, @MinutoSaida1 int, @MinutoEntrada2 int, @MinutoSaida2 int, @SomaTotal int

  SET @EntradaManhaVinculo = CASE WHEN (CONVERT(DATE,@EntradaManhaVinculo) = '1900-01-01') THEN @EntradaManhaVinculo+@DataFrequencia ELSE @EntradaManhaVinculo END
  SET @SaidaManhaVinculo = CASE WHEN (CONVERT(DATE,@SaidaManhaVinculo) = '1900-01-01') THEN @SaidaManhaVinculo+@DataFrequencia ELSE @SaidaManhaVinculo END

  IF(@EntradaManhaVinculo >= @SaidaManhaVinculo)
  BEGIN
	SET @SaidaManhaVinculo = DATEADD(DAY,1,@SaidaManhaVinculo)
  END


  SET @EntradaTardeVinculo = CASE WHEN (CONVERT(DATE,@EntradaTardeVinculo) = '1900-01-01') THEN @EntradaTardeVinculo+@DataFrequencia ELSE @EntradaTardeVinculo END
  SET @SaidaTardeVinculo = CASE WHEN (CONVERT(DATE,@SaidaTardeVinculo) = '1900-01-01') THEN @SaidaTardeVinculo+@DataFrequencia ELSE @SaidaTardeVinculo END
  

  IF(@EntradaTardeVinculo >= @SaidaTardeVinculo)
  BEGIN
	SET @SaidaTardeVinculo = DATEADD(DAY,1,@SaidaTardeVinculo)
  END


  SET @MinutoEntrada1 = (DATEDIFF(SECOND,@EntradaManhaVinculo, @EntradaManha))
  SET @MinutoSaida1 = DATEDIFF(SECOND,@SaidaManhaVinculo,@SaidaManha)
  SET @MinutoEntrada2 = (DATEDIFF(SECOND,@EntradaTardeVinculo,@EntradaTarde))
  SET @MinutoSaida2 = (DATEDIFF(SECOND,@SaidaTardeVinculo, @SaidaTarde))
  SET @SomaTotal = 0
    IF(@MinutoEntrada1 > 0)
  BEGIN
	SET @SomaTotal = @SomaTotal + @MinutoEntrada1
  END

  IF(@MinutoSaida1 < 0)
  BEGIN
	SET @SomaTotal = @SomaTotal + ABS(@MinutoSaida1)
  END

  IF(@MinutoEntrada2 > 0)
  BEGIN
	SET @SomaTotal = @SomaTotal + @MinutoEntrada2
  END

  IF(@MinutoSaida2 < 0)
  BEGIN
	SET @SomaTotal = @SomaTotal + ABS(@MinutoSaida2)
  END

  IF(@SomaTotal > 0)
  BEGIN
	SET @SomaTotal = @SomaTotal-900
	SET @SomaTotal = @SomaTotal / 60
  END 
  RETURN(@SomaTotal)
END




GO


