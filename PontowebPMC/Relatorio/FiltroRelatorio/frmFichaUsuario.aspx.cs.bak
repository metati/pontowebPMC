using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetodosPontoFrequencia;

public partial class Relatorio_FiltroRelatorio_frmFichaUsuario : System.Web.UI.Page
{
    DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
    public PreencheTabela PT = new PreencheTabela();
    public Frequencia Freq = new Frequencia();
    private Crip cr = new Crip();

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

        cbSetorFolha.DataSource = ds;
        cbSetorFolha.DataBind();
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
            coIDUsuarioServidorFicha.Add("IDSetor", "");
            coIDUsuarioServidorFicha.Add("IDUsuario", "");
         }

        if (cbSetorFolha.Text != "")
        {
            coIDUsuarioServidorFicha["IDSetor"] = cr.CriptograFa(cbSetorFolha.Value.ToString());
            
            if(cbUsuarioBancoHora.Text != "")
                coIDUsuarioServidorFicha["IDUsuario"] = cr.CriptograFa(cbUsuarioBancoHora.Value.ToString());
            else
                coIDUsuarioServidorFicha["IDUsuario"] = "";
        }
    }
    protected void btrelaBancohora_Click(object sender, EventArgs e)
    {
        this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> if(ASPxClientEdit.ValidateGroup('ValidaGrupo'))MudaPagina();</script>");
    }
    protected void cbUsuario_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        PreencheddlUsuario(cbSetorFolha.SelectedItem.Value.ToString());
    }
    protected void btVoltar_Click(object sender, EventArgs e)
    {

    }
}