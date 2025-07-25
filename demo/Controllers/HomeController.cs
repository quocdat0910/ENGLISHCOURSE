using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using demo.Models;

namespace demo.Controllers
{
    public class HomeController : Controller
    {
        TiengAnhEntities db = new TiengAnhEntities();
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        public ActionResult Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                ViewBag.ErrorMessage = "Mời bạn nhập tên sản phẩm";
                return View();
            }
            List<Product> dsProduct = db.Products.Where(p => p.NamePro.Contains(keyword)).ToList();
            ViewBag.Keyword = keyword;
            return View(dsProduct);
        }
    }
}