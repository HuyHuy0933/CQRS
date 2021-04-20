namespace SalaryCalculator.Core.Domain
{
	public class GrossContractedSalary
	{
		public Money Amount { get; }

		public GrossContractedSalary(Money amount)
		{
			Amount = amount;
		}
	}
}