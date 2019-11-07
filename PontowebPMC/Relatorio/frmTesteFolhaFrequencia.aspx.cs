using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetodosPontoFrequencia;

public partial class Relatorio_frmTesteFolhaFrequencia : System.Web.UI.Page
{

    DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
    Frequencia Freq = new Frequencia();

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Freq.ExcluiFeriasJustificativasFrequencia(118, 12);
        //Freq.GeraFolhaFrequencia(118, 2, 12, 2011, Convert.ToInt32(Session["IDEmpresa"]));
        //Freq.TotalHorasMes(4,12,2011);
    }
}