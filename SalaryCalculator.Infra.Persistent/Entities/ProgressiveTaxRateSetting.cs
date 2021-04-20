using System;
using System.Collections.Generic;
using System.Text;
using SalaryCalculator.Infra.Persistent.Entities;

namespace SalaryCalculator.Infra.Persistent.Entities
{
	public class ProgressiveTaxRateSetting : EntityBase
	{
		public ProgressiveTaxRateSetting()
		{
		}
		public decimal LowerBound { get; set; }
		public decimal UpperBound { get; set; }
		public decimal Rate { get; set; }
		public int TaxRateLevel { get; set; }
		public Guid CurrencyId { get; set; }
		public SalaryCurrency SalaryCurrency { get; set; }
	}
}
