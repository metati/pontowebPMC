using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetodosPontoFrequencia;

public partial class Relatorio_frmRelFrequencia : System.Web.UI.Page
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
                PreencheComboSetor(Convert.ToInt32(Session["IDEmpresa"]));
                PreencheComboMes();
            }
    }
    protected void PreencheComboSetor(int IDEmpresa)
    {
        PT.PreencheTBSetorIDEmpresa(ds, IDEmpresa);
        cbSetor.DataSource = ds;
        cbSetor.DataBind();
    }
    protected void PreencheComboUsuario(int IDSetor)
    {

        PT.PreenchevwNomeUsuario(ds, IDSetor);
        cbUsuario.DataSource = ds;
        cbUsuario.DataBind();

    }
    protected void PreencheComboMes()
    {
        PT.PreencheTBMes(ds);
        cbMes.DataSource = ds;
        cbMes.DataBind();
    }
    protected void cbUsuario_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        PreencheComboUsuario(Convert.ToInt32( e.Parameter));
    }
    protected void btVoltar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }
    protected void btOk_Click(object sender, EventArgs e)
    {
        if (cbUsuario.Value.ToString().Trim() == "Todos os usuários")
        {
            Session["IDUsuarioRelFreq"] = "0";
        }
        else
        {
            Session["IDUsuarioRelFreq"] = cbUsuario.SelectedItem.Value;
        }
        
        Session["MesReferenciaRelFreq"] = cbMes.SelectedItem.Value;
        Session["IDSetorRelFreq"] = cbSetor.SelectedItem.Value;
        Response.Write("<script>window.open('/WebPontoFrequencia/Relatorio/frmVizualizaRelFreq.aspx');</script>");
    }
}