using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PontoWeb.API
{
    public partial class EscalaHorario : System.Web.UI.Page
    {
        public string IdEmpresa = "41";
        public string IdUsuario = "376";
        public string Empresa = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    IdEmpresa = Request.Form["idE"].ToString();
                    IdUsuario = Request.Form["idU"].ToString();
                }
                catch
                {
                    try
                    {
                        IdEmpresa = Request.Form["idE"].ToString();
                        IdUsuario = Request.Form["idU"].ToString();

                    }
                    catch
                    {
                        //IdEmpresa = "54";
                        //IdUsuario = "376";
                        //Response.Write("Erro...");
                    }
                }
                finally
                {
                    Empresa = Util.getScalar("SELECT DSEmpresa FROM dbo.TBEmpresa WHERE IDEmpresa = " + IdEmpresa);
                }
            }
        }
    }
}