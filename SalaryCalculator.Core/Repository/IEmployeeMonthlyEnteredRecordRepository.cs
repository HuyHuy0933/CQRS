using SalaryCalculator.Core.Domain;
using SalaryCalculator.Core.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SalaryCalculator.Core.Repository
{
	public interface IEmployeeMonthlyEnteredRecordRepository
	{
		Task<Result<(List<EmployeeMonthlyEnteredRecord> records, int standardWorkingDays)>> GetAsync(string yearMonth, string key);
		Task<Result<Nothing>> SaveAsync(List<EmployeeMonthlyEnteredRecord> records, string yearMonth, string key, int standardWorkingDays);
		Task<Result<Nothing>> AddAsync(List<EmployeeMonthlyEnteredRecord> records, string yearMonth, string key, int standardWorkingDays);
		Task<Result<bool>> ExistRecordAsync(string yearMonth);
		Task<Result<bool>> AnyAsync();
		Task<Result<Nothing>> DeleteAsync(string key, string yearMonth);
	}
}
