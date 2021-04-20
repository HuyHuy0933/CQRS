namespace SalaryCalculator.Core.Domain
{
	public class EmployeeSocialInsurance
	{
		public Money Amount { get; }

		public EmployeeSocialInsurance(Money amount)
		{
			Amount = amount;
		}
	}
}