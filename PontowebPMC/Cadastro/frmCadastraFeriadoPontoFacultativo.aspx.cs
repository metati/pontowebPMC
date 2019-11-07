using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetodosPontoFrequencia;

public partial class Cadastro_frmCadastraFeriadoPontoFacultativo : System.Web.UI.Page
{
    /// <summary>
    /// Feriados e pontos facultativos agora fazem parte da tabela TBDiasAno ...
    /// </summary>

    private MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.TBDiasAnoTableAdapter adpDiasAno = new MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.TBDiasAnoTableAdapter();
    public Cadastro Cad = new Cadastro();
    public PreencheTabela PT = new PreencheTabela();
    DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
    public string msg = "";

    protected void AlianhaFeriado()
    {
        msg = Cad.DiasAnoS(PT.FeriadosSAD());
        this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('" + msg + "');</script>");
        PreencheGrid();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated == false)
        {
            Response.Redirect("~/Account/Login.aspx");
        }
        else if (!IsPostBack)
        {
            if (Session["TPUsuario"].ToString() != "1")
            {
                cbFeriados.Visible = false;
                btOkFeriado.Visible = false;
            }
            pgFeriadoPontoFacultativo.TabPages[0].Enabled = false;
            //ddlTPFeiradaoPontoFacultativo.Items.Insert(0, "");
            //PreencheDrop();
        }
        
