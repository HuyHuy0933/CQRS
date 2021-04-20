using MassTransit;
using Microsoft.Extensions.Logging;
using SalaryCalculator.Application.Common.Helpers;
using SalaryCalculator.Application.Models.Event;
using SalaryCalculator.Core.Domain;
using SalaryCalculator.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SalaryCalculator.Core.Common.Extensions;

namespace SalaryCalculator.Application.Events.Consumers
{
	public class EmployeeProfileChangedConsumer : IConsumer<EmployeeProfileChangedEvent>
	{
		private readonly ILogger<EmployeeProfileChangedConsumer> _logger;
		private readonly IEmployeeProfileRepository _profileRepo;
		private readonly IEmployeeMonthlySalaryRepository _salaryRepo;
		public EmployeeProfileChangedConsumer(
			ILogger<EmployeeProfileChangedConsumer> logger,
			IEmployeeProfileRepository profileRepo,
			IEmployeeMonthlySalaryRepository salaryRepo)
		{
			_logger = logger;
			_profileRepo = profileRepo;
			_salaryRepo = salaryRepo;
		}
		public async Task Consume(ConsumeContext<EmployeeProfileChangedEvent> context)
		{
			try
			{
				_logger.LogInformation("Start EmployeeProfileChangedConsumer");
				var profileChangedEvent = context?.Message;

				if (profileChangedEvent == null)
				{
					_logger.LogInformation("profileChangedEvent is null");
					_logger.LogInformation("End EmployeeProfileChangedConsumer");
					return;
				}

				var sendDate = profileChangedEvent.SendDate.ToDateTime();

				var profile = await _profileRepo.SaveAsync(new EmployeeProfile(
					email: profileChangedEvent.Email,
					fullname: profileChangedEvent.Fullname,
					employeeType: profileChangedEvent.EmployeeType,
					position: profileChangedEvent.Position,
					dependantEndDates: profileChangedEvent.DependantEndDates.ToDateTimeArray(),
					inUnion: profileChangedEvent.InUnion,
					terminatedDate: profileChangedEvent.TerminationDate.ToDateTimeOrNull(),
					isForeigner: profileChangedEvent.IsForeigner),
					sendDate);

				var yearMonth = sendDate.ToString("yyyy-MM");
				var result = await profile.MapAsync(x => _salaryRepo.MarkNotLatestAsync(yearMonth));
				if (result.Failure) _logger.LogError(result.Error.Message);

				_logger.LogInformation("End EmployeeProfileChangedConsumer");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				_logger.LogInformation("End EmployeeProfileChangedConsumer");
			}
		}
	}
}
