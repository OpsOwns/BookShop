using Shop.Shared.Domain;
using System;

namespace Shop.Store.Core.BookContent
{
    public record ContentId : BaseId<Guid>
    {
        public ContentId(Guid value) : base(value)
        {
        }
    }
}
