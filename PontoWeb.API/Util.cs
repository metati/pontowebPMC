using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace PontoWeb.API
{
    class Util
    {
        public static string StringConexao
        {
            get
            {
                //return "Data Source=ribeiraodaponte.cuiaba.mt.gov.br;Initial Catalog=PontoFrequenciaPMC;Persist Security Info=True;User ID=pontoweb;Password=Pt@20wEb18;Min Pool Size=10;Max Pool Size=200;Connect Timeout=90;Packet Size=16000";
                //return "Data Source=200.241.193.131\\SQLMETATI16,1477;Initial Catalog=PontoFrequenciaPMC;Persist Security Info=True;User ID=sa;password=MetaTIPMC2016;Pooling=true;Enlist=false;Min Pool Size=5;";
                return "Data Source=resmarchetti.cuiaba.mt.gov.br;Initial Catalog=PontoFrequenciaPMC;Persist Security Info=True;User ID=pontoweb;Password=Pt@20wEb18@ZUR;Min Pool Size=10;Max Pool Size=200;Connect Timeout=90;Packet Size=16000";

            }
        }

        /// <summary>
        /// Função que permite executar comandos no banco de dados e retorna um valor SqlDataReader.
        /// </summary>
        /// <param name="comando">Instrução SQL.</param>
        /// <returns>Retorna um SqlDataReader</returns>
        public static SqlDataReader getDataReader(string comando)
        {
            SqlConnection connect = new SqlConnection(StringConexao);


            SqlDataReader dataReader;
            connect.Open();

            SqlCommand command = new SqlCommand(comando, connect);
            command.CommandTimeout = 5000;
            dataReader = command.ExecuteReader(CommandBehavior.CloseConnection);

            return dataReader;
        }

        /// <summary>
        /// Função que permite executar comandos no banco de dados e retorna um valor SCALAR.
        /// </summary>
        /// <param name="comando">Instrução SQL.</param>
        /// <returns>Retorna uma string</returns>
        public static string getScalar(string SQLComando)
        {
            string strRetorno = "";
            SqlConnection conect = new SqlConnection(StringConexao);
            SqlCommand command;
            conect.Open();
            try
            {
                command = new SqlCommand(SQLComando, conect);
                object obj = command.ExecuteScalar();
                if (obj != null)
                {
                    strRetorno = obj.ToString();
                }
            }
            finally
            {
                conect.Close();
                conect.Dispose();
            }
            command.Dispose();

            return strRetorno;
        }

        /// <summary>
        /// Função que permite executar comandos no banco de dados e retorna um valor SCALAR.
        /// </summary>
        /// <param name="comando">Instrução SQL.</param>
        public static void ExecuteNonQuery(string SQLComando)
        {
            SqlConnection conect = new SqlConnection(StringConexao);
            conect.Open();
            SqlCommand command = new SqlCommand(SQLComando, conect);
            try
            {
                command.CommandTimeout = 90000;
                command.ExecuteNonQuery();
            }
            catch (SqlException sqlExp)
            {
                if (sqlExp.Number == 547)
                {
                    throw new Exception(getScalar(" exec dbo.spErroConstraint '" + TratarString(sqlExp.Message).Replace('\n', ' ') + "' "));
                }
                else
                {
                    throw sqlExp;
                }
            }
            finally
            {
                conect.Close();
                command.Dispose();
                conect.Dispose();
            }
        }


        public static DataTable getDatset(string comando)
        {
            SqlConnection conect = new SqlConnection(StringConexao);
            conect.Open();

            SqlDataAdapter da = new SqlDataAdapter(comando, conect);
            DataSet ds = new DataSet();
            try
            {
                da.Fill(ds, "DataTable");
            }
            finally
            {
                da.Dispose();
                conect.Close();
            }
            return ds.Tables[0];
        }

        /// <summary>
        /// Função para tratar string sem aspas e %
        /// </summary>
        /// <param name="Valor"></param>
        /// <returns>strRetorno</returns>
        public static string TratarString(string Valor)
        {
            string strRetorno;
            if (string.IsNullOrEmpty(Valor))
            {
                strRetorno = "";
            }
            else
            {
                strRetorno = Valor.Replace("'", "");
                strRetorno = strRetorno.Replace("%", "");
            }

            return strRetorno;
        }


        /// <summary>
        /// Função para tratar string sem aspas e %
        /// </summary>
        /// <param name="Valor"></param>
        /// <returns>A função retorna o valor com aspas simples, caso estiver vazio returno NULL</returns>
        public static string TratarStringAspas(string Valor)
        {
            string strRetorno;
            if (string.IsNullOrEmpty(Valor))
            {
                strRetorno = "NULL";
            }
            else
            {
                strRetorno = Valor.Replace("'", "");
                strRetorno = strRetorno.Replace("%", "");
                strRetorno = "'" + strRetorno + "'";
            }

            return strRetorno;
        }


        /// <summary>
        /// Antes de gravar no banco ele coloca no formato correto para o SQL.
        /// PADRÃO DE RECEBIMENTO 2018-06-05T14:56:57.
        /// </summary>
        public static string TratarDataINSERT(string Data)
        {
            string strRetorno;
            //strRetorno = Data.ToString("yyyy-MM-dd HH:mm:ss.fff");
            try
            {
                var dateTime = DateTime.Parse(Data);
                strRetorno = dateTime.ToString("dd/MM/yyyy HH:mm:ss.fff");
                if (strRetorno.Contains("0001"))
                {
                    strRetorno = "NULL";
                }
                else
                {
                    strRetorno = "CONVERT(DATETIME, '" + strRetorno + "',103) ";
                }
            }
            catch
            {
                strRetorno = "NULL";
            }

            return strRetorno;
        }


        /// <summary>
        /// Antes de gravar no banco ela coloca em um block de TRANSECTION.
        /// </summary>
        public static string GetSqlBeginTry(string texto)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("BEGIN TRANSACTION Tran1 BEGIN TRY");
            sb.AppendLine(texto);
            sb.AppendLine("COMMIT TRANSACTION Tran1");
            sb.AppendLine("END TRY");
            sb.AppendLine(" BEGIN CATCH");
            sb.AppendLine("ROLLBACK TRANSACTION Tran1");
            sb.AppendLine("RAISERROR ('', 16, 1 );");
            sb.AppendLine("END CATCH  ");
            return sb.ToString();
        }


        public static string TratarNum(string Valor)
        {
            string strRetorno = Valor;
            if (string.IsNullOrEmpty(Valor))
            {
                strRetorno = "0";
            }
            else if (Valor.IndexOf(',') > -1)
            {
                strRetorno = Valor;
            }
            return strRetorno;
        }

        public static string GetDiaSemana(string num)
        {
            string dia = "";
            switch (num)
            {
                case "1":
                    dia = "Dom";
                    break;
                case "2":
                    dia = "Seg";
                    break;
                case "3":
                    dia = "Ter";
                    break;
                case "4":
                    dia = "Qua";
                    break;
                case "5":
                    dia = "Qui";
                    break;
                case "6":
                    dia = "Sex";
                    break;
                case "7":
                    dia = "Sáb";
                    break;

            }
            return dia;
        }
    }
}