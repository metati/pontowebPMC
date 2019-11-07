using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;



namespace MetodosPontoFrequencia
{
    public class Frequencia
    {
        DataSetPontoFrequencia.TBFrequenciaDataTable TBFrequencia = new DataSetPontoFrequencia.TBFrequenciaDataTable();
        DataSetPontoFrequenciaTableAdapters.TBFrequenciaTableAdapter adpFrequencia = new DataSetPontoFrequenciaTableAdapters.TBFrequenciaTableAdapter();

        DataSetPontoFrequencia.vwHorasDataTable TBhoras = new DataSetPontoFrequencia.vwHorasDataTable();
        DataSetPontoFrequenciaTableAdapters.vwHorasTableAdapter adpHoras = new DataSetPontoFrequenciaTableAdapters.vwHorasTableAdapter();

        DataSetPontoFrequencia.TBUsuarioDataTable TBUsuario = new DataSetPontoFrequencia.TBUsuarioDataTable();
        DataSetPontoFrequenciaTableAdapters.TBUsuarioTableAdapter adpUsuario = new DataSetPontoFrequenciaTableAdapters.TBUsuarioTableAdapter();

        DataSetPontoFrequencia.TBEmpresaDataTable TBEmpresa = new DataSetPontoFrequencia.TBEmpresaDataTable();
        DataSetPontoFrequenciaTableAdapters.TBEmpresaTableAdapter adpEmpresa = new DataSetPontoFrequenciaTableAdapters.TBEmpresaTableAdapter();

        DataSetPontoFrequencia.TBFeriasDataTable TBFeriasFrequencia = new DataSetPontoFrequencia.TBFeriasDataTable();
        DataSetPontoFrequenciaTableAdapters.TBFeriasTableAdapter adpFeriasFrequencia = new DataSetPontoFrequenciaTableAdapters.TBFeriasTableAdapter();

        DataSetPontoFrequencia.vwUsuariogridDataTable vwUsuarioGrid = new DataSetPontoFrequencia.vwUsuariogridDataTable();
        DataSetPontoFrequenciaTableAdapters.vwUsuariogridTableAdapter adpvwUsuarioGrid = new DataSetPontoFrequenciaTableAdapters.vwUsuariogridTableAdapter();

        DataSetPontoFrequenciaTableAdapters.vwFrequenciaDiaTableAdapter adpvwFrequenciaDia = new DataSetPontoFrequenciaTableAdapters.vwFrequenciaDiaTableAdapter();
        DataSetPontoFrequencia.vwFrequenciaDiaDataTable vwFrequenciaDia = new DataSetPontoFrequencia.vwFrequenciaDiaDataTable();


        //DataSetPontoFrequencia dsBaterPonto2;

        PreencheTabela PTbp = new PreencheTabela();

        private string NomeUsuario;
        private int idGrupoRegistro;
        private string TotHorasMes;
        private string TotHorasUsuario;
        private int HorasValidasMes;
        private string horas, minutos, segudos;
        private int data;
        //private int totalMesPercent;
        //private int TotalUsuarioPerct;
        private int IDMotivoFalta;
        private string OBS;
        private string HoraPadraoEntradaManha;
        private string HoraPadraoEntradaTarde;
        private string HoraPadraoSaidaManha;
        private string HoraPadraoSaidTarde;
        private int TotalHorasDiaUsuarioPadrao;
        private long IDRegimeHoraJustificativa;
        private string LimiteEntradaSaida;
        private string Limite, LimiteIntervalo;
        private string LimitePadraoAlmoco, LimitePadraoEntradaSaida;
        private string I;
        private bool TrabalhaSabado;
        private bool Bateu;
        private string TotalHorasDIAString;
        private bool DiaSeguinte;
        private int? IDmotivoFalta = null;
        private string textoJustificativa;
        private bool finalizadiaseguinte;
        private int IDEmpresaVinculo;
        private int LimiteMaximoPlantao;

        private int IDVinculoPE2, IDSetorPE2;

        private bool RegimePlantonista;

        private int vinculo;

        //para TESTE
        double H;


        //Datas para comparação da operação de datas justificadas
        private DateTime? horaentradamanhaJust, horasaidamanhaJust, horaentradatardeJust, horasaidatardeJust;
        // datas para comparação de horários
        private DateTime? horaentradaManha, horaentradatarde, horasaidamanha, horasaidatarde;

        private int? TotalExistenteSegudos;
        LogOperacao log = new LogOperacao();
        //protected DateTime TotHoras = new DateTime(00);

        TimeSpan sp = new TimeSpan(0, 0, 0);

        string msg;

        //28/06/2019
        private bool repeteProcedimento;
        TimeSpan TotalEscala, TotalJornada;

        //01/07/2019
        DateTime? saida2Anterior;

        //29/08/2019
        int TotalHorasJustificativa;

        //Usar esse esquema como padrão a partir de agora !
        private string HoraUsuarioEntradaManha
        {
            get { return HoraPadraoEntradaManha; }
        }
        private string HoraUsuarioSaidaManha
        {
            get { return HoraPadraoEntradaTarde; }
        }
        private string HoraUsuarioEntradaTarde
        {
            get { return HoraPadraoEntradaTarde; }
        }
        private string HoraUsuarioSaidaTarde
        {
            get { return HoraPadraoSaidTarde; }
        }
        private int TotalHOrasDiaPadraoUsuario
        {
            get { return TotalHorasDiaUsuarioPadrao; }
        }

        private void DadosUsuario(DataSetPontoFrequencia dsU, int IDVinculoUsuario, DateTime? DTFrequencia)
        {
            DataSetPontoFrequenciaTableAdapters.vwUsuariogridTableAdapter adpvwUsuarioGrid = new DataSetPontoFrequenciaTableAdapters.vwUsuariogridTableAdapter();
            //Alterado em 24/02/2016 - puxando os dados pela view acima atendendo os multiplos vinculos
            try
            {
                adpvwUsuarioGrid.Connection.Open();
                dsU.EnforceConstraints = false;
                adpvwUsuarioGrid.FillIDVinculoUsuario(dsU.vwUsuariogrid, IDVinculoUsuario);
                adpvwUsuarioGrid.Connection.Close();

                if (dsU.vwUsuariogrid.Rows.Count > 0)
                {
                    HoraPadraoEntradaManha = dsU.vwUsuariogrid[0].HoraEntradaManha;
                    HoraPadraoSaidaManha = dsU.vwUsuariogrid[0].HoraSaidaManha;
                    HoraPadraoEntradaTarde = dsU.vwUsuariogrid[0].HoraEntradaTarde;
                    HoraPadraoSaidTarde = dsU.vwUsuariogrid[0].HoraSaidaTarde;
                    //TotalHorasDiaUsuarioPadrao = dsU.vwUsuariogrid[0].TotHorasDiarias;
                    RegimePlantonista = dsU.vwUsuariogrid[0].RegimePlantonista;
                    IDRegimeHoraJustificativa = dsU.vwUsuariogrid[0].IDRegimeHora;
                    TotalHorasJustificativa = dsU.vwUsuariogrid[0].TotHorasDiarias;

                }
                else
                {
                    HoraPadraoEntradaManha = "00:00";
                    HoraPadraoSaidaManha = "00:00";
                    HoraPadraoEntradaTarde = "00:00";
                    HoraPadraoSaidTarde = "00:00";
                    //TotalHorasDiaUsuarioPadrao = 0;
                    IDRegimeHoraJustificativa = 0;
                    RegimePlantonista = false;
                }

                //Aqui pega o total de horas utilizado no dia da frequencia
                DataSetPontoFrequenciaTableAdapters.TBPadraoHoraUsuarioTableAdapter adpPadrao = new DataSetPontoFrequenciaTableAdapters.TBPadraoHoraUsuarioTableAdapter();
                adpPadrao.Connection.Open();
                adpPadrao.FillIDVinculoUsuarioData(dsU.TBPadraoHoraUsuario, DTFrequencia, IDVinculoUsuario);
                adpPadrao.Connection.Close();

                if (dsU.TBPadraoHoraUsuario.Rows.Count > 0)
                    TotalHorasDiaUsuarioPadrao = dsU.TBPadraoHoraUsuario[0].TotHorasDiarias;
                else
                    TotalHorasDiaUsuarioPadrao = 0;

            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        public void DadosUsuario(DataSetPontoFrequencia dsU, int IDVinculoUsuario)
        {
            DataSetPontoFrequenciaTableAdapters.vwUsuariogridTableAdapter adpvwUsuarioGrid = 
                new DataSetPontoFrequenciaTableAdapters.vwUsuariogridTableAdapter();

            try
            {
                adpvwUsuarioGrid.Connection.Open();
                dsU.EnforceConstraints = false;
                adpvwUsuarioGrid.FillIDVinculoUsuario(dsU.vwUsuariogrid, Convert.ToInt64(IDVinculoUsuario));
                adpvwUsuarioGrid.Connection.Close();

            }
            catch(Exception ex)
            {
                ex.ToString();
            }

        }

        public void DadosUsuario(DataSetPontoFrequencia dsU, int IdUsuario, int IdEmpresa, int IdSetor)
        {
            DataSetPontoFrequenciaTableAdapters.vwUsuariogridTableAdapter adpvwUsuarioGrid = new DataSetPontoFrequenciaTableAdapters.vwUsuariogridTableAdapter();
            //Alterado em 24/02/2016 - puxando os dados pela view acima atendendo os multiplos vinculos
            try
            {
                adpvwUsuarioGrid.Connection.Open();
                dsU.EnforceConstraints = false;
                adpvwUsuarioGrid.FillIDEmpresaSetorUsuario(dsU.vwUsuariogrid, IdEmpresa, IdSetor, IdUsuario);
                adpvwUsuarioGrid.Connection.Close();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        public string HashUsuario(DataSetPontoFrequencia ds, int IDUsuario, int IDEmpresa)
        {
            try
            {
                adpUsuario.FillHashPonto(ds.TBUsuario, IDUsuario, IDEmpresa);

                if (ds.TBUsuario.Rows.Count > 0)
                {
                    if (!ds.TBUsuario[0].IsTextHashCodeNull())
                        I = ds.TBUsuario[0].TextHashCode.Trim();
                    else
                    {
                        I = "1";
                    }
                }
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                ErroOperacao eo = new ErroOperacao();
                eo.RetornaErroOperacao(ex);
                I = "";
            }
            return I;
        }

        public bool BATEU
        {
            get
            {
                return Bateu;
            }
        }

        protected bool PermiteRegistroLimite(DateTime HoraRegistro, DateTime HoraPadraoRegistro, DateTime LimiteEntradaSaida, bool AcessoEspecial)
        {
            Limite = string.Empty;
            string Result = string.Empty;
            msg = string.Empty;

            //if (AcessoEspecial)
            //{
            //msg = "Horario";
            //return (true);
            //}

            //Segundo decreto, permitir registro de frequência entre as 12:00 e 20:00 - Horário padrão da SAD ...
            //Os que possuem horário flexivel , cadastrar como ponto Especial....

            switch (TBFrequencia.Rows.Count)
            {
                case 0: //Caso Zero - Sem registro na tabela, analise se o horário de registro está dentro do limite padrão
                    //De acordo com a reunião do dia 07/11/2014 - Usar como parâmetro de entrada e saída os padrões de entrada e saída de cada usuário
                    //Implementação logo abaixo    

                    if (HoraRegistro <= Convert.ToDateTime(string.Format("{0} 11:00:00.000", HoraRegistro.ToShortDateString()))) //Se Menor Usar parâmetros de entrada da manhã, caso contrário da tarde.
                    {
                        //--Se padrão de entrada estiver zerado - Usar como referência 08:00 para entrada manhã                       
                        if (HoraPadraoEntradaManha == "00:00")
                        {
                            HoraPadraoEntradaManha = string.Format("{0} 08:00:00.000", HoraRegistro.ToShortDateString());
                        }
                        else
                        {
                            HoraPadraoEntradaManha = string.Format("{0} {1}", HoraRegistro.ToShortDateString(), HoraPadraoEntradaManha);
                        }

                        //Verificar se houve adiantamento ou atraso---
                        if (HoraRegistro > Convert.ToDateTime(HoraPadraoEntradaManha))//Houve Atraso
                        {
                            if (HoraRegistro <= Convert.ToDateTime(HoraPadraoEntradaManha).AddMinutes(15))
                            {
                                msg = "Tolerancia";
                                return (true);
                            }
                            else if (HoraRegistro > Convert.ToDateTime(HoraPadraoEntradaManha).AddMinutes(15) && HoraRegistro <= Convert.ToDateTime(HoraPadraoEntradaManha).AddHours(1))
                            {
                                msg = "Atraso inf 1 hora";
                                return (true);
                            }
                            else if (HoraRegistro > Convert.ToDateTime(HoraPadraoEntradaManha).AddHours(1))
                            {
                                msg = "Atraso sup 1 hora";
                                return (true);
                            }
                        }
                        else
                        {
                            msg = "Horario";
                            return (true);
                        }
                    }
                    else //Parte tarde !
                    {
                        //--Se padrão de entrada estiver zerado - Usar como referência 13:00 para entrada tarde                       
                        if (HoraPadraoEntradaTarde == "00:00")
                        {
                            HoraPadraoEntradaTarde = string.Format("{0} 13:00:00.000", HoraRegistro.ToShortDateString());
                        }
                        else
                        {
                            HoraPadraoEntradaTarde = string.Format("{0} {1}", HoraRegistro.ToShortDateString(), HoraPadraoEntradaTarde);
                        }

                        //Verificar se houve adiantamento ou atraso---
                        if (HoraRegistro > Convert.ToDateTime(HoraPadraoEntradaTarde))//Houve Atraso
                        {
                            if (HoraRegistro <= Convert.ToDateTime(HoraPadraoEntradaTarde).AddMinutes(15))
                            {
                                msg = "Tolerancia";
                                return (true);
                            }
                            else if (HoraRegistro > Convert.ToDateTime(HoraPadraoEntradaTarde).AddMinutes(15) && HoraRegistro <= Convert.ToDateTime(HoraPadraoEntradaTarde).AddHours(1))
                            {
                                msg = "Atraso inf 1 hora";
                                return (true);
                            }
                            else if (HoraRegistro > Convert.ToDateTime(HoraPadraoEntradaTarde).AddHours(1))
                            {
                                msg = "Atraso sup 1 hora";
                                return (true);
                            }
                        }
                        else
                        {
                            msg = "Horario";
                            return (true);
                        }
                    }

                    //RETIRADO Atendendo a reunião de 07/11/2014
                    //if (HoraRegistro >= Convert.ToDateTime(string.Format("{0} {1}", HoraRegistro.ToShortDateString(), "12:00:00.000")) && HoraRegistro <= Convert.ToDateTime(string.Format("{0} {1}", HoraRegistro.ToShortDateString(), "20:00:00.000")))
                    //{
                    //Aqui -- Tratar Caso superior ou inferior a Uma Hora ...

                    //return (true);
                    //}
                    //else if (HoraRegistro < Convert.ToDateTime(string.Format("{0} {1}", HoraRegistro.ToShortDateString(), "12:00:00.000")))
                    //{
                    //Limite = Convert.ToString(Convert.ToDateTime(string.Format("{0} {1}",HoraRegistro.ToShortDateString(),"12:00:00")) - HoraRegistro);
                    //msg = "antecipado";
                    //return (false);
                    //}
                    //else if (HoraRegistro > Convert.ToDateTime(string.Format("{0} {1}", HoraRegistro.ToShortDateString(), "20:00:00.000")))
                    //{
                    //Limite = Convert.ToString(HoraRegistro - Convert.ToDateTime(string.Format("{0} {1}", HoraRegistro.ToShortDateString(), "20:00:00")));
                    //msg = "excedido";
                    //return (false);                        
                    //}
                    //RETIRADO Atendendo a reunião de 07/11/2014---------------------------------------------------------------------
                    break;
                case 1: //Tratamentro do complemento da jornada direto na ROTINA PARA ATENDER O DECRETO.
                    if (!TBFrequencia[0].IsIDMotivoFaltaNull())
                    {
                        switch (TBFrequencia[0].IDTPJustificativa)
                        {
                            case 0:
                                return (true);
                            case 1:
                                if ((TBFrequencia[0].TurnoJustificado.Trim() == "0" || TBFrequencia[0].TurnoJustificado.Trim() == "Ma") && HoraRegistro <= Convert.ToDateTime(string.Format("{0} {1}", HoraRegistro.ToShortDateString(), TBUsuario[0].HoraSaidaManha)))
                                {
                                    msg = "turno justificado";
                                    return (false);
                                }

                                if ((TBFrequencia[0].TurnoJustificado.Trim() == "1" || TBFrequencia[0].TurnoJustificado.Trim() == "Ve") && HoraRegistro <= Convert.ToDateTime(string.Format("{0} {1}", HoraRegistro.ToShortDateString(), TBUsuario[0].HoraSaidaTarde)))
                                {
                                    msg = "turno justificado";
                                    return (false);
                                }
                                //Caso não entre e nenhuma das excessões acima, entra abaixo.
                                return (true);

                            case 2:
                                msg = "dia justificado";
                                return (false);
                        }
                    }
                    return (true);
            }

            //tramento antigo - 15 minutos para Mais ou menos - Entrada e Saída....
            //if (HoraRegistro > HoraPadraoRegistro)
            //{
            //Result = string.Format("{0} {1}",HoraRegistro.Date.ToShortDateString(), Convert.ToString(HoraRegistro - HoraPadraoRegistro));

            //if (Convert.ToDateTime(Result) <= LimiteEntradaSaida)
            //{
            //return true;
            //}
            //else
            //{
            //Limite = Convert.ToString(Convert.ToDateTime(Result).ToShortTimeString());
            //msg = "excedido";
            //return false;
            //}
            //}
            //else
            //{
            //Result = string.Format("{0} {1}", HoraRegistro.Date.ToShortDateString(), Convert.ToString(HoraPadraoRegistro - HoraRegistro));

            //if (Convert.ToDateTime(Result) <= LimiteEntradaSaida)
            //{
            //return true;
            //}
            //else
            //{
            //msg = "antecipado";
            //Limite = Convert.ToString(Convert.ToDateTime(Result).ToShortTimeString());
            //return false;
            //}
            //}

            return (true);
        }

        private int ValidaVinculo(int IDUsuario, int IDEmpresa, string L)
        {
            DataSetPontoFrequencia.TBGrupoRegistroEmpresaDataTable TBGrupoRegistro = new DataSetPontoFrequencia.TBGrupoRegistroEmpresaDataTable();
            DataSetPontoFrequenciaTableAdapters.TBGrupoRegistroEmpresaTableAdapter adpGrupoRegistro = new DataSetPontoFrequenciaTableAdapters.TBGrupoRegistroEmpresaTableAdapter();
            DataSetPontoFrequenciaTableAdapters.TBVinculoUsuarioTableAdapter adpVinculo = new DataSetPontoFrequenciaTableAdapters.TBVinculoUsuarioTableAdapter();
            DataSetPontoFrequencia.TBVinculoUsuarioDataTable tbvinculo = new DataSetPontoFrequencia.TBVinculoUsuarioDataTable();

            //Grupo de registro vindo da batida de ponto.
            adpGrupoRegistro.Connection.Open();
            adpGrupoRegistro.FillIDEMPRESA(TBGrupoRegistro, IDEmpresa);
            adpGrupoRegistro.Connection.Close();
            if (TBGrupoRegistro.Rows.Count > 0)
                idGrupoRegistro = TBGrupoRegistro[0].IDGrupoRegistro;

            try
            {
                adpVinculo.Connection.Open();
                adpVinculo.FillIDUsuarioGrupoRegistro(tbvinculo, IDUsuario);
                adpVinculo.Connection.Close();

                if (tbvinculo.Rows.Count > 0) // Se não houver vínculo retorna falso
                {
                    adpGrupoRegistro.Connection.Open();
                    for (int i = 0; i <= (tbvinculo.Rows.Count - 1); i++)
                    {
                        adpGrupoRegistro.FillIDEMPRESA(TBGrupoRegistro, tbvinculo[i].IDEmpresa);
                        if (TBGrupoRegistro.Rows.Count > 0)
                        {
                            if (idGrupoRegistro == TBGrupoRegistro[i].IDGrupoRegistro)
                                return tbvinculo[i].IDEmpresa;
                        }
                        else
                            return 0;

                    }

                    adpGrupoRegistro.Connection.Close();
                }
                else
                    return 0;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return 0;
            }

            return 0;
        }

        private int ValidaVinculo(int IDUsuario, int IDEmpresa, string L, int IDVinculoUsuario)
        {
            DataSetPontoFrequencia.TBGrupoRegistroEmpresaDataTable TBGrupoRegistro = new DataSetPontoFrequencia.TBGrupoRegistroEmpresaDataTable();
            DataSetPontoFrequenciaTableAdapters.TBGrupoRegistroEmpresaTableAdapter adpGrupoRegistro = new DataSetPontoFrequenciaTableAdapters.TBGrupoRegistroEmpresaTableAdapter();
            DataSetPontoFrequenciaTableAdapters.TBVinculoUsuarioTableAdapter adpVinculo = new DataSetPontoFrequenciaTableAdapters.TBVinculoUsuarioTableAdapter();
            DataSetPontoFrequencia.TBVinculoUsuarioDataTable tbvinculo = new DataSetPontoFrequencia.TBVinculoUsuarioDataTable();

            //Grupo de registro vindo da batida de ponto.
            adpGrupoRegistro.Connection.Open();
            adpGrupoRegistro.FillIDEMPRESA(TBGrupoRegistro, IDEmpresa);
            adpGrupoRegistro.Connection.Close();
            if (TBGrupoRegistro.Rows.Count > 0)
                idGrupoRegistro = TBGrupoRegistro[0].IDGrupoRegistro;

            try
            {
                adpVinculo.Connection.Open();
                adpVinculo.FillIDUsuarioVinculoGrupoRegistro(tbvinculo, IDUsuario, IDVinculoUsuario);
                adpVinculo.Connection.Close();

                if (tbvinculo.Rows.Count > 0) // Se não houver vínculo retorna falso
                {
                    adpGrupoRegistro.Connection.Open();
                    for (int i = 0; i <= (tbvinculo.Rows.Count - 1); i++)
                    {
                        adpGrupoRegistro.FillIDEMPRESA(TBGrupoRegistro, tbvinculo[i].IDEmpresa);
                        if (TBGrupoRegistro.Rows.Count > 0)
                        {
                            if (idGrupoRegistro == TBGrupoRegistro[i].IDGrupoRegistro)
                                return tbvinculo[i].IDEmpresa;
                        }
                        else
                            return 0;

                    }

                    adpGrupoRegistro.Connection.Close();
                }
                else
                    return 0;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return 0;
            }

            return 0;
        }

        private bool ValidaVinculo(int IDUsuario, int IDEmpresa) //Valida o vinculo de um usuário com alguma empresa.
        {
            DataSetPontoFrequenciaTableAdapters.TBVinculoUsuarioTableAdapter adpVinculo = new DataSetPontoFrequenciaTableAdapters.TBVinculoUsuarioTableAdapter();
            DataSetPontoFrequencia.TBVinculoUsuarioDataTable tbvinculo = new DataSetPontoFrequencia.TBVinculoUsuarioDataTable();
            try
            {
                adpVinculo.Connection.Open();
                adpVinculo.FillIDUsuarioGrupoRegistro(tbvinculo, IDUsuario);
                adpVinculo.Connection.Close();

                if (tbvinculo.Rows.Count > 0) // Se não houver vínculo retorna falso
                {
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }
        }

        private bool VerificaIntervalodeRegistro(DateTime Batida, DateTime BatidaAnterior)
        {
            TimeSpan Totalminutos = Batida.Subtract(BatidaAnterior);

            if (Totalminutos.TotalMinutes <= 15)
                return false;
            else
                return true;
        }

        private bool FaltaLancada(int IDMotivoFalta)
        {
            if (IDMotivoFalta == 47)
                return true;
            else
                return false;
        }
        public string BaterPontoAssistSocial(int IDEmpresa, int idsetorbatida, int IDUsuario, DateTime HoraBatida,
        string HoraEntradaManha, string HoraSaidaManha, string horaEntradaTarde, string HoraSaidaTarde,
            int TotalHoraDia, string Nome, string PrimeiroNome, bool RegimePlantonista, long IDVinculoUsuario)
        {
            DataSetPontoFrequencia.TBVinculoUsuarioDataTable TBVinculo = new DataSetPontoFrequencia.TBVinculoUsuarioDataTable();
            DataSetPontoFrequenciaTableAdapters.TBVinculoUsuarioTableAdapter adpVinculoAss =
                new DataSetPontoFrequenciaTableAdapters.TBVinculoUsuarioTableAdapter();
            if (!RegimePlantonista) //Se não for plantonista, lets go. Senão. abaixo.
            {
                #region Registro de marcação regime de expediente

                //Não deixar registrar sem ser no seu órgão de origem
                //IDEmpresa = ValidaVinculo(IDUsuario, IDEmpresa, "");
                //adpvwUsuarioGrid.Connection.Open();
                //vwUsuarioGrid.Constraints.Clear();
                //adpvwUsuarioGrid.FillIDEmpresaUsuario(vwUsuarioGrid, IDEmpresa, IDUsuario);
                //adpvwUsuarioGrid.Connection.Close();
                //if (vwUsuarioGrid.Rows.Count == 0)
                //    return string.Format("{0}, faça sua marcação em seu órgão de lotação.", PrimeiroNome);

                //PreencheTabela PT = new PreencheTabela();
                //if (PT.GetIDEmpresaVinculo(IDVinculoUsuario) != IDEmpresa)
                //{
                //    return string.Format("{0}, faça sua marcação em seu órgão de lotação.", PrimeiroNome);
                //}
                //13/07/2018 - Início da nova rotina de registro de ponto.
                SomaTolerancia ST = new SomaTolerancia();

                //Primeiro, se a defasagem > que 5 dias. Retorna o texto abaixo.
                TimeSpan TotDias = DateTime.Now - HoraBatida;

                if (TotDias.TotalDays > 5)
                {
                    return string.Format("{0}, registro com mais de 5 dias ({1}) de defasagem. Procure o seu gestor!", NomeUsuario.Trim(), HoraBatida.ToShortDateString());
                }

                //Verifica se há afastamento em curso
                //Por enquanto FIxo de 2 horas ... Valor Inteiro... Após atrelar isso ao cadastro de empresas
                //LimiteMaximoPlantao = 2;

                try
                {
                    adpFeriasFrequencia.Connection.Open();
                    adpFeriasFrequencia.FillByVerificaFeriasCorrente(TBFeriasFrequencia, HoraBatida.Date, IDUsuario, IDVinculoUsuario); // Verifica férias corrente
                    adpFeriasFrequencia.Connection.Close();
                }
                catch
                {
                    adpFeriasFrequencia.Connection.Close();
                }

                if (TBFeriasFrequencia.Rows.Count > 0)
                {
                    return string.Format("{0}, Férias/Licença em vigência. Procure sua chefia imediata.", PrimeiroNome);
                }
                //Se o registro for entre as 7 e as 9 Verifica se há registro de ponto aberto na data anterior 
                //(Pesquisa dos plantões da PMC/Assist. Social)
                adpVinculoAss.FillIDVinculoUsuario(TBVinculo, IDVinculoUsuario);

                if (TBVinculo[0].IDCargo == 317) //Conselheiro Tutelar. Verificar se houve plantão.
                {
                    if (HoraBatida.Hour >= 5 & HoraBatida.Hour <= 9)
                    {
                        try
                        {
                            adpvwFrequenciaDia.Connection.Open();
                            adpvwFrequenciaDia.FillIDVinculoUsuarioAssistSocial(vwFrequenciaDia, IDVinculoUsuario,
                                HoraBatida.Date.Date.AddDays(-1));
                            adpvwFrequenciaDia.Connection.Close();

                            //Caso tenho um registro do dia anterior, finaliza a saída 2.
                            //Total Horas

                            if (vwFrequenciaDia.Rows.Count > 0)
                            {
                                //update saída tarde com o calculos de somatória.

                                DateTime? entradaManha, SaidaManha;

                                if (!vwFrequenciaDia[0].IsHoraEntraManhaNull())
                                {
                                    entradaManha = vwFrequenciaDia[0].HoraEntraManha;
                                }
                                else
                                    entradaManha = null;

                                if (!vwFrequenciaDia[0].IsHoraSaidaManhaNull())
                                {
                                    SaidaManha = vwFrequenciaDia[0].HoraSaidaManha;
                                }
                                else
                                    SaidaManha = null;

                                if (adpFrequencia.UpdateSaidaTarde(HoraBatida,
                                    ST.TotalDia(entradaManha, SaidaManha,
                                    vwFrequenciaDia[0].HoraEntradaTarde, HoraBatida, TotalHoraDia)
                                    , vwFrequenciaDia[0].IDUsuario, vwFrequenciaDia[0].IDFrequencia) == 0)
                                {
                                    return string.Format("{0}, banco de dados indisponível. Repita o processo.", PrimeiroNome);
                                }
                                else
                                {
                                    //Identificar a jornada  com a carga horária incompleta.
                                    if (ST.JornadaIncompleta)// Se false, não completou a jornada do dia.
                                    {
                                        return string.Format("{0}, jornada finalizada com sucesso!", PrimeiroNome); //jornada finalizada com a carga horária total incompleta!
                                    }
                                    else
                                        return string.Format("{0}, jornada finalizada com sucesso!", PrimeiroNome);

                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            adpvwFrequenciaDia.Connection.Close();
                            BaterPonto3(IDEmpresa, idsetorbatida, IDUsuario, HoraBatida,
            HoraEntradaManha, HoraSaidaManha, horaEntradaTarde, HoraSaidaTarde,
               TotalHoraDia, Nome, PrimeiroNome, RegimePlantonista, IDVinculoUsuario);
                            //return "Jornada de trabalho não identificada no banco de dados. Tente Novamente!";
                        }
                    }
                }
                //Caso não entre na clausula acima, segue com os registros para as demais situações.
                try
                {
                    adpvwFrequenciaDia.Connection.Open();
                    adpvwFrequenciaDia.FillIDVinculoUsuario(vwFrequenciaDia, IDVinculoUsuario, HoraBatida.Date.Date);
                    adpvwFrequenciaDia.Connection.Close();
                }
                catch
                {
                    adpvwFrequenciaDia.Connection.Close();
                    return "Jornada de trabalho não identificada no banco de dados. Tente Novamente!";
                }
                //Se não há registros, faz insert na entrada 1 ou entrada 2. Caso contrário, faz o tratamento das saídas.
                if (vwFrequenciaDia.Rows.Count == 0)
                {
                    //Se => 12 entrada 2, senão entrada 1
                    if (HoraBatida.Hour < 11) ///Entrada 1
                    {
                        if (adpFrequencia.Insert(IDUsuario, null, HoraBatida.Date, HoraBatida, null, null, null, null, null, HoraBatida.Month, HoraBatida.Year, IDEmpresa, IDVinculoUsuario) == 0)
                        {
                            return string.Format("{0}, banco de dados indisponível. Repita o processo.", PrimeiroNome);
                        }
                        else
                        {
                            if (ST.Tolerancia(Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.ToShortDateString(), HoraEntradaManha)), HoraBatida))
                            {
                                return string.Format("{0}, entrada 1 efetivada.", PrimeiroNome);
                            }
                            else
                                return string.Format("{0}, entrada 1 efetivada.", PrimeiroNome); //entrada 1 efetivada com atraso superior a 15 min.
                        }
                    } //Entrada 2
                    else
                    {
                        //Aqui, se for p 8 horas mandar a entrada da manhã como padrão de tolerância. Senão, mandar a da tarde.
                        if (adpFrequencia.Insert(IDUsuario, null, HoraBatida.Date, null, null, HoraBatida, null, null, null, HoraBatida.Month, HoraBatida.Year, IDEmpresa, IDVinculoUsuario) == 0)
                        {
                            return string.Format("{0}, banco de dados indisponível. Repita o processo.", PrimeiroNome);
                        }
                        else
                        {
                            if (TotalHoraDia == 8) // Se for 8 mandar a entrada da manhã. Senão. segue normal.
                            {
                                if (ST.Tolerancia(Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.ToShortDateString(), horaEntradaTarde)), HoraBatida))
                                {
                                    return string.Format("{0}, entrada 1 efetivada.", PrimeiroNome);
                                }
                                else
                                    return string.Format("{0}, entrada 2 efetivada.", PrimeiroNome); //entrada 2 efetivada com atraso superior a 15 min
                            }
                            else
                            {
                                if (ST.Tolerancia(Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.ToShortDateString(), HoraPadraoEntradaTarde)), HoraBatida))
                                {
                                    return string.Format("{0}, entrada 2 efetivada.", PrimeiroNome);
                                }
                                else
                                    return string.Format("{0}, entrada 2 efetivada.", PrimeiroNome); //entrada 2 efetivada com atraso superior a 15 min
                            }
                        }

                    }
                } //Parte das Saídas
                else //Saídas 1 e 2 . Aqui vem a principal novidade.
                {
                    //Primeiro. Trata a saída 2 caso todos os campos não estejam nulos.
                    if (!vwFrequenciaDia[0].IsHoraEntraManhaNull() && !vwFrequenciaDia[0].IsHoraSaidaManhaNull() && !vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                    {
                        if (HoraBatida < vwFrequenciaDia[0].HoraEntradaTarde)
                        {
                            return string.Format("{0}, horário de saída menor que a entrada. Verifique a hora informada.", PrimeiroNome);
                        }

                        if (!ST.PermiteRegistroTolerancia(HoraBatida, vwFrequenciaDia[0].HoraEntradaTarde))
                            return string.Format("{0}, respeite o limite de 10 minutos entre as marcações!", PrimeiroNome);


                        //update saída tarde com o calculos de somatória.
                        if (adpFrequencia.UpdateSaidaTarde(HoraBatida,
                            ST.TotalDia(vwFrequenciaDia[0].HoraEntraManha, vwFrequenciaDia[0].HoraSaidaManha,
                            vwFrequenciaDia[0].HoraEntradaTarde, HoraBatida, TotalHoraDia)
                            , vwFrequenciaDia[0].IDUsuario, vwFrequenciaDia[0].IDFrequencia) == 0)
                        {
                            return string.Format("{0}, banco de dados indisponível. Repita o processo.", PrimeiroNome);
                        }
                        else
                        {
                            //Identificar a jornada  com a carga horária incompleta.
                            if (ST.JornadaIncompleta)// Se false, não completou a jornada do dia.
                            {
                                return string.Format("{0}, jornada finalizada com sucesso.", PrimeiroNome); //jornada finalizada com a carga horária total incompleta!
                            }
                            else
                                return string.Format("{0}, jornada finalizada com sucesso!", PrimeiroNome);

                        }
                    }
                    //Aqui, entrada da tarde. Tratar aqui a questão do almoço.
                    if (!vwFrequenciaDia[0].IsHoraEntraManhaNull() && !vwFrequenciaDia[0].IsHoraSaidaManhaNull())
                    {

                        //Aqui, entrada da tarde. Tratar aqui a questão do almoço.
                        if (!ST.PermiteRegistroAlmoco(HoraBatida, vwFrequenciaDia[0].HoraSaidaManha))
                        {
                            return string.Format("{0}, horário de almoço, faça sua marcação em {1}", PrimeiroNome, ST.TotalRestante);
                        }
                        //Antes, aqui, fazer a tolerância de 10 minutos entre um registro e outro.
                        if (!ST.PermiteRegistroTolerancia(HoraBatida, vwFrequenciaDia[0].HoraSaidaManha))
                            return string.Format("{0}, respeite o limite de 10 minutos entre as marcações!", PrimeiroNome);

                        else
                        {
                            if (adpFrequencia.UpdateEntradaTarde(HoraBatida, vwFrequenciaDia[0].TotalHorasDiaSegundos, IDUsuario, HoraBatida.Date.Date, IDVinculoUsuario) == 0)
                            {
                                return string.Format("{0}, banco de dados indisponível. Repita o processo.", PrimeiroNome);
                            }
                            else
                            {
                                return string.Format("{0}, entrada 2 efetivada.", PrimeiroNome);
                            }
                        }
                    }
                    //Aqui, saída da manhã. Fazer Somatória.
                    if (!vwFrequenciaDia[0].IsHoraEntraManhaNull())
                    {
                        //Aqui, saída da tarde. Fazer somatória.
                        if (!ST.PermiteRegistroTolerancia(HoraBatida, vwFrequenciaDia[0].HoraEntraManha))
                            return string.Format("{0}, respeite o limite de 10 minutos entre as marcações!", PrimeiroNome);

                        //Aqui, saída da manhã. Fazer Somatória.
                        if (adpFrequencia.UpdateSaidaManha(HoraBatida, ST.TotalDia(vwFrequenciaDia[0].HoraEntraManha, HoraBatida, null, null, TotalHoraDia), IDUsuario,
                            HoraBatida.Date.Date, IDVinculoUsuario) == 0)
                        {
                            return string.Format("{0}, banco de dados indisponível. Repita o processo.", PrimeiroNome);
                        }
                        else
                        {
                            //Comentado em 20/08/2018
                            //if (ST.JornadaIncompleta)
                            //{
                            //    //Retirar a frase por estar causando confusão no pessoal da SME.
                            //    return string.Format("{0}, jornada finalizada com a carga horária total incompleta!",PrimeiroNome);
                            //}
                            //else
                            //{
                            //    //Tratando aqui por enquanto.
                            //    if (TotalHoraDia != 8)
                            //    {
                            //        return string.Format("{0}, jornada finalizada com sucesso.",PrimeiroNome);
                            //    }
                            //    else
                            //    {
                            return string.Format("{0}, saída 1 efetivada.", PrimeiroNome);
                            //    }
                            //}
                        }
                    }
                    //Aqui, saída da tarde. Fazer somatória.
                    if (!vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                    {
                        //Aqui, saída da tarde. Fazer somatória.
                        if (!ST.PermiteRegistroTolerancia(HoraBatida, vwFrequenciaDia[0].HoraEntradaTarde))
                            return string.Format("{0}, respeite o limite de 10 minutos entre as marcações!", PrimeiroNome);

                        if (adpFrequencia.UpdateSaidaTarde(HoraBatida, ST.TotalDia(null, null, vwFrequenciaDia[0].HoraEntradaTarde, HoraBatida, TotalHoraDia), IDUsuario, vwFrequenciaDia[0].IDFrequencia) == 0)
                        {
                            return string.Format("{0}, banco de dados indisponível. Repita o processo.", PrimeiroNome);
                        }
                        else
                        {
                            if (ST.JornadaIncompleta)
                            {
                                return string.Format("{0}, jornada finalizada.", PrimeiroNome);
                            }
                            else
                            {
                                return string.Format("{0}, jornada finalizada com sucesso!", PrimeiroNome);
                            }
                        }
                    }

                    //19/09/2018 -- Caso a jornada esteja justificada, faz as entradas com um update.
                    if (!vwFrequenciaDia[0].IsIDMotivoFaltaNull() && vwFrequenciaDia[0].IsHoraEntraManhaNull() &&
                       vwFrequenciaDia[0].IsHoraSaidaManhaNull() && vwFrequenciaDia[0].IsHoraEntradaTardeNull()
                       && vwFrequenciaDia[0].IsHoraSaidaTardeNull())
                    {
                        if (HoraBatida.Hour <= 11)
                        {
                            if (adpFrequencia.UpdateEntradaManhaJust(HoraBatida, vwFrequenciaDia[0].IDFrequencia, IDUsuario) > 0)
                            {
                                return string.Format("{0}, entrada efetivada com sucesso.", PrimeiroNome);
                            }
                        }
                        else
                        {
                            if (adpFrequencia.UpdateEntradaTardeJust(HoraBatida, vwFrequenciaDia[0].IDFrequencia, IDUsuario) > 0)
                            {
                                return string.Format("{0}, entrada efetivada com sucesso.", PrimeiroNome);
                            }
                        }
                    }
                }

                #endregion

            }
            else
            {
                #region Registro de marcação de ponto de plantonistas
                LimiteMaximoPlantao = 25; //Limite de 2 horas entre um plantão e outro.
                //Procura por registros realizados no dia anterior em busca de plantões a serem finalizados
                try
                {

                    adpvwFrequenciaDia.Connection.Open();
                    if (!repeteProcedimento)
                        adpvwFrequenciaDia.FillIDVinculoUsuario(vwFrequenciaDia, IDVinculoUsuario, HoraBatida.Date.Date.AddDays(-1));
                    else
                    {
                        adpvwFrequenciaDia.FillIDVinculoUsuario(vwFrequenciaDia, IDVinculoUsuario, HoraBatida.Date.Date);
                    }
                    adpvwFrequenciaDia.Connection.Close();
                }
                catch (Exception ex)
                {
                    adpvwFrequenciaDia.Connection.Close();
                    UtilLog.EscreveLog(DateTime.Now.ToLongDateString() + " ERRO BATERPONTO LINHA 1637" + ex.Message);
                    return "Jornada de trabalho não identificada no banco de dados. Tente Novamente!";
                }

                if (vwFrequenciaDia.Rows.Count > 0) //se há registros analizar os tempos
                {

                    // 28/06/2019 Limitar a finalização de um plantão caso ele já esteja com
                    //Comparar com a escala ou o total do regime.
                    //Caso a carga horária já esteja completa, Começar outra.
                    //Caso já tenha ultrapassado o limite de finalizar a jornada, começar outra jornada.
                    //a carga completa. Dar início a um novo plantão.
                    //Acrescentar tolerância de 15 minutos no total geral


                    //Buscar por escalas cadastradas nessa data.

                    if (!repeteProcedimento)
                    {
                        if (!vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                        {
                            TotalJornada = HoraBatida.Subtract(vwFrequenciaDia[0].HoraEntradaTarde);

                            if (!vwFrequenciaDia[0].IsHoraSaidaTardeNull())
                            {
                                saida2Anterior = vwFrequenciaDia[0].HoraSaidaTarde;
                            }
                        }
                        else
                        {
                            TotalJornada = HoraBatida.Subtract(vwFrequenciaDia[0].HoraEntraManha);
                        }
                        DataSetPontoFrequencia.TbRegimeHorario_EscalaDataTable TBEscala =
                            new DataSetPontoFrequencia.TbRegimeHorario_EscalaDataTable();
                        DataSetPontoFrequenciaTableAdapters.TbRegimeHorario_EscalaTableAdapter adpEscala =
                            new DataSetPontoFrequenciaTableAdapters.TbRegimeHorario_EscalaTableAdapter();
                        adpEscala.FillDataEscolaIDvinculoUsuario(TBEscala, HoraBatida.Date.Date.AddDays(-1), IDVinculoUsuario);
                        //Aqui busca o total que deve ser cumprido
                        if (TBEscala.Rows.Count > 0)
                        {
                            TotalEscala = TBEscala[0].DataHorasSaida.Subtract(TBEscala[0].DataHoraEntrada);
                        }
                        else
                        {
                            TotalEscala = new TimeSpan(TotalHoraDia, 0, 0);
                        }

                        //Aqui a mudança, caso total escala + tolerância seja menor que o total jornada
                        //repeti rotina, senão, segue em frente.
                        if (TotalEscala != new TimeSpan(10, 0, 0))
                        {
                            if (TotalEscala.Add(new TimeSpan(0, 15, 0)) < TotalJornada)
                            {
                                repeteProcedimento = true;
                                //repete rotina
                                BaterPonto3(IDEmpresa, idsetorbatida, IDUsuario, HoraBatida,
                                 HoraEntradaManha, HoraSaidaManha, horaEntradaTarde, HoraSaidaTarde,
                                TotalHoraDia, Nome, PrimeiroNome, RegimePlantonista, IDVinculoUsuario);
                                return msg;
                            }
                        }
                        else// Se total for 10 horas entra os médicos e odontólogos
                        {
                            if (TotalEscala.Add(new TimeSpan(2, 15, 0)) < TotalJornada)
                            {
                                repeteProcedimento = true;
                                //repete rotina
                                BaterPonto3(IDEmpresa, idsetorbatida, IDUsuario, HoraBatida,
                                 HoraEntradaManha, HoraSaidaManha, horaEntradaTarde, HoraSaidaTarde,
                                TotalHoraDia, Nome, PrimeiroNome, RegimePlantonista, IDVinculoUsuario);
                                return msg;
                            }
                        }

                    }
                    else
                        repeteProcedimento = false;

                    //19/08/2018

                    //19/09/2018 -- Caso a jornada esteja justificada, faz as entradas com um update.
                    if (!vwFrequenciaDia[0].IsIDMotivoFaltaNull() && vwFrequenciaDia[0].IsHoraEntraManhaNull() &&
                       vwFrequenciaDia[0].IsHoraSaidaManhaNull() && vwFrequenciaDia[0].IsHoraEntradaTardeNull()
                       && vwFrequenciaDia[0].IsHoraSaidaTardeNull())
                    {
                        if (HoraBatida.Hour <= 11)
                        {
                            UtilLog.EscreveLog(DateTime.Now.ToLongDateString() + " ERRO BaterPonto LINHA 1654");
                            if (adpFrequencia.UpdateEntradaManhaJust(HoraBatida, vwFrequenciaDia[0].IDFrequencia, IDUsuario) > 0)
                            {
                                return string.Format("{0}, entrada efetivada com sucesso.", PrimeiroNome);
                            }
                        }
                        else
                        {

                            if (adpFrequencia.UpdateEntradaTardeJust(HoraBatida, vwFrequenciaDia[0].IDFrequencia, IDUsuario) > 0)
                            {
                                return string.Format("{0}, entrada efetivada com sucesso.", PrimeiroNome);
                            }
                        }
                    }
                    //Caso esteja justificado e os campos de entrada e saída sejam nulos tratar a entrada e a saída.


                    ////Se houver uma falta lançada, não registra frequência
                    //if (!vwFrequenciaDia[0].IsIDMotivoFaltaNull())
                    //{
                    //    if (FaltaLancada(vwFrequenciaDia[0].IDMotivoFalta)) // Se o motivo for 47, não bate ponto.
                    //        return string.Format("{0}, há uma falta lançada na data corrente. Impossível lançar o registro de sua marcação!", NomeUsuario);
                    //}

                    if ((vwFrequenciaDia[0].IsTotalHorasDiaSegundosNull()) ||
                        (vwFrequenciaDia[0].TotalHorasDiaSegundos == 0)) // Se o total estiver nulo. Verificar qual das entradas foi utiliza (Manhã ou tarde)
                    {
                        TimeSpan Resultado;

                        if (!vwFrequenciaDia[0].IsHoraEntraManhaNull())// se não for nulo manipula entrada manhã
                        {
                            Resultado = HoraBatida.Subtract(vwFrequenciaDia[0].HoraEntraManha);
                        }
                        else
                        {
                            Resultado = HoraBatida.Subtract(vwFrequenciaDia[0].HoraEntradaTarde);
                        }

                        H = Resultado.TotalMinutes;

                        if (H > ((TotalHoraDia + LimiteMaximoPlantao) * 60)) //Se maior que a soma do total + limite... Começa um novo dia
                        {
                            //Se verdadeiro, inicia nova jornada
                            try
                            {
                                adpvwFrequenciaDia.Connection.Open();
                                adpvwFrequenciaDia.FillIDVinculoUsuario(vwFrequenciaDia, IDVinculoUsuario, HoraBatida.Date.Date);
                                adpvwFrequenciaDia.Connection.Close();
                            }
                            catch (Exception ex)
                            {

                                adpvwFrequenciaDia.Connection.Close();
                                return "Jornada de trabalho não identificada no banco de dados. Tente Novamente!";
                            }
                        }
                    }
                    else
                    {
                        if ((!vwFrequenciaDia[0].IsTotalHorasDiaSegundosNull())
                            || (vwFrequenciaDia[0].TotalHorasDiaSegundos != 0))
                        {
                            SomaTolerancia ST = new SomaTolerancia();
                            DateTime Limite5;
                            if (!vwFrequenciaDia[0].IsHoraSaidaTardeNull())
                            {
                                Limite5 = vwFrequenciaDia[0].HoraSaidaTarde;
                            }
                            else
                            {
                                Limite5 = vwFrequenciaDia[0].HoraSaidaManha;
                            }
                            if (!ST.PermiteRegistroTolerancia5Min(HoraBatida, Limite5))
                            {
                                return string.Format("{0}, jornada finalizada. Intervalo de 5 minutos para iniciar nova jornada.", PrimeiroNome);
                            }
                            else //Pode finalizar a jornada começada no dia anterior. ou no mesmo dia.
                            {
                                if (!vwFrequenciaDia[0].IsHoraSaidaTardeNull())
                                {


                                    //01/07/2019 - finaliza jornada começada na entrada 2 que 
                                    //já foi finalizada mas está com a jornada incompleta.
                                    if (vwFrequenciaDia[0].IsHoraEntraManhaNull() &&
                                    !vwFrequenciaDia[0].IsHoraEntradaTardeNull()
                                    && !vwFrequenciaDia[0].IsTotalHorasDiaSegundosNull())
                                    {
                                        if (TotalEscala == new TimeSpan(10, 0, 0))// se for médico
                                        {
                                            TotalEscala = TotalEscala.Add(new TimeSpan(2, 15, 0));
                                        }
                                        else
                                            TotalEscala = TotalEscala.Add(new TimeSpan(0, 15, 0));

                                        if (TotalEscala > TotalJornada)
                                        {
                                            //se for menor prosseguir com a atualização do campo
                                            if (adpFrequencia.UpdateSaidaTarde(HoraBatida, TotalHorasDia(null, null, vwFrequenciaDia[0].HoraEntradaTarde, HoraBatida, "Tarde", vwFrequenciaDia[0].DTFrequencia.Date.Date), vwFrequenciaDia[0].IDUsuario, vwFrequenciaDia[0].IDFrequencia) == 0)
                                            {
                                                msg = "Nao Inserido";
                                            }
                                            else
                                                msg = "Horario";

                                            //Total da jornada

                                            //adpFrequencia.UpdateTotHoras(TotalHorasDia(vwFrequenciaDia[0].HoraEntraManha, HoraBatida, null, null, "Manha", vwFrequenciaDia[0].DTFrequencia.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);

                                            TotalHorasDia(null, null, vwFrequenciaDia[0].HoraEntradaTarde, HoraBatida, "Tarde", vwFrequenciaDia[0].DTFrequencia.Date.Date);

                                            switch (msg)
                                            {
                                                case "Add Noturno":
                                                    msg = String.Format("{0}, adicional noturno iniciado com sucesso.", PrimeiroNome.Trim());
                                                    break;
                                                case "Carga Total":
                                                    msg = String.Format("{0}, jornada finalizada.", PrimeiroNome.Trim());
                                                    break;
                                                case "Carga Incompleta":
                                                    msg = String.Format("{0}, jornada finalizada.", PrimeiroNome.Trim());//com a carga horária incompleta.
                                                    break;
                                                case "Nao Atualizado":
                                                    msg = String.Format("{0}, marcação não realizada. Repita o processo.", PrimeiroNome.Trim());
                                                    break;
                                            }

                                            return msg;

                                        }

                                    }

                                    //update saída da tarde.
                                    if (!VerificaIntervalodeRegistro(HoraBatida, vwFrequenciaDia[0].HoraEntradaTarde))
                                        return string.Format("{0}, respeite o intervalo de 15 minutos entre as marcações.", PrimeiroNome);

                                    //Se a jornada está completa, não deixar registrar novamente.
                                    TimeSpan totalhoras;
                                    totalhoras = HoraBatida.Subtract(vwFrequenciaDia[0].HoraEntradaTarde);

                                    if (totalhoras.Hours < TotalHoraDia)
                                    {

                                        if (adpFrequencia.UpdateSaidaTarde(HoraBatida, TotalHorasDia(null, null, vwFrequenciaDia[0].HoraEntradaTarde, HoraBatida, "Tarde", vwFrequenciaDia[0].DTFrequencia.Date.Date), vwFrequenciaDia[0].IDUsuario, vwFrequenciaDia[0].IDFrequencia) == 0)
                                        {
                                            msg = "Nao Inserido";
                                        }
                                        else
                                        {
                                            msg = "Horario";
                                        }

                                        //Total da jornada
                                        //adpFrequencia.UpdateTotHoras(TotalHorasDia(null, null, vwFrequenciaDia[0].HoraEntradaTarde, HoraBatida, "Tarde", vwFrequenciaDia[0].DTFrequencia.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);

                                        TotalHorasDia(null, null, vwFrequenciaDia[0].HoraEntradaTarde, HoraBatida, "Tarde", vwFrequenciaDia[0].DTFrequencia.Date.Date);

                                        switch (msg)
                                        {
                                            case "Add Noturno":
                                                msg = String.Format("{0}, adicional noturno iniciado com sucesso.", PrimeiroNome);
                                                break;
                                            case "Carga Total":
                                                msg = String.Format("{0}, jornada finalizada.", PrimeiroNome);
                                                break;
                                            case "Carga Incompleta":
                                                msg = String.Format("{0}, jornada finalizada.", PrimeiroNome);// com a carga horária incompleta.
                                                break;
                                            case "Nao Atualizado":
                                                msg = String.Format("{0}, marcação não realizada. Repita o processo.", PrimeiroNome);
                                                break;
                                        }
                                        return msg;
                                    }
                                }
                                //aqui tratar plantao iniciado na entrada 1 28/06/2019 e finalizado
                                //com a carga horária incompleta.
                                else if (!vwFrequenciaDia[0].IsHoraEntraManhaNull() &&
                                    vwFrequenciaDia[0].IsHoraEntradaTardeNull()
                                    && !vwFrequenciaDia[0].IsTotalHorasDiaSegundosNull())
                                {
                                    if (TotalEscala == new TimeSpan(10, 0, 0))// se for médico
                                    {
                                        TotalEscala = TotalEscala.Add(new TimeSpan(2, 15, 0));
                                    }
                                    else
                                        TotalEscala = TotalEscala.Add(new TimeSpan(0, 15, 0));

                                    if (TotalEscala > TotalJornada)
                                    {
                                        //se for menor prosseguir com a atualização do campo
                                        if (adpFrequencia.UpdateSaidaManha(HoraBatida, TotalHorasDia(vwFrequenciaDia[0].HoraEntraManha, HoraBatida, null, null, "Manha", vwFrequenciaDia[0].DTFrequencia.Date.Date), IDUsuario, vwFrequenciaDia[0].DTFrequencia.Date.Date, IDVinculoUsuario) == 0)
                                        {
                                            msg = "Nao Inserido";
                                        }
                                        else
                                            msg = "Horario";

                                        //Total da jornada

                                        //adpFrequencia.UpdateTotHoras(TotalHorasDia(vwFrequenciaDia[0].HoraEntraManha, HoraBatida, null, null, "Manha", vwFrequenciaDia[0].DTFrequencia.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);

                                        TotalHorasDia(vwFrequenciaDia[0].HoraEntraManha, HoraBatida, null, null, "Manha", vwFrequenciaDia[0].DTFrequencia.Date.Date);

                                        switch (msg)
                                        {
                                            case "Add Noturno":
                                                msg = String.Format("{0}, adicional noturno iniciado com sucesso.", PrimeiroNome.Trim());
                                                break;
                                            case "Carga Total":
                                                msg = String.Format("{0}, jornada finalizada.", PrimeiroNome.Trim());
                                                break;
                                            case "Carga Incompleta":
                                                msg = String.Format("{0}, jornada finalizada.", PrimeiroNome.Trim());//com a carga horária incompleta.
                                                break;
                                            case "Nao Atualizado":
                                                msg = String.Format("{0}, marcação não realizada. Repita o processo.", PrimeiroNome.Trim());
                                                break;
                                        }

                                        return msg;

                                    }

                                }
                                else
                                {
                                    Limite5 = vwFrequenciaDia[0].HoraSaidaManha;
                                }
                            }

                        }
                        //Procura por registros no dia corrente.
                        try
                        {
                            adpvwFrequenciaDia.Connection.Open();
                            adpvwFrequenciaDia.FillIDVinculoUsuario(vwFrequenciaDia, IDVinculoUsuario, HoraBatida.Date.Date);
                            adpvwFrequenciaDia.Connection.Close();
                        }
                        catch (Exception ex)
                        {
                            UtilLog.EscreveLog(DateTime.Now.ToLongDateString() + " ERRO BATERPONTO LINHA 1792" + ex.Message);
                            adpvwFrequenciaDia.Connection.Close();
                            return "Jornada de trabalho não identificada no banco de dados. Tente Novamente!";
                        }
                    }
                }
                else //Se não ... Start com o dia corrente
                {
                    //Procura por registros realizados no dia anterior em busca de plantões a serem finalizados
                    try
                    {
                        adpvwFrequenciaDia.Connection.Open();
                        adpvwFrequenciaDia.FillIDVinculoUsuario(vwFrequenciaDia, IDVinculoUsuario, HoraBatida.Date.Date);
                        adpvwFrequenciaDia.Connection.Close();
                    }
                    catch (Exception ex)
                    {
                        UtilLog.EscreveLog(DateTime.Now.ToLongDateString() + " ERRO BATERPONTO LINHA 1809 " + ex.Message);
                        adpvwFrequenciaDia.Connection.Close();
                        return "Jornada de trabalho não identificada no banco de dados. Tente Novamente!";
                    }
                }

                switch (vwFrequenciaDia.Rows.Count)
                {
                    case 0: //Caso não tenha registros no dia, iniciar jornada de plantão.
                        #region Inicio Nova Jornada
                        if (HoraBatida.Hour < 11) //caso antes das 11 iniciar o plantão na parte da manhã
                        {
                            //01/07/2019 -- Caso a jornada do dia anterior tenha sido finalizada,
                            if (saida2Anterior.HasValue)
                            {
                                TimeSpan? H = HoraBatida - saida2Anterior;
                                if (H < new TimeSpan(0, 5, 0))
                                {
                                    msg = string.Format("{0}, intervalo de 5 min. entre o fim de uma jornada e o início de outra.", PrimeiroNome);
                                    return msg;
                                }
                            }
                            //tolerâcia para começar outra jornada.

                            //Caso insert retorne 0, manda msg para repetir operação. Caso contrário segue rotina

                            if (adpFrequencia.Insert(IDUsuario, null, HoraBatida.Date, HoraBatida, null, null, null, null, null, HoraBatida.Month, HoraBatida.Year, IDEmpresa, IDVinculoUsuario) == 0)
                            {
                                msg = "Nao Inserido";
                            }
                            else
                            {
                                msg = "Horario";
                            }
                        }
                        else //Caso acima das 11. Registrar na parte da tarde.
                        {
                            //Caso insert retorne 0, manda msg para repetir operação. Caso contrário segue rotina

                            if (adpFrequencia.Insert(IDUsuario, null, HoraBatida.Date, null, null, HoraBatida, null, null, null, HoraBatida.Month, HoraBatida.Year, IDEmpresa, IDVinculoUsuario) == 0)
                            {
                                msg = "Nao Inserido";
                            }
                            else
                            {
                                msg = "Horario";
                            }
                        }

                        switch (msg)
                        {
                            case "Horario":
                                msg = String.Format("{0}, entrada efetivada.", PrimeiroNome.Trim());
                                break;
                            case "Nao Inserido":
                                msg = String.Format("{0}, marcação não efetivada. Favor repetir a operação.", PrimeiroNome.Trim());
                                break;
                        }

                        #endregion
                        break;
                    case 1:

                        // 19/09/2018 - Caso tenha registro e estiver justificado. Trata a entrada 1 e entrada 2
                        //19/08/2018

                        //19/09/2018 -- Caso a jornada esteja justificada, faz as entradas com um update.
                        if (!vwFrequenciaDia[0].IsIDMotivoFaltaNull() && vwFrequenciaDia[0].IsHoraEntraManhaNull() &&
                           vwFrequenciaDia[0].IsHoraSaidaManhaNull() && vwFrequenciaDia[0].IsHoraEntradaTardeNull()
                           && vwFrequenciaDia[0].IsHoraSaidaTardeNull())
                        {
                            if (HoraBatida.Hour <= 11)
                            {

                                if (adpFrequencia.UpdateEntradaManhaJust(HoraBatida, vwFrequenciaDia[0].IDFrequencia, IDUsuario) > 0)
                                {
                                    return string.Format("{0}, entrada efetivada com sucesso.", PrimeiroNome);
                                }
                            }
                            else
                            {

                                if (adpFrequencia.UpdateEntradaTardeJust(HoraBatida, vwFrequenciaDia[0].IDFrequencia, IDUsuario) > 0)
                                {
                                    return string.Format("{0}, entrada efetivada com sucesso.", PrimeiroNome);
                                }
                            }
                        }
                        //--------------------------------------------------------------------------------------
                        //Tratativas de término da jornada e adiconal noturno.
                        //Adicional Noturno
                        if (vwFrequenciaDia[0].IsTotalHorasDiaSegundosNull() && (HoraBatida.Hour >= 21 && HoraBatida.Hour <= 23))
                        {
                            //Para cair aqui, já tem que haver dado entrada de um plantão.
                            TimeSpan totalhoras;

                            if (!vwFrequenciaDia[0].IsHoraEntraManhaNull())
                            {

                                //Verifica minutos entre a entrada e saída

                                if (!VerificaIntervalodeRegistro(HoraBatida, vwFrequenciaDia[0].HoraEntraManha))
                                    return string.Format("{0}, respeite o intervalo de 15 minutos entre as marcações.", PrimeiroNome);

                                totalhoras = HoraBatida.Subtract(vwFrequenciaDia[0].HoraEntraManha);

                                totalhoras += TimeSpan.Parse("00:15:00"); //Add os 15 minutos de tolerância;

                                if ((totalhoras.TotalHours >= TotalHoraDia) || (totalhoras.TotalHours >= 20))
                                {

                                    if (adpFrequencia.UpdateSaidaManha(HoraBatida, TotalHorasDia(vwFrequenciaDia[0].HoraEntraManha, HoraBatida, null, null, "Manha", vwFrequenciaDia[0].DTFrequencia.Date.Date), IDUsuario, vwFrequenciaDia[0].DTFrequencia.Date.Date, IDVinculoUsuario) == 0)
                                    {
                                        msg = "Nao Inserido";
                                    }
                                    else
                                        msg = "Horario";

                                    //Total da jornada

                                    //adpFrequencia.UpdateTotHoras(TotalHorasDia(vwFrequenciaDia[0].HoraEntraManha, HoraBatida, null, null, "Manha", vwFrequenciaDia[0].DTFrequencia.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);

                                    TotalHorasDia(vwFrequenciaDia[0].HoraEntraManha, HoraBatida, null, null, "Manha", vwFrequenciaDia[0].DTFrequencia.Date.Date);

                                    switch (msg)
                                    {
                                        case "Add Noturno":
                                            msg = String.Format("{0}, adicional noturno iniciado com sucesso.", PrimeiroNome.Trim());
                                            break;
                                        case "Carga Total":
                                            msg = String.Format("{0}, jornada finalizada.", PrimeiroNome.Trim());
                                            break;
                                        case "Carga Incompleta":
                                            msg = String.Format("{0}, jornada finalizada.", PrimeiroNome.Trim());//com a carga horária incompleta.
                                            break;
                                        case "Nao Atualizado":
                                            msg = String.Format("{0}, marcação não realizada. Repita o processo.", PrimeiroNome.Trim());
                                            break;
                                    }
                                }
                                else
                                {

                                    if (adpFrequencia.UpdateEntradaAddNoturno(HoraBatida, vwFrequenciaDia[0].IDFrequencia, vwFrequenciaDia[0].IDVinculoUsuario) > 0)
                                        msg = string.Format("{0}, adicional noturno informado com sucesso!", PrimeiroNome);
                                    else
                                        msg = string.Format("{0}, houve falha ao informar o adicional noturno. Tente Novamente!", PrimeiroNome);

                                    return msg;
                                }
                            }
                            else if (!vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                            {

                                if (!VerificaIntervalodeRegistro(HoraBatida, vwFrequenciaDia[0].HoraEntradaTarde))
                                    return string.Format("{0}, respeite o intervalo de 15 minutos entre as marcações.", PrimeiroNome);

                                totalhoras = HoraBatida.Subtract(vwFrequenciaDia[0].HoraEntradaTarde);

                                totalhoras += TimeSpan.Parse("00:15:00"); //Add os 15 minutos de tolerância;
                                                                          //COmentado em 04/09/2018 -- tratativa de ad. noturno direto na rotina.
                                                                          //if (totalhoras.TotalHours >= vwUsuarioGrid[vinculo].TotHorasDiarias || (totalhoras.TotalHours >= 20))
                                                                          //{

                                if (adpFrequencia.UpdateSaidaTarde(HoraBatida, TotalHorasDia(null, null, vwFrequenciaDia[0].HoraEntradaTarde, HoraBatida, "Tarde", vwFrequenciaDia[0].DTFrequencia.Date.Date), vwFrequenciaDia[0].IDUsuario, vwFrequenciaDia[0].IDFrequencia) == 0)
                                {
                                    msg = "Nao Inserido";
                                }
                                else
                                {
                                    msg = "Horario";
                                }

                                //Total da jornada
                                //adpFrequencia.UpdateTotHoras(TotalHorasDia(null, null, vwFrequenciaDia[0].HoraEntradaTarde, HoraBatida, "Tarde", vwFrequenciaDia[0].DTFrequencia.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);

                                TotalHorasDia(null, null, vwFrequenciaDia[0].HoraEntradaTarde, HoraBatida, "Tarde", vwFrequenciaDia[0].DTFrequencia.Date.Date);

                                switch (msg)
                                {
                                    case "Add Noturno":
                                        msg = String.Format("{0}, adicional noturno iniciado com sucesso.", PrimeiroNome);
                                        break;
                                    case "Carga Total":
                                        msg = String.Format("{0}, jornada finalizada.", PrimeiroNome);
                                        break;
                                    case "Carga Incompleta":
                                        msg = String.Format("{0}, jornada finalizada.", PrimeiroNome);//com a carga horária incompleta.
                                        break;
                                    case "Nao Atualizado":
                                        msg = String.Format("{0}, marcação não realizada. Repita o processo.", PrimeiroNome);
                                        break;
                                }

                                //}
                                //else
                                //{
                                //    if (adpFrequencia.UpdateEntradaAddNoturno(HoraBatida, vwFrequenciaDia[0].IDFrequencia, vwFrequenciaDia[0].IDVinculoUsuario) > 0)
                                //        msg = string.Format("{0}, adicional noturno informado com sucesso!", PrimeiroNome);
                                //    else
                                //        msg = string.Format("{0}, houve falha ao informar o adicional noturno. Tente Novamente!", PrimeiroNome);

                                //    return msg;
                                //}
                            }
                        }
                        else
                        {
                            //Aqui, update do fim da jornada e Total da mesma.
                            if (!vwFrequenciaDia[0].IsHoraEntraManhaNull())
                            {

                                if (!VerificaIntervalodeRegistro(HoraBatida, vwFrequenciaDia[0].HoraEntraManha))
                                    return string.Format("{0}, respeite o intervalo de 15 minutos entre as marcações.", PrimeiroNome);
                                UtilLog.EscreveLog(DateTime.Now.ToLongDateString() + " ERRO BaterPonto LINHA 2011");
                                if (adpFrequencia.UpdateSaidaManha(HoraBatida, TotalHorasDia(vwFrequenciaDia[0].HoraEntraManha, HoraBatida, null, null, "Manha", vwFrequenciaDia[0].DTFrequencia.Date.Date), IDUsuario, vwFrequenciaDia[0].DTFrequencia.Date.Date, IDVinculoUsuario) == 0)
                                {
                                    msg = "Nao Inserido";
                                }
                                else
                                    msg = "Horario";

                                //Total da jornada

                                //adpFrequencia.UpdateTotHoras(TotalHorasDia(vwFrequenciaDia[0].HoraEntraManha, HoraBatida, null, null, "Manha", vwFrequenciaDia[0].DTFrequencia.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);

                                TotalHorasDia(vwFrequenciaDia[0].HoraEntraManha, HoraBatida, null, null, "Manha", vwFrequenciaDia[0].DTFrequencia.Date.Date);
                            }
                            else
                            {

                                if (!VerificaIntervalodeRegistro(HoraBatida, vwFrequenciaDia[0].HoraEntradaTarde))
                                    return string.Format("{0}, respeite o intervalo de 15 minutos entre as marcações.", PrimeiroNome);

                                if (adpFrequencia.UpdateSaidaTarde(HoraBatida, TotalHorasDia(null, null, vwFrequenciaDia[0].HoraEntradaTarde, HoraBatida, "Tarde", vwFrequenciaDia[0].DTFrequencia.Date.Date), IDUsuario, vwFrequenciaDia[0].IDFrequencia) == 0)
                                {
                                    msg = "Nao Inserido";
                                }
                                else
                                    msg = "Horario";

                                //Total da jornada
                                //adpFrequencia.UpdateTotHoras(TotalHorasDia(null, null, vwFrequenciaDia[0].HoraEntradaTarde,HoraBatida, "Tarde", vwFrequenciaDia[0].DTFrequencia.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);

                                TotalHorasDia(null, null, vwFrequenciaDia[0].HoraEntradaTarde, HoraBatida, "Tarde", vwFrequenciaDia[0].DTFrequencia.Date.Date);
                            }

                            switch (msg)
                            {
                                case "Add Noturno":
                                    msg = String.Format("{0}, adicional noturno iniciado com sucesso.", PrimeiroNome);
                                    break;
                                case "Carga Total":
                                    msg = String.Format("{0}, jornada finalizada.", PrimeiroNome);
                                    break;
                                case "Carga Incompleta":
                                    msg = String.Format("{0}, jornada finalizada.", PrimeiroNome);// com a carga horária incompleta.
                                    break;
                                case "Nao Atualizado":
                                    msg = String.Format("{0}, marcação não realizada. Repita o processo.", PrimeiroNome);
                                    break;
                            }
                        }

                        break;
                }
                #endregion
            }

            return msg;
        }

        public string BaterPonto3(int IDEmpresa, int idsetorbatida, int IDUsuario, DateTime HoraBatida,
        string HoraEntradaManha, string HoraSaidaManha, string horaEntradaTarde, string HoraSaidaTarde,
        int TotalHoraDia, string Nome, string PrimeiroNome, bool RegimePlantonista, long IDVinculoUsuario)
        {


            if (!RegimePlantonista) //Se não for plantonista, lets go. Senão. abaixo.
            {
                #region Registro de marcação regime de expediente

                //Não deixar registrar sem ser no seu órgão de origem
                //IDEmpresa = ValidaVinculo(IDUsuario, IDEmpresa, "");

                //COMENTADO EM 20/03/2019 - UM NOVO MÉTODO PRA NÃO DEIXAR O VÍNCULO.
                //adpvwUsuarioGrid.Connection.Open();
                //vwUsuarioGrid.Constraints.Clear();
                //adpvwUsuarioGrid.FillIDEmpresaUsuario(vwUsuarioGrid, IDEmpresa, IDUsuario);
                //adpvwUsuarioGrid.Connection.Close();
                //if (vwUsuarioGrid.Rows.Count == 0)

                PreencheTabela PT = new PreencheTabela();
                if (PT.GetIDEmpresaVinculoScalar(IDVinculoUsuario) != IDEmpresa)
                {
                    return string.Format("{0}, faça sua marcação em seu órgão de lotação.", PrimeiroNome);
                }

                //PRÓXIMOS PASSOS, VERIFIFCAR NAS SAÍDAS SE O HORÁRIO QUE ESTÁ VINDO É MAIOR QUE O 
                //ULTIMO REGISTRO (Saida 1 e entrada 1).

                //13/07/2018 - Início da nova rotina de registro de ponto.
                SomaTolerancia ST = new SomaTolerancia();

                //Primeiro, se a defasagem > que 5 dias. Retorna o texto abaixo.
                TimeSpan TotDias = DateTime.Now - HoraBatida;

                if (TotDias.TotalDays > 5)
                {
                    return string.Format("{0}, registro com mais de 5 dias ({1}) de defasagem. Procure o seu gestor!", NomeUsuario.Trim(), HoraBatida.ToShortDateString());
                }

                //Verifica se há afastamento em curso
                //Por enquanto FIxo de 2 horas ... Valor Inteiro... Após atrelar isso ao cadastro de empresas
                //LimiteMaximoPlantao = 2;

                //try
                //{
                //    adpFeriasFrequencia.Connection.Open();
                //    adpFeriasFrequencia.FillByVerificaFeriasCorrente(TBFeriasFrequencia, HoraBatida.Date, IDUsuario, IDVinculoUsuario); // Verifica férias corrente
                //    adpFeriasFrequencia.Connection.Close();
                //}
                //catch
                //{
                //    adpFeriasFrequencia.Connection.Close();
                //}

                //if (TBFeriasFrequencia.Rows.Count > 0)
                if (PT.GetVerificaSeEstaDeFerias(IDUsuario, IDVinculoUsuario, HoraBatida.Date))
                {
                    return string.Format("{0}, Férias/Licença em vigência. Procure sua chefia imediata.", PrimeiroNome);
                }

                if (HoraBatida.Hour >= 0 && HoraBatida.Hour <= 2 && IDEmpresa == 50)
                {
                    try
                    {

                        adpvwFrequenciaDia.Connection.Open();
                        adpvwFrequenciaDia.FillIDVinculoUsuario(vwFrequenciaDia, IDVinculoUsuario, HoraBatida.Date.Date.AddDays(-1));
                        adpvwFrequenciaDia.Connection.Close();

                        //Caso tenho um registro do dia anterior, finaliza a saída 2.
                        //Total Horas

                        if (vwFrequenciaDia.Rows.Count > 0)
                        {
                            //update saída tarde com o calculos de somatória.

                            if (adpFrequencia.UpdateSaidaTarde(HoraBatida,
                                ST.TotalDia(null, null,
                                vwFrequenciaDia[0].HoraEntradaTarde, HoraBatida, TotalHoraDia)
                                , vwFrequenciaDia[0].IDUsuario, vwFrequenciaDia[0].IDFrequencia) == 0)
                            {
                                return string.Format("{0}, banco de dados indisponível. Repita o processo.", PrimeiroNome);
                            }
                            else
                            {
                                //Identificar a jornada  com a carga horária incompleta.
                                if (ST.JornadaIncompleta)// Se false, não completou a jornada do dia.
                                {
                                    return string.Format("{0}, jornada finalizada com a sucesso.", PrimeiroNome); //jornada finalizada com a carga horária total incompleta!
                                }
                                else
                                    return string.Format("{0}, jornada finalizada com sucesso!", PrimeiroNome);

                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        adpvwFrequenciaDia.Connection.Close();
                        UtilLog.EscreveLog(DateTime.Now.ToLongDateString() + " ERRO BATERPONTO LINHA 1400" + ex.Message);
                        return "Jornada de trabalho não identificada no banco de dados. Tente Novamente!";
                    }
                }
                else
                {
                    //Busca por registros no dia.
                    try
                    {

                        adpvwFrequenciaDia.Connection.Open();
                        adpvwFrequenciaDia.FillIDVinculoUsuario(vwFrequenciaDia, IDVinculoUsuario, HoraBatida.Date.Date);
                        adpvwFrequenciaDia.Connection.Close();
                    }
                    catch (Exception ex)
                    {
                        adpvwFrequenciaDia.Connection.Close();
                        UtilLog.EscreveLog(DateTime.Now.ToLongDateString() + " ERRO BATERPONTO LINHA 1416" + ex.Message);
                        return "Jornada de trabalho não identificada no banco de dados. Tente Novamente!";
                    }
                }

                //Se não há registros, faz insert na entrada 1 ou entrada 2. Caso contrário, faz o tratamento das saídas.
                if (vwFrequenciaDia.Rows.Count == 0)
                {
                    //Se => 12 entrada 2, senão entrada 1
                    if (HoraBatida.Hour < 11) ///Entrada 1
                    {

                        if (adpFrequencia.Insert(IDUsuario, null, HoraBatida.Date, HoraBatida, null, null, null, null, null, HoraBatida.Month, HoraBatida.Year, IDEmpresa, IDVinculoUsuario) == 0)
                        {
                            return string.Format("{0}, banco de dados indisponível. Repita o processo.", PrimeiroNome);
                        }
                        else
                        {
                            if (ST.Tolerancia(Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.ToShortDateString(), HoraEntradaManha)), HoraBatida))
                            {
                                return string.Format("{0}, entrada 1 efetivada.", PrimeiroNome);
                            }
                            else
                                return string.Format("{0}, entrada 1 efetivada.", PrimeiroNome);//entrada 1 efetivada com atraso superior a 15 min.
                        }
                    } //Entrada 2
                    else
                    {
                        //Aqui, se for p 8 horas mandar a entrada da manhã como padrão de tolerância. Senão, mandar a da tarde.

                        if (adpFrequencia.Insert(IDUsuario, null, HoraBatida.Date, null, null, HoraBatida, null, null, null, HoraBatida.Month, HoraBatida.Year, IDEmpresa, IDVinculoUsuario) == 0)
                        {
                            return string.Format("{0}, banco de dados indisponível. Repita o processo.", PrimeiroNome);
                        }
                        else
                        {
                            if (TotalHoraDia == 8) // Se for 8 mandar a entrada da manhã. Senão. segue normal.
                            {
                                if (ST.Tolerancia(Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.ToShortDateString(), horaEntradaTarde)), HoraBatida))
                                {
                                    return string.Format("{0}, entrada 1 efetivada.", PrimeiroNome);
                                }
                                else
                                    return string.Format("{0}, entrada 2 efetivada.", PrimeiroNome);// entrada 2 efetivada com atraso superior a 15 min.
                            }
                            else
                            {
                                if (ST.Tolerancia(Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.ToShortDateString(), HoraPadraoEntradaTarde)), HoraBatida))
                                {
                                    return string.Format("{0}, entrada 2 efetivada.", PrimeiroNome);
                                }
                                else
                                    return string.Format("{0}, entrada 2 efetivada.", PrimeiroNome);//entrada 2 efetivada com atraso superior a 15 min.
                            }
                        }

                    }
                } //Parte das Saídas
                else //Saídas 1 e 2 . Aqui vem a principal novidade.
                {
                    //Primeiro. Trata a saída 2 caso todos os campos não estejam nulos.

                    if (!vwFrequenciaDia[0].IsHoraEntraManhaNull() && !vwFrequenciaDia[0].IsHoraSaidaManhaNull() && !vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                    {
                        if (HoraBatida < vwFrequenciaDia[0].HoraEntradaTarde)
                        {
                            return string.Format("{0}, horário de saída menor que a entrada. Verifique a hora informada.", PrimeiroNome);
                        }

                        //update saída tarde com o calculos de somatória.

                        if (adpFrequencia.UpdateSaidaTarde(HoraBatida,
                            ST.TotalDia(vwFrequenciaDia[0].HoraEntraManha, vwFrequenciaDia[0].HoraSaidaManha,
                            vwFrequenciaDia[0].HoraEntradaTarde, HoraBatida, TotalHoraDia)
                            , vwFrequenciaDia[0].IDUsuario, vwFrequenciaDia[0].IDFrequencia) == 0)
                        {
                            return string.Format("{0}, banco de dados indisponível. Repita o processo.", PrimeiroNome);
                        }
                        else
                        {
                            //Identificar a jornada  com a carga horária incompleta.
                            if (ST.JornadaIncompleta)// Se false, não completou a jornada do dia.
                            {
                                return string.Format("{0}, jornada finalizada.", PrimeiroNome);// com a carga horária total incompleta!
                            }
                            else
                                return string.Format("{0}, jornada finalizada com sucesso!", PrimeiroNome);

                        }
                    }
                    //Aqui, entrada da tarde. Tratar aqui a questão do almoço.
                    if (!vwFrequenciaDia[0].IsHoraEntraManhaNull() && !vwFrequenciaDia[0].IsHoraSaidaManhaNull())
                    {

                        //Aqui, entrada da tarde. Tratar aqui a questão do almoço.
                        if (!ST.PermiteRegistroAlmoco(HoraBatida, vwFrequenciaDia[0].HoraSaidaManha))
                        {
                            return string.Format("{0}, horário de almoço, faça sua marcação em {1}", PrimeiroNome, ST.TotalRestante);
                        }
                        //Antes, aqui, fazer a tolerância de 10 minutos entre um registro e outro.
                        if (!ST.PermiteRegistroTolerancia(HoraBatida, vwFrequenciaDia[0].HoraSaidaManha))
                            return string.Format("{0}, respeite o limite de 10 minutos entre as marcações!", PrimeiroNome);

                        else
                        {

                            if (adpFrequencia.UpdateEntradaTarde(HoraBatida, vwFrequenciaDia[0].TotalHorasDiaSegundos, IDUsuario, HoraBatida.Date.Date, IDVinculoUsuario) == 0)
                            {
                                return string.Format("{0}, banco de dados indisponível. Repita o processo.", PrimeiroNome);
                            }
                            else
                            {
                                return string.Format("{0}, entrada 2 efetivada.", PrimeiroNome);
                            }
                        }
                    }
                    //Aqui, saída da manhã. Fazer Somatória.
                    if (!vwFrequenciaDia[0].IsHoraEntraManhaNull())
                    {
                        //Aqui, saída da tarde. Fazer somatória.
                        if (!ST.PermiteRegistroTolerancia(HoraBatida, vwFrequenciaDia[0].HoraEntraManha))
                            return string.Format("{0}, respeite o limite de 10 minutos entre as marcações!", PrimeiroNome);

                        //Aqui, saída da manhã. Fazer Somatória.
                        UtilLog.EscreveLog(DateTime.Now.ToLongDateString() + " ERRO BaterPonto LINHA 1542");
                        if (adpFrequencia.UpdateSaidaManha(HoraBatida, ST.TotalDia(vwFrequenciaDia[0].HoraEntraManha, HoraBatida, null, null, TotalHoraDia), IDUsuario,
                            HoraBatida.Date.Date, IDVinculoUsuario) == 0)
                        {
                            return string.Format("{0}, banco de dados indisponível. Repita o processo.", PrimeiroNome);
                        }
                        else
                        {
                            //Comentado em 20/08/2018
                            //if (ST.JornadaIncompleta)
                            //{
                            //    //Retirar a frase por estar causando confusão no pessoal da SME.
                            //    return string.Format("{0}, jornada finalizada com a carga horária total incompleta!",PrimeiroNome);
                            //}
                            //else
                            //{
                            //    //Tratando aqui por enquanto.
                            //    if (TotalHoraDia != 8)
                            //    {
                            //        return string.Format("{0}, jornada finalizada com sucesso.",PrimeiroNome);
                            //    }
                            //    else
                            //    {
                            return string.Format("{0}, saída 1 efetivada.", PrimeiroNome);
                            //    }
                            //}
                        }
                    }
                    //Aqui, saída da tarde. Fazer somatória.
                    if (!vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                    {
                        //Aqui, saída da tarde. Fazer somatória.
                        if (!ST.PermiteRegistroTolerancia(HoraBatida, vwFrequenciaDia[0].HoraEntradaTarde))
                            return string.Format("{0}, respeite o limite de 10 minutos entre as marcações!", PrimeiroNome);

                        if (adpFrequencia.UpdateSaidaTarde(HoraBatida, ST.TotalDia(null, null, vwFrequenciaDia[0].HoraEntradaTarde, HoraBatida, TotalHoraDia), IDUsuario, vwFrequenciaDia[0].IDFrequencia) == 0)
                        {
                            return string.Format("{0}, banco de dados indisponível. Repita o processo.", PrimeiroNome);
                        }
                        else
                        {
                            if (ST.JornadaIncompleta)
                            {
                                return string.Format("{0}, jornada finalizada.", PrimeiroNome);// com a carga horária total incompleta!
                            }
                            else
                            {
                                return string.Format("{0}, jornada finalizada com sucesso!", PrimeiroNome);
                            }
                        }
                    }

                    //19/09/2018 -- Caso a jornada esteja justificada, faz as entradas com um update.
                    if (!vwFrequenciaDia[0].IsIDMotivoFaltaNull() && vwFrequenciaDia[0].IsHoraEntraManhaNull() &&
                       vwFrequenciaDia[0].IsHoraSaidaManhaNull() && vwFrequenciaDia[0].IsHoraEntradaTardeNull()
                       && vwFrequenciaDia[0].IsHoraSaidaTardeNull())
                    {
                        if (HoraBatida.Hour <= 11)
                        {

                            if (adpFrequencia.UpdateEntradaManhaJust(HoraBatida, vwFrequenciaDia[0].IDFrequencia, IDUsuario) > 0)
                            {
                                return string.Format("{0}, entrada efetivada com sucesso.", PrimeiroNome);
                            }
                        }
                        else
                        {

                            if (adpFrequencia.UpdateEntradaTardeJust(HoraBatida, vwFrequenciaDia[0].IDFrequencia, IDUsuario) > 0)
                            {
                                return string.Format("{0}, entrada efetivada com sucesso.", PrimeiroNome);
                            }
                        }
                    }

                    //01/04/2019 -- TRATAR AQUI AS OPÇÕES QUANDO O REGISTRO ESTIVER COM PEDIDO DE JUSTIFICATIVA OU
                    // COM O PEDIDO APAGADO E A PESSOA FOR TENTAR REGISTRAR O PONTO.
                }

                #endregion
            }
            else
            {
                #region Registro de marcação de ponto de plantonistas
                LimiteMaximoPlantao = 25; //Limite de 2 horas entre um plantão e outro.
                //Procura por registros realizados no dia anterior em busca de plantões a serem finalizados
                try
                {

                    adpvwFrequenciaDia.Connection.Open();
                    if(!repeteProcedimento)
                        adpvwFrequenciaDia.FillIDVinculoUsuario(vwFrequenciaDia, IDVinculoUsuario, HoraBatida.Date.Date.AddDays(-1));
                    else
                    {
                        adpvwFrequenciaDia.FillIDVinculoUsuario(vwFrequenciaDia, IDVinculoUsuario, HoraBatida.Date.Date);
                    }
                    adpvwFrequenciaDia.Connection.Close();
                }
                catch (Exception ex)
                {
                    adpvwFrequenciaDia.Connection.Close();
                    UtilLog.EscreveLog(DateTime.Now.ToLongDateString() + " ERRO BATERPONTO LINHA 1637" + ex.Message);
                    return "Jornada de trabalho não identificada no banco de dados. Tente Novamente!";
                }

                if (vwFrequenciaDia.Rows.Count > 0) //se há registros analizar os tempos
                {

                    // 28/06/2019 Limitar a finalização de um plantão caso ele já esteja com
                    //Comparar com a escala ou o total do regime.
                    //Caso a carga horária já esteja completa, Começar outra.
                    //Caso já tenha ultrapassado o limite de finalizar a jornada, começar outra jornada.
                    //a carga completa. Dar início a um novo plantão.
                    //Acrescentar tolerância de 15 minutos no total geral


                    //Buscar por escalas cadastradas nessa data.

                    if(!repeteProcedimento)
                    {
                        if (!vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                        {
                            TotalJornada = HoraBatida.Subtract(vwFrequenciaDia[0].HoraEntradaTarde);

                            if(!vwFrequenciaDia[0].IsHoraSaidaTardeNull())
                            {
                                saida2Anterior = vwFrequenciaDia[0].HoraSaidaTarde;
                            }
                        }
                        else
                        {
                            TotalJornada = HoraBatida.Subtract(vwFrequenciaDia[0].HoraEntraManha);
                        }
                        DataSetPontoFrequencia.TbRegimeHorario_EscalaDataTable TBEscala =
                            new DataSetPontoFrequencia.TbRegimeHorario_EscalaDataTable();
                        DataSetPontoFrequenciaTableAdapters.TbRegimeHorario_EscalaTableAdapter adpEscala =
                            new DataSetPontoFrequenciaTableAdapters.TbRegimeHorario_EscalaTableAdapter();
                        adpEscala.FillDataEscolaIDvinculoUsuario(TBEscala, HoraBatida.Date.Date.AddDays(-1), IDVinculoUsuario);
                        //Aqui busca o total que deve ser cumprido
                        if (TBEscala.Rows.Count > 0)
                        {
                            TotalEscala = TBEscala[0].DataHorasSaida.Subtract(TBEscala[0].DataHoraEntrada);
                        }
                        else
                        {
                            TotalEscala = new TimeSpan(TotalHoraDia, 0, 0);
                        }

                        //Aqui a mudança, caso total escala + tolerância seja menor que o total jornada
                        //repeti rotina, senão, segue em frente.
                        if(TotalEscala != new TimeSpan(10,0,0))
                        {
                            if (TotalEscala.Add(new TimeSpan(0, 15, 0)) < TotalJornada)
                            {
                                repeteProcedimento = true;
                                //repete rotina
                                BaterPonto3(IDEmpresa, idsetorbatida, IDUsuario, HoraBatida,
                                 HoraEntradaManha, HoraSaidaManha, horaEntradaTarde, HoraSaidaTarde,
                                TotalHoraDia, Nome, PrimeiroNome, RegimePlantonista, IDVinculoUsuario);
                                return msg;
                            }
                        }
                        else// Se total for 10 horas entra os médicos e odontólogos
                        {
                            if (TotalEscala.Add(new TimeSpan(2, 15, 0)) < TotalJornada)
                            {
                                repeteProcedimento = true;
                                //repete rotina
                                BaterPonto3(IDEmpresa, idsetorbatida, IDUsuario, HoraBatida,
                                 HoraEntradaManha, HoraSaidaManha, horaEntradaTarde, HoraSaidaTarde,
                                TotalHoraDia, Nome, PrimeiroNome, RegimePlantonista, IDVinculoUsuario);
                                return msg;
                            }
                        }
                    }
                    else
                        repeteProcedimento = false;

                    //19/08/2018

                    //19/09/2018 -- Caso a jornada esteja justificada, faz as entradas com um update.
                    if (!vwFrequenciaDia[0].IsIDMotivoFaltaNull() && vwFrequenciaDia[0].IsHoraEntraManhaNull() &&
                       vwFrequenciaDia[0].IsHoraSaidaManhaNull() && vwFrequenciaDia[0].IsHoraEntradaTardeNull()
                       && vwFrequenciaDia[0].IsHoraSaidaTardeNull())
                    {
                        if (HoraBatida.Hour <= 11)
                        {
                            UtilLog.EscreveLog(DateTime.Now.ToLongDateString() + " ERRO BaterPonto LINHA 1654");
                            if (adpFrequencia.UpdateEntradaManhaJust(HoraBatida, vwFrequenciaDia[0].IDFrequencia, IDUsuario) > 0)
                            {
                                return string.Format("{0}, entrada efetivada com sucesso.", PrimeiroNome);
                            }
                        }
                        else
                        {

                            if (adpFrequencia.UpdateEntradaTardeJust(HoraBatida, vwFrequenciaDia[0].IDFrequencia, IDUsuario) > 0)
                            {
                                return string.Format("{0}, entrada efetivada com sucesso.", PrimeiroNome);
                            }
                        }
                    }
                    //Caso esteja justificado e os campos de entrada e saída sejam nulos tratar a entrada e a saída.


                    ////Se houver uma falta lançada, não registra frequência
                    //if (!vwFrequenciaDia[0].IsIDMotivoFaltaNull())
                    //{
                    //    if (FaltaLancada(vwFrequenciaDia[0].IDMotivoFalta)) // Se o motivo for 47, não bate ponto.
                    //        return string.Format("{0}, há uma falta lançada na data corrente. Impossível lançar o registro de sua marcação!", NomeUsuario);
                    //}

                    if ((vwFrequenciaDia[0].IsTotalHorasDiaSegundosNull()) || 
                        (vwFrequenciaDia[0].TotalHorasDiaSegundos == 0)) // Se o total estiver nulo. Verificar qual das entradas foi utiliza (Manhã ou tarde)
                    {
                        TimeSpan Resultado;

                        if (!vwFrequenciaDia[0].IsHoraEntraManhaNull())// se não for nulo manipula entrada manhã
                        {
                            Resultado = HoraBatida.Subtract(vwFrequenciaDia[0].HoraEntraManha);
                        }
                        else
                        {
                            Resultado = HoraBatida.Subtract(vwFrequenciaDia[0].HoraEntradaTarde);
                        }

                        H = Resultado.TotalMinutes;

                        if (H > ((TotalHoraDia + LimiteMaximoPlantao) * 60)) //Se maior que a soma do total + limite... Começa um novo dia
                        {
                            //Se verdadeiro, inicia nova jornada
                            try
                            {
                                adpvwFrequenciaDia.Connection.Open();
                                adpvwFrequenciaDia.FillIDVinculoUsuario(vwFrequenciaDia, IDVinculoUsuario, HoraBatida.Date.Date);
                                adpvwFrequenciaDia.Connection.Close();
                            }
                            catch (Exception ex)
                            {

                                adpvwFrequenciaDia.Connection.Close();
                                return "Jornada de trabalho não identificada no banco de dados. Tente Novamente!";
                            }
                        }
                    }
                    else
                    {
                        if ((!vwFrequenciaDia[0].IsTotalHorasDiaSegundosNull()) 
                            || (vwFrequenciaDia[0].TotalHorasDiaSegundos != 0))
                        {
                            SomaTolerancia ST = new SomaTolerancia();
                            DateTime Limite5;
                            if (!vwFrequenciaDia[0].IsHoraSaidaTardeNull())
                            {
                                Limite5 = vwFrequenciaDia[0].HoraSaidaTarde;
                            }
                            else
                            {
                                Limite5 = vwFrequenciaDia[0].HoraSaidaManha;
                            }
                            if (!ST.PermiteRegistroTolerancia5Min(HoraBatida, Limite5))
                            {
                                return string.Format("{0}, jornada finalizada. Intervalo de 5 minutos para iniciar nova jornada.", PrimeiroNome);
                            }
                            else //Pode finalizar a jornada começada no dia anterior. ou no mesmo dia.
                            {
                                if (!vwFrequenciaDia[0].IsHoraSaidaTardeNull())
                                {

                                    
                        //01/07/2019 - finaliza jornada começada na entrada 2 que 
                        //já foi finalizada mas está com a jornada incompleta.
                        if (vwFrequenciaDia[0].IsHoraEntraManhaNull() &&
                        !vwFrequenciaDia[0].IsHoraEntradaTardeNull()
                        && !vwFrequenciaDia[0].IsTotalHorasDiaSegundosNull())
                                    {
                                        if (TotalEscala == new TimeSpan(10, 0, 0))// se for médico
                                        {
                                            TotalEscala = TotalEscala.Add(new TimeSpan(2, 15, 0));
                                        }
                                        else
                                            TotalEscala = TotalEscala.Add(new TimeSpan(0, 15, 0));

                                        if (TotalEscala > TotalJornada)
                                        {
                                            //se for menor prosseguir com a atualização do campo
                                            if (adpFrequencia.UpdateSaidaTarde(HoraBatida, TotalHorasDia(null, null, vwFrequenciaDia[0].HoraEntradaTarde, HoraBatida, "Tarde", vwFrequenciaDia[0].DTFrequencia.Date.Date), vwFrequenciaDia[0].IDUsuario, vwFrequenciaDia[0].IDFrequencia) == 0)
                                            {
                                                msg = "Nao Inserido";
                                            }
                                            else
                                                msg = "Horario";

                                            //Total da jornada

                                            //adpFrequencia.UpdateTotHoras(TotalHorasDia(vwFrequenciaDia[0].HoraEntraManha, HoraBatida, null, null, "Manha", vwFrequenciaDia[0].DTFrequencia.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);

                                            TotalHorasDia(null, null, vwFrequenciaDia[0].HoraEntradaTarde, HoraBatida, "Tarde", vwFrequenciaDia[0].DTFrequencia.Date.Date);

                                            switch (msg)
                                            {
                                                case "Add Noturno":
                                                    msg = String.Format("{0}, adicional noturno iniciado com sucesso.", PrimeiroNome.Trim());
                                                    break;
                                                case "Carga Total":
                                                    msg = String.Format("{0}, jornada finalizada.", PrimeiroNome.Trim());
                                                    break;
                                                case "Carga Incompleta":
                                                    msg = String.Format("{0}, jornada finalizada.", PrimeiroNome.Trim());//com a carga horária incompleta.
                                                    break;
                                                case "Nao Atualizado":
                                                    msg = String.Format("{0}, marcação não realizada. Repita o processo.", PrimeiroNome.Trim());
                                                    break;
                                            }

                                            return msg;

                                        }

                                    }

                                    //update saída da tarde.
                                    if (!VerificaIntervalodeRegistro(HoraBatida, vwFrequenciaDia[0].HoraEntradaTarde))
                                        return string.Format("{0}, respeite o intervalo de 15 minutos entre as marcações.", PrimeiroNome);

                                    //Se a jornada está completa, não deixar registrar novamente.
                                    TimeSpan totalhoras;
                                    totalhoras = HoraBatida.Subtract(vwFrequenciaDia[0].HoraEntradaTarde);

                                    if (totalhoras.Hours < TotalHoraDia)
                                    {

                                        if (adpFrequencia.UpdateSaidaTarde(HoraBatida, TotalHorasDia(null, null, vwFrequenciaDia[0].HoraEntradaTarde, HoraBatida, "Tarde", vwFrequenciaDia[0].DTFrequencia.Date.Date), vwFrequenciaDia[0].IDUsuario, vwFrequenciaDia[0].IDFrequencia) == 0)
                                        {
                                            msg = "Nao Inserido";
                                        }
                                        else
                                        {
                                            msg = "Horario";
                                        }

                                        //Total da jornada
                                        //adpFrequencia.UpdateTotHoras(TotalHorasDia(null, null, vwFrequenciaDia[0].HoraEntradaTarde, HoraBatida, "Tarde", vwFrequenciaDia[0].DTFrequencia.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);

                                        TotalHorasDia(null, null, vwFrequenciaDia[0].HoraEntradaTarde, HoraBatida, "Tarde", vwFrequenciaDia[0].DTFrequencia.Date.Date);

                                        switch (msg)
                                        {
                                            case "Add Noturno":
                                                msg = String.Format("{0}, adicional noturno iniciado com sucesso.", PrimeiroNome);
                                                break;
                                            case "Carga Total":
                                                msg = String.Format("{0}, jornada finalizada.", PrimeiroNome);
                                                break;
                                            case "Carga Incompleta":
                                                msg = String.Format("{0}, jornada finalizada.", PrimeiroNome);// com a carga horária incompleta.
                                                break;
                                            case "Nao Atualizado":
                                                msg = String.Format("{0}, marcação não realizada. Repita o processo.", PrimeiroNome);
                                                break;
                                        }
                                        return msg;
                                    }
                                }
                                //aqui tratar plantao iniciado na entrada 1 28/06/2019 e finalizado
                                //com a carga horária incompleta.
                                else if(!vwFrequenciaDia[0].IsHoraEntraManhaNull() && 
                                    vwFrequenciaDia[0].IsHoraEntradaTardeNull()
                                    && !vwFrequenciaDia[0].IsTotalHorasDiaSegundosNull())
                                {
                                    if(TotalEscala == new TimeSpan(10,0,0))// se for médico
                                    {
                                        TotalEscala = TotalEscala.Add(new TimeSpan(2, 15, 0));
                                    }
                                    else
                                        TotalEscala = TotalEscala.Add(new TimeSpan(0, 15, 0));

                                    if (TotalEscala > TotalJornada)
                                    {
                                        //se for menor prosseguir com a atualização do campo
                                        if (adpFrequencia.UpdateSaidaManha(HoraBatida, TotalHorasDia(vwFrequenciaDia[0].HoraEntraManha, HoraBatida, null, null, "Manha", vwFrequenciaDia[0].DTFrequencia.Date.Date), IDUsuario, vwFrequenciaDia[0].DTFrequencia.Date.Date, IDVinculoUsuario) == 0)
                                        {
                                            msg = "Nao Inserido";
                                        }
                                        else
                                            msg = "Horario";

                                        //Total da jornada

                                        //adpFrequencia.UpdateTotHoras(TotalHorasDia(vwFrequenciaDia[0].HoraEntraManha, HoraBatida, null, null, "Manha", vwFrequenciaDia[0].DTFrequencia.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);

                                        TotalHorasDia(vwFrequenciaDia[0].HoraEntraManha, HoraBatida, null, null, "Manha", vwFrequenciaDia[0].DTFrequencia.Date.Date);

                                        switch (msg)
                                        {
                                            case "Add Noturno":
                                                msg = String.Format("{0}, adicional noturno iniciado com sucesso.", PrimeiroNome.Trim());
                                                break;
                                            case "Carga Total":
                                                msg = String.Format("{0}, jornada finalizada.", PrimeiroNome.Trim());
                                                break;
                                            case "Carga Incompleta":
                                                msg = String.Format("{0}, jornada finalizada.", PrimeiroNome.Trim());//com a carga horária incompleta.
                                                break;
                                            case "Nao Atualizado":
                                                msg = String.Format("{0}, marcação não realizada. Repita o processo.", PrimeiroNome.Trim());
                                                break;
                                        }

                                        return msg;

                                    }

                                }
                                else
                                {
                                    Limite5 = vwFrequenciaDia[0].HoraSaidaManha;
                                }
                            }

                        }
                        //Procura por registros no dia corrente.
                        try
                        {
                            adpvwFrequenciaDia.Connection.Open();
                            adpvwFrequenciaDia.FillIDVinculoUsuario(vwFrequenciaDia, IDVinculoUsuario, HoraBatida.Date.Date);
                            adpvwFrequenciaDia.Connection.Close();
                        }
                        catch (Exception ex)
                        {
                            UtilLog.EscreveLog(DateTime.Now.ToLongDateString() + " ERRO BATERPONTO LINHA 1792" + ex.Message);
                            adpvwFrequenciaDia.Connection.Close();
                            return "Jornada de trabalho não identificada no banco de dados. Tente Novamente!";
                        }
                    }
                }
                else //Se não ... Start com o dia corrente
                {
                    //Procura por registros realizados no dia anterior em busca de plantões a serem finalizados
                    try
                    {
                        adpvwFrequenciaDia.Connection.Open();
                        adpvwFrequenciaDia.FillIDVinculoUsuario(vwFrequenciaDia, IDVinculoUsuario, HoraBatida.Date.Date);
                        adpvwFrequenciaDia.Connection.Close();
                    }
                    catch (Exception ex)
                    {
                        UtilLog.EscreveLog(DateTime.Now.ToLongDateString() + " ERRO BATERPONTO LINHA 1809 " + ex.Message);
                        adpvwFrequenciaDia.Connection.Close();
                        return "Jornada de trabalho não identificada no banco de dados. Tente Novamente!";
                    }
                }

                switch (vwFrequenciaDia.Rows.Count)
                {
                    case 0: //Caso não tenha registros no dia, iniciar jornada de plantão.
                        #region Inicio Nova Jornada
                        if (HoraBatida.Hour < 11) //caso antes das 11 iniciar o plantão na parte da manhã
                        {
                            //01/07/2019 -- Caso a jornada do dia anterior tenha sido finalizada,
                            if(saida2Anterior.HasValue)
                            {
                                TimeSpan? H = HoraBatida - saida2Anterior;
                                if(H < new TimeSpan(0,5,0))
                                {
                                    msg = string.Format("{0}, intervalo de 5 min. entre o fim de uma jornada e o início de outra.", PrimeiroNome);
                                    return msg;
                                }
                            }
                            //tolerâcia para começar outra jornada.

                            //Caso insert retorne 0, manda msg para repetir operação. Caso contrário segue rotina

                            if (adpFrequencia.Insert(IDUsuario, null, HoraBatida.Date, HoraBatida, null, null, null, null, null, HoraBatida.Month, HoraBatida.Year, IDEmpresa, IDVinculoUsuario) == 0)
                            {
                                msg = "Nao Inserido";
                            }
                            else
                            {
                                msg = "Horario";
                            }
                        }
                        else //Caso acima das 11. Registrar na parte da tarde.
                        {
                            //Caso insert retorne 0, manda msg para repetir operação. Caso contrário segue rotina

                            if (adpFrequencia.Insert(IDUsuario, null, HoraBatida.Date, null, null, HoraBatida, null, null, null, HoraBatida.Month, HoraBatida.Year, IDEmpresa, IDVinculoUsuario) == 0)
                            {
                                msg = "Nao Inserido";
                            }
                            else
                            {
                                msg = "Horario";
                            }
                        }

                        switch (msg)
                        {
                            case "Horario":
                                msg = String.Format("{0}, entrada efetivada.", PrimeiroNome.Trim());
                                break;
                            case "Nao Inserido":
                                msg = String.Format("{0}, marcação não efetivada. Favor repetir a operação.", PrimeiroNome.Trim());
                                break;
                        }

                        #endregion
                        break;
                    case 1:

                        // 19/09/2018 - Caso tenha registro e estiver justificado. Trata a entrada 1 e entrada 2
                        //19/08/2018

                        //19/09/2018 -- Caso a jornada esteja justificada, faz as entradas com um update.
                        if (!vwFrequenciaDia[0].IsIDMotivoFaltaNull() && vwFrequenciaDia[0].IsHoraEntraManhaNull() &&
                           vwFrequenciaDia[0].IsHoraSaidaManhaNull() && vwFrequenciaDia[0].IsHoraEntradaTardeNull()
                           && vwFrequenciaDia[0].IsHoraSaidaTardeNull())
                        {
                            if (HoraBatida.Hour <= 11)
                            {

                                if (adpFrequencia.UpdateEntradaManhaJust(HoraBatida, vwFrequenciaDia[0].IDFrequencia, IDUsuario) > 0)
                                {
                                    return string.Format("{0}, entrada efetivada com sucesso.", PrimeiroNome);
                                }
                            }
                            else
                            {

                                if (adpFrequencia.UpdateEntradaTardeJust(HoraBatida, vwFrequenciaDia[0].IDFrequencia, IDUsuario) > 0)
                                {
                                    return string.Format("{0}, entrada efetivada com sucesso.", PrimeiroNome);
                                }
                            }
                        }
                        //--------------------------------------------------------------------------------------
                        //Tratativas de término da jornada e adiconal noturno.
                        //Adicional Noturno
                        if (vwFrequenciaDia[0].IsTotalHorasDiaSegundosNull() && (HoraBatida.Hour >= 21 && HoraBatida.Hour <= 23))
                        {
                            //Para cair aqui, já tem que haver dado entrada de um plantão.
                            TimeSpan totalhoras;

                            if (!vwFrequenciaDia[0].IsHoraEntraManhaNull())
                            {

                                //Verifica minutos entre a entrada e saída

                                if (!VerificaIntervalodeRegistro(HoraBatida, vwFrequenciaDia[0].HoraEntraManha))
                                    return string.Format("{0}, respeite o intervalo de 15 minutos entre as marcações.", PrimeiroNome);

                                totalhoras = HoraBatida.Subtract(vwFrequenciaDia[0].HoraEntraManha);

                                totalhoras += TimeSpan.Parse("00:15:00"); //Add os 15 minutos de tolerância;

                                if ((totalhoras.TotalHours >= TotalHoraDia) || (totalhoras.TotalHours >= 20))
                                {

                                    if (adpFrequencia.UpdateSaidaManha(HoraBatida, TotalHorasDia(vwFrequenciaDia[0].HoraEntraManha, HoraBatida, null, null, "Manha", vwFrequenciaDia[0].DTFrequencia.Date.Date), IDUsuario, vwFrequenciaDia[0].DTFrequencia.Date.Date, IDVinculoUsuario) == 0)
                                    {
                                        msg = "Nao Inserido";
                                    }
                                    else
                                        msg = "Horario";

                                    //Total da jornada

                                    //adpFrequencia.UpdateTotHoras(TotalHorasDia(vwFrequenciaDia[0].HoraEntraManha, HoraBatida, null, null, "Manha", vwFrequenciaDia[0].DTFrequencia.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);

                                    TotalHorasDia(vwFrequenciaDia[0].HoraEntraManha, HoraBatida, null, null, "Manha", vwFrequenciaDia[0].DTFrequencia.Date.Date);

                                    switch (msg)
                                    {
                                        case "Add Noturno":
                                            msg = String.Format("{0}, adicional noturno iniciado com sucesso.", PrimeiroNome.Trim());
                                            break;
                                        case "Carga Total":
                                            msg = String.Format("{0}, jornada finalizada.", PrimeiroNome.Trim());
                                            break;
                                        case "Carga Incompleta":
                                            msg = String.Format("{0}, jornada finalizada.", PrimeiroNome.Trim());//com a carga horária incompleta.
                                            break;
                                        case "Nao Atualizado":
                                            msg = String.Format("{0}, marcação não realizada. Repita o processo.", PrimeiroNome.Trim());
                                            break;
                                    }
                                }
                                else
                                {

                                    if (adpFrequencia.UpdateEntradaAddNoturno(HoraBatida, vwFrequenciaDia[0].IDFrequencia, vwFrequenciaDia[0].IDVinculoUsuario) > 0)
                                        msg = string.Format("{0}, adicional noturno informado com sucesso!", PrimeiroNome);
                                    else
                                        msg = string.Format("{0}, houve falha ao informar o adicional noturno. Tente Novamente!", PrimeiroNome);

                                    return msg;
                                }
                            }
                            else if (!vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                            {

                                if (!VerificaIntervalodeRegistro(HoraBatida, vwFrequenciaDia[0].HoraEntradaTarde))
                                    return string.Format("{0}, respeite o intervalo de 15 minutos entre as marcações.", PrimeiroNome);

                                totalhoras = HoraBatida.Subtract(vwFrequenciaDia[0].HoraEntradaTarde);

                                totalhoras += TimeSpan.Parse("00:15:00"); //Add os 15 minutos de tolerância;
                                                                          //COmentado em 04/09/2018 -- tratativa de ad. noturno direto na rotina.
                                                                          //if (totalhoras.TotalHours >= vwUsuarioGrid[vinculo].TotHorasDiarias || (totalhoras.TotalHours >= 20))
                                                                          //{

                                if (adpFrequencia.UpdateSaidaTarde(HoraBatida, TotalHorasDia(null, null, vwFrequenciaDia[0].HoraEntradaTarde, HoraBatida, "Tarde", vwFrequenciaDia[0].DTFrequencia.Date.Date), vwFrequenciaDia[0].IDUsuario, vwFrequenciaDia[0].IDFrequencia) == 0)
                                {
                                    msg = "Nao Inserido";
                                }
                                else
                                {
                                    msg = "Horario";
                                }

                                //Total da jornada
                                //adpFrequencia.UpdateTotHoras(TotalHorasDia(null, null, vwFrequenciaDia[0].HoraEntradaTarde, HoraBatida, "Tarde", vwFrequenciaDia[0].DTFrequencia.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);

                                TotalHorasDia(null, null, vwFrequenciaDia[0].HoraEntradaTarde, HoraBatida, "Tarde", vwFrequenciaDia[0].DTFrequencia.Date.Date);

                                switch (msg)
                                {
                                    case "Add Noturno":
                                        msg = String.Format("{0}, adicional noturno iniciado com sucesso.", PrimeiroNome);
                                        break;
                                    case "Carga Total":
                                        msg = String.Format("{0}, jornada finalizada.", PrimeiroNome);
                                        break;
                                    case "Carga Incompleta":
                                        msg = String.Format("{0}, jornada finalizada.", PrimeiroNome);//com a carga horária incompleta.
                                        break;
                                    case "Nao Atualizado":
                                        msg = String.Format("{0}, marcação não realizada. Repita o processo.", PrimeiroNome);
                                        break;
                                }

                                //}
                                //else
                                //{
                                //    if (adpFrequencia.UpdateEntradaAddNoturno(HoraBatida, vwFrequenciaDia[0].IDFrequencia, vwFrequenciaDia[0].IDVinculoUsuario) > 0)
                                //        msg = string.Format("{0}, adicional noturno informado com sucesso!", PrimeiroNome);
                                //    else
                                //        msg = string.Format("{0}, houve falha ao informar o adicional noturno. Tente Novamente!", PrimeiroNome);

                                //    return msg;
                                //}
                            }
                        }
                        else
                        {
                            //Aqui, update do fim da jornada e Total da mesma.
                            if (!vwFrequenciaDia[0].IsHoraEntraManhaNull())
                            {

                                if (!VerificaIntervalodeRegistro(HoraBatida, vwFrequenciaDia[0].HoraEntraManha))
                                    return string.Format("{0}, respeite o intervalo de 15 minutos entre as marcações.", PrimeiroNome);
                                UtilLog.EscreveLog(DateTime.Now.ToLongDateString() + " ERRO BaterPonto LINHA 2011");
                                if (adpFrequencia.UpdateSaidaManha(HoraBatida, TotalHorasDia(vwFrequenciaDia[0].HoraEntraManha, HoraBatida, null, null, "Manha", vwFrequenciaDia[0].DTFrequencia.Date.Date), IDUsuario, vwFrequenciaDia[0].DTFrequencia.Date.Date, IDVinculoUsuario) == 0)
                                {
                                    msg = "Nao Inserido";
                                }
                                else
                                    msg = "Horario";

                                //Total da jornada

                                //adpFrequencia.UpdateTotHoras(TotalHorasDia(vwFrequenciaDia[0].HoraEntraManha, HoraBatida, null, null, "Manha", vwFrequenciaDia[0].DTFrequencia.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);

                                TotalHorasDia(vwFrequenciaDia[0].HoraEntraManha, HoraBatida, null, null, "Manha", vwFrequenciaDia[0].DTFrequencia.Date.Date);
                            }
                            else
                            {

                                if (!VerificaIntervalodeRegistro(HoraBatida, vwFrequenciaDia[0].HoraEntradaTarde))
                                    return string.Format("{0}, respeite o intervalo de 15 minutos entre as marcações.", PrimeiroNome);

                                if (adpFrequencia.UpdateSaidaTarde(HoraBatida, TotalHorasDia(null, null, vwFrequenciaDia[0].HoraEntradaTarde, HoraBatida, "Tarde", vwFrequenciaDia[0].DTFrequencia.Date.Date), IDUsuario, vwFrequenciaDia[0].IDFrequencia) == 0)
                                {
                                    msg = "Nao Inserido";
                                }
                                else
                                    msg = "Horario";

                                //Total da jornada
                                //adpFrequencia.UpdateTotHoras(TotalHorasDia(null, null, vwFrequenciaDia[0].HoraEntradaTarde,HoraBatida, "Tarde", vwFrequenciaDia[0].DTFrequencia.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);

                                TotalHorasDia(null, null, vwFrequenciaDia[0].HoraEntradaTarde, HoraBatida, "Tarde", vwFrequenciaDia[0].DTFrequencia.Date.Date);
                            }

                            switch (msg)
                            {
                                case "Add Noturno":
                                    msg = String.Format("{0}, adicional noturno iniciado com sucesso.", PrimeiroNome);
                                    break;
                                case "Carga Total":
                                    msg = String.Format("{0}, jornada finalizada.", PrimeiroNome);
                                    break;
                                case "Carga Incompleta":
                                    msg = String.Format("{0}, jornada finalizada.", PrimeiroNome);// com a carga horária incompleta.
                                    break;
                                case "Nao Atualizado":
                                    msg = String.Format("{0}, marcação não realizada. Repita o processo.", PrimeiroNome);
                                    break;
                            }
                        }

                        break;
                }
                #endregion
            }
            return msg;
        }

        public string BaterPonto2(int IDUsuario, int IDEmpresa, DateTime HoraBatida, int IDSetor) //Acrescentado IDSetor aos parâmetros para procura dos parâmetros do usuário caso ele tenha
        //Mais de um vínculo com o mesmo Órgão.
        {
            //IMPORTANTE - 16/12/2015 - Alterei a busca por registros realizados para procurar por empresa ...
            /// FillFrequenciaDia para - FillFrequenciaDiaEmpresa - Para atender os multiplos Vínculos

            if (HoraBatida.Date.Date > System.DateTime.Now.Date.Date)
                return string.Format("Tentativa de registro com a data/hora maior({0}) que a atual. Registro não lançado!", HoraBatida.Date.ToShortDateString());

            #region Dados Pessoais do usuário

            //Procurar dados dos usuários pela view - vwUsuarioGrid
            try
            {
                adpvwUsuarioGrid.Connection.Open();
                adpvwUsuarioGrid.FillIDEmpresaUsuario(vwUsuarioGrid, IDEmpresa, IDUsuario);
                adpvwUsuarioGrid.Connection.Close();
            }
            catch
            {
                adpvwUsuarioGrid.Connection.Close();
            }

            vinculo = 0;

            //Caso tenho mais de um vínculo com o mesmo usuário - limita-se a busca pelo setor em que se faz o registro.
            if (vwUsuarioGrid.Rows.Count > 1) //Se maior que 1, procurar pelo setor para setar a Vinculo que irá registrar frequência.
            {
                for (int i = 0; i <= (vwUsuarioGrid.Rows.Count - 1); i++)
                {
                    if (IDSetor == vwUsuarioGrid[i].IDSetor)
                    {
                        vinculo = i;
                        break;
                    }
                }
            }
            else if (vwUsuarioGrid.Rows.Count == 0) //Faz aqui a localização de Grupo de Registros
            {
                //Busca pelo grupo de registro. Cancelado o grupo de registros a pedido da josiane (06/08/2018)

                try
                {
                    IDEmpresa = ValidaVinculo(IDUsuario, IDEmpresa, "");
                    adpvwUsuarioGrid.Connection.Open();
                    adpvwUsuarioGrid.FillIDEmpresaUsuario(vwUsuarioGrid, IDEmpresa, IDUsuario);
                    adpvwUsuarioGrid.Connection.Close();
                    if (vwUsuarioGrid.Rows.Count > 0)
                        return string.Format("{0}, faça registre seu ponto no órgão de lotação.");
                }
                catch
                {
                    adpvwUsuarioGrid.Connection.Close();
                }
            }

            msg = "";
            NomeUsuario = "";
            Bateu = false;



            //Dados Pessoais do Usuário..
            //adpUsuario.FillIDUsuario(TBUsuario, IDUsuario, IDEmpresa); -- Retirado, buscar agora apenas os dados pessoais
            //24/02/2016 - Alterado Procurar os atributos do usuário pelo vínculo --adpUsuario.FillIDusuarioApenas(TBUsuario, IDUsuario);

            if (vwUsuarioGrid.Rows.Count > 0)
            {
                NomeUsuario = vwUsuarioGrid[vinculo].PrimeiroNome.ToString();
                HoraPadraoEntradaManha = vwUsuarioGrid[vinculo].HoraEntradaManha.ToString();
                HoraPadraoEntradaTarde = vwUsuarioGrid[vinculo].HoraEntradaTarde.ToString();
                HoraPadraoSaidaManha = vwUsuarioGrid[vinculo].HoraSaidaManha.ToString();
                HoraPadraoSaidTarde = vwUsuarioGrid[vinculo].HoraSaidaTarde.ToString();
                LimiteEntradaSaida = "00:15:59.0000";
                LimitePadraoEntradaSaida = "00:15:59.0000";
                LimitePadraoAlmoco = "01:00:00.0000";

                //Trabalha Sábado
                TrabalhaSabado = false; //Retirei da jornada

                //Para atender a justificativas e registros
                TotalHorasDiaUsuarioPadrao = vwUsuarioGrid[vinculo].TotHorasDiarias;
                RegimePlantonista = vwUsuarioGrid[vinculo].RegimePlantonista;
            }
            else
            {
                return "Servidor não encontrado no banco de dados!";
            }

            #endregion
            //27/07/2016 -- Defasagem de Registro

            TimeSpan TotDias = DateTime.Now - HoraBatida;

            if (TotDias.TotalDays > 4)
            {
                return string.Format("{0}, registro com mais de 4 dias ({1}) de defasagem. Procure o seu gestor!", NomeUsuario.Trim(), HoraBatida.ToShortDateString());
            }

            //Por enquanto FIxo de 2 horas ... Valor Inteiro... Após atrelar isso ao cadastro de empresas
            LimiteMaximoPlantao = 2;

            //try
            //{
            //    adpFeriasFrequencia.Connection.Open();
            //    adpFeriasFrequencia.FillByVerificaFeriasCorrente(TBFeriasFrequencia, HoraBatida.Date, IDUsuario, vwUsuarioGrid[vinculo].IDVinculoUsuario); // Verifica férias corrente
            //    adpFeriasFrequencia.Connection.Close();
            //}
            //catch
            //{
            //    adpFeriasFrequencia.Connection.Close();
            //}
            PreencheTabela PT = new PreencheTabela();
            if (PT.GetVerificaSeEstaDeFerias(IDUsuario, vwUsuarioGrid[vinculo].IDVinculoUsuario, HoraBatida.Date))
            {
                return string.Format("{0}, Férias/Licença em vigência. Procure sua chefia imediata.", vwUsuarioGrid[vinculo].PrimeiroNome.Trim());
            }
            else /// Se não houver férias ou licenças. Registra ponto dos servidores ...
            {
                if (vwUsuarioGrid[vinculo].RegimePlantonista) //Se regime é de plantonista fazer os pontos de plantonista. Se não segue como regime de expediente
                {
                    #region Registro de marcação de ponto de plantonistas

                    //Procura por registros realizados no dia anterior em busca de plantões a serem finalizados
                    try
                    {
                        adpvwFrequenciaDia.Connection.Open();
                        adpvwFrequenciaDia.FillIDVinculoUsuario(vwFrequenciaDia, vwUsuarioGrid[vinculo].IDVinculoUsuario, HoraBatida.Date.Date.AddDays(-1));
                        adpvwFrequenciaDia.Connection.Close();
                    }
                    catch
                    {
                        adpvwFrequenciaDia.Connection.Close();
                        return "Jornada de trabalho não identificada no banco de dados. Tente Novamente!";
                    }

                    if (vwFrequenciaDia.Rows.Count > 0) //se há registros analizar os tempos
                    {

                        //Se houver uma falta lançada, não registra frequência
                        if (!vwFrequenciaDia[0].IsIDMotivoFaltaNull())
                        {
                            if (FaltaLancada(vwFrequenciaDia[0].IDMotivoFalta)) // Se o motivo for 47, não bate ponto.
                                return string.Format("{0}, há uma falta lançada na data corrente. Impossível lançar o registro de sua marcação!", NomeUsuario);
                        }

                        if ((vwFrequenciaDia[0].IsTotalHorasDiaSegundosNull()) || (vwFrequenciaDia[0].TotalHorasDiaSegundos == 0)) // Se o total estiver nulo. Verificar qual das entradas foi utiliza (Manhã ou tarde)
                        {
                            TimeSpan Resultado;

                            if (!vwFrequenciaDia[0].IsHoraEntraManhaNull())// se não for nulo manipula entrada manhã
                            {
                                Resultado = HoraBatida.Subtract(vwFrequenciaDia[0].HoraEntraManha);
                            }
                            else
                            {
                                Resultado = HoraBatida.Subtract(vwFrequenciaDia[0].HoraEntradaTarde);
                            }

                            H = Resultado.TotalMinutes;

                            if (H > ((vwUsuarioGrid[vinculo].TotHorasDiarias + LimiteMaximoPlantao) * 60)) //Se maior que a soma do total + limite... Começa um novo dia
                            {
                                //Se verdadeiro, inicia nova jornada
                                try
                                {
                                    adpvwFrequenciaDia.Connection.Open();
                                    adpvwFrequenciaDia.FillIDVinculoUsuario(vwFrequenciaDia, vwUsuarioGrid[vinculo].IDVinculoUsuario, HoraBatida.Date.Date);
                                    adpvwFrequenciaDia.Connection.Close();
                                }
                                catch
                                {
                                    adpvwFrequenciaDia.Connection.Close();
                                    return "Jornada de trabalho não identificada no banco de dados. Tente Novamente!";
                                }
                            }
                        }
                        else
                        {
                            //Procura por registros no dia corrente.
                            try
                            {
                                adpvwFrequenciaDia.Connection.Open();
                                adpvwFrequenciaDia.FillIDVinculoUsuario(vwFrequenciaDia, vwUsuarioGrid[vinculo].IDVinculoUsuario, HoraBatida.Date.Date);
                                adpvwFrequenciaDia.Connection.Close();
                            }
                            catch
                            {
                                adpvwFrequenciaDia.Connection.Close();
                                return "Jornada de trabalho não identificada no banco de dados. Tente Novamente!";
                            }
                        }
                    }
                    else //Se não ... Start com o dia corrente
                    {
                        //Procura por registros realizados no dia anterior em busca de plantões a serem finalizados
                        try
                        {
                            adpvwFrequenciaDia.Connection.Open();
                            adpvwFrequenciaDia.FillIDVinculoUsuario(vwFrequenciaDia, vwUsuarioGrid[vinculo].IDVinculoUsuario, HoraBatida.Date.Date);
                            adpvwFrequenciaDia.Connection.Close();
                        }
                        catch
                        {
                            adpvwFrequenciaDia.Connection.Close();
                            return "Jornada de trabalho não identificada no banco de dados. Tente Novamente!";
                        }
                    }

                    switch (vwFrequenciaDia.Rows.Count)
                    {
                        case 0: //Caso não tenha registros no dia, iniciar jornada de plantão.
                            #region Inicio Nova Jornada
                            if (HoraBatida.Hour < 11) //caso antes das 11 iniciar o plantão na parte da manhã
                            {
                                //Caso insert retorne 0, manda msg para repetir operação. Caso contrário segue rotina
                                if (adpFrequencia.Insert(IDUsuario, null, HoraBatida.Date, HoraBatida, null, null, null, null, null, HoraBatida.Month, HoraBatida.Year, IDEmpresa, vwUsuarioGrid[vinculo].IDVinculoUsuario) == 0)
                                {
                                    msg = "Nao Inserido";
                                }
                                else
                                {
                                    msg = "Horario";
                                }
                            }
                            else //Caso acima das 11. Registrar na parte da tarde.
                            {
                                //Caso insert retorne 0, manda msg para repetir operação. Caso contrário segue rotina
                                if (adpFrequencia.Insert(IDUsuario, null, HoraBatida.Date, null, null, HoraBatida, null, null, null, HoraBatida.Month, HoraBatida.Year, IDEmpresa, vwUsuarioGrid[vinculo].IDVinculoUsuario) == 0)
                                {
                                    msg = "Nao Inserido";
                                }
                                else
                                {
                                    msg = "Horario";
                                }
                            }

                            switch (msg)
                            {
                                case "Horario":
                                    msg = String.Format("{0}, entrada 1 efetivada.", NomeUsuario.Trim());
                                    break;
                                case "Nao Inserido":
                                    msg = String.Format("{0}, marcação não efetivada. Favor repetir a operação.", NomeUsuario.Trim());
                                    break;
                            }

                            #endregion
                            break;
                        case 1:
                            //Tratativas de término da jornada e adiconal noturno.
                            //Adicional Noturno
                            if (vwFrequenciaDia[0].IsTotalHorasDiaSegundosNull() && (HoraBatida.Hour >= 21 && HoraBatida.Hour <= 23))
                            {
                                //Aqui, update Adiconal Noturno -- Quais os possíveis casos para adicional noturno. --09/03/2016
                                //Para cair aqui, já tem que haver dado entrada de um plantão.
                                TimeSpan totalhoras;

                                if (!vwFrequenciaDia[0].IsHoraEntraManhaNull())
                                {

                                    //Verifica minutos entre a entrada e saída
                                    if (!VerificaIntervalodeRegistro(HoraBatida, vwFrequenciaDia[0].HoraEntraManha))
                                        return string.Format("{0}, respeite o intervalo de 15 minutos entre as marcações.", NomeUsuario);

                                    totalhoras = HoraBatida.Subtract(vwFrequenciaDia[0].HoraEntraManha);

                                    totalhoras += TimeSpan.Parse("00:15:00"); //Add os 15 minutos de tolerância;

                                    if ((totalhoras.TotalHours >= vwUsuarioGrid[vinculo].TotHorasDiarias) || (totalhoras.TotalHours >= 20))
                                    {
                                        if (adpFrequencia.UpdateSaidaManha(HoraBatida, TotalHorasDia(vwFrequenciaDia[0].HoraEntraManha, HoraBatida, null, null, "Manha", vwFrequenciaDia[0].DTFrequencia.Date.Date), IDUsuario, vwFrequenciaDia[0].DTFrequencia.Date.Date, vwUsuarioGrid[vinculo].IDVinculoUsuario) == 0)
                                        {
                                            msg = "Nao Inserido";
                                        }
                                        else
                                            msg = "Horario";

                                        //Total da jornada

                                        //adpFrequencia.UpdateTotHoras(TotalHorasDia(vwFrequenciaDia[0].HoraEntraManha, HoraBatida, null, null, "Manha", vwFrequenciaDia[0].DTFrequencia.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);

                                        TotalHorasDia(vwFrequenciaDia[0].HoraEntraManha, HoraBatida, null, null, "Manha", vwFrequenciaDia[0].DTFrequencia.Date.Date);

                                        switch (msg)
                                        {
                                            case "Add Noturno":
                                                msg = String.Format("{0}, adicional noturno iniciado com sucesso.", NomeUsuario.Trim());
                                                break;
                                            case "Carga Total":
                                                msg = String.Format("{0}, jornada finalizada.", NomeUsuario.Trim());
                                                break;
                                            case "Carga Incompleta":
                                                msg = String.Format("{0}, jornada finalizada com a carga horária incompleta.", NomeUsuario.Trim());
                                                break;
                                            case "Nao Atualizado":
                                                msg = String.Format("{0}, marcação não realizada. Repita o processo.", NomeUsuario.Trim());
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        if (adpFrequencia.UpdateEntradaAddNoturno(HoraBatida, vwFrequenciaDia[0].IDFrequencia, vwFrequenciaDia[0].IDVinculoUsuario) > 0)
                                            msg = string.Format("{0}, adicional noturno informado com sucesso!", NomeUsuario);
                                        else
                                            msg = string.Format("{0}, houve falha ao informar o adicional noturno. Tente Novamente!", NomeUsuario);

                                        return msg;
                                    }
                                }
                                else if (!vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                                {
                                    if (!VerificaIntervalodeRegistro(HoraBatida, vwFrequenciaDia[0].HoraEntradaTarde))
                                        return string.Format("{0}, respeite o intervalo de 15 minutos entre as marcações.", NomeUsuario);

                                    totalhoras = HoraBatida.Subtract(vwFrequenciaDia[0].HoraEntradaTarde);

                                    totalhoras += TimeSpan.Parse("00:15:00"); //Add os 15 minutos de tolerância;

                                    if (totalhoras.TotalHours >= vwUsuarioGrid[vinculo].TotHorasDiarias || (totalhoras.TotalHours >= 20))
                                    {

                                        if (adpFrequencia.UpdateSaidaTarde(HoraBatida, TotalHorasDia(null, null, vwFrequenciaDia[0].HoraEntradaTarde, HoraBatida, "Tarde", vwFrequenciaDia[0].DTFrequencia.Date.Date), vwFrequenciaDia[0].IDUsuario, vwFrequenciaDia[0].IDFrequencia) == 0)
                                        {
                                            msg = "Nao Inserido";
                                        }
                                        else
                                        {
                                            msg = "Horario";
                                        }

                                        //Total da jornada
                                        //adpFrequencia.UpdateTotHoras(TotalHorasDia(null, null, vwFrequenciaDia[0].HoraEntradaTarde, HoraBatida, "Tarde", vwFrequenciaDia[0].DTFrequencia.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);

                                        TotalHorasDia(null, null, vwFrequenciaDia[0].HoraEntradaTarde, HoraBatida, "Tarde", vwFrequenciaDia[0].DTFrequencia.Date.Date);

                                        switch (msg)
                                        {
                                            case "Add Noturno":
                                                msg = String.Format("{0}, adicional noturno iniciado com sucesso.", NomeUsuario.Trim());
                                                break;
                                            case "Carga Total":
                                                msg = String.Format("{0}, jornada finalizada.", NomeUsuario.Trim());
                                                break;
                                            case "Carga Incompleta":
                                                msg = String.Format("{0}, jornada finalizada com a carga horária incompleta.", NomeUsuario.Trim());
                                                break;
                                            case "Nao Atualizado":
                                                msg = String.Format("{0}, marcação não realizada. Repita o processo.", NomeUsuario.Trim());
                                                break;
                                        }

                                    }
                                    else
                                    {
                                        if (adpFrequencia.UpdateEntradaAddNoturno(HoraBatida, vwFrequenciaDia[0].IDFrequencia, vwFrequenciaDia[0].IDVinculoUsuario) > 0)
                                            msg = string.Format("{0}, adicional noturno informado com sucesso!", NomeUsuario);
                                        else
                                            msg = string.Format("{0}, houve falha ao informar o adicional noturno. Tente Novamente!", NomeUsuario);

                                        return msg;
                                    }
                                }
                            }
                            else
                            {
                                //Aqui, update do fim da jornada e Total da mesma.
                                if (!vwFrequenciaDia[0].IsHoraEntraManhaNull())
                                {

                                    if (!VerificaIntervalodeRegistro(HoraBatida, vwFrequenciaDia[0].HoraEntraManha))
                                        return string.Format("{0}, respeite o intervalo de 15 minutos entre as marcações.", NomeUsuario);

                                    if (adpFrequencia.UpdateSaidaManha(HoraBatida, TotalHorasDia(vwFrequenciaDia[0].HoraEntraManha, HoraBatida, null, null, "Manha", vwFrequenciaDia[0].DTFrequencia.Date.Date), IDUsuario, vwFrequenciaDia[0].DTFrequencia.Date.Date, vwUsuarioGrid[vinculo].IDVinculoUsuario) == 0)
                                    {
                                        msg = "Nao Inserido";
                                    }
                                    else
                                        msg = "Horario";

                                    //Total da jornada

                                    //adpFrequencia.UpdateTotHoras(TotalHorasDia(vwFrequenciaDia[0].HoraEntraManha, HoraBatida, null, null, "Manha", vwFrequenciaDia[0].DTFrequencia.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);

                                    TotalHorasDia(vwFrequenciaDia[0].HoraEntraManha, HoraBatida, null, null, "Manha", vwFrequenciaDia[0].DTFrequencia.Date.Date);
                                }
                                else
                                {

                                    if (!VerificaIntervalodeRegistro(HoraBatida, vwFrequenciaDia[0].HoraEntradaTarde))
                                        return string.Format("{0}, respeite o intervalo de 15 minutos entre as marcações.", NomeUsuario);

                                    if (adpFrequencia.UpdateSaidaTarde(HoraBatida, TotalHorasDia(null, null, vwFrequenciaDia[0].HoraEntradaTarde, HoraBatida, "Tarde", vwFrequenciaDia[0].DTFrequencia.Date.Date), IDUsuario, vwFrequenciaDia[0].IDFrequencia) == 0)
                                    {
                                        msg = "Nao Inserido";
                                    }
                                    else
                                        msg = "Horario";

                                    //Total da jornada
                                    //adpFrequencia.UpdateTotHoras(TotalHorasDia(null, null, vwFrequenciaDia[0].HoraEntradaTarde,HoraBatida, "Tarde", vwFrequenciaDia[0].DTFrequencia.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);

                                    TotalHorasDia(null, null, vwFrequenciaDia[0].HoraEntradaTarde, HoraBatida, "Tarde", vwFrequenciaDia[0].DTFrequencia.Date.Date);
                                }

                                switch (msg)
                                {
                                    case "Add Noturno":
                                        msg = String.Format("{0}, adicional noturno iniciado com sucesso.", NomeUsuario.Trim());
                                        break;
                                    case "Carga Total":
                                        msg = String.Format("{0}, jornada finalizada.", NomeUsuario.Trim());
                                        break;
                                    case "Carga Incompleta":
                                        msg = String.Format("{0}, jornada finalizada com a carga horária incompleta.", NomeUsuario.Trim());
                                        break;
                                    case "Nao Atualizado":
                                        msg = String.Format("{0}, marcação não realizada. Repita o processo.", NomeUsuario.Trim());
                                        break;
                                }
                            }

                            break;
                    }
                    #endregion
                }
                else //Tratativa para regime de expediente
                {
                    #region Registros de Marcação de ponto Regime de expediente

                    //TBFrequencia.DataSet.EnforceConstraints = false;

                    //Dia seguinte retirado -- Os que fazem plantões irão utilizar outras rotinas
                    #region DiaSeguinte
                    DiaSeguinte = false;
                    //Tratativa para quem finaliza a jornada no dia seguinte
                    //if (!TBUsuario[0].IsHoraSaidaTardeNull())
                    //{
                    //if (TBUsuario[0].FinalizaDiaSeguinte && HoraBatida >= Convert.ToDateTime(string.Format("{0} 00:00:00.000", HoraBatida.Date.ToShortDateString())) && HoraBatida <= Convert.ToDateTime(string.Format("{0} 07:30:00.000", HoraBatida.Date.ToShortDateString())))
                    //{
                    //DateTime DTSeguinte = HoraBatida.Date.Date.AddDays(-1); //Varíavel que possibilita a jornada do dia anterior para complemento.
                    //TBFrequencia.BeginLoadData();
                    //adpFrequencia.FillFrequenciaDiaIDempresa(TBFrequencia, DTSeguinte.Date.Date, IDUsuario, IDEmpresa);
                    //adpFrequencia.FillFrequenciaDia(TBFrequencia, DTSeguinte.Date.Date, IDUsuario); //Verifica se há ponto registrado no dia com a varíavel DTSeguinte
                    //TBFrequencia.EndLoadData();
                    //DiaSeguinte = true;
                    //Verificar se existe ponto para o dia Anterior ---
                    //BatidaDiaAnterior = HoraBatida.AddDays(-1);
                    //adpFrequencia.FillFrequenciaDia(TBFrequencia, BatidaDiaAnterior.Date.Date, IDUsuario);
                    //}
                    //else
                    //{
                    //DiaSeguinte = false;
                    //TBFrequencia.BeginLoadData();
                    //adpFrequencia.FillFrequenciaDiaIDempresa(TBFrequencia, HoraBatida.Date.Date, IDUsuario,IDEmpresa); //Verifica se há ponto registrado no dia
                    //adpFrequencia.FillFrequenciaDia(TBFrequencia, HoraBatida.Date.Date, IDUsuario); //Verifica se há ponto registrado no dia
                    //TBFrequencia.EndLoadData();
                    //}

                    //
                    // else
                    //{
                    //DiaSeguinte = false;
                    //}
                    //}
                    //else
                    //{

                    //

                    #endregion
                    //------------------------------------------------------------------------
                    //--16/01/2014 - Alterei os limites entre entrada e saida de 2 para 1 hora.

                    //troquei para view por dia, para os que fazem regime de expediente.
                    //Procura por registros realizados no dia corrente

                    try
                    {
                        adpvwFrequenciaDia.Connection.Open();
                        adpvwFrequenciaDia.FillIDVinculoUsuario(vwFrequenciaDia, vwUsuarioGrid[vinculo].IDVinculoUsuario, HoraBatida.Date.Date);
                        adpvwFrequenciaDia.Connection.Close();
                    }
                    catch
                    {
                        adpvwFrequenciaDia.Connection.Close();
                        return "Jornada de trabalho não identificada no banco de dados. Tente Novamente!";
                    }


                    //adpFrequencia.FillFrequenciaDiaIDempresa(TBFrequencia, HoraBatida.Date.Date, IDUsuario, IDEmpresa, vwUsuarioGrid[vinculo].IDVinculoUsuario); //Verifica se há ponto registrado no dia

                    if (vwFrequenciaDia.Rows.Count == 0 && !DiaSeguinte) //Se número de linhas igual zero, não há registro de frequência para o dia -- Se DiaSeguinte verdadeiro, insere o fim da jornada no dia anterior
                    {
                        if (vwUsuarioGrid[vinculo].TotHorasDiarias >= 8)
                        {
                            if (Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.ToShortDateString(), HoraPadraoSaidaManha)) >= HoraBatida) //Se menor, tratar ou entrada ou saída da manhã (Entrada 1  - saída 1)
                            {
                                Limite = "";

                                if ((Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.ToShortDateString(), HoraPadraoSaidaManha)) > HoraBatida))
                                    Limite = Convert.ToString(Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.ToShortDateString(), HoraPadraoSaidaManha)) - HoraBatida);
                                else
                                    Limite = Convert.ToString(HoraBatida - Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.ToShortDateString(), HoraPadraoSaidaManha)));

                                if (Convert.ToDateTime(Limite) >= Convert.ToDateTime("01:00:00.000")) //Entrada da manhã - Entrada 1
                                {
                                    if (PermiteRegistroLimite(HoraBatida, Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.Date.ToShortDateString(), HoraPadraoEntradaManha)), Convert.ToDateTime(LimitePadraoEntradaSaida), vwUsuarioGrid[vinculo].AcessoEspecial))
                                    {
                                        //Caso insert retorne 0, manda msg para repetir operação. Caso contrário segue rotina
                                        if (adpFrequencia.Insert(IDUsuario, null, HoraBatida.Date, HoraBatida, null, null, null, null, null, HoraBatida.Month, HoraBatida.Year, IDEmpresa, vwUsuarioGrid[vinculo].IDVinculoUsuario) == 0)
                                        {
                                            msg = "Nao Inserido";
                                        }

                                        switch (msg)
                                        {
                                            case "Horario":
                                                msg = String.Format("{0}, entrada 1 efetivada.", NomeUsuario.Trim());
                                                break;
                                            case "Tolerancia":
                                                msg = String.Format("{0}, entrada 1 efetivada.", NomeUsuario.Trim());
                                                break;
                                            case "Atraso inf 1 hora":
                                                msg = String.Format("{0}, entrada 1 efetivada.", NomeUsuario.Trim());
                                                //msg = String.Format("{0}, seu Registro de Entrada Manhã foi realizado com atraso inferior a 1 Hora.", NomeUsuario.Trim());
                                                break;
                                            case "Atraso sup 1 hora":
                                                msg = String.Format("{0}, entrada 1 efetivada.", NomeUsuario.Trim());
                                                //msg = String.Format("{0}, seu Registro de Entrada Manhã foi realizado com atraso superior a 1 Hora.", NomeUsuario.Trim());
                                                break;
                                            case "Nao Inserido":
                                                msg = String.Format("{0}, marcação não foi efetivada. Por favor, repita a operação.", NomeUsuario.Trim());
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        switch (msg)
                                        {
                                            case "excedido":
                                                msg = String.Format("{0}, limite para registro excedido em {1}", NomeUsuario.Trim(), Limite);
                                                break;
                                            case "antecipado":
                                                msg = String.Format("{0}, permissão para realizar registro em {1}", NomeUsuario.Trim(), Limite);
                                                break;
                                            case "turno justificado":
                                                msg = String.Format("{0}, Turno Justificado, Marcação de Ponto não realizada.", NomeUsuario.Trim());
                                                break;
                                            case "dia justificado":
                                                msg = String.Format("{0}, Jornada justificada na data corrente.", NomeUsuario.Trim());
                                                break;
                                        }
                                    }
                                }
                                else  // Saída da manhã - Saída 1
                                {
                                    if (adpFrequencia.Insert(IDUsuario, null, HoraBatida.Date, null, HoraBatida, null, null, null, null, HoraBatida.Month, HoraBatida.Year, IDEmpresa, vwUsuarioGrid[vinculo].IDVinculoUsuario) == 0)
                                        msg = String.Format("{0}, marcação não efetivada. Por favor, repita a operação.", NomeUsuario.Trim());
                                    else
                                        msg = String.Format("{0}, saída 1 efetivada.", NomeUsuario.Trim());
                                }
                            }
                            else if (Convert.ToDateTime(HoraPadraoSaidaManha) < HoraBatida) // Se menor, cai direto para a parte da tarde - Segundo Período
                            {
                                if (Convert.ToDateTime(HoraPadraoSaidTarde) > HoraBatida)
                                {
                                    Limite = "";
                                    if (Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.ToShortDateString(), HoraPadraoSaidTarde)) > HoraBatida)
                                        Limite = Convert.ToString(Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.ToShortDateString(), HoraPadraoSaidTarde)) - HoraBatida);
                                    else
                                        Limite = Convert.ToString(HoraBatida - Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.ToShortDateString(), HoraPadraoSaidTarde)));

                                    if (Convert.ToDateTime(Limite) >= Convert.ToDateTime("01:00:00.000")) //Entrada da tarde - Entrada 2
                                    {
                                        if (PermiteRegistroLimite(HoraBatida, Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.Date.ToShortDateString(), HoraPadraoEntradaTarde)), Convert.ToDateTime(LimitePadraoEntradaSaida), vwUsuarioGrid[vinculo].AcessoEspecial))
                                        {
                                            if (adpFrequencia.Insert(IDUsuario, null, HoraBatida.Date, null, null, HoraBatida, null, null, null, HoraBatida.Month, HoraBatida.Year, IDEmpresa, vwUsuarioGrid[vinculo].IDVinculoUsuario) == 0)
                                            {
                                                msg = "Nao Inserido";
                                            }
                                            switch (msg)
                                            {
                                                case "Horario":

                                                    msg = String.Format("{0}, entrada 2 efetivada.", NomeUsuario.Trim());
                                                    break;
                                                case "Tolerancia":
                                                    msg = String.Format("{0}, entrada 2 efetivada.", NomeUsuario.Trim());
                                                    //msg = String.Format("{0}, seu Registro de Entrada Tarde foi realizado com sucesso.", NomeUsuario.Trim());
                                                    break;
                                                case "Atraso inf 1 hora":
                                                    msg = String.Format("{0}, entrada 2 efetivada.", NomeUsuario.Trim());
                                                    //msg = String.Format("{0}, seu Registro de Entrada Tarde foi realizado com atraso inferior a 1 Hora.", NomeUsuario.Trim());
                                                    break;
                                                case "Atraso sup 1 hora":
                                                    msg = String.Format("{0}, entrada 2 efetivada.", NomeUsuario.Trim());
                                                    //msg = String.Format("{0}, seu Registro de Entrada Tarde foi realizado com atraso superior a 1 Hora.", NomeUsuario.Trim());
                                                    break;
                                                case "Nao Inserido":
                                                    msg = String.Format("{0}, marcação não efetivada. Por favor, repita a operação.", NomeUsuario.Trim());
                                                    break;
                                            }
                                        }

                                    }
                                    else
                                    {
                                        if (adpFrequencia.Insert(IDUsuario, null, HoraBatida.Date, null, null, null, HoraBatida, null, null, HoraBatida.Month, HoraBatida.Year, IDEmpresa, vwUsuarioGrid[vinculo].IDVinculoUsuario) == 0)
                                            msg = String.Format("{0}, marcação não efetivada. Por favor, repita a operação.", NomeUsuario.Trim());
                                        else
                                            msg = String.Format("{0}, jornada finalizada.", NomeUsuario.Trim());
                                    }
                                }
                                else
                                {
                                    Limite = "";
                                    Limite = Convert.ToString(HoraBatida - Convert.ToDateTime(HoraPadraoSaidTarde));

                                    if (Convert.ToDateTime(Limite) <= Convert.ToDateTime("01:00:00.000"))
                                    {
                                        if (adpFrequencia.Insert(IDUsuario, null, HoraBatida.Date, null, null, null, HoraBatida, null, null, HoraBatida.Month, HoraBatida.Year, IDEmpresa, vwUsuarioGrid[vinculo].IDVinculoUsuario) == 0)
                                            msg = String.Format("{0}, marcação não efetivada. Por favor, repita a operação", NomeUsuario.Trim());
                                        else
                                            msg = String.Format("{0}, jornada finalizada.", NomeUsuario.Trim());
                                    }
                                    else
                                    {
                                        //17-03-2015 - Verificar aqui... Pode ser que o erro esteja acontecendo AQUI -- ALTERADO - Conforme padrão das outras Condições.

                                        if (adpFrequencia.Insert(IDUsuario, null, HoraBatida.Date, null, null, HoraBatida, null, null, null, HoraBatida.Month, HoraBatida.Year, IDEmpresa, vwUsuarioGrid[vinculo].IDVinculoUsuario) == 0)
                                        {
                                            msg = "Nao Inserido";
                                        }
                                        switch (msg)
                                        {
                                            case "Horario":

                                                msg = String.Format("{0}, entrada 2 efetivada.", NomeUsuario.Trim());
                                                break;
                                            case "Tolerancia":
                                                msg = String.Format("{0}, entrada 2 efetivada.", NomeUsuario.Trim());
                                                //msg = String.Format("{0}, seu Registro de Entrada Tarde foi realizado com sucesso.", NomeUsuario.Trim());
                                                break;
                                            case "Atraso inf 1 hora":
                                                msg = String.Format("{0}, entrada 2 efetivada.", NomeUsuario.Trim());
                                                //msg = String.Format("{0}, seu Registro de Entrada Tarde foi realizado com atraso inferior a 1 Hora.", NomeUsuario.Trim());
                                                break;
                                            case "Atraso sup 1 hora":
                                                msg = String.Format("{0}, entrada 2 efetivada.", NomeUsuario.Trim());
                                                //msg = String.Format("{0}, seu Registro de Entrada Tarde foi realizado com atraso superior a 1 Hora.", NomeUsuario.Trim());
                                                break;
                                            case "Nao Inserido":
                                                msg = String.Format("{0}, marcação não efetivada. Por favor, repita a operação.", NomeUsuario.Trim());
                                                break;
                                        }
                                    }
                                }
                            }
                        }//Pessoas que trabalham 6 horas diretas ou mais
                        else if (HoraBatida <= Convert.ToDateTime("11:00:00.000"))
                        {
                            if (PermiteRegistroLimite(HoraBatida, Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.Date.ToShortDateString(), HoraPadraoEntradaManha)), Convert.ToDateTime(LimitePadraoEntradaSaida), vwUsuarioGrid[vinculo].AcessoEspecial))
                            {
                                if (adpFrequencia.Insert(IDUsuario, null, HoraBatida.Date, HoraBatida, null, null, null, null, null, HoraBatida.Month, HoraBatida.Year, IDEmpresa, vwUsuarioGrid[vinculo].IDVinculoUsuario) == 0)
                                {
                                    msg = "Nao Inserido";
                                }

                                switch (msg)
                                {
                                    case "Horario":
                                        msg = String.Format("{0}, entrada 1 efetivada.", NomeUsuario.Trim());
                                        break;
                                    case "Tolerancia":
                                        msg = String.Format("{0}, entrada 1 efetivada.", NomeUsuario.Trim());
                                        //msg = String.Format("{0}, seu Registro de Entrada Manhã foi realizado com sucesso.", NomeUsuario.Trim());
                                        break;
                                    case "Atraso inf 1 hora":
                                        msg = String.Format("{0}, entrada 1 efetivada.", NomeUsuario.Trim());
                                        //msg = String.Format("{0}, seu Registro de Entrada Manhã foi realizado com atraso inferior a 1 Hora.", NomeUsuario.Trim());
                                        break;
                                    case "Atraso sup 1 hora":
                                        msg = String.Format("{0}, entrada 1 efetivada.", NomeUsuario.Trim());
                                        //msg = String.Format("{0}, seu Registro de Entrada Manhã foi realizado com atraso superior a 1 Hora.", NomeUsuario.Trim());
                                        break;
                                    case "Nao Inserido":
                                        msg = String.Format("{0}, marcação não efetivada. Por favor, repita a operação.", NomeUsuario.Trim());
                                        break;
                                }
                            }
                            else
                            {
                                switch (msg)
                                {
                                    case "excedido":
                                        msg = String.Format("{0}, limite para registro excedido em {1}", NomeUsuario.Trim(), Limite);
                                        break;
                                    case "antecipado":
                                        msg = String.Format("{0}, permissão para realizar registro em {1}", NomeUsuario.Trim(), Limite);
                                        break;
                                    case "turno justificado":
                                        msg = String.Format("{0}, turno justificado, marcação de ponto não realizada.", NomeUsuario.Trim());
                                        break;
                                    case "dia justificado":
                                        msg = String.Format("{0}, Jornada justificada na data corrente.", NomeUsuario.Trim());
                                        break;
                                }

                            }
                        }
                        else //Se passou das onze, registra na parte da tarde
                        {
                            if (PermiteRegistroLimite(HoraBatida, Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.Date.ToShortDateString(), HoraPadraoEntradaTarde)), Convert.ToDateTime(LimitePadraoEntradaSaida), vwUsuarioGrid[vinculo].AcessoEspecial))
                            {
                                if (adpFrequencia.Insert(IDUsuario, null, HoraBatida.Date, null, null, HoraBatida, null, null, null, HoraBatida.Month, HoraBatida.Year, IDEmpresa, vwUsuarioGrid[vinculo].IDVinculoUsuario) == 0)
                                {
                                    msg = "Nao Inserido";
                                }
                                switch (msg)
                                {
                                    case "Horario":

                                        msg = String.Format("{0}, entrada 2 efetivada.", NomeUsuario.Trim());
                                        break;
                                    case "Tolerancia":
                                        msg = String.Format("{0}, entrada 2 efetivada.", NomeUsuario.Trim());
                                        //msg = String.Format("{0}, seu Registro de Entrada Tarde foi realizado com sucesso.", NomeUsuario.Trim());
                                        break;
                                    case "Atraso inf 1 hora":
                                        msg = String.Format("{0}, entrada 2 efetivada.", NomeUsuario.Trim());
                                        //msg = String.Format("{0}, seu Registro de Entrada Tarde foi realizado com atraso inferior a 1 Hora.", NomeUsuario.Trim());
                                        break;
                                    case "Atraso sup 1 hora":
                                        msg = String.Format("{0}, entrada 2 efetivada.", NomeUsuario.Trim());
                                        //msg = String.Format("{0}, seu Registro de Entrada Tarde foi realizado com atraso superior a 1 Hora.", NomeUsuario.Trim());
                                        break;
                                    case "Nao Inserido":
                                        msg = String.Format("{0}, marcação não efetivada. Por favor, repita a operação.", NomeUsuario.Trim());
                                        break;
                                }
                            }
                            else
                            {
                                switch (msg)
                                {
                                    case "excedido":
                                        msg = String.Format("{0}, limite para registro excedido em {1}", NomeUsuario.Trim(), Limite);
                                        break;
                                    case "antecipado":
                                        msg = String.Format("{0}, permissão para realizar registro em {1}", NomeUsuario.Trim(), Limite);
                                        break;
                                    case "turno justificado":
                                        msg = String.Format("{0}, turno justificado, marcação de ponto não realizada.", NomeUsuario.Trim());
                                        break;
                                    case "dia justificado":
                                        msg = String.Format("{0}, Jornada justificada na data corrente.", NomeUsuario.Trim());
                                        break;
                                }
                            }
                        }
                    }

                    else if (vwFrequenciaDia.Rows.Count > 0) // Se maior tratativa de outros horários
                    {
                        //Se houver uma falta lançada, não registra frequência
                        if (!vwFrequenciaDia[0].IsIDMotivoFaltaNull())
                        {
                            if (FaltaLancada(vwFrequenciaDia[0].IDMotivoFalta)) // Se o motivo for 47, não bate ponto.
                                return string.Format("{0}, há uma falta lançada na data corrente. Impossível registrar frequência!", NomeUsuario);
                        }

                        #region 8 horas Diárias
                        if (vwUsuarioGrid[vinculo].TotHorasDiarias >= 8) //Tratativa para pessoas que trabalham 8 horas diretas
                        {
                            if (vwFrequenciaDia[0].IsHoraSaidaManhaNull() && !vwFrequenciaDia[0].IsHoraEntraManhaNull() && HoraBatida < Convert.ToDateTime(HoraPadraoSaidTarde).AddHours(1))
                            {
                                Limite = "";
                                LimiteIntervalo = string.Empty;
                                Limite = Convert.ToString(HoraBatida - vwFrequenciaDia[0].HoraEntraManha);

                                if (Convert.ToDateTime(Limite) <= Convert.ToDateTime("06:30:00.000") && vwFrequenciaDia[0].IsHoraSaidaManhaNull() && (vwFrequenciaDia[0].HoraEntraManha != HoraBatida)) // Saída Manhã - Saída 1
                                {
                                    LimiteIntervalo = Convert.ToString(HoraBatida - vwFrequenciaDia[0].HoraEntraManha);

                                    if (Convert.ToDateTime(LimiteIntervalo) > Convert.ToDateTime("00:10:00.000"))
                                    {

                                        if (PermiteRegistroLimite(HoraBatida, Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.Date.ToShortDateString(), HoraPadraoSaidaManha)), Convert.ToDateTime(LimitePadraoEntradaSaida), vwUsuarioGrid[vinculo].AcessoEspecial))
                                        {
                                            //    //Total Horas
                                            DateTime? Entrada1, Saida1, Entrada2, Saida2;

                                            if (!vwFrequenciaDia[0].IsHoraEntraManhaNull())
                                                Entrada1 = vwFrequenciaDia[0].HoraEntraManha;
                                            else
                                                Entrada1 = null;

                                            if (!vwFrequenciaDia[0].IsHoraSaidaManhaNull())
                                                Saida1 = vwFrequenciaDia[0].HoraSaidaManha;
                                            else
                                                Saida1 = HoraBatida;


                                            Entrada2 = null;

                                            Saida2 = null;

                                            //AQUI - FILTRAR OS POR IDVINCULO TAMBÉM
                                            //Se retorno > 0 mensagem de sucesso.. Senão, apresentar msg de erro.
                                            if (adpFrequencia.UpdateSaidaManha(HoraBatida, TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Manha", HoraBatida.Date.Date), IDUsuario, HoraBatida.Date, vwUsuarioGrid[vinculo].IDVinculoUsuario) == 0)
                                            {
                                                msg = "Nao Atualizado";
                                            }
                                            else
                                            {

                                                TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Manha", HoraBatida.Date.Date);

                                                //    adpFrequencia.UpdateTotHoras(TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Manha", HoraBatida.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);
                                            }


                                            switch (msg)
                                            {
                                                case "Carga Total":
                                                    msg = String.Format("{0}, saída 1 efetivada.", NomeUsuario.Trim());
                                                    break;
                                                case "Carga Incompleta":
                                                    msg = String.Format("{0}, saída 1 efetivada.", NomeUsuario.Trim());
                                                    //msg = String.Format("{0}, frequência registrada com a carga horária total incompleta. Saída da Manhã.", NomeUsuario.Trim());
                                                    break;
                                                case "Nao Atualizado":
                                                    msg = String.Format("{0}, marcação não realizada. Repita o processo.", NomeUsuario.Trim());
                                                    break;
                                            }
                                        }
                                        else
                                        {
                                            switch (msg)
                                            {
                                                case "excedido":
                                                    msg = String.Format("{0}, limite para registro excedido em {1}", NomeUsuario.Trim(), Limite);
                                                    break;
                                                case "antecipado":
                                                    msg = String.Format("{0}, permissão para realizar registro em {1}", NomeUsuario.Trim(), Limite);
                                                    break;
                                                case "turno justificado":
                                                    msg = String.Format("{0}, turno justificado, marcação de ponto não realizada.", NomeUsuario.Trim());
                                                    break;
                                                case "dia justificado":
                                                    msg = String.Format("{0}, Jornada justificada na data corrente.", NomeUsuario.Trim());
                                                    break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        msg = string.Format("{0}, respeite o intervalo de 10 minutos entre as marcações.", NomeUsuario);
                                    }
                                }
                                else if (vwFrequenciaDia[0].IsHoraEntradaTardeNull() || vwFrequenciaDia[0].IsHoraSaidaTardeNull())
                                {
                                    Limite = "";
                                    if (Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.ToShortDateString(), HoraPadraoSaidTarde)) > HoraBatida)
                                        Limite = Convert.ToString(Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.ToShortDateString(), HoraPadraoSaidTarde)) - HoraBatida);
                                    else
                                        Limite = Convert.ToString(HoraBatida - Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.ToShortDateString(), HoraPadraoSaidTarde)));

                                    if (Convert.ToDateTime(Limite) >= Convert.ToDateTime("01:00:00.000") && vwFrequenciaDia[0].IsHoraEntradaTardeNull() && (vwFrequenciaDia[0].HoraEntraManha != HoraBatida) && vwFrequenciaDia[0].IsHoraSaidaTardeNull()) //Entrada tarde Entrada 2
                                    {
                                        Limite = "";
                                        if (Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.ToShortDateString(), HoraPadraoSaidTarde)) > HoraBatida)
                                            Limite = Convert.ToString(Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.ToShortDateString(), HoraPadraoSaidTarde)) - HoraBatida);
                                        else
                                            Limite = Convert.ToString(HoraBatida - Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.ToShortDateString(), HoraPadraoSaidTarde)));

                                        if (!vwFrequenciaDia[0].IsHoraSaidaManhaNull()) ///Se não for nulo, faz o comparativo.
                                        {
                                            LimiteEntradaSaida = string.Empty;
                                            LimiteEntradaSaida = Convert.ToString(HoraBatida - vwFrequenciaDia[0].HoraSaidaManha);

                                            if (Convert.ToDateTime(LimiteEntradaSaida) >= Convert.ToDateTime(LimitePadraoAlmoco))
                                            {
                                                if (Convert.ToDateTime(Limite) >= Convert.ToDateTime("01:00:00.000"))
                                                {
                                                    if (PermiteRegistroLimite(HoraBatida, Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.Date.ToShortDateString(), HoraPadraoEntradaTarde)), Convert.ToDateTime(LimitePadraoEntradaSaida), vwUsuarioGrid[vinculo].AcessoEspecial))
                                                    {
                                                        //Prevendo hora de almoço superior ou igual a uma hora
                                                        if (vwFrequenciaDia[0].IsHoraSaidaManhaNull())
                                                        {
                                                            if (adpFrequencia.UpdateEntradaTarde(HoraBatida, vwFrequenciaDia[0].TotalHorasDiaSegundos, IDUsuario, HoraBatida.Date, vwUsuarioGrid[vinculo].IDVinculoUsuario) > 0)
                                                                msg = string.Format("{0}, entrada 2 efetivada.", NomeUsuario);
                                                            else
                                                                msg = string.Format("{0}, marcação não realizada. Por favor, repita a operação", NomeUsuario);
                                                        }
                                                        else
                                                        {
                                                            LimiteEntradaSaida = string.Empty;
                                                            LimiteEntradaSaida = Convert.ToString(HoraBatida - vwFrequenciaDia[0].HoraSaidaManha);

                                                            if (Convert.ToDateTime(LimiteEntradaSaida) >= Convert.ToDateTime(string.Format("{0} 01:00:00", HoraBatida.ToShortDateString())))
                                                            {
                                                                if (adpFrequencia.UpdateEntradaTarde(HoraBatida, vwFrequenciaDia[0].TotalHorasDiaSegundos, IDUsuario, HoraBatida.Date, vwUsuarioGrid[vinculo].IDVinculoUsuario) > 0)
                                                                    msg = string.Format("{0}, entrada 2 efetivada.", NomeUsuario);
                                                                else
                                                                    msg = string.Format("{0}, marcação não realizada. Repita a operação", NomeUsuario);
                                                            }
                                                            else
                                                                msg = string.Format("{0}, horário de almoço. Permissão para registro em {1}.", NomeUsuario, Convert.ToDateTime(LimiteEntradaSaida).ToShortTimeString());
                                                        }
                                                    }
                                                    else
                                                    {
                                                        switch (msg)
                                                        {
                                                            case "excedido":
                                                                msg = String.Format("{0}, limite para registro excedido em {1}", NomeUsuario.Trim(), Limite);
                                                                break;
                                                            case "antecipado":
                                                                msg = String.Format("{0}, permissão para realizar registro em {1}", NomeUsuario.Trim(), Limite);
                                                                break;
                                                            case "turno justificado":
                                                                msg = String.Format("{0}, turno justificado, marcação de ponto não realizada.", NomeUsuario.Trim());
                                                                break;
                                                            case "dia justificado":
                                                                msg = String.Format("{0}, Jornada justificada na data corrente.", NomeUsuario.Trim());
                                                                break;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (PermiteRegistroLimite(HoraBatida, Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.Date.ToShortDateString(), HoraPadraoSaidTarde)), Convert.ToDateTime(LimitePadraoEntradaSaida), vwUsuarioGrid[vinculo].AcessoEspecial))
                                                    {
                                                        //Total Horas
                                                        DateTime? Entrada1, Saida1, Entrada2, Saida2;

                                                        if (!vwFrequenciaDia[0].IsHoraEntraManhaNull())
                                                            Entrada1 = vwFrequenciaDia[0].HoraEntraManha;
                                                        else
                                                            Entrada1 = null;

                                                        if (!vwFrequenciaDia[0].IsHoraSaidaManhaNull())
                                                            Saida1 = vwFrequenciaDia[0].HoraSaidaManha;
                                                        else
                                                            Saida1 = null;

                                                        if (!vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                                                            Entrada2 = vwFrequenciaDia[0].HoraEntradaTarde;
                                                        else
                                                            Entrada2 = null;

                                                        Saida2 = HoraBatida;

                                                        if (adpFrequencia.UpdateSaidaTarde(HoraBatida, TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), IDUsuario, vwFrequenciaDia[0].IDFrequencia) == 0)
                                                        {
                                                            msg = "Nao Atualizado";
                                                        }
                                                        else
                                                        {


                                                            //adpFrequencia.UpdateTotHoras(TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);
                                                            TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date);

                                                            switch (msg)
                                                            {
                                                                case "Carga Total":
                                                                    msg = String.Format("{0}, jornada finalizada.", NomeUsuario.Trim());
                                                                    break;
                                                                case "Carga Incompleta":
                                                                    msg = String.Format("{0}, jornada finalizada com a carga horária total incompleta.", NomeUsuario.Trim());
                                                                    break;
                                                                case "Nao Atualizado":
                                                                    msg = String.Format("{0}, marcação não realizada. Repita a operação", NomeUsuario.Trim());
                                                                    break;
                                                            }

                                                        }
                                                    }
                                                    else
                                                    {
                                                        switch (msg)
                                                        {
                                                            case "excedido":
                                                                msg = String.Format("{0}, limite para registro excedido em {1}", NomeUsuario.Trim(), Limite);
                                                                break;
                                                            case "antecipado":
                                                                msg = String.Format("{0}, permissão para realizar registro em {1}", NomeUsuario.Trim(), Limite);
                                                                break;
                                                            case "turno justificado":
                                                                msg = String.Format("{0}, turno justificado, marcação de ponto não realizada.", NomeUsuario.Trim());
                                                                break;
                                                            case "dia justificado":
                                                                msg = String.Format("{0}, Jornada justificada na data corrente.", NomeUsuario.Trim());
                                                                break;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (Convert.ToDateTime(Limite) >= Convert.ToDateTime("01:00:00.000"))
                                            {
                                                if (PermiteRegistroLimite(HoraBatida, Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.Date.ToShortDateString(), HoraPadraoEntradaTarde)), Convert.ToDateTime(LimitePadraoEntradaSaida), vwUsuarioGrid[vinculo].AcessoEspecial))
                                                {
                                                    //Prevendo hora de almoço superior ou igual a uma hora
                                                    if (vwFrequenciaDia[0].IsHoraSaidaManhaNull())
                                                    {
                                                        if (adpFrequencia.UpdateEntradaTarde(HoraBatida, vwFrequenciaDia[0].TotalHorasDiaSegundos, IDUsuario, HoraBatida.Date, vwUsuarioGrid[vinculo].IDVinculoUsuario) > 0)
                                                            msg = string.Format("{0}, entrada 2 efetivada.", NomeUsuario);
                                                        else
                                                            msg = string.Format("{0}, marcação não efetivada. Repita a operação", NomeUsuario);
                                                    }
                                                    else
                                                    {
                                                        LimiteEntradaSaida = string.Empty;
                                                        LimiteEntradaSaida = Convert.ToString(HoraBatida - vwFrequenciaDia[0].HoraSaidaManha);

                                                        if (Convert.ToDateTime(LimiteEntradaSaida) >= Convert.ToDateTime(string.Format("{0} 01:00:00", HoraBatida.ToShortDateString())))
                                                        {
                                                            if (adpFrequencia.UpdateEntradaTarde(HoraBatida, vwFrequenciaDia[0].TotalHorasDiaSegundos, IDUsuario, HoraBatida.Date, vwUsuarioGrid[vinculo].IDVinculoUsuario) > 0)
                                                                msg = string.Format("{0}, entrada 2 efetivada.", NomeUsuario);
                                                            else
                                                                msg = string.Format("{0}, marcação não efetivada. Repita a operação", NomeUsuario);
                                                        }
                                                        else
                                                            msg = string.Format("{0}, horário de almoço. Permissão para registro em {1}.", NomeUsuario, Convert.ToDateTime(LimiteEntradaSaida).ToShortTimeString());
                                                    }
                                                }
                                                else
                                                {
                                                    switch (msg)
                                                    {
                                                        case "excedido":
                                                            msg = String.Format("{0}, limite para registro excedido em {1}", NomeUsuario.Trim(), Limite);
                                                            break;
                                                        case "antecipado":
                                                            msg = String.Format("{0}, permissão para realizar registro em {1}", NomeUsuario.Trim(), Limite);
                                                            break;
                                                        case "turno justificado":
                                                            msg = String.Format("{0}, turno justificado, marcação de ponto não realizada.", NomeUsuario.Trim());
                                                            break;
                                                        case "dia justificado":
                                                            msg = String.Format("{0}, Jornada justificada na data corrente.", NomeUsuario.Trim());
                                                            break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else if (vwFrequenciaDia[0].IsHoraSaidaTardeNull() && (vwFrequenciaDia[0].HoraEntraManha != HoraBatida) && !vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                                    {
                                        LimiteIntervalo = string.Empty;

                                        LimiteIntervalo = Convert.ToString(HoraBatida - vwFrequenciaDia[0].HoraEntradaTarde);

                                        if (Convert.ToDateTime(LimiteIntervalo) > Convert.ToDateTime("00:10:00.000"))
                                        {
                                            if (PermiteRegistroLimite(HoraBatida, Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.Date.ToShortDateString(), HoraPadraoSaidTarde)), Convert.ToDateTime(LimitePadraoEntradaSaida), vwUsuarioGrid[vinculo].AcessoEspecial))
                                            {

                                                //Total Horas
                                                DateTime? Entrada1, Saida1, Entrada2, Saida2;

                                                if (!vwFrequenciaDia[0].IsHoraEntraManhaNull())
                                                    Entrada1 = vwFrequenciaDia[0].HoraEntraManha;
                                                else
                                                    Entrada1 = null;

                                                if (!vwFrequenciaDia[0].IsHoraSaidaManhaNull())
                                                    Saida1 = vwFrequenciaDia[0].HoraSaidaManha;
                                                else
                                                    Saida1 = null;

                                                if (!vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                                                    Entrada2 = vwFrequenciaDia[0].HoraEntradaTarde;
                                                else
                                                    Entrada2 = null;

                                                Saida2 = HoraBatida;

                                                if (adpFrequencia.UpdateSaidaTarde(HoraBatida, TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), IDUsuario, vwFrequenciaDia[0].IDFrequencia) == 0)
                                                {
                                                    msg = "Nao Atualizado";
                                                }
                                                else
                                                {


                                                    //adpFrequencia.UpdateTotHoras(TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);
                                                    TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date);

                                                    switch (msg)
                                                    {
                                                        case "Carga Total":
                                                            msg = String.Format("{0}, jornada finalizada.", NomeUsuario.Trim());
                                                            break;
                                                        case "Carga Incompleta":
                                                            msg = String.Format("{0}, jornada finalizada com a carga horária total incompleta.", NomeUsuario.Trim());
                                                            break;
                                                        case "Nao Atualizado":
                                                            msg = String.Format("{0}, marcação não efetivada. Repita a operação", NomeUsuario.Trim());
                                                            break;
                                                    }

                                                }
                                            }
                                            else
                                            {
                                                switch (msg)
                                                {
                                                    case "excedido":
                                                        msg = String.Format("{0}, limite para registro excedido em {1}", NomeUsuario.Trim(), Limite);
                                                        break;
                                                    case "antecipado":
                                                        msg = String.Format("{0}, permissão para realizar registro em {1}", NomeUsuario.Trim(), Limite);
                                                        break;
                                                    case "turno justificado":
                                                        msg = String.Format("{0}, turno justificado, marcação de ponto não realizada.", NomeUsuario.Trim());
                                                        break;
                                                    case "dia justificado":
                                                        msg = String.Format("{0}, Jornada justificada na data corrente.", NomeUsuario.Trim());
                                                        break;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            msg = string.Format("{0}, respeite o intervalo de 10 minutos entre as marcações.", NomeUsuario);
                                        }
                                    }
                                    else if (vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                                    {
                                        if (PermiteRegistroLimite(HoraBatida, Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.Date.ToShortDateString(), HoraPadraoSaidTarde)), Convert.ToDateTime(LimitePadraoEntradaSaida), vwUsuarioGrid[vinculo].AcessoEspecial))
                                        {

                                            //Total Horas
                                            DateTime? Entrada1, Saida1, Entrada2, Saida2;

                                            if (!vwFrequenciaDia[0].IsHoraEntraManhaNull())
                                                Entrada1 = vwFrequenciaDia[0].HoraEntraManha;
                                            else
                                                Entrada1 = null;

                                            if (!vwFrequenciaDia[0].IsHoraSaidaManhaNull())
                                                Saida1 = vwFrequenciaDia[0].HoraSaidaManha;
                                            else
                                                Saida1 = null;

                                            if (!vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                                                Entrada2 = vwFrequenciaDia[0].HoraEntradaTarde;
                                            else
                                                Entrada2 = null;

                                            Saida2 = HoraBatida;

                                            if (adpFrequencia.UpdateSaidaTarde(HoraBatida, TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), IDUsuario, vwFrequenciaDia[0].IDFrequencia) == 0)
                                            {
                                                msg = "Nao Atualizado";
                                            }
                                            else
                                            {

                                                //adpFrequencia.UpdateTotHoras(TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);
                                                TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date);
                                                switch (msg)
                                                {
                                                    case "Carga Total":
                                                        msg = String.Format("{0}, jornada finalizada.", NomeUsuario.Trim());
                                                        break;
                                                    case "Carga Incompleta":
                                                        msg = String.Format("{0}, jornada finalizada com a carga horária total incompleta.", NomeUsuario.Trim());
                                                        break;
                                                    case "Nao Atualizado":
                                                        msg = String.Format("{0}, marcação não efetivada. Repita a operação", NomeUsuario.Trim());
                                                        break;
                                                }

                                            }
                                        }
                                        else
                                        {
                                            switch (msg)
                                            {
                                                case "excedido":
                                                    msg = String.Format("{0}, limite para registro excedido em {1}", NomeUsuario.Trim(), Limite);
                                                    break;
                                                case "antecipado":
                                                    msg = String.Format("{0}, permissão para realizar registro em {1}", NomeUsuario.Trim(), Limite);
                                                    break;
                                                case "turno justificado":
                                                    msg = String.Format("{0}, turno justificado, marcação de ponto não realizada.", NomeUsuario.Trim());
                                                    break;
                                                case "dia justificado":
                                                    msg = String.Format("{0}, Jornada justificada na data corrente.", NomeUsuario.Trim());
                                                    break;
                                            }
                                        }
                                    }
                                }
                                else if (!vwFrequenciaDia[0].IsHoraEntradaTardeNull() || !vwFrequenciaDia[0].IsHoraSaidaTardeNull())
                                {
                                    //msg = string.Format("{0}, jornada completa para o período.", NomeUsuario); Retirado ... Poder realizar a saida da tarde.

                                    if (PermiteRegistroLimite(HoraBatida, Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.Date.ToShortDateString(), HoraPadraoSaidTarde)), Convert.ToDateTime(LimitePadraoEntradaSaida), vwUsuarioGrid[vinculo].AcessoEspecial))
                                    {
                                        //Total Horas
                                        DateTime? Entrada1, Saida1, Entrada2, Saida2;

                                        if (!vwFrequenciaDia[0].IsHoraEntraManhaNull())
                                            Entrada1 = vwFrequenciaDia[0].HoraEntraManha;
                                        else
                                            Entrada1 = null;

                                        if (!vwFrequenciaDia[0].IsHoraSaidaManhaNull())
                                            Saida1 = vwFrequenciaDia[0].HoraSaidaManha;
                                        else
                                            Saida1 = null;

                                        if (!vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                                            Entrada2 = vwFrequenciaDia[0].HoraEntradaTarde;
                                        else
                                            Entrada2 = null;

                                        Saida2 = HoraBatida;

                                        if (adpFrequencia.UpdateSaidaTarde(HoraBatida, TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), IDUsuario, vwFrequenciaDia[0].IDFrequencia) == 0)
                                        {
                                            msg = "Nao Atualizado";
                                        }
                                        else
                                        {

                                            //adpFrequencia.UpdateTotHoras(TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);
                                            TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date);
                                            switch (msg)
                                            {
                                                case "Carga Total":
                                                    msg = String.Format("{0}, jornada finalizada.", NomeUsuario.Trim());
                                                    break;
                                                case "Carga Incompleta":
                                                    msg = String.Format("{0}, jornada finalizada com a carga horária total incompleta.", NomeUsuario.Trim());
                                                    break;
                                                case "Nao Atualizado":
                                                    msg = String.Format("{0}, marcação não realizada. Repita a operação", NomeUsuario.Trim());
                                                    break;
                                            }

                                        }
                                    }
                                    else
                                    {
                                        switch (msg)
                                        {
                                            case "excedido":
                                                msg = String.Format("{0}, limite para registro excedido em {1}", NomeUsuario.Trim(), Limite);
                                                break;
                                            case "antecipado":
                                                msg = String.Format("{0}, permissão para realizar registro em {1}", NomeUsuario.Trim(), Limite);
                                                break;
                                            case "turno justificado":
                                                msg = String.Format("{0}, turno justificado, marcação de ponto não realizada.", NomeUsuario.Trim());
                                                break;
                                            case "dia justificado":
                                                msg = String.Format("{0}, Jornada justificada na data corrente.", NomeUsuario.Trim());
                                                break;
                                        }
                                    }

                                }
                            }
                            else if (vwFrequenciaDia[0].IsHoraSaidaManhaNull() && vwFrequenciaDia[0].IsHoraEntraManhaNull() && HoraBatida < Convert.ToDateTime(HoraPadraoSaidTarde).AddHours(1) && !vwFrequenciaDia[0].IsIDMotivoFaltaNull()) //Prevendo a justificativa em caso de não batida matinal
                            {
                                if (PermiteRegistroLimite(HoraBatida, Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.Date.ToShortDateString(), HoraPadraoSaidaManha)), Convert.ToDateTime(LimitePadraoEntradaSaida), vwUsuarioGrid[vinculo].AcessoEspecial))
                                {
                                    if (vwFrequenciaDia[0].IDTPJustificativa != 1 && (vwFrequenciaDia[0].TurnoJustificado == "Mat" || vwFrequenciaDia[0].TurnoJustificado == "0"))
                                    {
                                        //Total Horas
                                        DateTime? Entrada1, Saida1, Entrada2, Saida2;

                                        if (!vwFrequenciaDia[0].IsHoraEntraManhaNull())
                                            Entrada1 = vwFrequenciaDia[0].HoraEntraManha;
                                        else
                                            Entrada1 = null;

                                        if (!vwFrequenciaDia[0].IsHoraSaidaManhaNull())
                                            Saida1 = vwFrequenciaDia[0].HoraSaidaManha;
                                        else
                                            Saida1 = HoraBatida;


                                        Entrada2 = null;

                                        Saida2 = null;
                                        //Se retorno > 0 mensagem de sucesso.. Senão, apresentar msg de erro.
                                        if (adpFrequencia.UpdateSaidaManha(HoraBatida, TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Manha", HoraBatida.Date.Date), IDUsuario, HoraBatida.Date, vwUsuarioGrid[vinculo].IDVinculoUsuario) == 0)
                                        {
                                            msg = "Nao Atualizado";
                                        }
                                        else
                                        {

                                            //adpFrequencia.UpdateTotHoras(TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Manha", HoraBatida.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);

                                            TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Manha", HoraBatida.Date.Date);
                                        }

                                    }
                                    else if (HoraBatida > Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.ToShortDateString(), vwUsuarioGrid[vinculo].HoraSaidaManha)).AddHours(1) && vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                                    {//Casa haja uma justificativa de meio período, dar a entrada da tarde
                                        if (adpFrequencia.UpdateEntradaTarde(HoraBatida, vwFrequenciaDia[0].TotalHorasDiaSegundos, IDUsuario, HoraBatida.Date, vwUsuarioGrid[vinculo].IDVinculoUsuario) > 0)
                                            msg = string.Format("{0}, entrada 2 efetivada.", NomeUsuario);
                                        else
                                            msg = string.Format("{0}, marcação não efetivada. Repita a operação", NomeUsuario);
                                    }
                                    else if (!vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                                    {
                                        if (vwFrequenciaDia[0].HoraEntradaTarde < HoraBatida.AddMinutes(15))
                                        {
                                            //Saída da tarde
                                            if (PermiteRegistroLimite(HoraBatida, Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.Date.ToShortDateString(), HoraPadraoSaidTarde)), Convert.ToDateTime(LimitePadraoEntradaSaida), vwUsuarioGrid[vinculo].AcessoEspecial))
                                            {
                                                //Total Horas
                                                DateTime? Entrada1, Saida1, Entrada2, Saida2;

                                                if (!vwFrequenciaDia[0].IsHoraEntraManhaNull())
                                                    Entrada1 = vwFrequenciaDia[0].HoraEntraManha;
                                                else
                                                    Entrada1 = null;

                                                if (!vwFrequenciaDia[0].IsHoraSaidaManhaNull())
                                                    Saida1 = vwFrequenciaDia[0].HoraSaidaManha;
                                                else
                                                    Saida1 = null;

                                                if (!vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                                                    Entrada2 = vwFrequenciaDia[0].HoraEntradaTarde;
                                                else
                                                    Entrada2 = null;

                                                Saida2 = HoraBatida;

                                                if (adpFrequencia.UpdateSaidaTarde(HoraBatida, TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), IDUsuario, vwFrequenciaDia[0].IDFrequencia) == 0)
                                                {
                                                    msg = "Nao Atualizado";
                                                }
                                                else
                                                {

                                                    //adpFrequencia.UpdateTotHoras(TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);
                                                    TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date);
                                                    switch (msg)
                                                    {
                                                        case "Carga Total":
                                                            msg = String.Format("{0}, jornada finalizada.", NomeUsuario.Trim());
                                                            break;
                                                        case "Carga Incompleta":
                                                            msg = String.Format("{0}, jornada finalizada com a carga horária total incompleta.", NomeUsuario.Trim());
                                                            break;
                                                        case "Nao Atualizado":
                                                            msg = String.Format("{0}, marcação não efetivada. Repita a operação", NomeUsuario.Trim());
                                                            break;
                                                    }

                                                }
                                            }
                                            else
                                            {
                                                switch (msg)
                                                {
                                                    case "excedido":
                                                        msg = String.Format("{0}, limite para registro excedido em {1}", NomeUsuario.Trim(), Limite);
                                                        break;
                                                    case "antecipado":
                                                        msg = String.Format("{0}, permissão para realizar registro em {1}", NomeUsuario.Trim(), Limite);
                                                        break;
                                                    case "turno justificado":
                                                        msg = String.Format("{0}, turno justificado, marcação de ponto não realizada.", NomeUsuario.Trim());
                                                        break;
                                                    case "dia justificado":
                                                        msg = String.Format("{0}, Jornada justificada na data corrente.", NomeUsuario.Trim());
                                                        break;
                                                }
                                            }
                                        }
                                        //msg = "Turno matutino justificado. Favor realizar a marcação de acordo com os seus padrões de Entrada e Saída";
                                    }

                                    switch (msg)
                                    {
                                        case "Carga Total":
                                            msg = String.Format("{0}, saída 2 efetivada.", NomeUsuario.Trim());
                                            break;
                                        case "Carga Incompleta":
                                            msg = String.Format("{0}, saída 2 efetivada.", NomeUsuario.Trim());
                                            //msg = String.Format("{0}, frequência registrada com a carga horária total incompleta. Saída da Manhã.", NomeUsuario.Trim());
                                            break;
                                        case "Nao Atualizado":
                                            msg = String.Format("{0}, marcação não efetivada. Repita o processo.", NomeUsuario.Trim());
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (msg)
                                    {
                                        case "excedido":
                                            msg = String.Format("{0}, limite para registro excedido em {1}", NomeUsuario.Trim(), Limite);
                                            break;
                                        case "antecipado":
                                            msg = String.Format("{0}, permissão para realizar registro em {1}", NomeUsuario.Trim(), Limite);
                                            break;
                                        case "turno justificado":
                                            msg = String.Format("{0}, turno justificado, marcação de ponto não realizada.", NomeUsuario.Trim());
                                            break;
                                        case "dia justificado":
                                            msg = String.Format("{0}, Jornada justificada na data corrente.", NomeUsuario.Trim());
                                            break;
                                    }
                                }
                            }
                            else if (!vwFrequenciaDia[0].IsHoraEntraManhaNull() || !vwFrequenciaDia[0].IsHoraSaidaManhaNull() || !vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                            {
                                if (vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                                {

                                    Limite = "";
                                    if (HoraBatida <= Convert.ToDateTime(HoraPadraoSaidTarde))
                                    {
                                        Limite = "";
                                        if (Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.ToShortDateString(), HoraPadraoSaidTarde)) > HoraBatida)
                                            Limite = Convert.ToString(Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.ToShortDateString(), HoraPadraoSaidTarde)) - HoraBatida);
                                        else
                                            Limite = Convert.ToString(HoraBatida - Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.ToShortDateString(), HoraPadraoSaidTarde)));

                                        if (!vwFrequenciaDia[0].IsHoraSaidaManhaNull()) ///Se não for nulo, faz o comparativo.
                                        {
                                            LimiteEntradaSaida = string.Empty;
                                            LimiteEntradaSaida = Convert.ToString(HoraBatida - vwFrequenciaDia[0].HoraSaidaManha);

                                            if (Convert.ToDateTime(LimiteEntradaSaida) >= Convert.ToDateTime(LimitePadraoAlmoco))
                                            {
                                                if (Convert.ToDateTime(Limite) >= Convert.ToDateTime("01:00:00.000"))
                                                {
                                                    if (PermiteRegistroLimite(HoraBatida, Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.Date.ToShortDateString(), HoraPadraoEntradaTarde)), Convert.ToDateTime(LimitePadraoEntradaSaida), vwUsuarioGrid[vinculo].AcessoEspecial))
                                                    {
                                                        //Prevendo hora de almoço superior ou igual a uma hora
                                                        if (vwFrequenciaDia[0].IsHoraSaidaManhaNull())
                                                        {
                                                            if (adpFrequencia.UpdateEntradaTarde(HoraBatida, vwFrequenciaDia[0].TotalHorasDiaSegundos, IDUsuario, HoraBatida.Date, vwUsuarioGrid[vinculo].IDVinculoUsuario) > 0)
                                                                msg = string.Format("{0}, entrada 2 efetivada.", NomeUsuario);
                                                            else
                                                                msg = string.Format("{0}, marcação não efetivada. Repita a operação", NomeUsuario);
                                                        }
                                                        else
                                                        {
                                                            LimiteEntradaSaida = string.Empty;
                                                            LimiteEntradaSaida = Convert.ToString(HoraBatida - vwFrequenciaDia[0].HoraSaidaManha);

                                                            if (Convert.ToDateTime(LimiteEntradaSaida) >= Convert.ToDateTime(string.Format("{0} 01:00:00", HoraBatida.ToShortDateString())))
                                                            {
                                                                if (adpFrequencia.UpdateEntradaTarde(HoraBatida, vwFrequenciaDia[0].TotalHorasDiaSegundos, IDUsuario, HoraBatida.Date, vwUsuarioGrid[vinculo].IDVinculoUsuario) > 0)
                                                                    msg = string.Format("{0}, entrada 2 efetivada.", NomeUsuario);
                                                                else
                                                                    msg = string.Format("{0}, marcação não efetivada. Repita a operação", NomeUsuario);
                                                            }
                                                            else
                                                                msg = string.Format("{0}, horário de almoço. Permissão para registro em {1}.", NomeUsuario, Convert.ToDateTime(LimiteEntradaSaida).ToShortTimeString());
                                                        }
                                                    }
                                                    else
                                                    {
                                                        switch (msg)
                                                        {
                                                            case "excedido":
                                                                msg = String.Format("{0}, limite para registro excedido em {1}", NomeUsuario.Trim(), Limite);
                                                                break;
                                                            case "antecipado":
                                                                msg = String.Format("{0}, permissão para realizar registro em {1}", NomeUsuario.Trim(), Limite);
                                                                break;
                                                            case "turno justificado":
                                                                msg = String.Format("{0}, turno justificado, marcação de ponto não realizada.", NomeUsuario.Trim());
                                                                break;
                                                            case "dia justificado":
                                                                msg = String.Format("{0}, Jornada justificada na data corrente.", NomeUsuario.Trim());
                                                                break;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (PermiteRegistroLimite(HoraBatida, Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.Date.ToShortDateString(), HoraPadraoSaidTarde)), Convert.ToDateTime(LimitePadraoEntradaSaida), vwUsuarioGrid[vinculo].AcessoEspecial))
                                                    {
                                                        //Total Horas
                                                        DateTime? Entrada1, Saida1, Entrada2, Saida2;

                                                        if (!vwFrequenciaDia[0].IsHoraEntraManhaNull())
                                                            Entrada1 = vwFrequenciaDia[0].HoraEntraManha;
                                                        else
                                                            Entrada1 = null;

                                                        if (!vwFrequenciaDia[0].IsHoraSaidaManhaNull())
                                                            Saida1 = vwFrequenciaDia[0].HoraSaidaManha;
                                                        else
                                                            Saida1 = null;

                                                        if (!vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                                                            Entrada2 = vwFrequenciaDia[0].HoraEntradaTarde;
                                                        else
                                                            Entrada2 = null;

                                                        Saida2 = HoraBatida;

                                                        if (adpFrequencia.UpdateSaidaTarde(HoraBatida, TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), IDUsuario, vwFrequenciaDia[0].IDFrequencia) == 0)
                                                        {
                                                            msg = "Nao Atualizado";
                                                        }
                                                        else
                                                        {

                                                            //adpFrequencia.UpdateTotHoras(TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);
                                                            TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date);
                                                            switch (msg)
                                                            {
                                                                case "Carga Total":
                                                                    msg = String.Format("{0}, Jornada finalizada.", NomeUsuario.Trim());
                                                                    break;
                                                                case "Carga Incompleta":
                                                                    msg = String.Format("{0}, jornada finalizada com a carga horária total incompleta.", NomeUsuario.Trim());
                                                                    break;
                                                                case "Nao Atualizado":
                                                                    msg = String.Format("{0}, marcação não efetivada. Repita a operação", NomeUsuario.Trim());
                                                                    break;
                                                            }

                                                        }
                                                    }
                                                    else
                                                    {
                                                        switch (msg)
                                                        {
                                                            case "excedido":
                                                                msg = String.Format("{0}, limite para registro excedido em {1}", NomeUsuario.Trim(), Limite);
                                                                break;
                                                            case "antecipado":
                                                                msg = String.Format("{0}, permissão para realizar registro em {1}", NomeUsuario.Trim(), Limite);
                                                                break;
                                                            case "turno justificado":
                                                                msg = String.Format("{0}, turno justificado, marcação de ponto não realizada.", NomeUsuario.Trim());
                                                                break;
                                                            case "dia justificado":
                                                                msg = String.Format("{0}, Jornada justificada na data corrente.", NomeUsuario.Trim());
                                                                break;
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                string TempoRestante = Convert.ToString(Convert.ToDateTime(LimitePadraoAlmoco) - Convert.ToDateTime(LimiteEntradaSaida));
                                                msg = string.Format("{0}, horário de almoço. Faça sua marcação em {1}", NomeUsuario.Trim(), Convert.ToDateTime(TempoRestante).ToShortTimeString());
                                            }
                                        }
                                        else
                                        {
                                            if (Convert.ToDateTime(Limite) >= Convert.ToDateTime("01:00:00.000"))
                                            {
                                                if (PermiteRegistroLimite(HoraBatida, Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.Date.ToShortDateString(), HoraPadraoEntradaTarde)), Convert.ToDateTime(LimitePadraoEntradaSaida), vwUsuarioGrid[vinculo].AcessoEspecial))
                                                {
                                                    //Prevendo hora de almoço superior ou igual a uma hora
                                                    if (vwFrequenciaDia[0].IsHoraSaidaManhaNull())
                                                    {
                                                        if (adpFrequencia.UpdateEntradaTarde(HoraBatida, vwFrequenciaDia[0].TotalHorasDiaSegundos, IDUsuario, HoraBatida.Date, vwUsuarioGrid[vinculo].IDVinculoUsuario) > 0)
                                                            msg = string.Format("{0}, entrada 2 efetivada.", NomeUsuario);
                                                        else
                                                            msg = string.Format("{0}, marcação não efetivada. Repita a operação", NomeUsuario);
                                                    }
                                                    else
                                                    {
                                                        LimiteEntradaSaida = string.Empty;
                                                        LimiteEntradaSaida = Convert.ToString(HoraBatida - vwFrequenciaDia[0].HoraSaidaManha);

                                                        if (Convert.ToDateTime(LimiteEntradaSaida) >= Convert.ToDateTime(string.Format("{0} 01:00:00", HoraBatida.ToShortDateString())))
                                                        {
                                                            if (adpFrequencia.UpdateEntradaTarde(HoraBatida, vwFrequenciaDia[0].TotalHorasDiaSegundos, IDUsuario, HoraBatida.Date, vwUsuarioGrid[vinculo].IDVinculoUsuario) > 0)
                                                                msg = string.Format("{0}, entrada 2 efetivada.", NomeUsuario);
                                                            else
                                                                msg = string.Format("{0}, marcação não efetivada. Repita a operação", NomeUsuario);
                                                        }
                                                        else
                                                            msg = string.Format("{0}, horário de almoço. Permissão para registro em {1}.", NomeUsuario, Convert.ToDateTime(LimiteEntradaSaida).ToShortTimeString());
                                                    }
                                                }
                                                else
                                                {
                                                    switch (msg)
                                                    {
                                                        case "excedido":
                                                            msg = String.Format("{0}, limite para registro excedido em {1}", NomeUsuario.Trim(), Limite);
                                                            break;
                                                        case "antecipado":
                                                            msg = String.Format("{0}, permissão para realizar registro em {1}", NomeUsuario.Trim(), Limite);
                                                            break;
                                                        case "turno justificado":
                                                            msg = String.Format("{0}, turno justificado, marcação de ponto não realizada.", NomeUsuario.Trim());
                                                            break;
                                                        case "dia justificado":
                                                            msg = String.Format("{0}, Jornada justificada na data corrente.", NomeUsuario.Trim());
                                                            break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else //if (TBFrequencia[0].IsHoraSaidaTardeNull()) -- retirado ... Poder bater novamente a saida da tarde
                                    {

                                        if (!vwFrequenciaDia[0].IsHoraEntradaTardeNull())//Define a diferença entre entrada e saída tarde
                                        {
                                            LimiteEntradaSaida = string.Empty;
                                            LimiteEntradaSaida = Convert.ToString(HoraBatida - vwFrequenciaDia[0].HoraEntradaTarde);

                                            if (Convert.ToDateTime(LimiteEntradaSaida) >= Convert.ToDateTime(LimitePadraoAlmoco))
                                            {
                                                if (PermiteRegistroLimite(HoraBatida, Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.Date.ToShortDateString(), HoraPadraoSaidTarde)), Convert.ToDateTime(LimitePadraoEntradaSaida), vwUsuarioGrid[vinculo].AcessoEspecial))
                                                {
                                                    //Total Horas
                                                    DateTime? Entrada1, Saida1, Entrada2, Saida2;

                                                    if (!vwFrequenciaDia[0].IsHoraEntraManhaNull())
                                                        Entrada1 = vwFrequenciaDia[0].HoraEntraManha;
                                                    else
                                                        Entrada1 = null;

                                                    if (!vwFrequenciaDia[0].IsHoraSaidaManhaNull())
                                                        Saida1 = vwFrequenciaDia[0].HoraSaidaManha;
                                                    else
                                                        Saida1 = null;

                                                    if (!vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                                                        Entrada2 = vwFrequenciaDia[0].HoraEntradaTarde;
                                                    else
                                                        Entrada2 = null;

                                                    Saida2 = HoraBatida;

                                                    if (adpFrequencia.UpdateSaidaTarde(HoraBatida, TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), IDUsuario, vwFrequenciaDia[0].IDFrequencia) == 0)
                                                    {
                                                        msg = "Nao Atualizado";
                                                    }
                                                    else
                                                    {

                                                        //adpFrequencia.UpdateTotHoras(TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);
                                                        TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date);
                                                        switch (msg)
                                                        {
                                                            case "Carga Total":
                                                                msg = String.Format("{0}, entrada 2 efetivada.", NomeUsuario.Trim());
                                                                break;
                                                            case "Carga Incompleta":
                                                                msg = String.Format("{0}, entrada 2 efetivada com a carga horária total incompleta.", NomeUsuario.Trim());
                                                                break;
                                                            case "Nao Atualizado":
                                                                msg = String.Format("{0}, marcação não efetivada. Repita a operação", NomeUsuario.Trim());
                                                                break;
                                                        }

                                                    }
                                                }
                                                else
                                                {
                                                    switch (msg)
                                                    {
                                                        case "excedido":
                                                            msg = String.Format("{0}, limite para registro excedido em {1}", NomeUsuario.Trim(), Limite);
                                                            break;
                                                        case "antecipado":
                                                            msg = String.Format("{0}, permissão para realizar registro em {1}", NomeUsuario.Trim(), Limite);
                                                            break;
                                                        case "turno justificado":
                                                            msg = String.Format("{0}, turno justificado, marcação de ponto não realizada.", NomeUsuario.Trim());
                                                            break;
                                                        case "dia justificado":
                                                            msg = String.Format("{0}, Jornada justificada na data corrente.", NomeUsuario.Trim());
                                                            break;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                string TempoRestante = Convert.ToString(Convert.ToDateTime(LimitePadraoAlmoco) - Convert.ToDateTime(LimiteEntradaSaida));
                                                msg = string.Format("{0}, horário de almoço. Faça sua marcação em {1}", NomeUsuario.Trim(), Convert.ToDateTime(TempoRestante).ToShortTimeString());
                                            }
                                        }
                                        else
                                        {
                                            if (PermiteRegistroLimite(HoraBatida, Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.Date.ToShortDateString(), HoraPadraoSaidTarde)), Convert.ToDateTime(LimitePadraoEntradaSaida), vwUsuarioGrid[vinculo].AcessoEspecial))
                                            {
                                                //Total Horas
                                                DateTime? Entrada1, Saida1, Entrada2, Saida2;

                                                if (!vwFrequenciaDia[0].IsHoraEntraManhaNull())
                                                    Entrada1 = vwFrequenciaDia[0].HoraEntraManha;
                                                else
                                                    Entrada1 = null;

                                                if (!vwFrequenciaDia[0].IsHoraSaidaManhaNull())
                                                    Saida1 = vwFrequenciaDia[0].HoraSaidaManha;
                                                else
                                                    Saida1 = null;

                                                if (!vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                                                    Entrada2 = vwFrequenciaDia[0].HoraEntradaTarde;
                                                else
                                                    Entrada2 = null;

                                                Saida2 = HoraBatida;

                                                if (adpFrequencia.UpdateSaidaTarde(HoraBatida, TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), IDUsuario, vwFrequenciaDia[0].IDFrequencia) == 0)
                                                {
                                                    msg = "Nao Atualizado";
                                                }
                                                else
                                                {

                                                    //adpFrequencia.UpdateTotHoras(TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);
                                                    TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date);
                                                    switch (msg)
                                                    {
                                                        case "Carga Total":
                                                            msg = String.Format("{0}, jornada finalizada.", NomeUsuario.Trim());
                                                            break;
                                                        case "Carga Incompleta":
                                                            msg = String.Format("{0}, jornada finalizada com a carga horária total incompleta.", NomeUsuario.Trim());
                                                            break;
                                                        case "Nao Atualizado":
                                                            msg = String.Format("{0}, marcação não efetivada. Repita a operação", NomeUsuario.Trim());
                                                            break;
                                                    }

                                                }
                                            }
                                            else
                                            {
                                                switch (msg)
                                                {
                                                    case "excedido":
                                                        msg = String.Format("{0}, limite para registro excedido em {1}", NomeUsuario.Trim(), Limite);
                                                        break;
                                                    case "antecipado":
                                                        msg = String.Format("{0}, permissão para realizar registro em {1}", NomeUsuario.Trim(), Limite);
                                                        break;
                                                    case "turno justificado":
                                                        msg = String.Format("{0}, turno justificado, marcação de ponto não realizada.", NomeUsuario.Trim());
                                                        break;
                                                    case "dia justificado":
                                                        msg = String.Format("{0}, Jornada justificada na data corrente.", NomeUsuario.Trim());
                                                        break;
                                                }
                                            }
                                        }
                                    }
                                    //else -- retirado ... poder realizar a saída da tarde.
                                    //{
                                    //    msg = string.Format("{0}, jornada completa para o período.", NomeUsuario);
                                    //}
                                }
                                else //if (TBFrequencia[0].IsHoraSaidaTardeNull()) -- Retirado ... poder realizar a saída da tarde mais de uma vez.
                                {

                                    if (!vwFrequenciaDia[0].IsHoraEntradaTardeNull())//Define a diferença entre entrada e saída tarde
                                    {
                                        LimiteEntradaSaida = string.Empty;
                                        LimiteEntradaSaida = Convert.ToString(HoraBatida - vwFrequenciaDia[0].HoraEntradaTarde);

                                        if (Convert.ToDateTime(LimiteEntradaSaida) >= Convert.ToDateTime("00:10:00.000"))
                                        {
                                            if (PermiteRegistroLimite(HoraBatida, Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.Date.ToShortDateString(), HoraPadraoSaidTarde)), Convert.ToDateTime(LimitePadraoEntradaSaida), vwUsuarioGrid[vinculo].AcessoEspecial))
                                            {
                                                //Total Horas
                                                DateTime? Entrada1, Saida1, Entrada2, Saida2;

                                                if (!vwFrequenciaDia[0].IsHoraEntraManhaNull())
                                                    Entrada1 = vwFrequenciaDia[0].HoraEntraManha;
                                                else
                                                    Entrada1 = null;

                                                if (!vwFrequenciaDia[0].IsHoraSaidaManhaNull())
                                                    Saida1 = vwFrequenciaDia[0].HoraSaidaManha;
                                                else
                                                    Saida1 = null;

                                                if (!vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                                                    Entrada2 = vwFrequenciaDia[0].HoraEntradaTarde;
                                                else
                                                    Entrada2 = null;

                                                Saida2 = HoraBatida;

                                                if (adpFrequencia.UpdateSaidaTarde(HoraBatida, TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), IDUsuario, vwFrequenciaDia[0].IDFrequencia) == 0)
                                                {
                                                    msg = "Nao Atualizado";
                                                }
                                                else
                                                {

                                                    //adpFrequencia.UpdateTotHoras(TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);
                                                    TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date);
                                                    switch (msg)
                                                    {
                                                        case "Carga Total":
                                                            msg = String.Format("{0}, jornada finalizada.", NomeUsuario.Trim());
                                                            break;
                                                        case "Carga Incompleta":
                                                            msg = String.Format("{0}, jornada finalizada com a carga horária total incompleta.", NomeUsuario.Trim());
                                                            break;
                                                        case "Nao Atualizado":
                                                            msg = String.Format("{0}, marcação não efetivada. Repita a operação", NomeUsuario.Trim());
                                                            break;
                                                    }

                                                }
                                            }
                                            else
                                            {
                                                switch (msg)
                                                {
                                                    case "excedido":
                                                        msg = String.Format("{0}, limite para registro excedido em {1}", NomeUsuario.Trim(), Limite);
                                                        break;
                                                    case "antecipado":
                                                        msg = String.Format("{0}, permissão para realizar registro em {1}", NomeUsuario.Trim(), Limite);
                                                        break;
                                                    case "turno justificado":
                                                        msg = String.Format("{0}, turno justificado, marcação de ponto não realizada.", NomeUsuario.Trim());
                                                        break;
                                                    case "dia justificado":
                                                        msg = String.Format("{0}, Jornada justificada na data corrente.", NomeUsuario.Trim());
                                                        break;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            msg = string.Format("{0}, limite de dez minutos entre as marcações.", NomeUsuario.Trim());
                                        }
                                    }
                                    else
                                    {
                                        if (PermiteRegistroLimite(HoraBatida, Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.Date.ToShortDateString(), HoraPadraoSaidTarde)), Convert.ToDateTime(LimitePadraoEntradaSaida), vwUsuarioGrid[vinculo].AcessoEspecial))
                                        {
                                            //Total Horas
                                            DateTime? Entrada1, Saida1, Entrada2, Saida2;

                                            if (!vwFrequenciaDia[0].IsHoraEntraManhaNull())
                                                Entrada1 = vwFrequenciaDia[0].HoraEntraManha;
                                            else
                                                Entrada1 = null;

                                            if (!vwFrequenciaDia[0].IsHoraSaidaManhaNull())
                                                Saida1 = vwFrequenciaDia[0].HoraSaidaManha;
                                            else
                                                Saida1 = null;

                                            if (!vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                                                Entrada2 = vwFrequenciaDia[0].HoraEntradaTarde;
                                            else
                                                Entrada2 = null;

                                            Saida2 = HoraBatida;

                                            if (adpFrequencia.UpdateSaidaTarde(HoraBatida, TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), IDUsuario, vwFrequenciaDia[0].IDFrequencia) == 0)
                                            {
                                                msg = "Nao Atualizado";
                                            }
                                            else
                                            {


                                                //adpFrequencia.UpdateTotHoras(TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);
                                                TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date);
                                            }

                                            switch (msg)
                                            {
                                                case "Carga Total":
                                                    msg = String.Format("{0}, jornada finalizada", NomeUsuario.Trim());
                                                    break;
                                                case "Carga Incompleta":
                                                    msg = String.Format("{0}, jornada finalizada com a carga horária total incompleta.", NomeUsuario.Trim());
                                                    break;
                                                case "Nao Atualizado":
                                                    msg = String.Format("{0}, marcação não efetivada. Repita a operação", NomeUsuario.Trim());
                                                    break;
                                            }
                                        }
                                        else
                                        {
                                            switch (msg)
                                            {
                                                case "excedido":
                                                    msg = String.Format("{0}, limite para registro excedido em {1}", NomeUsuario.Trim(), Limite);
                                                    break;
                                                case "antecipado":
                                                    msg = String.Format("{0}, permissão para realizar registro em {1}", NomeUsuario.Trim(), Limite);
                                                    break;
                                                case "turno justificado":
                                                    msg = String.Format("{0}, turno justificado, marcação de ponto não realizada.", NomeUsuario.Trim());
                                                    break;
                                                case "dia justificado":
                                                    msg = String.Format("{0}, Jornada justificada na data corrente.", NomeUsuario.Trim());
                                                    break;
                                            }
                                        }
                                    }
                                }
                                //else //Retirado .. poder realizar a saída da tarde mais de uma vez
                                //{
                                //msg = string.Format("{0}, jornada completa para o período.", NomeUsuario);
                                //}
                            }
                            else if (vwFrequenciaDia[0].IsHoraEntraManhaNull() && vwFrequenciaDia[0].IsHoraSaidaManhaNull() && HoraBatida >= Convert.ToDateTime(string.Format("{0} 12:00:00.000", HoraBatida.ToShortDateString())) && HoraBatida < Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.ToShortDateString(), vwUsuarioGrid[vinculo].HoraSaidaTarde)).AddHours(4) && vwFrequenciaDia[0].IsHoraSaidaTardeNull()) //Se justificado e não batido pela manhã
                            {
                                //Entrada da tarde direto.
                                if (adpFrequencia.UpdateEntradaTarde(HoraBatida, null, IDUsuario, HoraBatida.Date, vwUsuarioGrid[vinculo].IDVinculoUsuario) > 0)
                                    msg = string.Format("{0}, entrada 2 efetivada.", NomeUsuario);
                                else
                                    msg = string.Format("{0}, marcação não efetivada. Repita a operação", NomeUsuario);
                            }
                            else if (vwFrequenciaDia[0].IsHoraEntraManhaNull() && vwFrequenciaDia[0].IsHoraSaidaManhaNull() && HoraBatida >= Convert.ToDateTime(string.Format("{0} 12:00:00.000", HoraBatida.ToShortDateString())) && HoraBatida >= Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.ToShortDateString(), vwUsuarioGrid[vinculo].HoraSaidaTarde)).AddHours(4)) //Se maior, registro da saída da tarde.
                            {
                                //Total Horas
                                DateTime? Entrada1, Saida1, Entrada2, Saida2;

                                if (!vwFrequenciaDia[0].IsHoraEntraManhaNull())
                                    Entrada1 = vwFrequenciaDia[0].HoraEntraManha;
                                else
                                    Entrada1 = null;

                                if (!vwFrequenciaDia[0].IsHoraSaidaManhaNull())
                                    Saida1 = vwFrequenciaDia[0].HoraSaidaManha;
                                else
                                    Saida1 = null;

                                if (!vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                                    Entrada2 = vwFrequenciaDia[0].HoraEntradaTarde;
                                else
                                    Entrada2 = null;

                                Saida2 = HoraBatida;

                                if (adpFrequencia.UpdateSaidaTarde(HoraBatida, TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), IDUsuario, vwFrequenciaDia[0].IDFrequencia) == 0)
                                {
                                    msg = "Nao Atualizado";
                                }
                                else
                                {
                                    //adpFrequencia.UpdateTotHoras(TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);
                                    TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date);
                                }

                                switch (msg)
                                {
                                    case "Carga Total":
                                        msg = String.Format("{0}, jornada finalizada.", NomeUsuario.Trim());
                                        break;
                                    case "Carga Incompleta":
                                        msg = String.Format("{0}, jornada finalizada com a carga horária total incompleta.", NomeUsuario.Trim());
                                        break;
                                    case "Nao Atualizado":
                                        msg = String.Format("{0}, marcação não efetivada. Repita a operação", NomeUsuario.Trim());
                                        break;
                                }
                            }
                            else if (!vwFrequenciaDia[0].IsHoraSaidaTardeNull())
                            {
                                //Total Horas
                                DateTime? Entrada1, Saida1, Entrada2, Saida2;

                                if (!vwFrequenciaDia[0].IsHoraEntraManhaNull())
                                    Entrada1 = vwFrequenciaDia[0].HoraEntraManha;
                                else
                                    Entrada1 = null;

                                if (!vwFrequenciaDia[0].IsHoraSaidaManhaNull())
                                    Saida1 = vwFrequenciaDia[0].HoraSaidaManha;
                                else
                                    Saida1 = null;

                                if (!vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                                    Entrada2 = vwFrequenciaDia[0].HoraEntradaTarde;
                                else
                                    Entrada2 = null;

                                Saida2 = HoraBatida;
                                if (adpFrequencia.UpdateSaidaTarde(HoraBatida, TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), IDUsuario, vwFrequenciaDia[0].IDFrequencia) == 0)
                                {
                                    msg = "Nao Atualizado";
                                }
                                else
                                {

                                    //adpFrequencia.UpdateTotHoras(TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);
                                    TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date);
                                }

                                switch (msg)
                                {
                                    case "Carga Total":
                                        msg = String.Format("{0}, jornada finalizada", NomeUsuario.Trim());
                                        break;
                                    case "Carga Incompleta":
                                        msg = String.Format("{0}, jornada finalizada com a carga horária total incompleta.", NomeUsuario.Trim());
                                        break;
                                    case "Nao Atualizado":
                                        msg = String.Format("{0}, marcação não efetivada. Repita a operação", NomeUsuario.Trim());
                                        break;
                                }
                            }

                        } // Até aqui pessoas que trabalham 8 horas por dia
                        #endregion
                        else //Pessoas que trabalham 6 horas diretas ou menos ou mais
                        {
                            if (!vwFrequenciaDia[0].IsHoraEntraManhaNull() && vwFrequenciaDia[0].IsHoraSaidaManhaNull())
                            {
                                if (vwFrequenciaDia[0].HoraEntraManha != HoraBatida)
                                {
                                    LimiteEntradaSaida = string.Empty;
                                    LimiteEntradaSaida = Convert.ToString(HoraBatida - vwFrequenciaDia[0].HoraEntraManha);

                                    if (Convert.ToDateTime(LimiteEntradaSaida) > Convert.ToDateTime("00:10:00.000"))
                                    {
                                        if (PermiteRegistroLimite(HoraBatida, Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.Date.ToShortDateString(), HoraPadraoSaidaManha)), Convert.ToDateTime(LimitePadraoEntradaSaida), vwUsuarioGrid[vinculo].AcessoEspecial))
                                        {
                                            //    //Total Horas
                                            DateTime? Entrada1, Saida1, Entrada2, Saida2;

                                            if (!vwFrequenciaDia[0].IsHoraEntraManhaNull())
                                                Entrada1 = vwFrequenciaDia[0].HoraEntraManha;
                                            else
                                                Entrada1 = null;

                                            if (!vwFrequenciaDia[0].IsHoraSaidaManhaNull())
                                                Saida1 = vwFrequenciaDia[0].HoraSaidaManha;
                                            else
                                                Saida1 = HoraBatida;


                                            Entrada2 = null;

                                            Saida2 = null;
                                            //Se retorno = 0 mensagem de erro para repetir operação.. Senão, apresentar msg de erro.
                                            if (adpFrequencia.UpdateSaidaManha(HoraBatida, TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Manha", HoraBatida.Date.Date), IDUsuario, HoraBatida.Date, vwUsuarioGrid[vinculo].IDVinculoUsuario) == 0)
                                            {
                                                msg = "Nao Atualizado";
                                            }
                                            else
                                            {


                                                //adpFrequencia.UpdateTotHoras(TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Manha", HoraBatida.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);

                                                TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Manha", HoraBatida.Date.Date);
                                            }


                                            switch (msg)
                                            {
                                                case "Carga Total":
                                                    msg = String.Format("{0}, jornada finalizada.", NomeUsuario.Trim());
                                                    break;
                                                case "Carga Incompleta":
                                                    msg = String.Format("{0}, jornada finalizada com a carga horária total incompleta.", NomeUsuario.Trim());
                                                    break;
                                                case "Nao Atualizado":
                                                    msg = String.Format("{0}, marcação não efetivada. Repita o processo.", NomeUsuario.Trim());
                                                    break;
                                            }
                                        }
                                        else
                                        {
                                            switch (msg)
                                            {
                                                case "excedido":
                                                    msg = String.Format("{0}, limite para registro excedido em {1}", NomeUsuario.Trim(), Limite);
                                                    break;
                                                case "antecipado":
                                                    msg = String.Format("{0}, permissão para realizar registro em {1}", NomeUsuario.Trim(), Limite);
                                                    break;
                                                case "turno justificado":
                                                    msg = String.Format("{0}, turno justificado, marcação de ponto não realizada.", NomeUsuario.Trim());
                                                    break;
                                                case "dia justificado":
                                                    msg = String.Format("{0}, Jornada justificada na data corrente.", NomeUsuario.Trim());
                                                    break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        msg = string.Format("{0}, respeite o intervalo de 10 minutos entre as marcações.", NomeUsuario);
                                    }
                                }
                            }
                            else if (!vwFrequenciaDia[0].IsHoraEntraManhaNull() && !vwFrequenciaDia[0].IsHoraSaidaManhaNull() && vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                            {
                                if (vwFrequenciaDia[0].HoraSaidaManha != HoraBatida)
                                {
                                    if (PermiteRegistroLimite(HoraBatida, Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.Date.ToShortDateString(), HoraPadraoEntradaTarde)), Convert.ToDateTime(LimitePadraoEntradaSaida), vwUsuarioGrid[vinculo].AcessoEspecial))
                                    {
                                        //Prevendo hora de almoço superior ou igual a uma hora
                                        if (vwFrequenciaDia[0].IsHoraSaidaManhaNull())
                                        {
                                            if (adpFrequencia.UpdateEntradaTarde(HoraBatida, vwFrequenciaDia[0].TotalHorasDiaSegundos, IDUsuario, HoraBatida.Date, vwUsuarioGrid[vinculo].IDVinculoUsuario) > 0)
                                                msg = string.Format("{0}, entrada 2 efetivada.", NomeUsuario);
                                            else
                                                msg = string.Format("{0}, marcação não efetivada. Repita a operação", NomeUsuario);
                                        }
                                        else
                                        {
                                            LimiteEntradaSaida = string.Empty;
                                            LimiteEntradaSaida = Convert.ToString(HoraBatida - vwFrequenciaDia[0].HoraSaidaManha);

                                            if (Convert.ToDateTime(LimiteEntradaSaida) >= Convert.ToDateTime(string.Format("{0} 01:00:00", HoraBatida.ToShortDateString())))
                                            {
                                                if (adpFrequencia.UpdateEntradaTarde(HoraBatida, vwFrequenciaDia[0].TotalHorasDiaSegundos, IDUsuario, HoraBatida.Date, vwUsuarioGrid[vinculo].IDVinculoUsuario) > 0)
                                                    msg = string.Format("{0}, entrada 2 não efetivada.", NomeUsuario);
                                                else
                                                    msg = string.Format("{0}, marcação não efetivada. Repita a operação", NomeUsuario);
                                            }
                                            else
                                                msg = string.Format("{0}, horário de almoço. Permissão para registro em {1}.", NomeUsuario, Convert.ToDateTime(LimiteEntradaSaida).ToShortTimeString());
                                        }
                                    }
                                    else
                                    {
                                        switch (msg)
                                        {
                                            case "excedido":
                                                msg = String.Format("{0}, limite para registro excedido em {1}", NomeUsuario.Trim(), Limite);
                                                break;
                                            case "antecipado":
                                                msg = String.Format("{0}, permissão para realizar registro em {1}", NomeUsuario.Trim(), Limite);
                                                break;
                                            case "turno justificado":
                                                msg = String.Format("{0}, turno justificado, marcação de ponto não realizada.", NomeUsuario.Trim());
                                                break;
                                            case "dia justificado":
                                                msg = String.Format("{0}, Jornada justificada na data corrente.", NomeUsuario.Trim());
                                                break;
                                        }
                                    }
                                }
                            }
                            else if (!vwFrequenciaDia[0].IsHoraEntraManhaNull() && !vwFrequenciaDia[0].IsHoraSaidaManhaNull() && !vwFrequenciaDia[0].IsHoraEntradaTardeNull() && vwFrequenciaDia[0].IsHoraSaidaTardeNull())
                            {
                                if (vwFrequenciaDia[0].HoraEntradaTarde != HoraBatida)
                                {
                                    if (PermiteRegistroLimite(HoraBatida, Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.Date.ToShortDateString(), HoraPadraoSaidTarde)), Convert.ToDateTime(LimitePadraoEntradaSaida), vwUsuarioGrid[vinculo].AcessoEspecial))
                                    {
                                        //Total Horas
                                        DateTime? Entrada1, Saida1, Entrada2, Saida2;

                                        if (!vwFrequenciaDia[0].IsHoraEntraManhaNull())
                                            Entrada1 = vwFrequenciaDia[0].HoraEntraManha;
                                        else
                                            Entrada1 = null;

                                        if (!vwFrequenciaDia[0].IsHoraSaidaManhaNull())
                                            Saida1 = vwFrequenciaDia[0].HoraSaidaManha;
                                        else
                                            Saida1 = null;

                                        if (!vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                                            Entrada2 = vwFrequenciaDia[0].HoraEntradaTarde;
                                        else
                                            Entrada2 = null;

                                        Saida2 = HoraBatida;

                                        if (adpFrequencia.UpdateSaidaTarde(HoraBatida, TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), IDUsuario, vwFrequenciaDia[0].IDFrequencia) == 0)
                                        {
                                            msg = "Nao Atualizado";
                                        }
                                        else
                                        {

                                            //adpFrequencia.UpdateTotHoras(TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);
                                            TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date);

                                            switch (msg)
                                            {
                                                case "Carga Total":
                                                    msg = String.Format("{0}, Jornada finalizada.", NomeUsuario.Trim());
                                                    break;
                                                case "Carga Incompleta":
                                                    msg = String.Format("{0}, jornada finalizada com a carga horária total incompleta.", NomeUsuario.Trim());
                                                    break;
                                                case "Nao Atualizado":
                                                    msg = String.Format("{0}, marcação não efetivada. Repita a operação", NomeUsuario.Trim());
                                                    break;
                                            }

                                        }

                                    }
                                    else
                                    {
                                        switch (msg)
                                        {
                                            case "excedido":
                                                msg = String.Format("{0}, limite para registro excedido em {1}", NomeUsuario.Trim(), Limite);
                                                break;
                                            case "antecipado":
                                                msg = String.Format("{0}, permissão para realizar registro em {1}", NomeUsuario.Trim(), Limite);
                                                break;
                                            case "turno justificado":
                                                msg = String.Format("{0}, turno justificado, marcação de ponto não realizada.", NomeUsuario.Trim());
                                                break;
                                            case "dia justificado":
                                                msg = String.Format("{0}, Jornada justificada na data corrente.", NomeUsuario.Trim());
                                                break;
                                        }
                                    }
                                }

                                //if (!vwFrequenciaDia[0].IsHoraEntraManhaNull() && !vwFrequenciaDia[0].IsHoraSaidaManhaNull() && !vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                                //{
                                //    //Total Horas
                                //    DateTime? Entrada1, Saida1, Entrada2, Saida2;

                                //    if (!vwFrequenciaDia[0].IsHoraEntraManhaNull())
                                //        Entrada1 = vwFrequenciaDia[0].HoraEntraManha;
                                //    else
                                //        Entrada1 = null;

                                //    if (!vwFrequenciaDia[0].IsHoraSaidaManhaNull())
                                //        Saida1 = vwFrequenciaDia[0].HoraSaidaManha;
                                //    else
                                //        Saida1 = null;

                                //    if (!vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                                //        Entrada2 = vwFrequenciaDia[0].HoraEntradaTarde;
                                //    else
                                //        Entrada2 = null;

                                //    Saida2 = HoraBatida;

                                //    adpFrequencia.UpdateTotHoras(TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);

                                //    switch (msg)
                                //    {
                                //        case "Carga Total":
                                //            msg = String.Format("{0}, frequência registrada com sucesso. Saída da Tarde.", NomeUsuario.Trim());
                                //            break;
                                //        case "Carga Incompleta":
                                //            msg = String.Format("{0}, frequência registrada com a carga horária total incompleta. Saída da Tarde.", NomeUsuario.Trim());
                                //            break;
                                //    }

                                //}
                                //else
                                //{
                                //    //Total Horas
                                //    DateTime? Entrada1, Saida1, Entrada2, Saida2;

                                //    if (!vwFrequenciaDia[0].IsHoraEntraManhaNull())
                                //        Entrada1 = vwFrequenciaDia[0].HoraEntraManha;
                                //    else
                                //        Entrada1 = null;

                                //    if (!vwFrequenciaDia[0].IsHoraSaidaManhaNull())
                                //        Saida1 = vwFrequenciaDia[0].HoraSaidaManha;
                                //    else
                                //        Saida1 = null;

                                //    if (!vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                                //        Entrada2 = vwFrequenciaDia[0].HoraEntradaTarde;
                                //    else
                                //        Entrada2 = null;

                                //    Saida2 = HoraBatida;

                                //    adpFrequencia.UpdateTotHoras(TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);

                                //    switch (msg)
                                //    {
                                //        case "Carga Total":
                                //            msg = String.Format("{0}, frequência registrada com sucesso. Saída da Tarde.", NomeUsuario.Trim());
                                //            break;
                                //        case "Carga Incompleta":
                                //            msg = String.Format("{0}, frequência registrada com a carga horária total incompleta. Saída da Tarde.", NomeUsuario.Trim());
                                //            break;
                                //    }
                                //}
                            }
                            else if (vwFrequenciaDia[0].IsHoraEntraManhaNull() && vwFrequenciaDia[0].IsHoraSaidaManhaNull() && !vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                            {
                                LimiteEntradaSaida = string.Empty;
                                LimiteEntradaSaida = Convert.ToString(HoraBatida - vwFrequenciaDia[0].HoraEntradaTarde);

                                if (Convert.ToDateTime(LimiteEntradaSaida) > Convert.ToDateTime("00:10:00.000"))
                                {
                                    if (PermiteRegistroLimite(HoraBatida, Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.Date.ToShortDateString(), HoraPadraoSaidTarde)), Convert.ToDateTime(LimitePadraoEntradaSaida), vwUsuarioGrid[vinculo].AcessoEspecial))
                                    {
                                        //Total Horas
                                        DateTime? Entrada1, Saida1, Entrada2, Saida2;

                                        if (!vwFrequenciaDia[0].IsHoraEntraManhaNull())
                                            Entrada1 = vwFrequenciaDia[0].HoraEntraManha;
                                        else
                                            Entrada1 = null;

                                        if (!vwFrequenciaDia[0].IsHoraSaidaManhaNull())
                                            Saida1 = vwFrequenciaDia[0].HoraSaidaManha;
                                        else
                                            Saida1 = null;

                                        if (!vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                                            Entrada2 = vwFrequenciaDia[0].HoraEntradaTarde;
                                        else
                                            Entrada2 = null;

                                        Saida2 = HoraBatida;

                                        if (adpFrequencia.UpdateSaidaTarde(HoraBatida, TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), IDUsuario, vwFrequenciaDia[0].IDFrequencia) == 0)
                                        {
                                            msg = "Nao Atualizado";
                                        }
                                        else
                                        {

                                            //adpFrequencia.UpdateTotHoras(TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);
                                            TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date);
                                            switch (msg)
                                            {
                                                case "Carga Total":
                                                    msg = String.Format("{0}, jornada finalizada.", NomeUsuario.Trim());
                                                    break;
                                                case "Carga Incompleta":
                                                    msg = String.Format("{0}, jornada finalizada com a carga horária total incompleta.", NomeUsuario.Trim());
                                                    break;
                                                case "Nao Atualizado":
                                                    msg = String.Format("{0}, marcação não efetivada. Repita a operação", NomeUsuario.Trim());
                                                    break;
                                            }

                                        }
                                    }
                                    else
                                    {
                                        switch (msg)
                                        {
                                            case "excedido":
                                                msg = String.Format("{0}, limite para registro excedido em {1}", NomeUsuario.Trim(), Limite);
                                                break;
                                            case "antecipado":
                                                msg = String.Format("{0}, permissão para realizar registro em {1}", NomeUsuario.Trim(), Limite);
                                                break;
                                            case "turno justificado":
                                                msg = String.Format("{0}, turno justificado, marcação de ponto não realizada.", NomeUsuario.Trim());
                                                break;
                                            case "dia justificado":
                                                msg = String.Format("{0}, Jornada justificada na data corrente.", NomeUsuario.Trim());
                                                break;
                                        }
                                    }
                                }
                                else
                                {
                                    msg = string.Format("{0}, respeite o intervalo de 10 minutos entre as marcações.", NomeUsuario);
                                }
                            }
                            //Para períodos justificados -- Liberar batida exceto atraso superior a uma hora.
                            else if (vwFrequenciaDia[0].IsHoraEntraManhaNull() && vwFrequenciaDia[0].IsHoraSaidaManhaNull() && vwFrequenciaDia[0].IsHoraEntradaTardeNull() && vwFrequenciaDia[0].IsHoraSaidaTardeNull() && !vwFrequenciaDia[0].IsIDMotivoFaltaNull())
                            {
                                if (HoraBatida <= Convert.ToDateTime(string.Format("{0} 11:00:00.000", HoraBatida.Date.ToShortDateString())))
                                {
                                    if (PermiteRegistroLimite(HoraBatida, Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.Date.ToShortDateString(), HoraPadraoEntradaManha)), Convert.ToDateTime(LimitePadraoEntradaSaida), vwUsuarioGrid[vinculo].AcessoEspecial))
                                    {
                                        if (adpFrequencia.UpdateEntradaManhaJustificada(HoraBatida, vwFrequenciaDia[0].OBS, vwFrequenciaDia[0].IDMotivoFalta, vwFrequenciaDia[0].IDFrequencia, IDUsuario) > 0)
                                            msg = string.Format("{0}, entrada 1 efetivada.", NomeUsuario);
                                        else
                                            msg = string.Format("{0}, marcação não efetivada. Tente novamente.", NomeUsuario);
                                    }
                                    else
                                    {
                                        switch (msg)
                                        {
                                            case "excedido":
                                                msg = String.Format("{0}, limite para registro excedido em {1}", NomeUsuario.Trim(), Limite);
                                                break;
                                            case "antecipado":
                                                msg = String.Format("{0}, permissão para realizar registro em {1}", NomeUsuario.Trim(), Limite);
                                                break;
                                            case "turno justificado":
                                                msg = String.Format("{0}, turno justificado, marcação de ponto não realizada.", NomeUsuario.Trim());
                                                break;
                                            case "dia justificado":
                                                msg = String.Format("{0}, Jornada justificada na data corrente.", NomeUsuario.Trim());
                                                break;
                                        }
                                    }

                                }
                                else if (vwFrequenciaDia[0].IsHoraEntraManhaNull() && vwFrequenciaDia[0].IsHoraEntraManhaNull())
                                {
                                    if (PermiteRegistroLimite(HoraBatida, Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.Date.ToShortDateString(), HoraPadraoEntradaTarde)), Convert.ToDateTime(LimitePadraoEntradaSaida), vwUsuarioGrid[vinculo].AcessoEspecial))
                                    {
                                        //Prevendo hora de almoço superior ou igual a uma hora
                                        if (vwFrequenciaDia[0].IsHoraSaidaManhaNull())
                                        {
                                            if (adpFrequencia.UpdateEntradaTarde(HoraBatida, vwFrequenciaDia[0].TotalHorasDiaSegundos, IDUsuario, HoraBatida.Date, vwUsuarioGrid[vinculo].IDVinculoUsuario) > 0)
                                                msg = string.Format("{0}, entrada 2 efetivada.", NomeUsuario);
                                            else
                                                msg = string.Format("{0}, marcação não efetivada. Repita a operação", NomeUsuario);
                                        }
                                        else
                                        {
                                            LimiteEntradaSaida = string.Empty;
                                            LimiteEntradaSaida = Convert.ToString(HoraBatida - vwFrequenciaDia[0].HoraSaidaManha);

                                            if (Convert.ToDateTime(LimiteEntradaSaida) >= Convert.ToDateTime(string.Format("{0} 01:00:00", HoraBatida.ToShortDateString())))
                                            {
                                                if (adpFrequencia.UpdateEntradaTarde(HoraBatida, vwFrequenciaDia[0].TotalHorasDiaSegundos, IDUsuario, HoraBatida.Date, vwUsuarioGrid[vinculo].IDVinculoUsuario) > 0)
                                                    msg = string.Format("{0}, entrada 2 efetivada.", NomeUsuario);
                                                else
                                                    msg = string.Format("{0}, marcação não efetivada. Repita a operação", NomeUsuario);
                                            }
                                            else
                                                msg = string.Format("{0}, horário de almoço. Permissão para registro em {1}.", NomeUsuario, Convert.ToDateTime(LimiteEntradaSaida).ToShortTimeString());
                                        }
                                    }
                                    else
                                    {
                                        switch (msg)
                                        {
                                            case "excedido":
                                                msg = String.Format("{0}, limite para registro excedido em {1}", NomeUsuario.Trim(), Limite);
                                                break;
                                            case "antecipado":
                                                msg = String.Format("{0}, permissão para realizar registro em {1}", NomeUsuario.Trim(), Limite);
                                                break;
                                            case "turno justificado":
                                                msg = String.Format("{0}, turno justificado, marcação de ponto não realizada.", NomeUsuario.Trim());
                                                break;
                                            case "dia justificado":
                                                msg = String.Format("{0}, Jornada justificada na data corrente.", NomeUsuario.Trim());
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    #endregion
                }

            }

            #region Envia SMS
            //para teste - Enviar SMS
            if (!vwUsuarioGrid[0].IsTelefoneSMSNull())
            {
                if (vwUsuarioGrid[0].IDEmpresa == 32 || vwUsuarioGrid[0].IDEmpresa == 34)
                {
                    if (msg.IndexOf("efetivada") > 0 || msg.IndexOf("finalizada") > 0)
                    {
                        if (msg.IndexOf("não") < 0)
                        {
                            EnviaSMS eSMS = new EnviaSMS();
                            eSMS.enviaSMS(vwUsuarioGrid[0].PrimeiroNome, HoraBatida, vwUsuarioGrid[0].TelefoneSMS);
                        }
                    }
                }
            }
            #endregion
            return msg;
        }

        public string BaterPonto2(int IDUsuario, int IDEmpresa, DateTime HoraBatida, int IDSetor, int IDVinculoUsuarioParam) //Acrescentado IDSetor aos parâmetros para procura dos parâmetros do usuário caso ele tenha
                                                                                                                             //Mais de um vínculo com o mesmo Órgão.
        {
            //IMPORTANTE - 16/12/2015 - Alterei a busca por registros realizados para procurar por empresa ...
            /// FillFrequenciaDia para - FillFrequenciaDiaEmpresa - Para atender os multiplos Vínculos

            if (HoraBatida.Date.Date > System.DateTime.Now.Date.Date)
                return string.Format("Tentativa de registro com a data/hora maior({0}) que a atual. Registro não lançado!", HoraBatida.Date.ToShortDateString());

            #region Dados Pessoais do usuário

            //Procurar dados dos usuários pela view - vwUsuarioGrid
            try
            {
                adpvwUsuarioGrid.Connection.Open();
                adpvwUsuarioGrid.FillIDEmpresaUsuario(vwUsuarioGrid, IDEmpresa, IDUsuario);
                adpvwUsuarioGrid.Connection.Close();
            }
            catch
            {
                adpvwUsuarioGrid.Connection.Close();
            }

            vinculo = 0;

            //Caso tenho mais de um vínculo com o mesmo usuário - limita-se a busca pelo setor em que se faz o registro.
            if (vwUsuarioGrid.Rows.Count > 1) //Se maior que 1, procurar pelo setor para setar a Vinculo que irá registrar frequência.
            {
                for (int i = 0; i <= (vwUsuarioGrid.Rows.Count - 1); i++)
                {
                    if (IDSetor == vwUsuarioGrid[i].IDSetor)
                    {
                        vinculo = i;
                        break;
                    }
                }
            }
            else if (vwUsuarioGrid.Rows.Count == 0) //Faz aqui a localização de Grupo de Registros
            {
                //Adicionado em 21/03/2016 -- Definir o parâmetro IDEmpresa com a empresa original do usuário participante do grupo de registro.
                try
                {
                    IDEmpresa = ValidaVinculo(IDUsuario, IDEmpresa, "", IDVinculoUsuarioParam);
                    adpvwUsuarioGrid.Connection.Open();
                    adpvwUsuarioGrid.FillIDEmpresaUsuario(vwUsuarioGrid, IDEmpresa, IDUsuario);
                    adpvwUsuarioGrid.Connection.Close();

                    if (vwUsuarioGrid.Rows.Count > 0)
                    {
                        if (vwUsuarioGrid[0].IDEmpresa != 40)
                            return string.Format("{0}, faça seu registro em sua secretaria de origem.", vwUsuarioGrid[0].PrimeiroNome);
                    }
                }
                catch
                {
                    adpvwUsuarioGrid.Connection.Close();
                }
            }

            msg = "";
            NomeUsuario = "";
            Bateu = false;



            //Dados Pessoais do Usuário..
            //adpUsuario.FillIDUsuario(TBUsuario, IDUsuario, IDEmpresa); -- Retirado, buscar agora apenas os dados pessoais
            //24/02/2016 - Alterado Procurar os atributos do usuário pelo vínculo --adpUsuario.FillIDusuarioApenas(TBUsuario, IDUsuario);

            if (vwUsuarioGrid.Rows.Count > 0)
            {
                NomeUsuario = vwUsuarioGrid[vinculo].PrimeiroNome.ToString();
                HoraPadraoEntradaManha = vwUsuarioGrid[vinculo].HoraEntradaManha.ToString();
                HoraPadraoEntradaTarde = vwUsuarioGrid[vinculo].HoraEntradaTarde.ToString();
                HoraPadraoSaidaManha = vwUsuarioGrid[vinculo].HoraSaidaManha.ToString();
                HoraPadraoSaidTarde = vwUsuarioGrid[vinculo].HoraSaidaTarde.ToString();
                LimiteEntradaSaida = "00:15:59.0000";
                LimitePadraoEntradaSaida = "00:15:59.0000";
                LimitePadraoAlmoco = "01:00:00.0000";

                //Trabalha Sábado
                TrabalhaSabado = false; //Retirei da jornada

                //Para atender a justificativas e registros
                TotalHorasDiaUsuarioPadrao = vwUsuarioGrid[vinculo].TotHorasDiarias;
                RegimePlantonista = vwUsuarioGrid[vinculo].RegimePlantonista;
            }
            else
            {
                return "Servidor não encontrado no banco de dados!";
            }

            #endregion


            //27/07/2016 -- Defasagem de Registro

            TimeSpan TotDias = DateTime.Now - HoraBatida;

            if (TotDias.TotalDays > 4)
            {
                return string.Format("{0}, registro com mais de 4 dias ({1}) de defasagem. Procure o seu gestor!", NomeUsuario.Trim(), HoraBatida.ToShortDateString());
            }

            //Por enquanto FIxo de 2 horas ... Valor Inteiro... Após atrelar isso ao cadastro de empresas
            LimiteMaximoPlantao = 2;

            try
            {
                adpFeriasFrequencia.Connection.Open();
                adpFeriasFrequencia.FillByVerificaFeriasCorrente(TBFeriasFrequencia, HoraBatida.Date, IDUsuario, IDVinculoUsuarioParam); // Verifica férias corrente
                adpFeriasFrequencia.Connection.Close();
            }
            catch
            {
                adpFeriasFrequencia.Connection.Close();
            }


            if (TBFeriasFrequencia.Rows.Count > 0)
            {
                return string.Format("{0}, Férias/Licença em vigência. Procure sua chefia imediata.", vwUsuarioGrid[vinculo].PrimeiroNome.Trim());
            }
            else /// Se não houver férias ou licenças. Registra ponto dos servidores ...
            {
                if (vwUsuarioGrid[vinculo].RegimePlantonista) //Se regime é de plantonista fazer os pontos de plantonista. Se não segue como regime de expediente
                {
                    #region Registro de marcação de ponto de plantonistas

                    //Procura por registros realizados no dia anterior em busca de plantões a serem finalizados
                    try
                    {
                        adpvwFrequenciaDia.Connection.Open();
                        adpvwFrequenciaDia.FillIDVinculoUsuario(vwFrequenciaDia, IDVinculoUsuarioParam, HoraBatida.Date.Date.AddDays(-1));
                        adpvwFrequenciaDia.Connection.Close();
                    }
                    catch
                    {
                        adpvwFrequenciaDia.Connection.Close();
                        return "Jornada de trabalho não identificada no banco de dados. Tente Novamente!";
                    }

                    if (vwFrequenciaDia.Rows.Count > 0) //se há registros analizar os tempos
                    {

                        //Se houver uma falta lançada, não registra frequência
                        if (!vwFrequenciaDia[0].IsIDMotivoFaltaNull())
                        {
                            if (FaltaLancada(vwFrequenciaDia[0].IDMotivoFalta)) // Se o motivo for 47, não bate ponto.
                                return string.Format("{0}, há uma falta lançada na data corrente. Impossível lançar o registro de sua marcação!", NomeUsuario);
                        }

                        if ((vwFrequenciaDia[0].IsTotalHorasDiaSegundosNull()) || (vwFrequenciaDia[0].TotalHorasDiaSegundos == 0)) // Se o total estiver nulo. Verificar qual das entradas foi utiliza (Manhã ou tarde)
                        {
                            TimeSpan Resultado;

                            if (!vwFrequenciaDia[0].IsHoraEntraManhaNull())// se não for nulo manipula entrada manhã
                            {
                                Resultado = HoraBatida.Subtract(vwFrequenciaDia[0].HoraEntraManha);
                            }
                            else
                            {
                                Resultado = HoraBatida.Subtract(vwFrequenciaDia[0].HoraEntradaTarde);
                            }

                            H = Resultado.TotalMinutes;

                            if (H > ((vwUsuarioGrid[vinculo].TotHorasDiarias + LimiteMaximoPlantao) * 60)) //Se maior que a soma do total + limite... Começa um novo dia
                            {
                                //Se verdadeiro, inicia nova jornada
                                try
                                {
                                    adpvwFrequenciaDia.Connection.Open();
                                    adpvwFrequenciaDia.FillIDVinculoUsuario(vwFrequenciaDia, IDVinculoUsuarioParam, HoraBatida.Date.Date);
                                    adpvwFrequenciaDia.Connection.Close();
                                }
                                catch
                                {
                                    adpvwFrequenciaDia.Connection.Close();
                                    return "Jornada de trabalho não identificada no banco de dados. Tente Novamente!";
                                }
                            }
                        }
                        else
                        {
                            //Procura por registros no dia corrente.
                            try
                            {
                                adpvwFrequenciaDia.Connection.Open();
                                adpvwFrequenciaDia.FillIDVinculoUsuario(vwFrequenciaDia, IDVinculoUsuarioParam, HoraBatida.Date.Date);
                                adpvwFrequenciaDia.Connection.Close();
                            }
                            catch
                            {
                                adpvwFrequenciaDia.Connection.Close();
                                return "Jornada de trabalho não identificada no banco de dados. Tente Novamente!";
                            }
                        }
                    }
                    else //Se não ... Start com o dia corrente
                    {
                        //Procura por registros realizados no dia anterior em busca de plantões a serem finalizados
                        try
                        {
                            adpvwFrequenciaDia.Connection.Open();
                            adpvwFrequenciaDia.FillIDVinculoUsuario(vwFrequenciaDia, IDVinculoUsuarioParam, HoraBatida.Date.Date);
                            adpvwFrequenciaDia.Connection.Close();
                        }
                        catch
                        {
                            adpvwFrequenciaDia.Connection.Close();
                            return "Jornada de trabalho não identificada no banco de dados. Tente Novamente!";
                        }
                    }

                    switch (vwFrequenciaDia.Rows.Count)
                    {
                        case 0: //Caso não tenha registros no dia, iniciar jornada de plantão.
                            #region Inicio Nova Jornada
                            if (HoraBatida.Hour < 11) //caso antes das 11 iniciar o plantão na parte da manhã
                            {
                                //Caso insert retorne 0, manda msg para repetir operação. Caso contrário segue rotina
                                if (adpFrequencia.Insert(IDUsuario, null, HoraBatida.Date, HoraBatida, null, null, null, null, null, HoraBatida.Month, HoraBatida.Year, IDEmpresa, IDVinculoUsuarioParam) == 0)
                                {
                                    msg = "Nao Inserido";
                                }
                                else
                                {
                                    msg = "Horario";
                                }
                            }
                            else //Caso acima das 11. Registrar na parte da tarde.
                            {
                                //Caso insert retorne 0, manda msg para repetir operação. Caso contrário segue rotina
                                if (adpFrequencia.Insert(IDUsuario, null, HoraBatida.Date, null, null, HoraBatida, null, null, null, HoraBatida.Month, HoraBatida.Year, IDEmpresa, IDVinculoUsuarioParam) == 0)
                                {
                                    msg = "Nao Inserido";
                                }
                                else
                                {
                                    msg = "Horario";
                                }
                            }

                            switch (msg)
                            {
                                case "Horario":
                                    msg = String.Format("{0}, entrada 1 efetivada.", NomeUsuario.Trim());
                                    break;
                                case "Nao Inserido":
                                    msg = String.Format("{0}, marcação não efetivada. Favor repetir a operação.", NomeUsuario.Trim());
                                    break;
                            }

                            #endregion
                            break;
                        case 1:
                            //Tratativas de término da jornada e adiconal noturno.
                            //Adicional Noturno
                            if (vwFrequenciaDia[0].IsTotalHorasDiaSegundosNull() && (HoraBatida.Hour >= 21 && HoraBatida.Hour <= 23))
                            {
                                //Aqui, update Adiconal Noturno -- Quais os possíveis casos para adicional noturno. --09/03/2016
                                //Para cair aqui, já tem que haver dado entrada de um plantão.
                                TimeSpan totalhoras;

                                if (!vwFrequenciaDia[0].IsHoraEntraManhaNull())
                                {

                                    //Verifica minutos entre a entrada e saída
                                    if (!VerificaIntervalodeRegistro(HoraBatida, vwFrequenciaDia[0].HoraEntraManha))
                                        return string.Format("{0}, respeite o intervalo de 15 minutos entre as marcações.", NomeUsuario);

                                    totalhoras = HoraBatida.Subtract(vwFrequenciaDia[0].HoraEntraManha);

                                    totalhoras += TimeSpan.Parse("00:15:00"); //Add os 15 minutos de tolerância;

                                    if ((totalhoras.TotalHours >= vwUsuarioGrid[vinculo].TotHorasDiarias) || (totalhoras.TotalHours >= 20))
                                    {
                                        if (adpFrequencia.UpdateSaidaManha(HoraBatida, TotalHorasDia(vwFrequenciaDia[0].HoraEntraManha, HoraBatida, null, null, "Manha", vwFrequenciaDia[0].DTFrequencia.Date.Date), IDUsuario, vwFrequenciaDia[0].DTFrequencia.Date.Date, IDVinculoUsuarioParam) == 0)
                                        {
                                            msg = "Nao Inserido";
                                        }
                                        else
                                            msg = "Horario";

                                        //Total da jornada

                                        //adpFrequencia.UpdateTotHoras(TotalHorasDia(vwFrequenciaDia[0].HoraEntraManha, HoraBatida, null, null, "Manha", vwFrequenciaDia[0].DTFrequencia.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);

                                        TotalHorasDia(vwFrequenciaDia[0].HoraEntraManha, HoraBatida, null, null, "Manha", vwFrequenciaDia[0].DTFrequencia.Date.Date);

                                        switch (msg)
                                        {
                                            case "Add Noturno":
                                                msg = String.Format("{0}, adicional noturno iniciado com sucesso.", NomeUsuario.Trim());
                                                break;
                                            case "Carga Total":
                                                msg = String.Format("{0}, jornada finalizada.", NomeUsuario.Trim());
                                                break;
                                            case "Carga Incompleta":
                                                msg = String.Format("{0}, jornada finalizada com a carga horária incompleta.", NomeUsuario.Trim());
                                                break;
                                            case "Nao Atualizado":
                                                msg = String.Format("{0}, marcação não realizada. Repita o processo.", NomeUsuario.Trim());
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        if (adpFrequencia.UpdateEntradaAddNoturno(HoraBatida, vwFrequenciaDia[0].IDFrequencia, vwFrequenciaDia[0].IDVinculoUsuario) > 0)
                                            msg = string.Format("{0}, adicional noturno informado com sucesso!", NomeUsuario);
                                        else
                                            msg = string.Format("{0}, houve falha ao informar o adicional noturno. Tente Novamente!", NomeUsuario);

                                        return msg;
                                    }
                                }
                                else if (!vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                                {
                                    if (!VerificaIntervalodeRegistro(HoraBatida, vwFrequenciaDia[0].HoraEntradaTarde))
                                        return string.Format("{0}, respeite o intervalo de 15 minutos entre as marcações.", NomeUsuario);

                                    totalhoras = HoraBatida.Subtract(vwFrequenciaDia[0].HoraEntradaTarde);

                                    totalhoras += TimeSpan.Parse("00:15:00"); //Add os 15 minutos de tolerância;

                                    if (totalhoras.TotalHours >= vwUsuarioGrid[vinculo].TotHorasDiarias || (totalhoras.TotalHours >= 20))
                                    {

                                        if (adpFrequencia.UpdateSaidaTarde(HoraBatida, TotalHorasDia(null, null, vwFrequenciaDia[0].HoraEntradaTarde, HoraBatida, "Tarde", vwFrequenciaDia[0].DTFrequencia.Date.Date), vwFrequenciaDia[0].IDUsuario, vwFrequenciaDia[0].IDFrequencia) == 0)
                                        {
                                            msg = "Nao Inserido";
                                        }
                                        else
                                        {
                                            msg = "Horario";
                                        }

                                        //Total da jornada
                                        //adpFrequencia.UpdateTotHoras(TotalHorasDia(null, null, vwFrequenciaDia[0].HoraEntradaTarde, HoraBatida, "Tarde", vwFrequenciaDia[0].DTFrequencia.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);

                                        TotalHorasDia(null, null, vwFrequenciaDia[0].HoraEntradaTarde, HoraBatida, "Tarde", vwFrequenciaDia[0].DTFrequencia.Date.Date);

                                        switch (msg)
                                        {
                                            case "Add Noturno":
                                                msg = String.Format("{0}, adicional noturno iniciado com sucesso.", NomeUsuario.Trim());
                                                break;
                                            case "Carga Total":
                                                msg = String.Format("{0}, jornada finalizada.", NomeUsuario.Trim());
                                                break;
                                            case "Carga Incompleta":
                                                msg = String.Format("{0}, jornada finalizada com a carga horária incompleta.", NomeUsuario.Trim());
                                                break;
                                            case "Nao Atualizado":
                                                msg = String.Format("{0}, marcação não realizada. Repita o processo.", NomeUsuario.Trim());
                                                break;
                                        }

                                    }
                                    else
                                    {
                                        if (adpFrequencia.UpdateEntradaAddNoturno(HoraBatida, vwFrequenciaDia[0].IDFrequencia, vwFrequenciaDia[0].IDVinculoUsuario) > 0)
                                            msg = string.Format("{0}, adicional noturno informado com sucesso!", NomeUsuario);
                                        else
                                            msg = string.Format("{0}, houve falha ao informar o adicional noturno. Tente Novamente!", NomeUsuario);

                                        return msg;
                                    }
                                }
                            }
                            else
                            {
                                //Aqui, update do fim da jornada e Total da mesma.
                                if (!vwFrequenciaDia[0].IsHoraEntraManhaNull())
                                {

                                    if (!VerificaIntervalodeRegistro(HoraBatida, vwFrequenciaDia[0].HoraEntraManha))
                                        return string.Format("{0}, respeite o intervalo de 15 minutos entre as marcações.", NomeUsuario);

                                    if (adpFrequencia.UpdateSaidaManha(HoraBatida, TotalHorasDia(vwFrequenciaDia[0].HoraEntraManha, HoraBatida, null, null, "Manha", vwFrequenciaDia[0].DTFrequencia.Date.Date), IDUsuario, vwFrequenciaDia[0].DTFrequencia.Date.Date, IDVinculoUsuarioParam) == 0)
                                    {
                                        msg = "Nao Inserido";
                                    }
                                    else
                                        msg = "Horario";

                                    //Total da jornada

                                    //adpFrequencia.UpdateTotHoras(TotalHorasDia(vwFrequenciaDia[0].HoraEntraManha, HoraBatida, null, null, "Manha", vwFrequenciaDia[0].DTFrequencia.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);

                                    TotalHorasDia(vwFrequenciaDia[0].HoraEntraManha, HoraBatida, null, null, "Manha", vwFrequenciaDia[0].DTFrequencia.Date.Date);
                                }
                                else
                                {

                                    if (!VerificaIntervalodeRegistro(HoraBatida, vwFrequenciaDia[0].HoraEntradaTarde))
                                        return string.Format("{0}, respeite o intervalo de 15 minutos entre as marcações.", NomeUsuario);

                                    if (adpFrequencia.UpdateSaidaTarde(HoraBatida, TotalHorasDia(null, null, vwFrequenciaDia[0].HoraEntradaTarde, HoraBatida, "Tarde", vwFrequenciaDia[0].DTFrequencia.Date.Date), IDUsuario, vwFrequenciaDia[0].IDFrequencia) == 0)
                                    {
                                        msg = "Nao Inserido";
                                    }
                                    else
                                        msg = "Horario";

                                    //Total da jornada
                                    //adpFrequencia.UpdateTotHoras(TotalHorasDia(null, null, vwFrequenciaDia[0].HoraEntradaTarde,HoraBatida, "Tarde", vwFrequenciaDia[0].DTFrequencia.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);

                                    TotalHorasDia(null, null, vwFrequenciaDia[0].HoraEntradaTarde, HoraBatida, "Tarde", vwFrequenciaDia[0].DTFrequencia.Date.Date);
                                }

                                switch (msg)
                                {
                                    case "Add Noturno":
                                        msg = String.Format("{0}, adicional noturno iniciado com sucesso.", NomeUsuario.Trim());
                                        break;
                                    case "Carga Total":
                                        msg = String.Format("{0}, jornada finalizada.", NomeUsuario.Trim());
                                        break;
                                    case "Carga Incompleta":
                                        msg = String.Format("{0}, jornada finalizada com a carga horária incompleta.", NomeUsuario.Trim());
                                        break;
                                    case "Nao Atualizado":
                                        msg = String.Format("{0}, marcação não realizada. Repita o processo.", NomeUsuario.Trim());
                                        break;
                                }
                            }

                            break;
                    }
                    #endregion
                }
                else //Tratativa para regime de expediente
                {
                    #region Registros de Marcação de ponto Regime de expediente

                    //TBFrequencia.DataSet.EnforceConstraints = false;

                    //Dia seguinte retirado -- Os que fazem plantões irão utilizar outras rotinas
                    #region DiaSeguinte
                    DiaSeguinte = false;
                    //Tratativa para quem finaliza a jornada no dia seguinte
                    //if (!TBUsuario[0].IsHoraSaidaTardeNull())
                    //{
                    //if (TBUsuario[0].FinalizaDiaSeguinte && HoraBatida >= Convert.ToDateTime(string.Format("{0} 00:00:00.000", HoraBatida.Date.ToShortDateString())) && HoraBatida <= Convert.ToDateTime(string.Format("{0} 07:30:00.000", HoraBatida.Date.ToShortDateString())))
                    //{
                    //DateTime DTSeguinte = HoraBatida.Date.Date.AddDays(-1); //Varíavel que possibilita a jornada do dia anterior para complemento.
                    //TBFrequencia.BeginLoadData();
                    //adpFrequencia.FillFrequenciaDiaIDempresa(TBFrequencia, DTSeguinte.Date.Date, IDUsuario, IDEmpresa);
                    //adpFrequencia.FillFrequenciaDia(TBFrequencia, DTSeguinte.Date.Date, IDUsuario); //Verifica se há ponto registrado no dia com a varíavel DTSeguinte
                    //TBFrequencia.EndLoadData();
                    //DiaSeguinte = true;
                    //Verificar se existe ponto para o dia Anterior ---
                    //BatidaDiaAnterior = HoraBatida.AddDays(-1);
                    //adpFrequencia.FillFrequenciaDia(TBFrequencia, BatidaDiaAnterior.Date.Date, IDUsuario);
                    //}
                    //else
                    //{
                    //DiaSeguinte = false;
                    //TBFrequencia.BeginLoadData();
                    //adpFrequencia.FillFrequenciaDiaIDempresa(TBFrequencia, HoraBatida.Date.Date, IDUsuario,IDEmpresa); //Verifica se há ponto registrado no dia
                    //adpFrequencia.FillFrequenciaDia(TBFrequencia, HoraBatida.Date.Date, IDUsuario); //Verifica se há ponto registrado no dia
                    //TBFrequencia.EndLoadData();
                    //}

                    //
                    // else
                    //{
                    //DiaSeguinte = false;
                    //}
                    //}
                    //else
                    //{

                    //

                    #endregion
                    //------------------------------------------------------------------------
                    //--16/01/2014 - Alterei os limites entre entrada e saida de 2 para 1 hora.

                    //troquei para view por dia, para os que fazem regime de expediente.
                    //Procura por registros realizados no dia corrente


                    //19/09/2018

                    //Prevendo a saída da SEMOB.
                    //Se o registro estiver entre 00:00 e 02:00 da matina -- pesquisar o dia anterior para poder realizar a saída.
                    if (HoraBatida.Hour >= 0 && HoraBatida.Hour <= 2 && IDEmpresa == 50)
                    {
                        try
                        {
                            adpvwFrequenciaDia.Connection.Open();
                            adpvwFrequenciaDia.FillIDVinculoUsuario(vwFrequenciaDia, IDVinculoUsuarioParam, HoraBatida.Date.Date.AddDays(-1));
                            adpvwFrequenciaDia.Connection.Close();

                            //Caso tenho um registro do dia anterior, finaliza a saída 2.
                            //Total Horas

                            if (vwFrequenciaDia.Rows.Count > 0)
                            {

                            }

                        }
                        catch
                        {
                            adpvwFrequenciaDia.Connection.Close();
                            return "Jornada de trabalho não identificada no banco de dados. Tente Novamente!";
                        }
                    }
                    else
                    {
                        try
                        {
                            adpvwFrequenciaDia.Connection.Open();
                            adpvwFrequenciaDia.FillIDVinculoUsuario(vwFrequenciaDia, IDVinculoUsuarioParam, HoraBatida.Date.Date);
                            adpvwFrequenciaDia.Connection.Close();
                        }
                        catch
                        {
                            adpvwFrequenciaDia.Connection.Close();
                            return "Jornada de trabalho não identificada no banco de dados. Tente Novamente!";
                        }
                    }
                    //adpFrequencia.FillFrequenciaDiaIDempresa(TBFrequencia, HoraBatida.Date.Date, IDUsuario, IDEmpresa, IDVinculoUsuarioParam); //Verifica se há ponto registrado no dia

                    if (vwFrequenciaDia.Rows.Count == 0 && !DiaSeguinte) //Se número de linhas igual zero, não há registro de frequência para o dia -- Se DiaSeguinte verdadeiro, insere o fim da jornada no dia anterior
                    {
                        if (vwUsuarioGrid[vinculo].TotHorasDiarias >= 8)
                        {
                            if (Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.ToShortDateString(), HoraPadraoSaidaManha)) >= HoraBatida) //Se menor, tratar ou entrada ou saída da manhã (Entrada 1  - saída 1)
                            {
                                Limite = "";

                                if ((Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.ToShortDateString(), HoraPadraoSaidaManha)) > HoraBatida))
                                    Limite = Convert.ToString(Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.ToShortDateString(), HoraPadraoSaidaManha)) - HoraBatida);
                                else
                                    Limite = Convert.ToString(HoraBatida - Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.ToShortDateString(), HoraPadraoSaidaManha)));

                                if (Convert.ToDateTime(Limite) >= Convert.ToDateTime("01:00:00.000")) //Entrada da manhã - Entrada 1
                                {
                                    if (PermiteRegistroLimite(HoraBatida, Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.Date.ToShortDateString(), HoraPadraoEntradaManha)), Convert.ToDateTime(LimitePadraoEntradaSaida), vwUsuarioGrid[vinculo].AcessoEspecial))
                                    {
                                        //Caso insert retorne 0, manda msg para repetir operação. Caso contrário segue rotina
                                        if (adpFrequencia.Insert(IDUsuario, null, HoraBatida.Date, HoraBatida, null, null, null, null, null, HoraBatida.Month, HoraBatida.Year, IDEmpresa, IDVinculoUsuarioParam) == 0)
                                        {
                                            msg = "Nao Inserido";
                                        }

                                        switch (msg)
                                        {
                                            case "Horario":
                                                msg = String.Format("{0}, entrada 1 efetivada.", NomeUsuario.Trim());
                                                break;
                                            case "Tolerancia":
                                                msg = String.Format("{0}, entrada 1 efetivada.", NomeUsuario.Trim());
                                                break;
                                            case "Atraso inf 1 hora":
                                                msg = String.Format("{0}, entrada 1 efetivada.", NomeUsuario.Trim());
                                                //msg = String.Format("{0}, seu Registro de Entrada Manhã foi realizado com atraso inferior a 1 Hora.", NomeUsuario.Trim());
                                                break;
                                            case "Atraso sup 1 hora":
                                                msg = String.Format("{0}, entrada 1 efetivada.", NomeUsuario.Trim());
                                                //msg = String.Format("{0}, seu Registro de Entrada Manhã foi realizado com atraso superior a 1 Hora.", NomeUsuario.Trim());
                                                break;
                                            case "Nao Inserido":
                                                msg = String.Format("{0}, marcação não foi efetivada. Por favor, repita a operação.", NomeUsuario.Trim());
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        switch (msg)
                                        {
                                            case "excedido":
                                                msg = String.Format("{0}, limite para registro excedido em {1}", NomeUsuario.Trim(), Limite);
                                                break;
                                            case "antecipado":
                                                msg = String.Format("{0}, permissão para realizar registro em {1}", NomeUsuario.Trim(), Limite);
                                                break;
                                            case "turno justificado":
                                                msg = String.Format("{0}, Turno Justificado, Marcação de Ponto não realizada.", NomeUsuario.Trim());
                                                break;
                                            case "dia justificado":
                                                msg = String.Format("{0}, Jornada justificada na data corrente.", NomeUsuario.Trim());
                                                break;
                                        }
                                    }
                                }
                                else  // Saída da manhã - Saída 1
                                {
                                    if (adpFrequencia.Insert(IDUsuario, null, HoraBatida.Date, null, HoraBatida, null, null, null, null, HoraBatida.Month, HoraBatida.Year, IDEmpresa, IDVinculoUsuarioParam) == 0)
                                        msg = String.Format("{0}, marcação não efetivada. Por favor, repita a operação.", NomeUsuario.Trim());
                                    else
                                        msg = String.Format("{0}, saída 1 efetivada.", NomeUsuario.Trim());
                                }
                            }
                            else if (Convert.ToDateTime(HoraPadraoSaidaManha) < HoraBatida) // Se menor, cai direto para a parte da tarde - Segundo Período
                            {
                                if (Convert.ToDateTime(HoraPadraoSaidTarde) > HoraBatida)
                                {
                                    Limite = "";
                                    if (Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.ToShortDateString(), HoraPadraoSaidTarde)) > HoraBatida)
                                        Limite = Convert.ToString(Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.ToShortDateString(), HoraPadraoSaidTarde)) - HoraBatida);
                                    else
                                        Limite = Convert.ToString(HoraBatida - Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.ToShortDateString(), HoraPadraoSaidTarde)));

                                    if (Convert.ToDateTime(Limite) >= Convert.ToDateTime("01:00:00.000")) //Entrada da tarde - Entrada 2
                                    {
                                        if (PermiteRegistroLimite(HoraBatida, Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.Date.ToShortDateString(), HoraPadraoEntradaTarde)), Convert.ToDateTime(LimitePadraoEntradaSaida), vwUsuarioGrid[vinculo].AcessoEspecial))
                                        {
                                            if (adpFrequencia.Insert(IDUsuario, null, HoraBatida.Date, null, null, HoraBatida, null, null, null, HoraBatida.Month, HoraBatida.Year, IDEmpresa, IDVinculoUsuarioParam) == 0)
                                            {
                                                msg = "Nao Inserido";
                                            }
                                            switch (msg)
                                            {
                                                case "Horario":

                                                    msg = String.Format("{0}, entrada 2 efetivada.", NomeUsuario.Trim());
                                                    break;
                                                case "Tolerancia":
                                                    msg = String.Format("{0}, entrada 2 efetivada.", NomeUsuario.Trim());
                                                    //msg = String.Format("{0}, seu Registro de Entrada Tarde foi realizado com sucesso.", NomeUsuario.Trim());
                                                    break;
                                                case "Atraso inf 1 hora":
                                                    msg = String.Format("{0}, entrada 2 efetivada.", NomeUsuario.Trim());
                                                    //msg = String.Format("{0}, seu Registro de Entrada Tarde foi realizado com atraso inferior a 1 Hora.", NomeUsuario.Trim());
                                                    break;
                                                case "Atraso sup 1 hora":
                                                    msg = String.Format("{0}, entrada 2 efetivada.", NomeUsuario.Trim());
                                                    //msg = String.Format("{0}, seu Registro de Entrada Tarde foi realizado com atraso superior a 1 Hora.", NomeUsuario.Trim());
                                                    break;
                                                case "Nao Inserido":
                                                    msg = String.Format("{0}, marcação não efetivada. Por favor, repita a operação.", NomeUsuario.Trim());
                                                    break;
                                            }
                                        }

                                    }
                                    else
                                    {
                                        if (adpFrequencia.Insert(IDUsuario, null, HoraBatida.Date, null, null, null, HoraBatida, null, null, HoraBatida.Month, HoraBatida.Year, IDEmpresa, IDVinculoUsuarioParam) == 0)
                                            msg = String.Format("{0}, marcação não efetivada. Por favor, repita a operação.", NomeUsuario.Trim());
                                        else
                                            msg = String.Format("{0}, jornada finalizada.", NomeUsuario.Trim());
                                    }
                                }
                                else
                                {
                                    Limite = "";
                                    Limite = Convert.ToString(HoraBatida - Convert.ToDateTime(HoraPadraoSaidTarde));

                                    if (Convert.ToDateTime(Limite) <= Convert.ToDateTime("01:00:00.000"))
                                    {
                                        if (adpFrequencia.Insert(IDUsuario, null, HoraBatida.Date, null, null, null, HoraBatida, null, null, HoraBatida.Month, HoraBatida.Year, IDEmpresa, IDVinculoUsuarioParam) == 0)
                                            msg = String.Format("{0}, marcação não efetivada. Por favor, repita a operação", NomeUsuario.Trim());
                                        else
                                            msg = String.Format("{0}, jornada finalizada.", NomeUsuario.Trim());
                                    }
                                    else
                                    {
                                        //17-03-2015 - Verificar aqui... Pode ser que o erro esteja acontecendo AQUI -- ALTERADO - Conforme padrão das outras Condições.

                                        if (adpFrequencia.Insert(IDUsuario, null, HoraBatida.Date, null, null, HoraBatida, null, null, null, HoraBatida.Month, HoraBatida.Year, IDEmpresa, IDVinculoUsuarioParam) == 0)
                                        {
                                            msg = "Nao Inserido";
                                        }
                                        switch (msg)
                                        {
                                            case "Horario":

                                                msg = String.Format("{0}, entrada 2 efetivada.", NomeUsuario.Trim());
                                                break;
                                            case "Tolerancia":
                                                msg = String.Format("{0}, entrada 2 efetivada.", NomeUsuario.Trim());
                                                //msg = String.Format("{0}, seu Registro de Entrada Tarde foi realizado com sucesso.", NomeUsuario.Trim());
                                                break;
                                            case "Atraso inf 1 hora":
                                                msg = String.Format("{0}, entrada 2 efetivada.", NomeUsuario.Trim());
                                                //msg = String.Format("{0}, seu Registro de Entrada Tarde foi realizado com atraso inferior a 1 Hora.", NomeUsuario.Trim());
                                                break;
                                            case "Atraso sup 1 hora":
                                                msg = String.Format("{0}, entrada 2 efetivada.", NomeUsuario.Trim());
                                                //msg = String.Format("{0}, seu Registro de Entrada Tarde foi realizado com atraso superior a 1 Hora.", NomeUsuario.Trim());
                                                break;
                                            case "Nao Inserido":
                                                msg = String.Format("{0}, marcação não efetivada. Por favor, repita a operação.", NomeUsuario.Trim());
                                                break;
                                        }
                                    }
                                }
                            }
                        }//Pessoas que trabalham 6 horas diretas ou mais
                        else if (HoraBatida <= Convert.ToDateTime("11:00:00.000"))
                        {
                            if (PermiteRegistroLimite(HoraBatida, Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.Date.ToShortDateString(), HoraPadraoEntradaManha)), Convert.ToDateTime(LimitePadraoEntradaSaida), vwUsuarioGrid[vinculo].AcessoEspecial))
                            {
                                if (adpFrequencia.Insert(IDUsuario, null, HoraBatida.Date, HoraBatida, null, null, null, null, null, HoraBatida.Month, HoraBatida.Year, IDEmpresa, IDVinculoUsuarioParam) == 0)
                                {
                                    msg = "Nao Inserido";
                                }

                                switch (msg)
                                {
                                    case "Horario":
                                        msg = String.Format("{0}, entrada 1 efetivada.", NomeUsuario.Trim());
                                        break;
                                    case "Tolerancia":
                                        msg = String.Format("{0}, entrada 1 efetivada.", NomeUsuario.Trim());
                                        //msg = String.Format("{0}, seu Registro de Entrada Manhã foi realizado com sucesso.", NomeUsuario.Trim());
                                        break;
                                    case "Atraso inf 1 hora":
                                        msg = String.Format("{0}, entrada 1 efetivada.", NomeUsuario.Trim());
                                        //msg = String.Format("{0}, seu Registro de Entrada Manhã foi realizado com atraso inferior a 1 Hora.", NomeUsuario.Trim());
                                        break;
                                    case "Atraso sup 1 hora":
                                        msg = String.Format("{0}, entrada 1 efetivada.", NomeUsuario.Trim());
                                        //msg = String.Format("{0}, seu Registro de Entrada Manhã foi realizado com atraso superior a 1 Hora.", NomeUsuario.Trim());
                                        break;
                                    case "Nao Inserido":
                                        msg = String.Format("{0}, marcação não efetivada. Por favor, repita a operação.", NomeUsuario.Trim());
                                        break;
                                }
                            }
                            else
                            {
                                switch (msg)
                                {
                                    case "excedido":
                                        msg = String.Format("{0}, limite para registro excedido em {1}", NomeUsuario.Trim(), Limite);
                                        break;
                                    case "antecipado":
                                        msg = String.Format("{0}, permissão para realizar registro em {1}", NomeUsuario.Trim(), Limite);
                                        break;
                                    case "turno justificado":
                                        msg = String.Format("{0}, turno justificado, marcação de ponto não realizada.", NomeUsuario.Trim());
                                        break;
                                    case "dia justificado":
                                        msg = String.Format("{0}, Jornada justificada na data corrente.", NomeUsuario.Trim());
                                        break;
                                }

                            }
                        }
                        else //Se passou das onze, registra na parte da tarde
                        {
                            if (PermiteRegistroLimite(HoraBatida, Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.Date.ToShortDateString(), HoraPadraoEntradaTarde)), Convert.ToDateTime(LimitePadraoEntradaSaida), vwUsuarioGrid[vinculo].AcessoEspecial))
                            {
                                if (adpFrequencia.Insert(IDUsuario, null, HoraBatida.Date, null, null, HoraBatida, null, null, null, HoraBatida.Month, HoraBatida.Year, IDEmpresa, IDVinculoUsuarioParam) == 0)
                                {
                                    msg = "Nao Inserido";
                                }
                                switch (msg)
                                {
                                    case "Horario":

                                        msg = String.Format("{0}, entrada 2 efetivada.", NomeUsuario.Trim());
                                        break;
                                    case "Tolerancia":
                                        msg = String.Format("{0}, entrada 2 efetivada.", NomeUsuario.Trim());
                                        //msg = String.Format("{0}, seu Registro de Entrada Tarde foi realizado com sucesso.", NomeUsuario.Trim());
                                        break;
                                    case "Atraso inf 1 hora":
                                        msg = String.Format("{0}, entrada 2 efetivada.", NomeUsuario.Trim());
                                        //msg = String.Format("{0}, seu Registro de Entrada Tarde foi realizado com atraso inferior a 1 Hora.", NomeUsuario.Trim());
                                        break;
                                    case "Atraso sup 1 hora":
                                        msg = String.Format("{0}, entrada 2 efetivada.", NomeUsuario.Trim());
                                        //msg = String.Format("{0}, seu Registro de Entrada Tarde foi realizado com atraso superior a 1 Hora.", NomeUsuario.Trim());
                                        break;
                                    case "Nao Inserido":
                                        msg = String.Format("{0}, marcação não efetivada. Por favor, repita a operação.", NomeUsuario.Trim());
                                        break;
                                }
                            }
                            else
                            {
                                switch (msg)
                                {
                                    case "excedido":
                                        msg = String.Format("{0}, limite para registro excedido em {1}", NomeUsuario.Trim(), Limite);
                                        break;
                                    case "antecipado":
                                        msg = String.Format("{0}, permissão para realizar registro em {1}", NomeUsuario.Trim(), Limite);
                                        break;
                                    case "turno justificado":
                                        msg = String.Format("{0}, turno justificado, marcação de ponto não realizada.", NomeUsuario.Trim());
                                        break;
                                    case "dia justificado":
                                        msg = String.Format("{0}, Jornada justificada na data corrente.", NomeUsuario.Trim());
                                        break;
                                }
                            }
                        }
                    }

                    else if (vwFrequenciaDia.Rows.Count > 0) // Se maior tratativa de outros horários
                    {
                        //Se houver uma falta lançada, não registra frequência
                        if (!vwFrequenciaDia[0].IsIDMotivoFaltaNull())
                        {
                            if (FaltaLancada(vwFrequenciaDia[0].IDMotivoFalta)) // Se o motivo for 47, não bate ponto.
                                return string.Format("{0}, há uma falta lançada na data corrente. Impossível registrar frequência!", NomeUsuario);
                        }

                        #region 8 horas Diárias
                        if (vwUsuarioGrid[vinculo].TotHorasDiarias >= 8) //Tratativa para pessoas que trabalham 8 horas diretas
                        {
                            if (vwFrequenciaDia[0].IsHoraSaidaManhaNull() && !vwFrequenciaDia[0].IsHoraEntraManhaNull() && HoraBatida < Convert.ToDateTime(HoraPadraoSaidTarde).AddHours(1))
                            {
                                Limite = "";
                                LimiteIntervalo = string.Empty;
                                Limite = Convert.ToString(HoraBatida - vwFrequenciaDia[0].HoraEntraManha);

                                if (Convert.ToDateTime(Limite) <= Convert.ToDateTime("06:30:00.000") && vwFrequenciaDia[0].IsHoraSaidaManhaNull() && (vwFrequenciaDia[0].HoraEntraManha != HoraBatida)) // Saída Manhã - Saída 1
                                {
                                    LimiteIntervalo = Convert.ToString(HoraBatida - vwFrequenciaDia[0].HoraEntraManha);

                                    if (Convert.ToDateTime(LimiteIntervalo) > Convert.ToDateTime("00:10:00.000"))
                                    {

                                        if (PermiteRegistroLimite(HoraBatida, Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.Date.ToShortDateString(), HoraPadraoSaidaManha)), Convert.ToDateTime(LimitePadraoEntradaSaida), vwUsuarioGrid[vinculo].AcessoEspecial))
                                        {
                                            //    //Total Horas
                                            DateTime? Entrada1, Saida1, Entrada2, Saida2;

                                            if (!vwFrequenciaDia[0].IsHoraEntraManhaNull())
                                                Entrada1 = vwFrequenciaDia[0].HoraEntraManha;
                                            else
                                                Entrada1 = null;

                                            if (!vwFrequenciaDia[0].IsHoraSaidaManhaNull())
                                                Saida1 = vwFrequenciaDia[0].HoraSaidaManha;
                                            else
                                                Saida1 = HoraBatida;


                                            Entrada2 = null;

                                            Saida2 = null;

                                            //AQUI - FILTRAR OS POR IDVINCULO TAMBÉM
                                            //Se retorno > 0 mensagem de sucesso.. Senão, apresentar msg de erro.
                                            if (adpFrequencia.UpdateSaidaManha(HoraBatida, TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Manha", HoraBatida.Date.Date), IDUsuario, HoraBatida.Date, IDVinculoUsuarioParam) == 0)
                                            {
                                                msg = "Nao Atualizado";
                                            }
                                            else
                                            {

                                                TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Manha", HoraBatida.Date.Date);

                                                //    adpFrequencia.UpdateTotHoras(TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Manha", HoraBatida.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);
                                            }


                                            switch (msg)
                                            {
                                                case "Carga Total":
                                                    msg = String.Format("{0}, saída 1 efetivada.", NomeUsuario.Trim());
                                                    break;
                                                case "Carga Incompleta":
                                                    msg = String.Format("{0}, saída 1 efetivada.", NomeUsuario.Trim());
                                                    //msg = String.Format("{0}, frequência registrada com a carga horária total incompleta. Saída da Manhã.", NomeUsuario.Trim());
                                                    break;
                                                case "Nao Atualizado":
                                                    msg = String.Format("{0}, marcação não realizada. Repita o processo.", NomeUsuario.Trim());
                                                    break;
                                            }
                                        }
                                        else
                                        {
                                            switch (msg)
                                            {
                                                case "excedido":
                                                    msg = String.Format("{0}, limite para registro excedido em {1}", NomeUsuario.Trim(), Limite);
                                                    break;
                                                case "antecipado":
                                                    msg = String.Format("{0}, permissão para realizar registro em {1}", NomeUsuario.Trim(), Limite);
                                                    break;
                                                case "turno justificado":
                                                    msg = String.Format("{0}, turno justificado, marcação de ponto não realizada.", NomeUsuario.Trim());
                                                    break;
                                                case "dia justificado":
                                                    msg = String.Format("{0}, Jornada justificada na data corrente.", NomeUsuario.Trim());
                                                    break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        msg = string.Format("{0}, respeite o intervalo de 10 minutos entre as marcações.", NomeUsuario);
                                    }
                                }
                                else if (vwFrequenciaDia[0].IsHoraEntradaTardeNull() || vwFrequenciaDia[0].IsHoraSaidaTardeNull())
                                {
                                    Limite = "";
                                    if (Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.ToShortDateString(), HoraPadraoSaidTarde)) > HoraBatida)
                                        Limite = Convert.ToString(Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.ToShortDateString(), HoraPadraoSaidTarde)) - HoraBatida);
                                    else
                                        Limite = Convert.ToString(HoraBatida - Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.ToShortDateString(), HoraPadraoSaidTarde)));

                                    if (Convert.ToDateTime(Limite) >= Convert.ToDateTime("01:00:00.000") && vwFrequenciaDia[0].IsHoraEntradaTardeNull() && (vwFrequenciaDia[0].HoraEntraManha != HoraBatida) && vwFrequenciaDia[0].IsHoraSaidaTardeNull()) //Entrada tarde Entrada 2
                                    {
                                        Limite = "";
                                        if (Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.ToShortDateString(), HoraPadraoSaidTarde)) > HoraBatida)
                                            Limite = Convert.ToString(Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.ToShortDateString(), HoraPadraoSaidTarde)) - HoraBatida);
                                        else
                                            Limite = Convert.ToString(HoraBatida - Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.ToShortDateString(), HoraPadraoSaidTarde)));

                                        if (!vwFrequenciaDia[0].IsHoraSaidaManhaNull()) ///Se não for nulo, faz o comparativo.
                                        {
                                            LimiteEntradaSaida = string.Empty;
                                            LimiteEntradaSaida = Convert.ToString(HoraBatida - vwFrequenciaDia[0].HoraSaidaManha);

                                            if (Convert.ToDateTime(LimiteEntradaSaida) >= Convert.ToDateTime(LimitePadraoAlmoco))
                                            {
                                                if (Convert.ToDateTime(Limite) >= Convert.ToDateTime("01:00:00.000"))
                                                {
                                                    if (PermiteRegistroLimite(HoraBatida, Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.Date.ToShortDateString(), HoraPadraoEntradaTarde)), Convert.ToDateTime(LimitePadraoEntradaSaida), vwUsuarioGrid[vinculo].AcessoEspecial))
                                                    {
                                                        //Prevendo hora de almoço superior ou igual a uma hora
                                                        if (vwFrequenciaDia[0].IsHoraSaidaManhaNull())
                                                        {
                                                            if (adpFrequencia.UpdateEntradaTarde(HoraBatida, vwFrequenciaDia[0].TotalHorasDiaSegundos, IDUsuario, HoraBatida.Date, IDVinculoUsuarioParam) > 0)
                                                                msg = string.Format("{0}, entrada 2 efetivada.", NomeUsuario);
                                                            else
                                                                msg = string.Format("{0}, marcação não realizada. Por favor, repita a operação", NomeUsuario);
                                                        }
                                                        else
                                                        {
                                                            LimiteEntradaSaida = string.Empty;
                                                            LimiteEntradaSaida = Convert.ToString(HoraBatida - vwFrequenciaDia[0].HoraSaidaManha);

                                                            if (Convert.ToDateTime(LimiteEntradaSaida) >= Convert.ToDateTime(string.Format("{0} 01:00:00", HoraBatida.ToShortDateString())))
                                                            {
                                                                if (adpFrequencia.UpdateEntradaTarde(HoraBatida, vwFrequenciaDia[0].TotalHorasDiaSegundos, IDUsuario, HoraBatida.Date, IDVinculoUsuarioParam) > 0)
                                                                    msg = string.Format("{0}, entrada 2 efetivada.", NomeUsuario);
                                                                else
                                                                    msg = string.Format("{0}, marcação não realizada. Repita a operação", NomeUsuario);
                                                            }
                                                            else
                                                                msg = string.Format("{0}, horário de almoço. Permissão para registro em {1}.", NomeUsuario, Convert.ToDateTime(LimiteEntradaSaida).ToShortTimeString());
                                                        }
                                                    }
                                                    else
                                                    {
                                                        switch (msg)
                                                        {
                                                            case "excedido":
                                                                msg = String.Format("{0}, limite para registro excedido em {1}", NomeUsuario.Trim(), Limite);
                                                                break;
                                                            case "antecipado":
                                                                msg = String.Format("{0}, permissão para realizar registro em {1}", NomeUsuario.Trim(), Limite);
                                                                break;
                                                            case "turno justificado":
                                                                msg = String.Format("{0}, turno justificado, marcação de ponto não realizada.", NomeUsuario.Trim());
                                                                break;
                                                            case "dia justificado":
                                                                msg = String.Format("{0}, Jornada justificada na data corrente.", NomeUsuario.Trim());
                                                                break;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (PermiteRegistroLimite(HoraBatida, Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.Date.ToShortDateString(), HoraPadraoSaidTarde)), Convert.ToDateTime(LimitePadraoEntradaSaida), vwUsuarioGrid[vinculo].AcessoEspecial))
                                                    {
                                                        //Total Horas
                                                        DateTime? Entrada1, Saida1, Entrada2, Saida2;

                                                        if (!vwFrequenciaDia[0].IsHoraEntraManhaNull())
                                                            Entrada1 = vwFrequenciaDia[0].HoraEntraManha;
                                                        else
                                                            Entrada1 = null;

                                                        if (!vwFrequenciaDia[0].IsHoraSaidaManhaNull())
                                                            Saida1 = vwFrequenciaDia[0].HoraSaidaManha;
                                                        else
                                                            Saida1 = null;

                                                        if (!vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                                                            Entrada2 = vwFrequenciaDia[0].HoraEntradaTarde;
                                                        else
                                                            Entrada2 = null;

                                                        Saida2 = HoraBatida;

                                                        if (adpFrequencia.UpdateSaidaTarde(HoraBatida, TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), IDUsuario, vwFrequenciaDia[0].IDFrequencia) == 0)
                                                        {
                                                            msg = "Nao Atualizado";
                                                        }
                                                        else
                                                        {


                                                            //adpFrequencia.UpdateTotHoras(TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);
                                                            TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date);

                                                            switch (msg)
                                                            {
                                                                case "Carga Total":
                                                                    msg = String.Format("{0}, jornada finalizada.", NomeUsuario.Trim());
                                                                    break;
                                                                case "Carga Incompleta":
                                                                    msg = String.Format("{0}, jornada finalizada com a carga horária total incompleta.", NomeUsuario.Trim());
                                                                    break;
                                                                case "Nao Atualizado":
                                                                    msg = String.Format("{0}, marcação não realizada. Repita a operação", NomeUsuario.Trim());
                                                                    break;
                                                            }

                                                        }
                                                    }
                                                    else
                                                    {
                                                        switch (msg)
                                                        {
                                                            case "excedido":
                                                                msg = String.Format("{0}, limite para registro excedido em {1}", NomeUsuario.Trim(), Limite);
                                                                break;
                                                            case "antecipado":
                                                                msg = String.Format("{0}, permissão para realizar registro em {1}", NomeUsuario.Trim(), Limite);
                                                                break;
                                                            case "turno justificado":
                                                                msg = String.Format("{0}, turno justificado, marcação de ponto não realizada.", NomeUsuario.Trim());
                                                                break;
                                                            case "dia justificado":
                                                                msg = String.Format("{0}, Jornada justificada na data corrente.", NomeUsuario.Trim());
                                                                break;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (Convert.ToDateTime(Limite) >= Convert.ToDateTime("01:00:00.000"))
                                            {
                                                if (PermiteRegistroLimite(HoraBatida, Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.Date.ToShortDateString(), HoraPadraoEntradaTarde)), Convert.ToDateTime(LimitePadraoEntradaSaida), vwUsuarioGrid[vinculo].AcessoEspecial))
                                                {
                                                    //Prevendo hora de almoço superior ou igual a uma hora
                                                    if (vwFrequenciaDia[0].IsHoraSaidaManhaNull())
                                                    {
                                                        if (adpFrequencia.UpdateEntradaTarde(HoraBatida, vwFrequenciaDia[0].TotalHorasDiaSegundos, IDUsuario, HoraBatida.Date, IDVinculoUsuarioParam) > 0)
                                                            msg = string.Format("{0}, entrada 2 efetivada.", NomeUsuario);
                                                        else
                                                            msg = string.Format("{0}, marcação não efetivada. Repita a operação", NomeUsuario);
                                                    }
                                                    else
                                                    {
                                                        LimiteEntradaSaida = string.Empty;
                                                        LimiteEntradaSaida = Convert.ToString(HoraBatida - vwFrequenciaDia[0].HoraSaidaManha);

                                                        if (Convert.ToDateTime(LimiteEntradaSaida) >= Convert.ToDateTime(string.Format("{0} 01:00:00", HoraBatida.ToShortDateString())))
                                                        {
                                                            if (adpFrequencia.UpdateEntradaTarde(HoraBatida, vwFrequenciaDia[0].TotalHorasDiaSegundos, IDUsuario, HoraBatida.Date, IDVinculoUsuarioParam) > 0)
                                                                msg = string.Format("{0}, entrada 2 efetivada.", NomeUsuario);
                                                            else
                                                                msg = string.Format("{0}, marcação não efetivada. Repita a operação", NomeUsuario);
                                                        }
                                                        else
                                                            msg = string.Format("{0}, horário de almoço. Permissão para registro em {1}.", NomeUsuario, Convert.ToDateTime(LimiteEntradaSaida).ToShortTimeString());
                                                    }
                                                }
                                                else
                                                {
                                                    switch (msg)
                                                    {
                                                        case "excedido":
                                                            msg = String.Format("{0}, limite para registro excedido em {1}", NomeUsuario.Trim(), Limite);
                                                            break;
                                                        case "antecipado":
                                                            msg = String.Format("{0}, permissão para realizar registro em {1}", NomeUsuario.Trim(), Limite);
                                                            break;
                                                        case "turno justificado":
                                                            msg = String.Format("{0}, turno justificado, marcação de ponto não realizada.", NomeUsuario.Trim());
                                                            break;
                                                        case "dia justificado":
                                                            msg = String.Format("{0}, Jornada justificada na data corrente.", NomeUsuario.Trim());
                                                            break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else if (vwFrequenciaDia[0].IsHoraSaidaTardeNull() && (vwFrequenciaDia[0].HoraEntraManha != HoraBatida) && !vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                                    {
                                        LimiteIntervalo = string.Empty;

                                        LimiteIntervalo = Convert.ToString(HoraBatida - vwFrequenciaDia[0].HoraEntradaTarde);

                                        if (Convert.ToDateTime(LimiteIntervalo) > Convert.ToDateTime("00:10:00.000"))
                                        {
                                            if (PermiteRegistroLimite(HoraBatida, Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.Date.ToShortDateString(), HoraPadraoSaidTarde)), Convert.ToDateTime(LimitePadraoEntradaSaida), vwUsuarioGrid[vinculo].AcessoEspecial))
                                            {

                                                //Total Horas
                                                DateTime? Entrada1, Saida1, Entrada2, Saida2;

                                                if (!vwFrequenciaDia[0].IsHoraEntraManhaNull())
                                                    Entrada1 = vwFrequenciaDia[0].HoraEntraManha;
                                                else
                                                    Entrada1 = null;

                                                if (!vwFrequenciaDia[0].IsHoraSaidaManhaNull())
                                                    Saida1 = vwFrequenciaDia[0].HoraSaidaManha;
                                                else
                                                    Saida1 = null;

                                                if (!vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                                                    Entrada2 = vwFrequenciaDia[0].HoraEntradaTarde;
                                                else
                                                    Entrada2 = null;

                                                Saida2 = HoraBatida;

                                                if (adpFrequencia.UpdateSaidaTarde(HoraBatida, TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), IDUsuario, vwFrequenciaDia[0].IDFrequencia) == 0)
                                                {
                                                    msg = "Nao Atualizado";
                                                }
                                                else
                                                {


                                                    //adpFrequencia.UpdateTotHoras(TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);
                                                    TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date);

                                                    switch (msg)
                                                    {
                                                        case "Carga Total":
                                                            msg = String.Format("{0}, jornada finalizada.", NomeUsuario.Trim());
                                                            break;
                                                        case "Carga Incompleta":
                                                            msg = String.Format("{0}, jornada finalizada com a carga horária total incompleta.", NomeUsuario.Trim());
                                                            break;
                                                        case "Nao Atualizado":
                                                            msg = String.Format("{0}, marcação não efetivada. Repita a operação", NomeUsuario.Trim());
                                                            break;
                                                    }

                                                }
                                            }
                                            else
                                            {
                                                switch (msg)
                                                {
                                                    case "excedido":
                                                        msg = String.Format("{0}, limite para registro excedido em {1}", NomeUsuario.Trim(), Limite);
                                                        break;
                                                    case "antecipado":
                                                        msg = String.Format("{0}, permissão para realizar registro em {1}", NomeUsuario.Trim(), Limite);
                                                        break;
                                                    case "turno justificado":
                                                        msg = String.Format("{0}, turno justificado, marcação de ponto não realizada.", NomeUsuario.Trim());
                                                        break;
                                                    case "dia justificado":
                                                        msg = String.Format("{0}, Jornada justificada na data corrente.", NomeUsuario.Trim());
                                                        break;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            msg = string.Format("{0}, respeite o intervalo de 10 minutos entre as marcações.", NomeUsuario);
                                        }
                                    }
                                    else if (vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                                    {
                                        if (PermiteRegistroLimite(HoraBatida, Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.Date.ToShortDateString(), HoraPadraoSaidTarde)), Convert.ToDateTime(LimitePadraoEntradaSaida), vwUsuarioGrid[vinculo].AcessoEspecial))
                                        {

                                            //Total Horas
                                            DateTime? Entrada1, Saida1, Entrada2, Saida2;

                                            if (!vwFrequenciaDia[0].IsHoraEntraManhaNull())
                                                Entrada1 = vwFrequenciaDia[0].HoraEntraManha;
                                            else
                                                Entrada1 = null;

                                            if (!vwFrequenciaDia[0].IsHoraSaidaManhaNull())
                                                Saida1 = vwFrequenciaDia[0].HoraSaidaManha;
                                            else
                                                Saida1 = null;

                                            if (!vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                                                Entrada2 = vwFrequenciaDia[0].HoraEntradaTarde;
                                            else
                                                Entrada2 = null;

                                            Saida2 = HoraBatida;

                                            if (adpFrequencia.UpdateSaidaTarde(HoraBatida, TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), IDUsuario, vwFrequenciaDia[0].IDFrequencia) == 0)
                                            {
                                                msg = "Nao Atualizado";
                                            }
                                            else
                                            {

                                                //adpFrequencia.UpdateTotHoras(TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);
                                                TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date);
                                                switch (msg)
                                                {
                                                    case "Carga Total":
                                                        msg = String.Format("{0}, jornada finalizada.", NomeUsuario.Trim());
                                                        break;
                                                    case "Carga Incompleta":
                                                        msg = String.Format("{0}, jornada finalizada com a carga horária total incompleta.", NomeUsuario.Trim());
                                                        break;
                                                    case "Nao Atualizado":
                                                        msg = String.Format("{0}, marcação não efetivada. Repita a operação", NomeUsuario.Trim());
                                                        break;
                                                }

                                            }
                                        }
                                        else
                                        {
                                            switch (msg)
                                            {
                                                case "excedido":
                                                    msg = String.Format("{0}, limite para registro excedido em {1}", NomeUsuario.Trim(), Limite);
                                                    break;
                                                case "antecipado":
                                                    msg = String.Format("{0}, permissão para realizar registro em {1}", NomeUsuario.Trim(), Limite);
                                                    break;
                                                case "turno justificado":
                                                    msg = String.Format("{0}, turno justificado, marcação de ponto não realizada.", NomeUsuario.Trim());
                                                    break;
                                                case "dia justificado":
                                                    msg = String.Format("{0}, Jornada justificada na data corrente.", NomeUsuario.Trim());
                                                    break;
                                            }
                                        }
                                    }
                                }
                                else if (!vwFrequenciaDia[0].IsHoraEntradaTardeNull() || !vwFrequenciaDia[0].IsHoraSaidaTardeNull())
                                {
                                    //msg = string.Format("{0}, jornada completa para o período.", NomeUsuario); Retirado ... Poder realizar a saida da tarde.

                                    if (PermiteRegistroLimite(HoraBatida, Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.Date.ToShortDateString(), HoraPadraoSaidTarde)), Convert.ToDateTime(LimitePadraoEntradaSaida), vwUsuarioGrid[vinculo].AcessoEspecial))
                                    {
                                        //Total Horas
                                        DateTime? Entrada1, Saida1, Entrada2, Saida2;

                                        if (!vwFrequenciaDia[0].IsHoraEntraManhaNull())
                                            Entrada1 = vwFrequenciaDia[0].HoraEntraManha;
                                        else
                                            Entrada1 = null;

                                        if (!vwFrequenciaDia[0].IsHoraSaidaManhaNull())
                                            Saida1 = vwFrequenciaDia[0].HoraSaidaManha;
                                        else
                                            Saida1 = null;

                                        if (!vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                                            Entrada2 = vwFrequenciaDia[0].HoraEntradaTarde;
                                        else
                                            Entrada2 = null;

                                        Saida2 = HoraBatida;

                                        if (adpFrequencia.UpdateSaidaTarde(HoraBatida, TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), IDUsuario, vwFrequenciaDia[0].IDFrequencia) == 0)
                                        {
                                            msg = "Nao Atualizado";
                                        }
                                        else
                                        {

                                            //adpFrequencia.UpdateTotHoras(TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);
                                            TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date);
                                            switch (msg)
                                            {
                                                case "Carga Total":
                                                    msg = String.Format("{0}, jornada finalizada.", NomeUsuario.Trim());
                                                    break;
                                                case "Carga Incompleta":
                                                    msg = String.Format("{0}, jornada finalizada com a carga horária total incompleta.", NomeUsuario.Trim());
                                                    break;
                                                case "Nao Atualizado":
                                                    msg = String.Format("{0}, marcação não realizada. Repita a operação", NomeUsuario.Trim());
                                                    break;
                                            }

                                        }
                                    }
                                    else
                                    {
                                        switch (msg)
                                        {
                                            case "excedido":
                                                msg = String.Format("{0}, limite para registro excedido em {1}", NomeUsuario.Trim(), Limite);
                                                break;
                                            case "antecipado":
                                                msg = String.Format("{0}, permissão para realizar registro em {1}", NomeUsuario.Trim(), Limite);
                                                break;
                                            case "turno justificado":
                                                msg = String.Format("{0}, turno justificado, marcação de ponto não realizada.", NomeUsuario.Trim());
                                                break;
                                            case "dia justificado":
                                                msg = String.Format("{0}, Jornada justificada na data corrente.", NomeUsuario.Trim());
                                                break;
                                        }
                                    }

                                }
                            }
                            else if (vwFrequenciaDia[0].IsHoraSaidaManhaNull() && vwFrequenciaDia[0].IsHoraEntraManhaNull() && HoraBatida < Convert.ToDateTime(HoraPadraoSaidTarde).AddHours(1) && !vwFrequenciaDia[0].IsIDMotivoFaltaNull()) //Prevendo a justificativa em caso de não batida matinal
                            {
                                if (PermiteRegistroLimite(HoraBatida, Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.Date.ToShortDateString(), HoraPadraoSaidaManha)), Convert.ToDateTime(LimitePadraoEntradaSaida), vwUsuarioGrid[vinculo].AcessoEspecial))
                                {
                                    if (vwFrequenciaDia[0].IDTPJustificativa != 1 && (vwFrequenciaDia[0].TurnoJustificado == "Mat" || vwFrequenciaDia[0].TurnoJustificado == "0"))
                                    {
                                        //Total Horas
                                        DateTime? Entrada1, Saida1, Entrada2, Saida2;

                                        if (!vwFrequenciaDia[0].IsHoraEntraManhaNull())
                                            Entrada1 = vwFrequenciaDia[0].HoraEntraManha;
                                        else
                                            Entrada1 = null;

                                        if (!vwFrequenciaDia[0].IsHoraSaidaManhaNull())
                                            Saida1 = vwFrequenciaDia[0].HoraSaidaManha;
                                        else
                                            Saida1 = HoraBatida;


                                        Entrada2 = null;

                                        Saida2 = null;
                                        //Se retorno > 0 mensagem de sucesso.. Senão, apresentar msg de erro.
                                        if (adpFrequencia.UpdateSaidaManha(HoraBatida, TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Manha", HoraBatida.Date.Date), IDUsuario, HoraBatida.Date, IDVinculoUsuarioParam) == 0)
                                        {
                                            msg = "Nao Atualizado";
                                        }
                                        else
                                        {

                                            //adpFrequencia.UpdateTotHoras(TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Manha", HoraBatida.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);

                                            TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Manha", HoraBatida.Date.Date);
                                        }

                                    }
                                    else if (HoraBatida > Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.ToShortDateString(), vwUsuarioGrid[vinculo].HoraSaidaManha)).AddHours(1) && vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                                    {//Casa haja uma justificativa de meio período, dar a entrada da tarde
                                        if (adpFrequencia.UpdateEntradaTarde(HoraBatida, vwFrequenciaDia[0].TotalHorasDiaSegundos, IDUsuario, HoraBatida.Date, IDVinculoUsuarioParam) > 0)
                                            msg = string.Format("{0}, entrada 2 efetivada.", NomeUsuario);
                                        else
                                            msg = string.Format("{0}, marcação não efetivada. Repita a operação", NomeUsuario);
                                    }
                                    else if (!vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                                    {
                                        if (vwFrequenciaDia[0].HoraEntradaTarde < HoraBatida.AddMinutes(15))
                                        {
                                            //Saída da tarde
                                            if (PermiteRegistroLimite(HoraBatida, Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.Date.ToShortDateString(), HoraPadraoSaidTarde)), Convert.ToDateTime(LimitePadraoEntradaSaida), vwUsuarioGrid[vinculo].AcessoEspecial))
                                            {
                                                //Total Horas
                                                DateTime? Entrada1, Saida1, Entrada2, Saida2;

                                                if (!vwFrequenciaDia[0].IsHoraEntraManhaNull())
                                                    Entrada1 = vwFrequenciaDia[0].HoraEntraManha;
                                                else
                                                    Entrada1 = null;

                                                if (!vwFrequenciaDia[0].IsHoraSaidaManhaNull())
                                                    Saida1 = vwFrequenciaDia[0].HoraSaidaManha;
                                                else
                                                    Saida1 = null;

                                                if (!vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                                                    Entrada2 = vwFrequenciaDia[0].HoraEntradaTarde;
                                                else
                                                    Entrada2 = null;

                                                Saida2 = HoraBatida;

                                                if (adpFrequencia.UpdateSaidaTarde(HoraBatida, TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), IDUsuario, vwFrequenciaDia[0].IDFrequencia) == 0)
                                                {
                                                    msg = "Nao Atualizado";
                                                }
                                                else
                                                {

                                                    //adpFrequencia.UpdateTotHoras(TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);
                                                    TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date);
                                                    switch (msg)
                                                    {
                                                        case "Carga Total":
                                                            msg = String.Format("{0}, jornada finalizada.", NomeUsuario.Trim());
                                                            break;
                                                        case "Carga Incompleta":
                                                            msg = String.Format("{0}, jornada finalizada com a carga horária total incompleta.", NomeUsuario.Trim());
                                                            break;
                                                        case "Nao Atualizado":
                                                            msg = String.Format("{0}, marcação não efetivada. Repita a operação", NomeUsuario.Trim());
                                                            break;
                                                    }

                                                }
                                            }
                                            else
                                            {
                                                switch (msg)
                                                {
                                                    case "excedido":
                                                        msg = String.Format("{0}, limite para registro excedido em {1}", NomeUsuario.Trim(), Limite);
                                                        break;
                                                    case "antecipado":
                                                        msg = String.Format("{0}, permissão para realizar registro em {1}", NomeUsuario.Trim(), Limite);
                                                        break;
                                                    case "turno justificado":
                                                        msg = String.Format("{0}, turno justificado, marcação de ponto não realizada.", NomeUsuario.Trim());
                                                        break;
                                                    case "dia justificado":
                                                        msg = String.Format("{0}, Jornada justificada na data corrente.", NomeUsuario.Trim());
                                                        break;
                                                }
                                            }
                                        }
                                        //msg = "Turno matutino justificado. Favor realizar a marcação de acordo com os seus padrões de Entrada e Saída";
                                    }

                                    switch (msg)
                                    {
                                        case "Carga Total":
                                            msg = String.Format("{0}, saída 2 efetivada.", NomeUsuario.Trim());
                                            break;
                                        case "Carga Incompleta":
                                            msg = String.Format("{0}, saída 2 efetivada.", NomeUsuario.Trim());
                                            //msg = String.Format("{0}, frequência registrada com a carga horária total incompleta. Saída da Manhã.", NomeUsuario.Trim());
                                            break;
                                        case "Nao Atualizado":
                                            msg = String.Format("{0}, marcação não efetivada. Repita o processo.", NomeUsuario.Trim());
                                            break;
                                    }
                                }
                                else
                                {
                                    switch (msg)
                                    {
                                        case "excedido":
                                            msg = String.Format("{0}, limite para registro excedido em {1}", NomeUsuario.Trim(), Limite);
                                            break;
                                        case "antecipado":
                                            msg = String.Format("{0}, permissão para realizar registro em {1}", NomeUsuario.Trim(), Limite);
                                            break;
                                        case "turno justificado":
                                            msg = String.Format("{0}, turno justificado, marcação de ponto não realizada.", NomeUsuario.Trim());
                                            break;
                                        case "dia justificado":
                                            msg = String.Format("{0}, Jornada justificada na data corrente.", NomeUsuario.Trim());
                                            break;
                                    }
                                }
                            }
                            else if (!vwFrequenciaDia[0].IsHoraEntraManhaNull() || !vwFrequenciaDia[0].IsHoraSaidaManhaNull() || !vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                            {
                                if (vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                                {

                                    Limite = "";
                                    if (HoraBatida <= Convert.ToDateTime(HoraPadraoSaidTarde))
                                    {
                                        Limite = "";
                                        if (Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.ToShortDateString(), HoraPadraoSaidTarde)) > HoraBatida)
                                            Limite = Convert.ToString(Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.ToShortDateString(), HoraPadraoSaidTarde)) - HoraBatida);
                                        else
                                            Limite = Convert.ToString(HoraBatida - Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.ToShortDateString(), HoraPadraoSaidTarde)));

                                        if (!vwFrequenciaDia[0].IsHoraSaidaManhaNull()) ///Se não for nulo, faz o comparativo.
                                        {
                                            LimiteEntradaSaida = string.Empty;
                                            LimiteEntradaSaida = Convert.ToString(HoraBatida - vwFrequenciaDia[0].HoraSaidaManha);

                                            if (Convert.ToDateTime(LimiteEntradaSaida) >= Convert.ToDateTime(LimitePadraoAlmoco))
                                            {
                                                if (Convert.ToDateTime(Limite) >= Convert.ToDateTime("01:00:00.000"))
                                                {
                                                    if (PermiteRegistroLimite(HoraBatida, Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.Date.ToShortDateString(), HoraPadraoEntradaTarde)), Convert.ToDateTime(LimitePadraoEntradaSaida), vwUsuarioGrid[vinculo].AcessoEspecial))
                                                    {
                                                        //Prevendo hora de almoço superior ou igual a uma hora
                                                        if (vwFrequenciaDia[0].IsHoraSaidaManhaNull())
                                                        {
                                                            if (adpFrequencia.UpdateEntradaTarde(HoraBatida, vwFrequenciaDia[0].TotalHorasDiaSegundos, IDUsuario, HoraBatida.Date, IDVinculoUsuarioParam) > 0)
                                                                msg = string.Format("{0}, entrada 2 efetivada.", NomeUsuario);
                                                            else
                                                                msg = string.Format("{0}, marcação não efetivada. Repita a operação", NomeUsuario);
                                                        }
                                                        else
                                                        {
                                                            LimiteEntradaSaida = string.Empty;
                                                            LimiteEntradaSaida = Convert.ToString(HoraBatida - vwFrequenciaDia[0].HoraSaidaManha);

                                                            if (Convert.ToDateTime(LimiteEntradaSaida) >= Convert.ToDateTime(string.Format("{0} 01:00:00", HoraBatida.ToShortDateString())))
                                                            {
                                                                if (adpFrequencia.UpdateEntradaTarde(HoraBatida, vwFrequenciaDia[0].TotalHorasDiaSegundos, IDUsuario, HoraBatida.Date, IDVinculoUsuarioParam) > 0)
                                                                    msg = string.Format("{0}, entrada 2 efetivada.", NomeUsuario);
                                                                else
                                                                    msg = string.Format("{0}, marcação não efetivada. Repita a operação", NomeUsuario);
                                                            }
                                                            else
                                                                msg = string.Format("{0}, horário de almoço. Permissão para registro em {1}.", NomeUsuario, Convert.ToDateTime(LimiteEntradaSaida).ToShortTimeString());
                                                        }
                                                    }
                                                    else
                                                    {
                                                        switch (msg)
                                                        {
                                                            case "excedido":
                                                                msg = String.Format("{0}, limite para registro excedido em {1}", NomeUsuario.Trim(), Limite);
                                                                break;
                                                            case "antecipado":
                                                                msg = String.Format("{0}, permissão para realizar registro em {1}", NomeUsuario.Trim(), Limite);
                                                                break;
                                                            case "turno justificado":
                                                                msg = String.Format("{0}, turno justificado, marcação de ponto não realizada.", NomeUsuario.Trim());
                                                                break;
                                                            case "dia justificado":
                                                                msg = String.Format("{0}, Jornada justificada na data corrente.", NomeUsuario.Trim());
                                                                break;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (PermiteRegistroLimite(HoraBatida, Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.Date.ToShortDateString(), HoraPadraoSaidTarde)), Convert.ToDateTime(LimitePadraoEntradaSaida), vwUsuarioGrid[vinculo].AcessoEspecial))
                                                    {
                                                        //Total Horas
                                                        DateTime? Entrada1, Saida1, Entrada2, Saida2;

                                                        if (!vwFrequenciaDia[0].IsHoraEntraManhaNull())
                                                            Entrada1 = vwFrequenciaDia[0].HoraEntraManha;
                                                        else
                                                            Entrada1 = null;

                                                        if (!vwFrequenciaDia[0].IsHoraSaidaManhaNull())
                                                            Saida1 = vwFrequenciaDia[0].HoraSaidaManha;
                                                        else
                                                            Saida1 = null;

                                                        if (!vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                                                            Entrada2 = vwFrequenciaDia[0].HoraEntradaTarde;
                                                        else
                                                            Entrada2 = null;

                                                        Saida2 = HoraBatida;

                                                        if (adpFrequencia.UpdateSaidaTarde(HoraBatida, TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), IDUsuario, vwFrequenciaDia[0].IDFrequencia) == 0)
                                                        {
                                                            msg = "Nao Atualizado";
                                                        }
                                                        else
                                                        {

                                                            //adpFrequencia.UpdateTotHoras(TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);
                                                            TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date);
                                                            switch (msg)
                                                            {
                                                                case "Carga Total":
                                                                    msg = String.Format("{0}, Jornada finalizada.", NomeUsuario.Trim());
                                                                    break;
                                                                case "Carga Incompleta":
                                                                    msg = String.Format("{0}, jornada finalizada com a carga horária total incompleta.", NomeUsuario.Trim());
                                                                    break;
                                                                case "Nao Atualizado":
                                                                    msg = String.Format("{0}, marcação não efetivada. Repita a operação", NomeUsuario.Trim());
                                                                    break;
                                                            }

                                                        }
                                                    }
                                                    else
                                                    {
                                                        switch (msg)
                                                        {
                                                            case "excedido":
                                                                msg = String.Format("{0}, limite para registro excedido em {1}", NomeUsuario.Trim(), Limite);
                                                                break;
                                                            case "antecipado":
                                                                msg = String.Format("{0}, permissão para realizar registro em {1}", NomeUsuario.Trim(), Limite);
                                                                break;
                                                            case "turno justificado":
                                                                msg = String.Format("{0}, turno justificado, marcação de ponto não realizada.", NomeUsuario.Trim());
                                                                break;
                                                            case "dia justificado":
                                                                msg = String.Format("{0}, Jornada justificada na data corrente.", NomeUsuario.Trim());
                                                                break;
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                string TempoRestante = Convert.ToString(Convert.ToDateTime(LimitePadraoAlmoco) - Convert.ToDateTime(LimiteEntradaSaida));
                                                msg = string.Format("{0}, horário de almoço. Faça sua marcação em {1}", NomeUsuario.Trim(), Convert.ToDateTime(TempoRestante).ToShortTimeString());
                                            }
                                        }
                                        else
                                        {
                                            if (Convert.ToDateTime(Limite) >= Convert.ToDateTime("01:00:00.000"))
                                            {
                                                if (PermiteRegistroLimite(HoraBatida, Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.Date.ToShortDateString(), HoraPadraoEntradaTarde)), Convert.ToDateTime(LimitePadraoEntradaSaida), vwUsuarioGrid[vinculo].AcessoEspecial))
                                                {
                                                    //Prevendo hora de almoço superior ou igual a uma hora
                                                    if (vwFrequenciaDia[0].IsHoraSaidaManhaNull())
                                                    {
                                                        if (adpFrequencia.UpdateEntradaTarde(HoraBatida, vwFrequenciaDia[0].TotalHorasDiaSegundos, IDUsuario, HoraBatida.Date, IDVinculoUsuarioParam) > 0)
                                                            msg = string.Format("{0}, entrada 2 efetivada.", NomeUsuario);
                                                        else
                                                            msg = string.Format("{0}, marcação não efetivada. Repita a operação", NomeUsuario);
                                                    }
                                                    else
                                                    {
                                                        LimiteEntradaSaida = string.Empty;
                                                        LimiteEntradaSaida = Convert.ToString(HoraBatida - vwFrequenciaDia[0].HoraSaidaManha);

                                                        if (Convert.ToDateTime(LimiteEntradaSaida) >= Convert.ToDateTime(string.Format("{0} 01:00:00", HoraBatida.ToShortDateString())))
                                                        {
                                                            if (adpFrequencia.UpdateEntradaTarde(HoraBatida, vwFrequenciaDia[0].TotalHorasDiaSegundos, IDUsuario, HoraBatida.Date, IDVinculoUsuarioParam) > 0)
                                                                msg = string.Format("{0}, entrada 2 efetivada.", NomeUsuario);
                                                            else
                                                                msg = string.Format("{0}, marcação não efetivada. Repita a operação", NomeUsuario);
                                                        }
                                                        else
                                                            msg = string.Format("{0}, horário de almoço. Permissão para registro em {1}.", NomeUsuario, Convert.ToDateTime(LimiteEntradaSaida).ToShortTimeString());
                                                    }
                                                }
                                                else
                                                {
                                                    switch (msg)
                                                    {
                                                        case "excedido":
                                                            msg = String.Format("{0}, limite para registro excedido em {1}", NomeUsuario.Trim(), Limite);
                                                            break;
                                                        case "antecipado":
                                                            msg = String.Format("{0}, permissão para realizar registro em {1}", NomeUsuario.Trim(), Limite);
                                                            break;
                                                        case "turno justificado":
                                                            msg = String.Format("{0}, turno justificado, marcação de ponto não realizada.", NomeUsuario.Trim());
                                                            break;
                                                        case "dia justificado":
                                                            msg = String.Format("{0}, Jornada justificada na data corrente.", NomeUsuario.Trim());
                                                            break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else //if (TBFrequencia[0].IsHoraSaidaTardeNull()) -- retirado ... Poder bater novamente a saida da tarde
                                    {

                                        if (!vwFrequenciaDia[0].IsHoraEntradaTardeNull())//Define a diferença entre entrada e saída tarde
                                        {
                                            LimiteEntradaSaida = string.Empty;
                                            LimiteEntradaSaida = Convert.ToString(HoraBatida - vwFrequenciaDia[0].HoraEntradaTarde);

                                            if (Convert.ToDateTime(LimiteEntradaSaida) >= Convert.ToDateTime(LimitePadraoAlmoco))
                                            {
                                                if (PermiteRegistroLimite(HoraBatida, Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.Date.ToShortDateString(), HoraPadraoSaidTarde)), Convert.ToDateTime(LimitePadraoEntradaSaida), vwUsuarioGrid[vinculo].AcessoEspecial))
                                                {
                                                    //Total Horas
                                                    DateTime? Entrada1, Saida1, Entrada2, Saida2;

                                                    if (!vwFrequenciaDia[0].IsHoraEntraManhaNull())
                                                        Entrada1 = vwFrequenciaDia[0].HoraEntraManha;
                                                    else
                                                        Entrada1 = null;

                                                    if (!vwFrequenciaDia[0].IsHoraSaidaManhaNull())
                                                        Saida1 = vwFrequenciaDia[0].HoraSaidaManha;
                                                    else
                                                        Saida1 = null;

                                                    if (!vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                                                        Entrada2 = vwFrequenciaDia[0].HoraEntradaTarde;
                                                    else
                                                        Entrada2 = null;

                                                    Saida2 = HoraBatida;

                                                    if (adpFrequencia.UpdateSaidaTarde(HoraBatida, TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), IDUsuario, vwFrequenciaDia[0].IDFrequencia) == 0)
                                                    {
                                                        msg = "Nao Atualizado";
                                                    }
                                                    else
                                                    {

                                                        //adpFrequencia.UpdateTotHoras(TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);
                                                        TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date);
                                                        switch (msg)
                                                        {
                                                            case "Carga Total":
                                                                msg = String.Format("{0}, entrada 2 efetivada.", NomeUsuario.Trim());
                                                                break;
                                                            case "Carga Incompleta":
                                                                msg = String.Format("{0}, entrada 2 efetivada com a carga horária total incompleta.", NomeUsuario.Trim());
                                                                break;
                                                            case "Nao Atualizado":
                                                                msg = String.Format("{0}, marcação não efetivada. Repita a operação", NomeUsuario.Trim());
                                                                break;
                                                        }

                                                    }
                                                }
                                                else
                                                {
                                                    switch (msg)
                                                    {
                                                        case "excedido":
                                                            msg = String.Format("{0}, limite para registro excedido em {1}", NomeUsuario.Trim(), Limite);
                                                            break;
                                                        case "antecipado":
                                                            msg = String.Format("{0}, permissão para realizar registro em {1}", NomeUsuario.Trim(), Limite);
                                                            break;
                                                        case "turno justificado":
                                                            msg = String.Format("{0}, turno justificado, marcação de ponto não realizada.", NomeUsuario.Trim());
                                                            break;
                                                        case "dia justificado":
                                                            msg = String.Format("{0}, Jornada justificada na data corrente.", NomeUsuario.Trim());
                                                            break;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                string TempoRestante = Convert.ToString(Convert.ToDateTime(LimitePadraoAlmoco) - Convert.ToDateTime(LimiteEntradaSaida));
                                                msg = string.Format("{0}, horário de almoço. Faça sua marcação em {1}", NomeUsuario.Trim(), Convert.ToDateTime(TempoRestante).ToShortTimeString());
                                            }
                                        }
                                        else
                                        {
                                            if (PermiteRegistroLimite(HoraBatida, Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.Date.ToShortDateString(), HoraPadraoSaidTarde)), Convert.ToDateTime(LimitePadraoEntradaSaida), vwUsuarioGrid[vinculo].AcessoEspecial))
                                            {
                                                //Total Horas
                                                DateTime? Entrada1, Saida1, Entrada2, Saida2;

                                                if (!vwFrequenciaDia[0].IsHoraEntraManhaNull())
                                                    Entrada1 = vwFrequenciaDia[0].HoraEntraManha;
                                                else
                                                    Entrada1 = null;

                                                if (!vwFrequenciaDia[0].IsHoraSaidaManhaNull())
                                                    Saida1 = vwFrequenciaDia[0].HoraSaidaManha;
                                                else
                                                    Saida1 = null;

                                                if (!vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                                                    Entrada2 = vwFrequenciaDia[0].HoraEntradaTarde;
                                                else
                                                    Entrada2 = null;

                                                Saida2 = HoraBatida;

                                                if (adpFrequencia.UpdateSaidaTarde(HoraBatida, TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), IDUsuario, vwFrequenciaDia[0].IDFrequencia) == 0)
                                                {
                                                    msg = "Nao Atualizado";
                                                }
                                                else
                                                {

                                                    //adpFrequencia.UpdateTotHoras(TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);
                                                    TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date);
                                                    switch (msg)
                                                    {
                                                        case "Carga Total":
                                                            msg = String.Format("{0}, jornada finalizada.", NomeUsuario.Trim());
                                                            break;
                                                        case "Carga Incompleta":
                                                            msg = String.Format("{0}, jornada finalizada com a carga horária total incompleta.", NomeUsuario.Trim());
                                                            break;
                                                        case "Nao Atualizado":
                                                            msg = String.Format("{0}, marcação não efetivada. Repita a operação", NomeUsuario.Trim());
                                                            break;
                                                    }

                                                }
                                            }
                                            else
                                            {
                                                switch (msg)
                                                {
                                                    case "excedido":
                                                        msg = String.Format("{0}, limite para registro excedido em {1}", NomeUsuario.Trim(), Limite);
                                                        break;
                                                    case "antecipado":
                                                        msg = String.Format("{0}, permissão para realizar registro em {1}", NomeUsuario.Trim(), Limite);
                                                        break;
                                                    case "turno justificado":
                                                        msg = String.Format("{0}, turno justificado, marcação de ponto não realizada.", NomeUsuario.Trim());
                                                        break;
                                                    case "dia justificado":
                                                        msg = String.Format("{0}, Jornada justificada na data corrente.", NomeUsuario.Trim());
                                                        break;
                                                }
                                            }
                                        }
                                    }
                                    //else -- retirado ... poder realizar a saída da tarde.
                                    //{
                                    //    msg = string.Format("{0}, jornada completa para o período.", NomeUsuario);
                                    //}
                                }
                                else //if (TBFrequencia[0].IsHoraSaidaTardeNull()) -- Retirado ... poder realizar a saída da tarde mais de uma vez.
                                {

                                    if (!vwFrequenciaDia[0].IsHoraEntradaTardeNull())//Define a diferença entre entrada e saída tarde
                                    {
                                        LimiteEntradaSaida = string.Empty;
                                        LimiteEntradaSaida = Convert.ToString(HoraBatida - vwFrequenciaDia[0].HoraEntradaTarde);

                                        if (Convert.ToDateTime(LimiteEntradaSaida) >= Convert.ToDateTime("00:10:00.000"))
                                        {
                                            if (PermiteRegistroLimite(HoraBatida, Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.Date.ToShortDateString(), HoraPadraoSaidTarde)), Convert.ToDateTime(LimitePadraoEntradaSaida), vwUsuarioGrid[vinculo].AcessoEspecial))
                                            {
                                                //Total Horas
                                                DateTime? Entrada1, Saida1, Entrada2, Saida2;

                                                if (!vwFrequenciaDia[0].IsHoraEntraManhaNull())
                                                    Entrada1 = vwFrequenciaDia[0].HoraEntraManha;
                                                else
                                                    Entrada1 = null;

                                                if (!vwFrequenciaDia[0].IsHoraSaidaManhaNull())
                                                    Saida1 = vwFrequenciaDia[0].HoraSaidaManha;
                                                else
                                                    Saida1 = null;

                                                if (!vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                                                    Entrada2 = vwFrequenciaDia[0].HoraEntradaTarde;
                                                else
                                                    Entrada2 = null;

                                                Saida2 = HoraBatida;

                                                if (adpFrequencia.UpdateSaidaTarde(HoraBatida, TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), IDUsuario, vwFrequenciaDia[0].IDFrequencia) == 0)
                                                {
                                                    msg = "Nao Atualizado";
                                                }
                                                else
                                                {

                                                    //adpFrequencia.UpdateTotHoras(TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);
                                                    TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date);
                                                    switch (msg)
                                                    {
                                                        case "Carga Total":
                                                            msg = String.Format("{0}, jornada finalizada.", NomeUsuario.Trim());
                                                            break;
                                                        case "Carga Incompleta":
                                                            msg = String.Format("{0}, jornada finalizada com a carga horária total incompleta.", NomeUsuario.Trim());
                                                            break;
                                                        case "Nao Atualizado":
                                                            msg = String.Format("{0}, marcação não efetivada. Repita a operação", NomeUsuario.Trim());
                                                            break;
                                                    }

                                                }
                                            }
                                            else
                                            {
                                                switch (msg)
                                                {
                                                    case "excedido":
                                                        msg = String.Format("{0}, limite para registro excedido em {1}", NomeUsuario.Trim(), Limite);
                                                        break;
                                                    case "antecipado":
                                                        msg = String.Format("{0}, permissão para realizar registro em {1}", NomeUsuario.Trim(), Limite);
                                                        break;
                                                    case "turno justificado":
                                                        msg = String.Format("{0}, turno justificado, marcação de ponto não realizada.", NomeUsuario.Trim());
                                                        break;
                                                    case "dia justificado":
                                                        msg = String.Format("{0}, Jornada justificada na data corrente.", NomeUsuario.Trim());
                                                        break;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            msg = string.Format("{0}, limite de dez minutos entre as marcações.", NomeUsuario.Trim());
                                        }
                                    }
                                    else
                                    {
                                        if (PermiteRegistroLimite(HoraBatida, Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.Date.ToShortDateString(), HoraPadraoSaidTarde)), Convert.ToDateTime(LimitePadraoEntradaSaida), vwUsuarioGrid[vinculo].AcessoEspecial))
                                        {
                                            //Total Horas
                                            DateTime? Entrada1, Saida1, Entrada2, Saida2;

                                            if (!vwFrequenciaDia[0].IsHoraEntraManhaNull())
                                                Entrada1 = vwFrequenciaDia[0].HoraEntraManha;
                                            else
                                                Entrada1 = null;

                                            if (!vwFrequenciaDia[0].IsHoraSaidaManhaNull())
                                                Saida1 = vwFrequenciaDia[0].HoraSaidaManha;
                                            else
                                                Saida1 = null;

                                            if (!vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                                                Entrada2 = vwFrequenciaDia[0].HoraEntradaTarde;
                                            else
                                                Entrada2 = null;

                                            Saida2 = HoraBatida;

                                            if (adpFrequencia.UpdateSaidaTarde(HoraBatida, TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), IDUsuario, vwFrequenciaDia[0].IDFrequencia) == 0)
                                            {
                                                msg = "Nao Atualizado";
                                            }
                                            else
                                            {


                                                //adpFrequencia.UpdateTotHoras(TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);
                                                TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date);
                                            }

                                            switch (msg)
                                            {
                                                case "Carga Total":
                                                    msg = String.Format("{0}, jornada finalizada", NomeUsuario.Trim());
                                                    break;
                                                case "Carga Incompleta":
                                                    msg = String.Format("{0}, jornada finalizada com a carga horária total incompleta.", NomeUsuario.Trim());
                                                    break;
                                                case "Nao Atualizado":
                                                    msg = String.Format("{0}, marcação não efetivada. Repita a operação", NomeUsuario.Trim());
                                                    break;
                                            }
                                        }
                                        else
                                        {
                                            switch (msg)
                                            {
                                                case "excedido":
                                                    msg = String.Format("{0}, limite para registro excedido em {1}", NomeUsuario.Trim(), Limite);
                                                    break;
                                                case "antecipado":
                                                    msg = String.Format("{0}, permissão para realizar registro em {1}", NomeUsuario.Trim(), Limite);
                                                    break;
                                                case "turno justificado":
                                                    msg = String.Format("{0}, turno justificado, marcação de ponto não realizada.", NomeUsuario.Trim());
                                                    break;
                                                case "dia justificado":
                                                    msg = String.Format("{0}, Jornada justificada na data corrente.", NomeUsuario.Trim());
                                                    break;
                                            }
                                        }
                                    }
                                }
                                //else //Retirado .. poder realizar a saída da tarde mais de uma vez
                                //{
                                //msg = string.Format("{0}, jornada completa para o período.", NomeUsuario);
                                //}
                            }
                            else if (vwFrequenciaDia[0].IsHoraEntraManhaNull() && vwFrequenciaDia[0].IsHoraSaidaManhaNull() && HoraBatida >= Convert.ToDateTime(string.Format("{0} 12:00:00.000", HoraBatida.ToShortDateString())) && HoraBatida < Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.ToShortDateString(), vwUsuarioGrid[vinculo].HoraSaidaTarde)).AddHours(4) && vwFrequenciaDia[0].IsHoraSaidaTardeNull()) //Se justificado e não batido pela manhã
                            {
                                //Entrada da tarde direto.
                                if (adpFrequencia.UpdateEntradaTarde(HoraBatida, null, IDUsuario, HoraBatida.Date, IDVinculoUsuarioParam) > 0)
                                    msg = string.Format("{0}, entrada 2 efetivada.", NomeUsuario);
                                else
                                    msg = string.Format("{0}, marcação não efetivada. Repita a operação", NomeUsuario);
                            }
                            else if (vwFrequenciaDia[0].IsHoraEntraManhaNull() && vwFrequenciaDia[0].IsHoraSaidaManhaNull() && HoraBatida >= Convert.ToDateTime(string.Format("{0} 12:00:00.000", HoraBatida.ToShortDateString())) && HoraBatida >= Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.ToShortDateString(), vwUsuarioGrid[vinculo].HoraSaidaTarde)).AddHours(4)) //Se maior, registro da saída da tarde.
                            {
                                //Total Horas
                                DateTime? Entrada1, Saida1, Entrada2, Saida2;

                                if (!vwFrequenciaDia[0].IsHoraEntraManhaNull())
                                    Entrada1 = vwFrequenciaDia[0].HoraEntraManha;
                                else
                                    Entrada1 = null;

                                if (!vwFrequenciaDia[0].IsHoraSaidaManhaNull())
                                    Saida1 = vwFrequenciaDia[0].HoraSaidaManha;
                                else
                                    Saida1 = null;

                                if (!vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                                    Entrada2 = vwFrequenciaDia[0].HoraEntradaTarde;
                                else
                                    Entrada2 = null;

                                Saida2 = HoraBatida;

                                if (adpFrequencia.UpdateSaidaTarde(HoraBatida, TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), IDUsuario, vwFrequenciaDia[0].IDFrequencia) == 0)
                                {
                                    msg = "Nao Atualizado";
                                }
                                else
                                {
                                    //adpFrequencia.UpdateTotHoras(TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);
                                    TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date);
                                }

                                switch (msg)
                                {
                                    case "Carga Total":
                                        msg = String.Format("{0}, jornada finalizada.", NomeUsuario.Trim());
                                        break;
                                    case "Carga Incompleta":
                                        msg = String.Format("{0}, jornada finalizada com a carga horária total incompleta.", NomeUsuario.Trim());
                                        break;
                                    case "Nao Atualizado":
                                        msg = String.Format("{0}, marcação não efetivada. Repita a operação", NomeUsuario.Trim());
                                        break;
                                }
                            }
                            else if (!vwFrequenciaDia[0].IsHoraSaidaTardeNull())
                            {
                                //Total Horas
                                DateTime? Entrada1, Saida1, Entrada2, Saida2;

                                if (!vwFrequenciaDia[0].IsHoraEntraManhaNull())
                                    Entrada1 = vwFrequenciaDia[0].HoraEntraManha;
                                else
                                    Entrada1 = null;

                                if (!vwFrequenciaDia[0].IsHoraSaidaManhaNull())
                                    Saida1 = vwFrequenciaDia[0].HoraSaidaManha;
                                else
                                    Saida1 = null;

                                if (!vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                                    Entrada2 = vwFrequenciaDia[0].HoraEntradaTarde;
                                else
                                    Entrada2 = null;

                                Saida2 = HoraBatida;
                                if (adpFrequencia.UpdateSaidaTarde(HoraBatida, TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), IDUsuario, vwFrequenciaDia[0].IDFrequencia) == 0)
                                {
                                    msg = "Nao Atualizado";
                                }
                                else
                                {

                                    //adpFrequencia.UpdateTotHoras(TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);
                                    TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date);
                                }

                                switch (msg)
                                {
                                    case "Carga Total":
                                        msg = String.Format("{0}, jornada finalizada", NomeUsuario.Trim());
                                        break;
                                    case "Carga Incompleta":
                                        msg = String.Format("{0}, jornada finalizada com a carga horária total incompleta.", NomeUsuario.Trim());
                                        break;
                                    case "Nao Atualizado":
                                        msg = String.Format("{0}, marcação não efetivada. Repita a operação", NomeUsuario.Trim());
                                        break;
                                }
                            }

                        } // Até aqui pessoas que trabalham 8 horas por dia
                        #endregion
                        else //Pessoas que trabalham 6 horas diretas ou menos ou mais
                        {
                            if (!vwFrequenciaDia[0].IsHoraEntraManhaNull() && vwFrequenciaDia[0].IsHoraSaidaManhaNull())
                            {
                                if (vwFrequenciaDia[0].HoraEntraManha != HoraBatida)
                                {
                                    LimiteEntradaSaida = string.Empty;
                                    LimiteEntradaSaida = Convert.ToString(HoraBatida - vwFrequenciaDia[0].HoraEntraManha);

                                    if (Convert.ToDateTime(LimiteEntradaSaida) > Convert.ToDateTime("00:10:00.000"))
                                    {
                                        if (PermiteRegistroLimite(HoraBatida, Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.Date.ToShortDateString(), HoraPadraoSaidaManha)), Convert.ToDateTime(LimitePadraoEntradaSaida), vwUsuarioGrid[vinculo].AcessoEspecial))
                                        {
                                            //    //Total Horas
                                            DateTime? Entrada1, Saida1, Entrada2, Saida2;

                                            if (!vwFrequenciaDia[0].IsHoraEntraManhaNull())
                                                Entrada1 = vwFrequenciaDia[0].HoraEntraManha;
                                            else
                                                Entrada1 = null;

                                            if (!vwFrequenciaDia[0].IsHoraSaidaManhaNull())
                                                Saida1 = vwFrequenciaDia[0].HoraSaidaManha;
                                            else
                                                Saida1 = HoraBatida;


                                            Entrada2 = null;

                                            Saida2 = null;
                                            //Se retorno = 0 mensagem de erro para repetir operação.. Senão, apresentar msg de erro.
                                            if (adpFrequencia.UpdateSaidaManha(HoraBatida, TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Manha", HoraBatida.Date.Date), IDUsuario, HoraBatida.Date, IDVinculoUsuarioParam) == 0)
                                            {
                                                msg = "Nao Atualizado";
                                            }
                                            else
                                            {


                                                //adpFrequencia.UpdateTotHoras(TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Manha", HoraBatida.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);

                                                TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Manha", HoraBatida.Date.Date);
                                            }


                                            switch (msg)
                                            {
                                                case "Carga Total":
                                                    msg = String.Format("{0}, jornada finalizada.", NomeUsuario.Trim());
                                                    break;
                                                case "Carga Incompleta":
                                                    msg = String.Format("{0}, jornada finalizada com a carga horária total incompleta.", NomeUsuario.Trim());
                                                    break;
                                                case "Nao Atualizado":
                                                    msg = String.Format("{0}, marcação não efetivada. Repita o processo.", NomeUsuario.Trim());
                                                    break;
                                            }
                                        }
                                        else
                                        {
                                            switch (msg)
                                            {
                                                case "excedido":
                                                    msg = String.Format("{0}, limite para registro excedido em {1}", NomeUsuario.Trim(), Limite);
                                                    break;
                                                case "antecipado":
                                                    msg = String.Format("{0}, permissão para realizar registro em {1}", NomeUsuario.Trim(), Limite);
                                                    break;
                                                case "turno justificado":
                                                    msg = String.Format("{0}, turno justificado, marcação de ponto não realizada.", NomeUsuario.Trim());
                                                    break;
                                                case "dia justificado":
                                                    msg = String.Format("{0}, Jornada justificada na data corrente.", NomeUsuario.Trim());
                                                    break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        msg = string.Format("{0}, respeite o intervalo de 10 minutos entre as marcações.", NomeUsuario);
                                    }
                                }
                            }
                            else if (!vwFrequenciaDia[0].IsHoraEntraManhaNull() && !vwFrequenciaDia[0].IsHoraSaidaManhaNull() && vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                            {
                                if (vwFrequenciaDia[0].HoraSaidaManha != HoraBatida)
                                {
                                    if (PermiteRegistroLimite(HoraBatida, Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.Date.ToShortDateString(), HoraPadraoEntradaTarde)), Convert.ToDateTime(LimitePadraoEntradaSaida), vwUsuarioGrid[vinculo].AcessoEspecial))
                                    {
                                        //Prevendo hora de almoço superior ou igual a uma hora
                                        if (vwFrequenciaDia[0].IsHoraSaidaManhaNull())
                                        {
                                            if (adpFrequencia.UpdateEntradaTarde(HoraBatida, vwFrequenciaDia[0].TotalHorasDiaSegundos, IDUsuario, HoraBatida.Date, IDVinculoUsuarioParam) > 0)
                                                msg = string.Format("{0}, entrada 2 efetivada.", NomeUsuario);
                                            else
                                                msg = string.Format("{0}, marcação não efetivada. Repita a operação", NomeUsuario);
                                        }
                                        else
                                        {
                                            LimiteEntradaSaida = string.Empty;
                                            LimiteEntradaSaida = Convert.ToString(HoraBatida - vwFrequenciaDia[0].HoraSaidaManha);

                                            if (Convert.ToDateTime(LimiteEntradaSaida) >= Convert.ToDateTime(string.Format("{0} 01:00:00", HoraBatida.ToShortDateString())))
                                            {
                                                if (adpFrequencia.UpdateEntradaTarde(HoraBatida, vwFrequenciaDia[0].TotalHorasDiaSegundos, IDUsuario, HoraBatida.Date, IDVinculoUsuarioParam) > 0)
                                                    msg = string.Format("{0}, entrada 2 não efetivada.", NomeUsuario);
                                                else
                                                    msg = string.Format("{0}, marcação não efetivada. Repita a operação", NomeUsuario);
                                            }
                                            else
                                                msg = string.Format("{0}, horário de almoço. Permissão para registro em {1}.", NomeUsuario, Convert.ToDateTime(LimiteEntradaSaida).ToShortTimeString());
                                        }
                                    }
                                    else
                                    {
                                        switch (msg)
                                        {
                                            case "excedido":
                                                msg = String.Format("{0}, limite para registro excedido em {1}", NomeUsuario.Trim(), Limite);
                                                break;
                                            case "antecipado":
                                                msg = String.Format("{0}, permissão para realizar registro em {1}", NomeUsuario.Trim(), Limite);
                                                break;
                                            case "turno justificado":
                                                msg = String.Format("{0}, turno justificado, marcação de ponto não realizada.", NomeUsuario.Trim());
                                                break;
                                            case "dia justificado":
                                                msg = String.Format("{0}, Jornada justificada na data corrente.", NomeUsuario.Trim());
                                                break;
                                        }
                                    }
                                }
                            }
                            else if (!vwFrequenciaDia[0].IsHoraEntraManhaNull() && !vwFrequenciaDia[0].IsHoraSaidaManhaNull() && !vwFrequenciaDia[0].IsHoraEntradaTardeNull() && vwFrequenciaDia[0].IsHoraSaidaTardeNull())
                            {
                                if (vwFrequenciaDia[0].HoraEntradaTarde != HoraBatida)
                                {
                                    if (PermiteRegistroLimite(HoraBatida, Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.Date.ToShortDateString(), HoraPadraoSaidTarde)), Convert.ToDateTime(LimitePadraoEntradaSaida), vwUsuarioGrid[vinculo].AcessoEspecial))
                                    {
                                        //Total Horas
                                        DateTime? Entrada1, Saida1, Entrada2, Saida2;

                                        if (!vwFrequenciaDia[0].IsHoraEntraManhaNull())
                                            Entrada1 = vwFrequenciaDia[0].HoraEntraManha;
                                        else
                                            Entrada1 = null;

                                        if (!vwFrequenciaDia[0].IsHoraSaidaManhaNull())
                                            Saida1 = vwFrequenciaDia[0].HoraSaidaManha;
                                        else
                                            Saida1 = null;

                                        if (!vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                                            Entrada2 = vwFrequenciaDia[0].HoraEntradaTarde;
                                        else
                                            Entrada2 = null;

                                        Saida2 = HoraBatida;

                                        if (adpFrequencia.UpdateSaidaTarde(HoraBatida, TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), IDUsuario, vwFrequenciaDia[0].IDFrequencia) == 0)
                                        {
                                            msg = "Nao Atualizado";
                                        }
                                        else
                                        {

                                            //adpFrequencia.UpdateTotHoras(TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);
                                            TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date);

                                            switch (msg)
                                            {
                                                case "Carga Total":
                                                    msg = String.Format("{0}, Jornada finalizada.", NomeUsuario.Trim());
                                                    break;
                                                case "Carga Incompleta":
                                                    msg = String.Format("{0}, jornada finalizada com a carga horária total incompleta.", NomeUsuario.Trim());
                                                    break;
                                                case "Nao Atualizado":
                                                    msg = String.Format("{0}, marcação não efetivada. Repita a operação", NomeUsuario.Trim());
                                                    break;
                                            }

                                        }

                                    }
                                    else
                                    {
                                        switch (msg)
                                        {
                                            case "excedido":
                                                msg = String.Format("{0}, limite para registro excedido em {1}", NomeUsuario.Trim(), Limite);
                                                break;
                                            case "antecipado":
                                                msg = String.Format("{0}, permissão para realizar registro em {1}", NomeUsuario.Trim(), Limite);
                                                break;
                                            case "turno justificado":
                                                msg = String.Format("{0}, turno justificado, marcação de ponto não realizada.", NomeUsuario.Trim());
                                                break;
                                            case "dia justificado":
                                                msg = String.Format("{0}, Jornada justificada na data corrente.", NomeUsuario.Trim());
                                                break;
                                        }
                                    }
                                }

                                //if (!vwFrequenciaDia[0].IsHoraEntraManhaNull() && !vwFrequenciaDia[0].IsHoraSaidaManhaNull() && !vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                                //{
                                //    //Total Horas
                                //    DateTime? Entrada1, Saida1, Entrada2, Saida2;

                                //    if (!vwFrequenciaDia[0].IsHoraEntraManhaNull())
                                //        Entrada1 = vwFrequenciaDia[0].HoraEntraManha;
                                //    else
                                //        Entrada1 = null;

                                //    if (!vwFrequenciaDia[0].IsHoraSaidaManhaNull())
                                //        Saida1 = vwFrequenciaDia[0].HoraSaidaManha;
                                //    else
                                //        Saida1 = null;

                                //    if (!vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                                //        Entrada2 = vwFrequenciaDia[0].HoraEntradaTarde;
                                //    else
                                //        Entrada2 = null;

                                //    Saida2 = HoraBatida;

                                //    adpFrequencia.UpdateTotHoras(TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);

                                //    switch (msg)
                                //    {
                                //        case "Carga Total":
                                //            msg = String.Format("{0}, frequência registrada com sucesso. Saída da Tarde.", NomeUsuario.Trim());
                                //            break;
                                //        case "Carga Incompleta":
                                //            msg = String.Format("{0}, frequência registrada com a carga horária total incompleta. Saída da Tarde.", NomeUsuario.Trim());
                                //            break;
                                //    }

                                //}
                                //else
                                //{
                                //    //Total Horas
                                //    DateTime? Entrada1, Saida1, Entrada2, Saida2;

                                //    if (!vwFrequenciaDia[0].IsHoraEntraManhaNull())
                                //        Entrada1 = vwFrequenciaDia[0].HoraEntraManha;
                                //    else
                                //        Entrada1 = null;

                                //    if (!vwFrequenciaDia[0].IsHoraSaidaManhaNull())
                                //        Saida1 = vwFrequenciaDia[0].HoraSaidaManha;
                                //    else
                                //        Saida1 = null;

                                //    if (!vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                                //        Entrada2 = vwFrequenciaDia[0].HoraEntradaTarde;
                                //    else
                                //        Entrada2 = null;

                                //    Saida2 = HoraBatida;

                                //    adpFrequencia.UpdateTotHoras(TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);

                                //    switch (msg)
                                //    {
                                //        case "Carga Total":
                                //            msg = String.Format("{0}, frequência registrada com sucesso. Saída da Tarde.", NomeUsuario.Trim());
                                //            break;
                                //        case "Carga Incompleta":
                                //            msg = String.Format("{0}, frequência registrada com a carga horária total incompleta. Saída da Tarde.", NomeUsuario.Trim());
                                //            break;
                                //    }
                                //}
                            }
                            else if (vwFrequenciaDia[0].IsHoraEntraManhaNull() && vwFrequenciaDia[0].IsHoraSaidaManhaNull() && !vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                            {
                                LimiteEntradaSaida = string.Empty;
                                LimiteEntradaSaida = Convert.ToString(HoraBatida - vwFrequenciaDia[0].HoraEntradaTarde);

                                if (Convert.ToDateTime(LimiteEntradaSaida) > Convert.ToDateTime("00:10:00.000"))
                                {
                                    if (PermiteRegistroLimite(HoraBatida, Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.Date.ToShortDateString(), HoraPadraoSaidTarde)), Convert.ToDateTime(LimitePadraoEntradaSaida), vwUsuarioGrid[vinculo].AcessoEspecial))
                                    {
                                        //Total Horas
                                        DateTime? Entrada1, Saida1, Entrada2, Saida2;

                                        if (!vwFrequenciaDia[0].IsHoraEntraManhaNull())
                                            Entrada1 = vwFrequenciaDia[0].HoraEntraManha;
                                        else
                                            Entrada1 = null;

                                        if (!vwFrequenciaDia[0].IsHoraSaidaManhaNull())
                                            Saida1 = vwFrequenciaDia[0].HoraSaidaManha;
                                        else
                                            Saida1 = null;

                                        if (!vwFrequenciaDia[0].IsHoraEntradaTardeNull())
                                            Entrada2 = vwFrequenciaDia[0].HoraEntradaTarde;
                                        else
                                            Entrada2 = null;

                                        Saida2 = HoraBatida;

                                        if (adpFrequencia.UpdateSaidaTarde(HoraBatida, TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), IDUsuario, vwFrequenciaDia[0].IDFrequencia) == 0)
                                        {
                                            msg = "Nao Atualizado";
                                        }
                                        else
                                        {

                                            //adpFrequencia.UpdateTotHoras(TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date), OBS, IDmotivoFalta, IDUsuario, vwFrequenciaDia[0].IDFrequencia);
                                            TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", HoraBatida.Date.Date);
                                            switch (msg)
                                            {
                                                case "Carga Total":
                                                    msg = String.Format("{0}, jornada finalizada.", NomeUsuario.Trim());
                                                    break;
                                                case "Carga Incompleta":
                                                    msg = String.Format("{0}, jornada finalizada com a carga horária total incompleta.", NomeUsuario.Trim());
                                                    break;
                                                case "Nao Atualizado":
                                                    msg = String.Format("{0}, marcação não efetivada. Repita a operação", NomeUsuario.Trim());
                                                    break;
                                            }

                                        }
                                    }
                                    else
                                    {
                                        switch (msg)
                                        {
                                            case "excedido":
                                                msg = String.Format("{0}, limite para registro excedido em {1}", NomeUsuario.Trim(), Limite);
                                                break;
                                            case "antecipado":
                                                msg = String.Format("{0}, permissão para realizar registro em {1}", NomeUsuario.Trim(), Limite);
                                                break;
                                            case "turno justificado":
                                                msg = String.Format("{0}, turno justificado, marcação de ponto não realizada.", NomeUsuario.Trim());
                                                break;
                                            case "dia justificado":
                                                msg = String.Format("{0}, Jornada justificada na data corrente.", NomeUsuario.Trim());
                                                break;
                                        }
                                    }
                                }
                                else
                                {
                                    msg = string.Format("{0}, respeite o intervalo de 10 minutos entre as marcações.", NomeUsuario);
                                }
                            }
                            //Para períodos justificados -- Liberar batida exceto atraso superior a uma hora.
                            else if (vwFrequenciaDia[0].IsHoraEntraManhaNull() && vwFrequenciaDia[0].IsHoraSaidaManhaNull() && vwFrequenciaDia[0].IsHoraEntradaTardeNull() && vwFrequenciaDia[0].IsHoraSaidaTardeNull() && !vwFrequenciaDia[0].IsIDMotivoFaltaNull())
                            {
                                if (HoraBatida <= Convert.ToDateTime(string.Format("{0} 11:00:00.000", HoraBatida.Date.ToShortDateString())))
                                {
                                    if (PermiteRegistroLimite(HoraBatida, Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.Date.ToShortDateString(), HoraPadraoEntradaManha)), Convert.ToDateTime(LimitePadraoEntradaSaida), vwUsuarioGrid[vinculo].AcessoEspecial))
                                    {
                                        if (adpFrequencia.UpdateEntradaManhaJustificada(HoraBatida, vwFrequenciaDia[0].OBS, vwFrequenciaDia[0].IDMotivoFalta, vwFrequenciaDia[0].IDFrequencia, IDUsuario) > 0)
                                            msg = string.Format("{0}, entrada 1 efetivada.", NomeUsuario);
                                        else
                                            msg = string.Format("{0}, marcação não efetivada. Tente novamente.", NomeUsuario);
                                    }
                                    else
                                    {
                                        switch (msg)
                                        {
                                            case "excedido":
                                                msg = String.Format("{0}, limite para registro excedido em {1}", NomeUsuario.Trim(), Limite);
                                                break;
                                            case "antecipado":
                                                msg = String.Format("{0}, permissão para realizar registro em {1}", NomeUsuario.Trim(), Limite);
                                                break;
                                            case "turno justificado":
                                                msg = String.Format("{0}, turno justificado, marcação de ponto não realizada.", NomeUsuario.Trim());
                                                break;
                                            case "dia justificado":
                                                msg = String.Format("{0}, Jornada justificada na data corrente.", NomeUsuario.Trim());
                                                break;
                                        }
                                    }

                                }
                                else if (vwFrequenciaDia[0].IsHoraEntraManhaNull() && vwFrequenciaDia[0].IsHoraEntraManhaNull())
                                {
                                    if (PermiteRegistroLimite(HoraBatida, Convert.ToDateTime(string.Format("{0} {1}", HoraBatida.Date.ToShortDateString(), HoraPadraoEntradaTarde)), Convert.ToDateTime(LimitePadraoEntradaSaida), vwUsuarioGrid[vinculo].AcessoEspecial))
                                    {
                                        //Prevendo hora de almoço superior ou igual a uma hora
                                        if (vwFrequenciaDia[0].IsHoraSaidaManhaNull())
                                        {
                                            if (adpFrequencia.UpdateEntradaTarde(HoraBatida, vwFrequenciaDia[0].TotalHorasDiaSegundos, IDUsuario, HoraBatida.Date, IDVinculoUsuarioParam) > 0)
                                                msg = string.Format("{0}, entrada 2 efetivada.", NomeUsuario);
                                            else
                                                msg = string.Format("{0}, marcação não efetivada. Repita a operação", NomeUsuario);
                                        }
                                        else
                                        {
                                            LimiteEntradaSaida = string.Empty;
                                            LimiteEntradaSaida = Convert.ToString(HoraBatida - vwFrequenciaDia[0].HoraSaidaManha);

                                            if (Convert.ToDateTime(LimiteEntradaSaida) >= Convert.ToDateTime(string.Format("{0} 01:00:00", HoraBatida.ToShortDateString())))
                                            {
                                                if (adpFrequencia.UpdateEntradaTarde(HoraBatida, vwFrequenciaDia[0].TotalHorasDiaSegundos, IDUsuario, HoraBatida.Date, IDVinculoUsuarioParam) > 0)
                                                    msg = string.Format("{0}, entrada 2 efetivada.", NomeUsuario);
                                                else
                                                    msg = string.Format("{0}, marcação não efetivada. Repita a operação", NomeUsuario);
                                            }
                                            else
                                                msg = string.Format("{0}, horário de almoço. Permissão para registro em {1}.", NomeUsuario, Convert.ToDateTime(LimiteEntradaSaida).ToShortTimeString());
                                        }
                                    }
                                    else
                                    {
                                        switch (msg)
                                        {
                                            case "excedido":
                                                msg = String.Format("{0}, limite para registro excedido em {1}", NomeUsuario.Trim(), Limite);
                                                break;
                                            case "antecipado":
                                                msg = String.Format("{0}, permissão para realizar registro em {1}", NomeUsuario.Trim(), Limite);
                                                break;
                                            case "turno justificado":
                                                msg = String.Format("{0}, turno justificado, marcação de ponto não realizada.", NomeUsuario.Trim());
                                                break;
                                            case "dia justificado":
                                                msg = String.Format("{0}, Jornada justificada na data corrente.", NomeUsuario.Trim());
                                                break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    #endregion                
                }

            }

            #region Envia SMS
            //para teste - Enviar SMS
            if (!vwUsuarioGrid[0].IsTelefoneSMSNull())
            {
                if (vwUsuarioGrid[0].IDEmpresa == 32 || vwUsuarioGrid[0].IDEmpresa == 34)
                {
                    if (msg.IndexOf("efetivada") > 0 || msg.IndexOf("finalizada") > 0)
                    {
                        if (msg.IndexOf("não") < 0)
                        {
                            EnviaSMS eSMS = new EnviaSMS();
                            eSMS.enviaSMS(vwUsuarioGrid[0].PrimeiroNome, HoraBatida, vwUsuarioGrid[0].TelefoneSMS);
                        }
                    }
                }
            }
            #endregion
            return msg;
        }

        protected string FormataTotalHoraPadrao(string TH)
        {
            if (TH.Length == 1)
                TH = string.Format("0{0}", TH);

            if (TH == "24")
                TH = "00";
            return TH;
        }

        public int? TotalHorasDia(DateTime? Entrada1, DateTime? Saida1, DateTime? Entrada2, DateTime? Saida2, string UpManhaTarde, DateTime DTBatida)
        {
            string HorasAbonadas = string.Empty;
            TotalHorasDIAString = "00:00:00.000";

            //Variável time span para o total de dias.
            TimeSpan TotalDiaSegundos;

            ///
            /// Acrescentar aqui, pesquisar se há alguma justificativa feita para acrescentar na somatória do total de horas diárias!!
            /// 
            ///

            //Verificar se já há ponto batido para hj.
            if (vwFrequenciaDia.Rows.Count > 0)
            {
                //Verifica se há justificativa para o dia.
                if (!vwFrequenciaDia[0].IsIDMotivoFaltaNull())
                {
                    IDmotivoFalta = vwFrequenciaDia[0].IDMotivoFalta;
                    switch (vwFrequenciaDia[0].IDTPJustificativa)
                    {
                        case 0: //Caso Registro único
                            switch (vwFrequenciaDia[0].TurnoJustificado.Trim())
                            {
                                case "0":
                                    if (!Entrada1.HasValue)
                                        Entrada1 = vwFrequenciaDia[0].EntradaManhaJust;
                                    break;
                                case "1":
                                    if (!Saida1.HasValue)
                                        Saida1 = vwFrequenciaDia[0].SaidaManhaJust;
                                    break;
                                case "2":
                                    if (!Entrada2.HasValue)
                                        Entrada2 = vwFrequenciaDia[0].EntradaTardeJust;
                                    break;
                                case "3":
                                    if (!Saida2.HasValue)
                                        Saida2 = vwFrequenciaDia[0].SaidaTardeJust;
                                    break;
                            }
                            break;
                        case 1: //Caso meio Período
                            if (Abono(Convert.ToInt32(IDmotivoFalta)))
                                HorasAbonadas = (vwUsuarioGrid[vinculo].TotHorasDiarias / 2).ToString();
                            else
                                HorasAbonadas = "00:00:00";
                            break;
                        case 2: //Caso integral.
                            break;
                    }
                }
            }
            else //se não houver registro de motivo falta id recebe 0 (NULO)
                IDmotivoFalta = null;

            //-16/01/2015 -- Alterado, acrecentei período para saber se é update da saida da manhã ou da da tarde !
            // Se da manhã e faz 8 horas não apresentar msg de carga horária incompleta.

            if ((Saida1.HasValue && Saida2.HasValue && Entrada1.HasValue && Entrada2.HasValue))
            {
                TotalHorasDIAString = Convert.ToString((Saida1 - Entrada1) + (Saida2 - Entrada2));
            }
            else if (Saida2.HasValue && Entrada2.HasValue)
            {
                TotalHorasDIAString = Convert.ToString(Saida2 - Entrada2);
            }
            else if (Saida1.HasValue && Entrada1.HasValue)
            {
                TotalHorasDIAString = Convert.ToString(Saida1 - Entrada1);
            }

            //Se abonadas... acrescentar no total de horas.
            if (HorasAbonadas != string.Empty)
            {
                TotalHorasDIAString = Convert.ToDateTime(TotalHorasDIAString).AddHours(Convert.ToInt32(HorasAbonadas)).ToString();
            }

            TotalDiaSegundos = TimeSpan.Parse(TotalHorasDIAString);

            if ((TotalDiaSegundos.TotalMinutes + 15) >= (TotalHorasDiaUsuarioPadrao * 60)) //Td em minutos para fazer comparativo.
            {
                msg = "Carga Total";
                //Só nula o IDMotivoFalta se o valor for igual a 37 ou 38
                if (IDmotivoFalta == 37 || IDmotivoFalta == 38)
                {
                    OBS = string.Empty;
                    IDmotivoFalta = null;
                }

                if (!vwFrequenciaDia[0].IsIDMotivoFaltaNull() && TotalDiaSegundos.TotalHours >= TotalHorasDiaUsuarioPadrao)
                {
                    if (IDmotivoFalta == 37 || IDmotivoFalta == 38)
                    {
                        OBS = "Carga horária da jornada completa.";
                        IDmotivoFalta = vwFrequenciaDia[0].IDMotivoFalta;
                    }
                }
                //Se não completar a carga horária total do dia lançará a mensagem abaixo.
                else if ((TotalDiaSegundos.TotalMinutes + 15) < (TotalHorasDiaUsuarioPadrao * 60))
                {
                    msg = "Carga Incompleta";
                    if (IDmotivoFalta == 37 || IDmotivoFalta == 38)
                    {
                        OBS = "Carga horária da jornada incompleta.";
                    }
                }
            }
            else if ((TotalDiaSegundos.TotalMinutes + 15) < (TotalHorasDiaUsuarioPadrao * 60))
            {
                switch (UpManhaTarde)
                {
                    case "Tarde":
                        msg = "Carga Incompleta";
                        if (IDmotivoFalta == 37 || IDmotivoFalta == 38)
                        {
                            OBS = string.Empty;
                            OBS = "Carga horária da jornada incompleta.";
                            IDmotivoFalta = null;
                        }
                        break;
                    case "Manha":
                        if (vwUsuarioGrid[vinculo].TotHorasDiarias < 8 && RegimePlantonista) // Se menor trabalha 6 horas por dia e deve receber o aviso.
                        {
                            msg = "Carga Incompleta";
                            if (IDmotivoFalta == 37 || IDmotivoFalta == 38)
                            {
                                OBS = string.Empty;
                                OBS = "Carga horária da jornada incompleta.";
                                IDmotivoFalta = null;
                            }
                        }
                        else if (vwUsuarioGrid[vinculo].RegimePlantonista) //Se regime plantonistas seguir abaixo
                        {
                            if ((TotalDiaSegundos.TotalMinutes + 15) < (TotalHorasDiaUsuarioPadrao * 60))
                            {
                                msg = "Carga Incompleta";
                            }
                            else
                                msg = "Carga Total";
                        }
                        else
                        {
                            msg = "Carga Total";
                            if (IDmotivoFalta == 37 || IDmotivoFalta == 38)
                            {
                                OBS = string.Empty;
                                OBS = string.Empty;
                                IDmotivoFalta = null;
                            }
                        }
                        break;
                }
            }

            return Convert.ToInt32(TotalDiaSegundos.TotalSeconds);
        }

        //13/07/2017 - FUNÇÃO NOVA. IMPLEMENTADA PARA TIRAR O UPDATE DESSA ROTINA E COLOCA-LA DIRETO NO BANCO, NUMA TRIGGER!
        // RETORNA APENAS DIZENDO SE DEU CARGA TOTAL OU NÃO PARA INFORMAR O CLIENT.
        public string GetMSGCargaTotal(int TotalHorasSegundos)
        {
            //Verifica se há registro para o dia.
            if (vwFrequenciaDia.Rows.Count > 0)
            {
                //Primeiro: Carga total, O total completo + 15 minutos maior que o padrão de hora do dia = Carga Total.
                if (((TotalHorasSegundos) + (15 * 60)) >= (TotalHOrasDiaPadraoUsuario * 3600))
                {
                    msg = "Carga Total";
                }
                else
                {
                    msg = "Carga Incompleta";
                }
            }

            return msg;
        }

        public string HorasDiasOcorrencia(int mes, int IDUsuario, int Ano, DataSetPontoFrequencia ds, int IDEmpresa, int IDSetor, bool DescontoTotalJornada)
        {
            DataSetPontoFrequenciaTableAdapters.vWHorasDiaTableAdapter adpvWhoras = new DataSetPontoFrequenciaTableAdapters.vWHorasDiaTableAdapter();

            ds.EnforceConstraints = false;

            ds.vWHorasDia.BeginLoadData();

            if (!DescontoTotalJornada)
                adpvWhoras.FillByHorasDiasOcorrencia(ds.vWHorasDia, IDUsuario.ToString(), Ano, mes.ToString(), IDEmpresa, IDSetor.ToString());
            else
                adpvWhoras.FillByHorasDiasOcorrenciaTotalJornada(ds.vWHorasDia, IDUsuario.ToString(), Ano, mes.ToString(), IDEmpresa, IDSetor.ToString());


            ds.vWHorasDia.EndLoadData();

            try
            {
                if (ds.vWHorasDia.Rows.Count == 0)
                {
                    msg = "1"; //Não há Registros
                }
            }
            catch (Exception ex)
            {
                msg = "2"; //Houve Falha;
                //msg = ex.ToString();
                ex.ToString();
            }

            System.GC.Collect();

            return msg;
        }
        public string HorasDiasOcorrencia(int mes, int IDUsuario, int Ano, DataSetPontoFrequencia ds, 
            int IDEmpresa, int IDSetor, bool DescontoTotalJornada, int IDVinculoUsuario)
        {
            DataSetPontoFrequenciaTableAdapters.vWHorasDiaTableAdapter adpvWhoras = new DataSetPontoFrequenciaTableAdapters.vWHorasDiaTableAdapter();

            ds.EnforceConstraints = false;

            ds.vWHorasDia.BeginLoadData();

            if (!DescontoTotalJornada)
                adpvWhoras.FillByHorasDiasOcorrenciaVinculo(ds.vWHorasDia, IDUsuario.ToString(), Ano, mes.ToString(), IDEmpresa,Convert.ToInt64(IDVinculoUsuario), IDSetor.ToString());
            else
                adpvWhoras.FillByHorasDiasOcorrenciaTotalJornadaVinculo(ds.vWHorasDia, IDUsuario.ToString(), Ano, mes.ToString(), IDEmpresa, IDSetor.ToString(),Convert.ToInt64(IDVinculoUsuario));


            ds.vWHorasDia.EndLoadData();

            try
            {
                if (ds.vWHorasDia.Rows.Count == 0)
                {
                    msg = "1"; //Não há Registros
                }
            }
            catch (Exception ex)
            {
                msg = "2"; //Houve Falha;
                //msg = ex.ToString();
                ex.ToString();
            }

            System.GC.Collect();

            return msg;
        }

        public string HorasDias(int mes, int IDUsuario, int Ano, DataSetPontoFrequencia ds, int IDEmpresa, int IDSetor)
        {
            DataSetPontoFrequenciaTableAdapters.vWHorasDiaTableAdapter adpvWhoras = new DataSetPontoFrequenciaTableAdapters.vWHorasDiaTableAdapter();

            ds.EnforceConstraints = false;

            ds.vWHorasDia.BeginLoadData();

            if (IDUsuario != 0)
                adpvWhoras.FillByHorasDia(ds.vWHorasDia, IDUsuario.ToString(), Ano, mes.ToString(), IDEmpresa, IDSetor.ToString());
            else
            {
                adpvWhoras.FillByFreqMesFolha(ds.vWHorasDia, mes, Ano, IDSetor, IDEmpresa);
            }

            ds.vWHorasDia.EndLoadData();

            try
            {
                if (ds.vWHorasDia.Rows.Count == 0)
                {
                    msg = "1"; //Não há Registros
                }
            }
            catch (Exception ex)
            {
                msg = "2"; //Houve Falha;
                //msg = ex.ToString();
                ex.ToString();
            }

            System.GC.Collect();

            return msg;
        }

        public string HorasDias(int mes, int IDUsuario, int Ano, DataSetPontoFrequencia ds, int IDEmpresa, int IDSetor, int IDVinculoUsuario)
        {
            DataSetPontoFrequenciaTableAdapters.vWHorasDiaTableAdapter adpvWhoras = new DataSetPontoFrequenciaTableAdapters.vWHorasDiaTableAdapter();

            ds.EnforceConstraints = false;

            ds.vWHorasDia.BeginLoadData();

            if (IDUsuario != 0)
                adpvWhoras.FillByHorasDiaVinculo(ds.vWHorasDia, IDUsuario.ToString(), Ano, mes.ToString(), IDEmpresa, IDSetor.ToString(),Convert.ToInt64(IDVinculoUsuario));
            else
            {
                adpvWhoras.FillByFreqMesFolha(ds.vWHorasDia, mes, Ano, IDSetor, IDEmpresa);
            }

            ds.vWHorasDia.EndLoadData();

            try
            {
                if (ds.vWHorasDia.Rows.Count == 0)
                {
                    msg = "1"; //Não há Registros
                }
            }
            catch (Exception ex)
            {
                msg = "2"; //Houve Falha;
                //msg = ex.ToString();
                ex.ToString();
            }

            System.GC.Collect();

            return msg;
        }


        public string HorasDiasPlantonista(int mes, int IDUsuario, int Ano, DataSetPontoFrequencia ds, int IDEmpresa, int IDSetor, bool Ocorrencia)
        {
            DataSetPontoFrequenciaTableAdapters.vWHorasDiaTableAdapter adpvWhoras = new DataSetPontoFrequenciaTableAdapters.vWHorasDiaTableAdapter();

            ds.EnforceConstraints = false;

            ds.vWHorasDia.BeginLoadData();

            if (Ocorrencia)
            {
                adpvWhoras.FillByHorasDiaPlantonistaTotalDia(ds.vWHorasDia, IDUsuario.ToString(), Ano, mes.ToString(), IDEmpresa, IDSetor.ToString());
            }
            else
            {
                adpvWhoras.FillByHorasDiaPlantonista(ds.vWHorasDia, IDUsuario, mes, IDEmpresa, IDSetor,Ano);
            }


            ds.vWHorasDia.EndLoadData();

            try
            {
                if (ds.vWHorasDia.Rows.Count == 0)
                {
                    msg = "1"; //Não há Registros
                }
            }
            catch (Exception ex)
            {
                msg = "2"; //Houve Falha;
                //msg = ex.ToString();
                ex.ToString();
            }

            System.GC.Collect();

            return msg;
        }

        public string HorasDiasOrgao(int mes, int Ano, DataSetPontoFrequencia ds, int IDEmpresa)
        {
            DataSetPontoFrequenciaTableAdapters.vWHorasDiaTableAdapter adpvWhoras = new DataSetPontoFrequenciaTableAdapters.vWHorasDiaTableAdapter();

            ds.EnforceConstraints = false;

            ds.vWHorasDia.BeginLoadData();

            adpvWhoras.FillFreqMesEmpresa(ds.vWHorasDia, mes, DateTime.Now.Year, IDEmpresa);

            ds.vWHorasDia.EndLoadData();

            try
            {
                if (ds.vWHorasDia.Rows.Count == 0)
                {
                    msg = "1"; //Não há Registros
                }
            }
            catch (Exception ex)
            {
                msg = "2"; //Houve Falha;
                //msg = ex.ToString();
                ex.ToString();
            }

            System.GC.Collect();

            return msg;
        }

        private int TotalHorasDiaJust(DateTime? Entrada1, DateTime? Saida1, DateTime? Entrada2, DateTime? Saida2, DateTime DTFrequencia, string OBSJustificativa, int idmotivofalta)
        {
            string total = string.Format("{0} 00:00:00", DTFrequencia.ToShortDateString());
            TimeSpan TotalSegundos;

            if ((Saida1.HasValue && Saida2.HasValue && Entrada1.HasValue && Entrada2.HasValue))
            {
                total = Convert.ToString((Saida1 - Entrada1) + (Saida2 - Entrada2));
            }
            else if (Saida2.HasValue && Entrada2.HasValue)
            {
                total = Convert.ToString(Saida2 - Entrada2);
            }
            else if (Saida1.HasValue && Entrada1.HasValue)
            {
                total = Convert.ToString(Saida1 - Entrada1);
            }

            TimeSpan.TryParse(total, out TotalSegundos);

            if (OBSJustificativa == "Mat" || OBSJustificativa == "Vesp")
            {
                total = null;

                if (Abono(idmotivofalta))
                {
                    if (IDRegimeHoraJustificativa != 5 && TotalHorasDiaUsuarioPadrao == 4)
                        total = Convert.ToString((((TotalHorasDiaUsuarioPadrao / 2) * 3600) + TotalExistenteSegudos) + 1800);
                    else
                        total = Convert.ToString((((TotalHorasDiaUsuarioPadrao / 2) * 3600) + TotalExistenteSegudos));
                }

                else
                    total = TotalExistenteSegudos.ToString();
            }
            else
                total = Convert.ToInt32(TotalSegundos.TotalSeconds).ToString();

            return Convert.ToInt32(total);
        }

        private bool LimiteEntradaSaidaAlmoco(DateTime Entrada, DateTime Saida, string opcao)
        {
            bool perm = false;
            switch (opcao)
            {
                case "turno":
                    Entrada = Entrada.AddMinutes(10);
                    if (Saida > Entrada)
                        perm = true;
                    else
                        perm = false;
                    break;
                case "almoco":
                    Entrada = Entrada.AddHours(1);
                    if (Saida > Entrada)
                        perm = true;
                    else
                        perm = false;
                    break;
            }

            return perm;
        }

        private bool PermiteJustificativa(DataSetPontoFrequencia dsJ, int idfrequencia, int idusuario, int IDTPFrequencia, string OBSJustificativa, DateTime DTFrequenciaJust)
        {
            //23/01/2019
            //PERMISSÃO GERAL PARA JUSTIFICATIVA. MOTIVO: NA INTEGRAÇÃO SÃO DESCONSIDERADAS AS JUSTIFICATIVAS.
            bool permite = true;

            DataSetPontoFrequenciaTableAdapters.TBFrequenciaTableAdapter adpFreqJ = new DataSetPontoFrequenciaTableAdapters.TBFrequenciaTableAdapter();

            adpFreqJ.FillIDFreqUsuario(dsJ.TBFrequencia, idfrequencia, idusuario);

            if (dsJ.TBFrequencia.Rows.Count > 0)
            {

                //Variaveis para usar na função ParametrosOperacao
                if (!dsJ.TBFrequencia[0].IsHoraEntraManhaNull())
                    horaentradaManha = dsJ.TBFrequencia[0].HoraEntraManha;

                if (!dsJ.TBFrequencia[0].IsHoraSaidaManhaNull())
                    horasaidamanha = dsJ.TBFrequencia[0].HoraSaidaManha;

                if (!dsJ.TBFrequencia[0].IsHoraEntradaTardeNull())
                    horaentradatarde = dsJ.TBFrequencia[0].HoraEntradaTarde;

                if (!dsJ.TBFrequencia[0].IsHoraSaidaTardeNull())
                    horasaidatarde = dsJ.TBFrequencia[0].HoraSaidaTarde;

                switch (IDTPFrequencia)
                {
                    case 0: //Caso de registro único
                        if (OBSJustificativa == "0" || OBSJustificativa == "1")
                        {
                            if (!dsJ.TBFrequencia[0].IsEntradaManhaJustNull() || !dsJ.TBFrequencia[0].IsSaidaManhaJustNull())
                                permite = false;
                            else
                            {
                                switch (OBSJustificativa) //Analisando se período está em conformidade com os 10 minutos de tolerância
                                {
                                    case "0":
                                        if (!dsJ.TBFrequencia[0].IsHoraSaidaManhaNull())// Se de saída da manhã não for nulo, teste o limite de 10 minutos do turno
                                        {
                                            if (LimiteEntradaSaidaAlmoco(Convert.ToDateTime(string.Format("{0} {1}", DTFrequenciaJust.ToShortDateString(), HoraPadraoEntradaManha)), dsJ.TBFrequencia[0].HoraSaidaManha, "turno"))
                                                permite = true;
                                            else
                                            {
                                                msg = "Justificativa não respeita o intervalo entre Início e Fim do período. Utilize justificativa de meio período ou integral.";
                                                return true;

                                                //permite = false;
                                            }
                                        }
                                        else
                                        {
                                            permite = true;
                                        }
                                        break;
                                    case "1":
                                        if (!dsJ.TBFrequencia[0].IsHoraEntraManhaNull())// Se de entrada da manhã não for nulo, teste o limite de 10 minutos do turno
                                        {
                                            if (LimiteEntradaSaidaAlmoco(dsJ.TBFrequencia[0].HoraEntraManha, Convert.ToDateTime(string.Format("{0} {1}", DTFrequenciaJust.ToShortDateString(), HoraPadraoSaidaManha)), "turno"))
                                                permite = true;
                                            else
                                            {
                                                msg = "Justificativa não respeita o intervalo entre Início e fim do período. Utilize justificativa de meio período ou integral.";
                                                return true;
                                                //permite = false;
                                            }
                                        }
                                        else
                                        {
                                            permite = true;
                                        }
                                        break;
                                }
                                //permite = true;
                            }
                        }
                        else if (OBSJustificativa == "2" || OBSJustificativa == "3")
                        {
                            if (!dsJ.TBFrequencia[0].IsEntradaTardeJustNull() || !dsJ.TBFrequencia[0].IsSaidaTardeJustNull())
                            {
                                msg = "Registro já justificado. Exclua a justificativa e refaça a operação caso seja necessário.";
                                return true;
                                //permite = false;
                            }
                            else
                            {
                                switch (OBSJustificativa) //Analisando se período está em conformidade com os 10 minutos de tolerância
                                {
                                    case "2":
                                        if (!dsJ.TBFrequencia[0].IsHoraSaidaTardeNull())// Se a saída da tarde não for nulo, teste o limite de 10 minutos do turno
                                        {
                                            if (LimiteEntradaSaidaAlmoco(Convert.ToDateTime(string.Format("{0} {1}", DTFrequenciaJust.ToShortDateString(), HoraPadraoEntradaTarde)), dsJ.TBFrequencia[0].HoraSaidaTarde, "turno"))
                                                permite = true;
                                            else
                                            {
                                                msg = "Justificativa não respeita o intervalo entre Início e fim do período. Utilize justificativa de meio período ou integral.";
                                                return true;
                                                //permite = false;
                                            }
                                        }
                                        else
                                            permite = true;
                                        //Teste limite do almoço
                                        if (!dsJ.TBFrequencia[0].IsHoraSaidaManhaNull())
                                        {
                                            if (LimiteEntradaSaidaAlmoco(dsJ.TBFrequencia[0].HoraSaidaManha, Convert.ToDateTime(string.Format("{0} {1}", DTFrequenciaJust.ToShortDateString(), HoraPadraoEntradaTarde)), "almoco"))
                                                permite = true;
                                            else
                                            {
                                                msg = "Justificativa não respeita o horário de almoço. Utilize justificativa de meio período ou integral.";
                                                return true;
                                                //permite = false;
                                            }
                                        }
                                        break;
                                    case "3":
                                        if (!dsJ.TBFrequencia[0].IsHoraEntradaTardeNull())// Se de entrada da tarde não for nulo, teste o limite de 10 minutos do turno
                                        {
                                            if (LimiteEntradaSaidaAlmoco(dsJ.TBFrequencia[0].HoraEntradaTarde, Convert.ToDateTime(string.Format("{0} {1}", DTFrequenciaJust.ToShortDateString(), HoraPadraoSaidTarde)), "turno"))
                                                permite = true;
                                            else
                                            {
                                                msg = "Justificativa não respeita o intervalo entre Início e fim do período. Utilize justificativa de meio período ou integral.";
                                                return true;
                                                //permite = false;
                                            }
                                        }
                                        else
                                        {
                                            permite = true;
                                        }
                                        break;
                                }
                            }
                        }
                        break;
                    case 1: //Justificativa de meio periodo
                            //if (dsJ.TBFrequencia[0].IsTurnoJustificadoNull()) //Se TurnoJustificado for nulo, permite a inserção da justificativa de meio período.
                        permite = true;
                        //else
                        // permite = false;
                        break;
                    case 2:
                        permite = true;
                        break;
                }

                //Adicionado -- Caso Haja um total de horas já preenchido, adicionar valor para fazer a somatória do dia.
                if (!dsJ.TBFrequencia[0].IsTotalHorasDiaSegundosNull())
                    TotalExistenteSegudos = dsJ.TBFrequencia[0].TotalHorasDiaSegundos;
                else
                    TotalExistenteSegudos = 0;
            }
            else
            {
                permite = true;
            }

            return permite;
        }

        private int QuantidadeJustificativaMes(int IDMotivoFalta, long IDVinculoUsuario, int MesReferencia, int AnoReferencia)
        {
            DataSetPontoFrequenciaTableAdapters.vwQTDJustificativaUsuarioTableAdapter adpQTDJustificativa = new DataSetPontoFrequenciaTableAdapters.vwQTDJustificativaUsuarioTableAdapter();
            DataSetPontoFrequencia.vwQTDJustificativaUsuarioDataTable vwQTDJustificativa = new DataSetPontoFrequencia.vwQTDJustificativaUsuarioDataTable();

            adpQTDJustificativa.FillMesAnoRefIDVinculoIDMotivo(vwQTDJustificativa, MesReferencia, AnoReferencia, IDVinculoUsuario, IDMotivoFalta);

            if (vwQTDJustificativa.Rows.Count > 0)
            {
                return vwQTDJustificativa[0].TotalMotivoFalta;
            }
            else
                return 0;
        }

        private bool Abono(int IDMotivoFalta)
        {
            DataSetPontoFrequencia.TBMotivoFaltaDataTable TBmotivoFalta = new DataSetPontoFrequencia.TBMotivoFaltaDataTable();
            DataSetPontoFrequenciaTableAdapters.TBMotivoFaltaTableAdapter adpMotivoFalta = new DataSetPontoFrequenciaTableAdapters.TBMotivoFaltaTableAdapter();

            adpMotivoFalta.FillIDMotivoFalta(TBmotivoFalta, IDMotivoFalta);

            return TBmotivoFalta[0].AbonarHoras;
        }

        public string PermissaoJustificativaMeioPeriodo(int IDVinculoUsuario, DateTime DTFrequencia)
        {

            DataSetPontoFrequencia dsH = new DataSetPontoFrequencia();
            DataSetPontoFrequenciaTableAdapters.vWHorasDiaTableAdapter
                adphorasdias = new DataSetPontoFrequenciaTableAdapters.vWHorasDiaTableAdapter();
            adphorasdias.FillIDVinculoDTFrequencia(dsH.vWHorasDia, IDVinculoUsuario, DTFrequencia);
            if (dsH.vWHorasDia.Rows.Count > 0)
            {
                int totalString = ((dsH.vWHorasDia[0].TotHorasDiarias * 3600) / 2);
                totalString += (TimeSpan.Parse(dsH.vWHorasDia[0].HorasDia).Seconds);

                if (totalString < (dsH.vWHorasDia[0].TotHorasDiarias * 3600))
                {
                    return "Justificativa de meio período não realizada por não contemplar o total do dia. Utilize a justificativa integral.";
                }
            }

            return "sucesso";
        }
        public string JustificaFrequenciaDia(int IDFrequencia, int IDUsuario, int IDMotivoFalta, string OBS, DateTime DTFrequencia, int? TotalHorasDiaP, int IDTPJustificativa, int IDEmpresa, string OBSJustificativa, int IDUsuarioOperador, int IDVinculoUsuario, int SitJustificativa)
        {
            DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
            Justificativa.Justificativa jus = new Justificativa.Justificativa();
            DataSetPontoFrequenciaTableAdapters.TBFeriasTableAdapter adpFerias = 
                new DataSetPontoFrequenciaTableAdapters.TBFeriasTableAdapter();

            //Adicionado 29/07/2019 -- Verifica existência Motivo de Falta
            DataSetPontoFrequenciaTableAdapters.TBMotivoFaltaTableAdapter adpMotivo = new DataSetPontoFrequenciaTableAdapters.TBMotivoFaltaTableAdapter();
            adpMotivo.FillIDMotivoFalta(ds.TBMotivoFalta, IDMotivoFalta);
            if(ds.TBMotivoFalta.Rows.Count == 0)
            {
                return "Selecione um motivo de justificativa válido!";
            }
            //

            try
            {

                adpFerias.Connection.Open();
                adpFerias.FillByVerificaFeriasCorrente(ds.TBFerias, DTFrequencia, IDUsuario, IDVinculoUsuario);
                adpFerias.Connection.Close();
                if (ds.TBFerias.Rows.Count > 0)
                    return "Ferias/Licença no perído. Impossível incluir Justificativa.";

            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            //03/02/2019
            //verifica se houve alguma mudança na tentativa de registro e mudança no iD. De zero para outro número

            if (IDFrequencia == 0)
            {
                adpFrequencia.FillFrequenciaDia(ds.TBFrequencia, DTFrequencia, IDUsuario, IDVinculoUsuario);
                if (ds.TBFrequencia.Rows.Count > 0)
                {
                    IDFrequencia = ds.TBFrequencia[0].IDFrequencia;
                }
            }

            //Prevendo mais de 3 lançamentos do motivo 44 (Esquecimento), se já estiver igual a 3 não deixar realizar a próxima justificativa
            //Por enquanto, direto na rotina, quando houver mais limitações, desenvolver rotina para o mesmo.
            //if (IDMotivoFalta == 76)
            //{
            //if (QuantidadeJustificativaMes(IDMotivoFalta, IDVinculoUsuario, DTFrequencia.Month, DTFrequencia.Year) >= 3)
            //{
            //return "Servidor já utilizou três ou mais vezes a justificativa selecionada! Justificativa não lançada.";
            //}
            //}
            // ---

            try
            {
                //Dados do usuário em questão
                DadosUsuario(ds, IDVinculoUsuario, DTFrequencia);


                //29/08/2019 -- Verificar o total de horas que deveria ser feito

                if((IDTPJustificativa -1) == 1)
                {
                    DataSetPontoFrequencia dsH = new DataSetPontoFrequencia();
                    DataSetPontoFrequenciaTableAdapters.vWHorasDiaTableAdapter
                        adphorasdias = new DataSetPontoFrequenciaTableAdapters.vWHorasDiaTableAdapter();
                    adphorasdias.FillIDVinculoDTFrequencia(dsH.vWHorasDia, IDVinculoUsuario, DTFrequencia);
                    if(dsH.vWHorasDia.Rows.Count > 0 )
                    {
                        int totalString = ((dsH.vWHorasDia[0].TotHorasDiarias * 3600) / 2);
                        totalString += (TimeSpan.Parse(dsH.vWHorasDia[0].HorasDia).Seconds);

                        if(totalString < (dsH.vWHorasDia[0].TotHorasDiarias * 3600))
                        {
                            return "Justificativa de meio período não realizada por não contemplar o total do dia. Utilize a justificativa integral.";
                        }
                    }
                }

               // --------------------

                switch (RegimePlantonista)
                {
                    case true:
                        #region Regime Plantonista
                        //Aqui as justificativas do regime plantonista.
                        if (IDFrequencia == 0) //Inserção de frequência
                        {
                            switch (IDTPJustificativa - 1)
                            {
                                case 0:
                                    return "Utilize a justificativa de meio período ou integral para plantonistas";
                                    break;
                                case 1:
                                    if (Abono(IDMotivoFalta))
                                    {
                                        if (adpFrequencia.InsertJustificado(IDUsuario, IDMotivoFalta, DTFrequencia, string.Format("{0}: {1}", textoJustificativa, OBS), null, DTFrequencia.Month, DTFrequencia.Year, horaentradamanhaJust, horasaidamanhaJust, horaentradatardeJust, horasaidamanhaJust, IDTPJustificativa, null, (TotalHorasDiaUsuarioPadrao * (3600 / 2)), IDVinculoUsuario, IDEmpresa) > 0)
                                        {
                                            msg = "Justificativa realizada com sucesso.";
                                            jus.InsertaLogJustificativaDireta(IDFrequencia, IDUsuario, IDMotivoFalta, string.Format("{0}: {1}", textoJustificativa, OBS), DTFrequencia, TotalHorasDiaP, IDTPJustificativa, IDEmpresa, OBSJustificativa, IDUsuarioOperador, IDVinculoUsuario, SitJustificativa);
                                        }
                                        else
                                            msg = "Justificativa não realizada. Repita o processo.";
                                    }
                                    else
                                    {
                                        if (adpFrequencia.InsertJustificado(IDUsuario, IDMotivoFalta, DTFrequencia, string.Format("{0}: {1}", textoJustificativa, OBS), null, DTFrequencia.Month, DTFrequencia.Year, horaentradamanhaJust, horasaidamanhaJust, horaentradatardeJust, horasaidamanhaJust, IDTPJustificativa, null, null, IDVinculoUsuario, IDEmpresa) > 0)
                                        {

                                            msg = "Justificativa realizada com sucesso.";
                                            jus.InsertaLogJustificativaDireta(IDFrequencia, IDUsuario, IDMotivoFalta, string.Format("{0}: {1}", textoJustificativa, OBS), DTFrequencia, TotalHorasDiaP, IDTPJustificativa, IDEmpresa, OBSJustificativa, IDUsuarioOperador, IDVinculoUsuario, SitJustificativa);
                                        }
                                        else
                                            msg = "Justificativa não realizada. Repita o processo.";
                                    }
                                    break;
                                case 2:
                                    if (Abono(IDMotivoFalta))
                                    {
                                        if (adpFrequencia.InsertJustificado(IDUsuario, IDMotivoFalta, DTFrequencia, string.Format("{0}: {1}", textoJustificativa, OBS), null, DTFrequencia.Month, DTFrequencia.Year, horaentradamanhaJust, horasaidamanhaJust, horaentradatardeJust, horasaidamanhaJust, IDTPJustificativa, null, (TotalHorasDiaUsuarioPadrao * 3600), IDVinculoUsuario, IDEmpresa) > 0)
                                        {
                                            msg = "Justificativa realizada com sucesso.";
                                            jus.InsertaLogJustificativaDireta(IDFrequencia, IDUsuario, IDMotivoFalta, string.Format("{0}: {1}", textoJustificativa, OBS), DTFrequencia, TotalHorasDiaP, IDTPJustificativa, IDEmpresa, OBSJustificativa, IDUsuarioOperador, IDVinculoUsuario, SitJustificativa);
                                        }
                                        else
                                            msg = "Justificativa não realizada. Repita o processo.";
                                        break;
                                    }
                                    else
                                    {
                                        if (adpFrequencia.InsertJustificado(IDUsuario, IDMotivoFalta, DTFrequencia, string.Format("{0}: {1}", textoJustificativa, OBS), null, DTFrequencia.Month, DTFrequencia.Year, horaentradamanhaJust, horasaidamanhaJust, horaentradatardeJust, horasaidamanhaJust, IDTPJustificativa, null, null, IDVinculoUsuario, IDEmpresa) > 0)
                                        {
                                            msg = "Justificativa realizada com sucesso.";
                                            jus.InsertaLogJustificativaDireta(IDFrequencia, IDUsuario, IDMotivoFalta, string.Format("{0}: {1}", textoJustificativa, OBS), DTFrequencia, TotalHorasDiaP, IDTPJustificativa, IDEmpresa, OBSJustificativa, IDUsuarioOperador, IDVinculoUsuario, SitJustificativa);
                                        }
                                        else
                                            msg = "Justificativa não realizada. Repita o processo.";
                                    }
                                    break;
                            }
                        }
                        else //justificativa de registro realizado. (já há um registro realizado)
                        {
                            if (Abono(IDMotivoFalta))
                            {
                                if (IDTPJustificativa - 1 == 2)
                                    adpFrequencia.UpdateJustificaFaltaAtraso(IDUsuario, IDMotivoFalta, string.Format("{0}: {1}", textoJustificativa, OBS), 1, null, null, null, null, null, 3, null, (TotalHOrasDiaPadraoUsuario * 3600), SitJustificativa, IDFrequencia, IDUsuario);
                                else if (IDTPJustificativa - 1 == 1)
                                    adpFrequencia.UpdateJustificaFaltaAtraso(IDUsuario, IDMotivoFalta, string.Format("{0}: {1}", textoJustificativa, OBS), 1, null, null, null, null, null, 3, null, (TotalHOrasDiaPadraoUsuario * (3600 / 2)), SitJustificativa, IDFrequencia, IDUsuario);
                            }
                            else //bemm aqui num tem ?
                            {
                                if (PermiteJustificativa(ds, IDFrequencia, IDUsuario, IDTPJustificativa, OBSJustificativa, DTFrequencia))
                                {
                                    //Verificar qual dos registros está nulo - algum estando nulo preencher com os valores padrões do usuário
                                    if (!horaentradaManha.HasValue)
                                        horaentradaManha = horaentradamanhaJust;
                                    if (!horasaidamanha.HasValue)
                                        horasaidamanha = horasaidamanhaJust;
                                    if (!horaentradatarde.HasValue)
                                        horaentradatarde = horaentradatardeJust;
                                    if (!horasaidatarde.HasValue)
                                        horasaidatarde = horasaidatardeJust;

                                    switch (IDTPJustificativa - 1)
                                    {
                                        case 0:
                                            TotalHorasDiaP = (TotalHorasDiaJust(horaentradaManha, horasaidamanha, horaentradatarde, horasaidatarde, DTFrequencia, "", IDMotivoFalta));
                                            break;
                                        case 1:
                                            TotalHorasDiaP = (TotalHorasDiaJust(horaentradaManha, horasaidamanha, horaentradatarde, horasaidatarde, DTFrequencia, "Mat", IDMotivoFalta));
                                            break;
                                        case 2:
                                            TotalHorasDiaP = TotalExistenteSegudos;
                                            break;
                                    }

                                    adpFrequencia.UpdateJustificaFaltaAtraso(IDUsuario, IDMotivoFalta, string.Format("{0}: {1}", textoJustificativa, OBS), 1, null, null, null, null, null, 3, null, TotalHorasDiaP, SitJustificativa, IDFrequencia, IDUsuario);
                                }
                                else
                                    return "Justificativa negada. Utilize a de turno Integral.";

                            }
                            jus.InsertaLogJustificativaDireta(IDFrequencia, IDUsuario, IDMotivoFalta, string.Format("{0}: {1}", textoJustificativa, OBS), DTFrequencia, TotalHorasDiaP, IDTPJustificativa, IDEmpresa, OBSJustificativa, IDUsuarioOperador, IDVinculoUsuario, SitJustificativa);
                            msg = string.Format("Justificativa realizada com sucesso.");
                        }
                        #endregion
                        break;
                    case false:
                        #region Regime de expediente
                        if (IDFrequencia == 0) //Não há dado inserido na tabela de frequência
                        {
                            try
                            {
                                switch (IDTPJustificativa - 1)
                                {
                                    //Se IDFrequencia = 0 , não há registro de frequência, portanto, permitir a inclusão da justificativa.
                                    case 0: //Justificativa de um registro 
                                        //Define variáveis para o insert;
                                        ParametrosOperacao(OBSJustificativa, DTFrequencia);
                                        if (IDRegimeHoraJustificativa != 5 && TotalHorasDiaUsuarioPadrao == 4)
                                            TotalHorasDiaUsuarioPadrao = (TotalHorasDiaUsuarioPadrao * 3600) + 1800;
                                        else
                                            TotalHorasDiaUsuarioPadrao = (TotalHorasDiaUsuarioPadrao * 3600);

                                        if (adpFrequencia.InsertJustificado(IDUsuario, IDMotivoFalta, DTFrequencia, string.Format("{0}: {1}", textoJustificativa, OBS), null, DTFrequencia.Month, DTFrequencia.Year, horaentradamanhaJust, horasaidamanhaJust, horaentradatardeJust, horasaidamanhaJust, IDTPJustificativa, null, TotalHorasDiaUsuarioPadrao, IDVinculoUsuario, IDEmpresa) > 0)
                                        {
                                            msg = "Justificativa realizada com sucesso.";
                                            jus.InsertaLogJustificativaDireta(IDFrequencia, IDUsuario, IDMotivoFalta, string.Format("{0}: {1}", textoJustificativa, OBS), DTFrequencia, TotalHorasDiaP, IDTPJustificativa, IDEmpresa, OBSJustificativa, IDUsuarioOperador, IDVinculoUsuario, SitJustificativa);
                                        }
                                        else
                                            msg = "Justificativa não realizada, Repita o processo.";
                                        break;
                                    case 1: //Justificativa de um período
                                             

                                        if (Abono(IDMotivoFalta))///Se IDMotivoFalta permite abonar ---
                                        {
                                            if (IDRegimeHoraJustificativa != 5 && TotalHorasDiaUsuarioPadrao == 4)

                                                TotalHorasDiaP = ((TotalHorasDiaUsuarioPadrao / 2) * 3600) + 1800;
                                            else
                                                TotalHorasDiaP = (TotalHorasDiaUsuarioPadrao / 2) * 3600;
                                        }
                                        else// Senão
                                            TotalHorasDiaP = 0;

                                        if (OBSJustificativa == "0")
                                            ParametrosOperacao("Mat", DTFrequencia); //Se Matutino, Insere os padrões da manhã, senão, tarde!
                                        else
                                            ParametrosOperacao("Vesp", DTFrequencia);

                                        if (adpFrequencia.InsertJustificado(IDUsuario, IDMotivoFalta, DTFrequencia, string.Format("{0}: {1}", textoJustificativa, OBS), null, DTFrequencia.Month, DTFrequencia.Year, horaentradamanhaJust, horasaidamanhaJust, horaentradatardeJust, horasaidatardeJust, IDTPJustificativa, OBSJustificativa, TotalHorasDiaP, IDVinculoUsuario, IDEmpresa) > 0)
                                        {
                                            msg = "Justificativa realizada com sucesso.";
                                            jus.InsertaLogJustificativaDireta(IDFrequencia, IDUsuario, IDMotivoFalta, string.Format("{0}: {1}", textoJustificativa, OBS), DTFrequencia, TotalHorasDiaP, IDTPJustificativa, IDEmpresa, OBSJustificativa, IDUsuarioOperador, IDVinculoUsuario, SitJustificativa);
                                        }

                                        else
                                            msg = "Justificativa não realizada, repita o processo.";
                                        break;

                                    case 2: // Justificativa de período integral

                                        if (Abono(IDMotivoFalta))
                                        {
                                            if (IDRegimeHoraJustificativa != 5 && TotalHorasDiaUsuarioPadrao == 4)
                                                TotalHorasDiaP = (TotalHorasDiaUsuarioPadrao * 3600) + 1800;
                                            else
                                                TotalHorasDiaP = TotalHorasDiaUsuarioPadrao * 3600;
                                        }
                                        else
                                            TotalHorasDiaP = TotalExistenteSegudos;

                                        if (adpFrequencia.InsertJustificado(IDUsuario, IDMotivoFalta, DTFrequencia, string.Format("{0}: {1}", textoJustificativa, OBS), null, DTFrequencia.Month, DTFrequencia.Year, Convert.ToDateTime(string.Format("{0} {1}", DTFrequencia.ToShortDateString(), HoraPadraoEntradaManha)), Convert.ToDateTime(string.Format("{0} {1}", DTFrequencia.ToShortDateString(), HoraPadraoSaidaManha)), Convert.ToDateTime(string.Format("{0} {1}", DTFrequencia.ToShortDateString(), HoraPadraoEntradaTarde)), Convert.ToDateTime(string.Format("{0} {1}", DTFrequencia.ToShortDateString(), HoraPadraoSaidTarde)), IDTPJustificativa, null, TotalHorasDiaP, IDVinculoUsuario, IDEmpresa) > 0)
                                        {
                                            msg = "Justificativa realizada com sucesso.";
                                            jus.InsertaLogJustificativaDireta(IDFrequencia, IDUsuario, IDMotivoFalta, string.Format("{0}: {1}", textoJustificativa, OBS), DTFrequencia, TotalHorasDiaP, IDTPJustificativa, IDEmpresa, OBSJustificativa, IDUsuarioOperador, IDVinculoUsuario, SitJustificativa);
                                        }
                                        else
                                            msg = "Justificativa não realizada, repita o processo.";
                                        break;
                                }
                            }
                            catch (Exception ex)
                            {
                                ex.ToString();
                            }
                        }
                        else
                        {
                            if (OBSJustificativa == "")
                            {

                                adpFrequencia.FillIDFreqUsuario(ds.TBFrequencia, IDFrequencia, IDUsuario);
                                if (ds.TBFrequencia.Rows.Count > 0)
                                {
                                    if (!ds.TBFrequencia[0].IsTurnoJustificadoNull())
                                        OBSJustificativa = ds.TBFrequencia[0].TurnoJustificado.Trim();
                                }
                            }
                            try
                            {
                                if (IDTPJustificativa - 1 == 1 && (OBSJustificativa == "0"))
                                    OBSJustificativa = "Mat";
                                else if (IDTPJustificativa - 1 == 1 && (OBSJustificativa == "1"))
                                    OBSJustificativa = "Vesp";

                                if (PermiteJustificativa(ds, IDFrequencia, IDUsuario, IDTPJustificativa, OBSJustificativa, DTFrequencia))
                                {
                                    ParametrosOperacao(OBSJustificativa, DTFrequencia);

                                    //Verificar qual dos registros está nulo - algum estando nulo preencher com os valores padrões do usuário
                                    if (!horaentradaManha.HasValue)
                                        horaentradaManha = horaentradamanhaJust;
                                    if (!horasaidamanha.HasValue)
                                        horasaidamanha = horasaidamanhaJust;
                                    if (!horaentradatarde.HasValue)
                                        horaentradatarde = horaentradatardeJust;
                                    if (!horasaidatarde.HasValue)
                                        horasaidatarde = horasaidatardeJust;

                                    switch (IDTPJustificativa - 1)
                                    {
                                        case 0:
                                            TotalHorasDiaP = null;
                                            //comentando em 23/01/2019 - DESCOMENTADO EM 28/03/2019 PEDIDO JOSIANE
                                            TotalHorasDiaP = TotalHorasDiaJust(horaentradaManha, horasaidamanha, horaentradatarde, horasaidatarde, DTFrequencia, "", IDMotivoFalta);
                                            break;
                                        case 1:
                                            //comentando em 23/01/2019 - DESCOMENTANDO EM 28/03/2019 PEDIDO JOSIANE
                                            TotalHorasDiaP = (TotalHorasDiaJust(horaentradaManha, horasaidamanha, horaentradatarde, horasaidatarde, DTFrequencia, "Mat", IDMotivoFalta));
                                            if (Abono(IDMotivoFalta))
                                            {
                                                TotalHorasDiaP = TotalHOrasDiaPadraoUsuario * 3600;
                                            }
                                            break;
                                        case 2:
                                            textoJustificativa = "Justificativa de período integral.";
                                            if (Abono(IDMotivoFalta))
                                            {
                                                if (TotalHorasDiaUsuarioPadrao == 4 && IDRegimeHoraJustificativa != 5)
                                                    TotalHorasDiaP = (TotalHorasDiaUsuarioPadrao * 3600) + 1800;
                                                else
                                                    TotalHorasDiaP = TotalHOrasDiaPadraoUsuario * 3600;
                                            }
                                            else
                                                TotalHorasDiaP = TotalExistenteSegudos;
                                            break;
                                    }
                                    adpFrequencia.UpdateJustificaFaltaAtraso(IDUsuario, IDMotivoFalta, string.Format("{0}: {1}", textoJustificativa, OBS), 1, null, horaentradamanhaJust, horasaidamanhaJust, horaentradatardeJust, horasaidatardeJust, IDTPJustificativa, OBSJustificativa, TotalHorasDiaP, SitJustificativa, IDFrequencia, IDUsuario);
                                    jus.InsertaLogJustificativaDireta(IDFrequencia, IDUsuario, IDMotivoFalta, string.Format("{0}: {1}", textoJustificativa, OBS), DTFrequencia, TotalHorasDiaP, IDTPJustificativa, IDEmpresa, OBSJustificativa, IDUsuarioOperador, IDVinculoUsuario, SitJustificativa);
                                    msg = string.Format("Justificativa realizada com sucesso.");
                                }
                                else
                                    return "Justificativa negada. Utilize a de turno Integral.";

                            }
                            catch (Exception ex)
                            {
                                ex.ToString();
                            }
                        }
                        #endregion
                        break;
                }

            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                ErroOperacao EO = new ErroOperacao();
                msg = EO.RetornaErroOperacao(ex);
            }
            catch (Exception ex)
            {
                msg = string.Format("Houve falha na operação, Verifique os dados inseridos e tente novamente.");
                ex.ToString();
            }

            log.RegistraLog(IDUsuarioOperador, System.DateTime.Now, adpFrequencia.Adapter.UpdateCommand.ToString(), "Cadastro de Justificativa", string.Format("IDFrequencia: {0} TPJustificativa: {1} IDMotivoFalta: {2} IDUsuario: {3} Detalhe: {4}", IDFrequencia, IDTPJustificativa, IDMotivoFalta, IDUsuario, textoJustificativa), "", IDEmpresa);

            return msg;
        }

        public string JustificaFrequenciaDiaUsuario(int IDFrequencia, int IDUsuario, int IDMotivoFalta, string OBS, DateTime DTFrequencia, int? TotalHorasDiaP, int IDTPJustificativa, int IDEmpresa, string OBSJustificativa, int IDUsuarioOperador, int IDVinculoUsuario, int SitJustificativa)
        {
            DataSetPontoFrequencia ds = new DataSetPontoFrequencia();
            DataSetPontoFrequenciaTableAdapters.TBFeriasTableAdapter adpFerias = new DataSetPontoFrequenciaTableAdapters.TBFeriasTableAdapter();
            try
            {

                adpFerias.Connection.Open();
                adpFerias.FillByVerificaFeriasCorrente(ds.TBFerias, DTFrequencia, IDUsuario, IDVinculoUsuario);
                adpFerias.Connection.Close();
                if (ds.TBFerias.Rows.Count > 0)
                    return "Ferias/Licença no perído. Impossível incluir Justificativa.";

            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            if (IDFrequencia == 0)
            {
                adpFrequencia.FillFrequenciaDia(ds.TBFrequencia, DTFrequencia, IDUsuario, IDVinculoUsuario);
                if (ds.TBFrequencia.Rows.Count > 0)
                {
                    IDFrequencia = ds.TBFrequencia[0].IDFrequencia;
                }
            }

            //Prevendo mais de 3 lançamentos do motivo 44 (Esquecimento), se já estiver igual a 3 não deixar realizar a próxima justificativa
            //Por enquanto, direto na rotina, quando houver mais limitações, desenvolver rotina para o mesmo.
            //if (IDMotivoFalta == 76)
            //{
            //if (QuantidadeJustificativaMes(IDMotivoFalta, IDVinculoUsuario, DTFrequencia.Month, DTFrequencia.Year) >= 3)
            //{
            //return "Servidor já utilizou três ou mais vezes a justificativa selecionada! Justificativa não lançada.";
            //}
            //}
            // ---

            try
            {
                //Dados do usuário em questão
                DadosUsuario(ds, IDVinculoUsuario, DTFrequencia);

                switch (RegimePlantonista)
                {
                    case true:
                        #region Regime Plantonista
                        //Aqui as justificativas do regime plantonista.
                        if (IDFrequencia == 0) //Inserção de frequência
                        {
                            switch (IDTPJustificativa)
                            {
                                case 0:
                                    return "Utilize a justificativa de meio período ou integral para plantonistas";
                                    break;
                                case 1:
                                    if (Abono(IDMotivoFalta))
                                    {
                                        if (adpFrequencia.InsertJustificadoUsuario(IDUsuario, IDMotivoFalta, DTFrequencia,
                                            string.Format("{0}: {1}", textoJustificativa, OBS), null, DTFrequencia.Month, DTFrequencia.Year, horaentradamanhaJust, horasaidamanhaJust, horaentradatardeJust,
                                            horasaidamanhaJust, IDTPJustificativa, null, null, IDVinculoUsuario, IDEmpresa, 1) > 0)//(TotalHorasDiaUsuarioPadrao * (3600/2))
                                        {
                                            msg = "Justificativa realizada com sucesso.";
                                        }
                                        else
                                            msg = "Justificativa não realizada. Repita o processo.";
                                    }
                                    else
                                    {
                                        if (adpFrequencia.InsertJustificadoUsuario(IDUsuario, IDMotivoFalta, DTFrequencia, string.Format("{0}: {1}", textoJustificativa, OBS), null, DTFrequencia.Month, DTFrequencia.Year, horaentradamanhaJust, horasaidamanhaJust, horaentradatardeJust, horasaidamanhaJust, IDTPJustificativa, null, null, IDVinculoUsuario, IDEmpresa, 1) > 0)
                                        {
                                            msg = "Justificativa realizada com sucesso.";
                                        }
                                        else
                                            msg = "Justificativa não realizada. Repita o processo.";
                                    }
                                    break;
                                case 2:
                                    if (Abono(IDMotivoFalta))
                                    {
                                        if (adpFrequencia.InsertJustificadoUsuario(IDUsuario, IDMotivoFalta, DTFrequencia, string.Format("{0}: {1}", textoJustificativa, OBS), null, DTFrequencia.Month, DTFrequencia.Year, horaentradamanhaJust, horasaidamanhaJust, horaentradatardeJust, horasaidamanhaJust, IDTPJustificativa, null, null, IDVinculoUsuario, IDEmpresa, 1) > 0)//(TotalHorasDiaUsuarioPadrao * 3600)
                                        {
                                            msg = "Justificativa realizada com sucesso.";
                                        }
                                        else
                                            msg = "Justificativa não realizada. Repita o processo.";
                                        break;
                                    }
                                    else
                                    {
                                        if (adpFrequencia.InsertJustificadoUsuario(IDUsuario, IDMotivoFalta, DTFrequencia, string.Format("{0}: {1}", textoJustificativa, OBS), null, DTFrequencia.Month, DTFrequencia.Year, horaentradamanhaJust, horasaidamanhaJust, horaentradatardeJust, horasaidamanhaJust, IDTPJustificativa, null, null, IDVinculoUsuario, IDEmpresa, 1) > 0)
                                        {
                                            msg = "Justificativa realizada com sucesso.";
                                        }
                                        else
                                            msg = "Justificativa não realizada. Repita o processo.";
                                    }
                                    break;
                            }
                        }
                        else //justificativa de registro realizado. (já há um registro realizado)
                        {
                            if (Abono(IDMotivoFalta))
                            {
                                if (IDTPJustificativa == 2)
                                    adpFrequencia.UpdateJustificaFaltaAtraso(IDUsuario, IDMotivoFalta, string.Format("{0}: {1}", textoJustificativa, OBS), 1, null, null, null, null, null, 3, null, null, SitJustificativa, IDFrequencia, IDUsuario);//(TotalHOrasDiaPadraoUsuario * 3600)
                                else if (IDTPJustificativa == 1)
                                    adpFrequencia.UpdateJustificaFaltaAtraso(IDUsuario, IDMotivoFalta, string.Format("{0}: {1}", textoJustificativa, OBS), 1, null, null, null, null, null, 3, null, null, SitJustificativa, IDFrequencia, IDUsuario);//(TotalHOrasDiaPadraoUsuario * (3600/2))
                            }
                            else
                            {
                                adpFrequencia.UpdateJustificaFaltaAtraso(IDUsuario, IDMotivoFalta, string.Format("{0}: {1}", textoJustificativa, OBS), 1, null, null, null, null, null, 3, null, 0, SitJustificativa, IDFrequencia, IDUsuario);
                            }

                            msg = string.Format("Justificativa realizada com sucesso.");
                        }
                        #endregion
                        break;
                    case false:
                        #region Regime de expediente
                        if (IDFrequencia == 0) //Não há dado inserido na tabela de frequência
                        {
                            try
                            {
                                switch (IDTPJustificativa)
                                {
                                    //Se IDFrequencia = 0 , não há registro de frequência, portanto, permitir a inclusão da justificativa.
                                    case 0: //Justificativa de um registro 
                                        //Define variáveis para o insert;
                                        ParametrosOperacao(OBSJustificativa, DTFrequencia);
                                        if (IDRegimeHoraJustificativa != 5 && TotalHorasDiaUsuarioPadrao == 4)
                                            TotalHorasDiaUsuarioPadrao = (TotalHorasDiaUsuarioPadrao * 3600) + 1800;
                                        else
                                            TotalHorasDiaUsuarioPadrao = (TotalHorasDiaUsuarioPadrao * 3600);

                                        if (adpFrequencia.InsertJustificadoUsuario(IDUsuario, IDMotivoFalta, DTFrequencia,
                                            string.Format("{0}: {1}", textoJustificativa, OBS), null, DTFrequencia.Month, DTFrequencia.Year,
                                            horaentradamanhaJust, horasaidamanhaJust, horaentradatardeJust, horasaidamanhaJust, IDTPJustificativa, null, null, IDVinculoUsuario, IDEmpresa, 1) > 0)
                                            msg = "Justificativa realizada com sucesso.";
                                        else
                                            msg = "Justificativa não realizada, Repita o processo.";
                                        break;
                                    case 1: //Justificativa de um período

                                        if (Abono(IDMotivoFalta))///Se IDMotivoFalta permite abonar ---
                                        {
                                            if (IDRegimeHoraJustificativa != 5 && TotalHorasDiaUsuarioPadrao == 4)

                                                TotalHorasDiaP = ((TotalHorasDiaUsuarioPadrao / 2) * 3600) + 1800;
                                            else
                                                TotalHorasDiaP = (TotalHorasDiaUsuarioPadrao / 2) * 3600;
                                        }
                                        else// Senão
                                            TotalHorasDiaP = 0;

                                        if (OBSJustificativa == "0")
                                            ParametrosOperacao("Mat", DTFrequencia); //Se Matutino, Insere os padrões da manhã, senão, tarde!
                                        else
                                            ParametrosOperacao("Vesp", DTFrequencia);

                                        if (adpFrequencia.InsertJustificadoUsuario(IDUsuario, IDMotivoFalta, DTFrequencia, string.Format("{0}: {1}", textoJustificativa, OBS), null, DTFrequencia.Month, DTFrequencia.Year, horaentradamanhaJust, horasaidamanhaJust, horaentradatardeJust, horasaidatardeJust, IDTPJustificativa, OBSJustificativa, null, IDVinculoUsuario, IDEmpresa, 1) > 0)
                                            msg = "Justificativa realizada com sucesso.";
                                        else
                                            msg = "Justificativa não realizada, repita o processo.";
                                        break;

                                    case 2: // Justificativa de período integral

                                        if (Abono(IDMotivoFalta))
                                        {
                                            if (IDRegimeHoraJustificativa != 5 && TotalHorasDiaUsuarioPadrao == 4)
                                                TotalHorasDiaP = (TotalHorasDiaUsuarioPadrao * 3600) + 1800;
                                            else
                                                TotalHorasDiaP = TotalHorasDiaUsuarioPadrao * 3600;
                                        }
                                        else
                                            TotalHorasDiaP = TotalExistenteSegudos;

                                        if (adpFrequencia.InsertJustificadoUsuario(IDUsuario, IDMotivoFalta, DTFrequencia,
                                            string.Format("{0}: {1}", textoJustificativa, OBS), null, DTFrequencia.Month, DTFrequencia.Year, Convert.ToDateTime(string.Format("{0} {1}", DTFrequencia.ToShortDateString(), HoraPadraoEntradaManha)),
                                            Convert.ToDateTime(string.Format("{0} {1}", DTFrequencia.ToShortDateString(), HoraPadraoSaidaManha)), Convert.ToDateTime(string.Format("{0} {1}", DTFrequencia.ToShortDateString(), HoraPadraoEntradaTarde)), Convert.ToDateTime(string.Format("{0} {1}", DTFrequencia.ToShortDateString(), HoraPadraoSaidTarde)), IDTPJustificativa, null, null, IDVinculoUsuario, IDEmpresa, 1) > 0)
                                            msg = "Justificativa realizada com sucesso.";
                                        else
                                            msg = "Justificativa não realizada, repita o processo.";
                                        break;
                                }
                            }
                            catch (Exception ex)
                            {
                                ex.ToString();
                            }
                        }
                        else
                        {
                            try
                            {
                                if (IDTPJustificativa == 1 && (OBSJustificativa == "0"))
                                    OBSJustificativa = "Mat";
                                else if (IDTPJustificativa == 1 && (OBSJustificativa == "1"))
                                    OBSJustificativa = "Vesp";

                                if (PermiteJustificativa(ds, IDFrequencia, IDUsuario, IDTPJustificativa, OBSJustificativa, DTFrequencia))
                                {
                                    ParametrosOperacao(OBSJustificativa, DTFrequencia);

                                    //Verificar qual dos registros está nulo - algum estando nulo preencher com os valores padrões do usuário
                                    if (!horaentradaManha.HasValue)
                                        horaentradaManha = horaentradamanhaJust;
                                    if (!horasaidamanha.HasValue)
                                        horasaidamanha = horasaidamanhaJust;
                                    if (!horaentradatarde.HasValue)
                                        horaentradatarde = horaentradatardeJust;
                                    if (!horasaidatarde.HasValue)
                                        horasaidatarde = horasaidatardeJust;

                                    switch (IDTPJustificativa)
                                    {
                                        case 0:
                                            TotalHorasDiaP = TotalHorasDiaJust(horaentradaManha, horasaidamanha, horaentradatarde, horasaidatarde, DTFrequencia, "", IDMotivoFalta);
                                            break;
                                        case 1:
                                            TotalHorasDiaP = (TotalHorasDiaJust(horaentradaManha, horasaidamanha, horaentradatarde, horasaidatarde, DTFrequencia, "Mat", IDMotivoFalta));
                                            break;
                                        case 2:
                                            textoJustificativa = "Justificativa de período integral.";
                                            if (Abono(IDMotivoFalta))
                                            {
                                                if (TotalHorasDiaUsuarioPadrao == 4 && IDRegimeHoraJustificativa != 5)
                                                    TotalHorasDiaP = (TotalHorasDiaUsuarioPadrao * 3600) + 1800;
                                                else
                                                    TotalHorasDiaP = TotalHOrasDiaPadraoUsuario * 3600;
                                            }
                                            else
                                                TotalHorasDiaP = TotalExistenteSegudos;
                                            break;
                                    }
                                    adpFrequencia.UpdateJustificaFaltaAtraso(IDUsuario, IDMotivoFalta, string.Format("{0}: {1}", textoJustificativa, OBS),
                                        1, null, horaentradamanhaJust, horasaidamanhaJust, horaentradatardeJust, horasaidatardeJust, IDTPJustificativa,
                                        OBSJustificativa, null, SitJustificativa, IDFrequencia, IDUsuario);
                                    msg = string.Format("Justificativa realizada com sucesso.");
                                }

                            }
                            catch (Exception ex)
                            {
                                ex.ToString();
                            }
                        }
                        #endregion
                        break;
                }

            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                ErroOperacao EO = new ErroOperacao();
                msg = EO.RetornaErroOperacao(ex);
            }
            catch (Exception ex)
            {
                msg = string.Format("Houve falha na operação, tente novamente.");
                ex.ToString();
            }

            log.RegistraLog(IDUsuarioOperador, System.DateTime.Now, adpFrequencia.Adapter.UpdateCommand.ToString(), "Cadastro de Justificativa", string.Format("IDFrequencia: {0} TPJustificativa: {1} IDMotivoFalta: {2} IDUsuario: {3} Detalhe: {4}", IDFrequencia, IDTPJustificativa, IDMotivoFalta, IDUsuario, textoJustificativa), "", IDEmpresa);

            return msg;
        }

        private void ParametrosOperacao(string OBSJustificativa, DateTime DTFrequencia)
        {
            //Variável do texto de justificativa.
            textoJustificativa = string.Empty;
            switch (OBSJustificativa)
            {
                case "0": //Entrada da Manhã
                    horaentradamanhaJust = Convert.ToDateTime(string.Format("{0} {1}:00.000", DTFrequencia.ToShortDateString(), HoraPadraoEntradaManha));
                    horasaidamanhaJust = null;
                    horaentradatardeJust = null;
                    horasaidatardeJust = null;
                    textoJustificativa = string.Format("Entrada da Manhã Justificada: {0}", horaentradamanhaJust.ToString());
                    break;
                case "1"://Saida da manhã
                    horaentradamanhaJust = null;
                    horasaidamanhaJust = Convert.ToDateTime(string.Format("{0} {1}", DTFrequencia.ToShortDateString(), HoraPadraoSaidaManha));
                    horaentradatardeJust = null;
                    horasaidatardeJust = null;
                    textoJustificativa = string.Format("Saida da Manhã Justificada: {0}", horasaidamanhaJust.ToString());
                    break;
                case "2"://Entrada Tarde
                    horaentradamanhaJust = null;
                    horasaidamanhaJust = null;
                    horaentradatardeJust = Convert.ToDateTime(string.Format("{0} {1}", DTFrequencia.ToShortDateString(), HoraPadraoEntradaTarde));
                    horasaidatardeJust = null;
                    textoJustificativa = string.Format("Entrada da Tarde Justificada: {0}", horaentradatardeJust.ToString());
                    break;
                case "3"://Saida da Tarde
                    horaentradamanhaJust = null;
                    horasaidamanhaJust = null;
                    horaentradatardeJust = null;
                    horasaidatardeJust = Convert.ToDateTime(string.Format("{0} {1}", DTFrequencia.ToShortDateString(), HoraPadraoSaidTarde));
                    textoJustificativa = string.Format("Saida da Tarde Justificada: {0}", horasaidatardeJust.ToString());
                    break;
                case "Mat": //Período matutino
                    horaentradamanhaJust = Convert.ToDateTime(string.Format("{0} {1}", DTFrequencia.ToShortDateString(), HoraPadraoEntradaManha));
                    horasaidamanhaJust = Convert.ToDateTime(string.Format("{0} {1}", DTFrequencia.ToShortDateString(), HoraPadraoSaidaManha));
                    horaentradatardeJust = null;
                    horasaidatardeJust = null;
                    textoJustificativa = string.Format("Período Matutino Justificado: {0} - {1}", horaentradamanhaJust.ToString(), horasaidamanhaJust.ToString());
                    break;
                case "Vesp": //Período vespertino
                    horaentradamanhaJust = null;
                    horasaidamanhaJust = null;
                    horaentradatardeJust = Convert.ToDateTime(string.Format("{0} {1}", DTFrequencia.ToShortDateString(), HoraPadraoEntradaTarde));
                    horasaidatardeJust = Convert.ToDateTime(string.Format("{0} {1}", DTFrequencia.ToShortDateString(), HoraPadraoSaidTarde));
                    textoJustificativa = string.Format("Período Vespertino Justificado: {0} - {1}", horaentradatardeJust.ToString(), horasaidatardeJust.ToString());
                    break;
            }
        }

        public string LancaFalta(int IDUsuario, DateTime DTFalta, int IDMotivoFalta, string OBS, int IDEmpresa, long IDVinculoUsuario)
        {
            int Deixa = ProcuraFeriado(DTFalta, IDUsuario, IDEmpresa, IDVinculoUsuario);

            try
            {
                if (Deixa == 0)
                {
                    adpFrequencia.Insert(IDUsuario, IDMotivoFalta, DTFalta, null, null, null, null, OBS, null, DTFalta.Month, System.DateTime.Now.Year, IDEmpresa, IDVinculoUsuario);
                    msg = "Falta(s) lançada(s) com sucesso!";
                }
                else if (Deixa == 2)
                {
                    msg = "Impossível incluir a Falta, há um Ponto Facultativo na data selecionada.";
                }
                else if (Deixa == 1)
                {
                    msg = "Impossível incluir a Falta, há um feriado na data selecionada.";
                }
                else if (Deixa == 3)
                {
                    msg = "Há um ponto batido ou uma justificativa realizada nesta data. Impossível lançar falta.";
                }
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                ErroOperacao EO = new ErroOperacao();
                msg = EO.RetornaErroOperacao(ex);
            }
            catch (Exception ex)
            {
                msg = "Houve falha na tentativa de incluir o Registro, tente novamente.";
                ex.ToString();
            }
            return msg;
        }

        public int ProcuraFeriado(DateTime DTFalta, int IDUsuario, int IDEmpresa, long IDVinculoUsuario)
        {
            DataSetPontoFrequenciaTableAdapters.TBFeriadoPontoFacultativoTableAdapter adpFeriadoPonto = new DataSetPontoFrequenciaTableAdapters.TBFeriadoPontoFacultativoTableAdapter();
            DataSetPontoFrequencia.TBFeriadoPontoFacultativoDataTable TBFeriadoPonto = new DataSetPontoFrequencia.TBFeriadoPontoFacultativoDataTable();

            try
            {
                adpFeriadoPonto.FillByFeriadoPonto(TBFeriadoPonto, (Convert.ToString(DTFalta.Day)) + "/" + (Convert.ToString(DTFalta.Month)), IDEmpresa); //Para Feriados 

                if (TBFeriadoPonto.Rows.Count > 0)
                {
                    if (TBFeriadoPonto[0].IDTPFeriadoPontoFacultativo != 4)
                        data = 1; //Há um Feriado
                    else
                    {
                        adpFeriadoPonto.FillByFeriadoPontoFacultativo(TBFeriadoPonto, (Convert.ToString(DTFalta.Day)) + "/" + Convert.ToString(DTFalta.Month) + "/" + Convert.ToString(DTFalta.Year), IDEmpresa); //Para Pontos Facultativos

                        if (TBFeriadoPonto.Rows.Count > 0)
                        {
                            data = 2; //Há um ponto Facultativo
                        }
                        else
                        {
                            data = 0; // Não há feriado nem ponto facultativo;
                        }
                    }
                }
                else
                {
                    adpFeriadoPonto.FillByFeriadoPontoFacultativo(TBFeriadoPonto, (Convert.ToString(DTFalta.Day)) + "/" + Convert.ToString(DTFalta.Month) + "/" + Convert.ToString(DTFalta.Year), IDEmpresa); //Para Pontos Facultativos

                    if (TBFeriadoPonto.Rows.Count > 0)
                    {
                        data = 2; //Há um ponto Facultativo
                    }
                    else
                    {
                        data = 0; // Não há feriado nem ponto facultativo;
                    }
                }
                try
                {
                    adpFrequencia.FillFrequenciaDiaIDempresa(TBFrequencia, DTFalta, IDUsuario, IDEmpresa, IDVinculoUsuario);
                    if (TBFrequencia.Rows.Count > 0)
                    {
                        data = 3; //Há ponto batido para o dia
                    }
                    else
                    {
                        data = 0; //Não há ponto batido para o dia
                    }
                }
                catch (Exception ex)
                {
                    ex.ToString();
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
            return data;
        }

        public string GeraFolhaFrequencia(int IDUsuario, int IDSetor, int IDMesReferencia, int Ano, int IDEmpresa, int IDVinculoUsuario)
        {
            //Rotina Para Geração da Folha de Frequência do Ano Corrente e Mes Referencia

            DateTime PrimeiroDiaMes = new DateTime(Ano, IDMesReferencia, 01); //Variaveis Manipulação de dias
            DateTime PrimeiroDiaProximoMes = PrimeiroDiaMes.AddMonths(1);
            DateTime UltimoDiaMes = PrimeiroDiaProximoMes.AddDays(-1);
            DateTime Dias = PrimeiroDiaMes;

            DataSetPontoFrequencia.vwHorasDataTable vwHoras = new DataSetPontoFrequencia.vwHorasDataTable();
            DataSetPontoFrequenciaTableAdapters.vwHorasTableAdapter adpHoras = new DataSetPontoFrequenciaTableAdapters.vwHorasTableAdapter();

            DataSetPontoFrequencia.TBEmpresaDataTable TBEmpresa = new DataSetPontoFrequencia.TBEmpresaDataTable();
            DataSetPontoFrequenciaTableAdapters.TBEmpresaTableAdapter adpEmpresa = new DataSetPontoFrequenciaTableAdapters.TBEmpresaTableAdapter();

            adpEmpresa.FillByIDEmpresa(TBEmpresa, IDEmpresa);

            adpHoras.Fill(vwHoras);

            DataSetPontoFrequencia.TBFeriadoPontoFacultativoDataTable TBFeriadoPonto = new DataSetPontoFrequencia.TBFeriadoPontoFacultativoDataTable();
            DataSetPontoFrequenciaTableAdapters.TBFeriadoPontoFacultativoTableAdapter adpFeriadoPonto = new DataSetPontoFrequenciaTableAdapters.TBFeriadoPontoFacultativoTableAdapter();
            DataSetPontoFrequencia.TBFeriasDataTable TBFerias = new DataSetPontoFrequencia.TBFeriasDataTable();
            DataSetPontoFrequenciaTableAdapters.TBFeriasTableAdapter adpFerias = new DataSetPontoFrequenciaTableAdapters.TBFeriasTableAdapter();
            DateTime dt = System.DateTime.Now;

            while (Dias <= UltimoDiaMes)
            {
                adpFerias.FillFeriasFolhaFrequencia(TBFerias, IDUsuario, Dias);
                adpFrequencia.FillFrequenciaDia(TBFrequencia, Dias, IDUsuario, IDVinculoUsuario);

                if (TBFerias.Rows.Count > 0 && TBFrequencia.Rows.Count == 0) // Caso Tenha Férias, publica na folha de Frequência
                {
                    int IDTipoFerias = TBFerias[0].IDTPFerias;

                    if (IDTipoFerias == 1)
                    {
                        OBS = "014 - FÉRIAS (ART. 129, I, LC 04/90)";
                        IDMotivoFalta = 48;
                    }
                    if (IDTipoFerias == 2)
                    {
                        OBS = "LICENÇA PRÊMIO";
                        IDMotivoFalta = 52;
                    }
                    if (IDTipoFerias == 3)
                    {
                        OBS = "LICENÇA MATERNIDADE";
                        IDMotivoFalta = 50;
                    }
                    if (IDTipoFerias == 4)
                    {
                        OBS = "LICENÇA TRATAMENTO DE SAÚDE";
                        IDMotivoFalta = 51;
                    }

                    dt = Convert.ToDateTime(Dias.Day + "/" + IDMesReferencia + "/" + Ano);

                    if (vwHoras[0].Horas.Date > dt.Date) // Se menor permite a inserção do dia no relatório.
                        adpFrequencia.Insert(IDUsuario, IDMotivoFalta, Dias, null, null, null, null, OBS, null, IDMesReferencia, Ano, IDMesReferencia, IDVinculoUsuario);
                }
                else
                {
                    try
                    {
                        adpFeriadoPonto.FillFeriadoFolhaFrequencia(TBFeriadoPonto, Convert.ToString(Dias.Day) + "/" + Convert.ToString(Dias.Month), IDEmpresa);
                    }
                    catch (Exception ex)
                    {
                        ex.ToString();
                        msg = "Houve falha na tentativa de gerar a folha de Frequência. Tente Novamente.";
                    }

                    if (TBFrequencia.Rows.Count == 0 || TBFeriadoPonto.Rows.Count > 0)
                    {
                        if (Dias.DayOfWeek.ToString() == "Saturday" && !TBEmpresa[0].TrabalhaSabado && TBFrequencia.Rows.Count == 0)
                        {
                            try
                            {

                                if (vwHoras[0].Horas.Date > dt.Date) // Se menor permite a inserção do dia no relatório.
                                {
                                    adpFrequencia.Insert(IDUsuario, 7, Dias, null, null, null, null, "Sábado", null, IDMesReferencia, Ano, IDEmpresa, IDVinculoUsuario);
                                    msg = "Folha gerada com sucesso.";
                                }
                            }
                            catch (Exception ex)
                            {
                                ex.ToString();
                                msg = "Houve falha na tentativa de gerar a folha de Frequência. Tente Novamente.";
                            }
                        }
                        else if (Dias.DayOfWeek.ToString() == "Saturday" && TBEmpresa[0].TrabalhaSabado)
                        {
                            try
                            {
                                if (vwHoras[0].Horas.Date > dt.Date) // Se menor permite a inserção do dia no relatório.
                                {
                                    adpFrequencia.Insert(IDUsuario, 10, Dias, null, null, null, null, "Falta não justificada", null, IDMesReferencia, TBhoras[0].Horas.Year, IDEmpresa, IDVinculoUsuario);
                                    msg = "Folha gerada com sucesso.";
                                }
                            }
                            catch (System.Data.OleDb.OleDbException ex)
                            {
                                ErroOperacao EO = new ErroOperacao();
                                msg = EO.RetornaErroOperacao(ex);
                            }
                            catch (Exception ex)
                            {
                                ex.ToString();
                            }
                        }
                        else if (Dias.DayOfWeek.ToString() == "Saturday" && !TBEmpresa[0].TrabalhaSabado)
                        {
                            adpFrequencia.Insert(IDUsuario, 7, Dias, null, null, null, null, "Sábado.", null, IDMesReferencia, TBhoras[0].Horas.Year, IDEmpresa, IDVinculoUsuario);
                            msg = "Folha gerada com sucesso.";
                        }
                        else if (Dias.DayOfWeek.ToString() == "Sunday")
                        {
                            try
                            {
                                adpFrequencia.Insert(IDUsuario, 8, Dias, null, null, null, null, "Domingo", null, IDMesReferencia, Ano, IDEmpresa, IDVinculoUsuario);
                                msg = "Folha gerada com sucesso.";
                            }
                            catch (System.Data.OleDb.OleDbException ex)
                            {
                                ErroOperacao EO = new ErroOperacao();
                                msg = EO.RetornaErroOperacao(ex);
                            }
                            catch (Exception ex)
                            {
                                ex.ToString();
                                msg = "Houve falha na tentativa de gerar a folha de Frequência. Tente Novamente.";
                            }
                        }
                        else if (TBFeriadoPonto.Rows.Count > 0 && TBFrequencia.Rows.Count == 0)
                        {
                            try
                            {
                                adpFrequencia.Insert(IDUsuario, 9, Dias, null, null, null, null, TBFeriadoPonto[0].DSFeriadoPontoFacultativo.ToString().Trim(), null, IDMesReferencia, Ano, IDEmpresa, IDVinculoUsuario);
                                msg = "Folha gerada com sucesso.";
                            }
                            catch (System.Data.OleDb.OleDbException ex)
                            {
                                ErroOperacao EO = new ErroOperacao();
                                msg = EO.RetornaErroOperacao(ex);
                            }
                            catch (Exception ex)
                            {
                                ex.ToString();
                                msg = "Houve falha na tentativa de gerar a folha de Frequência. Tente Novamente.";
                            }
                        }
                        else if ((Dias.DayOfWeek.ToString() != "Saturday") && (Dias.DayOfWeek.ToString() != "Sunday") && (TBFeriadoPonto.Rows.Count == 0) && (TBFrequencia.Rows.Count == 0))
                        {
                            int MesAnterior = Dias.Month - System.DateTime.Now.Month; //No mês corrente, não permitir gerar Falta não Justificada.
                            int AnoAnterior = Dias.Year - System.DateTime.Now.Year; // Para refinar pesquisa, no ano corrente tb não gera folha se MesAnterior difere de zero.
                            if (MesAnterior != 0 && AnoAnterior != 0) // - Se Subtração igual a zero, está no mês corrente.
                            {
                                try
                                {

                                    adpFrequencia.Insert(IDUsuario, 10, Dias, null, null, null, null, "Falta não justificada", null, IDMesReferencia, Ano, IDEmpresa, IDVinculoUsuario);
                                    msg = "Folha gerada com sucesso.";
                                }
                                catch (System.Data.OleDb.OleDbException ex)
                                {
                                    ErroOperacao EO = new ErroOperacao();
                                    msg = EO.RetornaErroOperacao(ex);
                                }
                                catch (Exception ex)
                                {
                                    ex.ToString();
                                    msg = "Falha ao tentar gerar folha de frequência. Tente novamente.";
                                }
                            }
                            else if (AnoAnterior == 0)
                            {
                                dt = Convert.ToDateTime(Dias.Day + "/" + IDMesReferencia + "/" + Ano);

                                if (vwHoras[0].Horas.Date > dt.Date)
                                {
                                    try
                                    {
                                        adpFrequencia.Insert(IDUsuario, 10, Dias, null, null, null, null, "Falta não justificada", null, IDMesReferencia, Ano, IDEmpresa, IDVinculoUsuario);
                                        msg = "Folha gerada com sucesso.";
                                    }
                                    catch (System.Data.OleDb.OleDbException ex)
                                    {
                                        ErroOperacao EO = new ErroOperacao();
                                        msg = EO.RetornaErroOperacao(ex);
                                    }
                                    catch (Exception ex)
                                    {
                                        ex.ToString();
                                    }
                                }
                            }
                        }
                    }
                }
                Dias = Dias.AddDays(1);
            }
            return msg;
        }

        //Retorna Total De horas Válidas do mÊs corrente
        public string TOTHORASMES
        {
            get
            {
                return TotHorasMes;
            }
        }

        //Retorna Total de horas do usuário no MÊs corrente

        public string TOTHORASUSUARIO
        {
            get
            {
                return TotHorasUsuario;
            }
        }
        //Retorna Total de Horas Válidas no Mês
        //public int TotalHorasValidasMes
        //{
        //get
        //{
        //    return totalMesPercent;
        //}
        //}
        //Retonar total de horas do usuário no Mês
        // public int TotalHorasUsuarioMes
        //{
        //get
        //{
        //return TotalUsuarioPerct;
        //}
        // }
        public void TotalHorasMesUsuario(int IDUsuario, int MesReferencia, int Ano, int IDempresa, DataSetPontoFrequencia ds)
        {

            DataSetPontoFrequenciaTableAdapters.vwTotalHorasMesTableAdapter adpTotalHorasMes = new DataSetPontoFrequenciaTableAdapters.vwTotalHorasMesTableAdapter();

            DataSetPontoFrequenciaTableAdapters.TBFeriadoPontoFacultativoTableAdapter adpFeriadoPonto = new DataSetPontoFrequenciaTableAdapters.TBFeriadoPontoFacultativoTableAdapter();

            adpEmpresa.FillByIDEmpresa(TBEmpresa, IDempresa);

            DateTime PrimeiroDiaMes = new DateTime(Ano, MesReferencia, 01); //Variaveis Manipulação de dias
            DateTime PrimeiroDiaProximoMes = PrimeiroDiaMes.AddMonths(1);
            DateTime UltimoDiaMes = PrimeiroDiaProximoMes.AddDays(-1);
            DateTime Dias = PrimeiroDiaMes;

            adpTotalHorasMes.Fill(ds.vwTotalHorasMes, IDUsuario, MesReferencia, Ano);

            if (ds.vwTotalHorasMes.Rows.Count > 0) //Total de Horas do Usuário no mês corrente.
            {
                if (!ds.vwTotalHorasMes[0].IsHorasNull())
                {
                    if (ds.vwTotalHorasMes[0].Horas.ToString().Length == 1)
                    {
                        horas = "0" + ds.vwTotalHorasMes[0].Horas.ToString();
                    }
                    else
                    {
                        horas = ds.vwTotalHorasMes[0].Horas.ToString();
                    }

                    if (ds.vwTotalHorasMes[0].Minutos.ToString().Length == 1)
                    {
                        minutos = "0" + ds.vwTotalHorasMes[0].Minutos.ToString();
                    }
                    else
                    {
                        minutos = ds.vwTotalHorasMes[0].Minutos.ToString();
                    }
                    if (ds.vwTotalHorasMes[0].Segundos.ToString().Length == 1)
                    {
                        segudos = "0" + ds.vwTotalHorasMes[0].Segundos.ToString();
                    }
                    else
                    {
                        segudos = ds.vwTotalHorasMes[0].Segundos.ToString();
                    }

                    //TotHorasUsuario = horas + ":" + minutos + ":" + segudos;
                }
                else
                {
                    TotHorasUsuario = "00:00:00";
                }
            }
        }
        public void TotalHorasMes(int IDUsuario, int MesReferencia, int Ano, int IDempresa)
        {

            DataSetPontoFrequencia.vwTotalHorasMesDataTable vwTotalHorasMes = new DataSetPontoFrequencia.vwTotalHorasMesDataTable();
            DataSetPontoFrequenciaTableAdapters.vwTotalHorasMesTableAdapter adpTotalHorasMes = new DataSetPontoFrequenciaTableAdapters.vwTotalHorasMesTableAdapter();

            DataSetPontoFrequencia.TBFeriadoPontoFacultativoDataTable TBFeriadoPonto = new DataSetPontoFrequencia.TBFeriadoPontoFacultativoDataTable();
            DataSetPontoFrequenciaTableAdapters.TBFeriadoPontoFacultativoTableAdapter adpFeriadoPonto = new DataSetPontoFrequenciaTableAdapters.TBFeriadoPontoFacultativoTableAdapter();

            adpEmpresa.FillByIDEmpresa(TBEmpresa, IDempresa);

            DateTime PrimeiroDiaMes = new DateTime(Ano, MesReferencia, 01); //Variaveis Manipulação de dias
            DateTime PrimeiroDiaProximoMes = PrimeiroDiaMes.AddMonths(1);
            DateTime UltimoDiaMes = PrimeiroDiaProximoMes.AddDays(-1);
            DateTime Dias = PrimeiroDiaMes;

            adpTotalHorasMes.Fill(vwTotalHorasMes, IDUsuario, MesReferencia, Ano);

            if (vwTotalHorasMes.Rows.Count > 0) //Total de Horas do Usuário no mês corrente.
            {
                if (!vwTotalHorasMes[0].IsHorasNull())
                {
                    if (vwTotalHorasMes[0].Horas.ToString().Length == 1)
                    {
                        horas = "0" + vwTotalHorasMes[0].Horas.ToString();
                    }
                    else
                    {
                        horas = vwTotalHorasMes[0].Horas.ToString();
                    }
                    if (vwTotalHorasMes[0].Minutos.ToString().Length == 1)
                    {
                        minutos = "0" + vwTotalHorasMes[0].Minutos.ToString();
                    }
                    else
                    {
                        minutos = vwTotalHorasMes[0].Minutos.ToString();
                    }
                    if (vwTotalHorasMes[0].Segundos.ToString().Length == 1)
                    {
                        segudos = "0" + vwTotalHorasMes[0].Segundos.ToString();
                    }
                    else
                    {
                        segudos = vwTotalHorasMes[0].Segundos.ToString();
                    }
                    TotHorasUsuario = horas + ":" + minutos + ":" + segudos;
                }
                else
                {
                    TotHorasUsuario = "00:00:00";
                }
            }
            adpUsuario.FillIDUsuario(TBUsuario, IDUsuario, IDempresa);

            if (TBUsuario.Rows.Count > 0)
            {
                while (Dias <= UltimoDiaMes) //Horas válidas no mês corrente.
                {
                    adpFeriadoPonto.FillFeriadoFolhaFrequencia(TBFeriadoPonto, Convert.ToString(Dias.Day) + "/" + Convert.ToString(Dias.Month), IDempresa);

                    //Esta clausula abaixo, é pra quem não trabalha no sábado.
                    if ((TBFeriadoPonto.Rows.Count == 0) && (Dias.DayOfWeek.ToString() != "Sunday") && Dias.DayOfWeek.ToString() != "Saturday")
                    {
                        if (vwUsuarioGrid[0].TotHorasDiarias >= 8)
                            HorasValidasMes = HorasValidasMes + 8;
                        else
                            HorasValidasMes = HorasValidasMes + 6;
                    }//Abaixo contempla quem trabalha aos sábados.
                    else if ((TBFeriadoPonto.Rows.Count == 0) && (Dias.DayOfWeek.ToString() != "Sunday") && TBEmpresa[0].TrabalhaSabado)
                    {
                        if (vwUsuarioGrid[0].TotHorasDiarias == 8 && Dias.DayOfWeek.ToString() != "Saturday")
                            HorasValidasMes = HorasValidasMes + 8;
                        else if (vwUsuarioGrid[0].TotHorasDiarias == 8 && Dias.DayOfWeek.ToString() == "Saturday")
                            HorasValidasMes = HorasValidasMes + 4; //Se for sábado soma meio período - geralmente 4 horas
                        else if (vwUsuarioGrid[0].TotHorasDiarias == 6)
                            HorasValidasMes = HorasValidasMes + 6;
                    }
                    Dias = Dias.AddDays(1);
                }
                TotHorasMes = HorasValidasMes.ToString();
            }
        }

        protected void LocalRegistro(int IDusuario, int iDSetor, DateTime HoraRegistro, string MSG, int IDempresa)
        {
            MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.TBLocalRegistroTableAdapter adpLocalRegistro = new DataSetPontoFrequenciaTableAdapters.TBLocalRegistroTableAdapter();

            try
            {
                adpLocalRegistro.Insert(IDusuario, IDempresa, iDSetor, HoraRegistro, MSG);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }

        public string PontoEspecial(string login, string SenhaAdmin, int IDEmpresa, DateTime HoraBatida, DataSetPontoFrequencia DS)
        {
            try
            {

                DS.EnforceConstraints = false;

                adpUsuario.FillBySenhaEspecial(DS.TBUsuario, login, SenhaAdmin, IDEmpresa);

                if (DS.TBUsuario.Rows.Count > 0)
                {
                    if (!DS.TBUsuario[0].IsSenhaDigitalNull())
                    {
                        if (DS.TBUsuario[0].SenhaDigital.Trim() == "1")
                        {
                            msg = BaterPonto2(DS.TBUsuario[0].IDUsuario, IDEmpresa, HoraBatida, 0);

                            //Log de registro de ponto web
                            LocalRegistro(DS.TBUsuario[0].IDUsuario, DS.TBUsuario[0].IDSetor, HoraBatida, string.Format("REGISTRO MANUAL: {0}", msg), IDEmpresa);
                        }
                        else
                            msg = string.Format("Servidor não tem permissão para registrar frequência em modo especial.");
                    }
                    else
                    {
                        msg = "Servidor não tem permissão para registro manual.";
                    }
                }
                else
                {
                    msg = "Servidor não localizado.";
                }
            }
            catch (Exception ex)
            {
                msg = "Falha no registro da Frequência. Por favor tente novamente.";
                ex.ToString();
            }
            return msg;
        }

        public string PontoEspecial2(string login, string SenhaAdmin, int IDEmpresa, DateTime HoraBatida, DataSetPontoFrequencia DS, string Matricula)
        {
            try
            {
                DS.EnforceConstraints = false;

                adpUsuario.FillBySenhaEspecial(DS.TBUsuario, login, SenhaAdmin, IDEmpresa);

                //31/07/2018 - Primeiramente, vai entrar com o usuário e senha que segue o padrão da outra.
                //Após, verifica se há uma matricula vinda de fora, se tiver, busca o ID do vinculo pela matricula.

                if (DS.TBUsuario.Rows.Count > 0)
                {
                    //Achou alguém, vê se a matricula não é nula.
                    if (Matricula != string.Empty)
                    {
                        //ALTERAÇÃO REALIZADA EM 27/09/2018
                        PreencheTabela pt = new PreencheTabela();
                        pt.GetVinculoUsuario(DS, Matricula.Trim(), DS.TBUsuario[0].IDUsuario);
                        if (DS.TBVinculoUsuario.Rows.Count > 0)
                        {
                            if (!DS.TBUsuario[0].IsSenhaDigitalNull())
                            {
                                if (DS.TBUsuario[0].SenhaDigital.Trim() == "1")
                                {
                                    DataSetPontoFrequenciaTableAdapters.TBRegimeHoraTableAdapter adpRegime =
                                        new DataSetPontoFrequenciaTableAdapters.TBRegimeHoraTableAdapter();
                                    adpRegime.FillIDRegimeHora(DS.TBRegimeHora, DS.TBVinculoUsuario[0].IDRegimeHora);

                                    if (IDEmpresa != 41)
                                    {
                                        msg = BaterPonto3(IDEmpresa, DS.TBVinculoUsuario[0].IDSetor,
    DS.TBUsuario[0].IDUsuario, HoraBatida, DS.TBVinculoUsuario[0].HoraEntradaManha,
    DS.TBVinculoUsuario[0].HoraSaidaManha, DS.TBVinculoUsuario[0].HoraEntradaTarde,
    DS.TBVinculoUsuario[0].HoraSaidaTarde, DS.TBRegimeHora[0].TotalHoraDia,
    DS.TBUsuario[0].DSUsuario, DS.TBUsuario[0].PrimeiroNome,
    DS.TBRegimeHora[0].RegimePlantonista, DS.TBVinculoUsuario[0].IDVinculoUsuario);
                                    }
                                    else
                                    {
                                        msg = BaterPontoAssistSocial(IDEmpresa, DS.TBVinculoUsuario[0].IDSetor,
    DS.TBUsuario[0].IDUsuario, HoraBatida, DS.TBVinculoUsuario[0].HoraEntradaManha,
    DS.TBVinculoUsuario[0].HoraSaidaManha, DS.TBVinculoUsuario[0].HoraEntradaTarde,
    DS.TBVinculoUsuario[0].HoraSaidaTarde, DS.TBRegimeHora[0].TotalHoraDia,
    DS.TBUsuario[0].DSUsuario, DS.TBUsuario[0].PrimeiroNome,
    DS.TBRegimeHora[0].RegimePlantonista, DS.TBVinculoUsuario[0].IDVinculoUsuario);
                                    }

                                    //msg = BaterPonto2(DS.TBUsuario[0].IDUsuario, IDEmpresa, HoraBatida, IDSetorPE2, IDVinculoPE2);

                                    //Log de registro de ponto web
                                    LocalRegistro(DS.TBUsuario[0].IDUsuario, DS.TBUsuario[0].IDSetor, HoraBatida, string.Format("REGISTRO MANUAL: {0}", msg), IDEmpresa);
                                }
                                else
                                    msg = string.Format("Servidor não tem permissão para registrar frequência em modo especial.");
                            }
                            else
                            {
                                msg = "Servidor não tem permissão para registro manual.";
                            }

                            //IDVinculoPE2 = Convert.ToInt32(DS.TBVinculoUsuario[0].IDVinculoUsuario);
                            //IDSetorPE2 = DS.TBVinculoUsuario[0].IDSetor;
                        }
                        else
                        {
                            return "Matricula não localizada para o login informado. Favor verificar.";
                        }
                    }
                    else
                        return "FAVOR INFORMAR A MATRICULA PARA REALIZAR O REGISTRO.";
                }
                else
                {
                    msg = "Servidor não identificado, verifique usuário e a senha!";
                }
            }
            catch (Exception ex)
            {
                msg = "Falha no registro da Frequência. Por favor tente novamente. " + ex.ToString();
                ex.ToString();
            }
            return msg;
        }

        public string ExcluirFalta(int IDFrequencia, int IDUsuario)
        {
            try
            {
                DataSetPontoFrequenciaTableAdapters.TBFrequenciaJustificativaTableAdapter adpFreqJ = new DataSetPontoFrequenciaTableAdapters.TBFrequenciaJustificativaTableAdapter();
                adpFreqJ.DeleteID(IDFrequencia);

                adpFrequencia.FillExcluirFrequencia(TBFrequencia, IDFrequencia);
                //Se registro não estiver todo zerado, apenas da um update na condição de baixo.
                if (TBFrequencia.Rows.Count > 0)
                {
                    try
                    {
                        adpFrequencia.Delete(IDFrequencia);
                        msg = string.Format("Registro excluído com sucesso.");
                    }
                    catch (System.Data.OleDb.OleDbException ex)
                    {
                        ErroOperacao EO = new ErroOperacao();
                        msg = EO.RetornaErroOperacao(ex);
                    }
                    catch (Exception ex)
                    {
                        ex.ToString();
                    }
                }
                else
                {
                    adpFrequencia.UpdateExcluirJust(IDFrequencia, IDUsuario);
                    msg = string.Format("Registro excluído com sucesso.");
                }
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                ex.ToString();
                msg = string.Format("Houve falha na tentativa de exclusão. Tente novamente.");
            }
            return msg;
        }

        public void ExcluiFeriasJustificativasFrequencia(int IDUsuario, int MesReferencia)
        {
            adpFrequencia.FillFrequenciaFeriasJustificativas(TBFrequencia, IDUsuario, MesReferencia);

            if (TBFrequencia.Rows.Count > 0)
            {
                for (int i = 0; i <= (TBFrequencia.Rows.Count - 1); i++)
                {
                    if ((TBFrequencia[i].IDMotivoFalta == 10) || (TBFrequencia[i].IDMotivoFalta == 15) || (TBFrequencia[i].IDMotivoFalta == 16) || (TBFrequencia[i].IDMotivoFalta == 17))
                        adpFrequencia.Delete(TBFrequencia[i].IDFrequencia);
                }
            }
        }

        public string ExcluirJustificativa(DataSetPontoFrequencia ds, int IDFrequencia, int IDUsuario, int IDEmpresa, DateTime dtFrequencia, int IDUsuarioOperador, long IDVinculoUsuario)
        {
            ds.EnforceConstraints = false;

            adpFeriasFrequencia.FillPeriodoFerias(ds.TBFerias, dtFrequencia, IDUsuario);

            if (ds.TBFerias.Rows.Count > 0)
                return "Servidor em férias, impossível exluir";

            adpFrequencia.FillIDFreqUsuario(ds.TBFrequencia, IDFrequencia, IDUsuario);

            Justificativa.Justificativa jus = new Justificativa.Justificativa();

            if (ds.TBFrequencia.Rows.Count > 0)
            {
                //adpFrequencia.FillExcluirFrequencia(ds.TBFrequencia, IDFrequencia);

                DataSetPontoFrequenciaTableAdapters.TBFrequencia_TEMPTableAdapter adpFreq_TEMP = new DataSetPontoFrequenciaTableAdapters.TBFrequencia_TEMPTableAdapter();

                if (ds.TBFrequencia[0].IsHoraEntraManhaNull() && ds.TBFrequencia[0].IsHoraSaidaManhaNull() && ds.TBFrequencia[0].IsHoraEntradaTardeNull() && ds.TBFrequencia[0].IsHoraSaidaTardeNull())
                {
                    try
                    {
                        adpFrequencia.UpdateExcluirJust(IDFrequencia, IDUsuario);
                        jus.LogExcluirJustificativa(IDFrequencia, IDUsuario);
                        DataSetPontoFrequenciaTableAdapters.TBFrequenciaJustificativaTableAdapter adpFreqJ = new DataSetPontoFrequenciaTableAdapters.TBFrequenciaJustificativaTableAdapter();
                        //adpFreqJ.DeleteID(IDFrequencia);

                        //adpFrequencia.Delete(IDFrequencia);
                        //adpFreq_TEMP.DeleteIDFrequencia(IDFrequencia);
                        msg = string.Format("Justificativa excluída com sucesso.");
                    }
                    catch (Exception ex)
                    {
                        ex.ToString();
                        msg = string.Format("Houve falha ao tentar excluir justificativa. Contate o adminisrtador informando o código 1.");
                    }
                }
                else
                {
                    try
                    {
                        adpFrequencia.UpdateExcluirJust(IDFrequencia, IDUsuario);
                        jus.LogExcluirJustificativa(IDFrequencia, IDUsuario);
                        msg = string.Format("Justificativa excluída com sucesso.");

                        //Total Horas
                        DateTime? Entrada1, Saida1, Entrada2, Saida2;

                        if (!ds.TBFrequencia[0].IsHoraEntraManhaNull())
                            Entrada1 = ds.TBFrequencia[0].HoraEntraManha;
                        else
                            Entrada1 = null;

                        if (!ds.TBFrequencia[0].IsHoraSaidaManhaNull())
                            Saida1 = ds.TBFrequencia[0].HoraSaidaManha;
                        else
                            Saida1 = null;

                        if (!ds.TBFrequencia[0].IsHoraEntradaTardeNull())
                            Entrada2 = ds.TBFrequencia[0].HoraEntradaTarde;
                        else
                            Entrada2 = null;

                        if (!ds.TBFrequencia[0].IsHoraSaidaTardeNull())
                            Saida2 = ds.TBFrequencia[0].HoraSaidaTarde;
                        else
                            Saida2 = null;

                        adpUsuario.FillIDUsuario(TBUsuario, IDUsuario, IDEmpresa);

                        adpvwFrequenciaDia.FillIDVinculoUsuario(vwFrequenciaDia, IDVinculoUsuario, ds.TBFrequencia[0].DTFrequencia);
                        //adpFrequencia.FillFrequenciaDia(TBFrequencia, ds.TBFrequencia[0].DTFrequencia, IDUsuario); //Verifica se há ponto registrado no dia

                        adpFrequencia.UpdateTotHoras(TotalHorasDia(Entrada1, Saida1, Entrada2, Saida2, "Tarde", ds.TBFrequencia[0].DTFrequencia), OBS, null, IDUsuario, IDFrequencia);
                    }
                    catch (System.Data.OleDb.OleDbException ex)
                    {
                        msg = string.Format("Houve falha ao tentar excluir justificativa. Contate o adminisrtador informando o código 1.");
                    }
                    catch (System.Exception ex)
                    {
                        msg = string.Format("Houve falha ao tentar excluir justificativa. Contate o adminisrtador informando o código 2.");
                    }
                }

                log.RegistraLog(IDUsuarioOperador, System.DateTime.Now, adpFrequencia.Adapter.UpdateCommand.ToString(), "Exclusão de Justificativa", string.Format("IDFrequencia: {0} IDMotivoFalta: {1} IDUsuario: {2} Detalhe: {3}", IDFrequencia, IDMotivoFalta, IDUsuario, textoJustificativa), "", IDEmpresa);
            }
            return msg;
        }

        public string ManutencaoFrequenciaFeriasLicenca(int IDusuario, int IDTPFerias, DateTime PrimeiroDia, DateTime UltimoDia, string Operacao, DateTime DTInicialAlteracao, DateTime DTFinalAlteracao, int IDEmpresa, long IDVinculoUsuario)
        {
            int IDTipoFerias = IDTPFerias;
            string totalHorasDiarias = string.Empty;

            DataSetPontoFrequenciaTableAdapters.TBFrequenciaTableAdapter adpFReq = new DataSetPontoFrequenciaTableAdapters.TBFrequenciaTableAdapter();
            DataSetPontoFrequencia.TBFrequenciaDataTable Tbfreq = new DataSetPontoFrequencia.TBFrequenciaDataTable();

            DataSetPontoFrequencia.TBFrequencia_TEMPDataTable TBFreq_TEMP = new DataSetPontoFrequencia.TBFrequencia_TEMPDataTable();
            DataSetPontoFrequenciaTableAdapters.TBFrequencia_TEMPTableAdapter adpFreq_TEMP = new DataSetPontoFrequenciaTableAdapters.TBFrequencia_TEMPTableAdapter();

            DataSetPontoFrequencia.vwVinculoUsuarioDataTable vwVinculoUsuario = new DataSetPontoFrequencia.vwVinculoUsuarioDataTable();
            DataSetPontoFrequenciaTableAdapters.vwVinculoUsuarioTableAdapter adpvwVinculousuario = new DataSetPontoFrequenciaTableAdapters.vwVinculoUsuarioTableAdapter();

            adpvwVinculousuario.FillIDVinculo(vwVinculoUsuario, IDVinculoUsuario);

            //Total de horas abonadas por dia.
            if (vwVinculoUsuario.Rows.Count > 0)
            {
                totalHorasDiarias = string.Format("0{0}:00:00.000", vwVinculoUsuario[0].TotalHoraDia);
            }

            //Prefeitura de Cuiabá.

            if (IDTipoFerias == 65)
            {
                OBS = "ABORTO ESPONTÂNEO/AUTORIZADO JUDICIALMENTE";
                IDMotivoFalta = 165;
            }
            if (IDTipoFerias == 47)
            {
                OBS = "ACOMPANHAMENTO DE DOENTE FAMILIAR REMUNERADO (ATÉ 90 DIAS)";
                IDMotivoFalta = 161;
            }
            if (IDTipoFerias == 43)
            {
                OBS = "AFASTAMENTO MÉDICO (15 DIAS PELA EMPRESA)";
                IDMotivoFalta = 162;
            }
            if (IDTipoFerias == 77)
            {
                OBS = "AFASTAMENTO PARA ATIVIDADE POLITICA COM ONUS";
                IDMotivoFalta = 163;
            }
            if (IDTipoFerias == 62)
            {
                OBS = "AFASTAMENTO PARA ESTUDO/MISSÃO EM OUTRO MUNICÍPIO NÃO LIMÍTROFE OU NO EXTERIOR";
                IDMotivoFalta = 164;
            }
            if (IDTipoFerias == 76)
            {
                OBS = "AFASTAMENTO PRÉ CANDIDATO CARGO ELETIVO SEM ÔNUS";
                IDMotivoFalta = 165;
            }

            if (IDTipoFerias == 79)
            {
                OBS = "AFASTAMENTO PREVENTIVO SINDICÂNCIA/ PAD";
                IDMotivoFalta = 166;
            }

            if (IDTipoFerias == 78)
            {
                OBS = "AGUARDANDO PUBLICAÇÃO DE DOCUMENTOS";
                IDMotivoFalta = 167;
            }
            if (IDTipoFerias == 45)
            {
                OBS = "CEDENCIA COM ÔNUS - CUSTEIO PMC";
                IDMotivoFalta = 168;
            }
            if (IDTipoFerias == 60)
            {
                OBS = "CEDENCIA COM ÔNUS - REEMBOLSO PARA PMC";
                IDMotivoFalta = 169;
            }
            if (IDTipoFerias == 44)
            {
                OBS = "CEDENCIA SEM ÔNUS";
                IDMotivoFalta = 170;
            }
            if (IDTipoFerias == 63)
            {
                OBS = "DESEMPENHO DE MANDATO CLASSISTA"; IDMotivoFalta = 171;
            }
            if (IDTipoFerias == 74)
            {
                OBS = "EM PROCESSO DE APOSENTADORIA POR INVALIDEZ - INSS"; IDMotivoFalta = 172;
            }

            if (IDTipoFerias == 61)
            {
                OBS = "EXERCÍCIO DE MANDATO ELETIVO COM ÔNUS - CAMPANHA ELEITORAL";
                IDMotivoFalta = 173;
            }

            if (IDTipoFerias == 46)
            {
                OBS = "EXERCÍCIO DE MANDATO SINDICAL COM ÔNUS";
                IDMotivoFalta = 174;
            }
            if (IDTipoFerias == 54)
            {
                OBS = "FUNCIONÁRIO EM VACANCIA - EFETIVO";
                IDMotivoFalta = 175;
            }
            if (IDTipoFerias == 52)
            {
                OBS = "LAUDO MEDICO - AUXILIO DOENÇA - CUIABAPREV";
                IDMotivoFalta = 176;
            }
            if (IDTipoFerias == 71)
            {
                OBS = "LAUDO MEDICO - AUXILIO DOENÇA - CUIABAPREV - CUSTEIO CUIABAPREV (A PARTIR DOS 30 PRIMEIROS DIAS)";
                IDMotivoFalta = 177;
            }
            if (IDTipoFerias == 70)
            {
                OBS = "LAUDO MEDICO - AUXILIO DOENÇA - CUIABAPREV - CUSTEIO PMC (30 PRIMEIROS DIAS)";
                IDMotivoFalta = 178;
            }
            if (IDTipoFerias == 53)
            {
                OBS = "LAUDO MEDICO - AUXILIO DOENÇA - PRORROGAÇÃO - CUIABAPREV  (A PARTIR DOS 30 PRIMEIROS DIAS)";
                IDMotivoFalta = 179;
            }
            if (IDTipoFerias == 68)
            {
                OBS = "LICENÇA ADOÇÃO/GUARDA JUDICIAL OU TUTELA DE CRIANÇA - A PARTIR DE 04 ANOS";
                IDMotivoFalta = 180;
            }
            if (IDTipoFerias == 66)
            {
                OBS = "LICENÇA ADOÇÃO/GUARDA JUDICIAL OU TUTELA DE CRIANÇA - ATÉ 01 ANO DE IDADE";
                IDMotivoFalta = 181;
            }
            if (IDTipoFerias == 67)
            {
                OBS = "LICENÇA ADOÇÃO/GUARDA JUDICIAL OU TUTELA DE CRIANÇA - IDADE 01 ANO A 04 ANOS";
                IDMotivoFalta = 182;
            }
            if (IDTipoFerias == 57)
            {
                OBS = "LICENCA GALA - CASAMENTO";
                IDMotivoFalta = 183;
            }
            if (IDTipoFerias == 42)
            {
                OBS = "LICENÇA MATERNIDADE";
                IDMotivoFalta = 184;
            }
            if (IDTipoFerias == 56)
            {
                OBS = "LICENÇA NOJO - FALECIMENTO";
                IDMotivoFalta = 185;
            }
            if (IDTipoFerias == 48)
            {
                OBS = "LICENÇA PARA PRESTAÇÃO SERVIÇO MILITAR";
                IDMotivoFalta = 186;
            }
            if (IDTipoFerias == 75)
            {
                OBS = "LICENÇA PARA QUALIFICAÇÃO PROFISSIONAL - SME";
                IDMotivoFalta = 187;
            }
            if (IDTipoFerias == 39)
            {
                OBS = "LICENÇA PARTICULAR S/ ONUS";
                IDMotivoFalta = 188;
            }
            if (IDTipoFerias == 41)
            {
                OBS = "LICENÇA PATERNIDADE";
                IDMotivoFalta = 189;
            }
            if (IDTipoFerias == 50)
            {
                OBS = "LICENÇA POR DISPONIBILIDADE COM ÔNUS";
                IDMotivoFalta = 190;
            }
            if (IDTipoFerias == 49)
            {
                OBS = "LICENÇA POR DISPONIBILIDADE SEM ÔNUS";
                IDMotivoFalta = 191;
            }
            if (IDTipoFerias == 64)
            {
                OBS = "LICENCA POR MOTIVO AFASTAMENTO DO CÔNJUGE OU COMPANHEIRO";
                IDMotivoFalta = 192;
            }
            if (IDTipoFerias == 40)
            {
                OBS = "LICENÇA SAÚDE - NAO EFETIVO (INSS) - 15 DIAS PELA EMPRESA";
                IDMotivoFalta = 193;
            }
            if (IDTipoFerias == 55)
            {
                OBS = "LICENÇA SAÚDE - PRORROGAÇAO - NAO EFETIVO (INSS)";
                IDMotivoFalta = 194;
            }
            if (IDTipoFerias == 73)
            {
                OBS = "MANDATO ELETIVO - NÃO COMPATIBILIDADE DE HORARIO - COM ONUS";
                IDMotivoFalta = 195;
            }
            if (IDTipoFerias == 72)
            {
                OBS = "MANDATO ELETIVO - NÃO COMPATIBILIDADE DE HORARIO - SEM ONUS";
                IDMotivoFalta = 196;
            }
            if (IDTipoFerias == 69)
            {
                OBS = "PARTICIPAÇÃO EM JÚRI/OUTROS SERVIÇOS OBRIGATÓRIOS POR LEI";
                IDMotivoFalta = 197;
            }
            if (IDTipoFerias == 59)
            {
                OBS = "PROR.DE ACOMPANHAMENTO DE DOENTE FAMILIAR SEM REMUNERACAO (ACIMA 90 DIAS)";
                IDMotivoFalta = 198;
            }
            if (IDTipoFerias == 58)
            {
                OBS = "PRORROGACAO DE ACOMPANHAMENTO DE DOENTE FAMILIAR C/REM (DENTRO PERÍODO DE 90 DIAS)";
                IDMotivoFalta = 199;
            }
            if (IDTipoFerias == 51)
            {
                OBS = "PRORROGAÇÃO DE LICENÇA MATERNIDADE";
                IDMotivoFalta = 200;
            }
            if (IDTipoFerias == 81)
            {
                OBS = "LICENÇA PRÊMIO POR ASSIDUIDADE";
                IDMotivoFalta = 202;
            }

            if (IDTipoFerias == 82)
            {
                OBS = "FÉRIAS";
                IDMotivoFalta = 203;
            }

            if (Operacao == "Inclusao")
            {

                while (PrimeiroDia <= UltimoDia)
                {
                    try
                    {
                        adpFReq.FillFrequenciaDia(Tbfreq, PrimeiroDia, IDusuario, IDVinculoUsuario);

                        //Total de horas no dia
                        totalHorasDiarias = string.Empty;
                        totalHorasDiarias = string.Format("0{0}:00:00.000", vwVinculoUsuario[0].TotalHoraDia);
                        totalHorasDiarias = string.Format("{0} {1}", PrimeiroDia.ToShortDateString(), totalHorasDiarias);

                        if (Tbfreq.Rows.Count == 0) //&& PrimeiroDia.DayOfWeek.ToString() != "Saturday"
                        {
                            //if (PrimeiroDia.DayOfWeek.ToString() != "Sunday")
                            //{
                            adpFrequencia.InsertFeriasLicenca(IDusuario, IDMotivoFalta, PrimeiroDia, null, null, null, null, OBS, null, PrimeiroDia.Month, PrimeiroDia.Year, IDEmpresa, IDVinculoUsuario);
                            msg = "Registro de Férias/Licença lançado na folha de frequência.";
                            //}
                        }
                        else if (PrimeiroDia.DayOfWeek.ToString() != "Saturday")
                        {
                            if (PrimeiroDia.DayOfWeek.ToString() != "Sunday")
                            {
                                adpFrequencia.UpdateFeriasFrequencia(IDMotivoFalta, OBS, IDusuario, PrimeiroDia, IDVinculoUsuario);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        msg = "Houve falha ao tentar registrar Férias/Licença na folha de frequência. Tente novamente Editando o Registro.";
                        ex.ToString();
                    }
                    PrimeiroDia = PrimeiroDia.AddDays(1);
                }
            }
            if (Operacao == "Alteracao")
            {
                adpFrequencia.FillExcluirFeriasLicenca(TBFrequencia, IDVinculoUsuario, DTInicialAlteracao, DTFinalAlteracao);

                if (TBFrequencia.Rows.Count > 0)
                {
                    for (int i = 0; i <= (TBFrequencia.Rows.Count - 1); i++)
                    {
                        if ((TBFrequencia[i].IsHoraEntraManhaNull() && TBFrequencia[i].IsHoraSaidaManhaNull() && TBFrequencia[i].IsHoraEntradaTardeNull() && TBFrequencia[i].IsHoraSaidaTardeNull()))
                        {
                            adpFrequencia.Delete(TBFrequencia[i].IDFrequencia);
                            //exclusão de registros da temporária
                            adpFreq_TEMP.DeleteIDFrequencia(TBFrequencia[i].IDFrequencia);
                        }
                        else
                        {
                            adpFrequencia.UpdateExcluiJustSemCargaHoraria(TBFrequencia[i].IDFrequencia, TBFrequencia[i].IDUsuario);

                            DateTime? Entrada1, Saida1, Entrada2, Saida2;


                            if (TBFrequencia[i].IsHoraEntraManhaNull())
                                Entrada1 = null;
                            else
                                Entrada1 = TBFrequencia[i].HoraEntraManha;

                            if (TBFrequencia[i].IsHoraSaidaManhaNull())
                                Saida1 = null;
                            else
                                Saida1 = TBFrequencia[i].HoraSaidaManha;

                            if (TBFrequencia[i].IsHoraEntradaTardeNull())
                                Entrada2 = null;
                            else
                                Entrada2 = TBFrequencia[i].HoraEntradaTarde;

                            if (TBFrequencia[i].IsHoraSaidaTardeNull())
                                Saida2 = null;
                            else
                                Saida2 = TBFrequencia[i].HoraSaidaTarde;

                            if ((Saida1.HasValue && Saida2.HasValue && Entrada1.HasValue && Entrada2.HasValue))
                            {
                                TotalHorasDIAString = Convert.ToString((Saida1 - Entrada1) + (Saida2 - Entrada2));
                            }
                            else if (Saida2.HasValue && Entrada2.HasValue)
                            {
                                TotalHorasDIAString = Convert.ToString(Saida2 - Entrada2);
                            }
                            else if (Saida1.HasValue && Entrada1.HasValue)
                            {
                                TotalHorasDIAString = Convert.ToString(Saida1 - Entrada1);
                            }

                            if (TotalHorasDIAString != string.Empty && TotalHorasDIAString != null)
                            {
                                TimeSpan totalSec = TimeSpan.Parse(TotalHorasDIAString);

                                adpFrequencia.UpdateTotHoras(Convert.ToInt32(totalSec.TotalSeconds), "", null, TBFrequencia[i].IDUsuario, TBFrequencia[i].IDFrequencia);
                            }

                            TotalHorasDIAString = string.Empty;
                        }
                    }
                    while (PrimeiroDia <= UltimoDia)
                    {
                        adpFrequencia.FillFrequenciaDia(TBFrequencia, PrimeiroDia, IDusuario, IDVinculoUsuario);

                        //Total de horas no dia
                        totalHorasDiarias = string.Empty;
                        totalHorasDiarias = string.Format("0{0}:00:00.000", vwVinculoUsuario[0].TotalHoraDia);
                        totalHorasDiarias = string.Format("{0} {1}", PrimeiroDia.ToShortDateString(), totalHorasDiarias);
                        try
                        {
                            adpFReq.FillFrequenciaDia(Tbfreq, PrimeiroDia, IDusuario, IDVinculoUsuario);

                            if (Tbfreq.Rows.Count == 0)//&& PrimeiroDia.DayOfWeek.ToString() != "Saturday"
                            {
                                //if (PrimeiroDia.DayOfWeek.ToString() != "Sunday")
                                //{
                                adpFrequencia.Insert(IDusuario, IDMotivoFalta, PrimeiroDia, null, null, null, null, OBS, null, PrimeiroDia.Month, PrimeiroDia.Year, IDEmpresa, IDVinculoUsuario);
                                msg = "Registro de Férias/Licença lançado na folha de frequência.";
                                //}
                            }
                            else
                            {
                                adpFrequencia.UpdateFeriasFrequencia(IDmotivoFalta, OBS, IDusuario, PrimeiroDia, vwUsuarioGrid[0].IDVinculoUsuario);
                            }
                        }
                        catch (Exception ex)
                        {
                            msg = "Houve falha ao tentar alterar Férias/Licença na folha de frequência. Tente novamente Editando o Registro.";
                            ex.ToString();
                        }

                        PrimeiroDia = PrimeiroDia.AddDays(1);
                    }
                }
            }
            if (Operacao == "Exclusao")
            {
                adpFrequencia.FillExcluirFeriasLicenca(TBFrequencia, IDVinculoUsuario, PrimeiroDia, UltimoDia);

                if (TBFrequencia.Rows.Count > 0)
                {
                    try
                    {
                        for (int i = 0; i <= (TBFrequencia.Rows.Count - 1); i++)
                        {
                            if (TBFrequencia[i].IsHoraEntraManhaNull() && TBFrequencia[i].IsHoraSaidaManhaNull() && TBFrequencia[i].IsHoraEntradaTardeNull() && TBFrequencia[i].IsHoraSaidaTardeNull())
                            {
                                adpFrequencia.Delete(TBFrequencia[i].IDFrequencia);
                                //exclusão de registros da temporária
                                adpFreq_TEMP.DeleteIDFrequencia(TBFrequencia[i].IDFrequencia);
                            }
                            else
                            {
                                adpFrequencia.UpdateExcluiJustSemCargaHoraria(TBFrequencia[i].IDFrequencia, TBFrequencia[i].IDUsuario);

                                DateTime? Entrada1, Saida1, Entrada2, Saida2;


                                if (TBFrequencia[i].IsHoraEntraManhaNull())
                                    Entrada1 = null;
                                else
                                    Entrada1 = TBFrequencia[i].HoraEntraManha;

                                if (TBFrequencia[i].IsHoraSaidaManhaNull())
                                    Saida1 = null;
                                else
                                    Saida1 = TBFrequencia[i].HoraSaidaManha;

                                if (TBFrequencia[i].IsHoraEntradaTardeNull())
                                    Entrada2 = null;
                                else
                                    Entrada2 = TBFrequencia[i].HoraEntradaTarde;

                                if (TBFrequencia[i].IsHoraSaidaTardeNull())
                                    Saida2 = null;
                                else
                                    Saida2 = TBFrequencia[i].HoraSaidaTarde;

                                if ((Saida1.HasValue && Saida2.HasValue && Entrada1.HasValue && Entrada2.HasValue))
                                {
                                    TotalHorasDIAString = Convert.ToString((Saida1 - Entrada1) + (Saida2 - Entrada2));
                                }
                                else if (Saida2.HasValue && Entrada2.HasValue)
                                {
                                    TotalHorasDIAString = Convert.ToString(Saida2 - Entrada2);
                                }
                                else if (Saida1.HasValue && Entrada1.HasValue)
                                {
                                    TotalHorasDIAString = Convert.ToString(Saida1 - Entrada1);
                                }

                                if (TotalHorasDIAString != string.Empty && TotalHorasDIAString != null)
                                {
                                    TimeSpan totsec = TimeSpan.Parse(TotalHorasDIAString);
                                    adpFrequencia.UpdateTotHoras(Convert.ToInt32(totsec.TotalSeconds), "", null, TBFrequencia[i].IDUsuario, TBFrequencia[i].IDFrequencia);
                                }

                                TotalHorasDIAString = string.Empty;
                            }
                        }
                        msg = "Os registros de férias/licença foram excluídos com sucesso.";
                    }
                    catch (Exception ex)
                    {
                        ex.ToString();
                    }
                }
            }
            return msg;
        }
    }
}