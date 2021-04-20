using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Logging;
using SalaryCalculator.Core;
using SalaryCalculator.Core.Domain;
using SalaryCalculator.Core.Framework;
using SalaryCalculator.Core.Repository;
using SalaryCalculator.Core.ValueObjects;
using SalaryCalculator.Infra.Persistent.Entities;

namespace SalaryCalculator.Infra.Persistent.Repository
{
	public class SalaryConfigRepository : ISalaryConfigRepository
	{
		private readonly SalaryCalculatorDbContext _dbContext;
		private readonly ILogger<SalaryConfigRepository> _logger;

		public SalaryConfigRepository(
			SalaryCalculatorDbContext dbContext,
			ILogger<SalaryConfigRepository> logger)
		{
			_dbContext = dbContext;
			_logger = logger;
		}

		public async Task<Result<SalaryConfig>> GetAsync()
		{
			try
			{
				var salarySetting = await _dbContext.SalarySettings.Include(x => x.SalaryCurrency)
						.ThenInclude(y => y.ProgressiveTaxRateSettings)
						.FirstOrDefaultAsync();

				if (salarySetting == null)
				{
					_logger.LogError(Errors.SalaryConfig.GetSalaryConfigSalarySettingNotFoundError.Message);
					return Errors.SalaryConfig.GetSalaryConfigSalarySettingNotFoundError;
				}

				var currency = new Currency(salarySetting.SalaryCurrency.Value);

				var salaryConfig = new SalaryConfig(
					id: salarySetting.Id,
					commonMinimumWage: new Money(salarySetting.CommonMinimumWage, currency),
					regionalMinimumWage: new Money(salarySetting.RegionalMinimumWage, currency),
					coefficientSocialCare: salarySetting.CoefficientSocialCare,
					minimumNonWorkingDay: salarySetting.MinimumNonWorkingDay,
					employerSocialInsuranceRate: salarySetting.EmployerSocialInsuranceRate,
					employeeSocialInsuranceRate: salarySetting.EmployeeSocialInsuranceRate,
					healthCareInsuranceEmployeeRate: salarySetting.EmployeeHealthCareInsuranceRate,
					healthCareInsuranceEmployerRate: salarySetting.EmployerHealthCareInsuranceRate,
					unemploymentInsuranceEmployeeRate: salarySetting.EmployeeUnemploymentInsuranceRate,
					unemploymentInsuranceEmployerRate: salarySetting.EmployerUnemploymentInsuranceRate,
					foreignEmployerSocialInsuranceRate: salarySetting.ForeignEmployerSocialInsuranceRate,
					foreignEmployeeSocialInsuranceRate: salarySetting.ForeignEmployeeSocialInsuranceRate,
					foreignHealthCareInsuranceEmployeeRate: salarySetting.ForeignEmployeeHealthCareInsuranceRate,
					foreignHealthCareInsuranceEmployerRate: salarySetting.ForeignEmployerHealthCareInsuranceRate,
					foreignUnemploymentInsuranceEmployeeRate: salarySetting.ForeignEmployeeUnemploymentInsuranceRate,
					foreignUnemploymentInsuranceEmployerRate: salarySetting.ForeignEmployerUnemploymentInsuranceRate,
					employeeUnionFeeRate: salarySetting.EmployeeUnionFeeRate,
					employerUnionFeeRate: salarySetting.EmployerUnionFeeRate,
					maximumUnionFeeRate: salarySetting.MaximumUnionFeeRate,
					personalDeduction: new Money(salarySetting.PersonalDeduction, currency),
					dependantDeduction: new Money(salarySetting.DependantDeduction, currency),
					progressiveTaxRateLookUpTable: new ProgressiveTaxRateLookUpTable(salarySetting.SalaryCurrency.ProgressiveTaxRateSettings
						.Select(x => new ProgressiveTaxRate(
								lowerBound: new Money(x.LowerBound, currency),
								upperBound: new Money(x.UpperBound, currency),
								rate: x.Rate,
								progressiveTaxRateLevel: (ProgressiveTaxRateLevel)x.TaxRateLevel
							)).ToArray()),
					defaultProbationTaxRate: salarySetting.DefaultProbationTaxRate,
					isInsurancePaidFullSalary: salarySetting.IsInsurancePaidFullSalary,
					insurancePaidAmount: new Money(salarySetting.InsurancePaidAmount, currency)
					);

				return Result<SalaryConfig>.Ok(salaryConfig);
			}
			catch (SqlException ex)
			{
				_logger.LogError(ex, Errors.SalaryConfig.GetSalaryConfigDatabaseError.Message);
				return Errors.SalaryConfig.GetSalaryConfigDatabaseError;
			}
		}

