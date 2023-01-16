using MVC_eCommerce.DAL;
using MVC_eCommerce.Models.Admin;
using MVC_eCommerce.Models.Home;
using MVC_eCommerce.Models.Order;
using MVC_eCommerce.Repository;
using MVC_eCommerce.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MVC_eCommerce.Controllers
{
    public class OrderController : Controller, IController
    {
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        OrderSevice orderSevice = new OrderSevice();

        // GET: Order/CreateOrder
        [OutputCache(Duration = 60)]
        [HttpGet]
        public ActionResult CreateOrder()
        {          
            UserVM user = new UserVM();
            user.Order = Session["order"] as OrderVM ?? new OrderVM();
            List<CartItemVM> cart = Session["cart"] as List<CartItemVM> ?? new List<CartItemVM>();

            if (cart.Count == 0 || Session["cart"] == null)
            {
                TempData["DM"] = "You cart is empty!";
                return RedirectToAction("Index", "Home", new { TempData });
            }
            decimal? total = 0m;
            foreach (var item in cart)
            {
                total += item.Product.Price * item.Count;
            }
            Session["cart"] = cart;

            user.Order.Cart=
            user.Order.Cart = cart;
            user.Order.TotalPrice = total;
            Session["order"] = user.Order;
            return View(user);
        }

        StatusVM statusVM = new StatusVM();
        public static List<OrderVM> orderVMList = new List<OrderVM>();

        [HttpPost]
        public ActionResult CreateOrder(UserVM user)
        {
            if (!ModelState.IsValid)
            {
                TempData["DM"] = "You Form is empty!";
                return RedirectToAction("CreateOrder", "Order", new { TempData });
            }

            OrderVM order = Session["order"] as OrderVM ?? new OrderVM();
            List<CartItemVM> cart = Session["cart"] as List<CartItemVM> ?? new List<CartItemVM>();
            List<OrderVM> orders = Session["orders"] as List<OrderVM> ?? new List<OrderVM>();

            if (cart.Count == 0 || Session["cart"] == null)
            {
                TempData["DM"] = "You cart is empty!";
                return RedirectToAction("Index", "Home", new { TempData });
            }
            decimal? total = 0m;
            var statusVMList = _unitOfWork.GetRepositoryInstance<Tbl_Status>().GetAllRecordsIQueryable().Where(x => x.Id != 0).ToList();
            List<CartItemVM> cartRes = new List<CartItemVM>();
            foreach (var item in cart)
            {
                total += item.Product.Price * item.Count;
                cartRes.Add(new CartItemVM
                {
                    TotalPrice = item.Product.Price * item.Count,
                    Count = item.Count,
                    ProductId = item.Product.Id,
                });
            }
            order.Cart = cartRes;
            order.TotalPrice = total;
            order.StatusId = statusVMList[0].Id;
            order.OrderDate = DateTime.Now;
            order.User = user;
            order.IsActive = true;
            order.User.IpAddress = HttpContext.Request.UserHostAddress;

            orders.Add(new OrderVM()
            {
                User = order.User,
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice,
                Cart = order.Cart
            });
            Session["order"] = order;
            Session["orders"] = orders;
            if (string.IsNullOrEmpty(user.CastomerName) && string.IsNullOrEmpty(user.Email)
                && string.IsNullOrEmpty(user.Address))
            {
                ModelState.AddModelError("", "Model is not valid");
                return View(user);
            }
            _unitOfWork.GetRepositoryInstance<Tbl_Order>().Add(AutoMapper.Mapper.Map<Tbl_Order>(order));
            _unitOfWork.SaveChanges();

            orderSevice.RemoveOrderedCart(cart);
            Session["cart"] = cart;
            DateTime? orderDate = order.OrderDate;
            TempData["SM"] = $"You order for the amount:{order.TotalPrice}$ dated {orderDate.Value.ToString("dd.MM.yyyy")} is accepted! Delivery of the order will be sent to your email address.";
            return RedirectToAction("Index", "Home", new { TempData });
        }

        private int orderCount;

        public ActionResult OrderPartial()
        {
            if (Session["orders"] == null)
            {
                orderCount = 0;
                ViewBag.OrderCount = orderCount;
                return PartialView("_OrderPartial", orderCount);
            }
            List<OrderVM> orderVMLists = (List<OrderVM>)Session["orders"];
            orderCount = orderVMLists.Distinct().Count();
            ViewBag.OrderCount = orderVMLists.Count();
            Session["orders"] = orderVMLists;
            return PartialView("_OrderPartial", orderCount);
        }

        //GET: order/CheckoutOrderDetails
        public ActionResult CheckoutOrderDetails()
        {
            OrderVM order = Session["order"] as OrderVM ?? new OrderVM();

            List<CartItemVM> cart = Session["cart"] as List<CartItemVM> ?? new List<CartItemVM>();
            List<OrderVM> orders = Session["orders"] as List<OrderVM> ?? new List<OrderVM>();
            List<CartItemVM> cartItemVM = new List<CartItemVM>();
            List<OrderVM> orderVMLists = new List<OrderVM>();
            List<OrderDetailVM> orderDetailVMList = HttpContext.Cache["orderDetailVMList"] as List<OrderDetailVM> ?? new List<OrderDetailVM>();


            if (orders.Count == 0 || Session["orders"] == null)
            {
                ViewBag.Message = "You have no orders ";
                return RedirectToAction("CheckoutPastOrderDetails", "Order");
            }
            List<ProductVM> products = AutoMapper.Mapper.Map<List<ProductVM>>(_unitOfWork.GetRepositoryInstance<Tbl_Product>().GetAllRecords());
            OrderDetailVM orderVM = new OrderDetailVM();
            ProductVM productVM = new ProductVM();
            var tbl_productList = _unitOfWork.GetRepositoryInstance<Tbl_Product>().GetAllRecords().ToList();
            foreach (var item in orders)
            {
                List<string> productImgList = new List<string>();

                foreach (var c in item.Cart)
                {
                    int prodId = c.ProductId;
                    productImgList.Add(products.FirstOrDefault(x => x.Id == c.ProductId).ProductImage);
                }
                orderDetailVMList.Add(new OrderDetailVM
                {
                    Order = item,
                    ProductImgList = productImgList,
                });
            }
            return View(orderDetailVMList);
        }

        //GET: order/CheckoutPastOrderDetails

        public ActionResult CheckoutPastOrderDetails()
        {
            string ipAddress = HttpContext.Request.UserHostAddress;
            List<OrderDetailVM> pastOrderDetailVMList = new List<OrderDetailVM>();
            List<ProductVM> products = AutoMapper.Mapper.Map<List<ProductVM>>(_unitOfWork.GetRepositoryInstance<Tbl_Product>().GetAllRecords());
            List<OrderVM> orderVMList = AutoMapper.Mapper.Map<List<OrderVM>>(orderSevice.GetOrderList());
            var orderList = orderVMList.Where(x => x.User.IpAddress == ipAddress).ToList();
           
            foreach (var item in orderList)
            {
                List<string> productImgList = new List<string>();

                foreach (var c in item.Cart)
                {
                    int prodId = c.ProductId;
                    productImgList.Add(products.FirstOrDefault(x => x.Id == c.ProductId).ProductImage);
                }
                pastOrderDetailVMList.Add(new OrderDetailVM
                {
                    Order = item,
                    ProductImgList = productImgList,
                });
            }         
            return View(pastOrderDetailVMList.OrderByDescending(s => s.Order.OrderDate));
        }
    }
}