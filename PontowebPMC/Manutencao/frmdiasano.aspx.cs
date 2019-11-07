using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Manutencao_frmdiasano : System.Web.UI.Page
{
    DateTime PrimeiroDiaMes;
    DateTime PrimeiroDiaProximoMes;
    DateTime UltimoDiaMes;
    DateTime Dias;

    MetodosPontoFrequencia.DataSetPontoFrequencia ds = new MetodosPontoFrequencia.DataSetPontoFrequencia();
    MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.TBDiasAnoTableAdapter adpDiasAno = new MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.TBDiasAnoTableAdapter();

    MetodosPontoFrequencia.PreencheTabela pt = new MetodosPontoFrequencia.PreencheTabela();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated == false)
        {
            Response.Redirect("~/Account/Login.aspx");
        }
    }
    protected void cbDia_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        pbDia.Position = 0;
        pbDia.Visible = true;
        pbDia.DataBind();
        InserirDiasAno();
    }

    public void InserirDiasAno()
    {
        for (int i = 1; i <= 12; i++)
        {
            DateTime PrimeiroDiaMes = new DateTime(System.DateTime.Now.Year, i, 01); //Variaveis Manipulação de dias
            DateTime PrimeiroDiaProximoMes = PrimeiroDiaMes.AddMonths(1);
            DateTime UltimoDiaMes = PrimeiroDiaProximoMes.AddDays(-1);
            DateTime Dias = PrimeiroDiaMes;

            while (Dias <= UltimoDiaMes)
            {
                if (!pt.Permitealteracao(ds, Dias.ToShortDateString(), Convert.ToInt32(Session["IDEmpresa"])))
                    adpDiasAno.Insert(Dias, Convert.ToInt32(Session["IDEmpresa"]), false, ""); ;
                Dias = Dias.AddDays(1);
            }
            
            pbDia.Position = i+10;
            pbDia.DataBind();
        }
        pbDia.Position = 100;
        pbDia.DataBind();
    }
}