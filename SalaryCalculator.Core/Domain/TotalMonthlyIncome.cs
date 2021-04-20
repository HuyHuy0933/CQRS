using System.Linq;

namespace SalaryCalculator.Core.Domain
{
	public class TotalMonthlyIncome
	{
		public Money Amount { get; }

		public TotalMonthlyIncome(
			ActualGrossSalary actualGrossSalary,
			TaxableAllowance[] taxableAllowances,
			NonTaxableAllowance[] nonTaxableAllowances)
		{
			Amount = actualGrossSalary.Amount +
			         taxableAllowances.Select(_ => _.Amount).Sum() +
			         nonTaxableAllowances.Select(_ => _.Amount).Sum();
		}
	}
}