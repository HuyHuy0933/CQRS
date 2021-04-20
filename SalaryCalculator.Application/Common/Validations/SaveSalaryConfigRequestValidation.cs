using FluentValidation;
using SalaryCalculator.Application.Models.RequestModel;
using SalaryCalculator.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SalaryCalculator.Application.Common.Validations
{
	public class SaveSalaryConfigRequestValidation : AbstractValidator<SaveSalaryConfigRequest>
	{
		public SaveSalaryConfigRequestValidation()
		{
			this.CascadeMode = CascadeMode.Stop;

			RuleFor(x => x.YearMonth).NotEmpty().WithMessage(ValiationErrors.YEAR_MONTH_NULL_ERROR)
				.Must(x => YearMonth.TryParse(x)).WithMessage(ValiationErrors.YEAR_MONTH_INVALID_FORMAT_ERROR);
			RuleFor(x => x.ProgressiveTaxRates).NotEmpty().WithMessage(ValiationErrors.PROGRESSIVE_TAX_RATE_EMPTY_ERROR)
				.Must(x => !x.GroupBy(y => y.Rate).Any(g => g.Count() > 1)).WithMessage(ValiationErrors.PROGRESSIVE_TAX_RATE_DUPLICATED_ERROR);
			RuleForEach(x => x.ProgressiveTaxRates).ChildRules(rate =>
			{
				rate.RuleFor(y => y.TaxRateLevel).IsInEnum().WithMessage(ValiationErrors.PROGRESSIVE_TAX_RATE_LEVEL_INVALID_ENUM_ERROR);
			});
			RuleForEach(x => x.ProgressiveTaxRates).ChildRules(rate =>
			{
				rate.RuleFor(y => y.LowerBound).GreaterThanOrEqualTo(0).WithMessage(ValiationErrors.LOWER_BOUND_NEGATIVE_ERROR);
			});
			RuleForEach(x => x.ProgressiveTaxRates).ChildRules(rate =>
			{
				rate.RuleFor(y => y.UpperBound).GreaterThanOrEqualTo(0).WithMessage(ValiationErrors.UPPER_BOUND_NEGATIVE_ERROR);
			});
			RuleForEach(x => x.ProgressiveTaxRates).ChildRules(rate =>
			{
				rate.RuleFor(y => y.Rate).GreaterThanOrEqualTo(0).WithMessage(ValiationErrors.PROGRESSIVE_TAX_RATE_NEGATIVE_ERROR);
			});
			RuleFor(x => x.SalarySetting).NotNull().WithMessage(ValiationErrors.SALARY_SETTING_NULL_ERROR);
			RuleFor(x => x.SalarySetting.Id).NotEmpty().WithMessage(ValiationErrors.ID_EMPTY_ERROR);
			RuleFor(x => x.SalarySetting.CommonMinimumWage).GreaterThanOrEqualTo(0).WithMessage(ValiationErrors.COMMON_MINIUM_WAGE_NEGATIVE_ERROR);
			RuleFor(x => x.SalarySetting.RegionalMinimumWage).GreaterThanOrEqualTo(0).WithMessage(ValiationErrors.REGIONAL_MINIUM_WAGE_NEGATIVE_ERROR);
			RuleFor(x => x.SalarySetting.MinimumNonWorkingDay).GreaterThanOrEqualTo(0).WithMessage(ValiationErrors.MINIUM_NON_WORKING_DAY_NEGATIVE_ERROR);
			RuleFor(x => x.SalarySetting.DefaultProbationTaxRate).GreaterThanOrEqualTo(0).WithMessage(ValiationErrors.DEFAULT_TAX_RATE_NEGATIVE_ERROR);
			RuleFor(x => x.SalarySetting.CoefficientSocialCare).GreaterThanOrEqualTo(0).WithMessage(ValiationErrors.COEFFICIENT_SOCIAL_CARE_NEGATIVE_ERROR);
			RuleFor(x => x.SalarySetting.EmployeeSocialInsuranceRate).GreaterThanOrEqualTo(0).WithMessage(ValiationErrors.EMPPLOYEE_SOCIAL_INSURANCE_RATE_NEGATIVE_ERROR);
			RuleFor(x => x.SalarySetting.EmployeeHealthCareInsuranceRate).GreaterThanOrEqualTo(0).WithMessage(ValiationErrors.EMPPLOYEE_HEALTHCARE_INSURANCE_RATE_NEGATIVE_ERROR);
			RuleFor(x => x.SalarySetting.EmployeeUnemploymentInsuranceRate).GreaterThanOrEqualTo(0).WithMessage(ValiationErrors.EMPPLOYEE_UNEMPLOYMENT_INSURANCE_RATE_NEGATIVE_ERROR);
			RuleFor(x => x.SalarySetting.EmployeeUnionFeeRate).GreaterThanOrEqualTo(0).WithMessage(ValiationErrors.EMPPLOYEE_UNION_FEE_RATE_NEGATIVE_ERROR);
			RuleFor(x => x.SalarySetting.EmployerSocialInsuranceRate).GreaterThanOrEqualTo(0).WithMessage(ValiationErrors.EMPPLOYEE_UNION_FEE_RATE_NEGATIVE_ERROR); 
			RuleFor(x => x.SalarySetting.EmployerHealthCareInsuranceRate).GreaterThanOrEqualTo(0).WithMessage(ValiationErrors.EMPPLOYER_HEALTHCARE_INSURANCE_RATE_NEGATIVE_ERROR);
			RuleFor(x => x.SalarySetting.EmployerUnemploymentInsuranceRate).GreaterThanOrEqualTo(0).WithMessage(ValiationErrors.EMPPLOYER_UNEMPLOYMENT_INSURANCE_RATE_NEGATIVE_ERROR);
			RuleFor(x => x.SalarySetting.EmployerUnionFeeRate).GreaterThanOrEqualTo(0).WithMessage(ValiationErrors.EMPPLOYER_UNION_FEE_RATE_NEGATIVE_ERROR);
			RuleFor(x => x.SalarySetting.MaximumUnionFeeRate).GreaterThanOrEqualTo(0).WithMessage(ValiationErrors.EMPPLOYER_UNION_FEE_RATE_NEGATIVE_ERROR);
			RuleFor(x => x.SalarySetting.PersonalDeduction).GreaterThanOrEqualTo(0).WithMessage(ValiationErrors.EMPPLOYER_PERSONAL_DEDUCTION_NEGATIVE_ERROR);
			RuleFor(x => x.SalarySetting.DependantDeduction).GreaterThanOrEqualTo(0).WithMessage(ValiationErrors.EMPPLOYER_DEPENDANT_DEDUCTION_NEGATIVE_ERROR);
			RuleFor(x => x.SalarySetting.InsurancePaidAmount).GreaterThanOrEqualTo(0).WithMessage(ValiationErrors.EMPPLOYER_DEPENDANT_DEDUCTION_NEGATIVE_ERROR);
			RuleFor(x => x.SalarySetting.Currency).NotEmpty().WithMessage(ValiationErrors.CURRENCY_EMPTY_ERROR);
		}
	}
}
