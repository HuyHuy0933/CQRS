using CsvHelper;
using MediatR;
using Microsoft.Extensions.Logging;
using SalaryCalculator.Application.Common.Helpers;
using SalaryCalculator.Application.Models.RequestModel;
using SalaryCalculator.Application.Models.ResponseModel;
using SalaryCalculator.Core;
using SalaryCalculator.Core.Common;
using SalaryCalculator.Core.Common.Extensions;
using SalaryCalculator.Core.Domain;
using SalaryCalculator.Core.Framework;
using SalaryCalculator.Core.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SalaryCalculator.Application.CQRS.Queries.EmpMonthlySalary
{
	public class ExportEmpMonthlySalaryHandler : IRequestHandler<ExportEmpMonthlySalaryRequest,
		Result<ExportEmpMonthlySalaryResponse>>
	{
		private readonly IMediator _mediator;
		private readonly ISalaryConfigRepository _salaryConfigRepo;
		private readonly IFileHelper _fileHelper;
		private readonly ILogger<ExportEmpMonthlySalaryHandler> _logger;
		public ExportEmpMonthlySalaryHandler(
			IMediator mediator,
			ISalaryConfigRepository salaryConfigRepo,
			IFileHelper fileHelper,
			ILogger<ExportEmpMonthlySalaryHandler> logger)
		{
			_mediator = mediator;
			_salaryConfigRepo = salaryConfigRepo;
			_fileHelper = fileHelper;
			_logger = logger;
		}
		public async Task<Result<ExportEmpMonthlySalaryResponse>> Handle(ExportEmpMonthlySalaryRequest request, CancellationToken cancellationToken)
		{
			var salaryConfig = await _salaryConfigRepo.GetAsync();

			var previewResult = await _mediator.Send(new PreviewEmpMonthlySalaryRequest
			{
				Key = request.Key,
				YearMonth = request.YearMonth
			});

			try
			{
				return await salaryConfig.Map(() => previewResult).Map(x =>
				{
					var salaries = x.Salaries.GroupBy(x => x.Email).Select((y, index) =>
					{
						var salary = y.FirstOrDefault();
						return new ExportEmpMonthlySalary
						{
							No = $"{index + 1}",
							Fullname = salary.Fullname,
							Email = salary.Email,
							EmployeeType = y.Count() == 2 ? $"{EmployeeTypeEnum.Probation}/{EmployeeTypeEnum.Permanent}"
										  : salary.EmployeeType,
							Position = salary.Position,
							StandardWorkingDays = salary.StandardWorkingDays,
							ActualWorkingDays = y.Sum(z => z.ActualWorkingDays),
							GrossContractSalary = salary.GrossContractSalary,
							InsuranceSalary = salary.InsuranceSalary,
							ActualGrossSalary = y.Sum(z => z.ActualGrossSalary),
							NonTaxableAllowances = y.Sum(z => z.NonTaxableAllowances),
							Taxable13MonthSalary = y.Sum(z => z.Taxable13MonthSalary),
							TaxableAnnualLeave = y.Sum(z => z.TaxableAnnualLeave),
							TaxableOthers = y.Sum(z => z.TaxableOthers),
							TotalMonthlyIncome = y.Sum(z => z.TotalMonthlyIncome),
							TaxableIncome = y.Sum(z => z.TaxableIncome),
							EmployeeSocialInsurance = y.Sum(z => z.EmployeeSocialInsurance),
							EmployeeHealthcareInsurance = y.Sum(z => z.EmployeeHealthcareInsurance),
							EmployeeUnemploymentInsurance = y.Sum(z => z.EmployeeUnemploymentInsurance),
							EmployeeUnionFee = y.Sum(z => z.EmployeeUnionFee),
							EmployerSocialInsurance = y.Sum(z => z.EmployerSocialInsurance),
							EmployerHealthcareInsurance = y.Sum(z => z.EmployerHealthcareInsurance),
							EmployerUnemploymentInsurance = y.Sum(z => z.EmployerUnemploymentInsurance),
							EmployerUnionFee = y.Sum(z => z.EmployerUnionFee),
							PersonalDeduction = salary.PersonalDeduction,
							NumberOfDependants = salary.NumberOfDependants,
							DependantDeduction = salary.DependantDeduction,
							AssessableIncome = y.Sum(z => z.AssessableIncome),
							NetIncome = y.Sum(z => z.NetIncome),
							PIT = y.Sum(z => z.PIT),
							TotalSalaryCost = y.Sum(z => z.TotalSalaryCost),
							PaymentAdvance = salary.PaymentAdvance,
							AdjustmentDeduction = salary.AdjustmentDeduction,
							AdjustmentAddition = salary.AdjustmentAddition,
							NetPayment = y.Sum(z => z.NetPayment),
						};
					}).ToList();

					salaries.Add(new ExportEmpMonthlySalary
					{
						No = "Grand Total",
						StandardWorkingDays = salaries.Sum(y => y.StandardWorkingDays),
						ActualWorkingDays = salaries.Sum(y => y.ActualWorkingDays),
						GrossContractSalary = salaries.Sum(y => y.GrossContractSalary),
						InsuranceSalary = salaries.Sum(y => y.InsuranceSalary),
						ActualGrossSalary = salaries.Sum(y => y.ActualGrossSalary),
						NonTaxableAllowances = salaries.Sum(y => y.NonTaxableAllowances),
						Taxable13MonthSalary = salaries.Sum(y => y.Taxable13MonthSalary),
						TaxableAnnualLeave = salaries.Sum(y => y.TaxableAnnualLeave),
						TaxableOthers = salaries.Sum(y => y.TaxableOthers),
						TotalMonthlyIncome = salaries.Sum(y => y.TotalMonthlyIncome),
						TaxableIncome = salaries.Sum(y => y.TaxableIncome),
						EmployeeSocialInsurance = salaries.Sum(y => y.EmployeeSocialInsurance),
						EmployeeHealthcareInsurance = salaries.Sum(y => y.EmployeeHealthcareInsurance),
						EmployeeUnemploymentInsurance = salaries.Sum(y => y.EmployeeUnemploymentInsurance),
						EmployeeUnionFee = salaries.Sum(y => y.EmployeeUnionFee),
						EmployerSocialInsurance = salaries.Sum(y => y.EmployerSocialInsurance),
						EmployerHealthcareInsurance = salaries.Sum(y => y.EmployerHealthcareInsurance),
						EmployerUnemploymentInsurance = salaries.Sum(y => y.EmployerUnemploymentInsurance),
						EmployerUnionFee = salaries.Sum(y => y.EmployerUnionFee),
						PersonalDeduction = salaries.Sum(y => y.PersonalDeduction),
						NumberOfDependants = salaries.Sum(y => y.NumberOfDependants),
						DependantDeduction = salaries.Sum(y => y.DependantDeduction),
						AssessableIncome = salaries.Sum(y => y.AssessableIncome),
						NetIncome = salaries.Sum(y => y.NetIncome),
						PIT = salaries.Sum(y => y.PIT),
						TotalSalaryCost = salaries.Sum(y => y.TotalSalaryCost),
						PaymentAdvance = salaries.Sum(y => y.PaymentAdvance),
						AdjustmentDeduction = salaries.Sum(y => y.AdjustmentDeduction),
						AdjustmentAddition = salaries.Sum(y => y.AdjustmentAddition),
						NetPayment = salaries.Sum(y => y.NetPayment)
					});

					return salaries;
				})
				.MapAsync(x => _fileHelper.CreateCsvByteArrayAsync(x, salaryConfig.Data, request.YearMonth))
				.MapAsync(x => new ExportEmpMonthlySalaryResponse
				{
					CsvSalaries = x
				});
			}
			catch (CsvHelperException ex)
			{
				_logger.LogError(ex, Errors.ExportEmpMonthlySalary.ExportWriteCSVError.Message);
				return Errors.ExportEmpMonthlySalary.ExportWriteCSVError;
			}
		}
	}
}
