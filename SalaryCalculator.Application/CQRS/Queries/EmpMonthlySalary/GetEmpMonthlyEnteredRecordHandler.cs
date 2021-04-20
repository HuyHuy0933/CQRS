using MediatR;
using SalaryCalculator.Application.Models.RequestModel;
using SalaryCalculator.Application.Models.ResponseModel;
using SalaryCalculator.Core.Framework;
using SalaryCalculator.Core.Repository;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SalaryCalculator.Core.Common.Extensions;

namespace SalaryCalculator.Application.CQRS.Queries.EmpMonthlySalary
{
	public class GetEmpMonthlyEnteredRecordHandler : IRequestHandler
		<GetEmpMonthlyEnteredRecordRequest, Result<GetEmpMonthlyEnteredRecordResponse>>
	{
		private readonly IEmployeeMonthlyEnteredRecordRepository _empMonthlyEnteredRecordRepo;
		private readonly IEmployeeProfileRepository _profileRepo;

		public GetEmpMonthlyEnteredRecordHandler(
			IEmployeeMonthlyEnteredRecordRepository empMonthlyEnteredRecordRepo,
			IEmployeeProfileRepository profileRepo)
		{
			_empMonthlyEnteredRecordRepo = empMonthlyEnteredRecordRepo;
			_profileRepo = profileRepo;
		}
		public async Task<Result<GetEmpMonthlyEnteredRecordResponse>> Handle(
			GetEmpMonthlyEnteredRecordRequest request,
			CancellationToken cancellationToken)
		{
			var recordsRepo = await _empMonthlyEnteredRecordRepo.GetAsync(request.YearMonth, request.Key);
			var profiles = await _profileRepo.GetAsync(request.YearMonth);

			return profiles
				.Map(() => recordsRepo)
				.Map(x => x.records.Where(y => profiles.Data.Any(z => y.Email == z.Email)))
				.Map(x => new GetEmpMonthlyEnteredRecordResponse(
					x.Select(y => new PartialGetEmpMonthlyEnteredRecordResponse(
						fullname: y.Fullname,
						email: y.Email,
						grossContractSalary: y.grossContractSalary.Amount,
						probationGrossContractSalary: y.ProbationGrossContractSalary.Amount,
						actualWorkingDays: y.ActualWorkingDays,
						probationWorkingDays: y.ProbationWorkingDays,
						taxableAnnualLeave: y.TaxableAnnualLeave.Amount,
						taxable13MonthSalary: y.Taxable13MonthSalary.Amount,
						taxableOthers: y.TaxableOthers.Amount,
						nonTaxableAllowance: y.NonTaxableAllowances.Select(_ => _.Amount.Value).Sum(),
						paymentAdvance: y.PaymentAdvance.Amount,
						adjustmentAddition: y.AdjustmentAdditions.Select(_ => _.Amount.Value).Sum(),
						adjustmentDeduction: y.AdjustmentDeductions.Select(_ => _.Amount.Value).Sum(),
						currency: y.grossContractSalary.Amount.Currency
				)).ToList(),
					standardWorkingDays: recordsRepo.Data.standardWorkingDays
			));
		}
	}
}
