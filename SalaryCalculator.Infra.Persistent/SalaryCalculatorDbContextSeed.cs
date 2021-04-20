using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using SalaryCalculator.Core;
using SalaryCalculator.Core.Domain;
using SalaryCalculator.Core.ValueObjects;
using SalaryCalculator.Infra.Persistent.Entities;

namespace SalaryCalculator.Infra.Persistent
{
    public static class SalaryCalculatorDbContextSeed
    {
        public static async Task SeedDataAsync(SalaryCalculatorDbContext context)
        {
            if ((await context.SalaryCurrencies.FirstOrDefaultAsync()) is null)
            {
                var salaryCurrency = new SalaryCurrency()
                {
                    Id = Guid.NewGuid(),
                    IsDeleted = false,
                    Value = Currency.VND.Value,
                    SalarySettings = new List<SalarySetting>()
                    {
                        new SalarySetting()
                        {
                            CommonMinimumWage = 1_490_000m,
                            RegionalMinimumWage = 4_729_400m,
                            CoefficientSocialCare = 20m,
                            EmployerSocialInsuranceRate = 17.5m,
                            EmployeeSocialInsuranceRate = 8m,
                            EmployerHealthCareInsuranceRate = 3m,
                            EmployeeHealthCareInsuranceRate = 1.5m,
                            EmployerUnemploymentInsuranceRate = 1m,
                            EmployeeUnemploymentInsuranceRate = 1m,
                            MinimumNonWorkingDay = 14,
                            EmployeeUnionFeeRate = 1m,
                            EmployerUnionFeeRate = 2m,
                            MaximumUnionFeeRate = 10m,
                            PersonalDeduction = 11_000_000m,
                            DependantDeduction = 4_400_000,
                            DefaultProbationTaxRate = 10m,
                            IsInsurancePaidFullSalary = true,
                            InsurancePaidAmount = decimal.Zero,
                            IsDeleted = false,
                        }
                    },
                    ProgressiveTaxRateSettings = new List<ProgressiveTaxRateSetting>()
                    {
                        new ProgressiveTaxRateSetting()
                        {
                            LowerBound = 0m,
                            UpperBound = 5_000_000m,
                            TaxRateLevel = (int) ProgressiveTaxRateLevel.One,
                            Rate = 5,
                        },
                        new ProgressiveTaxRateSetting()
                        {
                            LowerBound = 5_000_001m,
                            UpperBound = 10_000_000m,
                            TaxRateLevel = (int) ProgressiveTaxRateLevel.Two,
                            Rate = 10,
                        },
                        new ProgressiveTaxRateSetting()
                        {
                            LowerBound = 10_000_001m,
                            UpperBound = 18_000_000m,
                            TaxRateLevel = (int) ProgressiveTaxRateLevel.Three,
                            Rate = 15,
                        },
                        new ProgressiveTaxRateSetting()
                        {
                            LowerBound = 18_000_001m,
                            UpperBound = 32_000_000m,
                            TaxRateLevel = (int) ProgressiveTaxRateLevel.Four,
                            Rate = 20,
                        },
                        new ProgressiveTaxRateSetting()
                        {
                            LowerBound = 32_000_001m,
                            UpperBound = 52_000_000m,
                            TaxRateLevel = (int) ProgressiveTaxRateLevel.Five,
                            Rate = 25,
                        },
                        new ProgressiveTaxRateSetting()
                        {
                            LowerBound = 52_000_001m,
                            UpperBound = 80_000_000m,
                            TaxRateLevel = (int) ProgressiveTaxRateLevel.Six,
                            Rate = 30,
                        },
                        new ProgressiveTaxRateSetting()
                        {
                            LowerBound = 80_000_001m,
                            UpperBound = 80_000_001m * 5,
                            TaxRateLevel = (int) ProgressiveTaxRateLevel.Seven,
                            Rate = 35,
                        }
                    }
                };
                context.SalaryCurrencies.Add(salaryCurrency);
                await context.SaveChangesAsync();
            }
        }
    }
}
