using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetodosPontoFrequencia;
using MetodosPontoFrequencia.Justificativa;

public partial class frmHorasDiarias : System.Web.UI.Page
{
    public DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
    private Justificativa jus = new Justificativa();
    public Frequencia Freq = new Frequencia();
    public PreencheTabela PT = new PreencheTabela();
    Justificativa justificativa = new Justificativa();
    public int Lei;
    public string msg;
    public bool Linhas;
    private DateTime Horas;
    protected int Ano = 0;
    protected TimeSpan terminojornada, sugestaoEntradaTarde;
    protected string tothorasDiarias;
    protected string Hora;
    protected TimeSpan Segundos;
    protected string TotalHoraDia;
    public int TotalHorasDia;
    bool AbonoFalta;
    int? TotaDia;
    private string TextoConfirmacao;
    private TimeSpan EntradaManha, SaidaManha, EntradaTarde, SaidaTarde, HEntradaManha,
     HSaidaManha, HEntradaTarde, HSaidaTarde;
    private double SomaTotal;
    private bool _usuarioPlantonista;
    public string DataDesconsidera
    {
        get
        {
            return Acesso.DataDesconsidera;
        }
    }
    private bool IsencaoPonto;

    public string ObrigaQuatroBatidas
    {
        get
        {
            return Acesso.ObrigaQuatroBatidas;
        }
    }

    Crip crp = new Crip();

    protected void Page_Load(object sender, EventArgs e)
    {
        var teste = Session;
        Crip crp = new Crip();

        if (HttpContext.Current.User.Identity.IsAuthenticated == false)
        {
            Response.Redirect("~/Account/Login.aspx");
        }
        else
        {

            lblegenda1.BackColor = System.Drawing.Color.DodgerBlue;
            lblegenda1.Text = "**";
            lblegenda1.ForeColor = System.Drawing.Color.DodgerBlue;
            lblegenda2.BackColor = System.Drawing.Color.ForestGreen;
            lblegenda2.ForeColor = System.Drawing.Color.ForestGreen;
            lblegenda2.Text = "**";
            lblegenda3.BackColor = System.Drawing.Color.DarkOrange;
            lblegenda3.ForeColor = System.Drawing.Color.DarkOrange;
            lblegenda3.Text = "**";
            lblegenda4.BackColor = System.Drawing.Color.IndianRed;
            lblegenda4.ForeColor = System.Drawing.Color.IndianRed;
            lblegenda4.Text = "**";
            lblegenda5.BackColor = System.Drawing.Color.Yellow;
            lblegenda5.ForeColor = System.Drawing.Color.Yellow;
            lblegenda5.Text = "**";

            lblegenda6.BackColor = System.Drawing.Color.DarkSlateGray;
            lblegenda6.ForeColor = System.Drawing.Color.DarkSlateGray;
            lblegenda6.Text = "**";


            if (!IsPostBack)
            {
                coIDusuario.Add("iduMinhasHoras", crp.CriptograFa(Convert.ToString(Session["IDUsuario"])));
                coIDusuario.Add("iduvMinhasHoras", crp.CriptograFa(Convert.ToString(Session["IDVinculoUsuarioFinal"])));
                coIDSetorMinhas.Add("idsetorMinhasHoras", crp.CriptograFa(Convert.ToString(Session["IDSETOR"])));
                CarregaDropMes();
                PreencheddlMotivoFalta();
                cbMesN.Value = System.DateTime.Now.Month.ToString();
                //FrequenciaMes(Convert.ToInt32(Session["IDUsuario"]), System.DateTime.Now.Month);
                this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> PreencheGrid();</script>");
            }
        }
    }
    public void CarregaDropMes()
    {
        PT.PreencheTBMes(ds);
        cbMesN.DataSource = ds;
        cbMesN.DataBind();
    }
    public void FolhaPDF(bool rel)
    {
        if (rel)
        {
            //Session["MesCorrenteH"] = cbMesN.SelectedItem.Value;
            Response.Write("<script>window.open('/Relatorio/frmRelHorasDia.aspx');</script>");
        }
        else
        {
            this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('Não há registros para o mês selecionado.');</script>");
        }
    }
    public void FrequenciaMes(int IDUsuario, int Mes)
    {
        DataSetPontoFrequencia dsu = new DataSetPontoFrequencia();

        Freq.DadosUsuario(dsu, IDUsuario, Convert.ToInt32(Session["IDEmpresa"]), Convert.ToInt32(Session["IDSETOR"]));
        //coIDUsuario. = Convert.ToString(Session["IDUsuario"]);
        //coIDSetor.Value = Convert.ToString(Session["IDSETOR"]);
        if (rbAno.SelectedItem.Text == "Ano Corrente")
            Ano = System.DateTime.Now.Year;
        else
            Ano = System.DateTime.Now.Year - 1;

        try
        {
            _usuarioPlantonista = dsu.vwUsuariogrid[0].RegimePlantonista;
        }
        catch { }
        

        if (_usuarioPlantonista)
        {
            msg = Freq.HorasDiasPlantonista(Mes, IDUsuario, Ano, ds, Convert.ToInt32(Session["IDEmpresa"]), Convert.ToInt32(Session["IDSETOR"]), false);
        }
        else
        {
            msg = Freq.HorasDias(Mes, IDUsuario, Ano, ds, Convert.ToInt32(Session["IDEmpresa"]), Convert.ToInt32(Session["IDSETOR"]));
        }


        if (msg != "1")
        {
            gridHoras.DataSource = ds;

            if (!ds.vWHorasDia[0].IsDSSetorNull())
            {
                //Label3.Text = ds.vWHorasDia[0].DSSetor.ToString();
                //Label3.DataBind();
            }

            if (ds.vWHorasDia[0].DSUsuario != "")
            {
                //Label2.Text = ds.vWHorasDia[0].DSUsuario.ToString();
                //Label2.DataBind();
            }
            gridHoras.DataBind();
        }
        else
        {
            this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('Não há registros para o mês selecionado.');</script>");
            gridHoras.DataBind();
        }
    }

