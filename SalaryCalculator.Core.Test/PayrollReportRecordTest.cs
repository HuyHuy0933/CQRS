using System;
using System.Linq;
using SalaryCalculator.Core.Common;
using SalaryCalculator.Core.Domain;
using SalaryCalculator.Core.ValueObjects;
using Xunit;

namespace SalaryCalculator.Core.Test
{
	/*
	 * Test are based on this file https://docs.google.com/spreadsheets/d/1TO6_3CBAD1GSrRUFkCmMCpyodbmtHxpyzQQUKZyUZPk/edit#gid=1217766354
	 */
	public class PayrollReportRecordTest
	{
		// TODO: Name the test
		[Fact]
		public void Test_Vietnam1()
		{
			// Arrange
			var employeeMonthlyRecords = new EmployeeMonthlyRecord[]
			{
				new EmployeeMonthlyRecord(
					name: "Huy Tran",
					position: "Full Stack Developer",
					email: "aa@gmail.com",
					workingDays: 22,
					standardWorkingDays: 22,
					inUnion: false,
					employeeType: "Permanent",
					numberOfDependants: 0,
					probationWorkingDays: 0,
					grossContractedSalary: new GrossContractedSalary(new Money(3_000_000m, Currency.VND)),
					taxableAllowances: Enumerable.Empty<TaxableAllowance>().ToArray(),
					nonTaxableAllowances: Enumerable.Empty<NonTaxableAllowance>().ToArray(),
					paymentAdvance: new PaymentAdvance(Money.ZeroVND),
					adjustmentAdditions: new AdjustmentAddition[] {new AdjustmentAddition(new Money(300_000m, Currency.VND))},
					adjustmentDeduction: Enumerable.Empty<AdjustmentDeduction>().ToArray(),
					isForeigner: false)
			};

			var salaryConfig = new SalaryConfig(
				id: Guid.NewGuid(),
				isInsurancePaidFullSalary: true,
				insurancePaidAmount: Money.ZeroVND,
				commonMinimumWage: new Money(1_490_000m, Currency.VND),
				regionalMinimumWage: new Money(4_729_400m, Currency.VND),
				coefficientSocialCare: 20m,
				employerSocialInsuranceRate: 17.5m,
				employeeSocialInsuranceRate: 8m,
				healthCareInsuranceEmployerRate: 3m,
				healthCareInsuranceEmployeeRate: 1.5m,
				unemploymentInsuranceEmployerRate: 1m,
				unemploymentInsuranceEmployeeRate: 1m,
				minimumNonWorkingDay: 14,
				employeeUnionFeeRate: 1m,
				employerUnionFeeRate: 2m,
				maximumUnionFeeRate: 10m,
				personalDeduction: new Money(11_000_000m, Currency.VND),
				dependantDeduction: new Money(4_400_000, Currency.VND),
				defaultProbationTaxRate: 10m,
				progressiveTaxRateLookUpTable: new ProgressiveTaxRateLookUpTable(
					new ProgressiveTaxRate[]
					{
						new ProgressiveTaxRate(
							lowerBound: new Money(0m, Currency.VND),
							upperBound: new Money(5_000_000m, Currency.VND),
							progressiveTaxRateLevel: ProgressiveTaxRateLevel.One,
							rate: 5),
						new ProgressiveTaxRate(
							lowerBound: new Money(5_000_001m, Currency.VND),
							upperBound: new Money(10_000_000m, Currency.VND),
							progressiveTaxRateLevel: ProgressiveTaxRateLevel.Two,
							rate: 10),
						new ProgressiveTaxRate(
							lowerBound: new Money(10_000_001m, Currency.VND),
							upperBound: new Money(18_000_000m, Currency.VND),
							progressiveTaxRateLevel: ProgressiveTaxRateLevel.Three,
							rate: 15),
						new ProgressiveTaxRate(
							lowerBound: new Money(18_000_001m, Currency.VND),
							upperBound: new Money(32_000_000m, Currency.VND),
							progressiveTaxRateLevel: ProgressiveTaxRateLevel.Four,
							rate: 20),
						new ProgressiveTaxRate(
							lowerBound: new Money(32_000_001m, Currency.VND),
							upperBound: new Money(52_000_000m, Currency.VND),
							progressiveTaxRateLevel: ProgressiveTaxRateLevel.Five,
							rate: 25),
						new ProgressiveTaxRate(
							lowerBound: new Money(52_000_001m, Currency.VND),
							upperBound: new Money(80_000_000m, Currency.VND),
							progressiveTaxRateLevel: ProgressiveTaxRateLevel.Six,
							rate: 30),
						new ProgressiveTaxRate(
							lowerBound: new Money(80_000_001m, Currency.VND),
							upperBound: new Money(decimal.MaxValue, Currency.VND),
							progressiveTaxRateLevel: ProgressiveTaxRateLevel.Seven,
							rate: 35),
					}
				)
			);

			// Act
			var payrollRecords = Main.GeneratePayRollRecord(
				employeeMonthlyRecords: employeeMonthlyRecords,
				salaryConfig: salaryConfig).First();

			// Assert
			Assert.Equal(3_000_000m, payrollRecords.InsuranceSalary);
			Assert.Equal(3_000_000m, payrollRecords.ActualGrossSalary);
			Assert.Equal(3_000_000m, payrollRecords.TotalMonthlyIncome);
			Assert.Equal(3_000_000m, payrollRecords.TaxableIncome);
			Assert.Equal(240_000m, payrollRecords.EmployeeSocialInsurance);
			Assert.Equal(45_000m, payrollRecords.EmployeeHealthcareInsurance);
			Assert.Equal(30_000m, payrollRecords.EmployeeUnemploymentInsurance);
			Assert.Equal(525_000m, payrollRecords.EmployerSocialInsurance);
			Assert.Equal(90_000m, payrollRecords.EmployerHealthcareInsurance);
			Assert.Equal(30_000m, payrollRecords.EmployerUnemploymentInsurance);
			Assert.Equal(60_000m, payrollRecords.EmployerUnionFee);
			Assert.Equal(11_000_000m, payrollRecords.PersonalDeduction);
			Assert.Equal(0, payrollRecords.AssessableIncome);
			Assert.Equal(2_685_000m, payrollRecords.NetIncome);
			Assert.Equal(3_705_000m, payrollRecords.TotalSalaryCost);
			Assert.Equal(2_985_000m, payrollRecords.NetPayment);
			
		}

