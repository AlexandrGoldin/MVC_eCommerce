using System.ComponentModel.DataAnnotations;

namespace MVC_eCommerce.Models.Account
{
    public class EmploeeProfileVM
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^[0-9]{3}-[0-9]{3}-[0-9]{4}$", ErrorMessage = "Incorrect phone number")]
        public string PhoneNumber { get; set; }
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }
    }
}