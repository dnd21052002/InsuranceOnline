using Common;
using Insurance.Data.Dao;
using Insurance.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InsuranceOnline.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult Header()
        {
           if (Session[CommonConstants.USER_SESSION] != null)
            {
                ViewBag.User = Session[CommonConstants.USER_SESSION];
            }
            var productCateDao = new ProductCategoryDao();
            var listProductCate = productCateDao.ListAll();

            return PartialView(listProductCate);
        }

        [ChildActionOnly]
        public ActionResult ProductCat()
        {
            var productCateDao = new ProductCategoryDao();
            var listProductCate = productCateDao.ListAll().Take(6);

            return PartialView(listProductCate);
        }

        //[ChildActionOnly]
        public ActionResult ProductCatFooter()
        {
            var productCateDao = new ProductCategoryDao();
            var listProductCate = productCateDao.ListAll().Take(6);

            return PartialView(listProductCate);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}