		[Fact]
		public void Test_Vietnam2()
		{
			// Arrange
			var employeeMonthlyRecords = new EmployeeMonthlyRecord[]
			{
				new EmployeeMonthlyRecord(
					name: "Huy Tran",
					position: "Full Stack Developer",
					email: "aa@gmail.com",
					workingDays: 7,
					standardWorkingDays: 22,
					inUnion: false,
					employeeType: "Permanent",
					numberOfDependants: 0,
					probationWorkingDays: 0,
					grossContractedSalary: new GrossContractedSalary(new Money(25_000_000, Currency.VND)),
					taxableAllowances: Enumerable.Empty<TaxableAllowance>().ToArray(),
					nonTaxableAllowances: Enumerable.Empty<NonTaxableAllowance>().ToArray(),
					paymentAdvance: new PaymentAdvance(Money.ZeroVND),
					adjustmentAdditions: Enumerable.Empty<AdjustmentAddition>().ToArray(),
					adjustmentDeduction: Enumerable.Empty<AdjustmentDeduction>().ToArray(),
					isForeigner: false)
			};

			var salaryConfig = new SalaryConfig(
				id: Guid.NewGuid(),
				isInsurancePaidFullSalary: true,
				insurancePaidAmount: Money.ZeroVND,
				commonMinimumWage: new Money(1_490_000m, Currency.VND),
				regionalMinimumWage: new Money(4_729_400m, Currency.VND),
				coefficientSocialCare: 20m,
				employerSocialInsuranceRate: 17.5m,
				employeeSocialInsuranceRate: 8m,
				healthCareInsuranceEmployerRate: 3m,
				healthCareInsuranceEmployeeRate: 1.5m,
				unemploymentInsuranceEmployerRate: 1m,
				unemploymentInsuranceEmployeeRate: 1m,
				minimumNonWorkingDay: 14,
				employeeUnionFeeRate: 1m,
				employerUnionFeeRate: 2m,
				maximumUnionFeeRate: 10m,
				personalDeduction: new Money(11_000_000m, Currency.VND),
				dependantDeduction: new Money(4_400_000, Currency.VND),
				defaultProbationTaxRate: 10m,
				progressiveTaxRateLookUpTable: new ProgressiveTaxRateLookUpTable(
					new ProgressiveTaxRate[]
					{
						new ProgressiveTaxRate(
							lowerBound: new Money(0m, Currency.VND),
							upperBound: new Money(5_000_000m, Currency.VND),
							progressiveTaxRateLevel: ProgressiveTaxRateLevel.One,
							rate: 5),
						new ProgressiveTaxRate(
							lowerBound: new Money(5_000_001m, Currency.VND),
							upperBound: new Money(10_000_000m, Currency.VND),
							progressiveTaxRateLevel: ProgressiveTaxRateLevel.Two,
							rate: 10),
						new ProgressiveTaxRate(
							lowerBound: new Money(10_000_001m, Currency.VND),
							upperBound: new Money(18_000_000m, Currency.VND),
							progressiveTaxRateLevel: ProgressiveTaxRateLevel.Three,
							rate: 15),
						new ProgressiveTaxRate(
							lowerBound: new Money(18_000_001m, Currency.VND),
							upperBound: new Money(32_000_000m, Currency.VND),
							progressiveTaxRateLevel: ProgressiveTaxRateLevel.Four,
							rate: 20),
						new ProgressiveTaxRate(
							lowerBound: new Money(32_000_001m, Currency.VND),
							upperBound: new Money(52_000_000m, Currency.VND),
							progressiveTaxRateLevel: ProgressiveTaxRateLevel.Five,
							rate: 25),
						new ProgressiveTaxRate(
							lowerBound: new Money(52_000_001m, Currency.VND),
							upperBound: new Money(80_000_000m, Currency.VND),
							progressiveTaxRateLevel: ProgressiveTaxRateLevel.Six,
							rate: 30),
						new ProgressiveTaxRate(
							lowerBound: new Money(80_000_001m, Currency.VND),
							upperBound: new Money(decimal.MaxValue, Currency.VND),
							progressiveTaxRateLevel: ProgressiveTaxRateLevel.Seven,
							rate: 35),
					}
				)
			);

			// Act
			var payrollRecords = Main.GeneratePayRollRecord(
				employeeMonthlyRecords: employeeMonthlyRecords,
				salaryConfig: salaryConfig).First();

			// Assert
			Assert.Equal(0m, payrollRecords.InsuranceSalary);
			Assert.Equal(7_954_545m, payrollRecords.ActualGrossSalary);
			Assert.Equal(7_954_545m, payrollRecords.TotalMonthlyIncome);
			Assert.Equal(7_954_545m, payrollRecords.TaxableIncome);
			Assert.Equal(0m, payrollRecords.EmployeeSocialInsurance);
			Assert.Equal(0m, payrollRecords.EmployeeHealthcareInsurance);
			Assert.Equal(0m, payrollRecords.EmployeeUnemploymentInsurance);
			Assert.Equal(0m, payrollRecords.EmployerSocialInsurance);
			Assert.Equal(0m, payrollRecords.EmployerHealthcareInsurance);
			Assert.Equal(0m, payrollRecords.EmployerUnemploymentInsurance);
			Assert.Equal(0m, payrollRecords.EmployerUnionFee);
			Assert.Equal(11_000_000m, payrollRecords.PersonalDeduction);
			Assert.Equal(0, payrollRecords.AssessableIncome);
			Assert.Equal(7_954_545m, payrollRecords.NetIncome);
			Assert.Equal(7_954_545m, payrollRecords.TotalSalaryCost);
			Assert.Equal(7_954_545m, payrollRecords.NetPayment);
		}