		public async Task<Result<Nothing>> SaveAsync(SalaryConfig salaryConfig)
		{
			//var metadata = new Dictionary<string, string>();
			//metadata.Add("Id", salaryConfig.Id.ToString());
			//metadata.Add("CoefficientSocialCare", salaryConfig.CoefficientSocialCare.ToString());
			//metadata.Add("CommonMinimumWage", salaryConfig.CommonMinimumWage.Value.ToString());
			//metadata.Add("RegionalMinimumWage", salaryConfig.RegionalMinimumWage.Value.ToString());
			//metadata.Add("EmployeeSocialInsuranceRate", salaryConfig.EmployeeSocialInsuranceRate.ToString());
			//metadata.Add("EmployeeHealthCareInsuranceRate", salaryConfig.EmployeeHealthCareInsuranceRate.ToString());
			//metadata.Add("EmployeeUnemploymentInsuranceRate", salaryConfig.EmployeeUnemploymentInsuranceRate.ToString());
			//metadata.Add("EmployeeUnionFeeRate", salaryConfig.EmployeeUnionFeeRate.ToString());
			//metadata.Add("EmployerHealthCareInsuranceRate", salaryConfig.EmployerHealthCareInsuranceRate.ToString());
			//metadata.Add("EmployerSocialInsuranceRate", salaryConfig.EmployerSocialInsuranceRate.ToString());
			//metadata.Add("EmployerUnemploymentInsuranceRate", salaryConfig.EmployerUnemploymentInsuranceRate.ToString());
			//metadata.Add("EmployerUnionFeeRate", salaryConfig.EmployerUnionFeeRate.ToString());
			//metadata.Add("MaximumUnionFeeRate", salaryConfig.MaximumUnionFeeRate.ToString());
			//metadata.Add("MinimumNonWorkingDay", salaryConfig.MinimumNonWorkingDay.ToString());
			//metadata.Add("Currency", salaryConfig.CommonMinimumWage.Currency.Value.ToString());
			//metadata.Add("DefaultProbationTaxRate", salaryConfig.DefaultProbationTaxRate.ToString());
			//metadata.Add("DependantDeduction", salaryConfig.DependantDeduction.Value.ToString());
			//metadata.Add("PersonalDeduction", salaryConfig.PersonalDeduction.Value.ToString());
			//metadata.Add("IsInsurancePaidFullSalary", salaryConfig.IsInsurancePaidFullSalary.ToString());
			//metadata.Add("InsurancePaidAmount", salaryConfig.InsurancePaidAmount.Value.ToString());
			//metadata.Add("ProgressiveTaxRates", salaryConfig.ProgressiveTaxRateLookUpTable.ToString());
			//_logger.LogInformation(Errors.SalaryConfig.SaveSalaryConfigCurrencyNotFoundError.Message 
				//+ " - params: {metadata}", metadata);
			try
			{
				var currency = await
					_dbContext.SalaryCurrencies.FirstOrDefaultAsync(x =>
						x.Value == salaryConfig.CommonMinimumWage.Currency.Value);

				if (currency == null)
				{
					_logger.LogError(Errors.SalaryConfig.SaveSalaryConfigCurrencyNotFoundError.Message);
					return Errors.SalaryConfig.SaveSalaryConfigCurrencyNotFoundError;
				}

				_dbContext.SalarySettings.Update(new SalarySetting()
				{
					Id = salaryConfig.Id,
					CoefficientSocialCare = salaryConfig.CoefficientSocialCare,
					CommonMinimumWage = salaryConfig.CommonMinimumWage,
					RegionalMinimumWage = salaryConfig.RegionalMinimumWage,
					EmployeeSocialInsuranceRate = salaryConfig.EmployeeSocialInsuranceRate * 100,
					EmployeeHealthCareInsuranceRate = salaryConfig.EmployeeHealthCareInsuranceRate * 100,
					EmployeeUnemploymentInsuranceRate = salaryConfig.EmployeeUnemploymentInsuranceRate * 100,
					EmployerHealthCareInsuranceRate = salaryConfig.EmployerHealthCareInsuranceRate * 100,
					EmployerSocialInsuranceRate = salaryConfig.EmployerSocialInsuranceRate * 100,
					EmployerUnemploymentInsuranceRate = salaryConfig.EmployerUnemploymentInsuranceRate * 100,
					ForeignEmployeeSocialInsuranceRate = salaryConfig.ForeignEmployeeSocialInsuranceRate * 100,
					ForeignEmployeeHealthCareInsuranceRate = salaryConfig.ForeignEmployeeHealthCareInsuranceRate * 100,
					ForeignEmployeeUnemploymentInsuranceRate = salaryConfig.ForeignEmployeeUnemploymentInsuranceRate * 100,
					ForeignEmployerHealthCareInsuranceRate = salaryConfig.ForeignEmployerHealthCareInsuranceRate * 100,
					ForeignEmployerSocialInsuranceRate = salaryConfig.ForeignEmployerSocialInsuranceRate * 100,
					ForeignEmployerUnemploymentInsuranceRate = salaryConfig.ForeignEmployerUnemploymentInsuranceRate * 100,
					EmployeeUnionFeeRate = salaryConfig.EmployeeUnionFeeRate * 100,
					EmployerUnionFeeRate = salaryConfig.EmployerUnionFeeRate * 100,
					MaximumUnionFeeRate = salaryConfig.MaximumUnionFeeRate * 100,
					MinimumNonWorkingDay = salaryConfig.MinimumNonWorkingDay,
					CurrencyId = currency.Id,
					DefaultProbationTaxRate = salaryConfig.DefaultProbationTaxRate * 100,
					DependantDeduction = salaryConfig.DependantDeduction,
					PersonalDeduction = salaryConfig.PersonalDeduction,
					IsInsurancePaidFullSalary = salaryConfig.IsInsurancePaidFullSalary,
					InsurancePaidAmount = salaryConfig.IsInsurancePaidFullSalary ? 0 : salaryConfig.InsurancePaidAmount.Value
				});

				var existingTaxRates = await _dbContext.ProgressiveTaxRateSettings.ToListAsync();
				var newTaxRates = salaryConfig.ProgressiveTaxRateLookUpTable.AsReadOnlyCollection().ToList();

				newTaxRates.ForEach(x =>
				{
					var existingTaxRate = existingTaxRates.FirstOrDefault(y =>
						y.TaxRateLevel == (int)x.ProgressiveTaxRateLevel);

					if (existingTaxRate != null)
					{
						existingTaxRate.CurrencyId = currency.Id;
						existingTaxRate.LowerBound = x.LowerBound;
						existingTaxRate.UpperBound = x.UpperBound;
						existingTaxRate.Rate = x.Rate * 100;
					}
				});

				_dbContext.ProgressiveTaxRateSettings.UpdateRange(existingTaxRates);

				await _dbContext.SaveChangesAsync();

				return Result<Nothing>.Ok(Nothing.Value);
			}
			catch (SqlException ex)
			{
				_logger.LogError(ex, Errors.SalaryConfig.SaveSalaryConfigDatabaseError.Message);
				return Errors.SalaryConfig.SaveSalaryConfigDatabaseError;
			}
		}
	}
}