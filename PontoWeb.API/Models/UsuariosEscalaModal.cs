using System.Collections.Generic;

namespace PontoWeb.API.Models
{
    public class RegimesEscalaModal
    {
        public string IdRegime { get; set; }
        public string Regime { get; set; }
        public string NumeroLinhas { get; set; }
        public List<UsuariosEscalaModal> Usuarios { get; set; }
    }

    public class UsuariosEscalaModal
    {
        public string IdUsuario { get; set; }
        public string Nome { get; set; }
        public List<DataEscalaModal> Datas { get; set; }
        public string IdVinculoRegime { get; internal set; }
    }

    public class DataEscalaModal
    {
        public string IdEscala { get; set; }
        public string DataEscala { get; set; }
        public string DataEntrada { get; set; }
        public string HoraEntrada { get; set; }
        public string DataSaida { get; set; }
        public string HoraSaida { get; set; }
        public string DiaMes { get; set; }
        public string IdUsuario { get; set; }
        public string IdVinculoRegime { get; set; }
    }
}