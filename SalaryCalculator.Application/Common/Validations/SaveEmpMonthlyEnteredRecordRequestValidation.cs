using FluentValidation;
using SalaryCalculator.Application.Models.RequestModel;
using SalaryCalculator.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SalaryCalculator.Application.Common.Validations
{
	public class SaveEmpMonthlyEnteredRecordRequestValidation : AbstractValidator<SaveEmpMonthlyEnteredRecordRequest>
	{
		public SaveEmpMonthlyEnteredRecordRequestValidation()
		{
			this.CascadeMode = CascadeMode.Stop;

			RuleFor(x => x.YearMonth).NotEmpty().WithMessage(ValiationErrors.YEAR_MONTH_NULL_ERROR)
				.Must(x => YearMonth.TryParse(x)).WithMessage(ValiationErrors.YEAR_MONTH_INVALID_FORMAT_ERROR);
			RuleFor(x => x.Key).NotEmpty().WithMessage(ValiationErrors.KEY_EMPTY_ERROR);
			RuleFor(x => x.StandardWorkingDays).GreaterThanOrEqualTo(1).WithMessage(ValiationErrors.STANDARD_WORKING_DAY_MIN_ERROR)
				.LessThanOrEqualTo(23).WithMessage(ValiationErrors.STANDARD_WORKING_DAY_MAX_ERROR);
			RuleFor(x => x.Records).Must(x => !x.GroupBy(y => y.Email).Any(g => g.Count() > 1))
				.WithMessage(ValiationErrors.RECORD_DUPLICATED_ERROR);
			RuleForEach(x => x.Records).ChildRules(record =>
			{
				record.CascadeMode = CascadeMode.Stop;
				record.RuleFor(y => y.Email).EmailAddress().WithMessage(ValiationErrors.EMAIL_INVALID_ERROR);
				record.RuleFor(y => y.Fullname).NotEmpty().WithMessage(ValiationErrors.FULLNAME_EMPTY_ERROR);
				record.RuleFor(y => y.Currency).NotEmpty().WithMessage(ValiationErrors.CURRENCY_EMPTY_ERROR);
				record.RuleFor(y => y.GrossContractSalary).GreaterThanOrEqualTo(0).WithMessage(ValiationErrors.GROSS_CONTRACT_SALARY_NEGATIVE_ERROR);
				record.RuleFor(y => y.ProbationWorkingDays).GreaterThanOrEqualTo(0).WithMessage(ValiationErrors.PROBATION_WORKING_DAY_NEGATIVE_ERROR);
				record.RuleFor(y => y.ActualWorkingDays).GreaterThanOrEqualTo(0).WithMessage(ValiationErrors.ACTUAL_WORKING_DAY_NEGATIVE_ERROR);
				record.RuleFor(y => y.NonTaxableAllowance).GreaterThanOrEqualTo(0).WithMessage(ValiationErrors.NON_TAXABLE_ALLOWANCE_NEGATIVE_ERROR);
				record.RuleFor(y => y.TaxableAnnualLeave).GreaterThanOrEqualTo(0).WithMessage(ValiationErrors.TAXABLE_ANNUAL_LEAVE_NEGATIVE_ERROR);
				record.RuleFor(y => y.Taxable13MonthSalary).GreaterThanOrEqualTo(0).WithMessage(ValiationErrors.TAXABLE_13_MONTH_SALARY_NEGATIVE_ERROR);
				record.RuleFor(y => y.TaxableOthers).GreaterThanOrEqualTo(0).WithMessage(ValiationErrors.TAXABLE_OTHERS_NEGATIVE_ERROR);
				record.RuleFor(y => y.PaymentAdvance).GreaterThanOrEqualTo(0).WithMessage(ValiationErrors.PAYMENT_ADVANCE_NEGATIVE_ERROR);
				record.RuleFor(y => y.AdjustmentAddition).GreaterThanOrEqualTo(0).WithMessage(ValiationErrors.ADJUSTMENT_ADDITION_NEGATIVE_ERROR);
				record.RuleFor(y => y.AdjustmentDeduction).GreaterThanOrEqualTo(0).WithMessage(ValiationErrors.ADJUSTMENT_DEDUCTION_NEGATIVE_ERROR);
				record.RuleFor(y => y.Currency).NotEmpty().WithMessage(ValiationErrors.CURRENCY_EMPTY_ERROR);
			});

		}
	}
}
