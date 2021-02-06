using Shop.Store.Core.Book;
using Shop.Store.Tests.Helper;
using System.Collections.Generic;
using System.Data.SqlTypes;
using Shop.Store.Core.Price;
using Xunit;

namespace Shop.Store.Tests.Domain
{
    public class PriceInfoTest : TestBase
    {
        [Fact]
        public void ShouldAddPriceTest()
        {
            var price = new BookPrice();
            price.AddBook(DefaultBookInfo, 20.5M, 5);
        }
    }

    public class BookPrice
    {
        private List<BookPrice> _books = new();
        public IReadOnlyList<BookPrice> BookInfos => _books;
        public Books BookInfo { get; private set; }
        public Money Money { get; set; }
        public void AddBook(Books defaultBookInfo, decimal prize, int quantity)
        {
        
        }
    }
}
