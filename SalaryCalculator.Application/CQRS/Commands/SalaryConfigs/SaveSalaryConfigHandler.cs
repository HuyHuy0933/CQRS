using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SalaryCalculator.Application.Models.RequestModel;
using SalaryCalculator.Application.Models.ResponseModel;
using SalaryCalculator.Core;
using SalaryCalculator.Core.Domain;
using SalaryCalculator.Core.Framework;
using SalaryCalculator.Core.Repository;
using SalaryCalculator.Core.ValueObjects;
using SalaryCalculator.Core.Common.Extensions;

namespace SalaryCalculator.Application.CQRS.Commands.SalaryConfigs
{
	public class SaveSalaryConfigHandler : IRequestHandler<SaveSalaryConfigRequest, Result<SaveSalaryConfigResponse>>
	{
		private readonly ISalaryConfigRepository _salaryConfigRepository;
		private readonly IEmployeeMonthlySalaryRepository _empMonthlySalaryRepo;
		public SaveSalaryConfigHandler(
			ISalaryConfigRepository salaryConfigRepository,
			IEmployeeMonthlySalaryRepository empMonthlySalaryRepo)
		{
			_salaryConfigRepository = salaryConfigRepository;
			_empMonthlySalaryRepo = empMonthlySalaryRepo;
		}
		public async Task<Result<SaveSalaryConfigResponse>> Handle(SaveSalaryConfigRequest request, CancellationToken cancellationToken)
		{
			var currency = new Currency(request.SalarySetting.Currency);
			var result = await _salaryConfigRepository.SaveAsync(new SalaryConfig
				(
					id: request.SalarySetting.Id,
					commonMinimumWage: new Money(request.SalarySetting.CommonMinimumWage, currency),
					regionalMinimumWage: new Money(request.SalarySetting.RegionalMinimumWage, currency),
					coefficientSocialCare: request.SalarySetting.CoefficientSocialCare,
					employerSocialInsuranceRate: request.SalarySetting.EmployerSocialInsuranceRate,
					employeeSocialInsuranceRate: request.SalarySetting.EmployeeSocialInsuranceRate,
					healthCareInsuranceEmployerRate: request.SalarySetting.EmployerHealthCareInsuranceRate,
					healthCareInsuranceEmployeeRate: request.SalarySetting.EmployeeHealthCareInsuranceRate,
					unemploymentInsuranceEmployerRate: request.SalarySetting.EmployerUnemploymentInsuranceRate,
					unemploymentInsuranceEmployeeRate: request.SalarySetting.EmployeeUnemploymentInsuranceRate,
					foreignEmployerSocialInsuranceRate: request.SalarySetting.ForeignEmployerSocialInsuranceRate,
					foreignEmployeeSocialInsuranceRate: request.SalarySetting.ForeignEmployeeSocialInsuranceRate,
					foreignHealthCareInsuranceEmployeeRate: request.SalarySetting.ForeignEmployeeHealthCareInsuranceRate,
					foreignHealthCareInsuranceEmployerRate: request.SalarySetting.ForeignEmployerHealthCareInsuranceRate,
					foreignUnemploymentInsuranceEmployeeRate: request.SalarySetting.ForeignEmployeeUnemploymentInsuranceRate,
					foreignUnemploymentInsuranceEmployerRate: request.SalarySetting.ForeignEmployerUnemploymentInsuranceRate,
					minimumNonWorkingDay: request.SalarySetting.MinimumNonWorkingDay,
					employeeUnionFeeRate: request.SalarySetting.EmployeeUnionFeeRate,
					employerUnionFeeRate: request.SalarySetting.EmployerUnionFeeRate,
					maximumUnionFeeRate: request.SalarySetting.MaximumUnionFeeRate,
					personalDeduction: new Money(request.SalarySetting.PersonalDeduction, currency),
					dependantDeduction: new Money(request.SalarySetting.DependantDeduction, currency),
					defaultProbationTaxRate: request.SalarySetting.DefaultProbationTaxRate,
					isInsurancePaidFullSalary: request.SalarySetting.IsInsurancePaidFullSalary,
					insurancePaidAmount: new Money(request.SalarySetting.InsurancePaidAmount, currency),
					progressiveTaxRateLookUpTable: new ProgressiveTaxRateLookUpTable(
						request.ProgressiveTaxRates.Select(
							x => new ProgressiveTaxRate
							(
								lowerBound: new Money(x.LowerBound, currency),
								upperBound: new Money(x.UpperBound, currency),
								rate: x.Rate,
								progressiveTaxRateLevel: x.TaxRateLevel
							)).ToArray()
						)
				)
			);

			var markResult = await _empMonthlySalaryRepo.MarkNotLatestAsync(request.YearMonth);

			return result.Map(() => markResult).Map(x => new SaveSalaryConfigResponse());
		}
	}
}
