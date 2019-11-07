using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetodosPontoFrequencia;
using MetodosPontoFrequencia.Justificativa;

public partial class Manutencao_frmManutencaoFrequencia : System.Web.UI.Page
{
    public DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
    public Frequencia Freq = new Frequencia();
    public PreencheTabela PT = new PreencheTabela();
    Justificativa justificativa = new Justificativa();
    public int Lei;
    public bool Linhas;
    public string msg = "";
    public int TotalHorasDia;
    bool AbonoFalta;
    DateTime Horas;
    int? TotaDia;
    private string TextoConfirmacao;
    Crip crp = new Crip();
    private TimeSpan Segundos;
    //string msg;
    protected string TotalHoraDia;
    private MetodosPontoFrequencia.Justificativa.Justificativa JustPedido;
    private TimeSpan EntradaManha, SaidaManha, EntradaTarde, SaidaTarde, HEntradaManha,
        HSaidaManha, HEntradaTarde, HSaidaTarde;
    private double SomaTotal;
    private bool FormaDesconto, IsencaoPonto;
    private bool _usuarioPlantonista;
    private string NomeUser;

    public string DataDesconsidera
    {
        get
        {
            return Acesso.DataDesconsidera;
        }
    }

    public string ObrigaQuatroBatidas
    {
        get
        {
            return Acesso.ObrigaQuatroBatidas;
        }
    }


