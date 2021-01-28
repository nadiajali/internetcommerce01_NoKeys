using internetcommerce01.DAL;
using internetcommerce01.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace internetcommerce01.Controllers
{
    public class HomeController : Controller
    {
        db_internetcommerce01Entities context = new db_internetcommerce01Entities();

        public ActionResult Index(string search, int? page)
        {
            HomeIndexViewModel model = new HomeIndexViewModel();
            return View(model.CreateModel(search, 4, page));
        }

        public ActionResult CheckoutDetails()
        {
            return View();
        }

        public ActionResult AddToCart(int productID, string url)
        {
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                var product = context.Tbl_Product.Find(productID);
                cart.Add(new Item()
                {
                    Product = product,
                    Quantity = 1
                });
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["cart"];

                bool productIDNotFound = true;
                foreach (var item in cart)
                {
                    if (item.Product.ProductID == productID)
                    {
                        item.Quantity = item.Quantity + 1;
                        productIDNotFound = false;
                        break;
                    }
                }

                if (productIDNotFound)
                {
                    var product = context.Tbl_Product.Find(productID);
                    cart.Add(new Item()
                    {
                        Product = product,
                        Quantity = 1
                    });
                }

                Session["cart"] = cart;
            }
            return Redirect(url);
        }

        public ActionResult DecreaseQuantity(int productID, string url)
        {
            List<Item> cart = (List<Item>)Session["cart"];

            foreach (var item in cart)
            {
                if (item.Product.ProductID == productID)
                {
                    item.Quantity = item.Quantity - 1;
                    if (item.Quantity < 1)
                    {
                        cart.Remove(item);
                    }
                    break;
                }
            }
            Session["cart"] = cart;
            return Redirect(url);
        }

        public ActionResult RemoveFromCart(int productID)
        {
            ViewBag.PreviousUrl = System.Web.HttpContext.Current.Request.UrlReferrer.ToString();

            List<Item> cart = (List<Item>)Session["cart"];
            foreach (var item in cart)
            {
                if (item.Product.ProductID == productID)
                {
                    cart.Remove(item);
                    break;
                }
            }
            Session["cart"] = cart;
            return Redirect(ViewBag.PreviousUrl);
        }

        //public ActionResult Contact()
        //{
        //    ViewBag.Message = "Your contact page.";

        //    return View();
        //}
    }
}