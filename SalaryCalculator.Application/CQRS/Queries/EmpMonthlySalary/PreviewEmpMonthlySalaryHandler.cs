using MediatR;
using SalaryCalculator.Application.Models.RequestModel;
using SalaryCalculator.Application.Models.ResponseModel;
using SalaryCalculator.Core;
using SalaryCalculator.Core.Domain;
using SalaryCalculator.Core.Framework;
using SalaryCalculator.Core.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SalaryCalculator.Core.Common.Extensions;
using SalaryCalculator.Core.Common;

namespace SalaryCalculator.Application.CQRS.Queries.EmpMonthlySalary
{
	public class PreviewEmpMonthlySalaryHandler : IRequestHandler<PreviewEmpMonthlySalaryRequest, Result<PreviewEmpMonthlySalaryResponse>>
	{
		private readonly IEmployeeMonthlyEnteredRecordRepository _recordRepo;
		private readonly ISalaryConfigRepository _configRepo;
		private readonly IEmployeeProfileRepository _profileRepo;
		private readonly IEmployeeMonthlySalaryRepository _salaryRepo;

		public PreviewEmpMonthlySalaryHandler(
			IEmployeeMonthlyEnteredRecordRepository recordRepo,
			ISalaryConfigRepository configRepo,
			IEmployeeProfileRepository profileRepo,
			IEmployeeMonthlySalaryRepository salaryRepo)
		{
			_recordRepo = recordRepo;
			_configRepo = configRepo;
			_profileRepo = profileRepo;
			_salaryRepo = salaryRepo;
		}

		public async Task<Result<PreviewEmpMonthlySalaryResponse>> Handle(
			PreviewEmpMonthlySalaryRequest request,
			CancellationToken cancellationToken)
		{
			// get calculated salary
			var calculatedSalaries = await _salaryRepo.GetAsync(request.Key, request.YearMonth);
			if (calculatedSalaries.Failure) return calculatedSalaries.Error;

			if (calculatedSalaries.Data.Length > 0)
			{
				return Result<PreviewEmpMonthlySalaryResponse>.Ok(new PreviewEmpMonthlySalaryResponse(
				calculatedSalaries.Data.Select(x => new PreviewEmpMonthlySalary(
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
					taxableAllowances: x.TaxableAllowances,
					nonTaxableAllowances: x.NonTaxableAllowances.Select(x => x.Amount.Value).Sum(),
					netPayment: x.NetPayment,
					adjustmentAddition: x.AdjustmentAddition,
					adjustmentDeduction: x.AdjustmentDeduction)).OrderBy(x => x.Fullname).ToArray()));
			}

			var salaryConfig = await _configRepo.GetAsync();
			var recordsRepo = await _recordRepo.GetAsync(request.YearMonth, request.Key);
			var profilesRepo = await _profileRepo.GetAsync(request.YearMonth);

			var records = salaryConfig.Map(() => profilesRepo).Map(() => recordsRepo)
				.Map(x => recordsRepo.Data.records.Where(y => profilesRepo.Data.Any(z =>
					y.Email == z.Email)).ToList());

			var result = await records.Map(x => MapModelToCalculateSalary(x, profilesRepo.Data,
					recordsRepo.Data.standardWorkingDays))
				.Map(x => Main.GeneratePayRollRecord(x.ToArray(), salaryConfig.Data))
				// add new month records if there is no current month records.
				.ExecuteAsync(x => _recordRepo.SaveAsync(records.Data, request.YearMonth, request.Key,
					recordsRepo.Data.standardWorkingDays))
				// save calculated salary to db
				.ExecuteAsync(x => _salaryRepo.SaveAsync(x, request.Key, request.YearMonth));

			return result.Map(x => new PreviewEmpMonthlySalaryResponse(
				x.Select(y => new PreviewEmpMonthlySalary(
					fullname: y.Fullname,
					email: y.Email,
					employeeType: y.EmployeeType,
					position: y.Position,
					numberOfDependants: y.NumberOfDependants,
					standardWorkingDays: y.StandardWorkingDays,
					actualWorkingDays: y.ActualWorkingDays,
					grossContractSalary: y.GrossContractSalary,
					insuranceSalary: y.InsuranceSalary,
					actualGrossSalary: y.ActualGrossSalary,
					taxableIncome: y.TaxableIncome,
					totalMonthlyIncome: y.TotalMonthlyIncome,
					employeeSocialInsurance: y.EmployeeSocialInsurance,
					employeeHealthcareInsurance: y.EmployeeHealthcareInsurance,
					employeeUnemploymentInsurance: y.EmployeeUnemploymentInsurance,
					employeeUnionFee: y.EmployeeUnionFee,
					employerSocialInsurance: y.EmployerSocialInsurance,
					employerHealthcareInsurance: y.EmployerHealthcareInsurance,
					employerUnemploymentInsurance: y.EmployerUnemploymentInsurance,
					employerUnionFee: y.EmployerUnionFee,
					personalDeduction: y.PersonalDeduction,
					dependantDeduction: y.DependantDeduction,
					assessableIncome: y.AssessableIncome,
					netIncome: y.NetIncome,
					pit: y.PIT,
					totalSalaryCost: y.TotalSalaryCost,
					paymentAdvance: y.PaymentAdvance,
					taxableAllowances: y.TaxableAllowances,
					nonTaxableAllowances: y.NonTaxableAllowances.Select(z => z.Amount.Value).Sum(),
					netPayment: y.NetPayment,
					adjustmentAddition: y.AdjustmentAddition,
					adjustmentDeduction: y.AdjustmentDeduction)).ToArray()));
		}

