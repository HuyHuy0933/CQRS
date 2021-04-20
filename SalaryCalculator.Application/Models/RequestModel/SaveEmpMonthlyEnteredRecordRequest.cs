using MediatR;
using SalaryCalculator.Application.Models.ResponseModel;
using SalaryCalculator.Core.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalaryCalculator.Application.Models.RequestModel
{
	public class SaveEmpMonthlyEnteredRecordRequest : IRequest<Result<SaveEmpMonthlyEnteredRecordResponse>>
	{
		public List<PartialSaveEmpMonthlyEnteredRecordRequest> Records { get; set; }
		public string Key { get; set; }
		public string YearMonth { get; set; }
		public int StandardWorkingDays { get; set; }
	}

	public class PartialSaveEmpMonthlyEnteredRecordRequest
	{
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
