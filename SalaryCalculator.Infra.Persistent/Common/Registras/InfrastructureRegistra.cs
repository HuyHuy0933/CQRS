using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SalaryCalculator.Core.Repository;
using SalaryCalculator.Infra.Persistent.Repository;

namespace SalaryCalculator.Infra.Persistent.Common.Registras
{
	public static class InfrastructureRegistra
	{
		public static IServiceCollection AddInfrastructure(this IServiceCollection services,
			IConfiguration configuration)
		{
			services.AddDbContext<SalaryCalculatorDbContext>(options =>
				options.UseSqlServer(
					configuration["ConnectionString"],
					b => b.MigrationsAssembly(typeof(SalaryCalculatorDbContext).Assembly.FullName)));

			services.AddScoped<ISalaryConfigRepository, SalaryConfigRepository>();
			services.AddScoped<IEmployeeMonthlyEnteredRecordRepository, EmployeeMonthlyEnteredRecordRepository>();
			services.AddScoped<IEmployeeProfileRepository, EmployeeProfileRepository>();
			services.AddScoped<IEmployeeMonthlySalaryRepository, EmployeeMonthlySalaryRepository>();

			return services;
		}
	}
}
