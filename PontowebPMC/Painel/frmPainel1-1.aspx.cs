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

public partial class Painel_frmPainel1_1 : System.Web.UI.Page
{
    //Variáveis
    DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
    
    MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwHorasDiaSituacaoTableAdapter adpHoraDiaSitu = new MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwHorasDiaSituacaoTableAdapter();

    Frequencia Freq = new Frequencia();
    PreencheTabela PT = new PreencheTabela();

    protected void PreencherbSetor()
    {
        ds.EnforceConstraints = false;
        MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.TBSetorTableAdapter adpSetor = new MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.TBSetorTableAdapter();
        adpSetor.FillByIDEmpresa(ds.TBSetor,Convert.ToInt32(Session["IDEmpresa"]));
        //rbSetor.DataSource = ds.TBSetor;
        //rbSetor.DataBind();
    }

    protected void PreencheGridRegistro()
    {
        ds.EnforceConstraints = false;
        MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwLogRegistroSetorTableAdapter adpLog = new MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwLogRegistroSetorTableAdapter();
        try
        {
            adpLog.FillTodos(ds.vwLogRegistroSetor, Convert.ToInt32(Session["IDEmpresa"]));


            gridRegistro.DataSource = ds;
            gridRegistro.DataBind();
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

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

    protected void PReencheTop5NumeroFaltas()
    {
        graficoTop5.Series.Remove(graficoTop5.Series["Registro Efetuado"]);

        graficoTop5.Series.Add("Registro Efetuado", ViewType.Bar);

        //XYDiagram diagrama = graficoGeral.Diagram as XYDiagram;

        //diagrama.AxisY.Visible = false;
        //diagrama.AxisX.Visible = false;      
        
        graficoTop5.Legend.Visible = false;

        graficoTop5.Legend.TextVisible = true;
        graficoTop5.Legend.AlignmentVertical = LegendAlignmentVertical.Top;
        graficoTop5.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;


        graficoTop5.Series["Registro Efetuado"].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

        graficoTop5.Series["Registro Efetuado"].Label.Border.Visible = true;
        graficoTop5.Series["Registro Efetuado"].Label.LineVisible = true;

        graficoTop5.Series["Registro Efetuado"].Label.TextAlignment = System.Drawing.StringAlignment.Center;

        //Para trocar a visão da legenda ...
        graficoTop5.Series["Registro Efetuado"].LegendPointOptions.PointView = PointView.Argument;

        graficoTop5.Series["Registro Efetuado"].ValueScaleType = ScaleType.Numerical;

        //Título dos gráficos
        //Dados extas ao gráfico no título
        ChartTitle Titulo = new ChartTitle();
        //ChartTitle Titulo2 = new ChartTitle();

        Titulo.Font = new System.Drawing.Font("Tahoma", 12, System.Drawing.FontStyle.Bold);

        //Titulo2.Font = new System.Drawing.Font("Tahoma", 9, System.Drawing.FontStyle.Bold);

        Titulo.Text = string.Format("Top 5 Maior índice de faltas (%).");

        if (graficoTop5.Titles.Count > 0)
            graficoTop5.Titles.Clear();
        
        graficoTop5.Titles.Add(Titulo);


        //Preenchendo dados
        ds.EnforceConstraints = false;
        MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwSituacaoGeralSetorTableAdapter adpSitu = new MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwSituacaoGeralSetorTableAdapter();

        adpSitu.FillDiaEmpresaAusencia(ds.vwSituacaoGeralSetor,Convert.ToInt32(Session["IDEmpresa"]),System.DateTime.Now.Date.Date);

        if (ds.vwSituacaoGeralSetor.Rows.Count > 0)
        {
            
            for (int i = 0; i <= ds.vwSituacaoGeralSetor.Rows.Count - 1; i++)
            {
                graficoTop5.Series["Registro Efetuado"].Points.Add(new SeriesPoint(ds.vwSituacaoGeralSetor[i].Sigla,ds.vwSituacaoGeralSetor[i].Total));
            }
        }
    }

    protected void PReencheTop5Justificados()
    {
        graficoTop5Justificado.Series.Remove(graficoTop5Justificado.Series["Registro Efetuado"]);

        graficoTop5Justificado.Series.Add("Registro Efetuado", ViewType.Bar);

        //XYDiagram diagrama = graficoGeral.Diagram as XYDiagram;

        //diagrama.AxisY.Visible = false;
        //diagrama.AxisX.Visible = false;      

        graficoTop5Justificado.Legend.Visible = false;

        graficoTop5Justificado.Legend.TextVisible = true;
        graficoTop5Justificado.Legend.AlignmentVertical = LegendAlignmentVertical.Top;
        graficoTop5Justificado.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;

        graficoTop5Justificado.Series["Registro Efetuado"].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

        graficoTop5Justificado.Series["Registro Efetuado"].Label.Border.Visible = true;
        graficoTop5Justificado.Series["Registro Efetuado"].Label.LineVisible = true;

        graficoTop5Justificado.Series["Registro Efetuado"].Label.TextAlignment = System.Drawing.StringAlignment.Center;

        //Para trocar a visão da legenda ...
        graficoTop5Justificado.Series["Registro Efetuado"].LegendPointOptions.PointView = PointView.Argument;

        graficoTop5Justificado.Series["Registro Efetuado"].ValueScaleType = ScaleType.Numerical;

        //Título dos gráficos
        //Dados extas ao gráfico no título
        ChartTitle Titulo = new ChartTitle();
        //ChartTitle Titulo2 = new ChartTitle();

        Titulo.Font = new System.Drawing.Font("Tahoma", 12, System.Drawing.FontStyle.Bold);

        //Titulo2.Font = new System.Drawing.Font("Tahoma", 9, System.Drawing.FontStyle.Bold);

        Titulo.Text = string.Format("Top 5 Maior índice de justificativas (%).");

        if (graficoTop5Justificado.Titles.Count > 0)
            graficoTop5Justificado.Titles.Clear();
        
        
        graficoTop5Justificado.Titles.Add(Titulo);

        //Preenchendo dados
        MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwSituacaoGeralSetorTableAdapter adpSitu = new MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwSituacaoGeralSetorTableAdapter();
        ds.EnforceConstraints = false;
        adpSitu.FillDiaEmpresaJustificado(ds.vwSituacaoGeralSetor, Convert.ToInt32(Session["IDEmpresa"]),System.DateTime.Now.Date.Date);

        if (ds.vwSituacaoGeralSetor.Rows.Count > 0)
        {
            for (int i = 0; i <= ds.vwSituacaoGeralSetor.Rows.Count - 1; i++)
            {
                graficoTop5Justificado.Series["Registro Efetuado"].Points.Add(new SeriesPoint(ds.vwSituacaoGeralSetor[i].Sigla, ds.vwSituacaoGeralSetor[i].Total));
            }
        }
    }

    protected void PReencheTop5Batidos()
    {
        graficoTop5Batido.Series.Remove(graficoTop5Batido.Series["Registro Efetuado"]);

        graficoTop5Batido.Series.Add("Registro Efetuado", ViewType.Bar);

        //XYDiagram diagrama = graficoGeral.Diagram as XYDiagram;

        //diagrama.AxisY.Visible = false;
        //diagrama.AxisX.Visible = false;      

        graficoTop5Batido.Legend.Visible = false;

        graficoTop5Batido.Legend.TextVisible = true;
        graficoTop5Batido.Legend.AlignmentVertical = LegendAlignmentVertical.Top;
        graficoTop5Batido.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center;


        graficoTop5Batido.Series["Registro Efetuado"].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

        graficoTop5Batido.Series["Registro Efetuado"].Label.Border.Visible = true;
        graficoTop5Batido.Series["Registro Efetuado"].Label.LineVisible = true;

        graficoTop5Batido.Series["Registro Efetuado"].Label.TextAlignment = System.Drawing.StringAlignment.Center;

        //Para trocar a visão da legenda ...
        graficoTop5Batido.Series["Registro Efetuado"].LegendPointOptions.PointView = PointView.Argument;

        graficoTop5Batido.Series["Registro Efetuado"].ValueScaleType = ScaleType.Numerical;

        //Título dos gráficos
        //Dados extas ao gráfico no título
        ChartTitle Titulo = new ChartTitle();
        //ChartTitle Titulo2 = new ChartTitle();

        Titulo.Font = new System.Drawing.Font("Tahoma", 12, System.Drawing.FontStyle.Bold);

        //Titulo2.Font = new System.Drawing.Font("Tahoma", 9, System.Drawing.FontStyle.Bold);

        Titulo.Text = string.Format("Top 5 Maior índice de registros (%).");

        if (graficoTop5Batido.Titles.Count > 0)
            graficoTop5Batido.Titles.Clear();
        
        graficoTop5Batido.Titles.Add(Titulo);


        //Preenchendo dados
        MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwSituacaoGeralSetorTableAdapter adpSitu = new MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwSituacaoGeralSetorTableAdapter();
        ds.EnforceConstraints = false;
        adpSitu.FillDiaEmpresaRegistro(ds.vwSituacaoGeralSetor, Convert.ToInt32(Session["IDEmpresa"]), System.DateTime.Now.Date.Date);

        if (ds.vwSituacaoGeralSetor.Rows.Count > 0)
        {

            for (int i = 0; i <= ds.vwSituacaoGeralSetor.Rows.Count - 1; i++)
            {
                graficoTop5Batido.Series["Registro Efetuado"].Points.Add(new SeriesPoint(ds.vwSituacaoGeralSetor[i].Sigla, ds.vwSituacaoGeralSetor[i].Total));
            }
        }
    }

    protected void MontaSituacaoGeralJustificativa()
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

        Titulo.Text = string.Format("Justificativas Geral.");

        if (graficoGeralJustificativa.Titles.Count > 0)
            graficoGeralJustificativa.Titles.Clear();

        graficoGeralJustificativa.Titles.Add(Titulo);

        //graficoGeralJustificativa.Series.Remove(graficoGeralJustificativa.Series["Ausência de registro"]);

        //graficoGeralJustificativa.Series.Add("Ausência de registro", ViewType.Pie);

        //graficoGeralJustificativa.Series["Ausência de registro"].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

        //graficoGeralJustificativa.Series["Ausência de registro"].LegendPointOptions.ValueNumericOptions.Format = NumericFormat.General;

        //graficoGeralJustificativa.Series["Ausência de registro"].ValueScaleType = ScaleType.Numerical;

        //graficoGeralJustificativa.Series.Remove(graficoGeralJustificativa.Series["Justificado"]);

        //graficoGeralJustificativa.Series.Add("Justificado", ViewType.Pie);

        //graficoGeralJustificativa.Series["Justificado"].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

        //graficoGeralJustificativa.Series["Justificado"].LegendPointOptions.ValueNumericOptions.Format = NumericFormat.General;

        //graficoGeralJustificativa.Series["Justificado"].ValueScaleType = ScaleType.Numerical;

        //Preenchedo Séries do gráfico.
        MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwTotalMotivosFaltaTableAdapter adpGeral = new MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwTotalMotivosFaltaTableAdapter();
        ds.EnforceConstraints = false;
        adpGeral.FillGeralTop5(ds.vwTotalMotivosFalta, System.DateTime.Now.Date, Convert.ToInt32(Session["IDEmpresa"]));


        if (ds.vwTotalMotivosFalta.Rows.Count > 0)
        {
            for (int i = 0; i <= ds.vwTotalMotivosFalta.Rows.Count - 1; i++)
            {
                graficoGeralJustificativa.Series["Series 1"].Points.Add(new SeriesPoint(ds.vwTotalMotivosFalta[i].DSMotivoFalta, ds.vwTotalMotivosFalta[i].TotalMotivoFalta));
            }
        }

    }

    protected void MontaSituacaoGeral()
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

        Titulo.Font = new System.Drawing.Font("Tahoma", 12, System.Drawing.FontStyle.Bold);

        //Titulo2.Font = new System.Drawing.Font("Tahoma", 9, System.Drawing.FontStyle.Bold);

        Titulo.Text = string.Format("Situação geral");

        if (graficoGeral.Titles.Count > 0)
            graficoGeral.Titles.Clear();
        
        graficoGeral.Titles.Add(Titulo);

        //graficoGeral.Series.Remove(graficoGeral.Series["Ausência de registro"]);

        //graficoGeral.Series.Add("Ausência de registro", ViewType.Pie);

        //graficoGeral.Series["Ausência de registro"].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

        //graficoGeral.Series["Ausência de registro"].LegendPointOptions.ValueNumericOptions.Format = NumericFormat.General;

        //graficoGeral.Series["Ausência de registro"].ValueScaleType = ScaleType.Numerical;

        //graficoGeral.Series.Remove(graficoGeral.Series["Justificado"]);

        //graficoGeral.Series.Add("Justificado", ViewType.Pie);

        //graficoGeral.Series["Justificado"].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

        //graficoGeral.Series["Justificado"].LegendPointOptions.ValueNumericOptions.Format = NumericFormat.General;

        //graficoGeral.Series["Justificado"].ValueScaleType = ScaleType.Numerical;

        //Preenchedo Séries do gráfico.
        MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwSituacaoGeralDiaTableAdapter adpGeral = new MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwSituacaoGeralDiaTableAdapter();
        ds.EnforceConstraints = false;
        adpGeral.FillIDEmpresa(ds.vwSituacaoGeralDia,Convert.ToInt32(Session["IDEmpresa"]));


        if (ds.vwSituacaoGeralDia.Rows.Count > 0)
        {
            for (int i = 0; i <= ds.vwSituacaoGeralDia.Rows.Count - 1; i++)
            {
                switch (ds.vwSituacaoGeralDia[i].Situ)
                {
                    case "Batido":
                        graficoGeral.Series["Registro Efetuado"].Points.Add(new SeriesPoint("Registro(s)", ds.vwSituacaoGeralDia[i].Total));
                        //graficoGeral.Series["Registro Efetuado"].Points[].
                        break;
                    case "Justificado":
                        graficoGeral.Series["Registro Efetuado"].Points.Add(new SeriesPoint("Justificado(s)", ds.vwSituacaoGeralDia[i].Total));
                        break;
                    case "Nao Batido":
                        graficoGeral.Series["Registro Efetuado"].Points.Add(new SeriesPoint("Ausência de registro", ds.vwSituacaoGeralDia[i].Total));
                        break;
                }
            }
        }


    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (HttpContext.Current.User.Identity.IsAuthenticated == false)
        //{
            //Response.Redirect("~/Account/Login.aspx");
        //}
        //else if(IsPostBack)
        //{
            MontaSituacaoGeral();
            //PreencheGridRegistro();
            PreencherbSetor();
            PReencheTop5NumeroFaltas();
            PReencheTop5Justificados();
            PReencheTop5Batidos();
            MontaSituacaoGeralJustificativa();
        //}
    }
    protected void cbGeral_Callback(object sender, CallbackEventArgsBase e)
    {
        MontaSituacaoGeral();
        //PreencheGridRegistro();
        PreencherbSetor();
        PReencheTop5NumeroFaltas();
        PReencheTop5Justificados();
        PReencheTop5Batidos();
        MontaSituacaoGeralJustificativa();
    }
}