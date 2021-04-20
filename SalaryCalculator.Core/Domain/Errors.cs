using SalaryCalculator.Core.Framework;

namespace SalaryCalculator.Core.Domain
{
	public static class Errors
	{
		private const string GETSALARYCONFIG = "getsalaryconfig";
		private const string SAVESALARYCONFIG = "savesalaryconfig";
		private const string GETSALARYCONFIG_SALARYSETING_NOTFOUND = "getsalaryconfig_salarysetting_notfound";
		private const string SAVESALARYCONFIG_CURRENCY_NOTFOUND = "savesalaryconfig_currency_notfound";
		private const string SAVESALARYCONFIG_REQUESTMODEL_NULL = "savesalaryconfig_requestmodel_null";
		private const string GET_RECORD_DB = "get_record_db";
		private const string ANY_RECORD_DB = "any_record_db";
		private const string EXIST_RECORD_DB = "exist_record_db";
		private const string GET_RECORD_KEY_INVALID = "get_record_key_invalid";
		private const string GET_RECORD_NOTFOUND = "get_record_notfound";
		private const string GET_RECORD_YEARMONTH_INVALID = "get_record_yearmonth_invalid";
		private const string SAVE_RECORD_NOTFOUND = "save_record_notfound";
		private const string SAVE_RECORD_DB = "sav_record_db";
		private const string SAVE_RECORD_KEY_INVALID = "save_record_key_invalid";
		private const string SAVE_RECORD_REQUESTMODEL_NULL = "save_record_requestmodel_null";
		private const string DELETE_RECORD_NOTFOUND = "delete_record_notfound";
		private const string DELETE_RECORD_DB = "delete_record_db";
		private const string DELETE_RECORD_KEY_INVALID = "delete_record_key_invalid";
		private const string PREVIEW_RECORD_EMPTY = "preview_record_empty";
		private const string PREVIEW_PROFILE_FEWER_THAN_RECORD = "preview_profile_fewer_than_record";
		private const string PREVIEW_RECORD_EMAIL_NOT_EXIST_PROFILE_EMAIL = "preview_record_email_not_exist_profile_email";
		private const string GET_PROFILE_DB = "get_profile_db";
		private const string GET_PROFILE_EMPTY = "get_profile_empty";
		private const string GET_PROFILE_YEARMONTH_INVALID = "get_profile_yearmonth_invalid";
		private const string SAVE_SALARY_DB = "save_salary_db";
		private const string SAVE_SALARY_KEY_INVALID = "save_salary_key_invalid";
		private const string SAVE_SALARY_RECORD_NULL = "save_salary_record_null";
		private const string GET_SALARY_KEY_INVALID = "get_salary_key_invalid";
		private const string GET_SALARY_DB = "get_salary_db";
		private const string MARK_NOT_LATEST_DB = "mark_not_latest_db";
		private const string EXPORT_CSV = "EXPORT_CSV";


		public static class SalaryConfig
		{
			public static Error GetSalaryConfigDatabaseError => new Error(GETSALARYCONFIG, 
				"GetSalaryConfig: Unable to get salary config.");
			public static Error GetSalaryConfigSalarySettingNotFoundError => new Error(GETSALARYCONFIG_SALARYSETING_NOTFOUND, 
				"GetSalaryConfig: SalarySetting not found.");
			public static Error SaveSalaryConfigDatabaseError => new Error(SAVESALARYCONFIG, 
				"SaveSalaryConfig: Unable to save salary config.");
			public static Error SaveSalaryConfigCurrencyNotFoundError => new Error(SAVESALARYCONFIG_CURRENCY_NOTFOUND, 
				"SaveSalaryConfig: Currency not found.");
			public static Error SaveSalaryConfigRequestModelNullError => new Error(SAVESALARYCONFIG_REQUESTMODEL_NULL, 
				"SaveSalaryConfig: request model null.");

		}

