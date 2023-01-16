using System.ComponentModel.DataAnnotations;

namespace MVC_eCommerce.Models.Account
{
    public class LoginEmploeeVM
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}