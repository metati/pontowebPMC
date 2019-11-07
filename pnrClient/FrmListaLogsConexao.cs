using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace pnrClient
{
    public partial class FrmListaLogsConexao : Form
    {
        public FrmListaLogsConexao()
        {
            InitializeComponent();
            GetDados();
        }


        private void GetDados()
        {
            crudPtServer crud = new crudPtServer("");
            gridListaLog.DataSource = crud.GetListaLogs();
            //gridListaLog.DataBindingComplete();
        }
    }
}