    //prevendo o término da jornada
    protected string TerminoJornada(DevExpress.Web.ASPxGridView.GridViewDataItemTemplateContainer Container)
    {
        string EntrManha, SaidaManha, EntrTarde, SaidaTarde, horasdia, dtfrequencia, IMF, IDRegimeHora;

        IMF = gridHoras.GetRowValues(Container.VisibleIndex, "IMF").ToString();

        terminojornada = TimeSpan.Parse("00:00:00");

        if (IMF == "0")
        {

            IDRegimeHora = gridHoras.GetRowValues(Container.VisibleIndex, "IDRegimeHora").ToString();
            EntrManha = gridHoras.GetRowValues(Container.VisibleIndex, "EntradaManha").ToString();
            SaidaManha = gridHoras.GetRowValues(Container.VisibleIndex, "SaidaManha").ToString();
            EntrTarde = gridHoras.GetRowValues(Container.VisibleIndex, "EntradaTarde").ToString();
            SaidaTarde = gridHoras.GetRowValues(Container.VisibleIndex, "SaidaTarde").ToString();
            tothorasDiarias = gridHoras.GetRowValues(Container.VisibleIndex, "TotHorasDiarias").ToString();
            horasdia = gridHoras.GetRowValues(Container.VisibleIndex, "HorasDia").ToString();
            dtfrequencia = gridHoras.GetRowValues(Container.VisibleIndex, "DataFrequencia").ToString();

            if (tothorasDiarias == "")
                tothorasDiarias = "0";

            //Horas
            if (EntrManha == "00:00:00" && SaidaManha == "00:00:00" && EntrTarde == "00:00:00" && SaidaTarde == "00:00:00")
            {
                terminojornada = TimeSpan.Parse("00:00:00");
            }
            else if (EntrManha != "00:00:00" && SaidaManha == "00:00:00" && EntrTarde == "00:00:00" && SaidaTarde == "00:00:00")
            {
                terminojornada = TimeSpan.Parse(Convert.ToDateTime(EntrManha).AddHours(Convert.ToInt32(tothorasDiarias)).ToLongTimeString());
                //Adiciona 2 horas de almoço para regime de expediente de 8 horas
                if (tothorasDiarias == "8")
                    terminojornada += TimeSpan.Parse("02:00:00");
            }
            else if (EntrManha == "00:00:00" && SaidaManha == "00:00:00" && EntrTarde != "00:00:00" && SaidaTarde == "00:00:00")
            {
                if (tothorasDiarias == "4" && IDRegimeHora == "7")
                {
                    terminojornada = TimeSpan.Parse(Convert.ToDateTime(EntrTarde).AddHours(Convert.ToInt32(tothorasDiarias)).ToLongTimeString());
                    terminojornada = terminojornada + new TimeSpan(0, 30, 0);
                }
                else
                    terminojornada = TimeSpan.Parse(Convert.ToDateTime(EntrTarde).AddHours(Convert.ToInt32(tothorasDiarias)).ToLongTimeString());
            }
            else if (horasdia != "00:00:00" && tothorasDiarias == "8" && EntrTarde == "00:00:00" && SaidaTarde == "00:00:00") //Se regime de expedinte de 8 horas fazer os calculos para conseguir a saída final
            {
                sugestaoEntradaTarde = TimeSpan.Parse(SaidaManha).Add(TimeSpan.Parse("02:00:00"));

                terminojornada = TimeSpan.Parse("08:00:00") - TimeSpan.Parse(horasdia);

                terminojornada = terminojornada + sugestaoEntradaTarde;
            }
            else if (horasdia != "00:00:00" && tothorasDiarias == "8" && EntrTarde != "00:00:00" && SaidaTarde == "00:00:00") //Se regime de expedinte de 8 horas fazer os calculos para conseguir a saída final
            {
                terminojornada = TimeSpan.Parse("08:00:00") - TimeSpan.Parse(horasdia); //Qts horas se passaram pela manhã;
                terminojornada = TimeSpan.Parse(EntrTarde).Add(terminojornada);
            }
        }
        else
        {
            terminojornada = TimeSpan.Parse("00:00:00");
        }

        Hora = terminojornada.ToString();
        //CorDaFonte();

        if (terminojornada.ToString() == "00:00:00")
            return "------";
        else
            return terminojornada.ToString();
    }

    protected System.Drawing.Color CorDaFonte()
    {
        if (Hora == "00:00:00")
            return System.Drawing.Color.Black;
        else
            return System.Drawing.Color.White;
    }

