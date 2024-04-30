using Insurance.Data.Dao;
using Insurance.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InsuranceOnline.Areas.Admin.Controllers
{
    public class OrderController : BaseController
    {
        // GET: Admin/Order
        public ActionResult Index(string searchString, int page = 1, int pageSize = 10)
        {
            var dao = new OrderDao();
            var model = dao.ListAllPaging(searchString, page, pageSize);

            ViewBag.SearchString = searchString;
            return View(model);
        }

        public ActionResult Detail(long id)
        {
            var orderDetails = new OrderDao().GetOrderDetailsByOrderId(id);
            var productQuantities = orderDetails
                                .GroupBy(od => od.ProductID)
                                .Select(group => new
                                {
                                    ProductID = group.Key,
                                    Quantity = group.Count() // hoặc tính tổng dựa trên một trường số lượng nếu bạn có
                                })
                                .ToList();

            // Lưu trữ kết quả trong ViewBag
            ViewBag.ProductQuantities = productQuantities;
            return View(orderDetails);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {
                var dao = new OrderDao();
                long id = dao.Insert(order);
                if (id > 0)
                {
                    SetAlert("Thêm đơn hàng thành công", "success");
                    return RedirectToAction("Index", "Order");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm đơn hàng không thành công");
                }
            }
            return View();
        }

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var order = new OrderDao().ViewDetail(id);

            return View(order);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Order order)
        {
            ModelState.Remove("CustomerName");
            ModelState.Remove("CustomerAddress");
            ModelState.Remove("CustomerEmail");
            ModelState.Remove("CustomerIdentity");
            ModelState.Remove("CustomerMobile");
            ModelState.Remove("PaymentStatus");
            if (ModelState.IsValid)
            {
                var dao = new OrderDao();
                var result = dao.Update(order);
                if (result)
                {
                    SetAlert("Cập nhật đơn hàng thành công", "success");
                    return RedirectToAction("Index", "Order");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật đơn hàng không thành công");
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var dao = new OrderDao();
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
    }
}