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

public partial class Painel_frmPainel : System.Web.UI.Page
{

    //Variáveis
    DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
    MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwHorasDiaSituacaoTableAdapter adpHoraDiaSitu = new MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwHorasDiaSituacaoTableAdapter();

    Frequencia Freq = new Frequencia();
    PreencheTabela PT = new PreencheTabela();

    protected void PreencherbSetor()
    {
        MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.TBSetorTableAdapter adpSetor = new MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.TBSetorTableAdapter();
        adpSetor.Fill(ds.TBSetor);
        //rbSetor.DataSource = ds.TBSetor;
        //rbSetor.DataBind();
    }

    protected void PreencheGridRegistro()
    {
        MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwLogRegistroSetorTableAdapter adpLog = new MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwLogRegistroSetorTableAdapter();
        //adpLog.Fill(ds.vwLogRegistroSetor);

        gridRegistro.DataSource = ds;
        gridRegistro.DataBind();
    }
    
    protected string PegarImagem(DevExpress.Web.ASPxGridView.GridViewDataItemTemplateContainer container)
    {
        string caminho;
        DateTime DataParametro;
        DataParametro = Convert.ToDateTime(gridRegistro.GetRowValues(container.VisibleIndex, "MaiorRegistro").ToString());

        if(System.DateTime.Now >= DataParametro.AddHours(3))
        {
            caminho = "~/Images/VermelhoGrid.png";
        }
        else
            caminho = "~/Images/VerdeGrid.png";
        
        return caminho;
    }
    
    protected void MontaSituacaoGeral()
    {
        //Montando dados do Grafico
        graficoGeral.Series.Remove(graficoGeral.Series["Registro Efetuado"]);

        graficoGeral.Series.Add("Registro Efetuado", ViewType.Pie);

        //XYDiagram diagrama = graficoGeral.Diagram as XYDiagram;

        //diagrama.AxisY.Visible = false;
        //diagrama.AxisX.Visible = false;      
        
        graficoGeral.Legend.Visible = true;

        graficoGeral.Legend.TextVisible = true;

                
        graficoGeral.Series["Registro Efetuado"].LabelsVisibility = DevExpress.Utils.DefaultBoolean.True;

        graficoGeral.Series["Registro Efetuado"].Label.Border.Visible = true;
        graficoGeral.Series["Registro Efetuado"].Label.LineVisible = true;

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

        Titulo.Text = string.Format("Situação geral do Ponto Frequência.");

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

        adpGeral.Fill(ds.vwSituacaoGeralDia);


        if (ds.vwSituacaoGeralDia.Rows.Count > 0)
        {
            for (int i = 0; i <= ds.vwSituacaoGeralDia.Rows.Count - 1; i++)
            {
                    switch (ds.vwSituacaoGeralDia[i].Situ)
                    {
                        case "Batido":
                            graficoGeral.Series["Registro Efetuado"].Points.Add(new SeriesPoint("Registro(s)", ds.vwSituacaoGeralDia[i].Total));
                            lbRegistro.Text = string.Format("{0} {1}", lbRegistro.Text, ds.vwSituacaoGeralDia[i].Total);    
                        //graficoGeral.Series["Registro Efetuado"].Points[].
                            break;
                        case "Justificado":
                            graficoGeral.Series["Registro Efetuado"].Points.Add(new SeriesPoint("Justificado(s)", ds.vwSituacaoGeralDia[i].Total));
                            lbJustificado.Text = string.Format("{0} {1}", lbJustificado.Text, ds.vwSituacaoGeralDia[i].Total);
                            break;
                        case "Nao Batido":
                            graficoGeral.Series["Registro Efetuado"].Points.Add(new SeriesPoint("Ausência de registro", ds.vwSituacaoGeralDia[i].Total));
                            lbAusencia.Text = string.Format("{0} {1}", lbAusencia.Text, ds.vwSituacaoGeralDia[i].Total);
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
        PreencheGridRegistro();
        PreencherbSetor();
        //}
    }
}