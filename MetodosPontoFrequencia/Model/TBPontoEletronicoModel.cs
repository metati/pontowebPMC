using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetodosPontoFrequencia.Model
{
    public class TBPontoEletronicoModel
    {
        public int PontoEletronicoID { get; set; }

        public string PontoEletronico_Nome { get; set; }

        public string PontoEletronico_Local { get; set; }

        public string PontoEletronico_Ip { get; set; }

        public string PontoEletronico_Usuario { get; set; }

        public string PontoEletronico_Senha { get; set; }

        public int PontoEletronico_Porta { get; set; }

    }
}
