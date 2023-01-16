using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_eCommerce.DAL
{
    public class Tbl_EmploeeRole
    {
        [Key, Column(Order=0)]
        public int EmploeeId { get; set; }
        [Key, Column(Order = 1)]
        public int RoleId { get; set; }

        [ForeignKey("EmploeeId")]
        public virtual Tbl_Emploee Tbl_Emploee { get; set; }

        [ForeignKey("RoleId")]
        public virtual Tbl_Role Tbl_Role { get; set; }

    }
}