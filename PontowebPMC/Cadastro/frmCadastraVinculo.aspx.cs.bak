using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetodosPontoFrequencia;

public partial class Cadastro_frmCadastraVinculo : System.Web.UI.Page
{
    public Cadastro Cad = new Cadastro();
    public PreencheTabela PT = new PreencheTabela();
    DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
    public string msg = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated == false)
        {
            Response.Redirect("~/Account/Login.aspx");
        }
        else if (!IsPostBack)
        {
            pgVinculo.TabPages[0].Enabled = false;
        }

        PreencheGrid();
    }

    public void PreencheGrid()
    {
        PT.PreencheTBEntidade(ds);
        gridEntidade.DataSource = ds;
        gridEntidade.DataBind();
    }

    protected void CadastraEntidade(string DsEntidade)
    {
        msg = Cad.CadastraEntidade(DsEntidade, "");
        this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('" + msg + "');</script>");
    }

    protected void btVoltarCad_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }

    protected void PreencheCampos()
    {
        tbDescricaoCargo.Text = gridEntidade.GetRowValues(gridEntidade.FocusedRowIndex, "DSEntidade").ToString();
    }

    protected void btSalvar_Click(object sender, EventArgs e)
    {
        if ((int)Session["OperacaoVinculo"] == 1)
        {
            CadastraEntidade(tbDescricaoCargo.Text);
            ApagaCampos();
        }
        else if ((int)Session["OperacaoVinculo"] == 2)
        {
            AlterarEntidade(tbDescricaoCargo.Text);
            ApagaCampos();
        }

    }

    protected void btCadastrar_Click(object sender, EventArgs e)
    {
        ApagaCampos();
        Session["OperacaoVinculo"] = 1;
        pgVinculo.TabPages[0].Enabled = true;
        pgVinculo.ActiveTabIndex = 0;
        pgVinculo.TabPages[1].Enabled = false;
        pgVinculo.DataBind();
    }

    protected void AlterarEntidade(string Entidade)
    {
        msg = Cad.AlteraEntidade(Entidade, Convert.ToInt32(gridEntidade.GetRowValues(gridEntidade.FocusedRowIndex, "IDEntidade").ToString()),"");
        this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('" + msg + "');</script>");
    }

    protected void ApagaCampos()
    {
        tbDescricaoCargo.Text = "";
    }
    protected void btLista_Click(object sender, EventArgs e)
    {
        pgVinculo.TabPages[0].Enabled = false;
        pgVinculo.ActiveTabIndex = 1;
        pgVinculo.TabPages[1].Enabled = true;
        pgVinculo.DataBind();
    }
    protected void pgVinculo_ActiveTabChanged(object source, DevExpress.Web.ASPxTabControl.TabControlEventArgs e)
    {

    }
    protected void btAlterar_Click(object sender, EventArgs e)
    {
        PreencheCampos();
        pgVinculo.TabPages[0].Enabled = true;
        pgVinculo.TabPages[1].Enabled = false;
        Session["OperacaoVinculo"] = 2;
        pgVinculo.ActiveTabIndex = 0;
        pgVinculo.DataBind();
    }
    protected void btVoltar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }
}