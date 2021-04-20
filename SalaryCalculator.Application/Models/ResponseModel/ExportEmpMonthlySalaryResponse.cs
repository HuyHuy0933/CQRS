using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using SalaryCalculator.Application.Common.Converters;
using SalaryCalculator.Core.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SalaryCalculator.Application.Models.ResponseModel
{
	public class ExportEmpMonthlySalaryResponse
	{
		public byte[] CsvSalaries { get; set; }
	}

	public sealed class ExportEmpMonthlySalaryMap : ClassMap<ExportEmpMonthlySalary>
	{
		public ExportEmpMonthlySalaryMap()
		{
			AutoMap(CultureInfo.InvariantCulture);
			Map(x => x.No).Name("No.").Index(0);
			Map(x => x.Fullname).Name("Fullname").Index(1);
			Map(x => x.Email).Name("Email").Index(2);
			Map(x => x.Position).Name("Position").Index(3);
			Map(x => x.EmployeeType).Name("Type").Index(4);
			Map(x => x.GrossContractSalary).Name("Gross contract salary").Index(5).TypeConverter<SalaryConverter>();
			Map(x => x.InsuranceSalary).Name("Insurance salary").Index(6).TypeConverter<SalaryConverter>();
			Map(x => x.StandardWorkingDays).Name("Standard working days").Index(7).TypeConverter<SalaryConverter>();
			Map(x => x.ActualWorkingDays).Name("Actual working days").Index(8).TypeConverter<SalaryConverter>();
			Map(x => x.ActualGrossSalary).Name("Actual gross salary").Index(9).TypeConverter<SalaryConverter>();
			Map(x => x.NonTaxableAllowances).Name("Non taxable allowances").Index(10).TypeConverter<SalaryConverter>();
			Map(x => x.TaxableAnnualLeave).Name("Taxable annual leave").Index(11).TypeConverter<SalaryConverter>();
			Map(x => x.Taxable13MonthSalary).Name("Taxable 13th month salary").Index(12).TypeConverter<SalaryConverter>();
			Map(x => x.TaxableOthers).Name("Taxable others").Index(13).TypeConverter<SalaryConverter>();
			Map(x => x.TotalMonthlyIncome).Name("Total monthly income").Index(14).TypeConverter<SalaryConverter>();
			Map(x => x.TaxableIncome).Name("Taxable income").Index(15).TypeConverter<SalaryConverter>();
			Map(x => x.EmployeeSocialInsurance).Name("Employee social insurance").Index(16).TypeConverter<SalaryConverter>();
			Map(x => x.EmployeeHealthcareInsurance).Name("Employee healthcare insurance").Index(17).TypeConverter<SalaryConverter>();
			Map(x => x.EmployeeUnemploymentInsurance).Name("Employee unemployment insurance").Index(18).TypeConverter<SalaryConverter>();
			Map(x => x.EmployeeUnionFee).Name("Employee union fee").Index(19).TypeConverter<SalaryConverter>();
			Map(x => x.EmployerSocialInsurance).Name("Employer social insurance").Index(20).TypeConverter<SalaryConverter>();
			Map(x => x.EmployerHealthcareInsurance).Name("Employer healthcare insurance").Index(21).TypeConverter<SalaryConverter>();
			Map(x => x.EmployerUnemploymentInsurance).Name("Employer unemployment insurance").Index(22).TypeConverter<SalaryConverter>();
			Map(x => x.EmployerUnionFee).Name("Employer union fee").Index(23).TypeConverter<SalaryConverter>();
			Map(x => x.PersonalDeduction).Name("Personal deduction").Index(24).TypeConverter<SalaryConverter>();
			Map(x => x.NumberOfDependants).Name("Number of dependants").Index(25).TypeConverter<SalaryConverter>();
			Map(x => x.DependantDeduction).Name("Dependant deduction").Index(26).TypeConverter<SalaryConverter>();
			Map(x => x.AssessableIncome).Name("Assessable income").Index(27).TypeConverter<SalaryConverter>();
			Map(x => x.PIT).Name("PIT").Index(28).TypeConverter<SalaryConverter>();
			Map(x => x.NetIncome).Name("Net income").Index(29).TypeConverter<SalaryConverter>();
			Map(x => x.TotalSalaryCost).Name("Total salary cost").Index(30).TypeConverter<SalaryConverter>();
			Map(x => x.PaymentAdvance).Name("Payment advance").Index(31).TypeConverter<SalaryConverter>();
			Map(x => x.AdjustmentDeduction).Name("Adjustment deduction").Index(32).TypeConverter<SalaryConverter>();
			Map(x => x.AdjustmentAddition).Name("Adjustment addition").Index(33).TypeConverter<SalaryConverter>();
			Map(x => x.NetPayment).Name("Net payment").Index(34).TypeConverter<SalaryConverter>();
		}
	}
	public class ExportEmpMonthlySalary
	{
		public string No { get; set; }
		public string Fullname { get; set; }
		public string Email { get; set; }
		public string Position { get; set; }
		public string EmployeeType { get; set; }
		public decimal GrossContractSalary { get; set; }
		public decimal InsuranceSalary { get; set; }
		public int StandardWorkingDays { get; set; }
		public int ActualWorkingDays { get; set; }
		public decimal ActualGrossSalary { get; set; }
		public decimal NonTaxableAllowances { get; set; }
		public decimal TaxableAnnualLeave { get; set; }
		public decimal Taxable13MonthSalary { get; set; }
		public decimal TaxableOthers { get; set; }
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
		public decimal AdjustmentDeduction { get; set; }
		public decimal AdjustmentAddition { get; set; }

	}
}
