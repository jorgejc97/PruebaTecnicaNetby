using DynamicFormApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DynamicFormApi.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Form> Forms { get; set; } = null!;
        public DbSet<FormField> FormFields { get; set; } = null!;
        public DbSet<FormResponse> FormResponses { get; set; } = null!;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Form>()
                .HasMany(f => f.Fields)
                .WithOne(ff => ff.Form)
                .HasForeignKey(ff => ff.FormId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FormResponse>()
                .HasOne(fr => fr.Form)
                .WithMany()
                .HasForeignKey(fr => fr.FormId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
