using DevExpress.Web.ASPxGridView;
using MetodosPontoFrequencia;
using System;
using System.Collections.Generic;
using System.Web;

public partial class Relatorio_EspelhoPonto : System.Web.UI.Page
{
    DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
    PreencheTabela PT = new PreencheTabela();
    MetaTI.Util.HorasExtras.PermissoesDAO pdo = new MetaTI.Util.HorasExtras.PermissoesDAO();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated == false)
        {
            Response.Redirect("~/Account/Login.aspx");
        }
        else if (!IsPostBack)
        {
            PreencheddlSetor();
            divDetalhes.Visible = false;
            ddlMes.SelectedValue = (DateTime.Now.Month - 1).ToString();
        }

    }


    protected void PreencheddlSetor()
    {
        if (Convert.ToInt32(Session["TPUsuario"]) == 3)
        {
            //PT.PreencheTBSetor(ds); -- cookie
            //PT.PreencheTBSetorIDSetor(ds, Convert.ToInt32(Session["IDSETOR"]), Convert.ToInt32(Session["IDEmpresa"]));
            PT.PreencheTBSetorGestor(ds, Convert.ToInt32(Session["IDUsuario"]), Convert.ToInt32(Session["IDEmpresa"]));
        }
        else
        {
            PT.PreencheTBSetorIDEmpresa(ds, Convert.ToInt32(Session["IDEmpresa"]));
        }

        cbSetor.DataSource = ds;

        cbSetor.IncrementalFilteringMode = DevExpress.Web.ASPxEditors.IncrementalFilteringMode.Contains;
        ;
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

    protected void btFilter_Click(object sender, EventArgs e)
    {
        GetDados();
    }

    protected void btFilterLacamento_Click(object sender, EventArgs e)
    {
        GetDadosHoras();
    }

    private void LimpaControles()
    {
        gridUsuario.DataSource = null;
        gridUsuario.DataBind();
        Mensagem("Salvo com sucesso");
    }

    private void Mensagem(string texto)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + texto + "');", true);
    }


    private void GetDados()
    {
        string strSetor = "";
        try
        {
            strSetor = cbSetor.Value.ToString();
        }
        catch { }
        gridUsuario.DataSource = pdo.GetUsuarios(Session["IDEmpresa"].ToString(), strSetor, txtNome.Text, txtMatricula.Text);
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


    private void GetDadosHoras()
    {
        List<object> valor = new List<object>();

        gridHoras.DataSource = pdo.GetLista(detCodigo.InnerText, ddlMes.SelectedValue);
        gridHoras.DataBind();
    }

    protected void btnSelect_Click(object sender, EventArgs e)
    {
        var list = gridUsuario.GetSelectedFieldValues("Codigo");
        if (divFuncionarios.Visible == true)
        {
            if (list.Count > 0)
            {
                divFuncionarios.Visible = false;
                btnSelect.Text = "SELECIONAR OUTRO COLABORADOR";
                #region InfoServidor
                List<object> valor;
                valor = gridUsuario.GetSelectedFieldValues("Codigo");
                detCodigo.InnerText = valor[0].ToString();
                detRegime.InnerText = "Regime de Horário: " + pdo.GetDescRegime(valor[0].ToString());
                valor = gridUsuario.GetSelectedFieldValues("DSUsuario");
                detServidor.InnerText = valor[0].ToString();
                valor = gridUsuario.GetSelectedFieldValues("Matricula");
                detMatricula.InnerText = valor[0].ToString();
                #endregion InfoServidor
                divDetalhes.Visible = true;
                GetDadosHoras();
            }
            else
            {
                Mensagem("FOVOR SELECIONAR UM COLABORADOR!");
            }
        }
        else
        {
            divFuncionarios.Visible = true;
            divDetalhes.Visible = false;
            btnSelect.Text = "VISUALIZAR ESPELHO DO PONTO";
            PreencheddlSetor();
        }

    }

    protected void btDeletar_Click(object sender, EventArgs e)
    {

    }

}