        PreencheGrid();
    }

    public void PreencheGrid()
    {
        //PT.PreencheTBFeriadoPontoFacultativo(ds);
        //PT.PreenchevwFeriadoPontoFacultativo(ds);
       
        adpDiasAno.FillIDEmpresa(ds.TBDiasAno, Convert.ToInt32(Session["IDEmpresa"]));

        gridFeriadoPontoFacultativo.DataSource = ds;
        gridFeriadoPontoFacultativo.DataBind();
    }

    //public void PreencheDrop()
   // {
        //PT.PreencheTBTPFeriadoPontoFacultativo(ds);
        //ddlTPFeiradaoPontoFacultativo.DataSource = ds;
        //ddlTPFeiradaoPontoFacultativo.DataBind();
    //}

    protected void btVoltar_Click(object sender, EventArgs e)
    {
        Response.Redirect("./Default.aspx");
    }

    protected void btAlterar_Click(object sender, EventArgs e)
    {
        PreencheCampos();
        pgFeriadoPontoFacultativo.TabPages[0].Enabled = true;
        pgFeriadoPontoFacultativo.TabPages[1].Enabled = false;
        Session["OperacaoPontoFacultativo"] = 2;
        pgFeriadoPontoFacultativo.ActiveTabIndex = 0;
        pgFeriadoPontoFacultativo.DataBind();
    }

    protected void btLista_Click(object sender, EventArgs e)
    {
        pgFeriadoPontoFacultativo.TabPages[0].Enabled = false;
        pgFeriadoPontoFacultativo.ActiveTabIndex = 1;
        pgFeriadoPontoFacultativo.TabPages[1].Enabled = true;
        pgFeriadoPontoFacultativo.DataBind();
    }

    protected void CadastraFeriadoPontoFacultativo(string DsFeriadoPontoFacultativo, DateTime DTFeriadoPontoFacultativo, int IDTPFeriadoPontoFacultativo, int IDEmpresa)
    {
        try
        {
            adpDiasAno.UpdateDiasAno(DTFeriadoPontoFacultativo, true, DsFeriadoPontoFacultativo, Convert.ToInt32(Session["IDEmpresa"]), DTFeriadoPontoFacultativo);
            msg = "Registro incluído com sucesso.";
        }
        catch (Exception ex)
        {
            ex.ToString();
            msg = "Falha ao incluir o registro. Tente novamente.";
        }
        //msg = Cad.CadastraFeriadoPontoFacultativo(DsFeriadoPontoFacultativo, DTFeriadoPontoFacultativo, IDTPFeriadoPontoFacultativo, Convert.ToInt32(Session["IDEmpresa"]));
        this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('" + msg + "');</script>");
    }

    protected void btVoltarCad_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }

    protected void PreencheCampos()
    {
        tbDescricaoFeriado.Text = gridFeriadoPontoFacultativo.GetRowValues(gridFeriadoPontoFacultativo.FocusedRowIndex, "OBS").ToString();
        deFeriadoPontoFacultativo.Date = Convert.ToDateTime(gridFeriadoPontoFacultativo.GetRowValues(gridFeriadoPontoFacultativo.FocusedRowIndex, "DTDiasAno").ToString());
        //ddlTPFeiradaoPontoFacultativo.SelectedIndex = Convert.ToInt32(gridFeriadoPontoFacultativo.GetRowValues(0,"IDTPFeriadoPontoFacultativo").ToString());
    }

    protected void btSalvar_Click(object sender, EventArgs e)
    {
        if ((int)Session["OperacaoPontoFacultativo"] == 1)
        {
            CadastraFeriadoPontoFacultativo(tbDescricaoFeriado.Text,deFeriadoPontoFacultativo.Date,0,Convert.ToInt32(Session["IDEmpresa"]));
            ApagaCampos();
        }
        else if ((int)Session["OperacaoPontoFacultativo"] == 2)
        {
            AlterarFeriadoPontoFacultativo(tbDescricaoFeriado.Text, deFeriadoPontoFacultativo.Date, 0);
            ApagaCampos();
        }

    }

    protected void btCadastrar_Click(object sender, EventArgs e)
    {
        ApagaCampos();
        Session["OperacaoPontoFacultativo"] = 1;
        pgFeriadoPontoFacultativo.TabPages[0].Enabled = true;
        pgFeriadoPontoFacultativo.ActiveTabIndex = 0;
        pgFeriadoPontoFacultativo.TabPages[1].Enabled = false;
        pgFeriadoPontoFacultativo.DataBind();
    }

    protected void AlterarFeriadoPontoFacultativo(string DSFeriadoPontoFacultativo, DateTime DTFeriadoPontoFacultativo, int IDTPFeriadoPontoFacultativo)
    {
        try
        {
            if (Convert.ToDateTime(gridFeriadoPontoFacultativo.GetRowValues(gridFeriadoPontoFacultativo.FocusedRowIndex, "DTDiasAno")) != DTFeriadoPontoFacultativo)
            {
                adpDiasAno.UpdateDiasAno(Convert.ToDateTime(gridFeriadoPontoFacultativo.GetRowValues(gridFeriadoPontoFacultativo.FocusedRowIndex, "DTDiasAno")), false, string.Empty, Convert.ToInt32(Session["IDEmpresa"]), Convert.ToDateTime(gridFeriadoPontoFacultativo.GetRowValues(gridFeriadoPontoFacultativo.FocusedRowIndex, "DTDiasAno")));
                adpDiasAno.UpdateDiasAno(DTFeriadoPontoFacultativo, true, DSFeriadoPontoFacultativo, Convert.ToInt32(Session["IDEmpresa"]), DTFeriadoPontoFacultativo);
                msg = "Registro alterado com sucesso.";
            }
            else
            {
                adpDiasAno.UpdateDiasAno(DTFeriadoPontoFacultativo, true, DSFeriadoPontoFacultativo, Convert.ToInt32(Session["IDEmpresa"]), DTFeriadoPontoFacultativo);
                msg = "Registro alterado com sucesso.";
            }

        }
        catch (Exception ex)
        {
            ex.ToString();
            msg = "Falha ao alterar o registro. Tente novamente.";
        }
        //msg = Cad.AlteraFeriadoPontoFacultativo(DSFeriadoPontoFacultativo, DTFeriadoPontoFacultativo, Convert.ToInt32(gridFeriadoPontoFacultativo.GetRowValues(gridFeriadoPontoFacultativo.FocusedRowIndex, "IDFeriadoPontoFacultativo").ToString()), IDTPFeriadoPontoFacultativo, Convert.ToInt32(Session["IDEmpresa"]));
        this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('" + msg + "');</script>");
    }

    protected void ApagaCampos()
    {
        tbDescricaoFeriado.Text = "";
        deFeriadoPontoFacultativo.Text = "";
        //ddlTPFeiradaoPontoFacultativo.SelectedIndex = 0;
    }
    protected void pgFeriadoPontoFacultativo_ActiveTabChanged(object source, DevExpress.Web.ASPxTabControl.TabControlEventArgs e)
    {

    }
    protected void btOkFeriado_Click(object sender, EventArgs e)
    {
        AlianhaFeriado();
    }
}