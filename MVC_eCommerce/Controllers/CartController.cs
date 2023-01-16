using MVC_eCommerce.DAL;
using MVC_eCommerce.Models.Admin;
using MVC_eCommerce.Models.Home;
using MVC_eCommerce.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MVC_eCommerce.Controllers
{
    public class CartController : Controller
    {
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();

        // GET: Cart
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddToCartPartial(int productId)
        {
            List<CartItemVM> cart = Session["cart"] as List<CartItemVM> ?? new List<CartItemVM>();
            CartItemVM model = new CartItemVM();
            var product = _unitOfWork.GetRepositoryInstance<Tbl_Product>().GetFirstorDefault(productId);
            var productCart = cart.FirstOrDefault(x => x.Product.Id == productId);

            if (productCart == null)
            {
                cart.Add(new CartItemVM()
                {
                    ProductId = AutoMapper.Mapper.Map<ProductVM>(product).Id,
                    Product = AutoMapper.Mapper.Map<ProductVM>(product),
                    Count = 1
                });
            }
            else
            {
                productCart.Count++;
            }

            Nullable<int> qty = 0; decimal? price = 0m;

            foreach (var item in cart)
            {
                qty += item.Count;
                price += item.Product.Price;
            }

            model.ProductId = product.Id;
            model.Count = qty;
            model.TotalPrice = price;
            Session["cart"] = cart;
            
            return RedirectToAction("CheckoutCartDetails", "Cart");
        }

        //GET: cart/CheckoutCartDetails
        public ActionResult CheckoutCartDetails()
        {
            List<CartItemVM> cart = Session["cart"] as List<CartItemVM> ?? new List<CartItemVM>();
            if (cart.Count == 0 || Session["cart"] == null)
            {
                TempData["DM"] = "You cart is empty!";
                return RedirectToAction("Index", "Home", new { TempData });
            }
            decimal? total = 0m;
            foreach (var item in cart)
            {
                total += item.Product.Price * item.Count; ;
            }
            ViewBag.GrandTotal = total;

            return View(cart);
        }

        //GET: /cart/IncrementProduct
        public JsonResult IncrementProduct(int productId)
        {
            List<CartItemVM> cart = Session["cart"] as List<CartItemVM> ?? new List<CartItemVM>();
            CartItemVM model = cart.FirstOrDefault(x => x.Product.Id == productId);

            model.Count++;

            var result = new { qty = model.Count, price = model.Product.Price };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //GET: /cart/DecrementProduct
        public JsonResult DecrementProduct(int productId)
        {
            List<CartItemVM> cart = Session["cart"] as List<CartItemVM> ?? new List<CartItemVM>();
            CartItemVM model = cart.FirstOrDefault(x => x.Product.Id == productId);

            if (model.Count > 1)
                model.Count--;
            else
            {
                model.Count = 0;
                cart.Remove(model);
            }
            var result = new { qty = model.Count, price = model.Product.Price };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: cart/RemoveFromCart
        public void RemoveFromCart(int productId)
        {
            List<CartItemVM> cart = Session["cart"] as List<CartItemVM>;
            CartItemVM model = cart.FirstOrDefault(x => x.Product.Id == productId);
            cart.Remove(model);
        }

        // GET: cart/CartPartial
        public ActionResult CartPartial()
        {
            CartItemVM model = new CartItemVM();
            Nullable<int> qty = 0;
            decimal? price = 0m;

            if (Session["cart"] != null)
            {
                List<CartItemVM> list = (List<CartItemVM>)Session["cart"];

                foreach (var item in list)
                {
                    qty += item.Count;
                    price += item.Product.Price * item.Count;
                }
                model.Count = qty;
                model.TotalPrice = price;
            }
            else
            {
                model.Count = 0;
                model.TotalPrice = 0;
            }
            return PartialView("_CartPartial", model);
        }
    }
}