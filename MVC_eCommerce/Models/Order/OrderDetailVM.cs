using System.Collections.Generic;

namespace MVC_eCommerce.Models.Order
{
    public class OrderDetailVM
    {
        public OrderVM Order { get; set; }
        public List<string> ProductImgList { get; set; }
    }
}