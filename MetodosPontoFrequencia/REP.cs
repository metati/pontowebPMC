using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetodosPontoFrequencia
{
    public class REP
    {
        //Método construtor
        public REP()
        {
        }

        public void PreencheTBREPEmpresaSetor(DataSetREP ds, int IDEmpresa, int IDSetor)
        {
            DataSetREPTableAdapters.TBREPTableAdapter adpREP = new DataSetREPTableAdapters.TBREPTableAdapter();

            try
            {
                adpREP.FillEmpresaSetor(ds.TBREP, IDEmpresa, IDSetor);
            }
            finally
            {
            }
        }

        public void PreencheTBREPEmpresa(DataSetREP ds, int IDEmpresa)
        {
            DataSetREPTableAdapters.TBREPTableAdapter adpREP = new DataSetREPTableAdapters.TBREPTableAdapter();

            try
            {
                adpREP.FillEmpresa(ds.TBREP, IDEmpresa);
            }
            finally
            {
            }
        }

        //Última coleta da REP
        public DateTime UltimaColetaREP(DataSetREP ds, int IDREP)
        {
            DataSetREPTableAdapters.TBREPColetaTableAdapter adpREPColeta = new DataSetREPTableAdapters.TBREPColetaTableAdapter();
            DateTime dd = new DateTime(1900, 01, 01);
            try
            {
                adpREPColeta.FillIDREP(ds.TBREPColeta, IDREP);
                dd = ds.TBREPColeta[0].DTUltimaColeta;
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
               ex.ToString();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }

            return dd;
        }

        //Última data de inserção do REP
        public void CadastraUltimaColeta(int IDREP, DateTime DTUltimaColeta)
        {
            DataSetREPTableAdapters.TBREPColetaTableAdapter adpColetaREP = new DataSetREPTableAdapters.TBREPColetaTableAdapter();
            try
            {
                adpColetaREP.Insert(IDREP, DTUltimaColeta);    
            }
            catch (Exception ex)
            {
            }
        }

        public string CadastraRelacaoREPUsuario(int IDUsuario, int IDREP,int IDEmpresa)
        {
            string msg = "";
            DataSetREPTableAdapters.TBREPUsuarioTableAdapter adpREPUser = new DataSetREPTableAdapters.TBREPUsuarioTableAdapter();
            try
            {
                adpREPUser.Insert(IDUsuario, IDEmpresa, IDREP);
                msg = "1";
            }
            catch (Exception ex)
            {
                msg = "0";
            }

            return msg;
        }

        //Para ultima coleta quando for cadastro de usuário

        public void CadastraUltimaColetaUsuario(int IDREP, DateTime DTUltimaColeta, string DT)
        {
            DataSetREPTableAdapters.TBREPColetaTableAdapter adpColeta = new DataSetREPTableAdapters.TBREPColetaTableAdapter();
            DataSetREP.TBREPColetaDataTable TBcoleta = new DataSetREP.TBREPColetaDataTable();

            adpColeta.FillIDREPColeta(TBcoleta, IDREP, DT);

            if (TBcoleta.Rows.Count == 0)
            {
                try
                {
                    adpColeta.Insert(IDREP, DTUltimaColeta);
                }
                catch (Exception ex)
                {
                }
            }
            else
            {
                adpColeta.UpdateUltimaColeta(DTUltimaColeta, IDREP);
            }
        }
    }
}
