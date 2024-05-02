using Common;
using Insurance.Data.Dao;
using Insurance.Data.Models;
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

        public ActionResult Login(string returnUrl)
        {
            if (returnUrl == null)
            {
                return View();
            }
            else
            {
                ViewBag.Message = "Vui lòng đăng nhập để tiếp tục";
            }

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
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
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return Redirect("/");
                    }
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

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            if(ModelState.IsValid)
            {
                var dao = new CustomerUserDao();
                if(dao.CheckUserName(model.UserName))
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại");
                }
                else if(dao.CheckEmail(model.Email))
                {
                    ModelState.AddModelError("", "Email đã tồn tại");
                }
                else
                {
                    var user = new CustomerUser();
                    user.Username = model.UserName;
                    user.Password = Encryptor.MD5Hash(model.Password);
                    user.Email = model.Email;
                    user.FullName = model.FullName;
                    user.Status = 1;

                    var result = dao.Insert(user);
                    if(result > 0)
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

        public ActionResult Logout()
        {
            Session[CommonConstants.USER_SESSION] = null;
            return Redirect("/");
        }

        public ActionResult Infor()
        {
            var returnUrl1 = Request.Url.PathAndQuery;
            if (returnUrl1.Contains("/dang-nhap"))
            {
                ViewBag.ReturnUrl = "";
            }
            else
            {
                ViewBag.ReturnUrl = returnUrl1;
            }

            if (Session[CommonConstants.USER_SESSION] == null)
            {
                return RedirectToAction("Login", "User", new { returnUrl = returnUrl1 });
            }

            var user = (UserLogin)Session[CommonConstants.USER_SESSION];
            var productCusDao = new ProductCustomerDao();

            var listProduct = productCusDao.ListByCustomer(user.UserID);

            foreach (var item in listProduct)
            {
                var product = new ProductDao().ViewDetail(item.ProductID);
                if (product.ExpireType == 0)
                {
                    item.ExpireTime = item.CreatedDate.AddMonths(product.ExpireTime);
                }
                else
                {
                    item.ExpireTime = item.CreatedDate.AddYears(product.ExpireTime);
                }
            }

            return View(listProduct);

        }
    }
}