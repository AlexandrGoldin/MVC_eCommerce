using MVC_eCommerce.Models.Admin;
using MVC_eCommerce.Models.Order;
using System;
using System.ComponentModel.DataAnnotations;

namespace MVC_eCommerce.Models.Home
{
    public class CartItemVM
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int ProductId { get; set; }
        public Nullable<int> Count { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public OrderVM Order { get; set; }
        public decimal? TotalPrice { get; set; }
        public ProductVM Product { get; set; }



    }
} 