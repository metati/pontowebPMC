using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetodosPontoFrequencia;
using DevExpress.Web.ASPxClasses;
using DevExpress.XtraCharts;
using DevExpress.XtraCharts.Web;
using DevExpress.Web;

public partial class Painel_frmPainelEmpresa : System.Web.UI.Page
{

    DataSetPontoFrequencia dsP = new DataSetPontoFrequencia();
    MetodosPontoFrequencia.DataSetPontoFrequencia dsB = new DataSetPontoFrequencia();

    MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwRegistrosDiaEmpresaTableAdapter adpEmpresaGeral = new MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwRegistrosDiaEmpresaTableAdapter();

    MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwRegistrosDiaEmpresaSetorTableAdapter adpRegistroSetor = new MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwRegistrosDiaEmpresaSetorTableAdapter();
    MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwTotalMotivosFaltaSetorTableAdapter adpMotivoFaltaSetor = new MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwTotalMotivosFaltaSetorTableAdapter();

    protected string PegarImagem(DevExpress.Web.ASPxGridView.GridViewDataItemTemplateContainer container)
    {
        string caminho;
        DateTime DataParametro;
        DataParametro = Convert.ToDateTime(gridRegistro.GetRowValues(container.VisibleIndex, "MaiorRegistro").ToString());

        if (System.DateTime.Now >= DataParametro.AddMinutes(10))
        {
            caminho = "~/Images/VermelhoGrid.png";
        }
        else
            caminho = "~/Images/VerdeGrid.png";

        return caminho;
    }

