using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SalaryCalculator.Application.Models.RequestModel;
using SalaryCalculator.Application.Models.ResponseModel;
using SalaryCalculator.Core.Framework;
using SalaryCalculator.Core.Repository;
using SalaryCalculator.Core.Common.Extensions;

namespace SalaryCalculator.Application.CQRS.Queries.SalaryConfigs
{
	public class GetSalaryConfigHandler : IRequestHandler<GetSalaryConfigRequest, Result<GetSalaryConfigResponse>>
	{
		private readonly ISalaryConfigRepository _salaryConfigRepository;

		public GetSalaryConfigHandler(
			ISalaryConfigRepository salaryConfigRepository)
		{
			_salaryConfigRepository = salaryConfigRepository;
		}
		public async Task<Result<GetSalaryConfigResponse>> Handle(GetSalaryConfigRequest request, CancellationToken cancellationToken)
		{
			var salaryConfig = await _salaryConfigRepository.GetAsync();

			return salaryConfig.Map(x => new GetSalaryConfigResponse(
					salarySetting: new SalarySettingResponse(
						id: x.Id,
						commonMinimumWage: x.CommonMinimumWage,
						regionalMinimumWage: x.RegionalMinimumWage,
						coefficientSocialCare: x.CoefficientSocialCare,
						employerSocialInsuranceRate: x.EmployerSocialInsuranceRate,
						employeeSocialInsuranceRate: x.EmployeeSocialInsuranceRate,
						employerHealthCareInsuranceRate: x.EmployerHealthCareInsuranceRate,
						employeeHealthCareInsuranceRate: x.EmployeeHealthCareInsuranceRate,
						employeeUnemploymentInsuranceRate: x.EmployeeUnemploymentInsuranceRate,
						employerUnemploymentInsuranceRate: x.EmployerUnemploymentInsuranceRate,
						foreignEmployerSocialInsuranceRate: x.ForeignEmployerSocialInsuranceRate,
						foreignEmployeeSocialInsuranceRate: x.ForeignEmployeeSocialInsuranceRate,
						foreignHealthCareInsuranceEmployeeRate: x.ForeignEmployeeHealthCareInsuranceRate,
						foreignHealthCareInsuranceEmployerRate: x.ForeignEmployerHealthCareInsuranceRate,
						foreignUnemploymentInsuranceEmployeeRate: x.ForeignEmployeeUnemploymentInsuranceRate,
						foreignUnemploymentInsuranceEmployerRate: x.ForeignEmployerUnemploymentInsuranceRate,
						employeeUnionFeeRate: x.EmployeeUnionFeeRate,
						employerUnionFeeRate: x.EmployerUnionFeeRate,
						maximumUnionFeeRate: x.MaximumUnionFeeRate,
						defaultProbationTaxRate: x.DefaultProbationTaxRate,
						isInsurancePaidFullSalary: x.IsInsurancePaidFullSalary,
						insurancePaidAmount: x.InsurancePaidAmount,
						personalDeduction: x.PersonalDeduction,
						dependantDeduction: x.DependantDeduction,
						currency: x.CommonMinimumWage.Currency,
						minimumNonWorkingDay: x.MinimumNonWorkingDay),
					progressiveTaxRates: x.ProgressiveTaxRateLookUpTable.AsReadOnlyCollection()
						.Select(x => new ProgressiveTaxRateResponse(
							lowerBound: x.LowerBound,
							upperBound: x.UpperBound,
							taxRateLevel: x.ProgressiveTaxRateLevel,
							rate: x.Rate)).ToList()
					));
		}
	}
}
