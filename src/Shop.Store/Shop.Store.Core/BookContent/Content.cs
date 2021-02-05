using Shop.Shared.Domain;
using System;

namespace Shop.Store.Core.BookContent
{
    public class Content : Entity
    {
        public ContentId ImageId { get; private set; }
        public FileContent FileContent { get; private set; }

        private Content()
        {
            ImageId = new ContentId(Guid.NewGuid());
        }
        public Content(FileContent fileContent) : this()
        {
            FileContent = fileContent;
        }


    }
}
