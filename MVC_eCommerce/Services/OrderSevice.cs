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
    public class OrderSevice : IOrderSevice
    {
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();

        public OrderVM GetOrder(int orderId)
        {
            Tbl_Order order = _unitOfWork.GetRepositoryInstance<Tbl_Order>().GetFirstorDefault(orderId);
            OrderVM orderVM = AutoMapper.Mapper.Map<OrderVM>(order);
            List<CartItemVM> cartItemVMlList = new List<CartItemVM>();
            List<Tbl_Cart> cartList = _unitOfWork.GetRepositoryInstance<Tbl_Cart>().GetAllRecordsIQueryable().Where(x=>x.OrderId== orderId).ToList();

            foreach (var item in cartList)
            {
                var product = _unitOfWork.GetRepositoryInstance<Tbl_Product>().GetFirstorDefault(item.ProductId); 
                CartItemVM cartItemVM = AutoMapper.Mapper.Map<CartItemVM>(item);
                cartItemVM.TotalPrice = item.Count * item.Product.Price;
                cartItemVM.Product = AutoMapper.Mapper.Map<ProductVM>(product);

                cartItemVMlList.Add(cartItemVM);
            }

            if (orderVM != null)
                orderVM.Cart = cartItemVMlList;
                orderVM.User = AutoMapper.Mapper.Map<UserVM>(_unitOfWork.GetRepositoryInstance<Tbl_User>().GetFirstorDefault(orderId));
                orderVM.Status = AutoMapper.Mapper.Map<StatusVM>(_unitOfWork.GetRepositoryInstance<Tbl_Status>().GetFirstorDefault(orderVM.StatusId));

            return orderVM;
        }

        public IEnumerable<OrderVM> GetOrderList()
        {
            OrderVM orderVM = new OrderVM();
            List<OrderVM> orderVMList = new List<OrderVM>();
            var orderIdList = _unitOfWork.GetRepositoryInstance<Tbl_Order>().GetAllRecords();

            foreach (var o in orderIdList)
            {
                orderVM = GetOrder(o.Id);

                if (orderVM != null)
                    orderVMList.Add(orderVM);
            }
            return orderVMList;
        }

        public void RemoveOrderedCart(List<CartItemVM> cart)
        {
            foreach (var item in cart.ToList())
            {
                cart.Remove(item);
            }
        }     
    }
}