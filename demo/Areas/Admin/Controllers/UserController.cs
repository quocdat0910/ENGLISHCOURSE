using demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace demo.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        private TiengAnhEntities db = new TiengAnhEntities();
        public ActionResult Index()
        {
            var user = db.Customers.ToList();
            return View(user);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Customer customer = db.Customers.FirstOrDefault(p => p.IDCus == id);
            if (customer != null)   
                return View(customer);
            else
                return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDCus,NameCus,EmailCus,PassCus")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                var customerDB = db.Customers.FirstOrDefault(p => p.IDCus == customer.IDCus);
                if (customerDB != null)
                {
                    customerDB.IDCus = customer.IDCus;
                    customerDB.NameCus = customer.NameCus;
                    customerDB.EmailCus = customer.EmailCus;
                    customerDB.PassCus = customer.PassCus;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Customer customer = db.Customers.FirstOrDefault(p => p.IDCus == id);
            if (customer != null)
                return View(customer);
            else return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id)
        {
            int i = int.Parse(id);
            var customerDB = db.Customers.FirstOrDefault(p => p.IDCus == i);
            if (customerDB != null)
            {
                db.Customers.Remove(customerDB);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}