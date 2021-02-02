using Microsoft.EntityFrameworkCore;
using Shop.Store.Core.Book;
using Shop.Store.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Store.Infrastructure.Db
{
    public class BookRepository : IBookRepository
    {
        private readonly BookContext _bookContext;

        public BookRepository(BookContext bookContext) => _bookContext = bookContext;
        public async Task<bool> IsBookExists(Expression<Func<BookInfo, bool>> expression, CancellationToken cancellationToken = default) => await _bookContext.Books.AnyAsync(expression, cancellationToken);
        public async Task<IEnumerable<BookInfo>> GetBooks(CancellationToken cancellationToken)
            => await _bookContext.Books.ToListAsync(cancellationToken);
        public async Task AddBook(BookInfo bookInfo, CancellationToken cancellationToken = default)
        {
            await _bookContext.Books.AddAsync(bookInfo, cancellationToken);
            await _bookContext.SaveChangesAsync(cancellationToken);
        }
        public async Task<BookInfo> FindBook(BookId requestBookId) => await _bookContext.Books.FindAsync(requestBookId);
    }
}
