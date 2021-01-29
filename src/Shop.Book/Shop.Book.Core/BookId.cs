using Shop.Shared.Domain;
using System;

namespace Shop.Book.Core
{
    public record BookId : BaseId<Guid>
    {
        public BookId(Guid value) : base(value)
        {
        }
    }
}