    protected void gridHoras_HtmlRowPrepared(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewTableRowEventArgs e)
    {
        if (Lei == 1)
        {
            e.Row.Font.Size = 9;

            string IDRegimeHOra = Convert.ToString(e.GetValue("IDRegimeHora"));

            string Justificado = Convert.ToString(e.GetValue("IMF"));

            int TotalHoras = Convert.ToInt32(e.GetValue("TotHorasDiarias"));
            string SituacaoJustificativa = "";

            bool FormaDesconto = Convert.ToBoolean(e.GetValue("DescontoTotalJornada"));
            try
            {
                SituacaoJustificativa = e.GetValue("SituacaoJustificativa").ToString();
            }
            catch { }

            DateTime DTFrequencia = Convert.ToDateTime(e.GetValue("DataFrequencia"));

            try
            {
                       //Isencao Ponto
            if (e.GetValue("IsencaoPonto").ToString() != string.Empty)
                IsencaoPonto = Convert.ToBoolean(e.GetValue("IsencaoPonto"));
            else
                IsencaoPonto = false;
            }
            catch
            {
                IsencaoPonto = false;
            }
 


            //zerar a variável.
            SomaTotal = 0;

            if (DTFrequencia.ToShortDateString() != "01/01/0001")
            {
                //if (e.GetValue("HorasDia").ToString() != "")
                //Horas = Convert.ToDateTime(e.GetValue("HorasDia"));
                //else
                //Horas = Convert.ToDateTime("00:00:00");
                TotalHoraDia = e.GetValue("HorasDia").ToString();
                Segundos = new TimeSpan(Convert.ToInt32(TotalHoraDia.Substring(0, 2)), Convert.ToInt32(TotalHoraDia.Substring(3, 2)), Convert.ToInt32(TotalHoraDia.Substring(6, 2)));
                Session["DescontoTotalJornada"] = FormaDesconto;
            }


            //09/12/2018
            // - Printar de vermelho para os atrasos tb. Mais que 15 minutos e o não 
            //cumprimento para o horário de almoço para que tem jornada diária de 8 horas.

            if (!FormaDesconto)
            {
                if (DTFrequencia.ToShortDateString() != "01/01/0001")
                {

                    //isenção ponto
                    if (IsencaoPonto)
                    {
                        if (e.Row.Cells.Count == 9)
                        {
                            e.Row.Cells[8].Text = "";
                            e.Row.Cells[7].Text = "";
                        }
                        return;
                    }

                    if (DTFrequencia.Date < DateTime.Now.Date)
                    {
                        if (IDRegimeHOra == "1" && (e.GetValue("EntradaManha").ToString() == "00:00:00" ||
        e.GetValue("SaidaManha").ToString() == "00:00:00" || e.GetValue("EntradaTarde").ToString() == "00:00:00" ||
        e.GetValue("SaidaTarde").ToString() == "00:00:00") && (Justificado == "0")
        && (DTFrequencia.DayOfWeek.ToString() != "Saturday" && DTFrequencia.DayOfWeek.ToString() != "Sunday" || _usuarioPlantonista) && SituacaoJustificativa == "")
                        {
                            e.Row.BackColor = System.Drawing.Color.IndianRed;
                            e.Row.ForeColor = System.Drawing.Color.White;
                            if (e.Row.Cells.Count == 9)
                            {
                                e.Row.Cells[8].Text = "";
                            }
                            return;
                        }
						
						if (!_usuarioPlantonista && (e.GetValue("EntradaManha").ToString() == "00:00:00" &&
                                e.GetValue("SaidaManha").ToString() == "00:00:00" && e.GetValue("EntradaTarde").ToString() == "00:00:00" &&
                            e.GetValue("SaidaTarde").ToString() == "00:00:00") && (Justificado == "0")
                        && (DTFrequencia.DayOfWeek.ToString() == "Saturday" || DTFrequencia.DayOfWeek.ToString() == "Sunday"))
                        {
                            e.Row.BackColor = System.Drawing.Color.Gainsboro;
                            if (e.Row.Cells.Count == 9)
                            {
								e.Row.Cells[7].Text = "";
                                e.Row.Cells[8].Text = "";
                                //e.Row.Cells[2].Text = "";
                            }
                            return;
                        }
						
						if (IDRegimeHOra == "1" && (e.GetValue("EntradaManha").ToString() == "00:00:00" ||
                                 e.GetValue("EntradaTarde").ToString() == "00:00:00") && (Justificado == "0")
                        
                        && SituacaoJustificativa == "")
                        {
                            e.Row.BackColor = System.Drawing.Color.IndianRed;
                            if (e.Row.Cells.Count == 9)
                            {
                                //e.Row.Cells[7].Text = "";
                                e.Row.Cells[8].Text = "";
                            }
                            return;
                        }

                        //Atrasos maiores que 15 minutos
                        EntradaManha = TimeSpan.Parse(e.GetValue("EntradaManha").ToString());
                        SaidaManha = TimeSpan.Parse(e.GetValue("SaidaManha").ToString());
                        EntradaTarde = TimeSpan.Parse(e.GetValue("EntradaTarde").ToString());
                        SaidaTarde = TimeSpan.Parse(e.GetValue("SaidaTarde").ToString());

                        HEntradaManha = TimeSpan.Parse(e.GetValue("HoraEntradaManha").ToString() + ":00");
                        HSaidaManha = TimeSpan.Parse(e.GetValue("HoraSaidaManha").ToString());
                        HEntradaTarde = TimeSpan.Parse(e.GetValue("HoraEntradaTarde").ToString());
                        HSaidaTarde = TimeSpan.Parse(e.GetValue("HoraSaidaTarde").ToString());

                        if (IDRegimeHOra == "1")
                        {

                            //Nas entradas pegar só a maior.
                            if (EntradaManha >= HEntradaManha)
                            {
                                SomaTotal = (EntradaManha - HEntradaManha).TotalMinutes;
                            }
                            //Saídas tratar as antecipadas.
                            if (SaidaManha < HSaidaManha)
                            {
                                SomaTotal = SomaTotal + (HSaidaManha - SaidaManha).TotalMinutes;
                            }
                            //Entrada só atrasada
                            if (EntradaTarde >= HEntradaTarde)
                            {
                                SomaTotal = SomaTotal + (EntradaTarde - HEntradaTarde).TotalMinutes;
                            }
                            //saída só na antecipação.
                            if (SaidaTarde < HSaidaTarde)
                            {
                                SomaTotal = SomaTotal + (HSaidaTarde - SaidaTarde).TotalMinutes;
                            }
                        }
                        else
                        {
                            //Prevendo entrada e saida 1
                            if (e.GetValue("EntradaManha").ToString() != "00:00:00")
                            {
                                if (EntradaManha >= HEntradaManha)
                                {
                                    SomaTotal = (EntradaManha - HEntradaManha).TotalMinutes;
                                }
                                //Saídas tratar as antecipadas.
                                if (SaidaManha < HSaidaManha)
                                {
                                    SomaTotal = SomaTotal + (HSaidaManha - SaidaManha).TotalMinutes;
                                }
                                try
                                {
                                    if (SomaTotal < 15)
                                    {
                                        SaidaManha = (SaidaManha.TotalMinutes == 0) ? EntradaManha : SaidaManha;
                                        double minu = (int.Parse(e.GetValue("TotHorasDiarias").ToString()) * 60) - (SaidaManha - EntradaManha).TotalMinutes;
                                        if (minu > 15 && _usuarioPlantonista)
                                        {
                                            SomaTotal = minu;
                                        }
                                    }

                                }
                                catch { }
                            }
                            //PRevendo entrada 2 e saída 2
                            if (e.GetValue("EntradaTarde").ToString() != "00:00:00")
                            {
                                //Entrada só atrasada
                                if (EntradaTarde >= HEntradaTarde)
                                {
                                    SomaTotal = SomaTotal + (EntradaTarde - HEntradaTarde).TotalMinutes;
                                }
                                //saída só na antecipação.
                                if (SaidaTarde < HSaidaTarde)
                                {
                                    SomaTotal = SomaTotal + (HSaidaTarde - SaidaTarde).TotalMinutes;
                                }

                                try
                                {
                                    if (SomaTotal < 15)
                                    {
                                        SaidaTarde = (SaidaTarde.TotalMinutes == 0) ? EntradaTarde : SaidaTarde;
                                        double minu = (int.Parse(e.GetValue("TotHorasDiarias").ToString()) * 60) - (SaidaTarde - EntradaTarde).TotalMinutes;
                                        if (minu > 15 && _usuarioPlantonista)
                                        {
                                            SomaTotal = minu;
                                        }
                                    }

                                }
                                catch { }
                            }

                            //se não houver uma das entradas e nem justificativa printa como falta.
                            if (e.GetValue("EntradaManha").ToString() == "00:00:00" &&
                                (e.GetValue("EntradaTarde").ToString() == "00:00:00") && (Justificado == "" || Justificado == "0"))
                            {
                                SomaTotal = 30;
                            }
                        }

                        //SomaTotal = SomaTotal - 15;

                        if (SomaTotal > 15 && (Justificado == "" || Justificado == "0") && (DTFrequencia.DayOfWeek.ToString() != "Saturday" && DTFrequencia.DayOfWeek.ToString() != "Sunday" || _usuarioPlantonista) && (SituacaoJustificativa == "" || SituacaoJustificativa == "0"))
                        {
                            e.Row.BackColor = System.Drawing.Color.IndianRed;
                            e.Row.ForeColor = System.Drawing.Color.White;
                            if (e.Row.Cells.Count == 9)
                            {
                                e.Row.Cells[8].Text = "";
                            }
                            return;
                        }
                        //--------------
                    }
                }
            }
            else if (ObrigaQuatroBatidas.Equals("S") && DataDesconsidera != string.Empty)// alterado atender help redmine #111
            {
                if (DTFrequencia.ToShortDateString() != "01/01/0001")
                {
                    if (DTFrequencia.Date < DateTime.Now.Date && DTFrequencia.Date > Convert.ToDateTime(DataDesconsidera))
                    {
                        if (IDRegimeHOra == "1" && (e.GetValue("EntradaManha").ToString() == "00:00:00" ||
                                            e.GetValue("SaidaManha").ToString() == "00:00:00" || e.GetValue("EntradaTarde").ToString() == "00:00:00" ||
                                            e.GetValue("SaidaTarde").ToString() == "00:00:00") && (DTFrequencia.DayOfWeek.ToString() != "Saturday" && DTFrequencia.DayOfWeek.ToString() != "Sunday") && (Justificado == "0") && SituacaoJustificativa == "")
                        {
                            e.Row.BackColor = System.Drawing.Color.IndianRed;
                            if (e.Row.Cells.Count == 10)
                            {
                                e.Row.Cells[1].Text = "";
                                e.Row.Cells[2].Text = "";
                            }
                            return;
                        }
                    }
                }
            }

            if (IsencaoPonto)
            {
                if (e.Row.Cells.Count == 9)
                {
                    e.Row.Cells[8].Text = "";
                    e.Row.Cells[7].Text = "";
                }
                return;
            }

            //Caso Frequência igual igual ou maior a carga horária pintar de branco.
            //Caso na tolerância - pentar de laranjo
            //Caso abaixo da tolerância pintar de vermelho.

            if (Segundos.TotalSeconds != 0 && Justificado == "0" && SituacaoJustificativa == "")
            {
                TotalHoras = TotalHoras * 3600;

                if (TotalHoras == 4 && IDRegimeHOra == "7") // Se regime for de 4:30 , acrescentar mais 1800 segundos.s
                    TotalHoras = TotalHoras + 1800;


                if (Segundos.TotalSeconds >= TotalHoras)
                {
                    e.Row.BackColor = System.Drawing.Color.ForestGreen; // Completou a carga horária.
                    e.Row.ForeColor = System.Drawing.Color.White;

                    if (e.Row.Cells.Count == 9)
                    {
                        e.Row.Cells[7].Text = "";
                        e.Row.Cells[8].Text = "";
                    }
                }
                else if ((Segundos.TotalSeconds < TotalHoras) && (Segundos.TotalSeconds >= ((TotalHoras) - (15 * 60))))
                {
                    e.Row.BackColor = System.Drawing.Color.DarkOrange; //Tolerãncia
                    if (e.Row.Cells.Count == 9)
                    {
                        e.Row.Cells[7].Text = "";
                        e.Row.Cells[8].Text = "";
                    }
                }
                else if (SituacaoJustificativa == "1")
                {
                    e.Row.BackColor = System.Drawing.Color.Yellow;
                    e.Row.ForeColor = System.Drawing.Color.Black;
                    DevExpress.Web.ASPxGridView.GridViewDataColumn a = new DevExpress.Web.ASPxGridView.GridViewDataColumn("SituacaoJustificativa1");
                    if (e.Row.Cells.Count == 9)
                    {
                        e.Row.Cells[7].Text = "";
                    }

                    //var teste = e.Row.Cells[9];
                    //teste.Text = "TESTANDO";
                }
                else if (SituacaoJustificativa == "2")
                {
                    e.Row.BackColor = System.Drawing.Color.DarkSlateGray;
                    e.Row.ForeColor = System.Drawing.Color.White;
                    DevExpress.Web.ASPxGridView.GridViewDataColumn a = new DevExpress.Web.ASPxGridView.GridViewDataColumn("SituacaoJustificativa1");
                    if (e.Row.Cells.Count == 9)
                    {
                        e.Row.Cells[7].Text = "";
                    }
                    //var teste = e.Row.Cells[9];
                    //teste.Text = "TESTANDO";
                }
                else if (IDRegimeHOra != "300" && IDRegimeHOra != "400" && Segundos.TotalSeconds < TotalHoras)
                {
                    e.Row.BackColor = System.Drawing.Color.IndianRed;
                    e.Row.ForeColor = System.Drawing.Color.White;
                    if (e.Row.Cells.Count == 9)
                    {
                        e.Row.Cells[8].Text = "";
                    }
                }
                return;
            }
            else if ((DTFrequencia.DayOfWeek.ToString() == "Saturday" || DTFrequencia.DayOfWeek.ToString() == "Sunday" && !_usuarioPlantonista) && Justificado == "0" && (SituacaoJustificativa == "" || SituacaoJustificativa == "0"))
            {
                if (!_usuarioPlantonista)
                {
					
					//TRATAR REGISTRO SE HOUVER MARCAÇAO
								if ((e.GetValue("EntradaManha").ToString() != "00:00:00" ||
                                 e.GetValue("EntradaTarde").ToString() != "00:00:00") && (Justificado == "0")
                        
                        && SituacaoJustificativa == "" && (Segundos.TotalSeconds < TotalHoras) )
                        {
                            e.Row.BackColor = System.Drawing.Color.IndianRed;
                            if (e.Row.Cells.Count == 9)
                            {
                                //e.Row.Cells[7].Text = "";
                                e.Row.Cells[8].Text = "";
                            }
                        }
						else
						{
					e.Row.BackColor = System.Drawing.Color.Gainsboro;
                    if (e.Row.Cells.Count == 9)
                    {
                        e.Row.Cells[7].Text = "";
                        e.Row.Cells[8].Text = "";
                        //e.Row.Cells[2].Text = "";
                    }
						}
							
					

                }
                else
                {
                    e.Row.BackColor = System.Drawing.Color.IndianRed;
                    e.Row.ForeColor = System.Drawing.Color.White;
                    if (e.Row.Cells.Count == 10)
                    {
                        e.Row.Cells[1].Text = "";
                        e.Row.Cells[2].Text = "";
                    }
                }

                return;
            }
            else if (SituacaoJustificativa == "1")
            {
                e.Row.BackColor = System.Drawing.Color.Yellow;
                e.Row.ForeColor = System.Drawing.Color.Black;
                if (e.Row.Cells.Count == 9)
                {
                    e.Row.Cells[7].Text = "";
                }
                return;
            }
            else if (SituacaoJustificativa == "2")
            {
                e.Row.BackColor = System.Drawing.Color.DarkSlateGray;
                e.Row.ForeColor = System.Drawing.Color.White;
                DevExpress.Web.ASPxGridView.GridViewDataColumn a = new DevExpress.Web.ASPxGridView.GridViewDataColumn("SituacaoJustificativa1");
                if (e.Row.Cells.Count == 9)
                {
                    e.Row.Cells[7].Text = "";
                }
                //var teste = e.Row.Cells[9];
                //teste.Text = "TESTANDO";
            }
            else if ((Justificado != "0") && (Justificado != "10") && (Justificado != ""))
            {
                e.Row.BackColor = System.Drawing.Color.DodgerBlue;
                e.Row.ForeColor = System.Drawing.Color.White;
                if (e.Row.Cells.Count == 9)
                {
                    e.Row.Cells[7].Text = "";
                }
                return;
            }
            else if (((Justificado == "0" || Justificado == "10") && SituacaoJustificativa != "-1") && DTFrequencia.Date < DateTime.Now.Date
                && (IDRegimeHOra != "300" && IDRegimeHOra != "400"))
            {
                e.Row.BackColor = System.Drawing.Color.IndianRed;
                e.Row.ForeColor = System.Drawing.Color.White;
                if (e.Row.Cells.Count == 9)
                {
                    e.Row.Cells[8].Text = "";
                }
                return;
            }
            else if (DTFrequencia > DateTime.Now)
            {
                if (e.Row.Cells.Count == 9)
                {
                    e.Row.Cells[7].Text = "";
                    e.Row.Cells[8].Text = "";
                }
            }
            else if (SituacaoJustificativa == "-1")
            {
                if (e.Row.Cells.Count == 9)
                {
                    e.Row.Cells[7].Text = "";
                    e.Row.Cells[8].Text = "";
                }
            }
        }
    }

