using Common;
using Insurance.Data.Dao;
using InsuranceOnline.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InsuranceOnline.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        //Trang đăng nhập
        public ActionResult Index()
        {
            return View();
        }

        //Xử lý đăng nhập
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                //Khởi tạo đối tượng AdminUserDao
                var dao = new AdminUserDao();
                var result = dao.Login(model.UserName, Encryptor.MD5Hash(model.Password));
                if (result == 1)
                {
                    var user = dao.GetByID(model.UserName);
                    var adminSession = new UserLogin
                    {
                        UserName = user.Username,
                        UserID = user.Id
                    };
                    Session.Add(CommonConstants.ADMIN_SESSION, adminSession);
                    return RedirectToAction("Index", "Home");
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
                    ModelState.AddModelError("", "Mật khẩu không đúng");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng nhập không đúng");
                }
            }
            return View("Index");
        }

        public ActionResult Logout()
        {
            Session[CommonConstants.ADMIN_SESSION] = null;
            return Redirect("/Admin/Login");
        }
    }
}