using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetodosPontoFrequencia;

public partial class Cadastro_frmMudaOrgao : System.Web.UI.Page
{

    DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
    PreencheTabela PT = new PreencheTabela();

    protected void UsuarioTroca()
    {
        Cadastro cad = new Cadastro();
        cad.trocaUsuario(Convert.ToInt32(cbEmpresa.Value), Convert.ToInt32(cbSetorTroca.Value), Convert.ToInt32(coIDUsuarioTroca["IDusuarioTroca"].ToString()));
    }
    
    protected void PreencheTBEmpresa()
    {
        ds.EnforceConstraints = false;
        PT.PreencheTBEmpresa(ds);
        cbEmpresa.DataSource = ds.TBEmpresa;
        cbEmpresa.DataBind();
    }

    protected void PreencheTBSetor(int IDEmpresa)
    {
        ds.EnforceConstraints = false;
        PT.PreencheTBSetorIDEmpresa(ds, IDEmpresa);
        cbSetorTroca.DataSource = ds.TBSetor;
        cbSetorTroca.DataBind();
    }

    protected void PreencheGrid(string Busca)
    {
        if (rblOpcao.Value == "0")
        {
            PT.PreenchevwUsuarioGridNomeGeral(ds, Busca);
        }
        else
        {
            PT.PreenchevwUsuarioGridCPFGeral(ds, Busca);
        }

        gridBuscaServidor.DataSource = ds;
        gridBuscaServidor.DataBind();
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated == false)
        {
            Response.Redirect("~/Account/Login.aspx");
        }
        else
        {
            if (!IsPostBack)
            {
                PreencheTBEmpresa();
            }
        }

        if(tbBuscapServidor.Text != "")
            PreencheGrid(tbBuscapServidor.Text.Trim());
    }
    protected void gridBuscaServidor_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters == "Op")
        {
            UsuarioTroca();
        }
        PreencheGrid(tbBuscapServidor.Text.Trim());
    }
    protected void cbSetorTroca_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        PreencheTBSetor(Convert.ToInt32(cbEmpresa.Value));
    }
    protected void gridBuscaServidor_PageIndexChanged(object sender, EventArgs e)
    {
        PreencheGrid(tbBuscapServidor.Text.Trim());
    }
    protected void btVoltar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }
}