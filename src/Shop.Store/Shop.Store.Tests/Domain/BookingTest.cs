using Bogus;
using Shop.Store.Core;
using Shop.Store.Core.Book;
using System;
using Xunit;

namespace Shop.Store.Tests.Domain
{
    public class BookingTest
    {
        [Fact]
        public void CreateBookFailTest()
        {
            var result = Assert.Throws<BookException>(() => new Books(null, null, null, null));
            Assert.Equal("author is null", result.Code);
        }

        [Fact]
        public void CreateBookSuccessTest()
        {
            var faker = new Faker();
            var author = new Author(FullName.Create(faker.Name.FirstName(), faker.Name.LastName()).Value);
            var bookCategory = BookCategory.Create(CategoryBook.Business, faker.Locale);
            var bookDescription = BookDescription.Create(faker.Locale, faker.Date.Random.Number(1, DateTime.Now.Year));
            var bookIsbn = Isbn.Create(TypeIsbn.Isbn10, "ISBN 1-58182-008-9");
            var book = new Books(author, bookCategory.Value, bookDescription.Value, bookIsbn.Value);
            Assert.NotNull(book);
        }

        [Fact]
        public void EditBookDetailsTest()
        {
            var faker = new Faker();
            var author = new Author(FullName.Create(faker.Name.FirstName(), faker.Name.LastName()).Value);
            var bookCategory = BookCategory.Create(CategoryBook.Business, faker.Lorem.Text()).Value;
            var bookDescription = BookDescription
                .Create(faker.Lorem.Text(), faker.Date.Random.Number(1, DateTime.Now.Year)).Value;
            var bookIsbn = Isbn.Create(TypeIsbn.Isbn10, "ISBN 1-58182-008-9").Value;
            var book = new Books(author, bookCategory, bookDescription, bookIsbn);
            var oldId = book.BookId.Value;
            Assert.NotNull(book);
            var newAuthor = new Author(FullName.Create(faker.Name.FirstName(), faker.Name.LastName()).Value);
            var newBookCategory = BookCategory.Create(CategoryBook.Business, faker.Lorem.Text()).Value;
            var newBookDescription = BookDescription
                .Create(faker.Lorem.Text(), faker.Date.Random.Number(1, DateTime.Now.Year)).Value;
            var newBookIsbn = Isbn.Create(TypeIsbn.Isbn10, "ISBN 1-55182-008-9").Value;
            book.EditBookDetails(newAuthor, newBookCategory, newBookDescription, newBookIsbn);
            Assert.NotEqual(book.Author.FullName.SureName, author.FullName.Name);
            Assert.NotEqual(book.Author.FullName.SureName, author.FullName.Name);
            Assert.NotEqual(book.BookCategory.CategoryName, bookCategory.CategoryName);
            Assert.NotEqual(book.BookDescription.Title, bookDescription.Title);
            Assert.NotEqual(book.BookDescription.Year, bookDescription.Year);
            Assert.NotEqual(book.Isbn.IsbnCode, bookIsbn.IsbnCode);
            Assert.Equal(oldId, book.BookId.Value);
        }
    }
}