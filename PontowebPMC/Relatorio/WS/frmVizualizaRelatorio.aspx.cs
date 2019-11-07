using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Stimulsoft;
using System.IO;
using MetodosPontoFrequencia;

public partial class Relatorio_frmVizualizaRelatorio : System.Web.UI.Page
{
    DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
    Frequencia Freq = new Frequencia();
    PreencheTabela PT = new PreencheTabela();
    public string msg;
    private int TotalHoraMes, TotalDiasMes, TotalDiasCumprido, Segundo, Ano;
    private string HorasCumpridas, Hora, Minuto, HorasRealizadas, INICIO, FIM;
    private Crip Cr = new Crip();
    private string IDUSUARIO, IDSETOR;

    private int RelUtilizado;

    protected void DadosEmpresa(string IDEmpresa)
    {
        PT.PreencheTBEmpresaID(ds, Convert.ToInt32(Session["IDEmpresa"]));
    }


    protected string FormataHMS(string Valor)
    {
        if (Valor.Length == 1)
            Valor = string.Format("0{0}", Valor);

        return Valor;
    }
    protected void PreenchevwBancoHoraMes(int IDEmpresa, string IDSetor, string IDUsuario, int Mes, int Ano)
    {
        MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwBancoHoraMesTableAdapter adpBanco = new MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwBancoHoraMesTableAdapter();

        ds.EnforceConstraints = false;

        try
        {
            if (Convert.ToInt32(IDUsuario) == 0)//Se zero relatorio completo, se não passa como parâmetros para as variáveis
            {
                adpBanco.Connection.Open();
                adpBanco.FillByIDUsuarioMes(ds.vwBancoHoraMes, IDEmpresa, Convert.ToInt32(IDSetor), Mes, Ano);
                adpBanco.Connection.Close();

                if (ds.vwBancoHoraMes.Rows.Count == 0)
                {
                    msg = "Sem dados para exibir";

                    HorasCumpridas = "00:00";
                    TotalDiasCumprido = 0;
                    TotalDiasMes = 0;
                    TotalDiasCumprido = 0;
                    HorasRealizadas = "00:00";
                }
            }
            else
            {
                adpBanco.Connection.Open();
                adpBanco.FillIDMes(ds.vwBancoHoraMes, IDEmpresa, Convert.ToInt32(IDSetor), Convert.ToInt32(IDUsuario), Mes, Ano);
                adpBanco.Connection.Close();

                if (ds.vwBancoHoraMes.Rows.Count == 0)
                {
                    msg = "Sem dados para exibir";

                    HorasCumpridas = "00:00";
                    TotalDiasCumprido = 0;
                    TotalDiasMes = 0;
                    TotalDiasCumprido = 0;
                }
                else
                {
                    TotalHoraMes = ds.vwBancoHoraMes[0].HorasMes;
                    if (ds.vwBancoHoraMes[0].IsSaldoHorasNull())
                    {
                        Hora = "0";
                        HorasCumpridas = "00:00";
                    }
                    else
                    {
                        HorasCumpridas = ds.vwBancoHoraMes[0].SaldoHoras;

                        if (!ds.vwBancoHoraMes[0].IsHorasNull())
                            HorasRealizadas = string.Format("{0}:", FormataHMS(ds.vwBancoHoraMes[0].Horas.ToString()));
                        else
                            HorasRealizadas = "00:";

                        if (!ds.vwBancoHoraMes[0].IsMinutosNull())
                        {
                            HorasRealizadas += string.Format("{0}", FormataHMS(ds.vwBancoHoraMes[0].Minutos.ToString()));
                        }
                        else
                            HorasRealizadas += "00";

                        //if (!ds.vwBancoHoraMes[0].IsSegundosNull())
                        //{
                        //HorasRealizadas += string.Format("{0}", FormataHMS(ds.vwBancoHoraMes[0].Segundos.ToString()));
                        //}
                        //else
                        //HorasRealizadas += "00";
                    }

                    TotalDiasMes = ds.vwBancoHoraMes[0].DiasUteis;
                    TotalDiasCumprido = ds.vwBancoHoraMes[0].QTDRegistroMes;
                }
            }
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated == false)
        {
            Response.Redirect("~/Account/Login.aspx");
        }
        else
        {
            DadosEmpresa(Session["IDEmpresa"].ToString());

            if (Convert.ToString(Request.QueryString["Rel"]) == "frmfi")
            {
                PreencheUsuarioGestor((string)Session["IDEmpresa"], Convert.ToString(Request.QueryString["Setor"]), Convert.ToString(Request.QueryString["User"]));
            }
            if (Convert.ToString(Request.QueryString["Rel"]) == "frmdesc" || Convert.ToString(Request.QueryString["Rel"]) == "frmfaltaInjust")
            {
                try
                {
                    GetFaltaDesconto(Session["IDEmpresa"].ToString(),
                        Request.QueryString["Setor"].ToString(),
                        Request.QueryString["User"].ToString(),
                        Convert.ToDateTime(Request.QueryString["DTInicio"]),
                        Convert.ToDateTime(Request.QueryString["DTFinal"]),
                        Request.QueryString["Rel"].ToString(),
                        Request.QueryString["Situ"].ToString());
                }
                catch (Exception ex)
                {
                    INICIO = ex.Message.Trim();
                    this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> Alert('Sem dados para exibir');</script>");

                    ex.ToString();
                }
            }
            if (Convert.ToString(Request.QueryString["Rel"]) == "frmfca" && Convert.ToString(Request.QueryString["Setor"]) != "null")
            {
                PreencheUsuarioFicha(Convert.ToString(Request.QueryString["User"]), Convert.ToString(Request.QueryString["Setor"]), Convert.ToString(Request.QueryString["Situ"]));
            }
            if (Convert.ToString(Request.QueryString["Rel"]) == "frmZurel" || Convert.ToString(Request.QueryString["Rel"]) == "frmZuxa")
            {
                HorasMes(Cr.Descriptogra(Request.QueryString["User"].ToString()), Convert.ToInt32(Request.QueryString["Mes"]), Convert.ToInt32(Request.QueryString["Ano"]), Cr.Descriptogra(Request.QueryString["Setor"]));
            }
            else if (Convert.ToString(Request.QueryString["Rel"]) == "frmsitu")
            {
                PreencheVwSituacaoUsuario(Cr.Descriptogra(Request.QueryString["Setor"]));
            }
            else if (Convert.ToString(Request.QueryString["Rel"]) == "frmbco")
            {
                if (Convert.ToInt32(Request.QueryString["Ano"]) == 0)
                    Ano = System.DateTime.Now.Year;
                else
                    Ano = System.DateTime.Now.Year - 1;

                PreencheVwBancoHoras(Cr.Descriptogra(Request.QueryString["Setor"]), Convert.ToInt32(Session["IDEmpresa"]), Cr.Descriptogra(Request.QueryString["User"].ToString()), Convert.ToInt32(Request.QueryString["Mes"]), Ano);
            }
            else if (Convert.ToString(Request.QueryString["Rel"]) == "frmbcoHoraMes")
            {
                if (Convert.ToInt32(Request.QueryString["Ano"]) == 0)
                    Ano = System.DateTime.Now.Year;
                else
                    Ano = System.DateTime.Now.Year - 1;

                PreenchevwBancoHoraMes(Convert.ToInt32(Session["IDEmpresa"]), Cr.Descriptogra(Request.QueryString["Setor"]), "0", Convert.ToInt32(Request.QueryString["Mes"]), Ano);
            }
            else if (Convert.ToString(Request.QueryString["Rel"]) == "frmRelacDia")
            {
                string ll = Request.QueryString["Ausencia"].ToString().Trim();
                if (ll == "null")
                    ll = "false";
                else
                    ll = "True";
                PreenchevwHorasDiaDia(Cr.Descriptogra(Request.QueryString["Setor"].ToString()), Convert.ToString(Request.QueryString["DiaIni"]), Request.QueryString["DiaFim"].ToString(), Request.QueryString["User"].ToString(), Convert.ToBoolean(ll));
            }
            else if (Convert.ToString(Request.QueryString["Rel"]) == "frmLocalRegistro")
            {
                LocalRegistor(Cr.Descriptogra(Request.QueryString["User"].ToString()), Cr.Descriptogra(Request.QueryString["Setor"].ToString()), Convert.ToString(Request.QueryString["DataIni"]), Convert.ToString(Request.QueryString["DataFim"]), Convert.ToString(Request.QueryString["FiltroManual"]));
            }
            else if (Convert.ToString(Request.QueryString["Rel"]) == "frmMotivoFalta")
            {
                RelacaoMotivoFalta(Cr.Descriptogra(Request.QueryString["Setor"].ToString()), Convert.ToString(Request.QueryString["Dia"]), Convert.ToString(Request.QueryString["DiaFinal"]), Convert.ToString(Request.QueryString["MotivoFalta"]));
            }
            else if ((Convert.ToString(Request.QueryString["Rel"]) == "frmMotivoFalta"))
            {

            }
            else if ((Convert.ToString(Request.QueryString["Rel"]) == "frmAuditRel"))
            {
                GetAuditoria(Request.QueryString["DiaIni"].ToString(), Request.QueryString["DiaFim"].ToString(), Session["IDEmpresa"].ToString(), Request.QueryString["User"].ToString());
            }
            else if ((Convert.ToString(Request.QueryString["Rel"]) == "frmLogMaqRel"))
            {
                GetLogMaquinas(Request.QueryString["DiaIni"].ToString(), Request.QueryString["DiaFim"].ToString(), Request.QueryString["Emp"].ToString(), Request.QueryString["IDSetor"].ToString());
            }
            else if ((Convert.ToString(Request.QueryString["Rel"]) == "frmInfoMaqRel"))
            {
                GetInfoMaquinas(Request.QueryString["DiaIni"].ToString(), Request.QueryString["DiaFim"].ToString(), Request.QueryString["Emp"].ToString(), Request.QueryString["IDSetor"].ToString());
            }

            SetandoRel();
        }
    }

