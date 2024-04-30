﻿using Insurance.Data.Dao;
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
                return RedirectToAction("Login", "User", new {returnUrl = returnUrl1 });
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
                foreach (var item in listCartItem)
                {
                    listProduct.Add(productDao.ViewDetail(item.ProductID));
                }
                ViewBag.ListProduct = listProduct;
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
            if(Session[CommonConstants.USER_SESSION] != null)
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
    }
}