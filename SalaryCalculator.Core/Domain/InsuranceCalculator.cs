using SalaryCalculator.Core.Common;
using System;
using System.Linq;

namespace SalaryCalculator.Core.Domain
{
	public static class InsuranceCalculator
	{
		public static InsuranceSalary CalculateInsuranceSalary(
			SalaryConfig salaryConfig,
			EmployeeMonthlyRecord employeeMonthlyRecord)
		{
			var insurancePaidAmount = salaryConfig.IsInsurancePaidFullSalary
				? employeeMonthlyRecord.GrossContractedSalary.Amount
				: salaryConfig.InsurancePaidAmount;

			var actualPaidAmount = employeeMonthlyRecord.IsEligibleForSocialInsurance() || employeeMonthlyRecord.IsOnProbation()
				? insurancePaidAmount
				: Money.ZeroVND;

			return new InsuranceSalary(actualPaidAmount);
		}

		public static EmployerSocialInsurance CalculateEmployerSocialInsurance(
			SalaryConfig salaryConfig,
			EmployeeMonthlyRecord employeeMonthlyRecord,
			InsuranceSalary insuranceSalary)
		{
			return new EmployerSocialInsurance(Calculate(
				employeeMonthlyRecord: employeeMonthlyRecord,
				minimumWage: salaryConfig.CommonMinimumWage,
				coefficientSocialCare: salaryConfig.CoefficientSocialCare,
				rateOfDeductionOfCompulsoryInsurance: employeeMonthlyRecord.IsForeigner
					? salaryConfig.ForeignEmployerSocialInsuranceRate : salaryConfig.EmployerSocialInsuranceRate,
				amount: insuranceSalary.Amount));
		}

		public static EmployeeSocialInsurance CalculateEmployeeSocialInsurance(
			SalaryConfig salaryConfig,
			EmployeeMonthlyRecord employeeMonthlyRecord,
			InsuranceSalary insuranceSalary)
		{
			return new EmployeeSocialInsurance(Calculate(
				employeeMonthlyRecord: employeeMonthlyRecord,
				minimumWage: salaryConfig.CommonMinimumWage,
				coefficientSocialCare: salaryConfig.CoefficientSocialCare,
				rateOfDeductionOfCompulsoryInsurance: employeeMonthlyRecord.IsForeigner
					? salaryConfig.ForeignEmployeeSocialInsuranceRate : salaryConfig.EmployeeSocialInsuranceRate,
				amount: insuranceSalary.Amount));
		}

		public static EmployeeHealthCareInsurance CalculateEmployeeHealthCareInsurance(
			SalaryConfig salaryConfig,
			EmployeeMonthlyRecord employeeMonthlyRecord,
			InsuranceSalary insuranceSalary)
		{
			return new EmployeeHealthCareInsurance(Calculate(
				employeeMonthlyRecord: employeeMonthlyRecord,
				minimumWage: salaryConfig.CommonMinimumWage,
				coefficientSocialCare: salaryConfig.CoefficientSocialCare,
				rateOfDeductionOfCompulsoryInsurance: employeeMonthlyRecord.IsForeigner
					? salaryConfig.ForeignEmployeeHealthCareInsuranceRate : salaryConfig.EmployeeHealthCareInsuranceRate,
				amount: insuranceSalary.Amount));
		}

		public static EmployerHealthcareInsurance CalculateEmployerHealthCareInsurance(
			SalaryConfig salaryConfig,
			EmployeeMonthlyRecord employeeMonthlyRecord,
			InsuranceSalary insuranceSalary)
		{
			return new EmployerHealthcareInsurance(Calculate(
				employeeMonthlyRecord: employeeMonthlyRecord,
				minimumWage: salaryConfig.CommonMinimumWage,
				coefficientSocialCare: salaryConfig.CoefficientSocialCare,
				rateOfDeductionOfCompulsoryInsurance: employeeMonthlyRecord.IsForeigner
					? salaryConfig.ForeignEmployerHealthCareInsuranceRate : salaryConfig.EmployerHealthCareInsuranceRate,
				amount: insuranceSalary.Amount));
		}

