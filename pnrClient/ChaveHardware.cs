using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;
using System.Data;
using System.Management;

namespace pnrClient
{
    class ChaveHardware
    {
        //Para o banco de dados local
        SqlCeCommand SQLComand;
        SqlCeConnection SQLConn;

        //Objetos para a busca de dados da máquina local.
        ManagementObjectSearcher s2;
        string IDProcessador;
        string SerialHD;
        string MACRede;
        string Memoria;
        string ArquiteturaP, NomeProcessador, CapacidadeHD, EspacoLivreHD;

        //Objetos para Vefificação de registros
        SqlCeDataAdapter DA;
        DataTable TBCH;

        //retorno do prosseguimento.
        string retorno;

        public string TotalMemoria
        {
            get
            {
                return Memoria;
            }
        }

        public string ArquiteturaProcessador
        {
            get
            {
                return ArquiteturaP;
            }
        }

        public string ModeloProcessador
        {
            get
            {
                return NomeProcessador;
            }
        }

        public string ArmazenamentoHD
        {
            get
            {
                return CapacidadeHD;
            }
        }

        public string EspacoLivre
        {
            get
            {
                return EspacoLivreHD;
            }
        }

        public string MACREDE
        {
            get
            {
                return MACRede;
            }
        }

        public string SerialPROCESSADOR
        {
            get
            {
                return IDProcessador;
            }
        }

        public string SERIALHD
        {
            get
            {
                return SerialHD;
            }
        }

        public ChaveHardware(string Conexao)
        {
            SQLConn = new SqlCeConnection(Conexao);
        }

        public string VerificaRegistro()
        {
            //Variáveis.
            DA = null;
            DA = new SqlCeDataAdapter("Select * from CH_Hardware", SQLConn); //Instância DataAdapter com o cod a executar e a conexão
            TBCH = null;
            TBCH = new DataTable();
            DA.Fill(TBCH); //preenche tabela
            string Processador, HD, MAC;

            if (TBCH.Rows.Count == 0) //Se igual a zero, insere na tabela.
            {
                SQLConn.Close();
                InsereDados();
                retorno = "C";
            }
            else //Senão, faz a comparação com o arquivo local.
            {
                InformacaoHD("IDHD");
                InformacaoHD("Capacidade");
                InformacaoHD("EspacoLivre");
                InformacaoProcessador("IDProcessador");
                InformacaoProcessador("Nome");
                InformacaoProcessador("Arquitetura");
                InformacaoMemoria("Capacidade");
                InformacaoRede();

                Processador = TBCH.Rows[0]["SERIALPROCESSADOR"].ToString();
                HD = TBCH.Rows[0]["SERIALHD"].ToString();
                MAC = TBCH.Rows[0]["EnderecoMAC"].ToString();

                if ((Processador.Length <= 1) || (HD.Length <= 1) || (MAC.Length <= 1))
                {
                    ExcluirTabela();
                    CriaTabelaChaveHardware();
                    //InsereDados();
                    return "C";
                }

                //Faz a comparação.
                if ((IDProcessador.Trim() == Processador.Trim())
                    && (SerialHD.Trim() == HD.Trim())
                    && (MACRede.Trim() == MAC.Trim()))
                {
                    retorno = "C";
                }
                else if (MACRede == "")
                    retorno = "C";
                else
                    retorno = "N";
            }
            return retorno;
        }

        private void InsereDados()
        {
            InformacaoHD("IDHD");
            InformacaoHD("Capacidade");
            InformacaoHD("EspacoLivre");
            InformacaoProcessador("IDProcessador");
            InformacaoProcessador("Nome");
            InformacaoProcessador("Arquitetura");
            InformacaoMemoria("Capacidade");
            InformacaoRede();

            //Variáveis.
            string cod;
            cod = string.Format("Insert into CH_HARDWARE (SERIALPROCESSADOR,SERIALHD,ENDERECOMAC,MODELOPROCESSADOR,ARQUITETURAMAQUINA,TOTALMEMORIA,CAPACIDADEHD,ESPACOLIVREHD) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')", IDProcessador, SerialHD, MACRede, NomeProcessador, ArquiteturaP, Memoria, CapacidadeHD, EspacoLivreHD);
            UtilLocalDB localDBD = new UtilLocalDB();
            localDBD.Insert("DELETE FROM CH_HARDWARE");
            UtilLocalDB localDB = new UtilLocalDB();
            localDB.Insert(cod);
        }

