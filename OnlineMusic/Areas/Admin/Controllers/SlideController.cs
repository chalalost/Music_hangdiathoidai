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
    public class SlideController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        // GET: Admin/User/Sua/5
        public ActionResult Sua(int? id)
        {
            using (OnlineMusicDB dbModel = new OnlineMusicDB())
            {
                var kq = dbModel.SLIDEs.Where(x => x.ID == id).FirstOrDefault();
                return View(kq);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Sua(int? id , SLIDE slide)
        {
            try
            {
                using (OnlineMusicDB dbModel = new OnlineMusicDB())
                {
                    dbModel.Entry(slide).State = EntityState.Modified;
                    dbModel.SaveChanges();
                }
                return RedirectToAction("Danhsach");
            }
            catch
            {
                return View();
            }

        }
        public ActionResult Them(SLIDE slide)
        {
            if (ModelState.IsValid)
            {
                var dao = new SLIDE_DAO();
                var id = dao.Insert(slide);
                if (id == true)
                {
                    return RedirectToAction("Danhsach", "Slide");
                }
                else
                {
                    ModelState.AddModelError("", "Them slider ko thanh cong");
                }
            }
            return View("Index");
        }
        public ActionResult Danhsach(int page = 1, int pageSize = 10)
        {
            var dao = new SLIDE_DAO();
            var model = dao.ListAllPaging(page, pageSize);
            return View(model);
        }
        [HttpGet]
        public ActionResult Xoa(int? id)
        {
            using (OnlineMusicDB dbModel = new OnlineMusicDB())
            {
                return View(dbModel.SLIDEs.Where(x => x.ID == id).FirstOrDefault());
            }
        }


        [HttpPost]
        public ActionResult Xoa(int? id, SLIDE slide)
        {
            try
            {
                using (OnlineMusicDB dbModel = new OnlineMusicDB())
                {
                    slide = dbModel.SLIDEs.Where(x => x.ID == id).FirstOrDefault();
                    dbModel.Entry(slide).State = EntityState.Modified;
                    dbModel.SLIDEs.Remove(slide);
                    dbModel.SaveChanges();

                }
                return RedirectToAction("Danhsach");
            }
            catch
            {
                return View();
            }
        }
    }
}