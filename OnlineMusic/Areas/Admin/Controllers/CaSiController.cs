using OnlineMusic.DAO;
using OnlineMusic.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OnlineMusic.Areas.Admin.Controllers
{
    public class CaSiController : Controller
    {
        // GET: Admin/CaSi
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Danhsach(int page = 1, int pageSize = 4)
        {
            var dao = new CASI_DAO();
            var model = dao.ListAllPaging(page, pageSize);
            return View(model);
        }
        [HttpGet]
        public ActionResult Xoa(string meta)
        {
            using (OnlineMusicDB dbModel = new OnlineMusicDB())
            {
                return View(dbModel.CASIs.Where(x => x.MetaTitle == meta).FirstOrDefault());
            }
        }


        [HttpPost]
        public ActionResult Xoa(string meta, CASI casi)
        {
            try
            {
                using (OnlineMusicDB dbModel = new OnlineMusicDB())
                {
                    casi = dbModel.CASIs.Where(x => x.MetaTitle == meta).FirstOrDefault();
                    dbModel.Entry(casi).State = EntityState.Modified;
                    dbModel.CASIs.Remove(casi);
                    dbModel.SaveChanges();

                }
                return RedirectToAction("Danhsach");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Them(CASI casi)
        {
            if (ModelState.IsValid)
            {
                var dao = new CASI_DAO();
                var id = dao.Insert(casi);
                if (id == true)
                {
                    return RedirectToAction("Danhsach", "CaSi");
                }
                else
                {
                    ModelState.AddModelError("", "Them Ca si ko thanh cong");
                }
            }
            return View("Index");
        }
        public ActionResult Sua(string meta)
        {
            using (OnlineMusicDB dbModel = new OnlineMusicDB())
            {
                var kq = dbModel.CASIs.Where(x => x.MetaTitle == meta).FirstOrDefault();
                return View(kq);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Sua(string meta, CASI casi)
        {
            try
            {
                using (OnlineMusicDB dbModel = new OnlineMusicDB())
                {
                    dbModel.Entry(casi).State = EntityState.Modified;
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