    protected void gridHoras_PageIndexChanged(object sender, EventArgs e)
    {
        Freq.HorasDias(Convert.ToInt32(cbMesN.SelectedItem.Value), Convert.ToInt32(Session["IDUsuario"]), System.DateTime.Now.Year, ds, Convert.ToInt32(Session["IDEmpresa"]), Convert.ToInt32(Session["IDSETOR"]));
    }
    protected void btPDF_Click(object sender, EventArgs e)
    {
        if (gridHoras.PageCount > 0)
        {//achar um jeito de dar ou negar permissão

            FrequenciaMes(Convert.ToInt32(Session["IDUsuario"]), Convert.ToInt32(cbMesN.SelectedItem.Value));
            btPDF.Font.Size = 9;
            //ASPxLabel9.Font.Size = 9;
            //ASPxLabel13.Font.Size = 9;
            Lei = 1;
            FolhaPDF(true);
        }
        else
            this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('Não há registros para o mês selecionado.');</script>");
    }
    protected void gridHoras_CustomCallback(object sender, DevExpress.Web.ASPxGridView.ASPxGridViewCustomCallbackEventArgs e)
    {
        if (e.Parameters == "0")
        {
            try
            {
                //Freq.ExcluirJustificativa(ds, Convert.ToInt32(hdItensJustificativa["IDFrequencia"].ToString()),
                //Convert.ToInt32(hdItensJustificativa["IDUsuario"].ToString()), Convert.ToInt32(Session["IDEmpresa"]),
                //Convert.ToDateTime(hdItensJustificativa["DTJust"].ToString()), Convert.ToInt32(Session["IDUsuario"].ToString()),
                //Convert.ToInt64(hdItensJustificativa["IDVinculoUsuario"]));

                //jus.ChangeStatusPedidoJustificativa(0, DateTime.Now, null, null, Convert.ToInt32(hdItensJustificativa["IDVinculoUsuario"]),
                //    Convert.ToInt32(hdItensJustificativa["IDFrequencia"].ToString()), Convert.ToDateTime(hdItensJustificativa["DTJust"].ToString()),
                //    Convert.ToInt32(hdItensJustificativa["IDUsuario"].ToString()), null);

            }
            catch (Exception ex)
            {

            }
        }
        FrequenciaMes(Convert.ToInt32(Session["IDUsuario"]), Convert.ToInt32(cbMesN.SelectedItem.Value));
        Lei = 1;
    }

