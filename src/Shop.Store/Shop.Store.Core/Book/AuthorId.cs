using Shop.Shared.Domain;
using System;

namespace Shop.Store.Core.Book
{
    public record AuthorId : BaseId<Guid>
    {
        public AuthorId(Guid Value) : base(Value)
        {
        }
    }
}
