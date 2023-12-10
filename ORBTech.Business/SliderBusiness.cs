using ORBTech.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORBTech.Business
{
    public class SliderBusiness : BaseBusiness
    {
        private SliderDTO db2DTO(DataRow row)
        {
            UserBusiness userBusiness = new UserBusiness();

            UserDTO USER = string.IsNullOrEmpty(row["USER_KODU"].ToString()) ? new UserDTO() { USER_KODU = -1 } : userBusiness.GetUser((int)row["USER_KODU"]) ?? new UserDTO() { USER_KODU = (int)row["USER_KODU"] };
            UserDTO UPD_USER = string.IsNullOrEmpty(row["UPD_USER_KODU"].ToString()) ? new UserDTO() { USER_KODU = -1 } : userBusiness.GetUser((int)row["UPD_USER_KODU"]) ?? new UserDTO() { USER_KODU = (int)row["UPD_USER_KODU"] };
            UserDTO IPT_USER = string.IsNullOrEmpty(row["IPT_USER_KODU"].ToString()) ? new UserDTO() { USER_KODU = -1 } : userBusiness.GetUser((int)row["IPT_USER_KODU"]) ?? new UserDTO() { USER_KODU = (int)row["IPT_USER_KODU"] };

            SliderDTO slider = new SliderDTO();
            slider.SLIDER_ID = (int)row["SLIDER_ID"];
            slider.BASLIK = row["BASLIK"].ToString();
            slider.ACIKLAMA = row["ACIKLAMA"].ToString();
            slider.WEB_URL = row["WEB_URL"].ToString();
            slider.RESIM_URL = row["RESIM_URL"].ToString();
            slider.SIRA_NO = (int)row["SIRA_NO"];
            slider.PASIF = (int)row["PASIF"];
            slider.USER = USER;
            slider.USER_DATE = row["USER_DATE"].ToString();
            slider.UPD_USER = UPD_USER;
            slider.UPD_USER_DATE = row["UPD_USER_DATE"].ToString();
            slider.IPT_KODU = (int)row["IPT_KODU"];
            slider.IPT_USER = IPT_USER;
            slider.IPT_USER_DATE = row["IPT_USER_DATE"].ToString();
            return slider;
        }
        public List<SliderDTO> GetSliderList()
        {
            List<SliderDTO> sliders = new List<SliderDTO>();
            string query = "SELECT * FROM slider WHERE IPT_KODU = 0 ORDER BY SIRA_NO";
            DataTable dt = GetDataTable(query);
            foreach (DataRow row in dt.Rows)
            {
                SliderDTO slider = db2DTO(row);
                sliders.Add(slider);
            }
            return sliders;
        }
         public List<SliderDTO> GetSliderListByPasif(int PASIF)
        {
            List<SliderDTO> sliders = new List<SliderDTO>();
            string query = "SELECT * FROM slider WHERE IPT_KODU = 0 AND PASIF = '" + PASIF + "' ORDER BY SIRA_NO";
            DataTable dt = GetDataTable(query);
            foreach (DataRow row in dt.Rows)
            {
                SliderDTO slider = db2DTO(row);
                sliders.Add(slider);
            }
            return sliders;
        }
        public SliderDTO GetSliderBySliderId(string SLIDER_ID)
        {
            SliderDTO slider = null;
            string query = "SELECT * FROM slider WHERE IPT_KODU = 0 and SLIDER_ID = '" + SLIDER_ID + "'";
            DataTable dt = GetDataTable(query);
            foreach (DataRow row in dt.Rows)
            {
                slider = db2DTO(row);
            }
            return slider;
        }

        public int SetSlider(SliderDTO slider)
        {
            string query = "";
            if (slider == null)
            {
                return -1;
            }
            if (slider.SLIDER_ID == 0)
            {
                query = "INSERT INTO `slider` (`SLIDER_ID`, `BASLIK`, `ACIKLAMA`, `WEB_URL`, `RESIM_URL`, `SIRA_NO`,`PASIF`,`USER_KODU`, `USER_DATE`) " +
                    "VALUES (NULL, '" + slider.BASLIK + "', '" + slider.ACIKLAMA + "', '" + slider.WEB_URL + "', '" + slider.RESIM_URL + "', '"+ slider.SIRA_NO + "', '"+ slider.PASIF + "', '" + slider.USER.USER_KODU + "', current_timestamp())";
            }
            else
            {
                query = "UPDATE `slider` SET " +
                            "`BASLIK` = \"" + slider.BASLIK + "\", " +
                            "`ACIKLAMA`= '" + slider.ACIKLAMA + "', " +
                            "`WEB_URL` = '" + slider.WEB_URL + "', " +
                            "`RESIM_URL`  = '" + slider.RESIM_URL + "', " +
                            "`PASIF`  = '" + slider.PASIF + "', " +
                            "`SIRA_NO`  = '" + slider.SIRA_NO + "', " +
                            "`UPD_USER_KODU` = '" + slider.UPD_USER.USER_KODU + "', " +
                            "`UPD_USER_DATE` =  current_timestamp() " +
                            " WHERE `SLIDER_ID` = '" + slider.SLIDER_ID + "'";
            }
            int response = SetDbData(query);
            return response;
        }
        public int RemoveSlider(UserDTO LOGON_USER, string SLIDER_ID)
        {
            string query = "UPDATE `slider` SET " +
                            "`IPT_KODU`  = 1, " +
                            "`IPT_USER_KODU` = '" + LOGON_USER.USER_KODU + "', " +
                            "`IPT_USER_DATE` =  current_timestamp() " +
                            " WHERE `SLIDER_ID` = '" + SLIDER_ID + "'";
            int response = SetDbData(query);
            return response;
        }
    }
}