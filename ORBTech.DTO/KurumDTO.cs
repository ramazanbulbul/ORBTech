using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORBTech.DTO
{
    public class KurumDTO
    {
        public int KURUM_ID { get; set; }
        public int KURUM_TIPI { get; set; } //0-Belirsiz 1-Tedarikçi 2-Müşteri 3- Tedarikçi/Müşteri
        public string KURUM_ADI { get; set; }
        public string KURUM_ACIKLAMA { get; set; }
        public string KURUM_ADRESI { get; set; }
        public string KURUM_HESAP_NO { get; set; }
        public string KURUM_IBAN { get; set; }
        public string KURUM_TC_NO { get; set; }
        public string KURUM_VERGI_NO { get; set; }
        public string KURUM_VERGI_DAIRESI { get; set; }
        public string KURUM_YETKILI_UNVAN { get; set; }
        public string KURUM_YETKILI_ADSOYAD { get; set; }
        public string KURUM_YETKILI_TEL { get; set; }
        public UserDTO USER { get; set; }
        public string USER_DATE { get; set; }
        public UserDTO UPD_USER { get; set; }
        public string UPD_USER_DATE { get; set; }
    }
}
