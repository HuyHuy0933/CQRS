namespace SalaryCalculator.Core.Domain
{
	public class EmployeeHealthCareInsurance
	{
		public Money Amount { get; }

		public EmployeeHealthCareInsurance(Money amount)
		{
			Amount = amount;
		}
	}
}