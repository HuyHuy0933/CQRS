namespace SalaryCalculator.Core.Domain
{
	public class EmployeeUnemploymentInsurance
	{
		public Money Amount { get; }

		public EmployeeUnemploymentInsurance(Money amount)
		{
			Amount = amount;
		}
	}
}