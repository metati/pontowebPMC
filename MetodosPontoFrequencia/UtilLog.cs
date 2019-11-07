using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MetodosPontoFrequencia
{
    public class UtilLog
    {
        public static void EscreveLog(string Texto)
        {
            try
            {
                string path = Path.Combine("C:\\Temp\\", "log.txt");
                //if (!File.Exists(path))
                //    File.Create(path);
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Texto);
                File.AppendAllText(path, "\n" + sb.ToString());
            }
            catch (Exception e){ }
        }
    }
}
