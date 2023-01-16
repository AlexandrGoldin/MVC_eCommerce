using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MVC_eCommerce.Models.Admin
{
    public class ProductVM
    {
       [Key]
        public int Id { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        [Display(Name = "Product Name")]
        [StringLength(49, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 5)]
        public string ProductName { get; set; }
        [Display(Name = "Is Delete")]
        public Nullable<bool> IsDelete { get; set; }
        [Display(Name = "Created Date")]
        public Nullable<System.DateTime> CreatedDate { get; set; }
        [Display(Name = "Modified Date")]
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        [Required]
        [StringLength(1100, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 3)]
        public string Description { get; set; }
        [Display(Name = "Product Image")]
        public string ProductImage { get; set; }
        [Display(Name = "Is Featured")]
        public Nullable<bool> IsFeatured { get; set; }
        public Nullable<int> Quantity { get; set; }
        public decimal? Price { get; set; }
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }
    }
}