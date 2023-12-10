using ORBTech.DTO;
using ORBTech.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORBTech.Business
{
    public class UserBusiness : BaseBusiness
    {
        private UserDTO db2DTO(DataRow row)
        {
            UserDTO user = new UserDTO();
            user.USER_KODU = (int)row["USER_KODU"];
            user.USER_KULLANICI_ADI = row["USER_KULLANICI_ADI"].ToString();
            user.USER_ADI = row["USER_ADI"].ToString();
            user.USER_EPOSTA = row["USER_EPOSTA"].ToString();
            user.USER_DATE = row["USER_DATE"].ToString();
            user.USER_PASSWORD = row["USER_PASSWORD"].ToString();
            user.PASIF = (int)row["PASIF"];
            return user;
        }
        public UserDTO GetUser(int USER_KODU)
        {
            UserDTO user = null;
            string query = "SELECT * FROM user WHERE USER_KODU=" + USER_KODU;
            DataTable dt = GetDataTable(query);
            foreach (DataRow row in dt.Rows)
            {
                user = db2DTO(dt.Rows[0]);
            }
            return user;
        }
        public UserDTO Login(string USERNAME, string PASSWORD)
        {
            PASSWORD = GeneralFunctions.CreateMD5(PASSWORD);
            DataTable dt = new DataTable();
            UserDTO user = null;
            string query = "SELECT * FROM user WHERE USER_KULLANICI_ADI = '" + USERNAME + "' AND USER_PASSWORD = '" + PASSWORD + "'";
            dt = GetDataTable(query);
            foreach (DataRow row in dt.Rows)
            {
                user = db2DTO(row);
            }
            return user;
        }
    }
}