    protected void PreencheGridRegistro(int IDEmpresa)
    {
        dsB.EnforceConstraints = false;
        MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwLogRegistroSetorTableAdapter adpLog = new MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwLogRegistroSetorTableAdapter();
        try
        {
            adpLog.FillTodos(dsB.vwLogRegistroSetor, IDEmpresa);


            gridRegistro.DataSource = dsB.vwLogRegistroSetor;
            gridRegistro.DataBind();
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    protected void PreenchecbSetor(int IDEmpresa)
    {
        PreencheTabela PT = new PreencheTabela();

        if (Convert.ToInt32(Session["TPUsuario"]) == 3 || Convert.ToInt32(Session["TPUsuario"]) == 9)
        {
            //PT.PreencheTBSetor(ds); -- cookie
            //PT.PreencheTBSetorIDSetor(ds, Convert.ToInt32(Session["IDSETOR"]), Convert.ToInt32(Session["IDEmpresa"]));
            PT.PreencheTBSetorGestor(dsP, Convert.ToInt32(Session["IDUsuario"]), IDEmpresa);
        }
        else
        {
            PT.PreencheTBSetorIDEmpresa(dsP, IDEmpresa);
        }


        cbSetorPainel.DataSource = dsP.TBSetor;
        cbSetorPainel.ValueField = "IDSetor";
        cbSetorPainel.TextField = "DSSetor";

        //if (Session["TPUsuario"].ToString() != "1" || Session["TPUsuario"].ToString() != "7")
        //{
            //cbSetorPainel.ValidationSettings.RequiredField.IsRequired = true;
            //cbSetorPainel.SelectedIndex = 0;
        //}

        cbSetorPainel.DataBind();

        //Troca da Session["IDEmpresa"] para a que for escolhida na como de empresa.
        //Session["IDEmpresa"] = cbEmpresaPainel.Value.ToString();
    }

    protected void PreenchecbEmpresa()
    {
        //Opção só para usuários administradores.
        if (Session["TPUsuario"].ToString() != "1")
        {
            cbGrupoGeral.Visible = false;
            cbGrupoGeral.DataBind();
            return;
        }

        PreencheTabela PT = new PreencheTabela();
        PT.PreencheTBEmpresaAdmin(dsP);

        cbEmpresaPainel.DataSource = dsP.TBEmpresa;
        cbEmpresaPainel.TextField = "DSEmpresa";
        cbEmpresaPainel.ValueField = "IDEmpresa";
        cbEmpresaPainel.DataBind();
    }

    protected string PreencheGrafico(DataSetPontoFrequencia ds , string visao, string periodo, string IDTPUsuario, string TPGrafico)
    {
        string texto = string.Empty;
        string empresa = string.Empty;
        string diaMesAno = string.Empty;
        string idempresa = string.Empty;
        string setor = string.Empty;

        if (cbSetorPainel.Text != "")
        {
            setor = cbSetorPainel.Text.Trim();
        }

        if (IDTPUsuario == "7" || IDTPUsuario == "3" || IDTPUsuario == "9" || IDTPUsuario == "8")
        {
            idempresa = Session["IDEmpresa"].ToString();
            empresa = Session["DSEmpresa"].ToString();
        }
        else if (!cbGrupoGeral.Checked && IDTPUsuario == "1") //Se não estiver marcado e usuário for admin
        {
            idempresa = cbEmpresaPainel.Value.ToString();
            empresa = cbEmpresaPainel.Text.Trim();
        }
        else if (cbGrupoGeral.Checked)
            empresa = "PREFEITURA MUNICIPAL DE CUIABÁ";
        /// 11/06/2015 Definir a visão pelo tipo de usuário---
        /// O administrador possui o maior número de visão
        /// Se visao = 0 - Fazer gráfico geral - Todos os órgãos juntos
        /// Se visao = 1 - Filtrar por órgão
        /// Se visao = 2 - Filtrar por setor.
        /// 11/06/2015
        /// 

        ///11/06/2015 Período 
        ///Caso período = 0 - Filtrar por dia
        ///Período = 1 - Filtrar por Mês/Ano
        ///Período = 2 - Filtrar por Ano.

        if (visao == "2") //rotina permitida a todos os tipos de usuários
        {
            try
            {
                MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwRegistroDiaEmpresaSetorUsuarioTableAdapter adpRegistroUsuario = new MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwRegistroDiaEmpresaSetorUsuarioTableAdapter();   
                dsB.EnforceConstraints = false;
                switch (periodo)
                {

                    case"0": //Dia

                        if (TPGrafico == "IndicePresenca")
                        {
                            adpRegistroSetor.FillDTDiasAno(dsP.vwRegistrosDiaEmpresaSetor, Convert.ToInt32(cbSetorPainel.Value), Convert.ToInt32(idempresa), deDTReferencia.Date.Date);
                        }
                        else
                        {
                            adpMotivoFaltaSetor.FillDTFrequenciaIDEmpresaIDSetor(dsP.vwTotalMotivosFaltaSetor, deDTReferencia.Date.Date, Convert.ToInt32(idempresa), Convert.ToInt32(cbSetorPainel.Value));
                        }

                        diaMesAno = deDTReferencia.Date.ToShortDateString();
                        
                        dsB.EnforceConstraints = false;
                        MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vWHorasDiaTableAdapter adpHorasDia = new MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vWHorasDiaTableAdapter();
                        adpHorasDia.FillFreqDia(dsB.vWHorasDia, Convert.ToInt32(idempresa), Convert.ToInt32(cbSetorPainel.Value), deDTReferencia.Date.Date, deDTReferencia.Date.Date);
                        
                        break;
                    case"1": //mês/Ano
                        if (cbMesAnoReferencia.Text != "" && cbMesAnoReferencia.Text.IndexOf('/') >= 0)
                        {
                            diaMesAno = cbMesAnoReferencia.Text.Trim();
                            string mes, ano; int pos;
                            pos = cbMesAnoReferencia.Text.IndexOf('/');
                            mes = cbMesAnoReferencia.Text.Substring(0, pos);
                            ano = cbMesAnoReferencia.Text.Substring(pos + 1, 4);

                            if (TPGrafico == "IndicePresenca")
                                adpRegistroSetor.FillMesAnoEmpresaSetor(dsP.vwRegistrosDiaEmpresaSetor, Convert.ToInt32(ano), Convert.ToInt32(mes), Convert.ToInt32(idempresa), Convert.ToInt32(cbSetorPainel.Value));
                            else
                                adpMotivoFaltaSetor.FillMesAnoIDEmpresaIDSetor(dsP.vwTotalMotivosFaltaSetor, Convert.ToInt32(mes), Convert.ToInt32(ano), Convert.ToInt32(idempresa), Convert.ToInt32(cbSetorPainel.Value));

                            adpRegistroUsuario.FillPerctMesAnoEmpresaSetor(dsB.vwRegistroDiaEmpresaSetorUsuario, Convert.ToInt32(idempresa), Convert.ToInt32(cbSetorPainel.Value), Convert.ToInt32(mes), Convert.ToInt32(ano));
                        }
                        else
                        {
                            diaMesAno = string.Format("{0}/{1}", System.DateTime.Now.Month, System.DateTime.Now.Year);
                            if(TPGrafico=="IndicePresenca")
                                adpRegistroSetor.FillMesAnoEmpresaSetor(dsP.vwRegistrosDiaEmpresaSetor,System.DateTime.Now.Year, System.DateTime.Now.Month, Convert.ToInt32(idempresa), Convert.ToInt32(Session["IDSETOR"]));
                            else
                                adpMotivoFaltaSetor.FillMesAnoIDEmpresaIDSetor(dsP.vwTotalMotivosFaltaSetor, System.DateTime.Now.Month, System.DateTime.Now.Year, Convert.ToInt32(idempresa), Convert.ToInt32(cbSetorPainel.Value));

                            adpRegistroUsuario.FillPerctMesAnoEmpresaSetor(dsB.vwRegistroDiaEmpresaSetorUsuario, Convert.ToInt32(idempresa), Convert.ToInt32(cbSetorPainel.Value), System.DateTime.Now.Month, System.DateTime.Now.Year);
                        }

                        break;
                    case"2":
                        if (cbMesAnoReferencia.Text.IndexOf('/') >= 0)
                        {
                            string ano1; int pos1;
                            pos1 = cbMesAnoReferencia.Text.IndexOf('/');
                            ano1 = cbMesAnoReferencia.Text.Substring(pos1 + 1, 4);
                            diaMesAno = ano1;
                            if (TPGrafico == "IndicePresenca")
                                adpRegistroSetor.FillAnoEmpresaSetor(dsP.vwRegistrosDiaEmpresaSetor, Convert.ToInt32(ano1), Convert.ToInt32(idempresa), Convert.ToInt32(cbSetorPainel.Value));
                            else
                                adpMotivoFaltaSetor.FillAnoEmpresaSetor(dsP.vwTotalMotivosFaltaSetor, Convert.ToInt32(ano1), Convert.ToInt32(idempresa), Convert.ToInt32(cbSetorPainel.Value));


                            adpRegistroUsuario.FillPercTAnoEmpresaSetor(dsB.vwRegistroDiaEmpresaSetorUsuario, Convert.ToInt32(idempresa), Convert.ToInt32(cbSetorPainel.Value), Convert.ToInt32(ano1));
                        }
                        else
                        {
                            diaMesAno = System.DateTime.Now.Year.ToString();
                            if (TPGrafico == "IndicePresenca")
                                adpRegistroSetor.FillAnoEmpresaSetor(dsP.vwRegistrosDiaEmpresaSetor, System.DateTime.Now.Year, Convert.ToInt32(idempresa), Convert.ToInt32(Session["IDSETOR"]));
                            else
                                adpMotivoFaltaSetor.FillAnoEmpresaSetor(dsP.vwTotalMotivosFaltaSetor, System.DateTime.Now.Year, Convert.ToInt32(idempresa), Convert.ToInt32(cbSetorPainel.Value));

                            
                            adpRegistroUsuario.FillPercTAnoEmpresaSetor(dsB.vwRegistroDiaEmpresaSetorUsuario, Convert.ToInt32(idempresa), Convert.ToInt32(cbSetorPainel.Value), System.DateTime.Now.Year);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            texto = string.Format("{0} / {1} - Referência: {2}", empresa, setor, diaMesAno);
            return texto;
        }

        if ((IDTPUsuario == "1" || IDTPUsuario == "7" || IDTPUsuario == "8") && visao == "1")
        {
            //Aqui realizar o preenchimento para admin e admin de orgao

                MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwRegistrosDiaEmpresaSetorTableAdapter adpregistroDiaEmpresaSetor = new MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwRegistrosDiaEmpresaSetorTableAdapter();
                dsB.EnforceConstraints = false;
                
            switch(periodo)
                {
                    case"0": //Dia
                        if (TPGrafico == "IndicePresenca")
                            adpEmpresaGeral.FillIDEmpresaDTDia(dsP.vwRegistrosDiaEmpresa, Convert.ToInt32(idempresa), deDTReferencia.Date.Date);
                        else
                            
                            adpMotivoFaltaSetor.FillDTFrequenciaEmpresa(dsP.vwTotalMotivosFaltaSetor, deDTReferencia.Date.Date, Convert.ToInt32(idempresa));
                        //Data do texto
                        diaMesAno = deDTReferencia.Date.ToShortDateString();

                        adpregistroDiaEmpresaSetor.FillPerctDia(dsB.vwRegistrosDiaEmpresaSetor, Convert.ToInt32(idempresa), deDTReferencia.Date.Date);
                        break;
                    case"1": //Mes/Ano
                        if (cbMesAnoReferencia.Text != "" && cbMesAnoReferencia.Text.IndexOf('/') >= 0)
                        {
                            diaMesAno = cbMesAnoReferencia.Text.Trim();
                            string mes, ano; int pos;
                            pos = cbMesAnoReferencia.Text.IndexOf('/');
                            mes = cbMesAnoReferencia.Text.Substring(0, pos);
                            ano = cbMesAnoReferencia.Text.Substring(pos + 1, 4);

                            if(TPGrafico=="IndicePresenca")
                                adpEmpresaGeral.FilllIDEmpresaMesAno(dsP.vwRegistrosDiaEmpresa, Convert.ToInt32(mes), Convert.ToInt32(ano), Convert.ToInt32(idempresa));
                            else
                                adpMotivoFaltaSetor.FillMesAnoEmpresa(dsP.vwTotalMotivosFaltaSetor,Convert.ToInt32(mes),Convert.ToInt32(ano),Convert.ToInt32(idempresa));

                            adpregistroDiaEmpresaSetor.FillPerctMesAno(dsB.vwRegistrosDiaEmpresaSetor, Convert.ToInt32(ano), Convert.ToInt32(mes), Convert.ToInt32(idempresa));
                        }
                        else
                        {
                            diaMesAno = string.Format("{0}/{1}", System.DateTime.Now.Month, System.DateTime.Now.Year);
                            
                            if(TPGrafico=="IndicePresenca")
                                adpEmpresaGeral.FilllIDEmpresaMesAno(dsP.vwRegistrosDiaEmpresa, System.DateTime.Now.Month, System.DateTime.Now.Year, Convert.ToInt32(idempresa));
                            else
                                adpMotivoFaltaSetor.FillMesAnoEmpresa(dsP.vwTotalMotivosFaltaSetor,System.DateTime.Now.Month,System.DateTime.Now.Year,Convert.ToInt32(idempresa));

                            adpregistroDiaEmpresaSetor.FillPerctMesAno(dsB.vwRegistrosDiaEmpresaSetor, System.DateTime.Now.Year, System.DateTime.Now.Month, Convert.ToInt32(idempresa));
                        }
                        break;
                    case"2": //Ano
                        if (cbMesAnoReferencia.Text.IndexOf('/') >= 0)
                        {
                            diaMesAno = cbMesAnoReferencia.Text.Trim();
                            string ano; int pos;
                            pos = cbMesAnoReferencia.Text.IndexOf('/');
                            ano = cbMesAnoReferencia.Text.Substring(pos + 1, 4);

                            if(IDTPUsuario=="IndicePresenca")
                                adpEmpresaGeral.FillIDEmpresaAno(dsP.vwRegistrosDiaEmpresa, Convert.ToInt32(ano), Convert.ToInt32(idempresa));
                            else
                                adpMotivoFaltaSetor.FillAnoEmpresa(dsP.vwTotalMotivosFaltaSetor,Convert.ToInt32(ano),Convert.ToInt32(idempresa));

                            adpregistroDiaEmpresaSetor.FillPerctAno(dsB.vwRegistrosDiaEmpresaSetor, Convert.ToInt32(ano), Convert.ToInt32(idempresa));
                        }
                        else
                        {
                            diaMesAno = System.DateTime.Now.Year.ToString();
                            
                            if(IDTPUsuario=="IndicePresenca")
                                adpEmpresaGeral.FillIDEmpresaAno(dsP.vwRegistrosDiaEmpresa, System.DateTime.Now.Year, Convert.ToInt32(idempresa));
                            else
                                adpMotivoFaltaSetor.FillAnoEmpresa(dsP.vwTotalMotivosFaltaSetor,System.DateTime.Now.Year,Convert.ToInt32(idempresa));

                            adpregistroDiaEmpresaSetor.FillPerctAno(dsB.vwRegistrosDiaEmpresaSetor, System.DateTime.Now.Year, Convert.ToInt32(idempresa));
                        }
                        break;
                }
                texto = string.Format("{0} - Referência: {1}",empresa,diaMesAno);
            return texto;
        }
        if (IDTPUsuario == "1" && visao == "0")
        {
            MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwRegistrosDiaEmpresaTableAdapter adpRegistroDiaEmpresa = new MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwRegistrosDiaEmpresaTableAdapter();
            dsB.EnforceConstraints = false;
            switch (periodo)
            {
                case "0": //Dia
                    if (TPGrafico == "IndicePresenca")
                        adpEmpresaGeral.FillDTDiasAno(dsP.vwRegistrosDiaEmpresa, deDTReferencia.Date.Date);
                    else
                        adpMotivoFaltaSetor.FillDTFrequencia(dsP.vwTotalMotivosFaltaSetor, deDTReferencia.Date.Date);
                    
                    diaMesAno = deDTReferencia.Date.ToShortDateString();
                    
                    //Para usar nos gráficos de baixo - Índice de Registros, Justificativas e faltas !
                    adpRegistroDiaEmpresa.FillPerctDia(dsB.vwRegistrosDiaEmpresa, deDTReferencia.Date.Date);
                    break;
                case "1": //Mes/Ano
                    if (cbMesAnoReferencia.Text != "" && cbMesAnoReferencia.Text.IndexOf('/') >= 0)
                    {
                        diaMesAno = cbMesAnoReferencia.Text.Trim();
                        string mes, ano; int pos;
                        pos = cbMesAnoReferencia.Text.IndexOf('/');
                        mes = cbMesAnoReferencia.Text.Substring(0, pos);
                        ano = cbMesAnoReferencia.Text.Substring(pos + 1, 4);
                        if (TPGrafico == "IndicePresenca")
                            adpEmpresaGeral.FillMesAno(dsP.vwRegistrosDiaEmpresa, Convert.ToInt32(mes), Convert.ToInt32(ano));
                        else
                            adpMotivoFaltaSetor.FillMesAno(dsP.vwTotalMotivosFaltaSetor, Convert.ToInt32(mes), Convert.ToInt32(ano));

                        //Gráficos de baixo
                        adpRegistroDiaEmpresa.FillPerctMesAno(dsB.vwRegistrosDiaEmpresa, Convert.ToInt32(mes), Convert.ToInt32(ano));
                        
                    }
                    else
                    {
                        diaMesAno = string.Format("{0}/{1}", System.DateTime.Now.Month, System.DateTime.Now.Year);

                        if (TPGrafico == "IndicePresenca")
                            adpEmpresaGeral.FillMesAno(dsP.vwRegistrosDiaEmpresa, System.DateTime.Now.Month, System.DateTime.Now.Year);
                        else
                            adpMotivoFaltaSetor.FillMesAno(dsP.vwTotalMotivosFaltaSetor, System.DateTime.Now.Month, System.DateTime.Now.Year);

                        //Para usar nos gráficos de baixo - Índice de Registros, Justificativas e faltas !
                        adpRegistroDiaEmpresa.FillPerctMesAno(dsB.vwRegistrosDiaEmpresa, System.DateTime.Now.Month, System.DateTime.Now.Year);
                    }
                    break;
                case "2": //Ano
                    if (cbMesAnoReferencia.Text != "" && cbMesAnoReferencia.Text.IndexOf('/') < 0)
                    {
                        diaMesAno = cbMesAnoReferencia.Text.Trim();
                        string ano; int pos;
                        pos = cbMesAnoReferencia.Text.IndexOf('/');
                        ano = cbMesAnoReferencia.Text.Substring(pos + 1, 4);
                        if (TPGrafico == "IndicePresenca")
                            adpEmpresaGeral.FillAno(dsP.vwRegistrosDiaEmpresa, Convert.ToInt32(ano));
                        else
                            adpMotivoFaltaSetor.FillAno(dsP.vwTotalMotivosFaltaSetor, Convert.ToInt32(ano));

                        //Gráficos de baixo
                        adpRegistroDiaEmpresa.FillPerctAno(dsB.vwRegistrosDiaEmpresa, Convert.ToInt32(ano));
                    }
                    else
                    {
                        diaMesAno = System.DateTime.Now.Year.ToString();
                        if(TPGrafico=="IndicePresencao")
                            adpEmpresaGeral.FillAno(dsP.vwRegistrosDiaEmpresa, System.DateTime.Now.Year);
                        else
                            adpMotivoFaltaSetor.FillAno(dsP.vwTotalMotivosFaltaSetor, System.DateTime.Now.Year);

                        //Gráficos de baixo
                        adpRegistroDiaEmpresa.FillPerctAno(dsB.vwRegistrosDiaEmpresa, System.DateTime.Now.Year);
                    }
                    break;
            }
            texto = string.Format("{0} - Referência: {1}", empresa, diaMesAno);
            return texto;
        }

        return texto;
    }
    
    protected void PreenchecbMesAnoRef(int IDEmpresa, string PosVisao)
    {
        switch (PosVisao)
        {
            case "0":
                cbMesAnoReferencia.Visible = false;
                break;
            case "1":
                MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwMesAnoReferenciaTableAdapter adpMesAno = new MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwMesAnoReferenciaTableAdapter();
                adpMesAno.FillIDEmpresa(dsP.vwMesAnoReferencia, IDEmpresa);
                cbMesAnoReferencia.Visible = true;
                deDTReferencia.Visible = false;
                cbMesAnoReferencia.DataSource = dsP.vwMesAnoReferencia;
                cbMesAnoReferencia.Value = -1;
                cbMesAnoReferencia.Text = "";
                cbMesAnoReferencia.Text = string.Format("{0}/{1}", System.DateTime.Now.Month.ToString(), System.DateTime.Now.Year.ToString());
                break;
            case"2":
                cbMesAnoReferencia.Items.Clear();
                cbMesAnoReferencia.Columns.Clear();
                MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwAnoTableAdapter adpAno = new MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwAnoTableAdapter();
                adpAno.Fill(dsP.vwAno);
                cbMesAnoReferencia.Visible = true;
                deDTReferencia.Visible = false;
                cbMesAnoReferencia.DataSource = dsP.vwAno;
                cbMesAnoReferencia.TextField = "AnoReferencia";
                cbMesAnoReferencia.Text = "";
                cbMesAnoReferencia.Text = System.DateTime.Now.Year.ToString();
                break;
        }
        
        cbMesAnoReferencia.DataBind();
    }

    protected void PreencheGraficosPresencaJustificativa(string Periodo, string Visao, string TPGrafico)
    {
        try
        {
            dsP.EnforceConstraints = false;

            //Para Preencher Gráfico
            lblarica.Text =  PreencheGrafico(dsP, Visao, Periodo, Session["TPUsuario"].ToString(),TPGrafico);

            //CONTINUAR DAQUI - 12/06/2016
            
            switch (TPGrafico)
            {
                case"IndicePresenca":
                    //Gráfico de Presença
                    if (dsP.vwRegistrosDiaEmpresa.Rows.Count > 0 || dsP.vwRegistrosDiaEmpresaSetor.Rows.Count > 0)
                    {
                        //Montando dados do Grafico
                        graficoGeral.Series.Remove(graficoGeral.Series["Registro Efetuado"]);

                        graficoGeral.Series.Add("Registro Efetuado", ViewType.Pie);

                        //XYDiagram diagrama = graficoGeral.Diagram as XYDiagram;

                        //diagrama.AxisY.Visible = false;
                        //diagrama.AxisX.Visible = false;      

                        graficoGeral.Legend.Visible = false;

                        graficoGeral.Legend.TextVisible = false;
                        graficoGeral.Legend.AlignmentVertical = LegendAlignmentVertical.Top;
                        graficoGeral.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;


                        graficoGeral.Series["Registro Efetuado"].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

                        graficoGeral.Series["Registro Efetuado"].Label.Border.Visible = false;
                        graficoGeral.Series["Registro Efetuado"].Label.LineVisible = true;

                        graficoGeral.Series["Registro Efetuado"].Label.PointOptions.PointView = PointView.ArgumentAndValues;


                        graficoGeral.Series["Registro Efetuado"].Label.TextAlignment = System.Drawing.StringAlignment.Center;

                        //Para trocar a visão da legenda ...
                        graficoGeral.Series["Registro Efetuado"].LegendPointOptions.PointView = PointView.Argument;

                        graficoGeral.Series["Registro Efetuado"].ValueScaleType = ScaleType.Numerical;

                        //Título dos gráficos
                        //Dados extas ao gráfico no título
                        ChartTitle Titulo = new ChartTitle();
                        //ChartTitle Titulo2 = new ChartTitle();

                        Titulo.Font = new System.Drawing.Font("Tahoma", 11, System.Drawing.FontStyle.Bold);

                        //Titulo2.Font = new System.Drawing.Font("Tahoma", 9, System.Drawing.FontStyle.Bold);



                        Titulo.Text = "Situação Geral";

                        if (graficoGeral.Titles.Count > 0)
                            graficoGeral.Titles.Clear();

                        graficoGeral.Titles.Add(Titulo);

                        //Se visão diferente de dois, seguir com os principais, senão usar usar view vwRegistroDiaSetor
                        if (Visao != "2")
                        {
                            graficoGeral.Series["Registro Efetuado"].Points.Add(new SeriesPoint("Registro(s)", dsP.vwRegistrosDiaEmpresa[0].QTDRegistro));
                            graficoGeral.Series["Registro Efetuado"].Points.Add(new SeriesPoint("Ausência(s)", dsP.vwRegistrosDiaEmpresa[0].QTDAusencia));
                            graficoGeral.Series["Registro Efetuado"].Points.Add(new SeriesPoint("Justificado(s)", dsP.vwRegistrosDiaEmpresa[0].QTDJustificado));
                        }
                        else
                        {
                            graficoGeral.Series["Registro Efetuado"].Points.Add(new SeriesPoint("Registro(s)", dsP.vwRegistrosDiaEmpresaSetor[0].QTDRegistro));
                            graficoGeral.Series["Registro Efetuado"].Points.Add(new SeriesPoint("Ausência(s)", dsP.vwRegistrosDiaEmpresaSetor[0].QTDAusencia));
                            graficoGeral.Series["Registro Efetuado"].Points.Add(new SeriesPoint("Justificado(s)", dsP.vwRegistrosDiaEmpresaSetor[0].QTDJustificado));
                        }
                    }

                    break;
                case"IndiceJustificativa":
                    //Gráfico de Justificativa
                    if (dsP.vwTotalMotivosFaltaSetor.Rows.Count > 0)
                    {
                        //Montando dados do Grafico
                        graficoGeralJustificativa.Series.Remove(graficoGeralJustificativa.Series["Series 1"]);

                        graficoGeralJustificativa.Series.Add("Series 1", ViewType.Pie);

                        //XYDiagram diagrama = graficoGeralJustificativa.Diagram as XYDiagram;

                        //diagrama.AxisY.Visible = false;
                        //diagrama.AxisX.Visible = false;      

                        graficoGeralJustificativa.Legend.Visible = false;

                        graficoGeralJustificativa.Legend.TextVisible = true;
                        graficoGeralJustificativa.Legend.AlignmentVertical = LegendAlignmentVertical.Top;
                        graficoGeralJustificativa.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right;

                        //graficoGeralJustificativa.Series["Registro Efetuado"].Label.

                        graficoGeralJustificativa.Series["Series 1"].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;



                        graficoGeralJustificativa.Series["Series 1"].Label.Border.Visible = false;
                        graficoGeralJustificativa.Series["Series 1"].Label.LineVisible = true;

                        graficoGeralJustificativa.Series["Series 1"].Label.PointOptions.PointView = PointView.ArgumentAndValues;

                        //Para visualizar melhor as Labels
                        graficoGeralJustificativa.Series["Series 1"].Label.TextOrientation = TextOrientation.TopToBottom;
                        graficoGeralJustificativa.Series["Series 1"].Label.TextAlignment = System.Drawing.StringAlignment.Center;

                        //graficoGeral.Series["Series 1"].Label.TextAlignment = System.Drawing.StringAlignment.Far;


                        //Para trocar a visão da legenda ...
                        graficoGeralJustificativa.Series["Series 1"].LegendPointOptions.PointView = PointView.ArgumentAndValues;

                        graficoGeralJustificativa.Series["Series 1"].ValueScaleType = ScaleType.Numerical;

                        //Título dos gráficos
                        //Dados extas ao gráfico no título
                        ChartTitle Titulo = new ChartTitle();
                        //ChartTitle Titulo2 = new ChartTitle();

                        Titulo.Font = new System.Drawing.Font("Tahoma", 12, System.Drawing.FontStyle.Bold);

                        //Titulo2.Font = new System.Drawing.Font("Tahoma", 9, System.Drawing.FontStyle.Bold);

                        Titulo.Text = string.Format("Justificativas Utilizadas.");

                        if (graficoGeralJustificativa.Titles.Count > 0)
                            graficoGeralJustificativa.Titles.Clear();

                        graficoGeralJustificativa.Titles.Add(Titulo);

                        for (int i = 0; i <= dsP.vwTotalMotivosFaltaSetor.Rows.Count - 1; i++)
                        {
                            graficoGeralJustificativa.Series["Series 1"].Points.Add(new SeriesPoint(dsP.vwTotalMotivosFaltaSetor[i].DSMotivoFalta, dsP.vwTotalMotivosFaltaSetor[i].TotalMotivoFalta));
                        }

                        graficoGeralJustificativa.DataBind();
                    }
                    break;
            }

            PReencheTop5NumeroFaltas(Visao);
            PReencheTop5Justificados(Visao);
            PReencheTop5Registro(Visao);
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    protected void PReencheTop5Registro(string visao)
    {
        graficoTop5Registro.Series.Remove(graficoTop5Registro.Series["Registro Efetuado"]);

        graficoTop5Registro.Series.Add("Registro Efetuado", ViewType.Bar);

        //XYDiagram diagrama = graficoGeral.Diagram as XYDiagram;

        //diagrama.AxisY.Visible = false;
        //diagrama.AxisX.Visible = false;      

        graficoTop5Registro.Legend.Visible = false;

        graficoTop5Registro.Legend.TextVisible = true;
        graficoTop5Registro.Legend.AlignmentVertical = LegendAlignmentVertical.Top;
        graficoTop5Registro.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;


        graficoTop5Registro.Series["Registro Efetuado"].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

        graficoTop5Registro.Series["Registro Efetuado"].Label.Border.Visible = true;
        graficoTop5Registro.Series["Registro Efetuado"].Label.LineVisible = true;

        graficoTop5Registro.Series["Registro Efetuado"].Label.TextAlignment = System.Drawing.StringAlignment.Center;

        //Para trocar a visão da legenda ...
        graficoTop5Registro.Series["Registro Efetuado"].LegendPointOptions.PointView = PointView.Argument;

        graficoTop5Registro.Series["Registro Efetuado"].ValueScaleType = ScaleType.Numerical;

        //Título dos gráficos
        //Dados extas ao gráfico no título
        ChartTitle Titulo = new ChartTitle();
        //ChartTitle Titulo2 = new ChartTitle();

        Titulo.Font = new System.Drawing.Font("Tahoma", 12, System.Drawing.FontStyle.Bold);

        //Titulo2.Font = new System.Drawing.Font("Tahoma", 9, System.Drawing.FontStyle.Bold);

        Titulo.Text = string.Format("Top 5 Índice de registros (%).");

        if (graficoTop5Registro.Titles.Count > 0)
            graficoTop5Registro.Titles.Clear();


        graficoTop5Registro.Titles.Add(Titulo);

        dsB.EnforceConstraints = false;

        graficoTop5Registro.Series["Registro Efetuado"].TopNOptions.Count = 5;

        graficoTop5Registro.Series["Registro Efetuado"].TopNOptions.Enabled = true;

        graficoTop5Registro.Series["Registro Efetuado"].TopNOptions.ShowOthers = false;

        graficoTop5Registro.Series["Registro Efetuado"].SeriesPointsSortingKey = SeriesPointKey.Value_1;

        graficoTop5Registro.Series["Registro Efetuado"].SeriesPointsSorting = SortingMode.Descending;

        switch (visao)
        {
            case "0":
                if (dsB.vwRegistrosDiaEmpresa.Rows.Count > 0)
                {
                    for (int i = 0; i <= dsB.vwRegistrosDiaEmpresa.Rows.Count - 1; i++)
                    {
                        graficoTop5Registro.Series["Registro Efetuado"].Points.Add(new SeriesPoint(dsB.vwRegistrosDiaEmpresa[i].SiglaEmpresa, dsB.vwRegistrosDiaEmpresa[i].QTDRegistro));
                    }
                }
                break;
            case "1":
                if (dsB.vwRegistrosDiaEmpresaSetor.Rows.Count > 0)
                {
                    for (int i = 0; i <= dsB.vwRegistrosDiaEmpresaSetor.Rows.Count - 1; i++)
                    {
                        graficoTop5Registro.Series["Registro Efetuado"].Points.Add(new SeriesPoint(dsB.vwRegistrosDiaEmpresaSetor[i].Sigla, dsB.vwRegistrosDiaEmpresaSetor[i].QTDRegistro));
                    }                    
                }
                break;
            case "2":
                if (dsB.vwRegistroDiaEmpresaSetorUsuario.Rows.Count > 0)
                {
                    for (int i = 0; i <= dsB.vwRegistroDiaEmpresaSetorUsuario.Rows.Count - 1; i++)
                    {
                        graficoTop5Registro.Series["Registro Efetuado"].Points.Add(new SeriesPoint(dsB.vwRegistroDiaEmpresaSetorUsuario[i].PrimeiroNome, dsB.vwRegistroDiaEmpresaSetorUsuario[i].QTDFrequencia));
                    }
                }

                if (dsB.vWHorasDia.Rows.Count > 0)
                {
                    for (int i = 0; i <= dsB.vWHorasDia.Rows.Count - 1; i++)
                    {
                        if(dsB.vWHorasDia[i].SituacaoN == 2)
                            graficoTop5Registro.Series["Registro Efetuado"].Points.Add(new SeriesPoint(dsB.vWHorasDia[i].PrimeiroNome,100));
                    }                    
                }

                break;
        }

    }

    protected void PReencheTop5Justificados(string visao)
    {
        graficoTop5Justificativa.Series.Remove(graficoTop5Justificativa.Series["Registro Efetuado"]);

        graficoTop5Justificativa.Series.Add("Registro Efetuado", ViewType.Bar);

        //XYDiagram diagrama = graficoGeral.Diagram as XYDiagram;

        //diagrama.AxisY.Visible = false;
        //diagrama.AxisX.Visible = false;      

        graficoTop5Justificativa.Legend.Visible = false;

        graficoTop5Justificativa.Legend.TextVisible = true;
        graficoTop5Justificativa.Legend.AlignmentVertical = LegendAlignmentVertical.Top;
        graficoTop5Justificativa.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;


        graficoTop5Justificativa.Series["Registro Efetuado"].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

        graficoTop5Justificativa.Series["Registro Efetuado"].Label.Border.Visible = true;
        graficoTop5Justificativa.Series["Registro Efetuado"].Label.LineVisible = true;

        graficoTop5Justificativa.Series["Registro Efetuado"].Label.TextAlignment = System.Drawing.StringAlignment.Center;

        //Para trocar a visão da legenda ...
        graficoTop5Justificativa.Series["Registro Efetuado"].LegendPointOptions.PointView = PointView.Argument;

        graficoTop5Justificativa.Series["Registro Efetuado"].ValueScaleType = ScaleType.Numerical;

        //Título dos gráficos
        //Dados extas ao gráfico no título
        ChartTitle Titulo = new ChartTitle();
        //ChartTitle Titulo2 = new ChartTitle();

        Titulo.Font = new System.Drawing.Font("Tahoma", 12, System.Drawing.FontStyle.Bold);

        //Titulo2.Font = new System.Drawing.Font("Tahoma", 9, System.Drawing.FontStyle.Bold);

        Titulo.Text = string.Format("Top 5 Índice de justificativas (%).");

        if (graficoTop5Justificativa.Titles.Count > 0)
            graficoTop5Justificativa.Titles.Clear();

        graficoTop5Justificativa.Titles.Add(Titulo);

        dsB.EnforceConstraints = false;

        graficoTop5Justificativa.Series["Registro Efetuado"].TopNOptions.Count = 5;

        graficoTop5Justificativa.Series["Registro Efetuado"].TopNOptions.Enabled = true;

        graficoTop5Justificativa.Series["Registro Efetuado"].TopNOptions.ShowOthers = false;

        graficoTop5Justificativa.Series["Registro Efetuado"].SeriesPointsSortingKey = SeriesPointKey.Value_1;

        graficoTop5Justificativa.Series["Registro Efetuado"].SeriesPointsSorting = SortingMode.Descending;

        switch (visao)
        {
            case "0":
                if (dsB.vwRegistrosDiaEmpresa.Rows.Count > 0)
                {
                    for (int i = 0; i <= dsB.vwRegistrosDiaEmpresa.Rows.Count - 1; i++)
                    {
                        graficoTop5Justificativa.Series["Registro Efetuado"].Points.Add(new SeriesPoint(dsB.vwRegistrosDiaEmpresa[i].SiglaEmpresa, dsB.vwRegistrosDiaEmpresa[i].QTDJustificado));
                    }
                }
                break;
            case "1":
                if (dsB.vwRegistrosDiaEmpresaSetor.Rows.Count > 0)
                {
                    for (int i = 0; i <= dsB.vwRegistrosDiaEmpresaSetor.Rows.Count - 1; i++)
                    {
                        graficoTop5Justificativa.Series["Registro Efetuado"].Points.Add(new SeriesPoint(dsB.vwRegistrosDiaEmpresaSetor[i].Sigla, dsB.vwRegistrosDiaEmpresaSetor[i].QTDJustificado));
                    }
                }
                break;
            case "2":
                if (dsB.vwRegistroDiaEmpresaSetorUsuario.Rows.Count > 0)
                {
                    for (int i = 0; i <= dsB.vwRegistroDiaEmpresaSetorUsuario.Rows.Count - 1; i++)
                    {
                        graficoTop5Justificativa.Series["Registro Efetuado"].Points.Add(new SeriesPoint(dsB.vwRegistroDiaEmpresaSetorUsuario[i].PrimeiroNome, dsB.vwRegistroDiaEmpresaSetorUsuario[i].QTDJustificado));
                    }
                }

                if (dsB.vWHorasDia.Rows.Count > 0)
                {
                    for (int i = 0; i <= dsB.vWHorasDia.Rows.Count - 1; i++)
                    {
                        if (dsB.vWHorasDia[i].SituacaoN == 1)
                            graficoTop5Justificativa.Series["Registro Efetuado"].Points.Add(new SeriesPoint(dsB.vWHorasDia[i].PrimeiroNome, 100));
                    }
                }
                break;
        }

    }

    protected void  PReencheTop5NumeroFaltas(string visao)
    {
        graficoTop5Ausencia.Series.Remove(graficoTop5Ausencia.Series["Registro Efetuado"]);
        graficoTop5Ausencia.Series.Add("Registro Efetuado", ViewType.Bar);

        //XYDiagram diagrama = graficoGeral.Diagram as XYDiagram;

        //diagrama.AxisY.Visible = false;
        //diagrama.AxisX.Visible = false;      

        graficoTop5Ausencia.Legend.Visible = false;

        graficoTop5Ausencia.Legend.TextVisible = true;
        graficoTop5Ausencia.Legend.AlignmentVertical = LegendAlignmentVertical.Top;
        graficoTop5Ausencia.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;


        graficoTop5Ausencia.Series["Registro Efetuado"].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

        graficoTop5Ausencia.Series["Registro Efetuado"].Label.Border.Visible = true;
        graficoTop5Ausencia.Series["Registro Efetuado"].Label.LineVisible = true;

        graficoTop5Ausencia.Series["Registro Efetuado"].Label.TextAlignment = System.Drawing.StringAlignment.Center;

        //Para trocar a visão da legenda ...
        graficoTop5Ausencia.Series["Registro Efetuado"].LegendPointOptions.PointView = PointView.Argument;

        graficoTop5Ausencia.Series["Registro Efetuado"].ValueScaleType = ScaleType.Numerical;

        //Título dos gráficos
        //Dados extas ao gráfico no título
        ChartTitle Titulo = new ChartTitle();
        //ChartTitle Titulo2 = new ChartTitle();

        Titulo.Font = new System.Drawing.Font("Tahoma", 12, System.Drawing.FontStyle.Bold);

        //Titulo2.Font = new System.Drawing.Font("Tahoma", 9, System.Drawing.FontStyle.Bold);

        Titulo.Text = string.Format("Top 5 Índice de ausências (%).");

        if (graficoTop5Ausencia.Titles.Count > 0)
            graficoTop5Ausencia.Titles.Clear();

        graficoTop5Ausencia.Titles.Add(Titulo);

        //Preenchendo dados
        dsB.EnforceConstraints = false;

        graficoTop5Ausencia.Series["Registro Efetuado"].TopNOptions.Count = 5;

        graficoTop5Ausencia.Series["Registro Efetuado"].TopNOptions.Enabled = true;

        graficoTop5Ausencia.Series["Registro Efetuado"].TopNOptions.ShowOthers = false;

        graficoTop5Ausencia.Series["Registro Efetuado"].SeriesPointsSortingKey = SeriesPointKey.Value_1;

        graficoTop5Ausencia.Series["Registro Efetuado"].SeriesPointsSorting = SortingMode.Descending;

        switch(visao)
        {
            case"0":
                if (dsB.vwRegistrosDiaEmpresa.Rows.Count > 0)
                {

                    for (int i = 0; i <= dsB.vwRegistrosDiaEmpresa.Rows.Count - 1; i++)
                    {
                        graficoTop5Ausencia.Series["Registro Efetuado"].Points.Add(new SeriesPoint(dsB.vwRegistrosDiaEmpresa[i].SiglaEmpresa, dsB.vwRegistrosDiaEmpresa[i].QTDAusencia));
                    }
                }
                break;
            case"1":
                if (dsB.vwRegistrosDiaEmpresaSetor.Rows.Count > 0)
                {
                    for (int i = 0; i <= dsB.vwRegistrosDiaEmpresaSetor.Rows.Count - 1; i++)
                    {
                        graficoTop5Ausencia.Series["Registro Efetuado"].Points.Add(new SeriesPoint(dsB.vwRegistrosDiaEmpresaSetor[i].Sigla, dsB.vwRegistrosDiaEmpresaSetor[i].QTDAusencia));
                    }
                }
                break;
            case"2":
                if (dsB.vwRegistroDiaEmpresaSetorUsuario.Rows.Count > 0)
                {
                    for (int i = 0; i <= dsB.vwRegistroDiaEmpresaSetorUsuario.Rows.Count - 1; i++)
                    {
                        graficoTop5Ausencia.Series["Registro Efetuado"].Points.Add(new SeriesPoint(dsB.vwRegistroDiaEmpresaSetorUsuario[i].PrimeiroNome, dsB.vwRegistroDiaEmpresaSetorUsuario[i].QTDAusencia));
                    }
                }

                if (dsB.vWHorasDia.Rows.Count > 0)
                {
                    for (int i = 0; i <= dsB.vWHorasDia.Rows.Count - 1; i++)
                    {
                        if (dsB.vWHorasDia[i].SituacaoN == 3)
                            graficoTop5Ausencia.Series["Registro Efetuado"].Points.Add(new SeriesPoint(dsB.vWHorasDia[i].PrimeiroNome, 100));
                    }
                }
                break;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated == true)
        {
            if (!IsPostBack)
            {
                coEmpresaSetor.Add("tpuser", Session["TPUsuario"].ToString());
                PreenchecbEmpresa();

                if (Session["TPUsuario"].ToString() != "1")
                {
                    PreenchecbSetor(Convert.ToInt32(Session["IDEmpresa"]));
                }

                deDTReferencia.Date = System.DateTime.Now.Date.Date;
                if (Session["TPUsuario"].ToString() == "1")
                {
                    cbGrupoGeral.Checked = true;
                    coEmpresaSetor.Add("TPUsuario", "1");
                    PreencheGraficosPresencaJustificativa(rbVisao.Value.ToString(), "0", "IndicePresenca");
                    PreencheGraficosPresencaJustificativa(rbVisao.Value.ToString(), "0", "IndiceJustificativa");
                }
                else if (Session["TPUsuario"].ToString() == "7" || Session["TPUsuario"].ToString() == "8")
                {
                    PreencheGraficosPresencaJustificativa(rbVisao.Value.ToString(), "1", "IndicePresenca");
                    PreencheGraficosPresencaJustificativa(rbVisao.Value.ToString(), "1", "IndiceJustificativa");
                    //PreencheGridRegistro(Convert.ToInt32(Session["IDEmpresa"]));
                }
                else
                {
                    PreencheGraficosPresencaJustificativa(rbVisao.Value.ToString(), "2", "IndicePresenca");
                    PreencheGraficosPresencaJustificativa(rbVisao.Value.ToString(), "2", "IndiceJustificativa");
                    //gridRegistro.Visible = false;
                }
            }
        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }
    }
    protected void cbGeralEmpresa_Callback(object sender, CallbackEventArgsBase e)
    {
        PreencheGraficosPresencaJustificativa(rbVisao.Value.ToString(), e.Parameter,"IndicePresenca");
        PreencheGraficosPresencaJustificativa(rbVisao.Value.ToString(), e.Parameter, "IndiceJustificativa");
    }
    protected void cpVisaoComponente_Callback(object sender, CallbackEventArgsBase e)
    {
        PreenchecbMesAnoRef(Convert.ToInt32(Session["IDEmpresa"].ToString()), rbVisao.Value.ToString());
    }
    protected void cbSetorPainel_Callback(object sender, CallbackEventArgsBase e)
    {
        PreenchecbSetor(Convert.ToInt32(e.Parameter));
    }
    protected void btAbrepopPeriodo1_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Default.aspx");
    }
    protected void gridRegistro_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        
        switch(coEmpresaSetor["tpuser"].ToString())
        {
            case"1":
                //PreencheGridRegistro(Convert.ToInt32(cbEmpresaPainel.Value));
                break;
            case"7":
                //PreencheGridRegistro(Convert.ToInt32(Session["IDEmpresa"]));
                break;
            case"3":
                gridRegistro.Visible = false;
                break;
            case"9":
                gridRegistro.Visible = false;
                break;
        }
    }
}