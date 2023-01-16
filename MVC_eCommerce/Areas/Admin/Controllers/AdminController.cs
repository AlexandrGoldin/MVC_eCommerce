using MVC_eCommerce.DAL;
using MVC_eCommerce.Models.Admin;
using MVC_eCommerce.Models.Order;
using MVC_eCommerce.Repository;
using MVC_eCommerce.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq; 
using System.Web;
using System.Web.Mvc;

namespace MVC_eCommerce.Areas.Admin.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        OrderSevice orderSevice = new OrderSevice();

        public ActionResult Index()
        {
            return RedirectToAction("Dashboard");
        }

        // GET: /Admin/Admin/Dashboard
        public ActionResult Dashboard()
        {
            return View();
        }

        // GET: /Admin/Admin/
        public ActionResult Categories()
        {
            List<Tbl_Category> allcategories = _unitOfWork.GetRepositoryInstance<Tbl_Category>().GetAllRecordsIQueryable().ToList();
            return View(allcategories);
        }

        public ActionResult AddCategory()
        {
            return View("AddCategory");
        }

        [HttpPost]
        public ActionResult AddCategory(CategoryVM model)
        {
            if (_unitOfWork.GetRepositoryInstance<Tbl_Category>().GetProductList().Any(x => x.CategoryName == model.CategoryName))
            {
                ModelState.AddModelError("CategoryName", "That category name is taken!");
                return View(model);
            }
            _unitOfWork.GetRepositoryInstance<Tbl_Category>().Add(AutoMapper.Mapper.Map<Tbl_Category>(model));
            _unitOfWork.SaveChanges();

            TempData["SM"] = "You have added a category!";
            return RedirectToAction("AddCategory");
        }

        public ActionResult CategoryEdit(int categoryId)
        {
            return View (AutoMapper.Mapper.Map<CategoryVM>(_unitOfWork.GetRepositoryInstance<Tbl_Category>().GetFirstorDefault(categoryId)));
        }

        [HttpPost]
        public ActionResult CategoryEdit(CategoryVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _unitOfWork.GetRepositoryInstance<Tbl_Category>().Update(AutoMapper.Mapper.Map<Tbl_Category>(model));
            _unitOfWork.SaveChanges();
            TempData["SM"] = "You have edited a category!";
            return RedirectToAction("CategoryEdit", new {model.Id });
        }

        // GET: /admin/admin/Products
        public ActionResult Products(int? catId, string msg)
        {
            List<ProductVM> productVMList = AutoMapper.Mapper.Map<List<ProductVM>>(_unitOfWork.GetRepositoryInstance<Tbl_Product>().GetProductList());
            var ProductVMList = from product in productVMList
                                where (catId == null || catId == 0 || product.CategoryId == catId)
                                select product;
            ViewBag.Categories= new SelectList(_unitOfWork.GetRepositoryInstance<Tbl_Category>().GetAllRecords(), "Id", "CategoryName");
            ViewBag.SelectedCat = catId.ToString();
            TempData["SM"] = msg;
            return View(ProductVMList);
        }

        [HttpGet]
        public ActionResult ProductEdit(int Id)
        {
            ProductVM model = new ProductVM();         
            var product=_unitOfWork.GetRepositoryInstance<Tbl_Product>().GetFirstorDefault(Id);
           if(product==null)
           {
                return Content("That product does not exist");
           }
            model = AutoMapper.Mapper.Map<ProductVM>(product);
            model.Categories = new SelectList(_unitOfWork.GetRepositoryInstance<Tbl_Category>().GetAllRecords(), "Id", "CategoryName");
            return View(model);
        }

        [HttpPost]
        public ActionResult ProductEdit(ProductVM model, HttpPostedFileBase file)
        {
            model.Categories = new SelectList(_unitOfWork.GetRepositoryInstance<Tbl_Category>().GetAllRecords(), "Id", "CategoryName");

            if (!ModelState.IsValid)
            {
                return View(model);
            }     
            string pic = null;
            if (file != null&&file.ContentLength>0)
            {
                string ext = file.ContentType.ToLower();
                if(ext !="image/jpg"&&
                   ext != "image/jpeg" &&
                   ext != "image/pjpeg" &&
                   ext != "image/gif" &&
                   ext != "image/png" &&
                   ext != "image/x-png" )
                {
                    ModelState.AddModelError("", "That image was not uploaded - wrong image extension");
                    return View(model);
                }
                pic = Path.GetFileName(file.FileName);
                string path = Path.Combine(Server.MapPath("~/ProductImg"), pic);
                file.SaveAs(path);
            }
            model.ProductImage = file != null ? pic : model.ProductImage;
            model.ModifiedDate = DateTime.Now;
            using(GenericUnitOfWork _unitOfWork = new GenericUnitOfWork())
            {
                _unitOfWork.GetRepositoryInstance<Tbl_Product>().Update(AutoMapper.Mapper.Map<Tbl_Product>(model));
                _unitOfWork.SaveChanges();
            }           
            TempData["SM"] = "You have edited a product!";
            return RedirectToAction("ProductEdit", new {model.Id} );
        }

        [HttpGet]
        public ActionResult ProductAdd()
        {
            ProductVM model = new ProductVM();
            model.Categories = new SelectList(_unitOfWork.GetRepositoryInstance<Tbl_Category>().GetAllRecords(), "Id", "CategoryName");
            return View(model);
        }

        [HttpPost]
        public ActionResult ProductAdd(ProductVM model, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = new SelectList(_unitOfWork.GetRepositoryInstance<Tbl_Category>().GetAllRecords(), "Id", "CategoryName");
                return View(model);
            }
            if(_unitOfWork.GetRepositoryInstance<Tbl_Product>().GetProductList().Any(x=>x.ProductName==model.ProductName))
            {
                model.Categories = new SelectList(_unitOfWork.GetRepositoryInstance<Tbl_Category>().GetAllRecords(), "Id", "CategoryName");
                ModelState.AddModelError("", "That product name is taken!");
                return View(model);
            }
            string pic = null;
            if (file != null && file.ContentLength > 0)
            {
                string ext = file.ContentType.ToLower();
                if (ext != "image/jpg" &&
                   ext != "image/jpeg" &&
                   ext != "image/pjpeg" &&
                   ext != "image/gif" &&
                   ext != "image/png" &&
                   ext != "image/x-png")
                {
                    model.Categories = new SelectList(_unitOfWork.GetRepositoryInstance<Tbl_Category>().GetAllRecords(), "Id", "CategoryName");
                    ModelState.AddModelError("", "That image was not uploaded - wrong image extension");
                    return View(model);
                }

                pic = Path.GetFileName(file.FileName);
                string path = Path.Combine(Server.MapPath("~/ProductImg"), pic);
                file.SaveAs(path);
            }
            model.ProductImage = pic;
            model.CreatedDate = DateTime.Now;
            _unitOfWork.GetRepositoryInstance<Tbl_Product>().Add(AutoMapper.Mapper.Map<Tbl_Product>(model));
            _unitOfWork.SaveChanges();
            TempData["SM"] = "You have added a product!";
            return RedirectToAction("ProductAdd");
        }

        [HttpGet]
        public ActionResult ProductDelete(int productId)
        {
            var product =_unitOfWork.GetRepositoryInstance<Tbl_Product>().GetFirstorDefault(productId);
            var category = _unitOfWork.GetRepositoryInstance<Tbl_Category>().GetFirstorDefault(product.CategoryId);

            if (product == null)
            {
                return HttpNotFound();
            }
            ProductVM model = new ProductVM();
            model = AutoMapper.Mapper.Map<ProductVM>(product);
            model.CategoryName = category.CategoryName;         
            return View(model);
        }

        [HttpPost, ActionName("ProductDelete")]
        public ActionResult DeleteConfirmed(int productId)
        {
            var product = _unitOfWork.GetRepositoryInstance<Tbl_Product>().GetFirstorDefault(productId);
            if (product == null)
            {
                return HttpNotFound();

            }
            _unitOfWork.GetRepositoryInstance<Tbl_Product>().Remove(productId);
            _unitOfWork.SaveChanges();

            var productList= _unitOfWork.GetRepositoryInstance<Tbl_Product>().GetAllRecords().Where(x=>x.ProductImage==product.ProductImage);
            if(productList.Count()==0)
            {
                string path = Path.Combine(Server.MapPath("~/ProductImg"), product.ProductImage);
                if (Directory.Exists(path))
                    Directory.Delete(path);

                return RedirectToAction("Products", new { msg = "You have deleted a order!" });
            }

            return RedirectToAction("Products", new { msg= "You have deleted a order!" });
        }


        // GET: Admin/Admin/GetOrder?orderId={id}
        public ActionResult GetOrder(int orderId)
        {         
            var orderVM = orderSevice.GetOrder(orderId);
            
            if (orderVM == null)
            {
                return Content("That order does not exist");
            }
            return View(orderVM);
        }

        // GET: Admin/Admin/GetOrderList
        public ActionResult GetOrderList()
        {
            return View(orderSevice.GetOrderList());
        }

        // GET: Admin/Admin/OrderEdit?orderId={id}
        public ActionResult OrderEdit(int orderId)
        {
            OrderVM model = orderSevice.GetOrder(orderId);
            model.User = AutoMapper.Mapper.Map<UserVM>(_unitOfWork.GetRepositoryInstance<Tbl_User>().GetFirstorDefault(model.UserId));
            if (model == null)
            {
                return Content("That order does not exist");
            }          
            model.StatusSelectList = new SelectList(_unitOfWork.GetRepositoryInstance<Tbl_Status>().GetAllRecords(), "Id", "StatusName");
            return View(model);
        }

        [HttpPost]
        public ActionResult OrderEdit(OrderVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            model.ModifiedDate= DateTime.Now;
            using (GenericUnitOfWork _unitOfWork = new GenericUnitOfWork())
            {
                _unitOfWork.GetRepositoryInstance<Tbl_Order>().Update(AutoMapper.Mapper.Map<Tbl_Order>(model));
                _unitOfWork.SaveChanges();
            }
            TempData["SM"] = "You have edited a order!";
            return RedirectToAction("OrderEdit", new { orderId=model.Id });
        }

        // GET: Admin/Admin/OrderDelete
        public ActionResult OrderDelete(int orderId)
        {
            _unitOfWork.GetRepositoryInstance<Tbl_Order>().Remove(orderId);
            _unitOfWork.SaveChanges();

            TempData["SM"] = "You have deleted a order!";
            return RedirectToAction("GetOrderList");
        }
    }
}