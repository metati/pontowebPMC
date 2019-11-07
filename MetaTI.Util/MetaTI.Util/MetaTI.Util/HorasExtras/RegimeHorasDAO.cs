using MetaTI.Util.Model;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace MetaTI.Util.HorasExtras
{
    public class RegimeHorasDAO
    {
        public DataTable GetRegimes()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT IDRegimeHora Codigo,DSRegimeHora RegimeHora,TotalHoraSemana,TotalHoraDia, CASE WHEN RegimePlantonista = 1 THEN 'Sim'  ELSE 'Não' END RegimePlantonista, CASE WHEN PermitehoraExtra = 1 THEN 'Sim' ELSE 'Não' END PermitehoraExtra, TotalMaximoHoraExtraDia,TotalMaximoHoraExtraMes, TotalHorasFolgaPlantonista");
            sb.AppendLine("FROM PontoFrequenciaPMC.dbo.TBRegimeHora");
            return Util.getDatset(sb.ToString());
        }

        public RegimeModel GetRegime(string IdRegimeHora)
        {
            RegimeModel rm = new RegimeModel();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("SELECT IDRegimeHora,DSRegimeHora,TotalHoraSemana,TotalHoraDia, RegimePlantonista,PermitehoraExtra, TotalMaximoHoraExtraDia,TotalMaximoHoraExtraMes, TotalHorasFolgaPlantonista");
            sb.AppendLine("FROM PontoFrequenciaPMC.dbo.TBRegimeHora");
            sb.AppendLine("WHERE IdRegimeHora = " + IdRegimeHora);
            SqlDataReader dr = Util.getDataReader(sb.ToString());
            if (dr.Read())
            {
                rm.IDRegimeHora = dr["IDRegimeHora"].ToString();
                rm.DSRegimeHora = dr["DSRegimeHora"].ToString();
                rm.TotalHoraSemana = dr["TotalHoraSemana"].ToString();
                rm.TotalHoraDia = dr["TotalHoraDia"].ToString();
                rm.RegimePlantonista = dr["RegimePlantonista"].ToString();
                rm.PermitehoraExtra = dr["PermitehoraExtra"].ToString();
                rm.TotalMaximoHoraExtraDia = dr["TotalMaximoHoraExtraDia"].ToString();
                rm.TotalMaximoHoraExtraMes = dr["TotalMaximoHoraExtraMes"].ToString();
                rm.TotalHorasFolgaPlantonista = dr["TotalHorasFolgaPlantonista"].ToString();
            }
            dr.Close();
            return rm;
        }

        public void Salvar(RegimeModel item)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("");
            sb.AppendLine("INSERT INTO dbo.TBRegimeHora");
            sb.AppendLine("(DSRegimeHora, TotalHoraSemana, TotalHoraDia, RegimePlantonista, PermitehoraExtra, TotalMaximoHoraExtraDia, TotalMaximoHoraExtraMes, TotalHorasFolgaPlantonista)");
            sb.AppendLine("VALUES");
            sb.AppendLine("(" + Util.TratarStringAspas(item.DSRegimeHora) + ", " + Util.TratarNum(item.TotalHoraSemana) + ", " + Util.TratarNum(item.TotalHoraDia) + "," + Util.TratarNum(item.RegimePlantonista) +
                ", " + Util.TratarNum(item.PermitehoraExtra) + ", " + Util.TratarNum(item.TotalMaximoHoraExtraDia) + ", " + Util.TratarNum(item.TotalMaximoHoraExtraMes) + ", " + Util.TratarNum(item.TotalHorasFolgaPlantonista) + ")");
            Util.ExecuteNonQuery(sb.ToString());
        }

        public void Alterar(RegimeModel item)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("");
            sb.AppendLine("UPDATE dbo.TBRegimeHora");
            sb.AppendLine("SET DSRegimeHora = " + Util.TratarStringAspas(item.DSRegimeHora));
            sb.AppendLine(", TotalHoraSemana = " + Util.TratarNum(item.TotalHoraSemana));
            sb.AppendLine(", TotalHoraDia = " + Util.TratarNum(item.TotalHoraDia));
            sb.AppendLine(", RegimePlantonista = " + Util.TratarNum(item.RegimePlantonista));
            sb.AppendLine(", PermitehoraExtra = " + Util.TratarNum(item.PermitehoraExtra));
            sb.AppendLine(", TotalMaximoHoraExtraDia = " + Util.TratarNum(item.TotalMaximoHoraExtraDia));
            sb.AppendLine(", TotalMaximoHoraExtraMes = " + Util.TratarNum(item.TotalMaximoHoraExtraMes));
            sb.AppendLine(", TotalHorasFolgaPlantonista = " + Util.TratarNum(item.TotalHorasFolgaPlantonista));
            sb.AppendLine("WHERE IDRegimeHora = " + Util.TratarString(item.IDRegimeHora));
            Util.ExecuteNonQuery(sb.ToString());
        }

        public void Deletar(string IDRegimeHora)
        {
            Util.ExecuteNonQuery("DELETE FROM TBRegimeHora WHERE IDRegimeHora = " + Util.TratarString(IDRegimeHora));
        }
    }
}
