using Shop.Book.Tests.Helper;
using Shop.Store.Core.Book;
using Xunit;

namespace Shop.Book.Tests.Domain.Book
{
    public class IsbnTest
    {
        [Fact]
        public void InvalidIsbnCodeTest()
        {
            var code = Isbn.Create(TypeIsbn.Isbn10, string.Empty);
            var expectedResult = "Missing ISBN type or isbnCode is empty";
            Assert.True(code.IsFailure);
            Assert.Equal(expectedResult, code.Error);
        }

        [Theory]
        [MemberData(nameof(DataGenerator.GetInvalidIsbn10Code), MemberType = typeof(DataGenerator))]
        public void InvalidIsbnCode10Test(string value)
        {
            var code = Isbn.Create(TypeIsbn.Isbn10, value);
            Assert.True(code.IsFailure);
            Assert.Equal("Invalid ISBN code", code.Error);
        }

        [Theory]
        [MemberData(nameof(DataGenerator.GetInvalidIsbn13Code), MemberType = typeof(DataGenerator))]
        public void InvalidIsbnCode13Test(string value)
        {
            var code = Isbn.Create(TypeIsbn.Isbn13, value);
            Assert.True(code.IsFailure);
            Assert.Equal("Invalid ISBN code", code.Error);
        }

        [Theory]
        [MemberData(nameof(DataGenerator.GetIsbn10Code), MemberType = typeof(DataGenerator))]
        public void ValidIsbnCode10Test(string value)
        {
            var code = Isbn.Create(TypeIsbn.Isbn10, value);
            Assert.True(code.IsSuccess);
            Assert.Equal(code.Value.IsbnCode, value);
        }

        [Theory]
        [MemberData(nameof(DataGenerator.GetIsbn13Code), MemberType = typeof(DataGenerator))]
        public void ValidIsbnCode13Test(string value)
        {
            var code = Isbn.Create(TypeIsbn.Isbn13, value);
            Assert.True(code.IsSuccess);
            Assert.Equal(code.Value.IsbnCode, value);
        }
    }
}