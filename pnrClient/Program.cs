using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;

namespace pnrClient
{
    static class Program
    {
        //Prevendo mais de uma instância

        static Boolean PreInstance()
        {
            if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //if (!PreInstance())
                Application.Run(new frmPrincipal());
            //else
            //{
            //    MessageBox.Show("Não é possível abrir mais de uma instância do aplicativo ao mesmo tempo. Aguarde a finalização do segundo processo.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    Application.Exit();
            //}
        }
    }
}
