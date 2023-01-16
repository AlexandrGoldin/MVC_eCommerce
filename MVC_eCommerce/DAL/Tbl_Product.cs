using System;
using System.ComponentModel.DataAnnotations;

namespace MVC_eCommerce.DAL
{
    public class Tbl_Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        [StringLength(49, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 5)]
        public string ProductName { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        [Required]
        [StringLength(1100, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 3)]
        public string Description { get; set; }
        public string ProductImage { get; set; }
        public Nullable<bool> IsFeatured { get; set; }
        public Nullable<int> Quantity { get; set; }
        public decimal? Price { get; set; }
        public Tbl_Category Category { get; set; }
    }
}