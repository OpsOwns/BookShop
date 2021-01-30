using CSharpFunctionalExtensions;
using Shop.Shared.ResultResponse;
using Shop.Shared.SeedWork;
using Shop.Store.Core.Book;
using Shop.Store.Core.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Store.Application.Command.Book
{
    public abstract record CreateBookCommand(string Name, string SureName, string Title,
        int Year, int IsbnType, string IsbnCode, int CategoryBook, string CategoryName) : IRequestResult;
    public class CreateBookCommandHandler : IHandlerResult<CreateBookCommand>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateBookCommandHandler(IUnitOfWork unitOfWork, IBookRepository bookRepository)
        {
            _unitOfWork = unitOfWork;
            _bookRepository = bookRepository;
        }
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
            var dbBook = await _bookRepository.FindBook(x => x.BookDescription.Title == request.Title);
            if (dbBook.HasValue)
                return Result.Failure("Book already exists");
            await _bookRepository.AddBook(book);
            await _unitOfWork.Commit(cancellationToken);
            return Result.Success("");
        }
    }
}
