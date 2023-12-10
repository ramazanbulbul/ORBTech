using ORBTech.DTO;
using ORBTech.UI.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ORBTech.Utility;
using System.IO;

namespace ORBTech.UI.Controllers
{
    public class FTRController : BaseController
    {
        // GET: FTR
        [AuthFTR]
        public ActionResult Index()
        {
            CreateLog("/FTR/Index");
            List<LogDTO> logs = new List<LogDTO>();
            logs = logBusiness.GetLogsList();
            return View(logs);
        }
        public ActionResult LoginPartial()
        {
            CreateLog("/FTR/LoginPartial");
            return View();
        }
        public ActionResult Login(string USERNAME, string PASSWORD)
        {
            CreateLog("/FTR/Login");
            UserDTO user = userBusiness.Login(USERNAME, PASSWORD);
            if (user != null)
            {
                Session["LOGON_FATURA_USER"] = user;
                LOGON_FATURA_USER = (UserDTO)Session["LOGON_FATURA_USER"];
            }
            else
            {
                ViewBag.Message = "Kullanıcı adınız veya şifreniz yanlış!";
                return View("LoginPartial");
            }
            return RedirectToAction("Index");
        }
        public ActionResult Logout()
        {
            CreateLog("/FTR/Logout");
            Session["LOGON_FATURA_USER"] = null;
            return View("LoginPartial");
        }

