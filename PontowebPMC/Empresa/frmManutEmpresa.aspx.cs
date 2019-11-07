using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MetodosPontoFrequencia;

public partial class Empresa_frmManutEmpresa : System.Web.UI.Page
{

    Cadastro cad = new Cadastro();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void ucLogoEmpresa_FileUploadComplete(object sender, DevExpress.Web.ASPxUploadControl.FileUploadCompleteEventArgs e)
    {
        //string name = e.UploadedFile.FileName;
        //string url = ucLogoEmpresa.SpriteImageUrl;
        //long sizeInKilobytes = e.UploadedFile.ContentLength / 1024;
        //string sizeText = sizeInKilobytes.ToString() + " KB";
        //e.CallbackData = name + "|" + url + "|" + sizeText;
        

        cad.ImgEmpresa(36, e.UploadedFile.FileBytes);

    }
}