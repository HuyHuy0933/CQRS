using System;
using System.Collections.Generic;
using System.Text;

namespace SalaryCalculator.Core.Domain
{
	public class EmployeeMonthlyEnteredRecord
	{
		public EmployeeMonthlyEnteredRecord
		(
			string fullname,
			string email,
			GrossContractedSalary grossContractSalary,
			GrossContractedSalary probationGrossContractSalary,
			int actualWorkingDays,
			int probationWorkingDays,
			TaxableAllowance taxableAnnualLeave,
			TaxableAllowance taxable13MonthSalary,
			TaxableAllowance taxableOthers,
			NonTaxableAllowance[] nonTaxableAllowances,
			PaymentAdvance paymentAdvance,
			AdjustmentAddition[] adjustmentAdditions,
			AdjustmentDeduction[] adjustmentDeductions
		)
		{
			Fullname = fullname;
			Email = email;
			this.grossContractSalary = grossContractSalary;
			ProbationGrossContractSalary = probationGrossContractSalary;
			ActualWorkingDays = actualWorkingDays;
			ProbationWorkingDays = probationWorkingDays;
			TaxableAnnualLeave = taxableAnnualLeave;
			Taxable13MonthSalary = taxable13MonthSalary;
			TaxableOthers = taxableOthers;
			NonTaxableAllowances = nonTaxableAllowances;
			PaymentAdvance = paymentAdvance;
			AdjustmentAdditions = adjustmentAdditions;
			AdjustmentDeductions = adjustmentDeductions;
		}

		public string Fullname { get; set; }
		public string Email { get; }
		public GrossContractedSalary grossContractSalary { get; }
		public GrossContractedSalary ProbationGrossContractSalary { get; }
		public int ActualWorkingDays { get; }
		public int ProbationWorkingDays { get; }
		public TaxableAllowance TaxableAnnualLeave { get; }
		public TaxableAllowance Taxable13MonthSalary { get; }
		public TaxableAllowance TaxableOthers { get; }
		public NonTaxableAllowance[] NonTaxableAllowances { get; }
		public PaymentAdvance PaymentAdvance { get; }
		public AdjustmentAddition[] AdjustmentAdditions { get; }
		public AdjustmentDeduction[] AdjustmentDeductions { get; }
	}
}
