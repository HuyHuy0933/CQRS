using System.Linq;
using SalaryCalculator.Core.Domain;

namespace SalaryCalculator.Core
{
	public static class Main
	{
		public static PayrollReportRecord[] GeneratePayRollRecord(
			EmployeeMonthlyRecord[] employeeMonthlyRecords,
			SalaryConfig salaryConfig)
		{
			return employeeMonthlyRecords.Select(employeeMonthlyRecord =>
			{
				var insuranceSalary = InsuranceCalculator.CalculateInsuranceSalary(
					salaryConfig: salaryConfig,
					employeeMonthlyRecord: employeeMonthlyRecord);

				var actualGrossSalary = new ActualGrossSalary(
					grossContractedSalary: employeeMonthlyRecord.GrossContractedSalary,
					standardWorkingDays: employeeMonthlyRecord.StandardWorkingDays,
					probationWorkingDays: employeeMonthlyRecord.ProbationWorkingDays,
					workingDays: employeeMonthlyRecord.WorkingDays);

				var employeeSocialInsurance = InsuranceCalculator.CalculateEmployeeSocialInsurance(
					salaryConfig: salaryConfig,
					employeeMonthlyRecord: employeeMonthlyRecord,
					insuranceSalary: insuranceSalary);
				var employeeUnemploymentInsurance = InsuranceCalculator.CalculateEmployeeUnemploymentInsurance(
					salaryConfig: salaryConfig,
					employeeMonthlyRecord: employeeMonthlyRecord,
					insuranceSalary: insuranceSalary);
				var employeeHealthcareInsurance = InsuranceCalculator.CalculateEmployeeHealthCareInsurance(
					salaryConfig: salaryConfig,
					employeeMonthlyRecord: employeeMonthlyRecord,
					insuranceSalary: insuranceSalary);
				var employeeUnionFee = InsuranceCalculator.CalculateEmployeeUnionFee(salaryConfig: salaryConfig,
					employeeMonthlyRecord: employeeMonthlyRecord,
					insuranceSalary: insuranceSalary);

				var employerSocialInsurance = InsuranceCalculator.CalculateEmployerSocialInsurance(
					salaryConfig: salaryConfig,
					employeeMonthlyRecord: employeeMonthlyRecord,
					insuranceSalary: insuranceSalary);
				var employerUnemploymentInsurance = InsuranceCalculator.CalculateEmployerUnemploymentInsurance(
					salaryConfig: salaryConfig,
					employeeMonthlyRecord: employeeMonthlyRecord,
					insuranceSalary: insuranceSalary);
				var employerHealthcareInsurance = InsuranceCalculator.CalculateEmployerHealthCareInsurance(
					salaryConfig: salaryConfig,
					employeeMonthlyRecord: employeeMonthlyRecord,
					insuranceSalary: insuranceSalary);
				var employerUnionFee = InsuranceCalculator.CalculateEmployerUnionFee(salaryConfig: salaryConfig,
					employeeMonthlyRecord: employeeMonthlyRecord,
					insuranceSalary: insuranceSalary);

				var taxableIncome = new TaxableIncome(actualGrossSalary, employeeMonthlyRecord.TaxableAllowances);

				var assessableIncome = new AssessableIncome(
					taxableIncome,
					employeeSocialInsurance: employeeSocialInsurance,
					employeeHealthCareInsurance: employeeHealthcareInsurance,
					employeeUnemploymentInsurance: employeeUnemploymentInsurance,
					employeeUnionFee: employeeUnionFee,
					totalDeduction: new TotalDeduction(employeeMonthlyRecord: employeeMonthlyRecord,
						salaryConfig: salaryConfig));

				var pit = employeeMonthlyRecord.IsOnProbation()
					? new PersonalIncomeTax((assessableIncome.Amount * salaryConfig.DefaultProbationTaxRate).Round())
					: salaryConfig.ProgressiveTaxRateLookUpTable[assessableIncome];

				var totalMonthlyIncome = new TotalMonthlyIncome(
					actualGrossSalary: actualGrossSalary,
					taxableAllowances: employeeMonthlyRecord.TaxableAllowances,
					nonTaxableAllowances: employeeMonthlyRecord.NonTaxableAllowances);

				var netIncome = new NetIncome(
					totalMonthlyIncome: totalMonthlyIncome,
					employeeHealthcareInsurance: employeeHealthcareInsurance,
					employeeSocialInsurance: employeeSocialInsurance,
					employeeUnemploymentInsurance: employeeUnemploymentInsurance,
					employeeUnionFee: employeeUnionFee,
					pit: pit);

				return new PayrollReportRecord(
					employeeMonthlyRecord: employeeMonthlyRecord,
					insuranceSalary: insuranceSalary.Amount,
					actualGrossSalary: actualGrossSalary.Amount,
					totalMonthlyIncome: totalMonthlyIncome.Amount,
					taxableIncome: taxableIncome.Amount,
					employeeSocialInsurance: employeeSocialInsurance.Amount,
					employeeHealthcareInsurance: employeeHealthcareInsurance.Amount,
					employeeUnemploymentInsurance: employeeUnemploymentInsurance.Amount,
					employeeUnionFee: employeeUnionFee.Amount,
					employerSocialInsurance: employerSocialInsurance.Amount,
					employerHealthcareInsurance: employerHealthcareInsurance.Amount,
					employerUnemploymentInsurance: employerUnemploymentInsurance.Amount,
					employerUnionFee: employerUnionFee.Amount,
					personalDeduction: salaryConfig.PersonalDeduction,
					dependantDeduction: salaryConfig.DependantDeduction * employeeMonthlyRecord.NumberOfDependants,
					assessableIncome: assessableIncome.Amount,
					netIncome: netIncome.Amount,
					pit: pit.Amount,
					totalSalaryCost: new TotalSalaryCost(
						totalMonthlyIncome: totalMonthlyIncome,
						employerSocialInsurance: employerSocialInsurance,
						employerHealthcareInsurance: employerHealthcareInsurance,
						employerUnemploymentInsurance: employerUnemploymentInsurance,
						employerUnionFee: employerUnionFee).Amount,
					netPayment: new NetPayment(
						netIncome: netIncome,
						paymentAdvance: employeeMonthlyRecord.PaymentAdvance,
						adjustmentAdditions: employeeMonthlyRecord.AdjustmentAdditions,
						adjustmentDeductions: employeeMonthlyRecord.AdjustmentDeduction
					).Amount);
			}).ToArray();
		}
	}
}