		[Fact]
		public void Test_Vietnam3()
		{
			// Arrange
			var employeeMonthlyRecords = new EmployeeMonthlyRecord[]
			{
				new EmployeeMonthlyRecord(
					name: "Huy Tran",
					position: "Full Stack Developer",
					email: "aa@gmail.com",
					workingDays: 22,
					standardWorkingDays: 22,
					inUnion: false,
					employeeType: "Permanent",
					numberOfDependants: 0,
					probationWorkingDays: 0,
					grossContractedSalary: new GrossContractedSalary(new Money(18_000_000, Currency.VND)),
					taxableAllowances: Enumerable.Empty<TaxableAllowance>().ToArray(),
					nonTaxableAllowances: Enumerable.Empty<NonTaxableAllowance>().ToArray(),
					paymentAdvance: new PaymentAdvance(Money.ZeroVND),
					adjustmentAdditions: Enumerable.Empty<AdjustmentAddition>().ToArray(),
					adjustmentDeduction: Enumerable.Empty<AdjustmentDeduction>().ToArray(),
					isForeigner: false)
			};

			var salaryConfig = new SalaryConfig(
				id: Guid.NewGuid(),
				isInsurancePaidFullSalary: true,
				insurancePaidAmount: Money.ZeroVND,
				commonMinimumWage: new Money(1_490_000m, Currency.VND),
				regionalMinimumWage: new Money(4_729_400m, Currency.VND),
				coefficientSocialCare: 20m,
				employerSocialInsuranceRate: 17.5m,
				employeeSocialInsuranceRate: 8m,
				healthCareInsuranceEmployerRate: 3m,
				healthCareInsuranceEmployeeRate: 1.5m,
				unemploymentInsuranceEmployerRate: 1m,
				unemploymentInsuranceEmployeeRate: 1m,
				minimumNonWorkingDay: 14,
				employeeUnionFeeRate: 1m,
				employerUnionFeeRate: 2m,
				maximumUnionFeeRate: 10m,
				personalDeduction: new Money(11_000_000m, Currency.VND),
				dependantDeduction: new Money(4_400_000, Currency.VND),
				defaultProbationTaxRate: 10m,
				progressiveTaxRateLookUpTable: new ProgressiveTaxRateLookUpTable(
					new ProgressiveTaxRate[]
					{
						new ProgressiveTaxRate(
							lowerBound: new Money(0m, Currency.VND),
							upperBound: new Money(5_000_000m, Currency.VND),
							progressiveTaxRateLevel: ProgressiveTaxRateLevel.One,
							rate: 5),
						new ProgressiveTaxRate(
							lowerBound: new Money(5_000_001m, Currency.VND),
							upperBound: new Money(10_000_000m, Currency.VND),
							progressiveTaxRateLevel: ProgressiveTaxRateLevel.Two,
							rate: 10),
						new ProgressiveTaxRate(
							lowerBound: new Money(10_000_001m, Currency.VND),
							upperBound: new Money(18_000_000m, Currency.VND),
							progressiveTaxRateLevel: ProgressiveTaxRateLevel.Three,
							rate: 15),
						new ProgressiveTaxRate(
							lowerBound: new Money(18_000_001m, Currency.VND),
							upperBound: new Money(32_000_000m, Currency.VND),
							progressiveTaxRateLevel: ProgressiveTaxRateLevel.Four,
							rate: 20),
						new ProgressiveTaxRate(
							lowerBound: new Money(32_000_001m, Currency.VND),
							upperBound: new Money(52_000_000m, Currency.VND),
							progressiveTaxRateLevel: ProgressiveTaxRateLevel.Five,
							rate: 25),
						new ProgressiveTaxRate(
							lowerBound: new Money(52_000_001m, Currency.VND),
							upperBound: new Money(80_000_000m, Currency.VND),
							progressiveTaxRateLevel: ProgressiveTaxRateLevel.Six,
							rate: 30),
						new ProgressiveTaxRate(
							lowerBound: new Money(80_000_001m, Currency.VND),
							upperBound: new Money(decimal.MaxValue, Currency.VND),
							progressiveTaxRateLevel: ProgressiveTaxRateLevel.Seven,
							rate: 35),
					}
				)
			);

			// Act
			var payrollRecords = Main.GeneratePayRollRecord(
				employeeMonthlyRecords: employeeMonthlyRecords,
				salaryConfig: salaryConfig).First();

			// Assert
			Assert.Equal(18_000_000m, payrollRecords.InsuranceSalary);
			Assert.Equal(18_000_000m, payrollRecords.ActualGrossSalary);
			Assert.Equal(18_000_000m, payrollRecords.TotalMonthlyIncome);
			Assert.Equal(18_000_000m, payrollRecords.TaxableIncome);
			Assert.Equal(1_440_000m, payrollRecords.EmployeeSocialInsurance);
			Assert.Equal(270_000m, payrollRecords.EmployeeHealthcareInsurance);
			Assert.Equal(180_000m, payrollRecords.EmployeeUnemploymentInsurance);
			Assert.Equal(3_150_000m, payrollRecords.EmployerSocialInsurance);
			Assert.Equal(540_000m, payrollRecords.EmployerHealthcareInsurance);
			Assert.Equal(180_000m, payrollRecords.EmployerUnemploymentInsurance);
			Assert.Equal(360_000m, payrollRecords.EmployerUnionFee);
			Assert.Equal(11_000_000m, payrollRecords.PersonalDeduction);
			Assert.Equal(5_110_000m, payrollRecords.AssessableIncome);
			Assert.Equal(261_000m, payrollRecords.PIT);
			Assert.Equal(15_849_000m, payrollRecords.NetIncome);
			Assert.Equal(22_230_000m, payrollRecords.TotalSalaryCost);
			Assert.Equal(15_849_000m, payrollRecords.NetPayment);
		}
		
