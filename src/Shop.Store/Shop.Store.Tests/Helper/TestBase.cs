using Bogus;
using Shop.Store.Core.Book;
using System;

namespace Shop.Store.Tests.Helper
{
    public abstract class TestBase
    {
        protected Books DefaultBookInfo { get; set; }
        protected TestBase()
        {
            var faker = new Faker();
            var author = new Author(FullName.Create(faker.Name.FirstName(), faker.Name.LastName()).Value);
            var bookCategory = BookCategory.Create(CategoryBook.Business, faker.Locale);
            var bookDescription = BookDescription.Create(faker.Locale, faker.Date.Random.Number(1, DateTime.Now.Year));
            var bookIsbn = Isbn.Create(TypeIsbn.Isbn10, "ISBN 1-58182-008-9");
            DefaultBookInfo = new Books(author, bookCategory.Value, bookDescription.Value, bookIsbn.Value);
        }
    }
}
