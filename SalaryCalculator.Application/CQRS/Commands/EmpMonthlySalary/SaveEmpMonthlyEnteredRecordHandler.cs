using MediatR;
using SalaryCalculator.Application.Models.RequestModel;
using SalaryCalculator.Application.Models.ResponseModel;
using SalaryCalculator.Core;
using SalaryCalculator.Core.Common;
using SalaryCalculator.Core.Domain;
using SalaryCalculator.Core.Framework;
using SalaryCalculator.Core.Repository;
using SalaryCalculator.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SalaryCalculator.Core.Common.Extensions;

namespace SalaryCalculator.Application.CQRS.Commands.EmpMonthlySalary
{
	public class SaveEmpMonthlyEnteredRecordHandler : 
		IRequestHandler<SaveEmpMonthlyEnteredRecordRequest, Result<SaveEmpMonthlyEnteredRecordResponse>>
	{
		private readonly IEmployeeMonthlyEnteredRecordRepository _empMonthlyEnteredRecordRepo;
		private readonly IEmployeeMonthlySalaryRepository _empMonthlySalaryRepo;

		public SaveEmpMonthlyEnteredRecordHandler(
			IEmployeeMonthlyEnteredRecordRepository empMonthlyEnteredRecordRepo,
			IEmployeeMonthlySalaryRepository empMonthlySalaryRepo)
		{
			_empMonthlyEnteredRecordRepo = empMonthlyEnteredRecordRepo;
			_empMonthlySalaryRepo = empMonthlySalaryRepo;
		}
		public async Task<Result<SaveEmpMonthlyEnteredRecordResponse>> Handle(
			SaveEmpMonthlyEnteredRecordRequest request, 
			CancellationToken cancellationToken)
		{
			var record = request.Records.Count == 0 
				? await _empMonthlyEnteredRecordRepo.DeleteAsync(request.Key, request.YearMonth) 
				: await _empMonthlyEnteredRecordRepo.SaveAsync(records: request.Records.Select(x =>
				{
					var currency = new Currency(x.Currency);
					return new EmployeeMonthlyEnteredRecord(
						fullname: x.Fullname,
						email: x.Email,
						grossContractSalary: new GrossContractedSalary(new Money(x.GrossContractSalary, currency)),
						probationGrossContractSalary: new GrossContractedSalary(new Money(x.ProbationGrossContractSalary, currency)),
						actualWorkingDays: x.ActualWorkingDays,
						probationWorkingDays: x.ProbationWorkingDays,
						taxableAnnualLeave: new TaxableAllowance(name: Constants.TAXABLE_ANNUAL_LEAVE, 
							amount: new Money(x.TaxableAnnualLeave, currency)),
						taxable13MonthSalary: new TaxableAllowance(name: Constants.TAXABLE_13MONTH_SALARY, 
							amount: new Money(x.Taxable13MonthSalary, currency)),
						taxableOthers: new TaxableAllowance(name: Constants.TAXABLE_OTHERS, 
							amount: new Money(x.TaxableOthers, currency)),
						nonTaxableAllowances: new NonTaxableAllowance[] { new NonTaxableAllowance(
							amount: new Money(x.NonTaxableAllowance, currency),
							name: Constants.NON_TAXABLE_ALLOWANCE) },
						paymentAdvance: new PaymentAdvance(new Money(x.PaymentAdvance, currency)),
						adjustmentAdditions: new AdjustmentAddition[] { new AdjustmentAddition(
							amount: new Money(x.AdjustmentAddition, currency ))},
						adjustmentDeductions: new AdjustmentDeduction[] { new AdjustmentDeduction(
							amount: new Money(x.AdjustmentDeduction, currency ))}
						);
				}).ToList(), yearMonth: request.YearMonth, key: request.Key, request.StandardWorkingDays);

			var markResult = await _empMonthlySalaryRepo.MarkNotLatestAsync(request.YearMonth);

			return record.Map(() => markResult).Map(x => new SaveEmpMonthlyEnteredRecordResponse());
		}
	}
}
