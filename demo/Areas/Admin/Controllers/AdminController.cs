using demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace demo.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        TiengAnhEntities database = new TiengAnhEntities();
        // GET: Admin
        public ActionResult Index()
        {
            var userAD = database.AdminUsers.ToList();
            return View(userAD);
        }
        [HttpGet]
        public ActionResult LoginAcount()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginAcount(AdminUser model)
        {
            if (ModelState.IsValid)
            {
                var existingCustomer = database.AdminUsers.FirstOrDefault(k => k.NameUser == model.NameUser && k.PasswordUser == model.PasswordUser);

                if (existingCustomer != null)
                {
                    Session["NameUser"] = existingCustomer;
                    Session["PasswordUser"] = existingCustomer.NameUser;
                    Session["IsLoggedIn"] = existingCustomer.ID;
                    return RedirectToAction("Index", "SanPham");
                }
                else
                {
                    ViewBag.ThongBao = "Sai tài khoản Admin";
                }
            }

            return View();
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AdminUser customer)
        {
            if (ModelState.IsValid)
            {    
                database.AdminUsers.Add(customer);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customer);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            AdminUser customer = database.AdminUsers.FirstOrDefault(p => p.ID == id);
            if (customer != null)
                return View(customer);
            else
                return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NameUser,PasswordUser")] AdminUser admin)
        {
            if (ModelState.IsValid)
            {
                var customerDB = database.AdminUsers.FirstOrDefault(p => p.ID == admin.ID);
                if (customerDB != null)
                {
                    customerDB.ID = admin.ID;
                    customerDB.NameUser = admin.NameUser;
                    customerDB.PasswordUser = admin.PasswordUser;
                }
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(admin);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            AdminUser customer = database.AdminUsers.FirstOrDefault(p => p.ID == id);
            if (customer != null)
                return View(customer);
            else return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id)
        {
            int i = int.Parse(id);
            var customerDB = database.AdminUsers.FirstOrDefault(p => p.ID == i);
            if (customerDB != null)
            {
                database.AdminUsers.Remove(customerDB);
                database.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}