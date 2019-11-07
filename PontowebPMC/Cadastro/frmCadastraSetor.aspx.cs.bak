using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetodosPontoFrequencia;

public partial class Cadastro_frmCadastraSetor : System.Web.UI.Page
{
    public Cadastro Cad = new Cadastro();
    public PreencheTabela PT = new PreencheTabela();
    DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
    public string msg = "";
    public int IDEmp;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated == false)
        {
            Response.Redirect("~/Account/Login.aspx");
        }
        else if (!IsPostBack)
        {
            ASPxPageControl1.TabPages[0].Enabled = false;
            PreencheDropStatus();
        }
        PreencheGrid();
        IDEmp = Convert.ToInt32(Session["IDEmpresa"]);
    }

    protected void PreencheDropStatus()
    {
        PT.PreencheTBStatus(ds);
        cbStatus.DataSource = ds;
        cbStatus.DataBind();
    }

    public void PreencheGrid()
    {
        PT.PreencheTBSetorIDEmpresaGeral(ds,Convert.ToInt32(Session["IDEmpresa"]));
        gridSetor.DataSource = ds;
        gridSetor.DataBind();
    }
    protected void btVoltar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }
    protected void btAlterar_Click(object sender, EventArgs e)
    {
        PreencheCampos();
        ASPxPageControl1.TabPages[0].Enabled = true;
        ASPxPageControl1.TabPages[1].Enabled = false;
        Session["OperacaoSetor"] = 2;
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

    protected void CadastraSetor(string DsSetor, string Sigla)
    {
        msg = Cad.CadastraSetor(DsSetor,Convert.ToInt32(cbStatus.SelectedItem.Value), Sigla, IDEmp);
        this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('"+ msg + "');</script>");
    }
    protected void ASPxPageControl1_ActiveTabChanged(object source, DevExpress.Web.ASPxTabControl.TabControlEventArgs e)
    {

    }
    protected void ASPxPageControl1_ActiveTabChanged1(object source, DevExpress.Web.ASPxTabControl.TabControlEventArgs e)
    {

    }
    protected void btVoltarCad_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }

    protected void PreencheCampos()
    {
        tbDescricaoSetor.Text = gridSetor.GetRowValues(gridSetor.FocusedRowIndex, "DSSetor").ToString();
        tbSiglaSetor.Text = gridSetor.GetRowValues(gridSetor.FocusedRowIndex, "Sigla").ToString();
        cbStatus.Value = gridSetor.GetRowValues(gridSetor.FocusedRowIndex, "IDStatus").ToString();
    }

    protected void btSalvar_Click(object sender, EventArgs e)
    {
        if ((int)Session["OperacaoSetor"] == 1)
        {
            CadastraSetor(tbDescricaoSetor.Text, tbSiglaSetor.Text);
            ApagaCampos();
        }
        else if ((int)Session["OperacaoSetor"] == 2)
        {
            AlterarSetor(tbDescricaoSetor.Text, tbSiglaSetor.Text);
            ApagaCampos();
        }
        
    }
    protected void btCadastrar_Click(object sender, EventArgs e)
    {
        ApagaCampos();
        Session["OperacaoSetor"] = 1;
        ASPxPageControl1.TabPages[0].Enabled = true;
        ASPxPageControl1.ActiveTabIndex = 0;
        ASPxPageControl1.TabPages[1].Enabled = false;
        ASPxPageControl1.DataBind();
    }

    protected void AlterarSetor(string Setor, string SIGLA)
    {
        msg = Cad.AlterarSetor(Setor, SIGLA, Convert.ToInt32(gridSetor.GetRowValues(gridSetor.FocusedRowIndex, "IDSetor").ToString()),Convert.ToInt32(Session["IDEmpresa"]),Convert.ToInt32(cbStatus.SelectedItem.Value));
        this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('" + msg + "');</script>");
    }

    protected void ApagaCampos()
    {
        tbDescricaoSetor.Text = "";
        tbSiglaSetor.Text = "";
        cbStatus.Text = string.Empty;
    }
}