using Insurance.Data;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InsuranceOnline.Areas.Admin.Controllers
{
    public class ClaimController : BaseController
    {
        private readonly InsuranceDbContext _context = new InsuranceDbContext();

        // GET: AdminClaim
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            var claims = _context.Claims.Include("ClaimDocuments").ToList();
            // topagedlist

            return View(claims.ToPagedList(page, pageSize));
        }

        // GET: AdminClaim/Details/5
        public ActionResult Details(int id)
        {
            var claim = _context.Claims.Include("ClaimDocuments").SingleOrDefault(c => c.Id == id);
            if (claim == null)
            {
                return HttpNotFound();
            }
            return View(claim);
        }

        // POST: AdminClaim/Approve/5
        [HttpPost]
        public ActionResult Approve(int id)
        {
            var claim = _context.Claims.SingleOrDefault(c => c.Id == id);
            if (claim == null)
            {
                return HttpNotFound();
            }
            claim.Status = 1;
            claim.ApprovedDate = DateTime.Now;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: AdminClaim/Reject/5
        [HttpPost]
        public ActionResult Reject(int id)
        {
            var claim = _context.Claims.SingleOrDefault(c => c.Id == id);
            if (claim == null)
            {
                return HttpNotFound();
            }
            claim.Status = 2;
            claim.ApprovedDate = DateTime.Now;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}