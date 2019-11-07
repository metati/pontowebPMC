using MetaTI.API.Modal;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace MetaTI.API.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("vs1/justificativas")]
    public class JustificativasController : ApiController
    {

        private string SalvarPedidoJustificativa(int IDFrequencia, int IDMotivoFalta, string OBS,
                string DTJust, int? TotaDia, int TPJust, int IDEmpresa, string Index, int IDUsuario, int IDVinculoUsuario)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("INSERT INTO dbo.TBFrequenciaJustificativa_Pedido");
            sb.AppendLine("(IDFrequencia, IDMotivoFalta, DTJust, IDTPJustificativa,TotaDia, IndexU, IDUsuario, IDVinculoUsuario, IDEmpresa, OBS, StatusPedido)");
            sb.AppendLine("VALUES");
            sb.AppendLine("(" + IDFrequencia + ", " + IDMotivoFalta + ", CONVERT(DATETIME,'" + DTJust + "',103), " + TPJust + "," + TotaDia + ", " + Index +
                "," + IDUsuario + ", " + IDVinculoUsuario + "," + IDEmpresa + ",'" + OBS + "', 2)");
            sb.AppendLine("UPDATE dbo.TBFrequencia SET SituacaoJustificativa = 1 where IDFrequencia = " + IDFrequencia);
            MetaTI.Util.Util.ExecuteNonQuery(sb.ToString());
            return "";
        }

        [HttpGet]
        [Route("GetDetalhesPedido/{IDFrequencia}")]
        public async Task<IHttpActionResult> GetDetalhesJustPedido(string IDFrequencia)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("IF((SELECT COUNT(*) FROM dbo.TBFrequenciaJustificativa_Pedido JP WHERE JP.IDFrequencia = " + IDFrequencia + ") > 0)");
            sb.AppendLine("BEGIN");
            sb.AppendLine("	SELECT U.DSUsuario, CONVERT(VARCHAR(12),F.DTFrequencia, 103) DTFrequencia, MV.DSMotivoFalta, TJ.DSTPJustificativa, JP.OBS,  REPLACE(isNull(JP.OBSGestor, ''), 'null','') OBSGestor,F.SituacaoJustificativa");
            sb.AppendLine("	FROM dbo.TBFrequenciaJustificativa_Pedido JP");
            sb.AppendLine("	JOIN dbo.TBVinculoUsuario VU ON (VU.IDVinculoUsuario = JP.IDVinculoUsuario)");
            sb.AppendLine("	JOIN dbo.TBUsuario U ON (U.IDUsuario = VU.IDUsuario)");
            sb.AppendLine("	JOIN dbo.TBMotivoFalta MV ON (MV.IDMotivoFalta = JP.IDMotivoFalta)");
            sb.AppendLine("	JOIN dbo.TBFrequencia F ON (F.IdFrequencia = JP.IdFrequencia)");
            sb.AppendLine("	JOIN dbo.TBTipoJustificativa TJ ON (TJ.IDTPJustificativa = JP.IDTPJustificativa)");
            sb.AppendLine("	WHERE ");
            sb.AppendLine(" JP.IDFrequencia = " + IDFrequencia);
            sb.AppendLine("Order by JP.DataInclusao desc");
            sb.AppendLine("END");
            sb.AppendLine("ELSE");
            sb.AppendLine("BEGIN");
            sb.AppendLine("	SELECT U.DSUsuario, CONVERT(VARCHAR(12),F.DTFrequencia, 103) DTFrequencia, MV.DSMotivoFalta, TJ.DSTPJustificativa, F.OBS, '' OBSGestor,F.SituacaoJustificativa");
            sb.AppendLine("	FROM dbo.TBFrequencia F");
            sb.AppendLine("	JOIN dbo.TBVinculoUsuario VU ON (VU.IDVinculoUsuario = F.IDVinculoUsuario)");
            sb.AppendLine("	JOIN dbo.TBUsuario U ON (U.IDUsuario = VU.IDUsuario)");
            sb.AppendLine("	JOIN dbo.TBMotivoFalta MV ON (MV.IDMotivoFalta = F.IDMotivoFalta)");
            sb.AppendLine("	LEFT JOIN dbo.TBTipoJustificativa TJ ON (TJ.IDTPJustificativa = F.IDTPJustificativa)");

            sb.AppendLine("	WHERE ");
            sb.AppendLine(" F.IDFrequencia = " + IDFrequencia);
            sb.AppendLine("END");

            SqlDataReader dr = MetaTI.Util.Util.getDataReader(sb.ToString());
            DetPedidoJustModal det = null;
            while (dr.Read())
            {
                det = new DetPedidoJustModal()
                {
                    DSUsuario = dr["DSUsuario"].ToString(),
                    DTFrequencia = dr["DTFrequencia"].ToString(),
                    DSMotivoFalta = dr["DSMotivoFalta"].ToString(),
                    DSTPJustificativa = dr["DSTPJustificativa"].ToString(),
                    OBS = dr["DSTPJustificativa"].ToString() + " - " + dr["OBS"].ToString(),
                    OBSGestor = dr["OBSGestor"].ToString(),
                    Situacao = dr["SituacaoJustificativa"].ToString(),
                };
                break;
            }
            dr.Close();
            return Ok(det);
        }

        [HttpGet]
        [Route("GetDetalhes/{IDFrequencia}")]
        public async Task<IHttpActionResult> GetDetalhes(string IDFrequencia)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("IF((SELECT COUNT(*) FROM dbo.TBFrequenciaJustificativa_Pedido JP WHERE JP.IDFrequencia = " + IDFrequencia + ") > 0)");
            sb.AppendLine("BEGIN");
            sb.AppendLine("SELECT JP.IDJustificativaPedido, U.DSUsuario, CONVERT(VARCHAR(12),F.DTFrequencia, 103) DTFrequencia, MV.DSMotivoFalta, TJ.DSTPJustificativa,");
            sb.AppendLine("JP.IDJustificativaPedido,JP.IDFrequencia,JP.IDMotivoFalta,CONVERT(VARCHAR(12),JP.DTJust,103) DTJust,JP.TotaDia,JP.IndexU,JP.IDTPJustificativa,JP.IDUsuario,JP.IDVinculoUsuario,JP.IDEmpresa,JP.OBS, REPLACE(isNull(JP.OBSGestor, ''), 'null','') OBSGestor,JP.StatusPedido,JP.DataInclusao,JP.IDVinculoUsuario_Aprovacao,JP.DataAprovacao,JP.DataExclusao");
            sb.AppendLine("FROM dbo.TBFrequenciaJustificativa_Pedido JP");
            sb.AppendLine("JOIN dbo.TBVinculoUsuario VU ON (VU.IDVinculoUsuario = JP.IDVinculoUsuario)");
            sb.AppendLine("JOIN dbo.TBUsuario U ON (U.IDUsuario = VU.IDUsuario)");
            sb.AppendLine("JOIN dbo.TBMotivoFalta MV ON (MV.IDMotivoFalta = JP.IDMotivoFalta)");
            sb.AppendLine("JOIN dbo.TBFrequencia F ON (F.IdFrequencia = JP.IdFrequencia)");
            sb.AppendLine("LEFT JOIN dbo.TBTipoJustificativa TJ ON (TJ.IDTPJustificativa = JP.IDTPJustificativa)");
            sb.AppendLine("WHERE");
            sb.AppendLine("JP.IDFrequencia = " + IDFrequencia);
            sb.AppendLine("Order by JP.DataInclusao desc");
            sb.AppendLine("END");
            sb.AppendLine("ELSE");
            sb.AppendLine("BEGIN");
            sb.AppendLine("SELECT NULL IDJustificativaPedido, U.DSUsuario, CONVERT(VARCHAR(12),F.DTFrequencia, 103) DTFrequencia, MV.DSMotivoFalta, TJ.DSTPJustificativa,");
            sb.AppendLine("NULL IDJustificativaPedido,F.IDFrequencia,F.IDMotivoFalta,NULL DTJust,F.TotalHorasDias TotaDia, NULL IndexU,F.IDTPJustificativa,F.IDUsuario,F.IDVinculoUsuario,F.IDEmpresa,F.OBS, '' OBSGestor,0 StatusPedido,");
            sb.AppendLine("	NULL DataInclusao,NULL IDVinculoUsuario_Aprovacao,NULL DataAprovacao,NULL DataExclusao");
            sb.AppendLine("FROM dbo.TBFrequencia F ");
            sb.AppendLine("JOIN dbo.TBVinculoUsuario VU ON (VU.IDVinculoUsuario = F.IDVinculoUsuario)");
            sb.AppendLine("JOIN dbo.TBUsuario U ON (U.IDUsuario = VU.IDUsuario)");
            sb.AppendLine("JOIN dbo.TBMotivoFalta MV ON (MV.IDMotivoFalta = F.IDMotivoFalta) ");
            sb.AppendLine("LEFT JOIN dbo.TBTipoJustificativa TJ ON (TJ.IDTPJustificativa = F.IDTPJustificativa)");
            sb.AppendLine("WHERE F.IDFrequencia =" + IDFrequencia);
            sb.AppendLine("END");
            SqlDataReader dr = MetaTI.Util.Util.getDataReader(sb.ToString());
            DetPedidoJustModal det = null;
            while (dr.Read())
            {
                det = new DetPedidoJustModal()
                {
                    DSUsuario = dr["DSUsuario"].ToString(),
                    DTFrequencia = dr["DTFrequencia"].ToString(),
                    DSMotivoFalta = dr["DSMotivoFalta"].ToString(),
                    DSTPJustificativa = dr["DSTPJustificativa"].ToString(),
                    OBS = dr["DSTPJustificativa"].ToString() + " - " + dr["OBS"].ToString(),
                    OBSGestor = dr["OBSGestor"].ToString(),
                    Situacao = dr["StatusPedido"].ToString(),
                    TB = new DetPedidoJustTBModal
                    {
                        IDJustificativaPedido = dr["IDJustificativaPedido"].ToString(),
                        DTJust = dr["DTJust"].ToString(),
                        IDEmpresa = dr["IDEmpresa"].ToString(),
                        IDFrequencia = dr["IDFrequencia"].ToString(),
                        IDMotivoFalta = dr["IDMotivoFalta"].ToString(),
                        IDTPJustificativa = dr["IDTPJustificativa"].ToString(),
                        IDUsuario = dr["IDUsuario"].ToString(),
                        IDVinculoUsuario = dr["IDVinculoUsuario"].ToString(),
                        IndexU = dr["IndexU"].ToString(),
                        OBS = dr["OBS"].ToString(),
                        TotalDia = dr["TotaDia"].ToString(),
                    }
                };

                break;
            }
            dr.Close();
            return Ok(det);
        }

        [HttpPost]
        [Route("ExcluirPedido/{IDFrequencia}")]
        public async Task<IHttpActionResult> ExcluirPedido(string IDFrequencia)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("UPDATE dbo.TBFrequenciaJustificativa_Pedido ");
            sb.AppendLine("SET StatusPedido = 0, DataExclusao = GETDATE()");
            sb.AppendLine("WHERE StatusPedido = 2");
            sb.AppendLine("AND IDFrequencia = " + IDFrequencia);
            sb.AppendLine("UPDATE dbo.TBFrequencia SET SituacaoJustificativa = null WHERE IDFrequencia = " + IDFrequencia);
            MetaTI.Util.Util.ExecuteNonQuery(sb.ToString());
            return Ok(1);
        }

        [HttpPost]
        [Route("Rejeitar/{IDUsuario}")]
        public async Task<IHttpActionResult> Rejeitar(int IDUsuario, DetPedidoJustTBModal det)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("UPDATE dbo.TBFrequenciaJustificativa_Pedido ");
            sb.AppendLine("SET StatusPedido = 0, DataAprovacao = GETDATE(), IDVinculoUsuario_Aprovacao = " + IDUsuario + ", ObsGestor = '" + det.OBSGestor + "'");
            sb.AppendLine("WHERE StatusPedido = 2");
            sb.AppendLine("AND IDFrequencia = " + det.IDFrequencia);
            sb.AppendLine("UPDATE dbo.TBFrequencia SET SituacaoJustificativa = 2 WHERE IDFrequencia = " + det.IDFrequencia);
            MetaTI.Util.Util.ExecuteNonQuery(Util.Util.GetSqlBeginTry(sb.ToString()));
            return Ok(1);
        }


        [HttpPost]
        [Route("Aceitar/{IDUsuario}")]
        public async Task<IHttpActionResult> Aceitar(int IDUsuario, DetPedidoJustTBModal det)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("UPDATE dbo.TBFrequenciaJustificativa_Pedido ");
            sb.AppendLine("SET StatusPedido = 1, DataAprovacao = GETDATE(), IDVinculoUsuario_Aprovacao = " + IDUsuario + ", ObsGestor = '" + det.OBSGestor + "'");
            sb.AppendLine("WHERE StatusPedido = 2");
            sb.AppendLine("AND IDFrequencia = " + det.IDFrequencia);
            sb.AppendLine("UPDATE dbo.TBFrequencia SET SituacaoJustificativa = 0 WHERE IDFrequencia = " + det.IDFrequencia);
            MetaTI.Util.Util.ExecuteNonQuery(Util.Util.GetSqlBeginTry(sb.ToString()));
            return Ok(1);
        }

        [HttpPost]
        [Route("EditarObs/{IDUsuario}")]
        public async Task<IHttpActionResult> EditarObs(int IDUsuario, DetPedidoJustTBModal det)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("UPDATE dbo.TBFrequenciaJustificativa_Pedido ");
            sb.AppendLine("SET IDVinculoUsuario_Aprovacao = " + IDUsuario + ", ObsGestor = '" + det.OBSGestor + "'");
            sb.AppendLine("WHERE IDJustificativaPedido = " + det.IDJustificativaPedido);
            MetaTI.Util.Util.ExecuteNonQuery(Util.Util.GetSqlBeginTry(sb.ToString()));
            return Ok(1);
        }
        
    }
}
