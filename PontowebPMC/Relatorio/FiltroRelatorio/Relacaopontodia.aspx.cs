using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetodosPontoFrequencia;
using MetodosPontoFrequencia.Justificativa;

public partial class Relatorio_FiltroRelatorio_Relacaopontodia : System.Web.UI.Page
{
    DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
    public PreencheTabela PT = new PreencheTabela();
    public Frequencia Freq = new Frequencia();
    Justificativa justificativa = new Justificativa();
    private Crip cr = new Crip();

    protected void PreencheddlSetor()
    {

        if (Convert.ToInt32(Session["TPUsuario"]) == 3 || Convert.ToInt32(Session["TPUsuario"]) == 9)
        {
            //PT.PreencheTBSetor(ds);


            //PT.PreencheTBSetorIDSetor(ds, Convert.ToInt32(Session["IDSETOR"]), Convert.ToInt32(Session["IDEmpresa"]));
            PT.PreencheTBSetorGestor(ds, Convert.ToInt32(Session["IDUsuario"]), Convert.ToInt32(Session["IDEmpresa"]));
        }
        else if (Convert.ToInt32(Session["TPUsuario"]) == 1 || Convert.ToInt32(Session["TPUsuario"]) == 8)
        {
            PT.PreencheTBSetorIDEmpresa(ds, Convert.ToInt32(Session["IDEmpresa"]));
        }

        cbRelacaoDia.DataSource = ds;
        cbRelacaoDia.DataBind();
    }

    protected void PreencheddlUsuario(string IDSetor)
    {
        //PT.PreenchevwNomeUsuario(ds, Convert.ToInt32(IDSetor));
        //cbUsuarioRelacaoPontoDia.DataSource = ds;
        //cbUsuarioRelacaoPontoDia.DataBind();
        MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwUsuariogridTableAdapter adpvwUsuariogrid =
         new MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwUsuariogridTableAdapter();
        MetodosPontoFrequencia.DataSetPontoFrequencia ds = new DataSetPontoFrequencia();

        ds.EnforceConstraints = false;

        adpvwUsuariogrid.FillIDEmpresaSetor(ds.vwUsuariogrid, Convert.ToInt32(Session["IDEmpresa"]), Convert.ToInt32(IDSetor));
        cbUsuarioRelacaoPontoDia.DataSource = ds.vwUsuariogrid;
        cbUsuarioRelacaoPontoDia.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated == false)
        {
            Response.Redirect("~/Account/Login.aspx");
        }
        if (!IsPostBack)
        {
            PreencheddlSetor();
            deDataRelacao.Date = System.DateTime.Now.Date.Date;
            deDataRelacao0.Date = System.DateTime.Now.Date.Date;
            deDataRelacao0.DataBind();
            deDataRelacao.DataBind();
            coRelacaoPontoDia.Add("IDSetor", "");
            coIDUsuarioRelacaoPontoDiaV.Add("IDUsuario", "");
            coIDUsuarioRelacaoPontoDia.Add("IDVinculoUsuario", "");
        }

        if (cbRelacaoDia.Text != "")
            coRelacaoPontoDia["IDSetor"] = cr.CriptograFa(cbRelacaoDia.Value.ToString());

        if (cbUsuarioRelacaoPontoDia.Text != "")
        {
            coIDUsuarioRelacaoPontoDia["IDVinculoUsuario"] = cr.CriptograFa(cbUsuarioRelacaoPontoDia.Value.ToString());
            var idUser = justificativa.GetIdUserByVinculo(cbUsuarioRelacaoPontoDia.Value.ToString());
            IDUsuario.Value = idUser;
            coIDUsuarioRelacaoPontoDiaV["IDUsuario"] = cr.CriptograFa(idUser);
        }

    }
    protected void btVoltar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }
    protected void btRelacaoDia_Click(object sender, EventArgs e)
    {
        this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> if(ASPxClientEdit.ValidateGroup('ValidaGrupo'))MudaPagina();</script>");
    }
    protected void cbUsuarioRelacaoPontoDia_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        PreencheddlUsuario(e.Parameter);
    }
}