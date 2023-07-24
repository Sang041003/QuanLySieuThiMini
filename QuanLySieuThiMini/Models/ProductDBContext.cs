using Microsoft.EntityFrameworkCore;

namespace QuanLySieuThiMini.Models
{
    public class ProductDBContext:DbContext
    {
        public ProductDBContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<BillDetail> BillDetails { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Guest> Guests { get; set; }

    }
}
