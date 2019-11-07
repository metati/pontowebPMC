using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using MetodosPontoFrequencia;

public partial class Account_Login : System.Web.UI.Page
{

    Acesso AC = new Acesso();// classe de acesso aos dados.

    DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        //RegisterHyperLink.NavigateUrl = "Register.aspx?ReturnUrl=" + HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
        if (HttpContext.Current.User.Identity.IsAuthenticated)
        {
            Response.Redirect("~/Default.aspx");
            
        }
    }
    protected void LoginUser_Authenticate(object sender, AuthenticateEventArgs e)
    {
        try
        {
            Cript crip = new Cript();
            //  AC.VerificaAcesso2(Server.HtmlEncode(LoginUser.UserName), Server.HtmlEncode(LoginUser.Password), ds); 
          //  AC.VerificaAcesso2(Server.HtmlEncode(LoginUser.UserName), Server.ActionEncrypt(LoginUser.Password), ds);
            AC.VerificaAcesso2(Server.HtmlEncode(LoginUser.UserName), Server.HtmlEncode(crip.ActionEncrypt(LoginUser.Password)), ds);

            e.Authenticated = AC.Autorizado;
        }
        catch (Exception ex)
        {
            this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('Falha na tentaviva de LOGIN. Tente Novamente');</script>");
            ex.ToString();
        }

        if (e.Authenticated == true)
        {
            Session["Usuario"] = AC.IDUSUARIO ;
            Session["DSUsuario"] = AC.PRIMEIRONOME;
            Session["IDUsuario"] = AC.IDUSUARIO;
            Session["PrimeiroAcesso"] = AC.PRIMEIROACESSO;
            Session["LOGIN"] = Server.HtmlEncode(LoginUser.UserName);
            Session["DIGITAL"] = AC.SENHADIGITAL;
            Session["DASHBOARDCORPORATIVO"] = AC.DASHBOARDPAINEL;
            Session.Add("TrocaSession", "0");
            
            //Se o Número de vínculos for > 1, Define a session para posterior escolha de vínculo. Senão realiza o vínculo aqui mesmo.
            if (AC.QTDVINCULOS > 1)
            {
                Session.Add("QTDVinculo", AC.QTDVINCULOS);// Para verificar a quantidade d vínculos cadastrados para o usuário que está tentando logar.
                Session.Add("VinculoSelecionado", false); //Ainda não houve seleção do vinculo para liberar o menu
            }
            else
            {
                DefineVinculo DF = new DefineVinculo();
                DF.DefineVinculousuario(AC.IDUSUARIO, false);

                Session.Add("VinculoSelecionado", true);//Houve seleção do vinculo para liberar o menu

                Session["TPUsuario"] = DF.IDTPUSUARIO.ToString();
                Session["IDSETOR"] = DF.IDSETOR.ToString();
                Session["IDEmpresa"] = DF.IDEMPRESA.ToString();
                Session["THU"] = DF.TOTALHORADIARIA.ToString();
                Session["DSEmpresa"] = DF.DSEMPRESA.ToString();
                Session["IDVinculoUsuarioFinal"] = DF.IDVINCULOUSUARIO.ToString();
            }

        }
    }
    protected void LoginButton_Click(object sender, EventArgs e)
    {

    }
}
