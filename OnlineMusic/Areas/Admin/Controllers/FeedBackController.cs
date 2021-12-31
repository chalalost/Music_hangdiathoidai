using OnlineMusic.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineMusic.Areas.Admin.Controllers
{
    public class FeedBackController : Controller
    {
        // GET: Admin/FeedBack
        public ActionResult Chitiet()
        {
            return View();
        }
        public ActionResult Danhsach( int page = 1, int pageSize = 60)
        {
            var dao = new FEEDBACK_DAO();
            var model = dao.ListAllPaging( page, pageSize);
            return View(model);
        }
    }
}