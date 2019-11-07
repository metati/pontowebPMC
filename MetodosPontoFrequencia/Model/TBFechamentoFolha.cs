using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetodosPontoFrequencia.Model
{
  public  class TBFechamentoFolha
    {
        public int IDFechamento { get; set; }
        public int IDEmpresa { get; set; }
        public int Mes{ get; set; }
        public int Ano { get; set; }
        public DateTime DataProcessamento { get; set; }
    }
}
