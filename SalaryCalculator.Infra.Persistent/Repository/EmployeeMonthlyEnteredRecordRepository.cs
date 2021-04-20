using Microsoft.AspNetCore.DataProtection;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SalaryCalculator.Core;
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
using System.Text.Json;
using System.Threading.Tasks;

namespace SalaryCalculator.Infra.Persistent.Repository
{
	public class EmployeeMonthlyEnteredRecordRepository : IEmployeeMonthlyEnteredRecordRepository
	{
		private readonly SalaryCalculatorDbContext _dbContext;
		private readonly ILogger<EmployeeMonthlyEnteredRecordRepository> _logger;
		private readonly IDataProtectionProvider _dataProtectionProvider;

		public EmployeeMonthlyEnteredRecordRepository(
			SalaryCalculatorDbContext dbContext,
			ILogger<EmployeeMonthlyEnteredRecordRepository> logger,
			IDataProtectionProvider dataProtectionProvider
		)
		{
			_dbContext = dbContext;
			_logger = logger;
			_dataProtectionProvider = dataProtectionProvider;
		}

		public async Task<Result<Nothing>> AddAsync(List<EmployeeMonthlyEnteredRecord> records,
			string yearMonth, string key, int standardWorkingDays)
		{
			if ((await ExistRecordAsync(yearMonth)).Data is false)
			{
				await SaveAsync(records, yearMonth, key, standardWorkingDays);
			}

			return Result<Nothing>.Ok(Nothing.Value);

		}

		public async Task<Result<bool>> AnyAsync()
		{
			try
			{
				return Result<bool>.Ok(await _dbContext.EncryptedEmpMonthlyEnteredRecords.AnyAsync());
			}
			catch (SqlException ex)
			{
				_logger.LogError(ex, Errors.EmpMonthlyEnteredRecord.AnyRecordDatabaseError.Message);
				return Errors.EmpMonthlyEnteredRecord.AnyRecordDatabaseError;
			}
		}

		public async Task<Result<Nothing>> DeleteAsync(string key, string yearMonth)
		{
			try
			{
				var closestMonthRecord = await _dbContext.EncryptedEmpMonthlyEnteredRecords
								.OrderByDescending(x => x.CreatedDate).FirstOrDefaultAsync();

				// check key whether valid or not, if not, it throw an exception.
				_dataProtectionProvider.CreateProtector(key).Unprotect(closestMonthRecord.EncryptedRecord);

				if (new YearMonth(closestMonthRecord.EncryptedYearMonth) != new YearMonth(yearMonth))
				{
					_logger.LogError(Errors.EmpMonthlyEnteredRecord.DeleteRecordNotFoundError.Message);
					return Errors.EmpMonthlyEnteredRecord.DeleteRecordNotFoundError;
				}

				_dbContext.EncryptedEmpMonthlyEnteredRecords.Remove(closestMonthRecord);
				await _dbContext.SaveChangesAsync();
				return Result<Nothing>.Ok(Nothing.Value);
			}
			catch (CryptographicException ex)
			{
				_logger.LogError(ex, Errors.EmpMonthlyEnteredRecord.DeleteRecordInvalidKeyError.Message);
				return Errors.EmpMonthlyEnteredRecord.DeleteRecordInvalidKeyError;
			}
			catch (SqlException ex)
			{
				_logger.LogError(ex, Errors.EmpMonthlyEnteredRecord.DeleteRecordDatabaseError.Message);
				return Errors.EmpMonthlyEnteredRecord.DeleteRecordDatabaseError;
			}
		}

		public async Task<Result<bool>> ExistRecordAsync(string yearMonth)
		{
			try
			{
				return Result<bool>.Ok((await _dbContext.EncryptedEmpMonthlyEnteredRecords
				.AnyAsync(x => new YearMonth(x.EncryptedYearMonth) == new YearMonth(yearMonth))));
			}
			catch (SqlException ex)
			{
				_logger.LogError(ex, Errors.EmpMonthlyEnteredRecord.ExistRecordDatabaseError.Message);
				return Errors.EmpMonthlyEnteredRecord.ExistRecordDatabaseError;
			}
		}

