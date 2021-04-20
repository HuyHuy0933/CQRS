namespace SalaryCalculator.Core.Domain
{
	public class InsuranceSalary
	{
		public Money Amount { get; }

		public InsuranceSalary(Money amount)
		{
			Amount = amount;
		}
	}
}