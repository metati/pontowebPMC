using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using DevExpress.Web.ASPxUploadControl;
using DevExpress.Web.ASPxEditors;
using System.Drawing;

public partial class Content_upload : System.Web.UI.Page
{

    protected byte[] ImagemByte;
    const string UploadDirectory = "~/UploadImages/";
    const string ThumbnailFileName = "ThumbnailImage.jpg";

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void upFoto_FileUploadComplete(object sender, DevExpress.Web.ASPxUploadControl.FileUploadCompleteEventArgs e)
    {
        e.CallbackData = SalvarImagem(e.UploadedFile);

    }

    string SalvarImagem(DevExpress.Web.ASPxUploadControl.UploadedFile ArquivoUpLoad)
    {
        if (!ArquivoUpLoad.IsValid)
        {
            return string.Empty;
        }
        else
        {
            //ImagemByte = new byte[ArquivoUpLoad.PostedFile.InputStream.Length + 1];

            //ArquivoUpLoad.PostedFile.InputStream.Read(ImagemByte, 0, ImagemByte.Length);

            string fileName = Path.Combine(MapPath(UploadDirectory), ThumbnailFileName);

            using (Image original = Image.FromStream(ArquivoUpLoad.FileContent))

                
            //using (Image thumbnail = PhotoUtils.Inscribe(original, 100))
              //  {
             //     PhotoUtils.SaveToJpeg(thumbnail, fileName);
             //   }

                //SubirFoto(Convert.ToInt32(coIDUsuario.Value), Convert.ToInt32(Session["IDEmpresa"]), ImagemByte);

               ArquivoUpLoad.SaveAs(fileName,true);

               return ThumbnailFileName;
        }
    }


}