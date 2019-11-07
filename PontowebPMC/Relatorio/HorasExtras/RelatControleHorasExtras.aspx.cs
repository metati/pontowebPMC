using DevExpress.Web.ASPxGridView;
using MetodosPontoFrequencia;
using System;
using System.Web;

public partial class Relatorio_HorasExtras_RelatControleHorasExtras : System.Web.UI.Page
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
            PreencheddlSetor();
        }

    }

    protected void cbSetor_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDados();
    }


    protected void btDeletar_Click(object sender, EventArgs e)
    {
        try
        {

            var list = gridUsuario.GetSelectedFieldValues("Codigo");
            if (list.Count > 0)
            {
                MetodosPontoFrequencia.HorasExtras.PermissoesDAO pdo = new MetodosPontoFrequencia.HorasExtras.PermissoesDAO();
                pdo.DeletarPermissoes(list);
            }
        }
        catch { }
        finally
        {
            GetDados();
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

    protected void btFilter_Click(object sender, EventArgs e)
    {
        GetDados();
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
        gridUsuario.DataSource = pdo.GetPermissoes(Session["IDEmpresa"].ToString(), strSetor, txtNome.Text, txtMatricula.Text);
        gridUsuario.DataBind();
        if (gridUsuario.Columns.IndexOf(gridUsuario.Columns["Deletar"]) == -1)
        {
            GridViewCommandColumn col = new GridViewCommandColumn();
            col.ShowSelectCheckbox = true;
            col.Name = "Deletar";
            col.Index = 0;
            gridUsuario.Columns.Add(col);
        }
    }
}