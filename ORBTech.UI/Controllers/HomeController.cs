using ORBTech.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ORBTech.UI.Controllers
{
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            CreateLog("/HOME/Index");
            return View();
        }
        public ActionResult Urunler()
        {
            CreateLog("/HOME/Urunler");
            return View();
        }public ActionResult UrunlerPartial()
        {
            List<UrunDTO> uruns = urunBusiness.GetUrunList();
            return PartialView(uruns);
        }
        public ActionResult Iletisim()
        {
            CreateLog("/HOME/Iletisim");
            return View();
        }
        public ActionResult BlogPartial()
        {
            //CreateLog("/HOME/BlogPartial");
            List<BlogDTO> blogs = blogBusiness.GetBlogList();
            return PartialView(blogs);
        }
        public ActionResult BlogDetayPartial(string BLOG_ID)
        {
            CreateLog("/HOME/BlogDetayPartial");
            BlogDTO blog = blogBusiness.GetBlogByBlogId(BLOG_ID);
            return View(blog);
        }
       public ActionResult UrunDetayPartial(string URUN_ID)
        {
            CreateLog("/HOME/UrunDetayPartial");
            UrunDTO urun = urunBusiness.GetUrunByUrunId(URUN_ID);
            return View(urun);
        }
        public ActionResult SliderPartial()
        {
            //CreateLog("/HOME/SliderPartial");
            List<SliderDTO> sliders = sliderBusiness.GetSliderListByPasif(0);
            return PartialView(sliders);
        }
    }
}