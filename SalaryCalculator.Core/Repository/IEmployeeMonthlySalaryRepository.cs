using SalaryCalculator.Core.Domain;
using SalaryCalculator.Core.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SalaryCalculator.Core.Repository
{
	public interface IEmployeeMonthlySalaryRepository
	{
		Task<Result<PayrollReportRecord[]>> GetAsync(string key, string yearMonth);
		Task<Result<Nothing>> SaveAsync(PayrollReportRecord[] salaries, 
			string key, string yearMonth);
		Task<Result<Nothing>> MarkNotLatestAsync(string yearMonth);
	}
}
