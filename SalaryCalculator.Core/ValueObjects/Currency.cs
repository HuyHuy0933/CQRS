using System;

namespace SalaryCalculator.Core.ValueObjects
{
	public struct Currency : IEquatable<Currency>
	{
		public static Currency VND => new Currency("VND");
		public string Value { get; private set; }

		public Currency(string value)
		{
			Value = value;
		}

		public static bool operator ==(Currency a, Currency b) => a.Equals(b);
		public static bool operator !=(Currency a, Currency b) => !(a == b);

		public bool Equals(Currency other)
		{
			return Value == other.Value;
		}

		public override bool Equals(object obj)
		{
			if (obj is Currency)
			{
				return Equals(obj);
			}

			return false;
		}

		public override int GetHashCode()
		{
			return Value.GetHashCode();
		}

		public Currency Clone()
		{
			return new Currency(Value);
		}

		public static implicit operator string(Currency c) => c.Value;
	}
}