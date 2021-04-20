using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SalaryCalculator.Core.Common;
using SalaryCalculator.Core.Domain;
using SalaryCalculator.Core.Framework;
using SalaryCalculator.Core.Repository;
using SalaryCalculator.Core.ValueObjects;
using SalaryCalculator.Infra.Persistent.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SalaryCalculator.Infra.Persistent.Repository
{
	public class EmployeeProfileRepository : IEmployeeProfileRepository
	{
		private readonly SalaryCalculatorDbContext _dbContext;
		private readonly ILogger<EmployeeProfileRepository> _logger;

		public EmployeeProfileRepository(
			SalaryCalculatorDbContext dbContext,
			ILogger<EmployeeProfileRepository> logger)
		{
			_dbContext = dbContext;
			_logger = logger;
		}

		public async Task<Result<Nothing>> DeleteAsync(string email, DateTime sendDate)
		{
			var existingEmployee = await _dbContext.SyncEmployeeProfiles
				.SingleOrDefaultAsync(x => x.Email == email);

			if(existingEmployee != null && existingEmployee.UpdatedDate < sendDate)
			{
				existingEmployee.IsDeleted = true;
				_dbContext.SyncEmployeeProfiles.Update(existingEmployee);
				await _dbContext.SaveChangesAsync();
			}

			return Result<Nothing>.Ok(Nothing.Value);
		}

		public async Task<Result<List<EmployeeProfile>>> GetAsync(string yearMonth)
		{
			try
			{
				if (YearMonth.TryParse(yearMonth, out YearMonth selectedYearMonth) is false)
				{
					_logger.LogError(Errors.EmployeeProfile.GetProfileYearMonthInvalidError.Message);
					return Errors.EmployeeProfile.GetProfileYearMonthInvalidError;
				}

				//todo enhancement
				var profiles = await _dbContext.SyncEmployeeProfiles.ToListAsync();
				var filteredProfiles = profiles.Where(x => x.TerminatedDate == null 
					|| (x.TerminatedDate != null && new YearMonth(x.TerminatedDate.Value) >= selectedYearMonth)).ToList();

				if (filteredProfiles.Count == 0)
				{
					_logger.LogError(Errors.EmployeeProfile.GetProfileEmptyError.Message);
					return Errors.EmployeeProfile.GetProfileEmptyError;
				}

				return Result<List<EmployeeProfile>>.Ok(filteredProfiles.Select(x => 
				{
					var dependantEndDates = string.IsNullOrEmpty(x.DependantEndDates) ? Array.Empty<DateTime>() 
						: JsonSerializer.Deserialize<DateTime[]>(x.DependantEndDates);
					return new EmployeeProfile(
						email: x.Email,
						fullname: x.FullName,
						employeeType: x.EmployeeType,
						position: x.Position,
						dependantEndDates: dependantEndDates.Length == 0 ? dependantEndDates
							: dependantEndDates.Where(x => x == DateTime.MinValue || new YearMonth(x) > new YearMonth(DateTime.UtcNow))
							.ToArray(),
						inUnion: x.InUnion,
						terminatedDate: x.TerminatedDate,
						isForeigner: x.IsForeigner);
				}).ToList());
			}
			catch (SqlException ex)
			{
				_logger.LogError(ex, Errors.EmployeeProfile.GetProfileDatabaseError.Message);
				return Errors.EmployeeProfile.GetProfileDatabaseError;
			}
		}

		public async Task<Result<Nothing>> SaveAsync(EmployeeProfile profile, DateTime sendDate)
		{
			var existingEmployee = await _dbContext.SyncEmployeeProfiles.IgnoreQueryFilters()
				.FirstOrDefaultAsync(x => x.Email == profile.Email);

			if(existingEmployee == null)
			{
				_dbContext.SyncEmployeeProfiles.Add(new SyncEmployeeProfile
				{
					Email = profile.Email,
					EmployeeType = profile.EmployeeType,
					FullName = profile.Fullname,
					InUnion = false,
					DependantEndDates = JsonSerializer.Serialize(Array.Empty<DateTime>()),
					Position = profile.Position,
					IsDeleted = false,
					TerminatedDate = profile.TerminatedDate,
					IsForeigner = profile.IsForeigner
				});

				await _dbContext.SaveChangesAsync();
				return Result<Nothing>.Ok(Nothing.Value);
			}

			if(existingEmployee.UpdatedDate < sendDate)
			{
				existingEmployee.FullName = profile.Fullname;
				existingEmployee.Position = profile.Position;
				existingEmployee.EmployeeType = profile.EmployeeType;
				existingEmployee.DependantEndDates = profile.EmployeeType == nameof(EmployeeTypeEnum.Permanent) 
					? JsonSerializer.Serialize(profile.DependantEndDates) : JsonSerializer.Serialize(Array.Empty<DateTime>());
				existingEmployee.InUnion = profile.InUnion;
				existingEmployee.TerminatedDate = profile.TerminatedDate;
				existingEmployee.IsForeigner = profile.IsForeigner;
				existingEmployee.IsDeleted = false;
				_dbContext.SyncEmployeeProfiles.Update(existingEmployee);
				await _dbContext.SaveChangesAsync();
			}

			return Result<Nothing>.Ok(Nothing.Value);
		}
	}
}