    #region Trata Apostulo
    protected string TrataNome(DevExpress.Web.ASPxGridView.GridViewDataItemTemplateContainer Container)
    {

        int posicao;
        try
        {
            NomeUser = gridFrequencia.GetRowValues(Container.VisibleIndex, "DSUsuario").ToString().Trim();
            if (NomeUser.IndexOf("'") > 0)
            {
                posicao = NomeUser.IndexOf("'");
                NomeUser = NomeUser.Substring(0, posicao) + "1" + NomeUser.Substring(posicao + 1, (NomeUser.Length - (posicao + 1)));
            }

        }
        finally
        {

        }
        return NomeUser;
    }
    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated == false)
        {
            Response.Redirect("~/Default.aspx");
        }
        else
        {
            lblegenda1.BackColor = System.Drawing.Color.CornflowerBlue;
            lblegenda1.Text = "***";
            lblegenda1.ForeColor = System.Drawing.Color.CornflowerBlue;
            lblegenda2.BackColor = System.Drawing.Color.WhiteSmoke;
            lblegenda2.ForeColor = System.Drawing.Color.WhiteSmoke;
            lblegenda2.Text = "***";
            lblegenda3.BackColor = System.Drawing.Color.Wheat;
            lblegenda3.ForeColor = System.Drawing.Color.Wheat;
            lblegenda3.Text = "***";
            lblegenda4.BackColor = System.Drawing.Color.IndianRed;
            lblegenda4.ForeColor = System.Drawing.Color.IndianRed;
            lblegenda4.Text = "***";
            lblegenda5.BackColor = System.Drawing.Color.Yellow;
            lblegenda5.ForeColor = System.Drawing.Color.Yellow;
            lblegenda5.Text = "***";
            lblegenda6.ForeColor = System.Drawing.Color.DarkSlateGray;
            lblegenda6.Text = "***";
            if (!IsPostBack)
            {
                PreencheddlSetor();
                //PreencheddlMes();
                PreencheddlMotivoFalta();
                PreencheddlMes();
                Session["IDVinculoUsuarioLinha"] = 0;
                //PreenchegridFrequencia(Convert.ToInt32(Session["IDSETOR"]), deData.Date.Date.ToShortDateString());
                //cbSetor.Value = Session["IDSETOR"];
            }
            lbAlerta.Visible = false;
            lbAlerta.Text = "";

            if (cbSetor.Text != "" && cbUsuario.Text != "")
            {
                Freq = new Frequencia();
                MetodosPontoFrequencia.DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
                Freq.DadosUsuario(ds, Convert.ToInt32(cbUsuario.Value));
                if (ds.vwUsuariogrid.Rows.Count > 0)
                {
                    coIDUsuarioSetorManut["IDusuarioFolha"] = crp.CriptograFa(ds.vwUsuariogrid[0].IDUsuario.ToString());
                    coIDUsuarioSetorManutV["IDVinculoUsuario"] = crp.CriptograFa(cbUsuario.Value.ToString());

                }
                coIDUsuarioSetorManut["IDsetorFolha"] = crp.CriptograFa(cbSetor.Value.ToString());
            }
        }
    }

    protected string FormatarData(string DTFrequencia)
    {
        if (DTFrequencia.Length == 9)
        {
            if (DTFrequencia.Substring(0, 2).IndexOf("/") == 1)
                DTFrequencia = string.Format("0{0}", DTFrequencia);
            else
            {
                DTFrequencia = string.Format("{0}0{1}", DTFrequencia.Substring(0, 3), DTFrequencia.Substring(3, 6));
            }
        }
        else if (DTFrequencia.Length == 8)
        {
            DTFrequencia = string.Format("0{0}", DTFrequencia);
            DTFrequencia = string.Format("{0}0{1}", DTFrequencia.Substring(0, 3), DTFrequencia.Substring(3, 6));
        }

        return DTFrequencia;
    }

    protected void TotalHorasAbonoFalta(int IDUsuario, int IDMotivoFalta, DateTime DataJust, string TotDia, string HorasTotais)
    {
        MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwUsuariogridTableAdapter adpUser = new MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwUsuariogridTableAdapter();
        MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.TBMotivoFaltaTableAdapter adpMOti = new MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.TBMotivoFaltaTableAdapter();

        if (hdJustificativa["TPJust"].ToString() == "2")
        {
            try
            {
                adpUser.FillIDEmpresaSetorUsuario(ds.vwUsuariogrid, Convert.ToInt32(Session["IDEmpresa"]), Convert.ToInt32(cbSetorModo.Value.ToString()), IDUsuario);

                if (ds.vwUsuariogrid.Rows.Count > 0)
                {
                    TotalHorasDia = ds.vwUsuariogrid[0].TotHorasDiarias;
                }

                adpMOti.FillIDMotivoFalta(ds.TBMotivoFalta, IDMotivoFalta);

                if (ds.TBMotivoFalta.Rows.Count > 0)
                    AbonoFalta = ds.TBMotivoFalta[0].AbonarHoras;

                if (AbonoFalta)
                {
                    TotaDia = ds.vwUsuariogrid[0].TotHorasDiarias;
                }
                else
                {
                    TotaDia = 0;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            //Caso maior não atualiza o valor da hora total
            //if (Convert.ToDateTime(HorasTotais) > TotaDia)
            //{
            //TotaDia = Convert.ToDateTime(HorasTotais);
            //}
        }

    }
    protected void TotalHorasAbonoFaltaSolicitacao(int IDUsuario, int IDMotivoFalta, DateTime DataJust, string TotDia, string HorasTotais)
    {
        MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwUsuariogridTableAdapter adpUser = new MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwUsuariogridTableAdapter();
        MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.TBMotivoFaltaTableAdapter adpMOti = new MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.TBMotivoFaltaTableAdapter();

        if (hdItensSolicitacao["TPJust"].ToString() == "2")
        {
            try
            {
                if (cbSetorModo.Text != "")
                    adpUser.FillIDEmpresaSetorUsuario(ds.vwUsuariogrid, Convert.ToInt32(Session["IDEmpresa"]), Convert.ToInt32(cbSetorModo.Value.ToString()), IDUsuario);
                else
                    adpUser.FillIDEmpresaSetorUsuario(ds.vwUsuariogrid, Convert.ToInt32(Session["IDEmpresa"]), Convert.ToInt32(cbSetor.Value.ToString()), IDUsuario);


                if (ds.vwUsuariogrid.Rows.Count > 0)
                {
                    TotalHorasDia = ds.vwUsuariogrid[0].TotHorasDiarias;
                }

                adpMOti.FillIDMotivoFalta(ds.TBMotivoFalta, IDMotivoFalta);

                if (ds.TBMotivoFalta.Rows.Count > 0)
                    AbonoFalta = ds.TBMotivoFalta[0].AbonarHoras;

                if (AbonoFalta)
                {
                    TotaDia = ds.vwUsuariogrid[0].TotHorasDiarias;
                }
                else
                {
                    TotaDia = 0;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            //Caso maior não atualiza o valor da hora total
            //if (Convert.ToDateTime(HorasTotais) > TotaDia)
            //{
            //TotaDia = Convert.ToDateTime(HorasTotais);
            //}
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
        cbSetor0.DataSource = ds;
        cbSetorModo.DataSource = ds;

        cbSetor.IncrementalFilteringMode = DevExpress.Web.ASPxEditors.IncrementalFilteringMode.Contains;
        cbSetor0.IncrementalFilteringMode = DevExpress.Web.ASPxEditors.IncrementalFilteringMode.Contains;
        cbSetorModo.IncrementalFilteringMode = DevExpress.Web.ASPxEditors.IncrementalFilteringMode.Contains;

        cbSetorModo.DataBind();
        cbSetor.DataBind();
        cbSetor0.DataBind();
    }

    protected void PreencheddlUsuario(string IDSetor)
    {
        MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwUsuariogridTableAdapter adpvwUsuariogrid =
            new MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwUsuariogridTableAdapter();
        MetodosPontoFrequencia.DataSetPontoFrequencia ds = new DataSetPontoFrequencia();

        ds.EnforceConstraints = false;

        adpvwUsuariogrid.FillIDEmpresaSetor(ds.vwUsuariogrid, Convert.ToInt32(Session["IDEmpresa"]), Convert.ToInt32(IDSetor));

        //PT.PreenchevwNomeUsuario(ds, Convert.ToInt32(IDSetor));
        cbUsuario.DataSource = ds.vwUsuariogrid;
        cbUsuario.DataBind();
    }

    protected void PreencheddlMes()
    {
        PT.PreencheTBMes(ds);
        cbMes.DataSource = ds;
        cbMes.DataBind();
    }

    protected void PreencheGridSemRegistro(int IDEmpresa, int IDSetor)
    {
        PT.UsuarioSemRegistroDiario(ds, IDEmpresa, IDSetor, System.DateTime.Now.ToShortDateString());
        gridSemRegistro.DataSource = ds;
        gridSemRegistro.DataBind();
    }

    protected void PreencheddlMotivoFalta()
    {
        if ((string)Session["TPUsuario"] != "1")
            PT.PreencheTBMotivoFalta(ds);
        else
            PT.PreencheTBMotivoFaltaCInoperante(ds);

        cbMotivoFaltaManut.DataSource = ds;
        cbMotivoFalta.DataSource = ds;

        cbMotivoFaltaManut.Items.Add("Justificar para o órgão.");

        cbMotivoFaltaManut.IncrementalFilteringMode = DevExpress.Web.ASPxEditors.IncrementalFilteringMode.Contains;
        cbMotivoFalta.IncrementalFilteringMode = DevExpress.Web.ASPxEditors.IncrementalFilteringMode.Contains;
        cbMotivoFalta.DataBind();
        cbMotivoFaltaManut.DataBind();
    }

    protected void PreenchegridFrequenciaSession()
    {
        gridFrequencia.DataSource = (MetodosPontoFrequencia.DataSetPontoFrequencia)Session["Dataset"];
        //gridFrequencia.PageIndex = 0;
        gridFrequencia.DataBind();
    }

    protected void PreenchegridFrequencia(int IDSetor, int IDMesRef, int IDVInculoUsuario)
    {
        int IDUsuario;
        //VERIFICA O ANO
        int Ano;
        if (rbAno.SelectedItem.Text == "Ano Corrente")
            Ano = System.DateTime.Now.Year;
        else
            Ano = System.DateTime.Now.Year - 1;


        //PEGA OS DADOS DO USUARIO, PARA VERIFICAR SE É OU NÃO DE REGIME PLANTONISTA
        //Pega IDUsuario
        DataSetPontoFrequencia dsu = new DataSetPontoFrequencia();
        Freq.DadosUsuario(dsu, IDVInculoUsuario);

        IDUsuario = dsu.vwUsuariogrid[0].IDUsuario;
        Session["IDUsuarioManutFrequencia"] = IDUsuario;
        _usuarioPlantonista = dsu.vwUsuariogrid[0].RegimePlantonista;

        if (!(dsu.vwUsuariogrid[0].RegimePlantonista))
        {
            if (cbOcorrencia.Checked)
            {
                Freq.HorasDiasOcorrencia(IDMesRef, IDUsuario, Ano, ds, Convert.ToInt32(Session["IDEmpresa"]), IDSetor, Convert.ToBoolean(Session["DescontoTotalJornada"]),
                    IDVInculoUsuario);
            }
            else
            {
                Freq.HorasDias(IDMesRef, IDUsuario, Ano, ds, Convert.ToInt32(Session["IDEmpresa"]), IDSetor,
                    IDVInculoUsuario);
            }
        }
        else
        {
            Freq.HorasDiasPlantonista(IDMesRef, IDUsuario, Ano, ds, Convert.ToInt32(Session["IDEmpresa"]), IDSetor,
                cbOcorrencia.Checked);
        }

        #region OLD
        //if (rbAno.SelectedItem.ToString() == "Ano Corrente")
        //{
        //    if (cbOcorrencia.Checked)
        //    {
        //        Freq.HorasDiasOcorrencia(IDMesRef, IDUsuario, DateTime.Now.Year, ds, Convert.ToInt32(Session["IDEmpresa"]), IDSetor, Convert.ToBoolean(Session["DescontoTotalJornada"]));
        //    }
        //    else
        //        Freq.HorasDias(IDMesRef, IDUsuario, DateTime.Now.Year, ds, Convert.ToInt32(Session["IDEmpresa"]), IDSetor);
        //}

        //else
        //{
        //    if (cbOcorrencia.Checked)
        //    {
        //        Freq.HorasDiasOcorrencia(IDMesRef, IDUsuario, DateTime.Now.Year - 1, ds, Convert.ToInt32(Session["IDEmpresa"]), IDSetor, Convert.ToBoolean(Session["DescontoTotalJornada"]));
        //    }
        //    else
        //    {
        //        Freq.HorasDias(IDMesRef, IDUsuario, DateTime.Now.Year - 1, ds, Convert.ToInt32(Session["IDEmpresa"]), IDSetor);
        //    }
        //}
        #endregion OLD

        if (ds.vWHorasDia.Rows.Count > 0)
        {
            Lei = 1;
            Session["Dataset"] = ds;
        }

        gridFrequencia.DataSource = ds;
        //gridFrequencia.PageIndex = 0;
        gridFrequencia.DataBind();

    }
    protected void gridFrequencia_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {

        if (e.Parameters == "1")
        {
            JustificativaColetiva();
        }
        if (e.Parameters == "J")
        {
            ExcluirJustificativa();
        }
        if (e.Parameters == "JU")
        {
            SalvarJustificativa(Convert.ToInt32(hdItensJustificativa["IDUsuario"].ToString()), Convert.ToInt32(hdItensJustificativa["IDFrequencia"].ToString()), hdJustificativa["OBS"].ToString(), Convert.ToInt32(hdJustificativa["IDMotivoFalta"].ToString()));
        }

        if (e.Parameters == "SOJU")
        {
            SalvarSolicitacaoJustificativa(Convert.ToInt32(hdItensSolicitacao["IDUsuario"].ToString()),
                Convert.ToInt32(hdItensSolicitacao["IDFrequencia"].ToString()),
                hdItensSolicitacao["OBS"].ToString(),
                Convert.ToInt32(hdItensSolicitacao["IDMotivoFalta"].ToString()));
        }
        if (e.Parameters == "REJU")
        {
            //SalvarSolicitacaoJustificativaRejeitar(Convert.ToInt32(hdItensSolicitacao["IDUsuario"].ToString()),
            //Convert.ToInt32(hdItensSolicitacao["IDFrequencia"].ToString()),
            //    hdItensSolicitacao["OBS"].ToString(),
            //    Convert.ToInt32(hdItensSolicitacao["IDMotivoFalta"].ToString()));
        }

        //if(cbSetor.Text != "" && cbUsuario.Text != "" && cbMes.Text != "")
        PreenchegridFrequencia(Convert.ToInt32(cbSetor.Value), Convert.ToInt32(cbMes.Value), Convert.ToInt32(cbUsuario.Value));
        //PreenchegridFrequencia(Convert.ToInt32(cbSetor.SelectedItem.Value), Convert.ToInt32(cbUsuario.Value), Convert.ToInt32(cbMes.SelectedItem.Value));
    }
    protected void gridFrequencia_PageIndexChanged(object sender, EventArgs e)
    {
        if (cbSetor.Text != "" && cbUsuario.Text != "" && cbMes.Text != "")
            PreenchegridFrequencia(Convert.ToInt32(cbSetor.SelectedItem.Value),
                Convert.ToInt32(cbMes.SelectedItem.Value), Convert.ToInt32(cbUsuario.Value));
    }
    protected void gridFrequencia_ProcessColumnAutoFilter(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewAutoFilterEventArgs e)
    {
        if (cbSetor.Text != "" && cbUsuario.Text != "" && cbMes.Text != "")
            PreenchegridFrequencia(Convert.ToInt32(cbSetor.SelectedItem.Value),
                Convert.ToInt32(cbMes.SelectedItem.Value), Convert.ToInt32(cbUsuario.Value));
    }
    protected void gridFrequencia_BeforeColumnSortingGrouping(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewBeforeColumnGroupingSortingEventArgs e)
    {
        if (cbSetor.Text != "" && cbUsuario.Text != "" && cbMes.Text != "")
            PreenchegridFrequencia(Convert.ToInt32(cbSetor.SelectedItem.Value),
                Convert.ToInt32(cbMes.SelectedItem.Value), Convert.ToInt32(cbUsuario.Value));
    }
    protected void btDetalhar_Click(object sender, EventArgs e)
    {

    }
    protected void SalvarJustificativa(int IDUsuario, int IDFrequencia, string OBS, int IDMotivoFalta)
    {
        //string HorasDia = gridFrequencia.GetRowValues(gridFrequencia.FocusedRowIndex, "HorasDia").ToString();
        //string TotHorasDiarias = gridFrequencia.GetRowValues(gridFrequencia.FocusedRowIndex, "TotHorasDiarias").ToString()


        //if (Convert.ToDateTime(HorasDia) < Convert.ToDateTime("0"+TotHorasDiarias+":00:00"))
        //{
        try
        {
            if (Session["TPUsuario"].ToString() == "9")
            {
                //this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('Usuário perfil Diretor. Impossível realizar justificativa ao próprio usuário.');</script>");
                msg = "Usuário de perfil Diretor. Não é possível realizar justificativa ao próprio usuário.";
                lbResposta.Text = msg;
                lbResposta.DataBind();
                return;
            }

            TotalHorasAbonoFalta(IDUsuario, IDMotivoFalta, Convert.ToDateTime(hdItensJustificativa["DTJust"].ToString()),
                hdItensJustificativa["TotalHorasDiarias"].ToString(), hdItensJustificativa["TotDIa"].ToString());

            msg = Freq.JustificaFrequenciaDia(IDFrequencia, IDUsuario, IDMotivoFalta, OBS,
                Convert.ToDateTime(hdItensJustificativa["DTJust"].ToString()), TotaDia,
                Convert.ToInt32(hdJustificativa["TPJust"].ToString()) + 1, Convert.ToInt32(Session["IDEmpresa"].ToString()),
                hdJustificativa["INDEX"].ToString(), Convert.ToInt32(Session["IDUsuario"]),
                Convert.ToInt32(hdItensJustificativa["IDVinculoUsuario"]), 0);

            lbResposta.Text = msg;

            if (lbResposta.Text.IndexOf("sucesso") >= 0)
            {
                lbResposta.Text = "";
                lbResposta.DataBind();
            }

            //lbResposta.Text = msg;
            //lbResposta.DataBind();

        }
        catch (Exception ex)
        {
            ex.ToString();
        }

        cbMotivoFaltaManut.SelectedIndex = 0; //Zerar Motivos de Falta
        cbMotivoFaltaManut.DataBind();
        memoOBS.Text = ""; ////Zerar Motivos de Falta
                           //}
                           //else
                           // {
                           //     msg = "Frequência do dia compatível com a carga horária diária. Impossível alterar.";
                           // }

        //PreenchegridFrequencia(Convert.ToInt32(cbSetor.SelectedItem.Value), Convert.ToInt32(cbUsuario.Value), Convert.ToInt32(cbMes.SelectedItem.Value));
    }

    protected void SalvarSolicitacaoJustificativa(int IDUsuario, int IDFrequencia, string OBS, int IDMotivoFalta)
    {
        //string HorasDia = gridFrequencia.GetRowValues(gridFrequencia.FocusedRowIndex, "HorasDia").ToString();
        //string TotHorasDiarias = gridFrequencia.GetRowValues(gridFrequencia.FocusedRowIndex, "TotHorasDiarias").ToString()


        //if (Convert.ToDateTime(HorasDia) < Convert.ToDateTime("0"+TotHorasDiarias+":00:00"))
        //{
        try
        {
            if (Session["TPUsuario"].ToString() == "9")
            {
                //this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('Usuário perfil Diretor. Impossível realizar justificativa ao próprio usuário.');</script>");
                msg = "Usuário de perfil Gestor/Administrador de Órgão . Não é possível realizar justificativa ao próprio usuário.";
                lbResposta.Text = msg;
                lbResposta.DataBind();
                return;
            }


            Convert.ToInt32(Session["IDEmpresa"].ToString());
            Convert.ToInt32(Session["Usuario"]);

            TotalHorasAbonoFaltaSolicitacao(IDUsuario, IDMotivoFalta, Convert.ToDateTime(hdItensSolicitacao["DTJust"].ToString()),
                hdItensSolicitacao["TotalHorasDiarias"].ToString(), hdItensSolicitacao["TotDIa"].ToString());

            msg = Freq.JustificaFrequenciaDia(IDFrequencia, IDUsuario, IDMotivoFalta, OBS,
                Convert.ToDateTime(hdItensSolicitacao["DTJust"].ToString()), TotaDia,
                Convert.ToInt32(hdItensSolicitacao["TPJust"].ToString()), Convert.ToInt32(Session["IDEmpresa"].ToString()),
                "", Convert.ToInt32(Session["Usuario"]),
                Convert.ToInt32(hdItensSolicitacao["IDVinculoUsuario"]), 0);


            if (msg.IndexOf("sucesso") > 0)
            {
                JustPedido = new MetodosPontoFrequencia.Justificativa.Justificativa();
                //JustPedido.ChangeStatusPedidoJustificativa(0, null, DateTime.Now, Convert.ToInt32(Session["Usuario"].ToString()),
                //Convert.ToInt32(hdItensSolicitacao["IDVinculoUsuario"]), IDFrequencia, Convert.ToDateTime(hdItensSolicitacao["DTJust"].ToString()), IDUsuario, string.Format("{0} Gestor: {1}", OBS, memoOBSS0.Text.Trim()));

            }

            lbResposta.Text = msg;

            if (lbResposta.Text.IndexOf("sucesso") >= 0)
            {
                this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('" + msg + "');</script>");
            }

            //lbResposta.Text = msg;
            //lbResposta.DataBind();

        }
        catch (Exception ex)
        {
            ex.ToString();
        }

        cbMotivoFaltaManut.SelectedIndex = 0; //Zerar Motivos de Falta
        cbMotivoFaltaManut.DataBind();
        memoOBS.Text = ""; ////Zerar Motivos de Falta
                           //}
                           //else
                           // {
                           //     msg = "Frequência do dia compatível com a carga horária diária. Impossível alterar.";
                           // }

        PreenchegridFrequencia(Convert.ToInt32(cbSetor.SelectedItem.Value),
            Convert.ToInt32(cbMes.SelectedItem.Value), Convert.ToInt32(cbUsuario.Value));
    }

    protected void SalvarSolicitacaoJustificativaRejeitar(int IDUsuario, int IDFrequencia, string OBS, int IDMotivoFalta)
    {
        //string HorasDia = gridFrequencia.GetRowValues(gridFrequencia.FocusedRowIndex, "HorasDia").ToString();
        //string TotHorasDiarias = gridFrequencia.GetRowValues(gridFrequencia.FocusedRowIndex, "TotHorasDiarias").ToString()


        //if (Convert.ToDateTime(HorasDia) < Convert.ToDateTime("0"+TotHorasDiarias+":00:00"))
        //{
        try
        {
            if (Session["TPUsuario"].ToString() == "9")
            {
                //this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('Usuário perfil Diretor. Impossível realizar justificativa ao próprio usuário.');</script>");
                msg = "Usuário de perfil Gestor/Administrador de Órgão . Não é possível realizar justificativa ao próprio usuário.";
                lbResposta.Text = msg;
                lbResposta.DataBind();
                return;
            }
            TotalHorasAbonoFaltaSolicitacao(IDUsuario, IDMotivoFalta, Convert.ToDateTime(hdItensSolicitacao["DTJust"].ToString()),
                hdItensSolicitacao["TotalHorasDiarias"].ToString(), hdItensSolicitacao["TotDIa"].ToString());

            msg = Freq.JustificaFrequenciaDia(IDFrequencia, IDUsuario, IDMotivoFalta, string.Format("{0} Gestor: {1}", OBS, memoOBSS0.Text.Trim()),
                Convert.ToDateTime(hdItensSolicitacao["DTJust"].ToString()), TotaDia,
                Convert.ToInt32(hdItensSolicitacao["TPJust"].ToString()), Convert.ToInt32(Session["IDEmpresa"].ToString()),
                "", Convert.ToInt32(Session["Usuario"]),
                Convert.ToInt32(hdItensSolicitacao["IDVinculoUsuario"]), 2);


            if (msg.IndexOf("sucesso") > 0)
            {
                JustPedido = new MetodosPontoFrequencia.Justificativa.Justificativa();
                JustPedido.ChangeStatusPedidoJustificativa(2, null, DateTime.Now, Convert.ToInt32(Session["Usuario"].ToString()),
                Convert.ToInt32(hdItensSolicitacao["IDVinculoUsuario"]), IDFrequencia, Convert.ToDateTime(hdItensSolicitacao["DTJust"].ToString()), IDUsuario, string.Format("{0} Gestor: {1}", OBS, memoOBSS0.Text.Trim()));

            }

            lbResposta.Text = msg;

            if (lbResposta.Text.IndexOf("sucesso") >= 0)
            {
                this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('" + msg + "');</script>");
            }

            //lbResposta.Text = msg;
            //lbResposta.DataBind();

        }
        catch (Exception ex)
        {
            ex.ToString();
        }

        cbMotivoFaltaManut.SelectedIndex = 0; //Zerar Motivos de Falta
        cbMotivoFaltaManut.DataBind();
        memoOBS.Text = ""; ////Zerar Motivos de Falta
                           //}
                           //else
                           // {
                           //     msg = "Frequência do dia compatível com a carga horária diária. Impossível alterar.";
                           // }

        PreenchegridFrequencia(Convert.ToInt32(cbSetor.SelectedItem.Value),
            Convert.ToInt32(cbMes.SelectedItem.Value), Convert.ToInt32(cbUsuario.Value));
    }
    protected void btSalvar_Click(object sender, EventArgs e)
    {
        SalvarJustificativa(Convert.ToInt32(coIDUsuario.Value), Convert.ToInt32(coIDFrequencia.Value), memoOBSOLD.Text, Convert.ToInt32(cbMotivoFaltaManut.SelectedItem.Value));
    }
    protected void btFechar_Click(object sender, EventArgs e)
    {
        cbMotivoFaltaManut.SelectedIndex = 0; //Zerar Motivos de Falta
        cbMotivoFaltaManut.DataBind();
        memoOBSOLD.Text = ""; ////Zerar Motivos de Falta
    }
    protected void gridFrequencia_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
    {
        if (Lei == 1)
        {
            e.Row.Font.Size = 9;

            int IDUsuario = 0;
            string IDRegimeHOra = Convert.ToString(e.GetValue("IDRegimeHora"));

            string SituacaoJustificativa = Convert.ToString(e.GetValue("SituacaoJustificativa"));

            string Justificado = Convert.ToString(e.GetValue("IMF"));

            int TotalHoras = Convert.ToInt32(e.GetValue("TotHorasDiarias"));

            int IDvinculousuario = Convert.ToInt32(e.GetValue("IDVinculoUsuario"));

            if (IDvinculousuario > 0)
                IDUsuario = int.Parse(justificativa.GetIdUserByVinculo(IDvinculousuario.ToString()));


            DateTime DTFrequencia = Convert.ToDateTime(e.GetValue("DataFrequencia"));
            SomaTotal = 0;
            //string Obs = e.GetValue("OBS").ToString();

            if (DTFrequencia.ToShortDateString() != "01/01/0001")
            {
                //if (e.GetValue("HorasDia").ToString() != "")
                //Horas = Convert.ToDateTime(e.GetValue("HorasDia"));
                //else
                //Horas = Convert.ToDateTime("00:00:00");
                TotalHoraDia = e.GetValue("HorasDia").ToString();
                Segundos = new TimeSpan(Convert.ToInt32(TotalHoraDia.Substring(0, 2)),
                    Convert.ToInt32(TotalHoraDia.Substring(3, 2)), Convert.ToInt32(TotalHoraDia.Substring(6, 2)));

                FormaDesconto = Convert.ToBoolean(e.GetValue("DescontoTotalJornada"));

                if (e.GetValue("IsencaoPonto").ToString() != string.Empty)
                    IsencaoPonto = Convert.ToBoolean(e.GetValue("IsencaoPonto"));
                else
                    IsencaoPonto = false;


                Session["DescontoTotalJornada"] = FormaDesconto;
            }

            //09/12/2018
            // - Printar de vermelho para os atrasos tb. Mais que 15 minutos e o não 
            //cumprimento para o horário de almoço para que tem jornada diária de 8 horas.

            if (!FormaDesconto)
            {
                if (DTFrequencia.ToShortDateString() != "01/01/0001")
                {
                    //isenção ponto
                    if (IsencaoPonto)
                    {
                        var dataIsenta = justificativa.DataIsenta(DTFrequencia.Date, IDUsuario, IDvinculousuario);
                        if (dataIsenta)
                        {
                            e.Row.Cells[0].Text = "";
                            if (e.Row.Cells.Count == 10)
                            {
                                e.Row.Cells[1].Text = "";
                                e.Row.Cells[2].Text = "";
                            }
                            return;
                        }
                    }

                    if (DTFrequencia.Date < DateTime.Now.Date)
                    {
                        if (IDRegimeHOra == "1" && (e.GetValue("EntradaManha").ToString() == "00:00:00" ||
                                e.GetValue("SaidaManha").ToString() == "00:00:00" || e.GetValue("EntradaTarde").ToString() == "00:00:00" ||
                            e.GetValue("SaidaTarde").ToString() == "00:00:00") && (Justificado == "0")
                        && (DTFrequencia.DayOfWeek.ToString() != "Saturday" && DTFrequencia.DayOfWeek.ToString() != "Sunday" || _usuarioPlantonista)
                        && SituacaoJustificativa == "")
                        {
                            e.Row.BackColor = System.Drawing.Color.IndianRed;
                            if (e.Row.Cells.Count == 10)
                            {
                                e.Row.Cells[1].Text = "";
                                e.Row.Cells[2].Text = "";
                            }
                            return;
                        }

                        if (!_usuarioPlantonista && (e.GetValue("EntradaManha").ToString() == "00:00:00" &&
                            e.GetValue("SaidaManha").ToString() == "00:00:00" && e.GetValue("EntradaTarde").ToString() == "00:00:00" &&
                        e.GetValue("SaidaTarde").ToString() == "00:00:00") && (Justificado == "0")
                    && (DTFrequencia.DayOfWeek.ToString() == "Saturday" || DTFrequencia.DayOfWeek.ToString() == "Sunday"))
                        {
                            e.Row.BackColor = System.Drawing.Color.Gainsboro;
                            if (e.Row.Cells.Count == 10)
                            {
                                e.Row.Cells[0].Text = "";
                                e.Row.Cells[1].Text = "";
                                e.Row.Cells[2].Text = "";
                            }
                            return;
                        }


                        if (IDRegimeHOra == "1" && (e.GetValue("EntradaManha").ToString() == "00:00:00" ||
                                 e.GetValue("EntradaTarde").ToString() == "00:00:00") && (Justificado == "0")

                        && SituacaoJustificativa == "")
                        {
                            e.Row.BackColor = System.Drawing.Color.IndianRed;
                            if (e.Row.Cells.Count == 10)
                            {
                                e.Row.Cells[1].Text = "";
                                e.Row.Cells[2].Text = "";
                            }
                            return;
                        }

                        //Atrasos maiores que 15 minutos
                        EntradaManha = TimeSpan.Parse(e.GetValue("EntradaManha").ToString());
                        SaidaManha = TimeSpan.Parse(e.GetValue("SaidaManha").ToString());
                        EntradaTarde = TimeSpan.Parse(e.GetValue("EntradaTarde").ToString());
                        SaidaTarde = TimeSpan.Parse(e.GetValue("SaidaTarde").ToString());

                        HEntradaManha = TimeSpan.Parse(e.GetValue("HoraEntradaManha").ToString() + ":00");
                        HSaidaManha = TimeSpan.Parse(e.GetValue("HoraSaidaManha").ToString());
                        HEntradaTarde = TimeSpan.Parse(e.GetValue("HoraEntradaTarde").ToString());
                        HSaidaTarde = TimeSpan.Parse(e.GetValue("HoraSaidaTarde").ToString());


                        if (IDRegimeHOra == "1")
                        {

                            //Nas entradas pegar só a maior.
                            if (EntradaManha >= HEntradaManha)
                            {
                                SomaTotal = (EntradaManha - HEntradaManha).TotalMinutes;
                            }
                            //Saídas tratar as antecipadas.
                            if (SaidaManha < HSaidaManha)
                            {
                                SomaTotal = SomaTotal + (HSaidaManha - SaidaManha).TotalMinutes;
                            }
                            //Entrada só atrasada
                            if (EntradaTarde >= HEntradaTarde)
                            {
                                SomaTotal = SomaTotal + (EntradaTarde - HEntradaTarde).TotalMinutes;
                            }
                            //saída só na antecipação.
                            if (SaidaTarde < HSaidaTarde)
                            {
                                SomaTotal = SomaTotal + (HSaidaTarde - SaidaTarde).TotalMinutes;
                            }
                        }
                        else
                        {
                            //Prevendo entrada e saida 1
                            if (e.GetValue("EntradaManha").ToString() != "00:00:00")
                            {
                                if (EntradaManha >= HEntradaManha)
                                {
                                    SomaTotal = (EntradaManha - HEntradaManha).TotalMinutes;
                                }
                                //Saídas tratar as antecipadas.
                                if (SaidaManha < HSaidaManha)
                                {
                                    SomaTotal = SomaTotal + (HSaidaManha - SaidaManha).TotalMinutes;
                                }
                                try
                                {
                                    if (SomaTotal < 15 && ((int.Parse(e.GetValue("TotHorasDiarias").ToString()) * 60) - 15) > TimeSpan.Parse(TotalHoraDia).TotalMinutes)
                                    {
                                        if (EntradaManha > SaidaManha)
                                        {
                                            SaidaManha = SaidaManha + TimeSpan.Parse("24:00:00");
                                        }
                                        SaidaManha = (SaidaManha.TotalMinutes == 0) ? EntradaManha : SaidaManha;
                                        double minu = (int.Parse(e.GetValue("TotHorasDiarias").ToString()) * 60) - (SaidaManha - EntradaManha).TotalMinutes;
                                        if (minu > 15 && _usuarioPlantonista)
                                        {
                                            SomaTotal = minu;
                                        }
                                    }

                                }
                                catch { }
                            }
                            //PRevendo entrada 2 e saída 2
                            if (e.GetValue("EntradaTarde").ToString() != "00:00:00")
                            {
                                //Entrada só atrasada
                                if (EntradaTarde >= HEntradaTarde)
                                {
                                    SomaTotal = SomaTotal + (EntradaTarde - HEntradaTarde).TotalMinutes;
                                }
                                //saída só na antecipação.
                                if (SaidaTarde < HSaidaTarde)
                                {
                                    SomaTotal = SomaTotal + (HSaidaTarde - SaidaTarde).TotalMinutes;
                                }
                                try
                                {
                                    if (SomaTotal < 15 && ((int.Parse(e.GetValue("TotHorasDiarias").ToString()) * 60) - 15) > TimeSpan.Parse(TotalHoraDia).TotalMinutes)
                                    {
                                        SaidaTarde = (SaidaTarde.TotalMinutes == 0) ? EntradaTarde : SaidaTarde;
                                        if (EntradaTarde > SaidaTarde)
                                        {
                                            SaidaTarde = SaidaTarde + TimeSpan.Parse("24:00:00");
                                        }
                                        double minu = (int.Parse(e.GetValue("TotHorasDiarias").ToString()) * 60) - (SaidaTarde - EntradaTarde).TotalMinutes;
                                        if (minu > 15 && _usuarioPlantonista)
                                        {
                                            SomaTotal = minu;
                                        }
                                    }

                                }
                                catch { }
                            }

                            if (e.GetValue("EntradaManha").ToString() == "00:00:00" &&
                                (e.GetValue("EntradaTarde").ToString() == "00:00:00")
                                && (Justificado == "" || Justificado == "0"))
                            {
                                SomaTotal = 30;
                            }
                        }

                        //SomaTotal = SomaTotal - 15;

                        if (SomaTotal > 15 && (Justificado == "" || Justificado == "0") && (DTFrequencia.DayOfWeek.ToString() != "Saturday" && DTFrequencia.DayOfWeek.ToString() != "Sunday" || _usuarioPlantonista) && (SituacaoJustificativa == "" || SituacaoJustificativa == "0"))
                        {
                            e.Row.BackColor = System.Drawing.Color.IndianRed;
                            if (e.Row.Cells.Count == 10)
                            {
                                e.Row.Cells[1].Text = "";
                                e.Row.Cells[2].Text = "";
                            }
                            return;
                        }
                        //--------------
                    }
                }
            }
            else if (ObrigaQuatroBatidas.Equals("S") && DataDesconsidera != string.Empty)// alterado atender help redmine #111
            {
                if (DTFrequencia.ToShortDateString() != "01/01/0001")
                {
                    if (DTFrequencia.Date < DateTime.Now.Date && DTFrequencia.Date > Convert.ToDateTime(DataDesconsidera))
                    {
                        if (IDRegimeHOra == "1" && (e.GetValue("EntradaManha").ToString() == "00:00:00" ||
                                            e.GetValue("SaidaManha").ToString() == "00:00:00" || e.GetValue("EntradaTarde").ToString() == "00:00:00" ||
                                            e.GetValue("SaidaTarde").ToString() == "00:00:00") && (DTFrequencia.DayOfWeek.ToString() != "Saturday" && DTFrequencia.DayOfWeek.ToString() != "Sunday") && (Justificado == "0") && SituacaoJustificativa == "")
                        {
                            e.Row.BackColor = System.Drawing.Color.IndianRed;
                            if (e.Row.Cells.Count == 10)
                            {
                                e.Row.Cells[1].Text = "";
                                e.Row.Cells[2].Text = "";
                            }
                            return;
                        }
                    }
                }
            }

            //isenção ponto
            if (IsencaoPonto)
            {
                var dataIsenta = justificativa.DataIsenta(DTFrequencia.Date, IDUsuario, IDvinculousuario);
                if (dataIsenta)
                {
                    e.Row.Cells[0].Text = "";
                    if (e.Row.Cells.Count == 10)
                    {
                        e.Row.Cells[1].Text = "";
                        e.Row.Cells[2].Text = "";
                    }
                    return;
                }
            }
            //Caso Frequência igual igual ou maior a carga horária pintar de branco.
            //Caso na tolerância - pentar de laranja
            //Caso abaixo da tolerância pintar de vermelho.

            if (Segundos.TotalSeconds != 0 && Justificado == "0")
            {
                TotalHoras = TotalHoras * 3600;

                if (TotalHoras == 4 && IDRegimeHOra == "7")
                    TotalHoras += 1800;

                if (Segundos.TotalSeconds >= TotalHoras && SituacaoJustificativa == "")
                {

                    e.Row.BackColor = System.Drawing.Color.WhiteSmoke;
                    e.Row.Enabled = false;
                    e.Row.Visible = false;
                    if (e.Row.Cells.Count == 10)
                    {
                        e.Row.Cells[0].Text = "";
                        e.Row.Cells[1].Text = "";
                        e.Row.Cells[2].Text = "";
                    }
                }
                else if (SituacaoJustificativa == "1")
                {
                    e.Row.BackColor = System.Drawing.Color.Yellow;
                    if (e.Row.Cells.Count == 10)
                    {
                        e.Row.Cells[0].Text = "";
                        e.Row.Cells[2].Text = "";
                    }
                }
                else if (SituacaoJustificativa == "2")
                {
                    e.Row.BackColor = System.Drawing.Color.DarkSlateGray;
                    e.Row.ForeColor = System.Drawing.Color.White;
                    if (e.Row.Cells.Count == 10)
                    {
                        e.Row.Cells[0].Text = "";
                        //e.Row.Cells[2].Text = "";
                    }
                }
                else if ((Segundos.TotalSeconds < TotalHoras) && (Segundos.TotalSeconds >= ((TotalHoras) - (15 * 60))))
                {
                    e.Row.BackColor = System.Drawing.Color.Wheat;
                    if (e.Row.Cells.Count == 10)
                    {
                        e.Row.Cells[0].Text = "";
                        e.Row.Cells[1].Text = "";
                        e.Row.Cells[2].Text = "";
                    }
                }
                else if (IDRegimeHOra != "300" && IDRegimeHOra != "4000" && Segundos.TotalSeconds < TotalHoras)
                {
                    e.Row.BackColor = System.Drawing.Color.IndianRed;
                    e.Row.ForeColor = System.Drawing.Color.White;
                    if (e.Row.Cells.Count == 10)
                    {
                        e.Row.Cells[1].Text = "";
                        e.Row.Cells[2].Text = "";
                    }
                }

                return;
            }
            else if (SituacaoJustificativa == "1")
            {
                e.Row.BackColor = System.Drawing.Color.Yellow;
                if (e.Row.Cells.Count == 10)
                {
                    e.Row.Cells[0].Text = "";
                    e.Row.Cells[2].Text = "";
                }
            }
            else if (SituacaoJustificativa == "2")
            {
                e.Row.BackColor = System.Drawing.Color.DarkSlateGray;
                e.Row.ForeColor = System.Drawing.Color.White;
                if (e.Row.Cells.Count == 10)
                {
                    e.Row.Cells[0].Text = "";
                    //e.Row.Cells[2].Text = "";
                }
            }
            else if ((DTFrequencia.DayOfWeek.ToString() == "Saturday" || DTFrequencia.DayOfWeek.ToString() == "Sunday" && !_usuarioPlantonista) && Justificado == "0" && (SituacaoJustificativa == "" || SituacaoJustificativa == "0"))
            {
                if (!_usuarioPlantonista)
                {

                    //TRATAR REGISTRO SE HOUVER MARCAÇAO
                    if ((e.GetValue("EntradaManha").ToString() != "00:00:00" ||
                     e.GetValue("EntradaTarde").ToString() != "00:00:00") && (Justificado == "0")

            && SituacaoJustificativa == "" && (Segundos.TotalSeconds < TotalHoras))
                    {
                        e.Row.BackColor = System.Drawing.Color.IndianRed;
                        if (e.Row.Cells.Count == 10)
                        {
                            e.Row.Cells[1].Text = "";
                            e.Row.Cells[2].Text = "";
                        }
                    }
                    else
                    {
                        e.Row.BackColor = System.Drawing.Color.Gainsboro;
                        if (e.Row.Cells.Count == 10)
                        {
                            e.Row.Cells[0].Text = "";
                            e.Row.Cells[1].Text = "";
                            e.Row.Cells[2].Text = "";
                        }
                    }



                }
                else
                {
                    e.Row.BackColor = System.Drawing.Color.IndianRed;
                    e.Row.ForeColor = System.Drawing.Color.White;
                    if (e.Row.Cells.Count == 10)
                    {
                        e.Row.Cells[1].Text = "";
                        e.Row.Cells[2].Text = "";
                    }
                }

                return;

            }
            else if (((Justificado == "0" || Justificado == "10") && SituacaoJustificativa != "-1") && DTFrequencia.Date < DateTime.Now.Date && (IDRegimeHOra != "4"))
            {
                e.Row.BackColor = System.Drawing.Color.IndianRed;
                e.Row.ForeColor = System.Drawing.Color.White;
                if (e.Row.Cells.Count == 10)
                {
                    e.Row.Cells[1].Text = "";
                    e.Row.Cells[2].Text = "";
                }
                return;
            }

            else if (SituacaoJustificativa == "1")
            {
                e.Row.BackColor = System.Drawing.Color.Yellow;
                if (e.Row.Cells.Count == 10)
                {
                    e.Row.Cells[0].Text = "";
                    e.Row.Cells[2].Text = "";
                }
            }
            else if (SituacaoJustificativa == "2")
            {
                e.Row.BackColor = System.Drawing.Color.DarkSlateGray;
                if (e.Row.Cells.Count == 10)
                {
                    e.Row.Cells[0].Text = "";
                    //e.Row.Cells[2].Text = "";
                }
            }
            else if ((Justificado != "0") && (Justificado != "10") && (Justificado != ""))
            {
                e.Row.BackColor = System.Drawing.Color.CornflowerBlue;
                e.Row.ForeColor = System.Drawing.Color.White;
                if (e.Row.Cells.Count == 10)
                {
                    e.Row.Cells[0].Text = "";
                    //if (SituacaoJustificativa != "")
                    //{
                    //    e.Row.Cells[2].Text = "";
                    //}
                }
                return;
            }

            else
            {
                e.Row.Visible = false;
                if (e.Row.Cells.Count == 10)
                {
                    e.Row.Cells[0].Text = "";
                    e.Row.Cells[1].Text = "";
                    e.Row.Cells[2].Text = "";
                }
            }
        }
    }
    protected void btPesquisar_Click(object sender, EventArgs e)
    {
    }
    protected void gridFrequencia_DetailRowExpandedChanged(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewDetailRowEventArgs e)
    {
        PreenchegridFrequencia(Convert.ToInt32(cbSetor.SelectedItem.Value),
            Convert.ToInt32(cbMes.SelectedItem.Value), Convert.ToInt32(cbUsuario.Value));
    }
    protected void gridFrequencia_CustomColumnGroup(object sender, DevExpress.Web.ASPxGridView.CustomColumnSortEventArgs e)
    {
        PreenchegridFrequencia(Convert.ToInt32(cbSetor.SelectedItem.Value),
            Convert.ToInt32(cbMes.SelectedItem.Value), Convert.ToInt32(cbUsuario.Value));
    }
    protected void gridFrequencia_DetailsChanged(object sender, EventArgs e)
    {
        PreenchegridFrequencia(Convert.ToInt32(cbSetor.SelectedItem.Value),
            Convert.ToInt32(cbMes.SelectedItem.Value), Convert.ToInt32(cbUsuario.Value));
    }
    protected void gridFrequencia_CustomColumnSort(object sender, DevExpress.Web.ASPxGridView.CustomColumnSortEventArgs e)
    {
        PreenchegridFrequencia(Convert.ToInt32(cbSetor.SelectedItem.Value),
            Convert.ToInt32(cbMes.SelectedItem.Value), Convert.ToInt32(cbUsuario.Value));
    }
    protected void btOK_Click(object sender, EventArgs e)
    {
        SalvarExclusao();
    }
    protected void SalvarExclusao()
    {
        string msg = Freq.ExcluirFalta(Convert.ToInt32(coIDFrequencia.Value), Convert.ToInt32(coIDUsuario.Value));

        PreenchegridFrequencia(Convert.ToInt32(cbSetor.SelectedItem.Value),
            Convert.ToInt32(cbMes.SelectedItem.Value), Convert.ToInt32(cbUsuario.Value));

        this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('" + msg + "');</script>");
    }
    protected void gridSemRegistro_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        PreencheGridSemRegistro(Convert.ToInt32(Session["IDEmpresa"]), Convert.ToInt32(cbSetor.SelectedItem.Value));
    }
    protected void gridSemRegistro_PageIndexChanged(object sender, EventArgs e)
    {
        PreencheGridSemRegistro(Convert.ToInt32(Session["IDEmpresa"]), Convert.ToInt32(cbSetor.SelectedItem.Value));
    }

    protected void ExcluirJustificativa()
    {
        msg = Freq.ExcluirJustificativa(ds, Convert.ToInt32(coIDFrequencia.Value), Convert.ToInt32(coIDUsuario.Value),
            Convert.ToInt32(Session["IDEmpresa"]), Convert.ToDateTime(coDTFrequencia.Value),
            Convert.ToInt32(Session["IDUsuario"]), Convert.ToInt32(coIDVinculoUsuario.Value));

        PreenchegridFrequencia(Convert.ToInt32(cbSetor.SelectedItem.Value),
            Convert.ToInt32(cbMes.SelectedItem.Value), Convert.ToInt32(cbUsuario.Value));
        //this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('" + msg + "');</script>");
    }

    protected void JustificativaColetiva()
    {
        DataSetPontoFrequencia dsw = new DataSetPontoFrequencia();

        PT.PreenchevwHorasDia(dsw, FormatarData(Convert.ToDateTime(hdItensJustificativa["DataColetiva"].ToString()).ToShortDateString()), Convert.ToInt32(hdItensJustificativa["SetorColetiva"].ToString()), Convert.ToInt32(Session["IDEmpresa"]));

        if (dsw.vWHorasDia.Rows.Count > 0)
        {
            try
            {
                //Prever aqui situação de alguma falha, parar as inserções caso ocorra alguma exceção
                for (int i = 0; i <= (dsw.vWHorasDia.Rows.Count - 1); i++)
                {
                    TotalHorasAbonoFalta((int)dsw.vWHorasDia[i].IDusuario, Convert.ToInt32(hdItensJustificativa["SetorColetiva"].ToString()), Convert.ToDateTime(hdItensJustificativa["DataColetiva"].ToString()), string.Format("{0} 0{1}:00:00", Convert.ToDateTime(hdItensJustificativa["DataColetiva"].ToString()).ToShortDateString(), dsw.vWHorasDia[i].TotHorasDiarias.ToString()), dsw.vWHorasDia[i].HorasDia);

                    msg = Freq.JustificaFrequenciaDia(dsw.vWHorasDia[i].IDFrequencia, (int)dsw.vWHorasDia[i].IDusuario,
                        Convert.ToInt32(hdJustificativa["IDMotivoFalta"].ToString()), hdJustificativa["OBS"].ToString(),
                        dsw.vWHorasDia[i].DTFrequencia, TotaDia, Convert.ToInt32(hdJustificativa["TPJust"].ToString()),
                        dsw.vWHorasDia[i].IDEmpresa, hdJustificativa["INDEX"].ToString(),
                        Convert.ToInt32(Session["IDUsuario"]), (int)dsw.vWHorasDia[i].IDVinculoUsuario, 0);
                }
                this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('" + msg + "');</script>");

            }
            catch (Exception ex)
            {
                ex.ToString();
                this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('Houve falha em tentar lançar a justificativa coletiva. Tente novamente.');</script>");
            }
        }
        else
        {
            this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('Não há registro(s) de frequência para a data selecionada. Lance uma falta/exceção.');</script>");
        }

    }
    protected void cbUsuario_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        PreencheddlUsuario(cbSetor.Value.ToString());
    }

    private void MontaRadioList(string FASE)
    {
        if (FASE == "0")
        {
            if (hdJustificativa.Count > 0)
                rbList.Value = hdJustificativa["TPJust"].ToString();

            rbList.Items.Clear();
            rbList.Items.Add("Justificativa de registro único", 0);
            rbList.Items.Add("Justificativa de meio período", 1);
            rbList.Items.Add("Justificativa de período integral", 2);
            //labels
            lbMotivo.Visible = false;
            lbConfirmacao.Visible = false;
            lbOBS.Visible = false;
            cbMotivoFaltaManut.Visible = false;
            memoOBS.Visible = false;
            btRetornar.Visible = false;
            cbMotivoFaltaManut.Value = "";

            //Modo coletivo
            cbSetorModo.Visible = false;
            deDataFaltaModo.Visible = false;
            lbSetorModo.Visible = false;
            lbDataModo.Visible = false;
            lbResposta.Visible = false;
        }

        if (FASE == "1")
        {
            if (hdJustificativa.Count > 1)
            {
                if (hdJustificativa["INDEX"].ToString() != "" && rbList.Value == null)
                    rbList.Value = hdJustificativa["INDEX"].ToString();
            }

            switch (hdJustificativa["TPJust"].ToString())
            {
                case "0":
                    rbList.Items.Clear();
                    rbList.Items.Add("Entrada Manhã", 0);
                    rbList.Items.Add("Saída Manhã", 1);
                    rbList.Items.Add("Entrada Tarde", 2);
                    rbList.Items.Add("Saída Tarde", 3);
                    rbList.DataBind();
                    //labels
                    lbMotivo.Visible = false;
                    lbConfirmacao.Visible = false;
                    lbOBS.Visible = false;
                    cbMotivoFaltaManut.Visible = false;
                    memoOBS.Visible = false;
                    btRetornar.Visible = true;
                    lbResposta.Visible = false;

                    //Modo coletivo
                    cbSetorModo.Visible = false;
                    deDataFaltaModo.Visible = false;

                    lbSetorModo.Visible = false;
                    lbDataModo.Visible = false;

                    break;
                case "1":
                    rbList.Items.Clear();
                    rbList.Items.Add("Matutino", 0);
                    rbList.Items.Add("Vespertino", 1);
                    rbList.DataBind();

                    //labels
                    lbMotivo.Visible = false;
                    lbConfirmacao.Visible = false;
                    lbOBS.Visible = false;
                    cbMotivoFaltaManut.Visible = false;
                    memoOBS.Visible = false;
                    btRetornar.Visible = true;

                    //Modo coletivo
                    cbSetorModo.Visible = false;
                    deDataFaltaModo.Visible = false;
                    lbSetorModo.Visible = false;
                    lbDataModo.Visible = false;

                    lbResposta.Visible = false;
                    break;
                case "2":
                    rbList.Items.Clear();
                    rbList.Visible = false;
                    rbList.DataBind();
                    //labels
                    lbMotivo.Visible = true;
                    lbConfirmacao.Visible = false;
                    lbOBS.Visible = true;
                    cbMotivoFaltaManut.Visible = true;
                    btSalvarAvanc.Text = "Avançar";
                    btRetornar.Text = "Retornar";
                    memoOBS.Visible = true;

                    lbResposta.Visible = false;

                    if (hdItensJustificativa["Modo"].ToString() == "Coletivo")
                    {
                        //Modo coletivo
                        cbSetorModo.Visible = true;
                        deDataFaltaModo.Visible = true;

                        lbSetorModo.Visible = true;
                        lbDataModo.Visible = true;
                    }
                    else
                    {
                        //Modo coletivo
                        cbSetorModo.Visible = false;
                        deDataFaltaModo.Visible = false;

                        lbSetorModo.Visible = false;
                        lbDataModo.Visible = false;
                    }
                    break;
            }

        }
        else if (FASE == "2")
        {
            switch (hdJustificativa["TPJust"].ToString())
            {
                case "0":
                    rbList.Visible = false;
                    lbConfirmacao.Visible = false;
                    lbMotivo.Visible = true;
                    lbOBS.Visible = true;
                    cbMotivoFaltaManut.Visible = true;
                    btSalvarAvanc.Text = "Avançar";
                    btRetornar.Text = "Retornar";
                    memoOBS.Visible = true;
                    lbResposta.Visible = false;

                    if (hdItensJustificativa["Modo"].ToString() == "Coletivo")
                    {
                        //Modo coletivo
                        cbSetorModo.Visible = true;
                        deDataFaltaModo.Visible = true;

                        lbSetorModo.Visible = true;
                        lbDataModo.Visible = true;

                    }
                    else
                    {
                        //Modo coletivo
                        cbSetorModo.Visible = false;
                        deDataFaltaModo.Visible = false;

                        lbSetorModo.Visible = false;
                        lbDataModo.Visible = false;
                    }

                    break;
                case "1":
                    rbList.Visible = false;
                    lbConfirmacao.Visible = false;
                    lbMotivo.Visible = true;
                    lbOBS.Visible = true;
                    cbMotivoFaltaManut.Visible = true;
                    btSalvarAvanc.Text = "Avançar";
                    btRetornar.Text = "Retornar";
                    memoOBS.Visible = true;

                    lbResposta.Visible = false;

                    if (hdItensJustificativa["Modo"].ToString() == "Coletivo")
                    {
                        //Modo coletivo
                        cbSetorModo.Visible = true;
                        deDataFaltaModo.Visible = true;

                        lbSetorModo.Visible = true;
                        lbDataModo.Visible = true;


                    }
                    else
                    {
                        //Modo coletivo
                        cbSetorModo.Visible = false;
                        deDataFaltaModo.Visible = false;

                        lbSetorModo.Visible = false;
                        lbDataModo.Visible = false;

                    }

                    break;
                case "2":
                    lbAlerta.Visible = false;
                    lbAlerta.Text = "";
                    bool motivoExiste = true;
                    var motivoId = cbMotivoFaltaManut.Value.ToString();
                    
                    if (!string.IsNullOrEmpty(motivoId))
                        motivoExiste = justificativa.ValidaMotivoExiste(motivoId);

                    //acrescentado em 11/10/2019
                    //se houver menos de 4 caracteres na combo não deixa avançar
                    if(motivoExiste)
                    {
                        if (cbMotivoFaltaManut.Text.Length <= 4)
                            motivoExiste = false;
                    }

                    if (motivoExiste)
                    {
                        rbList.Visible = false;
                        lbConfirmacao.Visible = true;
                        lbMotivo.Visible = false;
                        lbOBS.Visible = false;
                        cbMotivoFaltaManut.Visible = false;
                        memoOBS.Visible = false;
                        MontaTextoConfirmacao(cbMotivoFaltaManut.Text);
                        btSalvarAvanc.Text = "Salvar";
                        btRetornar.Text = "Cancelar";

                        lbResposta.Visible = false;

                        //Modo coletivo
                        cbSetorModo.Visible = false;
                        deDataFaltaModo.Visible = false;
                        lbSetorModo.Visible = false;
                        lbDataModo.Visible = false;
                    }
                    else
                    {
                        lbAlerta.Visible = true;
                        lbAlerta.Text = "O motivo selecionado não existe, favor selecionar um válido.";
                        MontaRadioList("1");
                        break;
                    }
                    break;
            }
        }
        else if (FASE == "3")
        {
            lbAlerta.Visible = false;
            lbAlerta.Text = "";
            if (hdJustificativa["TPJust"].ToString() == "0" || hdJustificativa["TPJust"].ToString() == "1")
            {
                bool motivoExiste = true;
                var motivoId = cbMotivoFaltaManut.Value.ToString();
                if (!string.IsNullOrEmpty(motivoId))
                    motivoExiste = justificativa.ValidaMotivoExiste(motivoId);

                if (motivoExiste)
                {

                    rbList.Visible = false;
                    lbConfirmacao.Visible = true;
                    lbMotivo.Visible = false;
                    lbOBS.Visible = false;
                    cbMotivoFaltaManut.Visible = false;
                    memoOBS.Visible = false;

                    lbResposta.Visible = false;

                    //Modo coletivo
                    cbSetorModo.Visible = false;
                    deDataFaltaModo.Visible = false;

                    lbSetorModo.Visible = false;
                    lbDataModo.Visible = false;

                    MontaTextoConfirmacao(cbMotivoFaltaManut.Text);
                    btSalvarAvanc.Text = "Salvar";
                    btRetornar.Text = "Cancelar";
                }
                else
                {
                    lbAlerta.Visible = true;
                    lbAlerta.Text = "O motivo selecionado não existe, favor selecionar um válido";
                    MontaRadioList("2");
                }
            }
        }
        else if (FASE == "JU" || FASE == "JC")
        {
            rbList.Visible = false;
            lbConfirmacao.Visible = true;
            lbMotivo.Visible = false;
            lbOBS.Visible = false;
            cbMotivoFaltaManut.Visible = false;
            memoOBS.Visible = false;

            //Modo coletivo
            cbSetorModo.Visible = false;
            deDataFaltaModo.Visible = false;

            lbSetorModo.Visible = false;
            lbDataModo.Visible = false;

            //MontaTextoConfirmacao(cbMotivoFaltaManut.Text);
            btSalvarAvanc.Text = "Salvar";
            btRetornar.Text = "Cancelar";

            switch (FASE)
            {
                case "JU":
                    SalvarJustificativa(Convert.ToInt32(hdItensJustificativa["IDUsuario"].ToString()), Convert.ToInt32(hdItensJustificativa["IDFrequencia"].ToString()), hdJustificativa["OBS"].ToString(), Convert.ToInt32(hdJustificativa["IDMotivoFalta"].ToString()));
                    break;
                case "JC":
                    JustificativaColetiva();
                    lbResposta.Text = ""; //Paleativo, até se desenvolver outra rotina para listar os que não deram certo na justificativa.
                    break;
            }

            if (lbResposta.Text == "")
            {
                MontaTextoConfirmacao(cbMotivoFaltaManut.Text);
                lbConfirmacao.Visible = true;
                lbConfirmacao.Text = "Operação realizada com sucesso.";
                lbConfirmacao.Font.Size = System.Web.UI.WebControls.FontUnit.Medium;
                lbConfirmacao.ForeColor = System.Drawing.Color.Green;
                lbConfirmacao.Font.Bold = true;

                lbResposta.Visible = false;
                btSalvarAvanc.Text = "Finalizar";
                btRetornar.Visible = false;
            }
            else
            {
                lbConfirmacao.Visible = false;
                btSalvarAvanc.Visible = false;
                lbResposta.ForeColor = System.Drawing.Color.Red;
                lbResposta.Font.Bold = true;
                lbResposta.Font.Size = System.Web.UI.WebControls.FontUnit.Medium;
                lbResposta.Visible = true;
                btRetornar.Text = "Cancelar";
            }
        }
        else if (FASE == "SOJU")
        {
            SalvarSolicitacaoJustificativa(Convert.ToInt32(hdItensSolicitacao["IDUsuario"].ToString()), Convert.ToInt32(hdItensSolicitacao["IDFrequencia"].ToString()), hdItensSolicitacao["OBS"].ToString(), Convert.ToInt32(hdItensSolicitacao["IDMotivoFalta"].ToString()));
        }
    }

    private void MontaTextoConfirmacao(string MotivoFalta)
    {
        lbConfirmacao.Text = string.Format("Confirmar a justificativa: {0} .", MotivoFalta);
    }

    protected void cpJust_Callback1(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        lbAlerta.Visible = false;
        lbAlerta.Text = "";
        MontaRadioList(e.Parameter);
    }
    protected void btOK0_Click(object sender, EventArgs e)
    {
        //ExcluirJustificativa();
    }
    protected void cbMes_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        coIDUsuarioSetorManut["IDusuarioFolha"] = crp.CriptograFa(cbUsuario.Value.ToString());
        coIDUsuarioSetorManut["IDsetorFofolha"] = crp.CriptograFa(cbSetor.Value.ToString());
    }
    protected void btGerarFolha_Click(object sender, EventArgs e)
    {
        //string meuscript = @"function MudaPagina()"
        //Page.ClientScript.RegisterClientScriptBlock(this.GetType(),"MyScript")
        this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> if(ASPxClientEdit.ValidateGroup('ValidaGrupo'))MudaPagina();</script>");
    }
    protected void cbUsuario_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

}