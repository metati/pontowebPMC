using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlServerCe;
using System.Data;
//using System.Text;



namespace pnrClient
{
    public class RegistroLocal
    {
        //20/09/2016 - Envio de registros feitos off-line com falhas
        //Objetos para Vefificação de registros
        SqlCeDataAdapter DA;
            
        SqlCeCommand SQLComand;
        SqlCeConnection SQLConn;
        DataTable TBFL;

        string msg = string.Empty;
        string NomeUsuario;
        DataSetpnrClient dsL;
        DataSetpnrClientTableAdapters.TBFrequenciaLocalTableAdapter adpFrequencia = new DataSetpnrClientTableAdapters.TBFrequenciaLocalTableAdapter();
        DataSetpnrClientTableAdapters.TBUsuarioLocalTableAdapter adpUsuario = new DataSetpnrClientTableAdapters.TBUsuarioLocalTableAdapter();

        public string InsertRegistroLocal(int idusuario,DateTime DTRegistro, int IDVinculoUsuario)
        {
            adpFrequencia.Connection.ConnectionString = pnrClient.Properties.Settings.Default.ptServer35ConnectionString;
            adpUsuario.Connection.ConnectionString = pnrClient.Properties.Settings.Default.ptServer35ConnectionString;

            dsL = new DataSetpnrClient();
            dsL.EnforceConstraints = false;
            adpUsuario.FillIDUsuario(dsL.TBUsuarioLocal,idusuario);
            
            NomeUsuario = string.Empty;
            
            if(dsL.TBUsuarioLocal.Rows.Count > 0)
                NomeUsuario = dsL.TBUsuarioLocal[0].PrimeiroNome;
            else
                NomeUsuario = "Servidor";

            try
            {
                if (MaxRegistro(Convert.ToInt32(dsL.TBUsuarioLocal[0].IDUsuario), DTRegistro))
                {
                    adpFrequencia.Insert(DTRegistro,idusuario,false,null,IDVinculoUsuario);
                    msg = string.Format("{0}, rede indisponível. Registro efetivado localmente e passível de análise.", NomeUsuario);
                }
                else
                    msg = string.Format("{0}, respeite o limite de 10 minutos entre registros de entrada e saída.", NomeUsuario);
            }
            catch (Exception ex)
            {
                if(NomeUsuario == "Servidor")
                {
                    msg = "BANCO DE DADOS LOCAL ESTÁ VAZIO. CONTATE O ADMINISTRADOR PARA ATUALIZAÇÃO.";
                }
                else
                    msg = string.Format("Houve falha ao salvar registro localmente. Contate o administrador com o erro n.: {0}. ",ex.Source);
            }

            return msg;
        }
        //Atualiza status do registro para true, caso o registro local foi enviado para a central de dados.
        public void registroEnviado(int idfrequencia, string MSG)
        {
            adpFrequencia.Connection.ConnectionString = pnrClient.Properties.Settings.Default.ptServer35ConnectionString;

            adpFrequencia.UpdateRegistroEnviado(true,MSG, idfrequencia);
        }

        public DataSetpnrClient PreencheTBFrequencia(string DTFrequencia)
        {
            adpFrequencia.Connection.ConnectionString = pnrClient.Properties.Settings.Default.ptServer35ConnectionString;

            adpFrequencia.FillDTFrequencia(dsL.TBFrequenciaLocal, DTFrequencia);

            return dsL;
        }

        protected bool MaxRegistro(int IDUsuario, DateTime UltimaBatida)
        {

            adpFrequencia.FillMaxPonto(dsL.TBFrequenciaLocal, IDUsuario);

            if (dsL.TBFrequenciaLocal.Rows.Count > 0)
            {

                if (dsL.TBFrequenciaLocal[0].IDFrequenciaLocal > 0)
                {
                    if (dsL.TBFrequenciaLocal[0].DTFrequencia.AddMinutes(10) < UltimaBatida)
                        return true;
                    else
                        return false;
                }
                else
                    return true;
            }
            else
                return true;
        }

        //novaRotina -- dar um update no registro que já tenha sido enviado.
        private void RegistroEnviado(int IDfrequenciaLocal, string RetornoMSG, bool EnvioRegistro)
        {
            SQLConn = new SqlCeConnection(pnrClient.Properties.Settings.Default.ptServer35ConnectionString);
            SQLComand = new SqlCeCommand("UPDATE TBFrequenciaLocal SET registroenviado = @Envio, RETORNOMSG = @RETORNOMSG where IDFrequenciaLocal = @IDFL",SQLConn);
            SQLComand.Parameters.Add("IDFL", IDfrequenciaLocal);
            SQLComand.Parameters.Add("RETORNOMSG", RetornoMSG);
            SQLComand.Parameters.Add("Envio", EnvioRegistro);
            SQLConn.Open();
            SQLComand.ExecuteNonQuery();
            SQLConn.Close();
        }

