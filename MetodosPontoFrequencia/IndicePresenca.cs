using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetodosPontoFrequencia
{
    public class IndicePresenca
    {
        public void PreencheIndiceClienteSIG(DataSetPontoFrequencia dsP, string DTFrequencia, int IDClienteSIG)
        {
            DataSetPontoFrequenciaTableAdapters.vwIndicePresencaTableAdapter adpIndicePresenca = new DataSetPontoFrequenciaTableAdapters.vwIndicePresencaTableAdapter();

            try
            {
                adpIndicePresenca.FillEmpresaClienteDT(dsP.vwIndicePresenca,IDClienteSIG,DTFrequencia);
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                ex.ToString();
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
    }
}
