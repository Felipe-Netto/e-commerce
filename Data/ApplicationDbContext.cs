using Microsoft.EntityFrameworkCore;

namespace APIWithControllers.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Aqui você pode adicionar suas entidades
        public DbSet<User> Users { get; set; }
    }
}
