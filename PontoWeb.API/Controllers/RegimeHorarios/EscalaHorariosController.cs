using PontoWeb.API.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace PontoWeb.API.Controllers.RegimeHorarios
{
    [RoutePrefix("api/vs1/escala")]
    public class EscalaHorariosController : ApiController
    {

        [HttpGet]
        [Route("GetCalendario/{mes}/{IdEmpresa}")]
        public async Task<IHttpActionResult> GetCalendario(string mes, string IdEmpresa)
        {
            mes = Util.TratarString(mes);
            List<object> list = new List<object>();
            StringBuilder sb = new StringBuilder();
            if (mes == "0") { mes = "AND datepart(MM, DTDiasAno) = datepart(MM, GETDATE())"; } else { mes = "AND datepart(MM, DTDiasAno) = " + mes; }
            sb.AppendLine("SELECT datepart(DW, DTDiasAno) NumDiaSemana, datepart(MM, DTDiasAno) MesAno,datepart(DAY, DTDiasAno) DiaMes,datepart(week, DTDiasAno) SemanaAno, DTDiasAno");
            sb.AppendLine("FROM PontoFrequenciaPMC.dbo.TBDiasAno");
            sb.AppendLine("WHERE IDEmpresa = " + IdEmpresa + " AND datepart(YYYY, DTDiasAno) = datepart(YYYY, GETDATE())");
            sb.AppendLine(mes);
            sb.AppendLine("ORDER BY DiaMes");
            SqlDataReader dr = Util.getDataReader(sb.ToString());
            while (dr.Read())
            {
                list.Add(new
                {
                    NumDiaSemana = dr["NumDiaSemana"].ToString(),
                    DiaSemana = Util.GetDiaSemana(dr["NumDiaSemana"].ToString()),
                    MesAno = dr["MesAno"].ToString(),
                    DiaMes = dr["DiaMes"].ToString(),
                    SemanaAno = dr["SemanaAno"].ToString(),
                    DTDiasAno = dr["DTDiasAno"].ToString(),
                });
            }
            dr.Close();
            return Ok(list);
        }


        [HttpPost]
        [Route("GetEscalasUsuarios")]
        public async Task<IHttpActionResult> GetEscalasUsuarios(ServidorFiltroModel filtro)
        {
            int RowInicial = (((filtro.NumeroPagina) * 50) - 50);
            int RowFinal = (((filtro.NumeroPagina) * 50));

            string where = "", where2 = "";
            if (!string.IsNullOrEmpty(filtro.Nome))
            {
                where += " AND U.DSUsuario like '%" + Util.TratarString(filtro.Nome) + "%'";
                where2 += " AND U.DSUsuario like '%" + Util.TratarString(filtro.Nome) + "%'";
            }
            if (!string.IsNullOrEmpty(filtro.Matricula))
            {
                where += " AND VU.Matricula like '" + Util.TratarString(filtro.Matricula) + "'";
                where2 += " AND VU.Matricula like '" + Util.TratarString(filtro.Matricula) + "'";
            }
            if (!string.IsNullOrEmpty(filtro.IdSetor))
            {
                where += " AND VU.IDSetor = " + Util.TratarString(filtro.IdSetor);
                where2 += " AND VU.IDSetor = " + Util.TratarString(filtro.IdSetor);
            }
            if (!string.IsNullOrEmpty(filtro.Mes))
            {
                where += " AND isNull(datepart(MM, RE.DataEscala)," + Util.TratarString(filtro.Mes) + ") = " + Util.TratarString(filtro.Mes);
            }
            if (!string.IsNullOrEmpty(filtro.IdTipoRegime))
            {
                where += " AND VU.IDRegimeHora = " + Util.TratarString(filtro.IdTipoRegime);
                where2 += " AND VU.IDRegimeHora = " + Util.TratarString(filtro.IdTipoRegime);
            }

            string idRegime = "", idUsuario = "";
            bool loopRegime = true;
            List<RegimesEscalaModal> list = new List<RegimesEscalaModal>();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT datepart(DAY, RE.DataEscala) DiaMes, RE.IdEscala, RE.DataEscala, CONVERT(VARCHAR(12),RE.DataHoraEntrada,103) DataEntrada, CONVERT(VARCHAR(5),CONVERT(TIME(4),RE.DataHoraEntrada)) HoraEntrada, ");
            sb.AppendLine("CONVERT(VARCHAR(12),RE.DataHorasSaida,103) DataSaida, CONVERT(VARCHAR(5),CONVERT(TIME(4),RE.DataHorasSaida)) HoraSaida, VU.IdVinculoUsuario IdUsuario, VU.IDVinculoUsuario IdVinculoRegime,R.IDRegimeHora,R.DSRegimeHora Regime,");
            sb.AppendLine("U.DSUsuario");
            sb.AppendLine("INTO #TEMP");
            sb.AppendLine("FROM dbo.TBVinculoUsuario VU");
            sb.AppendLine("JOIN dbo.TBRegimeHora R ON (R.IDRegimeHora = VU.IDRegimeHora)");
            sb.AppendLine("JOIN dbo.TBUsuario U ON (U.IdUsuario = VU.IdUsuario)");
            sb.AppendLine("LEFT JOIN TbRegimeHorario_Escala RE ON(VU.IDVinculoUsuario = RE.IdVinculoUsuario)");
            sb.AppendLine("WHERE VU.IDStatus = 1 AND VU.IdEmpresa = " + filtro.usu.IdEmpresa);
            sb.AppendLine(where);


            sb.AppendLine("SELECT *");
            sb.AppendLine("INTO #TEMP2");
            sb.AppendLine("FROM(");
            sb.AppendLine("SELECT * FROM #TEMP");
            sb.AppendLine("UNION");
            sb.AppendLine("SELECT NULL DiaMes, NULL IdEscala, NULL DataEscala, NULL DataEntrada, NULL HoraEntrada, NULL DataSaida, NULL HoraSaida, ");
            sb.AppendLine("VU.IdVinculoUsuario IdUsuario, VU.IdVinculoUsuario IdVinculoRegime,R.IDRegimeHora,R.DSRegimeHora Regime,");
            sb.AppendLine("U.DSUsuario");
            sb.AppendLine("FROM dbo.TBVinculoUsuario VU WITH(NOLOCK)");
            sb.AppendLine("JOIN dbo.TBRegimeHora R  WITH(NOLOCK) ON (R.IDRegimeHora = VU.IDRegimeHora)");
            sb.AppendLine("JOIN dbo.TBUsuario U WITH(NOLOCK) ON (U.IdUsuario = VU.IdUsuario)");
            sb.AppendLine("WHERE VU.IDStatus = 1 AND VU.IdEmpresa = " + filtro.usu.IdEmpresa);
            sb.AppendLine("AND VU.IDVinculoUsuario NOT IN(SELECT IdVinculoRegime FROM #TEMP)");
            sb.AppendLine(where2);
            sb.AppendLine(") A");
            sb.AppendLine("DECLARE @CountRows int");
            sb.AppendLine("SET @CountRows = (SELECT COUNT(*) FROM (SELECT DISTINCT IdVinculoRegime FROM #TEMP2) A)");
            sb.AppendLine("SELECT DiaMes, IdEscala, DataEscala, DataEntrada, HoraEntrada, DataSaida, HoraSaida, IdUsuario, IdVinculoRegime, IDRegimeHora, Regime, @CountRows NumeroLinhas,");
            sb.AppendLine("CONVERT(VARCHAR(20), DSUsuario) DSUsuario");
            sb.AppendLine("FROM #TEMP2");
            sb.AppendLine("WHERE IdVinculoRegime IN");
            sb.AppendLine("(");
            sb.AppendLine("    SELECT DISTINCT IdVinculoRegime");
            sb.AppendLine("	FROM #TEMP2");
            sb.AppendLine("	ORDER BY IdVinculoRegime");
            sb.AppendLine("	OFFSET " + RowInicial + " ROWS FETCH NEXT " + RowFinal + " ROWS ONLY");
            sb.AppendLine(")");
            sb.AppendLine("ORDER BY IDRegimeHora, DSUsuario,  IdUsuario, DiaMes");
            //sb.AppendLine("ORDER BY IDRegimeHora, DSUsuario, DiaMes");




            #region OLD
            //sb.AppendLine("SELECT datepart(DAY, RE.DataEscala) DiaMes, RE.IdEscala, RE.DataEscala, CONVERT(VARCHAR(12),RE.DataHoraEntrada,103) DataEntrada, CONVERT(VARCHAR(5),CONVERT(TIME(4),RE.DataHoraEntrada)) HoraEntrada, ");
            //sb.AppendLine("CONVERT(VARCHAR(12),RE.DataHorasSaida,103) DataSaida, CONVERT(VARCHAR(5),CONVERT(TIME(4),RE.DataHorasSaida)) HoraSaida, VU.IdVinculoUsuario IdUsuario, RV.IdVinculoRegime,R.IDRegimeHora,R.DSRegimeHora Regime,");
            //sb.AppendLine("CASE WHEN SUBSTRING(U.DSUsuario, 0, CHARINDEX(' ', U.DSUsuario, CHARINDEX(' ', U.DSUsuario, 0) + 1)) <> '' THEN SUBSTRING(U.DSUsuario, 0, CHARINDEX(' ', U.DSUsuario, CHARINDEX(' ', U.DSUsuario, 0) + 1)) ELSE U.DSUsuario END DSUsuario");
            //sb.AppendLine("INTO #TEMP");
            //sb.AppendLine("FROM dbo.TbRegimeHorario_Vinculo RV");
            //sb.AppendLine("JOIN dbo.TBRegimeHora R ON (R.IDRegimeHora = RV.IDRegimeHora)");
            //sb.AppendLine("JOIN dbo.TBVinculoUsuario VU ON (VU.IDVinculoUsuario = RV.IDVinculoUsuario)");
            //sb.AppendLine("JOIN dbo.TBUsuario U ON (U.IdUsuario = VU.IdUsuario)");
            //sb.AppendLine("LEFT JOIN TbRegimeHorario_Escala RE ON(RV.IdVinculoRegime = RE.IdVinculoRegime)");
            //sb.AppendLine("WHERE VU.IdEmpresa = " + filtro.usu.IdEmpresa);
            //sb.AppendLine(where);

            //sb.AppendLine("SELECT * FROM #TEMP");
            //sb.AppendLine("UNION");
            //sb.AppendLine("SELECT NULL DiaMes, NULL IdEscala, NULL DataEscala, NULL DataEntrada, NULL HoraEntrada, NULL DataSaida, NULL HoraSaida, ");
            //sb.AppendLine("VU.IdVinculoUsuario IdUsuario, RV.IdVinculoRegime,R.IDRegimeHora,R.DSRegimeHora Regime,");
            //sb.AppendLine("CASE WHEN SUBSTRING(U.DSUsuario, 0, CHARINDEX(' ', U.DSUsuario, CHARINDEX(' ', U.DSUsuario, 0) + 1)) <> '' THEN SUBSTRING(U.DSUsuario, 0, CHARINDEX(' ', U.DSUsuario, CHARINDEX(' ', U.DSUsuario, 0) + 1)) ELSE U.DSUsuario END DSUsuario");
            //sb.AppendLine("FROM dbo.TbRegimeHorario_Vinculo RV");
            //sb.AppendLine("JOIN dbo.TBRegimeHora R ON (R.IDRegimeHora = RV.IDRegimeHora)");
            //sb.AppendLine("JOIN dbo.TBVinculoUsuario VU ON (VU.IDVinculoUsuario = RV.IDVinculoUsuario)");
            //sb.AppendLine("JOIN dbo.TBUsuario U ON (U.IdUsuario = VU.IdUsuario)");
            //sb.AppendLine("WHERE VU.IdEmpresa = " + filtro.usu.IdEmpresa);
            //sb.AppendLine(where2);
            //sb.AppendLine("ORDER BY IDRegimeHora, DSUsuario, DiaMes");
            #endregion OLD

            SqlDataReader dr = Util.getDataReader(sb.ToString());
            int i = 0;
            int indUs = 0;
            while (dr.Read())
            {
                if (idRegime != dr["IDRegimeHora"].ToString()) { loopRegime = true; }
                if (loopRegime)
                {
                    list.Add(new RegimesEscalaModal()
                    {
                        IdRegime = dr["IDRegimeHora"].ToString(),
                        Regime = dr["Regime"].ToString(),
                        NumeroLinhas = dr["NumeroLinhas"].ToString(),
                        Usuarios = new List<UsuariosEscalaModal>()
                    });
                    loopRegime = false;
                }

                i = list.Count - 1;

                if ((idUsuario != dr["IdUsuario"].ToString()) || (idRegime != dr["IDRegimeHora"].ToString()))
                {
                    list[i].Usuarios.Add(new UsuariosEscalaModal
                    {
                        IdUsuario = dr["IdUsuario"].ToString(),
                        Nome = dr["DSUsuario"].ToString(),
                        IdVinculoRegime = dr["IdVinculoRegime"].ToString(),
                        Datas = new List<DataEscalaModal>()
                    });
                }
                indUs = list[i].Usuarios.Count - 1;

                list[i].Usuarios[indUs].Datas.Add(new DataEscalaModal
                {
                    IdEscala = dr["IdEscala"].ToString(),
                    DataEscala = dr["DataEscala"].ToString(),
                    DataEntrada = dr["DataEntrada"].ToString(),
                    HoraEntrada = dr["HoraEntrada"].ToString(),
                    DataSaida = dr["DataSaida"].ToString(),
                    HoraSaida = dr["HoraSaida"].ToString(),
                    DiaMes = dr["DiaMes"].ToString(),
                });

                idRegime = dr["IDRegimeHora"].ToString();
                idUsuario = dr["IdUsuario"].ToString();
            }
            dr.Close();
            return Ok(list);
        }


        [HttpPost]
        [Route("GetSetores")]
        public async Task<IHttpActionResult> GetSetores(UsuarioModel us)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT A.IDSetor,DSSetor Descricao FROM dbo.TBSetor A WHERE A.IDEmpresa = " + us.IdEmpresa);
            SqlDataReader dr = Util.getDataReader(sb.ToString());
            List<object> list = new List<object>();
            list.Add(new { IdSetor = "", Descricao = "" });
            while (dr.Read())
            {
                list.Add(new
                {
                    IdSetor = dr["IdSetor"].ToString(),
                    Descricao = dr["Descricao"].ToString(),
                });
            }
            dr.Close();
            return Ok(list);
        }

        [HttpPost]
        [Route("GetRegimes")]
        public async Task<IHttpActionResult> GetRegimes(UsuarioModel us)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM dbo.TBRegimeHora WHERE RegimePlantonista = 1");
            SqlDataReader dr = Util.getDataReader(sb.ToString());
            List<object> list = new List<object>();
            list.Add(new { IDRegimeHora = "", DSRegimeHora = "" });
            while (dr.Read())
            {
                list.Add(new
                {
                    IDRegimeHora = dr["IDRegimeHora"].ToString(),
                    DSRegimeHora = dr["DSRegimeHora"].ToString(),
                    TotalHoraSemana = dr["TotalHoraSemana"].ToString(),
                    TotalHoraDia = dr["TotalHoraDia"].ToString(),
                    RegimePlantonista = dr["RegimePlantonista"].ToString(),
                    PermitehoraExtra = dr["PermitehoraExtra"].ToString(),
                    TotalMaximoHoraExtraDia = dr["TotalMaximoHoraExtraDia"].ToString(),
                    TotalMaximoHoraExtraMes = dr["TotalMaximoHoraExtraMes"].ToString(),
                    TotalHorasFolgaPlantonista = dr["TotalHorasFolgaPlantonista"].ToString(),
                    HoraEntrada = ((dr["HoraEntrada"].ToString().Length > 0) ? dr["HoraEntrada"].ToString().Substring(0, 4) : dr["HoraEntrada"].ToString()),
                    HoraSaida = ((dr["HoraSaida"].ToString().Length > 0) ? dr["HoraSaida"].ToString().Substring(0, 4) : dr["HoraSaida"].ToString()),
                });
            }
            dr.Close();
            return Ok(list);
        }


        [HttpPost]
        [Route("GetUsuarios")]
        public async Task<IHttpActionResult> GetUsuarios(ServidorFiltroModel filtro)
        {
            StringBuilder sb = new StringBuilder();
            string where = "";
            if (!string.IsNullOrEmpty(filtro.Nome))
            {
                where += " AND A.DSUsuario like '%" + Util.TratarString(filtro.Nome) + "%'";
            }
            if (!string.IsNullOrEmpty(filtro.Matricula))
            {
                where += " AND B.Matricula like '" + Util.TratarString(filtro.Matricula) + "'";
            }
            if (!string.IsNullOrEmpty(filtro.IdSetor))
            {
                where += " AND B.IDSetor = " + Util.TratarString(filtro.IdSetor);
            }
            sb.AppendLine("SELECT DISTINCT B.IDVinculoUsuario Codigo, A.DSUsuario, isNull(B.Matricula, '') Matricula, B.IDRegimeHora");
            sb.AppendLine("FROM TBUsuario A");
            sb.AppendLine("JOIN TBVinculoUsuario B ON (A.IDUsuario = B.IDUsuario)");
            //sb.AppendLine("LEFT JOIN TbRegimeHorario_Vinculo C ON (C.IDVinculoUsuario = B.IDVinculoUsuario AND C.Situacao = 1)");
            sb.AppendLine("WHERE B.IDEmpresa = " + filtro.usu.IdEmpresa);
            sb.AppendLine(where);
            List<object> list = new List<object>();
            SqlDataReader dr = Util.getDataReader(sb.ToString());
            while (dr.Read())
            {
                list.Add(new
                {
                    Codigo = dr["Codigo"].ToString(),
                    Nome = dr["DSUsuario"].ToString(),
                    Matricula = dr["Matricula"].ToString(),
                    IdRegimeHora = dr["IDRegimeHora"].ToString(),
                });
            }
            dr.Close();
            return Ok(list);
        }

        [HttpPost]
        [Route("SalvarRegimeVinculo")]
        public async Task<IHttpActionResult> SalvarRegimeVinculo(SalvarRegimeVinculoModal obj)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in obj.lista)
            {
                sb.AppendLine("IF((SELECT COUNT(A.IdVinculoRegime) FROM TbRegimeHorario_Vinculo A  WHERE IDVinculoUsuario = " + item + " AND Situacao = 1 AND IDRegimeHora = " + obj.IdRegime + ") = 0)");
                sb.AppendLine("BEGIN");
                sb.AppendLine("DELETE FROM TbRegimeHorario_Vinculo WHERE IDVinculoUsuario = " + item);
                sb.AppendLine("AND IdVinculoRegime NOT IN ");
                sb.AppendLine("(SELECT A.IdVinculoRegime FROM TbRegimeHorario_Vinculo A  JOIN TbRegimeHorario_Escala B ON (A.IdVinculoRegime = B.IdVinculoRegime)  WHERE IDVinculoUsuario = " + item + " AND Situacao = 0)");

                sb.AppendLine("UPDATE TbRegimeHorario_Vinculo SET Situacao = 0 WHERE IDVinculoUsuario = " + item);
                sb.AppendLine("INSERT INTO TbRegimeHorario_Vinculo (IDVinculoUsuario, IDRegimeHora, IdOperadorInclusao)");
                sb.AppendLine("VALUES (" + item + ", " + obj.IdRegime + ", " + obj.usu.IdUsuario + ")");
                sb.AppendLine("END");
            }
            Util.ExecuteNonQuery(Util.GetSqlBeginTry(sb.ToString()));
            return Ok(1);
        }

        [HttpPost]
        [Route("SalvarEscala/{IdEmpresa}/{IdUsuario}/{CriarAutomatico}")]
        public async Task<IHttpActionResult> SalvarEscala(string IdEmpresa, string IdUsuario, int CriarAutomatico, DataEscalaModal obj)
        {
            StringBuilder sb = new StringBuilder();


            sb.AppendLine("DECLARE @IdVinculoUsuario bigint, @DataEscala datetime, @DataHoraEntrada datetime, @DataHorasSaida datetime, @IdOperadorInclusao int, @Mes int, @HorasFolga int, @TotalJornada int, @IDRegimeHora INT");

            sb.AppendLine("SET @IdVinculoUsuario = " + obj.IdVinculoRegime);
            sb.AppendLine("SET @DataEscala = CONVERT(DATETIME, '" + obj.DataEscala + "')");
            sb.AppendLine("SET @DataHoraEntrada = CONVERT(DATETIME, '" + obj.DataEntrada + " " + obj.HoraEntrada + "')");
            sb.AppendLine("SET @DataHorasSaida = CONVERT(DATETIME, '" + obj.DataSaida + " " + obj.HoraSaida + "') ");
            sb.AppendLine("SET @IdOperadorInclusao = " + IdUsuario);
            sb.AppendLine("SET @IDRegimeHora = (SELECT IdRegimeHora FROM TBVinculoUsuario WHERE IdVinculoUsuario = @IdVinculoUsuario)");

            sb.AppendLine("CREATE TABLE #TEMP(IdVinculoUsuario bigint NULL,DataEscala datetime NOT NULL,DataHoraEntrada datetime NULL,DataHorasSaida datetime NULL,IdOperadorInclusao int NOT NULL)");

            sb.AppendLine("SET @Mes = DATEPART(MM, @DataEscala)");
            sb.AppendLine("SELECT @HorasFolga = TotalHorasFolgaPlantonista, @TotalJornada = TotalHoraDia");
            sb.AppendLine("FROM TBRegimeHora ");
            sb.AppendLine("WHERE IDRegimeHora = @IDRegimeHora");


            sb.AppendLine("IF(@TotalJornada = 10 AND @IDRegimeHora = 22)");
            sb.AppendLine("BEGIN");
            sb.AppendLine("	SET @TotalJornada = @TotalJornada+2");
            sb.AppendLine("END ");

            sb.AppendLine("INSERT INTO #TEMP");
            sb.AppendLine("(IdVinculoUsuario, DataEscala, DataHoraEntrada, DataHorasSaida, IdOperadorInclusao)");
            sb.AppendLine("VALUES");
            sb.AppendLine("(@IdVinculoUsuario, @DataEscala, @DataHoraEntrada, @DataHorasSaida, @IdOperadorInclusao)");


            //PREENCHE OS OUTROS DIAS, CASO AUTORIZADO
            sb.AppendLine("IF(1 = " + CriarAutomatico + " AND @HorasFolga > 0 AND @IDRegimeHora <> 21)");
            sb.AppendLine("BEGIN");
            sb.AppendLine("	WHILE(@Mes = DATEPART(MM, @DataHorasSaida))");
            sb.AppendLine("	BEGIN");
            sb.AppendLine("		SET @DataHoraEntrada = DATEADD(HOUR, @HorasFolga,@DataHorasSaida)");
            sb.AppendLine("		SET @DataHorasSaida = DATEADD(HOUR, @HorasFolga+@TotalJornada,@DataHorasSaida)");
            sb.AppendLine("		SET @DataEscala = CONVERT(DATETIME, CONVERT(VARCHAR(12),@DataHoraEntrada,103), 103)");
            sb.AppendLine("		INSERT INTO #TEMP");
            sb.AppendLine("		(IdVinculoUsuario, DataEscala, DataHoraEntrada, DataHorasSaida, IdOperadorInclusao)");
            sb.AppendLine("		VALUES");
            sb.AppendLine("		(@IdVinculoUsuario, @DataEscala, @DataHoraEntrada, @DataHorasSaida, @IdOperadorInclusao)");
            sb.AppendLine("	END");
            sb.AppendLine("END");

            sb.AppendLine("ELSE IF(1 = " + CriarAutomatico + " AND @HorasFolga > 0 AND @IDRegimeHora = 21)");
            sb.AppendLine("BEGIN");
            sb.AppendLine("		INSERT INTO #TEMP");
            sb.AppendLine("		(IdVinculoUsuario, DataEscala, DataHoraEntrada, DataHorasSaida, IdOperadorInclusao)");
            sb.AppendLine("		SELECT @IdVinculoUsuario,DTDiasAno,");
            sb.AppendLine("		CAST(CAST(DTDiasAno AS DATE) AS VARCHAR(10)) + ' ' + CAST(CAST(@DataHoraEntrada  AS TIME) AS varchar(8)),");
            sb.AppendLine("		CAST(CAST(DTDiasAno  AS DATE) AS VARCHAR(10)) + ' ' + CAST(CAST(@DataHorasSaida AS TIME) AS varchar(8)),");
            sb.AppendLine("		@IdOperadorInclusao");
            sb.AppendLine("		FROM [PontoFrequenciaPMC].[dbo].[TBDiasAno]");
            sb.AppendLine("		WHERE DATEPART(YYYY,DTDiasAno) = DATEPART(YYYY,GETDATE()) ");
            sb.AppendLine("		AND ( FeriadoPontoFacultativo = 1 OR ");
            sb.AppendLine("		DATEPART(WEEKDAY,DTDiasAno) = 7 OR DATEPART(WEEKDAY,DTDiasAno) = 1)");
            sb.AppendLine("		AND IDEmpresa = " + IdEmpresa);
            sb.AppendLine("		AND DATEPART(MM,DTDiasAno) = @Mes");
            sb.AppendLine("		AND DTDiasAno NOT IN (@DataEscala)");
            sb.AppendLine("		ORDER BY DTDiasAno");
            sb.AppendLine("END");


            sb.AppendLine("INSERT INTO TbRegimeHorario_Escala");
            sb.AppendLine("(IdVinculoUsuario, DataEscala, DataHoraEntrada, DataHorasSaida, IdOperadorInclusao, IdVinculoRegime)");
            sb.AppendLine("SELECT IdVinculoUsuario, DataEscala, DataHoraEntrada, DataHorasSaida, IdOperadorInclusao, 1 FROM #TEMP");
            sb.AppendLine("WHERE DataEscala NOT IN(SELECT DataEscala FROM TbRegimeHorario_Escala WHERE DATEPART(MM, DataEscala) = @Mes AND IdVinculoUsuario = @IdVinculoUsuario)");
            Util.ExecuteNonQuery(sb.ToString());
            return Ok(obj);
        }


        [HttpPost]
        [Route("AlterarEscala/{IdEmpresa}/{IdUsuario}")]
        public async Task<IHttpActionResult> AlterarEscala(string IdEmpresa, string IdUsuario, DataEscalaModal obj)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("UPDATE TbRegimeHorario_Escala SET");
            sb.AppendLine("IdVinculoUsuario = " + obj.IdVinculoRegime);
            sb.AppendLine(",DataEscala = CONVERT(DATETIME, '" + obj.DataEscala + "')");
            sb.AppendLine(",DataHoraEntrada =  CONVERT(DATETIME, '" + obj.DataEntrada + " " + obj.HoraEntrada + "')");
            sb.AppendLine(",DataHorasSaida = CONVERT(DATETIME, '" + obj.DataSaida + " " + obj.HoraSaida + "')");
            sb.AppendLine(",IdOperadorAlteracao = " + IdUsuario);
            sb.AppendLine(" WHERE IdEscala = " + obj.IdEscala);
            Util.ExecuteNonQuery(sb.ToString());
            return Ok(obj);
        }

        [HttpPost]
        [Route("Deletar/{IdEscala}")]
        public async Task<IHttpActionResult> Deletar(string IdEscala, UsuarioModel us)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("DELETE FROM TbRegimeHorario_Escala ");
            sb.AppendLine(" WHERE IdEscala = " + IdEscala);
            Util.ExecuteNonQuery(sb.ToString());
            return Ok(1);
        }

        [HttpPost]
        [Route("DeletarLancamentos/{IdUsuario}/{Mes}")]
        public async Task<IHttpActionResult> DeletarLancamentos(string IdUsuario, string Mes)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("DELETE FROM TbRegimeHorario_Escala ");
            sb.AppendLine(" WHERE IdVinculoUsuario = " + Util.TratarString(IdUsuario));
            sb.AppendLine(" AND DATEPART(MM,DataEscala) = " + Util.TratarString(Mes));
            Util.ExecuteNonQuery(sb.ToString());
            return Ok(1);
        }

        [HttpGet]
        [Route("GetDadosUsuario/{IdUsuario}")]
        public async Task<IHttpActionResult> GetDadosUsuario(string IdUsuario)
        {
            object item = null;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT U.DSUsuario, VU.HoraEntradaManha, VU.HoraSaidaManha, VU.HoraEntradaTarde, VU.HoraSaidaTarde, VU.Matricula, S.DSSetor");
            sb.AppendLine("FROM TbVinculoUsuario VU WITH(NOLOCK)");
            sb.AppendLine("JOIN TBUsuario U WITH(NOLOCK) ON (U.IdUsuario = VU.IdUsuario)");
            sb.AppendLine("JOIN TBEmpresa E WITH(NOLOCK) ON (E.IdEmpresa = VU.IdEmpresa)");
            sb.AppendLine("JOIN TBSetor S WITH(NOLOCK) ON (S.IDSetor = VU.IDSetor)");
            sb.AppendLine("WHERE IdVinculoUsuario = " + IdUsuario);
            SqlDataReader dr = Util.getDataReader(sb.ToString());
            if (dr.Read())
            {
                item = new
                {
                    Nome = dr["DSUsuario"].ToString(),
                    Matricula = dr["Matricula"].ToString(),
                    Setor = dr["DSSetor"].ToString(),
                    Entrada1 = dr["HoraEntradaManha"].ToString(),
                    Saida1 = dr["HoraSaidaManha"].ToString(),
                    Entrada2 = dr["HoraEntradaTarde"].ToString(),
                    Saida2 = dr["HoraSaidaTarde"].ToString(),
                };
            }
            dr.Close();
            return Ok(item);
        }
    }
}