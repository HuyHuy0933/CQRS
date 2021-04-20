using System;
using System.Collections.Generic;
using System.Text;

namespace SalaryCalculator.Application.Common.Validations
{
	public static class ValidationHelpers
	{
		public static bool IsValidYearMonth(string yearMonth)
		{
			var split = yearMonth.Split("-");
			return split.Length == 2 && Int32.TryParse(split[0], out int year)
				&& Int32.TryParse(split[1], out int month)
				&& year > 0 && month > 0 && month < 13;
		}
	}
}
