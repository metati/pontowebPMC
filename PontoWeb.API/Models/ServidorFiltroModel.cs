namespace PontoWeb.API.Models
{
    public class ServidorFiltroModel
    {
        public string Nome { get; set; }
        public string Matricula { get; set; }
        public string IdSetor { get; set; }
        public string Mes { get; set; }
        public UsuarioModel usu { get; set; }
        public string IdTipoRegime { get; set; }
        public int NumeroPagina { get; set; }
    }
}