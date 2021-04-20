namespace SalaryCalculator.Core.Domain
{
	public class EmployerUnionFee
	{
		public Money Amount { get; }

		public EmployerUnionFee(Money amount)
		{
			Amount = amount;
		}
	}
}