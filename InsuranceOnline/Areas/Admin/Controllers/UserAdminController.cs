using Common;
using Insurance.Data.Dao;
using Insurance.Data.Models;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace InsuranceOnline.Areas.Admin.Controllers
{
    public class UserAdminController : BaseController
    {
        // GET: Admin/UserAdmin
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new AdminUserDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);

            ViewBag.SearchString = searchString;
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(AdminUser adminUser)
        {
            if (ModelState.IsValid)
            {
                var dao = new AdminUserDao();
                var encryptedMd5Pas = Encryptor.MD5Hash(adminUser.Password);
                adminUser.Password = encryptedMd5Pas;
                long id = dao.Insert(adminUser);
                if (id > 0)
                {
                    SetAlert("Thêm tài khoản thành công", "success");
                    return RedirectToAction("Index", "UserAdmin");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm tài khoản không thành công");
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var adminUser = new AdminUserDao().ViewDetail(id);
            return View(adminUser);
        }

        [HttpPost]
        public ActionResult Edit(AdminUser adminUser)
        {
            if (ModelState.IsValid)
            {
                var dao = new AdminUserDao();
                if (!string.IsNullOrEmpty(adminUser.Password))
                {
                    var encryptedMd5Pas = Encryptor.MD5Hash(adminUser.Password);
                    adminUser.Password = encryptedMd5Pas;
                }
                var result = dao.Update(adminUser);
                if (result)
                {
                    SetAlert("Cập nhật tài khoản thành công", "success");
                    return RedirectToAction("Index", "UserAdmin");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật tài khoản thành công");
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var dao = new AdminUserDao();
            var result = dao.Delete(id);
            if (result)
            {
                SetAlert("Xóa tài khoản thành công", "success");
                return RedirectToAction("Index");
            }
            else
            {
                SetAlert("Xóa tài khoản không thành công", "error");
                return RedirectToAction("Index");
            }
        }
    }
}