		public async Task<Result<(List<EmployeeMonthlyEnteredRecord> records,
			int standardWorkingDays)>> GetAsync(string yearMonth, string key)
		{
			try
			{
				if (YearMonth.TryParse(yearMonth, out YearMonth selectedYearMonth) is false)
				{
					_logger.LogError(Errors.EmpMonthlyEnteredRecord.GetRecordYearMonthInvalidError.Message);
					return Errors.EmpMonthlyEnteredRecord.GetRecordYearMonthInvalidError;
				}

				if ((await _dbContext.EncryptedEmpMonthlyEnteredRecords.AnyAsync()) is false)
				{
					return Result<(List<EmployeeMonthlyEnteredRecord>, int standardWorkingDays)>.Ok(
						(new List<EmployeeMonthlyEnteredRecord>(),
						new MonthlyWorkingDay(yearMonth).CalculatedMonthlyWorkingDays()));
				}

				var existingRecord = await _dbContext.EncryptedEmpMonthlyEnteredRecords
						.FirstOrDefaultAsync(x => x.EncryptedYearMonth == yearMonth);

				var newYearMonth = false;
				if (existingRecord == null)
				{
					var closestMonthRecord = await _dbContext.EncryptedEmpMonthlyEnteredRecords
						.OrderByDescending(x => x.CreatedDate).FirstOrDefaultAsync();

					if (closestMonthRecord == null)
					{
						_logger.LogError(Errors.EmpMonthlyEnteredRecord.GetRecordNotFoundError.Message);
						return Errors.EmpMonthlyEnteredRecord.GetRecordNotFoundError;
					}

					//if user selects previous year-month, then check if the closetYearMonth is after or before selectedYearMonth
					var closestYearMonth = new YearMonth(closestMonthRecord.EncryptedYearMonth);
					

					// return empty if it is after, means that previous year-month havent's any record.
					if (closestYearMonth > selectedYearMonth)
					{
						return Result<(List<EmployeeMonthlyEnteredRecord>, int standardWorkingDays)>.Ok(
							(new List<EmployeeMonthlyEnteredRecord>(),
							new MonthlyWorkingDay(yearMonth).CalculatedMonthlyWorkingDays()));
					}

					// use closest monthly salary record for current monthly salary month record
					existingRecord = closestMonthRecord;
					newYearMonth = true;
				}

				var decruptedJson = _dataProtectionProvider.CreateProtector(key).Unprotect(existingRecord.EncryptedRecord);
				var decryptedRecords = JsonSerializer.Deserialize<List<DecryptedEmpMonthlyEnteredRecord>>(
					decruptedJson
					);

				var newMonthlyWorkingDays = new MonthlyWorkingDay(yearMonth).CalculatedMonthlyWorkingDays();
				var result = newYearMonth switch
				{
					true => (decryptedRecords.Select(x =>
						{
							var currency = new Currency(x.Currency);
							return new EmployeeMonthlyEnteredRecord(
								fullname: x.Fullname,
								email: x.Email,
								grossContractSalary: new GrossContractedSalary(new Money(value: x.GrossContractSalary, currency)),
								probationGrossContractSalary: new GrossContractedSalary(new Money(value: x.ProbationGrossContractSalary, currency)),
								actualWorkingDays: newMonthlyWorkingDays,
								probationWorkingDays: 0,
								taxableAnnualLeave: new TaxableAllowance(name: x.TaxableAnnualLeave.Name,
									amount: new Money(0, currency)),
								taxable13MonthSalary: new TaxableAllowance(name: x.Taxable13MonthSalary.Name,
									amount: new Money(0, currency)),
								taxableOthers: new TaxableAllowance(name: x.TaxableOthers.Name,
									amount: new Money(0, currency)),
								nonTaxableAllowances: x.NonTaxableAllowances.Select(y =>
									new NonTaxableAllowance(amount: new Money(0, currency), name: y.Name)).ToArray(),
								paymentAdvance: new PaymentAdvance(new Money(0, currency)),
								adjustmentAdditions: x.AdjustmentAdditions.Select(y => new AdjustmentAddition(new Money(0, currency))).ToArray(),
								adjustmentDeductions: x.AdjustmentDeductions.Select(y => new AdjustmentDeduction(new Money(0, currency))).ToArray()
								);
						}).ToList(), newMonthlyWorkingDays),
					_ => (decryptedRecords.Select(x =>
						{
							var currency = new Currency(x.Currency);
							return new EmployeeMonthlyEnteredRecord(
								fullname: x.Fullname,
								email: x.Email,
								grossContractSalary: new GrossContractedSalary(new Money(value: x.GrossContractSalary, currency)),
								probationGrossContractSalary: new GrossContractedSalary(new Money(value: x.ProbationGrossContractSalary, currency)),
								actualWorkingDays: x.ActualWorkingDays,
								probationWorkingDays: x.ProbationWorkingDays,
								taxableAnnualLeave: new TaxableAllowance(name: x.TaxableAnnualLeave.Name,
									amount: new Money(x.TaxableAnnualLeave.Amount, currency)),
								taxable13MonthSalary: new TaxableAllowance(name: x.Taxable13MonthSalary.Name,
									amount: new Money(x.Taxable13MonthSalary.Amount, currency)),
								taxableOthers: new TaxableAllowance(name: x.TaxableOthers.Name,
									amount: new Money(x.TaxableOthers.Amount, currency)),
								nonTaxableAllowances: x.NonTaxableAllowances.Select(y =>
									new NonTaxableAllowance(amount: new Money(y.Amount, currency), name: y.Name)).ToArray(),
								paymentAdvance: new PaymentAdvance(new Money(x.PaymentAdvance, currency)),
								adjustmentAdditions: x.AdjustmentAdditions.Select(y => new AdjustmentAddition(new Money(y, currency))).ToArray(),
								adjustmentDeductions: x.AdjustmentDeductions.Select(y => new AdjustmentDeduction(new Money(y, currency))).ToArray()
								);
						}).ToList(), existingRecord.StandardWorkingDays)
				};

				return Result<(List<EmployeeMonthlyEnteredRecord>, int standardWorkingDays)>.Ok(result);
			}
			catch (CryptographicException ex)
			{
				_logger.LogError(ex, Errors.EmpMonthlyEnteredRecord.GetRecordKeyInvalidError.Message);
				return Errors.EmpMonthlyEnteredRecord.GetRecordKeyInvalidError;
			}
			catch (SqlException ex)
			{
				_logger.LogError(ex, Errors.EmpMonthlyEnteredRecord.GetRecordDatabaseError.Message);
				return Errors.EmpMonthlyEnteredRecord.GetRecordDatabaseError;
			}
		}