        #region KURUM
        [AuthFTR]
        public ActionResult KurumListesiPartial()
        {
            CreateLog("/FTR/KurumListesiPartial");
            List<KurumDTO> kurums = kurumBusiness.GetKurumList();
            return View(kurums);
        }
        [AuthFTR]
        public ActionResult KurumDuzenlePartial(string KURUM_ID)
        {
            CreateLog("/FTR/KurumDuzenlePartial");
            KurumDTO kurum = kurumBusiness.GetKurumByKurumId(KURUM_ID);
            return View(kurum);
        }
        [AuthFTR]
        public ActionResult KurumDuzenle(KurumDTO kurum)
        {
            CreateLog("/FTR/KurumDuzenle");
            LOGON_FATURA_USER = (UserDTO)Session["LOGON_FATURA_USER"];
            kurum.USER = LOGON_FATURA_USER;
            kurum.UPD_USER = LOGON_FATURA_USER;
            int response = kurumBusiness.SetKurum(kurum);
            if (response < 0)
            {
                ViewBag.Message = "Sistemde bir hata gerçekleşti! Lütfen daha sonra tekrar deneyiniz!";
            }
            else
            {
                ViewBag.Message = "İşlem başarılı!";
            }
            return RedirectToAction("KurumListesiPartial");
        }
        [AuthFTR]
        public ActionResult KurumEklePartial()
        {
            CreateLog("/FTR/KurumEklePartial");
            return View("KurumDuzenlePartial", null);
        }
        [AuthFTR]
        public ActionResult KurumSil(string KURUM_ID)
        {
            CreateLog("/FTR/KurumSil");
            LOGON_FATURA_USER = (UserDTO)Session["LOGON_FATURA_USER"];
            int response = kurumBusiness.RemoveKurum(LOGON_FATURA_USER, KURUM_ID);
            if (response < 0)
            {
                ViewBag.Message = "Sistemde bir hata gerçekleşti! Lütfen daha sonra tekrar deneyiniz!";
            }
            else
            {
                ViewBag.Message = "İşlem başarılı!";
            }
            return RedirectToAction("KurumListesiPartial");
        }
        #endregion
        #region FATURA
        [AuthFTR]
        public ActionResult FaturaListesiPartial(string TIP = "", string FATURA_ID="", string KURUM_ID = "", string FATURA_KESIM_TARIHI_MIN = "", string FATURA_KESIM_TARIHI_MAX = "")
        {
            CreateLog("/FTR/FaturaListesiPartial");
            List<FaturaDTO> faturas = faturaBusiness.GetFaturaList().Where(p=> (p.FATURA_ID.ToString() == FATURA_ID || FATURA_ID == "") 
                                                                            && (p.KURUM.KURUM_ID.ToString() == KURUM_ID || KURUM_ID == "")
                                                                            && (p.FATURA_KESIM_TARIHI.ToDateTime() < FATURA_KESIM_TARIHI_MAX.ToDateTime() || FATURA_KESIM_TARIHI_MAX == "")
                                                                            && (p.FATURA_KESIM_TARIHI.ToDateTime() > FATURA_KESIM_TARIHI_MIN.ToDateTime() || FATURA_KESIM_TARIHI_MIN == "")
                                                                            && (string.IsNullOrEmpty(TIP) || p.FATURA_TIPI.ToString() == TIP)
                                                                            ).ToList();
            ViewBag.KURUM_LIST = kurumBusiness.GetKurumList();
            ViewBag.FATURA_ID = FATURA_ID;
            ViewBag.KURUM_ID = KURUM_ID;
            ViewBag.FATURA_KESIM_TARIHI_MIN = FATURA_KESIM_TARIHI_MIN;
            ViewBag.FATURA_KESIM_TARIHI_MAX = FATURA_KESIM_TARIHI_MAX;
            return View(faturas);
        }
        [AuthFTR]
        public ActionResult FaturaDuzenlePartial(string FATURA_ID)
        {
            CreateLog("/FTR/FaturaDuzenlePartial");
            FaturaDTO fatura = faturaBusiness.GetFaturaByFaturaId(FATURA_ID);
            ViewBag.KURUM_LIST = kurumBusiness.GetKurumList();
            return View(fatura);
        }
        [AuthFTR]
        public ActionResult FaturaDuzenle(FaturaDTO fatura, string KURUM_ID)
        {
            CreateLog("/FTR/FaturaDuzenle");
            LOGON_FATURA_USER = (UserDTO)Session["LOGON_FATURA_USER"];
            fatura.USER = LOGON_FATURA_USER;
            fatura.UPD_USER = LOGON_FATURA_USER;
            fatura.KURUM = kurumBusiness.GetKurumByKurumId(KURUM_ID);

            if (Request.Files.Count > 0)
            {
                string filename = Path.GetFileName(Request.Files[0].FileName);
                string extention = Path.GetExtension(Request.Files[0].FileName);
                if (filename != "")
                {
                    string root = "~/Upload/FTR/" + fatura.KURUM.KURUM_ID + "_" + fatura.FATURA_KESIM_TARIHI.Replace("-", "") + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + extention;
                    Request.Files[0].SaveAs(Server.MapPath(root));
                    fatura.FATURA_URL = "/Upload/FTR/" + fatura.KURUM.KURUM_ID + "_" + fatura.FATURA_KESIM_TARIHI.Replace("-", "") + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + extention;
                }
            }

            int response = faturaBusiness.SetFatura(fatura);
            if (response < 0)
            {
                ViewBag.Message = "Sistemde bir hata gerçekleşti! Lütfen daha sonra tekrar deneyiniz!";
            }
            else
            {
                ViewBag.Message = "İşlem başarılı!";
            }
            return RedirectToAction("FaturaListesiPartial");
        }
        [AuthFTR]
        public ActionResult FaturaEklePartial()
        {
            CreateLog("/FTR/FaturaEklePartial");
            ViewBag.KURUM_LIST = kurumBusiness.GetKurumList();
            return View("FaturaDuzenlePartial", null);
        }
        [AuthFTR]
        public ActionResult FaturaSil(string FATURA_ID)
        {
            CreateLog("/FTR/FaturaSil");
            LOGON_FATURA_USER = (UserDTO)Session["LOGON_FATURA_USER"];
            int response = faturaBusiness.RemoveFatura(LOGON_FATURA_USER, FATURA_ID);
            if (response < 0)
            {
                ViewBag.Message = "Sistemde bir hata gerçekleşti! Lütfen daha sonra tekrar deneyiniz!";
            }
            else
            {
                ViewBag.Message = "İşlem başarılı!";
            }
            return RedirectToAction("FaturaListesiPartial");
        }
        #endregion
    }
}