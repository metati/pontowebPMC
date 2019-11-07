using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetodosPontoFrequencia;
using MetodosPontoFrequencia;

public partial class Relatorio_FiltroRelatorio_frmRelregistroAusente : System.Web.UI.Page
{

    DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
    public PreencheTabela PT = new PreencheTabela();
    public Frequencia Freq = new Frequencia();
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

        cbRelSetorFaltaInjustificada.DataSource = ds;
        cbRelSetorFaltaInjustificada.DataBind();
    }

    protected void PreencheddlUsuario(string IDSetor)
    {
        PT.PreenchevwNomeUsuario(ds, Convert.ToInt32(IDSetor));
        cbRelUsuarioFaltaInjustificada.DataSource = ds;
        cbRelUsuarioFaltaInjustificada.DataBind();
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated == false)
        {
            Response.Redirect("~/Account/Login.aspx");
        }
        else if (!IsPostBack)
        {
            deReldtInicioFaltaInjustificada.Date = System.DateTime.Now.Date.Date;
            deReldtFimFaltaInjustificada.Date = System.DateTime.Now.Date.Date;
            PreencheddlSetor();

        }

        if (cbRelSetorFaltaInjustificada.Text != "")
            coRelFalta["IDSetor"] = cr.CriptograFa(cbRelSetorFaltaInjustificada.Value.ToString());

        if (cbRelUsuarioFaltaInjustificada.Text != "")
            coRelFaltaUsuario["IDUsuario"] = cr.CriptograFa(cbRelUsuarioFaltaInjustificada.Value.ToString());
    }
    protected void btVoltar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }
    
    protected void cbRelUsuarioFaltaInjustificada_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        PreencheddlUsuario(cbRelSetorFaltaInjustificada.Value.ToString());
    }
}