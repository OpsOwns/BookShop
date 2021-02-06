using CSharpFunctionalExtensions;
using Shop.Shared.Shared;
using System.Collections.Generic;

namespace Shop.Store.Core.Book
{
    public class FullName : ValueObject
    {
        private FullName(string name, string sureName)
        {
            Name = name;
            SureName = sureName;
        }

        public string Name { get; }
        public string SureName { get; }

        public static Result<FullName> Create(string name, string sureName)
        {
            return name.IsEmpty() ? Result.Failure<FullName>($"invalid {nameof(name)}") :
                sureName.IsEmpty() ? Result.Failure<FullName>($"invalid {nameof(sureName)}") :
                Result.Success(new FullName(name, sureName));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return SureName;
        }
    }
}
