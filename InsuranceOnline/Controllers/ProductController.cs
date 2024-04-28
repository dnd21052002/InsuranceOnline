using Insurance.Data.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InsuranceOnline.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductCategory(int id)
        {
            var dao = new ProductCategoryDao();

            var model = dao.ViewDetail(id);

            ViewBag.ListCategory = dao.ListAll();

            return View(model);
        }
    }
}