using BaseProjectAPI.Data.Config;
using Microsoft.EntityFrameworkCore;

namespace BaseProjectAPI.Data
{
    public class BookStoreDbContext: DbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options) { }

        #region DbSet
        public DbSet<Book>? Books { get; set; }
        #endregion DbSet

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookConfig());
        }
    }
}