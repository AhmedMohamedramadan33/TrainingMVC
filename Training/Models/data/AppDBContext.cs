using Microsoft.EntityFrameworkCore;

namespace Training.Models.data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
    }
}