    protected void cpJust_Callback1(object sender, DevExpress.Web.ASPxClasses.CallbackEventArgsBase e)
    {
        lbAlerta.Visible = false;
        lbAlerta.Text = "";
        MontaRadioList(e.Parameter);
    }

    private void MontaRadioList(string FASE)
    {
        if (FASE == "0")
        {
            if (hdJustificativa.Count > 0)
                rbList.Value = hdJustificativa["TPJust"].ToString();

            rbList.Items.Clear();
            rbList.Items.Add("Justificativa de registro único", 0);
            rbList.Items.Add("Justificativa de meio período", 1);
            rbList.Items.Add("Justificativa de período integral", 2);
            //labels
            lbMotivo.Visible = false;
            lbConfirmacao.Visible = false;
            lbOBS.Visible = false;
            cbMotivoFaltaManut.Visible = false;
            memoOBS.Visible = false;
            btRetornar.Visible = false;
            cbMotivoFaltaManut.Value = "";

            //Modo coletivo
            cbSetorModo.Visible = false;
            deDataFaltaModo.Visible = false;
            lbSetorModo.Visible = false;
            lbDataModo.Visible = false;
            lbResposta.Visible = false;
        }

        if (FASE == "1")
        {
            if (hdJustificativa.Count > 1)
            {
                if (hdJustificativa["INDEX"].ToString() != "" && rbList.Value == null)
                    rbList.Value = hdJustificativa["INDEX"].ToString();
            }

            switch (hdJustificativa["TPJust"].ToString())
            {
                case "0":
                    rbList.Items.Clear();
                    rbList.Items.Add("Entrada Manhã", 0);
                    rbList.Items.Add("Saída Manhã", 1);
                    rbList.Items.Add("Entrada Tarde", 2);
                    rbList.Items.Add("Saída Tarde", 3);
                    rbList.DataBind();
                    //labels
                    lbMotivo.Visible = false;
                    lbConfirmacao.Visible = false;
                    lbOBS.Visible = false;
                    cbMotivoFaltaManut.Visible = false;
                    memoOBS.Visible = false;
                    btRetornar.Visible = true;
                    lbResposta.Visible = false;

                    //Modo coletivo
                    cbSetorModo.Visible = false;
                    deDataFaltaModo.Visible = false;

                    lbSetorModo.Visible = false;
                    lbDataModo.Visible = false;

                    break;
                case "1":
                    rbList.Items.Clear();
                    rbList.Items.Add("Matutino", 0);
                    rbList.Items.Add("Vespertino", 1);
                    rbList.DataBind();

                    //labels
                    lbMotivo.Visible = false;
                    lbConfirmacao.Visible = false;
                    lbOBS.Visible = false;
                    cbMotivoFaltaManut.Visible = false;
                    memoOBS.Visible = false;
                    btRetornar.Visible = true;

                    //Modo coletivo
                    cbSetorModo.Visible = false;
                    deDataFaltaModo.Visible = false;
                    lbSetorModo.Visible = false;
                    lbDataModo.Visible = false;

                    lbResposta.Visible = false;
                    break;
                case "2":
                    rbList.Items.Clear();
                    rbList.Visible = false;
                    rbList.DataBind();
                    //labels
                    lbMotivo.Visible = true;
                    lbConfirmacao.Visible = false;
                    lbOBS.Visible = true;
                    cbMotivoFaltaManut.Visible = true;
                    btSalvarAvanc.Text = "Avançar";
                    btRetornar.Text = "Retornar";
                    memoOBS.Visible = true;

                    lbResposta.Visible = false;

                    if (hdItensJustificativa["Modo"].ToString() == "Coletivo")
                    {
                        //Modo coletivo
                        cbSetorModo.Visible = true;
                        deDataFaltaModo.Visible = true;

                        lbSetorModo.Visible = true;
                        lbDataModo.Visible = true;
                    }
                    else
                    {
                        //Modo coletivo
                        cbSetorModo.Visible = false;
                        deDataFaltaModo.Visible = false;

                        lbSetorModo.Visible = false;
                        lbDataModo.Visible = false;
                    }
                    break;
            }

        }
        else if (FASE == "2")
        {
            switch (hdJustificativa["TPJust"].ToString())
            {
                case "0":
                    rbList.Visible = false;
                    lbConfirmacao.Visible = false;
                    lbMotivo.Visible = true;
                    lbOBS.Visible = true;
                    cbMotivoFaltaManut.Visible = true;
                    btSalvarAvanc.Text = "Avançar";
                    btRetornar.Text = "Retornar";
                    memoOBS.Visible = true;
                    lbResposta.Visible = false;

                    if (hdItensJustificativa["Modo"].ToString() == "Coletivo")
                    {
                        //Modo coletivo
                        cbSetorModo.Visible = true;
                        deDataFaltaModo.Visible = true;

                        lbSetorModo.Visible = true;
                        lbDataModo.Visible = true;


                    }
                    else
                    {
                        //Modo coletivo
                        cbSetorModo.Visible = false;
                        deDataFaltaModo.Visible = false;

                        lbSetorModo.Visible = false;
                        lbDataModo.Visible = false;
                    }

                    break;
                case "1":
                    rbList.Visible = false;
                    lbConfirmacao.Visible = false;
                    lbMotivo.Visible = true;
                    lbOBS.Visible = true;
                    cbMotivoFaltaManut.Visible = true;
                    btSalvarAvanc.Text = "Avançar";
                    btRetornar.Text = "Retornar";
                    memoOBS.Visible = true;

                    lbResposta.Visible = false;

                    if (hdItensJustificativa["Modo"].ToString() == "Coletivo")
                    {
                        //Modo coletivo
                        cbSetorModo.Visible = true;
                        deDataFaltaModo.Visible = true;

                        lbSetorModo.Visible = true;
                        lbDataModo.Visible = true;


                    }
                    else
                    {
                        //Modo coletivo
                        cbSetorModo.Visible = false;
                        deDataFaltaModo.Visible = false;

                        lbSetorModo.Visible = false;
                        lbDataModo.Visible = false;

                    }

                    break;
                case "2":
                    lbAlerta.Visible = false;
                    lbAlerta.Text = "";
                    bool motivoExiste = true;
                    var motivoId = cbMotivoFaltaManut.Value.ToString();

                    if (!string.IsNullOrEmpty(motivoId))
                        motivoExiste = justificativa.ValidaMotivoExiste(motivoId);

                    //acrescentado em 11/10/2019
                    //se houver menos de 4 caracteres na combo não deixa avançar
                    if (motivoExiste)
                    {
                        if (cbMotivoFaltaManut.Text.Length <= 4)
                            motivoExiste = false;
                    }

                    if (motivoExiste)
                    {
                        rbList.Visible = false;
                        lbConfirmacao.Visible = true;
                        lbMotivo.Visible = false;
                        lbOBS.Visible = false;
                        cbMotivoFaltaManut.Visible = false;
                        memoOBS.Visible = false;
                        MontaTextoConfirmacao(cbMotivoFaltaManut.Text);
                        btSalvarAvanc.Text = "Salvar";
                        btRetornar.Text = "Cancelar";

                        lbResposta.Visible = false;

                        //Modo coletivo
                        cbSetorModo.Visible = false;
                        deDataFaltaModo.Visible = false;
                        lbSetorModo.Visible = false;
                        lbDataModo.Visible = false;
                    }
                    else
                    {
                        lbAlerta.Visible = true;
                        lbAlerta.Text = "O motivo selecionado não existe, favor selecionar um válido";
                        MontaRadioList("1");
                        break;
                    }
                    break;
            }
        }
        else if (FASE == "3")
        {
            lbAlerta.Visible = false;
            lbAlerta.Text = "";
            if (hdJustificativa["TPJust"].ToString() == "0" || hdJustificativa["TPJust"].ToString() == "1")
            {
                bool motivoExiste = true;
                var motivoId = cbMotivoFaltaManut.Value.ToString();
                if (!string.IsNullOrEmpty(motivoId))
                    motivoExiste = justificativa.ValidaMotivoExiste(motivoId);

                if (motivoExiste)
                {
                    rbList.Visible = false;
                    lbConfirmacao.Visible = true;
                    lbMotivo.Visible = false;
                    lbOBS.Visible = false;
                    cbMotivoFaltaManut.Visible = false;
                    memoOBS.Visible = false;

                    lbResposta.Visible = false;

                    //Modo coletivo
                    cbSetorModo.Visible = false;
                    deDataFaltaModo.Visible = false;

                    lbSetorModo.Visible = false;
                    lbDataModo.Visible = false;

                    MontaTextoConfirmacao(cbMotivoFaltaManut.Text);
                    btSalvarAvanc.Text = "Salvar";
                    btRetornar.Text = "Cancelar";
                }
                else
                {
                    lbAlerta.Visible = true;
                    lbAlerta.Text = "O motivo selecionado não existe, favor selecionar um válido";
                    MontaRadioList("2");
                }
            }
        }
        else if (FASE == "JU" || FASE == "JC")
        {
            rbList.Visible = false;
            lbConfirmacao.Visible = true;
            lbMotivo.Visible = false;
            lbOBS.Visible = false;
            cbMotivoFaltaManut.Visible = false;
            memoOBS.Visible = false;

            //Modo coletivo
            cbSetorModo.Visible = false;
            deDataFaltaModo.Visible = false;

            lbSetorModo.Visible = false;
            lbDataModo.Visible = false;

            //MontaTextoConfirmacao(cbMotivoFaltaManut.Text);
            btSalvarAvanc.Text = "Salvar";
            btRetornar.Text = "Cancelar";

            switch (FASE)
            {
                case "JU":
                    SalvarJustificativa(Convert.ToInt16(Session["IDUsuario"].ToString()), Convert.ToInt32(hdItensJustificativa["IDFrequencia"].ToString()), hdJustificativa["OBS"].ToString(), Convert.ToInt32(hdJustificativa["IDMotivoFalta"].ToString()));
                    break;
                case "JC":
                    JustificativaColetiva();
                    lbResposta.Text = ""; //Paleativo, até se desenvolver outra rotina para listar os que não deram certo na justificativa.
                    break;
            }

            if (lbResposta.Text == "")
            {
                MontaTextoConfirmacao(cbMotivoFaltaManut.Text);
                lbConfirmacao.Visible = true;
                lbConfirmacao.Text = "Operação realizada com sucesso.";
                lbConfirmacao.Font.Size = System.Web.UI.WebControls.FontUnit.Medium;
                lbConfirmacao.ForeColor = System.Drawing.Color.Green;
                lbConfirmacao.Font.Bold = true;

                lbResposta.Visible = false;
                btSalvarAvanc.Text = "Finalizar";
                btRetornar.Visible = false;
            }
            else
            {
                lbConfirmacao.Visible = false;
                btSalvarAvanc.Visible = false;
                lbResposta.ForeColor = System.Drawing.Color.Red;
                lbResposta.Font.Bold = true;
                lbResposta.Font.Size = System.Web.UI.WebControls.FontUnit.Medium;
                lbResposta.Visible = true;
                btRetornar.Text = "Cancelar";
            }
        }
    }

