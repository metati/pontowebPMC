using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetaTI.Util
{
    public class InfoClints
    {
        public void InsereNovoClientHash(string SerialHD, string SerialProcessador, string MACRede, string IPLocal, string NomeComputador,
string SistemaOperacional, DateTime DTultimaConexao, int IDEmpresa, int IDSetor, string VersaoClient, string MemoriaTotal,
string EspacoLivreHD, string CapacidadeHD, string ArquiteturaMaquina, string Processador, string Chave, string HashMaquina)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("IF((SELECT COUNT(*) FROM TBClient WHERE Chave = '" + Chave + "') = 0)");
            sb.AppendLine("BEGIN");
            sb.AppendLine("INSERT INTO dbo.TBClient");
            sb.AppendLine("(Chave, SerialHD, SerialProcessador, MACRede, TotalMemoria, CapacidadeHD, EspacoLivreHD, NomeProcessador, Arquitetura, IPLocal, NomeComputador, SistemaOperacional, DTUltimaExecucao, IDEmpresa, IDSetor, VersaoClient, HashMaquina)");
            sb.AppendLine("VALUES");
            sb.AppendLine("('" + Chave + "', '" + SerialHD + "', '" + SerialProcessador + "', '" + MACRede + "', '" + MemoriaTotal + "', '" + CapacidadeHD + "', '" + EspacoLivreHD + "','" + Processador + "' ,'" + ArquiteturaMaquina + "' , " +
                "'" + IPLocal + "' , '" + NomeComputador + "' , '" + SistemaOperacional + "', '" + DTultimaConexao.ToString("yyyy-MM-dd HH:mm:ss.fff") + "', " + IDEmpresa + ", " + IDSetor + ", '" + VersaoClient + "', '" + Util.TratarString(HashMaquina) + "')");
            sb.AppendLine("END");
            sb.AppendLine("ELSE");
            sb.AppendLine("BEGIN");
            sb.AppendLine("UPDATE dbo.TBClient");
            sb.AppendLine("SET DTUltimaExecucao = '" + DTultimaConexao.ToString("yyyy-MM-dd HH:mm:ss.fff") + "',");
            sb.AppendLine("SerialHD = '" + SerialHD + "',");
            sb.AppendLine("SerialProcessador = '" + SerialProcessador + "',");
            sb.AppendLine("MACRede = '" + MACRede + "',");
            sb.AppendLine("TotalMemoria = '" + MemoriaTotal + "',");
            sb.AppendLine("CapacidadeHD = '" + CapacidadeHD + "',");
            sb.AppendLine("EspacoLivreHD = '" + EspacoLivreHD + "',");
            sb.AppendLine("NomeProcessador = '" + Processador + "',");
            sb.AppendLine("Arquitetura = '" + ArquiteturaMaquina + "',");
            sb.AppendLine("IPLocal = '" + IPLocal + "',");
            sb.AppendLine("NomeComputador = '" + NomeComputador + "',");
            sb.AppendLine("SistemaOperacional = '" + SistemaOperacional + "',");
            sb.AppendLine("IDEmpresa = " + IDEmpresa + ",");
            sb.AppendLine("IDSetor = " + IDSetor + ",");
            sb.AppendLine("VersaoClient = '" + VersaoClient + "',");
            sb.AppendLine("HashMaquina = '" + Util.TratarString(HashMaquina) + "'");
            sb.AppendLine("WHERE Chave = '" + Chave + "'");
            sb.AppendLine("END");
            try
            {
                Util.ExecuteNonQuery(sb.ToString());
            }
            catch { }
        }

    }
}
