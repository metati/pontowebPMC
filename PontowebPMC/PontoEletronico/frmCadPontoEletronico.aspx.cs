using Controlid;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxGridView.Rendering;
using MetodosPontoFrequencia.Model;
using MetodosPontoFrequencia.PontoEletronico;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PontoEletronico_frmCadPontoEletronico : System.Web.UI.Page
{

    #region Propriedades

    public int IDEmpresa;
    PontoEletronicoSrv pontoEletronicoSrv = new PontoEletronicoSrv();
    public static int acao { get; set; } //1 - salvar ,2 - alterar
    public TBPontoEletronicoModel TBPontoEletronico { get; set; }

    RepCid _rep;

    #endregion

    #region Eventos
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated == false)
        {
            Response.Redirect("~/Account/Login.aspx");
        }
        else if (!IsPostBack)
        {
            ASPxPageControlPontoE.TabPages[0].Enabled = false;
        }

        if (!string.IsNullOrEmpty(PontoID.Value))
            populaGridSetor();
        if (!string.IsNullOrEmpty(PontoIDUsuario.Value))
            populaGridUsuarioColaborador();
        popupaGrid();
        populaDropSetor();
        IDEmpresa = Convert.ToInt32(Session["IDEmpresa"]);
    }

    protected void ASPxPageControlPontoE_ActiveTabChanged(object source, DevExpress.Web.ASPxTabControl.TabControlEventArgs e)
    {

    }

    protected void gridPontoEletronico_PageIndexChanged(object sender, EventArgs e)
    {
        popupaGrid();
    }

    protected void btSalvar_Click(object sender, EventArgs e)
    {
        try
        {
            salvar();
            limparControles();
            this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('Registro Salvo com sucesso!');</script>");
        }
        catch (Exception ex)
        {
            this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('Falha ao Salvar o Registro. Tente Novamente!');</script>");
        }
    }

    protected void btLista_Click(object sender, EventArgs e)
    {
        ASPxPageControlPontoE.TabPages[0].Enabled = false;
        ASPxPageControlPontoE.ActiveTabIndex = 1;
        ASPxPageControlPontoE.TabPages[1].Enabled = true;
        ASPxPageControlPontoE.DataBind();
    }

    protected void btVoltarCad_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }

    protected void btCadastrar_Click(object sender, EventArgs e)
    {
        acao = 1;
        ASPxPageControlPontoE.TabPages[0].Enabled = true;
        ASPxPageControlPontoE.ActiveTabIndex = 0;
        ASPxPageControlPontoE.TabPages[1].Enabled = false;
        ASPxPageControlPontoE.DataBind();
    }

    protected void btAlterar_Click(object sender, EventArgs e)
    {
        acao = 2;
        populaCampos();
        ASPxPageControlPontoE.TabPages[0].Enabled = true;
        ASPxPageControlPontoE.TabPages[1].Enabled = false;
        ASPxPageControlPontoE.ActiveTabIndex = 0;
        ASPxPageControlPontoE.DataBind();
    }

    protected void btVoltar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }

    protected void btnSalvarSetor_Click(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(cbSetor.Value.ToString()))
            {
                if (pontoEletronicoSrv.ValidarSetorVinculado(Convert.ToInt32(cbSetor.Value)))
                {
                    TBPontoEletronicoSetorModel entity = new TBPontoEletronicoSetorModel();
                    entity.IDSetor = Convert.ToInt32(cbSetor.Value);
                    entity.PontoEletronicoID = Convert.ToInt32(PontoID.Value);
                    pontoEletronicoSrv.SalvarVinculoSetor(entity);
                    populaGridSetor();
                }
                else
                {
                    this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('O setor Informado já está vinculado a um Ponto Eletrônico. Por favor verifique!');</script>");
                }
            }
            else
            {
                this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('Favor informe o setor!');</script>");
            }
        }
        catch (Exception ex)
        {
            this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('Falha ao Salvar o Registro. Tente Novamente!');</script>");
        }
        cbSetor.SelectedIndex = -1;
    }

    protected void gridSetor_PageIndexChanged(object sender, EventArgs e)
    {
        populaGridSetor();
    }

    protected void Excluir_Init(object sender, EventArgs e)
    {
        var img = (ASPxImage)sender;
        int visibleIndex = (img.NamingContainer as GridViewDataItemTemplateContainer).VisibleIndex;
        img.ClientSideEvents.Click = "function(s,e) { onClick(" + visibleIndex + "); }";
    }

    protected void gridSetor_RowDeleting1(object sender, DevExpress.Web.Data.ASPxDataDeletingEventArgs e)
    {
        try
        {
            int id = Convert.ToInt32(e.Keys[0]);
            e.Cancel = true;
            pontoEletronicoSrv.ExcluirVinculoSetor(id);
            populaGridSetor();
        }
        catch (Exception ex)
        {
            this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('Falha ao excluit o Registro!');</script>");
        }

    }

    protected void gridUsuarioColaborador_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
    {
        populaGridUsuarioColaborador();
    }

    protected void gridUsuarioColaborador_PageIndexChanged(object sender, EventArgs e)
    {
        populaGridUsuarioColaborador();
    }

    protected void btnAtualizar_Click(object sender, ImageClickEventArgs e)
    {
        string cPIS;
        string cNome;
        int nCodigo;
        string cSenha;
        string cBarras;
        int nRFID;
        int priv;
        bool lOK;
        int id = Convert.ToInt32(((sender as ImageButton).Parent as GridViewDataItemTemplateContainer).KeyValue);
        TBPontoEletronicoModel pontoEletronico = new TBPontoEletronicoModel();
        pontoEletronico = pontoEletronicoSrv.GetPontoEletronicoByID(id);

        try
        {

            int ok = 0;
            int erro = 0;
            if (conexao(true, pontoEletronico))
            {
                List<TBUsuarioInfo> usuarios = pontoEletronicoSrv.GetUsuariosImportacao(id, Session["IDEmpresa"].ToString());
                foreach (var usuario in usuarios)
                {
                    cPIS = usuario.PIS_GERADO;
                    cNome = usuario.DSUsuario;
                    nCodigo = 0;
                    cSenha = string.Empty;
                    cBarras = string.Empty;
                    nRFID = 0;
                    priv = 0;
                    _rep.GravarUsuario(Convert.ToInt64(cPIS), cNome, nCodigo, cSenha, cBarras, nRFID, priv, out lOK);
                    string log;
                    if (!_rep.GetLastLog(out log))
                    {
                        erro++;
                        log = "";
                    }
                    else
                        ok++;
                }
            }
            _rep.Desconectar();
            this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('" + ok + ": atualizados, " + erro + ": não atualizados.');</script>");
        }
        catch (Exception ex)
        {
            this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('Erro ao atualizar Coloaboradores');</script>");
        }
    }

    #endregion

    #region Metodos
    private void popupaGrid()
    {
        DataTable dt = new DataTable();
        dt = pontoEletronicoSrv.GetListaPontoEletronico();
        gridPontoEletronico.DataSource = dt;
        gridPontoEletronico.DataBind();
    }

    private void populaGridSetor()
    {
        gridSetor.DataSource = pontoEletronicoSrv.GetPontoEletronicoSetor(PontoID.Value);
        gridSetor.DataBind();
    }

    private void populaGridUsuarioColaborador()
    {
        DataTable TBUsuarios = new DataTable();
        TBPontoEletronicoModel pontoEletronico = new TBPontoEletronicoModel();
        pontoEletronico = pontoEletronicoSrv.GetPontoEletronicoByID(int.Parse(PontoIDUsuario.Value));
        if (conexao(true, pontoEletronico))
        {
            int num_usuarios;
            if (_rep.CarregarUsuarios(false, out num_usuarios))
            {
                TBUsuarios = _rep.Usuarios;
                gridUsuarioColaborador.DataSource = TBUsuarios;
                gridUsuarioColaborador.DataBind();
            }
        }
        _rep.Desconectar();
    }

    private void populaCampos()
    {
        txtCodigo.Text = gridPontoEletronico.GetRowValues(gridPontoEletronico.FocusedRowIndex, "PontoEletronicoID").ToString();
        txtNome.Text = gridPontoEletronico.GetRowValues(gridPontoEletronico.FocusedRowIndex, "PontoEletronico_Nome").ToString();
        txtLocal.Text = gridPontoEletronico.GetRowValues(gridPontoEletronico.FocusedRowIndex, "PontoEletronico_Local").ToString();
        txtIp.Text = gridPontoEletronico.GetRowValues(gridPontoEletronico.FocusedRowIndex, "PontoEletronico_Ip").ToString();
        txtSenha.Text = gridPontoEletronico.GetRowValues(gridPontoEletronico.FocusedRowIndex, "PontoEletronico_Usuario").ToString();
        txtUsuario.Text = gridPontoEletronico.GetRowValues(gridPontoEletronico.FocusedRowIndex, "PontoEletronico_Senha").ToString();
        txtPorta.Text = gridPontoEletronico.GetRowValues(gridPontoEletronico.FocusedRowIndex, "PontoEletronico_Porta").ToString();
    }

    private void populaClasse()
    {
        TBPontoEletronico = new TBPontoEletronicoModel();
        if (acao == 2)
            TBPontoEletronico.PontoEletronicoID = Convert.ToInt32(txtCodigo.Text);
        TBPontoEletronico.PontoEletronico_Ip = txtIp.Text.Trim();
        TBPontoEletronico.PontoEletronico_Local = txtLocal.Text.Trim();
        TBPontoEletronico.PontoEletronico_Nome = txtNome.Text.Trim();
        TBPontoEletronico.PontoEletronico_Senha = txtSenha.Text.Trim();
        TBPontoEletronico.PontoEletronico_Usuario = txtUsuario.Text.Trim();
        TBPontoEletronico.PontoEletronico_Porta = Convert.ToInt32(txtPorta.Text.Trim());
    }

    private void limparControles()
    {
        txtCodigo.Text = string.Empty;
        txtNome.Text = string.Empty;
        txtLocal.Text = string.Empty;
        txtIp.Text = string.Empty;
        txtSenha.Text = string.Empty;
        txtUsuario.Text = string.Empty;
        txtPorta.Text = string.Empty;
    }

    private void salvar()
    {
        populaClasse();
        pontoEletronicoSrv.Salvar(TBPontoEletronico, acao);
    }

    private void populaDropSetor()
    {
        cbSetor.DataSource = pontoEletronicoSrv.GetSetor(Session["IDEmpresa"].ToString());
        cbSetor.DataBind();
    }

    private bool conexao(bool conect, TBPontoEletronicoModel dados)
    {
        if (_rep != null)
            _rep.Desconectar();
        _rep = new RepCid();
        if (_rep.Conectar(dados.PontoEletronico_Ip.Trim(), dados.PontoEletronico_Porta, (uint)0) == RepCid.ErrosRep.OK)
        {
            if (!conect)
                return true;
        }
        else
        {
            return false;
        }

        return true;
    }

    #endregion

}