		[Fact]
		public void Test_Vietnam4()
		{	
			// Arrange
			var employeeMonthlyRecords = new EmployeeMonthlyRecord[]
			{
				new EmployeeMonthlyRecord(
					name: "Huy Tran",
					position: "Full Stack Developer",
					email: "aa@gmail.com",
					workingDays: 22,
					standardWorkingDays: 22,
					inUnion: false,
					employeeType: "Permanent",
					numberOfDependants: 1,
					probationWorkingDays: 0,
					grossContractedSalary: new GrossContractedSalary(new Money(35_000_000m, Currency.VND)),
					taxableAllowances: Enumerable.Empty<TaxableAllowance>().ToArray(),
					nonTaxableAllowances: Enumerable.Empty<NonTaxableAllowance>().ToArray(),
					paymentAdvance: new PaymentAdvance(Money.ZeroVND),
					adjustmentAdditions: Enumerable.Empty<AdjustmentAddition>().ToArray(),
					adjustmentDeduction: Enumerable.Empty<AdjustmentDeduction>().ToArray(),
					isForeigner: false)
			};

			var salaryConfig = new SalaryConfig(
				id: Guid.NewGuid(),
				isInsurancePaidFullSalary: true,
				insurancePaidAmount: Money.ZeroVND,
				commonMinimumWage: new Money(1_490_000m, Currency.VND),
				regionalMinimumWage: new Money(4_729_400m, Currency.VND),
				coefficientSocialCare: 20m,
				employerSocialInsuranceRate: 17.5m,
				employeeSocialInsuranceRate: 8m,
				healthCareInsuranceEmployerRate: 3m,
				healthCareInsuranceEmployeeRate: 1.5m,
				unemploymentInsuranceEmployerRate: 1m,
				unemploymentInsuranceEmployeeRate: 1m,
				minimumNonWorkingDay: 14,
				employeeUnionFeeRate: 1m,
				employerUnionFeeRate: 2m,
				maximumUnionFeeRate: 10m,
				personalDeduction: new Money(11_000_000m, Currency.VND),
				dependantDeduction: new Money(4_400_000, Currency.VND),
				defaultProbationTaxRate: 10m,
				progressiveTaxRateLookUpTable: new ProgressiveTaxRateLookUpTable(
					new ProgressiveTaxRate[]
					{
						new ProgressiveTaxRate(
							lowerBound: new Money(0m, Currency.VND),
							upperBound: new Money(5_000_000m, Currency.VND),
							progressiveTaxRateLevel: ProgressiveTaxRateLevel.One,
							rate: 5),
						new ProgressiveTaxRate(
							lowerBound: new Money(5_000_001m, Currency.VND),
							upperBound: new Money(10_000_000m, Currency.VND),
							progressiveTaxRateLevel: ProgressiveTaxRateLevel.Two,
							rate: 10),
						new ProgressiveTaxRate(
							lowerBound: new Money(10_000_001m, Currency.VND),
							upperBound: new Money(18_000_000m, Currency.VND),
							progressiveTaxRateLevel: ProgressiveTaxRateLevel.Three,
							rate: 15),
						new ProgressiveTaxRate(
							lowerBound: new Money(18_000_001m, Currency.VND),
							upperBound: new Money(32_000_000m, Currency.VND),
							progressiveTaxRateLevel: ProgressiveTaxRateLevel.Four,
							rate: 20),
						new ProgressiveTaxRate(
							lowerBound: new Money(32_000_001m, Currency.VND),
							upperBound: new Money(52_000_000m, Currency.VND),
							progressiveTaxRateLevel: ProgressiveTaxRateLevel.Five,
							rate: 25),
						new ProgressiveTaxRate(
							lowerBound: new Money(52_000_001m, Currency.VND),
							upperBound: new Money(80_000_000m, Currency.VND),
							progressiveTaxRateLevel: ProgressiveTaxRateLevel.Six,
							rate: 30),
						new ProgressiveTaxRate(
							lowerBound: new Money(80_000_001m, Currency.VND),
							upperBound: new Money(decimal.MaxValue, Currency.VND),
							progressiveTaxRateLevel: ProgressiveTaxRateLevel.Seven,
							rate: 35),
					}
				)
			);

			// Act
			var payrollRecords = Main.GeneratePayRollRecord(
				employeeMonthlyRecords: employeeMonthlyRecords,
				salaryConfig: salaryConfig).First();

			// Assert
			Assert.Equal(35_000_000m, payrollRecords.InsuranceSalary);
			Assert.Equal(35_000_000m, payrollRecords.ActualGrossSalary);
			Assert.Equal(35_000_000m, payrollRecords.TotalMonthlyIncome);
			Assert.Equal(35_000_000m, payrollRecords.TaxableIncome);
			Assert.Equal(2_384_000m, payrollRecords.EmployeeSocialInsurance);
			Assert.Equal(447_000m, payrollRecords.EmployeeHealthcareInsurance);
			Assert.Equal(350_000m, payrollRecords.EmployeeUnemploymentInsurance);
			Assert.Equal(5_215_000m, payrollRecords.EmployerSocialInsurance);
			Assert.Equal(894_000m, payrollRecords.EmployerHealthcareInsurance);
			Assert.Equal(350_000m, payrollRecords.EmployerUnemploymentInsurance);
			Assert.Equal(596_000m, payrollRecords.EmployerUnionFee);
			Assert.Equal(11_000_000m, payrollRecords.PersonalDeduction);
			Assert.Equal(4_400_000m, payrollRecords.DependantDeduction);
			Assert.Equal(16_419_000, payrollRecords.AssessableIncome);
			Assert.Equal(1_712_850m, payrollRecords.PIT);
			Assert.Equal(30_106_150m, payrollRecords.NetIncome);
			Assert.Equal(42_055_000m, payrollRecords.TotalSalaryCost);
			Assert.Equal(30_106_150m, payrollRecords.NetPayment);
		}
		
			
		[Fact]
		public void Test_Vietnam5()
		{	
			// Arrange
			var employeeMonthlyRecords = new EmployeeMonthlyRecord[]
			{
				new EmployeeMonthlyRecord(
					name: "Huy Tran",
					position: "Full Stack Developer",
					email: "aa@gmail.com",
					workingDays: 18,
					standardWorkingDays: 22,
					inUnion: false,
					employeeType: "Probation",
					numberOfDependants: 2,
					probationWorkingDays: 0,
					grossContractedSalary: new GrossContractedSalary(new Money(35_000_000m, Currency.VND)),
					taxableAllowances: Enumerable.Empty<TaxableAllowance>().ToArray(),
					nonTaxableAllowances: Enumerable.Empty<NonTaxableAllowance>().ToArray(),
					paymentAdvance: new PaymentAdvance(Money.ZeroVND),
					adjustmentAdditions: Enumerable.Empty<AdjustmentAddition>().ToArray(),
					adjustmentDeduction: Enumerable.Empty<AdjustmentDeduction>().ToArray(),
					isForeigner: false)
			};

			var salaryConfig = new SalaryConfig(
				id: Guid.NewGuid(),
				isInsurancePaidFullSalary: true,
				insurancePaidAmount: Money.ZeroVND,
				commonMinimumWage: new Money(1_490_000m, Currency.VND),
				regionalMinimumWage: new Money(4_729_400m, Currency.VND),
				coefficientSocialCare: 20m,
				employerSocialInsuranceRate: 17.5m,
				employeeSocialInsuranceRate: 8m,
				healthCareInsuranceEmployerRate: 3m,
				healthCareInsuranceEmployeeRate: 1.5m,
				unemploymentInsuranceEmployerRate: 1m,
				unemploymentInsuranceEmployeeRate: 1m,
				minimumNonWorkingDay: 14,
				employeeUnionFeeRate: 1m,
				employerUnionFeeRate: 2m,
				maximumUnionFeeRate: 10m,
				personalDeduction: new Money(11_000_000m, Currency.VND),
				dependantDeduction: new Money(4_400_000m, Currency.VND),
				defaultProbationTaxRate: 10m,
				progressiveTaxRateLookUpTable: new ProgressiveTaxRateLookUpTable(
					new ProgressiveTaxRate[]
					{
						new ProgressiveTaxRate(
							lowerBound: new Money(0m, Currency.VND),
							upperBound: new Money(5_000_000m, Currency.VND),
							progressiveTaxRateLevel: ProgressiveTaxRateLevel.One,
							rate: 5),
						new ProgressiveTaxRate(
							lowerBound: new Money(5_000_001m, Currency.VND),
							upperBound: new Money(10_000_000m, Currency.VND),
							progressiveTaxRateLevel: ProgressiveTaxRateLevel.Two,
							rate: 10),
						new ProgressiveTaxRate(
							lowerBound: new Money(10_000_001m, Currency.VND),
							upperBound: new Money(18_000_000m, Currency.VND),
							progressiveTaxRateLevel: ProgressiveTaxRateLevel.Three,
							rate: 15),
						new ProgressiveTaxRate(
							lowerBound: new Money(18_000_001m, Currency.VND),
							upperBound: new Money(32_000_000m, Currency.VND),
							progressiveTaxRateLevel: ProgressiveTaxRateLevel.Four,
							rate: 20),
						new ProgressiveTaxRate(
							lowerBound: new Money(32_000_001m, Currency.VND),
							upperBound: new Money(52_000_000m, Currency.VND),
							progressiveTaxRateLevel: ProgressiveTaxRateLevel.Five,
							rate: 25),
						new ProgressiveTaxRate(
							lowerBound: new Money(52_000_001m, Currency.VND),
							upperBound: new Money(80_000_000m, Currency.VND),
							progressiveTaxRateLevel: ProgressiveTaxRateLevel.Six,
							rate: 30),
						new ProgressiveTaxRate(
							lowerBound: new Money(80_000_001m, Currency.VND),
							upperBound: new Money(decimal.MaxValue, Currency.VND),
							progressiveTaxRateLevel: ProgressiveTaxRateLevel.Seven,
							rate: 35),
					}
				)
			);

			// Act
			var payrollRecords = Main.GeneratePayRollRecord(
				employeeMonthlyRecords: employeeMonthlyRecords,
				salaryConfig: salaryConfig).First();

			// Assert
			Assert.Equal(35_000_000m, payrollRecords.InsuranceSalary);
			Assert.Equal(28_636_364m, payrollRecords.ActualGrossSalary);
			Assert.Equal(28_636_364m, payrollRecords.TotalMonthlyIncome);
			Assert.Equal(28_636_364m, payrollRecords.TaxableIncome);
			Assert.Equal(0m, payrollRecords.EmployeeSocialInsurance);
			Assert.Equal(0m, payrollRecords.EmployeeHealthcareInsurance);
			Assert.Equal(0m, payrollRecords.EmployeeUnemploymentInsurance);
			Assert.Equal(0m, payrollRecords.EmployerSocialInsurance);
			Assert.Equal(0m, payrollRecords.EmployerHealthcareInsurance);
			Assert.Equal(0m, payrollRecords.EmployerUnemploymentInsurance);
			Assert.Equal(0m, payrollRecords.EmployerUnionFee);
			Assert.Equal(0m, payrollRecords.EmployeeUnionFee);
			Assert.Equal(11_000_000m, payrollRecords.PersonalDeduction);
			Assert.Equal(28_636_364m, payrollRecords.AssessableIncome);
			Assert.Equal(28_636_36m, payrollRecords.PIT);
			Assert.Equal(25_772_728m, payrollRecords.NetIncome);
			Assert.Equal(28_636_364m, payrollRecords.TotalSalaryCost);
			Assert.Equal(25_772_728m, payrollRecords.NetPayment);
		}

