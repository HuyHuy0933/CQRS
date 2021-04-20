namespace SalaryCalculator.Core.Domain
{
	public class EmployerSocialInsurance
	{
		public Money Amount { get; }

		public EmployerSocialInsurance(Money amount)
		{
			Amount = amount;
		}
	}
}