    protected void GetAuditoria(string Inicio, string Fim, string IDEmpresa, string Nome)
    {
        PT.GetAuditoria(ds, Convert.ToDateTime(Inicio), Convert.ToDateTime(Fim), Nome, Convert.ToInt32(IDEmpresa));
        INICIO = Inicio;
        FIM = Fim;
    }

    protected void GetInfoMaquinas(string Inicio, string Fim, string IDEmpresa, string IDSetor)
    {
        PT.GetInfoMaquinas(ds);
        INICIO = Inicio;
        FIM = Fim;
    }


    protected void GetLogMaquinas(string Inicio, string Fim, string IDEmpresa, string IDSetor)
    {
        PT.GetLogsMaquinas(ds);
        INICIO = Inicio;
        FIM = Fim;
    }

    protected void PreencheUsuarioGestor(string IDEmpresa, string IDSetor, string IDVinculoUSuario)
    {
        if (IDVinculoUSuario != string.Empty)
        {
            PT.PreenchevwUsuarioGestao(ds, Convert.ToInt32(IDEmpresa), Convert.ToInt32(IDSetor), Convert.ToInt64(IDVinculoUSuario));
        }
        if (IDSetor != string.Empty && IDVinculoUSuario == string.Empty)
        {
            PT.PreenchevwUsuarioGestao(ds, Convert.ToInt32(IDEmpresa), Convert.ToInt32(IDSetor));
        }
        else
        {
            PT.PreenchevwUsuarioGestao(ds, Convert.ToInt32(IDEmpresa));
        }
    }

