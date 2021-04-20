namespace SalaryCalculator.Core.Domain
{
	public class AdjustmentAddition
	{
		public Money Amount { get; }

		public AdjustmentAddition(Money amount)
		{
			Amount = amount;
		}

	}
}