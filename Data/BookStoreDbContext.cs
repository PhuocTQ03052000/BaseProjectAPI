using BaseProjectAPI.Data.Config;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BaseProjectAPI.Data
{
    public class BookStoreDbContext: IdentityDbContext<ApplicationUser>
    {
        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options) : base(options) { }

        #region DbSet
        public DbSet<Book>? Books { get; set; }
        #endregion DbSet

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserLogin<string>>().HasNoKey();
												modelBuilder.Entity<IdentityUserRole<string>>().HasNoKey();
												modelBuilder.Entity<IdentityUserToken<string>>().HasNoKey();
												modelBuilder.ApplyConfiguration(new BookConfig());
        }
    }
}