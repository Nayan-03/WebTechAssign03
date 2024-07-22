namespace Nayan_Assignement3.Database
{
    using global::Nayan_Assignment3.Entities;
    using Microsoft.EntityFrameworkCore;
    
    namespace Nayan_Assignment3.Data
    {
        public class DatabaseContext : DbContext
        {
            public DatabaseContext(DbContextOptions<DatabaseContext> options)
                : base(options)
            {
            }

            public DbSet<StoreProduct> StoreProducts { get; set; }
            public DbSet<AppUser> AppUsers { get; set; }
            public DbSet<ProductComment> ProductComments { get; set; }
            public DbSet<ShoppingCart> ShoppingCarts { get; set; }
            public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
            public DbSet<CustomerOrder> CustomerOrders { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                // Fluent API configuration can be added here
            }
        }
    }

}
