using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetodosPontoFrequencia;

public partial class Manutencao_frmPadraoHoras : System.Web.UI.Page
{
    protected DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
    protected Cadastro Cad = new Cadastro();
    protected PreencheTabela PT = new PreencheTabela();
    protected string msg;

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
                ddlSetor.Items.Insert(0, "");
                ddlSetorLista.Items.Insert(0, "");
                PreencheddlSetor();
                
                if(Convert.ToInt32(Session["TPUsuario"]) == 3)
                  PreencheddlUsuario(Convert.ToInt32(Session["IDSETOR"]));
                
                ddlUsuario.Items.Insert(0, "");

                ASPxPageControl1.TabPages[0].Enabled = false;

                Session["OpecaoHoras"] = "Usuario";
            }
        }
    }

    protected void PreencheddlSetor()
    {
        PT.PreencheTBSetor(ds);
        ddlSetor.DataSource = ds;

        if (Convert.ToInt32(Session["TPUsuario"]) == 3)
        {
            ddlSetor.SelectedValue = Convert.ToString(Session["IDSETOR"]);
            ddlSetor.Enabled = false;
        }
        
        ddlSetor.DataBind();

        if (Convert.ToInt32(Session["TPUsuario"]) == 3)
        {
            ddlSetorLista.SelectedValue = Convert.ToString(Session["IDSETOR"]);
            ddlSetorLista.Enabled = false;
        }

        ddlSetorLista.DataSource = ds;
        ddlSetorLista.DataBind();
    }
    protected void PreencheddlUsuario(int IDSetor)
    {
        ddlUsuario.Items.Insert(0, "");
        ddlUsuario.Items.Clear();
        
        PT.PreenchevwNomeUsuario(ds, IDSetor);
        
        ddlUsuario.DataSource = ds;
        ddlUsuario.DataBind();
    }
    protected void ControlaPage()
    {
        if (CheckBoxList1.Items[0].Selected == true)
        {
            ASPxPageControl1.TabPages[1].Enabled = true;
            ASPxPageControl1.ActiveTabIndex = 1;
            ASPxPageControl1.TabPages[0].Enabled = false;

            Session["OpecaoHoras"] = "Usuario";
        }
        else
        {
            ASPxPageControl1.TabPages[0].Enabled = true;
            ASPxPageControl1.ActiveTabIndex = 0;
            ASPxPageControl1.TabPages[1].Enabled = false;

            Session["OpecaoHoras"] = "Setor";
        }
    }
    protected void CheckBoxList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ControlaPage();
    }
    protected void ddlSetor_SelectedIndexChanged(object sender, EventArgs e)
    {
        PreencheddlUsuario(Convert.ToInt32(ddlSetor.SelectedValue));
        ddlUsuario.Items.Insert(0, "");
    }
    protected void btSalvar_Click(object sender, EventArgs e)
    {
        if (Convert.ToString(Session["OpecaoHoras"]) == "Usuario")
        {
            msg = Cad.AlteraHorasUsuario(Convert.ToInt32(ddlUsuario.SelectedValue), "", tbEntradaManha.Text, tbEntradaTarde.Text, tbSaidaManha.Text, tbSaidaTarde.Text, Convert.ToInt32(Session["IDUsuario"]), Convert.ToInt32(Session["IDEmpresa"]));

            this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('" + msg + "');</script>");
        }
        else
        {
            msg = Cad.AlteraSetorHoras(ddlSetorLista.SelectedIndex, "", Convert.ToDateTime(tbEntradaManhaSetor.Text), Convert.ToDateTime(tbEntradaTardeSetor.Text), Convert.ToDateTime(tbSaidaManhaSetor.Text), Convert.ToDateTime(tbSaidaTardeSetor.Text), Convert.ToInt32(Session["IDEmpresa"]));
            
            this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('" + msg + "');</script>");
        }
    }
    protected void btVoltar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }
}