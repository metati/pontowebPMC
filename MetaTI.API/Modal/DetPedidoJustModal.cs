namespace MetaTI.API.Modal
{
    public class DetPedidoJustModal
    {
        public string DSTPJustificativa { get; internal set; }
        public string DSUsuario { get; internal set; }
        public string DSMotivoFalta { get; internal set; }
        public string DTFrequencia { get; internal set; }
        public string OBS { get; internal set; }
        public string OBSGestor { get; internal set; }
        public string Situacao { get; internal set; }
        public DetPedidoJustTBModal TB { get; internal set; }
    }

    public class DetPedidoJustTBModal
    {
        public string IDJustificativaPedido { get; set; }
        public string IDFrequencia { get;  set; }
        public string IDMotivoFalta { get;  set; }
        public string DTJust { get; set; }
        public string TotalDia { get; set; }
        public string IndexU { get; set; }
        public string IDTPJustificativa { get; set; }
        public string IDUsuario { get; set; }
        public string IDVinculoUsuario { get;set; }
        public string IDEmpresa { get; set; }
        public string OBS { get; set; }
        public string OBSGestor { get; set; }
    }
}