using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using MetodosPontoFrequencia;

public partial class Relatorio_frmRelHorasDia : System.Web.UI.Page
{
    DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
    Frequencia Freq = new Frequencia();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated == false)
        {
            Response.Redirect("~/Account/Login.aspx");
        }
        else
        {
            if (Request.QueryString["Mes"] != null)
                HorasMes(Convert.ToInt32(Session["IDUsuario"]), Convert.ToInt32(Request.QueryString["Mes"]), Convert.ToInt32(Session["AnoFolhaFrequencia"]));
        }
    }

    protected void HorasMes(int IDUsuario, int Mes, int Ano)
    {
            Freq.HorasDias(Mes, IDUsuario, System.DateTime.Now.Year, ds, Convert.ToInt32(Session["IDEmpresa"]),Convert.ToInt32(Session["IDSETOR"]));

            Freq.TotalHorasMesUsuario(IDUsuario, Mes, System.DateTime.Now.Year, Convert.ToInt32(Session["IDEmpresa"]), ds);
            
            //CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new ReportDocument(); //Construção do Relatório
            //rpt.Load(Server.MapPath("/WebPontoFrequencia/Relatorio/RPT/rptFolhaFrequencia.rpt"));
            //rpt.Load(Server.MapPath(@"~/Relatorio/RPT/rptHorasDia.rpt"));
            //rpt.SetDataSource(ds);
           
            //crwHorasDia.ReportSource = rpt;

            //BinaryReader stream = new BinaryReader(rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat));
            
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.ContentType = "application/pdf";
            //HttpContext.Current.Response.BinaryWrite(stream.ReadBytes(Convert.ToInt32(stream.BaseStream.Length)));
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.Close();
    }
}