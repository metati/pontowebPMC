using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetodosPontoFrequencia;

public partial class Relatorio_FiltroRelatorio_frmAusenteInjust : System.Web.UI.Page
{
    DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
    public PreencheTabela PT = new PreencheTabela();
    public Frequencia Freq = new Frequencia();
    private Crip cr = new Crip();

    protected void PreencheddlUsuario(string IDSetor)
    {
        PT.PreenchevwNomeUsuario(ds, Convert.ToInt32(IDSetor));
        cbServidorAusenteInjust.DataSource = ds;
        cbServidorAusenteInjust.DataBind();
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
            PT.PreencheTBSetorIDEmpresa(ds, Convert.ToInt32(Session["IDEmpresa"]));
        }

        cbSetorAusenteInjust.DataSource = ds;
        cbSetorAusenteInjust.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated == false)
        {
            Response.Redirect("~/Account/Login.aspx");
        }
         else if (!IsPostBack)
        {
            dtAusenteInjustInicio.Date = DateTime.Now.Date.Date;
            dtAusenteInjustFim.Date = DateTime.Now.Date.Date;
             PreencheddlSetor();
            coIDUsuarioAusenteInjust.Add("IDSetor", "");
            coIDUsuarioAusenteInjust.Add("IDUsuario", "");
         }

        if (cbSetorAusenteInjust.Text != "")
        {
            coIDUsuarioAusenteInjust["IDSetor"] = cr.CriptograFa(cbSetorAusenteInjust.Value.ToString());
            
            if(cbServidorAusenteInjust.Text != "")
                coIDUsuarioAusenteInjust["IDUsuario"] = cr.CriptograFa(cbServidorAusenteInjust.Value.ToString());
            else
                coIDUsuarioAusenteInjust["IDUsuario"] = "";
        }
    }
    protected void btrelaBancohora_Click(object sender, EventArgs e)
    {
        this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> if(ASPxClientEdit.ValidateGroup('ValidaGrupo'))MudaPagina();</script>");
    }
    protected void cbUsuario_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        PreencheddlUsuario(cbSetorAusenteInjust.SelectedItem.Value.ToString());
    }
    protected void btVoltar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }
}