using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORBTech.DTO
{
    public class LogDTO
    {
        public string LOG_ID { get; set; }
        public string SAYFA_ANAHTARI { get; set; }
        public string GIRIS_YAPAN_IP { get; set; }
        public string GIRIS_ZAMANI { get; set; }
        public string GELDIGI_URL { get; set; }
        public string GITTIGI_URL { get; set; }
        public string TARAYICI_BILGISI { get; set; }
        public string LOGON_USER_KODU { get; set; }
        public UserDTO LOGON_USER { get; set; }
        public string PLATFORM { get; set; }
        public string VERSION { get; set; }
        public string MOBIL_CIHAZ { get; set; }
        public string MOBIL_CIHAZ_MODEL { get; set; }
    }
}
