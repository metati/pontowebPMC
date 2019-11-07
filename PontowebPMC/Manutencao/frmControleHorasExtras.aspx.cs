using DevExpress.Web.ASPxGridView;
using MetodosPontoFrequencia;
using System;
using System.Web;

public partial class Manutencao_frmControleHorasExtras : System.Web.UI.Page
{
    DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
    PreencheTabela PT = new PreencheTabela();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated == false)
        {
            Response.Redirect("~/Account/Login.aspx");
        }
        else if (!IsPostBack)
        {
            deDataInicio.Value = DateTime.Now.Date;
            PreencheddlSetor();
        }

    }


    protected void PreencheddlSetor()
    {
        if (Convert.ToInt32(Session["TPUsuario"]) != 2 || Convert.ToInt32(Session["TPUsuario"]) == 4)
        {
            if (Convert.ToInt32(Session["TPUsuario"]) == 3)
            {
                //PT.PreencheTBSetor(ds);

                //PT.PreencheVWGestorSetor(ds, Convert.ToInt32(Session["IDEmpresa"]), Convert.ToInt32(Session["IDUsuario"]));
                //PT.PreencheTBSetorIDSetor(ds, Convert.ToInt32(Session["IDSETOR"]), Convert.ToInt32(Session["IDEmpresa"]));
                PT.PreencheTBSetorGestor(ds, Convert.ToInt32(Session["IDUsuario"]), Convert.ToInt32(Session["IDEmpresa"]));

                //cbSetor.DataSource = ds.vwGestorSetor;
            }
            else if (Convert.ToInt32(Session["TPUsuario"]) == 1 || Convert.ToInt32(Session["TPUsuario"]) == 8 || Convert.ToInt32(Session["TPUsuario"]) == 9)
            {
                PT.PreencheTBSetorIDEmpresa(ds, Convert.ToInt32(Session["IDEmpresa"]));

                //cbSetor.DataSource = ds.TBSetor;
            }
        }
        else
        {
            cbSetor.Visible = false;
        }

        cbSetor.DataSource = ds;
        cbSetor.IncrementalFilteringMode = DevExpress.Web.ASPxEditors.IncrementalFilteringMode.Contains;
        cbSetor.DataBind();
    }


    protected void cbEmpresa_SelectedIndexChanged(object sender, EventArgs e)
    {
        PreencheddlSetor();
    }

    protected void cbSetor_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ObjectDataSource1.Select();
        //ObjectDataSource1.DataBind();
        GetDados();
    }

    protected void btNovo_Click(object sender, EventArgs e)
    {
        var list = gridUsuario.GetSelectedFieldValues("Codigo");
        if (list.Count > 0)
        {
            MetodosPontoFrequencia.HorasExtras.PermissoesDAO pdo = new MetodosPontoFrequencia.HorasExtras.PermissoesDAO();
            string dataFim = "";
            try
            {
                dataFim = deDataFim.Value.ToString();
            }
            catch { }
            pdo.IncluirPermissoes(list, deDataInicio.Value.ToString(), dataFim, Session["IDUsuario"].ToString());
            LimpaControles();
        }
        else {
            Mensagem("Seleciona um colaborador!");
        }
    }

    protected void btFilter_Click(object sender, EventArgs e)
    {
        GetDados();
    } 

    private void LimpaControles()
    {
        deDataInicio.Value = DateTime.Now.Date;
        deDataFim.Value = "";
        Mensagem("Salvo com sucesso");
    }

    private void Mensagem(string texto)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + texto + "');", true);
    }


    private void GetDados()
    {
        MetodosPontoFrequencia.HorasExtras.PermissoesDAO pdo = new MetodosPontoFrequencia.HorasExtras.PermissoesDAO();
        string strSetor = "";
        try
        {
            strSetor = cbSetor.Value.ToString();
        }
        catch { }
        gridUsuario.DataSource = pdo.GetUsuarios(Session["IDEmpresa"].ToString(),strSetor, txtNome.Text, txtMatricula.Text);
        gridUsuario.DataBind();
        if (gridUsuario.Columns.IndexOf(gridUsuario.Columns["IDUsuario"]) == -1)
        {
            GridViewCommandColumn col = new GridViewCommandColumn();
            col.ShowSelectCheckbox = true;
            col.Name = "IDUsuario";
            col.Index = 1;
            gridUsuario.Columns.Add(col);
        }
    }
}