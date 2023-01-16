using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC_eCommerce.Areas.Admin.Models.Admin
{
    public class CategoryDetailVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Category Name Required")]
        [StringLength(100, ErrorMessage = "Minimum 3 and minimum 5 and maximum 100 charaters are allwed", MinimumLength = 3)]
        public string CategoryName { get; set; }
        public ICollection<ProductDetailVM> Products { get; set; }

    }
}