using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Store.Core.Book;
using Shop.Store.Core.BookContent;

namespace Shop.Store.Infrastructure.Db.Config
{
    public class BookContentConfig : IEntityTypeConfiguration<Content>
    {
        public void Configure(EntityTypeBuilder<Content> builder)
        {
            builder.ToTable("BookContent");
            builder.HasKey(x => x.ImageId);
            builder.OwnsOne(x => x.FileContent, p =>
            {
                p.Property(x => x.File).HasColumnName("ImageFile");
                p.Property(x => x.FileTitle).HasColumnName("ImageTitle");
            });
        }
    }
}
