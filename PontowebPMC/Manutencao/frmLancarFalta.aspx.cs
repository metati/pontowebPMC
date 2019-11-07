using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetodosPontoFrequencia;

public partial class Manutencao_frmLancarFalta : System.Web.UI.Page
{
    DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
    PreencheTabela PT = new PreencheTabela();
    Frequencia Freq = new Frequencia();

    //ID padrão para lançamento de faltas = 47

    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated == false)
        {
            Response.Redirect("~/Default.aspx");
        }
        else if(!IsPostBack)
        {
            //ddlEmpresa.Items.Insert(0, "Selecione uma empresa");
            PreencheddlSetor(Convert.ToInt32(Session["IDEmpresa"]));
        }
    }
/*    protected void PreencheddlEmpresa()
    {
        PT.PreencheTBEmpresa(ds);
        //ddlEmpresa.DataSource = ds;
//        if (Convert.ToInt32(Session["TPUsuario"]) == 3)
 //       {
//            ddlEmpresa.SelectedValue = Convert.ToString(Session["IDEmpresa"]);
//            ddlEmpresa.Enabled = false;
//        }
//        ddlEmpresa.DataBind();
        if (Convert.ToInt32(Session["TPUsuario"]) == 3)
        {
           // ddlEmpresa.SelectedValue = Convert.ToString(Session["IDEmpresa"]);
           // ddlEmpresa.Enabled = false;
            PreencheddlSetor(Convert.ToInt32( Session["IDEmpresa"]));
        }
    }*/
    protected void PreencheddlSetor(int IDEmpresa)
    {
        if (Convert.ToInt32(Session["TPUsuario"]) == 3)
        {
            PT.PreencheTBSetorGestor(ds, Convert.ToInt32(Session["IDUsuario"]), Convert.ToInt32(Session["IDEmpresa"]));
        }
        else
        {
            PT.PreencheTBSetorIDEmpresa(ds, IDEmpresa);
        }

        cbSetor.DataSource = ds;
        cbSetor.DataBind();
    }

    protected void PreencheddlUsuario()
    {
        PT.PreenchevwNomeUsuario(ds, Convert.ToInt32(cbSetor.SelectedItem.Value));
        cbUsuario.DataSource = ds;
        cbUsuario.DataBind();
    }

    protected void SalvarFalta(int IDUsuario, DateTime DTFrequencia, int IDMotivoFalta, string OBS)
    {
        
        
        string msg = Freq.LancaFalta(IDUsuario, DTFrequencia, IDMotivoFalta, OBS,Convert.ToInt32(Session["IDEmpresa"]),PT.RetornaIDVinculoUsuario(Convert.ToInt32(Session["IDEmpresa"]), Convert.ToInt32(cbSetor.Value), IDUsuario));
        
        this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('" + msg + "');</script>");
    }
    protected void btSalvar_Click(object sender, EventArgs e)
    {
        if (!cheqSetor.Checked)
            SalvarFalta(Convert.ToInt32(cbUsuario.SelectedItem.Value), deData.Date, 47, memoOBS.Text);
        else
        {
            if (ds.vwNomeUsuario.Rows.Count > 0)
            {
                for (int i = 0; i <= (cbUsuario.Items.Count - 1); i++)
                {
                    SalvarFalta(Convert.ToInt32(ds.vwNomeUsuario[i].IDUsuario), deData.Date, 47, memoOBS.Text);        
                }

                this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('Rotina executada com sucesso!');</script>");

            }
            else
            {
                this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('Sem usuários para o setor selecionado.');</script>");    
            }
        }

        cbUsuario.Text = "";
        deData.Text = "";

        memoOBS.Text = "";
    }

    protected void ddlEmpresa_SelectedIndexChanged(object sender, EventArgs e)
    {
        PreencheddlSetor(Convert.ToInt32(Session["IDEmpresa"]));
    }
    protected void btVoltar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }
    protected void cbUsuario_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        PreencheddlUsuario();
    }
}