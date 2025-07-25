using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using demo.Models;

namespace demo.Controllers
{
    public class CartController : Controller
    {   
        TiengAnhEntities database=new TiengAnhEntities();

        public List<CartItem> GetCart()
        {
            List<CartItem> myCart = Session["GioHang"] as
            List<CartItem>;
                if (myCart == null)
                {
                myCart = new List<CartItem>();
                Session["GioHang"] = myCart;
                }
            return myCart;
        }

        public ActionResult AddToCart(int id)
        {
            List<CartItem> myCart = GetCart();
            CartItem currentProduct = myCart.FirstOrDefault(p =>
            p.ProductID == id);
            if (currentProduct == null)
            {
                currentProduct = new CartItem(id);
                myCart.Add(currentProduct);
            }
            else
            {
                currentProduct.Number++;
            }
            return RedirectToAction("Index", "Home");
        }

        private int GetTotalNumber()
        {
            int totalNumber = 0;
            List<CartItem> myCart = GetCart();
            if (myCart != null)
                totalNumber = myCart.Sum(sp => sp.Number);
            return totalNumber;
        }

        public decimal GetTotalPrice()
        {
            decimal totalPrice = 0;
            List<CartItem> myCart = GetCart();
            if (myCart != null)
            {
                totalPrice = myCart.Sum(sp => sp.FinalPrice());
            }
            return totalPrice;
        }

        public ActionResult GetCartInfo()
        {
            List<CartItem> myCart = GetCart();
            if (myCart == null || myCart.Count == 0)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.TotalNumber = GetTotalNumber();
            ViewBag.TotalPrice = GetTotalPrice();
            return View(myCart);
        }

        public ActionResult CartPartial()
        {
            int totalNumber = GetTotalNumber();
            decimal totalPrice = GetTotalPrice();

            ViewBag.IsEmpty = (totalNumber == 0);

            if (!ViewBag.IsEmpty)
            {
                ViewBag.TotalNumber = totalNumber;
                ViewBag.TotalPrice = totalPrice;
            }

            return PartialView();
        }   
        public ActionResult Infor(int id)
        {
            var product = database.Products.FirstOrDefault(p => p.ProductID == id);
            if (product != null)
            {
                if (string.Equals(product.Category, "TI", StringComparison.OrdinalIgnoreCase))
                {
                    product.Category = "TOEIC";
                }
                if (string.Equals(product.Category, "IE", StringComparison.OrdinalIgnoreCase))
                {
                    product.Category = "IELTS";
                }
            }
            return View(product);
        }

        public ActionResult RemoveFromCart(int id)
        {
            List<CartItem> myCart = GetCart();
            CartItem productToRemove = myCart.FirstOrDefault(p => p.ProductID == id);

            if (productToRemove != null)
            {
                myCart.Remove(productToRemove);
            }

           
            Session["GioHang"] = myCart;

           
            return RedirectToAction("GetCartInfo");
        }

        public ActionResult EndCart()
        {

            return View();
        }

        public ActionResult ClearCart()
        {
           
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("EndCart");
            }

           
            Session["GioHang"] = null;

           
            return RedirectToAction("Index", "Home");
        }


        public ActionResult OtherCourse()
        {
            return PartialView(database.Products.ToList());
        }

    }
}