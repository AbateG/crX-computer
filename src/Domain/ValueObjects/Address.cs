using System;

namespace CR_COMPUTER.Domain.ValueObjects
{
    /// <summary>
    /// Represents an address value object
    /// </summary>
    public class Address : IEquatable<Address>
    {
        public string Street { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string ZipCode { get; private set; }
        public string Country { get; private set; }

        protected Address() { } // EF Core constructor

        public Address(string street, string city, string state, string zipCode, string country)
        {
            Street = street ?? throw new ArgumentNullException(nameof(street));
            City = city ?? throw new ArgumentNullException(nameof(city));
            State = state ?? throw new ArgumentNullException(nameof(state));
            ZipCode = zipCode ?? throw new ArgumentNullException(nameof(zipCode));
            Country = country ?? throw new ArgumentNullException(nameof(country));
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Address);
        }

        public bool Equals(Address? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return Street == other.Street &&
                   City == other.City &&
                   State == other.State &&
                   ZipCode == other.ZipCode &&
                   Country == other.Country;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Street, City, State, ZipCode, Country);
        }

        public static bool operator ==(Address? left, Address? right)
        {
            if (left is null) return right is null;
            return left.Equals(right);
        }

        public static bool operator !=(Address? left, Address? right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return $"{Street}, {City}, {State} {ZipCode}, {Country}";
        }
    }
}
