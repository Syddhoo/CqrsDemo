using CqrsDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace CqrsDemo.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        public DbSet<Employee> Employees => Set<Employee>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employees");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.Salary)
                    .HasPrecision(18, 2)
                    .IsRequired();
            });
        }
    }
}
