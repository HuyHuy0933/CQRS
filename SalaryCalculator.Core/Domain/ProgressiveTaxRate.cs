using System;
using SalaryCalculator.Core.ValueObjects;

namespace SalaryCalculator.Core.Domain
{
	public class ProgressiveTaxRate
	{
		public ProgressiveTaxRate(
			Money lowerBound,
			Money upperBound,
			decimal rate,
			ProgressiveTaxRateLevel progressiveTaxRateLevel)
		{
			LowerBound = lowerBound;
			UpperBound = upperBound;
			Rate = rate / 100;
			ProgressiveTaxRateLevel = progressiveTaxRateLevel;
		}

		public Money LowerBound { get; }
		public Money UpperBound { get; }
		public ProgressiveTaxRateLevel ProgressiveTaxRateLevel { get; }
		public decimal Rate { get; }
		public Money MaxIncomeTaxPerLevel 
			=> new Money((UpperBound - LowerBound) * Rate, LowerBound.Currency).Round();

		public bool HasAssessableIncomeWithinRange(AssessableIncome assessableIncome)
		{
			return LowerBound <= assessableIncome.Amount && assessableIncome.Amount < UpperBound;
		}
	}
}