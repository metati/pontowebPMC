using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetodosPontoFrequencia;

public partial class Relatorio_FiltroRelatorio_frmLocalRegistro : System.Web.UI.Page
{

    DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
    public PreencheTabela PT = new PreencheTabela();
    public Frequencia Freq = new Frequencia();
    Crip cr = new Crip();

    protected void PreencheddlUsuario(string IDSetor)
    {
        PT.PreenchevwNomeUsuario(ds, Convert.ToInt32(IDSetor));
        cbUsuarioBancoHora.DataSource = ds;
        cbUsuarioBancoHora.DataBind();
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

        cbSetorLocalRegistro.DataSource = ds;
        cbSetorLocalRegistro.DataBind();
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated == false)
        {
            Response.Redirect("~/Account/Login.aspx");
        }
        else if (!IsPostBack)
        {
            deDataRelacao.Text = System.DateTime.Now.ToShortDateString();
            deDataRelacao0.Text = System.DateTime.Now.ToShortDateString();
            PreencheddlSetor();
            coLocalMarcacao.Add("IDSetor", "");
            coLocalMarcacao.Add("IDUsuario", "");
        }

        if (cbSetorLocalRegistro.Text != "")
            coLocalMarcacao["IDSetor"] = cr.CriptograFa(cbSetorLocalRegistro.Value.ToString());

        if (cbUsuarioBancoHora.Text != "")
            coLocalMarcacao["IDUsuario"] = cr.CriptograFa(cbUsuarioBancoHora.Value.ToString());
        
    }
    protected void btVoltar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }
    protected void cbUsuarioBancoHora_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        PreencheddlUsuario(e.Parameter); 
    }
    protected void btrelaBancohora_Click(object sender, EventArgs e)
    {
        if(deDataRelacao.Date.Date <= deDataRelacao0.Date.Date)
            this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> if(ASPxClientEdit.ValidateGroup('ValidaGrupo'))MudaPagina();</script>");
        else
            this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('Data inicial da pesquisa não ser maior que a data final.');</script>");
    }
}