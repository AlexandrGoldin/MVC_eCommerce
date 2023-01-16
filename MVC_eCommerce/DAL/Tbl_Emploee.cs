using System.ComponentModel.DataAnnotations;

namespace MVC_eCommerce.DAL
{
    public class Tbl_Emploee
    {
        [Key]
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}