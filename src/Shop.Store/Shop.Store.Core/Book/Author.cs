using System.Collections.Generic;
using CSharpFunctionalExtensions;
using Shop.Shared.Shared;

namespace Shop.Store.Core.Book
{
    public class Author : ValueObject
    {
        private Author(string name, string sureName)
        {
            Name = name;
            SureName = sureName;
        }

        public string Name { get; }
        public string SureName { get; }

        public static Result<Author> Create(string name, string sureName)
        {
            return name.IsEmpty() ? Result.Failure<Author>($"invalid {nameof(name)}") :
                sureName.IsEmpty() ? Result.Failure<Author>($"invalid {nameof(sureName)}") :
                Result.Success(new Author(name, sureName));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return SureName;
        }
    }
}