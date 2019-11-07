using MetodosPontoFrequencia.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MetodosPontoFrequencia.PontoEletronico
{
    public class PontoEletronicoSrv
    {
        public string getQuery(string id = null)
        {
            StringBuilder query = new StringBuilder();
            query.Append(" select p.PontoEletronicoID, p.PontoEletronico_Local, p.PontoEletronico_Nome,PontoEletronico_Porta,");
            query.Append(" p.PontoEletronico_Ip, p.PontoEletronico_Usuario, p.PontoEletronico_Senha");
            query.Append(" from dbo.TBPontoEletronico p");
            query.Append(" Where 1 = 1");

            if (!string.IsNullOrEmpty(id))
                query.AppendFormat(" And p.PontoEletronicoID = {0}", id);

            query.Append(" order by p.PontoEletronicoID");
            return query.ToString();
        }

        public DataTable GetListaPontoEletronico()
        {
            DataTable dt = new DataTable();
            string query = getQuery();
            using (dt = Util.getDatset(query))
                return dt;
        }

        public TBPontoEletronicoModel GetPontoEletronicoByID(int id)
        {
            TBPontoEletronicoModel item = new TBPontoEletronicoModel();
            string query = getQuery(id.ToString());

            IDataReader reader = Util.getDataReader(query);
            using (reader)
            {
                if (reader.Read())
                {
                    item.PontoEletronicoID = Convert.ToInt32(reader["PontoEletronicoID"].ToString());
                    item.PontoEletronico_Ip = reader["PontoEletronico_Ip"].ToString();
                    item.PontoEletronico_Porta = int.Parse(reader["PontoEletronico_Porta"].ToString());
                    item.PontoEletronico_Local = reader["PontoEletronico_Local"].ToString();
                    item.PontoEletronico_Nome = reader["PontoEletronico_Nome"].ToString();
                    item.PontoEletronico_Senha = reader["PontoEletronico_Senha"].ToString();
                    item.PontoEletronico_Usuario = reader["PontoEletronico_Usuario"].ToString();
                }
            }

            return item;
        }

        public DataTable GetSetor(string empresaId = null)
        {
            DataTable dt = new DataTable();
            StringBuilder query = new StringBuilder();

            query.Append(" select IDSetor, DSSetor from tbsetor where 1 = 1");

            if (!string.IsNullOrEmpty(empresaId))
                query.AppendFormat(" And idempresa = {0}", empresaId);

            using (dt = Util.getDatset(query.ToString()))
                return dt;


        }

        public DataTable GetPontoEletronicoSetor(string IdPonto)
        {
            DataTable dt = new DataTable();
            StringBuilder query = new StringBuilder();
            query.Append(" select v.PontoEletronicoID, v.PontoEletronicoSetorID, v.IDSetor, s.DSSetor");
            query.Append(" from TBPontoEletronicoSetor V");
            query.Append(" left join TBSetor s on s.IDSetor = v.IDSetor");
            query.Append(" where 1 = 1");
            if (!string.IsNullOrEmpty(IdPonto))
                query.AppendFormat(" and v.PontoEletronicoID = {0}", IdPonto);

            using (dt = Util.getDatset(query.ToString()))
                return dt;
        }



        public bool ValidarSetorVinculado(int setor)
        {
            StringBuilder query = new StringBuilder();
            query.Append(" select top 1 IDSetor from TBPontoEletronicoSetor");
            query.AppendFormat(" where IDSetor = {0}", setor);

            IDataReader reader = Util.getDataReader(query.ToString());
            using (reader)
            {
                if (reader.Read())
                    return false;
                else
                    return true;
            }
        }

        public void ExcluirVinculoSetor(int id)
        {
            try
            {
                StringBuilder query = new StringBuilder();
                query.Append(" Delete from dbo.TBPontoEletronicoSetor");
                query.AppendFormat(" where PontoEletronicoSetorID = {0}", id);
                Util.ExecuteNonQuery(query.ToString());
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Salvar(TBPontoEletronicoModel dados, int acao)
        {
            StringBuilder query;
            try
            {
                if (acao == 1)//inserir
                {
                    query = new StringBuilder();
                    query.Append(" insert into TBPontoEletronico ( ");
                    query.Append(" PontoEletronico_Nome,");
                    query.Append(" PontoEletronico_Local,");
                    query.Append(" PontoEletronico_Ip,");
                    query.Append(" PontoEletronico_Porta,");
                    query.Append(" PontoEletronico_Usuario,");
                    query.Append(" PontoEletronico_Senha) ");
                    query.AppendFormat(" values ( '{0}',", dados.PontoEletronico_Nome);
                    query.AppendFormat("          '{0}',", dados.PontoEletronico_Local);
                    query.AppendFormat("          '{0}',", dados.PontoEletronico_Ip);
                    query.AppendFormat("           {0},", dados.PontoEletronico_Porta);
                    query.AppendFormat("          '{0}',", dados.PontoEletronico_Usuario);
                    query.AppendFormat("          '{0}');", dados.PontoEletronico_Senha);
                    Util.ExecuteNonQuery(query.ToString());

                }
                else //alterar
                {
                    query = new StringBuilder();
                    query.Append(" update TBPontoEletronico set");
                    query.AppendFormat(" PontoEletronico_Nome = '{0}',", dados.PontoEletronico_Nome);
                    query.AppendFormat(" PontoEletronico_Local = '{0}',", dados.PontoEletronico_Local);
                    query.AppendFormat(" PontoEletronico_Ip = '{0}',", dados.PontoEletronico_Ip);
                    query.AppendFormat(" PontoEletronico_Porta = {0},", dados.PontoEletronico_Porta);
                    query.AppendFormat(" PontoEletronico_Usuario = '{0}',", dados.PontoEletronico_Usuario);
                    query.AppendFormat(" PontoEletronico_Senha = '{0}'", dados.PontoEletronico_Senha);
                    query.AppendFormat(" Where PontoEletronicoID = {0}", dados.PontoEletronicoID);
                    Util.ExecuteNonQuery(query.ToString());
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public void SalvarVinculoSetor(TBPontoEletronicoSetorModel dados)
        {
            StringBuilder query = new StringBuilder();
            try
            {
                query.Append(" Insert into TBPontoEletronicoSetor (");
                query.Append(" PontoEletronicoID,");
                query.Append(" IDSetor)");
                query.AppendFormat(" Values ({0},", dados.PontoEletronicoID);
                query.AppendFormat(" {0})", dados.IDSetor);

                Util.ExecuteNonQuery(query.ToString());
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public List<TBUsuarioInfo> getUsuarios(int pontoId, string empresaId)
        {
            List<TBUsuarioInfo> lista = new List<TBUsuarioInfo>();
            StringBuilder query = new StringBuilder();
            query.Append(" select distinct v.IDUsuario, u.DSUsuario, u.PIS,u.PIS_GERADO, u.Matricula, v.IDStatus,v.IDSetor");
            query.Append(" from TBVinculoUsuario v");
            query.Append(" left join TBUsuario u on u.IDUsuario = v.IDUsuario");
            query.Append(" where v.IDSetor in (select s.IDSetor from TBPontoEletronicoSetor s");
            query.Append(" left join TBPontoEletronico p on p.PontoEletronicoID = s.PontoEletronicoID");
            query.AppendFormat(" where p.PontoEletronicoID = {0})", pontoId);
            query.AppendFormat(" and v.IDEmpresa = {0}", empresaId);
            query.Append(" and v.IDStatus = 1");

            IDataReader reader = Util.getDataReader(query.ToString());
            using (reader)
            {
                while (reader.Read())
                {
                    TBUsuarioInfo item = new TBUsuarioInfo();
                    item.DSUsuario = (reader["DSUsuario"].ToString());
                    item.IDUsuario = int.Parse(reader["IDUsuario"].ToString());
                    item.PIS = (reader["PIS"].ToString());
                    item.PIS_GERADO = (reader["PIS_GERADO"].ToString());
                    item.Matricula = (reader["Matricula"].ToString());
                    item.IDStatus = int.Parse(reader["IDStatus"].ToString());
                    item.IDSetor = int.Parse(reader["IDSetor"].ToString());
                    lista.Add(item);
                }

            }
            return lista;
        }
        public List<TBUsuarioInfo> GetUsuariosImportacao(int pontoId, string empresaId)
        {
            List<TBUsuarioInfo> lista = getUsuarios(pontoId, empresaId);
            var listaPisGerado = lista.Where(t => t.PIS_GERADO == string.Empty).ToList();
            foreach (var item in listaPisGerado)
            {
                PreenchePisGerado(item);
            }
            if (listaPisGerado.Count() > 0)
            {
                lista = new List<TBUsuarioInfo>();
                return lista = getUsuarios(pontoId, empresaId);
            }
            else
                return lista;
        }

        public bool VerificaPisGeradoExiste(string pis)
        {
            StringBuilder query = new StringBuilder();
            query.Append(" select PIS_GERADO from TBUsuario ");
            query.AppendFormat(" Where PIS_GERADO = '{0}'", pis.Trim());
            IDataReader reader = Util.getDataReader(query.ToString());
            using (reader)
            {
                if (reader.Read())
                    return true;
                else
                    return false;
            }
        }

        public string GeradorAleatorioPIS()
        {
            Random randon = new Random();
            int num = 0;
            string numero = "";
            for (int y = 0; y < 10; y++)
            {
                num = randon.Next(9);
                numero += num.ToString();
            }
            var t = numero.Substring(9, 1);
            int a = Convert.ToInt32(numero.Substring(0, 1));
            int b = Convert.ToInt32(numero.Substring(1, 1));
            int c = Convert.ToInt32(numero.Substring(2, 1));
            int d = Convert.ToInt32(numero.Substring(3, 1));
            int e = Convert.ToInt32(numero.Substring(4, 1));
            int f = Convert.ToInt32(numero.Substring(5, 1));
            int g = Convert.ToInt32(numero.Substring(6, 1));
            int h = Convert.ToInt32(numero.Substring(7, 1));
            int i = Convert.ToInt32(numero.Substring(8, 1));
            int j = Convert.ToInt32(numero.Substring(9, 1));


            int soma = (3 * a) + (2 * b) + (9 * c) + (8 * d) + (7 * e) + (6 * f) + (5 * g) + (4 * h) + (3 * i) + (2 * j);
            int resto = soma % 11;
            int digitoVerificador = 11 - resto;
            if (digitoVerificador == 10 || digitoVerificador == 11)
                digitoVerificador = 0;
            numero += digitoVerificador.ToString();

            return numero;
        }

        public void PreenchePisGerado(TBUsuarioInfo usuario)
        {
            try
            {
                var pis = GeradorAleatorioPIS();
                if (!VerificaPisGeradoExiste(pis))
                {
                    string query = string.Format(" update TBUsuario set PIS_GERADO = '{0}' where IDUsuario = {1}", pis.Trim(), usuario.IDUsuario);
                    Util.ExecuteNonQuery(query);
                }
                else
                    PreenchePisGerado(usuario);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
