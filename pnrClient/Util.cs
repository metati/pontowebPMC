using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace pnrClient
{
    class Util
    {
        private static string connString = pnrClient.Properties.Settings.Default.ptServer35ConnectionString;

        public static string GetHash(string Url)
        {
            crudPtServer crud = new crudPtServer(Url);
            string hash = ConfigurationManager.AppSettings["hash"].ToString();
            if (string.IsNullOrEmpty(hash))
            {
                try
                {
                    hash = crud.GetTBBancoLocalHash();
                }
                catch { }
            }
            crud.AtualizaScriptBanco();
            crud.LimpaTabelaHash();
            return hash;
        }

        public static string CriarHash(string hash)
        {
            crudPtServer crud = new crudPtServer("");
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (string.IsNullOrEmpty(hash))
            {
                hash = Guid.NewGuid().ToString();
                crud.AtualizaScriptBanco();
                hash = crud.GetTBLocalHash(hash);
            }
            config.AppSettings.Settings["hash"].Value = hash;
            config.Save(ConfigurationSaveMode.Modified);

            ConfigurationManager.RefreshSection("appSettings");
            return config.AppSettings.Settings["hash"].ToString();
        }


        private static string StringConexao
        {
            get
            {
                return @"Data Source=|DataDirectory|ptServer35.sdf;Password=dbpontoSADCTI;Persist Security Info=false";
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
                strRetorno = strRetorno.TrimEnd();
                strRetorno = strRetorno.TrimStart();
            }

            return strRetorno;
        }

        public static void getDataSet()
        {
            DataSet ds = new System.Data.DataSet();
            SqlCeConnection con = new SqlCeConnection(StringConexao);
            SqlCeDataAdapter da = new SqlCeDataAdapter("SELECT * FROM TBUsuarioLocal", con);
            da.Fill(ds);
            con.Close();
            da.Dispose();
        }


        public static WindowsVersion GetWindowsVersion()
        {

            Version winVersion = Environment.OSVersion.Version;
            switch (winVersion.Major)
            {
                case 1:
                    return WindowsVersion.Windows_1_01;
                case 2:
                    switch (winVersion.Minor)
                    {
                        case 3:
                            return WindowsVersion.Windows_2_03;
                        case 10:
                            return WindowsVersion.Windows_2_10;
                        case 11:
                            return WindowsVersion.Windows_2_11;
                        default:
                            return WindowsVersion.None;
                    }
                case 3:
                    switch (winVersion.Minor)
                    {
                        case 0:
                            return WindowsVersion.Windows_3_0;
                        case 1:
                            return WindowsVersion.Windows_for_Workgroups_3_1;
                        case 2:
                            return WindowsVersion.Windows_3_2;
                        case 5:
                            return WindowsVersion.Windows_NT_3_5;
                        case 11:
                            return WindowsVersion.Windows_for_Workgroups_3_11;
                        case 51:
                            return WindowsVersion.Windows_NT_3_51;
                        default:
                            return WindowsVersion.None;
                    }
                case 4:
                    switch (winVersion.Minor)
                    {
                        case 0:
                            switch (winVersion.Build)
                            {
                                case 950:
                                    return WindowsVersion.Windows_95;
                                case 1381:
                                    return WindowsVersion.Windows_NT_4_0;
                                default:
                                    return WindowsVersion.None;
                            }
                        case 10:
                            switch (winVersion.Build)
                            {
                                case 1998:
                                    return WindowsVersion.Windows_98;
                                case 2222:
                                    return WindowsVersion.Windows_98_SE;
                                default:
                                    return WindowsVersion.None;
                            }
                        case 90:
                            return WindowsVersion.Windows_Me;
                        default:
                            return WindowsVersion.None;
                    }
                case 5:
                    switch (winVersion.Minor)
                    {
                        case 0:
                            return WindowsVersion.Windows_2000;
                        case 1:
                            return WindowsVersion.Windows_XP;
                        case 2:
                            return WindowsVersion.Windows_Server_2003;
                        default:
                            return WindowsVersion.None;
                    }
                case 6:
                    switch (winVersion.Minor)
                    {
                        case 0:
                            return WindowsVersion.Windows_Vista;
                        case 1:
                            return WindowsVersion.Windows_7;
                        case 2:
                            return WindowsVersion.Windows_8;
                        case 6:
                            return WindowsVersion.Windows_10;
                        default:
                            return WindowsVersion.None;
                    }
                default:
                    return WindowsVersion.None;
            }
        }

        public enum WindowsVersion
        {
            None = 0,
            Windows_1_01,
            Windows_2_03,
            Windows_2_10,
            Windows_2_11,
            Windows_3_0,
            Windows_for_Workgroups_3_1,
            Windows_for_Workgroups_3_11,
            Windows_3_2,
            Windows_NT_3_5,
            Windows_NT_3_51,
            Windows_95,
            Windows_NT_4_0,
            Windows_98,
            Windows_98_SE,
            Windows_2000,
            Windows_Me,
            Windows_XP,
            Windows_Server_2003,
            Windows_Vista,
            Windows_Home_Server,
            Windows_7,
            Windows_8,
            Windows_10,
        }
    }



    class UtilLocalDB
    {

        private SqlCeConnection objSqlCeConnection = null;
        private UtilLocalDB objSqlServerCeDAL = null;
        private string connString = pnrClient.Properties.Settings.Default.ptServer35ConnectionString;

        public UtilLocalDB()
        {
            connString = pnrClient.Properties.Settings.Default.ptServer35ConnectionString;
            objSqlCeConnection = new SqlCeConnection(connString);
        }

        public UtilLocalDB GetInstance(string connString)
        {
            if (objSqlServerCeDAL == null)
            {
                objSqlServerCeDAL = new UtilLocalDB();
            }
            return objSqlServerCeDAL;
        }

        public void Open()
        {
            try
            {
                if (string.IsNullOrEmpty(objSqlCeConnection.ConnectionString))
                    objSqlCeConnection.ConnectionString = connString;
                if (objSqlCeConnection.State == ConnectionState.Closed)
                {
                    objSqlCeConnection.Open();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Dispose()
        {
            try
            {
                if (objSqlCeConnection.State != ConnectionState.Closed)
                {
                    objSqlCeConnection.Close();
                    objSqlCeConnection.Dispose();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public DataTable Load(string sql)
        {
            UtilLocalDB objSqlCeServerDAL = GetInstance(connString);
            objSqlCeServerDAL.Open();
            SqlCeDataAdapter dAd = new SqlCeDataAdapter(sql, objSqlCeServerDAL.objSqlCeConnection);
            dAd.SelectCommand.CommandType = CommandType.Text;
            DataSet dSet = new DataSet();
            try
            {
                dAd.Fill(dSet, "Tabela");
                return dSet.Tables["Tabela"];
            }
            catch
            {
                throw;
            }
            finally
            {
                dSet.Dispose();
                dAd.Dispose();
                objSqlCeServerDAL.Dispose();
            }
        }

        public int Insert(string sql)
        {
            UtilLocalDB objSqlCeServerDAL = GetInstance(connString);
            objSqlCeServerDAL.Open();
            SqlCeCommand dCmd = new SqlCeCommand(sql, objSqlCeServerDAL.objSqlCeConnection);
            dCmd.CommandType = CommandType.Text;
            try
            {
                return dCmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                dCmd.Dispose();
                objSqlCeServerDAL.Dispose();
            }
        }
    }


    class UtilConexao
    {
        Stopwatch swIntervalo = new Stopwatch();
        public Stopwatch swServidor { get; set; }
        public pontonarede.TesteConexaoModel tcm { get; }
        private string UrlWSInterno = "http://jdubirajara.cuiaba.mt.gov.br/Service.asmx";
        private string UrlWSExterno = "http://ws.pontoweb.cuiaba.mt.gov.br/Service.asmx";
        public bool ConexaoOK { get; set; }
        public int QtdeMinutos { get; set; }

        [DllImport("wininet.dll")]
        private static extern bool InternetGetConnectedState(out int Description, int ReservedValue);

        public UtilConexao()
        {
            tcm = new pontonarede.TesteConexaoModel();
            swServidor = new Stopwatch();
            swServidor.Start();
        }

        public void AtualizaStatusThread(bool DeviceConnect)
        {
            var thread = new Thread(() =>
            {
                //Código que será executado em paralelo ao resto do código
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                int desc;
                bool hasConnection;

                //VERIFICA SE TEM CONEXÃO
                hasConnection = InternetGetConnectedState(out desc, 0);

                if (hasConnection)
                {
                    tcm.ConexaoRedeLocal = 1;
                    tcm.ConexaoRedeLocalMensagem = "OK";
                    VerificaInternet();
                    VerificaWSExterno();
                    //VerificaInternet();
                    //VerificaWSExterno();
                }
                else
                {
                    tcm.ConexaoRedeLocal = 0;
                    tcm.ConexaoRedeLocalMensagem = "Conexão de rede não identificada";
                    tcm.ConexaoInternet = 0;
                    tcm.ConexaoInternetMensagem = "NÃO FOI POSSÍVEL CONECTAR NO HOST";
                    tcm.ConexaoWSExterno = 0;
                    tcm.ConexaoWSExternoMensagem = "NÃO FOI POSSÍVEL CONECTAR NO HOST";
                }



                VerificaWSInterno();
                VerificaConexaoDB();


                //VERIFICA SE O LEITOR ESTÁ CONECTADO
                if (DeviceConnect)
                {
                    tcm.ConexaoLeitor = 1;
                    tcm.ConexaoLeitorMensagem = "OK";
                }
                else
                {
                    tcm.ConexaoLeitor = 0;
                    tcm.ConexaoLeitorMensagem = "LEITOR NÃO CONECTADO";
                }

                if (tcm.ConexaoWSInterno == 1)
                {
                    tcm.UrlWS = UrlWSInterno;
                }
                else if (tcm.ConexaoWSExterno == 1)
                {
                    tcm.UrlWS = UrlWSExterno;
                }
                else
                {
                    tcm.UrlWS = UrlWSInterno;
                }
            });
            //Inicia a execução da thread (em paralelo a esse código)
            thread.Start();
        }

        public void AtualizaStatus(bool DeviceConnect)
        {
            //Código que será executado em paralelo ao resto do código
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            int desc;
            bool hasConnection;

            //VERIFICA SE TEM CONEXÃO
            hasConnection = InternetGetConnectedState(out desc, 0);

            if (hasConnection)
            {
                tcm.ConexaoRedeLocal = 1;
                tcm.ConexaoRedeLocalMensagem = "OK";
                VerificaInternet();
                VerificaWSExterno();
                //VerificaInternet();
                //VerificaWSExterno();
            }
            else
            {
                tcm.ConexaoRedeLocal = 0;
                tcm.ConexaoRedeLocalMensagem = "Conexão de rede não identificada";

                tcm.ConexaoInternet = 0;
                tcm.ConexaoInternetMensagem = "NÃO FOI POSSÍVEL CONECTAR NO HOST";
                tcm.ConexaoWSExterno = 0;
                tcm.ConexaoWSExternoMensagem = "NÃO FOI POSSÍVEL CONECTAR NO HOST";
            }

            VerificaWSInterno();
            VerificaConexaoDB();


            //VERIFICA SE O LEITOR ESTÁ CONECTADO
            if (DeviceConnect)
            {
                tcm.ConexaoLeitor = 1;
                tcm.ConexaoLeitorMensagem = "OK";
            }
            else
            {
                tcm.ConexaoLeitor = 0;
                tcm.ConexaoLeitorMensagem = "LEITOR NÃO CONECTADO";
            }

            if (tcm.ConexaoWSInterno == 1)
            {
                tcm.UrlWS = UrlWSInterno;
            }
            else if (tcm.ConexaoWSExterno == 1)
            {
                tcm.UrlWS = UrlWSExterno;
            }
            else
            {
                tcm.UrlWS = UrlWSInterno;
            }
        }


        private void VerificaInternet()
        {
            string URI = "https://www.google.com.br/";
            try
            {
                StreamReader reader;
                WebClient webClient = new WebClient();
                webClient.Timeout = 3000;
                Stream stream = webClient.OpenRead(URI);
                reader = new StreamReader(stream);
                String request = reader.ReadToEnd();
                tcm.ConexaoInternet = 1;
                tcm.ConexaoInternetMensagem = "OK";
            }
            catch (WebException ex)
            {
                tcm.ConexaoInternet = 0;
                if (ex.Response is HttpWebResponse)
                {
                    tcm.ConexaoInternetMensagem = ex.Response.ToString();
                    switch (((HttpWebResponse)ex.Response).StatusCode)
                    {
                        case HttpStatusCode.NotFound:
                            break;
                        default:
                            throw ex;
                    }
                }
                else
                {
                    tcm.ConexaoInternetMensagem = "NÃO FOI POSSÍVEL CONECTAR NO HOST";
                    //NÃO FOI ENCONTRADO O HOST
                }
            }
        }

        private void VerificaWSInterno()
        {
            swIntervalo.Reset();
            swIntervalo.Start();
            try
            {
                StreamReader reader;
                WebClient webClient = new WebClient();
                webClient.Timeout = 3000;
                Stream stream = webClient.OpenRead(UrlWSInterno);
                reader = new StreamReader(stream);
                String request = reader.ReadToEnd();
                tcm.ConexaoWSInterno = 1;
                tcm.ConexaoWSInternoMensagem = "OK";
            }
            catch (WebException ex)
            {
                tcm.ConexaoWSInterno = 0;
                if (ex.Response is HttpWebResponse)
                {
                    tcm.ConexaoWSInternoMensagem = ex.Message + " - " + ((HttpWebResponse)ex.Response).StatusDescription;
                }
                else
                {
                    //NÃO FOI ENCONTRADO O HOST
                    tcm.ConexaoWSInternoMensagem = "NÃO FOI POSSÍVEL CONECTAR NO WS";
                }
            }
            swIntervalo.Stop();
            tcm.ConexaoWSInternoTempo = swIntervalo.Elapsed.ToString();
        }

        private void VerificaWSExterno()
        {
            swIntervalo.Reset();
            swIntervalo.Start();
            try
            {
                StreamReader reader;
                WebClient webClient = new WebClient();
                webClient.Timeout = 3000;
                Stream stream = webClient.OpenRead(UrlWSExterno);
                reader = new StreamReader(stream);
                String request = reader.ReadToEnd();
                tcm.ConexaoWSExterno = 1;
                tcm.ConexaoWSExternoMensagem = "OK";
            }
            catch (WebException ex)
            {
                tcm.ConexaoWSExterno = 0;
                if (ex.Response is HttpWebResponse)
                {
                    tcm.ConexaoWSExternoMensagem = ex.Message + " - " + ((HttpWebResponse)ex.Response).StatusDescription;
                }
                else
                {
                    //NÃO FOI ENCONTRADO O HOST
                    tcm.ConexaoWSExternoMensagem = "NÃO FOI POSSÍVEL CONECTAR NO WS";
                }
            }
            swIntervalo.Stop();
            tcm.ConexaoWSExternoTempo = swIntervalo.Elapsed.ToString();
        }

        private void VerificaConexaoDB()
        {
            try
            {
                UtilLocalDB localDB = new UtilLocalDB();
                tcm.ConexaoBancoLocal = 1;
                tcm.ConexaoBancoLocalMensagem = "OK";
            }
            catch (Exception e)
            {
                tcm.ConexaoBancoLocal = 0;
                tcm.ConexaoBancoLocalMensagem = "Conexão com o banco local não existente";
            }
        }

        private static bool VerificaRedeTeste()
        {
            int desc;
            bool hasConnection;
            //PRIMEIRA VERIFICAÇÃO COM A DLL wininet.dll
            hasConnection = InternetGetConnectedState(out desc, 0);


            //SEGUNDA VERIFICAÇÃO, EXECUTA PING
            hasConnection = (new Ping().Send("ws.pontoweb.cuiaba.mt.gov.br").Status == IPStatus.Success) ? true : false;

            //Checa adaptadores de rede
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                //Busca todos os adaptadores de rede
                foreach (NetworkInterface network in NetworkInterface.GetAllNetworkInterfaces())
                {
                    if (network.OperationalStatus == OperationalStatus.Up && network.NetworkInterfaceType != NetworkInterfaceType.Tunnel && network.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                    {
                        hasConnection = true;
                    }
                    else
                    {
                        hasConnection = false;
                    }
                }
            }
            return hasConnection;
        }

        private static void GetPing()
        {
            string txtIpNomePing = "";
            try
            {
                // constrói um objeto da classe 
                Ping ping = new Ping();

                // cria um objeto da classe PingReply e atribui a ela
                // o resultado de uma chamada ao método Send da classe Ping 
                string pings = "";

                for (int i = 0; i < 4; i++)
                {
                    string host = "ws.pontoweb.cuiaba.mt.gov.br";

                    //Veja que o timeout da operação é de 3 segundos
                    PingReply resp = ping.Send(host, 3000);

                    pings = pings + ("\nResultado de ping para: " + resp.Address + " - " +
                    "Tempo de resposta: " + resp.RoundtripTime + "ms - " +
                    "Status: " + resp.Status);
                }
            }
            catch (Exception ex)
            {

            }
        }

        public string GetSqlInsert()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("INSERT INTO Teste");
            sb.AppendLine("(RedeLocal, RedeLocalMensagem, Internet, InternetMensagem, WSInterno, WSInternoMensagem, WSInternoTempo, WSExterno, WSExternoMensagem, WSExternoTempo, Leitor, LeitorMensagem, BancoLocal, BancoLocalMensagem, UrlWS)");
            sb.AppendLine("VALUES");
            sb.AppendLine("(" + tcm.ConexaoRedeLocal + ", '" + tcm.ConexaoRedeLocalMensagem + "', " + tcm.ConexaoInternet + ",'" + tcm.ConexaoInternetMensagem + "' " +
                ", " + tcm.ConexaoWSInterno + ", '" + tcm.ConexaoWSInternoMensagem + "', '" + tcm.ConexaoWSInternoTempo + "', " + tcm.ConexaoWSExterno + ", '" + tcm.ConexaoWSExternoMensagem + "','" + tcm.ConexaoWSExternoTempo + "' ," + tcm.ConexaoLeitor + ",'" + tcm.ConexaoLeitorMensagem + "'," +
                " " + tcm.ConexaoBancoLocal + ", '" + tcm.ConexaoBancoLocalMensagem + "', '" + tcm.UrlWS + "')");
            return sb.ToString();
        }


        public void IniciaCronometro()
        {
            swIntervalo.Reset();
            swIntervalo.Start();
        }

        public void FinalizaCronometro()
        {
            Console.WriteLine(swIntervalo.Elapsed.ToString());
            swIntervalo.Stop();
        }

        private void OnKillProcessComplete(IAsyncResult result)
        {
            Console.WriteLine("Process killed ...");
            //Console.WriteLine(utilConexao.GetSqlInsert());
        }

    }

    class WebClient : System.Net.WebClient
    {
        public int Timeout { get; set; }

        protected override WebRequest GetWebRequest(Uri uri)
        {
            WebRequest lWebRequest = base.GetWebRequest(uri);
            lWebRequest.Timeout = Timeout;
            ((HttpWebRequest)lWebRequest).ReadWriteTimeout = Timeout;
            return lWebRequest;
        }

        private string GetRequest(string aURL)
        {
            using (var lWebClient = new WebClient())
            {
                lWebClient.Timeout = 600 * 60 * 1000;
                return lWebClient.DownloadString(aURL);
            }
        }
    }


    public class OSVersionInfo
    {
        public string Name { get; set; }

        public string FullName
        {
            get
            {
                return "" + Name + " " + "[Version " + Major + "." + Minor + "." + Build + "]";
            }
        }

        public int Minor { get; set; }

        public int Major { get; set; }

        public int Build { get; set; }

        private OSVersionInfo() { }

        /// <summary> 
        /// Init OSVersionInfo object by current windows environment 
        /// </summary> 
        /// <returns></returns> 
        public static OSVersionInfo GetOSVersionInfo()
        {
            System.OperatingSystem osVersionObj = System.Environment.OSVersion;

            OSVersionInfo osVersionInfo = new OSVersionInfo()
            {
                Name = GetOSName(osVersionObj),
                Major = osVersionObj.Version.Major,
                Minor = osVersionObj.Version.Minor,
                Build = osVersionObj.Version.Build
            };

            return osVersionInfo;
        }

        /// <summary> 
        /// Get current windows name 
        /// </summary> 
        /// <param name="osInfo"></param> 
        /// <returns></returns> 
        static string GetOSName(System.OperatingSystem osInfo)
        {
            string osName = "unknown";

            switch (osInfo.Platform)
            {
                //for old windows kernel 
                case System.PlatformID.Win32Windows:
                    osName = ForWin32Windows(osInfo);
                    break;
                //fow NT kernel 
                case System.PlatformID.Win32NT:
                    osName = ForWin32NT(osInfo);
                    break;
            }

            return osName;
        }

        /// <summary> 
        /// for old windows kernel 
        /// this function is the child function for method GetOSName 
        /// </summary> 
        /// <param name="osInfo"></param> 
        /// <returns></returns> 
        static string ForWin32Windows(System.OperatingSystem osInfo)
        {
            string osVersion = "Unknown";

            //Code to determine specific version of Windows 95,  
            //Windows 98, Windows 98 Second Edition, or Windows Me. 
            switch (osInfo.Version.Minor)
            {
                case 0:
                    osVersion = "Windows 95";
                    break;
                case 10:
                    switch (osInfo.Version.Revision.ToString())
                    {
                        case "2222A":
                            osVersion = "Windows 98 Second Edition";
                            break;
                        default:
                            osVersion = "Windows 98";
                            break;
                    }
                    break;
                case 90:
                    osVersion = "Windows Me";
                    break;
            }

            return osVersion;
        }

        /// <summary> 
        /// fow NT kernel 
        /// this function is the child function for method GetOSName 
        /// </summary> 
        /// <param name="osInfo"></param> 
        /// <returns></returns> 
        static string ForWin32NT(System.OperatingSystem osInfo)
        {
            string osVersion = "Unknown";

            //Code to determine specific version of Windows NT 3.51,  
            //Windows NT 4.0, Windows 2000, or Windows XP. 
            switch (osInfo.Version.Major)
            {
                case 3:
                    osVersion = "Windows NT 3.51";
                    break;
                case 4:
                    osVersion = "Windows NT 4.0";
                    break;
                case 5:
                    switch (osInfo.Version.Minor)
                    {
                        case 0:
                            osVersion = "Windows 2000";
                            break;
                        case 1:
                            osVersion = "Windows XP";
                            break;
                        case 2:
                            osVersion = "Windows 2003";
                            break;
                    }
                    break;
                case 6:
                    switch (osInfo.Version.Minor)
                    {
                        case 0:
                            osVersion = "Windows Vista";
                            break;
                        case 1:
                            osVersion = "Windows 7";
                            break;
                        case 2:
                            osVersion = "Windows 8";
                            break;
                        case 3:
                            osVersion = "Windows 8.1";
                            break;
                    }
                    break;
                case 10:
                    osVersion = "Windows 10";
                    break;
            }

            return osVersion;
        }
    }
}
