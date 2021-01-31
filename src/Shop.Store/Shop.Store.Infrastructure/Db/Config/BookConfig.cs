﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Store.Core.Book;
using System;

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
            builder.OwnsOne(x => x.Author, p =>
            {
                p.Property(x => x.Name).HasColumnName("Name");
                p.Property(x => x.SureName).HasColumnName("SureName");
            });
            builder.OwnsOne(x => x.BookCategory, p =>
            {
                p.Property(x => x.CategoryBook).HasConversion(v => v.ToString(),
                    v => (CategoryBook)Enum.Parse(typeof(CategoryBook), v)).HasColumnName("BookCategory");
                p.Property(x => x.CategoryName).HasColumnName("NameCategory");
            });
            builder.OwnsOne(x => x.Isbn, p =>
            {
                p.Property(x => x.IsbnCode).HasColumnName("IsbnCode");
            });
        }
    }
}
