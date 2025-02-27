using DynamicFormApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DynamicFormApi.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Form> Forms { get; set; }
        public DbSet<FormField> FormFields { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Form>()
                .HasMany(f => f.Fields)
                .WithOne(ff => ff.Form)
                .HasForeignKey(ff => ff.FormId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
