using BaseProjectAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BaseProjectAPI.Data
{
    public class BookStoreDbContext: DbContext
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options) { }

        #region DbSet
        public DbSet<Book>? Books { get; set; }
        #endregion DbSet
    }
}