namespace MetaTI.API.Modal
{
    public class FuncionarioModel
    {
        public int IDVinculoUsuario { get; set; }
        public int IDUsuario { get; set; }
        public int IDEmpresa { get; set; }
        public int IDSetor { get; set; }
        public int IDStatus { get; set; }
        public string DSUsuario { get; set; }
        public string CPF { get; set; }
        public string Matricula { get; set; }
        public string Login { get; set; }
        public string Secretaria { get; set; }
        public string Lotacao { get; set; }
        public string Cargo { get; set; }
        public string SenhaAdmin { get; set; }
        public int IDTPUsuario { get; set; }
        public string PIS { get; set; }
        public string PrimeiroNome { get; set; }
        public bool CadastraDigital { get; set; }
        public byte[] Template1 { get; set; }
        public byte[] Template2 { get; set; }
        public int TotalHoraDia { get; set; }
        public string HoraSaidaTarde { get; set; }
        public string HoraEntradaTarde { get; set; }
        public string HoraSaidaManha { get; set; }
        public string HoraEntradaManha { get; set; }
        public bool RegimePlantonista { get; set; }
        public string TextHashCode { get; set; }
        public string ReferenciaREP { get; set; }
    }
}