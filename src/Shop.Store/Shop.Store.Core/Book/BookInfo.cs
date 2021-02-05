using System;
using Shop.Shared.Domain;
using Shop.Store.Core.BookContent;

namespace Shop.Store.Core.Book
{
    public class BookInfo : Entity
    {
        private BookInfo()
        {
            BookId = new BookId(Guid.NewGuid());
        }
        public BookInfo(Author author, BookCategory bookCategory, BookDescription bookDescription, Isbn isbn, Content content = null) : this()
        {
            Author = author ?? throw new BookException($"{nameof(author)} is null");
            BookDescription = bookDescription ?? throw new BookException($"{nameof(bookDescription)} is null");
            BookCategory = bookCategory ?? throw new BookException($"{nameof(bookCategory)} is null");
            Isbn = isbn ?? throw new BookException($"{nameof(isbn)} is null");
            Content = content;
        }
        public BookId BookId { get; }
        public Author Author { get; private set; }
        public BookDescription BookDescription { get; private set; }
        public BookCategory BookCategory { get; private set; }
        public Isbn Isbn { get; private set; }
        public Content Content { get; private set; }
        public void AddContent(Content content) => Content = content ?? throw new BookException($"{nameof(content)} is null");
        public BookInfo EditBookDetails(Author author, BookCategory bookCategory, BookDescription bookDescription,
            Isbn isbn)
        {
            Author = author ?? throw new BookException($"{nameof(author)} is null");
            BookDescription = bookDescription ?? throw new BookException($"{nameof(bookDescription)} is null");
            BookCategory = bookCategory ?? throw new BookException($"{nameof(bookCategory)} is null");
            Isbn = isbn ?? throw new BookException($"{nameof(isbn)} is null");
            return this;
        }
    }
}