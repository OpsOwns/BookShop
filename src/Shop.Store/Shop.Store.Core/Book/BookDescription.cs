using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;
using Shop.Shared.Shared;

namespace Shop.Store.Core.Book
{
    public class BookDescription : ValueObject
    {
        private BookDescription(string title, int year)
        {
            Title = title;
            Year = year;
        }

        public string Title { get; }
        public int Year { get; }

        public static Result<BookDescription> Create(string title, int year)
        {
            return title.IsEmpty()
                ? Result.Failure<BookDescription>($"invalid {nameof(title)}")
                : year < 0 && year > DateTime.Now.Year
                    ? Result.Failure<BookDescription>($"invalid {nameof(year)}")
                    : Result.Success(new BookDescription(title, year));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Year;
            yield return Title;
        }
    }
}