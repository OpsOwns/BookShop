using CSharpFunctionalExtensions;
using Shop.Shared.Shared;
using System;
using System.Collections.Generic;

namespace Shop.Store.Core.Price
{
    public class Money : ValueObject
    {
        public decimal Amount { get; }
        public string Currency { get; }
        private Money(decimal amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }
        public static Result<Money> Create(decimal amount, string currency) =>
            currency.IsEmpty() ? Result.Failure<Money>($"{nameof(currency)} can't be empty") :
            amount < 0 ? Result.Failure<Money>($"{nameof(amount)} can't be less than 0") :
            Result.Success(new Money(amount, currency));
        public static Money operator +(Money money1, Money money2) =>
            money1.Currency != money2.Currency
                ? throw new Exception("Currency must be same")
                : new Money(money1.Amount + money2.Amount, money1.Currency);
        public static Money operator -(Money money1, Money money2) =>
            money1.Currency != money2.Currency
                ? throw new Exception("Currency must be same")
                : new Money(money1.Amount - money2.Amount, money1.Currency);
        public static Money operator *(Money money1, Money money2) =>
            money1.Currency != money2.Currency
                ? throw new Exception("Currency must be same")
                : new Money(money1.Amount * money2.Amount, money1.Currency);
        public override string ToString() => Currency + Amount.ToString("0.00");
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
            yield return Currency;
        }
        public override bool Equals(object obj) => ReferenceEquals(this, obj) || !ReferenceEquals(obj, null);
        protected bool Equals(Money other) => base.Equals(other) && Amount == other.Amount && Currency == other.Currency;
        public override int GetHashCode() => HashCode.Combine(base.GetHashCode(), Amount, Currency);
    }
}
