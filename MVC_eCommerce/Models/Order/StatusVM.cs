using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC_eCommerce.Models.Order
{
    public class StatusVM
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Status Name")]     
        public string StatusName { get; set; }

        [Display(Name = "Is Delete")]
        public Nullable<bool> IsDelete { get; set; }

        public ICollection<OrderVM> Order { get; set; }

    }
}