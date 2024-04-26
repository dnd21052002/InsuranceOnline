using Common;
using Insurance.Data.Dao;
using InsuranceOnline.Common;
using InsuranceOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InsuranceOnline.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if(ModelState.IsValid)
            {
                //Khởi tạo đối tượng CustomerUserDao
                var dao = new CustomerUserDao();
                var result = dao.Login(model.UserName, Encryptor.MD5Hash(model.Password));
                if(result == 1)
                {
                    var user = dao.GetByUsername(model.UserName);
                    var userSession = new UserLogin
                    {
                        UserName = user.Username,
                        UserID = user.Id
                    };
                    Session.Add(CommonConstants.USER_SESSION, userSession);
                    return Redirect("/");
                }

                else if(result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tại");
                }
                else if(result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản đang bị khóa");
                }
                else if(result == -2)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng nhập không đúng");
                }
            }
            return View(model);
        }
    }
}