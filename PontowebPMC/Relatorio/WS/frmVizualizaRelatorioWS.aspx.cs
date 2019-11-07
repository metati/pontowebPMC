using MetodosPontoFrequencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Relatorio_WS_frmVizualizaRelatorioWS : System.Web.UI.Page
{
    DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
    PreencheTabela PT = new PreencheTabela();
    Frequencia Freq = new Frequencia();
    private int TotalHoraMes, TotalDiasMes, TotalDiasCumprido, Segundo, Ano;
    private string HorasCumpridas, Hora, Minuto, HorasRealizadas, INICIO, FIM;
    public string msg;
    private string IdEmpresa = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        IdEmpresa = Request.QueryString["Emp"].ToString();
        DadosEmpresa(IdEmpresa);

        if (Convert.ToString(Request.QueryString["Rel"]) == "frmZurel" || Convert.ToString(Request.QueryString["Rel"]) == "frmZuxa")
        {
            HorasMes(Request.QueryString["User"].ToString(), Convert.ToInt32(Request.QueryString["Mes"]), Convert.ToInt32(Request.QueryString["Ano"]), Request.QueryString["Setor"]);
        }

        SetandoRel();
    }


    protected void DadosEmpresa(string IDEmpresa)
    {
        PT.PreencheTBEmpresaID(ds, Convert.ToInt32(IdEmpresa));
    }

    protected void SetandoRel()
    {
        //Report da stimulsoft

        Stimulsoft.Report.StiReport Report = new Stimulsoft.Report.StiReport();

        //Stimulsoft.Report.Web.StiWebDesigner st1 = new Stimulsoft.Report.Web.StiWebDesigner();

        if (Convert.ToString(Request.QueryString["Rel"]) == "frmZuxa")
        {
            Report.Load(Server.MapPath(@"~/Relatorio/STF/EspelhoPonto.mrt"));
            Report.Compile();
            Report["DiasMes"] = TotalDiasMes.ToString();
            Report["DiasCumpridos"] = TotalDiasCumprido.ToString();
            Report["TotalHorasMes"] = TotalHoraMes.ToString();
            Report["HorasCumpridas"] = HorasCumpridas.ToString();
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



    protected void HorasMes(string IDUsuario, int Mes, int Ano, string IDSetor)
    {
        //Adicionado rotina para impressão de folha de frequência para o setor inteiro

        //ds = null;

        if (Ano == 0 && IDUsuario != "null") // se ano Zero - Ano atual ...
        {
            Freq.HorasDias(Mes, Convert.ToInt32(IDUsuario), System.DateTime.Now.Year, ds, Convert.ToInt32(IdEmpresa), Convert.ToInt32(IDSetor));
        }
        else if (Ano != 0 && IDUsuario != "null") // se ano Zero - Ano atual ...
        {
            Freq.HorasDias(Mes, Convert.ToInt32(IDUsuario), (System.DateTime.Now.Year - 1), ds, Convert.ToInt32(IdEmpresa), Convert.ToInt32(IDSetor));
        }
        else if (Ano == 0 && IDUsuario == "null") // se ano Zero - Ano atual ...
        {
            Freq.HorasDias(Mes, 0, (System.DateTime.Now.Year), ds, Convert.ToInt32(IdEmpresa), Convert.ToInt32(IDSetor));
        }
        else if (Ano != 0 && IDUsuario == "null") // se ano Zero - Ano atual ...
        {
            Freq.HorasDias(Mes, 0, (System.DateTime.Now.Year - 1), ds, Convert.ToInt32(IdEmpresa), Convert.ToInt32(IDSetor));
        }

        //Dados sobre horas e dias no mês
        if (IDUsuario == "null")
            IDUsuario = "0";

        if (Ano == 0)
            PreenchevwBancoHoraMes(Convert.ToInt32(IdEmpresa), IDSetor, IDUsuario, Mes, System.DateTime.Now.Year);
        else
            PreenchevwBancoHoraMes(Convert.ToInt32(IdEmpresa), IDSetor, IDUsuario, Mes, System.DateTime.Now.Year - 1);

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

    protected string FormataHMS(string Valor)
    {
        if (Valor.Length == 1)
            Valor = string.Format("0{0}", Valor);

        return Valor;
    }
}