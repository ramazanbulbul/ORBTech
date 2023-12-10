using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORBTech.DTO
{
    public class UserDTO
    {
        public int USER_KODU { get; set; }
        public string USER_ADI { get; set; }
        public string USER_KULLANICI_ADI { get; set; }
        public string USER_PASSWORD { get; set; }
        public string USER_DATE { get; set; }
        public string USER_EPOSTA { get; set; }
        public int PASIF { get; set; }
    }
}
