using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORBTech.Business
{
    public class BaseBusiness
    {
        private string connetionString = ConfigurationManager.ConnectionStrings["csOrbTech"].ConnectionString;
        private MySqlConnection conn;
        public DataTable GetDataTable(string query)
        {
            DataTable dt = new DataTable();
            using (conn = new MySqlConnection(connetionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
                conn.Close();
            }
            return dt;
        }
        public int SetDbData(string query)
        {
            int response = -1;
            using (conn = new MySqlConnection(connetionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                response = cmd.ExecuteNonQuery();
                conn.Close();
            }
            return response;
        }
    }
}
