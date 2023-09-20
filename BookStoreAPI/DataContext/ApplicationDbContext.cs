using BookStoreAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStoreAPI.DataContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }


        public DbSet<BookStore> Books { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Order>()
            .HasOne(o => o.BookStore)
            .WithMany()
            .HasForeignKey(o => o.BookId);
        }

    }
}
