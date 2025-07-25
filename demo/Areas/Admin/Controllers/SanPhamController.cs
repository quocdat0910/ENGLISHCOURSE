using demo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace demo.Areas.Admin.Controllers
{
    public class SanPhamController : Controller
    {
        private TiengAnhEntities db = new TiengAnhEntities();
        // GET: Admin/SanPham
        public ActionResult Index()
        {
            var items = db.Products;
            return View(items);
        }

      
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product model)
        {
            if(ModelState.IsValid)
            {   
                db.Products.Add(model);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            Product pro = db.Products.FirstOrDefault(p => p.ProductID == id);
            if (pro != null)
                return View(pro);
            else
                return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include ="ProductID,NamePro,ImagePro,DescriptionPro,Price,Category,Teacher,Time,Amount")] Product product)
        {
            if (ModelState.IsValid)
            {
                var productDB = db.Products.FirstOrDefault(p => p.ProductID == product.ProductID);
                if (productDB != null)
                {
                    productDB.NamePro = product.NamePro;
                    productDB.ImagePro = product.ImagePro;
                    productDB.DescriptionPro = product.DescriptionPro;
                    productDB.Price = product.Price;
                    productDB.Category = product.Category;
                    productDB.Teacher = product.Teacher;
                    productDB.Time = product.Time;
                    productDB.Amount = product.Amount;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        [HttpGet]
        public ActionResult Delete (int id)
        {
            Product pro=db.Products.FirstOrDefault(p => p.ProductID == id);
            if (pro != null)
                return View(pro);
            else return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete (string id)
        {
            int i = int.Parse(id);
            var productDB = db.Products.FirstOrDefault(p => p.ProductID == i);
            if(productDB!=null)
            {
                db.Products.Remove(productDB);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}