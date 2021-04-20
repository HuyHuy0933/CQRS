using System;
using System.Collections.Generic;
using System.Text;

namespace SalaryCalculator.Infra.Persistent.Common.Models
{
	public class DecryptedEmpMonthlySalary
	{
		public string Fullname { get; set; }
		public string Email { get; set; }
		public string Position { get; set; }
		public string EmployeeType { get; set; }
		public decimal GrossContractSalary { get; set; }
		public decimal InsuranceSalary { get; set; }
		public int StandardWorkingDays { get; set; }
		public int ActualWorkingDays { get; set; }
		public int ProbationWorkingDays { get; set; }
		public decimal ActualGrossSalary { get; set; }
		public decimal TotalMonthlyIncome { get; set; }
		public decimal TaxableIncome { get; set; }
		public decimal EmployeeSocialInsurance { get; set; }
		public decimal EmployeeHealthcareInsurance { get; set; }
		public decimal EmployeeUnemploymentInsurance { get; set; }
		public decimal EmployeeUnionFee { get; set; }
		public decimal EmployerSocialInsurance { get; set; }
		public decimal EmployerHealthcareInsurance { get; set; }
		public decimal EmployerUnemploymentInsurance { get; set; }
		public decimal EmployerUnionFee { get; set; }
		public decimal PersonalDeduction { get; set; }
		public int NumberOfDependants { get; set; }
		public decimal DependantDeduction { get; set; }
		public decimal AssessableIncome { get; set; }
		public decimal PIT { get; set; }
		public decimal NetIncome { get; set; }
		public decimal TotalSalaryCost { get; set; }
		public decimal NetPayment { get; set; }
		public decimal PaymentAdvance { get; set; }
		public DecryptedTaxableAllowance[] TaxableAllowances { get; set; }
		public DecryptedNonTaxableAllowance[] NonTaxableAllowances { get; set; }
		public decimal AdjustmentDeduction { get; set; }
		public decimal AdjustmentAddition { get; set; }
	}
}
