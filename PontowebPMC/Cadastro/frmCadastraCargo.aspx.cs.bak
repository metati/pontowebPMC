using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetodosPontoFrequencia;

public partial class Cadastro_frmCadastraCargo : System.Web.UI.Page
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
            ASPxPageControl2.TabPages[0].Enabled = false;
        }
        
        PreencheGrid();
    }

    public void PreencheGrid()
    {
        PT.PreencheTBCargo(ds);
        gridCargo.DataSource = ds;
        gridCargo.DataBind();
    }

    protected void btVoltar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }

    protected void btAlterar_Click(object sender, EventArgs e)
    {
        PreencheCampos();
        ASPxPageControl2.TabPages[0].Enabled = true;
        ASPxPageControl2.TabPages[1].Enabled = false;
        Session["OperacaoCargo"] = 2;
        ASPxPageControl2.ActiveTabIndex = 0;
        ASPxPageControl2.DataBind();
    }

    protected void btLista_Click(object sender, EventArgs e)
    {
        ASPxPageControl2.TabPages[0].Enabled = false;
        ASPxPageControl2.ActiveTabIndex = 1;
        ASPxPageControl2.TabPages[1].Enabled = true;
        ASPxPageControl2.DataBind();
    }

    protected void CadastraCargo(string DsCargo)
    {
        msg = Cad.CadastraCargo(DsCargo); ;
        this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('" + msg + "');</script>");
    }

    protected void btVoltarCad_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }

    protected void PreencheCampos()
    {
        tbDescricaoCargo.Text = gridCargo.GetRowValues(gridCargo.FocusedRowIndex, "DSCargo").ToString();
    }

    protected void btSalvar_Click(object sender, EventArgs e)
    {
        if ((int)Session["OperacaoCargo"] == 1)
        {
            CadastraCargo(tbDescricaoCargo.Text);
            ApagaCampos();
        }
        else if ((int)Session["OperacaoCargo"] == 2)
        {
            AlterarCargo(tbDescricaoCargo.Text);
            ApagaCampos();
        }

    }

    protected void btCadastrar_Click(object sender, EventArgs e)
    {
        ApagaCampos();
        Session["OperacaoCargo"] = 1;
        ASPxPageControl2.TabPages[0].Enabled = true;
        ASPxPageControl2.ActiveTabIndex = 0;
        ASPxPageControl2.TabPages[1].Enabled = false;
        ASPxPageControl2.DataBind();
    }

    protected void AlterarCargo(string Cargo)
    {
        msg = Cad.AlteraCargo(Cargo,Convert.ToInt32(gridCargo.GetRowValues(gridCargo.FocusedRowIndex, "IDCargo").ToString()));
        this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('" + msg + "');</script>");
    }

    protected void ApagaCampos()
    {
        tbDescricaoCargo.Text = "";
    }
    protected void ASPxPageControl2_ActiveTabChanged(object source, DevExpress.Web.ASPxTabControl.TabControlEventArgs e)
    {

    }
}