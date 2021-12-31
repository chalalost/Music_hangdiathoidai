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
    public class CustomerController : Controller
    {
        // GET: User
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new CUSTOMER_DAO();
                if (dao.CheckUserName(model.UserName))
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");
                }
                else if(dao.CheckEmail(model.Email))
                {
                    ModelState.AddModelError("", "Email đã tồn tại");
                }
                else
                {
                    var cus = new CUSTOMER();
                    cus.UserName = model.UserName;
                    cus.Email = model.Email;
                    cus.Name = model.Name;
                    cus.Password = model.Password;
                    cus.Phone = model.Phone;
                    cus.Address = model.Address;
                    cus.Status = true;
                    var result = dao.Insert(cus);
                    if(result == true)
                    {
                        ViewBag.Success = "Đăng ký thành công";
                        model = new RegisterModel();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Đăng ký không thành công");
                    }
                }
            }
            return View(model);
        }
        public ActionResult LogIn()
        {
            return View();
        }
        public ActionResult LogOut()
        {
            Session[CommonConstants.CUSTOMER_SESSION] = null;

            return Redirect("/");
        }
        [HttpPost]
        public ActionResult LogIn(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new CUSTOMER_DAO();
                var result = dao.Login(model.UserName, model.Password);
                if (result == 1)
                {
                    var user = dao.GetByID(model.UserName);
                    var userSession = new CustomerLogin();
                    userSession.UserName = user.UserName;
                    userSession.Name = user.Name;

                    Session.Add(CommonConstants.CUSTOMER_SESSION, userSession);
                    return Redirect("/");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản đang bị khóa");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mật khẩu không chính xác");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng nhập không thành công");
                }
            }
            
            return View(model);
        }
    }
}