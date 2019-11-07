using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetodosPontoFrequencia;

public partial class Cadastro_frmLancarFerias : System.Web.UI.Page
{
    protected Cadastro Cad = new Cadastro();
    protected PreencheTabela PT = new PreencheTabela();
    DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
    protected string msg = "";
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated == false)
        {
            Response.Redirect("~/Account/Login.aspx");
        }
        else if (!IsPostBack)
        {
            ddlSetor.Items.Insert(0, "");
            PreencheddlSetor();
            ddlSetorLista.Items.Insert(0, "");
            PreencheddlSetorLista();
            PreencheddlUsuario(Convert.ToInt32(Session["IDSETOR"]));
            ddlUsuario.Items.Insert(0, "");
            tbFerias.TabPages[0].Enabled = false;
        }
    }

    protected void PreencheddlSetor()
    {
        if (Convert.ToInt32(Session["TPUsuario"]) == 3)
        {
            ddlSetor.SelectedIndex = Convert.ToInt32(Session["IDSETOR"]);
            ddlSetor.Enabled = false;
        }
        
        PT.PreencheTBSetor(ds);
        ddlSetor.DataSource = ds;
        ddlSetor.DataBind();
    }

    protected void PreencheddlSetorLista()
    {
        if (Convert.ToInt32(Session["TPUsuario"]) == 3)
        {
            int al = Convert.ToInt32(Session["IDSETOR"]);
            ddlSetorLista.SelectedIndex = Convert.ToInt32(Session["IDSETOR"]);
            ddlSetorLista.Enabled = false;
        }

        PT.PreencheTBSetor(ds);
        ddlSetorLista.DataSource = ds;
        ddlSetorLista.DataBind();

        if (Convert.ToInt32(Session["TPUsuario"]) == 3)
        {
            PreencheGrid();
        }
    }

    protected void PreencheddlUsuario(int IDSetor)
    {
       PT.PreenchevwNomeUsuario(ds, IDSetor);
        
       ddlUsuario.DataSource = ds;
       ddlUsuario.DataBind();
    }

    protected void PreencheGrid()
    {
        PT.PreenchevwFeriasIDSetor(ds, ddlSetorLista.SelectedIndex);
        gridFerias.DataSource = ds;
        gridFerias.DataBind();
    }

    protected void PreencheCampos()
    {
        if (gridFerias.FocusedRowIndex >= 0)
        {
            ddlUsuario.SelectedValue = gridFerias.GetRowValues(gridFerias.FocusedRowIndex, "IDUsuario").ToString();
            ddlSetor.SelectedIndex = Convert.ToInt32(gridFerias.GetRowValues(gridFerias.FocusedRowIndex, "IDSetor").ToString());
             
            
            deInicioFerias.Date = Convert.ToDateTime(gridFerias.GetRowValues(gridFerias.FocusedRowIndex, "DTInicioFerias1").ToString());
            deFimFerias.Date = Convert.ToDateTime(gridFerias.GetRowValues(gridFerias.FocusedRowIndex, "DTFimFerias1").ToString());

            Session["IDFeriasL"] = Convert.ToInt32(gridFerias.GetRowValues(gridFerias.FocusedRowIndex, "IDFerias").ToString());

            tbFerias.TabPages[0].Enabled = true;
            tbFerias.TabPages[1].Enabled = false;

            Session["OperacaoFerias"] = 2;
            tbFerias.ActiveTabIndex = 0;
            tbFerias.DataBind();
        }
        else
        {
            this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('Selecione um Registro Válido.');</script>");
        }
        
    }

    protected void LimpaCampos()
    {
        if (Convert.ToInt32(Session["TPUsuario"]) == 3)
        {
            ddlSetor.SelectedIndex = Convert.ToInt32(Session["IDSETOR"]);
        }
        else
        { 
            ddlSetor.SelectedIndex = 0;
        }
        
        ddlUsuario.SelectedIndex = 0;
        deInicioFerias.Text = "";
        deFimFerias.Text = "";
    }
    protected void ddlSetor_SelectedIndexChanged(object sender, EventArgs e)
    {
        PreencheddlUsuario(ddlSetor.SelectedIndex);
    }
    protected void btSalvar_Click(object sender, EventArgs e)
    {
        //string msg;
        if (Convert.ToInt32(Session["OperacaoFerias"]) == 1)
        {
          //  msg = Cad.CadastraFerias(Convert.ToInt32(ddlUsuario.SelectedItem.Value), 1, deInicioFerias.Date, deFimFerias.Date);
        }
        else
        {
           // msg = Cad.AlteraFerias(Convert.ToInt32(ddlUsuario.SelectedItem.Value), 1, deInicioFerias.Date, deFimFerias.Date,Convert.ToInt32(Session["IDFeriasL"]));
        }

        this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('" + msg + "');</script>");
    }
    protected void ddlSetorLista_SelectedIndexChanged(object sender, EventArgs e)
    {
        PreencheGrid();
    }
    protected void btCadastrar_Click(object sender, EventArgs e)
    {
        LimpaCampos();
        Session["OperacaoFerias"] = 1;

        tbFerias.TabPages[0].Enabled = true;
        tbFerias.ActiveTabIndex = 0;
        tbFerias.TabPages[1].Enabled = false;
        tbFerias.DataBind();
    }
    protected void btAlterar_Click(object sender, EventArgs e)
    {
        PreencheCampos();
    }
    protected void btVoltarS_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }
    protected void btListar_Click(object sender, EventArgs e)
    {
        tbFerias.TabPages[0].Enabled = false;
        tbFerias.ActiveTabIndex = 1;
        tbFerias.TabPages[1].Enabled = true;
        tbFerias.DataBind();

        if (Convert.ToInt32(Session["TPUsuario"]) == 3)
        {
            ddlSetorLista.SelectedIndex = Convert.ToInt32(Session["IDSETOR"]);
            PreencheGrid();
        }
        else
        {
            ddlSetorLista.SelectedIndex = 0;
        }
        

        LimpaCampos();
    }
}