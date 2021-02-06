using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Store.Core.Book;

namespace Shop.Store.Infrastructure.Db.Config
{
    public class AuthorConfig : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.ToTable("Author");
            builder.HasKey(x => x.AuthorId);
            builder.OwnsOne(x => x.FullName, c =>
            {
                c.Property(x => x.Name).HasColumnName("Name");
                c.Property(x => x.SureName).HasColumnName("SureName");
            });
            builder.HasMany(x => x.Books)
                .WithOne(z => z.Author);
        }
    }
}
