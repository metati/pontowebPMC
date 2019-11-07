USE [PontoFrequenciaPMC]
GO

/****** Object:  UserDefinedFunction [dbo].[CalculaAtrazoPonto]    Script Date: 30/07/2019 14:24:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



ALTER FUNCTION [dbo].[CalculaAtrazoPonto](@EntradaManha TIME, @SaidaManha TIME, @EntradaTarde TIME, @SaidaTarde TIME, 
										   @EntradaManhaVinculo TIME, @SaidaManhaVinculo TIME, @EntradaTardeVinculo TIME, @SaidaTardeVinculo TIME)
RETURNS int
AS
BEGIN
  DECLARE @MinutoEntrada1 int, @MinutoSaida1 int, @MinutoEntrada2 int, @MinutoSaida2 int, @SomaTotal int
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
	SET @SomaTotal = @SomaTotal-(900) -- em segundos
	SET @SomaTotal = @SomaTotal/60
  END 
    
  RETURN(@SomaTotal)
END



GO