        private void ExcluirRegistrosEnviados()
        {
            SQLConn = new SqlCeConnection(pnrClient.Properties.Settings.Default.ptServer35ConnectionString);
            SQLComand = new SqlCeCommand("Delete from tbfrequencialocal where DATEDIFF(day,dtfrequencia,getdate()) > 1 or retornomsg like '%frequência efetivada%' or retornomsg like '%finalizada%'", SQLConn);
            SQLConn.Open();
            SQLComand.ExecuteNonQuery();
            SQLConn.Close();

            SQLConn = new SqlCeConnection(pnrClient.Properties.Settings.Default.ptServer35ConnectionString);
            SQLComand = new SqlCeCommand("Delete from tbfrequencialocalManual where DATEDIFF(day,dtfrequencia,getdate()) > 1 or retornomsg like '%frequência efetivada%' or retornomsg like '%finalizada%'", SQLConn);
            SQLConn.Open();
            SQLComand.ExecuteNonQuery();
            SQLConn.Close();
        }
        //-----------------------------------------------------------------

        //Nova Rotina - Envio de registros off por meio manual, dispensando o dataset Tipado!
        public void EnviarFrequencia(DateTime DTFrequencia, int IDEmpresa, int IDSetor)
        {
            //para o dataset Tipado.
            DataSetpnrClientTableAdapters.TBUsuarioLocalTableAdapter adpLocal =
                new DataSetpnrClientTableAdapters.TBUsuarioLocalTableAdapter();
            adpLocal.Connection.ConnectionString = pnrClient.Properties.Settings.Default.ptServer35ConnectionString;
            DataSetpnrClient.TBUsuarioLocalDataTable TBUsuarioLocal;
            //Antes de executar --- Excluir os registros mais antigos que 5 dias!
            ExcluirRegistrosEnviados();
            
            bool registroenviado = false;

            SQLConn = new SqlCeConnection(pnrClient.Properties.Settings.Default.ptServer35ConnectionString);

            DA = null;
            DA = new SqlCeDataAdapter("SELECT * FROM TBFrequenciaLocal WHERE DTFrequencia <= @DTFREQUENCIA AND (RegistroEnviado = 0 OR RegistroEnviado IS NULL) ORDER BY DTFrequencia", SQLConn);

            DA.SelectCommand.Parameters.Add("DTFREQUENCIA", DTFrequencia);

            TBFL = null;
            TBFL = new DataTable();
            DA.Fill(TBFL);
            
            if (TBFL.Rows.Count > 0)
            {
                pontonarede.ServiceSoapClient webS = new pontonarede.ServiceSoapClient();
                webS.Endpoint.Binding.OpenTimeout = TimeSpan.FromSeconds(3);
                webS.Endpoint.Binding.SendTimeout = TimeSpan.FromSeconds(2);
                webS.Open();
                for (int i = 0; i <= TBFL.Rows.Count - 1; i++)
                {
                    msg = string.Empty;

                    //Acha o usuario
                    TBUsuarioLocal = new DataSetpnrClient.TBUsuarioLocalDataTable();
                    adpLocal.FillIDVinculoUsuario(TBUsuarioLocal, Convert.ToInt32(TBFL.Rows[i]["IDVinculoUsuario"]));

                    try
                    {
                        int teste = Convert.ToInt32(TBFL.Rows[i]["IDUsuarioLocal"]);
                        msg = webS.BaterPonto(IDEmpresa, IDSetor, Convert.ToInt32(TBFL.Rows[i]["IDUsuarioLocal"]),Convert.ToDateTime(TBFL.Rows[i]["DTFrequencia"]), "TentoWebServiceNovamente7x24dm12",TBUsuarioLocal[0].HoraEntradaManha,
                            TBUsuarioLocal[0].HoraSaidaManha, TBUsuarioLocal[0].HoraEntradaTarde, TBUsuarioLocal[0].HoraSaidaTarde, TBUsuarioLocal[0].TotalHoraDia, TBUsuarioLocal[0].DSUsuario, TBUsuarioLocal[0].PrimeiroNome, TBUsuarioLocal[0].RegimePlantonista, TBUsuarioLocal[0].IDVinculoUsuario);
                        registroenviado = true;
                    }
                    catch (Exception ex)
                    {
                        msg = string.Format("Usuario: {0} - Falha: {1}", Convert.ToInt32(TBFL.Rows[i]["IDUsuarioLocal"]), ex.Message.ToString());
                        if (msg.Length > 100)
                            msg = msg.Substring(1, 100);
                        registroenviado = false;
                    }

                    if (msg.Length > 10)
                        RegistroEnviado(Convert.ToInt32(TBFL.Rows[i]["IDFrequenciaLocal"]),msg,registroenviado);

                }

                webS.Close();
                GC.Collect();
            }
        }
        // --------------------------------------------------------------------------------

