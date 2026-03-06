using Microsoft.EntityFrameworkCore;
using Models.Entity;

namespace Data.Context
{
    public class ProductDbContext: DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options):base(options)
        {
        }
        public DbSet<Product> Products { get; set; }

    }
}
