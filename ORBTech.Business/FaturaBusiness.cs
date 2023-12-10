using ORBTech.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORBTech.Business
{
    public class FaturaBusiness : BaseBusiness
    {
        private FaturaDTO db2DTO(DataRow row)
        {
            UserBusiness userBusiness = new UserBusiness();
            KurumBusiness kurumBusiness = new KurumBusiness();
            UserDTO USER = string.IsNullOrEmpty(row["USER_KODU"].ToString()) ? new UserDTO() { USER_KODU = -1 } : userBusiness.GetUser((int)row["USER_KODU"]) ?? new UserDTO() { USER_KODU = (int)row["USER_KODU"] };
            UserDTO UPD_USER = string.IsNullOrEmpty(row["UPD_USER_KODU"].ToString()) ? new UserDTO() { USER_KODU = -1 } : userBusiness.GetUser((int)row["UPD_USER_KODU"]) ?? new UserDTO() { USER_KODU = (int)row["UPD_USER_KODU"] };
            KurumDTO KURUM = string.IsNullOrEmpty(row["KURUM_ID"].ToString()) ? new KurumDTO() { KURUM_ID = -1 } : kurumBusiness.GetKurumByKurumId(row["KURUM_ID"].ToString()) ?? new KurumDTO() { KURUM_ID = (int)row["KURUM_ID"] };

            FaturaDTO fatura = new FaturaDTO();
            fatura.FATURA_ID            = (int)row["FATURA_ID"];
            fatura.FATURA_TIPI          = (int)row["FATURA_TIPI"];
            fatura.KURUM                = KURUM;
            fatura.KDVSIZ_FATURA_TUTARI = row["KDVSIZ_FATURA_TUTARI"].ToString();
            fatura.KDV_ORANI            = row["KDV_ORANI"].ToString();
            fatura.FATURA_URL           = row["FATURA_URL"].ToString();
            fatura.FATURA_KESIM_TARIHI  = row["FATURA_KESIM_TARIHI"].ToString();
            fatura.USER                 = USER;
            fatura.USER_DATE            = row["USER_DATE"].ToString();
            fatura.UPD_USER             = UPD_USER;
            fatura.UPD_USER_DATE        = row["UPD_USER_DATE"].ToString();
            
            return fatura;
        }
        public List<FaturaDTO> GetFaturaList()
        {
            List<FaturaDTO> faturas = new List<FaturaDTO>();
            string query = "SELECT * FROM fatura WHERE IPT_KODU = 0 ORDER BY FATURA_KESIM_TARIHI DESC";
            DataTable dt = GetDataTable(query);
            foreach (DataRow row in dt.Rows)
            {
                FaturaDTO fatura = db2DTO(row);
                faturas.Add(fatura);
            }
            return faturas;
        }
        public FaturaDTO GetFaturaByFaturaId(string FATURA_ID)
        {
            FaturaDTO fatura = null;
            string query = "SELECT * FROM fatura WHERE IPT_KODU = 0 and FATURA_ID = '" + FATURA_ID + "'";
            DataTable dt = GetDataTable(query);
            foreach (DataRow row in dt.Rows)
            {
                fatura = db2DTO(row);
            }
            return fatura;
        }

        public int SetFatura(FaturaDTO fatura)
        {
            string query = "";
            if (fatura == null)
            {
                return -1;
            }
            if (fatura.FATURA_ID == 0)
            {
                query = "INSERT INTO `fatura` (`FATURA_ID`, `FATURA_TIPI`, `KURUM_ID`, `KDVSIZ_FATURA_TUTARI`, `KDV_ORANI`, `FATURA_URL`,`FATURA_KESIM_TARIHI`,`USER_KODU`,`USER_DATE`) " +
                    "VALUES (NULL, '" + fatura.FATURA_TIPI + "', '" + fatura.KURUM.KURUM_ID + "', '" + fatura.KDVSIZ_FATURA_TUTARI + "', '" + fatura.KDV_ORANI + "', '" + fatura.FATURA_URL + "', '" + fatura.FATURA_KESIM_TARIHI + "', '" + fatura.USER.USER_KODU + "', current_timestamp())";
            }
            else
            {
                query = "UPDATE `fatura` SET ";
                query += "`FATURA_TIPI` = \"" + fatura.FATURA_TIPI + "\", ";
                query += "`KURUM_ID`= '" + fatura.KURUM.KURUM_ID + "', ";
                query += "`KDVSIZ_FATURA_TUTARI` = '" + fatura.KDVSIZ_FATURA_TUTARI + "', ";
                query += "`KDV_ORANI`  = '" + fatura.KDV_ORANI + "', ";
                query += string.IsNullOrEmpty(fatura.FATURA_URL) ? "" : "`FATURA_URL`  = '" + fatura.FATURA_URL + "', ";
                query += "`FATURA_KESIM_TARIHI`  = '" + fatura.FATURA_KESIM_TARIHI + "', ";
                query += "`UPD_USER_KODU` = '" + fatura.UPD_USER.USER_KODU + "', ";
                query += "`UPD_USER_DATE` =  current_timestamp() ";
                query += " WHERE `FATURA_ID` = '" + fatura.FATURA_ID + "'";
            }
            int response = SetDbData(query);
            return response;
        }
        public int RemoveFatura(UserDTO LOGON_USER, string FATURA_ID)
        {
            string query = "UPDATE `fatura` SET " +
                            "`IPT_KODU`  = 1, " +
                            "`IPT_USER_KODU` = '" + LOGON_USER.USER_KODU + "', " +
                            "`IPT_USER_DATE` =  current_timestamp() " +
                            " WHERE `FATURA_ID` = '" + FATURA_ID + "'";
            int response = SetDbData(query);
            return response;
        }
    }
}
