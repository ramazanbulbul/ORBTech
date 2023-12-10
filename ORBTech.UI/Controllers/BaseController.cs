using ORBTech.Business;
using ORBTech.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ORBTech.UI.Controllers
{
    public class BaseController : Controller
    {
        public UserBusiness userBusiness;
        public SliderBusiness sliderBusiness;
        public BlogBusiness blogBusiness;
        public UrunBusiness urunBusiness;
        public LogBusiness logBusiness;
        public KurumBusiness kurumBusiness;
        public FaturaBusiness faturaBusiness;
        public UserDTO LOGON_USER;
        public UserDTO LOGON_FATURA_USER;
        // GET: Base
        public BaseController()
        {
            userBusiness = new UserBusiness();
            sliderBusiness = new SliderBusiness();
            blogBusiness = new BlogBusiness();
            urunBusiness = new UrunBusiness();
            logBusiness = new LogBusiness();
            kurumBusiness = new KurumBusiness();
            faturaBusiness = new FaturaBusiness();
        }
        public void CreateLog(string SAYFA_ANAHTARI)
        {
            LogDTO log = new LogDTO();
            var browser = Request.Browser;
            log.SAYFA_ANAHTARI = SAYFA_ANAHTARI;
            log.TARAYICI_BILGISI = browser.Browser;
            log.VERSION = browser.Version;
            log.PLATFORM = browser.Platform;
            log.MOBIL_CIHAZ = browser.IsMobileDevice ? "1" : "0";
            log.MOBIL_CIHAZ_MODEL = browser.MobileDeviceModel;
            log.GELDIGI_URL = Request.UrlReferrer?.ToString();
            log.GITTIGI_URL = Request.Url?.ToString();
            log.GIRIS_YAPAN_IP = Request.UserHostAddress == null ? Request.ServerVariables["HTTP_X_FORWARDED_FOR"] + ": " + Request.ServerVariables["REMOTE_ADDR"] : Request.UserHostAddress;
            log.LOGON_USER_KODU = Session["LOGON_USER"] != null ? ((UserDTO)Session["LOGON_USER"]).USER_KODU.ToString() : "GUEST";
            logBusiness.setLog(log);
        }
    }
}