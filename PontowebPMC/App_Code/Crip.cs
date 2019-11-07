using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MetodosPontoFrequencia;

/// <summary>
/// Summary description for Crip
/// </summary>
public class Crip
{
	public Crip()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    Cript Crpt = new Cript();

    public string CriptograFa(string stringDescritografada)
    {
        return Crpt.ActionEncrypt(stringDescritografada);
    }

    public string Descriptogra(string stringCriptografada)
    {
        if (stringCriptografada != "")
            return Crpt.ActionDecrypt(stringCriptografada);
        else
            return "0";
    }
}