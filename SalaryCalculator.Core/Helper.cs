using System;
using System.Collections.Generic;
using System.Linq;

namespace SalaryCalculator.Core
{
	public static class Helper
	{
		public static Money Round(this Money money)
		{
			return new Money(Math.Round(money.Value, MidpointRounding.AwayFromZero), money.Currency);
		}

		public static Money Sum(this IEnumerable<Money> money)
		{
			return money.Aggregate(Money.ZeroVND, (a,b) => a + b);
		}
	}
}