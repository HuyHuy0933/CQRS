using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalaryCalculator.Infra.Persistent.Common.Extensions;
using SalaryCalculator.Infra.Persistent.Entities;

namespace SalaryCalculator.Infra.Persistent
{
	public class SalaryCalculatorDbContext : DbContext
	{
		public DbSet<SalarySetting> SalarySettings { get; set; }
		public DbSet<SalaryCurrency> SalaryCurrencies { get; set; }
		public DbSet<ProgressiveTaxRateSetting> ProgressiveTaxRateSettings { get; set; }
		public DbSet<EncryptedEmpMonthlyEnteredRecord> EncryptedEmpMonthlyEnteredRecords { get; set; }
		public DbSet<EncryptedEmpMonthlySalary> EncryptedEmpMonthlySalaries { get; set; }
		public DbSet<SyncEmployeeProfile> SyncEmployeeProfiles { get; set; }

		public SalaryCalculatorDbContext(
			DbContextOptions options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

			foreach (var entityType in modelBuilder.Model.GetEntityTypes())
			{
				//other automated configurations left out
				if (typeof(EntityBase).IsAssignableFrom(entityType.ClrType))
				{
					entityType.AddSoftDeleteQueryFilter();
				}
			}
		}

		public Task<int> SaveChangesAsync()
		{
			foreach (var entry in base.ChangeTracker.Entries<EntityBase>())
			{
				switch (entry.State)
				{
					case EntityState.Added:
						entry.Entity.CreatedDate = DateTime.UtcNow;
						break;
					case EntityState.Modified:
						entry.Entity.UpdatedDate = DateTime.UtcNow;
						break;
				}
			}

			return base.SaveChangesAsync(CancellationToken.None);
		}
	}
}