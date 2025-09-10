using System;

namespace CR_COMPUTER.Domain.ValueObjects
{
    /// <summary>
    /// Represents a monetary value with currency
    /// </summary>
    public class Money : IEquatable<Money>
    {
        public decimal Amount { get; private set; }
        public string Currency { get; private set; }

        protected Money() { } // EF Core constructor

        public Money(decimal amount, string currency)
        {
            if (amount < 0) throw new ArgumentException("Amount cannot be negative", nameof(amount));
            Amount = amount;
            Currency = currency ?? throw new ArgumentNullException(nameof(currency));
        }

        public Money Add(Money other)
        {
            if (Currency != other.Currency)
                throw new InvalidOperationException("Cannot add money with different currencies");

            return new Money(Amount + other.Amount, Currency);
        }

        public Money Subtract(Money other)
        {
            if (Currency != other.Currency)
                throw new InvalidOperationException("Cannot subtract money with different currencies");

            return new Money(Amount - other.Amount, Currency);
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Money);
        }

        public bool Equals(Money? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return Amount == other.Amount && Currency == other.Currency;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Amount, Currency);
        }

        public static bool operator ==(Money? left, Money? right)
        {
            if (left is null) return right is null;
            return left.Equals(right);
        }

        public static bool operator !=(Money? left, Money? right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return $"{Amount} {Currency}";
        }
    }
}
