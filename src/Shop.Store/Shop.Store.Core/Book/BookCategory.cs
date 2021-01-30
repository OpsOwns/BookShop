using System.Collections.Generic;
using CSharpFunctionalExtensions;
using Shop.Shared.Shared;

namespace Shop.Store.Core.Book
{
    #region Category type of books

    public enum CategoryBook
    {
        Horror,
        Kids,
        Business,
        Art,
        Biography,
        Cooking,
        History,
        Health,
        It,
        Religion,
        Home,
        Comics,
        Sports,
        Teen,
        Travel,
        Westerns,
        Science,
        Action
    }

    #endregion

    public class BookCategory : ValueObject
    {
        private BookCategory(string categoryName, CategoryBook categoryBook)
        {
            CategoryName = categoryName;
            CategoryBook = categoryBook;
        }

        public CategoryBook CategoryBook { get; }
        public string CategoryName { get; }

        public static Result<BookCategory> Create(CategoryBook categoryBook, string categoryName)
        {
            return categoryName.IsEmpty()
                ? Result.Failure<BookCategory>("invalid book category name")
                : Result.Success(new BookCategory(categoryName, categoryBook));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return CategoryBook;
            yield return CategoryName;
        }
    }
}