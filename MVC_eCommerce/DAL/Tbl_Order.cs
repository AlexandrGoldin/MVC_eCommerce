using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MVC_eCommerce.DAL
{
    public class Tbl_Order
    {
        [Key]
        public int Id { get; set; }
        public int StatusId { get; set; }

        public Nullable<System.DateTime> OrderDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public decimal? TotalPrice { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public  Tbl_User User { get; set; }
        public Tbl_Status Status { get; set; }
        public List<Tbl_Cart> Tbl_Cart { get; set; }





    }
}