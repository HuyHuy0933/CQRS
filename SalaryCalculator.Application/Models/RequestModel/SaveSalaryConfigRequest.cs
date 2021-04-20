using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using SalaryCalculator.Application.Models.ResponseModel;
using SalaryCalculator.Core.Domain;
using SalaryCalculator.Core.Framework;

namespace SalaryCalculator.Application.Models.RequestModel
{
	public class SaveSalaryConfigRequest : IRequest<Result<SaveSalaryConfigResponse>>
	{
		public SalarySettingRequest SalarySetting { get; set; }
		public List<ProgressiveTaxRateRequest> ProgressiveTaxRates { get; set; }
		public string YearMonth { get; set; }
    }

	public class SalarySettingRequest
	{
		public Guid Id { get; set; }
		public decimal CommonMinimumWage { get; set; }
		public decimal RegionalMinimumWage { get; set; }
		public decimal CoefficientSocialCare { get; set; }
		public decimal EmployerSocialInsuranceRate { get; set; }
		public decimal EmployeeSocialInsuranceRate { get; set; }
		public decimal EmployerHealthCareInsuranceRate { get; set; }
		public decimal EmployeeHealthCareInsuranceRate { get; set; }
		public decimal EmployeeUnemploymentInsuranceRate { get; set; }
		public decimal EmployerUnemploymentInsuranceRate { get; set; }
		public decimal ForeignEmployerSocialInsuranceRate { get; set; }
		public decimal ForeignEmployeeSocialInsuranceRate { get; set; }
		public decimal ForeignEmployerHealthCareInsuranceRate { get; set; }
		public decimal ForeignEmployeeHealthCareInsuranceRate { get; set; }
		public decimal ForeignEmployeeUnemploymentInsuranceRate { get; set; }
		public decimal ForeignEmployerUnemploymentInsuranceRate { get; set; }
		public decimal DefaultProbationTaxRate { get; set; }
		public decimal EmployeeUnionFeeRate { get; set; }
		public decimal EmployerUnionFeeRate { get; set; }
		public decimal MaximumUnionFeeRate { get; set; }
		public decimal PersonalDeduction { get; set; }
		public decimal DependantDeduction { get; set; }
		public string Currency { get; set; }
		public int MinimumNonWorkingDay { get; set; }
		public bool IsInsurancePaidFullSalary { get; set; }
		public decimal InsurancePaidAmount { get; set; }
	}

	public class ProgressiveTaxRateRequest
	{
		public decimal LowerBound { get; set; }
		public decimal UpperBound { get; set; }
		public ProgressiveTaxRateLevel TaxRateLevel { get; set; }
		public decimal Rate { get; set; }
	}
}
