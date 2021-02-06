using Shop.Shared.Domain;
using System;
using System.Collections.Generic;

namespace Shop.Store.Core.Book
{
    public class Author : Entity
    {
        public AuthorId AuthorId { get; private set; }
        public FullName FullName { get; private set; }
        private Author() => AuthorId = new AuthorId(Guid.NewGuid());
        private readonly List<Books> _books = new();
        public IReadOnlyList<Books> Books => _books;
        public Author(FullName fullName) : this()
        {
            FullName = fullName;
        }
    }
}