    protected void PreencheUsuarioFicha(string IDUsuario, string IDSetor, string FiltroSituacao)
    {
        IDSetor = Cr.Descriptogra(IDSetor);
        IDUsuario = Cr.Descriptogra(IDUsuario);

        PT.PreenchevwUsuarioGrid(ds, IDSetor, IDUsuario, Convert.ToInt32(FiltroSituacao));
    }
    protected void GetFaltaDesconto(string IDEmpresa, string IDSetor, string IDVinculoUsuario, DateTime Inicio, DateTime Fim, string Relatorio, string FiltroDesconto)
    {
        IDSetor = Cr.Descriptogra(IDSetor);
        ds.EnforceConstraints = false;

        INICIO = Inicio.ToShortDateString();
        FIM = Fim.ToShortDateString();

        switch (Relatorio)
        {
            case "frmfaltaInjust":
                switch (FiltroDesconto)
                {
                    case "0":
                        RelUtilizado = 0;
                        if (IDEmpresa != string.Empty && IDSetor == string.Empty && IDVinculoUsuario == string.Empty)
                        {
                            PT.PreenchevwFaltaInjustificada(ds, Convert.ToInt32(IDEmpresa), Inicio, Fim);
                        }
                        else if (IDEmpresa != string.Empty && IDSetor != string.Empty && IDVinculoUsuario == string.Empty)
                        {
                            PT.PreenchevwFaltaInjustificada(ds, Convert.ToInt32(IDEmpresa), Convert.ToInt32(IDSetor), Inicio, Fim);
                        }
                        else if (IDEmpresa != string.Empty && IDSetor != string.Empty && IDVinculoUsuario != string.Empty)
                        {
                            IDVinculoUsuario = Cr.Descriptogra(IDVinculoUsuario); //descriptografa
                                                                                  //pega o ID do vínculo!
                            IDVinculoUsuario = PT.RetornaIDVinculoUsuario(Convert.ToInt32(IDEmpresa), Convert.ToInt32(IDSetor), Convert.ToInt32(IDVinculoUsuario)).ToString();
                            PT.PreenchevwFaltaInjustificada(ds, Convert.ToInt32(IDEmpresa), Convert.ToInt32(IDSetor), Convert.ToInt32(IDVinculoUsuario), Inicio, Fim);
                        }
                        break;
                    case "1":
                        RelUtilizado = 1;
                        if (IDEmpresa != string.Empty && IDSetor == string.Empty && IDVinculoUsuario == string.Empty)
                        {
                            PT.PreenchevwRegistroAusente(ds, Convert.ToInt32(IDEmpresa), Inicio, Fim);
                        }
                        else if (IDEmpresa != string.Empty && IDSetor != string.Empty && IDVinculoUsuario == string.Empty)
                        {
                            PT.PreenchevwRegistroAusente(ds, Convert.ToInt32(IDEmpresa), Convert.ToInt32(IDSetor), Inicio, Fim);
                        }
                        else if (IDEmpresa != string.Empty && IDSetor != string.Empty && IDVinculoUsuario != string.Empty)
                        {
                            PT.PreenchevwRegistroAusente(ds, Convert.ToInt32(IDEmpresa), Convert.ToInt32(IDSetor), Convert.ToInt32(IDVinculoUsuario), Inicio, Fim);
                        }
                        break;
                }
                break;
            case "frmdesc":
                if (IDEmpresa != string.Empty && IDSetor == string.Empty && IDVinculoUsuario == string.Empty)
                {
                    switch (FiltroDesconto)
                    {
                        case "0":
                            PT.PreenchevwDesconto(ds, Convert.ToInt32(IDEmpresa), Inicio, Fim);
                            break;
                        case "1":
                            PT.PreenchevwDescontoFiltro(ds, Convert.ToInt32(IDEmpresa), Inicio, Fim, "D13");
                            break;
                        case "2":
                            PT.PreenchevwDescontoFiltro(ds, Convert.ToInt32(IDEmpresa), Inicio, Fim, "DI");
                            break;
                    }

                }
                else if (IDEmpresa != string.Empty && IDSetor != string.Empty && IDVinculoUsuario == string.Empty)
                {
                    switch (FiltroDesconto)
                    {
                        case "0":
                            PT.PreenchevwDesconto(ds, Convert.ToInt32(IDEmpresa), Convert.ToInt32(IDSetor), Inicio, Fim);
                            break;
                        case "1":
                            PT.PreenchevwDescontoFiltro(ds, Convert.ToInt32(IDEmpresa), Convert.ToInt32(IDSetor), Inicio, Fim, "D13");
                            break;
                        case "3":
                            PT.PreenchevwDescontoFiltro(ds, Convert.ToInt32(IDEmpresa), Convert.ToInt32(IDSetor), Inicio, Fim, "DI");
                            break;
                    }

                }
                else if (IDEmpresa != string.Empty && IDSetor != string.Empty && IDVinculoUsuario != string.Empty)
                {
                    IDVinculoUsuario = Cr.Descriptogra(IDVinculoUsuario); //descriptografa
                                                                          //pega o ID do vínculo!
                    IDVinculoUsuario = PT.RetornaIDVinculoUsuario(Convert.ToInt32(IDEmpresa), Convert.ToInt32(IDSetor), Convert.ToInt32(IDVinculoUsuario)).ToString();
                    switch (FiltroDesconto)
                    {
                        case "0":
                            PT.PreenchevwDesconto(ds, Convert.ToInt32(IDEmpresa), Convert.ToInt32(IDSetor), Convert.ToInt32(IDVinculoUsuario), Inicio, Fim);
                            break;
                        case "1":
                            PT.PreenchevwDescontoFiltro(ds, Convert.ToInt32(IDEmpresa), Convert.ToInt32(IDSetor), Convert.ToInt32(IDVinculoUsuario), Inicio, Fim, "D13");
                            break;
                        case "2":
                            PT.PreenchevwDescontoFiltro(ds, Convert.ToInt32(IDEmpresa), Convert.ToInt32(IDSetor), Convert.ToInt32(IDVinculoUsuario), Inicio, Fim, "DI");
                            break;
                    }

                }
                break;
        }
    }
    protected void RelacaoFaltaInjustificadaDesconto(string IDEmpresa, string IDSetor, string IDVinculoUsuario, DateTime Inicio, DateTime Fim, string Relatorio, string FiltroDesconto)
    {
        IDSetor = Cr.Descriptogra(IDSetor);
        ds.EnforceConstraints = false;

        INICIO = Inicio.ToShortDateString();
        FIM = Fim.ToShortDateString();

        switch (Relatorio)
        {
            case "frmfaltaInjust":
                switch (FiltroDesconto)
                {
                    case "0":
                        RelUtilizado = 0;
                        if (IDEmpresa != string.Empty && IDSetor == string.Empty && IDVinculoUsuario == string.Empty)
                        {
                            PT.PreenchevwFaltaInjustificada(ds, Convert.ToInt32(IDEmpresa), Inicio, Fim);
                        }
                        else if (IDEmpresa != string.Empty && IDSetor != string.Empty && IDVinculoUsuario == string.Empty)
                        {
                            PT.PreenchevwFaltaInjustificada(ds, Convert.ToInt32(IDEmpresa), Convert.ToInt32(IDSetor), Inicio, Fim);
                        }
                        else if (IDEmpresa != string.Empty && IDSetor != string.Empty && IDVinculoUsuario != string.Empty)
                        {
                            IDVinculoUsuario = Cr.Descriptogra(IDVinculoUsuario); //descriptografa
                                                                                  //pega o ID do vínculo!
                            IDVinculoUsuario = PT.RetornaIDVinculoUsuario(Convert.ToInt32(IDEmpresa), Convert.ToInt32(IDSetor), Convert.ToInt32(IDVinculoUsuario)).ToString();
                            PT.PreenchevwFaltaInjustificada(ds, Convert.ToInt32(IDEmpresa), Convert.ToInt32(IDSetor), Convert.ToInt32(IDVinculoUsuario), Inicio, Fim);
                        }
                        break;
                    case "1":
                        RelUtilizado = 1;
                        if (IDEmpresa != string.Empty && IDSetor == string.Empty && IDVinculoUsuario == string.Empty)
                        {
                            PT.PreenchevwRegistroAusente(ds, Convert.ToInt32(IDEmpresa), Inicio, Fim);
                        }
                        else if (IDEmpresa != string.Empty && IDSetor != string.Empty && IDVinculoUsuario == string.Empty)
                        {
                            PT.PreenchevwRegistroAusente(ds, Convert.ToInt32(IDEmpresa), Convert.ToInt32(IDSetor), Inicio, Fim);
                        }
                        else if (IDEmpresa != string.Empty && IDSetor != string.Empty && IDVinculoUsuario != string.Empty)
                        {
                            PT.PreenchevwRegistroAusente(ds, Convert.ToInt32(IDEmpresa), Convert.ToInt32(IDSetor), Convert.ToInt32(IDVinculoUsuario), Inicio, Fim);
                        }
                        break;
                }
                break;
            case "frmdesc":
                if (IDEmpresa != string.Empty && IDSetor == string.Empty && IDVinculoUsuario == string.Empty)
                {
                    switch (FiltroDesconto)
                    {
                        case "0":
                            PT.PreenchevwDesconto(ds, Convert.ToInt32(IDEmpresa), Inicio, Fim);
                            break;
                        case "1":
                            PT.PreenchevwDescontoFiltro(ds, Convert.ToInt32(IDEmpresa), Inicio, Fim, "D13");
                            break;
                        case "2":
                            PT.PreenchevwDescontoFiltro(ds, Convert.ToInt32(IDEmpresa), Inicio, Fim, "DI");
                            break;
                    }

                }
                else if (IDEmpresa != string.Empty && IDSetor != string.Empty && IDVinculoUsuario == string.Empty)
                {
                    switch (FiltroDesconto)
                    {
                        case "0":
                            PT.PreenchevwDesconto(ds, Convert.ToInt32(IDEmpresa), Convert.ToInt32(IDSetor), Inicio, Fim);
                            break;
                        case "1":
                            PT.PreenchevwDescontoFiltro(ds, Convert.ToInt32(IDEmpresa), Convert.ToInt32(IDSetor), Inicio, Fim, "D13");
                            break;
                        case "3":
                            PT.PreenchevwDescontoFiltro(ds, Convert.ToInt32(IDEmpresa), Convert.ToInt32(IDSetor), Inicio, Fim, "DI");
                            break;
                    }

                }
                else if (IDEmpresa != string.Empty && IDSetor != string.Empty && IDVinculoUsuario != string.Empty)
                {
                    IDVinculoUsuario = Cr.Descriptogra(IDVinculoUsuario); //descriptografa
                                                                          //pega o ID do vínculo!
                    IDVinculoUsuario = PT.RetornaIDVinculoUsuario(Convert.ToInt32(IDEmpresa), Convert.ToInt32(IDSetor), Convert.ToInt32(IDVinculoUsuario)).ToString();
                    switch (FiltroDesconto)
                    {
                        case "0":
                            PT.PreenchevwDesconto(ds, Convert.ToInt32(IDEmpresa), Convert.ToInt32(IDSetor), Convert.ToInt32(IDVinculoUsuario), Inicio, Fim);
                            break;
                        case "1":
                            PT.PreenchevwDescontoFiltro(ds, Convert.ToInt32(IDEmpresa), Convert.ToInt32(IDSetor), Convert.ToInt32(IDVinculoUsuario), Inicio, Fim, "D13");
                            break;
                        case "2":
                            PT.PreenchevwDescontoFiltro(ds, Convert.ToInt32(IDEmpresa), Convert.ToInt32(IDSetor), Convert.ToInt32(IDVinculoUsuario), Inicio, Fim, "DI");
                            break;
                    }

                }
                break;
        }
    }
    protected void RelacaoPerfisOrgao(string IDEmpresa, string IDSetor, string IDVinculoUsuario)
    {
        IDSetor = Cr.Descriptogra(IDSetor);

        if (IDEmpresa != string.Empty && IDSetor == string.Empty && IDVinculoUsuario == string.Empty)
        {
            PT.PreenchevwUsuarioGestao(ds, Convert.ToInt32(IDEmpresa));
        }
        else if (IDEmpresa != string.Empty && IDSetor != string.Empty && IDVinculoUsuario == string.Empty)
        {
            PT.PreenchevwUsuarioGestao(ds, Convert.ToInt32(IDEmpresa), Convert.ToInt32(IDSetor));
        }
        else if (IDEmpresa != string.Empty && IDSetor != string.Empty && IDVinculoUsuario != string.Empty)
        {
            IDVinculoUsuario = Cr.Descriptogra(IDVinculoUsuario); //descriptografa
                                                                  //pega o ID do vínculo!
            IDVinculoUsuario = PT.RetornaIDVinculoUsuario(Convert.ToInt32(IDEmpresa), Convert.ToInt32(IDSetor), Convert.ToInt32(IDVinculoUsuario)).ToString();
            PT.PreenchevwUsuarioGestao(ds, Convert.ToInt32(IDEmpresa), Convert.ToInt32(IDSetor), Convert.ToInt32(IDVinculoUsuario));
        }
    }
    protected void RelacaoMotivoFalta(string IDSetor, string Inicio, string Fim, string IDMotivoFalta)
    {
        ds.EnforceConstraints = false;

        INICIO = Inicio;
        FIM = Fim;

        if (Inicio.Length == 9)
        {
            if (Inicio.Substring(0, 2).IndexOf("/") == 1)
                Inicio = string.Format("0{0}", Inicio);
            else
            {
                Inicio = string.Format("{0}0{1}", Inicio.Substring(0, 3), Inicio.Substring(3, 6));
            }
        }
        else if (Inicio.Length == 8)
        {
            Inicio = string.Format("0{0}", Inicio);
            Inicio = string.Format("{0}0{1}", Inicio.Substring(0, 3), Inicio.Substring(3, 6));
        }

        if (Fim.Length == 9)
        {
            if (Fim.Substring(0, 2).IndexOf("/") == 1)
                Fim = string.Format("0{0}", Fim);
            else
            {
                Fim = string.Format("{0}0{1}", Fim.Substring(0, 3), Fim.Substring(3, 6));
            }
        }
        else if (Fim.Length == 8)
        {
            Fim = string.Format("0{0}", Fim);
            Fim = string.Format("{0}0{1}", Fim.Substring(0, 3), Fim.Substring(3, 6));
        }

        //Chamando os dados
        MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwDiasMotivosFaltaDescricaoTableAdapter adpMotivo = new MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwDiasMotivosFaltaDescricaoTableAdapter();
        MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwDiasMotivosFaltaTotalTableAdapter adpMotivoTotal = new MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwDiasMotivosFaltaTotalTableAdapter();
        try
        {
            adpMotivo.Connection.Open();
            if (IDMotivoFalta == "null")
            {
                adpMotivo.FillPeriodoSetor(ds.vwDiasMotivosFaltaDescricao, Convert.ToInt32(IDSetor), Convert.ToDateTime(Inicio), Convert.ToDateTime(Fim));
                adpMotivoTotal.Connection.Open();
                adpMotivoTotal.FillSetorData(ds.vwDiasMotivosFaltaTotal, Convert.ToInt32(IDSetor), Convert.ToDateTime(Inicio), Convert.ToDateTime(Fim));
                adpMotivoTotal.Connection.Close();
            }
            else
            {
                adpMotivo.FillSetorDataMotivoFalta(ds.vwDiasMotivosFaltaDescricao, Convert.ToInt32(IDSetor), Convert.ToDateTime(Inicio), Convert.ToDateTime(Fim), Convert.ToInt32(IDMotivoFalta));
            }

            adpMotivo.Connection.Close();
        }
        catch (Exception ex)
        {
        }
    }
    protected void LocalRegistor(string IDUsaurio, string IDSetor, string DtLocalRegistro, string DtLocalRegistroFim, string localregistro)
    {
        ds.EnforceConstraints = false;

        INICIO = DtLocalRegistro;
        FIM = DtLocalRegistroFim;

        //Acrescentar um dia na data fim, para trazer o intervalo de datas
        DtLocalRegistroFim = Convert.ToDateTime(DtLocalRegistroFim).AddDays(1).ToShortDateString();

        if (DtLocalRegistro.Length == 9)
        {
            if (DtLocalRegistro.Substring(0, 2).IndexOf("/") == 1)
                DtLocalRegistro = string.Format("0{0}", DtLocalRegistro);
            else
            {
                DtLocalRegistro = string.Format("{0}0{1}", DtLocalRegistro.Substring(0, 3), DtLocalRegistro.Substring(3, 6));
            }
        }
        else if (DtLocalRegistro.Length == 8)
        {
            DtLocalRegistro = string.Format("0{0}", DtLocalRegistro);
            DtLocalRegistro = string.Format("{0}0{1}", DtLocalRegistro.Substring(0, 3), DtLocalRegistro.Substring(3, 6));
        }

        if (DtLocalRegistroFim.Length == 9)
        {
            if (DtLocalRegistroFim.Substring(0, 2).IndexOf("/") == 1)
                DtLocalRegistroFim = string.Format("0{0}", DtLocalRegistroFim);
            else
            {
                DtLocalRegistroFim = string.Format("{0}0{1}", DtLocalRegistroFim.Substring(0, 3), DtLocalRegistroFim.Substring(3, 6));
            }
        }
        else if (DtLocalRegistroFim.Length == 8)
        {
            DtLocalRegistroFim = string.Format("0{0}", DtLocalRegistroFim);
            DtLocalRegistroFim = string.Format("{0}0{1}", DtLocalRegistroFim.Substring(0, 3), DtLocalRegistroFim.Substring(3, 6));
        }

        MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwLocalRegistroTableAdapter adpLocalRegistro = new MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwLocalRegistroTableAdapter();

        adpLocalRegistro.Connection.Open();
        if (IDUsaurio != "0" && localregistro == "false")
        {
            adpLocalRegistro.FillIDSetorDTLocalUser(ds.vwLocalRegistro, Convert.ToInt32(IDSetor), Convert.ToDateTime(DtLocalRegistro).Date, Convert.ToDateTime(DtLocalRegistroFim).Date, Convert.ToInt32(IDUsaurio));
        }
        else if (IDUsaurio == "0" && localregistro == "false")
            adpLocalRegistro.FillIDSetorData(ds.vwLocalRegistro, Convert.ToInt32(IDSetor), Convert.ToInt32(Session["IDEmpresa"]), Convert.ToDateTime(DtLocalRegistro).Date, Convert.ToDateTime(DtLocalRegistroFim).Date);
        else if (IDUsaurio != "0" && localregistro == "true")
        {
            adpLocalRegistro.FillIDSetorUserRegistroManual(ds.vwLocalRegistro, Convert.ToInt32(IDSetor), Convert.ToDateTime(DtLocalRegistro).Date, Convert.ToDateTime(DtLocalRegistroFim).Date, Convert.ToInt32(IDUsaurio));
        }
        else if (IDUsaurio == "0" && localregistro == "true")
        {
            adpLocalRegistro.FillIDSetorDataRegistroManual(ds.vwLocalRegistro, Convert.ToInt32(IDSetor), Convert.ToDateTime(DtLocalRegistro).Date, Convert.ToDateTime(DtLocalRegistroFim).Date);
        }

        adpLocalRegistro.Connection.Close();
    }
    protected void PreenchevwHorasDiaDia(string IDSetor, string DTFrequenciaInicial, string DTFrequenciaFinal, string User, bool Ausencia)
    {
        User = Cr.Descriptogra(User);

        MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vWHorasDiaTableAdapter adpHorasDia = new MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vWHorasDiaTableAdapter();

        if (DTFrequenciaInicial.Length == 9)
        {
            if (DTFrequenciaInicial.Substring(0, 2).IndexOf("/") == 1)
                DTFrequenciaInicial = string.Format("0{0}", DTFrequenciaInicial);
            else
            {
                DTFrequenciaInicial = string.Format("{0}0{1}", DTFrequenciaInicial.Substring(0, 3), DTFrequenciaInicial.Substring(3, 6));
            }
        }
        else if (DTFrequenciaInicial.Length == 8)
        {
            DTFrequenciaInicial = string.Format("0{0}", DTFrequenciaInicial);
            DTFrequenciaInicial = string.Format("{0}0{1}", DTFrequenciaInicial.Substring(0, 3), DTFrequenciaInicial.Substring(3, 6));
        }


        if (DTFrequenciaFinal.Length == 9)
        {
            if (DTFrequenciaFinal.Substring(0, 2).IndexOf("/") == 1)
                DTFrequenciaFinal = string.Format("0{0}", DTFrequenciaFinal);
            else
            {
                DTFrequenciaFinal = string.Format("{0}0{1}", DTFrequenciaFinal.Substring(0, 3), DTFrequenciaFinal.Substring(3, 6));
            }
        }
        else if (DTFrequenciaFinal.Length == 8)
        {
            DTFrequenciaFinal = string.Format("0{0}", DTFrequenciaFinal);
            DTFrequenciaFinal = string.Format("{0}0{1}", DTFrequenciaFinal.Substring(0, 3), DTFrequenciaFinal.Substring(3, 6));
        }

        INICIO = DTFrequenciaInicial;
        FIM = DTFrequenciaFinal;

        try
        {
            ds.EnforceConstraints = false;

            //09-09-2015 - Adicionei o campo ausência para filtrar apenas as ausências.

            adpHorasDia.Connection.Open();
            if (User == "0" && !Ausencia)
                adpHorasDia.FillFreqDia(ds.vWHorasDia, Convert.ToInt32(Session["IDEmpresa"]), Convert.ToInt32(IDSetor), Convert.ToDateTime(DTFrequenciaInicial), Convert.ToDateTime(DTFrequenciaFinal));
            else if (User == "0" && Ausencia)
                adpHorasDia.FillFreqDiaSituacao(ds.vWHorasDia, Convert.ToInt32(Session["IDEmpresa"]), Convert.ToInt32(IDSetor), Convert.ToDateTime(DTFrequenciaInicial), Convert.ToDateTime(DTFrequenciaFinal), 3);
            else if (User != "0" && !Ausencia)
                adpHorasDia.FillFreqDiaUser(ds.vWHorasDia, Convert.ToInt32(Session["IDEmpresa"]), Convert.ToInt32(IDSetor), Convert.ToDateTime(DTFrequenciaInicial), Convert.ToDateTime(DTFrequenciaFinal), Convert.ToInt32(User));
            else if (User != "0" && Ausencia)
                adpHorasDia.FillFreqDiaUserSituacao(ds.vWHorasDia, Convert.ToInt32(Session["IDEmpresa"]), Convert.ToInt32(IDSetor), Convert.ToDateTime(DTFrequenciaInicial), Convert.ToDateTime(DTFrequenciaFinal), Convert.ToInt32(User), 3);

            adpHorasDia.Connection.Close();
        }
        catch (Exception ex)
        {
            Label1.Text = "Houve Falha ao gerar relatório. Contate o administrador.";
        }
    }

