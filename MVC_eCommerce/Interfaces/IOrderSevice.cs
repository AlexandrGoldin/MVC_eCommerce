using MVC_eCommerce.Models.Home;
using MVC_eCommerce.Models.Order;
using System.Collections.Generic;

namespace MVC_eCommerce.Interfaces
{
    public interface IOrderSevice
    {
        IEnumerable<OrderVM> GetOrderList();
        OrderVM GetOrder(int id);
        void RemoveOrderedCart(List<CartItemVM> cart);
    }
}