		public async Task<Result<Nothing>> SaveAsync(List<EmployeeMonthlyEnteredRecord> records, 
			string yearMonth, string key, int standardWorkingDays)
		{
			try
			{
				var json = JsonSerializer.Serialize(records.Select(x => new DecryptedEmpMonthlyEnteredRecord
				{
					Fullname = x.Fullname,
					Email = x.Email,
					GrossContractSalary = x.grossContractSalary.Amount,
					ProbationGrossContractSalary = x.ProbationGrossContractSalary.Amount,
					ActualWorkingDays = x.ActualWorkingDays,
					ProbationWorkingDays = x.ProbationWorkingDays,
					TaxableAnnualLeave = new DecryptedTaxableAllowance
					{
						Amount = x.TaxableAnnualLeave.Amount,
						Name = x.TaxableAnnualLeave.Name
					},
					Taxable13MonthSalary = new DecryptedTaxableAllowance
					{
						Amount = x.Taxable13MonthSalary.Amount,
						Name = x.Taxable13MonthSalary.Name
					},
					TaxableOthers = new DecryptedTaxableAllowance
					{
						Amount = x.TaxableOthers.Amount,
						Name = x.TaxableOthers.Name
					},
					NonTaxableAllowances = x.NonTaxableAllowances.Select(y => new DecryptedNonTaxableAllowance
					{
						Amount = y.Amount,
						Name = y.Name
					}).ToArray(),
					PaymentAdvance = x.PaymentAdvance.Amount,
					AdjustmentAdditions = x.AdjustmentAdditions.Select(y => y.Amount.Value).ToArray(),
					AdjustmentDeductions = x.AdjustmentDeductions.Select(y => y.Amount.Value).ToArray(),
					Currency = x.grossContractSalary.Amount.Currency.Value ?? Currency.VND.Value
				}
				));

				var encryptedRecord = _dataProtectionProvider.CreateProtector(key).Protect(json);

				if ((await _dbContext.EncryptedEmpMonthlyEnteredRecords.AnyAsync()) is false)
				{
					_dbContext.EncryptedEmpMonthlyEnteredRecords.Add(new EncryptedEmpMonthlyEnteredRecord
					{
						EncryptedYearMonth = yearMonth,
						EncryptedRecord = encryptedRecord,
						StandardWorkingDays = standardWorkingDays
					});

					await _dbContext.SaveChangesAsync();
					return Result<Nothing>.Ok(Nothing.Value);
				}

				var closestMonthRecord = await _dbContext.EncryptedEmpMonthlyEnteredRecords
						.OrderByDescending(x => x.CreatedDate).FirstOrDefaultAsync();

				// check key whether valid or not, if not, it throw an exception.
				_dataProtectionProvider.CreateProtector(key).Unprotect(closestMonthRecord.EncryptedRecord);

				if (new YearMonth(closestMonthRecord.EncryptedYearMonth) == new YearMonth(yearMonth))
				{
					closestMonthRecord.EncryptedRecord = encryptedRecord;
					closestMonthRecord.StandardWorkingDays = standardWorkingDays;
					_dbContext.EncryptedEmpMonthlyEnteredRecords.Update(closestMonthRecord);

					await _dbContext.SaveChangesAsync();
					return Result<Nothing>.Ok(Nothing.Value);
				}

				_dbContext.EncryptedEmpMonthlyEnteredRecords.Add(new EncryptedEmpMonthlyEnteredRecord
				{
					EncryptedYearMonth = yearMonth,
					EncryptedRecord = encryptedRecord,
					StandardWorkingDays = standardWorkingDays
				});

				await _dbContext.SaveChangesAsync();
				return Result<Nothing>.Ok(Nothing.Value);
			}
			catch (CryptographicException ex)
			{
				_logger.LogError(ex, Errors.EmpMonthlyEnteredRecord.SaveRecordKeyInvalidError.Message);
				return Errors.EmpMonthlyEnteredRecord.SaveRecordKeyInvalidError;
			}
			catch (SqlException ex)
			{
				_logger.LogError(ex, Errors.EmpMonthlyEnteredRecord.SaveRecordDatabaseError.Message);
				return Errors.EmpMonthlyEnteredRecord.SaveRecordDatabaseError;
			}
		}
	}
}
