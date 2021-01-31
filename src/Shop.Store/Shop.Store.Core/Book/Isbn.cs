using System.Collections.Generic;
using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;
using Shop.Shared.Shared;

namespace Shop.Store.Core.Book
{
    public enum TypeIsbn
    {
        Isbn10 = 1,
        Isbn13 = 2,
    }

    public class Isbn : ValueObject
    {
        private Isbn(string isbn)
        {
            IsbnCode = isbn;
        }
        protected Isbn() { }
        public string IsbnCode { get; }

        public static Result<Isbn> Create(TypeIsbn typeIsbn, string isbnCode)
        {
            return !CheckValidIsbn(isbnCode)
                ? Result.Failure<Isbn>($"Missing ISBN type or {nameof(isbnCode)} is empty")
                : typeIsbn == TypeIsbn.Isbn13
                    ? !ValidIsbn13(isbnCode) ? Result.Failure<Isbn>("Invalid ISBN code") :
                    Result.Success(new Isbn(isbnCode))
                    : !ValidIsbn10(isbnCode)
                        ? Result.Failure<Isbn>("Invalid ISBN code")
                        : Result.Success(new Isbn(isbnCode));
        }

        private static bool ValidIsbn10(string value)
        {
            return Regex.IsMatch(value,
                @"^(?:ISBN(?:-10)?:?\ *((?=\d{1,5}([ -]?)\d{1,7}\2?\d{1,6}\2?\d)(?:\d\2*){9}[\dX]))$");
        }

        private static bool ValidIsbn13(string value)
        {
            return Regex.IsMatch(value,
                @"^(?:ISBN(?:-13)?:?\ *(97(?:8|9)([ -]?)(?=\d{1,5}\2?\d{1,7}\2?\d{1,6}\2?\d)(?:\d\2*){9}\d))$");
        }

        private static bool CheckValidIsbn(string value)
        {
            return !value.IsEmpty() && value.ToUpper().Contains("ISBN");
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return IsbnCode;
        }
    }
}