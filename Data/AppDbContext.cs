using Microsoft.EntityFrameworkCore;
using WebApplication10.Entities;

namespace WebApplication10.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
                        : base (options)
        {
        }
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(x => x.Email).IsUnique();
                entity.Property(x => x.Email).IsRequired();
                entity.Property(x => x.Name).IsRequired();
                entity.Property(x => x.Surname).IsRequired();
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
