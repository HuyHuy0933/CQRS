using System;
using System.Collections.Generic;
using System.Text;
using SalaryCalculator.Infra.Persistent.Entities;

namespace SalaryCalculator.Infra.Persistent.Entities
{
	public class SalarySetting : EntityBase
	{
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
        public decimal CommonMinimumWage { get; set; }
        public decimal RegionalMinimumWage { get; set; }
        public decimal PersonalDeduction { get; set; }
        public decimal DependantDeduction { get; set; }
        public int MinimumNonWorkingDay { get; set; }
        public bool IsInsurancePaidFullSalary { get; set; }
        public decimal InsurancePaidAmount { get; set; }

        public Guid CurrencyId { get; set; }
        public SalaryCurrency SalaryCurrency { get; set; }
	}
}