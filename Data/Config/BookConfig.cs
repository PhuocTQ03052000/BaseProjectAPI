using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BaseProjectAPI.Data.Config
{
    public class BookConfig : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(n => n.Title).IsRequired().HasMaxLength(100);
            builder.Property(n => n.Description).IsRequired(false).HasMaxLength(250);
            builder.Property(n => n.Price).IsRequired().HasMaxLength(100);
            builder.Property(n => n.Quantity).IsRequired().HasMaxLength(100);

            builder.HasData(new List<Book>()
            {
                new Book()
                {
                    Id = 1,
                    Title = "Conan",
                    Description = "Thám tử Conan phá án",
                    Price = 11.111,
                    Quantity = 90
                },
                new Book()
                {
                    Id = 2,
                    Title = "Bảy viên ngọc rồng",
                    Description = "Cuộc phiêu lưu của Goku",
                    Price = 11.1167,
                    Quantity = 90
                },
                new Book()
                {
                    Id = 3,
                    Title = "Shin Cậu Bé Bút Chì",
                    Description = "Chú bé đáng yêu",
                    Price = 10.000,
                    Quantity = 90
                }
            });
        }
    }
}
