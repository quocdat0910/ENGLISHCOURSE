
using demo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace demo.Controllers
{
    public class DeMoController : Controller
    {
        TiengAnhEntities database = new TiengAnhEntities();
        // GET: DeMo

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult TOEIC(string cat="TI")
        {   
            return View(database.Products.Where(p=>p.Category==cat));   
        }

        public ActionResult IELTS(string cat="IE")
        {
            return View(database.Products.Where(p=>p.Category==cat));
        }
 
    }
}