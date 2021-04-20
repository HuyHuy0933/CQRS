using SalaryCalculator.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SalaryCalculator.Core.Test
{
	public class MonthlyWorkingDayTest
	{
		[Fact]
		public void February_Working_Days_Not_Leap_Year()
		{
			var days = new MonthlyWorkingDay("2019-02").CalculatedMonthlyWorkingDays();

			Assert.Equal(20, days);
		}

		[Fact]
		public void February_Working_Days_In_Leap_Year()
		{
			var days = new MonthlyWorkingDay("2020-02").CalculatedMonthlyWorkingDays();

			Assert.Equal(20, days);
		}

		[Fact]
		public void Working_Days_In_30_Days_Month()
		{
			var days = new MonthlyWorkingDay("2020-04").CalculatedMonthlyWorkingDays();

			Assert.Equal(22, days);
		}

		[Fact]
		public void Working_Days_In_31_Days_Month()
		{
			var days = new MonthlyWorkingDay("2020-10").CalculatedMonthlyWorkingDays();

			Assert.Equal(22, days);
		}
	}
}
