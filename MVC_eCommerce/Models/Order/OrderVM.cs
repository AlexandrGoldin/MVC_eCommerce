using MVC_eCommerce.Areas.Admin.Models.Admin;
using MVC_eCommerce.Models.Home;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MVC_eCommerce.Models.Order
{
    public class OrderVM
    {
        [Key]
        public int Id { get; set; }
        public int StatusId { get; set; }
        public int UserId { get; set; }

        [Display(Name = "Order Date")]
        public Nullable<System.DateTime> OrderDate { get; set; }

        [Display(Name = "Modified Date")]
        public Nullable<System.DateTime> ModifiedDate { get; set; }

        [Display(Name = "Total Price")]
        public decimal? TotalPrice { get; set; }

        [DefaultValue(true)]
        [Display(Name = "Is Active")]
        public Nullable<bool> IsActive { get; set; }

        [Display(Name = "Current Status")]
        public StatusVM Status { get; set; }
        
        public UserVM User { get; set; }

        [Display(Name = "Cart(qty)")]
        public List<CartItemVM> Cart { get; set; }
        public IEnumerable<SelectListItem> StatusSelectList { get; set; }
        public ProductDetailVM ProducVM { get; set; }


    }
}