using System;
using SalaryCalculator.Core.ValueObjects;

namespace SalaryCalculator.Core
{
	public struct Money : IEquatable<Money>
	{
		public decimal Value { get; private set; }
		public Currency Currency { get; private set; }

		public static Money ZeroVND => new Money(decimal.Zero, Currency.VND);
		public static Money OneVND => new Money(decimal.One, Currency.VND);

		public Money(
			decimal value,
			Currency currency)
		{
			Value = value;
			Currency = currency;
		}

		public static Money operator +(Money a, Money b)
		{
			if (a.Currency != b.Currency)
			{
				throw new InvalidOperationException("Money has to have the same currency");
			}

			return new Money(a.Value + b.Value, a.Currency);
		}

		public static Money operator -(Money a, Money b)
		{
			if (a.Currency != b.Currency)
			{
				throw new InvalidOperationException("Money has to have the same currency");
			}

			return new Money(a.Value - b.Value, a.Currency);
		}

		public static Money operator *(Money a, int b)
		{
			return new Money(a.Value * b, a.Currency);
		}

		public static Money operator *(Money a, decimal b)
		{
			return new Money(a.Value * b, a.Currency);
		}

		public static Money operator *(decimal a, Money b)
		{
			return new Money(a * b.Value, b.Currency);
		}

		public static Money operator *(int a, Money b)
		{
			return new Money(a * b.Value, b.Currency);
		}

		public static Money operator /(Money a, decimal b)
		{
			return new Money(a.Value / b, a.Currency);
		}

		public static bool operator ==(Money a, Money b)
		{
			if (a.Currency != b.Currency)
			{
				return false;
			}

			return a.Equals(b);
		}

		public static bool operator !=(Money a, Money b) => !(a == b);

		public static bool operator >(Money a, Money b)
		{
			if (a.Currency != b.Currency)
			{
				throw new InvalidOperationException("Money has to have the same currency");
			}

			return a.Value > b.Value;
		}

		public static bool operator <(Money a, Money b)
		{
			if (a.Currency != b.Currency)
			{
				throw new InvalidOperationException("Money has to have the same currency");
			}

			return a.Value < b.Value;
		}

		public static bool operator <=(Money a, Money b)
		{
			if (a.Currency != b.Currency)
			{
				throw new InvalidOperationException("Money has to have the same currency");
			}

			return a.Value <= b.Value;
		}

		public static bool operator >=(Money a, Money b)
		{
			if (a.Currency != b.Currency)
			{
				throw new InvalidOperationException("Money has to have the same currency");
			}

			return a.Value >= b.Value;
		}

		public static implicit operator decimal(Money money) => money.Value;

		public bool Equals(Money other)
		{
			return Value == other.Value && Currency == other.Currency;
		}

		public override bool Equals(object obj)
		{
			if (obj is Money)
			{
				return Equals(obj);
			}

			return false;
		}

		public override int GetHashCode()
		{
			return (Value.GetHashCode() + Currency.GetHashCode()) ^ 3;
		}
	}
}