    protected void PreencheVwBancoHoras(string IDsetor, int IDEmpresa, string iDusuario, int MesRef, int AnoRef)
    {
        MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwBancoHoraTableAdapter adpBancohora = new MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwBancoHoraTableAdapter();
        try
        {
            ds.EnforceConstraints = false;
            adpBancohora.Connection.Open();
            adpBancohora.FillIDEmpresaSetor(ds.vwBancoHora, IDEmpresa, Convert.ToInt32(IDsetor), Convert.ToInt32(iDusuario), MesRef, AnoRef);
            adpBancohora.Connection.Close();
        }
        catch (Exception ex)
        {
            Label1.Text = "Houve Falha ao gerar relatório. Contate o administrador.";
        }
    }

    protected void PreencheVwSituacaoUsuario(string IDSetor)
    {
        MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwSituacaoUsuarioTableAdapter adpSituacao = new MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwSituacaoUsuarioTableAdapter();
        try
        {
            adpSituacao.Connection.Open();

            if (IDSetor != "0")
            {
                adpSituacao.FillIDSetor(ds.vwSituacaoUsuario, Convert.ToInt32(IDSetor));
            }
            else
            {
                adpSituacao.FillIDEmpresa(ds.vwSituacaoUsuario, Convert.ToInt32(Session["IDEmpresa"]));
            }

            adpSituacao.Connection.Close();
        }
        catch (Exception ex)
        {
            Label1.Text = "Houve Falha ao gerar relatório. Contate o administrador.";
        }
    }
    protected void HorasMes(string IDUsuario, int Mes, int Ano, string IDSetor)
    {
        //Adicionado rotina para impressão de folha de frequência para o setor inteiro

        //ds = null;

        if (Ano == 0 && IDUsuario != "null") // se ano Zero - Ano atual ...
        {
            Freq.HorasDias(Mes, Convert.ToInt32(IDUsuario), System.DateTime.Now.Year, ds, Convert.ToInt32(Session["IDEmpresa"]), Convert.ToInt32(IDSetor));
        }
        else if (Ano != 0 && IDUsuario != "null") // se ano Zero - Ano atual ...
        {
            Freq.HorasDias(Mes, Convert.ToInt32(IDUsuario), (System.DateTime.Now.Year - 1), ds, Convert.ToInt32(Session["IDEmpresa"]), Convert.ToInt32(IDSetor));
        }
        else if (Ano == 0 && IDUsuario == "null") // se ano Zero - Ano atual ...
        {
            Freq.HorasDias(Mes, 0, (System.DateTime.Now.Year), ds, Convert.ToInt32(Session["IDEmpresa"]), Convert.ToInt32(IDSetor));
        }
        else if (Ano != 0 && IDUsuario == "null") // se ano Zero - Ano atual ...
        {
            Freq.HorasDias(Mes, 0, (System.DateTime.Now.Year - 1), ds, Convert.ToInt32(Session["IDEmpresa"]), Convert.ToInt32(IDSetor));
        }

        //Dados sobre horas e dias no mês
        if (IDUsuario == "null")
            IDUsuario = "0";

        if (Ano == 0)
            PreenchevwBancoHoraMes(Convert.ToInt32(Session["IDEmpresa"]), IDSetor, IDUsuario, Mes, System.DateTime.Now.Year);
        else
            PreenchevwBancoHoraMes(Convert.ToInt32(Session["IDEmpresa"]), IDSetor, IDUsuario, Mes, System.DateTime.Now.Year - 1);

        if (msg == "1")
        {
            Label1.Text = "Sem dados para Exibir";
        }
        else
        {
            Label1.Visible = false;
            btFechar.Visible = false;
        }
    }

