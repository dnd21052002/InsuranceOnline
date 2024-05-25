using Common;
using Insurance.Data;
using Insurance.Data.Dao;
using Insurance.Data.Models;
using InsuranceOnline.Common;
using InsuranceOnline.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InsuranceOnline.Controllers
{
    public class ClaimController : Controller
    {
        private readonly InsuranceDbContext _context = new InsuranceDbContext();

        // GET: Claim/Create
        public ActionResult Create()
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
            var model = new ClaimViewModel
            {
                Insurances = GetInsurances()
            };
            return View(model);
        }

        // POST: Claim/Create
        [HttpPost]
        public ActionResult Create(ClaimViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = (UserLogin)Session[CommonConstants.USER_SESSION];
                var claim = new Claim
                {
                    FullName = model.FullName,
                    CitizenId = model.CitizenId,
                    InsuranceId = model.SelectedInsuranceId,
                    InsuranceName = _context.ProductCategories.FirstOrDefault(x => x.ID == model.SelectedInsuranceId).Name,
                    ClaimDocuments = new List<ClaimDocument>(),
                    Status = 0,
                    UserId = user.UserID,
                    CreatedDate = DateTime.Now
                };

                // Save claim to database
                _context.Claims.Add(claim);
                _context.SaveChanges();

                // Save uploaded files and their information to the database
                foreach (var file in model.Documents)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine("/Data/Claim", fileName);
                        var absolutePath = Server.MapPath(path);
                        file.SaveAs(absolutePath);

                        var claimDocument = new ClaimDocument
                        {
                            ClaimId = claim.Id,
                            FileName = fileName,
                            FilePath = path
                        };

                        _context.ClaimDocuments.Add(claimDocument);
                    }
                }

                _context.SaveChanges();

                return RedirectToAction("Success");
            }

            model.Insurances = GetInsurances();
            return View(model);
        }

        private List<SelectListItem> GetInsurances()
        {
            var user = (UserLogin)Session[CommonConstants.USER_SESSION];
            var productCustomerDao = new ProductCustomerDao();
            var products = productCustomerDao.ListByCustomer(user.UserID);

            var categories = new HashSet<int>();
            var selectListItems = new List<SelectListItem>();

            foreach (var productCustomer in products)
            {
                var categoryId = productCustomer.Product.ProductCategory.ID;
                if (!categories.Contains(categoryId))
                {
                    categories.Add(categoryId);
                    selectListItems.Add(new SelectListItem
                    {
                        Text = productCustomer.Product.ProductCategory.Name,
                        Value = categoryId.ToString()
                    });
                }
            }

            return selectListItems;
        }


        public ActionResult Success()
        {
            return View();
        }
    }
}