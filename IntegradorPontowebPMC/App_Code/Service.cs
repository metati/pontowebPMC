using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using MetodosPontoFrequencia;
using System.Xml;
using System.Data;
using MetodosPontoFrequencia.Model;
using MetodosPontoFrequencia.Justificativa;
using MetodosPontoFrequencia.RegistroPonto;
using System.Net;
using System.IO;
using MetaTI.Util;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]

public class Service : System.Web.Services.WebService
{
    public bool bateu;
    string retmsg;
    bool RetCadREP;
    string MaiorRefREP;
    private Byte[] Foto;
    Cadastro CD = new Cadastro();

    //Variáveis
    string IDFrequenciaLocal, DTFrequencia, IDusuario, IDEmpresa, IDSetor, RetornoMSG, strHoraCumprir;
    int pos, pos2, Tamanho;
    TimeSpan horaDia, horaCumprir, resultHora;

    [WebMethod]
    public void InformarOcorrencia(DateTime DTOcorrencia, string DSOcorrencia, int IDEmpresa, int IDSetor,
        string IPLocal, string VersaoClient, string NomeMaquina)
    {
        try
        {
            CD.InsertOcorrencia(DTOcorrencia, DSOcorrencia, IDEmpresa, IDSetor, IPLocal, NomeMaquina, VersaoClient);
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    [WebMethod]
    public DataSetUsuario SearchNomeCPFLogin(string TextoBusca, int TipoBusca, int IDEmpresa, string TokenAcesso)
    {
        int IDGrupoRegistro;
        DataSetUsuario dsu;
        PreencheTabela PT;

        if (TokenAcesso != "TentoWebServiceNovamente7x24dm12")
        {
            return null;
        }

        try
        {
            //1 Busca por nome, 2 Busca por Matricula, 3 Busca por CPF
            dsu = new DataSetUsuario();
            dsu.EnforceConstraints = false;
            PT = new PreencheTabela();
            MetodosPontoFrequencia.DataSetUsuarioTableAdapters.vwUsuarioWebServiceTableAdapter adpvwUsuario =
                new MetodosPontoFrequencia.DataSetUsuarioTableAdapters.vwUsuarioWebServiceTableAdapter();
            IDGrupoRegistro = PT.GrupoRegistro(IDEmpresa);
            switch (TipoBusca)
            {
                case 1:
                    adpvwUsuario.FillSearchNome(dsu.vwUsuarioWebService, TextoBusca, IDEmpresa, IDGrupoRegistro);
                    break;
                case 2:
                    adpvwUsuario.FillSearchMatriculaGrupoRegistro(dsu.vwUsuarioWebService, TextoBusca.Trim(), IDEmpresa, IDGrupoRegistro);
                    break;
                case 3:
                    adpvwUsuario.FillSearchCPFIDempresa(dsu.vwUsuarioWebService, TextoBusca, IDEmpresa, IDGrupoRegistro);
                    break;
            }

            return dsu;
        }
        catch (Exception ex)
        {
            ex.ToString();
            return null;
        }
    }

    [WebMethod]
    public string RegistroFalha(string[] Registros)
    {
        Cadastro Cad = new Cadastro();
        if (Registros.Length > 0)
        {

            for (int i = 0; i <= Registros.Length - 1; i++)
            {
                //Desmontando String[] --

                //Zerando os posicionadores
                pos = 0;
                pos2 = 0;
                Tamanho = 0;

                //Pegando o primeiro Limitador
                pos = Registros[i].IndexOf(';');
                //obtendo o numero da frequência Local
                IDFrequenciaLocal = Registros[i].Substring(0, pos);
                //Mudando posicionador para a próxima posição para a busca.
                pos++;
                // Instânciando o pos2
                pos2 = Registros[i].IndexOf(';', pos);
                //Definindo o tamanho da substring
                Tamanho = pos2 - pos;
                //Definindo a data
                DTFrequencia = Registros[i].Substring(pos, Tamanho);
                //próxima casa para pos2
                pos2++;
                // próxima posição pos
                pos = Registros[i].IndexOf(';', pos2);
                //Mudando tamanho da substring
                Tamanho = pos - pos2;
                //Definindo IDusuario
                IDusuario = Registros[i].Substring(pos2, Tamanho);
                //Definindo Empresa
                IDEmpresa = Registros[i].Substring(pos + 1, 1);
                //Definindo Setor
                IDSetor = Registros[i].Substring(pos + 3, 1);
                //Definindo o retorno da mensagem
                RetornoMSG = Registros[i].Substring(pos + 5);

                try
                {
                    Cad.RegistroLocalFalha(Convert.ToInt32(IDFrequenciaLocal), Convert.ToDateTime(DTFrequencia), Convert.ToInt32(IDusuario), RetornoMSG, Convert.ToInt32(IDEmpresa), Convert.ToInt32(IDSetor));
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }
        }
        return "";
    }

    [WebMethod]
    public void MonitoramentoClient(string SerialHD, string SerialProcessador, string MACRede, string IPLocal, string NomeComputador,
        string SistemaOperacional, DateTime DTultimaConexao, int IDEmpresa, int IDSetor, string VersaoClient, string MemoriaTotal,
        string EspacoLivreHD, string CapacidadeHD, string ArquiteturaMaquina, string Processador)
    {
        CD.InsereNovoClient(SerialHD, SerialProcessador, MACRede, IPLocal, NomeComputador, SistemaOperacional, DTultimaConexao, IDEmpresa, IDSetor,
            VersaoClient, MemoriaTotal, EspacoLivreHD, CapacidadeHD, ArquiteturaMaquina, Processador, string.Format("{0}{1}{2}", SerialProcessador, SerialHD, MACRede));
    }

    [WebMethod]
    public void MonitoramentoClientHash(string SerialHD, string SerialProcessador, string MACRede, string IPLocal, string NomeComputador,
    string SistemaOperacional, DateTime DTultimaConexao, int IDEmpresa, int IDSetor, string VersaoClient, string MemoriaTotal,
    string EspacoLivreHD, string CapacidadeHD, string ArquiteturaMaquina, string Processador, string HashMaquina)
    {
        if ("V_2.9_BETA" != VersaoClient)
        {
            VersaoClient = "V_2.8_BETA";
        }
        try
        {

            InfoClints info = new InfoClints();
            info.InsereNovoClientHash(SerialHD, SerialProcessador, MACRede, IPLocal, NomeComputador, SistemaOperacional, DTultimaConexao, IDEmpresa, IDSetor,
               VersaoClient, MemoriaTotal, EspacoLivreHD, CapacidadeHD, ArquiteturaMaquina, Processador, string.Format("{0}{1}{2}", SerialProcessador, SerialHD, MACRede), HashMaquina);
        }
        catch (Exception ex)
        {
            UtilLog.EscreveLog(DateTime.Now.ToLocalTime() + " MonitoramentoClientHash ERRO " + ex.Message);
        }
        //CD.InsereNovoClientHash(SerialHD, SerialProcessador, MACRede, IPLocal, NomeComputador, SistemaOperacional, DTultimaConexao, IDEmpresa, IDSetor,
        //    VersaoClient, MemoriaTotal, EspacoLivreHD, CapacidadeHD, ArquiteturaMaquina, Processador, string.Format("{0}{1}{2}", SerialProcessador, SerialHD, MACRede), HashMaquina);
    }

    [WebMethod]
    public string GetHashMaquina(string SerialProcessador, string SerialHD, string MACRede)
    {
        return CD.GetHashMaquina(string.Format("{0}{1}{2}", SerialProcessador, SerialHD, MACRede));
    }

    [WebMethod]
    public DateTime HoraServidor()
    {
        //MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwHorasTableAdapter adpHora = new MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwHorasTableAdapter();
        //DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
        try
        {
            //adpHora.Connection.Open();
            //adpHora.Fill(ds.vwHoras);
            //adpHora.Connection.Close();
            return System.DateTime.Now;//ds.vwHoras[0].Horas;
        }
        catch (Exception ex)
        {
            return System.DateTime.Now;
        }
    }


    //Teste Atualização Client Ponto
    [WebMethod]
    public bool DisponibilidadeServidor(int IDEmpresa, int IDSetor)
    {
        //UtilLog.EscreveLog(DateTime.Now.ToLocalTime() + " DisponibilidadeServidor");
        //MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.TBDisponibilidadeServidorTableAdapter adpDisp = new MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.TBDisponibilidadeServidorTableAdapter();

        //Trocado por enquanto até definir novo meio de verificar a disponibilidade dos client em cada órgão. -- 07/03/2016

        //MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwHorasTableAdapter adpHoras = new MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwHorasTableAdapter();
        //DataSetPontoFrequencia.vwHorasDataTable vwhoras = new DataSetPontoFrequencia.vwHorasDataTable();

        try
        {
            //adpHoras.Fill(vwhoras);
            return true;
        }
        catch (Exception ex)
        {
            UtilLog.EscreveLog(DateTime.Now.ToLocalTime() + " Disponibilidade ERRO " + ex.Message);
            return false;
        }
        //try
        //{
        //if (adpDisp.Insert(System.DateTime.Now, IDSetor, IDEmpresa) > 0)
        //{
        //return true;
        //}

        //else return false;
        //}
        //catch (Exception ex)
        //{    
        //ex.ToString();
        //return false;
        //}
    }


    [WebMethod]
    public string VersaoClient(int IDEmpresa)
    {
        //06/04/2016 --
        //VERIFICAR A POSSIBILIDADE DE ATUALIZAR SEM PRECISAR SER POR EMPRESA.

        string Versao = string.Empty;
        DataSetUsuario dsU = new DataSetUsuario();
        PreencheTabela PT = new PreencheTabela();

        try
        {
            PT.PreencheUltimaVersaoClientBiometria(dsU, IDEmpresa);

            if (dsU.TBConfiguracaoFTP.Rows.Count > 0)
            {
                Versao = dsU.TBConfiguracaoFTP[0].VersaoClientBiometria.Trim();
            }
            else
            {
                Versao = string.Empty;
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
            Versao = string.Empty;
        }

        return Versao;
    }

    public bool BATEUU
    {
        get
        {
            return bateu;
        }
    }

    public Service()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }
    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }
    [WebMethod]
    public DataSetREP REPEmpresaSetor(int IDEmpresa, int IDSetor, string TokenAcesso)
    {
        DataSetREP dsR = new DataSetREP();
        REP rep = new REP();
        if (TokenAcesso == "TentoWebServiceNovamente7x24dm12")
        {
            PreencheTabela PT = new PreencheTabela();

            try
            {
                rep.PreencheTBREPEmpresaSetor(dsR, IDEmpresa, IDSetor);
            }
            finally
            {

            }
        }
        return dsR;
    }
    [WebMethod]
    public DataSetPontoFrequencia RetornaEmpresas()
    {
        PreencheTabela PT = new PreencheTabela();
        DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
        try
        {
            PT.PreencheTBEmpresa(ds);
        }
        catch (Exception ex)
        {
            ex.ToString();
        }

        return ds;
    }
    [WebMethod]
    public DataSetPontoFrequencia SetorEmpresa(int IDEmpresa)
    {
        DataSetPontoFrequencia dsP = new DataSetPontoFrequencia();
        PreencheTabela PT = new PreencheTabela();
        PT.PreencheTBSetorIDEmpresa(dsP, IDEmpresa);
        return dsP;
    }

    [WebMethod]
    public string NomeSetor(int IDSetor, int IDEmpresa, string TokenAcesso)
    {
        string Setor = "";
        try
        {
            DefineSetor DS = new DefineSetor();
            Setor = DS.NomeSetor(IDSetor, IDEmpresa);
        }
        finally
        {
        }
        return Setor;
    }
    [WebMethod]
    public DataSetREP REPEmpresa(int IDEmpresa, string TokenAcesso)
    {
        DataSetREP dsR = new DataSetREP();
        REP rep = new REP();

        if (TokenAcesso == "TentoWebServiceNovamente7x24dm12")
        {
            PreencheTabela PT = new PreencheTabela();

            try
            {
                rep.PreencheTBREPEmpresa(dsR, IDEmpresa);
            }
            finally
            {

            }
        }
        return dsR;
    }
    [WebMethod]
    public DataSetUsuario UsuariosPonto(int IDEmpresa, string TokenAcesso)
    {
        DataSetUsuario dsU = new DataSetUsuario();
        if (TokenAcesso == "TentoWebServiceNovamente7x24dm12")
        {
            PreencheTabela PT = new PreencheTabela();
            try
            {
                PT.PreenchevwUsuarioWebService(dsU, IDEmpresa);
            }
            catch (Exception ex)
            {
                UtilLog.EscreveLog(DateTime.Now.ToLocalTime() + " UsuariosPonto ERRO " + ex.Message);
            }
            finally
            {

            }
        }
        return dsU;
    }

    [WebMethod]
    public DataSetUsuario UsuariosPontoHash(int IDEmpresa, string TokenAcesso, string HashMaquina)
    {
        DataSetUsuario dsU = new DataSetUsuario();
        if (TokenAcesso == "TentoWebServiceNovamente7x24dm12")
        {
            PreencheTabela PT = new PreencheTabela();
            try
            {
                PT.PreenchevwUsuarioWebServiceHash3(dsU, IDEmpresa, HashMaquina);
            }
            catch (Exception ex)
            {
                UtilLog.EscreveLog(DateTime.Now.ToLocalTime() + " UsuariosPontoHash ERRO " + ex.Message);
            }
            finally
            {

            }
        }
        return dsU;
    }

    [WebMethod]
    public string ManutencaoHashCode(int IDUsuario, string hash, int IDempresa, string TokenAcesso)
    {
        Cadastro Cad = new Cadastro();
        string msg = "";
        if (TokenAcesso == "TentoWebServiceNovamente7x24dm12")
        {
            if (hash != "")
            {
                Cadastro cad = new Cadastro();
                msg = cad.ManutencaoSenhaTextHashCode(IDUsuario, hash, IDempresa);
            }
        }
        return msg;
    }
    [WebMethod]
    public string ManutencaoTemplate(int IDVinculoUsuario, int IDEmpresa, byte[] Template, string LoginOperador)
    {
        Cadastro cad = new Cadastro();
        if (!cad.InsertTemplate1(IDVinculoUsuario, IDEmpresa, Template, LoginOperador))
            return "Tentativo de cadastro biométrico não obteve êxito. Repita o processo!";
        else
            return "Biometria incluída com sucesso!";
    }
    [WebMethod]
    public string ManutencaoTemplate2(int IDVinculoUsuario, int IDEmpresa, byte[] Template, byte[] Template2, string LoginOperador)
    {
        Cadastro cad = new Cadastro();
        if (!cad.InsertTemplate1_2(IDVinculoUsuario, IDEmpresa, Template, Template2, LoginOperador))
            return "Tentativo de cadastro biométrico não obteve êxito. Repita o processo!";
        else
            return "Biometria incluída com sucesso!";
    }
    [WebMethod]
    public DataSetUsuario UsuariosPontoNome(int IDEmpresa, string Nome, string TokenAcesso)
    {
        DataSetUsuario dsU = new DataSetUsuario();
        if (TokenAcesso == "TentoWebServiceNovamente7x24dm12")
        {
            PreencheTabela PT = new PreencheTabela();
            try
            {
                PT.PreenchevwUsuarioWebServiceNome(dsU, IDEmpresa, Nome);
            }
            finally
            {

            }
        }
        return dsU;
    }
    [WebMethod]
    public DataSetUsuario UsuarioSemREP(int IDEmpresa, string TokenAcesso, int IDREP)
    {
        DataSetUsuario dsU = new DataSetUsuario();
        if (TokenAcesso == "TentoWebServiceNovamente7x24dm12")
        {
            PreencheTabela PT = new PreencheTabela();
            try
            {
                PT.PreencheVWUsuarioWebServiceREP(dsU, IDEmpresa, IDREP);
            }
            finally
            {

            }
        }
        return dsU;
    }
    [WebMethod]
    public DataSetUsuario UsuarioSemREPGeral(int IDEmpresa, string TokenAcesso, int IDREP)
    {
        DataSetUsuario dsU = new DataSetUsuario();
        if (TokenAcesso == "TentoWebServiceNovamente7x24dm12")
        {
            PreencheTabela PT = new PreencheTabela();

            try
            {
                PT.PreencheVWUsuarioWebServiceREPGeral(dsU, IDEmpresa, IDREP);
            }
            finally
            {

            }
        }
        return dsU;
    }
    [WebMethod]
    public string UltimaRefREP(int IDEmpresa, string TokenAcesso)
    {
        DataSetUsuario dsU = new DataSetUsuario();
        if (TokenAcesso == "TentoWebServiceNovamente7x24dm12")
        {
            PreencheTabela PT = new PreencheTabela();
            try
            {
                PT.UltimaRefREPCadastrada(dsU, IDEmpresa);
                if (dsU.vwMaiorRefREP.Rows.Count > 0)
                    MaiorRefREP = dsU.vwMaiorRefREP[0].MaiorRefREP.ToString();
            }
            catch (Exception ex)
            {
                MaiorRefREP = "0";
            }
        }

        return MaiorRefREP;
    }
    [WebMethod]
    public void DTUltimaColeta(int IDREP, DateTime dtultimaColeta)
    {
        REP rep = new REP();
        try
        {
            rep.CadastraUltimaColetaUsuario(IDREP, dtultimaColeta, dtultimaColeta.ToShortDateString());
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }
    [WebMethod]
    public bool CadUltimaRefREP(int IDEmpresa, int IDUsuario, string RefREP, string TokenAcesso, int IDREP)
    {
        if (TokenAcesso == "TentoWebServiceNovamente7x24dm12")
        {
            Cadastro Cad = new Cadastro();
            REP rep = new REP();
            try
            {
                RetCadREP = Cad.AlteraReferenciaREP(IDEmpresa, IDUsuario, RefREP);
                rep.CadastraRelacaoREPUsuario(IDUsuario, IDREP, IDEmpresa);
            }
            finally
            {
                RetCadREP = false;
            }

        }
        return RetCadREP;
    }
    [WebMethod]
    public DataSetUsuario UsuariosPontoLogin(int IDEmpresa, string Login, string TokenAcesso)
    {
        DataSetUsuario dsU = new DataSetUsuario();
        if (TokenAcesso == "TentoWebServiceNovamente7x24dm12")
        {
            PreencheTabela PT = new PreencheTabela();
            try
            {
                PT.PreenchevwUsuarioWebServiceLogin(dsU, IDEmpresa, Login);
            }
            finally
            {

            }
        }
        return dsU;
    }
    [WebMethod]
    public bool LogarAdmin(int IDEmpresa, string Login, string Senha, string TokenAcesso)
    {
        DataSetUsuario dsU = new DataSetUsuario();
        if (TokenAcesso == "TentoWebServiceNovamente7x24dm12")
        {
            PreencheTabela PT = new PreencheTabela();
            try
            {
                PT.PreenchevwUsuarioWebServiceLogar(dsU, IDEmpresa, Login, Senha);

                if (dsU.vwUsuarioWebService.Rows.Count > 0)
                {
                    if (dsU.vwUsuarioWebService[0].IDTPUsuario == 1 || dsU.vwUsuarioWebService[0].CadastraDigital || dsU.vwUsuarioWebService[0].IDTPUsuario == 7)
                    {
                        if (dsU.vwUsuarioWebService[0].IDTPUsuario == 1)
                            return true;

                        if (IDEmpresa == dsU.vwUsuarioWebService[0].IDEmpresa && dsU.vwUsuarioWebService[0].IDTPUsuario == 7)
                            return true;

                        if (dsU.vwUsuarioWebService[0].CadastraDigital && IDEmpresa == dsU.vwUsuarioWebService[0].IDEmpresa)
                            return true;

                        //Verificação se a empresa pertence a algum grupo ... Acrescentado em 11/09/2015 - 
                        //P atender o grupo de registro Paiaguás
                        if (IDEmpresa != dsU.vwUsuarioWebService[0].IDEmpresa)
                        {
                            return PT.EmpresaGrupoRegistro(dsU.vwUsuarioWebService[0].IDEmpresa);
                        }
                    }
                    else
                        return false;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        return false;
    }
    [WebMethod]
    public string LocalizaUsuarioPIS(string PIS, string Data, string Hora, int IDEmpresa, string TokenAcesso)
    {
        string DataHora = "";
        if (TokenAcesso == "TentoWebServiceNovamente7x24dm12")
        {
            PreencheTabela PT = new PreencheTabela();
            DataSetUsuario dsU = new DataSetUsuario();

            PT.PreenchevwUsuarioWebService(dsU, IDEmpresa, PIS);

            if (dsU.vwUsuarioWebService.Rows.Count > 0)
            {
                Data = string.Format("{0}/{1}/{2}", Data.Substring(0, 2), Data.Substring(2, 2), Data.Substring(4, 4));
                Hora = string.Format("{0}:{1}", Hora.Substring(0, 2), Hora.Substring(2, 2));
                DataHora = string.Format("{0} {1}", Data, Hora);
                retmsg = BaterPonto(IDEmpresa, 0, dsU.vwUsuarioWebService[0].IDUsuario,
                    Convert.ToDateTime(DataHora), "TentoWebServiceNovamente7x24dm12", "", "",
                    "", "", 0, "", "", false, 1);
            }
        }

        return retmsg;
    }
    [WebMethod]
    public Byte[] FotoUsuario(int IDusuario, int IDEmpresa)
    {
        PreencheTabela PT = new PreencheTabela();
        DataSetUsuario dsU = new DataSetUsuario();

        PT.PreenchevwUsuarioWebServiceIDUsuario(dsU, IDEmpresa, IDusuario);

        if (dsU.vwUsuarioWebServiceFoto.Rows.Count > 0)
        {
            if (!dsU.vwUsuarioWebServiceFoto[0].IsFotoUsuarioNull())
            {
                Foto = dsU.vwUsuarioWebServiceFoto[0].FotoUsuario;
            }
        }

        return Foto;
    }
    [WebMethod]
    //Para testes. Bater ponto3
    public string BaterPonto3(int IDEmpresa, int idsetorbatida, int IDUsuario, DateTime HoraBatida, string TokenAcesso,
        string HoraEntradaManha, string HoraSaidaManha, string horaEntradaTarde, string HoraSaidaTarde,
        int TotalHoraDia, string Nome, string PrimeiroNome, bool RegimePlantonista, long IDVinculoUsuario)
    {
        Frequencia freq = new Frequencia();
        return freq.BaterPonto3(IDEmpresa, idsetorbatida, IDUsuario, HoraBatida, HoraEntradaManha, HoraSaidaManha, horaEntradaTarde,
            HoraSaidaTarde, TotalHoraDia, Nome, PrimeiroNome, RegimePlantonista, IDVinculoUsuario);
    }
    [WebMethod]

    public string BaterPonto(int IDEmpresa, int idsetorbatida, int IDUsuario, DateTime HoraBatida, string TokenAcesso,
        string HoraEntradaManha, string HoraSaidaManha, string horaEntradaTarde, string HoraSaidaTarde,
        int TotalHoraDia, string Nome, string PrimeiroNome, bool RegimePlantonista, long IDVinculoUsuario)
    {
        
        string msg = "";
        Frequencia Freq = new Frequencia();
        PreencheTabela PT = new PreencheTabela();
        DataSetPontoFrequencia dsP = new DataSetPontoFrequencia();

        if (TokenAcesso == "TentoWebServiceNovamente7x24dm12")
        {
            //msg = Freq.BaterPonto2(IDUsuario, IDEmpresa, HoraBatida,idsetorbatida,Convert.ToInt32(IDVinculoUsuario));
            if (IDEmpresa != 41)
            {
                msg = Freq.BaterPonto3(IDEmpresa, idsetorbatida, IDUsuario, HoraBatida,
                HoraEntradaManha, HoraSaidaManha, horaEntradaTarde, HoraSaidaTarde,
                TotalHoraDia, Nome, PrimeiroNome, RegimePlantonista, IDVinculoUsuario);
                if (idsetorbatida != 0)
                    LocalRegistroFrequencia(IDEmpresa, msg, IDUsuario, idsetorbatida, HoraBatida);

            }
            else
            {
                //TRATATIVA DIFERENCIADA PARA ASSIST. SOCIAL/PMC //29/09/2018
                if (IDEmpresa == 41)
                {
                    msg = Freq.BaterPontoAssistSocial(IDEmpresa, idsetorbatida, IDUsuario,
                        HoraBatida, HoraEntradaManha, HoraSaidaManha,
                        horaEntradaTarde, HoraSaidaTarde, TotalHoraDia, Nome, PrimeiroNome,
                        RegimePlantonista, IDVinculoUsuario);
                    if (idsetorbatida != 0)
                        LocalRegistroFrequencia(IDEmpresa, msg + " assist.", IDUsuario, idsetorbatida, HoraBatida);

                }
            }
        }
        else
        {
            msg = string.Format("Acesso a rotina não é válido.");
        }

        bateu = Freq.BATEU;

        return msg;
    }

    [WebMethod]
    public string BaterPontoHash(int IDEmpresa, int idsetorbatida, int IDUsuario, DateTime HoraBatida, string TokenAcesso,
        string HoraEntradaManha, string HoraSaidaManha, string horaEntradaTarde, string HoraSaidaTarde,
        int TotalHoraDia, string Nome, string PrimeiroNome, bool RegimePlantonista, long IDVinculoUsuario, string HashMaquina, string TempoLeitura)
    {
        string msg = "";
        Frequencia Freq = new Frequencia();
        PreencheTabela PT = new PreencheTabela();
        DataSetPontoFrequencia dsP = new DataSetPontoFrequencia();
        MetodosPontoFrequencia.RegistroPonto.LogRegistro log = new MetodosPontoFrequencia.RegistroPonto.LogRegistro();
        if (TokenAcesso == "TentoWebServiceNovamente7x24dm12")
        {
            //msg = Freq.BaterPonto2(IDUsuario, IDEmpresa, HoraBatida,idsetorbatida,Convert.ToInt32(IDVinculoUsuario));
            if (IDEmpresa != 41)
            {
                msg = Freq.BaterPonto3(IDEmpresa, idsetorbatida, IDUsuario, HoraBatida,
                    HoraEntradaManha, HoraSaidaManha, horaEntradaTarde, HoraSaidaTarde,
                    TotalHoraDia, Nome, PrimeiroNome, RegimePlantonista, IDVinculoUsuario);
                if (idsetorbatida != 0)
                {
                    LocalRegistroFrequencia(IDEmpresa, msg, IDUsuario, idsetorbatida, HoraBatida);
                    log.LogBatida(IDEmpresa, IDUsuario, idsetorbatida, HoraBatida, HashMaquina, TempoLeitura);
                }

            }
            else
            {
                try
                {
                    //TRATATIVA DIFERENCIADA PARA ASSIST. SOCIAL/PMC //29/09/2018
                    if (IDEmpresa == 41)
                    {
                        msg = Freq.BaterPontoAssistSocial(IDEmpresa, idsetorbatida, IDUsuario,
                            HoraBatida, HoraEntradaManha, HoraSaidaManha,
                            horaEntradaTarde, HoraSaidaTarde, TotalHoraDia, Nome, PrimeiroNome,
                            RegimePlantonista, IDVinculoUsuario);
                        if (idsetorbatida != 0)
                        {
                            log.LogBatida(IDEmpresa, IDUsuario, idsetorbatida, HoraBatida, HashMaquina, TempoLeitura);
                            LocalRegistroFrequencia(IDEmpresa, msg + " assist.", IDUsuario, idsetorbatida, HoraBatida);
                        }


                    }
                }
                catch (Exception EX)
                {
                    EX.ToString();
                }
            }
        }
        else
        {
            msg = string.Format("Acesso a rotina não é válido.");
        }

        bateu = Freq.BATEU;

        return msg;
    }

    [WebMethod]
    public DateTime UltimaColetaREP(int IDREP)
    {
        DataSetREP dsR = new DataSetREP();
        REP rep = new REP();
        DateTime DD = new DateTime(1900, 01, 01);

        try
        {
            DD = rep.UltimaColetaREP(dsR, IDREP);
        }
        finally
        {

        }
        return DD;
    }
    [WebMethod]
    public string PontoEspecial(string login, string senha, int IDEmpresa, DateTime Data, string TokenAcesso)
    {
        string msg = "";
        DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
        Frequencia Freq = new Frequencia();

        if (TokenAcesso == "TentoWebServiceNovamente7x24dm12")
        {
            msg = Freq.PontoEspecial(login, senha, IDEmpresa, Data, ds);
        }
        else
        {
            msg = string.Format("Acesso a rotina não é válido.");
        }

        return msg;
    }
    [WebMethod]
    public string PontoEspecial2(string login, string senha, int IDEmpresa, DateTime Data, string Matricula, string TokenAcesso)
    {
        //25/07/2018
        //Acrescentado matricula para a PMC.
        string msg = "";
        DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
        Frequencia Freq = new Frequencia();

        if (TokenAcesso == "TentoWebServiceNovamente7x24dm12")
        {
            msg = Freq.PontoEspecial2(login, senha, IDEmpresa, Data, ds, Matricula);
        }
        else
        {
            msg = string.Format("Acesso a rotina não é válido.");
        }

        return msg;
    }
    [WebMethod]
    public bool BATEU()
    {
        return BATEUU;
    }
    [WebMethod]
    public void LocalRegistroFrequencia(int IDEmpresa, string msgbatida, int idusuario, int idsetorBatida, DateTime DTFrequencia)
    {
        MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.TBLocalRegistroTableAdapter adpLocalRegistro =
            new MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.TBLocalRegistroTableAdapter();

        try
        {
            adpLocalRegistro.Connection.Open();
            adpLocalRegistro.Insert(idusuario, IDEmpresa, idsetorBatida, DTFrequencia, msgbatida);
            adpLocalRegistro.Connection.Close();
        }
        catch (Exception ex)
        {
            ex.ToString();
        }

    }

    [WebMethod]
    public int PedidoJustificativaLocal(string Usuario, string Matricula, string Senha, string Observacao, int IDEmpresa, int IDSetor, string TokenAcesso)
    {
        int retorno;
        if (TokenAcesso == "TentoWebServiceNovamente7x24dm12")
        {
            Justificativa jus = new Justificativa();
            retorno = jus.SalvarPedidoLocal(Usuario, Matricula, Senha, Observacao, IDEmpresa, IDSetor);
            //jus.()
        }
        else
        {
            retorno = 1;
        }
        return retorno;
    }


    [WebMethod]
    public bool SalvarLogConexoes(List<TesteConexaoModel> list, int IDEmpresa, int IDSetor, string _hashMaquina, string TokenAcesso)
    {
        bool retorno;
        if (TokenAcesso == "TentoWebServiceNovamente7x24dm12")
        {
            LogConexoesClint log = new LogConexoesClint();
            try
            {
                retorno = log.LogClint(list, IDEmpresa, IDSetor, _hashMaquina);
            }
            catch
            {
                retorno = false;
            }
        }
        else
        {
            retorno = false;
        }
        return retorno;
    }


    [WebMethod]
    public ConfigHorasSincModel GetConfigHorasSincronizacao(int IDEmpresa, int IDSetor, string _hashMaquina, string TokenAcesso)
    {
        ConfigHorasSincModel config = null;
        if (TokenAcesso == "TentoWebServiceNovamente7x24dm12")
        {
            switch (IDEmpresa)
            {
                //SAUDE
                case 54:
                    config = new ConfigHorasSincModel()
                    {
                        PrimeiraSincHoraInicio = "10:05:00.000",
                        PrimeiraSincHoraFim = "10:05:02.000",
                        SegundaSincHoraInicio = "14:45:00.000",
                        SegundaSincHoraFim = "14:45:05.000",
                        MinutosLogConexao = 10
                    };
                    break;
                default:
                    config = new ConfigHorasSincModel()
                    {
                        PrimeiraSincHoraInicio = "10:05:00.000",
                        PrimeiraSincHoraFim = "10:05:02.000",
                        SegundaSincHoraInicio = "14:45:00.000",
                        SegundaSincHoraFim = "14:45:05.000",
                        MinutosLogConexao = 10
                    };
                    break;
            }
        }
        return config;
    }

    [WebMethod]
    public byte[] GetPDF(string matricula, string Mes, string Ano, string Usuario, string Senha)
    {
        matricula = Util.TratarString(matricula);
        Mes = Util.TratarString(Mes);
        Ano = Util.TratarString(Ano);
        Usuario = Util.TratarString(Usuario);
        Senha = Util.TratarString(Senha);


        if (Usuario == "IntegradorWS" && Senha == "Pontoweb2Integrador")
        {
			if (Ano == "2019")
				Ano = "0";
            DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
            PreencheTabela PT = new PreencheTabela();
            PT.GetVinculoUsuario(ds, matricula);
            byte[] retorno;
				
            string Url = "http://pontoweb.cuiaba.mt.gov.br/Relatorio/WS/frmVizualizaRelatorioWS.aspx?Mes=" + Mes + "&Setor=" + ds.TBVinculoUsuario[0].IDSetor + "&User=" + ds.TBVinculoUsuario[0].IDUsuario + "&Ano=" + Ano + "&Rel=frmZuxa&Emp=" + ds.TBVinculoUsuario[0].IDEmpresa;
            using (WebClient client = new WebClient())
            {

                retorno = client.DownloadData(Url);
                client.Dispose();
            }
            return retorno;
        }
        else
        {
            return null;
        }
    }
    //web métodos para a integração com o e-turmalina da ÁBACO.
    public class HorasDia
    {
        public string matricula
        {
            get;
            set;
        }

        public string CPF
        {
            get;
            set;
        }

        public string Hora
        {
            get;
            set;
        }

        public string CodVerba
        {
            get;
            set;
        }
        public string Data
        {
            get;
            set;
        }
        public string Situacao
        {
            get;
            set;
        }

    }

    #region Integração e-turmalina
    [WebMethod]
    public List<MetaTI.Util.Model.HorasDiaModel> ImportHoras(string Usuario, string Senha, string FiltroSituacao, int IDEmpresaOrgao_eTurmalina, int MesRef, int AnoRef)
    {
        DataSetPontoFrequencia ds;
        //FILTRO F= FALTA e H = HORAS GERAIS

        //15/06/2018 - Importando o log das operações para testar.
        if (MesRef == 1)
        {
            AnoRef = AnoRef - 1;
        }
        MesRef = MesRef - 1;
        if (Usuario == "ETURMALINAINTEGRACAO") { Usuario = "eTurmalinaIntegracao"; }
        if (Senha == "E@TURMALINA2PONTONAREDE") { Senha = "e@Turmalina2pontonarede"; }

        //Busca os dados
        Cadastro cad = new Cadastro();
        ds = cad.HorasMesAno1(Usuario, Senha, IDEmpresaOrgao_eTurmalina, MesRef, AnoRef, FiltroSituacao);

        if (ds == null)
            return null;

        //MetodosPontoFrequencia.HorasExtras.HorasExtrasDAO hed = new MetodosPontoFrequencia.HorasExtras.HorasExtrasDAO();
        MetaTI.Util.HorasExtras.HorasExtrasDAO hed = new MetaTI.Util.HorasExtras.HorasExtrasDAO();
        List<MetaTI.Util.Model.HorasDiaModel> list = hed.GetDadosWS(IDEmpresaOrgao_eTurmalina.ToString(), MesRef.ToString(), AnoRef.ToString());

        foreach (var item in list)
        {
            if (item.CodVerba == "1244" || item.CodVerba == "1235")
            {
                item.Situacao = "H";
            }
            else
            {
                item.Situacao = "F";
            }

        }

        return list;
    }

    [WebMethod]
    public DataSet RetornoHoras(string Usuario, string Senha, int IDEmpresaOrgao_eTurmalina, int MesRef, int AnoRef)
    {
        Cadastro cad = new Cadastro();
        return cad.HorasMesAno(Usuario, Senha, 0, MesRef, AnoRef);
    }
    [WebMethod]
    public bool InsertModifyEmpresaOrgao(string Usuario, string Senha, string TipoProcedimento, string NomeEmpresaOrgao, int IDEmpresaOrgao_eTurmalina, string Sigla, int StatusOrgao)
    {
        Cadastro cad = new Cadastro();
        return cad.InsertEmpresaOrgao(Usuario, Senha, TipoProcedimento, NomeEmpresaOrgao, IDEmpresaOrgao_eTurmalina, Sigla, StatusOrgao);
    }
    [WebMethod]
    public bool InsertModifySetorLotacao(string Usuario, string Senha, string TipoProcedimento, string Lotacao, string Sigla, int IDSetor_eTurmalina, int IDEmpresaOrgao_eTurmalina)
    {
        Cadastro cad = new Cadastro();
        return cad.InsertSetor(Usuario, Senha, TipoProcedimento, Lotacao, Sigla, IDSetor_eTurmalina, IDEmpresaOrgao_eTurmalina);
    }
    [WebMethod]
    public bool InsertModifyUsuarioVinculo(string Usuario, string Senha, string TipoProcedimento, string CPF
            , string Matricula, string Nome, int IDCargo_eTurmalina, int IDEmpresaOrgao_eTurmalina, int IDRegimeHora, int IDSetor_eTurmalina,
            string TelefoneSMS)
    {
        //Tratar setor, e mostrar o regime de horas. Tipo 1 = Regime de expediente, 2 p seis horas.
        Cadastro cad = new Cadastro();
        IDRegimeHora = 1; // Todos serão cadastrados como 8 horas diárias. Após, no ponto, trocar as horas.
        //IDCargo_eTurmalina = 1; //enquanto não houver os ID's dos cargos.
        return cad.InsertModifyUsuarioVinculo(Usuario, Senha, TipoProcedimento, CPF, Matricula, Nome, IDCargo_eTurmalina, IDEmpresaOrgao_eTurmalina, IDRegimeHora, IDSetor_eTurmalina, TelefoneSMS);
    }
    [WebMethod]
    public bool InsertModifyAfastamento(string Usuario, string Senha, string TipoProcedimento, string Matricula, int IDSetor_eTurmalina, int IDEmpresa_eTurmalina, int TipoAfastamento, DateTime DTInicial, DateTime DTFinal, int CodRegistroComitante)
    {
        Cadastro cad = new Cadastro();
        //Após, desativar o usuário em questão.
        if (TipoAfastamento == 80) //Para exonerados.
            return true;

        if (TipoAfastamento == 12)
            return true;


        if (TipoAfastamento == 8) //Para todos estes, afastar o usuário em questão.
            return true;

        return cad.InsertAfastamento(Usuario, Senha, Matricula, IDSetor_eTurmalina, IDEmpresa_eTurmalina,
                TipoAfastamento, 0, DTInicial, DTFinal, TipoProcedimento);
    }
    #endregion
}