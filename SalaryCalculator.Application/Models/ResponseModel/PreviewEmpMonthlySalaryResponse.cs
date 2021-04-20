using CsvHelper.Configuration;
using SalaryCalculator.Core.Common;
using SalaryCalculator.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace SalaryCalculator.Application.Models.ResponseModel
{
	public class PreviewEmpMonthlySalaryResponse
	{
		public PreviewEmpMonthlySalaryResponse(
			PreviewEmpMonthlySalary[] salaries)
		{
			Salaries = salaries;
		}
		public PreviewEmpMonthlySalary[] Salaries { get; }
	}

	public class PreviewEmpMonthlySalary
	{
		public PreviewEmpMonthlySalary(
			string fullname,
			string email,
			string employeeType,
			string position,
			int numberOfDependants,
			int standardWorkingDays,
			int actualWorkingDays,
			decimal grossContractSalary,
			decimal insuranceSalary,
			decimal actualGrossSalary,
			decimal taxableIncome,
			decimal totalMonthlyIncome,
			decimal employeeSocialInsurance,
			decimal employeeHealthcareInsurance,
			decimal employeeUnemploymentInsurance,
			decimal employeeUnionFee,
			decimal employerSocialInsurance,
			decimal employerHealthcareInsurance,
			decimal employerUnemploymentInsurance,
			decimal employerUnionFee,
			decimal personalDeduction,
			decimal dependantDeduction,
			decimal assessableIncome,
			decimal netIncome,
			decimal pit,
			decimal totalSalaryCost,
			decimal paymentAdvance,
			TaxableAllowance[] taxableAllowances,
			decimal nonTaxableAllowances,
			decimal netPayment,
			decimal adjustmentDeduction,
			decimal adjustmentAddition)
		{
			Fullname = fullname;
			Email = email;
			Position = position;
			EmployeeType = employeeType;
			GrossContractSalary = grossContractSalary;
			InsuranceSalary = insuranceSalary;
			StandardWorkingDays = standardWorkingDays;
			ActualWorkingDays = actualWorkingDays;
			ActualGrossSalary = actualGrossSalary;
			TotalMonthlyIncome = totalMonthlyIncome;
			TaxableIncome = taxableIncome;
			EmployeeSocialInsurance = employeeSocialInsurance;
			EmployeeHealthcareInsurance = employeeHealthcareInsurance;
			EmployeeUnemploymentInsurance = employeeUnemploymentInsurance;
			EmployeeUnionFee = employeeUnionFee;
			EmployerSocialInsurance = employerSocialInsurance;
			EmployerHealthcareInsurance = employerHealthcareInsurance;
			EmployerUnemploymentInsurance = employerUnemploymentInsurance;
			EmployerUnionFee = employerUnionFee;
			PersonalDeduction = personalDeduction;
			NumberOfDependants = numberOfDependants;
			DependantDeduction = dependantDeduction;
			AssessableIncome = assessableIncome;
			PIT = pit;
			PaymentAdvance = paymentAdvance;
			TaxableAnnualLeave = taxableAllowances.ToList().FirstOrDefault(x => 
				x.Name == Constants.TAXABLE_ANNUAL_LEAVE).Amount.Value;
			Taxable13MonthSalary = taxableAllowances.ToList().FirstOrDefault(x => 
				x.Name == Constants.TAXABLE_13MONTH_SALARY).Amount.Value;
			TaxableOthers = taxableAllowances.ToList().FirstOrDefault(x => 
				x.Name == Constants.TAXABLE_OTHERS).Amount.Value;
			NonTaxableAllowances = nonTaxableAllowances;
			NetIncome = netIncome;
			TotalSalaryCost = totalSalaryCost;
			AdjustmentAddition = adjustmentAddition;
			AdjustmentDeduction = adjustmentDeduction;
			NetPayment = netPayment;
		}
		public string Fullname { get; }
		public string Email { get; }
		public string Position { get; }
		public string EmployeeType { get; }
		public decimal GrossContractSalary { get; }
		public decimal InsuranceSalary { get; }
		public int StandardWorkingDays { get; }
		public int ActualWorkingDays { get; }
		public decimal ActualGrossSalary { get; }
		public decimal TotalMonthlyIncome { get; }
		public decimal TaxableIncome { get; }
		public decimal EmployeeSocialInsurance { get; }
		public decimal EmployeeHealthcareInsurance { get; }
		public decimal EmployeeUnemploymentInsurance { get; }
		public decimal EmployeeUnionFee { get; }
		public decimal EmployerSocialInsurance { get; }
		public decimal EmployerHealthcareInsurance { get; }
		public decimal EmployerUnemploymentInsurance { get; }
		public decimal EmployerUnionFee { get; }
		public decimal PersonalDeduction { get; }
		public int NumberOfDependants { get; }
		public decimal DependantDeduction { get; }
		public decimal AssessableIncome { get; }
		public decimal PIT { get; }
		public decimal NetIncome { get; }
		public decimal TotalSalaryCost { get; }
		public decimal NetPayment { get; }
		public decimal PaymentAdvance { get; }

		[JsonIgnore]
		public TaxableAllowance[] TaxableAllowances { get; }
		public decimal TaxableAnnualLeave { get; }
		public decimal Taxable13MonthSalary { get; set; }
		public decimal TaxableOthers { get; set; }
		public decimal NonTaxableAllowances { get; }
		public decimal AdjustmentDeduction { get; }
		public decimal AdjustmentAddition { get; }
	}
}
