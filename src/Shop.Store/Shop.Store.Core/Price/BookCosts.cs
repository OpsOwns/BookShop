using Shop.Shared.Domain;
using Shop.Store.Core.Book;
using System;

namespace Shop.Store.Core.Price
{
    public class BookCosts : Entity
    {
        public BookCostsId BookCostsId { get; private set; }
        public Money BookCost { get; private set; }
        public int Quantity { get; private set; }
        public Books Book { get; private set; }
        public bool OutBooks => Quantity == 0;
        protected BookCosts()
        {
            BookCostsId = new BookCostsId(Guid.NewGuid());
        }
        public BookCosts(Money bookCost, int quantity, Books book) : this()
        {
            BookCost = bookCost;
            Quantity = quantity >= 0 ? quantity : throw new BookException($"{nameof(quantity)} can't be less than 0");
            Book = book;
        }
        public void IncreaseCosts(Money money)
        {
            BookCost += money ?? throw new BookException($"{nameof(money)} can't be null");
        }
        public void ChangeCosts(Money money, int quantity, Books book)
        {
            if (quantity < 0)
                throw new BookException($"{nameof(quantity)} can't be less than 0");
            BookCost = money ?? throw new BookException($"{nameof(money)} can't be null");
            Quantity = quantity;
            Book = book ?? throw new BookException($"{nameof(book)} can't be null");
        }
    }
}