		[Fact]
		public void Test_Foreigner1()
		{
			// Arrange
			var employeeMonthlyRecords = new EmployeeMonthlyRecord[]
			{
				new EmployeeMonthlyRecord(
					name: "Huy Tran",
					position: "Full Stack Developer",
					email: "aa@gmail.com",
					workingDays: 22,
					standardWorkingDays: 22,
					inUnion: false,
					employeeType: "Permanent",
					numberOfDependants: 0,
					probationWorkingDays: 0,
					grossContractedSalary: new GrossContractedSalary(new Money(35_000_000m, Currency.VND)),
					taxableAllowances: Enumerable.Empty<TaxableAllowance>().ToArray(),
					nonTaxableAllowances: Enumerable.Empty<NonTaxableAllowance>().ToArray(),
					paymentAdvance: new PaymentAdvance(Money.ZeroVND),
					adjustmentAdditions: Enumerable.Empty<AdjustmentAddition>().ToArray(),
					adjustmentDeduction: Enumerable.Empty<AdjustmentDeduction>().ToArray(),
					isForeigner: true)
			};

			var salaryConfig = new SalaryConfig(
				id: Guid.NewGuid(),
				isInsurancePaidFullSalary: true,
				insurancePaidAmount: Money.ZeroVND,
				commonMinimumWage: new Money(1_490_000m, Currency.VND),
				regionalMinimumWage: new Money(4_729_400m, Currency.VND),
				coefficientSocialCare: 20m,
				employerSocialInsuranceRate: 17.5m,
				employeeSocialInsuranceRate: 8m,
				healthCareInsuranceEmployerRate: 3m,
				healthCareInsuranceEmployeeRate: 1.5m,
				unemploymentInsuranceEmployerRate: 1m,
				unemploymentInsuranceEmployeeRate: 1m,
				foreignEmployerSocialInsuranceRate: 3.5m,
				foreignEmployeeSocialInsuranceRate: 0m,
				foreignHealthCareInsuranceEmployerRate: 3m,
				foreignHealthCareInsuranceEmployeeRate: 1.5m,
				foreignUnemploymentInsuranceEmployerRate: 0m,
				foreignUnemploymentInsuranceEmployeeRate: 0m,
				minimumNonWorkingDay: 14,
				employeeUnionFeeRate: 1m,
				employerUnionFeeRate: 2m,
				maximumUnionFeeRate: 10m,
				personalDeduction: new Money(11_000_000m, Currency.VND),
				dependantDeduction: new Money(4_400_000m, Currency.VND),
				defaultProbationTaxRate: 10m,
				progressiveTaxRateLookUpTable: new ProgressiveTaxRateLookUpTable(
					new ProgressiveTaxRate[]
					{
						new ProgressiveTaxRate(
							lowerBound: new Money(0m, Currency.VND),
							upperBound: new Money(5_000_000m, Currency.VND),
							progressiveTaxRateLevel: ProgressiveTaxRateLevel.One,
							rate: 5),
						new ProgressiveTaxRate(
							lowerBound: new Money(5_000_001m, Currency.VND),
							upperBound: new Money(10_000_000m, Currency.VND),
							progressiveTaxRateLevel: ProgressiveTaxRateLevel.Two,
							rate: 10),
						new ProgressiveTaxRate(
							lowerBound: new Money(10_000_001m, Currency.VND),
							upperBound: new Money(18_000_000m, Currency.VND),
							progressiveTaxRateLevel: ProgressiveTaxRateLevel.Three,
							rate: 15),
						new ProgressiveTaxRate(
							lowerBound: new Money(18_000_001m, Currency.VND),
							upperBound: new Money(32_000_000m, Currency.VND),
							progressiveTaxRateLevel: ProgressiveTaxRateLevel.Four,
							rate: 20),
						new ProgressiveTaxRate(
							lowerBound: new Money(32_000_001m, Currency.VND),
							upperBound: new Money(52_000_000m, Currency.VND),
							progressiveTaxRateLevel: ProgressiveTaxRateLevel.Five,
							rate: 25),
						new ProgressiveTaxRate(
							lowerBound: new Money(52_000_001m, Currency.VND),
							upperBound: new Money(80_000_000m, Currency.VND),
							progressiveTaxRateLevel: ProgressiveTaxRateLevel.Six,
							rate: 30),
						new ProgressiveTaxRate(
							lowerBound: new Money(80_000_001m, Currency.VND),
							upperBound: new Money(decimal.MaxValue, Currency.VND),
							progressiveTaxRateLevel: ProgressiveTaxRateLevel.Seven,
							rate: 35),
					}
				)
			);

			// Act
			var payrollRecords = Main.GeneratePayRollRecord(
				employeeMonthlyRecords: employeeMonthlyRecords,
				salaryConfig: salaryConfig).First();

			// Assert
			Assert.Equal(35_000_000m, payrollRecords.InsuranceSalary);
			Assert.Equal(35_000_000m, payrollRecords.ActualGrossSalary);
			Assert.Equal(35_000_000m, payrollRecords.TotalMonthlyIncome);
			Assert.Equal(35_000_000m, payrollRecords.TaxableIncome);
			Assert.Equal(0m, payrollRecords.EmployeeSocialInsurance);
			Assert.Equal(447_000m, payrollRecords.EmployeeHealthcareInsurance);
			Assert.Equal(0m, payrollRecords.EmployeeUnemploymentInsurance);
			Assert.Equal(1_043__000m, payrollRecords.EmployerSocialInsurance);
			Assert.Equal(894_000m, payrollRecords.EmployerHealthcareInsurance);
			Assert.Equal(0m, payrollRecords.EmployerUnemploymentInsurance);
			Assert.Equal(596_000m, payrollRecords.EmployerUnionFee);
			Assert.Equal(0m, payrollRecords.EmployeeUnionFee);
			Assert.Equal(11_000_000m, payrollRecords.PersonalDeduction);
			Assert.Equal(23_553_000m, payrollRecords.AssessableIncome);
			Assert.Equal(3_060_600m, payrollRecords.PIT);
			Assert.Equal(31_492_400m, payrollRecords.NetIncome);
			Assert.Equal(37_533_000m, payrollRecords.TotalSalaryCost);
			Assert.Equal(31_492_400m, payrollRecords.NetPayment);
		}
	}
}