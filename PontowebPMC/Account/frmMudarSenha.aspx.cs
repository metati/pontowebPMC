using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetodosPontoFrequencia;

public partial class Account_frmMudarSenha : System.Web.UI.Page
{
    protected MetodosPontoFrequencia.DataSetPontoFrequencia ds = new  MetodosPontoFrequencia.DataSetPontoFrequencia();
    protected Acesso AC = new Acesso();
    protected PreencheTabela PT = new PreencheTabela();
    string msg = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToInt32(Session["PrimeiroAcesso"]) == 2)
        {
            Response.Redirect("~/Default.aspx");
        }
        
        ASPxLabel1.Visible = false;
    }

    protected void TrocaSenha(int IDUsuario, string SenhaAntiga)
    {
        if (tbSenhaNova.Text.Length <= 20)
        {

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

                msg = AC.SenhaAdmin2(IDUsuario, cript.ActionEncrypt(tbSenhaNova.Text),tbSenhaNova.Text.Trim(), cript.ActionEncrypt(SenhaAntiga), Convert.ToInt32(Session["IDEmpresa"]));
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
    protected void btConfirmar_Click(object sender, EventArgs e)
    {
        TrocaSenha(Convert.ToInt32(Session["IDUsuario"]), tbSenhaAntiga.Text);
        if (msg == "1")
        {
            Session["PrimeiroAcesso"] = 2;
            Response.Redirect("~/Default.aspx");
        }
    }
    protected void btCancelar_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("~/default.aspx");
    }
}