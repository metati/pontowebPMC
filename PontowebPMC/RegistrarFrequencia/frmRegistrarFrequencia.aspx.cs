using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NBioBSPCOMLib;
using MetodosPontoFrequencia;

public partial class RegistrarFrequencia_frmRegistrarFrequencia : System.Web.UI.Page
{
    DataSetUsuario dsU = new DataSetUsuario();
    MetodosPontoFrequencia.DataSetUsuarioTableAdapters.vwUsuarioWebServiceTableAdapter adpVWUsuer = new MetodosPontoFrequencia.DataSetUsuarioTableAdapters.vwUsuarioWebServiceTableAdapter();

    public NBioBSPCOMLib.NBioBSP objNBioBSP;
    public NBioBSPCOMLib.IDevice objDevice;
    public NBioBSPCOMLib.IExtraction objExtraction;
    public NBioBSPCOMLib.IMatching objMatching;
    public NBioBSPCOMLib.IIndexSearch m_index;
    public NBioBSPCOMLib.IFPImage objImage;
    public string hNewFIR;
    public string Ztext;

    protected void PreencherFIR(int IDEmpresa)
    {
        dsU.EnforceConstraints = false;
        adpVWUsuer.FillIDEmpresa(dsU.vwUsuarioWebService, IDEmpresa);

        for (int i = 0; i <= (dsU.vwUsuarioWebService.Rows.Count - 1); i++)
        {
            if (!dsU.vwUsuarioWebService[i].IsTextHashCodeNull())
            {
                hNewFIR = string.Empty;
                hNewFIR = dsU.vwUsuarioWebService[i].TextHashCode;
                m_index.AddFIR(hNewFIR, dsU.vwUsuarioWebService[i].IDUsuario);
            }
        }

    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        objNBioBSP = new NBioBSPCOMLib.NBioBSPClass();
        objDevice = (NBioBSPCOMLib.IDevice)objNBioBSP.Device; //Dispositivo da leitora
        objExtraction = (NBioBSPCOMLib.IExtraction)objNBioBSP.Extraction; //Extraindo novas digitais
        m_index = (NBioBSPCOMLib.IIndexSearch)objNBioBSP.IndexSearch; //Anexando Digitais do banco
        objImage = (NBioBSPCOMLib.IFPImage)objNBioBSP.FPImage; //Pegando imagem para Caixa de imagem

        //if (!IsPostBack)
        //{
        //Preenchendo fir.
        PreencherFIR(4);
        //}
    }


    protected void registrarFrequencia()
    {
        // Fecha máquina se estiver aberta.
        objDevice.Close(objDevice.OpenedDeviceID);

        //Abre máquina
        objDevice.Open(NBioBSPType.DEVICE_ID.AUTO);

        if (objDevice.ErrorCode == NBioBSPError.NONE)
        {
            objExtraction.Capture((int)NBioBSPType.FIR_PURPOSE.DENTIFY);

            objDevice.Close(NBioBSPType.DEVICE_ID.AUTO);

            //ImagemBox();

            Ztext = "";
            Ztext = objExtraction.TextEncodeFIR;

            m_index.IdentifyUser(Ztext, 9);

            if (m_index.ErrorCode == 0)
            {
                Frequencia freq = new Frequencia();
                TextBox1.Text = freq.BaterPonto2(m_index.UserID, 4, System.DateTime.Now, 7);

                TextBox1.DataBind();
            }
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        registrarFrequencia();
    }
}