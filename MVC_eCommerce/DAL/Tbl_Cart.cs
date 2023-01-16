using System;
using System.ComponentModel.DataAnnotations;

namespace MVC_eCommerce.DAL
{
    public class Tbl_Cart
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<int> Count { get; set; }
        public decimal? TotalPrice { get; set; }
        public Tbl_Order Order { get; set; }
        public Tbl_Product Product { get; set; }
    }
}