    private void MontaTextoConfirmacao(string MotivoFalta)
    {
        lbConfirmacao.Text = string.Format("Confirmar a justificativa: {0} .", MotivoFalta);
    }

    protected void SalvarJustificativa(int IDUsuario, int IDFrequencia, string OBS, int IDMotivoFalta)
    {
        //string HorasDia = gridFrequencia.GetRowValues(gridFrequencia.FocusedRowIndex, "HorasDia").ToString();
        //string TotHorasDiarias = gridFrequencia.GetRowValues(gridFrequencia.FocusedRowIndex, "TotHorasDiarias").ToString()


        //if (Convert.ToDateTime(HorasDia) < Convert.ToDateTime("0"+TotHorasDiarias+":00:00"))
        //{
        try
        {
            if (Session["TPUsuario"].ToString() == "9")
            {
                //this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('Usuário perfil Diretor. Impossível realizar justificativa ao próprio usuário.');</script>");
                msg = "Usuário de perfil Diretor. Não é possível realizar justificativa ao próprio usuário.";
                lbResposta.Text = msg;
                lbResposta.DataBind();
                return;
            }

            if(Convert.ToInt32(hdJustificativa["TPJust"].ToString()) + 1 == 2)
            {
                Frequencia freq = new Frequencia();
                if (freq.PermissaoJustificativaMeioPeriodo(Convert.ToInt32(hdItensJustificativa["IDVinculoUsuario"]),
                    Convert.ToDateTime(hdItensJustificativa["DTJust"].ToString())).IndexOf("sucesso") < 0)
                {
                    lbResposta.Text = "Justificativa de meio período não realizada por não contemplar o total do dia. Utilize a justificativa integral.";
                }

            }
            else
            {
                TotalHorasAbonoFalta(IDUsuario, IDMotivoFalta, Convert.ToDateTime(hdItensJustificativa["DTJust"].ToString()),
     hdItensJustificativa["TotalHorasDiarias"].ToString(), hdItensJustificativa["TotDIa"].ToString());

                msg = jus.SalvarPedidoJustificativa(IDFrequencia, IDMotivoFalta, OBS,
                    hdItensJustificativa["DTJust"].ToString(), TotaDia,
                    (Convert.ToInt32(hdJustificativa["TPJust"].ToString()) + 1), Convert.ToInt32(Session["IDEmpresa"].ToString()),
                    hdJustificativa["INDEX"].ToString(), Convert.ToInt32(Session["IDUsuario"]),
                    Convert.ToInt32(hdItensJustificativa["IDVinculoUsuario"]), Convert.ToInt32(Session["IDUsuario"]));
                //Freq.JustificaFrequenciaDia(); 

                lbResposta.Text = msg;
            }


            if (lbResposta.Text.IndexOf("sucesso") >= 0)
            {
                lbResposta.Text = "";
                lbResposta.DataBind();
            }
            else
            {
                if (Convert.ToInt32(hdJustificativa["TPJust"].ToString()) + 1 != 1)
                {
                    lbResposta.Text = "Favor utilizar o pedido de justificativa para meio período ou integral.";
                }

            }

            //lbResposta.Text = msg;
            //lbResposta.DataBind();

        }
        catch (Exception ex)
        {
            ex.ToString();
        }

        cbMotivoFaltaManut.SelectedIndex = 0; //Zerar Motivos de Falta
        cbMotivoFaltaManut.DataBind();
        memoOBS.Text = ""; ////Zerar Motivos de Falta
                           //}
                           //else
                           // {
                           //     msg = "Frequência do dia compatível com a carga horária diária. Impossível alterar.";
                           // }

        //PreenchegridFrequencia(Convert.ToInt32(cbSetor.SelectedItem.Value), Convert.ToInt32(cbUsuario.Value), Convert.ToInt32(cbMes.SelectedItem.Value));
    }

