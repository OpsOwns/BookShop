using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Store.Core.Book;

namespace Shop.Store.Infrastructure.Db.Config
{
    public class BookConfig : IEntityTypeConfiguration<BookInfo>
    {
        public void Configure(EntityTypeBuilder<BookInfo> builder)
        {
            builder.ToTable("Books");
            builder.HasKey(x => x.BookId);
            builder.OwnsOne(x => x.BookDescription, p =>
            {
                p.Property(x => x.Title).HasColumnName("Title");
                p.Property(x => x.Year).HasColumnName("Year");
            });
        }
    }
}
