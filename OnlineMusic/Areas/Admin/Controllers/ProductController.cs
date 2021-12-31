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
    public class ProductController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        // GET: Admin/User/Sua/5
        public ActionResult Sua(string meta)
        {
            using (OnlineMusicDB dbModel = new OnlineMusicDB())
            {
                var kq = dbModel.SANPHAMs.Where(x => x.MetaTitle == meta).FirstOrDefault();
                return View(kq);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Sua(string meta, SANPHAM dm)
        {
            try
            {
                using (OnlineMusicDB dbModel = new OnlineMusicDB())
                {
                    dbModel.Entry(dm).State = EntityState.Modified;
                    dbModel.SaveChanges();
                }
                return RedirectToAction("Danhsach");
            }
            catch
            {
                return View();
            }

        }
        public ActionResult Them(SANPHAM dm)
        {
            if (ModelState.IsValid)
            {
                var dao = new PRODUCT_DAO();
                var id = dao.Insert(dm);
                if (id == true)
                {
                    return RedirectToAction("Danhsach", "Product");
                }
                else
                {
                    ModelState.AddModelError("", "Them san pham ko thanh cong");
                }
            }
            return View("Index");
        }
        public ActionResult Danhsach(string searching,int page = 1, int pageSize = 40)
        {
            var dao = new PRODUCT_DAO();
            var model = dao.ListAllPaging(searching, page, pageSize);
            return View(model);
        }
        [HttpGet]
        public ActionResult Xoa(string meta)
        {
            using (OnlineMusicDB dbModel = new OnlineMusicDB())
            {
                return View(dbModel.SANPHAMs.Where(x => x.MetaTitle == meta).FirstOrDefault());
            }
        }


        [HttpPost]
        public ActionResult Xoa(string meta, SANPHAM dm)
        {
            try
            {
                using (OnlineMusicDB dbModel = new OnlineMusicDB())
                {
                    dm = dbModel.SANPHAMs.Where(x => x.MetaTitle == meta).FirstOrDefault();
                    dbModel.Entry(dm).State = EntityState.Modified;
                    dbModel.SANPHAMs.Remove(dm);
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