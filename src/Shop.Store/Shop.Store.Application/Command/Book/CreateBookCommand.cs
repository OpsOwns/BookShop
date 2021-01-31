using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Shop.Shared.ResultResponse;
using Shop.Store.Core.Book;
using Shop.Store.Infrastructure.Db;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Store.Application.Command.Book
{
    public record CreateBookCommand(string Name, string SureName, string Title,
        int Year, int IsbnType, string IsbnCode, int CategoryBook, string CategoryName) : IRequestResult;
    public class CreateBookCommandHandler : IHandlerResult<CreateBookCommand>
    {
        private readonly BookContext _bookContext;
        public CreateBookCommandHandler(BookContext bookContext) => _bookContext = bookContext;
        public async Task<Result> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var author = Author.Create(request.Name, request.SureName);
            var category = BookCategory.Create((CategoryBook)request.CategoryBook, request.CategoryName);
            var description = BookDescription.Create(request.Title, request.Year);
            var isbn = Isbn.Create((TypeIsbn)request.IsbnType, request.IsbnCode);
            var result = Result.Combine(author, category, description, isbn);
            if (result.IsFailure)
                return Result.Failure(result.Error);
            var book = new BookInfo(author.Value, category.Value, description.Value, isbn.Value);
            var dbBook = Maybe<BookInfo>.From(await _bookContext.Books.FirstOrDefaultAsync(x => x.BookDescription.Title == book.BookDescription.Title, cancellationToken: cancellationToken));
            if (dbBook.HasValue)
                return Result.Failure("Book already exists");
            await _bookContext.Books.AddAsync(book, cancellationToken);
            await _bookContext.SaveChangesAsync(cancellationToken);
            return Result.Success("");
        }
    }
}
