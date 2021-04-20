using System;
using System.Collections.Generic;
using System.Text;

namespace SalaryCalculator.Infra.Persistent.Common.Models
{
	public class DecryptedEmpMonthlyEnteredRecord
	{
		public string Fullname { get; set; }
		public string Email { get; set; }
		public decimal GrossContractSalary { get; set; }
		public decimal ProbationGrossContractSalary { get; set; }
		public int ActualWorkingDays { get; set; }
		public int ProbationWorkingDays { get; set; }
		public DecryptedTaxableAllowance TaxableAnnualLeave { get; set; }
		public DecryptedTaxableAllowance Taxable13MonthSalary { get; set; }
		public DecryptedTaxableAllowance TaxableOthers { get; set; }
		public DecryptedNonTaxableAllowance[] NonTaxableAllowances { get; set; }
		public decimal PaymentAdvance { get; set; }
		public decimal[] AdjustmentAdditions { get; set; }
		public decimal[] AdjustmentDeductions { get; set; }
		public string Currency { get; set; }
	}
}
