using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetodosPontoFrequencia
{
    public class AtualizacaoAutomatica
    {
        public void InsereNaTabela(System.DateTime DataHora)
        {
            DataSetPontoFrequenciaTableAdapters.TBTesteAutomaticoTableAdapter adpTeste = new DataSetPontoFrequenciaTableAdapters.TBTesteAutomaticoTableAdapter();
            adpTeste.Insert(DataHora);
        }
        public void Seleciona(DataSetPontoFrequencia ds)
        {
            DataSetPontoFrequenciaTableAdapters.TBTesteAutomaticoTableAdapter adpTeste = new DataSetPontoFrequenciaTableAdapters.TBTesteAutomaticoTableAdapter();
            adpTeste.Fill(ds.TBTesteAutomatico);
        }

        public void InsereGrafico(int Dado, int TPDado)
        {
            DataSetPontoFrequenciaTableAdapters.TBDadoTableAdapter adpDado = new DataSetPontoFrequenciaTableAdapters.TBDadoTableAdapter();
            adpDado.Insert(Dado, TPDado);
        }
        public void SelecionaGrafico(DataSetPontoFrequencia ds)
        {
            DataSetPontoFrequenciaTableAdapters.viewDadoTableAdapter adpViwDado = new DataSetPontoFrequenciaTableAdapters.viewDadoTableAdapter();
            adpViwDado.Fill(ds.viewDado);
        }
    }
}
