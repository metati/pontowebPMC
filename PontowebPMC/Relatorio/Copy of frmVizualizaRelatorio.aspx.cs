using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using MetodosPontoFrequencia;

   public partial class Relatorio_frmVizualizaRelatorio : System.Web.UI.Page
    {
        DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
        Frequencia Freq = new Frequencia();
        PreencheTabela PT = new PreencheTabela();
        public string msg;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated == false)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
            else
            {
                HorasMes(Convert.ToString(Request.QueryString["User"]), Convert.ToInt32(Request.QueryString["Mes"]), Convert.ToInt32(Request.QueryString["Ano"]));
            }
        }

        protected void HorasMes(string IDUsuario, int Mes, int Ano)
        {               

            //Adicionado rotina para impressão de folha de frequência para o setor inteiro

            if (Convert.ToString(Request.QueryString["User"]) == "null")
            {
                PT.PreencheUsuarioEmpresaSetor(ds, Convert.ToInt32(Session["IDEmpresa"]), Convert.ToInt32(Request.QueryString["Setor"]));

                if (ds.TBUsuario.Rows.Count > 0)
                {
                    for (int I = 0; I <= (ds.TBUsuario.Rows.Count - 1); I++)
                    {
                        if (Ano == 0)
                        {
                            Freq.ExcluiFeriasJustificativasFrequencia(ds.TBUsuario[I].IDUsuario, Mes);
                            //Freq.GeraFolhaFrequencia(ds.TBUsuario[I].IDUsuario, Convert.ToInt32(Request.QueryString["Setor"]), Mes, System.DateTime.Now.Year, Convert.ToInt32(Session["IDEmpresa"]));
                            msg = Freq.HorasDias(Mes, 0, System.DateTime.Now.Year, ds, Convert.ToInt32(Session["IDEmpresa"]), Convert.ToInt32(Request.QueryString["Setor"]));
                        }
                        else
                        {
                            Freq.ExcluiFeriasJustificativasFrequencia(ds.TBUsuario[I].IDUsuario, Mes);
                            //Freq.GeraFolhaFrequencia(ds.TBUsuario[I].IDUsuario, Convert.ToInt32(Request.QueryString["Setor"]), Mes, (System.DateTime.Now.Year - 1), Convert.ToInt32(Session["IDEmpresa"]));
                            msg = Freq.HorasDias(Mes, 0, (System.DateTime.Now.Year - 1), ds, Convert.ToInt32(Session["IDEmpresa"]), Convert.ToInt32(Request.QueryString["Setor"]));
                        }
                    }

                    if (msg == "1")
                    {
                        Label1.Text = "Sem dados para Exibir";
                    }
                    else
                    {
                        Label1.Visible = false;
                        btFechar.Visible = false;
                        //CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new ReportDocument(); //Construção do Relatório
                        //rpt.Load(Server.MapPath("/WebPontoFrequencia/Relatorio/RPT/rptFolhaFrequencia.rpt"));
                       // rpt.Load(Server.MapPath(@"~/Relatorio/RPT/FolhaFrequenciaGrupal.rpt"));
                        //rpt.SetDataSource(ds);
                        //crwFolhaFrequencia.ReportSource = rpt;

                        //BinaryReader stream = new BinaryReader(rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat));
                        HttpContext.Current.Response.ClearContent();
                        HttpContext.Current.Response.ClearHeaders();
                        HttpContext.Current.Response.ContentType = "application/pdf";
                        //HttpContext.Current.Response.BinaryWrite(stream.ReadBytes(Convert.ToInt32(stream.BaseStream.Length)));
                        //HttpContext.Current.Response.Flush();
                        //HttpContext.Current.Response.Close();
                    }
                }
            }
            else
            {
                if (Ano == 0)
                {
                    Freq.ExcluiFeriasJustificativasFrequencia(Convert.ToInt32(IDUsuario), Mes);
                    //Freq.GeraFolhaFrequencia(Convert.ToInt32(IDUsuario), Convert.ToInt32(Request.QueryString["IDSetor"]), Mes, System.DateTime.Now.Year, Convert.ToInt32(Session["IDEmpresa"]));
                    msg = Freq.HorasDias(Mes, Convert.ToInt32(IDUsuario), System.DateTime.Now.Year, ds, Convert.ToInt32(Session["IDEmpresa"]), Convert.ToInt32(Request.QueryString["Setor"]));
                }
                else
                {
                    Freq.ExcluiFeriasJustificativasFrequencia(Convert.ToInt32(IDUsuario), Mes);
                    //Freq.GeraFolhaFrequencia(Convert.ToInt32(IDUsuario), Convert.ToInt32(Request.QueryString["Setor"]), Mes, (System.DateTime.Now.Year - 1), Convert.ToInt32(Session["IDEmpresa"]));
                    msg = Freq.HorasDias(Mes, Convert.ToInt32(IDUsuario), (System.DateTime.Now.Year - 1), ds, Convert.ToInt32(Session["IDEmpresa"]), Convert.ToInt32(Request.QueryString["Setor"]));
                }

                if (msg == "1")
                {
                    Label1.Text = "Sem dados para Exibir";
                }
                else
                {
                    Label1.Visible = false;
                    btFechar.Visible = false;
                    //CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new ReportDocument(); //Construção do Relatório
                    //rpt.Load(Server.MapPath("/WebPontoFrequencia/Relatorio/RPT/rptFolhaFrequencia.rpt"));
                    //rpt.Load(Server.MapPath(@"~/Relatorio/RPT/rptFolhaFrequencia.rpt"));
                    //rpt.SetDataSource(ds);
                    //crwFolhaFrequencia.ReportSource = rpt;

                    //BinaryReader stream = new BinaryReader(rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat));
                    HttpContext.Current.Response.ClearContent();
                    HttpContext.Current.Response.ClearHeaders();
                    HttpContext.Current.Response.ContentType = "application/pdf";
                    //HttpContext.Current.Response.BinaryWrite(stream.ReadBytes(Convert.ToInt32(stream.BaseStream.Length)));
                    //HttpContext.Current.Response.Flush();
                    //HttpContext.Current.Response.Close();
                }
            }
        }
}
