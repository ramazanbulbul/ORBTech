using ORBTech.Business;
using ORBTech.DTO;
using ORBTech.UI.Attributes;
using ORBTech.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ORBTech.UI.Controllers
{
    public class AdminController : BaseController
    {
        [Auth]
        public ActionResult Index()
        {
            CreateLog("/ADMIN/Index");
            List<LogDTO> logs = new List<LogDTO>();
            logs = logBusiness.GetLogsList();
            ViewBag.test = logs[5].GIRIS_ZAMANI;
            return View(logs);
        }
        public ActionResult LoginPartial()
        {
            CreateLog("/ADMIN/LoginPartial");
            return View();
        }
        public ActionResult Login(string USERNAME, string PASSWORD)
        {
            CreateLog("/ADMIN/Login");
            UserDTO user = userBusiness.Login(USERNAME, PASSWORD);
            if (user != null)
            {
                Session["LOGON_USER"] = user;
                LOGON_USER = (UserDTO)Session["LOGON_USER"];
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
            CreateLog("/ADMIN/Logout");
            Session["LOGIN_USER"] = null;
            return View("LoginPartial");
        }
        #region SLIDER
        [Auth]
        public ActionResult SliderListesiPartial()
        {
            CreateLog("/ADMIN/SliderListesiPartial");
            List<SliderDTO> sliders = sliderBusiness.GetSliderList();
            return View(sliders);
        }
        [Auth]
        public ActionResult SliderDuzenlePartial(string SLIDER_ID)
        {
            CreateLog("/ADMIN/SliderDuzenlePartial");
            SliderDTO slider = sliderBusiness.GetSliderBySliderId(SLIDER_ID);
            return View(slider);
        }
        [Auth]
        public ActionResult SliderDuzenle(SliderDTO slider)
        {
            CreateLog("/ADMIN/SliderDuzenle");
            LOGON_USER = (UserDTO)Session["LOGON_USER"];
            slider.USER = LOGON_USER; ;
            slider.UPD_USER = LOGON_USER;
            int response = sliderBusiness.SetSlider(slider);
            if (response < 0)
            {
                ViewBag.Message = "Sistemde bir hata gerçekleşti! Lütfen daha sonra tekrar deneyiniz!";
            }
            else
            {
                ViewBag.Message = "İşlem başarılı!";
            }
            return RedirectToAction("SliderListesiPartial");
        }
        [Auth]
        public ActionResult SliderEklePartial()
        {
            CreateLog("/ADMIN/SliderEklePartial");
            return View("SliderDuzenlePartial", null);
        }
        [Auth]
        public ActionResult SliderSil(string SLIDER_ID)
        {
            CreateLog("/ADMIN/SliderSil");
            LOGON_USER = (UserDTO)Session["LOGON_USER"];
            int response = sliderBusiness.RemoveSlider(LOGON_USER, SLIDER_ID);
            if (response < 0)
            {
                ViewBag.Message = "Sistemde bir hata gerçekleşti! Lütfen daha sonra tekrar deneyiniz!";
            }
            else
            {
                ViewBag.Message = "İşlem başarılı!";
            }
            return RedirectToAction("SliderListesiPartial");
        }
        #endregion
        #region BLOG
        [Auth]
        public ActionResult BlogListesiPartial()
        {
            CreateLog("/ADMIN/BlogListesiPartial");
            List<BlogDTO> blogs = blogBusiness.GetBlogList();
            return View(blogs);
        }
        [Auth]
        public ActionResult BlogDuzenlePartial(string BLOG_ID)
        {
            CreateLog("/ADMIN/BlogDuzenlePartial");
            BlogDTO blog = blogBusiness.GetBlogByBlogId(BLOG_ID);
            return View(blog);
        }
        [Auth]
        public ActionResult BlogDuzenle(BlogDTO blog)
        {
            CreateLog("/ADMIN/BlogDuzenle");
            LOGON_USER = (UserDTO)Session["LOGON_USER"];
            blog.USER = LOGON_USER;;
            blog.UPD_USER = LOGON_USER;
            int response = blogBusiness.SetBlog(blog);
            if (response < 0)
            {
                ViewBag.Message = "Sistemde bir hata gerçekleşti! Lütfen daha sonra tekrar deneyiniz!";
            }
            else
            {
                ViewBag.Message = "İşlem başarılı!";
            }
            return RedirectToAction("BlogListesiPartial");
        }
        [Auth]
        public ActionResult BlogEklePartial()
        {
            CreateLog("/ADMIN/BlogEklePartial");
            return View("BlogDuzenlePartial", null);
        }
        [Auth]
        public ActionResult BlogSil(string BLOG_ID)
        {
            CreateLog("/ADMIN/BlogSil");
            LOGON_USER = (UserDTO)Session["LOGON_USER"];
            int response = blogBusiness.RemoveBlog(LOGON_USER, BLOG_ID);
            if (response < 0)
            {
                ViewBag.Message = "Sistemde bir hata gerçekleşti! Lütfen daha sonra tekrar deneyiniz!";
            }
            else
            {
                ViewBag.Message = "İşlem başarılı!";
            }
            return RedirectToAction("BlogListesiPartial");
        }
        #endregion
        #region BLOG
        [Auth]
        public ActionResult UrunListesiPartial()
        {
            CreateLog("/ADMIN/UrunListesiPartial");
            List<UrunDTO> uruns = urunBusiness.GetUrunList();
            return View(uruns);
        }
        [Auth]
        public ActionResult UrunDuzenlePartial(string URUN_ID)
        {
            CreateLog("/ADMIN/UrunDuzenlePartial");
            UrunDTO urun = urunBusiness.GetUrunByUrunId(URUN_ID);
            return View(urun);
        }
        [Auth]
        public ActionResult UrunDuzenle(UrunDTO urun)
        {
            CreateLog("/ADMIN/UrunDuzenle");
            LOGON_USER = (UserDTO)Session["LOGON_USER"];
            urun.USER = LOGON_USER; ;
            urun.UPD_USER = LOGON_USER;
            int response = urunBusiness.SetUrun(urun);
            if (response < 0)
            {
                ViewBag.Message = "Sistemde bir hata gerçekleşti! Lütfen daha sonra tekrar deneyiniz!";
            }
            else
            {
                ViewBag.Message = "İşlem başarılı!";
            }
            return RedirectToAction("UrunListesiPartial");
        }
        [Auth]
        public ActionResult UrunEklePartial()
        {
            CreateLog("/ADMIN/UrunEklePartial");
            return View("UrunDuzenlePartial", null);
        }
        [Auth]
        public ActionResult UrunSil(string URUN_ID)
        {
            CreateLog("/ADMIN/UrunSil");
            LOGON_USER = (UserDTO)Session["LOGON_USER"];
            int response = urunBusiness.RemoveUrun(LOGON_USER, URUN_ID);
            if (response < 0)
            {
                ViewBag.Message = "Sistemde bir hata gerçekleşti! Lütfen daha sonra tekrar deneyiniz!";
            }
            else
            {
                ViewBag.Message = "İşlem başarılı!";
            }
            return RedirectToAction("UrunListesiPartial");
        }
        #endregion
    }
}