namespace SalaryCalculator.Core.Domain
{
	public class PaymentAdvance
	{
		public PaymentAdvance(Money amount)
		{
			Amount = amount;
		}

		public Money Amount { get; }
	}
}