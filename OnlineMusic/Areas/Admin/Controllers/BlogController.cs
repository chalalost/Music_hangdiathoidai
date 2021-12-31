using OnlineMusic.DAO;
using OnlineMusic.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineMusic.Areas.Admin.Controllers
{
    public class BlogController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Danhsach(string searching,int page = 1, int pageSize = 4)
        {
            var dao = new BLOG_DAO();
            var model = dao.ListAllPaging(searching,page, pageSize);
            return View(model);
        }
        [HttpGet]
        public ActionResult Xoa(string meta)
        {
            using (OnlineMusicDB dbModel = new OnlineMusicDB())
            {
                return View(dbModel.NEWS.Where(x => x.MetaTitle == meta).FirstOrDefault());
            }
        }


        [HttpPost]
        public ActionResult Xoa(string meta, NEWS ne)
        {
            try
            {
                using (OnlineMusicDB dbModel = new OnlineMusicDB())
                {
                    ne = dbModel.NEWS.Where(x => x.MetaTitle == meta).FirstOrDefault();
                    dbModel.Entry(ne).State = EntityState.Modified;
                    dbModel.NEWS.Remove(ne);
                    dbModel.SaveChanges();

                }
                return RedirectToAction("Danhsach");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Them(NEWS ne)
        {
            if (ModelState.IsValid)
            {
                var dao = new BLOG_DAO();
                var id = dao.Insert(ne);
                if (id == true)
                {
                    return RedirectToAction("Danhsach", "Blog");
                }
                else
                {
                    ModelState.AddModelError("", "Them Blog ko thanh cong");
                }
            }
            return View("Index");
        }
        public ActionResult Sua(string meta)
        {
            using (OnlineMusicDB dbModel = new OnlineMusicDB())
            {
                var kq = dbModel.NEWS.Where(x => x.MetaTitle == meta).FirstOrDefault();
                return View(kq);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Sua(string meta, NEWS ne)
        {
            try
            {
                using (OnlineMusicDB dbModel = new OnlineMusicDB())
                {
                    dbModel.Entry(ne).State = EntityState.Modified;
                    dbModel.SaveChanges();
                }
                return RedirectToAction("Danhsach");
            }
            catch
            {
                return View();
            }

        }
        public ActionResult Chitiet()
        {
            return View();
        }
    }
}