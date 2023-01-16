using MVC_eCommerce.DAL;
using MVC_eCommerce.Interfaces;
using MVC_eCommerce.Models.Admin;
using MVC_eCommerce.Models.Home;
using MVC_eCommerce.Models.Order;
using MVC_eCommerce.Repository;
using System.Collections.Generic;
using System.Linq;

namespace MVC_eCommerce.Helper
{
    public class HomeService : IHomeService
    {
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();

        public IndexVM GetSearch(string search)
        {
            IndexVM indexVM = new IndexVM();
            indexVM.categoryVMList = AutoMapper.Mapper.Map<List<CategoryVM>>(_unitOfWork.GetRepositoryInstance<Tbl_Category>().GetProductList());
            
            var categoryVM = indexVM.categoryVMList.Where(x => x.CategoryName.ToLower().Contains(search.ToLower())).ToList();
            var productVMList = AutoMapper.Mapper.Map<List<ProductVM>>(_unitOfWork.GetRepositoryInstance<Tbl_Product>().GetProductList());

            if (categoryVM.Count== 0)
            {
                indexVM.CurrentSearch = search;
                indexVM.ListProductVM = productVMList.Where(x => x.ProductName.ToLower().Contains(search.ToLower()) && x.IsDelete == false);
                return indexVM;
            }
           else
            {
                var catId = categoryVM.ToList()[0].Id;
                var categoryId = catId;
                indexVM.CatIdToString = catId.ToString();
                indexVM.ListProductVM = from product in productVMList
                                        where (categoryId == 0 || product.CategoryId == categoryId)
                                        where product.IsDelete == false
                                        select product;
                return indexVM;
            }
        }

        public int IndexOrderPartial(OrderVM order)
        {
            List<OrderVM> orders = new List<OrderVM>();
            int count = 0;
            if (order.Cart == null && order.User == null)
            {
                return count;
            }      
            count += 1;
      
            return count;
        }
    }
}