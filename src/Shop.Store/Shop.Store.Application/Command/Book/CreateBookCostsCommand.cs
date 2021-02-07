using CSharpFunctionalExtensions;
using Shop.Shared.ResultResponse;
using Shop.Store.Core.Book;
using Shop.Store.Core.Price;
using Shop.Store.Core.Repository;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shop.Store.Application.Command.Book
{
    public record CreateBookCostsCommand(Guid BookId, decimal Amount, string Currency, int Quantity) : ICommandResultOf<string>;
    public class CreateBookCostsCommandHandler : ICommandHandler<CreateBookCostsCommand, string>
    {
        private readonly IBookRepository _bookRepository;
        public CreateBookCostsCommandHandler(IBookRepository bookRepository) => _bookRepository = bookRepository;
        public async Task<Result<string>> Handle(CreateBookCostsCommand request, CancellationToken cancellationToken)
        {
            var money = Money.Create(request.Amount, request.Currency);
            var book = await _bookRepository.FindBook(new BookId(request.BookId));
            var bookCosts = Maybe<BookCosts>.None;
            string msg;
            await book.ToResult(msg ="Book not exists")
                .Tap(async () => bookCosts = await _bookRepository.FindBookCosts(book.Value.BookId));
            return await money.Ensure(_ => book.HasValue, msg)
                .TapIf(bookCosts.HasNoValue, async () =>
                   {
                       await _bookRepository.AddBookPrice(new BookCosts(money.Value, request.Quantity, book.Value),
                           cancellationToken);
                       msg = "book costs was added";
                   }).TapIf(bookCosts.HasValue, () =>
                   {
                       bookCosts.Value.ChangeCosts(money.Value, request.Quantity, book.Value);
                       _bookRepository.ModifyCosts(bookCosts.Value);
                       msg = "book cost was modified";
                   }).Tap(async () => await _bookRepository.Save(cancellationToken)).Finally(x => x.IsFailure ? x.Error : msg);
        }
    }
}
