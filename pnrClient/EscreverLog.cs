using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace pnrClient
{
    public class EscreverLog
    {
        public void Esrevelog(string msg, System.Windows.Forms.ListBox lbLog)
        {
            lbLog.Items.Clear();
            lbLog.Items.Add(msg);
            lbLog.Update();
        }

        public void EscreverLogUser(string msg, System.Windows.Forms.ListBox lbLog)
        {
            lbLog.SelectedIndex = lbLog.Items.Count - 1;
            lbLog.Items.Add(msg);
            lbLog.Update();
        }
    }
}
