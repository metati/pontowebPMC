using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using MetodosPontoFrequencia;

public partial class Relatorio_testeRel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        MetodosPontoFrequencia.Frequencia Freq = new MetodosPontoFrequencia.Frequencia();

        MetodosPontoFrequencia.DataSetPontoFrequencia ds = new DataSetPontoFrequencia();

        MetodosPontoFrequencia.PreencheTabela pt = new MetodosPontoFrequencia.PreencheTabela();

        Freq.HorasDias(7, 688, 2014, ds, 4, 7);

        Stimulsoft.Report.StiReport Report = new Stimulsoft.Report.StiReport();

        //Stimulsoft.Report.Web.StiWebDesigner st1 = new Stimulsoft.Report.Web.StiWebDesigner();

        Report.Load(Server.MapPath(@"~/Relatorio/RPT/ReportTesteFeito.mrt"));

        Report.RegData("DatasetPontoFrequencia", ds);

        Report.Render();

        MemoryStream memStream = new MemoryStream();

        Stimulsoft.Report.Export.StiPdfExportService Export = new Stimulsoft.Report.Export.StiPdfExportService();

        Export.ExportPdf(Report, memStream, Stimulsoft.Report.StiPagesRange.All, 100, 100, false, false, true, true, "", "", Stimulsoft.Report.Export.StiUserAccessPrivileges.All, Stimulsoft.Report.Export.StiPdfEncryptionKeyLength.Bit128, false);

        //HttpContext.Current.Response.ClearContent();
        //HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.ContentType = "application/pdf";
        HttpContext.Current.Response.BinaryWrite(memStream.ToArray());
        //HttpContext.Current.Response.Flush();
        //HttpContext.Current.Response.Close();
    }
}