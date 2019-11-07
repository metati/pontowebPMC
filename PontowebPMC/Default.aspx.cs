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


public partial class _Default : System.Web.UI.Page
{
    DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
    MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwSituacaoRegistroUsuarioMesAnoTableAdapter adpSituacaoUsuario = new MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwSituacaoRegistroUsuarioMesAnoTableAdapter();
    MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwSituacaoRegistroEmpresaSetorTableAdapter adpRegistroSituacao = new MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwSituacaoRegistroEmpresaSetorTableAdapter();
    
    Frequencia Freq = new Frequencia();
    PreencheTabela PT = new PreencheTabela();

    int IDSetor, QTDAusente;

    string textoJustificado = "JUSTIFICADO(S):<br>";
    string texto = "EFETUOU REGISTRO:<br>";
    string textoAusencia = "NÃO EFETUOU REGISTRO:<br>";
    int QTDRegistro, QTDJustificado;

    protected void PreencheddlSetor()
    {
        if (Convert.ToInt32(Session["TPUsuario"]) != 2 || Convert.ToInt32(Session["TPUsuario"]) == 4)
        {
            if (Convert.ToInt32(Session["TPUsuario"]) == 3)
            {
                //PT.PreencheTBSetor(ds);

                //PT.PreencheVWGestorSetor(ds, Convert.ToInt32(Session["IDEmpresa"]), Convert.ToInt32(Session["IDUsuario"]));
                //PT.PreencheTBSetorIDSetor(ds, Convert.ToInt32(Session["IDSETOR"]), Convert.ToInt32(Session["IDEmpresa"]));
                PT.PreencheTBSetorGestor(ds, Convert.ToInt32(Session["IDUsuario"]), Convert.ToInt32(Session["IDEmpresa"]));
                
                //cbSetor.DataSource = ds.vwGestorSetor;
            }
            else if (Convert.ToInt32(Session["TPUsuario"]) == 1 || Convert.ToInt32(Session["TPUsuario"]) == 8 || Convert.ToInt32(Session["TPUsuario"]) == 9)
            {
                PT.PreencheTBSetorIDEmpresa(ds, Convert.ToInt32(Session["IDEmpresa"]));

                //cbSetor.DataSource = ds.TBSetor;
            }
        }
        else
        {
            cbSetor.Visible = false;
        }

        cbSetor.DataSource = ds;
        cbSetor.IncrementalFilteringMode = DevExpress.Web.ASPxEditors.IncrementalFilteringMode.Contains;
        cbSetor.DataBind();

        

        //Convert.ToString(Session["TPUsuario"]) == "1" ||  -- Retirei do IF Abaixo.
        //RETIREI DIA 07/06/2016 - PARA NÃO SELECIONAR AUTOMATICAMENTE O SETOR.
        //if (((Convert.ToString(Session["TPUsuario"]) == "3" || Convert.ToString(Session["TPUsuario"]) == "9") && cbSetor.Items.Count == 1))
        //{
            //cbSetor.Value = Session["IDSETOR"];
            //IDSetor = Convert.ToInt32(Session["IDSETOR"]);
        //}
    }

