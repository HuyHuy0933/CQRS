using System.Threading.Tasks;
using SalaryCalculator.Core.Domain;
using SalaryCalculator.Core.Framework;

namespace SalaryCalculator.Core.Repository
{
	public interface ISalaryConfigRepository
	{
        Task<Result<SalaryConfig>> GetAsync();

        Task<Result<Nothing>> SaveAsync(SalaryConfig salaryConfig);
	}
}