    protected void TotalHorasAbonoFalta(int IDUsuario, int IDMotivoFalta, DateTime DataJust, string TotDia, string HorasTotais)
    {
        MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwUsuariogridTableAdapter adpUser = new MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vwUsuariogridTableAdapter();
        MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.TBMotivoFaltaTableAdapter adpMOti = new MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.TBMotivoFaltaTableAdapter();

        if (hdJustificativa["TPJust"].ToString() == "2")
        {
            try
            {
                adpUser.FillIDEmpresaSetorUsuario(ds.vwUsuariogrid, Convert.ToInt32(Session["IDEmpresa"]), Convert.ToInt32(Session["IDSetor"].ToString()), IDUsuario);

                if (ds.vwUsuariogrid.Rows.Count > 0)
                {
                    TotalHorasDia = ds.vwUsuariogrid[0].TotHorasDiarias;
                }

                adpMOti.FillIDMotivoFalta(ds.TBMotivoFalta, IDMotivoFalta);

                if (ds.TBMotivoFalta.Rows.Count > 0)
                    AbonoFalta = ds.TBMotivoFalta[0].AbonarHoras;

                if (AbonoFalta)
                {
                    TotaDia = ds.vwUsuariogrid[0].TotHorasDiarias;
                }
                else
                {
                    TotaDia = 0;
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            //Caso maior não atualiza o valor da hora total
            //if (Convert.ToDateTime(HorasTotais) > TotaDia)
            //{
            //TotaDia = Convert.ToDateTime(HorasTotais);
            //}
        }

    }

    protected void JustificativaColetiva()
    {
        DataSetPontoFrequencia dsw = new DataSetPontoFrequencia();

        PT.PreenchevwHorasDia(dsw, FormatarData(Convert.ToDateTime(hdItensJustificativa["DataColetiva"].ToString()).ToShortDateString()), Convert.ToInt32(hdItensJustificativa["SetorColetiva"].ToString()), Convert.ToInt32(Session["IDEmpresa"]));

        if (dsw.vWHorasDia.Rows.Count > 0)
        {
            try
            {
                //Prever aqui situação de alguma falha, parar as inserções caso ocorra alguma exceção
                for (int i = 0; i <= (dsw.vWHorasDia.Rows.Count - 1); i++)
                {
                    TotalHorasAbonoFalta(dsw.vWHorasDia[i].IDusuario, Convert.ToInt32(hdItensJustificativa["SetorColetiva"].ToString()), Convert.ToDateTime(hdItensJustificativa["DataColetiva"].ToString()), string.Format("{0} 0{1}:00:00", Convert.ToDateTime(hdItensJustificativa["DataColetiva"].ToString()).ToShortDateString(), dsw.vWHorasDia[i].TotHorasDiarias.ToString()), dsw.vWHorasDia[i].HorasDia);

                    msg = Freq.JustificaFrequenciaDia(dsw.vWHorasDia[i].IDFrequencia, dsw.vWHorasDia[i].IDusuario,
                        Convert.ToInt32(hdJustificativa["IDMotivoFalta"].ToString()), hdJustificativa["OBS"].ToString(),
                        dsw.vWHorasDia[i].DTFrequencia, TotaDia, Convert.ToInt32(hdJustificativa["TPJust"].ToString()),
                        dsw.vWHorasDia[i].IDEmpresa, hdJustificativa["INDEX"].ToString(),
                        Convert.ToInt32(Session["IDUsuario"]), (int)dsw.vWHorasDia[i].IDVinculoUsuario, 0);
                }
                this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('" + msg + "');</script>");

            }
            catch (Exception ex)
            {
                ex.ToString();
                this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('Houve falha em tentar lançar a justificativa coletiva. Tente novamente.');</script>");
            }
        }
        else
        {
            this.Page.RegisterStartupScript("MSG", @"<script language='javascript'> alert('Não há registro(s) de frequência para a data selecionada. Lance uma falta/exceção.');</script>");
        }

    }

    protected string FormatarData(string DTFrequencia)
    {
        if (DTFrequencia.Length == 9)
        {
            if (DTFrequencia.Substring(0, 2).IndexOf("/") == 1)
                DTFrequencia = string.Format("0{0}", DTFrequencia);
            else
            {
                DTFrequencia = string.Format("{0}0{1}", DTFrequencia.Substring(0, 3), DTFrequencia.Substring(3, 6));
            }
        }
        else if (DTFrequencia.Length == 8)
        {
            DTFrequencia = string.Format("0{0}", DTFrequencia);
            DTFrequencia = string.Format("{0}0{1}", DTFrequencia.Substring(0, 3), DTFrequencia.Substring(3, 6));
        }

        return DTFrequencia;
    }

    protected void btSalvar_Click(object sender, EventArgs e)
    {
        SalvarJustificativa(Convert.ToInt32(coIDUsuarioModal.Value), Convert.ToInt32(coIDFrequencia.Value), memoOBSOLD.Text, Convert.ToInt32(cbMotivoFaltaManut.SelectedItem.Value));
    }

    protected void btOK0_Click(object sender, EventArgs e)
    {
        //ExcluirJustificativa();
    }

    protected void PreencheddlMotivoFalta()
    {
        if ((string)Session["TPUsuario"] != "1")
            PT.PreencheTBMotivoFalta(ds);
        else
            PT.PreencheTBMotivoFaltaCInoperante(ds);

        cbMotivoFaltaManut.DataSource = ds;
        cbMotivoFalta.DataSource = ds;

        cbMotivoFaltaManut.Items.Add("Justificar para o órgão.");

        cbMotivoFaltaManut.IncrementalFilteringMode = DevExpress.Web.ASPxEditors.IncrementalFilteringMode.Contains;
        cbMotivoFalta.IncrementalFilteringMode = DevExpress.Web.ASPxEditors.IncrementalFilteringMode.Contains;
        cbMotivoFalta.DataBind();
        cbMotivoFaltaManut.DataBind();
    }

    private void GetDetalhesJustPedido(string IDFrequencia)
    {

    }
}