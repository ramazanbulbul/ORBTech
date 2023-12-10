using ORBTech.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ORBTech.Business
{
    public class BlogBusiness : BaseBusiness
    {
        private BlogDTO db2DTO(DataRow row)
        {
            UserBusiness userBusiness = new UserBusiness();

            UserDTO USER = string.IsNullOrEmpty(row["USER_KODU"].ToString()) ? new UserDTO() { USER_KODU=-1 } : userBusiness.GetUser((int)row["USER_KODU"]) ?? new UserDTO() { USER_KODU = (int)row["USER_KODU"] };
            UserDTO UPD_USER = string.IsNullOrEmpty(row["UPD_USER_KODU"].ToString()) ? new UserDTO() { USER_KODU = -1 } : userBusiness.GetUser((int)row["UPD_USER_KODU"]) ?? new UserDTO() { USER_KODU = (int)row["UPD_USER_KODU"] };
            UserDTO IPT_USER = string.IsNullOrEmpty(row["IPT_USER_KODU"].ToString())? new UserDTO() { USER_KODU = -1 } : userBusiness.GetUser((int)row["IPT_USER_KODU"]) ?? new UserDTO() { USER_KODU = (int)row["IPT_USER_KODU"] };

            BlogDTO blog = new BlogDTO();
            blog.BLOG_ID = (int)row["BLOG_ID"];
            blog.BASLIK = row["BASLIK"].ToString();
            blog.ACIKLAMA = row["ACIKLAMA"].ToString();
            blog.KISA_ACIKLAMA = row["KISA_ACIKLAMA"].ToString();
            blog.RESIM_URL = row["RESIM_URL"].ToString();
            blog.USER = USER;
            blog.USER_DATE = row["USER_DATE"].ToString();
            blog.UPD_USER = UPD_USER;
            blog.UPD_USER_DATE = row["UPD_USER_DATE"].ToString();
            blog.IPT_KODU = (int)row["IPT_KODU"];
            blog.IPT_USER = IPT_USER;
            blog.IPT_USER_DATE = row["IPT_USER_DATE"].ToString();
            return blog;
        }
        public List<BlogDTO> GetBlogList()
        {
            List<BlogDTO> blogs = new List<BlogDTO>();
            string query = "SELECT * FROM blog WHERE IPT_KODU = 0 ORDER BY USER_DATE DESC";
            DataTable dt = GetDataTable(query);
            foreach (DataRow row in dt.Rows)
            {
                BlogDTO blog = db2DTO(row);
                blogs.Add(blog);
            }
            return blogs;
        }
        public BlogDTO GetBlogByBlogId(string BLOG_ID)
        {
            BlogDTO blog = null;
            string query = "SELECT * FROM blog WHERE IPT_KODU = 0 and BLOG_ID = '" + BLOG_ID + "' ORDER BY USER_DATE DESC";
            DataTable dt = GetDataTable(query);
            foreach (DataRow row in dt.Rows)
            {
                blog = db2DTO(row);
            }
            return blog;
        }

        public int SetBlog(BlogDTO blog)
        {
            string query = "";
            if (blog == null)
            {
                return -1;
            }
            if (blog.BLOG_ID == 0)
            {
                query = "INSERT INTO `blog` (`BLOG_ID`, `BASLIK`, `ACIKLAMA`, `KISA_ACIKLAMA`, `RESIM_URL`, `USER_KODU`, `USER_DATE`) " +
                    "VALUES (NULL, '" + blog.BASLIK + "', '" + blog.ACIKLAMA + "', '" + blog.KISA_ACIKLAMA + "', '" + blog.RESIM_URL + "', '" + blog.USER.USER_KODU + "', current_timestamp())";
            }
            else
            {
                query = "UPDATE `blog` SET " +
                            "`BASLIK` = \"" + blog.BASLIK + "\", " +
                            "`ACIKLAMA`= '" + blog.ACIKLAMA + "', " +
                            "`KISA_ACIKLAMA` = '" + blog.KISA_ACIKLAMA + "', " +
                            "`RESIM_URL`  = '" + blog.RESIM_URL + "', " +
                            "`UPD_USER_KODU` = '" + blog.UPD_USER.USER_KODU + "', " +
                            "`UPD_USER_DATE` =  current_timestamp() " +
                            " WHERE `BLOG_ID` = '" + blog.BLOG_ID + "'";
            }
            int response = SetDbData(query);
            return response;
        }
        public int RemoveBlog(UserDTO LOGON_USER, string BLOG_ID)
        {
            string query = "UPDATE `blog` SET " +
                            "`IPT_KODU`  = 1, " +
                            "`IPT_USER_KODU` = '" + LOGON_USER.USER_KODU + "', " +
                            "`IPT_USER_DATE` =  current_timestamp() " +
                            " WHERE `BLOG_ID` = '" + BLOG_ID + "'";
            int response = SetDbData(query);
            return response;
        }
    }
}
