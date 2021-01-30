using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Shop.Store.Core.Book;
using Shop.Store.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Shop.Store.Infrastructure.Db.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookContext _bookContext;

        public BookRepository(BookContext bookContext)
        {
            _bookContext = bookContext;
        }

        public async Task AddBook(BookInfo book)
        {
            await _bookContext.Books.AddAsync(book);
        }

        public async Task<Maybe<BookInfo>> FindBook(Expression<Func<BookInfo, bool>> expression) => await _bookContext.Books.FirstAsync(expression);

        public Task<IEnumerable<BookInfo>> GetBooks()
        {
            throw new NotImplementedException();
        }
    }
}