		public static EmployeeUnemploymentInsurance CalculateEmployeeUnemploymentInsurance(
			SalaryConfig salaryConfig,
			EmployeeMonthlyRecord employeeMonthlyRecord,
			InsuranceSalary insuranceSalary)
		{
			return new EmployeeUnemploymentInsurance(Calculate(
				employeeMonthlyRecord: employeeMonthlyRecord,
				minimumWage: salaryConfig.RegionalMinimumWage,
				coefficientSocialCare: salaryConfig.CoefficientSocialCare,
				rateOfDeductionOfCompulsoryInsurance: employeeMonthlyRecord.IsForeigner
					? salaryConfig.ForeignEmployeeUnemploymentInsuranceRate : salaryConfig.EmployeeUnemploymentInsuranceRate,
				amount: insuranceSalary.Amount));
		}

		public static EmployerUnemploymentInsurance CalculateEmployerUnemploymentInsurance(
			SalaryConfig salaryConfig,
			EmployeeMonthlyRecord employeeMonthlyRecord,
			InsuranceSalary insuranceSalary)
		{
			return new EmployerUnemploymentInsurance(Calculate(
				employeeMonthlyRecord: employeeMonthlyRecord,
				minimumWage: salaryConfig.RegionalMinimumWage,
				coefficientSocialCare: salaryConfig.CoefficientSocialCare,
				rateOfDeductionOfCompulsoryInsurance: employeeMonthlyRecord.IsForeigner
					? salaryConfig.ForeignEmployerUnemploymentInsuranceRate : salaryConfig.EmployerUnemploymentInsuranceRate,
				amount: insuranceSalary.Amount));
		}

		public static EmployeeUnionFee CalculateEmployeeUnionFee(
			SalaryConfig salaryConfig,
			EmployeeMonthlyRecord employeeMonthlyRecord,
			InsuranceSalary insuranceSalary)
		{
			if (employeeMonthlyRecord.InUnion is false)
			{
				return new EmployeeUnionFee(Money.ZeroVND);
			}

			return new EmployeeUnionFee(Calculate(
				employeeMonthlyRecord: employeeMonthlyRecord,
				minimumWage: salaryConfig.CommonMinimumWage,
				coefficientSocialCare: salaryConfig.MaximumUnionFeeRate,
				rateOfDeductionOfCompulsoryInsurance: 1m,
				amount: Calculate(
					employeeMonthlyRecord,
					salaryConfig.CommonMinimumWage,
					salaryConfig.CoefficientSocialCare,
					salaryConfig.EmployeeUnionFeeRate,
					insuranceSalary.Amount)));
		}

		public static EmployerUnionFee CalculateEmployerUnionFee(
			SalaryConfig salaryConfig,
			EmployeeMonthlyRecord employeeMonthlyRecord,
			InsuranceSalary insuranceSalary)
		{
			var unionEmployerRate = employeeMonthlyRecord.InUnion
				? salaryConfig.EmployerUnionFeeRate - salaryConfig.EmployeeUnionFeeRate
				: salaryConfig.EmployerUnionFeeRate;

			return new EmployerUnionFee(Calculate(
				employeeMonthlyRecord: employeeMonthlyRecord,
				minimumWage: salaryConfig.CommonMinimumWage,
				coefficientSocialCare: salaryConfig.CoefficientSocialCare,
				rateOfDeductionOfCompulsoryInsurance: unionEmployerRate,
				amount: insuranceSalary.Amount));
		}

		private static Money Calculate(
			EmployeeMonthlyRecord employeeMonthlyRecord,
			Money minimumWage,
			decimal coefficientSocialCare,
			decimal rateOfDeductionOfCompulsoryInsurance,
			Money amount)
		{
			if (employeeMonthlyRecord.IsEligibleForSocialInsurance() is false || employeeMonthlyRecord.IsOnProbation())
			{
				return Money.ZeroVND;
			}

			var specifiedAmount = minimumWage * coefficientSocialCare;

			if (amount > specifiedAmount)
			{
				return specifiedAmount * rateOfDeductionOfCompulsoryInsurance;
			}

			return amount * rateOfDeductionOfCompulsoryInsurance;
		}
	}
}