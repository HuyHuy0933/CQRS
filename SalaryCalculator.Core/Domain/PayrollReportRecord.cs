using SalaryCalculator.Core.ValueObjects;
using System.Linq;

namespace SalaryCalculator.Core.Domain
{
	public class PayrollReportRecord
	{
		public PayrollReportRecord(
			EmployeeMonthlyRecord employeeMonthlyRecord,
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
			decimal netPayment)
		{
			Fullname = employeeMonthlyRecord.Name;
			Email = employeeMonthlyRecord.Email;
			Position = employeeMonthlyRecord.Position;
			EmployeeType = employeeMonthlyRecord.EmployeeType;
			GrossContractSalary = employeeMonthlyRecord.GrossContractedSalary.Amount;
			InsuranceSalary = insuranceSalary;
			StandardWorkingDays = employeeMonthlyRecord.StandardWorkingDays;
			ActualWorkingDays = employeeMonthlyRecord.WorkingDays + employeeMonthlyRecord.ProbationWorkingDays;
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
			NumberOfDependants = employeeMonthlyRecord.NumberOfDependants;
			DependantDeduction = dependantDeduction;
			AssessableIncome = assessableIncome;
			PIT = pit;
			PaymentAdvance = employeeMonthlyRecord.PaymentAdvance.Amount.Value;
			TaxableAllowances = employeeMonthlyRecord.TaxableAllowances;
			NonTaxableAllowances = employeeMonthlyRecord.NonTaxableAllowances;
			NetIncome = netIncome;
			TotalSalaryCost = totalSalaryCost;
			NetPayment = netPayment;
			AdjustmentAddition = employeeMonthlyRecord.AdjustmentAdditions.Select(_ => _.Amount.Value).Sum();
			AdjustmentDeduction = employeeMonthlyRecord.AdjustmentDeduction.Select(_ => _.Amount.Value).Sum();
		}

		public PayrollReportRecord(
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
			NonTaxableAllowance[] nonTaxableAllowances,
			decimal netPayment,
			decimal adjustmentDeduction,
			decimal adjustmentAddition
			)
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
			NonTaxableAllowances = nonTaxableAllowances;
			TaxableAllowances = taxableAllowances;
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
			NetIncome = netIncome;
			TotalSalaryCost = totalSalaryCost;
			NetPayment = netPayment;
			AdjustmentDeduction = adjustmentDeduction;
			AdjustmentAddition = adjustmentAddition;
			PaymentAdvance = paymentAdvance;
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
		public TaxableAllowance[] TaxableAllowances { get; }
		public NonTaxableAllowance[] NonTaxableAllowances { get; }
		public decimal AdjustmentDeduction { get; }
		public decimal AdjustmentAddition { get; }
	}
}