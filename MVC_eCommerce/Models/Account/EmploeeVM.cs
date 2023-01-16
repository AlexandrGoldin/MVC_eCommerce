using System.ComponentModel.DataAnnotations;

namespace MVC_eCommerce.Models.Account
{
    public class EmploeeVM
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
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}