    protected void Preenchegrafico(int IDTPUsuario, int IDEmpresa, DateTime DTFrequencia)
    {
        
        //if (DTFrequencia.Length == 9)
        //{
            //if (DTFrequencia.Substring(0, 2).IndexOf("/") == 1)
                //DTFrequencia = string.Format("0{0}", DTFrequencia);
            //else
            //{
                //DTFrequencia = string.Format("{0}0{1}", DTFrequencia.Substring(0, 3), DTFrequencia.Substring(3, 6));
            //}
        //}
        //else if (DTFrequencia.Length == 8)
        //{
            //DTFrequencia = string.Format("0{0}", DTFrequencia);
            //DTFrequencia = string.Format("{0}0{1}", DTFrequencia.Substring(0, 3), DTFrequencia.Substring(3, 6));
        //}

        if (IDTPUsuario == 1 || IDTPUsuario == 3 || IDTPUsuario == 2 || IDTPUsuario == 7 || IDTPUsuario == 9 || IDTPUsuario == 8)
        {
            //Situação
            graficoFaltaSetor.Series.Remove(graficoFaltaSetor.Series["Registro Efetuado"]);

            graficoFaltaSetor.Series.Add("Registro Efetuado", ViewType.Bar);

            graficoFaltaSetor.Series.Remove(graficoFaltaSetor.Series["Registro Ausente"]);

            graficoFaltaSetor.Series.Add("Registro Ausente", ViewType.Bar);

            graficoFaltaSetor.Series.Remove(graficoFaltaSetor.Series["Registro Justificado"]);

            graficoFaltaSetor.Series.Add("Registro Justificado", ViewType.Bar);

            //Tentativa tirar DIAGRAMA - Deu certo
            XYDiagram diagrama = graficoFaltaSetor.Diagram as XYDiagram;

            diagrama.AxisY.Visible = false;
            diagrama.AxisX.Visible = false;
            
            graficoFaltaSetor.Series["Registro Efetuado"].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

            graficoFaltaSetor.Series["Registro Efetuado"].LegendPointOptions.ValueNumericOptions.Format = NumericFormat.General;

            graficoFaltaSetor.Series["Registro Efetuado"].ValueScaleType = ScaleType.Numerical;

            graficoFaltaSetor.Series["Registro Ausente"].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

            graficoFaltaSetor.Series["Registro Ausente"].LegendPointOptions.ValueNumericOptions.Format = NumericFormat.General;

            graficoFaltaSetor.Series["Registro Ausente"].ValueScaleType = ScaleType.Numerical;


            graficoFaltaSetor.Series["Registro Justificado"].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

            graficoFaltaSetor.Series["Registro Justificado"].LegendPointOptions.ValueNumericOptions.Format = NumericFormat.General;

            graficoFaltaSetor.Series["Registro Justificado"].ValueScaleType = ScaleType.Numerical;

            //Setores
            //graficoFaltaSetor.Series.Remove(graficoFaltaSetor.Series["Setor"]);
            //graficoFaltaSetor.Series.Add("Setor", ViewType.FullStackedBar);
            //graficoFaltaSetor.Series["Setor"].LabelsVisibility = DevExpress.Utils.DefaultBoolean.False; //Não precisa aparecer no gráfico

            //graficoFaltaSetor.Series["Setor"].LegendPointOptions.ValueNumericOptions.Format = NumericFormat.General;

            //((FullStackedBarPointOptions)graficoFaltaSetor.Series["Setor"].Label.PointOptions).PercentOptions.ValueAsPercent = false;
        }
        if (IDTPUsuario == 1 || IDTPUsuario == 3 || IDTPUsuario == 7 || IDTPUsuario == 9|| IDTPUsuario == 8)
        {
            IDSetor = Convert.ToInt32(cbSetor.Value);
            //lbSetorSecretaria.Text = (string)Session["DSEMPRESA"];//"Secretaria de Estado de Administração - Situação do dia corrente.";
            
            //adpHoraDiaSitu.FillIDEmpresa(ds.vwHorasDiaSituacao, IDEmpresa, DTFrequencia);

            ds.EnforceConstraints = false;

            //03/07/2018 -- Mudando a Rotina aqui. Para por equanto usando a horas dia.
            //adpRegistroSituacao.FillIDSetorDTFrequencia(ds.vwSituacaoRegistroEmpresaSetor, DTFrequencia.ToShortDateString(), IDSetor);
            MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vWHorasDiaTableAdapter adpHorasDia =
                new MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vWHorasDiaTableAdapter();

            adpHorasDia.FillFreqDia(ds.vWHorasDia,IDEmpresa, Convert.ToInt32(cbSetor.Value),
                Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")), Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd")));

