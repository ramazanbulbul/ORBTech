using ORBTech.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORBTech.Business
{
    public class UrunBusiness : BaseBusiness
    {
        private UrunDTO db2DTO(DataRow row)
        {
            UserBusiness userBusiness = new UserBusiness();

            UserDTO USER = string.IsNullOrEmpty(row["USER_KODU"].ToString()) ? new UserDTO() { USER_KODU = -1 } : userBusiness.GetUser((int)row["USER_KODU"]) ?? new UserDTO() { USER_KODU = (int)row["USER_KODU"] };
            UserDTO UPD_USER = string.IsNullOrEmpty(row["UPD_USER_KODU"].ToString()) ? new UserDTO() { USER_KODU = -1 } : userBusiness.GetUser((int)row["UPD_USER_KODU"]) ?? new UserDTO() { USER_KODU = (int)row["UPD_USER_KODU"] };
            UserDTO IPT_USER = string.IsNullOrEmpty(row["IPT_USER_KODU"].ToString()) ? new UserDTO() { USER_KODU = -1 } : userBusiness.GetUser((int)row["IPT_USER_KODU"]) ?? new UserDTO() { USER_KODU = (int)row["IPT_USER_KODU"] };

            UrunDTO urun = new UrunDTO();
            urun.URUN_ID = (int)row["URUN_ID"];
            urun.BASLIK = row["BASLIK"].ToString();
            urun.ACIKLAMA = row["ACIKLAMA"].ToString();
            urun.WEB_URL = row["WEB_URL"].ToString();
            urun.RESIM_URL = row["RESIM_URL"].ToString();
            urun.USER = USER;
            urun.USER_DATE = row["USER_DATE"].ToString();
            urun.UPD_USER = UPD_USER;
            urun.UPD_USER_DATE = row["UPD_USER_DATE"].ToString();
            urun.IPT_KODU = (int)row["IPT_KODU"];
            urun.IPT_USER = IPT_USER;
            urun.IPT_USER_DATE = row["IPT_USER_DATE"].ToString();
            return urun;
        }
        public List<UrunDTO> GetUrunList()
        {
            List<UrunDTO> uruns = new List<UrunDTO>();
            string query = "SELECT * FROM urun WHERE IPT_KODU = 0 ORDER BY USER_DATE DESC";
            DataTable dt = GetDataTable(query);
            foreach (DataRow row in dt.Rows)
            {
                UrunDTO urun = db2DTO(row);
                uruns.Add(urun);
            }
            return uruns;
        }
        public UrunDTO GetUrunByUrunId(string URUN_ID)
        {
            UrunDTO urun = null;
            string query = "SELECT * FROM urun WHERE IPT_KODU = 0 and URUN_ID = '" + URUN_ID + "'";
            DataTable dt = GetDataTable(query);
            foreach (DataRow row in dt.Rows)
            {
                urun = db2DTO(row);
            }
            return urun;
        }

        public int SetUrun(UrunDTO urun)
        {
            string query = "";
            if (urun == null)
            {
                return -1;
            }
            if (urun.URUN_ID == 0)
            {
                query = "INSERT INTO `urun` (`URUN_ID`, `BASLIK`,`KISA_ACIKLAMA`,  `ACIKLAMA`, `WEB_URL`, `RESIM_URL`, `USER_KODU`, `USER_DATE`) " +
                    "VALUES (NULL, '" + urun.BASLIK + "', '" + urun.KISA_ACIKLAMA + "', '" + urun.ACIKLAMA + "', '" + urun.WEB_URL + "', '" + urun.RESIM_URL + "', '" + urun.USER.USER_KODU + "', current_timestamp())";
            }
            else
            {
                query = "UPDATE `urun` SET " +
                            "`BASLIK` = \"" + urun.BASLIK + "\", " +
                            "`ACIKLAMA`= '" + urun.ACIKLAMA + "', " +
                            "`KISA_ACIKLAMA`= '" + urun.KISA_ACIKLAMA + "', " +
                            "`WEB_URL` = '" + urun.WEB_URL + "', " +
                            "`RESIM_URL`  = '" + urun.RESIM_URL + "', " +
                            "`UPD_USER_KODU` = '" + urun.UPD_USER.USER_KODU + "', " +
                            "`UPD_USER_DATE` =  current_timestamp() " +
                            " WHERE `URUN_ID` = '" + urun.URUN_ID + "'";
            }
            int response = SetDbData(query);
            return response;
        }
        public int RemoveUrun(UserDTO LOGON_USER, string URUN_ID)
        {
            string query = "UPDATE `urun` SET " +
                            "`IPT_KODU`  = 1, " +
                            "`IPT_USER_KODU` = '" + LOGON_USER.USER_KODU + "', " +
                            "`IPT_USER_DATE` =  current_timestamp() " +
                            " WHERE `URUN_ID` = '" + URUN_ID + "'";
            int response = SetDbData(query);
            return response;
        }
    }
}
