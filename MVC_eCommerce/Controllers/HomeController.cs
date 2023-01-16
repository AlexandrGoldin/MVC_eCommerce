using MVC_eCommerce.DAL;
using MVC_eCommerce.Models.Admin;
using MVC_eCommerce.Models.Order;
using MVC_eCommerce.Repository;
using MVC_eCommerce.Helper;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MVC_eCommerce.Controllers
{
    public class HomeController : Controller
    {
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        HomeService homeService = new HomeService();

        // GET: Home/Index 
        public ActionResult Index( int? page, int? catId, string sortOrder, string searchString
            , string currentFilter, OrderVM order)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.PriceSortDesc = sortOrder == "price_desc" ? "price_asc" : "price_desc";
            ViewBag.PriceSortAsc = sortOrder == "price_asc" ? "price_desc" : "price_asc";

            if (searchString!=null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;
            int pageSize =0;
            int pageNumber = 0;
            List<ProductVM> listProductVM = AutoMapper.Mapper.Map<List<ProductVM>>(_unitOfWork.GetRepositoryInstance<Tbl_Product>().GetProductList().Where(x => x.IsDelete == false)).ToList();
            List<ProductVM> ListProductVM;
            if (!String.IsNullOrEmpty(searchString))
            {
                var categoryVMList = AutoMapper.Mapper.Map<List<CategoryVM>>(_unitOfWork.GetRepositoryInstance<Tbl_Category>().GetProductList());
                var categoryVM = categoryVMList.Where(x => x.CategoryName.ToLower().Contains(searchString.ToLower())).ToList();
                var productVMList = AutoMapper.Mapper.Map<List<ProductVM>>(_unitOfWork.GetRepositoryInstance<Tbl_Product>().GetProductList());
                pageSize = 8;
                pageNumber = (page ?? 1);
                if (categoryVM.Count == 0)
                {
                    ViewBag.CurrentFilter = searchString;
                    ListProductVM = listProductVM.Where(x => x.ProductName.ToLower().Contains(searchString.ToLower()) && x.IsDelete == false).ToList();
                    switch (sortOrder)
                    {
                        case "price_desc":
                            ListProductVM = ListProductVM.OrderByDescending(x => x.Price).ToList();
                            break;
                        case "price_asc":
                            ListProductVM = ListProductVM.OrderBy(x => x.Price).ToList();
                            break;
                        default:
                            ListProductVM = ListProductVM.OrderBy(x => x.Price).ToList();
                            break;
                    }
                    return View(ListProductVM.ToPagedList(pageNumber, pageSize));
                }
                else
                {
                    catId = categoryVM.ToList()[0].Id;
                    var categoryId = catId;
                    ViewBag.SelectedCat = catId.ToString();
                    ListProductVM = (from product in listProductVM
                                            where (categoryId == null || categoryId == 0 || product.CategoryId == categoryId)
                                            where product.IsDelete == false
                                            select product).ToList();

                    switch (sortOrder)
                    {
                        case "price_desc":
                            ListProductVM = ListProductVM.OrderByDescending(x => x.Price).ToList();
                            break;
                        case "price_asc":
                            ListProductVM = ListProductVM.OrderBy(x => x.Price).ToList();
                            break;                      
                        default:  
                            ListProductVM = ListProductVM.OrderBy(x => x.Price).ToList();
                            break;
                    }
                    return View(ListProductVM.ToPagedList(pageNumber, pageSize)); 
                }
            }
            ListProductVM = (from product in listProductVM
                            where (catId == null || catId == 0 || product.CategoryId == catId)
                            where product.IsDelete == false
                            select product).ToList();
            ViewBag.SelectedCat = catId.ToString();

            switch (sortOrder)
            {
                case "price_desc":
                    ListProductVM = ListProductVM.OrderByDescending(x => x.Price).ToList();
                    break;
                case "price_asc":
                    ListProductVM = ListProductVM.OrderBy(x => x.Price).ToList();
                    break;
                default:
                    ListProductVM = ListProductVM.OrderBy(x => x.Price).ToList();
                    break;
            }
            ViewBag.IndexOrderPartial=homeService.IndexOrderPartial(order);
            pageSize = 8;
            pageNumber = (page ?? 1);
            return View(ListProductVM.ToPagedList(pageNumber, pageSize));
        }

        // GET: home/CategoryMenuPartial
        public ActionResult CategoryMenuPartial()
        {
            List<CategoryVM> categoryVMList = AutoMapper.Mapper.Map<List<CategoryVM>>(_unitOfWork.GetRepositoryInstance<Tbl_Category>()
                .GetAllRecords().Where(x => x.IsDelete == false));            
            return PartialView("_CategoryMenuPartial", categoryVMList);
        }

        //GET: home/ProductDetails? Id = prodId
        public ActionResult ProductDetails (int prodId)
        {
            ProductVM model = new ProductVM();
            var product = _unitOfWork.GetRepositoryInstance<Tbl_Product>().GetFirstorDefault(prodId);
            if (product == null || product.IsDelete == true)
            {
                return RedirectToAction("Index", "Home");
            }
            model = AutoMapper.Mapper.Map<ProductVM>(product);
            return View("ProductDetails",model);

        }       
    }
}