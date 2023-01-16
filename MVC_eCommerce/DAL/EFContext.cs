using System.Data.Entity;

namespace MVC_eCommerce.DAL
{
    public class EFContext : DbContext
    {
        public DbSet<Tbl_Cart> Tbl_Cart { get; set; }
        public DbSet<Tbl_Category> Tbl_Category { get; set; }
        public DbSet<Tbl_Product> Tbl_Product { get; set; }
        public DbSet<Tbl_Order> Tbl_Order { get; set; }
        public DbSet<Tbl_User> Tbl_User { get; set; }
        public DbSet<Tbl_Status> Tbl_Status { get; set; }
        public DbSet<Tbl_Emploee> Tbl_Emploee { get; set; }
        public DbSet<Tbl_Role> Tbl_Role { get; set; }
        public DbSet<Tbl_EmploeeRole> Tbl_EmploeeRole { get; set; }
    }
}
