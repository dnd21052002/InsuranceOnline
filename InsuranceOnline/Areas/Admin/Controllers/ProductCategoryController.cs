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
    public class ProductCategoryController : BaseController
    {
        // GET: Admin/ProductCategory
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new ProductCategoryDao();
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
        [ValidateInput(false)]
        public ActionResult Create(ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductCategoryDao();
                long id = dao.Insert(productCategory);
                if (id > 0)
                {
                    SetAlert("Thêm danh mục sản phẩm thành công", "success");
                    return RedirectToAction("Index", "ProductCategory");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm danh mục sản phẩm không thành công");
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var productCategory = new ProductCategoryDao().ViewDetail(id);
            return View(productCategory);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductCategoryDao();
                var result = dao.Update(productCategory);
                if (result)
                {
                    SetAlert("Cập nhật danh mục sản phẩm thành công", "success");
                    return RedirectToAction("Index", "ProductCategory");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật danh mục sản phẩm không thành công");
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var dao = new ProductCategoryDao();
            var result = dao.Delete(id);
            if (result)
            {
                SetAlert("Xóa danh mục sản phẩm thành công", "success");
                return RedirectToAction("Index");
            }
            else
            {
                SetAlert("Xóa danh mục sản phẩm không thành công", "error");
                return RedirectToAction("Index");
            }
        }
    }
}