using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetodosPontoFrequencia;

public partial class Account_frmTrocaSenha : System.Web.UI.Page
{
    public string msg;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated == false)
        {
            Response.Redirect("~/Account/Login.aspx");
        }

        ASPxLabel1.Visible = false;
    }
    protected void TrocaSenha2(int IDUsuario, string SenhaAntiga)
    {
        if (tbSenhaNova.Text.Length <= 20)
        {
            MetodosPontoFrequencia.DataSetPontoFrequencia ds = new MetodosPontoFrequencia.DataSetPontoFrequencia();
            PreencheTabela PT = new PreencheTabela();
            Acesso AC = new Acesso();
            if (tbSenhaNova.Text != tbConfirmaSenhaNova.Text)
            {
                tbConfirmaSenhaNova.Text = "";
                tbSenhaAntiga.Text = "";
                tbSenhaNova.Text = "";
                ASPxLabel1.Text = "Nova senha difere da confirmação.";
                ASPxLabel1.Visible = true;
            }
            else if (tbSenhaNova.Text != "pontonarede")
            {
                Cript cript = new Cript();
                tbSenhaNova.Text = tbSenhaNova.Text.TrimStart();
                tbSenhaNova.Text = tbSenhaNova.Text.TrimEnd();
                tbSenhaNova.Text = tbSenhaNova.Text.Trim();

                msg = AC.SenhaAdmin2(IDUsuario, cript.ActionEncrypt(tbSenhaNova.Text),tbSenhaNova.Text.Trim(), 
                    cript.ActionEncrypt(SenhaAntiga), Convert.ToInt32(Session["IDEmpresa"]));
                if (msg == "1")
                {
                    ASPxLabel1.Text = "Senha alterada com sucesso.";
                    //Response.Redirect("/WebPontoFrequencia/Default.aspx");
                    PT.PreencheTBUsuarioIDUsuario(ds, IDUsuario, Convert.ToInt32(Session["IDEmpresa"]));
                    //Session["PrimeiroAcesso"] = ds.TBUsuario[0].PrimeiroAcesso;
                }
                else
                {
                    ASPxLabel1.Text = "Senha antiga não confere, tente novamente.";
                    ASPxLabel1.Visible = true;
                }
            }
            else
            {
                ASPxLabel1.Text = "Nova senha não pode ser igual a antiga. Repita o processo.";
                ASPxLabel1.Visible = true;
            }
        }
        else
        {
            ASPxLabel1.Text = "Senha não pode ter mais que 20 caracteres.";
            ASPxLabel1.Visible = true;
        }
    }
    protected void TrocaSenha(int IDUsuario, string SenhaAntiga)
    {
        MetodosPontoFrequencia.DataSetPontoFrequencia ds = new MetodosPontoFrequencia.DataSetPontoFrequencia();
        PreencheTabela PT = new PreencheTabela();
        Acesso AC = new Acesso();

        if (tbSenhaNova.Text != tbConfirmaSenhaNova.Text)
        {
            tbConfirmaSenhaNova.Text = "";
            tbSenhaAntiga.Text = "";
            tbSenhaNova.Text = "";
            ASPxLabel1.Text = "Nova senha difere da confirmação.";
            ASPxLabel1.Visible = true;
        }
        else if (tbSenhaNova.Text != "pontonarede")
        {

            tbSenhaNova.Text = tbSenhaNova.Text.TrimStart();
            tbSenhaNova.Text = tbSenhaNova.Text.TrimEnd();
            tbSenhaNova.Text = tbSenhaNova.Text.Trim();

            msg = AC.SenhaAdmin(IDUsuario, tbSenhaNova.Text, SenhaAntiga, Convert.ToInt32(Session["IDEmpresa"]));
            if (msg == "1")
            {
                ASPxLabel1.Text = "Senha alterada com sucesso.";
                //Response.Redirect("/WebPontoFrequencia/Default.aspx");
                PT.PreencheTBUsuarioID(ds, IDUsuario, Convert.ToInt32(Session["IDEmpresa"]));
                Session["PrimeiroAcesso"] = 2;
            }
            else
            {
                ASPxLabel1.Text = "Senha antiga não confere, tente novamente.";
                ASPxLabel1.Visible = true;
            }
        }
        else
        {
            ASPxLabel1.Text = "Nova senha não pode ser igual a antiga. Repita o processo.";
            ASPxLabel1.Visible = true;
        }
    }
    protected void btConfirmar_Click(object sender, EventArgs e)
    {
        TrocaSenha2(Convert.ToInt32(Session["IDUsuario"]), tbSenhaAntiga.Text);
        if (msg == "1")
            Response.Redirect("~/Default.aspx");
    }

    protected void btCancelar_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }

}