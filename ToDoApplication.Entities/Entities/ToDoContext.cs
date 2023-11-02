using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TodoApplication.Authentication;

namespace TodoApplication.Entities
{
    public class ToDoContext : IdentityDbContext<User>
    {
        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options)
        {

        }

        public DbSet<ToDoItem> ToDoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ToDoItem>(entity =>
            {
                entity.Property(e => e.ItemName)
                .IsRequired()
                .HasMaxLength(100);

                entity.Property(e => e.ItemDescription)
                .IsRequired()
                .HasMaxLength(100);

                entity.Property(e => e.ItemStatus)
                .IsRequired()
                .HasMaxLength(1);
            });

            base.OnModelCreating(builder);
        }
    }
}
