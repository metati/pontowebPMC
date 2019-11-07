using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetodosPontoFrequencia;
using System.Xml;
using DevExpress.Web.ASPxGridView;

public partial class Cadastro_frmCadastraUsuario : System.Web.UI.Page
{
    public DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
    public string msg = "",criterioFiltro="";
    public PreencheTabela PT = new PreencheTabela();
    public Cadastro Cad = new Cadastro();
    private string Nome,primeiroNome;
    protected byte[] ImagemByte;
    private int cont;
    private int IDuserFoto;
    private string[] Gerencias;
	private string NomeUser = string.Empty;
    //private string linhaExpandida = null;
	private string filtro;

	
	    #region Trata Apostulo
    protected string TrataNome(DevExpress.Web.ASPxGridView.GridViewDataItemTemplateContainer Container)
    {
		if (Session["linhaExpandida"].ToString() != null || filtro != null)
        {
            int posicao;
            try
            {
                NomeUser = gridUsuario.GetRowValues(Convert.ToInt32(Session["linhaExpandida"].ToString()), "DSUsuario").ToString().Trim();
                if (NomeUser.IndexOf("'") > 0)
                {
                    posicao = NomeUser.IndexOf("'");
                    NomeUser = NomeUser.Substring(0, posicao) + "1" + NomeUser.Substring(posicao+1, (NomeUser.Length - (posicao + 1)));
                }

            }
            finally
            {
            
            }
        }

        //linhaExpandida = null;
		filtro = null;
        return NomeUser;
    }
    #endregion
    
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
                coOperacao.Value = "";
                tpCadastro.ActiveTabIndex = 0;
                Session["OperacaoFiltroUsuario"] = "Inclusao";
                Session["IDUsuarioF"] = "";
                
                if (Convert.ToInt32(Session["TPUsuario"]) == 3)
                {
                    btDesfazer.EnableClientSideAPI = false;
                    btDesfazer.DataBind();
                }
                cotpuser.Add("tpuser", Convert.ToString(Session["TPUsuario"]));

                if (Convert.ToString(Session["TPUsuario"]) == "1" || Convert.ToString(Session["TPUsuario"]) == "7" || Convert.ToString(Session["TPUsuario"]) == "8")
                {
                    cbEspecial.Visible = true;
                    cbEspecial.DataBind();
                }

