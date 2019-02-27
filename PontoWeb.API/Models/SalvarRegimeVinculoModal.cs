using System.Collections.Generic;

namespace PontoWeb.API.Models
{
    public class SalvarRegimeVinculoModal
    {
        public string IdRegime { get; set; }
        public List<string> lista { get; set; }
        public UsuarioModel usu { get; set; }
    }
}