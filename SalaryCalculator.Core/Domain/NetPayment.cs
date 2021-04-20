using System.Linq;

namespace SalaryCalculator.Core.Domain
{
	public class NetPayment
	{
		public Money Amount { get; }

		public NetPayment(
			NetIncome netIncome,
			PaymentAdvance paymentAdvance,
			AdjustmentAddition[] adjustmentAdditions,
			AdjustmentDeduction[] adjustmentDeductions)
		{
			Amount = netIncome.Amount - paymentAdvance.Amount + 
			         adjustmentAdditions.Select(_ => _.Amount).Sum() - 
			         adjustmentDeductions.Select(_ => _.Amount).Sum();
		}
	}
}