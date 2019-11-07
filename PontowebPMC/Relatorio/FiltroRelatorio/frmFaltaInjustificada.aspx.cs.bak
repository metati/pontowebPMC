using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetodosPontoFrequencia;
public partial class Relatorio_FiltroRelatorio_frmFaltaInjustificada : System.Web.UI.Page
{

    DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
    public PreencheTabela PT = new PreencheTabela();
    public Frequencia Freq = new Frequencia();
    private Crip cr = new Crip();

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
            PT.PreencheTBSetorIDEmpresa(ds, Convert.ToInt32(Session["IDEmpresa"]));
        }

        cbSetorFaltaInjustificada.DataSource = ds;
        cbSetorFaltaInjustificada.DataBind();
    }

    protected void PreencheddlUsuario(string IDSetor)
    {
        PT.PreenchevwNomeUsuario(ds, Convert.ToInt32(IDSetor));
        cbUsuarioFaltaInjustificada.DataSource = ds;
        cbUsuarioFaltaInjustificada.DataBind();
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated == false)
        {
            Response.Redirect("~/Account/Login.aspx");
        }
        if (!IsPostBack)
        {
            PreencheddlSetor();
            deDataInicioFaltaInjustificada.Date = System.DateTime.Now.Date.Date;
            deDataFimFaltaInjustificada.Date = System.DateTime.Now.Date.Date;
            deDataInicioFaltaInjustificada.DataBind();
            deDataFimFaltaInjustificada.DataBind();

            coFaltaInjustificada.Add("IDSetor", "");
            coFaltaInjustificada.Add("IDVinculoUsuario", "");
        }

        if (cbSetorFaltaInjustificada.Text != "")
            coFaltaInjustificada["IDSetor"] = cr.CriptograFa(cbSetorFaltaInjustificada.Value.ToString());

        if (cbUsuarioFaltaInjustificada.Text != "")
            coFaltaInjustificada["IDVinculoUsuario"] = cr.CriptograFa(cbUsuarioFaltaInjustificada.Value.ToString());
    }
    protected void btRelacaoDia_Click(object sender, EventArgs e)
    {
        this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> if(ASPxClientEdit.ValidateGroup('ValidaGrupo'))MudaPagina();</script>");
    }
    protected void btVoltar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }
    protected void cbUsuarioRelacaoPontoDia_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        PreencheddlUsuario(e.Parameter);
    }
}