		private List<EmployeeMonthlyRecord> MapModelToCalculateSalary(
			List<EmployeeMonthlyEnteredRecord> records,
			List<EmployeeProfile> profiles,
			int standardWorkingDays)
		{
			List<EmployeeMonthlyRecord> employeeMonthlyRecords = new List<EmployeeMonthlyRecord>();

			foreach (var enteredRecord in records)
			{
				var profile = profiles.FirstOrDefault(y => y.Email == enteredRecord.Email);

				// if there is any employee who is on probation in the first days and on permanent in the last days of a month
				// seperate enteredRecord into two records to calculate probation's salary and permanent'salary of the employee
				if (enteredRecord.ActualWorkingDays > 0 && enteredRecord.ProbationWorkingDays > 0)
				{
					employeeMonthlyRecords.Add(new EmployeeMonthlyRecord(
						name: profile.Fullname,
						email: enteredRecord.Email,
						workingDays: enteredRecord.ActualWorkingDays,
						employeeType: nameof(EmployeeTypeEnum.Permanent),
						numberOfDependants: profile.DependantEndDates.Length,
						probationWorkingDays: 0,
						standardWorkingDays: standardWorkingDays,
						inUnion: profile.InUnion,
						grossContractedSalary: enteredRecord.grossContractSalary,
						taxableAllowances: new TaxableAllowance[]
						{
							enteredRecord.TaxableAnnualLeave,
							enteredRecord.Taxable13MonthSalary,
							enteredRecord.TaxableOthers
						},
						nonTaxableAllowances: enteredRecord.NonTaxableAllowances,
						paymentAdvance: enteredRecord.PaymentAdvance,
						adjustmentAdditions: enteredRecord.AdjustmentAdditions,
						adjustmentDeduction: enteredRecord.AdjustmentDeductions,
						position: profile.Position, 
						isForeigner: profile.IsForeigner));

					employeeMonthlyRecords.Add(new EmployeeMonthlyRecord(
						name: profile.Fullname,
						email: enteredRecord.Email,
						workingDays: 0,
						employeeType: nameof(EmployeeTypeEnum.Probation),
						numberOfDependants: profile.DependantEndDates.Length,
						probationWorkingDays: enteredRecord.ProbationWorkingDays,
						standardWorkingDays: standardWorkingDays,
						inUnion: profile.InUnion,
						grossContractedSalary: enteredRecord.ProbationGrossContractSalary,
						taxableAllowances: new TaxableAllowance[]
						{
							enteredRecord.TaxableAnnualLeave,
							enteredRecord.Taxable13MonthSalary,
							enteredRecord.TaxableOthers
						},
						nonTaxableAllowances: enteredRecord.NonTaxableAllowances,
						paymentAdvance: enteredRecord.PaymentAdvance,
						adjustmentAdditions: enteredRecord.AdjustmentAdditions,
						adjustmentDeduction: enteredRecord.AdjustmentDeductions,
						position: profile.Position,
						isForeigner: profile.IsForeigner));
				}
				else
				{
					employeeMonthlyRecords.Add(new EmployeeMonthlyRecord(
					name: profile.Fullname,
					email: enteredRecord.Email,
					workingDays: enteredRecord.ActualWorkingDays,
					employeeType: profile.EmployeeType,
					numberOfDependants: profile.DependantEndDates.Length,
					probationWorkingDays: enteredRecord.ProbationWorkingDays,
					standardWorkingDays: standardWorkingDays,
					inUnion: profile.InUnion,
					grossContractedSalary: enteredRecord.grossContractSalary,
					taxableAllowances: new TaxableAllowance[]
					{
						enteredRecord.TaxableAnnualLeave,
						enteredRecord.Taxable13MonthSalary,
						enteredRecord.TaxableOthers
					},


					nonTaxableAllowances: enteredRecord.NonTaxableAllowances,
					paymentAdvance: enteredRecord.PaymentAdvance,
					adjustmentAdditions: enteredRecord.AdjustmentAdditions,
					adjustmentDeduction: enteredRecord.AdjustmentDeductions,
					position: profile.Position,
					isForeigner: profile.IsForeigner));
				}
			}

			return employeeMonthlyRecords;
		}
	}
}
