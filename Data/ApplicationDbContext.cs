using Microsoft.EntityFrameworkCore;
using e_commerce.Models.User;
using e_commerce.Models.Product;

namespace e_commerce.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Aqui você pode adicionar suas entidades
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