            if (ds.vWHorasDia.Rows.Count > 0)
            {
                for (int i= 0; i<= ds.vWHorasDia.Rows.Count-1; i++)
                {
                switch(ds.vWHorasDia[i].SituacaoN)
                {
                    case 1:
                        QTDJustificado++;
                        textoJustificado += string.Format("{0}<br>", ds.vWHorasDia[i].DSUsuario);
                        break;
                    case 2:
                        QTDRegistro++;
                        texto += string.Format("{0}<br>", ds.vWHorasDia[i].DSUsuario);
                        break;
                    case 3:
                        if(ds.vWHorasDia[i].Situacao != "Sábado" && ds.vWHorasDia[i].Situacao != "Domingo")
                        {
                            QTDAusente++;
                            textoAusencia += string.Format("{0}<br>", ds.vWHorasDia[i].DSUsuario);
                        }
                        break;
                    case 4:
                        break;
                }
            }

                //lbSetorSecretaria.Text = (string)Session["DSEMPRESA"] + "-Situação do dia corrente.";
                graficoFaltaSetor.Series["Registro Efetuado"].Points.Add(new SeriesPoint(cbSetor.Text.Trim(), QTDRegistro));
                graficoFaltaSetor.Series["Registro Efetuado"].ToolTipEnabled = DevExpress.Utils.DefaultBoolean.True;

                ////Monta Texto
                //MontaTextoToolTip mt = new MontaTextoToolTip(2);
                //mt.MontaTexto(IDSetor, DTFrequencia, 2, ds);

                graficoFaltaSetor.Series["Registro Efetuado"].ToolTipPointPattern = texto;

                graficoFaltaSetor.Series["Registro Justificado"].Points.Add(new SeriesPoint(cbSetor.Text.Trim(), QTDJustificado));
                graficoFaltaSetor.Series["Registro Justificado"].ToolTipEnabled = DevExpress.Utils.DefaultBoolean.True;

                graficoFaltaSetor.Series["Registro Justificado"].ToolTipPointPattern = textoJustificado;

                //if (IDEmpresa == 9 || IDEmpresa == 18 || IDEmpresa == 19)
                //{
                //    MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwUsuariogridTableAdapter adpUsuario = new MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwUsuariogridTableAdapter();
                //    adpUsuario.FillTotalSemRegistro(ds.vwUsuariogrid, IDEmpresa, IDSetor);
                //    QTDAusente = ds.vwSituacaoRegistroEmpresaSetor[0].QTDAUSENTE - ds.vwUsuariogrid[0].IDSetor;
                //}
                //else
                //    QTDAusente = ds.vwSituacaoRegistroEmpresaSetor[0].QTDAUSENTE;

                //Quando for plantonista, não contar como falta.
                graficoFaltaSetor.Series["Registro Ausente"].Points.Add(new SeriesPoint(cbSetor.Text.Trim(), QTDAusente));
                graficoFaltaSetor.Series["Registro Ausente"].ToolTipEnabled = DevExpress.Utils.DefaultBoolean.True;

                graficoFaltaSetor.Series["Registro Ausente"].ToolTipPointPattern = textoAusencia;
            }
            
            graficoFaltaSetor.DataBind();

