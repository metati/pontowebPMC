using MetodosPontoFrequencia.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetodosPontoFrequencia.RegistroPonto
{
    public class LogConexoesClint
    {
        public bool LogClint(List<TesteConexaoModel> list, int IDEmpresa, int IDSetor, string _hashMaquina)
        {
            bool retorno = true;
            StringBuilder sb = new StringBuilder();
            foreach (var item in list)
            {
                sb.AppendLine("INSERT INTO Log_ConexoesClint (RedeLocal,RedeLocalMensagem,Internet, InternetMensagem, WSInterno, WSInternoMensagem, WSInternoTempo, WSExterno, WSExternoMensagem, WSExternoTempo, Leitor, LeitorMensagem, BancoLocal, BancoLocalMensagem, IDEmpresa, IDSetor, DataLog, HashMaquina) VALUES ");
                sb.AppendLine("(" + item.ConexaoRedeLocal + ",'" + Util.TratarString(item.ConexaoRedeLocalMensagem) + "'," +
                    item.ConexaoInternet + ", '" + Util.TratarString(item.ConexaoInternetMensagem) + "', " +
                    item.ConexaoWSInterno + ", '" + Util.TratarString(item.ConexaoWSInternoMensagem) + "', '" + Util.TratarString(item.ConexaoWSInternoTempo) + "', " +
                    item.ConexaoWSExterno + ", '" + Util.TratarString(item.ConexaoWSExternoMensagem) + "', '" + Util.TratarString(item.ConexaoWSExternoTempo) + "', " +
                    item.ConexaoLeitor + ",'" + Util.TratarString(item.ConexaoLeitorMensagem) + "', " + item.ConexaoBancoLocal + " ,'" + Util.TratarString(item.ConexaoBancoLocalMensagem) + "', " + IDEmpresa + ", " + IDSetor + ", '" + item.DataLog + "','" + Util.TratarString(_hashMaquina) + "')");
            }

            try
            {
                retorno = true;
                Util.ExecuteNonQuery(Util.GetSqlBeginTry(sb.ToString()));
            }
            catch
            {
                retorno = false;
            }

            return retorno;
        }
    }
}
