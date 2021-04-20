using System;

namespace SalaryCalculator.Core.Domain
{
	public class SalaryConfig
	{
		
		public SalaryConfig(
			Guid id,
			Money commonMinimumWage,
			Money regionalMinimumWage,
			decimal coefficientSocialCare,
			decimal employerSocialInsuranceRate,
			decimal employeeSocialInsuranceRate,
			decimal healthCareInsuranceEmployerRate,
			decimal healthCareInsuranceEmployeeRate,
			decimal unemploymentInsuranceEmployeeRate,
			decimal unemploymentInsuranceEmployerRate,
			int minimumNonWorkingDay,
			decimal employeeUnionFeeRate,
			decimal employerUnionFeeRate,
			decimal maximumUnionFeeRate,
			Money personalDeduction,
			Money dependantDeduction,
			ProgressiveTaxRateLookUpTable progressiveTaxRateLookUpTable,
			decimal defaultProbationTaxRate, 
			bool isInsurancePaidFullSalary, 
			Money insurancePaidAmount)
		{
			Id = id;
			CommonMinimumWage = commonMinimumWage;
			RegionalMinimumWage = regionalMinimumWage;
			CoefficientSocialCare = coefficientSocialCare;
			EmployerSocialInsuranceRate = employerSocialInsuranceRate / 100;
			EmployeeSocialInsuranceRate = employeeSocialInsuranceRate / 100;
			EmployerHealthCareInsuranceRate = healthCareInsuranceEmployerRate / 100;
			EmployeeHealthCareInsuranceRate = healthCareInsuranceEmployeeRate / 100;
			EmployeeUnemploymentInsuranceRate = unemploymentInsuranceEmployeeRate / 100;
			EmployerUnemploymentInsuranceRate = unemploymentInsuranceEmployerRate / 100;
			MinimumNonWorkingDay = minimumNonWorkingDay;
			EmployeeUnionFeeRate = employeeUnionFeeRate / 100;
			EmployerUnionFeeRate = employerUnionFeeRate / 100;
			MaximumUnionFeeRate = maximumUnionFeeRate / 100;
			PersonalDeduction = personalDeduction;
			DependantDeduction = dependantDeduction;
			ProgressiveTaxRateLookUpTable = progressiveTaxRateLookUpTable;
			IsInsurancePaidFullSalary = isInsurancePaidFullSalary;
			InsurancePaidAmount = insurancePaidAmount;
			DefaultProbationTaxRate = defaultProbationTaxRate / 100;
		}

		public SalaryConfig(
			Guid id,
			Money commonMinimumWage,
			Money regionalMinimumWage,
			decimal coefficientSocialCare,
			int minimumNonWorkingDay,
			decimal employerSocialInsuranceRate,
			decimal employeeSocialInsuranceRate,
			decimal healthCareInsuranceEmployerRate,
			decimal healthCareInsuranceEmployeeRate,
			decimal unemploymentInsuranceEmployeeRate,
			decimal unemploymentInsuranceEmployerRate,
			decimal foreignEmployerSocialInsuranceRate,
			decimal foreignEmployeeSocialInsuranceRate,
			decimal foreignHealthCareInsuranceEmployerRate,
			decimal foreignHealthCareInsuranceEmployeeRate,
			decimal foreignUnemploymentInsuranceEmployeeRate,
			decimal foreignUnemploymentInsuranceEmployerRate,
			decimal employeeUnionFeeRate,
			decimal employerUnionFeeRate,
			decimal maximumUnionFeeRate,
			Money personalDeduction,
			Money dependantDeduction,
			ProgressiveTaxRateLookUpTable progressiveTaxRateLookUpTable,
			decimal defaultProbationTaxRate,
			bool isInsurancePaidFullSalary,
			Money insurancePaidAmount)
		{
			Id = id;
			CommonMinimumWage = commonMinimumWage;
			RegionalMinimumWage = regionalMinimumWage;
			CoefficientSocialCare = coefficientSocialCare;
			MinimumNonWorkingDay = minimumNonWorkingDay;
			EmployerSocialInsuranceRate = employerSocialInsuranceRate / 100;
			EmployeeSocialInsuranceRate = employeeSocialInsuranceRate / 100;
			EmployerHealthCareInsuranceRate = healthCareInsuranceEmployerRate / 100;
			EmployeeHealthCareInsuranceRate = healthCareInsuranceEmployeeRate / 100;
			EmployeeUnemploymentInsuranceRate = unemploymentInsuranceEmployeeRate / 100;
			EmployerUnemploymentInsuranceRate = unemploymentInsuranceEmployerRate / 100;
			ForeignEmployerSocialInsuranceRate = foreignEmployerSocialInsuranceRate / 100;
			ForeignEmployeeSocialInsuranceRate = foreignEmployeeSocialInsuranceRate / 100;
			ForeignEmployerHealthCareInsuranceRate = foreignHealthCareInsuranceEmployerRate / 100;
			ForeignEmployeeHealthCareInsuranceRate = foreignHealthCareInsuranceEmployeeRate / 100;
			ForeignEmployeeUnemploymentInsuranceRate = foreignUnemploymentInsuranceEmployeeRate / 100;
			ForeignEmployerUnemploymentInsuranceRate = foreignUnemploymentInsuranceEmployerRate / 100;
			EmployeeUnionFeeRate = employeeUnionFeeRate / 100;
			EmployerUnionFeeRate = employerUnionFeeRate / 100;
			MaximumUnionFeeRate = maximumUnionFeeRate / 100;
			PersonalDeduction = personalDeduction;
			DependantDeduction = dependantDeduction;
			ProgressiveTaxRateLookUpTable = progressiveTaxRateLookUpTable;
			IsInsurancePaidFullSalary = isInsurancePaidFullSalary;
			InsurancePaidAmount = insurancePaidAmount;
			DefaultProbationTaxRate = defaultProbationTaxRate / 100;
		}

		public Guid Id { get; }
		public Money CommonMinimumWage { get; }
		public Money RegionalMinimumWage { get; }
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
		public decimal DefaultProbationTaxRate { get; }
		public ProgressiveTaxRateLookUpTable ProgressiveTaxRateLookUpTable { get; }
		public decimal EmployeeUnionFeeRate { get; }
		public decimal EmployerUnionFeeRate { get; }
		public decimal MaximumUnionFeeRate { get; }
		public Money PersonalDeduction { get; }
		public Money DependantDeduction { get; }
		public int MinimumNonWorkingDay { get; }
		public bool IsInsurancePaidFullSalary { get; }
		public Money InsurancePaidAmount { get; }
	}
}