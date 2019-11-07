using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetodosPontoFrequencia
{
    public class LogOperacao
    {
        public void RegistraLog(int IDUsuario, DateTime DTOperacao, string Metodo, string Operacao, string Dado, string DadoAnterior, int IDEmpresa)
        {
            DataSetPontoFrequenciaTableAdapters.TBLogTableAdapter adpLog = new DataSetPontoFrequenciaTableAdapters.TBLogTableAdapter();
            try
            {
                adpLog.Connection.Open();
                adpLog.Insert(IDUsuario, Operacao, Dado, DadoAnterior, DTOperacao, Metodo,IDEmpresa);
                adpLog.Connection.Close();
            }
            catch(Exception ex)
            {
                ex.ToString();
            }
        }
    }
}
