using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetodosPontoFrequencia;

public partial class Manutencao_frmCadastroFeriasLicenca : System.Web.UI.Page
{
    DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
    Cadastro Cad = new Cadastro();
    PreencheTabela PT = new PreencheTabela();
    Frequencia Freq = new Frequencia();
    string msg, msg1;
    private long IDVinculoUsuario;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated == false)
        {
            Response.Redirect("~/Default.aspx");
        }
        else
        {
            if (!IsPostBack)
            {
                ddlSetor.Items.Insert(0, "");
                PreencheddlSetor();
                ddlTipoFerias.Items.Insert(0, "");
                PreencheddlTipoFerias();
            }
        }
    }

    private void RetornaVinculoUsuario(int IDUsuario, int IDEmpresa, int IDSetor)
    {
        IDVinculoUsuario = PT.RetornaIDVinculoUsuario(IDEmpresa, IDSetor, IDUsuario);
    }

    public void PreencheGrid()
    {
        ds.EnforceConstraints = false;
        PT.PreenchevwFeriasIDSetor(ds,Convert.ToInt32(ddlSetor.SelectedValue));
        gridFerias.DataSource = ds;
        gridFerias.DataBind();

        DTinicial.Value = "";
        DTFinal.Value = "";

        DTinicial.DataBind();
        DTFinal.DataBind();
    }

    protected void PreencheddlUsuario(int IDSetor)
    {
        PT.PreenchevwNomeUsuario(ds, IDSetor);
        ddlUsuario.DataSource = ds;
        ddlUsuario.DataBind();
    }

    protected void PreencheddlTipoFerias()
    {
        ds.EnforceConstraints = false;
        PT.PreencheTBTipoFerias(ds);
        ddlTipoFerias.DataSource = ds;
        ddlTipoFerias.DataBind();
    }

    protected void PreencheddlSetor()
    {
        if (Convert.ToInt32(Session["TPUsuario"]) == 3)
        {
            //PT.PreencheTBSetor(ds); -- cookie
            //PT.PreencheTBSetorIDSetor(ds, Convert.ToInt32(Session["IDSETOR"]), Convert.ToInt32(Session["IDEmpresa"]));
            PT.PreencheTBSetorGestor(ds, Convert.ToInt32(Session["IDUsuario"]), Convert.ToInt32(Session["IDEmpresa"]));
        }
        else
        {
            PT.PreencheTBSetorIDEmpresa(ds, Convert.ToInt32(Session["IDEmpresa"]));
        }

        ddlSetor.DataSource = ds;
        ddlSetor.DataBind();
    }
    
    protected void DeletaRegistro()
    {
        RetornaVinculoUsuario(Convert.ToInt32(IDUsuario.Value), Convert.ToInt32(Session["IDEmpresa"]), Convert.ToInt32(ddlSetor.SelectedItem.Value));
        
        msg = Cad.ExcluiFerias(Convert.ToInt32(CampoOculto.Value), Convert.ToInt32(Session["IDUsuario"]), Convert.ToInt32(Session["IDEmpresa"]), deInicio.Date.Date, deFim.Date.Date, Convert.ToInt32(IDUsuario.Value), Convert.ToInt32(TPFerias.Value));

        msg1 = Freq.ManutencaoFrequenciaFeriasLicenca(Convert.ToInt32(IDUsuario.Value), Convert.ToInt32(TPFerias.Value), Convert.ToDateTime(DTinicial.Value), Convert.ToDateTime(DTFinal.Value), "Exclusao", Convert.ToDateTime(DTinicial.Value), Convert.ToDateTime(DTFinal.Value),Convert.ToInt32(Session["IDEmpresa"]),IDVinculoUsuario);

        this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('" + msg + "');</script>");
        PreencheGrid();
    }
    protected void SalvaFerias()
    {
        RetornaVinculoUsuario(Convert.ToInt32(ddlUsuario.SelectedValue), Convert.ToInt32(Session["IDEmpresa"]), Convert.ToInt32(ddlSetor.SelectedItem.Value));
        
        if (CampoOculto.Value == "1")
        {
            msg = Cad.CadastraFerias(Convert.ToInt32(ddlUsuario.SelectedValue), 1, deInicio.Date, deFim.Date, Convert.ToInt32(ddlTipoFerias.SelectedItem.Value), Convert.ToInt32(Session["IDUsuario"]), Convert.ToInt32(Session["IDEmpresa"]),IDVinculoUsuario);
            //Lança os dias de férias na TBFrequência
            msg1 = Freq.ManutencaoFrequenciaFeriasLicenca(Convert.ToInt32(ddlUsuario.SelectedValue), Convert.ToInt32(ddlTipoFerias.SelectedValue), deInicio.Date, deFim.Date, "Inclusao", deInicio.Date, deFim.Date,Convert.ToInt32(Session["IDEmpresa"]),IDVinculoUsuario);
            //--------------------------------------
        }
        else
        {
            msg = Cad.AlteraFerias(Convert.ToInt32(ddlUsuario.SelectedValue), 1, deInicio.Date, deFim.Date, Convert.ToInt32(IDFerias.Value), Convert.ToInt32(TPFerias.Value), Convert.ToInt32(Session["IDUsuario"]), Convert.ToInt32(Session["IDEmpresa"]),IDVinculoUsuario);

            msg1 = Freq.ManutencaoFrequenciaFeriasLicenca(Convert.ToInt32(IDUsuario.Value), Convert.ToInt32(ddlTipoFerias.SelectedValue), deInicio.Date, deFim.Date, "Alteracao", Convert.ToDateTime(DTinicial.Value), Convert.ToDateTime(DTFinal.Value), Convert.ToInt32(Session["IDEmpresa"]),IDVinculoUsuario);
        }
        
        this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('" + msg + "');</script>");
        
        this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('" + msg1 + "');</script>");
        
        PreencheGrid();
    }
    protected void gridFerias_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        if (ddlSetor.Text != "")
        {
            PreencheGrid();
        }
    }
    protected void gridFerias_DetailRowExpandedChanged(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewDetailRowEventArgs e)
    {
        if (ddlSetor.Text != "")
        {
            PreencheGrid();
        }
    }
    protected void gridFerias_DetailsChanged(object sender, EventArgs e)
    {
        if (ddlSetor.Text != "")
        {
            PreencheGrid();
        }
    }
    protected void gridFerias_PageIndexChanged(object sender, EventArgs e)
    {
        if (ddlSetor.Text != "")
        {
            PreencheGrid();
        }
    }
    protected void gridFerias_BeforeColumnSortingGrouping(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
    {
        if (ddlSetor.Text != "")
        {
            PreencheGrid();
        }
    }
    protected void ddlSetor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSetor.Text != "")
        {
            ddlUsuario.Items.Insert(0, "");
            PreencheddlUsuario(Convert.ToInt32(ddlSetor.SelectedValue));
        }
    }
    protected void btSalvar_Click(object sender, EventArgs e)
    {
        SalvaFerias();
    }
    protected void btCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }
    protected void btConfirmar_Click(object sender, EventArgs e)
    {
        DeletaRegistro();
    }

    protected void gridFerias_HeaderFilterFillItems(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewHeaderFilterEventArgs e)
    {
        if (ddlSetor.Text != "")
        {
            PreencheGrid();
        }
    }
    protected void gridFerias_ProcessColumnAutoFilter(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewAutoFilterEventArgs e)
    {
        if (ddlSetor.Text != "")
        {
            PreencheGrid();
        }
    }
}