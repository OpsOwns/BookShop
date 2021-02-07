using System;
using Shop.Store.Core.Price;
using Shop.Store.Tests.Helper;
using Xunit;

namespace Shop.Store.Tests.Domain
{
    public class PriceInfoTest : TestBase
    {
        [Fact]
        public void ShouldCreateDefaultBookCostsTest()
        {
            var bookCosts = new BookCosts(Money.Create(5, "PL").Value, 10, DefaultBookInfo);
            Assert.NotNull(bookCosts);
            Assert.Equal(5, bookCosts.BookCost.Amount);
            Assert.Equal("PL", bookCosts.BookCost.Currency);
        }
        [Fact]
        public void IncreaseCostOfBookWithDifferentCurrencyFailTest()
        {
            var bookCosts = new BookCosts(Money.Create(5, "PL").Value, 10, DefaultBookInfo);
            var exception = Assert.Throws<Exception>(() => bookCosts.IncreaseCosts(Money.Create(10, "EU").Value));
            Assert.Equal("Currency must be same", exception.Message);
        }
        [Fact]
        public void IncreaseCostsOfBookTest()
        {
            var bookCosts = new BookCosts(Money.Create(5, "PL").Value, 10, DefaultBookInfo);
            bookCosts.IncreaseCosts(Money.Create(10, "PL").Value);
            Assert.Equal(15, bookCosts.BookCost.Amount);
        }
    }

}
