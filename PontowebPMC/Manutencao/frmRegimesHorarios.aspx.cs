using DevExpress.Web.ASPxGridView;
using MetodosPontoFrequencia.HorasExtras;
using MetodosPontoFrequencia.Model;
using System;
using System.Web;

public partial class Manutencao_frmRegimesHorarios : System.Web.UI.Page
{
    RegimeHorasDAO rhd = new RegimeHorasDAO();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated == false)
        {
            Response.Redirect("~/Account/Login.aspx");
        }
        else if (!IsPostBack)
        {
            GetDados();
            divForm.Visible = false;
        }

    }

    protected void btFilter_Click(object sender, EventArgs e)
    {
        GetDados();
    }


    private void GetDados()
    {
        gridRegimes.DataSource = rhd.GetRegimes();
        gridRegimes.DataBind();
        if (gridRegimes.Columns.IndexOf(gridRegimes.Columns["check"]) == -1)
        {
            GridViewCommandColumn col = new GridViewCommandColumn();
            col.ShowSelectCheckbox = true;
            col.Name = "check";
            gridRegimes.Columns.Add(col);
        }
    }

    protected void btNovo_Click(object sender, EventArgs e)
    {
        divLista.Visible = false;
        divForm.Visible = true;
        textoForm.InnerText = "Cadastro de Regime de Horas";
    }


    protected void btAlterar_Click(object sender, EventArgs e)
    {
  
        var list = gridRegimes.GetSelectedFieldValues("Codigo");
        if (list.Count > 0)
        {
            divLista.Visible = false;
            divForm.Visible = true;
            textoForm.InnerText = "Alterar Regime de Horas";
            var item = rhd.GetRegime(list[0].ToString());
            txtCodigo.Text = item.IDRegimeHora;
            txtDescricao.Text = item.DSRegimeHora;
            txtTotalHoraSemana.Text = item.TotalHoraSemana;
            txtTotalHoraDia.Text = item.TotalHoraDia;
            txtTotalMaxHoraDia.Text = item.TotalMaximoHoraExtraDia;
            txtTotalMaxHoraMes.Text = item.TotalMaximoHoraExtraMes;
            txtTotalHorasFolga.Text = item.TotalHorasFolgaPlantonista;
            if (item.RegimePlantonista == "True")
            {
                cbxRegime.Checked = true;
            }
            else
            {
                cbxRegime.Checked = false;
            }

            if (item.PermitehoraExtra == "True")
            {
                cbxHorasExtra.Checked = true;
            }
            else
            {
                cbxHorasExtra.Checked = false;
            }
        }
        else
        {
            Mensagem("Favor selecionar um registro!");
        }
    }

    protected void btExcluir_Click(object sender, EventArgs e)
    {
        var list = gridRegimes.GetSelectedFieldValues("Codigo");
        if (list.Count > 0)
        {
            rhd.Deletar(list[0].ToString());
            LimparControles();
            GetDados();
        }
        else
        {
            Mensagem("Favor selecionar um registro!");
        }
    }

    protected void btSalvar_Click(object sender, EventArgs e)
    {
        divLista.Visible = true;
        divForm.Visible = false;
        RegimeModel rm = new RegimeModel();
        rm.IDRegimeHora = txtCodigo.Text;
        rm.DSRegimeHora = txtDescricao.Text;
        rm.TotalHoraSemana = txtTotalHoraSemana.Text;
        rm.TotalHoraDia = txtTotalHoraDia.Text;
        rm.TotalMaximoHoraExtraDia = txtTotalMaxHoraDia.Text;
        rm.TotalMaximoHoraExtraMes = txtTotalMaxHoraMes.Text;
        rm.TotalHorasFolgaPlantonista = txtTotalHorasFolga.Text;
        if (cbxRegime.Checked)
        {
            rm.RegimePlantonista = "1";
        }
        else
        {
            rm.RegimePlantonista = "0";
        }

        if (cbxHorasExtra.Checked)
        {
            rm.PermitehoraExtra = "1";
        }
        else
        {
            rm.PermitehoraExtra = "0";
        }

        if (string.IsNullOrEmpty(txtCodigo.Text))
        {
            rhd.Salvar(rm);
            LimparControles();
            GetDados();
        }
        else
        {
            rhd.Alterar(rm);
            LimparControles();
            GetDados();
        }
    }

    protected void btCancelar_Click(object sender, EventArgs e)
    {
        divLista.Visible = true;
        divForm.Visible = false;
    }

    private void Mensagem(string texto)
    {
        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + texto + "');", true);
    }

    private void LimparControles()
    {
        txtCodigo.Text = "";
        txtDescricao.Text = "";
        txtTotalHoraSemana.Text = "";
        txtTotalHoraDia.Text = "";
        txtTotalMaxHoraDia.Text = "";
        txtTotalMaxHoraMes.Text = "";
        txtTotalHorasFolga.Text = "";
    }
}