        //public void PontoEnviar(DateTime DTFrequencia, int IDEmpresa, int IDSetor)
        //{
            
        //    bool registroenviado = false;   
        //    adpFrequencia.Connection.ConnectionString = pnrClient.Properties.Settings.Default.ptServer35ConnectionString;
        //    dsL = new DataSetpnrClient();
            
        //    //Preenchendo a tabela.
        //    adpFrequencia.FillDTFrequencia(dsL.TBFrequenciaLocal, DTFrequencia.ToShortDateString());

        //    if (dsL.TBFrequenciaLocal.Rows.Count > 0)
        //    {
        //        pontonarede.ServiceSoapClient webS = new pontonarede.ServiceSoapClient();

        //        for (int i = 0; i <= dsL.TBFrequenciaLocal.Rows.Count - 1; i++)
        //        {
        //            msg = string.Empty;
                    
        //            try
        //            {
        //                msg = webS.BaterPonto(IDEmpresa, IDSetor, dsL.TBFrequenciaLocal[i].IDUsuarioLocal, dsL.TBFrequenciaLocal[i].DTFrequencia, "TentoWebServiceNovamente7x24dm12");
        //                registroenviado = true;
        //            }
        //            catch(Exception ex)
        //            {
        //                msg = string.Format("Usuario: {0} - Houve falha ao enviar o registro ou Registro já efetivado: {1}", dsL.TBFrequenciaLocal[i].IDUsuarioLocal,ex.Message.ToString());
        //                registroenviado = false;
        //            }

        //            if(msg.Length > 3)
        //                adpFrequencia.UpdateRegistroEnviado(registroenviado,msg, dsL.TBFrequenciaLocal[i].IDFrequenciaLocal);
        //        }
        //    }
        //    System.GC.Collect();
        //}

        
        public void PontoEnviarManual(DateTime DTFrequencia, int IDEmpresa, int IDSetor)
        {


            DataSetpnrClientTableAdapters.TBFrequenciaLocalManualTableAdapter adpFrequenciaManual = new DataSetpnrClientTableAdapters.TBFrequenciaLocalManualTableAdapter();
            adpFrequenciaManual.Connection.ConnectionString = pnrClient.Properties.Settings.Default.ptServer35ConnectionString;

            Cript cr = new Cript();

            dsL = new DataSetpnrClient();

            adpFrequenciaManual.FillRegistroNaoEnviado(dsL.TBFrequenciaLocalManual);

            if (dsL.TBFrequenciaLocalManual.Rows.Count > 0)
            {
                pontonarede.ServiceSoapClient webS = new pontonarede.ServiceSoapClient();

                for (int i = 0; i <= (dsL.TBFrequenciaLocalManual.Rows.Count - 1); i++)
                {
                    try
                    {
                        msg = webS.PontoEspecial(dsL.TBFrequenciaLocalManual[i].login, cr.ActionDecrypt(dsL.TBFrequenciaLocalManual[i].Senha), IDEmpresa, DTFrequencia, "TentoWebServiceNovamente7x24dm12");
                        adpFrequenciaManual.UpdateRetornoMSG(msg, dsL.TBFrequenciaLocalManual[i].IDFrequenciaManual);
                    }
                    catch (Exception ex)
                    {
                        ex.ToString();
                        adpFrequenciaManual.UpdateRetornoMSG(string.Format("Usuário: {0}. Houve falha no envio ou registro já enviado."), dsL.TBFrequenciaLocalManual[i].IDFrequenciaManual);
                    }
                }
            }
        }

        public string PontoManual(string Login, string Senha, DateTime DTFrequencia)
        {
            DataSetpnrClientTableAdapters.TBFrequenciaLocalManualTableAdapter adpManualLocal = new DataSetpnrClientTableAdapters.TBFrequenciaLocalManualTableAdapter();
            try
            {
                adpManualLocal.Connection.ConnectionString = pnrClient.Properties.Settings.Default.ptServer35ConnectionString;
                adpManualLocal.Insert(DTFrequencia, Login, Senha, null);
                msg = string.Format("Servidor: {0}. Registro efetuado localmente e passível de análise.",Login);
            }
            catch (Exception ex)
            {
                msg = string.Format("Servidor: {0}. houve falha na tentativa de registro local.",Login);
            }

            return msg;
        }

