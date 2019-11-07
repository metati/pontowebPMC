using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetodosPontoFrequencia.RegistroPonto
{
    public class LogRegistro
    {
        public void LogBatida(int IDEmpresa, int idusuario, int idsetorBatida, DateTime DTFrequencia, string HashMaquina, string TempoLeitura)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("INSERT INTO dbo.Log_RegistrosClint");
            sb.AppendLine("(IDUsuario, IDEmpresa, IDSetorBatida, HashMaquina, TempoBatida)");
            sb.AppendLine("VALUES");
            sb.AppendLine("(" + idusuario + ", " + IDEmpresa + ", " + idsetorBatida + ", '" + HashMaquina + "', '" + TempoLeitura + "')");
            try
            {
                Util.ExecuteNonQuery(sb.ToString());
            }
            catch { }
        }
    }
}
