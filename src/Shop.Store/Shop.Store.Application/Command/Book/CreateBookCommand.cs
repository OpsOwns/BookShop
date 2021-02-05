using CSharpFunctionalExtensions;
using Shop.Shared.ResultResponse;
using Shop.Store.Core.Book;
using Shop.Store.Core.BookContent;
using Shop.Store.Core.Repository;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Store.Application.Command.Book
{
    public record BookContent(string FileTitle, byte[] File);
    public record CreateBookCommand(string Name, string SureName, string Title,
        int Year, int IsbnType, string IsbnCode, int CategoryBook, string CategoryName, BookContent BookContent) : ICommand<string>;
    public class CreateBookCommandHandler : ICommandHandler<CreateBookCommand, string>
    {
        private readonly IBookRepository _bookRepository;
        public CreateBookCommandHandler(IBookRepository bookRepository) => _bookRepository = bookRepository;
        public async Task<Result<string>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var author = Author.Create(request.Name, request.SureName);
            var category = BookCategory.Create((CategoryBook)request.CategoryBook, request.CategoryName);
            var description = BookDescription.Create(request.Title, request.Year);
            var isbn = Isbn.Create((TypeIsbn)request.IsbnType, request.IsbnCode);
            Result<FileContent> fileContent = null;
            if (request.BookContent is not null)
                fileContent = FileContent.Create(request.BookContent.File, request.BookContent.FileTitle);
            return await Result.Combine(author, category, description, isbn, fileContent)
                .Ensure(async () => !await _bookRepository.IsBookExists(y => y.BookDescription.Title == description.Value.Title, cancellationToken: cancellationToken), "Book already exists")
                .Tap(async () => await _bookRepository.AddBook(new BookInfo(author.Value, category.Value, description.Value, isbn.Value, new Content(fileContent.Value)), cancellationToken))
                .Tap(async () => await _bookRepository.Save(cancellationToken))
                .Finally(x => x.IsFailure ? x.Error : Result.Success("Book was added"));
        }
    }
}
