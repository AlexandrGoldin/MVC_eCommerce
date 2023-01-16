using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_eCommerce.DAL
{
    public class Tbl_User
    {
        [Key]
        [ForeignKey("Order")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter Your Name")]
        [StringLength(49, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 2)]
        public string CastomerName { get; set; }

        [Required(ErrorMessage = "Enter Your Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string IpAddress { get; set; }

        [Required(ErrorMessage = "Enter Your Address")]
        [DataType(DataType.EmailAddress)]
        [StringLength(100, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 7)]
        public string Address { get; set; }

        public Tbl_Order Order { get; set; }
    }
}