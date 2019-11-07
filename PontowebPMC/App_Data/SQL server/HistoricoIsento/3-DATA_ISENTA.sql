CREATE FUNCTION DATA_ISENTA(
@P_DTFREQUENCIA DATE,
@P_IDUSUARIO INT,
@P_IDVINCULO BIGINT
)
RETURNS VARCHAR(1)
AS
BEGIN
DECLARE @RESULTADO VARCHAR(1), @DTINICIO DATE, @DTFIM DATE
SET @RESULTADO = 'N';
DECLARE  CURSOR_ISENTO CURSOR LOCAL
FOR 
SELECT P.DTInicio, P.DTFim 
FROM TBUsuarioIsentoPadrao P
WHERE P.IDUsuario = @P_IDUSUARIO
AND P.IDVinculoUsuario = @P_IDVINCULO
AND P.Isento = 1
OPEN CURSOR_ISENTO
FETCH NEXT FROM CURSOR_ISENTO INTO @DTINICIO, @DTFIM
WHILE @@FETCH_STATUS = 0 
BEGIN
IF(@DTFIM IS NOT NULL)
BEGIN
   IF(@P_DTFREQUENCIA BETWEEN @DTINICIO AND @DTFIM)
    BEGIN
     SET @RESULTADO = 'S';
     BREAK;
    END
END
ELSE
BEGIN
 IF(@P_DTFREQUENCIA >= @DTINICIO)
     BEGIN
      SET @RESULTADO = 'S';
      BREAK;
     END
END

FETCH NEXT FROM CURSOR_ISENTO INTO @DTINICIO, @DTFIM
END
CLOSE CURSOR_ISENTO
DEALLOCATE CURSOR_ISENTO
RETURN(@RESULTADO)
END





