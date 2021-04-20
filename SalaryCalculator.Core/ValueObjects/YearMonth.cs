using System;
using System.Collections.Generic;
using System.Text;

namespace SalaryCalculator.Core.ValueObjects
{
	public struct YearMonth : IEquatable<YearMonth>
	{
		public string Value { get; private set; }
		public int Year { get; private set; }
		public int Month { get; private set; }

		public static YearMonth FirstYearMonth => new YearMonth();

		public YearMonth(string yearMonth)
		{
			Value = yearMonth;
			Year = Int32.Parse(yearMonth.Split('-')[0]);
			Month = Int32.Parse(yearMonth.Split('-')[1]);
		}

		public YearMonth(int year, int month)
		{
			Value = new DateTime(year, month, 1).ToString("yyyy-MM");
			Year = year;
			Month = month;
		}

		public YearMonth(DateTime date)
		{
			Value = date.ToString("yyyy-MM");
			Year = date.Year;
			Month = date.Month;
		}

		public static bool TryParse(string yearMonth)
		{
			if (yearMonth == null) return false;

			var split = yearMonth.Split('-');
			return split.Length == 2 && Int32.TryParse(split[0], out int year)
				&& Int32.TryParse(split[1], out int month)
				&& year > 0 && month > 0 && month < 13;
		}

		public static bool TryParse(string yearMonth, out YearMonth parsedYearMonth)
		{
			if (yearMonth == null)
			{
				parsedYearMonth = new YearMonth(); 
				return false;
			}

			var split = yearMonth.Split('-');
			
			if (split.Length == 2 && Int32.TryParse(split[0], out int year)
				&& Int32.TryParse(split[1], out int month)
				&& year > 0 && month > 0 && month < 13)
			{
				parsedYearMonth = new YearMonth(year, month);
				return true;
			}

			parsedYearMonth = new YearMonth();
			return false;
		}

		public static int Parse(int year, int month)
		{
			return year * 100 + month;
		}

		public static bool operator >=(YearMonth a, YearMonth b) 
		{
			return Parse(a.Year, a.Month) >= Parse(b.Year, b.Month);
		}

		public static bool operator <=(YearMonth a, YearMonth b)
		{
			return Parse(a.Year, a.Month) <= Parse(b.Year, b.Month);
		}

		public static bool operator >(YearMonth a, YearMonth b)
		{
			return Parse(a.Year, a.Month) > Parse(b.Year, b.Month);
		}

		public static bool operator <(YearMonth a, YearMonth b)
		{
			return Parse(a.Year, a.Month) < Parse(b.Year, b.Month);
		}

		public static bool operator ==(YearMonth a, YearMonth b) => a.Equals(b);

		public static bool operator !=(YearMonth a, YearMonth b) => !(a == b);

		public bool Equals(YearMonth other)
		{
			return Year == other.Year && Month == other.Month;
		}

		public override bool Equals(object obj)
		{
			if (obj is YearMonth)
			{
				return Equals(obj);
			}

			return false;
		}

		public override int GetHashCode()
		{
			return Year.GetHashCode() + Month.GetHashCode();
		}

		public static implicit operator string(YearMonth yearMonth) => yearMonth.Value;
	}
}
