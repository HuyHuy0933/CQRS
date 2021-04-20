using System;
using System.Collections.Generic;
using System.Text;

namespace SalaryCalculator.Application.Common.Validations
{
	public class ValiationErrors
	{
		
		public const string YEAR_MONTH_NULL_ERROR = "year_month_null_error";
		public const string YEAR_MONTH_INVALID_FORMAT_ERROR = "year_month_invalid_format_error";
		public const string PROGRESSIVE_TAX_RATE_EMPTY_ERROR = "progressive_tax_rate_null_error";
		public const string PROGRESSIVE_TAX_RATE_DUPLICATED_ERROR = "progressive_tax_rate_duplicated_error";
		public const string PROGRESSIVE_TAX_RATE_LEVEL_INVALID_ENUM_ERROR = "progressive_tax_rate_level_invalid_enum_error";
		public const string LOWER_BOUND_NEGATIVE_ERROR = "lower_bound_negative_error";
		public const string UPPER_BOUND_NEGATIVE_ERROR = "upper_bound_negative_error";
		public const string PROGRESSIVE_TAX_RATE_NEGATIVE_ERROR = "progressive_tax_rate_negative_error";
		public const string SALARY_SETTING_NULL_ERROR = "salary_setting_null_error";
		public const string ID_EMPTY_ERROR = "id_empty_error";
		public const string COMMON_MINIUM_WAGE_NEGATIVE_ERROR = "common_minimum_wage_negative_error";
		public const string REGIONAL_MINIUM_WAGE_NEGATIVE_ERROR = "regional_minimum_wage_negative_error";
		public const string MINIUM_NON_WORKING_DAY_NEGATIVE_ERROR = "minimum_non_working_day_negative_error";
		public const string DEFAULT_TAX_RATE_NEGATIVE_ERROR = "default_tax_rate_negative_error";
		public const string COEFFICIENT_SOCIAL_CARE_NEGATIVE_ERROR = "coefficient_social_care_negative_error";
		public const string EMPPLOYEE_SOCIAL_INSURANCE_RATE_NEGATIVE_ERROR = "employee_social_insurance_rate_negative_error";
		public const string EMPPLOYEE_HEALTHCARE_INSURANCE_RATE_NEGATIVE_ERROR = "employee_healthcare_insurance_rate_negative_error";
		public const string EMPPLOYEE_UNEMPLOYMENT_INSURANCE_RATE_NEGATIVE_ERROR = "employee_unemployment_insurance_rate_negative_error";
		public const string EMPPLOYEE_UNION_FEE_RATE_NEGATIVE_ERROR = "employee_union_fee_rate_negative_error";
		public const string EMPPLOYER_SOCIAL_INSURANCE_RATE_NEGATIVE_ERROR = "employeR_social_insurance_rate_negative_error";
		public const string EMPPLOYER_HEALTHCARE_INSURANCE_RATE_NEGATIVE_ERROR = "employer_healthcare_insurance_rate_negative_error";
		public const string EMPPLOYER_UNEMPLOYMENT_INSURANCE_RATE_NEGATIVE_ERROR = "employer_unemployment_insurance_rate_negative_error";
		public const string EMPPLOYER_UNION_FEE_RATE_NEGATIVE_ERROR = "employer_union_fee_rate_negative_error";
		public const string EMPPLOYER_PERSONAL_DEDUCTION_NEGATIVE_ERROR = "employer_personal_deduction_negative_error";
		public const string EMPPLOYER_DEPENDANT_DEDUCTION_NEGATIVE_ERROR = "employer_dependant_deduction_negative_error";
		public const string INSURANCE_PAID_AMOUNT_NEGATIVE_ERROR = "insurance_paid_amount_negative_error";
		public const string CURRENCY_EMPTY_ERROR = "currency_empty_error";

		public const string KEY_EMPTY_ERROR = "key_empty_error";
		public const string STANDARD_WORKING_DAY_MIN_ERROR = "standard_working_day_min_must_be_1_day_error";
		public const string STANDARD_WORKING_DAY_MAX_ERROR = "standard_working_day_max_must_be_23_day_error";
		public const string RECORD_EMPTY_ERROR = "record_empty_error";
		public const string RECORD_DUPLICATED_ERROR = "record_duplicated_error";
		public const string EMAIL_INVALID_ERROR = "email_invalid_error";
		public const string FULLNAME_EMPTY_ERROR = "fullname_empty_error";
		public const string GROSS_CONTRACT_SALARY_NEGATIVE_ERROR = "gross_contract_salary_negative_error";
		public const string PROBATION_WORKING_DAY_NEGATIVE_ERROR = "probation_working_day_negative_error";
		public const string ACTUAL_WORKING_DAY_NEGATIVE_ERROR = "actual_working_day_negative_error";
		public const string NON_TAXABLE_ALLOWANCE_NEGATIVE_ERROR = "non_taxable_allowance_negative_error";
		public const string TAXABLE_ANNUAL_LEAVE_NEGATIVE_ERROR = "taxable_annual_leave_negative_error";
		public const string TAXABLE_13_MONTH_SALARY_NEGATIVE_ERROR = "taxable_13_month_salary_negative_error";
		public const string TAXABLE_OTHERS_NEGATIVE_ERROR = "taxable_others_negative_error";
		public const string PAYMENT_ADVANCE_NEGATIVE_ERROR = "payment_advance_negative_error";
		public const string ADJUSTMENT_DEDUCTION_NEGATIVE_ERROR = "adjustment_deduction_negative_error";
		public const string ADJUSTMENT_ADDITION_NEGATIVE_ERROR = "adjustment_addition_negative_error";
	}
}
