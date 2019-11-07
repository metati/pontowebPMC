using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetodosPontoFrequencia;

/// <summary>
/// Summary description for MontaTextoToolTip
/// </summary>
public class MontaTextoToolTip
{
    int TipoFrequencia;
    string texto, textoJustificado, textoAusencia;
    MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vWHorasDiaTableAdapter adpHoras = new MetodosPontoFrequencia.DataSetPontoFrequenciaTableAdapters.vWHorasDiaTableAdapter();

    public string TEXTO
    {
        get
        {
            return texto;
        }
    }

    public string TEXTOJUSTIFICADO
    {
        get
        {
            return textoJustificado;
        }
    }

    public string TEXTOAUSENCIA
    {
        get
        {
            return textoAusencia;
        }
    }

    //MetodosPontoFrequencia.DataSetPontoFrequencia.vWHorasDiaDataTable vwHorasDia = new DataSetPontoFrequencia.vWHorasDiaDataTable();
    public MontaTextoToolTip(int tpfrequencia)
	{
		//
		// TODO: Add constructor logic here
		//
        TipoFrequencia = tpfrequencia;
	}

    public void MontaTextoUsuarioComum(int IDUsuario,int IDVinculoUsuario, int MesReferencia,int AnoReferencia, string Situacao, DataSetPontoFrequencia ds,int IDEmpresa)
    {     
        texto = string.Empty;
        ds.EnforceConstraints = false;
        adpHoras.Connection.Open();
        adpHoras.FillSituacaoDiaUsuario(ds.vWHorasDia,IDUsuario,IDVinculoUsuario,AnoReferencia,MesReferencia,IDEmpresa);
        adpHoras.Connection.Close();
        
        if (ds.vWHorasDia.Rows.Count > 0)
        {
            textoJustificado = "JUSTIFICADO(S):<br>";
            texto = "EFETUOU REGISTRO:<br>";
            textoAusencia = "NÃO EFETUOU REGISTRO:<br>";

            for (int i = 0; i <= (ds.vWHorasDia.Rows.Count - 1); i++)
            {
                switch (ds.vWHorasDia[i].SituacaoN)
                {
                    case 1:
                        textoJustificado += string.Format("{0}<br>", ds.vWHorasDia[i].DataFrequencia);
                        break;
                    case 2:
                        texto += string.Format("{0}<br>", ds.vWHorasDia[i].DataFrequencia);
                        break;
                    case 3:
                        textoAusencia += string.Format("{0}<br>", ds.vWHorasDia[i].DataFrequencia);
                        break;
                }
            }
        }

    }

    public void MontaTexto(int IDSetor, DateTime dtfrequencia, int SituacaoN ,DataSetPontoFrequencia ds)
    {
        texto = string.Empty;
        ds.EnforceConstraints = false;
        adpHoras.FillSituacaoDia(ds.vWHorasDia, IDSetor, dtfrequencia);
        texto = string.Empty;
        
        if (ds.vWHorasDia.Rows.Count > 0)
        {
            textoJustificado = "JUSTIFICADO(S):<br>";
            texto = "REGISTRADO(S):<br>";
            textoAusencia = "AUSÊNCIA(S):<br>";

            for (int i = 0; i <= (ds.vWHorasDia.Rows.Count - 1);i++ )
            {

                switch (ds.vWHorasDia[i].SituacaoN)
                {
                    case 1:
                        textoJustificado += string.Format("Nome: {0}<br>", ds.vWHorasDia[i].DSUsuario);
                        break;
                    case 2:
                        texto += string.Format("Nome: {0}<br>", ds.vWHorasDia[i].DSUsuario);
                        break;
                    case 3:
                        textoAusencia += string.Format("Nome: {0}<br>", ds.vWHorasDia[i].DSUsuario);
                        break;
                }
            }
        }

        ds = null;
    }
}