namespace SalaryCalculator.Core.Domain
{
	public class EmployerUnemploymentInsurance
	{
		public Money Amount { get; }

		public EmployerUnemploymentInsurance(Money amount)
		{
			Amount = amount;
		}
	}
}