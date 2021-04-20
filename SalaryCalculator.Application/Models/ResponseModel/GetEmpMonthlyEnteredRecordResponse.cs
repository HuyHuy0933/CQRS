using System;
using System.Collections.Generic;
using System.Text;

namespace SalaryCalculator.Application.Models.ResponseModel
{
	public class GetEmpMonthlyEnteredRecordResponse
	{
		public GetEmpMonthlyEnteredRecordResponse(
			List<PartialGetEmpMonthlyEnteredRecordResponse> records,
			int standardWorkingDays)
		{
			Records = records;
			StandardWorkingDays = standardWorkingDays;
		}

		public List<PartialGetEmpMonthlyEnteredRecordResponse> Records { get; }
		public int StandardWorkingDays { get; }
	}

	public class PartialGetEmpMonthlyEnteredRecordResponse
	{
		public PartialGetEmpMonthlyEnteredRecordResponse(
			string email,
			string fullname,
			decimal grossContractSalary,
			decimal probationGrossContractSalary,
			int actualWorkingDays,
			int probationWorkingDays,
			decimal taxableAnnualLeave,
			decimal taxable13MonthSalary,
			decimal taxableOthers,
			decimal nonTaxableAllowance,
			decimal paymentAdvance,
			decimal adjustmentAddition,
			decimal adjustmentDeduction,
			string currency
		)
		{
			Id = email;
			Fullname = fullname;
			Email = email;
			GrossContractSalary = grossContractSalary;
			ProbationGrossContractSalary = probationGrossContractSalary;
			ActualWorkingDays = actualWorkingDays;
			ProbationWorkingDays = probationWorkingDays;
			TaxableAnnualLeave = taxableAnnualLeave;
			Taxable13MonthSalary = taxable13MonthSalary;
			TaxableOthers = taxableOthers;
			NonTaxableAllowance = nonTaxableAllowance;
			PaymentAdvance = paymentAdvance;
			AdjustmentAddition = adjustmentAddition;
			AdjustmentDeduction = adjustmentDeduction;
			Currency = currency;
		}

		public string Id { get; set; }
		public string Fullname { get; set; }
		public string Email { get; set; }
		public decimal GrossContractSalary { get; set; }
		public decimal ProbationGrossContractSalary { get; set; }
		public int ActualWorkingDays { get; set; }
		public int ProbationWorkingDays { get; set; }
		public decimal TaxableAnnualLeave { get; set; }
		public decimal Taxable13MonthSalary { get; set; }
		public decimal TaxableOthers { get; set; }
		public decimal NonTaxableAllowance { get; set; }
		public decimal PaymentAdvance { get; set; }
		public decimal AdjustmentAddition { get; set; }
		public decimal AdjustmentDeduction { get; set; }
		public string Currency { get; set; }
	}
}
