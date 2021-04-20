using Microsoft.AspNetCore.DataProtection;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SalaryCalculator.Core.Common;
using SalaryCalculator.Core.Domain;
using SalaryCalculator.Core.Framework;
using SalaryCalculator.Core.Repository;
using SalaryCalculator.Core.ValueObjects;
using SalaryCalculator.Infra.Persistent.Common.Models;
using SalaryCalculator.Infra.Persistent.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SalaryCalculator.Infra.Persistent.Repository
{
	public class EmployeeMonthlySalaryRepository : IEmployeeMonthlySalaryRepository
	{
		private readonly SalaryCalculatorDbContext _dbContext;
		private readonly ILogger<EmployeeMonthlySalaryRepository> _logger;
		private readonly IDataProtectionProvider _dataProtectionProvider;

		public EmployeeMonthlySalaryRepository(
			SalaryCalculatorDbContext dbContext,
			ILogger<EmployeeMonthlySalaryRepository> logger,
			IDataProtectionProvider dataProtectionProvider
		)
		{
			_dbContext = dbContext;
			_logger = logger;
			_dataProtectionProvider = dataProtectionProvider;
		}

		public async Task<Result<PayrollReportRecord[]>> GetAsync(string key, string yearMonth)
		{
			try
			{
				var latestSalary = await _dbContext.EncryptedEmpMonthlySalaries
					.Include(x => x.EncryptedEmpMonthlyEnteredRecord)
					.FirstOrDefaultAsync(x => x.EncryptedEmpMonthlyEnteredRecord.EncryptedYearMonth == yearMonth);

				if(latestSalary == null || latestSalary.IsLatest is false)
				{
					return Result<PayrollReportRecord[]>.Ok(new List<PayrollReportRecord>().ToArray());
				}

				var salarySetting = await _dbContext.SalarySettings.Include(x => x.SalaryCurrency)
						.ThenInclude(y => y.ProgressiveTaxRateSettings)
						.FirstOrDefaultAsync();
				var profiles = await _dbContext.SyncEmployeeProfiles.ToListAsync();

				var decruptedJson = _dataProtectionProvider.CreateProtector(key).Unprotect(latestSalary.EncryptedSalary);
				var decryptedRecords = JsonSerializer.Deserialize<List<DecryptedEmpMonthlySalary>>(
					decruptedJson
					);

				return Result<PayrollReportRecord[]>.Ok(decryptedRecords.Select(x => new PayrollReportRecord(
					fullname: x.Fullname,
					email: x.Email,
					employeeType: x.EmployeeType,
					position: x.Position,
					numberOfDependants: x.NumberOfDependants,
					standardWorkingDays: x.StandardWorkingDays,
					actualWorkingDays: x.ActualWorkingDays,
					grossContractSalary: x.GrossContractSalary,
					insuranceSalary: x.InsuranceSalary,
					actualGrossSalary: x.ActualGrossSalary,
					taxableIncome: x.TaxableIncome,
					totalMonthlyIncome: x.TotalMonthlyIncome,
					employeeSocialInsurance: x.EmployeeSocialInsurance,
					employeeHealthcareInsurance: x.EmployeeHealthcareInsurance,
					employeeUnemploymentInsurance: x.EmployeeUnemploymentInsurance,
					employeeUnionFee: x.EmployeeUnionFee,
					employerSocialInsurance: x.EmployerSocialInsurance,
					employerHealthcareInsurance: x.EmployerHealthcareInsurance,
					employerUnemploymentInsurance: x.EmployerUnemploymentInsurance,
					employerUnionFee: x.EmployerUnionFee,
					personalDeduction: x.PersonalDeduction,
					dependantDeduction: x.DependantDeduction,
					assessableIncome: x.AssessableIncome,
					netIncome: x.NetIncome,
					pit: x.PIT,
					totalSalaryCost: x.TotalSalaryCost,
					paymentAdvance: x.PaymentAdvance,
					taxableAllowances: x.TaxableAllowances.Select(y => 
						new TaxableAllowance(y.Name, new Core.Money(y.Amount, Currency.VND))).ToArray(),
					nonTaxableAllowances: x.NonTaxableAllowances.Select(y =>
						new NonTaxableAllowance(new Core.Money(y.Amount, Currency.VND), y.Name)).ToArray(),
					netPayment: x.NetPayment,
					adjustmentDeduction: x.AdjustmentDeduction,
					adjustmentAddition: x.AdjustmentAddition
				)).ToArray());
			}
			catch (CryptographicException ex)
			{
				_logger.LogError(ex, Errors.EmpMonthlySalary.GetSalaryKeyInvalidError.Message);
				return Errors.EmpMonthlySalary.GetSalaryKeyInvalidError;
			}
			catch (SqlException ex)
			{
				_logger.LogError(ex, Errors.EmpMonthlySalary.GetSalaryDatabaseError.Message);
				return Errors.EmpMonthlySalary.GetSalaryDatabaseError;
			}
		}

		public async Task<Result<Nothing>> SaveAsync(
			PayrollReportRecord[] salaries, 
			string key, string yearMonth)
		{
			try
			{
				var json = JsonSerializer.Serialize(salaries.Select(x => new DecryptedEmpMonthlySalary
				{
					Email = x.Email,
					Fullname = x.Fullname,
					EmployeeType = x.EmployeeType,
					Position = x.Position,
					GrossContractSalary = x.GrossContractSalary,
					InsuranceSalary = x.InsuranceSalary,
					StandardWorkingDays = x.StandardWorkingDays,
					ActualGrossSalary = x.ActualGrossSalary,
					ActualWorkingDays = x.ActualWorkingDays,
					NonTaxableAllowances = x.NonTaxableAllowances.Select(y => new DecryptedNonTaxableAllowance
					{
						Amount = y.Amount,
						Name = y.Name
					}).ToArray(),
					TaxableAllowances = x.TaxableAllowances.Select(y => new DecryptedTaxableAllowance
					{
						Amount = y.Amount,
						Name = y.Name
					}).ToArray(),
					TotalMonthlyIncome = x.TotalMonthlyIncome,
					TaxableIncome = x.TaxableIncome,
					EmployeeSocialInsurance = x.EmployeeSocialInsurance,
					EmployeeHealthcareInsurance = x.EmployeeHealthcareInsurance,
					EmployeeUnemploymentInsurance = x.EmployeeUnemploymentInsurance,
					EmployeeUnionFee = x.EmployeeUnionFee,
					EmployerSocialInsurance = x.EmployerSocialInsurance,
					EmployerHealthcareInsurance = x.EmployerHealthcareInsurance,
					EmployerUnemploymentInsurance = x.EmployerUnemploymentInsurance,
					EmployerUnionFee = x.EmployerUnionFee,
					PersonalDeduction = x.PersonalDeduction,
					NumberOfDependants = x.NumberOfDependants,
					DependantDeduction = x.DependantDeduction,
					AssessableIncome = x.AssessableIncome,
					PIT = x.PIT,
					NetIncome = x.NetIncome,
					TotalSalaryCost = x.TotalSalaryCost,
					PaymentAdvance = x.PaymentAdvance,
					AdjustmentAddition = x.AdjustmentAddition,
					AdjustmentDeduction = x.AdjustmentDeduction,
					NetPayment = x.NetPayment
				}));

				var encryptedSalary = _dataProtectionProvider.CreateProtector(key).Protect(json);
				var existingRecord = await _dbContext.EncryptedEmpMonthlyEnteredRecords
					.Include(x => x.EncryptedEmpMonthlySalary)
					.FirstOrDefaultAsync(x => x.EncryptedYearMonth == yearMonth);

				if (existingRecord == null)
				{
					_logger.LogError(Errors.EmpMonthlySalary.SaveSalaryRecordNullError.Message);
					return Errors.EmpMonthlySalary.SaveSalaryRecordNullError;
				}

				// check key whether valid or not, if not, it throw an exception.
				_dataProtectionProvider.CreateProtector(key).Unprotect(existingRecord.EncryptedRecord);

				if (existingRecord.EncryptedEmpMonthlySalary == null)
				{
					_dbContext.EncryptedEmpMonthlySalaries.Add(new EncryptedEmpMonthlySalary
					{
						EncryptedRecordId = existingRecord.Id,
						EncryptedSalary = encryptedSalary,
						IsLatest = true
					});
				}
				else
				{
					existingRecord.EncryptedEmpMonthlySalary.EncryptedSalary = encryptedSalary;
					existingRecord.EncryptedEmpMonthlySalary.IsLatest = true;
					_dbContext.EncryptedEmpMonthlySalaries.Update(existingRecord.EncryptedEmpMonthlySalary);
				}

				await _dbContext.SaveChangesAsync();
				return Result<Nothing>.Ok(Nothing.Value);
			}
			catch (CryptographicException ex)
			{
				_logger.LogError(ex, Errors.EmpMonthlySalary.SaveSalaryKeyInvalidError.Message);
				return Errors.EmpMonthlySalary.SaveSalaryKeyInvalidError;
			}
			catch (SqlException ex)
			{
				_logger.LogError(ex, Errors.EmpMonthlySalary.SaveSalaryDatabaseError.Message);
				return Errors.EmpMonthlySalary.SaveSalaryDatabaseError;
			}
		}

		public async Task<Result<Nothing>> MarkNotLatestAsync(string yearMonth)
		{
			try
			{
				var existingSalary = await _dbContext.EncryptedEmpMonthlySalaries
						.FirstOrDefaultAsync(x => x.EncryptedEmpMonthlyEnteredRecord.EncryptedYearMonth == yearMonth);

				if (existingSalary == null) return Result<Nothing>.Ok(Nothing.Value);

				existingSalary.IsLatest = false;
				_dbContext.EncryptedEmpMonthlySalaries.Update(existingSalary);
				await _dbContext.SaveChangesAsync();

				return Result<Nothing>.Ok(Nothing.Value);
			}
			catch (SqlException ex)
			{
				_logger.LogError(ex, Errors.EmpMonthlySalary.MarkNotLatestDatabaseError.Message);
				return Errors.EmpMonthlySalary.MarkNotLatestDatabaseError;
			}
		}

	}
}
