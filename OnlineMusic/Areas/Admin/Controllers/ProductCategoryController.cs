using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineMusic.DAO;
using OnlineMusic.EF;

namespace OnlineMusic.Areas.Admin.Controllers
{
    public class ProductCategoryController : Controller
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
                var kq = dbModel.DANHMUCSANPHAMs.Where(x => x.MetaTitle == meta).FirstOrDefault();
                return View(kq);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Sua(string meta, DANHMUCSANPHAM dm)
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
        public ActionResult Them(DANHMUCSANPHAM dm)
        {
            if (ModelState.IsValid)
            {
                var dao = new PRODUCTCATEGORY_DAO();
                var id = dao.Insert(dm);
                if (id == true)
                {
                    return RedirectToAction("Danhsach", "ProductCategory");
                }
                else
                {
                    ModelState.AddModelError("", "Them danh muc san pham ko thanh cong");
                }
            }
            return View("Index");
        }
        public ActionResult Danhsach(int page = 1, int pageSize = 10)
        {
            var dao = new PRODUCTCATEGORY_DAO();
            var model = dao.ListAllPaging(page, pageSize);
            return View(model);
        }
        [HttpGet]
        public ActionResult Xoa(string meta)
        {
            using (OnlineMusicDB dbModel = new OnlineMusicDB())
            {
                return View(dbModel.DANHMUCSANPHAMs.Where(x => x.MetaTitle == meta).FirstOrDefault());
            }
        }


        [HttpPost]
        public ActionResult Xoa(string meta, DANHMUCSANPHAM dm)
        {
            try
            {
                using (OnlineMusicDB dbModel = new OnlineMusicDB())
                {
                    dm = dbModel.DANHMUCSANPHAMs.Where(x => x.MetaTitle == meta).FirstOrDefault();
                    dbModel.Entry(dm).State = EntityState.Modified;
                    dbModel.DANHMUCSANPHAMs.Remove(dm);
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