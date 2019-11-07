using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetodosPontoFrequencia;

public partial class frmVinculo : System.Web.UI.Page
{
    PreencheTabela PT = new PreencheTabela();
    DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        
        if (HttpContext.Current.User.Identity.IsAuthenticated == false)
        {
            Response.Redirect("~/Account/Login.aspx");
        }
        else if (!IsPostBack)
        {
            PreencheCombo(Convert.ToInt32(Session["IDUsuario"]));
        }
    }

    protected void PreencheCombo(int IDUsuario)
    {
        PT.PreenchevwVinculoUsuario(ds, IDUsuario);
        rlbVinculo.DataSource = ds.vwVinculoUsuario;
        rlbVinculo.TextField = "TextoFinal";
        rlbVinculo.ValueField = "IDVinculoUsuario";
        rlbVinculo.DataBind();

        Label1.Text = string.Format("{0}, seleciona um vínculo para começar!",Session["DSUSuario"]);
    }

    protected void trocaSession(int IDVinculoUsuario)
    {
        DefineVinculo df = new DefineVinculo();

        df.DefineVinculousuario(IDVinculoUsuario,true);

        //Troca se houver mais de um vínculo aqui
        Session["TPUsuario"] = df.IDTPUSUARIO.ToString();
        Session["IDSETOR"] = df.IDSETOR.ToString();
        Session["IDEmpresa"] = df.IDEMPRESA.ToString();
        Session["THU"] = df.TOTALHORADIARIA.ToString();
        Session["DSEmpresa"] = df.DSEMPRESA;
        Session["IDVinculoUsuario"] = df.IDVINCULOUSUARIO.ToString();
        Session["IDVinculoUsuarioFinal"] = df.IDVINCULOUSUARIO.ToString();
        Session["VinculoSelecionado"] = true;

        if (df.IDTPUSUARIO == 1)
            Response.Redirect("~/Defaultsession.aspx"); //Se admin geral, escolhe órgão para gerenciar. Senão direto p default.
        else
            Response.Redirect("~/Default.aspx");
    }
    protected void btSalvar_Click(object sender, EventArgs e)
    {
        trocaSession(Convert.ToInt32(rlbVinculo.SelectedItem.Value));
    }
}