using OnlineMusic.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineMusic.Controllers
{
    public class Blog1Controller : Controller
    {
        // GET: Blog1
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Blog_Detail(int id)
        {
            var blog = new BLOG_DAO().ViewDetail(id);
            return View(blog);
        }
    }
}