using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetodosPontoFrequencia;

public partial class Account_ChangePassword : System.Web.UI.Page
{

    protected Acesso AC = new Acesso();
    string msg = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToInt32(Session["PrimeiroAcesso"]) == 2)
        {
            Response.Redirect("/WebPontoFrequencia/Default.aspx");
        }
    }
    protected void ChangeUserPassword_ChangedPassword(object sender, EventArgs e)
    {
        //AC.SenhaAdmin(Convert.ToInt32(Session["IDUsuario"]),ChangeUserPassword.NewPassword);
    }

    protected void ChangeUserPassword_CancelButtonClick(object sender, EventArgs e)
    {
        Response.Redirect("/WebPontoFrequencia/Account/Login.aspx");
    }
    protected void ChangeUserPassword_ChangingPassword(object sender, LoginCancelEventArgs e)
    {
        //AC.SenhaAdmin(Convert.ToInt32(Session["IDUsuario"]), ChangeUserPassword.NewPassword);
    }
}
