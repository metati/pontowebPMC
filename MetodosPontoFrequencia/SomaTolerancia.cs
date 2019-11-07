using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetodosPontoFrequencia
{
    public class SomaTolerancia
    {
        private string Total;
        private bool CargaIncompleta;
        private string horarioAlmoco;

        public bool JornadaIncompleta
        {
            get
            {
                return CargaIncompleta;
            }
        }
        public string TotalRestante
        {
            get
            {
                return horarioAlmoco;
            }
        }

        public bool Tolerancia(DateTime EntradaPadrao, DateTime Batida)
        {
            //Tolerância de 15 minutos na entrada
            //true para chegou no horário e False para não chegou no horário
            if (Batida <= EntradaPadrao)
            {
                return true;
            }
            else if (Batida > EntradaPadrao.AddMinutes(15))
                return false;
            else
                return true;
        }
        public int TotalDia(DateTime? Entrada1, DateTime? Saida1, DateTime? Entrada2, DateTime? Saida2,int TotalJornadaDia)
        {
            if ((Entrada1 != null) && (Saida1 != null) && (Entrada2 != null) && (Saida2 != null))
            {
                Total = ((Saida1 - Entrada1) + (Saida2 - Entrada2)).ToString();
                MSGTotalJornada(Total, TotalJornadaDia);
                return Convert.ToInt32(TimeSpan.Parse(Total).TotalSeconds);
            }

            if ((Entrada1 != null) && (Saida1 != null))
            {
                Total = ((Saida1 - Entrada1)).ToString();
                
                if (TotalJornadaDia != 8)
                    MSGTotalJornada(Total, TotalJornadaDia);
                else
                    CargaIncompleta = false;

                return Convert.ToInt32(TimeSpan.Parse(Total).TotalSeconds);
            }

            if ((Entrada2 != null) && (Saida2 != null))
            {
                Total = ((Saida2 - Entrada2)).ToString();
                MSGTotalJornada(Total, TotalJornadaDia);
                
                return Convert.ToInt32(TimeSpan.Parse(Total).TotalSeconds);
            }

            //Caso não entre em nehum desses, retornar 0
            return 0;
        }

        private void MSGTotalJornada(string Total, int TotalJornadaDia)
        {
            //Por enquanto fixo. Mas mudar para as cargas horárias contidas em banco de dados.
            TimeSpan minutoTolerancia = TimeSpan.Parse("00:15:00");

            Total = (TimeSpan.Parse(Total) + minutoTolerancia).ToString();

            switch (TotalJornadaDia)
            {
                case 4:
                    if (TimeSpan.Parse(Total) < TimeSpan.Parse("04:00:00"))
                    {
                        CargaIncompleta = true;
                    }
                    else
                        CargaIncompleta = false;
                    break;
                case 6:
                    if (TimeSpan.Parse(Total) < TimeSpan.Parse("06:00:00"))
                    {
                        CargaIncompleta = true;
                    }
                    else
                        CargaIncompleta = false;
                    break;
                case 8:
                    if ((TimeSpan.Parse(Total) < TimeSpan.Parse("08:00:00")))
                    {
                        CargaIncompleta = true;
                    }
                    else
                        CargaIncompleta = false;
                    break;
                case 12:
                    if ((TimeSpan.Parse(Total) < TimeSpan.Parse("12:00:00")))
                    {
                        CargaIncompleta = true;
                    }
                    else
                        CargaIncompleta = false;
                    break;
                case 24:
                    if ((TimeSpan.Parse(Total) < TimeSpan.Parse("24:00:00")))
                    {
                        CargaIncompleta = true;
                    }
                    else
                        CargaIncompleta = false;
                    break;
            }
        }
        public bool PermiteRegistroAlmoco(DateTime Saida1, DateTime Entrada2)
        {
            //Mínio de 1 hora para o almoço.
            Total = (Saida1 - Entrada2).ToString();

            if (TimeSpan.Parse(Total) <= TimeSpan.Parse("01:00:00"))
            {
                horarioAlmoco = (TimeSpan.Parse("01:00:00") - TimeSpan.Parse(Total)).ToString();
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool PermiteRegistroTolerancia(DateTime Saida, DateTime Entrada)
        {
            //Mínimo de 1 hora para o almoço.
            Total = (Saida - Entrada).ToString();

            if (TimeSpan.Parse(Total) <= TimeSpan.Parse("00:10:00"))
            {
                horarioAlmoco = Total;
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool PermiteRegistroTolerancia5Min(DateTime Saida, DateTime Entrada)
        {
            //Mínimo de 1 hora para o almoço.
            Total = (Saida - Entrada).ToString();

            if (TimeSpan.Parse(Total) <= TimeSpan.Parse("00:05:00"))
            {
                horarioAlmoco = Total;
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
