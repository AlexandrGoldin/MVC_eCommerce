using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC_eCommerce.Models.Admin
{
    public class CategoryVM
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Category Name")]
        [StringLength(20, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 5)]
        public string CategoryName { get; set; }
        [Display(Name = "Is Delete")]
        public Nullable<bool> IsDelete { get; set; }
        public ICollection<ProductVM> Products { get; set; }
    }
}