using CSharpFunctionalExtensions;
using Shop.Shared.Domain;
using Shop.Store.Core.Book;
using Shop.Store.Core.BookContent;
using Shop.Store.Core.Price;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Store.Core.Repository
{
    public interface IBookRepository : IRepository
    {
        Task<bool> IsBookExists(Expression<Func<Books, bool>> expression,
           CancellationToken cancellationToken = default);
        Task<IEnumerable<Books>> GetBooks(CancellationToken cancellationToken = default);
        Task AddBook(Books bookInfo, CancellationToken cancellationToken = default);
        Task<Maybe<Books>> FindBook(BookId requestBookId);
        Task<Maybe<Author>> FindAuthor(Author author);
        Task<Maybe<BookCosts>> FindBookCosts(BookId bookId);
        Task AddContent(Content fileContent, CancellationToken cancellationToken = default);
        Task AddBookPrice(BookCosts bookCosts, CancellationToken cancellationToken = default);
        void ModifyCosts(BookCosts bookCosts);
    }
}
