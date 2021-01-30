using CSharpFunctionalExtensions;
using Shop.Shared.Domain;
using Shop.Store.Core.Book;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Shop.Store.Core.Repositories
{
    public interface IBookRepository : IRepository
    {
        public Task AddBook(BookInfo book);
        public Task<Maybe<BookInfo>> FindBook(Expression<Func<BookInfo, bool>> expression);
        public Task<IEnumerable<BookInfo>> GetBooks();
    }
}