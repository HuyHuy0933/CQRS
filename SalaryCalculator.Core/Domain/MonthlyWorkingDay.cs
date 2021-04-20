using SalaryCalculator.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalaryCalculator.Core.Domain
{
	public class MonthlyWorkingDay
	{
		public MonthlyWorkingDay(string yearMonth)
		{
			YearMonth = new YearMonth(yearMonth);
			First = new DateTime(YearMonth.Year, YearMonth.Month, 1);
			DaysInMonth = DateTime.DaysInMonth(YearMonth.Year, YearMonth.Month);
		}

		public YearMonth YearMonth { get; }
		public DateTime First { get; }
		public int DaysInMonth { get; set; }

		public int CalculatedMonthlyWorkingDays()
		{
			var weekendsInMonth = 0;
			var first = First;
			for (var i = 0; i < DaysInMonth; i++)
			{
				if (first.DayOfWeek == DayOfWeek.Saturday || first.DayOfWeek == DayOfWeek.Sunday)
				{
					weekendsInMonth++;
				}
				first = first.AddDays(1);
			}

			return DaysInMonth - weekendsInMonth;
		}
	}
}
