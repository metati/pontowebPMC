using DevExpress.Web.ASPxGridView;
using MetodosPontoFrequencia.FechamentoFolha;
using MetodosPontoFrequencia.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FechamentoFolha_frmFechamentoFolha : System.Web.UI.Page
{
    #region Propriedades

    FechamentoFolhaSrv fechamentoFolhaSrv = new FechamentoFolhaSrv();

    #endregion


    #region Eventos
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PopularDropMes();
            cbMesHidden.Add("IDMes", "");
        }
        lbValidar.Text = string.Empty;
        lbValidar.Visible = false;
        Msg(string.Empty, false);
    }

    protected void btnAbrirSetores_Click(object sender, ImageClickEventArgs e)
    {
        if (cbSecretaria.Value != null)
        {
            IDEmpresa.Value = cbSecretaria.Value.ToString();
            popSetor.ShowOnPageLoad = true;
        }
    }

    protected void cbSecretaria_TextChanged(object sender, EventArgs e)
    {
        if (cbSecretaria.Value != null)
        {
            btnAbrirSetores.Enabled = true;
            btnAbriCargos.Enabled = true;
            IDEmpresa.Value = cbSecretaria.Value.ToString();
            chkAllCargo.Checked = false;
            chkAllSetor.Checked = false;
            PopulaSetor();
            PopulaCargo();
        }
        else
        {
            btnAbrirSetores.Enabled = false;
            btnAbriCargos.Enabled = false;
        }
    }

    protected void btnAbriCargos_Click(object sender, ImageClickEventArgs e)
    {
        if (cbSecretaria.Value != null)
            popCargo.ShowOnPageLoad = true;
    }

    protected void btnProcessar_Click(object sender, EventArgs e)
    {
        var setores = getSetoresSelecionados();
        var cargos = getCargosSelecionados();
        if (cbSecretaria.Value != null)
        {
            if (string.IsNullOrEmpty(setores) && !chkAllSetor.Checked)
            {
                lbValidar.Text = "Nenhum Setor Informado!";
                lbValidar.Visible = true;
            }
            else if (string.IsNullOrEmpty(cargos) && !chkAllCargo.Checked)
            {
                lbValidar.Text = "Nenhum Cargo Informado!";
                lbValidar.Visible = true;
            }
            else if (cbMes.Value == null)
            {
                lbValidar.Text = "Mês não Selecionado!";
                lbValidar.Visible = true;
            }
            else
            {
                lbValidar.Text = "";
                lbValidar.Visible = false;
                string retorno = fechamentoFolhaSrv.Processar(cbMes.Value.ToString(), cbSecretaria.Value.ToString(), setores, cargos);
                if (string.IsNullOrEmpty(retorno))
                {
                    cbSecretaria_TextChanged(null, null);
                    PopulaGridProcessados();
                    Msg("Folha Processada com Sucesso.", true);
                }
                else
                {
                    Msg(retorno, true);
                }
            }
        }
        else
        {
            lbValidar.Text = "Secretaria não informada!";
            lbValidar.Visible = true;
        }

    }
    protected void gridProcessados_PageIndexChanged(object sender, EventArgs e)
    {
        PopulaGridProcessados();
    }

    protected void cbMes_TextChanged(object sender, EventArgs e)
    {
        if (cbMes.Value != null)
        {
            cbSecretaria.Enabled = true;
            btnImprimir.Enabled = true;
            cbMesHidden["IDMes"] = cbMes.Value;

            PopularDropEmpresa();
            PopulaGridProcessados();
        }
        else
        {
            cbSecretaria.Enabled = false;
            btnImprimir.Enabled = false;
            Msg(string.Empty, false);
        }
    }

    protected void btnReprocessar_Click(object sender, ImageClickEventArgs e)
    {
        int id = Convert.ToInt32(((sender as ImageButton).Parent as GridViewDataItemTemplateContainer).KeyValue);
        TBFechamentoFolha folha = fechamentoFolhaSrv.GetEmpresaProcessada(id);
        List<int> setores = fechamentoFolhaSrv.GetSetoresBYEmpresaProcessados(id);
        List<int> cargos = fechamentoFolhaSrv.GetCargosBYEmpresaProcessados(id);
        string retorno = fechamentoFolhaSrv.Processar(folha.Mes.ToString(), folha.IDEmpresa.ToString(), String.Join(", ", setores.ToArray()), String.Join(", ", cargos.ToArray()), id);
        if (string.IsNullOrEmpty(retorno))
        {
            cbSecretaria_TextChanged(null, null);
            PopulaGridProcessados();
            Msg("Secretaria Reprocessada com Sucesso!", true);
        }
        else
        {
            Msg(retorno, true);
        }

    }

    #endregion

    #region Metodos

    private void Msg(string msg, bool visivel)
    {
        lbValidar.Visible = false;
        lbRetorno.Visible = visivel;
        lbRetorno.Text = msg;
    }

    private string getSetoresSelecionados()
    {
        string setores = string.Empty;

        if (!chkAllSetor.Checked)
            foreach (ListItem item in chekSetor.Items)
                if (item.Selected)
                {
                    if (setores == string.Empty)
                        setores = string.Format("{0}", item.Value);
                    else
                        setores = setores + string.Format(",{0}", item.Value);
                }

        return setores;
    }

    private string getCargosSelecionados()
    {
        string cargos = string.Empty;
        if (!chkAllCargo.Checked)
            foreach (ListItem item in chekCargo.Items)
                if (item.Selected)
                {
                    if (cargos == string.Empty)
                        cargos = string.Format("{0}", item.Value);
                    else
                        cargos = cargos + string.Format(",{0}", item.Value);
                }

        return cargos;
    }

    private void PopulaSetor()
    {
        chekSetor.DataSource = fechamentoFolhaSrv.GetSetor(cbMes.Value.ToString(), cbSecretaria.Value.ToString());
        chekSetor.DataBind();
    }

    private void PopularDropEmpresa()
    {
        cbSecretaria.DataSource = fechamentoFolhaSrv.GetEmpresa(cbMes.Value.ToString());
        cbSecretaria.DataBind();
    }

    private void PopularDropMes()
    {
        cbMes.DataSource = fechamentoFolhaSrv.GetMes();
        cbMes.DataBind();
    }

    private void PopulaCargo()
    {
        chekCargo.DataSource = fechamentoFolhaSrv.GetCargo(cbMes.Value.ToString(), cbSecretaria.Value.ToString());
        chekCargo.DataBind();
    }

    private void PopulaGridProcessados()
    {
        if (cbMes.Value != null)
        {
            gridProcessados.DataSource = fechamentoFolhaSrv.GetFechamento(cbMes.Value.ToString());
            gridProcessados.DataBind();
        }
    }

    #endregion

}


