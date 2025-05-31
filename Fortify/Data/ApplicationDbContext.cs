using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Fortify.Data
{
    // Dědíš od IdentityDbContext pro plnou podporu Identity uživatelů a rolí
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Sem přidáš vlastní entity – příklad:
        // public DbSet<YourEntity> YourEntities { get; set; }
        // public DbSet<AnotherEntity> AnotherEntities { get; set; }
    }
}
