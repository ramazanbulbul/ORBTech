using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ORBTech.DTO
{
    public class BlogDTO
    {
        public int BLOG_ID { get; set; }
        public string RESIM_URL { get; set; }
        public string BASLIK { get; set; }
        [AllowHtml]
        public string ACIKLAMA { get; set; }
        public string KISA_ACIKLAMA { get; set; }
        public UserDTO USER { get; set; }
        public string USER_DATE  { get; set; }
        public UserDTO UPD_USER { get; set; }
        public string UPD_USER_DATE { get; set; }
        public int IPT_KODU { get; set; }
        public UserDTO IPT_USER { get; set; }
        public string IPT_USER_DATE { get; set; }
    }
}
