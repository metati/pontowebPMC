using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace MetodosPontoFrequencia
{
    public class EnviaSMS
    {
        //23/08/2017 - ENVIAR SMS PARA QUEM FEZ O REGISTRO
        #region Variaveis e constantes
        const string Login = "dayanbelem@gestao.mt.gov.br";
        const string senha = "dayanbelem";
        string msg;
        string url = "https://api.gtisms.com/gti/API/send.aspx";
        int cont;
        byte[] buffer;
        StringBuilder sb;
        string temp;
        Stream stream;
        string result;
        #endregion

        public void enviaSMS(string Nome, DateTime HoraBatida, string Telefone)
        {
                try
                {
                    //msg
                    msg = string.Format("Ponto na rede: {2}, registro efetivado {0} às {1}.", 
                        HoraBatida.ToShortDateString(), 
                        HoraBatida.ToShortTimeString(), Nome);
                    //Monta a url
                    url = string.Format("{0}?user={1}&senha={2}&msg={3}&n={4}&id=1", url, 
                        Login, senha, msg, Telefone.Trim());
                    
                    //Monta a requisição
                    HttpWebRequest requisicao = (HttpWebRequest)WebRequest.Create(url);
                    HttpWebResponse resposta = (HttpWebResponse)requisicao.GetResponse();

                    //Recebendo resposta
                    buffer = new byte[1000];
                    sb = new StringBuilder();
                    stream = resposta.GetResponseStream();
                    do
                    {
                        cont = stream.Read(buffer, 0, buffer.Length);
                        temp = Encoding.Default.GetString(buffer, 0, cont).Trim();
                        sb.Append(temp);
                    }while (cont > 0);

                    result = sb.ToString();

                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
        }
    }
}
