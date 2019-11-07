using DevExpress.Web.ASPxGridView;
using MetodosPontoFrequencia;
using System;
using System.Collections.Generic;
using System.Web;

public partial class Manutencao_frmHorasExtras : System.Web.UI.Page
{
    DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
    PreencheTabela PT = new PreencheTabela();
    MetaTI.Util.HorasExtras.PermissoesDAO pdo = new MetaTI.Util.HorasExtras.PermissoesDAO();
    MetaTI.Util.HorasExtras.HorasExtrasDAO hdo = new MetaTI.Util.HorasExtras.HorasExtrasDAO();

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
            divDetalhes.Visible = false;
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

    protected void btNovo_Click(object sender, EventArgs e)
    {
        string idUsuario = detCodigo.InnerText;
        if (!string.IsNullOrEmpty(idUsuario))
        {
            try
            {
                hdo.Incluir(idUsuario, rbAno.Value.ToString(), deDataInicio.Value.ToString(), txtHorasDia.Text, Session["IDUsuario"].ToString(), Session["IDEmpresa"].ToString());
                GetDadosHoras();
                LimpaControles();
                Mensagem("Salvo com sucesso!");
            }
            catch (Exception error)
            {
                string mensagem = "";
                if (string.IsNullOrEmpty(error.Message) || string.IsNullOrWhiteSpace(error.Message))
                {
                    mensagem = "Não há registro de ponto nesta data!";
                }
                else
                {
                    mensagem = error.Message;
                }
                Mensagem("Erro: " + mensagem);
            }
        }
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
        deDataInicio.Value = DateTime.Now.Date;
        txtHorasDia.Text = "";
        gridUsuario.DataSource = null;
        gridUsuario.DataBind();
        rbAno.Value = "1255";
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
        gridUsuario.DataSource = hdo.GetUsuarios(Session["IDEmpresa"].ToString(), strSetor, txtNome.Text, txtMatricula.Text);
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
        try
        {
            valor = gridUsuario.GetSelectedFieldValues("Codigo");
            valor[0].ToString();
        }
        catch
        {
            string strValor = detCodigo.InnerText.ToString();
            valor.Add(strValor);
        }

        gridHoras.DataSource = hdo.GetLista(valor[0].ToString());
        gridHoras.DataBind();
        if (gridHoras.Columns.IndexOf(gridHoras.Columns["IDUsuario"]) == -1)
        {
            GridViewCommandColumn col = new GridViewCommandColumn();
            col.ShowSelectCheckbox = true;
            col.Name = "IDUsuario";
            col.Index = 1;
            gridHoras.Columns.Add(col);
        }
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
            btnSelect.Text = "LANÇAR HORA EXTRA/ADICIONAL NOTURNO";
            PreencheddlSetor();
        }

    }

    protected void btDeletar_Click(object sender, EventArgs e)
    {
        try
        {
            var list = gridHoras.GetSelectedFieldValues("Codigo");
            if (list.Count > 0)
            {
                hdo.DeletarPermissoes(list);
            }
            GetDadosHoras();
        }
        catch
        {
            Mensagem("FAVOR SELECIONAR UM OU MAIS REGISTROS!");
        }
        finally
        {

        }
    }

}