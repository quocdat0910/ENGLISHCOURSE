using System;
using System.Linq;
using System.Web.Mvc;
using demo.Models;

namespace demo.Controllers
{
    public class AccountController : Controller
    {
        private TiengAnhEntities database;

        public AccountController()
        {
            database = new TiengAnhEntities();
        }

        // GET: Account
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterM model)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra xem có người nào đã đăng kí với tên đăng nhập này hay chưa                  
                var existingCustomer = database.Customers.FirstOrDefault(k => k.NameCus == model.NameCus);
                var existingAccount = database.Customers.FirstOrDefault(e => e.EmailCus == model.EmailCus);
                if (existingCustomer != null)
                {
                    ViewBag.CanhBao = "Tài khoản đã tồn tại!";
                    return View(model);
                }
                if (existingAccount != null)
                {
                    ViewBag.Alert = "Email đã tồn tại!";
                    return View(model);
                }

                // Kiểm tra xác nhận mật khẩu
                if (model.PassCus != model.ConfirmPassCus)
                {
                    ModelState.AddModelError(string.Empty, "Mật khẩu và xác nhận mật khẩu không khớp");
                    return View(model);
                }

                // Nếu mọi điều kiện hợp lệ, thêm mới khách hàng
                database.Customers.Add(new Customer
                {
                    NameCus = model.NameCus,
                    EmailCus = model.EmailCus,
                    PassCus = model.PassCus
                    // Thêm các trường khác nếu cần
                });

                database.SaveChanges();
                return RedirectToAction("Login");
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginM model)
        {
            if (ModelState.IsValid)
            {
                var existingCustomer = database.Customers.FirstOrDefault(k => k.NameCus == model.NameCus && k.PassCus == model.PassCus);

                if (existingCustomer != null)
                {
                    ViewBag.ThongBao = "Đăng nhập thành công";
                    Session["TaiKhoan"] = existingCustomer;
                    Session["TenNguoiDung"] = existingCustomer.NameCus;
                    Session["idUser"] = existingCustomer.IDCus;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
                }
            }

            return View();
        }

        public ActionResult Logout()
        {
            Session["TaiKhoan"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}
