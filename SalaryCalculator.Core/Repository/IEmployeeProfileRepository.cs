using SalaryCalculator.Core.Domain;
using SalaryCalculator.Core.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SalaryCalculator.Core.Repository
{
	public interface IEmployeeProfileRepository
	{
		Task<Result<List<EmployeeProfile>>> GetAsync(string yearMonth);
		Task<Result<Nothing>> SaveAsync(EmployeeProfile profile, DateTime sendDate);
		Task<Result<Nothing>> DeleteAsync(string email, DateTime sendDate);
	}
}
