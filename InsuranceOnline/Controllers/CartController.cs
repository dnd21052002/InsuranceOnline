using Insurance.Data.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common;
using InsuranceOnline.Common;
using Insurance.Data.Models;
using System.Configuration;
using InsuranceOnline.Payment;

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
            if (Session[CommonConstants.USER_SESSION] != null)
            {
                var userSession = (UserLogin)Session[CommonConstants.USER_SESSION];
                var cartDao = new CartDao();
                var cart = cartDao.GetByUser(userSession.UserID);

                if (cart == null)
                {
                    // Giỏ hàng chưa tồn tại, tạo giỏ hàng mới và thêm sản phẩm vào giỏ hàng đó
                    cart = new Cart
                    {
                        CustomerUserId = (int)userSession.UserID,
                        CreatedDate = DateTime.Now,
                        Status = true,
                    };
                    cartDao.Insert(cart);
                    var cartItem = new CartItem
                    {
                        CartID = cart.Id,
                        ProductID = productId,
                        Quantity = 1
                    };
                    new CartItemDao().Insert(cartItem);
                }
                else
                {
                    // Giỏ hàng đã tồn tại, kiểm tra xem sản phẩm đã có trong giỏ hàng chưa
                    var cartItemDao = new CartItemDao();
                    var existingCartItem = cartItemDao.GetByCartAndProduct(cart.Id, productId);

                    if (existingCartItem != null)
                    {
                        return Json(new
                        {
                            status = false,
                            exist = true
                        });
                    }
                    else
                    {
                        // Sản phẩm chưa có trong giỏ hàng, thêm sản phẩm mới vào giỏ hàng
                        var newCartItem = new CartItem
                        {
                            CartID = cart.Id,
                            ProductID = productId,
                            Quantity = 1
                        };
                        cartItemDao.Insert(newCartItem);
                    }
                }

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
        public ActionResult Checkout(string name, string address, string phone, string email, string identity, string BankCode)
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
                        PaymentMethod = "VNPAY",
                        Status = false,
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

                    string vnp_Returnurl = ConfigurationManager.AppSettings["vnp_Returnurl"];
                    string vnp_Url = ConfigurationManager.AppSettings["vnp_Url"];
                    string vnp_TmnCode = ConfigurationManager.AppSettings["vnp_TmnCode"];
                    string vnp_HashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"];

                    VnPayLibrary vnpay = new VnPayLibrary();
                    var Price = (long)order.TotalPrice * 100;
                    vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
                    vnpay.AddRequestData("vnp_Command", "pay");
                    vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
                    vnpay.AddRequestData("vnp_Amount", Price.ToString());
                    if(BankCode == "VNPAYQR")
                    {
                        vnpay.AddRequestData("vnp_BankCode", "VNPAYQR");
                    }
                    else if(BankCode == "VNBANK")
                    {
                         vnpay.AddRequestData("vnp_BankCode", "VNBANK");
                    }
                    else if(BankCode == "INTCARD")
                    {
                        vnpay.AddRequestData("vnp_BankCode", "INTCARD");
                    }
                    vnpay.AddRequestData("vnp_CreateDate", order.CreatedDate?.ToString("yyyyMMddHHmmss"));
                    vnpay.AddRequestData("vnp_CurrCode", "VND");
                    vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress());
                    vnpay.AddRequestData("vnp_Locale", "vn");
                    vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang:" + order.ID);
                    vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other

                    vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
                    vnpay.AddRequestData("vnp_TxnRef", order.ID.ToString()); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày

                    //Add Params of 2.1.0 Version
                    //Billing

                    string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);

                    return Redirect(paymentUrl);
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
        }

        public ActionResult CheckoutSuccess()
        {
            return View();
        }

        public ActionResult CheckoutError()
        {
            return View();
        }

        public ActionResult VnPayReturn()
        {
            if (Request.QueryString.Count > 0)
            {
                string vnp_HashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"]; //Chuoi bi mat
                var vnpayData = Request.QueryString;
                VnPayLibrary vnpay = new VnPayLibrary();

                foreach (string s in vnpayData)
                {
                    //get all querystring data
                    if (!string.IsNullOrEmpty(s) && s.StartsWith("vnp_"))
                    {
                        vnpay.AddResponseData(s, vnpayData[s]);
                    }
                }
                //vnp_TxnRef: Ma don hang merchant gui VNPAY tai command=pay    
                //vnp_TransactionNo: Ma GD tai he thong VNPAY
                //vnp_ResponseCode:Response code from VNPAY: 00: Thanh cong, Khac 00: Xem tai lieu
                //vnp_SecureHash: HmacSHA512 cua du lieu tra ve

                long orderId = Convert.ToInt64(vnpay.GetResponseData("vnp_TxnRef"));
                long vnpayTranId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
                string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
                string vnp_TransactionStatus = vnpay.GetResponseData("vnp_TransactionStatus");
                string vnp_SecureHash = Request.QueryString["vnp_SecureHash"];
                string TerminalID = Request.QueryString["vnp_TmnCode"];
                long vnp_Amount = Convert.ToInt64(vnpay.GetResponseData("vnp_Amount")) / 100;
                string bankCode = Request.QueryString["vnp_BankCode"];

                bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);
                if (checkSignature)
                {
                    if (vnp_ResponseCode == "00" && vnp_TransactionStatus == "00")
                    {
                        //Thanh toan thanh cong
                        ViewBag.InnerText = "Giao dịch được thực hiện thành công. Cảm ơn quý khách đã mua bảo hiểm của chúng tôi!";
                        //log.InfoFormat("Thanh toan thanh cong, OrderId={0}, VNPAY TranId={1}", orderId, vnpayTranId);

                        //Update trang thai don hang
                        var orderDao = new OrderDao();
                        var order = orderDao.ViewDetail(orderId);
                        order.Status = true;
                        orderDao.Update(order);

                        //Xoa gio hang
                        var cartDao = new CartDao();
                        var cart = cartDao.GetByUser(order.CustomerID);
                        if (cart != null)
                        {
                            cartDao.Delete(cart.Id);
                        }
                    }
                    else if (vnp_ResponseCode == "24")
                    {
                        ViewBag.InnerText = "Quý khách đã huỷ giao dịch, vui lòng thực hiện thanh toán lại!";
                    }
                    else
                    {
                        //Thanh toan khong thanh cong. Ma loi: vnp_ResponseCode
                        ViewBag.InnerText = "Có lỗi xảy ra trong quá trình xử lý. Mã lỗi: " + vnp_ResponseCode;
                        //log.InfoFormat("Thanh toan loi, OrderId={0}, VNPAY TranId={1},ResponseCode={2}", orderId, vnpayTranId, vnp_ResponseCode);
                    }
                    ViewBag.TerminalID = TerminalID;
                    ViewBag.orderId = orderId;
                    ViewBag.vnpayTranId = vnpayTranId;
                    ViewBag.vnp_Amount = vnp_Amount;
                    ViewBag.bankCode = bankCode;

                    //displayTxnRef.InnerText = "Mã giao dịch thanh toán:" + orderId.ToString();
                    //displayVnpayTranNo.InnerText = "Mã giao dịch tại VNPAY:" + vnpayTranId.ToString();
                    //displayAmount.InnerText = "Số tiền thanh toán (VND):" + vnp_Amount.ToString();
                    //displayBankCode.InnerText = "Ngân hàng thanh toán:" + bankCode;
                }
                else
                {
                    //log.InfoFormat("Invalid signature, InputData={0}", Request.RawUrl);
                    ViewBag.InnerText = "Có lỗi xảy ra trong quá trình xử lý";
                }
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}