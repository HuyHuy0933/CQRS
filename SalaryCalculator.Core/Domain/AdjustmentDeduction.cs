namespace SalaryCalculator.Core.Domain
{
	public class AdjustmentDeduction
	{
		public AdjustmentDeduction(Money amount)
		{
			Amount = amount;
		}

		public Money Amount { get; }
	}
}