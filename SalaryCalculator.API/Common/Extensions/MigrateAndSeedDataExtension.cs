using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SalaryCalculator.Infra.Persistent;

namespace SalaryCalculator.API.Common.Extensions
{
    public static class MigrateAndSeedDataExtension
    {
        public static void AddMigrationAndSeedData(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<SalaryCalculatorDbContext>();

                    if (context.Database.IsSqlServer())
                    {
                        context.Database.Migrate();
                    }
                    
                    SalaryCalculatorDbContextSeed.SeedDataAsync(context).GetAwaiter().GetResult();

                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {

                }
            }
        }
    }
}
