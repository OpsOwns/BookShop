using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Shop.Store.Core.Book;
using Shop.Store.Core.BookContent;
using Shop.Store.Core.Price;
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
        public async Task<bool> IsBookExists(Expression<Func<Books, bool>> expression, CancellationToken cancellationToken = default) =>
            await _bookContext.Books.AnyAsync(expression, cancellationToken);
        public async Task<IEnumerable<Books>> GetBooks(CancellationToken cancellationToken)
            => await _bookContext.Books.Include(x => x.Content).ToListAsync(cancellationToken);
        public async Task AddBook(Books bookInfo, CancellationToken cancellationToken = default) => await _bookContext.Books.AddAsync(bookInfo, cancellationToken);
        public async Task<Maybe<Books>> FindBook(BookId requestBookId) => await _bookContext.Books.Include(x => x.Content).FirstOrDefaultAsync(x => x.BookId == requestBookId);
        public async Task<Maybe<Author>> FindAuthor(Author author) => await _bookContext.Authors.SingleOrDefaultAsync(x => x.FullName.Name == author.FullName.Name && x.FullName.SureName == author.FullName.SureName);
        public async Task<Maybe<BookCosts>> FindBookCosts(BookId bookId) =>
            await _bookContext.BookCosts.FirstOrDefaultAsync(x => x.Book.BookId == bookId);
        public async Task AddContent(Content content, CancellationToken cancellationToken = default) =>
            await _bookContext.BookContents.AddAsync(content, cancellationToken);
        public async Task AddBookPrice(BookCosts bookCosts, CancellationToken cancellationToken) =>
         await _bookContext.BookCosts.AddAsync(bookCosts, cancellationToken);
        public void ModifyCosts(BookCosts bookCosts) => _bookContext.BookCosts.Attach(bookCosts);
        public async Task<int> Save(CancellationToken cancellationToken = default) => await _bookContext.SaveChangesAsync(cancellationToken);

    }
}
