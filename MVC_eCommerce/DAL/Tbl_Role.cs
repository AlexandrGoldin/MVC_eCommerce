using System.ComponentModel.DataAnnotations;

namespace MVC_eCommerce.DAL
{
    public class Tbl_Role
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}