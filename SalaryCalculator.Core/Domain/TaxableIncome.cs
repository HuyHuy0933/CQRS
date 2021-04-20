using System.Linq;

namespace SalaryCalculator.Core.Domain
{
	public class TaxableIncome
	{
		public Money Amount { get; }

		public TaxableIncome(
			ActualGrossSalary actualGrossSalary,
			TaxableAllowance[] taxableAllowances)
		{
			Amount = actualGrossSalary.Amount + taxableAllowances.Select(_ => _.Amount).Sum();
		}
	}
}