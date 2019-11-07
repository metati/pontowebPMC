using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Stimulsoft;
using MetodosPontoFrequencia;

public partial class Relatorio_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MetodosPontoFrequencia.Frequencia Freq = new MetodosPontoFrequencia.Frequencia();

        DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
        
        MetodosPontoFrequencia.PreencheTabela pt = new MetodosPontoFrequencia.PreencheTabela();

        Freq.HorasDias(7, 688, 2014, ds, 4, 7);
      
        Stimulsoft.Report.StiReport Report = new Stimulsoft.Report.StiReport();

        //Stimulsoft.Report.Web.StiWebDesigner st1 = new Stimulsoft.Report.Web.StiWebDesigner();

        Report.Load(Server.MapPath(@"~/Relatorio/RPT/rptFolhaFrequencia.mrt"));

        Report.RegData("DatasetPontoFrequencia", ds);

        Report.Render();

        System.IO.MemoryStream memStream = new System.IO.MemoryStream();

        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.ContentType = "application/pdf";
        
        HttpContext.Current.Response.AddHeader("Teste.pdf", "");
        
        Stimulsoft.Report.Export.StiPdfExportService Export = new Stimulsoft.Report.Export.StiPdfExportService();

        Export.ExportPdf(Report, memStream, Stimulsoft.Report.StiPagesRange.All, 100, 100, false, false, true, true, "", "", Stimulsoft.Report.Export.StiUserAccessPrivileges.All, Stimulsoft.Report.Export.StiPdfEncryptionKeyLength.Bit40, false);
        
        Response.ContentType = "application/pdf";
        
        Response.AddHeader("content-disposition", "inline; filename=Teste.pdf");

        
        Response.BinaryWrite(memStream.ToArray());

        //Stimulsoft.Report.Web.StiReportResponse.ResponseAs(this,
        
        //st1.Design(Report);
    }
}