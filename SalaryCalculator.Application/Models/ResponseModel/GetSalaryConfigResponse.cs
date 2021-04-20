using System;
using System.Collections.Generic;
using System.Text;
using SalaryCalculator.Core.Domain;

namespace SalaryCalculator.Application.Models.ResponseModel
{
	public class GetSalaryConfigResponse
	{
		public GetSalaryConfigResponse(
			SalarySettingResponse salarySetting,
			List<ProgressiveTaxRateResponse> progressiveTaxRates)
		{
			SalarySetting = salarySetting;
			ProgressiveTaxRates = progressiveTaxRates;
		}

		public SalarySettingResponse SalarySetting { get; }
		
		public List<ProgressiveTaxRateResponse> ProgressiveTaxRates { get; }
		
	}

	public class SalarySettingResponse
	{
		public SalarySettingResponse(
			decimal commonMinimumWage,
			decimal regionalMinimumWage,
			decimal coefficientSocialCare,
			decimal employerSocialInsuranceRate,
			decimal employeeSocialInsuranceRate,
			decimal employerHealthCareInsuranceRate,
			decimal employeeHealthCareInsuranceRate,
			decimal employeeUnemploymentInsuranceRate,
			decimal employerUnemploymentInsuranceRate,
			decimal foreignEmployerSocialInsuranceRate,
			decimal foreignEmployeeSocialInsuranceRate,
			decimal foreignHealthCareInsuranceEmployerRate,
			decimal foreignHealthCareInsuranceEmployeeRate,
			decimal foreignUnemploymentInsuranceEmployeeRate,
			decimal foreignUnemploymentInsuranceEmployerRate,
			decimal defaultProbationTaxRate,
			decimal employeeUnionFeeRate,
			decimal employerUnionFeeRate,
			decimal maximumUnionFeeRate,
			decimal personalDeduction,
			decimal dependantDeduction,
			string currency,
			int minimumNonWorkingDay,
			bool isInsurancePaidFullSalary,
			decimal insurancePaidAmount,
			Guid id)
		{
			Id = id;
			CommonMinimumWage = commonMinimumWage;
			RegionalMinimumWage = regionalMinimumWage;
			CoefficientSocialCare = coefficientSocialCare;
			EmployerSocialInsuranceRate = employerSocialInsuranceRate * 100;
			EmployeeSocialInsuranceRate = employeeSocialInsuranceRate * 100;
			EmployerHealthCareInsuranceRate = employerHealthCareInsuranceRate * 100;
			EmployeeHealthCareInsuranceRate = employeeHealthCareInsuranceRate * 100;
			EmployeeUnemploymentInsuranceRate = employeeUnemploymentInsuranceRate * 100;
			EmployerUnemploymentInsuranceRate = employerUnemploymentInsuranceRate * 100;
			ForeignEmployerSocialInsuranceRate = foreignEmployerSocialInsuranceRate * 100;
			ForeignEmployeeSocialInsuranceRate = foreignEmployeeSocialInsuranceRate * 100;
			ForeignEmployerHealthCareInsuranceRate = foreignHealthCareInsuranceEmployerRate * 100;
			ForeignEmployeeHealthCareInsuranceRate = foreignHealthCareInsuranceEmployeeRate * 100;
			ForeignEmployeeUnemploymentInsuranceRate = foreignUnemploymentInsuranceEmployeeRate * 100;
			ForeignEmployerUnemploymentInsuranceRate = foreignUnemploymentInsuranceEmployerRate * 100;
			DefaultProbationTaxRate = defaultProbationTaxRate * 100;
			EmployeeUnionFeeRate = employeeUnionFeeRate * 100;
			EmployerUnionFeeRate = employerUnionFeeRate * 100;
			MaximumUnionFeeRate = maximumUnionFeeRate * 100;
			PersonalDeduction = personalDeduction;
			DependantDeduction = dependantDeduction;
			Currency = currency;
			MinimumNonWorkingDay = minimumNonWorkingDay;
			IsInsurancePaidFullSalary = isInsurancePaidFullSalary;
			InsurancePaidAmount = insurancePaidAmount;
		}

		public Guid Id { get; }
		public decimal CommonMinimumWage { get; }
		public decimal RegionalMinimumWage { get; }
		public decimal CoefficientSocialCare { get; }
		public decimal EmployerSocialInsuranceRate { get; }
		public decimal EmployeeSocialInsuranceRate { get; }
		public decimal EmployerHealthCareInsuranceRate { get; }
		public decimal EmployeeHealthCareInsuranceRate { get; }
		public decimal EmployeeUnemploymentInsuranceRate { get; }
		public decimal EmployerUnemploymentInsuranceRate { get; }
		public decimal ForeignEmployerSocialInsuranceRate { get; set; }
		public decimal ForeignEmployeeSocialInsuranceRate { get; set; }
		public decimal ForeignEmployerHealthCareInsuranceRate { get; set; }
		public decimal ForeignEmployeeHealthCareInsuranceRate { get; set; }
		public decimal ForeignEmployeeUnemploymentInsuranceRate { get; set; }
		public decimal ForeignEmployerUnemploymentInsuranceRate { get; set; }
		public decimal EmployeeUnionFeeRate { get; }
		public decimal EmployerUnionFeeRate { get; }
		public decimal MaximumUnionFeeRate { get; }
		public decimal DefaultProbationTaxRate { get; }
		public decimal PersonalDeduction { get; }
		public decimal DependantDeduction { get; }
		public string Currency { get; }
		public int MinimumNonWorkingDay { get; }
		public bool IsInsurancePaidFullSalary { get; }
		public decimal InsurancePaidAmount { get; }
	}

	public class ProgressiveTaxRateResponse
	{
		public ProgressiveTaxRateResponse(
			decimal lowerBound,
			decimal upperBound,
			ProgressiveTaxRateLevel taxRateLevel,
			decimal rate)
		{
			LowerBound = lowerBound;
			UpperBound = upperBound;
			TaxRateLevel = taxRateLevel;
			Rate = rate * 100;
		}

		public decimal LowerBound { get; }
		public decimal UpperBound { get;  }
		public ProgressiveTaxRateLevel TaxRateLevel { get; }
		public decimal Rate { get; }
	}
}
