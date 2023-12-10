using ORBTech.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORBTech.Business
{
    public class LogBusiness : BaseBusiness
    {
        public LogDTO db2dto(DataRow row)
        {
            LogDTO log = new LogDTO();
            log.GELDIGI_URL = row["GELDIGI_URL"].ToString();
            log.GIRIS_YAPAN_IP = row["GIRIS_YAPAN_IP"].ToString();
            log.GIRIS_ZAMANI = row["GIRIS_ZAMANI"].ToString();
            log.GITTIGI_URL = row["GITTIGI_URL"].ToString();
            log.LOGON_USER_KODU = row["LOGON_USER_KODU"].ToString();
            log.LOG_ID = row["LOG_ID"].ToString();
            log.MOBIL_CIHAZ = row["MOBIL_CIHAZ"].ToString();
            log.MOBIL_CIHAZ_MODEL = row["MOBIL_CIHAZ_MODEL"].ToString();
            log.PLATFORM = row["PLATFORM"].ToString();
            log.SAYFA_ANAHTARI = row["SAYFA_ANAHTAR"].ToString();
            log.TARAYICI_BILGISI = row["TARAYICI_BILGISI"].ToString();
            log.VERSION = row["VERSION"].ToString();
            return log;
        }

        public List<LogDTO> GetLogsList()
        {
            List<LogDTO> logs = new List<LogDTO>();
            string query = "SELECT * FROM `log_tablosu` order by LOG_ID desc";
            DataTable dt = GetDataTable(query);
            foreach (DataRow row in dt.Rows)
            {
                LogDTO log = db2dto(row);
                logs.Add(log);
            }
            return logs;
        }

        public int setLog(LogDTO log)
        {
            string query = "INSERT INTO `log_tablosu` (`LOG_ID`, `SAYFA_ANAHTAR`, `GIRIS_YAPAN_IP`, `GIRIS_ZAMANI`, `GELDIGI_URL`,`GITTIGI_URL`, `TARAYICI_BILGISI`, `LOGON_USER_KODU`, `PLATFORM`, `MOBIL_CIHAZ`, `MOBIL_CIHAZ_MODEL`, `VERSION`) " +
                         "VALUES (NULL, '" + log.SAYFA_ANAHTARI + "', '" + log.GIRIS_YAPAN_IP + "', current_timestamp(), '" + log.GELDIGI_URL + "','" + log.GITTIGI_URL + "', '" + log.TARAYICI_BILGISI + "', '" + log.LOGON_USER_KODU + "', '" + log.PLATFORM + "', '" + log.MOBIL_CIHAZ + "', '" + log.MOBIL_CIHAZ_MODEL + "', '" + log.VERSION + "')";
            int response = SetDbData(query);
            return response;
        } 
    }
}
