using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace myproject.Data
{
    public class ProductContext : IdentityDbContext<ApplicationUser>
    {
        public ProductContext(DbContextOptions<ProductContext> options)
           : base(options)
        {

        }
        //Add DbSet for Product model

        public DbSet<Product> Products { get; set; }

    }
}