using Shop.Shared.Domain;
using System;

namespace Shop.Store.Core.Price
{
    public record BookCostsId : BaseId<Guid>
    {
        public BookCostsId(Guid Value) : base(Value)
        {
        }
    }
}
