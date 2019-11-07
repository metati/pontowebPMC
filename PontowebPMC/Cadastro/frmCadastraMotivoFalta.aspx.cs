using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetodosPontoFrequencia;

public partial class Cadastro_frmCadastraMotivoFalta : System.Web.UI.Page
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
            pgMotivoFalta.TabPages[0].Enabled = false;
            PreencheDropStatus();
        }

        PreencheGrid();
    }

    protected void PreencheDropStatus()
    {
        PT.PreencheTBStatus(ds);
        cbStatus.DataSource = ds;
        cbStatus.DataBind();
    }

    public void PreencheGrid()
    {
        PT.PreencheTBMotivoFaltaGeral(ds);
        gridMotivoFalta.DataSource = ds;
        gridMotivoFalta.DataBind();
    }

    protected void CadastraMotivoFalta(string DsMotivoFalta,bool AbonoFaltas)
    {
        msg = Cad.CadastraMotivoFalta(DsMotivoFalta,AbonoFaltas,Convert.ToInt32(cbStatus.SelectedItem.Value),0);
        this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('" + msg + "');</script>");
    }

    protected void btVoltarCad_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }

    protected void PreencheCampos()
    {
        tbDescricaoCargo.Text = gridMotivoFalta.GetRowValues(gridMotivoFalta.FocusedRowIndex, "DSMotivoFalta").ToString();
        cbAbono.Checked = Convert.ToBoolean(gridMotivoFalta.GetRowValues(gridMotivoFalta.FocusedRowIndex, "AbonarHoras").ToString());
        cbStatus.Value = gridMotivoFalta.GetRowValues(gridMotivoFalta.FocusedRowIndex, "IDStatus").ToString();
    }
    protected void btSalvar_Click(object sender, EventArgs e)
    {
        if ((int)Session["OperacaoMotivoFalta"] == 1)
        {
            CadastraMotivoFalta(tbDescricaoCargo.Text,cbAbono.Checked);
            ApagaCampos();
        }
        else if ((int)Session["OperacaoMotivoFalta"] == 2)
        {
            AlterarMotivoFalta(tbDescricaoCargo.Text,cbAbono.Checked);
            ApagaCampos();
        }

    }

    protected void btCadastrar_Click(object sender, EventArgs e)
    {
        ApagaCampos();
        Session["OperacaoMotivoFalta"] = 1;
        pgMotivoFalta.TabPages[0].Enabled = true;
        pgMotivoFalta.ActiveTabIndex = 0;
        pgMotivoFalta.TabPages[1].Enabled = false;
        cbStatus.Text = string.Empty;
        pgMotivoFalta.DataBind();
    }

    protected void AlterarMotivoFalta(string Entidade,bool AbonoFalta)
    {
        msg = Cad.AlteraMotivoFalta(Entidade, Convert.ToInt32(gridMotivoFalta.GetRowValues(gridMotivoFalta.FocusedRowIndex, "IDMotivoFalta").ToString()),AbonoFalta,Convert.ToInt32(cbStatus.SelectedItem.Value));
        this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('" + msg + "');</script>");
    }

    protected void ApagaCampos()
    {
        tbDescricaoCargo.Text = "";
        cbStatus.Text = string.Empty;
    }
    protected void btLista_Click(object sender, EventArgs e)
    {
        pgMotivoFalta.TabPages[0].Enabled = false;
        pgMotivoFalta.ActiveTabIndex = 1;
        pgMotivoFalta.TabPages[1].Enabled = true;
        pgMotivoFalta.DataBind();
    }
    protected void pgVinculo_ActiveTabChanged(object source, DevExpress.Web.ASPxTabControl.TabControlEventArgs e)
    {

    }
    protected void btAlterar_Click(object sender, EventArgs e)
    {
        PreencheCampos();
        pgMotivoFalta.TabPages[0].Enabled = true;
        pgMotivoFalta.TabPages[1].Enabled = false;
        Session["OperacaoMotivoFalta"] = 2;
        pgMotivoFalta.ActiveTabIndex = 0;
        pgMotivoFalta.DataBind();
    }
    protected void btVoltar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }
}