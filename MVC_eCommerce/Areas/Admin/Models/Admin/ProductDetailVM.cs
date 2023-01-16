using System;
using System.ComponentModel.DataAnnotations;

namespace MVC_eCommerce.Areas.Admin.Models.Admin
{
    public class ProductDetailVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Product Name Required")]
        [StringLength(100, ErrorMessage = "Minimum 3 and minimum 5 and maximum 100 charaters are allwed", MinimumLength = 3)]
        public string ProductName { get; set; }
        [Required]
        [Range(1, 50)]
        public Nullable<int> CategoryId { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        [Required(ErrorMessage = "Description is Required")]
        public string Description { get; set; }
        public string ProductImage { get; set; }
        public Nullable<bool> IsFeatured { get; set; }
        [Required]
        [Range(typeof(int), "1", "500", ErrorMessage = "Invalid Quantity")]
        public Nullable<int> Quantity { get; set; }
        [Required]
        [Range(typeof(decimal), "1", "5000", ErrorMessage = "Invalid Price")]
        public Nullable<decimal> Price { get; set; }
        public CategoryDetailVM CategoryVM { get; set; }
    }
}