        public void SetaInfoHardware()
        {
            InformacaoHD("IDHD");
            InformacaoHD("Capacidade");
            InformacaoHD("EspacoLivre");
            InformacaoProcessador("IDProcessador");
            InformacaoProcessador("Nome");
            InformacaoProcessador("Arquitetura");
            InformacaoMemoria("Capacidade");
            InformacaoRede();
        }

        public void CriaTabelaChaveHardware()
        {
            if (!TabelaExist())
            {
                string cod;
                cod = "Create table CH_HARDWARE"
                    + "(SerialProcessador nvarchar(60),"
                    + "SerialHD nvarchar(70),"
                + "EnderecoMAC nvarchar(100),"
                + "ModeloProcessador nvarchar(100),"
                + "ArquiteturaMaquina nvarchar(3),"
                + "TotalMemoria nvarchar(100),"
                + "CapacidadeHD nvarchar(100),"
                + "EspacoLivreHD nvarchar(100))";

                SQLComand = new SqlCeCommand(cod, SQLConn);

                try
                {
                    SQLConn.Open();
                    SQLComand.ExecuteNonQuery();
                    SQLConn.Close();

                    //Já lança os registros na tabela.
                    InsereDados();
                }
                catch (Exception ex)
                {
                    ex.ToString();
                    SQLConn.Close();
                }
            }
        }

