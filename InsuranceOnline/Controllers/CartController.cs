using Insurance.Data.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using InsuranceOnline.Common;
using Insurance.Data.Models;

namespace InsuranceOnline.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Index()
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
            var cartDao = new CartDao();
            var cartItemDao = new CartItemDao();
            var productDao = new ProductDao();
            var userSession = (UserLogin)Session[CommonConstants.USER_SESSION];
            var cart = cartDao.GetByUser(userSession.UserID);
            var listCartItem = new List<CartItem>();
            var listProduct = new List<Product>();
            if (cart != null)
            {
                listCartItem = cartItemDao.GetByCart(cart.Id);
            }

            return View(listCartItem);
        }

        [HttpPost]
        public JsonResult Add(int productId)
        {
            var product = new ProductDao().ViewDetail(productId);
            if (Session[CommonConstants.USER_SESSION] != null)
            {
                var userSession = (UserLogin)Session[CommonConstants.USER_SESSION];
                var cart = new CartDao().GetByUser(userSession.UserID);
                if (cart == null)
                {
                    cart = new Cart
                    {
                        CustomerUserId = (int)userSession.UserID,
                        CreatedDate = DateTime.Now,
                        Status = true,
                    };
                    new CartDao().Insert(cart);
                }
                var cartItem = new CartItem
                {
                    CartID = cart.Id,
                    ProductID = productId,
                    Quantity = 1
                };
                new CartItemDao().Insert(cartItem);
                return Json(new
                {
                    status = true
                });
            }
            else
            {
                return Json(new
                {
                    status = false
                });
            }
        }

        [HttpPost]
        public JsonResult DeleteItem(int id)
        {
            if (Session[CommonConstants.USER_SESSION] != null)
            {
                new CartItemDao().Delete(id);
                return Json(new
                {
                    status = true
                });
            }
            else
            {
                return Json(new
                {
                    status = false
                });
            }
        }

        [HttpPost]
        public JsonResult DeleteAll()
        {
            if (Session[CommonConstants.USER_SESSION] != null)
            {
                var userSession = (UserLogin)Session[CommonConstants.USER_SESSION];
                var cartDao = new CartDao();
                var cart = cartDao.GetByUser(userSession.UserID);
                cartDao.Delete(cart.Id);
                return Json(new
                {
                    status = true
                });
            }
            else
            {
                return Json(new
                {
                    status = false
                });
            }
        }

        public ActionResult Checkout()
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
            var cartDao = new CartDao();
            var cartItemDao = new CartItemDao();
            var userSession = (UserLogin)Session[CommonConstants.USER_SESSION];
            var cart = cartDao.GetByUser(userSession.UserID);
            var listCartItem = new List<CartItem>();
            if (cart != null)
            {
                listCartItem = cartItemDao.GetByCart(cart.Id);
            }

            return View(listCartItem);
        }

        [HttpPost]
        public ActionResult Checkout(string name, string address, string phone, string email, string identity)
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
            try
            {
                var cartDao = new CartDao();
                var cartItemDao = new CartItemDao();
                var productDao = new ProductDao();
                var userSession = (UserLogin)Session[CommonConstants.USER_SESSION];
                var cart = cartDao.GetByUser(userSession.UserID);
                var listCartItem = new List<CartItem>();
                var listProduct = new List<Product>();
                if (cart != null)
                {
                    listCartItem = cartItemDao.GetByCart(cart.Id);
                }

                bool check = false;
                //Thêm vào bảng ProductCustomer
                foreach (var item in listCartItem)
                {
                    var productCustomer = new ProductCustomer
                    {
                        ProductID = item.ProductID,
                        CustomerID = (int)userSession.UserID,
                        CreatedDate = DateTime.Now,
                        CreatedBy = userSession.UserName,
                        Status = true
                    };
                    check = new ProductCustomerDao().Insert(productCustomer);
                }

                if(check)
                {
                    var order = new Order
                    {
                        CustomerName = name,
                        CustomerIdentity = identity,
                        CustomerAddress = address,
                        CustomerMobile = phone,
                        CustomerEmail = email,
                        CreatedDate = DateTime.Now,
                        Status = true,
                        CustomerID = (int)userSession.UserID,
                        TotalPrice = listCartItem.Sum(x => x.Product.Price * x.Quantity),
                        CreatedBy = userSession.UserName
                    };
                    var orderId = new OrderDao().Insert(order);
                    foreach (var item in listCartItem)
                    {
                        var orderDetail = new OrderDetail
                        {
                            OrderID = orderId,
                            ProductID = item.ProductID
                        };
                        new OrderDetailDao().Insert(orderDetail);
                    }
                    cartDao.Delete(cart.Id);
                }
                else
                {
                    return RedirectToAction("CheckoutError", "Cart");
                }
                
            }
            catch (Exception ex)
            {
                return RedirectToAction("CheckoutError", "Cart");
            }

            return RedirectToAction("CheckoutSuccess", "Cart");
        }

        public ActionResult CheckoutSuccess()
        {
            return View();
        }

        public ActionResult CheckoutError()
        {
            return View();
        }
    }
}