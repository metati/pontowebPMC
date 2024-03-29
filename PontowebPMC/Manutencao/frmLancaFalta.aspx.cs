﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetodosPontoFrequencia;

public partial class Manutencao_frmLancaFalta : System.Web.UI.Page
{
    DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
    Frequencia Freq = new Frequencia();
    PreencheTabela PT = new PreencheTabela();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated == false)
        {
            Response.Redirect("~/Default.aspx");
        }
        else if (!IsPostBack)
        {
            PreencheddlSetor();
            PreencheddlMotivoFalta();

            if (Convert.ToInt32(Session["TPUsuario"]) == 3)
            {
                PreencheddlUsuario();
            }
        }
    }

    protected void PreencheddlSetor()
    {
        if (Convert.ToInt32(Session["TPUsuario"]) == 3)
        {
            //PT.PreencheTBSetor(ds); -- cookie
            //PT.PreencheTBSetorIDSetor(ds, Convert.ToInt32(Session["IDSETOR"]), Convert.ToInt32(Session["IDEmpresa"]));
            PT.PreencheTBSetorGestor(ds, Convert.ToInt32(Session["IDUsuario"]), Convert.ToInt32(Session["IDEmpresa"]));
        }
        else
        {
            PT.PreencheTBSetorIDEmpresa(ds, Convert.ToInt32(Session["IDEmpresa"]));
        }

        cbSetor.DataSource = ds;

        cbSetor.DataBind();
    }
        protected void PreencheddlUsuario()
        {
            PT.PreenchevwNomeUsuario(ds, Convert.ToInt32(cbSetor.SelectedItem.Value));
            cbUsuario.DataSource = ds;
            cbUsuario.DataBind();
        }

        protected void PreencheddlMotivoFalta()
        {
            PT.PreencheTBMotivoFalta(ds);
            cbMotivoFalta.DataSource = ds;
            cbMotivoFalta.DataBind();
        }

    protected void btSalvar_Click(object sender, EventArgs e)
    {

        if (!cheqSetor.Checked)
            SalvarFalta(Convert.ToInt32(cbUsuario.SelectedItem.Value), deDataFalta.Date, Convert.ToInt32(cbMotivoFalta.SelectedItem.Value), memoOBS.Text);
        else
        {
            if (ds.vwNomeUsuario.Rows.Count > 0)
            {
                for (int i = 0; i <= (cbUsuario.Items.Count - 1); i++)
                {
                    SalvarFalta(Convert.ToInt32(ds.vwNomeUsuario[i].IDUsuario), deDataFalta.Date, Convert.ToInt32(cbMotivoFalta.SelectedItem.Value), memoOBS.Text);
                }

            }
            else
            {
                this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('Sem usuários para o setor selecionado.');</script>");
            }
        }

        cbUsuario.Text = "";
        cbMotivoFalta.Text = "";
        deDataFalta.Text = "";

        memoOBS.Text = "";
        
    }

    protected void SalvarFalta(int IDUsuario, DateTime DTFrequencia, int IDMotivoFalta, string OBS)
    {
        string msg = Freq.LancaFalta(IDUsuario, DTFrequencia, IDMotivoFalta, OBS, Convert.ToInt32(Session["IDEmpresa"]),0);
        this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('" + msg + "');</script>");
    }
    protected void cbUsuario_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        PreencheddlUsuario();
    }
}