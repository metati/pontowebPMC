using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetodosPontoFrequencia.Model
{
    public class TesteConexaoModel
    {
        public int ConexaoRedeLocal { get; set; }
        public string ConexaoRedeLocalMensagem { get; set; }
        public int ConexaoInternet { get; set; }
        public string ConexaoInternetMensagem { get; set; }
        public int ConexaoWSInterno { get; set; }
        public string ConexaoWSInternoMensagem { get; set; }
        public string ConexaoWSInternoTempo { get; set; }
        public int ConexaoWSExterno { get; set; }
        public string ConexaoWSExternoMensagem { get; set; }
        public string ConexaoWSExternoTempo { get; set; }
        public int ConexaoLeitor { get; set; }
        public string ConexaoLeitorMensagem { get; set; }
        public int ConexaoBancoLocal { get; set; }
        public string ConexaoBancoLocalMensagem { get; set; }
        public string UrlWS { get; set; }
        public string DataLog { get; set; }
    }
}
