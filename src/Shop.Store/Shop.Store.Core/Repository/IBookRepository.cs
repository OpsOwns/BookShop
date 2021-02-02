using Shop.Shared.Domain;
using Shop.Store.Core.Book;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Store.Core.Repository
{
    public interface IBookRepository : IRepository
    {
        Task<bool> IsBookExists(Expression<Func<BookInfo, bool>> expression,
           CancellationToken cancellationToken = default);
        Task<IEnumerable<BookInfo>> GetBooks(CancellationToken cancellationToken = default);
        Task AddBook(BookInfo bookInfo, CancellationToken cancellationToken = default);
        Task<BookInfo> FindBook(BookId requestBookId);
    }
}
