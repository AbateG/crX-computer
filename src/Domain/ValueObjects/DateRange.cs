using System;

namespace CR_COMPUTER.Domain.ValueObjects
{
    /// <summary>
    /// Represents a date range value object
    /// </summary>
    public class DateRange : IEquatable<DateRange>
    {
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        protected DateRange() { } // EF Core constructor

        public DateRange(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
                throw new ArgumentException("Start date must be before or equal to end date");

            StartDate = startDate;
            EndDate = endDate;
        }

        public bool Includes(DateTime date)
        {
            return date >= StartDate && date <= EndDate;
        }

        public bool Overlaps(DateRange other)
        {
            return StartDate <= other.EndDate && EndDate >= other.StartDate;
        }

        public TimeSpan Duration => EndDate - StartDate;

        public override bool Equals(object? obj)
        {
            return Equals(obj as DateRange);
        }

        public bool Equals(DateRange? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;

            return StartDate == other.StartDate && EndDate == other.EndDate;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(StartDate, EndDate);
        }

        public static bool operator ==(DateRange? left, DateRange? right)
        {
            if (left is null) return right is null;
            return left.Equals(right);
        }

        public static bool operator !=(DateRange? left, DateRange? right)
        {
            return !(left == right);
        }

        public override string ToString()
        {
            return $"{StartDate:yyyy-MM-dd} to {EndDate:yyyy-MM-dd}";
        }
    }
}
