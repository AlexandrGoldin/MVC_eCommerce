using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC_eCommerce.DAL
{
    public class Tbl_Status
    {
        [Key]
        public int Id { get; set; }
        public string StatusName { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public ICollection<Tbl_Order> Order { get; set; }
    }
}