    protected void SetandoRel()
    {
        //Report da stimulsoft

        Stimulsoft.Report.StiReport Report = new Stimulsoft.Report.StiReport();

        //Stimulsoft.Report.Web.StiWebDesigner st1 = new Stimulsoft.Report.Web.StiWebDesigner();

        if (Convert.ToString(Request.QueryString["Rel"]) == "frmZurel")
        {
            Report.Load(Server.MapPath(@"~/Relatorio/STF/folhafrequencia.mrt"));
            Report.Compile();
            Report["DiasMes"] = TotalDiasMes.ToString();
            Report["DiasCumpridos"] = TotalDiasCumprido.ToString();
            Report["TotalHorasMes"] = TotalHoraMes.ToString();
            Report["HorasCumpridas"] = HorasCumpridas;
            Report["HorasRealizadas"] = HorasRealizadas;
        }
        else if (Convert.ToString(Request.QueryString["Rel"]) == "frmZuxa")
        {
            Report.Load(Server.MapPath(@"~/Relatorio/STF/EspelhoPonto.mrt"));
            Report.Compile();
            Report["DiasMes"] = TotalDiasMes.ToString();
            Report["DiasCumpridos"] = TotalDiasCumprido.ToString();
            Report["TotalHorasMes"] = TotalHoraMes.ToString();
            Report["HorasCumpridas"] = HorasCumpridas.ToString();
        }
        else if (Convert.ToString(Request.QueryString["Rel"]) == "frmsitu")
            Report.Load(Server.MapPath(@"~/Relatorio/STF/situacaousuario.mrt"));
        else if (Convert.ToString(Request.QueryString["Rel"]) == "frmbco")
        {
            Report.Load(Server.MapPath(@"~/Relatorio/STF/totalhorasdiarias.mrt"));
            Report.Compile();
            //Report["Teste"] = 932;
        }
        else if (Convert.ToString(Request.QueryString["Rel"]) == "frmbcoHoraMes")
        {
            Report.Load(Server.MapPath(@"~/Relatorio/STF/bancoHoraMes.mrt"));
        }
        else if (Convert.ToString(Request.QueryString["Rel"]) == "frmRelacDia")
        {
            Report.Load(Server.MapPath(@"~/Relatorio/STF/relacaopontoDia.mrt"));
            Report.Compile();
            Report["DTInicio"] = INICIO;
            Report["DTFim"] = FIM;
        }
        else if (Convert.ToString(Request.QueryString["Rel"]) == "frmLocalRegistro")
        {
            Report.Load(Server.MapPath(@"~/Relatorio/STF/LocalRegistro.mrt"));
            Report.Compile();
            Report["DTInicio"] = INICIO;
            Report["DTFim"] = FIM;

        }
        else if (Convert.ToString(Request.QueryString["Rel"]) == "frmMotivoFalta")
        {
            Report.Load(Server.MapPath(@"~/Relatorio/STF/RelacaoMotivoFalta.mrt"));
            Report.Compile();
            Report["DTInicio"] = INICIO;
            Report["DTFim"] = FIM;
        }
        else if (Convert.ToString(Request.QueryString["Rel"]) == "frmfca")
        {
            Report.Load(Server.MapPath(@"~/Relatorio/STF/FichaCadastral.mrt"));
            Report.Compile();
        }
        else if (Convert.ToString(Request.QueryString["Rel"]) == "frmfi")
        {
            Report.Load(Server.MapPath(@"~/Relatorio/STF/FichaCadastral.mrt"));
            Report.Compile();
        }
        else if (Convert.ToString(Request.QueryString["Rel"]) == "frmdesc")
        {
            Report.Load(Server.MapPath(@"~/Relatorio/STF/RelRelacaoRegistroPeriodo.mrt"));
            Report.Compile();
            Report["DTInicio"] = INICIO;
            Report["DTFim"] = FIM;
        }
        else if (Convert.ToString(Request.QueryString["Rel"]) == "frmfaltaInjust")
        {
            if (RelUtilizado == 0)
            {
                Report.Load(Server.MapPath(@"~/Relatorio/STF/RelDataInjustificada.mrt"));
            }
            else
            {
                Report.Load(Server.MapPath(@"~/Relatorio/STF/RelRegistroAusente.mrt"));
            }
            //o Outro Aqui
            Report.Compile();
            Report["DTInicio"] = INICIO;
            Report["DTFim"] = FIM;
        }
        else if (Convert.ToString(Request.QueryString["Rel"]) == "frmAuditRel")
        {
            Report.Load(Server.MapPath(@"~/Relatorio/STF/RelatorioAuditoriaJustificativa.mrt"));
            Report.Compile();
            Report["DTInicio"] = INICIO;
            Report["DTFim"] = FIM;
        }
        else if (Convert.ToString(Request.QueryString["Rel"]) == "frmLogMaqRel")
        {
            Report.Load(Server.MapPath(@"~/Relatorio/STF/AuditoriaGeralDasConexoes.mrt"));
            Report.Compile();
        }
        else if (Convert.ToString(Request.QueryString["Rel"]) == "frmInfoMaqRel")
        {
            Report.Load(Server.MapPath(@"~/Relatorio/STF/InformacoesMaquinasColetorLocal.mrt"));
            Report.Compile();
        }

        Report.RegData("DatasetPontoFrequencia", ds);

        Report.Render();

        System.IO.MemoryStream memStream = new System.IO.MemoryStream();

        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.ContentType = "application/pdf";

        HttpContext.Current.Response.AddHeader("Pontoweb.pdf", "");

        Stimulsoft.Report.Export.StiPdfExportService Export = new Stimulsoft.Report.Export.StiPdfExportService();

        Export.ExportPdf(Report, memStream, Stimulsoft.Report.StiPagesRange.All, 100, 100, false, false, true, true,
            "", "", Stimulsoft.Report.Export.StiUserAccessPrivileges.All, Stimulsoft.Report.Export.StiPdfEncryptionKeyLength.Bit40, false);

        Response.ContentType = "application/pdf";

        Response.AddHeader("content-disposition", "inline; filename=Pontoweb.pdf");

        Response.BinaryWrite(memStream.ToArray());
    }

}