        private bool TabelaExist()
        {
            DA = null;
            DA = new SqlCeDataAdapter("SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME LIKE '%CH_HARDWARE%'", SQLConn); //Instância DataAdapter com o cod a executar e a conexão
            TBCH = null;
            TBCH = new DataTable(); //Instância DataTable.
            DA.Fill(TBCH); //preenche tabela
            try
            {

                if (TBCH.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                return true;
            }
        }

        public bool ExcluirTabela()
        {
            string cod;
            string linha;
            cod = "Drop table CH_HARDWARE";
            SQLComand = new SqlCeCommand(cod, SQLConn);
            try
            {
                SQLConn.Open();
                linha = SQLComand.ExecuteNonQuery().ToString();
                SQLConn.Close();
            }
            catch (Exception ex)
            {
                ex.ToString();
                SQLConn.Close();
            }
            return true;
        }

        public void InformacaoProcessador(string InformacaoSolicitada)
        {
            s2 = null;
            s2 = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");

            switch (InformacaoSolicitada)
            {
                case "IDProcessador":
                    foreach (ManagementObject mo in s2.Get())
                    {
                        try
                        {
                            IDProcessador = mo["ProcessorId"].ToString().Trim();
                            return;
                        }
                        catch
                        {
                            IDProcessador = "";
                        }
                    }
                    break;
                case "Arquitetura":
                    foreach (ManagementObject mo in s2.Get())
                    {
                        try
                        {
                            ArquiteturaP = mo["DataWidth"].ToString().Trim();
                            return;
                        }
                        catch
                        {
                            ArquiteturaP = "";
                        }
                    }
                    break;
                case "Nome":
                    foreach (ManagementObject mo in s2.Get())
                    {
                        try
                        {
                            NomeProcessador = mo["Name"].ToString().Trim();
                            return;
                        }
                        catch
                        {
                            NomeProcessador = "";
                        }
                    }
                    break;
            }
        }

        public void InformacaoHD(string InformacaoSolicitada)
        {
            s2 = null;

            switch (InformacaoSolicitada)
            {
                case "IDHD":
                    s2 = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM  Win32_DiskDrive");
                    foreach (ManagementObject mo in s2.Get())
                    {
                        if (SerialHD != "")
                        {
                            try
                            {
                                SerialHD = mo["SerialNumber"].ToString().Trim();
                                return;
                            }
                            catch
                            {
                                SerialHD = "";
                            }
                        }
                    }
                    break;
                case "Capacidade":
                    s2 = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM  Win32_LogicalDisk");

                    foreach (ManagementObject mo in s2.Get())
                    {
                        if (CapacidadeHD == null)
                        {
                            try
                            {
                                CapacidadeHD = mo["Size"].ToString().Trim();
                                return;
                            }
                            catch
                            {
                                CapacidadeHD = "";
                            }
                        }
                    }
                    break;
                case "EspacoLivre":
                    s2 = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM  Win32_LogicalDisk");
                    foreach (ManagementObject mo in s2.Get())
                    {
                        if (EspacoLivreHD == null)
                        {
                            try
                            {
                                EspacoLivreHD = mo["FreeSpace"].ToString().Trim();
                                return;
                            }
                            catch
                            {
                                EspacoLivreHD = "";
                            }
                        }
                    }
                    break;
            }
        }

        public void InformacaoMemoria(string InformacaoSolicitada)
        {
            s2 = null;

            switch (InformacaoSolicitada)
            {
                case "Capacidade":
                    s2 = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM  Win32_PhysicalMemory");
                    foreach (ManagementObject mo in s2.Get())
                    {
                        try
                        {
                            Memoria = mo["Capacity"].ToString().Trim();
                            return;
                        }
                        catch
                        {
                            Memoria = "";
                        }
                    }
                    break;
            }
        }

        public void InformacaoRede()
        {
            System.Management.ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            System.Management.ManagementObjectCollection moc = mc.GetInstances();

            foreach (System.Management.ManagementBaseObject mo in moc)
            {
                string tt = mo["IPenabled"].ToString();

                if (mo["IPenabled"].ToString() == "True")
                {
                    try
                    {
                        MACRede = mo["MacAddress"].ToString().Trim();
                        return;
                    }
                    catch
                    {
                        MACRede = "";
                    }
                }
                else
                    MACRede = "";
            }
        }
        //PAREI AQUI --- COntinuar em 26/08
        public string InformacaoSO()
        {
            System.OperatingSystem osInfo = System.Environment.OSVersion;

            switch (osInfo.Platform)
            {
                case System.PlatformID.Win32Windows:
                    try
                    {
                        return osInfo.VersionString;
                    }
                    catch
                    {
                        return "";
                    }
                case System.PlatformID.Win32NT:
                    try
                    {
                        return osInfo.VersionString;
                    }
                    catch
                    {
                        return "";
                    }
            }
            return "";
        }

        public void EnviaInformacaohardware(int IDempresa, int IDsetor, string versao_Dispositivo, string IPLocal, string UserOS, string HashMaquina, string UrlWS)
        {
            InsereDados();
            //Variáveis.
            DA = null;
            DA = new SqlCeDataAdapter("Select * from CH_Hardware", SQLConn); //Instância DataAdapter com o cod a executar e a conexão
            TBCH = null;
            TBCH = new DataTable(); //Instância DataTable.
            DA.Fill(TBCH); //preenche tabela
            var osInfo = OSVersionInfo.GetOSVersionInfo();
            if (TBCH.Rows.Count > 0)
            {
                //Informações do hardware ---
                pontonarede.ServiceSoapClient WebS = new pontonarede.ServiceSoapClient("ServiceSoap", UrlWS);
                WebS.Endpoint.Binding.OpenTimeout = TimeSpan.FromSeconds(3);
                WebS.Endpoint.Binding.SendTimeout = TimeSpan.FromSeconds(5);
                try
                {
                    WebS.Open();
                    ChaveHardware CH =
                        new ChaveHardware(pnrClient.Properties.Settings.Default.ptServer35ConnectionString);

                    WebS.MonitoramentoClientHash(TBCH.Rows[0]["SerialHD"].ToString(), TBCH.Rows[0]["SerialProcessador"].ToString(),
                        TBCH.Rows[0]["EnderecoMAC"].ToString(), IPLocal, UserOS, osInfo.FullName,
                        DateTime.Now, IDempresa, IDsetor, versao_Dispositivo, TBCH.Rows[0]["TotalMemoria"].ToString(),
                        TBCH.Rows[0]["EspacoLivreHD"].ToString(), TBCH.Rows[0]["CapacidadeHD"].ToString(),
                        TBCH.Rows[0]["ArquiteturaMaquina"].ToString(), TBCH.Rows[0]["ModeloProcessador"].ToString(), HashMaquina);
                    WebS.Close();

                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Erro na hora de enviar informação da máquina! " + ex.ToString(),
                    //"Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ex.ToString();
                    WebS.Close();
                }
            }
        }
    }
}
