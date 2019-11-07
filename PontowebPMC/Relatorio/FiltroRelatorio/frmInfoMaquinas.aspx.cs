using MetodosPontoFrequencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Relatorio_FiltroRelatorio_frmInfoMaquinas : System.Web.UI.Page
{
    DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
    public PreencheTabela PT = new PreencheTabela();
    public Frequencia Freq = new Frequencia();
    private Crip cr = new Crip();

    //protected void PreencheddlSetor()
    //{

    //    if (Convert.ToInt32(Session["TPUsuario"]) == 3)
    //    {
    //        //PT.PreencheTBSetor(ds);

    //        //PT.PreencheTBSetorIDSetor(ds, Convert.ToInt32(Session["IDSETOR"]), Convert.ToInt32(Session["IDEmpresa"]));
    //        PT.PreencheTBSetorGestor(ds, Convert.ToInt32(Session["IDUsuario"]), Convert.ToInt32(Session["IDEmpresa"]));
    //    }
    //    else if (Convert.ToInt32(Session["TPUsuario"]) == 1 || Convert.ToInt32(Session["TPUsuario"]) == 8 || Convert.ToInt32(Session["TPUsuario"]) == 9)
    //    {
    //        PT.PreencheTBSetorIDEmpresa(ds, Convert.ToInt32(Session["IDEmpresa"]));
    //    }

    //    cbRelacaoDia.DataSource = ds;
    //    cbRelacaoDia.DataBind();
    //}

    //protected void PreencheddlUsuario(string IDSetor)
    //{
    //    PT.PreenchevwNomeUsuario(ds, Convert.ToInt32(IDSetor));
    //    cbUsuarioRelacaoPontoDia.DataSource = ds;
    //    cbUsuarioRelacaoPontoDia.DataBind();
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated == false)
        {
            Response.Redirect("~/Account/Login.aspx");
        }
        if (!IsPostBack)
        {
            //PreencheddlSetor();
            deDataRelacao.Date = System.DateTime.Now.Date.Date;
            deDataRelacao0.Date = System.DateTime.Now.Date.Date;
            deDataRelacao0.DataBind();
            deDataRelacao.DataBind();
            coRelJustAudit.Add("DTInicial", "");
            coRelJustAudit.Add("DTFinal", "");
            coRelJustAudit.Add("NomeServidor", "");
            coIDUsuarioRelJustAudit.Add("IDUsuario", "");
            PreencheddlSetor();
            PreencheddlSetor2();
        }

        //if (cbRelacaoDia.Text != "")
        //    coRelJustAudit["IDSetor"] = cr.CriptograFa(cbRelacaoDia.Value.ToString());

        //if (cbUsuarioRelacaoPontoDia.Text != "")
        //    coIDUsuarioRelJustAudit["IDUsuario"] = cr.CriptograFa(cbUsuarioRelacaoPontoDia.Value.ToString());

    }


    protected void PreencheddlSetor()
    {

        if (Convert.ToInt32(Session["TPUsuario"]) == 3)
        {
            //PT.PreencheTBSetor(ds);

            //PT.PreencheTBSetorIDSetor(ds, Convert.ToInt32(Session["IDSETOR"]), Convert.ToInt32(Session["IDEmpresa"]));
            PT.PreencheTBSetorGestor(ds, Convert.ToInt32(Session["IDUsuario"]), Convert.ToInt32(Session["IDEmpresa"]));
        }
        else if (Convert.ToInt32(Session["TPUsuario"]) == 1 || Convert.ToInt32(Session["TPUsuario"]) == 8 || Convert.ToInt32(Session["TPUsuario"]) == 9)
        {
            PT.PreencheTBSetorIDEmpresa(ds, Convert.ToInt32(Session["IDEmpresa"]));
        }

        cbSetorAusenteInjust.DataSource = ds;
        cbSetorAusenteInjust.DataBind();
    }

    protected void PreencheddlSetor2()
    {

        if (Convert.ToInt32(Session["TPUsuario"]) == 3)
        {
            //PT.PreencheTBSetor(ds);

            //PT.PreencheTBSetorIDSetor(ds, Convert.ToInt32(Session["IDSETOR"]), Convert.ToInt32(Session["IDEmpresa"]));
            PT.PreencheTBSetorGestor(ds, Convert.ToInt32(Session["IDUsuario"]), Convert.ToInt32(Session["IDEmpresa"]));
        }
        else if (Convert.ToInt32(Session["TPUsuario"]) == 1 || Convert.ToInt32(Session["TPUsuario"]) == 8 || Convert.ToInt32(Session["TPUsuario"]) == 9)
        {
            PT.PreencheTBSetorIDEmpresa(ds, Convert.ToInt32(Session["IDEmpresa"]));
        }

        cbSetorAusenteInjust2.DataSource = ds;
        cbSetorAusenteInjust2.DataBind();
    }

    protected void btVoltar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }
    protected void btRelacaoDia_Click(object sender, EventArgs e)
    {
        this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> if(ASPxClientEdit.ValidateGroup('ValidaGrupo'))MudaPagina();</script>");
    }


    protected void btMaquinasInstaladas_Click(object sender, EventArgs e)
    {
        this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> if(ASPxClientEdit.ValidateGroup('ValidaGrupo'))GetMaquinas();</script>");
    }
    //protected void cbUsuarioRelacaoPontoDia_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    //{
    //    //PreencheddlUsuario(e.Parameter);
    //}

    protected void cbUsuarioRelacaoPontoDia_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}