		public static class EmpMonthlyEnteredRecord
		{
			public static Error GetRecordDatabaseError => new Error(GET_RECORD_DB,
				"GetEmpMonthlyEntetedRecord: Unable to get record.");
			public static Error GetRecordNotFoundError =>
				new Error(GET_RECORD_NOTFOUND, "GetEmpMonthlyEntetedRecord: Record not found.");
			public static Error GetRecordKeyInvalidError =>
				new Error(GET_RECORD_KEY_INVALID, "GetEmpMonthlyEntetedRecord: Invalid key.");
			public static Error GetRecordYearMonthInvalidError =>
				new Error(GET_RECORD_YEARMONTH_INVALID, "GetEmpMonthlyEntetedRecord: Invalid year and month.");
			public static Error ExistRecordDatabaseError => new Error(EXIST_RECORD_DB,
				"GetEmpMonthlyEntetedRecord: Unable to check record.");
			public static Error SaveRecordDatabaseError => new Error(SAVE_RECORD_DB,
				"SaveEmpMonthlyEntetedRecord: Unable to save record.");
			public static Error SaveRecordKeyInvalidError =>
				new Error(SAVE_RECORD_KEY_INVALID, "SaveEmpMonthlyEntetedRecord: Invalid key.");
			public static Error SaveRecordRequestModelNullError => new Error(SAVE_RECORD_REQUESTMODEL_NULL,
				"SaveEmpMonthlyEntetedRecord: request model null.");
			public static Error AnyRecordDatabaseError => new Error(ANY_RECORD_DB,
				"AnyRecordDatabaseError: Unable to any record.");
			public static Error DeleteRecordNotFoundError =>
				new Error(DELETE_RECORD_NOTFOUND, "DeleteEmpMonthlyEnteredRecord: Record not found.");
			public static Error DeleteRecordDatabaseError =>
				new Error(DELETE_RECORD_NOTFOUND, "DeleteEmpMonthlyEnteredRecord: unable to delete record.");
			public static Error DeleteRecordInvalidKeyError =>
				new Error(DELETE_RECORD_NOTFOUND, "DeleteEmpMonthlyEnteredRecord: Invalid key.");

		}

		public static class PreviewEmpMonthlySalary
		{
			public static Error PreviewRecordEmptyError =>
				new Error(PREVIEW_RECORD_EMPTY, "PreviewEmpMonthlySalary: empty record.");
			public static Error PreviewProfileFewerThanRecordError =>
				new Error(PREVIEW_PROFILE_FEWER_THAN_RECORD, "PreviewEmpMonthlySalary: profiles is fewer than records.");
			public static Error PreviewRecordEmailNotExistProfileEmailError =>
				new Error(PREVIEW_RECORD_EMAIL_NOT_EXIST_PROFILE_EMAIL, "PreviewEmpMonthlySalary: record email is not exist in profile emails.");
		}

		public static class EmployeeProfile
		{
			public static Error GetProfileDatabaseError => new Error(GET_PROFILE_DB,
				"EmployeeProfile: Unable to get profile.");
			public static Error GetProfileEmptyError => new Error(GET_PROFILE_EMPTY,
				"EmployeeProfile: empty profile.");
			public static Error GetProfileYearMonthInvalidError =>
				new Error(GET_PROFILE_YEARMONTH_INVALID, "EmployeeProfile: Invalid year and month.");
		}

		public static class EmpMonthlySalary
		{
			public static Error SaveSalaryDatabaseError => 
				new Error(SAVE_SALARY_DB, "EmpMonthlySalary: Unable to save salary.");
			public static Error SaveSalaryKeyInvalidError => 
				new Error(SAVE_SALARY_KEY_INVALID, "EmpMonthlySalary: Invalid key to save.");
			public static Error SaveSalaryRecordNullError =>
				new Error(SAVE_SALARY_RECORD_NULL, "EmpMonthlySalary: Current month record is null.");
			public static Error GetSalarySalarySettingNullError =>
				new Error(GETSALARYCONFIG_SALARYSETING_NOTFOUND, "EmpMonthlySalary:  SalarySetting not found.");
			public static Error GetSalaryDatabaseError =>
				new Error(GET_SALARY_DB, "EmpMonthlySalary: Unable to get salary.");
			public static Error GetSalaryKeyInvalidError =>
				new Error(GET_SALARY_KEY_INVALID, "EmpMonthlySalary: Invalid key to get.");
			public static Error MarkNotLatestDatabaseError =>
				new Error(MARK_NOT_LATEST_DB, "EmpMonthlySalary: Unable to mark not latest salary.");

		}

		public static class ExportEmpMonthlySalary
		{
			public static Error ExportWriteCSVError =>
				new Error(EXPORT_CSV, "ExportEmpMonthlySalary: export salary to csv fail.");
		}
		// specify error by domain
	}
}