        //Registros com problemas -- Não enviados por algum motivo -- VOLTAR DAQUI.
        public void RegistroComFalha(int IDEmpresa, int IDSetor)
        {
            SQLConn = new SqlCeConnection(pnrClient.Properties.Settings.Default.ptServer35ConnectionString);
            DA = null;
            DA = new SqlCeDataAdapter(string.Format("Select * from tbfrequenciaLocal where RetornoMSG like {0} and RegistroEnviado = 0", "'%Falha%'"), SQLConn);
            TBFL = null;
            TBFL = new DataTable();
            DA.Fill(TBFL);
            if (TBFL.Rows.Count > 0)
            {
                string[] Registros = new string[TBFL.Rows.Count];
                for (int i = 0; i <= TBFL.Rows.Count - 1; i++)
                {
                    Registros[i] = string.Format("{0};{1};{2};{3};{4};{5}", TBFL.Rows[i]["IDFrequenciaLocal"].ToString(), 
                        TBFL.Rows[i]["DTFrequencia"].ToString(),TBFL.Rows[i]["IDUsuarioLocal"].ToString(),IDEmpresa.ToString(), 
                        IDSetor.ToString(), TBFL.Rows[i]["RetornoMSG"].ToString().Trim());
                }
                //Testando abertura da string
                //int pos = Registros[0].IndexOf(';');
                //string IDFrequencia = Registros[0].Substring(0, pos);

                //pos ++;

                //int pos2 = Registros[0].IndexOf(';', pos);

                //int posFinal = pos2 - pos;

                //string DTFreq = Registros[0].Substring(pos, posFinal);

                //pos2++;
                
                //pos = Registros[0].IndexOf(';', pos2);

                //posFinal = pos - pos2;

                //string IDusuario = Registros[0].Substring(pos2, posFinal);

                //string idempresa = Registros[0].Substring(pos + 1, 1);
                //string idsetor = Registros[0].Substring(pos + 3, 1);
                //string msg = Registros[0].Substring(pos + 5);


                pontonarede.ServiceSoapClient WebS = new pontonarede.ServiceSoapClient();
                WebS.Endpoint.Binding.OpenTimeout = TimeSpan.FromSeconds(2);
                WebS.Endpoint.Binding.SendTimeout = TimeSpan.FromSeconds(2);
                try
                {
                    WebS.Open();
                    WebS.RegistroFalha(Registros);
                    WebS.Close();
                }
                catch(Exception)
                {
                    WebS.Close();
                }
                
            }
        }

        public void PontoEnviar(DateTime DTFrequencia, int IDEmpresa, int IDSetor)
        {
            DataSetpnrClient dsL = new DataSetpnrClient();
            bool registroenviado = false;
            adpFrequencia.Connection.ConnectionString = pnrClient.Properties.Settings.Default.ptServer35ConnectionString;
            
            //Preenchendo a tabela.
            adpFrequencia.FillDTFrequencia(dsL.TBFrequenciaLocal, DTFrequencia.ToShortDateString());

            if (dsL.TBFrequenciaLocal.Rows.Count > 0)
            {
                pontonarede.ServiceSoapClient webS = new pontonarede.ServiceSoapClient();

                for (int i = 0; i <= dsL.TBFrequenciaLocal.Rows.Count - 1; i++)
                {
                    msg = string.Empty;

                    try
                    {
                        msg = webS.BaterPonto(IDEmpresa, IDSetor, dsL.TBFrequenciaLocal[i].IDUsuarioLocal, dsL.TBFrequenciaLocal[i].DTFrequencia, "TentoWebServiceNovamente7x24dm12","","","","",0,
                            "","",false,1);
                        registroenviado = true;
                    }
                    catch (Exception ex)
                    {
                        msg = string.Format("Usuario: {0} - Houve falha ao enviar o registro ou Registro já efetivado: {1}", dsL.TBFrequenciaLocal[i].IDUsuarioLocal, ex.Message.ToString());
                        registroenviado = false;
                    }

                    if (msg.Length > 3)
                        adpFrequencia.UpdateRegistroEnviado(registroenviado, msg, dsL.TBFrequenciaLocal[i].IDFrequenciaLocal);
                }
            }
            System.GC.Collect();
        }
    }
}
