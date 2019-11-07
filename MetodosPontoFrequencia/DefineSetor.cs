using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace MetodosPontoFrequencia
{
    public class DefineSetor
    {
        public string IDSetor = "";
        public string Setor = "";
        public string Empresa = "";

        public string IDEmpresa
        {
            get
            {
                return Empresa;
            }
        }

        public string IDSETOR
        {
            get
            {
                return Setor;
            }
        }


        public void PegaEmpresaSetor()
        {
            string Caminho = System.Environment.CurrentDirectory.ToString() + "//Setor.txt"; //"C://PontoFrequencia//Setor.txt";
            System.IO.StreamReader rs;
            string LindaAtual;


            if (!System.IO.File.Exists(Caminho))
            {
                Empresa = "0";
            }
            else
            {
                using (rs = new StreamReader(Caminho))
                {
                    while (!rs.EndOfStream)
                    {
                        LindaAtual = rs.ReadLine();

                        if (LindaAtual.Substring(0, 7) == "Empresa")
                        {
                            Empresa = LindaAtual.Substring(8, 1);
                        }

                        if (LindaAtual.Substring(0, 3) == "Set")
                        {
                            //Definir um método para pegar setor quando for mais de uma linha
                            Setor = LindaAtual.Substring(6,2);
                        }
                    }
                }
            }
        }
        public string NomeSetor(int IDSETOR, int IDEmpresa)
        {
            DataSetPontoFrequencia.TBInformacaoDiariaDataTable TBInformacaoDiaria = new DataSetPontoFrequencia.TBInformacaoDiariaDataTable();
            DataSetPontoFrequenciaTableAdapters.TBInformacaoDiariaTableAdapter adpInformacaoDiaria = new DataSetPontoFrequenciaTableAdapters.TBInformacaoDiariaTableAdapter();

            DataSetPontoFrequencia.TBSetorDataTable TBSetor = new DataSetPontoFrequencia.TBSetorDataTable();
            DataSetPontoFrequenciaTableAdapters.TBSetorTableAdapter adpSetor = new DataSetPontoFrequenciaTableAdapters.TBSetorTableAdapter();
            string msg = "";

            try
            {
                adpSetor.FillByIDSetor(TBSetor, IDSETOR, IDEmpresa);
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                msg = string.Format("Conexão com o banco de dados nula. Verifique o acesso a internet ou contate o administrador da rede");
                ex.ToString();
            }
            catch (Exception ex)
            {
                msg = "Setor Não Identificado";
                ex.ToString();
                return msg;
            }

            if (!TBSetor[0].IsDSSetorNull())
            {
                return TBSetor[0].DSSetor.ToString();
            }

                return "";   
        }

        public string InformacaoDiaria(int IDSETOR)
        {
            DataSetPontoFrequencia.TBInformacaoDiariaDataTable TBInformacaoDiaria = new DataSetPontoFrequencia.TBInformacaoDiariaDataTable();
            DataSetPontoFrequenciaTableAdapters.TBInformacaoDiariaTableAdapter adpInformacaoDiaria = new DataSetPontoFrequenciaTableAdapters.TBInformacaoDiariaTableAdapter();

            try
            {
                adpInformacaoDiaria.FillByTopInformacaoDiaria(TBInformacaoDiaria);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            if (!TBInformacaoDiaria[0].IsDSInformacaoDiariaNull())
            {
                return TBInformacaoDiaria[0].DSInformacaoDiaria.ToString();
            }
            else
            {
                return "";
            }
        }
    }
}
