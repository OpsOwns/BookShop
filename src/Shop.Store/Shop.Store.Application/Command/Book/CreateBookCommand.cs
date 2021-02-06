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
        int Year, int IsbnType, string IsbnCode, int CategoryBook, string CategoryName, BookContent BookContent) : ICommandResultOf<string>;
    public class CreateBookCommandHandler : ICommandHandler<CreateBookCommand, string>
    {
        private readonly IBookRepository _bookRepository;
        public CreateBookCommandHandler(IBookRepository bookRepository) => _bookRepository = bookRepository;
        public async Task<Result<string>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var fullName = FullName.Create(request.Name, request.SureName);
            var category = BookCategory.Create((CategoryBook)request.CategoryBook, request.CategoryName);
            var description = BookDescription.Create(request.Title, request.Year);
            var isbn = Isbn.Create((TypeIsbn)request.IsbnType, request.IsbnCode);
            var fileContent = request.BookContent != null ? FileContent.Create(request.BookContent.File, request.BookContent.FileTitle) : Result.Success(FileContent.Default);
            Books book = null!;
            Maybe<Author> author = null!;
            return await Result.Combine(category, description, isbn, fullName, fileContent)
                .Ensure(async () => !await _bookRepository.IsBookExists(y => y.BookDescription.Title == description.Value.Title, cancellationToken), "Book already exists")
                .Tap(async () => author = await _bookRepository.FindAuthor(new Author(fullName.Value)))
                .Tap(() => book = new Books(author.HasNoValue ? new Author(fullName.Value) : author.Value, category.Value, description.Value, isbn.Value))
                .TapIf(fileContent.Value != FileContent.Default, () => book.AddContent(new Content(fileContent.Value)))
                .Tap(async () => await _bookRepository.AddBook(book, cancellationToken))
                .Tap(async () => await _bookRepository.Save(cancellationToken))
                .Finally(x => x.IsFailure ? x.Error : Result.Success("Book was added"));
        }
    }
}
