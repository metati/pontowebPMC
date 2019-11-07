using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetodosPontoFrequencia
{
    public class Acesso
    {
        DataSetPontoFrequenciaTableAdapters.TBUsuarioTableAdapter adpUsuario = new DataSetPontoFrequenciaTableAdapters.TBUsuarioTableAdapter();
        DataSetPontoFrequencia.TBUsuarioDataTable TBUsuario = new DataSetPontoFrequencia.TBUsuarioDataTable();

        LogOperacao log = new LogOperacao();

        private Boolean autorizado = false;
        private string IDSetor = "";
        private string IDUser = "";
        private string UserLogado = "";
        public string msg = "";
        private string tpusuario = "";
        private string dssetor = "";
        private int idusuario;
        private string senha = "";
        private string PrimeiroNome = "";
        private int PrimeiroAcesso;
        private int TotHorasDiarias;
        private int IDEmpresa;
        private string senhaDigital;
        private bool DashboardPainel;
        private int qtdvinculo;

        public bool DASHBOARDPAINEL
        {
            get
            {
                return DashboardPainel;
            }
        }

        public string SENHADIGITAL
        {
            get
            {
                return senhaDigital;
            }
        }

        public int IDEMPRESA
        {
            get
            {
                return IDEmpresa;
            }
        }
        public Boolean Autorizado
        {
            get
            {
                return autorizado;
            }
        }

        public int PRIMEIROACESSO
        {
            get
            {
                return PrimeiroAcesso;
            }
        }

        public string MSG
        {
            get
            {
                return msg;
            }
        }


        public string IDSETOR
        {
            get
            {
                return IDSetor;
            }
        }

        public string IDUSER
        {
            get
            {
                return IDUser;
            }
        }

        public string DSSETOR
        {
            get
            {
                return dssetor;
            }
        }

        public string USERLOGADO
        {
            get
            {
                return UserLogado;
            }
        }
        public string TPUsuario
        {
            get
            {
                return tpusuario;
            }
        }

        public int IDUSUARIO
        {
            get
            {
                return idusuario;
            }
        }

        public string SENHA
        {
            get
            {
                return senha;
            }
        }

        public string PRIMEIRONOME
        {
            get
            {
                return PrimeiroNome;
            }
        }
        public int TOTHORASDIARIAS
        {
            get
            {
                return TotHorasDiarias;
            }
        }

        //Adicionado para antender a demanda por vínculos ---- 15/12/2015
        public int QTDVINCULOS
        {
            get
            {
                return qtdvinculo;
            }
        }

        public static string ObrigaQuatroBatidas
        {
            get { return System.Configuration.ConfigurationSettings.AppSettings["ObrigaQuatroBatidas"]; }
        }

        public static string DataDesconsidera
        {
            get { return System.Configuration.ConfigurationSettings.AppSettings["DataDesconsidera"]; }
        }

        private int QTDVinculo(int IDUsuario)
        {
            DataSetPontoFrequenciaTableAdapters.TBVinculoUsuarioTableAdapter adpVinculo = new DataSetPontoFrequenciaTableAdapters.TBVinculoUsuarioTableAdapter();
            DataSetPontoFrequencia.TBVinculoUsuarioDataTable TBVinculo = new DataSetPontoFrequencia.TBVinculoUsuarioDataTable();

            adpVinculo.FillQTDVinculo(TBVinculo, IDUsuario);

            if (TBVinculo.Rows.Count > 0)
            {
                return TBVinculo[0].IDUsuario;
            }
            else
                return 0;

        }


        public string VerificaAcesso(string login, String Senha, DataSetPontoFrequencia ds)
        {
            try
            {
                ds.EnforceConstraints = false;
                adpUsuario.LogarUsuario(ds.TBUsuario, login, Senha);

                if (ds.TBUsuario.Rows.Count > 0)
                {
                    autorizado = true;
                    IDSetor = ds.TBUsuario[0].IDSetor.ToString();
                    IDUser = ds.TBUsuario[0].Login.ToString();
                    dssetor = ds.TBUsuario[0].DSSetor.ToString();
                    UserLogado = ds.TBUsuario[0].DSUsuario.ToString();
                    tpusuario = ds.TBUsuario[0].IDTPUsuario.ToString();
                    idusuario = ds.TBUsuario[0].IDUsuario;
                    senha = ds.TBUsuario[0].SenhaAdmin;
                    PrimeiroNome = ds.TBUsuario[0].PrimeiroNome.ToString();
                    PrimeiroAcesso = ds.TBUsuario[0].PrimeiroAcesso;
                    TotHorasDiarias = ds.TBUsuario[0].TotHorasDiarias;
                    IDEmpresa = ds.TBUsuario[0].IDEmpresa;

                    if (!ds.TBUsuario[0].IsDashboardCorporativoNull())
                        DashboardPainel = ds.TBUsuario[0].DashboardCorporativo;
                    else
                        DashboardPainel = false;

                    if (!ds.TBUsuario[0].IsSenhaDigitalNull())
                        senhaDigital = ds.TBUsuario[0].SenhaDigital;

                    //Adiconado 15/12/2015 - para atender a demanda por vinculos - qtd de vinculos
                    qtdvinculo = QTDVinculo(ds.TBUsuario[0].IDUsuario);


                    //msg = "Usuario Autorizado.";
                    log.RegistraLog(idusuario, System.DateTime.Now, "adpUsuario.LogarUsuario(ds.TBUsuario, login, Senha).ToString()", "Acesso ao Sistema", "", "", IDEmpresa);


                }
                else
                {
                    msg = "Usuario ou Senha não confere.";
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                msg = "Falha ao Tentar logar. Contate o Administrador.";
            }

            return msg;
        }

        public string VerificaAcesso2(string login, String Senha, DataSetPontoFrequencia ds)
        {
            try
            {
                ds.EnforceConstraints = false;
                adpUsuario.FillLogarUsuario2(ds.TBUsuario, login, Senha);

                if (ds.TBUsuario.Rows.Count > 0)
                {
                    autorizado = true;
                    IDSetor = ds.TBUsuario[0].IDSetor.ToString();
                    IDUser = ds.TBUsuario[0].Login.ToString();
                    dssetor = ds.TBUsuario[0].DSSetor.ToString();
                    UserLogado = ds.TBUsuario[0].DSUsuario.ToString();
                    tpusuario = ds.TBUsuario[0].IDTPUsuario.ToString();
                    idusuario = ds.TBUsuario[0].IDUsuario;
                    senha = ds.TBUsuario[0].SenhaAdmin;
                    PrimeiroNome = ds.TBUsuario[0].PrimeiroNome.ToString();
                    PrimeiroAcesso = ds.TBUsuario[0].PrimeiroAcesso;
                    TotHorasDiarias = ds.TBUsuario[0].TotHorasDiarias;
                    IDEmpresa = ds.TBUsuario[0].IDEmpresa;

                    if (!ds.TBUsuario[0].IsDashboardCorporativoNull())
                        DashboardPainel = ds.TBUsuario[0].DashboardCorporativo;
                    else
                        DashboardPainel = false;

                    if (!ds.TBUsuario[0].IsSenhaDigitalNull())
                        senhaDigital = ds.TBUsuario[0].SenhaDigital;

                    //Adiconado 15/12/2015 - para atender a demanda por vinculos - qtd de vinculos
                    qtdvinculo = QTDVinculo(ds.TBUsuario[0].IDUsuario);


                    //msg = "Usuario Autorizado.";
                    log.RegistraLog(idusuario, System.DateTime.Now, "adpUsuario.LogarUsuario(ds.TBUsuario, login, Senha).ToString()", "Acesso ao Sistema", "", "", IDEmpresa);


                }
                else
                {
                    msg = "Usuario ou Senha não confere.";
                }
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                msg = "Falha ao Tentar logar. Contate o Administrador.";
            }

            return msg;
        }

        public Acesso()
        {
        }

        public string SenhaDigital(int IDUsuario, int IDEmpresa)
        {
            adpUsuario.FillSenhaNull(TBUsuario, IDUsuario, IDEmpresa);
            if (TBUsuario.Rows.Count > 0)
            {
                msg = "Primeiro Acesso. Favor cadastrar sua senha digital.";
            }
            else
            {
                msg = "";
            }

            return msg;
        }

        public string SenhaAdmin(int IDUsuario, string SenhaAdm, string SenhaAntiga, int IDEmpresa)
        {
            try
            {
                adpUsuario.UpdateAdminSenha(SenhaAdm, IDUsuario, SenhaAntiga);
                adpUsuario.FillByIDUsuario(TBUsuario, IDUsuario);

                if (TBUsuario[0].PrimeiroAcesso == 2)
                    msg = "1";
                else
                    msg = "2";
            }
            catch (Exception ex)
            {
                msg = "3";
                ex.ToString();
            }
            return msg;
        }

        public string SenhaAdmin2(int IDUsuario, string SenhaAdm, string Senhaadmin2, string SenhaAntiga, int IDEmpresa)
        {
            try
            {
                adpUsuario.UpdateAdminSenha2(SenhaAdm, Senhaadmin2, IDUsuario, SenhaAntiga);
                adpUsuario.FillByIDUsuario(TBUsuario, IDUsuario);

                if (TBUsuario[0].PrimeiroAcesso == 2)
                    msg = "1";
                else
                    msg = "2";
            }
            catch (Exception ex)
            {
                msg = "3";
                ex.ToString();
            }
            return msg;
        }

        public bool TipoAcesso(int TPUsuario, string Assunto, bool PontoEspecial, bool DashboardCorporativo, int IDEmpresa)
        {

            if (Assunto == "Mudar servidor de órgão" && TPUsuario == 1)
                return (true);

            if (Assunto == "Teste Painel")
                return (true);

            if (TPUsuario == 0)
                return (false);

            if (Assunto == "Registrar Frequência" && PontoEspecial && TPUsuario != 0)
                return (true);
            else if (Assunto == "Registrar Frequência" && !PontoEspecial)
                return (false);

            //Painel bloqueado para outros tipos de usuário. Momentaneamente.
            if (Assunto == "Painel Dashboard" && DashboardCorporativo && TPUsuario != 0)
                return (true);
            else if (Assunto == "Painel Dashboard" && !PontoEspecial)
                return (false);


            if (TPUsuario == 1)
                return (true);

            else if (TPUsuario == 2 || TPUsuario == 4)
            {
                if (Assunto == "Home")
                {
                    return (true);
                }
                if (Assunto == "Relatórios")
                {
                    return (false);
                }
                if (Assunto == "Meus Registros")
                {
                    return (true);
                }
                if (Assunto == "Manutenção de Frequência")
                {
                    return (false);
                }
                if (Assunto == "Cadastros")
                {
                    return (false);
                }
                if (Assunto == "Parâmetros do Sistema")
                {
                    return (false);
                }
                if (Assunto == "Sobre")
                {
                    return (true);
                }

                if (Assunto == "Alterar a minha senha")
                {
                    return (true);
                }
                if (Assunto == "Banco hora mês")
                {
                    return (false);
                }
                if (Assunto == "Total de horas diárias")
                {
                    return (false);
                }

                if (Assunto == "Relação de marcação por período")
                {
                    return (false);
                }

                if (Assunto == "Situação Cadastral Usuários")
                {
                    return (false);
                }

                if (Assunto == "Registros de Ponto e Locais de Marcação")
                {
                    return (false);
                }
                if (Assunto == "Relação de Justificativas por período")
                {
                    return (false);
                }

                if (Assunto == "Relação de faltas")
                {
                    return (false);
                }
                if (Assunto == "Relatório de Descontos")
                {
                    return (false);
                }
                if (Assunto == "Fechamento de Folha")
                {
                    return (false);
                }
            }
            else if (TPUsuario == 3)
            {
                if (Assunto == "Home")
                {
                    return (true);
                }
                if (Assunto == "Relatórios")
                {
                    return (true);
                }
                if (Assunto == "Meus Registros")
                {
                    return (true);
                }
                if (Assunto == "Manutenção de Frequência")
                {
                    return (true);
                }
                if (Assunto == "Realizar Justificativa")
                {
                    return (true);
                }
                if (Assunto == "Espelho de marcação de ponto")
                {
                    return (true);
                }
                if (Assunto == "Lançar Férias/Licenças")
                {
                    return (true);
                }
                if (Assunto == "Lançar Falta")
                {
                    return (true);
                }
                if (Assunto == "Feriados e Pontos Facultativos")
                {
                    return (false);
                }
                if (Assunto == "Cadastros")
                {
                    return (false);
                }
                if (Assunto == "Colaborador/Usuário")
                {
                    return (false);
                }
                if (Assunto == "Cargo")
                {
                    return (false);
                }
                if (Assunto == "Vínculos")
                {
                    return (false);
                }
                if (Assunto == "Parâmetros do Sistema")
                {
                    return (false);
                }
                if (Assunto == "Padrões de Horas")
                {
                    return (false);
                }
                if (Assunto == "Informações Diárias")
                {
                    return (true);
                }
                if (Assunto == "Sobre")
                {
                    return (true);
                }

                if (Assunto == "Alterar a minha senha")
                {
                    return (true);
                }

                if (Assunto == "Relatórios")
                {
                    return (true);
                }

                if (Assunto == "Situação Cadastral Usuários")
                {
                    return (false);
                }

                if (Assunto == "Banco hora mês")
                {
                    return (true);
                }

                if (Assunto == "Total de horas diárias")
                {
                    return (true);
                }

                if (Assunto == "Relação de marcação por período")
                {
                    return (true);
                }
                if (Assunto == "Registros de Ponto e Locais de Marcação")
                {
                    return (false);
                }
                if (Assunto == "Relação de Justificativas por período")
                {
                    return (true);
                }
                if (Assunto == "Relação de faltas")
                {
                    return (true);
                }

                if (Assunto == "Relatório de Descontos")
                {
                    return (false);
                }

                if (Assunto == "Escalas de horários")
                {
                    return (true);
                }

                if (Assunto == "Conferência Fechamento de Folha")
                {
                    return (true);
                }

                if (Assunto == "Ponto Eletrônico")
                {
                    return (false);
                }
                if (Assunto == "Fechamento de Folha")
                {
                    return (false);
                }
            }
            else if ((TPUsuario == 7) || (TPUsuario == 8) || (TPUsuario == 9))
            {
                //para saúde e educação
                if (IDEmpresa == 43 || IDEmpresa == 54)
                {
                    if (Assunto == "Administrar Outra Empresa/Órgão")
                        return (false);
                }
                else
                    return true;



                if (Assunto == "Home")
                {
                    return (true);
                }
                if (Assunto == "Relatórios")
                {
                    return (true);
                }
                if (Assunto == "Meus Registros")
                {
                    return (true);
                }
                if (Assunto == "Manutenção de Frequência")
                {
                    return (true);
                }
                if (Assunto == "Realizar Justificativa")
                {
                    return (true);
                }
                if (Assunto == "Espelho de marcação de ponto")
                {
                    return (true);
                }
                if (Assunto == "Lançar Falta")
                {
                    return (true);
                }
                if (Assunto == "Lançar Férias/Licenças")
                {
                    return (true);
                }
                if (Assunto == "Feriados e Pontos Facultativos")
                {
                    if (IDEmpresa == 43 || IDEmpresa == 54)
                        return (true);
                    else
                        return false;
                }
                if (Assunto == "Controle de Horas Extras")
                {
                    return true;
                }
                if (Assunto == "Lançamento de Horas Extras")
                {
                    return true;
                }
                if (Assunto == "Regime de horários")
                {
                    return true;
                }
                if (Assunto == "Escalas de horários")
                {
                    return true;
                }

                if (TPUsuario == 8)
                    if (Assunto == "Fechamento de Folha")
                    {
                        return true;
                    }
                //Pedido PMC - Não habilitar o cadatro p administradores de unidades.
                //25/11/2018 == Alguns itens liberados a pedido da PMC para SMS e SME
                if (Assunto == "Cadastros")
                {
                    if (IDEmpresa == 43 || IDEmpresa == 54)
                        return (true);
                    else
                        return false;
                }
                if (Assunto == "Colaborador/Usuário")
                {
                    if (IDEmpresa == 43 || IDEmpresa == 54)
                        return (true);
                    else
                        return false;
                }
                if (Assunto == "Cargo")
                {
                    if (IDEmpresa == 43 || IDEmpresa == 54)
                        return (true);
                    else
                        return false;
                }
                if (Assunto == "Vínculos")
                {
                    if (IDEmpresa == 43 || IDEmpresa == 54)
                        return (true);
                    else
                        return false;
                }
                if (Assunto == "Setor")
                {
                    if (IDEmpresa == 43 || IDEmpresa == 54)
                        return (true);
                    else
                        return false;
                }
                if (Assunto == "Parâmetros do Sistema")
                {
                    if (IDEmpresa == 43 || IDEmpresa == 54)
                        return (true);
                    else
                        return false;
                }
                if (Assunto == "Justificativas")
                    if (IDEmpresa == 43 || IDEmpresa == 54)
                        return (true);
                    else
                        return false;

                if (Assunto == "Padrões de Horas")
                {
                    if (IDEmpresa == 43 || IDEmpresa == 54)
                        return (true);
                    else
                        return false;
                }
                if (Assunto == "Informações Diárias")
                {
                    if (IDEmpresa == 43 || IDEmpresa == 54)
                        return (true);
                    else
                        return false;
                }
                if (Assunto == "Sobre")
                {
                    return (true);
                }

                if (Assunto == "Alterar a minha senha")
                {
                    return (true);
                }

                if (Assunto == "Relatórios")
                {
                    return (true);
                }

                if (Assunto == "Situação Cadastral Usuários")
                {
                    if (IDEmpresa == 43 || IDEmpresa == 54)
                        return (true);
                    else
                        return false;
                }

                if (Assunto == "Banco hora mês")
                {
                    if (IDEmpresa == 43 || IDEmpresa == 54)
                        return (true);
                    else
                        return false;
                }

                if (Assunto == "Total de horas diárias")
                {
                    return (true);
                }

                if (Assunto == "Relação de marcação por período")
                {
                    return (true);
                }
                if (Assunto == "Registros de Ponto e Locais de Marcação")
                {
                    return (true);
                }
                if (Assunto == "Relação de Justificativas por período")
                {
                    return (true);
                }

                if (Assunto == "Relação de faltas")
                {
                    return (true);
                }
                if (Assunto == "Relatório de Descontos")
                {
                    if (IDEmpresa == 43 || IDEmpresa == 54)
                        return (false);
                    else
                        return false;
                }
                if (Assunto == "Conferência Fechamento de Folha")
                {
                    return (true);
                }

                if (Assunto == "Ponto Eletrônico")
                {
                    if (IDEmpresa == 43 || IDEmpresa == 54)
                        return (true);
                    else
                        return false;
                }

            }
            else if (TPUsuario == 9)
            {
                if (Assunto == "Home")
                {
                    return (true);
                }
                if (Assunto == "Relatórios")
                {
                    return (true);
                }
                if (Assunto == "Meus Registros")
                {
                    return (true);
                }
                if (Assunto == "Manutenção de Frequência")
                {
                    return (false);
                }
                if (Assunto == "Realizar Justificativa")
                {
                    return (false);
                }
                if (Assunto == "Espelho de marcação de ponto")
                {
                    return (true);
                }
                if (Assunto == "Lançar Férias/Licenças")
                {
                    return (false);
                }
                if (Assunto == "Feriados e Pontos Facultativos")
                {
                    return (false);
                }
                if (Assunto == "Cadastros")
                {
                    return (false);
                }
                if (Assunto == "Colaborador/Usuário")
                {
                    return (false);
                }
                if (Assunto == "Cargo")
                {
                    if (IDEmpresa == 43 || IDEmpresa == 54)
                        return (true);
                    else
                        return false;
                }
                if (Assunto == "Vínculos")
                {
                    return (false);
                }
                if (Assunto == "Parâmetros do Sistema")
                {
                    return (false);
                }
                if (Assunto == "Padrões de Horas")
                {
                    return (false);
                }
                if (Assunto == "Informações Diárias")
                {
                    return (false);
                }
                if (Assunto == "Sobre")
                {
                    return (true);
                }

                if (Assunto == "Alterar a minha senha")
                {
                    return (true);
                }

                if (Assunto == "Relatórios")
                {
                    return (true);
                }

                if (Assunto == "Situação Cadastral Usuários")
                {
                    return (true);
                }

                if (Assunto == "Banco hora mês")
                {
                    return (true);
                }

                if (Assunto == "Total de horas diárias")
                {
                    return (true);
                }

                if (Assunto == "Relação de marcação por período")
                {
                    return (true);
                }
                if (Assunto == "Registros de Ponto e Locais de Marcação")
                {
                    return (true);
                }
                if (Assunto == "Relação de Justificativas por período")
                {
                    return (true);
                }
                if (Assunto == "Ficha de Servidores")
                {
                    return (true);
                }

                if (Assunto == "Relação de faltas")
                {
                    return (true);
                }
                if (Assunto == "Relatório de Descontos")
                {
                    return (true);
                }

                if (Assunto == "Ponto Eletrônico")
                {
                    return (false);
                }
                if (Assunto == "Fechamento de Folha")
                {
                    return (false);
                }

            }

            //else if (TPUsuario == 8)
            //{
            //    if (Assunto == "Home")
            //    {
            //        return (true);
            //    }
            //    if (Assunto == "Relatórios")
            //    {
            //        return (true);
            //    }
            //    if (Assunto == "Minhas horas diárias")
            //    {
            //        return (true);
            //    }
            //    if (Assunto == "Manutenção de Frequência")
            //    {
            //        return (false);
            //    }
            //    if (Assunto == "Justificativas de Faltas e Atrasos")
            //    {
            //        return (false);
            //    }
            //    if (Assunto == "Gerar Folha de Frequência")
            //    {
            //        return (true);
            //    }
            //    if (Assunto == "Lançar Férias/Licenças")
            //    {
            //        return (false);
            //    }
            //    if (Assunto == "Feriados e Pontos Facultativos")
            //    {
            //        return (false);
            //    }
            //    if (Assunto == "Cadastro")
            //    {
            //        return (false);
            //    }
            //    if (Assunto == "Usuário")
            //    {
            //        return (false);
            //    }
            //    if (Assunto == "Cargo")
            //    {
            //        return (false);
            //    }
            //    if (Assunto == "Vínculos")
            //    {
            //        return (false);
            //    }
            //    if (Assunto == "Parâmetros do Sistema")
            //    {
            //        return (false);
            //    }
            //    if (Assunto == "Padrões de Horas")
            //    {
            //        return (false);
            //    }
            //    if (Assunto == "Informações Diárias")
            //    {
            //        return (false);
            //    }
            //    if (Assunto == "Sobre")
            //    {
            //        return (true);
            //    }

            //    if (Assunto == "Mudar Senha de Acesso")
            //    {
            //        return (true);
            //    }

            //    if (Assunto == "Relatórios")
            //    {
            //        return (true);
            //    }

            //    if (Assunto == "Situação Cadastral Usuários")
            //    {
            //        return (true);
            //    }

            //    if (Assunto == "Banco hora mês")
            //    {
            //        return (true);
            //    }

            //    if (Assunto == "Total de horas diárias")
            //    {
            //        return (true);
            //    }

            //    if (Assunto == "Relação de marcação por período")
            //    {
            //        return (true);
            //    }
            //    if (Assunto == "Registros de Ponto e Locais de Marcação")
            //    {
            //        return (true);
            //    }
            //    if (Assunto == "Relação de Justificativas por período")
            //    {
            //        return (true);
            //    }
            //    if (Assunto == "Ficha de Servidores")
            //    {
            //        return (true);
            //    }
            //    if (Assunto == "Relação de faltas")
            //    {
            //        return (true);
            //    }
            //    if (Assunto == "Relatório de Descontos")
            //    {
            //        return (true);
            //    }

            //}

            return (false);
        }

    }
}