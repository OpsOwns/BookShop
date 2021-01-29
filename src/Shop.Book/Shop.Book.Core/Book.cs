using Shop.Shared.Domain;

namespace Shop.Book.Core
{
    public class Book : Entity
    {
        public BookId BookId { get; private set; }
    }
}
