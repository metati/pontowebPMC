using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetodosPontoFrequencia;

public partial class frmInformacao : System.Web.UI.Page
{
    DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
    PreencheTabela PT = new PreencheTabela();
    Cadastro Cad = new Cadastro();
    string msg;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated == false)
        {
            Response.Redirect("~/Account/Login.aspx");
        }
        else if (!IsPostBack)
        {
            ASPxPageControl1.TabPages[0].Enabled = false;
        }

        PreenchegridInformacao();
    }

    protected void PreenchegridInformacao()
    {
        PT.PreencheTBInformacaoDiaria(ds);
        gridInformacaoDiaria.DataSource = ds;
        gridInformacaoDiaria.DataBind();
    }

    protected void btAlterar_Click(object sender, EventArgs e)
    {
        PreencheCampos();
        ASPxPageControl1.TabPages[0].Enabled = true;
        ASPxPageControl1.TabPages[1].Enabled = false;
        Session["OperacaoCargo"] = 2;
        ASPxPageControl1.ActiveTabIndex = 0;
        ASPxPageControl1.DataBind();
    }

    protected void btLista_Click(object sender, EventArgs e)
    {
        ASPxPageControl1.TabPages[0].Enabled = false;
        ASPxPageControl1.ActiveTabIndex = 1;
        ASPxPageControl1.TabPages[1].Enabled = true;
        ASPxPageControl1.DataBind();
    }

    protected void PreencheCampos()
    {
        ASPxMemo1.Text = gridInformacaoDiaria.GetRowValues(gridInformacaoDiaria.FocusedRowIndex, "DSInformacaoDiaria").ToString();
    }

    protected void ApagaCampos()
    {
        ASPxMemo1.Text = "";
    }
    protected void btSalvar_Click(object sender, EventArgs e)
    {
        if ((int)Session["OperacaoCargo"] == 1)
        {
            SalvarInformacaoDiaria(ASPxMemo1.Text, Convert.ToInt32(Session["IDEmpresa"]));
            ApagaCampos();
        }
        else if ((int)Session["OperacaoCargo"] == 2)
        {
            int IDInformacao = (Convert.ToInt32(gridInformacaoDiaria.GetRowValues(gridInformacaoDiaria.FocusedRowIndex,"TBInformacaoDiaria").ToString()));
            AlteraInformacaoDiaria(ASPxMemo1.Text,IDInformacao,Convert.ToInt32(Session["IDEmpresa"]));
            ApagaCampos();
        }
    }

    protected void SalvarInformacaoDiaria(string Texto, int IDEmpresa)
    {
        msg = Cad.CadastraInformacaoDiaria(Texto, System.DateTime.Now, IDEmpresa);
        this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('" + msg + "');</script>");
    }

    protected void AlteraInformacaoDiaria(string Texto, int IDInformacaoDiaria, int IDEmpresa)
    {
        msg = Cad.AlteraInformacaoDiaria(Texto, IDInformacaoDiaria,IDEmpresa);
        this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('" + msg + "');</script>");
    }
    protected void btCadastrar_Click(object sender, EventArgs e)
    {
        ApagaCampos();
        Session["OperacaoCargo"] = 1;
        ASPxPageControl1.TabPages[0].Enabled = true;
        ASPxPageControl1.ActiveTabIndex = 0;
        ASPxPageControl1.TabPages[1].Enabled = false;
        ASPxPageControl1.DataBind();
    }
    protected void btAlterar_Click1(object sender, EventArgs e)
    {
        PreencheCampos();
        ASPxPageControl1.TabPages[0].Enabled = true;
        ASPxPageControl1.TabPages[1].Enabled = false;
        Session["OperacaoCargo"] = 2;
        ASPxPageControl1.ActiveTabIndex = 0;
        ASPxPageControl1.DataBind();
    }
    protected void btVoltar_Click(object sender, EventArgs e)
    {
        Response.Redirect("/webPontoFrequencia/Default.aspx");
    }
    protected void btLista_Click1(object sender, EventArgs e)
    {
        ASPxPageControl1.TabPages[0].Enabled = false;
        ASPxPageControl1.ActiveTabIndex = 1;
        ASPxPageControl1.TabPages[1].Enabled = true;
        ASPxPageControl1.DataBind();
    }
    protected void btSalvar_Click1(object sender, EventArgs e)
    {
        if ((int)Session["OperacaoCargo"] == 1)
        {
            SalvarInformacaoDiaria(ASPxMemo1.Text, Convert.ToInt32(Session["IDEmpresa"]));
            ApagaCampos();
        }
        else if ((int)Session["OperacaoCargo"] == 2)
        {
            int IDInformacao = (Convert.ToInt32(gridInformacaoDiaria.GetRowValues(gridInformacaoDiaria.FocusedRowIndex, "TBInformacaoDiaria").ToString()));
            AlteraInformacaoDiaria(ASPxMemo1.Text, IDInformacao, Convert.ToInt32(Session["IDEmpresa"]));
            ApagaCampos();
        }
    }
    protected void btVoltarCad_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }
}