using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Store.Core.Price;

namespace Shop.Store.Infrastructure.Db.Config
{
    public class BookCostsConfig : IEntityTypeConfiguration<BookCosts>
    {
        public void Configure(EntityTypeBuilder<BookCosts> builder)
        {
            builder.HasKey(x => x.BookCostsId);
            builder.OwnsOne(x => x.BookCost, c =>
            {
                c.Property(x => x.Currency).HasColumnName("Currency");
                c.Property(x => x.Amount).HasColumnName("Amount");
            });
            builder.HasOne(x => x.Book);
        }
    }
}