            texto = string.Empty;
            textoAusencia = string.Empty;
            textoJustificado = string.Empty;
            QTDRegistro = 0;
            QTDAusente = 0;
            QTDJustificado = 0;
        }
        else if (IDTPUsuario == 232) // Alterei de 3 p 232 só p testar a questão do stackoverFlow
        {
            if(cbSetor.Text == "")
                IDSetor = Convert.ToInt32(Session["IDSETOR"]);
            else
                IDSetor = Convert.ToInt32(cbSetor.Value);
            
            ds.EnforceConstraints = false;
            //adpHoraDiaSitu.FillIDEmpresaSetor(ds.vwHorasDiaSituacao, IDEmpresa, DTFrequencia, IDSetor);

            if (ds.vwHorasDiaSituacao.Rows.Count > 0)
            {
                //lbSetorSecretaria.Text = (string)Session["DSEMPRESA"];

                for (int i = 0; i <= ds.vwHorasDiaSituacao.Rows.Count - 1; i++)
                {
                    if (ds.vwHorasDiaSituacao[i].Situacao == "Bateu")
                    {
                        graficoFaltaSetor.Series["Registro Efetuado"].Points.Add(new SeriesPoint(ds.vwHorasDiaSituacao[i].DSSetor, ds.vwHorasDiaSituacao[i].totSitu));

                        MontaTextoToolTip mt = new MontaTextoToolTip(2);
                        graficoFaltaSetor.Series["Registro Efetuado"].ToolTipEnabled = DevExpress.Utils.DefaultBoolean.True;
                        //graficoFaltaSetor.Series["Registro Efetuado"].ToolTipPointPattern = mt.MontaTexto(IDSetor, DTFrequencia, 2,ds);

                    }
                    else if (ds.vwHorasDiaSituacao[i].Situacao == "Não Bateu")
                    {
                        graficoFaltaSetor.Series["Ausência de registro"].Points.Add(new SeriesPoint(ds.vwHorasDiaSituacao[i].DSSetor, ds.vwHorasDiaSituacao[i].totSitu));
                        
                        MontaTextoToolTip mt = new MontaTextoToolTip(3);
                        graficoFaltaSetor.Series["Ausência de registro"].ToolTipEnabled = DevExpress.Utils.DefaultBoolean.True;
                        //graficoFaltaSetor.Series["Ausência de registro"].ToolTipPointPattern = mt.MontaTexto(IDSetor, DTFrequencia, 3,ds);
                    }
                    else if (ds.vwHorasDiaSituacao[i].Situacao == "Just")
                    {
                        graficoFaltaSetor.Series["Justificado"].Points.Add(new SeriesPoint(ds.vwHorasDiaSituacao[i].DSSetor, ds.vwHorasDiaSituacao[i].totSitu));

                        MontaTextoToolTip mt = new MontaTextoToolTip(1);
                        graficoFaltaSetor.Series["Justificado"].ToolTipEnabled = DevExpress.Utils.DefaultBoolean.True;
                        //graficoFaltaSetor.Series["Justificado"].ToolTipPointPattern = mt.MontaTexto(IDSetor, DTFrequencia, 1, ds);
                        
                    }
                }
            }
        }
        else if (IDTPUsuario == 2)
        {            
            celula.InnerText = string.Empty;
            //lbSetorSecretaria.Visible = false;
            //graficoFaltaSetor.Visible = false;
            
            //Aqui usuário comum
            //lbSetorSecretaria.Text = "Situação do mês corrente.";

            ds.EnforceConstraints = false;

            //adpSituacaoUsuario.Connection.Open();
            //adpSituacaoUsuario.FillMesAnoIDVinculo(ds.vwSituacaoRegistroUsuarioMesAno,DTFrequencia.Month,DTFrequencia.Year,Convert.ToInt32(Session["IDVinculoUsuarioFinal"]));
            //adpSituacaoUsuario.Connection.Close();

            //Mudança na Rotina. 04/07/2018 fill na dias ano e direto na TBFrequencia();

            //AQui, pega as frequencias realizadas até o momento.
            PT.GetFrequenciaMesAnoVinculo(ds, DateTime.Now.Month, DateTime.Now.Year, Convert.ToInt32(Session["IDVinculoUsuarioFinal"]));
            //Tabela agora dos dias do ano.
            if (ds.TBDiasAno.Rows.Count > 0)
            {
                //ROTINA MONTADA APENAS PARA USUÁRIOS EM REGIME DE EXPEDIENTE. ADIANTE, PENSAR NO PLANTONISTA.
                //esse For para Faltas
                for (int i = 0; i<= ds.TBDiasAno.Rows.Count -1; i++)
                {
                    if(ds.TBDiasAno[i].DTDiasAno.DayOfWeek.ToString() != "Saturday" && ds.TBDiasAno[i].DTDiasAno.DayOfWeek.ToString() != "Sunday" && !ds.TBDiasAno[i].FeriadoPontoFacultativo)
                    {
                        QTDAusente++;
                        textoAusencia += string.Format("{0}<br>", ds.TBDiasAno[i].DTDiasAno.ToShortDateString());
                    }
                }
                // Esse for para Presença/Justificativa.
                for (int j = 0; j <= ds.TBFrequencia.Rows.Count - 1; j++)
                {
                    if (ds.TBFrequencia[j].IsIDMotivoFaltaNull())
                    {
                        QTDRegistro++;
                        texto += string.Format("{0}<br>", ds.TBFrequencia[j].DTFrequencia.ToShortDateString());
                    }
                    else
                    {
                        QTDJustificado++;
                        textoJustificado += string.Format("{0}<br>", ds.TBFrequencia[j].DTFrequencia.ToShortDateString());                    }
                }

                //MontaTextoToolTip mt = new MontaTextoToolTip(2);

                //mt.MontaTextoUsuarioComum(Convert.ToInt32(Session["IDUsuario"]), Convert.ToInt32(Session["IDVinculoUsuarioFinal"]), DateTime.Now.Month, DateTime.Now.Year, "Just", ds, Convert.ToInt32(Session["IDEmpresa"])); 

                graficoFaltaSetor.Series["Registro Efetuado"].Points.Add(new SeriesPoint(DateTime.Now.Month, QTDRegistro));
                graficoFaltaSetor.Series["Registro Efetuado"].ToolTipEnabled = DevExpress.Utils.DefaultBoolean.True;
                graficoFaltaSetor.Series["Registro Efetuado"].ToolTipPointPattern = texto;

                graficoFaltaSetor.Series["Registro Ausente"].Points.Add(new SeriesPoint(DateTime.Now.Month, QTDAusente));       
                graficoFaltaSetor.Series["Registro Ausente"].ToolTipEnabled = DevExpress.Utils.DefaultBoolean.True;
                graficoFaltaSetor.Series["Registro Ausente"].ToolTipPointPattern = textoAusencia;

                graficoFaltaSetor.Series["Registro Justificado"].Points.Add(new SeriesPoint(DateTime.Now.Month, QTDJustificado));      
                graficoFaltaSetor.Series["Registro Justificado"].ToolTipEnabled = DevExpress.Utils.DefaultBoolean.True;
                graficoFaltaSetor.Series["Registro Justificado"].ToolTipPointPattern = textoJustificado;

                graficoFaltaSetor.DataBind();
                texto = string.Empty;
                textoAusencia = string.Empty;
                textoJustificado = string.Empty;
                QTDRegistro = 0;
                QTDAusente = 0;
                QTDJustificado = 0;

            }
        }
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated == false)
        {
            Response.Redirect("~/Account/Login.aspx");
        }
        else
        {
            
            if (Convert.ToInt32(Session["PrimeiroAcesso"]) == 1)
            {
                Response.Redirect("~/Account/frmMudarSenha.aspx");
            }

            //Aqui - Se o número de vínculos for maior que 1, definir o vinculo de operação.

            if (Convert.ToInt32(Session["QTDVinculo"]) > 1 && (bool)Session["VinculoSelecionado"] == false)
            {
                Response.Redirect("~/frmVinculo.aspx");
                //Redirecionar para a página de vínculos de tratamento de vínculos.
            }


            if (Convert.ToInt32(Session["TPUsuario"]) == 1 && ((string)Session["TrocaSession"]) == "0")
            {
                Response.Redirect("~/Defaultsession.aspx");
            }

            if (!IsPostBack)
            {
                //UsuarioAdm(Convert.ToInt32(Session["T PUsuario"]));
                if((Convert.ToString(Session["TPUsuario"]) == "2"))
                {
                    //03/07/2018
                    PreencheTabela pt = new PreencheTabela();
                    pt.GetDiasMesCorrente(ds, Convert.ToInt32(Session["IDEmpresa"]), DateTime.Now.Month, DateTime.Now.Year, Convert.ToInt32(Session["IDVinculoUsuarioFinal"]));
                    Session["DatasetGrafico"] = ds;
                    cbSetor.Visible = false;
                    cbSetor.DataBind();
                    //HorasMes();
                }
                if ((Convert.ToString(Session["TPUsuario"]) == "1" || Convert.ToString(Session["TPUsuario"]) == "7" || Convert.ToString(Session["TPUsuario"]) == "8" || ((Convert.ToString(Session["TPUsuario"]) == "3" || Convert.ToString(Session["TPUsuario"]) == "9"))))
                {
                    PreencheddlSetor();
                }

                Preenchegrafico(Convert.ToInt32(Session["TPUsuario"]), Convert.ToInt32(Session["IDEmpresa"]), System.DateTime.Now.Date.Date);
            }
        }
        //lbNome.Text = (string)Session["DSUsuario"];
    }

    protected void UsuarioAdm(int idtpsuario)
    {
        if (idtpsuario == 1)
        {
            PreencheComboEmpresa();
            this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> AbrePop();</script>");
        }
        else
            PreencheddlSetor();
    }
    protected void HorasMes()
    {
        //Freq.TotalHorasMes(Convert.ToInt32(Session["IDUsuario"]), System.DateTime.Now.Month, System.DateTime.Now.Year, Convert.ToInt32(Session["IDEmpresa"]));
        //string TothorasMes = Freq.TOTHORASMES;
        //string TohorasUsuario = Freq.TOTHORASUSUARIO;

        //lbMensagemHoras.Text = ", você completou " + TohorasUsuario + " horas de " + TothorasMes + " disponíveis no mês corrente.";
        //lbMensagemHoras.DataBind();
    }

    protected void cbEmpresa_Callback(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        Session["IDEmpresa"] = e.Parameter.ToString();
    }
    protected void PreencheComboEmpresa()
    {
        PT.PreencheTBEmpresaAdmin(ds);
        cbEmpresa.DataSource = ds;
        cbEmpresa.DataBind();
    }
    protected void ASPxCallbackPanel1_Callback(object sender, CallbackEventArgsBase e)
    {
        if (HttpContext.Current.User.Identity.IsAuthenticated == false)
        {
            Response.Redirect("~/Account/Login.aspx");
        }
        Preenchegrafico(Convert.ToInt32(Session["TPUsuario"]), Convert.ToInt32(Session["IDEmpresa"]), System.DateTime.Now.Date.Date);
    }
    protected void cbEmpresa_SelectedIndexChanged(object sender, EventArgs e)
    {
        PreencheddlSetor();
    }
}
