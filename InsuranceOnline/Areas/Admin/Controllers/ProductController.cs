using Insurance.Data.Dao;
using Insurance.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InsuranceOnline.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Admin/Product
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new ProductDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);

            ViewBag.SearchString = searchString;
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductDao();
                long id = dao.Insert(product);

                if (id > 0)
                {
                    SetAlert("Thêm sản phẩm thành công", "success");
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm sản phẩm không thành công");
                }
            }
            SetViewBag();
            return View();
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var product = new ProductDao().ViewDetail(id);
            var categories = new ProductCategoryDao().ListAll();
            SetViewBag(product.CategoryID);
            SetViewBag(product.ExpireType);
            return View(product);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductDao();
                var result = dao.Update(product);
                if (result)
                {
                    SetAlert("Cập nhật phẩm thành công", "success");
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật sản phẩm không thành công");
                }
            }
            SetViewBag(product.CategoryID);
            SetViewBag(product.ExpireType);
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var dao = new ProductDao();
            var result = dao.Delete(id);
            if (result)
            {
                SetAlert("Xóa sản phẩm thành công", "success");
                return RedirectToAction("Index");
            }
            else
            {
                SetAlert("Xóa sản phẩm không thành công", "error");
                return RedirectToAction("Index");
            }
        }

        public void SetViewBag(long? selectedId = null)
        {
            var dao = new ProductCategoryDao();
            ViewBag.CategoryID = new SelectList(dao.ListAll(), "ID", "Name", null);
            var expireTypes = new List<SelectListItem>
            {
                new SelectListItem { Text = "Tháng", Value = "0" },
                new SelectListItem { Text = "Năm", Value = "1" }
            };

            ViewBag.ExpireType = new SelectList(expireTypes, "Value", "Text", null);
        }
    }
}