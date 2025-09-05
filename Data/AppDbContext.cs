using Microsoft.EntityFrameworkCore;
using TimeToRESTFromTodo.Models;

namespace TimeToRESTFromTodo.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<TaskItem> Tasks => Set<TaskItem>();

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<TaskItem>(b =>
            {
                b.HasKey(t => t.Id);                  
                b.Property(t => t.Title)
                    .IsRequired()
                    .HasMaxLength(100);
                b.Property(t => t.Description)
                    .HasMaxLength(1000);
                b.HasIndex(t => t.IsCompleted);
                b.Property(t => t.CreatedAt)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });


            
        }
    }
}
