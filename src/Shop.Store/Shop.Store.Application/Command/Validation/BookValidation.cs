using FluentValidation;
using Shop.Store.Application.Command.Book;

namespace Shop.Store.Application.Command.Validation
{
    public class BookValidation : AbstractValidator<CreateBookCommand>
    {
        public BookValidation()
        {
            RuleFor(x => x.Title).NotEmpty().NotNull();
            RuleFor(x => x.CategoryBook).GreaterThan(0).NotEmpty().NotNull();
            RuleFor(x => x.CategoryName).NotEmpty().NotNull().MinimumLength(3);
            RuleFor(x => x.IsbnCode).NotEmpty().NotNull();
            RuleFor(x => x.Name).NotEmpty().NotNull().MinimumLength(3);
            RuleFor(x => x.SureName).NotEmpty().NotNull().MinimumLength(3);
            RuleFor(x => x.Year).NotEmpty().NotEmpty().GreaterThan(0);
        }
    }
}
