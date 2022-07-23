using Microsoft.EntityFrameworkCore;

namespace BookStore.Models
{
    public class ProductContext : DbContext
    {
        public DbSet<ProductModel> Products { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=StoreDb;Username=postgres;Password=3742;Maximum Pool Size=100");
    }
}
