using System;
using Shop.Shared.Domain;

namespace Shop.Store.Core.Book
{
    public record BookId : BaseId<Guid>
    {
        public BookId(Guid value) : base(value)
        {
        }
    }
}