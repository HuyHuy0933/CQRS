namespace SalaryCalculator.Core.Domain
{
	public class EmployerHealthcareInsurance
	{
		public Money Amount { get; }

		public EmployerHealthcareInsurance(Money amount)
		{
			Amount = amount;
		}
	}
}