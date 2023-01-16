using System.ComponentModel.DataAnnotations;

namespace MVC_eCommerce.Models.Order
{
    public class UserVM
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Enter Your Name")]
        [Display(Name = "Castomer Name")]
        [StringLength(49, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 2)]
        public string CastomerName { get; set; }

        [Required(ErrorMessage = "Enter Your Email")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        public string IpAddress { get; set; }

        [Required(ErrorMessage = "Enter Your Address")]
        [StringLength(100, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 7)]
        public string Address { get; set; }

        public OrderVM Order { get; set; }
    }
}