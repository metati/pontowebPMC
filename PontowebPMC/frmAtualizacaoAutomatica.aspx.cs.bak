using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Timers;
using System.Web.UI.WebControls;
using MetodosPontoFrequencia;

public partial class frmAtualizacaoAutomatica : System.Web.UI.Page
{
    DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
    AtualizacaoAutomatica AT = new AtualizacaoAutomatica();
    public int Dado = 0;
    
    public System.Timers.Timer TT = new System.Timers.Timer();
    
    private void AtualizaPagina(DateTime DataHora, int TPDAdo)
    {
        AT.InsereNaTabela(DataHora);
        AT.Seleciona(ds);
        gridHora.DataSource = ds;
        gridHora.DataBind(); 

        //dados
        AT.InsereGrafico(1, TPDAdo);
        AT.SelecionaGrafico(ds);
        Grafico.DataSource = ds;
        Grafico.DataBind();


    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //TT.Elapsed +=new ElapsedEventHandler(TT_Elapsed);
        //TT.Interval = 1000;
        //TT.Enabled = true;
        //AtualizaPagina(System.DateTime.Now);
    }
    
   // private void TT_Elapsed(object source, ElapsedEventArgs e)
   // {
   //     AtualizaPagina(System.DateTime.Now);
   // }

    protected void gridHora_CustomCallback1(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
      // AtualizaPagina(System.DateTime.Now,Dado);
    }
    protected void Grafico_CustomCallback(object sender, DevExpress.XtraCharts.Web.CustomCallbackEventArgs e)
    {
       AtualizaPagina(System.DateTime.Now, Convert.ToInt32(e.Parameter));
        //Permitir atualização em tempo real mediante acesso via web... caso não tenha salvar dados em arquivo texto
        //para recompor banco de dados quando a reestabelecer a internet.......
        // Na tabela de celula --- faltando finaliza-la !
        // Finalizar tb tabela de estoque - Empresa - Tipo usuário
        //Acrescentar tudo o que for possível,
        //Criar ou não um tipo de célula - provavelmente sim ...
        //
    }
}