using Shop.Shared.Domain;
using Shop.Store.Core.BookContent;
using System;

namespace Shop.Store.Core.Book
{
    public class Books : AggregateRoot
    {
        private Books()
        {
            BookId = new BookId(Guid.NewGuid());
        }
        public Books(Author author, BookCategory bookCategory, BookDescription bookDescription, Isbn isbn) : this()
        {
            Author = author ?? throw new BookException($"{nameof(author)} is null");
            BookDescription = bookDescription ?? throw new BookException($"{nameof(bookDescription)} is null");
            BookCategory = bookCategory ?? throw new BookException($"{nameof(bookCategory)} is null");
            Isbn = isbn ?? throw new BookException($"{nameof(isbn)} is null");
        }
        public BookId BookId { get; }
        public Author Author { get; private set; }
        public BookDescription BookDescription { get; private set; }
        public BookCategory BookCategory { get; private set; }
        public Isbn Isbn { get; private set; }
        public Content Content { get; private set; }
        public void AddContent(Content content) => Content = content ?? throw new BookException($"{nameof(content)} is null");
        public Books EditBookDetails(Author author, BookCategory bookCategory, BookDescription bookDescription,
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