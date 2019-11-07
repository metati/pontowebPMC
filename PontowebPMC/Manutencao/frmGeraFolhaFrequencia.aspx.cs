using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetodosPontoFrequencia;
using MetodosPontoFrequencia.Justificativa;

public partial class Manutencao_frmGeraFolhaFrequencia : System.Web.UI.Page
{
    DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
    public PreencheTabela PT = new PreencheTabela();
    public Cadastro CAD = new Cadastro();
    public Frequencia Freq = new Frequencia();
    Justificativa justificativa = new Justificativa();
    private Crip crip = new Crip();
    public int IDSETOR;

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
                PreencheddlSetor();

                coIDUsuarioSetorFFrequencia.Add("IDSetor", "");
                coIDUsuarioSetorFFrequencia.Add("IDUsuario", "");

                //int teste = System.DateTime.DaysInMonth(System.DateTime.Now.Year, 7);
                //string tt = System.DateTime.Now.DayOfWeek.ToString();

                //DateTime ll = System.DateTime.Now;

                //DateTime dt = new DateTime(2011, 5, 27);

                //string diasemana = dt.DayOfWeek.ToString();

                PreencheddlMes();
            }
        }

        IDSETOR = Convert.ToInt32(Session["IDSETOR"]);
    }

    protected void PreencheddlSetor()
    {

        if (Convert.ToInt32(Session["TPUsuario"]) == 3)
        {
            //PT.PreencheTBSetor(ds);
            //PT.PreencheTBSetorIDSetor(ds, Convert.ToInt32(Session["IDSETOR"]), Convert.ToInt32(Session["IDEmpresa"]));
            PT.PreencheTBSetorGestor(ds, Convert.ToInt32(Session["IDUsuario"]), Convert.ToInt32(Session["IDEmpresa"]));
        }
        else
        {
            PT.PreencheTBSetorIDEmpresa(ds, Convert.ToInt32(Session["IDEmpresa"]));
        }

        cbSetorFolha.DataSource = ds;
        cbSetorFolha.IncrementalFilteringMode = DevExpress.Web.ASPxEditors.IncrementalFilteringMode.Contains;

        cbSetorFolha.DataBind();
    }

    protected void PreencheddlUsuario(string IDSetor)
    {
        MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwUsuariogridTableAdapter adpvwUsuariogrid =
           new MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwUsuariogridTableAdapter();
        MetodosPontoFrequencia.DataSetPontoFrequencia ds = new DataSetPontoFrequencia();

        ds.EnforceConstraints = false;

        adpvwUsuariogrid.FillIDEmpresaSetor(ds.vwUsuariogrid, Convert.ToInt32(Session["IDEmpresa"]), Convert.ToInt32(IDSetor));
        cbUsuario.DataSource = ds.vwUsuariogrid;
        cbUsuario.DataBind();
    }

    protected void PreencheddlMes()
    {
        PT.PreencheTBMes(ds);
        cbMes.DataSource = ds;
        cbMes.IncrementalFilteringMode = DevExpress.Web.ASPxEditors.IncrementalFilteringMode.Contains;
        cbMes.DataBind();
    }

    protected void HorasMes(int Mes)
    {
        Session["MesCorrente"] = cbMes.SelectedIndex;
        Session["IDSetorFolha"] = cbSetorFolha.SelectedItem.Value;
        Session["IDUsuarioFolhaFrequencia"] = cbUsuario.Value;
        Session["AnoFolhaFrequencia"] = rbAno.SelectedIndex;
        Response.Write("<script>window.open('/WebPontoFrequencia/Relatorio/frmVizualizaRelatorio.aspx');</script>");
        //Response.Write("<script>window.open('http://webponto.sad.infovia-mt/Relatorio/frmVizualizaRelatorio.aspx');</script>");
    }

    protected void btGerarFolhaFrequencia_Click(object sender, EventArgs e)
    {
        if (cbSetorFolha.Text != "")
            coIDUsuarioSetorFFrequencia["IDSetor"] = crip.CriptograFa(cbSetorFolha.Value.ToString());

        if (cbUsuario.Text != "")
        {

            // coIDUsuarioSetorFFrequencia["IDUsuario"] = crip.CriptograFa(cbUsuario.Value.ToString());
            coIDUsuarioSetorFFrequencia["IDVinculoUsuario"] = crip.CriptograFa(cbUsuario.Value.ToString());
            var idUser = justificativa.GetIdUserByVinculo(cbUsuario.Value.ToString());
            IDUsuario.Value = idUser;
            coIDUsuarioSetorFFrequenciaV["IDUsuario"] = crip.CriptograFa(IDUsuario.Value.ToString());

        }
        else
        {
            coIDUsuarioSetorFFrequencia["IDVinculoUsuario"] = crip.CriptograFa("0");
            coIDUsuarioSetorFFrequenciaV["IDUsuario"] = crip.CriptograFa("0");

        }



        this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> if(ASPxClientEdit.ValidateGroup('ValidaGrupo'))MudaPagina();</script>");
        //HorasMes(Convert.ToInt32(cbMes.SelectedItem.Value));
    }
    protected void btVoltar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }
    protected void btJustificar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Manutencao/Default.aspx");
    }
    protected void cbSetorFolha_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        PreencheddlUsuario(cbSetorFolha.SelectedItem.Value.ToString());
    }
    protected void cbSetorFolha_Callback1(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        PreencheddlUsuario(cbSetorFolha.SelectedItem.Value.ToString());
    }
    protected void cbUsuario_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        coIDUsuarioSetorFFrequencia["IDSetor"] = crip.CriptograFa(cbSetorFolha.Value.ToString());
        PreencheddlUsuario(cbSetorFolha.SelectedItem.Value.ToString());
    }
}