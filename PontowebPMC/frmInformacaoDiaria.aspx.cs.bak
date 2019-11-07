using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetodosPontoFrequencia;

public partial class frmInformacaoDiaria : System.Web.UI.Page
{
    protected DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
    protected PreencheTabela PT = new PreencheTabela();
    protected Cadastro Cad = new Cadastro();
    protected string msg ;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated == false)
        {
            Response.Redirect("/WebPontoFrequencia/Account/Login.aspx");
        }
        else if (!IsPostBack)
        {
            PreencheddlSetor();
            ddlSetor.Items.Insert(0, "Selecione um Setor");
        }

        if (Convert.ToInt32(Session["TPUsuario"]) == 3)
        {
            PreencheGrid(Convert.ToInt32(Session["IDSETOR"]));
            ddlSetor.SelectedValue = Convert.ToString(Session["IDSETOR"]);
            ddlSetor.Enabled = false;
        }
        else
        {
            PreencheGrid(0);
        }
    }

    protected void PreencheddlSetor()
    {
        PT.PreencheTBSetor(ds);
        ddlSetor.DataSource = ds;
        ddlSetor.DataBind();
    }

    protected void PreencheGrid(int IDSetor)
    {
        if (IDSetor <= 0)
        {
            PT.PreenchevwInformacaoDiaria(ds);
            gridInformacaoDiaria.DataSource = ds;
            gridInformacaoDiaria.DataBind();
        }
        else
        {
            PT.PreenchevwInformacaoDiariaIDSetor(ds, IDSetor);
            gridInformacaoDiaria.DataSource = ds;
            gridInformacaoDiaria.DataBind();
        }
    }

    protected void SalvaInformacaoDiaria()
    {
        if (cbSetores.Checked == true)
        {
            PT.PreencheTBSetor(ds);

            for (int i = 0; i <= (ds.TBSetor.Rows.Count - 1); i++)
            {
                //msg = Cad.CadastraInformacaoDiaria(memoInformacao.Text, ds.TBSetor[i].IDSetor, DateTime.Now.Date);
            }

            this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('"+ msg +"');</script>");
        }
        else
        {
           //msg = Cad.CadastraInformacaoDiaria(memoInformacao.Text, ddlSetor.SelectedIndex, DateTime.Now.Date);
           
           this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('" + msg + "');</script>");

        }
    }

    protected void AlteraInformacaoDiaria()
    {
        if (cbSetores.Checked == true)
        {
            for (int i = 0; i <= (ds.TBSetor.Rows.Count - 1); i++)
            {
                //msg = Cad.AlteraInformacaoDiaria(memoInformacao.Text, ds.TBSetor[i].IDSetor, DateTime.Now.Date, Convert.ToInt32(gridInformacaoDiaria.GetRowValues(gridInformacaoDiaria.FocusedRowIndex, "TBInformacaoDiaria").ToString()));
            }
            
            this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('" + msg + "');</script>");
        }
        else
        {
            //msg = Cad.AlteraInformacaoDiaria(memoInformacao.Text, ddlSetor.SelectedIndex, DateTime.Now.Date, Convert.ToInt32(gridInformacaoDiaria.GetRowValues(gridInformacaoDiaria.FocusedRowIndex, "TBInformacaoDiaria").ToString()));
            this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('" + msg + "');</script>");
        }
    }
    protected void btSalvar_Click(object sender, EventArgs e)
    {
        SalvaInformacaoDiaria();
    }
    protected void gridInformacaoDiaria_BeforeColumnSortingGrouping(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
    {
        if (Convert.ToInt32(Session["TPUsuario"]) == 3)
        {
            PreencheGrid(Convert.ToInt32(Session["IDSETOR"]));
            ddlSetor.SelectedValue = Convert.ToString(Session["IDSETOR"]);
            ddlSetor.Enabled = false;
        }
        else
        {
            PreencheGrid(0);
        }
    }
}