using OnlineMusic.Common;
using OnlineMusic.DAO;
using OnlineMusic.EF;
using OnlineMusic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineMusic.Controllers
{
    public class MainController : Controller
    {

        // GET: Home
        public ActionResult Index()
        {
            var productdao = new PRODUCT_DAO();
            ViewBag.SANPHAM = productdao.ListSanPham(1);
            return View();
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Blog()
        {
            var blogdao = new BLOG_DAO();
            ViewBag.NEWS = blogdao.ListBlog(1);
            return View();
        }
        
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Product()
        {
            return View();
        }
        public ActionResult Product_Detail()
        {
            return View();
        }
        public ActionResult Shopping_Cart()
        {
            return View();
        }
        [ChildActionOnly]
        public PartialViewResult CaSi()
        {
            var model = new CASI_DAO().ListAll();
            return PartialView(model);
        }

        [ChildActionOnly]
        public PartialViewResult Slider()
        {
            var model = new SLIDE_DAO().ListAll();
            return PartialView(model);
        }
        [ChildActionOnly]
        public PartialViewResult HeaderCart()
        {
            var cart = Session[CommonConstants.CartSession];
            var list = new List<CartItems>();
            if (cart != null)
            {
                list = (List<CartItems>)cart;
            }
            return PartialView(list);
        }
        [HttpPost]
        public ActionResult Send(string email, string content)
        {
            var feedback = new FEEDBACK();
            feedback.Email = email;
            feedback.CreateDate = DateTime.Now.ToString();
            feedback.FeedBackContent = content;
            var id = new FEEDBACK_DAO().Insert(feedback);
            if (id >0)
            {
                return Redirect("/");
                //send mail
            }

            else
                return Json(new
                {
                    status = false
                });

        }
    }
}