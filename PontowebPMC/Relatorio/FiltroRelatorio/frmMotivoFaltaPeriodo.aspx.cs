using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetodosPontoFrequencia;
public partial class Relatorio_FiltroRelatorio_frmMotivoFaltaPeriodo : System.Web.UI.Page
{

    DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
    public PreencheTabela PT = new PreencheTabela();
    public Frequencia Freq = new Frequencia();
    private Crip cr = new Crip();

    protected void PreencheddlMotivoFalta()
    {
        PT.PreencheTBMotivoFalta(ds);

        cbMotivoFaltaRel.DataSource = ds;
        cbMotivoFaltaRel.DataSource = ds;
        cbMotivoFaltaRel.DataBind();
        cbMotivoFaltaRel.DataBind();
    }


    protected void PreencheddlSetor()
    {

        if (Convert.ToInt32(Session["TPUsuario"]) == 3 || Convert.ToInt32(Session["TPUsuario"]) == 9)
        {
            //PT.PreencheTBSetor(ds);

            //PT.PreencheTBSetorIDSetor(ds, Convert.ToInt32(Session["IDSETOR"]), Convert.ToInt32(Session["IDEmpresa"]));
            PT.PreencheTBSetorGestor(ds, Convert.ToInt32(Session["IDUsuario"]), Convert.ToInt32(Session["IDEmpresa"]));
        }
        else if (Convert.ToInt32(Session["TPUsuario"]) == 1 || Convert.ToInt32(Session["TPUsuario"]) == 8)
        {
            PT.PreencheTBSetorIDEmpresa(ds,Convert.ToInt32(Session["IDEmpresa"]));
        }
        cbSetorMotivoFalta.DataSource = ds;
        cbSetorMotivoFalta.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated == false)
        {
            Response.Redirect("~/Account/Login.aspx");
        }
        else if (!IsPostBack)
        {
            PreencheddlSetor();
            PreencheddlMotivoFalta();
            deMotivoFalta.Date = System.DateTime.Now.Date.Date;
            deMotivoFaltaFim.Date = System.DateTime.Now.Date.Date;
            coMotivoFaltaPeriodo.Add("IDSetor", "");
        }

        if (cbSetorMotivoFalta.Text != "")
            coMotivoFaltaPeriodo["IDSetor"] = cr.CriptograFa(cbSetorMotivoFalta.Value.ToString());

    }
    protected void btVoltar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }
    protected void btRelacaoDia_Click(object sender, EventArgs e)
    {
        this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> if(ASPxClientEdit.ValidateGroup('ValidaGrupo'))MudaPagina();</script>");
    }
}