using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetodosPontoFrequencia;
/// <summary>
/// Summary description for DefineVinculo
/// </summary>
public class DefineVinculo
{
    private PreencheTabela PT = new PreencheTabela();
    private DataSetPontoFrequencia ds = new DataSetPontoFrequencia();

    private int idsetor;
    private int idempresa;
    private int idtpusuario;
    private int totalhoradiaria;
    private string dsempresa;
    private long idvinculousuario;

    public long IDVINCULOUSUARIO
    {
        get
        {
            return idvinculousuario;
        }
    }
    
    public int IDSETOR
    {
        get
        {
            return idsetor;
        }
    }

    public int IDEMPRESA
    {
        get
        {
            return idempresa;
        }
    }

    public int IDTPUSUARIO
    {
        get
        {
            return idtpusuario;
        }
    }

    public int TOTALHORADIARIA
    {
        get
        {
            return totalhoradiaria;
        }
    }

    public string DSEMPRESA
    {
        get
        {
            return dsempresa;
        }
    }

    

	
    public DefineVinculo()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public void DefineVinculousuario(int IDVinculoUsuario, bool PorVinculo)
    {
        if (PorVinculo)
            PT.PreenchevwVinculoUsuarioIDVinculoUsuario(ds, IDVinculoUsuario);
        else
            PT.PreenchevwVinculoUsuario(ds, IDVinculoUsuario);
        
        if (ds.vwVinculoUsuario.Rows.Count > 0)
        {
            idsetor = ds.vwVinculoUsuario[0].IDSetor;
            idempresa = ds.vwVinculoUsuario[0].IDEmpresa;
            idtpusuario = ds.vwVinculoUsuario[0].IDTPUsuario;
            totalhoradiaria = ds.vwVinculoUsuario[0].TotalHoraDia;
            dsempresa = ds.vwVinculoUsuario[0].DSEmpresa;
            idvinculousuario = ds.vwVinculoUsuario[0].IDVinculoUsuario;
        }
    }
}