using Common;
using Insurance.Data.Dao;
using Insurance.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InsuranceOnline.Areas.Admin.Controllers
{
    public class CustomerUserController : BaseController
    {
        // GET: Admin/CustomerUser
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new CustomerUserDao();
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
        public ActionResult Create(CustomerUser customerUser)
        {
            if (ModelState.IsValid)
            {
                var dao = new CustomerUserDao();
                var encryptedMd5Pas = Encryptor.MD5Hash(customerUser.Password);
                customerUser.Password = encryptedMd5Pas;
                long id = dao.Insert(customerUser);
                if (id > 0)
                {
                    SetAlert("Thêm tài khoản thành công", "success");
                    return RedirectToAction("Index", "CustomerUser");
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
            var customerUser = new CustomerUserDao().ViewDetail(id);
            return View(customerUser);
        }

        [HttpPost]
        public ActionResult Edit(CustomerUser customerUser)
        {
            if (ModelState.IsValid)
            {
                var dao = new CustomerUserDao();
                if (!string.IsNullOrEmpty(customerUser.Password))
                {
                    var encryptedMd5Pas = Encryptor.MD5Hash(customerUser.Password);
                    customerUser.Password = encryptedMd5Pas;
                }
                var result = dao.Update(customerUser);
                if (result)
                {
                    SetAlert("Cập nhật tài khoản thành công", "success");
                    return RedirectToAction("Index", "CustomerUser");
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
            var dao = new CustomerUserDao();
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