                PreencheDropSetor();
                PreencheDropCargo();
                PreencheDropStatus();
                PreenchecbTPUsuario();
                //PreencheDropSetorCadastro();
                //PreencheCbVinculo();
                PreencheCBLSetorGEstor();
                PreenchecbRegimeHora();
                //teEntradaTarde.Value = "13:00";
                //PreenchecbGrauInstr();
                //PreenchecbGenero();
            }
        }
    }

    protected void CadastraVinculoUsuario(int IDusuario, int IDSetor, int IDEmpresa, int IDVinculoUsuario, int idtpusuario, int idcargo,
        int identidade, int IDRegimeHora, string HoraEntradaManha, string HoraSaidaManha, 
        string HoraEntradaTarde, string HoraSaidaTarde,bool PainelDashboards, bool ManutencaoDigital)
    {
        if (PT.PermitePreenchevwVinculoUsuarioEmpresa(IDusuario, IDEmpresa, IDSetor))
        {
            //N�o esquecer de acrescentar a matricula posteriormente
            //Acrescentado em 04/07/2018
           msg = Cad.CadastraVinculoUsuario2(IDusuario, idtpusuario, IDEmpresa, IDRegimeHora, IDSetor, DateTime.Now, idcargo, identidade, ManutencaoDigital,
                PainelDashboards, HoraEntradaManha, HoraSaidaManha, HoraEntradaTarde, HoraSaidaTarde, Convert.ToInt32(Session["IDUsuario"]),1,tbPIS.Text.Trim(),cbDescontoTotalJornada.Checked,cbIsencaoPonto.Checked);

            if(msg.IndexOf("J� existente") > (0))
                Page.ClientScript.RegisterStartupScript(Page.GetType(), "MSG", @"<script language='javascript'> AbrepopErroKey();</script>");
        }
        else
        {
            Page.ClientScript.RegisterStartupScript(Page.GetType(), "MSG", @"<script language='javascript'> AbrepopErroKey();</script>");
        }
    }
    
    protected void AlteraVinculoUsuario(int IDusuario, int IDSetor, int IDEmpresa, int IDVinculoUsuario, int idtpusuario, int idcargo,
        int identidade, int IDRegimeHora, string HoraEntradaManha, string HoraSaidaManha, 
        string HoraEntradaTarde, string HoraSaidaTarde,bool PainelDashboards, bool ManutencaoDigital,int idstatus)
    {
        //N�o esquecer de acrescentar m�tricula posteriormente.
        //Acrescentado em 04/07/2018
        Cad.AlteraVinculoUsuario(IDusuario, idtpusuario, IDEmpresa, IDRegimeHora, IDSetor, DateTime.Now, idcargo, identidade, ManutencaoDigital, PainelDashboards,
            HoraEntradaManha, HoraSaidaManha, HoraEntradaTarde, HoraSaidaTarde, IDVinculoUsuario, 
            idstatus,tbPIS.Text.Trim(),cbDescontoTotalJornada.Checked,cbIsencaoPonto.Checked);
    }

    protected void PreenchegridVinculoUsuario(int idusuario, int idsetor,int IDEmpresa, int IDTPUsuario)
    {
        if (idusuario != 0)
        {
            //if(IDTPUsuario == 1 || IDTPUsuario == 7)
            //{
                //PT.PreenchevwVinculoUsuarioEmpresa(ds, idusuario, IDEmpresa, idsetor);    
            //}
            //else if (IDTPUsuario == 3)
            //{
                PT.PreenchevwVinculoUsuarioEmpresa(ds,Convert.ToInt32(coIDVinculoUsuario.Value));
            //}
        }
        gridVinculoUsuario.DataSource = ds;
        gridVinculoUsuario.DataBind();
    }
    
    protected void PreenchecbRegimeHora()
    {
        PT.PreencheRegiomeHora(ds);
        cbRegimeHora.DataSource = ds;
        cbRegimeHora.DataBind();
    }
    
    protected void PreenchecbGenero()
    {
        //PT.pReencheTBGenero(ds);
        //cbGenero.DataSource = ds;
        //cbGenero.DataBind();
    }

    protected void PreenchecbGrauInstr()
    {
        //PT.pReencheTBGrauInstr(ds);
        //cbGrauInstr.DataSource = ds;
        //cbGrauInstr.DataBind();
    }

    protected void PreencheCBLSetorGEstor()
    {
        PT.PreencheTBSetorIDEmpresa(ds, Convert.ToInt32(Session["IDEmpresa"]));
        cbLSetores.DataSource = ds.TBSetor;
        cbLSetores.SelectAll();
        cbLSetores.DataBind();

        //DropdownEditors
        
    }

    protected void PreencheDropSetor()
    {
        ds.EnforceConstraints = false;

        if (Convert.ToInt32(Session["TPUsuario"]) == 3)
        {
            //PT.PreencheTBSetorIDSetor(ds, Convert.ToInt32(Session["IDSETOR"]), Convert.ToInt32(Session["IDEmpresa"]));
            PT.PreencheVWGestorSetor(ds, Convert.ToInt32(Session["IDEmpresa"]), Convert.ToInt32(Session["IDUsuario"]));

            cbSetor.DataSource = ds.vwGestorSetor;
            cbSetorPrincipal.DataSource = ds.vwGestorSetor;
        }
        else
        {
            PT.PreencheTBSetorIDEmpresa(ds, Convert.ToInt32(Session["IDEmpresa"]));
            cbSetor.DataSource = ds.TBSetor;
            cbSetorPrincipal.DataSource = ds.TBSetor;
        }

        cbSetor.DataBind();
        cbSetorPrincipal.DataBind();
    }
    protected void PreencheDropCargo()
    {
        PT.PreencheTBCargo(ds);
        cbCargo.DataSource = ds;
        cbCargo.DataBind();
    }
    //protected void PreencheDropSetorCadastro()
    //{
    //    PT.PreencheTBEntidade(ds);
    //    cbVinculo.DataSource = ds;
    //    cbVinculo.DataBind();

    //}
    
    protected void PreencheDropStatus()
    {
        PT.PreencheTBStatus(ds);
        cbStatus.DataSource = ds;
        cbStatus.DataBind();
    }

    protected void PreenchecbTPUsuario()
    {

        if (Convert.ToInt32(Session["TPUsuario"]) == 3 ) // Se administrador de �rg�o, preenche da mesma forma que o gestor.
        {
            PT.PreencheTipoUsuarioFunc(ds);
        }
        else if ((Convert.ToInt32(Session["TPUsuario"]) == 7) || (Convert.ToInt32(Session["TPUsuario"]) == 8))
        {
            PT.PreencheTipoUsuarioAdminOrgao(ds);    
        }
        else
        {
            PT.PreencheTipoUsuario(ds);
        }

        cbTPUsuario.DataSource = ds;
        cbTPUsuario.DataBind();
    }
    //protected void PreencheCbVinculo()
    //{
    //    PT.PreencheTBEntidade(ds);
    //    cbVinculo.DataSource = ds;
    //    cbVinculo.DataBind();
    //}

    protected void btBuscar_Click(object sender, EventArgs e)
    {

    }

    protected void Preenchegrid(bool Detalhe)
    {
        switch (Detalhe)
        {
            case true:
                PT.PreenchevwVinculoUsuarioEmpresa(ds, 688, 4, 7);
                break;
            case false:
                if (cbSetorPrincipal.Text == "" && ((string)Session["TPUsuario"] == "1" || (string)Session["TPUsuario"] == "7") || ((string)Session["TPUsuario"] == "8"))
                {
                    //Por aqui
                    //if (criterioFiltro == "")
                    if ((string)Session["TPUsuario"] == "1")
                    {
                        PT.PreenchevwUsuarioGridAdmin(ds, Convert.ToInt32(Session["IDEmpresa"]));

                    }
                    else
                    {
                        PT.PreenchevwUsuarioGridAdminOrgao(ds, Convert.ToInt32(Session["IDEmpresa"]));
                    }
                    //else
                    //PT.PreenchevwUsuarioDSSetor(ds, Convert.ToInt32(Session["IDEmpresa"]), criterioFiltro);
                }
                else if (((string)Session["TPUsuario"] == "1" || (string)Session["TPUsuario"] == "7" || (string)Session["TPUsuario"] == "8") && cbSetorPrincipal.Text != "")
                {
                    if ((string)Session["TPUsuario"] == "1")
                        PT.PreenchevwUsuarioGridAdminSetor(ds, Convert.ToInt32(Session["IDEmpresa"]), Convert.ToInt32(cbSetorPrincipal.SelectedItem.Value));
                    else
                        PT.PreenchevwUsuarioGridAdminOrgaoSetor(ds, Convert.ToInt32(Session["IDEmpresa"]), Convert.ToInt32(cbSetorPrincipal.SelectedItem.Value));

                    gridUsuario.PageIndex = 0;
                }
                else if ((string)Session["TPUsuario"] == "3")
                {
                    PT.PreenchevwUsuarioGridAdminGestor(ds, Convert.ToInt32(Session["IDEmpresa"]), Convert.ToInt32(cbSetorPrincipal.Value));
                    gridUsuario.PageIndex = 0;
                }
                break;
        }          
        
        gridUsuario.DataSource = ds;
        gridUsuario.DataBind();
    }

    protected void gridUsuario_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        Preenchegrid(false);
    }
    protected void gridUsuario_PageIndexChanged(object sender, EventArgs e)
    {
        Preenchegrid(false);
    }

    protected void gridUsuario_ProcessColumnAutoFilter(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewAutoFilterEventArgs e)
    {
        //criterioFiltro = "";
        if (e.Value != "")
        {
            criterioFiltro = e.Value.ToString();
            filtro = e.Value.ToString();

        }
        Preenchegrid(false);
    }
    protected void btVoltar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }
    protected void btCadastrar_Click(object sender, EventArgs e)
    {

        Response.Redirect("~/Cadastro/frmCadastraUsuario.aspx");
    }
    protected void btAlterar_Click(object sender, EventArgs e)
    {
            PreencheCampos();
    }
    protected void PreencheCampos()
    {
        try
        {
            Session["OperacaoFiltroUsuario"] = "Alteracao";

            
            //gridUsuario
            //Session["IDUsuarioFiltro"] = GridUsuario.GetRowValues(GridUsuario.FocusedRowIndex, "IDUsuario").ToString();
            //Session["IDStatus"] = GridUsuario.GetRowValues(GridUsuario.FocusedRowIndex, "IDStatus").ToString();
            //Session["PrimeiroNomeFiltro"] = GridUsuario.GetRowValues(GridUsuario.FocusedRowIndex, "PrimeiroNome").ToString().Trim();
            //Session["DSUsuarioFiltro"] = GridUsuario.GetRowValues(GridUsuario.FocusedRowIndex, "DSUsuario").ToString().Trim();
            //Session["login"] = GridUsuario.GetRowValues(GridUsuario.FocusedRowIndex, "Login").ToString().Trim();
            //Session["IDSetor"] = GridUsuario.GetRowValues(GridUsuario.FocusedRowIndex, "IDSetor").ToString();
            //Session["IDTPUsuario"] = GridUsuario.GetRowValues(GridUsuario.FocusedRowIndex, "IDTPUsuario").ToString();
            //Session["IDEntidade"] = GridUsuario.GetRowValues(GridUsuario.FocusedRowIndex, "IDEntidade").ToString();
            //Session["IDCargo"] = GridUsuario.GetRowValues(GridUsuario.FocusedRowIndex, "IDCargo").ToString();
            //Session["TotalHoras"] = GridUsuario.GetRowValues(GridUsuario.FocusedRowIndex, "TotHorasDiarias").ToString();
            //Session["HoraEntradaManha"] = GridUsuario.GetRowValues(GridUsuario.FocusedRowIndex, "HoraEntradaManha").ToString();
            //Session["HoraEntradaTarde"] = GridUsuario.GetRowValues(GridUsuario.FocusedRowIndex, "HoraEntradaTarde").ToString();
            //Session["HoraSaidaManha"] = GridUsuario.GetRowValues(GridUsuario.FocusedRowIndex, "HoraSaidaManha").ToString();
            //Session["HoraSaidaTarde"] = GridUsuario.GetRowValues(GridUsuario.FocusedRowIndex, "HoraSaidaTarde").ToString();
            //Session["SenhaDigital"] = GridUsuario.GetRowValues(GridUsuario.FocusedRowIndex, "SenhaDigital").ToString();
            //Session["AcessoEspecial"] = GridUsuario.GetRowValues(GridUsuario.FocusedRowIndex, "AcessoEspecial").ToString();
            
            //if (GridUsuario.GetRowValues(gridUsuarioDetalhe.FocusedRowIndex, "FotoUsuario").ToString() != "")
                //Session["FotoUsuario"] = (Byte[])gridUsuarioDetalhe.GetRowValues(gridUsuarioDetalhe.FocusedRowIndex, "FotoUsuario");
        }
        catch(Exception ex)
        {
            Session["OperacaoFiltroUsuario"] = "";
            Session["IDUsuarioFiltro"] = "";
            Session["IDStatus"] = "";
            Session["PrimeiroNomeFiltro"] = "";
            Session["DSUsuarioFiltro"] = "";
            Session["login"] = "";
            Session["IDSetor"] = "";
            Session["IDTPUsuario"] = "";
            Session["IDEntidade"] = "";
            Session["IDCargo"] = "";
            Session["TotalHoras"] = "";
            Session["HoraEntradaManha"] = "";
            Session["HoraEntradaTarde"] = "";
            Session["HoraSaidaManha"] = "";
            Session["HoraSaidaTarde"] = "";
            Session["SenhaDigital"] = "";
            Session["AcessoEspecial"] = "";

            this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('Informe um usu�rio para realizar altera��es');</script>");
            ex.ToString();
        }
        string tt = Convert.ToString(Session["IDUsuarioFiltro"]);
        if (Convert.ToString(Session["IDUsuarioFiltro"]) != "")
        {
            Response.Redirect("~/Cadastro/frmCadastraUsuario.aspx");
        }
        else
        {
            this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('Informe um usu�rio para realizar altera��es');</script>");
        }
    }

    protected void gridUsuario_CustomCallback1(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        Preenchegrid(false);
    }

    protected void gridUsuario_BeforeColumnSortingGrouping(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
    {
        Preenchegrid(false);
    }

    protected void ASPxButton1_Click(object sender, EventArgs e)
    {
        Response.Write("~/Cadastrso/frmfoto.aspx");
    }
    protected void popManutencaoUsuario_WindowCallback(object source, DevExpress.Web.ASPxPopupControl.PopupWindowCallbackArgs e)
    {
        //ImgFotoUsuario.ContentBytes = (byte[]) coHash.Value;
    }

    protected void btSalvar_Click(object sender, EventArgs e)
    {
        if (coPermiteExecutar.Value == "True")
        {
            Cadastro Cad = new Cadastro();

            //Inclui apenas o v�nculo
            if (tbCPFMatricula.Text != "" && coOperacao.Value == "Incluir")
            {
                PT.PreencheTBUsuarioLogin(ds, tbCPFMatricula.Text.Trim());

                if (ds.TBUsuario.Rows.Count > 0)
                {
                    //N�O ESQUECER DA MATRICULA
                    msg = Cad.CadastraVinculoUsuario(ds.TBUsuario[0].IDUsuario, Convert.ToInt32(cbTPUsuario.Value), 
                        Convert.ToInt32(Session["IDEmpresa"]),
                    Convert.ToInt32(cbRegimeHora.Value), Convert.ToInt32(cbSetor.Value), 
                    DateTime.Now, Convert.ToInt32(cbCargo.Value),
                    17, cbCadastraDigital.Checked, cbDashboard.Checked, tbEntradaManha.Text.Trim(),
                    tbSaidaManha.Text.Trim(), tbEntradaTarde.Text.Trim(), 
                    tbSaidaTarde.Text.Trim(), Convert.ToInt32(Session["IDUsuario"]),tbPIS.Text.Trim(),
                    cbDescontoTotalJornada.Checked,cbIsencaoPonto.Checked);

                    int t = msg.IndexOf("J� existente");

                    if (msg.IndexOf("J� existente") > (-1))
                        Page.ClientScript.RegisterStartupScript(Page.GetType(), "MSG", @"<script language='javascript'> AbrepopErroKey();</script>");
                    
                    return;
                }
            }

            if (coCPF.Value == "True")
            {
                primeiroNome = PrimeiroNome(tbNomeCompleto.Text.Trim().ToUpper());

                if(cbTPUsuario.SelectedIndex > 0)
                {
                    if (cbTPUsuario.SelectedItem.Value.ToString() == "3" || cbTPUsuario.SelectedItem.Value.ToString() == "9")
                    {
                        Gerencias = new string[cbLSetores.SelectedItems.Count];

                        for (int i = 0; i <= (cbLSetores.SelectedItems.Count - 1); i++)
                        {
                            Gerencias[i] = cbLSetores.SelectedItems[i].Value.ToString();
                        }
                    }
                }
                try
                {
                    if (coOperacao.Value == "Incluir")
                    {
                        //NAO ESQUECER DA MATRICULA
                        msg = Cad.CadastraUsuario(tbCPFMatricula.Text.Trim().ToUpper(), null, tbNomeCompleto.Text.Trim().ToUpper(),
                            17, Convert.ToInt32(cbStatus.SelectedItem.Value), Convert.ToInt32(cbSetor.SelectedItem.Value), Convert.ToInt32(cbTPUsuario.SelectedItem.Value),
                               Convert.ToInt32(cbCargo.SelectedItem.Value), Convert.ToInt32(tbTotHoras.Text),
                               tbEntradaManha.Text, tbSaidaManha.Text.Trim(), tbEntradaTarde.Value.ToString(), tbSaidaTarde.Text, 1,
                               primeiroNome, Convert.ToInt32(Session["IDUsuario"]), "0", rbAcessoEspecial.Checked,
                               Convert.ToInt32(Session["IDempresa"]), Convert.ToDateTime("01/01/1900"),
                               Convert.ToDateTime("01/01/1900"),tbPIS.Text.Trim(), 1,
                               Convert.ToDateTime("01/01/1900"), string.Empty
                               , string.Empty, string.Empty, Convert.ToDateTime("01/01/1900"), string.Empty,
                               string.Empty, string.Empty, Convert.ToDateTime("01/01/1900"),
                               string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
                               string.Empty, 1, string.Empty, Gerencias, cbDashboard.Checked, 
                               cbCadastraDigital.Checked, cbPermiteDiaSeguinte.Checked, 
                               Convert.ToInt32( cbRegimeHora.Value),tbTelSMS.Text.Trim(),tbPIS.Text.Trim(),
                               cbIsencaoPonto.Checked );

                        if (msg.IndexOf("J� existete") > (0))
                            Page.ClientScript.RegisterStartupScript(Page.GetType(), "MSG", @"<script language='javascript'> AbrepopErroKey();</script>");

                    }
                    else if (coOperacao.Value == "Alterar")
                    {
                        int po = tbNomeCompleto.Text.IndexOf("", 2);

                        if (rbSenha.Checked)
                        {
                            Cad.AlteraUsuarioAdminSenha(tbCPFMatricula.Text.Trim().ToUpper(), tbNomeCompleto.Text.Trim().ToUpper(),
                                17,
                                Convert.ToInt32(cbStatus.SelectedItem.Value), Convert.ToInt32(cbSetor.SelectedItem.Value),
                                Convert.ToInt32(cbTPUsuario.SelectedItem.Value), Convert.ToInt32(cbCargo.SelectedItem.Value),
                                Convert.ToInt32(tbTotHoras.Text), tbEntradaManha.Text.Trim(), tbSaidaManha.Text.Trim(),
                                tbEntradaTarde.Text.Trim(), tbSaidaTarde.Text.Trim(), 1, primeiroNome,
                                Convert.ToInt32(coIDUsuario.Value), "pontonarede", Convert.ToInt32(cbTPUsuario.SelectedItem.Value),
                                Convert.ToInt32(cbCargo.SelectedItem.Value), Convert.ToInt32(Session["IDUsuario"]), "1",
                                rbAcessoEspecial.Checked, Convert.ToInt32(Session["IDEmpresa"]),
                                Convert.ToDateTime("01/01/1900"), Convert.ToDateTime("01/01/1900"),
                                tbPIS.Text.Trim(),
                                1,
                                Convert.ToDateTime("01/01/1900"), string.Empty,
                                string.Empty, string.Empty, Convert.ToDateTime("01/01/1900"),
                                string.Empty, string.Empty, string.Empty, Convert.ToDateTime("01/01/1900"),
                                string.Empty, string.Empty,
                                string.Empty, string.Empty, string.Empty, string.Empty,
                                string.Empty, 1, string.Empty, Gerencias,cbDashboard.Checked,
                                cbCadastraDigital.Checked,cbPermiteDiaSeguinte.Checked,tbTelSMS.Text.Trim());
                        }
                        else
                        {
                            string VLSenha = "";
                            if (cbEspecial.Checked)
                                VLSenha = "1";
                            else
                                VLSenha = "0";

                            msg = Cad.AlteraUsuarioAdmin(tbCPFMatricula.Text.Trim().ToUpper(), tbNomeCompleto.Text.Trim().ToUpper(),
                                17,
                                Convert.ToInt32(cbStatus.SelectedItem.Value), Convert.ToInt32(cbSetor.SelectedItem.Value),
                                Convert.ToInt32(cbTPUsuario.SelectedItem.Value),
                                    Convert.ToInt32(cbCargo.SelectedItem.Value), Convert.ToInt32(tbTotHoras.Text),
                                    tbEntradaManha.Text, tbSaidaManha.Text, tbEntradaTarde.Text, tbSaidaTarde.Text, 2,
                                    primeiroNome, Convert.ToInt32(coIDUsuario.Value), Convert.ToInt32(cbTPUsuario.SelectedItem.Value),
                                    Convert.ToInt32(cbCargo.SelectedItem.Value), Convert.ToInt32(Session["IDUsuario"]), VLSenha,
                                    rbAcessoEspecial.Checked, Convert.ToInt32(Session["IDEmpresa"]),
                                    Convert.ToDateTime("01/01/1900"), Convert.ToDateTime("01/01/1900"),tbPIS.Text.Trim(),
                                    1, Convert.ToDateTime("01/01/1900"), "",
                                    "", "", Convert.ToDateTime("01/01/1900"),
                                    "", "", "",
                                    Convert.ToDateTime("01/01/1900"),
                                    "", "", "", "",
                                    "", "", "", 1, "", Gerencias, cbDashboard.Checked,
                                    cbCadastraDigital.Checked,cbPermiteDiaSeguinte.Checked,tbTelSMS.Text.Trim(),cbIsencaoPonto.Checked);

                            if (msg.IndexOf("J� existe") > (0))
                                Page.ClientScript.RegisterStartupScript(Page.GetType(), "MSG", @"<script language='javascript'> AbrepopErroKey();</script>");

                        }
                    }
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }                
                Preenchegrid(false);
            }
        }//Permite Executar
    }

    protected string PrimeiroNome(string NomeCompleto)
    {
        if (NomeCompleto.IndexOf(' ') > 0)
        {
            while (NomeCompleto[cont] != ' ')
            {
                Nome = Nome + NomeCompleto[cont];
                cont++;
            }
        }
        else
        {
            Nome = NomeCompleto;
        }
        
        return Nome;
    }
    protected void upFoto_FileUploadComplete(object sender, DevExpress.Web.ASPxUploadControl.FileUploadCompleteEventArgs e)
    {
        string ll = coIDUsuario.Value;
        SalvarImagem((e.UploadedFile));
    }

    void SalvarImagem(DevExpress.Web.ASPxUploadControl.UploadedFile ArquivoUpLoad)
    {

        if (ArquivoUpLoad.IsValid)
        {
            ImagemByte = new byte[ArquivoUpLoad.PostedFile.InputStream.Length + 1];

            ArquivoUpLoad.PostedFile.InputStream.Read(ImagemByte, 0, ImagemByte.Length);

            SubirFoto(Convert.ToInt32(coIDUsuario.Value), Convert.ToInt32(Session["IDEmpresa"]), ImagemByte);
        }
    }

    public void SubirFoto(int IDUsuario, int IDEmpresa, Byte[] Foto)
    {
        Cad.SubirFoto(IDUsuario, IDEmpresa, Foto); //Adaptar para subir Imagem da empresa aqui. N�o permitir que usu�rios, nem que sejam administradores, subir imagens da empresa.
    }
    protected void cpPreencheItens_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        if (cbLSetores.Items.Count > 0)
        {
            
            if(e.Parameter == "PreencherCBL") // Se estiver preeenchendo para informa��o - Caso n�o a checklist j� foi mexida.
            {

                cbLSetores.UnselectAll(); //Para poder Selecionar todos novamentes - Atualiza a lista de setores para os novos selecionados
                
                MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.TBGestorSetorTableAdapter adpGestor = new MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.TBGestorSetorTableAdapter();
                adpGestor.FillIDUsuario(ds.TBGestorSetor, Convert.ToInt32(coIDUsuario.Value));

                if (ds.TBGestorSetor.Rows.Count > 0)
                {
                    for (int y = 0; y <= (ds.TBGestorSetor.Rows.Count - 1); y++)
                    {
                        for (int i = 0; i <= (cbLSetores.Items.Count - 1); i++)
                        {
                            if (ds.TBGestorSetor[y].IDSetor.ToString() == cbLSetores.Items[i].Value.ToString())
                            {
                                cbLSetores.Items[i].Selected = true;
                                cbLSetores.DataBind();
                                //coGestorSetor.Add(ds.TBGestorSetor[y].IDSetor.ToString(), ds.TBGestorSetor[y].IDSetor);
                            }
                        }
                    }
                }
            }
            else //caso n�o tenha nd na tabela - Ser� cadastrado com o valor do setor que h� na combo - Seja a opera��o Inclus�o ou Altera��o.
            {
                if (cbSetor.Text != string.Empty && cbLSetores.SelectedItems.Count == 0)
                {
                    for (int i = 0; i <= (cbLSetores.Items.Count - 1); i++)
                    {
                        if (cbSetor.Value.ToString() == cbLSetores.Items[i].Value.ToString())
                        {
                            cbLSetores.Items[i].Selected = true;
                            cbLSetores.DataBind();
                            //coGestorSetor.Add(ds.TBGestorSetor[y].IDSetor.ToString(), ds.TBGestorSetor[y].IDSetor);
                        }
                    }
                }
            }
        }
    }
    protected void cpRegimeHora_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        if (e.Parameter.ToString() != " ")
            tbTotHoras.Text = "08";
        tbTotHoras.DataBind();

    }
    protected void gridVinculoUsuario_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        //Apagar as linhas da grid
        //RemoverItensGrid(gridVinculoUsuario);

        if (coOperacaoVinculo.Value == "Alteracao" && e.Parameters != "" && e.Parameters != "Zerar")
        {
            //Altera Vinculo selecionado na grid
            AlteraVinculoUsuario(Convert.ToInt32(coIDUsuario.Value), Convert.ToInt32(cbSetor.Value), Convert.ToInt32(Session["IDEmpresa"]), Convert.ToInt32(coIDVinculoUsuario.Value),
                Convert.ToInt32(cbTPUsuario.Value), Convert.ToInt32(cbCargo.Value),17, Convert.ToInt32(cbRegimeHora.Value), tbEntradaManha.Text.Trim(),
                tbSaidaManha.Text.Trim(), tbEntradaTarde.Text.Trim(), tbSaidaTarde.Text.Trim(), cbDashboard.Checked, cbCadastraDigital.Checked, Convert.ToInt32(cbStatus.Value));

            //For�ar a execu��o do bot�o Salvar -- Caso o usu�rio n�o clique no mesmo.
            coPermiteExecutar.Value = "True";
            coCPF.Value = "True";
            btSalvar_Click(sender,e);

            PreenchegridVinculoUsuario(Convert.ToInt32(coIDUsuario.Value), Convert.ToInt32(coIDSetorVinculoUsuario["IDSetor"]), Convert.ToInt32(Session["IDEmpresa"]),Convert.ToInt32(Session["TPUsuario"]));
        }
        else if (coOperacaoVinculo.Value == "Inclusao" && coOperacao.Value != "Incluir")
        {
            CadastraVinculoUsuario(Convert.ToInt32(coIDUsuario.Value), Convert.ToInt32(cbSetor.Value), Convert.ToInt32(Session["IDEmpresa"]), 0,
                Convert.ToInt32(cbTPUsuario.Value), Convert.ToInt32(cbCargo.Value), 17, Convert.ToInt32(cbRegimeHora.Value), tbEntradaManha.Text.Trim(),
                tbSaidaManha.Text.Trim(), tbEntradaTarde.Text.Trim(), tbSaidaTarde.Text.Trim(), cbDashboard.Checked, cbCadastraDigital.Checked);

            //For�ar a execu��o do bot�o Salvar -- Caso o usu�rio n�o clique no mesmo.
            coPermiteExecutar.Value = "True";
            coCPF.Value = "True";
            btSalvar_Click(sender, e);

            PreenchegridVinculoUsuario(Convert.ToInt32(coIDUsuario.Value), Convert.ToInt32(coIDSetorVinculoUsuario["IDSetor"]), Convert.ToInt32(Session["IDEmpresa"]), Convert.ToInt32(Session["TPUsuario"]));
        }
        else if (coOperacaoVinculo.Value == "Inclusao" && coOperacao.Value == "Incluir" && cbSetor.Text != "")
        {
            
            ds.vwVinculoUsuario.AddvwVinculoUsuarioRow(Convert.ToInt32(Session["IDEmpresa"]), "", "", 0, 0, Convert.ToInt32(cbSetor.Value), cbSetor.Text.Trim(), "",
                cbTPUsuario.Text.ToString(), Convert.ToInt32(cbTPUsuario.Value), Convert.ToInt32(cbRegimeHora.Value), cbRegimeHora.Text.Trim(), "",
                1, 0, Convert.ToInt32(tbTotHoras.Text), cbCadastraDigital.Checked, cbDashboard.Checked, tbSaidaTarde.Text, tbSaidaManha.Text, tbEntradaTarde.Text,
                tbEntradaManha.Text, 17, Convert.ToInt32(cbCargo.Value), DateTime.Now, Convert.ToDateTime("1900-01-01"), cbCargo.Text, "CARREIRA/COMISSIONADO",
                cbStatus.Text,tbPIS.Text.Trim(),cbDescontoTotalJornada.Checked,cbIsencaoPonto.Checked);

            PreenchegridVinculoUsuario(0, Convert.ToInt32(cbSetor.Value), Convert.ToInt32(Session["IDEmpresa"]), Convert.ToInt32(Session["TPUsuario"]));
        }
        else if (e.Parameters != "Zerar")
            PreenchegridVinculoUsuario(Convert.ToInt32(coIDUsuario.Value), Convert.ToInt32(coIDSetorVinculoUsuario["IDSetor"]), Convert.ToInt32(Session["IDEmpresa"]), Convert.ToInt32(Session["TPUsuario"]));
        else
            PreenchegridVinculoUsuario(0, 0, 0, Convert.ToInt32(Session["TPUsuario"]));


    }
    protected void gridVinculoUsuario_PageIndexChanged(object sender, EventArgs e)
    {
        if (gridVinculoUsuario.VisibleRowCount > 0)
            PreenchegridVinculoUsuario(Convert.ToInt32(coIDUsuario.Value), Convert.ToInt32(coIDSetorVinculoUsuario["IDSetor"]), Convert.ToInt32(Session["IDEmpresa"]), Convert.ToInt32(Session["TPUsuario"]));
    }
    protected void gridVinculoUsuario_BeforeColumnSortingGrouping(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
    {
        if (gridVinculoUsuario.VisibleRowCount > 0)
            PreenchegridVinculoUsuario(Convert.ToInt32(coIDUsuario.Value), Convert.ToInt32(coIDSetorVinculoUsuario["IDSetor"]), Convert.ToInt32(Session["IDEmpresa"]), Convert.ToInt32(Session["TPUsuario"]));
    }
    protected void gridVinculoUsuario_ProcessColumnAutoFilter(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewAutoFilterEventArgs e)
    {
        if(gridVinculoUsuario.VisibleRowCount > 0)
            PreenchegridVinculoUsuario(Convert.ToInt32(coIDUsuario.Value), Convert.ToInt32(coIDSetorVinculoUsuario["IDSetor"]), Convert.ToInt32(Session["IDEmpresa"]), Convert.ToInt32(Session["TPUsuario"]));
    }
    protected void pcVinculoUsuario_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        if (e.Parameter == "RegimeHora")
        {
            //RegHora Para regime de expediente = 1, para Regime Plant�o = 0
            switch (cbRegimeHora.Value.ToString())
            {
                case "1":
                    tbTotHoras.Text = "8";
                    tbEntradaManha.Text = "08:00";
                    tbEntradaTarde.Text = "14:00";
                    tbSaidaManha.Text = "12:00";
                    tbSaidaTarde.Text = "18:00";
                    coRegHora.Value = "1";
                    break;
                case "2":
                    tbTotHoras.Text = "6";
                    tbEntradaManha.Text = "07:00";
                    tbEntradaTarde.Text = "13:00";
                    tbSaidaManha.Text = "13:00";
                    tbSaidaTarde.Text = "19:00";
                    coRegHora.Value = "1";
                    break;
                case "3":
                    tbTotHoras.Text = "12";
                    tbEntradaManha.Text = "06:00";
                    tbEntradaTarde.Text = "14:00";
                    tbSaidaManha.Text = "18:00";
                    tbSaidaTarde.Text = "02:00";
                    coRegHora.Value = "0";
                    break;
                case "4":
                    tbTotHoras.Text = "24";
                    tbEntradaManha.Text = "08:00";
                    tbEntradaTarde.Text = "15:00";
                    tbSaidaManha.Text = "08:00";
                    tbSaidaTarde.Text = "15:00";
                    coRegHora.Value = "0";
                    break;
                case "5":
                    tbTotHoras.Text = "4";
                    tbEntradaManha.Text = "08:00";
                    tbEntradaTarde.Text = "12:00";
                    tbSaidaManha.Text = "14:00";
                    tbSaidaTarde.Text = "18:00";
                    coRegHora.Value = "1";
                    break;

            }

            //Para controle visual do CheckBox gestorSetor... Deixando de usar a fun��o javascript VerificaTPUser2
            if (cbTPUsuario.Text == "Gestor" || cbTPUsuario.Text != "Confer�ncia de �rg�o")
                cbGestorSetor0.Visible = false;
        }

        //Para controle visual do CheckBox gestorSetor... Deixando de usar a fun��o javascript VerificaTPUser2
        if (e.Parameter == "GestorSetor")
        {
            if (cbTPUsuario.Value.ToString() == "3" || cbTPUsuario.Value.ToString() == "9")
                cbGestorSetor0.Visible = true;
            else
            {
                cbGestorSetor0.Visible = false;
            }
        }
    }

    protected void gridUsuarioDetalhe_DataSelect(object sender, EventArgs e)
    {
        Session["IDUSuarioSQL"] = (sender as ASPxGridView).GetMasterRowKeyValue().ToString();
        Session["IDEmpresaSQL"] = Session["IDEmpresa"];
        //PT.PreenchevwVinculoUsuarioEmpresa(ds, 688, 4, 7);
    }

    protected void gridDetalheUsuario_CustomUnboundColumnData(object sender, EventArgs e)
    {
        //PT.PreenchevwVinculoUsuarioEmpresa(ds, 688, 4, 7);
    }

    protected void RemoverItensGrid(ASPxGridView grid)
    {
        while (grid.VisibleRowCount > 0)
        {
            grid.DeleteRow(1);
        }
         
        grid.DataBind();
    }
    protected void gridUsuario_DetailRowExpandedChanged(object sender, ASPxGridViewDetailRowEventArgs e)
    {
        if (Session["TPUsuario"].ToString() == "3")
        {
            sqlGridUsuarioDetalhe.SelectParameters.Add("IDSetor", cbSetorPrincipal.Value.ToString());
            sqlGridUsuarioDetalhe.SelectCommand = "SELECT * FROM [vwUsuariogrid] WHERE (([IDEmpresa] = @IDEmpresa) AND ([IDUsuario] = @IDUsuario) AND ([IDSetor] = @IDSetor))";
            sqlGridUsuarioDetalhe.DataBind();
        }
		Session["linhaExpandida"] = e.VisibleIndex.ToString();
    }
    protected void coBuscaSEAP_CustomCallback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {

    }
    protected void sqlGridUsuarioDetalhe_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {

    }
}