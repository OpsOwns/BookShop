using Shop.Shared.Domain;
using Shop.Store.Core.Book;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Shop.Store.Core.BookContent;

namespace Shop.Store.Core.Repository
{
    public interface IBookRepository : IRepository
    {
        Task<bool> IsBookExists(Expression<Func<Books, bool>> expression,
           CancellationToken cancellationToken = default);
        Task<IEnumerable<Books>> GetBooks(CancellationToken cancellationToken = default);
        Task AddBook(Books bookInfo, CancellationToken cancellationToken = default);
        Task<Books> FindBook(BookId requestBookId);
        Task<Maybe<Author>> FindAuthor(Author author);
        Task AddContent(Content fileContent, CancellationToken cancellationToken = default);
    }
}
