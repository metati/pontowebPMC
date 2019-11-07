using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetodosPontoFrequencia;

public partial class Cadastro_frmfoto : System.Web.UI.Page
{
    private DatasetPontoFrequencia ds = new DatasetPontoFrequencia();
    private PreencheTabela PT = new PreencheTabela();

    public string IDEMPRESA;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated == false)
        {
            Response.Redirect("~/Account/Login.aspx");
        }
        else
        {

            this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> ZeraTudo();</script>");
            
            //ImgFotoUsuario.Visible = false;
            //ImgFotoUsuario.ContentBytes = null;
            //ImgFotoUsuario.DataBind();
            
            lbNome.Text = "";
            lbNome.DataBind();

            if (Request.QueryString["ID"] != null && Request.QueryString["nome"] != null && Request.QueryString["Sobre"] != null)
                SubirFoto(Request.QueryString["ID"]);
        }
    }
    protected void SubirFoto(string IDUsuario)
    {
        ImgFotoUsuario.ContentBytes = PT.preencheCampoFoto(Convert.ToInt32(IDUsuario), Convert.ToInt32(Session["IDEmpresa"]));
        lbNome.Text =  Request.QueryString["nome"].ToString() + " " + Request.QueryString["Sobre"].ToString() ;
        lbNome.DataBind();
        ImgFotoUsuario.Visible = true;
        ImgFotoUsuario.DataBind();
    }
}