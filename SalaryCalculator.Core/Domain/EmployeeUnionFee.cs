namespace SalaryCalculator.Core.Domain
{
	public class EmployeeUnionFee
	{
		public Money Amount { get; }

		public EmployeeUnionFee(Money amount)
		{
			Amount = amount;
		}
	}
}