using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace SalaryCalculator.Core.Domain
{
	public class ProgressiveTaxRateLookUpTable
	{
		private readonly ProgressiveTaxRate[] _progressiveTaxes;

		public ProgressiveTaxRateLookUpTable(ProgressiveTaxRate[] progressiveTaxes)
		{
			_progressiveTaxes = progressiveTaxes;
		}

		public PersonalIncomeTax this[AssessableIncome income]
		{
			get
			{
				var progressiveTaxRateContainsAssessableIncome = _progressiveTaxes
					.SingleOrDefault(p => p.HasAssessableIncomeWithinRange(income));

				if (progressiveTaxRateContainsAssessableIncome is null)
				{
					return new PersonalIncomeTax(Money.ZeroVND);
				}

				var pit = (income.Amount - progressiveTaxRateContainsAssessableIncome.LowerBound) *
					progressiveTaxRateContainsAssessableIncome.Rate + _progressiveTaxes
						.Where(_ => _.ProgressiveTaxRateLevel < progressiveTaxRateContainsAssessableIncome.ProgressiveTaxRateLevel)
						.Select(p => p.MaxIncomeTaxPerLevel)
						.Sum();
				return new PersonalIncomeTax(pit.Round());
			}
		}

		public IReadOnlyList<ProgressiveTaxRate> AsReadOnlyCollection()
		{
			return new ReadOnlyCollection<ProgressiveTaxRate>(_progressiveTaxes);
		}

		public override string ToString()
		{
			var result = new StringBuilder();
			foreach(var tax in _progressiveTaxes)
			{
				result.AppendLine($"Level: {tax.ProgressiveTaxRateLevel} - LowerBound: {tax.LowerBound.Value} " +
					$"- UpperBound: {tax.UpperBound} - Rate: {tax.Rate}");
				result.AppendLine();
			}
			return result.ToString();
		}
	}
}