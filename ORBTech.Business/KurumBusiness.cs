using ORBTech.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORBTech.Business
{
    public class KurumBusiness : BaseBusiness
    {
        private KurumDTO db2DTO(DataRow row)
        {
            UserBusiness userBusiness = new UserBusiness();
            UserDTO USER = string.IsNullOrEmpty(row["USER_KODU"].ToString()) ? new UserDTO() { USER_KODU = -1 } : userBusiness.GetUser((int)row["USER_KODU"]) ?? new UserDTO() { USER_KODU = (int)row["USER_KODU"] };
            UserDTO UPD_USER = string.IsNullOrEmpty(row["UPD_USER_KODU"].ToString()) ? new UserDTO() { USER_KODU = -1 } : userBusiness.GetUser((int)row["UPD_USER_KODU"]) ?? new UserDTO() { USER_KODU = (int)row["UPD_USER_KODU"] };

            KurumDTO kurum = new KurumDTO();
            kurum.KURUM_ID              = (int)row["KURUM_ID"];
            kurum.KURUM_TIPI            = (int)row["KURUM_TIPI"];
            kurum.KURUM_ADI             = row["ADI"].ToString();
            kurum.KURUM_ACIKLAMA        = row["ACIKLAMA"].ToString();
            kurum.KURUM_ADRESI          = row["ADRESI"].ToString();
            kurum.KURUM_HESAP_NO        = row["HESAP_NO"].ToString();
            kurum.KURUM_IBAN            = row["IBAN"].ToString();
            kurum.KURUM_TC_NO           = row["TC_NO"].ToString();
            kurum.KURUM_VERGI_NO        = row["VERGI_NO"].ToString();
            kurum.KURUM_VERGI_DAIRESI   = row["VERGI_DAIRESI"].ToString();
            kurum.KURUM_YETKILI_UNVAN   = row["YETKILI_UNVAN"].ToString();
            kurum.KURUM_YETKILI_ADSOYAD = row["YETKILI_ADSOYAD"].ToString();
            kurum.KURUM_YETKILI_TEL     = row["YETKILI_TEL"].ToString();
            kurum.USER                  = USER;
            kurum.USER_DATE             = row["USER_DATE"].ToString();
            kurum.UPD_USER              = UPD_USER;
            kurum.UPD_USER_DATE         = row["UPD_USER_DATE"].ToString();
            
            return kurum;
        }
        public List<KurumDTO> GetKurumList()
        {
            List<KurumDTO> kurums = new List<KurumDTO>();
            string query = "SELECT * FROM kurum WHERE IPT_KODU = 0 ORDER BY KURUM_ID DESC";
            DataTable dt = GetDataTable(query);
            foreach (DataRow row in dt.Rows)
            {
                KurumDTO kurum = db2DTO(row);
                kurums.Add(kurum);
            }
            return kurums;
        }
        public List<KurumDTO> GetKurumListByPasif(int PASIF)
        {
            List<KurumDTO> kurums = new List<KurumDTO>();
            string query = "SELECT * FROM kurum WHERE IPT_KODU = 0 ORDER BY KURUM_ID DESC";
            DataTable dt = GetDataTable(query);
            foreach (DataRow row in dt.Rows)
            {
                KurumDTO kurum = db2DTO(row);
                kurums.Add(kurum);
            }
            return kurums;
        }
        public KurumDTO GetKurumByKurumId(string KURUM_ID)
        {
            KurumDTO kurum = null;
            string query = "SELECT * FROM kurum WHERE IPT_KODU = 0 and KURUM_ID = '" + KURUM_ID + "'";
            DataTable dt = GetDataTable(query);
            foreach (DataRow row in dt.Rows)
            {
                kurum = db2DTO(row);
            }
            return kurum;
        }

        public int SetKurum(KurumDTO kurum)
        {
            string query = "";
            if (kurum == null)
            {
                return -1;
            }
            if (kurum.KURUM_ID == 0)
            {
                query = "INSERT INTO `kurum` (`KURUM_ID`, `KURUM_TIPI`, `ADI`, `ACIKLAMA`, `ADRESI`, `HESAP_NO`,`IBAN`,`TC_NO`,`VERGI_NO`,`VERGI_DAIRESI`,`YETKILI_UNVAN`,`YETKILI_ADSOYAD`,`YETKILI_TEL`,`USER_KODU`, `USER_DATE`) " +
                    "VALUES (NULL, '" + kurum.KURUM_TIPI + "', '" + kurum.KURUM_ADI + "', '" + kurum.KURUM_ACIKLAMA + "', '" + kurum.KURUM_ADRESI + "', '" + kurum.KURUM_HESAP_NO + "', '" + kurum.KURUM_IBAN + "', '" + kurum.KURUM_TC_NO + "','" + kurum.KURUM_VERGI_NO + "','" + kurum.KURUM_VERGI_DAIRESI + "','" + kurum.KURUM_YETKILI_UNVAN + "','" + kurum.KURUM_YETKILI_ADSOYAD + "','" + kurum.KURUM_YETKILI_TEL + "','" + kurum.USER.USER_KODU + "', current_timestamp())";
            }
            else
            {
                query = "UPDATE `kurum` SET " +
                            "`KURUM_TIPI` = \"" + kurum.KURUM_TIPI + "\", " +
                            "`ADI`= '" + kurum.KURUM_ADI + "', " +
                            "`ACIKLAMA` = '" + kurum.KURUM_ACIKLAMA + "', " +
                            "`ADRESI`  = '" + kurum.KURUM_ADRESI + "', " +
                            "`HESAP_NO`  = '" + kurum.KURUM_HESAP_NO + "', " +
                            "`IBAN`  = '" + kurum.KURUM_IBAN + "', " +
                            "`TC_NO` = '" + kurum.KURUM_TC_NO + "', " +
                            "`VERGI_NO` = '" + kurum.KURUM_VERGI_NO+ "', " +
                            "`VERGI_DAIRESI` = '" + kurum.KURUM_VERGI_DAIRESI + "', " +
                            "`YETKILI_UNVAN` = '" + kurum.KURUM_YETKILI_UNVAN + "', " +
                            "`YETKILI_ADSOYAD` = '" + kurum.KURUM_YETKILI_ADSOYAD + "', " +
                            "`YETKILI_TEL` = '" + kurum.KURUM_YETKILI_TEL + "', " +
                            "`UPD_USER_KODU` = '" + kurum.UPD_USER.USER_KODU + "', " +
                            "`UPD_USER_DATE` =  current_timestamp() " +
                            " WHERE `KURUM_ID` = '" + kurum.KURUM_ID + "'";
            }
            int response = SetDbData(query);
            return response;
        }
        public int RemoveKurum(UserDTO LOGON_USER, string KURUM_ID)
        {
            string query = "UPDATE `kurum` SET " +
                            "`IPT_KODU`  = 1, " +
                            "`IPT_USER_KODU` = '" + LOGON_USER.USER_KODU + "', " +
                            "`IPT_USER_DATE` =  current_timestamp() " +
                            " WHERE `KURUM_ID` = '" + KURUM_ID + "'";
            int response = SetDbData(query);
            return response;
        }
    }
}
