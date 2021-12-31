using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using OnlineMusic.DAO;
using OnlineMusic.EF;

namespace OnlineMusic.Controllers
{
    public class Product1Controller : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        [ChildActionOnly]
        public PartialViewResult ProductCategory()
        {
            var model = new PRODUCTCATEGORY_DAO().ListAll();
            return PartialView(model);
        }
        public ActionResult DanhMuc(long id)
        {
            return View();
        }
        public ActionResult Product()
        {
            var productdao = new PRODUCT_DAO();
            ViewBag.SANPHAM = productdao.ListSanPham(1);
            return View();
        }

        public ActionResult Product_Detail(long id)
        {
            var product = new PRODUCT_DAO().ViewDetail(id);
            ViewBag.ProductCategory = new PRODUCTCATEGORY_DAO().ViewDetail(product.CategoryID.Value);
            ViewBag.RelatedProduct = new PRODUCT_DAO().ListRelatedSanPham(id);
            return View(product);
        }
        public JsonResult ListName(string q)
        {
            var data = new PRODUCT_DAO().ListName(q);
            return Json(new
            {
                data = data,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Search(string ketqua)
        {
            using (OnlineMusicDB dbModel = new OnlineMusicDB())
            {

                List<SANPHAM> sp = new List<SANPHAM>();
                sp = dbModel.SANPHAMs.Where(x => x.Name.Contains(ketqua) || ketqua == null).ToList();
                return Json(ketqua, JsonRequestBehavior.AllowGet);
            }
        }

    }
}