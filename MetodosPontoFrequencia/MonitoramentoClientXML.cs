using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Data;

namespace MetodosPontoFrequencia
{
    public class MonitoramentoClientXML
    {
        bool criando = false;
        
        //Para ler arquivo XML
        XmlDocument xmlDoc = new XmlDocument();
        XmlTextWriter escritaXML;
        
        public void CriaEscreveArquivoXML(string Caminho, string IDEmpresa, string IDSetor, string SerialHD, string SerialProcessador, 
            string MACRede, string OS,string NomeComputador,string IPlocal,DateTime DTUltimaConexao, string ChaveUnica, string VersaoClient,string TotalMemoria,string EspacoLivreHD,
            string CapacidadeHD, string ArquiteturaMaquina,string Processador)
        {
            Caminho = "C:";
           
            if(!System.IO.File.Exists(string.Format("{0}\\WEBPontoClient.xml",Caminho)))
            {
                escritaXML = new XmlTextWriter(string.Format("{0}\\WEBPontoClient.xml", Caminho), null);
                
                //Inicia objeto para construção do XML.
                escritaXML.WriteStartDocument();
                //Inicia o elemento xml
                escritaXML.WriteStartElement("WEBPontoClient");
                //Escreve os subelementos do arquivo XML
                escritaXML.WriteElementString("Chave", ChaveUnica);
                escritaXML.WriteElementString("IDEmpresa", IDEmpresa);
                escritaXML.WriteElementString("IDSetor", IDSetor);
                escritaXML.WriteElementString("SerialHD", SerialHD);
                escritaXML.WriteElementString("SerialProcessador", SerialProcessador);
                escritaXML.WriteElementString("MACRede", MACRede);
                escritaXML.WriteElementString("SistemaOperacional", OS);
                escritaXML.WriteElementString("NomeComputador", NomeComputador);
                escritaXML.WriteElementString("IPLocal", IPlocal);
                escritaXML.WriteElementString("UltimaConexao", DTUltimaConexao.ToString());
                escritaXML.WriteElementString("VesaoClient", VersaoClient);
                escritaXML.WriteElementString("TotalMemoria", TotalMemoria);
                escritaXML.WriteElementString("EspacoLivreHD", EspacoLivreHD);
                escritaXML.WriteElementString("CapacidadeHD", CapacidadeHD);
                escritaXML.WriteElementString("ArquiteturaMaquina", ArquiteturaMaquina);
                escritaXML.WriteElementString("Processador", Processador);
                
                criando = true;                
            }

            if (!VerificaClient(ChaveUnica,string.Format("{0}\\WEBPontoClient.xml",Caminho),DTUltimaConexao.ToString()) && !criando)
            {

                //Inicia objeto para construção do XML.
                escritaXML.WriteStartDocument();
                //Inicia o elemento xml
                escritaXML.WriteStartElement("WEBPontoClient");

                //Escreve os subelementos do arquivo XML
                escritaXML.WriteElementString("Chave", ChaveUnica);
                escritaXML.WriteElementString("IDEmpresa", IDEmpresa);
                escritaXML.WriteElementString("IDSetor", IDSetor);
                escritaXML.WriteElementString("SerialHD", SerialHD);
                escritaXML.WriteElementString("SerialProcessador", SerialProcessador);
                escritaXML.WriteElementString("MACRede", MACRede);
                escritaXML.WriteElementString("SistemaOperacional", OS);
                escritaXML.WriteElementString("NomeComputador", NomeComputador);
                escritaXML.WriteElementString("IPLocal", IPlocal);
                escritaXML.WriteElementString("UltimaConexao", DTUltimaConexao.ToString());
                escritaXML.WriteElementString("VesaoClient", VersaoClient);
                escritaXML.WriteElementString("TotalMemoria", TotalMemoria);
                escritaXML.WriteElementString("EspacoLivreHD", EspacoLivreHD);
                escritaXML.WriteElementString("CapacidadeHD", CapacidadeHD);
                escritaXML.WriteElementString("ArquiteturaMaquina", ArquiteturaMaquina);
                escritaXML.WriteElementString("Processador", Processador);
            }
            //Finaliza o elemento Raiz.
            escritaXML.WriteEndElement();
            //Finaliza o objeto
            escritaXML.Close();
        }

        public bool VerificaClient(string Chave,string Caminho, string DTUltimaConexao)
        {
            DataSet dsXML = new DataSet();
            xmlDoc.Load(Caminho);
            try
            {
                if(System.IO.File.Exists(Caminho))
                {
                    XmlNode noPrincipal = xmlDoc.DocumentElement;
                    foreach (XmlNode no1 in noPrincipal.ChildNodes)
                    {
                        foreach (XmlNode no2 in no1.ChildNodes)
                        {
                            if (no2.Name.Trim() == "Chave")
                            {
                                no2.InnerText = DTUltimaConexao;
                                return true; //Retornar verdadeiro, significa que o ítem já existe.
                            }
                        }
                    }
                    
                    return false;
                }
            }
            catch
            {
                return false;
            }

            return false;
        }
    }
}
