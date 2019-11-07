using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetodosPontoFrequencia;

public partial class Especial_frmRegistraFrequenciaEspecial : System.Web.UI.Page
{
    DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
    Frequencia freq = new Frequencia();
    string msg;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated == false)
        {
            Response.Redirect("~/Account/Login.aspx");
        }
        else if (tbLogin.Text == "")
            tbLogin.Text = (string)Session["LOGIN"];
    }

    protected void baterponto()
    {
        msg = freq.PontoEspecial(tbLogin.Text, tbSenha.Text, Convert.ToInt32(Session["IDEmpresa"]),System.DateTime.Now,ds);
        this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('" + msg + "');</script>");
    }

    protected void btSalvar_Click(object sender, EventArgs e)
    {
        baterponto();
    }
    protected void btCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }
}