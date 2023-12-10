using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORBTech.DTO
{
    public class FaturaDTO
    {
        public int          FATURA_ID               { get; set; }
        public int          FATURA_TIPI             { get; set; } //0-Belirsiz 1-Alış 2- Satış
        public KurumDTO     KURUM                   { get; set; }
        public string       KDVSIZ_FATURA_TUTARI    { get; set; }
        public string       KDV_ORANI               { get; set; }
        public string       FATURA_URL               { get; set; }
        public string       FATURA_KESIM_TARIHI     { get; set; }
        public UserDTO      USER                    { get; set; }
        public string       USER_DATE               { get; set; }
        public UserDTO      UPD_USER                { get; set; }
        public string       UPD_USER_DATE           { get; set; }
        public int          IPT_KODU                { get; set; }
        public UserDTO      IPT_USER                { get; set; }
        public string       IPT_USER_DATE           { get; set; }
    }
}
