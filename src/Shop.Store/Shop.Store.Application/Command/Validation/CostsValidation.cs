using FluentValidation;
using Shop.Store.Application.Command.Book;

namespace Shop.Store.Application.Command.Validation
{
    public class CostsValidation : AbstractValidator<CreateBookCostsCommand>
    {
        public CostsValidation()
        {
            RuleFor(x => x.Currency).NotNull().NotEmpty();
            RuleFor(x => x.Amount).GreaterThan(0);
            RuleFor(x => x.Quantity).GreaterThanOrEqualTo(0);
            RuleFor(x => x.BookId).NotNull().NotEmpty();
        }
    }
}
