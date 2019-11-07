using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetodosPontoFrequencia;

public partial class Defaultsession : System.Web.UI.Page
{
    DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
    PreencheTabela PT = new PreencheTabela();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (HttpContext.Current.User.Identity.IsAuthenticated == false)
        {
            Response.Redirect("~/Account/Login.aspx");
        }
        else if (!IsPostBack)
        {
            PreencheComboEmpresa();
        }
    }

    protected void PreencheComboEmpresa()
    {
        PT.PreencheTBEmpresaAdmin(ds);
        rlbOrgao.DataSource = ds.TBEmpresa;
        rlbOrgao.TextField = "DSEmpresa";
        rlbOrgao.ValueField = "IDEmpresa";
        rlbOrgao.DataBind();
    }
    protected void cbEmpresa_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        Session["IDEmpresa"] = e.Parameter.ToString();
        Response.Redirect(@"/Default.aspx");
    }

    private void TrocaSession(int IDSession, string NomeEmpresa)
    {
        Session["IDEmpresa"] = IDSession;
        Session["DSEmpresa"] = NomeEmpresa;
    }
    protected void btSalvar_Click(object sender, EventArgs e)
    {
        TrocaSession(Convert.ToInt32(rlbOrgao.Value),rlbOrgao.SelectedItem.Text);
        Session["TrocaSession"] = "1";
        Response.Redirect("~/Default.aspx");
    }
}